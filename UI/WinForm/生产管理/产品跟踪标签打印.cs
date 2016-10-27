using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;

namespace SFIS_V2
{
    public partial class PrintSFCSn : Office2007Form// Form
    {
        public PrintSFCSn(MainParent frm)
        {
            InitializeComponent();
            mFrm = frm;
        }

        MainParent mFrm;
        /// <summary>
        /// 序列号的开始
        /// </summary>
        private string _snstart = string.Empty;
        /// <summary>
        /// 序列号的结束
        /// </summary>
        private string _snend = string.Empty;
        /// <summary>
        /// 序列号的头
        /// </summary>
        private string snhead = string.Empty;
      //  private string[] arrWoid = null;
        private Dictionary<string, string> facNameAndfacCode = new Dictionary<string, string>();
        private DateTime ServerTime = new DateTime();
        /// <summary>
        /// 工单信息
        /// </summary>
        IDictionary<string, object> dicwo = new Dictionary<string, object>();
        /// <summary>
        /// 工厂代码
        /// </summary>
        private string FacCode = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        private string mProductName = string.Empty;
        ///// <summary>
        ///// 记录月份信息1-12月用 1-Z表示
        ///// </summary>
        //private Dictionary<int, string> strMonth = new Dictionary<int, string>();
        ///// <summary>
        ///// 记录日期1-31 用1-Z表示
        ///// </summary>
        //private Dictionary<int, string> strDay = new Dictionary<int, string>();
        
