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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Diagnostics;
using GenericUtil;
using LabelManager2;

namespace SFIS_DCT_INPUT
{
    public partial class SFIS_DCT_INPUT : Office2007Form
    {
        public SFIS_DCT_INPUT()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ��ʾ��Ϣ����
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// ��ʾ��Ϣ������ɫ
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };
        WebServices.tWoInfo.tWoInfo tWoInfo = new WebServices.tWoInfo.tWoInfo();
        WebServices.tLineInfo.tLineInfo sLine = new WebServices.tLineInfo.tLineInfo();
        WebServices.tPublicStoredproc.tPublicStoredproc PubStor = new WebServices.tPublicStoredproc.tPublicStoredproc();
        WebServices.Check_Version.Check_Version chkver = new WebServices.Check_Version.Check_Version();
        WebServices.tWipTracking.tWipTracking twip = new WebServices.tWipTracking.tWipTracking();
        WebServices.tUserInfo.tUserInfo tUser = new WebServices.tUserInfo.tUserInfo();
        WebServices.tStationrecount.tStationrecount tstation = new WebServices.tStationrecount.tStationrecount();
        WebServices.tWipKeyPart.tWipKeyPart _wipkeyparts = new WebServices.tWipKeyPart.tWipKeyPart();
        string sEMP = string.Empty;
        string wo_qty="";
        string station_qty = "";
        string ProductLine = string.Empty;
        bool PrintLabel_Flag = false;
        /// <summary>
        /// C:\\SFIS\\SFIS.ini
        /// </summary>
        string IniFilePath = "C:\\SFIS\\SFIS.ini";
        ApplicationClass lbl = null;
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

