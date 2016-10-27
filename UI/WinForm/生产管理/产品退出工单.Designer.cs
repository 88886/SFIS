namespace SFIS_V2
{
    partial class Frm_OutOrder
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
            this.btncommit = new DevComponents.DotNetBar.ButtonX();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Labwo = new DevComponents.DotNetBar.LabelX();
            this.labproduct = new DevComponents.DotNetBar.LabelX();
            this.labpartnumber = new DevComponents.DotNetBar.LabelX();
            this.tb_inputdata = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvsnList = new System.Windows.Forms.DataGridView();
            this.ESN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PARTNUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCTNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WIPSTATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsnList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btncommit);
            this.panelEx1.Controls.Add(this.rtbmsg);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.Labwo);
            this.panelEx1.Controls.Add(this.labproduct);
            this.panelEx1.Controls.Add(this.labpartnumber);
            this.panelEx1.Controls.Add(this.tb_inputdata);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(275, 512);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btncommit
            // 
            this.btncommit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btncommit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btncommit.Location = new System.Drawing.Point(134, 263);
            this.btncommit.Name = "btncommit";
            this.btncommit.Size = new System.Drawing.Size(75, 23);
            this.btncommit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btncommit.TabIndex = 7;
            this.btncommit.Text = "提交";
            this.btncommit.Click += new System.EventHandler(this.btncommit_Click);
            // 
            // rtbmsg
            // 
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbmsg.Location = new System.Drawing.Point(0, 330);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.Size = new System.Drawing.Size(275, 182);
            this.rtbmsg.TabIndex = 6;
            this.rtbmsg.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "刷入";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "型号:";
            this.label3.Click += new System.EventHandler(this.label2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "料号:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Labwo
            // 
            // 
            // 
            // 
            this.Labwo.BackgroundStyle.Class = "";
            this.Labwo.Location = new System.Drawing.Point(79, 38);
            this.Labwo.Name = "Labwo";
            this.Labwo.Size = new System.Drawing.Size(75, 23);
            this.Labwo.TabIndex = 3;
            this.Labwo.Text = "labelX2";
            // 
            // labproduct
            // 
            // 
            // 
            // 
            this.labproduct.BackgroundStyle.Class = "";
            this.labproduct.Location = new System.Drawing.Point(79, 124);
            this.labproduct.Name = "labproduct";
            this.labproduct.Size = new System.Drawing.Size(75, 23);
            this.labproduct.TabIndex = 3;
            this.labproduct.Text = "labelX2";
            // 
            // labpartnumber
            // 
            // 
            // 
            // 
            this.labpartnumber.BackgroundStyle.Class = "";
            this.labpartnumber.Location = new System.Drawing.Point(79, 85);
            this.labpartnumber.Name = "labpartnumber";
            this.labpartnumber.Size = new System.Drawing.Size(75, 23);
            this.labpartnumber.TabIndex = 2;
            this.labpartnumber.Text = "labelX1";
            // 
            // tb_inputdata
            // 
            this.tb_inputdata.Location = new System.Drawing.Point(35, 211);
            this.tb_inputdata.Name = "tb_inputdata";
            this.tb_inputdata.Size = new System.Drawing.Size(174, 21);
            this.tb_inputdata.TabIndex = 1;
            this.tb_inputdata.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_inputdata_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "工单:";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.groupBox1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(275, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(562, 512);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvsnList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(562, 512);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "显示信息";
            // 
            // dgvsnList
            // 
            this.dgvsnList.AllowUserToAddRows = false;
            this.dgvsnList.AllowUserToDeleteRows = false;
            this.dgvsnList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvsnList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ESN,
            this.WOID,
            this.PARTNUMBER,
            this.PRODUCTNAME,
            this.WIPSTATION});
            this.dgvsnList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvsnList.Location = new System.Drawing.Point(3, 17);
            this.dgvsnList.Name = "dgvsnList";
            this.dgvsnList.ReadOnly = true;
            this.dgvsnList.RowTemplate.Height = 23;
            this.dgvsnList.Size = new System.Drawing.Size(556, 492);
            this.dgvsnList.TabIndex = 0;
            // 
            // ESN
            // 
            this.ESN.HeaderText = "ESN";
            this.ESN.Name = "ESN";
            this.ESN.ReadOnly = true;
            // 
            // WOID
            // 
            this.WOID.HeaderText = "WOID";
            this.WOID.Name = "WOID";
            this.WOID.ReadOnly = true;
            // 
            // PARTNUMBER
            // 
            this.PARTNUMBER.HeaderText = "PARTNUMBER";
            this.PARTNUMBER.Name = "PARTNUMBER";
            this.PARTNUMBER.ReadOnly = true;
            // 
            // PRODUCTNAME
            // 
            this.PRODUCTNAME.HeaderText = "PRODUCTNAME";
            this.PRODUCTNAME.Name = "PRODUCTNAME";
            this.PRODUCTNAME.ReadOnly = true;
            // 
            // WIPSTATION
            // 
            this.WIPSTATION.HeaderText = "WIPSTATION";
            this.WIPSTATION.Name = "WIPSTATION";
            this.WIPSTATION.ReadOnly = true;
            // 
            // Frm_OutOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 512);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "Frm_OutOrder";
            this.Text = "产品退出工单";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_OutOrder_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvsnList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.LabelX labproduct;
        private DevComponents.DotNetBar.LabelX labpartnumber;
        private System.Windows.Forms.TextBox tb_inputdata;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbmsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvsnList;
        private DevComponents.DotNetBar.LabelX Labwo;
        private DevComponents.DotNetBar.ButtonX btncommit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESN;
        private System.Windows.Forms.DataGridViewTextBoxColumn WOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PARTNUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCTNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn WIPSTATION;
    }
}