namespace SFIS_V2
{
    partial class Frm_ReworkPDMain
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
            this.imbt_ReworkPD = new DevComponents.DotNetBar.ButtonX();
            this.imbt_scrap = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.imbt_ReworkInfo = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imbt_ReworkPD
            // 
            this.imbt_ReworkPD.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_ReworkPD.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_ReworkPD.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_ReworkPD.Location = new System.Drawing.Point(124, 153);
            this.imbt_ReworkPD.Name = "imbt_ReworkPD";
            this.imbt_ReworkPD.Size = new System.Drawing.Size(187, 94);
            this.imbt_ReworkPD.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_ReworkPD.TabIndex = 0;
            this.imbt_ReworkPD.Text = "在线重工";
            this.imbt_ReworkPD.Click += new System.EventHandler(this.imbt_ReworkPD_Click);
            // 
            // imbt_scrap
            // 
            this.imbt_scrap.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_scrap.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_scrap.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_scrap.Location = new System.Drawing.Point(464, 153);
            this.imbt_scrap.Name = "imbt_scrap";
            this.imbt_scrap.Size = new System.Drawing.Size(187, 94);
            this.imbt_scrap.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_scrap.TabIndex = 0;
            this.imbt_scrap.Text = "虚拟入账";
            this.imbt_scrap.Click += new System.EventHandler(this.imbt_scrap_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.imbt_ReworkInfo);
            this.panelEx1.Controls.Add(this.imbt_scrap);
            this.panelEx1.Controls.Add(this.imbt_ReworkPD);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1046, 371);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            // 
            // imbt_ReworkInfo
            // 
            this.imbt_ReworkInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_ReworkInfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_ReworkInfo.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_ReworkInfo.Location = new System.Drawing.Point(762, 153);
            this.imbt_ReworkInfo.Name = "imbt_ReworkInfo";
            this.imbt_ReworkInfo.Size = new System.Drawing.Size(187, 94);
            this.imbt_ReworkInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_ReworkInfo.TabIndex = 0;
            this.imbt_ReworkInfo.Text = "重工信息查询";
            this.imbt_ReworkInfo.Click += new System.EventHandler(this.imbt_ReworkInfo_Click);
            // 
            // Frm_ReworkPDMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 371);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.Name = "Frm_ReworkPDMain";
            this.Text = "生产线重工功能";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_ReworkPDMain_Load);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX imbt_ReworkPD;
        private DevComponents.DotNetBar.ButtonX imbt_scrap;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX imbt_ReworkInfo;
    }
}