using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

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
