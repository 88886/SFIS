using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using LabelManager2;
using System.Diagnostics;

namespace Frm_TEST_INPUT
{
    public partial class Frm_TEST_INPUT : Office2007Form //Form
    {
        public Frm_TEST_INPUT()
        {
            InitializeComponent();
        }
        WebServices.tWoInfo.tWoInfo tWoInfo = null;   
        WebServices.tPublicStoredproc.tPublicStoredproc PubStor = null;
        WebServices.tLineInfo.tLineInfo sLine = null;
        WebServices.Check_Version.Check_Version chkver = null;
        WebServices.tWipKeyPart.tWipKeyPart wipkeyparts = null;
        string Emp = string.Empty;
        string LabDir = string.Empty;
        private string appfilename = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
        //  Buzzer.buzzer bzz = null;
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };
        public void SendMsg(mLogMsgType msgtype, string msg)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = mLogMsgTypeColor[(int)msgtype]; ;
                    rtbmsg.AppendText(msg + "\n");
                    rtbmsg.ScrollToCaret();
                }));
            }
            catch
            {
            }
        }
        private delegate void InitializationPrg();
        InitializationPrg Initialization;
        private void InitializationInfo()
        {
            string C_RES = "";
            try
            {
                this.Invoke(new EventHandler(delegate
                {
                SendMsg(mLogMsgType.Incoming, "正在加载基础数据!");               

                C_RES = "加载串口DLL失败";
                //  bzz = new Buzzer.buzzer();
                C_RES = "连接串口失败";
                //  bzz.ConnPort("SFIS_ISCM");
                C_RES = "工单类加载失败";
                tWoInfo = new WebServices.tWoInfo.tWoInfo();       
                C_RES = "公共方法PubStor加载失败";
                PubStor = new WebServices.tPublicStoredproc.tPublicStoredproc();
                C_RES = "绑定关系接口加载失败";
                wipkeyparts = new WebServices.tWipKeyPart.tWipKeyPart();
                C_RES = "线体信息加载失败";
                sLine = new WebServices.tLineInfo.tLineInfo();
              
                DataTable dt = ReleaseData.arrByteToDataTable(sLine.GetAllLineInfo());
                DataView dv = dt.DefaultView;
                dv.Sort = "线别 ASC";
                DataTable dTemp = dv.ToTable();
              
                    cb_Line.Items.Clear();
                    foreach (DataRow dr in dTemp.Rows)
                    {
                        cb_Line.Items.Add(dr["线别"].ToString());
                    }
              
                cb_Line.SelectedIndex = 0;
                C_RES = "加载SFIS_ISCM.ini失败";
                string filePath = System.Environment.CurrentDirectory + "\\SFIS_ISCM.ini";
                LabDir = ReadIniFile.IniReadValue("TEST_INPUT", "Patch", filePath);

                LabInput.Text = "";
                LabTarget.Text = "";
                LabRoute.Text = "";
                tb_Input.Focus();
                SendMsg(mLogMsgType.Incoming, "基础数据加载完成!");
                }));
            }
            catch
            {
                SendMsg(mLogMsgType.Error, C_RES);
            }
        }
        private void Frm_TEST_INPUT_Load(object sender, EventArgs e)
        {
            chkver = new WebServices.Check_Version.Check_Version();         
            string _StrErr = string.Empty;
            if (!chkver.CheckPrgVsersion("TEST_INPUT", this.ProductVersion, null, null, null, out _StrErr))               
                {
                    if (_StrErr == "OK")
                    {
                        MessageBox.Show("该程序为版本不是最新版\r\n请更新后运行");
                        this.Enabled = false;
                        RunFile(System.Windows.Forms.Application.StartupPath + @"\", "AutoUpdate.exe", appfilename);
                    }
                    else                    
                        MessageBox.Show("检查版本发生异常:" + _StrErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);                  
                string FileName = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
                Process[] prc = Process.GetProcessesByName(FileName.Substring(0, FileName.LastIndexOf('.')));
                if (prc.Length > 0)
                    foreach (Process pc in prc)
                    {
                        pc.Kill();
                    }
                return;
            }


            this.Text = "PHICOMM TEST_INPUT "+this.ProductVersion;
            //System.DateTime d1 = System.DateTime.Parse("2015/07/30");
            //System.DateTime d2 = System.DateTime.Now;
            //System.TimeSpan ts = d1 - d2;
            //if (ts.Days <= 0)
            //{
            //    MessageBox.Show("试用时间超出,请联系系统开发人员");
            //    this.Close();
            //    return;
            //}

            Initialization = new InitializationPrg(InitializationInfo);
            Initialization.BeginInvoke(null, null);
            tb_Input.Focus();
        }
        private void RunFile(string dir, string localFileName, string thisappname)
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
        private void tb_wo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_wo.Text) && e.KeyCode == Keys.Enter)
            {
                DataTable dt = ReleaseData.arrByteToDataTable(tWoInfo.GetWoInfoByWo(tb_wo.Text));
                if (dt.Rows.Count > 0)
                {
                    LabTarget.Text = dt.Rows[0]["qty"].ToString();
                    LabInput.Text = dt.Rows[0]["inputqty"].ToString();
                    LabRoute.Text = dt.Rows[0]["inputgroup"].ToString();
                    SendMsg(mLogMsgType.Outgoing, "工单信息OK");
                    tb_Input.Focus();
                }
                else
                {
                    SendMsg(mLogMsgType.Error, "工单不存在或输入错误");
                }

                tb_wo.SelectAll();

            }
        }

        private void tb_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_Input.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {                 
                 
                    if (string.IsNullOrEmpty(Emp))
                    {
                        tb_wo.Enabled = false;
                        cb_Line.Enabled = false;

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
                            cb_Line.Enabled = true;
                            LabInput.Text = "";
                            LabTarget.Text = "";
                            LabRoute.Text = "";

                            SendMsg(mLogMsgType.Outgoing, "UNDO OK");
                            SendMsg(mLogMsgType.Normal, "EMP ?");
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(tb_wo.Text) && !string.IsNullOrEmpty(LabRoute.Text))
                            {

                                if (ChkReprint.Checked)
                                {
                                    PrintLabel(tb_Input.Text.Trim());
                                    ChkReprint.Checked = false;
                                }
                                else
                                {
                                    SP_TEST_INPUT_ALL(tb_Input.Text.Trim(), cb_Line.Text, LabRoute.Text, tb_wo.Text, Emp);
                                }
                            }
                            else
                            {
                                SendMsg(mLogMsgType.Error, "工单或途程为空");
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    SendMsg(mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    tb_Input.Text = "";
                }
            }
        }
        private bool CheckEmp(string EmpLoyee)
        {
             
            //List<WebServices.tPublicStoredproc.ProcedureKey> LsPdk = new List<WebServices.tPublicStoredproc.ProcedureKey>();
            //WebServices.tPublicStoredproc.ProcedureKey Pdk = new WebServices.tPublicStoredproc.ProcedureKey();
            //Pdk.Variable = "DATA";
            //Pdk.Value = EmpLoyee;
            //LsPdk.Add(Pdk);
            
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", EmpLoyee);
            string dicstring = Newtonsoft.Json.JsonConvert.SerializeObject(dic);
            string RES = PubStor.ExecuteProcedure("PRO_CHECKEMP", dicstring);

            if (RES == "OK")
            {
                SendMsg(mLogMsgType.Incoming, "EMP " + RES);
                return true;
            }
            else
            {
                SendMsg(mLogMsgType.Error, RES);
                //  SendBuzz();
                return false;
            }

        }

        private bool SP_TEST_INPUT_ALL(string DATA, string LINE, string MYGROUP, string WO, string EMP)
        {

            //List<WebServices.tPublicStoredproc.ProcedureKey> LsPdk = new List<WebServices.tPublicStoredproc.ProcedureKey>();
            //WebServices.tPublicStoredproc.ProcedureKey Pdk = new WebServices.tPublicStoredproc.ProcedureKey();
            //Pdk.Variable = "DATA";
            //Pdk.Value = DATA;
            //LsPdk.Add(Pdk);

            //Pdk = new WebServices.tPublicStoredproc.ProcedureKey();
            //Pdk.Variable = "LINE";
            //Pdk.Value = LINE;
            //LsPdk.Add(Pdk);

            //Pdk = new WebServices.tPublicStoredproc.ProcedureKey();
            //Pdk.Variable = "MYGROUP";
            //Pdk.Value = MYGROUP;
            //LsPdk.Add(Pdk);

            //Pdk = new WebServices.tPublicStoredproc.ProcedureKey();
            //Pdk.Variable = "WO";
            //Pdk.Value = WO;
            //LsPdk.Add(Pdk);

            //Pdk = new WebServices.tPublicStoredproc.ProcedureKey();
            //Pdk.Variable = "EMP";
            //Pdk.Value = EMP;
            //LsPdk.Add(Pdk);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", DATA);
            dic.Add("LINE", LINE);
            dic.Add("MYGROUP", MYGROUP);
            dic.Add("WO", WO);
            dic.Add("EMP", EMP);
            string dicstring = Newtonsoft.Json.JsonConvert.SerializeObject(dic);

            string RES = PubStor.ExecuteProcedure("PRO_TEST_INPUT_ALL", dicstring);

            if (RES == "OK")
            {
                Calculation_MacPassword(DATA);

                LabInput.Text = (Convert.ToInt32(LabInput.Text) + 1).ToString();
                LabInput.Update();
                SendMsg(mLogMsgType.Incoming, "SN " + RES);
                PrintLabel(DATA.Trim());
                return true;
            }
            else
            {
                SendMsg(mLogMsgType.Error, RES);
                SendBuzz();
                return false;
            }
        }

        private void PrintLabel(string ESN)
        {
            WebServices.tWipKeyPart.tWipKeyPart wipkeyparts = new WebServices.tWipKeyPart.tWipKeyPart();
            DataTable dt_wipkeyparts = ReleaseData.arrByteToDataTable(wipkeyparts.GetWipKeyPart(ESN));

            string labfilefullpath = string.Format(@"{0}\{1}\{2}", this.LabDir.IndexOf(":") != -1 ? this.LabDir : this.LabDir + ":", this.tb_wo.Text, LabRoute.Text + ".lab");
            if (!File.Exists(labfilefullpath))
            {
                SendMsg(mLogMsgType.Error, "条码文件不存在:" + labfilefullpath);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Colnum", typeof(string));
                dt.Columns.Add("DATA", typeof(string));
                dt.Rows.Add("ESN", ESN);
                foreach (DataRow dr in dt_wipkeyparts.Rows)
                {
                    dt.Rows.Add(dr["SNTYPE"].ToString(), dr["SNVAL"].ToString());
                }
                Call_PrintLabel(dt, labfilefullpath, Convert.ToInt32(numprint.Value));
            }
        }
        private void SendBuzz()
        {
           // bzz.SendMsg("F");
        }

        private void ChkReprint_Click(object sender, EventArgs e)
        {
            tb_Input.Focus();
        }

        private void Calculation_MacPassword(string ESN)
        {

            Dictionary<string, string> dicpwd = new Dictionary<string, string>();
            List<string> _Allpwd = new List<string>();
            if (radNoPWD.Checked)
            {
            }
            else
            {
                DataTable dt_wipkeyparts = ReleaseData.arrByteToDataTable(wipkeyparts.GetWipKeyPart(ESN));
                if (dt_wipkeyparts.Rows.Count > 0)
                {
                    string MAC = string.Empty;
                    string woId = string.Empty;
                    foreach (DataRow dr in dt_wipkeyparts.Rows)
                    {
                        if (dr["SNTYPE"].ToString() == "MAC")
                        {
                            MAC = dr["SNVAL"].ToString();
                            woId = dr["WOID"].ToString();
                        }
                    }
                    if (!string.IsNullOrEmpty(MAC))
                    {
                        if (radMAC.Checked)
                        {
                            _Allpwd = macPassword.getMacAllPassword(MAC);
                            dicpwd.Add("DEK", _Allpwd[3]);
                        }
                        if (radMAC2.Checked)
                        {
                            string Mac_Prefix = MAC.Substring(0, 6);
                            int i = Convert.ToInt32(MAC.Substring(6,6), 16)+2;
                            string Mac_Suffix = string.Format("{0:X}", i);

                            SendMsg(mLogMsgType.Outgoing, "MAC:" + MAC);
                            SendMsg(mLogMsgType.Outgoing, "PLCMAC:" + Mac_Prefix + Mac_Suffix);

                            _Allpwd = macPassword.getMacAllPassword(Mac_Prefix + Mac_Suffix);
                            dicpwd.Add("DEK", _Allpwd[3]);
                            dicpwd.Add("PLCMAC", Mac_Prefix + Mac_Suffix);
                        }

                        List<Dictionary<string, object>> LsDic = new List<Dictionary<string, object>>();
                        Dictionary<string, object> dic = null;
                            
                        //List<WebServices.tWipTracking.tWipKeyPartTable> Listwip = new List<WebServices.tWipTracking.tWipKeyPartTable>();
                        //WebServices.tWipTracking.tWipKeyPartTable twip = null;
                        foreach (KeyValuePair<string, string> item in dicpwd)
                        {
                            dic = new Dictionary<string, object>();
                            dic.Add("ESN",MAC);
                            dic.Add("WOID", woId);
                            dic.Add("SNTYPE", item.Key);
                            dic.Add("SNVAL", item.Value);
                            dic.Add("STATION", LabRoute.Text);
                            dic.Add("KPNO", "NA");
                            LsDic.Add(dic);
                            //twip = new WebServices.tWipTracking.tWipKeyPartTable();
                            //twip.esn = MAC;
                            //twip.woId = woId;
                            //twip.sntype = item.Key;
                            //twip.snval = item.Value;
                            //twip.station = LabRoute.Text;
                            //twip.KPNO = "NA";
                            //Listwip.Add(twip);
                        }
                        WebServices.tWipTracking.tWipTracking mTracking = new WebServices.tWipTracking.tWipTracking();
                        string _StrErr = mTracking.InsertWipKeyParts(Newtonsoft.Json.JsonConvert.SerializeObject(LsDic));
                        if (_StrErr == "OK")
                            SendMsg(mLogMsgType.Incoming, "Inser KeyParts OK");
                        else
                            SendMsg(mLogMsgType.Error, "Inser KeyParts Err:" + _StrErr);
                    }
                }
            }
        }

        private void Frm_TEST_INPUT_FormClosing(object sender, FormClosingEventArgs e)
        {
           // sPrint.ExitCodeSoft();
           // bzz.ClosePort();
        }

        public void Call_PrintLabel(DataTable dt, string filepatch,int PrintQTY)
        {
           // StripStatusLabelPatch.Text = "Label File: " + filepatch;

           // string PrintQty = OperateIni.IniReadValue("PACK_CTN", "LabelQty", IniFilePath);
          //  string coordinateX = OperateIni.IniReadValue("PACK_CTN", "LabelX", IniFilePath);
           // string coordinateY = OperateIni.IniReadValue("PACK_CTN", "LabelY", IniFilePath);

            if (!File.Exists(filepatch))  //判断条码文件是否存在
            {
                SendMsg(mLogMsgType.Error, "条码档没有找到,路径:" + filepatch);
                return;
            }
            ApplicationClass lbl = new ApplicationClass();
            try
            {

                lbl.Documents.Open(filepatch, false);// 调用设计好的label文件
                Document doc = lbl.ActiveDocument;
                SendMsg(mLogMsgType.Incoming,  "清空模板变量...");
                for (int i = 0; i < doc.Variables.FormVariables.Count; i++)
                {
                    doc.Variables.FormVariables.Item(doc.Variables.FormVariables.Item(i + 1).Name).Value = "";
                }
                SendMsg(mLogMsgType.Incoming, string.Format("模板变量清空完成,共计{0}个...", doc.Variables.FormVariables.Count));
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        doc.Variables.FormVariables.Item(dr[0].ToString()).Value = dr[1].ToString(); //给参数传值
                        SendMsg(mLogMsgType.Outgoing, string.Format("填充打印变量完成:{0}->{1}", dr[0].ToString(), dr[1].ToString()));
                    }
                    catch
                    {
                    }
                }

                int Num = PrintQTY;        //打印数量
               // doc.Format.MarginLeft = (Convert.ToInt32(coordinateX)) * 100;
               // doc.Format.MarginTop = (Convert.ToInt32(coordinateY)) * 100;
                doc.PrintDocument(Num);               //打印
                SendMsg(mLogMsgType.Normal, "打印完成");
            }
            catch (Exception ex)
            {
                SendMsg(mLogMsgType.Error, "发生异常" + ex.Message);
            }
            finally
            {
                lbl.Quit(); //退出
            }

        }
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

