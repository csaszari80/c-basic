using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_11_kivételteljesítmény
{
    /// <summary>
    /// A kivételkezelés teljesítményigényét mérjük (meglehetősen erőforrásigénye)
    /// önmagában nem vészes de ciklusban nagy mennyiségben meglehetősen problémás lehet
    /// ciklusban nem ajánlott használni
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    throw new Exception();
                }
                catch (Exception)
                {

                    
                }
                     
            }
            Console.WriteLine("Eltelt idő: {0} ticks", sw.ElapsedTicks);
            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {
               

            }
            Console.WriteLine("Eltelt idő: {0} ticks", sw.ElapsedTicks);
            Console.ReadKey();
        }
    }
}
