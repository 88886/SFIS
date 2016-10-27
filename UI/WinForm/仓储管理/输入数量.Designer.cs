namespace SFIS_V2
{
    partial class InputQty
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
            this.bt_ok = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_qty = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cb_all = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // bt_ok
            // 
            this.bt_ok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_ok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_ok.Location = new System.Drawing.Point(52, 72);
            this.bt_ok.Name = "bt_ok";
            this.bt_ok.Size = new System.Drawing.Size(71, 23);
            this.bt_ok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_ok.TabIndex = 0;
            this.bt_ok.Text = "确定";
            this.bt_ok.Click += new System.EventHandler(this.bt_ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "收货数量:";
            // 
            // tb_qty
            // 
            // 
            // 
            // 
            this.tb_qty.Border.Class = "TextBoxBorder";
            this.tb_qty.Location = new System.Drawing.Point(99, 31);
            this.tb_qty.Name = "tb_qty";
            this.tb_qty.Size = new System.Drawing.Size(100, 21);
            this.tb_qty.TabIndex = 2;
            this.tb_qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_qty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_qty_KeyDown);
            // 
            // cb_all
            // 
            this.cb_all.AutoSize = true;
            this.cb_all.Location = new System.Drawing.Point(141, 79);
            this.cb_all.Name = "cb_all";
            this.cb_all.Size = new System.Drawing.Size(42, 16);
            this.cb_all.TabIndex = 3;
            this.cb_all.Text = "ALL";
            this.cb_all.UseVisualStyleBackColor = true;
            this.cb_all.CheckedChanged += new System.EventHandler(this.cb_all_CheckedChanged);
            // 
            // InputQty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 128);
            this.Controls.Add(this.cb_all);
            this.Controls.Add(this.tb_qty);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_ok);
            this.DoubleBuffered = true;
            this.Name = "InputQty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "输入数量";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX bt_ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cb_all;
        public DevComponents.DotNetBar.Controls.TextBoxX tb_qty;
    }
}