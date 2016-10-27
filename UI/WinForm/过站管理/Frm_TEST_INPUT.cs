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
using System.Diagnostics;

namespace SFIS_V2
{
    public partial class Frm_TEST_INPUT : Office2007Form //Form
    {
        public Frm_TEST_INPUT(Frm_Station_Management Frm)
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
        bool RePrintFlag = false;

        private readonly string IniFilePath = "C:\\SFIS\\SFIS.ini";
        /// <summary>
        /// 打印模板路径
        /// </summary>
        string labfilefullpath = string.Empty;
        string LabDir = string.Empty;

        FrmBLL.ClsCodeSoft7 ccs = new FrmBLL.ClsCodeSoft7();

        private void ReadIniFile()
        {
            MyLine = FrmBLL.ReadIniFile.IniReadValue("TEST_INPUT", "LINE", IniFilePath);
            MYGROUP = FrmBLL.ReadIniFile.IniReadValue("TEST_INPUT", "MYGROUP", IniFilePath);
            MyStation = FrmBLL.ReadIniFile.IniReadValue("TEST_INPUT", "STATION", IniFilePath);
            MySection = FrmBLL.ReadIniFile.IniReadValue("TEST_INPUT", "SECTION", IniFilePath);
            LabDir = FrmBLL.ReadIniFile.IniReadValue("TEST_INPUT", "Patch", IniFilePath);
            if (string.IsNullOrEmpty(LabDir))
            {
                LabDir = "D:";
                FrmBLL.ReadIniFile.IniWriteValue("TEST_INPUT", "Patch", LabDir, IniFilePath);
            }
        }
        public void SetStation()
        {
            LabStation.Text = MyLine + " : " + MYGROUP;
            FrmBLL.ReadIniFile.IniWriteValue("TEST_INPUT", "LINE", MyLine, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("TEST_INPUT", "MYGROUP", MYGROUP, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("TEST_INPUT", "STATION", MyStation, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("TEST_INPUT", "SECTION", MySection, IniFilePath);
        }
        private void SendMsgError(string Msg)
        {
            mFrm.ShowMsg(Frm_Station_Management.mLogMsgType.Error, Msg);
        }
        private void SendMsgOutgoing(string Msg)
        {
            mFrm.ShowMsg(Frm_Station_Management.mLogMsgType.Outgoing, Msg);
        }
        private void SendMsgPass(string Msg)
        {
            mFrm.ShowMsg(Frm_Station_Management.mLogMsgType.Incoming, Msg);
        }
        /// <summary>
        /// 检查系统进程中是否存在指定的进程
        /// </summary>
        /// <param name="prcname">进程名称</param>
        /// <returns>存在则返回真</returns>
        private bool checkprocessisrun(string prcname)
        {
            Process[] prc = Process.GetProcessesByName(prcname);
            if (prc.Length < 1)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 杀死进程(目前写死为lppa.exe)
        /// </summary>
        private void KillAllProcess()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Arguments = "/c taskkill /im lppa.exe /f";
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.Start();
            cmd.Close();
        }
        private void Frm_TEST_INPUT_Load(object sender, EventArgs e)
        {
            if (checkprocessisrun("lppa"))
            {
                if (MessageBoxEx.Show("检测到有打开的模板文件 \n\n 请先关闭模板文件再运行程序,否则可能导致程序出错!! \n\n 关闭模板文件选择[Yes] 否则选择[NO]", "提示!!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    KillAllProcess();
                }
            }
            try
            {
                SendMsgOutgoing("正在初始化....");
                ccs.OpenCodeSoft();
                My_UserId = mFrm.UserId;
                My_Password = mFrm.Password;

                ReadIniFile();
                SetStation();
                txt_InputData.Focus();
                ClearLabel();
                SendMsgPass("初始化完成....");
                SendMsgOutgoing("条码模板变量是[ESN,SN,MAC]");
            }
            catch (Exception ex)
            {
                SendMsgError("初始化失败:" + ex.Message);
            }
        }
        private void ClearLabel()
        {
            LabwoId.Text = string.Empty;
            LabPartnumber.Text = string.Empty;
            LabModelDesc.Text = string.Empty;
            LabInputQty.Text = "0";
            LabTargetQty.Text = "0";
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
                        RePrintFlag = false;
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
                if (My_InputGroup == MYGROUP)
                {
                    string C_RES = string.Empty;
                    if (!RePrintFlag)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("DATA", txt_sn.Text);
                        dic.Add("LINE", MyLine);
                        dic.Add("MYGROUP", MYGROUP);
                        dic.Add("SECTION_NAME", MySection);
                        dic.Add("STATION_NAME", MyStation);
                        dic.Add("WO", My_MoNumber);
                        dic.Add("EMP", My_UserId + "-" + My_Password);
                        C_RES = refWebProcedure.Instance.ExecuteProcedure("PRO_TEST_INPUT_ALL", FrmBLL.ReleaseData.DictionaryToJson(dic));

                        if (C_RES != "OK")
                        {
                            SendMsgError(C_RES);
                            return;
                        }
                        else
                        {
                            LabInputQty.Text = (Convert.ToInt32(LabInputQty.Text) + 1).ToString();
                            LabInputQty.Update();
                            SendMsgPass(string.Format("SN[{0}] {1}", txt_sn.Text, C_RES));
                            PrintLabel(txt_sn.Text);
                        }
                    }
                    else
                    {
                        PrintLabel(txt_sn.Text);
                        RePrintFlag = false;
                        SendMsgOutgoing("重复打印关闭");
                    }

                }
                else
                {
                    SendMsgError(string.Format("ERROR:选择途程[{0}]与工单投入途程[{1}]不一致", MYGROUP, My_InputGroup));
                }
            }
        }


        private void PrintLabel(string SN)
        {
            try
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.Get_WIP_TRACKING("ESN", SN, "ESN,SN,MAC"));
                if (dt.Rows.Count > 0)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("ESN", dt.Rows[0]["ESN"].ToString());
                    dic.Add("SN", dt.Rows[0]["SN"].ToString());
                    dic.Add("MAC", dt.Rows[0]["MAC"].ToString());
                    if (string.IsNullOrEmpty(labfilefullpath))
                        labfilefullpath = string.Format(@"{0}\{1}\{2}", this.LabDir.IndexOf(":") != -1 ? this.LabDir : this.LabDir + ":", My_MoNumber, MYGROUP + ".lab");
                    ccs.CheckFileExist(labfilefullpath);
                    ccs.OpenLabelFile(labfilefullpath, false);
                    ccs.Fill_Label_Variables(dic);
                    ccs.SetPrintNum(Convert.ToInt32(NumPrintQty.Value));
                    ccs.PrintLabel();
                    SendMsgPass("打印完成");
                }
            }
            catch (Exception ex)
            {
                SendMsgError(ex.Message);
            }

        }

