namespace SFIS_V2
{
    partial class Frm_Plan_Control
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.imbt_Excel = new DevComponents.DotNetBar.ButtonX();
            this.imbt_Query = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.chkall = new System.Windows.Forms.CheckBox();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.dgvShowData = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.label3 = new System.Windows.Forms.Label();
            this.imbt_queryRepair = new DevComponents.DotNetBar.ButtonX();
            this.label2 = new System.Windows.Forms.Label();
            this.dtRepairEnd = new System.Windows.Forms.DateTimePicker();
            this.dtRepairStart = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labcraft = new DevComponents.DotNetBar.LabelX();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(976, 526);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelEx3);
            this.tabPage1.Controls.Add(this.panelEx2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(968, 500);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "产出报表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.dgvData);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(3, 86);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(962, 411);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 5;
            this.panelEx3.Text = "panelEx3";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(962, 411);
            this.dgvData.TabIndex = 0;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panel1);
            this.panelEx2.Controls.Add(this.imbt_Excel);
            this.panelEx2.Controls.Add(this.imbt_Query);
            this.panelEx2.Controls.Add(this.label1);
            this.panelEx2.Controls.Add(this.chkall);
            this.panelEx2.Controls.Add(this.dtEnd);
            this.panelEx2.Controls.Add(this.dtStart);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(3, 3);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(962, 83);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 4;
            // 
            // imbt_Excel
            // 
            this.imbt_Excel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_Excel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_Excel.Location = new System.Drawing.Point(570, 22);
            this.imbt_Excel.Name = "imbt_Excel";
            this.imbt_Excel.Size = new System.Drawing.Size(75, 23);
            this.imbt_Excel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_Excel.TabIndex = 4;
            this.imbt_Excel.Text = "汇出Excel";
            this.imbt_Excel.Click += new System.EventHandler(this.imbt_Excel_Click);
            // 
            // imbt_Query
            // 
            this.imbt_Query.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_Query.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_Query.Location = new System.Drawing.Point(448, 22);
            this.imbt_Query.Name = "imbt_Query";
            this.imbt_Query.Size = new System.Drawing.Size(75, 23);
            this.imbt_Query.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_Query.TabIndex = 3;
            this.imbt_Query.Text = "查询";
            this.imbt_Query.Click += new System.EventHandler(this.imbt_Query_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "----->";
            // 
            // chkall
            // 
            this.chkall.AutoSize = true;
            this.chkall.Location = new System.Drawing.Point(30, 28);
            this.chkall.Name = "chkall";
            this.chkall.Size = new System.Drawing.Size(48, 16);
            this.chkall.TabIndex = 1;
            this.chkall.Text = "日期";
            this.chkall.UseVisualStyleBackColor = true;
            this.chkall.Click += new System.EventHandler(this.chkall_Click);
            // 
            // dtEnd
            // 
            this.dtEnd.Enabled = false;
            this.dtEnd.Location = new System.Drawing.Point(264, 24);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(126, 21);
            this.dtEnd.TabIndex = 0;
            // 
            // dtStart
            // 
            this.dtStart.Enabled = false;
            this.dtStart.Location = new System.Drawing.Point(84, 24);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(126, 21);
            this.dtStart.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelEx4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(968, 500);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "待维修报表";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.dgvShowData);
            this.panelEx4.Controls.Add(this.panelEx1);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(3, 3);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(962, 494);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 0;
            this.panelEx4.Text = "panelEx4";
            // 
            // dgvShowData
            // 
            this.dgvShowData.AllowUserToAddRows = false;
            this.dgvShowData.AllowUserToDeleteRows = false;
            this.dgvShowData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowData.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvShowData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShowData.Location = new System.Drawing.Point(0, 113);
            this.dgvShowData.Name = "dgvShowData";
            this.dgvShowData.ReadOnly = true;
            this.dgvShowData.RowTemplate.Height = 23;
            this.dgvShowData.Size = new System.Drawing.Size(962, 381);
            this.dgvShowData.TabIndex = 1;
            this.dgvShowData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvShowData_RowPostPaint);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmToExcel});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // tsmToExcel
            // 
            this.tsmToExcel.Name = "tsmToExcel";
            this.tsmToExcel.Size = new System.Drawing.Size(152, 22);
            this.tsmToExcel.Text = "汇出Excel";
            this.tsmToExcel.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.imbt_queryRepair);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.dtRepairEnd);
            this.panelEx1.Controls.Add(this.dtRepairStart);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(962, 113);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(5, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "起止日期:";
            // 
            // imbt_queryRepair
            // 
            this.imbt_queryRepair.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_queryRepair.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_queryRepair.Location = new System.Drawing.Point(511, 23);
            this.imbt_queryRepair.Name = "imbt_queryRepair";
            this.imbt_queryRepair.Size = new System.Drawing.Size(75, 37);
            this.imbt_queryRepair.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_queryRepair.TabIndex = 4;
            this.imbt_queryRepair.Text = "查询";
            this.imbt_queryRepair.Click += new System.EventHandler(this.imbt_queryRepair_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "----->";
            // 
            // dtRepairEnd
            // 
            this.dtRepairEnd.Location = new System.Drawing.Point(309, 22);
            this.dtRepairEnd.Name = "dtRepairEnd";
            this.dtRepairEnd.Size = new System.Drawing.Size(143, 21);
            this.dtRepairEnd.TabIndex = 1;
            // 
            // dtRepairStart
            // 
            this.dtRepairStart.Location = new System.Drawing.Point(97, 22);
            this.dtRepairStart.Name = "dtRepairStart";
            this.dtRepairStart.Size = new System.Drawing.Size(143, 21);
            this.dtRepairStart.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labcraft);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(962, 27);
            this.panel1.TabIndex = 5;
            // 
            // labcraft
            // 
            // 
            // 
            // 
            this.labcraft.BackgroundStyle.Class = "";
            this.labcraft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labcraft.Location = new System.Drawing.Point(0, 0);
            this.labcraft.Name = "labcraft";
            this.labcraft.Size = new System.Drawing.Size(962, 27);
            this.labcraft.TabIndex = 0;
            this.labcraft.Text = "labelX1";
            // 
            // Frm_Plan_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 526);
            this.Controls.Add(this.tabControl1);
            this.Name = "Frm_Plan_Control";
            this.Text = "计划报表";
            this.Load += new System.EventHandler(this.Frm_Plan_Control_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.DataGridView dgvData;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX imbt_Excel;
        private DevComponents.DotNetBar.ButtonX imbt_Query;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkall;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.TabPage tabPage2;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX imbt_queryRepair;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtRepairEnd;
        private System.Windows.Forms.DateTimePicker dtRepairStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvShowData;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmToExcel;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.LabelX labcraft;

    }
}