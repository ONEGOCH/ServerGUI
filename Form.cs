using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerGUI
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
            this.Text = "Сервер лицензий v1.0.0";
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var myProcess = new Process();

                myProcess.StartInfo.FileName = txbServer.Text;

                var ipClientsDgvrs = new List<DataGridViewRow>(dgv.Rows.Cast<DataGridViewRow>());

                if (ipClientsDgvrs.Count == 0)
                    throw new Exception("Укажите хотя бы одного клиента!");

                var ipClientsList = ipClientsDgvrs.Select(x => x.Cells[0].Value.ToString()).ToList();
                var ipClientsStr = string.Join(";", ipClientsList);

                var argStr = string.Join(" ", new string[] { txbKey.Text, txbPort.Text, ipClientsStr, txbLog.Text });

                myProcess.StartInfo.Arguments = argStr;
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                myProcess.Start();
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
            foreach (var process in Process.GetProcessesByName("BazisServer"))
            {
                process.Kill();

                var sw = new StreamWriter(txbLog.Text,true);

                sw.WriteLine("Сервер остановлен");
                sw.Close();
                sw.Dispose();
            }
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
