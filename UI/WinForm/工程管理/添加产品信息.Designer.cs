namespace SFIS_V2
{
    partial class createproduct
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_partnumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_productTypes = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_productColor = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_modelname = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label6 = new System.Windows.Forms.Label();
            this.clb_labeltypes = new System.Windows.Forms.CheckedListBox();
            this.bt_addlabletypes = new DevComponents.DotNetBar.ButtonX();
            this.tb_insertlabletypename = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.rtb_productDesc = new System.Windows.Forms.RichTextBox();
            this.bt_saveproduct = new DevComponents.DotNetBar.ButtonX();
            this.dgv_showproduct = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.sortname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VersionCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrayQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CartonQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PalletQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productcolor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.other = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCTSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOLUTION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BARCODE_LEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAL_PREFIX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSTOMER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STAGE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_selectenter = new DevComponents.DotNetBar.ButtonX();
            this.tb_selectproductname = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_selectpartnumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tb_product = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tb_STAGE = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_COUSTOMER = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_solution = new System.Windows.Forms.ComboBox();
            this.cb_projecttype = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbVersionCode = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.numpallet = new System.Windows.Forms.NumericUpDown();
            this.numcarton = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.numtray = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_barcode_len = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_NALPREFIX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_productsn = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showproduct)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tb_product.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numpallet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numcarton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numtray)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[成品料号]:";
            // 
            // tb_partnumber
            // 
            // 
            // 
            // 
            this.tb_partnumber.Border.Class = "TextBoxBorder";
            this.tb_partnumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tb_partnumber.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_partnumber.Location = new System.Drawing.Point(14, 25);
            this.tb_partnumber.Name = "tb_partnumber";
            this.tb_partnumber.Size = new System.Drawing.Size(187, 26);
            this.tb_partnumber.TabIndex = 1;
            this.tb_partnumber.Leave += new System.EventHandler(this.tb_partnumber_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(384, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "[产品描述]:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "[所属分类]:";
            // 
            // cb_productTypes
            // 
            this.cb_productTypes.DisplayMember = "Text";
            this.cb_productTypes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_productTypes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_productTypes.FormattingEnabled = true;
            this.cb_productTypes.ItemHeight = 20;
            this.cb_productTypes.Location = new System.Drawing.Point(219, 25);
            this.cb_productTypes.Name = "cb_productTypes";
            this.cb_productTypes.Size = new System.Drawing.Size(155, 26);
            this.cb_productTypes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_productTypes.TabIndex = 3;
            this.cb_productTypes.DropDown += new System.EventHandler(this.cb_productTypes_DropDown);
            this.cb_productTypes.Leave += new System.EventHandler(this.cb_productTypes_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "[产品颜色]:";
            // 
            // cb_productColor
            // 
            this.cb_productColor.DisplayMember = "Text";
            this.cb_productColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_productColor.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_productColor.FormattingEnabled = true;
            this.cb_productColor.ItemHeight = 20;
            this.cb_productColor.Location = new System.Drawing.Point(219, 75);
            this.cb_productColor.Name = "cb_productColor";
            this.cb_productColor.Size = new System.Drawing.Size(155, 26);
            this.cb_productColor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_productColor.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "[产品名称]:";
            // 
            // tb_modelname
            // 
            // 
            // 
            // 
            this.tb_modelname.Border.Class = "TextBoxBorder";
            this.tb_modelname.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tb_modelname.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_modelname.Location = new System.Drawing.Point(14, 75);
            this.tb_modelname.Name = "tb_modelname";
            this.tb_modelname.Size = new System.Drawing.Size(187, 26);
            this.tb_modelname.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 271);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "[标签类型]:";
            // 
            // clb_labeltypes
            // 
            this.clb_labeltypes.FormattingEnabled = true;
            this.clb_labeltypes.Location = new System.Drawing.Point(12, 297);
            this.clb_labeltypes.MultiColumn = true;
            this.clb_labeltypes.Name = "clb_labeltypes";
            this.clb_labeltypes.ScrollAlwaysVisible = true;
            this.clb_labeltypes.Size = new System.Drawing.Size(558, 100);
            this.clb_labeltypes.TabIndex = 4;
            this.toolTip1.SetToolTip(this.clb_labeltypes, "请勾选择该产品所具备的序列号类型");
            // 
            // bt_addlabletypes
            // 
            this.bt_addlabletypes.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_addlabletypes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_addlabletypes.Location = new System.Drawing.Point(217, 269);
            this.bt_addlabletypes.Name = "bt_addlabletypes";
            this.bt_addlabletypes.Size = new System.Drawing.Size(64, 21);
            this.bt_addlabletypes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_addlabletypes.TabIndex = 5;
            this.bt_addlabletypes.Text = "添加类型";
            this.bt_addlabletypes.Click += new System.EventHandler(this.bt_addlabletypes_Click);
            // 
            // tb_insertlabletypename
            // 
            // 
            // 
            // 
            this.tb_insertlabletypename.Border.Class = "TextBoxBorder";
            this.tb_insertlabletypename.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tb_insertlabletypename.Location = new System.Drawing.Point(87, 267);
            this.tb_insertlabletypename.Name = "tb_insertlabletypename";
            this.tb_insertlabletypename.Size = new System.Drawing.Size(120, 21);
            this.tb_insertlabletypename.TabIndex = 6;
            this.tb_insertlabletypename.Leave += new System.EventHandler(this.tb_insertlabletypename_Leave);
            // 
            // rtb_productDesc
            // 
            this.rtb_productDesc.Location = new System.Drawing.Point(386, 29);
            this.rtb_productDesc.Name = "rtb_productDesc";
            this.rtb_productDesc.Size = new System.Drawing.Size(508, 72);
            this.rtb_productDesc.TabIndex = 5;
            this.rtb_productDesc.Text = "";
            // 
            // bt_saveproduct
            // 
            this.bt_saveproduct.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_saveproduct.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_saveproduct.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_saveproduct.Location = new System.Drawing.Point(576, 297);
            this.bt_saveproduct.Name = "bt_saveproduct";
            this.bt_saveproduct.Size = new System.Drawing.Size(97, 100);
            this.bt_saveproduct.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_saveproduct.TabIndex = 5;
            this.bt_saveproduct.Text = "保  存";
            this.bt_saveproduct.Click += new System.EventHandler(this.bt_saveproduct_Click);
            // 
            // dgv_showproduct
            // 
            this.dgv_showproduct.AllowUserToAddRows = false;
            this.dgv_showproduct.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_showproduct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_showproduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_showproduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sortname,
            this.partnumber,
            this.productname,
            this.VersionCode,
            this.TrayQty,
            this.CartonQty,
            this.PalletQty,
            this.productcolor,
            this.productdesc,
            this.other,
            this.PRODUCTSN,
            this.SOLUTION,
            this.BARCODE_LEN,
            this.NAL_PREFIX,
            this.CUSTOMER,
            this.STAGE});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_showproduct.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_showproduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_showproduct.EnableHeadersVisualStyles = false;
            this.dgv_showproduct.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.dgv_showproduct.Location = new System.Drawing.Point(0, 0);
            this.dgv_showproduct.Name = "dgv_showproduct";
            this.dgv_showproduct.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_showproduct.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_showproduct.RowTemplate.Height = 23;
            this.dgv_showproduct.Size = new System.Drawing.Size(1158, 214);
            this.dgv_showproduct.TabIndex = 7;
            this.dgv_showproduct.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_showproduct_CellMouseDoubleClick);
            // 
            // sortname
            // 
            this.sortname.DataPropertyName = "sortname";
            this.sortname.HeaderText = "产品类型";
            this.sortname.Name = "sortname";
            this.sortname.ReadOnly = true;
            // 
            // partnumber
            // 
            this.partnumber.DataPropertyName = "partnumber";
            this.partnumber.HeaderText = "料号";
            this.partnumber.Name = "partnumber";
            this.partnumber.ReadOnly = true;
            // 
            // productname
            // 
            this.productname.DataPropertyName = "productname";
            this.productname.HeaderText = "产品型号";
            this.productname.Name = "productname";
            this.productname.ReadOnly = true;
            // 
            // VersionCode
            // 
            this.VersionCode.DataPropertyName = "VersionCode";
            this.VersionCode.HeaderText = "产品版本";
            this.VersionCode.Name = "VersionCode";
            this.VersionCode.ReadOnly = true;
            // 
            // TrayQty
            // 
            this.TrayQty.DataPropertyName = "TrayQty";
            this.TrayQty.HeaderText = "Tray盘数量";
            this.TrayQty.Name = "TrayQty";
            this.TrayQty.ReadOnly = true;
            // 
            // CartonQty
            // 
            this.CartonQty.DataPropertyName = "CartonQty";
            this.CartonQty.HeaderText = "卡通箱数量";
            this.CartonQty.Name = "CartonQty";
            this.CartonQty.ReadOnly = true;
            // 
            // PalletQty
            // 
            this.PalletQty.DataPropertyName = "PalletQty";
            this.PalletQty.HeaderText = "栈板数量";
            this.PalletQty.Name = "PalletQty";
            this.PalletQty.ReadOnly = true;
            // 
            // productcolor
            // 
            this.productcolor.DataPropertyName = "productcolor";
            this.productcolor.HeaderText = "颜色";
            this.productcolor.Name = "productcolor";
            this.productcolor.ReadOnly = true;
            // 
            // productdesc
            // 
            this.productdesc.DataPropertyName = "productdesc";
            this.productdesc.HeaderText = "产品描述";
            this.productdesc.Name = "productdesc";
            this.productdesc.ReadOnly = true;
            // 
            // other
            // 
            this.other.DataPropertyName = "other";
            this.other.HeaderText = "其他";
            this.other.Name = "other";
            this.other.ReadOnly = true;
            // 
            // PRODUCTSN
            // 
            this.PRODUCTSN.DataPropertyName = "PRODUCTSN";
            this.PRODUCTSN.HeaderText = "产品SN";
            this.PRODUCTSN.Name = "PRODUCTSN";
            this.PRODUCTSN.ReadOnly = true;
            // 
            // SOLUTION
            // 
            this.SOLUTION.DataPropertyName = "SOLUTION";
            this.SOLUTION.HeaderText = "产品平台";
            this.SOLUTION.Name = "SOLUTION";
            this.SOLUTION.ReadOnly = true;
            // 
            // BARCODE_LEN
            // 
            this.BARCODE_LEN.DataPropertyName = "BARCODE_LEN";
            this.BARCODE_LEN.HeaderText = "条码长度";
            this.BARCODE_LEN.Name = "BARCODE_LEN";
            this.BARCODE_LEN.ReadOnly = true;
            // 
            // NAL_PREFIX
            // 
            this.NAL_PREFIX.DataPropertyName = "NAL_PREFIX";
            this.NAL_PREFIX.HeaderText = "网标前缀";
            this.NAL_PREFIX.Name = "NAL_PREFIX";
            this.NAL_PREFIX.ReadOnly = true;
            // 
            // CUSTOMER
            // 
            this.CUSTOMER.DataPropertyName = "CUSTOMER";
            this.CUSTOMER.HeaderText = "客户";
            this.CUSTOMER.Name = "CUSTOMER";
            this.CUSTOMER.ReadOnly = true;
            // 
            // STAGE
            // 
            this.STAGE.DataPropertyName = "STAGE";
            this.STAGE.HeaderText = "阶段";
            this.STAGE.Name = "STAGE";
            this.STAGE.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_selectenter);
            this.panel1.Controls.Add(this.tb_selectproductname);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.tb_selectpartnumber);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1158, 29);
            this.panel1.TabIndex = 8;
            // 
            // bt_selectenter
            // 
            this.bt_selectenter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_selectenter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_selectenter.Location = new System.Drawing.Point(410, 5);
            this.bt_selectenter.Name = "bt_selectenter";
            this.bt_selectenter.Size = new System.Drawing.Size(58, 20);
            this.bt_selectenter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_selectenter.TabIndex = 2;
            this.bt_selectenter.Text = "查询";
            this.bt_selectenter.Click += new System.EventHandler(this.bt_selectenter_Click);
            // 
            // tb_selectproductname
            // 
            // 
            // 
            // 
            this.tb_selectproductname.Border.Class = "TextBoxBorder";
            this.tb_selectproductname.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tb_selectproductname.Location = new System.Drawing.Point(275, 4);
            this.tb_selectproductname.Name = "tb_selectproductname";
            this.tb_selectproductname.Size = new System.Drawing.Size(129, 21);
            this.tb_selectproductname.TabIndex = 1;
            this.tb_selectproductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_selectpartnumber_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(213, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "产品型号:";
            // 
            // tb_selectpartnumber
            // 
            // 
            // 
            // 
            this.tb_selectpartnumber.Border.Class = "TextBoxBorder";
            this.tb_selectpartnumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tb_selectpartnumber.Location = new System.Drawing.Point(68, 4);
            this.tb_selectpartnumber.Name = "tb_selectpartnumber";
            this.tb_selectpartnumber.Size = new System.Drawing.Size(129, 21);
            this.tb_selectpartnumber.TabIndex = 1;
            this.tb_selectpartnumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_selectpartnumber_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "成品料号:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_showproduct);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1158, 214);
            this.panel2.TabIndex = 9;
            // 
            // tb_product
            // 
            this.tb_product.Controls.Add(this.panel4);
            this.tb_product.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_product.Location = new System.Drawing.Point(0, 243);
            this.tb_product.Name = "tb_product";
            this.tb_product.Size = new System.Drawing.Size(1158, 403);
            this.tb_product.TabIndex = 11;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.tb_STAGE);
            this.panel4.Controls.Add(this.tb_COUSTOMER);
            this.panel4.Controls.Add(this.tb_solution);
            this.panel4.Controls.Add(this.cb_projecttype);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.tbVersionCode);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.bt_saveproduct);
            this.panel4.Controls.Add(this.numpallet);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.numcarton);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.numtray);
            this.panel4.Controls.Add(this.rtb_productDesc);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.tb_partnumber);
            this.panel4.Controls.Add(this.tb_barcode_len);
            this.panel4.Controls.Add(this.tb_NALPREFIX);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.tb_productsn);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.tb_modelname);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.clb_labeltypes);
            this.panel4.Controls.Add(this.cb_productTypes);
            this.panel4.Controls.Add(this.bt_addlabletypes);
            this.panel4.Controls.Add(this.tb_insertlabletypename);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.cb_productColor);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(933, 403);
            this.panel4.TabIndex = 19;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(586, 106);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(47, 12);
            this.label18.TabIndex = 24;
            this.label18.Text = "[阶段]:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(385, 106);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(47, 12);
            this.label19.TabIndex = 25;
            this.label19.Text = "[客户]:";
            // 
            // tb_STAGE
            // 
            // 
            // 
            // 
            this.tb_STAGE.Border.Class = "TextBoxBorder";
            this.tb_STAGE.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tb_STAGE.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_STAGE.Location = new System.Drawing.Point(587, 122);
            this.tb_STAGE.Name = "tb_STAGE";
            this.tb_STAGE.Size = new System.Drawing.Size(160, 26);
            this.tb_STAGE.TabIndex = 22;
            // 
            // tb_COUSTOMER
            // 
            // 
            // 
            // 
            this.tb_COUSTOMER.Border.Class = "TextBoxBorder";
            this.tb_COUSTOMER.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tb_COUSTOMER.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_COUSTOMER.Location = new System.Drawing.Point(386, 122);
            this.tb_COUSTOMER.Name = "tb_COUSTOMER";
            this.tb_COUSTOMER.Size = new System.Drawing.Size(187, 26);
            this.tb_COUSTOMER.TabIndex = 23;
            // 
            // tb_solution
            // 
            this.tb_solution.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_solution.FormattingEnabled = true;
            this.tb_solution.Items.AddRange(new object[] {
            "Qualcomm8625",
            "Qualcomm8225",
            "Qualcomm7627MTK6252",
            "Qualcomm7627",
            "Qualcomm7227",
            "Qualcomm8x10",
            "Qualcomm8210",
            "Qualcomm9225",
            "MTK6589",
            "MTK6253",
            "MTK6228",
            "Marvell1802",
            "Marvell968",
            "Marvell935",
            "Marvell920",
            "Marvell910",
            "Marvell610"});
            this.tb_solution.Location = new System.Drawing.Point(14, 121);
            this.tb_solution.Name = "tb_solution";
            this.tb_solution.Size = new System.Drawing.Size(183, 24);
            this.tb_solution.TabIndex = 21;
            // 
            // cb_projecttype
            // 
            this.cb_projecttype.FormattingEnabled = true;
            this.cb_projecttype.Items.AddRange(new object[] {
            "CNS",
            "CUST"});
            this.cb_projecttype.Location = new System.Drawing.Point(386, 267);
            this.cb_projecttype.Name = "cb_projecttype";
            this.cb_projecttype.Size = new System.Drawing.Size(121, 20);
            this.cb_projecttype.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(315, 271);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "项目类型:";
            // 
            // tbVersionCode
            // 
            this.tbVersionCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbVersionCode.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbVersionCode.Location = new System.Drawing.Point(280, 224);
            this.tbVersionCode.Name = "tbVersionCode";
            this.tbVersionCode.Size = new System.Drawing.Size(95, 23);
            this.tbVersionCode.TabIndex = 18;
            this.tbVersionCode.Text = "A";
            this.tbVersionCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbVersionCode.Leave += new System.EventHandler(this.tbVersionCode_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(213, 154);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 12);
            this.label17.TabIndex = 17;
            this.label17.Text = "[网标前缀]:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(12, 154);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 12);
            this.label16.TabIndex = 17;
            this.label16.Text = "[产品ENCODE]:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(278, 205);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 12);
            this.label13.TabIndex = 17;
            this.label13.Text = "[产品版本]:";
            // 
            // numpallet
            // 
            this.numpallet.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numpallet.Location = new System.Drawing.Point(192, 226);
            this.numpallet.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numpallet.Name = "numpallet";
            this.numpallet.Size = new System.Drawing.Size(76, 21);
            this.numpallet.TabIndex = 16;
            this.numpallet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numcarton
            // 
            this.numcarton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numcarton.Location = new System.Drawing.Point(97, 224);
            this.numcarton.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numcarton.Name = "numcarton";
            this.numcarton.Size = new System.Drawing.Size(80, 23);
            this.numcarton.TabIndex = 15;
            this.numcarton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(215, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "[条码长度]:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 104);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "[产品平台]:";
            // 
            // numtray
            // 
            this.numtray.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numtray.Location = new System.Drawing.Point(13, 224);
            this.numtray.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numtray.Name = "numtray";
            this.numtray.Size = new System.Drawing.Size(70, 23);
            this.numtray.TabIndex = 14;
            this.numtray.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(191, 205);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "[栈板容量]:";
            // 
            // tb_barcode_len
            // 
            // 
            // 
            // 
            this.tb_barcode_len.Border.Class = "TextBoxBorder";
            this.tb_barcode_len.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tb_barcode_len.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_barcode_len.Location = new System.Drawing.Point(217, 121);
            this.tb_barcode_len.Name = "tb_barcode_len";
            this.tb_barcode_len.Size = new System.Drawing.Size(157, 26);
            this.tb_barcode_len.TabIndex = 1;
            // 
            // tb_NALPREFIX
            // 
            // 
            // 
            // 
            this.tb_NALPREFIX.Border.Class = "TextBoxBorder";
            this.tb_NALPREFIX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tb_NALPREFIX.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_NALPREFIX.Location = new System.Drawing.Point(214, 170);
            this.tb_NALPREFIX.Name = "tb_NALPREFIX";
            this.tb_NALPREFIX.Size = new System.Drawing.Size(160, 26);
            this.tb_NALPREFIX.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(95, 207);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 12);
            this.label11.TabIndex = 12;
            this.label11.Text = "[Carton容量]:";
            // 
            // tb_productsn
            // 
            // 
            // 
            // 
            this.tb_productsn.Border.Class = "TextBoxBorder";
            this.tb_productsn.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tb_productsn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_productsn.Location = new System.Drawing.Point(13, 170);
            this.tb_productsn.Name = "tb_productsn";
            this.tb_productsn.Size = new System.Drawing.Size(187, 26);
            this.tb_productsn.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(12, 207);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "[Tray容量]:";
            // 
            // createproduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 646);
            this.Controls.Add(this.tb_product);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "createproduct";
            this.Text = "创建产品";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.createproduct_FormClosing);
            this.Load += new System.EventHandler(this.createproduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showproduct)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tb_product.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numpallet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numcarton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numtray)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_partnumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_productTypes;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_productColor;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_modelname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox clb_labeltypes;
        private DevComponents.DotNetBar.ButtonX bt_addlabletypes;
        private System.Windows.Forms.RichTextBox rtb_productDesc;
        private DevComponents.DotNetBar.ButtonX bt_saveproduct;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_showproduct;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_insertlabletypename;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_selectpartnumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ButtonX bt_selectenter;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_selectproductname;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel tb_product;
        private System.Windows.Forms.NumericUpDown numpallet;
        private System.Windows.Forms.NumericUpDown numcarton;
        private System.Windows.Forms.NumericUpDown numtray;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbVersionCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cb_projecttype;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_barcode_len;
        private System.Windows.Forms.Label label16;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_productsn;
        private System.Windows.Forms.ComboBox tb_solution;
        private System.Windows.Forms.Label label17;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_NALPREFIX;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_STAGE;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_COUSTOMER;
        private System.Windows.Forms.DataGridViewTextBoxColumn sortname;
        private System.Windows.Forms.DataGridViewTextBoxColumn partnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn productname;
        private System.Windows.Forms.DataGridViewTextBoxColumn VersionCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrayQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CartonQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn PalletQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn productcolor;
        private System.Windows.Forms.DataGridViewTextBoxColumn productdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn other;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCTSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOLUTION;
        private System.Windows.Forms.DataGridViewTextBoxColumn BARCODE_LEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAL_PREFIX;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTOMER;
        private System.Windows.Forms.DataGridViewTextBoxColumn STAGE;
    }
}