namespace SFIS_V2
{
    partial class FrmTargetPlan
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btdelete = new DevComponents.DotNetBar.ButtonX();
            this.btadded = new DevComponents.DotNetBar.ButtonX();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbside = new System.Windows.Forms.ComboBox();
            this.LBok = new DevComponents.DotNetBar.LabelX();
            this.lbcancel = new DevComponents.DotNetBar.LabelX();
            this.tbTargetQty = new System.Windows.Forms.TextBox();
            this.lbPartNumber = new System.Windows.Forms.Label();
            this.cbstationlist = new System.Windows.Forms.ComboBox();
            this.cbwolist = new System.Windows.Forms.ComboBox();
            this.cblinelist = new System.Windows.Forms.ComboBox();
            this.cbClass = new System.Windows.Forms.ComboBox();
            this.dtpWorkdate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.DgvTarget = new System.Windows.Forms.DataGridView();
            this.Rowid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工作日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.班别 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.站位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工单 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.产品料号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.线别 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.目标产出 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.开始时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.结束时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.面别 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cb_WorkTime = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.Cb_EndStation = new System.Windows.Forms.ComboBox();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpWorkdate)).BeginInit();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTarget)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btdelete);
            this.panelEx1.Controls.Add(this.btadded);
            this.panelEx1.Controls.Add(this.textBox1);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1030, 80);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btdelete
            // 
            this.btdelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btdelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btdelete.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btdelete.Location = new System.Drawing.Point(759, 17);
            this.btdelete.Name = "btdelete";
            this.btdelete.Size = new System.Drawing.Size(75, 52);
            this.btdelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btdelete.TabIndex = 2;
            this.btdelete.Text = "删除";
            this.btdelete.Click += new System.EventHandler(this.btdelete_Click);
            // 
            // btadded
            // 
            this.btadded.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btadded.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btadded.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btadded.Location = new System.Drawing.Point(626, 17);
            this.btadded.Name = "btadded";
            this.btadded.Size = new System.Drawing.Size(75, 52);
            this.btadded.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btadded.TabIndex = 2;
            this.btadded.Text = "新增";
            this.btadded.Click += new System.EventHandler(this.btadded_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(99, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(331, 29);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "工单:";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panel1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 227);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1030, 212);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbside);
            this.panel1.Controls.Add(this.LBok);
            this.panel1.Controls.Add(this.lbcancel);
            this.panel1.Controls.Add(this.tbTargetQty);
            this.panel1.Controls.Add(this.lbPartNumber);
            this.panel1.Controls.Add(this.Cb_EndStation);
            this.panel1.Controls.Add(this.cbstationlist);
            this.panel1.Controls.Add(this.cbwolist);
            this.panel1.Controls.Add(this.Cb_WorkTime);
            this.panel1.Controls.Add(this.cblinelist);
            this.panel1.Controls.Add(this.cbClass);
            this.panel1.Controls.Add(this.dtpWorkdate);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1030, 212);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // tbside
            // 
            this.tbside.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbside.FormattingEnabled = true;
            this.tbside.Items.AddRange(new object[] {
            "T",
            "B"});
            this.tbside.Location = new System.Drawing.Point(580, 138);
            this.tbside.Name = "tbside";
            this.tbside.Size = new System.Drawing.Size(146, 22);
            this.tbside.TabIndex = 8;
            // 
            // LBok
            // 
            this.LBok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.LBok.BackgroundStyle.Class = "";
            this.LBok.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LBok.Location = new System.Drawing.Point(858, 84);
            this.LBok.Name = "LBok";
            this.LBok.Size = new System.Drawing.Size(90, 23);
            this.LBok.TabIndex = 7;
            this.LBok.Text = "OK";
            this.LBok.TextAlignment = System.Drawing.StringAlignment.Center;
            this.LBok.Click += new System.EventHandler(this.LBok_Click_1);
            this.LBok.MouseEnter += new System.EventHandler(this.LBok_MouseEnter);
            this.LBok.MouseLeave += new System.EventHandler(this.LBok_MouseLeave);
            // 
            // lbcancel
            // 
            this.lbcancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.lbcancel.BackgroundStyle.Class = "";
            this.lbcancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbcancel.Location = new System.Drawing.Point(858, 140);
            this.lbcancel.Name = "lbcancel";
            this.lbcancel.Size = new System.Drawing.Size(90, 23);
            this.lbcancel.TabIndex = 6;
            this.lbcancel.Text = "Cancel";
            this.lbcancel.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lbcancel.Click += new System.EventHandler(this.lbcancel_Click);
            this.lbcancel.MouseEnter += new System.EventHandler(this.lbcancel_MouseEnter);
            this.lbcancel.MouseLeave += new System.EventHandler(this.lbcancel_MouseLeave);
            // 
            // tbTargetQty
            // 
            this.tbTargetQty.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTargetQty.Location = new System.Drawing.Point(582, 86);
            this.tbTargetQty.Name = "tbTargetQty";
            this.tbTargetQty.Size = new System.Drawing.Size(144, 23);
            this.tbTargetQty.TabIndex = 5;
            this.tbTargetQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTargetQty_KeyPress);
            // 
            // lbPartNumber
            // 
            this.lbPartNumber.AutoSize = true;
            this.lbPartNumber.BackColor = System.Drawing.Color.MediumAquamarine;
            this.lbPartNumber.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPartNumber.Location = new System.Drawing.Point(83, 89);
            this.lbPartNumber.Name = "lbPartNumber";
            this.lbPartNumber.Size = new System.Drawing.Size(89, 16);
            this.lbPartNumber.TabIndex = 4;
            this.lbPartNumber.Text = "901000888";
            // 
            // cbstationlist
            // 
            this.cbstationlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbstationlist.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbstationlist.FormattingEnabled = true;
            this.cbstationlist.Location = new System.Drawing.Point(582, 33);
            this.cbstationlist.Name = "cbstationlist";
            this.cbstationlist.Size = new System.Drawing.Size(144, 22);
            this.cbstationlist.TabIndex = 3;
            // 
            // cbwolist
            // 
            this.cbwolist.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbwolist.FormattingEnabled = true;
            this.cbwolist.Location = new System.Drawing.Point(84, 35);
            this.cbwolist.Name = "cbwolist";
            this.cbwolist.Size = new System.Drawing.Size(145, 22);
            this.cbwolist.TabIndex = 3;
            this.cbwolist.SelectionChangeCommitted += new System.EventHandler(this.cbwolist_SelectionChangeCommitted);
            this.cbwolist.Validating += new System.ComponentModel.CancelEventHandler(this.cbwolist_Validating);
            // 
            // cblinelist
            // 
            this.cblinelist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cblinelist.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cblinelist.FormattingEnabled = true;
            this.cblinelist.Location = new System.Drawing.Point(333, 87);
            this.cblinelist.Name = "cblinelist";
            this.cblinelist.Size = new System.Drawing.Size(145, 22);
            this.cblinelist.TabIndex = 3;
            // 
            // cbClass
            // 
            this.cbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClass.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbClass.FormattingEnabled = true;
            this.cbClass.Items.AddRange(new object[] {
            "D",
            "N"});
            this.cbClass.Location = new System.Drawing.Point(333, 35);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(145, 22);
            this.cbClass.TabIndex = 3;
            // 
            // dtpWorkdate
            // 
            // 
            // 
            // 
            this.dtpWorkdate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpWorkdate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpWorkdate.ButtonDropDown.Visible = true;
            this.dtpWorkdate.Location = new System.Drawing.Point(80, 137);
            this.dtpWorkdate.MinDate = new System.DateTime(2012, 7, 2, 0, 0, 0, 0);
            // 
            // 
            // 
            this.dtpWorkdate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpWorkdate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtpWorkdate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtpWorkdate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpWorkdate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpWorkdate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpWorkdate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpWorkdate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpWorkdate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpWorkdate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpWorkdate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtpWorkdate.MonthCalendar.DisplayMonth = new System.DateTime(2012, 10, 1, 0, 0, 0, 0);
            this.dtpWorkdate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpWorkdate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpWorkdate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpWorkdate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpWorkdate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpWorkdate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtpWorkdate.MonthCalendar.TodayButtonVisible = true;
            this.dtpWorkdate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpWorkdate.Name = "dtpWorkdate";
            this.dtpWorkdate.Size = new System.Drawing.Size(117, 21);
            this.dtpWorkdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpWorkdate.TabIndex = 2;
            this.dtpWorkdate.Value = new System.DateTime(2012, 10, 8, 0, 0, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(5, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "产品料号:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(33, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "工单:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(506, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "开始途程:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(533, 146);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 14);
            this.label11.TabIndex = 0;
            this.label11.Text = "面别:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(3, 189);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(210, 14);
            this.label12.TabIndex = 0;
            this.label12.Text = "时间格式为24小时制 HHMM(1330)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(256, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "生产时间:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(504, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 14);
            this.label8.TabIndex = 0;
            this.label8.Text = "目标产出:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(284, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = "线体:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(284, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "班别:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(5, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "工作日期:";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.DgvTarget);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 80);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1030, 147);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 2;
            // 
            // DgvTarget
            // 
            this.DgvTarget.AllowUserToAddRows = false;
            this.DgvTarget.AllowUserToDeleteRows = false;
            this.DgvTarget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvTarget.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Rowid,
            this.工作日期,
            this.班别,
            this.站位,
            this.工单,
            this.产品料号,
            this.线别,
            this.目标产出,
            this.开始时间,
            this.结束时间,
            this.面别});
            this.DgvTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvTarget.Location = new System.Drawing.Point(0, 0);
            this.DgvTarget.MultiSelect = false;
            this.DgvTarget.Name = "DgvTarget";
            this.DgvTarget.ReadOnly = true;
            this.DgvTarget.RowTemplate.Height = 23;
            this.DgvTarget.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvTarget.Size = new System.Drawing.Size(1030, 147);
            this.DgvTarget.TabIndex = 0;
            this.DgvTarget.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvTarget_CellMouseClick);
            this.DgvTarget.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvTarget_CellMouseDoubleClick);
            // 
            // Rowid
            // 
            this.Rowid.DataPropertyName = "ROWID";
            this.Rowid.HeaderText = "ROWID";
            this.Rowid.Name = "Rowid";
            this.Rowid.ReadOnly = true;
            this.Rowid.Visible = false;
            // 
            // 工作日期
            // 
            this.工作日期.DataPropertyName = "WorkDate";
            this.工作日期.HeaderText = "工作日期";
            this.工作日期.Name = "工作日期";
            this.工作日期.ReadOnly = true;
            // 
            // 班别
            // 
            this.班别.DataPropertyName = "Class";
            this.班别.HeaderText = "班别";
            this.班别.Name = "班别";
            this.班别.ReadOnly = true;
            // 
            // 站位
            // 
            this.站位.DataPropertyName = "Locstation";
            this.站位.HeaderText = "站位";
            this.站位.Name = "站位";
            this.站位.ReadOnly = true;
            // 
            // 工单
            // 
            this.工单.DataPropertyName = "woId";
            this.工单.HeaderText = "工单";
            this.工单.Name = "工单";
            this.工单.ReadOnly = true;
            // 
            // 产品料号
            // 
            this.产品料号.DataPropertyName = "PartNumber";
            this.产品料号.HeaderText = "产品料号";
            this.产品料号.Name = "产品料号";
            this.产品料号.ReadOnly = true;
            // 
            // 线别
            // 
            this.线别.DataPropertyName = "Line";
            this.线别.HeaderText = "线别";
            this.线别.Name = "线别";
            this.线别.ReadOnly = true;
            // 
            // 目标产出
            // 
            this.目标产出.DataPropertyName = "TargetQty";
            this.目标产出.HeaderText = "目标产出";
            this.目标产出.Name = "目标产出";
            this.目标产出.ReadOnly = true;
            // 
            // 开始时间
            // 
            this.开始时间.DataPropertyName = "StartTime";
            this.开始时间.HeaderText = "开始时间";
            this.开始时间.Name = "开始时间";
            this.开始时间.ReadOnly = true;
            // 
            // 结束时间
            // 
            this.结束时间.DataPropertyName = "EndTime";
            this.结束时间.HeaderText = "结束时间";
            this.结束时间.Name = "结束时间";
            this.结束时间.ReadOnly = true;
            // 
            // 面别
            // 
            this.面别.DataPropertyName = "Side";
            this.面别.HeaderText = "面别";
            this.面别.Name = "面别";
            this.面别.ReadOnly = true;
            // 
            // Cb_WorkTime
            // 
            this.Cb_WorkTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cb_WorkTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cb_WorkTime.FormattingEnabled = true;
            this.Cb_WorkTime.Location = new System.Drawing.Point(332, 133);
            this.Cb_WorkTime.Name = "Cb_WorkTime";
            this.Cb_WorkTime.Size = new System.Drawing.Size(145, 22);
            this.Cb_WorkTime.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(747, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 14);
            this.label13.TabIndex = 0;
            this.label13.Text = "结束途程:";
            // 
            // Cb_EndStation
            // 
            this.Cb_EndStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cb_EndStation.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cb_EndStation.FormattingEnabled = true;
            this.Cb_EndStation.Location = new System.Drawing.Point(823, 33);
            this.Cb_EndStation.Name = "Cb_EndStation";
            this.Cb_EndStation.Size = new System.Drawing.Size(144, 22);
            this.Cb_EndStation.TabIndex = 3;
            // 
            // FrmTargetPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 439);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmTargetPlan";
            this.Text = "生产目标设定";
            this.Load += new System.EventHandler(this.FrmTargetPlan_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpWorkdate)).EndInit();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvTarget)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.DataGridView DgvTarget;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpWorkdate;
        private System.Windows.Forms.ComboBox cbstationlist;
        private System.Windows.Forms.ComboBox cbClass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbPartNumber;
        private System.Windows.Forms.ComboBox cbwolist;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbTargetQty;
        private System.Windows.Forms.ComboBox cblinelist;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private DevComponents.DotNetBar.ButtonX btadded;
        private DevComponents.DotNetBar.ButtonX btdelete;
        private DevComponents.DotNetBar.LabelX lbcancel;
        private DevComponents.DotNetBar.LabelX LBok;
        private System.Windows.Forms.ComboBox tbside;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rowid;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工作日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 班别;
        private System.Windows.Forms.DataGridViewTextBoxColumn 站位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工单;
        private System.Windows.Forms.DataGridViewTextBoxColumn 产品料号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 线别;
        private System.Windows.Forms.DataGridViewTextBoxColumn 目标产出;
        private System.Windows.Forms.DataGridViewTextBoxColumn 开始时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 结束时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 面别;
        private System.Windows.Forms.ComboBox Cb_EndStation;
        private System.Windows.Forms.ComboBox Cb_WorkTime;
        private System.Windows.Forms.Label label13;
    }
}