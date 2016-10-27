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
    public partial class ManageRoute : Office2007Form // Form
    {
        public ManageRoute(MainParent frm)
        {
            InitializeComponent();
            this.mFrm = frm;
        }
        MainParent mFrm;

        private delegate void delegateroutemanage();
        delegateroutemanage drm;
        private void LoadData()
        {
            ShowData(FrmBLL.ReleaseData.arrByteToDataTable(refWebtRouteInfo.Instance.GetRouteManage(null)));
        }
        private void ShowData(DataTable _dt)
        {
            this.dgv_routemanage.Invoke(new EventHandler(delegate
            {
                dgv_routemanage.DataSource = _dt;
            }));
        }
        private void ManageRoute_Load(object sender, EventArgs e)
        {
            drm = new delegateroutemanage(LoadData);
            drm.BeginInvoke(null, null);

           string [] item = refWebtBomKeyPart.Instance.GetBomNumerList();
           foreach (string  im in item)
           {
               cb_bomnumber.Items.Add(im);
           }
        }
        private void tb_routgroupid_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_routgroupid.Text.Trim()))
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtRouteInfo.Instance.GetAllRouteInfo());
                foreach (DataRow dr in dt.Rows)
                {
                    if (this.tb_routgroupid.Text.Trim() == dr["routgroupId"].ToString())
                    {
                        return;
                    }
                }
                this.mFrm.ShowPrgMsg("该流程编号不存在!", MainParent.MsgType.Incoming);
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.tb_routgroupid.Text.Trim()))
                    throw new Exception("流程编号不能为空!");
                if (string.IsNullOrEmpty(this.tb_partnumber.Text.Trim()))
                    throw new Exception("成品料号不能为空!");

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("ROUTGROUPID", tb_routgroupid.Text.Trim());
                dic.Add("PARTNUMBER", tb_partnumber.Text.Trim());
                dic.Add("PRODUCTNAME", tb_productname.Text.Trim());
                dic.Add("BOMNUMBER", cb_bomnumber.Text.Trim());
                refWebtRouteInfo.Instance.InsertRouteManage(FrmBLL.ReleaseData.DictionaryToJson(dic));
                //refWebtRouteInfo.Instance.InsertRouteManage(new WebServices.tRouteInfo.tRouteInfo1()
                //{
                //    routgroupId = this.tb_routgroupid.Text.Trim(),
                //    Partnumber = this.tb_partnumber.Text.Trim(),
                //    ProductName = this.tb_productname.Text.Trim(),
                //    BomNumber = this.cb_bomnumber.Text.Trim()
                //});
              
                MessageBox.Show("添加成功");
                drm = new delegateroutemanage(LoadData);
                drm.BeginInvoke(null, null);
                bt_clear_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bti_select_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cbi_selectcondition.Text) || !string.IsNullOrEmpty(tbi_value.Text))
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    if (this.cbi_selectcondition.SelectedItem.ToString()=="成品料号")
                    dic.Add("PARTNUMBER", this.tbi_value.Text.Trim());
                    else
                      dic.Add("ROUTGROUPID", this.tbi_value.Text.Trim());//流程编号
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtRouteInfo.Instance.GetRouteManage(FrmBLL.ReleaseData.DictionaryToJson(dic)));
                    dgv_routemanage.DataSource = dt;
                }
                else
                    throw new Exception("查询条件为空");
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Incoming);
            }
        }

        private void bt_routegroup_Click(object sender, EventArgs e)
        {
            //ShowRoutGroup srg = new ShowRoutGroup(this);
            //srg.ShowDialog();

            Frm_RouteSelect fr = new Frm_RouteSelect(this);
            fr.ShowDialog();
        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
            this.tb_routgroupid.Text = "";
            this.tb_partnumber.Text = "";
            this.tb_productname.Text = "";
            this.cb_bomnumber.SelectedIndex = -1;
        }

        private void btLoadPartnumber_Click(object sender, EventArgs e)
        {
            SelectData sd = new SelectData(this, 
                FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetAllProduct()));
            sd.ShowDialog();
        }

        private void imbt_loadBom_Click(object sender, EventArgs e)
        {
          
        }

        private void dgv_routemanage_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                tb_routgroupid.Text = dgv_routemanage[0, e.RowIndex].Value.ToString();
                tb_partnumber.Text  = dgv_routemanage[1, e.RowIndex].Value.ToString();
               tb_productname.Text = dgv_routemanage[2, e.RowIndex].Value.ToString();
                cb_bomnumber.Text = dgv_routemanage[3, e.RowIndex].Value.ToString();
            }
        }

    }
}
