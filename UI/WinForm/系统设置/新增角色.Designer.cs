namespace SFIS_V2
{
    partial class fRoleInfo
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
            this.butadd = new DevComponents.DotNetBar.ButtonX();
            this.edtroledesc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.edtrolelevel = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.edtrolecaption = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.角色名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.角色等级 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.角色说明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.panelEx1.Size = new System.Drawing.Size(634, 201);
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
            this.角色名称,
            this.角色等级,
            this.角色说明});
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
            this.dataGridViewX1.Size = new System.Drawing.Size(634, 201);
            this.dataGridViewX1.TabIndex = 0;
            this.dataGridViewX1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseClick);
            this.dataGridViewX1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellMouseEnter);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.butadd);
            this.panelEx2.Controls.Add(this.edtroledesc);
            this.panelEx2.Controls.Add(this.edtrolelevel);
            this.panelEx2.Controls.Add(this.edtrolecaption);
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 201);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(634, 173);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // butadd
            // 
            this.butadd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butadd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butadd.Location = new System.Drawing.Point(454, 24);
            this.butadd.Name = "butadd";
            this.butadd.Size = new System.Drawing.Size(75, 23);
            this.butadd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butadd.TabIndex = 6;
            this.butadd.Text = "新增";
            this.butadd.Click += new System.EventHandler(this.butadd_Click);
            // 
            // edtroledesc
            // 
            // 
            // 
            // 
            this.edtroledesc.Border.Class = "TextBoxBorder";
            this.edtroledesc.Location = new System.Drawing.Point(119, 116);
            this.edtroledesc.Name = "edtroledesc";
            this.edtroledesc.Size = new System.Drawing.Size(282, 21);
            this.edtroledesc.TabIndex = 5;
            this.edtroledesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtroledesc_KeyPress);
            // 
            // edtrolelevel
            // 
            // 
            // 
            // 
            this.edtrolelevel.Border.Class = "TextBoxBorder";
            this.edtrolelevel.Location = new System.Drawing.Point(119, 71);
            this.edtrolelevel.Name = "edtrolelevel";
            this.edtrolelevel.Size = new System.Drawing.Size(282, 21);
            this.edtrolelevel.TabIndex = 4;
            this.edtrolelevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtrolelevel_KeyPress);
            // 
            // edtrolecaption
            // 
            // 
            // 
            // 
            this.edtrolecaption.Border.Class = "TextBoxBorder";
            this.edtrolecaption.Location = new System.Drawing.Point(120, 24);
            this.edtrolecaption.Name = "edtrolecaption";
            this.edtrolecaption.Size = new System.Drawing.Size(281, 21);
            this.edtrolecaption.TabIndex = 3;
            this.edtrolecaption.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtrolecaption_KeyPress);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Location = new System.Drawing.Point(49, 116);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(64, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "角色说明:";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Location = new System.Drawing.Point(50, 71);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "角色等级:";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(50, 28);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "角色名称:";
            // 
            // 角色名称
            // 
            this.角色名称.DataPropertyName = "rolecaption";
            this.角色名称.HeaderText = "角色名称";
            this.角色名称.Name = "角色名称";
            this.角色名称.ReadOnly = true;
            // 
            // 角色等级
            // 
            this.角色等级.DataPropertyName = "rolelevel";
            this.角色等级.HeaderText = "角色等级";
            this.角色等级.Name = "角色等级";
            this.角色等级.ReadOnly = true;
            // 
            // 角色说明
            // 
            this.角色说明.DataPropertyName = "roledesc";
            this.角色说明.HeaderText = "角色说明";
            this.角色说明.Name = "角色说明";
            this.角色说明.ReadOnly = true;
            // 
            // fRoleInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 374);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fRoleInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增角色";
            this.Load += new System.EventHandler(this.fRoleInfo_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fRoleInfo_FormClosed);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX edtroledesc;
        private DevComponents.DotNetBar.Controls.TextBoxX edtrolelevel;
        private DevComponents.DotNetBar.Controls.TextBoxX edtrolecaption;
        private DevComponents.DotNetBar.ButtonX butadd;
        private System.Windows.Forms.DataGridViewTextBoxColumn 角色名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 角色等级;
        private System.Windows.Forms.DataGridViewTextBoxColumn 角色说明;
    }
}