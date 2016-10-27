using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO.Ports;
using System.IO;
using LabelManager2;
using System.Diagnostics;
using GenericUtil;

namespace Packing_Ctn
{
    public partial class Frm_Packing_Ctn : Office2007Form //Form
    {
        public Frm_Packing_Ctn()
        {
            InitializeComponent();
        }
        #region 公共变量
        public string Line_Name = string.Empty;
        public string MYGROUP = string.Empty;
        public string Station = string.Empty;
        public string Section = string.Empty;
        int Language = 1;
        #endregion

        #region 私有变量
        System.Windows.Forms.Timer MsgTimerPrg = new System.Windows.Forms.Timer();
        private string appfilename = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
        string UserId = string.Empty;
        string UserPwd = string.Empty;
 
          /// <summary>
        /// C:\\SFIS\\SFIS.ini
        /// </summary>
        string IniFilePath = "C:\\SFIS\\SFIS.ini";
      //  string ProductLine = string.Empty;
        string My_LabelFilePatch = string.Empty;
        /// <summary>
        /// 包装容量
        /// </summary>
        int M_iCartonCapacity;
        /// <summary>
        /// 工单
        /// </summary>
        string My_MoNumber = string.Empty;

        /// <summary>
        /// 料号
        /// </summary>
        string My_PartNumber = string.Empty;
        /// <summary>
        /// 产品型号
        /// </summary>
        string My_Product = string.Empty;

        /// <summary>
        /// 版本
        /// </summary>
        string My_Ver = string.Empty;
        /// <summary>
        /// 产品ESN
        /// </summary>
        string My_ESN = string.Empty;

        /// <summary>
        /// 超时管控,当前时间
        /// </summary>
         System.DateTime Now_DateTime;

        string My_Carton_No = string.Empty;
        string My_MCarton_No = string.Empty;
        int M_TartgetQTY = 0;
        int My_CartonQtyOnDB = 0;
        string My_Cmd = string.Empty;
        string My_Check_No = string.Empty;

        /// <summary>
        /// 串口
        /// </summary>
        System.IO.Ports.SerialPort sPort = null;
        byte[] by = new byte[1024];
        double My_Weight_limit;
        double My_Weight_lower;
        #endregion

        #region 显示信息消息
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };

