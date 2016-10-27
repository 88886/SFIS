namespace SFIS_V2
{
    partial class CraftInfo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_craftid = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_craftname = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_craftparameturl = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.bt_savecraft = new DevComponents.DotNetBar.ButtonX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_showcraftinfo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showcraftinfo)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[工艺编号]:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "[工艺名称描述]:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "[参数文件]:";
            // 
            // tb_craftid
            // 
            // 
            // 
            // 
            this.tb_craftid.Border.Class = "TextBoxBorder";
            this.tb_craftid.Location = new System.Drawing.Point(15, 22);
            this.tb_craftid.Name = "tb_craftid";
            this.tb_craftid.Size = new System.Drawing.Size(131, 21);
            this.tb_craftid.TabIndex = 1;
            // 
            // tb_craftname
            // 
            // 
            // 
            // 
            this.tb_craftname.Border.Class = "TextBoxBorder";
            this.tb_craftname.Location = new System.Drawing.Point(15, 65);
            this.tb_craftname.Name = "tb_craftname";
            this.tb_craftname.Size = new System.Drawing.Size(220, 21);
            this.tb_craftname.TabIndex = 1;
            // 
            // tb_craftparameturl
            // 
            // 
            // 
            // 
            this.tb_craftparameturl.Border.Class = "TextBoxBorder";
            this.tb_craftparameturl.Location = new System.Drawing.Point(280, 63);
            this.tb_craftparameturl.Name = "tb_craftparameturl";
            this.tb_craftparameturl.Size = new System.Drawing.Size(205, 21);
            this.tb_craftparameturl.TabIndex = 1;
            // 
            // bt_savecraft
            // 
            this.bt_savecraft.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_savecraft.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_savecraft.Location = new System.Drawing.Point(512, 57);
            this.bt_savecraft.Name = "bt_savecraft";
            this.bt_savecraft.Size = new System.Drawing.Size(75, 27);
            this.bt_savecraft.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_savecraft.TabIndex = 2;
            this.bt_savecraft.Text = "保  存";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_showcraftinfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(611, 308);
            this.panel1.TabIndex = 3;
            // 
            // dgv_showcraftinfo
            // 
            this.dgv_showcraftinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_showcraftinfo.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_showcraftinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_showcraftinfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_showcraftinfo.Location = new System.Drawing.Point(0, 0);
            this.dgv_showcraftinfo.Name = "dgv_showcraftinfo";
            this.dgv_showcraftinfo.ReadOnly = true;
            this.dgv_showcraftinfo.RowTemplate.Height = 23;
            this.dgv_showcraftinfo.Size = new System.Drawing.Size(611, 308);
            this.dgv_showcraftinfo.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonX2);
            this.panel2.Controls.Add(this.tb_craftname);
            this.panel2.Controls.Add(this.tb_craftid);
            this.panel2.Controls.Add(this.bt_savecraft);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.tb_craftparameturl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 214);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(611, 94);
            this.panel2.TabIndex = 4;
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(148, 24);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(19, 18);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 3;
            // 
            // CraftInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 308);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CraftInfo";
            this.Text = "添加工艺";
            this.Load += new System.EventHandler(this.CraftInfo_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showcraftinfo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_craftid;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_craftname;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_craftparameturl;
        private DevComponents.DotNetBar.ButtonX bt_savecraft;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_showcraftinfo;
    }
}