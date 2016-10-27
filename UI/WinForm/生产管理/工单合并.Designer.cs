namespace SFIS_V2
{
    partial class FrmMoMerge
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
            this.tboldmo = new System.Windows.Forms.TextBox();
            this.tbnewmo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btok = new DevComponents.DotNetBar.ButtonX();
            this.rdchgmo = new System.Windows.Forms.RadioButton();
            this.rdtiaoji = new System.Windows.Forms.RadioButton();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dgvsmtio = new System.Windows.Forms.DataGridView();
            this.SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工单 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.机器编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.面别 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.人员工号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dgvmaterial = new System.Windows.Forms.DataGridView();
            this.panelEx1.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsmtio)).BeginInit();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvmaterial)).BeginInit();
            this.SuspendLayout();
            // 
            // tboldmo
            // 
            this.tboldmo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tboldmo.Location = new System.Drawing.Point(114, 19);
            this.tboldmo.Name = "tboldmo";
            this.tboldmo.Size = new System.Drawing.Size(292, 23);
            this.tboldmo.TabIndex = 0;
            this.tboldmo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tboldmo_KeyDown);      
            // 
            // tbnewmo
            // 
            this.tbnewmo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbnewmo.Location = new System.Drawing.Point(114, 67);
            this.tbnewmo.Name = "tbnewmo";
            this.tbnewmo.Size = new System.Drawing.Size(292, 23);
            this.tbnewmo.TabIndex = 1;
            this.tbnewmo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbnewmo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(28, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "原料站表:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(27, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "新料站表:";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btok);
            this.panelEx1.Controls.Add(this.rdchgmo);
            this.panelEx1.Controls.Add(this.rdtiaoji);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.tboldmo);
            this.panelEx1.Controls.Add(this.tbnewmo);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(703, 111);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 4;
            // 
            // btok
            // 
            this.btok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btok.Enabled = false;
            this.btok.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btok.Location = new System.Drawing.Point(600, 26);
            this.btok.Name = "btok";
            this.btok.Size = new System.Drawing.Size(91, 60);
            this.btok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btok.TabIndex = 6;
            this.btok.Text = "确认";
            this.btok.Click += new System.EventHandler(this.btok_Click);
            // 
            // rdchgmo
            // 
            this.rdchgmo.AutoSize = true;
            this.rdchgmo.Location = new System.Drawing.Point(481, 60);
            this.rdchgmo.Name = "rdchgmo";
            this.rdchgmo.Size = new System.Drawing.Size(71, 16);
            this.rdchgmo.TabIndex = 5;
            this.rdchgmo.Text = "合并工单";
            this.rdchgmo.UseVisualStyleBackColor = true;
            // 
            // rdtiaoji
            // 
            this.rdtiaoji.AutoSize = true;
            this.rdtiaoji.Checked = true;
            this.rdtiaoji.Location = new System.Drawing.Point(481, 26);
            this.rdtiaoji.Name = "rdtiaoji";
            this.rdtiaoji.Size = new System.Drawing.Size(83, 16);
            this.rdtiaoji.TabIndex = 4;
            this.rdtiaoji.TabStop = true;
            this.rdtiaoji.Text = "工单内调机";
            this.rdtiaoji.UseVisualStyleBackColor = true;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.dgvsmtio);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 111);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(703, 144);
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
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.TabIndex = 5;
            this.groupPanel1.Text = "备料表头信息";
            // 
            // dgvsmtio
            // 
            this.dgvsmtio.AllowUserToAddRows = false;
            this.dgvsmtio.AllowUserToDeleteRows = false;
            this.dgvsmtio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvsmtio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SEQ,
            this.工单,
            this.机器编号,
            this.状态,
            this.面别,
            this.人员工号,
            this.时间});
            this.dgvsmtio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvsmtio.Location = new System.Drawing.Point(0, 0);
            this.dgvsmtio.Name = "dgvsmtio";
            this.dgvsmtio.ReadOnly = true;
            this.dgvsmtio.RowTemplate.Height = 23;
            this.dgvsmtio.Size = new System.Drawing.Size(697, 120);
            this.dgvsmtio.TabIndex = 0;
            this.dgvsmtio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tboldmo_KeyDown);
            // 
            // SEQ
            // 
            this.SEQ.DataPropertyName = "MasterID";
            this.SEQ.HeaderText = "SEQ";
            this.SEQ.Name = "SEQ";
            this.SEQ.ReadOnly = true;
            // 
            // 工单
            // 
            this.工单.DataPropertyName = "woid";
            this.工单.HeaderText = "工单";
            this.工单.Name = "工单";
            this.工单.ReadOnly = true;
            // 
            // 机器编号
            // 
            this.机器编号.DataPropertyName = "machineid";
            this.机器编号.HeaderText = "机器编号";
            this.机器编号.Name = "机器编号";
            this.机器编号.ReadOnly = true;
            // 
            // 状态
            // 
            this.状态.DataPropertyName = "status";
            this.状态.HeaderText = "状态";
            this.状态.Name = "状态";
            this.状态.ReadOnly = true;
            // 
            // 面别
            // 
            this.面别.DataPropertyName = "side";
            this.面别.HeaderText = "面别";
            this.面别.Name = "面别";
            this.面别.ReadOnly = true;
            // 
            // 人员工号
            // 
            this.人员工号.DataPropertyName = "userid";
            this.人员工号.HeaderText = "人员工号";
            this.人员工号.Name = "人员工号";
            this.人员工号.ReadOnly = true;
            // 
            // 时间
            // 
            this.时间.DataPropertyName = "dtime";
            this.时间.HeaderText = "时间";
            this.时间.Name = "时间";
            this.时间.ReadOnly = true;
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.dgvmaterial);
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel2.Location = new System.Drawing.Point(0, 255);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(703, 167);
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
            this.groupPanel2.Style.Class = "";
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.Class = "";
            this.groupPanel2.TabIndex = 6;
            this.groupPanel2.Text = "上料信息";
            // 
            // dgvmaterial
            // 
            this.dgvmaterial.AllowUserToAddRows = false;
            this.dgvmaterial.AllowUserToDeleteRows = false;
            this.dgvmaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvmaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvmaterial.Location = new System.Drawing.Point(0, 0);
            this.dgvmaterial.Name = "dgvmaterial";
            this.dgvmaterial.ReadOnly = true;
            this.dgvmaterial.RowTemplate.Height = 23;
            this.dgvmaterial.Size = new System.Drawing.Size(697, 143);
            this.dgvmaterial.TabIndex = 0;
            // 
            // FrmMoMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 422);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.Name = "FrmMoMerge";
            this.Text = "SMT合并工单";
            this.Load += new System.EventHandler(this.FrmMoMerge_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvsmtio)).EndInit();
            this.groupPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvmaterial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tboldmo;
        private System.Windows.Forms.TextBox tbnewmo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.RadioButton rdchgmo;
        private System.Windows.Forms.RadioButton rdtiaoji;
        private DevComponents.DotNetBar.ButtonX btok;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.DataGridView dgvsmtio;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private System.Windows.Forms.DataGridView dgvmaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工单;
        private System.Windows.Forms.DataGridViewTextBoxColumn 机器编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 状态;
        private System.Windows.Forms.DataGridViewTextBoxColumn 面别;
        private System.Windows.Forms.DataGridViewTextBoxColumn 人员工号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 时间;
    }
}