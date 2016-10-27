namespace SFIS_V2
{
    partial class Frm_InputQty
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
            this.txt_qty = new System.Windows.Forms.TextBox();
            this.LabMsg = new System.Windows.Forms.Label();
            this.imbt_OK = new DevComponents.DotNetBar.ButtonX();
            this.imbt_cancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // txt_qty
            // 
            this.txt_qty.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_qty.Location = new System.Drawing.Point(53, 49);
            this.txt_qty.Name = "txt_qty";
            this.txt_qty.Size = new System.Drawing.Size(171, 35);
            this.txt_qty.TabIndex = 0;
            this.txt_qty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_qty_KeyDown);
            this.txt_qty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_qty_KeyPress);
            // 
            // LabMsg
            // 
            this.LabMsg.AutoSize = true;
            this.LabMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabMsg.Location = new System.Drawing.Point(85, 19);
            this.LabMsg.Name = "LabMsg";
            this.LabMsg.Size = new System.Drawing.Size(93, 16);
            this.LabMsg.TabIndex = 1;
            this.LabMsg.Text = "请输入数量";
            // 
            // imbt_OK
            // 
            this.imbt_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_OK.Location = new System.Drawing.Point(31, 108);
            this.imbt_OK.Name = "imbt_OK";
            this.imbt_OK.Size = new System.Drawing.Size(75, 23);
            this.imbt_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_OK.TabIndex = 2;
            this.imbt_OK.Text = "确认";
            this.imbt_OK.Click += new System.EventHandler(this.imbt_OK_Click);
            // 
            // imbt_cancel
            // 
            this.imbt_cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_cancel.Location = new System.Drawing.Point(149, 109);
            this.imbt_cancel.Name = "imbt_cancel";
            this.imbt_cancel.Size = new System.Drawing.Size(75, 23);
            this.imbt_cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_cancel.TabIndex = 2;
            this.imbt_cancel.Text = "退出";
            this.imbt_cancel.Click += new System.EventHandler(this.imbt_cancel_Click);
            // 
            // Frm_InputQty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 144);
            this.Controls.Add(this.imbt_cancel);
            this.Controls.Add(this.imbt_OK);
            this.Controls.Add(this.LabMsg);
            this.Controls.Add(this.txt_qty);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_InputQty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输入数量";
            this.Load += new System.EventHandler(this.Frm_InputQty_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_qty;
        private System.Windows.Forms.Label LabMsg;
        private DevComponents.DotNetBar.ButtonX imbt_OK;
        private DevComponents.DotNetBar.ButtonX imbt_cancel;
    }
}