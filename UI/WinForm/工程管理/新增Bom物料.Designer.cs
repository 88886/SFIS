namespace SFIS_V2
{
    partial class FrmKeyParts
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
            this.btdeleteparts = new DevComponents.DotNetBar.ButtonX();
            this.btaddparts = new DevComponents.DotNetBar.ButtonX();
            this.tbqrykpno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.PalData = new System.Windows.Forms.Panel();
            this.btexit = new DevComponents.DotNetBar.ButtonX();
            this.lbModify = new System.Windows.Forms.Label();
            this.txt_kpdesc = new System.Windows.Forms.TextBox();
            this.txt_kpname = new System.Windows.Forms.TextBox();
            this.tbPartNumber = new System.Windows.Forms.TextBox();
            this.txt_kpnumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.PalData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btdeleteparts);
            this.panelEx1.Controls.Add(this.btaddparts);
            this.panelEx1.Controls.Add(this.tbqrykpno);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(815, 100);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btdeleteparts
            // 
            this.btdeleteparts.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btdeleteparts.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btdeleteparts.Location = new System.Drawing.Point(513, 33);
            this.btdeleteparts.Name = "btdeleteparts";
            this.btdeleteparts.Size = new System.Drawing.Size(59, 41);
            this.btdeleteparts.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btdeleteparts.TabIndex = 3;
            this.btdeleteparts.Text = "删除";
            this.btdeleteparts.Click += new System.EventHandler(this.btdeleteparts_Click);
            // 
            // btaddparts
            // 
            this.btaddparts.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btaddparts.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btaddparts.Location = new System.Drawing.Point(383, 33);
            this.btaddparts.Name = "btaddparts";
            this.btaddparts.Size = new System.Drawing.Size(59, 41);
            this.btaddparts.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btaddparts.TabIndex = 2;
            this.btaddparts.Text = "新增";
            this.btaddparts.Click += new System.EventHandler(this.btaddparts_Click);
            // 
            // tbqrykpno
            // 
            this.tbqrykpno.BackColor = System.Drawing.Color.Aquamarine;
            this.tbqrykpno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbqrykpno.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbqrykpno.Location = new System.Drawing.Point(20, 48);
            this.tbqrykpno.Name = "tbqrykpno";
            this.tbqrykpno.Size = new System.Drawing.Size(195, 26);
            this.tbqrykpno.TabIndex = 1;
            this.tbqrykpno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbqrykpno_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "〖成品料号〗";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.PalData);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 299);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(815, 178);
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
            this.PalData.Controls.Add(this.panel1);
            this.PalData.Controls.Add(this.tbPartNumber);
            this.PalData.Controls.Add(this.label5);
            this.PalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PalData.Location = new System.Drawing.Point(0, 0);
            this.PalData.Name = "PalData";
            this.PalData.Size = new System.Drawing.Size(815, 178);
            this.PalData.TabIndex = 0;
            this.PalData.Visible = false;
            // 
            // btexit
            // 
            this.btexit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btexit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btexit.Location = new System.Drawing.Point(682, 11);
            this.btexit.Name = "btexit";
            this.btexit.Size = new System.Drawing.Size(49, 23);
            this.btexit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btexit.TabIndex = 7;
            this.btexit.Text = "退出";
            this.btexit.Click += new System.EventHandler(this.btexit_Click);
            // 
            // lbModify
            // 
            this.lbModify.AutoSize = true;
            this.lbModify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lbModify.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbModify.Location = new System.Drawing.Point(679, 70);
            this.lbModify.Name = "lbModify";
            this.lbModify.Size = new System.Drawing.Size(72, 16);
            this.lbModify.TabIndex = 6;
            this.lbModify.Text = "修改保存";
            this.lbModify.Visible = false;
            this.lbModify.Click += new System.EventHandler(this.lbModify_Click);
            this.lbModify.MouseEnter += new System.EventHandler(this.lbModify_MouseEnter);
            this.lbModify.MouseLeave += new System.EventHandler(this.lbModify_MouseLeave);
            // 
            // txt_kpdesc
            // 
            this.txt_kpdesc.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_kpdesc.Location = new System.Drawing.Point(84, 65);
            this.txt_kpdesc.Name = "txt_kpdesc";
            this.txt_kpdesc.Size = new System.Drawing.Size(587, 26);
            this.txt_kpdesc.TabIndex = 5;
            this.txt_kpdesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbkpdesc_KeyDown);
            // 
            // txt_kpname
            // 
            this.txt_kpname.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_kpname.Location = new System.Drawing.Point(429, 11);
            this.txt_kpname.Name = "txt_kpname";
            this.txt_kpname.Size = new System.Drawing.Size(242, 26);
            this.txt_kpname.TabIndex = 4;
            this.txt_kpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbkpname_KeyDown);
            // 
            // tbPartNumber
            // 
            this.tbPartNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbPartNumber.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPartNumber.Location = new System.Drawing.Point(93, 16);
            this.tbPartNumber.Name = "tbPartNumber";
            this.tbPartNumber.Size = new System.Drawing.Size(235, 26);
            this.tbPartNumber.TabIndex = 3;
            this.tbPartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbkpnumber_KeyDown);
            this.tbPartNumber.Leave += new System.EventHandler(this.tbkpnumber_Leave);
            this.tbPartNumber.MouseLeave += new System.EventHandler(this.tbkpnumber_MouseLeave);
            // 
            // txt_kpnumber
            // 
            this.txt_kpnumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_kpnumber.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_kpnumber.Location = new System.Drawing.Point(84, 11);
            this.txt_kpnumber.Name = "txt_kpnumber";
            this.txt_kpnumber.Size = new System.Drawing.Size(235, 26);
            this.txt_kpnumber.TabIndex = 3;
            this.txt_kpnumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbkpnumber_KeyDown);
            this.txt_kpnumber.Leave += new System.EventHandler(this.tbkpnumber_Leave);
            this.txt_kpnumber.MouseLeave += new System.EventHandler(this.tbkpnumber_MouseLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(2, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "物料描述:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(343, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "物料名称:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(10, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "成品料号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(1, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "物料料号:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 100);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(815, 199);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "kpnumber";
            this.Column1.HeaderText = "物料料号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "PartNumber";
            this.Column4.HeaderText = "成品料号";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "kpname";
            this.Column2.HeaderText = "物料名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "kpdesc";
            this.Column3.HeaderText = "物料描述";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 300;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btexit);
            this.panel1.Controls.Add(this.lbModify);
            this.panel1.Controls.Add(this.txt_kpdesc);
            this.panel1.Controls.Add(this.txt_kpname);
            this.panel1.Controls.Add(this.txt_kpnumber);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(7, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 111);
            this.panel1.TabIndex = 8;
            // 
            // FrmKeyParts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 477);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmKeyParts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增Bom物料";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmKeyParts_FormClosed);
            this.Load += new System.EventHandler(this.FrmKeyParts_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.PalData.ResumeLayout(false);
            this.PalData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox tbqrykpno;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX btdeleteparts;
        private DevComponents.DotNetBar.ButtonX btaddparts;
        private System.Windows.Forms.Panel PalData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_kpdesc;
        private System.Windows.Forms.TextBox txt_kpname;
        private System.Windows.Forms.TextBox txt_kpnumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbModify;
        private DevComponents.DotNetBar.ButtonX btexit;
        private System.Windows.Forms.TextBox tbPartNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Panel panel1;
    }
}