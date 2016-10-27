using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using WebServices.tPublicStoredproc;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;
using System.Net;
using System.Management;
using System.IO.Ports;
using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using WebServices.tCraftInfo;
using GenericUtil;

namespace SFIS_DCT_V2
{
    public partial class SFIS_DCT_V2 :  Office2007Form//Form
    {
        public SFIS_DCT_V2()
        {
            InitializeComponent();
        }
        tPublicStoredproc Pubstr = null;
        tCraftInfo craft = null;

        System.IO.Ports.SerialPort sPort= null;

       // System.IO

        byte[] by = new byte[1024];
        System.Windows.Forms.Timer AutoRun = new System.Windows.Forms.Timer();
        string ReadPath = "";
        string MovePath = "";
        string FailMovePath = "";
        string LabDir = "I";
        bool PortFlag = false;
        WebServices.Check_Version.Check_Version chkver = null;
        WebServices.tWipTracking.tWipTracking twip = new WebServices.tWipTracking.tWipTracking();
        private string appfilename = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
        private void SFIS_DCT_V2_Load(object sender, EventArgs e)
        {
          
            chkver = new WebServices.Check_Version.Check_Version();
            string _StrErr = string.Empty;
            if (!chkver.CheckPrgVsersion("SFIS_DCT_V3", Application.ProductVersion, null, null, null, out _StrErr))
            {
                if (_StrErr == "OK")
                {
                    MessageBox.Show("该程序为版本不是最新版\r\n请更新后运行");
                    RunFile(System.Windows.Forms.Application.StartupPath + @"\", "AutoUpdate.exe", appfilename);
                }
                else
                {
                    MessageBox.Show("检查程序版本异常\r\n "+_StrErr);
                }
                    string FileName = System.IO.Path.GetFileName(Application.ExecutablePath);
                    Process[] prc = Process.GetProcessesByName(FileName.Substring(0, FileName.LastIndexOf('.')));
                    if (prc.Length > 0)
                        foreach (Process pc in prc)
                        {
                            pc.Kill();
                        }
               
                return;
            }

            this.Text = "SFIS_DCT_V3  Version: " + Application.ProductVersion;
            tbMessage.Text = "正在初始......";
            this.PalConfig.Enabled = true;
            this.PalData.Enabled = true;
            Initialization = new InitializationPrg(InitializationInfo);
            Initialization.BeginInvoke(null, null);

            AutoRun.Interval = 100;
            AutoRun.Enabled = false;
            AutoRun.Tick += new EventHandler(AutoRun_Tick);
            btsetup.Enabled = false;


            string filePath = System.Environment.CurrentDirectory + "\\SFIS_DCT.ini";
            string _Port = ReadIniFile.IniReadValue("DATABASE", "myport", filePath);
            string _BaudRate = ReadIniFile.IniReadValue("DATABASE", "BaudRate", filePath);
            string _DataBits = ReadIniFile.IniReadValue("DATABASE", "DataBits", filePath);
            string _Parity = ReadIniFile.IniReadValue("DATABASE", "Parity", filePath);
            string _StopBits = ReadIniFile.IniReadValue("DATABASE", "StopBits", filePath);
            string _ReadBufferSize = ReadIniFile.IniReadValue("DATABASE", "ReadBufferSize", filePath);
            LabDir = ReadIniFile.IniReadValue("LABEL", "Patch", filePath);
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
                sPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(sPort_DataReceived);
                sPort.Open();
                comstatus.Text = "COM OK";
                PortFlag = true;
            }
            catch
            {
                comstatus.Text = "COM FAIL";
                PortFlag = false;
            }        


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

        /// <summary>
        /// RS232执行到第几步存储过程
        /// </summary>
        int RS232ExeSp = 1;
        /// <summary>
        /// RS232输入参数
        /// </summary>
        string RS232InputData = string.Empty;

        private void sPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            sPort.Read(by, 0, by.Length);
            this.Invoke(new SetTexts(SetText));
        }
        private delegate void SetTexts();
        public void SetText()
        {

           // tb_rs232.Text = null;
           // label7.Text = null;
           // tb_rs232.Text = Encoding.ASCII.GetString(by).TrimEnd('\0');
           // label7.Text = Encoding.ASCII.GetString(by).TrimEnd('\0');
            string RS232Data = "";
            RS232Data = Encoding.ASCII.GetString(by).TrimEnd('\0');
            foreach (byte x in by)
            {

                if (x == 13)
                {
                    //tbInputData_KeyDown(null, new KeyEventArgs(Keys.Enter));

                    RS232InputDataExecuteSp(RS232Data);
                    Array.Clear(by, 0, by.Length);
                    break;
                }
            }


        }


