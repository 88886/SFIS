using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using Notice;
using System.Reflection;
using RefWebService_BLL;
//using SFIS_V2.Properties;

namespace SFIS_V2
{
    public partial class MainParent : Office2007Form// Form
    {
        public MainParent()
        {
            InitializeComponent();
            FrmText = this.Text;
           // this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            //notifyIcon1.Icon = Resources.silver;
            //this.ShowInTaskbar = false;
            //this.blinkTiemr.Enabled = true;
        }

        //private Icon blank = Resources.blank;
        //private Icon striped = Resources.striped;
        //private bool blink = false;

        private string mIpAddress = string.Empty;
        private string appfilename = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
        // System.IO.Ports.SerialPort MyCom;
        private string FrmText;
        const int CLOSE_SIZE = 15;
        /// <summary>
        /// 保存登录的用户信息
        /// </summary>
        private tUserInfo  mUserInfo;
      
        /// <summary>
        /// 保存登录的用户信息
        /// </summary>
        public tUserInfo gUserInfo
        {
            get { return mUserInfo; }
            set { mUserInfo = value; }
        }
        public enum MsgType
        {
            /// <summary>
            /// 绿色
            /// </summary>
            Incoming,
            /// <summary>
            /// 蓝色
            /// </summary>
            Outgoing,
            /// <summary>
            /// 黑色
            /// </summary>
            Normal,
            /// <summary>
            /// 橙色
            /// </summary>
            Warning,
            /// <summary>
            /// 红色
            /// </summary>
            Error
        };
        System.Windows.Forms.Timer MsgTimerPrg = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer MsgTimerSys = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer ChkPrjVersionTimer = new System.Windows.Forms.Timer();
        MsgNotice.MsgNotice MsgNot = new MsgNotice.MsgNotice();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr GetFocus();
        [DllImport("user32.dll")]
        public static extern int PostMessage(int hwnd, int wMsg, int wParam, int lParam);

        private Color[] MsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };
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

