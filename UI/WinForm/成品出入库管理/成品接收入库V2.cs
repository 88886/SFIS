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
    public partial class Frm_Receiving_Storage : Office2007Form //Form
    {
        public Frm_Receiving_Storage(MainParent frm)
        {
            InitializeComponent();
            this.mFrm = frm;
        }
        MainParent mFrm;
        private void Frm_Receiving_Storage_Load(object sender, EventArgs e)
        {
            this.Enabled = false;
            txtlocid.Text = "NA";
            txtstore.Text = "1000";
            btnSubmit.Enabled = false;
        }
        string InputStr = string.Empty;

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
        private void btlocid_Click(object sender, EventArgs e)
        {            
            SelectData sd = new SelectData(this,
             FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetWarehouseListInfo()));//.GetAlltStorehouseLoctionInfo()));
            sd.ShowDialog();
        }

        private void tb_storenumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_storenumber.Text) && e.KeyCode == Keys.Enter)
            {
                DataTable mdtDgvstoreIn = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.Get_Warehouse_number_Info(this.tb_storenumber.Text.Trim()));
                 dgvstockIn.DataSource = mdtDgvstoreIn;
                 if (mdtDgvstoreIn.Rows.Count > 0)
                     btnSubmit.Enabled = true;
                 else btnSubmit.Enabled = false;

                DataTable toSap =FrmBLL.ReleaseData.arrByteToDataTable(refWebtZ_Whs_SAP_BackFlush.Instance.GetUpload_whs_Sap_Info_No("101",tb_storenumber.Text.Trim(),"LOTIN"));                
                dgvbackflush.DataSource=toSap;
                dgvbackflush.Columns[0].Visible = false;

                InputStr = tb_storenumber.Text.Trim();

                ShowPrgMsg(mLogMsgType.Incoming, "查询单据成功");
                tb_storenumber.SelectAll();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvstockIn.Rows.Count > 0)
                {
                    if (MessageBox.Show(string.Format("是否将单号[{0}],接收到仓库", InputStr), "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {

                        if (dgvstockIn.Rows[0].Cells["LOTIN"].Value.ToString() == InputStr)
                        {
                            DataTable ChktoSap = FrmBLL.ReleaseData.arrByteToDataTable(refWebtZ_Whs_SAP_BackFlush.Instance.GetUpload_whs_Sap_Info_No("101", tb_storenumber.Text.Trim(), "lotin"));

                            if (ChktoSap.Rows.Count == 0)
                            {

                                List<WebServices.tWarehouseWipTracking.Z_WHS_SAP_BACKFLUSHTable> Lszhsb = new List<WebServices.tWarehouseWipTracking.Z_WHS_SAP_BACKFLUSHTable>();
                                WebServices.tWarehouseWipTracking.Z_WHS_SAP_BACKFLUSHTable zhsb = null;

                                foreach (DataGridViewRow dr in dgvstockIn.Rows)
                                {
                                    zhsb = new WebServices.tWarehouseWipTracking.Z_WHS_SAP_BACKFLUSHTable();
                                    zhsb.LOTIN = dr.Cells["LOTIN"].Value.ToString();
                                    zhsb.WOID = dr.Cells["WOID"].Value.ToString();
                                    zhsb.PARTNUMBER = dr.Cells["PARTNUMBER"].Value.ToString();
                                    zhsb.PRODUCTNAME = dr.Cells["PRODUCTNAME"].Value.ToString();
                                    zhsb.LOTIN_QTY = Convert.ToInt32(dr.Cells["QTY"].Value.ToString());
                                    zhsb.MOVE_TYPE = "101";
                                    zhsb.PLANT = "2100";
                                    zhsb.UPLOAD_FLAG = "N";
                                    Lszhsb.Add(zhsb);
                                }

                                string _strErr = refWebtWarehouseWipTracking.Instance.Receiving_Storage(Lszhsb.ToArray(), "1", txtstore.Text.Trim(), txtlocid.Text.Trim(), mFrm.gUserInfo.userId, InputStr);
                                if (_strErr == "OK")
                                {
                                    ShowPrgMsg(mLogMsgType.Incoming, "SFIS 数据接收成功");
                                    UpLoadStockIn(InputStr);
                                }
                                else
                                {
                                    ShowPrgMsg(mLogMsgType.Error, "接收入库失败:" + _strErr);
                                }
                            }
                            else
                            {
                                ShowPrgMsg(mLogMsgType.Error, "此单号已经收货,如有数据有问题,请联系IT人员");
                            }
                        }
                        else
                        {
                            ShowPrgMsg(mLogMsgType.Error, "输入单据与查询结果不一致");
                        }

                    }
                }
                else
                {
                    ShowPrgMsg(mLogMsgType.Error, "没有可以接收入库的单据");
                }
            }
            catch (Exception ex)
            {
                ShowPrgMsg(mLogMsgType.Error, "程序发生异常:"+ex.Message);
            }
            finally
            {
                btnSubmit.Enabled = false;
                dgvbackflush.DataSource = null;
                dgvstockIn.DataSource = null;
                tb_storenumber.Text = "";
            }
        }

        private void UpLoadStockIn(string lotin)
        {
            ShowPrgMsg(mLogMsgType.Normal, "开始上传SAP.....");
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.GetUpload_whs_Sap_Info_No("101",lotin,"LOTIN"));

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["UPLOAD_FLAG"].ToString() == "N")
                        UpLoadSAP_StockIn(dr["LOTIN"].ToString(), dr["woId"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(), Convert.ToInt32(dr["LOTIN_QTY"].ToString()), dr["ROWID"].ToString());
                }
            }
            ShowPrgMsg( mLogMsgType.Normal,"上传SAP结束.....");
        }
        private void UpLoadSAP_StockIn(string SFCNmuber, string woId, string PartNo, string Product, int QTY, string sRowid)
        {
            string[] LsMsg = RefWebService_BLL.refWebSapConnector.Instance.Get_Z_RFC_AUFNR_MIGO(new WebServices.SapConnector.Z_RFC_AUFNR_MIGO()
            {
                PartNumber = PartNo,
                woId = woId,
                QTY = QTY,
                EMP_NO = mFrm.gUserInfo.userId,
                EMP_NAME = mFrm.gUserInfo.username
            });
            if (LsMsg.Length < 5)
            {
                ShowPrgMsg( mLogMsgType.Error,LsMsg[0].ToString());
            }
            else
            {
                string SAP_STOCKNO = LsMsg[0].ToString();
                string SAP_TYPE = LsMsg[1].ToString();
                string SAP_E_ID = LsMsg[2].ToString();
                string SAP_E_NUM = LsMsg[3].ToString();
                string SAP_MSG = LsMsg[4].ToString();
                if (SAP_TYPE.ToUpper() == "S")
                {
                    string sRes = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.Update_Z_Whs_SAP_BackFlush_Flag(sRowid, "Y");
                    ShowPrgMsg(mLogMsgType.Normal,string.Format("上传SAP成功,SAP单号[{0}],入库单[{6}],工单[{1}],料号[{2}],产品型号[{3}],数量[{4}],时间[{5}] ->" + sRes, SAP_STOCKNO, woId, PartNo, Product, QTY.ToString(), DateTime.Now.ToString(), SFCNmuber));
                }
                else
                {
                    string sSAP_MSG = string.Format("工单:{0},料号:{1},型号:{2},数量:{3},MSG:{4},时间[{5}]", woId, PartNo, Product, QTY.ToString(), SAP_TYPE + "-" + SAP_E_ID + "-" + SAP_E_NUM + "-" + SAP_MSG, DateTime.Now.ToString());
                    ShowPrgMsg(SAP_MSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, "SAP:" + sSAP_MSG);

                    string SFCMSG = RefWebService_BLL.refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG()
                    {
                        userId = "UPLOAD",
                        prg_name = "UPSTOCKIN",
                        action_type = woId,
                        action_desc = sSAP_MSG
                    });
                    ShowPrgMsg(SFCMSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, "SFC Log:" + SFCMSG);
                }
            }
        }
    }
}
