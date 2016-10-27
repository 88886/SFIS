namespace SFIS_V2
{
    partial class Frm_SetDay
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
            this.Btn_OK = new DevComponents.DotNetBar.ButtonX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.Cmb_ReCheckDay = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Btn_OK
            // 
            this.Btn_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Btn_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Btn_OK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_OK.Location = new System.Drawing.Point(269, 9);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(86, 26);
            this.Btn_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Btn_OK.TabIndex = 19;
            this.Btn_OK.Text = "确定";
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.Class = "";
            this.labelX9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX9.Location = new System.Drawing.Point(12, 12);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(94, 23);
            this.labelX9.TabIndex = 20;
            this.labelX9.Text = "重检天数：";
            // 
            // Cmb_ReCheckDay
            // 
            this.Cmb_ReCheckDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_ReCheckDay.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cmb_ReCheckDay.FormattingEnabled = true;
            this.Cmb_ReCheckDay.Items.AddRange(new object[] {
            "IMEI",
            "ESN"});
            this.Cmb_ReCheckDay.Location = new System.Drawing.Point(98, 9);
            this.Cmb_ReCheckDay.Name = "Cmb_ReCheckDay";
            this.Cmb_ReCheckDay.Size = new System.Drawing.Size(155, 24);
            this.Cmb_ReCheckDay.TabIndex = 21;
            // 
            // Frm_SetDay
            // 
            this.ClientSize = new System.Drawing.Size(367, 54);
            this.Controls.Add(this.Cmb_ReCheckDay);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.Btn_OK);
            this.DoubleBuffered = true;
            this.Name = "Frm_SetDay";
            this.Text = "Frm_SetDay";
            this.Load += new System.EventHandler(this.Frm_SetDay_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX Btn_OK;
        private DevComponents.DotNetBar.LabelX labelX9;
        private System.Windows.Forms.ComboBox Cmb_ReCheckDay;
    }
}