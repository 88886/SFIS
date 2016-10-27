namespace SFIS_V2
{
    partial class ExcelToDb
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelToDb));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.ip_linenamelist = new DevComponents.DotNetBar.ItemPanel();
            this.dgv_had = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgv_dta = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.menuBar = new DevComponents.DotNetBar.Bar();
            this.mbt_OpenExcel = new DevComponents.DotNetBar.ButtonItem();
            this.bt_excelTodb = new DevComponents.DotNetBar.ButtonItem();
            this.产品料号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.产品描述 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.机器编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOM版本 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.料站总数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.料站数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PCB面 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_had)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuBar)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_dta);
            this.splitContainer1.Size = new System.Drawing.Size(802, 534);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupPanel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgv_had);
            this.splitContainer2.Size = new System.Drawing.Size(802, 227);
            this.splitContainer2.SplitterDistance = 296;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.ip_linenamelist);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(296, 227);
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
            // 
            // ip_linenamelist
            // 
            // 
            // 
            // 
            this.ip_linenamelist.BackgroundStyle.Class = "ItemPanel";
            this.ip_linenamelist.ContainerControlProcessDialogKey = true;
            this.ip_linenamelist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ip_linenamelist.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.ip_linenamelist.Location = new System.Drawing.Point(0, 0);
            this.ip_linenamelist.Name = "ip_linenamelist";
            this.ip_linenamelist.Size = new System.Drawing.Size(290, 221);
            this.ip_linenamelist.TabIndex = 1;
            this.ip_linenamelist.Text = "itemPanel1";
            this.ip_linenamelist.ItemClick += new System.EventHandler(this.ip_linenamelist_ItemClick);
            // 
            // dgv_had
            // 
            this.dgv_had.AllowUserToAddRows = false;
            this.dgv_had.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_had.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.产品料号,
            this.产品描述,
            this.机器编号,
            this.BOM版本,
            this.料站总数,
            this.料站数,
            this.PCB面});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_had.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_had.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_had.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_had.Location = new System.Drawing.Point(0, 0);
            this.dgv_had.Name = "dgv_had";
            this.dgv_had.ReadOnly = true;
            this.dgv_had.RowTemplate.Height = 23;
            this.dgv_had.Size = new System.Drawing.Size(502, 227);
            this.dgv_had.TabIndex = 0;
            this.dgv_had.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_had_CellMouseLeave);
            this.dgv_had.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_had_CellMouseEnter);
            // 
            // dgv_dta
            // 
            this.dgv_dta.AllowUserToAddRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_dta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_dta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_dta.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_dta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_dta.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_dta.Location = new System.Drawing.Point(0, 0);
            this.dgv_dta.Name = "dgv_dta";
            this.dgv_dta.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_dta.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_dta.RowTemplate.Height = 23;
            this.dgv_dta.Size = new System.Drawing.Size(802, 303);
            this.dgv_dta.TabIndex = 0;
            this.dgv_dta.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_dta_CellMouseLeave);
            this.dgv_dta.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_dta_CellMouseEnter);
            // 
            // menuBar
            // 
            this.menuBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mbt_OpenExcel,
            this.bt_excelTodb});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(802, 27);
            this.menuBar.Stretch = true;
            this.menuBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.menuBar.TabIndex = 2;
            this.menuBar.TabStop = false;
            this.menuBar.Text = "bar1";
            // 
            // mbt_OpenExcel
            // 
            this.mbt_OpenExcel.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.mbt_OpenExcel.Image = ((System.Drawing.Image)(resources.GetObject("mbt_OpenExcel.Image")));
            this.mbt_OpenExcel.Name = "mbt_OpenExcel";
            this.mbt_OpenExcel.Text = "加载料站表(Excel)文件";
            this.mbt_OpenExcel.Click += new System.EventHandler(this.mbt_OpenExcel_Click);
            // 
            // bt_excelTodb
            // 
            this.bt_excelTodb.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.bt_excelTodb.Image = ((System.Drawing.Image)(resources.GetObject("bt_excelTodb.Image")));
            this.bt_excelTodb.Name = "bt_excelTodb";
            this.bt_excelTodb.Text = "将料站表导入系统";
            this.bt_excelTodb.Click += new System.EventHandler(this.bt_excelTodb_Click);
            // 
            // 产品料号
            // 
            this.产品料号.DataPropertyName = "产品料号";
            this.产品料号.HeaderText = "产品料号";
            this.产品料号.Name = "产品料号";
            this.产品料号.ReadOnly = true;
            // 
            // 产品描述
            // 
            this.产品描述.DataPropertyName = "产品描述";
            this.产品描述.HeaderText = "产品描述";
            this.产品描述.Name = "产品描述";
            this.产品描述.ReadOnly = true;
            // 
            // 机器编号
            // 
            this.机器编号.DataPropertyName = "机器编号";
            this.机器编号.HeaderText = "机器编号";
            this.机器编号.Name = "机器编号";
            this.机器编号.ReadOnly = true;
            // 
            // BOM版本
            // 
            this.BOM版本.DataPropertyName = "BOM版本";
            this.BOM版本.HeaderText = "BOM版本";
            this.BOM版本.Name = "BOM版本";
            this.BOM版本.ReadOnly = true;
            // 
            // 料站总数
            // 
            this.料站总数.DataPropertyName = "料站总数";
            this.料站总数.HeaderText = "料站总数";
            this.料站总数.Name = "料站总数";
            this.料站总数.ReadOnly = true;
            // 
            // 料站数
            // 
            this.料站数.DataPropertyName = "料站数";
            this.料站数.HeaderText = "料站数";
            this.料站数.Name = "料站数";
            this.料站数.ReadOnly = true;
            this.料站数.Visible = false;
            // 
            // PCB面
            // 
            this.PCB面.DataPropertyName = "PCB面";
            this.PCB面.HeaderText = "PCB面";
            this.PCB面.Name = "PCB面";
            this.PCB面.ReadOnly = true;
            // 
            // ExcelToDb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuBar);
            this.Name = "ExcelToDb";
            this.Text = "导入料站表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ExcelToDb_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_had)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevComponents.DotNetBar.Bar menuBar;
        private DevComponents.DotNetBar.ButtonItem mbt_OpenExcel;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_dta;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_had;
        private DevComponents.DotNetBar.ItemPanel ip_linenamelist;
        private DevComponents.DotNetBar.ButtonItem bt_excelTodb;
        private System.Windows.Forms.DataGridViewTextBoxColumn 产品料号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 产品描述;
        private System.Windows.Forms.DataGridViewTextBoxColumn 机器编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn BOM版本;
        private System.Windows.Forms.DataGridViewTextBoxColumn 料站总数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 料站数;
        private System.Windows.Forms.DataGridViewTextBoxColumn PCB面;
    }
}