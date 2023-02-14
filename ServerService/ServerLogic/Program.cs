using System.Globalization;
using System.Threading;

namespace ServerLogic
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var service = new Service(args);
            service.StartServer();
        }
    }
}