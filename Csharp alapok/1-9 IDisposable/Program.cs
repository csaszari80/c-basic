using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_9_IDisposable
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var tl =new TisztaLevego()) //Ahhoz, hogy a tisztalevego tipust a using kulcsszóval használhassuk a típusnak meg kell valósítani az IDisposable felületet
            {

            }

            ////A using-ot s fordító ilyen blokká fordítja (valami ilyesmit csinál a tl-el mint a következő rész a tl2-vel)
            //var tl2 = new TisztaLevego();
            //// A try ág kódblokkjában lévő kódot megpróbálja végrehajtani majd akár sikerül akár nem végrehajtja a finally ágban lévő kódot (catch blokkokal ez egyes hibatípusokat elfoghatjuk)
            //try
            //{
                
            //    // itt használjuk a t1 által hivatkozott példányt
            //}
            //finally
            //{
            //    //akármi történik ez a kódblokk lefut
            //    if (tl2 != null)
            //    {
            //        ((IDisposable)tl2).Dispose();
            //    }
                

            //}
        }
    }

    class TisztaLevego : IDisposable
    {
        //hogyan lehet szálbiztossá tenni egy osztályt? A metódusait és a propertyket (amelyeknek getter-e és settere van) szálbiztossá kell tenni
        public void Tennivalo()
        {
            if (isDisposed==1)
            {
                //ha már a dispose lefutott akkor hiba van, de ez még nem teljes megoldás
                throw new ObjectDisposedException(nameof(TisztaLevego));
            }
        }

        private int myProperty;
        public int MyProperty
        {
            get
            {
                if (isDisposed == 1)
                {
                    //ha már a dispose lefutott akkor hiba van, de ez még nem teljes megoldás
                    throw new ObjectDisposedException(nameof(TisztaLevego));
                }
                // a getter további feladatai
                return myProperty;
            }
            set
            {
                if (isDisposed == 1)
                {
                    //ha már a dispose lefutott akkor hiba van, de ez még nem teljes megoldás
                    throw new ObjectDisposedException(nameof(TisztaLevego));
                }
                // A setter további feladatai
            }
        }

        //Egy osztály sokféle információt tartalmazhat
        //Mikor kell megvalósítani az Idisposable-t

        //1. Mindenképp  biztosítanunk kell (illik) az Idisposable felület elérését ha olyan objektumokat használunk az oasztályunkban amelynek van finalizere pl stream típusú objektumok
        Stream managedStream = new FileStream("testfile.txt",FileMode.Create);

        //2. Ha nem menedzselt memóriát használunk pl: pointerekkel közvetlenül írt memória területek az ilyenhivatkozások kikerülnek a GC hatásköre alól ezért csak finalizerrel tudjuk megoldani a memória megfelelő felszabadítását
        // Ha finalizert használunk akkor viszont Idisposable -t is használnunk kell (különben mellékhatások jelentkeznek
        IntPtr nonManagedMemory = IntPtr.Zero;

        //3. nagyméretű menedzselt memóriát használunk
        List<string> managedMemory = new List<string>();
        

        /// <summary>
        /// A konstruktorban értéket adunk a változóinknak
        /// </summary>
        public TisztaLevego()
        {
            // A stream-et már lerendeztük annak már nem kell értéket adni

            //Nem menedzselt memória
            nonManagedMemory = Marshal.AllocHGlobal(1000000); //lefoglalunk ennyi bájtot
            GC.AddMemoryPressure(1000000); //Szólunk a GC-nek hogy ennyi bájttal kevesebb a menedzselt memória (mivel nem tud a nem menedzselt tevékenységemről)

            // feltöltjük a nagyméretű menedzselt memóriát
            for (int i = 0; i < 1000000; i++)
            {
                managedMemory.Add(new string('A', 1));
            }

        }


        //Megvalósítjuk az Idisposable felületet-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Az Idisposable felület megvalósításához szükséges ez a függvény (ez fut le az objektum megszűnésekor ha a using-ot használjuk 
        /// </summary>
        public void Dispose()
        {
            Dispose(true); //ld. lentebb

            //Mivel ez a normális ügymenet a Dispose függvény gondoskodik a memória felszabadításáról ezért a GC-nek jelezni kell, hogy bár van finalizer de mégsincs szükség a kétlépcsős takarításra
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// finalizer  ez akkor fut le, ha az objektum esetében nem használtuk a using kulcszót (az Idisposable felületet) és az objektum eltakarításra kerül
        /// mivel ugyanazt kell csinálnia mint a felületet megvalósító publikus dispose függvénynek és nem akarunk másolni ezért létrehozunk egy privát Dispose() függvényt 
        /// </summary>
        ~TisztaLevego() // 
        {
            Dispose(false); //ld lentebb
        }

        /// <summary>
        /// Ebben a függvényben írjuk le, hogy minek kell történni a finalizerben illetve a publikus Dispose függvényben a paraméter azért kell, hogy tudjuk, hogy melyik ág hívta meg. 
        /// Jelen esetben(ez a szokás is) ha a paraméter true akkor a publikus Dispose (using-al használták az osztályt)
        /// Ha a paraméter false akkor a finalizer hívta meg
        /// </summary>

        //private bool isDisposed =false;
        private int isDisposed=0;
        

        private void Dispose(bool dispose)
        {
            // Ez a megoldás biztosíthatja hogy ne fusson kétszer ez a függvény  de nem szálbiztos (ebben az isDisposed változónak boolean-nak kell lenni
            //if (!isDisposed)
            //{
            //    throw new ObjectDisposedException(nameof(TisztaLevego)); //ha már egyszer lefutott akkor hibát dobb az objektumtípus nevével (stringet vár ezért a nameof függvényt használjuk. beírhatnánk idézőjelbe is azonban akkor ha megváltoztatnánk az osztály nevét akkor nem jelezné nekünk a fordító hogy itt is kell változtatni
            //}
            //isDisposed = true; 

            /// ez már akkor is jó ha több szálon fut a program több megoldás is van ez egy viszonylag elegáns lehetőség a függvény első paraméterbe változót átírja a második paraméterben megadott értékre és visszatér az eredeti értékkel ha több szál is végre akarja hajtani akkor egyszerre csak egynek engedi 
            /// ez viszont nem működik booleannal
            If (Interlocked.Exchange(ref isDisposed, 1) == 1)
            {
                throw new ObjectDisposedException(nameof(TisztaLevego)); //ha már egyszer lefutott akkor hibát dobb az objektumtípus nevével (stringet vár ezért a nameof függvényt használjuk. beírhatnánk idézőjelbe is azonban akkor ha megváltoztatnánk az osztály nevét akkor nem jelezné nekünk a fordító hogy itt is kell változtatni
            }

            if (dispose)
            {
                //// Ilyenkor még ne futott le a GC mivel én szeretném felügyelni a memória kipucolását ezért 
                //// kitakarítom a felhasznált menedzselt objektumokat
                //// managedMemory.Clear(); // ez felszabadítja a memóriát
                //// managedMemory = null; //ez megszűnteti a hivatkozást

                ///// Az előző kettő egymás után olyan problémát okozhat, hogy ha a dispose valamiért véletlenül kétszer van meghívva ugyanarra az objektumra akkor a második zeroreference error-ra fut( az első futás megszüntette az objektumhivatkozást)
                ///// Ezt kétféleképp küszöbölhetjük ki ez első: (gyakran látni ilyet nem biztos, hogy ez a legjobb)
                //if (managedMemory!=null) //ha még llétezik a hivatkozás csak akkor szabadítjuk fel és szüntetjük meg
                //{
                //    managedMemory.Clear(); // ez felszabadítja a memóriát
                //    managedMemory = null; //ez megszűnteti a hivatkozást
                //}

                // A második hogy csak felszabadítjuk és más módon zárjuk ki annak lehetőségét, hogy a dispose függvényt kétszer hívják meg, (vagy több szálon futó kódból egyszerre) ld. a függvény elejét
                managedMemory.Clear(); 
                

                // a stream esetében a stream osztály dispose függvényét használjuk (itt is ugyanaz a két lehetőségünk van a hivatkozás megszüntetésére a másodikat használjuk)
                managedStream.Dispose();
               
            }

            //nem menedzselt memória takarítása (ennek akkor is meg kell történnie, ha a GC lefutott és a menedzselt memória már felszabadult
            Marshal.FreeHGlobal(nonManagedMemory);
            GC.RemoveMemoryPressure(1000000); //A Gc- nek is jelezzük, hogy már nem tartunk igényt a memóriaterületre
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------
    }
}
