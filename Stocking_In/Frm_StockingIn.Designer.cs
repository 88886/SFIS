namespace Stocking_In
{
    partial class Frm_StockingIn
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.imbt_SetConfig = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_setLineStation = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_CheckRoute = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_CheckSAP = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_RePrint = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_UpLoadData = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.LabError = new System.Windows.Forms.Label();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.panelEx11 = new DevComponents.DotNetBar.PanelEx();
            this.txt_InputStr = new System.Windows.Forms.TextBox();
            this.panelEx12 = new DevComponents.DotNetBar.PanelEx();
            this.LabMyGroup = new DevComponents.DotNetBar.LabelX();
            this.LabLine = new DevComponents.DotNetBar.LabelX();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dataGridEmp = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx8 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBoxCarton = new System.Windows.Forms.ListBox();
            this.dataGridCarton = new System.Windows.Forms.DataGridView();
            this.panelEx7 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxEsn = new System.Windows.Forms.ListBox();
            this.dataGridEsn = new System.Windows.Forms.DataGridView();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx10 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBoxPallet = new System.Windows.Forms.ListBox();
            this.dataGridPallet = new System.Windows.Forms.DataGridView();
            this.panelEx9 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxTray = new System.Windows.Forms.ListBox();
            this.dataGridTray = new System.Windows.Forms.DataGridView();
            this.PrintStockInNo = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.panelEx11.SuspendLayout();
            this.panelEx12.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmp)).BeginInit();
            this.panelEx5.SuspendLayout();
            this.panelEx8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCarton)).BeginInit();
            this.panelEx7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEsn)).BeginInit();
            this.panelEx6.SuspendLayout();
            this.panelEx10.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPallet)).BeginInit();
            this.panelEx9.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTray)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.imbt_SetConfig,
            this.imbt_UpLoadData});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(919, 27);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // imbt_SetConfig
            // 
            this.imbt_SetConfig.Name = "imbt_SetConfig";
            this.imbt_SetConfig.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.imbt_setLineStation,
            this.imbt_CheckRoute,
            this.imbt_CheckSAP,
            this.imbt_RePrint});
            this.imbt_SetConfig.Text = "设置";
            this.imbt_SetConfig.Click += new System.EventHandler(this.imbt_SetConfig_Click);
            // 
            // imbt_setLineStation
            // 
            this.imbt_setLineStation.Name = "imbt_setLineStation";
            this.imbt_setLineStation.Text = "设置线体与途程";
            this.imbt_setLineStation.Click += new System.EventHandler(this.imbt_setLineStation_Click);
            // 
            // imbt_CheckRoute
            // 
            this.imbt_CheckRoute.Checked = true;
            this.imbt_CheckRoute.Enabled = false;
            this.imbt_CheckRoute.Name = "imbt_CheckRoute";
            this.imbt_CheckRoute.Text = "检查途程";
            // 
            // imbt_CheckSAP
            // 
            this.imbt_CheckSAP.Checked = true;
            this.imbt_CheckSAP.Name = "imbt_CheckSAP";
            this.imbt_CheckSAP.Text = "检查SAP";
            this.imbt_CheckSAP.Click += new System.EventHandler(this.imbt_CheckSAP_Click);
            // 
            // imbt_RePrint
            // 
            this.imbt_RePrint.Name = "imbt_RePrint";
            this.imbt_RePrint.Text = "重复打印单据";
            this.imbt_RePrint.Click += new System.EventHandler(this.imbt_RePrint_Click);
            // 
            // imbt_UpLoadData
            // 
            this.imbt_UpLoadData.Name = "imbt_UpLoadData";
            this.imbt_UpLoadData.Text = "上抛未完成单据";
            this.imbt_UpLoadData.Click += new System.EventHandler(this.imbt_UpLoadData_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.LabError);
            this.panelEx1.Controls.Add(this.progressBarX1);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx1.Location = new System.Drawing.Point(0, 488);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(919, 76);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            // 
            // LabError
            // 
            this.LabError.AutoSize = true;
            this.LabError.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabError.ForeColor = System.Drawing.Color.Red;
            this.LabError.Location = new System.Drawing.Point(369, 20);
            this.LabError.Name = "LabError";
            this.LabError.Size = new System.Drawing.Size(106, 21);
            this.LabError.TabIndex = 1;
            this.LabError.Text = "NG COUNT";
            this.LabError.Visible = false;
            // 
            // progressBarX1
            // 
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarX1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarX1.Location = new System.Drawing.Point(0, 53);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.Size = new System.Drawing.Size(919, 23);
            this.progressBarX1.TabIndex = 0;
            this.progressBarX1.Text = "0";
            this.progressBarX1.TextVisible = true;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panelEx4);
            this.panelEx2.Controls.Add(this.panelEx3);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx2.Location = new System.Drawing.Point(0, 27);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(286, 461);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 2;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.rtbmsg);
            this.panelEx4.Controls.Add(this.panelEx11);
            this.panelEx4.Controls.Add(this.panelEx12);
            this.panelEx4.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(286, 461);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 1;
            // 
            // rtbmsg
            // 
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbmsg.Location = new System.Drawing.Point(0, 374);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.ReadOnly = true;
            this.rtbmsg.Size = new System.Drawing.Size(286, 87);
            this.rtbmsg.TabIndex = 3;
            this.rtbmsg.Text = "";
            // 
            // panelEx11
            // 
            this.panelEx11.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx11.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx11.Controls.Add(this.txt_InputStr);
            this.panelEx11.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx11.Location = new System.Drawing.Point(0, 320);
            this.panelEx11.Name = "panelEx11";
            this.panelEx11.Size = new System.Drawing.Size(286, 54);
            this.panelEx11.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx11.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx11.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx11.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx11.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx11.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx11.Style.GradientAngle = 90;
            this.panelEx11.TabIndex = 1;
            // 
            // txt_InputStr
            // 
            this.txt_InputStr.Location = new System.Drawing.Point(12, 14);
            this.txt_InputStr.Name = "txt_InputStr";
            this.txt_InputStr.Size = new System.Drawing.Size(216, 21);
            this.txt_InputStr.TabIndex = 0;
            this.txt_InputStr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_InputStr_KeyDown);
            // 
            // panelEx12
            // 
            this.panelEx12.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx12.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx12.Controls.Add(this.LabMyGroup);
            this.panelEx12.Controls.Add(this.LabLine);
            this.panelEx12.Controls.Add(this.groupBox5);
            this.panelEx12.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx12.Location = new System.Drawing.Point(0, 0);
            this.panelEx12.Name = "panelEx12";
            this.panelEx12.Size = new System.Drawing.Size(286, 320);
            this.panelEx12.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx12.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx12.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx12.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx12.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx12.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx12.Style.GradientAngle = 90;
            this.panelEx12.TabIndex = 2;
            // 
            // LabMyGroup
            // 
            // 
            // 
            // 
            this.LabMyGroup.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabMyGroup.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabMyGroup.Location = new System.Drawing.Point(34, 198);
            this.LabMyGroup.Name = "LabMyGroup";
            this.LabMyGroup.Size = new System.Drawing.Size(202, 35);
            this.LabMyGroup.TabIndex = 2;
            this.LabMyGroup.Text = "MyGroup";
            // 
            // LabLine
            // 
            // 
            // 
            // 
            this.LabLine.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabLine.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabLine.Location = new System.Drawing.Point(34, 157);
            this.LabLine.Name = "LabLine";
            this.LabLine.Size = new System.Drawing.Size(194, 35);
            this.LabLine.TabIndex = 2;
            this.LabLine.Text = "Line";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dataGridEmp);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(286, 140);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "员工信息";
            // 
            // dataGridEmp
            // 
            this.dataGridEmp.AllowUserToAddRows = false;
            this.dataGridEmp.AllowUserToDeleteRows = false;
            this.dataGridEmp.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridEmp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridEmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEmp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridEmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridEmp.Enabled = false;
            this.dataGridEmp.Location = new System.Drawing.Point(3, 17);
            this.dataGridEmp.Name = "dataGridEmp";
            this.dataGridEmp.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridEmp.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridEmp.RowHeadersVisible = false;
            this.dataGridEmp.RowTemplate.Height = 23;
            this.dataGridEmp.Size = new System.Drawing.Size(280, 120);
            this.dataGridEmp.TabIndex = 0;
            this.dataGridEmp.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridEmp_CellFormatting);
            this.dataGridEmp.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridEmp_DataBindingComplete);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "员工工号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "员工";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "扫描指令";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 80;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(286, 461);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 0;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.panelEx8);
            this.panelEx5.Controls.Add(this.panelEx7);
            this.panelEx5.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx5.Location = new System.Drawing.Point(286, 27);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(301, 461);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 3;
            this.panelEx5.Text = "panelEx5";
            // 
            // panelEx8
            // 
            this.panelEx8.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx8.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx8.Controls.Add(this.groupBox3);
            this.panelEx8.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx8.Location = new System.Drawing.Point(0, 195);
            this.panelEx8.Name = "panelEx8";
            this.panelEx8.Size = new System.Drawing.Size(301, 266);
            this.panelEx8.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx8.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx8.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx8.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx8.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx8.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx8.Style.GradientAngle = 90;
            this.panelEx8.TabIndex = 1;
            this.panelEx8.Text = "panelEx8";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBoxCarton);
            this.groupBox3.Controls.Add(this.dataGridCarton);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(301, 266);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CARTON";
            // 
            // listBoxCarton
            // 
            this.listBoxCarton.FormattingEnabled = true;
            this.listBoxCarton.ItemHeight = 12;
            this.listBoxCarton.Location = new System.Drawing.Point(110, 49);
            this.listBoxCarton.Name = "listBoxCarton";
            this.listBoxCarton.Size = new System.Drawing.Size(125, 76);
            this.listBoxCarton.TabIndex = 3;
            this.listBoxCarton.Visible = false;
            // 
            // dataGridCarton
            // 
            this.dataGridCarton.AllowUserToAddRows = false;
            this.dataGridCarton.AllowUserToDeleteRows = false;
            this.dataGridCarton.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridCarton.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCarton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridCarton.Enabled = false;
            this.dataGridCarton.Location = new System.Drawing.Point(3, 17);
            this.dataGridCarton.Name = "dataGridCarton";
            this.dataGridCarton.ReadOnly = true;
            this.dataGridCarton.RowHeadersVisible = false;
            this.dataGridCarton.RowTemplate.Height = 23;
            this.dataGridCarton.Size = new System.Drawing.Size(295, 246);
            this.dataGridCarton.TabIndex = 1;
            // 
            // panelEx7
            // 
            this.panelEx7.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx7.Controls.Add(this.groupBox1);
            this.panelEx7.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx7.Location = new System.Drawing.Point(0, 0);
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Size = new System.Drawing.Size(301, 195);
            this.panelEx7.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx7.Style.GradientAngle = 90;
            this.panelEx7.TabIndex = 0;
            this.panelEx7.Text = "panelEx7";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxEsn);
            this.groupBox1.Controls.Add(this.dataGridEsn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 195);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ESN";
            // 
            // listBoxEsn
            // 
            this.listBoxEsn.FormattingEnabled = true;
            this.listBoxEsn.ItemHeight = 12;
            this.listBoxEsn.Location = new System.Drawing.Point(110, 64);
            this.listBoxEsn.Name = "listBoxEsn";
            this.listBoxEsn.Size = new System.Drawing.Size(125, 76);
            this.listBoxEsn.TabIndex = 3;
            this.listBoxEsn.Visible = false;
            // 
            // dataGridEsn
            // 
            this.dataGridEsn.AllowUserToAddRows = false;
            this.dataGridEsn.AllowUserToDeleteRows = false;
            this.dataGridEsn.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridEsn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEsn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridEsn.Enabled = false;
            this.dataGridEsn.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridEsn.Location = new System.Drawing.Point(3, 17);
            this.dataGridEsn.Name = "dataGridEsn";
            this.dataGridEsn.ReadOnly = true;
            this.dataGridEsn.RowHeadersVisible = false;
            this.dataGridEsn.RowTemplate.Height = 23;
            this.dataGridEsn.Size = new System.Drawing.Size(295, 175);
            this.dataGridEsn.TabIndex = 2;
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.panelEx10);
            this.panelEx6.Controls.Add(this.panelEx9);
            this.panelEx6.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx6.Location = new System.Drawing.Point(587, 27);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(332, 461);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 4;
            this.panelEx6.Text = "panelEx6";
            // 
            // panelEx10
            // 
            this.panelEx10.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx10.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx10.Controls.Add(this.groupBox4);
            this.panelEx10.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx10.Location = new System.Drawing.Point(0, 195);
            this.panelEx10.Name = "panelEx10";
            this.panelEx10.Size = new System.Drawing.Size(332, 266);
            this.panelEx10.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx10.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx10.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx10.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx10.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx10.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx10.Style.GradientAngle = 90;
            this.panelEx10.TabIndex = 1;
            this.panelEx10.Text = "panelEx10";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listBoxPallet);
            this.groupBox4.Controls.Add(this.dataGridPallet);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(332, 266);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PALLET";
            // 
            // listBoxPallet
            // 
            this.listBoxPallet.FormattingEnabled = true;
            this.listBoxPallet.ItemHeight = 12;
            this.listBoxPallet.Location = new System.Drawing.Point(96, 49);
            this.listBoxPallet.Name = "listBoxPallet";
            this.listBoxPallet.Size = new System.Drawing.Size(125, 76);
            this.listBoxPallet.TabIndex = 3;
            this.listBoxPallet.Visible = false;
            // 
            // dataGridPallet
            // 
            this.dataGridPallet.AllowUserToAddRows = false;
            this.dataGridPallet.AllowUserToDeleteRows = false;
            this.dataGridPallet.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridPallet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPallet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridPallet.Enabled = false;
            this.dataGridPallet.Location = new System.Drawing.Point(3, 17);
            this.dataGridPallet.Name = "dataGridPallet";
            this.dataGridPallet.ReadOnly = true;
            this.dataGridPallet.RowHeadersVisible = false;
            this.dataGridPallet.RowTemplate.Height = 23;
            this.dataGridPallet.Size = new System.Drawing.Size(326, 246);
            this.dataGridPallet.TabIndex = 1;
            // 
            // panelEx9
            // 
            this.panelEx9.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx9.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx9.Controls.Add(this.groupBox2);
            this.panelEx9.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx9.Location = new System.Drawing.Point(0, 0);
            this.panelEx9.Name = "panelEx9";
            this.panelEx9.Size = new System.Drawing.Size(332, 195);
            this.panelEx9.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx9.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx9.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx9.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx9.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx9.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx9.Style.GradientAngle = 90;
            this.panelEx9.TabIndex = 0;
            this.panelEx9.Text = "panelEx9";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxTray);
            this.groupBox2.Controls.Add(this.dataGridTray);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 195);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tray";
            // 
            // listBoxTray
            // 
            this.listBoxTray.FormattingEnabled = true;
            this.listBoxTray.ItemHeight = 12;
            this.listBoxTray.Location = new System.Drawing.Point(96, 64);
            this.listBoxTray.Name = "listBoxTray";
            this.listBoxTray.Size = new System.Drawing.Size(125, 76);
            this.listBoxTray.TabIndex = 3;
            this.listBoxTray.Visible = false;
            // 
            // dataGridTray
            // 
            this.dataGridTray.AllowUserToAddRows = false;
            this.dataGridTray.AllowUserToDeleteRows = false;
            this.dataGridTray.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridTray.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridTray.Enabled = false;
            this.dataGridTray.Location = new System.Drawing.Point(3, 17);
            this.dataGridTray.Name = "dataGridTray";
            this.dataGridTray.ReadOnly = true;
            this.dataGridTray.RowHeadersVisible = false;
            this.dataGridTray.RowTemplate.Height = 23;
            this.dataGridTray.Size = new System.Drawing.Size(326, 175);
            this.dataGridTray.TabIndex = 1;
            // 
            // PrintStockInNo
            // 
            this.PrintStockInNo.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintStockInNo_PrintPage);
            // 
            // Frm_StockingIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(919, 564);
            this.Controls.Add(this.panelEx6);
            this.Controls.Add(this.panelEx5);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.bar1);
            this.DoubleBuffered = true;
            this.Name = "Frm_StockingIn";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_StockingIn_FormClosing);
            this.Load += new System.EventHandler(this.Frm_StockingIn_Load);
            this.Resize += new System.EventHandler(this.Frm_StockingIn_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.panelEx11.ResumeLayout(false);
            this.panelEx11.PerformLayout();
            this.panelEx12.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmp)).EndInit();
            this.panelEx5.ResumeLayout(false);
            this.panelEx8.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCarton)).EndInit();
            this.panelEx7.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEsn)).EndInit();
            this.panelEx6.ResumeLayout(false);
            this.panelEx10.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPallet)).EndInit();
            this.panelEx9.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTray)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem imbt_SetConfig;
        private DevComponents.DotNetBar.ButtonItem imbt_setLineStation;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.PanelEx panelEx8;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private DevComponents.DotNetBar.PanelEx panelEx10;
        private DevComponents.DotNetBar.PanelEx panelEx9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridCarton;
        private System.Windows.Forms.DataGridView dataGridPallet;
        private System.Windows.Forms.DataGridView dataGridTray;
        private System.Windows.Forms.DataGridView dataGridEsn;
        private DevComponents.DotNetBar.PanelEx panelEx11;
        private System.Windows.Forms.RichTextBox rtbmsg;
        private System.Windows.Forms.TextBox txt_InputStr;
        private DevComponents.DotNetBar.PanelEx panelEx12;
        private System.Windows.Forms.DataGridView dataGridEmp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.ListBox listBoxCarton;
        private System.Windows.Forms.ListBox listBoxEsn;
        private System.Windows.Forms.ListBox listBoxPallet;
        private System.Windows.Forms.ListBox listBoxTray;
        private System.Windows.Forms.GroupBox groupBox5;
        private DevComponents.DotNetBar.LabelX LabMyGroup;
        private DevComponents.DotNetBar.LabelX LabLine;
        private DevComponents.DotNetBar.ButtonItem imbt_CheckRoute;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarX1;
        private System.Windows.Forms.Label LabError;
        private DevComponents.DotNetBar.ButtonItem imbt_CheckSAP;
        private System.Drawing.Printing.PrintDocument PrintStockInNo;
        private DevComponents.DotNetBar.ButtonItem imbt_RePrint;
        private DevComponents.DotNetBar.ButtonItem imbt_UpLoadData;

    }
}

