using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using LabelManager2;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class Frm_Material_RePrint : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Material_RePrint(Frm_MaterialManage Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Frm_MaterialManage mFrm;
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
                        #region  填充控件值
                        FrmBLL.publicfuntion.Fill_Control(panelEx3,_dt);
                        #endregion

                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("TR_SN", _dt.Rows[0]["TR_SN"].ToString());
                        dic.Add("PART_NO", _dt.Rows[0]["KP_NO"].ToString());
                        dic.Add("DATE_CODE", _dt.Rows[0]["DATE_CODE"].ToString());
                        dic.Add("UNIT_SIZE", _dt.Rows[0]["QTY"].ToString());
                        dic.Add("VENDER_CODE", _dt.Rows[0]["VENDER_ID"].ToString());
                        dic.Add("LOT_ID", _dt.Rows[0]["LOT_CODE"].ToString());
                        dic.Add("EMP_NO", mFrm.UserId);
                        dic.Add("REMARK", _dt.Rows[0]["KP_DESC"].ToString());
                        dic.Add("STORLOC", string.Format("{0}/{1}", _dt.Rows[0]["STOCK_ID"].ToString(), _dt.Rows[0]["LOC_ID"].ToString()));
                        PublicPrintLabel(dic);
                    }
                    else
                    {
                        mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "TR_SN 错误");
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

        private void Frm_Material_RePrint_Load(object sender, EventArgs e)
        {
            LabelFlePatch.Text = Directory.GetCurrentDirectory() + "\\LabelFile\\FEIXUN_LOT.lab";
            txt_tr_sn.Focus();
        }

        public void PublicPrintLabel(Dictionary<string, string> dic)
        {
            if (!File.Exists(LabelFlePatch.Text))  //判断条码文件是否存在
            {
                mFrm.ShowMsg(Frm_MaterialManage.mLogMsgType.Error, "条码档没有找到,路径:" + LabelFlePatch.Text);
                return;
            }
            ApplicationClass lbl = new ApplicationClass();
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