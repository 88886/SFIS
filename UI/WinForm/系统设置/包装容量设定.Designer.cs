namespace SFIS_V2
{
    partial class FrmPackParameters
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tb_QryPartNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.PalData = new System.Windows.Forms.Panel();
            this.txt_PartNumber = new System.Windows.Forms.ComboBox();
            this.lbcancel = new DevComponents.DotNetBar.LabelX();
            this.lbok = new DevComponents.DotNetBar.LabelX();
            this.num_palletqty = new System.Windows.Forms.NumericUpDown();
            this.num_cartonqty = new System.Windows.Forms.NumericUpDown();
            this.num_trayqty = new System.Windows.Forms.NumericUpDown();
            this.txt_VersionCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.btDeletePackParam = new DevComponents.DotNetBar.ButtonX();
            this.btModifyPackParam = new DevComponents.DotNetBar.ButtonX();
            this.btAddPackParam = new DevComponents.DotNetBar.ButtonX();
            this.dgvPackParam = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.PalData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_palletqty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_cartonqty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_trayqty)).BeginInit();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackParam)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.tb_QryPartNo);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(893, 69);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // tb_QryPartNo
            // 
            this.tb_QryPartNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tb_QryPartNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_QryPartNo.Location = new System.Drawing.Point(152, 21);
            this.tb_QryPartNo.Name = "tb_QryPartNo";
            this.tb_QryPartNo.Size = new System.Drawing.Size(304, 26);
            this.tb_QryPartNo.TabIndex = 1;
            this.tb_QryPartNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_QryPartNo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(34, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "查询成品料号:";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.PalData);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 209);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(893, 225);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // PalData
            // 
            this.PalData.Controls.Add(this.txt_PartNumber);
            this.PalData.Controls.Add(this.lbcancel);
            this.PalData.Controls.Add(this.lbok);
            this.PalData.Controls.Add(this.num_palletqty);
            this.PalData.Controls.Add(this.num_cartonqty);
            this.PalData.Controls.Add(this.num_trayqty);
            this.PalData.Controls.Add(this.txt_VersionCode);
            this.PalData.Controls.Add(this.label6);
            this.PalData.Controls.Add(this.label5);
            this.PalData.Controls.Add(this.label4);
            this.PalData.Controls.Add(this.label3);
            this.PalData.Controls.Add(this.label2);
            this.PalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PalData.Location = new System.Drawing.Point(0, 0);
            this.PalData.Name = "PalData";
            this.PalData.Size = new System.Drawing.Size(893, 202);
            this.PalData.TabIndex = 0;
            // 
            // txt_PartNumber
            // 
            this.txt_PartNumber.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_PartNumber.FormattingEnabled = true;
            this.txt_PartNumber.Location = new System.Drawing.Point(113, 33);
            this.txt_PartNumber.Name = "txt_PartNumber";
            this.txt_PartNumber.Size = new System.Drawing.Size(208, 22);
            this.txt_PartNumber.TabIndex = 11;
            // 
            // lbcancel
            // 
            this.lbcancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.lbcancel.BackgroundStyle.Class = "";
            this.lbcancel.Location = new System.Drawing.Point(760, 102);
            this.lbcancel.Name = "lbcancel";
            this.lbcancel.Size = new System.Drawing.Size(75, 30);
            this.lbcancel.TabIndex = 10;
            this.lbcancel.Text = "Cancel";
            this.lbcancel.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lbcancel.Click += new System.EventHandler(this.lbcancel_Click);
            this.lbcancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbcancel_MouseDown);
            this.lbcancel.MouseEnter += new System.EventHandler(this.lbcancel_MouseEnter);
            this.lbcancel.MouseLeave += new System.EventHandler(this.lbcancel_MouseLeave);
            // 
            // lbok
            // 
            this.lbok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.lbok.BackgroundStyle.Class = "";
            this.lbok.Location = new System.Drawing.Point(760, 36);
            this.lbok.Name = "lbok";
            this.lbok.Size = new System.Drawing.Size(75, 30);
            this.lbok.TabIndex = 10;
            this.lbok.Text = "OK";
            this.lbok.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lbok.Click += new System.EventHandler(this.lbok_Click);
            this.lbok.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbok_MouseDown);
            this.lbok.MouseEnter += new System.EventHandler(this.lbok_MouseEnter);
            this.lbok.MouseLeave += new System.EventHandler(this.lbok_MouseLeave);
            // 
            // num_palletqty
            // 
            this.num_palletqty.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_palletqty.Location = new System.Drawing.Point(115, 133);
            this.num_palletqty.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_palletqty.Name = "num_palletqty";
            this.num_palletqty.Size = new System.Drawing.Size(123, 23);
            this.num_palletqty.TabIndex = 9;
            this.num_palletqty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // num_cartonqty
            // 
            this.num_cartonqty.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_cartonqty.Location = new System.Drawing.Point(435, 84);
            this.num_cartonqty.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_cartonqty.Name = "num_cartonqty";
            this.num_cartonqty.Size = new System.Drawing.Size(120, 23);
            this.num_cartonqty.TabIndex = 8;
            this.num_cartonqty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // num_trayqty
            // 
            this.num_trayqty.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_trayqty.Location = new System.Drawing.Point(116, 85);
            this.num_trayqty.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_trayqty.Name = "num_trayqty";
            this.num_trayqty.Size = new System.Drawing.Size(122, 23);
            this.num_trayqty.TabIndex = 7;
            this.num_trayqty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txt_VersionCode
            // 
            this.txt_VersionCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_VersionCode.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_VersionCode.Location = new System.Drawing.Point(435, 36);
            this.txt_VersionCode.Name = "txt_VersionCode";
            this.txt_VersionCode.Size = new System.Drawing.Size(205, 23);
            this.txt_VersionCode.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(35, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 4;
            this.label6.Text = "栈板容量:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(345, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "Carton容量:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(35, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tray容量:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(357, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "产品版本:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(37, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "产品料号:";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.labelX1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor;
            this.labelX1.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.labelX1.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.labelX1.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.labelX1.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Dash;
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelX1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.ForeColor = System.Drawing.Color.Red;
            this.labelX1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.labelX1.Location = new System.Drawing.Point(0, 202);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(893, 23);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "备注:  一个Tray装多少产品,一个Carton装多少个Tray,一个栈板装多少个Carton(没有Tray的产品,Carton容量直接输入装多少个产品)";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.btDeletePackParam);
            this.panelEx3.Controls.Add(this.btModifyPackParam);
            this.panelEx3.Controls.Add(this.btAddPackParam);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx3.Location = new System.Drawing.Point(0, 126);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(893, 83);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 2;
            // 
            // btDeletePackParam
            // 
            this.btDeletePackParam.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btDeletePackParam.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btDeletePackParam.Location = new System.Drawing.Point(474, 17);
            this.btDeletePackParam.Name = "btDeletePackParam";
            this.btDeletePackParam.Size = new System.Drawing.Size(75, 45);
            this.btDeletePackParam.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btDeletePackParam.TabIndex = 0;
            this.btDeletePackParam.Text = "删除";
            this.btDeletePackParam.Click += new System.EventHandler(this.btDeletePackParam_Click);
            // 
            // btModifyPackParam
            // 
            this.btModifyPackParam.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btModifyPackParam.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btModifyPackParam.Location = new System.Drawing.Point(317, 17);
            this.btModifyPackParam.Name = "btModifyPackParam";
            this.btModifyPackParam.Size = new System.Drawing.Size(75, 45);
            this.btModifyPackParam.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btModifyPackParam.TabIndex = 0;
            this.btModifyPackParam.Text = "修改";
            this.btModifyPackParam.Click += new System.EventHandler(this.btModifyPackParam_Click);
            // 
            // btAddPackParam
            // 
            this.btAddPackParam.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btAddPackParam.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btAddPackParam.Location = new System.Drawing.Point(161, 17);
            this.btAddPackParam.Name = "btAddPackParam";
            this.btAddPackParam.Size = new System.Drawing.Size(75, 45);
            this.btAddPackParam.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btAddPackParam.TabIndex = 0;
            this.btAddPackParam.Text = "新增";
            this.btAddPackParam.Click += new System.EventHandler(this.btAddPackParam_Click);
            // 
            // dgvPackParam
            // 
            this.dgvPackParam.AllowUserToAddRows = false;
            this.dgvPackParam.AllowUserToDeleteRows = false;
            this.dgvPackParam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPackParam.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvPackParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPackParam.Location = new System.Drawing.Point(0, 69);
            this.dgvPackParam.Name = "dgvPackParam";
            this.dgvPackParam.ReadOnly = true;
            this.dgvPackParam.RowTemplate.Height = 23;
            this.dgvPackParam.Size = new System.Drawing.Size(893, 57);
            this.dgvPackParam.TabIndex = 3;
            this.dgvPackParam.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPackParam_CellMouseClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Rowid";
            this.Column1.HeaderText = "RowId";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "PartNumber";
            this.Column2.HeaderText = "产品料号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 180;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "VersionCode";
            this.Column3.HeaderText = "产品版本";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "TrayQty";
            this.Column4.HeaderText = "Tray容量";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "CartonQty";
            this.Column5.HeaderText = "Carton容量";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "PalletQty";
            this.Column6.HeaderText = "栈板容量";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "RecDate";
            this.Column7.HeaderText = "时间";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 150;
            // 
            // FrmPackParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 434);
            this.Controls.Add(this.dgvPackParam);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmPackParameters";
            this.Text = "包装容量设定";
            this.Load += new System.EventHandler(this.FrmPackParameters_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.PalData.ResumeLayout(false);
            this.PalData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_palletqty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_cartonqty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_trayqty)).EndInit();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackParam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.TextBox tb_QryPartNo;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.DataGridView dgvPackParam;
        private DevComponents.DotNetBar.ButtonX btDeletePackParam;
        private DevComponents.DotNetBar.ButtonX btModifyPackParam;
        private DevComponents.DotNetBar.ButtonX btAddPackParam;
        private System.Windows.Forms.Panel PalData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.TextBox txt_VersionCode;
        private System.Windows.Forms.NumericUpDown num_palletqty;
        private System.Windows.Forms.NumericUpDown num_cartonqty;
        private System.Windows.Forms.NumericUpDown num_trayqty;
        private DevComponents.DotNetBar.LabelX lbcancel;
        private DevComponents.DotNetBar.LabelX lbok;
        private System.Windows.Forms.ComboBox txt_PartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}