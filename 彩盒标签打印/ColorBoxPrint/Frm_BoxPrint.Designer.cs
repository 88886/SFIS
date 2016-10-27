namespace ColorBoxPrint
{
    partial class Frm_BoxPrint
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
            this.components = new System.ComponentModel.Container();
            this.tb_Input = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.rdesn = new System.Windows.Forms.RadioButton();
            this.rdIMEI = new System.Windows.Forms.RadioButton();
            this.tb_wo = new System.Windows.Forms.TextBox();
            this.LabColor = new System.Windows.Forms.Label();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cb_ListRoute = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.comboItem10 = new DevComponents.Editors.ComboItem();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_chgwo = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_RePrint = new DevComponents.DotNetBar.ButtonItem();
            this.imbt_SelectLine = new DevComponents.DotNetBar.ButtonItem();
            this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.NumY = new System.Windows.Forms.NumericUpDown();
            this.NumX = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.LabLine = new DevComponents.DotNetBar.LabelX();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumX)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_Input
            // 
            this.tb_Input.Location = new System.Drawing.Point(53, 252);
            this.tb_Input.Name = "tb_Input";
            this.tb_Input.Size = new System.Drawing.Size(192, 21);
            this.tb_Input.TabIndex = 0;
            this.tb_Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Input_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "输入:";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(43, 51);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(40, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "工单:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbmsg);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 289);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 151);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "显示信息";
            // 
            // rtbmsg
            // 
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbmsg.Location = new System.Drawing.Point(3, 17);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.Size = new System.Drawing.Size(368, 131);
            this.rtbmsg.TabIndex = 0;
            this.rtbmsg.Text = "";
            // 
            // rdesn
            // 
            this.rdesn.AutoSize = true;
            this.rdesn.Checked = true;
            this.rdesn.Location = new System.Drawing.Point(53, 167);
            this.rdesn.Name = "rdesn";
            this.rdesn.Size = new System.Drawing.Size(41, 16);
            this.rdesn.TabIndex = 4;
            this.rdesn.TabStop = true;
            this.rdesn.Text = "ESN";
            this.rdesn.UseVisualStyleBackColor = true;
            // 
            // rdIMEI
            // 
            this.rdIMEI.AutoSize = true;
            this.rdIMEI.Location = new System.Drawing.Point(53, 195);
            this.rdIMEI.Name = "rdIMEI";
            this.rdIMEI.Size = new System.Drawing.Size(47, 16);
            this.rdIMEI.TabIndex = 5;
            this.rdIMEI.Text = "IMEI";
            this.rdIMEI.UseVisualStyleBackColor = true;
            // 
            // tb_wo
            // 
            this.tb_wo.Location = new System.Drawing.Point(92, 48);
            this.tb_wo.Name = "tb_wo";
            this.tb_wo.Size = new System.Drawing.Size(147, 21);
            this.tb_wo.TabIndex = 6;
            this.tb_wo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_wo_KeyDown);
            // 
            // LabColor
            // 
            this.LabColor.AutoSize = true;
            this.LabColor.Location = new System.Drawing.Point(96, 76);
            this.LabColor.Name = "LabColor";
            this.LabColor.Size = new System.Drawing.Size(113, 12);
            this.LabColor.TabIndex = 8;
            this.LabColor.Text = "------------------";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(45, 94);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(40, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "线体:";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(45, 134);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(40, 23);
            this.labelX4.TabIndex = 2;
            this.labelX4.Text = "途程:";
            // 
            // cb_ListRoute
            // 
            this.cb_ListRoute.DisplayMember = "Text";
            this.cb_ListRoute.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_ListRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ListRoute.FormattingEnabled = true;
            this.cb_ListRoute.ItemHeight = 15;
            this.cb_ListRoute.Items.AddRange(new object[] {
            this.comboItem6,
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem10});
            this.cb_ListRoute.Location = new System.Drawing.Point(91, 134);
            this.cb_ListRoute.Name = "cb_ListRoute";
            this.cb_ListRoute.Size = new System.Drawing.Size(149, 21);
            this.cb_ListRoute.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_ListRoute.TabIndex = 13;
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "comboItem1";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "comboItem2";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "comboItem3";
            // 
            // comboItem9
            // 
            this.comboItem9.Text = "comboItem4";
            // 
            // comboItem10
            // 
            this.comboItem10.Text = "comboItem5";
            // 
            // bar1
            // 
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(374, 26);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 15;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // buttonItem1
            // 
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.imbt_chgwo,
            this.imbt_RePrint,
            this.imbt_SelectLine});
            this.buttonItem1.Text = "Option";
            // 
            // imbt_chgwo
            // 
            this.imbt_chgwo.Name = "imbt_chgwo";
            this.imbt_chgwo.Text = "切换工单";
            this.imbt_chgwo.Click += new System.EventHandler(this.imbt_chgwo_Click);
            // 
            // imbt_RePrint
            // 
            this.imbt_RePrint.Name = "imbt_RePrint";
            this.imbt_RePrint.Text = "重复打印";
            this.imbt_RePrint.Click += new System.EventHandler(this.imbt_RePrint_Click);
            // 
            // imbt_SelectLine
            // 
            this.imbt_SelectLine.Name = "imbt_SelectLine";
            this.imbt_SelectLine.Text = "选择线体";
            this.imbt_SelectLine.Click += new System.EventHandler(this.imbt_SelectLine_Click);
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = false;
            this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.NumY);
            this.panel3.Controls.Add(this.NumX);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(107, 161);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(138, 60);
            this.panel3.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(52, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 16);
            this.label9.TabIndex = 16;
            this.label9.Text = "/";
            // 
            // NumY
            // 
            this.NumY.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NumY.Location = new System.Drawing.Point(74, 29);
            this.NumY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.NumY.Name = "NumY";
            this.NumY.Size = new System.Drawing.Size(42, 21);
            this.NumY.TabIndex = 17;
            this.NumY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumY.Leave += new System.EventHandler(this.NumY_Leave);
            // 
            // NumX
            // 
            this.NumX.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NumX.Location = new System.Drawing.Point(5, 28);
            this.NumX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.NumX.Name = "NumX";
            this.NumX.Size = new System.Drawing.Size(41, 21);
            this.NumX.TabIndex = 15;
            this.NumX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumX.Leave += new System.EventHandler(this.NumX_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(9, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 14);
            this.label8.TabIndex = 14;
            this.label8.Text = "[坐标X/坐标Y]";
            // 
            // LabLine
            // 
            // 
            // 
            // 
            this.LabLine.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabLine.Location = new System.Drawing.Point(92, 94);
            this.LabLine.Name = "LabLine";
            this.LabLine.Size = new System.Drawing.Size(147, 23);
            this.LabLine.TabIndex = 2;
            this.LabLine.Text = "线体:";
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // Frm_BoxPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 440);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.cb_ListRoute);
            this.Controls.Add(this.LabColor);
            this.Controls.Add(this.tb_wo);
            this.Controls.Add(this.rdIMEI);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.rdesn);
            this.Controls.Add(this.LabLine);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Input);
            this.Controls.Add(this.bar1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Name = "Frm_BoxPrint";
            this.Text = "彩盒标签打印";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_BoxPrint_FormClosing);
            this.Load += new System.EventHandler(this.Frm_BoxPrint_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_Input;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbmsg;
        private System.Windows.Forms.Label LabColor;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_ListRoute;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.Editors.ComboItem comboItem10;
        public System.Windows.Forms.RadioButton rdesn;
        public System.Windows.Forms.RadioButton rdIMEI;
        public System.Windows.Forms.TextBox tb_wo;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem imbt_chgwo;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
        private DevComponents.DotNetBar.ButtonItem imbt_RePrint;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown NumY;
        private System.Windows.Forms.NumericUpDown NumX;
        private System.Windows.Forms.Label label8;
        private DevComponents.DotNetBar.LabelX LabLine;
        private DevComponents.DotNetBar.ButtonItem imbt_SelectLine;
        private DevComponents.DotNetBar.StyleManager styleManager1;
    }
}