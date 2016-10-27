using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using System.Diagnostics;


namespace SMT_STOCKIN
{
    public partial class Frm_STOCKIN : Office2007Form //Form
    {
        public Frm_STOCKIN()
        {
            InitializeComponent();
        }

        public string userId = string.Empty;
        public string userPwd = string.Empty;
        public string userName = string.Empty;
        public string My_WipStation = string.Empty;

        /// <summary>
        /// C:\\SFIS\\SFIS.ini
        /// </summary>
        string IniFilePath = "C:\\SFIS\\SFIS.ini";
         IAsyncResult isResult ; 
       public OperateDB oDB = new OperateDB();
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
        private string appfilename = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
        private  void RunFile(string dir, string localFileName, string thisappname)
        {
            try
            {
                if (File.Exists(Path.Combine(dir, localFileName)))
                {
                    Process myProcess = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = dir + localFileName;
                    psi.WorkingDirectory = dir;
                    psi.UseShellExecute = false;
                    psi.Arguments = thisappname;
                    psi.RedirectStandardError = true;
                    psi.CreateNoWindow = true;
                    // psi.RedirectStandardOutput = true;
                    psi.WindowStyle = ProcessWindowStyle.Normal;
                    myProcess.StartInfo = psi;
                    myProcess.Start();
                    myProcess.WaitForExit(20);
                    myProcess.Close();
                    //ssssss
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        WebServices.Check_Version.Check_Version chkver = new WebServices.Check_Version.Check_Version();
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("C:\\SFIS"))
                Directory.CreateDirectory("C:\\SFIS");        

            FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath);
            this.Text = string.Format("SMT_STOCKIN Version:{0} (Build Date:{1})", myFileVersion.FileVersion, System.IO.File.GetLastWriteTime(Application.ExecutablePath).ToShortDateString());

            if (!chkver.CheckPrgVsersion("SMT_STOCKIN", System.Windows.Forms.Application.ProductVersion, null, null, null))
            {
              
                RunFile(System.Windows.Forms.Application.StartupPath + @"\", "AutoUpdate.exe", appfilename);
                MessageBox.Show("该程序为版本不是最新版\r\n请更新后运行");
                string FileName = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
                Process[] prc = Process.GetProcessesByName(FileName.Substring(0, FileName.LastIndexOf('.')));
                if (prc.Length > 0)
                    foreach (Process pc in prc)
                    {
                        pc.Kill();
                    }
                return;
            }

           // LabLine.Text = Encoder.Encoder.DecryptString(Encoder.ReadIniFile.IniReadValue("SMT_STOCKIN", "LINE", IniFilePath));
            LabLine.Text = OperateINI.IniReadValue("SMT_STOCKIN", "LINE", IniFilePath);
           // oDB.ConnDB();
            UserLogin ul = new UserLogin(this);
            if (ul.ShowDialog() == DialogResult.OK)
            {
                LabUser.Text = userId;
                //cb_line.Items.Clear();
                //List<string> LsLine = oDB.Get_Line_Info();
                //foreach (string item in LsLine)
                //{
                //    cb_line.Items.Add(item);
                //}
                //cb_line.SelectedIndex = 0;
                Check_woId_Data(); 


            }
            else
            {
                this.Close();
            }
         
        }

        private void Check_woId_Data()
        {
            dgvwiptracking.DataSource = null;
           string woId= InputQuery.ShowInputBox("请输入工单", string.Empty);
           if (!string.IsNullOrEmpty(woId))
           {
               DataTable dt = oDB.Get_woId_Info(woId);
               DataTable _dtStock = new DataTable();
               if (dt != null && dt.Rows.Count > 0)
               {
                   LabCraft.Text = dt.Rows[0]["OUTPUTGROUP"].ToString();
                   My_WipStation = dt.Rows[0]["LOC"].ToString();
                   //if (!CHECK_PRODUCT_LINE(dt.Rows[0]["LINEID"].ToString()))
                   //    return;
                   DataTable dtwip = oDB.Get_WIP_Tracking(woId, LabCraft.Text);
                   if (dtwip.Rows.Count > 0)
                   {
                       _dtStock = dtwip.Clone();
                       ShowMsg(mLogMsgType.Normal, "开始检查途程.....");
                       foreach (DataRow dr in dtwip.Rows)
                       {
                           string _StrErr = oDB.CheckRoute(dr["ESN"].ToString(), LabCraft.Text);
                           if (_StrErr == "OK")
                           {
                               _dtStock.ImportRow(dr);
                           }
                           else
                           {
                               ShowMsg(mLogMsgType.Warning, string.Format("检查途程失败,ESN [{0}]-> {1}", dr["ESN"].ToString(), _StrErr));                          
                           }
                       }

                       dgvwiptracking.DataSource = _dtStock;
                       ShowMsg(mLogMsgType.Incoming, string.Format("工单[{0}] OK,共计[{1}]笔", woId, dgvwiptracking.Rows.Count.ToString()));
                   }
                   else
                   {
                       ShowMsg(mLogMsgType.Incoming, string.Format("没有待入库数据"));
                   }
               }
               else
               {
                   ShowMsg(mLogMsgType.Error, string.Format("工单[{0}]不存在", woId));
                   Check_woId_Data();
               }
           }
        }

        private void imbt_selectwo_Click(object sender, EventArgs e)
        {
            if ( isResult!=null && !isResult.IsCompleted)
            {
                MessageBox.Show("入库数据未处理完成,请稍后再试?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                return;
            }
                Check_woId_Data();
            
        }



        private void imbt_commit_Click(object sender, EventArgs e)
        {
            //if (isResult!=null && !isResult.IsCompleted)
            //{
            //    MessageBox.Show("入库数据未处理完成,请稍后再试?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //    return;
            //}

            if (dgvwiptracking.Rows.Count > 0)
            {
                if (MessageBox.Show("确定入库?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DataTable dt = dgvwiptracking.DataSource as DataTable;
                    //DataView dataView = dt.DefaultView;
                    //DataTable dataTableDistinct = dataView.ToTable(true, "WOID", "WIPSTATION");
                    string STOCK_NO = oDB.GetStockInNumber();
                    WriteLog("开始SFIS单过站:"+STOCK_NO);
                    foreach (DataRow dr in dt.Rows)
                    {
                        oDB.UPDATE_STOCK_NO(STOCK_NO, dr["WOID"].ToString(), dr["WIPSTATION"].ToString(), dr["ESN"].ToString());
                    }

                    UpLoad_DB(dt, STOCK_NO, LabCraft.Text, LabLine.Text, 0, My_WipStation);

                    //UpLoadToDB = new UpLoadDB(UpLoad_DB);
                    //isResult = UpLoadToDB.BeginInvoke(dt, STOCK_NO, LabCraft.Text, LabLine.Text, 0, My_WipStation, null, null);
               
                }
            }
            else
            {
                ShowMsg(mLogMsgType.Error, "没有数据可以入库");
            }
        }
        private delegate void UpLoadDB(DataTable dt, string STOCK_NO, string MYGROUP, string LINE, int Flag, string WIP_STATION);
        UpLoadDB UpLoadToDB;
        private void UpLoad_DB(DataTable dt,string STOCK_NO, string MYGROUP, string LINE,int Flag,string WIP_STATION)
        {
            int x = 0;
            //this.Invoke(new EventHandler(delegate
            //         {
                         try
                         {
                             ShowMsg(mLogMsgType.Warning, "开始上传数据...");
                             imbt_commit.Enabled = false;
                             imbt_selectwo.Enabled = false;
                             string _StrErr = string.Empty;

                             foreach (DataRow dr in dt.Rows)
                             {
                                 System.Threading.Thread.Sleep(50);
                                 _StrErr = oDB.TEST_MAIN_ONLY(dr["ESN"].ToString(), MYGROUP, userId + "-" + userPwd, "NA", LINE);
                                 if (_StrErr != "OK")
                                 {
                                     ShowMsg(mLogMsgType.Error, string.Format("[{0}] [{1}]", dr["ESN"].ToString(), _StrErr));
                                     return;
                                 }
                              
                                 x++;
                                 progressBarX1.Value = ((dt.Rows.Count - x)*100 / dt.Rows.Count);
                                 progressBarX1.Text = string.Format("{0}/{1}", (dt.Rows.Count - x).ToString(), dt.Rows.Count);
                                 progressBarX1.Update();

                             }

                             oDB.UPDATE_WIPSTATION(STOCK_NO, WIP_STATION);
                               WriteLog("Insert_SAP_BackFlush:" + STOCK_NO);


                                 DataTable dtPrintDoc=oDB.GetStockInPrint(STOCK_NO);
                                // DataView dataView = dtPrintDoc.DefaultView;  //dt.DefaultView;
                                 // DataTable dataTableDistinct = dataView.ToTable(true, "WOID", "PARTNUMBER","PRODUCTNAME");
                                 foreach (DataRow dr in dtPrintDoc.Rows)
                                 {
                                     oDB.Insert_SAP_BackFlush(STOCK_NO, dr["WOID"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(), Convert.ToInt32(dr["QTY"].ToString()));
                                     WriteLog(string.Format("Insert_SAP_BackFlush [{0}] [{1}] [{2}] [{3}] [{4}]", STOCK_NO, dr["WOID"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(), dr["QTY"].ToString()));
                                 }
              

                             ShowMsg(mLogMsgType.Incoming, "上传数据完成...");
                             ShowMsg(mLogMsgType.Incoming, "开始打印单据...");
                             PrintInventoryDocuments(dtPrintDoc, STOCK_NO);
                             ShowMsg(mLogMsgType.Incoming, "打印单据完成...");
                         }
                         catch (Exception ex)
                         {
                             ShowMsg(mLogMsgType.Error, ex.Message);
                         }
                         finally
                         {
                             dgvwiptracking.DataSource = null;
                             imbt_commit.Enabled = true;
                             imbt_selectwo.Enabled = true;
                         }
                     //}));
        }
        private void Frm_STOCKIN_FormClosing(object sender, FormClosingEventArgs e)
        {
           // oDB.CloseDB();
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
        public void PrintInventoryDocuments(DataTable  _dtPintDoc,string _Doc)
        {
            bool PrintFlag = true;

            PrintStockNo = _Doc;
            dtStock = _dtPintDoc;
            foreach (DataRow dr in dtStock.Rows)
            {
                DataTable dtwo = oDB.Get_woId_Info(dr["WOID"].ToString());
                if (dr[2].ToString() != dtwo.Rows[0]["LOC"].ToString())
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

        private void rePrintMenu_Click(object sender, EventArgs e)
        {
            string STOCK_NO = InputQuery.ShowInputBox("输入入库单号", string.Empty);        
            if (!string.IsNullOrEmpty(STOCK_NO))
            {
                DataTable dt = oDB.GetStockInPrint(STOCK_NO);
                if (dt.Rows.Count > 0)
                    PrintInventoryDocuments(dt, STOCK_NO);
                else
                    MessageBox.Show("输入单据号错误");
            }
        }

        private void Imbt_ReUpload_Click(object sender, EventArgs e)
        {
            string STOCK_NO = InputQuery.ShowInputBox("输入入库单号", string.Empty);
            if (!string.IsNullOrEmpty(STOCK_NO))
            {
                DataTable dt = oDB.Get_WIP_Tracking_ReUpload(STOCK_NO);
                if (dt.Rows.Count > 0)
                {
                    DataTable dtwoinfo = oDB.Get_woId_Info(dt.Rows[0]["WOID"].ToString());
                    if (dtwoinfo.Rows.Count > 0)
                    {
                        //UpLoadToDB = new UpLoadDB(UpLoad_DB);
                        //UpLoadToDB.BeginInvoke(dt, STOCK_NO, dtwoinfo.Rows[0]["OUTPUTGROUP"].ToString(), LabLine.Text, 0, dtwoinfo.Rows[0]["LOC"].ToString(), null, null);
                        UpLoad_DB(dt, STOCK_NO, dtwoinfo.Rows[0]["OUTPUTGROUP"].ToString(), LabLine.Text, 0, dtwoinfo.Rows[0]["LOC"].ToString());
                    }
                    else
                    {
                        ShowMsg(mLogMsgType.Error, "没有找到工单");
                    }
                }
                else
                {
                    ShowMsg(mLogMsgType.Error, "没有未完上抛完成数据");
                    try
                    {
                        DataTable dtPrintDoc = oDB.GetStockInPrint(STOCK_NO);
                        if (dtPrintDoc.Rows.Count == 0)
                            return;
                        ShowMsg(mLogMsgType.Normal, "重新传入单据信息");
                        foreach (DataRow dr in dtPrintDoc.Rows)
                        {
                            oDB.Insert_SAP_BackFlush(STOCK_NO, dr["WOID"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(), Convert.ToInt32(dr["QTY"].ToString()));
                            WriteLog(string.Format("Insert_SAP_BackFlush [{0}] [{1}] [{2}] [{3}] [{4}]", STOCK_NO, dr["WOID"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(), dr["QTY"].ToString()));
                        }
                        ShowMsg(mLogMsgType.Normal, "重新传入单据完成");
                    }
                    catch (Exception ex)
                    {
                        ShowMsg(mLogMsgType.Error, ex.Message);
                    }

                }
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
                MessageBox.Show("写入Log失败:"+ex.Message+",\r\n请及时联系SFIS人员");
            }
            #endregion
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void imbt_selectline_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            Frm_Public.Frm_Select fd = new Frm_Public.Frm_Select(oDB.Get_ALL_Line_Info(), ref dic);
            if (fd.ShowDialog() == DialogResult.OK)
            {
                LabLine.Text = dic["线别"].ToString();
                OperateINI.IniWriteValue("SMT_STOCKIN", "LINE", dic["线别"].ToString(), IniFilePath);
            }

            //string[] EmpData = Input.InputBox.ShowInputBox("输入权限", string.Empty);
            //try
            //{
            //    string UserId = EmpData[0];
            //    string PWD = EmpData[1];
            //    if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(PWD))
            //    {
            //        IDictionary<string, object> user_dic = new Dictionary<string, object>();
            //        user_dic.Add("USERID", UserId);
            //        user_dic.Add("PWD", PWD);
            //        string _StrErr = oDB.CHECK_SET_LINE_EMPLOYEE(user_dic);
            //        if (_StrErr == "OK")
            //        {
            //            ShowMsg(mLogMsgType.Incoming, "权限正确");
            //            Dictionary<string, object> dic = new Dictionary<string, object>();
            //            Frm_Public.Frm_Select fd = new Frm_Public.Frm_Select(oDB.Get_ALL_Line_Info(), ref dic);
            //            if (fd.ShowDialog() == DialogResult.OK)
            //            {
            //                LabLine.Text = dic["线别"].ToString();
            //                Encoder.ReadIniFile.IniWriteValue("SMT_STOCKIN", "LINE", Encoder.Encoder.EncryptString(dic["线别"].ToString()), IniFilePath);
            //            }
                       
            //        }
            //        else
            //        {
            //            ShowMsg(mLogMsgType.Error, _StrErr);

            //        }
            //    }

            //}
            //catch
            //{
            //    ShowMsg(mLogMsgType.Error, "权限格式不正确");
            //}
        }


        private bool CHECK_PRODUCT_LINE(string ProductLine)
        {
            bool flag = false;
            foreach (string str in ProductLine.Split(','))
            {
                if (str == LabLine.Text)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                ShowMsg(mLogMsgType.Error, string.Format("此工单不可在{0}生产", LabLine.Text));
            return flag;
        }
    }
}
