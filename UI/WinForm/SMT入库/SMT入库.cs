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
using System.IO;
using GenericUtil;

namespace SFIS_V2
{
    public partial class Frm_SmtStockIn : Office2007Form //Form
    {
        public Frm_SmtStockIn(MainParent frm)
        {
            InitializeComponent();
            mFrm = frm;
        }
        private MainParent mFrm;
        private void Frm_SmtStockIn_Load(object sender, EventArgs e)
        {
            try
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
                ReadIniFile();
                SetStation();
                txt_woid.Focus();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private readonly string IniFilePath = "C:\\SFIS\\SFIS.ini";
        public string MyLine = string.Empty;
        public string MYGROUP = string.Empty;
        public string MyStation = string.Empty;
        public string MySection = string.Empty;

        BLL.tWipTracking mWipTraking = new BLL.tWipTracking();
       // BLL.Db_Procedure mProcedure = new BLL.Db_Procedure();
        WebServices.tZ_Whs_SAP_BackFlush.tZ_Whs_SAP_BackFlush zBackFlush = new WebServices.tZ_Whs_SAP_BackFlush.tZ_Whs_SAP_BackFlush();

        /// <summary>
        /// 工单储位
        /// </summary>
        private string Loc = string.Empty;

        public void SetStation()
        {
            LabStation.Text = MyLine + " : " + MYGROUP;
            FrmBLL.ReadIniFile.IniWriteValue("SMT_STOCKIN", "LINE", MyLine, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("SMT_STOCKIN", "MYGROUP", MYGROUP, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("SMT_STOCKIN", "STATION", MyStation, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("SMT_STOCKIN", "SECTION", MySection, IniFilePath);
        }

        private void ReadIniFile()
        {
            MyLine = FrmBLL.ReadIniFile.IniReadValue("SMT_STOCKIN", "LINE", IniFilePath);
            MYGROUP = FrmBLL.ReadIniFile.IniReadValue("SMT_STOCKIN", "MYGROUP", IniFilePath);
            MyStation = FrmBLL.ReadIniFile.IniReadValue("SMT_STOCKIN", "STATION", IniFilePath);
            MySection = FrmBLL.ReadIniFile.IniReadValue("SMT_STOCKIN", "SECTION", IniFilePath);
        }
        private void imbt_SelectLine_Click(object sender, EventArgs e)
        {
            Frm_StationName fsm = new Frm_StationName(this);
            fsm.ShowDialog();
        }
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };
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
                    rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = mLogMsgTypeColor[(int)msgtype];
                    rtbmsg.AppendText(msg + "\n");
                    rtbmsg.ScrollToCaret();
                    WriteLog(msg);
                }));
            }
            catch
            {
            }
        }
        private void WriteLog(string StrLog)
        {
            #region 存储失败日志在服务器
            try
            {
                //如果文件a.txt存在就打开，不存在就新建 .append 是追加写 
                string Patch = System.Environment.CurrentDirectory + "\\log";
                if (!File.Exists(Patch + "\\log.txt"))
                    Directory.CreateDirectory(Patch);
                FileStream fst = new FileStream(Patch + "\\log.txt", FileMode.Append);
                //写数据到a.txt格式 
                StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
                swt.WriteLine(StrLog + "  Time" + DateTime.Now.ToString());
                swt.Close();
                fst.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入Log失败:" + ex.Message + ",\r\n请及时联系SFIS人员");
            }
            #endregion
        }
        private void txt_woid_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_woid.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    dgvshowdata.DataSource = null;
                    if (string.IsNullOrEmpty(MYGROUP))
                        throw new Exception("请选择途程");
                    if (string.IsNullOrEmpty(MyLine))
                        throw new Exception("请选择线体");
         
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(txt_woid.Text, null, "LOC,OUTPUTGROUP"));
                    DataTable _dtStock = new DataTable();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["OUTPUTGROUP"].ToString() != MYGROUP)
                            throw new Exception(string.Format("选择的途程与工单结束途程不符[{0}≠{1}]", MYGROUP, dt.Rows[0]["OUTPUTGROUP"].ToString()));
                        Loc = dt.Rows[0]["LOC"].ToString();

                        Dictionary<string, object> mst = new Dictionary<string, object>();
                        mst.Add("WOID", txt_woid.Text);
                        mst.Add("STORENUMBER", "NA");
                        mst.Add("WIPSTATION", MYGROUP);
                        string Colnum = @"ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE,
                            ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,LINE,SECTIONNAME,ROUTGROUPID,STORENUMBER,QA_NO,QA_RESULT,REWORKNO";
                        DataTable dtwip = mWipTraking.GetWipTracking(mst, Colnum).Tables[0];
                        if (dtwip.Rows.Count > 0)
                        {
                            _dtStock = dtwip.Clone();
                            ShowMsg(mLogMsgType.Normal, "开始检查途程.....");
                            foreach (DataRow dr in dtwip.Rows)
                            {
                                Dictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add("DATA", dr["ESN"].ToString());
                                dic.Add("MYGROUP", MYGROUP);
                                string _StrErr = refWebProcedure.Instance.ExecuteProcedure("PRO_CHECKROUTE", FrmBLL.ReleaseData.DictionaryToJson(dic));
                                // string _StrErr = mProcedure.PRO_CHECK_ROUTE(dr["ESN"].ToString(), MYGROUP); // oDB.CheckRoute(dr["ESN"].ToString(), LabCraft.Text);
                                if (_StrErr == "OK")
                                {
                                    _dtStock.ImportRow(dr);
                                }
                                else
                                {
                                    ShowMsg(mLogMsgType.Warning, string.Format("检查途程失败,ESN [{0}]-> {1}", dr["ESN"].ToString(), _StrErr));
                                }
                            }

                            dgvshowdata.DataSource = _dtStock;
                            ShowMsg(mLogMsgType.Incoming, string.Format("工单[{0}] OK,共计[{1}]笔", txt_woid.Text, dgvshowdata.Rows.Count.ToString()));
                        }
                        else
                        {
                            ShowMsg(mLogMsgType.Error, string.Format("没有待入库数据"));
                        }
                    }
                    else
                    {
                        ShowMsg(mLogMsgType.Error, string.Format("工单[{0}]不存在", txt_woid.Text));

                    }
                }
                catch (Exception ex)
                {
                    ShowMsg(mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    txt_woid.SelectAll();
                }
            }
        }

        private void imbt_commit_Click(object sender, EventArgs e)
        {
            if (dgvshowdata.Rows.Count > 0)
            {
                try
                {
                    if (MessageBox.Show("确定入库?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        DataTable dt = dgvshowdata.DataSource as DataTable;
                        LabError.Visible = false;
                        string STOCK_NO = refWebProcedure.Instance.GetStockInNumber();
                        List<string> Fields = new List<string>();
                        Fields.Add("ESN");
                        Dictionary<string, object> mst = null;

                        WriteLog("开始SFIS单过站:" + STOCK_NO);
                        string _StrErr = string.Empty;
                        foreach (DataRow dr in dt.Rows)
                        {
                            mst= new Dictionary<string, object>();
                            mst.Add("STORENUMBER", STOCK_NO);
                            mst.Add("ESN", dr["ESN"].ToString());
                            _StrErr = refWebtWipTracking.Instance.Update_Wip_Tracking(FrmBLL.ReleaseData.DictionaryToJson(mst));//  mWipTraking.Update_WIP_TRACKING(mst, Fields);
                            if (_StrErr != "OK")
                                throw new Exception(string.Format("ESN:{0}  UPDATE STOCKNO Faill: {1}", dr["ESN"].ToString(), _StrErr));
                        }

                        #region 执行过站
                        LabError.Text = string.Empty;
                        Call_TEST_MAIN_ONLY(STOCK_NO);
                        if (!string.IsNullOrEmpty(LabError.Text))
                        {
                            ShowMsg(mLogMsgType.Error, "Call_TEST_STOCKIN Error");
                            return;
                        }
                        ShowMsg(mLogMsgType.Normal, "更改WIP STATION");

                        ///更改WIPSTATION
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("STORENUMBER", STOCK_NO);
                        dic.Add("WIPSTATION", Loc);
                        List<string> ListFields = new List<string>();
                        ListFields = new List<string>();
                        ListFields.Add("STORENUMBER");
                       // mWipTraking.Update_WIP_TRACKING(dic, ListFields);
                        _StrErr = refWebtWipTracking.Instance.Update_Wip_Tracking(FrmBLL.ReleaseData.DictionaryToJson(dic), ListFields.ToArray());

                        ShowMsg(mLogMsgType.Normal, "数据上传完成....");
                        PrintInventoryDocuments(STOCK_NO);
                        dgvshowdata.DataSource = null;
                        txt_woid.Text = string.Empty;
                        #endregion



                    }
                }
                catch (Exception ex)
                {
                    ShowMsg(mLogMsgType.Error, ex.Message);
                }

            }
            else
            {
                ShowMsg(mLogMsgType.Error, "没有数据可以入库");
            }
        }

        private void Call_TEST_MAIN_ONLY(string STOCK_NO)
        {
            int Cur_Rec = 0;
            int Tot_Rec = 0;
            int Err_Rec = 0;

            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("STORENUMBER", STOCK_NO);
            DataTable dt = FrmBLL.publicfuntion.getNewTable(mWipTraking.GetWipTracking(mst, "ESN,WOID,LOCSTATION").Tables[0], string.Format("LOCSTATION<>'{0}'", MYGROUP));
            if (dt.Rows.Count > 0)
            {
                DataTable dt_woinfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(dt.Rows[0]["WOID"].ToString(), null, "LOC"));
                if (dt_woinfo.Rows.Count <= 0)
                    throw new Exception("没有找到工单信息:" + dt.Rows[0]["WOID"].ToString());
                    Loc = dt_woinfo.Rows[0]["LOC"].ToString();
            }

            Tot_Rec = dt.Rows.Count;
            string _StrErr = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("DATA", dr["ESN"].ToString());
                dic.Add("MYGROUP", MYGROUP);
                dic.Add("SECTION_NAME", MySection);
                dic.Add("STATION_NAME", MyStation);
                dic.Add("EMP", mFrm.gUserInfo.userId + "-" + mFrm.gUserInfo.pwd);
                dic.Add("EC", "NA");
                dic.Add("LINE", MyLine);
                 _StrErr = refWebProcedure.Instance.ExecuteProcedure("PRO_TEST_MAIN_ONLY", MapListConverter.DictionaryToJson(dic));

              //  mProcedure.PRO_TEST_MAIN_ONLY(dr["ESN"].ToString(), MYGROUP, MySection, MyStation, mFrm.gUserInfo.userId + "-" + mFrm.gUserInfo.pwd, "NA",MyLine, out  _StrErr);
                Cur_Rec = Cur_Rec + 1;
                progressBarX1.Value = (int)(Math.Round((decimal)Cur_Rec / Tot_Rec, 2) * 100); //Convert.ToInt32((Convert.ToDouble(Cur_Rec / Tot_Rec) * 100));
                progressBarX1.Update();
                progressBarX1.Text = (Tot_Rec - Cur_Rec).ToString();
                progressBarX1.Update();
                if (Cur_Rec % 50 == 0)
                {
                    System.Threading.Thread.Sleep(200);
                    this.Refresh();
                }
                if (_StrErr != "OK")
                {
                    Err_Rec = Err_Rec + 1;
                    LabError.Visible = true;
                    LabError.Text = "NG Count-->" + Err_Rec.ToString();
                }
            }

        }

        /// <summary>
        /// 单据打印
        /// </summary>
        string PrintStockNo = string.Empty;
        DataTable dtStock = null;

        /// <summary>
        /// 列印入库单据
        /// </summary>
        /// <param name="StockIn"></param>
        public void PrintInventoryDocuments(string STOCK_NO)
        {
            bool PrintFlag = true;
            DataTable dtPrintDoc =  mWipTraking.GetStockInPrint(STOCK_NO).Tables[0];

            if (dtPrintDoc.Rows.Count == 0)
                throw new Exception("没有需要打印的数据,请确认单号是否正确...");

            ShowMsg(mLogMsgType.Outgoing, string.Format("单据号[{0}]", STOCK_NO));
            foreach (DataRow dr in dtPrintDoc.Rows)
            {
                zBackFlush.Insert_SAP_BackFlush(STOCK_NO, dr["WOID"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(), Convert.ToInt32(dr["QTY"].ToString()));
                WriteLog(string.Format("Insert_SAP_BackFlush [{0}] [{1}] [{2}] [{3}] [{4}]", STOCK_NO, dr["WOID"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(), dr["QTY"].ToString()));
            }

            PrintStockNo = STOCK_NO;
            dtStock = dtPrintDoc;
            foreach (DataRow dr in dtStock.Rows)
            {               
                if (dr[2].ToString() != Loc)
                {
                    ShowMsg(mLogMsgType.Error, "数据未全部上抛完成,不列印单据");
                    PrintFlag = false;
                    break;
                }
            }
            if ((PrintFlag) && (dtStock.Rows.Count > 0))
                printDocument1.Print();
            dtStock = null;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                int ofLeft = 15;
                int ofTop = 50; //距顶
                int height = 20; //行距
                int ofWidth = 110;//水平间距
                int x = 3; //距顶,设置列高
                int xx = 0;
                int yy = ofTop * x - 10; //表头上下距离
                int Total = 0;
                //定义表头及格式


                e.Graphics.DrawString("万得凯实业有限公司", new Font("宋体", 18, FontStyle.Bold), Brushes.Black, ofLeft + 240, ofTop - 15);
                e.Graphics.DrawString("入库单", new Font("宋体", 18, FontStyle.Bold), Brushes.Black, ofLeft + 315, 70);
                e.Graphics.DrawString("-----------------------------------------------------------------------------------------------",
                    new Font("宋体", 12, FontStyle.Regular), Brushes.Black, 5, 96);

                e.Graphics.DrawString("-----------------------------------------------------------------------------------------------",
                  new Font("宋体", 12, FontStyle.Regular), Brushes.Black, 5, 125);

                e.Graphics.DrawString("入库单号:" + PrintStockNo, new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 3 * ofWidth - 8, ofTop * 2 + 10);
                e.Graphics.DrawString("入库日期:" + DateTime.Now.ToString("yyyy.MM.dd HH:mm"), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 6 * ofWidth - 150, ofTop * 2 + 10);

                e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 30, yy);

                e.Graphics.DrawString("序号", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft - 10, yy);

                e.Graphics.DrawString("工单", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 1 * ofWidth - 70, yy);

                e.Graphics.DrawString("料号", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 2 * ofWidth - 90, yy);

                e.Graphics.DrawString("品名/规格", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 3 * ofWidth - 125, yy);

                e.Graphics.DrawString("单位", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 4 * ofWidth + 40, yy);

                e.Graphics.DrawString("数量", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 5 * ofWidth - 15, yy);

                e.Graphics.DrawString("实/收发数量", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 6 * ofWidth + 30 - 100, yy);

                e.Graphics.DrawString("仓库", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 7 * ofWidth - 80, yy);

                e.Graphics.DrawString("备注", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 8 * ofWidth - 140, yy);

                int zz = dtStock.Rows.Count / 8 + 1;
                for (int i = 0; i <= zz * 7; i++)
                {
                    try
                    {
                        xx = (ofTop + (i + x + 3) * height) - 10;
                        string woId = dtStock.Rows[i][0].ToString();
                        e.Graphics.DrawString((i + 1).ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft - 5, xx);
                        //工单
                        e.Graphics.DrawString(woId, new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 30, xx);
                        //料号
                        e.Graphics.DrawString(dtStock.Rows[i][1].ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 110, xx);
                        //品名
                        e.Graphics.DrawString(dtStock.Rows[i][3].ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 205, xx);

                        e.Graphics.DrawString("PC", new Font("宋体", 10, FontStyle.Regular), Brushes.Black, ofLeft + 485, xx);
                        //数量
                        e.Graphics.DrawString(dtStock.Rows[i][4].ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 538, xx);

                        e.Graphics.DrawString("________", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 600, xx - 1);

                        // e.Graphics.DrawString(dtStock.Rows[i][2].ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 690, xx);
                        e.Graphics.DrawString("_____", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 690, xx);

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 730, xx);

                        Total = Total + Convert.ToInt32(dtStock.Rows[i][4].ToString());
                    }
                    catch (Exception)
                    {

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft - 5, xx);
                        //工单
                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 30, xx);
                        //料号
                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 110, xx);
                        //品名
                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 205, xx);

                        e.Graphics.DrawString("", new Font("宋体", 10, FontStyle.Regular), Brushes.Black, ofLeft + 485, xx);
                        //数量
                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 538, xx);

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 600, xx - 1);

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 690, xx);

                        e.Graphics.DrawString("", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 730, xx);
                    }
                }

                e.Graphics.DrawString("-----------------------------------------------------------------------------------------------",
                           new Font("宋体", 12, FontStyle.Regular), Brushes.Black, 5, xx + 30);

                e.Graphics.DrawString("合计:", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 4 * ofWidth + 40, xx + 50);
                e.Graphics.DrawString(Total.ToString(), new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 4 * ofWidth + 100, xx + 50);
                e.Graphics.DrawString("生产部", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft, xx + 100);
                e.Graphics.DrawString("质量部", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 300, xx + 100);
                e.Graphics.DrawString("仓库", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 600, xx + 100);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void imbt_Reprint_Click(object sender, EventArgs e)
        {
            try
            {
                string STOCK_NO = Input.InputQuery.ShowInputBox("输入入库单号", string.Empty);
                if (!string.IsNullOrEmpty(STOCK_NO))
                {
                    PrintInventoryDocuments(STOCK_NO);
                }
            }
            catch (Exception ex)
            {
                ShowMsg(mLogMsgType.Error, ex.Message);
            }
        }

        private void Imbt_ReUpload_Click(object sender, EventArgs e)
        {
            string StockNo =Input.InputQuery.ShowInputBox("请输入入库单据", string.Empty).ToUpper();
            if (!string.IsNullOrEmpty(StockNo) && StockNo.Trim() != "NA")
            {
                try
                {
                    LabError.Text = string.Empty;
                    ShowMsg(mLogMsgType.Normal, "执行过站...");
                    Call_TEST_MAIN_ONLY(StockNo);
                    if (!string.IsNullOrEmpty(LabError.Text))
                    {
                        ShowMsg(mLogMsgType.Error, "Call_TEST_STOCKIN Error");
                        return;
                    }
                    ShowMsg(mLogMsgType.Normal, "更改WIP STATION");

                    ///更改WIPSTATION
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("STORENUMBER", StockNo);
                    dic.Add("WIPSTATION", Loc);
                    List<string> ListFields = new List<string>();
                    ListFields = new List<string>();
                    ListFields.Add("STORENUMBER");
                    //mWipTraking.Update_WIP_TRACKING(dic, ListFields);
                    refWebtWipTracking.Instance.Update_Wip_Tracking(FrmBLL.ReleaseData.DictionaryToJson(dic), ListFields.ToArray());

                    ShowMsg(mLogMsgType.Normal, "数据上传完成....");
                    PrintInventoryDocuments(StockNo);
                    dgvshowdata.DataSource = null;
                    txt_woid.Text = string.Empty;              

                }
                catch (Exception ex)
                {
                    ShowMsg(mLogMsgType.Error, ex.Message);
                }
            }
        }
    }
}
