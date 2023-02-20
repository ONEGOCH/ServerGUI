using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic;
using System;

namespace ServerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var service = new Service(new string[] { @"C:\BazisSoftware\BazisServer\serverConfig.json" });
            service.StartServer();
        }
    }
}
