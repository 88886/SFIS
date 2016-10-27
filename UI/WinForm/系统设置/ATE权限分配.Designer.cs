namespace SFIS_V2
{
    partial class FrmAteEmp
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btSave = new DevComponents.DotNetBar.ButtonX();
            this.bt_selectuser = new DevComponents.DotNetBar.ButtonX();
            this.tb_username = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvUserInfo = new System.Windows.Forms.DataGridView();
            this.userId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deptname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAteEmp = new System.Windows.Forms.DataGridView();
            this.cmbtSelectCraftId = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.craftId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.craftname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.beworkseg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.craftparameterurl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAteEmp)).BeginInit();
            this.cmbtSelectCraftId.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btSave);
            this.panel1.Controls.Add(this.bt_selectuser);
            this.panel1.Controls.Add(this.tb_username);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 42);
            this.panel1.TabIndex = 0;
            // 
            // btSave
            // 
            this.btSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btSave.Location = new System.Drawing.Point(755, 6);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(76, 30);
            this.btSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btSave.TabIndex = 5;
            this.btSave.Text = "保存";
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // bt_selectuser
            // 
            this.bt_selectuser.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_selectuser.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_selectuser.Location = new System.Drawing.Point(221, 13);
            this.bt_selectuser.Name = "bt_selectuser";
            this.bt_selectuser.Size = new System.Drawing.Size(42, 20);
            this.bt_selectuser.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_selectuser.TabIndex = 5;
            this.bt_selectuser.Text = "查询";
            this.bt_selectuser.Click += new System.EventHandler(this.bt_selectuser_Click);
            // 
            // tb_username
            // 
            // 
            // 
            // 
            this.tb_username.Border.Class = "TextBoxBorder";
            this.tb_username.Location = new System.Drawing.Point(58, 12);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(157, 21);
            this.tb_username.TabIndex = 4;
            this.tb_username.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_username_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "[用户]:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 42);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvUserInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvAteEmp);
            this.splitContainer1.Size = new System.Drawing.Size(843, 400);
            this.splitContainer1.SplitterDistance = 383;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgvUserInfo
            // 
            this.dgvUserInfo.AllowUserToAddRows = false;
            this.dgvUserInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userId,
            this.username,
            this.deptname});
            this.dgvUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUserInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvUserInfo.MultiSelect = false;
            this.dgvUserInfo.Name = "dgvUserInfo";
            this.dgvUserInfo.ReadOnly = true;
            this.dgvUserInfo.RowTemplate.Height = 23;
            this.dgvUserInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserInfo.Size = new System.Drawing.Size(383, 400);
            this.dgvUserInfo.TabIndex = 0;
            // 
            // userId
            // 
            this.userId.DataPropertyName = "userId";
            this.userId.HeaderText = "工号";
            this.userId.Name = "userId";
            this.userId.ReadOnly = true;
            // 
            // username
            // 
            this.username.DataPropertyName = "username";
            this.username.HeaderText = "名称";
            this.username.Name = "username";
            this.username.ReadOnly = true;
            // 
            // deptname
            // 
            this.deptname.DataPropertyName = "deptname";
            this.deptname.HeaderText = "部门";
            this.deptname.Name = "deptname";
            this.deptname.ReadOnly = true;
            // 
            // dgvAteEmp
            // 
            this.dgvAteEmp.AllowUserToAddRows = false;
            this.dgvAteEmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAteEmp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chk,
            this.craftId,
            this.craftname,
            this.beworkseg,
            this.craftparameterurl});
            this.dgvAteEmp.ContextMenuStrip = this.cmbtSelectCraftId;
            this.dgvAteEmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAteEmp.Location = new System.Drawing.Point(0, 0);
            this.dgvAteEmp.Name = "dgvAteEmp";
            this.dgvAteEmp.RowTemplate.Height = 23;
            this.dgvAteEmp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAteEmp.Size = new System.Drawing.Size(456, 400);
            this.dgvAteEmp.TabIndex = 0;
            this.dgvAteEmp.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAteEmp_CellValueChanged);
            // 
            // cmbtSelectCraftId
            // 
            this.cmbtSelectCraftId.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btToolStripMenuItem});
            this.cmbtSelectCraftId.Name = "cmbtSelectCraftId";
            this.cmbtSelectCraftId.Size = new System.Drawing.Size(101, 26);
            // 
            // btToolStripMenuItem
            // 
            this.btToolStripMenuItem.Name = "btToolStripMenuItem";
            this.btToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.btToolStripMenuItem.Text = "选择";
            this.btToolStripMenuItem.Click += new System.EventHandler(this.btToolStripMenuItem_Click);
            // 
            // chk
            // 
            this.chk.DataPropertyName = "chk";
            this.chk.HeaderText = "选择";
            this.chk.Name = "chk";
            // 
            // craftId
            // 
            this.craftId.DataPropertyName = "craftId";
            this.craftId.HeaderText = "工艺编号";
            this.craftId.Name = "craftId";
            this.craftId.ReadOnly = true;
            // 
            // craftname
            // 
            this.craftname.DataPropertyName = "craftname";
            this.craftname.HeaderText = "工艺名称";
            this.craftname.Name = "craftname";
            this.craftname.ReadOnly = true;
            // 
            // beworkseg
            // 
            this.beworkseg.DataPropertyName = "beworkseg";
            this.beworkseg.HeaderText = "类别";
            this.beworkseg.Name = "beworkseg";
            this.beworkseg.ReadOnly = true;
            // 
            // craftparameterurl
            // 
            this.craftparameterurl.DataPropertyName = "craftparameterurl";
            this.craftparameterurl.HeaderText = "craftparameterurl";
            this.craftparameterurl.Name = "craftparameterurl";
            this.craftparameterurl.ReadOnly = true;
            this.craftparameterurl.Visible = false;
            // 
            // FrmAteEmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 442);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmAteEmp";
            this.Text = "ATE用户权限管理";
            this.Load += new System.EventHandler(this.FrmAteEmp_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAteEmp)).EndInit();
            this.cmbtSelectCraftId.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvUserInfo;
        private System.Windows.Forms.DataGridView dgvAteEmp;
        private DevComponents.DotNetBar.ButtonX btSave;
        private DevComponents.DotNetBar.ButtonX bt_selectuser;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn userId;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn deptname;
        private System.Windows.Forms.ContextMenuStrip cmbtSelectCraftId;
        private System.Windows.Forms.ToolStripMenuItem btToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn craftId;
        private System.Windows.Forms.DataGridViewTextBoxColumn craftname;
        private System.Windows.Forms.DataGridViewTextBoxColumn beworkseg;
        private System.Windows.Forms.DataGridViewTextBoxColumn craftparameterurl;
    }
}