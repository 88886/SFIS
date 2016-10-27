using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Net;
using RefWebService_BLL;
using System.IO;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Drawing.Imaging;
using System.Threading;
using System.Drawing.Printing;

namespace SFIS_V2
{
    public partial class Frm_ProductOut : Office2007Form// Form
    {
        public Frm_ProductOut(MainParent mfm)
        {
            InitializeComponent();
            this.mFrm = mfm;
        }

        MainParent mFrm;
        /// <summary>
        /// 拉取出货订单信息进程结束标志
        /// </summary>
        private IAsyncResult iasyncresult, iasyncresult_1, iasync_outqty;
        private delegate void delegatesapinfo(string sapcode, bool flag);
        private delegatesapinfo eventloadsapinfo, eventloadsapinfo_1;

        /// <summary>
        /// 显示出库数量进程结束标志
        /// </summary>
        private delegate void deleshowoutqty();
        private deleshowoutqty eventshowoutqty;
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };

        /// <summary>
        /// 出货总数
        /// </summary>
        private int totaloutqty;
        private int totaloutqty_1 = 0;
        /// <summary>
        /// 拣货总数
        /// </summary>
        private int totalpickqty;
        private int totalpickqty_1 = 0;
        /// <summary>
        /// 重工工单
        /// </summary>
        string sReworkWO = string.Empty;
        /// <summary>
        /// 重工料号
        /// </summary>
        string sPartNumber = string.Empty;
        /// <summary>
        /// 重工产品名称
        /// </summary>
        string sProductName = string.Empty;
        /// <summary>
        /// 重工出库批次
        /// </summary>
        string sLotOutNo = string.Empty;

