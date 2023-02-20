using LicenseData;
using System;
using System.IO;

namespace LicenseInfoReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (!(File.Exists(args[0])))
                    throw new Exception("Не найден файл лицензии!");
                else
                {
                    var keyManager = new KeyManager();
                    var licInfo = keyManager.LoadFile(args[0]);

                    Console.WriteLine($"\nКомпания {licInfo.CompanyName}");
                    Console.WriteLine($"\nВерсия ПО {licInfo.KeyInfo.Version}");
                    Console.WriteLine($"\nВерсия лицензии {licInfo.KeyInfo.Edition}");
                    Console.WriteLine($"\nВид лицензии {licInfo.KeyInfo.Kind}");
                    Console.WriteLine($"\nКоличество клиентов {licInfo.KeyInfo.NumberOfClients}");

                    if (licInfo.KeyInfo.Type == LicenseType.Trial)
                        Console.WriteLine($"\nВремя окончания {licInfo.KeyInfo.Expiration}");
                    else
                        Console.WriteLine($"\nТип лицензии {licInfo.KeyInfo.Type}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
