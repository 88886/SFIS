namespace SFIS_V2
{
    partial class Frm_TEST_MAIN_ONLY
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
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.imbt_Select_line = new DevComponents.DotNetBar.ButtonItem();
            this.panelStation = new DevComponents.DotNetBar.PanelEx();
            this.LabStation = new DevComponents.DotNetBar.LabelX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.LabStatus = new DevComponents.DotNetBar.LabelX();
            this.LabUser = new DevComponents.DotNetBar.LabelX();
            this.txt_Pass_input = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.LabStatusreject = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txt_Fail_Input = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelStation.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.imbt_Select_line});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(897, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // imbt_Select_line
            // 
            this.imbt_Select_line.Name = "imbt_Select_line";
            this.imbt_Select_line.Text = "选择线体";
            this.imbt_Select_line.Click += new System.EventHandler(this.imbt_Select_line_Click);
            // 
            // panelStation
            // 
            this.panelStation.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelStation.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelStation.Controls.Add(this.LabStation);
            this.panelStation.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelStation.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStation.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelStation.Location = new System.Drawing.Point(0, 25);
            this.panelStation.Name = "panelStation";
            this.panelStation.Size = new System.Drawing.Size(897, 68);
            this.panelStation.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelStation.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelStation.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelStation.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelStation.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelStation.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelStation.Style.GradientAngle = 90;
            this.panelStation.TabIndex = 1;
            // 
            // LabStation
            // 
            // 
            // 
            // 
            this.LabStation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabStation.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabStation.ForeColor = System.Drawing.Color.Blue;
            this.LabStation.Location = new System.Drawing.Point(0, 0);
            this.LabStation.Name = "LabStation";
            this.LabStation.SingleLineColor = System.Drawing.SystemColors.AppWorkspace;
            this.LabStation.Size = new System.Drawing.Size(897, 68);
            this.LabStation.TabIndex = 0;
            this.LabStation.Text = "S080101:PACK_CTN";
            this.LabStation.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.splitContainer1);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 93);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(897, 322);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 5;
            this.panelEx1.Text = "panelEx1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(897, 322);
            this.splitContainer1.SplitterDistance = 444;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.LabStatus);
            this.groupPanel1.Controls.Add(this.LabUser);
            this.groupPanel1.Controls.Add(this.txt_Pass_input);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(444, 322);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "良品";
            // 
            // LabStatus
            // 
            // 
            // 
            // 
            this.LabStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LabStatus.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabStatus.Location = new System.Drawing.Point(0, 110);
            this.LabStatus.Name = "LabStatus";
            this.LabStatus.Size = new System.Drawing.Size(438, 188);
            this.LabStatus.TabIndex = 2;
            this.LabStatus.Text = "OK";
            this.LabStatus.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // LabUser
            // 
            // 
            // 
            // 
            this.LabUser.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabUser.Location = new System.Drawing.Point(66, 18);
            this.LabUser.Name = "LabUser";
            this.LabUser.Size = new System.Drawing.Size(75, 23);
            this.LabUser.TabIndex = 1;
            this.LabUser.Text = "labelX1";
            // 
            // txt_Pass_input
            // 
            // 
            // 
            // 
            this.txt_Pass_input.Border.Class = "TextBoxBorder";
            this.txt_Pass_input.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_Pass_input.Location = new System.Drawing.Point(66, 56);
            this.txt_Pass_input.Name = "txt_Pass_input";
            this.txt_Pass_input.PreventEnterBeep = true;
            this.txt_Pass_input.Size = new System.Drawing.Size(217, 21);
            this.txt_Pass_input.TabIndex = 0;
            this.txt_Pass_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Pass_input_KeyDown);
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.LabStatusreject);
            this.groupPanel2.Controls.Add(this.labelX1);
            this.groupPanel2.Controls.Add(this.txt_Fail_Input);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel2.Location = new System.Drawing.Point(0, 0);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(449, 322);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 1;
            this.groupPanel2.Text = "不良品";
            // 
            // LabStatusreject
            // 
            // 
            // 
            // 
            this.LabStatusreject.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabStatusreject.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LabStatusreject.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabStatusreject.Location = new System.Drawing.Point(0, 110);
            this.LabStatusreject.Name = "LabStatusreject";
            this.LabStatusreject.Size = new System.Drawing.Size(443, 188);
            this.LabStatusreject.TabIndex = 3;
            this.LabStatusreject.Text = "OK";
            this.LabStatusreject.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(64, 27);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(177, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "不良代码-->产品序列号";
            // 
            // txt_Fail_Input
            // 
            // 
            // 
            // 
            this.txt_Fail_Input.Border.Class = "TextBoxBorder";
            this.txt_Fail_Input.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_Fail_Input.Location = new System.Drawing.Point(64, 56);
            this.txt_Fail_Input.Name = "txt_Fail_Input";
            this.txt_Fail_Input.PreventEnterBeep = true;
            this.txt_Fail_Input.Size = new System.Drawing.Size(217, 21);
            this.txt_Fail_Input.TabIndex = 0;
            this.txt_Fail_Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Fail_Input_KeyDown);
            // 
            // Frm_TEST_MAIN_ONLY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 415);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.panelStation);
            this.Controls.Add(this.bar1);
            this.Name = "Frm_TEST_MAIN_ONLY";
            this.Text = "Frm_TEST_MAIN_ONLY";
            this.Load += new System.EventHandler(this.Frm_TEST_MAIN_ONLY_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelStation.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem imbt_Select_line;
        private DevComponents.DotNetBar.PanelEx panelStation;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX LabStation;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevComponents.DotNetBar.LabelX LabUser;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_Pass_input;
        private DevComponents.DotNetBar.LabelX LabStatus;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_Fail_Input;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX LabStatusreject;
    }
}