namespace SFIS_V2
{
    partial class ChkKpMarster
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
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_showkpmaster = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_viewkpmaster = new DevComponents.DotNetBar.ButtonX();
            this.bt_printer = new DevComponents.DotNetBar.ButtonX();
            this.bt_submit = new DevComponents.DotNetBar.ButtonX();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv_showkpdetalt = new System.Windows.Forms.DataGridView();
            this.stationno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpdistinct = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.replacegroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priorityclass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._reserve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._reserve1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.masterId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lineId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bomver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcbside = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reserve1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RESERVE2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showkpmaster)).BeginInit();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showkpdetalt)).BeginInit();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.panel2);
            this.groupPanel1.Controls.Add(this.panel1);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(897, 223);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "待审核的料站表";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_showkpmaster);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(891, 159);
            this.panel2.TabIndex = 4;
            // 
            // dgv_showkpmaster
            // 
            this.dgv_showkpmaster.AllowUserToAddRows = false;
            this.dgv_showkpmaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_showkpmaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.masterId,
            this.lineId,
            this.partnumber,
            this.modelname,
            this.bomver,
            this.pcbside,
            this.reserve1,
            this.recdate,
            this.userId,
            this.RESERVE2});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_showkpmaster.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_showkpmaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_showkpmaster.Location = new System.Drawing.Point(0, 0);
            this.dgv_showkpmaster.MultiSelect = false;
            this.dgv_showkpmaster.Name = "dgv_showkpmaster";
            this.dgv_showkpmaster.RowTemplate.Height = 23;
            this.dgv_showkpmaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_showkpmaster.Size = new System.Drawing.Size(891, 159);
            this.dgv_showkpmaster.TabIndex = 0;
            this.dgv_showkpmaster.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_showkpmaster_CellDoubleClick);
            this.dgv_showkpmaster.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgv_showkpmaster_ColumnWidthChanged);
            this.dgv_showkpmaster.CurrentCellChanged += new System.EventHandler(this.dgv_showkpmaster_CurrentCellChanged);
            this.dgv_showkpmaster.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgv_showkpmaster_RowPrePaint);
            this.dgv_showkpmaster.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgv_showkpmaster_Scroll);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_viewkpmaster);
            this.panel1.Controls.Add(this.bt_printer);
            this.panel1.Controls.Add(this.bt_submit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(891, 40);
            this.panel1.TabIndex = 3;
            // 
            // bt_viewkpmaster
            // 
            this.bt_viewkpmaster.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_viewkpmaster.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_viewkpmaster.Location = new System.Drawing.Point(97, 5);
            this.bt_viewkpmaster.Name = "bt_viewkpmaster";
            this.bt_viewkpmaster.Size = new System.Drawing.Size(95, 28);
            this.bt_viewkpmaster.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_viewkpmaster.TabIndex = 0;
            this.bt_viewkpmaster.Text = "查看料表内容";
            this.bt_viewkpmaster.Tooltip = "查看选中的料表的内容";
            this.bt_viewkpmaster.Click += new System.EventHandler(this.bt_viewkpmaster_Click);
            // 
            // bt_printer
            // 
            this.bt_printer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_printer.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_printer.Location = new System.Drawing.Point(562, 6);
            this.bt_printer.Name = "bt_printer";
            this.bt_printer.Size = new System.Drawing.Size(75, 29);
            this.bt_printer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_printer.TabIndex = 2;
            this.bt_printer.Text = "打印料站表";
            this.bt_printer.Click += new System.EventHandler(this.bt_printer_Click);
            // 
            // bt_submit
            // 
            this.bt_submit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_submit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_submit.Location = new System.Drawing.Point(350, 5);
            this.bt_submit.Name = "bt_submit";
            this.bt_submit.Size = new System.Drawing.Size(68, 27);
            this.bt_submit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_submit.TabIndex = 1;
            this.bt_submit.Text = "提交审核";
            this.bt_submit.Click += new System.EventHandler(this.bt_submit_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_showkpdetalt);
            this.splitContainer1.Size = new System.Drawing.Size(897, 452);
            this.splitContainer1.SplitterDistance = 223;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgv_showkpdetalt
            // 
            this.dgv_showkpdetalt.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            this.dgv_showkpdetalt.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_showkpdetalt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_showkpdetalt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stationno,
            this.kpnumber,
            this.kpdesc,
            this.kpdistinct,
            this.replacegroup,
            this.priorityclass,
            this.loction,
            this._reserve,
            this._reserve1});
            this.dgv_showkpdetalt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_showkpdetalt.Location = new System.Drawing.Point(0, 0);
            this.dgv_showkpdetalt.Name = "dgv_showkpdetalt";
            this.dgv_showkpdetalt.ReadOnly = true;
            this.dgv_showkpdetalt.RowTemplate.Height = 23;
            this.dgv_showkpdetalt.Size = new System.Drawing.Size(897, 225);
            this.dgv_showkpdetalt.TabIndex = 0;
            // 
            // stationno
            // 
            this.stationno.DataPropertyName = "stationno";
            this.stationno.HeaderText = "料站编号";
            this.stationno.Name = "stationno";
            this.stationno.ReadOnly = true;
            // 
            // kpnumber
            // 
            this.kpnumber.DataPropertyName = "kpnumber";
            this.kpnumber.HeaderText = "料号";
            this.kpnumber.Name = "kpnumber";
            this.kpnumber.ReadOnly = true;
            // 
            // kpdesc
            // 
            this.kpdesc.DataPropertyName = "kpdesc";
            this.kpdesc.HeaderText = "物料描述";
            this.kpdesc.Name = "kpdesc";
            this.kpdesc.ReadOnly = true;
            // 
            // kpdistinct
            // 
            this.kpdistinct.DataPropertyName = "kpdistinct";
            this.kpdistinct.HeaderText = "是否主料";
            this.kpdistinct.Name = "kpdistinct";
            this.kpdistinct.ReadOnly = true;
            // 
            // replacegroup
            // 
            this.replacegroup.DataPropertyName = "replacegroup";
            this.replacegroup.HeaderText = "替代组";
            this.replacegroup.Name = "replacegroup";
            this.replacegroup.ReadOnly = true;
            this.replacegroup.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.replacegroup.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // priorityclass
            // 
            this.priorityclass.DataPropertyName = "priorityclass";
            this.priorityclass.HeaderText = "优先级";
            this.priorityclass.Name = "priorityclass";
            this.priorityclass.ReadOnly = true;
            this.priorityclass.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.priorityclass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // loction
            // 
            this.loction.DataPropertyName = "loction";
            this.loction.HeaderText = "位置";
            this.loction.Name = "loction";
            this.loction.ReadOnly = true;
            this.loction.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.loction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _reserve
            // 
            this._reserve.DataPropertyName = "reserve";
            this._reserve.HeaderText = "Feeder类型";
            this._reserve.Name = "_reserve";
            this._reserve.ReadOnly = true;
            // 
            // _reserve1
            // 
            this._reserve1.DataPropertyName = "reserve1";
            this._reserve1.HeaderText = "保留1";
            this._reserve1.Name = "_reserve1";
            this._reserve1.ReadOnly = true;
            this._reserve1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._reserve1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // masterId
            // 
            this.masterId.DataPropertyName = "masterId";
            this.masterId.HeaderText = "料表编号";
            this.masterId.Name = "masterId";
            this.masterId.ReadOnly = true;
            // 
            // lineId
            // 
            this.lineId.DataPropertyName = "lineId";
            this.lineId.HeaderText = "机器编号";
            this.lineId.Name = "lineId";
            this.lineId.ReadOnly = true;
            // 
            // partnumber
            // 
            this.partnumber.DataPropertyName = "partnumber";
            this.partnumber.HeaderText = "成品料号";
            this.partnumber.Name = "partnumber";
            this.partnumber.ReadOnly = true;
            // 
            // modelname
            // 
            this.modelname.DataPropertyName = "modelname";
            this.modelname.HeaderText = "产品型号";
            this.modelname.Name = "modelname";
            this.modelname.ReadOnly = true;
            // 
            // bomver
            // 
            this.bomver.DataPropertyName = "bomver";
            this.bomver.HeaderText = "BOM版本";
            this.bomver.Name = "bomver";
            this.bomver.ReadOnly = true;
            // 
            // pcbside
            // 
            this.pcbside.DataPropertyName = "pcbside";
            this.pcbside.HeaderText = "PCB面";
            this.pcbside.Name = "pcbside";
            this.pcbside.ReadOnly = true;
            // 
            // reserve1
            // 
            this.reserve1.DataPropertyName = "reserve1";
            this.reserve1.HeaderText = "SMT程式";
            this.reserve1.Name = "reserve1";
            this.reserve1.ReadOnly = true;
            // 
            // recdate
            // 
            this.recdate.DataPropertyName = "recdate";
            this.recdate.HeaderText = "建立日期";
            this.recdate.Name = "recdate";
            this.recdate.ReadOnly = true;
            // 
            // userId
            // 
            this.userId.DataPropertyName = "userId";
            this.userId.HeaderText = "建立人";
            this.userId.Name = "userId";
            this.userId.ReadOnly = true;
            // 
            // RESERVE2
            // 
            this.RESERVE2.DataPropertyName = "RESERVE2";
            this.RESERVE2.HeaderText = "状态";
            this.RESERVE2.Name = "RESERVE2";
            this.RESERVE2.ReadOnly = true;
            // 
            // ChkKpMarster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 452);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ChkKpMarster";
            this.Text = "料站表审核";
            this.Load += new System.EventHandler(this.ChkKpMarster_Load);
            this.groupPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showkpmaster)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showkpdetalt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.DataGridView dgv_showkpmaster;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv_showkpdetalt;
        private DevComponents.DotNetBar.ButtonX bt_submit;
        private DevComponents.DotNetBar.ButtonX bt_viewkpmaster;
        private DevComponents.DotNetBar.ButtonX bt_printer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn lineId;
        private System.Windows.Forms.DataGridViewTextBoxColumn partnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelname;
        private System.Windows.Forms.DataGridViewTextBoxColumn bomver;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcbside;
        private System.Windows.Forms.DataGridViewTextBoxColumn reserve1;
        private System.Windows.Forms.DataGridViewTextBoxColumn recdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn userId;
        private System.Windows.Forms.DataGridViewTextBoxColumn RESERVE2;
    }
}