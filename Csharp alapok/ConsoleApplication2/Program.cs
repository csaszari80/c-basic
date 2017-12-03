using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var haziallat = new Haziallat();
            var haziallat2 = new Haziallat();

            if (haziallat ==haziallat2)
            { //ebben az esetben ugyanaz a referencia, ugyanaz a példány
                Console.WriteLine("Ugyanaz");
            }
            else
            { //ebben az esetben nem azonos a két példány
                Console.WriteLine("Nem ugyanaz");
            }

            // Ez így megtöri az egységbezárás elvét
            haziallat.Labakszama = 3;

            //Ez így már jobb
            haziallat.HanyLabaVanLekerdez();

            // Metódus használat érték szerinti átadással
            var lepes = 5;
            haziallat.lepjenEnnyit(lepes);
            Console.WriteLine("Ennyit lépett ez a jószág: {0}",lepes);

            //metódus használat referencia szerinti átadással
            haziallat.lepjenEnnyit2( ref lepes );
            Console.WriteLine("Ennyit lépett ez a jószág: {0}", lepes);

            //kimenő paraméter használata
            lepes = 5;
            int kimeno;
            haziallat.lepjenEnnyit3(lepes,out kimeno);
            Console.WriteLine("Ennyit lépett ez a jószág: {0}", lepes);
            Console.WriteLine("Ez meg a kimenő paraméter: {0}", kimeno);

            haziallat.NevMegadása(); // Mivel most van alapértelmezett paraméter, ezért nem muszáj megadni.Ha most nem adok meg nevet akkor az alapértelmezett lesz. 
            haziallat.NevMegadása(5);


            Console.ReadKey();
           

        }
    }

    class Haziallat
    {
        // ha nem adjuk meg a láthatóságot akkor alapértelmezés szerint private lesz
        string ValamiSzoveg;

        // ha public lesz akkor kívülről is lehet rá hivatkozni az ilyen változókat mezőknek hívjuk csak indokolt esetben használjuni ilyet
        public string KivulrolIsLatszik;

        public int Labakszama;

        //ehelyettegy private változót csinálunk amihez csinlunk egy kívülről látható függvényt
        // ezzel le tudjuk kérdezni a változót de nem tudjuk átírni (getter), ha estleg a tulajdonság megköveteli, hogy kívülről módosítsuk arra célszerű másik függvényt írni(setter).
        int labakszama;

        public int HanyLabaVanLekerdez()
        {
            return labakszama;
        }

        public void HanyLabaVanMegad(int labak)
        {
            labakszama = labak;
        }

        //A tulajdonság, a getter és a setter egyszerre létrehozható rövidítés prop tab tab
        // Ebben az esetben a get és a set rejtve létrejön ezeka lapértelmezetten publikusak ha valamelyiket privatere állítom akkor csak osztályon belülről lehet  elvégezni a műveletet
        public int HanySzemeVan { get; set; }

        // lehet kívülről csak lekérdezhető tulajdonságot létrehozni
        public int HanyFuleVan { get; }

        // kívülről csak beállítható tulajdonság ilyen módon történő létrehozása kicsit bonyolultabb: előszür létre kell hozni egy tulajdonságot( backing field ) majd a létrehozásakor a sethez meg kell adni egy kódblokkot
        private int hanyFogaVan;
        public int HanyFogaVan { set { hanyFogaVan = value; } }

        // a gettert is lehet egyénileg (kódblokkal) implementálni, ilyen esetben a settert is kódblokkal kell (ha az egyiket így adjuk meg a másik sem jön létre automatikusan)
        private int hanyFarkaVan; // privát változó: backing field
        public int HanyFarkaVan
        {
            get
            {
                return hanyFarkaVan;
            }
            set
            {
                hanyFarkaVan = value;
            }
        }
       
        /// Az osztály viselkedését a metódusai (függvényei határozzák meg)
        /// itt étékátadás történik, ha a függvényen belül megváltoztatjuk bemenő paraméter értékét akkor ez a változás kifelé nem látszik
        public void lepjenEnnyit(int lepes)
        {
            Console.WriteLine("Ennyit lepek:{0}",lepes);
            lepes = 4;
        }
        
        ///Lehetséges referencia szerint is átadni ha ilyet akarunk akkor a függvény megadásakor a bemenő paraméternél a ref kulcszót kell használnunk
        ///A függvény meghívásakor is használni kell a ref kulcszót
        public void lepjenEnnyit2(ref int lepes)
        {
            Console.WriteLine("Ennyit lepek:{0}", lepes);
            lepes = 4;
        }
        
        ///Lehetséges kimeneti paramétert is megadni (nem visszatérési érték) az out kulcsszóval
        ///Az ilyen paraméternek mindenképp ésrtéket kell adni a függvényen belül (kívülről nem lehet)
        public void lepjenEnnyit3(int lepes, out int ennyitLeptem)    // a függvény szignatúrája ebben az esetben:  lepjenEnnyit3(int, out int)
        {
            Console.WriteLine("Ennyit lepek:{0}", lepes);
            lepes = 4;
            ennyitLeptem = lepes;
        }

        string Nev;
        public void NevMegadása(string nev = "Bambi") //Alapértelmezett érték megadása
        {
            Nev = nev;
            /// ha nyomatékosítani akarom, hogy erre az objektumra vonatkozik akkor ezt is írhatom
            /// this.Nev=nev
        }

        // Lehetséges ugyanilyen névvel létrehozni függvényt ha más a szignatúrája pl a console. writeline() 19 különböző szignatúrája van ami azt jelenti hogy 19 féle képpen van megírva
        public void NevMegadása(int nev)
        {
            Console.WriteLine( nev ); ;
            /// ha nyomatékosítani akarom, hogy erre az objektumra vonatkozik akkor ezt is írhatom
            /// this.Nev=nev
        }
    }
}
