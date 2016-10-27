namespace SFIS_V2
{
    partial class Frm_LabelQty
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
            this.numqty = new System.Windows.Forms.NumericUpDown();
            this.imbt_OK = new DevComponents.DotNetBar.ButtonX();
            this.imbt_Cancel = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.numqty)).BeginInit();
            this.SuspendLayout();
            // 
            // numqty
            // 
            this.numqty.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numqty.Location = new System.Drawing.Point(93, 56);
            this.numqty.Name = "numqty";
            this.numqty.Size = new System.Drawing.Size(124, 30);
            this.numqty.TabIndex = 0;
            this.numqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // imbt_OK
            // 
            this.imbt_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_OK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_OK.Location = new System.Drawing.Point(44, 134);
            this.imbt_OK.Name = "imbt_OK";
            this.imbt_OK.Size = new System.Drawing.Size(75, 33);
            this.imbt_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_OK.TabIndex = 1;
            this.imbt_OK.Text = "OK";
            this.imbt_OK.Click += new System.EventHandler(this.imbt_OK_Click);
            // 
            // imbt_Cancel
            // 
            this.imbt_Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_Cancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_Cancel.Location = new System.Drawing.Point(188, 134);
            this.imbt_Cancel.Name = "imbt_Cancel";
            this.imbt_Cancel.Size = new System.Drawing.Size(75, 33);
            this.imbt_Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_Cancel.TabIndex = 2;
            this.imbt_Cancel.Text = "Cancel";
            this.imbt_Cancel.Click += new System.EventHandler(this.imbt_Cancel_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(56, 27);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(161, 23);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "Carton Label Qty";
            this.labelX1.Visible = false;
            // 
            // Frm_LabelQty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 193);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.imbt_Cancel);
            this.Controls.Add(this.imbt_OK);
            this.Controls.Add(this.numqty);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_LabelQty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Label Print Qty";
            this.Load += new System.EventHandler(this.Frm_LabelQty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numqty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numqty;
        private DevComponents.DotNetBar.ButtonX imbt_OK;
        private DevComponents.DotNetBar.ButtonX imbt_Cancel;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}