        private enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error } //文字输出类型
        private Color[] LogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };//文字颜色

        private LabelManager2.ApplicationClass lppx;
        private LabelManager2.Document doc;
        private IAsyncResult iasyncresult;
        private IAsyncResult iasyncresult1;
        private delegate void LoadLppxApp(string strHead, string startSn, string Productname, int printNum);
        LoadLppxApp delegateLoadlppxApp;

        private void PrintSFCSn_Load(object sender, EventArgs e)
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
                //for (int i = 1; i < 32; i++)
                //{
                //    if (i < 13)
                //        this.strMonth.Add(i, FrmBLL.publicfuntion.h2x(i, 36));
                //    this.strDay.Add(i, FrmBLL.publicfuntion.h2x(i, 36));
                //}
                FillFacName();
                FillWoList();
                GetDateTime();

                delegateLoadlppxApp = new LoadLppxApp(LoadLppx);
                iasyncresult = delegateLoadlppxApp.BeginInvoke("", "", "", 0, null, null);

            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }


        /// <summary>
        /// 加载标签文档
        /// </summary>
        private void LoadLppx(string strHead, string startSn, string Productname, int printNum)
        {
            string mdl = string.Empty;
            if (this.rb_printmodel1.Checked)
                mdl = "SFCSN1.lab";
            else
                mdl = "SFCSN.lab";
            string labpath = string.Format(@"{0}\{1}\{2}", System.IO.Directory.GetCurrentDirectory(), "Print", mdl);
            if (!File.Exists(labpath))
            {
                mFrm.ShowPrgMsg("条码档没有找找到,正在从Ftp下载..", MainParent.MsgType.Warning);
                FrmBLL.Ftp_MyFtp ftp = new FrmBLL.Ftp_MyFtp();
                ftp.DownloadFile(mdl, System.AppDomain.CurrentDomain.BaseDirectory + "Print", mdl);
                mFrm.ShowPrgMsg("标签文档下载完成", MainParent.MsgType.Outgoing);
            }
            lppx = new LabelManager2.ApplicationClass();
            doc = lppx.Documents.Open(labpath, true);
            doc.ViewMode = LabelManager2.enumViewMode.lppxViewModeSize;
        }

        /// <summary>
        /// 填充文档变量
        /// </summary>
        /// <param name="var0"></param>
        /// <param name="var1"></param>
        void FillLabelAndPrint(string var0, string var1)
        {
            this.doc.Variables.FormVariables.Item("VAR0").Value = var0;
            this.doc.Variables.FormVariables.Item("VAR1").Value = var1;
            this.doc.PrintDocument(1);
            this.ShowMsg(LogMsgType.Outgoing, string.Format("标签{0}已经输出", var0));
        }

        void Print2(string startValue,string var0,string var1,int printNum)
        {
            this.doc.Variables.FormVariables.Item("VAR0").Value = var0;
            this.doc.Variables.FormVariables.Item("VAR1").Value = var1;
           
            this.doc.Variables.Counters.Item("COUNT1").BaseType = LabelManager2.enumCounterBase.lppxBaseAlphaNumeric;
            this.doc.Variables.Counters.Item("COUNT1").Value = startValue;
            this.doc.Variables.Counters.Item("COUNT1").MaxValue = "ZZZZZZ";
            this.doc.PrintDocument(printNum);
            this.ShowMsg(LogMsgType.Outgoing, string.Format("序列号{0}{1} 到 {0}{2}已经输出", 
                this.FacCode + this.snhead, this._snstart, this._snend));
            EnaControl(true);

        }
        private void EnaControl(bool ena)
        {
            this.num_printnum.Invoke(new EventHandler(delegate
                {
                    this.cb_woid.Enabled = ena;
                    this.cb_facname.Enabled = ena;
                    this.num_printnum.Enabled = ena;
                }));
        }
        private void PrintLable(string strHead, string startSn, string Productname, int printNum)
        {
            string strTemp = string.Empty;
            strTemp = this.GetFromartSn(startSn);
            for (int i = 0; i < printNum; i++)
            {
                FillLabelAndPrint(strHead + this.GetFromartSn(FrmBLL.publicfuntion.h2x(FrmBLL.publicfuntion.x2h(strTemp, 36) + i, 36)),
                    Productname);
            }
        }
        private string GetFromartSn(string snval)
        {
            return ("000000" + snval).Substring(("000000" + snval).Length - 6, 6);
        }
        /// <summary>
        /// 关闭Label文档
        /// </summary>
        private void DisposeLppxApp()
        {
            try
            {
                this.doc.Close(false);
                this.lppx.Documents.CloseAll(false);
                this.lppx.Quit();
            }
            catch
            {
            }
        }

       
        /// <summary>
        /// 填充工单号
        /// </summary>
        private void FillWoList()
        {

            dicwo = FrmBLL.ReleaseData.JsonToDictionary(RefWebService_BLL.refWebt_wo_Info_Erp.Instance.Get_Erp_WoList());
           // this.cb_woid.Items.AddRange(arrWoid=RefWebService_BLL.refWebtWoInfo.Instance.GetWoListByState(0));
            this.cb_woid.Items.Clear();
            foreach (KeyValuePair<string, object> item in dicwo)
            {
                this.cb_woid.Items.Add(item.Key);
            }
        }

        /// <summary>
        /// 填充工厂
        /// </summary>
        private void FillFacName()
        {

            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtFacInfo.Instance.GetFacInfo());
            foreach (DataRow dr in dt.Rows)
            {
                this.facNameAndfacCode.Add(dr["facname"].ToString(), dr["facCode"].ToString());
                this.cb_facname.Items.Add(dr["facname"].ToString());
            }
        }

        /// <summary>
        /// 获取3位年加周
        /// </summary>
        private void GetDateTime()
        {
            DateTime dt = RefWebService_BLL.refWebtGetServersTime.Instance.GetServersTime();
            this.ServerTime = dt;
            string str = "00000" + FrmBLL.publicfuntion.h2x(int.Parse(dt.Year.ToString().Substring(2, 2) + FrmBLL.DayWeek.WeekOfYear(dt).ToString().PadLeft(2,'0')), 36);
            str = str.Substring(str.Length - 3, 3);
            this.snhead = str;
            ShowMsg(LogMsgType.Normal, string.Format("获取年周[{0}]", dt.Year.ToString().Substring(2, 2) + FrmBLL.DayWeek.WeekOfYear(dt).ToString().PadLeft(2, '0')));
        }
        /// <summary>
        /// 记录SN区间
        /// </summary>
        private void RunSnRule()
        {
            //12码序列号= 1位工厂编号+两位年+一位月1~C+一位日1~V+5位流水号16进制
            if (this.iasyncresult1 != null && !this.iasyncresult1.IsCompleted)
            {
                this.mFrm.ShowPrgMsg("程序正在进行中,请稍候.1.", MainParent.MsgType.Warning);
                return;
            }
            _snstart = string.Empty;
            _snend = string.Empty;

            int printNum = (int)this.num_printnum.Value - 1;
          //  BLL.Db_Procedure sss = new BLL.Db_Procedure();
            string C_MAXSN = string.Empty;
            string C_WOID = string.Empty;
            string C_ROWID = string.Empty;
          //  sss.PRO_GETSNEND(out C_MAXSN, out C_WOID, out C_ROWID);
            string[] MaxSn = RefWebService_BLL.refWebtWoInfo.Instance.GetMaxSn();
            if (MaxSn[0] == "NO SN")
            {
                MaxSn[2] = "0";
                MaxSn[1] = "xxxxxxxxxx";
                string yyWeek = ServerTime.Year.ToString().Substring(2, 2) + FrmBLL.DayWeek.WeekOfYear(ServerTime).ToString().PadLeft(2, '0');
                MaxSn[0] = FacCode + yyWeek + "000000";
            }
            else
            {
                if (FrmBLL.publicfuntion.x2h(MaxSn[0].Substring(1, MaxSn[0].Length - 7), 36) > FrmBLL.publicfuntion.x2h(this.snhead, 36))
                {
                    this.mFrm.ShowPrgMsg(string.Format("日期错误:{0}<{1}", this.snhead, MaxSn[0].Substring(1, MaxSn[0].Length - 7)), MainParent.MsgType.Warning);
                    return;
                }
            }
            if (FrmBLL.DayWeek.ChkStartWeek(this.ServerTime) && MaxSn[0].Substring(1, MaxSn[0].Length - 7) != this.snhead)
            {
                _snstart = "000001";
            }
            else
            {
                _snstart = FrmBLL.publicfuntion.h2x(FrmBLL.publicfuntion.x2h(MaxSn[0].Substring(MaxSn[0].Length - 6, 6), 36) + 1, 36);
            }
            _snstart = ("000000" + _snstart).Substring(("000000" + _snstart).Length - 6, 6);
            if (MaxSn[1].ToUpper() == this.cb_woid.Text.ToUpper())
            {
                _snend = ("000000" + FrmBLL.publicfuntion.h2x(FrmBLL.publicfuntion.x2h(_snstart, 36) + printNum, 36));

                Dictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("SNPREFIX", string.IsNullOrEmpty(this.tb_snprefix.Text) ? "NA" : tb_snprefix.Text);
                mst.Add("SNPOSTFIX", string.IsNullOrEmpty(this.tb_postfix.Text) ? "NA" : tb_postfix.Text);
                mst.Add("SNEND", string.Format("{0}{1}{2}", FacCode, this.snhead, _snend = _snend.Substring(_snend.Length - 6, 6)));
                mst.Add("VER", "NA");
                mst.Add("REVE", "NA");
                mst.Add("CURRSN", "1");
                mst.Add("ROWID", MaxSn[2]);
                RefWebService_BLL.refWebtWoInfo.Instance.UpdateSnRule(FrmBLL.ReleaseData.DictionaryToJson(mst));
                //RefWebService_BLL.refWebtWoInfo.Instance.UpdateSnRule(new WebServices.tWoInfo.tSnRule()
                //{
                //    snprefix = string.IsNullOrEmpty(this.tb_snprefix.Text) ? "NA" : tb_snprefix.Text,
                //    snpostfix = string.IsNullOrEmpty(this.tb_postfix.Text) ? "NA" : tb_postfix.Text,
                //    snend = string.Format("{0}{1}{2}", FacCode, this.snhead, _snend = _snend.Substring(_snend.Length - 6, 6)),
                //    ver = "NA",
                //    reve = "NA",
                //    currsn ="1"
                //}, MaxSn[2]);
            }
            else
            {
                _snend = "000000" + FrmBLL.publicfuntion.h2x(FrmBLL.publicfuntion.x2h(_snstart, 36) + printNum, 36);

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("WOID",this.cb_woid.Text);
                  dic.Add("SNPREFIX", string.IsNullOrEmpty(this.tb_snprefix.Text) ? "NA" : tb_snprefix.Text);
                  dic.Add("SNPOSTFIX", string.IsNullOrEmpty(this.tb_postfix.Text) ? "NA" : tb_postfix.Text);
                  dic.Add("SNSTART",string.Format("{0}{1}{2}", this.FacCode, this.snhead, ("000000" + _snstart).Substring(("000000" + _snstart).Length - 6, 6)));
                  dic.Add("SNEND",string.Format("{0}{1}{2}", this.FacCode, this.snhead, _snend = _snend.Substring(_snend.Length - 6, 6)));
                  dic.Add("CURRSN","1");
                  dic.Add("REVE","NA");
                  dic.Add("VER","NA");
                  RefWebService_BLL.refWebtWoInfo.Instance.InsetSnRule(FrmBLL.ReleaseData.DictionaryToJson(dic));
                //RefWebService_BLL.refWebtWoInfo.Instance.InsetSnRule(new WebServices.tWoInfo.tSnRule()
                //{
                //    woid = this.cb_woid.Text,
                //    snprefix = string.IsNullOrEmpty(this.tb_snprefix.Text) ? "NA" : tb_snprefix.Text,
                //    snpostfix = string.IsNullOrEmpty(this.tb_postfix.Text) ? "NA" : tb_postfix.Text,
                //    snstart = string.Format("{0}{1}{2}", this.FacCode, this.snhead, ("000000" + _snstart).Substring(("000000" + _snstart).Length - 6, 6)),
                //    snend = string.Format("{0}{1}{2}", this.FacCode, this.snhead, _snend = _snend.Substring(_snend.Length - 6, 6)),
                //     currsn="1",
                //      reve="NA",
                //       ver="NA"
                //});
            }
            this.ShowMsg(LogMsgType.Outgoing, "数据记录成功\n开始执行打印:");
            this.ShowSection(printNum);
            int dp = printNum + 1;
            if (this.rb_printmodel1.Checked)
            {
                Print2(this._snstart, this.FacCode + this.snhead, this.cb_woid.Text + "/" + this.mProductName, printNum + 1);
            }
            else
            {
                delegateLoadlppxApp = new LoadLppxApp(PrintLable);
                iasyncresult1 = delegateLoadlppxApp.BeginInvoke(this.facNameAndfacCode[this.cb_facname.Text] + this.snhead,
                    this._snstart, this.cb_woid.Text + "/" + this.mProductName, dp, null, null);
            }
        }
        /// <summary>
        /// 显示区间信息
        /// </summary>
        /// <param name="num"></param>
        private void ShowSection(int num)
        {
            this.lb_section.Invoke(new EventHandler(delegate
                {
                    this.lb_section.Text = string.Format("开始:{0} -- 结束:{1}",
                       this.FacCode + this.snhead + _snstart,
                       this.FacCode + this.snhead + _snend);
                }));
        }

        private void bt_print_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.cb_facname.Text))
                {
                    this.mFrm.ShowPrgMsg("请选择工厂", MainParent.MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.cb_woid.Text))
                {
                    this.mFrm.ShowPrgMsg("请选择工单", MainParent.MsgType.Warning);
                    return;
                }
                if (!this.ChkWoId(this.cb_woid.Text.Trim()))
                {
                    this.mFrm.ShowPrgMsg("无效工单号", MainParent.MsgType.Warning);
                    this.cb_woid.SelectAll();
                    this.cb_woid.Focus();
                    return;
                }
                if (this.iasyncresult != null && !this.iasyncresult.IsCompleted)
                {
                    this.mFrm.ShowPrgMsg("程序正在初始化中,请稍候..", MainParent.MsgType.Warning);
                    return;
                }

                if (this.iasyncresult1 != null && !this.iasyncresult1.IsCompleted)
                {
                    this.mFrm.ShowPrgMsg("打印程序正在执行中,请稍候..", MainParent.MsgType.Warning);
                    return;
                }

                this.FacCode = this.facNameAndfacCode[this.cb_facname.Text];
                this.EnaControl(false);
                this.RunSnRule();
            }
            catch (Exception ex)
            {
                ShowMsg(LogMsgType.Error, ex.Message);
            }
        }

        private void PrintSFCSn_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DisposeLppxApp();
        }

        private void ShowMsg(LogMsgType msgtype, string msg)
        {
            this.rtb_logmsg.Invoke(new EventHandler(delegate
            {
                rtb_logmsg.TabStop = false;
                rtb_logmsg.SelectedText = string.Empty;
                rtb_logmsg.SelectionFont = new Font(rtb_logmsg.SelectionFont, FontStyle.Bold);
                rtb_logmsg.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtb_logmsg.AppendText(msg + "\n");
                rtb_logmsg.ScrollToCaret();
            }));
        }

        private void rb_printmodel2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb_printmodel2.Checked)
            {
                this.DisposeLppxApp();
                LoadLppx("", "", "", 1);
            }
        }

        private void rb_printmodel1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb_printmodel1.Checked)
            {
                this.DisposeLppxApp();
                LoadLppx("", "", "", 1);
            }
        }

        private void cb_woid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string PartNumber = dicwo[cb_woid.Text].ToString();
              //  DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtProduct.Instance.GetProductInfoByWoId(this.cb_woid.Text));
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtProduct.Instance.GetProductByPartNumber(PartNumber));
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.mProductName = dt.Rows[0]["productname"].ToString();
                    ShowMsg(LogMsgType.Incoming, "产品描述:" + this.mProductName);
                }
                else
                    throw new Exception("系统成品料号重复");
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void cb_woid_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cb_woid.Text))
                return;
            if (!this.ChkWoId(this.cb_woid.Text.Trim()))
            {
                this.mFrm.ShowPrgMsg("无效工单号,请确认工单是否下达", MainParent.MsgType.Warning);
                this.cb_woid.SelectAll();
                this.cb_woid.Focus();
            }
        }

        private bool ChkWoId(string woid)
        {
            //foreach (string item in this.arrWoid)
            //{
            //    if (woid.ToUpper() == item.ToUpper())
            //        return true;
            //}
            if (dicwo.ContainsKey(woid))
                return true;

            return false;
        }

        private void tbotherSn_KeyDown(object sender, KeyEventArgs e)
        {
            if (13 == e.KeyValue)
            {
               // RefWebService_BLL.refWebtKeyPart.Instance.
            }
        }

        private void bt_rPrinter_Click(object sender, EventArgs e)
        {

        }

        private void txt_pring_esn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                esn_reprint();
            }
        }

        private void bt_reprint_Click(object sender, EventArgs e)
        {
            esn_reprint();
        }
        public void esn_reprint()
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt_pring_esn.Text))
                {
                    this.mFrm.ShowPrgMsg("请输入需要补印的ESN", MainParent.MsgType.Warning);
                    return;
                }
                string ESN_NO = this.txt_pring_esn.Text.Trim().ToUpper();

                if (string.IsNullOrEmpty(this.cb_woid.Text))
                {
                    this.mFrm.ShowPrgMsg("请选择工单", MainParent.MsgType.Warning);
                    return;
                }
                if (!this.ChkWoId(this.cb_woid.Text.Trim()))
                {
                    this.mFrm.ShowPrgMsg("无效工单号", MainParent.MsgType.Warning);
                    this.cb_woid.SelectAll();
                    this.cb_woid.Focus();
                    return;
                }
                if (!RefWebService_BLL.refWebtWoInfo.Instance.CheckEsnRule(this.cb_woid.Text.Trim(), ESN_NO))
                {
                    this.mFrm.ShowPrgMsg("工单和所补印的ESN不一致，或者ESN不存在，请确定！！", MainParent.MsgType.Warning);
                    this.txt_pring_esn.SelectAll();
                    this.txt_pring_esn.Focus();
                    return;
                }
                if (this.iasyncresult != null && !this.iasyncresult.IsCompleted)
                {
                    this.mFrm.ShowPrgMsg("程序正在初始化中,请稍候..", MainParent.MsgType.Warning);
                    return;
                }
                if (this.iasyncresult1 != null && !this.iasyncresult1.IsCompleted)
                {
                    this.mFrm.ShowPrgMsg("打印程序正在执行中,请稍候..", MainParent.MsgType.Warning);
                    return;
                }
                this.FacCode = this.facNameAndfacCode[this.cb_facname.Text];
                this.EnaControl(false);
                //获取去掉1位厂别 3位年周后的流水号
                //string _ESN_start = ("000000" + ESN_NO.Substring(ESN_NO.Length - 5)).Substring(("000000" + ESN_NO.Substring(ESN_NO.Length - 5)).Length - 6, 6);
                string _ESN_start = ESN_NO.Substring(4, ESN_NO.Length - 4);
                Print2(_ESN_start, ESN_NO.Substring(0, 1) + ESN_NO.Substring(1, 3), this.cb_woid.Text + "/" + this.mProductName, 1);
                txt_pring_esn.Text = "";
                this.ShowMsg(LogMsgType.Outgoing, string.Format("序列号{0} 到 {0}已经输出", ESN_NO));
            }
            catch (Exception ex)
            {
                ShowMsg(LogMsgType.Error, ex.Message);
            }
        }
    }
}
