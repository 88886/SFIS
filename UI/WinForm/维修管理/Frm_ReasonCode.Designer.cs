namespace SFIS_V2
{
    partial class Frm_ReasonCode
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
            this.imbt_search = new System.Windows.Forms.Button();
            this.tb_reasoncode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvReasonCode = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReasonCode)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.imbt_search);
            this.groupBox1.Controls.Add(this.tb_reasoncode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // imbt_search
            // 
            this.imbt_search.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imbt_search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.imbt_search.Location = new System.Drawing.Point(221, 25);
            this.imbt_search.Name = "imbt_search";
            this.imbt_search.Size = new System.Drawing.Size(83, 26);
            this.imbt_search.TabIndex = 1;
            this.imbt_search.Text = "Search";
            this.imbt_search.UseVisualStyleBackColor = true;
            this.imbt_search.Click += new System.EventHandler(this.imbt_search_Click);
            // 
            // tb_reasoncode
            // 
            this.tb_reasoncode.BackColor = System.Drawing.Color.Yellow;
            this.tb_reasoncode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_reasoncode.Location = new System.Drawing.Point(28, 27);
            this.tb_reasoncode.Name = "tb_reasoncode";
            this.tb_reasoncode.Size = new System.Drawing.Size(165, 26);
            this.tb_reasoncode.TabIndex = 0;
            this.tb_reasoncode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_reasoncode_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvReasonCode);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(616, 284);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dgvReasonCode
            // 
            this.dgvReasonCode.AllowUserToAddRows = false;
            this.dgvReasonCode.AllowUserToDeleteRows = false;
            this.dgvReasonCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReasonCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReasonCode.Location = new System.Drawing.Point(3, 17);
            this.dgvReasonCode.Name = "dgvReasonCode";
            this.dgvReasonCode.ReadOnly = true;
            this.dgvReasonCode.RowTemplate.Height = 23;
            this.dgvReasonCode.Size = new System.Drawing.Size(610, 264);
            this.dgvReasonCode.TabIndex = 0;
            this.dgvReasonCode.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvReasonCode_CellMouseDoubleClick);
            // 
            // Frm_ReasonCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 356);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ReasonCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reason Code List";
            this.Load += new System.EventHandler(this.Frm_ReasonCode_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReasonCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvReasonCode;
        private System.Windows.Forms.Button imbt_search;
        private System.Windows.Forms.TextBox tb_reasoncode;
    }
}