namespace SFIS_V2
{
    partial class Frm_PrintDataPartision
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
            this.bti_opentemplate = new DevComponents.DotNetBar.ButtonItem();
            this.bti_print = new DevComponents.DotNetBar.ButtonItem();
            this.rtb_msg = new System.Windows.Forms.RichTextBox();
            this.ip_serialtype = new DevComponents.DotNetBar.ItemPanel();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.lbi_path = new DevComponents.DotNetBar.LabelItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.bti_opentemplate,
            this.bti_print});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(598, 27);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // bti_opentemplate
            // 
            this.bti_opentemplate.Name = "bti_opentemplate";
            this.bti_opentemplate.Text = "选择模板";
            this.bti_opentemplate.Click += new System.EventHandler(this.bti_opentemplate_Click);
            // 
            // bti_print
            // 
            this.bti_print.Name = "bti_print";
            this.bti_print.SubItemsExpandWidth = 20;
            this.bti_print.Text = "打印";
            this.bti_print.Click += new System.EventHandler(this.bti_print_Click);
            // 
            // rtb_msg
            // 
            this.rtb_msg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_msg.Location = new System.Drawing.Point(0, 0);
            this.rtb_msg.Name = "rtb_msg";
            this.rtb_msg.Size = new System.Drawing.Size(598, 195);
            this.rtb_msg.TabIndex = 1;
            this.rtb_msg.Text = "";
            // 
            // ip_serialtype
            // 
            // 
            // 
            // 
            this.ip_serialtype.BackgroundStyle.Class = "ItemPanel";
            this.ip_serialtype.ContainerControlProcessDialogKey = true;
            this.ip_serialtype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ip_serialtype.Location = new System.Drawing.Point(0, 0);
            this.ip_serialtype.Name = "ip_serialtype";
            this.ip_serialtype.Size = new System.Drawing.Size(598, 78);
            this.ip_serialtype.TabIndex = 2;
            this.ip_serialtype.Text = "itemPanel1";
            // 
            // bar2
            // 
            this.bar2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lbi_path});
            this.bar2.Location = new System.Drawing.Point(0, 300);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(598, 21);
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar2.TabIndex = 2;
            this.bar2.TabStop = false;
            this.bar2.Text = "bar2";
            // 
            // lbi_path
            // 
            this.lbi_path.Name = "lbi_path";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ip_serialtype);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(598, 78);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtb_msg);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 105);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(598, 195);
            this.panel2.TabIndex = 4;
            // 
            // Frm_PrintDataPartision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 321);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bar2);
            this.Controls.Add(this.bar1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Frm_PrintDataPartision";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标签重印";
            this.Load += new System.EventHandler(this.Frm_PrintDataPartision_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem bti_opentemplate;
        private DevComponents.DotNetBar.ButtonItem bti_print;
        private System.Windows.Forms.RichTextBox rtb_msg;
        private DevComponents.DotNetBar.ItemPanel ip_serialtype;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.LabelItem lbi_path;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}