namespace SFIS_V2
{
    partial class Frm_StockReceive
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
            this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
            this.label2 = new System.Windows.Forms.Label();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.cmbstatus = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblstatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbtype = new System.Windows.Forms.GroupBox();
            this.gbrdstock0 = new System.Windows.Forms.RadioButton();
            this.gbrdstock3 = new System.Windows.Forms.RadioButton();
            this.gbrdstock2 = new System.Windows.Forms.RadioButton();
            this.gbrdstock1 = new System.Windows.Forms.RadioButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tb_selectedcount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_totalcount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtstore = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtlocid = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btlocid = new DevComponents.DotNetBar.ButtonX();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.dgvstockIn = new System.Windows.Forms.DataGridView();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.bt_refresh = new DevComponents.DotNetBar.ButtonX();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.tbboxnumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dgv_showcarton = new System.Windows.Forms.DataGridView();
            this.panelEx3.SuspendLayout();
            this.gbtype.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvstockIn)).BeginInit();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showcarton)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubmit.AllowDrop = true;
            this.btnSubmit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSubmit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSubmit.Location = new System.Drawing.Point(790, 3);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(69, 26);
            this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "提交接收";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "条码号码:";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.cmbstatus);
            this.panelEx3.Controls.Add(this.lblstatus);
            this.panelEx3.Controls.Add(this.label3);
            this.panelEx3.Controls.Add(this.gbtype);
            this.panelEx3.Controls.Add(this.splitContainer1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(859, 150);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 14;
            // 
            // cmbstatus
            // 
            this.cmbstatus.DisplayMember = "Text";
            this.cmbstatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbstatus.FormattingEnabled = true;
            this.cmbstatus.ItemHeight = 15;
            this.cmbstatus.Location = new System.Drawing.Point(99, 16);
            this.cmbstatus.Name = "cmbstatus";
            this.cmbstatus.Size = new System.Drawing.Size(139, 21);
            this.cmbstatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbstatus.TabIndex = 47;
            this.cmbstatus.SelectedIndexChanged += new System.EventHandler(this.cmbstatus_SelectedIndexChanged);
            // 
            // lblstatus
            // 
            this.lblstatus.AutoSize = true;
            this.lblstatus.Location = new System.Drawing.Point(31, 20);
            this.lblstatus.Name = "lblstatus";
            this.lblstatus.Size = new System.Drawing.Size(59, 12);
            this.lblstatus.TabIndex = 6;
            this.lblstatus.Text = "入库类型:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "入库方式:";
            // 
            // gbtype
            // 
            this.gbtype.Controls.Add(this.gbrdstock0);
            this.gbtype.Controls.Add(this.gbrdstock3);
            this.gbtype.Controls.Add(this.gbrdstock2);
            this.gbtype.Controls.Add(this.gbrdstock1);
            this.gbtype.Location = new System.Drawing.Point(99, 43);
            this.gbtype.Name = "gbtype";
            this.gbtype.Size = new System.Drawing.Size(429, 40);
            this.gbtype.TabIndex = 4;
            this.gbtype.TabStop = false;
            // 
            // gbrdstock0
            // 
            this.gbrdstock0.AutoSize = true;
            this.gbrdstock0.Checked = true;
            this.gbrdstock0.Location = new System.Drawing.Point(11, 17);
            this.gbrdstock0.Name = "gbrdstock0";
            this.gbrdstock0.Size = new System.Drawing.Size(83, 16);
            this.gbrdstock0.TabIndex = 0;
            this.gbrdstock0.TabStop = true;
            this.gbrdstock0.Text = "以批次入库";
            this.gbrdstock0.UseVisualStyleBackColor = true;
            this.gbrdstock0.CheckedChanged += new System.EventHandler(this.gbrdstock0_CheckedChanged);
            // 
            // gbrdstock3
            // 
            this.gbrdstock3.AutoSize = true;
            this.gbrdstock3.Location = new System.Drawing.Point(330, 17);
            this.gbrdstock3.Name = "gbrdstock3";
            this.gbrdstock3.Size = new System.Drawing.Size(83, 16);
            this.gbrdstock3.TabIndex = 3;
            this.gbrdstock3.TabStop = true;
            this.gbrdstock3.Text = "以Tray方式";
            this.gbrdstock3.UseVisualStyleBackColor = true;
            this.gbrdstock3.CheckedChanged += new System.EventHandler(this.gbrdstock3_CheckedChanged);
            // 
            // gbrdstock2
            // 
            this.gbrdstock2.AutoSize = true;
            this.gbrdstock2.Location = new System.Drawing.Point(227, 17);
            this.gbrdstock2.Name = "gbrdstock2";
            this.gbrdstock2.Size = new System.Drawing.Size(83, 16);
            this.gbrdstock2.TabIndex = 2;
            this.gbrdstock2.Text = "以卡通方式";
            this.gbrdstock2.UseVisualStyleBackColor = true;
            this.gbrdstock2.CheckedChanged += new System.EventHandler(this.gbrdstock2_CheckedChanged);
            // 
            // gbrdstock1
            // 
            this.gbrdstock1.AutoSize = true;
            this.gbrdstock1.Location = new System.Drawing.Point(116, 17);
            this.gbrdstock1.Name = "gbrdstock1";
            this.gbrdstock1.Size = new System.Drawing.Size(83, 16);
            this.gbrdstock1.TabIndex = 1;
            this.gbrdstock1.Text = "以栈板方式";
            this.gbrdstock1.UseVisualStyleBackColor = true;
            this.gbrdstock1.CheckedChanged += new System.EventHandler(this.gbrdstock1_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tb_selectedcount);
            this.splitContainer1.Panel1.Controls.Add(this.tb_totalcount);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.txtstore);
            this.splitContainer1.Panel1.Controls.Add(this.txtlocid);
            this.splitContainer1.Panel1.Controls.Add(this.txtCode);
            this.splitContainer1.Panel1.Controls.Add(this.btlocid);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbmsg);
            this.splitContainer1.Size = new System.Drawing.Size(859, 150);
            this.splitContainer1.SplitterDistance = 674;
            this.splitContainer1.TabIndex = 48;
            // 
            // tb_selectedcount
            // 
            // 
            // 
            // 
            this.tb_selectedcount.Border.Class = "TextBoxBorder";
            this.tb_selectedcount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tb_selectedcount.Location = new System.Drawing.Point(518, 96);
            this.tb_selectedcount.Name = "tb_selectedcount";
            this.tb_selectedcount.ReadOnly = true;
            this.tb_selectedcount.Size = new System.Drawing.Size(61, 21);
            this.tb_selectedcount.TabIndex = 50;
            this.tb_selectedcount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_totalcount
            // 
            // 
            // 
            // 
            this.tb_totalcount.Border.Class = "TextBoxBorder";
            this.tb_totalcount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tb_totalcount.Location = new System.Drawing.Point(518, 120);
            this.tb_totalcount.Name = "tb_totalcount";
            this.tb_totalcount.ReadOnly = true;
            this.tb_totalcount.Size = new System.Drawing.Size(93, 21);
            this.tb_totalcount.TabIndex = 49;
            this.tb_totalcount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "仓库/库位:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(453, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 48;
            this.label4.Text = "待入总数:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(453, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 48;
            this.label5.Text = "当前选中:";
            // 
            // txtstore
            // 
            // 
            // 
            // 
            this.txtstore.Border.Class = "TextBoxBorder";
            this.txtstore.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtstore.Enabled = false;
            this.txtstore.Location = new System.Drawing.Point(99, 122);
            this.txtstore.Name = "txtstore";
            this.txtstore.Size = new System.Drawing.Size(82, 21);
            this.txtstore.TabIndex = 42;
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
            this.txtlocid.Location = new System.Drawing.Point(187, 122);
            this.txtlocid.Name = "txtlocid";
            this.txtlocid.Size = new System.Drawing.Size(118, 21);
            this.txtlocid.TabIndex = 43;
            this.txtlocid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCode
            // 
            // 
            // 
            // 
            this.txtCode.Border.Class = "TextBoxBorder";
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCode.Location = new System.Drawing.Point(99, 96);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(206, 21);
            this.txtCode.TabIndex = 3;
            this.txtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            this.txtCode.MouseLeave += new System.EventHandler(this.txtCode_MouseLeave);
            // 
            // btlocid
            // 
            this.btlocid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btlocid.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btlocid.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btlocid.Location = new System.Drawing.Point(326, 123);
            this.btlocid.Name = "btlocid";
            this.btlocid.Size = new System.Drawing.Size(56, 20);
            this.btlocid.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btlocid.TabIndex = 2;
            this.btlocid.Text = "储位选择";
            this.btlocid.Click += new System.EventHandler(this.btlocid_Click);
            // 
            // rtbmsg
            // 
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbmsg.Location = new System.Drawing.Point(0, 0);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.ReadOnly = true;
            this.rtbmsg.Size = new System.Drawing.Size(181, 150);
            this.rtbmsg.TabIndex = 1;
            this.rtbmsg.Text = "";
            // 
            // dgvstockIn
            // 
            this.dgvstockIn.AllowDrop = true;
            this.dgvstockIn.AllowUserToAddRows = false;
            this.dgvstockIn.AllowUserToDeleteRows = false;
            this.dgvstockIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvstockIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvstockIn.Location = new System.Drawing.Point(0, 184);
            this.dgvstockIn.Name = "dgvstockIn";
            this.dgvstockIn.ReadOnly = true;
            this.dgvstockIn.RowTemplate.Height = 23;
            this.dgvstockIn.Size = new System.Drawing.Size(859, 258);
            this.dgvstockIn.TabIndex = 19;
            this.dgvstockIn.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvstockIn_CellClick);
            this.dgvstockIn.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvstockIn_CellMouseDoubleClick);
            this.dgvstockIn.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvstockIn_DataBindingComplete);
            this.dgvstockIn.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvstockIn_RowPostPaint);
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.bt_refresh);
            this.panelEx4.Controls.Add(this.comboBoxEx1);
            this.panelEx4.Controls.Add(this.buttonX1);
            this.panelEx4.Controls.Add(this.tbboxnumber);
            this.panelEx4.Controls.Add(this.label6);
            this.panelEx4.Controls.Add(this.btnSubmit);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx4.Location = new System.Drawing.Point(0, 150);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(859, 34);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 18;
            // 
            // bt_refresh
            // 
            this.bt_refresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_refresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_refresh.Location = new System.Drawing.Point(398, 6);
            this.bt_refresh.Name = "bt_refresh";
            this.bt_refresh.Size = new System.Drawing.Size(73, 21);
            this.bt_refresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_refresh.TabIndex = 4;
            this.bt_refresh.Text = "返回上一级";
            this.bt_refresh.Click += new System.EventHandler(this.bt_refresh_Click);
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 15;
            this.comboBoxEx1.Location = new System.Drawing.Point(52, 5);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(66, 21);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 3;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(309, 6);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(56, 21);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 2;
            this.buttonX1.Text = "查询";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // tbboxnumber
            // 
            this.tbboxnumber.Location = new System.Drawing.Point(131, 6);
            this.tbboxnumber.Name = "tbboxnumber";
            this.tbboxnumber.Size = new System.Drawing.Size(171, 21);
            this.tbboxnumber.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "查询";
            // 
            // dgv_showcarton
            // 
            this.dgv_showcarton.AllowUserToAddRows = false;
            this.dgv_showcarton.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_showcarton.Location = new System.Drawing.Point(468, 273);
            this.dgv_showcarton.Name = "dgv_showcarton";
            this.dgv_showcarton.ReadOnly = true;
            this.dgv_showcarton.RowTemplate.Height = 23;
            this.dgv_showcarton.Size = new System.Drawing.Size(180, 68);
            this.dgv_showcarton.TabIndex = 20;
            this.dgv_showcarton.Visible = false;
            this.dgv_showcarton.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_showcarton_CellClick);
            this.dgv_showcarton.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_showcarton_CellMouseDoubleClick);
            this.dgv_showcarton.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_showcarton_DataBindingComplete);
            this.dgv_showcarton.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_showcarton_RowPostPaint);
            // 
            // Frm_StockReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 442);
            this.Controls.Add(this.dgv_showcarton);
            this.Controls.Add(this.dgvstockIn);
            this.Controls.Add(this.panelEx4);
            this.Controls.Add(this.panelEx3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Frm_StockReceive";
            this.Text = "成品接收入库";
            this.Load += new System.EventHandler(this.Frm_StockReceive_Load);
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            this.gbtype.ResumeLayout(false);
            this.gbtype.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvstockIn)).EndInit();
            this.panelEx4.ResumeLayout(false);
            this.panelEx4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showcarton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSubmit;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.Label lblstatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbtype;
        private System.Windows.Forms.RadioButton gbrdstock3;
        private System.Windows.Forms.RadioButton gbrdstock2;
        private System.Windows.Forms.RadioButton gbrdstock1;
        private DevComponents.DotNetBar.ButtonX btlocid;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCode;
        public DevComponents.DotNetBar.Controls.TextBoxX txtlocid;
        public DevComponents.DotNetBar.Controls.TextBoxX txtstore;
        private System.Windows.Forms.RadioButton gbrdstock0;
        private System.Windows.Forms.DataGridView dgvstockIn;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbstatus;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_selectedcount;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_totalcount;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.TextBox tbboxnumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgv_showcarton;
        private DevComponents.DotNetBar.ButtonX bt_refresh;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rtbmsg;

    }
}