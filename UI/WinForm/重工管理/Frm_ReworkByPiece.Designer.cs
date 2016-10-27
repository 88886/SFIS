namespace SFIS_V2
{
    partial class Frm_ReworkByPiece
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_reworkno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_workorder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_inputgroup = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chk_kpesn = new System.Windows.Forms.CheckBox();
            this.chk_MAC = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_esn = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LabTotal = new System.Windows.Forms.Label();
            this.dgvSnList = new System.Windows.Forms.DataGridView();
            this.ESN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReworkNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ListSN = new System.Windows.Forms.ListBox();
            this.imbt_Exectue = new DevComponents.DotNetBar.ButtonX();
            this.imbt_Clear = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSnList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_inputgroup);
            this.groupBox1.Controls.Add(this.txt_workorder);
            this.groupBox1.Controls.Add(this.txt_reworkno);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(903, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rework No:";
            // 
            // txt_reworkno
            // 
            this.txt_reworkno.Enabled = false;
            this.txt_reworkno.Location = new System.Drawing.Point(128, 18);
            this.txt_reworkno.Name = "txt_reworkno";
            this.txt_reworkno.Size = new System.Drawing.Size(195, 21);
            this.txt_reworkno.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(19, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "WorkOrder:";
            // 
            // txt_workorder
            // 
            this.txt_workorder.Enabled = false;
            this.txt_workorder.Location = new System.Drawing.Point(128, 59);
            this.txt_workorder.Name = "txt_workorder";
            this.txt_workorder.Size = new System.Drawing.Size(195, 21);
            this.txt_workorder.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(362, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "Input Group:";
            // 
            // txt_inputgroup
            // 
            this.txt_inputgroup.Enabled = false;
            this.txt_inputgroup.Location = new System.Drawing.Point(471, 57);
            this.txt_inputgroup.Name = "txt_inputgroup";
            this.txt_inputgroup.Size = new System.Drawing.Size(159, 21);
            this.txt_inputgroup.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_MAC);
            this.groupBox2.Controls.Add(this.chk_kpesn);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(903, 97);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.imbt_Clear);
            this.groupBox3.Controls.Add(this.imbt_Exectue);
            this.groupBox3.Controls.Add(this.LabTotal);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txt_esn);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 193);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(903, 66);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ListSN);
            this.groupBox4.Controls.Add(this.dgvSnList);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 259);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(903, 219);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // chk_kpesn
            // 
            this.chk_kpesn.AutoSize = true;
            this.chk_kpesn.Location = new System.Drawing.Point(21, 24);
            this.chk_kpesn.Name = "chk_kpesn";
            this.chk_kpesn.Size = new System.Drawing.Size(96, 16);
            this.chk_kpesn.TabIndex = 0;
            this.chk_kpesn.Text = "Remove KPESN";
            this.chk_kpesn.UseVisualStyleBackColor = true;
            // 
            // chk_MAC
            // 
            this.chk_MAC.AutoSize = true;
            this.chk_MAC.Location = new System.Drawing.Point(21, 58);
            this.chk_MAC.Name = "chk_MAC";
            this.chk_MAC.Size = new System.Drawing.Size(84, 16);
            this.chk_MAC.TabIndex = 0;
            this.chk_MAC.Text = "Remove MAC";
            this.chk_MAC.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(34, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "ESN:";
            // 
            // txt_esn
            // 
            this.txt_esn.Location = new System.Drawing.Point(80, 24);
            this.txt_esn.Name = "txt_esn";
            this.txt_esn.Size = new System.Drawing.Size(178, 21);
            this.txt_esn.TabIndex = 2;
            this.txt_esn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_esn_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(339, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "Records:";
            // 
            // LabTotal
            // 
            this.LabTotal.AutoSize = true;
            this.LabTotal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabTotal.Location = new System.Drawing.Point(425, 26);
            this.LabTotal.Name = "LabTotal";
            this.LabTotal.Size = new System.Drawing.Size(16, 16);
            this.LabTotal.TabIndex = 3;
            this.LabTotal.Text = "0";
            // 
            // dgvSnList
            // 
            this.dgvSnList.AllowUserToAddRows = false;
            this.dgvSnList.AllowUserToDeleteRows = false;
            this.dgvSnList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSnList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ESN,
            this.ReworkNo});
            this.dgvSnList.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvSnList.Location = new System.Drawing.Point(3, 17);
            this.dgvSnList.Name = "dgvSnList";
            this.dgvSnList.ReadOnly = true;
            this.dgvSnList.RowTemplate.Height = 23;
            this.dgvSnList.Size = new System.Drawing.Size(689, 199);
            this.dgvSnList.TabIndex = 0;
            // 
            // ESN
            // 
            this.ESN.HeaderText = "ESN";
            this.ESN.Name = "ESN";
            this.ESN.ReadOnly = true;
            this.ESN.Width = 300;
            // 
            // ReworkNo
            // 
            this.ReworkNo.HeaderText = "ReworkNo";
            this.ReworkNo.Name = "ReworkNo";
            this.ReworkNo.ReadOnly = true;
            this.ReworkNo.Width = 200;
            // 
            // ListSN
            // 
            this.ListSN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListSN.FormattingEnabled = true;
            this.ListSN.ItemHeight = 12;
            this.ListSN.Location = new System.Drawing.Point(692, 17);
            this.ListSN.Name = "ListSN";
            this.ListSN.Size = new System.Drawing.Size(208, 199);
            this.ListSN.TabIndex = 1;
            // 
            // imbt_Exectue
            // 
            this.imbt_Exectue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_Exectue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_Exectue.Location = new System.Drawing.Point(544, 25);
            this.imbt_Exectue.Name = "imbt_Exectue";
            this.imbt_Exectue.Size = new System.Drawing.Size(75, 23);
            this.imbt_Exectue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_Exectue.TabIndex = 4;
            this.imbt_Exectue.Text = "Exectue";
            this.imbt_Exectue.Click += new System.EventHandler(this.imbt_Exectue_Click);
            // 
            // imbt_Clear
            // 
            this.imbt_Clear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_Clear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_Clear.Location = new System.Drawing.Point(663, 24);
            this.imbt_Clear.Name = "imbt_Clear";
            this.imbt_Clear.Size = new System.Drawing.Size(75, 23);
            this.imbt_Clear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_Clear.TabIndex = 5;
            this.imbt_Clear.Text = "Clear";
            this.imbt_Clear.Click += new System.EventHandler(this.imbt_Clear_Click);
            // 
            // Frm_ReworkByPiece
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 478);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_ReworkByPiece";
            this.Text = "解除绑定";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_ReworkByPiece_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSnList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_inputgroup;
        private System.Windows.Forms.TextBox txt_workorder;
        private System.Windows.Forms.TextBox txt_reworkno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chk_MAC;
        private System.Windows.Forms.CheckBox chk_kpesn;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevComponents.DotNetBar.ButtonX imbt_Clear;
        private DevComponents.DotNetBar.ButtonX imbt_Exectue;
        private System.Windows.Forms.Label LabTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_esn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox ListSN;
        private System.Windows.Forms.DataGridView dgvSnList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReworkNo;
    }
}