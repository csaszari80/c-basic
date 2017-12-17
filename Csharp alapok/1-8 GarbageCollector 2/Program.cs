using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_8_GarbageCollector_2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (!Console.KeyAvailable)    //Addig meg amíg a consolon nincs feldolgozandó karakter ( ha lenyomunk egy gombot akkor lesz és megszakad a ciklus)
            {
                //var stream = new MemoryStream(100000);      //bájtok sorozata a memóriában később még lesz róla szó ha csak ezt pörgetjük akkor a futtatás közben megfigyelhetjük, hogy szinte végig megy a GC  hogy ne fogyjon el a memória, de a processzort leterheli.
                ///gyors egymásutánban foglalunk le nagyméretű memóriát és szüntetjük meg a rámutató hivatkozást emiatt megy folyamatosan a GC

                //Thread.Sleep(10);
                //var bitmap = new Bitmap(1280,1024);  // Ahhoz hogy ezt tudjuk használni fel kell venni a referenciák közé egy külső assembly-t: System.Drawing (Soluton Explorer|Aktuális projekr|References(jobb klikk)|Add references|Assemblies|Framework|Syste.Drawing(kipipál)|OK) ezután már tudjuk usingal vagy teljes hivatkozással használni
                /// Amúgy egy bitmap képet, hoz létre ha ezt futtatjuk akkor paraméterhibát dob. Valójában az történik, hogy annyira gyorsan kifut a memóriából, hogy nincs ideje a GC-nek lefutni (16-bites rendszereken 1,5 GB a heap mérete ezért csak 2Gb-ból fut ki)
                /// a véglegesítője miatt a GC nem tudja elsőre kitakaríani ezeket a második kör előtt pedig elfogy a memória
                /// Ha elédobunk egy thread.Sleep(10) -et akkor lassíthatunk a folyamaton


                //Lehetésges megoldás az Idisposable felület megvalósítása
                //using (var stream = new MemoryStream(100000)) { }  //ezen nem segít 


                using (var bitmap = new Bitmap(1280, 1024)) { }  //Itt viszont semmi gond. ez megoldja a véglegesítő okozta gondot (nem fut le a véglegesítő) 
                // A Bitmap szülő osztálya az Image és ennek definiálva (ezért a bitmap is örökli) van az Idisposable felülete(interface)
                //(A Bitmapra ráállva F12-vel megnézhetjük az osztálydefiníciót ott látszik, hogy az Image-ből van leszármaztatva azt megnézve pedig látható az Idisposable felület

            }
        }
    }
}
