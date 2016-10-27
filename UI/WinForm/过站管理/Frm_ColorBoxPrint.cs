using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using GenericUtil;

namespace SFIS_V2
{
    public partial class Frm_ColorBoxPrint : Office2007Form //Form
    {
        public Frm_ColorBoxPrint(Frm_Station_Management Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Frm_Station_Management mFrm;

        public string MyLine = string.Empty;
        public string MYGROUP = string.Empty;
        public string MyStation = string.Empty;
        public string MySection = string.Empty;

        string My_UserId = string.Empty;
        string My_Password = string.Empty;

        string MyUserId = string.Empty;
        string MyPassword = string.Empty;

        bool sMoFlag = false;
        bool My_RePrint = false;
        string LabDir = string.Empty;
        private readonly string IniFilePath = "C:\\SFIS\\SFIS.ini";

        /// <summary>
        /// 打印模板路径
        /// </summary>
        string LabFilePatch = string.Empty;

        FrmBLL.ClsCodeSoft7 cs7 = new FrmBLL.ClsCodeSoft7();
        private void Frm_ColorBoxPrint_Load(object sender, EventArgs e)
        {
            SendMsgOutgoing("正在初始化.....");
            ClearLabel();
            ReadIniFile();
            SetStation();
            MyUserId = mFrm.UserId;
            MyPassword = mFrm.Password;
            cs7.OpenCodeSoft();
            SendMsgPass("初始化完成....");
            SendMsgPass("步骤1: 工单");
            SendMsgPass("步骤2: 产品序列号 备注: 工单只输入一次.");
        }

        private void ClearLabel()
        {
            LabwoId.Text = string.Empty;
            LabPartnumber.Text = string.Empty;
            LabModelDesc.Text = string.Empty;
            LabColor.Text = string.Empty;
            LabSwVer.Text = string.Empty;
            LabFwVer.Text = string.Empty;
        }
        private void ReadIniFile()
        {
            MyLine = FrmBLL.ReadIniFile.IniReadValue("COLOR_BOX_PRINT", "LINE", IniFilePath);
            MYGROUP = FrmBLL.ReadIniFile.IniReadValue("COLOR_BOX_PRINT", "MYGROUP", IniFilePath);
            MyStation = FrmBLL.ReadIniFile.IniReadValue("COLOR_BOX_PRINT", "STATION", IniFilePath);
            MySection = FrmBLL.ReadIniFile.IniReadValue("COLOR_BOX_PRINT", "SECTION", IniFilePath);
            numX.Value = Convert.ToDecimal(string.IsNullOrEmpty(FrmBLL.ReadIniFile.IniReadValue("COLOR_BOX_PRINT", "X", IniFilePath)) ? "0.00" : FrmBLL.ReadIniFile.IniReadValue("COLOR_BOX_PRINT", "X", IniFilePath));
            numY.Value = Convert.ToDecimal(string.IsNullOrEmpty(FrmBLL.ReadIniFile.IniReadValue("COLOR_BOX_PRINT", "Y", IniFilePath)) ? "0.00" : FrmBLL.ReadIniFile.IniReadValue("COLOR_BOX_PRINT", "Y", IniFilePath));


            string SamplingUint = FrmBLL.ReadIniFile.IniReadValue("COLOR_BOX_PRINT", "SamplingUint", IniFilePath);
            LabDir =FrmBLL. ReadIniFile.IniReadValue("COLOR_BOX_PRINT", "Patch", IniFilePath);
            string PatchFlag = FrmBLL.ReadIniFile.IniReadValue("COLOR_BOX_PRINT", "PatchFlag", IniFilePath);
            if (string.IsNullOrEmpty(PatchFlag))
            {
                imbt_LabelPartnumber.Checked = true;
                FrmBLL.ReadIniFile.IniWriteValue("COLOR_BOX_PRINT", "PatchFlag", "0", IniFilePath);
            }
            else
            {
                if (Convert.ToInt32(PatchFlag) == 0)
                    imbt_LabelPartnumber.Checked = true;
                else
                    imbt_LabelwoId.Checked = true;
            }
            if (string.IsNullOrEmpty(LabDir))
            {
                LabDir = "D:";
                FrmBLL.ReadIniFile.IniWriteValue("COLOR_BOX_PRINT","Patch",LabDir,IniFilePath);                
            }
            if (string.IsNullOrEmpty(SamplingUint))
                SamplingUint = "1";

            SamplingUint_Clear();
            switch (Convert.ToInt32(SamplingUint))
            {
                case 1:
                    labSamplingUint.Text = "[ESN]";
                    imbt_Esn.Checked = true;
                    break;
                case 2:
                    labSamplingUint.Text = "[IMEI]";
                   imbt_imei.Checked = true;
                    break;
            }

        }
        public void SetStation()
        {
            LabStation.Text = MyLine + " : " + MYGROUP;
            FrmBLL.ReadIniFile.IniWriteValue("COLOR_BOX_PRINT", "LINE", MyLine, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("COLOR_BOX_PRINT", "MYGROUP", MYGROUP, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("COLOR_BOX_PRINT", "STATION", MyStation, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("COLOR_BOX_PRINT", "SECTION", MySection, IniFilePath);
        }
        private void SendMsgError(string Msg)
        {
            mFrm.ShowMsg(Frm_Station_Management.mLogMsgType.Error, Msg);
        }
        private void SendMsgPass(string Msg)
        {
            mFrm.ShowMsg(Frm_Station_Management.mLogMsgType.Incoming, Msg);
        }
        private void SendMsgOutgoing(string Msg)
        {
            mFrm.ShowMsg(Frm_Station_Management.mLogMsgType.Outgoing, Msg);
        }
        private void SendMsgWarning(string Msg)
        {
            mFrm.ShowMsg(Frm_Station_Management.mLogMsgType.Warning, Msg);
        }
        private void SamplingUint_Clear()
        {
            imbt_Esn.Checked = false;
            imbt_imei.Checked = false;
        }

        private void txt_woId_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_woId.Text) && e.KeyCode == Keys.Enter)
            {
                DataTable dtwoinfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(txt_woId.Text, null, "WOID,PARTNUMBER,PRODUCTNAME,FW_VER,SW_VER"));
                if (dtwoinfo.Rows.Count > 0)
                {
                    LabwoId.Text = dtwoinfo.Rows[0]["WOID"].ToString();
                    LabPartnumber.Text = dtwoinfo.Rows[0]["PARTNUMBER"].ToString();
                    LabModelDesc.Text = dtwoinfo.Rows[0]["PRODUCTNAME"].ToString();
                    LabColor.Text = FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetProductByPartNumber(LabPartnumber.Text)).Rows[0]["PRODUCTCOLOR"].ToString();
                    LabFwVer.Text = dtwoinfo.Rows[0]["FW_VER"].ToString();
                    LabSwVer.Text = dtwoinfo.Rows[0]["SW_VER"].ToString();
                    sMoFlag = true;
                    SendMsgPass(string.Format("工单[{0}] OK..", txt_woId.Text));
                }
                else
                {
                    SendMsgError(string.Format("工单[{0}]不存在",txt_woId.Text));
                    sMoFlag = false;
                }
            }
        }

