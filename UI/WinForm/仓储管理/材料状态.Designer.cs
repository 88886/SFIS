namespace SFIS_V2
{
    partial class MaterialsStorageInfo
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.KpNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KpNumber,
            this.sstatus,
            this.qty,
            this.recdate});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(443, 298);
            this.dataGridView1.TabIndex = 0;
            // 
            // KpNumber
            // 
            this.KpNumber.DataPropertyName = "KpNumber";
            this.KpNumber.HeaderText = "料号";
            this.KpNumber.Name = "KpNumber";
            this.KpNumber.ReadOnly = true;
            this.KpNumber.Width = 54;
            // 
            // sstatus
            // 
            this.sstatus.DataPropertyName = "sstatus";
            this.sstatus.HeaderText = "状态";
            this.sstatus.Name = "sstatus";
            this.sstatus.ReadOnly = true;
            this.sstatus.Width = 54;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "数量";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            this.qty.Width = 54;
            // 
            // recdate
            // 
            this.recdate.DataPropertyName = "recdate";
            this.recdate.HeaderText = "日期";
            this.recdate.Name = "recdate";
            this.recdate.ReadOnly = true;
            this.recdate.Width = 54;
            // 
            // MaterialsStorageInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 298);
            this.Controls.Add(this.dataGridView1);
            this.DoubleBuffered = true;
            this.Name = "MaterialsStorageInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "材料出入库记录";
            this.Load += new System.EventHandler(this.MaterialsStorageInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn KpNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn sstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn recdate;
    }
}