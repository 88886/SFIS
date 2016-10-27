namespace SFIS_V2
{
    partial class PrintSFCSn
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_woid = new System.Windows.Forms.ComboBox();
            this.tb_snprefix = new System.Windows.Forms.TextBox();
            this.tb_postfix = new System.Windows.Forms.TextBox();
            this.num_printnum = new System.Windows.Forms.NumericUpDown();
            this.lb_section = new System.Windows.Forms.Label();
            this.bt_print = new DevComponents.DotNetBar.ButtonX();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_facname = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.bt_reprint = new DevComponents.DotNetBar.ButtonX();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_pring_esn = new System.Windows.Forms.TextBox();
            this.rb_printmodel2 = new System.Windows.Forms.RadioButton();
            this.rb_printmodel1 = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.rtb_logmsg = new System.Windows.Forms.RichTextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.bt_rPrinter = new DevComponents.DotNetBar.ButtonX();
            this.label10 = new System.Windows.Forms.Label();
            this.lsesn = new System.Windows.Forms.ListBox();
            this.tbotherSn = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.num_printnum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[工单号]:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "[前固定码]:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "[后固定码]:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "[打印数量]:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "[区   间]:";
            // 
            // cb_woid
            // 
            this.cb_woid.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_woid.FormattingEnabled = true;
            this.cb_woid.Location = new System.Drawing.Point(112, 83);
            this.cb_woid.Name = "cb_woid";
            this.cb_woid.Size = new System.Drawing.Size(141, 24);
            this.cb_woid.TabIndex = 2;
            this.cb_woid.SelectedIndexChanged += new System.EventHandler(this.cb_woid_SelectedIndexChanged);
            this.cb_woid.Leave += new System.EventHandler(this.cb_woid_Leave);
            // 
            // tb_snprefix
            // 
            this.tb_snprefix.Enabled = false;
            this.tb_snprefix.Location = new System.Drawing.Point(112, 122);
            this.tb_snprefix.Name = "tb_snprefix";
            this.tb_snprefix.Size = new System.Drawing.Size(140, 21);
            this.tb_snprefix.TabIndex = 3;
            // 
            // tb_postfix
            // 
            this.tb_postfix.Enabled = false;
            this.tb_postfix.Location = new System.Drawing.Point(112, 151);
            this.tb_postfix.Name = "tb_postfix";
            this.tb_postfix.Size = new System.Drawing.Size(140, 21);
            this.tb_postfix.TabIndex = 3;
            // 
            // num_printnum
            // 
            this.num_printnum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_printnum.Location = new System.Drawing.Point(112, 205);
            this.num_printnum.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.num_printnum.Name = "num_printnum";
            this.num_printnum.Size = new System.Drawing.Size(82, 21);
            this.num_printnum.TabIndex = 4;
            this.num_printnum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.num_printnum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lb_section
            // 
            this.lb_section.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_section.Location = new System.Drawing.Point(37, 279);
            this.lb_section.Name = "lb_section";
            this.lb_section.Size = new System.Drawing.Size(217, 30);
            this.lb_section.TabIndex = 5;
            this.lb_section.Text = "序号区间";
            this.lb_section.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bt_print
            // 
            this.bt_print.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_print.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_print.Location = new System.Drawing.Point(209, 206);
            this.bt_print.Name = "bt_print";
            this.bt_print.Size = new System.Drawing.Size(43, 23);
            this.bt_print.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_print.TabIndex = 7;
            this.bt_print.Text = "打印";
            this.bt_print.Click += new System.EventHandler(this.bt_print_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "[工厂]:";
            // 
            // cb_facname
            // 
            this.cb_facname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_facname.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_facname.FormattingEnabled = true;
            this.cb_facname.Location = new System.Drawing.Point(113, 48);
            this.cb_facname.Name = "cb_facname";
            this.cb_facname.Size = new System.Drawing.Size(141, 24);
            this.cb_facname.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(815, 483);
            this.splitContainer1.SplitterDistance = 318;
            this.splitContainer1.TabIndex = 8;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.bt_reprint);
            this.splitContainer2.Panel1.Controls.Add(this.label11);
            this.splitContainer2.Panel1.Controls.Add(this.txt_pring_esn);
            this.splitContainer2.Panel1.Controls.Add(this.rb_printmodel2);
            this.splitContainer2.Panel1.Controls.Add(this.rb_printmodel1);
            this.splitContainer2.Panel1.Controls.Add(this.label8);
            this.splitContainer2.Panel1.Controls.Add(this.tb_snprefix);
            this.splitContainer2.Panel1.Controls.Add(this.num_printnum);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.bt_print);
            this.splitContainer2.Panel1.Controls.Add(this.tb_postfix);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.lb_section);
            this.splitContainer2.Panel1.Controls.Add(this.cb_woid);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.label6);
            this.splitContainer2.Panel1.Controls.Add(this.cb_facname);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rtb_logmsg);
            this.splitContainer2.Size = new System.Drawing.Size(318, 483);
            this.splitContainer2.SplitterDistance = 318;
            this.splitContainer2.TabIndex = 0;
            // 
            // bt_reprint
            // 
            this.bt_reprint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_reprint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_reprint.Location = new System.Drawing.Point(209, 244);
            this.bt_reprint.Name = "bt_reprint";
            this.bt_reprint.Size = new System.Drawing.Size(43, 23);
            this.bt_reprint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_reprint.TabIndex = 12;
            this.bt_reprint.Text = "补印";
            this.bt_reprint.Click += new System.EventHandler(this.bt_reprint_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(37, 182);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 11;
            this.label11.Text = "[补印ESN]:";
            // 
            // txt_pring_esn
            // 
            this.txt_pring_esn.Location = new System.Drawing.Point(112, 179);
            this.txt_pring_esn.Name = "txt_pring_esn";
            this.txt_pring_esn.Size = new System.Drawing.Size(140, 21);
            this.txt_pring_esn.TabIndex = 10;
            this.txt_pring_esn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_pring_esn_KeyDown);
            // 
            // rb_printmodel2
            // 
            this.rb_printmodel2.AutoSize = true;
            this.rb_printmodel2.Location = new System.Drawing.Point(112, 252);
            this.rb_printmodel2.Name = "rb_printmodel2";
            this.rb_printmodel2.Size = new System.Drawing.Size(77, 16);
            this.rb_printmodel2.TabIndex = 9;
            this.rb_printmodel2.Text = "打印方式2";
            this.rb_printmodel2.UseVisualStyleBackColor = true;
            this.rb_printmodel2.CheckedChanged += new System.EventHandler(this.rb_printmodel2_CheckedChanged);
            // 
            // rb_printmodel1
            // 
            this.rb_printmodel1.AutoSize = true;
            this.rb_printmodel1.Checked = true;
            this.rb_printmodel1.Location = new System.Drawing.Point(112, 229);
            this.rb_printmodel1.Name = "rb_printmodel1";
            this.rb_printmodel1.Size = new System.Drawing.Size(77, 16);
            this.rb_printmodel1.TabIndex = 9;
            this.rb_printmodel1.TabStop = true;
            this.rb_printmodel1.Text = "打印方式1";
            this.rb_printmodel1.UseVisualStyleBackColor = true;
            this.rb_printmodel1.CheckedChanged += new System.EventHandler(this.rb_printmodel1_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label8.Location = new System.Drawing.Point(15, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(285, 28);
            this.label8.TabIndex = 8;
            this.label8.Text = "SFC系统产品跟踪标签打印";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtb_logmsg
            // 
            this.rtb_logmsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_logmsg.Location = new System.Drawing.Point(0, 0);
            this.rtb_logmsg.Name = "rtb_logmsg";
            this.rtb_logmsg.Size = new System.Drawing.Size(318, 161);
            this.rtb_logmsg.TabIndex = 0;
            this.rtb_logmsg.Text = "";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.bt_rPrinter);
            this.splitContainer3.Panel1.Controls.Add(this.label10);
            this.splitContainer3.Panel1.Controls.Add(this.lsesn);
            this.splitContainer3.Panel1.Controls.Add(this.tbotherSn);
            this.splitContainer3.Panel1.Controls.Add(this.label9);
            this.splitContainer3.Panel1.Controls.Add(this.label7);
            this.splitContainer3.Size = new System.Drawing.Size(493, 483);
            this.splitContainer3.SplitterDistance = 216;
            this.splitContainer3.TabIndex = 0;
            // 
            // bt_rPrinter
            // 
            this.bt_rPrinter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_rPrinter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_rPrinter.Location = new System.Drawing.Point(143, 195);
            this.bt_rPrinter.Name = "bt_rPrinter";
            this.bt_rPrinter.Size = new System.Drawing.Size(55, 21);
            this.bt_rPrinter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_rPrinter.TabIndex = 5;
            this.bt_rPrinter.Text = "打印";
            this.bt_rPrinter.Visible = false;
            this.bt_rPrinter.Click += new System.EventHandler(this.bt_rPrinter_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 4;
            this.label10.Text = "[ESN]:";
            this.label10.Visible = false;
            // 
            // lsesn
            // 
            this.lsesn.FormattingEnabled = true;
            this.lsesn.ItemHeight = 12;
            this.lsesn.Location = new System.Drawing.Point(18, 99);
            this.lsesn.Name = "lsesn";
            this.lsesn.Size = new System.Drawing.Size(180, 88);
            this.lsesn.TabIndex = 3;
            this.lsesn.Visible = false;
            // 
            // tbotherSn
            // 
            this.tbotherSn.Location = new System.Drawing.Point(18, 54);
            this.tbotherSn.Name = "tbotherSn";
            this.tbotherSn.Size = new System.Drawing.Size(160, 21);
            this.tbotherSn.TabIndex = 2;
            this.tbotherSn.Visible = false;
            this.tbotherSn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbotherSn_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "[序列号]:";
            this.label9.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label7.Location = new System.Drawing.Point(38, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "ESN查询&&重印";
            this.label7.Visible = false;
            // 
            // PrintSFCSn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 483);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PrintSFCSn";
            this.Text = "产品跟踪序号打印";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrintSFCSn_FormClosing);
            this.Load += new System.EventHandler(this.PrintSFCSn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_printnum)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_woid;
        private System.Windows.Forms.TextBox tb_snprefix;
        private System.Windows.Forms.TextBox tb_postfix;
        private System.Windows.Forms.NumericUpDown num_printnum;
        private System.Windows.Forms.Label lb_section;
        private DevComponents.DotNetBar.ButtonX bt_print;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_facname;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox rtb_logmsg;
        private System.Windows.Forms.RadioButton rb_printmodel2;
        private System.Windows.Forms.RadioButton rb_printmodel1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox tbotherSn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox lsesn;
        private DevComponents.DotNetBar.ButtonX bt_rPrinter;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_pring_esn;
        private DevComponents.DotNetBar.ButtonX bt_reprint;
    }
}