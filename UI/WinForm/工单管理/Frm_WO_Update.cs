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
    public partial class Frm_WO_Update : Office2007Form //Form
    {
        public Frm_WO_Update(int sFlag ,Office2007Form Frm)
        {
            InitializeComponent();
            Flag = sFlag;
            mFrm = Frm;
        }
        /// <summary>
        /// 0 新增工单,1 修改工单,2 在线修改工单信息
        /// </summary>
        int Flag = 0;
        DataGridViewCellEventArgs dgve;
        DataTable dt_erp_woinfo = null;
        DataGridView dgv_Info ;
        string Sap_WoType = string.Empty;
        Office2007Form mFrm;
        /// <summary>
        /// 条码类型
        /// </summary>
        public string Serial_Type = string.Empty;

        private void Frm_WO_Update_Load(object sender, EventArgs e)
        {

            dgv_Info = (mFrm as Frm_Crate_MO).dgv_woinfo;
            dgve = (mFrm as Frm_Crate_MO).dgve;
            this.txt_qty.ReadOnly = true;
            this.txt_Partnumber.ReadOnly = true;
            this.txt_routgroupid.ReadOnly = true;
            this.txt_inputgroup.ReadOnly = true;
            this.txt_outputgroup.ReadOnly = true;
            this.txt_wotype.ReadOnly = true;
            this.txt_productname.ReadOnly = true;
            this.txt_poid.ReadOnly = true;
            this.cbx_woId.Enabled = false;
            this.txt_check_no.Enabled = false;
            this.txt_clear_serial_type.Enabled = false;
            this.txt_check_no.Text = "NA";
            Get_BomNunmber();
            if (Flag == 0)
            {
                this.cbx_woId.Enabled = true;
                this.Text = "建立工单";
                Get_ERP_WO();
               
            }
            if (Flag == 1)
            {
                this.Text = "修改工单信息";
                FillDataGridToTextBox(dgve, panelEx1);
            }
            if (Flag == 2)
            {
                this.Text = "修改工单信息";
                imbt_routeselect.Enabled = false;
                FillDataGridToTextBox(dgve, panelEx1);
            }

            Dload_Route = new DloadRoute(GetAllRoute);
            Dload_Route.BeginInvoke(null, null);

        }

        private delegate void DloadRoute();
        DloadRoute Dload_Route;
        /// <summary>
        /// 加载所有的途程信息
        /// </summary>
        public DataTable dtRoute = null;
        private void GetAllRoute()
        {
            dtRoute = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtRouteInfo.Instance.GetAllRouteInfo());
        }

        private void Get_BomNunmber()
        {
           string[] mBomNumberList = refWebtBomKeyPart.Instance.GetBomNumerList();
          this.cbx_bomnumber.Items.AddRange(mBomNumberList);
        }

        private void FillDataGridToTextBox(DataGridViewCellEventArgs e, Control Ctrl)
        {
            foreach (Control ctrl in Ctrl.Controls)
            {
                switch (ctrl.GetType().Name)
                {
                    case "TextBox":                     
                    case "TextBoxX":
                    case "ComboBoxEx":
                        {
                            ctrl.Text = dgv_Info[ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper(), e.RowIndex].Value.ToString();
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        private void Get_ERP_WO()
        {
            cbx_woId.Items.Clear();
            dt_erp_woinfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebt_wo_Info_Erp.Instance.Get_Erp_woinfo());
            if (dt_erp_woinfo.Rows.Count > 0)
            {
                foreach (DataRow dr in FrmBLL.publicfuntion.DataTableToSort(dt_erp_woinfo, "WOID").Rows)
                {
                    cbx_woId.Items.Add(dr["WOID"].ToString());
                }
                cbx_woId.SelectedIndex = 0;
            } 
        }
        private void Get_BomNumber()
        {

        }

        private void cbx_woId_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbx_woId.Text) && Flag==0)
            {
                this.txt_routgroupid.Text = null;
                this.txt_inputgroup.Text = null;
                this.txt_outputgroup.Text = null;
                this.cbx_bomnumber.Text = null;
                this.txt_sw_ver.Text = null;
                this.txt_fw_ver.Text = null;
                this.txt_nal_prefix.Text = null;
            }
        }

        private void imbt_routeselect_Click(object sender, EventArgs e)
        {
            Frm_RouteSelect fr = new Frm_RouteSelect(this);
            fr.ShowDialog();
        }

        private void cbx_woId_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(cbx_woId.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    DataTable dtwoinfo = FrmBLL.publicfuntion.getNewTable(dt_erp_woinfo, string.Format("WOID='{0}'", cbx_woId.Text));
                    if (dtwoinfo.Rows.Count == 0 && ChkSap.Checked)
                    {
                        dtwoinfo = Get_Erp_woInof(cbx_woId.Text);
                    }
                    if (dtwoinfo.Rows.Count > 0)
                    {
                        Sap_WoType = dtwoinfo.Rows[0]["SAPWOTYPE"].ToString();
                        this.txt_wotype.Text = FrmBLL.publicfuntion.GetWOtype(Sap_WoType);
                        foreach (Control ctrl in panelEx1.Controls)
                        {
                            foreach (DataColumn dc in dtwoinfo.Columns)
                            {
                                if (dc.ColumnName == ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper())
                                {
                                    switch (ctrl.GetType().Name)
                                    {
                                        case "TextBox":
                                            {
                                                ctrl.Text = dtwoinfo.Rows[0][ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper()].ToString();
                                                break;
                                            }
                                        case "TextBoxX":
                                            {
                                                ctrl.Text = dtwoinfo.Rows[0][ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper()].ToString();
                                                break;
                                            }
                                        case "ComboBoxEx":
                                            {
                                                ctrl.Text = dtwoinfo.Rows[0][ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper()].ToString();
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                }
                            }
                        }

                        DataTable dtProduct = FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetProductByPartNumber(txt_Partnumber.Text));
                        if (dtProduct.Rows.Count > 0)
                        {
                            txt_productname.Text = dtProduct.Rows[0]["PRODUCTNAME"].ToString();
                        }
                        DataTable dt_RouteManage = FrmBLL.ReleaseData.arrByteToDataTable(refWebtRouteInfo.Instance.GetRouteManageByPartnumber(txt_Partnumber.Text));
                        if (dt_RouteManage.Rows.Count > 0 && txt_wotype.Text != "Rework" && txt_wotype.Text != "RMA")
                        {
                            txt_routgroupid.Text = dt_RouteManage.Rows[0]["ROUTGROUPID"].ToString();
                            cbx_bomnumber.Text = dt_RouteManage.Rows[0]["BOMNUMBER"].ToString();
                            foreach (DataRow dr in dt_RouteManage.Rows)
                            {
                                if (dr["ROUTEDESC"].ToString().ToUpper() == "IN")
                                    txt_inputgroup.Text = dr["CRAFTNAME"].ToString();
                                if (dr["ROUTEDESC"].ToString().ToUpper() == "OUT")
                                    txt_outputgroup.Text = dr["CRAFTNAME"].ToString();
                            }
                        }

                        txt_factoryid.Text = string.IsNullOrEmpty(txt_factoryid.Text) ? "2100" : txt_factoryid.Text;
                    }
                    else
                    {
                        MessageBox.Show("请确认SAP是否有推送此工单....", "工单提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "工单提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void imbt_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbx_woId.Text))
            {
                MessageBox.Show("工单不能为空,请选择工单后点击Enter键确认选择", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (string.IsNullOrEmpty(txt_qty.Text) || string.IsNullOrEmpty(txt_Partnumber.Text)||string.IsNullOrEmpty(txt_productname.Text))
            {
                MessageBox.Show("数量,料号或产品描述 为空,请选择工单后点击Enter键确认选择", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (string.IsNullOrEmpty(txt_routgroupid.Text) || string.IsNullOrEmpty(txt_inputgroup.Text) || string.IsNullOrEmpty(txt_outputgroup.Text))
            {
                MessageBox.Show("请选择生产流程", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (dgve != null && dgv_Info.Rows[dgve.RowIndex].Cells["WOID"].Value.ToString() == cbx_woId.Text)
            {
                DataTable dt_db_wo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(dgv_Info.Rows[dgve.RowIndex].Cells["WOID"].Value.ToString(), null, "WOSTATE"));
                if (Convert.ToInt32(dt_db_wo.Rows[0]["WOSTATE"].ToString()) != Convert.ToInt32(dgv_Info.Rows[dgve.RowIndex].Cells["WOSTATE"].Value.ToString()))
                {
                    MessageBoxEx.Show("工单数据发生变更,点击确定后,刷新数据后再操作");
                    mFrm.Close();
                    this.Close();
                    return;
                }
            }
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (Flag == 0 || Flag == 1)
                {
                    FrmBLL.publicfuntion.SerializeControl(dic, panelEx1);
                    if (Flag == 0)
                    {
                        dic.Add("SAPWOTYPE", Sap_WoType);
                        dic.Add("WOSOURCEFLAG", "E");
                    }
                    dic.Add("WOSTATE", "0");
                    dic.Add("USERID", (mFrm as Frm_Crate_MO).UserId);
                    dic.Add("INPUTQTY", 0);
                    dic.Add("OUTPUTQTY", 0);
                    dic.Add("SCRAPQTY", 0);
                }
                if (Flag == 2)
                {
                    dic.Add("WOID",cbx_woId.Text);
                    dic.Add("BOMNUMBER", cbx_bomnumber.Text);
                    dic.Add("BOMVER", txt_bomver.Text);
                    dic.Add("NAL_PREFIX", txt_nal_prefix.Text);
                    dic.Add("PVER", txt_pver.Text);
                    dic.Add("SW_VER", txt_sw_ver.Text);
                    dic.Add("FW_VER", txt_fw_ver.Text);
                    dic.Add("CHECK_NO", txt_check_no.Text);
                    dic.Add("CLEAR_SERIAL_TYPE", txt_clear_serial_type.Text);
                }
                string _StrErr = CHK_PACKING_ROUTE();
                if (_StrErr != "OK")
                {
                    MessageBox.Show(_StrErr, "超时管控判定信息提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                string JsonStr = FrmBLL.ReleaseData.DictionaryToJson(dic);
                _StrErr = refWebtWoInfo.Instance.Insert_Wo_Info(JsonStr, null, null, null);
                if (_StrErr == "OK")
                {
                    if (Flag == 0)
                    {
                        dic = new Dictionary<string, object>();
                        dic.Add("WOID",cbx_woId.Text);
                        dic.Add("WORLSFLAG", "1");
                        refWebt_wo_Info_Erp.Instance.Update_Erp_woInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));
                        FrmBLL.publicfuntion.InserSystemLog((mFrm as Frm_Crate_MO).UserId, "工单信息", "Add_" + cbx_woId.Text, JsonStr);
                    }
                    if (Flag == 1)
                        FrmBLL.publicfuntion.InserSystemLog((mFrm as Frm_Crate_MO).UserId, "工单信息", "Modify_" + cbx_woId.Text, JsonStr);
                    if (Flag == 2)
                        FrmBLL.publicfuntion.InserSystemLog((mFrm as Frm_Crate_MO).UserId, "工单信息", "Modify_2_" + cbx_woId.Text, JsonStr);

                    MessageBox.Show(Flag == 0 ? "新建工单完成" : "修改工单完成", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(Flag == 0 ? "新建工单异常:\r\n" + _StrErr : "修改工单异常:\r\n" + _StrErr, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生异常:\r\n"+ex.Message);
            }

        }

        private void imbt_close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 获取控件名,值
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="Ctrl"></param>
        public static void SerializeControl(IDictionary<string, object> dic, Control Ctrl)
        {
            foreach (Control ctrl in Ctrl.Controls)
            {
                switch (ctrl.GetType().Name)
                {
                    case "TextBox":
                        dic.Add(ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper(), ctrl.Text);
                        break;
                    case "TextBoxX":
                        dic.Add(ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper(), ctrl.Text);
                        break;
                    case "ComboBox":
                        dic.Add(ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper(), ctrl.Text);
                        break;
                    case "ComboBoxEx":
                        {
                            dic.Add(ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper(), ctrl.Text);
                            break;
                        }
                    case "CheckBox":
                        {
                            dic.Add(ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper(), (ctrl as CheckBox).Checked);
                            break;
                        }
                    case "CheckBoxX":
                        {
                            dic.Add(ctrl.Name.Substring(4, ctrl.Name.Length - 4).ToUpper(), (ctrl as DevComponents.DotNetBar.Controls.CheckBoxX).Checked);
                            break;
                        }
                }
                if (ctrl.Controls.Count > 0)
                    SerializeControl(dic, ctrl);
            }
        }

        private void imbt_sekectcheckno_Click(object sender, EventArgs e)
        {
            Frm_TimeOutSelect tos = new Frm_TimeOutSelect(this);
            tos.ShowDialog();
        }

        /// <summary>
        /// 保存流程
        /// </summary>
        public DataTable mRouteTable = new DataTable("route");
        /// <summary>
        /// 检查途程(包装检查途程站位)
        /// </summary>
        public string mChkRoute = string.Empty;
        /// <summary>
        /// 退回途程(包装检查超时后退回站位)
        /// </summary>
        public string mRollBackRoute = string.Empty;
        public string CHK_PACKING_ROUTE()
        {
            if (mRouteTable.Rows.Count > 0 && !string.IsNullOrEmpty(mChkRoute) && !string.IsNullOrEmpty(mRollBackRoute) && this.txt_check_no.Text != "NA")
            {
                DataTable dt_ChkRoute = FrmBLL.publicfuntion.getNewTable(dtRoute, string.Format("ROUTGROUPID='{0}'", mRouteTable.Rows[0][0].ToString()));
                if (dt_ChkRoute.AsEnumerable().Where(h => h.Field<string>("CRAFTNAME") == mChkRoute).Count() <= 0)
                {
                    return string.Format("[{0}]在途程编号[{1}]中不存在", mChkRoute, mRouteTable.Rows[0][0].ToString());
                }
                if (dt_ChkRoute.AsEnumerable().Where(h => h.Field<string>("CRAFTNAME") == mRollBackRoute).Count() <= 0)
                {
                    return string.Format("[{0}]在途程编号[{1}]中不存在", mRollBackRoute, mRouteTable.Rows[0][0].ToString());
                }
            }

            return "OK";
        }

        private void panelEx1_Click(object sender, EventArgs e)
        {

        }

        private void imbt_clearSerialType_Click(object sender, EventArgs e)
        {
            Serial_Type = string.Empty;
            Frm_CheckListBox fclb = new Frm_CheckListBox(this, refWebtProduct.Instance.GetLableList().ToList());
            if (fclb.ShowDialog() == DialogResult.OK)
            {
                txt_clear_serial_type.Text = Serial_Type;
            }

        }

        private void cbx_woId_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(cbx_woId.Text) && !cbx_woId.Items.Contains(cbx_woId.Text) && !ChkSap.Checked)
            {
                MessageBoxEx.Show("请检查工单号码是否正确","工单检查提示",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                cbx_woId.Focus();
            }
        }
        private DataTable Get_Erp_woInof(string woId)
        {
            if (FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(woId, null, "WOID")).Rows.Count > 0)
            {
                throw new Exception(string.Format("此工单[{0}]已经存在,请确认...", woId));                
            }
            DataTable gWoInfodt = FrmBLL.ReleaseData.arrByteToDataTable(refWebSapConnector.Instance.Get_Z_RFC_AFPO(woId.Trim()));
                  DataTable dt = new DataTable();
                dt.Columns.Add("WOID", typeof(string));
                dt.Columns.Add("POID", typeof(string));
                dt.Columns.Add("FACTORYID", typeof(string));
                dt.Columns.Add("LOC", typeof(string));
                dt.Columns.Add("PARTNUMBER", typeof(string));
                dt.Columns.Add("QTY", typeof(string));
                dt.Columns.Add("SAPWOTYPE", typeof(string));
                dt.Columns.Add("BOMVER", typeof(string));
            if (gWoInfodt.Rows.Count > 0)
            {
          
            
                dt.Rows.Add(cbx_woId.Text.Trim(), gWoInfodt.Rows[0]["PLNUM"].ToString().Trim(), "00000", "1000", gWoInfodt.Rows[0]["MATNR"].ToString().Trim(),
                    gWoInfodt.Rows[0]["PSMNG"].ToString().Trim(),
                    gWoInfodt.Rows[0]["DAUAT"].ToString().Trim(), "00");
                //this.tb_partnumber.Text = this.gWoInfodt.Rows[0]["MATNR"].ToString().Trim();
                //this.tb_woqty.Text = this.GetIntByString(this.gWoInfodt.Rows[0]["PSMNG"].ToString().Trim()).ToString();
                //this.tb_poname.Text = this.gWoInfodt.Rows[0]["PLNUM"].ToString().Trim();
                //this.cbwotye.SelectedItem = this.GetWOtype(this.gWoInfodt.Rows[0]["DAUAT"].ToString().Trim());
                //_SapWoType = this.gWoInfodt.Rows[0]["DAUAT"].ToString().Trim();

                FrmBLL.publicfuntion.InserSystemLog((mFrm as Frm_Crate_MO).UserId, "woinfo", "SAP_" + woId, "NA");
            }
             return dt;
        }

        private void Frm_WO_Update_SizeChanged(object sender, EventArgs e)
        {
           
        }

        private void cbx_bomnumber_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(cbx_bomnumber.Text) && !cbx_bomnumber.Items.Contains(cbx_bomnumber.Text))
            {
                MessageBoxEx.Show(string.Format("请检查BOM编号[{0}]是否正确", cbx_bomnumber.Text), "BOM检查提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbx_bomnumber.Focus();
            }
        }
    }
}
