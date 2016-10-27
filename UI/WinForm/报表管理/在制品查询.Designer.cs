namespace SFIS_V2
{
    partial class FrmWipTracking
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_ToExcel = new DevComponents.DotNetBar.ButtonX();
            this.chkallline = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btQuery = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.wolist = new System.Windows.Forms.ListBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btClearwolist = new System.Windows.Forms.ToolStripMenuItem();
            this.btwoselect = new DevComponents.DotNetBar.ButtonX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PartList = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btclearpartlist = new System.Windows.Forms.ToolStripMenuItem();
            this.btpartselect = new DevComponents.DotNetBar.ButtonX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.listallPart = new System.Windows.Forms.ListBox();
            this.listallwo = new System.Windows.Forms.ListBox();
            this.PalPartNo = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tb_selectpartnumber = new System.Windows.Forms.TextBox();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.btPartCancel = new DevComponents.DotNetBar.ButtonX();
            this.btPartOK = new DevComponents.DotNetBar.ButtonX();
            this.Palwo = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tb_selectwoId = new System.Windows.Forms.TextBox();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.btwoCancel = new DevComponents.DotNetBar.ButtonX();
            this.btwoOK = new DevComponents.DotNetBar.ButtonX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelEx2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.PalPartNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.Palwo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Font = new System.Drawing.Font("新宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(919, 71);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "Working In Process";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panel3);
            this.panelEx2.Controls.Add(this.groupBox2);
            this.panelEx2.Controls.Add(this.panel1);
            this.panelEx2.Controls.Add(this.groupBox1);
            this.panelEx2.Controls.Add(this.panel2);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 71);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(919, 140);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.bt_ToExcel);
            this.panel3.Controls.Add(this.chkallline);
            this.panel3.Controls.Add(this.btQuery);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(579, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(340, 140);
            this.panel3.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Green;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(305, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "     ";
            // 
            // bt_ToExcel
            // 
            this.bt_ToExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_ToExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_ToExcel.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_ToExcel.Location = new System.Drawing.Point(173, 58);
            this.bt_ToExcel.Name = "bt_ToExcel";
            this.bt_ToExcel.Size = new System.Drawing.Size(115, 46);
            this.bt_ToExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_ToExcel.TabIndex = 8;
            this.bt_ToExcel.Text = "汇出Excel";
            this.bt_ToExcel.Click += new System.EventHandler(this.bt_ToExcel_Click);
            // 
            // chkallline
            // 
            // 
            // 
            // 
            this.chkallline.BackgroundStyle.Class = "";
            this.chkallline.Location = new System.Drawing.Point(22, 29);
            this.chkallline.Name = "chkallline";
            this.chkallline.Size = new System.Drawing.Size(132, 23);
            this.chkallline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkallline.TabIndex = 7;
            this.chkallline.Text = "Summary All Line";
            // 
            // btQuery
            // 
            this.btQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btQuery.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btQuery.Location = new System.Drawing.Point(22, 58);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(115, 46);
            this.btQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btQuery.TabIndex = 6;
            this.btQuery.Text = "查询";
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.wolist);
            this.groupBox2.Controls.Add(this.btwoselect);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(332, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 140);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工单";
            // 
            // wolist
            // 
            this.wolist.ContextMenuStrip = this.contextMenuStrip2;
            this.wolist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wolist.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wolist.FormattingEnabled = true;
            this.wolist.ItemHeight = 14;
            this.wolist.Location = new System.Drawing.Point(3, 27);
            this.wolist.Name = "wolist";
            this.wolist.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.wolist.Size = new System.Drawing.Size(204, 87);
            this.wolist.TabIndex = 0;
            this.toolTip1.SetToolTip(this.wolist, "右键清空或选中项按DEL键删除");
            this.wolist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.wolist_KeyDown);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btClearwolist});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(101, 26);
            // 
            // btClearwolist
            // 
            this.btClearwolist.Name = "btClearwolist";
            this.btClearwolist.Size = new System.Drawing.Size(100, 22);
            this.btClearwolist.Text = "清空";
            this.btClearwolist.Click += new System.EventHandler(this.btClearwolist_Click);
            // 
            // btwoselect
            // 
            this.btwoselect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btwoselect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btwoselect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btwoselect.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btwoselect.Location = new System.Drawing.Point(3, 114);
            this.btwoselect.Name = "btwoselect";
            this.btwoselect.Size = new System.Drawing.Size(204, 23);
            this.btwoselect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btwoselect.TabIndex = 1;
            this.btwoselect.Text = "工单选择 ▼";
            this.btwoselect.Click += new System.EventHandler(this.btwoselect_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(254, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(78, 140);
            this.panel1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PartList);
            this.groupBox1.Controls.Add(this.btpartselect);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(32, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(222, 140);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "产品料号";
            // 
            // PartList
            // 
            this.PartList.ContextMenuStrip = this.contextMenuStrip1;
            this.PartList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PartList.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PartList.FormattingEnabled = true;
            this.PartList.ItemHeight = 14;
            this.PartList.Location = new System.Drawing.Point(3, 27);
            this.PartList.Name = "PartList";
            this.PartList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.PartList.Size = new System.Drawing.Size(216, 87);
            this.PartList.TabIndex = 0;
            this.toolTip1.SetToolTip(this.PartList, "右键清空或选中项按DEL键删除");
            this.PartList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PartList_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btclearpartlist});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // btclearpartlist
            // 
            this.btclearpartlist.Name = "btclearpartlist";
            this.btclearpartlist.Size = new System.Drawing.Size(100, 22);
            this.btclearpartlist.Text = "清空";
            this.btclearpartlist.Click += new System.EventHandler(this.btclearpartlist_Click);
            // 
            // btpartselect
            // 
            this.btpartselect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btpartselect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btpartselect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btpartselect.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btpartselect.Location = new System.Drawing.Point(3, 114);
            this.btpartselect.Name = "btpartselect";
            this.btpartselect.Size = new System.Drawing.Size(216, 23);
            this.btpartselect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btpartselect.TabIndex = 1;
            this.btpartselect.Text = "料号选择 ▼";
            this.btpartselect.Click += new System.EventHandler(this.btpartselect_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(32, 140);
            this.panel2.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 211);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(919, 384);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            // 
            // listallPart
            // 
            this.listallPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listallPart.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listallPart.FormattingEnabled = true;
            this.listallPart.ItemHeight = 14;
            this.listallPart.Location = new System.Drawing.Point(0, 0);
            this.listallPart.Name = "listallPart";
            this.listallPart.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listallPart.Size = new System.Drawing.Size(222, 256);
            this.listallPart.TabIndex = 3;
            this.listallPart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listallPart_MouseClick);
            this.listallPart.MouseEnter += new System.EventHandler(this.listallPart_MouseEnter);
            // 
            // listallwo
            // 
            this.listallwo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listallwo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listallwo.FormattingEnabled = true;
            this.listallwo.ItemHeight = 14;
            this.listallwo.Location = new System.Drawing.Point(0, 0);
            this.listallwo.Name = "listallwo";
            this.listallwo.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listallwo.Size = new System.Drawing.Size(207, 256);
            this.listallwo.TabIndex = 4;
            // 
            // PalPartNo
            // 
            this.PalPartNo.Controls.Add(this.splitContainer1);
            this.PalPartNo.Controls.Add(this.panelEx3);
            this.PalPartNo.Location = new System.Drawing.Point(32, 211);
            this.PalPartNo.Name = "PalPartNo";
            this.PalPartNo.Size = new System.Drawing.Size(222, 327);
            this.PalPartNo.TabIndex = 5;
            this.PalPartNo.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listallPart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tb_selectpartnumber);
            this.splitContainer1.Size = new System.Drawing.Size(222, 292);
            this.splitContainer1.SplitterDistance = 256;
            this.splitContainer1.TabIndex = 4;
            // 
            // tb_selectpartnumber
            // 
            this.tb_selectpartnumber.Location = new System.Drawing.Point(7, 5);
            this.tb_selectpartnumber.Name = "tb_selectpartnumber";
            this.tb_selectpartnumber.Size = new System.Drawing.Size(212, 21);
            this.tb_selectpartnumber.TabIndex = 0;
            this.tb_selectpartnumber.TextChanged += new System.EventHandler(this.tb_selectpartnumber_TextChanged);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.btPartCancel);
            this.panelEx3.Controls.Add(this.btPartOK);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx3.Location = new System.Drawing.Point(0, 292);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(222, 35);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 0;
            // 
            // btPartCancel
            // 
            this.btPartCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btPartCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btPartCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btPartCancel.Location = new System.Drawing.Point(119, 0);
            this.btPartCancel.Name = "btPartCancel";
            this.btPartCancel.Size = new System.Drawing.Size(103, 35);
            this.btPartCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btPartCancel.TabIndex = 1;
            this.btPartCancel.Text = "Cancel";
            this.btPartCancel.Click += new System.EventHandler(this.btPartCancel_Click);
            // 
            // btPartOK
            // 
            this.btPartOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btPartOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btPartOK.Dock = System.Windows.Forms.DockStyle.Left;
            this.btPartOK.Location = new System.Drawing.Point(0, 0);
            this.btPartOK.Name = "btPartOK";
            this.btPartOK.Size = new System.Drawing.Size(103, 35);
            this.btPartOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btPartOK.TabIndex = 0;
            this.btPartOK.Text = "OK";
            this.btPartOK.Click += new System.EventHandler(this.btPartOK_Click);
            // 
            // Palwo
            // 
            this.Palwo.Controls.Add(this.splitContainer2);
            this.Palwo.Controls.Add(this.panelEx4);
            this.Palwo.Location = new System.Drawing.Point(333, 210);
            this.Palwo.Name = "Palwo";
            this.Palwo.Size = new System.Drawing.Size(207, 328);
            this.Palwo.TabIndex = 6;
            this.Palwo.Visible = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listallwo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tb_selectwoId);
            this.splitContainer2.Size = new System.Drawing.Size(207, 293);
            this.splitContainer2.SplitterDistance = 256;
            this.splitContainer2.TabIndex = 6;
            // 
            // tb_selectwoId
            // 
            this.tb_selectwoId.Location = new System.Drawing.Point(9, 6);
            this.tb_selectwoId.Name = "tb_selectwoId";
            this.tb_selectwoId.Size = new System.Drawing.Size(191, 21);
            this.tb_selectwoId.TabIndex = 0;
            this.tb_selectwoId.TextChanged += new System.EventHandler(this.tb_selectwoId_TextChanged);
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.btwoCancel);
            this.panelEx4.Controls.Add(this.btwoOK);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx4.Location = new System.Drawing.Point(0, 293);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(207, 35);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 5;
            // 
            // btwoCancel
            // 
            this.btwoCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btwoCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btwoCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btwoCancel.Location = new System.Drawing.Point(104, 0);
            this.btwoCancel.Name = "btwoCancel";
            this.btwoCancel.Size = new System.Drawing.Size(103, 35);
            this.btwoCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btwoCancel.TabIndex = 1;
            this.btwoCancel.Text = "Cancel";
            this.btwoCancel.Click += new System.EventHandler(this.btwoCancel_Click);
            // 
            // btwoOK
            // 
            this.btwoOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btwoOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btwoOK.Dock = System.Windows.Forms.DockStyle.Left;
            this.btwoOK.Location = new System.Drawing.Point(0, 0);
            this.btwoOK.Name = "btwoOK";
            this.btwoOK.Size = new System.Drawing.Size(103, 35);
            this.btwoOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btwoOK.TabIndex = 0;
            this.btwoOK.Text = "OK";
            this.btwoOK.Click += new System.EventHandler(this.btwoOK_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ShowAlways = true;
            // 
            // FrmWipTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 595);
            this.Controls.Add(this.PalPartNo);
            this.Controls.Add(this.Palwo);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmWipTracking";
            this.Text = "在制品查询";
            this.Load += new System.EventHandler(this.FrmWipTracking_Load);
            this.panelEx2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.PalPartNo.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.Palwo.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox wolist;
        private System.Windows.Forms.ListBox PartList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ButtonX btwoselect;
        private DevComponents.DotNetBar.ButtonX btpartselect;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListBox listallPart;
        private System.Windows.Forms.ListBox listallwo;
        private System.Windows.Forms.Panel PalPartNo;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ButtonX btPartOK;
        private DevComponents.DotNetBar.ButtonX btPartCancel;
        private System.Windows.Forms.Panel Palwo;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ButtonX btwoCancel;
        private DevComponents.DotNetBar.ButtonX btwoOK;
        private DevComponents.DotNetBar.ButtonX btQuery;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkallline;
        private DevComponents.DotNetBar.ButtonX bt_ToExcel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tb_selectpartnumber;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox tb_selectwoId;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btclearpartlist;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem btClearwolist;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
    }
}