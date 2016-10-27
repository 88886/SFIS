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
    public partial class SAP_UPLOAD : Office2007Form //Form
    {
        public SAP_UPLOAD(MainParent info)
        {
            InitializeComponent();
            mFrm = info;
        }
        MainParent mFrm;
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };

        public void ShowPrgMsg(string msg, mLogMsgType msgtype, RichTextBox rtbmsg)
        {
            try
            {
                 rtbmsg.Invoke(new EventHandler(delegate
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
        private void SAP_UPLOAD_Load(object sender, EventArgs e)
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

            tabControl1.SelectedTabIndex = tabControl1.Tabs.Count - 1;
        }
        #region 入库抛SAP
        private void UpLoadStockIn()
        {
            //bttstockin.Enabled = false;
            //ShowPrgMsg("上传SAP.....", mLogMsgType.Normal, rtbstockin);         
            //DataTable dt =FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.GetUpload_whs_Sap_Info("101"));
     
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {                    
            //       UpLoadSAP_StockIn(dr["LOTIN"].ToString(), dr["woId"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(), Convert.ToInt32(dr["LOTIN_QTY"].ToString()), dr["ROWID"].ToString());                 
            //    }
            //}
            //ShowPrgMsg("上传SAP结束.....", mLogMsgType.Normal, rtbstockin);
            //bttstockin.Enabled = true;
        }
        private void UpLoadSAP_StockIn(string SFCNmuber, string woId, string PartNo, string Product, int QTY, string sRowid)
        {
            //string[] LsMsg = RefWebService_BLL.refWebSapConnector.Instance.Get_Z_RFC_AUFNR_MIGO(new WebServices.SapConnector.Z_RFC_AUFNR_MIGO()
            //{
            //    PartNumber = PartNo,
            //    woId = woId,
            //    QTY = QTY,
            //    EMP_NO = sFinfo.gUserInfo.userId,
            //    EMP_NAME = sFinfo.gUserInfo.username
            //});

            ////   "02", null, null, "1", woId, PartNo,QTY, "F", "2100", "101");
            //if (LsMsg.Length < 5)
            //{
            //    ShowPrgMsg(LsMsg[0].ToString(), mLogMsgType.Error, rtbstockin);
            //}
            //else
            //{
            //    string SAP_STOCKNO = LsMsg[0].ToString();
            //    string SAP_TYPE = LsMsg[1].ToString();
            //    string SAP_E_ID = LsMsg[2].ToString();
            //    string SAP_E_NUM = LsMsg[3].ToString();
            //    string SAP_MSG = LsMsg[4].ToString();
            //    if (SAP_TYPE.ToUpper() == "S")
            //    {
            //        string sRes = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.Update_Z_Whs_SAP_BackFlush_Flag(sRowid, "Y");
            //        ShowPrgMsg(string.Format("上传SAP成功,SAP单号[{0}],入库单[{6}],工单[{1}],料号[{2}],产品型号[{3}],数量[{4}],时间[{5}] ->" + sRes, SAP_STOCKNO, woId, PartNo, Product, QTY.ToString(), DateTime.Now.ToString(), SFCNmuber), mLogMsgType.Normal, rtbstockin);
            //    }
            //    else
            //    {


            //        string sSAP_MSG = string.Format("工单:{0},料号:{1},型号:{2},数量:{3},MSG:{4},时间[{5}]", woId, PartNo, Product, QTY.ToString(), SAP_TYPE + "-" + SAP_E_ID + "-" + SAP_E_NUM + "-" + SAP_MSG, DateTime.Now.ToString());
            //        ShowPrgMsg("SAP:" + sSAP_MSG, SAP_MSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, rtbstockin);

            //        string SFCMSG = RefWebService_BLL.refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG()
            //        {
            //            userId = "UPLOAD",
            //            prg_name = "UPSTOCKIN",
            //            action_type = woId,
            //            action_desc = sSAP_MSG
            //        });
            //        ShowPrgMsg("SFC Log:" + SFCMSG, SFCMSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, rtbstockin);
            //    }
            //}
        }
        #endregion

        #region 重工出库上抛
        private void UpLoadStockOut()
        {
            //imbt_stockout.Enabled = false;          
            //ShowPrgMsg("上传SAP.....", mLogMsgType.Normal, rtbstockout);
            //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.GetUpload_whs_Sap_Info("261"));
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {                   
            //       UpLoadSAP_StockOut("NA", dr["woId"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(), Convert.ToInt32(dr["LOTOUT_QTY"].ToString()), dr["ROWID"].ToString());                 
            //    }
            //}           
            //ShowPrgMsg("上传SAP结束.....", mLogMsgType.Normal, rtbstockout);
            //imbt_stockout.Enabled = true;
        }
        private void UpLoadSAP_StockOut(string SFCNmuber,string woId, string PartNo, string Product, int QTY, string sRowid)
        {
            //string[] LsMsg = RefWebService_BLL.refWebSapConnector.Instance.Get_Z_RFC_AUFNR_MIGO_OUT(new WebServices.SapConnector.Z_RFC_AUFNR_MIGO()
            //{
            //     PartNumber=PartNo,
            //      woId=woId,
            //       QTY=QTY,
            //        EMP_NO=sFinfo.gUserInfo.userId,
            //        EMP_NAME=sFinfo.gUserInfo.username
            //});
                
                
            ////    "03", null, null, "2", woId, PartNo, QTY, "0001", "2100", "261");
            //if (LsMsg.Length < 5)
            //{
            //    ShowPrgMsg(LsMsg[0].ToString(), mLogMsgType.Error,rtbstockout);
            //}
            //else
            //{
            //    string SAP_STOCKNO = LsMsg[0].ToString();
            //    string SAP_TYPE = LsMsg[1].ToString();
            //    string SAP_E_ID = LsMsg[2].ToString();
            //    string SAP_E_NUM = LsMsg[3].ToString();
            //    string SAP_MSG = LsMsg[4].ToString();
            //    if (SAP_TYPE.ToUpper() == "S")
            //    {
            //        string sRes = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.Update_Z_Whs_SAP_BackFlush_Flag(sRowid, "Y");
            //        ShowPrgMsg(string.Format("上传SAP成功,SAP单号[{0}],工单[{1}],料号[{2}],产品型号[{3}],数量[{4}],时间[{5}]" , SAP_STOCKNO, woId, PartNo, Product, QTY.ToString(),DateTime.Now.ToString()), mLogMsgType.Normal, rtbstockout);
            //    }
            //    else
            //    {
                   
            //        string sSAP_MSG = string.Format("工单:{0},料号:{1},型号:{2},数量:{3},MSG:{4},时间[{5}]",  woId, PartNo, Product, QTY.ToString(), SAP_TYPE + "-" + SAP_E_ID + "-" + SAP_E_NUM + "-" + SAP_MSG,DateTime.Now.ToString());
            //        ShowPrgMsg("SAP:" + sSAP_MSG, SAP_MSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, rtbstockout);

            //       string SFCMSG = RefWebService_BLL.refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG()
            //        {
            //            userId = "UPLOAD",
            //            prg_name = "SAP",
            //            action_type = woId,
            //            action_desc = sSAP_MSG
            //        });
            //        ShowPrgMsg( "SFC Log:" + SFCMSG,SFCMSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error,rtbstockout);
            //    }

            //}
        }
        #endregion

        #region 售出上抛
        private void UpLoadSale()
        {
            //imbt_sale.Enabled = false;
            //ShowPrgMsg("开始上传SAP.....", mLogMsgType.Normal, rtbsale);
            //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.GetUpload_whs_Sap_Info("601"));
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        UpLoadSAP_Sale( dr["LOTOUT"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(), Convert.ToInt32(dr["LOTOUT_QTY"].ToString()), dr["ROWID"].ToString());
            //    }
            //}
            //ShowPrgMsg("上传SAP结束.....", mLogMsgType.Normal, rtbsale);
            //imbt_sale.Enabled = true;
        }
        private void UpLoadSAP_Sale(string SAP_LOT,string PartNo,string Product,int QTY ,string sRowid)
        {
          //string[] LsMsg= RefWebService_BLL.refWebSapConnector.Instance.Get_Z_RFC_GD_DELIVERY(SAP_LOT);
          //if (LsMsg.Length < 4)
          //{
          //    ShowPrgMsg(LsMsg[0].ToString(), mLogMsgType.Error, rtbsale);
          //}
          //else
          //{              
          //    string SAP_TYPE = LsMsg[1].ToString();
          //    string SAP_E_ID = LsMsg[2].ToString();
          //    string SAP_E_NUM = LsMsg[3].ToString();
          //    string SAP_MSG = LsMsg[4].ToString();
          //    if (SAP_TYPE.ToUpper() == "S")
          //    {
          //        string sRes = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.Update_Z_Whs_SAP_BackFlush_Flag(sRowid, "Y");
          //        ShowPrgMsg(string.Format("上传SAP成功,SAP出货单[{0}],料号[{1}],产品型号[{2}],数量[{3}],时间[{4}]", SAP_LOT, PartNo, Product, QTY.ToString(), DateTime.Now.ToString()), mLogMsgType.Normal, rtbsale);
          //    }
          //    else
          //    {

          //        string sSAP_MSG = string.Format("出货单:{0},料号:{1},型号:{2},数量:{3},MSG:{4},时间[{5}]", SAP_LOT, PartNo, Product, QTY.ToString(), SAP_TYPE + "-" + SAP_E_ID + "-" + SAP_E_NUM + "-" + SAP_MSG, DateTime.Now.ToString());
          //        ShowPrgMsg("SAP:" + sSAP_MSG, SAP_MSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, rtbsale);

          //        string SFCMSG = RefWebService_BLL.refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG()
          //        {
          //            userId = "UPLOAD",
          //            prg_name = "SAP",
          //            action_type = SAP_LOT,
          //            action_desc = sSAP_MSG
          //        });
          //        ShowPrgMsg("SFC Log:" + SFCMSG, SFCMSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, rtbsale);
          //    }

          //}
        }
        #endregion

        #region 移库上抛
        /// <summary>
        /// 移库上抛
        /// </summary>
        private void UpLoadMove()
        {
            //RichTextBox rtb = rtbmove;

            //imbt_sale.Enabled = false;
            //ShowPrgMsg("开始上传SAP.....", mLogMsgType.Normal, rtb);
            //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtz_whs_move_store.Instance.Get_Z_WHS_MOVE_STORE());
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        UpLoadSAP_Move(dr["MOVE_STORE_ID"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(),
            //                            dr["MOVE_STORE"].ToString(), dr["IMMIGRATION_STORE"].ToString(), Convert.ToInt32(dr["QTY"].ToString()),
            //                            dr["ROWID"].ToString(),rtb);
            //    }
            //}
            //ShowPrgMsg("上传SAP结束.....", mLogMsgType.Normal, rtb);
            //imbt_sale.Enabled = true;
        }
        private void UpLoadSAP_Move(string SFCLOT,string PartNo, string Product,string sSTGE_LOC,string sMOVE_STLOC, int QTY, string sRowid,RichTextBox rtb)
        {
           /* Z_RFC_AUFNR_MIGO zfc = new Z_RFC_AUFNR_MIGO();
            zfc.EMP_NO = "K001947";
            zfc.EMP_NAME = "TEST";
            zfc.PartNumber = "900000258";
            zfc.STGE_LOC = "1000";
            zfc.QTY = 10;
            zfc.MOVE_STLOC = "1033";


            lszz = WHS_MOVE_Z_RFC_AUFNR_MIGO(zfc, "");*/

            //string[] LsMsg = RefWebService_BLL.refWebSapConnector.Instance.WHS_MOVE_Z_RFC_AUFNR_MIGO(new WebServices.SapConnector.Z_RFC_AUFNR_MIGO()
            //    {
            //        EMP_NO = sFinfo.gUserInfo.userId,
            //        EMP_NAME = sFinfo.gUserInfo.username,
            //        PartNumber = PartNo,
            //        STGE_LOC = sSTGE_LOC,
            //        QTY = QTY,
            //        MOVE_STLOC = sMOVE_STLOC
            //    }, "");
            //if (LsMsg.Length < 4)
            //{
            //    ShowPrgMsg(LsMsg[0].ToString(), mLogMsgType.Error, rtb);
            //}
            //else
            //{
            //    string SAP_LOT = LsMsg[0].ToString();
            //    string SAP_TYPE = LsMsg[1].ToString();
            //    string SAP_E_ID = LsMsg[2].ToString();
            //    string SAP_E_NUM = LsMsg[3].ToString();
            //    string SAP_MSG = LsMsg[4].ToString();
            //    if (SAP_TYPE.ToUpper() == "S")
            //    {
            //       // string sRes = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.Update_Z_Whs_SAP_BackFlush_Flag(sRowid, "Y");
            //        string sRes= refWebtz_whs_move_store.Instance.Update_Z_WHS_MOVE_STORES(sRowid);
            //        ShowPrgMsg(string.Format("上传SAP成功,SAP移库单号[{0}],料号[{1}],产品型号[{2}],数量[{3}],时间[{4}]", SAP_LOT, PartNo, Product, QTY.ToString(), DateTime.Now.ToString()), mLogMsgType.Normal, rtb);
            //    }
            //    else
            //    {

            //        string sSAP_MSG = string.Format("SFIS移库单:{0},料号:{1},型号:{2},数量:{3},MSG:{4},时间[{5}]", SFCLOT, PartNo, Product, QTY.ToString(), SAP_TYPE + "-" + SAP_E_ID + "-" + SAP_E_NUM + "-" + SAP_MSG, DateTime.Now.ToString());
            //        ShowPrgMsg("SAP:" + sSAP_MSG, SAP_MSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, rtb);

            //        string SFCMSG = RefWebService_BLL.refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG()
            //        {
            //            userId = "UPLOAD",
            //            prg_name = "SAP",
            //            action_type = SAP_LOT,
            //            action_desc = sSAP_MSG
            //        });
            //        ShowPrgMsg("SFC Log:" + SFCMSG, SFCMSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, rtb);
            //    }

            //}
        }
        #endregion

        private delegate void UpLoadSAP();
        UpLoadSAP SendUpLoadSAP;
        private delegate void OutUpLoadSAP();
        OutUpLoadSAP SendOutUpLoadSAP;
        private delegate void SaleLoadSAP();
        SaleLoadSAP SendSaleLoadSAP;
        private delegate void MoveLoadSAP();
        MoveLoadSAP SendMoveLoadSAP;


        private void bttstockin_Click(object sender, EventArgs e)
        {
                bttstockin.Enabled = false;
                SendUpLoadSAP = new UpLoadSAP(UpLoadStockIn);
                SendUpLoadSAP.BeginInvoke(null, null);       
        
        }

        private void imbt_stockout_Click(object sender, EventArgs e)
        {

            SendOutUpLoadSAP = new OutUpLoadSAP(UpLoadStockOut);
            SendOutUpLoadSAP.BeginInvoke(null, null);
           
        }

        private void imbt_sale_Click(object sender, EventArgs e)
        {
            SendSaleLoadSAP = new SaleLoadSAP(UpLoadSale);
            SendSaleLoadSAP.BeginInvoke(null,null);
        }

        private void imbt_move_Click(object sender, EventArgs e)
        {
            SendMoveLoadSAP = new MoveLoadSAP(UpLoadMove);
            SendMoveLoadSAP.BeginInvoke(null,null);
        }

        public DataTable dt_out = new DataTable();
        private void bt_woid_Click(object sender, EventArgs e)
        {
            dgv_woid.AutoGenerateColumns = false;
            bt_woid_sap.Enabled = true;

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("WOID", txt_woid.Text.ToString().Trim());
            dic.Add("DEBCRED", "OUT");
            dt_out = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.WMS_get_R_Sap_Back_Shipping(FrmBLL.ReleaseData.DictionaryToJson(dic)));
            if (dt_out.Rows.Count == 0)
                ShowPrgMsg("无资料，请确认是否已经上抛完成", mLogMsgType.Error, rtb_woid);
            dgv_woid.DataSource = dt_out;
        }

        private void txt_woid_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_woid.Text) && (e.KeyCode == Keys.Enter))
            {
                bt_woid_Click(null, null);
            }
        }

        private void bt_woid_sap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_woid.Text))
            {
                ShowPrgMsg("按照工单上抛SAP，请输入工单号码！", mLogMsgType.Error, rtb_woid);
                return;
            }

            bt_woid_Click(null, null);
            if (MessageBox.Show("是否要上抛数据?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (dt_out.Rows.Count == 0)
            {
                ShowPrgMsg("没有待上传数据", mLogMsgType.Error, rtb_woid);
                return;
            }
                IList<IDictionary<string, object>> list_back = new List<IDictionary<string, object>>();
                list_back = FrmBLL.publicfuntion.DataTableIsToDicList(dt_out);
                DataTable dt_sap = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.WMS_RFC_ZMM_GOODSMVT_CREATE_list(FrmBLL.ReleaseData.ListDictionaryToJson(list_back)));
                if (dt_sap.Rows[0][2].ToString() == "S")
                {
                    string _StrErr = refWebtR_Tr_Sn.Instance.WMS_Update_R_Sap_Back_Shipping(FrmBLL.ReleaseData.ListDictionaryToJson(list_back), dt_sap.Rows[0][0].ToString());
                    if (_StrErr == "OK")
                        ShowPrgMsg(string.Format("上传SAP成功,交易单号[{0}]", dt_sap.Rows[0][0].ToString()), mLogMsgType.Incoming, rtb_woid);
                    else
                        ShowPrgMsg(string.Format("更新SFIS上抛资料失败" + _StrErr), mLogMsgType.Error, rtb_woid);
                }
                else
                {
                    string sSAP_MSG = string.Format("上传SAP失败,MSG:[{0}]", dt_sap.Rows[0][2].ToString() + ":" + dt_sap.Rows[0][5].ToString());
                    ShowPrgMsg(sSAP_MSG, mLogMsgType.Error, rtb_woid);
                }
                bt_woid_sap.Enabled = false;
            
        }

        private void dgv_woid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgv_woid.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgv_woid.RowHeadersDefaultCellStyle.Font, rectangle,
                dgv_woid.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
