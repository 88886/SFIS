namespace SFIS_V2
{
    partial class ShowRoutGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowRoutGroup));
            this.tv_showroute = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_routegroupid = new System.Windows.Forms.TextBox();
            this.bt_select = new DevComponents.DotNetBar.ButtonX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tv_showroute
            // 
            this.tv_showroute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_showroute.ImageIndex = 0;
            this.tv_showroute.ImageList = this.imageList1;
            this.tv_showroute.Location = new System.Drawing.Point(0, 0);
            this.tv_showroute.Name = "tv_showroute";
            this.tv_showroute.SelectedImageIndex = 0;
            this.tv_showroute.Size = new System.Drawing.Size(199, 348);
            this.tv_showroute.TabIndex = 0;
            this.tv_showroute.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_showroute_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "group.bmp");
            this.imageList1.Images.SetKeyName(1, "dta.bmp");
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.tb_routegroupid);
            this.panel3.Controls.Add(this.bt_select);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(688, 29);
            this.panel3.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "[流程编号:]";
            // 
            // tb_routegroupid
            // 
            this.tb_routegroupid.Enabled = false;
            this.tb_routegroupid.Location = new System.Drawing.Point(81, 4);
            this.tb_routegroupid.Name = "tb_routegroupid";
            this.tb_routegroupid.Size = new System.Drawing.Size(122, 21);
            this.tb_routegroupid.TabIndex = 1;
            // 
            // bt_select
            // 
            this.bt_select.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_select.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_select.Location = new System.Drawing.Point(209, 4);
            this.bt_select.Name = "bt_select";
            this.bt_select.Size = new System.Drawing.Size(53, 20);
            this.bt_select.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_select.TabIndex = 0;
            this.bt_select.Text = "选择";
            this.bt_select.Click += new System.EventHandler(this.bt_select_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tv_showroute);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 348);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(199, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(489, 348);
            this.panel2.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(489, 348);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ShowRoutGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 377);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "ShowRoutGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择生产流程";
            this.Load += new System.EventHandler(this.ShowRoutGroup_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv_showroute;
        private System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.ButtonX bt_select;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tb_routegroupid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ImageList imageList1;


    }
}