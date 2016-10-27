namespace SFIS_V2
{
    partial class Frm_Station_Config_SubForm
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
            this.cbx_selectdata = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.imbt_ok = new DevComponents.DotNetBar.ButtonX();
            this.imbt_cancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // cbx_selectdata
            // 
            this.cbx_selectdata.DisplayMember = "Text";
            this.cbx_selectdata.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbx_selectdata.DropDownHeight = 100;
            this.cbx_selectdata.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_selectdata.FormattingEnabled = true;
            this.cbx_selectdata.IntegralHeight = false;
            this.cbx_selectdata.ItemHeight = 23;
            this.cbx_selectdata.Location = new System.Drawing.Point(33, 26);
            this.cbx_selectdata.Name = "cbx_selectdata";
            this.cbx_selectdata.Size = new System.Drawing.Size(217, 29);
            this.cbx_selectdata.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbx_selectdata.TabIndex = 0;
            this.cbx_selectdata.SelectedIndexChanged += new System.EventHandler(this.cbx_selectdata_SelectedIndexChanged);
            this.cbx_selectdata.Validating += new System.ComponentModel.CancelEventHandler(this.cbx_selectdata_Validating);
            // 
            // imbt_ok
            // 
            this.imbt_ok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_ok.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_ok.Location = new System.Drawing.Point(33, 75);
            this.imbt_ok.Name = "imbt_ok";
            this.imbt_ok.Size = new System.Drawing.Size(75, 23);
            this.imbt_ok.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_ok.TabIndex = 1;
            this.imbt_ok.Text = "选择";
            this.imbt_ok.Click += new System.EventHandler(this.imbt_ok_Click);
            // 
            // imbt_cancel
            // 
            this.imbt_cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.imbt_cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.imbt_cancel.Location = new System.Drawing.Point(175, 75);
            this.imbt_cancel.Name = "imbt_cancel";
            this.imbt_cancel.Size = new System.Drawing.Size(75, 23);
            this.imbt_cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.imbt_cancel.TabIndex = 1;
            this.imbt_cancel.Text = "退出";
            this.imbt_cancel.Click += new System.EventHandler(this.imbt_cancel_Click);
            // 
            // Frm_Station_Config_SubForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 123);
            this.Controls.Add(this.imbt_cancel);
            this.Controls.Add(this.imbt_ok);
            this.Controls.Add(this.cbx_selectdata);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Station_Config_SubForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Station_Config_SubForm";
            this.Load += new System.EventHandler(this.Frm_Station_Config_SubForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cbx_selectdata;
        private DevComponents.DotNetBar.ButtonX imbt_ok;
        private DevComponents.DotNetBar.ButtonX imbt_cancel;
    }
}