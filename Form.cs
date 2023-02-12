using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ServerLogic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ServerGUI
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var bit = Environment.Is64BitOperatingSystem ? "64" : "";

                var process = new Process();
                var startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments =
                        $@"/C C:\Windows\Microsoft.NET\Framework{bit}\v4.0.30319\InstallUtil.exe {Directory.GetCurrentDirectory()}\ServiceServer.exe"
                };
                process.StartInfo = startInfo;
                var config = new ServiceConfig
                {
                    port = txbPort.Text,
                    clientsIp = new List<DataGridViewRow>(dgv.Rows.Cast<DataGridViewRow>())
                        .Select(x => x.Cells[0].Value.ToString())
                        .ToList(),
                    licFile = txbKey.Text,
                    logFile = txbLog.Text
                };
                var configString = JsonSerializer.Serialize(config);
                using (var file = File.Open("serverConfig.json", FileMode.OpenOrCreate))
                    foreach (var symbol in configString)
                        file.WriteByte(Convert.ToByte(symbol));
                if (config.clientsIp.Count == 0)
                    throw new Exception("Укажите хотя бы одного клиента!");

                process.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
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
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = @"/C sc stop LicenseServer"
            };
            process.StartInfo = startInfo;
            process.Start();
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
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = @"/C sc delete LicenseServer"
            };
            process.StartInfo = startInfo;
            process.Start();
        }

        private void addClient_Click(object sender, EventArgs e)
        {
            dgv.Rows.Add();
        }

        private void txbServer_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == DialogResult.Cancel)
                return;

            txbServer.Text = openDialog.FileName;
        }

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
    }
}