        private delegate void InitializationPrg();
        InitializationPrg Initialization;

        private void InitializationInfo()
        {
            try
            {
                Pubstr = new tPublicStoredproc();
                craft = new tCraftInfo();
                GetLineList();
               // GetWoList();                
                GetProcedures();
                GetStationList();

                tbMessage.Invoke(new EventHandler(delegate
                {
                    tbMessage.Text = "初始化完成......";
                }));
                this.PalConfig.Enabled = true;
                this.PalData.Enabled = true;
            }
            catch
            {
                tbMessage.Invoke(new EventHandler(delegate
                {
                    tbMessage.Text = "初始化失败......";
                }));
            }
        }

        #region 获取初始值
        private void GetLineList()
        {
            this.Invoke(new EventHandler(delegate{
            List<string> LsLine = Pubstr.Get_Line_List().ToList();
            foreach (string str in LsLine)
            {              
               cbline.Items.Add(str);                   
            }
            if (cbline.Items.Count > 0)
                cbline.SelectedIndex = 0;
            }));
        }


        private void GetStationList()
        {
            this.Invoke(new EventHandler(delegate
            {           

                Dictionary<string, object> dic = new Dictionary<string, object>();
          
                DataTable dt = ReleaseData.arrByteToDataTable(craft.Get_Craft_Info(MapListConverter.DictionaryToJson(dic)));
                DataView dv = dt.DefaultView;
                dv.Sort = "craftname Asc";
                DataTable dt2 = dv.ToTable();

                foreach (DataRow dr in dt2.Rows)
                {
                    if (dr["TESTFLAG"].ToString() == "0" || dr["TESTFLAG"].ToString() == "3")
                        cbroute.Items.Add(dr["craftname"].ToString());
                }
                if (cbroute.Items.Count > 0)
                    cbroute.SelectedIndex = 0;
            }));
        }
        //private void GetWoList()
        //{
        //    string[] ls = Pubstr.GetWoList();
        //    for (int i = 0; i < ls.Length; i++)
        //    {
        //        cbwo.Invoke(new EventHandler(delegate
        //        {
        //            cbwo.Items.Add(ls[i]);
        //        }));
        //    }
        //}
        private void GetProcedures()
        {
            this.Invoke(new EventHandler(delegate
            {
                DataTable dt = Pubstr.GetListStationType().Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    cbprocedures.Items.Add(dt.Rows[i][0].ToString() + " " + dt.Rows[i][1].ToString());
                    cbproback.Items.Add(dt.Rows[i][0].ToString() + " " + dt.Rows[i][1].ToString());

                }
                if (cbprocedures.Items.Count > 0)
                    cbprocedures.SelectedIndex = 0;
                if (cbproback.Items.Count > 0)
                    cbproback.SelectedIndex = 0;
            }));
        }

