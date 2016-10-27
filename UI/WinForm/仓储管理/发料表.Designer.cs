namespace SFIS_V2
{
    partial class FrmSentMaterial
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
            this.bt_select = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.bt_toexcel = new DevComponents.DotNetBar.ButtonX();
            this.bt_selectwobominfo = new DevComponents.DotNetBar.ButtonX();
            this.cb_process = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_woid = new System.Windows.Forms.TextBox();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.woid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storehouseid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_select
            // 
            this.bt_select.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_select.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_select.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_select.Location = new System.Drawing.Point(300, 11);
            this.bt_select.Name = "bt_select";
            this.bt_select.Size = new System.Drawing.Size(53, 26);
            this.bt_select.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_select.TabIndex = 0;
            this.bt_select.Text = "查询";
            this.bt_select.Click += new System.EventHandler(this.bt_select_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.bt_toexcel);
            this.panelEx1.Controls.Add(this.bt_selectwobominfo);
            this.panelEx1.Controls.Add(this.cb_process);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.bt_select);
            this.panelEx1.Controls.Add(this.tb_woid);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(702, 53);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            // 
            // bt_toexcel
            // 
            this.bt_toexcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_toexcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_toexcel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_toexcel.Location = new System.Drawing.Point(451, 11);
            this.bt_toexcel.Name = "bt_toexcel";
            this.bt_toexcel.Size = new System.Drawing.Size(75, 26);
            this.bt_toexcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_toexcel.TabIndex = 8;
            this.bt_toexcel.Text = "导出excel";
            this.bt_toexcel.Click += new System.EventHandler(this.bt_toexcel_Click);
            // 
            // bt_selectwobominfo
            // 
            this.bt_selectwobominfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_selectwobominfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_selectwobominfo.Location = new System.Drawing.Point(572, 11);
            this.bt_selectwobominfo.Name = "bt_selectwobominfo";
            this.bt_selectwobominfo.Size = new System.Drawing.Size(101, 26);
            this.bt_selectwobominfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_selectwobominfo.TabIndex = 7;
            this.bt_selectwobominfo.Text = "导入SAP备料表";
            this.bt_selectwobominfo.Click += new System.EventHandler(this.bt_selectwobominfo_Click);
            // 
            // cb_process
            // 
            this.cb_process.FormattingEnabled = true;
            this.cb_process.Items.AddRange(new object[] {
            "SMD",
            "DIP"});
            this.cb_process.Location = new System.Drawing.Point(203, 14);
            this.cb_process.Name = "cb_process";
            this.cb_process.Size = new System.Drawing.Size(46, 20);
            this.cb_process.TabIndex = 6;
            this.cb_process.Text = "SMD";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "制程";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "工单号";
            // 
            // tb_woid
            // 
            this.tb_woid.Location = new System.Drawing.Point(62, 14);
            this.tb_woid.Name = "tb_woid";
            this.tb_woid.Size = new System.Drawing.Size(100, 21);
            this.tb_woid.TabIndex = 1;
            this.tb_woid.Leave += new System.EventHandler(this.tb_woid_Leave);
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.woid,
            this.kpnumber,
            this.storehouseid,
            this.locid,
            this.qty});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.ReadOnly = true;
            this.dataGridViewX1.RowTemplate.Height = 23;
            this.dataGridViewX1.Size = new System.Drawing.Size(702, 380);
            this.dataGridViewX1.TabIndex = 0;
            // 
            // woid
            // 
            this.woid.DataPropertyName = "remark";
            this.woid.HeaderText = "工单";
            this.woid.Name = "woid";
            this.woid.ReadOnly = true;
            // 
            // kpnumber
            // 
            this.kpnumber.DataPropertyName = "kpnumber";
            this.kpnumber.HeaderText = "料号";
            this.kpnumber.Name = "kpnumber";
            this.kpnumber.ReadOnly = true;
            // 
            // storehouseid
            // 
            this.storehouseid.DataPropertyName = "storehouseId";
            this.storehouseid.HeaderText = "仓库";
            this.storehouseid.Name = "storehouseid";
            this.storehouseid.ReadOnly = true;
            // 
            // locid
            // 
            this.locid.DataPropertyName = "locId";
            this.locid.HeaderText = "库位";
            this.locid.Name = "locid";
            this.locid.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "数量";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.dataGridViewX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 53);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(702, 380);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 2;
            // 
            // FrmSentMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 433);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.Name = "FrmSentMaterial";
            this.Text = "发料表";
            this.Load += new System.EventHandler(this.FrmSentMaterial_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX bt_select;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.TextBox tb_woid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_process;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.ButtonX bt_selectwobominfo;
        private DevComponents.DotNetBar.ButtonX bt_toexcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn woid;
        private System.Windows.Forms.DataGridViewTextBoxColumn kpnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehouseid;
        private System.Windows.Forms.DataGridViewTextBoxColumn locid;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
    }
}