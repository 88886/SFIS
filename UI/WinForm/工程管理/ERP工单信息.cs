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

namespace SFIS_V2
{
    public partial class Frm_ErpWoInfo : Office2007Form //Form
    {
        public Frm_ErpWoInfo()
        {
            InitializeComponent();
        }

        private delegate void Initialization();
        Initialization InitErpWoInfo;

        public string Erp_woId = string.Empty;
        public string Erp_QTY = string.Empty;
        public string Erp_PartNumber = string.Empty;
        public string Erp_POID = string.Empty;
        public string Erp_BOMVER = string.Empty;
        public string Erp_WOTYPE = string.Empty;

        private void Frm_ErpWoInfo_Load(object sender, EventArgs e)
        {
            // 禁止用户改变DataGridView1的所有列的列宽  
            dgv_erp_wo.AllowUserToResizeColumns = false;  
            //禁止用户改变DataGridView1の所有行的行高  
            dgv_erp_wo.AllowUserToResizeRows = false; 

            InitErpWoInfo = new Initialization(IniErp_woInfo);
            InitErpWoInfo.BeginInvoke(null, null);
        }

        private void IniErp_woInfo()
        {
            this.Invoke(new EventHandler(delegate
            {
                dgv_erp_wo.DataSource=FrmBLL.ReleaseData.arrByteToDataTable(refWebt_wo_Info_Erp.Instance.Get_Erp_woinfo());
            }));
        }

        private void tb_woid_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_woid.Text) && e.KeyCode == Keys.Enter)
            {
                FrmBLL.publicfuntion.SelectDataGridViewRows(tb_woid.Text.Trim(), dgv_erp_wo, 0);
            }
        }

        private void dgv_erp_wo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Erp_woId = dgv_erp_wo[0, e.RowIndex].Value.ToString();
                Erp_POID = dgv_erp_wo[1, e.RowIndex].Value.ToString();
                Erp_QTY = dgv_erp_wo[2, e.RowIndex].Value.ToString();
                Erp_PartNumber = dgv_erp_wo[3, e.RowIndex].Value.ToString();
                Erp_BOMVER = dgv_erp_wo[4, e.RowIndex].Value.ToString();
                Erp_WOTYPE = dgv_erp_wo[5, e.RowIndex].Value.ToString();
            }
        }

        private void imbt_NewWoInfo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否要新建工单:"+Erp_woId,"新建工单提示",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {

                Dictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOID", Erp_woId);
                mst.Add("POID", Erp_POID);
                mst.Add("QTY", Erp_QTY);
                mst.Add("WOSTATE", 0);
                mst.Add("USERID", "NA");
                mst.Add("PARTNUMBER", Erp_PartNumber);
                mst.Add("BOMVER", Erp_BOMVER);
                mst.Add("SAPWOTYPE", Erp_WOTYPE);
                mst.Add("WOTYPE", FrmBLL.publicfuntion.GetWOtype(Erp_WOTYPE));
                mst.Add("OUTPUTQTY", 0);
                mst.Add("INPUTQTY", 0);
                mst.Add("SCRAPQTY", 0);
                mst.Add("WOSOURCEFLAG", "E");

                string _StrErr = refWebtWoInfo.Instance.InsertWoInfo(FrmBLL.ReleaseData.DictionaryToJson(mst), null, null, null);
                if (_StrErr == "OK")
                {

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("WOID", Erp_woId);
                    dic.Add("WORLSFLAG", 1);
                    _StrErr = refWebt_wo_Info_Erp.Instance.Update_Erp_woInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));
                    if (_StrErr=="OK")
                    DialogResult = DialogResult.OK;
                    else
                        MessageBox.Show("更新ERP工单状态失败:" + _StrErr,"更新ERP工单信息提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("新建工单发生异常:" + _StrErr);
                }
            }
        }
    }
}
