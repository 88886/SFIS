namespace SFIS_V2
{
    partial class FrmPartInfo
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
            this.tbPartnumber = new System.Windows.Forms.TextBox();
            this.tbproductname = new System.Windows.Forms.TextBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.label3 = new System.Windows.Forms.Label();
            this.tblineid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[成品料号]:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "[产品名称]:";
            // 
            // tbPartnumber
            // 
            this.tbPartnumber.Location = new System.Drawing.Point(21, 24);
            this.tbPartnumber.Name = "tbPartnumber";
            this.tbPartnumber.Size = new System.Drawing.Size(162, 21);
            this.tbPartnumber.TabIndex = 1;
            // 
            // tbproductname
            // 
            this.tbproductname.Location = new System.Drawing.Point(21, 76);
            this.tbproductname.Name = "tbproductname";
            this.tbproductname.Size = new System.Drawing.Size(162, 21);
            this.tbproductname.TabIndex = 2;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(124, 155);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(59, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 4;
            this.buttonX1.Text = "确定";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "[产线编号]:";
            // 
            // tblineid
            // 
            this.tblineid.Location = new System.Drawing.Point(21, 122);
            this.tblineid.Name = "tblineid";
            this.tblineid.Size = new System.Drawing.Size(162, 21);
            this.tblineid.TabIndex = 3;
            this.tblineid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tblineid_KeyDown);
            this.tblineid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tblineid_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(19, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "(格式:080101)";
            // 
            // FrmPartInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 184);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.tblineid);
            this.Controls.Add(this.tbproductname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbPartnumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPartInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产品信息";
            this.Load += new System.EventHandler(this.FrmPartInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPartnumber;
        private System.Windows.Forms.TextBox tbproductname;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tblineid;
        private System.Windows.Forms.Label label4;
    }
}