        private void txt_sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_sn.Text) && e.KeyCode == Keys.Enter)
            {
          
                string _ESN = txt_sn.Text;
                string _StrErr = string.Empty;

                _StrErr = CHECK_SN(txt_sn.Text, out _ESN);
                if (_StrErr != "OK")
                    return;
                if (!My_RePrint)
                {
                    _StrErr = PRO_TEST_MAIN_ONLY(_ESN, "NA");
                    if (_StrErr != "OK")
                        return;
                }

                PrintLabel(_ESN);
            }
        }

        private string CHECK_SN(string DATA, out string ESN)
        {
            string sColnum = string.Empty;
            string StrErr = string.Empty;
            ESN = string.Empty;
            if ( imbt_Esn.Checked )
                sColnum = "ESN";
            if (imbt_imei.Checked)
                sColnum = "IMEI";

            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable( refWebtWipTracking.Instance.GetQueryWipAllInfo(sColnum, DATA));
            if (dt.Rows.Count > 0)
            {
                if (LabwoId.Text  == dt.Rows[0]["WOID"].ToString())
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
                    StrErr =string.Format( "WO Different [{0}≠{1}]",LabwoId.Text  , dt.Rows[0]["WOID"].ToString());
                }
            }
            else
            {
                StrErr = "NO SN";
            }
            if (StrErr == "OK")
                SendMsgPass(string.Format("CHECK_SN : {0}", StrErr));
            else
                SendMsgError(string.Format("CHECK_SN : {0}", StrErr));
            return StrErr;
        }

        private string PRO_TEST_MAIN_ONLY(string DATA,  string EC)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();  

            dic.Add("DATA", DATA);
            dic.Add("MYGROUP", MYGROUP);
            dic.Add("SECTION_NAME", MySection);
            dic.Add("STATION_NAME", MyStation);
            dic.Add("EMP", MyUserId + "-" + MyPassword);
            dic.Add("EC", EC);
            dic.Add("LINE", MyLine);   

            string _StrErr = refWebProcedure.Instance.ExecuteProcedure("PRO_TEST_MAIN_ONLY", MapListConverter.DictionaryToJson(dic));

            if (_StrErr == "OK")
            {                
                SendMsgPass(string.Format("SN : {0}", _StrErr));              
            }
            else
                SendMsgError(string.Format("SN : {0}", _StrErr));
            return _StrErr;
        }

        private void imbt_Esn_Click(object sender, EventArgs e)
        {
            SamplingUint_Clear();
            labSamplingUint.Text = "[ESN]";
            FrmBLL.ReadIniFile.IniWriteValue("COLOR_BOX_PRINT", "SamplingUint","1", IniFilePath);
            imbt_Esn.Checked = true;

        }

        private void imbt_imei_Click(object sender, EventArgs e)
        {
            SamplingUint_Clear();
            labSamplingUint.Text = "[IMEI]";
            FrmBLL.ReadIniFile.IniWriteValue("COLOR_BOX_PRINT", "SamplingUint", "2", IniFilePath);
            imbt_imei.Checked = true;
        }

        private void imbt_SelectLine_Click(object sender, EventArgs e)
        {
            Frm_StationName fsm = new Frm_StationName(this);
            fsm.ShowDialog();
        }

        private void txt_InputData_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_InputData.Text) && e.KeyCode == Keys.Enter)
            {
                string InputData = txt_InputData.Text.ToUpper().Trim();
                try
                {
                    if (string.IsNullOrEmpty(MyLine))
                        throw new Exception("请选择线别");

                    if (InputData == "UNDO")
                    {
                        sMoFlag = false;
                        ClearLabel();
                        SendMsgPass("UNDO OK");
                    }
                    else
                        if (!sMoFlag)
                        {
                            txt_woId.Text = InputData;
                            txt_woId_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                        }
                        else
                        {
                            txt_sn.Text = InputData;
                            txt_sn_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                        }
                }
                catch (Exception ex)
                {
                    SendMsgError(ex.Message);
                }
                finally
                {
                    txt_InputData.Text = string.Empty;
                    txt_InputData.Focus();
                }
            }
        }

        private void Frm_ColorBoxPrint_FormClosing(object sender, FormClosingEventArgs e)
        {
            cs7.QuitCodeSoft();
        }

        public void PrintLabel(string DATA)
        {
            try
            {
                if (Convert.ToInt32(NumPrintQty.Value) == 0)
                    return;

                SendMsgWarning("开始打印标签...");

                Dictionary<string, object> PrintDic = new Dictionary<string, object>();
                PrintDic.Add("ESN", DATA);
                PrintDic.Add("BLACK", LabColor.Text);
                PrintDic.Add("COLOR", LabColor.Text);
                PrintDic.Add("PARTNUMBER", LabPartnumber.Text);
                PrintDic.Add("FWVER", LabFwVer.Text);
                PrintDic.Add("SWVER", LabSwVer.Text);
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetEsnDataInfo("ESN", DATA));
                if (dt.Rows.Count > 0)
                {
                    if (LabwoId.Text != dt.Rows[0]["WOID"].ToString())
                        throw new Exception(string.Format("WO Different [{0}≠{1}]", LabwoId.Text, dt.Rows[0]["WOID"].ToString()));

                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintDic.Add(dr["SNTYPE"].ToString(), dr["SNVAL"].ToString());
                    }
                }
                else
                {
                    throw new Exception("没有信息可以打印...");
                }
                if (string.IsNullOrEmpty(LabFilePatch))
                {
                    if (imbt_LabelPartnumber.Checked)
                        LabFilePatch = string.Format(@"{0}\{1}\{2}", this.LabDir.IndexOf(":") != -1 ? this.LabDir : this.LabDir, "PACK_BOX", LabPartnumber.Text + ".lab");
                    else
                        LabFilePatch = string.Format(@"{0}\{1}\{2}", this.LabDir.IndexOf(":") != -1 ? this.LabDir : this.LabDir, LabwoId.Text, MYGROUP + ".lab");
                }
                cs7.CheckFileExist(LabFilePatch);
                cs7.OpenLabelFile(LabFilePatch, false);
                cs7.ClearVariables();
                cs7.Fill_Label_Variables(PrintDic);
                cs7.SetPrintPosition(numX.Value, numY.Value);
                cs7.SetPrintNum(Convert.ToInt32(NumPrintQty.Value));
                cs7.PrintLabel();
                SendMsgPass("打印完成");
            }
            catch (Exception ex)
            {
                SendMsgError(ex.Message);
            }
            finally
            {
                if (My_RePrint)
                {
                    My_RePrint = false;
                    SendMsgOutgoing("重复打印关闭");
                }
            }

        }

        private void numX_Leave(object sender, EventArgs e)
        {
            FrmBLL.ReadIniFile.IniWriteValue("COLOR_BOX_PRINT", "X", numX.Value.ToString(), IniFilePath);        
        }

        private void numY_Leave(object sender, EventArgs e)
        {
            FrmBLL.ReadIniFile.IniWriteValue("COLOR_BOX_PRINT", "Y", numY.Value.ToString(), IniFilePath);
        }  
        private void imbt_Reprint_Click(object sender, EventArgs e)
        {
                string[] EmpData = Input.InputBox.ShowInputBox("输入权限", string.Empty);
                try
                {
                    string UserId = EmpData[0];
                    string PWD = EmpData[1];
                   string _StrErr = refWebtUserInfo.Instance.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);
                    if (_StrErr == "OK")
                    {
                        FrmBLL.publicfuntion.InserSystemLog(UserId,"BoxPrint","RePrint", FrmBLL.publicfuntion.GetIpv4());
                        Frm_ColorBox_RePrint fcr = new Frm_ColorBox_RePrint(this);
                        fcr.ShowDialog();
                    }
                    else
                    {
                        SendMsgError(_StrErr);
                    }
                }
                catch
                {
                    SendMsgError("权限格式不正确");
                }
            //return;
            //My_RePrint = true;
            //SendMsgOutgoing("重复打印打开");
        }
        private void Clear_LabelPatch()
        {
            imbt_LabelPartnumber.Checked = false;
            imbt_LabelwoId.Checked = false;
        }
        private void imbt_LabelPartnumber_Click(object sender, EventArgs e)
        {
            Clear_LabelPatch();
            imbt_LabelPartnumber.Checked = true;
            FrmBLL.ReadIniFile.IniWriteValue("COLOR_BOX_PRINT", "PatchFlag", "0", IniFilePath);   
        }

        private void imbt_LabelwoId_Click(object sender, EventArgs e)
        {
            Clear_LabelPatch();
            imbt_LabelwoId.Checked = true;
            FrmBLL.ReadIniFile.IniWriteValue("COLOR_BOX_PRINT", "PatchFlag", "1", IniFilePath);   
        }

        private void imbt_LabelPatchSelect_Click(object sender, EventArgs e)
        {
            string[] EmpData = Input.InputBox.ShowInputBox("输入权限", string.Empty);
            try
            {
               
                string UserId = EmpData[0];
                string PWD = EmpData[1];
                string _StrErr = refWebtUserInfo.Instance.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);
                if (_StrErr == "OK")
                {
                    Clear_LabelPatch();
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "选择打印模板";
                    ofd.Filter = "(*.lab Labels)|*.lab";
                    DialogResult dlr = ofd.ShowDialog();
                    if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
                    {
                        LabFilePatch = ofd.FileName;
                        SendMsgWarning("选择打印模板路径成功");
                        SendMsgOutgoing(LabFilePatch);
                    }
                    else
                    {
                        LabFilePatch = string.Empty;
                        SendMsgOutgoing("未选择模板,恢复默认路径");
                        imbt_LabelPartnumber_Click(null,null);
                    }
                }

            }
            catch
            {
                SendMsgError("权限格式不正确");
            }
        }
    }
}
