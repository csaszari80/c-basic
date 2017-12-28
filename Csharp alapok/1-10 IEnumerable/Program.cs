using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_10_IEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            //foreach (var listaElem in BevasarloLista())
            //{
            //    Console.WriteLine(listaElem);
            //}

            //Az előzővel Valami ilyesmit szeretnénk elérni
            //Console.WriteLine("1 kg kenyér");
            //Console.WriteLine("10 dkg felvágott");
            //Console.WriteLine("1 kg liszt");
            //Console.WriteLine("1 l tej");
            //Console.WriteLine("Üdítő");
            //Console.WriteLine("Nasi");
            //Console.WriteLine("1 kg Darálthús");
            //Console.WriteLine("1 kg tészta");


            // feltöltjük a bejárható osztályt listaelemekkel
            var lista = new BejarhatoOsztaly();
            lista.Add("első");
            lista.Add("második");
            lista.Add("harmadik");
            lista.Add("negyedik");
            lista.Add("ötödik");
            lista.Add("hatodik");

            foreach (var elem in lista)
            {
                Console.WriteLine("-------foreach elem: {0}", elem); //bár objektumot kapunk vissza (nem típusos a megvalósítás) de mivel stringet tartalmaz ezért a cw tudja kezelni cast-olás nélkül is
            }

            //Console.WriteLine("--------------------------------------------------------------");

            // A foreach lényegében ezt csinálja (ennél persze bonyolultabb de a lényege ez)
            //var bejaro = lista.GetEnumerator();
            //    while (bejaro.MoveNext())
            //    {
            //        var elem2 = bejaro.Current;
            //        Console.WriteLine("-------while elem2: {0}", elem2);
            //    }

            Console.ReadKey();
        }
         

        private static IEnumerable<string> BevasarloLista() //speciális függvény, az IEnumareble objektumlisták bejárását szolgálja ki jelen esetben stringeket ad vissza a foreach mindig a következő yield return értéket adja eredményül
            /// az IEnumareable egy generikus osztály (paraméterei között típus is szerepel (<string>) ilyen még pl a List<típus>) ilyen módon használva szigorúan típusfüggő van nem típusfüggő változata is (nincs <típus>) ekkor object-eket ad vissza 
        {
            yield return "1 kg kenyér";
            yield return "10 dkg felvágott";
            yield return "1 kg liszt";
            yield return "1 l tej";
            yield return "Üdítő";
            yield return "Nasi";
            yield return "1 kg Darálthús";
            yield return "1 kg tészta";
        }

        /// <summary>
        /// Egy IEnumerable felületet megvalósító osztály (típus nélkül)
        /// Ebben az esetben objektumokat kapunk vissza a régebbi ADONET osztálykönvtárakban találkozahtunk vele hátrányam hogy a feldolgozáshoz megfelelően cast-olni kell ehhez pedik pontosan kell tudni mivel tér vissza
        /// </summary>
        class BejarhatoOsztaly : IEnumerable
        {
            List<string> lista = new List<string>(); // ez a stringekból álló lista nevű változó fogja tartalmazni az adatokat egyúttal példányosítva is van

            //Mivel nem akarom publikálni a listát ezért kell egy publikus függvény amivel elemeket lehet hozzáadni
            public void Add(string elem)
            {
                lista.Add(elem);
            }
            
            /// <summary>
            /// Az enumerátornak be kell tudni járni az osztály gyűjteményét
            /// Ezt megoldhatjuk külön osztályban this használatával
            /// Most a Bejaro osztály segítségével fogjuk elvégezni a bejárást.
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                Console.WriteLine("     Getenumerator hívása");
                return new Bejaro(lista);
            }
        }
        class Bejaro : IEnumerator  //A bejáráshoz az IEnumeratort kell implementálni (Current MoveNext és Reset függvények)
        {
            // ennek fogja átadni a BejarhatoOsztaly IEnumerator függvényének a paraméterét
            private List<string> lista;

            int pozicio = -1; // a kezdő pozíció

            /// <summary>
            /// A konstruktor értéket ad a lista változónak
            /// </summary>
            /// <param name="lista"></param>
            public Bejaro(List<string> lista)
            {
                this.lista = lista;
            }

            /// <summary>
            /// Vissza adja a lista aktuális pozíciójában lévő elemet
            /// </summary>
            public object Current
            {
                get
                {
                    var current = lista[pozicio];

                    Console.WriteLine("     Current ( pozíció: {0}, elem: {1} )",pozicio,current);

                    return current;
                }
            }

            /// <summary>
            /// Növeli a pozíciót és megmondja, hogy van-e még elem
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                pozicio++;
                var vanMegElem = pozicio < lista.Count;
                Console.WriteLine("     MoveNext ( pozíció: {0}, VanMeg: {1} )", pozicio, vanMegElem);

                return vanMegElem;
            }

            /// <summary>
            /// Visszaállítja a pozíció kezdőértékét
            /// </summary>
            public void Reset()
            {
                pozicio = -1;
            }
        }
    }
}
