namespace SFIS_V2
{
    partial class FrmQuerySix
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvWipTracking = new System.Windows.Forms.DataGridView();
            this.dgvWipKeyparts = new System.Windows.Forms.DataGridView();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.chkhistory = new System.Windows.Forms.CheckBox();
            this.btOutPutWoSn = new DevComponents.DotNetBar.ButtonX();
            this.Bt_excel = new DevComponents.DotNetBar.ButtonX();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDataSelect = new System.Windows.Forms.TextBox();
            this.btQuery = new DevComponents.DotNetBar.ButtonX();
            this.CbSelect = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btsnlist = new DevComponents.DotNetBar.ButtonX();
            this.btSnRange = new DevComponents.DotNetBar.ButtonX();
            this.btMAC = new DevComponents.DotNetBar.ButtonX();
            this.btPartsn = new DevComponents.DotNetBar.ButtonX();
            this.btpallet = new DevComponents.DotNetBar.ButtonX();
            this.bttray = new DevComponents.DotNetBar.ButtonX();
            this.btcarton = new DevComponents.DotNetBar.ButtonX();
            this.btWo = new DevComponents.DotNetBar.ButtonX();
            this.btSn = new DevComponents.DotNetBar.ButtonX();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgv_Trsn_Info = new System.Windows.Forms.DataGridView();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_tr_sn = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWipTracking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWipKeyparts)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Trsn_Info)).BeginInit();
            this.panelEx4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1232, 492);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Controls.Add(this.panelEx2);
            this.tabPage1.Controls.Add(this.panelEx1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1224, 466);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "产品信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 154);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvWipTracking);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvWipKeyparts);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1218, 309);
            this.splitContainer1.SplitterDistance = 113;
            this.splitContainer1.TabIndex = 10;
            // 
            // dgvWipTracking
            // 
            this.dgvWipTracking.AllowUserToAddRows = false;
            this.dgvWipTracking.AllowUserToDeleteRows = false;
            this.dgvWipTracking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWipTracking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWipTracking.Location = new System.Drawing.Point(0, 0);
            this.dgvWipTracking.Name = "dgvWipTracking";
            this.dgvWipTracking.ReadOnly = true;
            this.dgvWipTracking.RowTemplate.Height = 23;
            this.dgvWipTracking.Size = new System.Drawing.Size(1218, 113);
            this.dgvWipTracking.TabIndex = 8;
            this.dgvWipTracking.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWipTracking_CellDoubleClick);
            this.dgvWipTracking.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWipTracking_CellMouseEnter);
            this.dgvWipTracking.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvWipTracking_DataBindingComplete);
            this.dgvWipTracking.Click += new System.EventHandler(this.dgvWipTracking_Click);
            // 
            // dgvWipKeyparts
            // 
            this.dgvWipKeyparts.AllowUserToAddRows = false;
            this.dgvWipKeyparts.AllowUserToDeleteRows = false;
            this.dgvWipKeyparts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWipKeyparts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWipKeyparts.Location = new System.Drawing.Point(0, 0);
            this.dgvWipKeyparts.Name = "dgvWipKeyparts";
            this.dgvWipKeyparts.ReadOnly = true;
            this.dgvWipKeyparts.RowTemplate.Height = 23;
            this.dgvWipKeyparts.Size = new System.Drawing.Size(1218, 192);
            this.dgvWipKeyparts.TabIndex = 10;
            this.dgvWipKeyparts.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvWipKeyparts_DataBindingComplete);
            this.dgvWipKeyparts.Click += new System.EventHandler(this.dgvWipKeyparts_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.chkhistory);
            this.panelEx2.Controls.Add(this.btOutPutWoSn);
            this.panelEx2.Controls.Add(this.Bt_excel);
            this.panelEx2.Controls.Add(this.label2);
            this.panelEx2.Controls.Add(this.tbDataSelect);
            this.panelEx2.Controls.Add(this.btQuery);
            this.panelEx2.Controls.Add(this.CbSelect);
            this.panelEx2.Controls.Add(this.label1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(3, 68);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1218, 86);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 6;
            // 
            // chkhistory
            // 
            this.chkhistory.AutoSize = true;
            this.chkhistory.Location = new System.Drawing.Point(12, 6);
            this.chkhistory.Name = "chkhistory";
            this.chkhistory.Size = new System.Drawing.Size(72, 16);
            this.chkhistory.TabIndex = 7;
            this.chkhistory.Text = "历史信息";
            this.chkhistory.UseVisualStyleBackColor = true;
            // 
            // btOutPutWoSn
            // 
            this.btOutPutWoSn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOutPutWoSn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOutPutWoSn.Location = new System.Drawing.Point(1023, 18);
            this.btOutPutWoSn.Name = "btOutPutWoSn";
            this.btOutPutWoSn.Size = new System.Drawing.Size(117, 47);
            this.btOutPutWoSn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOutPutWoSn.TabIndex = 6;
            this.btOutPutWoSn.Text = "导出工单序列号";
            this.btOutPutWoSn.Click += new System.EventHandler(this.btOutPutWoSn_Click);
            // 
            // Bt_excel
            // 
            this.Bt_excel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Bt_excel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Bt_excel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Bt_excel.Location = new System.Drawing.Point(907, 18);
            this.Bt_excel.Name = "Bt_excel";
            this.Bt_excel.Size = new System.Drawing.Size(75, 47);
            this.Bt_excel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Bt_excel.TabIndex = 5;
            this.Bt_excel.Text = "汇出EXCEL";
            this.Bt_excel.Tooltip = "点击显示框汇出";
            this.Bt_excel.Click += new System.EventHandler(this.Bt_excel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(330, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "输入资料:";
            // 
            // tbDataSelect
            // 
            this.tbDataSelect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbDataSelect.Location = new System.Drawing.Point(425, 27);
            this.tbDataSelect.Name = "tbDataSelect";
            this.tbDataSelect.Size = new System.Drawing.Size(277, 26);
            this.tbDataSelect.TabIndex = 3;
            this.tbDataSelect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbDataSelect_KeyDown);
            // 
            // btQuery
            // 
            this.btQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btQuery.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btQuery.Location = new System.Drawing.Point(809, 18);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(75, 47);
            this.btQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btQuery.TabIndex = 2;
            this.btQuery.Text = "查询";
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // CbSelect
            // 
            this.CbSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbSelect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CbSelect.FormattingEnabled = true;
            this.CbSelect.Items.AddRange(new object[] {
            "唯一条码",
            "工单",
            "产品箱号",
            "Tray盘号",
            "栈板号",
            "SN",
            "MAC",
            "入库编号",
            "KeyParts"});
            this.CbSelect.Location = new System.Drawing.Point(103, 29);
            this.CbSelect.Name = "CbSelect";
            this.CbSelect.Size = new System.Drawing.Size(209, 24);
            this.CbSelect.TabIndex = 1;
            this.CbSelect.SelectedIndexChanged += new System.EventHandler(this.CbSelect_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "查询条件:";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btsnlist);
            this.panelEx1.Controls.Add(this.btSnRange);
            this.panelEx1.Controls.Add(this.btMAC);
            this.panelEx1.Controls.Add(this.btPartsn);
            this.panelEx1.Controls.Add(this.btpallet);
            this.panelEx1.Controls.Add(this.bttray);
            this.panelEx1.Controls.Add(this.btcarton);
            this.panelEx1.Controls.Add(this.btWo);
            this.panelEx1.Controls.Add(this.btSn);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(3, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1218, 65);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 5;
            // 
            // btsnlist
            // 
            this.btsnlist.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btsnlist.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btsnlist.Dock = System.Windows.Forms.DockStyle.Left;
            this.btsnlist.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btsnlist.Location = new System.Drawing.Point(624, 0);
            this.btsnlist.Name = "btsnlist";
            this.btsnlist.Size = new System.Drawing.Size(78, 65);
            this.btsnlist.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btsnlist.TabIndex = 10;
            this.btsnlist.Text = "详细条码";
            this.btsnlist.Click += new System.EventHandler(this.btsnlist_Click);
            // 
            // btSnRange
            // 
            this.btSnRange.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btSnRange.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btSnRange.Dock = System.Windows.Forms.DockStyle.Left;
            this.btSnRange.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSnRange.Location = new System.Drawing.Point(546, 0);
            this.btSnRange.Name = "btSnRange";
            this.btSnRange.Size = new System.Drawing.Size(78, 65);
            this.btSnRange.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btSnRange.TabIndex = 8;
            this.btSnRange.Text = "区间";
            this.btSnRange.Click += new System.EventHandler(this.btSnRange_Click);
            // 
            // btMAC
            // 
            this.btMAC.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btMAC.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btMAC.Dock = System.Windows.Forms.DockStyle.Left;
            this.btMAC.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btMAC.Location = new System.Drawing.Point(468, 0);
            this.btMAC.Name = "btMAC";
            this.btMAC.Size = new System.Drawing.Size(78, 65);
            this.btMAC.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btMAC.TabIndex = 7;
            this.btMAC.Text = "MAC";
            this.btMAC.Click += new System.EventHandler(this.btMAC_Click);
            // 
            // btPartsn
            // 
            this.btPartsn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btPartsn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btPartsn.Dock = System.Windows.Forms.DockStyle.Left;
            this.btPartsn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btPartsn.Location = new System.Drawing.Point(390, 0);
            this.btPartsn.Name = "btPartsn";
            this.btPartsn.Size = new System.Drawing.Size(78, 65);
            this.btPartsn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btPartsn.TabIndex = 6;
            this.btPartsn.Text = "SN";
            this.btPartsn.Click += new System.EventHandler(this.btPartsn_Click);
            // 
            // btpallet
            // 
            this.btpallet.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btpallet.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btpallet.Dock = System.Windows.Forms.DockStyle.Left;
            this.btpallet.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btpallet.Location = new System.Drawing.Point(312, 0);
            this.btpallet.Name = "btpallet";
            this.btpallet.Size = new System.Drawing.Size(78, 65);
            this.btpallet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btpallet.TabIndex = 5;
            this.btpallet.Text = "栈板号";
            this.btpallet.Click += new System.EventHandler(this.btpallet_Click);
            // 
            // bttray
            // 
            this.bttray.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bttray.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bttray.Dock = System.Windows.Forms.DockStyle.Left;
            this.bttray.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bttray.Location = new System.Drawing.Point(234, 0);
            this.bttray.Name = "bttray";
            this.bttray.Size = new System.Drawing.Size(78, 65);
            this.bttray.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bttray.TabIndex = 4;
            this.bttray.Text = "Tray盘号";
            this.bttray.Click += new System.EventHandler(this.bttray_Click);
            // 
            // btcarton
            // 
            this.btcarton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btcarton.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btcarton.Dock = System.Windows.Forms.DockStyle.Left;
            this.btcarton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btcarton.Location = new System.Drawing.Point(156, 0);
            this.btcarton.Name = "btcarton";
            this.btcarton.Size = new System.Drawing.Size(78, 65);
            this.btcarton.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btcarton.TabIndex = 3;
            this.btcarton.Text = "产品箱号";
            this.btcarton.Click += new System.EventHandler(this.btcarton_Click);
            // 
            // btWo
            // 
            this.btWo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btWo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btWo.Dock = System.Windows.Forms.DockStyle.Left;
            this.btWo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btWo.Location = new System.Drawing.Point(78, 0);
            this.btWo.Name = "btWo";
            this.btWo.Size = new System.Drawing.Size(78, 65);
            this.btWo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btWo.TabIndex = 2;
            this.btWo.Text = "工单";
            this.btWo.Click += new System.EventHandler(this.btWo_Click);
            // 
            // btSn
            // 
            this.btSn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btSn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btSn.Dock = System.Windows.Forms.DockStyle.Left;
            this.btSn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSn.Location = new System.Drawing.Point(0, 0);
            this.btSn.Name = "btSn";
            this.btSn.Size = new System.Drawing.Size(78, 65);
            this.btSn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btSn.TabIndex = 1;
            this.btSn.Text = "唯一条码";
            this.btSn.Click += new System.EventHandler(this.btSn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelEx3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1224, 466);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "物料信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.dgv_Trsn_Info);
            this.panelEx3.Controls.Add(this.panelEx4);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(3, 3);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1218, 460);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 0;
            this.panelEx3.Text = "panelEx3";
            // 
            // dgv_Trsn_Info
            // 
            this.dgv_Trsn_Info.AllowUserToAddRows = false;
            this.dgv_Trsn_Info.AllowUserToDeleteRows = false;
            this.dgv_Trsn_Info.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Trsn_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Trsn_Info.Location = new System.Drawing.Point(0, 105);
            this.dgv_Trsn_Info.Name = "dgv_Trsn_Info";
            this.dgv_Trsn_Info.ReadOnly = true;
            this.dgv_Trsn_Info.RowTemplate.Height = 23;
            this.dgv_Trsn_Info.Size = new System.Drawing.Size(1218, 355);
            this.dgv_Trsn_Info.TabIndex = 1;
            this.dgv_Trsn_Info.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_Trsn_Info_CellMouseClick);
            this.dgv_Trsn_Info.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_Trsn_Info_CellMouseDoubleClick);
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.label3);
            this.panelEx4.Controls.Add(this.tb_tr_sn);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx4.Location = new System.Drawing.Point(0, 0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1218, 105);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "唯一条码:";
            // 
            // tb_tr_sn
            // 
            this.tb_tr_sn.Location = new System.Drawing.Point(99, 39);
            this.tb_tr_sn.Name = "tb_tr_sn";
            this.tb_tr_sn.Size = new System.Drawing.Size(240, 21);
            this.tb_tr_sn.TabIndex = 0;
            this.tb_tr_sn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_tr_sn_KeyDown);
            // 
            // FrmQuerySix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 492);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Name = "FrmQuerySix";
            this.Text = "生产记录查询";
            this.Load += new System.EventHandler(this.FrmQuerySix_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWipTracking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWipKeyparts)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Trsn_Info)).EndInit();
            this.panelEx4.ResumeLayout(false);
            this.panelEx4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.CheckBox chkhistory;
        private DevComponents.DotNetBar.ButtonX btOutPutWoSn;
        private DevComponents.DotNetBar.ButtonX Bt_excel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDataSelect;
        private DevComponents.DotNetBar.ButtonX btQuery;
        private System.Windows.Forms.ComboBox CbSelect;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btsnlist;
        private DevComponents.DotNetBar.ButtonX btSnRange;
        private DevComponents.DotNetBar.ButtonX btMAC;
        private DevComponents.DotNetBar.ButtonX btPartsn;
        private DevComponents.DotNetBar.ButtonX btpallet;
        private DevComponents.DotNetBar.ButtonX bttray;
        private DevComponents.DotNetBar.ButtonX btcarton;
        private DevComponents.DotNetBar.ButtonX btWo;
        private DevComponents.DotNetBar.ButtonX btSn;
        private System.Windows.Forms.TabPage tabPage2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.DataGridView dgv_Trsn_Info;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_tr_sn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvWipTracking;
        private System.Windows.Forms.DataGridView dgvWipKeyparts;

    }
}