namespace SFIS_V2
{
    partial class Frm_Duty
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
            this.tb_duty = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvduty = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvduty)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.imbt_search);
            this.groupBox1.Controls.Add(this.tb_duty);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 72);
            this.groupBox1.TabIndex = 2;
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
            // tb_duty
            // 
            this.tb_duty.BackColor = System.Drawing.Color.Yellow;
            this.tb_duty.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_duty.Location = new System.Drawing.Point(28, 27);
            this.tb_duty.Name = "tb_duty";
            this.tb_duty.Size = new System.Drawing.Size(165, 26);
            this.tb_duty.TabIndex = 0;
            this.tb_duty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_duty_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvduty);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(511, 242);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // dgvduty
            // 
            this.dgvduty.AllowUserToAddRows = false;
            this.dgvduty.AllowUserToDeleteRows = false;
            this.dgvduty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvduty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvduty.Location = new System.Drawing.Point(3, 17);
            this.dgvduty.Name = "dgvduty";
            this.dgvduty.ReadOnly = true;
            this.dgvduty.RowTemplate.Height = 23;
            this.dgvduty.Size = new System.Drawing.Size(505, 222);
            this.dgvduty.TabIndex = 0;
            this.dgvduty.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvduty_CellMouseDoubleClick);
            // 
            // Frm_Duty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 314);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Duty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Duty";
            this.Load += new System.EventHandler(this.Frm_Duty_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvduty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button imbt_search;
        private System.Windows.Forms.TextBox tb_duty;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvduty;
    }
}