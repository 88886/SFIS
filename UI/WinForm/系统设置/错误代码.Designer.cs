namespace SFIS_V2
{
    partial class FrmErrorCode
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
            this.tb_Query = new System.Windows.Forms.TextBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.PalData = new System.Windows.Forms.Panel();
            this.lbcancel = new DevComponents.DotNetBar.LabelX();
            this.lbok = new DevComponents.DotNetBar.LabelX();
            this.txt_errordesc2 = new System.Windows.Forms.TextBox();
            this.txt_errordesc = new System.Windows.Forms.TextBox();
            this.txt_ErrorCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.btErrorCode_Delete = new DevComponents.DotNetBar.ButtonX();
            this.btErrorCode_Modify = new DevComponents.DotNetBar.ButtonX();
            this.btErrorCode_Add = new DevComponents.DotNetBar.ButtonX();
            this.dgverror = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.PalData.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgverror)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.tb_Query);
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
            // tb_Query
            // 
            this.tb_Query.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tb_Query.Location = new System.Drawing.Point(145, 14);
            this.tb_Query.Name = "tb_Query";
            this.tb_Query.Size = new System.Drawing.Size(237, 21);
            this.tb_Query.TabIndex = 1;
            this.tb_Query.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Query_KeyDown);
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
            this.labelX1.Text = "查询错误代码:";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.PalData);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 221);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(884, 180);
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
            this.PalData.Controls.Add(this.lbcancel);
            this.PalData.Controls.Add(this.lbok);
            this.PalData.Controls.Add(this.txt_errordesc2);
            this.PalData.Controls.Add(this.txt_errordesc);
            this.PalData.Controls.Add(this.txt_ErrorCode);
            this.PalData.Controls.Add(this.label3);
            this.PalData.Controls.Add(this.label2);
            this.PalData.Controls.Add(this.label1);
            this.PalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PalData.Location = new System.Drawing.Point(0, 0);
            this.PalData.Name = "PalData";
            this.PalData.Size = new System.Drawing.Size(884, 180);
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
            this.lbcancel.Location = new System.Drawing.Point(769, 106);
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
            // txt_errordesc2
            // 
            this.txt_errordesc2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_errordesc2.Location = new System.Drawing.Point(119, 116);
            this.txt_errordesc2.Name = "txt_errordesc2";
            this.txt_errordesc2.Size = new System.Drawing.Size(575, 21);
            this.txt_errordesc2.TabIndex = 1;
            this.txt_errordesc2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_errordesc2_KeyDown);
            // 
            // txt_errordesc
            // 
            this.txt_errordesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_errordesc.Location = new System.Drawing.Point(119, 77);
            this.txt_errordesc.Name = "txt_errordesc";
            this.txt_errordesc.Size = new System.Drawing.Size(575, 21);
            this.txt_errordesc.TabIndex = 1;
            this.txt_errordesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_errordesc_KeyDown);
            // 
            // txt_ErrorCode
            // 
            this.txt_ErrorCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_ErrorCode.Location = new System.Drawing.Point(119, 32);
            this.txt_ErrorCode.Name = "txt_ErrorCode";
            this.txt_ErrorCode.Size = new System.Drawing.Size(220, 21);
            this.txt_ErrorCode.TabIndex = 1;
            this.txt_ErrorCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_ErrorCode_KeyDown);
            this.txt_ErrorCode.Leave += new System.EventHandler(this.tb_ErrorCode_Leave);
            this.txt_ErrorCode.MouseLeave += new System.EventHandler(this.tb_ErrorCode_MouseLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(34, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "中文描述:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(34, 80);
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
            this.label1.Text = "错误代码:";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.btErrorCode_Delete);
            this.panelEx3.Controls.Add(this.btErrorCode_Modify);
            this.panelEx3.Controls.Add(this.btErrorCode_Add);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx3.Location = new System.Drawing.Point(0, 128);
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
            // btErrorCode_Delete
            // 
            this.btErrorCode_Delete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btErrorCode_Delete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btErrorCode_Delete.Location = new System.Drawing.Point(523, 28);
            this.btErrorCode_Delete.Name = "btErrorCode_Delete";
            this.btErrorCode_Delete.Size = new System.Drawing.Size(75, 45);
            this.btErrorCode_Delete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btErrorCode_Delete.TabIndex = 2;
            this.btErrorCode_Delete.Text = "删除";
            this.btErrorCode_Delete.Click += new System.EventHandler(this.btErrorCode_Delete_Click);
            // 
            // btErrorCode_Modify
            // 
            this.btErrorCode_Modify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btErrorCode_Modify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btErrorCode_Modify.Location = new System.Drawing.Point(345, 28);
            this.btErrorCode_Modify.Name = "btErrorCode_Modify";
            this.btErrorCode_Modify.Size = new System.Drawing.Size(75, 45);
            this.btErrorCode_Modify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btErrorCode_Modify.TabIndex = 1;
            this.btErrorCode_Modify.Text = "修改";
            this.btErrorCode_Modify.Click += new System.EventHandler(this.btErrorCode_Modify_Click);
            // 
            // btErrorCode_Add
            // 
            this.btErrorCode_Add.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btErrorCode_Add.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btErrorCode_Add.Location = new System.Drawing.Point(184, 27);
            this.btErrorCode_Add.Name = "btErrorCode_Add";
            this.btErrorCode_Add.Size = new System.Drawing.Size(75, 45);
            this.btErrorCode_Add.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btErrorCode_Add.TabIndex = 0;
            this.btErrorCode_Add.Text = "新增";
            this.btErrorCode_Add.Click += new System.EventHandler(this.btErrorCode_Add_Click);
            // 
            // dgverror
            // 
            this.dgverror.AllowUserToAddRows = false;
            this.dgverror.AllowUserToDeleteRows = false;
            this.dgverror.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgverror.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgverror.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgverror.Location = new System.Drawing.Point(0, 53);
            this.dgverror.Name = "dgverror";
            this.dgverror.ReadOnly = true;
            this.dgverror.RowTemplate.Height = 23;
            this.dgverror.Size = new System.Drawing.Size(884, 75);
            this.dgverror.TabIndex = 1;
            this.dgverror.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgverror_CellMouseClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ErrorCode";
            this.Column1.HeaderText = "错误代码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 130;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ErrorDesc";
            this.Column2.HeaderText = "英文描述";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "ErrorDesc2";
            this.Column3.HeaderText = "中文描述";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 200;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "recdate";
            this.Column4.HeaderText = "时间";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 120;
            // 
            // FrmErrorCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 401);
            this.Controls.Add(this.dgverror);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmErrorCode";
            this.Text = "错误代码";
            this.Load += new System.EventHandler(this.FrmErrorCode_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.PalData.ResumeLayout(false);
            this.PalData.PerformLayout();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgverror)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.TextBox tb_Query;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ButtonX btErrorCode_Add;
        private DevComponents.DotNetBar.ButtonX btErrorCode_Modify;
        private DevComponents.DotNetBar.ButtonX btErrorCode_Delete;
        private System.Windows.Forms.Panel PalData;
        private System.Windows.Forms.TextBox txt_errordesc2;
        private System.Windows.Forms.TextBox txt_errordesc;
        private System.Windows.Forms.TextBox txt_ErrorCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgverror;
        private DevComponents.DotNetBar.LabelX lbcancel;
        private DevComponents.DotNetBar.LabelX lbok;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}