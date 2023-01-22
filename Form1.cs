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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                var myProcess = new Process();

                myProcess.StartInfo.FileName = txbServer.Text;

                var portStr = txbPort.Text;
                var logStr = txbLog.Text;

                var argStr = string.Join(" ", new string[] { portStr, logStr });

                myProcess.StartInfo.Arguments = argStr;
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                myProcess.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                var fileStream = new StreamReader(txbLog.Text);

                richTextBox1.Clear();
                var str = "";
                while (str != null)
                {
                    str = fileStream.ReadLineAsync().Result;
                    richTextBox1.AppendText("\n" + str);
                }
                fileStream.Close();
            }
            catch (Exception)
            {


            }
        }
    }
}
