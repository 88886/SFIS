using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using GenericUtil;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace Stocking_In
{
    public partial class Frm_StockingIn : Office2007Form  //Form
    {
        public Frm_StockingIn()
        {
            InitializeComponent();
        }

        WebServices.SapConnector.SapConnector SapConn = new WebServices.SapConnector.SapConnector();
        private readonly string appfilename = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
        public string Line_Name = "NA";
        public string MYGROUP = "NA";
        public string Station=string.Empty;
        public string Section = string.Empty;
        public string My_MoNumber = string.Empty;
        public DataTable InputData = null;
        public string EmpLoyee = string.Empty;
        /// <summary>
        /// 打印入库单
        /// </summary>
        DataTable dtStock = null;

        /// <summary>
        /// C:\\SFIS\\SFIS.ini
        /// </summary>
        string IniFilePath = "C:\\SFIS\\SFIS.ini";
        /// <summary>
        /// 入库单号
        /// </summary>
        public string StockNo = string.Empty;
        /// <summary>
        /// 当前输入信息
        /// </summary>
        public string InputStr = string.Empty;

        bool b_findEmp = false;
        string sCmd = string.Empty;

        //private int WM_SYSCOMMAND = 0x112;
        //private long SC_MAXIMIZE = 0xF030;
        //private long SC_MINIMIZE = 0xF020;
        //private long SC_CLOSE = 0xF060;
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == WM_SYSCOMMAND)
        //    {
        //        if (m.WParam.ToInt64() == SC_MAXIMIZE)
        //        {
        //            //  MessageBox.Show("MAXIMIZE ");
        //            dataGridEsn.Columns[0].Width = (dataGridEsn.Width) / 3 * 2;
        //            dataGridEsn.Columns[1].Width = (dataGridEsn.Width) / 3 * 1;
        //              return;
        //        }
        //        if (m.WParam.ToInt64() == SC_MINIMIZE)
        //        {
        //            //  MessageBox.Show("MINIMIZE ");
        //            dataGridEsn.Columns[0].Width = (dataGridEsn.Width) / 3 * 2;
        //            dataGridEsn.Columns[1].Width = (dataGridEsn.Width) / 3 * 1;
        //             return;
        //        }
        //        if (m.WParam.ToInt64() == SC_CLOSE)
        //        {
        //            //MessageBox.Show("CLOSE ");
        //            // return;
        //        }
        //    }
        //    base.WndProc(ref m);
        //    SetWinForm();
        //}

        private void setTag(Control cons)
        {
            //MessageBox.Show("setTag");
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    //MessageBox.Show("ok");
                    setTag(con);
                }

            }
        }
        //调整每个控件的大小
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {

                //MessageBox.Show(con.Tag.ToString());
                string[] mytag = con.Tag.ToString().Split(':');
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }
        //窗体大小自适应
        private void NewOrderDashForm_Resize(object sender, EventArgs e)
        {
            //MessageBox.Show("resize");
            // throw new Exception("The method or operation is not implemented.");            
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
            this.Text = this.Width.ToString() + " " + this.Height.ToString();
        }

        int X=100;
        int Y = 100;
        private void SetWinForm()
        {
            
            panelEx7.Height = panelEx5.Height / 2;
            panelEx9.Height = panelEx5.Height / 2;
            panelEx5.Width = (panelEx1.Width - panelEx2.Width) / 2;
            if (dataGridEsn.Columns.Count > 0)
            {
                SetdataGridWidth();
            }
        }

        private void SetdataGridWidth()
        {
            dataGridEsn.Columns[0].Width = (dataGridEsn.Width) / 3 * 2;
            dataGridEsn.Columns[1].Width = (dataGridEsn.Width) / 3 * 1 - 5;

            dataGridTray.Columns[0].Width = (dataGridTray.Width) / 3 * 2;
            dataGridTray.Columns[1].Width = (dataGridTray.Width) / 3 * 1 - 5;

            dataGridCarton.Columns[0].Width = (dataGridCarton.Width) / 3 * 2;
            dataGridCarton.Columns[1].Width = (dataGridCarton.Width) / 3 * 1 - 5;

            dataGridPallet.Columns[0].Width = (dataGridPallet.Width) / 3 * 2;
            dataGridPallet.Columns[1].Width = (dataGridPallet.Width) / 3 * 1 - 5;
        }

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
                    WriteLog(msg);
                }));
            }
            catch
            {
            }
        }
        #endregion

        #region 调用API
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
        string key, string def, StringBuilder retVal,
        int size, string filePath);

        public static void IniWriteValue(string Section, string Key, string Value, string filepath)//对ini文件进行写操作的函数 
        {
            WritePrivateProfileString(Section, Key, Value, filepath);
        }

        public static string IniReadValue(string Section, string Key, string filepath)//对ini文件进行读操作的函数 
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
            255, filepath);
            return temp.ToString();
        }
        #endregion 
        public void SetStation()
        {
            LabLine.Text = Line_Name;
            LabMyGroup.Text = MYGROUP;
            IniWriteValue("STOCKIN", "LINE", Line_Name, IniFilePath);
            IniWriteValue("STOCKIN", "MYGROUP", MYGROUP, IniFilePath);
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
        private void Frm_StockingIn_Load(object sender, EventArgs e)
        {

            if (!Directory.Exists("C:\\SFIS"))
                Directory.CreateDirectory("C:\\SFIS");


            FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath);        
            this.Text = string.Format("{0} Version:{1} (Build Date:{2})", "Stockin_In", myFileVersion.FileVersion, System.IO.File.GetLastWriteTime(System.Windows.Forms.Application.ExecutablePath).ToShortDateString());

           string _StrErr=string.Empty;
           if (!OperateDB.Check_Version("STOCKIN", System.Windows.Forms.Application.ProductVersion, null, null, null, out _StrErr))
            {
                if (_StrErr == "OK")
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
                else
                {
                    MessageBox.Show("检查版本失败:" + _StrErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }

        Line_Name=IniReadValue("STOCKIN", "LINE", IniFilePath);
        MYGROUP = IniReadValue("STOCKIN", "MYGROUP", IniFilePath);
        SetStation();

            dataGridEsn.Columns.Add("ESN", "ESN");
            dataGridEsn.Columns.Add("Toatal", "Toatal Qty");          
            dataGridEsn.Rows.Add("Toatal", "0");

            dataGridTray.Columns.Add("Tray", "Tray");
            dataGridTray.Columns.Add("Toatal", "Toatal Qty");
            dataGridTray.Rows.Add("Toatal", "0");

            dataGridCarton.Columns.Add("CARTON", "CARTON");
            dataGridCarton.Columns.Add("Toatal", "Toatal Qty");
            dataGridCarton.Rows.Add("Toatal", "0");

            dataGridPallet.Columns.Add("Pallet", "PALLET");
            dataGridPallet.Columns.Add("Toatal", "Toatal Qty");      
            dataGridPallet.Rows.Add("Toatal", "0");
            SetdataGridWidth();
            dataGridEmp.Rows.Add("", "", "0");
            SetStation();

            if (!Directory.Exists(Application.StartupPath + @"\Database"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Database");
            }
            if (File.Exists(Application.StartupPath + @"\Database\dbtemp.mdb"))
            {
                File.Delete(Application.StartupPath + @"\Database\dbtemp.mdb");
            }
            CreateAccessDb CDB = new CreateAccessDb();
            CDB._CreateMDB();
            CDB._Create_T_CHECK_SAP();
            cdbAccess cda = new cdbAccess();
            cda.ExecuteOleDbCommand("delete from T_CHECK_SAP");
            txt_InputStr.Focus();
        }

        private void Frm_StockingIn_Resize(object sender, EventArgs e)
        {             
            SetWinForm();
        }

        private void imbt_setLineStation_Click(object sender, EventArgs e)
        {
            Frm_StationName fsn = new Frm_StationName(this);
            fsn.ShowDialog();
        }

        private void ClearData()
        {
            dataGridEsn.Rows.Clear();
            dataGridEsn.Rows.Add("Toatal", "0");
            dataGridTray.Rows.Clear();
            dataGridTray.Rows.Add("Toatal", "0");
            dataGridCarton.Rows.Clear();
            dataGridCarton.Rows.Add("Toatal", "0");
            dataGridPallet.Rows.Clear();
            dataGridPallet.Rows.Add("Toatal", "0");
            listBoxEsn.Items.Clear();
            listBoxTray.Items.Clear();
            listBoxCarton.Items.Clear();
            listBoxPallet.Items.Clear();
            cdbAccess cda = new cdbAccess();
            cda.ExecuteOleDbCommand("delete from T_CHECK_SAP");
        }

        private void txt_InputStr_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_InputStr.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                     InputStr = txt_InputStr.Text.Trim();
                     InputData = null;
                    if (InputStr == "UNDO")
                    {
                        EmpLoyee = string.Empty;
                        dataGridEmp.Rows[0].Cells[0].Value ="";
                        dataGridEmp.Rows[0].Cells[1].Value = "";
                        dataGridEmp.Rows[0].Cells[2].Value = "0";
                        LabError.Visible = false;
                        ClearData();
                        b_findEmp = false;
                        sCmd = string.Empty;
                        SendMsg(mLogMsgType.Incoming,"UNDO OK");
                        return;
                    }
                    if (InputStr == "NA")
                        return;

                    if (!b_findEmp)
                    {
                        b_findEmp = CHECK_EMP();
                        return;
                    }
                    if (InputStr == "END")
                    {
                        UpLoad_System();
                        return;
                    }
                    else
                    {
                        
                        if (dataGridEmp.Rows[0].Cells[2].Value.ToString() == "0")
                        {
                            CHECK_CMD();
                            return;                               
                        }
                        if (!CheckDupInput())
                        {
                            SendMsg(mLogMsgType.Error, InputStr + "  Duplicate Input");
                            return;
                        }
                        int m = CHECK_ERROR();
                        if (m != 0)
                        {
                            string Msg = string.Empty;
                            switch (m)
                            {
                                case 1:
                                    Msg = "  没有数据";
                                    break;
                                case  2:
                                    Msg = "  有报废产品";
                                    break;
                                case 3:
                                    Msg = "  有待维修产品";
                                    break;
                            }

                            SendMsg(mLogMsgType.Error,InputStr+ Msg);
                            return;
                        }
                        if (imbt_CheckSAP.Checked)
                        {
                            if (!CHECK_SAP())
                                return;
                        }

                        m = CHECK_WOSTATUS();
                        if (m != 1 && m != 2)
                        {
                            string Msg = string.Empty;
                            switch (m)
                            {
                                case 0:
                                    Msg = " Waiting Relaese";
                                    break;
                                case 1:
                                    Msg = " OK";
                                    break;
                                case 2:
                                    Msg = " OK";
                                    break;
                                case 3:
                                    Msg = " WO IS CLOSED ";
                                    break;
                                case 4:
                                    Msg = " WO HOLD";
                                    break;
                                case 99:
                                    Msg = " WO Not Exits";
                                    break;
                                default:
                                    Msg = " WO STATU ERROR";
                                    break;
                            }
                            SendMsg(mLogMsgType.Error, InputStr + Msg);
                            return;
                        }

                        if (!CHECK_CLOSE_STATUS())
                            return;
                        if (imbt_CheckRoute.Checked)
                        {
                            if (!CHECK_ROUTE())
                                return;
                        }
                        if (Check_DATA_Correct() != 0)
                            return;
                       
                        InputScanData();
                        SendMsg(mLogMsgType.Incoming,InputStr+" OK");
                    }





                }
                catch (Exception ex)
                {
                    SendMsg(mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    txt_InputStr.Text = string.Empty;
                    txt_InputStr.Focus();
                }
            }
        }

        private bool CHECK_EMP()
        {
            bool Flag = false;
            string EMP = InputStr;
            try
            {
                DataTable dt = OperateDB.Get_User_Info(EMP.Split('-')[0], EMP.Split('-')[1]);
                if (dt.Rows.Count > 0)
                {
                    //ListViewItem lvi = new ListViewItem();
                    //View.Details = System.Windows.Forms.View.Details;
                    //lvi.SubItems[0].Text = "00";
                    //lvi.SubItems.Add(dt.Rows[0]["USERID"].ToString());
                    //lvi.SubItems.Add(dt.Rows[0]["USERNAME"].ToString());
                    //listView1.Items.Add(lvi);
                    EmpLoyee = EMP;
                    dataGridEmp.Rows[0].Cells[0].Value = dt.Rows[0]["USERID"].ToString();
                    dataGridEmp.Rows[0].Cells[1].Value = dt.Rows[0]["USERNAME"].ToString();
                    SendMsg(mLogMsgType.Incoming, "EMP OK,Input CMD");
                    Flag = true;
                }
                else
                {
                    SendMsg(mLogMsgType.Error, "NO Employee");
                }
            }
            catch (Exception ex)
            {
                SendMsg(mLogMsgType.Error, "检查权限失败:"+ex.Message);
                Flag = false;
            }
            return Flag;
        }

        private void CHECK_CMD()
        {
            bool Flag = true;
            switch (InputStr)
            {
                case "ESN":

                    sCmd = "ESN";
                    dataGridEmp.Rows[0].Cells[2].Value = "E";
                    break;
                case "TRAY":

                    sCmd = "TRAYNO";
                    dataGridEmp.Rows[0].Cells[2].Value = "T";
                    break;
                case "CARTON":
                    sCmd = "CARTONNUMBER";
                    dataGridEmp.Rows[0].Cells[2].Value = "C";
                    break;
                case "PALLET":
                    sCmd = "PALLETNUMBER";
                    dataGridEmp.Rows[0].Cells[2].Value = "P";
                    break;

                default:
                    sCmd = string.Empty;
                    dataGridEmp.Rows[0].Cells[2].Value = "0";
                    Flag = false;
                    break;

            }
            if (Flag)
            SendMsg(mLogMsgType.Incoming, "CMD OK");
            else
                SendMsg(mLogMsgType.Error, "CMD ERROR");
           
        }

        private bool CheckDupInput()
        {
            bool Flag = true;

            switch (dataGridEmp.Rows[0].Cells[2].Value.ToString())
            {
                case "E":
                    foreach (DataGridViewRow dgvr in dataGridEsn.Rows)
                    {
                        if (InputStr == dgvr.Cells[0].Value.ToString())
                            Flag = false;
                    }

                    break;
                case "T":
                    foreach (DataGridViewRow dgvr in dataGridTray.Rows)
                    {
                        if (InputStr == dgvr.Cells[0].Value.ToString())
                            Flag = false;
                    }
                    break;
                case "C":
                    foreach (DataGridViewRow dgvr in dataGridCarton.Rows)
                    {
                        if (InputStr == dgvr.Cells[0].Value.ToString())
                            Flag = false;
                    }
                    break;
                case "P":
                    foreach (DataGridViewRow dgvr in  dataGridPallet.Rows )
                    {
                        if (InputStr == dgvr.Cells[0].Value.ToString())
                            Flag = false;
                    }
                    break;
            }



            return Flag;
        }

        /// <summary>
        /// 检查产品状态 1 没有数据,2有报废,3有不良
        /// </summary>
        /// <returns></returns>
        private int CHECK_ERROR()
        {
            int Flag = 0;
            DataTable dt = OperateDB.Get_Wip_Tracking(sCmd, InputStr);
            InputData = dt;
            if (dt.Rows.Count == 0)
            {
                Flag = 1;
                return Flag;
            }
            if (OperateDB.getNewTable(dt, string.Format("SCRAPFLAG<>'{0}'", "0")).Rows.Count > 0)
                Flag = 2;
            if (OperateDB.getNewTable(dt, string.Format("ERRFLAG<>'{0}'", "0")).Rows.Count > 0)
                Flag = 3;
            My_MoNumber = dt.Rows[0]["WOID"].ToString();
            return Flag;
        }

        private int  CHECK_WOSTATUS()
        {
            DataTable dt = OperateDB.Get_WoInfo(My_MoNumber);
            if (dt.Rows.Count == 0)
                return 99;
            else
            return Convert.ToInt32( dt.Rows[0]["WOSTATE"].ToString());
        }

        private bool CHECK_CLOSE_STATUS()
        {
            bool Flag = true;
            int PACKTYPE = 0;
            Dictionary<string, object> dic = new Dictionary<string, object>();
       
            switch (dataGridEmp.Rows[0].Cells[2].Value.ToString())
            {
                case "T":
                    PACKTYPE = 0;
                    break;
                case "C":
                    PACKTYPE = 1;
                    break;
                case "P":
                    PACKTYPE = 2;
                    break;
            }
            dic.Add("PALLETNUMBER", InputStr);
            dic.Add("PACKTYPE", PACKTYPE);
            DataTable dt = OperateDB.Get_Pallet_Info(dic);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["CLOSEFLAG"].ToString()) == 0)
                {
                    Flag = false;
                    SendMsg(mLogMsgType.Error, InputStr +" 没有关闭");
                }
                if (dt.Rows.Count > 1)
                {
                    Flag = false;
                    SendMsg(mLogMsgType.Error, InputStr + " 数据存在多笔,请联系SFIS人员");
                }
            }

            return Flag;
        }

        private bool CHECK_ROUTE()
        {
            bool Flag = true;
            DataView dataView = InputData.DefaultView;
            DataTable dataTableDistinct = dataView.ToTable(true, "LOCSTATION");
            foreach (DataRow dr in dataTableDistinct.Rows)
            {
                DataTable dt = OperateDB.getNewTable(InputData,string.Format("LOCSTATION='{0}'",dr["LOCSTATION"].ToString()));
               string _StrErr= OperateDB.CHECK_ROUTE(dt.Rows[0]["ESN"].ToString(), MYGROUP);
               if (_StrErr != "OK")
               {
                   Flag = false;
                   SendMsg(mLogMsgType.Error,dt.Rows[0]["ESN"].ToString()+" "+_StrErr);
                   break;
               }
            }
            return Flag;
        }

        private int Check_DATA_Correct()
        {
            int Flag = 0;
            foreach (DataRow dr in InputData.Rows)
            {
                if (OperateDB.CHECK_DATA_Z_WHS_WIP_TRACKING(dr["ESN"].ToString()) != 0)
                {
                    SendMsg(mLogMsgType.Error,dr["ESN"].ToString()+" 仓库数据重复");
                    return 1;
                }
            }


            return Flag;
        }
        private void InputScanData()
        {
            int _Total=0;
            switch (dataGridEmp.Rows[0].Cells[2].Value.ToString())
            {
                case "E":
                    dataGridEsn.Rows.Add(InputStr, InputData.Rows.Count.ToString());
                    listBoxEsn.Items.Add(InputStr);
                   for ( int j=1;j<dataGridEsn.Rows.Count;j++)
                    {
                        _Total += Convert.ToInt32(dataGridEsn.Rows[j].Cells[1].Value.ToString());
                       dataGridEsn.Rows[0].Cells[1].Value=_Total.ToString();
                    }

                    break;
                case "T":
                    dataGridTray.Rows.Add(InputStr, InputData.Rows.Count.ToString());
                    listBoxTray.Items.Add(InputStr);
                      for ( int j=1;j<dataGridEsn.Rows.Count;j++)
                    {
                       _Total+=Convert.ToInt32(dataGridTray.Rows[j].Cells[1].Value.ToString());
                       dataGridTray.Rows[0].Cells[1].Value=_Total.ToString();
                    }
                    break;
                case "C":
                    dataGridCarton.Rows.Add(InputStr, InputData.Rows.Count.ToString());
                    listBoxCarton.Items.Add(InputStr);
                      for ( int j=1;j<dataGridEsn.Rows.Count;j++)
                    {
                       _Total+=Convert.ToInt32(dataGridCarton.Rows[j].Cells[1].Value.ToString());
                       dataGridCarton.Rows[0].Cells[1].Value=_Total.ToString();
                    }
                    break;
                case "P":
                    dataGridPallet.Rows.Add(InputStr, InputData.Rows.Count.ToString());
                    listBoxPallet.Items.Add(InputStr);
                      for ( int j=1;j<dataGridPallet.Rows.Count;j++)
                    {
                       _Total+=Convert.ToInt32(dataGridPallet.Rows[j].Cells[1].Value.ToString());
                       dataGridPallet.Rows[0].Cells[1].Value = _Total.ToString();
                    }
                    break;
            }

            cdbAccess cda = new cdbAccess();
            cda.ExecuteOleDbCommand(string.Format("insert into T_CHECK_SAP (WOID,PARTNUMBER,DATA_NO,DATA_QTY) VALUES ('{0}','{1}','{2}','{3}')", InputData.Rows[0]["WOID"].ToString(),InputData.Rows[0]["PARTNUMBER"].ToString(), InputStr, InputData.Rows.Count));
        }

        private void UpLoad_System()
        {
            if (listBoxEsn.Items.Count == 0 && listBoxTray.Items.Count == 0 && listBoxCarton.Items.Count == 0 && listBoxPallet.Items.Count == 0)
            {
                SendMsg(mLogMsgType.Error, "No Data Upload");
                return;
            }
            else
            {
                SendMsg(mLogMsgType.Normal, "更改入库单号");
                #region 更改入库单号
                StockNo =OperateDB.GetStockinNumber();
                Dictionary<string, object> mst = null;
                List<string> Fields = null;
                string _StrErr = string.Empty;
                if (listBoxEsn.Items.Count > 0)
                {
                    Fields = new List<string>();
                    Fields.Add("ESN");
                    foreach (string ItemStr in listBoxEsn.Items)
                    {
                        mst = new Dictionary<string, object>();
                        mst.Add("STORENUMBER", StockNo);
                        mst.Add("ESN", ItemStr);
                      _StrErr=  OperateDB.UPDATE_WIP_TRACKING(mst,Fields);
                      if (_StrErr != "OK")
                      {
                          SendMsg( mLogMsgType.Error,"UPDATE STOCKNO Faill: "+_StrErr );
                          return;
                      }
                    }
                }
                if (listBoxTray.Items.Count > 0)
                {
                    Fields = new List<string>();
                    Fields.Add("TRAYNO");
                    foreach (string ItemStr in listBoxTray.Items)
                    {
                        mst = new Dictionary<string, object>();
                        mst.Add("STORENUMBER", StockNo);
                        mst.Add("TRAYNO", ItemStr);
                        _StrErr = OperateDB.UPDATE_WIP_TRACKING(mst, Fields);
                        if (_StrErr != "OK")
                        {
                            SendMsg(mLogMsgType.Error, "UPDATE STOCKNO Faill: " + _StrErr);
                            return;
                        }
                    }
                }
                if (listBoxCarton.Items.Count > 0)
                {
                    Fields = new List<string>();
                    Fields.Add("CARTONNUMBER");
                    foreach (string ItemStr in listBoxCarton.Items)
                    {
                        mst = new Dictionary<string, object>();
                        mst.Add("STORENUMBER", StockNo);
                        mst.Add("CARTONNUMBER", ItemStr);
                        _StrErr = OperateDB.UPDATE_WIP_TRACKING(mst, Fields);
                        if (_StrErr != "OK")
                        {
                            SendMsg(mLogMsgType.Error, "UPDATE STOCKNO Faill: " + _StrErr);
                            return;
                        }
                    }
                }
                if (listBoxPallet.Items.Count > 0)
                {
                    Fields = new List<string>();
                    Fields.Add("PALLETNUMBER");
                    foreach (string ItemStr in listBoxPallet.Items)
                    {
                        mst = new Dictionary<string, object>();
                        mst.Add("STORENUMBER", StockNo);
                        mst.Add("PALLETNUMBER", ItemStr);
                        _StrErr = OperateDB.UPDATE_WIP_TRACKING(mst, Fields);
                        if (_StrErr != "OK")
                        {
                            SendMsg(mLogMsgType.Error, "UPDATE STOCKNO Faill: " + _StrErr);
                            return;
                        }
                    }
                }
                #endregion

                LabError.Text = string.Empty;
                SendMsg(mLogMsgType.Normal, "执行过站...");
                Call_TEST_STOCKIN(StockNo);
                if (!string.IsNullOrEmpty(LabError.Text))
                {
                    SendMsg(mLogMsgType.Error, "Call_TEST_STOCKIN Error");
                    return;
                }
                SendMsg(mLogMsgType.Normal, "更改WIP STATION");
                UPDATE_WIPSTATION(StockNo);
                SendMsg(mLogMsgType.Normal, "写入数据到仓库...");
                Insert_Z_WIP_TRACKING(StockNo);
                ClearData();
                SendMsg(mLogMsgType.Normal, "数据上传完成....");
                PrintInventoryDocuments(StockNo); 
            }
        }

        private bool Call_TEST_STOCKIN(string STOCK_NO)
        {
          //  Application.DoEvents();
            bool C_Flag = false;
            int Cur_Rec=0;
            int Tot_Rec=0;
            int Err_Rec = 0;
            DataTable dt = OperateDB.getNewTable( OperateDB.Get_Wip_Tracking("STORENUMBER", STOCK_NO),string.Format("LOCSTATION<>'{0}'",MYGROUP));
            Tot_Rec = dt.Rows.Count;
            string _StrErr = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                _StrErr = OperateDB.TEST_MAIN_ONLY(dr["ESN"].ToString(), MYGROUP, Section, MYGROUP+"1", EmpLoyee, Line_Name);
                 Cur_Rec = Cur_Rec + 1;
                 progressBarX1.Value =(int)(Math.Round((decimal)Cur_Rec / Tot_Rec, 2)*100); //Convert.ToInt32((Convert.ToDouble(Cur_Rec / Tot_Rec) * 100));
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
                    LabError.Text = "NG Count-->"+Err_Rec.ToString();                
                }
            }
            return C_Flag;
        }

        private static void UPDATE_WIPSTATION(string STOCK_NO)
        {

            DataTable dt = OperateDB.Get_Wip_Tracking("STORENUMBER", STOCK_NO, "DISTINCT WOID");
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("WOID", dr["WOID"].ToString());
                dic.Add("STORENUMBER", STOCK_NO);
                dic.Add("WIPSTATION", OperateDB.GetWOLoc(dr["WOID"].ToString()));
                List<string> ListFields = new List<string>();
                ListFields = new List<string>();
                ListFields.Add("WOID");
                ListFields.Add("STORENUMBER");           
                OperateDB.UPDATE_WIP_TRACKING(dic, ListFields);
            }
        }
        private void Insert_Z_WIP_TRACKING(string STOCK_NO)
        {
          string _StrErr=  OperateDB.Inser_Z_WIP_TRACKING(STOCK_NO);
          if (_StrErr == "OK")
              SendMsg(mLogMsgType.Incoming,"上传数据完成");
          else
              SendMsg(mLogMsgType.Error, "上传数据到仓库异常:"+_StrErr);

        }

        private void dataGridEmp_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //foreach (DataGridViewColumn item in this.dataGridEmp.Columns)
            //{
            //    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}
        }

        private void dataGridEmp_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void imbt_CheckSAP_Click(object sender, EventArgs e)
        {
            imbt_CheckSAP.Checked = !imbt_CheckSAP.Checked;
        }
        private bool CHECK_SAP()
        {
            bool AccessFlag = false;
            cdbAccess cdb = new cdbAccess();
            DataTable dt = cdb.GetDatatable("select woId,partnumber,sum(data_qty) as  QTY from T_CHECK_SAP group by  woId,partnumber");
            if (dt.Rows.Count == 0)
            {
                AccessFlag = true;
                dt.Rows.Add(InputData.Rows[0]["WOID"].ToString(), InputData.Rows[0]["partnumber"].ToString(),  InputData.Rows.Count);
            }

            bool sFlag = false;
            string FactoryId = string.Empty;
         

            foreach (DataRow dr in dt.Rows)
            {
                int CHK_QTY = 0;
                if (dr["WOID"].ToString() == InputData.Rows[0]["WOID"].ToString() && !AccessFlag)
                    CHK_QTY= Convert.ToInt32(dr["QTY"].ToString()) + InputData.Rows.Count;
                else
                   CHK_QTY=Convert.ToInt32(dr["QTY"].ToString());

                FactoryId = OperateDB.Get_WoInfo(dr["WOID"].ToString()).Rows[0]["FACTORYID"].ToString();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PARTNUMBER", dr["PARTNUMBER"].ToString());
                dic.Add("WOID", dr["WOID"].ToString());
                dic.Add("QTY", CHK_QTY);
                dic.Add("EMP_NO", dr["QTY"].ToString());
                dic.Add("EMP_NAME", dr["QTY"].ToString());
                dic.Add("PLANT", string.IsNullOrEmpty(FactoryId) ? "2100" : FactoryId);
                string[] LsMsg = SapConn.CHK_STOCKIN_Z_RFC_AUFNR_MIGO(DictionaryToJson(dic));                
                if (LsMsg.Length < 5)
                {
                    SendMsg(mLogMsgType.Error, "检查SAP 发生异常" + LsMsg[0].ToString());
                    sFlag = false;
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

                        SendMsg(mLogMsgType.Incoming, string.Format("检查SAP 工单:[{0}] 数量:[{1}] OK", dr["WOID"].ToString(), CHK_QTY.ToString()));
                        
                        sFlag = true;
                    }
                    else
                    {
                        string sSAP_MSG = string.Format("工单:{0},料号:{1},数量:{2},MSG:{3}", dr["WOID"].ToString(), dr["PARTNUMBER"].ToString(), CHK_QTY.ToString(), SAP_TYPE + "-" + SAP_E_ID + "-" + SAP_E_NUM + "-" + SAP_MSG);                        
                 
                        SendMsg(mLogMsgType.Error, "SAP:" + sSAP_MSG);
                        sFlag = false;
                        break;
                    }
                }

            }
        
            SendMsg(mLogMsgType.Incoming, "检查SAP数据完成....");
            return sFlag;
        }

        public  string DictionaryToJson(IDictionary<string, object> Dic)
        {
            return MapListConverter.DictionaryToJson(Dic);
        }

        private void PrintStockInNo_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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

                e.Graphics.DrawString("入库单号:" + StockNo, new Font("宋体", 12, FontStyle.Regular), Brushes.Black, ofLeft + 3 * ofWidth - 8, ofTop * 2 + 10);
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
        /// 列印入库单据
        /// </summary>
        /// <param name="StockIn"></param>
        private void PrintInventoryDocuments(string StockIn)
        {
            bool PrintFlag = true;

            StockNo = StockIn;
            dtStock = OperateDB.GetStockInPrint(StockNo);
            foreach (DataRow dr in dtStock.Rows)
            {
                if (dr[2].ToString() != OperateDB.GetWOLoc(dr["WOID"].ToString()))
                {
                    SendMsg(mLogMsgType.Error, "数据未全部上抛完成,不列印单据");
                    PrintFlag = false;
                    break;
                }
            }
            if ((PrintFlag) && (dtStock.Rows.Count > 0))
                PrintStockInNo.Print();
            dtStock = null;
        }

        private void imbt_RePrint_Click(object sender, EventArgs e)
        {
            string StockIn = InputQuery.ShowInputBox("请输入入库单据", string.Empty).ToUpper();
            if (!string.IsNullOrEmpty(StockIn))
            {
                if (StockIn != "NA")
                {
                    PrintInventoryDocuments(StockIn);
                }
            }
            
        }

        private void imbt_UpLoadData_Click(object sender, EventArgs e)
        {
            if (b_findEmp)
            {
                string StockNo = InputQuery.ShowInputBox("请输入入库单据", string.Empty).ToUpper();
                if (!string.IsNullOrEmpty(StockNo) && StockNo.Trim() != "NA")
                {
                    LabError.Text = string.Empty;
                    SendMsg(mLogMsgType.Normal, "执行过站...");
                    Call_TEST_STOCKIN(StockNo);
                    if (!string.IsNullOrEmpty(LabError.Text))
                    {
                        SendMsg(mLogMsgType.Error, "Call_TEST_STOCKIN Error");
                        return;
                    }
                    SendMsg(mLogMsgType.Normal, "更改WIP STATION");
                    UPDATE_WIPSTATION(StockNo);
                    SendMsg(mLogMsgType.Normal, "写入数据到仓库...");
                    Insert_Z_WIP_TRACKING(StockNo);
                    ClearData();
                    SendMsg(mLogMsgType.Normal, "数据上传完成....");
                    PrintInventoryDocuments(StockNo);
                
                }
            }
            else
            {
                SendMsg(mLogMsgType.Error, "请输入权限...");
            }


        }

        private void Frm_StockingIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否要关闭程序?", "关闭提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)            
                e.Cancel = false;            
            else
                e.Cancel = true;
        }

        private void imbt_SetConfig_Click(object sender, EventArgs e)
        {

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
    }

     
}
