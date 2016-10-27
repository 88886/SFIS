namespace SFIS_V2
{
    partial class FrmReceiveMaterials
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
            this.tb_po = new System.Windows.Forms.TextBox();
            this.dgv_materialshad = new System.Windows.Forms.DataGridView();
            this.EBELN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LIFNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ERNAM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOEKZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_RET = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.bt_save = new DevComponents.DotNetBar.ButtonX();
            this.bt_query = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv_selectmaterials = new System.Windows.Forms.DataGridView();
            this.materialno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.bt_back = new DevComponents.DotNetBar.ButtonX();
            this.bt_receive = new DevComponents.DotNetBar.ButtonX();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dgv_materialsdta = new System.Windows.Forms.DataGridView();
            this.EBELP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MATNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAKTX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MENGE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MEINS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MATKL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WERKS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LGORT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOEKZ1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RETPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lb_qty = new System.Windows.Forms.Label();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgv_received = new System.Windows.Forms.DataGridView();
            this.trsn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_receive = new System.Windows.Forms.DataGridView();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_materialshad)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_selectmaterials)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.panelEx5.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_materialsdta)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_received)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_receive)).BeginInit();
            this.panelEx4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_po
            // 
            this.tb_po.Location = new System.Drawing.Point(107, 13);
            this.tb_po.Name = "tb_po";
            this.tb_po.Size = new System.Drawing.Size(146, 21);
            this.tb_po.TabIndex = 1;
            this.tb_po.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_po.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_po_KeyDown);
            // 
            // dgv_materialshad
            // 
            this.dgv_materialshad.AllowUserToAddRows = false;
            this.dgv_materialshad.AllowUserToDeleteRows = false;
            this.dgv_materialshad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_materialshad.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EBELN,
            this.LIFNR,
            this.NAME1,
            this.ERNAM,
            this.LOEKZ,
            this.E_RET});
            this.dgv_materialshad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_materialshad.Location = new System.Drawing.Point(0, 0);
            this.dgv_materialshad.Name = "dgv_materialshad";
            this.dgv_materialshad.ReadOnly = true;
            this.dgv_materialshad.RowTemplate.Height = 23;
            this.dgv_materialshad.Size = new System.Drawing.Size(617, 69);
            this.dgv_materialshad.TabIndex = 2;
            // 
            // EBELN
            // 
            this.EBELN.DataPropertyName = "EBELN";
            this.EBELN.HeaderText = "订单号";
            this.EBELN.Name = "EBELN";
            this.EBELN.ReadOnly = true;
            // 
            // LIFNR
            // 
            this.LIFNR.DataPropertyName = "LIFNR";
            this.LIFNR.HeaderText = "供应商代码";
            this.LIFNR.Name = "LIFNR";
            this.LIFNR.ReadOnly = true;
            // 
            // NAME1
            // 
            this.NAME1.DataPropertyName = "LIFNM";
            this.NAME1.HeaderText = "供应商名称";
            this.NAME1.Name = "NAME1";
            this.NAME1.ReadOnly = true;
            // 
            // ERNAM
            // 
            this.ERNAM.HeaderText = "创建人";
            this.ERNAM.Name = "ERNAM";
            this.ERNAM.ReadOnly = true;
            // 
            // LOEKZ
            // 
            this.LOEKZ.DataPropertyName = "LOEKZ";
            this.LOEKZ.HeaderText = "删除标志";
            this.LOEKZ.Name = "LOEKZ";
            this.LOEKZ.ReadOnly = true;
            // 
            // E_RET
            // 
            this.E_RET.DataPropertyName = "E_RET";
            this.E_RET.HeaderText = "错误标志";
            this.E_RET.Name = "E_RET";
            this.E_RET.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelEx2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(854, 45);
            this.panel1.TabIndex = 3;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.bt_save);
            this.panelEx2.Controls.Add(this.bt_query);
            this.panelEx2.Controls.Add(this.label1);
            this.panelEx2.Controls.Add(this.tb_po);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(854, 45);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 5;
            // 
            // bt_save
            // 
            this.bt_save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_save.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_save.Location = new System.Drawing.Point(442, 12);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(75, 23);
            this.bt_save.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_save.TabIndex = 6;
            this.bt_save.Text = "确定";
            this.bt_save.Visible = false;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // bt_query
            // 
            this.bt_query.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_query.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_query.Location = new System.Drawing.Point(303, 12);
            this.bt_query.Name = "bt_query";
            this.bt_query.Size = new System.Drawing.Size(75, 23);
            this.bt_query.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_query.TabIndex = 5;
            this.bt_query.Text = "查询";
            this.bt_query.Click += new System.EventHandler(this.bt_query_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "[PO订单号:]";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 45);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgv_selectmaterials);
            this.splitContainer1.Panel1.Controls.Add(this.panelEx3);
            this.splitContainer1.Panel1.Controls.Add(this.panelEx5);
            this.splitContainer1.Panel1.Controls.Add(this.lb_qty);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelEx1);
            this.splitContainer1.Size = new System.Drawing.Size(854, 417);
            this.splitContainer1.SplitterDistance = 183;
            this.splitContainer1.TabIndex = 6;
            // 
            // dgv_selectmaterials
            // 
            this.dgv_selectmaterials.AllowUserToAddRows = false;
            this.dgv_selectmaterials.AllowUserToDeleteRows = false;
            this.dgv_selectmaterials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_selectmaterials.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.materialno,
            this.partnumber,
            this.qty});
            this.dgv_selectmaterials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_selectmaterials.Location = new System.Drawing.Point(662, 0);
            this.dgv_selectmaterials.Name = "dgv_selectmaterials";
            this.dgv_selectmaterials.ReadOnly = true;
            this.dgv_selectmaterials.RowTemplate.Height = 23;
            this.dgv_selectmaterials.Size = new System.Drawing.Size(192, 183);
            this.dgv_selectmaterials.TabIndex = 4;
            // 
            // materialno
            // 
            this.materialno.HeaderText = "PO订单号";
            this.materialno.Name = "materialno";
            this.materialno.ReadOnly = true;
            // 
            // partnumber
            // 
            this.partnumber.HeaderText = "料号";
            this.partnumber.Name = "partnumber";
            this.partnumber.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.HeaderText = "数量";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.bt_back);
            this.panelEx3.Controls.Add(this.bt_receive);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx3.Location = new System.Drawing.Point(617, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(45, 183);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 3;
            // 
            // bt_back
            // 
            this.bt_back.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_back.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_back.Location = new System.Drawing.Point(4, 92);
            this.bt_back.Name = "bt_back";
            this.bt_back.Size = new System.Drawing.Size(35, 23);
            this.bt_back.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_back.TabIndex = 1;
            this.bt_back.Text = "<--";
            this.bt_back.Visible = false;
            this.bt_back.Click += new System.EventHandler(this.bt_back_Click);
            // 
            // bt_receive
            // 
            this.bt_receive.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_receive.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_receive.Location = new System.Drawing.Point(3, 28);
            this.bt_receive.Name = "bt_receive";
            this.bt_receive.Size = new System.Drawing.Size(36, 23);
            this.bt_receive.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_receive.TabIndex = 0;
            this.bt_receive.Text = "-->";
            this.bt_receive.Click += new System.EventHandler(this.bt_receive_Click);
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.splitContainer3);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx5.Location = new System.Drawing.Point(0, 0);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(617, 183);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 7;
            this.panelEx5.Text = "panelEx5";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dgv_materialshad);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgv_materialsdta);
            this.splitContainer3.Size = new System.Drawing.Size(617, 183);
            this.splitContainer3.SplitterDistance = 69;
            this.splitContainer3.TabIndex = 3;
            // 
            // dgv_materialsdta
            // 
            this.dgv_materialsdta.AllowUserToAddRows = false;
            this.dgv_materialsdta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_materialsdta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EBELP,
            this.MATNR,
            this.MAKTX,
            this.MENGE,
            this.MEINS,
            this.MATKL,
            this.WERKS,
            this.LGORT,
            this.LOEKZ1,
            this.RETPO});
            this.dgv_materialsdta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_materialsdta.Location = new System.Drawing.Point(0, 0);
            this.dgv_materialsdta.Name = "dgv_materialsdta";
            this.dgv_materialsdta.ReadOnly = true;
            this.dgv_materialsdta.RowTemplate.Height = 23;
            this.dgv_materialsdta.Size = new System.Drawing.Size(617, 110);
            this.dgv_materialsdta.TabIndex = 0;
            this.dgv_materialsdta.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_materialsdta_CellMouseClick);
            // 
            // EBELP
            // 
            this.EBELP.DataPropertyName = "EBELP";
            this.EBELP.HeaderText = "行号";
            this.EBELP.Name = "EBELP";
            this.EBELP.ReadOnly = true;
            this.EBELP.Visible = false;
            // 
            // MATNR
            // 
            this.MATNR.DataPropertyName = "MATNR";
            this.MATNR.HeaderText = "物料号";
            this.MATNR.Name = "MATNR";
            this.MATNR.ReadOnly = true;
            // 
            // MAKTX
            // 
            this.MAKTX.DataPropertyName = "MAKTX";
            this.MAKTX.HeaderText = "物料描述";
            this.MAKTX.Name = "MAKTX";
            this.MAKTX.ReadOnly = true;
            // 
            // MENGE
            // 
            this.MENGE.DataPropertyName = "MENGE";
            this.MENGE.HeaderText = "数量";
            this.MENGE.Name = "MENGE";
            this.MENGE.ReadOnly = true;
            // 
            // MEINS
            // 
            this.MEINS.DataPropertyName = "MEINS";
            this.MEINS.HeaderText = "单位";
            this.MEINS.Name = "MEINS";
            this.MEINS.ReadOnly = true;
            // 
            // MATKL
            // 
            this.MATKL.DataPropertyName = "MATKL";
            this.MATKL.HeaderText = "物料组";
            this.MATKL.Name = "MATKL";
            this.MATKL.ReadOnly = true;
            // 
            // WERKS
            // 
            this.WERKS.DataPropertyName = "WERKS";
            this.WERKS.HeaderText = "工厂";
            this.WERKS.Name = "WERKS";
            this.WERKS.ReadOnly = true;
            // 
            // LGORT
            // 
            this.LGORT.DataPropertyName = "LGORT";
            this.LGORT.HeaderText = "库存地点";
            this.LGORT.Name = "LGORT";
            this.LGORT.ReadOnly = true;
            // 
            // LOEKZ1
            // 
            this.LOEKZ1.DataPropertyName = "LOEKZ";
            this.LOEKZ1.HeaderText = "删除标志";
            this.LOEKZ1.Name = "LOEKZ1";
            this.LOEKZ1.ReadOnly = true;
            // 
            // RETPO
            // 
            this.RETPO.DataPropertyName = "RETPO";
            this.RETPO.HeaderText = "退回标志";
            this.RETPO.Name = "RETPO";
            this.RETPO.ReadOnly = true;
            // 
            // lb_qty
            // 
            this.lb_qty.AutoSize = true;
            this.lb_qty.Location = new System.Drawing.Point(586, 3);
            this.lb_qty.Name = "lb_qty";
            this.lb_qty.Size = new System.Drawing.Size(41, 12);
            this.lb_qty.TabIndex = 6;
            this.lb_qty.Text = "label5";
            this.lb_qty.Visible = false;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.splitContainer2);
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(854, 230);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgv_received);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgv_receive);
            this.splitContainer2.Size = new System.Drawing.Size(854, 206);
            this.splitContainer2.SplitterDistance = 639;
            this.splitContainer2.TabIndex = 2;
            // 
            // dgv_received
            // 
            this.dgv_received.AllowUserToAddRows = false;
            this.dgv_received.AllowUserToDeleteRows = false;
            this.dgv_received.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_received.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.trsn,
            this.PO,
            this.kpnumber,
            this.kpqty,
            this.status,
            this.flag});
            this.dgv_received.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_received.Location = new System.Drawing.Point(0, 0);
            this.dgv_received.Name = "dgv_received";
            this.dgv_received.ReadOnly = true;
            this.dgv_received.RowTemplate.Height = 23;
            this.dgv_received.Size = new System.Drawing.Size(639, 206);
            this.dgv_received.TabIndex = 0;
            // 
            // trsn
            // 
            this.trsn.DataPropertyName = "trsn";
            this.trsn.HeaderText = "唯一序列号";
            this.trsn.Name = "trsn";
            this.trsn.ReadOnly = true;
            // 
            // PO
            // 
            this.PO.DataPropertyName = "PO";
            this.PO.HeaderText = "采购订单号";
            this.PO.Name = "PO";
            this.PO.ReadOnly = true;
            // 
            // kpnumber
            // 
            this.kpnumber.DataPropertyName = "kpnumber";
            this.kpnumber.HeaderText = "料号";
            this.kpnumber.Name = "kpnumber";
            this.kpnumber.ReadOnly = true;
            // 
            // kpqty
            // 
            this.kpqty.DataPropertyName = "qty";
            this.kpqty.HeaderText = "数量";
            this.kpqty.Name = "kpqty";
            this.kpqty.ReadOnly = true;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // flag
            // 
            this.flag.DataPropertyName = "flag";
            this.flag.HeaderText = "标志位";
            this.flag.Name = "flag";
            this.flag.ReadOnly = true;
            // 
            // dgv_receive
            // 
            this.dgv_receive.AllowUserToAddRows = false;
            this.dgv_receive.AllowUserToDeleteRows = false;
            this.dgv_receive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_receive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_receive.Location = new System.Drawing.Point(0, 0);
            this.dgv_receive.Name = "dgv_receive";
            this.dgv_receive.ReadOnly = true;
            this.dgv_receive.RowTemplate.Height = 23;
            this.dgv_receive.Size = new System.Drawing.Size(211, 206);
            this.dgv_receive.TabIndex = 0;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.label4);
            this.panelEx4.Controls.Add(this.label3);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx4.Location = new System.Drawing.Point(0, 0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(854, 24);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(641, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "待收物料:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "已收物料:";
            // 
            // FrmReceiveMaterials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 462);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmReceiveMaterials";
            this.Text = "材料接收";
            this.Load += new System.EventHandler(this.FrmReceiveMaterials_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_materialshad)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_selectmaterials)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_materialsdta)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_received)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_receive)).EndInit();
            this.panelEx4.ResumeLayout(false);
            this.panelEx4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_materialshad;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX bt_query;
        private System.Windows.Forms.DataGridView dgv_selectmaterials;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgv_received;
        private System.Windows.Forms.DataGridView dgv_receive;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ButtonX bt_back;
        private DevComponents.DotNetBar.ButtonX bt_receive;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lb_qty;
        private DevComponents.DotNetBar.ButtonX bt_save;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dgv_materialsdta;
        private System.Windows.Forms.DataGridViewTextBoxColumn EBELN;
        private System.Windows.Forms.DataGridViewTextBoxColumn LIFNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ERNAM;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOEKZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_RET;
        private System.Windows.Forms.DataGridViewTextBoxColumn EBELP;
        private System.Windows.Forms.DataGridViewTextBoxColumn MATNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAKTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn MENGE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MEINS;
        private System.Windows.Forms.DataGridViewTextBoxColumn MATKL;
        private System.Windows.Forms.DataGridViewTextBoxColumn WERKS;
        private System.Windows.Forms.DataGridViewTextBoxColumn LGORT;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOEKZ1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RETPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn materialno;
        private System.Windows.Forms.DataGridViewTextBoxColumn partnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn trsn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PO;
        private System.Windows.Forms.DataGridViewTextBoxColumn kpnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn kpqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn flag;
        public System.Windows.Forms.TextBox tb_po;
    }
}