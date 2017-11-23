using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valtozok
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Milyen legyen az ebéd");
            Console.WriteLine("Vega");
            Console.WriteLine("Egyébként pörkölt");
            var valasz = Console.ReadLine(); 
            if (valasz.ToLower() == "Vega".ToLower())
            {
                //ha péntek van
                Console.WriteLine("Karfiol");
                Console.WriteLine("Hagyma");
                Console.WriteLine("Tejszín");
            }
            else
            {
                //ha name péntek van
                Console.WriteLine("Marhalábszár");
                Console.WriteLine("Hagyma");
                Console.WriteLine("Pirospaprika");
                Console.WriteLine("Szalonna");
            }
            Console.ReadLine();
        }
    }
}
