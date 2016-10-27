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
    public partial class FrmScrap : Office2007Form //Form
    {
        public FrmScrap(MainParent sInfo)
        {
            InitializeComponent();
            sMain = sInfo;
        }

        MainParent sMain;
        /// <summary>
        /// 原因代码
        /// </summary>
        DataTable dt = null;
        /// <summary>
        /// 库位
        /// </summary>
        DataTable dtz = null;
        private void FrmScrap_Load(object sender, EventArgs e)
        {


            //string C_RES = "";
            //BLL.Db_Procedure SSS = new BLL.Db_Procedure();
            //SSS.PRO_TEST_STOCKIN("A16D00008D", "PACK_StoreIn", "PACK_StoreIn", "FX005563-147", "NA", "S080101", out C_RES);


             dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtReasonCode.Instance.GetReasonCode());
            foreach (DataRow dr in dt.Rows)
            {
                cbreason.Items.Add(dr["reasoncode"].ToString());
            }
             dtz = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseInfo());
            foreach (DataRow dr in dtz.Rows)
            {
                cb_loc.Items.Add(dr[0].ToString());
            }
            cbreason.SelectedIndex = 0;
            cb_loc.SelectedIndex = 0;
        }

        private void rdsmt_CheckedChanged(object sender, EventArgs e)
        {
            if (rdsmt.Checked)
            {
                tbgroup.Text = "SC_SMT";
            }
        }

        private void rdtest_CheckedChanged(object sender, EventArgs e)
        {
            if (rdtest.Checked)
            {
                tbgroup.Text = "SC_TEST";
            }
        }

        private void tbesn_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbesn.Text)) && (e.KeyCode == Keys.Enter))
            {
                try
                {
                    string colnum = "ESN";
                    if (rbmac.Checked)
                        colnum = "MAC";
                    if (rbimei.Checked)
                        colnum = "IMEI";
                    if (rbsn.Checked)
                        colnum = "SN";
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo(colnum, tbesn.Text.Trim()));

                    if (dt.Rows.Count != 0)
                    {
                        string sEsn = dt.Rows[0]["ESN"].ToString();
                        if (dt.Rows[0]["ERRFLAG"].ToString() == "0")
                        {
                            if (dt.Rows[0]["SCRAPFLAG"].ToString().ToUpper() == "1")                           
                            {
                                ShowPrgMsg(mLogMsgType.Error, "该产品已经报废或虚拟入账,请确认......");
                                tbesn.SelectAll();
                                return;
                            }
                        }
                        else
                        {
                            ShowPrgMsg(mLogMsgType.Error, "该产品在修复站,请先从修复转出.");
                            tbesn.SelectAll();
                            return;
                        }

                        if (dt.Rows[0]["STORENUMBER"].ToString() != "NA" && !string.IsNullOrEmpty(dt.Rows[0]["STORENUMBER"].ToString()))
                        {
                            ShowPrgMsg(mLogMsgType.Error, "此产品在仓库或已经报废..." );
                            tbesn.SelectAll();
                            return;
                        }

                        if (dt.Rows[0]["WOID"].ToString() != lab_wo.Text)
                        {
                            ShowPrgMsg(mLogMsgType.Error, "工单不同--->" + dt.Rows[0]["WOID"].ToString());
                            tbesn.SelectAll();
                            return;
                        }
                        dataGridView1.Rows.Add(dt.Rows[0]["WOID"].ToString(), dt.Rows[0]["PARTNUMBER"].ToString(), dt.Rows[0]["WOID"].ToString(),cb_loc.Text,cbreason.Text);
                        string sMO = dt.Rows[0][1].ToString();
                        //string res = refWebtWipTracking.Instance.UpdateScrapWipTracking(new WebServices.tWipTracking.tWipTrackingTable()
                        //    {
                        //        WO = sMO,
                        //        locstation = tbgroup.Text,
                        //        wipstation = tbgroup.Text,
                        //        nextstation = "NA",
                        //        storenumber = DateTime.Now.ToString("yyyyMMdd"),
                        //        line = tbgroup.Text,
                        //        ESN = sEsn,
                        //        scrapflag = "1",
                        //        userId = sMain.gUserInfo.userId
                        //    });
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("WOID", sMO);
                        dic.Add("LOCSTATION", tbgroup.Text);
                        dic.Add("WIPSTATION", tbgroup.Text);
                        dic.Add("NEXTSTATION", "NA");
                        dic.Add("STORENUMBER", DateTime.Now.ToString("yyyyMMdd"));
                        dic.Add("LINE", tbgroup.Text);
                        dic.Add("ESN", sEsn);
                        dic.Add("SCRAPFLAG", "1");
                        dic.Add("USERID", sMain.gUserInfo.userId);                  
                        string res = refWebtWipTracking.Instance.UpdateScrapWipTracking( FrmBLL.ReleaseData.DictionaryToJson(dic));
                        ShowPrgMsg(mLogMsgType.Incoming, string.Format("SN: {0} , {1}", sEsn, res));
                    }
                    else
                    {
                        ShowPrgMsg(mLogMsgType.Error, "序列号不存在");
                        tbesn.SelectAll();
                        return;
                    }
                }

                catch (Exception ex)
                {
                    ShowPrgMsg(mLogMsgType.Error, "程序异常  " + ex.Message);
                    tbesn.SelectAll();

                }
                finally
                {
                    tbesn.Text = "";
                }
            }
        }

        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };
        public void ShowPrgMsg(mLogMsgType msgtype, string msg)
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

        private void cb_loc_Validated(object sender, EventArgs e)
        {
         
        }

        private void cbreason_Validating(object sender, CancelEventArgs e)
        {
          

        }

        private void cb_loc_SelectedIndexChanged(object sender, EventArgs e)
        {

            cb_loc_TextChanged(null,null );
        }

        private void cbreason_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbreason_TextChanged(null,null);
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void cb_loc_TextChanged(object sender, EventArgs e)
        {
            DataTable sdt = FrmBLL.publicfuntion.getNewTable(dtz, string.Format("storehouseId='{0}'", cb_loc.Text));

            if (sdt.Rows.Count != 0)
            {
                label6.Text = sdt.Rows[0][1].ToString();
            }
        }

        private void cbreason_TextChanged(object sender, EventArgs e)
        {
            DataTable sdt = FrmBLL.publicfuntion.getNewTable(dt, string.Format("reasoncode='{0}'", cbreason.Text));
            if (sdt.Rows.Count != 0)
            {
                label7.Text = sdt.Rows[0][2].ToString();
            }
        }

        private void lab_wo_Click(object sender, EventArgs e)
        {
            string sMO = Input.InputQuery.ShowInputBox("请输入工单", string.Empty);
            if (!string.IsNullOrEmpty(sMO))
            {
                lab_wo.Text = sMO;
            }
        }
    }
}
