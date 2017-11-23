using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04Ciklus
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i + 1);
                System.Threading.Thread.Sleep(100);
            }
            Console.ReadKey();
        }
    }
}
