using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_6_Felület_Síkidom
{
    class Program
    {
        static void Main(string[] args)
        {
            var teglalap = new Teglalap(magassag: 3, szelesseg: 2); //ha ilyen módon adom meg a paramétereket (nem csak az értéket hanem a nevét is) akkor amikor felajánlja (CTRL+.) hogy létrehozza a konstruktort akkor már az is meglesz.

            var haromszog = new Haromszog(alap : 10, magassag : 5);

            var kor = new Kor(sugar: 12);

            var lista = new List<ISikidom>(); //Az objektumainkat betesszük egy listába mivel az objektumaink nem egyforma tipusúak ezért nem lehet egyikét sem megadni a lista típusának ezért az interface-t adjuk meg mivel annak mindhárom objektum megvalósítja a felületét

            lista.Add(kor);
            lista.Add(haromszog);
            lista.Add(teglalap);
            var sum = 0;

            foreach (var sikidom in lista)
            {
                Console.WriteLine("Terulet: {0}",sikidom.Terulet());
                sum = sum + sikidom.Terulet();
            }
            Console.WriteLine("Összesen: {0}",sum);
            Console.ReadKey();
        }

        //Közös ősosztályból nem tudunk kiindulni mert nem tudjuk megmondani hogy mennyi a területe
        //Létrehozunk egy interface-t ami úgy néz ki mint egy osztály vannak tulajdonságai és metódusai de nincsnek implementálva
        //Névkonvenció: I-vel kezdődjön a neve

        interface ISikidom
        {
            int Terulet(); // Ez így azt jelenti, hogy minden osztálynak ami ezt megvalósitja lesz egy int visszatérési értékű Terulet nevű fügvénye
        }

        class Teglalap : ISikidom
        {
            public Teglalap(int magassag, int szelesseg)
            {
                Magassag = magassag;
                Szelesseg = szelesseg;
            }

            public int Magassag { get; private set; } //mivel most  a setter private ezért kell egy konstruktor
            public int Szelesseg { get; private set; }

            public int Terulet()
            {
                return Magassag * Szelesseg;
            }
        }

        class Haromszog : ISikidom
        {
            public Haromszog(int alap, int magassag)
            {
                Alap = alap;
                Magassag = magassag;
            }

            public int Alap { get; private set; }
            public int Magassag { get; private set; }

            public int Terulet()
            {
                return (Alap * Magassag) / 2;
            }
        }

        class Kor : ISikidom
        {
            public Kor(int sugar)
            {
                Sugar = sugar;
            }

            public int Sugar { get; private set; }

            public int Terulet()
            {
                return (int)(Sugar * Sugar * Math.PI); //típuskonverzió, most nem számít, hogy adatvesztés van
            }
        }
    }
}
