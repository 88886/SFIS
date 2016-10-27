namespace SFIS_V2
{
    partial class FrmGetDetailedSnList
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
            this.tb_wo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.dgvwosnrule = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkListSnType = new System.Windows.Forms.CheckedListBox();
            this.btn_compute = new DevComponents.DotNetBar.ButtonX();
            this.imbt_delete = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvwosnrule)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_wo
            // 
            this.tb_wo.Location = new System.Drawing.Point(93, 24);
            this.tb_wo.Name = "tb_wo";
            this.tb_wo.Size = new System.Drawing.Size(187, 21);
            this.tb_wo.TabIndex = 0;
            this.tb_wo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_wo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "工单:";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.imbt_delete);
            this.panelEx1.Controls.Add(this.tb_wo);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(643, 67);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 2;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.dgvwosnrule);
            this.panelEx2.Controls.Add(this.groupBox1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 67);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(643, 275);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 3;
            // 
            // dgvwosnrule
            // 
            this.dgvwosnrule.AllowUserToAddRows = false;
            this.dgvwosnrule.AllowUserToDeleteRows = false;
            this.dgvwosnrule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvwosnrule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvwosnrule.Location = new System.Drawing.Point(0, 0);
            this.dgvwosnrule.Name = "dgvwosnrule";
            this.dgvwosnrule.ReadOnly = true;
            this.dgvwosnrule.RowTemplate.Height = 23;
            this.dgvwosnrule.Size = new System.Drawing.Size(495, 275);
            this.dgvwosnrule.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkListSnType);
            this.groupBox1.Controls.Add(this.btn_compute);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(495, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(148, 275);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计算条码类型";
            // 
            // chkListSnType
            // 
            this.chkListSnType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkListSnType.FormattingEnabled = true;
            this.chkListSnType.Location = new System.Drawing.Point(3, 17);
            this.chkListSnType.Name = "chkListSnType";
            this.chkListSnType.Size = new System.Drawing.Size(142, 212);
            this.chkListSnType.TabIndex = 0;
            // 
            // btn_compute
            // 
            this.btn_compute.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_compute.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_compute.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_compute.Location = new System.Drawing.Point(3, 241);
            this.btn_compute.Name = "btn_compute";
            this.btn_compute.Size = new System.Drawing.Size(142, 31);
            this.btn_compute.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_compute.TabIndex = 1;
            this.btn_compute.Text = "计算";
            this.btn_compute.Click += new System.EventHandler(this.btn_compute_Click);
            // 
            // imbt_delete
            // 
            this.imbt_delete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_delete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_delete.Location = new System.Drawing.Point(311, 22);
            this.imbt_delete.Name = "imbt_delete";
            this.imbt_delete.Size = new System.Drawing.Size(75, 23);
            this.imbt_delete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_delete.TabIndex = 2;
            this.imbt_delete.Text = "删除";
            this.imbt_delete.Click += new System.EventHandler(this.imbt_delete_Click);
            // 
            // FrmGetDetailedSnList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 342);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmGetDetailedSnList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生产工单MAC号";
            this.Load += new System.EventHandler(this.FrmGetDetailedSnList_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvwosnrule)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tb_wo;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.DataGridView dgvwosnrule;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chkListSnType;
        private DevComponents.DotNetBar.ButtonX btn_compute;
        private DevComponents.DotNetBar.ButtonX imbt_delete;
    }
}