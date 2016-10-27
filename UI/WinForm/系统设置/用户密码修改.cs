using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SFIS_V2
{
    public partial class EditPwd :Office2007Form// Form
    {
        public EditPwd(MainParent _mfrm)
        {
            InitializeComponent();
            this.mFrm = _mfrm;
        }
        private MainParent mFrm;

        private void EditPwd_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
            {
                IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PROGID", this.Name);
                dic.Add("PROGNAME", this.Text);
                dic.Add("PROGDESC", this.Text);
                FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
            }
            #endregion

            this.tbDept.Text = mFrm.gUserInfo.deptname;
            this.tbUserId.Text = mFrm.gUserInfo.userId;
            this.tbUserName.Text = mFrm.gUserInfo.username;
            this.tbEmailAddress.Text = mFrm.gUserInfo.useremail;
            this.tbUserPhone.Text = this.mFrm.gUserInfo.userphone;
        }

        private void bt_edtpwd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tbOldpwd.Text.Trim().ToUpper() == this.mFrm.gUserInfo.pwd.Trim().ToUpper())
                {
                    if (string.IsNullOrEmpty(this.tbNewpwd.Text))
                    {
                        this.mFrm.ShowPrgMsg("输入的新密码不能为空,请重新输入", MainParent.MsgType.Warning);
                        return;
                    }

                    if (MessageBoxEx.Show("密码修改生效后需要重新登陆\n是否继续?", "提示"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
                    {
                        this.mFrm.ShowPrgMsg("取消修改密码!!", MainParent.MsgType.Warning);
                        return;
                    }

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("USERID", this.mFrm.gUserInfo.userId);
                    dic.Add("USERNAME", this.mFrm.gUserInfo.username);
                    dic.Add("USERPHONE", this.tbUserPhone.Text.Trim());
                    dic.Add("USEREMAIL", this.tbEmailAddress.Text.Trim());
                    dic.Add("PWD", this.tbNewpwd.Text.Trim());
                    RefWebService_BLL.refWebtUserInfo.Instance.UpdateUserPassword(FrmBLL.ReleaseData.DictionaryToJson(dic));
                    FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "用户权限", "Modify", "Modify_Pwd,UserId: " + this.mFrm.gUserInfo.userId);

                    this.mFrm.imbt_logOut_Click(null, null);
                }
                else
                {
                    this.mFrm.ShowPrgMsg("旧密码输入错误!!", MainParent.MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void tbOldpwd_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tbOldpwd.Text))
            {
                if (this.tbOldpwd.Text.Trim().ToUpper() != this.mFrm.gUserInfo.pwd.Trim().ToUpper())
                {
                    this.mFrm.ShowPrgMsg("旧密码输入错误!!", MainParent.MsgType.Warning);
                    this.tbOldpwd.SelectAll();
                    this.tbOldpwd.Focus();
                }
            }
        }

        private void tbEmailAddress_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tbEmailAddress.Text))
            {
                if (this.tbEmailAddress.Text.IndexOf(':') == -1)
                {
                    MessageBoxEx.Show("Email地址格式错误,请重新输入", "提示");
                    this.tbEmailAddress.SelectAll();
                    this.tbEmailAddress.Focus();
                }
            }
        }

        private void tbUserPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
