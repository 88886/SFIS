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
    public partial class Frm_ReworkByPiece : Office2007Form  //Form
    {
        public Frm_ReworkByPiece(MainParent Fmp)
        {
            InitializeComponent();
            sFrm = Fmp;
        }
        MainParent sFrm;

        string My_PCIP = string.Empty;
        string My_PCMAC = string.Empty;

        private void Frm_ReworkByPiece_Load(object sender, EventArgs e)
        {
            try
            {
                #region 添加应用程序
                if (this.sFrm.gUserInfo.rolecaption == "系统开发员")
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

            }
            catch (Exception ex)
            {
                this.sFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
            My_PCIP = FrmBLL.publicfuntion.GetIpv4();
            My_PCMAC = FrmBLL.publicfuntion.getMacList()[0];

            Clear_DATA();
            Get_Rework_NO();
        }
        string C_PARTNUMBER = string.Empty;
        private void Get_Rework_NO()
        {
            txt_reworkno.Text = refWebtReworkDetailInfo.Instance.GetReworkNo(sFrm.gUserInfo.userId);
        }
        private void Clear_DATA()
        {
            ListSN.Items.Clear();
            dgvSnList.Rows.Clear();
            LabTotal.Text = ListSN.Items.Count.ToString();
            txt_workorder.Text = string.Empty;
            txt_inputgroup.Text = string.Empty;
        }
        private void txt_esn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_esn.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (ListSN.Items.Count < 100)
                    {
                        foreach (string str in ListSN.Items)
                        {
                            if (str == txt_esn.Text)
                            {
                                throw new Exception("SN Duplication");
                            }
                        }

                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo("ESN", txt_esn.Text));
                        if (dt.Rows.Count == 0)
                            throw new Exception("NO SN");
                        if (dt.Rows[0]["SCRAPFLAG"].ToString() != "0")
                            throw new Exception(string.Format("SN HAS SCRAP"));
                        if (dt.Rows[0]["ERRFLAG"].ToString() != "0")
                            throw new Exception(string.Format("SN IN REPAIR"));
                        if (dt.Rows[0]["CARTONNUMBER"].ToString() != "NA")
                            throw new Exception(string.Format("SN HAVING CARTONNUMBER"));
                        if (dt.Rows[0]["STORENUMBER"].ToString() != "NA")
                            throw new Exception(string.Format("SN IN STOCKIN"));
                      

                        if (string.IsNullOrEmpty(txt_workorder.Text))
                        {
                            txt_workorder.Text = dt.Rows[0]["WOID"].ToString();
                            DataTable dt_wo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(txt_workorder.Text,null));
                            txt_inputgroup.Text = dt_wo.Rows[0]["INPUTGROUP"].ToString();
                            C_PARTNUMBER = dt_wo.Rows[0]["PARTNUMBER"].ToString();
                        }
                        else
                        {
                            if (txt_workorder.Text != dt.Rows[0]["WOID"].ToString())
                                throw new Exception(string.Format("WO Different [{0}]≠[{1}]", dt.Rows[0]["WOID"].ToString(), txt_workorder.Text));
                        }
                        if (dt.Rows[0]["NEXTSTATION"].ToString() != "NA" && dt.Rows[0]["NEXTSTATION"].ToString() == txt_inputgroup.Text)
                            throw new Exception("This SN NEXTSTATION="+txt_inputgroup.Text);
                        ListSN.Items.Add(dt.Rows[0]["ESN"].ToString());
                        LabTotal.Text = ListSN.Items.Count.ToString();
                    }
                    else
                    {
                        MessageBox.Show("一次最多处理100片产品");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    txt_esn.SelectAll();
                }
            }
        }

        private void imbt_Clear_Click(object sender, EventArgs e)
        {
            Clear_DATA();
            
        }

        private void imbt_Exectue_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListSN.Items.Count > 0)
                {
                    if (MessageBox.Show("确定解除绑定关系?", "解除绑定提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        dgvSnList.Rows.Clear();
                        string _StrErr = string.Empty;

                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("WOID", txt_workorder.Text);
                        dic.Add("PARTNUMBER", C_PARTNUMBER);
                        dic.Add("MEMO", "NA");
                        dic.Add("REWORKDESC", "Release_Bound IP:" + My_PCIP);
                        dic.Add("WORKDATE", System.DateTime.Now.ToString("yyyyMMdd"));
                        dic.Add("REWORKNO", txt_reworkno.Text);
                        dic.Add("TOTALQTY", ListSN.Items.Count.ToString());
                        dic.Add("USERID", sFrm.gUserInfo.userId);

                        _StrErr = refWebtReworkDetailInfo.Instance.InsertReworkDetail(FrmBLL.ReleaseData.DictionaryToJson(dic));

                        if (_StrErr != "OK")
                        {
                            MessageBox.Show("Insert ReworkDetail Error:" + _StrErr);
                            return;
                        }
                        foreach (string str in ListSN.Items)
                        {
                          _StrErr=  InserSnUnBind(str);
                          if (_StrErr != "OK")
                              throw new Exception(string.Format("InserSnUnBind Error:[{0}]-->{1}", str, _StrErr));
                            _StrErr = refWebtReworkDetailInfo.Instance.Release_Bound(str, txt_inputgroup.Text, txt_reworkno.Text);
                            if (_StrErr == "OK")
                            {
                                dgvSnList.Rows.Add(str, txt_reworkno.Text);

                            }
                            else
                            {
                                MessageBox.Show("Release_Bound Error:" + _StrErr);
                                return;
                            }

                        }

                        Get_Rework_NO();
                        ListSN.Items.Clear();
                        txt_esn.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("NO DATA");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        /// <summary>
        /// 记录解除绑定信息
        /// </summary>
        /// <param name="ESN"></param>
        /// <returns></returns>
        private string InserSnUnBind(string ESN)
        {

            string _StrErr = "OK";
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyParts("ESN", ESN));
            IList<IDictionary<string, object>> ListDic = new List<IDictionary<string, object>>();
            IDictionary<string, object> dic = null;
            string M_MYGROUP = "NA";
             DataTable dtwip=FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.Get_WIP_TRACKING("ESN",ESN,"WIPSTATION"));
            if (dtwip.Rows.Count>0)
                M_MYGROUP=dtwip.Rows[0][0].ToString();


            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SNTYPE"].ToString() == "KPESN")
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("ESN", dr["ESN"].ToString());
                    dic.Add("WOID", dr["WOID"].ToString());
                    dic.Add("SNTYPE", dr["SNTYPE"].ToString());
                    dic.Add("SNVAL", dr["SNVAL"].ToString());
                    dic.Add("STATION", dr["STATION"].ToString());
                    dic.Add("KPNO", dr["KPNO"].ToString());
                    dic.Add("RECDATE",Convert.ToDateTime(dr["RECDATE"]));
                    dic.Add("USERID", sFrm.gUserInfo.userId);
                    dic.Add("UNBIND_STATION", M_MYGROUP);
                    dic.Add("PC_IP", My_PCIP);
                    dic.Add("PC_MAC", My_PCMAC);
                    ListDic.Add(dic);
                }
            }
            if (ListDic.Count > 0)
                _StrErr = refWebtSnUnBind.Instance.Insert_Sn_UnBind(FrmBLL.ReleaseData.ListDictionaryToJson(ListDic));

            return _StrErr;

        }
    }
}
