using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_12_Log4Net
{
    class Program
    {
        // A naplózás indításához kell egy változó minden olyan osztályba ahol naplózni szeretnénk ennek privátnak kell lenni és readonly-nak (csak a konstruktor tudja írni)
        // (ezt a sort kell minden osztály elejére bemásolni)
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("_1_12_Log4Net.Program");
        static void Main(string[] args)
        {
            //Példa egyszerű naplózásra

            //PeldaNaplo1();

            //Ha ez kevés fejleszthetünk saját naplózást a debugoláshoz vagy használhatunk valamilyen rendszert mint pl a Log4Net ami egy nyílt forráskódú projekt 
            // van hozzá nuget így a package managerből egyszerűen betölthető (Soloution explorer | Jobb gomb a solution-on | Manage NuGet packages | A search boxba írjuk a log4net-et | Browse | a találatok közül válasszunk (most az elsőt)| pipáljuk ki a projektet és install) 
            //(vagy Tools | Nuget Package Manager | Package Manager Console | PM consol-ban adjuk ki a  'install-package log4net' parancsot)

            //ahhoz hogy adatbázisba is tudjunk logolni kell egy sql adatbázis ezt most a saját gépemre fogom tenni sql server expresst a chocolatey csomagkezelővel részletesen lásd a vide-t kb 8:30-tól

            //a log4net hez az appconfigot módosítani kell lásd videó 15 perctől ami fontos, hogy az app.configot szerkesztve újrafordítás nélkül módosíthatjuk a logolás működését már kész alkalmazások estében is

            //Első példa az (első appender) fájlba fogja írni (max 1 MB -os fileméret ha eléri újat kezd) dátum sorszám elnevezéssel
            //A második appender ConsoleAppender a consolba ír konfig a log4net oldalról letöltve
            
            //Ahhoz, hogy a log4net működjön be kell tölteni a log4net konfigurációját
            //Ennek egyik módja (az egyik legbiztosabb)
            log4net.Config.XmlConfigurator.Configure();

            //Példa a log4net használatára
            for (int i = 0; i < 10; i++)
            {
                log.Debug("Ez egy naplóüzenet a log4net-ből");
            }

            Console.ReadKey();

            

            

        }

        /// <summary>
        /// Példa egyszerű naplózásra: ez így most nem csinál különösebben semmit csak kiírja a Debug Outputra a megfelelő szöveget még csak nem is igazi napló
        /// de egyszer debugoláshoz használható
        /// </summary>
        private static void PeldaNaplo1()
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.WriteLine("Ez a hibakeresési információ: {0}", i);
            }
        }
    }
}
