using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//több sor kommentezése ctrl+k ctrl+c
//kommentezés megszűntetése ctrl+k ctrl+u
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // eredeti: Az eredeti érmefeldobót hívjuk meg
            var ermeFeldobo = new ErmeFeldobo();
            int eredmeny = ermeFeldobo.FeldobasEredmeny();
            Console.WriteLine("A feldobás1 eredménye: {0}", eredmeny);
            
            // Mi van ha a hamisítványt hívjuk meg: Mivel az ermeFeldobo2 a ErmeFeldobo típusú ezért amikor hivatkozunk rá az ErmeFeldobo (ős)osztály felületét érjük el ezért nem sikerül a hamisítás bár létrejön a gyermekosztálynak is egy példánya
            ErmeFeldobo ermeFeldobo2 = new HamisErmeFeldobo();
            int eredmeny2 = ermeFeldobo2.FeldobasEredmeny();
            Console.WriteLine("A feldobás2 eredménye: {0}", eredmeny2);

            //ahhoz, hogy működjön cast-olással kell használni a hamis érmefeldobó felületet
            int eredmeny3 = ((HamisErmeFeldobo)ermeFeldobo2).FeldobasEredmeny();
            Console.WriteLine("A feldobás3 eredménye: {0}", eredmeny3);
            //ez azonban nem valódi hamisítvány a fordító tudja, hogy nincs köze az ősosztályhoz


            //A hamisitható eredetire akarok hivatkozni most egy teljesen új példánynyal
            ErmeFeldobo ermeFeldobo4 = new ErmeFeldobo();
            int eredmeny4 = ermeFeldobo4.HamisithatoFeldobasEredmeny();
            Console.WriteLine("A feldobás4 eredménye: {0}", eredmeny4);

            //A hamisítható hamisítványára hivatkozunk
            ErmeFeldobo ermeFeldobo5 = new HamisErmeFeldobo();
            int eredmeny5 = ermeFeldobo5.HamisithatoFeldobasEredmeny();
            Console.WriteLine("A feldobás5 eredménye: {0}", eredmeny4);
            Console.ReadKey();
        }
    }
    /// <summary>
    /// Érmefeldobást szimuláló osztály
    /// </summary>
    class ErmeFeldobo
    {
        //konstruktor létrehozása ctor tab tab
        public ErmeFeldobo()
        {
            Console.WriteLine("Eredeti konstruktor");    
        }
        
        Random generator = new Random();

        /// <summary>
        /// feldobunk egy érmét és az eredményét visszaadjuk Ezzel így nem hamisítható
        /// </summary>
        /// <returns>0=fej 1=írás</returns>
        internal int FeldobasEredmeny()
        {
            Console.WriteLine("Véletlen feldobás");
            var kapottSzam=generator.Next(2);
            return kapottSzam;
        }
        
        /// <summary>
        /// Ez a függvény a virtual kulcszó miatt felülírhatú a leszármaztatott osztályokban
        /// </summary>
        /// <returns></returns>
        internal virtual int HamisithatoFeldobasEredmeny()
        {
            Console.WriteLine("Véletlen feldobás");
            var kapottSzam = generator.Next(2);
            return kapottSzam;
        }
    }
    
    /// <summary>
    /// Hamisító az érmefeldobó leszármaztatott osztálya
    /// ahhoz hogy az eredeti tulajdonságait és metódusaival azonos nevűeket hozzak létre ahhoz a new kulcsszót kell használni (ha kihagyjuk a fordító odaérti akkoris)
    /// </summary>
    class HamisErmeFeldobo : ErmeFeldobo
    {
        public HamisErmeFeldobo()
        {
            Console.WriteLine("Hamis konstruktor");
        }
        internal new int FeldobasEredmeny()
        {
            Console.WriteLine("Hamis feldobás");
            return 1;
        }

        /// <summary>
        /// Ha az ősosztályban virtual kulcsszó van akkor a leszármaztatott osztályban az override kulcsszóval lehet felülírni
        /// </summary>
        /// <returns></returns>
        internal override int HamisithatoFeldobasEredmeny()
        {
            Console.WriteLine("Hamis feldobás");
            return 1;
        }
    }
}