        #region 控件事件
        private string AssemblyVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }

        }
        public void imbt_exit_Click(object sender, System.EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
        private void MainParent_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            try
            {
                if (this.tabControl1.Tabs.Count > 1)
                {
                   
                    if (MessageBoxEx.Show("存在打开的功能!!\n是否确定退出?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        this.LogOut();
                        System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("lppa");
                        foreach (System.Diagnostics.Process p in process)
                        {
                            p.Kill();
                        }
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    this.LogOut();
                }
                RefWebService_BLL.refwebtEditing.Instance.DeletetEditingByUserId(this.gUserInfo.userId);
                closeproc("AutoUpdate.exe");
                closeproc(System.IO.Path.GetFileName(Application.ExecutablePath));             
            }
            catch
            {
            }
        }
        private void closeproc(string prcname)
        {
            Process[] prc = Process.GetProcessesByName(prcname.Substring(0, prcname.LastIndexOf('.')));
            if (prc.Length > 0)
                foreach (Process pc in prc)
                {
                    pc.Kill();
                }
        }
        private void InitComPort()
        {
            //#region COM
            //MyCom = new System.IO.Ports.SerialPort(); //= FrmBLL.ReadComm.Instance.GetCom;
            //MyCom.PortName = "COM1";
            //MyCom.BaudRate = 9600;
            //MyCom.DataBits = 8;
            //try
            //{
            //    MyCom.Open();
            //    MyCom.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(MyCom_DataReceived);
            //}
            //catch
            //{
            //    MyCom.Dispose();
            //}
            //#endregion
        }
        public bool loginOk { get; set; }
        //1.声明自适应类实例

        private void MainParent_Load(object sender, System.EventArgs e)
        {

            ShowApplicationMsg();
            this.MsgPanel.Visible = false;
            this.ExpUserPanel.Expanded = false;
            this.ExpMsgPanel.Expanded = false;

            this.ExpUserPanel.Enabled = false;
            this.ExpMsgPanel.Enabled = false;

            //string _title = string.Format("{0}({1})", this.FrmText, this.AssemblyVersion);
           // this.Text = _title;

            this.Text = string.Format("{0} Version: {1} (Build Date:{2})", this.FrmText, this.AssemblyVersion, System.IO.File.GetLastWriteTime(System.Windows.Forms.Application.ExecutablePath).ToShortDateString());


            //System.DateTime d1 = System.DateTime.Parse("2016/04/05");
            //System.DateTime d2 = System.DateTime.Now;
            //System.TimeSpan ts = d1 - d2;
            //if (ts.Days <= 0)
            //{
            //    MessageBox.Show("试用时间超出,请联系系统开发人员");
            //    this.Close();
            //    return;
            //}

            string Msg;
            if (!RefWebService_BLL.refWebCheck_Version.Instance.CheckPrgVsersion("SFIS_V4", Application.ProductVersion, null, null, null,out Msg))
            {
                RunFile(System.Windows.Forms.Application.StartupPath + @"\", "AutoUpdate.exe", appfilename);
                MessageBox.Show(Msg=="OK"?"该程序为版本不是最新版\r\n请更新后运行":Msg);           
                string FileName = System.IO.Path.GetFileName(Application.ExecutablePath);
                Process[] prc = Process.GetProcessesByName(FileName.Substring(0, FileName.LastIndexOf('.')));
                if (prc.Length > 0)
                    foreach (Process pc in prc)
                    {
                        pc.Kill();
                    }
                return;
            }


            #region 时间控制器
            this.MsgTimerPrg.Interval = 10000;
            this.MsgTimerPrg.Enabled = false;
            this.MsgTimerPrg.Tick += new EventHandler(MsgTimerPrg_Tick);

            this.MsgTimerSys.Interval = 10000;
            this.MsgTimerSys.Enabled = false;
            this.MsgTimerSys.Tick += new EventHandler(MsgTimerSys_Tick);

            this.ChkPrjVersionTimer.Interval = 1000*60*60*2; //每两个小时检查一次是否是最新程序
            this.ChkPrjVersionTimer.Enabled = true;
            this.ChkPrjVersionTimer.Tick += new EventHandler(ChkPrjVersionTimer_Tick);
            

            #endregion
            #region PING服务器
            XmlDocument doc = new XmlDocument();
            string XmlName = "DllConfig.xml";
            doc.Load(XmlName);
            this.mIpAddress = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ServerIP")).GetAttribute("IP").ToString();
            CheckNet = new CHKNET(PingTest);
            CheckNet.BeginInvoke(null, null);
            #endregion
            Login lgi = new Login(this);
            while (lgi.ShowDialog() != DialogResult.OK) ;
            if (loginOk)
            {
                GetAllFunctionButtom();
                this.lb_username.Text = string.Format("{0}({1})", this.mUserInfo.username, this.mUserInfo.userId);
                FrmBLL.InstallFont instfont = new FrmBLL.InstallFont();

                this.InitComPort();

                FrmBLL.ModifyLocalTime.ModifyTime();

                #region 清空当前用户以前锁住的所有项目
                string err = string.Empty;
                if ((err=RefWebService_BLL.refwebtEditing.Instance.DeletetEditingByUserId(this.gUserInfo.userId))!="OK")
                {
                    this.ShowPrgMsg("被锁资源清除失败:\n" + err + "\n建议重新启动程序..", MsgType.Warning);
                }
                #endregion
            }
            if (this.mUserInfo.rolecaption == "系统开发员")
            {
                //this.MsgPanel.Visible = true;
                //CheckNet = new CHKNET(ConnectMsgSer);
                //CheckNet.BeginInvoke(null, null);
            }

            lgi.Dispose();

            sideBarPanelItem1.Visible = false;
        }

        private void ShowApplicationMsg()
        {
            try
            {
                webSysMsg.Visible = false;
                string SysDBMsg = RefWebService_BLL.refWebCheck_Version.Instance.SystemMsg();
                webSysMsg.Visible = !string.IsNullOrEmpty(SysDBMsg);           
                webSysMsg.DocumentText = RefWebService_BLL.refWebCheck_Version.Instance.SystemMsg();
            }
            catch 
            {
                
            }
        }
        private void ConnectMsgSer()
        {
            this.MsgPanel.Invoke(new EventHandler(delegate
            {
                if (ConnectMsgServer())
                {
                    this.ExpUserPanel.Expanded = false;
                    this.ExpMsgPanel.Expanded = false;

                    this.ExpUserPanel.Enabled = true;
                    this.ExpMsgPanel.Enabled = true;

                }
                else
                {
                    this.ExpUserPanel.Expanded = false;
                    this.ExpMsgPanel.Expanded = false;

                    this.ExpUserPanel.Enabled = false;
                    this.ExpMsgPanel.Enabled = false;
                }
            }));
        }
        private void MyCom_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //try
            //{
            //    string str;
            //start:
            //    int bytesCount1 = this.MyCom.BytesToRead;
            //    Thread.Sleep(100);
            //    int bytesCount = this.MyCom.BytesToRead;
            //    if (bytesCount == bytesCount1)
            //    {
            //        byte[] buffer = new byte[bytesCount];
            //        this.MyCom.Read(buffer, 0, bytesCount);
            //        if (Mycontrol.GetType().Name.IndexOf("TextBox") != -1 || Mycontrol.GetType().Name.IndexOf("Comb") != -1)
            //        {
            //            str = ByteToString(buffer);
            //            if (!string.IsNullOrEmpty(str))
            //                FillTextBox(Mycontrol, str);
            //        }
            //    }
            //    else
            //    {
            //        goto start;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ShowPrgMsg(ex.Message, MsgType.Error);
            //}
        }
        private void FillTextBox(object obj, string str)
        {
            if (obj is DevComponents.DotNetBar.Controls.TextBoxX)
            {
                (obj as DevComponents.DotNetBar.Controls.TextBoxX).Invoke(new EventHandler(delegate
                {

                    (obj as DevComponents.DotNetBar.Controls.TextBoxX).Text = str;
                    PostMessage(hwnd, 0x0100, 0x0D, 0);
                }));
                return;
            }

            if (obj is TextBox)
            {
                (obj as TextBox).Invoke(new EventHandler(delegate
                {

                    (obj as TextBox).Text = str;
                    PostMessage(hwnd, 0x0100, 0x0D, 0);
                }));
                return;
            }
            if (obj is DevComponents.DotNetBar.Controls.ComboBoxEx)
            {
                (obj as DevComponents.DotNetBar.Controls.ComboBoxEx).Invoke(new EventHandler(delegate
                {

                    (obj as DevComponents.DotNetBar.Controls.ComboBoxEx).Text = str;
                    PostMessage(hwnd, 0x0100, 0x0D, 0);
                }));
                return;
            }


            if (obj is ComboBox)
            {
                (obj as ComboBox).Invoke(new EventHandler(delegate
                {
                    (obj as ComboBox).Text = str;
                    PostMessage(hwnd, 0x0100, 0x0D, 0);
                }));
                return;
            }
        }
        private string ByteToString(byte[] arrbyte)
        {
            return Encoding.Default.GetString(arrbyte);
        }
        private void MsgTimerSys_Tick(object sender, EventArgs e)
        {

        }
        int PrjClose = 0;
        private void ChkPrjVersionTimer_Tick(object sender, EventArgs e)
        {
            string _StrErr = refWebCheck_Version.Instance.CheckPrgVsersion("SFIS_V4", Application.ProductVersion);
            if (_StrErr != "OK")
            {
                PrjClose++;
                if (PrjClose >= 4)
                {
                    string FileName = System.IO.Path.GetFileName(Application.ExecutablePath);
                    Process[] prc = Process.GetProcessesByName(FileName.Substring(0, FileName.LastIndexOf('.')));
                    if (prc.Length > 0)
                        foreach (Process pc in prc)
                        {
                            pc.Kill();
                        }
                }
                MessageBox.Show(string.Format("{0}\r\n 请关闭程序并升级版本", _StrErr));
            }
        }
        

        private void MsgTimerPrg_Tick(object sender, EventArgs e)
        {
            this.ShowPrgMsg("", MsgType.Normal);
            this.lab_showgloabmsg.BackColor = Color.Transparent;
            this.MsgTimerPrg.Enabled = false;
        }
        private bool FindFrm(DevComponents.DotNetBar.TabControl TCtl, string name, string text, out TabControlPanel tcp)
        {
            TabItem ti = new TabItem();
            tcp = new TabControlPanel();
            for (int i = 0; i < TCtl.Tabs.Count; i++)
            {
                if (TCtl.Tabs[i].Name == name)
                {
                    TCtl.SelectedTab = TCtl.Tabs[i];
                    return true;
                }
            }
            ti.AttachedControl = tcp;
            ti.Name = name;
            tcp.Name = name;
            tcp.Text = text;
            ti.Text = string.Format(" {0} ", text);
            ti.MouseDown += new MouseEventHandler(tabItem_MouseDown);
            tcp.Dock = DockStyle.Fill;
            TCtl.Tabs.Add(ti);
            TCtl.Controls.Add(tcp);
            TCtl.SelectedTab = TCtl.Tabs[ti.Name];
            return false;
        }
        DataTable dtt = new DataTable("a01");
        private void imbt_excelToDb_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='ExcelToDb' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                ExcelToDb ed = new ExcelToDb(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                {
                    ed.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        public void imbt_printMpTable_Click(object sender, EventArgs e)
        {

            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='WoBomInfo' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                WoBomInfo ed = new WoBomInfo(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                {
                    ed.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }

        private void imbt_wiptracking_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmWipTracking' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                FrmWipTracking ed = new FrmWipTracking(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                {
                    ed.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void imbt_querysix_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmQuerySix' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                FrmQuerySix ed = new FrmQuerySix(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                {
                    ed.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }

        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='line_set' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion

                TabControlPanel tcp;
                line_set ed = new line_set(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                {
                    ed.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void imbt_matclearstor_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Material_obsolete' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion

                TabControlPanel tcp;
                Material_obsolete ed = new Material_obsolete(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void imbt_bkmat_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Material_Monitor' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                if (!ChkSoftInstall("CODESOFT 7"))
                {
                    MessageBoxEx.Show("本机没有安装Codesoft 7 不能使用该模块");
                    return;
                }
                TabControlPanel tcp;
                Material_Monitor ed = new Material_Monitor(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }

        private void imbt_editkpmaster_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='EditKpMarster' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                EditKpMarster ed = new EditKpMarster(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void imbt_lableprint_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    #region 权限判定
            //    //先判定是否有使用该程序的权限
            //    DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Print5in1label' and funId='SHOWFROM'");
            //    if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //    {
            //        throw new Exception("您没有权限使用该功能!!");
            //    }
            //    #endregion
            //    TabControlPanel tcp;
            //    Print5in1label ed = new Print5in1label(this);
            //    ed.WindowState = FormWindowState.Maximized;
            //    ed.FormBorderStyle = FormBorderStyle.None;
            //    ed.TopLevel = false;

            //    if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //    {
            //        tcp.Controls.Add(ed);
            //        ed.Show();
            //        this.tabControl1.Refresh();
            //    }
            //    else
            //        ed.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    this.ShowPrgMsg(ex.Message, MsgType.Error);
            //}
        }
        private void imbt_createworkorder_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    #region 权限判定
            //    //先判定是否有使用该程序的权限
            //    DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='WorkOrderCreate' and funId='SHOWFROM'");
            //    if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //    {
            //        throw new Exception("您没有权限使用该功能!!");
            //    }
            //    #endregion

            //    TabControlPanel tcp;
            //    WorkOrderCreate ed = new WorkOrderCreate(this);
            //    ed.WindowState = FormWindowState.Maximized;
            //    ed.FormBorderStyle = FormBorderStyle.None;
            //    ed.TopLevel = false;

            //    if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //    {
            //        tcp.Controls.Add(ed);
            //        ed.Show();
            //        this.tabControl1.Refresh();
            //    }
            //    else
            //        ed.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    this.ShowPrgMsg(ex.Message, MsgType.Error);
            //}
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_MO_Manage' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }

                #endregion
                TabControlPanel tcp;
                Frm_MO_Manage ed = new Frm_MO_Manage(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void imbt_createproduct_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='createproduct' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                createproduct ed = new createproduct(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        public void imbt_CreateRoute_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_Create_Route' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                //CreateRoute ed = new CreateRoute(this);
                Frm_Create_Route ed = new Frm_Create_Route(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        public void imbt_createCraft_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='CreateCraft' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                CreateCraft ed = new CreateCraft(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void butworkshop_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='formworkshop' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                formworkshop ed = new formworkshop(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void butworkfunctioninfo_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    #region 权限判定
            //    //先判定是否有使用该程序的权限
            //    DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='fworkfunctioninfo' and funId='SHOWFROM'");
            //    if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //    {
            //        throw new Exception("您没有权限使用该功能!!");
            //    }
            //    #endregion
            //    TabControlPanel tcp;
            //    fworkfunctioninfo ed = new fworkfunctioninfo(this);
            //    ed.WindowState = FormWindowState.Maximized;
            //    ed.FormBorderStyle = FormBorderStyle.None;
            //    ed.TopLevel = false;

            //    if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //    {
            //        tcp.Controls.Add(ed);
            //        ed.Show();
            //        this.tabControl1.Refresh();
            //    }
            //    else
            //        ed.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    this.ShowPrgMsg(ex.Message, MsgType.Error);
            //}
        }
        private void butuserinfo_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='fuserinfo' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                fuserinfo ed = new fuserinfo(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void butFacDept_Click(object sender, EventArgs e)
        {

            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='fFacDeptInfo' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                fFacDeptInfo ed = new fFacDeptInfo(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void imbt_smtkpquery_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='fmMaterialQuery' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                fmMaterialQuery ed = new fmMaterialQuery(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void imbt_editwokp_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    #region 权限判定
            //    //先判定是否有使用该程序的权限
            //    DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='fmMaterialManagement' and funId='SHOWFROM'");
            //    if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //    {
            //        throw new Exception("您没有权限使用该功能!!");
            //    }
            //    #endregion
            //    TabControlPanel tcp;
            //    fmMaterialManagement ed = new fmMaterialManagement(this);
            //    ed.WindowState = FormWindowState.Maximized;
            //    ed.FormBorderStyle = FormBorderStyle.None;
            //    ed.TopLevel = false;

            //    if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //    {
            //        tcp.Controls.Add(ed);
            //        ed.Show();
            //        this.tabControl1.Refresh();
            //    }
            //    else
            //        ed.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    this.ShowPrgMsg(ex.Message, MsgType.Error);
            //}
        }
        private void imbt_Monitor_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Monitor' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                Monitor ed = new Monitor(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void imbt_stationnoinfo_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='StationNoInfo' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                StationNoInfo ed = new StationNoInfo(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();

            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void imbt_createplot_Click(object sender, EventArgs e)
        {

            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='CreatePlot' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                CreatePlot ed = new CreatePlot(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void imbt_initcomport_Click(object sender, EventArgs e)
        {
            if (!imbt_initcomport.Checked)
            {
                this.InitComPort();
                imbt_initcomport.Checked = true;
            }
        }
        private void imbi_whmaterial_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    #region 权限判定
            //    //先判定是否有使用该程序的权限
            //    DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmWhMaterial' and funId='SHOWFROM'");
            //   // if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //     if (this.mUserInfo.rolecaption != "系统开发员")
            //    {
            //        throw new Exception("您没有权限使用该功能!!");
            //    }
            //    #endregion
            //    TabControlPanel tcp;
            //    FrmWhMaterial ed = new FrmWhMaterial(this);
            //    ed.WindowState = FormWindowState.Maximized;
            //    ed.FormBorderStyle = FormBorderStyle.None;
            //    ed.TopLevel = false;

            //    if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //    {
            //        tcp.Controls.Add(ed);
            //        ed.Show();
            //        this.tabControl1.Refresh();
            //    }
            //    else
            //        ed.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    this.ShowPrgMsg(ex.Message, MsgType.Error);
            //}
        }
        private void imbt_MaterialToPD_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    #region 权限判定
            //    //先判定是否有使用该程序的权限
            //    DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmMaterialToPD' and funId='SHOWFROM'");
            //    if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //    {
            //        throw new Exception("您没有权限使用该功能!!");
            //    }
            //    #endregion
            //    TabControlPanel tcp;
            //    FrmMaterialToPD ed = new FrmMaterialToPD(this);
            //    ed.WindowState = FormWindowState.Maximized;
            //    ed.FormBorderStyle = FormBorderStyle.None;
            //    ed.TopLevel = false;

            //    if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //    {
            //        tcp.Controls.Add(ed);
            //        ed.Show();
            //        this.tabControl1.Refresh();
            //    }
            //    else
            //        ed.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    this.ShowPrgMsg(ex.Message, MsgType.Error);
            //}
        }
        private void imbt_linehouseprint_Click(object sender, EventArgs e)
        {

            try
            {
                //#region 权限判定
                ////先判定是否有使用该程序的权限
                //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_MTR_RETURN' and funId='SHOWFROM'");
                //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                //{
                //    throw new Exception("您没有权限使用该功能!!");
                //}
                //#endregion
                //if (!ChkSoftInstall("CODESOFT 7"))
                //{
                //    MessageBoxEx.Show("本机没有安装Codesoft 7 不能使用该模块");
                //    return;
                //}
                //TabControlPanel tcp;
                //Frm_MTR_RETURN ed = new Frm_MTR_RETURN(this);
                //ed.WindowState = FormWindowState.Maximized;
                //ed.FormBorderStyle = FormBorderStyle.None;
                //ed.TopLevel = false;

                //if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                //{
                //    tcp.Controls.Add(ed);
                //    ed.Show();
                //    this.tabControl1.Refresh();
                //}
                //else
                //    ed.Dispose();
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_MaterialManage' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                if (!ChkSoftInstall("CODESOFT 7"))
                {
                    MessageBoxEx.Show("本机没有安装Codesoft 7 不能使用该模块");
                    return;
                }
                TabControlPanel tcp;
                Frm_MaterialManage ed = new Frm_MaterialManage(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
       
        }
        private void imbt_warehouse_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    #region 权限判定
            //    //先判定是否有使用该程序的权限
            //    DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='StoreLocManage' and funId='SHOWFROM'");
            //    if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //    {
            //        throw new Exception("您没有权限使用该功能!!");
            //    }
            //    #endregion
            //    TabControlPanel tcp;
            //    StoreLocManage ed = new StoreLocManage(this);
            //    ed.WindowState = FormWindowState.Maximized;
            //    ed.FormBorderStyle = FormBorderStyle.None;
            //    ed.TopLevel = false;

            //    if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //    {
            //        tcp.Controls.Add(ed);
            //        ed.Show();
            //        this.tabControl1.Refresh();
            //    }
            //    else
            //        ed.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    this.ShowPrgMsg(ex.Message, MsgType.Error);
            //}
        }
        public void imbt_userJurisdictionmanage_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmUseManage' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                FrmUseManage ed = new FrmUseManage(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void imbt_autofitoutmit_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    #region 权限判定
            //    //先判定是否有使用该程序的权限
            //    DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmSentMaterial' and funId='SHOWFROM'");
            //    if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //    {
            //        throw new Exception("您没有权限使用该功能!!");
            //    }
            //    #endregion
            //    TabControlPanel tcp;
            //    FrmSentMaterial ed = new FrmSentMaterial(this);
            //    ed.WindowState = FormWindowState.Maximized;
            //    ed.FormBorderStyle = FormBorderStyle.None;
            //    ed.TopLevel = false;

            //    if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //    {
            //        tcp.Controls.Add(ed);
            //        ed.Show();
            //        this.tabControl1.Refresh();
            //    }
            //    else
            //        ed.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }
        private void imbt_ErrorCode_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmErrorCode' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                FrmErrorCode ed = new FrmErrorCode(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }

        }
        private void imbt_reasoncode_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmReasonCode' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                FrmReasonCode ed = new FrmReasonCode(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }

        }
        private void imbt_packparam_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmPackParameters' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                FrmPackParameters ed = new FrmPackParameters(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        public void imbt_bom_Click(object sender, EventArgs e)
        {

            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmBomNo' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                FrmBomNo ed = new FrmBomNo(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowPrgMsg(ex.Message, MsgType.Error);
            }
        }
        private void imbt_storageing_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        #region 权限判定
        //        //先判定是否有使用该程序的权限
        //        DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='storagebuffer' and funId='SHOWFROM'");
        //        if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
        //        {
        //            throw new Exception("您没有权限使用该功能!!");
        //        }
        //        #endregion
        //        TabControlPanel tcp;
        //        storagebuffer ed = new storagebuffer(this,"",0);
        //        ed.WindowState = FormWindowState.Maximized;
        //        ed.FormBorderStyle = FormBorderStyle.None;
        //        ed.TopLevel = false;

        //        if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
        //        {
        //            tcp.Controls.Add(ed);
        //            ed.Show();
        //            this.tabControl1.Refresh();
        //        }
        //        else
        //            ed.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        }
        private void imbt_checkkpmaster_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='ChkKpMarster' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                ChkKpMarster ed = new ChkKpMarster(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void imbt_Target_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmTargetPlan' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                FrmTargetPlan ed = new FrmTargetPlan(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void imbt_merge_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmMoMerge' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                FrmMoMerge ed = new FrmMoMerge(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void imbt_Scrap_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmScrap' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                FrmScrap ed = new FrmScrap(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void imbt_sfcsnprint_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='PrintSFCSn' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                PrintSFCSn ed = new PrintSFCSn(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void imbt_customercom_Click(object sender, EventArgs e)
        {
        //    #region 权限判定
        //    //先判定是否有使用该程序的权限
        //    DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='CustomerComplaintManagement' and funId='SHOWFROM'");
        //    if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
        //    {
        //        throw new Exception("您没有权限使用该功能!!");
        //    }
        //    #endregion
        //    TabControlPanel tcp;
        //    CustomerComplaintManagement ed = new CustomerComplaintManagement(this);
        //    ed.WindowState = FormWindowState.Maximized;
        //    ed.FormBorderStyle = FormBorderStyle.None;
        //    ed.TopLevel = false;

        //    if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
        //    {
        //        tcp.Controls.Add(ed);
        //        ed.Show();
        //        this.tabControl1.Refresh();
        //    }
        //    else
        //        ed.Dispose();
        }

        public void imbt_logOut_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.Tabs.Count > 1)
            {
                if (MessageBoxEx.Show("注销会关闭所有已经打开的功能!!\n是否确定注销?", "提示",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Dictionary<string, TabItem> strname = new Dictionary<string, TabItem>();
                    foreach (TabItem ti in this.tabControl1.Tabs)
                    {
                        if (ti.Name != "systemmsg")
                            strname.Add(ti.Name, ti);
                    }
                    foreach (string str in strname.Keys)
                    {
                        (this.tabControl1.Tabs[str].AttachedControl.Controls[str] as Office2007Form).Close();
                        this.tabControl1.Tabs.Remove(strname[str]);
                    }
                    this.tabControl1.Refresh();
                }
            }
            this.LogOut();
            MainParent_Load(sender, e);
        }
        private void imbt_gangwang_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='SMTgangwangManage' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }
                #endregion
                TabControlPanel tcp;
                SMTgangwangManage ed = new SMTgangwangManage(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                {
                    ed.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
        #region 公共方法
        #region 公开的方法
        private void GetAllFunctionButtom()
        {
            for (int x = 0; x < this.sideBar2.ItemsContainer.SubItems.Count; x++)
            {
                if (sideBar2.ItemsContainer.SubItems[x].Name.ToUpper() == "sideBarStation".ToUpper())
                    break;

                for (int y = 0; y < this.sideBar2.ItemsContainer.SubItems[x].SubItems.Count; y++)
                {
                    string nam= this.sideBar2.ItemsContainer.SubItems[x].SubItems[y].Name.Remove(0, 5);
                    this.sideBar2.ItemsContainer.SubItems[x].SubItems[y].Visible = true;
                    DataRow[] ArrDr = this.mUserInfo.userPopList.Select(string.Format("progid='{0}' and funId='SHOWFROM'",
                     nam));
                    if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                    {
                        this.sideBar2.ItemsContainer.SubItems[x].SubItems[y].Visible = false;
                    }
                }
            }
        }
        /// <summary>
        /// 显示应用程序消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgColor"></param>
        public void ShowPrgMsg(string msg, MsgType msgtype)
        {
            this.lab_showgloabmsg.Invoke(new EventHandler(delegate
            {
                try
                {
                    ShowPrgMsg(msg);
                    return;
                    //1.自定义配置方式1
                    NoticeCenter.Cfg.Height = 140;//弹出框高
                    NoticeCenter.Cfg.Width = 250;//弹出框宽
                    NoticeCenter.Cfg.Overlap = false;//弹出框是否重叠，true时弹出的框位置一致，后面弹出的将遮挡前面弹出的
                    NoticeCenter.Cfg.TotalShow = 5;//同一时刻显示几个提示框，如果超出5个，该线程将等待前面的线程释放
                    Notice.Notice notice = new Notice.Notice
                    {
                        msg = msg,
                        wait = 5000,
                        detail = msg
                    };
                    NoticeCenter.Show(notice);
                    //NotifyForm.AnimalShow("提示", msg, 10);
                }
                catch
                {
                }

                //this.MsgTimerPrg.Enabled = true;
                //this.lab_showgloabmsg.BackColor = Color.FromArgb(255, 255, 192); //Color.Olive;
                //this.lab_showgloabmsg.Text = msg;

                //this.lab_showgloabmsg.ForeColor = MsgTypeColor[(int)msgtype];
            }));
        }
        public void ShowPrgMsg(string msg)
        {
            this.lab_showgloabmsg.Invoke(new EventHandler(delegate
            {
                try
                {
                    MsgNot.ShowMsg(msg);
                }
                catch
                {
                }
 
            }));
        }
        public void ShowSysMsg(string msg, MsgType msgtype)
        {
            this.lb_ShowSysMsg.Invoke(new EventHandler(delegate
            {
                this.lb_ShowSysMsg.BackColor = Color.FromArgb(255, 255, 192);
                this.lb_ShowSysMsg.Text = msg;
                this.lb_ShowSysMsg.ForeColor = MsgTypeColor[(int)msgtype];
            }));
        }
        //pb_globleprogressbar
        public void ShowGloabProgressbar(int val)
        {
            this.pb_globleprogressbar.Invoke(new EventHandler(delegate
            {
                this.pb_globleprogressbar.Value = val;
            }));
        }

        public void UpdateExcelSet()
        {
            //Microsoft.Win32.RegistryKey uninstallNode = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(:"SOFTWARE\Microsoft\Jet\4.0\Engines\");
            //string str = uninstallNode.OpenSubKey("Excel").GetValue("TypeGuessRows").ToString();
            //MessageBox.Show(str);
            //uninstallNode.OpenSubKey("Excel").SetValue("TypeGuessRows", 0);
        }
        #endregion
        private void tabItem_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            Rectangle r = this.tabControl1.SelectedTab.CloseButtonBounds;
            bool isClose = x > r.X && x < r.Right && y > r.Y && y < r.Bottom;

            if (isClose == true)
            {
                (this.tabControl1.Tabs[((TabItem)sender).Name].AttachedControl.Controls[((TabItem)sender).Name] as Office2007Form).Close();
                this.tabControl1.Tabs.Remove(this.tabControl1.SelectedTab);//.TabPages.Remove(this.MainTabControl.SelectedTab);
            }
        }

        /// <summary>
        /// 检查指定的软件是否安装
        /// </summary>
        /// <param name="softname"></param>
        /// <returns></returns>
        private bool ChkSoftInstall(string softname)
        {
            Microsoft.Win32.RegistryKey uninstallNode = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (string subKeyName in uninstallNode.GetSubKeyNames())
            {
                Microsoft.Win32.RegistryKey subKey = uninstallNode.OpenSubKey(subKeyName);
                object displayName = subKey.GetValue("DisplayName");
                if (displayName != null)
                {
                    // MessageBox.Show(displayName.ToString());
                    if (displayName.ToString().Contains(softname))
                    {
                        return true;
                        // MessageBox.Show(displayName.ToString());   
                    }
                }
            }
            return false;
        }
        #region 私有的方法
        private const long WM_GETMINMAXINFO = 0x24;
        public struct POINTAPI
        {
            public int x;
            public int y;
        }
        public struct MINMAXINFO
        {
            public POINTAPI ptReserved;
            public POINTAPI ptMaxSize;
            public POINTAPI ptMaxPosition;
            public POINTAPI ptMinTrackSize;
            public POINTAPI ptMaxTrackSize;
        }
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_GETMINMAXINFO)
            {
                MINMAXINFO mmi = (MINMAXINFO)m.GetLParam(typeof(MINMAXINFO));
                mmi.ptMinTrackSize.x = this.MinimumSize.Width;
                mmi.ptMinTrackSize.y = this.MinimumSize.Height;
                if (this.MaximumSize.Width != 0 || this.MaximumSize.Height != 0)
                {
                    mmi.ptMaxTrackSize.x = this.MaximumSize.Width;
                    mmi.ptMaxTrackSize.y = this.MaximumSize.Height;
                }
                mmi.ptMaxPosition.x = 1;
                mmi.ptMaxPosition.y = 1;

                System.Runtime.InteropServices.Marshal.StructureToPtr(mmi, m.LParam, true);
            }
        }
        #endregion

        int hwnd;
        private void timer1_Tick(object sender, EventArgs e)
        {
            // this.timer1.Enabled = false;
            // Mycontrol = GetFocusedControl(out hwnd);

            // checknetwork();

            //CheckNet = new CHKNET(checknetwork);
            //CheckNet.BeginInvoke(null, null);

        }
        #region xxxx

        /// <summary>
        /// 当前拥有焦点的控件
        /// </summary>
        /// <param name="formControl"></param>
        /// <returns></returns>   
        public static Control GetFocusedControl(out int hwnd)
        {
            hwnd = 0;
            Control focusedControl = null;

            try
            {
                IntPtr focusedHandle = GetFocus();

                if (focusedHandle != IntPtr.Zero)
                {
                    focusedControl = Control.FromChildHandle(focusedHandle);
                    hwnd = focusedHandle.ToInt32();
                }
            }
            catch { }

            return focusedControl;
        }
        #endregion
        #endregion

        #region  判断是否连接服务器网络

        System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
        //检查网络连接
        private void PingTest()
        {
            try
            {
                Ping p = new Ping();
                while (true)
                {
                    PingReply pr = p.Send(this.mIpAddress, 250);
                    if ((pr.Status.ToString().IndexOf("Success") == -1) || (Convert.ToInt32(pr.RoundtripTime) > 250))
                    {
                        lbnet.Invoke(new EventHandler(delegate
                        {
                            if (!lbnet.Visible)
                            {
                                lbnet.Dock = DockStyle.Fill;
                                lbnet.Visible = true;
                                lbnet.Text = "网络连接中断";
                            }
                            lbnet.BackColor = Color.Red;

                        }));
                    }
                    else
                    {
                        lbnet.Invoke(new EventHandler(delegate
                        {
                            label3.BackColor = Color.Transparent;

                            lbnet.Visible = false;
                            lbnet.Dock = DockStyle.None;
                        }));
                    }
                    Thread.Sleep(3000);
                }
            }
            catch
            {
            }
        }

        private delegate void CHKNET();
        CHKNET CheckNet;
        #endregion

        public bool EnaProgressBar = false;
        int pval = 1;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (this.EnaProgressBar)
            {
                this.pb_globleprogressbar.Value = pval;
                pval++;
                if (pval > 100)
                    pval = 0;
            }
            else
            {
                this.pb_globleprogressbar.Value = 0;
            }
        }

        private void imbt_repair_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmRepair' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }

                #endregion
                TabControlPanel tcp;
                FrmRepair ed = new FrmRepair(this, 1);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void imbt_feedermangement_Click(object sender, EventArgs e)
        {
            try
            {

                MessageBox.Show("Feeder 管理已经停用");
                //#region 权限判定
                ////先判定是否有使用该程序的权限
                //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FeederManage' and funId='SHOWFROM'");
                //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                //{
                //    throw new Exception("您没有权限使用该功能!!");
                //}

                //#endregion
                //TabControlPanel tcp;
                //FeederManage fm = new FeederManage(this);
                ////fm.WindowState = FormWindowState.Maximized;
                //fm.FormBorderStyle = FormBorderStyle.None;
                //fm.TopLevel = false;

                //if (!FindFrm(this.tabControl1, fm.Name, fm.Text, out tcp))
                //{
                //    tcp.Controls.Add(fm);
                //    fm.Show();
                //    this.tabControl1.Refresh();
                //}
                //else
                //    fm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void imbt_newkpnukber_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='AddMaterials' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }

                #endregion
                TabControlPanel tcp;
                AddMaterials ed = new AddMaterials(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void imbtUserEdtPassword_Click(object sender, EventArgs e)
        {

            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='EditPwd' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }

            #endregion

            TabControlPanel tcp;
            EditPwd ed = new EditPwd(this);
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose();
        }

        #region 处理消息
        /// <summary>
        /// 消息服务器IP地址
        /// </summary>
        private string MsgServerIP; //IP

        private string LocalhostIp;
        /// <summary>
        /// 消息服务器端口
        /// </summary>
        private int MsgPort;   //端口
        /// <summary>
        /// 提示消息类型
        /// </summary>
        private enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };
        /// <summary>
        /// 是否退出标志
        /// </summary>
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;

        /// <summary>
        /// 连接到消息服务器
        /// </summary>
        private bool ConnectMsgServer()
        {
            if (SetServerIPAndPort())
            {
                try
                {
                    //此处为方便演示，实际使用时要将Dns.GetHostName()改为服务器域名
                    //IPAddress ipAd = IPAddress.Parse("182.150.193.7");
                    //Dns.GetHostAddresses(Dns.GetHostName());
                    client = new TcpClient();
                    client.Connect(IPAddress.Parse(MsgServerIP), MsgPort);
                    AddTalkMessage("连接成功", mLogMsgType.Incoming, FontStyle.Bold);
                }
                catch (Exception ex)
                {
                    AddTalkMessage("连接失败，原因：" + ex.Message, mLogMsgType.Warning, FontStyle.Italic);
                    return false;
                }
                //获取网络流
                NetworkStream networkStream = client.GetStream();
                //将网络流作为二进制读写对象
                br = new BinaryReader(networkStream);
                bw = new BinaryWriter(networkStream);
                SendMessage("Login," + this.LocalhostIp + " " + this.gUserInfo.username);
                Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
                threadReceive.IsBackground = true;
                threadReceive.Start();
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 处理服务器信息
        /// </summary>
        private void ReceiveData()
        {
            string receiveString = null;
            while (isExit == false)
            {
                try
                {
                    //从网络流中读出字符串
                    //此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串
                    receiveString = br.ReadString();
                }
                catch
                {
                    if (isExit == false)
                    {
                        MessageBox.Show("与服务器失去连接");
                    }
                    break;
                }
                string[] splitString = receiveString.Split(',');
                string command = splitString[0].ToLower();
                switch (command)
                {
                    case "login":   //格式： login,用户名
                        AddOnline(splitString[1]);
                        break;
                    case "logout":  //格式： logout,用户名
                        RemoveUserName(splitString[1]);
                        break;
                    case "talk":    //格式： talk,用户名,对话信息
                        AddTalkMessage(splitString[1] + ":", mLogMsgType.Outgoing, FontStyle.Bold);
                        AddTalkMessage(receiveString.Substring(splitString[0].Length + splitString[1].Length + 2), mLogMsgType.Incoming, FontStyle.Bold);
                        break;
                    default:
                        AddTalkMessage("什么意思啊：" + receiveString, mLogMsgType.Normal, FontStyle.Bold);
                        break;
                }
            }
            //Application.Exit();
        }
        /// <summary>
        /// 向服务端发送消息
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(string message)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                bw.Write(message);
                bw.Flush();
            }
            catch
            {
                AddTalkMessage("发送失败", mLogMsgType.Warning, FontStyle.Bold);
            }
        }

        /// <summary>
        /// 在聊天对话框（txt_Message）中追加聊天信息
        /// </summary>
        /// <param name="message"></param>
        private void AddTalkMessage(string message, mLogMsgType msgtype, FontStyle fontstyle)
        {
            this.tbAllMsg.Invoke(new EventHandler(delegate
                {
                    tbAllMsg.TabStop = false;
                    tbAllMsg.SelectedText = string.Empty;
                    tbAllMsg.SelectionFont = new Font(tbAllMsg.SelectionFont, fontstyle);
                    tbAllMsg.SelectionColor = mLogMsgTypeColor[(int)msgtype];
                    this.tbAllMsg.AppendText(message + Environment.NewLine);
                    this.tbAllMsg.ScrollToCaret();
                }));
        }

        /// <summary>
        /// 在线框（lst_Online)中添加其他客户端信息
        /// </summary>
        /// <param name="userName"></param>
        private void AddOnline(string userName)
        {
            this.UserItemPanel.Invoke(new EventHandler(delegate
                {
                    ButtonItem BItem = null;
                    this.UserItemPanel.Items.Add(BItem = new DevComponents.DotNetBar.ButtonItem()
                    {
                        Name = userName,
                        Text = userName,
                        ButtonStyle = eButtonStyle.ImageAndText,
                        Image = imageList1.Images[0],
                        ImagePosition = eImagePosition.Left,
                    });
                    BItem.Click += new EventHandler(BItem_Click);
                    this.UserItemPanel.Refresh();
                }));
        }

        private void BItem_Click(object sender, EventArgs e)
        {
            foreach (object obj in this.UserItemPanel.Items)
            {
                if ((obj as ButtonItem).Name == (sender as ButtonItem).Name)
                {
                    (sender as ButtonItem).Checked = true;
                }
                else
                {
                    (obj as ButtonItem).Checked = false;
                }
            }
        }
        /// <summary>
        /// 在在线框(lst_Online)中移除不在线的客户端信息
        /// </summary>
        /// <param name="userName"></param>
        private void RemoveUserName(string userName)
        {
            this.UserItemPanel.Invoke(new EventHandler(delegate
                {
                    foreach (object obj in this.UserItemPanel.Items)
                    {
                        if ((obj as DevComponents.DotNetBar.ButtonItem).Name == userName)
                        {
                            this.UserItemPanel.Items.Remove(obj as DevComponents.DotNetBar.ButtonItem);
                            break;
                        }
                    }
                    this.UserItemPanel.Refresh();
                }));
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOut()
        {
            //未与服务器连接前 client 为 null
            if (client != null)
            {
                try
                {
                    SendMessage("Logout," + this.LocalhostIp + " " + this.gUserInfo.username);
                    isExit = true;
                    br.Close();
                    bw.Close();
                    client.Close();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 在发送信息的文本框中按下【Enter】键触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        bool keyA = false;
        private void tbSendMsgContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 17)
            {
                keyA = true;
                return;
            }
            if ((e.KeyValue == 13) && keyA)
            {
                //触发【发送】按钮的单击事件
                btMsgSend.PerformClick();
            }
            else
            {
                keyA = false;
            }

        }
        /// <summary>
        /// 【发送】按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btMsgSend_Click(object sender, EventArgs e)
        {
            if (UserItemPanel.GetChecked() != null)
            {
                //string sendtxt = string.Empty;
                //if (tbSendMsgContent.Rtf.IndexOf(:"{\pict\") != -1)
                //    sendtxt = tbSendMsgContent.Rtf;
                //else
                //sendtxt = tbSendMsgContent.Text;

                SendMessage("Talk," + UserItemPanel.GetChecked().Name + "," + tbSendMsgContent.Text.Trim());
                tbSendMsgContent.Clear();
            }
            else
            {
                MessageBox.Show("请先在【当前在线】中选择一个对话着");
            }
        }

        /// <summary>
        /// 配置消息服务器IP和端口
        /// </summary>
        /// <returns>成功返回真</returns>
        private bool SetServerIPAndPort()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("DllConfig.xml");
                this.MsgServerIP = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("MsgServer")).GetAttribute("IP");
                this.MsgPort = Convert.ToInt16(((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("MsgPort")).GetAttribute("PORT"));
                this.LocalhostIp = "0.0.0.0";
                IPAddress[] _IP = Dns.GetHostByName(Dns.GetHostName()).AddressList;
                string iptemp = string.Empty;
                foreach (IPAddress ip in _IP)
                {
                    foreach (byte str in ip.GetAddressBytes())
                    {
                        iptemp += str.ToString() + ".";
                    }
                    iptemp = iptemp.Substring(0, iptemp.Length - 1);
                    if (iptemp.Substring(0, 4) == this.MsgServerIP.Substring(0, 4))
                    {
                        this.LocalhostIp = iptemp;
                        break;
                    }
                }

                return true;
            }
            catch
            {
                this.ShowPrgMsg("消息服务器配置失败", MsgType.Warning);
                return false;
            }
        }

        #endregion

        private void imbtBomCompare_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmBomCompare' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }

                #endregion
                TabControlPanel tcp;
                FrmBomCompare ed = new FrmBomCompare(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void imbtInitwebser_Click(object sender, EventArgs e)
        {
            try
            {
                RecyclePools();
            }
            catch
            {
                MessageBoxEx.Show("回收失败!!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }

        }
        public void RecyclePools()
        {
            //XmlDocument doc = new XmlDocument();
            //string XmlName = "DllConfig.xml";
            //doc.Load(XmlName);
            //string str = string.Empty;
            //foreach (XmlNode xn in doc.SelectSingleNode("AutoCreate").SelectSingleNode("AppPools").ChildNodes)
            //{
            //    string _temp = ((XmlElement)xn).GetAttribute("Name");
            //    if (!string.IsNullOrEmpty(_temp))
            //    {
            //        str += RefWebService_BLL.refWebtErrorCode.Instance.ApplicationPoolsRecycle(_temp).Trim() + ";";
            //    }
            //}
           // MessageBoxEx.Show(str, "WEB修复", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void imbt_pallet_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmPallet' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            FrmPallet ed = new FrmPallet(this);
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose();

        }

        private void imbt_StockIn_Click(object sender, EventArgs e)
        {
            //#region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmStockIn' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}

            //#endregion
            //TabControlPanel tcp;
            //FrmStockIn ed = new FrmStockIn(this);
            //ed.WindowState = FormWindowState.Maximized;
            //ed.FormBorderStyle = FormBorderStyle.None;
            //ed.TopLevel = false;

            //if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //{
            //    tcp.Controls.Add(ed);
            //    ed.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    ed.Dispose();
        }

        private void imbt_qcinsp_Click(object sender, EventArgs e)
        {
            //#region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmQcInsp' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //FrmQcInsp ed = new FrmQcInsp(this);
            //ed.WindowState = FormWindowState.Maximized;
            //ed.FormBorderStyle = FormBorderStyle.None;
            //ed.TopLevel = false;

            //if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //{
            //    tcp.Controls.Add(ed);
            //    ed.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    ed.Dispose();
        }

        private void imbt_QM_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='QualityManagement' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            QualityManagement ed = new QualityManagement(this);
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose();

           // MessageBox.Show("请使用网页版产能报表功能");
        }

        private void imbt_stockreceive_Click(object sender, EventArgs e)
        {
          /*  #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_StockReceive' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            Frm_Receiving_Storage ed = new Frm_Receiving_Storage(this);
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose(); */
         
        }

        private void imbt_stockout_Click(object sender, EventArgs e)
        {
            //#region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_ProductOut' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //Frm_ProductOut ed = new Frm_ProductOut(this);
            //ed.WindowState = FormWindowState.Maximized;
            //ed.FormBorderStyle = FormBorderStyle.None;
            //ed.TopLevel = false;

            //if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //{
            //    tcp.Controls.Add(ed);
            //    ed.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    ed.Dispose();
        }

        private void imbt_stockquery_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_StockQuery' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            Frm_StockQuery ed = new Frm_StockQuery(this);
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose();
        }

        private void imbt_datapartition_Click(object sender, EventArgs e)
        {
            //#region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='DataPartition' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //DataPartition ed = new DataPartition(this, "", 0);
            //ed.WindowState = FormWindowState.Maximized;
            //ed.FormBorderStyle = FormBorderStyle.None;
            //ed.TopLevel = false;

            //if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //{
            //    tcp.Controls.Add(ed);
            //    ed.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    ed.Dispose();
        }

        private void imbt_custcomplaintquery_Click(object sender, EventArgs e)
        {
            //#region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='CustComplaintQuery' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //CustComplaintQuery ed = new CustComplaintQuery(this);
            //ed.WindowState = FormWindowState.Maximized;
            //ed.FormBorderStyle = FormBorderStyle.None;
            //ed.TopLevel = false;

            //if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //{
            //    tcp.Controls.Add(ed);
            //    ed.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    ed.Dispose();
        }

        private void imbt_QtyKpMaster_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmQryKpMaster' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            FrmQryKpMaster ed = new FrmQryKpMaster(this);
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose();
        }

        private void imbt_ReworkProduction_Click(object sender, EventArgs e)
        {

            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_ReworkPDMain' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }         
            #endregion
            TabControlPanel tcp;
            Frm_ReworkPDMain ed = new Frm_ReworkPDMain(this);
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose();
        }

        private void imbt_FQCReport_Click(object sender, EventArgs e)
        {
            //#region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_FQCReport' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //Frm_FQCReport ed = new Frm_FQCReport(this);
            //ed.WindowState = FormWindowState.Maximized;
            //ed.FormBorderStyle = FormBorderStyle.None;
            //ed.TopLevel = false;

            //if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //{
            //    tcp.Controls.Add(ed);
            //    ed.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    ed.Dispose();
        }

        private void imbt_Use_Rd_Click(object sender, EventArgs e)
        {

            //#region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_Qc_Rd' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //Frm_Qc_Rd ed = new Frm_Qc_Rd(this);
            //ed.WindowState = FormWindowState.Maximized;
            //ed.FormBorderStyle = FormBorderStyle.None;
            //ed.TopLevel = false;

            //if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            //{
            //    tcp.Controls.Add(ed);
            //    ed.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    ed.Dispose();
        }

        private void imbt_FrmAteEmp_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmAteEmp' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            FrmAteEmp ed = new FrmAteEmp(this);
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose();
        }

        private void imbt_FrmQueryTargetPlan_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmQueryTargetPlan' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            FrmQueryTargetPlan ed = new FrmQueryTargetPlan(this);
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose();
        }

        private void imbt_FrmReceiveMaterials_Click(object sender, EventArgs e)
        {
           /* #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmReceiveMaterials' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            FrmReceiveMaterials ed = new FrmReceiveMaterials(this);
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose();*/
        }

        private void imbt_Frm_POCheck_Click(object sender, EventArgs e)
        {
            //#region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_POCheck' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //Frm_POCheck pc = new Frm_POCheck(this);
            //pc.WindowState = FormWindowState.Maximized;
            //pc.FormBorderStyle = FormBorderStyle.None;
            //pc.TopLevel = false;

            //if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            //{
            //    tcp.Controls.Add(pc);
            //    pc.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    pc.Dispose();
        }
        ///// <summary>
        ///// 调用批量入库窗体
        ///// </summary>
        ///// <param name="trsn"></param>
        //public void storageing(string trsn)
        //{
        //    try
        //    {
        //        TabControlPanel tcp;
        //        storagebuffer ed = new storagebuffer(this, trsn, 1);
        //        ed.WindowState = FormWindowState.Maximized;
        //        ed.FormBorderStyle = FormBorderStyle.None;
        //        ed.TopLevel = false;

        //        if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
        //        {
        //            tcp.Controls.Add(ed);
        //            ed.Show();
        //            this.tabControl1.Refresh();
        //        }
        //        else
        //            ed.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

        private void imbt_FrmCustomerProduct_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='FrmCustomerProduct' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            FrmCustomerProduct pc = new FrmCustomerProduct(this);
            pc.WindowState = FormWindowState.Maximized;
            pc.FormBorderStyle = FormBorderStyle.None;
            pc.TopLevel = false;

            if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            {
                tcp.Controls.Add(pc);
                pc.Show();
                this.tabControl1.Refresh();
            }
            else
                pc.Dispose();
        }

        private void imbt_Frm_PerNum_Click(object sender, EventArgs e)
        {
            //#region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_PerNum' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //Frm_PerNum pc = new Frm_PerNum(this);
            //pc.WindowState = FormWindowState.Maximized;
            //pc.FormBorderStyle = FormBorderStyle.None;
            //pc.TopLevel = false;

            //if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            //{
            //    tcp.Controls.Add(pc);
            //    pc.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    pc.Dispose();
        }

        private void imbt_fmParMap_Click(object sender, EventArgs e)
        {
            //#region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='fmParMap' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //fmParMap pc = new fmParMap(this);
            //pc.WindowState = FormWindowState.Maximized;
            //pc.FormBorderStyle = FormBorderStyle.None;
            //pc.TopLevel = false;

            //if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            //{
            //    tcp.Controls.Add(pc);
            //    pc.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    pc.Dispose();
        }

        private void Imbt_Frm_FQC_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此功能已停止使用");
            //#region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_FQC' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //Frm_FQC pc = new Frm_FQC(this);
            //pc.WindowState = FormWindowState.Maximized;
            //pc.FormBorderStyle = FormBorderStyle.None;
            //pc.TopLevel = false;

            //if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            //{
            //    tcp.Controls.Add(pc);
            //    pc.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    pc.Dispose();
        }

        private void Imbt_Frm_Fqc_Report_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_Fqc_Report' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            Frm_Fqc_Report pc = new Frm_Fqc_Report(this);
            pc.WindowState = FormWindowState.Maximized;
            pc.FormBorderStyle = FormBorderStyle.None;
            pc.TopLevel = false;

            if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            {
                tcp.Controls.Add(pc);
                pc.Show();
                this.tabControl1.Refresh();
            }
            else
                pc.Dispose();
        }

        private void label3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

     

        private void imbt_Frm_Cust_SnRule_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_Cust_SnRule' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            Frm_Cust_SnRule pc = new Frm_Cust_SnRule(this);
            pc.WindowState = FormWindowState.Maximized;
            pc.FormBorderStyle = FormBorderStyle.None;
            pc.TopLevel = false;

            if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            {
                tcp.Controls.Add(pc);
                pc.Show();
                this.tabControl1.Refresh();
            }
            else
                pc.Dispose();
        }

        private void imbt_SAP_UPLOAD_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='SAP_UPLOAD' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            SAP_UPLOAD pc = new SAP_UPLOAD(this);
            pc.WindowState = FormWindowState.Maximized;
            pc.FormBorderStyle = FormBorderStyle.None;
            pc.TopLevel = false;

            if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            {
                tcp.Controls.Add(pc);
                pc.Show();
                this.tabControl1.Refresh();
            }
            else
                pc.Dispose();
        }

        private void imbt_Frm_OutOrder_Click(object sender, EventArgs e)
        {
          
            #region 权限判定 先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_OutOrder' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            Frm_OutOrder pc = new Frm_OutOrder(this);
            pc.WindowState = FormWindowState.Maximized;
            pc.FormBorderStyle = FormBorderStyle.None;
            pc.TopLevel = false;

            if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            {
                tcp.Controls.Add(pc);
                pc.Show();
                this.tabControl1.Refresh();
            }
            else
                pc.Dispose();
        

        }

        private void imbt_Frm_StockMove_Click(object sender, EventArgs e)
        {

            // #region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_StockMove' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //Frm_StockMove pc = new Frm_StockMove(this);
            //pc.WindowState = FormWindowState.Maximized;
            //pc.FormBorderStyle = FormBorderStyle.None;
            //pc.TopLevel = false;

            //if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            //{
            //    tcp.Controls.Add(pc);
            //    pc.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    pc.Dispose();
            
        }

        private void imbt_Frm_StockTOMove_Click(object sender, EventArgs e)
        {
            //  #region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_StockTOMove' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //Frm_StockTOMove pc = new Frm_StockTOMove(this);
            //pc.WindowState = FormWindowState.Maximized;
            //pc.FormBorderStyle = FormBorderStyle.None;
            //pc.TopLevel = false;

            //if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            //{
            //    tcp.Controls.Add(pc);
            //    pc.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    pc.Dispose();
            
        }

        private void imbt_Frm_ReceiveMoveStock_Click(object sender, EventArgs e)
        {
            //   #region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_ReceiveMoveStock' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //Frm_ReceiveMoveStock pc = new Frm_ReceiveMoveStock(this);
            //pc.WindowState = FormWindowState.Maximized;
            //pc.FormBorderStyle = FormBorderStyle.None;
            //pc.TopLevel = false;

            //if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            //{
            //    tcp.Controls.Add(pc);
            //    pc.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    pc.Dispose();
            
        }

        private void imbt_Frm_ShippingNotice_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_ShippingNotice' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            Frm_ShippingNotice pc = new Frm_ShippingNotice(this);
            pc.WindowState = FormWindowState.Maximized;
            pc.FormBorderStyle = FormBorderStyle.None;
            pc.TopLevel = false;

            if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            {
                tcp.Controls.Add(pc);
                pc.Show();
                this.tabControl1.Refresh();
            }
            else
                pc.Dispose();
        }

        private void imbt_wo_select_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='wo_select' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            wo_select pc = new wo_select(this);
            pc.WindowState = FormWindowState.Maximized;
            pc.FormBorderStyle = FormBorderStyle.None;
            pc.TopLevel = false;

            if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            {
                tcp.Controls.Add(pc);
                pc.Show();
                this.tabControl1.Refresh();
            }
            else
                pc.Dispose();
        }

        private void imbt_notify_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            this.Hide();
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
        }

        private void imbt_Frm_Material_Split_Click(object sender, EventArgs e)
        {
            //#region 权限判定
            ////先判定是否有使用该程序的权限
            //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_Material_Split' and funId='SHOWFROM'");
            //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            //{
            //    throw new Exception("您没有权限使用该功能!!");
            //}
            //#endregion
            //TabControlPanel tcp;
            //Frm_Material_Split pc = new Frm_Material_Split(this);
            //pc.WindowState = FormWindowState.Maximized;
            //pc.FormBorderStyle = FormBorderStyle.None;
            //pc.TopLevel = false;

            //if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            //{
            //    tcp.Controls.Add(pc);
            //    pc.Show();
            //    this.tabControl1.Refresh();
            //}
            //else
            //    pc.Dispose();

        }

        private void imbt_Frm_MaterialChgLine_Click(object sender, EventArgs e)
        {   
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_MaterialChgLine' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            Frm_MaterialChgLine pc = new Frm_MaterialChgLine(this);
            pc.WindowState = FormWindowState.Maximized;
            pc.FormBorderStyle = FormBorderStyle.None;
            pc.TopLevel = false;

            if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            {
                tcp.Controls.Add(pc);
                pc.Show();
                this.tabControl1.Refresh();
            }
            else
                pc.Dispose();
            
        }

        private void imbt_Frm_Plan_Control_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_Plan_Control' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            Frm_Plan_Control pc = new Frm_Plan_Control(this);
            pc.WindowState = FormWindowState.Maximized;
            pc.FormBorderStyle = FormBorderStyle.None;
            pc.TopLevel = false;

            if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            {
                tcp.Controls.Add(pc);
                pc.Show();
                this.tabControl1.Refresh();
            }
            else
                pc.Dispose();
        }

        private void imbt_Frm_TimeOut_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_TimeOut' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            Frm_TimeOut pc = new Frm_TimeOut(this);
            pc.WindowState = FormWindowState.Maximized;
            pc.FormBorderStyle = FormBorderStyle.None;
            pc.TopLevel = false;

            if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            {
                tcp.Controls.Add(pc);
                pc.Show();
                this.tabControl1.Refresh();
            }
            else
                pc.Dispose();
        }

        private void imbt_Frm_Version_info_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_Version_info' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            Frm_Version_info pc = new Frm_Version_info(this);
            pc.WindowState = FormWindowState.Maximized;
            pc.FormBorderStyle = FormBorderStyle.None;
            pc.TopLevel = false;

            if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            {
                tcp.Controls.Add(pc);
                pc.Show();
                this.tabControl1.Refresh();
            }
            else
                pc.Dispose();
        }

        private void imbt_Frm_PLAN_RATE_REPORT_Click(object sender, EventArgs e)
        {
            #region 权限判定
            //先判定是否有使用该程序的权限
            DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_PLAN_RATE_REPORT' and funId='SHOWFROM'");
            if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
            {
                throw new Exception("您没有权限使用该功能!!");
            }
            #endregion
            TabControlPanel tcp;
            Frm_PLAN_RATE_REPORT pc = new Frm_PLAN_RATE_REPORT(this);
            pc.WindowState = FormWindowState.Maximized;
            pc.FormBorderStyle = FormBorderStyle.None;
            pc.TopLevel = false;

            if (!FindFrm(this.tabControl1, pc.Name, pc.Text, out tcp))
            {
                tcp.Controls.Add(pc);
                pc.Show();
                this.tabControl1.Refresh();
            }
            else
                pc.Dispose();
        }

        private void imbt_Frm_RepairMain_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_RepairMain' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }

                #endregion
                TabControlPanel tcp;
                Frm_RepairMain ed = new Frm_RepairMain(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        //private void imbt_Frm_MO_Manage_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        #region 权限判定
        //        //先判定是否有使用该程序的权限
        //        DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_MO_Manage' and funId='SHOWFROM'");
        //        if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
        //        {
        //            throw new Exception("您没有权限使用该功能!!");
        //        }

        //        #endregion
        //        TabControlPanel tcp;
        //        Frm_MO_Manage ed = new Frm_MO_Manage(this);
        //        ed.WindowState = FormWindowState.Maximized;
        //        ed.FormBorderStyle = FormBorderStyle.None;
        //        ed.TopLevel = false;

        //        if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
        //        {
        //            tcp.Controls.Add(ed);
        //            ed.Show();
        //            this.tabControl1.Refresh();
        //        }
        //        else
        //            ed.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

        private void imbt_Frm_ReworkByPiece_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_ReworkByPiece' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }

                #endregion
                TabControlPanel tcp;
                Frm_ReworkByPiece ed = new Frm_ReworkByPiece(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void imbt_Frm_MO_Manage_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel_Mes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://mes.phicomm.com");
           // Process.Start("IEXPLORE.EXE", "http://mes.phicomm.com");
        }

        private void imbt_Frm_Station_Config_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_Station_Config' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }

                #endregion
                TabControlPanel tcp;
                Frm_Station_Config ed = new Frm_Station_Config(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void imbt_StationManagement_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                ////先判定是否有使用该程序的权限
                //DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_Station_Config' and funId='SHOWFROM'");
                //if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                //{
                //    throw new Exception("您没有权限使用该功能!!");
                //}

                #endregion
                TabControlPanel tcp;
                Frm_Station_Management ed = new Frm_Station_Management(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ReworkPD()
        {
            TabControlPanel tcp;
            Frm_ReworkPD ed = new Frm_ReworkPD(this, 0);
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose();
        }
        public void ReworkScrap()
        {
            TabControlPanel tcp;
            Frm_ReworkPD ed = new Frm_ReworkPD(this, 1);
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose();
        }
        public void ReworkInfoMation()
        {
            TabControlPanel tcp;
            Frm_ReworkDetailInfo ed = new Frm_ReworkDetailInfo();
            ed.WindowState = FormWindowState.Maximized;
            ed.FormBorderStyle = FormBorderStyle.None;
            ed.TopLevel = false;

            if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
            {
                tcp.Controls.Add(ed);
                ed.Show();
                this.tabControl1.Refresh();
            }
            else
                ed.Dispose();
        }

        private void MainParent_SizeChanged(object sender, EventArgs e)
        {
          
        }

        private void imbt_Frm_SmtStockIn_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_SmtStockIn' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }

                #endregion
                TabControlPanel tcp;
                Frm_SmtStockIn ed = new Frm_SmtStockIn(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void imbt_Frm_Packing_Ctn_Click(object sender, EventArgs e)
        {
            try
            {
                #region 权限判定
                //先判定是否有使用该程序的权限
                DataRow[] ArrDr = this.mUserInfo.userPopList.Select("progid='Frm_Packing_Ctn' and funId='SHOWFROM'");
                if ((ArrDr == null || ArrDr.Length < 1) && this.mUserInfo.rolecaption != "系统开发员")
                {
                    throw new Exception("您没有权限使用该功能!!");
                }

                #endregion
                TabControlPanel tcp;
                Frm_Packing_Ctn ed = new Frm_Packing_Ctn(this);
                ed.WindowState = FormWindowState.Maximized;
                ed.FormBorderStyle = FormBorderStyle.None;
                ed.TopLevel = false;

                if (!FindFrm(this.tabControl1, ed.Name, ed.Text, out tcp))
                {
                    tcp.Controls.Add(ed);
                    ed.Show();
                    this.tabControl1.Refresh();
                }
                else
                    ed.Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        

    

       

        
    }



      public class tUserInfo
    {
        /// <summary>
        /// 用户工号(主键)
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public string rolecaption { get; set; }
        /// <summary>
        /// 所在部门
        /// </summary>
        public string deptname { get; set; }
        /// <summary>
        /// 所属工厂编号
        /// </summary>
        public string facId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string pwd { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string userphone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string useremail { get; set; }
        /// <summary>
        /// 用户状态:0停用；1:启用
        /// </summary>
        public bool userstatus { get; set; }

        /// <summary>
        /// 保存用户的权限信息(progid and funid)
        /// </summary>
        public System.Data.DataTable userPopList { get; set; }
    }
}