        #region 出库状态的变化
        private void cmbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResiveForm(dstatus[string.IsNullOrEmpty(cmbstatus.Text)?"--选择--":cmbstatus.Text], cmbstatus.Text.ToString());

        }

        void ResiveForm(string status, string key)
        {
            this.txtSAPCode.Text = "";
            this.txtPartNumber.Text = "";
            this.txtQty.Text = "";
            this.txtCustomerId.Text = "";
            if (status == "6" || status == "7")
            {
                this.lblSAP.Text = "SAP出库单号";
                this.txtSAPCode.Enabled = true;
                this.lblSAPList.Visible = true;
                this.dgvSAPList.Visible = true;
                this.txtPartNumber.Enabled = false;
                this.txtQty.Enabled = false;
                buttonX1.Visible = false;
                this.panelEx1.Height = 214;

            }
            else
            {
                if (key.Trim() == "--选择--")
                {
                    this.lblSAP.Text = "出库单号";
                    this.txtSAPCode.Text = "";
                }
                else
                {
                    Random rad = new Random();
                    this.lblSAP.Text = key.Trim() + "出库单号";
                    this.txtSAPCode.Text = status + string.Format("{0:yyMMdd}", DateTime.Now) + rad.Next(1000, 10000).ToString();
                }
                this.txtSAPCode.Enabled = false;
                this.lblSAPList.Visible = false;
                this.dgvSAPList.Visible = false;
                this.txtPartNumber.Enabled = true;
                this.txtQty.Enabled = true;
                buttonX1.Visible = true;
                this.panelEx1.Height = 72;

            }
        }

        Dictionary<string, string> dstatus = new Dictionary<string, string>();
        #endregion
        private void Frm_ProductOut_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
            {
                List<WebServices.tUserInfo.tFunctionList> lsfunls = new List<WebServices.tUserInfo.tFunctionList>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                FrmBLL.publicfuntion.AddProgInfo(new WebServices.tUserInfo.tProgInfo()
                {
                    progid = this.Name,
                    progname = this.Text,
                    progdesc = this.Text

                }, lsfunls);
            }
            #endregion

            #region 服务端加载
            IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            txtServerIP.Text = ipAddr.ToString();
            this.onDownLoadList += new dDownloadList(Frm_ProductOut_onDownLoadList);
            this.onDownLoadList1 += new dDownloadList1(Frm_ProductOut_onDownLoadList1);
            #endregion
            string[,] objs = { { "0", "--选择--" }, { "6", "  售出" }, { "7", "  借出" }, { "8", "  领用" }, { "9", "  重工" }, { "10", "  赠品" }, { "11", "  调拨" } };
            DataTable dtcmb = ConverttoDt(objs);
            //cmbstatus.DataSource = dtcmb;
            //cmbstatus.DisplayMember = "value";
            //cmbstatus.ValueMember = "key";
            this.panel9.Visible = false;

            foreach (DataRow dr in dtcmb.Rows)
            {
                cmbstatus.Items.Add(dr[1].ToString());
                cb_outputtype.Items.Add(dr[1].ToString());
                dstatus.Add(dr[1].ToString(), dr[0].ToString());
            }
            cmbstatus.SelectedIndex = 0;
            cb_outputtype.SelectedIndex = 0;

            dgv_carton.Rows.Add("Total", "0");
            dgv_pallet.Rows.Add("Total", "0");
            dgv_serial.Rows.Add("Total", "0");
            dgv_tray.Rows.Add("Total", "0");
        }

        #region 将M行N咧 数组转化为datatable
        public static DataTable ConverttoDt(string[,] Arrays)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("key", typeof(string));
            dt.Columns.Add("value", typeof(string));

            for (int i1 = 0; i1 < Arrays.GetLength(0); i1++)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < 2; i++)
                {
                    dr[i] = Arrays[i1, i].ToString();
                }
                dt.Rows.Add(dr);
            }

            return dt;

        }

        #endregion

        private void txtSAPCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtSAPCode.Text.Trim()) || e.KeyValue != 13)
                return;
            if (iasyncresult != null && !iasyncresult.IsCompleted)
            {
                MessageBoxEx.Show("还有任务没有完成,请稍后..");
                return;
            }
            bool _usesapdata = false;
            if (MessageBoxEx.Show("是否需要重新从SAP导入出货信息?\n重新导入请选择[Yes],否则请选择[NO]",
                "提示", MessageBoxButtons.YesNo,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _usesapdata = true;
            }
            eventloadsapinfo = new delegatesapinfo(this.LoadSAPInfo);
            iasyncresult = eventloadsapinfo.BeginInvoke(this.txtSAPCode.Text.Trim(), _usesapdata, null, null);

        }
        /// <summary>
        /// 加载SAP出货订单信息
        /// </summary>
        /// <param name="saplotcode">STO&SO</param>
        /// <param name="flag">true-从SAP拉取；false-从SFIS拉取</param>
        private void LoadSAPInfo(string saplotcode, bool flag)
        {
            if (flag)
            {
                DataTable mdtable = dtsapdata = FrmBLL.ReleaseData.arrByteToDataTable(refWebSapConnector.Instance.Get_Z_RFC_LIPS(saplotcode, ""));
                FillDataGridView(mdtable);
                if (mdtable == null || mdtable.Rows.Count < 1)
                {
                    this.mFrm.ShowPrgMsg("该SAP出货单号无数据,请确认...", MainParent.MsgType.Error);
                    this.txtSAPCode.Text = "";
                    return;
                }
                #region 向SFIS系统填充出货信息
                List<WebServices.tWarehouseWipTracking.tSapLodeInfo> lssap = new List<WebServices.tWarehouseWipTracking.tSapLodeInfo>();

                foreach (DataRow dr in mdtable.Rows)
                {
                    string SerialCode = "CU" + Common.RandomTimeSerial(DateTime.Now, 3);
                    lssap.Add(new WebServices.tWarehouseWipTracking.tSapLodeInfo()
                    {
                        ContactPerson = dr["KUNNR"].ToString(),
                        CustomerName = dr["NAME1"].ToString(),
                        CustomerId = SerialCode,
                        Partnumber = dr["MATNR"].ToString(),
                        ProductDesc = dr["MAKTX"].ToString(),
                        SAPCode = dr["VBELN"].ToString(),
                        QTY = Convert.ToInt32(dr["LFIMG"].ToString().Split('.')[0]),
                        SapWarehouse = dr["WERKS"].ToString(),
                        UserId = mFrm.gUserInfo.userId,
                        SFCcode = "NA",
                        Address = "NA",
                        Contractno = "NA",
                        Customername2 = "NA"

                    });

                }
                string msg = refWebtWarehouseWipTracking.Instance.InsertOutPutRecordList(lssap.ToArray());
                if (msg != "OK")
                {
                    this.mFrm.ShowPrgMsg(msg, MainParent.MsgType.Error);
                    return;
                }
                this.mFrm.ShowPrgMsg("保存出库订单信息成功!", MainParent.MsgType.Error);
                #endregion

                # region +++
                //for (int i = 0; i < dgvSAPList.Rows.Count; i++)
                //{
                //    string SerialCode = "CU" + Common.RandomTimeSerial(DateTime.Now, 3);
                //    refWebtWarehouseWipTracking.Instance.SaveSAPOutInfo(new WebServices.tWarehouseWipTracking.tSapLodeInfo()
                //    {
                //        ContactPerson = dgvSAPList["KUNNR", i].Value.ToString(),
                //        CustomerName = dgvSAPList["NAME1", i].Value.ToString(),
                //        CustomerId = SerialCode,
                //        Partnumber = dgvSAPList["MATNR", i].Value.ToString(),
                //        ProductDesc = dgvSAPList["MAKTX", i].Value.ToString(),
                //        SAPCode = dgvSAPList["VBELN", i].Value.ToString(),
                //        QTY = Convert.ToInt32(dgvSAPList["LFIMG", i].Value.ToString().Split('.')[0]),
                //        SapWarehouse = dgvSAPList["WERKS", i].Value.ToString(),
                //        UserId = mFrm.gUserInfo.userId
                //    });
                //}
                //this.mFrm.ShowPrgMsg("保存出库订单信息成功!", MainParent.MsgType.Error);
                #endregion
            }
            else
            {
                DataTable dtout = new DataTable();
                dtout = dtsapdata = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetSAPInfo(saplotcode));
                if (dtout == null || dtout.Rows.Count < 1)
                {
                    this.mFrm.ShowPrgMsg("SFIS系统中该出货单号无数据,请确认...", MainParent.MsgType.Error);
                     txtSAPCode.Invoke(new EventHandler(delegate
                    {
                    this.txtSAPCode.Text = "";
                    }));
                    return;
                }
                FillDataGridView(dtout);
            }
            lspartnumber.Clear();
            foreach (DataRow drp in dtsapdata.Rows)
            {
                if (!lspartnumber.Contains(drp["MATNR"].ToString()))
                lspartnumber.Add(drp["MATNR"].ToString());
            }
        }

        private void FillDataGridView(DataTable dt)
        {
            if (dt == null)
                return;
            this.dgvSAPList.Invoke(new EventHandler(delegate
            {
                this.dgvSAPList.DataSource = dt.DefaultView;
                this.dgvSAPList.Refresh();
            }));
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbstatus.Text.Trim()))
                return;
            if (dstatus[cmbstatus.Text] == "11")  /// update 2013/02/22  
            {
                SelectData dz = new SelectData(this, FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetWarehouseList()));
                dz.ShowDialog();
            }
            else
            {
                fCustomerInfo dz = new fCustomerInfo(mFrm, this);

                dz.ShowDialog();
            }
        }
        private bool CheckDupData(DataGridView dgv, string data)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (dgv.Rows[i].Cells[0].Value.ToString() == data)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 拣货数量
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        private int CountTotalPickQty(DataGridView dgv)
        {
            int sum = 0;
            for (int i = 1; i < dgv.RowCount; i++)
            {
                sum += Convert.ToInt32(dgv.Rows[i].Cells[1].Value);
            }
            return sum;

        }

        private int CountAllPickQty()
        {
            int total = 0;
            total = Convert.ToInt32(dgv_serial[1, 0].Value) + Convert.ToInt32(dgv_carton[1, 0].Value) +
                Convert.ToInt32(dgv_pallet[1, 0].Value) + Convert.ToInt32(dgv_tray[1, 0].Value);
            return total;
        }
        /// <summary>
        /// 出货数量
        /// </summary>
        /// <returns></returns>
        private int CountTotalOutQty()
        {
            int sum = 0;
            for (int i = 0; i < dgvSAPList.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dgvSAPList["LFIMG", i].Value.ToString().Split('.')[0]);
            }
            return sum;
        }
        string strCMD = string.Empty;
        string strInit = string.Empty;
        string strKeypart = string.Empty;
        string InputData = string.Empty;
        string Err = string.Empty;
        string status;
        string CustomerId;
        private void tb_input_KeyDown(object sender, KeyEventArgs e)
        {
            Err = "";
            InputData = this.tb_input.Text.Trim().ToUpper();
            status = dstatus[string.IsNullOrEmpty( cmbstatus.Text)?"--选择--": cmbstatus.Text];//--选择--
            CustomerId = txtCustomerId.Text.Trim();
            if (!string.IsNullOrEmpty(InputData) && e.KeyValue == 13)
            {
                this.tb_input.Text = "";
                #region 初始化
                if (string.IsNullOrEmpty(strInit))
                {
                    if (status == "6" || status == "7")
                    {
                        if (!string.IsNullOrEmpty(txtSAPCode.Text.Trim()) && dgvSAPList.Rows.Count > 0 && InputData == "INIT")
                        {
                            strInit = InputData;
                            totaloutqty = CountTotalOutQty();
                            lsOut.Clear();
                            ServerSendToClient("INIT OK!");
                            return;
                        }
                        else
                        {
                            strInit = "";
                            ServerSendToClient("ERROR:INIT Fail!");
                            return;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtPartNumber.Text.Trim()) &&
                            !string.IsNullOrEmpty(txtQty.Text.Trim()) &&
                            !string.IsNullOrEmpty(txtCustomerId.Text.Trim()) &&
                            InputData == "INIT")
                        {
                            strInit = InputData;
                            totaloutqty = Convert.ToInt32(txtQty.Text.Trim());
                            ServerSendToClient("INIT OK!");
                            return;
                        }
                        else
                        {
                            ServerSendToClient("ERROR:INIT Fail!");
                            strInit = "";
                            return;
                        }
                    }

                }
                #endregion
                else if (string.IsNullOrEmpty(strCMD))
                {
                    if (InputData == "SN" || InputData == "CARTON" || InputData == "PALLET" || InputData == "TRAY")
                    {
                        ServerSendToClient("CMD OK!");
                        strCMD = InputData;
                        return;
                    }
                    else
                    {
                        ServerSendToClient("ERROR:CMD Fail!");
                        strCMD = "";
                        return;
                    }
                }
                else if (InputData == "UNDO")
                {
                    strCMD = "";
                    strInit = "";
                    strKeypart = "";
                    lsOut.Clear();
                    ServerSendToClient("UNDO OK,Please INIT?");

                    dgv_carton.Rows.Clear();
                    dgv_pallet.Rows.Clear();
                    dgv_tray.Rows.Clear();
                    dgv_serial.Rows.Clear();
                    dgv_serial.Rows.Add("Total", "0");
                    dgv_tray.Rows.Add("Total", "0");
                    dgv_carton.Rows.Add("Total", "0");
                    dgv_pallet.Rows.Add("Total", "0");
                    return;
                }
                else if (InputData == "CLEAR")
                {
                    strCMD = "";
                    strKeypart = "";
                    ServerSendToClient("CLEAR OK,Please Input CMD?");
                    return;
                }
                else if (InputData == "END")
                {
                    #region 上传系统批量出库
                    ServerSendToClient("System to upload, Please wait");
                    BLL.tWarehouseWipTracking ww = new BLL.tWarehouseWipTracking();
                    //AddDataToList();
                    if (lsOut.Count < 1)
                    {
                        ServerSendToClient("ERROR:NO OUT PRODUCT~");
                        return;
                    }
                    string Res = ww.ProductOut_1(lsOut);//直接调用BLL
                    if (Res != "OK")
                    {
                        ServerSendToClient("ERROR:" + Res);
                        return;
                    }
                    ServerSendToClient("Upload OK!");
                    //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(
                    //    refWebtWarehouseWipTracking.Instance.ProductOutQueryII(this.txtSAPCode.Text.Trim()));
                    //if (dt.Rows.Count > 0)
                    //{
                    //    DataToExcel(dt);
                    //}
                    dgv_carton.Rows.Clear();
                    dgv_pallet.Rows.Clear();
                    dgv_tray.Rows.Clear();
                    dgv_serial.Rows.Clear();
                    dgv_serial.Rows.Add("Total", "0");
                    dgv_tray.Rows.Add("Total", "0");
                    dgv_carton.Rows.Add("Total", "0");
                    dgv_pallet.Rows.Add("Total", "0");
                    return;
                    #endregion
                }

                else if (totaloutqty < (totalpickqty = CountAllPickQty()))
                {
                    ServerSendToClient("ERROR:OUT QTY WRONG~");
                    return;
                }
                else
                {
                    #region 刷入出库产品信息
                    if (strCMD == "SN")
                    {
                        if (!CheckDupData(dgv_serial, InputData))
                        {
                            ServerSendToClient("ERROR: " + InputData + " Duplicate !");
                            return;
                        }
                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.ProductOutPick(InputData, "0", out Err));
                        if (!string.IsNullOrEmpty(Err))
                        {
                            ServerSendToClient("ERROR:[" + InputData + "]" + Err);
                            return;
                        }
                        if (!lspartnumber.Contains(dt.Rows[0]["partnumber"].ToString()))
                        {
                            ServerSendToClient("ERROR:[" + InputData + "] partnumber IS ERROR");
                            return;
                        }
                        if (totalpickqty + 1 > totaloutqty)
                        {
                            ServerSendToClient("ERROR:PICKQTY>OUTQTY!");
                            return;
                        }
                        foreach (DataRow drsn in dt.Rows)
                        {
                            lsOut.Add(new Entity.tWarehouseWipTrackingTable()
                            {
                                Esn = drsn["esn"].ToString(),
                                SapCode = txtSAPCode.Text.Trim(),
                                PartNumber = drsn["partnumber"].ToString(),
                                CustomerId = CustomerId,
                                userid = mFrm.gUserInfo.userId,
                                Status = status,
                                mFlag = txtQty.Text.Trim(), //存放出库总数量
                                Address = "NA"
                            });
                        }
                        dgv_serial.Rows.Add(InputData, dt.Rows.Count);
                        dgv_serial.Rows[0].Cells[1].Value = CountTotalPickQty(dgv_serial);
                        ServerSendToClient(InputData + ":OK!");
                    }
                    else if (strCMD == "CARTON")
                    {
                        if (string.IsNullOrEmpty(strKeypart))
                        {
                            if (InputData == "ESN" || InputData == "KEYPART")
                            {
                                ServerSendToClient("SELECT ESN/KERPART OK!");
                                strKeypart = InputData;
                                return;
                            }
                            else
                            {
                                ServerSendToClient("ERROR:SELECT ESN/KERPART Fail!");
                                strKeypart = "";
                                return;
                            }
                        }
                        else
                        {
                            DataTable dt = null;
                            if (strKeypart == "ESN")
                                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.ProductOutPick(InputData, "1", out Err));
                            if (strKeypart == "KEYPART")
                                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductOutPickInfo(InputData, "1"));
                            if (dt == null || dt.Rows.Count < 1)
                            {
                                ServerSendToClient("ERROR:NO STOCK!");
                                return;
                            }
                            if (!CheckDupData(dgv_carton, dt.Rows[0][0].ToString()))
                            {
                                ServerSendToClient("ERROR: " + InputData + " Duplicate !");
                                return;
                            }
                            if (!lspartnumber.Contains(dt.Rows[0]["partnumber"].ToString()))
                            {
                                ServerSendToClient("ERROR:[" + InputData + "] partnumber IS ERROR");
                                return;
                            }
                            if (totalpickqty + Convert.ToInt32(dt.Rows.Count) > totaloutqty)
                            {
                                ServerSendToClient("ERROR:PICKQTY>OUTQTY!");
                                return;
                            }
                            foreach (DataRow drcarton in dt.Rows)
                            {
                                lsOut.Add(new Entity.tWarehouseWipTrackingTable()
                                {

                                    Esn = drcarton["esn"].ToString(),
                                    SapCode = txtSAPCode.Text.Trim(),
                                    PartNumber = drcarton["partnumber"].ToString(),
                                    CustomerId = CustomerId,
                                    userid = mFrm.gUserInfo.userId,
                                    Status = status,
                                    mFlag = txtQty.Text.Trim(), //存放出库总数量
                                    Address = "NA"
                                });
                            }
                            dgv_carton.Rows.Add(dt.Rows[0]["cartonnumber"].ToString(), dt.Rows.Count);
                            dgv_carton.Rows[0].Cells[1].Value = CountTotalPickQty(dgv_carton);
                            ServerSendToClient(dt.Rows[0][0].ToString() + ":OK!");
                        }
                    }
                    else if (strCMD == "PALLET")
                    {
                        if (string.IsNullOrEmpty(strKeypart))
                        {
                            if (InputData == "ESN" || InputData == "KEYPART")
                            {
                                ServerSendToClient("SELECT ESN/KERPART OK!");
                                strKeypart = InputData;
                                return;
                            }
                            else
                            {
                                ServerSendToClient("ERROR:SELECT ESN/KERPART Fail!");
                                strKeypart = "";
                                return;
                            }
                        }
                        else
                        {
                            DataTable dt = null;
                            if (strKeypart == "ESN")
                                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.ProductOutPick(InputData, "2", out Err));
                            if (strKeypart == "KEYPART")
                                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductOutPickInfo(InputData, "2"));
                            if (dt == null || dt.Rows.Count < 1)
                            {
                                ServerSendToClient("ERROR:NO STOCK!");
                                return;
                            }
                            if (!CheckDupData(dgv_pallet, dt.Rows[0][0].ToString()))
                            {
                                ServerSendToClient("ERROR: " + InputData + " Duplicate !");
                                return;
                            }
                            if (!lspartnumber.Contains(dt.Rows[0]["partnumber"].ToString()))
                            {
                                ServerSendToClient("ERROR:[" + InputData + "] partnumber IS ERROR");
                                return;
                            }
                            if (totalpickqty + Convert.ToInt32(dt.Rows.Count) > totaloutqty)
                            {
                                ServerSendToClient("ERROR:PICKQTY>OUTQTY!");
                                return;
                            }
                            foreach (DataRow drpallet in dt.Rows)
                            {
                                lsOut.Add(new Entity.tWarehouseWipTrackingTable()
                                {
                                    Esn = drpallet["esn"].ToString(),
                                    CustomerId = CustomerId,
                                    PartNumber = drpallet["partnumber"].ToString(),
                                    SapCode = txtSAPCode.Text.Trim(),
                                    userid = mFrm.gUserInfo.userId,
                                    Status = status,
                                    mFlag = txtQty.Text.Trim(),
                                    Address = "NA"
                                });
                            }
                            dgv_pallet.Rows.Add(dt.Rows[0]["palletnumber"].ToString(), dt.Rows.Count);
                            dgv_pallet.Rows[0].Cells[1].Value = CountTotalPickQty(dgv_pallet);
                            ServerSendToClient(dt.Rows[0][0].ToString() + ":OK!");

                        }
                    }
                    else if (strCMD == "TRAY")
                    {
                        if (string.IsNullOrEmpty(strKeypart))
                        {
                            if (InputData == "ESN" || InputData == "KEYPART")
                            {
                                ServerSendToClient("SELECT ESN/KERPART OK!");
                                strKeypart = InputData;
                                return;
                            }
                            else
                            {
                                ServerSendToClient("ERROR:SELECT ESN/KERPART Fail!");
                                strKeypart = "";
                                return;
                            }
                        }
                        else
                        {
                            DataTable dt = null;
                            if (strKeypart == "ESN")
                                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.ProductOutPick(InputData, "3", out Err));
                            if (strKeypart == "KEYPART")
                                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductOutPickInfo(InputData, "3"));
                            if (dt == null || dt.Rows.Count < 1)
                            {
                                ServerSendToClient("ERROR:NO STOCK!");
                                return;
                            }
                            if (!CheckDupData(dgv_tray, dt.Rows[0][0].ToString()))
                            {
                                ServerSendToClient("ERROR: " + InputData + " Duplicate !");
                                return;
                            }
                            if (!lspartnumber.Contains(dt.Rows[0]["partnumber"].ToString()))
                            {
                                ServerSendToClient("ERROR:[" + InputData + "] partnumber IS ERROR");
                                return;
                            }
                            if (totalpickqty + Convert.ToInt32(dt.Rows.Count) > totaloutqty)
                            {
                                ServerSendToClient("ERROR:PICKQTY>OUTQTY!");
                                return;
                            }
                            foreach (DataRow drtray in dt.Rows)
                            {
                                lsOut.Add(new Entity.tWarehouseWipTrackingTable()
                                {
                                    Esn = drtray["esn"].ToString(),
                                    PartNumber = drtray["partnumber"].ToString(),
                                    CustomerId = CustomerId,
                                    SapCode = txtSAPCode.Text.Trim(),
                                    userid = mFrm.gUserInfo.userId,
                                    Status = status,
                                    mFlag = txtQty.Text.Trim(),
                                    Address = "NA"
                                });
                            }
                            dgv_tray.Rows.Add(dt.Rows[0]["tray"].ToString(), dt.Rows.Count);
                            dgv_tray.Rows[0].Cells[1].Value = CountTotalPickQty(dgv_tray);
                            ServerSendToClient(dt.Rows[0][0].ToString() + ":OK!");

                        }
                    }
                    #endregion
                }
            }

        }

        //将datatable的数据导出到excel
        private void DataToExcel(DataTable mdt)
        {
            Excel.ApplicationClass oExcel = new Excel.ApplicationClass();
            Excel.Workbooks oBooks = oExcel.Workbooks;

            Excel._Workbook oBook = null;

            oBook = (Excel._Workbook)(oExcel.Workbooks.Add(true));// 引用excel工作薄 

            for (int i = 0; i < mdt.Columns.Count; i++)
            {
                oExcel.Cells[2, i + 1] = mdt.Columns[i].ColumnName.ToString();// m_DataView.Columns[i].HeaderText.ToString();
            }

            for (int i = 0; i < mdt.Rows.Count; i++)
            {
                for (int j = 0; j < mdt.Columns.Count; j++)
                {
                    oExcel.Cells[i + 3, j + 1] = mdt.Rows[i][j].ToString();
                }
            }

            oExcel.Visible = true;
            object Missing = System.Reflection.Missing.Value;
            //  oExcel.Run("Sheet1.printdoc", Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing);

            oBook.Application.DisplayAlerts = false;



        }
        List<WebServices.tWarehouseWipTracking.tWarehouseWipTrackingTable> lsOut = new List<WebServices.tWarehouseWipTracking.tWarehouseWipTrackingTable>();
        private void AddDataToList()
        {
            lsOut.Clear();

            #region 添加所有数据到list
            for (int i = 1; i < dgv_serial.Rows.Count; i++)
            {
                lsOut.Add(new Entity.tWarehouseWipTrackingTable()
                {
                    Esn = dgv_serial[0, i].Value.ToString(),
                    CustomerId = CustomerId,
                    SapCode = txtSAPCode.Text.Trim(),
                    userid = mFrm.gUserInfo.userId,
                    Status = status,
                    mFlag = "0"
                });
            }
            for (int i = 1; i < dgv_carton.Rows.Count; i++)
            {
                lsOut.Add(new Entity.tWarehouseWipTrackingTable()
                {
                    Esn = dgv_carton[0, i].Value.ToString(),
                    CustomerId = CustomerId,
                    SapCode = txtSAPCode.Text.Trim(),
                    userid = mFrm.gUserInfo.userId,
                    Status = status,
                    mFlag = "1"
                });
            }
            for (int i = 1; i < dgv_pallet.Rows.Count; i++)
            {
                lsOut.Add(new Entity.tWarehouseWipTrackingTable()
                {
                    Esn = dgv_pallet[0, i].Value.ToString(),
                    CustomerId = CustomerId,
                    SapCode = txtSAPCode.Text.Trim(),
                    userid = mFrm.gUserInfo.userId,
                    Status = status,
                    mFlag = "2"
                });
            }
            for (int i = 1; i < dgv_tray.Rows.Count; i++)
            {
                lsOut.Add(new Entity.tWarehouseWipTrackingTable()
                {
                    Esn = dgv_tray[0, i].Value.ToString(),
                    CustomerId = CustomerId,
                    SapCode = txtSAPCode.Text.Trim(),
                    userid = mFrm.gUserInfo.userId,
                    Status = status,
                    mFlag = "3"
                });
            }
            #endregion

        }


        #region socket服务
        #region variable
        /**
         * 服务端所需变量
         * */
        //服务器端监听对象
        private TcpListener listener;
        private NetworkStream nsServer;
        //存储客户端的消息byte[]
        private byte[] msgBytesByClient;
        //用于标识用户客户端执行是何种操作(0:发送数据　1:发送文件 2:发送数据和文件)
        private int iFunction = 0;
        //存储文件跟数据的总共byte[]
        private byte[] totalBuffer;
        private PrintDocument pdoc;
        private String printText;

        private String idcard;


        /**
         * 客户端所需变量
         * */
        //客户端连接对象
        private TcpClient client;
        //客户端网络工作流
        private NetworkStream nsClient;
        //接收服务端传来的消息
        private byte[] msgBytesByServer;
        private Queue<string> clientQueue;
        private object locker;
        private Thread workerThread;
        #endregion

        #region 启动服务器
        /// <summary>
        /// 启动服务器按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartServer_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 port = int.Parse(txtServerPort.Text);
                IPAddress localAddr = IPAddress.Parse(this.txtServerIP.Text);
                Frm_ProductOut_onDownLoadList("正在创建" + txtServerIP.Text + "服务端TcpListener对象");
                //创建TcpListener监听对象
                listener = new TcpListener(localAddr, port);
                Frm_ProductOut_onDownLoadList("已成功创建TcpListener对象，端口号为：" + txtServerPort.Text);
                //启动对端口的连接请求监听
                listener.Start();
                //this.button1.Enabled = true;
                this.btnStartServer.Enabled = false;
                //启动定时器
                //this.timerByServer.Enabled = true;
                Thread th = new Thread(new ThreadStart(ListenerServer));
                th.IsBackground = true;
                th.Start();
                this.mFrm.ShowPrgMsg("成功启动服务!", MainParent.MsgType.Outgoing);
            }
            catch (FormatException fx)
            {
                MessageBox.Show("服务器IP地址或端口号不是有效数据！详细信息：" + fx.ToString(), "输入数据格式错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("IP地址或端口号为空！详细信息：" + ex.ToString(), "输入数据错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("TcpListener启动失败！详细信息：" + ex.ToString(), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 线程启动方法监听客户端的连接请求
        public void ListenerServer()
        {
            try
            {
                while (true)
                {
                    //判断是否客户端连接请求
                    if (listener.Pending())
                    {
                        // 接收客户端的请求，并创建一个客户端连接
                        TcpClient client = listener.AcceptTcpClient();
                        this.Frm_ProductOut_onDownLoadList("接收来自" + client.Client.RemoteEndPoint + "请求！");
                        //为每一个客户端创建一个线程用于监听客户端消息
                        Thread thread = new Thread(new ParameterizedThreadStart(CreateNetworkstream));
                        thread.IsBackground = true;
                        thread.Start(client);
                    }
                    Thread.Sleep(200);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("操作无效！,详细信息：" + ex.ToString());
            }
            catch (SocketException ex)
            {
                MessageBox.Show("发生异常，详细信息：" + ex.ToString());
            }
        }
        #endregion


        #region 客户端用于监听服务端消息的定时器
        private void timerByClient_Tick(object sender, EventArgs e)//?
        {
            try
            {
                lock (nsClient)
                {
                    if (nsClient != null && nsClient.CanRead)
                    {
                        if (nsClient.DataAvailable)
                        {
                            BeginReadServerMsg(nsClient);
                        }
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                MessageBox.Show("数据无法读取，流对象已被销毁或与服务端已断开连接！详细信息：" + ex.ToString());
            }
        }
        #endregion

        #region 读取来自客户端的数据
        /// <summary>
        /// 读取来自客户端的数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void BeginReadServerMsg(NetworkStream nsClient)
        {
            lock (nsClient)
            {
                try
                {
                    if (nsClient.CanRead && nsClient.DataAvailable)
                    {
                        //保存从流中读取的字节数
                        int readSize = 0;
                        //保存从流中读取的总字节数
                        int totalSize = 0;
                        //创建临时保存每次读取数据的流对象
                        MemoryStream ms = new MemoryStream();
                        while (nsClient.DataAvailable)
                        {
                            byte[] msgByte = new byte[10240];
                            //每次从流中读取1KB的数据
                            readSize = nsClient.Read(msgByte, 0, 10240);
                            //累计总共流中保存的字节数
                            totalSize += readSize;
                            //写入临时流中用于一次性全部读取数据
                            ms.Write(msgByte, 0, readSize);
                            Frm_ProductOut_onDownLoadList("已接收" + readSize + "字节的数据");
                        }
                        Frm_ProductOut_onDownLoadList("服务端共发送" + totalSize + "字节的数据");
                        msgBytesByServer = new byte[totalSize];

                        // **** 这里，从ms中读取数据前，ms指针必须回零，不然会出错。****
                        ms.Position = 0;

                        //将ms临时流中保存的数据全部读出
                        int readAllSize = ms.Read(msgBytesByServer, 0, totalSize);
                        Frm_ProductOut_onDownLoadList("已接收到" + readAllSize + "字节的数据,字节数据的长度：" + msgBytesByServer.Length);
                        //将接收到的byte[]转成String
                        String serverMsg = Encoding.Default.GetString(msgBytesByServer);

                        // **** 在数组上调用ToString()得不到数据的
                        Frm_ProductOut_onDownLoadList("服务端消息：" + serverMsg.ToString());
                        nsClient.Flush();
                        ms.Dispose();

                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
            }
        }
        #endregion

        #region 服务器用于监听客户端连接的定时器
        private void timerByServer_Tick(object sender, EventArgs e)
        {
            try
            {
                //判断是否客户端连接请求
                if (listener.Pending())
                {
                    // 接收客户端的请求，并创建一个客户端连接
                    TcpClient client = listener.AcceptTcpClient();
                    this.lbxServer.Items.Add("接收来自" + client.Client.RemoteEndPoint + "请求！");
                    //为每一个客户端创建一个线程用于监听客户端消息
                    Thread thread = new Thread(new ParameterizedThreadStart(CreateNetworkstream));
                    thread.IsBackground = true;
                    thread.Start(client);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("操作无效！详细信息：" + ex.ToString());
            }
            catch (SocketException ex)
            {
                MessageBox.Show("发生异常，详细信息：" + ex.ToString());
            }
        }
        #endregion

        #region 创建接受客户端请求的网络工作流对象
        /// <summary>
        /// 创建网络工作流对象
        /// </summary>
        /// <param name="o">线程执行方法参数对象</param>
        public void CreateNetworkstream(Object o)
        {
            TcpClient client = o as TcpClient;
            CreateNetworkstream(client);
        }
        #endregion

        #region 创建客户端网络工作流对象
        /// <summary>
        /// 接受客户端的连接请求并创建网络工作流对象
        /// </summary>
        /// <param name="client">连接请求TcpClient对象</param>
        public void CreateNetworkstream(TcpClient server)
        {

            try
            {
                //接受客户端连接请求
                NetworkStream nsServer = server.GetStream();
                this.nsServer = nsServer;
                Frm_ProductOut_onDownLoadList("创建" + server.Client.RemoteEndPoint + "客户端的网络工作流对象");
                while (true)
                {
                    if (nsServer == null)
                    {
                        Thread.CurrentThread.Abort();
                        this.Frm_ProductOut_onDownLoadList("销毁" + server.Client.RemoteEndPoint + "客户端的线程");
                        break;
                    }
                    if (nsServer != null && nsServer.CanRead && nsServer.DataAvailable)
                    {
                        ReadByClient(nsServer);
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建客户端网络工作流失败！详细信息：" + ex.ToString());
            }
        }
        #endregion

        #region 开始读取来自客户端的数据
        /// <summary>
        /// 读取客户端数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void ReadDataByClient(byte[] msgBytesByClient)
        {

            //将接收到的byte[]转成String
            String serverMsg = Encoding.Default.GetString(msgBytesByClient, 0, msgBytesByClient.Length);
            Frm_ProductOut_onDownLoadList("客户端说：" + serverMsg);
            tb_input.Invoke(new EventHandler(delegate
            {
                tb_input.Text = serverMsg;
            }));

            tb_input.Invoke(new EventHandler(delegate
            {
                tb_input_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }));
            //return serverMsg.ToString();
        }
        #endregion

        #region 读取来自客户端的文件
        /// <summary>
        /// 读取客户端文件数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void ReadByClient(NetworkStream nsServer)
        {
            //锁住客户端数据读取
            lock (nsServer)
            {
                try
                {

                    if (nsServer.DataAvailable)
                    {
                        //保存从流中读取的字节数
                        int readSize = 0;
                        //保存从流中读取的总字节数
                        long totalSize = 0;
                        Frm_ProductOut_onDownLoadList("正在接收客户端数据，请稍候……");
                        byte[] hBuffer = new byte[100];
                        nsServer.Read(hBuffer, 0, 100);
                        string hMsg = Encoding.Default.GetString(hBuffer, 0, 100);
                        totalSize += 100;


                        if (hMsg.Trim().Replace("\0", "").IndexOf("DATA:") != -1)
                        {
                            MemoryStream ms = new MemoryStream();
                            while (nsServer.DataAvailable)
                            {
                                try
                                {
                                    byte[] msgByte = new byte[40960];
                                    //每次从流中读取1KB的数据
                                    readSize = nsServer.Read(msgByte, 0, 40960);
                                    //累计总共流中保存的字节数
                                    totalSize += readSize;
                                    //写入临时流中用于一次性全部读取数据
                                    ms.Write(msgByte, 0, readSize);
                                    Thread.Sleep(50);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("数据发生异常，详细信息：" + ex.ToString());
                                }
                            }
                            byte[] msgByClient = new byte[ms.Length];
                            ms.Position = 0;

                            //将ms临时流中保存的文件以及数据全部读出
                            int readAllSize = ms.Read(msgByClient, 0, (int)msgByClient.Length);
                            Frm_ProductOut_onDownLoadList("收到客户端发送" + totalSize + "字节的数据");
                            //this.cb_fixtureid.Invoke(new EventHandler(delegate
                            //{
                            //    this.cb_fixtureid.Text = ReadDataByClient(msgByClient);
                            //}));
                            // this.tb_fixtureid.Text = ReadDataByClient(msgByClient);
                            ReadDataByClient(msgByClient);
                        }
                        else
                        {
                            try
                            {
                                if (hMsg.Split(':').Length >= 2)
                                {
                                    string filename = hMsg.Split(':')[1].Trim().Replace("\0", "");

                                    if (!Directory.Exists(Application.StartupPath + @"\Download"))
                                    {
                                        Directory.CreateDirectory(Application.StartupPath + @"\Download\");
                                    }
                                    String filePath = Application.StartupPath + @"\Download\" + filename;
                                    //将接收到的byte[]写成文件
                                    try
                                    {
                                        FileStream fs = new FileStream(filePath, FileMode.Create);
                                        while (nsServer.DataAvailable)
                                        {
                                            try
                                            {
                                                Thread.Sleep(50);
                                                byte[] filebyte = new byte[40960];
                                                //每次从流中读取1KB的数据
                                                readSize = nsServer.Read(filebyte, 0, 40960);
                                                //累计总共流中保存的字节数
                                                totalSize += readSize;
                                                //写入临时流中用于一次性全部读取数据
                                                fs.Write(filebyte, 0, readSize);
                                                fs.Flush();
                                                Frm_ProductOut_onDownLoadList1("已接收：" + totalSize + " 字节");
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("数据发生异常，详细信息：" + ex.ToString());
                                            }
                                        }

                                        Frm_ProductOut_onDownLoadList("收到客户端发送" + totalSize + "字节的【" + filename + "】文件");
                                        if (nsServer != null)
                                        {
                                            //获取要发送的数据
                                            String msg = "服务器端已接收【" + filename + "】文件";
                                            //将string转成byte[]
                                            Byte[] data = Encoding.Default.GetBytes(msg);
                                            //向流中写数据发送到客户端
                                            nsServer.Write(data, 0, data.Length);
                                        }
                                        fs.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("写文件发生异常！" + ex.ToString());
                                    }
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
                finally
                {
                    nsServer.Flush();
                }

            }
        }
        #endregion


        #region 读取来自客户端的文件
        /// <summary>
        /// 读取客户端文件数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void ReadFileByClient(byte[] msgBytesByClient, string fileName)
        {
            if (!Directory.Exists(Application.StartupPath + @"\Download"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Download\");
            }
            String filePath = Application.StartupPath + @"\Download\" + fileName;
            //将接收到的byte[]写成文件
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                fs.Write(msgBytesByClient, 0, msgBytesByClient.Length);
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("写文件发生异常！" + ex.ToString());
            }
        }
        #endregion

        #region 读取客户端封装的文件和数据byte[]
        /// <summary>
        /// 读取客户端传送过来的存储着文件和数据byte[]
        /// </summary>
        /// <param name="nsServer"></param>
        public void ReadFileAndDataByClient(NetworkStream nsServer)
        {
            //锁住客户端数据读取
            lock (nsServer)
            {
                try
                {
                    if (nsServer.DataAvailable)
                    {
                        //保存从流中读取的字节数
                        int readSize = 0;
                        //保存从流中读取的总字节数
                        int totalSize = 0;
                        //创建临时保存每次读取数据的流对象
                        MemoryStream ms = new MemoryStream();
                        while (nsServer.DataAvailable)
                        {
                            byte[] msgByte = new byte[1024];
                            //每次从流中读取1KB的数据
                            readSize = nsServer.Read(msgByte, 0, 1024);
                            //累计总共流中保存的字节数
                            totalSize += readSize;
                            //写入临时流中用于一次性全部读取数据
                            ms.Write(msgByte, 0, readSize);
                            lbxServer.Items.Add("已接收" + readSize + "字节的数据");
                        }

                        lbxServer.Items.Add("客户端共发送" + totalSize + "字节的数据");
                        msgBytesByClient = new byte[totalSize];
                        ms.Position = 0;

                        //将ms临时流中保存的文件以及数据全部读出
                        int readAllSize = ms.Read(msgBytesByClient, 0, totalSize);

                        //取出byte[]中的文件数据并生成文件
                        String filePath = Application.StartupPath + @"\zp.wlt";
                        //将接收到的byte[]写成文件
                        try
                        {
                            FileStream fs = new FileStream(filePath, FileMode.Create);
                            fs.Write(msgBytesByClient, 0, 1024);
                            fs.Flush();
                            fs.Close();
                            lbxServer.Items.Add("已接收到1024个字节的文件");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("写文件发生异常！" + ex.ToString());
                        }

                        //取出byte[]中的数据并生成string
                        String msg = Encoding.Default.GetString(msgBytesByClient, 1024, msgBytesByClient.Length - 1024);
                        lbxServer.Items.Add("客户端：" + msg);
                        nsServer.Flush();
                        ms.Dispose();

                        printText = msg;

                        //解压zp.wlt相片文件
                        DesImageFile();
                        //打印客户端数据
                        printMethod();
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
            }
        }
        #endregion

        #region 打印客户端数据
        /// <summary>
        /// 打印客户端数据
        /// </summary>
        public void printMethod()
        {
            string msg = WriteImageFile();

            lock (locker)
            {
                lbxServer.Items.Add("入队列:" + msg);
                clientQueue.Enqueue(msg);		// 入队列
            }

            if (workerThread != null &&
                (workerThread.ThreadState & ThreadState.Suspended)
                != ThreadState.Running)
            {
                workerThread.Resume();		// 唤醒线程
            }
        }

        public String WriteImageFile()
        {
            string filepath = "";
            try
            {
                /**
                 *绘制背景图片
                 * 
                 */
                Image imgBack = Image.FromFile(Application.StartupPath + @"\IDCard.bmp");
                //create a Bitmap the Size of the original photograph
                Bitmap bmBack = new Bitmap(imgBack.Width, imgBack.Height);
                Graphics e = Graphics.FromImage(bmBack);
                //既然使用位图，就需要先画出图片，在画字
                e.DrawImage(imgBack, new Rectangle(0, 0, imgBack.Width, imgBack.Height));

                /**
                 * 
                 * 绘制头像图片
                 * */
                Image imgPhoto = Image.FromFile(Application.StartupPath + @"\zp.bmp");
                Bitmap bmPhoto = new Bitmap(imgPhoto);
                bmPhoto.MakeTransparent(bmPhoto.GetPixel(20, 20));

                //叠加
                e.DrawImage(bmPhoto, new Rectangle(200, 32, imgPhoto.Width, imgPhoto.Height));  //Set the detination Position
                String[] idCardInfo = printText.Split('|');
                idcard = idCardInfo[5];
                if (idCardInfo != null && idCardInfo.Length > 0)
                {
                    //姓名
                    e.DrawString(idCardInfo[0], new Font("宋体", 12), new System.Drawing.SolidBrush(Color.Black), 60, 32);
                    //性别
                    e.DrawString(idCardInfo[1], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 55);
                    //民族
                    e.DrawString(idCardInfo[2], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 130, 55);
                    try
                    {
                        //出生日期 
                        String year = idCardInfo[3].Substring(0, 4);
                        String month = idCardInfo[3].Substring(5, 2);
                        String date = idCardInfo[3].Substring(8, 2);
                        e.DrawString(year, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 80);
                        e.DrawString(month, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 110, 80);
                        e.DrawString(date, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 140, 80);
                    }
                    catch
                    {
                    }
                    //住址
                    if (idCardInfo[4].Length > 11)
                    {
                        int rows = 0;
                        if (idCardInfo[4].Length % 11 == 0)
                        {
                            rows = idCardInfo[4].Length / 11;
                        }
                        else
                        {
                            rows = idCardInfo[4].Length / 11 + 1;
                        }
                        if (rows == 2)
                        {
                            String startStr = idCardInfo[4].Substring(0, 11);
                            String endStr = idCardInfo[4].Substring(11, idCardInfo[4].Length - 11);
                            e.DrawString(startStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 108);
                            e.DrawString(endStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 125);
                        }
                        if (rows == 3)
                        {
                            String startStr = idCardInfo[4].Substring(0, 11);
                            String middleStr = idCardInfo[4].Substring(11, 11);
                            String endStr = idCardInfo[4].Substring(22, idCardInfo[4].Length - 22);
                            e.DrawString(startStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 108);
                            e.DrawString(middleStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 125);
                            e.DrawString(endStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 142);
                        }
                    }
                    else
                    {
                        e.DrawString(idCardInfo[4], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 108);
                    }
                    //身份号码
                    e.DrawString(idCardInfo[5], new Font("宋体", 12, FontStyle.Bold), new System.Drawing.SolidBrush(Color.Black), 106, 170);
                    //签发机关
                    e.DrawString(idCardInfo[6], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 130, 364);
                    //有效期限
                    e.DrawString(idCardInfo[7], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 130, 389);
                }
                filepath = Application.StartupPath + @"\" + idcard + ".bmp";
                bmBack.Save(filepath, ImageFormat.Bmp);
                e.Dispose();
                bmBack.Dispose();
                imgBack.Dispose();
                return filepath;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return filepath;
        }

        void pdoc_BeginPrint(object sender, PrintEventArgs e)
        {
            EventHandler del = delegate
            {
                lbxServer.Items.Add(String.Format("{0} 开始打印...{1}", DateTime.Now, Environment.NewLine));
            };

            lbxServer.Invoke(del);
        }

        void pdoc_EndPrint(object sender, PrintEventArgs e)
        {
            EventHandler del = delegate
            {
                lbxServer.Items.Add(String.Format("{0} 打印结束...{1}{1}", DateTime.Now, Environment.NewLine));
            };

            lbxServer.Invoke(del);
        }

        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // 这里的值不能是txtMessage.Text，而应该是由队列中取出的
            // 因为要打印时，txtMessgae的Text已经改变了，txtMessage和printer不同步
            string msg;
            lock (locker)
            {
                msg = clientQueue.Dequeue();	// 出队列
            }
            e.Graphics.DrawImage(Image.FromFile(msg), new Point(100, 100));
            lbxServer.Items.Add(msg);
        }
        #endregion

        #region 将zp.wlt文件解密成照片
        /// <summary>
        /// 解密zp.wlt相片文件
        /// </summary>
        /// <returns>解密结果</returns>
        public void DesImageFile()
        {
            try
            {
                String filePath = Application.StartupPath + @"\zp.wlt";
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("zp.wlt文件不存在！");
                    return;
                }
                int returnValue = GetBmp(filePath, 1);
                String showMsg = "";
                switch (returnValue)
                {
                    case 0: showMsg = "调用sdtapi.dll错误！"; break;
                    case -1: showMsg = "照片解密错误！"; break;
                    case -2: showMsg = "wlt文件后缀错误！"; break;
                    case -3: showMsg = "wlt文件打开错误！"; break;
                    case -4: showMsg = "wlt文件格式错误！"; break;
                    case -5: showMsg = "软件未授权！"; break;
                    case -6: showMsg = "设备连接错误！"; break;
                    default: showMsg = ""; break;
                }
                if (showMsg != "")
                {
                    MessageBox.Show(showMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 向客户端发送数据
        private void ServerSendToClient(string SendMsg)
        {
            try
            {
                if (nsServer != null)
                {
                    //获取要发送的数据
                    String msg = SendMsg;
                    //将string转成byte[]
                    Byte[] data = Encoding.Default.GetBytes(msg);
                    //向流中写数据发送到客户端
                    nsServer.Write(data, 0, data.Length);
                    //发送数据
                    nsServer.Flush();
                    Frm_ProductOut_onDownLoadList(data.Length + "字节的数据已成功发送！");
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("向客户端发送数据失败，详细信息：" + ex.ToString());
            }
        }
        #endregion


        #region 解密相片文件
        /// <summary>
        /// 解密文件的SDK方法
        /// </summary>
        /// <param name="file_name"></param>
        /// <param name="intf"></param>
        /// <returns></returns>
        [DllImport("WltRS.dll")]
        public static extern int GetBmp(string file_name, int intf);
        #endregion


        //委托
        public delegate void dDownloadList(string msg);
        //事件
        public event dDownloadList onDownLoadList;

        public void Frm_ProductOut_onDownLoadList(string msg)
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new Frm_ProductOut.dDownloadList(Frm_ProductOut_onDownLoadList), new object[] { msg });
            }
            else
            {
                lbxServer.Items.Add(msg);
                Application.DoEvents();
            }
        }

        //委托
        public delegate void dDownloadList1(string msg);
        //事件
        public event dDownloadList1 onDownLoadList1;

        public void Frm_ProductOut_onDownLoadList1(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Frm_ProductOut.dDownloadList1(Frm_ProductOut_onDownLoadList1), new object[] { msg });
            }
            else
            {
                //lblFileSend.Text = msg;
                Application.DoEvents();
            }
        }
        public void dispose()
        {
            if (nsClient != null)
                nsClient.Close();
            if (client != null)
                client.Close();
        }
        /// <summary>
        /// 将listview的内容写入txt文本。
        /// </summary>
        private void ListViewToTxt()
        {
            if (lbxServer.Items.Count < 1)
                return;
            try
            {
                string str = string.Empty;
                long cols = lbxServer.Items.Count;
                foreach (ListViewItem lvi in lbxServer.Items)
                {
                    for (long i = 0; i < cols; i++)
                    {
                        str += lvi.SubItems[(Int32)i].Text + "\r\n";
                    }
                }
                FileStream fs = new FileStream(string.Format("{0}Socket.txt",
                System.AppDomain.CurrentDomain.BaseDirectory), FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(str);
                sw.Close();
                fs.Close();
                lbxServer.Items.Clear();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        #endregion

        #region
        private void cb_outputtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearInfo();
            //string status = this.cb_outputtype.SelectedValue.ToString();
            string status = dstatus[this.cb_outputtype.Text];
            string key = this.cb_outputtype.Text.ToString();
            this.label11.Visible = false;
            this.cb_woid.Visible = false;
            this.lb_outqty.Text = "0";
            this.listpart.Items.Clear();


            if (status == "6" || status == "7")
            {
                this.label2.Text = "SAP出库单号";
                this.tb_sapcode.Enabled = true;
                this.tb_partnumber.Enabled = false;
                this.tb_qty.Enabled = false;
                this.tb_customerid.Enabled = false;
                this.bt_customer.Enabled = false;

            }
            else
            {
                if (key.Trim() == "--选择--")
                {
                    this.label2.Text = "出库单号";
                    this.tb_sapcode.Text = "";
                }
                else
                {
                    Random rad = new Random();
                    this.lblSAP.Text = key.Trim() + "出库单号";
                    this.tb_sapcode.Text = status + string.Format("{0:yyMMdd}", DateTime.Now) + rad.Next(1000, 10000).ToString();
                    if (status == "9")
                    {
                        this.label11.Visible = true;
                        this.cb_woid.Visible = true;
                    }
                }
                this.tb_sapcode.Enabled = false;

            }
        }
        public string mFlag = "0";
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    mFlag = "0";
                    break;
                case 1:
                    mFlag = "1";
                    bt_adddata.Visible = false;
                    label12.Visible = false;
                    tb_serialend.Visible = false;
                    break;
                default:
                    mFlag = "0";
                    break;
            }
        }

        public void ClearInfo()
        {
            this.tb_sapcode.Text = "";
            this.cb_outputstyle.SelectedIndex = -1;
            this.tb_serialval.Text = "";
            this.tb_partnumber.Text = "";
            this.tb_qty.Text = "";
            this.tb_customerid.Text = "";
            this.tb_sapcode.Enabled = true;
            this.tb_partnumber.Enabled = true;
            this.tb_qty.Enabled = true;
            this.tb_customerid.Enabled = true;
            this.bt_customer.Enabled = true;
        }
        private void tb_sapcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_sapcode.Text.Trim()) || e.KeyValue != 13)
                return;
            if (iasyncresult_1 != null && !iasyncresult_1.IsCompleted)
            {
                ShowMsg(mLogMsgType.Error, "还有任务没有完成,请稍后..");
                return;
            }
            bool _usesapdata = false;
            if (MessageBoxEx.Show("是否需要重新从SAP导入出货信息?\n重新导入请选择[Yes],否则请选择[NO]",
                "提示", MessageBoxButtons.YesNo,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _usesapdata = true;
            }
            eventloadsapinfo_1 = new delegatesapinfo(this.LoadSAPInfo_1);
            iasyncresult_1 = eventloadsapinfo_1.BeginInvoke(this.tb_sapcode.Text.Trim(), _usesapdata, null, null);
            this.tb_sapcode.Enabled = true;

            ShowOutQTY();
        }
        DataTable dtsapdata = null;
        List<string> lspartnumber = new List<string>();
        /// <summary>
        /// 加载SAP出货订单信息
        /// </summary>
        /// <param name="saplotcode">STO&SO</param>
        /// <param name="flag">true-从SAP拉取；false-从SFIS拉取</param>
        private void LoadSAPInfo_1(string saplotcode, bool flag)
        {
            if (flag)
            {
                ShowMsg(mLogMsgType.Incoming, "正在从SAP拉取数据...");
                DataTable mdtable = dtsapdata = FrmBLL.ReleaseData.arrByteToDataTable(refWebSapConnector.Instance.Get_Z_RFC_LIPS(saplotcode, ""));
                if (mdtable == null || mdtable.Rows.Count < 1)
                {
                    ShowMsg(mLogMsgType.Error, "该SAP出货单号无数据,请确认...");
                    return;
                }
                FillDataGridView_1(mdtable);
                #region 向SFIS系统填充出货信息
                List<WebServices.tWarehouseWipTracking.tSapLodeInfo> lssap = new List<WebServices.tWarehouseWipTracking.tSapLodeInfo>();

                foreach (DataRow dr in mdtable.Rows)
                {
                    string SerialCode = "CU" + Common.RandomTimeSerial(DateTime.Now, 3);
                    lssap.Add(new WebServices.tWarehouseWipTracking.tSapLodeInfo()
                    {
                        ContactPerson = dr["KUNNR"].ToString(),
                        CustomerName = dr["NAME1"].ToString(),
                        CustomerId = SerialCode,
                        Partnumber = dr["MATNR"].ToString(),
                        ProductDesc = dr["MAKTX"].ToString(),
                        SAPCode = dr["VBELN"].ToString(),
                        QTY = Convert.ToInt32(dr["LFIMG"].ToString().Split('.')[0]),
                        SapWarehouse = dr["WERKS"].ToString(),
                        UserId = mFrm.gUserInfo.userId,
                        SFCcode = "NA",
                        Contractno = dr["BSTKD"].ToString(),
                        Customername2 = dr["NAME1_AG"].ToString(),
                        Address = string.IsNullOrEmpty(tb_address.Text) ? "NA" : tb_address.Text.Trim()                         
                    });

                }
                ShowMsg(mLogMsgType.Incoming, "保存出库订单信息到系统...");
                string msg = refWebtWarehouseWipTracking.Instance.InsertOutPutRecordList(lssap.ToArray());
                if (msg!="OK")
                {
                    MessageBox.Show("保存出货信息失败:"+msg);
                    this.mFrm.ShowPrgMsg(msg, MainParent.MsgType.Error);
                    return;
                }
                ShowMsg(mLogMsgType.Outgoing, "保存出库订单信息成功!");
                //dtsapdata = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetSAPInfo(saplotcode));
                #endregion


            }
            else
            {
                DataTable dtout = new DataTable();
                dtout = dtsapdata = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetSAPInfo(saplotcode));
                if (dtout == null || dtout.Rows.Count < 1)
                {
                    ShowMsg(mLogMsgType.Error, "SFIS系统中该出货单号无数据,请确认...");
                    return;
                }
                FillDataGridView_1(dtout);
                ShowMsg(mLogMsgType.Outgoing, "读取出库订单信息成功!");
            }
            lspartnumber.Clear();
            foreach (DataRow drp in dtsapdata.Rows)
            {
                if (!lspartnumber.Contains(drp["MATNR"].ToString()))
                lspartnumber.Add(drp["MATNR"].ToString());
            }
        }

        private void FillDataGridView_1(DataTable dt)
        {
            if (dt == null)
                return;
            this.dgv_sapdata.Invoke(new EventHandler(delegate
            {
                this.dgv_sapdata.DataSource = dt.DefaultView;
                this.dgv_sapdata.Refresh();
            }));
        }

        private void bt_customer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cb_outputtype.Text.Trim()))
                return;
            if (dstatus[cb_outputtype.Text] == "11" || dstatus[cb_outputtype.Text] == "9")  // update 2013/08/31  
            {
                SelectData dz = new SelectData(this, FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetWarehouseList()));
                dz.ShowDialog();
            }
            else
            {
                fCustomerInfo dz = new fCustomerInfo(mFrm, this);

                dz.ShowDialog();
            }
        }
        string outStyleFlag = "0";
        List<WebServices.tWarehouseWipTracking.tWarehouseWipTrackingTable> lsproductout = new List<WebServices.tWarehouseWipTracking.tWarehouseWipTrackingTable>();
        private void tb_serialval_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_serialval.Text) && e.KeyValue == 13)
            {
                try
                {
                    string errorMsg = ValidateControl();
                    if (!string.IsNullOrEmpty(errorMsg))
                        throw new Exception(errorMsg);
                    DataTable dt = null;
                    string snval = this.tb_serialval.Text.Trim();

                    #region 针对加载的序列号
                    if (_addserialval)
                    {
                        lsproductout.Clear();
                        foreach (string sn in SNList)
                        {
                            #region 判断条件
                            dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductInfoBySN(sn, tb_serialend.Text.Trim(), "0"));
                            if (dt == null || dt.Rows.Count < 1)
                            {
                                this.ShowMsg(mLogMsgType.Error, string.Format("[{0}]数据不存在，请确认...", sn));
                                continue;
                            }
                            if (dt.Rows.Count > 1)
                            {
                                this.ShowMsg(mLogMsgType.Warning, string.Format("[{0}]存在[{1}]笔数据...", sn, dt.Rows.Count));
                                continue;
                            }
                            string ccstatus = dt.Rows[0]["status"].ToString();
                            if (ccstatus != "1" && ccstatus != "2" && ccstatus != "3")
                            {
                                this.ShowMsg(mLogMsgType.Warning, string.Format("[{0}]不在仓库[{1}]...(6:售出 7:借出 8:领用 9:重工 10:赠品 11:调拨)", sn, ccstatus));
                                continue;
                            }
                            string ccpartnumber =sPartNumber= dt.Rows[0]["partnumber"].ToString();                       
                            if (!lspartnumber.Contains(ccpartnumber))
                            {
                                this.ShowMsg(mLogMsgType.Error, string.Format("[{0}]的料号为[{1}]，不在出库料号中,请确认...", sn, ccpartnumber));
                                continue;
                            }
                            #endregion
                            lsproductout.Add(new WebServices.tWarehouseWipTracking.tWarehouseWipTrackingTable()
                            {
                                Esn = dt.Rows[0]["esn"].ToString(),
                                SapCode = this.tb_sapcode.Text.Trim(),
                                PartNumber = dt.Rows[0]["partnumber"].ToString(),
                                CustomerId = this.tb_customerid.Text.Trim(),
                                userid = mFrm.gUserInfo.userId,
                                Status = dstatus[cb_outputtype.Text],
                                mFlag = this.tb_qty.Text.Trim(), //存放数量
                                Address = string.IsNullOrEmpty(tb_address.Text) ? "NA" : tb_address.Text.Trim()
                            });
                        }
                        _addserialval = false;
                    }
                    #endregion
                    else
                    {
                        dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductInfoBySN(snval, tb_serialend.Text.Trim(), outStyleFlag));
                        if (dt == null || dt.Rows.Count < 1)
                            throw new Exception("数据不存在，请确认...");

                        lsproductout.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            string aastatus = dr["status"].ToString();
                            if (aastatus != "1" && aastatus != "2" && aastatus != "3")
                            {
                                this.ShowMsg(mLogMsgType.Warning, string.Format("esn:[{0}]状态[{1}]...(6:售出 7:借出 8:领用 9:重工 10:赠品 11:调拨)",
                                    dr["esn"].ToString(), aastatus));
                                continue;
                            }
                            string ccpartnumber =sPartNumber= dt.Rows[0]["partnumber"].ToString();
                            if (!lspartnumber.Contains(ccpartnumber))
                            {
                                this.ShowMsg(mLogMsgType.Warning, string.Format("esn:[{0}]的料号为[{1}]，不在出库料号中,请确认...",
                                    dr["esn"].ToString(), ccpartnumber));
                                continue;
                            }

                            lsproductout.Add(new WebServices.tWarehouseWipTracking.tWarehouseWipTrackingTable()
                            {
                                Esn = dr["esn"].ToString(),
                                SapCode = this.tb_sapcode.Text.Trim(),
                                PartNumber = dt.Rows[0]["partnumber"].ToString(),
                                CustomerId = this.tb_customerid.Text.Trim(),
                                userid = mFrm.gUserInfo.userId,
                                Status = dstatus[cb_outputtype.Text],
                                mFlag = this.tb_qty.Text.Trim(), //存放出库总数量
                                Address = string.IsNullOrEmpty(tb_address.Text) ? "NA" : tb_address.Text.Trim()
                            });
                        }
                    }
                    CheckOutQty();
                    foreach (var item in dic)
                    {
                        totaloutqty_1 = dicallout[item.Key];//获取该料号总出库数量
                        int itemoutqty = 0;
                        if (dicout.ContainsKey(item.Key))
                            itemoutqty = dicout[item.Key];
                        totalpickqty_1 = item.Value + itemoutqty;//lsproductout.Count + Convert.ToInt32(this.lb_outqty.Text.Trim());
                        if (totalpickqty_1 > totaloutqty_1)
                            throw new Exception(string.Format("错误:待出库数量【{0}】大于出库数量【{1}】", totalpickqty_1, totaloutqty_1));

                    }

                    string Err = "";
                    Err = refWebtWarehouseWipTracking.Instance.ProductOut_1(lsproductout.ToArray());
                    if (Err != "OK")
                        throw new Exception(Err);


                    if (dstatus[cb_outputtype.Text] == "9")//重工
                    {
                        List<string> lsesn = new List<string>();
                        foreach (WebServices.tWarehouseWipTracking.tWarehouseWipTrackingTable item in lsproductout)
                        {
                            lsesn.Add(item.Esn);
                        }

                        UpLoadSAP(sReworkWO, sPartNumber, sProductName, lsesn.Count, tb_sapcode.Text.Trim().ToUpper());
                        //string Err1 = "";
                        //Err1 = refWebtReworkDetailInfo.Instance.InsertReworkTempInfo(this.cb_woid.Text.Trim(), lsesn.ToArray());
                        //if (Err1 != "OK")
                        //    throw new Exception(Err1);
                    }
                    if (lsproductout.Count > 0)
                        ShowMsg(mLogMsgType.Normal, string.Format("[{1}] 成功出库{0}PCS!", lsproductout.Count, snval));
                    this.tb_serialval.SelectAll();
                    ShowOutQTY();

                }
                catch (Exception ex)
                {
                    this.tb_serialval.Focus();
                    this.tb_serialval.SelectAll();
                    this.ShowMsg(mLogMsgType.Error, ex.Message);
                }
            }
        }
        /// <summary>
        /// 存放料号的待出库数量
        /// </summary>
        Dictionary<string, int> dic = new Dictionary<string, int>();
        /// <summary>
        /// 存放料号的已出库数量
        /// </summary>
        Dictionary<string, int> dicout = new Dictionary<string, int>();
        /// <summary>
        /// 存放料号总出库数量
        /// </summary>
        Dictionary<string, int> dicallout = new Dictionary<string, int>();
        private void CheckOutQty()
        {
            dic.Clear();
            foreach (string ipart in lspartnumber)
            {
                int icount = 0;
                //WebServices.tWarehouseWipTracking.tWarehouseWipTrackingTable
                foreach (var ioutqty in lsproductout)
                {
                    if (ioutqty.PartNumber == ipart)
                    {
                        icount++;
                    }
                }
                if (dic.ContainsKey(ipart))
                    dic[ipart] += icount;
                else
                    dic.Add(ipart, icount);


            }
        }
        /// <summary>
        /// 显示出库数量
        /// </summary>
        private void ShowOutQTY()
        {
            if (!string.IsNullOrEmpty(this.tb_sapcode.Text.Trim()))
            {
                DataTable dtqty;
                this.lb_outqty.Text = "0";
                this.listpart.Items.Clear();
                int aaqty = 0;
                dicout.Clear();
                dtqty = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.CountProductOutQTY(this.tb_sapcode.Text.Trim()));
                if (dtqty == null || dtqty.Rows.Count < 1)
                {
                    this.ShowMsg(mLogMsgType.Warning, "该单号没有出库产品...");
                    return;
                }
                for (int i = 0; i < dtqty.Rows.Count; i++)
                {
                    listpart.Items.Add(dtqty.Rows[i]["partnumber"].ToString() + ":" + dtqty.Rows[i]["qty"].ToString());
                    dicout.Add(dtqty.Rows[i]["partnumber"].ToString(), Convert.ToInt32(dtqty.Rows[i]["qty"].ToString()));
                    aaqty += Convert.ToInt32(dtqty.Rows[i]["qty"].ToString());
                }
                this.lb_outqty.Text = aaqty.ToString();

            }
        }


        /// <summary>
        /// 初始化控件值
        /// </summary>
        private string ValidateControl()
        {
            string msg = "";

            dicallout.Clear();
            if (dstatus[cb_outputtype.Text] == "6" || dstatus[cb_outputtype.Text] == "7")
            {
                if (string.IsNullOrEmpty(cb_outputtype.Text))
                {
                    msg += "请选择出库类型...";
                }
                if (string.IsNullOrEmpty(tb_sapcode.Text) || dgv_sapdata.Rows.Count < 1)
                {
                    msg += "请确认SAP出库单号是否正确或是否有出库数据...";
                }
                if (string.IsNullOrEmpty(cb_outputstyle.Text))
                {
                    msg += "请选择出库方式...";
                }
                foreach (DataRow dr in dtsapdata.Rows)
                {

                    if (dicallout.ContainsKey(dr["MATNR"].ToString()))
                    {
                        dicallout[dr["MATNR"].ToString()] = dicallout[dr["MATNR"].ToString()] + Convert.ToInt32(dr["LFIMG"].ToString().Split('.')[0]);
                    }
                    else
                    {
                        dicallout.Add(dr["MATNR"].ToString(), Convert.ToInt32(dr["LFIMG"].ToString().Split('.')[0]));
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(cb_outputtype.Text))
                {
                    msg += "请选择出库类型...";
                }
                if (string.IsNullOrEmpty(tb_sapcode.Text))
                {
                    msg += "请确认SAP出库单号是否为空...";
                }
                if (string.IsNullOrEmpty(cb_outputstyle.Text))
                {
                    msg += "请选择出库方式...";
                }
                if (string.IsNullOrEmpty(tb_partnumber.Text.Trim()))
                {
                    msg += "请输入料号...";
                }
                if (string.IsNullOrEmpty(tb_qty.Text.Trim()))
                {
                    msg += "请输入数量...";
                }
                if (string.IsNullOrEmpty(tb_customerid.Text.Trim()))
                {
                    msg += "请选择客户编号...";
                }
                if (dicallout.ContainsKey(tb_partnumber.Text.Trim()))
                    dicallout[tb_partnumber.Text.Trim()] += Convert.ToInt32(string.IsNullOrEmpty(tb_qty.Text) ? "0" : tb_qty.Text.Trim());
                else
                    dicallout.Add(tb_partnumber.Text.Trim(), Convert.ToInt32(string.IsNullOrEmpty(tb_qty.Text)?"0":tb_qty.Text.Trim()));
                //dicallout.Add(tb_partnumber.Text.Trim(), Convert.ToInt32(this.tb_qty.Text.Trim()));
            }
            return msg;
        }
        /// <summary>
        /// 显示消息函数
        /// </summary>
        /// <param name="msgtype"></param>
        /// <param name="msg"></param>
        public void ShowMsg(mLogMsgType msgtype, string msg)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    //rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = mLogMsgTypeColor[(int)msgtype];
                    rtbmsg.AppendText(msg + "\n");
                    rtbmsg.ScrollToCaret();
                }));
            }
            catch
            {
            }
        }
        private void cb_outputstyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cb_outputstyle.SelectedIndex)
            {
                case 0:
                    bt_adddata.Visible = true;
                    label12.Visible = false;
                    tb_serialend.Visible = false;
                    outStyleFlag = "0";
                    break;
                case 1:
                    bt_adddata.Visible = false;
                    label12.Visible = true;
                    tb_serialend.Visible = true;
                    outStyleFlag = "1";
                    break;
                case 2:
                    bt_adddata.Visible = false;
                    label12.Visible = false;
                    tb_serialend.Visible = false;
                    outStyleFlag = "2";
                    break;
                case 3:
                    bt_adddata.Visible = false;
                    label12.Visible = false;
                    tb_serialend.Visible = false;
                    outStyleFlag = "3";
                    break;
                case 4:
                    bt_adddata.Visible = false;
                    label12.Visible = false;
                    tb_serialend.Visible = false;
                    outStyleFlag = "4";
                    break;
                case 5:
                    bt_adddata.Visible = false;
                    label12.Visible = false;
                    tb_serialend.Visible = false;
                    outStyleFlag = "5";
                    break;
                case 6:
                    bt_adddata.Visible = true;
                    label12.Visible = false;
                    tb_serialend.Visible = false;
                    outStyleFlag = "6";
                    break;
                case 7:
                    bt_adddata.Visible = true;
                    label12.Visible = false;
                    tb_serialend.Visible = false;
                    outStyleFlag = "7";
                    break;
                case 8:
                    bt_adddata.Visible = true;
                    label12.Visible = false;
                    tb_serialend.Visible = false;
                    outStyleFlag = "8";
                    break;
                default:
                    bt_adddata.Visible = false;
                    label12.Visible = false;
                    tb_serialend.Visible = false;
                    break;
            }
        }
        List<string> SNList = new List<string>();
        bool _addserialval = false;
        private void bt_adddata_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "导入出库序列号";
                ofd.Filter = "(*.txt 文本文件)|*.txt";
                ofd.Multiselect = true;
                DialogResult dlr = ofd.ShowDialog();
                DataTable dt = new DataTable();
                if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
                {
                    SNList.Clear();
                    SNList = ReadTxt(ofd.FileName);
                    _addserialval = true;
                    this.tb_serialval.Text = str;
                    this.tb_serialval.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowMsg(mLogMsgType.Error, ex.Message);
                return;
            }
        }
        string str = "";
        public List<String> ReadTxt(string filePathName)
        {
            List<String> ls = new List<String>();
            StreamReader fileReader = new StreamReader(filePathName);
            string strLine = "";
            while (strLine != null)
            {
                strLine = fileReader.ReadLine();
                if (strLine != null && strLine.Length > 0)
                {
                    strLine.Trim();
                    ls.Add(strLine);
                    str = strLine;
                    this.ShowMsg(mLogMsgType.Normal, string.Format("加载序列[{0}]", strLine));
                }
            }
            fileReader.Close();
            return ls;
        }


        private void cb_woid_DropDown(object sender, EventArgs e)
        {
            try
            {
                this.cb_woid.Items.Clear();
                this.cb_woid.Items.AddRange(refWebtWoInfo.Instance.GetWoListByWoType());
                this.cb_woid.Refresh();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void cb_woid_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cb_woid.Text.Trim()))
            {
                //if (!refWebtWoInfo.Instance.CheckWoISavailability(this.cb_woid.Text.Trim()))
                //{
                //    this.ShowMsg(mLogMsgType.Error, "该重工工单不存在,请确认...");
                //    this.cb_woid.Focus();
                //    this.cb_woid.Text = "";
                //    return;
                //}
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfoByWo(this.cb_woid.Text.Trim()));
                if (dt == null || dt.Rows.Count < 1)
                {
                    this.ShowMsg(mLogMsgType.Error, "该重工工单不存在,请确认...");
                    this.cb_woid.Focus();
                    this.cb_woid.Text = "";
                    return;
                }
                else
                {
                    this.sReworkWO = dt.Rows[0]["woId"].ToString();
                  //  this.sPartNumber = dt.Rows[0]["PARTNUMBER"].ToString();
                    this.sProductName = dt.Rows[0]["PRODUCTNAME"].ToString();
                    ShowMsg(mLogMsgType.Normal, string.Format("工单确认完成...料号:[{0}],型号:[{1}]",dt.Rows[0]["PARTNUMBER"].ToString(),ProductName));
                }
            }
        }

        private void tb_partnumber_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_partnumber.Text.Trim()))
            {
                this.ShowMsg(mLogMsgType.Warning, "料号为空...");
                tb_partnumber.Focus();
                return;
            }
            string outsstatus = dstatus[string.IsNullOrEmpty(this.cb_outputtype.Text) ? "----" : this.cb_outputtype.Text];
            if (outsstatus != "6" && outsstatus != "7")
            {
                lspartnumber.Clear();
                lspartnumber.Add(tb_partnumber.Text.Trim());
            }
        }

        private void tb_serialend_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_serialend.Text.Trim()) && e.KeyValue == 13)
            {
                tb_serialval_KeyDown(null, e);
            }
        }

        #endregion

        private void bt_submit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_sapcode.Text.Trim()))
            {
                string Err = string.Empty;
                if (MessageBox.Show(string.Format("确定[{0}]该单号已完成出库?", tb_sapcode.Text), "消息提示框", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Err = refWebtWarehouseWipTracking.Instance.UpdatetOutputlotrecordNew(this.tb_sapcode.Text.Trim());
                    if (Err!="OK")
                    {
                        this.ShowMsg(mLogMsgType.Error, Err);
                    }
                    else
                    {
                        UpLoadSale();
                    }
                }
            }
        }

        private void UpLoadSAP(string woId, string PartNo, string Product, int QTY, string LOTNO)
        {
            try
            {
                string[] LsMsg = RefWebService_BLL.refWebSapConnector.Instance.Get_Z_RFC_AUFNR_MIGO_OUT(new WebServices.SapConnector.Z_RFC_AUFNR_MIGO()
                {
                     woId=woId,
                     PartNumber=PartNo,
                      QTY=QTY,
                       EMP_NAME=mFrm.gUserInfo.username,
                        EMP_NO=mFrm.gUserInfo.userId
                });
                    
                   // "03", null, null, "2", woId, PartNo, QTY, "0001", "2100", "261");
                if (LsMsg.Length < 5)
                {
                    RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.InsertZ_Whs_SAP_BackFlush(new WebServices.tZ_Whs_SAP_BackFlush.Z_WHS_SAP_BACKFLUSHTable()
                    {
                        LOTOUT = LOTNO,
                        WOID = woId,
                        PARTNUMBER = PartNo,
                        PRODUCTNAME = Product,
                        LOTOUT_QTY = QTY,
                        MOVE_TYPE = "261",
                        PLANT = "2100",
                        UPLOAD_FLAG = "N"
                    });
                    ShowMsg(mLogMsgType.Error, LsMsg[0].ToString());
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
                        string InserRes = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.InsertZ_Whs_SAP_BackFlush(new WebServices.tZ_Whs_SAP_BackFlush.Z_WHS_SAP_BACKFLUSHTable()
                        {
                            LOTOUT = LOTNO,
                            LOTOUT_QTY = QTY,
                            WOID = woId,
                            PARTNUMBER = PartNo,
                            PRODUCTNAME = Product,
                            MOVE_TYPE = "261",
                            PLANT = "2100",
                            UPLOAD_FLAG = "Y"

                        });
                        ShowMsg(mLogMsgType.Normal, string.Format("上传SAP成功,SAP单号[{0}],工单[{1}],料号[{2}],产品型号[{3}],数量[{4}]->" + InserRes, SAP_STOCKNO, woId, PartNo, Product, QTY.ToString()));
                    }
                    else
                    {
                        string SFCMSG = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.InsertZ_Whs_SAP_BackFlush(new WebServices.tZ_Whs_SAP_BackFlush.Z_WHS_SAP_BACKFLUSHTable()
                        {
                            LOTOUT = LOTNO,
                            WOID = woId,
                            PARTNUMBER = PartNo,
                            PRODUCTNAME = Product,
                            LOTOUT_QTY = QTY,
                            MOVE_TYPE = "261",
                            PLANT = "2100",
                            UPLOAD_FLAG = "N"
                        });
                        ShowMsg(SFCMSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, "Insert SFC " + SFCMSG);
                        string sSAP_MSG = string.Format("入库编号:{0},工单:{1},料号:{2},型号:{3},数量:{4},MSG:{5}", LOTNO, woId, PartNo, Product, QTY.ToString(), SAP_TYPE + "-" + SAP_E_ID + "-" + SAP_E_NUM + "-" + SAP_MSG);
                        ShowMsg(SAP_MSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, "SAP:" + sSAP_MSG);

                        SFCMSG = RefWebService_BLL.refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG()
                        {
                            userId = "UPLOAD",
                            prg_name = "UPWHS",
                            action_type = woId,
                            action_desc = sSAP_MSG
                        });
                        ShowMsg(SFCMSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error, "SFC Log:" + SFCMSG);
                    }

                }
            }
            catch (Exception ex)
            {
                string SFCMSG = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.InsertZ_Whs_SAP_BackFlush(new WebServices.tZ_Whs_SAP_BackFlush.Z_WHS_SAP_BACKFLUSHTable()
                {
                    LOTOUT = LOTNO,
                    WOID = woId,
                    PARTNUMBER = PartNo,
                    PRODUCTNAME = Product,
                    LOTOUT_QTY = QTY,
                    MOVE_TYPE = "261",
                    PLANT = "2100",
                    UPLOAD_FLAG = "N"
                });
                ShowMsg(mLogMsgType.Error, "上传SAP异常:" + ex.Message);
            }
        }

        #region 售出上抛
        private void UpLoadSale()
        {

            ShowMsg( mLogMsgType.Normal,"开始上传SAP.....");
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.GetUpload_whs_Sap_Info("601"));
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    UpLoadSAP_Sale(dr["LOTOUT"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(), Convert.ToInt32(dr["LOTOUT_QTY"].ToString()), dr["ROWID"].ToString());
                }
            }
            ShowMsg(mLogMsgType.Normal,"上传SAP结束.....");
           
        }
        private void UpLoadSAP_Sale(string SAP_LOT, string PartNo, string Product, int QTY, string sRowid)
        {
            string[] LsMsg = RefWebService_BLL.refWebSapConnector.Instance.Get_Z_RFC_GD_DELIVERY(SAP_LOT);
            if (LsMsg.Length < 4)
            {
                ShowMsg(mLogMsgType.Error,LsMsg[0].ToString());
            }
            else
            {
                string SAP_TYPE = LsMsg[1].ToString();
                string SAP_E_ID = LsMsg[2].ToString();
                string SAP_E_NUM = LsMsg[3].ToString();
                string SAP_MSG = LsMsg[4].ToString();
                if (SAP_TYPE.ToUpper() == "S")
                {
                    string sRes = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.Update_Z_Whs_SAP_BackFlush_Flag(sRowid, "Y");
                    ShowMsg(mLogMsgType.Normal,string.Format("上传SAP成功,SAP出货单[{0}],料号[{1}],产品型号[{2}],数量[{3}],时间[{4}]", SAP_LOT, PartNo, Product, QTY.ToString(), DateTime.Now.ToString()));
                }
                else
                {

                    string sSAP_MSG = string.Format("出货单:{0},料号:{1},型号:{2},数量:{3},MSG:{4},时间[{5}]", SAP_LOT, PartNo, Product, QTY.ToString(), SAP_TYPE + "-" + SAP_E_ID + "-" + SAP_E_NUM + "-" + SAP_MSG, DateTime.Now.ToString());
                    ShowMsg(SAP_MSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error,"SAP:" + sSAP_MSG);

                    string SFCMSG = RefWebService_BLL.refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG()
                    {
                        userId = "UPLOAD",
                        prg_name = "SAP",
                        action_type = SAP_LOT,
                        action_desc = sSAP_MSG
                    });
                    ShowMsg(SFCMSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error,"SFC Log:" + SFCMSG);
                }

            }
        }
        #endregion

    }

}
