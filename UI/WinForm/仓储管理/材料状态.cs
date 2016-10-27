using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;


namespace SFIS_V2
{
    public partial class MaterialsStorageInfo : Office2007Form// Form
    {
        public MaterialsStorageInfo(string kpnumber)
        {
            InitializeComponent();
            this.mkpnumber = kpnumber;
        }
        private string mkpnumber;

        private delegate void DelegateShowInfo();
        DelegateShowInfo si;
        private void ShowInfo()
        {
            this.dataGridView1.Invoke(new EventHandler(delegate
                {
                    dataGridView1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.GetMaterialInfoByKpnumber(mkpnumber));
                }));
        }
        private void MaterialsStorageInfo_Load(object sender, EventArgs e)
        {
            si = new DelegateShowInfo(ShowInfo);
            si.BeginInvoke(null, null);
        }

    }
}
