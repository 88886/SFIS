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
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class Frm_TEST_MAIN_ONLY : Office2007Form //Form
    {
        public Frm_TEST_MAIN_ONLY(Frm_Station_Management Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Frm_Station_Management mFrm;
        public string MyLine = string.Empty;
        public string MYGROUP = string.Empty;
        public string MyStation = string.Empty;
        public string MySection = string.Empty;

        string MyUserId = string.Empty;
        string MyPassword = string.Empty;
        string MyEC = "NA";
        string My_MoNumber = string.Empty;
        /// <summary>
        /// 工单绑定的线体
        /// </summary>
        string My_Line_woInfo = string.Empty;
        private readonly string IniFilePath = "C:\\SFIS\\SFIS.ini";
        private void Frm_TEST_MAIN_ONLY_Load(object sender, EventArgs e)
        {
            MyUserId = mFrm.UserId;
            MyPassword = mFrm.Password;
            LabUser.Text = mFrm.UserName;
            ReadIniFile();
            SetStation();
            groupPanel1.Width = panelStation.Width / 2;
            txt_Pass_input.Focus();
            SendMsgPass("良品: 直接刷入产品序列号");
            SendMsgPass("不良品 1: 不良代码  2: 产品序列号");
            LabStatus.Height = groupPanel1.Height / 2;
            LabStatusreject.Height = groupPanel1.Height / 2;
        }

        private void ReadIniFile()
        {
            MyLine = FrmBLL.ReadIniFile.IniReadValue("TEST_MAIN_ONLY", "LINE", IniFilePath);
            MYGROUP = FrmBLL.ReadIniFile.IniReadValue("TEST_MAIN_ONLY", "MYGROUP", IniFilePath);
            MyStation = FrmBLL.ReadIniFile.IniReadValue("TEST_MAIN_ONLY", "STATION", IniFilePath);
            MySection = FrmBLL.ReadIniFile.IniReadValue("TEST_MAIN_ONLY", "SECTION", IniFilePath);
        }
        public void SetStation()
        {
            LabStation.Text = MyLine + " : " + MYGROUP;
            FrmBLL.ReadIniFile.IniWriteValue("TEST_MAIN_ONLY", "LINE", MyLine, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("TEST_MAIN_ONLY", "MYGROUP", MYGROUP, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("TEST_MAIN_ONLY", "STATION", MyStation, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("TEST_MAIN_ONLY", "SECTION", MySection, IniFilePath);
        }

        private void imbt_Select_line_Click(object sender, EventArgs e)
        {
            string[] EmpData = Input.InputBox.ShowInputBox("输入权限", string.Empty);
            try
            {
                string UserId = EmpData[0];
                string PWD = EmpData[1];
                string _StrErr = refWebtUserInfo.Instance.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);
                if (_StrErr == "OK")
                {
                    Frm_StationName fsm = new Frm_StationName(this);
                    fsm.ShowDialog();
                }
                else
                {
                  SendMsgError( _StrErr);
                   
                }
            }
            catch
            {
                SendMsgError("权限格式不正确");                 
            }

         
        }

        private void SendMsgError(string Msg)
        {
            mFrm.ShowMsg(Frm_Station_Management.mLogMsgType.Error,Msg);
        }
         private void SendMsgPass(string Msg)
        {
            mFrm.ShowMsg(Frm_Station_Management.mLogMsgType.Incoming,Msg);
        }
         private void SendMsgOutgoing(string Msg)
         {
             mFrm.ShowMsg(Frm_Station_Management.mLogMsgType.Outgoing, Msg);
         }


        private string CHECK_SN(string SN)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", SN);

            string _StrErr = refWebProcedure.Instance.ExecuteProcedure("PRO_CHECKSN", MapListConverter.DictionaryToJson(dic));
         
            if (_StrErr == "OK")
                SendMsgPass("CHECK SN OK");
            else
                SendMsgError( _StrErr);

            return _StrErr;
        }

        public string PRO_CHECK_SN(string DATA)
        {
            string C_ERRFLAG = string.Empty;
            string C_SCRAPFLAG = string.Empty;
            string C_WOID = string.Empty;
            string RES = "OK";
            try
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.Get_WIP_TRACKING("ESN", DATA, "WOID,ERRFLAG,SCRAPFLAG")); ;
                if (dt.Rows.Count > 0)
                {
                    C_ERRFLAG = dt.Rows[0]["ERRFLAG"].ToString();
                    C_SCRAPFLAG = dt.Rows[0]["SCRAPFLAG"].ToString();
                    C_WOID = dt.Rows[0]["WOID"].ToString();
                    if (C_ERRFLAG != "0")                 
                        throw new Exception("SN IN REPAIR");
                    if (C_SCRAPFLAG != "0")
                        throw new Exception("SN HAS SCRAP");
                }
                else
                {
                    throw new Exception("NO SN");
                }

                if (RES == "OK")
                {
                    if (string.IsNullOrEmpty(My_MoNumber))
                    {
                        DataTable dt_woinfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(C_WOID, null, "LINEID"));
                        if (dt_woinfo.Rows.Count > 0)
                        {
                            My_MoNumber = C_WOID;
                            My_Line_woInfo = dt_woinfo.Rows[0]["LINEID"].ToString();
                        }
                        else
                        {
                          throw new Exception(string.Format("工单[{0}]没有找到", C_WOID));                           
                        }
                    }
                    if (My_MoNumber != C_WOID)                    
                    {
                        RES = string.Format("工单不同,当前应该扫描工单[{0}],实际扫描工单[{1}]", My_MoNumber, C_WOID);
                        throw new Exception(RES);
                    }

                    if (!string.IsNullOrEmpty(My_Line_woInfo))
                    {
                        if (!My_Line_woInfo.Split(',').Contains(MyLine))
                        {
                            RES = string.Format("当前选择线体[{0}],与工单[{1}]设定线体[{2}]不符", MyLine, C_WOID, My_Line_woInfo);
                            throw new Exception(RES);
                        }
                    }

                    if (RES == "OK")
                    {
                        SendMsgOutgoing(string.Format("[{0}]--> 工单[{1}]", DATA, C_WOID));
                    }
                }
                if (RES == "OK")
                    SendMsgPass(RES);
                else
                    SendMsgError(RES);


                return RES;
            }
            catch (Exception ex)
            {
                SendMsgError(ex.Message);
                return ex.Message;
            }
        }
        private string CHECK_EC(string EC)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", EC);
            string _StrErr = refWebProcedure.Instance.ExecuteProcedure("PRO_CHECKEC", MapListConverter.DictionaryToJson(dic));
            if (_StrErr == "OK")
            {
                SendMsgPass("EC OK");
                LabStatusreject.Text = "OK";
                LabStatusreject.ForeColor = Color.Green;
            }
            else
                SendMsgError("EC ERROR: " + _StrErr);
            return _StrErr;
        }
        private string TEST_MAIN_ONLY(string SN,string EC)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", SN);
            dic.Add("MYGROUP", MYGROUP);
            dic.Add("SECTION_NAME", MySection);
            dic.Add("STATION_NAME", MyStation);
            dic.Add("EMP", MyUserId+"-"+MyPassword);
            dic.Add("EC", EC);
            dic.Add("LINE", MyLine);          
            string _StrErr = refWebProcedure.Instance.ExecuteProcedure("PRO_TEST_MAIN_ONLY", MapListConverter.DictionaryToJson(dic));
            if (_StrErr == "OK")
                SendMsgPass(string.Format("SN[{0}] OK",SN));
            else
                SendMsgError(string.Format("SN[{0}] {1}",SN, _StrErr));

            return _StrErr;
        }


        private void txt_Pass_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Pass_input.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (string.IsNullOrEmpty(MyLine))
                        throw new Exception("请选择线别");


                    string InputStr = txt_Pass_input.Text.ToUpper().Trim();
                    if (InputStr == "UNDO")
                    {
                        LabStatus.Text = "OK";
                        LabStatus.ForeColor = Color.Green;
                        My_MoNumber = string.Empty;
                        My_Line_woInfo = string.Empty;
                        SendMsgPass("UNDO OK..");
                        return;
                    }


                    LabStatus.Text = "FAIL";
                    LabStatus.ForeColor = Color.Red;
                   // if (CHECK_SN(InputStr) == "OK")
                    if (PRO_CHECK_SN(InputStr)=="OK")
                    {
                       if( TEST_MAIN_ONLY(InputStr, "NA")=="OK")
                       {
                           LabStatus.Text = "OK";
                           LabStatus.ForeColor = Color.Green;
                       }
                    }
                }

                catch (Exception ex)
                {
                    SendMsgError("Execption: " + ex.Message);
                }

                finally
                {
                    txt_Pass_input.Text = string.Empty;
                    txt_Pass_input.Focus();
                }
            }
        }

        private void txt_Fail_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Fail_Input.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    string InputStr = txt_Fail_Input.Text.ToUpper().Trim();
                    if (string.IsNullOrEmpty(MyLine))
                        throw new Exception("请选择线别");

                    if (InputStr == "UNDO")
                    {
                        LabStatusreject.Text = "OK";
                        LabStatusreject.ForeColor = Color.Green;
                        My_MoNumber = string.Empty;
                        My_Line_woInfo = string.Empty;
                        SendMsgPass("UNDO OK");
                        MyEC = "NA";
                        return;
                    }
                    LabStatusreject.Text = "Fail";            
                    LabStatusreject.ForeColor = Color.Red;
                    if (MyEC == "NA")
                    {
                         if (CHECK_EC(InputStr) == "OK")                      
                        {
                            MyEC = InputStr;
                        }
                    }
                    else
                    {

                       // if (CHECK_SN(InputStr) == "OK")
                        if (PRO_CHECK_SN(InputStr) == "OK")
                        {
                            if (TEST_MAIN_ONLY(InputStr, MyEC) == "OK")
                            {
                                LabStatusreject.Text = "OK";
                                LabStatusreject.ForeColor = Color.Green;
                            }
                            MyEC = "NA";
                        }
                    }
                }

                catch (Exception ex)
                {
                    SendMsgError("Execption: " + ex.Message);
                }

                finally
                {
                    txt_Fail_Input.Text = string.Empty;
                    txt_Fail_Input.Focus();
                }
            }
        }
    }
}
