namespace SFIS_V2
{
    partial class FrmStockIn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.LabConfig = new DevComponents.DotNetBar.LabelX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.txtServerIP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnselectcraft = new DevComponents.DotNetBar.ButtonX();
            this.btnselectLine = new DevComponents.DotNetBar.ButtonX();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.btnStartServer = new DevComponents.DotNetBar.ButtonX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.labListen = new DevComponents.DotNetBar.LabelX();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.lbxServer = new System.Windows.Forms.ListBox();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.lblFileSend = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ListEsn = new System.Windows.Forms.ListBox();
            this.labfrmMsg = new DevComponents.DotNetBar.LabelX();
            this.grouptray = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dgvtray = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupCarton = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dgvcarton = new System.Windows.Forms.DataGridView();
            this.箱号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Palgroup = new DevComponents.DotNetBar.PanelEx();
            this.groupPallet = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dgvpallet = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrintStockInNo = new System.Drawing.Printing.PrintDocument();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RePrintStockInNo = new System.Windows.Forms.ToolStripMenuItem();
            this.ReUpLoadStock = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grouptray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtray)).BeginInit();
            this.groupCarton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvcarton)).BeginInit();
            this.Palgroup.SuspendLayout();
            this.groupPallet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpallet)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.LabConfig);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 25);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(922, 57);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // LabConfig
            // 
            // 
            // 
            // 
            this.LabConfig.BackgroundStyle.Class = "";
            this.LabConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabConfig.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabConfig.Location = new System.Drawing.Point(593, 0);
            this.LabConfig.Name = "LabConfig";
            this.LabConfig.Size = new System.Drawing.Size(329, 57);
            this.LabConfig.TabIndex = 25;
            this.LabConfig.Text = "LabConfig";
            this.LabConfig.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.txtServerIP);
            this.panelEx3.Controls.Add(this.label1);
            this.panelEx3.Controls.Add(this.btnselectcraft);
            this.panelEx3.Controls.Add(this.btnselectLine);
            this.panelEx3.Controls.Add(this.txtServerPort);
            this.panelEx3.Controls.Add(this.btnStartServer);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(593, 57);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 26;
            // 
            // txtServerIP
            // 
            this.txtServerIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtServerIP.FormattingEnabled = true;
            this.txtServerIP.Location = new System.Drawing.Point(126, 17);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(121, 20);
            this.txtServerIP.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 14);
            this.label1.TabIndex = 19;
            this.label1.Text = "服务器IP(本机):";
            // 
            // btnselectcraft
            // 
            this.btnselectcraft.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnselectcraft.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnselectcraft.Location = new System.Drawing.Point(500, 14);
            this.btnselectcraft.Name = "btnselectcraft";
            this.btnselectcraft.Size = new System.Drawing.Size(61, 25);
            this.btnselectcraft.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnselectcraft.TabIndex = 24;
            this.btnselectcraft.Text = "选择途程";
            this.btnselectcraft.Click += new System.EventHandler(this.btnselectcraft_Click);
            // 
            // btnselectLine
            // 
            this.btnselectLine.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnselectLine.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnselectLine.Location = new System.Drawing.Point(411, 14);
            this.btnselectLine.Name = "btnselectLine";
            this.btnselectLine.Size = new System.Drawing.Size(61, 25);
            this.btnselectLine.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnselectLine.TabIndex = 23;
            this.btnselectLine.Text = "选择线体";
            this.btnselectLine.Click += new System.EventHandler(this.btnselectLine_Click);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(256, 17);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(47, 21);
            this.txtServerPort.TabIndex = 21;
            this.txtServerPort.Text = "8898";
            // 
            // btnStartServer
            // 
            this.btnStartServer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStartServer.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStartServer.Location = new System.Drawing.Point(326, 13);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(59, 26);
            this.btnStartServer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnStartServer.TabIndex = 22;
            this.btnStartServer.Text = "启动服务";
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.labListen);
            this.panelEx2.Controls.Add(this.progressBarX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 281);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(922, 65);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // labListen
            // 
            // 
            // 
            // 
            this.labListen.BackgroundStyle.Class = "";
            this.labListen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labListen.Location = new System.Drawing.Point(0, 0);
            this.labListen.Name = "labListen";
            this.labListen.Size = new System.Drawing.Size(922, 42);
            this.labListen.TabIndex = 1;
            this.labListen.Text = "监听状态......";
            // 
            // progressBarX1
            // 
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.Class = "";
            this.progressBarX1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarX1.Location = new System.Drawing.Point(0, 42);
            this.progressBarX1.Maximum = 500;
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.Size = new System.Drawing.Size(922, 23);
            this.progressBarX1.TabIndex = 0;
            this.progressBarX1.Text = "progressBarX1";
            // 
            // lbxServer
            // 
            this.lbxServer.FormattingEnabled = true;
            this.lbxServer.ItemHeight = 12;
            this.lbxServer.Location = new System.Drawing.Point(131, 3);
            this.lbxServer.Name = "lbxServer";
            this.lbxServer.Size = new System.Drawing.Size(66, 28);
            this.lbxServer.TabIndex = 2;
            // 
            // tbInput
            // 
            this.tbInput.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbInput.Location = new System.Drawing.Point(5, 19);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(100, 21);
            this.tbInput.TabIndex = 3;
            this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
            // 
            // lblFileSend
            // 
            this.lblFileSend.AutoSize = true;
            this.lblFileSend.Location = new System.Drawing.Point(3, 59);
            this.lblFileSend.Name = "lblFileSend";
            this.lblFileSend.Size = new System.Drawing.Size(71, 12);
            this.lblFileSend.TabIndex = 4;
            this.lblFileSend.Text = "lblFileSend";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ListEsn);
            this.panel1.Controls.Add(this.labfrmMsg);
            this.panel1.Controls.Add(this.tbInput);
            this.panel1.Controls.Add(this.lbxServer);
            this.panel1.Controls.Add(this.lblFileSend);
            this.panel1.Location = new System.Drawing.Point(60, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 93);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // ListEsn
            // 
            this.ListEsn.FormattingEnabled = true;
            this.ListEsn.ItemHeight = 12;
            this.ListEsn.Location = new System.Drawing.Point(131, 37);
            this.ListEsn.Name = "ListEsn";
            this.ListEsn.Size = new System.Drawing.Size(57, 40);
            this.ListEsn.TabIndex = 6;
            // 
            // labfrmMsg
            // 
            // 
            // 
            // 
            this.labfrmMsg.BackgroundStyle.Class = "";
            this.labfrmMsg.Location = new System.Drawing.Point(80, 69);
            this.labfrmMsg.Name = "labfrmMsg";
            this.labfrmMsg.Size = new System.Drawing.Size(75, 23);
            this.labfrmMsg.TabIndex = 5;
            this.labfrmMsg.Text = "labfrmMsg";
            // 
            // grouptray
            // 
            this.grouptray.CanvasColor = System.Drawing.SystemColors.Control;
            this.grouptray.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grouptray.Controls.Add(this.dgvtray);
            this.grouptray.Dock = System.Windows.Forms.DockStyle.Left;
            this.grouptray.Location = new System.Drawing.Point(0, 0);
            this.grouptray.Name = "grouptray";
            this.grouptray.Size = new System.Drawing.Size(284, 199);
            // 
            // 
            // 
            this.grouptray.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grouptray.Style.BackColorGradientAngle = 90;
            this.grouptray.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grouptray.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grouptray.Style.BorderBottomWidth = 1;
            this.grouptray.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grouptray.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grouptray.Style.BorderLeftWidth = 1;
            this.grouptray.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grouptray.Style.BorderRightWidth = 1;
            this.grouptray.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grouptray.Style.BorderTopWidth = 1;
            this.grouptray.Style.Class = "";
            this.grouptray.Style.CornerDiameter = 4;
            this.grouptray.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grouptray.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grouptray.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grouptray.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grouptray.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.grouptray.StyleMouseOver.Class = "";
            this.grouptray.TabIndex = 6;
            this.grouptray.Text = "Tray";
            // 
            // dgvtray
            // 
            this.dgvtray.AllowUserToAddRows = false;
            this.dgvtray.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            this.dgvtray.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvtray.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvtray.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvtray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvtray.Location = new System.Drawing.Point(0, 0);
            this.dgvtray.Name = "dgvtray";
            this.dgvtray.ReadOnly = true;
            this.dgvtray.RowTemplate.Height = 23;
            this.dgvtray.Size = new System.Drawing.Size(278, 177);
            this.dgvtray.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tray";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "数量";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // groupCarton
            // 
            this.groupCarton.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupCarton.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupCarton.Controls.Add(this.panel1);
            this.groupCarton.Controls.Add(this.dgvcarton);
            this.groupCarton.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupCarton.Location = new System.Drawing.Point(284, 0);
            this.groupCarton.Name = "groupCarton";
            this.groupCarton.Size = new System.Drawing.Size(288, 199);
            // 
            // 
            // 
            this.groupCarton.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupCarton.Style.BackColorGradientAngle = 90;
            this.groupCarton.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupCarton.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupCarton.Style.BorderBottomWidth = 1;
            this.groupCarton.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupCarton.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupCarton.Style.BorderLeftWidth = 1;
            this.groupCarton.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupCarton.Style.BorderRightWidth = 1;
            this.groupCarton.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupCarton.Style.BorderTopWidth = 1;
            this.groupCarton.Style.Class = "";
            this.groupCarton.Style.CornerDiameter = 4;
            this.groupCarton.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupCarton.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupCarton.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupCarton.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupCarton.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.groupCarton.StyleMouseOver.Class = "";
            this.groupCarton.TabIndex = 7;
            this.groupCarton.Text = "产品箱号";
            // 
            // dgvcarton
            // 
            this.dgvcarton.AllowUserToAddRows = false;
            this.dgvcarton.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Silver;
            this.dgvcarton.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvcarton.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvcarton.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.箱号,
            this.数量});
            this.dgvcarton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvcarton.Location = new System.Drawing.Point(0, 0);
            this.dgvcarton.Name = "dgvcarton";
            this.dgvcarton.ReadOnly = true;
            this.dgvcarton.RowTemplate.Height = 23;
            this.dgvcarton.Size = new System.Drawing.Size(282, 175);
            this.dgvcarton.TabIndex = 0;
            // 
            // 箱号
            // 
            this.箱号.HeaderText = "箱号";
            this.箱号.Name = "箱号";
            this.箱号.ReadOnly = true;
            this.箱号.Width = 200;
            // 
            // 数量
            // 
            this.数量.HeaderText = "数量";
            this.数量.Name = "数量";
            this.数量.ReadOnly = true;
            // 
            // Palgroup
            // 
            this.Palgroup.CanvasColor = System.Drawing.SystemColors.Control;
            this.Palgroup.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Palgroup.Controls.Add(this.groupPallet);
            this.Palgroup.Controls.Add(this.groupCarton);
            this.Palgroup.Controls.Add(this.grouptray);
            this.Palgroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Palgroup.Location = new System.Drawing.Point(0, 82);
            this.Palgroup.Name = "Palgroup";
            this.Palgroup.Size = new System.Drawing.Size(922, 199);
            this.Palgroup.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.Palgroup.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.Palgroup.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.Palgroup.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.Palgroup.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.Palgroup.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.Palgroup.Style.GradientAngle = 90;
            this.Palgroup.TabIndex = 8;
            // 
            // groupPallet
            // 
            this.groupPallet.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPallet.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPallet.Controls.Add(this.dgvpallet);
            this.groupPallet.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupPallet.Location = new System.Drawing.Point(572, 0);
            this.groupPallet.Name = "groupPallet";
            this.groupPallet.Size = new System.Drawing.Size(288, 199);
            // 
            // 
            // 
            this.groupPallet.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPallet.Style.BackColorGradientAngle = 90;
            this.groupPallet.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPallet.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPallet.Style.BorderBottomWidth = 1;
            this.groupPallet.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPallet.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPallet.Style.BorderLeftWidth = 1;
            this.groupPallet.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPallet.Style.BorderRightWidth = 1;
            this.groupPallet.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPallet.Style.BorderTopWidth = 1;
            this.groupPallet.Style.Class = "";
            this.groupPallet.Style.CornerDiameter = 4;
            this.groupPallet.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPallet.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPallet.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPallet.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPallet.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.groupPallet.StyleMouseOver.Class = "";
            this.groupPallet.TabIndex = 8;
            this.groupPallet.Text = "产品栈板号";
            // 
            // dgvpallet
            // 
            this.dgvpallet.AllowUserToAddRows = false;
            this.dgvpallet.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver;
            this.dgvpallet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvpallet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvpallet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvpallet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvpallet.Location = new System.Drawing.Point(0, 0);
            this.dgvpallet.Name = "dgvpallet";
            this.dgvpallet.ReadOnly = true;
            this.dgvpallet.RowTemplate.Height = 23;
            this.dgvpallet.Size = new System.Drawing.Size(282, 175);
            this.dgvpallet.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "栈板";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "数量";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // PrintStockInNo
            // 
            this.PrintStockInNo.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintStockInNo_PrintPage);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选项ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(922, 25);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 选项ToolStripMenuItem
            // 
            this.选项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RePrintStockInNo,
            this.ReUpLoadStock});
            this.选项ToolStripMenuItem.Name = "选项ToolStripMenuItem";
            this.选项ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.选项ToolStripMenuItem.Text = "选项";
            // 
            // RePrintStockInNo
            // 
            this.RePrintStockInNo.Name = "RePrintStockInNo";
            this.RePrintStockInNo.Size = new System.Drawing.Size(160, 22);
            this.RePrintStockInNo.Text = "重复列印单据";
            this.RePrintStockInNo.Click += new System.EventHandler(this.RePrintStockInNo_Click);
            // 
            // ReUpLoadStock
            // 
            this.ReUpLoadStock.Name = "ReUpLoadStock";
            this.ReUpLoadStock.Size = new System.Drawing.Size(160, 22);
            this.ReUpLoadStock.Text = "提交未完成单据";
            this.ReUpLoadStock.Click += new System.EventHandler(this.ReUpLoadStock_Click);
            // 
            // FrmStockIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 346);
            this.Controls.Add(this.Palgroup);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmStockIn";
            this.Text = "成品入库";
            this.Load += new System.EventHandler(this.FrmStockIn_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmStockIn_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmStockIn_FormClosing);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grouptray.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvtray)).EndInit();
            this.groupCarton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvcarton)).EndInit();
            this.Palgroup.ResumeLayout(false);
            this.groupPallet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvpallet)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnStartServer;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labListen;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarX1;
        private System.Windows.Forms.ListBox lbxServer;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label lblFileSend;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.LabelX labfrmMsg;
        private DevComponents.DotNetBar.Controls.GroupPanel grouptray;
        private DevComponents.DotNetBar.Controls.GroupPanel groupCarton;
        private DevComponents.DotNetBar.PanelEx Palgroup;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPallet;
        private System.Windows.Forms.DataGridView dgvcarton;
        private System.Windows.Forms.DataGridViewTextBoxColumn 箱号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量;
        private System.Windows.Forms.DataGridView dgvtray;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridView dgvpallet;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DevComponents.DotNetBar.ButtonX btnselectcraft;
        private DevComponents.DotNetBar.ButtonX btnselectLine;
        public DevComponents.DotNetBar.LabelX LabConfig;
        private System.Windows.Forms.ListBox ListEsn;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Drawing.Printing.PrintDocument PrintStockInNo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RePrintStockInNo;
        private System.Windows.Forms.ToolStripMenuItem ReUpLoadStock;
        private System.Windows.Forms.ComboBox txtServerIP;
    }
}