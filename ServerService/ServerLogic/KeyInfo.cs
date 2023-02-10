using System;

namespace ServerLogic
{
    public class KeyInfo
    {
        public int NumberOfClients { get; set; } = 1;

        public Edition Edition { get; set; }

        public string Version { get; set; }

        public LicenseType Type { get; set; }

        public LicenseKind Kind { get; set; }

        public DateTime Expiration { get; set; } = DateTime.Now;

        public string MAC { get; set; }

        public override string ToString() => Type == LicenseType.Endless ? string.Join(" ", Type, Kind, NumberOfClients, Edition, Version) : string.Join(" ", Type, string.Format("Осталось {0} дней", (Expiration - DateTime.Now.Date).Days), Kind, NumberOfClients, Edition, Version);
    }
}