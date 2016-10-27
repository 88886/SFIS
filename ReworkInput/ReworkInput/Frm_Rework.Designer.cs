namespace ReworkInput
{
    partial class Frm_Rework
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_wo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Partnumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_TargetQty = new System.Windows.Forms.TextBox();
            this.tb_Inputqty = new System.Windows.Forms.TextBox();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.LabLine = new System.Windows.Forms.Label();
            this.chkimei = new System.Windows.Forms.CheckBox();
            this.radmobile = new System.Windows.Forms.RadioButton();
            this.radioshutong = new System.Windows.Forms.RadioButton();
            this.tb_sn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.rtbmsg = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imbt_ChgLine = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "工单:";
            // 
            // tb_wo
            // 
            this.tb_wo.Location = new System.Drawing.Point(98, 13);
            this.tb_wo.Name = "tb_wo";
            this.tb_wo.Size = new System.Drawing.Size(146, 21);
            this.tb_wo.TabIndex = 1;
            this.tb_wo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_wo_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "料号:";
            // 
            // tb_Partnumber
            // 
            this.tb_Partnumber.Enabled = false;
            this.tb_Partnumber.Location = new System.Drawing.Point(97, 49);
            this.tb_Partnumber.Name = "tb_Partnumber";
            this.tb_Partnumber.Size = new System.Drawing.Size(147, 21);
            this.tb_Partnumber.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "工单套数:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "投板数:";
            // 
            // tb_TargetQty
            // 
            this.tb_TargetQty.Enabled = false;
            this.tb_TargetQty.Location = new System.Drawing.Point(96, 93);
            this.tb_TargetQty.Name = "tb_TargetQty";
            this.tb_TargetQty.Size = new System.Drawing.Size(149, 21);
            this.tb_TargetQty.TabIndex = 6;
            // 
            // tb_Inputqty
            // 
            this.tb_Inputqty.Enabled = false;
            this.tb_Inputqty.Location = new System.Drawing.Point(97, 129);
            this.tb_Inputqty.Name = "tb_Inputqty";
            this.tb_Inputqty.Size = new System.Drawing.Size(147, 21);
            this.tb_Inputqty.TabIndex = 7;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.LabLine);
            this.panelEx1.Controls.Add(this.chkimei);
            this.panelEx1.Controls.Add(this.radmobile);
            this.panelEx1.Controls.Add(this.radioshutong);
            this.panelEx1.Controls.Add(this.tb_sn);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.tb_Inputqty);
            this.panelEx1.Controls.Add(this.tb_TargetQty);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.tb_Partnumber);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.tb_wo);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(360, 396);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 8;
            // 
            // LabLine
            // 
            this.LabLine.AutoSize = true;
            this.LabLine.Location = new System.Drawing.Point(98, 168);
            this.LabLine.Name = "LabLine";
            this.LabLine.Size = new System.Drawing.Size(41, 12);
            this.LabLine.TabIndex = 18;
            this.LabLine.Text = "label7";
            // 
            // chkimei
            // 
            this.chkimei.AutoSize = true;
            this.chkimei.Location = new System.Drawing.Point(266, 224);
            this.chkimei.Name = "chkimei";
            this.chkimei.Size = new System.Drawing.Size(48, 16);
            this.chkimei.TabIndex = 17;
            this.chkimei.Text = "IMEI";
            this.chkimei.UseVisualStyleBackColor = true;
            // 
            // radmobile
            // 
            this.radmobile.AutoSize = true;
            this.radmobile.Location = new System.Drawing.Point(196, 196);
            this.radmobile.Name = "radmobile";
            this.radmobile.Size = new System.Drawing.Size(107, 16);
            this.radmobile.TabIndex = 13;
            this.radmobile.Text = "不清除绑定关系";
            this.radmobile.UseVisualStyleBackColor = true;
            // 
            // radioshutong
            // 
            this.radioshutong.AutoSize = true;
            this.radioshutong.Checked = true;
            this.radioshutong.Location = new System.Drawing.Point(49, 196);
            this.radioshutong.Name = "radioshutong";
            this.radioshutong.Size = new System.Drawing.Size(125, 16);
            this.radioshutong.TabIndex = 12;
            this.radioshutong.TabStop = true;
            this.radioshutong.Text = "不清除(MAC,KPESN)";
            this.radioshutong.UseVisualStyleBackColor = true;
            // 
            // tb_sn
            // 
            this.tb_sn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tb_sn.Location = new System.Drawing.Point(94, 221);
            this.tb_sn.Name = "tb_sn";
            this.tb_sn.Size = new System.Drawing.Size(149, 21);
            this.tb_sn.TabIndex = 11;
            this.tb_sn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_sn_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "线体:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "刷入产品:";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.rtbmsg);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 262);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(360, 134);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 9;
            this.panelEx2.Text = "panelEx2";
            // 
            // rtbmsg
            // 
            this.rtbmsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbmsg.Location = new System.Drawing.Point(0, 0);
            this.rtbmsg.Name = "rtbmsg";
            this.rtbmsg.ReadOnly = true;
            this.rtbmsg.Size = new System.Drawing.Size(360, 134);
            this.rtbmsg.TabIndex = 0;
            this.rtbmsg.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imbt_ChgLine});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // imbt_ChgLine
            // 
            this.imbt_ChgLine.Name = "imbt_ChgLine";
            this.imbt_ChgLine.Size = new System.Drawing.Size(124, 22);
            this.imbt_ChgLine.Text = "线体设置";
            this.imbt_ChgLine.Click += new System.EventHandler(this.imbt_ChgLine_Click);
            // 
            // Frm_Rework
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 396);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Name = "Frm_Rework";
            this.Text = "重工投板";
            this.Load += new System.EventHandler(this.Frm_Rework_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_wo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_Partnumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_TargetQty;
        private System.Windows.Forms.TextBox tb_Inputqty;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.RichTextBox rtbmsg;
        private System.Windows.Forms.TextBox tb_sn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radmobile;
        private System.Windows.Forms.RadioButton radioshutong;
        private System.Windows.Forms.CheckBox chkimei;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem imbt_ChgLine;
        private System.Windows.Forms.Label LabLine;
    }
}