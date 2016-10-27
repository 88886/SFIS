namespace SFIS_V2
{
    partial class Frm_SelcetNextStation
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Lab_ReturnStation = new System.Windows.Forms.Label();
            this.cb_NextStation = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.imbt_OK = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Lab_ReturnStation);
            this.groupBox1.Controls.Add(this.cb_NextStation);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // Lab_ReturnStation
            // 
            this.Lab_ReturnStation.AutoSize = true;
            this.Lab_ReturnStation.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_ReturnStation.ForeColor = System.Drawing.Color.Navy;
            this.Lab_ReturnStation.Location = new System.Drawing.Point(28, 23);
            this.Lab_ReturnStation.Name = "Lab_ReturnStation";
            this.Lab_ReturnStation.Size = new System.Drawing.Size(143, 16);
            this.Lab_ReturnStation.TabIndex = 1;
            this.Lab_ReturnStation.Text = "Return Station:";
            // 
            // cb_NextStation
            // 
            this.cb_NextStation.DisplayMember = "Text";
            this.cb_NextStation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_NextStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_NextStation.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_NextStation.FormattingEnabled = true;
            this.cb_NextStation.ItemHeight = 20;
            this.cb_NextStation.Location = new System.Drawing.Point(29, 52);
            this.cb_NextStation.Name = "cb_NextStation";
            this.cb_NextStation.Size = new System.Drawing.Size(225, 26);
            this.cb_NextStation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_NextStation.TabIndex = 0;
            // 
            // imbt_OK
            // 
            this.imbt_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_OK.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_OK.Location = new System.Drawing.Point(90, 125);
            this.imbt_OK.Name = "imbt_OK";
            this.imbt_OK.Size = new System.Drawing.Size(93, 34);
            this.imbt_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_OK.TabIndex = 1;
            this.imbt_OK.Text = "OK";
            this.imbt_OK.Click += new System.EventHandler(this.imbt_OK_Click);
            // 
            // Frm_SelcetNextStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 171);
            this.Controls.Add(this.imbt_OK);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_SelcetNextStation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelcetNextStation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_SelcetNextStation_FormClosing);
            this.Load += new System.EventHandler(this.Frm_SelcetNextStation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_NextStation;
        private DevComponents.DotNetBar.ButtonX imbt_OK;
        private System.Windows.Forms.Label Lab_ReturnStation;
    }
}