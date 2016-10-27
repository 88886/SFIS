using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using RefWebService_BLL;


namespace SFIS_V2
{
    public partial class Frm_MTR_RETURN : Office2007Form
    {
        public Frm_MTR_RETURN(MainParent mp)
        {
            InitializeComponent();
            mFrm = mp;
        }
        MainParent mFrm;
        //     LoginInfo UserInfo = LoginInfo.GetLoginInfo();
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };

        PrintDLL.PrintLabel sPL = null;

        /// <summary>
        /// 保存TrSn原信息
        /// </summary>
        DataTable _dt = null;

        /// <summary>
        /// 准备上抛SAP信息
        /// </summary>
        DataTable dt_UpLoad_Erp = new DataTable("mydt");

        /// <summary>
        /// 物料发料退料工单
        /// </summary>
        public string My_MoNumber = string.Empty;
        public string My_Plan = string.Empty;

        /// <summary>
        /// 打印退料工单
        /// </summary>
        public string return_woId = string.Empty;


        /// <summary>
        /// 显示消息函数
        /// </summary>
        /// <param name="msgtype"></param>
        /// <param name="msg"></param>
        public void ShowMsg(mLogMsgType msgtype, string msg)
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

        /// <summary>
        /// 工单退料信息显示
        /// </summary>
        /// <param name="msgtype"></param>
        /// <param name="msg"></param>
        public void ShowMsg_Return(mLogMsgType msgtype, string msg)
        {
            try
            {
                this.rtbreturn.Invoke(new EventHandler(delegate
                {
                    rtbreturn.TabStop = false;
                    rtbreturn.SelectedText = string.Empty;
                    rtbreturn.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbreturn.SelectionColor = mLogMsgTypeColor[(int)msgtype];
                    rtbreturn.AppendText(msg + "\n");
                    rtbreturn.ScrollToCaret();
                }));
            }
            catch
            {
            }
        }
        private void tb_woId_KeyDown(object sender, KeyEventArgs e)
        {

            if (!string.IsNullOrEmpty(tb_woId.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {

                    dgvmtrlist.Rows.Clear();
                    dgvTrsnList.Rows.Clear();


                    if (!Get_WO_Erp_Info(tb_woId.Text))
                        return;

                    DataTable dtwoInfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoBomInfo.Instance.GetWoBomInfo(tb_woId.Text));

                    foreach (DataRow dr in dtwoInfo.Rows)
                    {
                        dgvmtrlist.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                    }

                    DataTable dtTrsnList = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_woId_Trsn_List(tb_woId.Text, null));
                    foreach (DataRow dr in dtTrsnList.Rows)
                    {
                        if (dr["STATUS"].ToString() == "1")
                        {
                            dgvTrsnList.Rows.Add(dr["TR_SN"].ToString(), dr["KP_NO"].ToString(), dr["VENDER_ID"].ToString(), dr["DATE_CODE"].ToString(), dr["LOT_CODE"].ToString(), dr["QTY"].ToString(), dr["STOCK_ID"].ToString());
                        }
                    }

                    ShowMsg(mLogMsgType.Warning, "正在计算发料总数......");
                    foreach (DataGridViewRow dgvr in dgvmtrlist.Rows)
                    {

                        int x = 0;
                        DataTable dtqty = FrmBLL.publicfuntion.getNewTable(dtTrsnList, string.Format("KP_NO='{0}'", dgvr.Cells[2].Value.ToString()));
                        foreach (DataRow dr in dtqty.Rows)
                        {
                            x += Convert.ToInt32(dr["QTY"].ToString());
                        }
                        dgvr.Cells[5].Value = x.ToString();
                    }

                    ShowMsg(mLogMsgType.Incoming, "计算发料总数完成......");

                    ShowMsg(mLogMsgType.Incoming, "查询完成...");
                }
                catch (Exception ex)
                {
                    ShowMsg(mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    tb_woId.SelectAll();
                }
            }

        }

        private void Frm_MTR_RETURN_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
            {
                IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PROGID", this.Name);
                dic.Add("PROGNAME", this.Text);
                dic.Add("PROGDESC", this.Text);
                FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
            }

            imbt_RePrint.Enabled = false;
            #endregion

            tb_FilePatch.Text = Directory.GetCurrentDirectory() + "\\LabelFile\\FEIXUN_LOT.lab";
            splitContainer1.Width = this.Width / 3 - 30;
            tabControl1.SelectedIndex = 0;
            tb_woId.Focus();
            try
            {
                sPL = new PrintDLL.PrintLabel();
                sPL.ConnCodeSoft();
                sPL.RichTextBoxMsg(rtbcancel);
            }
            catch
            {

            }


