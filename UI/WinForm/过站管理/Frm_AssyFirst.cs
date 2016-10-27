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
using System.IO;

namespace SFIS_V2
{
    public partial class Frm_AssyFirst : Office2007Form //Form
    {
        public Frm_AssyFirst(Frm_Station_Management Frm)
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
        private readonly string IniFilePath = "C:\\SFIS\\SFIS.ini";

        string LabDir = string.Empty;

        /// <summary>
        /// 打印模板路径
        /// </summary>
        string LabFilePatch = string.Empty;
        /// <summary>
        /// 重复打印标记
        /// </summary>
        bool My_RePrint = false;

        string My_InputGroup = string.Empty;
        string My_MoNumber = string.Empty;
        bool sMoFlag = false;
        bool sEsnFlag = false;

        FrmBLL.ClsCodeSoft7 cs7 = new FrmBLL.ClsCodeSoft7();

        public void SetStation()
        {
            LabStation.Text = MyLine + " : " + MYGROUP;
            FrmBLL.ReadIniFile.IniWriteValue("ASSY_FIRST", "LINE", MyLine, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("ASSY_FIRST", "MYGROUP", MYGROUP, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("ASSY_FIRST", "STATION", MyStation, IniFilePath);
            FrmBLL.ReadIniFile.IniWriteValue("ASSY_FIRST", "SECTION", MySection, IniFilePath);
        }

        private void Frm_AssyInput_Load(object sender, EventArgs e)
        {

        }

        private void imbt_SelectLine_Click(object sender, EventArgs e)
        {
            Frm_StationName fsm = new Frm_StationName(this);
            fsm.ShowDialog();
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
            MyLine = FrmBLL.ReadIniFile.IniReadValue("ASSY_FIRST", "LINE", IniFilePath);
            MYGROUP = FrmBLL.ReadIniFile.IniReadValue("ASSY_FIRST", "MYGROUP", IniFilePath);
            MyStation = FrmBLL.ReadIniFile.IniReadValue("ASSY_FIRST", "STATION", IniFilePath);
            MySection = FrmBLL.ReadIniFile.IniReadValue("ASSY_FIRST", "SECTION", IniFilePath);
            LabDir = FrmBLL.ReadIniFile.IniReadValue("ASSY_FIRST", "Patch", IniFilePath);
            NumPrintQty.Value = Convert.ToDecimal(string.IsNullOrEmpty(FrmBLL.ReadIniFile.IniReadValue("ASSY_FIRST", "PrintQTY", IniFilePath)) ? "0" : FrmBLL.ReadIniFile.IniReadValue("ASSY_FIRST", "PrintQTY", IniFilePath));
            if (string.IsNullOrEmpty(LabDir))
            {
                LabDir = "D:";
                FrmBLL.ReadIniFile.IniWriteValue("ASSY_FIRST", "Patch", LabDir, IniFilePath);
            }
        }
        private void Frm_AssyFirst_Load(object sender, EventArgs e)
        {
            SendMsgOutgoing("正在初始化.....");
            ClearLabel();
            ReadIniFile();
            SetStation();
            My_UserId = mFrm.UserId;
            My_Password = mFrm.Password;
            cs7.OpenCodeSoft();
            SendMsgPass("初始化完成....");
            SendMsgPass("步骤1: 工单  备注: 工单只输入一次.");
            SendMsgPass("步骤2: ESN,KPESN");
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

        private void txt_kpesn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                sEsnFlag = false;//修改KPSN失败后回到SN重刷 20160330

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("DATA", txt_kpesn.Text.ToUpper().Trim().ToString());
                dic.Add("SN", txt_sn.Text.ToUpper().Trim().ToString());
                dic.Add("WO", LabwoId.Text);
                string ChkRes = refWebProcedure.Instance.ExecuteProcedure("PRO_CHECK_KPS_VALID", FrmBLL.ReleaseData.DictionaryToJson(dic));
                if (ChkRes == "OK")
                {
                    INSERT_KEYPARTS();
                }
                else
                {
                    SendMsgError("输入的KPSN[" + txt_kpesn.Text.ToUpper().Trim().ToString() + "] 异常->" + change_messge(ChkRes));
                    return;
                }
            }
        }

        private void txt_sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("DATA", txt_sn.Text.ToUpper().Trim().ToString());
                dic.Add("MYGROUP", MYGROUP);
                dic.Add("LINE", MyLine);
                dic.Add("WO", LabwoId.Text);
                dic.Add("EMP", My_UserId + "-" + My_Password);
                string ChkRes = refWebProcedure.Instance.ExecuteProcedure("PRO_SN_INPUT_WIPFIRST", FrmBLL.ReleaseData.DictionaryToJson(dic));
                if (ChkRes == "OK")
                {
                    if (CHECKROUTE() == "OK")
                        sEsnFlag = true;
                }
                else
                {
                    SendMsgError("输入的SN: " + txt_sn.Text.ToUpper().Trim().ToString() + " 异常->" + ChkRes);
                    txt_sn.Focus();
                    txt_sn.SelectAll();
                    return;
                }
            }
        }
        public string CHECKROUTE()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", txt_sn.Text.ToUpper().Trim().ToString());
            dic.Add("MYGROUP", MYGROUP);

