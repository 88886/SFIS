namespace Packing_Ctn
{
    partial class Frm_NoCloseCtn
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
            this.tb_wo = new System.Windows.Forms.TextBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.lv_Carton = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // tb_wo
            // 
            this.tb_wo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_wo.Location = new System.Drawing.Point(120, 24);
            this.tb_wo.Name = "tb_wo";
            this.tb_wo.Size = new System.Drawing.Size(237, 26);
            this.tb_wo.TabIndex = 0;
            this.tb_wo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_wo_KeyDown);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(54, 27);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(48, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "WO:";
            // 
            // lv_Carton
            // 
            this.lv_Carton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lv_Carton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lv_Carton.GridLines = true;
            this.lv_Carton.Location = new System.Drawing.Point(0, 92);
            this.lv_Carton.Name = "lv_Carton";
            this.lv_Carton.Size = new System.Drawing.Size(561, 218);
            this.lv_Carton.TabIndex = 2;
            this.lv_Carton.UseCompatibleStateImageBehavior = false;
            // 
            // Frm_NoCloseCtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 310);
            this.Controls.Add(this.lv_Carton);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.tb_wo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_NoCloseCtn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_NoCloseCtn";
            this.Load += new System.EventHandler(this.Frm_NoCloseCtn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_wo;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.ListView lv_Carton;
    }
}