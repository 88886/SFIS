namespace SFIS_V2
{
    partial class formworkshop
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
            this.butDelete = new DevComponents.DotNetBar.ButtonX();
            this.butModify = new DevComponents.DotNetBar.ButtonX();
            this.butAdd = new DevComponents.DotNetBar.ButtonX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_getwsid = new DevComponents.DotNetBar.ButtonX();
            this.bt_addFac = new DevComponents.DotNetBar.ButtonX();
            this.bt_qutryuser = new DevComponents.DotNetBar.ButtonX();
            this.textfacid = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.butOK = new DevComponents.DotNetBar.ButtonX();
            this.textwsname = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textuserid = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textwsid = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
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
            this.panelEx1.Size = new System.Drawing.Size(906, 227);
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
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            this.dataGridViewX1.Size = new System.Drawing.Size(906, 227);
            this.dataGridViewX1.TabIndex = 0;
            this.dataGridViewX1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseClick);
            this.dataGridViewX1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseDoubleClick);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.butDelete);
            this.panelEx2.Controls.Add(this.butModify);
            this.panelEx2.Controls.Add(this.butAdd);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 227);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(906, 85);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // butDelete
            // 
            this.butDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butDelete.Location = new System.Drawing.Point(716, 18);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(66, 48);
            this.butDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butDelete.TabIndex = 2;
            this.butDelete.Text = "删除";
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // butModify
            // 
            this.butModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butModify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butModify.Location = new System.Drawing.Point(563, 18);
            this.butModify.Name = "butModify";
            this.butModify.Size = new System.Drawing.Size(64, 48);
            this.butModify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butModify.TabIndex = 1;
            this.butModify.Text = "修改";
            this.butModify.Click += new System.EventHandler(this.butModify_Click);
            // 
            // butAdd
            // 
            this.butAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butAdd.Location = new System.Drawing.Point(422, 18);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(64, 48);
            this.butAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butAdd.TabIndex = 0;
            this.butAdd.Text = "新增";
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.panel1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 312);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(906, 206);
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
            this.panel1.Controls.Add(this.bt_getwsid);
            this.panel1.Controls.Add(this.bt_addFac);
            this.panel1.Controls.Add(this.bt_qutryuser);
            this.panel1.Controls.Add(this.textfacid);
            this.panel1.Controls.Add(this.buttonX1);
            this.panel1.Controls.Add(this.butOK);
            this.panel1.Controls.Add(this.textwsname);
            this.panel1.Controls.Add(this.textuserid);
            this.panel1.Controls.Add(this.textwsid);
            this.panel1.Controls.Add(this.labelX4);
            this.panel1.Controls.Add(this.labelX3);
            this.panel1.Controls.Add(this.labelX2);
            this.panel1.Controls.Add(this.labelX1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(906, 206);
            this.panel1.TabIndex = 0;
            // 
            // bt_getwsid
            // 
            this.bt_getwsid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_getwsid.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_getwsid.Location = new System.Drawing.Point(544, 23);
            this.bt_getwsid.Name = "bt_getwsid";
            this.bt_getwsid.Size = new System.Drawing.Size(62, 20);
            this.bt_getwsid.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_getwsid.TabIndex = 22;
            this.bt_getwsid.Text = "获取编号";
            this.bt_getwsid.Click += new System.EventHandler(this.bt_getwsid_Click);
            // 
            // bt_addFac
            // 
            this.bt_addFac.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_addFac.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_addFac.Location = new System.Drawing.Point(544, 69);
            this.bt_addFac.Name = "bt_addFac";
            this.bt_addFac.Size = new System.Drawing.Size(62, 20);
            this.bt_addFac.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_addFac.TabIndex = 22;
            this.bt_addFac.Text = "添加工厂";
            this.bt_addFac.Click += new System.EventHandler(this.bt_addFac_Click);
            // 
            // bt_qutryuser
            // 
            this.bt_qutryuser.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_qutryuser.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_qutryuser.Location = new System.Drawing.Point(544, 116);
            this.bt_qutryuser.Name = "bt_qutryuser";
            this.bt_qutryuser.Size = new System.Drawing.Size(62, 20);
            this.bt_qutryuser.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_qutryuser.TabIndex = 21;
            this.bt_qutryuser.Text = "选择用户";
            this.bt_qutryuser.Click += new System.EventHandler(this.bt_qutryuser_Click);
            // 
            // textfacid
            // 
            this.textfacid.DisplayMember = "Text";
            this.textfacid.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.textfacid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textfacid.FormattingEnabled = true;
            this.textfacid.ItemHeight = 15;
            this.textfacid.Location = new System.Drawing.Point(191, 69);
            this.textfacid.Name = "textfacid";
            this.textfacid.Size = new System.Drawing.Size(350, 21);
            this.textfacid.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.textfacid.TabIndex = 20;
            this.textfacid.DropDown += new System.EventHandler(this.textfacid_DropDown);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(716, 104);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 19;
            this.buttonX1.Text = "Cancel";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // butOK
            // 
            this.butOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butOK.Location = new System.Drawing.Point(716, 44);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butOK.TabIndex = 18;
            this.butOK.Text = "OK";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // textwsname
            // 
            // 
            // 
            // 
            this.textwsname.Border.Class = "TextBoxBorder";
            this.textwsname.Location = new System.Drawing.Point(191, 163);
            this.textwsname.Name = "textwsname";
            this.textwsname.Size = new System.Drawing.Size(350, 21);
            this.textwsname.TabIndex = 17;
            // 
            // textuserid
            // 
            // 
            // 
            // 
            this.textuserid.Border.Class = "TextBoxBorder";
            this.textuserid.Location = new System.Drawing.Point(191, 116);
            this.textuserid.Name = "textuserid";
            this.textuserid.Size = new System.Drawing.Size(350, 21);
            this.textuserid.TabIndex = 16;
            // 
            // textwsid
            // 
            // 
            // 
            // 
            this.textwsid.Border.Class = "TextBoxBorder";
            this.textwsid.Location = new System.Drawing.Point(191, 23);
            this.textwsid.Name = "textwsid";
            this.textwsid.Size = new System.Drawing.Size(350, 21);
            this.textwsid.TabIndex = 14;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.Location = new System.Drawing.Point(136, 160);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(46, 20);
            this.labelX4.TabIndex = 13;
            this.labelX4.Text = "名称:";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Location = new System.Drawing.Point(125, 116);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(57, 23);
            this.labelX3.TabIndex = 12;
            this.labelX3.Text = "负责人:";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Location = new System.Drawing.Point(116, 69);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(66, 23);
            this.labelX2.TabIndex = 11;
            this.labelX2.Text = "工厂编号:";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(116, 23);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(66, 23);
            this.labelX1.TabIndex = 10;
            this.labelX1.Text = "车间编号:";
            // 
            // formworkshop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 518);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "formworkshop";
            this.Text = "车间设定";
            this.Load += new System.EventHandler(this.formworkshop_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ButtonX butAdd;
        private DevComponents.DotNetBar.ButtonX butDelete;
        private DevComponents.DotNetBar.ButtonX butModify;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX butOK;
        private DevComponents.DotNetBar.Controls.TextBoxX textwsname;
        private DevComponents.DotNetBar.Controls.TextBoxX textwsid;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx textfacid;
        private DevComponents.DotNetBar.ButtonX bt_qutryuser;
        public DevComponents.DotNetBar.Controls.TextBoxX textuserid;
        private DevComponents.DotNetBar.ButtonX bt_addFac;
        private DevComponents.DotNetBar.ButtonX bt_getwsid;
    }
}