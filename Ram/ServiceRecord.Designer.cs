namespace Ram
{
    partial class ServiceRecord
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
            this.btn_reset = new System.Windows.Forms.Button();
            this.Btn_Setting = new System.Windows.Forms.Button();
            this.Txt_UserId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Btn_SubMit = new System.Windows.Forms.Button();
            this.Txt_BadCause = new System.Windows.Forms.TextBox();
            this.Txt_BadDevice = new System.Windows.Forms.TextBox();
            this.Txt_Mac = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Rdb_SN = new System.Windows.Forms.RadioButton();
            this.Rdb_Mac = new System.Windows.Forms.RadioButton();
            this.lbl_status = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.Txt_BadLoca = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_reset);
            this.groupBox1.Controls.Add(this.Btn_Setting);
            this.groupBox1.Controls.Add(this.Txt_UserId);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 59);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(562, 18);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 11;
            this.btn_reset.Text = "重置";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // Btn_Setting
            // 
            this.Btn_Setting.Location = new System.Drawing.Point(453, 18);
            this.Btn_Setting.Name = "Btn_Setting";
            this.Btn_Setting.Size = new System.Drawing.Size(75, 23);
            this.Btn_Setting.TabIndex = 10;
            this.Btn_Setting.Text = "设置";
            this.Btn_Setting.UseVisualStyleBackColor = true;
            this.Btn_Setting.Click += new System.EventHandler(this.Btn_Setting_Click);
            // 
            // Txt_UserId
            // 
            this.Txt_UserId.Location = new System.Drawing.Point(123, 20);
            this.Txt_UserId.Name = "Txt_UserId";
            this.Txt_UserId.Size = new System.Drawing.Size(265, 21);
            this.Txt_UserId.TabIndex = 5;
            this.Txt_UserId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_UserId_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "UserID：";
            // 
            // Btn_SubMit
            // 
            this.Btn_SubMit.Location = new System.Drawing.Point(578, 330);
            this.Btn_SubMit.Name = "Btn_SubMit";
            this.Btn_SubMit.Size = new System.Drawing.Size(75, 23);
            this.Btn_SubMit.TabIndex = 16;
            this.Btn_SubMit.Text = "提交";
            this.Btn_SubMit.UseVisualStyleBackColor = true;
            this.Btn_SubMit.Click += new System.EventHandler(this.Btn_SubMit_Click);
            // 
            // Txt_BadCause
            // 
            this.Txt_BadCause.Location = new System.Drawing.Point(123, 54);
            this.Txt_BadCause.Multiline = true;
            this.Txt_BadCause.Name = "Txt_BadCause";
            this.Txt_BadCause.Size = new System.Drawing.Size(388, 79);
            this.Txt_BadCause.TabIndex = 15;
            this.Txt_BadCause.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_BadCause_KeyDown);
            // 
            // Txt_BadDevice
            // 
            this.Txt_BadDevice.Location = new System.Drawing.Point(123, 139);
            this.Txt_BadDevice.Multiline = true;
            this.Txt_BadDevice.Name = "Txt_BadDevice";
            this.Txt_BadDevice.Size = new System.Drawing.Size(388, 111);
            this.Txt_BadDevice.TabIndex = 14;
            this.Txt_BadDevice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_BadDevice_KeyDown);
            // 
            // Txt_Mac
            // 
            this.Txt_Mac.Location = new System.Drawing.Point(123, 20);
            this.Txt_Mac.Name = "Txt_Mac";
            this.Txt_Mac.Size = new System.Drawing.Size(388, 21);
            this.Txt_Mac.TabIndex = 13;
            this.Txt_Mac.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServiceRecord_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "不良原因：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "测试状况：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "Mac：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(740, 636);
            this.splitContainer1.SplitterDistance = 59;
            this.splitContainer1.TabIndex = 18;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 441);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(740, 132);
            this.dataGridView1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Rdb_SN);
            this.groupBox2.Controls.Add(this.Rdb_Mac);
            this.groupBox2.Controls.Add(this.lbl_status);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.Txt_BadLoca);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.Btn_SubMit);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.Txt_BadCause);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Txt_BadDevice);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Txt_Mac);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 431);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "填入信息";
            // 
            // Rdb_SN
            // 
            this.Rdb_SN.AutoSize = true;
            this.Rdb_SN.Location = new System.Drawing.Point(564, 21);
            this.Rdb_SN.Name = "Rdb_SN";
            this.Rdb_SN.Size = new System.Drawing.Size(35, 16);
            this.Rdb_SN.TabIndex = 25;
            this.Rdb_SN.TabStop = true;
            this.Rdb_SN.Text = "SN";
            this.Rdb_SN.UseVisualStyleBackColor = true;
            // 
            // Rdb_Mac
            // 
            this.Rdb_Mac.AutoSize = true;
            this.Rdb_Mac.Location = new System.Drawing.Point(517, 21);
            this.Rdb_Mac.Name = "Rdb_Mac";
            this.Rdb_Mac.Size = new System.Drawing.Size(41, 16);
            this.Rdb_Mac.TabIndex = 24;
            this.Rdb_Mac.TabStop = true;
            this.Rdb_Mac.Text = "MAC";
            this.Rdb_Mac.UseVisualStyleBackColor = true;
            this.Rdb_Mac.CheckedChanged += new System.EventHandler(this.Rdb_Mac_CheckedChanged);
            // 
            // lbl_status
            // 
            this.lbl_status.Location = new System.Drawing.Point(123, 363);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.ReadOnly = true;
            this.lbl_status.Size = new System.Drawing.Size(530, 62);
            this.lbl_status.TabIndex = 23;
            this.lbl_status.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 363);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "状态信息：";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(517, 272);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(15, 14);
            this.checkBox3.TabIndex = 21;
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // Txt_BadLoca
            // 
            this.Txt_BadLoca.Location = new System.Drawing.Point(123, 256);
            this.Txt_BadLoca.Multiline = true;
            this.Txt_BadLoca.Name = "Txt_BadLoca";
            this.Txt_BadLoca.Size = new System.Drawing.Size(388, 97);
            this.Txt_BadLoca.TabIndex = 20;
            this.Txt_BadLoca.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_BadLoca_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 259);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "不良位置：";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(517, 166);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 18;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(517, 57);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ServiceRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 636);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ServiceRecord";
            this.Text = "维修信息录入";
            this.Load += new System.EventHandler(this.ServiceRecord_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_Setting;
        private System.Windows.Forms.TextBox Txt_UserId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Btn_SubMit;
        private System.Windows.Forms.TextBox Txt_BadCause;
        private System.Windows.Forms.TextBox Txt_BadDevice;
        private System.Windows.Forms.TextBox Txt_Mac;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TextBox Txt_BadLoca;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox lbl_status;
        private System.Windows.Forms.RadioButton Rdb_SN;
        private System.Windows.Forms.RadioButton Rdb_Mac;
    }
}