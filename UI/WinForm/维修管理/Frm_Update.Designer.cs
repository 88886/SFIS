namespace SFIS_V2
{
    partial class Frm_Update
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_memo = new System.Windows.Forms.TextBox();
            this.imbt_browserduty = new DevComponents.DotNetBar.ButtonX();
            this.imbt_browserRC = new DevComponents.DotNetBar.ButtonX();
            this.tb_DutyDesc = new System.Windows.Forms.TextBox();
            this.tb_ReasonCodeDesc = new System.Windows.Forms.TextBox();
            this.tb_Locataion = new System.Windows.Forms.TextBox();
            this.tb_Duty = new System.Windows.Forms.TextBox();
            this.tb_reasoncode = new System.Windows.Forms.TextBox();
            this.LabTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Chk_AutoCommit = new System.Windows.Forms.CheckBox();
            this.imbt_CommitMaterial = new DevComponents.DotNetBar.ButtonX();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_LotNo = new System.Windows.Forms.TextBox();
            this.tb_DateCode = new System.Windows.Forms.TextBox();
            this.tb_Vender = new System.Windows.Forms.TextBox();
            this.tb_Partnumber = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_trsn = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvMaterial = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.imbt_Cancel = new DevComponents.DotNetBar.ButtonX();
            this.imbt_OK = new DevComponents.DotNetBar.ButtonX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_memo);
            this.groupBox1.Controls.Add(this.imbt_browserduty);
            this.groupBox1.Controls.Add(this.imbt_browserRC);
            this.groupBox1.Controls.Add(this.tb_DutyDesc);
            this.groupBox1.Controls.Add(this.tb_ReasonCodeDesc);
            this.groupBox1.Controls.Add(this.tb_Locataion);
            this.groupBox1.Controls.Add(this.tb_Duty);
            this.groupBox1.Controls.Add(this.tb_reasoncode);
            this.groupBox1.Controls.Add(this.LabTime);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 396);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tb_memo
            // 
            this.tb_memo.Location = new System.Drawing.Point(139, 283);
            this.tb_memo.Multiline = true;
            this.tb_memo.Name = "tb_memo";
            this.tb_memo.Size = new System.Drawing.Size(237, 107);
            this.tb_memo.TabIndex = 4;
            // 
            // imbt_browserduty
            // 
            this.imbt_browserduty.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_browserduty.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_browserduty.Location = new System.Drawing.Point(326, 192);
            this.imbt_browserduty.Name = "imbt_browserduty";
            this.imbt_browserduty.Size = new System.Drawing.Size(33, 23);
            this.imbt_browserduty.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_browserduty.TabIndex = 3;
            this.imbt_browserduty.Click += new System.EventHandler(this.imbt_browserduty_Click);
            // 
            // imbt_browserRC
            // 
            this.imbt_browserRC.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_browserRC.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_browserRC.Location = new System.Drawing.Point(326, 65);
            this.imbt_browserRC.Name = "imbt_browserRC";
            this.imbt_browserRC.Size = new System.Drawing.Size(33, 23);
            this.imbt_browserRC.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_browserRC.TabIndex = 3;
            this.imbt_browserRC.Click += new System.EventHandler(this.imbt_browserRC_Click);
            // 
            // tb_DutyDesc
            // 
            this.tb_DutyDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tb_DutyDesc.Enabled = false;
            this.tb_DutyDesc.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_DutyDesc.Location = new System.Drawing.Point(139, 235);
            this.tb_DutyDesc.Name = "tb_DutyDesc";
            this.tb_DutyDesc.Size = new System.Drawing.Size(237, 23);
            this.tb_DutyDesc.TabIndex = 2;
            // 
            // tb_ReasonCodeDesc
            // 
            this.tb_ReasonCodeDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tb_ReasonCodeDesc.Enabled = false;
            this.tb_ReasonCodeDesc.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_ReasonCodeDesc.Location = new System.Drawing.Point(139, 107);
            this.tb_ReasonCodeDesc.Name = "tb_ReasonCodeDesc";
            this.tb_ReasonCodeDesc.Size = new System.Drawing.Size(237, 23);
            this.tb_ReasonCodeDesc.TabIndex = 2;
            // 
            // tb_Locataion
            // 
            this.tb_Locataion.BackColor = System.Drawing.Color.White;
            this.tb_Locataion.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_Locataion.Location = new System.Drawing.Point(139, 148);
            this.tb_Locataion.Name = "tb_Locataion";
            this.tb_Locataion.Size = new System.Drawing.Size(167, 23);
            this.tb_Locataion.TabIndex = 2;
            this.tb_Locataion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_reasoncode_KeyDown);
            // 
            // tb_Duty
            // 
            this.tb_Duty.BackColor = System.Drawing.Color.Yellow;
            this.tb_Duty.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_Duty.Location = new System.Drawing.Point(139, 191);
            this.tb_Duty.Name = "tb_Duty";
            this.tb_Duty.Size = new System.Drawing.Size(167, 23);
            this.tb_Duty.TabIndex = 2;
            this.tb_Duty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Duty_KeyDown);
            this.tb_Duty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_Duty_KeyPress);
            this.tb_Duty.Leave += new System.EventHandler(this.tb_Duty_Leave);
            // 
            // tb_reasoncode
            // 
            this.tb_reasoncode.BackColor = System.Drawing.Color.Yellow;
            this.tb_reasoncode.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_reasoncode.Location = new System.Drawing.Point(141, 64);
            this.tb_reasoncode.Name = "tb_reasoncode";
            this.tb_reasoncode.Size = new System.Drawing.Size(167, 23);
            this.tb_reasoncode.TabIndex = 2;
            this.tb_reasoncode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_reasoncode_KeyDown);
            this.tb_reasoncode.Leave += new System.EventHandler(this.tb_reasoncode_Leave);
            // 
            // LabTime
            // 
            this.LabTime.AutoSize = true;
            this.LabTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabTime.Location = new System.Drawing.Point(139, 31);
            this.LabTime.Name = "LabTime";
            this.LabTime.Size = new System.Drawing.Size(62, 16);
            this.LabTime.TabIndex = 1;
            this.LabTime.Text = "label2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(82, 281);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "备注:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(48, 237);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "责任描述:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(48, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "责任单位:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(48, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "零件位置:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(48, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "原因描述:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(48, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "原因代码:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(46, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "维修时间:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 396);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(394, 240);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Chk_AutoCommit);
            this.panel3.Controls.Add(this.imbt_CommitMaterial);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.tb_LotNo);
            this.panel3.Controls.Add(this.tb_DateCode);
            this.panel3.Controls.Add(this.tb_Vender);
            this.panel3.Controls.Add(this.tb_Partnumber);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 59);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(388, 178);
            this.panel3.TabIndex = 2;
            // 
            // Chk_AutoCommit
            // 
            this.Chk_AutoCommit.AutoSize = true;
            this.Chk_AutoCommit.Location = new System.Drawing.Point(141, 147);
            this.Chk_AutoCommit.Name = "Chk_AutoCommit";
            this.Chk_AutoCommit.Size = new System.Drawing.Size(72, 16);
            this.Chk_AutoCommit.TabIndex = 2;
            this.Chk_AutoCommit.Text = "直接提交";
            this.Chk_AutoCommit.UseVisualStyleBackColor = true;
            // 
            // imbt_CommitMaterial
            // 
            this.imbt_CommitMaterial.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_CommitMaterial.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_CommitMaterial.ForeColor = System.Drawing.Color.DarkBlue;
            this.imbt_CommitMaterial.Location = new System.Drawing.Point(241, 138);
            this.imbt_CommitMaterial.Name = "imbt_CommitMaterial";
            this.imbt_CommitMaterial.Size = new System.Drawing.Size(115, 34);
            this.imbt_CommitMaterial.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_CommitMaterial.TabIndex = 1;
            this.imbt_CommitMaterial.Text = "提交物料信息";
            this.imbt_CommitMaterial.Click += new System.EventHandler(this.imbt_CommitMaterial_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Navy;
            this.label12.Location = new System.Drawing.Point(17, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 16);
            this.label12.TabIndex = 0;
            this.label12.Text = "生产批次:";
            // 
            // tb_LotNo
            // 
            this.tb_LotNo.Location = new System.Drawing.Point(109, 106);
            this.tb_LotNo.Name = "tb_LotNo";
            this.tb_LotNo.Size = new System.Drawing.Size(249, 21);
            this.tb_LotNo.TabIndex = 0;
            // 
            // tb_DateCode
            // 
            this.tb_DateCode.Location = new System.Drawing.Point(109, 73);
            this.tb_DateCode.Name = "tb_DateCode";
            this.tb_DateCode.Size = new System.Drawing.Size(249, 21);
            this.tb_DateCode.TabIndex = 0;
            // 
            // tb_Vender
            // 
            this.tb_Vender.Location = new System.Drawing.Point(110, 41);
            this.tb_Vender.Name = "tb_Vender";
            this.tb_Vender.Size = new System.Drawing.Size(249, 21);
            this.tb_Vender.TabIndex = 0;
            // 
            // tb_Partnumber
            // 
            this.tb_Partnumber.Location = new System.Drawing.Point(111, 10);
            this.tb_Partnumber.Name = "tb_Partnumber";
            this.tb_Partnumber.Size = new System.Drawing.Size(249, 21);
            this.tb_Partnumber.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.Navy;
            this.label11.Location = new System.Drawing.Point(17, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 16);
            this.label11.TabIndex = 0;
            this.label11.Text = "生产周期:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Navy;
            this.label10.Location = new System.Drawing.Point(19, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "厂商代码:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Navy;
            this.label9.Location = new System.Drawing.Point(52, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "料号:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.tb_trsn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(388, 42);
            this.panel2.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(38, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "物料条码:";
            // 
            // tb_trsn
            // 
            this.tb_trsn.Location = new System.Drawing.Point(111, 11);
            this.tb_trsn.Name = "tb_trsn";
            this.tb_trsn.Size = new System.Drawing.Size(249, 21);
            this.tb_trsn.TabIndex = 0;
            this.tb_trsn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_trsn_KeyDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.panel4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(394, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(411, 636);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvMaterial);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox4.Location = new System.Drawing.Point(3, 396);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(405, 150);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Repair Material";
            // 
            // dgvMaterial
            // 
            this.dgvMaterial.AllowUserToAddRows = false;
            this.dgvMaterial.AllowUserToDeleteRows = false;
            this.dgvMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterial.Location = new System.Drawing.Point(3, 19);
            this.dgvMaterial.Name = "dgvMaterial";
            this.dgvMaterial.ReadOnly = true;
            this.dgvMaterial.RowTemplate.Height = 23;
            this.dgvMaterial.Size = new System.Drawing.Size(399, 128);
            this.dgvMaterial.TabIndex = 0;
            this.dgvMaterial.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMaterial_CellMouseDoubleClick);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.imbt_Cancel);
            this.panel4.Controls.Add(this.imbt_OK);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 546);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(405, 87);
            this.panel4.TabIndex = 0;
            // 
            // imbt_Cancel
            // 
            this.imbt_Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_Cancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_Cancel.ForeColor = System.Drawing.Color.Blue;
            this.imbt_Cancel.Location = new System.Drawing.Point(228, 27);
            this.imbt_Cancel.Name = "imbt_Cancel";
            this.imbt_Cancel.Size = new System.Drawing.Size(75, 39);
            this.imbt_Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_Cancel.TabIndex = 0;
            this.imbt_Cancel.Text = "&Cancel";
            this.imbt_Cancel.Click += new System.EventHandler(this.imbt_Cancel_Click);
            // 
            // imbt_OK
            // 
            this.imbt_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_OK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_OK.ForeColor = System.Drawing.Color.Blue;
            this.imbt_OK.Location = new System.Drawing.Point(61, 27);
            this.imbt_OK.Name = "imbt_OK";
            this.imbt_OK.Size = new System.Drawing.Size(75, 39);
            this.imbt_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_OK.TabIndex = 0;
            this.imbt_OK.Text = "&OK";
            this.imbt_OK.Click += new System.EventHandler(this.imbt_OK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 636);
            this.panel1.TabIndex = 3;
            // 
            // Frm_Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 636);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Update";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modify";
            this.Load += new System.EventHandler(this.Frm_Update_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label LabTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_ReasonCodeDesc;
        private System.Windows.Forms.TextBox tb_reasoncode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX imbt_browserRC;
        private System.Windows.Forms.TextBox tb_Locataion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.ButtonX imbt_browserduty;
        private System.Windows.Forms.TextBox tb_DutyDesc;
        private System.Windows.Forms.TextBox tb_Duty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_memo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_trsn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_LotNo;
        private System.Windows.Forms.TextBox tb_DateCode;
        private System.Windows.Forms.TextBox tb_Vender;
        private System.Windows.Forms.TextBox tb_Partnumber;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private DevComponents.DotNetBar.ButtonX imbt_Cancel;
        private DevComponents.DotNetBar.ButtonX imbt_OK;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvMaterial;
        private System.Windows.Forms.CheckBox Chk_AutoCommit;
        private DevComponents.DotNetBar.ButtonX imbt_CommitMaterial;
    }
}