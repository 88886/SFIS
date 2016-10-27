namespace SFIS_V2
{
    partial class Frm_ErpWoInfo
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tb_woid = new System.Windows.Forms.TextBox();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.dgv_erp_wo = new System.Windows.Forms.DataGridView();
            this.imbt_NewWoInfo = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_erp_wo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.imbt_NewWoInfo);
            this.panelEx1.Controls.Add(this.tb_woid);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(842, 78);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(41, 29);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(59, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "工单:";
            // 
            // tb_woid
            // 
            this.tb_woid.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_woid.Location = new System.Drawing.Point(97, 26);
            this.tb_woid.Name = "tb_woid";
            this.tb_woid.Size = new System.Drawing.Size(256, 26);
            this.tb_woid.TabIndex = 1;
            this.tb_woid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_woid_KeyDown);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.dgv_erp_wo);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 78);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(842, 427);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // dgv_erp_wo
            // 
            this.dgv_erp_wo.AllowUserToAddRows = false;
            this.dgv_erp_wo.AllowUserToDeleteRows = false;
            this.dgv_erp_wo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_erp_wo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_erp_wo.Location = new System.Drawing.Point(0, 0);
            this.dgv_erp_wo.Name = "dgv_erp_wo";
            this.dgv_erp_wo.ReadOnly = true;
            this.dgv_erp_wo.RowHeadersVisible = false;
            this.dgv_erp_wo.RowTemplate.Height = 23;
            this.dgv_erp_wo.Size = new System.Drawing.Size(842, 427);
            this.dgv_erp_wo.TabIndex = 0;
            this.dgv_erp_wo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_erp_wo_CellClick);
            // 
            // imbt_NewWoInfo
            // 
            this.imbt_NewWoInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_NewWoInfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_NewWoInfo.Location = new System.Drawing.Point(414, 26);
            this.imbt_NewWoInfo.Name = "imbt_NewWoInfo";
            this.imbt_NewWoInfo.Size = new System.Drawing.Size(75, 23);
            this.imbt_NewWoInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_NewWoInfo.TabIndex = 2;
            this.imbt_NewWoInfo.Text = "新建工单";
            this.imbt_NewWoInfo.Click += new System.EventHandler(this.imbt_NewWoInfo_Click);
            // 
            // Frm_ErpWoInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 505);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ErpWoInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERP工单信息";
            this.Load += new System.EventHandler(this.Frm_ErpWoInfo_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_erp_wo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.TextBox tb_woid;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.DataGridView dgv_erp_wo;
        private DevComponents.DotNetBar.ButtonX imbt_NewWoInfo;
    }
}