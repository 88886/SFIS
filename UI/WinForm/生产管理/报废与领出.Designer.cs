namespace SFIS_V2
{
    partial class FrmScrap
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ESN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PARTNUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.woId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.原因 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.库位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rbsn = new System.Windows.Forms.RadioButton();
            this.rbimei = new System.Windows.Forms.RadioButton();
            this.rbmac = new System.Windows.Forms.RadioButton();
            this.rbesn = new System.Windows.Forms.RadioButton();
            this.lab_wo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.tbmemo = new System.Windows.Forms.TextBox();
            this.tbesn = new System.Windows.Forms.TextBox();
            this.cbreason = new System.Windows.Forms.ComboBox();
            this.cb_loc = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbgroup = new System.Windows.Forms.TextBox();
            this.rdtest = new System.Windows.Forms.RadioButton();
            this.rdsmt = new System.Windows.Forms.RadioButton();
            this.panelEx1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.groupPanel2);
            this.panelEx1.Controls.Add(this.groupPanel1);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1077, 583);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.dataGridView1);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel2.Location = new System.Drawing.Point(485, 0);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(592, 583);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 1;
            this.groupPanel2.Text = "信息显示";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ESN,
            this.PARTNUMBER,
            this.woId,
            this.原因,
            this.库位});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(586, 559);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            // 
            // ESN
            // 
            this.ESN.HeaderText = "ESN";
            this.ESN.Name = "ESN";
            this.ESN.ReadOnly = true;
            // 
            // PARTNUMBER
            // 
            this.PARTNUMBER.HeaderText = "PARTNUMBER";
            this.PARTNUMBER.Name = "PARTNUMBER";
            this.PARTNUMBER.ReadOnly = true;
            // 
            // woId
            // 
            this.woId.HeaderText = "woId";
            this.woId.Name = "woId";
            this.woId.ReadOnly = true;
            // 
            // 原因
            // 
            this.原因.HeaderText = "原因";
            this.原因.Name = "原因";
            this.原因.ReadOnly = true;
            // 
            // 库位
            // 
            this.库位.HeaderText = "库位";
            this.库位.Name = "库位";
            this.库位.ReadOnly = true;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.rbsn);
            this.groupPanel1.Controls.Add(this.rbimei);
            this.groupPanel1.Controls.Add(this.rbmac);
            this.groupPanel1.Controls.Add(this.rbesn);
            this.groupPanel1.Controls.Add(this.lab_wo);
            this.groupPanel1.Controls.Add(this.label7);
            this.groupPanel1.Controls.Add(this.label6);
            this.groupPanel1.Controls.Add(this.rtbmsg);
            this.groupPanel1.Controls.Add(this.tbmemo);
            this.groupPanel1.Controls.Add(this.tbesn);
            this.groupPanel1.Controls.Add(this.cbreason);
            this.groupPanel1.Controls.Add(this.cb_loc);
            this.groupPanel1.Controls.Add(this.label3);
            this.groupPanel1.Controls.Add(this.label5);
            this.groupPanel1.Controls.Add(this.label4);
            this.groupPanel1.Controls.Add(this.label2);
            this.groupPanel1.Controls.Add(this.label1);
            this.groupPanel1.Controls.Add(this.tbgroup);
            this.groupPanel1.Controls.Add(this.rdtest);
            this.groupPanel1.Controls.Add(this.rdsmt);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(485, 583);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "产品报废与领出基础信息";
            // 
            // rbsn
            // 
            this.rbsn.AutoSize = true;
            this.rbsn.Location = new System.Drawing.Point(181, 441);
            this.rbsn.Name = "rbsn";
            this.rbsn.Size = new System.Drawing.Size(35, 16);
            this.rbsn.TabIndex = 15;
            this.rbsn.Text = "SN";
            this.rbsn.UseVisualStyleBackColor = true;
            // 
            // rbimei
            // 
            this.rbimei.AutoSize = true;
            this.rbimei.Location = new System.Drawing.Point(128, 441);
            this.rbimei.Name = "rbimei";
            this.rbimei.Size = new System.Drawing.Size(47, 16);
            this.rbimei.TabIndex = 14;
            this.rbimei.Text = "IMEI";
            this.rbimei.UseVisualStyleBackColor = true;
            // 
            // rbmac
            // 
            this.rbmac.AutoSize = true;
            this.rbmac.Location = new System.Drawing.Point(75, 441);
            this.rbmac.Name = "rbmac";
            this.rbmac.Size = new System.Drawing.Size(41, 16);
            this.rbmac.TabIndex = 13;
            this.rbmac.Text = "MAC";
            this.rbmac.UseVisualStyleBackColor = true;
            // 
            // rbesn
            // 
            this.rbesn.AutoSize = true;
            this.rbesn.Checked = true;
            this.rbesn.Location = new System.Drawing.Point(28, 441);
            this.rbesn.Name = "rbesn";
            this.rbesn.Size = new System.Drawing.Size(41, 16);
            this.rbesn.TabIndex = 12;
            this.rbesn.TabStop = true;
            this.rbesn.Text = "ESN";
            this.rbesn.UseVisualStyleBackColor = true;
            // 
            // lab_wo
            // 
            this.lab_wo.AutoSize = true;
            this.lab_wo.Location = new System.Drawing.Point(125, 374);
            this.lab_wo.Name = "lab_wo";
            this.lab_wo.Size = new System.Drawing.Size(89, 12);
            this.lab_wo.TabIndex = 11;
            this.lab_wo.Text = "在这里输入工单";
            this.lab_wo.Click += new System.EventHandler(this.lab_wo_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(226, 252);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "label6";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(226, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "label6";
            // 
            // rtbmsg
            // 
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbmsg.Location = new System.Drawing.Point(0, 463);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.ReadOnly = true;
            this.rtbmsg.Size = new System.Drawing.Size(479, 96);
            this.rtbmsg.TabIndex = 9;
            this.rtbmsg.Text = "";
            // 
            // tbmemo
            // 
            this.tbmemo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbmemo.Location = new System.Drawing.Point(28, 318);
            this.tbmemo.Name = "tbmemo";
            this.tbmemo.Size = new System.Drawing.Size(403, 26);
            this.tbmemo.TabIndex = 8;
            // 
            // tbesn
            // 
            this.tbesn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbesn.Location = new System.Drawing.Point(28, 401);
            this.tbesn.Name = "tbesn";
            this.tbesn.Size = new System.Drawing.Size(319, 26);
            this.tbesn.TabIndex = 7;
            this.tbesn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbesn_KeyDown);
            // 
            // cbreason
            // 
            this.cbreason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbreason.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbreason.FormattingEnabled = true;
            this.cbreason.Location = new System.Drawing.Point(28, 249);
            this.cbreason.Name = "cbreason";
            this.cbreason.Size = new System.Drawing.Size(192, 24);
            this.cbreason.TabIndex = 6;
            this.cbreason.SelectedIndexChanged += new System.EventHandler(this.cbreason_SelectedIndexChanged);
            this.cbreason.TextChanged += new System.EventHandler(this.cbreason_TextChanged);
            this.cbreason.Validating += new System.ComponentModel.CancelEventHandler(this.cbreason_Validating);
            // 
            // cb_loc
            // 
            this.cb_loc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_loc.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_loc.FormattingEnabled = true;
            this.cb_loc.Location = new System.Drawing.Point(28, 181);
            this.cb_loc.Name = "cb_loc";
            this.cb_loc.Size = new System.Drawing.Size(192, 24);
            this.cb_loc.TabIndex = 6;
            this.cb_loc.SelectedIndexChanged += new System.EventHandler(this.cb_loc_SelectedIndexChanged);
            this.cb_loc.TextChanged += new System.EventHandler(this.cb_loc_TextChanged);
            this.cb_loc.Validated += new System.EventHandler(this.cb_loc_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(25, 373);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "[刷入条码]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(25, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "[备注]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(25, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "[原因]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(25, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "[库位]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "[当前途程]";
            // 
            // tbgroup
            // 
            this.tbgroup.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbgroup.Location = new System.Drawing.Point(28, 114);
            this.tbgroup.Name = "tbgroup";
            this.tbgroup.ReadOnly = true;
            this.tbgroup.Size = new System.Drawing.Size(192, 26);
            this.tbgroup.TabIndex = 2;
            this.tbgroup.Text = "SC_SMT";
            // 
            // rdtest
            // 
            this.rdtest.AutoSize = true;
            this.rdtest.Location = new System.Drawing.Point(128, 31);
            this.rdtest.Name = "rdtest";
            this.rdtest.Size = new System.Drawing.Size(59, 16);
            this.rdtest.TabIndex = 1;
            this.rdtest.Text = "组测包";
            this.rdtest.UseVisualStyleBackColor = true;
            this.rdtest.CheckedChanged += new System.EventHandler(this.rdtest_CheckedChanged);
            // 
            // rdsmt
            // 
            this.rdsmt.AutoSize = true;
            this.rdsmt.Location = new System.Drawing.Point(28, 31);
            this.rdsmt.Name = "rdsmt";
            this.rdsmt.Size = new System.Drawing.Size(41, 16);
            this.rdsmt.TabIndex = 0;
            this.rdsmt.Text = "SMT";
            this.rdsmt.UseVisualStyleBackColor = true;
            this.rdsmt.CheckedChanged += new System.EventHandler(this.rdsmt_CheckedChanged);
            // 
            // FrmScrap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 583);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.Name = "FrmScrap";
            this.Text = "报废与领出";
            this.Load += new System.EventHandler(this.FrmScrap_Load);
            this.panelEx1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.TextBox tbgroup;
        private System.Windows.Forms.RadioButton rdtest;
        private System.Windows.Forms.RadioButton rdsmt;
        private System.Windows.Forms.ComboBox cb_loc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private System.Windows.Forms.TextBox tbesn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbreason;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbmemo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtbmsg;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lab_wo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESN;
        private System.Windows.Forms.DataGridViewTextBoxColumn PARTNUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn woId;
        private System.Windows.Forms.DataGridViewTextBoxColumn 原因;
        private System.Windows.Forms.DataGridViewTextBoxColumn 库位;
        private System.Windows.Forms.RadioButton rbsn;
        private System.Windows.Forms.RadioButton rbimei;
        private System.Windows.Forms.RadioButton rbmac;
        private System.Windows.Forms.RadioButton rbesn;
    }
}