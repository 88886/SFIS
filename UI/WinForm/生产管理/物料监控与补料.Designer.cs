namespace SFIS_V2
{
    partial class Monitor
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.选择项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemautorefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.DelMaterial = new System.Windows.Forms.ToolStripMenuItem();
            this.showrefreshtime = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.masterId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工单 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.线别 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.机台编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料料号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.料站号码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.标志位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.缺料时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.缺料刷入人员 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.补料时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.补料人员 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.未补料时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.未换料时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROWID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TR_SN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.palpartno = new DevComponents.DotNetBar.PanelEx();
            this.labpn = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cblistline = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.butRefresh = new DevComponents.DotNetBar.ButtonX();
            this.Scan_Data = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewX2 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.chkSum = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ToExcel = new DevComponents.DotNetBar.ButtonX();
            this.butQuery = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtpn = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtwo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.palpartno.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX2)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择项ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(972, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 选择项ToolStripMenuItem
            // 
            this.选择项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemautorefresh,
            this.DelMaterial,
            this.showrefreshtime});
            this.选择项ToolStripMenuItem.Name = "选择项ToolStripMenuItem";
            this.选择项ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.选择项ToolStripMenuItem.Text = "选项";
            // 
            // itemautorefresh
            // 
            this.itemautorefresh.Name = "itemautorefresh";
            this.itemautorefresh.Size = new System.Drawing.Size(148, 22);
            this.itemautorefresh.Text = "自动刷新";
            this.itemautorefresh.Click += new System.EventHandler(this.itemautorefresh_Click);
            // 
            // DelMaterial
            // 
            this.DelMaterial.Name = "DelMaterial";
            this.DelMaterial.Size = new System.Drawing.Size(148, 22);
            this.DelMaterial.Text = "删除信息";
            this.DelMaterial.Click += new System.EventHandler(this.DelMaterial_Click);
            // 
            // showrefreshtime
            // 
            this.showrefreshtime.Name = "showrefreshtime";
            this.showrefreshtime.Size = new System.Drawing.Size(148, 22);
            this.showrefreshtime.Text = "显示刷新时间";
            this.showrefreshtime.Click += new System.EventHandler(this.showrefreshtime_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.Location = new System.Drawing.Point(0, 25);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(972, 59);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            this.panelEx1.Text = "Material Monitor";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 84);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(972, 431);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewX1);
            this.tabPage1.Controls.Add(this.panelEx2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(964, 405);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "缺料监控";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.masterId,
            this.工单,
            this.线别,
            this.机台编号,
            this.物料料号,
            this.料站号码,
            this.标志位,
            this.缺料时间,
            this.缺料刷入人员,
            this.补料时间,
            this.补料人员,
            this.未补料时间,
            this.未换料时间,
            this.ROWID,
            this.TR_SN});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(3, 143);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.ReadOnly = true;
            this.dataGridViewX1.RowTemplate.Height = 23;
            this.dataGridViewX1.Size = new System.Drawing.Size(958, 259);
            this.dataGridViewX1.TabIndex = 1;
            this.dataGridViewX1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseClick);
            this.dataGridViewX1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseDoubleClick);
            this.dataGridViewX1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellMouseEnter);
            this.dataGridViewX1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewX1_RowPostPaint);
            this.dataGridViewX1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewX1_RowPrePaint);
            this.dataGridViewX1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewX1_MouseClick);
            // 
            // masterId
            // 
            this.masterId.DataPropertyName = "masterid";
            this.masterId.HeaderText = "SEQ";
            this.masterId.Name = "masterId";
            this.masterId.ReadOnly = true;
            // 
            // 工单
            // 
            this.工单.DataPropertyName = "woid";
            this.工单.HeaderText = "工单";
            this.工单.Name = "工单";
            this.工单.ReadOnly = true;
            // 
            // 线别
            // 
            this.线别.DataPropertyName = "lineid";
            this.线别.HeaderText = "线别";
            this.线别.Name = "线别";
            this.线别.ReadOnly = true;
            // 
            // 机台编号
            // 
            this.机台编号.DataPropertyName = "machineid";
            this.机台编号.HeaderText = "机台编号";
            this.机台编号.Name = "机台编号";
            this.机台编号.ReadOnly = true;
            // 
            // 物料料号
            // 
            this.物料料号.DataPropertyName = "kpnumber";
            this.物料料号.HeaderText = "物料料号";
            this.物料料号.Name = "物料料号";
            this.物料料号.ReadOnly = true;
            // 
            // 料站号码
            // 
            this.料站号码.DataPropertyName = "stationno";
            this.料站号码.HeaderText = "料站号码";
            this.料站号码.Name = "料站号码";
            this.料站号码.ReadOnly = true;
            // 
            // 标志位
            // 
            this.标志位.DataPropertyName = "cdata";
            this.标志位.HeaderText = "标志位";
            this.标志位.Name = "标志位";
            this.标志位.ReadOnly = true;
            // 
            // 缺料时间
            // 
            this.缺料时间.DataPropertyName = "scarcitytime";
            this.缺料时间.HeaderText = "缺料时间";
            this.缺料时间.Name = "缺料时间";
            this.缺料时间.ReadOnly = true;
            // 
            // 缺料刷入人员
            // 
            this.缺料刷入人员.DataPropertyName = "scarcityuser";
            this.缺料刷入人员.HeaderText = "缺料刷入人员";
            this.缺料刷入人员.Name = "缺料刷入人员";
            this.缺料刷入人员.ReadOnly = true;
            // 
            // 补料时间
            // 
            this.补料时间.DataPropertyName = "supplytime";
            this.补料时间.HeaderText = "补料时间";
            this.补料时间.Name = "补料时间";
            this.补料时间.ReadOnly = true;
            // 
            // 补料人员
            // 
            this.补料人员.DataPropertyName = "supplyuser";
            this.补料人员.HeaderText = "补料人员";
            this.补料人员.Name = "补料人员";
            this.补料人员.ReadOnly = true;
            // 
            // 未补料时间
            // 
            this.未补料时间.DataPropertyName = "nosupply";
            this.未补料时间.HeaderText = "未补料时间";
            this.未补料时间.Name = "未补料时间";
            this.未补料时间.ReadOnly = true;
            this.未补料时间.Visible = false;
            // 
            // 未换料时间
            // 
            this.未换料时间.DataPropertyName = "nochgkp";
            this.未换料时间.HeaderText = "未换料时间";
            this.未换料时间.Name = "未换料时间";
            this.未换料时间.ReadOnly = true;
            this.未换料时间.Visible = false;
            // 
            // ROWID
            // 
            this.ROWID.DataPropertyName = "kpmonitorid";
            this.ROWID.HeaderText = "ROWID";
            this.ROWID.Name = "ROWID";
            this.ROWID.ReadOnly = true;
            this.ROWID.Visible = false;
            // 
            // TR_SN
            // 
            this.TR_SN.DataPropertyName = "TRSN";
            this.TR_SN.HeaderText = "TR_SN";
            this.TR_SN.Name = "TR_SN";
            this.TR_SN.ReadOnly = true;
            this.TR_SN.Visible = false;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.groupBox1);
            this.panelEx2.Controls.Add(this.panel1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(3, 3);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(958, 140);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbmsg);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(851, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(107, 140);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "提示信息";
            // 
            // rtbmsg
            // 
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbmsg.Location = new System.Drawing.Point(3, 17);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.ReadOnly = true;
            this.rtbmsg.Size = new System.Drawing.Size(101, 120);
            this.rtbmsg.TabIndex = 0;
            this.rtbmsg.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.palpartno);
            this.panel1.Controls.Add(this.labelX3);
            this.panel1.Controls.Add(this.labelX2);
            this.panel1.Controls.Add(this.labelX1);
            this.panel1.Controls.Add(this.cblistline);
            this.panel1.Controls.Add(this.butRefresh);
            this.panel1.Controls.Add(this.Scan_Data);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 140);
            this.panel1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(318, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "上次刷新时间:xxxxxxxxxx";
            this.label1.Visible = false;
            // 
            // palpartno
            // 
            this.palpartno.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.palpartno.CanvasColor = System.Drawing.SystemColors.Control;
            this.palpartno.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.palpartno.Controls.Add(this.labpn);
            this.palpartno.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.palpartno.Location = new System.Drawing.Point(90, 98);
            this.palpartno.Name = "palpartno";
            this.palpartno.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.palpartno.ShowFocusRectangle = true;
            this.palpartno.Size = new System.Drawing.Size(734, 23);
            this.palpartno.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.palpartno.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.palpartno.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.palpartno.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.palpartno.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.palpartno.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.palpartno.Style.GradientAngle = 90;
            this.palpartno.StyleMouseOver.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor;
            this.palpartno.TabIndex = 10;
            // 
            // labpn
            // 
            // 
            // 
            // 
            this.labpn.BackgroundStyle.Class = "";
            this.labpn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labpn.Location = new System.Drawing.Point(0, 0);
            this.labpn.Name = "labpn";
            this.labpn.Size = new System.Drawing.Size(734, 23);
            this.labpn.TabIndex = 11;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Location = new System.Drawing.Point(28, 104);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(38, 23);
            this.labelX3.TabIndex = 9;
            this.labelX3.Text = "物料:";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Location = new System.Drawing.Point(21, 13);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(53, 23);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "补料:";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(10, 62);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(64, 21);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "过滤条件:";
            // 
            // cblistline
            // 
            this.cblistline.DisplayMember = "Text";
            this.cblistline.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cblistline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cblistline.FormattingEnabled = true;
            this.cblistline.ItemHeight = 15;
            this.cblistline.Location = new System.Drawing.Point(92, 60);
            this.cblistline.Name = "cblistline";
            this.cblistline.Size = new System.Drawing.Size(199, 21);
            this.cblistline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cblistline.TabIndex = 2;
            this.cblistline.SelectionChangeCommitted += new System.EventHandler(this.cblistline_SelectionChangeCommitted);
            // 
            // butRefresh
            // 
            this.butRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butRefresh.BackColor = System.Drawing.Color.Transparent;
            this.butRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butRefresh.Location = new System.Drawing.Point(554, 11);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(75, 23);
            this.butRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butRefresh.TabIndex = 1;
            this.butRefresh.Text = "刷新";
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // Scan_Data
            // 
            // 
            // 
            // 
            this.Scan_Data.Border.Class = "TextBoxBorder";
            this.Scan_Data.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Scan_Data.Location = new System.Drawing.Point(90, 11);
            this.Scan_Data.Name = "Scan_Data";
            this.Scan_Data.Size = new System.Drawing.Size(384, 21);
            this.Scan_Data.TabIndex = 0;
            this.Scan_Data.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Scan_Data_KeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewX2);
            this.tabPage2.Controls.Add(this.panelEx3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(964, 405);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "发料清单";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewX2
            // 
            this.dataGridViewX2.AllowUserToAddRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewX2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewX2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewX2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX2.Location = new System.Drawing.Point(3, 75);
            this.dataGridViewX2.Name = "dataGridViewX2";
            this.dataGridViewX2.ReadOnly = true;
            this.dataGridViewX2.RowTemplate.Height = 23;
            this.dataGridViewX2.Size = new System.Drawing.Size(958, 327);
            this.dataGridViewX2.TabIndex = 1;
            this.dataGridViewX2.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellMouseEnter);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.chkSum);
            this.panelEx3.Controls.Add(this.ToExcel);
            this.panelEx3.Controls.Add(this.butQuery);
            this.panelEx3.Controls.Add(this.labelX5);
            this.panelEx3.Controls.Add(this.txtpn);
            this.panelEx3.Controls.Add(this.txtwo);
            this.panelEx3.Controls.Add(this.labelX4);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(3, 3);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(958, 72);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 0;
            // 
            // chkSum
            // 
            // 
            // 
            // 
            this.chkSum.BackgroundStyle.Class = "";
            this.chkSum.Location = new System.Drawing.Point(531, 27);
            this.chkSum.Name = "chkSum";
            this.chkSum.Size = new System.Drawing.Size(57, 22);
            this.chkSum.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkSum.TabIndex = 6;
            this.chkSum.Text = "加总";
            // 
            // ToExcel
            // 
            this.ToExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ToExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ToExcel.Location = new System.Drawing.Point(646, 41);
            this.ToExcel.Name = "ToExcel";
            this.ToExcel.Size = new System.Drawing.Size(75, 23);
            this.ToExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ToExcel.TabIndex = 5;
            this.ToExcel.Text = "汇出Excel";
            this.ToExcel.Click += new System.EventHandler(this.ToExcel_Click);
            // 
            // butQuery
            // 
            this.butQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butQuery.Location = new System.Drawing.Point(646, 10);
            this.butQuery.Name = "butQuery";
            this.butQuery.Size = new System.Drawing.Size(75, 23);
            this.butQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butQuery.TabIndex = 4;
            this.butQuery.Text = "查询";
            this.butQuery.Click += new System.EventHandler(this.butQuery_Click);
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.Location = new System.Drawing.Point(257, 26);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(41, 23);
            this.labelX5.TabIndex = 3;
            this.labelX5.Text = "料号:";
            // 
            // txtpn
            // 
            // 
            // 
            // 
            this.txtpn.Border.Class = "TextBoxBorder";
            this.txtpn.Location = new System.Drawing.Point(313, 26);
            this.txtpn.Name = "txtpn";
            this.txtpn.Size = new System.Drawing.Size(186, 21);
            this.txtpn.TabIndex = 2;
            // 
            // txtwo
            // 
            // 
            // 
            // 
            this.txtwo.Border.Class = "TextBoxBorder";
            this.txtwo.Location = new System.Drawing.Point(59, 26);
            this.txtwo.Name = "txtwo";
            this.txtwo.Size = new System.Drawing.Size(184, 21);
            this.txtwo.TabIndex = 1;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.Location = new System.Drawing.Point(15, 26);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(38, 23);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "工单:";
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 515);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Monitor";
            this.Text = "物料监控与补料";
            this.Load += new System.EventHandler(this.Monitor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.palpartno.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX2)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX butRefresh;
        private DevComponents.DotNetBar.Controls.TextBoxX Scan_Data;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 选择项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemautorefresh;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cblistline;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.PanelEx palpartno;
        private DevComponents.DotNetBar.LabelX labpn;
        private System.Windows.Forms.ToolStripMenuItem DelMaterial;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ButtonX butQuery;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtpn;
        private DevComponents.DotNetBar.Controls.TextBoxX txtwo;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX ToExcel;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem showrefreshtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn masterId;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工单;
        private System.Windows.Forms.DataGridViewTextBoxColumn 线别;
        private System.Windows.Forms.DataGridViewTextBoxColumn 机台编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料料号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 料站号码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 标志位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 缺料时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 缺料刷入人员;
        private System.Windows.Forms.DataGridViewTextBoxColumn 补料时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 补料人员;
        private System.Windows.Forms.DataGridViewTextBoxColumn 未补料时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 未换料时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn ROWID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TR_SN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbmsg;
        private System.Windows.Forms.Panel panel1;
    }
}