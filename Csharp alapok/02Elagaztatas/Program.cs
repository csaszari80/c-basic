using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02Elagaztatas
{
    class Program
    {
        static void Main(string[] args)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
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
