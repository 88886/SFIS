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
using LabelManager2;
using System.IO;

namespace SFIS_V2
{
    public partial class Frm_ReturnofMaterial : Office2007Form //Form
    {
        public Frm_ReturnofMaterial(Frm_MaterialManage Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Frm_MaterialManage mFrm;
        ApplicationClass lbl = null;
        string My_MoNumber = string.Empty;
        List<string> LsMoNumber = new List<string>();
        DataTable dt_return = null;
        private void Frm_ReturnofMaterial_Load(object sender, EventArgs e)
        {
            try
            {
                lbl = new ApplicationClass();
                LabelFlePatch.Text = Directory.GetCurrentDirectory() + "\\LabelFile\\FEIXUN_LOT.lab";
                txt_tr_sn.Focus();
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Normal, "退料流程 步骤1:输入工单  步骤2:输入料盘");
            }
            catch
            {
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "初始化打印接口失败");
            }
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

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void txt_tr_sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_tr_sn.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {                  

                    string InputStr=txt_tr_sn.Text.Trim();
                    dt_return = null;
                    CleatTxt();
                    if (InputStr == "UNDO")
                    {
                        My_MoNumber = string.Empty;
                        LsMoNumber.Clear();
                        SendMsg(mLogMsgType.Incoming, "UNDO OK");
                        return;
                    }

                    if (string.IsNullOrEmpty(My_MoNumber))
                    {
                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable( refWebtWoInfo.Instance.GetWoInfo(InputStr, null, "WOID"));
                        if (dt.Rows.Count > 0)
                        {
                            My_MoNumber = InputStr;
                            LsMoNumber.Add(My_MoNumber);
                            Dictionary<string,object> mst = new Dictionary<string,object>();
                            mst.Add("NEW_WOID",My_MoNumber);
                            DataTable dt_MoMorge = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtWoMerge.Instance.Get_Smt_WO_Merge(FrmBLL.ReleaseData.DictionaryToJson(mst), "OLD_WOID,MERGE_NUM"));
                            if (dt_MoMorge.Rows.Count > 0)
                            {
                                mst = new Dictionary<string, object>();
                                mst.Add("MERGE_NUM", dt_MoMorge.Rows[0]["MERGE_NUM"].ToString());
                                dt_MoMorge = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtWoMerge.Instance.Get_Smt_WO_Merge(FrmBLL.ReleaseData.DictionaryToJson(mst), "NEW_WOID,OLD_WOID"));
                                foreach (DataRow dr in dt_MoMorge.Rows)
                                {
                                    if (!LsMoNumber.Contains(dr["NEW_WOID"].ToString()))
                                        LsMoNumber.Add(dr["NEW_WOID"].ToString());
                                    if (!LsMoNumber.Contains(dr["OLD_WOID"].ToString()))
                                        LsMoNumber.Add(dr["OLD_WOID"].ToString());
                                }
                            }

                            string Total_woId = string.Empty;
                            foreach (string itemStr in LsMoNumber)
                            {
                                Total_woId += itemStr + ",";
                            }

                            SendMsg(mLogMsgType.Incoming, string.Format("工单[{0}] 可退到 工单[{1}]", Total_woId.Substring(0, Total_woId.Length - 1), My_MoNumber));
                        }
                        else
                        {
                            My_MoNumber = string.Empty;
                            SendMsg(mLogMsgType.Error, "工单错误");
                        }
                    }
                    else
                    {

                        string _StrErr = string.Empty;
                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_Tr_Sn_Info(txt_tr_sn.Text, out _StrErr));
                        if (dt.Rows.Count > 0)
                        {
                            int C_STATUS= Convert.ToInt32( dt.Rows[0]["STATUS"].ToString());
                            if (C_STATUS == 7 || C_STATUS == 8)
                            {
                                if (LsMoNumber.Contains(dt.Rows[0]["WOID"].ToString()))
                                {
                                    dt_return = dt;
                                    FrmBLL.publicfuntion.Fill_Control(PanelData, dt);
                                    SendMsg(mLogMsgType.Normal, InputStr + " OK");
                                    txt_qty.Focus();
                                }
                                else
                                {
                                    SendMsg(mLogMsgType.Error, "工单不同->" + dt.Rows[0]["WOID"].ToString());
                                }
                            }
                            else
                            {
                                SendMsg(mLogMsgType.Error, "物料状态异常: " + FrmBLL.publicfuntion.Material_Status(C_STATUS.ToString()));
                            }
                        }
                        else
                        {
                            SendMsg(mLogMsgType.Error, "TR_SN 错误");
                        }
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

