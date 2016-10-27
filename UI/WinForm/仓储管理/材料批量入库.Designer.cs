namespace SFIS_V2
{
    partial class storagebuffer
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
            this.dgv_showdata = new System.Windows.Forms.DataGridView();
            this.trsn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendercode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lotId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storehouseId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.cm_selectmodel = new System.Windows.Forms.ComboBox();
            this.tb_selectvalue = new System.Windows.Forms.TextBox();
            this.bt_query = new DevComponents.DotNetBar.ButtonX();
            this.save = new DevComponents.DotNetBar.ButtonX();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbo_storehouseId = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbo_locId = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.bt_submit = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showdata)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_showdata
            // 
            this.dgv_showdata.AllowUserToAddRows = false;
            this.dgv_showdata.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            this.dgv_showdata.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_showdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_showdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.trsn,
            this.kpnumber,
            this.vendercode,
            this.datecode,
            this.lotId,
            this.qty,
            this.sstatus,
            this.recdate,
            this.userId,
            this.storehouseId,
            this.locId});
            this.dgv_showdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_showdata.Location = new System.Drawing.Point(0, 0);
            this.dgv_showdata.Name = "dgv_showdata";
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dgv_showdata.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_showdata.RowTemplate.Height = 23;
            this.dgv_showdata.Size = new System.Drawing.Size(781, 313);
            this.dgv_showdata.TabIndex = 1;
            this.dgv_showdata.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgv_showdata_Scroll);
            this.dgv_showdata.CurrentCellChanged += new System.EventHandler(this.dgv_showdata_CurrentCellChanged);
            this.dgv_showdata.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgv_showdata_ColumnWidthChanged);
            // 
            // trsn
            // 
            this.trsn.DataPropertyName = "trsn";
            this.trsn.HeaderText = "编号";
            this.trsn.Name = "trsn";
            this.trsn.ReadOnly = true;
            // 
            // kpnumber
            // 
            this.kpnumber.DataPropertyName = "kpnumber";
            this.kpnumber.HeaderText = "料号";
            this.kpnumber.Name = "kpnumber";
            this.kpnumber.ReadOnly = true;
            // 
            // vendercode
            // 
            this.vendercode.DataPropertyName = "vendercode";
            this.vendercode.HeaderText = "厂商代码";
            this.vendercode.Name = "vendercode";
            this.vendercode.ReadOnly = true;
            // 
            // datecode
            // 
            this.datecode.DataPropertyName = "datecode";
            this.datecode.HeaderText = "DateCode";
            this.datecode.Name = "datecode";
            this.datecode.ReadOnly = true;
            // 
            // lotId
            // 
            this.lotId.DataPropertyName = "lotId";
            this.lotId.HeaderText = "批次";
            this.lotId.Name = "lotId";
            this.lotId.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "数量";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            // 
            // sstatus
            // 
            this.sstatus.DataPropertyName = "sstatus";
            this.sstatus.HeaderText = "状态";
            this.sstatus.Name = "sstatus";
            this.sstatus.ReadOnly = true;
            // 
            // recdate
            // 
            this.recdate.DataPropertyName = "recdate";
            this.recdate.HeaderText = "记录日期";
            this.recdate.Name = "recdate";
            this.recdate.ReadOnly = true;
            // 
            // userId
            // 
            this.userId.DataPropertyName = "userId";
            this.userId.HeaderText = "经手人";
            this.userId.Name = "userId";
            this.userId.ReadOnly = true;
            // 
            // storehouseId
            // 
            this.storehouseId.DataPropertyName = "storehouseId";
            this.storehouseId.HeaderText = "仓库编号";
            this.storehouseId.Name = "storehouseId";
            this.storehouseId.ReadOnly = true;
            // 
            // locId
            // 
            this.locId.DataPropertyName = "locId";
            this.locId.HeaderText = "库位编号";
            this.locId.Name = "locId";
            this.locId.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(4, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "[查询条件:]";
            // 
            // cm_selectmodel
            // 
            this.cm_selectmodel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cm_selectmodel.FormattingEnabled = true;
            this.cm_selectmodel.Items.AddRange(new object[] {
            "ALL",
            "料号",
            "厂商代码",
            "DCode",
            "人员工号",
            "trsn"});
            this.cm_selectmodel.Location = new System.Drawing.Point(81, 14);
            this.cm_selectmodel.Name = "cm_selectmodel";
            this.cm_selectmodel.Size = new System.Drawing.Size(76, 20);
            this.cm_selectmodel.TabIndex = 3;
            // 
            // tb_selectvalue
            // 
            this.tb_selectvalue.Location = new System.Drawing.Point(81, 44);
            this.tb_selectvalue.Name = "tb_selectvalue";
            this.tb_selectvalue.Size = new System.Drawing.Size(150, 21);
            this.tb_selectvalue.TabIndex = 4;
            // 
            // bt_query
            // 
            this.bt_query.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_query.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_query.Location = new System.Drawing.Point(237, 44);
            this.bt_query.Name = "bt_query";
            this.bt_query.Size = new System.Drawing.Size(45, 21);
            this.bt_query.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_query.TabIndex = 5;
            this.bt_query.Text = "查询";
            this.bt_query.Click += new System.EventHandler(this.bt_query_Click);
            // 
            // save
            // 
            this.save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.save.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.save.Location = new System.Drawing.Point(6, 151);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(291, 46);
            this.save.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.save.TabIndex = 7;
            this.save.Text = "保存";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(4, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "[仓库编号:]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(4, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "[库位编号:]";
            // 
            // cbo_storehouseId
            // 
            this.cbo_storehouseId.DisplayMember = "Text";
            this.cbo_storehouseId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_storehouseId.FormattingEnabled = true;
            this.cbo_storehouseId.ItemHeight = 15;
            this.cbo_storehouseId.Location = new System.Drawing.Point(81, 75);
            this.cbo_storehouseId.Name = "cbo_storehouseId";
            this.cbo_storehouseId.Size = new System.Drawing.Size(150, 21);
            this.cbo_storehouseId.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbo_storehouseId.TabIndex = 10;
            this.cbo_storehouseId.SelectedIndexChanged += new System.EventHandler(this.cbo_storehouseId_SelectedIndexChanged);
            // 
            // cbo_locId
            // 
            this.cbo_locId.DisplayMember = "Text";
            this.cbo_locId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_locId.FormattingEnabled = true;
            this.cbo_locId.ItemHeight = 15;
            this.cbo_locId.Location = new System.Drawing.Point(81, 106);
            this.cbo_locId.Name = "cbo_locId";
            this.cbo_locId.Size = new System.Drawing.Size(150, 21);
            this.cbo_locId.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbo_locId.TabIndex = 10;
            this.cbo_locId.Validated += new System.EventHandler(this.cbo_locId_Validated);
            // 
            // bt_submit
            // 
            this.bt_submit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_submit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_submit.Location = new System.Drawing.Point(237, 105);
            this.bt_submit.Name = "bt_submit";
            this.bt_submit.Size = new System.Drawing.Size(47, 22);
            this.bt_submit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_submit.TabIndex = 11;
            this.bt_submit.Text = "提交";
            this.bt_submit.Click += new System.EventHandler(this.bt_submit_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.bt_submit);
            this.groupPanel1.Controls.Add(this.label2);
            this.groupPanel1.Controls.Add(this.cbo_locId);
            this.groupPanel1.Controls.Add(this.cm_selectmodel);
            this.groupPanel1.Controls.Add(this.cbo_storehouseId);
            this.groupPanel1.Controls.Add(this.tb_selectvalue);
            this.groupPanel1.Controls.Add(this.label4);
            this.groupPanel1.Controls.Add(this.bt_query);
            this.groupPanel1.Controls.Add(this.label3);
            this.groupPanel1.Controls.Add(this.save);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(316, 204);
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
            this.groupPanel1.TabIndex = 12;
            // 
            // rtb_log
            // 
            this.rtb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_log.Location = new System.Drawing.Point(0, 0);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.Size = new System.Drawing.Size(465, 204);
            this.rtb_log.TabIndex = 0;
            this.rtb_log.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 313);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(781, 204);
            this.panel1.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rtb_log);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(316, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(465, 204);
            this.panel3.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(316, 204);
            this.panel2.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgv_showdata);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(781, 313);
            this.panel4.TabIndex = 14;
            // 
            // storagebuffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 517);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "storagebuffer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "材料批量入库";
            this.Load += new System.EventHandler(this.storagebuffer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showdata)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_showdata;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cm_selectmodel;
        private System.Windows.Forms.TextBox tb_selectvalue;
        private DevComponents.DotNetBar.ButtonX bt_query;
        private System.Windows.Forms.DataGridViewTextBoxColumn trsn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kpnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendercode;
        private System.Windows.Forms.DataGridViewTextBoxColumn datecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn lotId;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn sstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn recdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn userId;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehouseId;
        private System.Windows.Forms.DataGridViewTextBoxColumn locId;
        private DevComponents.DotNetBar.ButtonX save;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbo_storehouseId;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbo_locId;
        private DevComponents.DotNetBar.ButtonX bt_submit;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.RichTextBox rtb_log;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
    }
}