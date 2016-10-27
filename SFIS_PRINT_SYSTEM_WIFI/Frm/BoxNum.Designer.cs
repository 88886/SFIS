namespace SFIS_PRINT_SYSTEM_WIFI
{
    partial class BoxNum
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
            this.tb_box = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "首箱箱号:";
            // 
            // tb_box
            // 
            this.tb_box.Location = new System.Drawing.Point(14, 38);
            this.tb_box.Name = "tb_box";
            this.tb_box.Size = new System.Drawing.Size(258, 21);
            this.tb_box.TabIndex = 1;
            this.tb_box.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_box_KeyDown);
            this.tb_box.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_box_KeyPress);
            // 
            // BoxNum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 71);
            this.Controls.Add(this.tb_box);
            this.Controls.Add(this.label1);
            this.Name = "BoxNum";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BoxNum";
            this.Load += new System.EventHandler(this.BoxNum_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_box;
    }
}