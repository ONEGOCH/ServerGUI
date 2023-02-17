using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var bit = Environment.Is64BitOperatingSystem ? "64" : "";
            richTextBox.AppendText("Создание сервиса \n");
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
            richTextBox.AppendText("\n Сервис создан \n");
            // try
            // {
            //     var myProcess = new Process();
            //
            //     myProcess.StartInfo.FileName = txbServer.Text;
            //
            //     if (chbRunOnBackGround.Checked)
            //         myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //
            //     var ipClientsDgvrs = new List<DataGridViewRow>(dgv.Rows.Cast<DataGridViewRow>());
            //
            //     
            //
            //     var ipClientsList = ipClientsDgvrs.Select(x => x.Cells[0].Value.ToString()).ToList();
            //     var ipClients = string.Join(";", ipClientsList);
            //
            //     var keyStr = $"\"{txbKey.Text}\"";
            //     var portStr = $"\"{txbPort.Text}\"";
            //     var ipClientsStr = $"\"{ipClients}\"";
            //     var logStr = $"\"{txbLog.Text}\"";
            //
            //     var argStr = string.Join(" ", new string[] { keyStr, portStr, ipClientsStr, logStr });
            //
            //     myProcess.StartInfo.Arguments = argStr;
            //     myProcess.Start();
            // }
            // catch (Exception ex)
            // {
            //     MessageBox.Show(ex.Message);
            // }
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
                    richTextBox.AppendText("\n" + str);
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
            richTextBox.AppendText("\n Остановка сервиса \n");
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = @"/C sc stop LicenseServer"
            };
            process.StartInfo = startInfo;
            process.Start();
            richTextBox.AppendText("\n Сервис остановлен \n");
            // foreach (var process in Process.GetProcessesByName("BazisServer"))
            // {
            //     process.Kill();
            //
            //     var sw = new StreamWriter(txbLog.Text, true);
            //
            //     sw.WriteLine("Сервер остановлен");
            //     sw.Close();
            //     sw.Dispose();
            // }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            
            richTextBox.AppendText("\n Удаление сервиса \n");
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = @"/C sc delete LicenseServer"
            };
            process.StartInfo = startInfo;
            process.Start();
            
            richTextBox.AppendText("\n Сервис удалён \n");
        }

        private void addClient_Click(object sender, EventArgs e)
        {
            dgv.Rows.Add();
        }

        // private void txbServer_Click(object sender, EventArgs e)
        // {
        //     var openDialog = new OpenFileDialog();
        //
        //     if (openDialog.ShowDialog() == DialogResult.Cancel)
        //         return;
        //
        //     txbServer.Text = openDialog.FileName;
        // }

        private void txbLog_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == DialogResult.Cancel)
                return;

            txbLog.Text = openDialog.FileName;
        }

        private void txbKey_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == DialogResult.Cancel)
                return;

            txbKey.Text = openDialog.FileName;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText("\n Начало работы сервиса \n");
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $@"/C sc start LicenseService",
                Verb = "runas"
            };
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}