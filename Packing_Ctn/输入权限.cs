using System;
using System.Windows.Forms;
using System.Collections.Generic;
using DevComponents.DotNetBar;

namespace Packing_Ctn
{
    /// <summary>
    /// clsInputBox 的摘要说明。
    /// </summary>
    public class InputBox :Office2007Form// System.Windows.Forms.Form
    {

        private System.Windows.Forms.TextBox textemp;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label lblInfo;    
        private Label label1;
        private Label label2;
        private System.ComponentModel.Container components = null;
        

        private InputBox()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textemp = new System.Windows.Forms.TextBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textemp
            // 
            this.textemp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textemp.Location = new System.Drawing.Point(25, 32);
            this.textemp.Name = "textemp";
            this.textemp.Size = new System.Drawing.Size(317, 23);
            this.textemp.TabIndex = 0;
            this.textemp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textemp_KeyDown);
            this.textemp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textemp_KeyPress);
            // 
            // txtData
            // 
            this.txtData.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtData.Location = new System.Drawing.Point(25, 91);
            this.txtData.Name = "txtData";
            this.txtData.PasswordChar = '*';
            this.txtData.Size = new System.Drawing.Size(317, 23);
            this.txtData.TabIndex = 2;
            this.txtData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtData_KeyDown);
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.SystemColors.Info;
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Location = new System.Drawing.Point(25, 135);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(317, 16);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "[Enter]确认 | [Esc]取消";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "USER ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "USER PWD:";
            // 
            // InputBox
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(354, 167);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textemp);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "InputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输入权限";
            this.Load += new System.EventHandler(this.InputBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //对键盘进行响应
        private void txtData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Close();
            }

            else if (e.KeyCode == Keys.Escape)
            {
                txtData.Text = string.Empty;
                this.Close();
            }

        }

        //显示InputBox
        public static string[] ShowInputBox(string Title, string keyInfo)
        {
            InputBox inputbox = new InputBox();
            inputbox.Text = Title;
            inputbox.textemp.Focus();
            if (keyInfo.Trim() != string.Empty)
                inputbox.lblInfo.Text = keyInfo;
           
            inputbox.ShowDialog();
           

            string zz = inputbox.textemp.Text + "|" + inputbox.txtData.Text;
            string[] ss = zz.Split('|');

            return ss;
        }
        
        private void textemp_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.Close();
            //}

            if (e.KeyCode == Keys.Escape)
            {
                textemp.Text = string.Empty;
                this.Close();
            }
        }

        private void textemp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textemp.Text.Trim() != "" && e.KeyChar == 13)
            {
                txtData.Focus();
               
            }
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            //List<WebServices.tUserInfo.tFunctionList> lsfunls = new List<WebServices.tUserInfo.tFunctionList>();
            //for (int i = 0; i < this.Controls.Count; i++)
            //{
            //    if (this.Controls[i] is Button || this.Controls[i] is DevComponents.DotNetBar.ButtonX)
            //    {
            //        lsfunls.Add(new WebServices.tUserInfo.tFunctionList()
            //        {
            //            funId = this.Controls[i].Name,
            //            funname = this.Controls[i].Text,
            //            fundesc = this.Controls[i].Text,
            //            progid = this.Name
            //        });
            //    }
            //}
            //FrmBLL.publicfuntion.AddProgInfo(new WebServices.tUserInfo.tProgInfo()
            //{
            //    progid = this.Name,
            //    progname = this.Text,
            //    progdesc = this.Text

            //}, lsfunls);
            #endregion
            
        }
    }

}