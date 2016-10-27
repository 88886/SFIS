namespace SFIS_V2
{
    partial class Frm_SmtStockIn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelStation = new DevComponents.DotNetBar.PanelEx();
            this.LabStation = new DevComponents.DotNetBar.LabelX();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.imbt_Option = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_SelectLine = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_Reprint = new DevComponents.DotNetBar.ButtonItem();
            this.Imbt_ReUpload = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dgvshowdata = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.LabError = new System.Windows.Forms.Label();
            this.imbt_commit = new DevComponents.DotNetBar.ButtonX();
            this.txt_woid = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panelStation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvshowdata)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelStation
            // 
            this.panelStation.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelStation.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelStation.Controls.Add(this.LabStation);
            this.panelStation.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelStation.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStation.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelStation.Location = new System.Drawing.Point(0, 25);
            this.panelStation.Name = "panelStation";
            this.panelStation.Size = new System.Drawing.Size(832, 56);
            this.panelStation.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelStation.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelStation.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelStation.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelStation.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelStation.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelStation.Style.GradientAngle = 90;
            this.panelStation.TabIndex = 21;
            // 
            // LabStation
            // 
            // 
            // 
            // 
            this.LabStation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabStation.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabStation.ForeColor = System.Drawing.Color.Blue;
            this.LabStation.Location = new System.Drawing.Point(0, 0);
            this.LabStation.Name = "LabStation";
            this.LabStation.SingleLineColor = System.Drawing.SystemColors.AppWorkspace;
            this.LabStation.Size = new System.Drawing.Size(832, 56);
            this.LabStation.TabIndex = 0;
            this.LabStation.Text = "S080101:PACK_CTN";
            this.LabStation.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.imbt_Option});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(832, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 20;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // imbt_Option
            // 
            this.imbt_Option.Name = "imbt_Option";
            this.imbt_Option.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.imbt_SelectLine,
            this.imbt_Reprint,
            this.Imbt_ReUpload});
            this.imbt_Option.Text = "选项";
            // 
            // imbt_SelectLine
            // 
            this.imbt_SelectLine.Name = "imbt_SelectLine";
            this.imbt_SelectLine.Stretch = true;
            this.imbt_SelectLine.Text = "选择线体";
            this.imbt_SelectLine.Click += new System.EventHandler(this.imbt_SelectLine_Click);
            // 
            // imbt_Reprint
            // 
            this.imbt_Reprint.Name = "imbt_Reprint";
            this.imbt_Reprint.Text = "重复打印单据";
            this.imbt_Reprint.Click += new System.EventHandler(this.imbt_Reprint_Click);
            // 
            // Imbt_ReUpload
            // 
            this.Imbt_ReUpload.Name = "Imbt_ReUpload";
            this.Imbt_ReUpload.Text = "提交未完成单据";
            this.Imbt_ReUpload.Click += new System.EventHandler(this.Imbt_ReUpload_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.groupPanel1);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 81);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(832, 322);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 25;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.dgvshowdata);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(345, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(487, 322);
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
            this.groupPanel1.TabIndex = 4;
            this.groupPanel1.Text = "数据显示";
            // 
            // dgvshowdata
            // 
            this.dgvshowdata.AllowUserToAddRows = false;
            this.dgvshowdata.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvshowdata.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvshowdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvshowdata.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvshowdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvshowdata.EnableHeadersVisualStyles = false;
            this.dgvshowdata.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.dgvshowdata.Location = new System.Drawing.Point(0, 0);
            this.dgvshowdata.Name = "dgvshowdata";
            this.dgvshowdata.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvshowdata.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvshowdata.RowTemplate.Height = 23;
            this.dgvshowdata.Size = new System.Drawing.Size(481, 298);
            this.dgvshowdata.TabIndex = 0;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.rtbmsg);
            this.panelEx2.Controls.Add(this.panelEx3);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(345, 322);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // rtbmsg
            // 
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbmsg.Location = new System.Drawing.Point(0, 116);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.Size = new System.Drawing.Size(345, 206);
            this.rtbmsg.TabIndex = 6;
            this.rtbmsg.Text = "";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.LabError);
            this.panelEx3.Controls.Add(this.imbt_commit);
            this.panelEx3.Controls.Add(this.txt_woid);
            this.panelEx3.Controls.Add(this.labelX1);
            this.panelEx3.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(345, 116);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 2;
            // 
            // LabError
            // 
            this.LabError.AutoSize = true;
            this.LabError.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabError.ForeColor = System.Drawing.Color.Red;
            this.LabError.Location = new System.Drawing.Point(106, 81);
            this.LabError.Name = "LabError";
            this.LabError.Size = new System.Drawing.Size(106, 21);
            this.LabError.TabIndex = 3;
            this.LabError.Text = "NG COUNT";
            this.LabError.Visible = false;
            // 
            // imbt_commit
            // 
            this.imbt_commit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_commit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_commit.Location = new System.Drawing.Point(241, 19);
            this.imbt_commit.Name = "imbt_commit";
            this.imbt_commit.Size = new System.Drawing.Size(75, 23);
            this.imbt_commit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_commit.TabIndex = 2;
            this.imbt_commit.Text = "提交入库";
            this.imbt_commit.Click += new System.EventHandler(this.imbt_commit_Click);
            // 
            // txt_woid
            // 
            // 
            // 
            // 
            this.txt_woid.Border.Class = "TextBoxBorder";
            this.txt_woid.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_woid.Location = new System.Drawing.Point(60, 21);
            this.txt_woid.Name = "txt_woid";
            this.txt_woid.PreventEnterBeep = true;
            this.txt_woid.Size = new System.Drawing.Size(152, 21);
            this.txt_woid.TabIndex = 1;
            this.txt_woid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_woid_KeyDown);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(10, 19);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(44, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "工单:";
            // 
            // progressBarX1
            // 
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarX1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarX1.Location = new System.Drawing.Point(0, 403);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.Size = new System.Drawing.Size(832, 23);
            this.progressBarX1.TabIndex = 0;
            this.progressBarX1.Text = "0/0";
            this.progressBarX1.TextVisible = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // Frm_SmtStockIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 426);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.progressBarX1);
            this.Controls.Add(this.panelStation);
            this.Controls.Add(this.bar1);
            this.DoubleBuffered = true;
            this.Name = "Frm_SmtStockIn";
            this.Text = "SMT入库";
            this.Load += new System.EventHandler(this.Frm_SmtStockIn_Load);
            this.panelStation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvshowdata)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelStation;
        private DevComponents.DotNetBar.LabelX LabStation;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem imbt_Option;
        private DevComponents.DotNetBar.ButtonItem imbt_SelectLine;
        private DevComponents.DotNetBar.ButtonItem imbt_Reprint;
        private DevComponents.DotNetBar.ButtonItem Imbt_ReUpload;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarX1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvshowdata;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_woid;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.RichTextBox rtbmsg;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ButtonX imbt_commit;
        private System.Windows.Forms.Label LabError;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}