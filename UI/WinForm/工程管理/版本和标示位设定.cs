using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using WebServices.Webt_woInfo;
using System.Linq;

namespace SFIS_V2
{
    public partial class Frm_Version_info : Office2007Form
    {
        public Frm_Version_info(MainParent _mfrm)
        {
            InitializeComponent();
            mFrm = _mfrm;
        }
        MainParent mFrm = null;
        private void Frm_Version_info_Load(object sender, EventArgs e)
        {
            try
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
                #endregion
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
            txt_version_value.Enabled = false;
            txt_mark_type.Enabled = false;
            txt_mark_value.Enabled = false;
            txt_wo.Focus();
        }

        private void txt_wo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Rtb_version_msg.Text = "";
                dgv_version_info.Rows.Clear();
                if (string.IsNullOrEmpty(this.txt_wo.Text))
                {
                    Rtb_version_msg.Text = "请先输入工单号码";
                    return;
                }
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtVersion_Mark.Instance.QueryVersionInfoByWo(this.txt_wo.Text.Trim()));
                 if (dt.Rows.Count > 0)
                 {
                     for (int i = 0; i < dt.Rows.Count; i++)
                     {
                         dgv_version_info.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString());
                     }
                 }
                 else
                 {
                     dgv_version_info.Rows.Clear();
                     DataTable dt_wo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(this.txt_wo.Text.Trim(),null));
                     if (dt_wo.Rows.Count <1)
                     { 
                         Rtb_version_msg.Text = "工单号码不存在";
                        return;
                     }
                 }
                 this.txt_wo.Enabled = false;
                 this.txt_version_value.Enabled = true;
                 this.txt_version_value.Focus();
            }
        }

        private void bt_version_cancel_Click(object sender, EventArgs e)
        {
            this.txt_version_value.Text = "";
        }

        private void bt_version_add_Click(object sender, EventArgs e)
        {
            add_version();
        }
       
        private void txt_version_value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                add_version();
            }
        }
        private void add_version()
        {
            Rtb_version_msg.Text = "";
            if (txt_wo.Enabled == true)
            {
                Rtb_version_msg.Text = "请输入工单号码并确定";
                return;
            }
            if (string.IsNullOrEmpty(cb_version_type.Text.ToString()))
            {
                Rtb_version_msg.Text = "请选择版本类型";
                return;
            }
            if (string.IsNullOrEmpty(txt_version_value.Text.ToString()))
            {
                Rtb_version_msg.Text = "请输入版本值";
                return;
            }
            if (dgv_version_info.Rows.Count > 0)
            {
                for (int i = 0; i < dgv_version_info.Rows.Count; i++)
                {
                    if (cb_version_type.Text.ToString() == dgv_version_info.Rows[i].Cells[1].Value.ToString())
                    {
                        Rtb_version_msg.Text = cb_version_type.Text.ToString()+"类型已经存在！！";
                        return;
                    }
                }
            }
            dgv_version_info.Rows.Add(this.txt_wo.Text, cb_version_type.Text.ToString(), txt_version_value.Text.Trim(), this.mFrm.gUserInfo.username, System.DateTime.Now.ToString());
            txt_version_value.Text = "";
            txt_version_value.Focus();
        }

        private void bt_all_cancel_Click(object sender, EventArgs e)
        {
            txt_version_value.Text = "";
            txt_version_value.Enabled = false;
            txt_wo.Enabled = true;
            txt_wo.Text = "";
            txt_wo.Focus();
            dgv_version_info.Rows.Clear();
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            Rtb_version_msg.Text = "";
            if (this.dgv_version_info.SelectedRows.Count > 0)
            {
                dgv_version_info.Rows.Remove(dgv_version_info.CurrentRow);
            }
            else
            {
                Rtb_version_msg.Text = "请选中需要删除的行！";
                return;
            }
        }

        private void bt_confirm_Click(object sender, EventArgs e)
        {
            //List<WebServices.tVersion_Mark.tVersionMark> lsVersion = new List<WebServices.tVersion_Mark.tVersionMark>();

            List<IDictionary<string, object>> _lsVersion = new List<IDictionary<string, object>>();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (dgv_version_info.Rows.Count > 0)
            {
                for(int i=0;i<dgv_version_info.Rows.Count;i++)
                {
                    dic.Add("WOID", txt_wo.Text);
                    dic.Add("VERSION_NAME", dgv_version_info.Rows[i].Cells[1].Value.ToString());
                    dic.Add("VERSION_VALUES", dgv_version_info.Rows[i].Cells[2].Value.ToString());
                    _lsVersion.Add(dic);
                    //lsVersion.Add(new WebServices.tVersion_Mark.tVersionMark()
                    //{
                    //    WOID = txt_wo.Text,
                    //    VERSION_NAME = dgv_version_info.Rows[i].Cells[1].Value.ToString(),
                    //    VERSION_VALUES = dgv_version_info.Rows[i].Cells[2].Value.ToString()
                    //});
                }
                string confirm_err = refWebtVersion_Mark.Instance.InsertVersionInfo(txt_wo.Text, FrmBLL.ReleaseData.ListDictionaryToJson(_lsVersion), this.mFrm.gUserInfo.userId);
                if (confirm_err == "OK")
                {
                    Rtb_version_msg.Text = "工单版本建立成功！！";
                }
                else
                {
                    Rtb_version_msg.Text = "工单版本建立失败！！" + confirm_err;
                    return;
                }
                dgv_version_info.Rows.Clear();
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtVersion_Mark.Instance.QueryVersionInfoByWo(this.txt_wo.Text.Trim()));
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgv_version_info.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString());
                    }
                }
                txt_wo.Enabled = true;
                txt_wo.SelectAll();
                txt_wo.Focus();
            }
        }

        private void txt_pn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Rt_mark_mes.Text = "";
                dgv_mark_info.Rows.Clear();
                if (string.IsNullOrEmpty(this.txt_pn.Text))
                {
                    Rtb_version_msg.Text = "请先输入产品料号";
                    return;
                }
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtVersion_Mark.Instance.QueryMarkBitByPn(this.txt_pn.Text.Trim()));
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgv_mark_info.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString());
                    }
                }
                else
                {
                    dgv_mark_info.Rows.Clear();
                    DataTable dt_pn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetProductByPartNumber(this.txt_pn.Text.Trim()));
                    if (dt_pn.Rows.Count < 1)
                    {
                        Rtb_version_msg.Text = "产品料号不存在";
                        return;
                    }
                }
                this.txt_pn.Enabled = false;
                this.txt_mark_type.Enabled = true;
                txt_mark_value.Enabled = true;
                txt_mark_type.Text = "";
                this.txt_mark_type.Focus();
            }
        }

        private void txt_mark_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_mark_value.SelectAll();
                txt_mark_value.Focus();
            }
        }

        private void bt_mark_cancel_Click(object sender, EventArgs e)
        {
            txt_mark_type.Text = "";
            txt_mark_value.Text = "";
        }

        private void txt_mark_value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                add_mark_bit();
            }
        }

        private void bt_mark_add_Click(object sender, EventArgs e)
        {
            add_mark_bit();
        }
        private void add_mark_bit()
        {
            Rt_mark_mes.Text = "";
            if (txt_pn.Enabled == true)
            {
                Rt_mark_mes.Text = "请输入产品料号并确定";
                return;
            }
            if (string.IsNullOrEmpty(txt_mark_type.Text.ToString()))
            {
                Rt_mark_mes.Text = "请输入标示位类型";
                return;
            }
            if (string.IsNullOrEmpty(txt_mark_value.Text.ToString()))
            {
                Rt_mark_mes.Text = "请输入标示位值";
                return;
            }
            dgv_mark_info.Rows.Add(this.txt_pn.Text, txt_mark_type.Text.ToString(), txt_mark_value.Text.Trim(), this.mFrm.gUserInfo.username, System.DateTime.Now.ToString());
            txt_mark_type.Text = "";
            txt_mark_value.Text = "";
            txt_mark_type.Focus();
        }

        private void bt_mark_all_cancel_Click(object sender, EventArgs e)
        {
            txt_mark_type.Text = "";
            txt_mark_type.Enabled = false;
            txt_mark_value.Text = "";
            txt_mark_value.Enabled = false;
            txt_pn.Enabled = true;
            txt_pn.Text = "";
            txt_pn.Focus();
            dgv_mark_info.Rows.Clear();
        }

        private void bt_mark_delete_Click(object sender, EventArgs e)
        {
            Rt_mark_mes.Text = "";
            if (this.dgv_mark_info.SelectedRows.Count > 0)
            {
                dgv_mark_info.Rows.Remove(dgv_mark_info.CurrentRow);
            }
            else
            {
                Rt_mark_mes.Text = "请选中需要删除的行！";
                return;
            }
        }

        private void bt_mark_confirm_Click(object sender, EventArgs e)
        {
            //List<WebServices.tVersion_Mark.tVersionMark> lsMark = new List<WebServices.tVersion_Mark.tVersionMark>();
            IList<IDictionary<string, object>> _lsMark = new List<IDictionary<string, object>>();
            IDictionary<string, object> dic = new Dictionary<string, object>();
            if (dgv_mark_info.Rows.Count > 0)
            {
                for (int i = 0; i < dgv_mark_info.Rows.Count; i++)
                {
                    dic.Add("PARTNUMBER", txt_pn.Text);
                    dic.Add("MARK_BIT_NAME", dgv_mark_info.Rows[i].Cells[1].Value.ToString());
                    dic.Add("MARK_BIT_VALUES", dgv_mark_info.Rows[i].Cells[2].Value.ToString());
                    _lsMark.Add(dic);
                    //lsMark.Add(new WebServices.tVersion_Mark.tVersionMark()
                    //{
                    //    PARTNUMBER = txt_pn.Text,
                    //    MARK_BIT_NAME = dgv_mark_info.Rows[i].Cells[1].Value.ToString(),
                    //    MARK_BIT_VALUES = dgv_mark_info.Rows[i].Cells[2].Value.ToString()
                    //});
                }
                string confirm_err = refWebtVersion_Mark.Instance.InsertMarkBitInfo(txt_pn.Text, FrmBLL.ReleaseData.ListDictionaryToJson(_lsMark), this.mFrm.gUserInfo.userId);
                if (confirm_err == "OK")
                {
                    Rt_mark_mes.Text = "产品标示位建立成功！！";
                }
                else
                {
                    Rt_mark_mes.Text = "产品标示位建立失败！！" + confirm_err;
                    return;
                }
                dgv_mark_info.Rows.Clear();
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtVersion_Mark.Instance.QueryMarkBitByPn(this.txt_pn.Text.Trim()));
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgv_mark_info.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString());
                    }
                }
                txt_pn.Enabled = true;
                txt_pn.SelectAll();
                txt_pn.Focus();
            }
        }
    }
}