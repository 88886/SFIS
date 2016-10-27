namespace SFIS_V2
{
    partial class FrmBomNo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dgvbomno = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.tbbom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgvkeyparts = new System.Windows.Forms.DataGridView();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.btaddparts = new DevComponents.DotNetBar.ButtonX();
            this.tbparts = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx9 = new DevComponents.DotNetBar.PanelEx();
            this.dgvshowlist = new System.Windows.Forms.DataGridView();
            this.panelEx8 = new DevComponents.DotNetBar.PanelEx();
            this.label6 = new System.Windows.Forms.Label();
            this.cbstationlist = new System.Windows.Forms.ComboBox();
            this.lbtoright = new System.Windows.Forms.Label();
            this.lbtoleft = new System.Windows.Forms.Label();
            this.panelEx7 = new DevComponents.DotNetBar.PanelEx();
            this.btdelete = new DevComponents.DotNetBar.ButtonX();
            this.btmodify = new DevComponents.DotNetBar.ButtonX();
            this.btadd = new DevComponents.DotNetBar.ButtonX();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.btcancel = new System.Windows.Forms.Button();
            this.tbaddbom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbomno)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvkeyparts)).BeginInit();
            this.panelEx4.SuspendLayout();
            this.panelEx5.SuspendLayout();
            this.panelEx9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvshowlist)).BeginInit();
            this.panelEx8.SuspendLayout();
            this.panelEx7.SuspendLayout();
            this.panelEx6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dgvbomno);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(298, 535);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // dgvbomno
            // 
            this.dgvbomno.AllowUserToAddRows = false;
            this.dgvbomno.AllowUserToDeleteRows = false;
            this.dgvbomno.AllowUserToOrderColumns = true;
            this.dgvbomno.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvbomno.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvbomno.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvbomno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvbomno.Location = new System.Drawing.Point(0, 100);
            this.dgvbomno.Name = "dgvbomno";
            this.dgvbomno.ReadOnly = true;
            this.dgvbomno.RowTemplate.Height = 23;
            this.dgvbomno.Size = new System.Drawing.Size(298, 435);
            this.dgvbomno.TabIndex = 3;
            this.dgvbomno.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvbomno_CellMouseClick);
            this.dgvbomno.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvbomno_CellLeave);
            this.dgvbomno.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvbomno_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序列号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Bom编号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.tbbom);
            this.panelEx2.Controls.Add(this.label1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(298, 100);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelEx2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 2;
            this.panelEx2.TabStop = true;
            this.panelEx2.TextDockConstrained = false;
            // 
            // tbbom
            // 
            this.tbbom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tbbom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbbom.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbbom.Location = new System.Drawing.Point(17, 46);
            this.tbbom.Name = "tbbom";
            this.tbbom.Size = new System.Drawing.Size(219, 26);
            this.tbbom.TabIndex = 1;
            this.tbbom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbbom_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "【查询Bom编号】";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.dgvkeyparts);
            this.panelEx3.Controls.Add(this.panelEx4);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelEx3.Location = new System.Drawing.Point(719, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(345, 535);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 1;
            // 
            // dgvkeyparts
            // 
            this.dgvkeyparts.AllowUserToAddRows = false;
            this.dgvkeyparts.AllowUserToDeleteRows = false;
            this.dgvkeyparts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvkeyparts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column9,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvkeyparts.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvkeyparts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvkeyparts.Location = new System.Drawing.Point(0, 100);
            this.dgvkeyparts.Name = "dgvkeyparts";
            this.dgvkeyparts.ReadOnly = true;
            this.dgvkeyparts.RowTemplate.Height = 23;
            this.dgvkeyparts.Size = new System.Drawing.Size(345, 435);
            this.dgvkeyparts.TabIndex = 1;
            this.dgvkeyparts.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvkeyparts_CellLeave);
            this.dgvkeyparts.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvkeyparts_RowPostPaint);
            this.dgvkeyparts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvkeyparts_CellClick);
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.btaddparts);
            this.panelEx4.Controls.Add(this.tbparts);
            this.panelEx4.Controls.Add(this.label2);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx4.Location = new System.Drawing.Point(0, 0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(345, 100);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelEx4.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 0;
            // 
            // btaddparts
            // 
            this.btaddparts.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btaddparts.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btaddparts.Location = new System.Drawing.Point(246, 46);
            this.btaddparts.Name = "btaddparts";
            this.btaddparts.Size = new System.Drawing.Size(28, 26);
            this.btaddparts.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btaddparts.TabIndex = 2;
            this.btaddparts.Click += new System.EventHandler(this.btaddparts_Click);
            // 
            // tbparts
            // 
            this.tbparts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tbparts.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbparts.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbparts.Location = new System.Drawing.Point(21, 46);
            this.tbparts.Name = "tbparts";
            this.tbparts.Size = new System.Drawing.Size(219, 26);
            this.tbparts.TabIndex = 1;
            this.tbparts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbparts_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(14, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "【查询料号】";
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.panelEx9);
            this.panelEx5.Controls.Add(this.panelEx8);
            this.panelEx5.Controls.Add(this.panelEx7);
            this.panelEx5.Controls.Add(this.panelEx6);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx5.Location = new System.Drawing.Point(298, 0);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(421, 535);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 2;
            // 
            // panelEx9
            // 
            this.panelEx9.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx9.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx9.Controls.Add(this.dgvshowlist);
            this.panelEx9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx9.Location = new System.Drawing.Point(0, 100);
            this.panelEx9.Name = "panelEx9";
            this.panelEx9.Size = new System.Drawing.Size(220, 244);
            this.panelEx9.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx9.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx9.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx9.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx9.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx9.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx9.Style.GradientAngle = 90;
            this.panelEx9.TabIndex = 4;
            // 
            // dgvshowlist
            // 
            this.dgvshowlist.AllowUserToAddRows = false;
            this.dgvshowlist.AllowUserToDeleteRows = false;
            this.dgvshowlist.AllowUserToOrderColumns = true;
            this.dgvshowlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvshowlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvshowlist.Location = new System.Drawing.Point(0, 0);
            this.dgvshowlist.Name = "dgvshowlist";
            this.dgvshowlist.ReadOnly = true;
            this.dgvshowlist.RowTemplate.Height = 23;
            this.dgvshowlist.Size = new System.Drawing.Size(220, 244);
            this.dgvshowlist.TabIndex = 0;
            this.dgvshowlist.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvshowlist_RowPostPaint);
            this.dgvshowlist.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvshowlist_CellClick);
            // 
            // panelEx8
            // 
            this.panelEx8.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx8.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx8.Controls.Add(this.label6);
            this.panelEx8.Controls.Add(this.cbstationlist);
            this.panelEx8.Controls.Add(this.lbtoright);
            this.panelEx8.Controls.Add(this.lbtoleft);
            this.panelEx8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelEx8.Location = new System.Drawing.Point(220, 100);
            this.panelEx8.Name = "panelEx8";
            this.panelEx8.Size = new System.Drawing.Size(201, 244);
            this.panelEx8.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx8.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx8.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx8.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx8.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx8.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx8.Style.GradientAngle = 90;
            this.panelEx8.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(32, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 14);
            this.label6.TabIndex = 2;
            this.label6.Text = "【途程】";
            // 
            // cbstationlist
            // 
            this.cbstationlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbstationlist.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbstationlist.FormattingEnabled = true;
            this.cbstationlist.Location = new System.Drawing.Point(40, 41);
            this.cbstationlist.Name = "cbstationlist";
            this.cbstationlist.Size = new System.Drawing.Size(121, 24);
            this.cbstationlist.TabIndex = 1;
            // 
            // lbtoright
            // 
            this.lbtoright.AutoSize = true;
            this.lbtoright.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbtoright.Enabled = false;
            this.lbtoright.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbtoright.Location = new System.Drawing.Point(44, 173);
            this.lbtoright.Name = "lbtoright";
            this.lbtoright.Size = new System.Drawing.Size(111, 33);
            this.lbtoright.TabIndex = 0;
            this.lbtoright.Text = "----->";
            this.lbtoright.MouseLeave += new System.EventHandler(this.lbtoright_MouseLeave);
            this.lbtoright.Click += new System.EventHandler(this.lbtoright_Click);
            this.lbtoright.MouseEnter += new System.EventHandler(this.lbtoright_MouseEnter);
            // 
            // lbtoleft
            // 
            this.lbtoleft.AutoSize = true;
            this.lbtoleft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbtoleft.Enabled = false;
            this.lbtoleft.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbtoleft.Location = new System.Drawing.Point(44, 91);
            this.lbtoleft.Name = "lbtoleft";
            this.lbtoleft.Size = new System.Drawing.Size(111, 33);
            this.lbtoleft.TabIndex = 0;
            this.lbtoleft.Text = "<-----";
            this.lbtoleft.MouseLeave += new System.EventHandler(this.lbtoleft_MouseLeave);
            this.lbtoleft.Click += new System.EventHandler(this.lbtoleft_Click);
            this.lbtoleft.MouseEnter += new System.EventHandler(this.lbtoleft_MouseEnter);
            // 
            // panelEx7
            // 
            this.panelEx7.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx7.Controls.Add(this.btdelete);
            this.panelEx7.Controls.Add(this.btmodify);
            this.panelEx7.Controls.Add(this.btadd);
            this.panelEx7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx7.Location = new System.Drawing.Point(0, 344);
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Size = new System.Drawing.Size(421, 191);
            this.panelEx7.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx7.Style.GradientAngle = 90;
            this.panelEx7.TabIndex = 2;
            // 
            // btdelete
            // 
            this.btdelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btdelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btdelete.Location = new System.Drawing.Point(399, 27);
            this.btdelete.Name = "btdelete";
            this.btdelete.Size = new System.Drawing.Size(75, 45);
            this.btdelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btdelete.TabIndex = 2;
            this.btdelete.Text = "删除";
            this.btdelete.Click += new System.EventHandler(this.btdelete_Click);
            // 
            // btmodify
            // 
            this.btmodify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btmodify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btmodify.Location = new System.Drawing.Point(225, 28);
            this.btmodify.Name = "btmodify";
            this.btmodify.Size = new System.Drawing.Size(75, 44);
            this.btmodify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btmodify.TabIndex = 1;
            this.btmodify.Text = "修改";
            this.btmodify.Click += new System.EventHandler(this.btmodify_Click);
            // 
            // btadd
            // 
            this.btadd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btadd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btadd.Location = new System.Drawing.Point(60, 27);
            this.btadd.Name = "btadd";
            this.btadd.Size = new System.Drawing.Size(75, 44);
            this.btadd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btadd.TabIndex = 0;
            this.btadd.Text = "新增";
            this.btadd.Click += new System.EventHandler(this.btadd_Click);
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.btcancel);
            this.panelEx6.Controls.Add(this.tbaddbom);
            this.panelEx6.Controls.Add(this.label3);
            this.panelEx6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx6.Location = new System.Drawing.Point(0, 0);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(421, 100);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelEx6.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 1;
            // 
            // btcancel
            // 
            this.btcancel.Location = new System.Drawing.Point(305, 46);
            this.btcancel.Name = "btcancel";
            this.btcancel.Size = new System.Drawing.Size(75, 26);
            this.btcancel.TabIndex = 2;
            this.btcancel.Text = "Cancel";
            this.btcancel.UseVisualStyleBackColor = true;
            this.btcancel.Click += new System.EventHandler(this.btcancel_Click);
            // 
            // tbaddbom
            // 
            this.tbaddbom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tbaddbom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbaddbom.Enabled = false;
            this.tbaddbom.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbaddbom.Location = new System.Drawing.Point(21, 46);
            this.tbaddbom.Name = "tbaddbom";
            this.tbaddbom.Size = new System.Drawing.Size(219, 26);
            this.tbaddbom.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(14, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "【新建BOM】";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "kpnumber";
            this.Column3.HeaderText = "物料料号";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "PartNumber";
            this.Column9.HeaderText = "成品料号";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "kpname";
            this.Column4.HeaderText = "物料名称";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "kpdesc";
            this.Column5.HeaderText = "物料描述";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // FrmBomNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 535);
            this.Controls.Add(this.panelEx5);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.Name = "FrmBomNo";
            this.Text = "Bom建立";
            this.Load += new System.EventHandler(this.FrmBomNo_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvbomno)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvkeyparts)).EndInit();
            this.panelEx4.ResumeLayout(false);
            this.panelEx4.PerformLayout();
            this.panelEx5.ResumeLayout(false);
            this.panelEx9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvshowlist)).EndInit();
            this.panelEx8.ResumeLayout(false);
            this.panelEx8.PerformLayout();
            this.panelEx7.ResumeLayout(false);
            this.panelEx6.ResumeLayout(false);
            this.panelEx6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvbomno;
        private System.Windows.Forms.TextBox tbbom;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private System.Windows.Forms.TextBox tbparts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvkeyparts;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.PanelEx panelEx9;
        private DevComponents.DotNetBar.PanelEx panelEx8;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private System.Windows.Forms.TextBox tbaddbom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvshowlist;
        private System.Windows.Forms.Label lbtoright;
        private System.Windows.Forms.Label lbtoleft;
        private DevComponents.DotNetBar.ButtonX btdelete;
        private DevComponents.DotNetBar.ButtonX btmodify;
        private DevComponents.DotNetBar.ButtonX btadd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbstationlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private DevComponents.DotNetBar.ButtonX btaddparts;
        private System.Windows.Forms.Button btcancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}