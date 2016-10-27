namespace SFIS_V2
{
    partial class fmParMap
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.edqrypn = new System.Windows.Forms.TextBox();
            this.butmodify = new DevComponents.DotNetBar.ButtonX();
            this.butadd = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.edtoldpn = new System.Windows.Forms.TextBox();
            this.edpndesc = new System.Windows.Forms.TextBox();
            this.edtpn = new System.Windows.Forms.TextBox();
            this.chmodify = new System.Windows.Forms.CheckBox();
            this.butcancel = new DevComponents.DotNetBar.ButtonX();
            this.butok = new DevComponents.DotNetBar.ButtonX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtvenkp = new System.Windows.Forms.TextBox();
            this.kpnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selfkpnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.venderkpnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kpdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dataGridViewX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(820, 237);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kpnumber,
            this.selfkpnumber,
            this.venderkpnumber,
            this.kpdesc});
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
            this.dataGridViewX1.Size = new System.Drawing.Size(820, 237);
            this.dataGridViewX1.TabIndex = 0;
            this.dataGridViewX1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseClick);
            this.dataGridViewX1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewX1_RowPostPaint);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.edqrypn);
            this.panelEx2.Controls.Add(this.butmodify);
            this.panelEx2.Controls.Add(this.butadd);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 237);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(820, 62);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // edqrypn
            // 
            this.edqrypn.Location = new System.Drawing.Point(69, 16);
            this.edqrypn.Name = "edqrypn";
            this.edqrypn.Size = new System.Drawing.Size(265, 21);
            this.edqrypn.TabIndex = 24;
            this.edqrypn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edqrypn_KeyPress);
            // 
            // butmodify
            // 
            this.butmodify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butmodify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butmodify.Location = new System.Drawing.Point(648, 16);
            this.butmodify.Name = "butmodify";
            this.butmodify.Size = new System.Drawing.Size(75, 23);
            this.butmodify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butmodify.TabIndex = 9;
            this.butmodify.Text = "修改";
            this.butmodify.Click += new System.EventHandler(this.butmodify_Click);
            // 
            // butadd
            // 
            this.butadd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butadd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butadd.Location = new System.Drawing.Point(541, 16);
            this.butadd.Name = "butadd";
            this.butadd.Size = new System.Drawing.Size(75, 23);
            this.butadd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butadd.TabIndex = 8;
            this.butadd.Text = "新增";
            this.butadd.Click += new System.EventHandler(this.butadd_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(30, 18);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(46, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "查询:";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.panel1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 299);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(820, 130);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.edtoldpn);
            this.panel1.Controls.Add(this.edpndesc);
            this.panel1.Controls.Add(this.txtvenkp);
            this.panel1.Controls.Add(this.edtpn);
            this.panel1.Controls.Add(this.chmodify);
            this.panel1.Controls.Add(this.butcancel);
            this.panel1.Controls.Add(this.butok);
            this.panel1.Controls.Add(this.labelX4);
            this.panel1.Controls.Add(this.labelX5);
            this.panel1.Controls.Add(this.labelX3);
            this.panel1.Controls.Add(this.labelX2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(820, 130);
            this.panel1.TabIndex = 0;
            // 
            // edtoldpn
            // 
            this.edtoldpn.Location = new System.Drawing.Point(340, 36);
            this.edtoldpn.Name = "edtoldpn";
            this.edtoldpn.Size = new System.Drawing.Size(182, 21);
            this.edtoldpn.TabIndex = 27;
            // 
            // edpndesc
            // 
            this.edpndesc.Location = new System.Drawing.Point(84, 102);
            this.edpndesc.Name = "edpndesc";
            this.edpndesc.Size = new System.Drawing.Size(438, 21);
            this.edpndesc.TabIndex = 26;
            // 
            // edtpn
            // 
            this.edtpn.Location = new System.Drawing.Point(84, 36);
            this.edtpn.Name = "edtpn";
            this.edtpn.Size = new System.Drawing.Size(186, 21);
            this.edtpn.TabIndex = 25;
            this.edtpn.Leave += new System.EventHandler(this.edtpn_Leave);
            // 
            // chmodify
            // 
            this.chmodify.AutoSize = true;
            this.chmodify.Location = new System.Drawing.Point(541, 37);
            this.chmodify.Name = "chmodify";
            this.chmodify.Size = new System.Drawing.Size(48, 16);
            this.chmodify.TabIndex = 24;
            this.chmodify.Text = "修改";
            this.chmodify.UseVisualStyleBackColor = true;
            this.chmodify.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chmodify_MouseClick);
            // 
            // butcancel
            // 
            this.butcancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butcancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butcancel.Location = new System.Drawing.Point(700, 79);
            this.butcancel.Name = "butcancel";
            this.butcancel.Size = new System.Drawing.Size(75, 23);
            this.butcancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butcancel.TabIndex = 21;
            this.butcancel.Text = "Cancel";
            this.butcancel.Click += new System.EventHandler(this.butcancel_Click);
            // 
            // butok
            // 
            this.butok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butok.Location = new System.Drawing.Point(700, 30);
            this.butok.Name = "butok";
            this.butok.Size = new System.Drawing.Size(75, 23);
            this.butok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butok.TabIndex = 20;
            this.butok.Text = "OK";
            this.butok.Click += new System.EventHandler(this.butok_Click);
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.Location = new System.Drawing.Point(36, 106);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(46, 23);
            this.labelX4.TabIndex = 18;
            this.labelX4.Text = "描述:";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Location = new System.Drawing.Point(284, 37);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(50, 23);
            this.labelX3.TabIndex = 16;
            this.labelX3.Text = "原料号:";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Location = new System.Drawing.Point(36, 40);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(38, 23);
            this.labelX2.TabIndex = 14;
            this.labelX2.Text = "料号:";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.Location = new System.Drawing.Point(12, 74);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(62, 23);
            this.labelX5.TabIndex = 14;
            this.labelX5.Text = "原厂料号:";
            // 
            // txtvenkp
            // 
            this.txtvenkp.Location = new System.Drawing.Point(84, 70);
            this.txtvenkp.Name = "txtvenkp";
            this.txtvenkp.Size = new System.Drawing.Size(186, 21);
            this.txtvenkp.TabIndex = 25;
            this.txtvenkp.Leave += new System.EventHandler(this.edtpn_Leave);
            // 
            // kpnumber
            // 
            this.kpnumber.DataPropertyName = "kpnumber";
            this.kpnumber.HeaderText = "新料号";
            this.kpnumber.Name = "kpnumber";
            this.kpnumber.ReadOnly = true;
            this.kpnumber.Width = 150;
            // 
            // selfkpnumber
            // 
            this.selfkpnumber.DataPropertyName = "selfkpnumber";
            this.selfkpnumber.HeaderText = "原料号";
            this.selfkpnumber.Name = "selfkpnumber";
            this.selfkpnumber.ReadOnly = true;
            this.selfkpnumber.Width = 150;
            // 
            // venderkpnumber
            // 
            this.venderkpnumber.DataPropertyName = "venderkpnumber";
            this.venderkpnumber.HeaderText = "原厂料号";
            this.venderkpnumber.Name = "venderkpnumber";
            this.venderkpnumber.ReadOnly = true;
            this.venderkpnumber.Width = 150;
            // 
            // kpdesc
            // 
            this.kpdesc.DataPropertyName = "kpdesc";
            this.kpdesc.HeaderText = "物料描述";
            this.kpdesc.Name = "kpdesc";
            this.kpdesc.ReadOnly = true;
            this.kpdesc.Width = 300;
            // 
            // fmParMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(820, 429);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmParMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加物料规格";
            this.Load += new System.EventHandler(this.fmParMap_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.panelEx3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX butmodify;
        private DevComponents.DotNetBar.ButtonX butadd;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX butcancel;
        private DevComponents.DotNetBar.ButtonX butok;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.CheckBox chmodify;
        private System.Windows.Forms.TextBox edqrypn;
        private System.Windows.Forms.TextBox edtpn;
        private System.Windows.Forms.TextBox edtoldpn;
        private System.Windows.Forms.TextBox edpndesc;
        private System.Windows.Forms.TextBox txtvenkp;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.DataGridViewTextBoxColumn kpnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn selfkpnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn venderkpnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn kpdesc;
    }
}