            dt_UpLoad_Erp.Columns.Add("SHIPPING_NO");
            dt_UpLoad_Erp.Columns.Add("KP_NO");
            dt_UpLoad_Erp.Columns.Add("QTY");
            dt_UpLoad_Erp.Columns.Add("DEB_CRED");
            dt_UpLoad_Erp.Columns.Add("STORE_LOC");
            dt_UpLoad_Erp.Columns.Add("MOVE_STLOC");
            dt_UpLoad_Erp.Columns.Add("PLANT");
        }

        private void tb_trsn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_trsn.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (tb_trsn.Text.ToUpper() == "END")
                    {
                        Upload_Erp();
                        return;
                    }

                    if (dgvTrsnList.Rows.Count < 1)
                    {
                        throw new Exception("没有物料可以接收");
                    }
                    bool chkflag = false;
                 
                    foreach (DataGridViewRow dgvr in dgvTrsnList.Rows)
                    {
                        if (tb_trsn.Text == dgvr.Cells[0].Value.ToString())
                        {

                            DataRow NEWROW = dt_UpLoad_Erp.NewRow();
                            NEWROW["SHIPPING_NO"] =My_MoNumber;
                            NEWROW["KP_NO"] = dgvr.Cells["KP_NO"].Value.ToString();
                            NEWROW["QTY"] = dgvr.Cells["QTY"].Value.ToString();
                            NEWROW["DEB_CRED"] = "OUT";
                            if (string.IsNullOrEmpty(dgvr.Cells["STOCK_ID"].Value.ToString()) || dgvr.Cells["STOCK_ID"].Value.ToString() == "NA")
                                NEWROW["STORE_LOC"] = "1001";
                            else
                                NEWROW["STORE_LOC"] = dgvr.Cells[6].Value.ToString();
                            NEWROW["MOVE_STLOC"] = "1005";
                            NEWROW["PLANT"] = My_Plan;
                            dt_UpLoad_Erp.Rows.Add(NEWROW);

                            chkflag = true;
                        }
                    }
                    if (chkflag)
                    {
                        string _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(tb_trsn.Text, "NA", mFrm.gUserInfo.userId, "2", "NA", "NA");
                        if (_StrErr == "OK")
                        {
                            DeleteReceivedTrSn(tb_trsn.Text);
                            ShowMsg(mLogMsgType.Incoming, string.Format("TrSn[{0}]备料完成", tb_trsn.Text));
                            Upload_Erp(dt_UpLoad_Erp);
                        }
                        else
                        {
                            ShowMsg(mLogMsgType.Error, string.Format("更新 TrSn[{0}] 状态失败,错误提示[{1}]", tb_trsn.Text, _StrErr));
                        }
                    }
                    else
                    {
                        ShowMsg(mLogMsgType.Error, "TrSn 刷入错误或不属于该工单");
                    }
                }
                catch (Exception ex)
                {
                    ShowMsg(mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    tb_trsn.SelectAll();
                    dt_UpLoad_Erp.Rows.Clear();
                }
            }
        }

        /// <summary>
        /// 删除已经接收完成的唯一条码
        /// </summary>
        /// <param name="Trsn"></param>
        private void DeleteReceivedTrSn(string Trsn)
        {

            for (int i = dgvTrsnList.Rows.Count - 1; i >= 0; i--)
            {
                if (dgvTrsnList.Rows[i].Cells[0].Value.ToString() == Trsn)
                {
                    dgvTrsnList.Rows.RemoveAt(i);
                }
            }
        }



        private void imbt_Receiving_Click(object sender, EventArgs e)
        {
            if (dgvTrsnList.Rows.Count > 0)
            {
                try
                {
                    List<string> ListTrsn = new List<string>();
                    string Num = (Math.Floor(Convert.ToDouble(dgvTrsnList.Rows.Count / 500)) + 1).ToString();
                    if (MessageBox.Show(string.Format("工单发料料盘总数[{0}]盘\r\n每次只能接收[500]盘\r\n需要做[{1}]次接收", dgvTrsnList.Rows.Count.ToString(), Num), "工单接收物料提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {                     
                        int x = 0;
                        foreach (DataGridViewRow dgvr in dgvTrsnList.Rows)
                        {
                            #region
                            DataRow NEWROW = dt_UpLoad_Erp.NewRow();
                            NEWROW["SHIPPING_NO"] =My_MoNumber;
                            NEWROW["KP_NO"] = dgvr.Cells[1].Value.ToString();
                            NEWROW["QTY"] = dgvr.Cells[5].Value.ToString();
                            NEWROW["DEB_CRED"] = "OUT";
                            if (string.IsNullOrEmpty(dgvr.Cells[6].Value.ToString()) || dgvr.Cells[6].Value.ToString() == "NA")
                                NEWROW["STORE_LOC"] = "1001";
                            else
                                NEWROW["STORE_LOC"] = dgvr.Cells[6].Value.ToString();
                            NEWROW["MOVE_STLOC"] = "1005";
                            NEWROW["PLANT"] = My_Plan;
                            dt_UpLoad_Erp.Rows.Add(NEWROW);
                            #endregion

                            ListTrsn.Add(dgvr.Cells[0].Value.ToString());
                            x++;
                            if (x == 500)
                            {
                                break;
                            }                          
                        }


                        foreach (string Item in ListTrsn)
                        {
                            string _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(Item, "NA", mFrm.gUserInfo.userId, "2", "NA", "NA");
                            if (_StrErr == "OK")
                            {
                                DeleteReceivedTrSn(Item);
                                ShowMsg(mLogMsgType.Incoming, string.Format("TrSn[{0}]接收备料完成", Item));
                            }
                        }


                        #region 发料过账
                        Upload_Erp(dt_UpLoad_Erp);
                        Upload_Erp();
                        #endregion



                        ShowMsg(mLogMsgType.Incoming, string.Format("接收备料完成...."));
                        tb_woId.SelectAll();
                    }
                }
                catch(Exception ex)
                {
                    ShowMsg(mLogMsgType.Error, string.Format("Exection: {0}",ex.Message));
                }
                finally
                {
                    dt_UpLoad_Erp.Rows.Clear();
                }
            }
            else
            {
                ShowMsg(mLogMsgType.Error, "没有物料可以接收");
            }

        }

        private void tb_wo_cancel_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_wo_cancel.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    dgvmtrlist.Rows.Clear();
                    dgvTrsnList.Rows.Clear();


                    if (!Get_WO_Erp_Info(tb_wo_cancel.Text))
                        return;

                    DataTable dtTrsnList = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_woId_Trsn_List(tb_wo_cancel.Text, null));
                    foreach (DataRow dr in dtTrsnList.Rows)
                    {
                        if (dr["STATUS"].ToString() == "2")
                        {
                            dgvTrsnList.Rows.Add(dr["TR_SN"].ToString(), dr["KP_NO"].ToString(), dr["VENDER_ID"].ToString(), dr["DATE_CODE"].ToString(), dr["LOT_CODE"].ToString(), dr["QTY"].ToString(), dr["STOCK_ID"].ToString());
                        }
                    }

                    ShowMsg(mLogMsgType.Incoming, "查询工单物料完成...");
                }
                catch (Exception ex)
                {
                    ShowMsg(mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    tb_wo_cancel.SelectAll();
                }
            }
        }

        private void imbt_wocancel_Click(object sender, EventArgs e)
        {
            if (dgvTrsnList.Rows.Count > 0)
            {
                try
                {
                    List<string> ListTrsn = new List<string>();
                    string Num = (Math.Floor(Convert.ToDouble(dgvTrsnList.Rows.Count / 500)) + 1).ToString();
                    if (MessageBox.Show(string.Format("工单料盘总数 [{0}] 盘\r\n每次只能撤销[500]盘\r\n需要做[{1}]次撤销", dgvTrsnList.Rows.Count.ToString(), Num), "工单接收物料提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {

                        int x = 0;
                        foreach (DataGridViewRow dgvr in dgvTrsnList.Rows)
                        {

                            #region
                            DataRow NEWROW = dt_UpLoad_Erp.NewRow();
                            NEWROW["SHIPPING_NO"] = tb_woId.Text.ToString();
                            NEWROW["KP_NO"] = dgvr.Cells[1].Value.ToString();
                            NEWROW["QTY"] = dgvr.Cells[5].Value.ToString();
                            NEWROW["DEB_CRED"] = "BACK";
                            NEWROW["STORE_LOC"] = "1005";
                            if (string.IsNullOrEmpty(dgvr.Cells[6].Value.ToString()) || dgvr.Cells[6].Value.ToString() == "NA")
                                NEWROW["MOVE_STLOC"] = "1001";
                            else
                                NEWROW["MOVE_STLOC"] = dgvr.Cells[6].Value.ToString();
                            NEWROW["PLANT"] =My_Plan;
                            dt_UpLoad_Erp.Rows.Add(NEWROW);
                            #endregion

                            ListTrsn.Add(dgvr.Cells[0].Value.ToString());
                            x++;
                            if (x == 500)
                            {
                                break;
                            }                         
                         

                        }

                        foreach (string Item in ListTrsn)
                        {
                            string _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(Item, "NA", mFrm.gUserInfo.userId, "1", "NA", "NA");
                            if (_StrErr == "OK")
                            {
                                DeleteReceivedTrSn(Item);
                                ShowMsg(mLogMsgType.Incoming, string.Format("TrSn[{0}]撤销接收备料完成", Item));
                            }
                        }

                        #region 撤销发料过账
                        Rollback_Upload_Erp(dt_UpLoad_Erp);
                        Rollback_Upload_Erp();
                        #endregion

                        ShowMsg(mLogMsgType.Incoming, string.Format("撤销接收备料完成...."));
                        tb_wo_cancel.SelectAll();
                    }
                }
                catch (Exception ex)
                {
                    ShowMsg(mLogMsgType.Incoming, string.Format("Execption: {0}", ex.Message));
                }
                finally
                {
                    dt_UpLoad_Erp.Rows.Clear();
                }
            }
            else
            {
                ShowMsg(mLogMsgType.Error, "没有物料可以撤销");
            }
        }

        private void radReceiving_CheckedChanged(object sender, EventArgs e)
        {
            dgvmtrlist.Rows.Clear();
            dgvTrsnList.Rows.Clear();
            tb_woId.Text = "";
            tb_trsn.Text = "";
            groupBox4.Enabled = false;
            groupBox3.Enabled = true;
            tb_woId.Focus();
        }



        private void radcancel_CheckedChanged(object sender, EventArgs e)
        {
            dgvmtrlist.Rows.Clear();
            dgvTrsnList.Rows.Clear();
            tb_wo_cancel.Text = "";
            tb_trsncancel.Text = "";
            groupBox4.Enabled = true;
            groupBox3.Enabled = false;
            tb_wo_cancel.Focus();
        }

        int InitQTY = 0;
        private void tb_old_tr_sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!String.IsNullOrEmpty(tb_old_tr_sn.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                   
                    ClearTextBox();
                    string _StrErr = string.Empty;
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_Tr_Sn_Info(tb_old_tr_sn.Text, out _StrErr));
                    if (dt.Rows.Count > 0)
                    {
                        _dt = dt;
                        sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Incoming, string.Format("找到TrSn[{0}]...", tb_old_tr_sn.Text));
                        if (Convert.ToInt32(  dt.Rows[0]["status"].ToString())>1)
                        {
                            if (dt.Rows[0]["status"].ToString() =="9")
                            {
                                sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, string.Format("Tr_Sn[{0}],已经被拆分,不可再次拆分", tb_old_tr_sn.Text));
                                return;
                            }
                            if (Convert.ToInt32(dt.Rows[0]["status"].ToString()) == 10)
                            {
                                sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, string.Format("Tr_Sn[{0}],已盘点出库,不可拆分", tb_old_tr_sn.Text));
                                return;
                            }
                            tb_show_trsn.Text = tb_old_tr_sn.Text.Trim();
                            tb_show_pn.Text = dt.Rows[0]["KP_NO"].ToString();
                            tb_show_vc.Text = dt.Rows[0]["VENDER_ID"].ToString();
                            tb_show_dc.Text = dt.Rows[0]["DATE_CODE"].ToString();
                            tb_show_lc.Text = dt.Rows[0]["LOT_CODE"].ToString();
                            tb_show_qty.Text = dt.Rows[0]["QTY"].ToString();
                            tb_show_woId.Text = dt.Rows[0]["WOID"].ToString();
                            tb_kpdesc.Text = dt.Rows[0]["KP_DESC"].ToString();
                            InitQTY = Convert.ToInt32(dt.Rows[0]["QTY"].ToString());
                            sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Incoming, string.Format("查询TrSn 完成"));
                        }
                        else
                        {
                            if (ChkRepRint.Checked)
                            {
                                tb_show_pn.Text = dt.Rows[0]["KP_NO"].ToString();
                                tb_show_vc.Text = dt.Rows[0]["VENDER_ID"].ToString();
                                tb_show_dc.Text = dt.Rows[0]["DATE_CODE"].ToString();
                                tb_show_lc.Text = dt.Rows[0]["LOT_CODE"].ToString();
                                tb_show_qty.Text = dt.Rows[0]["QTY"].ToString();
                                tb_show_woId.Text = dt.Rows[0]["WOID"].ToString();
                                tb_kpdesc.Text = dt.Rows[0]["KP_DESC"].ToString();
                                sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Incoming, string.Format("Tr_Sn[{0}],查询完成,可补执行补印....", tb_old_tr_sn.Text));
                            }
                            else
                            {                               
                                sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, string.Format("Tr_Sn[{0}],状态没有使用,不可做退料作业....", tb_old_tr_sn.Text));
                            }
                        }

                        tb_show_qty.Focus();
                        tb_show_qty.SelectAll();
                    }
                    else
                    {
                        sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, string.Format("Tr_Sn[{0}],没有找到信息", tb_old_tr_sn.Text));

                    }
                }
                catch (Exception ex)
                {
                    sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    tb_old_tr_sn.Text = string.Empty;
                }
            }
        }

        private void tb_show_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tb_show_qty_TextAlignChanged(object sender, EventArgs e)
        {

            MessageBox.Show("发生变化");
        }



        private void tb_wo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_wo.Text) && e.KeyCode == Keys.Enter)
            {
                dgvCancelList.Rows.Clear();
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_woId_Trsn_List(tb_wo.Text, null));

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["STATUS"].ToString() == "4")
                    {
                        dgvCancelList.Rows.Add(dr["woId"].ToString(), dr["tr_sn"].ToString(), dr["kp_no"].ToString(), dr["vender_id"].ToString(), dr["date_code"].ToString(), dr["lot_code"].ToString(), dr["QTY"].ToString(), dr["KP_DESC"].ToString());
                    }
                }
                tb_wo.SelectAll();
            }
        }


        private void imbt_Print(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tb_show_qty.Text) >= InitQTY)
                {
                    sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, string.Format("退料打印数量错误,新料盘数量必须小于原数量,原数量为[{0}],新料盘数量[{1}]", InitQTY.ToString(), tb_show_qty.Text));
                }
                else
                {
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                       // if (_dt.Rows[0]["STATUS"].ToString() != "7") //使用后方可退料
                        if (Convert.ToInt32( _dt.Rows[0]["STATUS"].ToString()) < 2) //线边仓接收后,可做退料
                        {                        
                            throw new Exception("线边仓未接收物料,不可退料打印");
                        }
                        if (Convert.ToInt32(_dt.Rows[0]["STATUS"].ToString()) == 9)
                        {
                            throw new Exception("物料已经拆分,不可退料打印");
                        }
                        if (Convert.ToInt32(_dt.Rows[0]["STATUS"].ToString()) == 10)
                        {
                            throw new Exception("物料已盘点出库,不可退料打印");
                        }
                        string Tr_Sn = refWebtR_Tr_Sn.Instance.GetSeqTrSnInfo();
                        if (string.IsNullOrEmpty(Tr_Sn))
                        {
                            throw new Exception("获取新的Trsn号码错误");
                        }

                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("PO_ID", _dt.Rows[0]["PO_ID"].ToString());
                        dic.Add("TR_SN", Tr_Sn);
                        dic.Add("KP_NO",tb_show_pn.Text);
                        dic.Add("VENDER_ID", tb_show_vc.Text);
                        dic.Add("VENDER_NAME", _dt.Rows[0]["vender_name"].ToString());
                        dic.Add("DATE_CODE", tb_show_dc.Text);
                        dic.Add("LOT_CODE", tb_show_lc.Text);
                        dic.Add("QTY", tb_show_qty.Text);
                        dic.Add("STOCK_ID", _dt.Rows[0]["STOCK_ID"].ToString());
                        dic.Add("LOC_ID", _dt.Rows[0]["LOC_ID"].ToString());
                        dic.Add("TANSFER_NO", _dt.Rows[0]["TANSFER_NO"].ToString());
                        dic.Add("URGENT_PN", _dt.Rows[0]["URGENT_PN"].ToString());
                        dic.Add("STOCK_NO", _dt.Rows[0]["STOCK_NO"].ToString());
                        dic.Add("STOCK_DATE", _dt.Rows[0]["STOCK_DATE"].ToString());
                        dic.Add("WOID", tb_show_woId.Text);
                        dic.Add("USER_ID", mFrm.gUserInfo.userId);
                        dic.Add("KP_DESC", tb_kpdesc.Text);
                        dic.Add("STATUS", "NA");
                        dic.Add("FIFO_DC", _dt.Rows[0]["FIFO_DC"].ToString());

                       string _StrErr= refWebtR_Tr_Sn.Instance.insert_into_R_tr_sn(FrmBLL.ReleaseData.DictionaryToJson(dic));                   
                        if (_StrErr == "OK")
                        {
                            //更新原Trsn状态为9 盘点完成,并记录新的Trsn
                            _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(tb_show_trsn.Text, "NA", mFrm.gUserInfo.userId, "9", "NA", Tr_Sn);

                            //更新新的Trsn状态,并记录原Trsn
                            _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(Tr_Sn, "NA", mFrm.gUserInfo.userId, "4", tb_show_trsn.Text, "NA");
                            if (_StrErr == "OK")
                            {
                                sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Incoming, string.Format("开始打印Tr_Sn[{0}].", Tr_Sn));
                                dgvCancelList.Rows.Add(tb_show_woId.Text, Tr_Sn, tb_show_pn.Text, tb_show_vc.Text, tb_show_dc.Text, tb_show_lc.Text, tb_show_qty.Text, tb_kpdesc.Text);

                                DataTable PrintDt = new DataTable();
                                PrintDt.Columns.Add("Colnums", typeof(string));
                                PrintDt.Columns.Add("DATA", typeof(string));
                                PrintDt.Rows.Add("TR_SN", Tr_Sn);
                                PrintDt.Rows.Add("PART_NO", tb_show_pn.Text);
                                PrintDt.Rows.Add("DATE_CODE", tb_show_dc.Text);
                                PrintDt.Rows.Add("UNIT_SIZE", tb_show_qty.Text);
                                PrintDt.Rows.Add("VENDER_CODE", tb_show_vc.Text);
                                PrintDt.Rows.Add("LOT_ID", tb_show_lc.Text);
                                PrintDt.Rows.Add("EMP_NO", mFrm.gUserInfo.userId);
                                PrintDt.Rows.Add("REMARK", tb_kpdesc.Text);
                                PrintDt.Rows.Add("STORLOC", string.Format("{0}/{1}", _dt.Rows[0]["STOCK_ID"].ToString(), _dt.Rows[0]["LOC_ID"].ToString()));
                                

                                PrintLabel(PrintDt, tb_FilePatch.Text);

                                ClearTextBox();
                                MessageBox.Show("打印完成....");
                            }
                            else
                            {
                                sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, string.Format("更新条码[{0}]状态失败,提示信息【{1}】...", Tr_Sn, _StrErr));
                            }
                        }
                        else
                        {
                            //  Show_Msg.SendMsg(rtbcancel, Show_Msg.mLogMsgType.Error, string.Format("插入新条码信息失败[{0}]...", _StrErr));
                            sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, string.Format("插入新条码信息失败[{0}]...", _StrErr));
                        }
                    }
                    else
                    {
                        // Show_Msg.SendMsg(rtbcancel, Show_Msg.mLogMsgType.Error, ;
                        sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, string.Format("获取打印条码信息失败."));
                    }
                }
            }
            catch (Exception ex)
            {
                // Show_Msg.SendMsg(rtbcancel, Show_Msg.mLogMsgType.Error, string.Format(ex.Message));
                sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, ex.Message);

            }
        }

        private void PrintLabel(DataTable dt, string FilePatch)
        {
            sPL.SendPrintLabel(dt, FilePatch, 1);
        }

        private void Frm_MTR_RETURN_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                sPL.ExitCodeSoft();
            }
            catch (Exception ex)
            {
                MessageBox.Show("退出CodeSoft失败:" + ex.Message);
            }
        }

        private void imbt_RePrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    DataTable PrintDt = new DataTable();
                    PrintDt.Columns.Add("Colnums", typeof(string));
                    PrintDt.Columns.Add("DATA", typeof(string));
                    PrintDt.Rows.Add("TR_SN", _dt.Rows[0]["TR_SN"].ToString());
                    PrintDt.Rows.Add("PART_NO", _dt.Rows[0]["KP_NO"].ToString());
                    PrintDt.Rows.Add("DATE_CODE", _dt.Rows[0]["DATE_CODE"].ToString());
                    if (Convert.ToInt32(_dt.Rows[0]["QTY"].ToString())==Convert.ToInt32(tb_show_qty.Text))
                    PrintDt.Rows.Add("UNIT_SIZE", _dt.Rows[0]["QTY"].ToString());
                    else
                    {
                        PrintDt.Rows.Add("UNIT_SIZE", tb_show_qty.Text);

                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("TR_SN", _dt.Rows[0]["TR_SN"].ToString());
                        dic.Add("QTY", tb_show_qty.Text);
                        string _StrErr = refWebtR_Tr_Sn.Instance.Update_Tr_Sn_QTY(FrmBLL.ReleaseData.DictionaryToJson(dic));
                      
                        if (_StrErr!="OK")
                            MessageBox.Show("Update_Tr_Sn_QTY ERROR \r\n 提示: "+_StrErr);
                        refWebtR_Tr_Sn.Instance.Update_TR_SN(_dt.Rows[0]["TR_SN"].ToString(), "NA", mFrm.gUserInfo.userId, "2", "NA", "X");
                    
                     }
                    PrintDt.Rows.Add("VENDER_CODE", _dt.Rows[0]["VENDER_ID"].ToString());
                    PrintDt.Rows.Add("LOT_ID", _dt.Rows[0]["LOT_CODE"].ToString());
                    PrintDt.Rows.Add("EMP_NO", mFrm.gUserInfo.userId);
                    PrintDt.Rows.Add("REMARK", _dt.Rows[0]["KP_DESC"].ToString());
                    PrintDt.Rows.Add("STORLOC", string.Format("{0}/{1}", _dt.Rows[0]["STOCK_ID"].ToString(), _dt.Rows[0]["LOC_ID"].ToString()));
                    PrintLabel(PrintDt, tb_FilePatch.Text);
                    tb_show_pn.Text =string.Empty;
                    tb_show_vc.Text = string.Empty;
                    tb_show_dc.Text = string.Empty;
                    tb_show_lc.Text = string.Empty;
                    tb_show_qty.Text = string.Empty;
                    tb_show_woId.Text = string.Empty;
                    tb_kpdesc.Text = string.Empty;
                    _dt = null;
                }
            }
            catch (Exception ex)
            {
                sPL.SendMsg(PrintDLL.PrintLabel.mLogMsgType.Error, ex.Message);
            }
        }

        private void tb_trsncancel_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_trsncancel.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (tb_trsncancel.Text.ToUpper() == "END")
                    {
                        Rollback_Upload_Erp();
                        return;
                    }
                    if (dgvTrsnList.Rows.Count < 1)
                    {
                        throw new Exception("没有物料可以接收");
                    }
                    bool chkflag = false;
                    foreach (DataGridViewRow dgvr in dgvTrsnList.Rows)
                    {
                        if (tb_trsncancel.Text == dgvr.Cells[0].Value.ToString())
                        {
                            #region                         
                            DataRow NEWROW = dt_UpLoad_Erp.NewRow();
                            NEWROW["SHIPPING_NO"] = tb_woId.Text.ToString();
                            NEWROW["KP_NO"] = dgvr.Cells["KP_NO"].Value.ToString();
                            NEWROW["QTY"] = dgvr.Cells["QTY"].Value.ToString();
                            NEWROW["DEB_CRED"] = "BACK";
                            NEWROW["STORE_LOC"] = "1005";
                            if (string.IsNullOrEmpty(dgvr.Cells["STOCK_ID"].Value.ToString()) || dgvr.Cells["STOCK_ID"].Value.ToString() == "NA")
                                NEWROW["MOVE_STLOC"] = "1001";
                            else
                                NEWROW["MOVE_STLOC"] = dgvr.Cells["STOCK_ID"].Value.ToString();
                            NEWROW["PLANT"] = My_Plan;
                            dt_UpLoad_Erp.Rows.Add(NEWROW);
                            #endregion
                            chkflag = true;
                        }
                    }
                    if (chkflag)
                    {
                        string _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(tb_trsncancel.Text, "NA", mFrm.gUserInfo.userId, "1", "NA", "NA");
                        if (_StrErr == "OK")
                        {
                            DeleteReceivedTrSn(tb_trsncancel.Text);
                            ShowMsg(mLogMsgType.Incoming, string.Format("TrSn[{0}]撤销发料完成", tb_trsncancel.Text));
                            Rollback_Upload_Erp(dt_UpLoad_Erp);
                        }
                        else
                        {
                            ShowMsg(mLogMsgType.Error, string.Format("更新 TrSn[{0}] 状态失败,错误提示[{1}]", tb_trsncancel.Text, _StrErr));
                        }
                    }
                    else
                    {
                        ShowMsg(mLogMsgType.Error, "TrSn 刷入错误或不属于该工单");
                    }
                }
                catch (Exception ex)
                {
                    ShowMsg(mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    dt_UpLoad_Erp.Rows.Clear();
                    tb_trsncancel.SelectAll();
                }
            }

        }

        private void ChkRepRint_Click(object sender, EventArgs e)
        {

          //  ChkRepRint.Checked = !ChkRepRint.Checked;
            if (ChkRepRint.Checked)
            {
                imbt_RePrint.Enabled = true;
                buttonX3.Enabled = false;
            }
            else
            {
                imbt_RePrint.Enabled = false;
                buttonX3.Enabled = true;

            }
        }

        private void ClearTextBox()
        {
            _dt = null;
            tb_show_trsn.Text = string.Empty;
            tb_show_pn.Text = string.Empty;
            tb_show_dc.Text = string.Empty;
            tb_show_vc.Text = string.Empty;
            tb_show_lc.Text = string.Empty;
            tb_show_qty.Text = "0";
            tb_show_woId.Text = string.Empty;
            tb_kpdesc.Text = string.Empty;

        }

        string woId = string.Empty;
        private void tb_return_wo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_return_wo.Text) && e.KeyCode == Keys.Enter)
            {
                woId = string.Empty;
                DataTable dtwo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(tb_return_wo.Text,null));
                if (dtwo.Rows.Count > 0)
                {
                    woId = tb_return_wo.Text;
                    ShowMsg_Return(mLogMsgType.Incoming,string.Format( "工单确认完成[{0}]",woId));
                    tb_return_trsn.Focus();
                }
                else
                {
                    ShowMsg_Return(mLogMsgType.Error, "工单不正确....");
                    tb_return_wo.Text = string.Empty;
                }
            }
        }

        private void tb_return_trsn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_return_trsn.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (string.IsNullOrEmpty(woId))
                    {
                        ShowMsg_Return(mLogMsgType.Error, "请先输入工单");
                        return;
                    }

                    string _StrErr = string.Empty;
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_Tr_Sn_Info(tb_return_trsn.Text, out _StrErr));
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["STATUS"].ToString() != "4")
                        {
                            if (Convert.ToInt32(dt.Rows[0]["STATUS"].ToString()) > 1)
                            {
                                dgv_return.Rows.Add(tb_return_trsn.Text, dt.Rows[0]["KP_NO"].ToString(), dt.Rows[0]["KP_DESC"].ToString(), dt.Rows[0]["VENDER_ID"].ToString(), dt.Rows[0]["DATE_CODE"].ToString(), dt.Rows[0]["LOT_CODE"].ToString(), dt.Rows[0]["QTY"].ToString(), woId);
                                if (refWebtR_Tr_Sn.Instance.Update_TR_SN(tb_return_trsn.Text, woId, mFrm.gUserInfo.userId, "4", "NA", "NA") == "OK")
                                    ShowMsg_Return(mLogMsgType.Incoming, string.Format("退料至工单[{0}]完成", woId));
                                else
                                    ShowMsg_Return(mLogMsgType.Error, "更新物料状态失败");
                            }
                            else
                            {
                                ShowMsg_Return(mLogMsgType.Error, "物料未使用过,请走工单撤销....");
                            }
                        }
                        else
                        {
                            ShowMsg_Return(mLogMsgType.Error,string.Format("此物料已退至工单[{0}]",dt.Rows[0]["WOID"].ToString()));
                        }
                    }
                    else
                    {
                        ShowMsg_Return(mLogMsgType.Error, "唯一条码不正确....");
                    }
                }
                catch (Exception ex)
                {
                    ShowMsg_Return(mLogMsgType.Error, "发生异常："+ex.Message);
                }
                finally
                {
                    tb_return_trsn.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 发料过账到SAP
        /// </summary>
        /// <param name="dt"></param>
        private void Upload_Erp(DataTable dt)
        {
            #region 发料过账

            IList<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
            list = FrmBLL.publicfuntion.DataTableIsToDicList(dt);

            string mes_out = refWebtR_Tr_Sn.Instance.WMS_Insert_R_SAP_BACK_SHIPPING(FrmBLL.ReleaseData.ListDictionaryToJson(list));
            if (mes_out == "OK")
                ShowMsg(mLogMsgType.Normal, "写入R_SAP_BACK_SHIPPING成功");
            else
                ShowMsg(mLogMsgType.Error, "Error: R_SAP_BACK_SHIPPING:"+mes_out);
            #endregion
        }

        /// <summary>
        /// 上抛sap
        /// </summary>
        private void Upload_Erp()
        {
            if (!string.IsNullOrEmpty(My_MoNumber))
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("WOID", My_MoNumber);
                dic.Add("DEBCRED", "OUT");
                DataTable dt_out = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.WMS_get_R_Sap_Back_Shipping(FrmBLL.ReleaseData.DictionaryToJson(dic)));
                if (dt_out.Rows.Count > 0)
                {
                    IList<IDictionary<string, object>> list_out = new List<IDictionary<string, object>>();
                    list_out = FrmBLL.publicfuntion.DataTableIsToDicList(dt_out);
                    DataTable dt_sap = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.WMS_RFC_ZMM_GOODSMVT_CREATE_list(FrmBLL.ReleaseData.ListDictionaryToJson(list_out)));
                    if (dt_sap.Rows[0][2].ToString() == "S")
                    {
                        string _StrErr = refWebtR_Tr_Sn.Instance.WMS_Update_R_Sap_Back_Shipping(FrmBLL.ReleaseData.ListDictionaryToJson(list_out), dt_sap.Rows[0][0].ToString());
                        if (_StrErr == "OK")
                            ShowMsg(mLogMsgType.Incoming, string.Format("上传SAP成功,交易单号[{0}]", dt_sap.Rows[0][0].ToString()));
                        else
                            ShowMsg(mLogMsgType.Error, string.Format("更新SFIS上抛资料失败" + _StrErr));
                    }
                    else
                    {
                        string sSAP_MSG = string.Format("上传SAP失败,MSG:{0}，请手动上抛SAP", dt_sap.Rows[0][2].ToString() + ":" + dt_sap.Rows[0][5].ToString());
                        ShowMsg(mLogMsgType.Error, sSAP_MSG);
                    }
                }
                else                
                    ShowMsg(mLogMsgType.Error, "没有待上传数据");
                 
            }
            else           
                ShowMsg(mLogMsgType.Error, "请先确认工单...");
            
        }
       

        /// <summary>
        /// 撤销发料过账
        /// </summary>
        /// <param name="dt"></param>
        private void Rollback_Upload_Erp(DataTable dt)
        {
            #region 撤销发料过账
            IList<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
            list = FrmBLL.publicfuntion.DataTableIsToDicList(dt);
            string mes_out = refWebtR_Tr_Sn.Instance.WMS_Insert_R_SAP_BACK_SHIPPING(FrmBLL.ReleaseData.ListDictionaryToJson(list));
            if (mes_out == "OK")
                ShowMsg(mLogMsgType.Normal, "写入R_SAP_BACK_SHIPPING成功");
            else
                ShowMsg(mLogMsgType.Error, "Error: R_SAP_BACK_SHIPPING:" + mes_out);
            #endregion
        }
        /// <summary>
        /// 上抛SAP
        /// </summary>
        private void Rollback_Upload_Erp()
        {
            if (!string.IsNullOrEmpty(My_MoNumber))
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("WOID", My_MoNumber);
                dic.Add("DEBCRED", "BACK");
                DataTable dt_out = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.WMS_get_R_Sap_Back_Shipping(FrmBLL.ReleaseData.DictionaryToJson(dic)));
                if (dt_out.Rows.Count > 0)
                {
                    IList<IDictionary<string, object>> list_out = new List<IDictionary<string, object>>();
                    list_out = FrmBLL.publicfuntion.DataTableIsToDicList(dt_out);
                    DataTable dt_sap = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.WMS_RFC_ZMM_GOODSMVT_CREATE_list(FrmBLL.ReleaseData.ListDictionaryToJson(list_out)));
                    if (dt_sap.Rows[0][2].ToString() == "S")
                    {
                        string _StrErr = refWebtR_Tr_Sn.Instance.WMS_Update_R_Sap_Back_Shipping(FrmBLL.ReleaseData.ListDictionaryToJson(list_out), dt_sap.Rows[0][0].ToString());
                        if (_StrErr == "OK")
                            ShowMsg(mLogMsgType.Incoming, string.Format("上传SAP成功,交易单号[{0}]", dt_sap.Rows[0][0].ToString()));
                        else
                            ShowMsg(mLogMsgType.Error, string.Format("更新SFIS上抛资料失败" + _StrErr));
                    }
                    else
                    {
                        string sSAP_MSG = string.Format("上传SAP失败,MSG:{0}，请手动上抛SAP", dt_sap.Rows[0][2].ToString() + ":" + dt_sap.Rows[0][5].ToString());
                        ShowMsg(mLogMsgType.Error, sSAP_MSG);
                    }
                }
                else
                    ShowMsg(mLogMsgType.Error, "没有待撤销数据");
            }
            else
            {
                ShowMsg(mLogMsgType.Error, "请先确认工单...");
            }
        }

        private bool Get_WO_Erp_Info(string woId)
        {
            bool Flag = false;
            try
            {
                DataTable dt_Erp_wo = FrmBLL.ReleaseData.arrByteToDataTable(refWebt_wo_Info_Erp.Instance.Get_WO_Info_Erp(woId, "WOID,FACTORYID"));
                if (dt_Erp_wo.Rows.Count == 0)
                    throw new Exception("ERP 未推送工单信息");

                My_MoNumber = dt_Erp_wo.Rows[0]["WOID"].ToString();
                My_Plan = dt_Erp_wo.Rows[0]["FACTORYID"].ToString();
                ShowMsg(mLogMsgType.Incoming, string.Format("工单[{0}],工厂[{1}]", My_MoNumber, My_Plan));
                Flag = true;
            }
            catch (Exception ex)
            {
                ShowMsg(mLogMsgType.Error, ex.Message);
            }

            return Flag;
        }

        private void txt_InputwoId_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_InputwoId.Text) && e.KeyCode == Keys.Enter)
            {
                return_woId = txt_InputwoId.Text;            
            }
        }
 
    }
}
