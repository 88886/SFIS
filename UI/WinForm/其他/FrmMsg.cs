using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class FrmMsg : Office2007Form// Form
    {
        public FrmMsg(Office2007Form frm,string woid, string msg)
        {
            InitializeComponent();
            this.mMsg = msg;
            this.mWoid = woid;
            this.mFrm = frm;
         
        }
        private string mMsg = string.Empty;
        private string mWoid = string.Empty;
        private Office2007Form mFrm;   
        private void FrmMsg_Load(object sender, EventArgs e)
        {
            if (mFrm is WorkOrderCreate)
            {
                if (!string.IsNullOrEmpty(mMsg))
                {
                    this.lbMsg.Text = mMsg;
                    eventloadwoinfo = new delegateloadwoinfo(loadwoinfo);
                    eventloadwoinfo.BeginInvoke(this.mWoid, null, null);
                }
                else
                {
                    this.lbMsg.Text = mMsg;
                    eventloadATE_Script = new delegateATE_Script(Get_ATE_Script);
                    eventloadATE_Script.BeginInvoke(this.mWoid, null, null);
                }
            }
        }

        private void btCanle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        private delegate void delegateloadwoinfo(string woid);
        delegateloadwoinfo eventloadwoinfo;

        private delegate void delegateATE_Script(string woid);
        delegateATE_Script eventloadATE_Script;

        private void loadwoinfo(string woid)
        {           
            (mFrm as WorkOrderCreate).gWoInfodt = FrmBLL.ReleaseData.arrByteToDataTable(refWebSapConnector.Instance.Get_Z_RFC_AFPO(woid));
            this.DialogResult = DialogResult.OK;
        }
        private void Get_ATE_Script(string woId)
        {
            this.Invoke(new EventHandler(delegate
            {
                try
                {
                    (mFrm as WorkOrderCreate).listbScript.Items.Clear();
                    string IniFileName = System.AppDomain.CurrentDomain.BaseDirectory + "Updatecfg.ini";
                    string FtpHost = FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "Host", IniFileName);
                    string User = FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "User", IniFileName);
                    string UsePwd = FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "Password", IniFileName);
                    FrmBLL.Ftp_Socket fm = new FrmBLL.Ftp_Socket(FtpHost, woId, User, UsePwd, 21);
                    this.lbMsg.Text = "正在获取脚本....";
                    List<string> ss = fm.FileList("*.ts");
                    foreach (string item in ss)
                    {
                        (mFrm as WorkOrderCreate).listbScript.Items.Add(item);
                    }
                    this.lbMsg.Text = "获取脚本完成,正在获取条码档....";
                    List<string> Lab = fm.FileList("*.Lab");
                    foreach (string item in Lab)
                    {
                        (mFrm as WorkOrderCreate).listbScript.Items.Add(item);
                    }
                    this.lbMsg.Text = "获取完成...";              
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    this.lbMsg.Text = ex.Message;
                    System.Threading.Thread.Sleep(3000);
                    this.DialogResult = DialogResult.No;
                }

            }));
        }
    }
}
