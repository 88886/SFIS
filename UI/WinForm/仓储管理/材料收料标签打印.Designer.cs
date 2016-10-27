namespace SFIS_V2
{
    partial class FrmMaterialsPrint
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.print_qty = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.edt_trsn = new System.Windows.Forms.TextBox();
            this.edt_Scan_Data = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkreprint = new System.Windows.Forms.CheckBox();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.lb_trsn = new System.Windows.Forms.Label();
            this.labshowdesc = new System.Windows.Forms.Label();
            this.butprint = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labprinqty = new DevComponents.DotNetBar.LabelX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chk_notinputstorehouse = new System.Windows.Forms.CheckBox();
            this.chkdeparture = new System.Windows.Forms.CheckBox();
            this.edt_pn = new System.Windows.Forms.ComboBox();
            this.edt_vc = new System.Windows.Forms.ComboBox();
            this.tb_vendernumber = new System.Windows.Forms.TextBox();
            this.edt_qty = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btlocid = new DevComponents.DotNetBar.ButtonX();
            this.cb_store = new System.Windows.Forms.ComboBox();
            this.cblocal = new System.Windows.Forms.ComboBox();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.getdc = new System.Windows.Forms.CheckBox();
            this.lb_kpdesc = new System.Windows.Forms.Label();
            this.labshowvc = new System.Windows.Forms.Label();
            this.edt_lot = new System.Windows.Forms.TextBox();
            this.edt_dc = new System.Windows.Forms.TextBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.labqty = new DevComponents.DotNetBar.LabelX();
            this.lablot = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labpn = new DevComponents.DotNetBar.LabelX();
            this.labvc = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labdc = new DevComponents.DotNetBar.LabelX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelfilepatch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lb_restqty = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Font = new System.Drawing.Font("华文行楷", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(893, 82);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "材料入库+打印五合一条码";
            this.panelEx1.Click += new System.EventHandler(this.panelEx1_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panel4);
            this.panelEx2.Controls.Add(this.panel3);
            this.panelEx2.Controls.Add(this.panel2);
            this.panelEx2.Controls.Add(this.panel1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 82);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(893, 474);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rtb_log);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(726, 253);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(167, 221);
            this.panel4.TabIndex = 41;
            // 
            // rtb_log
            // 
            this.rtb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_log.Location = new System.Drawing.Point(0, 0);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.Size = new System.Drawing.Size(167, 221);
            this.rtb_log.TabIndex = 0;
            this.rtb_log.Text = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lb_restqty);
            this.panel3.Controls.Add(this.print_qty);
            this.panel3.Controls.Add(this.edt_trsn);
            this.panel3.Controls.Add(this.edt_Scan_Data);
            this.panel3.Controls.Add(this.chkreprint);
            this.panel3.Controls.Add(this.labelX6);
            this.panel3.Controls.Add(this.lb_trsn);
            this.panel3.Controls.Add(this.labshowdesc);
            this.panel3.Controls.Add(this.butprint);
            this.panel3.Controls.Add(this.labelX2);
            this.panel3.Controls.Add(this.labprinqty);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 253);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(726, 221);
            this.panel3.TabIndex = 40;
            // 
            // print_qty
            // 
            // 
            // 
            // 
            this.print_qty.Border.Class = "TextBoxBorder";
            this.print_qty.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.print_qty.ForeColor = System.Drawing.Color.Green;
            this.print_qty.Location = new System.Drawing.Point(613, 29);
            this.print_qty.MaxLength = 2;
            this.print_qty.Name = "print_qty";
            this.print_qty.Size = new System.Drawing.Size(75, 26);
            this.print_qty.TabIndex = 19;
            this.print_qty.Text = "1";
            this.print_qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.print_qty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.print_qty_KeyPress);
            // 
            // edt_trsn
            // 
            this.edt_trsn.Enabled = false;
            this.edt_trsn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edt_trsn.Location = new System.Drawing.Point(100, 186);
            this.edt_trsn.Name = "edt_trsn";
            this.edt_trsn.Size = new System.Drawing.Size(592, 26);
            this.edt_trsn.TabIndex = 34;
            this.edt_trsn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.edt_trsn_KeyDown);
            // 
            // edt_Scan_Data
            // 
            // 
            // 
            // 
            this.edt_Scan_Data.Border.Class = "TextBoxBorder";
            this.edt_Scan_Data.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.edt_Scan_Data.Enabled = false;
            this.edt_Scan_Data.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edt_Scan_Data.Location = new System.Drawing.Point(99, 129);
            this.edt_Scan_Data.Name = "edt_Scan_Data";
            this.edt_Scan_Data.Size = new System.Drawing.Size(592, 26);
            this.edt_Scan_Data.TabIndex = 16;
            this.edt_Scan_Data.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edt_Scan_Data_KeyPress);
            // 
            // chkreprint
            // 
            this.chkreprint.AutoSize = true;
            this.chkreprint.Enabled = false;
            this.chkreprint.Location = new System.Drawing.Point(572, 164);
            this.chkreprint.Name = "chkreprint";
            this.chkreprint.Size = new System.Drawing.Size(120, 16);
            this.chkreprint.TabIndex = 35;
            this.chkreprint.Text = "重复列印唯一条码";
            this.chkreprint.UseVisualStyleBackColor = true;
            this.chkreprint.Click += new System.EventHandler(this.chkreprint_Click);
            this.chkreprint.CheckedChanged += new System.EventHandler(this.chkreprint_CheckedChanged);
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.Location = new System.Drawing.Point(22, 188);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(82, 23);
            this.labelX6.TabIndex = 33;
            this.labelX6.Text = "[唯一条码:]";
            // 
            // lb_trsn
            // 
            this.lb_trsn.AutoSize = true;
            this.lb_trsn.Location = new System.Drawing.Point(27, 49);
            this.lb_trsn.Name = "lb_trsn";
            this.lb_trsn.Size = new System.Drawing.Size(71, 12);
            this.lb_trsn.TabIndex = 30;
            this.lb_trsn.Text = "[唯一编号:]";
            // 
            // labshowdesc
            // 
            this.labshowdesc.AutoSize = true;
            this.labshowdesc.Location = new System.Drawing.Point(39, 10);
            this.labshowdesc.Name = "labshowdesc";
            this.labshowdesc.Size = new System.Drawing.Size(59, 12);
            this.labshowdesc.TabIndex = 27;
            this.labshowdesc.Text = "[厂商名:]";
            // 
            // butprint
            // 
            this.butprint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butprint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butprint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butprint.Location = new System.Drawing.Point(555, 74);
            this.butprint.Name = "butprint";
            this.butprint.Size = new System.Drawing.Size(133, 35);
            this.butprint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butprint.TabIndex = 18;
            this.butprint.Text = "列 印";
            this.butprint.Click += new System.EventHandler(this.butprint_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Location = new System.Drawing.Point(12, 132);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(93, 18);
            this.labelX2.TabIndex = 17;
            this.labelX2.Text = "[五合一条码:]";
            // 
            // labprinqty
            // 
            // 
            // 
            // 
            this.labprinqty.BackgroundStyle.Class = "";
            this.labprinqty.Location = new System.Drawing.Point(541, 32);
            this.labprinqty.Name = "labprinqty";
            this.labprinqty.Size = new System.Drawing.Size(75, 23);
            this.labprinqty.TabIndex = 14;
            this.labprinqty.Text = "[打印数量:]";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chk_notinputstorehouse);
            this.panel2.Controls.Add(this.chkdeparture);
            this.panel2.Controls.Add(this.edt_pn);
            this.panel2.Controls.Add(this.edt_vc);
            this.panel2.Controls.Add(this.tb_vendernumber);
            this.panel2.Controls.Add(this.edt_qty);
            this.panel2.Controls.Add(this.btlocid);
            this.panel2.Controls.Add(this.cb_store);
            this.panel2.Controls.Add(this.cblocal);
            this.panel2.Controls.Add(this.labelX5);
            this.panel2.Controls.Add(this.getdc);
            this.panel2.Controls.Add(this.lb_kpdesc);
            this.panel2.Controls.Add(this.labshowvc);
            this.panel2.Controls.Add(this.edt_lot);
            this.panel2.Controls.Add(this.edt_dc);
            this.panel2.Controls.Add(this.buttonX1);
            this.panel2.Controls.Add(this.labqty);
            this.panel2.Controls.Add(this.lablot);
            this.panel2.Controls.Add(this.labelX3);
            this.panel2.Controls.Add(this.labpn);
            this.panel2.Controls.Add(this.labvc);
            this.panel2.Controls.Add(this.labelX4);
            this.panel2.Controls.Add(this.labdc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(893, 206);
            this.panel2.TabIndex = 39;
            // 
            // chk_notinputstorehouse
            // 
            this.chk_notinputstorehouse.AutoSize = true;
            this.chk_notinputstorehouse.Checked = true;
            this.chk_notinputstorehouse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_notinputstorehouse.Enabled = false;
            this.chk_notinputstorehouse.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_notinputstorehouse.Location = new System.Drawing.Point(732, 146);
            this.chk_notinputstorehouse.Name = "chk_notinputstorehouse";
            this.chk_notinputstorehouse.Size = new System.Drawing.Size(60, 16);
            this.chk_notinputstorehouse.TabIndex = 38;
            this.chk_notinputstorehouse.Text = "无库位";
            this.toolTip1.SetToolTip(this.chk_notinputstorehouse, "不记录库位,适用于暂时收料时不确定库位位置");
            this.chk_notinputstorehouse.UseVisualStyleBackColor = true;
            this.chk_notinputstorehouse.CheckedChanged += new System.EventHandler(this.chk_notinputstorehouse_CheckedChanged);
            // 
            // chkdeparture
            // 
            this.chkdeparture.AutoSize = true;
            this.chkdeparture.Enabled = false;
            this.chkdeparture.Location = new System.Drawing.Point(207, 148);
            this.chkdeparture.Name = "chkdeparture";
            this.chkdeparture.Size = new System.Drawing.Size(84, 16);
            this.chkdeparture.TabIndex = 37;
            this.chkdeparture.Text = "[料盘拆分]";
            this.chkdeparture.UseVisualStyleBackColor = true;
            this.chkdeparture.CheckedChanged += new System.EventHandler(this.chkdeparture_CheckedChanged);
            // 
            // edt_pn
            // 
            this.edt_pn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edt_pn.FormattingEnabled = true;
            this.edt_pn.Location = new System.Drawing.Point(108, 71);
            this.edt_pn.Name = "edt_pn";
            this.edt_pn.Size = new System.Drawing.Size(240, 22);
            this.edt_pn.TabIndex = 25;
            // 
            // edt_vc
            // 
            this.edt_vc.FormattingEnabled = true;
            this.edt_vc.Location = new System.Drawing.Point(108, 107);
            this.edt_vc.Name = "edt_vc";
            this.edt_vc.Size = new System.Drawing.Size(240, 20);
            this.edt_vc.TabIndex = 25;
            // 
            // tb_vendernumber
            // 
            this.tb_vendernumber.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_vendernumber.Location = new System.Drawing.Point(108, 28);
            this.tb_vendernumber.Name = "tb_vendernumber";
            this.tb_vendernumber.Size = new System.Drawing.Size(240, 23);
            this.tb_vendernumber.TabIndex = 21;
            this.tb_vendernumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_vendernumber_KeyDown);
            // 
            // edt_qty
            // 
            // 
            // 
            // 
            this.edt_qty.Border.Class = "TextBoxBorder";
            this.edt_qty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.edt_qty.Location = new System.Drawing.Point(108, 144);
            this.edt_qty.Name = "edt_qty";
            this.edt_qty.Size = new System.Drawing.Size(93, 21);
            this.edt_qty.TabIndex = 12;
            this.edt_qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edt_qty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edt_qty_KeyPress);
            this.edt_qty.TextChanged += new System.EventHandler(this.edt_qty_TextChanged);
            // 
            // btlocid
            // 
            this.btlocid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btlocid.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btlocid.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btlocid.Location = new System.Drawing.Point(696, 144);
            this.btlocid.Name = "btlocid";
            this.btlocid.Size = new System.Drawing.Size(31, 20);
            this.btlocid.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btlocid.TabIndex = 36;
            this.btlocid.Text = "储位";
            this.btlocid.Click += new System.EventHandler(this.btlocid_Click);
            // 
            // cb_store
            // 
            this.cb_store.FormattingEnabled = true;
            this.cb_store.Location = new System.Drawing.Point(488, 144);
            this.cb_store.Name = "cb_store";
            this.cb_store.Size = new System.Drawing.Size(79, 20);
            this.cb_store.TabIndex = 32;
            this.cb_store.SelectedValueChanged += new System.EventHandler(this.cb_store_SelectedValueChanged);
            this.cb_store.Validated += new System.EventHandler(this.cb_store_Validated);
            // 
            // cblocal
            // 
            this.cblocal.FormattingEnabled = true;
            this.cblocal.Location = new System.Drawing.Point(572, 144);
            this.cblocal.Name = "cblocal";
            this.cblocal.Size = new System.Drawing.Size(119, 20);
            this.cblocal.TabIndex = 32;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.Location = new System.Drawing.Point(393, 144);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(92, 24);
            this.labelX5.TabIndex = 31;
            this.labelX5.Text = "[仓 库/库 位:]";
            this.labelX5.Click += new System.EventHandler(this.labelX5_Click);
            // 
            // getdc
            // 
            this.getdc.AutoSize = true;
            this.getdc.Location = new System.Drawing.Point(732, 73);
            this.getdc.Name = "getdc";
            this.getdc.Size = new System.Drawing.Size(54, 16);
            this.getdc.TabIndex = 29;
            this.getdc.Text = "DC/LC";
            this.toolTip1.SetToolTip(this.getdc, "自动获取生产周期和生产批次编号");
            this.getdc.UseVisualStyleBackColor = true;
            this.getdc.Click += new System.EventHandler(this.getdc_Click);
            // 
            // lb_kpdesc
            // 
            this.lb_kpdesc.Location = new System.Drawing.Point(490, 21);
            this.lb_kpdesc.Name = "lb_kpdesc";
            this.lb_kpdesc.Size = new System.Drawing.Size(391, 36);
            this.lb_kpdesc.TabIndex = 28;
            this.lb_kpdesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labshowvc
            // 
            this.labshowvc.AutoSize = true;
            this.labshowvc.Location = new System.Drawing.Point(43, 184);
            this.labshowvc.Name = "labshowvc";
            this.labshowvc.Size = new System.Drawing.Size(59, 12);
            this.labshowvc.TabIndex = 26;
            this.labshowvc.Text = "[厂商名:]";
            // 
            // edt_lot
            // 
            this.edt_lot.Location = new System.Drawing.Point(490, 107);
            this.edt_lot.Name = "edt_lot";
            this.edt_lot.Size = new System.Drawing.Size(236, 21);
            this.edt_lot.TabIndex = 24;
            this.edt_lot.MouseEnter += new System.EventHandler(this.edt_lot_MouseLeave);
            // 
            // edt_dc
            // 
            this.edt_dc.Location = new System.Drawing.Point(490, 70);
            this.edt_dc.Name = "edt_dc";
            this.edt_dc.Size = new System.Drawing.Size(236, 21);
            this.edt_dc.TabIndex = 22;
            this.edt_dc.TextChanged += new System.EventHandler(this.edt_dc_TextChanged);
            this.edt_dc.MouseEnter += new System.EventHandler(this.edt_dc_MouseLeave);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Enabled = false;
            this.buttonX1.Location = new System.Drawing.Point(654, 173);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 20;
            this.buttonX1.Text = "添加描述";
            this.buttonX1.Visible = false;
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // labqty
            // 
            // 
            // 
            // 
            this.labqty.BackgroundStyle.Class = "";
            this.labqty.Location = new System.Drawing.Point(25, 144);
            this.labqty.Name = "labqty";
            this.labqty.Size = new System.Drawing.Size(84, 23);
            this.labqty.TabIndex = 13;
            this.labqty.Text = "[数量[QTY]:]";
            // 
            // lablot
            // 
            // 
            // 
            // 
            this.lablot.BackgroundStyle.Class = "";
            this.lablot.Location = new System.Drawing.Point(385, 108);
            this.lablot.Name = "lablot";
            this.lablot.Size = new System.Drawing.Size(109, 23);
            this.lablot.TabIndex = 11;
            this.lablot.Text = "[生产批次[LC]:]";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Location = new System.Drawing.Point(29, 28);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(79, 23);
            this.labelX3.TabIndex = 8;
            this.labelX3.Text = "[厂商料号:]";
            // 
            // labpn
            // 
            // 
            // 
            // 
            this.labpn.BackgroundStyle.Class = "";
            this.labpn.Location = new System.Drawing.Point(4, 71);
            this.labpn.Name = "labpn";
            this.labpn.Size = new System.Drawing.Size(105, 23);
            this.labpn.TabIndex = 8;
            this.labpn.Text = "[斐讯料号[PN]:]";
            // 
            // labvc
            // 
            // 
            // 
            // 
            this.labvc.BackgroundStyle.Class = "";
            this.labvc.Location = new System.Drawing.Point(5, 106);
            this.labvc.Name = "labvc";
            this.labvc.Size = new System.Drawing.Size(106, 23);
            this.labvc.TabIndex = 7;
            this.labvc.Text = "[厂商代码[VC]:]";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.Location = new System.Drawing.Point(402, 28);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(92, 23);
            this.labelX4.TabIndex = 5;
            this.labelX4.Text = "[描   述:]";
            // 
            // labdc
            // 
            // 
            // 
            // 
            this.labdc.BackgroundStyle.Class = "";
            this.labdc.Location = new System.Drawing.Point(385, 71);
            this.labdc.Name = "labdc";
            this.labdc.Size = new System.Drawing.Size(109, 23);
            this.labdc.TabIndex = 5;
            this.labdc.Text = "[生产周期[DC]:]";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelX1);
            this.panel1.Controls.Add(this.labelfilepatch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(893, 47);
            this.panel1.TabIndex = 2;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(15, 14);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(88, 23);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "条码文件路径:";
            // 
            // labelfilepatch
            // 
            // 
            // 
            // 
            this.labelfilepatch.Border.Class = "TextBoxBorder";
            this.labelfilepatch.Location = new System.Drawing.Point(107, 14);
            this.labelfilepatch.Name = "labelfilepatch";
            this.labelfilepatch.ReadOnly = true;
            this.labelfilepatch.Size = new System.Drawing.Size(704, 21);
            this.labelfilepatch.TabIndex = 2;
            // 
            // lb_restqty
            // 
            this.lb_restqty.AutoSize = true;
            this.lb_restqty.Location = new System.Drawing.Point(119, 86);
            this.lb_restqty.Name = "lb_restqty";
            this.lb_restqty.Size = new System.Drawing.Size(11, 12);
            this.lb_restqty.TabIndex = 36;
            this.lb_restqty.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "[未打标签:]";
            // 
            // FrmMaterialsPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 556);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMaterialsPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "材料收料标签打印";
            this.Load += new System.EventHandler(this.print5in1label_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.print5in1label_FormClosed);
            this.panelEx2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX labelfilepatch;
        private System.Windows.Forms.Label lb_kpdesc;
        private System.Windows.Forms.CheckBox chk_notinputstorehouse;
        private DevComponents.DotNetBar.Controls.TextBoxX print_qty;
        private System.Windows.Forms.CheckBox chkdeparture;
        private System.Windows.Forms.TextBox edt_trsn;
        private DevComponents.DotNetBar.Controls.TextBoxX edt_Scan_Data;
        private System.Windows.Forms.ComboBox edt_pn;
        private System.Windows.Forms.ComboBox edt_vc;
        private System.Windows.Forms.TextBox tb_vendernumber;
        private DevComponents.DotNetBar.Controls.TextBoxX edt_qty;
        private DevComponents.DotNetBar.ButtonX btlocid;
        private System.Windows.Forms.CheckBox chkreprint;
        private DevComponents.DotNetBar.LabelX labelX6;
        public System.Windows.Forms.ComboBox cb_store;
        public System.Windows.Forms.ComboBox cblocal;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.Label lb_trsn;
        private System.Windows.Forms.CheckBox getdc;
        private System.Windows.Forms.Label labshowdesc;
        private System.Windows.Forms.Label labshowvc;
        private System.Windows.Forms.TextBox edt_lot;
        private System.Windows.Forms.TextBox edt_dc;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX butprint;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labprinqty;
        private DevComponents.DotNetBar.LabelX labqty;
        private DevComponents.DotNetBar.LabelX lablot;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labpn;
        private DevComponents.DotNetBar.LabelX labvc;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labdc;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RichTextBox rtb_log;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lb_restqty;
        private System.Windows.Forms.Label label1;
    }
}