        private void Frm_TEST_INPUT_FormClosing(object sender, FormClosingEventArgs e)
        {
            ccs.QuitCodeSoft();
        }

        private void imbt_SelectLine_Click(object sender, EventArgs e)
        {
            Frm_StationName fsm = new Frm_StationName(this);
            fsm.ShowDialog();
        }

        private void imbt_Reprint_Click(object sender, EventArgs e)
        {
            RePrintFlag = true;
            SendMsgOutgoing("重复打印打开");
        }

        private void Frm_TEST_INPUT_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void imbt_LabelPatch_Click(object sender, EventArgs e)
        {

            string[] EmpData = Input.InputBox.ShowInputBox("输入权限", string.Empty);
            try
            {
                string UserId = EmpData[0];
                string PWD = EmpData[1];
                string _StrErr = refWebtUserInfo.Instance.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);
                if (_StrErr == "OK")
                {

                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "选择打印模板";
                    ofd.Filter = "(*.lab Labels)|*.lab";
                    DialogResult dlr = ofd.ShowDialog();
                    if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
                    {
                        // string filename = ofd.SafeFileName;
                        labfilefullpath = ofd.FileName;
                        SendMsgOutgoing("选择打印模板路径成功");
                        SendMsgOutgoing(labfilefullpath);
                    }
                    else
                    {
                        labfilefullpath = string.Empty;
                        SendMsgOutgoing("未选择模板,恢复默认路径");
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
