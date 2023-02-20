using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;
using LicenseData;
using Newtonsoft.Json;
using ServerLogic;

//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace ServerGUI
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = "Версия " + $"{ver.Major}.{ver.Minor}.{ver.Build}";
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                CreateConfig();

                var bit = Environment.Is64BitOperatingSystem ? "64" : "";
                lblToolStrip.Text = "Создание сервиса...";

                var process = new Process();
                var currentDirectory = Directory.GetCurrentDirectory();
                var startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments =
                        $@"/C setx /m LicenseServerPath {currentDirectory} && C:\Windows\Microsoft.NET\Framework{bit}\v4.0.30319\InstallUtil.exe {currentDirectory}\ServiceServer.exe",
                    Verb = "runas"
                };
                process.StartInfo = startInfo;

                process.Start();
                lblToolStrip.Text = "Сервис создан";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateConfig()
        {
            var config = new ServiceConfig
            {
                port = txbPort.Text,
                clientsIp = new List<DataGridViewRow>(dgv.Rows.Cast<DataGridViewRow>())
                    .Select(x => x.Cells[0].Value.ToString())
                    .ToList(),
                licFile = txbKey.Text,
                logFile = txbLog.Text
            };
            var configString = JsonConvert.SerializeObject(config);
            File.WriteAllText("serverConfig.json", configString);
            if (config.clientsIp.Count == 0)
                throw new Exception("Укажите хотя бы одного клиента!");
        }

        private void InfoBtb_Click(object sender, EventArgs e)
        {
            try
            {
                var fileStream = new StreamReader(txbLog.Text);

                richTextBox.Clear();
                var str = "";
                while (str != null)
                {
                    str = fileStream.ReadLineAsync().Result;
                    richTextBox.AppendText($"\n{str}");
                }

                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var runProc = Process.GetProcessesByName("ServiceServer")[0];

                var process = new Process();
                var startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = $"/C taskkill /pid {runProc.Id} /f",
                    Verb = "runas"
                };
                process.StartInfo = startInfo;
                process.Start();
                lblToolStrip.Text = "Сервис остановлен";
            }
            catch (Exception)
            {
                MessageBox.Show("Вероятно сервер не запущен!");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            lblToolStrip.Text = "Удаление сервиса...";
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = @"/C sc delete BazisServer",
                Verb = "runas"
            };
            process.StartInfo = startInfo;
            process.Start();

            lblToolStrip.Text = "Сервис удалён";
        }

        private void BtnAddClient_Click(object sender, EventArgs e)
        {
            dgv.Rows.Add();
        }

        private void TxbLog_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == DialogResult.Cancel)
                return;

            txbLog.Text = openDialog.FileName;
        }

        private void TxbKey_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == DialogResult.Cancel)
                return;

            txbKey.Text = openDialog.FileName;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            lblToolStrip.Text = "Начало работы сервиса...";
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $@"/C sc start BazisServer",
                Verb = "runas"
            };
            process.StartInfo = startInfo;
            process.Start();
            lblToolStrip.Text = "Сервис запущен";
        }

        private void BtnInfoLic_Click(object sender, EventArgs e)
        {
            try
            {

                if (!(File.Exists(txbKey.Text)))
                    throw new Exception("Не найден файл лицензии!");
                else
                {
                    var keyManager = new KeyManager();
                    var licInfo = keyManager.LoadFile(txbKey.Text);

                    richTextBox.AppendText($"\nКомпания {licInfo.CompanyName}");
                    richTextBox.AppendText($"\nВерсия ПО {licInfo.KeyInfo.Version}");
                    richTextBox.AppendText($"\nВерсия лицензии {licInfo.KeyInfo.Edition}");
                    richTextBox.AppendText($"\nВид лицензии {licInfo.KeyInfo.Kind}");
                    richTextBox.AppendText($"\nКоличество клиентов {licInfo.KeyInfo.NumberOfClients}");

                    if (licInfo.KeyInfo.Type == LicenseType.Trial)
                        richTextBox.AppendText($"\nВремя окончания {licInfo.KeyInfo.Expiration}");
                    else
                        richTextBox.AppendText($"\nТип лицензии {licInfo.KeyInfo.Type}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnLoadConfig_Click(object sender, EventArgs e)
        {
            try
            {
                var serverConfig =
        (ServiceConfig)JsonConvert.DeserializeObject(
            File.ReadAllText(
                $@"{Environment.GetEnvironmentVariable("LicenseServerPath", EnvironmentVariableTarget.Machine)}\serverConfig.json"),
            typeof(ServiceConfig));
                if (serverConfig != null)
                {
                    txbKey.Text = serverConfig.licFile;
                    txbPort.Text = serverConfig.port;

                    dgv.Rows.Clear();
                    serverConfig.clientsIp.ForEach(x => dgv.Rows.Add(x));
                    txbLog.Text = serverConfig.logFile;
                }
                else
                    throw new SerializationException();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                CreateConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}