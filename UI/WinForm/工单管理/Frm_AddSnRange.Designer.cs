namespace SFIS_V2
{
    partial class Frm_AddSnRange
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
            this.snpostfix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.ver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.snprefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usenum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.snend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.snleng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvserialnumberrule = new System.Windows.Forms.DataGridView();
            this.woId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sntype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.snstart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btInputdata = new DevComponents.DotNetBar.ButtonX();
            this.bt_save = new DevComponents.DotNetBar.ButtonX();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvserialnumberrule)).BeginInit();
            this.SuspendLayout();
            // 
            // snpostfix
            // 
            this.snpostfix.DataPropertyName = "snpostfix";
            this.snpostfix.HeaderText = "后缀";
            this.snpostfix.Name = "snpostfix";
            this.snpostfix.Visible = false;
            this.snpostfix.Width = 90;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(770, 47);
            this.label1.TabIndex = 5;
            this.label1.Text = "序列号区间设置";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ver
            // 
            this.ver.DataPropertyName = "ver";
            this.ver.HeaderText = "版本";
            this.ver.Name = "ver";
            this.ver.Visible = false;
            this.ver.Width = 70;
            // 
            // snprefix
            // 
            this.snprefix.DataPropertyName = "snprefix";
            this.snprefix.HeaderText = "前缀";
            this.snprefix.Name = "snprefix";
            this.snprefix.Visible = false;
            this.snprefix.Width = 90;
            // 
            // usenum
            // 
            this.usenum.DataPropertyName = "usenum";
            this.usenum.HeaderText = "用量";
            this.usenum.Name = "usenum";
            // 
            // reve
            // 
            this.reve.DataPropertyName = "reve";
            this.reve.HeaderText = "备注";
            this.reve.Name = "reve";
            // 
            // snend
            // 
            this.snend.DataPropertyName = "snend";
            this.snend.HeaderText = "结束";
            this.snend.Name = "snend";
            // 
            // snleng
            // 
            this.snleng.DataPropertyName = "snleng";
            this.snleng.HeaderText = "长度";
            this.snleng.Name = "snleng";
            this.snleng.Width = 70;
            // 
            // dgvserialnumberrule
            // 
            this.dgvserialnumberrule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvserialnumberrule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.woId,
            this.sntype,
            this.snstart,
            this.snend,
            this.snprefix,
            this.snpostfix,
            this.ver,
            this.reve,
            this.snleng,
            this.usenum});
            this.dgvserialnumberrule.Location = new System.Drawing.Point(2, 73);
            this.dgvserialnumberrule.Name = "dgvserialnumberrule";
            this.dgvserialnumberrule.RowTemplate.Height = 23;
            this.dgvserialnumberrule.Size = new System.Drawing.Size(766, 230);
            this.dgvserialnumberrule.TabIndex = 10;
            this.dgvserialnumberrule.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvserialnumberrule_CellBeginEdit);
            this.dgvserialnumberrule.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvserialnumberrule_CellValueChanged);
            this.dgvserialnumberrule.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvserialnumberrule_RowValidated);
            // 
            // woId
            // 
            this.woId.DataPropertyName = "woId";
            this.woId.HeaderText = "工单号";
            this.woId.Name = "woId";
            this.woId.ReadOnly = true;
            this.woId.Width = 80;
            // 
            // sntype
            // 
            this.sntype.DataPropertyName = "sntype";
            this.sntype.HeaderText = "类型";
            this.sntype.Name = "sntype";
            this.sntype.ReadOnly = true;
            this.sntype.Width = 70;
            // 
            // snstart
            // 
            this.snstart.DataPropertyName = "snstart";
            this.snstart.HeaderText = "开始";
            this.snstart.Name = "snstart";
            // 
            // btInputdata
            // 
            this.btInputdata.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btInputdata.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btInputdata.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btInputdata.Location = new System.Drawing.Point(540, 311);
            this.btInputdata.Name = "btInputdata";
            this.btInputdata.Size = new System.Drawing.Size(86, 33);
            this.btInputdata.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btInputdata.TabIndex = 8;
            this.btInputdata.Text = "加载数据";
            this.btInputdata.Click += new System.EventHandler(this.btInputdata_Click);
            // 
            // bt_save
            // 
            this.bt_save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_save.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_save.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_save.Location = new System.Drawing.Point(663, 309);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(86, 33);
            this.bt_save.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_save.TabIndex = 9;
            this.bt_save.Text = "保  存";
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "格式说明:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "SN区间:";
            // 
            // Frm_AddSnRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 350);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvserialnumberrule);
            this.Controls.Add(this.btInputdata);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_AddSnRange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_AddSnRange";
            this.Load += new System.EventHandler(this.Frm_AddSnRange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvserialnumberrule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn snpostfix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ver;
        private System.Windows.Forms.DataGridViewTextBoxColumn snprefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn usenum;
        private System.Windows.Forms.DataGridViewTextBoxColumn reve;
        private System.Windows.Forms.DataGridViewTextBoxColumn snend;
        private System.Windows.Forms.DataGridViewTextBoxColumn snleng;
        private System.Windows.Forms.DataGridView dgvserialnumberrule;
        private System.Windows.Forms.DataGridViewTextBoxColumn woId;
        private System.Windows.Forms.DataGridViewTextBoxColumn sntype;
        private System.Windows.Forms.DataGridViewTextBoxColumn snstart;
        private DevComponents.DotNetBar.ButtonX btInputdata;
        private DevComponents.DotNetBar.ButtonX bt_save;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}