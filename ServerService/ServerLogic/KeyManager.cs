using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace ServerLogic
{
    public class KeyManager
    {
        public bool GenerateKey(KeyInfo kv, ref string productKey)
        {
            if (kv.MAC.Length == 0)
                return false;
            List<string> values = new List<string>();
            values.Add(kv.MAC);
            values.Add(kv.Version);
            values.Add(kv.Type.ToString());
            DateTime dateTime = kv.Expiration;
            dateTime = dateTime.Date;
            values.Add(dateTime.ToString());
            values.Add(kv.Edition.ToString());
            values.Add(kv.Kind.ToString());
            values.Add(kv.NumberOfClients.ToString());
            string cipherStr = string.Join("%", values);
            productKey = Encrypt(cipherStr);
            return true;
        }

        public bool ValidKey(KeyInfo keyInfo)
        {
            if (keyInfo.Type == LicenseType.Trial && (keyInfo.Expiration - DateTime.Now.Date).Days <= 0)
                throw new Exception("Действие лицензии закончилось! Обратитесь к поставщику лицензии для продления.");
            if (!GetMacAddress().Contains(keyInfo.MAC))
                throw new Exception("Не обнаружен корректный сетевой адрес!");
            return true;
        }

        public List<string> GetMacAddress() =>
            NetworkInterface.GetAllNetworkInterfaces()
            .Select(
                nic => nic.GetPhysicalAddress().ToString()).ToList();

        public void DisassembleKey(string productKey, KeyInfo keyInfo)
        {
            var strArray1 = Decrypt(productKey).Split('%');
            keyInfo.MAC = strArray1.Length == 7 ? strArray1[0] : throw new Exception("Файл лицензии поврежден!");
            keyInfo.Version = strArray1[1];
            if (strArray1[2] == "Endless")
            {
                keyInfo.Type = LicenseType.Endless;
            }
            else
            {
                keyInfo.Type = LicenseType.Trial;
                string[] strArray2 = strArray1[3].Split(' ')[0].Split('.');
                DateTime dateTime = new DateTime(int.Parse(strArray2[2]), int.Parse(strArray2[1]),
                    int.Parse(strArray2[0]));
                keyInfo.Expiration = dateTime;
            }

            keyInfo.Edition = !(strArray1[4] == "Professional") ? Edition.Study : Edition.Professional;
            keyInfo.Kind = !(strArray1[5] == "Net") ? LicenseKind.Local : LicenseKind.Net;
            keyInfo.NumberOfClients = int.Parse(strArray1[6]);
        }

        public LicenseInfo LoadFile(string keyPath)
        {
            if (!File.Exists(keyPath))
                throw new Exception("Отсутствует файл лицензии!");
            using (var streamReader = new StreamReader(keyPath))
            {
                streamReader.ReadLine();
                streamReader.ReadLine();
                var str1 = streamReader.ReadLine().Split(':')[1].Replace(" ", "");
                var line = ParseLine(streamReader.ReadLine());
                var str2 = line.Count == 4 ? line[3] : throw new Exception("Файл лицензии поврежден!");
                var licenseInfo = new LicenseInfo();
                licenseInfo.ProductKey = str2;
                licenseInfo.CompanyName = str1;
                DisassembleKey(licenseInfo.ProductKey, licenseInfo.KeyInfo);
                return licenseInfo;
            }
        }

        public static string Encrypt(string cipherStr)
        {
            var rc4 = new RC4(convertStringToByte("bazisXN3.0.0"));
            var numArray = convertStringToByte(cipherStr);
            var dataB = numArray;
            var length = numArray.Length;
            return convertByteToString(rc4.Encode(dataB, length));
        }

        public static string Decrypt(string cipherStr)
        {
            var rc4 = new RC4(convertStringToByte("bazisXN3.0.0"));
            var numArray = convertStringToByte(cipherStr);
            var dataB = numArray;
            var length = numArray.Length;
            return convertByteToString(rc4.Decode(dataB, length));
        }

        private static string convertByteToString(byte[] ar)
        {
            var stringBuilder = new StringBuilder();
            foreach (var t in ar)
                stringBuilder.Append((char)t);

            return stringBuilder.ToString();
        }

        private static byte[] convertStringToByte(string str)
        {
            var numArray = new byte[str.Length];
            for (var index = 0; index < numArray.Length; ++index)
                numArray[index] = (byte)str[index];
            return numArray;
        }

        public static List<string> ParseLine(string line)
        {
            var line1 = new List<string>();
            var length = line.ToCharArray().Length;
            var num = 0;
            while (num < line.Length)
            {
                var token = ReadField(line, num);
                if (line[num] != ' ')
                {
                    line1.Add(token.Value);
                    num += token.Length;
                }
                else
                    ++num;
            }

            return line1;
        }

        private static Token ReadField(string line, int startIndex)
        {
            var num = (int)line[startIndex];
            switch (line[startIndex])
            {
                case ' ':
                    return new Token(" ", startIndex, 1);
                case '"':
                    return ParseQuotedField(line, startIndex, "\"");
                case '\'':
                    return ParseQuotedField(line, startIndex, "'");
                default:
                    return ParseFieldWithoutQuotes(line, startIndex);
            }
        }

        private static Token ParseQuotedField(string line, int startIndex, string quote) =>
            ParseField(line, startIndex + 1, quote.ToCharArray(), 2);

        private static Token ParseFieldWithoutQuotes(string line, int startIndex) =>
            ParseField(line, startIndex, stopCharsAr, 0);

        private static readonly char[] stopCharsAr = new char[3]
        {
            ' ',
            '"',
            '\''
        };


        private static Token ParseField(
            string line,
            int startIndex,
            IReadOnlyCollection<char> stopChars,
            int quotesNumber)
        {
            var stringBuilder = new StringBuilder();
            var num1 = 0;
            var num2 = 0;
            for (var index = startIndex; index < line.Length; ++index)
            {
                var ch = line[index];
                if (!stopChars.Contains(ch))
                {
                    if (stopChars.Count > 1)
                        stringBuilder.Append(ch);
                    else switch (ch)
                    {
                        case '\\' when line[index + 1] == '\\':
                            stringBuilder.Append(ch);
                            ++num2;
                            ++index;
                            break;
                        case '\\' when line[index + 1] == '\'':
                            stringBuilder.Append('\'');
                            ++num1;
                            ++index;
                            break;
                        case '\\' when line[index + 1] == '"':
                            stringBuilder.Append('"');
                            ++num1;
                            ++index;
                            break;
                        default:
                            stringBuilder.Append(ch);
                            break;
                    }
                }
                else
                    break;
            }

            return new Token(stringBuilder.ToString(), startIndex, stringBuilder.Length + quotesNumber + num1 + num2);
        }
    }
}