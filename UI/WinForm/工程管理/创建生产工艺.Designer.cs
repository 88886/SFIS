namespace SFIS_V2
{
    partial class CreateCraft
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_craftid = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt_craftname = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_qutrycraft = new DevComponents.DotNetBar.ButtonX();
            this.tb_qutrycraftid = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label4 = new System.Windows.Forms.Label();
            this.bt_savecraft = new DevComponents.DotNetBar.ButtonX();
            this.dgv_addcraftitem = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.craid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CraftItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CraftItemDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemParmetUpperLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CraftItemParmetLowerLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CraftItemParametOther = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_showcraftitem = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.craftId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.craftname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.craftparameterurl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.beworkseg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TESTFLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checktoolsflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bt_getcarfttId = new DevComponents.DotNetBar.ButtonX();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txt_testflag = new System.Windows.Forms.TextBox();
            this.cbx_checktoolsflag = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbx_beworkseg = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txt_CRAFTPARAMETERURL = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_addcraftitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showcraftitem)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[工艺编号]:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "[工艺名称]:";
            // 
            // txt_craftid
            // 
            // 
            // 
            // 
            this.txt_craftid.Border.Class = "TextBoxBorder";
            this.txt_craftid.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_craftid.Location = new System.Drawing.Point(16, 27);
            this.txt_craftid.Name = "txt_craftid";
            this.txt_craftid.ReadOnly = true;
            this.txt_craftid.Size = new System.Drawing.Size(111, 21);
            this.txt_craftid.TabIndex = 1;
            // 
            // txt_craftname
            // 
            // 
            // 
            // 
            this.txt_craftname.Border.Class = "TextBoxBorder";
            this.txt_craftname.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_craftname.Location = new System.Drawing.Point(171, 26);
            this.txt_craftname.Name = "txt_craftname";
            this.txt_craftname.Size = new System.Drawing.Size(101, 21);
            this.txt_craftname.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txt_craftname, "填写工艺名称(不超过10个字符)");
            this.txt_craftname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_craftdesc_KeyDown);
            this.txt_craftname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_craftdesc_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_qutrycraft);
            this.panel1.Controls.Add(this.tb_qutrycraftid);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1075, 27);
            this.panel1.TabIndex = 2;
            // 
            // bt_qutrycraft
            // 
            this.bt_qutrycraft.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_qutrycraft.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_qutrycraft.Location = new System.Drawing.Point(299, 4);
            this.bt_qutrycraft.Name = "bt_qutrycraft";
            this.bt_qutrycraft.Size = new System.Drawing.Size(47, 19);
            this.bt_qutrycraft.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_qutrycraft.TabIndex = 2;
            this.bt_qutrycraft.Text = "查询";
            this.bt_qutrycraft.Click += new System.EventHandler(this.bt_qutrycraft_Click);
            // 
            // tb_qutrycraftid
            // 
            // 
            // 
            // 
            this.tb_qutrycraftid.Border.Class = "TextBoxBorder";
            this.tb_qutrycraftid.Location = new System.Drawing.Point(137, 3);
            this.tb_qutrycraftid.Name = "tb_qutrycraftid";
            this.tb_qutrycraftid.Size = new System.Drawing.Size(149, 21);
            this.tb_qutrycraftid.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "[现有的生产工艺]:";
            // 
            // bt_savecraft
            // 
            this.bt_savecraft.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_savecraft.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_savecraft.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_savecraft.Location = new System.Drawing.Point(22, 110);
            this.bt_savecraft.Name = "bt_savecraft";
            this.bt_savecraft.Size = new System.Drawing.Size(517, 26);
            this.bt_savecraft.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_savecraft.TabIndex = 2;
            this.bt_savecraft.Text = "保 存  工 艺";
            this.bt_savecraft.Click += new System.EventHandler(this.bt_savecraft_Click);
            // 
            // dgv_addcraftitem
            // 
            this.dgv_addcraftitem.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_addcraftitem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_addcraftitem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_addcraftitem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.craid,
            this.CraftItem,
            this.CraftItemDesc,
            this.ItemParmetUpperLimit,
            this.CraftItemParmetLowerLimit,
            this.CraftItemParametOther});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_addcraftitem.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_addcraftitem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_addcraftitem.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_addcraftitem.Location = new System.Drawing.Point(0, 0);
            this.dgv_addcraftitem.Name = "dgv_addcraftitem";
            this.dgv_addcraftitem.RowTemplate.Height = 23;
            this.dgv_addcraftitem.Size = new System.Drawing.Size(1075, 121);
            this.dgv_addcraftitem.TabIndex = 0;
            this.dgv_addcraftitem.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_addcraftitem_CellValueChanged);
            this.dgv_addcraftitem.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_addcraftitem_RowsAdded);
            this.dgv_addcraftitem.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgv_addcraftitem_RowsRemoved);
            this.dgv_addcraftitem.MouseEnter += new System.EventHandler(this.dgv_addcraftitem_MouseEnter);
            this.dgv_addcraftitem.MouseLeave += new System.EventHandler(this.dgv_addcraftitem_MouseLeave);
            // 
            // craid
            // 
            this.craid.DataPropertyName = "craftId";
            this.craid.HeaderText = "craftID";
            this.craid.Name = "craid";
            this.craid.Visible = false;
            // 
            // CraftItem
            // 
            this.CraftItem.DataPropertyName = "craftItem";
            this.CraftItem.HeaderText = "工艺项目号";
            this.CraftItem.Name = "CraftItem";
            this.CraftItem.ReadOnly = true;
            this.CraftItem.Width = 90;
            // 
            // CraftItemDesc
            // 
            this.CraftItemDesc.DataPropertyName = "craftparameterdes";
            this.CraftItemDesc.HeaderText = "项目描述";
            this.CraftItemDesc.Name = "CraftItemDesc";
            // 
            // ItemParmetUpperLimit
            // 
            this.ItemParmetUpperLimit.DataPropertyName = "upperlimit";
            this.ItemParmetUpperLimit.HeaderText = "项目参数上限";
            this.ItemParmetUpperLimit.Name = "ItemParmetUpperLimit";
            this.ItemParmetUpperLimit.Width = 102;
            // 
            // CraftItemParmetLowerLimit
            // 
            this.CraftItemParmetLowerLimit.DataPropertyName = "lowerlimit";
            this.CraftItemParmetLowerLimit.HeaderText = "项目参数下限";
            this.CraftItemParmetLowerLimit.Name = "CraftItemParmetLowerLimit";
            this.CraftItemParmetLowerLimit.Width = 102;
            // 
            // CraftItemParametOther
            // 
            this.CraftItemParametOther.DataPropertyName = "other";
            this.CraftItemParametOther.HeaderText = "其他参数";
            this.CraftItemParametOther.Name = "CraftItemParametOther";
            this.CraftItemParametOther.Width = 78;
            // 
            // dgv_showcraftitem
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_showcraftitem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_showcraftitem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_showcraftitem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.craftId,
            this.craftname,
            this.craftparameterurl,
            this.beworkseg,
            this.TESTFLAG,
            this.checktoolsflag});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_showcraftitem.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_showcraftitem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_showcraftitem.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_showcraftitem.Location = new System.Drawing.Point(0, 0);
            this.dgv_showcraftitem.Name = "dgv_showcraftitem";
            this.dgv_showcraftitem.ReadOnly = true;
            this.dgv_showcraftitem.RowTemplate.Height = 23;
            this.dgv_showcraftitem.Size = new System.Drawing.Size(1075, 191);
            this.dgv_showcraftitem.TabIndex = 0;
            this.dgv_showcraftitem.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_showcraftitem_CellMouseClick);
            // 
            // craftId
            // 
            this.craftId.DataPropertyName = "craftId";
            this.craftId.HeaderText = "工艺编号";
            this.craftId.Name = "craftId";
            this.craftId.ReadOnly = true;
            // 
            // craftname
            // 
            this.craftname.DataPropertyName = "craftname";
            this.craftname.HeaderText = "工艺名称";
            this.craftname.Name = "craftname";
            this.craftname.ReadOnly = true;
            this.craftname.Width = 150;
            // 
            // craftparameterurl
            // 
            this.craftparameterurl.DataPropertyName = "craftparameterurl";
            this.craftparameterurl.HeaderText = "工艺描述";
            this.craftparameterurl.Name = "craftparameterurl";
            this.craftparameterurl.ReadOnly = true;
            this.craftparameterurl.Width = 150;
            // 
            // beworkseg
            // 
            this.beworkseg.DataPropertyName = "beworkseg";
            this.beworkseg.HeaderText = "所属制程段";
            this.beworkseg.Name = "beworkseg";
            this.beworkseg.ReadOnly = true;
            // 
            // TESTFLAG
            // 
            this.TESTFLAG.DataPropertyName = "TESTFLAG";
            this.TESTFLAG.HeaderText = "工艺标记";
            this.TESTFLAG.Name = "TESTFLAG";
            this.TESTFLAG.ReadOnly = true;
            // 
            // checktoolsflag
            // 
            this.checktoolsflag.DataPropertyName = "checktoolsflag";
            this.checktoolsflag.HeaderText = "是否使用治具";
            this.checktoolsflag.Name = "checktoolsflag";
            this.checktoolsflag.ReadOnly = true;
            // 
            // bt_getcarfttId
            // 
            this.bt_getcarfttId.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_getcarfttId.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_getcarfttId.Location = new System.Drawing.Point(130, 29);
            this.bt_getcarfttId.Name = "bt_getcarfttId";
            this.bt_getcarfttId.Size = new System.Drawing.Size(19, 18);
            this.bt_getcarfttId.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_getcarfttId.TabIndex = 3;
            this.bt_getcarfttId.Tooltip = "点击获取工艺编号";
            this.bt_getcarfttId.Click += new System.EventHandler(this.bt_getcarfttId_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "[编辑添加工艺项目参数]:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgv_showcraftitem);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1075, 191);
            this.panel3.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txt_testflag);
            this.panel5.Controls.Add(this.cbx_checktoolsflag);
            this.panel5.Controls.Add(this.cbx_beworkseg);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.txt_CRAFTPARAMETERURL);
            this.panel5.Controls.Add(this.txt_craftname);
            this.panel5.Controls.Add(this.bt_getcarfttId);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.bt_savecraft);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.txt_craftid);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 218);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1075, 164);
            this.panel5.TabIndex = 9;
            // 
            // txt_testflag
            // 
            this.txt_testflag.Location = new System.Drawing.Point(296, 79);
            this.txt_testflag.Name = "txt_testflag";
            this.txt_testflag.Size = new System.Drawing.Size(224, 21);
            this.txt_testflag.TabIndex = 5;
            this.txt_testflag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_craftflag_KeyDown);
            this.txt_testflag.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_craftflag_KeyPress);
            // 
            // cbx_checktoolsflag
            // 
            this.cbx_checktoolsflag.DisplayMember = "Text";
            this.cbx_checktoolsflag.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbx_checktoolsflag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_checktoolsflag.FormattingEnabled = true;
            this.cbx_checktoolsflag.ItemHeight = 15;
            this.cbx_checktoolsflag.Location = new System.Drawing.Point(424, 25);
            this.cbx_checktoolsflag.Name = "cbx_checktoolsflag";
            this.cbx_checktoolsflag.Size = new System.Drawing.Size(105, 21);
            this.cbx_checktoolsflag.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbx_checktoolsflag.TabIndex = 4;
            this.cbx_checktoolsflag.Leave += new System.EventHandler(this.cb_bworkseg_Leave);
            // 
            // cbx_beworkseg
            // 
            this.cbx_beworkseg.DisplayMember = "Text";
            this.cbx_beworkseg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbx_beworkseg.FormattingEnabled = true;
            this.cbx_beworkseg.ItemHeight = 15;
            this.cbx_beworkseg.Location = new System.Drawing.Point(298, 26);
            this.cbx_beworkseg.Name = "cbx_beworkseg";
            this.cbx_beworkseg.Size = new System.Drawing.Size(105, 21);
            this.cbx_beworkseg.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbx_beworkseg.TabIndex = 4;
            this.toolTip1.SetToolTip(this.cbx_beworkseg, "选择制程段(SMT、插件、测试、组装、包装)");
            this.cbx_beworkseg.Leave += new System.EventHandler(this.cb_bworkseg_Leave);
            // 
            // txt_CRAFTPARAMETERURL
            // 
            // 
            // 
            // 
            this.txt_CRAFTPARAMETERURL.Border.Class = "TextBoxBorder";
            this.txt_CRAFTPARAMETERURL.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_CRAFTPARAMETERURL.Location = new System.Drawing.Point(14, 78);
            this.txt_CRAFTPARAMETERURL.Name = "txt_CRAFTPARAMETERURL";
            this.txt_CRAFTPARAMETERURL.Size = new System.Drawing.Size(253, 21);
            this.txt_CRAFTPARAMETERURL.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(294, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(323, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "[途程标记 (0:非测试站,1测试站,2维修站,3测试+非测试)]:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "[工艺描述]:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(295, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "[制程段]:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(422, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "[是否使用治具]:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgv_addcraftitem);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 382);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1075, 121);
            this.panel4.TabIndex = 10;
            // 
            // CreateCraft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 503);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CreateCraft";
            this.Text = "添加生产工艺 参数";
            this.Load += new System.EventHandler(this.CreateCraft_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_addcraftitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showcraftitem)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_craftid;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_craftname;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX bt_savecraft;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_addcraftitem;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_showcraftitem;
        private DevComponents.DotNetBar.ButtonX bt_getcarfttId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_CRAFTPARAMETERURL;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.ButtonX bt_qutrycraft;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_qutrycraftid;
        private System.Windows.Forms.DataGridViewTextBoxColumn craid;
        private System.Windows.Forms.DataGridViewTextBoxColumn CraftItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn CraftItemDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemParmetUpperLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn CraftItemParmetLowerLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn CraftItemParametOther;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbx_beworkseg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txt_testflag;
        private System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbx_checktoolsflag;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn craftId;
        private System.Windows.Forms.DataGridViewTextBoxColumn craftname;
        private System.Windows.Forms.DataGridViewTextBoxColumn craftparameterurl;
        private System.Windows.Forms.DataGridViewTextBoxColumn beworkseg;
        private System.Windows.Forms.DataGridViewTextBoxColumn TESTFLAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn checktoolsflag;
    }
}