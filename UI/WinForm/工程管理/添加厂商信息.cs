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
    public partial class AddManufacturer : Office2007Form// Form
    {
        public AddManufacturer(MainParent mfrm, SMTgangwangManage mgw)
        {
            InitializeComponent();
            this.mFrm = mfrm;
            this.mSMT = mgw;
        }
        MainParent mFrm;
        SMTgangwangManage mSMT;
        private void ShowDataGridView(DataTable dt)
        {
            this.dataGridViewX1.Invoke(new EventHandler(delegate
            {
                dataGridViewX1.DataSource = dt;
            }));
        }
        private void AddManufacturer_Load(object sender, EventArgs e)
        {
            //dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebVenderInfo.Instance.QueryVender());
            ShowDataGridView(FrmBLL.ReleaseData.arrByteToDataTable(refWebVenderInfo.Instance.QueryVender(null)));
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_vendercode.Text.Trim()))
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebVenderInfo.Instance.QueryVender(null));
                foreach (DataRow dr in dt.Rows)
                {
                    if (this.tb_vendercode.Text.Trim() == dr["venderId"].ToString())
                    {
                        MessageBox.Show("该厂商编号已存在!");
                        this.tb_vendercode.SelectAll();
                        this.tb_vendercode.Focus();
                        return;
                    }
                }

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("VENDERID",tb_vendercode.Text.Trim());
                dic.Add("VENDERNAME", tb_englishname.Text.Trim());
                dic.Add("VENDERNAME2", tb_chinaname.Text.Trim());
                refWebVenderInfo.Instance.InsertVenderInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));
                //refWebVenderInfo.Instance.InsertVenderInfo(new WebServices.tVenderInfo.tWenderInfo()
                //{
                //    VenderId = this.tb_vendercode.Text.Trim(),
                //    VenderName = this.tb_englishname.Text,
                //    VenderName2 = this.tb_chinaname.Text
                //});
                mFrm.ShowPrgMsg("添加信息成功!", MainParent.MsgType.Outgoing);
                ClearInfo();
            }
        }
        private void ClearInfo()
        {
            this.tb_chinaname.Text = "";
            this.tb_englishname.Text = "";
            this.tb_vendercode.Text = "";
        }

        private void bt_query_Click(object sender, EventArgs e)
        {
            ShowDataGridView(FrmBLL.ReleaseData.arrByteToDataTable(refWebVenderInfo.Instance.QueryVender(this.tb_vendercode.Text.Trim())));
        }

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                mSMT.tb_vendercode.Text = dataGridViewX1["venderId", e.RowIndex].Value.ToString();
            }
        }

        private void tb_vendercode_Leave(object sender, EventArgs e)
        {
            if (this.tb_vendercode.Text.Length > 10)
            {
                this.mFrm.ShowPrgMsg("厂商编号不能超过十位...", MainParent.MsgType.Warning);
                this.tb_vendercode.SelectAll();
                this.tb_vendercode.Focus();
            }
        }
    }
}
