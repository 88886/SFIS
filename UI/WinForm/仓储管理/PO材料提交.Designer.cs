namespace SFIS_V2
{
    partial class Frm_POCheck
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
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            this.cmi_querycondition = new DevComponents.DotNetBar.ComboBoxItem();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.tbi_po = new DevComponents.DotNetBar.TextBoxItem();
            this.bti_select = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.bti_submitsap = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dgv_materialinfo = new System.Windows.Forms.DataGridView();
            this.trsn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkd = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.location = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_materialinfo)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem3,
            this.cmi_querycondition,
            this.tbi_po,
            this.bti_select,
            this.labelItem2,
            this.bti_submitsap});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(858, 28);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // labelItem3
            // 
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "查询条件:";
            // 
            // cmi_querycondition
            // 
            this.cmi_querycondition.ComboWidth = 80;
            this.cmi_querycondition.DropDownHeight = 106;
            this.cmi_querycondition.ItemHeight = 17;
            this.cmi_querycondition.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3});
            this.cmi_querycondition.Name = "cmi_querycondition";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "PO";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "料号";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "唯一序列号";
            // 
            // tbi_po
            // 
            this.tbi_po.Name = "tbi_po";
            this.tbi_po.TextBoxWidth = 120;
            this.tbi_po.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.tbi_po.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbi_po_KeyDown);
            // 
            // bti_select
            // 
            this.bti_select.Name = "bti_select";
            this.bti_select.Text = "查询";
            this.bti_select.Click += new System.EventHandler(this.bti_select_Click);
            // 
            // labelItem2
            // 
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "                       ";
            // 
            // bti_submitsap
            // 
            this.bti_submitsap.Name = "bti_submitsap";
            this.bti_submitsap.Text = "提交SAP";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dgv_materialinfo);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 28);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(858, 424);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            this.panelEx1.Text = "panelEx1";
            // 
            // dgv_materialinfo
            // 
            this.dgv_materialinfo.AllowUserToAddRows = false;
            this.dgv_materialinfo.AllowUserToDeleteRows = false;
            this.dgv_materialinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_materialinfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.trsn,
            this.PO,
            this.kpnumber,
            this.qty,
            this.status,
            this.flag,
            this.recdate,
            this.checkd,
            this.location});
            this.dgv_materialinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_materialinfo.Location = new System.Drawing.Point(0, 0);
            this.dgv_materialinfo.Name = "dgv_materialinfo";
            this.dgv_materialinfo.ReadOnly = true;
            this.dgv_materialinfo.RowTemplate.Height = 23;
            this.dgv_materialinfo.Size = new System.Drawing.Size(858, 424);
            this.dgv_materialinfo.TabIndex = 0;
            this.dgv_materialinfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DGV_MouseClick);
            this.dgv_materialinfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_materialinfo_CellClick);
            // 
            // trsn
            // 
            this.trsn.DataPropertyName = "trsn";
            this.trsn.HeaderText = "唯一序列";
            this.trsn.Name = "trsn";
            this.trsn.ReadOnly = true;
            // 
            // PO
            // 
            this.PO.DataPropertyName = "PO";
            this.PO.HeaderText = "物料订单号";
            this.PO.Name = "PO";
            this.PO.ReadOnly = true;
            // 
            // kpnumber
            // 
            this.kpnumber.DataPropertyName = "kpnumber";
            this.kpnumber.HeaderText = "料号";
            this.kpnumber.Name = "kpnumber";
            this.kpnumber.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "数量";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // flag
            // 
            this.flag.DataPropertyName = "flag";
            this.flag.HeaderText = "标志位";
            this.flag.Name = "flag";
            this.flag.ReadOnly = true;
            // 
            // recdate
            // 
            this.recdate.DataPropertyName = "recdate";
            this.recdate.HeaderText = "时间";
            this.recdate.Name = "recdate";
            this.recdate.ReadOnly = true;
            // 
            // checkd
            // 
            this.checkd.HeaderText = "审核";
            this.checkd.Name = "checkd";
            this.checkd.ReadOnly = true;
            this.checkd.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.checkd.Visible = false;
            // 
            // location
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "选择库位";
            this.location.DefaultCellStyle = dataGridViewCellStyle1;
            this.location.HeaderText = "分配库位";
            this.location.Name = "location";
            this.location.ReadOnly = true;
            // 
            // Frm_POCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 452);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.bar1);
            this.Name = "Frm_POCheck";
            this.Text = "PO材料提交";
            this.Load += new System.EventHandler(this.Frm_POCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_materialinfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.TextBoxItem tbi_po;
        private DevComponents.DotNetBar.ButtonItem bti_select;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private DevComponents.DotNetBar.ButtonItem bti_submitsap;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.DataGridView dgv_materialinfo;
        private DevComponents.DotNetBar.LabelItem labelItem3;
        private DevComponents.DotNetBar.ComboBoxItem cmi_querycondition;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private System.Windows.Forms.DataGridViewTextBoxColumn trsn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PO;
        private System.Windows.Forms.DataGridViewTextBoxColumn kpnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn flag;
        private System.Windows.Forms.DataGridViewTextBoxColumn recdate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkd;
        private System.Windows.Forms.DataGridViewButtonColumn location;
    }
}