namespace SFIS_V2
{
    partial class Frm_ColorBoxPrint
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
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.imbt_Option = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_SelectLine = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_Reprint = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_SamplingUint1 = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_Esn = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_imei = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_LabelPatch = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_LabelPartnumber = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_LabelwoId = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.txt_sn = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt_woId = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.LabSwVer = new DevComponents.DotNetBar.LabelX();
            this.NumPrintQty = new System.Windows.Forms.NumericUpDown();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.labSamplingUint = new DevComponents.DotNetBar.LabelX();
            this.txt_InputData = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.LabColor = new DevComponents.DotNetBar.LabelX();
            this.LabModelDesc = new DevComponents.DotNetBar.LabelX();
            this.LabPartnumber = new DevComponents.DotNetBar.LabelX();
            this.panelStation = new DevComponents.DotNetBar.PanelEx();
            this.LabStation = new DevComponents.DotNetBar.LabelX();
            this.LabwoId = new DevComponents.DotNetBar.LabelX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.LabFwVer = new DevComponents.DotNetBar.LabelX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.imbt_LabelPatchSelect = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumPrintQty)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            this.panelStation.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.expandablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX7.Location = new System.Drawing.Point(24, 65);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(42, 23);
            this.labelX7.TabIndex = 0;
            this.labelX7.Text = "SN:";
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.imbt_Option});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(968, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 15;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // imbt_Option
            // 
            this.imbt_Option.Name = "imbt_Option";
            this.imbt_Option.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.imbt_SelectLine,
            this.imbt_Reprint,
            this.imbt_SamplingUint1,
            this.imbt_LabelPatch});
            this.imbt_Option.Text = "选项";
            // 
            // imbt_SelectLine
            // 
            this.imbt_SelectLine.Name = "imbt_SelectLine";
            this.imbt_SelectLine.Stretch = true;
            this.imbt_SelectLine.Text = "选择线体";
            this.imbt_SelectLine.Click += new System.EventHandler(this.imbt_SelectLine_Click);
            // 
            // imbt_Reprint
            // 
            this.imbt_Reprint.Name = "imbt_Reprint";
            this.imbt_Reprint.Text = "重复打印";
            this.imbt_Reprint.Click += new System.EventHandler(this.imbt_Reprint_Click);
            // 
            // imbt_SamplingUint1
            // 
            this.imbt_SamplingUint1.Name = "imbt_SamplingUint1";
            this.imbt_SamplingUint1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.imbt_Esn,
            this.imbt_imei});
            this.imbt_SamplingUint1.Text = "输入类型";
            // 
            // imbt_Esn
            // 
            this.imbt_Esn.Checked = true;
            this.imbt_Esn.Name = "imbt_Esn";
            this.imbt_Esn.Text = "ESN";
            this.imbt_Esn.Click += new System.EventHandler(this.imbt_Esn_Click);
            // 
            // imbt_imei
            // 
            this.imbt_imei.Name = "imbt_imei";
            this.imbt_imei.Text = "IMEI";
            this.imbt_imei.Click += new System.EventHandler(this.imbt_imei_Click);
            // 
            // imbt_LabelPatch
            // 
            this.imbt_LabelPatch.Name = "imbt_LabelPatch";
            this.imbt_LabelPatch.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.imbt_LabelPartnumber,
            this.imbt_LabelwoId,
            this.imbt_LabelPatchSelect});
            this.imbt_LabelPatch.Text = "条码路径";
            // 
            // imbt_LabelPartnumber
            // 
            this.imbt_LabelPartnumber.Checked = true;
            this.imbt_LabelPartnumber.Name = "imbt_LabelPartnumber";
            this.imbt_LabelPartnumber.Text = "以料号获取";
            this.imbt_LabelPartnumber.Click += new System.EventHandler(this.imbt_LabelPartnumber_Click);
            // 
            // imbt_LabelwoId
            // 
            this.imbt_LabelwoId.Name = "imbt_LabelwoId";
            this.imbt_LabelwoId.Text = "以工单获取";
            this.imbt_LabelwoId.Click += new System.EventHandler(this.imbt_LabelwoId_Click);
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.txt_sn);
            this.panelEx4.Controls.Add(this.txt_woId);
            this.panelEx4.Controls.Add(this.labelX7);
            this.panelEx4.Controls.Add(this.labelX3);
            this.panelEx4.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx4.Location = new System.Drawing.Point(356, 51);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(272, 123);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 1;
            this.panelEx4.Visible = false;
            // 
            // txt_sn
            // 
            // 
            // 
            // 
            this.txt_sn.Border.Class = "TextBoxBorder";
            this.txt_sn.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_sn.Location = new System.Drawing.Point(77, 67);
            this.txt_sn.Name = "txt_sn";
            this.txt_sn.PreventEnterBeep = true;
            this.txt_sn.Size = new System.Drawing.Size(161, 21);
            this.txt_sn.TabIndex = 0;
            this.txt_sn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sn_KeyDown);
            // 
            // txt_woId
            // 
            // 
            // 
            // 
            this.txt_woId.Border.Class = "TextBoxBorder";
            this.txt_woId.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_woId.Location = new System.Drawing.Point(77, 19);
            this.txt_woId.Name = "txt_woId";
            this.txt_woId.PreventEnterBeep = true;
            this.txt_woId.Size = new System.Drawing.Size(161, 21);
            this.txt_woId.TabIndex = 0;
            this.txt_woId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_woId_KeyDown);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(23, 19);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(36, 23);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "MO:";
            // 
            // LabSwVer
            // 
            // 
            // 
            // 
            this.LabSwVer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabSwVer.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabSwVer.Location = new System.Drawing.Point(151, 202);
            this.LabSwVer.Name = "LabSwVer";
            this.LabSwVer.Size = new System.Drawing.Size(228, 23);
            this.LabSwVer.TabIndex = 0;
            this.LabSwVer.Text = "0000000000";
            this.LabSwVer.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // NumPrintQty
            // 
            this.NumPrintQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.NumPrintQty.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NumPrintQty.Location = new System.Drawing.Point(301, 58);
            this.NumPrintQty.Name = "NumPrintQty";
            this.NumPrintQty.Size = new System.Drawing.Size(78, 23);
            this.NumPrintQty.TabIndex = 2;
            this.NumPrintQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumPrintQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.labSamplingUint);
            this.panelEx2.Controls.Add(this.txt_InputData);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(307, 400);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // labSamplingUint
            // 
            // 
            // 
            // 
            this.labSamplingUint.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labSamplingUint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labSamplingUint.Location = new System.Drawing.Point(47, 38);
            this.labSamplingUint.Name = "labSamplingUint";
            this.labSamplingUint.Size = new System.Drawing.Size(63, 23);
            this.labSamplingUint.TabIndex = 1;
            this.labSamplingUint.Text = "[输入]";
            // 
            // txt_InputData
            // 
            // 
            // 
            // 
            this.txt_InputData.Border.Class = "TextBoxBorder";
            this.txt_InputData.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_InputData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_InputData.Location = new System.Drawing.Point(47, 67);
            this.txt_InputData.Name = "txt_InputData";
            this.txt_InputData.PreventEnterBeep = true;
            this.txt_InputData.Size = new System.Drawing.Size(225, 26);
            this.txt_InputData.TabIndex = 0;
            this.txt_InputData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_InputData_KeyDown);
            // 
            // numY
            // 
            this.numY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.numY.DecimalPlaces = 2;
            this.numY.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numY.Location = new System.Drawing.Point(151, 57);
            this.numY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(78, 23);
            this.numY.TabIndex = 2;
            this.numY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numY.Leave += new System.EventHandler(this.numY_Leave);
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX11.Location = new System.Drawing.Point(151, 34);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(97, 23);
            this.labelX11.TabIndex = 1;
            this.labelX11.Text = "[打印坐标Y]";
            // 
            // numX
            // 
            this.numX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.numX.DecimalPlaces = 2;
            this.numX.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numX.Location = new System.Drawing.Point(51, 58);
            this.numX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(78, 23);
            this.numX.TabIndex = 2;
            this.numX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numX.Leave += new System.EventHandler(this.numX_Leave);
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX10.Location = new System.Drawing.Point(301, 34);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(97, 23);
            this.labelX10.TabIndex = 1;
            this.labelX10.Text = "[打印数量]";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.Location = new System.Drawing.Point(51, 33);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(97, 23);
            this.labelX5.TabIndex = 1;
            this.labelX5.Text = "[打印坐标X]";
            // 
            // LabColor
            // 
            // 
            // 
            // 
            this.LabColor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabColor.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabColor.Location = new System.Drawing.Point(151, 151);
            this.LabColor.Name = "LabColor";
            this.LabColor.Size = new System.Drawing.Size(228, 23);
            this.LabColor.TabIndex = 0;
            this.LabColor.Text = "0000000000";
            this.LabColor.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // LabModelDesc
            // 
            // 
            // 
            // 
            this.LabModelDesc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabModelDesc.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabModelDesc.Location = new System.Drawing.Point(151, 107);
            this.LabModelDesc.Name = "LabModelDesc";
            this.LabModelDesc.Size = new System.Drawing.Size(228, 23);
            this.LabModelDesc.TabIndex = 0;
            this.LabModelDesc.Text = "0000000000";
            this.LabModelDesc.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // LabPartnumber
            // 
            // 
            // 
            // 
            this.LabPartnumber.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabPartnumber.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabPartnumber.Location = new System.Drawing.Point(151, 59);
            this.LabPartnumber.Name = "LabPartnumber";
            this.LabPartnumber.Size = new System.Drawing.Size(228, 23);
            this.LabPartnumber.TabIndex = 0;
            this.LabPartnumber.Text = "0000000000";
            this.LabPartnumber.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // panelStation
            // 
            this.panelStation.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelStation.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelStation.Controls.Add(this.LabStation);
            this.panelStation.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelStation.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStation.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelStation.Location = new System.Drawing.Point(0, 25);
            this.panelStation.Name = "panelStation";
            this.panelStation.Size = new System.Drawing.Size(968, 56);
            this.panelStation.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelStation.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelStation.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelStation.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelStation.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelStation.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelStation.Style.GradientAngle = 90;
            this.panelStation.TabIndex = 16;
            // 
            // LabStation
            // 
            // 
            // 
            // 
            this.LabStation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabStation.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabStation.ForeColor = System.Drawing.Color.Blue;
            this.LabStation.Location = new System.Drawing.Point(0, 0);
            this.LabStation.Name = "LabStation";
            this.LabStation.SingleLineColor = System.Drawing.SystemColors.AppWorkspace;
            this.LabStation.Size = new System.Drawing.Size(968, 56);
            this.LabStation.TabIndex = 0;
            this.LabStation.Text = "S080101:PACK_CTN";
            this.LabStation.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // LabwoId
            // 
            // 
            // 
            // 
            this.LabwoId.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabwoId.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabwoId.Location = new System.Drawing.Point(151, 16);
            this.LabwoId.Name = "LabwoId";
            this.LabwoId.Size = new System.Drawing.Size(228, 23);
            this.LabwoId.TabIndex = 0;
            this.LabwoId.Text = "0000000000";
            this.LabwoId.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.panelEx4);
            this.panelEx3.Controls.Add(this.LabFwVer);
            this.panelEx3.Controls.Add(this.LabSwVer);
            this.panelEx3.Controls.Add(this.LabColor);
            this.panelEx3.Controls.Add(this.LabModelDesc);
            this.panelEx3.Controls.Add(this.LabPartnumber);
            this.panelEx3.Controls.Add(this.labelX12);
            this.panelEx3.Controls.Add(this.LabwoId);
            this.panelEx3.Controls.Add(this.labelX8);
            this.panelEx3.Controls.Add(this.labelX6);
            this.panelEx3.Controls.Add(this.labelX4);
            this.panelEx3.Controls.Add(this.labelX2);
            this.panelEx3.Controls.Add(this.labelX1);
            this.panelEx3.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(307, 93);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(661, 307);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 4;
            // 
            // LabFwVer
            // 
            // 
            // 
            // 
            this.LabFwVer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabFwVer.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabFwVer.Location = new System.Drawing.Point(151, 249);
            this.LabFwVer.Name = "LabFwVer";
            this.LabFwVer.Size = new System.Drawing.Size(228, 23);
            this.LabFwVer.TabIndex = 0;
            this.LabFwVer.Text = "0000000000";
            this.LabFwVer.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX12
            // 
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX12.Location = new System.Drawing.Point(42, 249);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(86, 23);
            this.labelX12.TabIndex = 0;
            this.labelX12.Text = "硬件版本:";
            this.labelX12.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX8.Location = new System.Drawing.Point(42, 202);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(86, 23);
            this.labelX8.TabIndex = 0;
            this.labelX8.Text = "软件版本:";
            this.labelX8.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX6.Location = new System.Drawing.Point(42, 151);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(86, 23);
            this.labelX6.TabIndex = 0;
            this.labelX6.Text = "产品颜色:";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(42, 107);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(86, 23);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "产品描述:";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(42, 59);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(86, 23);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "产品料号:";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(42, 16);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(86, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "工    单:";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.expandablePanel1);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 81);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(968, 400);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 17;
            this.panelEx1.Text = "panelEx1";
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.labelX10);
            this.expandablePanel1.Controls.Add(this.NumPrintQty);
            this.expandablePanel1.Controls.Add(this.numY);
            this.expandablePanel1.Controls.Add(this.labelX5);
            this.expandablePanel1.Controls.Add(this.labelX11);
            this.expandablePanel1.Controls.Add(this.numX);
            this.expandablePanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(307, 0);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(661, 93);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 2;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "打印设置";
            // 
            // imbt_LabelPatchSelect
            // 
            this.imbt_LabelPatchSelect.Name = "imbt_LabelPatchSelect";
            this.imbt_LabelPatchSelect.Text = "加载模板路径";
            this.imbt_LabelPatchSelect.Click += new System.EventHandler(this.imbt_LabelPatchSelect_Click);
            // 
            // Frm_ColorBoxPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 481);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.panelStation);
            this.Controls.Add(this.bar1);
            this.DoubleBuffered = true;
            this.Name = "Frm_ColorBoxPrint";
            this.Text = "Frm_ColorBoxPrint";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_ColorBoxPrint_FormClosing);
            this.Load += new System.EventHandler(this.Frm_ColorBoxPrint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumPrintQty)).EndInit();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            this.panelStation.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.expandablePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem imbt_Option;
        private DevComponents.DotNetBar.ButtonItem imbt_SelectLine;
        private DevComponents.DotNetBar.ButtonItem imbt_Reprint;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_sn;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_woId;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX LabSwVer;
        private System.Windows.Forms.NumericUpDown NumPrintQty;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labSamplingUint;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_InputData;
        private DevComponents.DotNetBar.LabelX LabColor;
        private DevComponents.DotNetBar.LabelX LabModelDesc;
        private DevComponents.DotNetBar.LabelX LabPartnumber;
        private DevComponents.DotNetBar.PanelEx panelStation;
        private DevComponents.DotNetBar.LabelX LabStation;
        private DevComponents.DotNetBar.LabelX LabwoId;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.NumericUpDown numY;
        private DevComponents.DotNetBar.LabelX labelX11;
        private System.Windows.Forms.NumericUpDown numX;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.ButtonItem imbt_SamplingUint1;
        private DevComponents.DotNetBar.ButtonItem imbt_Esn;
        private DevComponents.DotNetBar.ButtonItem imbt_imei;
        private DevComponents.DotNetBar.LabelX LabFwVer;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private DevComponents.DotNetBar.ButtonItem imbt_LabelPatch;
        private DevComponents.DotNetBar.ButtonItem imbt_LabelPartnumber;
        private DevComponents.DotNetBar.ButtonItem imbt_LabelwoId;
        private DevComponents.DotNetBar.ButtonItem imbt_LabelPatchSelect;
    }
}