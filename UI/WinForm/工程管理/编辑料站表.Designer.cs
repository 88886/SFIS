namespace SFIS_V2
{
    partial class EditKpMarster
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
            this.tb_partnumber = new DevComponents.DotNetBar.TextBoxItem();
            this.bt_select = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.bt_submit = new DevComponents.DotNetBar.ButtonItem();
            this.bt_expkpmaster = new DevComponents.DotNetBar.ButtonItem();
            this.bt_deleteMaster = new DevComponents.DotNetBar.ButtonItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv_ShowKpMaster = new System.Windows.Forms.DataGridView();
            this.masterId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bomver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcbside = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reserve1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reserve2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updateedit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgv_ShowKPDetalt = new System.Windows.Forms.DataGridView();
            this.stationno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpdistinct = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.replacegroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priorityclass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._reserve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._reserve1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShowKpMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShowKPDetalt)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.tb_partnumber,
            this.bt_select,
            this.labelItem2,
            this.bt_submit,
            this.bt_expkpmaster,
            this.bt_deleteMaster});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(912, 27);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 1;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "[成品料号]:";
            // 
            // tb_partnumber
            // 
            this.tb_partnumber.Name = "tb_partnumber";
            this.tb_partnumber.TextBoxWidth = 128;
            this.tb_partnumber.WatermarkColor = System.Drawing.SystemColors.GrayText;
            // 
            // bt_select
            // 
            this.bt_select.Name = "bt_select";
            this.bt_select.Text = "查询";
            this.bt_select.Click += new System.EventHandler(this.bt_select_Click);
            // 
            // labelItem2
            // 
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "               ";
            // 
            // bt_submit
            // 
            this.bt_submit.Name = "bt_submit";
            this.bt_submit.Text = "提交修改";
            this.bt_submit.Click += new System.EventHandler(this.bt_submit_Click);
            // 
            // bt_expkpmaster
            // 
            this.bt_expkpmaster.Name = "bt_expkpmaster";
            this.bt_expkpmaster.Text = "打印料站表";
            this.bt_expkpmaster.Click += new System.EventHandler(this.bt_expkpmaster_Click);
            // 
            // bt_deleteMaster
            // 
            this.bt_deleteMaster.Name = "bt_deleteMaster";
            this.bt_deleteMaster.Text = "删除料站表";
            this.bt_deleteMaster.Click += new System.EventHandler(this.bt_deleteMaster_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgv_ShowKpMaster);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_ShowKPDetalt);
            this.splitContainer1.Size = new System.Drawing.Size(912, 396);
            this.splitContainer1.SplitterDistance = 184;
            this.splitContainer1.TabIndex = 2;
            // 
            // dgv_ShowKpMaster
            // 
            this.dgv_ShowKpMaster.AllowUserToAddRows = false;
            this.dgv_ShowKpMaster.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ShowKpMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ShowKpMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ShowKpMaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.masterId,
            this.machine,
            this.userId,
            this.partnumber,
            this.modelname,
            this.bomver,
            this.pcbside,
            this.recdate,
            this.reserve1,
            this.reserve2,
            this.updateedit});
            this.dgv_ShowKpMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ShowKpMaster.Location = new System.Drawing.Point(0, 0);
            this.dgv_ShowKpMaster.Name = "dgv_ShowKpMaster";
            this.dgv_ShowKpMaster.RowTemplate.Height = 23;
            this.dgv_ShowKpMaster.Size = new System.Drawing.Size(912, 184);
            this.dgv_ShowKpMaster.TabIndex = 0;
            this.dgv_ShowKpMaster.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ShowKpMaster_CellDoubleClick);
            this.dgv_ShowKpMaster.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ShowKpMaster_CellEndEdit);
            this.dgv_ShowKpMaster.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ShowKpMaster_CellEnter);
            this.dgv_ShowKpMaster.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_ShowKpMaster_CellMouseClick);
            this.dgv_ShowKpMaster.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgv_ShowKpMaster_RowPrePaint);
            this.dgv_ShowKpMaster.Leave += new System.EventHandler(this.dgv_ShowKpMaster_Leave);
            // 
            // masterId
            // 
            this.masterId.DataPropertyName = "masterId";
            this.masterId.HeaderText = "料表编号";
            this.masterId.Name = "masterId";
            this.masterId.ReadOnly = true;
            this.masterId.Width = 80;
            // 
            // machine
            // 
            this.machine.DataPropertyName = "lineId";
            this.machine.HeaderText = "机器名称";
            this.machine.Name = "machine";
            this.machine.Width = 80;
            // 
            // userId
            // 
            this.userId.DataPropertyName = "userId";
            this.userId.HeaderText = "建立人";
            this.userId.Name = "userId";
            this.userId.ReadOnly = true;
            this.userId.Width = 70;
            // 
            // partnumber
            // 
            this.partnumber.DataPropertyName = "partnumber";
            this.partnumber.HeaderText = "成品料号";
            this.partnumber.Name = "partnumber";
            this.partnumber.Width = 80;
            // 
            // modelname
            // 
            this.modelname.DataPropertyName = "modelname";
            this.modelname.HeaderText = "产品型号";
            this.modelname.Name = "modelname";
            // 
            // bomver
            // 
            this.bomver.DataPropertyName = "bomver";
            this.bomver.HeaderText = "BOM版本";
            this.bomver.Name = "bomver";
            this.bomver.Width = 70;
            // 
            // pcbside
            // 
            this.pcbside.DataPropertyName = "pcbside";
            this.pcbside.HeaderText = "PCB面";
            this.pcbside.Name = "pcbside";
            this.pcbside.ReadOnly = true;
            this.pcbside.Width = 60;
            // 
            // recdate
            // 
            this.recdate.DataPropertyName = "recdate";
            this.recdate.HeaderText = "建立日期";
            this.recdate.Name = "recdate";
            // 
            // reserve1
            // 
            this.reserve1.DataPropertyName = "reserve1";
            this.reserve1.HeaderText = "SMT程式名称";
            this.reserve1.Name = "reserve1";
            // 
            // reserve2
            // 
            this.reserve2.DataPropertyName = "reserve2";
            this.reserve2.HeaderText = "审核状态";
            this.reserve2.Name = "reserve2";
            this.reserve2.Width = 80;
            // 
            // updateedit
            // 
            this.updateedit.HeaderText = "提交修改";
            this.updateedit.Name = "updateedit";
            this.updateedit.ReadOnly = true;
            // 
            // dgv_ShowKPDetalt
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            this.dgv_ShowKPDetalt.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ShowKPDetalt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_ShowKPDetalt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ShowKPDetalt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stationno,
            this.kpnumber,
            this.kpdesc,
            this.kpdistinct,
            this.replacegroup,
            this.priorityclass,
            this.loction,
            this._reserve,
            this._reserve1});
            this.dgv_ShowKPDetalt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ShowKPDetalt.Location = new System.Drawing.Point(0, 0);
            this.dgv_ShowKPDetalt.Name = "dgv_ShowKPDetalt";
            this.dgv_ShowKPDetalt.RowTemplate.Height = 23;
            this.dgv_ShowKPDetalt.Size = new System.Drawing.Size(912, 208);
            this.dgv_ShowKPDetalt.TabIndex = 0;
            this.dgv_ShowKPDetalt.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ShowKPDetalt_CellEndEdit);
            this.dgv_ShowKPDetalt.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ShowKPDetalt_CellEnter);
            this.dgv_ShowKPDetalt.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_ShowKPDetalt_DataError);
            this.dgv_ShowKPDetalt.Leave += new System.EventHandler(this.dgv_ShowKPDetalt_Leave);
            // 
            // stationno
            // 
            this.stationno.DataPropertyName = "stationno";
            this.stationno.HeaderText = "料站编号";
            this.stationno.Name = "stationno";
            // 
            // kpnumber
            // 
            this.kpnumber.DataPropertyName = "kpnumber";
            this.kpnumber.HeaderText = "料号";
            this.kpnumber.Name = "kpnumber";
            // 
            // kpdesc
            // 
            this.kpdesc.DataPropertyName = "kpdesc";
            this.kpdesc.HeaderText = "物料描述";
            this.kpdesc.Name = "kpdesc";
            // 
            // kpdistinct
            // 
            this.kpdistinct.DataPropertyName = "kpdistinct";
            this.kpdistinct.HeaderText = "是否主料";
            this.kpdistinct.Name = "kpdistinct";
            this.kpdistinct.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.kpdistinct.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // replacegroup
            // 
            this.replacegroup.DataPropertyName = "replacegroup";
            this.replacegroup.HeaderText = "所属分组";
            this.replacegroup.Name = "replacegroup";
            // 
            // priorityclass
            // 
            this.priorityclass.DataPropertyName = "priorityclass";
            this.priorityclass.HeaderText = "优先级";
            this.priorityclass.Name = "priorityclass";
            // 
            // loction
            // 
            this.loction.DataPropertyName = "loction";
            this.loction.HeaderText = "位置";
            this.loction.Name = "loction";
            // 
            // _reserve
            // 
            this._reserve.DataPropertyName = "reserve";
            this._reserve.HeaderText = "Feeder类型";
            this._reserve.Name = "_reserve";
            // 
            // _reserve1
            // 
            this._reserve1.DataPropertyName = "reserve1";
            this._reserve1.HeaderText = "保留";
            this._reserve1.Name = "_reserve1";
            // 
            // EditKpMarster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 423);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EditKpMarster";
            this.Text = "修改料站表信息";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditKpMarster_FormClosing);
            this.Load += new System.EventHandler(this.EditKpMarster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShowKpMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShowKPDetalt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.TextBoxItem tb_partnumber;
        private DevComponents.DotNetBar.ButtonItem bt_select;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv_ShowKpMaster;
        private System.Windows.Forms.DataGridView dgv_ShowKPDetalt;
        private DevComponents.DotNetBar.ButtonItem bt_submit;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private DevComponents.DotNetBar.ButtonItem bt_expkpmaster;
        private DevComponents.DotNetBar.ButtonItem bt_deleteMaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationno;
        private System.Windows.Forms.DataGridViewTextBoxColumn kpnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn kpdesc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn kpdistinct;
        private System.Windows.Forms.DataGridViewTextBoxColumn replacegroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn priorityclass;
        private System.Windows.Forms.DataGridViewTextBoxColumn loction;
        private System.Windows.Forms.DataGridViewTextBoxColumn _reserve;
        private System.Windows.Forms.DataGridViewTextBoxColumn _reserve1;
        private System.Windows.Forms.DataGridViewTextBoxColumn masterId;
        private System.Windows.Forms.DataGridViewTextBoxColumn machine;
        private System.Windows.Forms.DataGridViewTextBoxColumn userId;
        private System.Windows.Forms.DataGridViewTextBoxColumn partnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelname;
        private System.Windows.Forms.DataGridViewTextBoxColumn bomver;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcbside;
        private System.Windows.Forms.DataGridViewTextBoxColumn recdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn reserve1;
        private System.Windows.Forms.DataGridViewTextBoxColumn reserve2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn updateedit;
    }
}