using System;
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
            foreach (var listaElem in BevasarloLista())
            {
                Console.WriteLine(listaElem);
            }
            //Valami ilyesmit szeretnénk elérni
            //Console.WriteLine("1 kg kenyér");
            //Console.WriteLine("10 dkg felvágott");
            //Console.WriteLine("1 kg liszt");
            //Console.WriteLine("1 l tej");
            //Console.WriteLine("Üdítő");
            //Console.WriteLine("Nasi");
            //Console.WriteLine("1 kg Darálthús");
            //Console.WriteLine("1 kg tészta");

            Console.ReadKey();
        }
        //21 percnél tartok
        private static IEnumerable<string> BevasarloLista() //speciális függvény az IEnumareble objektumlisták bejárását szolgálja ki jelen esetben stringeket ad vissza a foreach mindig a következő yield return értéket adja eredményül
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
    }
}
