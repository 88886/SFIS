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
using System.IO;


namespace SFIS_V2
{
    public partial class Frm_ReworkPD : Office2007Form //Form
    {
        public Frm_ReworkPD(MainParent Frm,int Flag)
        {
            InitializeComponent();
            mFrm = Frm;
            sFlag = Flag;
        }
        MainParent mFrm;
        /// <summary>
        /// 0 重工作业 1 TurnOut
        /// </summary>
        int sFlag; 
      //  BLL.tWoInfo woinfo = new BLL.tWoInfo();
        BLL.tWipTracking wipTracking = new BLL.tWipTracking();
        BLL.tCraftInfo craftinfo = new BLL.tCraftInfo();
        BLL.tProduct Product = new BLL.tProduct();
        BLL.tReworkDetailInfo reworkdetail = new BLL.tReworkDetailInfo();
        BLL.tRouteInfo routeinfo = new BLL.tRouteInfo();

        /// <summary>
        /// 记录流程编号
        /// </summary>
        string My_RouteGroupId = string.Empty;

        public string Rework_WOID = string.Empty;
        public string Rework_Partnumber = string.Empty;
        public string Rework_MEMO = string.Empty;
        public string Rework_DESC = string.Empty;
        DataTable dtReasonCode = null;

        /// <summary>
        /// 调用CheckListBox标记 0站位,1 SerialType
        /// </summary>
        public int CheckListBoxFlag = 0;
        private void Frm_ReworkPD_Load(object sender, EventArgs e)
        {
            PanelScrap.Visible = false;
            panelrework.Visible = false;

            Get_Rework_No();
            if (sFlag == 0)
            {
                this.Text = "Rework";
                panelrework.Visible = true;
            }
            else
            {
                this.Text = "Turn Out";
                PanelScrap.Visible = true;
                GetReasonCode();
                GetStoreHouse();
            }

            cbx_group.SelectedIndex = 0;
        }

