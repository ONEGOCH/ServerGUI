﻿namespace ServerGUI
{
    partial class Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnInfoConnection = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.btnAddClient = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbKey = new System.Windows.Forms.TextBox();
            this.txbLog = new System.Windows.Forms.TextBox();
            this.txbPort = new System.Windows.Forms.TextBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.IpAdr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnInfoLic = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInfoConnection
            // 
            this.btnInfoConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInfoConnection.Location = new System.Drawing.Point(3, 544);
            this.btnInfoConnection.Name = "btnInfoConnection";
            this.btnInfoConnection.Size = new System.Drawing.Size(156, 34);
            this.btnInfoConnection.TabIndex = 0;
            this.btnInfoConnection.Text = "Инфо о подключениях";
            this.btnInfoConnection.UseVisualStyleBackColor = true;
            this.btnInfoConnection.Click += new System.EventHandler(this.InfoBtb_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 181F));
            this.tableLayoutPanel1.Controls.Add(this.richTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnStop, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnInfoConnection, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCreate, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnStart, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnInfoLic, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 581);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // richTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.richTextBox, 2);
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Location = new System.Drawing.Point(3, 3);
            this.richTextBox.Name = "richTextBox";
            this.tableLayoutPanel1.SetRowSpan(this.richTextBox, 3);
            this.richTextBox.Size = new System.Drawing.Size(318, 535);
            this.richTextBox.TabIndex = 2;
            this.richTextBox.Text = "";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnLoadConfig);
            this.panel1.Controls.Add(this.btnAddClient);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txbKey);
            this.panel1.Controls.Add(this.txbLog);
            this.panel1.Controls.Add(this.txbPort);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(327, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(355, 165);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ключ";
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.AutoSize = true;
            this.btnLoadConfig.Location = new System.Drawing.Point(149, 128);
            this.btnLoadConfig.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(140, 27);
            this.btnLoadConfig.TabIndex = 2;
            this.btnLoadConfig.Text = "Загрузить сведения";
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // btnAddClient
            // 
            this.btnAddClient.AutoSize = true;
            this.btnAddClient.Location = new System.Drawing.Point(3, 128);
            this.btnAddClient.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.btnAddClient.Name = "btnAddClient";
            this.btnAddClient.Size = new System.Drawing.Size(140, 27);
            this.btnAddClient.TabIndex = 2;
            this.btnAddClient.Text = "Добавить клиента";
            this.btnAddClient.UseVisualStyleBackColor = true;
            this.btnAddClient.Click += new System.EventHandler(this.BtnAddClient_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Лог";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Порт";
            // 
            // txbKey
            // 
            this.txbKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbKey.Location = new System.Drawing.Point(3, 103);
            this.txbKey.Name = "txbKey";
            this.txbKey.Size = new System.Drawing.Size(351, 20);
            this.txbKey.TabIndex = 0;
            this.txbKey.Text = "Укажите лицензионный ключ";
            this.txbKey.Click += new System.EventHandler(this.txbKey_Click);
            // 
            // txbLog
            // 
            this.txbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbLog.Location = new System.Drawing.Point(3, 65);
            this.txbLog.Name = "txbLog";
            this.txbLog.Size = new System.Drawing.Size(351, 20);
            this.txbLog.TabIndex = 0;
            this.txbLog.Text = "Укажите выходной файл";
            this.txbLog.Click += new System.EventHandler(this.txbLog_Click);
            // 
            // txbPort
            // 
            this.txbPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbPort.Location = new System.Drawing.Point(3, 27);
            this.txbPort.Name = "txbPort";
            this.txbPort.Size = new System.Drawing.Size(351, 20);
            this.txbPort.TabIndex = 0;
            this.txbPort.Text = "Укажите порт";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IpAdr});
            this.tableLayoutPanel1.SetColumnSpan(this.dgv, 2);
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(327, 174);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(355, 324);
            this.dgv.TabIndex = 4;
            // 
            // IpAdr
            // 
            this.IpAdr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IpAdr.HeaderText = "IP адреса клиентов";
            this.IpAdr.Name = "IpAdr";
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Location = new System.Drawing.Point(507, 544);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(175, 34);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Стоп";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Location = new System.Drawing.Point(507, 504);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(175, 34);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCreate.Location = new System.Drawing.Point(327, 504);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(174, 34);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Создать";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.Location = new System.Drawing.Point(327, 544);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(174, 34);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Старт";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnInfoLic
            // 
            this.btnInfoLic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInfoLic.Location = new System.Drawing.Point(165, 544);
            this.btnInfoLic.Name = "btnInfoLic";
            this.btnInfoLic.Size = new System.Drawing.Size(156, 34);
            this.btnInfoLic.TabIndex = 5;
            this.btnInfoLic.Text = "Инфо о лицензии";
            this.btnInfoLic.UseVisualStyleBackColor = true;
            this.btnInfoLic.Click += new System.EventHandler(this.btnInfoLic_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 581);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form";
            this.ShowIcon = false;
            this.Text = "Сервер лицензий";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnStart;

        private System.Windows.Forms.Button btnDelete;

        #endregion

        private System.Windows.Forms.Button btnInfoConnection;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbLog;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn IpAdr;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnAddClient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbKey;
        private System.Windows.Forms.Button btnInfoLic;
        private System.Windows.Forms.Button btnLoadConfig;
        //private System.Windows.Forms.CheckBox chbRunOnBackGround;
    }
}

