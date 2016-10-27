namespace SFIS_V2
{
    partial class MyContextMenu
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
            this.dgv_craftitemparamet = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.CraftId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CraftItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CraftItemDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uplimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lowerlimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.other = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_save = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_craftitemparamet)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_craftitemparamet
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_craftitemparamet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_craftitemparamet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_craftitemparamet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CraftId,
            this.CraftItem,
            this.CraftItemDes,
            this.uplimit,
            this.lowerlimit,
            this.other});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_craftitemparamet.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_craftitemparamet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_craftitemparamet.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_craftitemparamet.Location = new System.Drawing.Point(0, 0);
            this.dgv_craftitemparamet.Name = "dgv_craftitemparamet";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_craftitemparamet.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_craftitemparamet.RowTemplate.Height = 23;
            this.dgv_craftitemparamet.Size = new System.Drawing.Size(590, 231);
            this.dgv_craftitemparamet.TabIndex = 1;
            this.dgv_craftitemparamet.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_craftitemparamet_CellValueChanged);
            this.dgv_craftitemparamet.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_craftitemparamet_RowsAdded);
            this.dgv_craftitemparamet.MouseLeave += new System.EventHandler(this.dgv_craftitemparamet_MouseLeave);
            this.dgv_craftitemparamet.MouseEnter += new System.EventHandler(this.dgv_craftitemparamet_MouseEnter);
            // 
            // CraftId
            // 
            this.CraftId.DataPropertyName = "craftId";
            this.CraftId.HeaderText = "工艺编号";
            this.CraftId.Name = "CraftId";
            this.CraftId.ReadOnly = true;
            // 
            // CraftItem
            // 
            this.CraftItem.DataPropertyName = "craftItem";
            this.CraftItem.HeaderText = "项目编号";
            this.CraftItem.Name = "CraftItem";
            this.CraftItem.ReadOnly = true;
            this.CraftItem.Width = 80;
            // 
            // CraftItemDes
            // 
            this.CraftItemDes.DataPropertyName = "craftparameterdes";
            this.CraftItemDes.HeaderText = "项目描述";
            this.CraftItemDes.Name = "CraftItemDes";
            // 
            // uplimit
            // 
            this.uplimit.DataPropertyName = "upperlimit";
            this.uplimit.HeaderText = "参数上限";
            this.uplimit.Name = "uplimit";
            this.uplimit.Width = 80;
            // 
            // lowerlimit
            // 
            this.lowerlimit.DataPropertyName = "lowerlimit";
            this.lowerlimit.HeaderText = "参数下限";
            this.lowerlimit.Name = "lowerlimit";
            this.lowerlimit.Width = 80;
            // 
            // other
            // 
            this.other.DataPropertyName = "other";
            this.other.HeaderText = "其他";
            this.other.Name = "other";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_save);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 25);
            this.panel1.TabIndex = 2;
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(12, 1);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(53, 23);
            this.bt_save.TabIndex = 0;
            this.bt_save.Text = "保存";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_craftitemparamet);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(590, 231);
            this.panel2.TabIndex = 3;
            // 
            // MyContextMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 256);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyContextMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "工艺项目及项目参数设置";
            this.Load += new System.EventHandler(this.MyContextMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_craftitemparamet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_craftitemparamet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CraftId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CraftItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn CraftItemDes;
        private System.Windows.Forms.DataGridViewTextBoxColumn uplimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn lowerlimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn other;

    }
}