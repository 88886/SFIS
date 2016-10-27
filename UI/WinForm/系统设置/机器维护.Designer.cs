namespace SFIS_V2
{
    partial class MachineInfo
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
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_showmachineInfo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.machineId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lineId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fixtureId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machinedesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IpAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IpAddress1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IpAddress2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bt_delete = new DevComponents.DotNetBar.ButtonX();
            this.bt_add = new DevComponents.DotNetBar.ButtonX();
            this.txt_ipaddress2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt_ipaddress1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt_ipaddress = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt_fixtureId = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt_machinedesc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt_machineId = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cbx_lineId = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showmachineInfo)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(803, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_showmachineInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 205);
            this.panel1.TabIndex = 1;
            // 
            // dgv_showmachineInfo
            // 
            this.dgv_showmachineInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_showmachineInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.machineId,
            this.lineId,
            this.fixtureId,
            this.machinedesc,
            this.IpAddress,
            this.IpAddress1,
            this.IpAddress2});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_showmachineInfo.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_showmachineInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_showmachineInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_showmachineInfo.Location = new System.Drawing.Point(0, 0);
            this.dgv_showmachineInfo.Name = "dgv_showmachineInfo";
            this.dgv_showmachineInfo.RowTemplate.Height = 23;
            this.dgv_showmachineInfo.Size = new System.Drawing.Size(803, 205);
            this.dgv_showmachineInfo.TabIndex = 0;
            this.dgv_showmachineInfo.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_showmachineInfo_CellMouseDown);
            // 
            // machineId
            // 
            this.machineId.DataPropertyName = "machineId";
            this.machineId.HeaderText = "机器编号";
            this.machineId.Name = "machineId";
            // 
            // lineId
            // 
            this.lineId.DataPropertyName = "lineId";
            this.lineId.HeaderText = "线条编号";
            this.lineId.Name = "lineId";
            // 
            // fixtureId
            // 
            this.fixtureId.DataPropertyName = "fixtureId";
            this.fixtureId.HeaderText = "设备编号";
            this.fixtureId.Name = "fixtureId";
            // 
            // machinedesc
            // 
            this.machinedesc.DataPropertyName = "machinedesc";
            this.machinedesc.HeaderText = "机器描述";
            this.machinedesc.Name = "machinedesc";
            // 
            // IpAddress
            // 
            this.IpAddress.DataPropertyName = "IpAddress";
            this.IpAddress.HeaderText = "IP1";
            this.IpAddress.Name = "IpAddress";
            // 
            // IpAddress1
            // 
            this.IpAddress1.DataPropertyName = "IpAddress1";
            this.IpAddress1.HeaderText = "IP2";
            this.IpAddress1.Name = "IpAddress1";
            // 
            // IpAddress2
            // 
            this.IpAddress2.DataPropertyName = "IpAddress2";
            this.IpAddress2.HeaderText = "IP3";
            this.IpAddress2.Name = "IpAddress2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bt_delete);
            this.panel2.Controls.Add(this.bt_add);
            this.panel2.Controls.Add(this.txt_ipaddress2);
            this.panel2.Controls.Add(this.txt_ipaddress1);
            this.panel2.Controls.Add(this.txt_ipaddress);
            this.panel2.Controls.Add(this.txt_fixtureId);
            this.panel2.Controls.Add(this.txt_machinedesc);
            this.panel2.Controls.Add(this.txt_machineId);
            this.panel2.Controls.Add(this.cbx_lineId);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 230);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(803, 134);
            this.panel2.TabIndex = 2;
            // 
            // bt_delete
            // 
            this.bt_delete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_delete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_delete.Location = new System.Drawing.Point(656, 37);
            this.bt_delete.Name = "bt_delete";
            this.bt_delete.Size = new System.Drawing.Size(75, 23);
            this.bt_delete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_delete.TabIndex = 3;
            this.bt_delete.Text = "删除";
            this.bt_delete.Click += new System.EventHandler(this.bt_delete_Click);
            // 
            // bt_add
            // 
            this.bt_add.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_add.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_add.Location = new System.Drawing.Point(547, 37);
            this.bt_add.Name = "bt_add";
            this.bt_add.Size = new System.Drawing.Size(75, 23);
            this.bt_add.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_add.TabIndex = 3;
            this.bt_add.Text = "增加";
            this.bt_add.Click += new System.EventHandler(this.bt_add_Click);
            // 
            // txt_ipaddress2
            // 
            // 
            // 
            // 
            this.txt_ipaddress2.Border.Class = "TextBoxBorder";
            this.txt_ipaddress2.Location = new System.Drawing.Point(656, 94);
            this.txt_ipaddress2.Name = "txt_ipaddress2";
            this.txt_ipaddress2.Size = new System.Drawing.Size(121, 21);
            this.txt_ipaddress2.TabIndex = 2;
            // 
            // txt_ipaddress1
            // 
            // 
            // 
            // 
            this.txt_ipaddress1.Border.Class = "TextBoxBorder";
            this.txt_ipaddress1.Location = new System.Drawing.Point(514, 94);
            this.txt_ipaddress1.Name = "txt_ipaddress1";
            this.txt_ipaddress1.Size = new System.Drawing.Size(121, 21);
            this.txt_ipaddress1.TabIndex = 2;
            // 
            // txt_ipaddress
            // 
            // 
            // 
            // 
            this.txt_ipaddress.Border.Class = "TextBoxBorder";
            this.txt_ipaddress.Location = new System.Drawing.Point(361, 94);
            this.txt_ipaddress.Name = "txt_ipaddress";
            this.txt_ipaddress.Size = new System.Drawing.Size(121, 21);
            this.txt_ipaddress.TabIndex = 2;
            // 
            // txt_fixtureId
            // 
            // 
            // 
            // 
            this.txt_fixtureId.Border.Class = "TextBoxBorder";
            this.txt_fixtureId.Location = new System.Drawing.Point(214, 39);
            this.txt_fixtureId.Name = "txt_fixtureId";
            this.txt_fixtureId.Size = new System.Drawing.Size(100, 21);
            this.txt_fixtureId.TabIndex = 2;
            // 
            // txt_machinedesc
            // 
            // 
            // 
            // 
            this.txt_machinedesc.Border.Class = "TextBoxBorder";
            this.txt_machinedesc.Location = new System.Drawing.Point(28, 94);
            this.txt_machinedesc.Name = "txt_machinedesc";
            this.txt_machinedesc.Size = new System.Drawing.Size(286, 21);
            this.txt_machinedesc.TabIndex = 2;
            // 
            // txt_machineId
            // 
            // 
            // 
            // 
            this.txt_machineId.Border.Class = "TextBoxBorder";
            this.txt_machineId.Location = new System.Drawing.Point(28, 39);
            this.txt_machineId.Name = "txt_machineId";
            this.txt_machineId.Size = new System.Drawing.Size(130, 21);
            this.txt_machineId.TabIndex = 2;
            // 
            // cbx_lineId
            // 
            this.cbx_lineId.DisplayMember = "Text";
            this.cbx_lineId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbx_lineId.FormattingEnabled = true;
            this.cbx_lineId.ItemHeight = 15;
            this.cbx_lineId.Location = new System.Drawing.Point(361, 39);
            this.cbx_lineId.Name = "cbx_lineId";
            this.cbx_lineId.Size = new System.Drawing.Size(121, 21);
            this.cbx_lineId.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbx_lineId.TabIndex = 1;
            this.cbx_lineId.DropDown += new System.EventHandler(this.cb_lineId_DropDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(654, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "[IpAddr_3]:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "[设备编号]:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(512, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "[IpAddr_2]:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "[描述]:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(359, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "[IpAddr_1]:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(359, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "[生产线编号]:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[机器/工站编号]:";
            // 
            // MachineInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 364);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MachineInfo";
            this.Text = "MachineInfo";
            this.Load += new System.EventHandler(this.MachineInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showmachineInfo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_showmachineInfo;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_ipaddress1;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_ipaddress;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_fixtureId;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_machinedesc;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_machineId;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbx_lineId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX bt_delete;
        private DevComponents.DotNetBar.ButtonX bt_add;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_ipaddress2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineId;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineId;
        private System.Windows.Forms.DataGridViewTextBoxColumn fixtureId;
        private System.Windows.Forms.DataGridViewTextBoxColumn machinedesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn IpAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn IpAddress1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IpAddress2;
    }
}