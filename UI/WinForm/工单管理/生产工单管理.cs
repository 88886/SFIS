using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SFIS_V2
{
    public partial class Frm_MO_Manage : Office2007Form //Form
    {
        public Frm_MO_Manage(MainParent frm)
        {
            InitializeComponent();
            mFrm = frm;
        }
         MainParent mFrm;
        public string UserId = string.Empty;
        public string UserName = string.Empty;

        /// <summary>
        /// 工单建立标记0,正常,1 修改工单信息
        /// </summary>
        public int CreateMO_Flag = 0;
      //  public FrmBLL.AutoSizeFormClass Frm_Asc = new FrmBLL.AutoSizeFormClass();
        private void Frm_MO_Manage_Load(object sender, EventArgs e)
        {

            
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
            {
                IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                IDictionary<string, object> funls = new Dictionary<string, object>();
                funls.Add("FUNID", "WO_MANAGE");
                funls.Add("PROGID", this.Name);
                funls.Add("FUNNAME", "WO_MANAGE");
                funls.Add("FUNDESC", "工单维护");
                lsfunls.Add(funls);

                funls = new Dictionary<string, object>();
                funls.Add("FUNID", "ATE_SCRIPT");
                funls.Add("PROGID", this.Name);
                funls.Add("FUNNAME", "ATE_SCRIPT");
                funls.Add("FUNDESC", "工单脚本");
                lsfunls.Add(funls);

                funls = new Dictionary<string, object>();
                funls.Add("FUNID", "SNRANGE");
                funls.Add("PROGID", this.Name);
                funls.Add("FUNNAME", "SnRange");
                funls.Add("FUNDESC", "工单区间");
                lsfunls.Add(funls);

                funls = new Dictionary<string, object>();
                funls.Add("FUNID", "MODIFYMO");
                funls.Add("PROGID", this.Name);
                funls.Add("FUNNAME", "ModifyMO");
                funls.Add("FUNDESC", "工单修改");
                lsfunls.Add(funls);

                //funls = new Dictionary<string, object>();
                //funls.Add("FUNID", "SETLINE");
                //funls.Add("PROGID", this.Name);
                //funls.Add("FUNNAME", "SetLine");
                //funls.Add("FUNDESC", "线体设置");
                //lsfunls.Add(funls);


                funls = new Dictionary<string, object>();
                funls.Add("FUNID", "MODIFY_LINE");
                funls.Add("PROGID", "TE_SET_LINE");
                funls.Add("FUNNAME", "Modify_Line");
                funls.Add("FUNDESC", "TE在线更改线体");
                lsfunls.Add(funls);

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

            #region 权限判定
            //先判定是否有使用该程序的权限
            string FunID = "WO_MANAGE,ATE_SCRIPT,SNRANGE,MODIFYMO,SETLINE";
            foreach (string Str in FunID.Split(','))
            {
                DataRow[] ArrDr = this.mFrm.gUserInfo.userPopList.Select(string.Format("progid='Frm_MO_Manage' and funId='{0}'",Str));
                if ((ArrDr == null || ArrDr.Length < 1) && this.mFrm.gUserInfo.rolecaption != "系统开发员")
                {
                    if (Str == "WO_MANAGE")
                        imbt_wo_management.Enabled = false;
                    if (Str == "ATE_SCRIPT")
                        imbt_ATE_SCRIPT.Enabled = false;
                    if (Str == "SNRANGE")
                        imbt_SnRange.Enabled = false;
                    if (Str == "MODIFYMO")
                        imbt_ModifyMO.Enabled = false;
                    if (Str == "SETLINE")
                        imbt_lineset.Enabled = false;
                }
            }

            #endregion
        }
        #region 显示信息消息
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        public Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };

        public void SendMsg(mLogMsgType msgtype, string msg)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = mLogMsgTypeColor[(int)msgtype];
                    rtbmsg.AppendText(msg + "\n");
                    rtbmsg.ScrollToCaret();
                }));
            }
            catch
            {
            }
        }
        #endregion
        private void Control_Add(Office2007Form form)
        {
            foreach (Control ctl in panelEx1.Controls)
            {
                if (ctl.Name == form.Name)
                {
                    return;
                }               
            }
            foreach (Control ctl in panelEx1.Controls)
            {
                (ctl as Office2007Form).Close();
                panelEx1.Controls.Remove(ctl);
            }

            panelEx1.Controls.Clear(); //移除所有控件
            form.TopLevel = false;  //设置为非顶级窗体
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;//设置窗体为非边框样式
            form.Dock = System.Windows.Forms.DockStyle.Fill;//设置样式是否填充整个panel
            panelEx1.Controls.Add(form);//添加窗体
            form.Show();//窗体运行
           
            
        }

    
        private void imbt_wo_management_Click(object sender, EventArgs e)
        {
            CreateMO_Flag = 0;
           Frm_Crate_MO fcm = new Frm_Crate_MO(this);
            Control_Add(fcm);
        
        }

        private void imbt_ATE_SCRIPT_Click(object sender, EventArgs e)
        {
            Frm_ATE_SCRIPT fas = new Frm_ATE_SCRIPT(this);
            Control_Add(fas);

        }

        private void imbt_SnRange_Click(object sender, EventArgs e)
        {
            Frm_SnRange fas = new Frm_SnRange(this);
            Control_Add(fas);
        }

        private void imbt_ModifyMO_Click(object sender, EventArgs e)
        {
            CreateMO_Flag = 1;
            Frm_Crate_MO fcm = new Frm_Crate_MO(this);
            Control_Add(fcm);
        }

        private void imbt_lineset_Click(object sender, EventArgs e)
        {
          
            Frm_WO_LineSet fwl = new Frm_WO_LineSet(this,0);
            Control_Add(fwl);
        }

        private void Frm_MO_Manage_SizeChanged(object sender, EventArgs e)
        {
          
        }

        private void panelEx1_Resize(object sender, EventArgs e)
        {
           
        }
    }
}
