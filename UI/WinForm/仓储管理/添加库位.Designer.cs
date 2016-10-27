namespace SFIS_V2
{
    partial class FrmWarehouseManage
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
            this.bt_checkloc = new DevComponents.DotNetBar.ButtonX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_number = new System.Windows.Forms.TextBox();
            this.bt_select = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_selcondition = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bt_updateinfo = new DevComponents.DotNetBar.ButtonX();
            this.bt_clearinfo = new DevComponents.DotNetBar.ButtonX();
            this.bt_deleteinfo = new DevComponents.DotNetBar.ButtonX();
            this.cb_loctype = new System.Windows.Forms.ComboBox();
            this.cb_warehouse = new System.Windows.Forms.ComboBox();
            this.bt_saveinfo = new DevComponents.DotNetBar.ButtonX();
            this.tb_remark = new System.Windows.Forms.TextBox();
            this.tb_loctotal = new System.Windows.Forms.TextBox();
            this.tb_locdesc = new System.Windows.Forms.TextBox();
            this.tb_uplocid = new System.Windows.Forms.TextBox();
            this.tb_locid = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgv_warehouseinfo = new System.Windows.Forms.DataGridView();
            this.locId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loctype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storehouseId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loctotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uplocId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_warehouseinfo)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_checkloc
            // 
            this.bt_checkloc.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_checkloc.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_checkloc.Location = new System.Drawing.Point(191, 51);
            this.bt_checkloc.Name = "bt_checkloc";
            this.bt_checkloc.Size = new System.Drawing.Size(57, 23);
            this.bt_checkloc.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_checkloc.TabIndex = 0;
            this.bt_checkloc.Text = "检查库位";
            this.bt_checkloc.Click += new System.EventHandler(this.bt_checkloc_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tb_number);
            this.panel1.Controls.Add(this.bt_select);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cb_selcondition);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(721, 33);
            this.panel1.TabIndex = 1;
            // 
            // tb_number
            // 
            this.tb_number.Location = new System.Drawing.Point(175, 7);
            this.tb_number.Name = "tb_number";
            this.tb_number.Size = new System.Drawing.Size(100, 21);
            this.tb_number.TabIndex = 4;
            // 
            // bt_select
            // 
            this.bt_select.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_select.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_select.Location = new System.Drawing.Point(296, 7);
            this.bt_select.Name = "bt_select";
            this.bt_select.Size = new System.Drawing.Size(75, 23);
            this.bt_select.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_select.TabIndex = 3;
            this.bt_select.Text = "查询";
            this.bt_select.Click += new System.EventHandler(this.bt_select_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "查询条件";
            // 
            // cb_selcondition
            // 
            this.cb_selcondition.FormattingEnabled = true;
            this.cb_selcondition.Items.AddRange(new object[] {
            "库位编号",
            "仓库编号"});
            this.cb_selcondition.Location = new System.Drawing.Point(83, 7);
            this.cb_selcondition.Name = "cb_selcondition";
            this.cb_selcondition.Size = new System.Drawing.Size(75, 20);
            this.cb_selcondition.TabIndex = 1;
            this.cb_selcondition.Text = "库位编号";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bt_updateinfo);
            this.panel2.Controls.Add(this.bt_clearinfo);
            this.panel2.Controls.Add(this.bt_deleteinfo);
            this.panel2.Controls.Add(this.cb_loctype);
            this.panel2.Controls.Add(this.cb_warehouse);
            this.panel2.Controls.Add(this.bt_saveinfo);
            this.panel2.Controls.Add(this.tb_remark);
            this.panel2.Controls.Add(this.tb_loctotal);
            this.panel2.Controls.Add(this.tb_locdesc);
            this.panel2.Controls.Add(this.tb_uplocid);
            this.panel2.Controls.Add(this.tb_locid);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.bt_checkloc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 295);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(721, 184);
            this.panel2.TabIndex = 3;
            // 
            // bt_updateinfo
            // 
            this.bt_updateinfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_updateinfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_updateinfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_updateinfo.Location = new System.Drawing.Point(200, 134);
            this.bt_updateinfo.Name = "bt_updateinfo";
            this.bt_updateinfo.Size = new System.Drawing.Size(75, 32);
            this.bt_updateinfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_updateinfo.TabIndex = 25;
            this.bt_updateinfo.Text = "更新";
            this.bt_updateinfo.Click += new System.EventHandler(this.bt_updateinfo_Click);
            // 
            // bt_clearinfo
            // 
            this.bt_clearinfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_clearinfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_clearinfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_clearinfo.Location = new System.Drawing.Point(438, 134);
            this.bt_clearinfo.Name = "bt_clearinfo";
            this.bt_clearinfo.Size = new System.Drawing.Size(75, 32);
            this.bt_clearinfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_clearinfo.TabIndex = 24;
            this.bt_clearinfo.Text = "清除";
            this.bt_clearinfo.Click += new System.EventHandler(this.bt_clearinfo_Click);
            // 
            // bt_deleteinfo
            // 
            this.bt_deleteinfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_deleteinfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_deleteinfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_deleteinfo.Location = new System.Drawing.Point(315, 134);
            this.bt_deleteinfo.Name = "bt_deleteinfo";
            this.bt_deleteinfo.Size = new System.Drawing.Size(75, 32);
            this.bt_deleteinfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_deleteinfo.TabIndex = 23;
            this.bt_deleteinfo.Text = "删除";
            this.bt_deleteinfo.Click += new System.EventHandler(this.bt_deleteinfo_Click);
            // 
            // cb_loctype
            // 
            this.cb_loctype.FormattingEnabled = true;
            this.cb_loctype.Location = new System.Drawing.Point(324, 26);
            this.cb_loctype.Name = "cb_loctype";
            this.cb_loctype.Size = new System.Drawing.Size(100, 20);
            this.cb_loctype.TabIndex = 22;
            this.cb_loctype.Leave += new System.EventHandler(this.cb_loctype_Leave);
            // 
            // cb_warehouse
            // 
            this.cb_warehouse.FormattingEnabled = true;
            this.cb_warehouse.Location = new System.Drawing.Point(83, 22);
            this.cb_warehouse.Name = "cb_warehouse";
            this.cb_warehouse.Size = new System.Drawing.Size(99, 20);
            this.cb_warehouse.TabIndex = 20;
            this.cb_warehouse.Leave += new System.EventHandler(this.cb_warehouse_Leave);
            // 
            // bt_saveinfo
            // 
            this.bt_saveinfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_saveinfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_saveinfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_saveinfo.Location = new System.Drawing.Point(83, 134);
            this.bt_saveinfo.Name = "bt_saveinfo";
            this.bt_saveinfo.Size = new System.Drawing.Size(75, 32);
            this.bt_saveinfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_saveinfo.TabIndex = 18;
            this.bt_saveinfo.Text = "保存";
            this.bt_saveinfo.Click += new System.EventHandler(this.bt_saveinfo_Click);
            // 
            // tb_remark
            // 
            this.tb_remark.Location = new System.Drawing.Point(84, 95);
            this.tb_remark.Name = "tb_remark";
            this.tb_remark.Size = new System.Drawing.Size(525, 21);
            this.tb_remark.TabIndex = 14;
            // 
            // tb_loctotal
            // 
            this.tb_loctotal.Location = new System.Drawing.Point(508, 26);
            this.tb_loctotal.Name = "tb_loctotal";
            this.tb_loctotal.Size = new System.Drawing.Size(100, 21);
            this.tb_loctotal.TabIndex = 13;
            this.tb_loctotal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_loctotal_KeyPress);
            // 
            // tb_locdesc
            // 
            this.tb_locdesc.Location = new System.Drawing.Point(324, 57);
            this.tb_locdesc.Name = "tb_locdesc";
            this.tb_locdesc.Size = new System.Drawing.Size(100, 21);
            this.tb_locdesc.TabIndex = 12;
            // 
            // tb_uplocid
            // 
            this.tb_uplocid.Location = new System.Drawing.Point(508, 57);
            this.tb_uplocid.Name = "tb_uplocid";
            this.tb_uplocid.Size = new System.Drawing.Size(100, 21);
            this.tb_uplocid.TabIndex = 9;
            // 
            // tb_locid
            // 
            this.tb_locid.Location = new System.Drawing.Point(82, 51);
            this.tb_locid.Name = "tb_locid";
            this.tb_locid.Size = new System.Drawing.Size(100, 21);
            this.tb_locid.TabIndex = 8;
            this.tb_locid.Leave += new System.EventHandler(this.tb_locid_Leave);
            this.tb_locid.Enter += new System.EventHandler(this.tb_locid_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "备注";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(436, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "库位容量";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(267, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "库位描述";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "所属仓库";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(265, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "库位类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(436, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "上级库位";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "库位编号";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgv_warehouseinfo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(721, 262);
            this.panel3.TabIndex = 4;
            // 
            // dgv_warehouseinfo
            // 
            this.dgv_warehouseinfo.AllowUserToAddRows = false;
            this.dgv_warehouseinfo.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_warehouseinfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_warehouseinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_warehouseinfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.locId,
            this.loctype,
            this.storehouseId,
            this.loctotal,
            this.locdesc,
            this.uplocId,
            this.remark});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_warehouseinfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_warehouseinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_warehouseinfo.Location = new System.Drawing.Point(0, 0);
            this.dgv_warehouseinfo.Name = "dgv_warehouseinfo";
            this.dgv_warehouseinfo.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_warehouseinfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_warehouseinfo.RowTemplate.Height = 23;
            this.dgv_warehouseinfo.Size = new System.Drawing.Size(721, 262);
            this.dgv_warehouseinfo.TabIndex = 0;
            this.dgv_warehouseinfo.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_warehouseinfo_CellMouseDoubleClick);
            // 
            // locId
            // 
            this.locId.DataPropertyName = "locId";
            this.locId.HeaderText = "库位编号";
            this.locId.Name = "locId";
            this.locId.ReadOnly = true;
            // 
            // loctype
            // 
            this.loctype.DataPropertyName = "loctype";
            this.loctype.HeaderText = "库位类型";
            this.loctype.Name = "loctype";
            this.loctype.ReadOnly = true;
            // 
            // storehouseId
            // 
            this.storehouseId.DataPropertyName = "storehouseId";
            this.storehouseId.HeaderText = "仓库编号";
            this.storehouseId.Name = "storehouseId";
            this.storehouseId.ReadOnly = true;
            // 
            // loctotal
            // 
            this.loctotal.DataPropertyName = "loctotal";
            this.loctotal.HeaderText = "库位容量";
            this.loctotal.Name = "loctotal";
            this.loctotal.ReadOnly = true;
            // 
            // locdesc
            // 
            this.locdesc.DataPropertyName = "locdesc";
            this.locdesc.HeaderText = "库位描述";
            this.locdesc.Name = "locdesc";
            this.locdesc.ReadOnly = true;
            // 
            // uplocId
            // 
            this.uplocId.DataPropertyName = "uplocId";
            this.uplocId.HeaderText = "上级库位";
            this.uplocId.Name = "uplocId";
            this.uplocId.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // FrmWarehouseManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 479);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmWarehouseManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加库位";
            this.Load += new System.EventHandler(this.FrmWarehouseManage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_warehouseinfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX bt_checkloc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_selcondition;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgv_warehouseinfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_remark;
        private System.Windows.Forms.TextBox tb_loctotal;
        private System.Windows.Forms.TextBox tb_locdesc;
        private System.Windows.Forms.TextBox tb_uplocid;
        private System.Windows.Forms.TextBox tb_locid;
        private System.Windows.Forms.Label label8;
        private DevComponents.DotNetBar.ButtonX bt_select;
        private DevComponents.DotNetBar.ButtonX bt_saveinfo;
        private DevComponents.DotNetBar.ButtonX bt_deleteinfo;
        private System.Windows.Forms.ComboBox cb_loctype;
        private System.Windows.Forms.ComboBox cb_warehouse;
        private DevComponents.DotNetBar.ButtonX bt_clearinfo;
        private System.Windows.Forms.TextBox tb_number;
        private DevComponents.DotNetBar.ButtonX bt_updateinfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn locId;
        private System.Windows.Forms.DataGridViewTextBoxColumn loctype;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehouseId;
        private System.Windows.Forms.DataGridViewTextBoxColumn loctotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn locdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn uplocId;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
    }
}