namespace ServerLogic
{
    public class LicenseInfo
    {
        public string ProductKey;
        public string CompanyName;

        public KeyInfo KeyInfo { get; set; }

        public LicenseInfo() => KeyInfo = new KeyInfo();

        public override string ToString() => string.Join(" ", "\"Лицензия принадлежит : " + CompanyName + "\"", "\"Ключ продукта : " + ProductKey + "\"", (object) ("\"Информация о ключе : " + KeyInfo + "\""));
    }
}