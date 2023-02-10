using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace ServiceServer
{
    [RunInstaller(true)]
    public partial class InstallerServiceServer : Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;
        public InstallerServiceServer()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();
            
            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = "LicenseServer";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
