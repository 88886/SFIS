namespace SFIS_V2
{
    partial class Frm_StockMove
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bt_modify = new DevComponents.DotNetBar.ButtonX();
            this.bt_confirm = new DevComponents.DotNetBar.ButtonX();
            this.bt_delete = new DevComponents.DotNetBar.ButtonX();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.tb_in = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_out = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_add = new DevComponents.DotNetBar.ButtonX();
            this.tb_cancel = new DevComponents.DotNetBar.ButtonX();
            this.tb_pn = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_qty = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_id = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgv_productsninfo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.bt_rrefresh = new DevComponents.DotNetBar.ButtonX();
            this.bt_excel = new DevComponents.DotNetBar.ButtonX();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.to_excel = new DevComponents.DotNetBar.ButtonX();
            this.bt_select = new DevComponents.DotNetBar.ButtonX();
            this.tb_selectvalue = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cb_selectcondition = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.移库单号 = new DevComponents.Editors.ComboItem();
            this.料号 = new DevComponents.Editors.ComboItem();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgv_moveinfo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panelEx7 = new DevComponents.DotNetBar.PanelEx();
            this.bt_rrefreshid = new DevComponents.DotNetBar.ButtonX();
            this.b_excel = new DevComponents.DotNetBar.ButtonX();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.to_excelid = new DevComponents.DotNetBar.ButtonX();
            this.bt_selectd = new DevComponents.DotNetBar.ButtonX();
            this.tb_value = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_productsninfo)).BeginInit();
            this.panelEx5.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_moveinfo)).BeginInit();
            this.panelEx7.SuspendLayout();
            this.panelEx6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(891, 587);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 79);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(885, 505);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(877, 479);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "成品移库单申请";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(871, 473);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.panelEx3);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(362, 17);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(506, 388);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.listView);
            this.panelEx3.Controls.Add(this.bt_modify);
            this.panelEx3.Controls.Add(this.bt_confirm);
            this.panelEx3.Controls.Add(this.bt_delete);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(3, 17);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(500, 368);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 0;
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(500, 289);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.SmallIcon;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "移库单号";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "成品料号";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.DisplayIndex = 5;
            this.columnHeader3.Text = "成品类型";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.DisplayIndex = 2;
            this.columnHeader4.Text = "移库数量";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.DisplayIndex = 3;
            this.columnHeader5.Text = "移出仓库";
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.DisplayIndex = 4;
            this.columnHeader6.Text = "移入仓库";
            this.columnHeader6.Width = 150;
            // 
            // bt_modify
            // 
            this.bt_modify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_modify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_modify.Font = new System.Drawing.Font("宋体", 11F);
            this.bt_modify.Location = new System.Drawing.Point(34, 331);
            this.bt_modify.Name = "bt_modify";
            this.bt_modify.Size = new System.Drawing.Size(75, 23);
            this.bt_modify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_modify.TabIndex = 12;
            this.bt_modify.Text = "修改";
            this.bt_modify.Click += new System.EventHandler(this.bt_modify_Click);
            // 
            // bt_confirm
            // 
            this.bt_confirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_confirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_confirm.Font = new System.Drawing.Font("宋体", 11F);
            this.bt_confirm.Location = new System.Drawing.Point(326, 331);
            this.bt_confirm.Name = "bt_confirm";
            this.bt_confirm.Size = new System.Drawing.Size(75, 23);
            this.bt_confirm.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_confirm.TabIndex = 13;
            this.bt_confirm.Text = "确认";
            this.bt_confirm.Click += new System.EventHandler(this.bt_confirm_Click);
            // 
            // bt_delete
            // 
            this.bt_delete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_delete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_delete.Font = new System.Drawing.Font("宋体", 11F);
            this.bt_delete.Location = new System.Drawing.Point(181, 331);
            this.bt_delete.Name = "bt_delete";
            this.bt_delete.Size = new System.Drawing.Size(75, 23);
            this.bt_delete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_delete.TabIndex = 14;
            this.bt_delete.Text = "删除";
            this.bt_delete.Click += new System.EventHandler(this.bt_delete_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panelEx2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Location = new System.Drawing.Point(3, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(359, 388);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.tb_in);
            this.panelEx2.Controls.Add(this.tb_out);
            this.panelEx2.Controls.Add(this.tb_add);
            this.panelEx2.Controls.Add(this.tb_cancel);
            this.panelEx2.Controls.Add(this.tb_pn);
            this.panelEx2.Controls.Add(this.tb_qty);
            this.panelEx2.Controls.Add(this.tb_id);
            this.panelEx2.Controls.Add(this.labelX5);
            this.panelEx2.Controls.Add(this.labelX4);
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(3, 17);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(353, 368);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 2;
            // 
            // tb_in
            // 
            // 
            // 
            // 
            this.tb_in.Border.Class = "TextBoxBorder";
            this.tb_in.Font = new System.Drawing.Font("宋体", 11F);
            this.tb_in.Location = new System.Drawing.Point(139, 279);
            this.tb_in.Name = "tb_in";
            this.tb_in.Size = new System.Drawing.Size(142, 24);
            this.tb_in.TabIndex = 17;
            // 
            // tb_out
            // 
            // 
            // 
            // 
            this.tb_out.Border.Class = "TextBoxBorder";
            this.tb_out.Font = new System.Drawing.Font("宋体", 11F);
            this.tb_out.Location = new System.Drawing.Point(139, 216);
            this.tb_out.Name = "tb_out";
            this.tb_out.Size = new System.Drawing.Size(142, 24);
            this.tb_out.TabIndex = 16;
            this.tb_out.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_out_KeyDown);
            // 
            // tb_add
            // 
            this.tb_add.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.tb_add.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.tb_add.Font = new System.Drawing.Font("宋体", 11F);
            this.tb_add.Location = new System.Drawing.Point(171, 331);
            this.tb_add.Name = "tb_add";
            this.tb_add.Size = new System.Drawing.Size(75, 23);
            this.tb_add.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tb_add.TabIndex = 11;
            this.tb_add.Text = "新增";
            this.tb_add.Click += new System.EventHandler(this.tb_add_Click);
            // 
            // tb_cancel
            // 
            this.tb_cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.tb_cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.tb_cancel.Font = new System.Drawing.Font("宋体", 11F);
            this.tb_cancel.Location = new System.Drawing.Point(29, 331);
            this.tb_cancel.Name = "tb_cancel";
            this.tb_cancel.Size = new System.Drawing.Size(75, 23);
            this.tb_cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tb_cancel.TabIndex = 10;
            this.tb_cancel.Text = "取消";
            this.tb_cancel.Click += new System.EventHandler(this.tb_cancel_Click);
            // 
            // tb_pn
            // 
            // 
            // 
            // 
            this.tb_pn.Border.Class = "TextBoxBorder";
            this.tb_pn.Font = new System.Drawing.Font("宋体", 11F);
            this.tb_pn.Location = new System.Drawing.Point(139, 91);
            this.tb_pn.Name = "tb_pn";
            this.tb_pn.Size = new System.Drawing.Size(142, 24);
            this.tb_pn.TabIndex = 9;
            this.tb_pn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_pn_KeyDown);
            // 
            // tb_qty
            // 
            // 
            // 
            // 
            this.tb_qty.Border.Class = "TextBoxBorder";
            this.tb_qty.Font = new System.Drawing.Font("宋体", 11F);
            this.tb_qty.Location = new System.Drawing.Point(139, 158);
            this.tb_qty.Name = "tb_qty";
            this.tb_qty.Size = new System.Drawing.Size(142, 24);
            this.tb_qty.TabIndex = 8;
            this.tb_qty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_qty_KeyDown);
            // 
            // tb_id
            // 
            // 
            // 
            // 
            this.tb_id.Border.Class = "TextBoxBorder";
            this.tb_id.Enabled = false;
            this.tb_id.Font = new System.Drawing.Font("宋体", 11F);
            this.tb_id.Location = new System.Drawing.Point(139, 29);
            this.tb_id.Name = "tb_id";
            this.tb_id.Size = new System.Drawing.Size(142, 24);
            this.tb_id.TabIndex = 5;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.Font = new System.Drawing.Font("宋体", 11F);
            this.labelX5.Location = new System.Drawing.Point(15, 280);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(97, 23);
            this.labelX5.TabIndex = 4;
            this.labelX5.Text = "移入仓库：";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.Font = new System.Drawing.Font("宋体", 11F);
            this.labelX4.Location = new System.Drawing.Point(18, 92);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(86, 23);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "成品料号：";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Font = new System.Drawing.Font("宋体", 11F);
            this.labelX3.Location = new System.Drawing.Point(18, 159);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(86, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "移库数量：";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Font = new System.Drawing.Font("宋体", 11F);
            this.labelX2.Location = new System.Drawing.Point(15, 217);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(97, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "移出仓库：";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Font = new System.Drawing.Font("宋体", 11F);
            this.labelX1.Location = new System.Drawing.Point(18, 30);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(86, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "移库单号：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtbmsg);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(3, 405);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(865, 65);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // rtbmsg
            // 
            this.rtbmsg.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbmsg.ForeColor = System.Drawing.Color.Red;
            this.rtbmsg.Location = new System.Drawing.Point(6, 6);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.ReadOnly = true;
            this.rtbmsg.Size = new System.Drawing.Size(787, 53);
            this.rtbmsg.TabIndex = 7;
            this.rtbmsg.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv_productsninfo);
            this.tabPage2.Controls.Add(this.panelEx5);
            this.tabPage2.Controls.Add(this.panelEx4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(877, 479);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "成品移库单查询";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgv_productsninfo
            // 
            this.dgv_productsninfo.AllowUserToAddRows = false;
            this.dgv_productsninfo.AllowUserToDeleteRows = false;
            this.dgv_productsninfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_productsninfo.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_productsninfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_productsninfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_productsninfo.Location = new System.Drawing.Point(3, 76);
            this.dgv_productsninfo.Name = "dgv_productsninfo";
            this.dgv_productsninfo.ReadOnly = true;
            this.dgv_productsninfo.RowTemplate.Height = 23;
            this.dgv_productsninfo.Size = new System.Drawing.Size(871, 330);
            this.dgv_productsninfo.TabIndex = 3;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.bt_rrefresh);
            this.panelEx5.Controls.Add(this.bt_excel);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx5.Location = new System.Drawing.Point(3, 406);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(871, 70);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 1;
            // 
            // bt_rrefresh
            // 
            this.bt_rrefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_rrefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_rrefresh.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_rrefresh.Location = new System.Drawing.Point(261, 21);
            this.bt_rrefresh.Name = "bt_rrefresh";
            this.bt_rrefresh.Size = new System.Drawing.Size(82, 27);
            this.bt_rrefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_rrefresh.TabIndex = 7;
            this.bt_rrefresh.Text = "刷新";
            this.bt_rrefresh.Click += new System.EventHandler(this.bt_rrefresh_Click);
            // 
            // bt_excel
            // 
            this.bt_excel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_excel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_excel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_excel.Location = new System.Drawing.Point(462, 23);
            this.bt_excel.Name = "bt_excel";
            this.bt_excel.Size = new System.Drawing.Size(95, 27);
            this.bt_excel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_excel.TabIndex = 6;
            this.bt_excel.Text = "汇出excel";
            this.bt_excel.Click += new System.EventHandler(this.bt_excel_Click);
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.to_excel);
            this.panelEx4.Controls.Add(this.bt_select);
            this.panelEx4.Controls.Add(this.tb_selectvalue);
            this.panelEx4.Controls.Add(this.cb_selectcondition);
            this.panelEx4.Controls.Add(this.labelX6);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx4.Location = new System.Drawing.Point(3, 3);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(871, 73);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 0;
            // 
            // to_excel
            // 
            this.to_excel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.to_excel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.to_excel.Location = new System.Drawing.Point(668, 23);
            this.to_excel.Name = "to_excel";
            this.to_excel.Size = new System.Drawing.Size(75, 23);
            this.to_excel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.to_excel.TabIndex = 12;
            this.to_excel.Text = "导出";
            this.to_excel.Click += new System.EventHandler(this.bt_excel_Click);
            // 
            // bt_select
            // 
            this.bt_select.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_select.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_select.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_select.Location = new System.Drawing.Point(527, 23);
            this.bt_select.Name = "bt_select";
            this.bt_select.Size = new System.Drawing.Size(82, 27);
            this.bt_select.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_select.TabIndex = 11;
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
            this.tb_selectvalue.Location = new System.Drawing.Point(313, 23);
            this.tb_selectvalue.Name = "tb_selectvalue";
            this.tb_selectvalue.Size = new System.Drawing.Size(154, 23);
            this.tb_selectvalue.TabIndex = 10;
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
            this.移库单号,
            this.料号});
            this.cb_selectcondition.Location = new System.Drawing.Point(136, 22);
            this.cb_selectcondition.Name = "cb_selectcondition";
            this.cb_selectcondition.Size = new System.Drawing.Size(132, 23);
            this.cb_selectcondition.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_selectcondition.TabIndex = 9;
            // 
            // 移库单号
            // 
            this.移库单号.Text = "移库单号";
            // 
            // 料号
            // 
            this.料号.Text = "料号";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX6.Location = new System.Drawing.Point(54, 22);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(76, 23);
            this.labelX6.TabIndex = 8;
            this.labelX6.Text = "查询条件:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgv_moveinfo);
            this.tabPage3.Controls.Add(this.panelEx7);
            this.tabPage3.Controls.Add(this.panelEx6);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(877, 479);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "成品移库查询";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgv_moveinfo
            // 
            this.dgv_moveinfo.AllowUserToAddRows = false;
            this.dgv_moveinfo.AllowUserToDeleteRows = false;
            this.dgv_moveinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_moveinfo.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_moveinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_moveinfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_moveinfo.Location = new System.Drawing.Point(3, 69);
            this.dgv_moveinfo.Name = "dgv_moveinfo";
            this.dgv_moveinfo.ReadOnly = true;
            this.dgv_moveinfo.RowTemplate.Height = 23;
            this.dgv_moveinfo.Size = new System.Drawing.Size(871, 337);
            this.dgv_moveinfo.TabIndex = 4;
            // 
            // panelEx7
            // 
            this.panelEx7.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx7.Controls.Add(this.bt_rrefreshid);
            this.panelEx7.Controls.Add(this.b_excel);
            this.panelEx7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx7.Location = new System.Drawing.Point(3, 406);
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Size = new System.Drawing.Size(871, 70);
            this.panelEx7.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx7.Style.GradientAngle = 90;
            this.panelEx7.TabIndex = 2;
            // 
            // bt_rrefreshid
            // 
            this.bt_rrefreshid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_rrefreshid.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_rrefreshid.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_rrefreshid.Location = new System.Drawing.Point(261, 21);
            this.bt_rrefreshid.Name = "bt_rrefreshid";
            this.bt_rrefreshid.Size = new System.Drawing.Size(82, 27);
            this.bt_rrefreshid.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_rrefreshid.TabIndex = 7;
            this.bt_rrefreshid.Text = "刷新";
            this.bt_rrefreshid.Click += new System.EventHandler(this.bt_rrefreshid_Click);
            // 
            // b_excel
            // 
            this.b_excel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.b_excel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.b_excel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.b_excel.Location = new System.Drawing.Point(462, 23);
            this.b_excel.Name = "b_excel";
            this.b_excel.Size = new System.Drawing.Size(95, 27);
            this.b_excel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.b_excel.TabIndex = 6;
            this.b_excel.Text = "汇出excel";
            this.b_excel.Click += new System.EventHandler(this.b_excel_Click);
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.to_excelid);
            this.panelEx6.Controls.Add(this.bt_selectd);
            this.panelEx6.Controls.Add(this.tb_value);
            this.panelEx6.Controls.Add(this.labelX7);
            this.panelEx6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx6.Location = new System.Drawing.Point(3, 3);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(871, 66);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 1;
            // 
            // to_excelid
            // 
            this.to_excelid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.to_excelid.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.to_excelid.Location = new System.Drawing.Point(668, 23);
            this.to_excelid.Name = "to_excelid";
            this.to_excelid.Size = new System.Drawing.Size(75, 23);
            this.to_excelid.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.to_excelid.TabIndex = 12;
            this.to_excelid.Text = "导出";
            this.to_excelid.Click += new System.EventHandler(this.to_excelid_Click);
            // 
            // bt_selectd
            // 
            this.bt_selectd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_selectd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_selectd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_selectd.Location = new System.Drawing.Point(527, 23);
            this.bt_selectd.Name = "bt_selectd";
            this.bt_selectd.Size = new System.Drawing.Size(82, 27);
            this.bt_selectd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_selectd.TabIndex = 11;
            this.bt_selectd.Text = "查询";
            this.bt_selectd.Click += new System.EventHandler(this.bt_selectd_Click);
            // 
            // tb_value
            // 
            // 
            // 
            // 
            this.tb_value.Border.Class = "TextBoxBorder";
            this.tb_value.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_value.Location = new System.Drawing.Point(282, 23);
            this.tb_value.Name = "tb_value";
            this.tb_value.Size = new System.Drawing.Size(154, 23);
            this.tb_value.TabIndex = 10;
            this.tb_value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_value_KeyDown);
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX7.Location = new System.Drawing.Point(195, 22);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(76, 23);
            this.labelX7.TabIndex = 8;
            this.labelX7.Text = "移库单号:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelEx1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(885, 62);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Font = new System.Drawing.Font("宋体", 13F);
            this.panelEx1.Location = new System.Drawing.Point(3, 17);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(879, 42);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "成品移库单申请";
            // 
            // Frm_StockMove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 587);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "Frm_StockMove";
            this.Text = "成品移库单申请";
            this.Load += new System.EventHandler(this.Frm_StockMove_Load);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_productsninfo)).EndInit();
            this.panelEx5.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_moveinfo)).EndInit();
            this.panelEx7.ResumeLayout(false);
            this.panelEx6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtbmsg;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX tb_add;
        private DevComponents.DotNetBar.ButtonX tb_cancel;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_pn;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_qty;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_id;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.GroupBox groupBox6;
        private DevComponents.DotNetBar.ButtonX bt_delete;
        private DevComponents.DotNetBar.ButtonX bt_confirm;
        private DevComponents.DotNetBar.ButtonX bt_modify;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ButtonX to_excel;
        private DevComponents.DotNetBar.ButtonX bt_select;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_selectvalue;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_selectcondition;
        private DevComponents.Editors.ComboItem 移库单号;
        private DevComponents.Editors.ComboItem 料号;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.ButtonX bt_rrefresh;
        private DevComponents.DotNetBar.ButtonX bt_excel;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_productsninfo;
        internal System.Windows.Forms.ListView listView;
        internal System.Windows.Forms.ColumnHeader columnHeader1;
        internal System.Windows.Forms.ColumnHeader columnHeader2;
        internal System.Windows.Forms.ColumnHeader columnHeader4;
        internal System.Windows.Forms.ColumnHeader columnHeader5;
        internal System.Windows.Forms.ColumnHeader columnHeader6;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_in;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_out;
        private System.Windows.Forms.TabPage tabPage3;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_moveinfo;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private DevComponents.DotNetBar.ButtonX bt_rrefreshid;
        private DevComponents.DotNetBar.ButtonX b_excel;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private DevComponents.DotNetBar.ButtonX to_excelid;
        private DevComponents.DotNetBar.ButtonX bt_selectd;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_value;
        private DevComponents.DotNetBar.LabelX labelX7;
        private System.Windows.Forms.ColumnHeader columnHeader3;


    }
}