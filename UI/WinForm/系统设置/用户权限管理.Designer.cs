namespace SFIS_V2
{
    partial class FrmUseManage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bt_selectuser = new DevComponents.DotNetBar.ButtonX();
            this.tb_username = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_ShowUser = new System.Windows.Forms.DataGridView();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deptname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bt_save = new DevComponents.DotNetBar.ButtonX();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgv_showJurisdiction = new System.Windows.Forms.DataGridView();
            this.Jurisdiction = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.progId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.funId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.funname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fundesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShowUser)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showJurisdiction)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bt_selectuser);
            this.panel2.Controls.Add(this.tb_username);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(728, 35);
            this.panel2.TabIndex = 1;
            // 
            // bt_selectuser
            // 
            this.bt_selectuser.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_selectuser.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_selectuser.Location = new System.Drawing.Point(217, 7);
            this.bt_selectuser.Name = "bt_selectuser";
            this.bt_selectuser.Size = new System.Drawing.Size(42, 20);
            this.bt_selectuser.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_selectuser.TabIndex = 2;
            this.bt_selectuser.Text = "选择";
            this.bt_selectuser.Click += new System.EventHandler(this.bt_selectuser_Click);
            // 
            // tb_username
            // 
            // 
            // 
            // 
            this.tb_username.Border.Class = "TextBoxBorder";
            this.tb_username.Location = new System.Drawing.Point(54, 6);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(157, 21);
            this.tb_username.TabIndex = 1;
            this.tb_username.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_username_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[用户]:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_ShowUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 177);
            this.panel1.TabIndex = 2;
            // 
            // dgv_ShowUser
            // 
            this.dgv_ShowUser.AllowUserToAddRows = false;
            this.dgv_ShowUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ShowUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.username,
            this.userId,
            this.deptname});
            this.dgv_ShowUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ShowUser.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_ShowUser.Location = new System.Drawing.Point(0, 0);
            this.dgv_ShowUser.MultiSelect = false;
            this.dgv_ShowUser.Name = "dgv_ShowUser";
            this.dgv_ShowUser.ReadOnly = true;
            this.dgv_ShowUser.RowTemplate.Height = 23;
            this.dgv_ShowUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ShowUser.Size = new System.Drawing.Size(728, 177);
            this.dgv_ShowUser.TabIndex = 0;
            // 
            // username
            // 
            this.username.DataPropertyName = "username";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.username.DefaultCellStyle = dataGridViewCellStyle4;
            this.username.HeaderText = "用户名";
            this.username.Name = "username";
            this.username.ReadOnly = true;
            // 
            // userId
            // 
            this.userId.DataPropertyName = "userId";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.userId.DefaultCellStyle = dataGridViewCellStyle5;
            this.userId.HeaderText = "工号";
            this.userId.Name = "userId";
            this.userId.ReadOnly = true;
            // 
            // deptname
            // 
            this.deptname.DataPropertyName = "deptname";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.deptname.DefaultCellStyle = dataGridViewCellStyle6;
            this.deptname.HeaderText = "所属部门";
            this.deptname.Name = "deptname";
            this.deptname.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.bt_save);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 212);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(728, 39);
            this.panel3.TabIndex = 3;
            // 
            // bt_save
            // 
            this.bt_save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_save.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_save.Location = new System.Drawing.Point(373, 5);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(69, 30);
            this.bt_save.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_save.TabIndex = 1;
            this.bt_save.Text = "保存";
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "权限列表:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgv_showJurisdiction);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 251);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(728, 254);
            this.panel4.TabIndex = 4;
            // 
            // dgv_showJurisdiction
            // 
            this.dgv_showJurisdiction.AllowUserToAddRows = false;
            this.dgv_showJurisdiction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_showJurisdiction.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Jurisdiction,
            this.progId,
            this.progname,
            this.funId,
            this.funname,
            this.fundesc});
            this.dgv_showJurisdiction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_showJurisdiction.Location = new System.Drawing.Point(0, 0);
            this.dgv_showJurisdiction.Name = "dgv_showJurisdiction";
            this.dgv_showJurisdiction.RowTemplate.Height = 23;
            this.dgv_showJurisdiction.Size = new System.Drawing.Size(728, 254);
            this.dgv_showJurisdiction.TabIndex = 0;
            // 
            // Jurisdiction
            // 
            this.Jurisdiction.DataPropertyName = "Jurisdiction";
            this.Jurisdiction.HeaderText = "权限";
            this.Jurisdiction.Name = "Jurisdiction";
            // 
            // progId
            // 
            this.progId.DataPropertyName = "progid";
            this.progId.HeaderText = "程序编号";
            this.progId.Name = "progId";
            this.progId.ReadOnly = true;
            this.progId.Visible = false;
            // 
            // progname
            // 
            this.progname.DataPropertyName = "progname";
            this.progname.HeaderText = "程序名称";
            this.progname.Name = "progname";
            this.progname.ReadOnly = true;
            // 
            // funId
            // 
            this.funId.DataPropertyName = "funid";
            this.funId.HeaderText = "功能编号";
            this.funId.Name = "funId";
            this.funId.ReadOnly = true;
            this.funId.Visible = false;
            // 
            // funname
            // 
            this.funname.DataPropertyName = "funname";
            this.funname.HeaderText = "功能名称";
            this.funname.Name = "funname";
            this.funname.ReadOnly = true;
            // 
            // fundesc
            // 
            this.fundesc.DataPropertyName = "fundesc";
            this.fundesc.HeaderText = "功能描述";
            this.fundesc.Name = "fundesc";
            this.fundesc.ReadOnly = true;
            // 
            // FrmUseManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 505);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmUseManage";
            this.Text = "用户权限管理";
            this.Load += new System.EventHandler(this.FrmUseManage_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShowUser)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showJurisdiction)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ButtonX bt_selectuser;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_ShowUser;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX bt_save;
        private System.Windows.Forms.Panel panel4;
        //private DevComponents.DotNetBar.Controls.DataGridViewX dgv_showJurisdiction;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn userId;
        private System.Windows.Forms.DataGridViewTextBoxColumn deptname;
        private System.Windows.Forms.DataGridView dgv_showJurisdiction;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Jurisdiction;
        private System.Windows.Forms.DataGridViewTextBoxColumn progId;
        private System.Windows.Forms.DataGridViewTextBoxColumn progname;
        private System.Windows.Forms.DataGridViewTextBoxColumn funId;
        private System.Windows.Forms.DataGridViewTextBoxColumn funname;
        private System.Windows.Forms.DataGridViewTextBoxColumn fundesc;
    }
}