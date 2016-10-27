namespace SFIS_V2
{
    partial class Frm_Sel_Info
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.clb_labeltypes = new System.Windows.Forms.CheckedListBox();
            this.panelEx8 = new DevComponents.DotNetBar.PanelEx();
            this.chk_stockno = new System.Windows.Forms.CheckBox();
            this.chk_mac = new System.Windows.Forms.CheckBox();
            this.chk_sn = new System.Windows.Forms.CheckBox();
            this.chk_PalletNo = new System.Windows.Forms.CheckBox();
            this.chk_CartonNo = new System.Windows.Forms.CheckBox();
            this.chk_tray = new System.Windows.Forms.CheckBox();
            this.Btn_OK = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.panelEx8.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.clb_labeltypes);
            this.panelEx1.Controls.Add(this.panelEx8);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(323, 470);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 22;
            this.panelEx1.Text = "panelEx1";
            // 
            // clb_labeltypes
            // 
            this.clb_labeltypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clb_labeltypes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clb_labeltypes.FormattingEnabled = true;
            this.clb_labeltypes.Location = new System.Drawing.Point(0, 182);
            this.clb_labeltypes.Name = "clb_labeltypes";
            this.clb_labeltypes.Size = new System.Drawing.Size(323, 288);
            this.clb_labeltypes.TabIndex = 2;
            // 
            // panelEx8
            // 
            this.panelEx8.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx8.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx8.Controls.Add(this.chk_stockno);
            this.panelEx8.Controls.Add(this.chk_mac);
            this.panelEx8.Controls.Add(this.chk_sn);
            this.panelEx8.Controls.Add(this.chk_PalletNo);
            this.panelEx8.Controls.Add(this.chk_CartonNo);
            this.panelEx8.Controls.Add(this.chk_tray);
            this.panelEx8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx8.Location = new System.Drawing.Point(0, 0);
            this.panelEx8.Name = "panelEx8";
            this.panelEx8.Size = new System.Drawing.Size(323, 182);
            this.panelEx8.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx8.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx8.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx8.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx8.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx8.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx8.Style.GradientAngle = 90;
            this.panelEx8.TabIndex = 3;
            // 
            // chk_stockno
            // 
            this.chk_stockno.AutoSize = true;
            this.chk_stockno.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_stockno.Location = new System.Drawing.Point(12, 135);
            this.chk_stockno.Name = "chk_stockno";
            this.chk_stockno.Size = new System.Drawing.Size(171, 20);
            this.chk_stockno.TabIndex = 1;
            this.chk_stockno.Text = "Remove storenumber";
            this.chk_stockno.UseVisualStyleBackColor = true;
            // 
            // chk_mac
            // 
            this.chk_mac.AutoSize = true;
            this.chk_mac.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_mac.Location = new System.Drawing.Point(12, 109);
            this.chk_mac.Name = "chk_mac";
            this.chk_mac.Size = new System.Drawing.Size(115, 20);
            this.chk_mac.TabIndex = 0;
            this.chk_mac.Text = "Remove MAC.";
            this.chk_mac.UseVisualStyleBackColor = true;
            // 
            // chk_sn
            // 
            this.chk_sn.AutoSize = true;
            this.chk_sn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_sn.Location = new System.Drawing.Point(12, 84);
            this.chk_sn.Name = "chk_sn";
            this.chk_sn.Size = new System.Drawing.Size(107, 20);
            this.chk_sn.TabIndex = 0;
            this.chk_sn.Text = "Remove SN.";
            this.chk_sn.UseVisualStyleBackColor = true;
            // 
            // chk_PalletNo
            // 
            this.chk_PalletNo.AutoSize = true;
            this.chk_PalletNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_PalletNo.Location = new System.Drawing.Point(12, 58);
            this.chk_PalletNo.Name = "chk_PalletNo";
            this.chk_PalletNo.Size = new System.Drawing.Size(155, 20);
            this.chk_PalletNo.TabIndex = 0;
            this.chk_PalletNo.Text = "Remove PalletNo.";
            this.chk_PalletNo.UseVisualStyleBackColor = true;
            // 
            // chk_CartonNo
            // 
            this.chk_CartonNo.AutoSize = true;
            this.chk_CartonNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_CartonNo.Location = new System.Drawing.Point(12, 29);
            this.chk_CartonNo.Name = "chk_CartonNo";
            this.chk_CartonNo.Size = new System.Drawing.Size(155, 20);
            this.chk_CartonNo.TabIndex = 0;
            this.chk_CartonNo.Text = "Remove CartonNo.";
            this.chk_CartonNo.UseVisualStyleBackColor = true;
            // 
            // chk_tray
            // 
            this.chk_tray.AutoSize = true;
            this.chk_tray.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_tray.Location = new System.Drawing.Point(12, 3);
            this.chk_tray.Name = "chk_tray";
            this.chk_tray.Size = new System.Drawing.Size(139, 20);
            this.chk_tray.TabIndex = 0;
            this.chk_tray.Text = "Remove TrayNo.";
            this.chk_tray.UseVisualStyleBackColor = true;
            // 
            // Btn_OK
            // 
            this.Btn_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Btn_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Btn_OK.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Btn_OK.Location = new System.Drawing.Point(0, 413);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(323, 57);
            this.Btn_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Btn_OK.TabIndex = 23;
            this.Btn_OK.Text = "OK";
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Frm_Sel_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 470);
            this.Controls.Add(this.Btn_OK);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.Name = "Frm_Sel_Info";
            this.Text = "Frm_Sel_Info";
            this.Load += new System.EventHandler(this.Frm_Sel_Info_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx8.ResumeLayout(false);
            this.panelEx8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx8;
        private System.Windows.Forms.CheckBox chk_PalletNo;
        private System.Windows.Forms.CheckBox chk_CartonNo;
        private DevComponents.DotNetBar.ButtonX Btn_OK;
        private System.Windows.Forms.CheckedListBox clb_labeltypes;
        private System.Windows.Forms.CheckBox chk_stockno;
        private System.Windows.Forms.CheckBox chk_mac;
        private System.Windows.Forms.CheckBox chk_sn;
        private System.Windows.Forms.CheckBox chk_tray;
    }
}