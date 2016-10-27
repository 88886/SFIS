namespace Ram
{
    partial class PackRecord
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
            this.Btn_Submit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_Reset = new System.Windows.Forms.Button();
            this.Btn_Set = new System.Windows.Forms.Button();
            this.Txt_UserId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Txt_NewSn = new System.Windows.Forms.TextBox();
            this.Txt_BoxNum = new System.Windows.Forms.TextBox();
            this.Txt_Mac = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Ckb_IsUse = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Txt_Num = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Txt_Filepath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Rdb_SN = new System.Windows.Forms.RadioButton();
            this.Rdb_Mac = new System.Windows.Forms.RadioButton();
            this.Lbl_Status = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
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
            // Btn_Submit
            // 
            this.Btn_Submit.Location = new System.Drawing.Point(466, 126);
            this.Btn_Submit.Name = "Btn_Submit";
            this.Btn_Submit.Size = new System.Drawing.Size(104, 23);
            this.Btn_Submit.TabIndex = 21;
            this.Btn_Submit.Text = "提交";
            this.Btn_Submit.UseVisualStyleBackColor = true;
            this.Btn_Submit.Click += new System.EventHandler(this.Btn_Submit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Btn_Reset);
            this.groupBox1.Controls.Add(this.Btn_Set);
            this.groupBox1.Controls.Add(this.Txt_UserId);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(661, 66);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // Btn_Reset
            // 
            this.Btn_Reset.Location = new System.Drawing.Point(521, 18);
            this.Btn_Reset.Name = "Btn_Reset";
            this.Btn_Reset.Size = new System.Drawing.Size(104, 23);
            this.Btn_Reset.TabIndex = 11;
            this.Btn_Reset.Text = "重置";
            this.Btn_Reset.UseVisualStyleBackColor = true;
            this.Btn_Reset.Click += new System.EventHandler(this.Btn_Reset_Click);
            // 
            // Btn_Set
            // 
            this.Btn_Set.Location = new System.Drawing.Point(411, 18);
            this.Btn_Set.Name = "Btn_Set";
            this.Btn_Set.Size = new System.Drawing.Size(104, 23);
            this.Btn_Set.TabIndex = 10;
            this.Btn_Set.Text = "设置";
            this.Btn_Set.UseVisualStyleBackColor = true;
            this.Btn_Set.Click += new System.EventHandler(this.Btn_Set_Click);
            // 
            // Txt_UserId
            // 
            this.Txt_UserId.Location = new System.Drawing.Point(88, 20);
            this.Txt_UserId.Name = "Txt_UserId";
            this.Txt_UserId.Size = new System.Drawing.Size(290, 21);
            this.Txt_UserId.TabIndex = 9;
            this.Txt_UserId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_UserId_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "工号：";
            // 
            // Txt_NewSn
            // 
            this.Txt_NewSn.Location = new System.Drawing.Point(112, 89);
            this.Txt_NewSn.Name = "Txt_NewSn";
            this.Txt_NewSn.Size = new System.Drawing.Size(272, 21);
            this.Txt_NewSn.TabIndex = 19;
            this.Txt_NewSn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_NewSn_KeyDown);
            // 
            // Txt_BoxNum
            // 
            this.Txt_BoxNum.Location = new System.Drawing.Point(112, 124);
            this.Txt_BoxNum.Name = "Txt_BoxNum";
            this.Txt_BoxNum.Size = new System.Drawing.Size(272, 21);
            this.Txt_BoxNum.TabIndex = 17;
            this.Txt_BoxNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_BoxNum_KeyDown);
            // 
            // Txt_Mac
            // 
            this.Txt_Mac.Location = new System.Drawing.Point(112, 57);
            this.Txt_Mac.Name = "Txt_Mac";
            this.Txt_Mac.Size = new System.Drawing.Size(272, 21);
            this.Txt_Mac.TabIndex = 16;
            this.Txt_Mac.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_Mac_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "箱号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "NewSn：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "Mac：";
            // 
            // Ckb_IsUse
            // 
            this.Ckb_IsUse.AutoSize = true;
            this.Ckb_IsUse.Location = new System.Drawing.Point(407, 131);
            this.Ckb_IsUse.Name = "Ckb_IsUse";
            this.Ckb_IsUse.Size = new System.Drawing.Size(15, 14);
            this.Ckb_IsUse.TabIndex = 22;
            this.Ckb_IsUse.UseVisualStyleBackColor = true;
            this.Ckb_IsUse.CheckedChanged += new System.EventHandler(this.Ckb_IsUse_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Btn_OK);
            this.groupBox2.Controls.Add(this.Txt_Num);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.Txt_Filepath);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.Rdb_SN);
            this.groupBox2.Controls.Add(this.Rdb_Mac);
            this.groupBox2.Controls.Add(this.Lbl_Status);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.Ckb_IsUse);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Btn_Submit);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.Txt_NewSn);
            this.groupBox2.Controls.Add(this.Txt_Mac);
            this.groupBox2.Controls.Add(this.Txt_BoxNum);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(661, 236);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "包装信息";
            // 
            // Btn_OK
            // 
            this.Btn_OK.Location = new System.Drawing.Point(596, 20);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(59, 23);
            this.Btn_OK.TabIndex = 34;
            this.Btn_OK.Text = "确定";
            this.Btn_OK.UseVisualStyleBackColor = true;
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Txt_Num
            // 
            this.Txt_Num.Location = new System.Drawing.Point(559, 22);
            this.Txt_Num.Name = "Txt_Num";
            this.Txt_Num.Size = new System.Drawing.Size(31, 21);
            this.Txt_Num.TabIndex = 33;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(488, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 32;
            this.label7.Text = "打印数量：";
            // 
            // Txt_Filepath
            // 
            this.Txt_Filepath.Location = new System.Drawing.Point(112, 22);
            this.Txt_Filepath.Name = "Txt_Filepath";
            this.Txt_Filepath.Size = new System.Drawing.Size(272, 21);
            this.Txt_Filepath.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 30;
            this.label6.Text = "模板文件路径：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(407, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "选择";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Rdb_SN
            // 
            this.Rdb_SN.AutoSize = true;
            this.Rdb_SN.Location = new System.Drawing.Point(455, 63);
            this.Rdb_SN.Name = "Rdb_SN";
            this.Rdb_SN.Size = new System.Drawing.Size(35, 16);
            this.Rdb_SN.TabIndex = 28;
            this.Rdb_SN.TabStop = true;
            this.Rdb_SN.Text = "SN";
            this.Rdb_SN.UseVisualStyleBackColor = true;
            // 
            // Rdb_Mac
            // 
            this.Rdb_Mac.AutoSize = true;
            this.Rdb_Mac.Location = new System.Drawing.Point(408, 63);
            this.Rdb_Mac.Name = "Rdb_Mac";
            this.Rdb_Mac.Size = new System.Drawing.Size(41, 16);
            this.Rdb_Mac.TabIndex = 27;
            this.Rdb_Mac.TabStop = true;
            this.Rdb_Mac.Text = "MAC";
            this.Rdb_Mac.UseVisualStyleBackColor = true;
            this.Rdb_Mac.CheckedChanged += new System.EventHandler(this.Rdb_Mac_CheckedChanged);
            // 
            // Lbl_Status
            // 
            this.Lbl_Status.Location = new System.Drawing.Point(112, 161);
            this.Lbl_Status.Name = "Lbl_Status";
            this.Lbl_Status.ReadOnly = true;
            this.Lbl_Status.Size = new System.Drawing.Size(458, 55);
            this.Lbl_Status.TabIndex = 26;
            this.Lbl_Status.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "状态：";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(408, 99);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 24;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
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
            this.splitContainer1.Size = new System.Drawing.Size(661, 445);
            this.splitContainer1.SplitterDistance = 66;
            this.splitContainer1.TabIndex = 24;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 242);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(661, 133);
            this.dataGridView1.TabIndex = 24;
            // 
            // PackRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 445);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PackRecord";
            this.Text = "包装信息录入";
            this.Load += new System.EventHandler(this.PackRecord_Load);
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

        private System.Windows.Forms.Button Btn_Submit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_Set;
        private System.Windows.Forms.TextBox Txt_UserId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Txt_NewSn;
        private System.Windows.Forms.TextBox Txt_BoxNum;
        private System.Windows.Forms.TextBox Txt_Mac;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Btn_Reset;
        private System.Windows.Forms.CheckBox Ckb_IsUse;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox Lbl_Status;
        private System.Windows.Forms.RadioButton Rdb_SN;
        private System.Windows.Forms.RadioButton Rdb_Mac;
        private System.Windows.Forms.TextBox Txt_Filepath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Txt_Num;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Btn_OK;
    }
}