namespace SFIS_PRINT_SYSTEM_WIFI
{
    partial class ServerIpConfig
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
            this.bt_saveip = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_ipaddress = new DevComponents.Editors.IpAddressInput();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ipaddress)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_saveip
            // 
            this.bt_saveip.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_saveip.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_saveip.Location = new System.Drawing.Point(148, 79);
            this.bt_saveip.Name = "bt_saveip";
            this.bt_saveip.Size = new System.Drawing.Size(61, 23);
            this.bt_saveip.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_saveip.TabIndex = 0;
            this.bt_saveip.Text = "保存";
            this.bt_saveip.Click += new System.EventHandler(this.bt_saveip_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "[WebService IP]:";
            // 
            // tb_ipaddress
            // 
            this.tb_ipaddress.AutoOverwrite = true;
            // 
            // 
            // 
            this.tb_ipaddress.BackgroundStyle.Class = "DateTimeInputBackground";
            this.tb_ipaddress.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.tb_ipaddress.ButtonFreeText.Visible = true;
            this.tb_ipaddress.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_ipaddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tb_ipaddress.Location = new System.Drawing.Point(14, 41);
            this.tb_ipaddress.Name = "tb_ipaddress";
            this.tb_ipaddress.Size = new System.Drawing.Size(195, 26);
            this.tb_ipaddress.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tb_ipaddress.TabIndex = 2;
            // 
            // ServerIpConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 110);
            this.Controls.Add(this.tb_ipaddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_saveip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerIpConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WEBSERVICE IP地址设置";
            ((System.ComponentModel.ISupportInitialize)(this.tb_ipaddress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX bt_saveip;
        private System.Windows.Forms.Label label1;
        private DevComponents.Editors.IpAddressInput tb_ipaddress;
    }
}