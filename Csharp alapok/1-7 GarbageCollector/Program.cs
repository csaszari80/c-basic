using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// A Garbage Collektor generációkat kezel
/// 0. Azok a Heap-en lévő objektumok amelyeken még nem futott a szemétgyűjtés (A leggyakrabban ezekből kell selejtezni)
/// 1. Azok melyeken már gy szemétgyűjtő ciklus lefutott de nem kellett őket kitakarítani.
/// 2. Azoka amelyek már két ciklust is "túléltek", ezekre fut legritkábban a szemétgyűjtés (mivel ami már két ciklust túlélt arra valószínűleg hosszabb távon szükség van


namespace _1_7_GarbageCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            //GC osztály tartalmazza a legfontosabb GC-vel kapcsolatos funkciókat
            var alap = new Alap();
            //var leszarmaztatott = new Leszarmaztatott();
            Console.WriteLine(GC.GetGeneration(alap)); // generáció lekérdezése
            GC.Collect(); // szemétgyűjtés kikényszerítése (négyféle szignatúrája van ez a legegyszerűbb minden generációra lefuttatja)
            Console.WriteLine(GC.GetGeneration(alap)); 
            GC.Collect();
            Console.WriteLine(GC.GetGeneration(alap));
            GC.Collect();
            Console.WriteLine(GC.GetGeneration(alap));
            GC.Collect();
            Console.WriteLine(GC.GetGeneration(alap));
            GC.Collect();
            Console.WriteLine(GC.GetGeneration(alap));
            Console.WriteLine("Alap -> null :");
            alap = null; // megszüntetkük az alap-ot így lefut a finalizer
            //Console.WriteLine("Leszármaztatott -> null :");
            //leszarmaztatott = null;
            GC.Collect();

            Console.ReadKey();
            Console.WriteLine("Szemétgyűjtés lefutott");

            //Az hogy egy objektum mennyi helyet foglal a memóriában nem kérdezhető le közvetlenül de az elfoglalt memória lekérdezésével meghatározható
            Console.WriteLine("A lefoglalt memória {0}",GC.GetTotalMemory(false));  //lekérdezzük az elfoglalt memóriát úgy, hogy előtte nem végez szemétgyűjtést
            var lista = new List<string>();
            //létrehozunk egy 1000 string-ből álló listát amelynek mindegyik eleme 6000 "A" karakterből áll
            for (int i = 0; i < 1000; i++)
            {
                lista.Add(new string('A', 6000));
            }
            Console.WriteLine("A lefoglalt memória {0}", GC.GetTotalMemory(false));  //lekérdezzük az elfoglalt memóriát úgy, hogy előtte nem végez szemétgyűjtést


            for (int i = 0; i < 3; i++)
            {
                var leszarmaztatott = new Leszarmaztatott(i); // mivel ezt a változót kódblokkon belül jön létre ezért a ciklus végén ennek elvileg meg is kellene szűnnie még akkor is ha megkülönböztetjük őket
                                                              // Valójában az utolsó példány nem szűnik meg az ő finalizere nem fut le csak a programfutás végén.
                                                              //ez a debug mód miatt van így csak akkor szünteti meg a példányra való hivatkozást ha már felülírtuk vagy ha kilép a függvényből (Main())
                                                              //release módban egy rebuild után az elvárt módon fut vagy debug módban cikluson belül nullra kell irányítani az objektumpéldányt
               //leszarmaztatott = null;
            }
            GC.Collect();

            //Kövessük nyomon egy hivatkozás korosítását menet közben
            var lista2 = new List<string>();
            //létrehozunk egy 2000 string-ből álló listát amelynek mindegyik eleme 6000 "A" karakterből áll


            for (int i = 0; i < 2000; i++)
            {
                Thread.Sleep(10);
                lista2.Add(new string('A', 6000));
                Console.Write(GC.GetGeneration(lista2));
            }

            Console.ReadKey();








        }
        class Alap
        {
           protected int i; // így ezt a változót az osztályon belül és a leszármaztatott osztályon belül látszik

            public Alap()
            {
                this.i = -10;
            }

            public Alap(int i)
            {
                this.i = i;
            }

            ~Alap()
            {
                Console.WriteLine("Alap véglegesítő: {0}",i);
            }
        }
        
        class Leszarmaztatott : Alap
        {
            public Leszarmaztatott(int i) :base(i)
            {
             
            }

            ~Leszarmaztatott()
            {
                Console.WriteLine("Leszármaztatott véglegesítő: {0}", i);
            }
        }
    }
}
