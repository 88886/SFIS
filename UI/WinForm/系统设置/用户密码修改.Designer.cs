namespace SFIS_V2
{
    partial class EditPwd
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUserId = new System.Windows.Forms.TextBox();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbDept = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbOldpwd = new System.Windows.Forms.TextBox();
            this.tbNewpwd = new System.Windows.Forms.TextBox();
            this.bt_edtpwd = new DevComponents.DotNetBar.ButtonX();
            this.label6 = new System.Windows.Forms.Label();
            this.tbEmailAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbUserPhone = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "[工号]:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "[姓名]:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "[部门]:";
            // 
            // tbUserId
            // 
            this.tbUserId.Location = new System.Drawing.Point(72, 62);
            this.tbUserId.Name = "tbUserId";
            this.tbUserId.ReadOnly = true;
            this.tbUserId.Size = new System.Drawing.Size(130, 21);
            this.tbUserId.TabIndex = 3;
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(72, 93);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.ReadOnly = true;
            this.tbUserName.Size = new System.Drawing.Size(130, 21);
            this.tbUserName.TabIndex = 3;
            // 
            // tbDept
            // 
            this.tbDept.Location = new System.Drawing.Point(72, 31);
            this.tbDept.Name = "tbDept";
            this.tbDept.ReadOnly = true;
            this.tbDept.Size = new System.Drawing.Size(169, 21);
            this.tbDept.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "[旧密码]:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 222);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "[新密码]:";
            // 
            // tbOldpwd
            // 
            this.tbOldpwd.Location = new System.Drawing.Point(72, 186);
            this.tbOldpwd.Name = "tbOldpwd";
            this.tbOldpwd.PasswordChar = '*';
            this.tbOldpwd.Size = new System.Drawing.Size(169, 21);
            this.tbOldpwd.TabIndex = 3;
            this.tbOldpwd.Leave += new System.EventHandler(this.tbOldpwd_Leave);
            // 
            // tbNewpwd
            // 
            this.tbNewpwd.Location = new System.Drawing.Point(72, 217);
            this.tbNewpwd.Name = "tbNewpwd";
            this.tbNewpwd.PasswordChar = '*';
            this.tbNewpwd.Size = new System.Drawing.Size(169, 21);
            this.tbNewpwd.TabIndex = 3;
            // 
            // bt_edtpwd
            // 
            this.bt_edtpwd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_edtpwd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_edtpwd.Location = new System.Drawing.Point(166, 244);
            this.bt_edtpwd.Name = "bt_edtpwd";
            this.bt_edtpwd.Size = new System.Drawing.Size(75, 23);
            this.bt_edtpwd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_edtpwd.TabIndex = 4;
            this.bt_edtpwd.Text = "修改";
            this.bt_edtpwd.Click += new System.EventHandler(this.bt_edtpwd_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "[电话]:";
            // 
            // tbEmailAddress
            // 
            this.tbEmailAddress.Location = new System.Drawing.Point(72, 155);
            this.tbEmailAddress.Name = "tbEmailAddress";
            this.tbEmailAddress.Size = new System.Drawing.Size(169, 21);
            this.tbEmailAddress.TabIndex = 3;
            this.tbEmailAddress.Leave += new System.EventHandler(this.tbEmailAddress_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "[Email]:";
            // 
            // tbUserPhone
            // 
            this.tbUserPhone.Location = new System.Drawing.Point(72, 124);
            this.tbUserPhone.Name = "tbUserPhone";
            this.tbUserPhone.Size = new System.Drawing.Size(169, 21);
            this.tbUserPhone.TabIndex = 3;
            this.tbUserPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbUserPhone_KeyPress);
            // 
            // EditPwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 298);
            this.Controls.Add(this.tbNewpwd);
            this.Controls.Add(this.tbUserPhone);
            this.Controls.Add(this.tbEmailAddress);
            this.Controls.Add(this.tbOldpwd);
            this.Controls.Add(this.tbDept);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.tbUserId);
            this.Controls.Add(this.bt_edtpwd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EditPwd";
            this.Text = "用户密码修改";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EditPwd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUserId;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbDept;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbOldpwd;
        private System.Windows.Forms.TextBox tbNewpwd;
        private DevComponents.DotNetBar.ButtonX bt_edtpwd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbEmailAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbUserPhone;
    }
}