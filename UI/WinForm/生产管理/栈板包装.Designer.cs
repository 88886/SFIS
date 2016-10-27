namespace SFIS_V2
{
    partial class FrmPallet
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
            this.components = new System.ComponentModel.Container();
            this.labListen = new DevComponents.DotNetBar.LabelX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.lblFileSend = new System.Windows.Forms.Label();
            this.lbxServer = new System.Windows.Forms.ListBox();
            this.timerByServer = new System.Windows.Forms.Timer(this.components);
            this.PalConfig = new DevComponents.DotNetBar.PanelEx();
            this.LabConfig = new DevComponents.DotNetBar.LabelX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.listctn = new System.Windows.Forms.ListBox();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labpallet = new DevComponents.DotNetBar.LabelX();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.LabCustPallet = new DevComponents.DotNetBar.LabelX();
            this.panelEx12 = new DevComponents.DotNetBar.PanelEx();
            this.labfrmMsg = new DevComponents.DotNetBar.LabelX();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.labemployee = new DevComponents.DotNetBar.LabelX();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.label8 = new System.Windows.Forms.Label();
            this.panelEx11 = new DevComponents.DotNetBar.PanelEx();
            this.labver = new DevComponents.DotNetBar.LabelX();
            this.panelEx10 = new DevComponents.DotNetBar.PanelEx();
            this.btn_ClosePallet = new DevComponents.DotNetBar.ButtonX();
            this.panelEx9 = new DevComponents.DotNetBar.PanelEx();
            this.labcount = new DevComponents.DotNetBar.LabelX();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelEx8 = new DevComponents.DotNetBar.PanelEx();
            this.labTotalCount = new DevComponents.DotNetBar.LabelX();
            this.label5 = new System.Windows.Forms.Label();
            this.panelEx7 = new DevComponents.DotNetBar.PanelEx();
            this.labwoId = new DevComponents.DotNetBar.LabelX();
            this.label4 = new System.Windows.Forms.Label();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.labPartnumber = new DevComponents.DotNetBar.LabelX();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmesn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmcarton = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmkeyparts = new System.Windows.Forms.ToolStripMenuItem();
            this.rePrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReprintPallet = new System.Windows.Forms.ToolStripMenuItem();
            this.smClosePallet = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.imbt_LineSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1.SuspendLayout();
            this.PalConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.panelEx12.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.panelEx5.SuspendLayout();
            this.panelEx11.SuspendLayout();
            this.panelEx10.SuspendLayout();
            this.panelEx9.SuspendLayout();
            this.panelEx8.SuspendLayout();
            this.panelEx7.SuspendLayout();
            this.panelEx6.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labListen
            // 
            // 
            // 
            // 
            this.labListen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labListen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labListen.Location = new System.Drawing.Point(0, 0);
            this.labListen.Name = "labListen";
            this.labListen.Size = new System.Drawing.Size(811, 33);
            this.labListen.TabIndex = 0;
            this.labListen.Text = "等待监听.....";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.labListen);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx1.Location = new System.Drawing.Point(0, 567);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(811, 33);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(107, 28);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(222, 21);
            this.tbInput.TabIndex = 2;
            this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
            // 
            // lblFileSend
            // 
            this.lblFileSend.AutoSize = true;
            this.lblFileSend.Location = new System.Drawing.Point(36, 108);
            this.lblFileSend.Name = "lblFileSend";
            this.lblFileSend.Size = new System.Drawing.Size(71, 12);
            this.lblFileSend.TabIndex = 5;
            this.lblFileSend.Text = "lblFileSend";
            // 
            // lbxServer
            // 
            this.lbxServer.FormattingEnabled = true;
            this.lbxServer.ItemHeight = 12;
            this.lbxServer.Location = new System.Drawing.Point(18, 20);
            this.lbxServer.Name = "lbxServer";
            this.lbxServer.Size = new System.Drawing.Size(109, 76);
            this.lbxServer.TabIndex = 6;
            // 
            // PalConfig
            // 
            this.PalConfig.CanvasColor = System.Drawing.SystemColors.Control;
            this.PalConfig.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.PalConfig.Controls.Add(this.LabConfig);
            this.PalConfig.DisabledBackColor = System.Drawing.Color.Empty;
            this.PalConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.PalConfig.Location = new System.Drawing.Point(0, 25);
            this.PalConfig.Name = "PalConfig";
            this.PalConfig.Size = new System.Drawing.Size(811, 55);
            this.PalConfig.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.PalConfig.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.PalConfig.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.PalConfig.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.PalConfig.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.PalConfig.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.PalConfig.Style.GradientAngle = 90;
            this.PalConfig.TabIndex = 7;
            // 
            // LabConfig
            // 
            // 
            // 
            // 
            this.LabConfig.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabConfig.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabConfig.Location = new System.Drawing.Point(0, 0);
            this.LabConfig.Name = "LabConfig";
            this.LabConfig.Size = new System.Drawing.Size(811, 55);
            this.LabConfig.TabIndex = 0;
            this.LabConfig.Text = "Line:  Station:";
            this.LabConfig.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbxServer);
            this.groupBox1.Controls.Add(this.lblFileSend);
            this.groupBox1.Location = new System.Drawing.Point(519, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 141);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "隐藏";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.groupPanel1);
            this.panelEx3.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelEx3.Location = new System.Drawing.Point(525, 80);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(286, 487);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 9;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.listctn);
            this.groupPanel1.Controls.Add(this.groupPanel2);
            this.groupPanel1.Controls.Add(this.groupPanel3);
            this.groupPanel1.Controls.Add(this.panelEx12);
            this.groupPanel1.Controls.Add(this.listBox1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(286, 487);
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
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "Carton明细";
            // 
            // listctn
            // 
            this.listctn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listctn.FormattingEnabled = true;
            this.listctn.ItemHeight = 21;
            this.listctn.Location = new System.Drawing.Point(0, 0);
            this.listctn.Name = "listctn";
            this.listctn.Size = new System.Drawing.Size(280, 267);
            this.listctn.TabIndex = 0;
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.labpallet);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupPanel2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupPanel2.Location = new System.Drawing.Point(0, 267);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(280, 61);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 3;
            this.groupPanel2.Text = "栈板号码";
            // 
            // labpallet
            // 
            // 
            // 
            // 
            this.labpallet.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labpallet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labpallet.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labpallet.Location = new System.Drawing.Point(0, 0);
            this.labpallet.Name = "labpallet";
            this.labpallet.Size = new System.Drawing.Size(274, 35);
            this.labpallet.TabIndex = 0;
            this.labpallet.Text = "P12120600001";
            this.labpallet.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // groupPanel3
            // 
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.LabCustPallet);
            this.groupPanel3.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupPanel3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupPanel3.Location = new System.Drawing.Point(0, 328);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(280, 64);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 4;
            this.groupPanel3.Text = "客户栈板";
            // 
            // LabCustPallet
            // 
            // 
            // 
            // 
            this.LabCustPallet.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabCustPallet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabCustPallet.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabCustPallet.Location = new System.Drawing.Point(0, 0);
            this.LabCustPallet.Name = "LabCustPallet";
            this.LabCustPallet.Size = new System.Drawing.Size(274, 40);
            this.LabCustPallet.TabIndex = 0;
            this.LabCustPallet.Text = "CUST0000001";
            this.LabCustPallet.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // panelEx12
            // 
            this.panelEx12.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx12.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx12.Controls.Add(this.labfrmMsg);
            this.panelEx12.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx12.Location = new System.Drawing.Point(0, 392);
            this.panelEx12.Name = "panelEx12";
            this.panelEx12.Size = new System.Drawing.Size(280, 60);
            this.panelEx12.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx12.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx12.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx12.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx12.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx12.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx12.Style.GradientAngle = 90;
            this.panelEx12.TabIndex = 2;
            // 
            // labfrmMsg
            // 
            // 
            // 
            // 
            this.labfrmMsg.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labfrmMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labfrmMsg.Location = new System.Drawing.Point(0, 0);
            this.labfrmMsg.Name = "labfrmMsg";
            this.labfrmMsg.Size = new System.Drawing.Size(280, 60);
            this.labfrmMsg.TabIndex = 0;
            this.labfrmMsg.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labfrmMsg.Click += new System.EventHandler(this.labfrmMsg_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(8, 8);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 88);
            this.listBox1.TabIndex = 1;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.labemployee);
            this.panelEx4.Controls.Add(this.label3);
            this.panelEx4.Controls.Add(this.label2);
            this.panelEx4.Controls.Add(this.tbInput);
            this.panelEx4.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx4.Location = new System.Drawing.Point(0, 453);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(519, 114);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 10;
            // 
            // labemployee
            // 
            // 
            // 
            // 
            this.labemployee.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labemployee.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labemployee.Location = new System.Drawing.Point(107, 68);
            this.labemployee.Name = "labemployee";
            this.labemployee.Size = new System.Drawing.Size(222, 23);
            this.labemployee.TabIndex = 5;
            this.labemployee.Text = "XXXXXXXX";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Employee:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(30, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Carton:";
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(108)))), ((int)(((byte)(122)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(108)))), ((int)(((byte)(122)))));
            this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(120)))));
            this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(120)))));
            this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(200)))), ((int)(((byte)(103)))));
            this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(135)))));
            this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(108)))), ((int)(((byte)(122)))));
            this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(120)))));
            this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(108)))), ((int)(((byte)(122)))));
            this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.Location = new System.Drawing.Point(519, 80);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(6, 487);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 11;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.label8);
            this.panelEx5.Controls.Add(this.panelEx11);
            this.panelEx5.Controls.Add(this.panelEx10);
            this.panelEx5.Controls.Add(this.panelEx9);
            this.panelEx5.Controls.Add(this.label7);
            this.panelEx5.Controls.Add(this.label6);
            this.panelEx5.Controls.Add(this.panelEx8);
            this.panelEx5.Controls.Add(this.label5);
            this.panelEx5.Controls.Add(this.panelEx7);
            this.panelEx5.Controls.Add(this.label4);
            this.panelEx5.Controls.Add(this.panelEx6);
            this.panelEx5.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx5.Location = new System.Drawing.Point(0, 80);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(519, 373);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(77, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 21);
            this.label8.TabIndex = 10;
            this.label8.Text = "版本:";
            // 
            // panelEx11
            // 
            this.panelEx11.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx11.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx11.Controls.Add(this.labver);
            this.panelEx11.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx11.Location = new System.Drawing.Point(154, 126);
            this.panelEx11.Name = "panelEx11";
            this.panelEx11.Size = new System.Drawing.Size(222, 37);
            this.panelEx11.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx11.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx11.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx11.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx11.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx11.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx11.Style.GradientAngle = 90;
            this.panelEx11.TabIndex = 9;
            // 
            // labver
            // 
            // 
            // 
            // 
            this.labver.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labver.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labver.Location = new System.Drawing.Point(0, 0);
            this.labver.Name = "labver";
            this.labver.Size = new System.Drawing.Size(222, 37);
            this.labver.TabIndex = 0;
            this.labver.Text = "00S0";
            this.labver.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // panelEx10
            // 
            this.panelEx10.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx10.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx10.Controls.Add(this.btn_ClosePallet);
            this.panelEx10.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx10.Location = new System.Drawing.Point(0, 319);
            this.panelEx10.Name = "panelEx10";
            this.panelEx10.Size = new System.Drawing.Size(519, 54);
            this.panelEx10.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx10.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx10.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx10.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx10.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx10.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx10.Style.GradientAngle = 90;
            this.panelEx10.TabIndex = 8;
            // 
            // btn_ClosePallet
            // 
            this.btn_ClosePallet.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_ClosePallet.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_ClosePallet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ClosePallet.Enabled = false;
            this.btn_ClosePallet.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ClosePallet.Location = new System.Drawing.Point(0, 0);
            this.btn_ClosePallet.Name = "btn_ClosePallet";
            this.btn_ClosePallet.Size = new System.Drawing.Size(519, 54);
            this.btn_ClosePallet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_ClosePallet.TabIndex = 0;
            this.btn_ClosePallet.Text = "Close Pallet";
            this.btn_ClosePallet.Click += new System.EventHandler(this.btn_ClosePallet_Click);
            // 
            // panelEx9
            // 
            this.panelEx9.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx9.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx9.Controls.Add(this.labcount);
            this.panelEx9.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx9.Location = new System.Drawing.Point(154, 233);
            this.panelEx9.Name = "panelEx9";
            this.panelEx9.Size = new System.Drawing.Size(222, 37);
            this.panelEx9.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx9.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx9.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx9.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx9.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx9.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx9.Style.GradientAngle = 90;
            this.panelEx9.TabIndex = 7;
            // 
            // labcount
            // 
            // 
            // 
            // 
            this.labcount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labcount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labcount.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labcount.Location = new System.Drawing.Point(0, 0);
            this.labcount.Name = "labcount";
            this.labcount.Size = new System.Drawing.Size(222, 37);
            this.labcount.TabIndex = 0;
            this.labcount.Text = "0";
            this.labcount.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(33, 241);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 21);
            this.label7.TabIndex = 6;
            this.label7.Text = "已扫描数:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(33, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 21);
            this.label6.TabIndex = 5;
            this.label6.Text = "栈板总数:";
            // 
            // panelEx8
            // 
            this.panelEx8.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx8.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx8.Controls.Add(this.labTotalCount);
            this.panelEx8.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx8.Location = new System.Drawing.Point(154, 178);
            this.panelEx8.Name = "panelEx8";
            this.panelEx8.Size = new System.Drawing.Size(222, 37);
            this.panelEx8.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx8.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx8.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx8.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx8.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx8.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx8.Style.GradientAngle = 90;
            this.panelEx8.TabIndex = 4;
            // 
            // labTotalCount
            // 
            // 
            // 
            // 
            this.labTotalCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labTotalCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labTotalCount.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTotalCount.Location = new System.Drawing.Point(0, 0);
            this.labTotalCount.Name = "labTotalCount";
            this.labTotalCount.Size = new System.Drawing.Size(222, 37);
            this.labTotalCount.TabIndex = 0;
            this.labTotalCount.Text = "0";
            this.labTotalCount.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(77, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 21);
            this.label5.TabIndex = 3;
            this.label5.Text = "工单:";
            // 
            // panelEx7
            // 
            this.panelEx7.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx7.Controls.Add(this.labwoId);
            this.panelEx7.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx7.Location = new System.Drawing.Point(154, 74);
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Size = new System.Drawing.Size(222, 37);
            this.panelEx7.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx7.Style.GradientAngle = 90;
            this.panelEx7.TabIndex = 2;
            // 
            // labwoId
            // 
            // 
            // 
            // 
            this.labwoId.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labwoId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labwoId.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labwoId.Location = new System.Drawing.Point(0, 0);
            this.labwoId.Name = "labwoId";
            this.labwoId.Size = new System.Drawing.Size(222, 37);
            this.labwoId.TabIndex = 0;
            this.labwoId.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(77, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "料号:";
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.labPartnumber);
            this.panelEx6.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx6.Location = new System.Drawing.Point(154, 22);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(222, 37);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 0;
            // 
            // labPartnumber
            // 
            // 
            // 
            // 
            this.labPartnumber.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labPartnumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labPartnumber.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPartnumber.Location = new System.Drawing.Point(0, 0);
            this.labPartnumber.Name = "labPartnumber";
            this.labPartnumber.Size = new System.Drawing.Size(222, 37);
            this.labPartnumber.TabIndex = 0;
            this.labPartnumber.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选项ToolStripMenuItem,
            this.rePrintToolStripMenuItem,
            this.imbt_LineSelect});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(811, 25);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 选项ToolStripMenuItem
            // 
            this.选项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmesn,
            this.tsmcarton,
            this.tsmkeyparts});
            this.选项ToolStripMenuItem.Name = "选项ToolStripMenuItem";
            this.选项ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.选项ToolStripMenuItem.Text = "选项";
            // 
            // tsmesn
            // 
            this.tsmesn.CheckOnClick = true;
            this.tsmesn.Name = "tsmesn";
            this.tsmesn.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.tsmesn.Size = new System.Drawing.Size(141, 22);
            this.tsmesn.Text = "ESN";
            this.tsmesn.Click += new System.EventHandler(this.tsmesn_Click);
            // 
            // tsmcarton
            // 
            this.tsmcarton.Checked = true;
            this.tsmcarton.CheckOnClick = true;
            this.tsmcarton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmcarton.Name = "tsmcarton";
            this.tsmcarton.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.tsmcarton.Size = new System.Drawing.Size(141, 22);
            this.tsmcarton.Text = "Carton";
            this.tsmcarton.Click += new System.EventHandler(this.tsmcarton_Click);
            // 
            // tsmkeyparts
            // 
            this.tsmkeyparts.CheckOnClick = true;
            this.tsmkeyparts.Name = "tsmkeyparts";
            this.tsmkeyparts.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.tsmkeyparts.Size = new System.Drawing.Size(141, 22);
            this.tsmkeyparts.Text = "KeyPart";
            this.tsmkeyparts.Click += new System.EventHandler(this.tsmkeyparts_Click);
            // 
            // rePrintToolStripMenuItem
            // 
            this.rePrintToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReprintPallet,
            this.smClosePallet,
            this.LabelPrint});
            this.rePrintToolStripMenuItem.Name = "rePrintToolStripMenuItem";
            this.rePrintToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.rePrintToolStripMenuItem.Text = "RePrint";
            // 
            // ReprintPallet
            // 
            this.ReprintPallet.Name = "ReprintPallet";
            this.ReprintPallet.Size = new System.Drawing.Size(137, 22);
            this.ReprintPallet.Text = "栈板补印";
            this.ReprintPallet.Click += new System.EventHandler(this.ReprintPallet_Click);
            // 
            // smClosePallet
            // 
            this.smClosePallet.Name = "smClosePallet";
            this.smClosePallet.Size = new System.Drawing.Size(137, 22);
            this.smClosePallet.Text = "关闭栈板";
            this.smClosePallet.Click += new System.EventHandler(this.smClosePallet_Click);
            // 
            // LabelPrint
            // 
            this.LabelPrint.Checked = true;
            this.LabelPrint.CheckOnClick = true;
            this.LabelPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LabelPrint.Name = "LabelPrint";
            this.LabelPrint.Size = new System.Drawing.Size(137, 22);
            this.LabelPrint.Text = "Print Label";
            // 
            // imbt_LineSelect
            // 
            this.imbt_LineSelect.Name = "imbt_LineSelect";
            this.imbt_LineSelect.Size = new System.Drawing.Size(68, 21);
            this.imbt_LineSelect.Text = "选择线体";
            this.imbt_LineSelect.Click += new System.EventHandler(this.imbt_LineSelect_Click);
            // 
            // FrmPallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 600);
            this.Controls.Add(this.panelEx5);
            this.Controls.Add(this.panelEx4);
            this.Controls.Add(this.expandableSplitter1);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PalConfig);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmPallet";
            this.Text = "栈板包装";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPallet_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPallet_FormClosed);
            this.Load += new System.EventHandler(this.FrmPallet_Load);
            this.panelEx1.ResumeLayout(false);
            this.PalConfig.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelEx3.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            this.panelEx12.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.panelEx4.PerformLayout();
            this.panelEx5.ResumeLayout(false);
            this.panelEx5.PerformLayout();
            this.panelEx11.ResumeLayout(false);
            this.panelEx10.ResumeLayout(false);
            this.panelEx9.ResumeLayout(false);
            this.panelEx8.ResumeLayout(false);
            this.panelEx7.ResumeLayout(false);
            this.panelEx6.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labListen;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Label lblFileSend;
        private System.Windows.Forms.ListBox lbxServer;
        private System.Windows.Forms.Timer timerByServer;
        private System.Windows.Forms.TextBox tbInput;
        private DevComponents.DotNetBar.PanelEx PalConfig;
        public DevComponents.DotNetBar.LabelX LabConfig;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.LabelX labemployee;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private DevComponents.DotNetBar.LabelX labPartnumber;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private DevComponents.DotNetBar.LabelX labwoId;
        private DevComponents.DotNetBar.PanelEx panelEx9;
        private DevComponents.DotNetBar.LabelX labcount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.PanelEx panelEx8;
        private DevComponents.DotNetBar.LabelX labTotalCount;
        private DevComponents.DotNetBar.PanelEx panelEx10;
        private DevComponents.DotNetBar.ButtonX btn_ClosePallet;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.ListBox listctn;
        private System.Windows.Forms.Label label8;
        private DevComponents.DotNetBar.PanelEx panelEx11;
        private DevComponents.DotNetBar.LabelX labver;
        private DevComponents.DotNetBar.PanelEx panelEx12;
        private DevComponents.DotNetBar.LabelX labfrmMsg;
        private System.Windows.Forms.ListBox listBox1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.LabelX labpallet;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmesn;
        private System.Windows.Forms.ToolStripMenuItem tsmcarton;
        private System.Windows.Forms.ToolStripMenuItem rePrintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReprintPallet;
        private System.Windows.Forms.ToolStripMenuItem smClosePallet;
        private System.Windows.Forms.ToolStripMenuItem LabelPrint;
        private System.Windows.Forms.ToolStripMenuItem tsmkeyparts;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private DevComponents.DotNetBar.LabelX LabCustPallet;
        private System.Windows.Forms.ToolStripMenuItem imbt_LineSelect;
    }
}