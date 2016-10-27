namespace Frm_iSCM
{
    partial class Frm_iSCM
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LabLoadMsg = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.imbt_select = new DevComponents.DotNetBar.ButtonX();
            this.cmb_prglist = new System.Windows.Forms.ComboBox();
            this.PanelFrm = new System.Windows.Forms.Panel();
            this.GroupBox_Frm = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.GroupBox_Frm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 534);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::Frm_iSCM.Properties.Resources.ISC;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(172, 421);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LabLoadMsg);
            this.panel2.Controls.Add(this.labelX1);
            this.panel2.Controls.Add(this.imbt_select);
            this.panel2.Controls.Add(this.cmb_prglist);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 421);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(172, 113);
            this.panel2.TabIndex = 0;
            // 
            // LabLoadMsg
            // 
            // 
            // 
            // 
            this.LabLoadMsg.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabLoadMsg.Location = new System.Drawing.Point(3, 87);
            this.LabLoadMsg.Name = "LabLoadMsg";
            this.LabLoadMsg.Size = new System.Drawing.Size(146, 23);
            this.LabLoadMsg.TabIndex = 3;
            this.LabLoadMsg.Text = "labelX2";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "选择程序";
            // 
            // imbt_select
            // 
            this.imbt_select.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_select.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_select.Location = new System.Drawing.Point(74, 59);
            this.imbt_select.Name = "imbt_select";
            this.imbt_select.Size = new System.Drawing.Size(75, 23);
            this.imbt_select.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_select.TabIndex = 1;
            this.imbt_select.Text = "确定";
            this.imbt_select.Click += new System.EventHandler(this.imbt_select_Click);
            // 
            // cmb_prglist
            // 
            this.cmb_prglist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_prglist.FormattingEnabled = true;
            this.cmb_prglist.Location = new System.Drawing.Point(12, 27);
            this.cmb_prglist.Name = "cmb_prglist";
            this.cmb_prglist.Size = new System.Drawing.Size(154, 20);
            this.cmb_prglist.TabIndex = 0;
            // 
            // PanelFrm
            // 
            this.PanelFrm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelFrm.Location = new System.Drawing.Point(3, 17);
            this.PanelFrm.Name = "PanelFrm";
            this.PanelFrm.Size = new System.Drawing.Size(349, 514);
            this.PanelFrm.TabIndex = 1;
            // 
            // GroupBox_Frm
            // 
            this.GroupBox_Frm.Controls.Add(this.PanelFrm);
            this.GroupBox_Frm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox_Frm.Location = new System.Drawing.Point(172, 0);
            this.GroupBox_Frm.Name = "GroupBox_Frm";
            this.GroupBox_Frm.Size = new System.Drawing.Size(355, 534);
            this.GroupBox_Frm.TabIndex = 2;
            this.GroupBox_Frm.TabStop = false;
            this.GroupBox_Frm.Text = "groupBox1";
            // 
            // Frm_iSCM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 534);
            this.Controls.Add(this.GroupBox_Frm);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_iSCM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iSCM管理系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_iSCM_FormClosing);
            this.Load += new System.EventHandler(this.Frm_iSCM_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.GroupBox_Frm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PanelFrm;
        private System.Windows.Forms.GroupBox GroupBox_Frm;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX imbt_select;
        private System.Windows.Forms.ComboBox cmb_prglist;
        private DevComponents.DotNetBar.LabelX LabLoadMsg;
    }
}

