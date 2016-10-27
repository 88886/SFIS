namespace SFIS_V2
{
    partial class Frm_StockQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.产品信息查询 = new System.Windows.Forms.TabPage();
            this.dgv_productsninfo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panelEx11 = new DevComponents.DotNetBar.PanelEx();
            this.bt_rrefresh = new DevComponents.DotNetBar.ButtonX();
            this.bt_excel = new DevComponents.DotNetBar.ButtonX();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.to_excel = new DevComponents.DotNetBar.ButtonX();
            this.bt_select = new DevComponents.DotNetBar.ButtonX();
            this.tb_selectvalue = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cb_selectcondition = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.工单 = new DevComponents.Editors.ComboItem();
            this.栈板 = new DevComponents.Editors.ComboItem();
            this.卡通 = new DevComponents.Editors.ComboItem();
            this.入库批次 = new DevComponents.Editors.ComboItem();
            this.出库批次 = new DevComponents.Editors.ComboItem();
            this.ESN = new DevComponents.Editors.ComboItem();
            this.SNVAL = new DevComponents.Editors.ComboItem();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.库存查询 = new System.Windows.Forms.TabPage();
            this.dgvStock = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.cb_select = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDATA = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btQuery2 = new DevComponents.DotNetBar.ButtonX();
            this.panelEx7 = new DevComponents.DotNetBar.PanelEx();
            this.stock_toexcel = new DevComponents.DotNetBar.ButtonX();
            this.bt_Refesh2 = new DevComponents.DotNetBar.ButtonX();
            this.dgvReport = new System.Windows.Forms.TabControl();
            this.产品信息查询.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_productsninfo)).BeginInit();
            this.panelEx11.SuspendLayout();
            this.panelEx6.SuspendLayout();
            this.库存查询.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            this.panelEx5.SuspendLayout();
            this.panelEx7.SuspendLayout();
            this.dgvReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // 产品信息查询
            // 
            this.产品信息查询.Controls.Add(this.dgv_productsninfo);
            this.产品信息查询.Controls.Add(this.panelEx11);
            this.产品信息查询.Controls.Add(this.panelEx6);
            this.产品信息查询.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.产品信息查询.Location = new System.Drawing.Point(4, 22);
            this.产品信息查询.Name = "产品信息查询";
            this.产品信息查询.Padding = new System.Windows.Forms.Padding(3);
            this.产品信息查询.Size = new System.Drawing.Size(992, 574);
            this.产品信息查询.TabIndex = 5;
            this.产品信息查询.Text = "产品信息查询";
            this.产品信息查询.UseVisualStyleBackColor = true;
            // 
            // dgv_productsninfo
            // 
            this.dgv_productsninfo.AllowUserToAddRows = false;
            this.dgv_productsninfo.AllowUserToDeleteRows = false;
            this.dgv_productsninfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_productsninfo.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_productsninfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_productsninfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_productsninfo.Location = new System.Drawing.Point(3, 56);
            this.dgv_productsninfo.Name = "dgv_productsninfo";
            this.dgv_productsninfo.ReadOnly = true;
            this.dgv_productsninfo.RowTemplate.Height = 23;
            this.dgv_productsninfo.Size = new System.Drawing.Size(986, 474);
            this.dgv_productsninfo.TabIndex = 2;
            this.dgv_productsninfo.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_productsninfo_RowPostPaint);
            // 
            // panelEx11
            // 
            this.panelEx11.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx11.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx11.Controls.Add(this.bt_rrefresh);
            this.panelEx11.Controls.Add(this.bt_excel);
            this.panelEx11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx11.Location = new System.Drawing.Point(3, 530);
            this.panelEx11.Name = "panelEx11";
            this.panelEx11.Size = new System.Drawing.Size(986, 41);
            this.panelEx11.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx11.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx11.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx11.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx11.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx11.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx11.Style.GradientAngle = 90;
            this.panelEx11.TabIndex = 1;
            // 
            // bt_rrefresh
            // 
            this.bt_rrefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_rrefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_rrefresh.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_rrefresh.Location = new System.Drawing.Point(322, 9);
            this.bt_rrefresh.Name = "bt_rrefresh";
            this.bt_rrefresh.Size = new System.Drawing.Size(82, 27);
            this.bt_rrefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_rrefresh.TabIndex = 5;
            this.bt_rrefresh.Text = "刷新";
            this.bt_rrefresh.Click += new System.EventHandler(this.bt_rrefresh_Click);
            // 
            // bt_excel
            // 
            this.bt_excel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_excel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_excel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_excel.Location = new System.Drawing.Point(523, 11);
            this.bt_excel.Name = "bt_excel";
            this.bt_excel.Size = new System.Drawing.Size(95, 27);
            this.bt_excel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_excel.TabIndex = 4;
            this.bt_excel.Text = "汇出excel";
            this.bt_excel.Click += new System.EventHandler(this.bt_excel_Click);
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.to_excel);
            this.panelEx6.Controls.Add(this.bt_select);
            this.panelEx6.Controls.Add(this.tb_selectvalue);
            this.panelEx6.Controls.Add(this.cb_selectcondition);
            this.panelEx6.Controls.Add(this.labelX1);
            this.panelEx6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx6.Location = new System.Drawing.Point(3, 3);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(986, 53);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 0;
            // 
            // to_excel
            // 
            this.to_excel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.to_excel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.to_excel.Location = new System.Drawing.Point(677, 14);
            this.to_excel.Name = "to_excel";
            this.to_excel.Size = new System.Drawing.Size(75, 23);
            this.to_excel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.to_excel.TabIndex = 7;
            this.to_excel.Text = "导出";
            this.to_excel.Click += new System.EventHandler(this.to_excel_Click);
            // 
            // bt_select
            // 
            this.bt_select.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_select.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_select.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_select.Location = new System.Drawing.Point(536, 14);
            this.bt_select.Name = "bt_select";
            this.bt_select.Size = new System.Drawing.Size(82, 27);
            this.bt_select.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_select.TabIndex = 3;
            this.bt_select.Text = "查询";
            this.bt_select.Click += new System.EventHandler(this.bt_select_Click);
            // 
            // tb_selectvalue
            // 
            // 
            // 
            // 
            this.tb_selectvalue.Border.Class = "TextBoxBorder";
            this.tb_selectvalue.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_selectvalue.Location = new System.Drawing.Point(322, 14);
            this.tb_selectvalue.Name = "tb_selectvalue";
            this.tb_selectvalue.Size = new System.Drawing.Size(154, 23);
            this.tb_selectvalue.TabIndex = 2;
            this.tb_selectvalue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_selectvalue_KeyDown);
            // 
            // cb_selectcondition
            // 
            this.cb_selectcondition.DisplayMember = "Text";
            this.cb_selectcondition.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_selectcondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_selectcondition.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_selectcondition.FormattingEnabled = true;
            this.cb_selectcondition.ItemHeight = 17;
            this.cb_selectcondition.Items.AddRange(new object[] {
            this.工单,
            this.栈板,
            this.卡通,
            this.入库批次,
            this.出库批次,
            this.ESN,
            this.SNVAL});
            this.cb_selectcondition.Location = new System.Drawing.Point(145, 13);
            this.cb_selectcondition.Name = "cb_selectcondition";
            this.cb_selectcondition.Size = new System.Drawing.Size(132, 23);
            this.cb_selectcondition.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_selectcondition.TabIndex = 1;
            this.cb_selectcondition.SelectedIndexChanged += new System.EventHandler(this.cb_selectcondition_SelectedIndexChanged);
            // 
            // 工单
            // 
            this.工单.Text = "工单";
            // 
            // 栈板
            // 
            this.栈板.Text = "栈板";
            // 
            // 卡通
            // 
            this.卡通.Text = "卡通";
            // 
            // 入库批次
            // 
            this.入库批次.Text = "入库批次";
            // 
            // 出库批次
            // 
            this.出库批次.Text = "出库批次";
            // 
            // ESN
            // 
            this.ESN.Text = "ESN";
            // 
            // SNVAL
            // 
            this.SNVAL.Text = "SNVAL";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(63, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(76, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "查询条件:";
            // 
            // 库存查询
            // 
            this.库存查询.Controls.Add(this.dgvStock);
            this.库存查询.Controls.Add(this.panelEx4);
            this.库存查询.Controls.Add(this.panelEx5);
            this.库存查询.Controls.Add(this.panelEx7);
            this.库存查询.Location = new System.Drawing.Point(4, 22);
            this.库存查询.Name = "库存查询";
            this.库存查询.Padding = new System.Windows.Forms.Padding(3);
            this.库存查询.Size = new System.Drawing.Size(992, 574);
            this.库存查询.TabIndex = 1;
            this.库存查询.Text = "库存查询";
            this.库存查询.UseVisualStyleBackColor = true;
            // 
            // dgvStock
            // 
            this.dgvStock.AllowUserToAddRows = false;
            this.dgvStock.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStock.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStock.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvStock.Location = new System.Drawing.Point(3, 77);
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStock.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvStock.RowTemplate.Height = 23;
            this.dgvStock.Size = new System.Drawing.Size(986, 451);
            this.dgvStock.TabIndex = 7;
            this.dgvStock.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvStock_RowPostPaint);
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx4.Location = new System.Drawing.Point(3, 51);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(986, 26);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 6;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.cb_select);
            this.panelEx5.Controls.Add(this.label1);
            this.panelEx5.Controls.Add(this.txtDATA);
            this.panelEx5.Controls.Add(this.btQuery2);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx5.Location = new System.Drawing.Point(3, 3);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(986, 48);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 5;
            // 
            // cb_select
            // 
            this.cb_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_select.FormattingEnabled = true;
            this.cb_select.Items.AddRange(new object[] {
            "ESN",
            "CARTON",
            "PALLET"});
            this.cb_select.Location = new System.Drawing.Point(77, 14);
            this.cb_select.Name = "cb_select";
            this.cb_select.Size = new System.Drawing.Size(121, 20);
            this.cb_select.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "[料号]:";
            // 
            // txtDATA
            // 
            // 
            // 
            // 
            this.txtDATA.Border.Class = "TextBoxBorder";
            this.txtDATA.Location = new System.Drawing.Point(223, 13);
            this.txtDATA.Name = "txtDATA";
            this.txtDATA.Size = new System.Drawing.Size(145, 21);
            this.txtDATA.TabIndex = 4;
            // 
            // btQuery2
            // 
            this.btQuery2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btQuery2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btQuery2.ForeColor = System.Drawing.Color.Purple;
            this.btQuery2.Location = new System.Drawing.Point(424, 15);
            this.btQuery2.Name = "btQuery2";
            this.btQuery2.Size = new System.Drawing.Size(75, 23);
            this.btQuery2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btQuery2.TabIndex = 0;
            this.btQuery2.Text = "查询";
            this.btQuery2.Click += new System.EventHandler(this.btQuery2_Click);
            // 
            // panelEx7
            // 
            this.panelEx7.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx7.Controls.Add(this.stock_toexcel);
            this.panelEx7.Controls.Add(this.bt_Refesh2);
            this.panelEx7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx7.Location = new System.Drawing.Point(3, 528);
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Size = new System.Drawing.Size(986, 43);
            this.panelEx7.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx7.Style.GradientAngle = 90;
            this.panelEx7.TabIndex = 4;
            // 
            // stock_toexcel
            // 
            this.stock_toexcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.stock_toexcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.stock_toexcel.Location = new System.Drawing.Point(524, 16);
            this.stock_toexcel.Name = "stock_toexcel";
            this.stock_toexcel.Size = new System.Drawing.Size(75, 23);
            this.stock_toexcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.stock_toexcel.TabIndex = 1;
            this.stock_toexcel.Text = "汇出Excel";
            this.stock_toexcel.Click += new System.EventHandler(this.stock_toexcel_Click);
            // 
            // bt_Refesh2
            // 
            this.bt_Refesh2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_Refesh2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_Refesh2.ForeColor = System.Drawing.Color.Purple;
            this.bt_Refesh2.Location = new System.Drawing.Point(424, 15);
            this.bt_Refesh2.Name = "bt_Refesh2";
            this.bt_Refesh2.Size = new System.Drawing.Size(75, 23);
            this.bt_Refesh2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_Refesh2.TabIndex = 0;
            this.bt_Refesh2.Text = "刷新";
            this.bt_Refesh2.Click += new System.EventHandler(this.bt_Refesh2_Click);
            // 
            // dgvReport
            // 
            this.dgvReport.Controls.Add(this.库存查询);
            this.dgvReport.Controls.Add(this.产品信息查询);
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.Location = new System.Drawing.Point(0, 0);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.SelectedIndex = 0;
            this.dgvReport.Size = new System.Drawing.Size(1000, 600);
            this.dgvReport.TabIndex = 3;
            // 
            // Frm_StockQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Frm_StockQuery";
            this.Text = "成品出入库查询";
            this.Load += new System.EventHandler(this.Frm_StockQuery_Load);
            this.产品信息查询.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_productsninfo)).EndInit();
            this.panelEx11.ResumeLayout(false);
            this.panelEx6.ResumeLayout(false);
            this.库存查询.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            this.panelEx5.ResumeLayout(false);
            this.panelEx5.PerformLayout();
            this.panelEx7.ResumeLayout(false);
            this.dgvReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage 产品信息查询;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_productsninfo;
        private DevComponents.DotNetBar.PanelEx panelEx11;
        private DevComponents.DotNetBar.ButtonX bt_rrefresh;
        private DevComponents.DotNetBar.ButtonX bt_excel;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private DevComponents.DotNetBar.ButtonX bt_select;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_selectvalue;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_selectcondition;
        private DevComponents.Editors.ComboItem 工单;
        private DevComponents.Editors.ComboItem 栈板;
        private DevComponents.Editors.ComboItem 卡通;
        private DevComponents.Editors.ComboItem 入库批次;
        private DevComponents.Editors.ComboItem 出库批次;
        private DevComponents.Editors.ComboItem ESN;
        private DevComponents.Editors.ComboItem SNVAL;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.TabPage 库存查询;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvStock;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDATA;
        private DevComponents.DotNetBar.ButtonX btQuery2;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private DevComponents.DotNetBar.ButtonX stock_toexcel;
        private DevComponents.DotNetBar.ButtonX bt_Refesh2;
        private System.Windows.Forms.TabControl dgvReport;
        private DevComponents.DotNetBar.ButtonX to_excel;
        private System.Windows.Forms.ComboBox cb_select;



    }
}