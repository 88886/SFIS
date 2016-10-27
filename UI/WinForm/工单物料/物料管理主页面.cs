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
    public partial class Frm_MaterialManage : Office2007Form
    {
        public Frm_MaterialManage(MainParent Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        MainParent mFrm;

        public string UserId = string.Empty;
        public string UserName = string.Empty;
        private void Control_Add(Office2007Form form)
        {
            rtbmsg.Clear();
            foreach (Control ctl in groupBoxForm.Controls)
            {
                if (ctl.Name == form.Name)
                {
                    return;
                }
            }
            foreach (Control ctl in groupBoxForm.Controls)
            {
                (ctl as Office2007Form).Close();
                groupBoxForm.Controls.Remove(ctl);
            }

            groupBoxForm.Controls.Clear(); //移除所有控件
            form.TopLevel = false;  //设置为非顶级窗体
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;//设置窗体为非边框样式
            form.Dock = System.Windows.Forms.DockStyle.Fill;//设置样式是否填充整个panel
            groupBoxForm.Controls.Add(form);//添加窗体
            groupBoxForm.Text = form.Text;
            form.Show();//窗体运行
        }

        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error } //文字输出类型
        public Color[] LogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };//文字颜色

        public void ShowMsg(mLogMsgType msgtype, string msg)
        {
            this.rtbmsg.Invoke(new EventHandler(delegate
            {
                this.rtbmsg.TabStop = false;
                rtbmsg.SelectedText = string.Empty;
                rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                rtbmsg.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtbmsg.AppendText(msg + "\n");
                rtbmsg.ScrollToCaret();
            }));
        }
        private void imbt_MaterialReceive_Click(object sender, EventArgs e)
        {
            Frm_ReceiveAndBack fra = new Frm_ReceiveAndBack(this);
            Control_Add(fra);
        }

        private void Frm_MaterialManage_Load(object sender, EventArgs e)
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

            UserId = mFrm.gUserInfo.userId;
            UserName = mFrm.gUserInfo.username;
        }

        private void imbt_RePrint_Click(object sender, EventArgs e)
        {
            Frm_Material_RePrint fra = new Frm_Material_RePrint(this);
            Control_Add(fra);
        }

        private void imbt_ReturnMaterial_Click(object sender, EventArgs e)
        {
            Frm_ReturnofMaterial frm = new Frm_ReturnofMaterial(this);
            Control_Add(frm);
        }

        private void imbt_SpecifiedMaterial_Click(object sender, EventArgs e)
        {
            Frm_SpecifiedMaterial fsrm = new Frm_SpecifiedMaterial(this);
            Control_Add(fsrm);
        }

        private void imbt_ModifyTrsnQty_Click(object sender, EventArgs e)
        {
            Frm_Modify_TrSn_Qty fsrm = new Frm_Modify_TrSn_Qty(this);
            Control_Add(fsrm);

        }

        private void imbt_Frm_Material_Split_Click(object sender, EventArgs e)
        {
            Frm_Material_Split fsrm = new Frm_Material_Split(this);
            Control_Add(fsrm);

        }
    }
}