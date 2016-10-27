namespace SFIS_V2
{
    partial class SelectData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_query = new DevComponents.DotNetBar.ButtonX();
            this.tb_selectvalue = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cmb_selecttype = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_showdata = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showdata)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_query);
            this.panel1.Controls.Add(this.tb_selectvalue);
            this.panel1.Controls.Add(this.cmb_selecttype);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 41);
            this.panel1.TabIndex = 0;
            // 
            // bt_query
            // 
            this.bt_query.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_query.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_query.Location = new System.Drawing.Point(379, 8);
            this.bt_query.Name = "bt_query";
            this.bt_query.Size = new System.Drawing.Size(43, 23);
            this.bt_query.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_query.TabIndex = 3;
            this.bt_query.Text = "查询";
            this.bt_query.Click += new System.EventHandler(this.bt_query_Click);
            // 
            // tb_selectvalue
            // 
            // 
            // 
            // 
            this.tb_selectvalue.Border.Class = "TextBoxBorder";
            this.tb_selectvalue.Location = new System.Drawing.Point(215, 10);
            this.tb_selectvalue.Name = "tb_selectvalue";
            this.tb_selectvalue.Size = new System.Drawing.Size(145, 21);
            this.tb_selectvalue.TabIndex = 2;
            this.tb_selectvalue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_selectvalue_KeyDown);
            // 
            // cmb_selecttype
            // 
            this.cmb_selecttype.DisplayMember = "Text";
            this.cmb_selecttype.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_selecttype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_selecttype.FormattingEnabled = true;
            this.cmb_selecttype.ItemHeight = 15;
            this.cmb_selecttype.Location = new System.Drawing.Point(92, 10);
            this.cmb_selecttype.Name = "cmb_selecttype";
            this.cmb_selecttype.Size = new System.Drawing.Size(103, 21);
            this.cmb_selecttype.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmb_selecttype.TabIndex = 1;
            this.cmb_selecttype.DropDown += new System.EventHandler(this.cmb_selecttype_DropDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[检索类型]:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_showdata);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(580, 283);
            this.panel2.TabIndex = 1;
            // 
            // dgv_showdata
            // 
            this.dgv_showdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_showdata.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_showdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_showdata.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_showdata.Location = new System.Drawing.Point(0, 0);
            this.dgv_showdata.Name = "dgv_showdata";
            this.dgv_showdata.ReadOnly = true;
            this.dgv_showdata.RowTemplate.Height = 23;
            this.dgv_showdata.Size = new System.Drawing.Size(580, 283);
            this.dgv_showdata.TabIndex = 0;
            this.dgv_showdata.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_showdata_CellMouseDoubleClick);
            // 
            // SelectData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 324);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SelectData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据选择";
            this.Load += new System.EventHandler(this.SelectData_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showdata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_selectvalue;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmb_selecttype;
        private DevComponents.DotNetBar.ButtonX bt_query;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_showdata;
    }
}