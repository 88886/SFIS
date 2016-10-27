namespace SFIS_V2
{
    partial class Frm_CloseCarton
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
            this.txt_data = new System.Windows.Forms.TextBox();
            this.radESN = new System.Windows.Forms.RadioButton();
            this.radSN = new System.Windows.Forms.RadioButton();
            this.radIMEI = new System.Windows.Forms.RadioButton();
            this.radCarton = new System.Windows.Forms.RadioButton();
            this.imbt_OK = new DevComponents.DotNetBar.ButtonX();
            this.imbt_abort = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // txt_data
            // 
            this.txt_data.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_data.Location = new System.Drawing.Point(67, 71);
            this.txt_data.Name = "txt_data";
            this.txt_data.Size = new System.Drawing.Size(287, 26);
            this.txt_data.TabIndex = 0;
            this.txt_data.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_data_KeyDown);
            // 
            // radESN
            // 
            this.radESN.AutoSize = true;
            this.radESN.Location = new System.Drawing.Point(67, 40);
            this.radESN.Name = "radESN";
            this.radESN.Size = new System.Drawing.Size(41, 16);
            this.radESN.TabIndex = 1;
            this.radESN.Text = "ESN";
            this.radESN.UseVisualStyleBackColor = true;
            // 
            // radSN
            // 
            this.radSN.AutoSize = true;
            this.radSN.Location = new System.Drawing.Point(129, 40);
            this.radSN.Name = "radSN";
            this.radSN.Size = new System.Drawing.Size(35, 16);
            this.radSN.TabIndex = 1;
            this.radSN.Text = "SN";
            this.radSN.UseVisualStyleBackColor = true;
            // 
            // radIMEI
            // 
            this.radIMEI.AutoSize = true;
            this.radIMEI.Location = new System.Drawing.Point(195, 40);
            this.radIMEI.Name = "radIMEI";
            this.radIMEI.Size = new System.Drawing.Size(47, 16);
            this.radIMEI.TabIndex = 1;
            this.radIMEI.Text = "IMEI";
            this.radIMEI.UseVisualStyleBackColor = true;
            // 
            // radCarton
            // 
            this.radCarton.AutoSize = true;
            this.radCarton.Checked = true;
            this.radCarton.Location = new System.Drawing.Point(260, 40);
            this.radCarton.Name = "radCarton";
            this.radCarton.Size = new System.Drawing.Size(59, 16);
            this.radCarton.TabIndex = 1;
            this.radCarton.TabStop = true;
            this.radCarton.Text = "Carton";
            this.radCarton.UseVisualStyleBackColor = true;
            // 
            // imbt_OK
            // 
            this.imbt_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_OK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_OK.Location = new System.Drawing.Point(67, 130);
            this.imbt_OK.Name = "imbt_OK";
            this.imbt_OK.Size = new System.Drawing.Size(97, 35);
            this.imbt_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_OK.TabIndex = 2;
            this.imbt_OK.Text = "OK";
            this.imbt_OK.Click += new System.EventHandler(this.imbt_OK_Click);
            // 
            // imbt_abort
            // 
            this.imbt_abort.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_abort.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_abort.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_abort.Location = new System.Drawing.Point(257, 130);
            this.imbt_abort.Name = "imbt_abort";
            this.imbt_abort.Size = new System.Drawing.Size(97, 35);
            this.imbt_abort.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_abort.TabIndex = 2;
            this.imbt_abort.Text = "Abort";
            this.imbt_abort.Click += new System.EventHandler(this.imbt_abort_Click);
            // 
            // Frm_CloseCarton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 198);
            this.Controls.Add(this.imbt_abort);
            this.Controls.Add(this.imbt_OK);
            this.Controls.Add(this.radCarton);
            this.Controls.Add(this.radIMEI);
            this.Controls.Add(this.radSN);
            this.Controls.Add(this.radESN);
            this.Controls.Add(this.txt_data);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_CloseCarton";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Close Carton";
            this.Load += new System.EventHandler(this.Frm_CloseCarton_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_data;
        private System.Windows.Forms.RadioButton radESN;
        private System.Windows.Forms.RadioButton radSN;
        private System.Windows.Forms.RadioButton radIMEI;
        private System.Windows.Forms.RadioButton radCarton;
        private DevComponents.DotNetBar.ButtonX imbt_OK;
        private DevComponents.DotNetBar.ButtonX imbt_abort;
    }
}