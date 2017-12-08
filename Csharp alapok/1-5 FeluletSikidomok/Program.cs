using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_5_Letrehozo_Finalizer //A névtér neve mindig betűvel vagy _-al kezdődik
{
    //Teljes elérés: _1_5_FeluletSikidomok.Program
    class Program
    {
        static void Main(string[] args)
        {
            var alap = new Alap();
            Console.WriteLine();
            var leszarmaztatott = new Leszarmaztatott();
            Console.WriteLine();
            var tovabbszarmaztatott = new TovabbSzarmaztatott();
            Console.WriteLine();

            var alap2 = new Alap("Kezdőéerték");
            Console.WriteLine();

            var alap3 = new Alap("Ez itt a nev", "Ez itt a cim");
            Console.WriteLine();

            Console.ReadKey();
        }
        class Alap
        {
            // Ha nincs külön implementálva akkor a fordító automatikusan létrehoz egy paraméter nélküli alapértelmezett konstruktort
            // konstruktor létrehozása gyorsan ctor tab tab
            public Alap() // visszatérési érték nélküli publikus függvény aminek ugyanaz a neve mint az osztálynak
            {
                Console.WriteLine("Alap konstruktor");
            }

            //Egy osztálynak több konstruktora is lehet eltérő szignatúrával pl olyan esetben ha van amikor be akarunk állítani egy tulajdonságot valamikor meg nem. Konstruktor overloading
            string Nev;
            string Cim;

            public Alap(string nev)
            {
                
                Console.WriteLine("Alap konstruktor 2: {0}", nev);
                Nev = nev;
                
            }

            //Megcsinálhatjuk így is
            //public Alap(string nev, string cim)
            //{

            //    Nev = nev;
            //    Cim = cim;
            //    Console.WriteLine("Alap konstruktor 3: {0}; {1}", Nev, Cim);


            //}
            //Vagy felhasználhatjuk egy előző konstruktor munkáját
            public Alap(string nev, string cim) : this(nev) //ez meghivja a parameterrel az elozo konstruktort
            {
               
                Console.WriteLine("Alap konstruktor 3: {0}; {1}", nev, cim);
                Cim = cim;
            }

            //A másik speciális függvény a Finalizer ami akkor fut ha az osztálypéldány befejezte életét. Ha implementálhatjuk akkor van. Ha nincs akkor a szemétgyűjtő alapértelmezett módon szünteti meg az objektumokat
            // csak omegfelelő indokkal, és módszerrel (IDisposable) használjuk részletesen később
            // gyors létrehozás: ~ tab
            // Ha ctrl+F5-el futtatjuk ezt a programot akkor láthatjuk a működést
            ~Alap() // a finalizer nem meghívató csak a futtatókörnyezet hívhatja meg ezért se visszatérési értéktípusa se láthatósági szabálya nincs 
            {
                Console.WriteLine("Alap finalizer");
            }
        }

        class Leszarmaztatott : Alap
        {
            public Leszarmaztatott()
            {
                Console.WriteLine("Leszarmaztatott konstruktor");
            }
            ~Leszarmaztatott()
            {
                Console.WriteLine("Leszarmaztatott finalizer");
            }
        }
        class TovabbSzarmaztatott : Leszarmaztatott
        {
            public TovabbSzarmaztatott()
            {
                Console.WriteLine("Tovabbszarmaztatott konstruktor");
            }
            ~TovabbSzarmaztatott()
            {
                Console.WriteLine("Tovabbszarmaztatott finalizer");
            }
        }
    }
}
