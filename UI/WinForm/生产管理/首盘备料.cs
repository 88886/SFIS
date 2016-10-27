using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LabelManager2;
using System.IO;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Drawing.Printing;
using System.Threading;
using System.Net;

namespace SFIS_V2
{
    public partial class Material_Monitor : Office2007Form// Form
    {
        public Material_Monitor(MainParent Msg)
        {
            InitializeComponent();
            sInfo = Msg;


        }

        MainParent sInfo;

        string sSEQ = "";
        string sStationList = "";
        string sMO = "";
        string MachineCode = "";
        string sSIDE = "";
        string sSTATUS = "";
        string sSation = "";
        int KpMoCount = 0;
        int MonitorCount = 0;
        Dictionary<string, object> dic = null;
        ApplicationClass lbl; //= new ApplicationClass();
        string filepatch = Directory.GetCurrentDirectory() + "\\LabelFile\\STATION.lab";
        public readonly bool Check_Mo = true;
        Thread th = null;

        System.Windows.Forms.Timer TimeCom = new System.Windows.Forms.Timer();
        private void Material_Monitor_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.sInfo.gUserInfo.rolecaption == "系统开发员")
            {
                IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PROGID", this.Name);
                dic.Add("PROGNAME", this.Text);
                dic.Add("PROGDESC", this.Text);
                FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
            }
            #endregion
            lbl = new ApplicationClass();

            IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            txtServerIP.Text = ipAddr.ToString();
            this.onDownLoadList += new dDownloadList(FrmPallet_onDownLoadList);
            this.onDownLoadList1 += new dDownloadList1(FrmPallet_onDownLoadList1);
        }
        private void UndoInfo()
        {
            sSEQ = "";
            sMO = "";
            MachineCode = "";
            sSIDE = "";
            sSTATUS = "";
            sStationList = "";
            sSation = "";
            KpMoCount = 0;
            MonitorCount = 0;
            listBox1.Items.Clear();
            palcount.Text = "0";
            dataGridViewX1.DataSource = "";
            //  sInfo.ShowPrgMsg("UNDO OK,请刷入料表", MainParent.MsgType.Incoming);
            SendMsgToClient("UNDO OK,请刷入料表", "UNDO OK,Please Input SEQ_WO");
            ed_Scan.SelectAll();
            listBox1.Items.Clear();
        }

        private void ed_Scan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(ed_Scan.Text) && e.KeyChar == 13)
            {
                try
                {
                    if (tsm_todb.Checked)
                    {
                        string[] txtwobom = ed_Scan.Text.Trim().Split(' ');
                     string _StrErr= refWebtMaterialPreparation.Instance.InsertMaterialPreparation_1(
                            txtwobom[0], txtwobom[1], sInfo.gUserInfo.userId);
                     if (_StrErr == "OK")
                     {
                         sInfo.ShowPrgMsg(string.Format("料站表:[{0}]插入数据成功!", ed_Scan.Text), MainParent.MsgType.Incoming);
                         SendMsgToClient(string.Format("料站表:[{0}]插入数据成功!", ed_Scan.Text), "Stock Table OK");
                         ed_Scan.SelectAll();
                     }
                     else
                     {
                         throw new Exception(_StrErr);
                     }
                    }
                    else
                    {
                        #region 首盘备料
                        if (ed_Scan.Text.Trim() == "UNDO")
                        {
                            UndoInfo();
                        }
                        else
                            if (sSEQ == "")
                            {
                                listBox1.Items.Clear();
                                try
                                {
                                    string str = ed_Scan.Text.Trim();
                                    int i = str.IndexOf(' ');
                                    sSEQ = str.Substring(0, i);
                                    sMO = str.Substring(i + 1, str.Length - i - 1);
                                }
                                catch (Exception)
                                {
                                    SendMsgToClient("料站表刷入错误,请重新刷入料站表 ", "ERROR:SEQ WO ERROR");
                                    ed_Scan.Focus();
                                    ed_Scan.SelectAll();
                                    sSEQ = "";
                                    return;
                                }                             
                                DataTable data = FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.GetSmtKpMaster(sSEQ));

                                if (data.Rows.Count == 0)
                                {
                                    SendMsgToClient("ECN 错误,请刷入正确的料表", "ERROR: ECN ERROR");
                                    ed_Scan.SelectAll();
                                    sSEQ = "";
                                    return;
                                }
                                else
                                {
                                    MachineCode = data.Rows[0][1].ToString();
                                    sSIDE = data.Rows[0][6].ToString();
                                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtIO.Instance.GetSmtIO(sSEQ, sMO));
                                    if (dt.Rows.Count == 0)
                                    {

                                        dic = new Dictionary<string, object>();
                                        dic.Add("MASTERID", sSEQ);
                                        dic.Add("WOID", sMO);
                                        dic.Add("USERID", sInfo.gUserInfo.userId);
                                        dic.Add("MACHINEID", MachineCode);
                                        dic.Add("STATUS", "0");
                                        dic.Add("SIDE", sSIDE);
                                        refWebSmtIO.Instance.InserSmtIo(FrmBLL.ReleaseData.DictionaryToJson(dic));                                     

                                    }
                                    else
                                    {
                                        sSTATUS = dt.Rows[0]["status"].ToString();
                                        string _StrErr = "ERROR";
                                        switch (sSTATUS)
                                        {
                                            case "0":
                                                _StrErr = "OK";
                                                break;
                                            case "1":
                                                SendMsgToClient("该料站表已经备完料,请刷入未备料料表 ", "ERROR: This SEQ WO Is Finished");
                                                sSEQ = "";
                                                ed_Scan.SelectAll();                                               
                                                break;
                                            case "2":
                                                SendMsgToClient("该料站表正在换线中,不可再做备料 ", "ERROR: This SEQ WO CHGLINE...");
                                                sSEQ = "";
                                                ed_Scan.SelectAll();
                                                break;
                                            case "3":
                                                SendMsgToClient("该料站表已经刷完换线,不可再做备料 ", "ERROR: This SEQ WO Already CHGLINE");
                                                sSEQ = "";
                                                ed_Scan.SelectAll();
                                                break;
                                            case "4":
                                                SendMsgToClient("该料站表已经下线,不可再做备料 ", "ERROR: This SEQ WO Off Line");
                                                sSEQ = "";
                                                ed_Scan.SelectAll();
                                                break;
                                            default:
                                                SendMsgToClient("该料站表状态异常 ", "ERROR: This SEQ WO Exception");
                                                sSEQ = "";
                                                ed_Scan.SelectAll();
                                                break;
                                        }

                                        if (_StrErr != "OK")
                                        {
                                            return;
                                        }
                                    }

                                    dic = new Dictionary<string, object>();
                                    dic.Add("MASTERID", sSEQ);
                                    dic.Add("WOID", sMO);
                                    dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtMaterialPreparation.Instance.GetMaterialPreparationKpAndStation(FrmBLL.ReleaseData.DictionaryToJson(dic)));
                                   
                                    DataTable da = FrmBLL.ReleaseData.arrByteToDataTable(refWebtMaterialPreparation.Instance.GetMaterialPreparationStation(FrmBLL.ReleaseData.DictionaryToJson(dic)));
                                   
                                    KpMoCount = da.Rows.Count;
                                    DataTable db = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpMonitor.Instance.GetSmtkPMonnitorStation(sSEQ, sMO));

                                    MonitorCount = db.Rows.Count;

                                    if (MonitorCount != 0)
                                    {
                                        for (int x = 0; x < db.Rows.Count; x++)
                                        {
                                            DeleteStation(db.Rows[x][0].ToString());
                                        }
                                    }
                                    palcount.Text = (KpMoCount - MonitorCount).ToString();
                                    SendMsgToClient("ECN OK,请刷入唯一条码.", "ECN OK,Please Input SEQ WO");
                                    ed_Scan.SelectAll();
                                }
                            }
                            else
                            {
                                string trsn = ed_Scan.Text.Trim();
                                if (GetTrsnMaterial(trsn))
                                {

                                    string PartNo = GetString(ed_Scan.Text, 1);
                                    string VenderCode = GetString(ed_Scan.Text, 2);
                                    string DateCode = GetString(ed_Scan.Text, 3);
                                    string LotCode = GetString(ed_Scan.Text, 4);
                                    string QTY = GetString(ed_Scan.Text, 5);
                                    ed_Scan.Text = trsn;
                                    if ((PartNo != "") && (VenderCode != "") && (DateCode != "") && (LotCode != "") && (QTY != ""))
                                    {
                                        //确认清仓物料
                                        if (Check_MaterialScrap(PartNo, VenderCode, DateCode, LotCode))
                                        {
                                            Dictionary<string,object> dic = new Dictionary<string,object>();
                                            dic.Add("MASTERID",sSEQ);
                                            dic.Add("WOID",sMO);
                                            dic.Add("KPNUMBER",PartNo);
                                            DataTable dc = FrmBLL.ReleaseData.arrByteToDataTable(refWebtMaterialPreparation.Instance.GetMaterialPreparation(FrmBLL.ReleaseData.DictionaryToJson(dic)));
                                                                                      
                                            if (dc.Rows.Count != 0)
                                            {
                                                int mm = 0;
                                                for (int x = 0; x < dc.Rows.Count; x++)
                                                {
                                                    mm++;

                                                    sSation = dc.Rows[x][3].ToString();

                                                    #region 卡料号 2013.10.30
                                                    //false:不可备料true:可备料
                                                    bool sGroupFlag = true;
                                                    bool sPriorFlag = true;
                                                    //string sqls = "select *  from tMaterialPreparation_1 where woId=sMO and masterId=sSEQ and stationno=sSation";
                                                    DataTable dg = FrmBLL.ReleaseData.arrByteToDataTable(
                                                        refWebtMaterialPreparation.Instance.GetMaterialPreByStation_1(sMO, sSEQ, sSation));
                                                    if (dg == null || dg.Rows.Count < 1)
                                                    {
                                                        sInfo.ShowPrgMsg("请先导入工单料表,再备料...", MainParent.MsgType.Outgoing);
                                                        ed_Scan.SelectAll();
                                                        return;
                                                    }
                                                    if (dg.Rows[0]["stationnum"].ToString() != "1")
                                                    {
                                                        int yy = 0;
                                                        for (int i = 0; i < dg.Rows.Count; i++)
                                                        {
                                                            DataTable dtt = FrmBLL.ReleaseData.arrByteToDataTable(
                                                                refWebtMaterialPreparation.Instance.GetMaterialPreByKpnumber_1(dg.Rows[i]["kpnumber"].ToString(), sMO)); ;
                                                            //刷入的料号
                                                            if (dg.Rows[i]["kpnumber"].ToString() == PartNo)
                                                            {
                                                                if (dtt == null || dtt.Rows.Count <= 1)
                                                                    break;
                                                                if (dtt.Rows.Count > 1)
                                                                {
                                                                    foreach (DataRow idr in dtt.Rows)
                                                                    {
                                                                        if (idr["stationnum"].ToString() == "1")
                                                                        {
                                                                            yy++;
                                                                            sGroupFlag = false;
                                                                            sPriorFlag = false;
                                                                            break;
                                                                        }
                                                                    }
                                                                    sPriorFlag = true;
                                                                    break;
                                                                }
                                                            }
                                                            //刷入料号的替代料
                                                            else
                                                            {
                                                                if (dtt == null || dtt.Rows.Count <= 1)
                                                                {
                                                                    sGroupFlag = true;
                                                                    break;
                                                                }
                                                                if (dtt.Rows.Count > 1)
                                                                {
                                                                    foreach (DataRow idr in dtt.Rows)
                                                                    {
                                                                        if (idr["stationnum"].ToString() == "1")
                                                                        {
                                                                            yy++;
                                                                            sGroupFlag = false;
                                                                            sPriorFlag = false;
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        if (yy == dg.Rows.Count)//表示同级别料
                                                        {
                                                            sGroupFlag = true;
                                                        }
                                                    }
                                                    if ((!sGroupFlag) && !(sPriorFlag))
                                                        continue;
                                                    # endregion
                                                
                                                    DataTable dd = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpMonitor.Instance.ChkStationInMonitor(sSEQ, sMO, sSation));

                                                    if (dd.Rows.Count == 0)
                                                    {
                                                        dic = new Dictionary<string, object>();
                                                        dic.Add("MASTERID", sSEQ);
                                                        dic.Add("WOID", sMO);
                                                        dic.Add("MACHINEID", MachineCode);
                                                        dic.Add("STATIONNO", sSation);
                                                        dic.Add("CDATA", 3);
                                                        dic.Add("KPNUMBER", PartNo);
                                                        dic.Add("SUPPLYUSER", sInfo.gUserInfo.userId);
                                                        dic.Add("FLAG", 0);
                                                        dic.Add("QTY", QTY );
                                                        dic.Add("VENDERCODE", VenderCode);
                                                        dic.Add("DATECODE", DateCode);
                                                        dic.Add("LOTID", LotCode);
                                                        dic.Add("TRSN", trsn);
                                                        refWebtSmtKpMonitor.Instance.InsertSmtIoData(FrmBLL.ReleaseData.DictionaryToJson(dic));                                                     

                                                        DeleteStation(sSation);
                                                        palcount.Text = (Convert.ToInt32(palcount.Text) - 1).ToString();
                                                        if (labelprint.Checked)
                                                        {
                                                            priner_station_label(sSation);
                                                        }
                                                       string _StrErr= refWebtR_Tr_Sn.Instance.Update_TR_SN(trsn, "NA", sInfo.gUserInfo.userId, "3", "NA", "NA");
                                                       if (_StrErr != "OK")
                                                       {
                                                           throw new Exception("更改Tr_Sn状态失败:" + _StrErr);
                                                       }
                                                        SendMsgToClient("OK,请扫描下一盘料", "OK,Scan Next");
                                                        ed_Scan.SelectAll();

                                                        if (Convert.ToInt32(palcount.Text) <= 0)
                                                        {

                                                            dic = new Dictionary<string, object>();
                                                            dic.Add("MASTERID", sSEQ);
                                                            dic.Add("WOID", sMO);
                                                            dic.Add("STATUS", "1");
                                                            refWebSmtIO.Instance.UpdateSmtIOStatus(FrmBLL.ReleaseData.DictionaryToJson(dic));
                                                          
                                                            UndoInfo();
                                                            SendMsgToClient("首盘备料完成,请扫描下一套料表", "Finished,Scan Next SEQ WO");
                                                            ed_Scan.SelectAll();
                                                        }
                                                        return;
                                                    }
                                                }
                                                if (mm >= dc.Rows.Count)
                                                {
                                                    SendMsgToClient("此颗料已经备好---->" + PartNo, "ERROR,This Parts Already Stock Finished");
                                                    ed_Scan.SelectAll();
                                                }

                                            }
                                            else
                                            {
                                                SendMsgToClient("料站表内无此颗物料或已经备好,请确认---->" + PartNo, "ERROR: No Parts or Stock Finished");
                                                ed_Scan.SelectAll();
                                            }
                                        }
                                        else
                                        {
                                            SendMsgToClient("此物料为清仓物料,不能使用", "ERROR: Scrap Parts");
                                            ed_Scan.SelectAll();
                                        }

                                    }
                                    else
                                    {
                                        SendMsgToClient("五合一条码格式错误.", "ERROR: This Label Format Error");
                                        ed_Scan.SelectAll();
                                    }
                                }
                            }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("出现异常  "+ex.Message);
                }
                finally
                {
                    ed_Scan.Text = "";
                }

            }
           
        }

        private void ed_reprintstation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(ed_reprintstation.Text) && e.KeyChar == 13)
            {
                priner_station_label(ed_reprintstation.Text.Trim());
                ed_reprintstation.Text = string.Empty;
            }
        }

        public bool GetTrsnMaterial(string trsn)
        {         
            string _StrErr=string.Empty;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_Tr_Sn_Info(trsn,out _StrErr));
            try
            {
                if (dt.Rows[0]["STATUS"].ToString() != "2")
                {
                    SendMsgToClient("此盘物料不在线边仓或已经备好,请确认......", "ERROR: This Parts Not In Line Side Storehouse");
                    ed_Scan.SelectAll();
                    return false;
                }
                if (Check_Mo)
                {
                    if (dt.Rows[0]["WOID"].ToString() != sMO)
                    {
                        SendMsgToClient(string.Format("物料工单不同,{0}≠{1}", dt.Rows[0]["WOID"].ToString(), sMO), string.Format("WO Different,{0}≠{1}", dt.Rows[0]["WOID"].ToString(), sMO));
                        ed_Scan.SelectAll();
                        return false;
                    }
                }



                ed_Scan.Text = dt.Rows[0]["KP_NO"].ToString() + "|" + dt.Rows[0]["VENDER_ID"].ToString() + "|" + dt.Rows[0]["DATE_CODE"].ToString() + "|" + dt.Rows[0]["LOT_CODE"].ToString() + "|" + dt.Rows[0]["QTY"].ToString();
                ed_Scan.SelectAll();
                return true;
            }
            catch
            {        
                SendMsgToClient("唯一条码错误,请刷正确的唯一条码", "ERROR: Trsn Error");
                ed_Scan.SelectAll();
                return false;
            }

        }
        private void priner_station_label(string PrinData)
        {

            if (!File.Exists(filepatch))  //判断条码文件是否存在
            {
                sInfo.ShowPrgMsg("条码档没有找到,请确认当前目录是否存在 STATION.lab", MainParent.MsgType.Error);
                return;
            }

            try
            {
                lbl.Documents.Open(filepatch, false);// 调用设计好的label文件
                Document doc = lbl.ActiveDocument;
                doc.Variables.FormVariables.Item("STATION").Value = PrinData; //给参数传值
                //     doc.Variables.FormVariables.Item("Var1").Value = txtContent2.Text.Trim(); //给参数传值

                int Num = Convert.ToInt32("1");        //打印数量
                doc.PrintDocument(Num);                             //打印
            }
            catch (Exception ex)
            {
                sInfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
            finally
            {
                //退出
            }
        }

        private void dataGridViewX1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridViewX1.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }

        }

        private void DeleteStation(string sStation)
        {

            for (int i = dataGridViewX1.Rows.Count - 1; i >= 0; i--)
            {
                if (dataGridViewX1[0, i].Value.ToString() == sStation)
                {
                    sStationList = sStationList + sStation + ";";
                    dataGridViewX1.Rows.RemoveAt(i);

                }

            }

            listBox1.Items.Add("已备料站: " + sStationList);
            //  palcount.Text = (dataGridViewX1.RowCount-1).ToString();
        }

        private string GetString(string temp, int i)
        {
            int x = i - 1;
            string s = temp.Trim();
            string sMsg = "";
            string[] sArray = s.Split('|');
            if ((sArray.Length - 1) != 4)
            {
                sMsg = "";
            }
            else
            {
                sMsg = sArray[x];
            }

            return sMsg;
        }

        /// <summary>
        /// 检查物料是否被清仓
        /// </summary>
        /// <param name="sPN"></param>
        /// <param name="sVC"></param>
        /// <param name="sDC"></param>
        /// <param name="sLC"></param>
        /// <returns></returns>
        private bool Check_MaterialScrap(string sPN, string sVC, string sDC, string sLC)
        {
          //  return FrmBLL.publicfuntion.Check_MaterialScrap(sPN, sVC, sDC, sLC);
          return  refWebtPartBlocked.Instance.Check_MaterialScrap(sPN, sVC, sDC, sLC);
        }

        private void Material_Monitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                lbl.Quit();
            }
            catch
            {
            }

        }

        private void chkcom_Click(object sender, EventArgs e)
        {
            //if (chkcom.Checked)
            //{
            //    rd.OpenCom();
            //    TimeCom.Enabled = true;              
            //}
            //else
            //{
            //    TimeCom.Enabled = false;
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ed_Scan.SelectAll();
            string sSEQMO = Input.InputQuery.ShowInputBox("请输入密码", string.Empty);
            if (string.IsNullOrEmpty(sSEQMO))
            {
                MessageBox.Show("输入资料为空,请重新输入");
                return;
            }
            if (MessageBox.Show("确定要删除备料信息吗???", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(ed_Scan.Text))
                {
                    try
                    {
                        string str = ed_Scan.Text.Trim();
                        int i = str.IndexOf(' ');
                        sSEQ = str.Substring(0, i);
                        sMO = str.Substring(i + 1, str.Length - i - 1);
                    }
                    catch (Exception)
                    {
                        sInfo.ShowPrgMsg("料站表刷入错误,请重新刷入料站表 ", MainParent.MsgType.Error);
                        ed_Scan.Focus();
                        ed_Scan.SelectAll();
                        sSEQ = "";
                        return;
                    }

                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtIO.Instance.GetSmtIO(sSEQ, sMO));
                    if (dt.Rows.Count != 0)
                    {
                        int sSatus = Convert.ToInt32(dt.Rows[0]["status"].ToString());

                        if (sSatus > 1)
                        {
                            sInfo.ShowPrgMsg("不可删除此份料站表 ", MainParent.MsgType.Error);
                            sSEQ = "";
                            ed_Scan.SelectAll();
                            return;
                        }

                        string[] TrsnList = refWebtSmtKpMonitor.Instance.GetMaterialTrsnList(sSEQ, sMO);
                        //  DataTable dtTrsn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpMonitor.Instance.GetSmtkPMonnitorStation(sSEQ, sMO));
                        if (TrsnList.Length != 0)
                        {
                            for (int x = 0; x < TrsnList.Length; x++)
                            {
                                if (!string.IsNullOrEmpty(TrsnList[x]))
                                  //  refWebtSmtKpMonitor.Instance.UpdateTrsnStatus(TrsnList[x], "1", sInfo.gUserInfo.userId);
                                refWebtR_Tr_Sn.Instance.Update_TR_SN(TrsnList[x], "NA", sInfo.gUserInfo.userId, "2", "NA", "NA");
                            }

                            refWebtSmtKpMonitor.Instance.DeleteSmtKpMonitor(sSEQ, sMO);
                            refWebSmtIO.Instance.DeleteSmtIo(sSEQ, sMO);
                            FrmBLL.publicfuntion.InserSystemLog(sInfo.gUserInfo.userId, "Material_Monitor", "删除", "删除首盘备料编号: " + sSEQ + " " + sMO);                        
                            MessageBox.Show("删除完成");
                            ed_Scan.Text = "";


                        }
                        else
                        {
                            sInfo.ShowPrgMsg("没有资料可以删除 ", MainParent.MsgType.Error);
                        }
                    }
                    else
                    {
                        sInfo.ShowPrgMsg("料站表刷入错误,重新刷入", MainParent.MsgType.Error);
                    }
                }
            }
        }

        private void labelprint_Click(object sender, EventArgs e)
        {
            labelprint.Checked = true;
            labelnoprint.Checked = false;
        }

        private void labelnoprint_Click(object sender, EventArgs e)
        {
            labelprint.Checked = false;
            labelnoprint.Checked = true;
        }
        #region   Socket服务
        #region variable
        /**
         * 服务端所需变量
         * */
        //服务器端监听对象
        private TcpListener listener;
        private NetworkStream nsServer;
        //存储客户端的消息byte[]
        private byte[] msgBytesByClient;
        //用于标识用户客户端执行是何种操作(0:发送数据　1:发送文件 2:发送数据和文件)
        private int iFunction = 0;
        //存储文件跟数据的总共byte[]
        private byte[] totalBuffer;
        private PrintDocument pdoc;
        private String printText;

        private String idcard;


        /**
         * 客户端所需变量
         * */
        //客户端连接对象
        private TcpClient client;
        //客户端网络工作流
        private NetworkStream nsClient;
        //接收服务端传来的消息
        private byte[] msgBytesByServer;
        private Queue<string> clientQueue;
        private object locker;
        private Thread workerThread;
        #endregion

        #region 启动服务器
        /// <summary>
        /// 启动服务器按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartServer_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("正在启动监听......");
            try
            {
                Int32 port = int.Parse(txtServerPort.Text);
                IPAddress localAddr = IPAddress.Parse(this.txtServerIP.Text);
                FrmPallet_onDownLoadList("正在创建" + txtServerIP.Text + "服务端TcpListener对象");
                //创建TcpListener监听对象
                listener = new TcpListener(localAddr, port);
                FrmPallet_onDownLoadList("已成功创建TcpListener对象，端口号为：" + txtServerPort.Text);
                //启动对端口的连接请求监听
                listener.Start();
                //  this.button1.Enabled = true;
                this.OpenSocket.Enabled = false;
                this.txtServerIP.Enabled = false;
                this.txtServerPort.Enabled = false;
                //启动定时器
                //this.timerByServer.Enabled = true;
                 th = new Thread(new ThreadStart(ListenerServer));
                th.IsBackground = true;
                th.Start();
                listBox1.Items.Add("正在监听......");
            }
            catch (FormatException fx)
            {
                MessageBox.Show("服务器IP地址或端口号不是有效数据！详细信息：" + fx.ToString(), "输入数据格式错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listBox1.Items.Add("启动监听失败......");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("IP地址或端口号为空！详细信息：" + ex.ToString(), "输入数据错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listBox1.Items.Add("启动监听失败......");
            }
            catch (SocketException ex)
            {
                MessageBox.Show("TcpListener启动失败！详细信息：" + ex.ToString(), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listBox1.Items.Add("启动监听失败......");
            }


        }
        #endregion

        #region 线程启动方法监听客户端的连接请求
        public void ListenerServer()
        {
            try
            {
                while (true)
                {
                    //判断是否客户端连接请求
                    if (listener.Pending())
                    {
                        // 接收客户端的请求，并创建一个客户端连接
                        TcpClient client = listener.AcceptTcpClient();
                        this.FrmPallet_onDownLoadList("接收来自" + client.Client.RemoteEndPoint + "请求！");
                        //为每一个客户端创建一个线程用于监听客户端消息
                        Thread thread = new Thread(new ParameterizedThreadStart(CreateNetworkstream));
                        thread.IsBackground = true;
                        thread.Start(client);
                    }
                    Thread.Sleep(200);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("操作无效！,详细信息：" + ex.ToString());
            }
            catch (SocketException ex)
            {
                MessageBox.Show("发生异常，详细信息：" + ex.ToString());
            }
        }
        #endregion


        #region 客户端用于监听服务端消息的定时器
        private void timerByClient_Tick(object sender, EventArgs e)
        {
            try
            {
                lock (nsClient)
                {
                    if (nsClient != null && nsClient.CanRead)
                    {
                        if (nsClient.DataAvailable)
                        {
                            BeginReadServerMsg(nsClient);
                        }
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                MessageBox.Show("数据无法读取，流对象已被销毁或与服务端已断开连接！详细信息：" + ex.ToString());
            }
        }
        #endregion

        #region 读取来自客户端的数据
        /// <summary>
        /// 读取来自客户端的数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void BeginReadServerMsg(NetworkStream nsClient)
        {
            lock (nsClient)
            {
                try
                {

                    if (nsClient.CanRead && nsClient.DataAvailable)
                    {
                        //保存从流中读取的字节数
                        int readSize = 0;
                        //保存从流中读取的总字节数
                        int totalSize = 0;
                        //创建临时保存每次读取数据的流对象
                        MemoryStream ms = new MemoryStream();
                        while (nsClient.DataAvailable)
                        {
                            byte[] msgByte = new byte[10240];
                            //每次从流中读取1KB的数据
                            readSize = nsClient.Read(msgByte, 0, 10240);
                            //累计总共流中保存的字节数
                            totalSize += readSize;
                            //写入临时流中用于一次性全部读取数据
                            ms.Write(msgByte, 0, readSize);
                            FrmPallet_onDownLoadList("已接收" + readSize + "字节的数据");
                        }
                        FrmPallet_onDownLoadList("服务端共发送" + totalSize + "字节的数据");
                        msgBytesByServer = new byte[totalSize];

                        // **** 这里，从ms中读取数据前，ms指针必须回零，不然会出错。****
                        ms.Position = 0;

                        //将ms临时流中保存的数据全部读出
                        int readAllSize = ms.Read(msgBytesByServer, 0, totalSize);
                        FrmPallet_onDownLoadList("已接收到" + readAllSize + "字节的数据,字节数据的长度：" + msgBytesByServer.Length);
                        //将接收到的byte[]转成String
                        String serverMsg = Encoding.Default.GetString(msgBytesByServer);

                        // **** 在数组上调用ToString()得不到数据的
                        FrmPallet_onDownLoadList("服务端消息：" + serverMsg.ToString());
                        nsClient.Flush();
                        ms.Dispose();
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
            }
        }
        #endregion

        #region 服务器用于监听客户端连接的定时器
        private void timerByServer_Tick(object sender, EventArgs e)
        {

            try
            {
                //判断是否客户端连接请求
                if (listener.Pending())
                {
                    // 接收客户端的请求，并创建一个客户端连接
                    TcpClient client = listener.AcceptTcpClient();
                    this.lbxServer.Items.Add("接收来自" + client.Client.RemoteEndPoint + "请求！");
                    //为每一个客户端创建一个线程用于监听客户端消息
                    Thread thread = new Thread(new ParameterizedThreadStart(CreateNetworkstream));
                    thread.IsBackground = true;
                    thread.Start(client);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("操作无效！,详细信息：" + ex.ToString());
            }
            catch (SocketException ex)
            {
                MessageBox.Show("发生异常，详细信息：" + ex.ToString());
            }
        }
        #endregion

        #region 创建接受客户端请求的网络工作流对象
        /// <summary>
        /// 创建网络工作流对象
        /// </summary>
        /// <param name="o">线程执行方法参数对象</param>
        public void CreateNetworkstream(Object o)
        {
            TcpClient client = o as TcpClient;
            CreateNetworkstream(client);
        }
        #endregion

        #region 创建客户端网络工作流对象
        /// <summary>
        /// 接受客户端的连接请求并创建网络工作流对象
        /// </summary>
        /// <param name="client">连接请求TcpClient对象</param>
        public void CreateNetworkstream(TcpClient server)
        {

            try
            {
                //接受客户端连接请求
                NetworkStream nsServer = server.GetStream();
                this.nsServer = nsServer;
                FrmPallet_onDownLoadList("创建" + server.Client.RemoteEndPoint + "客户端的网络工作流对象");
                while (true)
                {
                    if (nsServer == null)
                    {
                        Thread.CurrentThread.Abort();
                        this.FrmPallet_onDownLoadList("销毁" + server.Client.RemoteEndPoint + "客户端的线程");
                        break;
                    }
                    if (nsServer != null && nsServer.CanRead && nsServer.DataAvailable)
                    {
                        ReadByClient(nsServer);
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建客户端网络工作流失败！详细信息：" + ex.ToString());
            }
        }
        #endregion

        #region 开始读取来自客户端的数据
        /// <summary
        /// 读取客户端数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void ReadDataByClient(byte[] msgBytesByClient)
        {

            //将接收到的byte[]转成String
            String serverMsg = Encoding.Default.GetString(msgBytesByClient, 0, msgBytesByClient.Length);
            FrmPallet_onDownLoadList("客户端说：" + serverMsg);
            ed_Scan.Text = serverMsg;
            ed_Scan_KeyPress(null, new KeyPressEventArgs(Convert.ToChar(13)));
            //  tbInput_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }
        #endregion

        #region 读取来自客户端的文件
        /// <summary>
        /// 读取客户端文件数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void ReadByClient(NetworkStream nsServer)
        {
            //锁住客户端数据读取
            lock (nsServer)
            {
                try
                {

                    if (nsServer.DataAvailable)
                    {
                        //保存从流中读取的字节数
                        int readSize = 0;
                        //保存从流中读取的总字节数
                        long totalSize = 0;
                        FrmPallet_onDownLoadList("正在接收客户端数据，请稍候……");
                        byte[] hBuffer = new byte[100];
                        nsServer.Read(hBuffer, 0, 100);
                        string hMsg = Encoding.Default.GetString(hBuffer, 0, 100);
                        totalSize += 100;


                        if (hMsg.Trim().Replace("\0", "").IndexOf("DATA:") != -1)
                        {
                            MemoryStream ms = new MemoryStream();
                            while (nsServer.DataAvailable)
                            {
                                try
                                {
                                    byte[] msgByte = new byte[40960];
                                    //每次从流中读取1KB的数据
                                    readSize = nsServer.Read(msgByte, 0, 40960);
                                    //累计总共流中保存的字节数
                                    totalSize += readSize;
                                    //写入临时流中用于一次性全部读取数据
                                    ms.Write(msgByte, 0, readSize);
                                    Thread.Sleep(50);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("数据发生异常，详细信息：" + ex.ToString());
                                }
                            }
                            byte[] msgByClient = new byte[ms.Length];
                            ms.Position = 0;

                            //将ms临时流中保存的文件以及数据全部读出
                            int readAllSize = ms.Read(msgByClient, 0, (int)msgByClient.Length);
                            FrmPallet_onDownLoadList("收到客户端发送" + totalSize + "字节的数据");
                            ReadDataByClient(msgByClient);
                        }
                        else
                        {
                            try
                            {
                                if (hMsg.Split(':').Length >= 2)
                                {
                                    string filename = hMsg.Split(':')[1].Trim().Replace("\0", "");

                                    if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\Download"))
                                    {
                                        Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Download\");
                                    }
                                    String filePath = System.Windows.Forms.Application.StartupPath + @"\Download\" + filename;
                                    //将接收到的byte[]写成文件
                                    try
                                    {
                                        FileStream fs = new FileStream(filePath, FileMode.Create);
                                        while (nsServer.DataAvailable)
                                        {
                                            try
                                            {
                                                Thread.Sleep(50);
                                                byte[] filebyte = new byte[40960];
                                                //每次从流中读取1KB的数据
                                                readSize = nsServer.Read(filebyte, 0, 40960);
                                                //累计总共流中保存的字节数
                                                totalSize += readSize;
                                                //写入临时流中用于一次性全部读取数据
                                                fs.Write(filebyte, 0, readSize);
                                                fs.Flush();
                                                FrmPallet_onDownLoadList1("已接收：" + totalSize + " 字节");
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("数据发生异常，详细信息：" + ex.ToString());
                                            }
                                        }

                                        FrmPallet_onDownLoadList("收到客户端发送" + totalSize + "字节的【" + filename + "】文件");
                                        if (nsServer != null)
                                        {
                                            //获取要发送的数据
                                            String msg = "服务器端已接收【" + filename + "】文件";
                                            //将string转成byte[]
                                            Byte[] data = Encoding.Default.GetBytes(msg);
                                            //向流中写数据发送到客户端
                                            nsServer.Write(data, 0, data.Length);
                                        }
                                        fs.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("写文件发生异常！" + ex.ToString());
                                    }
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
                finally
                {
                    nsServer.Flush();
                }

            }
        }
        #endregion


        #region 读取来自客户端的文件
        /// <summary>
        /// 读取客户端文件数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void ReadFileByClient(byte[] msgBytesByClient, string fileName)
        {
            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\Download"))
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Download\");
            }
            String filePath = System.Windows.Forms.Application.StartupPath + @"\Download\" + fileName;
            //将接收到的byte[]写成文件
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                fs.Write(msgBytesByClient, 0, msgBytesByClient.Length);
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("写文件发生异常！" + ex.ToString());
            }
        }
        #endregion

        #region 读取客户端封装的文件和数据byte[]
        /// <summary>
        /// 读取客户端传送过来的存储着文件和数据byte[]
        /// </summary>
        /// <param name="nsServer"></param>
        public void ReadFileAndDataByClient(NetworkStream nsServer)
        {
            //锁住客户端数据读取
            lock (nsServer)
            {
                try
                {
                    if (nsServer.DataAvailable)
                    {
                        //保存从流中读取的字节数
                        int readSize = 0;
                        //保存从流中读取的总字节数
                        int totalSize = 0;
                        //创建临时保存每次读取数据的流对象
                        MemoryStream ms = new MemoryStream();
                        while (nsServer.DataAvailable)
                        {
                            byte[] msgByte = new byte[1024];
                            //每次从流中读取1KB的数据
                            readSize = nsServer.Read(msgByte, 0, 1024);
                            //累计总共流中保存的字节数
                            totalSize += readSize;
                            //写入临时流中用于一次性全部读取数据
                            ms.Write(msgByte, 0, readSize);
                            lbxServer.Items.Add("已接收" + readSize + "字节的数据");
                        }

                        lbxServer.Items.Add("客户端共发送" + totalSize + "字节的数据");
                        msgBytesByClient = new byte[totalSize];
                        ms.Position = 0;

                        //将ms临时流中保存的文件以及数据全部读出
                        int readAllSize = ms.Read(msgBytesByClient, 0, totalSize);

                        //取出byte[]中的文件数据并生成文件
                        String filePath = System.Windows.Forms.Application.StartupPath + @"\zp.wlt";
                        //将接收到的byte[]写成文件
                        try
                        {
                            FileStream fs = new FileStream(filePath, FileMode.Create);
                            fs.Write(msgBytesByClient, 0, 1024);
                            fs.Flush();
                            fs.Close();
                            lbxServer.Items.Add("已接收到1024个字节的文件");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("写文件发生异常！" + ex.ToString());
                        }

                        //取出byte[]中的数据并生成string
                        String msg = Encoding.Default.GetString(msgBytesByClient, 1024, msgBytesByClient.Length - 1024);
                        lbxServer.Items.Add("客户端：" + msg);
                        nsServer.Flush();
                        ms.Dispose();

                        printText = msg;

                        //解压zp.wlt相片文件
                        DesImageFile();
                        //打印客户端数据
                        //   printMethod();
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
            }
        }
        #endregion

        #region 打印客户端数据

        void pdoc_BeginPrint(object sender, PrintEventArgs e)
        {
            EventHandler del = delegate
            {
                lbxServer.Items.Add(String.Format("{0} 开始打印...{1}", System.DateTime.Now, Environment.NewLine));
            };

            lbxServer.Invoke(del);
        }

        void pdoc_EndPrint(object sender, PrintEventArgs e)
        {
            EventHandler del = delegate
            {
                lbxServer.Items.Add(String.Format("{0} 打印结束...{1}{1}", System.DateTime.Now, Environment.NewLine));
            };

            lbxServer.Invoke(del);
        }

        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // 这里的值不能是txtMessage.Text，而应该是由队列中取出的
            // 因为要打印时，txtMessgae的Text已经改变了，txtMessage和printer不同步
            string msg;
            lock (locker)
            {
                msg = clientQueue.Dequeue();	// 出队列
            }
            e.Graphics.DrawImage(System.Drawing.Image.FromFile(msg), new Point(100, 100));
            lbxServer.Items.Add(msg);
        }
        #endregion

        #region 将zp.wlt文件解密成照片
        /// <summary>
        /// 解密zp.wlt相片文件
        /// </summary>
        /// <returns>解密结果</returns>
        public void DesImageFile()
        {
            try
            {
                String filePath = System.Windows.Forms.Application.StartupPath + @"\zp.wlt";
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("zp.wlt文件不存在！");
                    return;
                }
                int returnValue = GetBmp(filePath, 1);
                String showMsg = "";
                switch (returnValue)
                {
                    case 0: showMsg = "调用sdtapi.dll错误！"; break;
                    case -1: showMsg = "照片解密错误！"; break;
                    case -2: showMsg = "wlt文件后缀错误！"; break;
                    case -3: showMsg = "wlt文件打开错误！"; break;
                    case -4: showMsg = "wlt文件格式错误！"; break;
                    case -5: showMsg = "软件未授权！"; break;
                    case -6: showMsg = "设备连接错误！"; break;
                    default: showMsg = ""; break;
                }
                if (showMsg != "")
                {
                    MessageBox.Show(showMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 向客户端发送数据
        private void SendMsgToClient(string SendMsg, string SendMsg2)
        {
            // sInfo.ShowPrgMsg(SendMsg,MainParent.MsgType.Incoming);
            LabMsg.Text = SendMsg;
            try
            {
                if (nsServer != null)
                {
                    try
                    {

                        //  sInfo.ShowPrgMsg(SendMsg, MainParent.MsgType.Incoming);
                        LabMsg.Text = SendMsg;
                        //获取要发送的数据
                        String msg = SendMsg2;
                        //将string转成byte[]
                        Byte[] data = Encoding.Default.GetBytes(msg);
                        //向流中写数据发送到客户端
                        nsServer.Write(data, 0, data.Length);
                        //发送数据
                        nsServer.Flush();
                        FrmPallet_onDownLoadList(data.Length + "字节的数据已成功发送！");
                    }
                    catch
                    {
                    }
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("向客户端发送数据失败，详细信息：" + ex.ToString());
            }
        }
        #endregion


        #region 解密相片文件
        /// <summary>
        /// 解密文件的SDK方法
        /// </summary>
        /// <param name="file_name"></param>
        /// <param name="intf"></param>
        /// <returns></returns>
        [DllImport("WltRS.dll")]
        public static extern int GetBmp(string file_name, int intf);
        #endregion



        //委托
        public delegate void dDownloadList(string msg);
        //事件
        public event dDownloadList onDownLoadList;

        public void FrmPallet_onDownLoadList(string msg)
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new Material_Monitor.dDownloadList(FrmPallet_onDownLoadList), new object[] { msg });
            }
            else
            {
                lbxServer.Items.Add(msg);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        //委托
        public delegate void dDownloadList1(string msg);
        //事件
        public event dDownloadList1 onDownLoadList1;

        public void FrmPallet_onDownLoadList1(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Material_Monitor.dDownloadList1(FrmPallet_onDownLoadList1), new object[] { msg });
            }
            else
            {
                lblFileSend.Text = msg;
                System.Windows.Forms.Application.DoEvents();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        public void dispose()
        {
            if (nsClient != null)
                nsClient.Close();
            if (client != null)
                client.Close();
            if (listener.Server.IsBound)
            {
                listener.Stop();
                listener.Server.Close();
                listener.Server.Dispose();
                th.Abort();
            } 
        }

        #endregion

        private void Material_Monitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            dispose();
        }

        private void tsm_todb_Click(object sender, EventArgs e)
        {
            if (!tsm_todb.Checked)
            {
                tsm_todb.Checked = true;
            }
            else
            {
                tsm_todb.Checked = false;
            }
        }
    }
}
