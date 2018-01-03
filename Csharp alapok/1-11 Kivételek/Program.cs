using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_11_Kivételek
{
    class Program
    {
        static void Main(string[] args)
        {

            //Ha csak a kivételek logolása a cél (nem akarjuk kezelni csak szeretnénk tudni, hogy vannak -e) akkor feliratkozhatunk egy eseményre (később lesz róluk szó)
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            
            //kivétel kiváltása: szükséges hozzá egy kivétel osztálypéldányra
            //throw new Exception();

            //kivétel kezelő kódblock
            /// a try kódblokkban lévő kódot megpróbálja végrehajtani
            try
            {
                Console.WriteLine("Try kezdődik");

                //most nem egy sima exception objektumot hanem egy abból leszármaztatott osztály példányát generáljuk sokféle exception van az ősosztály az Exception onnan ágaznak el lefelé fa struktúrában
                // a catch ág paramétereként megadott osztály azt adja meg hogy a struktúra mely ágára vonatkozik a catch blokk
                // pl: catch(Exception){} - minden hiba (Minden egyéb kivétel osztály ennek a leszármazottja) kezelése
                // catch(ArgumentException){} -Az ArgumentException osztályba vagy annak valamely leszármaztatott osztályába tartozó kivételek kezelése
                //throw new InvalidOperationException(); // dobunk egy új egy InvalidOperationException osztálypéldányt

                //Egymásba ágyazott hibakezelések mi történik (az exception-től kifelé haladva a catchek kiértékelődnek és ahol az adott típusú hiba kezelve van ott fut le a catchblokk egyszer)
                Foprogram();


                Console.WriteLine("Try vége"); //ez már nem fut le ha előtte van exception
            }

            /// Ha kivétel keletkezik akkor a catch kódblokban lévő kódra ugrik
            /// A catch szűrőparamétere adja meg hogy milyen típusú(mely osztályba tartozó) hibákat kezel
            /// a szűrőparaméter nélküli catch régebben a nem exception osztályba (nem dotnet keretrendzeren kívüli hibákat kezelte most ezeket a RuntimeWrappedExtension osztályba terelték ami szintén az Exceptionból van leszármaztatva
            /// több catch águnk is lehet ha ez a helyzet akkor úgy kell csinálni hogy a szűkebb körtől haladjunk az általánosabb(az Exception fában) felé fentről lefelé
            catch (OutOfMemoryException) //A filterek kiértékelése fentről lefelé történik
            {
            }
            catch (InvalidOperationException) //lehetnek függetlenek 
            {
            }
            //vagy egyre feljebb a leszármaztatási fában
            catch (Exception ex) // ex néven vesszük át az exception objektumot
            {
                Console.WriteLine("catch kezdete");
                Console.WriteLine(ex.ToString()); //kiíratjuk a hibát

                //Az egyik lehetőség, hogy továbblépünk a kivételen(elnyomjuk a hibát)




                //A másik, hogy dobunk egy újabb kivételt (ha ez nincs kikommentelve és szeretnénk, hogy itt lássuk végig futni a programot célszerű ctrl+f5-el vagy nem debug módban futtatni
                //throw;


                Console.WriteLine("catch vége");
            }
            
            /// A finally kódblokk(nem kötelező) mindenképp végrehajtódik akár volt kivétel akár nem akár továbbléptünk akár dobtunk egy újabb kivételt
            finally
            {
                Console.WriteLine("Finally kezdete");


                Console.WriteLine("Finally Vége");
            }
            Console.WriteLine("Finally után"); // ez csak akkor fut,ha a try-ban nem volt kivétel vagy, ha a try-ban keletkezet kivételt a catch kezelte és ott nem dobtunk újabb kivételt
            Console.ReadKey();

        }

        /// <summary>
        /// Ez hívódik meg az esemény bejövetkezik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            Console.WriteLine("Log: {0}",e.Exception.ToString());
        }

        private static void Foprogram()
        {
            try
            {
                Console.WriteLine("Foprogram Try kezdődik");
                              
                Alprogram();
                
                Console.WriteLine("Foprogram Try vége"); //ez már nem fut le ha előtte van exception
            }
            
            catch (Exception ex) 
            
            //Az alprogramban generált spéci hibát most ez sem kezeli
            //catch(OutOfMemoryException ex)
            {
                Console.WriteLine("Foprogram catch kezdete");
                Console.WriteLine(ex.ToString()); //kiíratjuk a hibát
                                                  //vagy továbbmegyünk

                //vagy
                //1.rethrow paraméter nélkül dobunk kivételt ez a throw helyén lévő stack tracet írja át
                //throw;

                //2. throw ex -átvesszük és továbbdobjuk ez újraszámozza a stack trace-t úgy fog látszódni mintha itt keletkezett volna a hiba
                //throw ex;

                //3. Új kivételt dobunk aminek az inner exception paraméterébe továbbadjuk az eredetit is.
                throw new Exception("Ez már a mi kivételünk", ex);

                Console.WriteLine("Foprogram catch vége");
            }
            /// A finally kódblokk(nem kötelező) mindenképp végrehajtódik akár volt kivétel akár nem akár továbbléptünk akár dobtunk egy újabb kivételt
            finally
            {
                Console.WriteLine("Foprogram Finally kezdete");


                Console.WriteLine("Foprogram Finally Vége");
            }
            Console.WriteLine("Foprogram Finally után");
        }

        private static void Alprogram()
        {
            try
            {
                Console.WriteLine("Alprogram Try kezdődik");

                //throw new Exception();
                //most generáljunk egy speciálisabb hibát
                //throw new ArgumentOutOfRangeException();

                //most egy saját kivételt dobunk
                throw new SajatException();

                Console.WriteLine("Alprogram Try vége"); //ez már nem fut le ha előtte van exception
            }

            //catch (Exception ex) // ex néven vesszük át az exception objektumot
            //A speciális hibát ez a függvény most nem kezeli
            catch(InvalidOperationException ex)
            {
                Console.WriteLine("Alprogram catch kezdete");
                Console.WriteLine(ex.ToString()); 

                Console.WriteLine("Alprogram catch vége");
            }
            /// A finally kódblokk(nem kötelező) mindenképp végrehajtódik akár volt kivétel akár nem akár továbbléptünk akár dobtunk egy újabb kivételt
            finally
            {
                Console.WriteLine("Alprogram Finally kezdete");


                Console.WriteLine("Alprogram Finally Vége");
            }
            Console.WriteLine("Alprogram Finally után");
        }
    }

    /// <summary>
    /// Saját kivételosztályt sőt akár saját kivételosztályhierarchiát is létrehozhatunk
    /// </summary>
    public class SajatException : Exception
    {

    }
    public class SajatAdatException : SajatException
    {

    }
    public class SajatServiceException : SajatException
    {

    }
}
