using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.Drawing.Drawing2D;
using ZedGraph;

namespace SFIS_V2
{
    public partial class QualityManagement : Office2007Form ///Form
    {
        public QualityManagement(MainParent sInfo)
        {
            InitializeComponent();
            sMain = sInfo;
        }
        MainParent sMain;

        #region 公共变量
        ///// <summary>
        ///// 产品料号
        ///// </summary>
        //public string SelectMode;
        ///// <summary>
        ///// 工单
        ///// </summary>
        //public string SelectwoId;
        ///// <summary>
        ///// 线别
        ///// </summary>
        //public string SelectLine;
        ///// <summary>
        ///// 途程
        ///// </summary>
        //public string SelectRoute;

        public string MODEL = "MODEL";
        public string LINE = "LINE";
        public string woId = "WO";
        public string ROUTE = "ROUTE";
        public string sortname = "SORTNAME";

        /// <summary>
        /// 加载产品料号是否完成标记
        /// </summary>
        bool ProductFlag = false;
        /// <summary>
        /// 加载工单是否完成标记
        /// </summary>
        bool woflag = false;
       /// <summary>
        /// 加载线体是否完成标记
       /// </summary>
        bool LineFlag = false;
        /// <summary>
        /// 加载途程是否完成标记
        /// </summary>
        bool CraftFlag = false;

        Dictionary<string, string> Allcraft = new Dictionary<string, string>();
        Dictionary<string, string> AllProduct = new Dictionary<string, string>();
        Dictionary<string, string> AllWoPart = new Dictionary<string, string>();
        Dictionary<string, string> AllErrorCode = new Dictionary<string, string>();
        Dictionary<string, string> AllReaconCode = new Dictionary<string, string>();
        #endregion

        /// <summary>
        /// 线体信息表
        /// </summary>
        DataTable dtLine = null;
        /// <summary>
        /// 产品信息表
        /// </summary>
        DataTable dtProduct = null;
        /// <summary>
        /// 工单表
        /// </summary>
        DataTable dtwoList = null;
        /// <summary>
        /// 途程表
        /// </summary>
        DataTable dtCraft = null;

        private void FillListBox(string sItem,string strValue)
        {
            this.listBox1.Invoke(new EventHandler(delegate
            {
                //if (this.listBox1.Items.Count > 0)
                //{
                int lsItem = -1;
                if ((lsItem = this.listBox1.FindString(sItem)) != -1)
                {
                    this.listBox1.Items.RemoveAt(lsItem);
                }
                this.listBox1.Items.Add(sItem + "  " + strValue);

                // }
            }));
        }
        private void QualityManagement_Load(object sender, EventArgs e)
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

            for (int i=0;i<24;i++)
            {
                combStime.Items.Add(i.ToString().PadLeft(2, '0') + ":30");
                combEtime.Items.Add(i.ToString().PadLeft(2, '0') + ":30");
            }
            
            combStime.SelectedIndex=0;////.Text = combStime.SelectedIndex;
            combEtime.SelectedIndex=0;//.Text = combEtime.SelectedItem = -1;

            GetProduct = new GetProductList(GettProductList);
            GetProduct.BeginInvoke(null,null);

            GetwoId = new GetwoIdList(GettwoIdList);
            GetwoId.BeginInvoke(null,null);

            GetLine = new GetLineList(GettLineList);
            GetLine.BeginInvoke(null,null);

            GetCraft = new GetCraftList(GettCraftList);
            GetCraft.BeginInvoke(null,null);

            GetErrorCode = new GetErrorCodeList(GettErrorCode);
            GetErrorCode.BeginInvoke(null,null);

            GetReasonCode = new GetReasonCodeList(GettReasonCode);
            GetReasonCode.BeginInvoke(null,null);

          //  panelEx4.Right = panelEx3.Width * (2 / 3);
            imbt_Checktime.Left = panelEx4.Width + 150;

            #region 单元格交替颜色
            this.dgvYieldRate.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvYieldRate.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            this.dgvDefectAnalysis.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvDefectAnalysis.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            this.dgvRepairAnalysis.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvRepairAnalysis.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            #endregion
        }

        #region 加载基本资料到Access

        /// <summary>
        /// 获取所有产品料号
        /// </summary>
        private delegate void GetProductList();
        GetProductList GetProduct;
        private void GettProductList()
        {
            this.ProductFlag = false;

           dtProduct = FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetAllProduct());         
          ///  FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
           // ass.ExecuteOracleCommand("delete from tProduct");
           // string err = ass.ExecuteAccessCommandProduct(dt, "tProduct", "partnumber,sortname,productname,productcolor,productdesc,other", "partnumber");
           //this.FillListBox("Product Loading " + err, dt.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString());

