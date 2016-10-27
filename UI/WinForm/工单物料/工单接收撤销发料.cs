using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class Frm_ReceiveAndBack : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_ReceiveAndBack( Frm_MaterialManage Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Frm_MaterialManage mFrm;

        /// <summary>
        /// 保存工单物料储位信息
        /// </summary>
        Dictionary<string, string> dic_loc = null;

        private readonly string strFnumName = "ReceivingMaterial";

        /// <summary>
        /// 物料发料退料工单
        /// </summary>
        public string My_MoNumber = string.Empty;
        public string My_Plan = string.Empty;

        /// <summary>
        /// 准备上抛SAP信息
        /// </summary>
        DataTable dt_UpLoad_Erp = new DataTable("mydt");

        private void Frm_ReceiveAndBack_Load(object sender, EventArgs e)
        {
            dt_UpLoad_Erp.Columns.Add("SHIPPING_NO");
            dt_UpLoad_Erp.Columns.Add("KP_NO");
            dt_UpLoad_Erp.Columns.Add("QTY");
            dt_UpLoad_Erp.Columns.Add("DEB_CRED");
            dt_UpLoad_Erp.Columns.Add("STORE_LOC");
            dt_UpLoad_Erp.Columns.Add("MOVE_STLOC");
            dt_UpLoad_Erp.Columns.Add("PLANT");
        }

        private void tb_woId_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_woId.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {

                    //if (!Chk_User_Editing(tb_woId.Text))
                    //    return;

                    dgvmtrlist.Rows.Clear();
                    dgvTrsnList.Rows.Clear();


                    if (!Get_WO_Erp_Info(tb_woId.Text))
                        return;

                    DataTable dtwoInfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoBomInfo.Instance.GetWoBomInfo(tb_woId.Text));
                    dic_loc = new Dictionary<string, string>();
                    foreach (DataRow dr in dtwoInfo.Rows)
                    {
                        dgvmtrlist.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                        if (!dic_loc.ContainsKey(dr[2].ToString()))
                            dic_loc.Add(dr[2].ToString(), dr[7].ToString());
                    }

                    DataTable dtTrsnList = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_woId_Trsn_List(tb_woId.Text, null));
                    foreach (DataRow dr in dtTrsnList.Rows)
                    {
                        if (dr["STATUS"].ToString() == "1")
                        {
                            dgvTrsnList.Rows.Add(dr["TR_SN"].ToString(), dr["KP_NO"].ToString(), dr["VENDER_ID"].ToString(), dr["DATE_CODE"].ToString(), dr["LOT_CODE"].ToString(), dr["QTY"].ToString(), dr["STOCK_ID"].ToString());
                        }
                    }

                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Warning, "正在计算发料总数......");
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

                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, "计算发料总数完成......");

                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, "查询完成...");
                }
                catch (Exception ex)
                {
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    tb_woId.SelectAll();
                }
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
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("工单[{0}],工厂[{1}]", My_MoNumber, My_Plan));
                Flag = true;
            }
            catch (Exception ex)
            {
               mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, ex.Message);
            }

            return Flag;
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
                            if (!CHECK_TR_SN_Status(tb_trsn.Text,1))
                                return;
                            DataRow NEWROW = dt_UpLoad_Erp.NewRow();
                            NEWROW["SHIPPING_NO"] = My_MoNumber;
                            NEWROW["KP_NO"] = dgvr.Cells["KP_NO"].Value.ToString();
                            NEWROW["QTY"] = dgvr.Cells["QTY"].Value.ToString();
                            NEWROW["DEB_CRED"] = "OUT";
                            if (string.IsNullOrEmpty(dgvr.Cells["STOCK_ID"].Value.ToString()) || dgvr.Cells["STOCK_ID"].Value.ToString() == "NA")
                                NEWROW["STORE_LOC"] = "1001";
                            else
                                NEWROW["STORE_LOC"] = dgvr.Cells[6].Value.ToString();
                            NEWROW["MOVE_STLOC"] = dic_loc[dgvr.Cells["KP_NO"].Value.ToString()]; //"1005";
                            NEWROW["PLANT"] = My_Plan;
                            dt_UpLoad_Erp.Rows.Add(NEWROW);

                            chkflag = true;
                        }
                    }
                    if (chkflag)
                    {
                        Dictionary<string, object> mst = new Dictionary<string, object>();
                        mst.Add("TR_SN", tb_trsn.Text);
                        mst.Add("USER_ID", mFrm.UserId);
                        mst.Add("STATUS", 2);
                        string _StrErr = refWebtWOMaterial.Instance.Insert_T_WO_Material_Update_Status(FrmBLL.ReleaseData.DictionaryToJson(mst));
                       // string _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(tb_trsn.Text, "NA", mFrm.UserId, "2", "NA", "NA");
                        if (_StrErr == "OK")
                        {
                            DeleteReceivedTrSn(tb_trsn.Text);
                            mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("TrSn[{0}]备料完成", tb_trsn.Text));
                            Upload_Erp(dt_UpLoad_Erp);
                        }
                        else
                        {
                            mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, string.Format("更新 TrSn[{0}] 状态失败,错误提示[{1}]", tb_trsn.Text, _StrErr));
                        }
                    }
                    else
                    {
                        mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "TrSn 刷入错误或不属于该工单");
                    }
                }
                catch (Exception ex)
                {
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, ex.Message);
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
               mFrm. ShowMsg(Frm_MaterialManage.mLogMsgType.Normal, "写入R_SAP_BACK_SHIPPING成功");
            else
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "Error: R_SAP_BACK_SHIPPING:" + mes_out);
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
                            mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("上传SAP成功,交易单号[{0}]", dt_sap.Rows[0][0].ToString()));
                        else
                            mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, string.Format("更新SFIS上抛资料失败" + _StrErr));
                    }
                    else
                    {
                        string sSAP_MSG = string.Format("上传SAP失败,MSG:{0}，请手动上抛SAP", dt_sap.Rows[0][2].ToString() + ":" + dt_sap.Rows[0][5].ToString());
                        mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, sSAP_MSG);
                    }
                }
                else
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "没有待上传数据");

            }
            else
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "请先确认工单...");

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
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Normal, "写入R_SAP_BACK_SHIPPING成功");
            else
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "Error: R_SAP_BACK_SHIPPING:" + mes_out);
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
                            mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("上传SAP成功,交易单号[{0}]", dt_sap.Rows[0][0].ToString()));
                        else
                            mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, string.Format("更新SFIS上抛资料失败" + _StrErr));
                    }
                    else
                    {
                        string sSAP_MSG = string.Format("上传SAP失败,MSG:{0}，请手动上抛SAP", dt_sap.Rows[0][2].ToString() + ":" + dt_sap.Rows[0][5].ToString());
                        mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, sSAP_MSG);
                    }
                }
                else
                   mFrm. ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "没有待撤销数据");
            }
            else
            {
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "请先确认工单...");
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
                            NEWROW["SHIPPING_NO"] = My_MoNumber;
                            NEWROW["KP_NO"] = dgvr.Cells[1].Value.ToString();
                            NEWROW["QTY"] = dgvr.Cells[5].Value.ToString();
                            NEWROW["DEB_CRED"] = "OUT";
                            if (string.IsNullOrEmpty(dgvr.Cells[6].Value.ToString()) || dgvr.Cells[6].Value.ToString() == "NA")
                                NEWROW["STORE_LOC"] = "1001";
                            else
                                NEWROW["STORE_LOC"] = dgvr.Cells[6].Value.ToString();
                            NEWROW["MOVE_STLOC"] = dic_loc[dgvr.Cells[1].Value.ToString()]; // "1005";
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
                            Dictionary<string, object> mst = new Dictionary<string, object>();
                            mst.Add("TR_SN", Item);
                            mst.Add("USER_ID", mFrm.UserId);
                            mst.Add("STATUS", 2);
                            string _StrErr = refWebtWOMaterial.Instance.Insert_T_WO_Material_Update_Status(FrmBLL.ReleaseData.DictionaryToJson(mst));
                         //   string _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(Item, "NA", mFrm.UserId, "2", "NA", "NA");
                            if (_StrErr == "OK")
                            {
                                DeleteReceivedTrSn(Item);
                                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("TrSn[{0}]接收备料完成", Item));
                            }
                        }


                        #region 发料过账
                        Upload_Erp(dt_UpLoad_Erp);
                        Upload_Erp();
                        #endregion



                        mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("接收备料完成...."));
                        tb_woId.SelectAll();
                    }
                }
                catch (Exception ex)
                {
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, string.Format("Exection: {0}", ex.Message));
                }
                finally
                {
                    dt_UpLoad_Erp.Rows.Clear();
                }
            }
            else
            {
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "没有物料可以接收");
            }
        }

        private void tb_wo_cancel_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_wo_cancel.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    //if (!Chk_User_Editing(tb_woId.Text))
                    //    return;

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

                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, "查询工单物料完成...");
                }
                catch (Exception ex)
                {
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    tb_wo_cancel.SelectAll();
                }
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
                            if (!CHECK_TR_SN_Status(tb_trsn.Text, 2))
                                return;

                            #region
                            DataRow NEWROW = dt_UpLoad_Erp.NewRow();
                            NEWROW["SHIPPING_NO"] = My_MoNumber;
                            NEWROW["KP_NO"] = dgvr.Cells["KP_NO"].Value.ToString();
                            NEWROW["QTY"] = dgvr.Cells["QTY"].Value.ToString();
                            NEWROW["DEB_CRED"] = "BACK";
                            NEWROW["STORE_LOC"] = dic_loc[dgvr.Cells["KP_NO"].Value.ToString()];// "1005";
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
                        Dictionary<string, object> mst = new Dictionary<string, object>();
                        mst.Add("TR_SN", tb_trsncancel.Text);
                        mst.Add("USER_ID", mFrm.UserId);
                        mst.Add("STATUS", 1);
                        mst.Add("WOID", My_MoNumber);
                        string _StrErr = refWebtWOMaterial.Instance.Insert_T_WO_Material_Update_Status(FrmBLL.ReleaseData.DictionaryToJson(mst));
                       // string _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(tb_trsncancel.Text, "NA", mFrm.UserId, "1", "NA", "NA");
                        if (_StrErr == "OK")
                        {
                            DeleteReceivedTrSn(tb_trsncancel.Text);
                            mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("TrSn[{0}]撤销发料完成", tb_trsncancel.Text));
                            Rollback_Upload_Erp(dt_UpLoad_Erp);
                        }
                        else
                        {
                            mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, string.Format("更新 TrSn[{0}] 状态失败,错误提示[{1}]", tb_trsncancel.Text, _StrErr));
                        }
                    }
                    else
                    {
                        mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "TrSn 刷入错误或不属于该工单");
                    }
                }
                catch (Exception ex)
                {
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    dt_UpLoad_Erp.Rows.Clear();
                    tb_trsncancel.SelectAll();
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
                            NEWROW["SHIPPING_NO"] = My_MoNumber;
                            NEWROW["KP_NO"] = dgvr.Cells[1].Value.ToString();
                            NEWROW["QTY"] = dgvr.Cells[5].Value.ToString();
                            NEWROW["DEB_CRED"] = "BACK";
                            NEWROW["STORE_LOC"] = dic_loc[dgvr.Cells[1].Value.ToString()]; //"1005";
                            if (string.IsNullOrEmpty(dgvr.Cells[6].Value.ToString()) || dgvr.Cells[6].Value.ToString() == "NA")
                                NEWROW["MOVE_STLOC"] = "1001";
                            else
                                NEWROW["MOVE_STLOC"] = dgvr.Cells[6].Value.ToString();
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
                            Dictionary<string, object> mst = new Dictionary<string, object>();
                            mst.Add("TR_SN", Item);
                            mst.Add("USER_ID", mFrm.UserId);
                            mst.Add("STATUS",1);
                            mst.Add("WOID", My_MoNumber);
                            string _StrErr = refWebtWOMaterial.Instance.Insert_T_WO_Material_Update_Status(FrmBLL.ReleaseData.DictionaryToJson(mst));
                            //string _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(Item, "NA", mFrm.UserId, "1", "NA", "NA");
                            if (_StrErr == "OK")
                            {
                                DeleteReceivedTrSn(Item);
                                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("TrSn[{0}]撤销接收备料完成", Item));
                            }
                        }

                        #region 撤销发料过账
                        Rollback_Upload_Erp(dt_UpLoad_Erp);
                        Rollback_Upload_Erp();
                        #endregion

                        mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("撤销接收备料完成...."));
                        tb_wo_cancel.SelectAll();
                    }
                }
                catch (Exception ex)
                {
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("Execption: {0}", ex.Message));
                }
                finally
                {
                    dt_UpLoad_Erp.Rows.Clear();
                }
            }
            else
            {
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "没有物料可以撤销");
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

        private bool Chk_User_Editing(string woId)
        {
            #region 检查是否用户是否正在编辑此流程
            List<string> LsIp = new List<string>();
            LsIp = FrmBLL.publicfuntion.GetIPList();

            string err = FrmBLL.publicfuntion.ChktEditing(woId, strFnumName, this.mFrm.UserId, this.mFrm.UserName);
            if (err != "OK")
            {
                if (err.IndexOf("ERROR") != -1)
                {
                    this.mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error,err );
                    return false;
                }
                else
                {
                    MessageBoxEx.Show(string.Format("用户:[{0}]/[{1}]--{2}正在作业此工单,请选择其它工单!!",
err.Split(':')[1].Split(',')[0], err.Split(':')[1].Split(',')[1], err.Split(':')[1].Split(',')[2]), "提示!!");
                    return false;
                }
            }
            #endregion

            return true;
        }

        private void Frm_ReceiveAndBack_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                refwebtEditing.Instance.DeletetEditingByUserIdAndPrj(this.mFrm.UserId, this.strFnumName);
            }
            catch
            {
            }
        }

        private bool CHECK_TR_SN_Status(string tr_sn,int status)
        {
            bool ChkStatus = false;
            string _StrErr=string.Empty;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_Tr_Sn_Info(tr_sn,out _StrErr));
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["STATUS"].ToString()) == status)
                {
                    ChkStatus = true;
                }
                else
                {
                    _StrErr = FrmBLL.publicfuntion.Material_Status(dt.Rows[0]["STATUS"].ToString());
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error,string.Format( "物料条码状态异常:[{0}]",_StrErr));
                }

            }
            else
            {
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "物料条码不正确");
            }

            return ChkStatus;

        }
    }
}