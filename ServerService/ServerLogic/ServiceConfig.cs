using System;
using System.Collections.Generic;

namespace ServerLogic
{
    [Serializable]
    public class ServiceConfig
    {
        public string licFile;
        public string port;
        public List<string> clientsIp;
        public string logFile;
    }
}