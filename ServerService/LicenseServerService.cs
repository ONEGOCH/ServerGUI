using System.ServiceProcess;
using System.Threading;
using ServerLogic;

namespace ServiceServer
{
    public partial class LicenseServerService : ServiceBase
    {
        private Service service;
        public static string name;
        public LicenseServerService()
        {
            InitializeComponent();
            CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            service = new Service(args);
            new Thread(service.StartServer).Start();
        }

        protected override void OnStop()
        {
            service.StopServer();
            Thread.Sleep(1000);
        }
    }
}