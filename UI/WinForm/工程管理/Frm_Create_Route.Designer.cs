namespace SFIS_V2
{
    partial class Frm_Create_Route
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv_routeatt = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_routecode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_woinfo = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imbt_select = new DevComponents.DotNetBar.ButtonX();
            this.txt_woid = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.imbt_add = new DevComponents.DotNetBar.ButtonX();
            this.imbt_save = new DevComponents.DotNetBar.ButtonX();
            this.imbt_manageroute = new DevComponents.DotNetBar.ButtonX();
            this.chknum = new System.Windows.Forms.CheckBox();
            this.numUpDown = new System.Windows.Forms.NumericUpDown();
            this.imbt_OK = new DevComponents.DotNetBar.ButtonX();
            this.cb_reflowcraft = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_repaircraft = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_nextcraft = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_craftname = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lb_routedesc = new DevComponents.DotNetBar.LabelX();
            this.lb_routecode = new DevComponents.DotNetBar.LabelX();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgv_routeinfo = new System.Windows.Forms.DataGridView();
            this.SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRAFTNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NEXTCRAFTNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REPAIR_CRAFT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REFLOW_CRAFT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StripMenu_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.imbt_clear = new DevComponents.DotNetBar.ButtonX();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StripMenu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.cbx_SelectFiled = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_routeatt)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_woinfo)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_routeinfo)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1017, 228);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgv_routeatt);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_woinfo);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(1011, 208);
            this.splitContainer1.SplitterDistance = 353;
            this.splitContainer1.TabIndex = 2;
            // 
            // dgv_routeatt
            // 
            this.dgv_routeatt.AllowUserToAddRows = false;
            this.dgv_routeatt.AllowUserToDeleteRows = false;
            this.dgv_routeatt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_routeatt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_routeatt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_routeatt.Location = new System.Drawing.Point(0, 28);
            this.dgv_routeatt.Name = "dgv_routeatt";
            this.dgv_routeatt.ReadOnly = true;
            this.dgv_routeatt.RowHeadersVisible = false;
            this.dgv_routeatt.RowTemplate.Height = 23;
            this.dgv_routeatt.Size = new System.Drawing.Size(353, 180);
            this.dgv_routeatt.TabIndex = 3;
            this.dgv_routeatt.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_routeatt_CellDoubleClick);
            this.dgv_routeatt.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_routeatt_CellMouseDown);
            this.dgv_routeatt.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_routeatt_DataBindingComplete);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbx_SelectFiled);
            this.panel1.Controls.Add(this.tb_routecode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(353, 28);
            this.panel1.TabIndex = 2;
            // 
            // tb_routecode
            // 
            this.tb_routecode.Location = new System.Drawing.Point(135, 3);
            this.tb_routecode.Name = "tb_routecode";
            this.tb_routecode.Size = new System.Drawing.Size(184, 21);
            this.tb_routecode.TabIndex = 1;
            this.tb_routecode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_routecode_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "流程代码:";
            this.label1.Visible = false;
            // 
            // dgv_woinfo
            // 
            this.dgv_woinfo.AllowUserToAddRows = false;
            this.dgv_woinfo.AllowUserToDeleteRows = false;
            this.dgv_woinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_woinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_woinfo.Location = new System.Drawing.Point(0, 28);
            this.dgv_woinfo.Name = "dgv_woinfo";
            this.dgv_woinfo.ReadOnly = true;
            this.dgv_woinfo.RowHeadersVisible = false;
            this.dgv_woinfo.RowTemplate.Height = 23;
            this.dgv_woinfo.Size = new System.Drawing.Size(654, 180);
            this.dgv_woinfo.TabIndex = 4;
            this.dgv_woinfo.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_woinfo_CellMouseDoubleClick);
            this.dgv_woinfo.ColumnHeaderCellChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgv_woinfo_ColumnHeaderCellChanged);
            this.dgv_woinfo.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_woinfo_DataBindingComplete);
            this.dgv_woinfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_woinfo_MouseDoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.imbt_select);
            this.panel2.Controls.Add(this.txt_woid);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(654, 28);
            this.panel2.TabIndex = 3;
            // 
            // imbt_select
            // 
            this.imbt_select.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_select.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_select.Location = new System.Drawing.Point(286, 1);
            this.imbt_select.Name = "imbt_select";
            this.imbt_select.Size = new System.Drawing.Size(75, 23);
            this.imbt_select.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_select.TabIndex = 2;
            this.imbt_select.Text = "选择";
            this.imbt_select.Click += new System.EventHandler(this.imbt_select_Click);
            // 
            // txt_woid
            // 
            this.txt_woid.Location = new System.Drawing.Point(57, 2);
            this.txt_woid.Name = "txt_woid";
            this.txt_woid.Size = new System.Drawing.Size(203, 21);
            this.txt_woid.TabIndex = 1;
            this.txt_woid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_woid_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(3, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "工单:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.chknum);
            this.groupBox2.Controls.Add(this.numUpDown);
            this.groupBox2.Controls.Add(this.imbt_OK);
            this.groupBox2.Controls.Add(this.cb_reflowcraft);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cb_repaircraft);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cb_nextcraft);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cb_craftname);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 228);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(495, 395);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.imbt_add);
            this.groupBox5.Controls.Add(this.imbt_save);
            this.groupBox5.Controls.Add(this.imbt_manageroute);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(3, 95);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(489, 69);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            // 
            // imbt_add
            // 
            this.imbt_add.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_add.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_add.Location = new System.Drawing.Point(26, 20);
            this.imbt_add.Name = "imbt_add";
            this.imbt_add.Size = new System.Drawing.Size(75, 38);
            this.imbt_add.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_add.TabIndex = 3;
            this.imbt_add.Text = "新增途程";
            this.imbt_add.Click += new System.EventHandler(this.imbt_add_Click);
            // 
            // imbt_save
            // 
            this.imbt_save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_save.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_save.Location = new System.Drawing.Point(313, 20);
            this.imbt_save.Name = "imbt_save";
            this.imbt_save.Size = new System.Drawing.Size(82, 38);
            this.imbt_save.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_save.TabIndex = 1;
            this.imbt_save.Text = "保存";
            this.imbt_save.Click += new System.EventHandler(this.imbt_save_Click);
            // 
            // imbt_manageroute
            // 
            this.imbt_manageroute.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_manageroute.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_manageroute.Location = new System.Drawing.Point(164, 20);
            this.imbt_manageroute.Name = "imbt_manageroute";
            this.imbt_manageroute.Size = new System.Drawing.Size(75, 38);
            this.imbt_manageroute.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_manageroute.TabIndex = 3;
            this.imbt_manageroute.Text = "管理途程";
            this.imbt_manageroute.Click += new System.EventHandler(this.imbt_manageroute_Click);
            // 
            // chknum
            // 
            this.chknum.AutoSize = true;
            this.chknum.Location = new System.Drawing.Point(115, 179);
            this.chknum.Name = "chknum";
            this.chknum.Size = new System.Drawing.Size(48, 16);
            this.chknum.TabIndex = 8;
            this.chknum.Text = "序号";
            this.chknum.UseVisualStyleBackColor = true;
            this.chknum.Click += new System.EventHandler(this.chknum_Click);
            // 
            // numUpDown
            // 
            this.numUpDown.Enabled = false;
            this.numUpDown.Location = new System.Drawing.Point(181, 174);
            this.numUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDown.Name = "numUpDown";
            this.numUpDown.Size = new System.Drawing.Size(61, 21);
            this.numUpDown.TabIndex = 7;
            this.numUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // imbt_OK
            // 
            this.imbt_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_OK.Location = new System.Drawing.Point(346, 209);
            this.imbt_OK.Name = "imbt_OK";
            this.imbt_OK.Size = new System.Drawing.Size(75, 23);
            this.imbt_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_OK.TabIndex = 6;
            this.imbt_OK.Text = "====>";
            this.imbt_OK.Click += new System.EventHandler(this.imbt_OK_Click);
            // 
            // cb_reflowcraft
            // 
            this.cb_reflowcraft.DisplayMember = "Text";
            this.cb_reflowcraft.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cb_reflowcraft.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_reflowcraft.FormattingEnabled = true;
            this.cb_reflowcraft.ItemHeight = 20;
            this.cb_reflowcraft.Location = new System.Drawing.Point(115, 338);
            this.cb_reflowcraft.Name = "cb_reflowcraft";
            this.cb_reflowcraft.Size = new System.Drawing.Size(207, 26);
            this.cb_reflowcraft.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_reflowcraft.TabIndex = 5;
            this.cb_reflowcraft.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cb_reflowcraft_DrawItem);
            this.cb_reflowcraft.TextUpdate += new System.EventHandler(this.cb_reflowcraft_TextUpdate);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(29, 338);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "回流途程:";
            // 
            // cb_repaircraft
            // 
            this.cb_repaircraft.DisplayMember = "Text";
            this.cb_repaircraft.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cb_repaircraft.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_repaircraft.FormattingEnabled = true;
            this.cb_repaircraft.ItemHeight = 20;
            this.cb_repaircraft.Location = new System.Drawing.Point(115, 295);
            this.cb_repaircraft.Name = "cb_repaircraft";
            this.cb_repaircraft.Size = new System.Drawing.Size(207, 26);
            this.cb_repaircraft.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_repaircraft.TabIndex = 5;
            this.cb_repaircraft.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cb_repaircraft_DrawItem);
            this.cb_repaircraft.TextUpdate += new System.EventHandler(this.cb_repaircraft_TextUpdate);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(29, 300);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "维修途程:";
            // 
            // cb_nextcraft
            // 
            this.cb_nextcraft.DisplayMember = "Text";
            this.cb_nextcraft.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cb_nextcraft.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_nextcraft.FormattingEnabled = true;
            this.cb_nextcraft.ItemHeight = 20;
            this.cb_nextcraft.Location = new System.Drawing.Point(115, 250);
            this.cb_nextcraft.Name = "cb_nextcraft";
            this.cb_nextcraft.Size = new System.Drawing.Size(207, 26);
            this.cb_nextcraft.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_nextcraft.TabIndex = 5;
            this.cb_nextcraft.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cb_nextcraft_DrawItem);
            this.cb_nextcraft.TextUpdate += new System.EventHandler(this.cb_nextcraft_TextUpdate);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(29, 260);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "下一途程:";
            // 
            // cb_craftname
            // 
            this.cb_craftname.DisplayMember = "Text";
            this.cb_craftname.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cb_craftname.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_craftname.FormattingEnabled = true;
            this.cb_craftname.ItemHeight = 20;
            this.cb_craftname.Location = new System.Drawing.Point(115, 211);
            this.cb_craftname.Name = "cb_craftname";
            this.cb_craftname.Size = new System.Drawing.Size(207, 26);
            this.cb_craftname.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_craftname.TabIndex = 5;
            this.cb_craftname.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cb_craftname_DrawItem);
            this.cb_craftname.SelectedIndexChanged += new System.EventHandler(this.cb_craftname_SelectedIndexChanged);
            this.cb_craftname.TextUpdate += new System.EventHandler(this.cb_craftname_TextUpdate);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(29, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "当前途程:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lb_routedesc);
            this.groupBox4.Controls.Add(this.lb_routecode);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(489, 78);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // lb_routedesc
            // 
            // 
            // 
            // 
            this.lb_routedesc.BackgroundStyle.Class = "";
            this.lb_routedesc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_routedesc.Location = new System.Drawing.Point(90, 50);
            this.lb_routedesc.Name = "lb_routedesc";
            this.lb_routedesc.Size = new System.Drawing.Size(374, 23);
            this.lb_routedesc.TabIndex = 1;
            this.lb_routedesc.Text = "NA";
            // 
            // lb_routecode
            // 
            // 
            // 
            // 
            this.lb_routecode.BackgroundStyle.Class = "";
            this.lb_routecode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_routecode.Location = new System.Drawing.Point(89, 20);
            this.lb_routecode.Name = "lb_routecode";
            this.lb_routecode.Size = new System.Drawing.Size(374, 23);
            this.lb_routecode.TabIndex = 1;
            this.lb_routecode.Text = "NA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(8, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "途程代码:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(8, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "途程描述:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgv_routeinfo);
            this.groupBox3.Controls.Add(this.panelEx1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(495, 228);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(522, 395);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "途程显示";
            // 
            // dgv_routeinfo
            // 
            this.dgv_routeinfo.AllowUserToAddRows = false;
            this.dgv_routeinfo.AllowUserToDeleteRows = false;
            this.dgv_routeinfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_routeinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_routeinfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SEQ,
            this.CRAFTNAME,
            this.NEXTCRAFTNAME,
            this.REPAIR_CRAFT,
            this.REFLOW_CRAFT});
            this.dgv_routeinfo.ContextMenuStrip = this.contextMenuStrip1;
            this.dgv_routeinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_routeinfo.Location = new System.Drawing.Point(3, 17);
            this.dgv_routeinfo.Name = "dgv_routeinfo";
            this.dgv_routeinfo.ReadOnly = true;
            this.dgv_routeinfo.RowTemplate.Height = 23;
            this.dgv_routeinfo.Size = new System.Drawing.Size(516, 312);
            this.dgv_routeinfo.TabIndex = 0;
            this.dgv_routeinfo.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_routeinfo_CellMouseDoubleClick);
            this.dgv_routeinfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_routeinfo_MouseDoubleClick);
            // 
            // SEQ
            // 
            this.SEQ.HeaderText = "序号";
            this.SEQ.Name = "SEQ";
            this.SEQ.ReadOnly = true;
            this.SEQ.Width = 54;
            // 
            // CRAFTNAME
            // 
            this.CRAFTNAME.HeaderText = "当前途程";
            this.CRAFTNAME.Name = "CRAFTNAME";
            this.CRAFTNAME.ReadOnly = true;
            this.CRAFTNAME.Width = 78;
            // 
            // NEXTCRAFTNAME
            // 
            this.NEXTCRAFTNAME.HeaderText = "下一途程";
            this.NEXTCRAFTNAME.Name = "NEXTCRAFTNAME";
            this.NEXTCRAFTNAME.ReadOnly = true;
            this.NEXTCRAFTNAME.Width = 78;
            // 
            // REPAIR_CRAFT
            // 
            this.REPAIR_CRAFT.HeaderText = "维修途程";
            this.REPAIR_CRAFT.Name = "REPAIR_CRAFT";
            this.REPAIR_CRAFT.ReadOnly = true;
            this.REPAIR_CRAFT.Width = 78;
            // 
            // REFLOW_CRAFT
            // 
            this.REFLOW_CRAFT.HeaderText = "回流途程";
            this.REFLOW_CRAFT.Name = "REFLOW_CRAFT";
            this.REFLOW_CRAFT.ReadOnly = true;
            this.REFLOW_CRAFT.Width = 78;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMenu_Clear});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // StripMenu_Clear
            // 
            this.StripMenu_Clear.Name = "StripMenu_Clear";
            this.StripMenu_Clear.Size = new System.Drawing.Size(100, 22);
            this.StripMenu_Clear.Text = "清除";
            this.StripMenu_Clear.Click += new System.EventHandler(this.StripMenu_Clear_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.imbt_clear);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx1.Location = new System.Drawing.Point(3, 329);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(516, 63);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            // 
            // imbt_clear
            // 
            this.imbt_clear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_clear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_clear.Location = new System.Drawing.Point(55, 18);
            this.imbt_clear.Name = "imbt_clear";
            this.imbt_clear.Size = new System.Drawing.Size(75, 23);
            this.imbt_clear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_clear.TabIndex = 0;
            this.imbt_clear.Text = "清除";
            this.imbt_clear.Visible = false;
            this.imbt_clear.Click += new System.EventHandler(this.imbt_clear_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMenu_Delete});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(101, 26);
            // 
            // StripMenu_Delete
            // 
            this.StripMenu_Delete.Name = "StripMenu_Delete";
            this.StripMenu_Delete.Size = new System.Drawing.Size(100, 22);
            this.StripMenu_Delete.Text = "删除";
            this.StripMenu_Delete.Click += new System.EventHandler(this.StripMenu_Delete_Click);
            // 
            // cbx_SelectFiled
            // 
            this.cbx_SelectFiled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_SelectFiled.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_SelectFiled.FormattingEnabled = true;
            this.cbx_SelectFiled.Items.AddRange(new object[] {
            "途程代码",
            "途程描述"});
            this.cbx_SelectFiled.Location = new System.Drawing.Point(14, 2);
            this.cbx_SelectFiled.Name = "cbx_SelectFiled";
            this.cbx_SelectFiled.Size = new System.Drawing.Size(111, 22);
            this.cbx_SelectFiled.TabIndex = 2;
            // 
            // Frm_Create_Route
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 623);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Create_Route";
            this.Text = "制定生产工序";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Create_Route_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Create_Route_Load);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_routeatt)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_woinfo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_routeinfo)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgv_routeinfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevComponents.DotNetBar.ButtonX imbt_add;
        public DevComponents.DotNetBar.LabelX lb_routedesc;
        public DevComponents.DotNetBar.LabelX lb_routecode;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_reflowcraft;
        private System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_repaircraft;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_nextcraft;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_craftname;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.ButtonX imbt_OK;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX imbt_clear;
        private DevComponents.DotNetBar.ButtonX imbt_save;
        private DevComponents.DotNetBar.ButtonX imbt_manageroute;
        private System.Windows.Forms.CheckBox chknum;
        private System.Windows.Forms.NumericUpDown numUpDown;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv_routeatt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_routecode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_woinfo;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ButtonX imbt_select;
        private System.Windows.Forms.TextBox txt_woid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRAFTNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn NEXTCRAFTNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn REPAIR_CRAFT;
        private System.Windows.Forms.DataGridViewTextBoxColumn REFLOW_CRAFT;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem StripMenu_Clear;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem StripMenu_Delete;
        private System.Windows.Forms.ComboBox cbx_SelectFiled;
    }
}