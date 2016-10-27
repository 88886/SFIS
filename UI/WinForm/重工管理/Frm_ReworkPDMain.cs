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
    public partial class Frm_ReworkPDMain :  Office2007Form //Form
    {
        public Frm_ReworkPDMain(MainParent Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        MainParent mFrm;
        private void Frm_ReworkPDMain_Load(object sender, EventArgs e)
        {
            try
            {
                #region 添加应用程序
                if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
                {
                    IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                    FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("FUNID", "ReowrkPD");
                    dic.Add("PROGID", this.Name);
                    dic.Add("FUNNAME", "ReowrkPD");
                    dic.Add("FUNDESC", "生产线重工");
                    lsfunls.Add(dic);

                    dic = new Dictionary<string, object>();
                    dic.Add("FUNID", "ReowrkScrap");
                    dic.Add("PROGID", this.Name);
                    dic.Add("FUNNAME", "ReowrkScrap");
                    dic.Add("FUNDESC", "虚拟入库");
                    lsfunls.Add(dic);


                    FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                    dic = new Dictionary<string, object>();
                    dic.Add("PROGID", this.Name);
                    dic.Add("PROGNAME", this.Text);
                    dic.Add("PROGDESC", this.Text);
                    FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
                }
                #endregion

            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }


            #region 权限判定
            //先判定是否有使用该程序的权限
            string FunID = "ReowrkScrap,ReowrkPD";
            foreach (string Str in FunID.Split(','))
            {
                DataRow[] ArrDr = this.mFrm.gUserInfo.userPopList.Select(string.Format("progid='Frm_ReworkPDMain' and funId='{0}'", Str));
                if ((ArrDr == null || ArrDr.Length < 1) && this.mFrm.gUserInfo.rolecaption != "系统开发员")
                {
                    if (Str == "ReowrkScrap")
                        imbt_scrap.Enabled = false;
                    if (Str == "ReowrkPD")
                        imbt_ReworkPD.Enabled = false;
                    
                }
            }

            #endregion
        }

        private void imbt_ReworkPD_Click(object sender, EventArgs e)
        {

            mFrm.ReworkPD();
           
        }

        private void imbt_scrap_Click(object sender, EventArgs e)
        {
            mFrm.ReworkScrap();
        }

        private void imbt_ReworkInfo_Click(object sender, EventArgs e)
        {
            mFrm.ReworkInfoMation();
        }
    }
}
