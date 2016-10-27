namespace SFIS_DCT_V2
{
    partial class SFIS_DCT_V2
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PalConfig = new DevComponents.DotNetBar.PanelEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbproback = new System.Windows.Forms.ComboBox();
            this.cbprocedures = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Butok = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbline = new System.Windows.Forms.ComboBox();
            this.cbroute = new System.Windows.Forms.ComboBox();
            this.btsetup = new System.Windows.Forms.Button();
            this.PalData = new DevComponents.DotNetBar.PanelEx();
            this.chkimei = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbInputData = new System.Windows.Forms.TextBox();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.hooksetup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hook = new System.Windows.Forms.ToolStripMenuItem();
            this.unhook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmtopmosttrue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmtopmostfalse = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.hookstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.comstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.PalConfig.SuspendLayout();
            this.panel1.SuspendLayout();
            this.PalData.SuspendLayout();
            this.hooksetup.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PalConfig
            // 
            this.PalConfig.CanvasColor = System.Drawing.SystemColors.Control;
            this.PalConfig.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.PalConfig.Controls.Add(this.panel1);
            this.PalConfig.Controls.Add(this.Butok);
            this.PalConfig.Controls.Add(this.label1);
            this.PalConfig.Controls.Add(this.label2);
            this.PalConfig.Controls.Add(this.cbline);
            this.PalConfig.Controls.Add(this.cbroute);
            this.PalConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.PalConfig.Location = new System.Drawing.Point(0, 0);
            this.PalConfig.Name = "PalConfig";
            this.PalConfig.Size = new System.Drawing.Size(576, 113);
            this.PalConfig.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.PalConfig.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.PalConfig.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.PalConfig.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.PalConfig.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.PalConfig.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.PalConfig.Style.GradientAngle = 90;
            this.PalConfig.TabIndex = 13;
            this.PalConfig.Click += new System.EventHandler(this.PalConfig_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbproback);
            this.panel1.Controls.Add(this.cbprocedures);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(211, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 109);
            this.panel1.TabIndex = 14;
            // 
            // cbproback
            // 
            this.cbproback.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbproback.FormattingEnabled = true;
            this.cbproback.Location = new System.Drawing.Point(74, 74);
            this.cbproback.Name = "cbproback";
            this.cbproback.Size = new System.Drawing.Size(151, 20);
            this.cbproback.TabIndex = 13;
            this.cbproback.TextChanged += new System.EventHandler(this.cbproback_TextChanged);
            // 
            // cbprocedures
            // 
            this.cbprocedures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbprocedures.FormattingEnabled = true;
            this.cbprocedures.Location = new System.Drawing.Point(74, 15);
            this.cbprocedures.Name = "cbprocedures";
            this.cbprocedures.Size = new System.Drawing.Size(151, 20);
            this.cbprocedures.TabIndex = 11;
            this.cbprocedures.TextChanged += new System.EventHandler(this.cbprocedures_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "行为模式2:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "行为模式1:";
            // 
            // Butok
            // 
            this.Butok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Butok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Butok.Location = new System.Drawing.Point(462, 16);
            this.Butok.Name = "Butok";
            this.Butok.Size = new System.Drawing.Size(75, 23);
            this.Butok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Butok.TabIndex = 12;
            this.Butok.Text = "OK";
            this.Butok.Click += new System.EventHandler(this.Butok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "线别:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "途程:";
            // 
            // cbline
            // 
            this.cbline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbline.FormattingEnabled = true;
            this.cbline.Location = new System.Drawing.Point(58, 72);
            this.cbline.Name = "cbline";
            this.cbline.Size = new System.Drawing.Size(147, 20);
            this.cbline.TabIndex = 3;
            // 
            // cbroute
            // 
            this.cbroute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbroute.FormattingEnabled = true;
            this.cbroute.Location = new System.Drawing.Point(58, 21);
            this.cbroute.Name = "cbroute";
            this.cbroute.Size = new System.Drawing.Size(147, 20);
            this.cbroute.TabIndex = 5;
            this.cbroute.TextChanged += new System.EventHandler(this.cbroute_TextChanged);
            // 
            // btsetup
            // 
            this.btsetup.Location = new System.Drawing.Point(462, 11);
            this.btsetup.Name = "btsetup";
            this.btsetup.Size = new System.Drawing.Size(75, 23);
            this.btsetup.TabIndex = 13;
            this.btsetup.Text = "Manual";
            this.btsetup.UseVisualStyleBackColor = true;
            this.btsetup.Click += new System.EventHandler(this.btsetup_Click);
            // 
            // PalData
            // 
            this.PalData.CanvasColor = System.Drawing.SystemColors.Control;
            this.PalData.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.PalData.Controls.Add(this.chkimei);
            this.PalData.Controls.Add(this.btsetup);
            this.PalData.Controls.Add(this.label4);
            this.PalData.Controls.Add(this.tbInputData);
            this.PalData.Controls.Add(this.tbMessage);
            this.PalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PalData.Location = new System.Drawing.Point(0, 113);
            this.PalData.Name = "PalData";
            this.PalData.Size = new System.Drawing.Size(576, 224);
            this.PalData.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.PalData.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.PalData.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.PalData.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.PalData.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.PalData.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.PalData.Style.GradientAngle = 90;
            this.PalData.TabIndex = 14;
            // 
            // chkimei
            // 
            this.chkimei.AutoSize = true;
            this.chkimei.Location = new System.Drawing.Point(462, 40);
            this.chkimei.Name = "chkimei";
            this.chkimei.Size = new System.Drawing.Size(48, 16);
            this.chkimei.TabIndex = 14;
            this.chkimei.Text = "IMEI";
            this.chkimei.UseVisualStyleBackColor = true;
            this.chkimei.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "输入资料:";
            // 
            // tbInputData
            // 
            this.tbInputData.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbInputData.Location = new System.Drawing.Point(70, 11);
            this.tbInputData.Name = "tbInputData";
            this.tbInputData.Size = new System.Drawing.Size(380, 21);
            this.tbInputData.TabIndex = 7;
            this.tbInputData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInputData_KeyDown);
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(70, 38);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(380, 126);
            this.tbMessage.TabIndex = 8;
            // 
            // hooksetup
            // 
            this.hooksetup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hook,
            this.unhook,
            this.tsmtopmosttrue,
            this.tsmtopmostfalse});
            this.hooksetup.Name = "hooksetup";
            this.hooksetup.Size = new System.Drawing.Size(149, 92);
            // 
            // hook
            // 
            this.hook.Name = "hook";
            this.hook.Size = new System.Drawing.Size(148, 22);
            this.hook.Text = "Hook";
            this.hook.Click += new System.EventHandler(this.hook_Click);
            // 
            // unhook
            // 
            this.unhook.Name = "unhook";
            this.unhook.Size = new System.Drawing.Size(148, 22);
            this.unhook.Text = "UnHook";
            this.unhook.Click += new System.EventHandler(this.unhook_Click);
            // 
            // tsmtopmosttrue
            // 
            this.tsmtopmosttrue.Name = "tsmtopmosttrue";
            this.tsmtopmosttrue.Size = new System.Drawing.Size(148, 22);
            this.tsmtopmosttrue.Text = "窗体前端显示";
            this.tsmtopmosttrue.Click += new System.EventHandler(this.tsmtopmosttrue_Click);
            // 
            // tsmtopmostfalse
            // 
            this.tsmtopmostfalse.Name = "tsmtopmostfalse";
            this.tsmtopmostfalse.Size = new System.Drawing.Size(148, 22);
            this.tsmtopmostfalse.Text = "窗体正常显示";
            this.tsmtopmostfalse.Click += new System.EventHandler(this.tsmtopmostfalse_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hookstatus,
            this.comstatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 337);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(576, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // hookstatus
            // 
            this.hookstatus.Name = "hookstatus";
            this.hookstatus.Size = new System.Drawing.Size(71, 17);
            this.hookstatus.Text = "HOOK OFF";
            // 
            // comstatus
            // 
            this.comstatus.Name = "comstatus";
            this.comstatus.Size = new System.Drawing.Size(60, 17);
            this.comstatus.Text = "COM OK";
            // 
            // SFIS_DCT_V2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 359);
            this.ContextMenuStrip = this.hooksetup;
            this.Controls.Add(this.PalData);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.PalConfig);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "SFIS_DCT_V2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SFIS_DCT_V2_FormClosing);
            this.Load += new System.EventHandler(this.SFIS_DCT_V2_Load);
            this.PalConfig.ResumeLayout(false);
            this.PalConfig.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PalData.ResumeLayout(false);
            this.PalData.PerformLayout();
            this.hooksetup.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx PalConfig;
        private DevComponents.DotNetBar.ButtonX Butok;
        private System.Windows.Forms.ComboBox cbprocedures;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbline;
        private System.Windows.Forms.ComboBox cbroute;
        private DevComponents.DotNetBar.PanelEx PalData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbInputData;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Button btsetup;
        private System.Windows.Forms.ContextMenuStrip hooksetup;
        private System.Windows.Forms.ToolStripMenuItem hook;
        private System.Windows.Forms.ToolStripMenuItem unhook;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel hookstatus;
        private System.Windows.Forms.ToolStripStatusLabel comstatus;
        private System.Windows.Forms.ToolStripMenuItem tsmtopmosttrue;
        private System.Windows.Forms.ToolStripMenuItem tsmtopmostfalse;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbproback;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkimei;
    }
}

