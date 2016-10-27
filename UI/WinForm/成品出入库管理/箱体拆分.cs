using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using FrmBLL;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class DataPartition : Office2007Form// Form
    {
        public DataPartition(Office2007Form _frm, string cartonnumber, int flag)
        {
            InitializeComponent();
            querycode = cartonnumber;
            bflag = flag;
            mFrm = _frm;
        }
        private Office2007Form mFrm = null;
        private string partnumber;
        private DataTable mDatatable = null;
        private string querycode = "";
        private int bflag = 0;
        #region formload
        private void DataPartition_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (mFrm is MainParent)
            {
                if ((mFrm as MainParent).gUserInfo.rolecaption == "系统开发员")
                {
                    IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                    FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("PROGID", this.Name);
                    dic.Add("PROGNAME", this.Text);
                    dic.Add("PROGDESC", this.Text);
                    FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
                }
            }
            #endregion

            cmbdptype.Items.Clear();
            cmbdptype.Items.Add("栈板拆分");
            cmbdptype.Items.Add("箱体拆分");
            cmbdptype.SelectedIndex = 1;
            if (bflag == 2)
                mDatatable = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetPalletList(querycode));
            if (bflag == 1)
            {
               DataTable dt  = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetCartonList(querycode));
                DataView dv = dt.DefaultView;
                dv.Sort = "ESN ASC";
                DataTable dTemp = dv.ToTable();
                mDatatable = FrmBLL.DataTableCrosstab.DataTableCross(dTemp,dTemp.Columns.Count);
            }

            if (bflag == 1)
            {

                Random rad = new Random();//实例化随机数产生器rad；
                int value = rad.Next(1000, 10000);
                lblNewcarton.Text = mDatatable.Rows[0]["woId"].ToString() + "U" + value.ToString();
                partnumber = mDatatable.Rows[0]["partnumber"].ToString();
                cmbdptype.Enabled = false;
                txtCode.Enabled = false;
                this.TabDepartation.TabPages.Remove(this.箱栈板合并);
            }
            if (bflag == 2)
            {
                Random rad = new Random();//实例化随机数产生器rad；
                int value = rad.Next(1000, 10000);

                lblNewcarton.Text = "PU" + value.ToString();
                cmbdptype.SelectedIndex = 0;
                cmbdptype.Enabled = false;
                txtCode.Enabled = false;

            }
            txtCode.Text = querycode;
            this.dgv_showdata.DataSource = mDatatable;

            //---------------------------------------------------
            cmbcombine.Items.Clear();
            cmbcombine.Items.Add("栈板合并");
            cmbcombine.Items.Add("箱体合并");
            cmbcombine.SelectedIndex = 1;

        }
        #endregion


        private void bt_query_Click(object sender, EventArgs e)
        {




            //if (string.IsNullOrEmpty(this.txtCode.Text))
            //{
            this.mDatatable = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetCartonList(this.txtCode.Text.Trim()));
            DataView dv = mDatatable.DefaultView;
            dv.Sort = "ESN ASC";
            DataTable dTemp = dv.ToTable();
            this.dgv_showdata.DataSource = FrmBLL.DataTableCrosstab.DataTableCross(dTemp, dTemp.Columns.Count);
            //}
            //else
            //{
            //    this.dgv_showdata.DataSource = publicfuntion.getNewTable(this.mDatatable, string.Format("{0}='{1}'",
            //        this.cmbdptype.Text.Trim(), this.txtCode.Text.Trim()));
            //}

        }

        //选择被拆分部分
        private void dgv_showdata_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                btSelect_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            string esn;
            if (dgv_showdata.RowCount > 1 && dgv_showdata.CurrentCell != null)
            {
                if (cmbdptype.SelectedIndex == 1)
                {
                    esn = dgv_showdata["esn", dgv_showdata.CurrentRow.Index].Value.ToString();
                }
                else
                {
                    esn = dgv_showdata["卡通号", dgv_showdata.CurrentRow.Index].Value.ToString();
                }
                string reccode = lblNewcarton.Text.Trim();
                int utype = cmbdptype.SelectedIndex + 1;
                // 更新选中的esn的卡通号 或栈板号
                refWebtWarehouseWipTracking.Instance.UpdatetWhWipInfo(esn, reccode, "", "", utype);

                //刷新dgv 

                RefDgv();
            }
        }


        private void RefDgv()
        {
            if (cmbdptype.SelectedIndex == 1)
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetCartonList(this.txtCode.Text.Trim()));
                DataView dv = dt.DefaultView;
                dv.Sort = "ESN ASC";
                DataTable dTemp = dv.ToTable(); 
                dgv_showdata.DataSource = FrmBLL.DataTableCrosstab.DataTableCross(dTemp,dTemp.Columns.Count);

                DataTable dt1 = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetCartonList(this.lblNewcarton.Text.Trim()));
                DataView dv1 = dt1.DefaultView;
                dv1.Sort = "ESN ASC";
                DataTable dTemp1 = dv1.ToTable();
                dgvPartition.DataSource = FrmBLL.DataTableCrosstab.DataTableCross(dTemp1, dTemp1.Columns.Count); 
                

            }
            else
            {
                dgv_showdata.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetPalletList(this.txtCode.Text.Trim()));
                dgvPartition.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetPalletList(this.lblNewcarton.Text.Trim()));

            }

        }

        //选错了退回
        private void btBack_Click(object sender, EventArgs e)
        {
            try
            {
                string esn;
                if (dgvPartition.RowCount >= 1 && dgvPartition.CurrentCell != null)
                {
                    if (cmbdptype.SelectedIndex == 1)
                    {
                        esn = dgvPartition["esn", dgv_showdata.CurrentRow.Index].Value.ToString();
                    }
                    else
                    {
                        esn = dgvPartition["卡通号", dgv_showdata.CurrentRow.Index].Value.ToString();
                    }
                    //esn = dgvPartition["esn", dgvPartition.CurrentRow.Index].Value.ToString();
                    string reccode = txtCode.Text.Trim();
                    int utype = cmbdptype.SelectedIndex + 1;
                    // 更新选中的esn的卡通号 或栈板号
                    refWebtWarehouseWipTracking.Instance.UpdatetWhWipInfo(esn, reccode, "", "", utype);
                    //刷新dgv 
                    RefDgv();
                }
            }
            catch (Exception ex)
            {
                if (mFrm is MainParent)
                {
                    (mFrm as MainParent).ShowPrgMsg(ex.Message, MainParent.MsgType.Outgoing);
                }

            }
        }

        private void dgvPartition_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                btBack_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }

        }

        //关闭窗体，出库窗体的列表信息
        private void DataPartition_FormClosed(object sender, FormClosedEventArgs e)
        {           

        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtCode.Text.Trim() != "")
            {
                // this.bt_query.Focus();
                txtCode_LostFocus(null, null);
            }
        }

        private void txtCode_MouseLeave(object sender, EventArgs e)
        {
            txtCode_LostFocus(null, null);

        }
        private void txtCode_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCode.Text.Trim()))
                {
                    if (cmbdptype.SelectedIndex == 1)
                    {
                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetCartonList(txtCode.Text.Trim()));

                        DataView dv = dt.DefaultView;
                        dv.Sort = "ESN ASC";
                        DataTable dTemp = dv.ToTable();
                        mDatatable = FrmBLL.DataTableCrosstab.DataTableCross(dTemp, dTemp.Columns.Count);
                    }
                    else
                    {
                        mDatatable = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetPalletList(txtCode.Text.Trim()));
                    }
                    this.dgv_showdata.DataSource = mDatatable;
                    if (mDatatable.Rows.Count == 0)
                    {
                        lblNewcarton.Text = "";

                        txtCode.Text = "";
                        MessageBox.Show("该号码错误或没有库存！");

                    }
                    else
                    {
                        Random rad = new Random();//实例化随机数产生器rad；
                        int value = rad.Next(1000, 10000);
                        if (cmbdptype.SelectedIndex == 1)
                        {
                            lblNewcarton.Text = mDatatable.Rows[0]["woId"].ToString() + "U" + value.ToString();
                        }
                        else
                        {
                            lblNewcarton.Text = "PU" + value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void cmbdptype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbdptype.SelectedIndex == 1)
            {
                label2.Text = "[被拆分箱]";
                label3.Text = "[拆出箱号]:";
            }
            else
            {
                label2.Text = "[被拆分栈板]";
                label3.Text = "[拆出栈板号]:";
            }

            txtCode.Text = "";
            //RefDgv();
            txtCode.Select();
            txtCode.Focus();

        }

        private void cmbcombine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbcombine.SelectedIndex == 1)
            {
                lblSource.Text = "[被合并箱]";
                lblTarget.Text = "[目标箱号]:";
            }
            else
            {
                label2.Text = "[被合并栈板]";
                label3.Text = "[目标栈板号]:";
            }
        }

        private void txtTarget_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtTarget.Text.Trim() != "")
            {
                txtTarget_LostFocus(null, null);
            }
        }

        private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtSource.Text.Trim() != "")
            {
                txtSource_LostFocus(null, null);
            }
        }

        private void txtSource_MouseLeave(object sender, EventArgs e)
        {
            txtSource.LostFocus += new EventHandler(txtSource_LostFocus);
        }
        private void txtSource_LostFocus(object sender, EventArgs e)
        {
            getdata(txtSource.Text.Trim(), txtSource, lblsourcehouse, lblsourceloc, dgvSource);
        }

        private void txtTarget_MouseLeave(object sender, EventArgs e)
        {
            txtTarget.LostFocus += new EventHandler(txtTarget_LostFocus);
        }

        private void txtTarget_LostFocus(object sender, EventArgs e)
        {
            getdata(txtTarget.Text.Trim(), txtTarget, lbltargethouse, lbltargetloc, dgvTarget);
        }

        private void getdata(string reccode, TextBox txtreccode, Label houseId, Label locId, DataGridView dgv)
        {
            try
            {

                if (!string.IsNullOrEmpty(reccode))
                {
                    DataTable tmpdt;
                    if (this.cmbcombine.SelectedIndex == 1)
                    {
                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetCartonList(reccode));
                        DataView dv = dt.DefaultView;
                        dv.Sort = "ESN ASC";
                        DataTable dTemp = dv.ToTable();
                        tmpdt = FrmBLL.DataTableCrosstab.DataTableCross(dTemp,dTemp.Columns.Count);
                    }
                    else
                    {
                        tmpdt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetPalletList(reccode));
                    }
                    dgv.DataSource = tmpdt;
                    if (tmpdt.Rows.Count == 0)
                    {
                        houseId.Text = ""; locId.Text = ""; txtreccode.Text = "";
                        MessageBox.Show("该号码错误或没有库存！");

                    }
                    else
                    {

                        houseId.Text = tmpdt.Rows[0]["storehouseId"].ToString();
                        locId.Text = tmpdt.Rows[0]["locId"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }

        }

        //合并
        private void bt_combine1_Click(object sender, EventArgs e)
        {

            if (txtSource.Text.Trim() == "")
            {
                MessageBox.Show("被合并项不能为空！"); return;
            }
            if (txtTarget.Text.Trim() == "")
            {
                MessageBox.Show("目标项不能为空！"); return;
            }
            if (txtSource.Text.Trim() == txtTarget.Text.Trim())
            {
                MessageBox.Show("被合并项与目标项相同，请重新改变其中任何一项！"); return;
            }

            string esn;

            if (this.dgvSource.RowCount >= 1 && dgvSource.CurrentCell != null) // 大于等于1
            {
                if (this.cmbcombine.SelectedIndex == 1)
                {
                    esn = dgvSource["esn", dgvSource.CurrentRow.Index].Value.ToString();
                }
                else
                {
                    esn = dgvSource["卡通号", dgvSource.CurrentRow.Index].Value.ToString();
                }
                string reccode = txtTarget.Text.Trim();
                string storehouseId = lbltargethouse.Text.Trim();
                string locId = lbltargetloc.Text.Trim();
                int utype = cmbcombine.SelectedIndex + 3;
                // 更新选中的esn的卡通号 或栈板号以及库位编号
                refWebtWarehouseWipTracking.Instance.UpdatetWhWipInfo(esn, reccode, storehouseId, locId, utype);
                RefCombineDgv();
            }

        }

        private void dgvSource_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                bt_combine1_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }

        }

        private void RefCombineDgv()
        {
            if (cmbcombine.SelectedIndex == 1)
            {
               DataTable dt=  FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetCartonList(this.txtSource.Text.Trim()));
                DataView dv = dt.DefaultView;
                dv.Sort = "ESN ASC";
                DataTable dTemp = dv.ToTable(); 
                dgvSource.DataSource = FrmBLL.DataTableCrosstab.DataTableCross(dTemp,dTemp.Columns.Count);
                DataTable dts = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetCartonList(this.txtTarget.Text.Trim()));
                DataView dv1 = dts.DefaultView;
                dv1.Sort = "ESN ASC";
                DataTable dTemp1 = dv1.ToTable();
                dgvTarget.DataSource = FrmBLL.DataTableCrosstab.DataTableCross(dTemp1, dTemp1.Columns.Count);


            }
            else
            {
                dgvSource.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetPalletList(this.txtSource.Text.Trim()));
                dgvTarget.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetPalletList(this.txtTarget.Text.Trim()));

            }

        }

        #region  添加行号
        private void dgvSource_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
               e.RowBounds.Location.Y,
               dgvSource.RowHeadersWidth - 4,
               e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgvSource.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgvSource.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

        }

        private void dgvTarget_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
              e.RowBounds.Location.Y,
              dgvTarget.RowHeadersWidth - 4,
              e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgvTarget.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgvTarget.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);


        }

        private void dgv_showdata_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
              e.RowBounds.Location.Y,
              dgv_showdata.RowHeadersWidth - 4,
              e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgv_showdata.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgv_showdata.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgvPartition_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
             e.RowBounds.Location.Y,
             dgvPartition.RowHeadersWidth - 4,
             e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgvPartition.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgvPartition.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
        #endregion

        private void tsmi_printpre_Click(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)contextMenuStrip1.SourceControl;
            if (dgv.DataSource == null || dgv.Rows.Count < 1)
                return;

            if (cmbdptype.SelectedIndex == 1)
                PrintCartonLabel(dgv["cartonnumber", 0].Value.ToString());
            else
                PrintPalletLabel(dgv["栈板号", 0].Value.ToString());

        }

        private void PrintCartonLabel(string carton_value)
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductInstockSerialInfo("cartonnumber", carton_value));
            if (dt.Rows.Count > 0)
            {
                DataView dv = dt.DefaultView;
                dv.Sort = "ESN ASC";
                DataTable dTemp = dv.ToTable();
                Frm_PrintDataPartision pdp = new Frm_PrintDataPartision(this, FrmBLL.DataTableCrosstab.DataTableCross(dTemp, 4));
                pdp.ShowDialog();
            }
        }
        private void PrintPalletLabel(string pallet_value)
        {
            MessageBox.Show("栈板功能未开放");
            //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductInstockSerialInfo("palletnumber", pallet_value));
            //Frm_PrintDataPartision pdp = new Frm_PrintDataPartision(this, dt);
            //pdp.ShowDialog();
        }

        private void dgv_showdata_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            lb_source.Text = "";
            if (dgv_showdata.Rows.Count > 0)
            {
                int sum = 0;
                if (cmbdptype.SelectedIndex == 0)
                {
                    for (int i = 0; i < dgv_showdata.Rows.Count; i++)
                    {
                        sum += Convert.ToInt32(dgv_showdata["数量", i].Value.ToString());
                    }
                }
                else
                {
                    sum = dgv_showdata.Rows.Count;
                }
                lb_source.Text = "总数量:" + sum.ToString();
            }
        }

        private void dgvPartition_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.lb_partion.Text = "";
            if (dgvPartition.Rows.Count > 0)
            {
                int sum = 0;
                if (cmbdptype.SelectedIndex == 0)
                {
                    for (int i = 0; i < dgvPartition.Rows.Count; i++)
                    {
                        sum += Convert.ToInt32( dgvPartition["数量", i].Value.ToString());
                    }
                }
                else
                {
                    sum = dgvPartition.Rows.Count;
                }
                this.lb_partion.Text = "总数量:" + sum.ToString();
            }
        }
    }
}

