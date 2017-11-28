using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_1_Tipusok
{
    class Program
    {
        static void Main(string[] args)
        {
                        
            //értéktípus: Az érték lemásolódik értékadáskor, új példány jön létre
            //primitív típusok: számok, logikai érték, enum  ezek bár objektumok, de úgy működnek mint a hagyományos változók
            var ertek1 = 0;
            //ez ugyanaz mint
            int ertek0 = 0;
            Int32 ertekobj = new Int32(); // az alapértelmezett értéke 0


            var ertek2 = ertek1;
            ertek1 = 10;

            Console.WriteLine("Érték1: {0}; Érték2: {1};",ertek1,ertek2);
            //eredmény Érték1:10 Érték2:0
 
            //referencuatípus: értékadáskor a változóra mutató referencia adódik át
            var hivatkozás1 = new int[] { 0 }; //egyelemű egész tömb
            var hivatkozás2 = hivatkozás1;
            hivatkozás1[0] = 10;
            Console.WriteLine("Hivatkozás1: {0}; Hivatkozás2: {1};", hivatkozás1[0], hivatkozás2[0]);
            //eredmény Hivatkozas1:10 hivatkozás2:10

            var sajatertek1 = new SajatErtektipus();
            sajatertek1.Ertek = 0;
            sajatertek1.hiv = new SajatHivatkozasTipus();
            sajatertek1.hiv.Ertek = 0;

            var sajatertek2 = sajatertek1;
            sajatertek1.Ertek = 10;
            sajatertek1.hiv.Ertek = 10;
            Console.WriteLine("Sajátérérték1: {0}; Sajátérték2: {1};", sajatertek1.Ertek, sajatertek2.Ertek);
            Console.WriteLine("Sajátérérték1.hiv: {0}; Sajátérték2.hiv: {1};", sajatertek1.hiv.Ertek, sajatertek2.hiv.Ertek);
            //Eredmény: Sajátérérték1: 10; Sajátérték2: 0;
            //          Sajátérérték1.hiv: 10; Sajátérték2.hiv: 10;

            var sajathiv1 = new SajatHivatkozasTipus();
            sajathiv1.Ertek = 0;

            var sajathiv2 = sajathiv1;
            sajathiv1.Ertek = 10;
            Console.WriteLine("Sajáthivatkozás1: {0}; Sajáthivatkozás2: {1};", sajathiv1.Ertek, sajathiv2.Ertek);
            //Eredmény: Sajáthivatkozás1: 10; Sajáthivatkozás2: 10;

            //string kezelése: hivatkozástípus ugyab, de értéktípusként viselkedik minden egyes módousuláskor lemásolódik 
            var szoveg1 = "Eredeti szöveg";
            var szoveg2 = szoveg1;
            szoveg1 = "Módosított szöveg";
            Console.WriteLine("Szöveg1: {0}; Szöveg2: {1};", szoveg1, szoveg2);
            //Eredmény: Szöveg1: Módosított szöveg; Szöveg2: Eredeti szöveg;

            // Ezért ilyet ne csináljunk
            /*var szoveg = "";
            for (int i = 0; i < 10000000000; i++)
            {
                szoveg = szoveg + "valami új";
            }*/
            //ez nagyon sok példányban fogja lefoglalni a memóriát
            //Helyette használjuk ezt:
            /*var sb = new StringBuilder();
            for (int i = 0; i < 10000000; i++)
            {
                sb.Append("valami új");
            }
            var szoveg = sb.ToString();*/
            Console.ReadKey();

        }
    }
    //Referenciatípus létrehozása Osztályon keresztül
    class SajatHivatkozasTipus
    {
        public int Ertek;
    }

    //Értéktípus létrehozása Struct-al
    struct SajatErtektipus
    {
        public int Ertek;
        public SajatHivatkozasTipus hiv;
    }
}
