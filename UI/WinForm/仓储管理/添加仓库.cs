using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RefWebService_BLL;
using DevComponents.DotNetBar;

namespace SFIS_V2
{
    public partial class Frmwarehouse : Office2007Form
    {
        public Frmwarehouse(MainParent frm, StoreLocManage floc)
        {
            InitializeComponent();
            this.mFrm = frm;
            this.mFloc = floc;
        }
        MainParent mFrm;
        StoreLocManage mFloc;
        private delegate void delegateloadstore();
        delegateloadstore dls;
        private void LoadFunc()
        {
            ShowStorehouse(FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseInfo()));
            ShowStoreType(FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GettStorehouseType()));
        }
        private void ShowStorehouse(DataTable dt)
        {
            dgv_warehouseinfo.Invoke(new EventHandler(delegate 
                {
                     dgv_warehouseinfo.DataSource = dt;
                }));           
        }
        private void ShowStoreType(DataTable dt)
        {
            cb_storetype.Invoke(new EventHandler(delegate {
                foreach (DataRow dr in dt.Rows)
                {
                    cb_storetype.Items.Add(dr["storehousetype"].ToString());
                }
            }));
        }
        private void Frmwarehouse_Load(object sender, EventArgs e)
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
            dls = new delegateloadstore(LoadFunc);
            dls.BeginInvoke(null, null);
            //try
            //{               
            //    dgv_warehouseinfo.DataSource = refWebtStorehouseManage.Instance.GetAlltStorehouseInfo();
            //    DataTable dt;
            //    dt = refWebtStorehouseManage.Instance.GettStorehouseType();
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        this.cb_storetype.Items.Add(dr["storehousetype"].ToString());
            //    }
            //}
            //catch (Exception ex)
            //{

            //    mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            //}

        }

        private void bt_addstore_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.tb_storeid.Text.Trim()))
                    throw new Exception("请填写仓库编号");
                if (string.IsNullOrEmpty(this.tb_storename.Text.Trim()))
                    throw new Exception("请填写仓库名称");
                if (string.IsNullOrEmpty(this.tb_storeman.Text))
                    throw new Exception("请选择负责人");
                if (string.IsNullOrEmpty(this.tb_storedesc.Text.Trim()))
                    throw new Exception("请填写仓库描述");
                if (string.IsNullOrEmpty(this.cb_storetype.Text))
                    throw new Exception("请选择仓库类型");

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("STOREHOUSEID", this.tb_storeid.Text.Trim());
                dic.Add("STOREHOUSENAME", this.tb_storename.Text.Trim());
                dic.Add("STOREHOUSEDESC", this.tb_storedesc.Text.Trim());
                dic.Add("STOREHOUSEMAN", this.tb_storeman.Text.Trim());
                dic.Add("STOREHOUSETYPE", this.cb_storetype.Text.Trim());
                dic.Add("REMARK", this.tb_remark.Text.Trim());
                refWebtStorehouseManage.Instance.AddStorehouse(FrmBLL.ReleaseData.DictionaryToJson(dic));

                //refWebtStorehouseManage.Instance.AddStorehouse(new WebServices.tStorehouseManage.tStorehouseInfo() 
                //{
                //    storehouseId = this.tb_storeid.Text.Trim(),
                //    storehouseman = this.tb_storeman.Text.Trim(),
                //    storehousedesc = this.tb_storedesc.Text.Trim(),
                //    storehousename = this.tb_storename.Text.Trim(),
                //    storehousetype = this.cb_storetype.Text,
                //    remark = this.tb_remark.Text.Trim()
                //});
                this.mFrm.ShowPrgMsg("增加仓库信息成功", MainParent.MsgType.Outgoing);
                dgv_warehouseinfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseInfo());

                bt_clear_Click(null, null);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

        }

        private void bx_resman_Click(object sender, EventArgs e)
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo());
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if ((dt.Columns[i].ToString().ToUpper() == "PWD") || (dt.Columns[i].ToString().ToUpper() == "密码"))
                {
                    dt.Columns.Remove(dt.Columns[i].ToString());
                }
            }
            SelectData sd = new SelectData(this, dt);
            sd.ShowDialog();

            //SelectData sd = new SelectData(this, FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo()));
            //sd.ShowDialog();
        }

        private void cb_storetype_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.cb_storetype.Text))
                {
                    DataTable dt;
                    dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GettStorehouseType());
                    if (dt != null)
                    {
                        if (dt.Rows.Count < 1)
                        {
                            if (MessageBox.Show("输入的仓库类型不存在，是否确认添加?\n", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                this.cb_storetype.Text = "";
                                this.cb_storetype.Focus();
                            }
                            else
                                this.tb_storedesc.Focus();
                        }
                        else
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (this.cb_storetype.Text == dr["storehousetype"].ToString())
                                {
                                    this.tb_storedesc.Focus();
                                    return;
                                }
                            }
                            if (MessageBox.Show("输入的仓库类型不存在，是否确认添加?\n", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                this.cb_storetype.SelectAll();
                                this.cb_storetype.Focus();
                            }
                            else
                                this.tb_storedesc.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void dgv_warehouseinfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    this.tb_storeid.Text = dgv_warehouseinfo["storehouseId", e.RowIndex].Value.ToString();
                    this.tb_storename.Text = dgv_warehouseinfo["storehousename", e.RowIndex].Value.ToString();
                    this.tb_storeman.Text = dgv_warehouseinfo["storehouseman", e.RowIndex].Value.ToString();
                    this.cb_storetype.Text = dgv_warehouseinfo["storehousetype", e.RowIndex].Value.ToString();
                    this.tb_storedesc.Text = dgv_warehouseinfo["storehousedesc", e.RowIndex].Value.ToString();
                    this.tb_remark.Text = dgv_warehouseinfo["remark", e.RowIndex].Value.ToString();
                    this.tb_storeid.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
            this.tb_storeid.Text = "";
            this.tb_storename.Text = "";
            this.tb_storeman.Text = "";
            this.cb_storetype.Text = "";
            this.tb_storedesc.Text = "";
            this.tb_remark.Text = "";
            this.bt_addstore.Enabled = true;
            this.bt_updatestore.Enabled = true;
            this.tb_storeid.Enabled = true;
        }

        private void bt_selectwarehouse_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_number.Text))
                dgv_warehouseinfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GettStorehouseInfoById(this.tb_number.Text.Trim()));

        }

        private void tb_storeid_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_storeid.Text))
            {
                DataTable dt;
                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseInfo());
                if (dt != null && dt.Rows.Count > 1)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (this.tb_storeid.Text.Trim() == dr["storehouseId"].ToString())
                        {
                            if (MessageBox.Show("该仓库编号已存在，请重置!\n", "提示", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                this.tb_storeid.Text = "";
                                this.tb_storeid.Focus();
                            }
                            else
                            {
                                this.tb_storeid.SelectAll();
                                this.tb_storeid.Focus();
                            }
                        }
                    }
                }
            }
        }

        private void bt_updatestore_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_storeid.Text.Trim()))
            {
                this.tb_storeid.Enabled = false;
                this.bt_addstore.Enabled = false;
                DataTable dt;
                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GettStorehouseInfoById(this.tb_storeid.Text.Trim()));
                if(dt!=null&&dt.Rows.Count<1)
                    MessageBox.Show("该仓库编号不存在，无法更新!");
                else
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("storehousename".ToUpper(),tb_storename.Text.Trim());
                    dic.Add("storehousedesc".ToUpper(), tb_storedesc.Text.Trim());
                    dic.Add("storehouseman".ToUpper(), tb_storeman.Text.Trim());
                    dic.Add("storehousetype".ToUpper(), cb_storetype.Text.Trim());
                    dic.Add("remark".ToUpper(), tb_remark.Text.Trim());
                    dic.Add("storehouseId".ToUpper(), tb_storeid.Text.Trim());
                    refWebtStorehouseManage.Instance.UpdateStorehouse(FrmBLL.ReleaseData.DictionaryToJson(dic));
                    //refWebtStorehouseManage.Instance.UpdateStorehouse(new WebServices.tStorehouseManage.tStorehouseInfo()
                    //{
                    //    storehousedesc = this.tb_storedesc.Text.Trim(),
                    //    storehouseman = this.tb_storeman.Text,
                    //    storehousename = this.tb_storename.Text.Trim(),
                    //    storehousetype = this.cb_storetype.Text,
                    //    remark = this.tb_remark.Text.Trim(),
                    //    storehouseId = this.tb_storeid.Text.Trim()
                    //});
                    this.mFrm.ShowPrgMsg("更新仓库信息成功!", MainParent.MsgType.Outgoing);
                    dgv_warehouseinfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseInfo());
                }
                bt_clear_Click(null, null);
            }
        }
    }
}
