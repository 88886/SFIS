using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;
using DevComponents.DotNetBar;

namespace SMT_STOCKIN
{
    public partial class UserLogin : Office2007Form //Form
    {
        public UserLogin(Frm_STOCKIN mp)
        {
            InitializeComponent();
            this.mpm = mp;
        }
        Frm_STOCKIN mpm;
        private IAsyncResult iasyncresult;
        private delegate void delegatecreatedll();

        private void cre()
        {           
            
        }
     
        private void CreateWebserviceDll()
        {
          
        }
        private void EnableLogin(bool msg)
        {
            this.bt_login.Invoke(new EventHandler(delegate
                {
                    this.bt_login.Enabled = msg;
                }));
        }
        private void Login_Load(object sender, EventArgs e)
        {
            
        }
        private void bt_login_Click(object sender, EventArgs e)
        {
        //    if (!File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\getWebServices.dll"))
        //    {
        //        this.Close();
        //        return;
        //    }
        //    if (iasyncresult != null && !iasyncresult.IsCompleted)
        //    {
        //        this.mpm.ShowMsg(PrintMain.mLogMsgType.Warning ,"正在与服务器通讯中,请稍后.");
        //        return;
        //    }

        //    this.bt_CfgIp.Enabled = false;
        //    try
        //    {
        //        if (string.IsNullOrEmpty(this.tb_username.Text))
        //            throw new Exception("用户名不能为空!!");
        //        DataTable _dt =SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfoByUserId(this.tb_username.Text.Trim()));
        //        if (_dt == null || _dt.Rows.Count < 1 || _dt.Rows[0]["pwd"].ToString() != this.tb_password.Text)
        //            throw new Exception("用户名或密码输入错误\n请重新输入!!");

        //        this.mpm.gUserInfo =  new  WebServices.tUserInfo.tUserInfo1()
        //        {
        //            userId = _dt.Rows[0]["userId"].ToString(),
        //            pwd = _dt.Rows[0]["pwd"].ToString(),
        //            username = _dt.Rows[0]["username"].ToString(),
        //            useremail = _dt.Rows[0]["useremail"].ToString(),
        //            userphone = _dt.Rows[0]["userphone"].ToString(),
        //            userstatus = Convert.ToBoolean(int.Parse(_dt.Rows[0]["userstatus"].ToString())),
        //            deptname = _dt.Rows[0]["deptname"].ToString(),
        //            facId = _dt.Rows[0]["facId"].ToString(),
        //            rolecaption = _dt.Rows[0]["rolecaption"].ToString(),
        //            userPopList = SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(SFIS_PRINT_SYSTEM.BLL.refWebtUserInfo.Instance.GetUserJurisdictionByUserId(this.tb_username.Text.Trim()))
        //        };
        //        BLL.CreateAccessDb cad = new CreateAccessDb();
        //        this.mpm.loginOk = true;
        //        this.DialogResult = DialogResult.OK;
        //    }
        //    catch (Exception ex)
        //    {
        //        mpm.ShowMsg(PrintMain.mLogMsgType.Error, ex.Message);
        //        MessageBox.Show(ex.Message, "提示");
        //        this.tb_username.SelectAll();
        //            this.tb_username.Focus();
        //    }
            string _StrErr = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(this.tb_username.Text))
                    throw new Exception("用户名不能为空!!");
                _StrErr = mpm.oDB.Check_User(this.tb_username.Text, this.tb_password.Text);
                if (_StrErr == "OK")
                {
                    mpm.userId = this.tb_username.Text;
                    mpm.userPwd = this.tb_password.Text;                 

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(_StrErr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_exit_Click(object sender, EventArgs e)
        {
                //this.mpm.loginOk = false;
                //this.mpm.gUserInfo = new  WebServices.tUserInfo.tUserInfo1()
                //{
                //    userId = "",
                //    username = "",
                //};
                this.DialogResult = DialogResult.No;
                this.Dispose();
               // this.mpm.imbtexit_Click(sender, e);
        }

        private void tb_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                bt_login_Click(sender, e);
        }

        private void tb_username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.tb_password.SelectAll();
                this.tb_password.Focus();
            }
        }
        public string WebServiceIpAddress { get; set; }
        private void bt_CfgIp_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cre();
        }
    }
}
