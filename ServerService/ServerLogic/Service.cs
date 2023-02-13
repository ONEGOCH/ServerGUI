using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ServerLogic
{
    public class Service
    {
        private readonly string licFile;
        private readonly int port;
        private readonly List<string> clientsIp;
        private readonly string logFile;
        
        private readonly TcpListener tcpListener;
        private readonly StreamWriter streamWriter;
        private bool enable;

        public Service()
        {
            var builder = new StringBuilder();
            using (var file = File.OpenRead("serverConfig.json"))
            {
                builder.Append(Convert.ToChar(file.ReadByte()));
            }
            var serverConfig = JsonSerializer.Deserialize<ServiceConfig>(builder.ToString());
            licFile = serverConfig.licFile;
            port = int.Parse(serverConfig.port);
            clientsIp = serverConfig.clientsIp;
            logFile = serverConfig.logFile;
            enable = true;

            var host = Dns.GetHostName();
            //Получение ip-адреса.
            var localIp = Dns.GetHostEntry(host).AddressList[1];

            Console.WriteLine($"Сервер запущен. IP : {localIp} {DateTime.Now}");
            Console.WriteLine("Не закрывайте приложение...");

            //streamWriter = new StreamWriter(logFile, true);
            //streamWriter.AutoFlush = true;
            //Console.SetOut(streamWriter);

            tcpListener = new TcpListener(localIp, port);
            try
            {
                if (CheckLicense(licFile, out var licenseInfo))
                {
                    tcpListener.Start(); // запускаем сервер
                    Console.WriteLine($"Сервер запущен. IP : {localIp} {DateTime.Now}");
                    Console.WriteLine(licenseInfo.CompanyName);
                    Console.WriteLine(licenseInfo.ProductKey);
                    Console.WriteLine(licenseInfo.KeyInfo.ToString());
                    Console.WriteLine(licenseInfo.CompanyName);
                    Console.WriteLine("Ожидание подключений... ");
                }
                else Console.WriteLine("Не найден подходящий сетевой адрес оборудования");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка : {ex.Message} {DateTime.Now}");
                Console.ReadLine();
            }
        }

        public void StopServer()
        {
            tcpListener.Stop();
            streamWriter.Close();
            enable = false;
            Console.WriteLine("Сервер остановлен");
        }

        public void StartServer()
        {
            while (enable)
            {
                // ждем подключения в виде TcpClient
                var tcpClient = tcpListener.AcceptTcpClient();
                new Thread(async () => await ProcessClientAsync(tcpClient)).Start();
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            using (var sw = new StreamWriter(logFile, true))
            {
                sw.AutoFlush = true;
                Console.SetOut(sw);

                Console.WriteLine(
                    $"Входящее подключение. IP : {tcpClient.Client.RemoteEndPoint}  {DateTime.Now}");
                // получаем объект NetworkStream для взаимодействия с клиентом

                var streamClient = tcpClient.GetStream();
                // буфер для входящих данных
                var response = new List<byte>();
                var bytesRead = 10;
                while (true)
                {
                    // считываем данные до конечного символа
                    while ((bytesRead = streamClient.ReadByte()) != '\n')
                    {
                        // добавляем в буфер
                        response.Add((byte)bytesRead);
                    }

                    var responseStr = Encoding.UTF8.GetString(response.ToArray());

                    // если прислан маркер окончания взаимодействия,
                    // выходим из цикла и завершаем взаимодействие с клиентом
                    if (responseStr == "END") break;

                    Console.WriteLine($"Запрошено действие {responseStr}");

                    var answerStr = string.Empty;
                    if (!clientsIp.Contains(tcpClient.Client.RemoteEndPoint.ToString().Split(':')[0]))
                        answerStr = $"Адрес {tcpClient.Client.RemoteEndPoint} не поддерживается сервером";
                    else
                    {
                        // находим команду и отправляем ответ обратно клиенту
                        answerStr = CheckResponse(responseStr, licFile);
                    }

                    // добавляем символ окончания ответа 
                    answerStr += '\n';

                    // отправляем перевод слова из словаря
                    var answer = Encoding.UTF8.GetBytes(answerStr);
                    await streamClient.WriteAsync(answer, 0, answer.Length);
                    Console.WriteLine($"Ответ : {answerStr}");
                    response.Clear();
                }

                Console.WriteLine($"Сеанс завершен. IP : {tcpClient.Client.RemoteEndPoint}");
                Console.WriteLine("...Ожидание подключений...");
                Console.WriteLine("|------------------------|");
            }
        }

        public static string CheckResponse(string action, string licFile)
        {
            try
            {
                if (CheckLicense(licFile, out var licInfo))
                {
                    if (action == "Разблокировать пре/пост процессор" | action == "Разблокировать решатель")
                        return "можно";
                    if (action == "Запрос на подключение")
                        return "Подключение установлено";
                    if (action == "Проверить элементы")
                    {
                        if (licInfo.KeyInfo.Edition == Edition.Study)
                            return "30000";
                        if (licInfo.KeyInfo.Edition == Edition.Demo)
                            return "10000";
                        return "20000000";
                    }

                    return "Не зарегистрированная команда";
                }

                return "Не найден подходящий сетевой адресс оборудования";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        public static bool CheckLicense(string licFile, out LicenseInfo licInfo)
        {
            var kManager = new KeyManager();
            //Load lic file
            licInfo = kManager.LoadFile(licFile);

            //Check license file
            return kManager.ValidKey(licInfo.KeyInfo);
        }
    }
}