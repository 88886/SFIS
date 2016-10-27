namespace SFIS_V2
{
    partial class Frm_ReceiveMoveStock
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.list_info = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rtbmsg = new DevComponents.DotNetBar.LabelX();
            this.labListen = new DevComponents.DotNetBar.LabelX();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.list_QTY = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labfrmMsg = new System.Windows.Forms.Label();
            this.lbxServer = new System.Windows.Forms.ListBox();
            this.lblFileSend = new System.Windows.Forms.Label();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServerIP = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnStartServer = new DevComponents.DotNetBar.ButtonX();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.Labname = new System.Windows.Forms.Label();
            this.Labempno = new System.Windows.Forms.Label();
            this.listMoveID = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bt_modify = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.bt_confirm = new DevComponents.DotNetBar.ButtonX();
            this.tb_moveid = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelEx2);
            this.groupBox1.Controls.Add(this.groupPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1127, 501);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.list_info);
            this.panelEx2.Controls.Add(this.groupPanel2);
            this.panelEx2.Controls.Add(this.labListen);
            this.panelEx2.Controls.Add(this.progressBarX1);
            this.panelEx2.Controls.Add(this.panelEx3);
            this.panelEx2.Controls.Add(this.groupBox2);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(3, 64);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1121, 434);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 2;
            // 
            // list_info
            // 
            this.list_info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.list_info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader8});
            this.list_info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_info.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.list_info.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.list_info.FullRowSelect = true;
            this.list_info.GridLines = true;
            this.list_info.Location = new System.Drawing.Point(0, 233);
            this.list_info.Name = "list_info";
            this.list_info.Size = new System.Drawing.Size(1121, 121);
            this.list_info.TabIndex = 7;
            this.list_info.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "栈板号";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 200;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "成品料号";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader11.Width = 200;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "移出仓库";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader12.Width = 200;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "移入仓库";
            this.columnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader13.Width = 200;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "移入库位";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 200;
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.rtbmsg);
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupPanel2.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupPanel2.Location = new System.Drawing.Point(0, 354);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(1121, 37);
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
            this.groupPanel2.Style.Class = "";
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.Class = "";
            this.groupPanel2.TabIndex = 6;
            // 
            // rtbmsg
            // 
            this.rtbmsg.AutoSize = true;
            // 
            // 
            // 
            this.rtbmsg.BackgroundStyle.Class = "";
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbmsg.Font = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbmsg.ForeColor = System.Drawing.Color.Red;
            this.rtbmsg.Location = new System.Drawing.Point(0, 0);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.Size = new System.Drawing.Size(50, 24);
            this.rtbmsg.TabIndex = 0;
            this.rtbmsg.Text = "MSG:";
            // 
            // labListen
            // 
            this.labListen.AutoSize = true;
            this.labListen.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labListen.BackgroundStyle.Class = "";
            this.labListen.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labListen.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labListen.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labListen.Location = new System.Drawing.Point(0, 391);
            this.labListen.Name = "labListen";
            this.labListen.Size = new System.Drawing.Size(31, 18);
            this.labListen.TabIndex = 5;
            this.labListen.Text = "状态";
            // 
            // progressBarX1
            // 
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.Class = "";
            this.progressBarX1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarX1.Location = new System.Drawing.Point(0, 409);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.Size = new System.Drawing.Size(1121, 25);
            this.progressBarX1.TabIndex = 4;
            this.progressBarX1.Text = "0/0";
            this.progressBarX1.TextVisible = true;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.list_QTY);
            this.panelEx3.Controls.Add(this.labfrmMsg);
            this.panelEx3.Controls.Add(this.lbxServer);
            this.panelEx3.Controls.Add(this.lblFileSend);
            this.panelEx3.Controls.Add(this.tbInput);
            this.panelEx3.Controls.Add(this.label3);
            this.panelEx3.Controls.Add(this.txtServerIP);
            this.panelEx3.Controls.Add(this.btnStartServer);
            this.panelEx3.Controls.Add(this.txtServerPort);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(0, 184);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1121, 49);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 3;
            // 
            // list_QTY
            // 
            this.list_QTY.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader18});
            this.list_QTY.GridLines = true;
            this.list_QTY.Location = new System.Drawing.Point(764, 16);
            this.list_QTY.Name = "list_QTY";
            this.list_QTY.Size = new System.Drawing.Size(326, 30);
            this.list_QTY.TabIndex = 25;
            this.list_QTY.UseCompatibleStateImageBehavior = false;
            this.list_QTY.Visible = false;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "移库单号";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "料号";
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "移库数量";
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "数量";
            // 
            // labfrmMsg
            // 
            this.labfrmMsg.AutoSize = true;
            this.labfrmMsg.Location = new System.Drawing.Point(909, 32);
            this.labfrmMsg.Name = "labfrmMsg";
            this.labfrmMsg.Size = new System.Drawing.Size(59, 12);
            this.labfrmMsg.TabIndex = 24;
            this.labfrmMsg.Text = "labfrmMsg";
            this.labfrmMsg.Visible = false;
            // 
            // lbxServer
            // 
            this.lbxServer.FormattingEnabled = true;
            this.lbxServer.ItemHeight = 12;
            this.lbxServer.Location = new System.Drawing.Point(641, 16);
            this.lbxServer.Name = "lbxServer";
            this.lbxServer.Size = new System.Drawing.Size(117, 28);
            this.lbxServer.TabIndex = 23;
            this.lbxServer.Visible = false;
            // 
            // lblFileSend
            // 
            this.lblFileSend.AutoSize = true;
            this.lblFileSend.Location = new System.Drawing.Point(561, 26);
            this.lblFileSend.Name = "lblFileSend";
            this.lblFileSend.Size = new System.Drawing.Size(71, 12);
            this.lblFileSend.TabIndex = 22;
            this.lblFileSend.Text = "lblFileSend";
            this.lblFileSend.Visible = false;
            // 
            // tbInput
            // 
            this.tbInput.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbInput.Location = new System.Drawing.Point(466, 17);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(70, 21);
            this.tbInput.TabIndex = 21;
            this.tbInput.Visible = false;
            this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(12, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 14);
            this.label3.TabIndex = 20;
            this.label3.Text = "服务器IP(本机):";
            // 
            // txtServerIP
            // 
            this.txtServerIP.DisplayMember = "Text";
            this.txtServerIP.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.txtServerIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtServerIP.FormattingEnabled = true;
            this.txtServerIP.ItemHeight = 15;
            this.txtServerIP.Location = new System.Drawing.Point(149, 14);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(121, 21);
            this.txtServerIP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.txtServerIP.TabIndex = 4;
            // 
            // btnStartServer
            // 
            this.btnStartServer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStartServer.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStartServer.Location = new System.Drawing.Point(393, 15);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(67, 23);
            this.btnStartServer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnStartServer.TabIndex = 2;
            this.btnStartServer.Text = "启动服务";
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(296, 15);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(64, 21);
            this.txtServerPort.TabIndex = 1;
            this.txtServerPort.Text = "8898";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelX4);
            this.groupBox2.Controls.Add(this.labelX3);
            this.groupBox2.Controls.Add(this.Labname);
            this.groupBox2.Controls.Add(this.Labempno);
            this.groupBox2.Controls.Add(this.listMoveID);
            this.groupBox2.Controls.Add(this.bt_modify);
            this.groupBox2.Controls.Add(this.labelX1);
            this.groupBox2.Controls.Add(this.bt_confirm);
            this.groupBox2.Controls.Add(this.tb_moveid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1121, 184);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.Font = new System.Drawing.Font("宋体", 11F);
            this.labelX4.Location = new System.Drawing.Point(747, 23);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(58, 23);
            this.labelX4.TabIndex = 17;
            this.labelX4.Text = "姓名：";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Font = new System.Drawing.Font("宋体", 11F);
            this.labelX3.Location = new System.Drawing.Point(574, 20);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(58, 29);
            this.labelX3.TabIndex = 16;
            this.labelX3.Text = "工号：";
            // 
            // Labname
            // 
            this.Labname.AutoSize = true;
            this.Labname.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Labname.Location = new System.Drawing.Point(811, 27);
            this.Labname.Name = "Labname";
            this.Labname.Size = new System.Drawing.Size(48, 16);
            this.Labname.TabIndex = 15;
            this.Labname.Text = "-----";
            // 
            // Labempno
            // 
            this.Labempno.AutoSize = true;
            this.Labempno.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Labempno.Location = new System.Drawing.Point(638, 24);
            this.Labempno.Name = "Labempno";
            this.Labempno.Size = new System.Drawing.Size(48, 16);
            this.Labempno.TabIndex = 14;
            this.Labempno.Text = "-----";
            // 
            // listMoveID
            // 
            this.listMoveID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.listMoveID.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader7,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader3,
            this.columnHeader6});
            this.listMoveID.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listMoveID.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.listMoveID.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.listMoveID.FullRowSelect = true;
            this.listMoveID.GridLines = true;
            this.listMoveID.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listMoveID.Location = new System.Drawing.Point(3, 55);
            this.listMoveID.Name = "listMoveID";
            this.listMoveID.Size = new System.Drawing.Size(1115, 126);
            this.listMoveID.TabIndex = 13;
            this.listMoveID.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "移库单号";
            this.columnHeader1.Width = 180;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "成品料号";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 180;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "栈板号";
            this.columnHeader7.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "移出库位";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 180;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "移入库位";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 180;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "移库数量";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 180;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "数量";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 180;
            // 
            // bt_modify
            // 
            this.bt_modify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_modify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_modify.Location = new System.Drawing.Point(426, 20);
            this.bt_modify.Name = "bt_modify";
            this.bt_modify.Size = new System.Drawing.Size(75, 23);
            this.bt_modify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_modify.TabIndex = 12;
            this.bt_modify.Text = "修改";
            this.bt_modify.Click += new System.EventHandler(this.bt_modify_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Font = new System.Drawing.Font("宋体", 11F);
            this.labelX1.Location = new System.Drawing.Point(25, 20);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(86, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "移库单号：";
            // 
            // bt_confirm
            // 
            this.bt_confirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_confirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_confirm.Location = new System.Drawing.Point(313, 20);
            this.bt_confirm.Name = "bt_confirm";
            this.bt_confirm.Size = new System.Drawing.Size(75, 23);
            this.bt_confirm.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_confirm.TabIndex = 11;
            this.bt_confirm.Text = "确认";
            this.bt_confirm.Click += new System.EventHandler(this.bt_confirm_Click);
            // 
            // tb_moveid
            // 
            // 
            // 
            // 
            this.tb_moveid.Border.Class = "TextBoxBorder";
            this.tb_moveid.Font = new System.Drawing.Font("宋体", 11F);
            this.tb_moveid.Location = new System.Drawing.Point(126, 19);
            this.tb_moveid.Name = "tb_moveid";
            this.tb_moveid.Size = new System.Drawing.Size(142, 24);
            this.tb_moveid.TabIndex = 10;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.panelEx1);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(3, 17);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(1121, 47);
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
            this.groupPanel1.TabIndex = 1;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Font = new System.Drawing.Font("宋体", 16F);
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1115, 41);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "成品接受移库";
            // 
            // Frm_ReceiveMoveStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 501);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_ReceiveMoveStock";
            this.Text = "成品接受移库";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_ReceiveMoveStock_FormClosing);
            this.Load += new System.EventHandler(this.Frm_ReceiveMoveStock_Load);
            this.groupBox1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.ListView list_info;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.LabelX rtbmsg;
        private DevComponents.DotNetBar.LabelX labListen;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarX1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.Label labfrmMsg;
        private System.Windows.Forms.ListBox lbxServer;
        private System.Windows.Forms.Label lblFileSend;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx txtServerIP;
        private DevComponents.DotNetBar.ButtonX btnStartServer;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.Label Labname;
        private System.Windows.Forms.Label Labempno;
        private System.Windows.Forms.ListView listMoveID;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private DevComponents.DotNetBar.ButtonX bt_modify;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX bt_confirm;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_moveid;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ListView list_QTY;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader18;
    }
}