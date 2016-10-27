namespace Frm_Public
{
    partial class Frm_Select
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_selectvalues = new System.Windows.Forms.TextBox();
            this.cbx_selecttype = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_showdata = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showdata)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_selectvalues);
            this.panel1.Controls.Add(this.cbx_selecttype);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 49);
            this.panel1.TabIndex = 0;
            // 
            // txt_selectvalues
            // 
            this.txt_selectvalues.Location = new System.Drawing.Point(238, 14);
            this.txt_selectvalues.Name = "txt_selectvalues";
            this.txt_selectvalues.Size = new System.Drawing.Size(212, 21);
            this.txt_selectvalues.TabIndex = 2;
            this.txt_selectvalues.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_selectvalues_KeyDown);
            // 
            // cbx_selecttype
            // 
            this.cbx_selecttype.FormattingEnabled = true;
            this.cbx_selecttype.Location = new System.Drawing.Point(71, 14);
            this.cbx_selecttype.Name = "cbx_selecttype";
            this.cbx_selecttype.Size = new System.Drawing.Size(134, 20);
            this.cbx_selecttype.TabIndex = 1;
            this.cbx_selecttype.DropDown += new System.EventHandler(this.cbx_selecttype_DropDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "检索类型";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_showdata);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(567, 264);
            this.panel2.TabIndex = 1;
            // 
            // dgv_showdata
            // 
            this.dgv_showdata.AllowUserToAddRows = false;
            this.dgv_showdata.AllowUserToDeleteRows = false;
            this.dgv_showdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_showdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_showdata.Location = new System.Drawing.Point(0, 0);
            this.dgv_showdata.Name = "dgv_showdata";
            this.dgv_showdata.ReadOnly = true;
            this.dgv_showdata.RowTemplate.Height = 23;
            this.dgv_showdata.Size = new System.Drawing.Size(567, 264);
            this.dgv_showdata.TabIndex = 0;
            this.dgv_showdata.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_showdata_CellMouseDoubleClick);
            // 
            // Frm_Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 313);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Select";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择数据";
            this.Load += new System.EventHandler(this.Frm_Select_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showdata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_showdata;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbx_selecttype;
        private System.Windows.Forms.TextBox txt_selectvalues;
    }
}

