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

namespace SFIS_V2
{
    public partial class Frm_ReworkInput : Office2007Form //Form
    {
        public Frm_ReworkInput(Frm_Station_Management Frm)
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
        string My_MoNumber = string.Empty;
        string My_InputGroup = string.Empty;

        bool sMoFlag = false;

        private readonly string IniFilePath = "C:\\SFIS\\SFIS.ini";
        private void Frm_ReworkInput_Load(object sender, EventArgs e)
        {
            My_UserId = mFrm.UserId;
            My_Password = mFrm.Password;
            ReadIniFile();
            SetStation();
            ClearLabel();

            SendMsgPass("步骤1: 工单");
            SendMsgPass("步骤2: 产品序列号 备注: 工单只输入一次.");
        }
        private void ReadIniFile()
        {
            MyLine = FrmBLL.ReadIniFile.IniReadValue("REWORK_INPUT", "LINE", IniFilePath);
            MYGROUP = FrmBLL.ReadIniFile.IniReadValue("REWORK_INPUT", "MYGROUP", IniFilePath);
            MyStation = FrmBLL.ReadIniFile.IniReadValue("REWORK_INPUT", "STATION", IniFilePath);
            MySection = FrmBLL.ReadIniFile.IniReadValue("REWORK_INPUT", "SECTION", IniFilePath);
        }
        public void SetStation()
        {
            LabStation.Text = MyLine + " : " + MYGROUP;
            FrmBLL.ReadIniFile.IniWriteValue("REWORK_INPUT", "LINE", MyLine, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("REWORK_INPUT", "MYGROUP", MYGROUP, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("REWORK_INPUT", "STATION", MyStation, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("REWORK_INPUT", "SECTION", MySection, IniFilePath);
        }
        private void SendMsgError(string Msg)
        {
            mFrm.ShowMsg(Frm_Station_Management.mLogMsgType.Error, Msg);
        }
        private void SendMsgPass(string Msg)
        {
            mFrm.ShowMsg(Frm_Station_Management.mLogMsgType.Incoming, Msg);
        }
        private void imbt_Select_line_Click(object sender, EventArgs e)
        {
            Frm_StationName fsm = new Frm_StationName(this);
            fsm.ShowDialog();
        }
        private void ClearLabel()
        {
            LabwoId.Text = string.Empty;
            LabPartnumber.Text = string.Empty;
            LabModelDesc.Text = string.Empty;
            LabInputQty.Text = "0";
            LabTargetQty.Text = "0";
            LabClearType.Text = string.Empty;
        }
        private void txt_woId_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_woId.Text) && e.KeyCode == Keys.Enter)
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(txt_woId.Text, null, "PARTNUMBER,PRODUCTNAME,QTY,INPUTQTY,INPUTGROUP,CLEAR_SERIAL_TYPE"));
                if (dt.Rows.Count > 0)
                {
                    LabwoId.Text = txt_woId.Text;
                    LabPartnumber.Text = dt.Rows[0]["PARTNUMBER"].ToString();
                    LabModelDesc.Text = dt.Rows[0]["PRODUCTNAME"].ToString();
                    LabTargetQty.Text = dt.Rows[0]["QTY"].ToString();
                    LabInputQty.Text = dt.Rows[0]["INPUTQTY"].ToString();
                    My_InputGroup = dt.Rows[0]["INPUTGROUP"].ToString();
                    LabClearType.Text = dt.Rows[0]["CLEAR_SERIAL_TYPE"].ToString();
                    My_MoNumber = txt_woId.Text;
                    sMoFlag = true;
                    SendMsgPass(string.Format("WO[{0}] OK", txt_woId.Text));
                }
                else
                {
                    SendMsgError(string.Format("WO[{0}] Not Found", txt_woId.Text));
                    sMoFlag = false;
                }

            }
        }

        private void txt_InputData_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_InputData.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    string ScanData = txt_InputData.Text.Trim().ToUpper();

                    if (string.IsNullOrEmpty(MyLine))
                        throw new Exception("请选择线别");

                    if (ScanData == "UNDO")
                    {
                   
                        sMoFlag = false;
                        ClearLabel();
                        SendMsgPass("UNDO OK");
                        return;
                    }
                    if (!sMoFlag)
                    {
                        txt_woId.Text = ScanData;
                        txt_woId_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    }
                    else
                    {                      
                        txt_sn.Text = ScanData;
                        txt_sn_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    }
                }
                catch (Exception ex)
                {
                    SendMsgError("Execption: " + ex.Message);
                }
                finally
                {
                    txt_InputData.Text = string.Empty;
                    txt_InputData.Focus();
                }
            }
        }

        private void txt_sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_sn.Text) && e.KeyCode == Keys.Enter)
            {
                string C_ESN=txt_sn.Text;
                if (radIMEI.Checked)
                {

                    DataTable dtImei = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo("IMEI", C_ESN));
                    if (dtImei.Rows.Count > 0)
                    {
                        C_ESN = dtImei.Rows[0]["ESN"].ToString();
                    }
                    else
                    {
                        SendMsgError("IMEI 没有找到");
                        return;
                    }  
                }


                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("DATA",C_ESN);
                dic.Add("MYGROUP", MYGROUP);
                dic.Add("SECTION_NAME", MySection);
                dic.Add("STATION_NAME", MyStation);
                dic.Add("EMP", My_UserId + "-" + My_Password);
                dic.Add("MODEL", LabPartnumber.Text);
                dic.Add("WO", My_MoNumber);
                dic.Add("LINE", MyLine);
                dic.Add("FLAG", 0);
                string _StrErr= refWebProcedure.Instance.ExecuteProcedure("PRO_TEST_REWORK_INPUT", GenericUtil.MapListConverter.DictionaryToJson(dic));
                if (_StrErr == "OK")
                {
                    LabInputQty.Text = (Convert.ToInt32(LabInputQty.Text) + 1).ToString();
                    SendMsgPass(string.Format("SN[{0}] OK", txt_sn.Text));
                }
                else
                {
                    SendMsgError(string.Format("ERROR:[{0}] {1}", txt_sn.Text, _StrErr));
                }
            
            }
        }
    }
}
