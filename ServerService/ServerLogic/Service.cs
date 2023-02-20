using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LicenseData;
using Newtonsoft.Json;

namespace ServerLogic
{
    public class Service
    {
        private string licFile;
        private int port;
        private List<string> clientsIp;
        private string logFile;
        private IPAddress localIp;
        private string[] args;

        private readonly TcpListener tcpListener;
        //private readonly StreamWriter streamWriter;
        private bool enable;

        public Service(string[] args)
        {
            this.args = args;
            var serverConfig =
                (ServiceConfig)JsonConvert.DeserializeObject(
                    File.ReadAllText(
                        $@"{Environment.GetEnvironmentVariable("LicenseServerPath", EnvironmentVariableTarget.Machine)}\serverConfig.json"),
                    typeof(ServiceConfig));
            if (serverConfig != null)
            {
                licFile = serverConfig.licFile;
                port = int.Parse(serverConfig.port);
                clientsIp = serverConfig.clientsIp;
                logFile = serverConfig.logFile;
            }
            else
                throw new SerializationException();

            enable = true;

            var host = Dns.GetHostName();
            localIp = Dns.GetHostEntry(host).AddressList[1];

            //streamWriter = new StreamWriter(logFile, true);
            //streamWriter.AutoFlush = true;
            //Console.SetOut(streamWriter);

            tcpListener = new TcpListener(localIp, port);
        }

        public void StopServer()
        {
            tcpListener.Stop();
            //streamWriter.Close();
            enable = false;
            File.AppendAllText(logFile, "\nСервер остановлен");
        }

        public void StartServer()
        {
            try
            {
                if (CheckLicense(licFile, out LicenseInfo licenseInfo))
                {
                    tcpListener.Start(); // запускаем сервер
                    File.AppendAllText(logFile, $"\nСервер запущен. IP : {localIp} {DateTime.Now}");
                    File.AppendAllText(logFile, "\nОжидание подключений... ");
                    //streamWriter.Close();
                    //streamWriter.Dispose();

                    while (enable)
                    {
                        // ждем подключения в виде TcpClient
                        var tcpClient = tcpListener.AcceptTcpClient();
                        new Thread(async () => await ProcessClientAsync(tcpClient)).Start();
                    }
                }
                else File.AppendAllText(logFile, "\nНе найден подходящий сетевой адрес оборудования");
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFile, $"\nОшибка : {ex.Message} {DateTime.Now}");
                //Console.ReadLine();
            }
            finally
            {
                File.AppendAllText(logFile, "\nИсправьте ошибки и перезапустите сервис");
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
                Console.WriteLine("Ожидание подключений...");
                Console.WriteLine("|------------------------|");
            }
        }

        public string CheckResponse(string action, string licFile)
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

        public bool CheckLicense(string licFile, out LicenseInfo licInfo)
        {
            var kManager = new KeyManager();
            //Load lic file
            licInfo = kManager.LoadFile(licFile);

            //Check license file

            if (clientsIp.Count > licInfo.KeyInfo.NumberOfClients)
                throw new Exception("Превышено допустимое количество клиентов");

            return kManager.ValidKey(licInfo.KeyInfo);
        }
    }
}