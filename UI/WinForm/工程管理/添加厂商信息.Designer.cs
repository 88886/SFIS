namespace SFIS_V2
{
    partial class AddManufacturer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tb_englishname = new System.Windows.Forms.TextBox();
            this.tb_chinaname = new System.Windows.Forms.TextBox();
            this.tb_vendercode = new System.Windows.Forms.TextBox();
            this.bt_query = new DevComponents.DotNetBar.ButtonX();
            this.bt_add = new DevComponents.DotNetBar.ButtonX();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.venderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendername2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.tb_englishname);
            this.panelEx1.Controls.Add(this.tb_chinaname);
            this.panelEx1.Controls.Add(this.tb_vendercode);
            this.panelEx1.Controls.Add(this.bt_query);
            this.panelEx1.Controls.Add(this.bt_add);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(435, 65);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // tb_englishname
            // 
            this.tb_englishname.Location = new System.Drawing.Point(95, 39);
            this.tb_englishname.Name = "tb_englishname";
            this.tb_englishname.Size = new System.Drawing.Size(100, 21);
            this.tb_englishname.TabIndex = 7;
            // 
            // tb_chinaname
            // 
            this.tb_chinaname.Location = new System.Drawing.Point(294, 6);
            this.tb_chinaname.Name = "tb_chinaname";
            this.tb_chinaname.Size = new System.Drawing.Size(100, 21);
            this.tb_chinaname.TabIndex = 6;
            // 
            // tb_vendercode
            // 
            this.tb_vendercode.Location = new System.Drawing.Point(95, 6);
            this.tb_vendercode.Name = "tb_vendercode";
            this.tb_vendercode.Size = new System.Drawing.Size(100, 21);
            this.tb_vendercode.TabIndex = 5;
            this.toolTip1.SetToolTip(this.tb_vendercode, "编号位数不超过10位。");
            this.tb_vendercode.Leave += new System.EventHandler(this.tb_vendercode_Leave);
            // 
            // bt_query
            // 
            this.bt_query.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_query.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_query.Location = new System.Drawing.Point(213, 38);
            this.bt_query.Name = "bt_query";
            this.bt_query.Size = new System.Drawing.Size(75, 23);
            this.bt_query.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_query.TabIndex = 4;
            this.bt_query.Text = "查询";
            this.bt_query.Click += new System.EventHandler(this.bt_query_Click);
            // 
            // bt_add
            // 
            this.bt_add.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_add.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_add.Location = new System.Drawing.Point(319, 37);
            this.bt_add.Name = "bt_add";
            this.bt_add.Size = new System.Drawing.Size(75, 23);
            this.bt_add.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_add.TabIndex = 3;
            this.bt_add.Text = "添加";
            this.bt_add.Click += new System.EventHandler(this.bt_add_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "厂商名称(英):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "厂商名称(中):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "厂商编号:";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.dataGridViewX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 65);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(435, 307);
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
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.venderId,
            this.vendername2,
            this.vendername});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.ReadOnly = true;
            this.dataGridViewX1.RowTemplate.Height = 23;
            this.dataGridViewX1.Size = new System.Drawing.Size(435, 307);
            this.dataGridViewX1.TabIndex = 0;
            this.dataGridViewX1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseDoubleClick);
            // 
            // venderId
            // 
            this.venderId.DataPropertyName = "venderId";
            this.venderId.HeaderText = "厂商编号";
            this.venderId.Name = "venderId";
            this.venderId.ReadOnly = true;
            // 
            // vendername2
            // 
            this.vendername2.DataPropertyName = "vendername2";
            this.vendername2.HeaderText = "厂商名称(中)";
            this.vendername2.Name = "vendername2";
            this.vendername2.ReadOnly = true;
            // 
            // vendername
            // 
            this.vendername.DataPropertyName = "vendername";
            this.vendername.HeaderText = "厂商名称(英)";
            this.vendername.Name = "vendername";
            this.vendername.ReadOnly = true;
            // 
            // AddManufacturer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 372);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddManufacturer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加厂商信息";
            this.Load += new System.EventHandler(this.AddManufacturer_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private System.Windows.Forms.TextBox tb_englishname;
        private System.Windows.Forms.TextBox tb_chinaname;
        private System.Windows.Forms.TextBox tb_vendercode;
        private DevComponents.DotNetBar.ButtonX bt_query;
        private DevComponents.DotNetBar.ButtonX bt_add;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn venderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendername2;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendername;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}