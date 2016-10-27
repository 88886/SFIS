namespace SFIS_V2
{
    partial class FrmReasonCode
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
            this.tb_QryReason = new System.Windows.Forms.TextBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.PalData = new System.Windows.Forms.Panel();
            this.lbcancel = new DevComponents.DotNetBar.LabelX();
            this.lbok = new DevComponents.DotNetBar.LabelX();
            this.txt_reasondesc2 = new System.Windows.Forms.TextBox();
            this.txt_reasondesc = new System.Windows.Forms.TextBox();
            this.txt_dutystation = new System.Windows.Forms.TextBox();
            this.txt_ReasonCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.btReasonCode_Delete = new DevComponents.DotNetBar.ButtonX();
            this.btReasonCode_Modify = new DevComponents.DotNetBar.ButtonX();
            this.btReasonCode_Add = new DevComponents.DotNetBar.ButtonX();
            this.dgvReason = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_reasontype = new System.Windows.Forms.ComboBox();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.PalData.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReason)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.tb_QryReason);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(884, 53);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // tb_QryReason
            // 
            this.tb_QryReason.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tb_QryReason.Location = new System.Drawing.Point(145, 14);
            this.tb_QryReason.Name = "tb_QryReason";
            this.tb_QryReason.Size = new System.Drawing.Size(237, 21);
            this.tb_QryReason.TabIndex = 1;
            this.tb_QryReason.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_QryReason_KeyDown);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(28, 14);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(119, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "查询原因代码:";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.PalData);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 217);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(884, 228);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 2;
            // 
            // PalData
            // 
            this.PalData.Controls.Add(this.txt_reasontype);
            this.PalData.Controls.Add(this.lbcancel);
            this.PalData.Controls.Add(this.lbok);
            this.PalData.Controls.Add(this.txt_reasondesc2);
            this.PalData.Controls.Add(this.txt_reasondesc);
            this.PalData.Controls.Add(this.txt_dutystation);
            this.PalData.Controls.Add(this.txt_ReasonCode);
            this.PalData.Controls.Add(this.label3);
            this.PalData.Controls.Add(this.label5);
            this.PalData.Controls.Add(this.label4);
            this.PalData.Controls.Add(this.label2);
            this.PalData.Controls.Add(this.label1);
            this.PalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PalData.Location = new System.Drawing.Point(0, 0);
            this.PalData.Name = "PalData";
            this.PalData.Size = new System.Drawing.Size(884, 228);
            this.PalData.TabIndex = 0;
            // 
            // lbcancel
            // 
            this.lbcancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.lbcancel.BackgroundStyle.Class = "";
            this.lbcancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbcancel.ForeColor = System.Drawing.Color.Blue;
            this.lbcancel.Location = new System.Drawing.Point(769, 120);
            this.lbcancel.Name = "lbcancel";
            this.lbcancel.Size = new System.Drawing.Size(75, 29);
            this.lbcancel.TabIndex = 4;
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
            this.lbok.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbok.ForeColor = System.Drawing.Color.Blue;
            this.lbok.Location = new System.Drawing.Point(768, 45);
            this.lbok.Name = "lbok";
            this.lbok.Size = new System.Drawing.Size(75, 30);
            this.lbok.TabIndex = 3;
            this.lbok.Text = "OK";
            this.lbok.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lbok.Click += new System.EventHandler(this.lbok_Click);
            this.lbok.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbok_MouseDown);
            this.lbok.MouseEnter += new System.EventHandler(this.lbok_MouseEnter);
            this.lbok.MouseLeave += new System.EventHandler(this.lbok_MouseLeave);
            // 
            // txt_reasondesc2
            // 
            this.txt_reasondesc2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_reasondesc2.Location = new System.Drawing.Point(119, 183);
            this.txt_reasondesc2.Name = "txt_reasondesc2";
            this.txt_reasondesc2.Size = new System.Drawing.Size(575, 21);
            this.txt_reasondesc2.TabIndex = 1;
            // 
            // txt_reasondesc
            // 
            this.txt_reasondesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_reasondesc.Location = new System.Drawing.Point(119, 144);
            this.txt_reasondesc.Name = "txt_reasondesc";
            this.txt_reasondesc.Size = new System.Drawing.Size(575, 21);
            this.txt_reasondesc.TabIndex = 1;
            this.txt_reasondesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_reasondesc_KeyDown);
            // 
            // txt_dutystation
            // 
            this.txt_dutystation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_dutystation.Location = new System.Drawing.Point(119, 105);
            this.txt_dutystation.Name = "txt_dutystation";
            this.txt_dutystation.Size = new System.Drawing.Size(220, 21);
            this.txt_dutystation.TabIndex = 1;
            this.txt_dutystation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_dutystation_KeyDown);
            // 
            // txt_ReasonCode
            // 
            this.txt_ReasonCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_ReasonCode.Location = new System.Drawing.Point(119, 32);
            this.txt_ReasonCode.Name = "txt_ReasonCode";
            this.txt_ReasonCode.Size = new System.Drawing.Size(220, 21);
            this.txt_ReasonCode.TabIndex = 1;
            this.txt_ReasonCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_ReasonCode_KeyDown);
            this.txt_ReasonCode.Leave += new System.EventHandler(this.tb_ReasonCode_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(34, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "中文描述:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(34, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "责任单位:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(34, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "原因类型:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(34, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "英文描述:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(34, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "原因代码:";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.btReasonCode_Delete);
            this.panelEx3.Controls.Add(this.btReasonCode_Modify);
            this.panelEx3.Controls.Add(this.btReasonCode_Add);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx3.Location = new System.Drawing.Point(0, 124);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(884, 93);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 3;
            // 
            // btReasonCode_Delete
            // 
            this.btReasonCode_Delete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btReasonCode_Delete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btReasonCode_Delete.Location = new System.Drawing.Point(523, 24);
            this.btReasonCode_Delete.Name = "btReasonCode_Delete";
            this.btReasonCode_Delete.Size = new System.Drawing.Size(75, 45);
            this.btReasonCode_Delete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btReasonCode_Delete.TabIndex = 2;
            this.btReasonCode_Delete.Text = "删除";
            this.btReasonCode_Delete.Click += new System.EventHandler(this.btReasonCode_Delete_Click);
            // 
            // btReasonCode_Modify
            // 
            this.btReasonCode_Modify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btReasonCode_Modify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btReasonCode_Modify.Location = new System.Drawing.Point(345, 24);
            this.btReasonCode_Modify.Name = "btReasonCode_Modify";
            this.btReasonCode_Modify.Size = new System.Drawing.Size(75, 45);
            this.btReasonCode_Modify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btReasonCode_Modify.TabIndex = 1;
            this.btReasonCode_Modify.Text = "修改";
            this.btReasonCode_Modify.Click += new System.EventHandler(this.btReasonCode_Modify_Click);
            // 
            // btReasonCode_Add
            // 
            this.btReasonCode_Add.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btReasonCode_Add.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btReasonCode_Add.Location = new System.Drawing.Point(184, 23);
            this.btReasonCode_Add.Name = "btReasonCode_Add";
            this.btReasonCode_Add.Size = new System.Drawing.Size(75, 45);
            this.btReasonCode_Add.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btReasonCode_Add.TabIndex = 0;
            this.btReasonCode_Add.Text = "新增";
            this.btReasonCode_Add.Click += new System.EventHandler(this.btReasonCode_Add_Click);
            // 
            // dgvReason
            // 
            this.dgvReason.AllowUserToAddRows = false;
            this.dgvReason.AllowUserToDeleteRows = false;
            this.dgvReason.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReason.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dgvReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReason.Location = new System.Drawing.Point(0, 53);
            this.dgvReason.Name = "dgvReason";
            this.dgvReason.ReadOnly = true;
            this.dgvReason.RowTemplate.Height = 23;
            this.dgvReason.Size = new System.Drawing.Size(884, 71);
            this.dgvReason.TabIndex = 1;
            this.dgvReason.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvReason_CellMouseClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ReasonCode";
            this.Column1.HeaderText = "原因代码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 130;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ReasonDesc";
            this.Column2.HeaderText = "英文描述";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "ReasonDesc2";
            this.Column3.HeaderText = "中文描述";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 200;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "reasontype";
            this.Column4.HeaderText = "原因类型";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 120;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "dutystation";
            this.Column5.HeaderText = "责任单位";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "recdate";
            this.Column6.HeaderText = "时间";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // txt_reasontype
            // 
            this.txt_reasontype.FormattingEnabled = true;
            this.txt_reasontype.Location = new System.Drawing.Point(119, 70);
            this.txt_reasontype.Name = "txt_reasontype";
            this.txt_reasontype.Size = new System.Drawing.Size(220, 20);
            this.txt_reasontype.TabIndex = 5;
            // 
            // FrmReasonCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 445);
            this.Controls.Add(this.dgvReason);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmReasonCode";
            this.Text = "原因代码";
            this.Load += new System.EventHandler(this.FrmReasonCode_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.PalData.ResumeLayout(false);
            this.PalData.PerformLayout();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReason)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.TextBox tb_QryReason;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ButtonX btReasonCode_Add;
        private DevComponents.DotNetBar.ButtonX btReasonCode_Modify;
        private DevComponents.DotNetBar.ButtonX btReasonCode_Delete;
        private System.Windows.Forms.Panel PalData;
        private System.Windows.Forms.TextBox txt_reasondesc2;
        private System.Windows.Forms.TextBox txt_reasondesc;
        private System.Windows.Forms.TextBox txt_ReasonCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvReason;
        private DevComponents.DotNetBar.LabelX lbcancel;
        private DevComponents.DotNetBar.LabelX lbok;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_dutystation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.ComboBox txt_reasontype;
    }
}