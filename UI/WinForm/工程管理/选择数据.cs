using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using FrmBLL;

namespace SFIS_V2
{
    public partial class SelectData : Office2007Form// Form
    {
        public SelectData(Office2007Form _frm, DataTable _dt)
        {
            InitializeComponent();
            //sqlcmd = sql;
            mDatatable = mTempDt = _dt;
            mFrm = _frm;
        }
        private Office2007Form mFrm = null;
        //private string sqlcmd = "";

        private DataTable mDatatable = null;
        private DataTable mTempDt = null;
        private void SelectData_Load(object sender, EventArgs e)
        {
            this.dgv_showdata.DataSource = mDatatable;//BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sqlcmd);
        }

        private void cmb_selecttype_DropDown(object sender, EventArgs e)
        {
            this.cmb_selecttype.Items.Clear();
            for (int i = 0; i < this.mDatatable.Columns.Count; i++)
            {
                this.cmb_selecttype.Items.Add(this.mDatatable.Columns[i].ColumnName.ToString());
            }
        }

        private void tb_selectvalue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (string.IsNullOrEmpty(this.tb_selectvalue.Text))
                    return;
                this.bt_query_Click(null, null);
            }
        }

        private void bt_query_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_selectvalue.Text))
            {
                this.dgv_showdata.DataSource = this.mDatatable;
            }
            else
            {
                this.dgv_showdata.DataSource = publicfuntion.getNewTable(this.mDatatable, string.Format("{0}='{1}'",
                    this.cmb_selecttype.Text.Trim(), this.tb_selectvalue.Text.Trim()));
            }

        }

        private void dgv_showdata_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //判定是哪个FROM需要
                if (mFrm is fFacDeptInfo)
                {
                    (mFrm as fFacDeptInfo).edtuserId.Text = this.dgv_showdata[0, e.RowIndex].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                if (mFrm is formworkshop)
                {
                    (mFrm as formworkshop).textuserid.Text = this.dgv_showdata[0, e.RowIndex].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                if (mFrm is line_set)
                {
                    (mFrm as line_set).txt_userid.Text = this.dgv_showdata[0, e.RowIndex].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                if (mFrm is Frmwarehouse)
                {
                    (mFrm as Frmwarehouse).tb_storeman.Text = this.dgv_showdata[0, e.RowIndex].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                //if (mFrm is Print5in1label)
                //{

                //    (mFrm as Print5in1label).cblocal.Text = "";
                //    (mFrm as Print5in1label).cblocal.SelectedText = this.dgv_showdata["locId", e.RowIndex].Value.ToString();
                //    (mFrm as Print5in1label).cb_store.Text = "";
                //    (mFrm as Print5in1label).cb_store.SelectedText = this.dgv_showdata["storehouseId", e.RowIndex].Value.ToString();
                //    this.DialogResult = DialogResult.OK;
                //    return;
                //}

                //if (mFrm is FrmPallet)
                //{
                //    if ((mFrm as FrmPallet).Line == true)
                //    {
                //        (mFrm as FrmPallet).LineName = this.dgv_showdata[0, e.RowIndex].Value.ToString();
                //        (mFrm as FrmPallet).LineCode = "S1";
                //    }
                //    else
                //        if ((mFrm as FrmPallet).Craft == true)
                //        {
                //           // (mFrm as FrmPallet).LabConfig.Text = "线体: " + (mFrm as FrmPallet).LineName + "  --  途程: " + this.dgv_showdata[1, e.RowIndex].Value.ToString();
                          
                //            (mFrm as FrmPallet).CraftName = this.dgv_showdata[1, e.RowIndex].Value.ToString();
                //        }

                //    this.DialogResult = DialogResult.OK;
                //    return;
                //}

                //if (mFrm is FrmStockIn)
                //{
                //    if ((mFrm as FrmStockIn).Line == true)
                //    {
                //        (mFrm as FrmStockIn).LineName = this.dgv_showdata[0, e.RowIndex].Value.ToString();
                //    }
                //    else
                //    {
                //        (mFrm as FrmStockIn).LabConfig.Text = "线体: " + (mFrm as FrmStockIn).LineName + "  --  途程: " + this.dgv_showdata[1, e.RowIndex].Value.ToString();
                //        (mFrm as FrmStockIn).CraftId = this.dgv_showdata[0, e.RowIndex].Value.ToString();
                //        (mFrm as FrmStockIn).CraftName = this.dgv_showdata[1, e.RowIndex].Value.ToString();
                //    }

                //    this.DialogResult = DialogResult.OK;
                //    return;
                //}

                //if (mFrm is Frm_StockReceive)
                //{
                //    if (dgv_showdata.Columns.Count == 2 && e.RowIndex != -1 && e.ColumnIndex != -1)
                //    {
                //        dgv_showdata.DataSource = mDatatable = FrmBLL.ReleaseData.arrByteToDataTable(
                //            RefWebService_BLL.refWebtStorehouseManage.Instance.GetLotIdByStorehouseId(
                //            dgv_showdata["仓库编号", e.RowIndex].Value.ToString()));
                //        ShowButton();

                //    }
                //    else
                //    {
                //        (mFrm as Frm_StockReceive).txtlocid.Text = "";
                //        (mFrm as Frm_StockReceive).txtlocid.Text = this.dgv_showdata["库位编号", e.RowIndex].Value.ToString();
                //        (mFrm as Frm_StockReceive).txtstore.Text = "";
                //        (mFrm as Frm_StockReceive).txtstore.Text = this.dgv_showdata["仓库编号", e.RowIndex].Value.ToString();
                //        this.DialogResult = DialogResult.OK;
                //        return;
                //    }
                //}

                //if (mFrm is Frm_Receiving_Storage)
                //{
                //    //if (dgv_showdata.Columns.Count == 2 && e.RowIndex != -1 && e.ColumnIndex != -1)
                //    //{
                //    //    dgv_showdata.DataSource = mDatatable = FrmBLL.ReleaseData.arrByteToDataTable(
                //    //        RefWebService_BLL.refWebtStorehouseManage.Instance.GetLotIdByStorehouseId(
                //    //        dgv_showdata["仓库编号", e.RowIndex].Value.ToString()));
                //    //    ShowButton();

                //    //}
                //    //else
                //    //{
                //        (mFrm as Frm_Receiving_Storage).txtlocid.Text = "NA";
                //      //  (mFrm as Frm_Receiving_Storage).txtlocid.Text = this.dgv_showdata["库位编号", e.RowIndex].Value.ToString();
                //        (mFrm as Frm_Receiving_Storage).txtstore.Text = "";
                //        (mFrm as Frm_Receiving_Storage).txtstore.Text = this.dgv_showdata["仓库编号", e.RowIndex].Value.ToString();
                //        this.DialogResult = DialogResult.OK;
                //        return;
                //  //  }
                //}

                if (mFrm is ManageRoute)
                {
                    (mFrm as ManageRoute).tb_partnumber.Text = this.dgv_showdata["partnumber", e.RowIndex].Value.ToString();
                    (mFrm as ManageRoute).tb_productname.Text = this.dgv_showdata["productname", e.RowIndex].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                    return;
                }
              
                //if (mFrm is Frm_ProductOut)
                //{
                //    Frm_ProductOut po = mFrm as Frm_ProductOut;
                //    if (po.mFlag == "0")
                //    {
                //        (mFrm as Frm_ProductOut).txtCustomerId.Text = "";
                //        (mFrm as Frm_ProductOut).txtCustomerId.Text = this.dgv_showdata["storehouseId", e.RowIndex].Value.ToString();
                //        this.DialogResult = DialogResult.OK;
                //        return;
                //    }
                //    if (po.mFlag == "1")
                //    {
                //        (mFrm as Frm_ProductOut).tb_customerid.Text = "";
                //        (mFrm as Frm_ProductOut).tb_customerid.Text = this.dgv_showdata["storehouseId", e.RowIndex].Value.ToString();
                //        this.DialogResult = DialogResult.OK;
                //        return;
                //    }

                //}              
                if (mFrm is FrmRepair)
                {
                    (mFrm as FrmRepair).cbreasoncode.Text = this.dgv_showdata[0, e.RowIndex].Value.ToString();
                    (mFrm as FrmRepair).tbrcdesc.Text = this.dgv_showdata[2, e.RowIndex].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                if (mFrm is Frm_FQC)
                {
                    Frm_FQC po = mFrm as Frm_FQC;
                    if (po.mFlag == "0")
                    {
                        (mFrm as Frm_FQC).cbreasoncode.Text = this.dgv_showdata[0, e.RowIndex].Value.ToString();
                        (mFrm as Frm_FQC).tbrcdesc.Text = this.dgv_showdata[2, e.RowIndex].Value.ToString();
                        this.DialogResult = DialogResult.OK;
                        return;
                    }
                    if (po.mFlag == "1")
                    {
                        (mFrm as Frm_FQC).Lab_Craft.Text = this.dgv_showdata[1, e.RowIndex].Value.ToString();
                        po.CraftId = this.dgv_showdata[1, e.RowIndex].Value.ToString();
                        this.DialogResult = DialogResult.OK;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private Button mButton;
        private void ShowButton()
        {
            mButton = new Button();
            mButton.Text = "显示仓库";
            mButton.Location = new Point(447, 8);
            mButton.Size = new Size(75, 23);
            mButton.Click += new EventHandler(mButton_Click);
            this.panel1.Controls.Add(mButton);
        }

        private void mButton_Click(object sender, EventArgs e)
        {
            try
            {
                cmb_selecttype.SelectedIndex = -1;
                tb_selectvalue.Text = "";
                mDatatable = null;
                dgv_showdata.DataSource = null;
                dgv_showdata.DataSource = mDatatable = mTempDt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
