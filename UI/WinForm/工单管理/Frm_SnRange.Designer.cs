namespace SFIS_V2
{
    partial class Frm_SnRange
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
            this.chk_krms = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cbSetEsn = new System.Windows.Forms.ComboBox();
            this.cmb_PlanOrder = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txt_woId = new System.Windows.Forms.TextBox();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgv_woinfo = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Lb_PlanInfo = new System.Windows.Forms.ListBox();
            this.ip_labletypes = new DevComponents.DotNetBar.ItemPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_snRange = new System.Windows.Forms.DataGridView();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.imbt_ConverSnList = new DevComponents.DotNetBar.ButtonX();
            this.imbt_save = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_woinfo)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_snRange)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.chk_krms);
            this.panelEx1.Controls.Add(this.label19);
            this.panelEx1.Controls.Add(this.cbSetEsn);
            this.panelEx1.Controls.Add(this.cmb_PlanOrder);
            this.panelEx1.Controls.Add(this.txt_woId);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(900, 94);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // chk_krms
            // 
            this.chk_krms.AutoSize = true;
            this.chk_krms.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_krms.Location = new System.Drawing.Point(225, 21);
            this.chk_krms.Name = "chk_krms";
            this.chk_krms.Size = new System.Drawing.Size(15, 14);
            this.chk_krms.TabIndex = 18;
            this.chk_krms.UseVisualStyleBackColor = true;
            this.chk_krms.Click += new System.EventHandler(this.chk_krms_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(484, 18);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(96, 16);
            this.label19.TabIndex = 17;
            this.label19.Text = "强制指定ESN";
            // 
            // cbSetEsn
            // 
            this.cbSetEsn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSetEsn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSetEsn.FormattingEnabled = true;
            this.cbSetEsn.Location = new System.Drawing.Point(487, 45);
            this.cbSetEsn.Name = "cbSetEsn";
            this.cbSetEsn.Size = new System.Drawing.Size(105, 24);
            this.cbSetEsn.TabIndex = 3;
            // 
            // cmb_PlanOrder
            // 
            this.cmb_PlanOrder.DisplayMember = "Text";
            this.cmb_PlanOrder.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_PlanOrder.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_PlanOrder.FormattingEnabled = true;
            this.cmb_PlanOrder.ItemHeight = 20;
            this.cmb_PlanOrder.Location = new System.Drawing.Point(223, 43);
            this.cmb_PlanOrder.Name = "cmb_PlanOrder";
            this.cmb_PlanOrder.Size = new System.Drawing.Size(198, 26);
            this.cmb_PlanOrder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmb_PlanOrder.TabIndex = 2;
            this.cmb_PlanOrder.SelectedIndexChanged += new System.EventHandler(this.cmb_PlanOrder_SelectedIndexChanged);
            // 
            // txt_woId
            // 
            this.txt_woId.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_woId.Location = new System.Drawing.Point(12, 45);
            this.txt_woId.Name = "txt_woId";
            this.txt_woId.Size = new System.Drawing.Size(138, 26);
            this.txt_woId.TabIndex = 1;
            this.txt_woId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_woId_KeyDown);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(243, 18);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(68, 23);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "投产单:";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(12, 16);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(56, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "工单:";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.groupBox3);
            this.panelEx2.Controls.Add(this.groupBox4);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 94);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(900, 181);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgv_woinfo);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(549, 181);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "工单显示";
            // 
            // dgv_woinfo
            // 
            this.dgv_woinfo.AllowUserToAddRows = false;
            this.dgv_woinfo.AllowUserToDeleteRows = false;
            this.dgv_woinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_woinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_woinfo.Location = new System.Drawing.Point(3, 17);
            this.dgv_woinfo.Name = "dgv_woinfo";
            this.dgv_woinfo.ReadOnly = true;
            this.dgv_woinfo.RowTemplate.Height = 23;
            this.dgv_woinfo.Size = new System.Drawing.Size(543, 161);
            this.dgv_woinfo.TabIndex = 20;
            this.dgv_woinfo.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_woinfo_DataBindingComplete);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Lb_PlanInfo);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox4.Location = new System.Drawing.Point(549, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(351, 181);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "投产单信息";
            // 
            // Lb_PlanInfo
            // 
            this.Lb_PlanInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lb_PlanInfo.FormattingEnabled = true;
            this.Lb_PlanInfo.ItemHeight = 12;
            this.Lb_PlanInfo.Location = new System.Drawing.Point(3, 17);
            this.Lb_PlanInfo.Name = "Lb_PlanInfo";
            this.Lb_PlanInfo.Size = new System.Drawing.Size(345, 161);
            this.Lb_PlanInfo.TabIndex = 19;
            // 
            // ip_labletypes
            // 
            // 
            // 
            // 
            this.ip_labletypes.BackgroundStyle.Class = "ItemPanel";
            this.ip_labletypes.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ip_labletypes.ContainerControlProcessDialogKey = true;
            this.ip_labletypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ip_labletypes.DragDropSupport = true;
            this.ip_labletypes.Location = new System.Drawing.Point(3, 17);
            this.ip_labletypes.MultiLine = true;
            this.ip_labletypes.Name = "ip_labletypes";
            this.ip_labletypes.Size = new System.Drawing.Size(894, 73);
            this.ip_labletypes.TabIndex = 5;
            this.ip_labletypes.ItemClick += new System.EventHandler(this.ip_labletypes_ItemClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ip_labletypes);
            this.groupBox1.Location = new System.Drawing.Point(0, 275);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(900, 93);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "区间类型";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgv_snRange);
            this.groupBox2.Controls.Add(this.panelEx3);
            this.groupBox2.Location = new System.Drawing.Point(0, 368);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(900, 207);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "区间明细";
            // 
            // dgv_snRange
            // 
            this.dgv_snRange.AllowUserToAddRows = false;
            this.dgv_snRange.AllowUserToDeleteRows = false;
            this.dgv_snRange.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_snRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_snRange.Location = new System.Drawing.Point(3, 17);
            this.dgv_snRange.Name = "dgv_snRange";
            this.dgv_snRange.ReadOnly = true;
            this.dgv_snRange.RowTemplate.Height = 23;
            this.dgv_snRange.Size = new System.Drawing.Size(712, 187);
            this.dgv_snRange.TabIndex = 0;
            this.dgv_snRange.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_snRange_DataBindingComplete);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.imbt_ConverSnList);
            this.panelEx3.Controls.Add(this.imbt_save);
            this.panelEx3.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelEx3.Location = new System.Drawing.Point(715, 17);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(182, 187);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 1;
            // 
            // imbt_ConverSnList
            // 
            this.imbt_ConverSnList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_ConverSnList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_ConverSnList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_ConverSnList.Location = new System.Drawing.Point(43, 107);
            this.imbt_ConverSnList.Name = "imbt_ConverSnList";
            this.imbt_ConverSnList.Size = new System.Drawing.Size(94, 42);
            this.imbt_ConverSnList.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_ConverSnList.TabIndex = 0;
            this.imbt_ConverSnList.Text = "计算序号";
            this.imbt_ConverSnList.Visible = false;
            this.imbt_ConverSnList.Click += new System.EventHandler(this.imbt_ConverSnList_Click);
            // 
            // imbt_save
            // 
            this.imbt_save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_save.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_save.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_save.Location = new System.Drawing.Point(43, 28);
            this.imbt_save.Name = "imbt_save";
            this.imbt_save.Size = new System.Drawing.Size(94, 42);
            this.imbt_save.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_save.TabIndex = 0;
            this.imbt_save.Text = "保存";
            this.imbt_save.Click += new System.EventHandler(this.imbt_save_Click);
            // 
            // Frm_SnRange
            // 
            this.ClientSize = new System.Drawing.Size(900, 575);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "Frm_SnRange";
            this.Text = "Frm_SnRange";
            this.Load += new System.EventHandler(this.Frm_SnRange_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_woinfo)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_snRange)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmb_PlanOrder;
        private System.Windows.Forms.TextBox txt_woId;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.DataGridView dgv_woinfo;
        private System.Windows.Forms.ListBox Lb_PlanInfo;
        private DevComponents.DotNetBar.ItemPanel ip_labletypes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbSetEsn;
        public System.Windows.Forms.DataGridView dgv_snRange;
        private System.Windows.Forms.CheckBox chk_krms;
        private System.Windows.Forms.Label label19;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ButtonX imbt_save;
        private DevComponents.DotNetBar.ButtonX imbt_ConverSnList;

    }
}