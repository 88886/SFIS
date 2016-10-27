namespace SFIS_V2
{
    partial class Frm_Station_Config
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Station_Config));
            this.treeView_StationConfig = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.imbt_delete = new DevComponents.DotNetBar.ButtonX();
            this.imbt_add = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txt_station_name = new System.Windows.Forms.TextBox();
            this.cbx_Line_name = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbx_section_name = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbx__group_name = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView_StationConfig
            // 
            this.treeView_StationConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_StationConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView_StationConfig.ImageIndex = 0;
            this.treeView_StationConfig.ImageList = this.imageList1;
            this.treeView_StationConfig.Location = new System.Drawing.Point(0, 0);
            this.treeView_StationConfig.Name = "treeView_StationConfig";
            this.treeView_StationConfig.SelectedImageIndex = 0;
            this.treeView_StationConfig.Size = new System.Drawing.Size(373, 408);
            this.treeView_StationConfig.TabIndex = 0;
            this.treeView_StationConfig.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_StationConfig_AfterSelect);
            this.treeView_StationConfig.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_StationConfig_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "APPLINE.BMP");
            this.imageList1.Images.SetKeyName(1, "treesection.bmp");
            this.imageList1.Images.SetKeyName(2, "treegroup.bmp");
            this.imageList1.Images.SetKeyName(3, "TREEDTA.BMP");
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.treeView_StationConfig);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx1.Location = new System.Drawing.Point(0, 75);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(373, 408);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            this.panelEx1.Text = "panelEx1";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1059, 75);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 2;
            // 
            // imbt_delete
            // 
            this.imbt_delete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_delete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_delete.Location = new System.Drawing.Point(769, 426);
            this.imbt_delete.Name = "imbt_delete";
            this.imbt_delete.Size = new System.Drawing.Size(75, 45);
            this.imbt_delete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_delete.TabIndex = 0;
            this.imbt_delete.Text = "删除";
            this.imbt_delete.Click += new System.EventHandler(this.imbt_section_name_Click);
            // 
            // imbt_add
            // 
            this.imbt_add.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_add.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_add.Location = new System.Drawing.Point(500, 426);
            this.imbt_add.Name = "imbt_add";
            this.imbt_add.Size = new System.Drawing.Size(75, 45);
            this.imbt_add.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_add.TabIndex = 0;
            this.imbt_add.Text = "新增";
            this.imbt_add.Click += new System.EventHandler(this.imbt_addline_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(464, 132);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(62, 23);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "线别:";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(446, 188);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(80, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "制程段:";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(464, 252);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(62, 23);
            this.labelX3.TabIndex = 3;
            this.labelX3.Text = "途程:";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(464, 308);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(62, 23);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "站位:";
            // 
            // txt_station_name
            // 
            this.txt_station_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_station_name.Location = new System.Drawing.Point(531, 308);
            this.txt_station_name.Name = "txt_station_name";
            this.txt_station_name.Size = new System.Drawing.Size(229, 26);
            this.txt_station_name.TabIndex = 4;
            // 
            // cbx_Line_name
            // 
            this.cbx_Line_name.DisplayMember = "Text";
            this.cbx_Line_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbx_Line_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_Line_name.FormattingEnabled = true;
            this.cbx_Line_name.ItemHeight = 20;
            this.cbx_Line_name.Location = new System.Drawing.Point(532, 129);
            this.cbx_Line_name.Name = "cbx_Line_name";
            this.cbx_Line_name.Size = new System.Drawing.Size(228, 26);
            this.cbx_Line_name.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbx_Line_name.TabIndex = 5;
            // 
            // cbx_section_name
            // 
            this.cbx_section_name.DisplayMember = "Text";
            this.cbx_section_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbx_section_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_section_name.FormattingEnabled = true;
            this.cbx_section_name.ItemHeight = 20;
            this.cbx_section_name.Location = new System.Drawing.Point(532, 185);
            this.cbx_section_name.Name = "cbx_section_name";
            this.cbx_section_name.Size = new System.Drawing.Size(228, 26);
            this.cbx_section_name.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbx_section_name.TabIndex = 5;
            // 
            // cbx__group_name
            // 
            this.cbx__group_name.DisplayMember = "Text";
            this.cbx__group_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbx__group_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx__group_name.FormattingEnabled = true;
            this.cbx__group_name.ItemHeight = 20;
            this.cbx__group_name.Location = new System.Drawing.Point(532, 249);
            this.cbx__group_name.Name = "cbx__group_name";
            this.cbx__group_name.Size = new System.Drawing.Size(228, 26);
            this.cbx__group_name.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbx__group_name.TabIndex = 5;
            // 
            // Frm_Station_Config
            // 
            this.ClientSize = new System.Drawing.Size(1059, 483);
            this.Controls.Add(this.cbx__group_name);
            this.Controls.Add(this.cbx_section_name);
            this.Controls.Add(this.imbt_delete);
            this.Controls.Add(this.cbx_Line_name);
            this.Controls.Add(this.imbt_add);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txt_station_name);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.panelEx2);
            this.DoubleBuffered = true;
            this.Name = "Frm_Station_Config";
            this.Text = "站位配置";
            this.Load += new System.EventHandler(this.Frm_Station_Config_Load);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView_StationConfig;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.ImageList imageList1;
        private DevComponents.DotNetBar.ButtonX imbt_add;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.TextBox txt_station_name;
        private DevComponents.DotNetBar.ButtonX imbt_delete;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbx_Line_name;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbx_section_name;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbx__group_name;
    }
}