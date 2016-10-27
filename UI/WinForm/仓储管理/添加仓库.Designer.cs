namespace SFIS_V2
{
    partial class Frmwarehouse
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
            this.label7 = new System.Windows.Forms.Label();
            this.bt_selectwarehouse = new DevComponents.DotNetBar.ButtonX();
            this.tb_number = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.bt_updatestore = new DevComponents.DotNetBar.ButtonX();
            this.bt_clear = new DevComponents.DotNetBar.ButtonX();
            this.bt_addstore = new DevComponents.DotNetBar.ButtonX();
            this.bx_resman = new DevComponents.DotNetBar.ButtonX();
            this.cb_storetype = new System.Windows.Forms.ComboBox();
            this.tb_remark = new System.Windows.Forms.TextBox();
            this.tb_storedesc = new System.Windows.Forms.TextBox();
            this.tb_storeman = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_storename = new System.Windows.Forms.TextBox();
            this.tb_storeid = new System.Windows.Forms.TextBox();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgv_warehouseinfo = new System.Windows.Forms.DataGridView();
            this.storehouseId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storehousename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storehousedesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storehouseman = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storehousetype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_warehouseinfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.bt_selectwarehouse);
            this.panelEx1.Controls.Add(this.tb_number);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(625, 40);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "仓库编号";
            // 
            // bt_selectwarehouse
            // 
            this.bt_selectwarehouse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_selectwarehouse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_selectwarehouse.Location = new System.Drawing.Point(190, 9);
            this.bt_selectwarehouse.Name = "bt_selectwarehouse";
            this.bt_selectwarehouse.Size = new System.Drawing.Size(75, 23);
            this.bt_selectwarehouse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_selectwarehouse.TabIndex = 1;
            this.bt_selectwarehouse.Text = "查询仓库";
            this.bt_selectwarehouse.Click += new System.EventHandler(this.bt_selectwarehouse_Click);
            // 
            // tb_number
            // 
            // 
            // 
            // 
            this.tb_number.Border.Class = "TextBoxBorder";
            this.tb_number.Location = new System.Drawing.Point(71, 11);
            this.tb_number.Name = "tb_number";
            this.tb_number.Size = new System.Drawing.Size(100, 21);
            this.tb_number.TabIndex = 0;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.bt_updatestore);
            this.panelEx2.Controls.Add(this.bt_clear);
            this.panelEx2.Controls.Add(this.bt_addstore);
            this.panelEx2.Controls.Add(this.bx_resman);
            this.panelEx2.Controls.Add(this.cb_storetype);
            this.panelEx2.Controls.Add(this.tb_remark);
            this.panelEx2.Controls.Add(this.tb_storedesc);
            this.panelEx2.Controls.Add(this.tb_storeman);
            this.panelEx2.Controls.Add(this.label6);
            this.panelEx2.Controls.Add(this.label5);
            this.panelEx2.Controls.Add(this.label4);
            this.panelEx2.Controls.Add(this.label3);
            this.panelEx2.Controls.Add(this.label2);
            this.panelEx2.Controls.Add(this.label1);
            this.panelEx2.Controls.Add(this.tb_storename);
            this.panelEx2.Controls.Add(this.tb_storeid);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 268);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(625, 130);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 2;
            // 
            // bt_updatestore
            // 
            this.bt_updatestore.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_updatestore.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_updatestore.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_updatestore.Location = new System.Drawing.Point(202, 95);
            this.bt_updatestore.Name = "bt_updatestore";
            this.bt_updatestore.Size = new System.Drawing.Size(75, 30);
            this.bt_updatestore.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_updatestore.TabIndex = 21;
            this.bt_updatestore.Text = "更新仓库";
            this.bt_updatestore.Click += new System.EventHandler(this.bt_updatestore_Click);
            // 
            // bt_clear
            // 
            this.bt_clear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_clear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_clear.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_clear.Location = new System.Drawing.Point(334, 95);
            this.bt_clear.Name = "bt_clear";
            this.bt_clear.Size = new System.Drawing.Size(75, 29);
            this.bt_clear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_clear.TabIndex = 20;
            this.bt_clear.Text = "清除";
            this.bt_clear.Click += new System.EventHandler(this.bt_clear_Click);
            // 
            // bt_addstore
            // 
            this.bt_addstore.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_addstore.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_addstore.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_addstore.Location = new System.Drawing.Point(59, 95);
            this.bt_addstore.Name = "bt_addstore";
            this.bt_addstore.Size = new System.Drawing.Size(75, 30);
            this.bt_addstore.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_addstore.TabIndex = 19;
            this.bt_addstore.Text = "添加仓库";
            this.bt_addstore.Click += new System.EventHandler(this.bt_addstore_Click);
            // 
            // bx_resman
            // 
            this.bx_resman.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bx_resman.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bx_resman.Location = new System.Drawing.Point(178, 32);
            this.bx_resman.Name = "bx_resman";
            this.bx_resman.Size = new System.Drawing.Size(26, 23);
            this.bx_resman.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bx_resman.TabIndex = 18;
            this.bx_resman.Click += new System.EventHandler(this.bx_resman_Click);
            // 
            // cb_storetype
            // 
            this.cb_storetype.FormattingEnabled = true;
            this.cb_storetype.Location = new System.Drawing.Point(335, 36);
            this.cb_storetype.Name = "cb_storetype";
            this.cb_storetype.Size = new System.Drawing.Size(99, 20);
            this.cb_storetype.TabIndex = 17;
            this.cb_storetype.Leave += new System.EventHandler(this.cb_storetype_Leave);
            // 
            // tb_remark
            // 
            this.tb_remark.Location = new System.Drawing.Point(334, 66);
            this.tb_remark.Name = "tb_remark";
            this.tb_remark.Size = new System.Drawing.Size(100, 21);
            this.tb_remark.TabIndex = 16;
            // 
            // tb_storedesc
            // 
            this.tb_storedesc.Location = new System.Drawing.Point(71, 65);
            this.tb_storedesc.Name = "tb_storedesc";
            this.tb_storedesc.Size = new System.Drawing.Size(100, 21);
            this.tb_storedesc.TabIndex = 15;
            // 
            // tb_storeman
            // 
            this.tb_storeman.Location = new System.Drawing.Point(71, 35);
            this.tb_storeman.Name = "tb_storeman";
            this.tb_storeman.Size = new System.Drawing.Size(100, 21);
            this.tb_storeman.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(275, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "备注";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(276, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "仓库类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "负责人";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "仓库描述";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "仓库名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "仓库编号";
            // 
            // tb_storename
            // 
            this.tb_storename.Location = new System.Drawing.Point(334, 7);
            this.tb_storename.Name = "tb_storename";
            this.tb_storename.Size = new System.Drawing.Size(100, 21);
            this.tb_storename.TabIndex = 7;
            // 
            // tb_storeid
            // 
            this.tb_storeid.Location = new System.Drawing.Point(71, 7);
            this.tb_storeid.Name = "tb_storeid";
            this.tb_storeid.Size = new System.Drawing.Size(100, 21);
            this.tb_storeid.TabIndex = 6;
            this.tb_storeid.Leave += new System.EventHandler(this.tb_storeid_Leave);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.dgv_warehouseinfo);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 40);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(625, 228);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 3;
            this.panelEx3.Text = "panelEx3";
            // 
            // dgv_warehouseinfo
            // 
            this.dgv_warehouseinfo.AllowUserToAddRows = false;
            this.dgv_warehouseinfo.AllowUserToDeleteRows = false;
            this.dgv_warehouseinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_warehouseinfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.storehouseId,
            this.storehousename,
            this.storehousedesc,
            this.storehouseman,
            this.storehousetype,
            this.remark});
            this.dgv_warehouseinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_warehouseinfo.Location = new System.Drawing.Point(0, 0);
            this.dgv_warehouseinfo.Name = "dgv_warehouseinfo";
            this.dgv_warehouseinfo.ReadOnly = true;
            this.dgv_warehouseinfo.RowTemplate.Height = 23;
            this.dgv_warehouseinfo.Size = new System.Drawing.Size(625, 228);
            this.dgv_warehouseinfo.TabIndex = 0;
            this.dgv_warehouseinfo.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_warehouseinfo_CellMouseDoubleClick);
            // 
            // storehouseId
            // 
            this.storehouseId.DataPropertyName = "storehouseId";
            this.storehouseId.HeaderText = "仓库编号";
            this.storehouseId.Name = "storehouseId";
            this.storehouseId.ReadOnly = true;
            // 
            // storehousename
            // 
            this.storehousename.DataPropertyName = "storehousename";
            this.storehousename.HeaderText = "仓库名称";
            this.storehousename.Name = "storehousename";
            this.storehousename.ReadOnly = true;
            // 
            // storehousedesc
            // 
            this.storehousedesc.DataPropertyName = "storehousedesc";
            this.storehousedesc.HeaderText = "仓库描述";
            this.storehousedesc.Name = "storehousedesc";
            this.storehousedesc.ReadOnly = true;
            // 
            // storehouseman
            // 
            this.storehouseman.DataPropertyName = "storehouseman";
            this.storehouseman.HeaderText = "负责人";
            this.storehouseman.Name = "storehouseman";
            this.storehouseman.ReadOnly = true;
            // 
            // storehousetype
            // 
            this.storehousetype.DataPropertyName = "storehousetype";
            this.storehousetype.HeaderText = "仓库类型";
            this.storehousetype.Name = "storehousetype";
            this.storehousetype.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // Frmwarehouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 398);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "Frmwarehouse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加仓库";
            this.Load += new System.EventHandler(this.Frmwarehouse_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_warehouseinfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_number;
        private DevComponents.DotNetBar.ButtonX bt_selectwarehouse;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.TextBox tb_storename;
        private System.Windows.Forms.TextBox tb_storeid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_storedesc;
        private System.Windows.Forms.ComboBox cb_storetype;
        private System.Windows.Forms.TextBox tb_remark;
        private DevComponents.DotNetBar.ButtonX bx_resman;
        private DevComponents.DotNetBar.ButtonX bt_addstore;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.DataGridView dgv_warehouseinfo;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox tb_storeman;
        private DevComponents.DotNetBar.ButtonX bt_clear;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehouseId;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehousename;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehousedesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehouseman;
        private System.Windows.Forms.DataGridViewTextBoxColumn storehousetype;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private DevComponents.DotNetBar.ButtonX bt_updatestore;

    }
}