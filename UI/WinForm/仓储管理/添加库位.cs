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
    public partial class FrmWarehouseManage : Office2007Form// Form
    {
        public FrmWarehouseManage(MainParent ware,StoreLocManage msl)
        {
            InitializeComponent();
            this.mWare = ware;
            this.mSloc = msl;
            dic.Add("仓库编号", "storehouseId");
            dic.Add("库位编号", "locId");
        }
        private enum EdtType
        {
            新增,
            修改,
            删除
        }
        EdtType medttye;
        MainParent mWare;
        StoreLocManage mSloc;
        private Dictionary<string, string> dic = new Dictionary<string, string>();

        private delegate void delegatelocation();
        delegatelocation dl;
        private void LoadLoc()
        {
            ShowLoc(FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseLoctionInfo()));
            ShowLocType(FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GettStorehouseLoctionType()));
            ShowStoreType(FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseInfo()));
        }
        private void ShowLoc(DataTable dt)
        {
            dgv_warehouseinfo.Invoke(new EventHandler(delegate
            {
                dgv_warehouseinfo.DataSource = dt;
            }));
        }
        private void ShowLocType(DataTable dt)
        {
            cb_loctype.Invoke(new EventHandler(delegate
            {
                foreach (DataRow dr in dt.Rows)
                {
                    this.cb_loctype.Items.Add(dr["loctype"].ToString());
                }
            }));
        }
        private void ShowStoreType(DataTable dt)
        {
            cb_warehouse.Invoke(new EventHandler(delegate {
                foreach (DataRow dr in dt.Rows)
                {
                    this.cb_warehouse.Items.Add(dr["storehouseId"].ToString());
                }
            }));
        }
        private void bt_select_Click(object sender, EventArgs e)
        {

            dgv_warehouseinfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GettStorehouseLoctionInfo(dic[this.cb_selcondition.Text.Trim()], this.tb_number.Text.Trim()));
        }

        //private void bt_addwarehouse_Click(object sender, EventArgs e)
        //{
        //    Frmwarehouse wh = new Frmwarehouse(mWare, this);
        //    wh.ShowDialog();
        //    bool st=bool.Parse(wh.IsDisposed.ToString());
        //    if (!st)
        //    {
        //        this.cb_warehouse.Items.Clear();
        //        DataTable dt;
        //        dt = refWebtStorehouseManage.Instance.GetAlltStorehouseInfo();
        //        foreach (DataRow dr in dt.Rows)
        //        {                    
        //            this.cb_warehouse.Items.Add(dr["storehouseId"].ToString());
        //        }
        //    }
        //}

        private void bt_saveinfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.tb_locid.Text.Trim()))
                    throw new Exception("请输入库位编号");
                if (string.IsNullOrEmpty(this.tb_loctotal.Text.Trim()))
                    throw new Exception("请输入库位容量");
                if (string.IsNullOrEmpty(this.tb_locdesc.Text.Trim()))
                    throw new Exception("请输入库位描述");
                if (string.IsNullOrEmpty(this.cb_loctype.Text))
                    throw new Exception("请选择库位类型");
                if (string.IsNullOrEmpty(this.cb_warehouse.Text))
                    throw new Exception("请选择所属仓库");

                this.bt_deleteinfo.Enabled = false;
                this.bt_updateinfo.Enabled = false;

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("LOCID", tb_locid.Text.Trim());
                dic.Add("UPLOCID", tb_uplocid.Text.Trim());
                dic.Add("LOCTYPE", cb_loctype.Text.Trim());
                dic.Add("STOREHOUSEID", cb_warehouse.Text.Trim());
                dic.Add("LOCDESC", tb_locdesc.Text.Trim());
                dic.Add("LOCTOTAL", int.Parse(this.tb_loctotal.Text.Trim()));
                dic.Add("REMARK", tb_remark.Text.Trim());
                refWebtStorehouseManage.Instance.AddStorehouseloction(FrmBLL.ReleaseData.DictionaryToJson(dic));
                //refWebtStorehouseManage.Instance.AddStorehouseloction(new WebServices.tStorehouseManage.tStorehouseLoctionInfo()
                //{
                //    locId = this.tb_locid.Text.Trim(),
                //    uplocId = this.tb_uplocid.Text.Trim(),
                //    locdesc = this.tb_locdesc.Text.Trim(),
                //    loctotal = int.Parse(this.tb_loctotal.Text.Trim()),
                //    loctype = this.cb_loctype.Text,
                //    storehouseId = this.cb_warehouse.Text,
                //    remark = this.tb_remark.Text.Trim()
                //});
                mWare.ShowPrgMsg("添加库位成功!", MainParent.MsgType.Outgoing);
                dgv_warehouseinfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable( refWebtStorehouseManage.Instance.GetAlltStorehouseLoctionInfo());


                bt_clearinfo_Click(null, null);
            }
            catch (Exception ex)
            {
                mWare.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_updateinfo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_locid.Text))
            {
                this.tb_locid.Enabled = false;
                this.bt_saveinfo.Enabled = false;
                this.bt_deleteinfo.Enabled = false;

                DataTable dt;
                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GettStorehouseLoctionInfoById(this.tb_locid.Text.Trim()));
                if (dt != null && dt.Rows.Count < 1)
                    MessageBox.Show("该库位不存在，无法更新!");
                else
                {
                    //  MessageBox.Show( this.tb_locdesc.Text.Trim());
                    // MessageBox.Show( this.tb_loctotal.Text.Trim());
                    // MessageBox.Show( this.cb_loctype.Text);
                    // MessageBox.Show(  this.cb_warehouse.Text);
                    //MessageBox.Show( this.tb_uplocid.Text.Trim());
                    //MessageBox.Show(this.tb_remark.Text.Trim());

                    //BLL.tStorehouseManage.UpdateStorehouseloction(new Entity.tStorehouseLoctionInfo()
                    //{
                    //    locdesc = this.tb_locdesc.Text.Trim(),
                    //    loctotal = int.Parse(this.tb_loctotal.Text.Trim()),
                    //    loctype = this.cb_loctype.Text,
                    //    storehouseId = this.cb_warehouse.Text,
                    //    uplocId = this.tb_uplocid.Text.Trim(),
                    //    remark = this.tb_remark.Text.Trim(),
                    //}, this.tb_locid.Text.Trim());

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("uplocId".ToUpper(), tb_uplocid.Text.Trim());
                    dic.Add("loctype".ToUpper(), cb_loctype.Text.Trim());
                    dic.Add("storehouseId".ToUpper(), cb_warehouse.Text.Trim());
                    dic.Add("locdesc".ToUpper(), tb_locdesc.Text.Trim());
                    dic.Add("loctotal".ToUpper(), int.Parse(this.tb_loctotal.Text.Trim()));
                    dic.Add("remark".ToUpper(), tb_remark.Text.Trim());
                    dic.Add("locId".ToUpper(), tb_locid.Text.Trim());
                    refWebtStorehouseManage.Instance.UpdateStorehouseloction(FrmBLL.ReleaseData.DictionaryToJson(dic));


                    //refWebtStorehouseManage.Instance.UpdateStorehouseloction(new WebServices.tStorehouseManage.tStorehouseLoctionInfo()
                    //   {
                    //       locdesc = this.tb_locdesc.Text.Trim(),
                    //       loctotal = int.Parse(this.tb_loctotal.Text.Trim()),
                    //       loctype = this.cb_loctype.Text,
                    //       storehouseId = this.cb_warehouse.Text,
                    //       uplocId = this.tb_uplocid.Text.Trim(),
                    //       remark = this.tb_remark.Text.Trim(),
                    //   }, this.tb_locid.Text.Trim());
                    this.mWare.ShowPrgMsg("更新库位信息成功!", MainParent.MsgType.Outgoing);
                }
            }
            bt_clearinfo_Click(null, null);
            dgv_warehouseinfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseLoctionInfo());
        }

        private void bt_deleteinfo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_locid.Text.Trim()))
            {
                this.tb_locid.Enabled = false;
                this.bt_saveinfo.Enabled = false;
                this.bt_updateinfo.Enabled = false;

                DataTable dt;
                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GettStorehouseLoctionInfoById(this.tb_locid.Text.Trim()));
                //dgv_warehouseinfo.DataSource = dt;
                if (dt != null && dt.Rows.Count < 1)
                    MessageBox.Show("该库位不存在，无法删除!");
                else
                {
                    refWebtStorehouseManage.Instance.DeleteStorehouseloction(this.tb_locid.Text);
                    this.mWare.ShowPrgMsg("删除该库位信息成功!", MainParent.MsgType.Outgoing);
                }
            }
            bt_clearinfo_Click(null, null);
            dgv_warehouseinfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseLoctionInfo());
        }

        private void FrmWarehouseManage_Load(object sender, EventArgs e)
        {

            #region 添加应用程序
            if (this.mWare.gUserInfo.rolecaption == "系统开发员")
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
            this.medttye = EdtType.新增;

            dl = new delegatelocation(LoadLoc);
            dl.BeginInvoke(null,null);
            //dgv_warehouseinfo.DataSource = refWebtStorehouseManage.Instance.GetAlltStorehouseLoctionInfo();
            //dt = refWebtStorehouseManage.Instance.GettStorehouseLoctionType();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    this.cb_loctype.Items.Add(dr["loctype"].ToString());
            //}
            //d = refWebtStorehouseManage.Instance.GetAlltStorehouseInfo();
            //foreach (DataRow dr in d.Rows)
            //{
            //    this.cb_warehouse.Items.Add(dr["storehouseId"].ToString());
            //}
        }

        private void dgv_warehouseinfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    this.tb_locid.Text = dgv_warehouseinfo["locId", e.RowIndex].Value.ToString();
                    this.cb_loctype.Text = dgv_warehouseinfo["loctype", e.RowIndex].Value.ToString();
                    this.tb_loctotal.Text = dgv_warehouseinfo["loctotal", e.RowIndex].Value.ToString();
                    this.cb_warehouse.Text = dgv_warehouseinfo["storehouseId", e.RowIndex].Value.ToString();
                    this.tb_locdesc.Text = dgv_warehouseinfo["locdesc", e.RowIndex].Value.ToString();
                    this.tb_uplocid.Text = dgv_warehouseinfo["uplocId", e.RowIndex].Value.ToString();
                    this.tb_remark.Text = dgv_warehouseinfo["remark", e.RowIndex].Value.ToString();
                    this.medttye = EdtType.修改;
                }

            }
            catch (Exception ex)
            {
                mWare.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_clearinfo_Click(object sender, EventArgs e)
        {
            this.medttye = EdtType.新增;
            this.tb_locid.Text = "";
            this.tb_locdesc.Text = "";
            this.tb_loctotal.Text = "";
            this.tb_remark.Text = "";
            this.tb_uplocid.Text = "";
            this.cb_warehouse.Text = "";
            this.cb_loctype.Text = "";
            this.bt_deleteinfo.Enabled = true;
            this.bt_saveinfo.Enabled = true;
            this.bt_updateinfo.Enabled = true;
            this.tb_locid.Enabled = true;
        }

        private void tb_locid_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_locid.Text))
            {
                if (string.IsNullOrEmpty(this.cb_warehouse.Text))
                {
                    MessageBoxEx.Show("请选择仓库");
                    this.cb_warehouse.Focus();
                    return;
                }

                if (refWebtStorehouseManage.Instance.ChkStoreLocaltion(this.cb_warehouse.Text.Trim(),
                     this.tb_locid.Text.Trim()))
                {
                    MessageBoxEx.Show("填写的库位在该仓库中已经存在!!，请重新输入");
                    this.tb_locid.Text = "";
                    this.tb_locid.Focus();
                    return;
                }
            }
        }

        private void bt_checkloc_Click(object sender, EventArgs e)
        {
            tb_locid_Leave(null, null);
        }
        /// <summary>
        /// 验证库位类型，不存在则插入库位类型表中.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_loctype_Leave(object sender, EventArgs e)
        {
            try
            {
                DataTable dt;
                if (!string.IsNullOrEmpty(this.cb_loctype.Text))
                {
                    dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GettStorehouseLoctionType());
                    if (dt.Rows.Count < 1)//库中没信息
                    {
                        if (MessageBox.Show("输入的库位类型不存在,是否确认增加?\n", "提示",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            refWebtStorehouseManage.Instance.AddStorehouselocType(this.cb_loctype.Text.Trim());
                            this.tb_loctotal.Focus();
                        }
                        else
                        {
                            this.cb_loctype.SelectAll();
                            this.cb_loctype.Focus();
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (this.cb_loctype.Text == dr["loctype"].ToString())
                            {
                                this.tb_loctotal.Focus();
                                return;
                            }
                        }
                        if (MessageBox.Show("输入的库位类型不存在,是否确认增加?\n", "提示", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            refWebtStorehouseManage.Instance.AddStorehouselocType(this.cb_loctype.Text.Trim());
                            this.tb_loctotal.Focus();
                        }
                        else
                        {
                            this.cb_loctype.SelectAll();
                            this.cb_loctype.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mWare.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void cb_warehouse_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cb_loctype.Text))
            {
                DataTable dt;
                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseInfo());
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (this.cb_warehouse.Text == dr["storehouseId"].ToString())
                            return;
                    }
                    MessageBox.Show("该仓库编号不存在，请添加!");
                    this.cb_warehouse.SelectAll();
                    this.cb_warehouse.Focus();
                }
            }
        }

        private void tb_locid_Enter(object sender, EventArgs e)
        {
            if (this.medttye == EdtType.修改 || this.medttye == EdtType.删除)
            {
                this.tb_locid.Enabled = false;
            }
            if (this.medttye == EdtType.新增)
            {
                this.tb_locid.Enabled = true;
            }
        }

        private void tb_loctotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