                    if (mLogMsgTypeColor[(int)msgtype] == Color.Red)
                    {
                        //  SendBuzz();
                    }
                }));
            }
            catch
            {
            }

        }
        #region  ��ѹ���Ͷ�ȡINI
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
            #region ����API
            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);

            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal,
            int size, string filePath);

            public static void IniWriteValue(string Section, string Key, string Value, string filepath)//��ini�ļ�����д�����ĺ��� 
            {
                WritePrivateProfileString(Section, Key, Value, filepath);
            }

            public static string IniReadValue(string Section, string Key, string filepath)//��ini�ļ����ж������ĺ��� 
            {
                StringBuilder temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "", temp,
                255, filepath);
                return temp.ToString();
            }
            #endregion
        }
        #endregion

        private delegate void InitializationPrg();
        InitializationPrg Initialization;
        private void InitializationInfo()
        {
            string C_RES = "";
            try
            {
                GetLineList();
                GetWoList();
                SendPrgMsg(mLogMsgType.Incoming, "������Ϣ��ʼ���ɹ�");
            }
            catch
            {
                SendPrgMsg(mLogMsgType.Error, C_RES);
            }
        }
        #region ��ȡ��ʼֵ
        private void GetLineList()
        {
            txt_line.Text = Encoder.Encoder.DecryptString(Encoder.ReadIniFile.IniReadValue("SFIS_DCT_INPUT", "LINE", IniFilePath));  
            //DataTable dt = ReleaseData.arrByteToDataTable(sLine.GetAllLineInfo());
            //DataView dv = new DataView(dt);
            //dv.Sort = dt.Columns[0].ToString();
            //DataTable dt2 = dv.ToTable();
            //foreach (DataRow dr in dt2.Rows)
            //{
            //    cbline.Invoke(new EventHandler(delegate
            //    {
            //        cbline.Items.Add(dr[0].ToString());
            //    }));
            //}
        }

        DataTable dtRoute = null;
        private void GetStationList(string woId)
        {

            cbroute.Text = "";
            DataTable dt = ReleaseData.arrByteToDataTable(tWoInfo.GetWoInfo(woId,null));
            if (dt.Rows.Count > 0)
            {
                // cbroute.Text = tWoInfo.GetCraftInfoBywoid(woId);    
                cbroute.Text = dt.Rows[0]["INPUTGROUP"].ToString();
                ProductLine = dt.Rows[0]["LINEID"].ToString();
                SendPrgMsg(mLogMsgType.Incoming, string.Format("�˹�������[{0}]������",ProductLine));
            }
        }
        private void GetWoList()
        {
            string[] ls = PubStor.GetWoList();
            for (int i = 0; i < ls.Length; i++)
            {
                cbwo.Invoke(new EventHandler(delegate
                {
                    cbwo.Items.Add(ls[i]);
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
        #endregion
        private void SFIS_DCT_INPUT_Load(object sender, EventArgs e)
        {
            string C_RES = string.Empty;
            try
            {
                if (!Directory.Exists("C:\\SFIS"))
                    Directory.CreateDirectory("C:\\SFIS");
                C_RES = "������汾��������ʧ��";
                chkver = new WebServices.Check_Version.Check_Version();
                if (!chkver.CheckPrgVsersion("SFIS_DCT_INPUT", this.ProductVersion, null, null, null))
                {
                    SendPrgMsg(mLogMsgType.Error, "�ó���Ϊ�汾�������°�\r\n����º�����");
                    RunFile(System.Windows.Forms.Application.StartupPath + @"\", "AutoUpdate.exe", "ISCM.exe");   
                    this.Enabled = false;
                }
                 lbl= new ApplicationClass();
                 SendPrgMsg(mLogMsgType.Incoming, "��ʼ��CodeSoft�ɹ�");
                Initialization = new InitializationPrg(InitializationInfo);
                Initialization.BeginInvoke(null, null);
                txt_userid.Enabled = false;
                txt_sn.Enabled = false;
                txt_kpsn.Enabled = false;
         
            }
            catch
            {
                SendPrgMsg(mLogMsgType.Error, C_RES);
            }
        }
        /// <summary>
        /// �Զ�����ָ���ĳ���
        /// </summary>
        /// <param name="dir">����·��</param>
        /// <param name="localFileName">��������</param>
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
        private void cbwo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(cbwo.Text) && e.KeyCode == Keys.Enter)
            {
                cbroute.Text = "";
                GetStationList(cbwo.Text.Trim());
            }
        }
        private void cbwo_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(!string.IsNullOrEmpty(cbwo.Text)) 
           {
             //cbroute.Items.Clear();
             GetStationList(cbwo.Text.Trim());
           }
        }
        private void bt_cancel_Click(object sender, EventArgs e)
        {
            cancel_all();
        }
        public void cancel_all()
        {
            
            cbroute.Text = "";
            //cbroute.Items.Clear();
            cbwo.Text = "";
            txt_userid.Text = "";
            txt_sn.Text = "";
            txt_kpsn.Text = "";
            bt_ok.Enabled = true;         
            cbroute.Enabled = true;
            cbwo.Enabled = true;
           // imbt_selectline.Enabled = true;
            ToolSelectLine.Visible = true;
            txt_userid.Enabled = false;
            txt_sn.Enabled = false;
            txt_kpsn.Enabled = false;
            lab_name.Text ="0/0";
            SendPrgMsg(mLogMsgType.Warning, "ȡ����ɣ��ѳ�ʼ������ѡ�񹤵�����");
        }
        private void bt_ok_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txt_line.Text))
            {
                SendPrgMsg(mLogMsgType.Error,"�߱�δѡ��,��ѡ���߱�");
                return;
            }
            if (string.IsNullOrEmpty(cbwo.Text))
            {
                SendPrgMsg(mLogMsgType.Error, "����δѡ��,��ѡ�񹤵�");
                return;
            }
            if (string.IsNullOrEmpty(cbroute.Text))
            {
                SendPrgMsg(mLogMsgType.Error, "��ǰ;��δѡ��,��ѡ��;��");
                return;
            }
            if (!CHECK_PRODUCT_LINE())
                return;

            string station_line_qty = tstation.get_station_qty(cbwo.Text.ToString(), cbroute.Text.ToString(), txt_line.Text.ToString());
            wo_qty = station_line_qty.Split('/')[0].ToString();
            station_qty = station_line_qty.Split('/')[1].ToString();
            lab_name.Text = wo_qty + "/" + station_qty;
            SendPrgMsg(mLogMsgType.Normal, "��ˢ�빤�ź��������Ȩ����֤����");    
            cbroute.Enabled = false;
            cbwo.Enabled = false;
            bt_ok.Enabled = false;
            ToolSelectLine.Visible = false;
            txt_userid.Enabled = true;
            txt_userid.Focus();
            txt_userid.SelectAll();
        }

        private void txt_userid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Check_EMP(txt_userid.Text.ToString()))
                {
                    sEMP = txt_userid.Text.ToString();
                    txt_userid.Text = ReleaseData.arrByteToDataTable(tUser.GetUserInfoByUserId(sEMP.Split('-')[0].ToString())).Rows[0]["username"].ToString();
                }
                else
                {
                    txt_userid.SelectAll();
                    txt_userid.Focus();
                    return;
                }
                txt_userid.Enabled = false;
                txt_sn.Enabled = true;
                txt_sn.Focus();
            }
        }
        private bool Check_EMP(string EMP)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", EMP);

            //string ChkRes = PubStor.ExecuteProcedure("PRO_CHECKEMP", MapListConverter.DictionaryToJson(dic));
            //WebServices.tPublicStoredproc.ProcedureKey Pdk = new WebServices.tPublicStoredproc.ProcedureKey();
            //List<WebServices.tPublicStoredproc.ProcedureKey> LsPdk = new List<WebServices.tPublicStoredproc.ProcedureKey>();
            //Pdk.Variable = "DATA";
            //Pdk.Value = EMP;
            //LsPdk.Add(Pdk);
            string ChkRes = PubStor.ExecuteProcedure("PRO_CHECKEMP", MapListConverter.DictionaryToJson(dic));
            if (ChkRes == "OK")
            {
                SendPrgMsg(mLogMsgType.Normal, "Ȩ�޼���OK����ˢ��SN���롭��");
                return true;
            }
            else
            {
                SendPrgMsg(mLogMsgType.Error, "Ȩ�޼���ʧ��!!" + ChkRes);
                return false;
            }
        }
        private void txt_sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("DATA", txt_sn.Text.ToUpper().Trim().ToString());
                dic.Add("MYGROUP", cbroute.Text.ToString());
                dic.Add("LINE", txt_line.Text.ToString());
                dic.Add("WO", cbwo.Text.ToString());
                dic.Add("EMP", sEMP);
                string ChkRes = PubStor.ExecuteProcedure("PRO_SN_INPUT_WIPFIRST", MapListConverter.DictionaryToJson(dic));
                if (ChkRes == "OK")
                {
                    CHECKROUTE();
                }
                else
                {
                    SendPrgMsg(mLogMsgType.Error, "�����SN: " + txt_sn.Text.ToUpper().Trim().ToString() + " �쳣->" + ChkRes);
                    txt_sn.Focus();
                    txt_sn.SelectAll();
                    return;
                }
            }
        }
        public void CHECKROUTE()
        {
            //WebServices.tPublicStoredproc.ProcedureKey Pdk = new WebServices.tPublicStoredproc.ProcedureKey();
            //List<WebServices.tPublicStoredproc.ProcedureKey> LsPdk = new List<WebServices.tPublicStoredproc.ProcedureKey>();
            //Pdk.Variable = "DATA";
            //Pdk.Value = txt_sn.Text.ToUpper().Trim().ToString();
            //LsPdk.Add(Pdk);
            //Pdk = new WebServices.tPublicStoredproc.ProcedureKey();
            //Pdk.Variable = "MYGROUP";
            //Pdk.Value = cbroute.Text.ToString();
            //LsPdk.Add(Pdk);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", txt_sn.Text.ToUpper().Trim().ToString());
            dic.Add("MYGROUP", cbroute.Text.ToString());

            string ChkRes = PubStor.ExecuteProcedure("PRO_CHECKROUTE", MapListConverter.DictionaryToJson(dic));
            SaveTxtLog(System.Environment.CurrentDirectory + "\\DCT_INPUT", "ESN",ChkRes,txt_sn.Text.ToUpper().Trim().ToString());
            if (ChkRes == "OK")
            {
                SendPrgMsg(mLogMsgType.Normal, "SN:"+ txt_sn.Text.ToUpper().Trim().ToString()+"  ����OK,��ˢ��KPSN����");
                txt_sn.Enabled = false;
                txt_kpsn.Enabled = true;
                txt_kpsn.Focus();
            }
            else
            {
                SendPrgMsg(mLogMsgType.Error, "���̼����󣡣�->" + ChkRes);
                txt_sn.Focus();
                txt_sn.SelectAll();
                return;
            }
        }

        private void txt_kpsn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {               
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("DATA", txt_kpsn.Text.ToUpper().Trim().ToString());
                dic.Add("SN", txt_sn.Text.ToUpper().Trim().ToString());
                dic.Add("WO", cbwo.Text.ToString());

                string ChkRes = PubStor.ExecuteProcedure("PRO_CHECK_KPS_VALID", MapListConverter.DictionaryToJson(dic));
                if (ChkRes == "OK")
                {
                    INSERT_KEYPARTS();
                }
                else
                {
                    SendPrgMsg(mLogMsgType.Error, "�����KPSN" + txt_kpsn.Text.ToUpper().Trim().ToString() + " �쳣->" + change_messge(ChkRes));
                    txt_kpsn.Focus();
                    txt_kpsn.SelectAll();
                    return;
                }
            }
        }
        private void INSERT_KEYPARTS()
        {            
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", txt_kpsn.Text.ToUpper().Trim().ToString());
            dic.Add("SN", txt_sn.Text.ToUpper().Trim().ToString());
            dic.Add("MYGROUP", cbroute.Text.ToString());
            dic.Add("EMP", sEMP);
            dic.Add("LINE", txt_line.Text.ToString());
            dic.Add("WO", cbwo.Text.ToString());

            string ChkRes = PubStor.ExecuteProcedure("PRO_INSERT_KEYPARTS", MapListConverter.DictionaryToJson(dic));
            SaveTxtLog(System.Environment.CurrentDirectory + "\\DCT_INPUT", "KPESN", ChkRes, txt_kpsn.Text.ToUpper().Trim().ToString());
            if (ChkRes == "OK")
            {
                station_qty = (Convert.ToInt32(station_qty)+1).ToString();
                lab_name.Text = wo_qty + "/" + station_qty;
                SendPrgMsg(mLogMsgType.Normal, "KPSN:" + txt_kpsn.Text.ToUpper().Trim().ToString() + " OK,��ˢ����һ��SN����");
                if (ToolPrintlabel.Checked)
                {
                    PrintLabel(txt_sn.Text);
                }
                txt_sn.Enabled = true;
                txt_kpsn.Enabled = false;
                txt_kpsn.Text = "";
                txt_sn.Text = "";
                txt_sn.Focus();
                
            }
            else
            {
                SendPrgMsg(mLogMsgType.Error, "KPSN:" + txt_kpsn.Text.ToUpper().Trim().ToString() + " д��ϵͳʧ�ܣ���->" + change_messge(ChkRes));
                txt_kpsn.Focus();
                txt_kpsn.SelectAll();
                return;
            }
        }
        public string change_messge(string ChkRes)
        {
            string c_messge = "";
            switch (ChkRes)
            {
                case "NO SN FOUND":
                    c_messge= "KPSN������";
                    break;
                case "KPS DUP":
                    c_messge= "�����ظ���KPSN�Ѿ�����";
                    break;
                case "SN HAVE KPS":
                    c_messge = "SN�Ѿ��󶨹�KPSN";
                    break;
                case "THE SAME TWO WO":
                    c_messge = "KPSN�������ʹ�����";
                    break;
                case "SN IN REPAIR":
                    c_messge = "KPSN��ά����";
                    break;
                case "KPS ROUTE NOT END":
                    c_messge = "KPSN������δ����";
                    break;
                default:
                    c_messge=ChkRes;
                    break;
            }
            return c_messge;
        }
        /// <summary>
        /// ������±�
        /// </summary>
        /// <param name="Path"></param>
        public void SaveTxtLog(string path, string sn_name,string sn_result,string esn)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string names = path + "\\" + cbwo.Text.ToString() + ".txt";

            if (!File.Exists(names))
            {
                FileStream myFs = new FileStream(names, FileMode.Create);
                myFs.Close();
            }

            FileStream fst = new FileStream(names, FileMode.Append);
            //д���ݵ�a.txt��ʽ 
            StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
            swt.WriteLine("-----------------------------------");
            swt.WriteLine(txt_line.Text.ToString());
            swt.WriteLine(cbroute.Text.ToString());
            swt.WriteLine(sn_name);
            swt.WriteLine(esn);
            swt.WriteLine(sn_result);
            swt.WriteLine(txt_userid.Text.ToString());
            swt.Close();
            fst.Close();
        }

        private void imbt_selectline_Click(object sender, EventArgs e)
        {
            string[] EmpData = Input.InputBox.ShowInputBox("����Ȩ��", string.Empty);
            try
            {
                string UserId = EmpData[0];
                string PWD = EmpData[1];
                if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(PWD))
                {                
                    string _StrErr = tUser.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);
                    if (_StrErr == "OK")
                    {
                        SendPrgMsg(mLogMsgType.Incoming, "Ȩ����ȷ");
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        Frm_Public.Frm_Select fd = new Frm_Public.Frm_Select(ReleaseData.arrByteToDataTable(sLine.GetAllLineInfo()), ref dic);
                        if (fd.ShowDialog() == DialogResult.OK)
                        {
                           txt_line.Text = dic["�߱�"].ToString();
                            Encoder.ReadIniFile.IniWriteValue("SFIS_DCT_INPUT", "LINE", Encoder.Encoder.EncryptString(dic["�߱�"].ToString()), IniFilePath);
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
                SendPrgMsg(mLogMsgType.Error, "Ȩ�޸�ʽ����ȷ");
            }
        }

        private bool CHECK_PRODUCT_LINE()
        {
            bool flag = false;
            foreach (string str in ProductLine.Split(','))
            {
                if (str ==  txt_line.Text)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                SendPrgMsg(mLogMsgType.Error, string.Format("�˹���������{0}����", txt_line.Text));
            return flag;
        }

        private void ToolSelectLine_Click(object sender, EventArgs e)
        {
            string[] EmpData = Input.InputBox.ShowInputBox("����Ȩ��", string.Empty);
            try
            {
                string UserId = EmpData[0];
                string PWD = EmpData[1];
                if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(PWD))
                {
                    string _StrErr = tUser.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);
                    if (_StrErr == "OK")
                    {
                        SendPrgMsg(mLogMsgType.Incoming, "Ȩ����ȷ");
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        Frm_Public.Frm_Select fd = new Frm_Public.Frm_Select(ReleaseData.arrByteToDataTable(sLine.GetAllLineInfo()), ref dic);
                        if (fd.ShowDialog() == DialogResult.OK)
                        {
                            txt_line.Text = dic["�߱�"].ToString();
                            Encoder.ReadIniFile.IniWriteValue("SFIS_DCT_INPUT", "LINE", Encoder.Encoder.EncryptString(dic["�߱�"].ToString()), IniFilePath);
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
                SendPrgMsg(mLogMsgType.Error, "Ȩ�޸�ʽ����ȷ");
            }
        }

        public void PrintLabel(string Esn)
        {
          
            DataTable dt = ReleaseData.arrByteToDataTable(_wipkeyparts.GetWipKeyPart(Esn));
            if (dt.Rows.Count > 0)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ESN", dt.Rows[0]["ESN"].ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    dic.Add(dr["SNTYPE"].ToString(), dr["SNVAL"].ToString());
                }
                PublicPrintLabel(dic);

            }
            else
            {
                SendPrgMsg(mLogMsgType.Error, "û����Ҫ��ӡ������");
            }

        }
        public void PublicPrintLabel(Dictionary<string,string> dic)
        {
          //  StripStatusLabelPatch.Text = "Label File: " + filepatch;
            string LabDir = ReadIniFile.IniReadValue("TEST_INPUT", "Patch", IniFilePath);
            string filepatch = string.Format(@"{0}\{1}\{2}", LabDir.IndexOf(":") != -1 ? LabDir : LabDir + ":", cbwo.Text,cbroute.Text + ".lab");

            string PrintQty = OperateIni.IniReadValue("TEST_INPUT", "LabelQty", IniFilePath);
            string 
                coordinateX = OperateIni.IniReadValue("TEST_INPUT", "LabelX", IniFilePath);
            string coordinateY = OperateIni.IniReadValue("TEST_INPUT", "LabelY", IniFilePath);

            if (!File.Exists(filepatch))  //�ж������ļ��Ƿ����
            {
                SendPrgMsg(mLogMsgType.Error,  "���뵵û���ҵ�,·��:" + filepatch);
                return;
            }
           
            try
            {

                lbl.Documents.Open(filepatch, false);// ������ƺõ�label�ļ�
                Document doc = lbl.ActiveDocument;
                SendPrgMsg(mLogMsgType.Incoming,  "���ģ�����...");
                for (int i = 0; i < doc.Variables.FormVariables.Count; i++)
                {
                    doc.Variables.FormVariables.Item(doc.Variables.FormVariables.Item(i + 1).Name).Value = "";
                }
                SendPrgMsg(mLogMsgType.Incoming,  string.Format("ģ�����������,����{0}��...", doc.Variables.FormVariables.Count));
                foreach (KeyValuePair<string,string> _DicKeyValues in dic)
                {
                    try
                    {
                        doc.Variables.FormVariables.Item(_DicKeyValues.Key).Value = _DicKeyValues.Value; //��������ֵ                     
                        SendPrgMsg(mLogMsgType.Outgoing, string.Format("����ӡ�������:{0}->{1}", _DicKeyValues.Key, _DicKeyValues.Value));
                    }
                    catch
                    {
                    }
                }

                int Num = Convert.ToInt32(PrintQty);        //��ӡ����
                doc.Format.MarginLeft = (Convert.ToInt32(coordinateX)) * 100;
                doc.Format.MarginTop = (Convert.ToInt32(coordinateY)) * 100;
                doc.PrintDocument(Num);               //��ӡ
                SendPrgMsg(mLogMsgType.Normal, "��ӡ���");
            }
            catch (Exception ex)
            {
                SendPrgMsg(mLogMsgType.Error,  "�����쳣" + ex.Message);
            }
            finally
            {
               // lbl.Quit(); //�˳�
            }

        }

        private void ToolRePrint_Click(object sender, EventArgs e)
        {
            Frm_RePrint frp = new Frm_RePrint(this);
            frp.ShowDialog();
        }

        private void ToolPrintlabel_Click(object sender, EventArgs e)
        {
            ToolPrintlabel.Checked = !ToolPrintlabel.Checked;
            if (ToolPrintlabel.Checked)
            {
                OperateIni.IniWriteValue("TEST_INPUT", "PrintLabel","1" ,IniFilePath);
                SendPrgMsg(mLogMsgType.Warning, "��ӡ����");
            }
            else
            {
                OperateIni.IniWriteValue("TEST_INPUT", "PrintLabel", "0", IniFilePath);
                SendPrgMsg(mLogMsgType.Warning, "��ӡ����ȡ��");
            }
        }

        private void SFIS_DCT_INPUT_FormClosing(object sender, FormClosingEventArgs e)
        {
            lbl.Quit(); //�˳�
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
    public class OperateIni
    {
        #region ����API
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
        string key, string def, StringBuilder retVal,
        int size, string filePath);

        public static void IniWriteValue(string Section, string Key, string Value, string filepath)//��ini�ļ�����д�����ĺ��� 
        {
            WritePrivateProfileString(Section, Key, Value, filepath);
        }

        public static string IniReadValue(string Section, string Key, string filepath)//��ini�ļ����ж������ĺ��� 
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, filepath);
            return temp.ToString();
        }
        #endregion
    }
}