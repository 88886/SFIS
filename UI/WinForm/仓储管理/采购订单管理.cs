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
    public partial class FrmPOManage : Office2007Form//Form
    {
        public FrmPOManage(MainParent mfr)
        {
            InitializeComponent();
            mFrm = mfr;
        }

        MainParent mFrm;
        private void bti_select_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds=FrmBLL.ReleaseData.arrByteToDataSet(
                    refWebSapConnector.Instance.Get_Z_RFC_AFPO(this.tbi_po.Text.Trim()));
                this.dgv_header.DataSource=ds.Tables[0];
                this.dgv_haddta.DataSource = ds.Tables[1];

            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Normal);
            }
        }
    }
}
