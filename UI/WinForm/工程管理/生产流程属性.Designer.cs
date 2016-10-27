namespace SFIS_V2
{
    partial class RoutegroupAtt
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
            this.tb_routegroupId = new System.Windows.Forms.TextBox();
            this.tb_routegroupname = new System.Windows.Forms.TextBox();
            this.bt_save = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[流程名称]:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "[流程编号]:";
            // 
            // tb_routegroupId
            // 
            this.tb_routegroupId.Location = new System.Drawing.Point(15, 26);
            this.tb_routegroupId.Name = "tb_routegroupId";
            this.tb_routegroupId.ReadOnly = true;
            this.tb_routegroupId.Size = new System.Drawing.Size(220, 21);
            this.tb_routegroupId.TabIndex = 1;
            // 
            // tb_routegroupname
            // 
            this.tb_routegroupname.Location = new System.Drawing.Point(15, 83);
            this.tb_routegroupname.Name = "tb_routegroupname";
            this.tb_routegroupname.Size = new System.Drawing.Size(220, 21);
            this.tb_routegroupname.TabIndex = 2;
            // 
            // bt_save
            // 
            this.bt_save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_save.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_save.Location = new System.Drawing.Point(160, 110);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(75, 23);
            this.bt_save.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_save.TabIndex = 3;
            this.bt_save.Text = "保存";
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // RoutegroupAtt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 141);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.tb_routegroupname);
            this.Controls.Add(this.tb_routegroupId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RoutegroupAtt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生产流程设置";
            this.Load += new System.EventHandler(this.RoutegroupAtt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_routegroupId;
        private System.Windows.Forms.TextBox tb_routegroupname;
        private DevComponents.DotNetBar.ButtonX bt_save;
    }
}