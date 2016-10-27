namespace SFIS_V2
{
    partial class StoreLocManage
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_allinfo = new DevComponents.DotNetBar.ButtonX();
            this.bt_select = new DevComponents.DotNetBar.ButtonX();
            this.tb_warehouse = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dgv_storeinfo = new System.Windows.Forms.DataGridView();
            this.storehouseId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storehousename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storehouseman = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storehousetype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storehousedesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bt_addhouse = new DevComponents.DotNetBar.ButtonX();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dgv_locinfo = new System.Windows.Forms.DataGridView();
            this.locId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loctype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouseid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loctotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uplocId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bt_addloc = new DevComponents.DotNetBar.ButtonX();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_storeinfo)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_locinfo)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_allinfo);
            this.panel1.Controls.Add(this.bt_select);
            this.panel1.Controls.Add(this.tb_warehouse);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(745, 44);
            this.panel1.TabIndex = 0;
            // 
            // bt_allinfo
            // 
            this.bt_allinfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_allinfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_allinfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_allinfo.Location = new System.Drawing.Point(315, 11);
            this.bt_allinfo.Name = "bt_allinfo";
            this.bt_allinfo.Size = new System.Drawing.Size(79, 27);
            this.bt_allinfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_allinfo.TabIndex = 3;
            this.bt_allinfo.Text = "更新仓库";
            this.bt_allinfo.Click += new System.EventHandler(this.bt_allinfo_Click);
            // 
            // bt_select
            // 
            this.bt_select.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_select.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_select.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_select.Location = new System.Drawing.Point(201, 11);
            this.bt_select.Name = "bt_select";
            this.bt_select.Size = new System.Drawing.Size(75, 27);
            this.bt_select.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_select.TabIndex = 2;
            this.bt_select.Text = "查询";
            this.bt_select.Click += new System.EventHandler(this.bt_select_Click);
            // 
            // tb_warehouse
            // 
            this.tb_warehouse.Location = new System.Drawing.Point(71, 14);
            this.tb_warehouse.Name = "tb_warehouse";
            this.tb_warehouse.Size = new System.Drawing.Size(100, 21);
            this.tb_warehouse.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "仓库编号";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.splitContainer1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 44);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(745, 436);
            this.panel3.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel5);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel6);
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Size = new System.Drawing.Size(745, 436);
            this.splitContainer1.SplitterDistance = 341;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dgv_storeinfo);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(341, 398);
            this.panel5.TabIndex = 2;
            // 
            // dgv_storeinfo
            // 
            this.dgv_storeinfo.AllowUserToDeleteRows = false;
            this.dgv_storeinfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_storeinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_storeinfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.storehouseId,
            this.storehousename,
            this.storehouseman,
            this.storehousetype,
            this.storehousedesc,
            this.remark});
            this.dgv_storeinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_storeinfo.Location = new System.Drawing.Point(0, 0);
            this.dgv_storeinfo.Name = "dgv_storeinfo";
            this.dgv_storeinfo.ReadOnly = true;
            this.dgv_storeinfo.RowTemplate.Height = 23;
            this.dgv_storeinfo.Size = new System.Drawing.Size(341, 398);
            this.dgv_storeinfo.TabIndex = 1;
            this.dgv_storeinfo.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_storeinfo_CellMouseDoubleClick);
            // 
            // storehouseId
            // 
            this.storehouseId.DataPropertyName = "storehouseId";
            this.storehouseId.HeaderText = "仓库编号";
            this.storehouseId.Name = "storehouseId";
            this.storehouseId.ReadOnly = true;
            this.storehouseId.Width = 78;
            // 
            // storehousename
            // 
            this.storehousename.DataPropertyName = "storehousename";
            this.storehousename.HeaderText = "仓库名称";
            this.storehousename.Name = "storehousename";
            this.storehousename.ReadOnly = true;
            this.storehousename.Width = 78;
            // 
            // storehouseman
            // 
            this.storehouseman.DataPropertyName = "storehouseman";
            this.storehouseman.HeaderText = "仓库负责人";
            this.storehouseman.Name = "storehouseman";
            this.storehouseman.ReadOnly = true;
            this.storehouseman.Width = 90;
            // 
            // storehousetype
            // 
            this.storehousetype.DataPropertyName = "storehousetype";
            this.storehousetype.HeaderText = "仓库类型";
            this.storehousetype.Name = "storehousetype";
            this.storehousetype.ReadOnly = true;
            this.storehousetype.Width = 78;
            // 
            // storehousedesc
            // 
            this.storehousedesc.DataPropertyName = "storehousedesc";
            this.storehousedesc.HeaderText = "仓库描述";
            this.storehousedesc.Name = "storehousedesc";
            this.storehousedesc.ReadOnly = true;
            this.storehousedesc.Width = 78;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 54;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bt_addhouse);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 398);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(341, 38);
            this.panel2.TabIndex = 1;
            // 
            // bt_addhouse
            // 
            this.bt_addhouse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_addhouse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_addhouse.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_addhouse.Location = new System.Drawing.Point(182, 5);
            this.bt_addhouse.Name = "bt_addhouse";
            this.bt_addhouse.Size = new System.Drawing.Size(75, 27);
            this.bt_addhouse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_addhouse.TabIndex = 5;
            this.bt_addhouse.Text = "添加仓库";
            this.bt_addhouse.Click += new System.EventHandler(this.bt_addhouse_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dgv_locinfo);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(400, 399);
            this.panel6.TabIndex = 3;
            // 
            // dgv_locinfo
            // 
            this.dgv_locinfo.AllowUserToDeleteRows = false;
            this.dgv_locinfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_locinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_locinfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.locId,
            this.loctype,
            this.warehouseid,
            this.loctotal,
            this.locdesc,
            this.uplocId,
            this.remar});
            this.dgv_locinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_locinfo.Location = new System.Drawing.Point(0, 0);
            this.dgv_locinfo.Name = "dgv_locinfo";
            this.dgv_locinfo.ReadOnly = true;
            this.dgv_locinfo.RowTemplate.Height = 23;
            this.dgv_locinfo.Size = new System.Drawing.Size(400, 399);
            this.dgv_locinfo.TabIndex = 2;
            this.dgv_locinfo.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_locinfo_CellMouseEnter);
            // 
            // locId
            // 
            this.locId.DataPropertyName = "locId";
            this.locId.HeaderText = "库位编号";
            this.locId.Name = "locId";
            this.locId.ReadOnly = true;
            this.locId.Width = 78;
            // 
            // loctype
            // 
            this.loctype.DataPropertyName = "loctype";
            this.loctype.HeaderText = "库位类型";
            this.loctype.Name = "loctype";
            this.loctype.ReadOnly = true;
            this.loctype.Width = 78;
            // 
            // warehouseid
            // 
            this.warehouseid.DataPropertyName = "storehouseId";
            this.warehouseid.HeaderText = "所属仓库";
            this.warehouseid.Name = "warehouseid";
            this.warehouseid.ReadOnly = true;
            this.warehouseid.Width = 78;
            // 
            // loctotal
            // 
            this.loctotal.DataPropertyName = "loctotal";
            this.loctotal.HeaderText = "库位数量";
            this.loctotal.Name = "loctotal";
            this.loctotal.ReadOnly = true;
            this.loctotal.Width = 78;
            // 
            // locdesc
            // 
            this.locdesc.DataPropertyName = "locdesc";
            this.locdesc.HeaderText = "库位描述";
            this.locdesc.Name = "locdesc";
            this.locdesc.ReadOnly = true;
            this.locdesc.Width = 78;
            // 
            // uplocId
            // 
            this.uplocId.DataPropertyName = "uplocId";
            this.uplocId.HeaderText = "上级库位";
            this.uplocId.Name = "uplocId";
            this.uplocId.ReadOnly = true;
            this.uplocId.Width = 78;
            // 
            // remar
            // 
            this.remar.DataPropertyName = "remark";
            this.remar.HeaderText = "备注";
            this.remar.Name = "remar";
            this.remar.ReadOnly = true;
            this.remar.Width = 54;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.bt_addloc);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 399);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(400, 37);
            this.panel4.TabIndex = 2;
            // 
            // bt_addloc
            // 
            this.bt_addloc.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_addloc.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_addloc.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_addloc.Location = new System.Drawing.Point(196, 5);
            this.bt_addloc.Name = "bt_addloc";
            this.bt_addloc.Size = new System.Drawing.Size(75, 26);
            this.bt_addloc.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_addloc.TabIndex = 6;
            this.bt_addloc.Text = "添加库位";
            this.bt_addloc.Click += new System.EventHandler(this.bt_addloc_Click);
            // 
            // StoreLocManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 480);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "StoreLocManage";
            this.Text = "仓库库位管理";
            this.Load += new System.EventHandler(this.StoreLocManage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_storeinfo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_locinfo)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_warehouse;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX bt_allinfo;
        private DevComponents.DotNetBar.ButtonX bt_select;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ButtonX bt_addhouse;
        private System.Windows.Forms.Panel panel4;
        private DevComponents.DotNetBar.ButtonX bt_addloc;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dgv_storeinfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehouseId;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehousename;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehouseman;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehousetype;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehousedesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView dgv_locinfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn locId;
        private System.Windows.Forms.DataGridViewTextBoxColumn loctype;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouseid;
        private System.Windows.Forms.DataGridViewTextBoxColumn loctotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn locdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn uplocId;
        private System.Windows.Forms.DataGridViewTextBoxColumn remar;
    }
}