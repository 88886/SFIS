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
using LabelManager2;

namespace SFIS_V2
{
    public partial class Frm_Modify_TrSn_Qty : Office2007Form //Form
    {
        public Frm_Modify_TrSn_Qty(Frm_MaterialManage Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Frm_MaterialManage mFrm;
        string FilePatch = string.Empty;
        public string Modify_Qty = string.Empty;
        private void Frm_Modify_TrSn_Qty_Load(object sender, EventArgs e)
        {
            FilePatch = Directory.GetCurrentDirectory() + "\\LabelFile\\FEIXUN_LOT.lab";
            mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Incoming, "条码路径: "+FilePatch);
            txt_tr_sn.Focus();
        }

        private void txt_tr_sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_tr_sn.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    string _StrErr = string.Empty;

                    DataTable _dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_Tr_Sn_Info(txt_tr_sn.Text, out _StrErr));
                    if (_dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(_dt.Rows[0]["STATUS"].ToString()) == 4)
                        {
                            #region  填充控件值
                            FrmBLL.publicfuntion.Fill_Control(panelEx3, _dt);
                            #endregion
                            Frm_InputQty fi = new Frm_InputQty(this);
                            if (fi.ShowDialog() == DialogResult.OK)
                            {
                                Dictionary<string, string> dic = new Dictionary<string, string>();
                                dic.Add("TR_SN", _dt.Rows[0]["TR_SN"].ToString());
                                dic.Add("PART_NO", _dt.Rows[0]["KP_NO"].ToString());
                                dic.Add("DATE_CODE", _dt.Rows[0]["DATE_CODE"].ToString());
                                dic.Add("UNIT_SIZE", Modify_Qty);
                                dic.Add("VENDER_CODE", _dt.Rows[0]["VENDER_ID"].ToString());
                                dic.Add("LOT_ID", _dt.Rows[0]["LOT_CODE"].ToString());
                                dic.Add("EMP_NO", mFrm.UserId);
                                dic.Add("REMARK", _dt.Rows[0]["KP_DESC"].ToString());
                                dic.Add("STORLOC", string.Format("{0}/{1}", _dt.Rows[0]["STOCK_ID"].ToString(), _dt.Rows[0]["LOC_ID"].ToString()));
                                Dictionary<string, object> mst = new Dictionary<string, object>();
                                mst.Add("TR_SN", _dt.Rows[0]["TR_SN"].ToString());
                                mst.Add("QTY", Convert.ToInt32(Modify_Qty));
                                refWebtR_Tr_Sn.Instance.UPDATE_TR_SN(FrmBLL.ReleaseData.DictionaryToJson(mst));
                                mst.Add("WOID", _dt.Rows[0]["WOID"].ToString());
                                mst.Add("STATUS","4");   
                                refWebtWOMaterial.Instance.UPDATE_T_WO_Material(FrmBLL.ReleaseData.DictionaryToJson(mst));
                                PublicPrintLabel(dic);
                            }
                        }
                        else
                        {
                            mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, string.Format("物料状态错误[{0}],不可修改数量", FrmBLL.publicfuntion.MaterialStatus(_dt.Rows[0]["STATUS"].ToString())));
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    txt_tr_sn.Text = string.Empty;
                }
            }
        }
        public void PublicPrintLabel(Dictionary<string, string> dic)
        {
            if (!File.Exists(FilePatch))  //判断条码文件是否存在
            {
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "条码档没有找到,路径:" + FilePatch);
                return;
            }
            ApplicationClass lbl = new ApplicationClass();
            try
            {
                lbl.Documents.Open(FilePatch, false);// 调用设计好的label文件
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
                int Num = Convert.ToInt32(numPrintQty.Value);        //打印数量          
                doc.PrintDocument(Num);               //打印
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Normal, "打印完成");
            }
            catch (Exception ex)
            {
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "发生异常" + ex.Message);
            }
            finally
            {

                lbl.Quit(); //退出
            }

        }
    }
}