        private void GetReasonCode()
        {
            cbx_ReasonCode.Items.Clear();
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtReasonCode.Instance.GetReasonCode());
            if (dt.Rows.Count > 0)
            {
                dtReasonCode = FrmBLL.publicfuntion.DataTableToSort(dt, "REASONCODE");
                foreach (DataRow dr in dtReasonCode.Rows)
                {
                    cbx_ReasonCode.Items.Add(dr["REASONCODE"].ToString());
                }
            }
        }
        private void GetStoreHouse()
        {
           DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseInfo());
            foreach (DataRow dr in dt.Rows)
            {
               cbx_loc.Items.Add(dr[0].ToString());
            }
        }
        private void Get_Rework_No()
        {
            txt_UpdateReworkNo.Text = refWebtReworkDetailInfo.Instance.GetReworkNo(mFrm.gUserInfo.userId);
        }

        private void imbt_SelectwoId_Click(object sender, EventArgs e)
        {

            DataTable dt =FrmBLL.ReleaseData.arrByteToDataTable( refWebtWoInfo.Instance.GetWoInfo(null, null, "WOID")); //woinfo.GetWoInfo(null, null, "WOID").Tables[0];
            DataTable dtSort = FrmBLL.publicfuntion.DataTableToSort(dt, "WOID");
            List<string> lsWoid = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                lsWoid.Add(dr["WOID"].ToString());
            }

            Frm_CheckListBox fcl = new Frm_CheckListBox(this, lsWoid);
            fcl.ShowDialog();
        }


        private void SendMsgError(string Msg)
        {
            MessageBox.Show(Msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        private void SendMsgInfo(string Msg)
        {
            MessageBox.Show(Msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txt_woid_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_woid.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txt_woid.Text == "UNDO")
                    {
                        List_woId.Items.Clear();
                        Clear_Control();
                        SendMsgInfo("UNDO OK");
                        My_RouteGroupId = string.Empty;
                        return;
                    }

                    foreach (string str in List_woId.Items)
                    {
                        if (txt_woid.Text.Trim() == str)
                            throw new Exception(string.Format("工单:[{0}]重复", txt_woid.Text));
                    }
                    //DataTable dt =  woinfo.GetWoInfo(txt_woid.Text, null, "WOSTATE,ROUTGROUPID").Tables[0];
                    //if (dt.Rows.Count > 0 && (Convert.ToInt32(dt.Rows[0][0]) == 1 || Convert.ToInt32(dt.Rows[0][0]) == 2))
                    //{
                    //    if (string.IsNullOrEmpty(My_RouteGroupId))
                    //        My_RouteGroupId = dt.Rows[0][1].ToString();
                    //    if (My_RouteGroupId != dt.Rows[0][1].ToString())
                    //        throw new Exception(string.Format("工单[{0}]流程不同", dt.Rows[0][1].ToString()));

                    List_woId.Items.Add(txt_woid.Text.Trim());
                    //  }

                }
                catch (Exception ex)
                {
                    SendMsgError(ex.Message);
                }
                finally
                {
                    txt_woid.SelectAll();
                    txt_woid.Focus();
                }
            }
        }

        private void imbt_Import_Woid_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择文本文件";
            ofd.Filter = "(*.txt 文本文件)|*.txt";
            ofd.Multiselect = true;

            DialogResult dlr = ofd.ShowDialog();
            if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName, Encoding.UTF8);
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    txt_woid.Text = s;
                    txt_woid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    txt_woid.SelectAll();
                }
            }
        }

        private void txt_Carton_No_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Carton_No.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txt_Carton_No.Text == "UNDO")
                    {
                        List_Carton_No.Items.Clear();
                        Clear_Control();
                        SendMsgInfo("UNDO OK");
                        return;
                    }

                    foreach (string str in List_Carton_No.Items)
                    {
                        if (txt_Carton_No.Text.Trim() == str)
                            throw new Exception(string.Format("箱号:[{0}]重复", txt_Carton_No.Text));
                    }

                    //if (List_woId.Items.Count > 0)
                    //{
                    //    DataTable dt = wipTracking.Get_WIP_TRACKING("CARTONNUMBER", txt_Carton_No.Text, "DISTINCT  WOID").Tables[0];
                    //    if (dt.Rows.Count > 1)                     
                    //        throw new Exception(string.Format("箱号[{0}]存在多个工单,请确认....", txt_Carton_No.Text));                         

                    //    string woId = dt.Rows[0][0].ToString();
                    //    if (!List_woId.Items.Contains(woId))                      
                    //        throw new Exception(string.Format("箱号[{0}]工单[{1}],不在输入工单内,请确认....", txt_Carton_No.Text, woId));

                    List_Carton_No.Items.Add(txt_Carton_No.Text);

                    //}
                    //else
                    //{
                    //    SendMsgError("请先输入工单");
                    //}

                }
                catch (Exception ex)
                {
                    SendMsgError(ex.Message);
                }
                finally
                {
                    txt_Carton_No.SelectAll();
                }
            }
        }

        private void txt_pallet_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_pallet_no.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txt_pallet_no.Text == "UNDO")
                    {
                        List_Pallet_No.Items.Clear();
                        Clear_Control();
                        SendMsgInfo("UNDO OK");
                        return;
                    }

                    foreach (string str in List_Pallet_No.Items)
                    {
                        if (txt_pallet_no.Text.Trim() == str)
                            throw new Exception(string.Format("栈板:[{0}]重复", txt_pallet_no.Text));
                    }

                    //if (List_woId.Items.Count > 0)
                    //{
                    //    DataTable dt = wipTracking.Get_WIP_TRACKING("PALLETNUMBER", txt_pallet_no.Text, "DISTINCT  WOID").Tables[0];
                    //    if (dt.Rows.Count > 1)                       
                    //        throw new Exception(string.Format("栈板[{0}]存在多个工单,请确认....", txt_pallet_no.Text));


                    //    string woId = dt.Rows[0][0].ToString();
                    //    if (!List_woId.Items.Contains(woId))                       
                    //        throw new Exception(string.Format("栈板[{0}]工单[{1}],不在输入工单内,请确认....", txt_pallet_no.Text, woId));

                    List_Pallet_No.Items.Add(txt_pallet_no.Text);

                    //}
                    //else
                    //{
                    //    SendMsgError("请先输入工单");
                    //}

                }
                catch (Exception ex)
                {
                    SendMsgError(ex.Message);
                }
                finally
                {
                    txt_pallet_no.SelectAll();
                }
            }

        }

        private void imbt_Import_CartonNo_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择文本文件";
            ofd.Filter = "(*.txt 文本文件)|*.txt";
            ofd.Multiselect = true;

            DialogResult dlr = ofd.ShowDialog();
            if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName, Encoding.UTF8);
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    txt_Carton_No.Text = s;
                    txt_Carton_No_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    txt_Carton_No.SelectAll();
                }
            }
        }

        private void imbt_Import_PalletNo_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择文本文件";
            ofd.Filter = "(*.txt 文本文件)|*.txt";
            ofd.Multiselect = true;

            DialogResult dlr = ofd.ShowDialog();
            if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName, Encoding.UTF8);
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    txt_pallet_no.Text = s;
                    txt_pallet_no_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    txt_pallet_no.SelectAll();
                }
            }
        }

        private void imbt_SelectGroup_Click(object sender, EventArgs e)
        {
            CheckListBoxFlag = 0;
            List<string> LsCraft = new List<string>();
            Dictionary<string, object> mst = new Dictionary<string, object>();
            DataTable dt = craftinfo.GetAllCraftInfo(mst).Tables[0];

            DataTable temp = null;
            if (dt.Rows.Count > 0)
            {
                dt = FrmBLL.publicfuntion.DataTableToSort(dt, "CRAFTNAME");
                temp = FrmBLL.publicfuntion.getNewTable(dt, string.Format("TESTFLAG<>'{0}'", "2"));
            }
            DataTable dtLine = new DataTable();
            dtLine.Columns.Add("SECTION", typeof(string));
            dtLine.Columns.Add("GROUP", typeof(string));
            dtLine.Columns.Add("STATION", typeof(string));
            foreach (DataRow dr in temp.Rows)
            {
                LsCraft.Add(dr["CRAFTNAME"].ToString());
            }

            Frm_CheckListBox clb = new Frm_CheckListBox(this, LsCraft);
            clb.ShowDialog();
        }

        private void txt_esn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_esn.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txt_esn.Text == "UNDO")
                    {
                        List_Esn.Items.Clear();
                        Clear_Control();
                        SendMsgInfo("UNDO OK");
                        return;
                    }
                    foreach (string str in List_Esn.Items)
                    {
                        if (txt_esn.Text.Trim() == str)
                            throw new Exception(string.Format("ESN:[{0}]重复", txt_esn.Text));
                    }

                    List_Esn.Items.Add(txt_esn.Text);


                }
                catch (Exception ex)
                {
                    SendMsgError(ex.Message);
                }
                finally
                {
                    txt_esn.SelectAll();
                }
            }
        }

        private void imbt_ImportEsn_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择文本文件";
            ofd.Filter = "(*.txt 文本文件)|*.txt";
            ofd.Multiselect = true;

            DialogResult dlr = ofd.ShowDialog();
            if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName, Encoding.UTF8);
                string s;
                while ((s = sr.ReadLine()) != null)
                {

                    txt_esn.Text = s;
                    txt_esn_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    txt_esn.SelectAll();
                }
            }
        }

        private void imbt_Query_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> mst = new Dictionary<string, object>();

                List<string> My_List_woId = new List<string>();
                List<string> My_List_Carton = new List<string>();
                List<string> My_List_Pallet = new List<string>();
                List<string> My_List_Esn = new List<string>();
                List<string> My_List_Station = new List<string>();
                string My_Rework_No = string.Empty;
                string My_Fqc_No = string.Empty;

                if (List_Esn.Items.Count > 0)
                {
                    foreach (string str in List_Esn.Items)
                    {
                        My_List_Esn.Add(str);
                    }
                    mst.Add("ESN", My_List_Esn);
                }
                if (List_Carton_No.Items.Count > 0)
                {
                    foreach (string str in List_Carton_No.Items)
                    {
                        My_List_Carton.Add(str);
                    }
                    mst.Add("CARTONNUMBER", My_List_Carton);
                }
                if (List_Pallet_No.Items.Count > 0)
                {
                    foreach (string str in List_Pallet_No.Items)
                    {
                        My_List_Pallet.Add(str);
                    }

                    mst.Add("PALLETNUMBER", My_List_Pallet);
                }
                if (List_woId.Items.Count > 0)
                {
                    foreach (string str in List_woId.Items)
                    {
                        My_List_woId.Add(str);
                    }
                    mst.Add("WOID", My_List_woId);
                }
                if (List_CarftInfo.Items.Count > 0)
                {
                    foreach (string str in List_CarftInfo.Items)
                    {
                        My_List_Station.Add(str);
                    }
                    mst.Add("WIPSTATION", My_List_Station);
                }
                if (My_List_Station.Count > 0 && My_List_woId.Count == 0)
                    throw new Exception("选择站位时,必须选择工单");

                if (Chk_Rework_No.Checked && !string.IsNullOrEmpty(txt_ReworkNo.Text))
                {
                    My_Rework_No = txt_ReworkNo.Text.Trim();
                    mst.Add("REWORKNO", My_Rework_No);
                }
                if (Chk_FqcNo.Checked && !string.IsNullOrEmpty(txt_fqc_no.Text))
                {
                    My_Fqc_No = txt_fqc_no.Text;
                    mst.Add("QA_NO", My_Fqc_No);
                }

                if (mst.Count == 0)
                {
                    MessageBoxEx.Show("请输入相关信息","信息提示");
                    return;
                }
               
                DataTable dt = wipTracking.Query_WIP_Tracking(mst).Tables[0];      
                if (dt.Rows.Count == 0)
                {
                    dgv_showData.DataSource = dt;
                    dgv_Total.DataSource = DataTable_Count(dt);
                    throw new Exception("没有数据");                         

                }
                cbx_RouteName.Items.Clear();
                cbx_GroupName.Items.Clear();
                List<string> LsColnum =new List<string>();
                LsColnum.Add("ROUTGROUPID");
                DataTable dt_Route = FrmBLL.publicfuntion.DataTableDistinct(dt, LsColnum);
                if (dt_Route.Rows.Count == 1)
                {

                    cbx_RouteName.Items.Add(routeinfo.GetAttRouteDesc(dt_Route.Rows[0][0].ToString()));
                    cbx_RouteName.SelectedIndex = 0;
                    DataTable dtRouteinfo = routeinfo.Get_Route_Info(dt_Route.Rows[0][0].ToString()).Tables[0];
                    foreach (DataRow dr in dtRouteinfo.Rows)
                    {
                        cbx_GroupName.Items.Add(dr["CRAFTNAME"].ToString());
                    }

                    dgv_showData.DataSource = dt;
                    txt_total.Text = dt.Rows.Count.ToString();
                    dgv_Total.DataSource = DataTable_Count(dt);
                    LabUpdateCount.Text = txt_total.Text;
                }
                else
                {
                    SendMsgError("多个流程的产品不可同时退站");
                }

            }
            catch (Exception ex)
            {
                SendMsgError(ex.Message);
            }
            finally
            {
            }
        }

        private void Chk_Rework_No_Click(object sender, EventArgs e)
        {
           // Chk_Rework_No.Checked = !Chk_Rework_No.Checked;

            txt_ReworkNo.Enabled = Chk_Rework_No.Checked;
        }

        private void Chk_FqcNo_Click(object sender, EventArgs e)
        {
          //  Chk_FqcNo.Checked = !Chk_FqcNo.Checked;
            txt_fqc_no.Enabled = Chk_FqcNo.Checked;

        }

        private DataTable DataTable_Count(DataTable dt)
        {

            var query = from t in dt.AsEnumerable()
                        group t by new { t1 = t.Field<string>("WOID") } into m
                        select new
                        {
                            ESN = m.First().Field<string>("ESN"),
                            WOID = m.Key.t1,
                            rowcount = m.Count()
                        };
            DataTable dtGroupBy = new DataTable();
            dtGroupBy.Columns.Add("WOID", typeof(string));
            dtGroupBy.Columns.Add("COUNT", typeof(string));
            query.ToList().ForEach(q => dtGroupBy.Rows.Add(q.WOID, q.rowcount));
            return dtGroupBy;

        }

        private void imbt_keyparts_Click(object sender, EventArgs e)
        {
            CheckListBoxFlag = 1;
            List<string> ListSerial = Product.GetLableList();

            Frm_CheckListBox clb = new Frm_CheckListBox(this, ListSerial);
            clb.ShowDialog();
        }

        private void imbt_Execute_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txt_total.Text) == 0)
                {
                    MessageBoxEx.Show("没有需要退站的产品信息", "确认信息");
                    return;
                }

                if (string.IsNullOrEmpty(cbx_GroupName.Text))
                {
                    MessageBoxEx.Show("请选择退回站位", "选择站位");
                    return;
                }
                
                    Rework_WOID = dgv_showData.Rows[0].Cells["WOID"].Value.ToString();
                    Rework_Partnumber = dgv_showData.Rows[0].Cells["PARTNUMBER"].Value.ToString();
              
                FrmReworkInfo ReworkInfo = new FrmReworkInfo(this);
                if (ReworkInfo.ShowDialog() != DialogResult.OK)
                    return;


                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("WOID", Rework_WOID);
                dic.Add("PARTNUMBER", Rework_Partnumber);
                dic.Add("MEMO", Rework_MEMO);
                dic.Add("REWORKDESC", Rework_DESC);
                dic.Add("WORKDATE", DateTime.Now.ToString("yyyyMMdd").ToString());
                dic.Add("REWORKNO", txt_UpdateReworkNo.Text);
                dic.Add("TOTALQTY", Convert.ToInt32(txt_total.Text));
                dic.Add("USERID", mFrm.gUserInfo.userId);

                refWebtReworkDetailInfo.Instance.InsertReworkDetail(FrmBLL.ReleaseData.DictionaryToJson(dic));

                List<string> LsKeyParts = new List<string>();
                Dictionary<string, object> mst = new Dictionary<string, object>();
                if (ListKeyParts.Items.Count > 0)
                {
                    foreach (string str in ListKeyParts.Items)
                    {
                        LsKeyParts.Add(str);
                    }
                }
                if (LsKeyParts.Contains("SN"))
                {
                    mst.Add("SN", "NA");
                }
                if (LsKeyParts.Contains("MAC"))
                {
                    mst.Add("MAC", "NA");
                }
                if (LsKeyParts.Contains("IMEI"))
                {
                    mst.Add("IMEI", "NA");
                }

                if (chk_RemoveTray.Checked)
                {
                    mst.Add("TRAYNO", "NA");
                }
                if (chk_RemoveCarton.Checked)
                {
                    mst.Add("CARTONNUMBER", "NA");
                    mst.Add("MCARTONNUMBER", "NA");
                }
                if (chk_RemovePallet.Checked)
                {
                    mst.Add("PALLETNUMBER", "NA");
                    mst.Add("MPALLETNUMBER", "NA");
                }
                if (chk_RemoveStockNo.Checked)
                {
                    mst.Add("STORENUMBER", "NA");
                }
                if (chk_RemoveFqcNo.Checked )
                {
                    mst.Add("QA_NO", "NA");
                    mst.Add("QA_RESULT", "NA");
                }

                mst.Add("LOCSTATION", cbx_GroupName.Text);
                mst.Add("WIPSTATION", cbx_GroupName.Text);
                mst.Add("NEXTSTATION", cbx_GroupName.Text);
                mst.Add("ESN", "");
                mst.Add("REWORKNO", txt_UpdateReworkNo.Text);

                foreach (DataGridViewRow dgvr in dgv_showData.Rows)
                {
                    mst["ESN"] = dgvr.Cells["ESN"].Value.ToString();
                    string _StrErr = reworkdetail.Rework_SN(mst, LsKeyParts);
                    if (_StrErr != "OK")
                        throw new Exception(string.Format("ESN[{0}] {1}", dgvr.Cells["ESN"].Value.ToString(), _StrErr));
                    dgv_showData.FirstDisplayedScrollingRowIndex = Convert.ToInt32(txt_total.Text) - (Convert.ToInt32(LabUpdateCount.Text));//移动DataGridView光标
                    dgv_showData.Update();
                    LabUpdateCount.Text = (Convert.ToInt32(LabUpdateCount.Text)-1).ToString();
                    LabUpdateCount.Update();
                  
                }
                Clear_Control();
                MessageBox.Show("执行完成", "执行完成", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "执行异常", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void Chk_FqcNo_CheckedChanged(object sender, EventArgs e)
        {
             
        }

        private void Chk_Rework_No_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void Clear_Control()
        {
            List_woId.Items.Clear();
            List_Carton_No.Items.Clear();
            List_Pallet_No.Items.Clear();
            List_Esn.Items.Clear();
            txt_ReworkNo.Text = string.Empty;
            txt_fqc_no.Text = string.Empty;
            txt_total.Text = "0";
            LabUpdateCount.Text = "0";
            dgv_showData.DataSource = null;
            dgv_Total.DataSource = null;
            ListKeyParts.Items.Clear();
            List_CarftInfo.Items.Clear();
            cbx_GroupName.Items.Clear();
            cbx_GroupName.Text = string.Empty;
            cbx_RouteName.Items.Clear();
            cbx_RouteName.Text = string.Empty;
            chk_RemoveCarton.Checked = false;
            chk_RemoveFqcNo.Checked = false;
            chk_RemovePallet.Checked = false;
            chk_RemoveStockNo.Checked = false;
            chk_RemoveTray.Checked = false;
            txt_woid.Text = string.Empty;
            txt_Carton_No.Text = string.Empty;
            txt_pallet_no.Text = string.Empty;
            txt_esn.Text = string.Empty;
            if (sFlag==0)
            Get_Rework_No();

        }

        private void imbt_GetReowrkNo_Click(object sender, EventArgs e)
        {
            Get_Rework_No();
        }

        private void cbx_ReasonCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = FrmBLL.publicfuntion.getNewTable(dtReasonCode, string.Format("REASONCODE='{0}'", cbx_ReasonCode.Text));
                txt_reasondesc1.Text = dt.Rows[0]["REASONDESC"].ToString();
                txt_reasondesc2.Text = dt.Rows[0]["REASONDESC2"].ToString();
            }
            catch
            {
                txt_reasondesc1.Text = null;
                txt_reasondesc2.Text = null;
                MessageBox.Show("原因代码异常");
            }
        }

        private void imbt_Confirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cbx_group.Text))
                    throw new Exception("请选择制程段");
                if (string.IsNullOrEmpty(cbx_ReasonCode.Text))
                    throw new Exception("请选择原因代码");
                if (string.IsNullOrEmpty(cbx_loc.Text))
                    throw new Exception("请选择库位");
                string Scrap_No = refWebProcedure.Instance.GetStockInNumber();
                Dictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("LOCSTATION",cbx_group.Text);
                mst.Add("STATIONNAME", cbx_group.Text+"1");
                mst.Add("WIPSTATION", cbx_loc.Text);
                mst.Add("NEXTSTATION","NA");
                mst.Add("STORENUMBER", Scrap_No);
                mst.Add("USERID", mFrm.gUserInfo.userId);
                mst.Add("SCRAPFLAG", "1");
                mst.Add("ESN", "");
                Dictionary<string, object> mst_scrap = new Dictionary<string, object>();
                mst_scrap.Add("SCRAP_NO", Scrap_No);
                mst_scrap.Add("USERID", mFrm.gUserInfo.userId);         
                mst_scrap.Add("WOID", "");
                mst_scrap.Add("PARTNUMBER", "");
                mst_scrap.Add("PRODUCTNAME", "");
                mst_scrap.Add("VERSION_CODE", "");
                mst_scrap.Add("ESN", "");
                mst_scrap.Add("LINEID", cbx_group.Text);
                mst_scrap.Add("SECTION_NAME", "");
                mst_scrap.Add("LOCSTATION", "");
                mst_scrap.Add("STATION_NAME", "");
                mst_scrap.Add("REMARK", "NA");
                mst_scrap.Add("SCRAP_REASON",txt_reasondesc1.Text);
                mst_scrap.Add("SCRAP_FLAG", "1");
                mst_scrap.Add("SCRAP_KIND",radTurnOut.Checked?"0":"1");
                mst_scrap.Add("REASON_CODE", cbx_ReasonCode.Text);
                mst_scrap.Add("REASON_TYPE", "NA");
                mst_scrap.Add("DUTY_STATION", "NA");
                mst_scrap.Add("QTY", 1);
                mst_scrap.Add("GD", "NA");
                mst_scrap.Add("LOC", cbx_loc.Text);
                mst_scrap.Add("PRE_SCRAP_GROUP_NAME", cbx_group.Text);
                mst_scrap.Add("ITHT", "Y");
                foreach (DataGridViewRow dgvr in dgv_showData.Rows)
                {
                    mst["ESN"] = dgvr.Cells["ESN"].Value.ToString();
                    mst_scrap["WOID"] = dgvr.Cells["WOID"].Value.ToString();
                    mst_scrap["PARTNUMBER"] = dgvr.Cells["PARTNUMBER"].Value.ToString();
                    mst_scrap["PRODUCTNAME"] = dgvr.Cells["PRODUCTNAME"].Value.ToString();
                    mst_scrap["VERSION_CODE"] = dgvr.Cells["VERSIONCODE"].Value.ToString();
                    mst_scrap["ESN"] = dgvr.Cells["ESN"].Value.ToString();
                    mst_scrap["SECTION_NAME"] = dgvr.Cells["SECTIONNAME"].Value.ToString();
                    mst_scrap["LOCSTATION"] = dgvr.Cells["LOCSTATION"].Value.ToString();
                    mst_scrap["STATION_NAME"] = dgvr.Cells["STATIONNAME"].Value.ToString();             
                    string _StrErr = reworkdetail.Scrap_SN(mst, mst_scrap);
                    if (_StrErr != "OK")
                        throw new Exception(string.Format("ESN[{0}] {1}", dgvr.Cells["ESN"].Value.ToString(), _StrErr));
                    dgv_showData.FirstDisplayedScrollingRowIndex = Convert.ToInt32(txt_total.Text) - (Convert.ToInt32(LabUpdateCount.Text));//移动DataGridView光标
                    dgv_showData.Update();
                    LabUpdateCount.Text = (Convert.ToInt32(LabUpdateCount.Text) - 1).ToString();
                    LabUpdateCount.Update();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "发生异常", MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            
        }
        
    }

}
