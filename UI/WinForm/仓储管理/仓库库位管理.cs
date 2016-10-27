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
    public partial class StoreLocManage : Office2007Form// Form
    {
        public StoreLocManage(MainParent mfm)
        {
            InitializeComponent();
            this.mFrm = mfm;

        }
        MainParent mFrm;

        private delegate void delegateloadstore();
        delegateloadstore dls;

        private void loadstore()
        {
            ShowStorehouse(FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseInfo()));
        }

        private void ShowStorehouse(DataTable dt)
        {
            dgv_storeinfo.Invoke(new EventHandler(delegate
                {
                    this.dgv_storeinfo.DataSource = dt;
                }));
        }

        private void StoreLocManage_Load(object sender, EventArgs e)
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
            dls = new delegateloadstore(loadstore);
            dls.BeginInvoke(null, null);

          
            //dgv_storeinfo.DataSource = refWebtStorehouseManage.Instance.GetAlltStorehouseInfo();
        }

        private void bt_select_Click(object sender, EventArgs e)
        {
            dgv_storeinfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GettStorehouseInfoById(this.tb_warehouse.Text.Trim()));
        }

        private void bt_allinfo_Click(object sender, EventArgs e)
        {
            dgv_storeinfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseInfo());
        }

        private void bt_addhouse_Click(object sender, EventArgs e)
        {
            Frmwarehouse wh = new Frmwarehouse(mFrm, this);
            wh.ShowDialog();
            bool st = bool.Parse(wh.IsDisposed.ToString());
            if (!st)
                dgv_storeinfo.DataSource=FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseInfo());
        }

        private void bt_addloc_Click(object sender, EventArgs e)
        {
            FrmWarehouseManage whm = new FrmWarehouseManage(mFrm, this);
            whm.ShowDialog();
        }

        private void dgv_storeinfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                dgv_locinfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GettStorehouseLoctionInfo("storehouseId", dgv_storeinfo["storehouseId", e.RowIndex].Value.ToString()));
            }
        }

        private void dgv_locinfo_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1&&e.ColumnIndex!=-1)
            {
                dgv_locinfo[e.ColumnIndex, e.RowIndex].ToolTipText = string.Format("当前累计有{0}行数据!" ,dgv_locinfo.RowCount-1);
            }
        }

      
    }
}
