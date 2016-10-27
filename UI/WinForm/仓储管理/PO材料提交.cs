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
    public partial class Frm_POCheck : Office2007Form//Form
    {
        public Frm_POCheck(MainParent mfm)
        {
            InitializeComponent();
            mFrm = mfm;
        }

        MainParent mFrm;
        private void bti_select_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tbi_po.Text.Trim()))
                {
                    this.dgv_materialinfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(
                        refwebtMaterialsReceive.Instance.GetMaterialsInfo(this.tbi_po.Text.Trim(), cmi_querycondition.SelectedIndex.ToString()));
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Normal);
            }
        }

        private void tbi_po_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tbi_po.Text.Trim()) && e.KeyValue == 13)
            {
                bti_select_Click(null, null);
            }
        }

        private void Frm_POCheck_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
            {
                List<WebServices.tUserInfo.tFunctionList> lsfunls = new List<WebServices.tUserInfo.tFunctionList>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                FrmBLL.publicfuntion.AddProgInfo(new WebServices.tUserInfo.tProgInfo()
                {
                    progid = this.Name,
                    progname = this.Text,
                    progdesc = this.Text

                }, lsfunls);
            }
            #endregion

            this.cmi_querycondition.SelectedIndex = 0;
        }

        //事件处理，想在右键菜单做业务处理---
        private void DGV_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Clicks == 1)
            //{
            //    DataGridViewCheckBoxCell ck = (DataGridViewCheckBoxCell)dgv_materialinfo["checkd", dgv_materialinfo.CurrentCell.RowIndex];
            //    ck.Value = true;
            //}
            //if (e.Clicks==2)
            //{
            //    DataGridViewCheckBoxCell ck = (DataGridViewCheckBoxCell)dgv_materialinfo["checkd", dgv_materialinfo.CurrentCell.RowIndex];
            //    ck.Value = false;
            //}
        }

        private void dgv_materialinfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_materialinfo.CurrentCell == null)
                return; 

            try
            {
                int i = dgv_materialinfo.CurrentCell.RowIndex;

                if (e.RowIndex != -1 && e.ColumnIndex !=-1 &&
                    dgv_materialinfo.Columns[e.ColumnIndex].Name == "location")
                {
                    string trsn = dgv_materialinfo["trsn",e.RowIndex].Value.ToString();
                    mFrm.storageing(trsn);
                    //storagebuffer sf = new storagebuffer(mFrm,trsn,1);
                    //sf.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Outgoing);
            }
        }
    }
}
