namespace Frm_TEST_INPUT
{
    partial class Frm_TEST_INPUT
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ChkReprint = new System.Windows.Forms.CheckBox();
            this.numprint = new System.Windows.Forms.NumericUpDown();
            this.cb_Line = new System.Windows.Forms.ComboBox();
            this.tb_wo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_Input = new System.Windows.Forms.TextBox();
            this.LabRoute = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LabInput = new System.Windows.Forms.Label();
            this.LabTarget = new System.Windows.Forms.Label();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radNoPWD = new System.Windows.Forms.RadioButton();
            this.radMAC = new System.Windows.Forms.RadioButton();
            this.radMAC2 = new System.Windows.Forms.RadioButton();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numprint)).BeginInit();
            this.SuspendLayout();
            // 
            // ChkReprint
            // 
            this.ChkReprint.AutoSize = true;
            this.ChkReprint.Location = new System.Drawing.Point(277, 290);
            this.ChkReprint.Name = "ChkReprint";
            this.ChkReprint.Size = new System.Drawing.Size(48, 16);
            this.ChkReprint.TabIndex = 32;
            this.ChkReprint.Text = "补印";
            this.ChkReprint.UseVisualStyleBackColor = true;
            this.ChkReprint.Click += new System.EventHandler(this.ChkReprint_Click);
            // 
            // numprint
            // 
            this.numprint.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numprint.Location = new System.Drawing.Point(125, 193);
            this.numprint.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numprint.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numprint.Name = "numprint";
            this.numprint.Size = new System.Drawing.Size(69, 23);
            this.numprint.TabIndex = 31;
            this.numprint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numprint.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cb_Line
            // 
            this.cb_Line.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Line.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_Line.FormattingEnabled = true;
            this.cb_Line.Location = new System.Drawing.Point(125, 49);
            this.cb_Line.Name = "cb_Line";
            this.cb_Line.Size = new System.Drawing.Size(121, 22);
            this.cb_Line.TabIndex = 30;
            // 
            // tb_wo
            // 
            this.tb_wo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_wo.Location = new System.Drawing.Point(126, 8);
            this.tb_wo.Name = "tb_wo";
            this.tb_wo.Size = new System.Drawing.Size(117, 23);
            this.tb_wo.TabIndex = 29;
            this.tb_wo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_wo_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(18, 289);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 14);
            this.label5.TabIndex = 28;
            this.label5.Text = "输入:";
            // 
            // tb_Input
            // 
            this.tb_Input.Location = new System.Drawing.Point(71, 288);
            this.tb_Input.Name = "tb_Input";
            this.tb_Input.Size = new System.Drawing.Size(192, 21);
            this.tb_Input.TabIndex = 27;
            this.tb_Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Input_KeyDown);
            // 
            // LabRoute
            // 
            this.LabRoute.AutoSize = true;
            this.LabRoute.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabRoute.Location = new System.Drawing.Point(125, 162);
            this.LabRoute.Name = "LabRoute";
            this.LabRoute.Size = new System.Drawing.Size(49, 14);
            this.LabRoute.TabIndex = 26;
            this.LabRoute.Text = "label5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(29, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 14);
            this.label7.TabIndex = 25;
            this.label7.Text = "打印数量:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(57, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 24;
            this.label4.Text = "途程:";
            // 
            // LabInput
            // 
            this.LabInput.AutoSize = true;
            this.LabInput.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabInput.Location = new System.Drawing.Point(124, 124);
            this.LabInput.Name = "LabInput";
            this.LabInput.Size = new System.Drawing.Size(49, 14);
            this.LabInput.TabIndex = 23;
            this.LabInput.Text = "label5";
            // 
            // LabTarget
            // 
            this.LabTarget.AutoSize = true;
            this.LabTarget.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabTarget.Location = new System.Drawing.Point(125, 84);
            this.LabTarget.Name = "LabTarget";
            this.LabTarget.Size = new System.Drawing.Size(49, 14);
            this.LabTarget.TabIndex = 22;
            this.LabTarget.Text = "label5";
            // 
            // rtbmsg
            // 
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbmsg.Location = new System.Drawing.Point(0, 333);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.ReadOnly = true;
            this.rtbmsg.Size = new System.Drawing.Size(388, 137);
            this.rtbmsg.TabIndex = 21;
            this.rtbmsg.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(45, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 20;
            this.label3.Text = "投入数:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(31, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 19;
            this.label2.Text = "工单套数:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(60, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 14);
            this.label6.TabIndex = 17;
            this.label6.Text = "线体:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(62, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 18;
            this.label1.Text = "工单:";
            // 
            // radNoPWD
            // 
            this.radNoPWD.AutoSize = true;
            this.radNoPWD.Checked = true;
            this.radNoPWD.Location = new System.Drawing.Point(216, 87);
            this.radNoPWD.Name = "radNoPWD";
            this.radNoPWD.Size = new System.Drawing.Size(83, 16);
            this.radNoPWD.TabIndex = 33;
            this.radNoPWD.TabStop = true;
            this.radNoPWD.Text = "不计算密码";
            this.radNoPWD.UseVisualStyleBackColor = true;
            // 
            // radMAC
            // 
            this.radMAC.AutoSize = true;
            this.radMAC.Location = new System.Drawing.Point(216, 124);
            this.radMAC.Name = "radMAC";
            this.radMAC.Size = new System.Drawing.Size(101, 16);
            this.radMAC.TabIndex = 33;
            this.radMAC.Text = "[MAC]计算密码";
            this.radMAC.UseVisualStyleBackColor = true;
            // 
            // radMAC2
            // 
            this.radMAC2.AutoSize = true;
            this.radMAC2.Location = new System.Drawing.Point(216, 162);
            this.radMAC2.Name = "radMAC2";
            this.radMAC2.Size = new System.Drawing.Size(113, 16);
            this.radMAC2.TabIndex = 33;
            this.radMAC2.Text = "[MAC+2]计算密码";
            this.radMAC2.UseVisualStyleBackColor = true;
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // Frm_TEST_INPUT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 470);
            this.Controls.Add(this.radMAC2);
            this.Controls.Add(this.radMAC);
            this.Controls.Add(this.radNoPWD);
            this.Controls.Add(this.ChkReprint);
            this.Controls.Add(this.numprint);
            this.Controls.Add(this.cb_Line);
            this.Controls.Add(this.tb_wo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_Input);
            this.Controls.Add(this.LabRoute);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LabInput);
            this.Controls.Add(this.LabTarget);
            this.Controls.Add(this.rtbmsg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_TEST_INPUT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_TEST_INPUT_FormClosing);
            this.Load += new System.EventHandler(this.Frm_TEST_INPUT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numprint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkReprint;
        private System.Windows.Forms.NumericUpDown numprint;
        private System.Windows.Forms.ComboBox cb_Line;
        private System.Windows.Forms.TextBox tb_wo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_Input;
        private System.Windows.Forms.Label LabRoute;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LabInput;
        private System.Windows.Forms.Label LabTarget;
        private System.Windows.Forms.RichTextBox rtbmsg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radNoPWD;
        private System.Windows.Forms.RadioButton radMAC;
        private System.Windows.Forms.RadioButton radMAC2;
        private DevComponents.DotNetBar.StyleManager styleManager1;
    }
}

