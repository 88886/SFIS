namespace SFIS_V2
{
    partial class fworkfunctioninfo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.butdelete = new DevComponents.DotNetBar.ButtonX();
            this.butmodify = new DevComponents.DotNetBar.ButtonX();
            this.butadd = new DevComponents.DotNetBar.ButtonX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butCancel = new DevComponents.DotNetBar.ButtonX();
            this.butOK = new DevComponents.DotNetBar.ButtonX();
            this.edtwfdesc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.edtworkurl = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.edtwfcaption = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.edtwfid = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.edtrolecaption = new DevComponents.DotNetBar.Controls.ComboBoxEx();
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
            this.panelEx1.Size = new System.Drawing.Size(974, 202);
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.ReadOnly = true;
            this.dataGridViewX1.RowTemplate.Height = 23;
            this.dataGridViewX1.Size = new System.Drawing.Size(974, 202);
            this.dataGridViewX1.TabIndex = 0;
            this.dataGridViewX1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseClick);
            this.dataGridViewX1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseDoubleClick);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.butdelete);
            this.panelEx2.Controls.Add(this.butmodify);
            this.panelEx2.Controls.Add(this.butadd);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 202);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(974, 100);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // butdelete
            // 
            this.butdelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butdelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butdelete.Location = new System.Drawing.Point(798, 29);
            this.butdelete.Name = "butdelete";
            this.butdelete.Size = new System.Drawing.Size(69, 46);
            this.butdelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butdelete.TabIndex = 2;
            this.butdelete.Text = "删除";
            this.butdelete.Click += new System.EventHandler(this.butdelete_Click);
            // 
            // butmodify
            // 
            this.butmodify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butmodify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butmodify.Location = new System.Drawing.Point(620, 29);
            this.butmodify.Name = "butmodify";
            this.butmodify.Size = new System.Drawing.Size(69, 46);
            this.butmodify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butmodify.TabIndex = 1;
            this.butmodify.Text = "修改";
            this.butmodify.Click += new System.EventHandler(this.butmodify_Click);
            // 
            // butadd
            // 
            this.butadd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butadd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butadd.Location = new System.Drawing.Point(444, 29);
            this.butadd.Name = "butadd";
            this.butadd.Size = new System.Drawing.Size(69, 46);
            this.butadd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butadd.TabIndex = 0;
            this.butadd.Text = "新增";
            this.butadd.Click += new System.EventHandler(this.butadd_Click);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.panel1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 302);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(974, 232);
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
            this.panel1.Controls.Add(this.edtrolecaption);
            this.panel1.Controls.Add(this.butCancel);
            this.panel1.Controls.Add(this.butOK);
            this.panel1.Controls.Add(this.edtwfdesc);
            this.panel1.Controls.Add(this.labelX5);
            this.panel1.Controls.Add(this.edtworkurl);
            this.panel1.Controls.Add(this.edtwfcaption);
            this.panel1.Controls.Add(this.edtwfid);
            this.panel1.Controls.Add(this.labelX4);
            this.panel1.Controls.Add(this.labelX3);
            this.panel1.Controls.Add(this.labelX2);
            this.panel1.Controls.Add(this.labelX1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(974, 232);
            this.panel1.TabIndex = 0;
            // 
            // butCancel
            // 
            this.butCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butCancel.Location = new System.Drawing.Point(707, 114);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butCancel.TabIndex = 11;
            this.butCancel.Text = "Cancel";
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butOK.Location = new System.Drawing.Point(707, 39);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butOK.TabIndex = 10;
            this.butOK.Text = "OK";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // edtwfdesc
            // 
            // 
            // 
            // 
            this.edtwfdesc.Border.Class = "TextBoxBorder";
            this.edtwfdesc.Location = new System.Drawing.Point(98, 143);
            this.edtwfdesc.Name = "edtwfdesc";
            this.edtwfdesc.Size = new System.Drawing.Size(415, 21);
            this.edtwfdesc.TabIndex = 9;
            this.edtwfdesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtwfdesc_KeyPress);
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.Location = new System.Drawing.Point(32, 103);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(60, 21);
            this.labelX5.TabIndex = 8;
            this.labelX5.Text = "功能名称:";
            // 
            // edtworkurl
            // 
            // 
            // 
            // 
            this.edtworkurl.Border.Class = "TextBoxBorder";
            this.edtworkurl.Location = new System.Drawing.Point(98, 181);
            this.edtworkurl.Name = "edtworkurl";
            this.edtworkurl.Size = new System.Drawing.Size(415, 21);
            this.edtworkurl.TabIndex = 7;
            this.edtworkurl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtworkurl_KeyPress);
            // 
            // edtwfcaption
            // 
            // 
            // 
            // 
            this.edtwfcaption.Border.Class = "TextBoxBorder";
            this.edtwfcaption.Location = new System.Drawing.Point(98, 99);
            this.edtwfcaption.Name = "edtwfcaption";
            this.edtwfcaption.Size = new System.Drawing.Size(415, 21);
            this.edtwfcaption.TabIndex = 6;
            this.edtwfcaption.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtwfcaption_KeyPress);
            // 
            // edtwfid
            // 
            // 
            // 
            // 
            this.edtwfid.Border.Class = "TextBoxBorder";
            this.edtwfid.Location = new System.Drawing.Point(98, 16);
            this.edtwfid.Name = "edtwfid";
            this.edtwfid.Size = new System.Drawing.Size(415, 21);
            this.edtwfid.TabIndex = 4;
            this.edtwfid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtwfid_KeyPress);
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.Location = new System.Drawing.Point(32, 185);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(71, 23);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "功能连接:";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Location = new System.Drawing.Point(32, 148);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(69, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "功能说明:";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Location = new System.Drawing.Point(32, 61);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(63, 21);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "角色名称:";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(32, 20);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(69, 21);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "功能编号:";
            // 
            // edtrolecaption
            // 
            this.edtrolecaption.DisplayMember = "Text";
            this.edtrolecaption.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.edtrolecaption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.edtrolecaption.FormattingEnabled = true;
            this.edtrolecaption.ItemHeight = 15;
            this.edtrolecaption.Location = new System.Drawing.Point(98, 59);
            this.edtrolecaption.Name = "edtrolecaption";
            this.edtrolecaption.Size = new System.Drawing.Size(415, 21);
            this.edtrolecaption.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.edtrolecaption.TabIndex = 12;
            // 
            // fworkfunctioninfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 534);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "fworkfunctioninfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "功能表设定";
            this.Load += new System.EventHandler(this.fworkfunctioninfo_Load);
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
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX butdelete;
        private DevComponents.DotNetBar.ButtonX butmodify;
        private DevComponents.DotNetBar.ButtonX butadd;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX edtwfdesc;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX edtworkurl;
        private DevComponents.DotNetBar.Controls.TextBoxX edtwfcaption;
        private DevComponents.DotNetBar.Controls.TextBoxX edtwfid;
        private DevComponents.DotNetBar.ButtonX butCancel;
        private DevComponents.DotNetBar.ButtonX butOK;
        private DevComponents.DotNetBar.Controls.ComboBoxEx edtrolecaption;

    }
}