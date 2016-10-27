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
    public partial class Frm_SpecifiedMaterial : Office2007Form
    {
        public Frm_SpecifiedMaterial(Frm_MaterialManage Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Frm_MaterialManage mFrm;
        string My_MoNumber = string.Empty;
        List<string> Ls_My_MoNumber = new List<string>();
        private void Frm_SpecifiedMaterial_Load(object sender, EventArgs e)
        {
            cbx_stock_id.SelectedIndex = 0;
        }

        private void txt_woId_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_woId.Text) && e.KeyCode == Keys.Enter)
            {
                My_MoNumber = string.Empty;
                Ls_My_MoNumber.Clear();
                DataTable dtwo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(txt_woId.Text, null));
                if (dtwo.Rows.Count > 0)
                {
                    My_MoNumber = txt_woId.Text;
                    Ls_My_MoNumber.Add(My_MoNumber);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("NEW_WOID", My_MoNumber);
                    DataTable dt_MoMorge = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtWoMerge.Instance.Get_Smt_WO_Merge(FrmBLL.ReleaseData.DictionaryToJson(dic), "OLD_WOID,MERGE_NUM"));
                    //if (dt_MoMorge.Rows.Count > 0)
                    //    Ls_My_MoNumber.Add(dt_MoMorge.Rows[0]["OLD_WOID"].ToString());          
                    if (dt_MoMorge.Rows.Count > 0)
                    {
                        dic = new Dictionary<string, object>();
                        dic.Add("MERGE_NUM", dt_MoMorge.Rows[0]["MERGE_NUM"].ToString());
                        dt_MoMorge = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtWoMerge.Instance.Get_Smt_WO_Merge(FrmBLL.ReleaseData.DictionaryToJson(dic), "NEW_WOID,OLD_WOID"));
                        foreach (DataRow dr in dt_MoMorge.Rows)
                        {
                            if (!Ls_My_MoNumber.Contains(dr["NEW_WOID"].ToString()))
                                Ls_My_MoNumber.Add(dr["NEW_WOID"].ToString());
                            if (!Ls_My_MoNumber.Contains(dr["OLD_WOID"].ToString()))
                                Ls_My_MoNumber.Add(dr["OLD_WOID"].ToString());
                        }
                    }

                    string MoNumberStr = string.Empty;
                    foreach (string itemstr in Ls_My_MoNumber)
                    {
                        MoNumberStr +=itemstr+ ",";
                    }

                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("工单确认完成[{0}]", MoNumberStr.Substring(0,MoNumberStr.Length-1)));
                    txt_tr_sn.Focus();
                }
                else
                {
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "工单不正确....");
                    txt_woId.Text = string.Empty;
                }
            }
        }

        private void txt_tr_sn_KeyDown(object sender, KeyEventArgs e)
        {

            if (!string.IsNullOrEmpty(txt_tr_sn.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (string.IsNullOrEmpty(My_MoNumber))
                    {
                        mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "请先输入工单");
                        return;
                    }


                    string _StrErr = string.Empty;
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_Tr_Sn_Info(txt_tr_sn.Text, out _StrErr));
                    if (dt.Rows.Count > 0)
                    {
                        if (!Ls_My_MoNumber.Contains(dt.Rows[0]["WOID"].ToString()))
                            throw new Exception("工单不同-->" + dt.Rows[0]["WOID"].ToString());

                        //if (dt.Rows[0]["STATUS"].ToString() == "2")
                        //{
                            if (Convert.ToInt32(dt.Rows[0]["STATUS"].ToString()) == 2) //状态为2可以指定工单退料
                            {
                                dgv_return.Rows.Add(txt_tr_sn.Text, dt.Rows[0]["KP_NO"].ToString(), dt.Rows[0]["KP_DESC"].ToString(), dt.Rows[0]["VENDER_ID"].ToString(), dt.Rows[0]["DATE_CODE"].ToString(), dt.Rows[0]["LOT_CODE"].ToString(), dt.Rows[0]["QTY"].ToString(), My_MoNumber);
                             

                                Dictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add("TR_SN", txt_tr_sn.Text);
                                dic.Add("WOID", My_MoNumber);
                                dic.Add("USER_ID", mFrm.UserId);
                                dic.Add("STATUS", "4");
                                if (cbx_stock_id.SelectedIndex == 1)
                                    dic.Add("STOCK_ID", "1010");
                              
                                    _StrErr = refWebtR_Tr_Sn.Instance.UPDATE_TR_SN(FrmBLL.ReleaseData.DictionaryToJson(dic));
                                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Normal, string.Format("UPDATE TR_SN [{0}]", _StrErr));

                                    //if (Convert.ToInt32(dt.Rows[0]["STATUS"].ToString()) == 2)
                                    //{
                                        dic = new Dictionary<string, object>();
                                        dic.Add("TR_SN", txt_tr_sn.Text);
                                        dic.Add("KP_NO", dt.Rows[0]["KP_NO"].ToString());
                                        dic.Add("VENDER_ID", dt.Rows[0]["VENDER_ID"].ToString());
                                        dic.Add("DATE_CODE", dt.Rows[0]["DATE_CODE"].ToString());
                                        dic.Add("LOT_CODE", dt.Rows[0]["LOT_CODE"].ToString());
                                        dic.Add("QTY", dt.Rows[0]["QTY"].ToString());
                                        dic.Add("WOID", My_MoNumber);
                                        dic.Add("USER_ID", mFrm.UserId);
                                        dic.Add("STATUS", "4");
                                        _StrErr = refWebtWOMaterial.Instance.Insert_T_WO_Material(FrmBLL.ReleaseData.DictionaryToJson(dic));
                                    //}
                                    //else
                                    //{
                                    //    dic = new Dictionary<string, object>();
                                    //    dic.Add("TR_SN", txt_tr_sn.Text);
                                    //    dic.Add("WOID", My_MoNumber);
                                    //    _StrErr = refWebtWOMaterial.Instance.UPDATE_T_WO_Material(FrmBLL.ReleaseData.DictionaryToJson(dic));
                                    //}
                                        mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Normal, string.Format("Insert_T_WO_Material [{0}]", _StrErr));
                                if (_StrErr == "OK")
                                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("退料至工单[{0}]完成", My_MoNumber));
                                else
                                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "更新物料状态失败: " + _StrErr);
                            }
                            else
                            {
                                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "物料状态错误-->" + FrmBLL.publicfuntion.MaterialStatus(dt.Rows[0]["STATUS"].ToString()));
                            }
                        //}
                        //else
                        //{
                        //    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, string.Format("此物料已退至工单[{0}]", dt.Rows[0]["WOID"].ToString()));
                        //}
                    }
                    else
                    {
                        mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "唯一条码不正确....");
                    }
                }
                catch (Exception ex)
                {
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    txt_tr_sn.Text = string.Empty;
                }
            }
        }
    }
}