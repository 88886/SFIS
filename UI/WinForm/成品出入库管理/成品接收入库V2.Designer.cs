namespace SFIS_V2
{
    partial class Frm_Receiving_Storage
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
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.tb_storenumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtstore = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtlocid = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btlocid = new DevComponents.DotNetBar.ButtonX();
            this.dgvstockIn = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvbackflush = new System.Windows.Forms.DataGridView();
            this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvstockIn)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbackflush)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dgvstockIn);
            this.panelEx1.Controls.Add(this.groupBox1);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(941, 394);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.btnSubmit);
            this.panelEx3.Controls.Add(this.btlocid);
            this.panelEx3.Controls.Add(this.label2);
            this.panelEx3.Controls.Add(this.txtstore);
            this.panelEx3.Controls.Add(this.txtlocid);
            this.panelEx3.Controls.Add(this.label1);
            this.panelEx3.Controls.Add(this.tb_storenumber);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(504, 228);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 0;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.rtbmsg);
            this.panelEx2.Controls.Add(this.panelEx3);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(941, 228);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // rtbmsg
            // 
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbmsg.Location = new System.Drawing.Point(504, 0);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.ReadOnly = true;
            this.rtbmsg.Size = new System.Drawing.Size(437, 228);
            this.rtbmsg.TabIndex = 1;
            this.rtbmsg.Text = "";
            // 
            // tb_storenumber
            // 
            this.tb_storenumber.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_storenumber.Location = new System.Drawing.Point(55, 37);
            this.tb_storenumber.Name = "tb_storenumber";
            this.tb_storenumber.Size = new System.Drawing.Size(180, 26);
            this.tb_storenumber.TabIndex = 0;
            this.tb_storenumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_storenumber_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(53, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "[入库单号]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(55, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 44;
            this.label2.Text = "仓库/库位:";
            // 
            // txtstore
            // 
            // 
            // 
            // 
            this.txtstore.Border.Class = "TextBoxBorder";
            this.txtstore.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtstore.Enabled = false;
            this.txtstore.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtstore.Location = new System.Drawing.Point(53, 97);
            this.txtstore.Name = "txtstore";
            this.txtstore.Size = new System.Drawing.Size(82, 26);
            this.txtstore.TabIndex = 45;
            this.txtstore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtlocid
            // 
            this.txtlocid.AcceptsTab = true;
            // 
            // 
            // 
            this.txtlocid.Border.Class = "TextBoxBorder";
            this.txtlocid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtlocid.Enabled = false;
            this.txtlocid.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtlocid.Location = new System.Drawing.Point(152, 97);
            this.txtlocid.Name = "txtlocid";
            this.txtlocid.Size = new System.Drawing.Size(80, 26);
            this.txtlocid.TabIndex = 46;
            this.txtlocid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btlocid
            // 
            this.btlocid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btlocid.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btlocid.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btlocid.Location = new System.Drawing.Point(243, 98);
            this.btlocid.Name = "btlocid";
            this.btlocid.Size = new System.Drawing.Size(86, 25);
            this.btlocid.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btlocid.TabIndex = 47;
            this.btlocid.Text = "储位选择";
            this.btlocid.Click += new System.EventHandler(this.btlocid_Click);
            // 
            // dgvstockIn
            // 
            this.dgvstockIn.AllowUserToAddRows = false;
            this.dgvstockIn.AllowUserToDeleteRows = false;
            this.dgvstockIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvstockIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvstockIn.Location = new System.Drawing.Point(0, 228);
            this.dgvstockIn.Name = "dgvstockIn";
            this.dgvstockIn.ReadOnly = true;
            this.dgvstockIn.RowTemplate.Height = 23;
            this.dgvstockIn.Size = new System.Drawing.Size(941, 66);
            this.dgvstockIn.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvbackflush);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 294);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(941, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SAP上传数据";
            // 
            // dgvbackflush
            // 
            this.dgvbackflush.AllowUserToAddRows = false;
            this.dgvbackflush.AllowUserToDeleteRows = false;
            this.dgvbackflush.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvbackflush.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvbackflush.Location = new System.Drawing.Point(3, 17);
            this.dgvbackflush.Name = "dgvbackflush";
            this.dgvbackflush.ReadOnly = true;
            this.dgvbackflush.RowTemplate.Height = 23;
            this.dgvbackflush.Size = new System.Drawing.Size(935, 80);
            this.dgvbackflush.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubmit.AllowDrop = true;
            this.btnSubmit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSubmit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSubmit.Location = new System.Drawing.Point(399, 98);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(69, 26);
            this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "提交接收";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // Frm_Receiving_Storage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 394);
            this.Controls.Add(this.panelEx1);
            this.Name = "Frm_Receiving_Storage";
            this.Text = "成品接收入库";
            this.Load += new System.EventHandler(this.Frm_Receiving_Storage_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvstockIn)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvbackflush)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.RichTextBox rtbmsg;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.TextBox tb_storenumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public DevComponents.DotNetBar.Controls.TextBoxX txtstore;
        public DevComponents.DotNetBar.Controls.TextBoxX txtlocid;
        private DevComponents.DotNetBar.ButtonX btlocid;
        private System.Windows.Forms.DataGridView dgvstockIn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvbackflush;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
    }
}