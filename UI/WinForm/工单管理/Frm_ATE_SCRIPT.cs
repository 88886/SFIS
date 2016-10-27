using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class Frm_ATE_SCRIPT : Office2007Form //Form
    {
        public Frm_ATE_SCRIPT(Office2007Form Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Office2007Form mFrm;
        private void Frm_ATE_SCRIPT_Load(object sender, EventArgs e)
        {
            lbMsg.Text = null;
        }
        string woId = string.Empty;
        private void txt_woId_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_woId.Text) && e.KeyCode == Keys.Enter)
            {
                this.txt_woId.SelectAll();
                woId = this.txt_woId.Text;
                //加载脚本
                listbScript.Items.Clear();
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetATEScripts(woId));
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (string item in dt.Rows[0][0].ToString().Split(','))
                    {
                        this.listbScript.Items.Add(item);
                    }
                }
                GetFtp_ATE_SCRIPT();
            }
        }

        /// <summary>
        /// 重新从Ftp获取ATE脚本
        /// </summary>
        private void GetFtp_ATE_SCRIPT()
        {
            if (listbScript.Items.Count > 0)
            {
                if (MessageBoxEx.Show("是否需要从Ftp 重新导入 ATE 脚本? ", "导入脚本提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Get_ATE_Script();
                }
            }
            else
            {
                Get_ATE_Script();
            }
        }
        private void Get_ATE_Script()
        {
            this.Invoke(new EventHandler(delegate
            {
                try
                {
                     
                    listbScript.Items.Clear();
                    string IniFileName = System.AppDomain.CurrentDomain.BaseDirectory + "Updatecfg.ini";
                    string FtpHost = FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "Host", IniFileName);
                    string User = FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "User", IniFileName);
                    string UsePwd = FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "Password", IniFileName);
                    FrmBLL.Ftp_Socket fm = new FrmBLL.Ftp_Socket(FtpHost, woId, User, UsePwd, 21);
                    this.lbMsg.Text = "正在获取脚本....";                    
                    lbMsg.Update();
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, "正在获取脚本....");
                    try
                    {
                        List<string> ss = fm.FileList("*.ts");
                        foreach (string item in ss)
                        {
                            listbScript.Items.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        lbMsg.Text = "获取脚本异常:"+ex.Message;
                        lbMsg.ForeColor = Color.Red;
                        lbMsg.Update();
                        (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, lbMsg.Text);
                    }
                    this.lbMsg.Text = "获取脚本完成,正在获取条码档....";
                    lbMsg.Update();
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, lbMsg.Text);
                    try
                    {
                        List<string> Lab = fm.FileList("*.Lab");
                        foreach (string item in Lab)
                        {
                            listbScript.Items.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {

                        lbMsg.Text = "获取条码档异常:" + ex.Message;
                        lbMsg.ForeColor = Color.Red;
                        lbMsg.Update();
                        (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, lbMsg.Text);
                    }
                    this.lbMsg.Text = "读取ftp完成...";
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, lbMsg.Text);
                    lbMsg.Update();
                    lbMsg.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    this.lbMsg.Text = ex.Message;
                    lbMsg.ForeColor = Color.Red;
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, lbMsg.Text);
                   
                }

            }));
        }
        private void SaveAteScript()
        {
            this.lbMsg.Text = "开始保存脚本信息...";
            lbMsg.ForeColor = Color.Black;
            lbMsg.Update();
            (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Normal, lbMsg.Text);
            string atescript = string.Empty;
            string showatescript=string.Format("工单:\r\n{0}\r\n脚本:\r\n",woId);
            
            for (int i = 0; i < this.listbScript.Items.Count; i++)
            {
                atescript += this.listbScript.Items[i].ToString() + ",";
                showatescript+=this.listbScript.Items[i].ToString() + "\r\n";
            }
            if (atescript != null && !string.IsNullOrEmpty(atescript))
            {
                atescript = atescript.Substring(0, atescript.Length - 1);
            }
            if (MessageBoxEx.Show("确定保存脚本" + showatescript, "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                IDictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("WOID", woId);
                dic.Add("SCRIPT", atescript);
                string JsonStr = FrmBLL.ReleaseData.DictionaryToJson(dic);
                string _StrErr = refWebtWoInfo.Instance.Insert_Wo_Info(null, JsonStr, null, null);
                if (_StrErr == "OK")
                {
                    FrmBLL.publicfuntion.InserSystemLog((mFrm as Frm_MO_Manage).UserId, "工单信息", "AteScript", JsonStr);
                    this.lbMsg.Text = "保存脚本信息完成...";
                    lbMsg.ForeColor = Color.Green;
                    lbMsg.Update();
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, lbMsg.Text);
                    MessageBoxEx.Show("保存脚本信息完成...", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txt_woId.Text = string.Empty;
                    this.txt_woId.Focus();
                    this.listbScript.Items.Clear();
                }
                else
                {
                    _StrErr = string.Format("保存脚本异常:" + _StrErr);
                    this.lbMsg.Text = _StrErr;
                    lbMsg.ForeColor = Color.Red;
                    lbMsg.Update();
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, lbMsg.Text);
                    MessageBoxEx.Show(_StrErr);

                }
            }


        }

        private void imbt_save_Click(object sender, EventArgs e)
        {
            SaveAteScript();
        }

        private void imbt_LoadATEScript_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Title = "选择测试脚本文件";
            ofd.InitialDirectory = @"d:\";
            ofd.Filter = "(*.tes;*.ts;*.lab)|*.tes;*.ts;*.lab|(*.tes)|*.tes|(*.ts)|*.ts|(*.lab)|*.lab|(*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (this.listbScript.Items.Count < 1)
                {
                    this.listbScript.Items.AddRange(ofd.SafeFileNames);
                }
                else
                {
                    List<string> ls = new List<string>();
                    for (int i = 0; i < this.listbScript.Items.Count; i++)
                    {
                        if (!tes(this.listbScript.Items[i].ToString(), ofd.SafeFileNames))
                        {
                            ls.Add(listbScript.Items[i].ToString());
                        }
                    }
                    this.listbScript.Items.Clear();
                    ls.AddRange(ofd.SafeFileNames);
                    this.listbScript.Items.AddRange(ls.ToArray());
                }
            }
        }
        private bool tes(string str, string[] arrStr)
        {
            bool tmp = false;
            foreach (string ss in arrStr)
            {
                if (ss == str)
                {
                    tmp = true;
                    break;
                }
            }
            return tmp;
        }
    }
}
