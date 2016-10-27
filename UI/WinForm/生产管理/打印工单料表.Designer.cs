namespace SFIS_V2
{
    partial class WoBomInfo
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
            this.dvg_wobominfo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.bt_printMpTable = new DevComponents.DotNetBar.ButtonX();
            this.bt_inputwobominfo = new DevComponents.DotNetBar.ButtonX();
            this.cb_woname = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cb_yuanyu = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtb_msglog = new System.Windows.Forms.RichTextBox();
            this.bt_entrywo = new DevComponents.DotNetBar.ButtonX();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_pcbside = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.T = new DevComponents.Editors.ComboItem();
            this.B = new DevComponents.Editors.ComboItem();
            this.cb_machineId = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.dvg_wobominfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvg_wobominfo
            // 
            this.dvg_wobominfo.AllowUserToAddRows = false;
            this.dvg_wobominfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvg_wobominfo.DefaultCellStyle = dataGridViewCellStyle1;
            this.dvg_wobominfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvg_wobominfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.dvg_wobominfo.Location = new System.Drawing.Point(0, 0);
            this.dvg_wobominfo.Name = "dvg_wobominfo";
            this.dvg_wobominfo.ReadOnly = true;
            this.dvg_wobominfo.RowTemplate.Height = 23;
            this.dvg_wobominfo.Size = new System.Drawing.Size(413, 504);
            this.dvg_wobominfo.TabIndex = 0;
            this.dvg_wobominfo.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvg_wobominfo_CellMouseEnter);
            // 
            // bt_printMpTable
            // 
            this.bt_printMpTable.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_printMpTable.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_printMpTable.Location = new System.Drawing.Point(13, 234);
            this.bt_printMpTable.Name = "bt_printMpTable";
            this.bt_printMpTable.Size = new System.Drawing.Size(145, 49);
            this.bt_printMpTable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_printMpTable.TabIndex = 0;
            this.bt_printMpTable.Text = "打印工单备料表";
            this.bt_printMpTable.Click += new System.EventHandler(this.bt_printMpTable_Click);
            // 
            // bt_inputwobominfo
            // 
            this.bt_inputwobominfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_inputwobominfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_inputwobominfo.Location = new System.Drawing.Point(15, 172);
            this.bt_inputwobominfo.Name = "bt_inputwobominfo";
            this.bt_inputwobominfo.Size = new System.Drawing.Size(142, 49);
            this.bt_inputwobominfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_inputwobominfo.TabIndex = 0;
            this.bt_inputwobominfo.Text = "导入工单领料表";
            this.bt_inputwobominfo.Click += new System.EventHandler(this.bt_inputwobominfo_Click);
            // 
            // cb_woname
            // 
            this.cb_woname.DisplayMember = "Text";
            this.cb_woname.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_woname.FormattingEnabled = true;
            this.cb_woname.ItemHeight = 15;
            this.cb_woname.Location = new System.Drawing.Point(17, 41);
            this.cb_woname.Name = "cb_woname";
            this.cb_woname.Size = new System.Drawing.Size(140, 21);
            this.cb_woname.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_woname.TabIndex = 1;
            this.cb_woname.DropDown += new System.EventHandler(this.cb_woname_DropDown);
            this.cb_woname.SelectedIndexChanged += new System.EventHandler(this.cb_woname_SelectedIndexChanged);
            this.cb_woname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_woname_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "[选择工单]:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cb_yuanyu);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.bt_entrywo);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.bt_inputwobominfo);
            this.splitContainer1.Panel1.Controls.Add(this.cb_pcbside);
            this.splitContainer1.Panel1.Controls.Add(this.cb_machineId);
            this.splitContainer1.Panel1.Controls.Add(this.cb_woname);
            this.splitContainer1.Panel1.Controls.Add(this.bt_printMpTable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dvg_wobominfo);
            this.splitContainer1.Size = new System.Drawing.Size(699, 504);
            this.splitContainer1.SplitterDistance = 282;
            this.splitContainer1.TabIndex = 3;
            // 
            // cb_yuanyu
            // 
            this.cb_yuanyu.AutoSize = true;
            this.cb_yuanyu.Location = new System.Drawing.Point(198, 44);
            this.cb_yuanyu.Name = "cb_yuanyu";
            this.cb_yuanyu.Size = new System.Drawing.Size(48, 16);
            this.cb_yuanyu.TabIndex = 5;
            this.cb_yuanyu.Text = "远御";
            this.cb_yuanyu.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtb_msglog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 300);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(282, 204);
            this.panel1.TabIndex = 4;
            // 
            // rtb_msglog
            // 
            this.rtb_msglog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_msglog.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtb_msglog.Location = new System.Drawing.Point(0, 0);
            this.rtb_msglog.Name = "rtb_msglog";
            this.rtb_msglog.Size = new System.Drawing.Size(282, 204);
            this.rtb_msglog.TabIndex = 0;
            this.rtb_msglog.Text = "";
            // 
            // bt_entrywo
            // 
            this.bt_entrywo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_entrywo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_entrywo.Location = new System.Drawing.Point(163, 44);
            this.bt_entrywo.Name = "bt_entrywo";
            this.bt_entrywo.Size = new System.Drawing.Size(19, 19);
            this.bt_entrywo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_entrywo.TabIndex = 3;
            this.bt_entrywo.Click += new System.EventHandler(this.bt_entrywo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(16, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "[PCB 面:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(16, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "[机器编号]:";
            // 
            // cb_pcbside
            // 
            this.cb_pcbside.DisplayMember = "Text";
            this.cb_pcbside.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_pcbside.FormattingEnabled = true;
            this.cb_pcbside.ItemHeight = 15;
            this.cb_pcbside.Items.AddRange(new object[] {
            this.T,
            this.B});
            this.cb_pcbside.Location = new System.Drawing.Point(18, 129);
            this.cb_pcbside.Name = "cb_pcbside";
            this.cb_pcbside.Size = new System.Drawing.Size(140, 21);
            this.cb_pcbside.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_pcbside.TabIndex = 1;
            this.cb_pcbside.DropDown += new System.EventHandler(this.cb_woname_DropDown);
            this.cb_pcbside.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_woname_KeyDown);
            // 
            // T
            // 
            this.T.Text = "T";
            // 
            // B
            // 
            this.B.Text = "B";
            // 
            // cb_machineId
            // 
            this.cb_machineId.DisplayMember = "Text";
            this.cb_machineId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_machineId.FormattingEnabled = true;
            this.cb_machineId.ItemHeight = 15;
            this.cb_machineId.Location = new System.Drawing.Point(16, 84);
            this.cb_machineId.Name = "cb_machineId";
            this.cb_machineId.Size = new System.Drawing.Size(140, 21);
            this.cb_machineId.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_machineId.TabIndex = 1;
            this.cb_machineId.DropDown += new System.EventHandler(this.cb_machineId_DropDown);
            this.cb_machineId.SelectedIndexChanged += new System.EventHandler(this.cb_machineId_SelectedIndexChanged);
            // 
            // WoBomInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 504);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "WoBomInfo";
            this.Text = "料站表打印";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WoBomInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dvg_wobominfo)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dvg_wobominfo;
        private DevComponents.DotNetBar.ButtonX bt_inputwobominfo;
        private DevComponents.DotNetBar.ButtonX bt_printMpTable;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_woname;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevComponents.DotNetBar.ButtonX bt_entrywo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_pcbside;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_machineId;
        private DevComponents.Editors.ComboItem T;
        private DevComponents.Editors.ComboItem B;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtb_msglog;
        private System.Windows.Forms.CheckBox cb_yuanyu;
    }
}