        public DataTable getNewTable(DataTable dt, string sql)
        {
            try
            {
                DataTable mydt = new DataTable();
                mydt = dt.Clone();
                DataRow[] arrDr = dt.Select(sql);
                for (int i = 0; i < arrDr.Length; i++)
                {
                    mydt.ImportRow((DataRow)arrDr[i]);
                }
                return mydt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cbroute_TextChanged(object sender, EventArgs e)
        {
            if (PalConfig.Visible)
            {
              //  DataTable dt = getNewTable(dtRoute, string.Format("craftname='{0}'", cbroute.Text.Trim()));
              //  lbstationid.Text = cbroute.Text;
            }
        }
     

        private void cbwo_TextChanged(object sender, EventArgs e)
        {          
           
        }
        #endregion

        /// <summary>
        /// 存储获取需要执行的全部存储过程
        /// </summary>
        DataTable dtSp = null;
        /// <summary>
        /// RS232存储获取需要执行的全部存储过程
        /// </summary>
        DataTable RS232dtSp = null;
        /// <summary>
        /// 存储获取系统全部需要的参数
        /// </summary>
        DataTable SysAllData = null;
        /// <summary>
        /// RS232存储获取系统全部需要的参数
        /// </summary>
        DataTable RS232SysAllData = null;
        /// <summary>
        /// 存储过程需要的参数
        /// </summary>
        DataTable SPvalues = null;

        string SpRes = "";
        string Msg = "";
        /// <summary>
        /// 传入串口信息
        /// </summary>
        string SendRS232 = "";

        private void ShowMessage(string Msg1, string Msg2,string Msg3)
        {
            tbMessage.Text = "     " + Msg1 + "  \r\n                          " + Msg2+" \r\n"+Msg3;
        }
        private void Butok_Click(object sender, EventArgs e)
        {            
            if (string.IsNullOrEmpty(cbline.Text))
            {
                ShowMessage("线别未选择", "请选择线别","");
                return;
            }
            //if (string.IsNullOrEmpty(cbwo.Text)) 
            //{
            //    ShowMessage("工单未选择", "请选择工单","");
            //    return;
            //}
            if (string.IsNullOrEmpty(cbprocedures.Text)) 
            {
                ShowMessage("行为模式未选择", "请选择行为模式","");
                return;
            }
            if (string.IsNullOrEmpty(cbroute.Text)) 
            {
                ShowMessage("当前途程未选择", "请选择途程","");
                return;
            }

            this.Text = "SFIS_DCT--" + "途程: " + cbroute.Text + "--线别: "+cbline.Text;
            PalConfig.Visible = false;
            tbMessage.Size = new Size(380, this.Size.Height - 110); 
            dtSp = Pubstr.GetSystemStoredproc(Convert.ToInt32(ExcuteSp)).Tables[0];//.GetStoredproc(ExcuteSp).Tables[0]; //找出需要执行那些存储过程
            RS232dtSp = Pubstr.GetSystemStoredproc(Convert.ToInt32(RS232ExcuteSp)).Tables[0];
            GetSystemInputAllData();// 获取系统全部需要的参数
            tbInputData.Text = "";
            tbInputData.Focus();
            btsetup.Enabled = true;
        }

        /// <summary>
        /// 选择的行为模式代码
        /// </summary>
        string ExcuteSp = "";
        private void cbprocedures_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string str = cbprocedures.Text.Trim();
                int i = str.IndexOf(' ');
                ExcuteSp = str.Substring(0, i);
                tbMessage.Text = "行为模式选择正确";
            }
            catch (Exception)
            {
                tbMessage.Text = "行为模式选择错误";
                cbprocedures.Focus();

            }
        }
        /// <summary>
        /// RS232选择的行为模式代码
        /// </summary>
        string RS232ExcuteSp = "";
        private void cbproback_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string str = cbproback.Text.Trim();
                int i = str.IndexOf(' ');
                RS232ExcuteSp = str.Substring(0, i);
                tbMessage.Text = "行为模式选择正确";
            }
            catch (Exception)
            {
                tbMessage.Text = "行为模式选择错误";
                cbproback.Focus();

            }
        }

        /// <summary>
        /// 获取系统全部需要的参数
        /// </summary>
        private void GetSystemInputAllData()   
        {
            SysAllData = Pubstr.GetSystemInputData().Tables[0];
            SysAllData.Columns.Add("data");
            SysAllData.Columns.Add("InRam");
            for (int i = 0; i < SysAllData.Rows.Count; i++)
            {
                SysAllData.Rows[i]["data"] = "NA";

                if (SysAllData.Rows[i]["param"].ToString().ToUpper() == "MYGROUP")
                {
                    SysAllData.Rows[i]["data"] = cbroute.Text;
                    SysAllData.Rows[i]["InRam"] = "1";
                }
                //if (SysAllData.Rows[i]["param"].ToString().ToUpper() == "WO")
                //{
                //    SysAllData.Rows[i]["data"] = cbwo.Text.Trim();
                //    SysAllData.Rows[i]["InRam"] = "1";
                //}
                if (SysAllData.Rows[i]["param"].ToString().ToUpper() == "LINE")
                {
                    SysAllData.Rows[i]["data"] = cbline.Text.Trim();
                    SysAllData.Rows[i]["InRam"] = "1";
                }

                if (SysAllData.Rows[i]["param"].ToString().ToUpper() == "MACADDRESS")
                {
                    SysAllData.Rows[i]["data"] = GetMac();
                    SysAllData.Rows[i]["InRam"] = "1";
                }
                if (SysAllData.Rows[i]["param"].ToString().ToUpper() == "IPADDRESS")
                {
                    SysAllData.Rows[i]["data"] = GetIpAddress();
                    SysAllData.Rows[i]["InRam"] = "1";
                }
                if (SysAllData.Rows[i]["param"].ToString().ToUpper() == "PCNAME")
                {
                    SysAllData.Rows[i]["data"] = GetHostName();
                    SysAllData.Rows[i]["InRam"] = "1";
                }   

                for (int x = 0; x < dtSp.Rows.Count; x++)
                {
                    if (SysAllData.Rows[i]["param"].ToString().ToUpper() == dtSp.Rows[x]["param"].ToString())
                    {
                        SysAllData.Rows[i]["InRam"] = dtSp.Rows[x]["InRam"].ToString();
                    }
                }

               

            }

        }
        /// <summary>
        /// 修改系统SysAllData内参数
        /// </summary>
        /// <param name="Conum"></param>
        /// <param name="InData"></param>
        private void ModifySysDataTable(string Conum, string InData)
        {
            for (int i = 0; i < SysAllData.Rows.Count; i++)
            {
                if (SysAllData.Rows[i]["param"].ToString().ToUpper() == Conum)
                {
                    SysAllData.Rows[i]["data"] = InData;
                }

            }
        }

        /// <summary>
        /// RS232修改系统SysAllData内参数
        /// </summary>
        /// <param name="Conum"></param>
        /// <param name="InData"></param>
        private void RS232ModifySysDataTable(string Conum, string InData)
        {
            for (int i = 0; i < RS232SysAllData.Rows.Count; i++)
            {
                if (RS232SysAllData.Rows[i]["param"].ToString().ToUpper() == Conum)
                {
                    RS232SysAllData.Rows[i]["data"] = InData;
                }

            }
        }

        /// <summary>
        /// 获取存储过程需要的参数,并增加变量值
        /// </summary>
        /// <param name="Stro"></param>
        private void GetStoredprocValues(string Stro)
        {
            //SPvalues = Pubstr.GetStoredprocValues(Stro).Tables[0];
            SPvalues = Pubstr.GetStoredProcValuesParam(Stro).Tables[0];
            SPvalues.Columns.Add("data");
            for (int i = 0; i < SPvalues.Rows.Count; i++)
            {
                for (int x = 0; x < SysAllData.Rows.Count; x++)
                {
                    if (SPvalues.Rows[i][0].ToString() == SysAllData.Rows[x]["code"].ToString())
                    {
                        SPvalues.Rows[i]["data"] = SysAllData.Rows[x]["data"].ToString();
                    }
                }
            }

        }
        /// <summary>
        /// RS232获取存储过程需要的参数,并增加变量值
        /// </summary>
        /// <param name="Stro"></param>
        private void RS232GetStoredprocValues(string Stro)
        {
            //SPvalues = Pubstr.GetStoredprocValues(Stro).Tables[0];
            SPvalues = Pubstr.GetStoredProcValuesParam(Stro).Tables[0];
            SPvalues.Columns.Add("data");
            for (int i = 0; i < SPvalues.Rows.Count; i++)
            {
                for (int x = 0; x < RS232SysAllData.Rows.Count; x++)
                {
                    if (SPvalues.Rows[i][0].ToString() == RS232SysAllData.Rows[x]["code"].ToString())
                    {
                        SPvalues.Rows[i]["data"] = RS232SysAllData.Rows[x]["data"].ToString();
                    }
                }
            }

        }

        /// <summary>
        /// 执行到第几步存储过程
        /// </summary>
        int ExeSp = 1;
        /// <summary>
        /// 输入参数
        /// </summary>
        string InputData = string.Empty;
        private void tbInputData_KeyDown(object sender, KeyEventArgs e)
        {
            if  (e.KeyCode==Keys.Enter)
            {
                tbInputData.SelectAll();
                if (!string.IsNullOrEmpty(tbInputData.Text))
                {
                   InputData = tbInputData.Text.Trim().ToUpper();
                   if (chkimei.Checked)
                   {
                       DataTable dtImei =ReleaseData.arrByteToDataTable( twip.GetQueryWipAllInfo("IMEI", InputData));
                       if (dtImei.Rows.Count > 0)
                       {
                           InputData = dtImei.Rows[0]["ESN"].ToString();
                       }
                       else
                       {
                           ShowMessage("没有IMEI信息","","");
                               return;
                       }
                   }
                }

                if (InputData == "UNDO")
                {
                    cbline.Text = "";
                    cbroute.Text = "";
                   // cbwo.Text = "";
                    cbprocedures.Text = "";
                    PalConfig.Visible = true;
                    tbMessage.Size = new Size(380, this.PalData.Size.Height - 60);               
                    ExeSp = 1;
                    btsetup.Enabled = false;
                }
                else
                    if (InputData == "CLEAR")
                    {
                        for (int i = 0; i < SysAllData.Rows.Count; i++)
                        {
                            if (SysAllData.Rows[i]["InRam"].ToString() != "1")
                            {
                                SysAllData.Rows[i]["data"] = "NA";
                            }
                        }
                    }
                    else
                    {
                        if (PalConfig.Visible)
                        {
                            tbMessage.Text = "请先设置选项";
                            return;
                        }

                        SendRS232 = null;
                        InputDataExecuteSp(InputData);
                        ExeSp++;
                        ShowMessage(SpRes, getNewTable(dtSp, string.Format("StoredprocIdx='{0}'", ExeSp)).Rows[0][11].ToString() + "?",InputData+"  "+SpRes);//PARAM
                        if (!string.IsNullOrEmpty(SendRS232))
                            SendMsgToCom(SendRS232);                    
                        tbInputData.Text = "";
                    }
            }
        }


        private void InputDataExecuteSp(string Input)
        {
            ModifySysDataTable("DATA", Input.Trim());
            DataTable dt = getNewTable(dtSp, string.Format("StoredprocIdx='{0}'", ExeSp));
            dt.DefaultView.Sort = "WORK_TYPE_INDEX";
            dt = dt.DefaultView.ToTable();          

            foreach (DataRow dr in dt.Rows)
            {
                GetStoredprocValues(dr[1].ToString()); //Storedproc
              //  SpRes = Pubstr.SP_PublicStoredproc(dr[1].ToString(), SPvalues);             
                string[] LsRES = Pubstr.SP_PublicStoredprocParam(dr[1].ToString(),DataTableToDictionaryString(SPvalues));
               // string[] LsRES = Pubstr.ExecuteProcedure(dr[1].ToString(), SPvalues);
                foreach (string Sendmsg in LsRES)
                {
                    if (Sendmsg.Substring(0, 3) == "RES")
                    {
                        SpRes = Sendmsg.Substring(4, Sendmsg.Length - 4);
                    }
                    else
                    {
                        SendRS232 = Sendmsg.Substring(8, Sendmsg.Length - 8);
                    }
                }

             //   SpRes = LsRES[0];

                Msg = dr[11].ToString() + "   " + SpRes;

                if (dr[8].ToString() == "1")  //最后一枪清除资料
                {
                    RS232SysAllData = SysAllData;
                }

                if (SpRes == "OK")
                {   
                    if (dr[8].ToString() == "1")  //最后一枪清除资料
                    {
                        RS232SysAllData = SysAllData;
                        for (int i = 0; i < SysAllData.Rows.Count; i++)
                        {
                            if (SysAllData.Rows[i]["InRam"].ToString() != "1")
                            {
                                SysAllData.Rows[i]["data"] = "NA";
                            }
                        }
                        DataTable dtRam = getNewTable(dtSp, string.Format("InRam='{0}'", "1"));
                        ExeSp = dtRam.Rows.Count;
                    }
                    ModifySysDataTable(dr[11].ToString(), Input.Trim());//PARAM 更改值                 
                }
                else
                {
                    if (SpRes == null)
                    {
                        MessageBox.Show(" 执行存储过程 " + dr[1].ToString() + " 发生异常,请联系IT处理  \r\n ");
                        return;
                    }

                    if (dr[6].ToString() == "1") //Fork 不为必刷则再执行一次
                    {
                        ModifySysDataTable(dr[11].ToString(), "NA");//PARAM
                        ExeSp = Convert.ToInt32(dr[4].ToString());//SECOND 报错时回到第几枪 
                        InputDataExecuteSp(InputData);
                        return;
                    }
                    ExeSp =Convert.ToInt32(dr[4].ToString())-1; //SECOND 报错时回到第几枪                   
                    return;
                }
            } 
          //  ExeSp++;
        }


        private void RS232InputDataExecuteSp(string Input)
        {
            if (RS232SysAllData == null)
                return;

            RS232ModifySysDataTable("DATA", Input.Trim());
            DataTable dt = RS232dtSp;
            dt.DefaultView.Sort = "WORK_TYPE_INDEX";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow dr in dt.Rows)
            {
                RS232GetStoredprocValues(dr[1].ToString()); //Storedproc      

                string[] LsRES = Pubstr.SP_PublicStoredprocParam(dr[1].ToString(), DataTableToDictionaryString(SPvalues));
                foreach (string Sendmsg in LsRES)
                {
                    if (Sendmsg.Substring(1, 3) == "RES")
                    {
                        SpRes = Sendmsg.Substring(5, Sendmsg.Length - 5);
                    }
                    else
                    {
                        SendRS232 = Sendmsg.Substring(9, Sendmsg.Length - 9);
                    }
                }             

                Msg = dr[11].ToString() + "   " + SpRes;

                if (SpRes == "OK")
                {
                    if (dr[8].ToString() == "1")  //最后一枪清除资料
                    {
                        ShowMessage(SpRes, RS232dtSp.Rows[0][11].ToString() + "?",SpRes);
                        if (!string.IsNullOrEmpty(SendRS232))
                            SendMsgToCom(SendRS232);
                       

                        //for (int i = 0; i < SysAllData.Rows.Count; i++)
                        //{
                        //    if (SysAllData.Rows[i]["InRam"].ToString() != "1")
                        //    {
                        //        SysAllData.Rows[i]["data"] = "NA";
                        //    }
                        //}
                        //DataTable dtRam = getNewTable(dtSp, string.Format("InRam='{0}'", "1"));
                        //ExeSp = dtRam.Rows.Count;
                    }
                    //ModifySysDataTable(dr[11].ToString(), Input.Trim());//PARAM 更改值                 
                }
                else
                {
                    if (SpRes == null)
                    {
                        MessageBox.Show(" 执行存储过程 " + dr[1].ToString() + " 发生异常,请联系IT处理  \r\n ");
                        return;
                    }
                    ShowMessage(SpRes, RS232dtSp.Rows[0][11].ToString() + "?",SpRes);
                    if (!string.IsNullOrEmpty(SendRS232))
                        SendMsgToCom(SendRS232);

                    return;                                    
               
                }
            }
        
        }


        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
        string key, string def, StringBuilder retVal,
        int size, string filePath);

        public static void IniWriteValue(string Section, string Key, string Value)//对ini文件进行写操作的函数 
        {
            string filepath = System.IO.Directory.GetCurrentDirectory() + "\\SFIS_DCT.ini";
            WritePrivateProfileString(Section, Key, Value, filepath);
        }

        public static string IniReadValue(string Section, string Key)//对ini文件进行读操作的函数 
        {
            string filepath = System.IO.Directory.GetCurrentDirectory() + "\\SFIS_DCT.ini";
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
            255, filepath);
            return temp.ToString();
        }

     

        private string GetHostName()
        {
            return Dns.GetHostName();
        }
        private string GetIpAddress()
        {
            IPHostEntry MyEntry = Dns.GetHostByName(Dns.GetHostName());
            IPAddress MyAddress = new IPAddress(MyEntry.AddressList[0].Address);
            return MyAddress.ToString();
        }
        private string GetMac()
        {
           string madAddr = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc2 = mc.GetInstances();
            foreach (ManagementObject mo in moc2)
            {
                if (Convert.ToBoolean(mo["IPEnabled"]) == true)
                {
                    madAddr = mo["MacAddress"].ToString();
                  //  madAddr = madAddr.Replace(':', '-');
                }
                mo.Dispose();
            }
            return madAddr;            
        }

        private void btsetup_Click(object sender, EventArgs e)
        {
            if (btsetup.Text == "Auto")
            {
                btsetup.Text = "Manual";
                AutoRun.Enabled = false;
            }
            else
            {
              
                string Inifilepath = System.IO.Directory.GetCurrentDirectory() + "\\SFIS_DCT.ini";
                if (!System.IO.File.Exists(string.Format(Inifilepath)))
                {
                    MessageBox.Show("SFIS_DCT配置文件没有找到");
                    return;
                }
                ReadPath = IniReadValue("SFIS_DCT", "READPATH");
                MovePath = IniReadValue("SFIS_DCT", "MOVEPATH");
                FailMovePath = IniReadValue("SFIS_DCT", "FAILMOVEPATH");   
                btsetup.Text = "Auto";
                AutoRun.Enabled = true;
            }
          
        }
        private void AutoRun_Tick(object sender, EventArgs e)
        {
            AutoGetFileData();
        }

        //string snStartBit = "";
        //string ecStartBit = "";
        //string mchStartBit = "";
        private void AutoGetFileData()
        {
            string path1 = ReadPath; //读取文件夹内容  //Application.StartupPath + "\\图片\\图形再认\\" + a + "\\oldPicture"; 
            string[] strlist1 = Directory.GetFiles(path1);
            ArrayList list = new ArrayList();
            for (int i = 0; i < strlist1.Length; i++)
            {
                FileInfo f = new FileInfo(strlist1[i]);
                if (f.Extension == ".txt")
                {
                    list.Add(strlist1[i]);
                    string xx = "";
                    FileInfo fi = new FileInfo(strlist1[i]);

                    StreamReader sr = fi.OpenText();
                    xx = sr.ReadToEnd(); //文本内容
                    sr.Close();

                    tbInputData.Text = xx;
                    tbInputData_KeyDown(null,new KeyEventArgs(Keys.Enter));
                    if (SpRes=="OK")
                     File.Move(strlist1[i], MovePath + "\\"+fi.Name); //搬迁至文件夹
                    else
                        File.Move(strlist1[i], FailMovePath + "\\" + SpRes+"_"+fi.Name); //搬迁至文件夹
                }
            }


            // MessageBox.Show( list);//里面存的就是所有的有效路径
        }
        #region 勾子管理类 
      
        private KeyboardHookLib _keyboardHook = null;
     
        /// <summary>
        /// 是否已经按下了Keydown
        /// </summary>
        bool hasKeyDown = false;
        /// <summary>
        /// 客户端键盘捕捉事件.
        /// </summary>
        /// <param name="hookStruct">由Hook程序发送的按键信息</param>
        /// <param name="handle">是否拦截</param>
        private void KeyPress(KeyboardHookLib.HookStruct hookStruct, out bool handle)
        {
            handle = false; //预设不拦截任何键 



            if ((Keys)hookStruct.vkCode != Keys.None)
            {
                if ((Keys)hookStruct.vkCode != Keys.Back)
                {
                    if (hasKeyDown)
                    {
                        hasKeyDown = false;
                        return;
                    }

                    hasKeyDown = true;
                }
                switch ((Keys)hookStruct.vkCode)
                {
                    case Keys.Escape:
                        tbInputData.Text = "";
                        //panel1.Visible = false;
                        break;
                    case Keys.Back:
                       // if (PalConfig.Visible == false)
                        {
                           // textBox1.Select(textBox1.Text.Length, 0);
                            tbInputData.Select(tbInputData.Text.Length, 0);
                            tbInputData.Focus();
                        }
                        break;
                }

                if (hookStruct.vkCode == 13)
                {
                    // Console.WriteLine(DateTime.Now + "接收到的键盘值是：" + textBox1.Text);
                   // MessageBox.Show(DateTime.Now + "接收到的键盘值是：" + textBox1.Text);
                 

                    //label1.Text = textBox1.Text;
                  //  Pal .Visible = false;
                  
                    //textBox1.Text = "";
                    tbInputData_KeyDown(null,new KeyEventArgs(Keys.Enter));
                    tbInputData.Text = "";
                    return;
                }
                else
                {
                    if ((hookStruct.vkCode >= 65 && hookStruct.vkCode <= 90) || (hookStruct.vkCode >= 48 && hookStruct.vkCode <= 57) || (hookStruct.vkCode >= 96 && hookStruct.vkCode <= 105))
                    {
                        Keys key = (Keys)hookStruct.vkCode;
                       // panel1.Visible = true;

                        tbInputData.Text += key.ToString().ToString().Substring(key.ToString().Length - 1, 1);
                        tbInputData.Select(tbInputData.Text.Length, 0);

                    }
                }
            }
        }

        #endregion
       

        private void hook_Click(object sender, EventArgs e)
        {
            //安装勾子 
            _keyboardHook = new KeyboardHookLib();
            _keyboardHook.InstallHook(this.KeyPress);
            hookstatus.Text = "HOOK ON";
        }

        private void unhook_Click(object sender, EventArgs e)
        {
            //取消勾子 
            if (_keyboardHook != null)
            {
                _keyboardHook.UninstallHook();
                hookstatus.Text = "HOOK OFF";
            }
        }

        /// <summary>
        /// 传数据给串口
        /// </summary>
        /// <param name="ComMsg"></param>
        private void SendMsgToCom(string ComMsg)
        {
            if (PortFlag)
            {
                if (!string.IsNullOrEmpty(ComMsg))
                {
                    ComMsg = ComMsg + "\r\n";
                    byte[] buf = Encoding.ASCII.GetBytes(ComMsg.ToString());
                    sPort.Write(buf, 0, buf.Length);
                    //  textBox1.Text = "";
                    //label1.Text = "发送成功！";

                }
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tsmtopmosttrue_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void tsmtopmostfalse_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
        }

        private void tb_rs232_TextChanged(object sender, EventArgs e)
        {

        }

        private void SFIS_DCT_V2_FormClosing(object sender, FormClosingEventArgs e)
        {
       
        }

        private void cbwo_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void cbwo_KeyDown(object sender, KeyEventArgs e)
        {
            // if (!string.IsNullOrEmpty(cbwo.Text) && e.KeyCode == Keys.Enter)
            //{
            //    cbroute.Text = "";                
            //        GetStationList(cbwo.Text.Trim());
                
            //}
        }

        private void cbwo_MouseLeave(object sender, EventArgs e)
        {

        }

        private void PalConfig_Click(object sender, EventArgs e)
        {

        }
        public  string DataTableToDictionaryString(DataTable dt)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (DataRow dr in dt.Rows)
            {
                dic.Add(dr[0].ToString(), dr[2].ToString());
            }
            return GenericUtil.MapListConverter.DictionaryToJson(dic);
        }
       


    }
    public class ReleaseData
    {
        public static DataTable arrByteToDataTable(byte[] zipBuffer)
        {
            if (zipBuffer == null || zipBuffer.Length < 1)
                return null;
            byte[] buffer = UnZipClass.Decompress(zipBuffer);
            BinaryFormatter ser = new BinaryFormatter();
            DataSetSurrogate dss = ser.Deserialize(new MemoryStream(buffer)) as DataSetSurrogate;
            DataSet dataSet = dss.ConvertToDataSet();

            return dataSet.Tables[0];
        }

        public static DataSet arrByteToDataSet(byte[] zipBuffer)
        {
            if (zipBuffer == null || zipBuffer.Length < 1)
                return null;
            byte[] buffer = UnZipClass.Decompress(zipBuffer);
            BinaryFormatter ser = new BinaryFormatter();
            DataSetSurrogate dss = ser.Deserialize(new MemoryStream(buffer)) as DataSetSurrogate;
            DataSet dataSet = dss.ConvertToDataSet();

            return dataSet;
        }
       
    }

    public static class UnZipClass
    {
        public static byte[] Decompress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                Stream zipStream = null;
                zipStream = new GZipStream(ms, CompressionMode.Decompress);
                byte[] dc_data = null;
                dc_data = ExtractBytesFromStream(zipStream, data.Length);
                return dc_data;
            }
            catch
            {
                return null;
            }
        }
        public static byte[] ExtractBytesFromStream(Stream zipStream, int dataBlock)
        {
            byte[] data = null;
            int totalBytesRead = 0;
            try
            {
                while (true)
                {
                    Array.Resize(ref data, totalBytesRead + dataBlock + 1);
                    int bytesRead = zipStream.Read(data, totalBytesRead, dataBlock);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    totalBytesRead += bytesRead;
                }
                Array.Resize(ref data, totalBytesRead);
                return data;
            }
            catch
            {
                return null;
            }
        }

       
    }
}
