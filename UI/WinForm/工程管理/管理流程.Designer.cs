namespace SFIS_V2
{
    partial class ManageRoute
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
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.cbi_selectcondition = new DevComponents.DotNetBar.ComboBoxItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.tbi_value = new DevComponents.DotNetBar.TextBoxItem();
            this.bti_select = new DevComponents.DotNetBar.ButtonItem();
            this.dgv_routemanage = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.routgroupId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bomnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tb_routgroupid = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tb_partnumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_productname = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.bt_save = new DevComponents.DotNetBar.ButtonX();
            this.bt_clear = new DevComponents.DotNetBar.ButtonX();
            this.cb_bomnumber = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.bt_routegroup = new DevComponents.DotNetBar.ButtonX();
            this.btLoadPartnumber = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_routemanage)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.cbi_selectcondition,
            this.tbi_value,
            this.bti_select});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(597, 28);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "查询条件:";
            // 
            // cbi_selectcondition
            // 
            this.cbi_selectcondition.ComboWidth = 100;
            this.cbi_selectcondition.DropDownHeight = 106;
            this.cbi_selectcondition.ItemHeight = 17;
            this.cbi_selectcondition.Items.AddRange(new object[] {
            this.comboItem2,
            this.comboItem3});
            this.cbi_selectcondition.Name = "cbi_selectcondition";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "流程编号";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "成品料号";
            // 
            // tbi_value
            // 
            this.tbi_value.Name = "tbi_value";
            this.tbi_value.TextBoxWidth = 100;
            this.tbi_value.WatermarkColor = System.Drawing.SystemColors.GrayText;
            // 
            // bti_select
            // 
            this.bti_select.Name = "bti_select";
            this.bti_select.SubItemsExpandWidth = 100;
            this.bti_select.Text = "查询";
            this.bti_select.Click += new System.EventHandler(this.bti_select_Click);
            // 
            // dgv_routemanage
            // 
            this.dgv_routemanage.AllowUserToAddRows = false;
            this.dgv_routemanage.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_routemanage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_routemanage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_routemanage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.routgroupId,
            this.partnumber,
            this.productname,
            this.bomnumber});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_routemanage.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_routemanage.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv_routemanage.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_routemanage.Location = new System.Drawing.Point(0, 28);
            this.dgv_routemanage.Name = "dgv_routemanage";
            this.dgv_routemanage.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_routemanage.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_routemanage.RowTemplate.Height = 23;
            this.dgv_routemanage.Size = new System.Drawing.Size(597, 164);
            this.dgv_routemanage.TabIndex = 1;
            this.dgv_routemanage.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_routemanage_CellMouseDoubleClick);
            // 
            // routgroupId
            // 
            this.routgroupId.DataPropertyName = "routgroupId";
            this.routgroupId.HeaderText = "流程编号";
            this.routgroupId.Name = "routgroupId";
            this.routgroupId.ReadOnly = true;
            // 
            // partnumber
            // 
            this.partnumber.DataPropertyName = "partnumber";
            this.partnumber.HeaderText = "成品料号";
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
            // bomnumber
            // 
            this.bomnumber.DataPropertyName = "bomnumber";
            this.bomnumber.HeaderText = "BOM编号";
            this.bomnumber.Name = "bomnumber";
            this.bomnumber.ReadOnly = true;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(11, 207);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(66, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "流程编号:";
            // 
            // tb_routgroupid
            // 
            // 
            // 
            // 
            this.tb_routgroupid.Border.Class = "TextBoxBorder";
            this.tb_routgroupid.Location = new System.Drawing.Point(76, 207);
            this.tb_routgroupid.Name = "tb_routgroupid";
            this.tb_routgroupid.ReadOnly = true;
            this.tb_routgroupid.Size = new System.Drawing.Size(130, 21);
            this.tb_routgroupid.TabIndex = 3;
            this.tb_routgroupid.Leave += new System.EventHandler(this.tb_routgroupid_Leave);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Location = new System.Drawing.Point(290, 207);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(69, 23);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "成品料号:";
            // 
            // tb_partnumber
            // 
            // 
            // 
            // 
            this.tb_partnumber.Border.Class = "TextBoxBorder";
            this.tb_partnumber.Location = new System.Drawing.Point(355, 207);
            this.tb_partnumber.Name = "tb_partnumber";
            this.tb_partnumber.ReadOnly = true;
            this.tb_partnumber.Size = new System.Drawing.Size(126, 21);
            this.tb_partnumber.TabIndex = 5;
            // 
            // tb_productname
            // 
            // 
            // 
            // 
            this.tb_productname.Border.Class = "TextBoxBorder";
            this.tb_productname.Location = new System.Drawing.Point(76, 250);
            this.tb_productname.Name = "tb_productname";
            this.tb_productname.ReadOnly = true;
            this.tb_productname.Size = new System.Drawing.Size(130, 21);
            this.tb_productname.TabIndex = 6;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Location = new System.Drawing.Point(5, 250);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(65, 23);
            this.labelX3.TabIndex = 8;
            this.labelX3.Text = "产品型号:";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.Location = new System.Drawing.Point(290, 252);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(59, 23);
            this.labelX4.TabIndex = 9;
            this.labelX4.Text = "BOM编号:";
            // 
            // bt_save
            // 
            this.bt_save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_save.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_save.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_save.Location = new System.Drawing.Point(131, 293);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(75, 26);
            this.bt_save.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_save.TabIndex = 10;
            this.bt_save.Text = "添加";
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // bt_clear
            // 
            this.bt_clear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_clear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_clear.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_clear.Location = new System.Drawing.Point(305, 293);
            this.bt_clear.Name = "bt_clear";
            this.bt_clear.Size = new System.Drawing.Size(75, 26);
            this.bt_clear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_clear.TabIndex = 11;
            this.bt_clear.Text = "清除";
            this.bt_clear.Click += new System.EventHandler(this.bt_clear_Click);
            // 
            // cb_bomnumber
            // 
            this.cb_bomnumber.DisplayMember = "Text";
            this.cb_bomnumber.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_bomnumber.FormattingEnabled = true;
            this.cb_bomnumber.ItemHeight = 15;
            this.cb_bomnumber.Items.AddRange(new object[] {
            this.comboItem1});
            this.cb_bomnumber.Location = new System.Drawing.Point(356, 252);
            this.cb_bomnumber.Name = "cb_bomnumber";
            this.cb_bomnumber.Size = new System.Drawing.Size(126, 21);
            this.cb_bomnumber.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_bomnumber.TabIndex = 12;
            // 
            // bt_routegroup
            // 
            this.bt_routegroup.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_routegroup.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_routegroup.Location = new System.Drawing.Point(209, 207);
            this.bt_routegroup.Name = "bt_routegroup";
            this.bt_routegroup.Size = new System.Drawing.Size(20, 20);
            this.bt_routegroup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_routegroup.TabIndex = 13;
            this.bt_routegroup.Click += new System.EventHandler(this.bt_routegroup_Click);
            // 
            // btLoadPartnumber
            // 
            this.btLoadPartnumber.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btLoadPartnumber.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btLoadPartnumber.Location = new System.Drawing.Point(484, 207);
            this.btLoadPartnumber.Name = "btLoadPartnumber";
            this.btLoadPartnumber.Size = new System.Drawing.Size(20, 20);
            this.btLoadPartnumber.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btLoadPartnumber.TabIndex = 13;
            this.btLoadPartnumber.Click += new System.EventHandler(this.btLoadPartnumber_Click);
            // 
            // ManageRoute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 337);
            this.Controls.Add(this.btLoadPartnumber);
            this.Controls.Add(this.bt_routegroup);
            this.Controls.Add(this.cb_bomnumber);
            this.Controls.Add(this.bt_clear);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.tb_productname);
            this.Controls.Add(this.tb_partnumber);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.tb_routgroupid);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.dgv_routemanage);
            this.Controls.Add(this.bar1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ManageRoute";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "管理流程";
            this.Load += new System.EventHandler(this.ManageRoute_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_routemanage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_routemanage;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX bt_save;
        private DevComponents.DotNetBar.ButtonX bt_clear;
        private DevComponents.DotNetBar.ComboBoxItem cbi_selectcondition;
        private DevComponents.DotNetBar.TextBoxItem tbi_value;
        private DevComponents.DotNetBar.ButtonItem bti_select;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_bomnumber;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private System.Windows.Forms.DataGridViewTextBoxColumn routgroupId;
        private System.Windows.Forms.DataGridViewTextBoxColumn partnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn productname;
        private System.Windows.Forms.DataGridViewTextBoxColumn bomnumber;
        private DevComponents.DotNetBar.ButtonX bt_routegroup;
        public DevComponents.DotNetBar.Controls.TextBoxX tb_routgroupid;
        private DevComponents.DotNetBar.ButtonX btLoadPartnumber;
        public DevComponents.DotNetBar.Controls.TextBoxX tb_partnumber;
        public DevComponents.DotNetBar.Controls.TextBoxX tb_productname;
    }
}