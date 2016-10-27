using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.IO;

namespace SFIS_V2
{
    public partial class Frm_Material_Split : Office2007Form
    {
        public Frm_Material_Split(Frm_MaterialManage Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Frm_MaterialManage mFrm;
        string TrsnStatus = string.Empty;
        string TrSn_woId = string.Empty;
        PrintDLL.PrintLabel PL = new PrintDLL.PrintLabel();
        /// <summary>
        /// 查询出来的TRSN信息
        /// </summary>
        DataTable dt = null;
        private enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error } //文字输出类型
        private Color[] LogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };//文字颜色

        private void ShowMsg(LogMsgType msgtype, string msg)
        {
            this.rtbmsg.Invoke(new EventHandler(delegate
            {
                this.rtbmsg.TabStop = false;
                rtbmsg.SelectedText = string.Empty;
                rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                rtbmsg.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtbmsg.AppendText(msg + "\n");
                rtbmsg.ScrollToCaret();
            }));
        }

        private void Frm_Material_Split_Load(object sender, EventArgs e)
        {
            //#region 添加应用程序
            //if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
            //{
            //    IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
            //    FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
            //    Dictionary<string, object> dic = new Dictionary<string, object>();
            //    dic.Add("PROGID", this.Name);
            //    dic.Add("PROGNAME", this.Text);
            //    dic.Add("PROGDESC", this.Text);
            //    FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
            //}
 
            //#endregion

            PL.ConnCodeSoft();
            PL.RichTextBoxMsg(rtbmsg);
        }

