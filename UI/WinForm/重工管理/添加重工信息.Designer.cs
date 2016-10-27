namespace SFIS_V2
{
    partial class FrmReworkInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_woid = new System.Windows.Forms.TextBox();
            this.tb_partnumber = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.imbt_OK = new DevComponents.DotNetBar.ButtonX();
            this.imbt_exit = new DevComponents.DotNetBar.ButtonX();
            this.tb_memo = new System.Windows.Forms.TextBox();
            this.tb_RewDesc = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "工单:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "料号:";
            // 
            // tb_woid
            // 
            this.tb_woid.Location = new System.Drawing.Point(138, 36);
            this.tb_woid.Name = "tb_woid";
            this.tb_woid.Size = new System.Drawing.Size(145, 21);
            this.tb_woid.TabIndex = 2;
            // 
            // tb_partnumber
            // 
            this.tb_partnumber.Location = new System.Drawing.Point(138, 79);
            this.tb_partnumber.Name = "tb_partnumber";
            this.tb_partnumber.Size = new System.Drawing.Size(145, 21);
            this.tb_partnumber.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_memo);
            this.groupBox1.Location = new System.Drawing.Point(13, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 108);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "备注:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_RewDesc);
            this.groupBox2.Location = new System.Drawing.Point(15, 259);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(399, 108);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "重工原因:";
            // 
            // imbt_OK
            // 
            this.imbt_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_OK.Location = new System.Drawing.Point(71, 392);
            this.imbt_OK.Name = "imbt_OK";
            this.imbt_OK.Size = new System.Drawing.Size(75, 23);
            this.imbt_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_OK.TabIndex = 6;
            this.imbt_OK.Text = "提交";
            this.imbt_OK.Click += new System.EventHandler(this.imbt_OK_Click);
            // 
            // imbt_exit
            // 
            this.imbt_exit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_exit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_exit.Location = new System.Drawing.Point(286, 391);
            this.imbt_exit.Name = "imbt_exit";
            this.imbt_exit.Size = new System.Drawing.Size(75, 23);
            this.imbt_exit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_exit.TabIndex = 7;
            this.imbt_exit.Text = "退出";
            this.imbt_exit.Click += new System.EventHandler(this.imbt_exit_Click);
            // 
            // tb_memo
            // 
            this.tb_memo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_memo.Location = new System.Drawing.Point(3, 17);
            this.tb_memo.Multiline = true;
            this.tb_memo.Name = "tb_memo";
            this.tb_memo.Size = new System.Drawing.Size(393, 88);
            this.tb_memo.TabIndex = 0;
            // 
            // tb_RewDesc
            // 
            this.tb_RewDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_RewDesc.Location = new System.Drawing.Point(3, 17);
            this.tb_RewDesc.Multiline = true;
            this.tb_RewDesc.Name = "tb_RewDesc";
            this.tb_RewDesc.Size = new System.Drawing.Size(393, 88);
            this.tb_RewDesc.TabIndex = 0;
            // 
            // FrmReworkInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(434, 428);
            this.Controls.Add(this.imbt_exit);
            this.Controls.Add(this.imbt_OK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_partnumber);
            this.Controls.Add(this.tb_woid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReworkInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReworkInfo";
            this.Load += new System.EventHandler(this.FrmReworkInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.ButtonX imbt_OK;
        private DevComponents.DotNetBar.ButtonX imbt_exit;
        public System.Windows.Forms.TextBox tb_woid;
        public System.Windows.Forms.TextBox tb_partnumber;
        public System.Windows.Forms.TextBox tb_memo;
        public System.Windows.Forms.TextBox tb_RewDesc;
    }
}