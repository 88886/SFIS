namespace SFIS_V2
{
    partial class Frm_ATE_SCRIPT
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.imbt_save = new DevComponents.DotNetBar.ButtonX();
            this.txt_woId = new System.Windows.Forms.TextBox();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.listbScript = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbMsg = new System.Windows.Forms.Label();
            this.imbt_LoadATEScript = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.imbt_LoadATEScript);
            this.groupBox1.Controls.Add(this.imbt_save);
            this.groupBox1.Controls.Add(this.txt_woId);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(709, 60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入工单";
            // 
            // imbt_save
            // 
            this.imbt_save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_save.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_save.Location = new System.Drawing.Point(374, 16);
            this.imbt_save.Name = "imbt_save";
            this.imbt_save.Size = new System.Drawing.Size(75, 37);
            this.imbt_save.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_save.TabIndex = 1;
            this.imbt_save.Text = "保存";
            this.imbt_save.Click += new System.EventHandler(this.imbt_save_Click);
            // 
            // txt_woId
            // 
            this.txt_woId.Location = new System.Drawing.Point(38, 23);
            this.txt_woId.Name = "txt_woId";
            this.txt_woId.Size = new System.Drawing.Size(255, 29);
            this.txt_woId.TabIndex = 0;
            this.txt_woId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_woId_KeyDown);
            // 
            // groupPanel3
            // 
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.listbScript);
            this.groupPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupPanel3.Location = new System.Drawing.Point(0, 60);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(709, 264);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.Class = "";
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.Class = "";
            this.groupPanel3.TabIndex = 1;
            this.groupPanel3.Text = "ATE脚本";
            // 
            // listbScript
            // 
            this.listbScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listbScript.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listbScript.FormattingEnabled = true;
            this.listbScript.ItemHeight = 16;
            this.listbScript.Location = new System.Drawing.Point(0, 0);
            this.listbScript.Name = "listbScript";
            this.listbScript.Size = new System.Drawing.Size(703, 231);
            this.listbScript.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbMsg);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 324);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(709, 72);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "信息提示";
            // 
            // lbMsg
            // 
            this.lbMsg.AutoSize = true;
            this.lbMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMsg.Location = new System.Drawing.Point(12, 36);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.Size = new System.Drawing.Size(56, 16);
            this.lbMsg.TabIndex = 0;
            this.lbMsg.Text = "label1";
            // 
            // imbt_LoadATEScript
            // 
            this.imbt_LoadATEScript.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_LoadATEScript.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_LoadATEScript.Location = new System.Drawing.Point(527, 16);
            this.imbt_LoadATEScript.Name = "imbt_LoadATEScript";
            this.imbt_LoadATEScript.Size = new System.Drawing.Size(103, 37);
            this.imbt_LoadATEScript.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_LoadATEScript.TabIndex = 1;
            this.imbt_LoadATEScript.Text = "加载脚本";
            this.imbt_LoadATEScript.Click += new System.EventHandler(this.imbt_LoadATEScript_Click);
            // 
            // Frm_ATE_SCRIPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 396);
            this.Controls.Add(this.groupPanel3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ATE_SCRIPT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATE测试脚本加载";
            this.Load += new System.EventHandler(this.Frm_ATE_SCRIPT_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_woId;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        public System.Windows.Forms.ListBox listbScript;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbMsg;
        private DevComponents.DotNetBar.ButtonX imbt_save;
        private DevComponents.DotNetBar.ButtonX imbt_LoadATEScript;
    }
}