        private void tb_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_Input.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    string InputStr = tb_Input.Text.Trim();
                    string _ErrStr=string.Empty;
                    dt= FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_Tr_Sn_Info(InputStr,out _ErrStr));
                    if (dt.Rows.Count > 0)
                    {
                        TrsnStatus = dt.Rows[0]["STATUS"].ToString();
                        if (TrsnStatus != "2" && TrsnStatus != "7" && TrsnStatus != "3")
                        {
                            ClearTextBox();
                            ShowMsg(LogMsgType.Error, string.Format("查询完成,该Trsn不可分盘,因状态为[{0}]", FrmBLL.publicfuntion.MaterialStatus(TrsnStatus)));
                        }
                        else
                        {
                            tb_trsn.Text = dt.Rows[0]["TR_SN"].ToString();
                            tb_pn.Text = dt.Rows[0]["KP_NO"].ToString();
                            tb_vc.Text = dt.Rows[0]["VENDER_ID"].ToString();
                            tb_dc.Text = dt.Rows[0]["DATE_CODE"].ToString();
                            tb_lc.Text = dt.Rows[0]["LOT_CODE"].ToString();
                            tb_qty.Text = dt.Rows[0]["QTY"].ToString();                       
                            TrSn_woId = dt.Rows[0]["WOID"].ToString();
                            tb_status.Text = FrmBLL.publicfuntion.MaterialStatus(TrsnStatus);
                            ShowMsg(LogMsgType.Incoming, "查询完成");
                        }
                    }
                    else
                    {
                        ClearTextBox();
                        ShowMsg(LogMsgType.Error, "Trsn Error");
                    }

                    
                }
                catch (Exception ex)
                {
                    ShowMsg(LogMsgType.Error, "程序异常:" + ex.Message);
                }
                finally
                {
                    tb_Input.Text = string.Empty;
                }
            }
        }

        private void Frm_Material_Split_FormClosing(object sender, FormClosingEventArgs e)
        {
            PL.ExitCodeSoft();
        }

        private void imbt_Print_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(TrsnStatus)>1) //线边仓接收后就可以分盘  20140926
                {
                    if (Convert.ToInt32(TrsnStatus) == 9)
                    {
                        throw new Exception("此物料已经盘点退料,不可分盘");
                    }
                    if (Convert.ToInt32(TrsnStatus) == 10)
                    {
                        throw new Exception("此物料已经盘点出库,不可分盘");
                    }
                    if (Convert.ToInt32(TrsnStatus) == 6)
                    {
                        throw new Exception("此物料已经售出,不可分盘");
                    }

                    if (Convert.ToInt32(TrsnStatus) != 2 && Convert.ToInt32(TrsnStatus) != 7 && Convert.ToInt32(TrsnStatus) != 3)
                        throw new Exception("只有接收备料,已使用物料或首盘备料可以分盘");
                     
                    if (dt != null && dt.Rows.Count >0)
                    {
                        int OldQTY = Convert.ToInt32(tb_qty.Text) - Convert.ToInt32(tb_new_qty.Text);
                        int NewQTY = Convert.ToInt32(tb_new_qty.Text);

                        if (NewQTY < 1)
                        {
                            throw new Exception("分盘数量不能<1");
                        }

                        if (NewQTY >= Convert.ToInt32(tb_qty.Text))
                        {
                            ShowMsg(LogMsgType.Error, string.Format("拆盘数量[{0}]不能大于原数量[{1}]", NewQTY.ToString(), tb_qty.Text));
                        }
                        else
                        {                            
                            Dictionary<string,object> dic = new Dictionary<string,object>();
                            dic.Add("TR_SN",tb_trsn.Text);
                             dic.Add("QTY",OldQTY.ToString());                       
                             dic.Add("REMARK1", "Split");
                            // string _StrErr = refWebtR_Tr_Sn.Instance.Update_Tr_Sn_QTY(FrmBLL.ReleaseData.DictionaryToJson(dic));
                             string _StrErr = refWebtR_Tr_Sn.Instance.UPDATE_TR_SN(FrmBLL.ReleaseData.DictionaryToJson(dic)); //更新trsn数量                             
                            ShowMsg(LogMsgType.Normal, string.Format("更新原Tr_Sn[{0}] [{1}]", tb_trsn.Text, _StrErr));
                            dic = new Dictionary<string, object>();
                            dic.Add("TR_SN", tb_trsn.Text);
                            dic.Add("WOID", TrSn_woId);
                            dic.Add("QTY", OldQTY.ToString());
                            _StrErr = refWebtWOMaterial.Instance.UPDATE_T_WO_Material(FrmBLL.ReleaseData.DictionaryToJson(dic));
                            ShowMsg(LogMsgType.Normal, string.Format("更新 WO_Material[{0}] [{1}]", tb_trsn.Text, _StrErr));

                            dic = new Dictionary<string, object>();
                            dic.Add("TRSN", tb_trsn.Text);
                            dic.Add("WOID", TrSn_woId);
                            dic.Add("QTY", OldQTY.ToString());
                            _StrErr=refWebtSmtKpMonitor.Instance.Update_SmtKpMonitor(FrmBLL.ReleaseData.DictionaryToJson(dic),new string []{"WOID" ,"TRSN"});
                            ShowMsg(LogMsgType.Normal, string.Format("更新 SmtKpMonitor[{0}] [{1}]", tb_trsn.Text, _StrErr));

                            string New_Tr_Sn = refWebtR_Tr_Sn.Instance.GetSeqTrSnInfo();
                            Dictionary<string, object> dictrsn = new Dictionary<string, object>();
                            dictrsn.Add("PO_ID", dt.Rows[0]["PO_ID"].ToString());
                            dictrsn.Add("TR_SN", New_Tr_Sn);
                            dictrsn.Add("KP_NO", dt.Rows[0]["KP_NO"].ToString());
                            dictrsn.Add("VENDER_ID", dt.Rows[0]["VENDER_ID"].ToString());
                            dictrsn.Add("VENDER_NAME", dt.Rows[0]["VENDER_NAME"].ToString());
                            dictrsn.Add("DATE_CODE", dt.Rows[0]["DATE_CODE"].ToString());
                            dictrsn.Add("LOT_CODE", dt.Rows[0]["LOT_CODE"].ToString());
                            dictrsn.Add("QTY", tb_new_qty.Text);
                            dictrsn.Add("STOCK_ID", dt.Rows[0]["STOCK_ID"].ToString());
                            dictrsn.Add("LOC_ID", dt.Rows[0]["LOC_ID"].ToString());
                            dictrsn.Add("TANSFER_NO", dt.Rows[0]["TANSFER_NO"].ToString());
                            dictrsn.Add("URGENT_PN", dt.Rows[0]["URGENT_PN"].ToString());
                            dictrsn.Add("STOCK_NO", dt.Rows[0]["STOCK_NO"].ToString());
                            dictrsn.Add("STOCK_DATE", dt.Rows[0]["STOCK_DATE"].ToString());
                            dictrsn.Add("WOID", dt.Rows[0]["WOID"].ToString());
                            dictrsn.Add("USER_ID", mFrm.UserId);
                            dictrsn.Add("KP_DESC", dt.Rows[0]["KP_DESC"].ToString());
                            dictrsn.Add("STATUS", "2");
                            dictrsn.Add("FIFO_DC", dt.Rows[0]["FIFO_DC"].ToString());

                            refWebtR_Tr_Sn.Instance.insert_into_R_tr_sn(FrmBLL.ReleaseData.DictionaryToJson(dictrsn));

                            dictrsn = new Dictionary<string, object>();
                            dictrsn.Add("TR_SN", New_Tr_Sn);
                            dictrsn.Add("KP_NO", dt.Rows[0]["KP_NO"].ToString());
                            dictrsn.Add("VENDER_ID", dt.Rows[0]["VENDER_ID"].ToString());
                            dictrsn.Add("DATE_CODE", dt.Rows[0]["DATE_CODE"].ToString());
                            dictrsn.Add("LOT_CODE", dt.Rows[0]["LOT_CODE"].ToString());
                            dictrsn.Add("QTY", tb_new_qty.Text);
                            dictrsn.Add("WOID", dt.Rows[0]["WOID"].ToString());
                            dictrsn.Add("USER_ID", mFrm.UserId);
                            dictrsn.Add("STATUS", "2");
                            refWebtWOMaterial.Instance.Insert_T_WO_Material(FrmBLL.ReleaseData.DictionaryToJson(dictrsn));
                            ShowMsg(LogMsgType.Normal, string.Format("记录新Trsn[{0}]" + _StrErr, New_Tr_Sn));
                            refWebtR_Tr_Sn.Instance.Update_TR_SN(New_Tr_Sn, "NA", mFrm.UserId, "2", tb_trsn.Text, "NA");
                            ShowMsg(LogMsgType.Normal, string.Format("开始打印新TrSn[{0}]", New_Tr_Sn));
                            PrintLabel(New_Tr_Sn, tb_pn.Text, tb_dc.Text, tb_vc.Text, tb_lc.Text, NewQTY.ToString(), dt.Rows[0]["KP_DESC"].ToString(), dt.Rows[0]["STOCK_ID"].ToString(), dt.Rows[0]["LOC_ID"].ToString());
                            ShowMsg(LogMsgType.Incoming, string.Format("TrSn[{0}]打印完成", New_Tr_Sn));

                            ShowMsg(LogMsgType.Normal, string.Format("开始打印被拆盘TrSn[{0}]", tb_trsn.Text));
                            PrintLabel(tb_trsn.Text, tb_pn.Text, tb_dc.Text, tb_vc.Text, tb_lc.Text, OldQTY.ToString(), dt.Rows[0]["KP_DESC"].ToString(), dt.Rows[0]["STOCK_ID"].ToString(), dt.Rows[0]["LOC_ID"].ToString());
                            ShowMsg(LogMsgType.Incoming, string.Format("TrSn[{0}]打印完成", tb_trsn.Text));

                            ShowMsg(LogMsgType.Warning, "拆盘作业完成");
                        }
                       
                    }
                    else
                    {
                        ShowMsg(LogMsgType.Error, "拆盘数据异常,请重新刷入料盘");
                    }

                }
                else
                {
                    ShowMsg(LogMsgType.Error, string.Format("状态不正确,不能拆盘打印", tb_status.Text));
                }

            }
            catch (Exception ex)
            {
                ShowMsg(LogMsgType.Error, string.Format("发生异常:{0}", ex.Message));
            }
            finally
            {
                dt = null;
                ClearTextBox();
                ShowMsg(LogMsgType.Incoming, "初始化数据完成");
            }
        }

        private void ClearTextBox()
        {
            tb_pn.Text = string.Empty;
            tb_dc.Text = string.Empty;
            tb_vc.Text = string.Empty;
            tb_lc.Text = string.Empty;
            tb_qty.Text = "0";
            tb_new_qty.Text = "0";
            tb_status.Text = string.Empty;
            tb_trsn.Text = string.Empty;
        }

        private void PrintLabel(string TrSn,string KP_No,string DC,string VC,string LC,string QTY,string Remark,string STOCK_ID,string LOC_ID)
        {
            DataTable PrintDt = new DataTable();
            PrintDt.Columns.Add("Colnums", typeof(string));
            PrintDt.Columns.Add("DATA", typeof(string));
            PrintDt.Rows.Add("TR_SN", TrSn);
            PrintDt.Rows.Add("PART_NO", KP_No);
            PrintDt.Rows.Add("DATE_CODE", DC);
            PrintDt.Rows.Add("UNIT_SIZE", QTY);
            PrintDt.Rows.Add("VENDER_CODE", VC);
            PrintDt.Rows.Add("LOT_ID", LC);
            PrintDt.Rows.Add("EMP_NO", mFrm.UserId);
            PrintDt.Rows.Add("REMARK", Remark);
            PrintDt.Rows.Add("STORLOC", string.Format("{0}/{1}", STOCK_ID, LOC_ID));

            string FilePatch=  Directory.GetCurrentDirectory() + "\\LabelFile\\FEIXUN_LOT.lab";
            PL.SendPrintLabel(PrintDt, FilePatch, 1);
        }

        private void tb_new_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
       
    }
}
