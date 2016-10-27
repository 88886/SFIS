using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LabelManager2;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.Runtime.InteropServices;

namespace SFIS_V2
{
    public partial class FrmMaterialsPrint : Office2007Form// Form
    {
        public FrmMaterialsPrint(MainParent Msg, string VenderCode,
            string VenderName, string Partnumber, string Kpdesc,
            string PartGroup, string QTY, string PO)
        {
            InitializeComponent();
            ShowMP = Msg;
            this.partnumber = Partnumber;
            this.kpdesc = Kpdesc;
            this.vendercode = VenderCode;
            this.vendername = VenderName;
            this.qty = QTY;
            this.partgroup = PartGroup;
            this.po = PO;
        }
        public MainParent ShowMP;
        //FrmReceiveMaterials mRecei = new FrmReceiveMaterials(ShowMP);
        string partnumber = string.Empty;
        string kpdesc = string.Empty;
        string vendercode = string.Empty;
        string vendername = string.Empty;
        string qty = string.Empty;
        string partgroup = string.Empty;
        string po = string.Empty;
        bool flag;

        private enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error } //文字输出类型
        private Color[] LogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };//文字颜色

        ApplicationClass lbl;// = new ApplicationClass();
        bool isprinter = true;
        private delegate void delegateopenlabel(string filename);
        delegateopenlabel dpl;
        IAsyncResult iasyncresult;
        string InputTrsn = string.Empty;//记录刷入的Trsn序列号,避免发生异常后edt_trsn.text资料丢失
        private void eventopenlablefile(string filename)
        {
            try
            {
                if (!File.Exists(filename))  //判断条码文件是否存在
                {
                    ShowMP.ShowPrgMsg("条码档没有找找到,正在从Ftp下载..", MainParent.MsgType.Warning);
                    FrmBLL.Ftp_MyFtp ftp = new FrmBLL.Ftp_MyFtp();
                    ftp.DownloadFile("FEIXUN_LOT.Lab", System.AppDomain.CurrentDomain.BaseDirectory + "Print", "FEIXUN_LOT.Lab");
                    ShowMP.ShowPrgMsg("下载完成", MainParent.MsgType.Outgoing);
                    //ShowMP.ShowPrgMsg("条码档没有找找到,请确认当前目录是否存在 FEIXUN_LOT.lab", MainParent.MsgType.Error);
                    //this.isprinter = false;
                }
            }
            catch
            {
                this.isprinter = false;
                return;
            }

            this.isprinter = true;
            lbl = new ApplicationClass();
            lbl.Documents.Open(labelfilepatch.Text.Trim(), false);// 调用设计好的label文件
        }
        private delegate void delegateQueryLocId();
        delegateQueryLocId dlQueryLocId;

        private void print5in1label_Load(object sender, EventArgs e)
        {
            try
            {

                if (!Directory.Exists(System.IO.Directory.GetCurrentDirectory() + @"\Print"))
                    Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() +@"\Print");
                labelfilepatch.Text = System.IO.Directory.GetCurrentDirectory() + "\\Print\\FEIXUN_LOT.lab";
                dpl = new delegateopenlabel(eventopenlablefile);
                iasyncresult = dpl.BeginInvoke(labelfilepatch.Text, null, null);
                edt_Scan_Data.Select();
                dlQueryLocId = new delegateQueryLocId(QueryStorehouse);
                dlQueryLocId.BeginInvoke(null, null);

                this.edt_pn.Text = partnumber;
                this.edt_vc.Text = vendercode;
                this.edt_qty.Text = qty;
                this.edt_vc.Items.Add(vendercode);
                this.lb_kpdesc.Text = kpdesc;
                this.labshowvc.Text = "[厂商名:] " + vendercode;
                this.labshowdesc.Text = "[厂商名:] " + vendername;
                flag = false;
                lb_restqty.Text = qty;
            }
            catch (Exception ex)
            {
                this.ShowMP.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void QueryStorehouse()
        {
            cb_store.BeginInvoke(new EventHandler(delegate
            {
                foreach (DataRow dr in FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetWarehouseList()).Rows)
                {
                    if (dr["storehouseId"].ToString().IndexOf("G") == -1)
                        this.cb_store.Items.Add(dr["storehouseId"].ToString());
                }
            }));
        }

        // DataTable vcdt;
        private void ExcuteQueryVender()
        {
            // edt_vc.Items.Clear();

            // RefWebService_BLL.refWeb_VenderInfo.tVenderInfo vn = new RefWebService_BLL.refWeb_VenderInfo.tVenderInfo();
            // vcdt = vn.QueryVender();
            //for (int i = 0; i < vcdt.Rows.Count; i++)
            //{
            //    edt_vc.Items.Add(vcdt.Rows[i][0].ToString());
            //}
        }

        private void edt_Scan_Data_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(edt_Scan_Data.Text) && e.KeyChar == 13)
            {
                string s = edt_Scan_Data.Text;
                string[] sArray = s.Split('|');
                if ((sArray.Length - 1) != 4)
                {
                    MessageBox.Show("五合一条码格式错误");
                    return;
                }

                edt_pn.Text = sArray[0];
                edt_vc.Text = sArray[1];
                edt_dc.Text = sArray[2];
                edt_lot.Text = sArray[3];
                edt_qty.Text = sArray[4];
                edt_Scan_Data.SelectAll();
                edt_Scan_Data.Text = "";
                edt_pn_Validating(null, null);
            }
        }

        private DataTable _datatable;
        private void chkPartNumber(string kpnumber)
        {
            _datatable = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartMap.Instance.QueryPartMaps(this.edt_pn.Text.Trim()));
            if (_datatable == null || _datatable.Rows.Count < 1)
                throw new Exception("输入的料号非法,输入的料号在系统中没有记录..");

            if ((!chkreprint.Checked) && (!chkdeparture.Checked))  //重复列印时直接显示厂商代码
            {
                this.edt_vc.Items.Clear();
                foreach (DataRow dr in _datatable.Rows)
                {
                    this.edt_vc.Items.Add(dr["venderId"].ToString());
                }

                if (this.edt_vc.Items.Count == 1)
                {
                    this.edt_vc.SelectedIndex = 0;
                    this.lb_kpdesc.Text = _datatable.Rows[0]["kpdesc"].ToString();
                }
                else
                    this.edt_vc.Text = "";
            }
        }
        private delegate void delegateprinterlable(bool repeatprt, int printNum, string kpdesc,
            WebServices.tPartStorehousehad.tPartStorehousehad1 partstorehousehadmodel);
        private delegate void delegatesplitprint(List<int> ls, string kpdesc,
            WebServices.tPartStorehousehad.tPartStorehousehad1 partstorehousehadmodel);

        delegateprinterlable eventprintlable;
        private string mTrsn = "";
        int talqty;
        private static int restQty;
        private void butprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (iasyncresult != null && !iasyncresult.IsCompleted)
                {
                    ShowMP.ShowPrgMsg("打印模板还没有初始化成功,请稍后..", MainParent.MsgType.Warning);
                    return;
                }
                if (!string.IsNullOrEmpty(edt_pn.Text) && !string.IsNullOrEmpty(edt_dc.Text.Trim())
                    && !string.IsNullOrEmpty(edt_lot.Text.Trim()) && !string.IsNullOrEmpty(edt_vc.Text.Trim())
                    && !string.IsNullOrEmpty(edt_qty.Text.Trim()))
                {
                    if (this.edt_dc.Text.Length > 9)
                        throw new Exception("生产周期位数过长,请保持在9位以内");
                    if (this.edt_lot.Text.Length > 9)
                        throw new Exception("生产批次位数过长,请保持在9位以内");
                    if (!this.chk_notinputstorehouse.Checked)
                    {
                        if (string.IsNullOrEmpty(this.cb_store.Text) || string.IsNullOrEmpty(this.cblocal.Text))
                            throw new Exception("请选择仓库和库位");
                    }
                    if (!flag)
                    {
                        talqty = Convert.ToInt32(qty) - (Convert.ToInt32(edt_qty.Text) * Convert.ToInt32(print_qty.Text));
                        restQty = talqty;
                        flag = true;
                    }
                    else
                    {
                        restQty = Convert.ToInt32(lb_restqty.Text.Trim()) - (Convert.ToInt32(edt_qty.Text) * Convert.ToInt32(print_qty.Text));
                    }
                    if (restQty < 0)
                    {
                        throw new Exception("打印的数量大于总数量,请确认...");
                    }

                    if (chkdeparture.Checked)
                    {//拆分物料打印
                        #region 拆分料盘 条件检测
                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.QueryTrSn(edt_trsn.Text));
                        string status = dt.Rows[0]["sstatus"].ToString();
                        if (status != "0" && status != "6")
                        {
                            ShowMP.ShowPrgMsg("此盘物料不在仓库,不能分盘", MainParent.MsgType.Error);
                            return;
                        }
                        if (print_qty.Text.Trim() != "1")
                        {
                            ShowMP.ShowPrgMsg("拆分物料列印数量只能选择为 1 ", MainParent.MsgType.Error);
                            return;
                        }

                        if (Convert.ToInt32(edt_qty.Text) > Convert.ToInt32(dt.Rows[0]["qty"].ToString()))
                        {
                            ShowMP.ShowPrgMsg("拆分数量大于原数量, 原数量为: " + dt.Rows[0]["qty"].ToString(), MainParent.MsgType.Error);
                            return;
                        }
                        if (Convert.ToInt32(edt_qty.Text) == Convert.ToInt32(dt.Rows[0]["qty"].ToString()))
                        {
                            ShowMP.ShowPrgMsg("拆分数量等于原数量,请确认", MainParent.MsgType.Error);
                            return;
                        }

                        #endregion
                        //   eventsplitprint = new delegatesplitprint(SplitPrint);
                        //   eventsplitprint.BeginInvoke(
                        RefWebService_BLL.refWebtPartStorehousehad.Instance.UpdateTrSnStatus("4", ShowMP.gUserInfo.userId, edt_trsn.Text);

                        SplitPrint(new List<int> { Convert.ToInt32(edt_qty.Text), Convert.ToInt32(dt.Rows[0]["qty"].ToString()) - Convert.ToInt32(edt_qty.Text) },
                           this.lb_kpdesc.Text,
                           new WebServices.tPartStorehousehad.tPartStorehousehad1()
                           {
                               KpNumber = edt_pn.Text.Trim(),
                               VenderCode = edt_vc.Text.Trim(),
                               DateCode = edt_dc.Text.Trim(),
                               LotId = edt_lot.Text.Trim(),
                               status = 0,
                               UserId = ShowMP.gUserInfo.userId,
                               LocId = cblocal.Text.Trim(),
                               storehouseId = this.cb_store.Text.Trim(),//12-09-11加
                               OUTQTY = 0,
                               recdate = dt.Rows[0]["recdate"].ToString(),
                               FLAG = "DEPART",
                               Remark = InputTrsn
                           });//, null, null);
                    }
                    else
                    {//正常打印和重复打印
                        this.mTrsn = this.edt_trsn.Text.Trim();
                        eventprintlable = new delegateprinterlable(ZPLPrinter);
                        iasyncresult = eventprintlable.BeginInvoke(this.chkreprint.Checked,
                            int.Parse(this.print_qty.Text),
                            this.lb_kpdesc.Text,
                            new WebServices.tPartStorehousehad.tPartStorehousehad1()
                            {
                                KpNumber = partnumber,//edt_pn.Text.Trim(),
                                VenderCode = vendercode, //edt_vc.Text.Trim(),
                                DateCode = edt_dc.Text.Trim(),
                                LotId = edt_lot.Text.Trim(),
                                QTY = Convert.ToInt32(edt_qty.Text.Trim()),
                                UserId = ShowMP.gUserInfo.userId
                                //LocId = cblocal.Text.Trim(),
                                //storehouseId = this.cb_store.Text.Trim(),//12-09-11加
                            }, null, null);
                    }
                    this.lb_restqty.Text = restQty.ToString();
                }

                else
                {
                    ShowMP.ShowPrgMsg("五合一信息输入不完整", MainParent.MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                this.ShowMP.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void ClearTextBox()
        {
            this.tb_vendernumber.Invoke(new EventHandler(delegate
            {
                this.tb_vendernumber.Text = "";
                this.edt_dc.Text = "";
                this.edt_lot.Text = "";
                this.edt_pn.Text = "";
                this.edt_qty.Text = "";
                this.edt_Scan_Data.Text = "";
                this.edt_trsn.Text = "";
                this.edt_vc.Text = "";
                this.cb_store.Text = "";
                this.cblocal.Text = "";
                this.lb_kpdesc.Text = "";
                this.chkdeparture.Checked = false;
                this.print_qty.Text = "1";
            }));
        }
        private void ClearEdtQty()
        {
            this.edt_qty.Invoke(new EventHandler(delegate
            {
                this.edt_qty.Text = "";
                this.print_qty.Text = "1";
            }));
        }
        /// <summary>
        /// 物料拆分打印
        /// </summary>
        /// <param name="ls">拆分数量集合</param>
        /// <param name="kpdesc">物料描述</param>
        /// <param name="partstorehousehadmodel">记录物料信息的实体</param>
        private void SplitPrint(List<int> ls, string kpdesc,
            WebServices.tPartStorehousehad.tPartStorehousehad1 partstorehousehadmodel)
        {
            string prtCmd;
            FrmBLL.PrintControls ZplPrt;// = new FrmBLL.PrintControls();
            if (Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "LableFile"))
            {
                ZplPrt = new FrmBLL.PrintControls();
                StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + @"LableFile\FEIXUN_LOT.TXT",
                    Encoding.GetEncoding("gb2312"));
                prtCmd = sr.ReadToEnd();
                sr.Close();
            }
            string kpdesclast = "";
            string kpdescnext = "";
            try
            {
                kpdesclast = kpdesc.Substring(0, (kpdesc.Length / 2) + 1);
                kpdescnext = kpdesc.Substring((kpdesc.Length / 2) + 1, kpdesc.Length - ((kpdesc.Length / 2) + 1));
            }
            catch
            {
            }

            foreach (int item in ls)
            {
                string _trsn = RefWebService_BLL.refWebtPartStorehousehad.Instance.GetSeqTrSn();
                this.ShowTrSn(_trsn);
                partstorehousehadmodel.Tr_Sn = _trsn;
                partstorehousehadmodel.QTY = item;
                RefWebService_BLL.refWebtPartStorehousehad.Instance.InsertPartStorehousehad(partstorehousehadmodel);

                if (false)
                {
                    ZplPrt.Write(string.Format(prtCmd,
                        partstorehousehadmodel.KpNumber,
                        partstorehousehadmodel.VenderCode,
                        partstorehousehadmodel.DateCode,
                        partstorehousehadmodel.LotId,
                        partstorehousehadmodel.QTY.ToString(),
                        System.DateTime.Now.ToString("yyyy/MM/dd hh:mm"),
                        _trsn,
                        "1",
                        kpdesclast,
                        kpdescnext));
                    ClearTextBox();
                }
                else
                {
                    PrinterLable(kpdesc, _trsn, partstorehousehadmodel);
                    ClearTextBox();
                }
            }
        }
        /// <summary>
        /// 显示唯一编号
        /// </summary>
        /// <param name="trsn"></param>
        private void ShowTrSn(string trsn)
        {
            this.lb_trsn.Invoke(new EventHandler(delegate
            {
                this.lb_trsn.Text = "[唯一编号:] " + trsn;
            }));
        }
        /// <summary>
        /// 正常打印和重复打印及记录入库打印记录
        /// </summary>
        /// <param name="repeatprt">是否重复打印</param>
        /// <param name="printNum">打印数量</param>
        /// <param name="kpdesc">物料描述</param>
        /// <param name="partstorehousehadmodel">记录物料信息的实体</param>
        private void ZPLPrinter(bool repeatprt, int printNum, string kpdesc,
            WebServices.tPartStorehousehad.tPartStorehousehad1 partstorehousehadmodel)
        {
            string prtCmd;
            FrmBLL.PrintControls ZplPrt;// = new FrmBLL.PrintControls();
            if (Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "LableFile"))
            {
                ZplPrt = new FrmBLL.PrintControls();
                StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory +@"LableFile\FEIXUN_LOT.TXT",
                    Encoding.GetEncoding("gb2312"));
                prtCmd = sr.ReadToEnd();
                sr.Close();
            }
            string kpdesclast = "";
            string kpdescnext = "";
            try
            {
                kpdesclast = kpdesc.Substring(0, (kpdesc.Length / 2) + 1);
                kpdescnext = kpdesc.Substring((kpdesc.Length / 2) + 1, kpdesc.Length - ((kpdesc.Length / 2) + 1));
            }
            catch
            {
            }
            for (int i = 0; i < printNum; i++)
            {
                string _trsn = this.mTrsn;//= RefWebService_BLL.refWebtPartStorehousehad.Instance.GetSeqTrSn();
                if (!repeatprt)
                {
                    _trsn = RefWebService_BLL.refWebtPartStorehousehad.Instance.GetSeqTrSn();
                    partstorehousehadmodel.Tr_Sn = _trsn;
                    RefWebService_BLL.refWebtPartStorehousehad.Instance.MaterialPrint(
                        partstorehousehadmodel, kpdesc, partgroup, vendername, po);
                    this.ShowTrSn(_trsn);
                }

                if (false)
                {
                    ZplPrt.Write(string.Format(prtCmd,
                         partstorehousehadmodel.KpNumber,
                         partstorehousehadmodel.VenderCode,
                         partstorehousehadmodel.DateCode,
                         partstorehousehadmodel.LotId,
                         partstorehousehadmodel.QTY.ToString(),
                         System.DateTime.Now.ToString("yyyy/MM/dd hh:mm"),
                         _trsn,
                        "1",
                        kpdesclast,
                        kpdescnext));
                    ClearTextBox();
                }
                else
                {
                    PrinterLable(kpdesc, _trsn, partstorehousehadmodel);
                    ClearEdtQty();
                }
            }
        }

        /// <summary>
        /// 标签打印
        /// </summary>
        /// <param name="kpdesc">描述</param>
        /// <param name="trsn">唯一序列号</param>
        /// <param name="partstorehousehadmodel">记录物料信息的实体</param>
        private void PrinterLable(string kpdesc,
            string trsn,
            WebServices.tPartStorehousehad.tPartStorehousehad1 partstorehousehadmodel)
        {
            if (this.isprinter)
            {
                Document doc = lbl.ActiveDocument;
                doc.Variables.FormVariables.Item("PN").Value = partstorehousehadmodel.KpNumber;// edt_pn.Text.Trim(); //给参数传值
                doc.Variables.FormVariables.Item("VENDER_CODE").Value = partstorehousehadmodel.VenderCode;// edt_vc.Text.Trim();
                doc.Variables.FormVariables.Item("DATECODE").Value = partstorehousehadmodel.DateCode;// edt_dc.Text.Trim();
                doc.Variables.FormVariables.Item("LOT_ID").Value = partstorehousehadmodel.LotId;// edt_lot.Text.Trim();
                doc.Variables.FormVariables.Item("UNIT_SIZE").Value = partstorehousehadmodel.QTY.ToString();// edt_qty.Text.Trim();
                doc.Variables.FormVariables.Item("EMP_NO").Value = ShowMP.gUserInfo.userId;
                doc.Variables.FormVariables.Item("REMARK").Value = kpdesc;
                doc.Variables.FormVariables.Item("TR_SN").Value = trsn;
                doc.Variables.FormVariables.Item("STORLOC").Value = partstorehousehadmodel.storehouseId + "-" + partstorehousehadmodel.LocId;
                doc.PrintDocument(1);//打印
                this.ShowMsg(LogMsgType.Outgoing, string.Format("编号:{1}; 料号:{0}; 打印成功",
                    partstorehousehadmodel.KpNumber, trsn));
            }
            else
            {
                throw new Exception("没有发现条码文档..");
            }
        }

        private void ShowMsg(LogMsgType msgtype, string msg)
        {
            this.rtb_log.Invoke(new EventHandler(delegate
            {
                rtb_log.TabStop = false;
                rtb_log.SelectedText = string.Empty;
                rtb_log.SelectionFont = new Font(rtb_log.SelectionFont, FontStyle.Bold);
                rtb_log.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtb_log.AppendText(msg + "\n");
                rtb_log.ScrollToCaret();
            }));
        }
        private bool priner_station_label(string KpDesc, string Tr_Sn)
        {
            //string filepatch = labelfilepatch.Text.Trim(); ;
            //if (!File.Exists(filepatch))  //判断条码文件是否存在
            //{
            //    ShowMP.ShowPrgMsg("条码档没有找找到,请确认当前目录是否存在 FEIXUN_LOT.lab", MainParent.MsgType.Error);
            //    return false;
            //}

            try
            {
                if (this.isprinter)
                {
                    // lbl.Documents.Open(filepatch, false);// 调用设计好的label文件
                    Document doc = lbl.ActiveDocument;
                    doc.Variables.FormVariables.Item("PN").Value = edt_pn.Text.Trim(); //给参数传值
                    doc.Variables.FormVariables.Item("VENDER_CODE").Value = edt_vc.Text.Trim();
                    doc.Variables.FormVariables.Item("DATECODE").Value = edt_dc.Text.Trim();
                    doc.Variables.FormVariables.Item("LOT_ID").Value = edt_lot.Text.Trim();
                    doc.Variables.FormVariables.Item("UNIT_SIZE").Value = edt_qty.Text.Trim();
                    doc.Variables.FormVariables.Item("EMP_NO").Value = ShowMP.gUserInfo.userId;
                    doc.Variables.FormVariables.Item("REMARK").Value = KpDesc;
                    doc.Variables.FormVariables.Item("TR_SN").Value = Tr_Sn;

                    int Num = Convert.ToInt32(print_qty.Text.Trim());        //打印数量
                    doc.PrintDocument(Num);//打印                  
                }
                else
                {
                    throw new Exception("没有发现条码文档..");
                }

                return true;
            }
            catch (Exception ex)
            {
                ShowMP.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                return false;
            }
        }

        private void edt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void print_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            fmParMap fPM = new fmParMap(ShowMP);
            fPM.ShowDialog();
        }

        private void edt_qty_TextChanged(object sender, EventArgs e)
        {

        }

        private void print5in1label_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                lbl.Documents.CloseAll(false);
                lbl.Quit();                                         //退出      
            }
            catch
            {
            }
        }
        /// <summary>
        /// 当选择厂商改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void edt_vc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.edt_vc.Text))
                return;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebVenderInfo.Instance.QueryVenderInfoByVenderId(this.edt_vc.Text.Trim()));

            if (dt == null || dt.Rows.Count < 1)
            {
                this.labshowdesc.ForeColor = Color.Red;
                this.labshowvc.ForeColor = Color.Red;
                this.labshowvc.Text = "[厂商名:] 没有找到厂商信息..";
                this.labshowdesc.Text = "[厂商名:] 没有找到厂商信息";

            }
            else
            {
                this.labshowdesc.ForeColor = Color.Black;
                this.labshowvc.ForeColor = Color.Black;
                this.labshowvc.Text = "[厂商名:] " + dt.Rows[0]["vendername"].ToString();
                this.labshowdesc.Text = "[厂商名:] " + dt.Rows[0]["vendername2"].ToString();
            }
            this.lb_kpdesc.Text = "";
            foreach (DataRow dr in this._datatable.Rows)
            {
                if (dr["venderId"].ToString().ToUpper() == this.edt_vc.Text.Trim().ToUpper())
                {
                    this.lb_kpdesc.Text = dr["kpdesc"].ToString();
                }
            }
        }

        public string checkvender(out string msg)
        {

            for (int z = 0; z < edt_vc.Items.Count; z++)
            {
                if (edt_vc.Items[z].ToString() == edt_vc.Text.Trim())
                {
                    return msg = "OK";
                    break;
                }
            }
            return msg = "NO";
        }

        private void tb_vendernumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.iasyncresult != null && !this.iasyncresult.IsCompleted)
            {
                this.ShowMP.ShowPrgMsg("请稍等..", MainParent.MsgType.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.tb_vendernumber.Text))
                return;
            if (e.KeyValue == 13)
            {
                DataTable _dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartMap.Instance.GetPartMapsBy(this.tb_vendernumber.Text));
                // DataTable _dt = refWebtVenderCode.Instance.GetVendCodeInfo(this.tb_vendernumber.Text);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    this.edt_pn.Items.Clear();
                    foreach (DataRow dr in _dt.Rows)
                    {
                        this.edt_pn.Items.Add(dr["kpnumber"].ToString());
                    }
                    if (_dt.Rows.Count < 2)
                        this.edt_pn.SelectedIndex = 0;
                    else
                        this.edt_pn.Text = "";
                    this.edt_vc.Text = "";
                    // MessageBox.Show(_dt.Rows[0]["venderId"].ToString());
                    this.edt_vc.SelectedText = _dt.Rows[0]["venderId"].ToString();
                    edt_vc_SelectedIndexChanged(null, null);
                }
                else
                {
                    this.edt_vc.Items.Clear();
                    this.edt_pn.Items.Clear();
                    this.edt_vc.Text = "";
                    this.edt_pn.Text = "";
                }
            }
        }

        private void panelEx1_Click(object sender, EventArgs e)
        {

        }

        private void edt_pn_Validating(object sender, CancelEventArgs e)
        {
            if (!chkreprint.Checked)  //重复列印直接显示列印数据
            {
                try
                {
                    if (this.iasyncresult != null && !this.iasyncresult.IsCompleted)
                    {
                        this.ShowMP.ShowPrgMsg("请稍等..", MainParent.MsgType.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(this.edt_pn.Text))
                        return;
                    this.chkPartNumber(this.edt_pn.Text.Trim());
                }
                catch (Exception ex)
                {
                    this.edt_vc.Items.Clear();
                    this.ShowMP.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                }
            }
        }

        private void getdc_Click(object sender, EventArgs e)
        {

        }

        private void edt_dc_MouseLeave(object sender, EventArgs e)
        {
            if (getdc.Checked)
            {
                edt_dc.Text = System.DateTime.Now.ToString("yyyyMMdd");
            }
        }

        private void edt_lot_MouseLeave(object sender, EventArgs e)
        {
            if (getdc.Checked)
            {
                edt_lot.Text = System.DateTime.Now.ToString("yyyyMMdd");
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Control myc = GetFocusedControl();
                if (myc.GetType().Name.IndexOf("TextBox") != -1)
                {
                    myc.Text = "12345";
                }
            }
            catch
            {
            }
            // return;
            //rd.GetComData(edt_Scan_Data);
            //edt_Scan_Data.SelectAll();
            //edt_Scan_Data.Focus();
        }

        #region xxxx

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr GetFocus();

        /// <summary>
        /// 当前拥有焦点的控件
        /// </summary>
        /// <param name="formControl"></param>
        /// <returns></returns>
        public static Control GetFocusedControl()
        {
            Control focusedControl = null;

            try
            {
                IntPtr focusedHandle = GetFocus();

                if (focusedHandle != IntPtr.Zero)
                {
                    focusedControl = Control.FromChildHandle(focusedHandle);
                }
            }
            catch { }

            return focusedControl;
        }

        #endregion

        private void edt_trsn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(edt_trsn.Text) & (e.KeyCode == Keys.Enter))
            {
                DataTable dttrsn = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.QueryTrSn(this.edt_trsn.Text.Trim()));
                if (dttrsn.Rows.Count > 0)
                {
                    edt_pn.Text = dttrsn.Rows[0]["kpnumber"].ToString();
                    edt_dc.Text = dttrsn.Rows[0]["datecode"].ToString();
                    edt_vc.Text = dttrsn.Rows[0]["vendercode"].ToString();
                    edt_lot.Text = dttrsn.Rows[0]["lotId"].ToString();
                    edt_qty.Text = dttrsn.Rows[0]["qty"].ToString();
                    cblocal.Text = dttrsn.Rows[0]["locId"].ToString();
                    cb_store.Text = dttrsn.Rows[0]["storehouseid"].ToString();
                    edt_trsn.Focus();
                    edt_trsn.SelectAll();
                    chkPartNumber(edt_pn.Text);
                    //TrSn = edt_trsn.Text.Trim();
                    edt_vc_SelectedIndexChanged(null, null);
                    InputTrsn = edt_trsn.Text.Trim();
                }
                else
                {
                    ShowMP.ShowPrgMsg("唯一条码输入错误,请确认", MainParent.MsgType.Error);
                    edt_trsn.Focus();
                    edt_trsn.SelectAll();
                }

            }
        }

        private void chkreprint_Click(object sender, EventArgs e)
        {
            //if (chkreprint.Checked == true)
            //{
            //    chkreprint.Checked = true;
            //    edt_trsn.Enabled = true;

            //}
            //else
            //{
            //    chkreprint.Checked = false;
            //    edt_trsn.Enabled = false;
            //}

        }

        private void btlocid_Click(object sender, EventArgs e)
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseLoctionInfo());

            //foreach (DataRow dr in dt.Rows)
            //{
            //    cblocal.Items.Add(dr["locid"].ToString());
            //}
            SelectData sd = new SelectData(this, dt);
            sd.ShowDialog();
        }

        private void edt_dc_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelX5_Click(object sender, EventArgs e)
        {

        }

        private void cb_store_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((!chkreprint.Checked) && (!chkdeparture.Checked))
            {
                try
                {
                    this.cblocal.Text = "";
                    this.cblocal.Items.Clear();
                    foreach (DataRow dr in FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetWarehouseLoctionListBystorehouseId(this.cb_store.Text.Trim())).Rows)
                    {
                        this.cblocal.Items.Add(dr["locId"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    this.ShowMP.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                }
            }
        }

        private void cb_store_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.cb_store.Text))
                    return;
                cb_store_SelectedValueChanged(sender, e);
            }
            catch (Exception ex)
            {
                this.ShowMP.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void chkdeparture_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdeparture.Checked)
            {
                if (this.chk_notinputstorehouse.Checked)
                {
                    MessageBoxEx.Show("物料暂收功能已经启用，不能使用分盘功能!!", "提示");
                    this.chkdeparture.Checked = false;
                    return;
                }

                if (this.chkreprint.Checked)
                {
                    MessageBoxEx.Show("重复列印状态下不能使用分盘功能", "提示");
                    this.chkdeparture.Checked = false;
                    return;
                }
                edt_Scan_Data.Enabled = false;
                edt_trsn.Enabled = true;
                chkreprint.Enabled = false;
                this.chk_notinputstorehouse.Enabled = false;
                edt_trsn.Focus();
            }
            else
            {
                this.chk_notinputstorehouse.Enabled = true;
                edt_Scan_Data.Enabled = true;
                edt_trsn.Enabled = false;
                chkreprint.Enabled = true;
                edt_Scan_Data.Focus();
                chkreprint.Checked = false;
            }
        }

        private void chk_notinputstorehouse_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_notinputstorehouse.Checked)
            {
                if (MessageBoxEx.Show("使用该功能将不记录物料的库存位置,可用于暂时收料\n\n确认库位后请使用〔材料批量入库功能〕为其分配库具体的位",
                    "提示",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Asterisk) != DialogResult.Yes)
                {
                    this.chk_notinputstorehouse.Checked = false;
                    return;
                }
                this.cb_store.Text = string.Empty;
                this.cblocal.Text = string.Empty;
                this.cb_store.Enabled = false;
                this.cblocal.Enabled = false;
                this.btlocid.Enabled = false;
                chkdeparture.Enabled = false;
                chkdeparture.Checked = false;
                this.chk_notinputstorehouse.ForeColor = Color.Red;
            }
            else
            {
                this.cb_store.Enabled = true;
                this.cblocal.Enabled = true;
                this.btlocid.Enabled = true;
                chkdeparture.Enabled = true;
                this.chk_notinputstorehouse.ForeColor = Color.Black;
            }
        }

        private void chkreprint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkreprint.Checked)
            {
                // chkreprint.Checked = true;
                edt_trsn.Enabled = true;
                chkdeparture.Checked = false;
                chkdeparture.Enabled = false;
                this.panel2.Enabled = false;

            }
            else
            {
                //chkreprint.Checked = false;
                edt_trsn.Enabled = false;
                if (!this.chk_notinputstorehouse.Checked)
                    chkdeparture.Enabled = true;
                this.panel2.Enabled = true;
            }
        }
    }
}
