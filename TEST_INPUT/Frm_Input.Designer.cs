namespace TEST_INPUT
{
    partial class Frm_Input
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.LabTarget = new System.Windows.Forms.Label();
            this.LabInput = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_Input = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_wo = new System.Windows.Forms.TextBox();
            this.LabRoute = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numprint = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.ChkReprint = new System.Windows.Forms.CheckBox();
            this.LabLine = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolSelectLine = new System.Windows.Forms.ToolStripMenuItem();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numprint)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(62, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "工单:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(31, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "工单套数:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(45, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "投入数:";
            // 
            // rtbmsg
            // 
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbmsg.Location = new System.Drawing.Point(0, 292);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.ReadOnly = true;
            this.rtbmsg.Size = new System.Drawing.Size(358, 137);
            this.rtbmsg.TabIndex = 3;
            this.rtbmsg.Text = "";
            // 
            // LabTarget
            // 
            this.LabTarget.AutoSize = true;
            this.LabTarget.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabTarget.Location = new System.Drawing.Point(146, 92);
            this.LabTarget.Name = "LabTarget";
            this.LabTarget.Size = new System.Drawing.Size(49, 14);
            this.LabTarget.TabIndex = 6;
            this.LabTarget.Text = "label5";
            this.LabTarget.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabInput
            // 
            this.LabInput.AutoSize = true;
            this.LabInput.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabInput.Location = new System.Drawing.Point(146, 129);
            this.LabInput.Name = "LabInput";
            this.LabInput.Size = new System.Drawing.Size(49, 14);
            this.LabInput.TabIndex = 7;
            this.LabInput.Text = "label5";
            this.LabInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(60, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "途程:";
            // 
            // tb_Input
            // 
            this.tb_Input.Location = new System.Drawing.Point(71, 250);
            this.tb_Input.Name = "tb_Input";
            this.tb_Input.Size = new System.Drawing.Size(192, 21);
            this.tb_Input.TabIndex = 10;
            this.tb_Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Input_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(18, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 14);
            this.label5.TabIndex = 11;
            this.label5.Text = "输入:";
            // 
            // tb_wo
            // 
            this.tb_wo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_wo.Location = new System.Drawing.Point(126, 16);
            this.tb_wo.Name = "tb_wo";
            this.tb_wo.Size = new System.Drawing.Size(117, 23);
            this.tb_wo.TabIndex = 13;
            this.tb_wo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_wo_KeyDown);
            // 
            // LabRoute
            // 
            this.LabRoute.AutoSize = true;
            this.LabRoute.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabRoute.Location = new System.Drawing.Point(146, 167);
            this.LabRoute.Name = "LabRoute";
            this.LabRoute.Size = new System.Drawing.Size(49, 14);
            this.LabRoute.TabIndex = 9;
            this.LabRoute.Text = "label5";
            this.LabRoute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(60, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "线体:";
            // 
            // numprint
            // 
            this.numprint.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numprint.Location = new System.Drawing.Point(146, 199);
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
            this.numprint.Size = new System.Drawing.Size(61, 23);
            this.numprint.TabIndex = 15;
            this.numprint.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(29, 203);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 14);
            this.label7.TabIndex = 8;
            this.label7.Text = "打印数量:";
            // 
            // ChkReprint
            // 
            this.ChkReprint.AutoSize = true;
            this.ChkReprint.Location = new System.Drawing.Point(277, 252);
            this.ChkReprint.Name = "ChkReprint";
            this.ChkReprint.Size = new System.Drawing.Size(48, 16);
            this.ChkReprint.TabIndex = 16;
            this.ChkReprint.Text = "补印";
            this.ChkReprint.UseVisualStyleBackColor = true;
            this.ChkReprint.Click += new System.EventHandler(this.ChkReprint_Click);
            // 
            // LabLine
            // 
            this.LabLine.AutoSize = true;
            this.LabLine.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabLine.Location = new System.Drawing.Point(146, 57);
            this.LabLine.Name = "LabLine";
            this.LabLine.Size = new System.Drawing.Size(49, 14);
            this.LabLine.TabIndex = 6;
            this.LabLine.Text = "label5";
            this.LabLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSelectLine});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // toolSelectLine
            // 
            this.toolSelectLine.Name = "toolSelectLine";
            this.toolSelectLine.Size = new System.Drawing.Size(124, 22);
            this.toolSelectLine.Text = "选择线体";
            this.toolSelectLine.Click += new System.EventHandler(this.toolSelectLine_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // Frm_Input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 429);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.ChkReprint);
            this.Controls.Add(this.numprint);
            this.Controls.Add(this.tb_wo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_Input);
            this.Controls.Add(this.LabRoute);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LabInput);
            this.Controls.Add(this.LabLine);
            this.Controls.Add(this.LabTarget);
            this.Controls.Add(this.rtbmsg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Name = "Frm_Input";
            this.Text = "投板打印工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Input_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Input_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numprint)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbmsg;
        private System.Windows.Forms.Label LabTarget;
        private System.Windows.Forms.Label LabInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_Input;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_wo;
        private System.Windows.Forms.Label LabRoute;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numprint;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox ChkReprint;
        private System.Windows.Forms.Label LabLine;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolSelectLine;
        private DevComponents.DotNetBar.StyleManager styleManager1;
    }
}