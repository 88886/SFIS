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
    public partial class Frm_INPUT_SN_FIRST : Office2007Form //Form
    {
        public Frm_INPUT_SN_FIRST(Frm_Station_Management Frm)
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
        string My_EC = "NA";
        string My_MoNumber = string.Empty;
        string My_InputGroup = string.Empty;

        bool sMoFlag = false;
        bool sECFlag = false;

        private readonly string IniFilePath = "C:\\SFIS\\SFIS.ini";

        private void Frm_INPUT_SN_FIRST_Load(object sender, EventArgs e)
        {
            My_UserId = mFrm.UserId;
            My_Password = mFrm.Password;
            // LabUser.Text = mFrm.UserName;
            ReadIniFile();
            SetStation();
            txt_InputData.Focus();
            ClearLabel();
            SendMsgPass("步骤1: 工单");
            SendMsgPass("步骤2: 产品序列号 备注: 工单只输入一次.");
             
        }
        private void ClearLabel()
        {
            LabwoId.Text = string.Empty;
            LabPartnumber.Text = string.Empty;
            LabModelDesc.Text = string.Empty;
            LabInputQty.Text = "0";
            LabTargetQty.Text = "0";
        }

        private void ReadIniFile()
        {
            MyLine = FrmBLL.ReadIniFile.IniReadValue("INPUT_SN_FIRST", "LINE", IniFilePath);
            MYGROUP = FrmBLL.ReadIniFile.IniReadValue("INPUT_SN_FIRST", "MYGROUP", IniFilePath);
            MyStation = FrmBLL.ReadIniFile.IniReadValue("INPUT_SN_FIRST", "STATION", IniFilePath);
            MySection = FrmBLL.ReadIniFile.IniReadValue("INPUT_SN_FIRST", "SECTION", IniFilePath);
        }
        public void SetStation()
        {
            LabStation.Text = MyLine + " : " + MYGROUP;
            FrmBLL.ReadIniFile.IniWriteValue("INPUT_SN_FIRST", "LINE", MyLine, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("INPUT_SN_FIRST", "MYGROUP", MYGROUP, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("INPUT_SN_FIRST", "STATION", MyStation, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("INPUT_SN_FIRST", "SECTION", MySection, IniFilePath);
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


        private void txt_woId_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_woId.Text) && e.KeyCode == Keys.Enter)
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(txt_woId.Text, null, "PARTNUMBER,PRODUCTNAME,QTY,INPUTQTY,INPUTGROUP"));
                if (dt.Rows.Count > 0)
                {
                    LabwoId.Text = txt_woId.Text;
                    LabPartnumber.Text = dt.Rows[0]["PARTNUMBER"].ToString();
                    LabModelDesc.Text = dt.Rows[0]["PRODUCTNAME"].ToString();
                    LabTargetQty.Text = dt.Rows[0]["QTY"].ToString();
                    LabInputQty.Text = dt.Rows[0]["INPUTQTY"].ToString();
                    My_InputGroup = dt.Rows[0]["INPUTGROUP"].ToString();
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

        private void txt_ec_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_ec.Text) && e.KeyCode == Keys.Enter)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("DATA", txt_ec.Text);
                string _StrErr = refWebProcedure.Instance.ExecuteProcedure("PRO_CHECKEC", MapListConverter.DictionaryToJson(dic));
                if (_StrErr == "OK")
                {
                    My_EC = txt_ec.Text;
                    sECFlag = true;
                    SendMsgPass("EC OK");
                }
                else
                {
                    My_EC = "NA";
                    txt_ec.Text = "NA";
                    sECFlag = false;
                    // SendMsgError("EC ERROR: " + _StrErr);
                }
            }

        }

        private void txt_sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_sn.Text) && e.KeyCode == Keys.Enter)
            {
                if (My_InputGroup == MYGROUP)
                {

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("DATA", txt_sn.Text);
                    dic.Add("MYGROUP", MYGROUP);
                    dic.Add("SECTION_NAME", MySection);
                    dic.Add("STATION_NAME", MyStation);
                    dic.Add("EMP", My_UserId + "-" + My_Password);
                    dic.Add("EC", My_EC);
                    dic.Add("LINE", MyLine);
                    dic.Add("WO", My_MoNumber);
                    string _StrErr = refWebProcedure.Instance.ExecuteProcedure("PRO_INPUT_SN_FIRST", MapListConverter.DictionaryToJson(dic));
                    if (_StrErr == "OK")
                    {
                        LabInputQty.Text = (Convert.ToInt32(LabInputQty.Text) + 1).ToString();
                        SendMsgPass(string.Format("SN[{0}] OK", txt_sn.Text));
                    }
                    else
                    {
                        SendMsgError(string.Format("ERROR:[{0}] {1}", txt_sn.Text, _StrErr));
                    }
                    My_EC = "NA";
                    sECFlag = false;
                }
                else
                {
                    SendMsgError(string.Format("ERROR:选择途程[{0}]与工单投入途程[{1}]不一致", MYGROUP,My_InputGroup));
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
                        My_EC = "NA";
                        sECFlag = false;
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
                        if (!sECFlag)
                        {
                            txt_ec.Text = ScanData;
                            txt_ec_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                            if (sECFlag)
                                return;
                        }
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
    }
}
