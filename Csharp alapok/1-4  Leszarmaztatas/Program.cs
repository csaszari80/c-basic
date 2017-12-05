using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_4__Leszarmaztatas
{
    class Program
    {
        static void Main(string[] args)
        {

            // A függvények a kutya oldaláról vannak meghívva
            Kutya kutya = new Kutya();
            kutya.Koszon();
            kutya.Enekel();
            kutya.Beszel();
          

            // eredmény:
            /*   A háziállat köszön.
                 A kutya enekel.
                 A kutya beszél.*/

            Console.WriteLine();

            //A függvényeket a háziállat felől is meghívhatjuk
            Haziallat haziallat = new Kutya();
            haziallat.Koszon();
            haziallat.Enekel();
            haziallat.Beszel();

            //eredmény
            //A háziállat köszön.
            //A háziállat énekel.
            //A kutya beszél.



            Console.WriteLine();

            //Direktben ki tudok jelölni felületet
            //A Haziallat tipusú változónak veszem a Kutya felületét
            // így ismét a kutya felől közelítek
            
            ((Kutya)haziallat).Koszon();
            ((Kutya)haziallat).Enekel();
            ((Kutya)haziallat).Beszel();

            Console.WriteLine();

            //eredmény ugyanaz mint először
            //A háziállat köszön.
            //A kutya enekel.
            //A kutya beszél.
            // ez a castolás ezt nem ellenőrzi a fordító ha olyan castolást csinálunk ami nem lehetséges az futásidejű hibát generál

            // var haziallat2 = new Haziallat();
            // ((Kutya)haziallat2).Beszel(); //mivel a haziallat2 egy Haziallat tipusú objektum ezért neki nincs Kutya felülete

            // Hogy érdemes típuskonverziót végezni?
            // Az object osztály minden osztály ősosztálya minden objektum beletehető egy ijen típusú változóba

            object o = new Kutya();
            //o.Beszel(); nincs ilyen metódusa
            //de hogy lehet innen kivenni?

            //ellenpróba ha az o-ba háziállatot teszek mivel objektum típusú ezért bármilyen osztály objektumával felülírható
            //o = new Haziallat();

            ObjectbolKutya1(o);

            ObjectbolKutya2(o);

            ObjectbolKutya3(o);

            //  nézzük meg a teljesítményeket
            // létrehozunk egy objektumtömböt feltöltjük kutyákkal és házállatokkal és megnézzük melyik függvény a gyorsabb
            var olist = new object[1000];
            for (int i = 0; i < 1000; i++)
            {
                if (i % 2 == 0)
                {
                    olist[i] = new Haziallat();
                }
                else
                {
                    olist[i] = new Kutya();
                }
            }
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                ObjectbolKutya1(olist[i]);
            }
            Console.WriteLine("1-es módszer: {0}", sw.ElapsedTicks);

            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {
                ObjectbolKutya2(olist[i]);
            }
            Console.WriteLine("2-es módszer: {0}", sw.ElapsedTicks);

            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {
                ObjectbolKutya3(olist[i]);
            }
            Console.WriteLine("3-as módszer: {0}", sw.ElapsedTicks);

            Console.ReadKey();
        }

        private static void ObjectbolKutya3(object o)
        {
            //esetleg megpróbálhatjuk átkonvertálni vagy sikerül vagy nem alapon
            // mivel ez elszálhat ezért célszerű egy try catch hibakezelő blokkot használni
            try
            {
                Kutya k3 = (Kutya)o;
                //Console.WriteLine("Ő egy kutya (try)"); // A méréshez kivesszük a kiíró utasításokat mindhárom fv-ből
                //k3.Beszel;  //már ez is jó lenne de most töltsük át
                //Kutya k = (Kutya)o;
                //k.Beszel();
            }
            catch (Exception)
            {

                //Console.WriteLine("Ez sajnos nem kutya (try)");
            }
        }

        private static void ObjectbolKutya2(object o)
        {
            Kutya k2 = o as Kutya; // Ha as-t használunk akkor csak akkor végzi el a konverziót ha az lehetséges egyébként null értéket ad vissza
            if (k2 != null)
            { // sikeres konverzió
                //Console.WriteLine("Ő egy kutya (as)");
                // k2.Beszel(); //már ez is jó lenne de most töltsük át
                Kutya k = (Kutya)o; // mivel a k változót előzőleg is egy blokkon belül hoztuk létre ezért itt újra felhasználható
                //k.Beszel();
            }
            else
            {
                //Console.WriteLine("Ez sajnos nem kutya (as)");
            }
            //Console.WriteLine();
        }

        private static void ObjectbolKutya1(object o)
        {
            if (o is Kutya)
            {
                //Console.WriteLine("Ő egy kutya (is)");
                Kutya k = (Kutya)o;
                //k.Beszel(); //így már jó
            }
            else
            {
                //Console.WriteLine("Ez sajnos nem kutya (is)");
            }
            //Console.WriteLine();
        }

        class Haziallat
        {
            public void Koszon()
            {
                Console.WriteLine("A háziállat köszön.");
            }
            public void Enekel()
            {
                Console.WriteLine("A háziállat énekel.");
            }
            // a virtual jelzi, hogy a leszármaztatott osztályban ezt felül lehet írni
            public virtual void Beszel()
            {
                Console.WriteLine("A háziállat beszél.");
            }
        }
        class Kutya : Haziallat
        {
            // ha ugyanoyan névvel hozzuk létre akkor azt ugyanazt jelenti mintha new kulcszóval hoztuk volna létre
            public void Enekel()
            {
                Console.WriteLine("A kutya enekel.");
            }
            public override void Beszel()
            {
                Console.WriteLine("A kutya beszél.");
                // Az ősosztály függvényét a base kulcszóval érhetjük el
                //base.Beszel();
            }
            

        }
        class Macska : Haziallat
        {

        }
    }
}
