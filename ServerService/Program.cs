using System.IO;
using System.ServiceProcess;

namespace ServiceServer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            File.WriteAllText("log.txt","test");
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new LicenseServerService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
