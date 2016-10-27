namespace SFIS_V2
{
    partial class Frm_StationName
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StationName));
            this.chkLine = new System.Windows.Forms.CheckBox();
            this.cb_line = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_station = new System.Windows.Forms.TextBox();
            this.txt_group = new System.Windows.Forms.TextBox();
            this.txt_section = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imbt_cancel = new DevComponents.DotNetBar.ButtonX();
            this.imbt_ok = new DevComponents.DotNetBar.ButtonX();
            this.dgvstation = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvstation)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkLine
            // 
            resources.ApplyResources(this.chkLine, "chkLine");
            this.chkLine.Name = "chkLine";
            this.chkLine.UseVisualStyleBackColor = true;
            this.chkLine.Click += new System.EventHandler(this.chkLine_Click);
            // 
            // cb_line
            // 
            resources.ApplyResources(this.cb_line, "cb_line");
            this.cb_line.DisplayMember = "Text";
            this.cb_line.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_line.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_line.FormattingEnabled = true;
            this.cb_line.Name = "cb_line";
            this.cb_line.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.cb_line);
            this.groupBox1.Controls.Add(this.chkLine);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.txt_station);
            this.groupBox2.Controls.Add(this.txt_group);
            this.groupBox2.Controls.Add(this.txt_section);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txt_station
            // 
            resources.ApplyResources(this.txt_station, "txt_station");
            this.txt_station.Name = "txt_station";
            this.txt_station.ReadOnly = true;
            // 
            // txt_group
            // 
            resources.ApplyResources(this.txt_group, "txt_group");
            this.txt_group.Name = "txt_group";
            this.txt_group.ReadOnly = true;
            // 
            // txt_section
            // 
            resources.ApplyResources(this.txt_section, "txt_section");
            this.txt_section.Name = "txt_section";
            this.txt_section.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.imbt_cancel);
            this.panel1.Controls.Add(this.imbt_ok);
            this.panel1.Name = "panel1";
            // 
            // imbt_cancel
            // 
            resources.ApplyResources(this.imbt_cancel, "imbt_cancel");
            this.imbt_cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_cancel.Name = "imbt_cancel";
            this.imbt_cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_cancel.Click += new System.EventHandler(this.imbt_cancel_Click);
            // 
            // imbt_ok
            // 
            resources.ApplyResources(this.imbt_ok, "imbt_ok");
            this.imbt_ok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_ok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_ok.Name = "imbt_ok";
            this.imbt_ok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_ok.Click += new System.EventHandler(this.imbt_ok_Click);
            // 
            // dgvstation
            // 
            resources.ApplyResources(this.dgvstation, "dgvstation");
            this.dgvstation.AllowUserToAddRows = false;
            this.dgvstation.AllowUserToDeleteRows = false;
            this.dgvstation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvstation.Name = "dgvstation";
            this.dgvstation.ReadOnly = true;
            this.dgvstation.RowTemplate.Height = 23;
            this.dgvstation.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvstation_CellMouseClick);
            this.dgvstation.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvstation_CellMouseDoubleClick);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.dgvstation);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // Frm_StationName
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_StationName";
            this.Load += new System.EventHandler(this.Frm_StationName_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvstation)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkLine;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_line;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_station;
        private System.Windows.Forms.TextBox txt_group;
        private System.Windows.Forms.TextBox txt_section;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX imbt_cancel;
        private DevComponents.DotNetBar.ButtonX imbt_ok;
        private System.Windows.Forms.DataGridView dgvstation;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}