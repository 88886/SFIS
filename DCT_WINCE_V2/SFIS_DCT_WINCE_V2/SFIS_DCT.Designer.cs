namespace SFIS_DCT_WINCE_V2
{
    partial class Frm_DCT
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.PalData = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbMessage = new System.Windows.Forms.ListBox();
            this.tbInputData = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.PalConfig = new System.Windows.Forms.Panel();
            this.Butok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbstationid = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbprocedures = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbroute = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbline = new System.Windows.Forms.ComboBox();
            this.cbwo = new System.Windows.Forms.ComboBox();
            this.PalData.SuspendLayout();
            this.PalConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // PalData
            // 
            this.PalData.Controls.Add(this.label5);
            this.PalData.Controls.Add(this.button1);
            this.PalData.Controls.Add(this.tbMessage);
            this.PalData.Controls.Add(this.tbInputData);
            this.PalData.Controls.Add(this.label6);
            this.PalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PalData.Location = new System.Drawing.Point(0, 64);
            this.PalData.Name = "PalData";
            this.PalData.Size = new System.Drawing.Size(638, 391);
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(0, 371);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(638, 20);
            this.label5.Text = "label5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(281, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 20);
            this.button1.TabIndex = 10;
            this.button1.Text = "关闭";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(19, 62);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(260, 66);
            this.tbMessage.TabIndex = 2;
            this.tbMessage.GotFocus += new System.EventHandler(this.tbMessage_GotFocus);
            // 
            // tbInputData
            // 
            this.tbInputData.Location = new System.Drawing.Point(19, 34);
            this.tbInputData.Name = "tbInputData";
            this.tbInputData.Size = new System.Drawing.Size(260, 23);
            this.tbInputData.TabIndex = 1;
            this.tbInputData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInputData_KeyDown);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.label6.Location = new System.Drawing.Point(4, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(318, 14);
            this.label6.Text = "输入资料:";
            // 
            // PalConfig
            // 
            this.PalConfig.Controls.Add(this.Butok);
            this.PalConfig.Controls.Add(this.label1);
            this.PalConfig.Controls.Add(this.lbstationid);
            this.PalConfig.Controls.Add(this.label2);
            this.PalConfig.Controls.Add(this.cbprocedures);
            this.PalConfig.Controls.Add(this.label3);
            this.PalConfig.Controls.Add(this.cbroute);
            this.PalConfig.Controls.Add(this.label4);
            this.PalConfig.Controls.Add(this.cbline);
            this.PalConfig.Controls.Add(this.cbwo);
            this.PalConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.PalConfig.Location = new System.Drawing.Point(0, 0);
            this.PalConfig.Name = "PalConfig";
            this.PalConfig.Size = new System.Drawing.Size(638, 64);
            // 
            // Butok
            // 
            this.Butok.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Butok.Location = new System.Drawing.Point(282, 7);
            this.Butok.Name = "Butok";
            this.Butok.Size = new System.Drawing.Size(30, 20);
            this.Butok.TabIndex = 9;
            this.Butok.Text = "OK";
            this.Butok.Click += new System.EventHandler(this.Butok_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.Text = "工单:";
            // 
            // lbstationid
            // 
            this.lbstationid.Location = new System.Drawing.Point(4, 50);
            this.lbstationid.Name = "lbstationid";
            this.lbstationid.Size = new System.Drawing.Size(280, 15);
            this.lbstationid.Text = "label5";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(138, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 20);
            this.label2.Text = "线别:";
            // 
            // cbprocedures
            // 
            this.cbprocedures.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cbprocedures.Location = new System.Drawing.Point(177, 30);
            this.cbprocedures.Name = "cbprocedures";
            this.cbprocedures.Size = new System.Drawing.Size(95, 19);
            this.cbprocedures.TabIndex = 7;
            this.cbprocedures.TextChanged += new System.EventHandler(this.cbprocedures_TextChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(4, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 20);
            this.label3.Text = "途程:";
            // 
            // cbroute
            // 
            this.cbroute.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cbroute.Location = new System.Drawing.Point(41, 30);
            this.cbroute.Name = "cbroute";
            this.cbroute.Size = new System.Drawing.Size(83, 19);
            this.cbroute.TabIndex = 6;
            this.cbroute.TextChanged += new System.EventHandler(this.cbroute_TextChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(126, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.Text = "行为模式:";
            // 
            // cbline
            // 
            this.cbline.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cbline.Location = new System.Drawing.Point(177, 6);
            this.cbline.Name = "cbline";
            this.cbline.Size = new System.Drawing.Size(95, 19);
            this.cbline.TabIndex = 5;
            // 
            // cbwo
            // 
            this.cbwo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cbwo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cbwo.Location = new System.Drawing.Point(41, 7);
            this.cbwo.Name = "cbwo";
            this.cbwo.Size = new System.Drawing.Size(83, 19);
            this.cbwo.TabIndex = 4;
            this.cbwo.Validating += new System.ComponentModel.CancelEventHandler(this.cbwo_Validating);
            this.cbwo.TextChanged += new System.EventHandler(this.cbwo_TextChanged);
            // 
            // Frm_DCT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.PalData);
            this.Controls.Add(this.PalConfig);
            this.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_DCT";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SFIS_DCT_V2_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Frm_DCT_Closing);
            this.PalData.ResumeLayout(false);
            this.PalConfig.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PalData;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox tbMessage;
        private System.Windows.Forms.TextBox tbInputData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel PalConfig;
        private System.Windows.Forms.Button Butok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbstationid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbprocedures;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbroute;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbline;
        private System.Windows.Forms.ComboBox cbwo;
        private System.Windows.Forms.Label label5;
    }
}

