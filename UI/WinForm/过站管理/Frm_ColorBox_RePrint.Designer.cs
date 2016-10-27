namespace SFIS_V2
{
    partial class Frm_ColorBox_RePrint
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
            this.txt_esn = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // txt_esn
            // 
            // 
            // 
            // 
            this.txt_esn.Border.Class = "TextBoxBorder";
            this.txt_esn.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_esn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_esn.Location = new System.Drawing.Point(92, 25);
            this.txt_esn.Name = "txt_esn";
            this.txt_esn.PreventEnterBeep = true;
            this.txt_esn.Size = new System.Drawing.Size(191, 26);
            this.txt_esn.TabIndex = 0;
            this.txt_esn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_esn_KeyDown);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(29, 28);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(57, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "ESN:";
            // 
            // Frm_ColorBox_RePrint
            // 
            this.ClientSize = new System.Drawing.Size(330, 87);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txt_esn);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ColorBox_RePrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重复打印";
            this.Load += new System.EventHandler(this.Frm_ColorBox_RePrint_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_ColorBox_RePrint_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txt_esn;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}