using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_10_IEnumerable_típusos
{
    class Program
    {
        static void Main(string[] args)
        {
            // agyűjtemény létrehozásakor most megkerüljük az add függvényt a megfelelő konstruktorok generálásával és hívásával ez nem feltétlenül egyszerűbb
            var adatok = new BejarhatoAdatok<Adatok>(
                new Adatok[] {
                    new Adatok(nev: "Első", szam: 1),
                    new Adatok(nev: "Második", szam: 2),
                    new Adatok(nev: "Harmadik", szam: 3),
                    new Adatok(nev: "Negyedik", szam: 4)
                }
            );

            // FONTOS: foreach cikluban a listát nem módosítjuk (lista típusnál hibát is dob rá)
            foreach (var item in adatok)
            {
                Console.WriteLine("Név: {0}, Szám: {1}",item.Nev,item.Szam);
            }

            Console.ReadKey();
        }
    }

    /// <summary>
    /// Egy IEnumerable felületet megvalósító osztály ezúttal szigorúan típusos megvalósítással
    /// Ebben az implementálni kell típusos és típus nélküli GetEnumerator függvényeket is.
    /// Továbbá a bejárást is osztályon belül oldjuk meg, Ehhez pedig nem csak az IEnumerable felületet, hanem az IEnumerator felületet is meg kell valósítani
    /// </summary>
    /// az osztályhivatkozásnak típusparamétert kell kapnia amit továbbad az IEnumerable és az IEnumerator felületnek
    class BejarhatoAdatok<T> : IEnumerable<T>, IEnumerator<T>
    {
        List<T> lista = new List<T>();
        int pozicio = -1;
        
        /// <summary>
        /// Ez a konstruktor amilyen típusó tömböt kap eredményű olyan típusú lesz a lista hoz létre
        /// </summary>
        /// <param name="adatok"></param>
        public BejarhatoAdatok(T[] adatok)
        {
            lista = new List<T>(adatok);
            
        }

        public T Current
        {
            get
            {
                return lista[pozicio];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        /// <summary>
        /// Ezt most nem valósítjuk meg mert macerás és nem ez a cél és tudom hogy nem szükséges
        /// üresnek kell lennie mert a ciklusból való kilépéskor az objektum is meg kell szűnön
        /// </summary>
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

      

        public bool MoveNext()
        {
            pozicio++;
            return pozicio < lista.Count();
        }

        public void Reset()
        {
            pozicio = -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }

    /// <summary>
    /// Ezt az osztályt fogjuk gyűjteményben használni a lényeg, hogy nem csak egyféle tulajdonsága van így nem lehet ugyanúgy használni mint az előzőt(feldolgozáshoz castolni kell vagy a megfelelően kell megvalósítani a gyűjtemény bejárását
    /// </summary>
    class Adatok
    {
        public Adatok(string nev, int szam)
        {
            Nev = nev;
            Szam = szam;
        }

        public string Nev { get; set; }

        public int Szam { get; set; }
    }
    
    

}
