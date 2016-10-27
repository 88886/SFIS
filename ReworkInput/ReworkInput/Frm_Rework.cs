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
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ReworkInput
{
    public partial class Frm_Rework : Office2007Form //Form
    {
        public Frm_Rework()
        {
            InitializeComponent();
        }

        #region 程序基础参数
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };

        public void SendPrgMsg(mLogMsgType msgtype, string msg)
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

        WebServices.tWoInfo.tWoInfo tWoInfo = null;
        WebServices.tLineInfo.tLineInfo tLineInfo = null;
        WebServices.tPublicStoredproc.tPublicStoredproc PubStor = null;
        WebServices.tUserInfo.tUserInfo tuser = null;
        WebServices.Check_Version.Check_Version chkver = null;
        WebServices.tBomKeyPart.tBomKeyPart bkp = null;
        WebServices.tWipTracking.tWipTracking twip = null;
      //  List<WebServices.tWipTracking.tWipKeyPartTable> LsWkp = null;
        /// <summary>
        /// C:\\SFIS\\SFIS.ini
        /// </summary>
        string IniFilePath = "C:\\SFIS\\SFIS.ini";
        string ProductLine = string.Empty;
     
        /// <summary>
        /// 途程代码
        /// </summary>
        string RouteCode = string.Empty;
        string EmpNo = string.Empty;
        string sSQLMAC = string.Empty;
        string sSQLSN = string.Empty;

        private void Frm_Rework_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("C:\\SFIS"))
                Directory.CreateDirectory("C:\\SFIS");

            string FilePatch = System.Environment.CurrentDirectory + "\\SFIS_ISCM.ini";       

            LoadDefaultData();
            string _StrErr = string.Empty;
            if (!chkver.CheckPrgVsersion("ReworkInput", this.ProductVersion, null, null, null, out _StrErr))
            {
                if (_StrErr == "OK")
                {
                    SendPrgMsg(mLogMsgType.Error, "该程序为版本不是最新版\r\n请更新后运行");
                    RunFile(System.Windows.Forms.Application.StartupPath + @"\", "AutoUpdate.exe", "ISCM.exe");
                }
                else
                    SendPrgMsg(mLogMsgType.Error, "检查版本失败:"+_StrErr);

                this.Enabled = false;             
            }

            LabLine.Text = string.Empty;
            LabLine.Text = Encoder.Encoder.DecryptString(Encoder.ReadIniFile.IniReadValue("ReworkInput", "LINE", IniFilePath));  
        
        }
        /// <summary>
        /// 自动运行指定的程序
        /// </summary>
        /// <param name="dir">所在路径</param>
        /// <param name="localFileName">程序名称</param>
        /// <param name="thisappname"></param>
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
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":Application Run Error");
            }
        }

        /// <summary>
        /// 加载基础数据
        /// </summary>
        private void LoadDefaultData()
        {
            tb_wo.Focus();
            string C_RES = "";
            try
            {
                C_RES = "加载 对象[WebServices.tWipTracking] 失败 ";
                twip = new WebServices.tWipTracking.tWipTracking();
                C_RES = "加载 对象[WebServices.tBomKeyPart] 失败 ";
                bkp = new WebServices.tBomKeyPart.tBomKeyPart();
                C_RES = "加载 对象[WebServices.tWoInfo] 失败 ";
                tWoInfo = new WebServices.tWoInfo.tWoInfo();
                C_RES = "加载 对象[WebServices.tLineInfo] 失败 ";
                tLineInfo = new WebServices.tLineInfo.tLineInfo();
                C_RES = "加载 对象[WebServices.tPublicStoredproc] 失败 ";
                PubStor = new WebServices.tPublicStoredproc.tPublicStoredproc();
                C_RES = "加载员工信息失败";
                tuser = new WebServices.tUserInfo.tUserInfo();
                C_RES = "加载检查版本信息失败";
                chkver = new WebServices.Check_Version.Check_Version();
                C_RES = "加载线体资料失败";
                LoadLineInfo();
             
                SendPrgMsg(mLogMsgType.Incoming,"基础数据加载完成");
                SendPrgMsg(mLogMsgType.Incoming, "请选择基础数据后输入权限...");
            }
            catch
            {
                SendPrgMsg(mLogMsgType.Error, C_RES);
                this.Enabled = false;
            }
        }
        private void LoadLineInfo()
        {
            //DataTable dt = ReleaseData.arrByteToDataTable(tLineInfo.GetAllLineInfo());
            //foreach (DataRow dr in dt.Rows)
            //{
            //    cb_Line.Items.Add(dr[0].ToString());
            //}
            //cb_Line.SelectedIndex =0;
        }

        private void tb_wo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tb_wo.Text)) && (e.KeyCode == Keys.Enter))
            {
               
                DataTable dt =ReleaseData.arrByteToDataTable(tWoInfo.GetWoInfo(tb_wo.Text,null));
                if (dt.Rows.Count != 0)
                {
                    tb_Partnumber.Text = dt.Rows[0]["PARTNUMBER"].ToString();
                    tb_Inputqty.Text = dt.Rows[0]["INPUTQTY"].ToString();
                    tb_TargetQty.Text = dt.Rows[0]["QTY"].ToString();
                    RouteCode = dt.Rows[0]["ROUTGROUPID"].ToString();
                    ProductLine = dt.Rows[0]["LINEID"].ToString();
                    SendPrgMsg(mLogMsgType.Normal, string.Format("此工单可在[{0}]线生产", ProductLine));
                    SendPrgMsg(mLogMsgType.Normal, string.Format("工单[{0}]确认完成", tb_wo.Text));
                    tb_wo.Enabled = false;
                    imbt_ChgLine.Visible = false;
                }
                else
                {
                    SendPrgMsg(mLogMsgType.Error, string.Format("工单[{0}]没有找到", tb_wo.Text));
                    tb_wo.Text = "";
                }

            }
        }

        private void tb_sn_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tb_sn.Text)) && (e.KeyCode == Keys.Enter))
            {
                try
                {
                    if (string.IsNullOrEmpty(EmpNo))
                    {
                        string Msg = "Emp Format Error";
                        try
                        {
                            
                            string PWD = tb_sn.Text.Split('-')[1];
                            Msg = "Emp No Data";
                            DataTable dt = ReleaseData.arrByteToDataTable(tuser.GetUserInfoByUserId(tb_sn.Text.Split('-')[0]));
                            if (dt.Rows.Count != 0)
                            {
                                if (PWD != dt.Rows[0]["pwd"].ToString())
                                {
                                    SendPrgMsg(mLogMsgType.Error, "NO EMP");
                                }
                                else
                                {
                                    SendPrgMsg(mLogMsgType.Incoming, "EMP OK");
                                    EmpNo = tb_sn.Text.Split('-')[0];
                                }
                            }
                            else
                            {
                                SendPrgMsg(mLogMsgType.Error, "NO USER EMP");
                            }
                        }
                        catch
                        {
                            SendPrgMsg(mLogMsgType.Error, "人员权限格式错误--"+Msg);
                        }

                    }
                    else
                    {
                        string InputData = tb_sn.Text.Trim();
                        if (InputData == "UNDO")
                        {
                            tb_wo.Enabled = true;
                            imbt_ChgLine.Visible = true ;
                            tb_wo.Text = "";
                            tb_Partnumber.Text = "";
                            tb_Inputqty.Text = "0";
                            tb_TargetQty.Text = "0";
                            EmpNo = string.Empty;
                            sSQLMAC = string.Empty;
                            sSQLSN = string.Empty;
                            SendPrgMsg(mLogMsgType.Normal, "清除完成,切换工单.....");
                        }
                        else
                        {
                            if (chkimei.Checked)
                            {
                              DataTable dtImei= ReleaseData.arrByteToDataTable(twip.GetQueryWipAllInfo("IMEI", InputData));
                              if (dtImei.Rows.Count > 0)
                              {
                                  InputData = dtImei.Rows[0]["ESN"].ToString();
                              }
                              else
                              {
                                  SendPrgMsg(mLogMsgType.Error, "IMEI 没有找到");
                                  return;
                              }                              
                            }

                            #region 增加存储过程参数
                            if (!CHECK_PRODUCT_LINE())
                                return;

                           string flag = "0";
                           if (radmobile.Checked)
                               flag = "1";
                           Dictionary<string, object> dic = new Dictionary<string, object>();
                           dic.Add("DATA", InputData);
                           dic.Add("EMP", EmpNo);
                           dic.Add("MODEL", tb_Partnumber.Text.Trim());
                           dic.Add("WO", tb_wo.Text.Trim());
                           dic.Add("LINE", LabLine.Text);
                           dic.Add("FLAG", flag);
                           string sMsg = PubStor.ExecuteProcedure("PRO_TEST_REWORK_INPUT", GenericUtil.MapListConverter.DictionaryToJson(dic));
                          #endregion                
                           
                            if (sMsg == "OK")
                            {
                                SendPrgMsg(mLogMsgType.Incoming, string.Format("SN: [{0}] {1}", InputData, sMsg));
                                tb_Inputqty.Text = (int.Parse(tb_Inputqty.Text) + 1).ToString();
                            }
                            else
                            {
                                SendPrgMsg(mLogMsgType.Error, string.Format("SN: [{0}] {1}", InputData, sMsg));
                            }
                            sSQLMAC = string.Empty;
                            sSQLSN = string.Empty;
                        }
                    }
                }
                catch (Exception ex)
                {
                    SendPrgMsg(mLogMsgType.Error,"程序异常-->"+ex.Message);
                }
                finally
                {
                    tb_sn.Text = "";
                }
            }
        }   

        private void button1_Click(object sender, EventArgs e)
        {
            
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
              SendPrgMsg(mLogMsgType.Error, string.Format("此工单不可在{0}生产", LabLine.Text));
            return flag;
        }

        private void imbt_ChgLine_Click(object sender, EventArgs e)
        {
            string[] EmpData = Input.InputBox.ShowInputBox("输入权限", string.Empty);
            try
            {
                string UserId = EmpData[0];
                string PWD = EmpData[1];
                if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(PWD))
                {
                    string _StrErr = tuser.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);
                    if (_StrErr == "OK")
                    {
                        SendPrgMsg(mLogMsgType.Incoming, "权限正确");
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        Frm_Public.Frm_Select fd = new Frm_Public.Frm_Select(ReleaseData.arrByteToDataTable(tLineInfo.GetAllLineInfo()), ref dic);
                        if (fd.ShowDialog() == DialogResult.OK)
                        {
                            LabLine.Text = dic["线别"].ToString();
                            Encoder.ReadIniFile.IniWriteValue("ReworkInput", "LINE", Encoder.Encoder.EncryptString(dic["线别"].ToString()), IniFilePath);
                        }

                    }
                    else
                    {
                        SendPrgMsg(mLogMsgType.Error, _StrErr);

                    }
                }

            }
            catch
            {
                SendPrgMsg(mLogMsgType.Error, "权限格式不正确");
            }
        }

        
      
    }
}
