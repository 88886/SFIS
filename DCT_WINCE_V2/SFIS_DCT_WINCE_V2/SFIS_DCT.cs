using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Net;
using Microsoft.Win32;


namespace SFIS_DCT_WINCE_V2
{
    public partial class Frm_DCT : Form
    {
        public Frm_DCT()
        {
            InitializeComponent();
        }

        PublicStro.tPublicStoredproc Pubstr = new PublicStro.tPublicStoredproc();
        private void SFIS_DCT_V2_Load(object sender, EventArgs e)
        {
            ReadInI();
            label5.Visible = false;
            Thread initprj = new Thread(new ThreadStart(InitPrj));
            initprj.Start();
        }
        #region 获取初始值
        private void GetLineList()
        {
            DataTable dt = Pubstr.GetLineList().Tables[0];
            DataView dv = new DataView(dt);
            dv.Sort = dt.Columns[0].ToString();
            DataTable dt2 = dv.ToTable();
            foreach (DataRow dr in dt2.Rows)
            {
                this.cbline.Invoke(new EventHandler(delegate
                {
                    cbline.Items.Add(dr[0]);
                }));
            }
        }

        DataTable dtRoute = null;
        private void GetStationList(string woId)
        {
            cbroute.Items.Clear();
            dtRoute = Pubstr.GetAllStation(woId).Tables[0];
            if (dtRoute.Rows.Count != 0)
            {
                DataView dv = new DataView(dtRoute);
                dv.Sort = dtRoute.Columns[0].ToString();
                DataTable dt2 = dv.ToTable();
                foreach (DataRow dr in dt2.Rows)
                {
                    cbroute.Items.Add(dr[1]);

                }
            }
        }
        private void GetWoList()
        {
            string[] ls = Pubstr.GetWoList();
            for (int i = 0; i < ls.Length; i++)
            {
                this.cbwo.Invoke(new EventHandler(delegate
                {
                    cbwo.Items.Add(ls[i]);
                }));
            }
        }
        private void GetProcedures()
        {
            DataTable dt = Pubstr.GetListStationType().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.cbprocedures.Invoke(new EventHandler(delegate
                {
                    cbprocedures.Items.Add(dt.Rows[i][0].ToString() + " " + dt.Rows[i][1].ToString());
                }));
            }

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
                DataTable dt = getNewTable(dtRoute, string.Format("craftname='{0}'", cbroute.Text.Trim()));
                lbstationid.Text = dt.Rows[0][0].ToString();
                IniRouteId = dt.Rows[0][0].ToString();
            }
        }


        private void cbwo_TextChanged(object sender, EventArgs e)
        {
            cbroute.Text = "";
            GetStationList(cbwo.Text.Trim());
        }
        #endregion

        /// <summary>
        /// 存储获取需要执行的全部存储过程
        /// </summary>
        DataTable dtSp = null;
        /// <summary>
        /// 存储获取系统全部需要的参数
        /// </summary>
        DataTable SysAllData = null;
        /// <summary>
        /// 存储过程需要的参数
        /// </summary>
        DataTable SPvalues = null;

        string SpRes = "";
        string Msg = "";

        private void ShowMessage(string Msg1, string Msg2, string Msg3)
        {
            tbMessage.Items.Clear();
            tbMessage.Items.Add(Msg1);
            tbMessage.Items.Add(Msg2);
            tbMessage.Items.Add(Msg3);
        }
        private void Butok_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(cbline.Text))
            {
                ShowMessage("线别未选择", "请选择线别", "");
                return;
            }
            if (string.IsNullOrEmpty(cbwo.Text))
            {
                ShowMessage("工单未选择", "请选择工单", "");
                return;
            }
            if (string.IsNullOrEmpty(cbprocedures.Text))
            {
                ShowMessage("行为模式未选择", "请选择行为模式", "");
                return;
            }
            if (string.IsNullOrEmpty(cbroute.Text))
            {
                ShowMessage("当前途程未选择", "请选择途程", "");
                return;
            }

            IniLine = cbline.Text;
            IniRoute = cbroute.Text;
            IniSp = cbprocedures.Text;
            IniWo = cbwo.Text;

            this.label5.Text = "【途程: " + IniRoute + "--线别: " + IniLine + "】";
            this.label5.Visible = true;
            PalConfig.Visible = false;
            tbMessage.Size = new Size(260, this.Size.Height - 100);
            dtSp = Pubstr.GetSystemStoredproc(Convert.ToInt32(ExcuteSp)).Tables[0];//.GetStoredproc(ExcuteSp).Tables[0]; //找出需要执行那些存储过程
            GetSystemInputAllData();// 获取系统全部需要的参数
            tbInputData.Text = "";
            tbInputData.Focus();
            ShowMessage("资料选择完成", getNewTable(dtSp, string.Format("StoredprocIdx='{0}'", ExeSp)).Rows[0][11].ToString() + "?", "");
            BeepSendOK();

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
                IniSpId = ExcuteSp;
                tbMessage.Text = "行为模式选择正确";
            }
            catch (Exception)
            {
                tbMessage.Text = "行为模式选择错误";
                cbprocedures.Focus();

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
                    SysAllData.Rows[i]["data"] = IniRouteId; //lbstationid.Text;
                    SysAllData.Rows[i]["InRam"] = "1";
                }
                if (SysAllData.Rows[i]["param"].ToString().ToUpper() == "WO")
                {
                    SysAllData.Rows[i]["data"] = IniWo; //cbwo.Text.Trim();
                    SysAllData.Rows[i]["InRam"] = "1";
                }
                if (SysAllData.Rows[i]["param"].ToString().ToUpper() == "LINE")
                {
                    SysAllData.Rows[i]["data"] = IniLine; //cbline.Text.Trim();
                    SysAllData.Rows[i]["InRam"] = "1";
                }
                if (SysAllData.Rows[i]["param"].ToString().ToUpper() == "MACADDRESS")
                {
                    SysAllData.Rows[i]["data"] = GetMacAddr();
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
        /// 获取存储过程需要的参数,并增加变量值
        /// </summary>
        /// <param name="Stro"></param>
        private void GetStoredprocValues(string Stro)
        {
            SPvalues = Pubstr.GetStoredprocValues(Stro).Tables[0];
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
        /// 执行到第几步存储过程
        /// </summary>
        int ExeSp = 1;
        /// <summary>
        /// 输入参数
        /// </summary>
        string InputData = string.Empty;
        private void tbInputData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(tbInputData.Text))
                {
                    InputData = tbInputData.Text.Trim().ToUpper();
                }
                tbInputData.Text = "";
                tbInputData.Focus();
                if (InputData == "UNDO")
                {
                    cbline.Text = "";
                    cbroute.Text = "";
                    cbwo.Text = "";
                    cbprocedures.Text = "";
                    PalConfig.Visible = true;
                    this.label5.Visible = false;
                    tbMessage.Size = new Size(260, this.PalData.Size.Height - 50);
                    ExeSp = 1;
                    ShowMessage("UNDO OK", getNewTable(dtSp, string.Format("StoredprocIdx='{0}'", ExeSp)).Rows[0][11].ToString() + "?", "");
                    BeepSendOK();
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
                        ExeSp = getNewTable(dtSp, "InRam='1'").Rows.Count;
                        ShowMessage("Clear OK", getNewTable(dtSp, string.Format("StoredprocIdx='{0}'", ExeSp)).Rows[0][11].ToString() + "?", "");
                        BeepSendOK();
                    }
                    else
                    {
                        if (PalConfig.Visible)
                        {
                            tbMessage.Text = "请先设置选项";
                            return;
                        }

                        InputDataExecuteSp(InputData);
                        ExeSp++;
                        ShowMessage(SpRes, getNewTable(dtSp, string.Format("StoredprocIdx='{0}'", ExeSp)).Rows[0][11].ToString() + "?", InputData + "  " + SpRes);//PARAM
                        if (SpRes == "OK")
                            BeepSendOK();
                        else
                            BeepSendFail();
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
                SpRes = Pubstr.SP_PublicStoredproc(dr[1].ToString(), SPvalues);
                Msg = dr[11].ToString() + "   " + SpRes;

                if (SpRes == "OK")
                {
                    if (dr[8].ToString() == "1")  //最后一枪清除资料
                    {
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
                    ExeSp = Convert.ToInt32(dr[4].ToString()) - 1; //SECOND 报错时回到第几枪                   
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region 控制蜂鸣器
        /// <summary>
        /// 错误提示音
        /// </summary>
        private void BeepSendFail()
        {
            sx = 2;
            TurnOff(null);
            BeepTime.Enabled = true;
            Beep(1, 100);
        }
        /// <summary>
        /// 正确提示音
        /// </summary>
        private void BeepSendOK()
        {
            sx = 1;
            TurnOff(null);
            BeepTime.Enabled = true;
            Beep(1, 100);
        }

        System.Windows.Forms.Timer BeepTime = new System.Windows.Forms.Timer();




        [DllImport("coredll.dll", EntryPoint = "DeviceIoControl", SetLastError = true)]
        internal static extern int DeviceIoControlCE(int hDevice, int dwIoControlCode, byte[] lpInBuffer, int nInBufferSize, byte[] lpOutBuffer, int nOutBufferSize, ref int lpBytesReturned, IntPtr lpOverlapped);

        [DllImport("coredll", SetLastError = true)]
        private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        private static IntPtr _pwmFile = CreateFile("PWM1:", 0x40000000, 0, IntPtr.Zero, 3, 0, IntPtr.Zero);

        public static void Beep(uint Frequency, int DurationMS)
        {

            uint frequency = Frequency;
            byte[] buffer = new byte[4];
            int accessType = 2;
            buffer[0] = (byte)(frequency & 0xff);
            frequency = frequency >> 8;
            buffer[1] = (byte)(frequency & 0xff);
            frequency = frequency >> 8;
            buffer[2] = (byte)(frequency & 0xff);
            frequency = frequency >> 8;
            buffer[3] = (byte)(frequency & 0xff);
            DeviceIoControlCE((int)_pwmFile, accessType, buffer, 4, buffer, 0, ref accessType, IntPtr.Zero);
            // var t = new System.Threading.Timer(TurnOff, null, DurationMS, System.Threading.Timeout.Infinite);
        }

        private static void TurnOff(Object obj)
        {
            byte[] buffer = new byte[4];
            int accessType = 1;
            DeviceIoControlCE((int)_pwmFile, accessType, buffer, 4, buffer, 0, ref accessType, IntPtr.Zero);
        }
        int sx = 5;
        private void BeepTime_Tick(object sender, EventArgs e)
        {
            sx = sx - 1;
            if (sx == 0)
            {
                TurnOff(null);
                BeepTime.Enabled = false;
            }

        }
        #endregion
        private void InitPrj()
        {
            try
            {
                this.tbMessage.Invoke(new EventHandler(delegate
                {
                    this.tbMessage.Items.Clear();
                    this.tbMessage.Items.Add("程序正在初始化...");
                }));
                this.mflag = true;
                GetLineList();
                //  GetStationList();
                GetWoList();
                GetProcedures();


                this.BeepTime.Interval = 1020;
                this.BeepTime.Enabled = false;
                this.BeepTime.Tick += new EventHandler(BeepTime_Tick);

                this.tbMessage.Invoke(new EventHandler(delegate
                {
                    this.tbMessage.Items.Clear();
                    this.tbMessage.Items.Add("程序初始化完成.");
                }));
                this.mflag = false;

                #region 获取默认上一次线体
                this.cbroute.Invoke(new EventHandler(delegate
                    {
                        for (int i = 0; i < cbroute.Items.Count - 1; i++)
                        {
                            if (IniRoute == cbroute.Items[i].ToString())
                            {
                                cbroute.SelectedIndex = i;
                            }
                        }
                    }));
                this.cbprocedures.Invoke(new EventHandler(delegate
                {
                    for (int i = 0; i < cbprocedures.Items.Count - 1; i++)
                    {
                        if (IniSp == cbprocedures.Items[i].ToString())
                        {
                            cbprocedures.SelectedIndex = i;
                        }
                    }
                }));
                this.cbline.Invoke(new EventHandler(delegate
                {
                    for (int i = 0; i < cbline.Items.Count - 1; i++)
                    {
                        if (IniLine == cbline.Items[i].ToString())
                        {
                            cbline.SelectedIndex = i;
                        }
                    }
                }));
                #endregion

            }
            catch (Exception ex)
            {
                this.tbMessage.Invoke(new EventHandler(delegate
                {
                    this.tbMessage.Items.Clear();
                    this.tbMessage.Items.Add("程序初始化失败!!!");
                    this.tbMessage.Items.Add(ex.Message);// ("服务器连接失败,请重新运行程序");
                }));
            }
        }

        private bool mflag = false;
        private void cbwo_Validating(object sender, CancelEventArgs e)
        {
            if (mflag)
            {
                MessageBox.Show("程序正在初始化中,请稍候..", "提示");
                this.cbwo.Text = "";
                return;
            }
            cbroute.Text = "";
            GetStationList(cbwo.Text.Trim());
        }

        private void tbMessage_GotFocus(object sender, EventArgs e)
        {
            tbInputData.Focus();
        }

        string IniWo = "";
        string IniRoute = "";
        string IniRouteId = "";
        string IniLine = "";
        string IniSp = "";
        string IniSpId = "";
        #region   读写INI文件

        public static string GetPath()
        {
            string fullAppName = Assembly.GetExecutingAssembly().GetName().CodeBase;
            string fullAppPath = Path.GetDirectoryName(fullAppName);
            if (fullAppPath.StartsWith("file:"))
            {
                fullAppPath = fullAppPath.Substring(6);
            }
            if (fullAppPath.Substring(fullAppPath.Length - 1, 1) != "//")
            {
                fullAppPath += "//";
            }
            return (fullAppPath);
        }

        string filePath = GetPath() + "\\SFIS.ini";// @"\ResidentFlash\SocketConfig\Socket.ini";   
        private void ReadInI()
        {
            IniWo = ZT_INI.GetINI("DCT", "WO", "", filePath);
            IniRoute = ZT_INI.GetINI("DCT", "Route", "", filePath);
            IniRouteId = ZT_INI.GetINI("DCT", "RouteID", "", filePath);
            IniLine = ZT_INI.GetINI("DCT", "Line", "", filePath);
            IniSp = ZT_INI.GetINI("DCT", "SP", "", filePath);
            IniSpId = ZT_INI.GetINI("DCT", "SPID", "", filePath);

            cbline.Text = IniLine;
            cbroute.Text = IniRoute;
            lbstationid.Text = IniRouteId;
            cbwo.Text = IniWo;
            cbprocedures.Text = IniSp;
            ExcuteSp = IniSpId;
        }
        private void WriteInI()
        {
            ZT_INI.PutINI("DCT", "WO", IniWo, filePath);
            ZT_INI.PutINI("DCT", "Route", IniRoute, filePath);
            ZT_INI.PutINI("DCT", "RouteID", IniRouteId, filePath);
            ZT_INI.PutINI("DCT", "Line", IniLine, filePath);
            ZT_INI.PutINI("DCT", "SP", IniSp, filePath);
            ZT_INI.PutINI("DCT", "SPID", IniSpId, filePath);

        }
        class ZT_INI
        {



            public static void PutINI(string strSection, string strKey, string strValue, string strFilePath)
            {
                INICommon(false, strSection, strKey, strValue, strFilePath);
            }


            public static string GetINI(string strSection, string strKey, string strDefault, string strFilePath)
            {
                return INICommon(true, strSection, strKey, strDefault, strFilePath);
            }
            private static string[] Split(string input, string pattern)
            {
                string[] arr = System.Text.RegularExpressions.Regex.Split(input, pattern);
                return arr;
            }
            private static void AppendToFile(string strPath, string strContent)
            {
                FileStream fs = new FileStream(strPath, FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(fs, System.Text.Encoding.Default);
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                streamWriter.WriteLine(strContent);
                streamWriter.Flush();
                streamWriter.Close();
                fs.Close();
            }
            private static void WriteArray(string strPath, string[] strContent)
            {
                FileStream fs = new FileStream(strPath, FileMode.Truncate);
                StreamWriter streamWriter = new StreamWriter(fs, System.Text.Encoding.Default);
                streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                for (int i = 0; i < strContent.Length; i++)
                {
                    if (strContent[i].Trim() == "\r\n")
                        continue;
                    streamWriter.WriteLine(strContent[i].Trim());
                }
                streamWriter.Flush();
                streamWriter.Close();
                fs.Close();
            }
            //INI解析
            private static string INICommon(bool isRead, string ApplicationName, string KeyName, string Default, string FileName)
            {
                string strSection = "[" + ApplicationName + "]";
                string strBuf;
                try
                {
                    //a.文件不存在则创建
                    if (!File.Exists(FileName))
                    {
                        FileStream sr = File.Create(FileName);
                        sr.Close();
                    }
                    //读取INI文件
                    System.IO.StreamReader stream = new System.IO.StreamReader(FileName, System.Text.Encoding.Default);
                    strBuf = stream.ReadToEnd();//读取ini文件
                    stream.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "INI文件读取异常");
                    return Default;
                }
                string[] rows = Split(strBuf, "\r\n");
                string oneRow;
                int i = 0;
                for (; i < rows.Length; i++)
                {
                    oneRow = rows[i].Trim();
                    //空行
                    if (0 == oneRow.Length)
                        continue;
                    //此行为注释
                    if (';' == oneRow[0])
                        continue;
                    //没找到
                    if (strSection != oneRow)
                        continue;
                    //找到了
                    break;
                }
                //b.没找到对应的section，创建一节并创建属性
                if (i >= rows.Length)
                {
                    AppendToFile(FileName, "\r\n" + strSection + "\r\n" + KeyName + "=" + Default);
                    return Default;
                }
                //找到section
                i += 1; //跳过section 
                int bakIdxSection = i;//备份section的下一行
                string[] strLeft;
                //查找属性
                for (; i < rows.Length; i++)
                {
                    oneRow = rows[i].Trim();
                    //空行
                    if (0 == oneRow.Length)
                        continue;
                    //此行为注释
                    if (';' == oneRow[0])
                        continue;
                    //越界
                    if ('[' == oneRow[0])
                        break;
                    strLeft = Split(oneRow, "=");
                    if (strLeft == null || strLeft.Length != 2)
                        continue;
                    //找到属性
                    if (strLeft[0].Trim() == KeyName)
                    {
                        //读
                        if (isRead)
                        {
                            //c.找到属性但没有值
                            if (0 == strLeft[1].Trim().Length)
                            {
                                rows[i] = strLeft[0].Trim() + "=" + Default;
                                WriteArray(FileName, rows);
                                return Default;
                            }
                            else
                            {
                                //找到了                       
                                return strLeft[1].Trim();
                            }
                        }
                        //写
                        else
                        {
                            rows[i] = strLeft[0].Trim() + "=" + Default;
                            WriteArray(FileName, rows);
                            return Default;
                        }
                    }
                }
                //d.没找到对应的属性,创建之并赋为默认
                rows[bakIdxSection] = rows[bakIdxSection] + "\r\n" + KeyName + "=" + Default;
                WriteArray(FileName, rows);
                return Default;
            }
        }

        #endregion

        private void Frm_DCT_Closing(object sender, CancelEventArgs e)
        {
            WriteInI();
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

        //获取mac地址
        private static string GetMacAddr()
        {
            Reg reg = new Reg();
            string mac = reg.ReadValue(Reg.HKEY.HKEY_LOCAL_MACHINE, @"Comm\DM9CE1\Parms", "NetWorkAddress").ToUpper();
            if (mac.Length == 12)
            {
                mac = mac.Substring(0, 2) + "-" + mac.Substring(2, 2) + "-" + mac.Substring(4, 2) + "-" +
                      mac.Substring(6, 2) + "-" + mac.Substring(8, 2) + "-" + mac.Substring(10, 2);
            }
            return mac;
        }


        public class Reg
        {
            public enum HKEY { HKEY_LOCAL_MACHINE = 0, HKEY_CLASSES_ROOT = 1, HKEY_CURRENT_USER = 2, HKEY_USERS = 3 };
            private RegistryKey[] reg = new RegistryKey[4];

            public Reg()
            {
                reg[(int)HKEY.HKEY_LOCAL_MACHINE] = Registry.LocalMachine;
                reg[(int)HKEY.HKEY_CLASSES_ROOT] = Registry.ClassesRoot;
                reg[(int)HKEY.HKEY_CURRENT_USER] = Registry.CurrentUser;
                reg[(int)HKEY.HKEY_USERS] = Registry.Users;
            }

            //读指定变量值
            public string ReadValue(HKEY Root, string SubKey, string ValueName)
            {
                RegistryKey subKey = reg[(int)Root];
                if (ValueName.Length == 0) return "NA";
                try
                {
                    if (SubKey.Length > 0)
                    {
                        string[] strSubKey = SubKey.Split('\\');
                        foreach (string strKeyName in strSubKey)
                        {
                            subKey = subKey.OpenSubKey(strKeyName);
                        }
                    }
                    string strKey = subKey.GetValue(ValueName).ToString();
                    subKey.Close();
                    return strKey;
                }
                catch
                {
                    return "NA";
                }
            }
        }

       


    }
}
