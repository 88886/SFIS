namespace ColorBoxPrint
{
    partial class Frm_RePrint
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
            this.tb_data = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imbt_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_data
            // 
            this.tb_data.Location = new System.Drawing.Point(22, 30);
            this.tb_data.Name = "tb_data";
            this.tb_data.Size = new System.Drawing.Size(180, 21);
            this.tb_data.TabIndex = 0;
            this.tb_data.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_data_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "ESN/IMEI";
            // 
            // imbt_close
            // 
            this.imbt_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.imbt_close.Location = new System.Drawing.Point(458, 13);
            this.imbt_close.Name = "imbt_close";
            this.imbt_close.Size = new System.Drawing.Size(28, 19);
            this.imbt_close.TabIndex = 2;
            this.imbt_close.Text = "button1";
            this.imbt_close.UseVisualStyleBackColor = true;
            this.imbt_close.Click += new System.EventHandler(this.imbt_close_Click);
            // 
            // Frm_RePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.CancelButton = this.imbt_close;
            this.ClientSize = new System.Drawing.Size(234, 76);
            this.Controls.Add(this.imbt_close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_data);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_RePrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_RePrint";
            this.Load += new System.EventHandler(this.Frm_RePrint_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_data;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button imbt_close;
    }
}