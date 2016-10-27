using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Collections.Specialized;


namespace TEST_INPUT
{
    public partial class Frm_Input : Office2007Form //Form
    {
        public Frm_Input()
        {
            InitializeComponent();
        }
        WebServices.tWoInfo.tWoInfo tWoInfo = null;
        PrintDLL.PrintLabel sPrint = new PrintDLL.PrintLabel();
        WebServices.tPublicStoredproc.tPublicStoredproc PubStor = null;
        WebServices.tLineInfo.tLineInfo sLine = null;
        WebServices.Check_Version.Check_Version chkver = null;
        WebServices.tUserInfo.tUserInfo userinfo = null;
        string Emp = string.Empty;
        string LabDir = string.Empty;   
        private string appfilename = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
        Buzzer.buzzer bzz = null;
        string ProductLine = string.Empty;
        /// <summary>
        /// C:\\SFIS\\SFIS.ini
        /// </summary>
        string IniFilePath = "C:\\SFIS\\SFIS.ini";

        private void Frm_Input_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("C:\\SFIS"))
                Directory.CreateDirectory("C:\\SFIS");
            string _StrErr = string.Empty;
            chkver = new WebServices.Check_Version.Check_Version();
            if (!chkver.CheckPrgVsersion("TEST_INPUT", this.ProductVersion, null, null, null, out _StrErr))
            {
                if (_StrErr == "OK")
                {
                    SendPrgMsg(Color.Red, "该程序为版本不是最新版\r\n请更新后运行");
                    RunFile(System.Windows.Forms.Application.StartupPath + @"\", "AutoUpdate.exe", "ISCM.exe");
                }
                else
                    SendPrgMsg(Color.Red, "检查版本失败:" + _StrErr);
                this.Enabled = false;
            }
       
                Initialization = new InitializationPrg(InitializationInfo);
                Initialization.BeginInvoke(null, null);
           
        }

