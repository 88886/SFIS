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

namespace SFIS_V2
{
    public partial class wo_select : Office2007Form// Form
    {
        public wo_select(MainParent _mfrm)
        {
            InitializeComponent();
            mFrm = _mfrm;
        }
        MainParent mFrm = null;

        private void tb_selectwoname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                {
                    this.bt_selectwoinfo_Click(null, null);
                }
        }

        private void FillDataGridView(DataTable _dt)
        {
            this.dgv_showwoinfo.Invoke(new EventHandler(delegate
            {
                this.dgv_showwoinfo.DataSource = _dt;
                if (_dt.Rows.Count > 0)
                {
                    DataGridViewCellEventArgs dataGridViewCellEventArgs = new DataGridViewCellEventArgs(0, 0);
                    dgv_showwoinfo_CellClick(dgv_showwoinfo, dataGridViewCellEventArgs);
                }
            }));
        }

        private void bt_selectwoinfo_Click(object sender, EventArgs e)
        {
            try
            {
                    FillDataGridView(FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(this.tb_selectwoname.Text.Trim(), this.tb_selectpartname.Text .Trim())));
                  

            }
            catch (Exception ex)
            {
                mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void tb_selectpartname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.bt_selectwoinfo_Click(null, null);
            }
        }

        private void wo_select_Load(object sender, EventArgs e)
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

            tb_selectwoname.Focus();
        }

        private void dgv_showwoinfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {             
                FillDataGridToTextBox(e, PanelShowData);
                GetSnRule(txt_woid.Text);
             
            }
        }
        private void FillDataGridToTextBox(DataGridViewCellEventArgs e, Control Ctrl)
        {
            foreach (Control ctrl in Ctrl.Controls)
            {
                switch (ctrl.GetType().Name)
                {
                    case "TextBox":
                        {
                            ctrl.Text = dgv_showwoinfo[ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper(), e.RowIndex].Value.ToString();
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void txt_wostate_TextChanged(object sender, EventArgs e)
        {
            int a = 0;
            if (int.TryParse(txt_wostate.Text, out a))
            {
                string _status = string.Empty;
                switch (txt_wostate.Text)
                {
                    case "0":
                        _status = "待上线";
                        break;
                    case "1":
                        _status = "已上线";
                        break;
                    case "2":
                        _status = "已上线";
                        break;
                    case "3":
                        _status = "已关闭";
                        break;
                    case "4":
                        _status = "已锁定";
                        break;
                }
                txt_wostate.Text = _status;
            }
        }

        private void dgv_showwoinfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
           // FrmBLL.publicfuntion.UpdateFieldName(dgv_showwoinfo);
        }

        private void GetSnRule(string woId)
        {
            dgv_snrule.Rows.Clear();
            DataTable dtesn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.Get_ESN_Rule(woId));
            foreach (DataRow dr in dtesn.Rows)
            {
                dgv_snrule.Rows.Add("ESN", dr["SNPREFIX"].ToString(), dr["SNPOSTFIX"].ToString(), dr["SNSTART"].ToString(), dr["SNEND"].ToString(), dr["SNLENG"].ToString(), dr["CURRSN"].ToString());
            }
            DataTable dt_custsn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoSnRule(woId, null));
            foreach (DataRow dr in dt_custsn.Rows)
            {
                dgv_snrule.Rows.Add(dr["SNTYPE"].ToString(), dr["SNPREFIX"].ToString(), dr["SNPOSTFIX"].ToString(), dr["SNSTART"].ToString(), dr["SNEND"].ToString(), dr["SNLENG"].ToString(), dr["USENUM"].ToString());
            }
        }
    }
}
