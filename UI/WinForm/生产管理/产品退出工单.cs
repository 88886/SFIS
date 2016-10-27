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
    public partial class Frm_OutOrder : Office2007Form //Form
    {
        public Frm_OutOrder(MainParent sInfo)
        {
            InitializeComponent();
            sMain = sInfo;
        }
        MainParent sMain;
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };

        public void SendPrgMsg(mLogMsgType msgtype, string msg)
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
        string sMo = string.Empty;
        private void Frm_OutOrder_Load(object sender, EventArgs e)
        {
            try
            {
                #region 添加应用程序
                if (this.sMain.gUserInfo.rolecaption == "系统开发员")
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
                this.sMain.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

            Labwo.Text = string.Empty;
            labpartnumber.Text = string.Empty;
            labproduct.Text = string.Empty;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tb_inputdata_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_inputdata.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    string InputData = tb_inputdata.Text.Trim().ToUpper();
                    if (InputData == "UNDO")
                    {
                        Labwo.Text = string.Empty;
                        sMo = string.Empty;
                        labpartnumber.Text = string.Empty;
                        labproduct.Text = string.Empty;
                        dgvsnList.Rows.Clear();
                        SendPrgMsg(mLogMsgType.Normal, "UNDO OK");
                    }
                    else
                    {
                        if (dgvsnList.Rows.Count > 200)
                        {
                            SendPrgMsg(mLogMsgType.Warning, "请先提交数据后,继续扫描...");
                            return;
                        }

                        if (string.IsNullOrEmpty(sMo))
                        {
                            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(InputData,null));
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["wostate"].ToString() == "2")
                                {
                                    Labwo.Text = sMo = dt.Rows[0]["woId"].ToString();
                                    labpartnumber.Text = dt.Rows[0]["PARTNUMBER"].ToString();
                                    labproduct.Text = dt.Rows[0]["PRODUCTNAME"].ToString();
                                    SendPrgMsg(mLogMsgType.Incoming, "工单信息确认完成");
                                }
                                else
                                {
                                    SendPrgMsg(mLogMsgType.Error, string.Format("工单状态为[{0}],请确认...", dt.Rows[0]["wostate"].ToString()));
                                }
                            }
                            else
                            {
                                SendPrgMsg(mLogMsgType.Error, "工单错误");
                            }
                        }
                        else
                        {
                            DataTable dtsn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo("ESN", InputData));
                            if (dtsn.Rows.Count > 0)
                            {
                                string ESN = dtsn.Rows[0]["ESN"].ToString();
                                string PartNumber = dtsn.Rows[0]["PARTNUMBER"].ToString();
                                string PRODUCT = dtsn.Rows[0]["PRODUCTNAME"].ToString();
                                string ERRFLAG = dtsn.Rows[0]["ERRFLAG"].ToString();
                                string SCRAPFLAG = dtsn.Rows[0]["SCRAPFLAG"].ToString();
                                string CTNNO = dtsn.Rows[0]["CARTONNUMBER"].ToString();
                                string woId = dtsn.Rows[0]["WOID"].ToString();
                                string wipstation = dtsn.Rows[0]["wipstation"].ToString();
                                //string ESN = dtsn.Rows[0]["ESN"].ToString();
                                if (woId == Labwo.Text)
                                {
                                    if (ERRFLAG == "0")
                                    {
                                        if (SCRAPFLAG == "1")
                                        {
                                            SendPrgMsg(mLogMsgType.Error, string.Format("此产品序号已经报废与领出，不可退出工单，请确认...", InputData));
                                            return;
                                        }
                                        if (CTNNO == "NA")
                                        {
                                            dgvsnList.Rows.Add(ESN,woId, PartNumber, PRODUCT, wipstation);
                                        }
                                        else
                                        {
                                            SendPrgMsg(mLogMsgType.Error, string.Format("此产品序号已经包装[{0}],请确认...", InputData));
                                        }
                                    }
                                    else
                                    {
                                        SendPrgMsg(mLogMsgType.Error, string.Format("此产品序号待维修[{0}],请确认...",InputData));
                                    }

                                }
                                else
                                {
                                    SendPrgMsg(mLogMsgType.Error, string.Format("工单不同[{0}],请确认...", woId));
                                }

                            }
                            else
                            {
                                SendPrgMsg(mLogMsgType.Error,string.Format( "序列号[{0}]检查失败",InputData));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {SendPrgMsg(mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    tb_inputdata.Text = "";
                }
            }
        }

        private void btncommit_Click(object sender, EventArgs e)
        {
            if (dgvsnList.Rows.Count != 0)
            {
                foreach (DataGridViewRow row in dgvsnList.Rows)
                {
                   //string sRES= refWebtWipTracking.Instance.UpdateTwipTracking(new WebServices.tWipTracking.tWipTrackingTable()
                   // {
                   //     ESN =row.Cells["ESN"].Value.ToString(), 
                   //     WO = "6666666"
                   // }, 0);

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("ESN", row.Cells["ESN"].Value.ToString());
                    dic.Add("WOID", "6666666");
                    string sRES = refWebtWipTracking.Instance.Update_Wip_Tracking(FrmBLL.ReleaseData.DictionaryToJson(dic));
                   if (sRES == "OK")
                   {
                       sRES = refWebtWoInfo.Instance.UpdateWoInfo( row.Cells["WOID"].Value.ToString());
                       if (sRES == "OK")
                       {
                           SendPrgMsg(mLogMsgType.Normal, string.Format("ESN[{0}]  {1}", row.Cells["ESN"].Value.ToString(), sRES));
                       }
                       else
                       {
                           SendPrgMsg(mLogMsgType.Error, string.Format("更新工单信息失败 ESN[{0}],提示【{1}】", row.Cells["ESN"].Value.ToString(), sRES));
                           return;
                       }
                   }
                   else
                   {
                       SendPrgMsg(mLogMsgType.Error, string.Format("更新WIP失败 ESN[{0}],提示【{1}】", row.Cells["ESN"].Value.ToString(), sRES));
                       return;
                   }
                }
                Labwo.Text = string.Empty;
                sMo = string.Empty;
                labpartnumber.Text = string.Empty;
                labproduct.Text = string.Empty;
                dgvsnList.Rows.Clear();
                SendPrgMsg(mLogMsgType.Normal, string.Format("退出工单完成"));

            }
            else
            {
                SendPrgMsg(mLogMsgType.Error,"没有可以提交的数据");
            }
        }

    }
}