        private enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error } //文字输出类型

        private void SendMsg(mLogMsgType MsgType,string Msg)
        {
            switch (MsgType)
            {
                case mLogMsgType.Incoming:
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, Msg);
                    break;
                case mLogMsgType.Outgoing:
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Outgoing, Msg);
                    break;
                case mLogMsgType.Normal:
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Normal, Msg);
                    break;
                case mLogMsgType.Warning:
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Warning, Msg);
                    break;
                case mLogMsgType.Error:
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, Msg);
                    break;
            }
           
        }
        private void txt_tr_sn_TextChanged(object sender, EventArgs e)
        {

        }

        private void imbt_Print_Click(object sender, EventArgs e)
        {
            if (dt_return!=null && dt_return.Rows.Count > 0)
            {
                try
                {
                    if (Convert.ToInt32(txt_qty.Text) >= Convert.ToInt32(dt_return.Rows[0]["QTY"].ToString()))
                    {
                        SendMsg(mLogMsgType.Error, string.Format("退料打印数量错误,新料盘数量必须小于原数量,原数量为[{0}],新料盘数量[{1}]", dt_return.Rows[0]["QTY"].ToString(), txt_qty.Text));
                    }
                    else
                    {

                        // if (_dt.Rows[0]["STATUS"].ToString() != "7") //使用后方可退料
                        if (Convert.ToInt32(dt_return.Rows[0]["STATUS"].ToString()) < 2) //线边仓接收后,可做退料                       
                            throw new Exception("线边仓未接收物料,不可退料打印");                        
                        if (Convert.ToInt32(dt_return.Rows[0]["STATUS"].ToString()) == 9)                       
                            throw new Exception("物料已经拆分,不可退料打印");                       
                        if (Convert.ToInt32(dt_return.Rows[0]["STATUS"].ToString()) == 10)                        
                            throw new Exception("物料已盘点出库,不可退料打印");
                        
                        string Tr_Sn = refWebtR_Tr_Sn.Instance.GetSeqTrSnInfo();
                        if (string.IsNullOrEmpty(Tr_Sn))
                        {
                            throw new Exception("获取新的Trsn号码错误");
                        }

                        Dictionary<string, object> dic = new Dictionary<string, object>();
                     
                        dic.Add("PO_ID", dt_return.Rows[0]["PO_ID"].ToString());
                        dic.Add("TR_SN", Tr_Sn);                    
                        dic.Add("KP_NO", dt_return.Rows[0]["KP_NO"].ToString());
                        dic.Add("VENDER_ID", dt_return.Rows[0]["VENDER_ID"].ToString());                      
                        dic.Add("VENDER_NAME", dt_return.Rows[0]["VENDER_NAME"].ToString());
                        dic.Add("DATE_CODE", dt_return.Rows[0]["DATE_CODE"].ToString());
                        dic.Add("LOT_CODE", dt_return.Rows[0]["LOT_CODE"].ToString());
                        dic.Add("QTY", txt_qty.Text);
                        dic.Add("STOCK_ID", dt_return.Rows[0]["STOCK_ID"].ToString());
                        dic.Add("LOC_ID", dt_return.Rows[0]["LOC_ID"].ToString());
                        dic.Add("TANSFER_NO", dt_return.Rows[0]["TANSFER_NO"].ToString());
                        dic.Add("URGENT_PN", dt_return.Rows[0]["URGENT_PN"].ToString());
                        dic.Add("STOCK_NO", dt_return.Rows[0]["STOCK_NO"].ToString());
                        dic.Add("STOCK_DATE", dt_return.Rows[0]["STOCK_DATE"].ToString());
                        dic.Add("WOID", My_MoNumber);
                        dic.Add("USER_ID", mFrm.UserId);
                        dic.Add("KP_DESC", dt_return.Rows[0]["KP_DESC"].ToString());
                        dic.Add("STATUS", "NA");
                        dic.Add("FIFO_DC", dt_return.Rows[0]["FIFO_DC"].ToString());

                        Dictionary<string, object> mst = new Dictionary<string, object>();
                        mst.Add("TR_SN", Tr_Sn);
                        mst.Add("KP_NO", dt_return.Rows[0]["KP_NO"].ToString());
                        mst.Add("VENDER_ID", dt_return.Rows[0]["VENDER_ID"].ToString());
                        mst.Add("DATE_CODE", dt_return.Rows[0]["DATE_CODE"].ToString());
                        mst.Add("LOT_CODE", dt_return.Rows[0]["LOT_CODE"].ToString());
                        mst.Add("QTY", txt_qty.Text);
                        mst.Add("WOID", My_MoNumber);
                        mst.Add("USER_ID", mFrm.UserId);
                        mst.Add("STATUS", 4);
                        string _StrErr = refWebtWOMaterial.Instance.Insert_T_WO_Material(FrmBLL.ReleaseData.DictionaryToJson(mst));
                        if (_StrErr != "OK")
                            throw new Exception("写入工单物料信息失败:"+_StrErr);
                         _StrErr = refWebtR_Tr_Sn.Instance.insert_into_R_tr_sn(FrmBLL.ReleaseData.DictionaryToJson(dic));
                        if (_StrErr == "OK")
                        {
                            //更新原Trsn状态为9 盘点完成,并记录新的Trsn
                            _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(dt_return.Rows[0]["TR_SN"].ToString(), "NA", mFrm.UserId, "9", "NA", Tr_Sn);

                            //更新新的Trsn状态,并记录原Trsn
                            _StrErr = refWebtR_Tr_Sn.Instance.Update_TR_SN(Tr_Sn, "NA", mFrm.UserId, "4", dt_return.Rows[0]["TR_SN"].ToString(), "NA");
                            if (_StrErr == "OK")
                            {
                                SendMsg(mLogMsgType.Incoming, string.Format("开始打印Tr_Sn[{0}].", Tr_Sn));
                                dgvCancelList.Rows.Add(My_MoNumber, Tr_Sn, dt_return.Rows[0]["KP_NO"].ToString(), dt_return.Rows[0]["VENDER_ID"].ToString(), dt_return.Rows[0]["DATE_CODE"].ToString(), dt_return.Rows[0]["LOT_CODE"].ToString(), txt_qty.Text, dt_return.Rows[0]["KP_DESC"].ToString());

                                Dictionary<string, string> dicPrint = new Dictionary<string, string>();

                                dicPrint.Add("TR_SN", Tr_Sn);
                                dicPrint.Add("PART_NO", dt_return.Rows[0]["KP_NO"].ToString());
                                dicPrint.Add("DATE_CODE", dt_return.Rows[0]["DATE_CODE"].ToString());
                                dicPrint.Add("UNIT_SIZE", txt_qty.Text);
                                dicPrint.Add("VENDER_CODE", dt_return.Rows[0]["VENDER_ID"].ToString());
                                dicPrint.Add("LOT_ID", dt_return.Rows[0]["LOT_CODE"].ToString());
                                dicPrint.Add("EMP_NO", mFrm.UserId);
                                dicPrint.Add("REMARK", dt_return.Rows[0]["KP_DESC"].ToString());
                                dicPrint.Add("STORLOC", string.Format("{0}/{1}", dt_return.Rows[0]["STOCK_ID"].ToString(), dt_return.Rows[0]["LOC_ID"].ToString()));
                                PublicPrintLabel(dicPrint);

                                dt_return = null;
                                CleatTxt();
                                txt_tr_sn.Focus();

                            }
                            else
                            {
                                SendMsg(mLogMsgType.Error, string.Format("更新条码[{0}]状态失败,提示信息【{1}】...", Tr_Sn, _StrErr));
                            }
                        }
                        else
                        {
                            //  Show_Msg.SendMsg(rtbcancel, Show_Msg.mLogMsgType.Error, string.Format("插入新条码信息失败[{0}]...", _StrErr));
                            SendMsg(mLogMsgType.Error, string.Format("插入新条码信息失败[{0}]...", _StrErr));
                        }
                    }
                }
                catch(Exception ex)
                {
                    mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, ex.Message);
                }
            }
            else
            {
                SendMsg(mLogMsgType.Error, "没有可以打印的信息");
            }
        }

        private void CleatTxt()
        {
            txt_date_code.Text = string.Empty;
            txt_kp_desc.Text = string.Empty;
            txt_kp_no.Text = string.Empty;
            txt_lot_code.Text = string.Empty;
            txt_qty.Text = "0";
            txt_vender_id.Text = string.Empty;
        }
        public void PublicPrintLabel(Dictionary<string, string> dic)
        {
            if (!File.Exists(LabelFlePatch.Text))  //判断条码文件是否存在
            {
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "条码档没有找到,路径:" + LabelFlePatch.Text);
                return;
            }

            try
            {
                lbl.Documents.Open(LabelFlePatch.Text, false);// 调用设计好的label文件
                Document doc = lbl.ActiveDocument;
                for (int i = 0; i < doc.Variables.FormVariables.Count; i++)
                {
                    doc.Variables.FormVariables.Item(doc.Variables.FormVariables.Item(i + 1).Name).Value = "";
                }
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, string.Format("模板变量清空完成,共计{0}个...", doc.Variables.FormVariables.Count));
                foreach (KeyValuePair<string, string> _DicKeyValues in dic)
                {
                    try
                    {
                        doc.Variables.FormVariables.Item(_DicKeyValues.Key).Value = _DicKeyValues.Value; //给参数传值                     
                        mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Outgoing, string.Format("填充打印变量完成:{0}->{1}", _DicKeyValues.Key, _DicKeyValues.Value));
                    }
                    catch
                    {
                    }
                }                   
                doc.PrintDocument(1);               //打印
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Normal, "打印完成");
            }
            catch (Exception ex)
            {
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "发生异常" + ex.Message);
            }
            finally
            {

               
            }

        }

        private void txt_qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_qty.Text) && e.KeyCode == Keys.Enter)
            {
                imbt_Print_Click(null,null);
            }
        }

        private void Frm_ReturnofMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                lbl.Quit(); //退出
            }
            catch
            {
            }
        }
    }
}
