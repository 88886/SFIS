namespace SFIS_V2
{
    partial class StationNoInfo
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
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_showstatonnoinfo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bt_addmachine = new DevComponents.DotNetBar.ButtonX();
            this.bt_addstation = new DevComponents.DotNetBar.ButtonX();
            this.bt_deletestation = new DevComponents.DotNetBar.ButtonX();
            this.cb_machineId = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cb_lineId = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.tb_spec = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_note = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tb_stationno = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showstatonnoinfo)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(665, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_showstatonnoinfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(665, 217);
            this.panel1.TabIndex = 1;
            // 
            // dgv_showstatonnoinfo
            // 
            this.dgv_showstatonnoinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_showstatonnoinfo.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_showstatonnoinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_showstatonnoinfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_showstatonnoinfo.Location = new System.Drawing.Point(0, 0);
            this.dgv_showstatonnoinfo.Name = "dgv_showstatonnoinfo";
            this.dgv_showstatonnoinfo.RowTemplate.Height = 23;
            this.dgv_showstatonnoinfo.Size = new System.Drawing.Size(665, 217);
            this.dgv_showstatonnoinfo.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bt_addmachine);
            this.panel2.Controls.Add(this.bt_addstation);
            this.panel2.Controls.Add(this.bt_deletestation);
            this.panel2.Controls.Add(this.cb_machineId);
            this.panel2.Controls.Add(this.cb_lineId);
            this.panel2.Controls.Add(this.tb_spec);
            this.panel2.Controls.Add(this.tb_note);
            this.panel2.Controls.Add(this.tb_stationno);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 242);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(665, 181);
            this.panel2.TabIndex = 2;
            // 
            // bt_addmachine
            // 
            this.bt_addmachine.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_addmachine.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_addmachine.Location = new System.Drawing.Point(575, 33);
            this.bt_addmachine.Name = "bt_addmachine";
            this.bt_addmachine.Size = new System.Drawing.Size(59, 23);
            this.bt_addmachine.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_addmachine.TabIndex = 5;
            this.bt_addmachine.Text = "新增机器";
            this.bt_addmachine.Click += new System.EventHandler(this.bt_addmachine_Click);
            // 
            // bt_addstation
            // 
            this.bt_addstation.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_addstation.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_addstation.Location = new System.Drawing.Point(494, 137);
            this.bt_addstation.Name = "bt_addstation";
            this.bt_addstation.Size = new System.Drawing.Size(75, 23);
            this.bt_addstation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_addstation.TabIndex = 4;
            this.bt_addstation.Text = "增加";
            this.bt_addstation.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // bt_deletestation
            // 
            this.bt_deletestation.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_deletestation.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_deletestation.Location = new System.Drawing.Point(64, 137);
            this.bt_deletestation.Name = "bt_deletestation";
            this.bt_deletestation.Size = new System.Drawing.Size(75, 23);
            this.bt_deletestation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_deletestation.TabIndex = 4;
            this.bt_deletestation.Text = "删除";
            this.bt_deletestation.Click += new System.EventHandler(this.bt_deletestation_Click);
            // 
            // cb_machineId
            // 
            this.cb_machineId.DisplayMember = "Text";
            this.cb_machineId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_machineId.FormattingEnabled = true;
            this.cb_machineId.ItemHeight = 15;
            this.cb_machineId.Location = new System.Drawing.Point(433, 33);
            this.cb_machineId.Name = "cb_machineId";
            this.cb_machineId.Size = new System.Drawing.Size(136, 21);
            this.cb_machineId.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_machineId.TabIndex = 3;
            this.cb_machineId.DropDown += new System.EventHandler(this.cb_machineId_DropDown);
            // 
            // cb_lineId
            // 
            this.cb_lineId.DisplayMember = "Text";
            this.cb_lineId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_lineId.FormattingEnabled = true;
            this.cb_lineId.ItemHeight = 15;
            this.cb_lineId.Location = new System.Drawing.Point(236, 33);
            this.cb_lineId.Name = "cb_lineId";
            this.cb_lineId.Size = new System.Drawing.Size(141, 21);
            this.cb_lineId.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_lineId.TabIndex = 3;
            this.cb_lineId.DropDown += new System.EventHandler(this.comboBoxEx1_DropDown);
            // 
            // tb_spec
            // 
            // 
            // 
            // 
            this.tb_spec.Border.Class = "TextBoxBorder";
            this.tb_spec.Location = new System.Drawing.Point(27, 97);
            this.tb_spec.Name = "tb_spec";
            this.tb_spec.Size = new System.Drawing.Size(154, 21);
            this.tb_spec.TabIndex = 2;
            // 
            // tb_note
            // 
            // 
            // 
            // 
            this.tb_note.Border.Class = "TextBoxBorder";
            this.tb_note.Location = new System.Drawing.Point(236, 97);
            this.tb_note.Name = "tb_note";
            this.tb_note.Size = new System.Drawing.Size(333, 21);
            this.tb_note.TabIndex = 2;
            // 
            // tb_stationno
            // 
            // 
            // 
            // 
            this.tb_stationno.Border.Class = "TextBoxBorder";
            this.tb_stationno.Location = new System.Drawing.Point(27, 33);
            this.tb_stationno.Name = "tb_stationno";
            this.tb_stationno.Size = new System.Drawing.Size(154, 21);
            this.tb_stationno.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "[规格]:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "[描述]:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(432, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "[机器/工站编号]:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "[产线编号]:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[料站编号]:";
            // 
            // StationNoInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 423);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "StationNoInfo";
            this.Text = "StationNoInfo";
            this.Load += new System.EventHandler(this.StationNoInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_showstatonnoinfo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_showstatonnoinfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX bt_addstation;
        private DevComponents.DotNetBar.ButtonX bt_deletestation;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_machineId;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_lineId;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_spec;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_note;
        private DevComponents.DotNetBar.Controls.TextBoxX tb_stationno;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.ButtonX bt_addmachine;
    }
}