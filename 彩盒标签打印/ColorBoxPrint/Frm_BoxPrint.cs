using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using WebServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Runtime.InteropServices;
using GenericUtil;
using LabelManager2;

namespace ColorBoxPrint
{
    public partial class Frm_BoxPrint : Office2007Form //Form
    {
        public Frm_BoxPrint()
        {
            InitializeComponent();
        }

        string _FwVer = string.Empty;

        /// <summary>
        /// C:\\SFIS\\SFIS.ini
        /// </summary>
        string IniFilePath = "C:\\SFIS\\SFIS.ini";

        string ProductLine = string.Empty;

        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };
        private void SendMsg(mLogMsgType msgtype, string Msg)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = mLogMsgTypeColor[(int)msgtype];
                    rtbmsg.AppendText( Msg + "\n");
                    rtbmsg.ScrollToCaret();
                }));
            }
            catch
            {
            }
        }

        /// <summary>
        /// 自动运行指定的程序
        /// </summary>
        /// <param name="dir">所在路径</param>
        /// <param name="localFileName">程序名称</param>
        /// <param name="thisappname"></param>
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
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":Application Run Error");
            }
        }
        private void Frm_BoxPrint_Load(object sender, EventArgs e)
        {
            if (!ChkSoftInstall("CODESOFT 7"))
            {
                MessageBoxEx.Show("本机没有安装Codesoft 7 不能使用");
                this.Enabled = false;
                return;
            }

            if (!Directory.Exists("C:\\SFIS"))
                Directory.CreateDirectory("C:\\SFIS");

            FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath);           
            this.groupBox1.Text+= string.Format("(Version: {0})",myFileVersion.FileVersion);
            string _StrErr = string.Empty;
            chkver = new WebServices.Check_Version.Check_Version();
            if (!chkver.CheckPrgVsersion("BOX_PRINT", this.ProductVersion, null, null, null, out _StrErr))
            {
                if (_StrErr == "OK")
                {
                    SendMsg(mLogMsgType.Error, "该程序为版本不是最新版\r\n请更新后运行");
                    RunFile(System.Windows.Forms.Application.StartupPath + @"\", "AutoUpdate.exe", "ISCM.exe");
                }
                else
                    SendMsg(mLogMsgType.Error, "检查版本失败:" + _StrErr);
                this.Enabled = false;
            }
        
            IniPrg = new IntPrgConfig(IniPrgConfigInfo);
            IniPrg.BeginInvoke(null,null);
        
            tb_Input.Enabled = false;
            tb_wo.Focus();
            string filePath = System.Environment.CurrentDirectory + "\\SFIS_ISCM.ini";
            LabDir = ReadIniFile.IniReadValue("BoxPrint", "Patch", filePath);
            NumX.Value = Convert.ToInt16(ReadIniFile.IniReadValue("BoxPrint", "X", filePath));
            NumY.Value = Convert.ToInt16(ReadIniFile.IniReadValue("BoxPrint", "Y", filePath));

            LabLine.Text = string.Empty;
            LabLine.Text = Encoder.Encoder.DecryptString(Encoder.ReadIniFile.IniReadValue("BOX_PRINT", "LINE", IniFilePath));  
            
        }
        WebServices.tWipKeyPart.tWipKeyPart wkp = new WebServices.tWipKeyPart.tWipKeyPart();
        WebServices.tWoInfo.tWoInfo swoinfo = new WebServices.tWoInfo.tWoInfo();
        WebServices.tProduct.tProduct sProduct = new WebServices.tProduct.tProduct();
        WebServices.Check_Version.Check_Version chkver = new WebServices.Check_Version.Check_Version();
        WebServices.tLineInfo.tLineInfo LsLine = new WebServices.tLineInfo.tLineInfo();
        WebServices.tPublicStoredproc.tPublicStoredproc PubStor = new WebServices.tPublicStoredproc.tPublicStoredproc();
        public WebServices.tWipTracking.tWipTracking tWipTrack = new WebServices.tWipTracking.tWipTracking();
        WebServices.tUserInfo.tUserInfo mUserInfo = new WebServices.tUserInfo.tUserInfo(); 
        string sPartNumber = "";
        string sPartColor = "";
        string LabDir = string.Empty;
        string StrEmp = string.Empty;
        string StrEC = string.Empty;
        string StrSN = string.Empty;
        private void tb_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tb_Input.Text)) && (e.KeyCode == Keys.Enter))
            {
                try
                {
                    string sESN = "";
                    string InputStr = tb_Input.Text.Trim();
                    if (InputStr == "UNDO")
                    {
                        StrEmp = string.Empty;
                        StrEC = string.Empty;
                        StrSN = string.Empty;
                        SendMsg(mLogMsgType.Incoming, "UNDO OK");
                    }
                    else
                    {

                        if (string.IsNullOrEmpty(StrEmp))
                        {
                            if (CHECK_EMP(InputStr) == "OK")
                            {
                                StrEmp = InputStr;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                            if (string.IsNullOrEmpty(StrEC))
                            {
                                if (CHECK_EC(InputStr) == "OK")
                                {
                                    StrEC = InputStr;
                                    return;
                                }
                                else
                                {
                                    if (CHECK_SN(InputStr, out sESN) == "OK")
                                    {
                                        if (!CHECK_PRODUCT_LINE())
                                            return;
                                        if (PRO_TEST_MAIN_ONLY(sESN, cb_ListRoute.Text, StrEmp, StrEC, LabLine.Text) == "OK")
                                        {
                                            PrintLabel(sESN);
                                            StrEC = string.Empty;
                                            StrSN = string.Empty;
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(StrSN))
                                {
                                    if (CHECK_SN(InputStr, out sESN) == "OK")
                                    {
                                        if (PRO_TEST_MAIN_ONLY(sESN, cb_ListRoute.Text, StrEmp, StrEC, LabLine.Text) == "OK")
                                        {
                                            PrintLabel(sESN);
                                            StrEC = string.Empty;
                                            StrSN = string.Empty;
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }

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


        private void Frm_BoxPrint_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void tb_wo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tb_wo.Text)) && (e.KeyCode == Keys.Enter))
            {
                try
                {
                    DataTable dtwo = ReleaseData.arrByteToDataTable(swoinfo.GetWoInfo(tb_wo.Text,null));
                    ProductLine = dtwo.Rows[0]["LINEID"].ToString();
                   SendMsg(mLogMsgType.Incoming, string.Format("此工单可在[{0}]线生产", ProductLine));
                    sPartNumber = dtwo.Rows[0][5].ToString();                
                    sPartColor = ReleaseData.arrByteToDataTable(sProduct.GetProductByPartNumber(sPartNumber)).Rows[0][3].ToString();
                    LabColor.Text = string.Format("颜色:[{0}]  料号:[{1}]",sPartColor,sPartNumber);
                    _FwVer = dtwo.Rows[0]["FW_VER"].ToString();
                    SendMsg(mLogMsgType.Incoming, "获取工单信息成功");
                    tb_Input.Enabled = true;
                    tb_wo.Enabled = false;
                    tb_Input.Focus();
                    cb_ListRoute.Items.Clear();
                    DataTable dtcraft = ReleaseData.arrByteToDataTable(swoinfo.GetAllCraftInfo(tb_wo.Text));
                    foreach (DataRow dr in dtcraft.Rows)
                    {
                        cb_ListRoute.Items.Add(dr["CRAFTNAME"].ToString());
                    }
                }
                catch
                {
                    SendMsg(mLogMsgType.Error, string.Format("获取{0}工单,产品颜色失败", tb_wo.Text));
                    tb_Input.Enabled = false;
                    tb_wo.Text = "";
                }
             
            }
        }

        private void imbt_chgwo_Click(object sender, EventArgs e)
        {
            tb_wo.Enabled = true;
            tb_Input.Enabled = false;
            tb_wo.Focus();
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

        private delegate void IntPrgConfig();
        IntPrgConfig IniPrg;

        private void IniPrgConfigInfo()
        {
            //this.Invoke(new EventHandler(delegate
            //{
            //    sPL.ConnCodeSoft();
            //    sPL.RichTextBoxMsg(rtbmsg);                 
            //      //cb_lineList.Items.Clear();

            //      //DataTable dtLine = ReleaseData.arrByteToDataTable(LsLine.GetAllLineInfo());            
            //      //dtLine.DefaultView.Sort = "线别 ASC";
            //      //DataTable dt = dtLine.DefaultView.ToTable();          
            //      //foreach (DataRow dr in dt.Rows)
            //      //{
            //      //    cb_lineList.Items.Add(dr["线别"].ToString());
            //      //}

            //  }));

            //sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Normal, "初始化成功");
        }

        private string CHECK_EC(string DATA)
        {
            //WebServices.tPublicStoredproc.ProcedureKey Pdk = new WebServices.tPublicStoredproc.ProcedureKey();
            //List<WebServices.tPublicStoredproc.ProcedureKey> LsPdk = new List<WebServices.tPublicStoredproc.ProcedureKey>();
            //Pdk.Variable = "DATA";
            //Pdk.Value = DATA;
            //LsPdk.Add(Pdk);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", DATA);           
            string ChkRes = PubStor.ExecuteProcedure("PRO_CHECKEC",  MapListConverter.DictionaryToJson(dic));
            SendMsg(ChkRes == "OK" ? mLogMsgType.Normal : mLogMsgType.Error, string.Format("EC: {0}", ChkRes));
            return ChkRes;
        }

        private string CHECK_EMP(string DATA)
        {            
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", DATA);
            string ChkRes = PubStor.ExecuteProcedure("PRO_CHECKEMP", MapListConverter.DictionaryToJson(dic));
            SendMsg(ChkRes == "OK" ? mLogMsgType.Normal : mLogMsgType.Error, string.Format("EMP: {0}", ChkRes));
            return ChkRes;
        }

        private string PRO_TEST_MAIN_ONLY(string DATA,string MYGROUP,string EMP, string EC,string LINE)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", DATA);
            dic.Add("MYGROUP", MYGROUP);
            dic.Add("EMP", EMP);
            dic.Add("EC", string.IsNullOrEmpty(EC) ? "NA" : EC);
            dic.Add("LINE", LINE);

            string ChkRes = PubStor.ExecuteProcedure("PRO_TEST_MAIN_ONLY", MapListConverter.DictionaryToJson(dic));
            SendMsg(ChkRes == "OK" ? mLogMsgType.Normal : mLogMsgType.Error, string.Format("SN: {0}", ChkRes));
            return ChkRes;
        }
        private string CHECK_SN(string DATA,out string ESN)
        {
            string sColnum=string.Empty;
            string StrErr = string.Empty;
            ESN = string.Empty;
            if (rdesn.Checked)
                sColnum="ESN";
            if (rdIMEI.Checked)
                sColnum="IMEI";

           DataTable dt=ReleaseData.arrByteToDataTable(tWipTrack.GetQueryWipAllInfo(sColnum,DATA));
           if (dt.Rows.Count > 0)
           {
               if (tb_wo.Text == dt.Rows[0]["WOID"].ToString())
               {

                   if (dt.Rows[0]["ERRFLAG"].ToString() != "0")
                   {
                       StrErr = "SN IN REPAIR";
                   }
                   else
                   {
                       if (dt.Rows[0]["SCRAPFLAG"].ToString() != "0")
                       {
                           StrErr = "SN HAS SCRAP";
                       }
                       else
                       {
                           StrErr = "OK";
                           ESN = dt.Rows[0]["ESN"].ToString();
                       }

                   }
               }
               else
               {
                   StrErr = "WO Different";
               }
           }
           else
           {
               StrErr = "NO SN";
           }

           SendMsg(StrErr == "OK" ? mLogMsgType.Normal : mLogMsgType.Error, string.Format("SN : {0}", StrErr));
           return StrErr;
        }

        public void PrintLabel(string sEsn)
        {
            #region 打印标签
            DataTable dt = ReleaseData.arrByteToDataTable(wkp.GetWipKeyPart(sEsn));
            if (dt.Rows.Count > 0)
            {
                string sWO = dt.Rows[0][1].ToString();
                if (sWO != tb_wo.Text)
                {
                    SendMsg(mLogMsgType.Error, "工单不同-->" + sWO);
                    return;
                }

                DataTable dtPrint = new DataTable();
                dtPrint.Columns.Add("Name", Type.GetType("System.String"));
                dtPrint.Columns.Add("val", Type.GetType("System.String"));
                dtPrint.Rows.Add("ESN", sEsn);
                dtPrint.Rows.Add("BLACK", sPartColor);
                dtPrint.Rows.Add("PARTNUMBER", sPartNumber);
                dtPrint.Rows.Add("FWVER", string.IsNullOrEmpty(_FwVer) ? "NA" : _FwVer);
                foreach (DataRow dr in dt.Rows)
                {
                    dtPrint.Rows.Add(dr[2].ToString().ToUpper(), dr[3].ToString().ToUpper());
                }
                //string filepatch = System.IO.Directory.GetCurrentDirectory() + "\\LabelFile\\" + sPartNumber + ".lab";
                //string labfilefullpath = string.Format(@"{0}\{1}\{2}", this.LabDir.IndexOf(":") != -1 ? this.LabDir : this.LabDir, "PACK_BOX", sPartNumber + ".lab");
                //if (!File.Exists(labfilefullpath))
                //{
                //    SendMsg(mLogMsgType.Error, "条码文件不存在:" + labfilefullpath);
                //}
                //else
                //{
                //    SendPrintLabel(dtPrint, labfilefullpath, 1);
                //}
                PrintLabel(dtPrint);
            }
            else
            {
                SendMsg(mLogMsgType.Error, "没有找到信息");
            }
            #endregion
        }

        public void PrintLabel(DataTable dt)
        {
            string filepatch = string.Format(@"{0}\{1}\{2}", this.LabDir.IndexOf(":") != -1 ? this.LabDir : this.LabDir, "PACK_BOX", sPartNumber + ".lab");

         
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

                int Num = 1;        //打印数量
                doc.Format.MarginLeft = (Convert.ToInt32(NumX.Value)) * 100;
                doc.Format.MarginTop = (Convert.ToInt32(NumY.Value)) * 100;
                doc.CopyToClipboard();
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

        private void imbt_RePrint_Click(object sender, EventArgs e)
        {
            Frm_RePrint fre = new Frm_RePrint(this);
            fre.ShowDialog();
          
        }

        private void NumX_Leave(object sender, EventArgs e)
        {
             string filePath = System.Environment.CurrentDirectory + "\\SFIS_ISCM.ini";     
             ReadIniFile.IniWriteValue("BoxPrint", "X", NumX.Value.ToString(), filePath);
   
           
        }

        private void NumY_Leave(object sender, EventArgs e)
        {
            string filePath = System.Environment.CurrentDirectory + "\\SFIS_ISCM.ini";
            ReadIniFile.IniWriteValue("BoxPrint", "Y", NumY.Value.ToString(), filePath);
           
        }

        private void imbt_SelectLine_Click(object sender, EventArgs e)
        {
            string[] EmpData = Input.InputBox.ShowInputBox("输入权限", string.Empty);
            try
            {
                string UserId = EmpData[0];
                string PWD = EmpData[1];
                if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(PWD))
                {
                    string _StrErr = mUserInfo.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);
                    if (_StrErr == "OK")
                    {
                        SendMsg(mLogMsgType.Incoming , "权限正确");
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        Frm_Public.Frm_Select fd = new Frm_Public.Frm_Select(ReleaseData.arrByteToDataTable(LsLine.GetAllLineInfo()), ref dic);
                        if (fd.ShowDialog() == DialogResult.OK)
                        {
                            LabLine.Text = dic["线别"].ToString();
                            Encoder.ReadIniFile.IniWriteValue("BOX_PRINT", "LINE", Encoder.Encoder.EncryptString(dic["线别"].ToString()), IniFilePath);
                        }

                    }
                    else
                    {
                        SendMsg(mLogMsgType.Error, _StrErr);

                    }
                }

            }
            catch (Exception ex)
            {
                SendMsg(mLogMsgType.Error, "权限格式不正确:"+ex.Message);
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
                SendMsg(mLogMsgType.Error, string.Format("此工单不可在{0}生产", LabLine.Text));
            return flag;
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


    #region  解压缩信息
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

    #endregion
}