           //int count = 0;
           //int rowcount = dt.Rows.Count;
           //foreach (DataRow dr in dt.Rows)
           //{
           //    count++;
           //    AllProduct.Add(dr[0].ToString().Trim().ToUpper(), dr[2].ToString());
           //    string sql = string.Format("insert into tProduct(partnumber,sortname,productname,productcolor,productdesc,other) values('{0}','{1}','{2}','{3}','{4}','{5}')",
           //     dr[0].ToString(),
           //     dr[1].ToString(),
           //     dr[2].ToString(),
           //     dr[3].ToString(),
           //     dr[4].ToString(),
           //     dr[5].ToString());
           //    ass.ExecuteOracleCommand(sql);
           //    this.FillListBox("Product Loading", count.ToString() + "/" + rowcount.ToString());
           //}
            this.ProductFlag = true;
        }

        /// <summary>
        /// 获取所有工单
        /// </summary>
        private delegate void GetwoIdList();
        GetwoIdList GetwoId;
        private void GettwoIdList()
        {
            this.woflag = false;
            dtwoList =FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(null,null,"WOID"));
            //FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            //ass.ExecuteOracleCommand("delete from tWorkOrderInfo");
            //string sColnum = "woId,poId,qty,wostate,userId,partnumber,bomver,inputgroup,outputgroup,routgroupId,bomnumber,wotype";
            //string err=ass.ExecuteAccessCommandTable(woList, "tWorkOrderInfo", sColnum, "woId");
            //this.FillListBox("WoList Loading " + err, woList.Rows.Count.ToString() + "/" + woList.Rows.Count.ToString());

            
            //int count = 0;
            //int rowcount = woList.Rows.Count;
            //foreach (DataRow dr in woList.Rows)
            //{
            //    count++;
            //    AllWoPart.Add(dr[0].ToString(), dr[5].ToString());
            //    //string sql = string.Format("insert into tWorkOrderInfo(woId,poId,qty,wostate,userId,partnumber,bomver,inputgroup,outputgroup,routgroupId,bomnumber,wotype) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
            //    //dr[0].ToString(),
            //    // dr[1].ToString(),
            //    // dr[2].ToString(),
            //    // dr[3].ToString(),
            //    // dr[4].ToString(),
            //    // dr[5].ToString(),
            //    // dr[6].ToString(),
            //    // dr[7].ToString(),
            //    // dr[8].ToString(),
            //    // dr[9].ToString(),
            //    // dr[10].ToString(),
            //    // dr[11].ToString());
            //    //ass.ExecuteOracleCommand(sql);
            //    this.FillListBox("WoList Loading", count.ToString() + "/" + rowcount.ToString());
            //}
            this.woflag = true;
        }

        /// <summary>
        /// 获取所有线体
        /// </summary>
        private delegate void GetLineList();
        GetLineList GetLine;
        private void GettLineList()
        {
            this.LineFlag = false;

            dtLine = FrmBLL.ReleaseData.arrByteToDataTable(refWebtLineInfo.Instance.GetAllLineInfo());       

          //  FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
          //  ass.ExecuteOracleCommand("delete from tLineInfo");
          // // string sColnum = "lineId,linedesc,startIpAddr,endIpAddr,wsId,userId,plotId";
          // // string err= ass.ExecuteAccessCommandTable(dt, "tLineInfo", sColnum, "lineId");
          ////  this.FillListBox("LineList Loading " + err, dt.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString());

          //  int count = 0;
          //  int rowcount = dt.Rows.Count;
          //  foreach (DataRow dr in dt.Rows)
          //  {
          //      count++;
          //      string sql = string.Format("insert into tLineInfo (lineId,linedesc,startIpAddr,endIpAddr,wsId,userId,plotId) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
          //       dr[0].ToString(),
          //       dr[1].ToString(),
          //       dr[2].ToString(),
          //       dr[3].ToString(),
          //       dr[4].ToString(),
          //       dr[5].ToString(),
          //       dr[6].ToString());
          //      ass.ExecuteOracleCommand(sql);
          //      this.FillListBox("LineList Loading", count.ToString() + "/" + rowcount.ToString());
          //  }
            this.LineFlag = true;
        }


        /// <summary>
        /// 获取所有途程
        /// </summary>
        private delegate void GetCraftList();
        GetCraftList GetCraft;
        private void GettCraftList()
        {
            this.CraftFlag = false;

            dtCraft = FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo());
           
            //FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            //ass.ExecuteOracleCommand("delete from tCraftInfo");
            ////string sColnum = "craftId,craftname,craftparameterurl,beworkseg";
            ////string err= ass.ExecuteAccessCommandTable(dt, "tCraftInfo", sColnum, "craftId");
            ////this.FillListBox("CraftList Loading " + err, dt.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString());

            //int count = 0;
            //int rowcount = dt.Rows.Count;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    count++;
            //    Allcraft.Add(dr[0].ToString().Trim().ToUpper(), dr[1].ToString());
            //    string sql = string.Format("insert into tCraftInfo (craftId,craftname,craftparameterurl,beworkseg) values('{0}','{1}','{2}','{3}')",
            //     dr[0].ToString(),
            //     dr[1].ToString(),
            //     dr[2].ToString(),
            //     dr[3].ToString());
            //    ass.ExecuteOracleCommand(sql);
            //    this.FillListBox("CraftList Loading", count.ToString() + "/" + rowcount.ToString());
            //}
            this.CraftFlag = true;
        }

        /// <summary>  //20130129
        /// 获取不良代码
        /// </summary>
        private delegate void GetErrorCodeList();
        GetErrorCodeList GetErrorCode;
        bool ECFalg = false;
        private void GettErrorCode()
        {
          /*  this.ECFalg = false;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtErrorCode.Instance.GetErrorCode());

            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            ass.ExecuteOracleCommand("delete from tErrorCode");
            //string sColnum = "ErrorCode,ErrorDesc,ErrorDesc2,Recdate";
            //string err=  ass.ExecuteAccessCommandTable(dt, "tErrorCode", sColnum, "ErrorCode");
            //this.FillListBox("ErrorCode Loading " + err, dt.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString());
           
            int count = 0;
            int rowcount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                count++;
                AllErrorCode.Add(dr[0].ToString().Trim().ToUpper(), dr[1].ToString());
                string sql = string.Format("insert into tErrorCode (ErrorCode,ErrorDesc,ErrorDesc2,Recdate) values('{0}','{1}','{2}','{3}')",
                 dr[0].ToString(),
                 dr[1].ToString(),
                 dr[2].ToString(),
                 dr[3].ToString());
                ass.ExecuteOracleCommand(sql);
                this.FillListBox("ErrorCode Loading", count.ToString() + "/" + rowcount.ToString());
            }

            this.ECFalg = true;*/
        }

        /// <summary>  //20130129
        /// 获取原因代码
        /// </summary>
        private delegate void GetReasonCodeList();
        GetReasonCodeList GetReasonCode;
        bool RCFalg = false;
        private void GettReasonCode()
        {
          /*  this.RCFalg = false;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtReasonCode.Instance.GetReasonCode());
            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            ass.ExecuteOracleCommand("delete from tReasonCode");
           // string sColnum = "ReasonCode,ReasonType,ReasonDesc,ReasonDesc2,DutyStation,RecDate";
           //string err= ass.ExecuteAccessCommandTable(dt, "tReasonCode", sColnum, "ReasonCode");
          //  this.FillListBox("ReasonCode Loading "+err, dt.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString());
            int count = 0;
            int rowcount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                count++;
                AllReaconCode.Add(dr[0].ToString().Trim().ToUpper(), dr[2].ToString());
                string sql = string.Format("insert into tReasonCode (ReasonCode,ReasonType,ReasonDesc,ReasonDesc2,DutyStation,RecDate) values('{0}','{1}','{2}','{3}','{4}','{5}')",
                 dr[0].ToString(),
                 dr[1].ToString(),
                 dr[2].ToString(),
                 dr[3].ToString(),
                 dr[4].ToString(),
                 dr[5].ToString());
                ass.ExecuteOracleCommand(sql);
                this.FillListBox("ReasonCode Loading", count.ToString() + "/" + rowcount.ToString());
            }
            this.RCFalg = true;
            */
        }

        #endregion


        private void tlmwo_Click(object sender, EventArgs e)
        {
          //  tlmwo.Checked = 
        }

        private void imbt_model_Click(object sender, EventArgs e)
        {


            SelectDataListBox sd = new SelectDataListBox(this, MODEL, dtProduct);
            sd.ShowDialog();
            
        }

        private void imbt_wo_Click(object sender, EventArgs e)
        {
            SelectDataListBox sd = new SelectDataListBox(this, woId,dtwoList);
           sd.ShowDialog();
        }

        private void imbt_line_Click(object sender, EventArgs e)
        {
            SelectDataListBox sd = new SelectDataListBox(this, LINE,dtLine);
            sd.ShowDialog();
        }

        private void imbt_route_Click(object sender, EventArgs e)
        {
            SelectDataListBox sd = new SelectDataListBox(this, ROUTE,dtCraft);
            sd.ShowDialog();
        }

        /// <summary>
        /// 查询间隔时间
        /// </summary>
        int Qtime = 0;
        private void imbt_Checktime_Click(object sender, EventArgs e)
        {       
            //if (!ProductFlag)
            //{
            //    sMain.ShowPrgMsg("正在加载产品料号资料,请稍等.....", MainParent.MsgType.Incoming);
            //    return;
            //}
            //if (!woflag)
            //{
            //    sMain.ShowPrgMsg("正在加载工单资料,请稍等.....", MainParent.MsgType.Incoming);
            //    return;
            //}
            //if (!LineFlag)
            //{
            //    sMain.ShowPrgMsg("正在加载线体资料,请稍等.....", MainParent.MsgType.Incoming);
            //    return;
            //}
            //if (!CraftFlag)
            //{
            //    sMain.ShowPrgMsg("正在加载途程资料,请稍等.....", MainParent.MsgType.Incoming);
            //    return;
            //}

            if (dtpSdate.Value.Date > dtpEdate.Value.Date)
            {
                sMain.ShowPrgMsg("开始日期不能大于结束日期",MainParent.MsgType.Incoming);
                return;
            }
            if (dtpSdate.Value.Date == dtpEdate.Value.Date)
            {
         
                if ((Convert.ToInt32(combStime.Text.Substring(0,2))) > (Convert.ToInt32(combEtime.Text.Substring(0,2))))
                {
                    sMain.ShowPrgMsg("选择相同日期,开始时间不能大于结束时间", MainParent.MsgType.Incoming);
                    return;
                }
            }

            if (timer1.Enabled)
            {
                sMain.ShowPrgMsg("刷新时间不能太频繁,请等待:" + Qtime.ToString() + " 秒后再试", MainParent.MsgType.Incoming);
                return;
            }
            Qtime = 10;
            timer1.Enabled = true;

            string StartTime = dtpSdate.Value.ToString("yyyyMMdd") + combStime.Text.Substring(0,2);
            string etime=combEtime.Text.Substring(0,2);
            if (etime == "00")
            {
                etime = "23";
            }
            else
            {
                etime =(Convert.ToInt32(etime) -1).ToString();
            }
            //string EndTime = dtpEdate.Value.ToString("yyyyMMdd") + etime.PadLeft(2,'0');
            //GetSTNRec = new GetSTNRecToAccess(GetStationRecToAccess);
            //GetSTNRec.BeginInvoke(StartTime, EndTime, null, null);

            //GetRepair = new DownloadRepair(DownLoadRpairIN);
            //GetRepair.BeginInvoke(StartTime, EndTime,null, null);

            //GetRepair = new DownloadRepair(DownLoadRpairOUT);
            //GetRepair.BeginInvoke(StartTime, EndTime, null, null);

            this.imbt_Checktime.Enabled = false;
        }

        private delegate void DownloadRepair(string StartTime, string EndTime);
        DownloadRepair GetRepair;
        bool DowloadFlagRepairIN = false;
        bool DowloadFlagRepairOUT = false;

        private void DownLoadRpairIN(string StartTime, string EndTime)
        {
          /*  this.DowloadFlagRepairIN = false;

            string flag = "IN";
            DataTable dt = null;//FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.DowloadRepairInfo(StartTime, EndTime, flag));
            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            ass.ExecuteOracleCommand(string.Format("delete from tRepairInfo where FLAG='{0}'",flag));
            int count = 0;
            int rowcount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                count++;
                string sql = string.Format("insert into tRepairInfo(ErrorCode,ErrorDesc,ReasonCode,ReasonDesc,esn,woId,partnumber,ProductName,CraftId,CraftName,lineId,Location,TClassDate,TCLASS,TWorkSection,RClassDate,RCLASS,RWorkSection,FLAG) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')",
                     dr[0].ToString(),
                     AllErrorCode[dr[0].ToString().Trim().ToUpper()],
                     dr[1].ToString(),
                     AllReaconCode[dr[1].ToString().Trim().ToUpper()],
                     dr[2].ToString(),
                     dr[3].ToString(),
                     dr[4].ToString(),
                     AllProduct[dr[4].ToString().Trim().ToUpper()],
                     dr[5].ToString(),
                     dr[5].ToString().Trim().ToUpper(),
                     dr[6].ToString(),
                     dr[7].ToString(),
                     dr[8].ToString(),
                     dr[9].ToString(),
                     dr[10].ToString(),
                     dr[11].ToString(),
                      dr[12].ToString(),
                      dr[13].ToString(),
                     flag);
                    ass.ExecuteOracleCommand(sql);
                    this.FillListBox("RepairIn Loading", count.ToString() + "/" + rowcount.ToString());
            }
            this.DowloadFlagRepairIN = true;*/
        }

        private void DownLoadRpairOUT(string StartTime, string EndTime)
        {
          /*  this.DowloadFlagRepairOUT = false;

            string flag = "OUT";
            DataTable dt = null;//FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.DowloadRepairInfo(StartTime, EndTime, flag));
            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            ass.ExecuteOracleCommand(string.Format("delete from tRepairInfo where FLAG='{0}'",flag));
            int count = 0;
            int rowcount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                count++;
                string sql = string.Format("insert into tRepairInfo(ErrorCode,ErrorDesc,ReasonCode,ReasonDesc,esn,woId,partnumber,ProductName,CraftId,CraftName,lineId,Location,TClassDate,TCLASS,TWorkSection,RClassDate,RCLASS,RWorkSection,FLAG) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')",
                  dr[0].ToString(),
                 AllErrorCode[dr[0].ToString().Trim().ToUpper()],
                 dr[1].ToString(),
                 AllReaconCode[dr[1].ToString().Trim().ToUpper()],
                 dr[2].ToString(),
                 dr[3].ToString(),
                 dr[4].ToString(),
                 AllProduct[dr[4].ToString().Trim().ToUpper()],
                 dr[5].ToString(),
                 dr[5].ToString().Trim().ToUpper(),
                 dr[6].ToString(),
                 dr[7].ToString(),
                 dr[8].ToString(),
                 dr[9].ToString(),
                 dr[10].ToString(),
                 dr[11].ToString(),
                 dr[12].ToString(),
                 dr[13].ToString(),
                 flag);
                ass.ExecuteOracleCommand(sql);
                this.FillListBox("RepairOut Loading", count.ToString() + "/" + rowcount.ToString());
            }
            this.DowloadFlagRepairOUT = true;*/
        }

        private delegate void GetSTNRecToAccess(string StartTime, string EndTime);
        GetSTNRecToAccess GetSTNRec;
        private void RunProgBar(int ival,int maxVal)
        {
            this.progressBarX1.Invoke(new EventHandler(delegate {
                if (maxVal != this.progressBarX1.Maximum)
                    this.progressBarX1.Maximum = maxVal;
                this.progressBarX1.Value = ival;
                this.progressBarX1.Text = string.Format("{0}/{1}", ival, maxVal);
                this.progressBarX1.Refresh();
            }));
        }
        bool GetStnRecFlag = false;
        private void GetStationRecToAccess(string StartTime,string EndTime )
        {
          /*  this.GetStnRecFlag = false;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStationRec.Instance.GetStationRec(StartTime, EndTime));
            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            ass.ExecuteOracleCommand("delete from tStationRec");
            int rowscount = dt.Rows.Count;
            int i = 0;  
            

            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    i++;
                    string sql = string.Format("insert into tStationRec(woId,partnumber,ProductName,craftId,craftname,workDate,worksection,class,classDate,lineId,passQty,failQty,rPassQty,rFailQty,flag) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')",
                     dr[0].ToString(),
                     AllWoPart[dr[0].ToString()].ToString(),
                     AllProduct[AllWoPart[dr[0].ToString()].ToString()],
                     dr[1].ToString(),
                     dr[1].ToString(),
                     dr[2].ToString(),
                     dr[3].ToString(),
                     dr[4].ToString(),
                     dr[5].ToString(),
                     dr[6].ToString(),
                     dr[7].ToString(),
                     dr[8].ToString(),
                     dr[9].ToString(),
                     dr[10].ToString(),
                     dr[11].ToString());
                    ass.ExecuteOracleCommand(sql);


                 //   ass.ExecuteAccessCommandTable();
                    this.RunProgBar(i, rowscount);
                }
                catch
                {
                    MessageBox.Show("工单加载失败：" + dr[0].ToString());
                }
            }
            this.GetStnRecFlag = true;*/
        }

        private void imbt_yield_Click(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (GetStnRecFlag)
            //{
                Qtime -= 1;
                if (Qtime == 0)
                {
                    timer1.Enabled = false;
                    this.imbt_Checktime.Enabled = true;
                }
           // }
        }

        private void dgvYieldRate_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvYieldRate.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }

        }

        private void dgvDefectAnalysis_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvDefectAnalysis.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }

        }

        private void dgvRepairAnalysis_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvRepairAnalysis.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }
        }

        private void tabControl1_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            imbt_chart.Enabled = false;
            switch (tabControl1.SelectedTabIndex)
            {

                case 0:                 
                    ShowFlag =1;  //良率查询
                    break;
                case 1:
                    ShowFlag = 2; //产品缺陷分析
                    imbt_chart.Enabled = true;
                    break;
                case 2:
                    ShowFlag = 3; //维修分析
                    break;
                case 3:
                    ShowFlag = 4;
                    break;
            }
        }

       
   /// <summary>
   /// 查询标记 1 良率查询,2 产品缺陷分析,3产品维修分析,4 待定
   /// </summary>
        int ShowFlag = 1;
  
        private void imbt_Refresh_Click(object sender, EventArgs e)
        {
            if (Check_Select_Item())
            {
                if (dtpSdate.Value.Date > dtpEdate.Value.Date)
                {
                    sMain.ShowPrgMsg("开始日期不能大于结束日期", MainParent.MsgType.Incoming);
                    return;
                }
                if (dtpSdate.Value.Date == dtpEdate.Value.Date)
                {

                    if ((Convert.ToInt32(combStime.Text.Substring(0, 2))) > (Convert.ToInt32(combEtime.Text.Substring(0, 2))))
                    {
                        sMain.ShowPrgMsg("选择相同日期,开始时间不能大于结束时间", MainParent.MsgType.Incoming);
                        return;
                    }
                }

                if (timer1.Enabled)
                {
                    sMain.ShowPrgMsg("刷新时间不能太频繁,请等待:" + Qtime.ToString() + " 秒后再试", MainParent.MsgType.Incoming);
                    return;
                }
                Qtime = 10;
                timer1.Enabled = true;

                if (ShowFlag == 1)
                {
                    GetYieldRateShow();
                }
                if (ShowFlag == 2)
                {
                    GetdefectAnalysisShow();
                }
                if (ShowFlag == 3)
                {
                    GetRepairAnalysis();
                }
            }
        }
        private void imbt_toExcel_Click(object sender, EventArgs e)
        {
            if (ShowFlag == 1)
            {
                if (dgvYieldRate.Rows.Count != 0)
                {
                    FrmBLL.DataGridViewToExcel.DataToExcel(dgvYieldRate);
                    sMain.ShowPrgMsg("导出Excel完成", MainParent.MsgType.Error);
                }
                else
                {
                    sMain.ShowPrgMsg("没有资料可以导出", MainParent.MsgType.Error);
                }
            }
            if (ShowFlag == 2)
            {

                if (dgvYieldRate.Rows.Count != 0)
                {
                    FrmBLL.DataGridViewToExcel.DataToExcel(dgvDefectAnalysis);
                    sMain.ShowPrgMsg("导出Excel完成", MainParent.MsgType.Error);
                }
                else
                {
                    sMain.ShowPrgMsg("没有资料可以导出", MainParent.MsgType.Error);
                }

            }
            if (ShowFlag == 2)
            {

                if (dgvYieldRate.Rows.Count != 0)
                {
                    FrmBLL.DataGridViewToExcel.DataToExcel(dgvRepairAnalysis);
                    sMain.ShowPrgMsg("导出Excel完成", MainParent.MsgType.Error);
                }
                else
                {
                    sMain.ShowPrgMsg("没有资料可以导出", MainParent.MsgType.Error);
                }

            }
        }


        /// <summary>
        /// 良率报表
        /// </summary>
        private void GetYieldRateShow()    //良率
        {          

            string StartTime = dtpSdate.Value.ToString("yyyyMMdd") + combStime.Text.Substring(0, 2);
            if (combStime.Text.Substring(0, 2) == "00")
            {
                StartTime = dtpSdate.Value.AddDays(-1).ToString("yyyyMMdd") + "23";

            }
            string etime = combEtime.Text.Substring(0, 2);
            if (etime == "00")
            {
                etime = "23";
            }
            else
            {
                etime = (Convert.ToInt32(etime) - 1).ToString();
            }
            string EndTime = dtpEdate.Value.ToString("yyyyMMdd") + etime.PadLeft(2, '0');
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("WOID", Analytical_ListBox(Listwo, 0));
            dic.Add("PARTNUMBER", Analytical_ListBox(ListMode, 1));
            dic.Add("LINEID", Analytical_ListBox(ListLine, 0));
            dic.Add("CRAFTID", Analytical_ListBox(ListRoute, 0));
            dic.Add("SHOW_WOID", tlmwo.Checked);
            dic.Add("SHOW_LINEID", tlmline.Checked);
            dic.Add("SHOW_PARTNUMBER", tlmpartname.Checked);
            dic.Add("SHOW_PRODUCTNAME", tlmmode.Checked);
            dic.Add("SHOW_WORKDATE", tlmworkdate.Checked);
            dic.Add("SHOW_CLASS", tlmclass.Checked);
            dic.Add("SHOW_CLASSDATE", tlmclassdate.Checked);
            dic.Add("SHOW_CRAFTID", tlmroute.Checked);
            dic.Add("SHOW_RPASSQTY", tlmrepass.Checked);
            dic.Add("SHOW_RFAILQTY", tlmrefail.Checked);
            dic.Add("STARTTIME", StartTime);
            dic.Add("ENDTIME", EndTime);
            DataTable dts = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStationRec.Instance.Get_YieldRate(FrmBLL.ReleaseData.DictionaryToJson(dic)));

            dts.Columns.Add("YieldRate", typeof(string));
            foreach (DataRow dr in dts.Rows)
            {
                double x = Convert.ToDouble(dr[dts.Columns.Count - 3].ToString());
                double y = Convert.ToDouble(dr[dts.Columns.Count - 3].ToString()) + Convert.ToDouble(dr[dts.Columns.Count - 2].ToString());
                if (y > 0)
                    dr[dts.Columns.Count - 1] = Math.Round((decimal)(x / y * 100), 2);
                else
                    dr[dts.Columns.Count - 1] = Math.Round((decimal)Convert.ToInt32("100"), 2);

            }
            UpdateDataTableColoumnsName(dts);
            dgvYieldRate.DataSource = null;
            dgvYieldRate.DataSource = dts;
        }
        /// <summary>
        /// 缺陷分析报表
        /// </summary>
        private void GetdefectAnalysisShow()  //缺陷分析
        {
            string StartTime = dtpSdate.Value.ToString("yyyyMMdd") + combStime.Text.Substring(0, 2);
            if (combStime.Text.Substring(0, 2) == "00")
            {
                StartTime = dtpSdate.Value.AddDays(-1).ToString("yyyyMMdd") + "23";
            }

            string etime = combEtime.Text.Substring(0, 2);
            if (etime == "00")
            {
                etime = "23";
            }
            else
            {
                etime = (Convert.ToInt32(etime) - 1).ToString();
            }
            string EndTime = dtpEdate.Value.ToString("yyyyMMdd") + etime.PadLeft(2, '0');

            dgvDefectAnalysis.DataSource = null;

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("SHOW_PARTNUMBER", d_partnumber.Checked);
            dic.Add("SHOW_PRODUCTNAME", d_productname.Checked);
            dic.Add("SHOW_WOID", d_wo.Checked);
            dic.Add("SHOW_LINEID", d_line.Checked);
            dic.Add("SHOW_CRAFTID", d_route.Checked);
            dic.Add("SHOW_TCLASSDATE", d_classdate.Checked);
            dic.Add("WOID", Analytical_ListBox(Listwo, 0));
            dic.Add("PARTNUMBER", Analytical_ListBox(ListMode, 1));
            dic.Add("CRAFTID", Analytical_ListBox(ListRoute, 0));
            dic.Add("LINEID", Analytical_ListBox(ListLine, 0));
            dic.Add("STARTTIME", StartTime);
            dic.Add("ENDTIME", EndTime);

            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.DowloadRepairInfo(FrmBLL.ReleaseData.DictionaryToJson(dic), "IN"));
         
            int Rate = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Rate += Convert.ToInt32(dr[dt.Columns.Count - 1].ToString());
            }
            UpdateDataTableColoumnsName(dt);

            dt.Columns.Add("Rate(%)", System.Type.GetType("System.String"));
            for (int x = 0; x < dt.Rows.Count; x++)
            {

                dt.Rows[x]["Rate(%)"] = Math.Round(((decimal)Convert.ToInt32(dt.Rows[x]["数量"].ToString()) / Rate) * 100, 2);
            }

            dt.DefaultView.Sort = "Rate(%) desc";
            DataTable dts = dt.DefaultView.ToTable();
            dgvDefectAnalysis.DataSource = dts;
            ShowGraph(dts, "缺陷分析");

        }
        /// <summary>
        /// 维修分析报表
        /// </summary>
        private void GetRepairAnalysis()   //维修转出
        {
            string StartTime = dtpSdate.Value.ToString("yyyyMMdd") + combStime.Text.Substring(0, 2);

            if (combStime.Text.Substring(0, 2) == "00")
            {
                StartTime = dtpSdate.Value.AddDays(-1).ToString("yyyyMMdd") + "23";
            }

            string etime = combEtime.Text.Substring(0, 2);
            if (etime == "00")
            {
                etime = "23";
            }
            else
            {
                etime = (Convert.ToInt32(etime) - 1).ToString();
            }
            string EndTime = dtpEdate.Value.ToString("yyyyMMdd") + etime.PadLeft(2, '0');
            dgvRepairAnalysis.DataSource = null;


            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("SHOW_PARTNUMBER", R_Partnumber.Checked);
            dic.Add("SHOW_PRODUCTNAME", R_ProductName.Checked);
            dic.Add("SHOW_WOID", R_wo.Checked);
            dic.Add("SHOW_LINEID", R_line.Checked);
            dic.Add("SHOW_CRAFTID", R_Craft.Checked);
            dic.Add("SHOW_TCLASSDATE", R_workdate.Checked);
            dic.Add("WOID", Analytical_ListBox(Listwo, 0));
            dic.Add("PARTNUMBER", Analytical_ListBox(ListMode, 1));
            dic.Add("CRAFTID", Analytical_ListBox(ListRoute, 0));
            dic.Add("LINEID", Analytical_ListBox(ListLine, 0));
            dic.Add("STARTTIME", StartTime);
            dic.Add("ENDTIME", EndTime);

            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.DowloadRepairInfo(FrmBLL.ReleaseData.DictionaryToJson(dic), "OUT"));
          
            int Rate = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Rate += Convert.ToInt32(dr[dt.Columns.Count - 1].ToString());
            }
            UpdateDataTableColoumnsName(dt);

            dt.Columns.Add("Rate(%)", System.Type.GetType("System.String"));
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                dt.Rows[x]["Rate(%)"] = Math.Round(((decimal)Convert.ToInt32(dt.Rows[x]["数量"].ToString()) / Rate) * 100, 2);
            }

            dt.DefaultView.Sort = "Rate(%) desc";
            DataTable dts = dt.DefaultView.ToTable();

            dgvRepairAnalysis.DataSource = dts;

        }


        #region 更改栏位名称
        private void UpdateDataTableColoumnsName(DataTable dt)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ToString().ToUpper() == "WORKDATE")
                {
                    dt.Columns[i].ColumnName = "工作日期";
                }

                if (dt.Columns[i].ToString().ToUpper() == "CRAFTID")
                {
                    dt.Columns[i].ColumnName = "途程";
                }

                if (dt.Columns[i].ToString().ToUpper() == "WOID")
                {
                    dt.Columns[i].ColumnName = "工单";
                }

                if (dt.Columns[i].ToString().ToUpper() == "PARTNUMBER")
                {
                    dt.Columns[i].ColumnName = "产品料号";
                }

                if (dt.Columns[i].ToString().ToUpper() == "PRODUCTNAME")
                {
                    dt.Columns[i].ColumnName = "产品名称";
                }

                if (dt.Columns[i].ToString().ToUpper() == "YIELDRATE")
                {
                    dt.Columns[i].ColumnName = "良率(%)";
                }

                if (dt.Columns[i].ToString().ToUpper() == "LINEID")
                {
                    dt.Columns[i].ColumnName = "线别";
                }

                if (dt.Columns[i].ToString().ToUpper() == "RPASSQTY")
                {
                    dt.Columns[i].ColumnName = "RePASS";
                }

                if (dt.Columns[i].ToString().ToUpper() == "RFAILQTY")
                {
                    dt.Columns[i].ColumnName = "ReFAIL";
                }

                if (dt.Columns[i].ToString().ToUpper() == "CLASS")
                {
                    dt.Columns[i].ColumnName = "班别";
                }

                if (dt.Columns[i].ToString().ToUpper() == "CLASSDATE")
                {
                    dt.Columns[i].ColumnName = "班别日期";
                }
                if (dt.Columns[i].ToString().ToUpper() == "TCLASSDATE")
                {
                    dt.Columns[i].ColumnName = "转入日期";
                }    

                if (dt.Columns[i].ToString().ToUpper() == "QTY")
                {
                    dt.Columns[i].ColumnName = "数量";
                }                    

                if (dt.Columns[i].ToString().ToUpper() == "ERRORCODE")
                {
                    dt.Columns[i].ColumnName = "不良代码";
                }

                if (dt.Columns[i].ToString().ToUpper() == "ERRORDESC")
                {
                    dt.Columns[i].ColumnName = "不良描述";
                }
                if (dt.Columns[i].ToString().ToUpper() == "RCLASSDATE")
                {
                    dt.Columns[i].ColumnName = "转出日期";
                }
                if (dt.Columns[i].ToString().ToUpper() == "REASONCODE")
                {
                    dt.Columns[i].ColumnName = "原因代码";
                }
                if (dt.Columns[i].ToString().ToUpper() == "REASONDESC")
                {
                    dt.Columns[i].ColumnName = "原因描述";
                }
            }


        }

        #endregion

        private void ShowGraph(DataTable _dt, string graphtitle)
        {
            ZedGraphControl zgc = new ZedGraphControl();
            zgc.IsEnableHZoom = false;
            zgc.IsEnableVZoom = false;

            GraphPane myPane = zgc.GraphPane;
            List<string> xname = new List<string>();
            myPane.Title.Text = graphtitle;
            myPane.XAxis.Title.Text = "";
            myPane.YAxis.Title.Text = "";
            PointPairList list = new PointPairList();

            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                list.Add(0.0, Convert.ToDouble(_dt.Rows[i][_dt.Columns.Count - 1].ToString()), _dt.Rows[i][_dt.Columns.Count - 4].ToString());
               // list.Add(0.0, Convert.ToDouble(_dt.Rows[i]["yValue"].ToString()), _dt.Rows[i]["xValue"].ToString());
               //  xname.Add(_dt.Rows[i]["xValue"].ToString());
                xname.Add(_dt.Rows[i][_dt.Columns.Count - 4].ToString());
            }
            BarItem myCurve = myPane.AddBar(null, list, Color.Blue);
            myPane.XAxis.Type = AxisType.Text;
            myPane.XAxis.Scale.TextLabels = xname.ToArray();
            myPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, 255, 166), 45.0F);
            BarItem.CreateBarLabels(myPane, false, "");
            zgc.AxisChange();
            zgc.Refresh();
            this.panelyr.Controls.Clear();
            this.panelyr.Controls.Add(zgc);
            zgc.Dock = DockStyle.Fill;
        }

        private void imbt_chart_Click(object sender, EventArgs e)
        {
            if (!panelyr.Visible)
            {
                panelyr.Visible = true;
            }
            else
            {
                panelyr.Visible = false;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvYieldRate_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private string Analytical_ListBox(ListBox LB, int Flag)
        {
            string ListData = string.Empty;
            if (LB.Items.Count > 0)
            {
                foreach (string Item in LB.Items)
                {
                    if (Flag == 0)
                    {
                        if (Item!="ALL")
                         ListData += Item + ",";
                    }
                    else
                    {
                        if (Item != "ALL")
                         ListData += Item.Split('-')[0] + ",";
                    }
                }

            }

            if (!string.IsNullOrEmpty(ListData))
                ListData = ListData;
            else
                ListData = "ALL";
                return   ListData;
        }

        private void tlmmode_Click(object sender, EventArgs e)
        {

        }
        private bool Check_Select_Item()
        {
            if (ListMode.Items.Count > 1000)
            {
                MessageBox.Show("选择产品料号不能>1000","提示信息",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return false;
            }
            if (Listwo.Items.Count>1000)
            {
                MessageBox.Show("选择工单不能>1000", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (ListLine.Items.Count>1000)
            {
                MessageBox.Show("选择产品线体不能>1000", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (ListRoute.Items.Count>1000)
            {
                MessageBox.Show("选择途程不能>1000", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;

        }

        private void imbt_sortname_Click(object sender, EventArgs e)
        {
            SelectDataListBox sd = new SelectDataListBox(this, sortname, dtProduct);
            sd.ShowDialog();
        }

    }
}
