namespace SFIS_V2
{
    partial class FrmQryKpMaster
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
            this.imbt_Query = new DevComponents.DotNetBar.ButtonX();
            this.numDays = new System.Windows.Forms.NumericUpDown();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvkpmaster = new System.Windows.Forms.DataGridView();
            this.masterId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lineId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bomver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcbside = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reserve1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.auditinguser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reserve2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvkpmaster)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.imbt_Query);
            this.panelEx1.Controls.Add(this.numDays);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(909, 66);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // imbt_Query
            // 
            this.imbt_Query.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_Query.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_Query.Location = new System.Drawing.Point(195, 17);
            this.imbt_Query.Name = "imbt_Query";
            this.imbt_Query.Size = new System.Drawing.Size(62, 33);
            this.imbt_Query.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_Query.TabIndex = 2;
            this.imbt_Query.Text = "查询";
            this.imbt_Query.Click += new System.EventHandler(this.imbt_Query_Click);
            // 
            // numDays
            // 
            this.numDays.Location = new System.Drawing.Point(41, 24);
            this.numDays.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDays.Name = "numDays";
            this.numDays.Size = new System.Drawing.Size(68, 21);
            this.numDays.TabIndex = 1;
            this.numDays.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.dgvkpmaster);
            this.panelEx2.Controls.Add(this.statusStrip1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 66);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(909, 404);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            this.panelEx2.Text = "panelEx2";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statuslabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 382);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(909, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statuslabel
            // 
            this.statuslabel.ForeColor = System.Drawing.SystemColors.InfoText;
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(32, 17);
            this.statuslabel.Text = "时间";
            // 
            // dgvkpmaster
            // 
            this.dgvkpmaster.AllowUserToAddRows = false;
            this.dgvkpmaster.AllowUserToDeleteRows = false;
            this.dgvkpmaster.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvkpmaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvkpmaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.masterId,
            this.lineId,
            this.userId,
            this.partnumber,
            this.modelname,
            this.bomver,
            this.pcbside,
            this.recdate,
            this.reserve1,
            this.auditinguser,
            this.reserve2});
            this.dgvkpmaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvkpmaster.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvkpmaster.Location = new System.Drawing.Point(0, 0);
            this.dgvkpmaster.Name = "dgvkpmaster";
            this.dgvkpmaster.ReadOnly = true;
            this.dgvkpmaster.RowTemplate.Height = 23;
            this.dgvkpmaster.Size = new System.Drawing.Size(909, 382);
            this.dgvkpmaster.TabIndex = 0;
            this.dgvkpmaster.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvkpmaster_RowPostPaint);
            // 
            // masterId
            // 
            this.masterId.DataPropertyName = "masterId";
            this.masterId.HeaderText = "SQE_NO";
            this.masterId.Name = "masterId";
            this.masterId.ReadOnly = true;
            // 
            // lineId
            // 
            this.lineId.DataPropertyName = "lineId";
            this.lineId.HeaderText = "线别";
            this.lineId.Name = "lineId";
            this.lineId.ReadOnly = true;
            // 
            // userId
            // 
            this.userId.DataPropertyName = "userId";
            this.userId.HeaderText = "料表制作人员";
            this.userId.Name = "userId";
            this.userId.ReadOnly = true;
            // 
            // partnumber
            // 
            this.partnumber.DataPropertyName = "partnumber";
            this.partnumber.HeaderText = "产品料号";
            this.partnumber.Name = "partnumber";
            this.partnumber.ReadOnly = true;
            // 
            // modelname
            // 
            this.modelname.DataPropertyName = "modelname";
            this.modelname.HeaderText = "产品名称";
            this.modelname.Name = "modelname";
            this.modelname.ReadOnly = true;
            // 
            // bomver
            // 
            this.bomver.DataPropertyName = "bomver";
            this.bomver.HeaderText = "BOM版本";
            this.bomver.Name = "bomver";
            this.bomver.ReadOnly = true;
            // 
            // pcbside
            // 
            this.pcbside.DataPropertyName = "pcbside";
            this.pcbside.HeaderText = "面别";
            this.pcbside.Name = "pcbside";
            this.pcbside.ReadOnly = true;
            // 
            // recdate
            // 
            this.recdate.DataPropertyName = "recdate";
            this.recdate.HeaderText = "时间";
            this.recdate.Name = "recdate";
            this.recdate.ReadOnly = true;
            // 
            // reserve1
            // 
            this.reserve1.DataPropertyName = "reserve1";
            this.reserve1.HeaderText = "产品全称";
            this.reserve1.Name = "reserve1";
            this.reserve1.ReadOnly = true;
            // 
            // auditinguser
            // 
            this.auditinguser.DataPropertyName = "auditinguser";
            this.auditinguser.HeaderText = "料号审核人员";
            this.auditinguser.Name = "auditinguser";
            this.auditinguser.ReadOnly = true;
            // 
            // reserve2
            // 
            this.reserve2.DataPropertyName = "reserve2";
            this.reserve2.HeaderText = "审核状态";
            this.reserve2.Name = "reserve2";
            this.reserve2.ReadOnly = true;
            // 
            // FrmQryKpMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 470);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.Name = "FrmQryKpMaster";
            this.Text = "查询料站表审核状况";
            this.Load += new System.EventHandler(this.FrmQryKpMaster_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvkpmaster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX imbt_Query;
        private System.Windows.Forms.NumericUpDown numDays;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.DataGridView dgvkpmaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn masterId;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineId;
        private System.Windows.Forms.DataGridViewTextBoxColumn userId;
        private System.Windows.Forms.DataGridViewTextBoxColumn partnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelname;
        private System.Windows.Forms.DataGridViewTextBoxColumn bomver;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcbside;
        private System.Windows.Forms.DataGridViewTextBoxColumn recdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn reserve1;
        private System.Windows.Forms.DataGridViewTextBoxColumn auditinguser;
        private System.Windows.Forms.DataGridViewTextBoxColumn reserve2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statuslabel;
    }
}