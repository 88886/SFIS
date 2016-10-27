namespace Packing_Ctn
{
    partial class Frm_Printcoordinates
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
            this.numerX = new System.Windows.Forms.NumericUpDown();
            this.numerY = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imbt_abort = new DevComponents.DotNetBar.ButtonX();
            this.imbt_OK = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.numerX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numerY)).BeginInit();
            this.SuspendLayout();
            // 
            // numerX
            // 
            this.numerX.DecimalPlaces = 2;
            this.numerX.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numerX.Location = new System.Drawing.Point(216, 30);
            this.numerX.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.numerX.Name = "numerX";
            this.numerX.Size = new System.Drawing.Size(98, 26);
            this.numerX.TabIndex = 0;
            this.numerX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numerY
            // 
            this.numerY.DecimalPlaces = 2;
            this.numerY.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numerY.Location = new System.Drawing.Point(216, 77);
            this.numerY.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.numerY.Name = "numerY";
            this.numerY.Size = new System.Drawing.Size(98, 26);
            this.numerY.TabIndex = 0;
            this.numerY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(26, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Coordinate X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(26, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Coordinate Y:";
            // 
            // imbt_abort
            // 
            this.imbt_abort.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_abort.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_abort.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_abort.Location = new System.Drawing.Point(237, 140);
            this.imbt_abort.Name = "imbt_abort";
            this.imbt_abort.Size = new System.Drawing.Size(97, 35);
            this.imbt_abort.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_abort.TabIndex = 10;
            this.imbt_abort.Text = "Abort";
            this.imbt_abort.Click += new System.EventHandler(this.imbt_abort_Click);
            // 
            // imbt_OK
            // 
            this.imbt_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_OK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_OK.Location = new System.Drawing.Point(47, 140);
            this.imbt_OK.Name = "imbt_OK";
            this.imbt_OK.Size = new System.Drawing.Size(97, 35);
            this.imbt_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_OK.TabIndex = 11;
            this.imbt_OK.Text = "OK";
            this.imbt_OK.Click += new System.EventHandler(this.imbt_OK_Click);
            // 
            // Frm_Printcoordinates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 202);
            this.Controls.Add(this.imbt_abort);
            this.Controls.Add(this.imbt_OK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numerY);
            this.Controls.Add(this.numerX);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Printcoordinates";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Coordinates";
            this.Load += new System.EventHandler(this.Frm_Printcoordinates_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numerX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numerY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numerX;
        private System.Windows.Forms.NumericUpDown numerY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX imbt_abort;
        private DevComponents.DotNetBar.ButtonX imbt_OK;
    }
}