        private void tb_wo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_wo.Text) && e.KeyCode == Keys.Enter)
            {
              DataTable dt=  ReleaseData.arrByteToDataTable( tWoInfo.GetWoInfoByWo(tb_wo.Text));
              if (dt.Rows.Count > 0)
              {
                  LabTarget.Text = dt.Rows[0]["qty"].ToString();
                  LabInput.Text = dt.Rows[0]["inputqty"].ToString();
                  LabRoute.Text = dt.Rows[0]["inputgroup"].ToString();
                  ProductLine = dt.Rows[0]["LINEID"].ToString();
                  SendPrgMsg(Color.Green, string.Format("此工单可在[{0}]线生产", ProductLine));
                  sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Outgoing, "工单信息OK");

              }
              else
              {
                  sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, "工单不存在或输入错误");
              }

              tb_wo.SelectAll();

            }

        }

        #region  解压缩和读取INI
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

        public class ReadIniFile
        {
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
        }
        #endregion

        
        private void tb_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_Input.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (string.IsNullOrEmpty(Emp))
                    {
                        tb_wo.Enabled = false;
                        toolSelectLine.Visible = false;
                       
                        if (CheckEmp(tb_Input.Text.Trim()))
                        {
                            Emp = tb_Input.Text.Trim();
                        }
                        else
                        {
                            Emp = string.Empty;
                        }
                    }
                    else
                    {
                        if (tb_Input.Text.Trim() == "UNDO")
                        {
                            Emp = string.Empty;
                            tb_wo.Enabled = true;                         
                            LabInput.Text = "";
                            LabTarget.Text = "";
                            LabRoute.Text = "";
                            toolSelectLine.Visible = true;
                            sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Outgoing, "UNDO OK");
                            sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Normal, "EMP ?");
                        }
                        else
                        {
                            if (!CHECK_PRODUCT_LINE())
                                return;
                            if (ChkReprint.Checked)
                            {
                                PrintLabel(tb_Input.Text.Trim());
                                ChkReprint.Checked = false;
                            }
                            else
                            {
                                SP_TEST_INPUT_ALL(tb_Input.Text.Trim(), LabLine.Text, LabRoute.Text, tb_wo.Text.Trim(), Emp);
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    tb_Input.Text = "";
                }
            }
        }
        private bool CheckEmp(string EmpLoyee)
        {
            //DataTable dt = new DataTable("mydt");
            //dt.Columns.Add("Colnum",typeof(string));
            //dt.Columns.Add("DATA", typeof(string));
            //dt.Rows.Add("DATA", EmpLoyee);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", EmpLoyee);                
           string RES= PubStor.ExecuteProcedure("PRO_CHECKEMP",GenericUtil.MapListConverter.DictionaryToJson(dic));
            
           if (RES == "OK")
           {
               sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Incoming, "EMP "+RES);
               return true;
           }
           else
           {
               sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, RES);
               SendBuzz();
               return false;
           }

        }

        private bool SP_TEST_INPUT_ALL(string DATA, string LINE, string MYGROUP, string WO, string EMP)
        {
                  
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", DATA);
            dic.Add("LINE", LINE);
            dic.Add("MYGROUP", MYGROUP);
            dic.Add("WO", WO);
            dic.Add("EMP", EMP); 

            string RES = PubStor.ExecuteProcedure("PRO_TEST_INPUT_ALL", GenericUtil.MapListConverter.DictionaryToJson(dic));

            if (RES == "OK")
            {
                LabInput.Text = (Convert.ToInt32(LabInput.Text) + 1).ToString();
                LabInput.Update();
                sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Incoming, "SN " + RES);
                PrintLabel(DATA.Trim());
                return true;
            }
            else
            {
                sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, RES);
                SendBuzz();
                return false;
            }
        }

        private void PrintLabel(string sMAC)
        {
            string labfilefullpath = string.Format(@"{0}\{1}\{2}", this.LabDir.IndexOf(":") != -1 ? this.LabDir : this.LabDir + ":", this.tb_wo.Text, LabRoute.Text + ".lab");
            if (!File.Exists(labfilefullpath))
            {
                sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, "条码文件不存在:" + labfilefullpath);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Colnum", typeof(string));
                dt.Columns.Add("DATA", typeof(string));
                dt.Rows.Add("MAC", sMAC);
                sPrint.SendPrintLabel(dt, labfilefullpath,Convert.ToInt32(numprint.Value));
            }
        }

        private delegate void InitializationPrg();
        InitializationPrg Initialization;
        private void InitializationInfo()
        {
            string C_RES = "";
            try
            {
                sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Incoming, "正在加载基础数据!");
                sPrint.RichTextBoxMsg(rtbmsg);

                C_RES = "加载串口DLL失败";
                bzz = new Buzzer.buzzer();
                C_RES = "连接串口失败";
                bzz.ConnPort("SFIS_ISCM");
                C_RES = "工单类加载失败";
                tWoInfo = new WebServices.tWoInfo.tWoInfo();
                C_RES = "连接CodeSoft失败";
                sPrint.ConnCodeSoft();
                C_RES = "公共方法PubStor加载失败";
                PubStor = new WebServices.tPublicStoredproc.tPublicStoredproc();
                C_RES = "线体信息加载失败";
                sLine = new WebServices.tLineInfo.tLineInfo();
                C_RES = "用户信息加载失败";
                userinfo= new WebServices.tUserInfo.tUserInfo();
                //DataTable dt = ReleaseData.arrByteToDataTable(sLine.GetAllLineInfo());
                //DataView dv = dt.DefaultView;
                //dv.Sort = "线别 ASC";
                //DataTable dTemp = dv.ToTable();
                //foreach (DataRow dr in dTemp.Rows)
                //{
                //    cb_Line.Items.Add(dr["线别"].ToString());
                //}
                //cb_Line.SelectedIndex = 0;
                string filePath = System.Environment.CurrentDirectory + "\\SFIS_ISCM.ini";
                LabDir = ReadIniFile.IniReadValue("TEST_INPUT", "Patch", filePath);

                LabInput.Text = "";
                LabTarget.Text = "";
                LabRoute.Text = "";
                LabLine.Text = string.Empty;
                LabLine.Text = Encoder.Encoder.DecryptString(Encoder.ReadIniFile.IniReadValue("TEST_INPUT", "LINE", IniFilePath));  
                sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Incoming, "基础数据加载完成!");
            }
            catch
            {
                sPrint.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, C_RES);
            }
        }

        public void SendPrgMsg(Color msgtype, string msg)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = msgtype;
                    rtbmsg.AppendText(msg + "\n");
                    rtbmsg.ScrollToCaret();
                }));
            }
            catch
            {
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

        private void ChkReprint_Click(object sender, EventArgs e)
        {
            tb_Input.Focus();
        }

        private void Frm_Input_FormClosing(object sender, FormClosingEventArgs e)
        {
            sPrint.ExitCodeSoft();
            bzz.ClosePort();
        }
        private void SendBuzz()
        {
            bzz.SendMsg("F");
        }

        private void toolSelectLine_Click(object sender, EventArgs e)
        {
            string[] EmpData = Input.InputBox.ShowInputBox("输入权限", string.Empty);
            try
            {
                string UserId = EmpData[0];
                string PWD = EmpData[1];
                if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(PWD))
                {
                    string _StrErr =userinfo.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);
                    if (_StrErr == "OK")
                    {
                        SendPrgMsg( Color.Green, "权限正确");
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        Frm_Public.Frm_Select fd = new Frm_Public.Frm_Select(ReleaseData.arrByteToDataTable(sLine.GetAllLineInfo()), ref dic);
                        if (fd.ShowDialog() == DialogResult.OK)
                        {
                            LabLine.Text= dic["线别"].ToString();
                            Encoder.ReadIniFile.IniWriteValue("TEST_INPUT", "LINE", Encoder.Encoder.EncryptString(dic["线别"].ToString()), IniFilePath);
                        }

                    }
                    else
                    {
                        SendPrgMsg(Color.Red, _StrErr);

                    }
                }

            }
            catch
            {
                SendPrgMsg(Color.Red, "权限格式不正确");
            }
        }
        private bool CHECK_PRODUCT_LINE()
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
                SendPrgMsg(Color.Red, string.Format("此工单不可在{0}生产", LabLine.Text));
            return flag;
        }
    }
}
