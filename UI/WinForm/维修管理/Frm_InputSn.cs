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
    public partial class Frm_InputSn : Office2007Form //Form
    {
        public Frm_InputSn(Office2007Form frm)
        {
            InitializeComponent();
            mFrm = frm;
        }

        Office2007Form mFrm;
        private void Frm_InputSn_Load(object sender, EventArgs e)
        {

        }

        private void tb_InputSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_InputSN.Text) && e.KeyCode == Keys.Enter)
            {
                imbt_OK_Click(null,null);
            }
        }

        private void showmessage(string Msg)
        {
            MessageBox.Show(Msg, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tb_InputSN.Focus();
            tb_InputSN.SelectAll();
        }

        private void imbt_OK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_InputSN.Text))
            {
                (mFrm as Frm_RepairMain).Initialization();
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo("ESN",tb_InputSN.Text));
                if (dt.Rows.Count > 0)
                {
                    string Err_Flag = dt.Rows[0]["ERRFLAG"].ToString();
                    if (Err_Flag == "0" || string.IsNullOrEmpty(Err_Flag))
                    {
                        showmessage(string.Format( "该序列号[{0}]不在维修状态",tb_InputSN.Text));
                        return;
                    }
                    (mFrm as Frm_RepairMain).M_sThisEsn = dt.Rows[0]["ESN"].ToString();
                    (mFrm as Frm_RepairMain).M_sThisCraftId = dt.Rows[0]["LOCSTATION"].ToString();
                    (mFrm as Frm_RepairMain).M_sThisInputUser = dt.Rows[0]["USERID"].ToString();
                    (mFrm as Frm_RepairMain).M_sThisLineId = dt.Rows[0]["LINE"].ToString();
                    (mFrm as Frm_RepairMain).M_sThisLocation = dt.Rows[0]["LOCSTATION"].ToString();
                    (mFrm as Frm_RepairMain).M_sThisPartnumber = dt.Rows[0]["PARTNUMBER"].ToString();
                    (mFrm as Frm_RepairMain).M_sThiswoId = dt.Rows[0]["WOID"].ToString();
                    (mFrm as Frm_RepairMain).M_sMac = dt.Rows[0]["MAC"].ToString();
                    (mFrm as Frm_RepairMain).M_sImei = dt.Rows[0]["IMEI"].ToString();
                    (mFrm as Frm_RepairMain).M_sSn = dt.Rows[0]["SN"].ToString();
                    (mFrm as Frm_RepairMain).M_sProduct = dt.Rows[0]["PRODUCTNAME"].ToString();
                    (mFrm as Frm_RepairMain).M_sIn_Line_Time = dt.Rows[0]["IN_LINE_TIME"].ToString();
                    (mFrm as Frm_RepairMain).M_sIn_Station_Time = dt.Rows[0]["RECDATE"].ToString();
                    (mFrm as Frm_RepairMain).M_sThisAteStationNo = dt.Rows[0]["ATE_STATION_NO"].ToString();
                    (mFrm as Frm_RepairMain).M_sThisRouteCode = dt.Rows[0]["ROUTGROUPID"].ToString();
                 
                    DataTable dtRepair = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetRepairSnInfo(tb_InputSN.Text));
                    if (dtRepair.Rows.Count == 0)
                    {
                        showmessage("维修信息异常,没有待维修记录");
                        return;
                    }
                    DataTable dt_Waiting_Repair = FrmBLL.publicfuntion.getNewTable(dtRepair, "STATUS='1'");
                    if (dt_Waiting_Repair.Rows.Count == 0)
                    {
                        showmessage("维修信息异常,没有待维修记录");
                        return;
                    }

                    (mFrm as Frm_RepairMain).M_sThisErrorCode = dt_Waiting_Repair.Rows[0]["ERRORCODE"].ToString();

                    List<string> Ls_ErrorDesc= refWebtErrorCode.Instance.GetErrorCodeDesc(dt_Waiting_Repair.Rows[0]["ERRORCODE"].ToString()).ToList();
                    if (Ls_ErrorDesc.Count > 0)
                        (mFrm as Frm_RepairMain).M_sThisErrorCode_Desc = Ls_ErrorDesc[0];

                    DataTable dtRoute = FrmBLL.ReleaseData.arrByteToDataTable(refWebtRouteInfo.Instance.Get_Route_Info(dt.Rows[0]["ROUTGROUPID"].ToString()));
                    if (dt.Rows.Count == 0)
                    {
                        showmessage(string.Format("途程代码[{0}]异常,不存在此途程信息", dt.Rows[0]["ROUTGROUPID"].ToString()));
                        return;
                    }
                    DataTable dt_Repair_Route=FrmBLL.publicfuntion.getNewTable(dtRoute,string.Format("CRAFTNAME='{0}' AND STATION_FLAG='1'", (mFrm as Frm_RepairMain).M_sThisLocation));
                    if (dt_Repair_Route.Rows.Count == 0)
                    {
                        showmessage("途程异常,维修途程没有找到,请找IE确认...");
                        return;
                    }
                     (mFrm as Frm_RepairMain).M_MYGROUP=dt_Repair_Route.Rows[0]["NEXTCRAFTNAME"].ToString();

                    DataTable dt_RefLow_Route=FrmBLL.publicfuntion.getNewTable(dtRoute, string.Format("CRAFTNAME='{0}' AND STATION_FLAG='2'", (mFrm as Frm_RepairMain).M_MYGROUP));
                    if (dt_RefLow_Route.Rows.Count > 0)
                    {
                        (mFrm as Frm_RepairMain).Ls_M_sTheNextGroup.Clear();
                        foreach (DataRow dr in dt_RefLow_Route.Rows)
                        {
                            (mFrm as Frm_RepairMain).Ls_M_sTheNextGroup.Add(dr["NEXTCRAFTNAME"].ToString());
                        }
                        if ((mFrm as Frm_RepairMain).Ls_M_sTheNextGroup.Count==1)
                         (mFrm as Frm_RepairMain).M_sTheNextGroup = dt_RefLow_Route.Rows[0]["NEXTCRAFTNAME"].ToString();
                    }
                    else
                    {
                        showmessage(string.Format("途程代码[{0}]异常,没有回流途程", dt.Rows[0]["ROUTGROUPID"].ToString()));
                        return;
                    }
                    (mFrm as Frm_RepairMain).SHOW_RC_DATA();

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    showmessage("输入的号码不存在");
                }

            }
        }

        private void imbt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
