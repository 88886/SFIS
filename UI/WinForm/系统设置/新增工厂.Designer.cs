namespace SFIS_V2
{
    partial class fFacInfo
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
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.cbx_facCode = new System.Windows.Forms.ComboBox();
            this.bt_getid = new DevComponents.DotNetBar.ButtonX();
            this.butadd = new DevComponents.DotNetBar.ButtonX();
            this.txt_address = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt_facname = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt_Facid = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.panelEx2.SuspendLayout();
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
            this.panelEx1.Size = new System.Drawing.Size(555, 153);
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
            this.Column1,
            this.Column2,
            this.Column3});
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
            this.dataGridViewX1.Size = new System.Drawing.Size(555, 153);
            this.dataGridViewX1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "facid";
            this.Column1.HeaderText = "工厂编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 120;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "facname";
            this.Column2.HeaderText = "工厂名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "address";
            this.Column3.HeaderText = "工厂地址";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 160;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.cbx_facCode);
            this.panelEx2.Controls.Add(this.bt_getid);
            this.panelEx2.Controls.Add(this.butadd);
            this.panelEx2.Controls.Add(this.txt_address);
            this.panelEx2.Controls.Add(this.txt_facname);
            this.panelEx2.Controls.Add(this.txt_Facid);
            this.panelEx2.Controls.Add(this.labelX4);
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 153);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(555, 199);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // cbx_facCode
            // 
            this.cbx_facCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_facCode.FormattingEnabled = true;
            this.cbx_facCode.Location = new System.Drawing.Point(85, 126);
            this.cbx_facCode.Name = "cbx_facCode";
            this.cbx_facCode.Size = new System.Drawing.Size(83, 20);
            this.cbx_facCode.TabIndex = 8;
            // 
            // bt_getid
            // 
            this.bt_getid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_getid.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_getid.Location = new System.Drawing.Point(366, 16);
            this.bt_getid.Name = "bt_getid";
            this.bt_getid.Size = new System.Drawing.Size(23, 20);
            this.bt_getid.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_getid.TabIndex = 7;
            this.bt_getid.Click += new System.EventHandler(this.bt_getid_Click);
            // 
            // butadd
            // 
            this.butadd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butadd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butadd.Location = new System.Drawing.Point(454, 46);
            this.butadd.Name = "butadd";
            this.butadd.Size = new System.Drawing.Size(71, 26);
            this.butadd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butadd.TabIndex = 6;
            this.butadd.Text = "OK";
            this.butadd.Click += new System.EventHandler(this.butadd_Click);
            // 
            // txt_address
            // 
            // 
            // 
            // 
            this.txt_address.Border.Class = "TextBoxBorder";
            this.txt_address.Location = new System.Drawing.Point(85, 96);
            this.txt_address.Name = "txt_address";
            this.txt_address.Size = new System.Drawing.Size(276, 21);
            this.txt_address.TabIndex = 5;
            this.txt_address.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtaddress_KeyPress);
            // 
            // txt_facname
            // 
            // 
            // 
            // 
            this.txt_facname.Border.Class = "TextBoxBorder";
            this.txt_facname.Location = new System.Drawing.Point(84, 57);
            this.txt_facname.Name = "txt_facname";
            this.txt_facname.Size = new System.Drawing.Size(277, 21);
            this.txt_facname.TabIndex = 4;
            this.txt_facname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtfacname_KeyPress);
            // 
            // txt_Facid
            // 
            // 
            // 
            // 
            this.txt_Facid.Border.Class = "TextBoxBorder";
            this.txt_Facid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Facid.Enabled = false;
            this.txt_Facid.Location = new System.Drawing.Point(83, 16);
            this.txt_Facid.MaxLength = 15;
            this.txt_Facid.Name = "txt_Facid";
            this.txt_Facid.Size = new System.Drawing.Size(278, 21);
            this.txt_Facid.TabIndex = 3;
            this.txt_Facid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtFacid_KeyPress);
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.Location = new System.Drawing.Point(20, 128);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(67, 23);
            this.labelX4.TabIndex = 2;
            this.labelX4.Text = "工厂代码:";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Location = new System.Drawing.Point(20, 97);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(67, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "工厂地址:";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Location = new System.Drawing.Point(20, 56);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(66, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "工厂名称:";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(20, 16);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(66, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "工厂编号:";
            // 
            // fFacInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 352);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fFacInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增工厂";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fFacInfo_FormClosed);
            this.Load += new System.EventHandler(this.fFacInfo_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX butadd;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_address;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_facname;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_Facid;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX bt_getid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.ComboBox cbx_facCode;
    }
}