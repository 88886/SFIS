namespace Ram
{
    partial class TestRecord
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
            this.Btn_Reset = new System.Windows.Forms.Button();
            this.Btn_Setting = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_UserID = new System.Windows.Forms.TextBox();
            this.Btn_Submit = new System.Windows.Forms.Button();
            this.Txt_BadCause = new System.Windows.Forms.TextBox();
            this.Txt_Mac = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Rdb_Mac = new System.Windows.Forms.RadioButton();
            this.Rdb_SN = new System.Windows.Forms.RadioButton();
            this.Lbl_Status = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Btn_Reset);
            this.groupBox1.Controls.Add(this.Btn_Setting);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Txt_UserID);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 52);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // Btn_Reset
            // 
            this.Btn_Reset.Location = new System.Drawing.Point(506, 18);
            this.Btn_Reset.Name = "Btn_Reset";
            this.Btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.Btn_Reset.TabIndex = 9;
            this.Btn_Reset.Text = "重置";
            this.Btn_Reset.UseVisualStyleBackColor = true;
            this.Btn_Reset.Click += new System.EventHandler(this.Btn_Reset_Click);
            // 
            // Btn_Setting
            // 
            this.Btn_Setting.Location = new System.Drawing.Point(424, 18);
            this.Btn_Setting.Name = "Btn_Setting";
            this.Btn_Setting.Size = new System.Drawing.Size(75, 23);
            this.Btn_Setting.TabIndex = 8;
            this.Btn_Setting.Text = "设置";
            this.Btn_Setting.UseVisualStyleBackColor = true;
            this.Btn_Setting.Click += new System.EventHandler(this.Btn_Setting_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "工号：";
            // 
            // Txt_UserID
            // 
            this.Txt_UserID.Location = new System.Drawing.Point(115, 20);
            this.Txt_UserID.Name = "Txt_UserID";
            this.Txt_UserID.Size = new System.Drawing.Size(286, 21);
            this.Txt_UserID.TabIndex = 5;
            this.Txt_UserID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_UserID_KeyDown);
            // 
            // Btn_Submit
            // 
            this.Btn_Submit.Location = new System.Drawing.Point(428, 53);
            this.Btn_Submit.Name = "Btn_Submit";
            this.Btn_Submit.Size = new System.Drawing.Size(75, 23);
            this.Btn_Submit.TabIndex = 12;
            this.Btn_Submit.Text = "提交";
            this.Btn_Submit.UseVisualStyleBackColor = true;
            this.Btn_Submit.Click += new System.EventHandler(this.Btn_Submit_Click);
            // 
            // Txt_BadCause
            // 
            this.Txt_BadCause.Location = new System.Drawing.Point(115, 55);
            this.Txt_BadCause.Multiline = true;
            this.Txt_BadCause.Name = "Txt_BadCause";
            this.Txt_BadCause.Size = new System.Drawing.Size(286, 99);
            this.Txt_BadCause.TabIndex = 11;
            this.Txt_BadCause.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_BadCause_KeyDown);
            // 
            // Txt_Mac
            // 
            this.Txt_Mac.Location = new System.Drawing.Point(115, 28);
            this.Txt_Mac.Name = "Txt_Mac";
            this.Txt_Mac.Size = new System.Drawing.Size(286, 21);
            this.Txt_Mac.TabIndex = 10;
            this.Txt_Mac.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_Sn_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "测试状况：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "MAC：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Rdb_Mac);
            this.groupBox2.Controls.Add(this.Rdb_SN);
            this.groupBox2.Controls.Add(this.Lbl_Status);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Btn_Submit);
            this.groupBox2.Controls.Add(this.Txt_Mac);
            this.groupBox2.Controls.Add(this.Txt_BadCause);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(681, 253);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "检测信息";
            // 
            // Rdb_Mac
            // 
            this.Rdb_Mac.AutoSize = true;
            this.Rdb_Mac.Location = new System.Drawing.Point(407, 29);
            this.Rdb_Mac.Name = "Rdb_Mac";
            this.Rdb_Mac.Size = new System.Drawing.Size(41, 16);
            this.Rdb_Mac.TabIndex = 17;
            this.Rdb_Mac.TabStop = true;
            this.Rdb_Mac.Text = "MAC";
            this.Rdb_Mac.UseVisualStyleBackColor = true;
            this.Rdb_Mac.CheckedChanged += new System.EventHandler(this.Rdb_Mac_CheckedChanged);
            // 
            // Rdb_SN
            // 
            this.Rdb_SN.AutoSize = true;
            this.Rdb_SN.Location = new System.Drawing.Point(454, 29);
            this.Rdb_SN.Name = "Rdb_SN";
            this.Rdb_SN.Size = new System.Drawing.Size(35, 16);
            this.Rdb_SN.TabIndex = 16;
            this.Rdb_SN.TabStop = true;
            this.Rdb_SN.Text = "SN";
            this.Rdb_SN.UseVisualStyleBackColor = true;
            // 
            // Lbl_Status
            // 
            this.Lbl_Status.Location = new System.Drawing.Point(115, 160);
            this.Lbl_Status.Name = "Lbl_Status";
            this.Lbl_Status.ReadOnly = true;
            this.Lbl_Status.Size = new System.Drawing.Size(388, 87);
            this.Lbl_Status.TabIndex = 15;
            this.Lbl_Status.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "状态：";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(407, 58);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
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
            this.splitContainer1.Size = new System.Drawing.Size(681, 484);
            this.splitContainer1.SplitterDistance = 52;
            this.splitContainer1.TabIndex = 15;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 261);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(681, 167);
            this.dataGridView1.TabIndex = 15;
            // 
            // TestRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 484);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TestRecord";
            this.Text = "测试信息录入";
            this.Load += new System.EventHandler(this.TestRecord_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_Setting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_UserID;
        private System.Windows.Forms.Button Btn_Submit;
        private System.Windows.Forms.TextBox Txt_BadCause;
        private System.Windows.Forms.TextBox Txt_Mac;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Reset;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox Lbl_Status;
        private System.Windows.Forms.RadioButton Rdb_Mac;
        private System.Windows.Forms.RadioButton Rdb_SN;
    }
}