namespace SFIS_V2
{
    partial class DataPartition
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.tsmi_printpre = new System.Windows.Forms.ToolStripMenuItem();
            this.TabDepartation = new SFIS_V2.MyTabControlEx();
            this.箱栈板拆分 = new System.Windows.Forms.TabPage();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.lb_partion = new System.Windows.Forms.Label();
            this.lblNewcarton = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvPartition = new System.Windows.Forms.DataGridView();
            this.btBack = new DevComponents.DotNetBar.ButtonX();
            this.btSelect = new DevComponents.DotNetBar.ButtonX();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.lb_source = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgv_showdata = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_query = new DevComponents.DotNetBar.ButtonX();
            this.txtCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cmbdptype = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.箱栈板合并 = new System.Windows.Forms.TabPage();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.lbltargetloc = new System.Windows.Forms.Label();
            this.lbltargethouse = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTarget = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblTarget = new System.Windows.Forms.Label();
            this.dgvTarget = new System.Windows.Forms.DataGridView();
            this.bt_combine1 = new DevComponents.DotNetBar.ButtonX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.lblsourceloc = new System.Windows.Forms.Label();
            this.lblsourcehouse = new System.Windows.Forms.Label();
            this.txtSource = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblSource = new System.Windows.Forms.Label();
            this.dgvSource = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbcombine = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.TabDepartation.SuspendLayout();
            this.箱栈板拆分.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartition)).BeginInit();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showdata)).BeginInit();
            this.panel1.SuspendLayout();
            this.箱栈板合并.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarget)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_printpre});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 26);
            // 
            // tsmi_printpre
            // 
            this.tsmi_printpre.Name = "tsmi_printpre";
            this.tsmi_printpre.Size = new System.Drawing.Size(118, 22);
            this.tsmi_printpre.Text = "打印标签";
            this.tsmi_printpre.Click += new System.EventHandler(this.tsmi_printpre_Click);
            // 
            // TabDepartation
            // 
            this.TabDepartation.Controls.Add(this.箱栈板拆分);
            this.TabDepartation.Controls.Add(this.箱栈板合并);
            this.TabDepartation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabDepartation.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabDepartation.Location = new System.Drawing.Point(0, 0);
            this.TabDepartation.Name = "TabDepartation";
            this.TabDepartation.SelectedIndex = 0;
            this.TabDepartation.Size = new System.Drawing.Size(1148, 393);
            this.TabDepartation.TabIndex = 0;
            // 
            // 箱栈板拆分
            // 
            this.箱栈板拆分.Controls.Add(this.panelEx3);
            this.箱栈板拆分.Controls.Add(this.panelEx4);
            this.箱栈板拆分.Controls.Add(this.panel1);
            this.箱栈板拆分.Location = new System.Drawing.Point(4, 22);
            this.箱栈板拆分.Name = "箱栈板拆分";
            this.箱栈板拆分.Padding = new System.Windows.Forms.Padding(3);
            this.箱栈板拆分.Size = new System.Drawing.Size(1140, 367);
            this.箱栈板拆分.TabIndex = 0;
            this.箱栈板拆分.Text = "箱/栈板拆分";
            this.箱栈板拆分.UseVisualStyleBackColor = true;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.lb_partion);
            this.panelEx3.Controls.Add(this.lblNewcarton);
            this.panelEx3.Controls.Add(this.label3);
            this.panelEx3.Controls.Add(this.dgvPartition);
            this.panelEx3.Controls.Add(this.btBack);
            this.panelEx3.Controls.Add(this.btSelect);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(668, 44);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(469, 320);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 16;
            // 
            // lb_partion
            // 
            this.lb_partion.AutoSize = true;
            this.lb_partion.Location = new System.Drawing.Point(492, 7);
            this.lb_partion.Name = "lb_partion";
            this.lb_partion.Size = new System.Drawing.Size(65, 12);
            this.lb_partion.TabIndex = 17;
            this.lb_partion.Text = "lb_partion";
            // 
            // lblNewcarton
            // 
            this.lblNewcarton.AutoSize = true;
            this.lblNewcarton.Location = new System.Drawing.Point(117, 3);
            this.lblNewcarton.Name = "lblNewcarton";
            this.lblNewcarton.Size = new System.Drawing.Size(11, 12);
            this.lblNewcarton.TabIndex = 16;
            this.lblNewcarton.Text = " ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "[拆出箱号]:";
            // 
            // dgvPartition
            // 
            this.dgvPartition.AllowUserToAddRows = false;
            this.dgvPartition.AllowUserToDeleteRows = false;
            this.dgvPartition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartition.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvPartition.Location = new System.Drawing.Point(48, 22);
            this.dgvPartition.Name = "dgvPartition";
            this.dgvPartition.ReadOnly = true;
            this.dgvPartition.RowTemplate.Height = 23;
            this.dgvPartition.ShowEditingIcon = false;
            this.dgvPartition.Size = new System.Drawing.Size(559, 488);
            this.dgvPartition.TabIndex = 14;
            this.dgvPartition.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPartition_CellMouseDoubleClick);
            this.dgvPartition.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvPartition_DataBindingComplete);
            this.dgvPartition.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvPartition_RowPostPaint);
            // 
            // btBack
            // 
            this.btBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btBack.AllowDrop = true;
            this.btBack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btBack.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btBack.Location = new System.Drawing.Point(6, 148);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(36, 26);
            this.btBack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btBack.TabIndex = 2;
            this.btBack.Text = "<---";
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // btSelect
            // 
            this.btSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btSelect.AllowDrop = true;
            this.btSelect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btSelect.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSelect.Location = new System.Drawing.Point(7, 64);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(36, 26);
            this.btSelect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btSelect.TabIndex = 1;
            this.btSelect.Text = "--->";
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.lb_source);
            this.panelEx4.Controls.Add(this.label2);
            this.panelEx4.Controls.Add(this.dgv_showdata);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx4.Location = new System.Drawing.Point(3, 44);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(665, 320);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 15;
            // 
            // lb_source
            // 
            this.lb_source.AutoSize = true;
            this.lb_source.Location = new System.Drawing.Point(565, 3);
            this.lb_source.Name = "lb_source";
            this.lb_source.Size = new System.Drawing.Size(59, 12);
            this.lb_source.TabIndex = 14;
            this.lb_source.Text = "lb_source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "[被拆分箱]:";
            // 
            // dgv_showdata
            // 
            this.dgv_showdata.AllowUserToAddRows = false;
            this.dgv_showdata.AllowUserToDeleteRows = false;
            this.dgv_showdata.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_showdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_showdata.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_showdata.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_showdata.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv_showdata.Location = new System.Drawing.Point(0, 18);
            this.dgv_showdata.Name = "dgv_showdata";
            this.dgv_showdata.ReadOnly = true;
            this.dgv_showdata.RowTemplate.Height = 23;
            this.dgv_showdata.ShowEditingIcon = false;
            this.dgv_showdata.Size = new System.Drawing.Size(662, 492);
            this.dgv_showdata.TabIndex = 12;
            this.dgv_showdata.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_showdata_CellMouseDoubleClick);
            this.dgv_showdata.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_showdata_DataBindingComplete);
            this.dgv_showdata.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_showdata_RowPostPaint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_query);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.cmbdptype);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1134, 41);
            this.panel1.TabIndex = 14;
            // 
            // bt_query
            // 
            this.bt_query.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_query.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_query.Location = new System.Drawing.Point(379, 8);
            this.bt_query.Name = "bt_query";
            this.bt_query.Size = new System.Drawing.Size(43, 23);
            this.bt_query.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_query.TabIndex = 3;
            this.bt_query.Text = "查询";
            this.bt_query.Visible = false;
            this.bt_query.Click += new System.EventHandler(this.bt_query_Click);
            // 
            // txtCode
            // 
            // 
            // 
            // 
            this.txtCode.Border.Class = "TextBoxBorder";
            this.txtCode.Location = new System.Drawing.Point(215, 10);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(145, 21);
            this.txtCode.TabIndex = 2;
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            this.txtCode.MouseLeave += new System.EventHandler(this.txtCode_MouseLeave);
            // 
            // cmbdptype
            // 
            this.cmbdptype.DisplayMember = "Text";
            this.cmbdptype.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbdptype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbdptype.FormattingEnabled = true;
            this.cmbdptype.ItemHeight = 15;
            this.cmbdptype.Location = new System.Drawing.Point(92, 10);
            this.cmbdptype.Name = "cmbdptype";
            this.cmbdptype.Size = new System.Drawing.Size(103, 21);
            this.cmbdptype.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbdptype.TabIndex = 1;
            this.cmbdptype.SelectedIndexChanged += new System.EventHandler(this.cmbdptype_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[拆分类型]:";
            // 
            // 箱栈板合并
            // 
            this.箱栈板合并.Controls.Add(this.panelEx1);
            this.箱栈板合并.Controls.Add(this.panelEx2);
            this.箱栈板合并.Controls.Add(this.panel2);
            this.箱栈板合并.Location = new System.Drawing.Point(4, 22);
            this.箱栈板合并.Name = "箱栈板合并";
            this.箱栈板合并.Padding = new System.Windows.Forms.Padding(3);
            this.箱栈板合并.Size = new System.Drawing.Size(1148, 367);
            this.箱栈板合并.TabIndex = 1;
            this.箱栈板合并.Text = "箱/栈板合并";
            this.箱栈板合并.UseVisualStyleBackColor = true;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.lbltargetloc);
            this.panelEx1.Controls.Add(this.lbltargethouse);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.txtTarget);
            this.panelEx1.Controls.Add(this.lblTarget);
            this.panelEx1.Controls.Add(this.dgvTarget);
            this.panelEx1.Controls.Add(this.bt_combine1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(698, 44);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(447, 320);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 19;
            // 
            // lbltargetloc
            // 
            this.lbltargetloc.AutoSize = true;
            this.lbltargetloc.Location = new System.Drawing.Point(327, 8);
            this.lbltargetloc.Name = "lbltargetloc";
            this.lbltargetloc.Size = new System.Drawing.Size(0, 12);
            this.lbltargetloc.TabIndex = 18;
            // 
            // lbltargethouse
            // 
            this.lbltargethouse.AutoSize = true;
            this.lbltargethouse.Location = new System.Drawing.Point(245, 8);
            this.lbltargethouse.Name = "lbltargethouse";
            this.lbltargethouse.Size = new System.Drawing.Size(0, 12);
            this.lbltargethouse.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(117, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = " ";
            // 
            // txtTarget
            // 
            // 
            // 
            // 
            this.txtTarget.Border.Class = "TextBoxBorder";
            this.txtTarget.Location = new System.Drawing.Point(129, 4);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(105, 21);
            this.txtTarget.TabIndex = 2;
            this.txtTarget.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTarget_KeyPress);
            this.txtTarget.MouseLeave += new System.EventHandler(this.txtTarget_MouseLeave);
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.Location = new System.Drawing.Point(47, 8);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(71, 12);
            this.lblTarget.TabIndex = 15;
            this.lblTarget.Text = "[目标箱号]:";
            // 
            // dgvTarget
            // 
            this.dgvTarget.AllowUserToAddRows = false;
            this.dgvTarget.AllowUserToDeleteRows = false;
            this.dgvTarget.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvTarget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTarget.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvTarget.Location = new System.Drawing.Point(49, 32);
            this.dgvTarget.Name = "dgvTarget";
            this.dgvTarget.ReadOnly = true;
            this.dgvTarget.RowTemplate.Height = 23;
            this.dgvTarget.ShowEditingIcon = false;
            this.dgvTarget.Size = new System.Drawing.Size(519, 478);
            this.dgvTarget.TabIndex = 14;
            this.dgvTarget.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvTarget_RowPostPaint);
            // 
            // bt_combine1
            // 
            this.bt_combine1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_combine1.AllowDrop = true;
            this.bt_combine1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_combine1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_combine1.Location = new System.Drawing.Point(7, 70);
            this.bt_combine1.Name = "bt_combine1";
            this.bt_combine1.Size = new System.Drawing.Size(36, 26);
            this.bt_combine1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_combine1.TabIndex = 1;
            this.bt_combine1.Text = "--->";
            this.bt_combine1.Click += new System.EventHandler(this.bt_combine1_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.lblsourceloc);
            this.panelEx2.Controls.Add(this.lblsourcehouse);
            this.panelEx2.Controls.Add(this.txtSource);
            this.panelEx2.Controls.Add(this.lblSource);
            this.panelEx2.Controls.Add(this.dgvSource);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx2.Location = new System.Drawing.Point(3, 44);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(695, 320);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 18;
            // 
            // lblsourceloc
            // 
            this.lblsourceloc.AutoSize = true;
            this.lblsourceloc.Location = new System.Drawing.Point(287, 8);
            this.lblsourceloc.Name = "lblsourceloc";
            this.lblsourceloc.Size = new System.Drawing.Size(0, 12);
            this.lblsourceloc.TabIndex = 16;
            // 
            // lblsourcehouse
            // 
            this.lblsourcehouse.AutoSize = true;
            this.lblsourcehouse.Location = new System.Drawing.Point(203, 8);
            this.lblsourcehouse.Name = "lblsourcehouse";
            this.lblsourcehouse.Size = new System.Drawing.Size(0, 12);
            this.lblsourcehouse.TabIndex = 15;
            // 
            // txtSource
            // 
            // 
            // 
            // 
            this.txtSource.Border.Class = "TextBoxBorder";
            this.txtSource.Location = new System.Drawing.Point(87, 4);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(103, 21);
            this.txtSource.TabIndex = 14;
            this.txtSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSource_KeyPress);
            this.txtSource.MouseLeave += new System.EventHandler(this.txtSource_MouseLeave);
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(3, 8);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(71, 12);
            this.lblSource.TabIndex = 13;
            this.lblSource.Text = "[被合并箱]:";
            // 
            // dgvSource
            // 
            this.dgvSource.AllowUserToAddRows = false;
            this.dgvSource.AllowUserToDeleteRows = false;
            this.dgvSource.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSource.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSource.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSource.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSource.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvSource.Location = new System.Drawing.Point(0, 34);
            this.dgvSource.Name = "dgvSource";
            this.dgvSource.ReadOnly = true;
            this.dgvSource.RowTemplate.Height = 23;
            this.dgvSource.ShowEditingIcon = false;
            this.dgvSource.Size = new System.Drawing.Size(682, 476);
            this.dgvSource.TabIndex = 12;
            this.dgvSource.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSource_CellMouseDoubleClick);
            this.dgvSource.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvSource_RowPostPaint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbcombine);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1142, 41);
            this.panel2.TabIndex = 17;
            // 
            // cmbcombine
            // 
            this.cmbcombine.DisplayMember = "Text";
            this.cmbcombine.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbcombine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcombine.FormattingEnabled = true;
            this.cmbcombine.ItemHeight = 15;
            this.cmbcombine.Location = new System.Drawing.Point(92, 10);
            this.cmbcombine.Name = "cmbcombine";
            this.cmbcombine.Size = new System.Drawing.Size(103, 21);
            this.cmbcombine.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbcombine.TabIndex = 1;
            this.cmbcombine.SelectedIndexChanged += new System.EventHandler(this.cmbcombine_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "合并类型]:";
            // 
            // DataPartition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 393);
            this.Controls.Add(this.TabDepartation);
            this.Name = "DataPartition";
            this.Text = "箱体拆分&合并";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DataPartition_FormClosed);
            this.Load += new System.EventHandler(this.DataPartition_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.TabDepartation.ResumeLayout(false);
            this.箱栈板拆分.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartition)).EndInit();
            this.panelEx4.ResumeLayout(false);
            this.panelEx4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showdata)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.箱栈板合并.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarget)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyTabControlEx TabDepartation;
        private System.Windows.Forms.TabPage 箱栈板拆分;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.Label lblNewcarton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvPartition;
        private DevComponents.DotNetBar.ButtonX btSelect;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgv_showdata;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX bt_query;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCode;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbdptype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage 箱栈板合并;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.DataGridView dgvTarget;
        private DevComponents.DotNetBar.ButtonX bt_combine1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.DataGridView dgvSource;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTarget;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbcombine;
        private System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSource;
        private System.Windows.Forms.Label lblsourcehouse;
        private System.Windows.Forms.Label lblsourceloc;
        private System.Windows.Forms.Label lbltargethouse;
        private System.Windows.Forms.Label lbltargetloc;
        private DevComponents.DotNetBar.ButtonX btBack;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_printpre;
        private System.Windows.Forms.Label lb_partion;
        private System.Windows.Forms.Label lb_source;

    }
}