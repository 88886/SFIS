namespace SFIS_PRINT_SYSTEM.Frm
{
    partial class ShowData
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbvalue = new System.Windows.Forms.TextBox();
            this.cbwhere = new System.Windows.Forms.ComboBox();
            this.btselect = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvdata = new System.Windows.Forms.DataGridView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdata)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.tbvalue);
            this.splitContainer1.Panel1.Controls.Add(this.cbwhere);
            this.splitContainer1.Panel1.Controls.Add(this.btselect);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvdata);
            this.splitContainer1.Size = new System.Drawing.Size(608, 372);
            this.splitContainer1.SplitterDistance = 46;
            this.splitContainer1.TabIndex = 0;
            // 
            // tbvalue
            // 
            this.tbvalue.Location = new System.Drawing.Point(199, 16);
            this.tbvalue.Name = "tbvalue";
            this.tbvalue.Size = new System.Drawing.Size(232, 21);
            this.tbvalue.TabIndex = 2;
            this.tbvalue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbvalue_KeyDown);
            // 
            // cbwhere
            // 
            this.cbwhere.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbwhere.FormattingEnabled = true;
            this.cbwhere.Location = new System.Drawing.Point(99, 16);
            this.cbwhere.Name = "cbwhere";
            this.cbwhere.Size = new System.Drawing.Size(92, 20);
            this.cbwhere.TabIndex = 1;
            // 
            // btselect
            // 
            this.btselect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btselect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btselect.Location = new System.Drawing.Point(446, 16);
            this.btselect.Name = "btselect";
            this.btselect.Size = new System.Drawing.Size(53, 21);
            this.btselect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btselect.TabIndex = 3;
            this.btselect.Text = "查询";
            this.btselect.Click += new System.EventHandler(this.btselect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[检索条件]:";
            // 
            // dgvdata
            // 
            this.dgvdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvdata.Location = new System.Drawing.Point(0, 0);
            this.dgvdata.Name = "dgvdata";
            this.dgvdata.ReadOnly = true;
            this.dgvdata.RowTemplate.Height = 23;
            this.dgvdata.Size = new System.Drawing.Size(608, 322);
            this.dgvdata.TabIndex = 0;
            this.dgvdata.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvdata_CellMouseDoubleClick);
            // 
            // ShowData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 372);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowData";
            this.Text = "数据预览";
            this.Load += new System.EventHandler(this.ShowData_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvdata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvdata;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX btselect;
        private System.Windows.Forms.TextBox tbvalue;
        private System.Windows.Forms.ComboBox cbwhere;
    }
}