        private void SendMsg(mLogMsgType msgtype, string msg)
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
        private void SendMsg(mLogMsgType msgtype, string EnglishMsg, string ChineseMsg)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {

                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = mLogMsgTypeColor[(int)msgtype];
                    rtbmsg.AppendText(Language == 0 ? EnglishMsg + "\n" : ChineseMsg + "\n");
                    rtbmsg.ScrollToCaret();
                }));
            }
            catch
            {
            }
        }

        private void PShowScreenInformation()
        {
            lab_CartonCapacity.Text = M_iCartonCapacity.ToString();
            lab_CartonCapacity.Update();

            lab_TargetQty.Text = M_TartgetQTY.ToString();
            lab_TargetQty.Update();

            lab_CartonCount.Text = My_CartonQtyOnDB.ToString();
            lab_CartonCount.Update();

            lab_CartonNo.Text = My_Carton_No;
            lab_CartonNo.Update();

        }
        #endregion

        public void SetStation()
        {
            lab_Option.Text = Line_Name + " : " + MYGROUP;
            OperateIni.IniWriteValue("PACK_CTN", "LINE", Line_Name, IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "STATION", MYGROUP, IniFilePath);

        }
        private void MsgTimerPrg_Tick(object sender, EventArgs e)
        {
            if (GetKeyState(144) == 0)
                num_kock.Text = "Num Lock Off";
            else
                num_kock.Text = "Num Lock On";

            if (GetKeyState(0x14) == 0)
                Caps_lock.Text = "Caps Lock Off";
            else
                Caps_lock.Text = "Caps Lock On";

            StripStatusTime.Text = " Time: " + System.DateTime.Now.ToString();
         
        }
        private void Frm_Packing_Ctn_Load(object sender, EventArgs e)
        {
            //FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath);
            //this.Text = "PHICOMM PACK_CTN--" + myFileVersion.FileVersion;
            //System.DateTime d1 = System.DateTime.Parse("2015/07/30");
            //System.DateTime d2 =System. DateTime.Now;
            //System.TimeSpan ts = d1 - d2;
            //if (ts.Days <= 0)
            //{
            //    MessageBox.Show("试用时间超出,请联系系统开发人员");
            //    this.Close();
            //    return;
            //}       
            if (!Directory.Exists("C:\\SFIS"))
                Directory.CreateDirectory("C:\\SFIS");
           
            FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath);
            this.Text = string.Format("{0} Version:{1} (Build Date:{2})", "PACK_CTN", myFileVersion.FileVersion, System.IO.File.GetLastWriteTime(System.Windows.Forms.Application.ExecutablePath).ToShortDateString());

            if (!refWebCheck_Version.Instance.CheckPrgVsersion("PACK_CTN", this.ProductVersion, null, null, null))
            {
                RunFile(System.Windows.Forms.Application.StartupPath + @"\", "AutoUpdate.exe", appfilename);
                MessageBox.Show("该程序为版本不是最新版\r\n请更新后运行");
                this.Close();
            }
            txt_sn.Enabled = false;
            txt_bc.PasswordChar = '*';      
            ReadIniFile();
            SetStation();
            ConnentCom();
            ChangeLanguage();
            txt_bc.Focus();
            lab_CartonNo.Text = string.Empty;
            this.dgvCartonInfo.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvCartonInfo.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.MsgTimerPrg.Interval = 1000;
            this.MsgTimerPrg.Enabled = true;
            this.MsgTimerPrg.Tick += new EventHandler(MsgTimerPrg_Tick);      
        }

        private static void RunFile(string dir, string localFileName, string thisappname)
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

        private void ReadIniFile()
        {
          //  Line_Name = OperateIni.IniReadValue("PACK_CTN", "LINE", IniFilePath);
            Line_Name =OperateIni.IniReadValue("PACK_CTN", "LINE", IniFilePath);

            MYGROUP = OperateIni.IniReadValue("PACK_CTN", "STATION", IniFilePath);
            string PatchFlag = OperateIni.IniReadValue("PACK_CTN", "PatchFlag", IniFilePath);
            string PrintProduct = OperateIni.IniReadValue("PACK_CTN", "PrintProduct", IniFilePath);
            string PrintLabel = OperateIni.IniReadValue("PACK_CTN", "PrintLabel", IniFilePath);
            My_LabelFilePatch = OperateIni.IniReadValue("PACK_CTN", "LabelFilePatch", IniFilePath);
            string OrderlyPacking = OperateIni.IniReadValue("PACK_CTN", "OrderlyPacking", IniFilePath);

            string My_Language = OperateIni.IniReadValue("PACK_CTN", "Language", IniFilePath);
            if (PatchFlag == "0")
                imbt_LabelPartnumber.Checked = true;
            else
                imbt_Labelwoid.Checked = true;
            switch (OperateIni.IniReadValue("PACK_CTN", "PACKTYPE", IniFilePath))
            {
                case "1":
                    itemESN.Checked = true;
                    itemESN_Click(null, null);
                    break;
                case "2":
                    itemSN.Checked = true;
                    itemSN_Click(null, null);
                    break;
                case "3":
                    itemIMEI.Checked = true;
                    itemIMEI_Click(null, null);
                    break;
                default:
                    itemESN.Checked = true;
                    itemESN_Click(null, null);
                    break;
            }
            if (PrintProduct == "0")
                imbt_PrintProduct.Checked = true;
            else
                imbt_PrintProduct.Checked = false;

            if (PrintLabel == "0")
                imbt_PrintCtnLabel.Checked = true;
            else
                imbt_PrintCtnLabel.Checked = false;
            if (My_Language == "0")
            {
                Language = 0;
                imbt_English.Checked = true;
            }
            else
            {
                Language = 1;
                imbt_Chinese.Checked = true;
            }
            if (OrderlyPacking == "0")
                imbt_OrderlyPacking.Checked = true;
            else
                imbt_OrderlyPacking.Checked = false;

        }


        /// <summary>
        /// 连接串口[电子称]
        /// </summary>
        /// <returns></returns>
        public bool ConnentCom()
        {
            string _Port = OperateIni.IniReadValue("PACK_CTN", "myport", IniFilePath);
            string _BaudRate = OperateIni.IniReadValue("PACK_CTN", "BaudRate", IniFilePath);
            string _DataBits = OperateIni.IniReadValue("PACK_CTN", "DataBits", IniFilePath);
            string _Parity = OperateIni.IniReadValue("PACK_CTN", "Parity", IniFilePath);
            string _StopBits = OperateIni.IniReadValue("PACK_CTN", "StopBits", IniFilePath);
            string _ReadBufferSize = OperateIni.IniReadValue("PACK_CTN", "ReadBufferSize", IniFilePath);
            My_Cmd = OperateIni.IniReadValue("PACK_CTN", "cmd", IniFilePath);

            try
            {
                sPort = new System.IO.Ports.SerialPort();
                sPort.PortName = "COM" + _Port;
                sPort.BaudRate = int.Parse(_BaudRate); //串口通信波特率19200 
                sPort.ReadBufferSize = int.Parse(_ReadBufferSize);
                sPort.DataBits = int.Parse(_DataBits); //数据位 8
                sPort.Parity = (Parity)Enum.Parse(typeof(Parity), _Parity); //奇偶校验0 
                sPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), _StopBits); //System.IO.Ports.StopBits.One;//停止位 
                sPort.ReadTimeout = 1000; //读超时 
                sPort.WriteTimeout = 1000;
                //  sPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(sPort_DataReceived);
                sPort.Open();

                SendMsg(mLogMsgType.Incoming, string.Format("Connection Com{0} OK", _Port), string.Format("连接串口 Com{0} OK", _Port));
                return true;
            }
            catch
            {

                SendMsg(mLogMsgType.Error, string.Format("Connection Com{0} Fail", _Port), string.Format("连接串口 Com{0} 失败", _Port));
                InitializeSserialPort();
                return false;
            }
        }
        /// <summary>
        /// 初始化串口信息
        /// </summary>
        private void InitializeSserialPort()
        {
            OperateIni.IniWriteValue("PACK_CTN", "LINE", "", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "PACKTYPE", "1", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "STATION", "NA", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "PrintProduct", "0", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "LabelQty", "0", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "LabelX", "0.00", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "LabelY", "0.00", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "LabelFilePatch", "D:", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "PrintLabel", "0", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "OrderlyPacking", "0", IniFilePath);          

            OperateIni.IniWriteValue("PACK_CTN", "myport", "1", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "BaudRate", "9600", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "DataBits", "8", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "Parity", "0", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "StopBits", "1", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "ReadBufferSize", "4096", IniFilePath);
            OperateIni.IniWriteValue("PACK_CTN", "cmd", "r", IniFilePath);
      
            SendMsg(mLogMsgType.Warning, "Read Serial Port Error, Initialize Serial Port", "读取参数失败,初始化串口信息,请重新打开程序");
            MessageBox.Show("The Program Is About To Close","Close Information");
            this.Close();
        }

        private void ChangeLanguage()
        {

            itemLine.Text = Language == 0 ? "Station Name" : "选择站名";
            SamplingUint1.Text = Language == 0 ? "Sampling Uint" : "菜单单元";
            groupBox1.Text = Language == 0 ? "Barcode Input Field" : "条码输入字段";
            groupBox3.Text = Language == 0 ? "Carton InfoMation" : "箱号信息";
            groupBox2.Text = Language == 0 ? "Message" : "显示消息";
            label1.Text = Language == 0 ? "Carton No:" : "箱号:";
            label2.Text = Language == 0 ? "Carton Capacity:" : "箱号容量:";
            label3.Text = Language == 0 ? "Count:" : "数量:";
            label5.Text = Language == 0 ? "Target QTY:" : "目标数量:";
            label4.Text = Language == 0 ? "Employee:" : "员工:";
            label6.Text = Language == 0 ? "PartNumber:" : "料号:";
            label7.Text = Language == 0 ? "WOID:" : "工单:";
            label9.Text = Language == 0 ? "Version:" : "版本:";
            label8.Text = Language == 0 ? "ProductName:" : "产品名称:";
            imbt_Labelqty.Text = Language == 0 ? "Label QTY" : "条码打印数量";
            imbt_ChkRoute.Text = Language == 0 ? "Check Route" : "检查途程";
            imbt_ManualCloseCarton.Text = Language == 0 ? "Close Carton" : "关闭箱号";
            imbt_PrintCtnLabel.Text = Language == 0 ? "Print Carton Label" : "打印箱号条码";
            imbt_PrintProduct.Text = Language == 0 ? "Print Product Information" : "打印产品信息";
            imbt_LabelPartnumber.Text = Language == 0 ? "Label Partnumber" : "以料号获取";
            imbt_Labelwoid.Text = Language == 0 ? "Label woId" : "以工单获取";
            imbt_ChkWeight.Text = Language == 0 ? "Check Weight" : "称重检查";
            imbt_Printcoordinates.Text = Language == 0 ? "Print Coordinates" : "打印坐标";
            imbt_OrderlyPacking.Text = Language == 0 ? "Orderly Packing" : "有序装箱";
            imbt_NoCloseCtn.Text= Language == 0 ? "No Close Carton" : "未关闭箱号";
            if (Language != 0)
            {
                label2.Location = new Point(label3.Location.X - 34, label2.Location.Y);
                label5.Location = new Point(label3.Location.X - 34, label5.Location.Y);

                label4.Location = new Point(La_GString.Location.X - 7, label4.Location.Y);
                label6.Location = new Point(La_GString.Location.X - 7, label6.Location.Y);
                label7.Location = new Point(La_GString.Location.X - 7, label7.Location.Y);
                label9.Location = new Point(La_GString.Location.X - 7, label9.Location.Y);
                label8.Location = new Point(La_GString.Location.X - 42, label8.Location.Y);
            }
        }

        #region  连接数据库与检查数据
        private bool Check_Emp(TextBox tx)
        {
            try
            {
                string Emp_ID = tx.Text.Split('-')[0];
                string Emp_PWD = tx.Text.Split('-')[1];
                DataTable dt = OperateDB.Get_User_Info(Emp_ID, Emp_PWD);
                if (dt.Rows.Count > 0)
                {
                    tx.PasswordChar = new char();
                    tx.ForeColor = Color.Green;
                    tx.Text = dt.Rows[0]["USERNAME"].ToString();
                    UserId = Emp_ID;
                    UserPwd = Emp_PWD;
                    SendMsg(mLogMsgType.Incoming, "Employee OK !", "员工权限正确!");
                }
                else
                {
                    SendMsg(mLogMsgType.Error, "Employee Not Exists !", "员工不存在!");
                    tx.Text = null;
                    tx.ForeColor = Color.Black;
                    tx.PasswordChar = '*';
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                SendMsg(mLogMsgType.Error, "Employee Format Error " + ex.Message, "权限格式错误:" + ex.Message);
                tx.Text = null;
                tx.ForeColor = Color.Black;
                tx.PasswordChar = '*';
                return false;
            }
        }
        private int GetCartonCapacityByPartNumber(string ParamNo)
        {
            DataTable dt = ReleaseData.arrByteToDataTable(refWebtPackParameters.Instance.GetPackModelParameters(ParamNo, null));
            if (dt.Rows.Count > 0)
            {
                string CartonQTY = dt.Rows[0]["CARTONQTY"].ToString();
                if (!string.IsNullOrEmpty(CartonQTY))
                    return Convert.ToInt32(CartonQTY);
                else
                    return -1;
            }
            else
            {
                return -1;
            }

        }

        private bool VerifySN(string SN)
        {
            try
            {
                if (string.IsNullOrEmpty(SN) || SN == "NA")
                {
                    SendMsg(mLogMsgType.Error, "Illegal ESN!", "无效的ESN !");
                    return false;
                }
                string _Colnum = string.Empty;
                if (itemESN.Checked)
                    _Colnum = "ESN";
                if (itemIMEI.Checked)
                    _Colnum = "IMEI";
                if (itemSN.Checked)
                    _Colnum = "SN";             
                DataTable dt = ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo(_Colnum, SN));              
                if (dt.Rows.Count > 0)
                {
                    if (imbt_OrderlyPacking.Checked && _Colnum=="SN")
                    {
                        if (!CompareSnAreaHistory(SN))
                        {
                            SendMsg(mLogMsgType.Error, "SN does not conform to rules", "序列号不符合规则");
                            if (MessageBoxEx.Show("序列号不符合规则,是否继续?\n继续请按[Yes],取消请按[No]", "警告!!",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                return false;
                            }
                        }
                        OperateIni.IniWriteValue("PACK_CTN", "SnHistory", SN, IniFilePath);
                    }

                    if (Convert.ToInt32(dt.Rows[0]["ERRFLAG"].ToString()) != 0)
                    {
                        SendMsg(mLogMsgType.Error, "This " + SN + " must into Repair Station!", SN + " 在维修站 !");
                        return false;
                    }

                    if (Convert.ToInt32(dt.Rows[0]["SCRAPFLAG"].ToString()) != 0)
                    {
                        SendMsg(mLogMsgType.Error, "This " + SN + " has been scraped !", SN + " 已经报废 ! ");
                        return false;
                    }

                    //if (dt.Rows[0]["QA_RESULT"].ToString() =="P")
                    //{
                    //    SendMsg(mLogMsgType.Error, string.Format("This {0} has been rejected !", _Colnum));
                    //    return false;
                    //}    

                    if (!string.IsNullOrEmpty(dt.Rows[0]["CARTONNUMBER"].ToString()) && dt.Rows[0]["CARTONNUMBER"].ToString() != "NA")
                    {
                        SendMsg(mLogMsgType.Error, string.Format("This " + SN + " Have Caront No [{0}] !", dt.Rows[0]["CARTONNUMBER"].ToString()), string.Format("该 " + SN + " 已经有箱号 [{0}] !", dt.Rows[0]["CARTONNUMBER"].ToString()));
                        return false;
                    }

                    if (string.IsNullOrEmpty(Line_Name))
                        Line_Name = dt.Rows[0]["LINE"].ToString();
                    My_ESN = dt.Rows[0]["ESN"].ToString();
                    My_MoNumber = dt.Rows[0]["WOID"].ToString();
                    My_PartNumber = dt.Rows[0]["PARTNUMBER"].ToString();
                    My_Product = dt.Rows[0]["PRODUCTNAME"].ToString();
                    My_Ver = dt.Rows[0]["VERSIONCODE"].ToString();
                }
                else
                {
                    SendMsg(mLogMsgType.Error, string.Format(Language == 0 ? "This {0} Not Found !!" : "{0} 没有找到 !!", SN));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                SendMsg(mLogMsgType.Error, "Exception " + ex.Message);
                return false;
            }
        }
        private bool CHECK_WOID()
        {
            string WO_Status = string.Empty;
            try
            {
                DataTable dt = ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(My_MoNumber,null));
                if (dt.Rows.Count > 0)
                {
                    WO_Status = dt.Rows[0]["WOSTATE"].ToString();
                  //  ProductLine = dt.Rows[0]["LINEID"].ToString();
                  //  SendMsg(mLogMsgType.Normal, string.Format("WO[{0}],Product Line[{1}]", My_MoNumber, ProductLine), string.Format("工单[{0}],可在[{1}]线生产", My_MoNumber, ProductLine));
                    string C_RES = "";
                    string C_RES_CHINESE = "";
                    switch (Convert.ToInt32(WO_Status))
                    {
                        case 0:
                            C_RES = "WO Waiting Relaese !!";
                            C_RES_CHINESE = "工单待Relaese";
                            break;
                        case 1:
                            C_RES = "OK";
                            C_RES_CHINESE = "OK";
                            break;
                        case 2:
                             C_RES = "OK";
                             C_RES_CHINESE = "OK";
                            break;
                        case 3:
                            C_RES = "WO CLOSED !!";
                            C_RES_CHINESE = "工单已关闭";
                            break;
                        case 4:
                            C_RES = "WO HOLD !!";
                            C_RES_CHINESE = "工单锁定";
                            break;
                        default:
                            C_RES = "Not Exists!!";
                            C_RES_CHINESE = "工单状态不存在";
                            break;
                    }
                    if (C_RES != "OK")
                    {
                        SendMsg(mLogMsgType.Error, C_RES, C_RES_CHINESE);
                        return false;
                    }                
                }
                else
                {
                    SendMsg(mLogMsgType.Error, "WOID [" + My_MoNumber + "] Not Found", "工单[" + My_MoNumber + "]没有找到");
                    return false;
                }
                My_Check_No = dt.Rows[0]["CHECK_NO"].ToString();
                M_TartgetQTY = Convert.ToInt32(dt.Rows[0]["QTY"].ToString());
                if (imbt_ChkWeight.Checked)
                {
                    try
                    {
                        My_Weight_limit = Convert.ToDouble(dt.Rows[0]["NAL_PREFIX"].ToString().Split('-')[0]);
                        My_Weight_lower = Convert.ToDouble(dt.Rows[0]["NAL_PREFIX"].ToString().Split('-')[1]);
                    }
                    catch
                    {
                        My_Weight_limit = 0;
                        My_Weight_lower = 0;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                SendMsg(mLogMsgType.Error, "Check WOID Error:" + ex.Message);
                return false;
            }
        }

        private bool FGetCartonNo()
        {
            DataTable dtNoCloseCtn = ReleaseData.arrByteToDataTable(refWebtPalletInfo.Instance.Get_Pallet_Info_bywo(My_MoNumber, 1, 0));
            if (dtNoCloseCtn.Rows.Count > 0)
            {
                DataTable dtChkCtn = Function.getNewTable(dtNoCloseCtn, string.Format("LINE='{0}' AND COMPUTER='{1}'", Line_Name, Function.getMacList()[0]));
                if (dtChkCtn.Rows.Count == 0)
                {
                    return GetNewCartonNo();
                }
                else
                {
                    My_Carton_No = dtChkCtn.Rows[0]["PALLETNUMBER"].ToString();
                    My_MCarton_No = dtChkCtn.Rows[0]["PALLETNUMBER"].ToString();
                }
                return true;
            }
            else
            {
                return GetNewCartonNo();
            }       
        }

        private bool GetNewCartonNo()
        {
            string _My_Carton_No = string.Empty;
            DataTable dtProduct = ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetProductByPartNumber(My_PartNumber));
            if (dtProduct.Rows.Count > 0)
            {
                if (dtProduct.Rows[0]["OTHER"].ToString() == "CUST")
                {
                    DataTable dtCustInfo = ReleaseData.arrByteToDataTable(refWebtB_SnRule_wo.Instance.GetB_SNRULE_WO(My_MoNumber));
                    if (dtCustInfo.Rows.Count > 0)
                    {
                        if (dtCustInfo.Rows.Count > 1)
                        {
                            SendMsg(mLogMsgType.Error, "WO Cust Snrule Find More Data !!", "工单客户箱号规则发现多笔数据 !!");
                            return false;
                        }
                        else
                        {
                            string Cust_Carton_PreFix = dtCustInfo.Rows[0]["CUST_CARTON_PREFIX"].ToString() == "NA" ? "" : dtCustInfo.Rows[0]["CUST_CARTON_PREFIX"].ToString();
                            string Cust_Carton_PostFix = dtCustInfo.Rows[0]["CUST_CARTON_POSTFIX"].ToString() == "NA" ? "" : dtCustInfo.Rows[0]["CUST_CARTON_POSTFIX"].ToString();
                            int Cust_Carton_Leng = Convert.ToInt32(dtCustInfo.Rows[0]["CUST_CARTON_LENG"].ToString());
                            string Cust_Carton_STR = dtCustInfo.Rows[0]["CUST_CARTON_STR"].ToString();
                            string Cust_Last_Carton = dtCustInfo.Rows[0]["CUST_LAST_CARTON"].ToString();
                            string Cust_End_Carton = string.IsNullOrEmpty(dtCustInfo.Rows[0]["CUST_END_CARTON"].ToString()) ? "0" : dtCustInfo.Rows[0]["CUST_END_CARTON"].ToString();
                            if (string.IsNullOrEmpty(Cust_Carton_PreFix) || Cust_Carton_PreFix == "NA")
                            {
                                SendMsg(mLogMsgType.Error, "WO Cust Snrule Define Error !!", "工单客户箱号规则设定错误 !!");
                                return false;
                            }
                            if (Cust_End_Carton != "0")
                            {
                                if (Convert.ToInt32(Cust_Last_Carton) > Convert.ToInt32(Cust_End_Carton))
                                {

                                    SendMsg(mLogMsgType.Error, "WO Has Been Assigned To The Maximum Number !!", "工单客户箱号已分配到最大箱号 !!");
                                    return false;
                                }
                            }
                            _My_Carton_No = Cust_Carton_PreFix + Cust_Last_Carton + Cust_Carton_PostFix;
                            if (_My_Carton_No.Length != Cust_Carton_Leng)
                            {

                                SendMsg(mLogMsgType.Error, string.Format("Create Cust Carton Length Error, Error Is[{0}],Actual is[{1}],Please Check...", My_Carton_No.Length.ToString(), Cust_Carton_Leng.ToString()), string.Format("生成客户CTN规则长度错误实际[{0}]位,规则要求[{1}]位,请确认...", My_Carton_No.Length.ToString(), Cust_Carton_Leng.ToString()));
                                return false;
                            }
                            string NextCartonStr = (Convert.ToInt32(Cust_Last_Carton) + 1).ToString().PadLeft(Cust_Carton_STR.Length, '0');
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("WOID", My_MoNumber);
                            string _StrErr = refWebtB_SnRule_wo.Instance.UpdateCustPalletCartonNo_WO(MapListConverter.DictionaryToJson(dic), NextCartonStr, 2);
                            //string _StrErr = refWebtB_SnRule_wo.Instance.UpdateCustPalletCartonNo_WO(new WebServices.tB_SnRule_WO.B_SNRULE_WO_Table()
                            //{
                            //    WOID = My_MoNumber,
                            //}, NextCartonStr, 2);
                            if (_StrErr != "OK")
                            {
                                SendMsg(mLogMsgType.Error, "Update Cust Snrule Error: " + _StrErr, "更新客户规则信息错误: " + _StrErr);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        SendMsg(mLogMsgType.Error, "Not Define WO Cust Snrule !!", " 没有定义工单客户箱号规则 !!");
                        return false;
                    }
                }
                else
                {
                    _My_Carton_No = refWebtPalletInfo.Instance.Get_Carton_No(My_MoNumber, "");

                }
            }
            else
            {
                SendMsg(mLogMsgType.Error, "Not Define ProductName !! ", "没有定义产品 !!");
                return false;
            }

            if (!Check_Dup_Ctn(_My_Carton_No))
            {
                return false;
            }

            My_Carton_No = _My_Carton_No;
            My_MCarton_No = _My_Carton_No;

            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("WOID", My_MoNumber);
                dic.Add("PARTNUMBER", My_PartNumber);
                dic.Add("LINE", Line_Name);
                dic.Add("PALLETNUMBER", My_Carton_No);
                dic.Add("PACKTYPE", 1);
                dic.Add("CLOSEFLAG", 0);
                dic.Add("TOTAL", M_iCartonCapacity);
                dic.Add("COMPUTER", Function.getMacList()[0]);
                refWebtPalletInfo.Instance.InsertPalletInfo(MapListConverter.DictionaryToJson(dic));
                //refWebtPalletInfo.Instance.InsertPalletInfo(new WebServices.tPalletInfo.tPalletInfoTable()
                //{
                //    woId = My_MoNumber,
                //    PartNumber = My_PartNumber,
                //    Line = Line_Name,
                //    PalletNumber = My_Carton_No,
                //    Packtype = 1,
                //    CloseFlag = 0,
                //    Total = M_iCartonCapacity,
                //    Computer = Function.getMacList()[0]
                //});
            }
            catch (Exception ex)
            {
                SendMsg(mLogMsgType.Error, "Inser PalletInfo Error: " + ex.Message, "写入PalletInfo失败: " + ex.Message);
                return false;
            }
            return true;
        }

        private bool Check_Dup_Ctn(string CTN)
        {
            DataTable dt = GetWipTracking(CTN);
            if (dt.Rows.Count > 0)
            {
                SendMsg(mLogMsgType.Error, "Carton No Duplicate ", "Carton No 重复!!");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool Chk_Ctn_Correspond()
        {
            if (!string.IsNullOrEmpty(lab_wo.Text))
            {
                if (lab_wo.Text != My_MoNumber)
                {
                    SendMsg(mLogMsgType.Error, string.Format("WO Different  [{0}]≠[{1}] !!", lab_wo.Text, My_MoNumber), string.Format("工单不同[{0}]≠[{1}] !!", lab_wo.Text, My_MoNumber));
                    return false;
                }
                if (lab_PartNumber.Text != My_PartNumber)
                {
                    SendMsg(mLogMsgType.Error, "Different PartNumber!!", "料号不同 !!");
                    return false;
                }               
            }
            //if (!CHECK_PRODUCT_LINE())
            //    return false;
            lab_PartNumber.Text = My_PartNumber;
            lab_wo.Text = My_MoNumber;
            lab_ver.Text = My_Ver;
            lab_Product.Text = My_Product;
            return true;
        }

        private bool Verify_Route()
        {            
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("MYGROUP", MYGROUP);
            dic.Add("DATA", My_ESN);
            string _StrErr = refWebProPublicStoredproc.Instance.ExecuteProcedure("PRO_CHECKROUTE", MapListConverter.DictionaryToJson(dic));
            if (_StrErr != "OK")
            {
                SendMsg(mLogMsgType.Error, "Route Error -" + _StrErr, "流程错误-" + _StrErr);
                return false;
            }
            return true;
        }

        private DataTable GetWipTracking(string _CTN)
        {
            return ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo("CARTONNUMBER", _CTN));
        }

        private bool Call_SP_TEST_CTN_PALT_TRAY()
        {
            string ErrMsg = refWebtPublicStoredproc.Instance.SP_TEST_CTN_PALT_TRAY(My_ESN, MYGROUP, UserId + "-" + UserPwd, "NA", Line_Name, My_Carton_No, My_MCarton_No, 1);
            if (ErrMsg != "OK")
            {
                SendMsg(mLogMsgType.Error, "Update DataBase Error:" + ErrMsg, "更新数据库失败:" + ErrMsg);
                return false;
            }
            return true;
        }

        public void Close_Carton(string My_Close_Ctn)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PALLETNUMBER", My_Close_Ctn);
            dic.Add("CLOSEFLAG", 1);
            refWebtPalletInfo.Instance.UpdatePalletCloseFlag(MapListConverter.DictionaryToJson(dic));  
        }

        /// <summary>
        /// SN号需要按递增规则
        /// </summary>
        /// <param name="serial">当前的SN号</param>
        /// <param name="history">上一个SN号</param>
        /// <returns></returns>
        private bool CompareSnAreaHistory(string serial)
        {
            try
            {
                string history = OperateIni.IniReadValue("PACK_CTN", "SnHistory", IniFilePath);

                if (history.Length != serial.Length)
                    return false;
                int flag = 0;
                for (int i = 1; i <= history.Length; i++)
                {
                    if ((history.Substring(0, i)) != (serial.Substring(0, i)))
                    {
                        flag = i;
                        break;
                    }
                }
                if (flag == 0)
                    return false;
                if (serial.Substring(0, flag - 1) != history.Substring(0, flag - 1))
                    return false;
                int _histroy = int.Parse(history.Substring(flag - 1, history.Length - flag + 1));
                int _serial = int.Parse(serial.Substring(flag - 1, history.Length - flag + 1));
                if (_serial == _histroy + 1)
                    return true;
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                SendMsg (mLogMsgType.Error, ex.Message);
                return false;
            }
        }

        #endregion

        private void UNDO()
        {
            My_MCarton_No = string.Empty;
            My_MCarton_No = string.Empty;
            My_MoNumber = string.Empty;
            My_PartNumber = string.Empty;
            My_Product = string.Empty;
            My_Weight_limit = 0;
            My_Weight_lower = 0;
            lab_CartonCapacity.Text = "0";
            lab_CartonCount.Text = "0";
            lab_TargetQty.Text = "0";
            lab_CartonNo.Text = string.Empty;
            lab_PartNumber.Text = string.Empty;
            lab_PartNumber.Text = string.Empty;
            lab_Product.Text = string.Empty;
            lab_ver.Text = string.Empty;
            lab_wo.Text = string.Empty;
        }

        private bool Record_Weighing()
        {
            string _strErr = string.Empty;
            string _EnstrErr = string.Empty;
            try
            {
                _strErr = "发送指令到串口失败";
                _EnstrErr = "Send Prot Fail";
                byte[] buf = Encoding.ASCII.GetBytes(My_Cmd);
                sPort.Write(buf, 0, buf.Length);
                System.Threading.Thread.Sleep(350);
                _strErr = "读取串口返回信息失败";
                _EnstrErr = "Read Port Fail";
                sPort.Read(by, 0, by.Length);
                string _Weight = Encoding.ASCII.GetString(by).Trim('\0');
                _Weight = _Weight.Trim();
                _strErr = "读取串口数据转为浮点型失败:" + _Weight;
                _EnstrErr = "Convert double Fail:" + _Weight;
                double x = Convert.ToDouble(_Weight);
                if (My_Weight_limit == 0 || My_Weight_lower == 0)
                {
                    SendMsg(mLogMsgType.Error, "Not Define Weight Initial value", "未设置称重参数,请确认...");
                    return false;
                }
                if ((x >= My_Weight_limit) && (x <= My_Weight_lower))
                {
                    SendMsg(mLogMsgType.Incoming, string.Format("Check Weight OK,Weight Range[{0}~{1}],Actual[{2}]", My_Weight_limit.ToString(), My_Weight_lower.ToString(), x.ToString()), string.Format("称重检查完成,称重范围为[{0}~{1}],实际[{2}]", My_Weight_limit.ToString(), My_Weight_lower.ToString(), x.ToString()));
                }
                else
                {
                    SendMsg(mLogMsgType.Error, string.Format("Check Weight Fall,Weight Range[{0}~{1}],Actual[{2}]", My_Weight_limit.ToString(), My_Weight_lower.ToString(), x.ToString()), string.Format("称重不符合范围,称重范围为[{0}~{1}],实际为[{2}]", My_Weight_limit.ToString(), My_Weight_lower.ToString(), x.ToString()));
                    return false;
                }
                string sWeightMsg = refWebtWipTracking.Instance.UpdateWipTrackingWeight(My_ESN, x.ToString());
                if (sWeightMsg != "OK")
                {
                    SendMsg(mLogMsgType.Error, "Weight Update DataBase Error", "重量更新数据失败");
                    return false;
                }
                return true;
            }
            catch
            {
                SendMsg(mLogMsgType.Error, _EnstrErr, _strErr);
                return false;
            }

        }
        private void itemLine_Click(object sender, EventArgs e)
        {
            //string[] EmpData = Input.InputBox.ShowInputBox("输入权限", string.Empty);
            //try
            //{
            //    string UserId = EmpData[0];
            //    string PWD = EmpData[1];
            //    if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(PWD))
            //    {
            //        string _StrErr = refWebtUserInfo.Instance.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);
            //        if (_StrErr == "OK")
            //        {
            //            SendMsg(mLogMsgType.Incoming, "权限正确");
                        //Frm_StationName fsn = new Frm_StationName(this);
                        //fsn.ShowDialog();
            //        }
            //        else
            //        {
            //            SendMsg(mLogMsgType.Error, _StrErr);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    SendMsg(mLogMsgType.Error, "权限格式不正确:" + ex.Message);
            //}
            Frm_StationName fsn = new Frm_StationName(this);
            fsn.ShowDialog();
        }
        private void Checked_ButtonItem(ButtonItem bi)
        {
            itemESN.Checked = false;
            itemIMEI.Checked = false;
            itemSN.Checked = false;
            bi.Checked = true;
        }
        private void itemESN_Click(object sender, EventArgs e)
        {
            Checked_ButtonItem(itemESN);
            La_GString.Text = "ESN:";
            OperateIni.IniWriteValue("PACK_CTN", "PACKTYPE", "1", IniFilePath);
        }
        private void itemSN_Click(object sender, EventArgs e)
        {
            Checked_ButtonItem(itemSN);
            La_GString.Text = "SN:";
            OperateIni.IniWriteValue("PACK_CTN", "PACKTYPE", "2", IniFilePath);
        }
        private void itemIMEI_Click(object sender, EventArgs e)
        {
            Checked_ButtonItem(itemIMEI);
            La_GString.Text = "IMEI:";
            OperateIni.IniWriteValue("PACK_CTN", "PACKTYPE", "3", IniFilePath);

        }
        private void txt_bc_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_bc.Text) && e.KeyCode == Keys.Enter)
            {
                if (Check_Emp(txt_bc))
                {
                    txt_sn.Enabled = true;
                    txt_sn.Text = "";
                    txt_sn.Focus();
                }
                else
                {
                    txt_bc.Focus();
                }
            }
        }

        private void txt_bc_TextChanged(object sender, EventArgs e)
        {
            if (txt_sn.Enabled)
            {
                txt_bc.ForeColor = Color.Black;
                txt_bc.PasswordChar = '*';
                txt_bc.SelectAll();
                txt_bc.Focus();
            }
            txt_sn.Enabled = false;
        }
       // System.Diagnostics.Stopwatch myWatch = System.Diagnostics.Stopwatch.StartNew();
        private void txt_sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_sn.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txt_sn.Text == "UNDO")
                    {
                        UNDO();             
                        return;
                    }
              
                    if (!VerifySN(txt_sn.Text))               
                        return;
                               
                    if (!CHECK_WOID())                                     
                        return;                    

                    if (!Chk_Ctn_Correspond())                                 
                        return;
                   

                    if (imbt_ChkRoute.Checked)
                    {
                        if (!Verify_Route())                                              
                            return;
                       
                    }               
                    PrepareAndPrint();
                   
                }
                catch (Exception ex)
                {
                    SendMsg(mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    txt_sn.Focus();
                    txt_sn.SelectAll();
                }
            }
        }

        private void PrepareAndPrint()
        {         
            if (!Check_RollBack_Station())           
                return;             

            if (imbt_ChkWeight.Checked)
            {
                if (!Record_Weighing())
                    return;
            }      
            PAfterGetSSN();


        }
        private void PAfterGetSSN()
        {

            M_iCartonCapacity = GetCartonCapacityByPartNumber(My_PartNumber);
            if (M_iCartonCapacity == -1 || M_iCartonCapacity == 0)
            {
                SendMsg(mLogMsgType.Error, "Not Define Carton Capacity !!", "没有定义箱号容量 !!");
                return;
            }
    

            if (!FGetCartonNo())          
                return;          

            if (!Call_SP_TEST_CTN_PALT_TRAY())          
                return;        

            imbt_CloseCarton.Enabled = true;
            DataTable dt_Show_Data = GetWipTracking(My_Carton_No);
            My_CartonQtyOnDB = dt_Show_Data.Rows.Count;
            dgvCartonInfo.Rows.Clear();
            foreach (DataRow dr in dt_Show_Data.Rows)
            {
                dgvCartonInfo.Rows.Add(dr["ESN"].ToString(), dr["SN"].ToString(), dr["IMEI"].ToString());
            }
            if (My_CartonQtyOnDB > 0)
                dgvCartonInfo.FirstDisplayedScrollingRowIndex = dgvCartonInfo.Rows.Count - 1;
            PShowScreenInformation();
            txt_sn.Text = string.Empty;
            txt_sn.Focus();
            SendMsg(mLogMsgType.Incoming, My_ESN + " OK");
            if (Convert.ToInt32(lab_CartonCount.Text) >= Convert.ToInt32(lab_CartonCapacity.Text))
            {
                SendMsg(mLogMsgType.Warning, "Carton Full", "箱号已满");
                imbt_CloseCarton.Enabled = false;
                Close_Carton(My_Carton_No);
                if (imbt_PrintCtnLabel.Checked)
                {
                    PrintCartonLabel_CodeSoft(My_Carton_No);
                }
            }
        }

        public void PrintCartonLabel_CodeSoft(string MyCartonNo)
        {

            DataTable dtCarton = GetWipTracking(MyCartonNo);
            if (dtCarton.Rows.Count > 0)
            {
                DataTable dtPrint = new DataTable("mydt");
                dtPrint.Columns.Add("Name", typeof(string));
                dtPrint.Columns.Add("value", typeof(string));
                dtPrint.Rows.Add("CARTON_NO", dtCarton.Rows[0]["CARTONNUMBER"].ToString());
                dtPrint.Rows.Add("MCARTON_NO", dtCarton.Rows[0]["MCARTONNUMBER"].ToString());
                dtPrint.Rows.Add("PRODUCTNAME", dtCarton.Rows[0]["PRODUCTNAME"].ToString());
                dtPrint.Rows.Add("PARTNUMBER", dtCarton.Rows[0]["PARTNUMBER"].ToString());
                if (imbt_PrintProduct.Checked)
                {
                    DataTable dtProductInfo = ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetProductByPartNumber(dtCarton.Rows[0]["PARTNUMBER"].ToString()));
                    if (dtProductInfo.Rows.Count > 0)
                    {
                        dtPrint.Rows.Add("COLOR", dtProductInfo.Rows[0]["PRODUCTCOLOR"].ToString());
                    }
                    DataTable dt_Soft_Fw_Ver = ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(dtCarton.Rows[0]["WOID"].ToString(),null));
                    if (dt_Soft_Fw_Ver.Rows.Count > 0)
                    {
                        dtPrint.Rows.Add("SVER", dt_Soft_Fw_Ver.Rows[0]["SW_VER"].ToString());
                        dtPrint.Rows.Add("FWVER", dt_Soft_Fw_Ver.Rows[0]["FW_VER"].ToString());
                    }

                }
                dtPrint.Rows.Add("QTY", dtCarton.Rows.Count.ToString());
                double Ctn_weight = 0;
                DataTable dtKeyParts = new DataTable("mydtKeyParts");
                dtKeyParts.Columns.Add("ESN", typeof(string));
                dtKeyParts.Columns.Add("SNTYPE", typeof(string));
                dtKeyParts.Columns.Add("SNVAL", typeof(string));
                foreach (DataRow dr in dtCarton.Rows)
                {
                    Ctn_weight += Convert.ToDouble(dr["WEIGHTQTY"].ToString());
                    DataTable dtwipkeypart = ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyPart(dr["ESN"].ToString()));

                    foreach (DataRow drKeyParts in dtwipkeypart.Rows)
                    {
                        dtKeyParts.Rows.Add(drKeyParts["ESN"].ToString(), drKeyParts["SNTYPE"].ToString(), drKeyParts["SNVAL"].ToString());
                    }
                }
                dtPrint.Rows.Add("CARTONWEIGHT", (Ctn_weight/1000).ToString("0.00"));

                foreach (DataRow dr in Function.DataTableSort(Function.DataTableCross(dtKeyParts, 3), "SN").Rows)
                {
                    dtPrint.Rows.Add(dr[0].ToString(), dr[1].ToString());
                }
                string _My_LabelFilePatch = string.Empty;
                if (!imbt_LabelPartnumber.Checked)
                {
                    _My_LabelFilePatch = My_LabelFilePatch + "\\" + dtCarton.Rows[0]["WOID"].ToString() + "\\" + MYGROUP + ".lab";
                }
                else
                {
                    _My_LabelFilePatch = My_LabelFilePatch + "\\"+ dtCarton.Rows[0]["PARTNUMBER"].ToString() + ".lab";
                }
                CsPrint = new CodeSoftPrintLabel(PublicPrintLabel);
                CsPrint.BeginInvoke(dtPrint, _My_LabelFilePatch, null, null);
            }
            else
            {
                SendMsg(mLogMsgType.Error, "No Data,Can Not Print !! ", "没有信息,不能打印!!");
            }

        }

        private void imbt_PrintCtnLabel_Click(object sender, EventArgs e)
        {
            string _SysUserId = string.Empty;
            if (Check_Setting_Employee("Set_PrintLabel", out _SysUserId))
            {
                imbt_PrintCtnLabel.Checked = !imbt_PrintCtnLabel.Checked;
                if (imbt_PrintCtnLabel.Checked)
                {
                    InsertSystemLog(_SysUserId, "PrintLabel", "PrintLabel Open");
                    SendMsg(mLogMsgType.Warning, "PrintLabel Open", "打印条码");
                    OperateIni.IniWriteValue("PACK_CTN", "PrintLabel", "0", IniFilePath);
                }
                else
                {
                    InsertSystemLog(_SysUserId, "PrintLabel", "PrintLabel Close");
                    SendMsg(mLogMsgType.Warning, "PrintLabel Close", "关闭打印");
                    OperateIni.IniWriteValue("PACK_CTN", "PrintLabel", "1", IniFilePath);
                }
            }
        }

        private void imbt_PrintProduct_Click(object sender, EventArgs e)
        {
            string _SysUserId = string.Empty;
            if (Check_Setting_Employee("Set_PrintProduct", out _SysUserId))
            {
                imbt_PrintProduct.Checked = !imbt_PrintProduct.Checked;
                if (imbt_PrintProduct.Checked)
                {
                    InsertSystemLog(_SysUserId, "PrintProduct", "PrintProduct Open");
                    SendMsg(mLogMsgType.Warning, "PrintProduct Open", "打印产品信息");
                    OperateIni.IniWriteValue("PACK_CTN", "PrintProduct", "0", IniFilePath);
                }
                else
                {
                    InsertSystemLog(_SysUserId, "PrintProduct", "PrintProduct Close");
                    SendMsg(mLogMsgType.Warning, "PrintProduct Close", "打印产品信息关闭");
                    OperateIni.IniWriteValue("PACK_CTN", "PrintProduct", "1", IniFilePath);
                }
            }
        }

        private void imbt_LabelPartnumber_Click(object sender, EventArgs e)
        {
            imbt_LabelPartnumber.Checked = !imbt_LabelPartnumber.Checked;
            if (imbt_LabelPartnumber.Checked)
            {
                OperateIni.IniWriteValue("PACK_CTN", "PatchFlag", "0", IniFilePath);
                imbt_Labelwoid.Checked = false;
            }
            else
                imbt_LabelPartnumber.Checked = true;
        }

        private void imbt_Labelwoid_Click(object sender, EventArgs e)
        {
            imbt_Labelwoid.Checked = !imbt_Labelwoid.Checked;
            if (imbt_Labelwoid.Checked)
            {
                OperateIni.IniWriteValue("PACK_CTN", "PatchFlag", "1", IniFilePath);
                imbt_LabelPartnumber.Checked = false;
            }
            else
                imbt_Labelwoid.Checked = true;
        }
        private delegate void CodeSoftPrintLabel(DataTable dt, string FilePath);
        CodeSoftPrintLabel CsPrint;
        public void PublicPrintLabel(DataTable dt, string filepatch)
        {
            StripStatusLabelPatch.Text = "Label File: " + filepatch;

            string PrintQty = OperateIni.IniReadValue("PACK_CTN", "LabelQty", IniFilePath);
            string coordinateX = OperateIni.IniReadValue("PACK_CTN", "LabelX", IniFilePath);
            string coordinateY = OperateIni.IniReadValue("PACK_CTN", "LabelY", IniFilePath);

            if (!File.Exists(filepatch))  //判断条码文件是否存在
            {
                SendMsg(mLogMsgType.Error, "Label File Not Found:" + filepatch, "条码档没有找到,路径:" + filepatch);
                return;
            }
            ApplicationClass lbl = new ApplicationClass();
            try
            {

                lbl.Documents.Open(filepatch, false);// 调用设计好的label文件
                Document doc = lbl.ActiveDocument;
                SendMsg(mLogMsgType.Incoming, "Clear Template Variable... ", "清空模板变量...");
                for (int i = 0; i < doc.Variables.FormVariables.Count; i++)
                {
                    doc.Variables.FormVariables.Item(doc.Variables.FormVariables.Item(i + 1).Name).Value = "";
                }
                SendMsg(mLogMsgType.Incoming, string.Format("Variable Clear OK,Total [{0}]", doc.Variables.FormVariables.Count), string.Format("模板变量清空完成,共计{0}个...", doc.Variables.FormVariables.Count));
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        doc.Variables.FormVariables.Item(dr[0].ToString()).Value = dr[1].ToString(); //给参数传值                     
                        SendMsg(mLogMsgType.Outgoing, string.Format("Fill Variable {0}->{1}", dr[0].ToString(), dr[1].ToString()), string.Format("填充打印变量完成:{0}->{1}", dr[0].ToString(), dr[1].ToString()));
                    }
                    catch
                    {
                    }
                }

                int Num = Convert.ToInt32(PrintQty);        //打印数量
                doc.Format.MarginLeft =Convert.ToInt32( (Convert.ToDecimal(coordinateX)) * 100);
                doc.Format.MarginTop = Convert.ToInt32((Convert.ToDecimal(coordinateY)) * 100);
                doc.PrintDocument(Num);               //打印
                SendMsg(mLogMsgType.Normal, "打印完成");
            }
            catch (Exception ex)
            {
                SendMsg(mLogMsgType.Error, "Execpt: " + ex.Message, "发生异常" + ex.Message);
            }
            finally
            {
                lbl.Quit(); //退出
            }          

        }

        private void imbt_ChkWeight_Click(object sender, EventArgs e)
        {
            string _SysUserId = string.Empty;
            if (Check_Setting_Employee("Set_Weight", out _SysUserId))
            {
                imbt_ChkWeight.Checked = !imbt_ChkWeight.Checked;
                if (!imbt_ChkWeight.Checked)
                {
                    InsertSystemLog(_SysUserId, "Weight", "Weighing Cancellation");
                    SendMsg(mLogMsgType.Warning, "Weighing Cancellation", "取消称重");
                }
                else
                {
                    InsertSystemLog(_SysUserId, "Weight", "Weighing Open");
                    SendMsg(mLogMsgType.Warning, "Weighing Open", "打开称重");
                }
            }
        }

        private void imbt_CloseCarton_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(lab_CartonCount.Text) != Convert.ToInt32(lab_CartonCapacity.Text))
            {
                if (MessageBox.Show("Are You Sure Close This Carton ? ", "Close Carton", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // SendMsg(mLogMsgType.Warning, "Carton Full", "箱号已满");
                    imbt_CloseCarton.Enabled = false;
                    Close_Carton(My_Carton_No);
                    My_CartonQtyOnDB = 0;
                    PShowScreenInformation();
                    txt_sn.Text = string.Empty;
                    txt_sn.Focus();
                    if (imbt_PrintCtnLabel.Checked)
                    {
                        string PrintQty = OperateIni.IniReadValue("PACK_CTN", "LabelQty", IniFilePath);
                        PrintCartonLabel_CodeSoft(My_Carton_No);
                    }
                }
            }
        }

        private void imbt_Labelqty_Click(object sender, EventArgs e)
        {
            Frm_LabelQty Flq = new Frm_LabelQty(IniFilePath);
            Flq.ShowDialog();
        }

        private void imbt_ManualCloseCarton_Click(object sender, EventArgs e)
        {
            Frm_CloseCarton fcc = new Frm_CloseCarton(this);
            fcc.ShowDialog();
        }

        private void imbt_RePrint_Click(object sender, EventArgs e)
        {
            string _SysUserId = string.Empty;
            if (Check_Setting_Employee("Set_RePrint", out _SysUserId))
            {
                Frm_Reprint fr = new Frm_Reprint(this);
                fr.ShowDialog();
            } 
        }

        private void imbt_Printcoordinates_Click(object sender, EventArgs e)
        {
            string _SysUserId = string.Empty;
            if (Check_Setting_Employee("Set_Print_Coordinate", out _SysUserId))
            {
                Frm_Printcoordinates fpd = new Frm_Printcoordinates(IniFilePath);
                fpd.ShowDialog();
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int GetKeyState(int nVirtKey);

        private void imbt_English_Click(object sender, EventArgs e)
        {
            imbt_English.Checked = true;

            Language = 0;
            OperateIni.IniWriteValue("PACK_CTN", "Language", "0", IniFilePath);
            SendMsg(mLogMsgType.Warning, "The language is English", "当前语言为英文");
            imbt_Chinese.Checked = false;          
        }

        private void imbt_Chinese_Click(object sender, EventArgs e)
        {
            imbt_Chinese.Checked = true;
            Language = 1;
            OperateIni.IniWriteValue("PACK_CTN", "Language", "1", IniFilePath);
            SendMsg(mLogMsgType.Warning, "The language is Chinese", "当前语言为中文");
            imbt_English.Checked = false;            
        }

        #region
        string mCHECK_ROUTE;
        string mROLLBACK_ROUTE;
        int mCHECK_TIMEOUT;
        string mREST_TIME;
        private bool Get_t_Check_Timeout()
        {
            DataTable dtCheckNo = ReleaseData.arrByteToDataTable(refWebt_Check_Timeout.Instance.Get_t_Check_Timeout(My_Check_No));
            if (dtCheckNo.Rows.Count > 0)
            {
                mCHECK_ROUTE = dtCheckNo.Rows[0][1].ToString();
                mROLLBACK_ROUTE = dtCheckNo.Rows[0][2].ToString();
                mCHECK_TIMEOUT = Convert.ToInt32(dtCheckNo.Rows[0][3].ToString());
                mREST_TIME = dtCheckNo.Rows[0][4].ToString();
                SendMsg(mLogMsgType.Incoming, string.Format(" CHECK_ROUTE[{0}]\r\n Rollback[{1}]\r\n TimeOut[{2}]Min \r\n RestTime[{3}]", mCHECK_ROUTE, mROLLBACK_ROUTE, mCHECK_TIMEOUT.ToString(), mREST_TIME), string.Format("检查途程[{0}]\r\n退回途程[{1}]\r\n超时时间[{2}]分钟\r\n休息时间[{3}]", mCHECK_ROUTE, mROLLBACK_ROUTE, mCHECK_TIMEOUT.ToString(), mREST_TIME));
                return true;
            }
            else
            {
                SendMsg(mLogMsgType.Error, string.Format("Check timeout no data[{0}]", My_Check_No), string.Format("检查超时无数据 [{0}]", My_Check_No));
                return false;
            }
        }

        private bool Check_RollBack_Station()
        {
            if (My_Check_No != "NA" && !string.IsNullOrEmpty(My_Check_No))
            {
                if (!Get_t_Check_Timeout() )
                    return false;
                string dtTime = string.Empty;
                System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "yyyy-MM-dd hh:mm:ss";
                DataTable dtsnDetail = ReleaseData.arrByteToDataTable(refWebtWipDetail.Instance.GetWipDetail(My_ESN));
                DataView dv = dtsnDetail.DefaultView;
                dv.Sort = string.Format("{0} DESC", "RECDATE");
                DataTable dtTempSnDetail = dv.ToTable();

                foreach (DataRow dr in dtTempSnDetail.Rows)
                {
                    if (dr["WOID"].ToString() == My_MoNumber && dr["LOCSTATION"].ToString() == mCHECK_ROUTE)
                    {
                        dtTime = dr["RECDATE"].ToString();
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(dtTime))
                {                    
                    Now_DateTime = refWebtGetServersTime.Instance.GetServersTime();
                    System.DateTime EsnTime = Convert.ToDateTime(dtTime, dtFormat);

                    #region  转换时间
                    SendMsg(mLogMsgType.Warning, string.Format("{0} Time {1}", mCHECK_ROUTE, EsnTime.ToString()), string.Format("{0}时间{1}", mCHECK_ROUTE, EsnTime.ToString()));
                    EsnTime = CHECK_REST_TIME_PRODUCT(mREST_TIME, EsnTime);
                    SendMsg(mLogMsgType.Warning,string.Format("{0} Conver Time {1}", mCHECK_ROUTE, EsnTime.ToString()), string.Format("{0}时间转换{1}", mCHECK_ROUTE, EsnTime.ToString()));
                    SendMsg(mLogMsgType.Outgoing, string.Format("Time Now {0}", Now_DateTime.ToString()), string.Format("当前时间{0}", Now_DateTime.ToString()));
                    #endregion
                    TimeSpan ts = Now_DateTime - EsnTime;
                    int d = Convert.ToInt32(Math.Round(ts.TotalMinutes));

                    if (d > mCHECK_TIMEOUT)
                    {
                        if (CHECK_REST_TIME(mREST_TIME, EsnTime))
                        {
                            //string ss = string.Format("相差时间[{0}]", d.ToString());
                            //MessageBox.Show(ss);
                        }
                        else
                        {
                            string _RollBackStr = refWebtWipTracking.Instance.RollBack_Station(My_ESN, mROLLBACK_ROUTE);
                            if (_RollBackStr == "OK")
                            {
                                SendMsg(mLogMsgType.Error, string.Format(string.Format("{1}Time Out" + mCHECK_TIMEOUT.ToString() + " Minute [{0}]", d.ToString(), mCHECK_ROUTE)), string.Format(string.Format("{1}到达当前途程超过" + mCHECK_TIMEOUT.ToString() + "分钟[{0}]", d.ToString(), mCHECK_ROUTE)));
                                return false;
                            }
                            else
                            {
                                SendMsg(mLogMsgType.Error,string.Format("Rollback Route Error:[{0}]", _RollBackStr), string.Format("退回途程异常:[{0}]", _RollBackStr));
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private bool CHECK_REST_TIME(string REST_TIME, System.DateTime dt)
        {
           
            System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy-MM-dd hh:mm:ss";
            foreach (string sTime in REST_TIME.Split('|'))
            {
                string StartTime = sTime.Split('~')[0];
                string EndTime = sTime.Split('~')[1];
                TimeSpan ts = Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + EndTime, dtFormat) - Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + StartTime, dtFormat);//计算差异时间(分钟)
                if (ts.TotalMinutes < 0)  //如果遇到23:50 到明天的00:40 则两个时间相减会是负数,那么它的差异时间加上1440分钟后就会获得两时间实际正常差异时间
                {
                    if (dt.AddMinutes(ts.TotalMinutes + 1440).Date != Now_DateTime.Date)  //加上差异时间如果日期与现在的日期不相等,则是产品现在的时间加上差异时间应该与23:50,加上一天的00:40比较
                    {
                        if (dt.AddMinutes(ts.TotalMinutes + 1440) >= Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + StartTime, dtFormat) && dt.AddMinutes(ts.TotalMinutes + 1440) <= Convert.ToDateTime(Now_DateTime.AddDays(1).ToString("yyyy-MM-dd") + " " + EndTime, dtFormat))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (dt.AddMinutes(ts.TotalMinutes + 1440) >= Convert.ToDateTime(Now_DateTime.AddDays(-1).ToString("yyyy-MM-dd") + " " + StartTime, dtFormat) && dt.AddMinutes(ts.TotalMinutes + 1440) <= Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + EndTime, dtFormat))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (dt.AddMinutes(ts.TotalMinutes) >= Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + StartTime, dtFormat) && dt.AddMinutes(ts.TotalMinutes) <= Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + EndTime, dtFormat))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// 遇到休息时间做的产品,将产品实际生产时间修改为休息时间的前一分钟
        /// </summary>
        /// <param name="REST_TIME"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private System.DateTime CHECK_REST_TIME_PRODUCT(string REST_TIME, System.DateTime dt)
        {         
            System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy-MM-dd hh:mm:ss";
            foreach (string  sTime in REST_TIME.Split('|'))
            {
                string StartTime = sTime.Split('~')[0];
                string EndTime = sTime.Split('~')[1];
                TimeSpan ts = Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + EndTime, dtFormat) - Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + StartTime, dtFormat);//计算差异时间(分钟)
                if (ts.TotalMinutes < 0)  //如果遇到23:50 到明天的00:40 则两个时间相减会是负数,那么它的差异时间加上1440分钟后就会获得两时间实际正常差异时间
                {
                    if (dt.AddMinutes(ts.TotalMinutes + 1440).Date != Now_DateTime.Date)  //加上差异时间如果日期与现在的日期不相等,则是产品现在的时间加上差异时间应该与23:50,加上一天的00:40比较
                    {
                        if (dt >= Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + StartTime, dtFormat) && dt <= Convert.ToDateTime(Now_DateTime.AddDays(1).ToString("yyyy-MM-dd") + " " + EndTime, dtFormat))
                        {
                            return Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + StartTime, dtFormat);
                        }
                    }
                    else
                    {
                        if (dt >= Convert.ToDateTime(Now_DateTime.AddDays(-1).ToString("yyyy-MM-dd") + " " + StartTime, dtFormat) && dt <= Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + EndTime, dtFormat))
                        {
                            return Convert.ToDateTime(Now_DateTime.AddDays(-1).ToString("yyyy-MM-dd") + " " + StartTime, dtFormat);
                        }
                    }
                }
                else
                {
                    if (dt >= Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + StartTime, dtFormat) && dt <= Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + EndTime, dtFormat))
                    {
                        return Convert.ToDateTime(Now_DateTime.ToString("yyyy-MM-dd") + " " + StartTime, dtFormat);
                    }
                }
            }

            return dt;
        }

        #endregion

        private void imbt_OrderlyPacking_Click(object sender, EventArgs e)
        {
            imbt_OrderlyPacking.Checked = !imbt_OrderlyPacking.Checked;
            if (imbt_OrderlyPacking.Checked)
                OperateIni.IniWriteValue("PACK_CTN", "OrderlyPacking", "0", IniFilePath);
            else
                OperateIni.IniWriteValue("PACK_CTN", "OrderlyPacking", "1", IniFilePath);

        }

        private void imbt_NoCloseCtn_Click(object sender, EventArgs e)
        {
            Frm_NoCloseCtn fnc = new Frm_NoCloseCtn();
            fnc.ShowDialog();
        }

        private void Frm_Packing_Ctn_SizeChanged(object sender, EventArgs e)
        {
            //asc.controlAutoSize(this);
        }
        private bool Check_Setting_Employee(string funid, out string UserId)
        {
            bool Chk_Flag = false;
            UserId = null;
            string[] EmpData = InputBox.ShowInputBox("Input Employee", string.Empty);
            if (string.IsNullOrEmpty(EmpData[0]))
            {
                SendMsg(mLogMsgType.Error, "USER ID IS NULL !!", "用户名不能为空!!");
                return false;
            }
            if (string.IsNullOrEmpty(EmpData[1]))
            {
                SendMsg(mLogMsgType.Error, "USER Password IS NULL !!", "用户密码不能为空!!");
                return false;
            }
            DataTable _dt = ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo(EmpData[0], null, EmpData[1]));

            if (_dt == null || _dt.Rows.Count < 1 )
            {
                SendMsg(mLogMsgType.Error, "USER_ID OR USER_PWD ERROR !!", "用户名或密码输入错误 请重新输入!!");
            }
            else
            {
                DataTable userPopList = ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserJurisdictionByUserId(EmpData[0]));
                DataRow[] ArrDr = userPopList.Select(string.Format("progid='PACK_CTN' and funId='{0}'", funid));
                if ((ArrDr == null || ArrDr.Length < 1))
                {
                    SendMsg(mLogMsgType.Error, "Please Call Test Engineer !!", "请联系TE人员, 请重新输入!!");
                }
                else
                {
                    UserId = EmpData[0];
                    Chk_Flag = true;
                }
            }
            return Chk_Flag;
        }
        private void InsertSystemLog(string SysUserId, string Action_Type, string Action_Desc)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("USERID", SysUserId);
            dic.Add("PRG_NAME", "PACK_CTN");
            dic.Add("ACTION_TYPE", Action_Type);
            dic.Add("ACTION_DESC", Action_Desc + " ->MAC:" + Function.getMacList()[0]);          
            refWebRecodeSystemLog.Instance.InsertSystemLog(MapListConverter.DictionaryToJson(dic));
        }
        private bool CHECK_PRODUCT_LINE()
        {
            //bool flag = false;
            //foreach (string str in ProductLine.Split(','))
            //{
            //    if (str == Line_Name)
            //    {
            //        flag = true;
            //        break;
            //    }
            //}
            //if (!flag)
            //    SendMsg(mLogMsgType.Error, string.Format("The WO is not available in the [{0}] line.", Line_Name), string.Format("此工单不可在{0}生产", Line_Name));
            //return flag;
            return true;
        }

        private void Frm_Packing_Ctn_FormClosing(object sender, FormClosingEventArgs e)
        {
            sPort.Close();
            sPort.Dispose();
        }
    }
}
