namespace SFIS_V2
{
    partial class Frm_CheckListBox
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
            this.ChkListData = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.imbt_cancel = new DevComponents.DotNetBar.ButtonX();
            this.imbt_ok = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkListData
            // 
            this.ChkListData.CheckOnClick = true;
            this.ChkListData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChkListData.FormattingEnabled = true;
            this.ChkListData.Location = new System.Drawing.Point(3, 17);
            this.ChkListData.MultiColumn = true;
            this.ChkListData.Name = "ChkListData";
            this.ChkListData.Size = new System.Drawing.Size(562, 258);
            this.ChkListData.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkListData);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 278);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.imbt_cancel);
            this.panelEx1.Controls.Add(this.imbt_ok);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx1.Location = new System.Drawing.Point(0, 278);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(568, 60);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 2;
            // 
            // imbt_cancel
            // 
            this.imbt_cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_cancel.Location = new System.Drawing.Point(354, 15);
            this.imbt_cancel.Name = "imbt_cancel";
            this.imbt_cancel.Size = new System.Drawing.Size(75, 33);
            this.imbt_cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_cancel.TabIndex = 0;
            this.imbt_cancel.Text = "退出";
            this.imbt_cancel.Click += new System.EventHandler(this.imbt_cancel_Click);
            // 
            // imbt_ok
            // 
            this.imbt_ok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_ok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_ok.Location = new System.Drawing.Point(92, 15);
            this.imbt_ok.Name = "imbt_ok";
            this.imbt_ok.Size = new System.Drawing.Size(75, 33);
            this.imbt_ok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_ok.TabIndex = 0;
            this.imbt_ok.Text = "选择";
            this.imbt_ok.Click += new System.EventHandler(this.imbt_ok_Click);
            // 
            // Frm_CheckListBox
            // 
            this.ClientSize = new System.Drawing.Size(568, 338);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_CheckListBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_CheckListBox";
            this.Load += new System.EventHandler(this.Frm_CheckListBox_Load);
            this.groupBox1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ChkListData;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX imbt_cancel;
        private DevComponents.DotNetBar.ButtonX imbt_ok;
    }
}