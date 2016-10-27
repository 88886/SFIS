namespace SFIS_V2
{
    partial class Frm_TimeOut
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
            this.dgvTimeOut = new System.Windows.Forms.DataGridView();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.PanelData = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.imbt_Cancel = new System.Windows.Forms.Button();
            this.imbt_OK = new System.Windows.Forms.Button();
            this.num_CHECK_TIMEOUT = new System.Windows.Forms.NumericUpDown();
            this.cbx_ROLLBACK_ROUTE = new System.Windows.Forms.ComboBox();
            this.cbx_CHECK_ROUTE = new System.Windows.Forms.ComboBox();
            this.txt_REST_TIME = new System.Windows.Forms.TextBox();
            this.txt_CHECK_NO = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.imbt_Delete = new DevComponents.DotNetBar.ButtonX();
            this.imbt_Modify = new DevComponents.DotNetBar.ButtonX();
            this.imbt_Add = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeOut)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.PanelData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_CHECK_TIMEOUT)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dgvTimeOut);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(931, 206);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "panelEx1";
            // 
            // dgvTimeOut
            // 
            this.dgvTimeOut.AllowUserToAddRows = false;
            this.dgvTimeOut.AllowUserToDeleteRows = false;
            this.dgvTimeOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimeOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTimeOut.Location = new System.Drawing.Point(0, 0);
            this.dgvTimeOut.Name = "dgvTimeOut";
            this.dgvTimeOut.ReadOnly = true;
            this.dgvTimeOut.RowTemplate.Height = 23;
            this.dgvTimeOut.Size = new System.Drawing.Size(931, 206);
            this.dgvTimeOut.TabIndex = 0;
            this.dgvTimeOut.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTimeOut_CellMouseClick);
            this.dgvTimeOut.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTimeOut_CellMouseDoubleClick);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.PanelData);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 287);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(931, 189);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // PanelData
            // 
            this.PanelData.Controls.Add(this.label6);
            this.PanelData.Controls.Add(this.imbt_Cancel);
            this.PanelData.Controls.Add(this.imbt_OK);
            this.PanelData.Controls.Add(this.num_CHECK_TIMEOUT);
            this.PanelData.Controls.Add(this.cbx_ROLLBACK_ROUTE);
            this.PanelData.Controls.Add(this.cbx_CHECK_ROUTE);
            this.PanelData.Controls.Add(this.txt_REST_TIME);
            this.PanelData.Controls.Add(this.txt_CHECK_NO);
            this.PanelData.Controls.Add(this.label4);
            this.PanelData.Controls.Add(this.label3);
            this.PanelData.Controls.Add(this.label5);
            this.PanelData.Controls.Add(this.label2);
            this.PanelData.Controls.Add(this.label1);
            this.PanelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelData.Location = new System.Drawing.Point(0, 0);
            this.PanelData.Name = "PanelData";
            this.PanelData.Size = new System.Drawing.Size(931, 189);
            this.PanelData.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(347, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "时间格式参考(24小时制,半角字符串):10:00~10:15|11:40~12:50";
            // 
            // imbt_Cancel
            // 
            this.imbt_Cancel.Location = new System.Drawing.Point(804, 115);
            this.imbt_Cancel.Name = "imbt_Cancel";
            this.imbt_Cancel.Size = new System.Drawing.Size(75, 23);
            this.imbt_Cancel.TabIndex = 5;
            this.imbt_Cancel.Text = "Cancel";
            this.imbt_Cancel.UseVisualStyleBackColor = true;
            this.imbt_Cancel.Click += new System.EventHandler(this.imbt_Cancel_Click);
            // 
            // imbt_OK
            // 
            this.imbt_OK.Location = new System.Drawing.Point(800, 46);
            this.imbt_OK.Name = "imbt_OK";
            this.imbt_OK.Size = new System.Drawing.Size(75, 23);
            this.imbt_OK.TabIndex = 4;
            this.imbt_OK.Text = "OK";
            this.imbt_OK.UseVisualStyleBackColor = true;
            this.imbt_OK.Click += new System.EventHandler(this.imbt_OK_Click);
            // 
            // num_CHECK_TIMEOUT
            // 
            this.num_CHECK_TIMEOUT.Location = new System.Drawing.Point(572, 58);
            this.num_CHECK_TIMEOUT.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.num_CHECK_TIMEOUT.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_CHECK_TIMEOUT.Name = "num_CHECK_TIMEOUT";
            this.num_CHECK_TIMEOUT.Size = new System.Drawing.Size(113, 21);
            this.num_CHECK_TIMEOUT.TabIndex = 3;
            this.num_CHECK_TIMEOUT.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // cbx_ROLLBACK_ROUTE
            // 
            this.cbx_ROLLBACK_ROUTE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_ROLLBACK_ROUTE.FormattingEnabled = true;
            this.cbx_ROLLBACK_ROUTE.Location = new System.Drawing.Point(389, 59);
            this.cbx_ROLLBACK_ROUTE.Name = "cbx_ROLLBACK_ROUTE";
            this.cbx_ROLLBACK_ROUTE.Size = new System.Drawing.Size(153, 20);
            this.cbx_ROLLBACK_ROUTE.TabIndex = 2;
            // 
            // cbx_CHECK_ROUTE
            // 
            this.cbx_CHECK_ROUTE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_CHECK_ROUTE.FormattingEnabled = true;
            this.cbx_CHECK_ROUTE.Location = new System.Drawing.Point(216, 59);
            this.cbx_CHECK_ROUTE.Name = "cbx_CHECK_ROUTE";
            this.cbx_CHECK_ROUTE.Size = new System.Drawing.Size(153, 20);
            this.cbx_CHECK_ROUTE.TabIndex = 2;
            // 
            // txt_REST_TIME
            // 
            this.txt_REST_TIME.Location = new System.Drawing.Point(49, 140);
            this.txt_REST_TIME.Name = "txt_REST_TIME";
            this.txt_REST_TIME.Size = new System.Drawing.Size(665, 21);
            this.txt_REST_TIME.TabIndex = 1;
            // 
            // txt_CHECK_NO
            // 
            this.txt_CHECK_NO.Enabled = false;
            this.txt_CHECK_NO.Location = new System.Drawing.Point(49, 59);
            this.txt_CHECK_NO.Name = "txt_CHECK_NO";
            this.txt_CHECK_NO.Size = new System.Drawing.Size(129, 21);
            this.txt_CHECK_NO.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(570, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "超时时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(387, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "退回途程";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "休息时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "检查途程";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "编号";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.imbt_Delete);
            this.panelEx3.Controls.Add(this.imbt_Modify);
            this.panelEx3.Controls.Add(this.imbt_Add);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx3.Location = new System.Drawing.Point(0, 206);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(931, 81);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 2;
            // 
            // imbt_Delete
            // 
            this.imbt_Delete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_Delete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_Delete.Location = new System.Drawing.Point(419, 18);
            this.imbt_Delete.Name = "imbt_Delete";
            this.imbt_Delete.Size = new System.Drawing.Size(75, 42);
            this.imbt_Delete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_Delete.TabIndex = 0;
            this.imbt_Delete.Text = "删除";
            this.imbt_Delete.Click += new System.EventHandler(this.imbt_Delete_Click);
            // 
            // imbt_Modify
            // 
            this.imbt_Modify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_Modify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_Modify.Location = new System.Drawing.Point(270, 18);
            this.imbt_Modify.Name = "imbt_Modify";
            this.imbt_Modify.Size = new System.Drawing.Size(75, 42);
            this.imbt_Modify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_Modify.TabIndex = 0;
            this.imbt_Modify.Text = "修改";
            this.imbt_Modify.Click += new System.EventHandler(this.imbt_Modify_Click);
            // 
            // imbt_Add
            // 
            this.imbt_Add.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_Add.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_Add.Location = new System.Drawing.Point(113, 18);
            this.imbt_Add.Name = "imbt_Add";
            this.imbt_Add.Size = new System.Drawing.Size(75, 42);
            this.imbt_Add.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_Add.TabIndex = 0;
            this.imbt_Add.Text = "新增";
            this.imbt_Add.Click += new System.EventHandler(this.imbt_Add_Click);
            // 
            // Frm_TimeOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 476);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.panelEx2);
            this.Name = "Frm_TimeOut";
            this.Text = "超时设置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_TimeOut_FormClosing);
            this.Load += new System.EventHandler(this.Frm_TimeOut_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeOut)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.PanelData.ResumeLayout(false);
            this.PanelData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_CHECK_TIMEOUT)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.DataGridView dgvTimeOut;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.Panel PanelData;
        private DevComponents.DotNetBar.ButtonX imbt_Delete;
        private DevComponents.DotNetBar.ButtonX imbt_Modify;
        private DevComponents.DotNetBar.ButtonX imbt_Add;
        private System.Windows.Forms.Button imbt_Cancel;
        private System.Windows.Forms.Button imbt_OK;
        private System.Windows.Forms.NumericUpDown num_CHECK_TIMEOUT;
        private System.Windows.Forms.ComboBox cbx_ROLLBACK_ROUTE;
        private System.Windows.Forms.ComboBox cbx_CHECK_ROUTE;
        private System.Windows.Forms.TextBox txt_REST_TIME;
        private System.Windows.Forms.TextBox txt_CHECK_NO;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
    }
}