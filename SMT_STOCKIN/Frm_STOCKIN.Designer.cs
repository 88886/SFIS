namespace SMT_STOCKIN
{
    partial class Frm_STOCKIN
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabLine = new System.Windows.Forms.Label();
            this.imbt_commit = new DevComponents.DotNetBar.ButtonX();
            this.imbt_selectwo = new DevComponents.DotNetBar.ButtonX();
            this.LabUser = new System.Windows.Forms.Label();
            this.LabCraft = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvwiptracking = new System.Windows.Forms.DataGridView();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.memuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rePrintMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Imbt_ReUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.imbt_selectline = new System.Windows.Forms.ToolStripMenuItem();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvwiptracking)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 361);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtbmsg);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 221);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(358, 137);
            this.panel2.TabIndex = 2;
            // 
            // rtbmsg
            // 
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbmsg.Location = new System.Drawing.Point(0, 0);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.ReadOnly = true;
            this.rtbmsg.Size = new System.Drawing.Size(358, 137);
            this.rtbmsg.TabIndex = 0;
            this.rtbmsg.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LabLine);
            this.panel1.Controls.Add(this.imbt_commit);
            this.panel1.Controls.Add(this.imbt_selectwo);
            this.panel1.Controls.Add(this.LabUser);
            this.panel1.Controls.Add(this.LabCraft);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 204);
            this.panel1.TabIndex = 1;
            // 
            // LabLine
            // 
            this.LabLine.AutoSize = true;
            this.LabLine.Location = new System.Drawing.Point(75, 28);
            this.LabLine.Name = "LabLine";
            this.LabLine.Size = new System.Drawing.Size(47, 12);
            this.LabLine.TabIndex = 7;
            this.LabLine.Text = "A080101";
            // 
            // imbt_commit
            // 
            this.imbt_commit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_commit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_commit.Location = new System.Drawing.Point(258, 160);
            this.imbt_commit.Name = "imbt_commit";
            this.imbt_commit.Size = new System.Drawing.Size(75, 23);
            this.imbt_commit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_commit.TabIndex = 6;
            this.imbt_commit.Text = "提交入库";
            this.imbt_commit.Click += new System.EventHandler(this.imbt_commit_Click);
            // 
            // imbt_selectwo
            // 
            this.imbt_selectwo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_selectwo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_selectwo.Location = new System.Drawing.Point(41, 160);
            this.imbt_selectwo.Name = "imbt_selectwo";
            this.imbt_selectwo.Size = new System.Drawing.Size(75, 23);
            this.imbt_selectwo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_selectwo.TabIndex = 5;
            this.imbt_selectwo.Text = "输入工单";
            this.imbt_selectwo.Click += new System.EventHandler(this.imbt_selectwo_Click);
            // 
            // LabUser
            // 
            this.LabUser.AutoSize = true;
            this.LabUser.Location = new System.Drawing.Point(75, 101);
            this.LabUser.Name = "LabUser";
            this.LabUser.Size = new System.Drawing.Size(17, 12);
            this.LabUser.TabIndex = 3;
            this.LabUser.Text = "NA";
            // 
            // LabCraft
            // 
            this.LabCraft.AutoSize = true;
            this.LabCraft.Location = new System.Drawing.Point(75, 62);
            this.LabCraft.Name = "LabCraft";
            this.LabCraft.Size = new System.Drawing.Size(17, 12);
            this.LabCraft.TabIndex = 3;
            this.LabCraft.Text = "NA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "人员:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "途程:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "线体:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvwiptracking);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(364, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 361);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据显示";
            // 
            // dgvwiptracking
            // 
            this.dgvwiptracking.AllowUserToAddRows = false;
            this.dgvwiptracking.AllowUserToDeleteRows = false;
            this.dgvwiptracking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvwiptracking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvwiptracking.Location = new System.Drawing.Point(3, 17);
            this.dgvwiptracking.Name = "dgvwiptracking";
            this.dgvwiptracking.ReadOnly = true;
            this.dgvwiptracking.RowTemplate.Height = 23;
            this.dgvwiptracking.Size = new System.Drawing.Size(494, 341);
            this.dgvwiptracking.TabIndex = 0;
            // 
            // progressBarX1
            // 
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarX1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarX1.Location = new System.Drawing.Point(0, 386);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.Size = new System.Drawing.Size(864, 30);
            this.progressBarX1.TabIndex = 2;
            this.progressBarX1.Text = "0/0";
            this.progressBarX1.TextVisible = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(864, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // memuToolStripMenuItem
            // 
            this.memuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rePrintMenu,
            this.Imbt_ReUpload,
            this.imbt_selectline});
            this.memuToolStripMenuItem.Name = "memuToolStripMenuItem";
            this.memuToolStripMenuItem.Size = new System.Drawing.Size(57, 21);
            this.memuToolStripMenuItem.Text = "Memu";
            // 
            // rePrintMenu
            // 
            this.rePrintMenu.Name = "rePrintMenu";
            this.rePrintMenu.Size = new System.Drawing.Size(160, 22);
            this.rePrintMenu.Text = "RePrint";
            this.rePrintMenu.Click += new System.EventHandler(this.rePrintMenu_Click);
            // 
            // Imbt_ReUpload
            // 
            this.Imbt_ReUpload.Name = "Imbt_ReUpload";
            this.Imbt_ReUpload.Size = new System.Drawing.Size(160, 22);
            this.Imbt_ReUpload.Text = "提交未完成数据";
            this.Imbt_ReUpload.Click += new System.EventHandler(this.Imbt_ReUpload_Click);
            // 
            // imbt_selectline
            // 
            this.imbt_selectline.Name = "imbt_selectline";
            this.imbt_selectline.Size = new System.Drawing.Size(160, 22);
            this.imbt_selectline.Text = "选择线体";
            this.imbt_selectline.Click += new System.EventHandler(this.imbt_selectline_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // Frm_STOCKIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 416);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBarX1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Frm_STOCKIN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMT_STOCKIN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_STOCKIN_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvwiptracking)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtbmsg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label LabUser;
        private System.Windows.Forms.Label LabCraft;
        private DevComponents.DotNetBar.ButtonX imbt_selectwo;
        private System.Windows.Forms.DataGridView dgvwiptracking;
        private DevComponents.DotNetBar.ButtonX imbt_commit;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarX1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem memuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rePrintMenu;
        private System.Windows.Forms.ToolStripMenuItem Imbt_ReUpload;
        private System.Windows.Forms.ToolStripMenuItem imbt_selectline;
        private System.Windows.Forms.Label LabLine;
        private DevComponents.DotNetBar.StyleManager styleManager1;
    }
}