            string ChkRes = refWebProcedure.Instance.ExecuteProcedure("PRO_CHECKROUTE", FrmBLL.ReleaseData.DictionaryToJson(dic));
            if (ChkRes == "OK")
                SendMsgPass("SN:" + txt_sn.Text.ToUpper().Trim().ToString() + "  输入OK,请刷入KPSN....");
            else
                SendMsgError("流程检查错误！！->" + ChkRes);
            return ChkRes;
        }
        private void INSERT_KEYPARTS()
        {

           
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", txt_kpesn.Text.ToUpper().Trim().ToString());
            dic.Add("SN", txt_sn.Text.ToUpper().Trim().ToString());
            dic.Add("MYGROUP", MYGROUP);
            dic.Add("EMP", My_UserId + "-" + My_Password);
            dic.Add("LINE", MyLine);
            dic.Add("WO", LabwoId.Text);

            string ChkRes = refWebProcedure.Instance.ExecuteProcedure("PRO_INSERT_KEYPARTS", FrmBLL.ReleaseData.DictionaryToJson(dic));
            // SaveTxtLog(System.Environment.CurrentDirectory + "\\DCT_INPUT", "KPESN", ChkRes, txt_kpesn.Text.ToUpper().Trim().ToString());
            if (ChkRes == "OK")
            {
                LabInputQty.Text = (Convert.ToInt32(LabInputQty.Text) + 1).ToString();
                SendMsgPass("KPSN:" + txt_kpesn.Text.ToUpper().Trim().ToString() + " OK,请刷入下一个SN……");
               // sEsnFlag = false;//修改KPSN失败后回到SN重刷 20160330
                PrintLabel(txt_sn.Text);
            }
            else
            {
                SendMsgError("KPSN:" + txt_kpesn.Text.ToUpper().Trim().ToString() + " 写入系统失败！！->" + change_messge(ChkRes));
                return;
            }
        }

        public string change_messge(string ChkRes)
        {
            string c_messge = "";
            switch (ChkRes)
            {
                case "NO SN FOUND":
                    c_messge = "KPSN不存在";
                    break;
                case "KPS DUP":
                    c_messge = "资料重复，KPSN已经被绑定";
                    break;
                case "SN HAVE KPS":
                    c_messge = "SN已经绑定过KPSN";
                    break;
                case "THE SAME TWO WO":
                    c_messge = "KPSN存在两笔工单中";
                    break;
                case "SN IN REPAIR":
                    c_messge = "KPSN在维修中";
                    break;
                case "KPS ROUTE NOT END":
                    c_messge = "KPSN的流程未结束";
                    break;
                default:
                    c_messge = ChkRes;
                    break;
            }
            return c_messge;
        }

        public void PrintLabel(string Esn)
        {

            if (Convert.ToInt32(NumPrintQty.Value) == 0)
                return;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyPart(Esn));
            if (dt.Rows.Count > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("ESN", dt.Rows[0]["ESN"].ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    dic.Add(dr["SNTYPE"].ToString(), dr["SNVAL"].ToString());
                }
                PublicPrintLabel(dic);

            }
            else
            {
                SendMsgError("没有需要打印的数据");
            }

            if (My_RePrint)
            {
                My_RePrint = false;
                SendMsgOutgoing("重复打印关闭");
            }

        }
        public void PublicPrintLabel(Dictionary<string, object> PrintDic)
        {
            try
            {
                if (string.IsNullOrEmpty(LabFilePatch))
                    LabFilePatch = string.Format(@"{0}\{1}\{2}", LabDir.IndexOf(":") != -1 ? LabDir : LabDir + ":", LabwoId.Text, MYGROUP + ".lab");

                cs7.CheckFileExist(LabFilePatch);
                cs7.OpenLabelFile(LabFilePatch, false);
                cs7.ClearVariables();
                cs7.Fill_Label_Variables(PrintDic);
                //  cs7.SetPrintPosition(numX.Value, numY.Value);
                cs7.SetPrintNum(Convert.ToInt32(NumPrintQty.Value));
                cs7.PrintLabel();
            }
            catch (Exception ex)
            {
                SendMsgError(ex.Message);
            }

            ////  StripStatusLabelPatch.Text = "Label File: " + filepatch;
            //string LabDir =FrmBLL.ReadIniFile.IniReadValue("ASSY_FIRST", "Patch", IniFilePath);
            //string filepatch = string.Format(@"{0}\{1}\{2}", LabDir.IndexOf(":") != -1 ? LabDir : LabDir + ":", LabwoId.Text, MYGROUP + ".lab");

            //string PrintQty = FrmBLL.ReadIniFile.IniReadValue("TEST_INPUT", "LabelQty", IniFilePath);
            //string
            //    coordinateX = FrmBLL.ReadIniFile.IniReadValue("TEST_INPUT", "LabelX", IniFilePath);
            //string coordinateY = FrmBLL.ReadIniFile.IniReadValue("TEST_INPUT", "LabelY", IniFilePath);

            //if (!File.Exists(filepatch))  //判断条码文件是否存在
            //{
            //  SendMsgError( "条码档没有找到,路径:" + filepatch);
            //    return;
            //}

            //try
            //{

            //    lbl.Documents.Open(filepatch, false);// 调用设计好的label文件
            //    Document doc = lbl.ActiveDocument;
            //    SendPrgMsg(mLogMsgType.Incoming, "清空模板变量...");
            //    for (int i = 0; i < doc.Variables.FormVariables.Count; i++)
            //    {
            //        doc.Variables.FormVariables.Item(doc.Variables.FormVariables.Item(i + 1).Name).Value = "";
            //    }
            //    SendPrgMsg(mLogMsgType.Incoming, string.Format("模板变量清空完成,共计{0}个...", doc.Variables.FormVariables.Count));
            //    foreach (KeyValuePair<string, string> _DicKeyValues in dic)
            //    {
            //        try
            //        {
            //            doc.Variables.FormVariables.Item(_DicKeyValues.Key).Value = _DicKeyValues.Value; //给参数传值                     
            //            SendPrgMsg(mLogMsgType.Outgoing, string.Format("填充打印变量完成:{0}->{1}", _DicKeyValues.Key, _DicKeyValues.Value));
            //        }
            //        catch
            //        {
            //        }
            //    }

            //    int Num = Convert.ToInt32(PrintQty);        //打印数量
            //    doc.Format.MarginLeft = (Convert.ToInt32(coordinateX)) * 100;
            //    doc.Format.MarginTop = (Convert.ToInt32(coordinateY)) * 100;
            //    doc.PrintDocument(Num);               //打印
            //    SendPrgMsg(mLogMsgType.Normal, "打印完成");
            //}
            //catch (Exception ex)
            //{
            //    SendPrgMsg(mLogMsgType.Error, "发生异常" + ex.Message);
            //}
            //finally
            //{
            //    // lbl.Quit(); //退出
            //}

        }

        private void Frm_AssyFirst_FormClosing(object sender, FormClosingEventArgs e)
        {
            cs7.QuitCodeSoft();
            FrmBLL.ReadIniFile.IniWriteValue("ASSY_FIRST", "PrintQTY", NumPrintQty.Value.ToString(), IniFilePath);
        }

        private void txt_InputData_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_InputData.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    string InputData = txt_InputData.Text.Trim().ToUpper();
                    if (string.IsNullOrEmpty(MyLine))
                        throw new Exception("请选择线别");

                    if (InputData == "UNDO")
                    {
                        sMoFlag = false;
                        sEsnFlag = false;
                        ClearLabel();
                        SendMsgPass("UNDO OK");
                        return;
                    }
                    if (!sMoFlag)
                    {
                        txt_woId.Text = InputData;
                        txt_woId_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    }
                    else
                    {
                        if (My_RePrint)
                        {
                            PrintLabel(InputData);
                        }
                        else
                        {
                            if (!sEsnFlag)
                            {
                                txt_sn.Text = InputData;
                                txt_sn_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                            }
                            else
                            {
                                txt_kpesn.Text = InputData;
                                txt_kpesn_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    SendMsgError(ex.Message);
                }
                finally
                {
                    txt_InputData.Text = string.Empty;
                }

            }
        }

        private void imbt_Reprint_Click(object sender, EventArgs e)
        {
            My_RePrint = true;
            SendMsgOutgoing("重复打印打开");
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
                        LabFilePatch = ofd.FileName;
                        SendMsgWarning("选择打印模板路径成功");
                        SendMsgOutgoing(LabFilePatch);
                    }
                    else
                    {
                        LabFilePatch = string.Empty;
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
