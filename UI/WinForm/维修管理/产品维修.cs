using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.Reflection;
using System.Threading;

namespace SFIS_V2
{
    public partial class FrmRepair : Office2007Form
    {
        public FrmRepair(MainParent sInfo, int Flag)
        {
            InitializeComponent();
            sMain = sInfo;
            sFlag = Flag;
        }

        MainParent sMain;
        int sFlag;
        DataTable dtWip = null;
       // string Rowid = "";
        string rowidToRepair = "";
        string rowidToLine = "";
        string CraftIdToLine = "";
        string EsnToLine = "";
        string ToScrap = "";
        /// <summary>
        /// 选择的产品系列
        /// </summary>
        public DataTable dtProduct = null;

        private void FrmRepair_Load(object sender, EventArgs e)
        {
            try
            {
                #region 添加应用程序
                if (this.sMain.gUserInfo.rolecaption == "系统开发员")
                {
                    IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                    IDictionary<string, object> funls = new Dictionary<string, object>();
                    funls = new Dictionary<string, object>();
                    funls.Add("FUNID", "PD_Transfer".ToUpper());
                    funls.Add("PROGID", this.Name);
                    funls.Add("FUNNAME", "生产转账");
                    funls.Add("FUNDESC", "生产转账");
                    lsfunls.Add(funls); 

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("PROGID", this.Name);
                    dic.Add("PROGNAME", this.Text);
                    dic.Add("PROGDESC", this.Text);                

                    FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                    FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
                }
                #endregion

            }
            catch (Exception ex)
            {
                this.sMain.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

          

            tbrepairuser.Text = sMain.gUserInfo.userId;

            RCList = new GetRCList(GetReasonCode);
            RCList.BeginInvoke(null, null);
            CraftInfo = new GetCraftInfo(GetAllCraftInfo);
            CraftInfo.BeginInvoke(null, null);
            GetDutyData = new GetDuty(GetDutyInfo);
            GetDutyData.BeginInvoke(null,null);

            #region 单元格交替颜色
            this.dgvallrepairinfo.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvallrepairinfo.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            this.dgvRepairReport.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvRepairReport.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            this.dgvQrepair.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvQrepair.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            #endregion

            tabSelect.SelectedIndex = 3;


            //for (int i = 0; i < 24; i++)
            //{
            //    combStime.Items.Add(i.ToString().PadLeft(2, '0') + ":30");
            //    combEtime.Items.Add(i.ToString().PadLeft(2, '0') + ":30");
            //}
            CB_Class.Items.Clear();
            CB_Class.Items.Add("ALL");
            CB_Class.Items.Add("D");
            CB_Class.Items.Add("N");
            CB_Class.SelectedIndex = 0;

            GetColnumName();
        
            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            ass.ExecuteOracleCommand("delete from tProduct");
        }

        private void GettProductList()
        {
            //progressBarX1.Invoke(new EventHandler(delegate
            //{
            //      FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            //     DataTable dtProduct=  ass.GetDatatable("select * from tProduct");
            //     if (dtProduct.Rows.Count <= 0)
            //     {
            //         DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetAllProduct());

            //         progressBarX1.Maximum = dt.Rows.Count;
            //         int count = 0;
            //         int rowcount = dt.Rows.Count;
            //         foreach (DataRow dr in dt.Rows)
            //         {                    
            //             string sql = string.Format("insert into tProduct(partnumber,sortname,productname,productcolor,productdesc,other) values('{0}','{1}','{2}','{3}','{4}','{5}')",
            //              dr[0].ToString(),
            //              dr[1].ToString(),
            //              dr[2].ToString(),
            //              dr[3].ToString(),
            //              dr[4].ToString(),
            //              dr[5].ToString());
            //             ass.ExecuteOracleCommand(sql);
            //             count++;
            //             progressBarX1.Value = count;
            //             progressBarX1.Text = count.ToString() + "/" + dt.Rows.Count.ToString();
            //         }
            //     }

            //}));
           
           
        }

        /// <summary>
        /// 获取所有产品料号
        /// </summary>
        private delegate void GetProductList();
        GetProductList GetProduct;

        private delegate void GetRCList();
        GetRCList RCList;
        private delegate void GetCraftInfo();
        GetCraftInfo CraftInfo;

        private delegate void GetDuty();
        GetDuty GetDutyData;


        DataTable dtrc = null;
        private void GetReasonCode()
        {
            cbreasoncode.Invoke(new EventHandler(delegate
            {
                dtrc = FrmBLL.ReleaseData.arrByteToDataTable(refWebtReasonCode.Instance.GetReasonCode());
                foreach (DataRow dr in dtrc.Rows)
                {
                    if (dr["ReasonCode"].ToString()!="NA")
                    cbreasoncode.Items.Add(dr["ReasonCode"].ToString());
                }
            }));
        }
        DataTable dtCraft = null;
        private void GetAllCraftInfo()
        {
            dtCraft = FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo());
        }

        DataTable dtDuty = null;
        private void GetDutyInfo()
        {
            cb_duty.Invoke(new EventHandler(delegate
                {
                    dtDuty = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetDutyInfo());
                    cb_duty.Items.Clear();
                    foreach (DataRow dr in dtDuty.Rows)
                    {
                        if (dr[0].ToString() != "NA")
                        {
                            cb_duty.Items.Add(dr[0].ToString());
                        }
                    }
                }));
        }

        /// <summary>
        /// 刷新责任单位
        /// </summary>
        public void RefreshDutyInfo()
        {
            GetDutyData = new GetDuty(GetDutyInfo);
            GetDutyData.BeginInvoke(null, null);
        }
        private void tabSelect_Click(object sender, EventArgs e)
        {

        }

        private void tabInOrOut_Click(object sender, EventArgs e)
        {


        }

        private void tabSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tabSelect.SelectedIndex)
            {
                case 0:

                    tbesn.Focus();
                    tbesn.SelectAll();
                    if ((sMain.gUserInfo.rolecaption != "产品维修") && (sMain.gUserInfo.rolecaption != "系统开发员"))
                    {
                        MessageBox.Show(" 您当前的角色为: " + sMain.gUserInfo.rolecaption + "\r\n 非维修人员不能使用该页面功能 \r\n 自动跳转为查询界面");
                        tabSelect.SelectedIndex = 3;
                    }

                    break;
                case 1:
                    tbsntorepair.Focus();
                    tbsntorepair.SelectAll();
                    btToRepair.Enabled = false;
                    if ((sMain.gUserInfo.rolecaption != "产品维修") && (sMain.gUserInfo.rolecaption != "系统开发员"))
                    {
                        MessageBox.Show(" 您当前的角色为: " + sMain.gUserInfo.rolecaption + "\r\n 非维修人员不能使用该页面功能 \r\n 自动跳转为查询界面");
                        tabSelect.SelectedIndex = 3;
                    }
                    break;

                case 2:

                      DataRow[] ArrDr = sMain.gUserInfo.userPopList.Select(string.Format("progid='FrmRepair' and funId='{0}'", "PD_Transfer".ToUpper()));
                    if ((sMain.gUserInfo.rolecaption != "生产转账") && (sMain.gUserInfo.rolecaption != "系统开发员")&&  (ArrDr == null || ArrDr.Length < 1))
                    {
                     
                        MessageBox.Show(" 您当前的角色为: " + sMain.gUserInfo.rolecaption + "\r\n 非生产转账人员不能使用该页面功能 \r\n 自动跳转为查询界面");
                        tabSelect.SelectedIndex = 3;
                    }
                    tbRepairToLine.Focus();
                    tbRepairToLine.SelectAll();

                    break;

                case 3:

                    tbesnselect.Focus();
                    tbesnselect.SelectAll();

                    break;
                case 5:
                    GetProduct = new GetProductList(GettProductList);
                    GetProduct.BeginInvoke(null, null);

                    //// ThreadGetProduct = new Thread(new ThreadStart(GettProductList));

                    //ThreadGetProduct = new Thread((ThreadStart)(delegate()
                    //{
                    //GettProductList();
                    //}));



                    //ThreadGetProduct.Start();


                    break;
            }
        }

        private void tbesn_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbesn.Text)) && (e.KeyCode == Keys.Enter))
            {
                PalRepairInof.Visible = false;
                ClearPalRepairInfo();
                ClearTextBox();

                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetRepairSnInfo(tbesn.Text.Trim()));
                UpdateStatusToChinese(dt);
                dgvrepair.DataSource = dt;
                lberrorcount.Text = "NG COUNT:  " + dt.Rows.Count.ToString();
                #region 显示不良点数
                dgverrlistcount.DataSource = null;
                dgverrlistcount.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetFailErrListCount(tbesn.Text.Trim()));
                #endregion
                tbesn.Focus();
                tbesn.SelectAll();

                //DataTable dtRepair = FrmBLL.publicfuntion.getNewTable(dt,"status='1'");
                // UpdateStatusToChinese(dtRepair);
                //dgvrepair.DataSource = dtRepair;
                //if (dtRepair.Rows.Count <= 0)
                //{
                //    sMain.ShowPrgMsg("该在不在维修状态,不可进行维修作业",MainParent.MsgType.Error);
                //    tbesn.Focus();
                //    tbesn.SelectAll();
                //}
                //else
                //{
                //#region 显示不良点数
                //dgverrlistcount.DataSource = null;
                //dgverrlistcount.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetFailErrListCount(tbesn.Text.Trim()));
                //#endregion
                //dgverrlistcount.DataSource = null;
                //dgverrlistcount.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetFailErrListCount(tbesn.Text.Trim()));

                //tbsn.Text = dtRepair.Rows[0]["esn"].ToString();
                //tbwoid.Text = dtRepair.Rows[0]["woId"].ToString();
                //tbpartnumber.Text = dtRepair.Rows[0]["partnumber"].ToString();
                //tbroute.Text = dtRepair.Rows[0]["craftname"].ToString();
                //tbInUser.Text = dtRepair.Rows[0]["inputuser"].ToString();
                //tbtime.Text = dtRepair.Rows[0]["inputdate"].ToString();
                //tbline.Text = dtRepair.Rows[0]["lineid"].ToString();
                //tberrorcode.Text = dtRepair.Rows[0]["errorcode"].ToString();
                //tberrordesc.Text = refWebtErrorCode.Instance.GetErrorCodeDesc(dtRepair.Rows[0]["errorcode"].ToString())[0];
                //tbesn.Focus();
                //tbesn.SelectAll();
                //dtWip =FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWip("esn",tbesn.Text.Trim()));
                //tbrestation.Text = FrmBLL.publicfuntion.getNewTable(dtCraft, string.Format("craftid='{0}'", dtWip.Rows[0]["wipstation"].ToString())).Rows[0][1].ToString();
                //}

            }
        }

        private void tbsntorepair_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbsntorepair.Text)) && (e.KeyCode == Keys.Enter))
            {

                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetRepairSnInfo(tbsntorepair.Text.Trim()));
                DataTable dtRepair = FrmBLL.publicfuntion.getNewTable(dt, "status='0'");
                UpdateStatusToChinese(dtRepair);
                dgvtorepair.DataSource = dtRepair;
                if (dtRepair.Rows.Count == 0)
                {
                    sMain.ShowPrgMsg("生产线未转维修", MainParent.MsgType.Error);
                    btToRepair.Enabled = false;

                }
                else
                {
                    rowidToRepair = dtRepair.Rows[0]["ID"].ToString();
                    btToRepair.Enabled = true;
                    ToScrap = "0";
                    //if (FrmBLL.publicfuntion.getNewTable(dt, string.Format("Craftname='{0}'", dtRepair.Rows[0]["Craftname"].ToString())).Rows.Count >= 3)
                    //{
                    //    ToScrap = "1";
                    //    MessageBox.Show("大于3次维修,将直接转入不可维修状态");
                    //}
                    //else
                    //{
                   //    ToScrap = "0";
                    //}  暂时不启用维修三次报废
                }

                UpdateStatusToChinese(dt);
                dgvshowallstatus.DataSource = dt;
                tbsntorepair.Focus();
                tbsntorepair.SelectAll();



                //  DataTable dtCratCount = new DataTable("mdt");
                //  dtCratCount.Columns.Add("途程", System.Type.GetType("System.String"));
                //  dtCratCount.Columns.Add("计数", System.Type.GetType("System.String"));
                ////  dtCraft.Rows.Add(dt.Rows[0]["CraftId"].ToString(),1);
                // DataTable dd= dt.DefaultView.ToTable(true, "Craftname");
                //  for (int y = 0; y < dd.Rows.Count; y++)
                //  {
                //      dtCratCount.Rows.Add(dd.Rows[y][0].ToString(),"0");
                //  }

                //  for (int x = 0; x < dt.Rows.Count; x++)
                //  {
                //      for (int z = 0; z < dtCratCount.Rows.Count; z++)
                //      {
                //          if (dt.Rows[x][6].ToString() == dtCratCount.Rows[z][0].ToString())
                //          {
                //              dtCratCount.Rows[z][1] = (Convert.ToInt32(dtCratCount.Rows[z][1].ToString()) + 1).ToString();
                //          }
                //      }
                //  }

            }
        }

        private void btToRepair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要将此产品转入到维修吗", "转入提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (ToScrap == "0")
                {
                    refWebRepairInfo.Instance.UpdateRepairSnStatus("1", rowidToRepair);
                }
                if (ToScrap == "1")
                {
                    refWebRepairInfo.Instance.UpdateRepairSnStatus("4", rowidToRepair);
                }
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetRepairSnInfo(tbsntorepair.Text.Trim()));
                DataTable dtRepair = FrmBLL.publicfuntion.getNewTable(dt, "status='0'");
                UpdateStatusToChinese(dtRepair);
                dgvtorepair.DataSource = dtRepair;
                btToRepair.Enabled = false;
                //  tbsntorepair_KeyDown(null, new KeyEventArgs(Keys.Enter));
                sMain.ShowPrgMsg("转入维修成功", MainParent.MsgType.Incoming);
                tbsntorepair.Focus();
                tbsntorepair.SelectAll();

            }
        }

        private void UpdateStatusToChinese(DataTable dtStatus)
        {
            for (int i = 0; i < dtStatus.Rows.Count; i++)
            {
                if (dtStatus.Rows[i]["status"].ToString() == "0")
                {
                    dtStatus.Rows[i]["status"] = "待转维修";
                }
                else
                    if (dtStatus.Rows[i]["status"].ToString() == "1")
                    {
                        dtStatus.Rows[i]["status"] = "待维修";
                    }
                    else
                        if (dtStatus.Rows[i]["status"].ToString() == "2")
                        {
                            dtStatus.Rows[i]["status"] = "维修已完成";
                        }
                        else
                            if (dtStatus.Rows[i]["status"].ToString() == "3")
                            {
                                dtStatus.Rows[i]["status"] = "已转入产线";
                            }
                            else
                                if (dtStatus.Rows[i]["status"].ToString() == "4")
                                {
                                    dtStatus.Rows[i]["status"] = "不可维修";
                                }

            }
        }

        private void cbreasoncode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tbrcdesc.Text = FrmBLL.publicfuntion.getNewTable(dtrc, string.Format("reasoncode='{0}'", cbreasoncode.Text)).Rows[0]["reasondesc"].ToString();
            try
            {
                tbrcdesc.Text = FrmBLL.publicfuntion.getNewTable(dtrc, string.Format("reasoncode='{0}'", cbreasoncode.Text)).Rows[0]["reasondesc"].ToString();
            }
            catch
            {
                MessageBox.Show("原因代码未找到,请重新选择");
                cbreasoncode.Focus();
                cbreasoncode.SelectAll();
            }
        }

        private void tbrcdesc_TextChanged(object sender, EventArgs e)
        {


        }
        private void ClearTextBox()
        {
            tbsn.Text = "";
            tbwoid.Text = "";
            tbpartnumber.Text = "";
            tbroute.Text = "";
            tbInUser.Text = "";
            tbtime.Text = "";
            tbline.Text = "";
            tberrorcode.Text = "";
            tberrordesc.Text = "";
            tbrestation.Text = "";         
            //Rowid = "";
        }
        private void dgvrepair_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string _StrErr = string.Empty;
                try
                {
                    if (dgvrepair.Rows[e.RowIndex].Cells[9].Value.ToString() != "待维修")
                    {
                        sMain.ShowPrgMsg("请双击 黄色【待维修】行", MainParent.MsgType.Error);
                        MessageBox.Show("请双击 黄色【待维修】行");
                        ClearTextBox();
                        PalRepairInof.Visible = false;
                        return;
                    }
                    PalRepairInof.Visible = true;
                    ClearPalRepairInfo();
                 //   Rowid = dgvrepair.Rows[e.RowIndex].Cells[1].Value.ToString();
                    tbsn.Text = dgvrepair.Rows[e.RowIndex].Cells[4].Value.ToString(); //唯一号码
                    tbwoid.Text = dgvrepair.Rows[e.RowIndex].Cells[5].Value.ToString(); //工单
                    tbpartnumber.Text = dgvrepair.Rows[e.RowIndex].Cells[6].Value.ToString(); //产品料号
                    tbroute.Text = dgvrepair.Rows[e.RowIndex].Cells[7].Value.ToString();//途程
                    tbInUser.Text = dgvrepair.Rows[e.RowIndex].Cells[8].Value.ToString(); //转入人员
                    tbtime.Text = dgvrepair.Rows[e.RowIndex].Cells[12].Value.ToString();//转入时间
                    tbline.Text = dgvrepair.Rows[e.RowIndex].Cells[14].Value.ToString();//线别
                    tberrorcode.Text = dgvrepair.Rows[e.RowIndex].Cells[2].Value.ToString();//不良代码     
                    _StrErr = "不良代码异常,请检查";
                    tberrordesc.Text = refWebtErrorCode.Instance.GetErrorCodeDesc(dgvrepair.Rows[e.RowIndex].Cells[2].Value.ToString())[0];
                    tbesn.Focus();
                    tbesn.SelectAll();
                    _StrErr = "获取回流途程异常";
                    dtWip = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetWipAndRouteData(tbesn.Text.Trim()));
                    tbrestation.Text = dtWip.Rows[0][1].ToString();
                 
                 
                    cboutroute.Items.Clear();
                    dicRoute = new DataTable("mydt");
                  //  dicRoute.Columns.Add("GroupName", "CraftId");
                    dicRoute.Columns.Add("GroupName", System.Type.GetType("System.String"));
                    dicRoute.Columns.Add("CraftId", System.Type.GetType("System.String"));
                    foreach (DataRow dr in dtWip.Rows)
                    {
                        string CraftId = dr[1].ToString();
                        if (CraftId != "NA")
                        {
                            string GroupName = CraftId;
                            dicRoute.Rows.Add(GroupName, CraftId);
                            cboutroute.Items.Add(GroupName);                            
                        }
                    }
                    if (dicRoute.Rows.Count == 0)
                    {
                        throw new Exception("获取维修途程失败"); 
                    }
                    cboutroute.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(" 维修资料异常,请联系SFC人员  \r\n 信息提示: " + ex.Message + " \r\n" + _StrErr);
                    PalRepairInof.Visible = false;
                }
            }
        }
      //  Dictionary<string, string> dicRoute = new Dictionary<string, string>();
        DataTable dicRoute = null;
        private void dgvrepair_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }

        private void dgvrepair_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow dgr = dgvrepair.Rows[e.RowIndex];
            if (dgr.Cells["状态"].Value.ToString() == "待维修")
            {
                //using (SolidBrush soldbrush = new SolidBrush(Color.FromArgb(255, 255, 0)))
                //{
                //    e.Graphics.FillRectangle(soldbrush, e.RowBounds);
                //    e.PaintCellsContent(e.ClipBounds);
                //    e.Handled = true;
                //}
                dgvrepair.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;

            }
            else
                if (dgr.Cells["状态"].Value.ToString() == "不可维修")
                {
                    //using (SolidBrush soldbrush = new SolidBrush(Color.FromArgb(255, 0, 0)))
                    //{
                    //    e.Graphics.FillRectangle(soldbrush, e.RowBounds);
                    //    e.PaintCellsContent(e.ClipBounds);
                    //    e.Handled = true;
                    //}
                    dgvrepair.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 0, 0);

                }
                else
                {
                    //using (SolidBrush soldbrush = new SolidBrush(Color.FromArgb(0, 128, 0)))
                    //{
                    //    e.Graphics.FillRectangle(soldbrush, e.RowBounds);
                    //    e.PaintCellsContent(e.ClipBounds);
                    //    e.Handled = true;
                    //}
                    dgvrepair.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(0, 128, 0);

                }
        }

        private void cboutroute_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbtrsn_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbtrsn.Text)) && (e.KeyCode == Keys.Enter))
            {
                DataTable dttrsn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.QueryTrSn(tbtrsn.Text.Trim()));
                if (dttrsn.Rows.Count != 0)
                {
                    tbpn.Text = dttrsn.Rows[0][1].ToString();
                    tbvendercode.Text = dttrsn.Rows[0][2].ToString();
                    tbdatecode.Text = dttrsn.Rows[0][3].ToString();
                    tblotid.Text = dttrsn.Rows[0][4].ToString();
                }
                else
                {
                    MessageBox.Show("物料条码错误!!!");
                    tbtrsn.Focus();
                    tbtrsn.SelectAll();
                }

            }
        }

        private void btUpdateRepairInfo_Click(object sender, EventArgs e)
        {
            ShowErrMessage("功能停用");
            return;

            if ((string.IsNullOrEmpty(cbreasoncode.Text)) || (cbreasoncode.Text.ToUpper() == "NA"))
            {
                ShowErrMessage("请选择维修原因代码");
                return;
            }
            if (string.IsNullOrEmpty(tblocation.Text))
            {
                ShowErrMessage("未填写零件位置,请输入维修零件位置");
                return;
            }
            if (string.IsNullOrEmpty(cboutroute.Text))
            {
                ShowErrMessage("为选择维修转出途程,请选择维修转出途程");
                return;
            }
            if (string.IsNullOrEmpty(cb_duty.Text))
            {
                ShowErrMessage("责任单位选择为空");
                return;
            }

         //   bool chkreson = false;
            //for (int i = 0; i < cbreasoncode.Items.Count; i++)
            //{
            //    if (cbreasoncode.Text == cbreasoncode.Items[i].ToString())
            //    {
                 //   chkreson = true;

            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("ESN",tbesn.Text.ToUpper());
            //  dic.Add("REASONCODE",cbreasoncode.Text.ToUpper());
            //  dic.Add("LOCATION",tblocation.Text.ToUpper());
            //  dic.Add("REUSER",sMain.gUserInfo.userId.ToUpper());
            //  dic.Add("REMARK",tbmemo.Text.ToUpper());
            //  dic.Add("KPNO", tbpn.Text.ToUpper());
            //  dic.Add("VENDERCODE", tbvendercode.Text.ToUpper());
            //  dic.Add("DATECODE", tbdatecode.Text.ToUpper());
            //  dic.Add("LOTCODE", tblotid.Text.ToUpper());
            //  dic.Add("OUTCRAFTID", FrmBLL.publicfuntion.getNewTable(dicRoute, string.Format("GroupName='{0}'", cboutroute.Text)).Rows[0][1].ToString());
            //  dic.Add("DUTY", cb_duty.Text.ToUpper());
            //  refWebRepairInfo.Instance.UpdateRepairInformationInsertMaterial(FrmBLL.ReleaseData.DictionaryToJson(dic), true);
                    //refWebRepairInfo.Instance.UpdateRepairInformationInsertMaterial(new WebServices.tRepairInfo.tRepairInfoTable()
                    //{
                    //    ESN = tbesn.Text.ToUpper(),
                    //    ReasonCode = cbreasoncode.Text.ToUpper(),
                    //    Location = tblocation.Text.ToUpper(),
                    //    ReUser = sMain.gUserInfo.userId.ToUpper(),
                    //    Remark = tbmemo.Text.ToUpper(),
                    //    PartNo = tbpn.Text.ToUpper(),
                    //    VenderCode = tbvendercode.Text.ToUpper(),
                    //    DateCode = tbdatecode.Text.ToUpper(),
                    //    LotCode = tblotid.Text.ToUpper(),
                    //    OutcraftId = FrmBLL.publicfuntion.getNewTable(dicRoute, string.Format("GroupName='{0}'", cboutroute.Text)).Rows[0][1].ToString(),  //dicRoute[cboutroute.Text]
                    //    Duty = cb_duty.Text.ToUpper()
                    //});
                    //tbesn_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    //PalRepairInof.Visible = false;
                    //ClearPalRepairInfo();
                    //ClearTextBox();
                    //sMain.ShowPrgMsg("维修完成", MainParent.MsgType.Incoming);
            //    }
            //}
            //if (!chkreson)
            //{
            //    MessageBox.Show("原因代码不存在,请确认......");
            //}
         
        }

        private void ShowErrMessage(string str)
        {
            sMain.ShowPrgMsg(str, MainParent.MsgType.Error);
            MessageBox.Show(str, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 清除修复所填写的信息   
        /// </summary>
        private void ClearPalRepairInfo()
        {
            tblocation.Text = "";
            tbrcdesc.Text = "";
            tbmemo.Text = "";
            tbtrsn.Text = "";
            tbpn.Text = "";
            tbvendercode.Text = "";
            tbdatecode.Text = "";
            tblotid.Text = "";

        }


        private void dgvtorepair_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                rowidToRepair = dgvtorepair.Rows[e.RowIndex].Cells["esn"].Value.ToString();
            }
        }

        private void tbRepairToLine_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbRepairToLine.Text)) && (e.KeyCode == Keys.Enter))
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetRepairSnInfo(tbRepairToLine.Text.Trim()));
                if (FrmBLL.publicfuntion.getNewTable(dt, "status='1'").Rows.Count > 0 || FrmBLL.publicfuntion.getNewTable(dt, "status='0'").Rows.Count > 0)
                {
                    sMain.ShowPrgMsg("此产品还有待维修记录,请维修完成后转出", MainParent.MsgType.Error);
                    btToLine.Enabled = false;

                }
                else
                {

                    DataTable dtLine = FrmBLL.publicfuntion.getNewTable(dt, "status='2'");
                    UpdateStatusToChinese(dtLine);
                    dgvtoLineAllStatus.DataSource = dtLine;
                    if (dgvtoLineAllStatus.Rows.Count == 0)
                    {
                        sMain.ShowPrgMsg("修复未转出", MainParent.MsgType.Error);
                        btToLine.Enabled = false;
                    }
                    else
                    {
                        
                         //   rowidToLine = dtLine.Rows[0]["ID"].ToString();    // dgvtoLineAllStatus.Rows[0].Cells[1].Value.ToString();
                         //   CraftIdToLine = dtLine.Rows[0]["OUTCRAFTID"].ToString();//dgvtoLineAllStatus.Rows[0].Cells["OUTCRAFTID"].Value.ToString();
                         //   EsnToLine = dtLine.Rows[0]["ESN"].ToString();//dgvtoLineAllStatus.Rows[0].Cells["ESN"].Value.ToString();
                         
                        btToLine.Enabled = true;
                    }
                }

                UpdateStatusToChinese(dt);
                dgvshowLinestatus.DataSource = dt;
                tbRepairToLine.Focus();
                tbRepairToLine.SelectAll();

            }
        }

        private void btToLine_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要将此产品转入到生产线吗？", "转入产线提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                // refWebRepairInfo.Instance.UpdateRepairSnStatus("3", rowidToLine);
                string ErrMsg = string.Empty;
                foreach (DataGridViewRow dgvr in dgvtoLineAllStatus.Rows)
                {                     
                    CraftIdToLine = dgvr.Cells[17].Value.ToString();
                    EsnToLine = dgvr.Cells[4].Value.ToString();
                    rowidToLine = dgvr.Cells[1].Value.ToString();
                    ErrMsg = refWebRepairInfo.Instance.UpdateRepairToWip(CraftIdToLine, EsnToLine, "3", rowidToLine);
                }
                if (ErrMsg == "OK")
                {
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetRepairSnInfo(tbRepairToLine.Text.Trim()));
                    DataTable dtLine = FrmBLL.publicfuntion.getNewTable(dt, "status='2'");
                    UpdateStatusToChinese(dtLine);
                    dgvtoLineAllStatus.DataSource = dtLine;
                    btToLine.Enabled = false;

                    //  MessageBox.Show("转入生产线成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sMain.ShowPrgMsg("转入生产线成功", MainParent.MsgType.Incoming);
                    tbRepairToLine.Focus();
                    tbRepairToLine.SelectAll();
                }
                else
                {
                    MessageBox.Show(string.Format("转入生产线失败->{0}", ErrMsg), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbRepairToLine.Focus();
                    tbRepairToLine.SelectAll();
                }

            }
        }

        private void dgvToLine_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                rowidToLine = dgvtoLineAllStatus.Rows[e.RowIndex].Cells[1].Value.ToString();
                CraftIdToLine = dgvtoLineAllStatus.Rows[e.RowIndex].Cells[17].Value.ToString();
                EsnToLine = dgvtoLineAllStatus.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        private void tbesnselect_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbesnselect.Text)) && (e.KeyCode == Keys.Enter))
            {
                DataTable dt1 = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetRepairSnInfo(tbesnselect.Text.Trim()));
               // DataTable dt2 = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetRepairMaterialInfo(tbesnselect.Text.Trim()));
               // DataTable dtall = UniteDataTable(dt1, dt2, "Repair");
                UpdateStatusToChinese(dt1);
                dgvallrepairinfo.DataSource = dt1;
                tbesnselect.SelectAll();
                sMain.ShowPrgMsg("查询完成", MainParent.MsgType.Incoming);
            }
        }

        //两个结构不同的DT合并
        /// <summary>
        /// 将两个列不同的DataTable合并成一个新的DataTable
        /// </summary>
        /// <param name="dt1">表1</param>
        /// <param name="dt2">表2</param>
        /// <param name="DTName">合并后新的表名</param>
        /// <returns></returns>
        private DataTable UniteDataTable(DataTable dt1, DataTable dt2, string DTName)
        {
            DataTable dt3 = dt1.Clone();
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                dt3.Columns.Add(dt2.Columns[i].ColumnName);
            }
            object[] obj = new object[dt3.Columns.Count];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dt1.Rows[i].ItemArray.CopyTo(obj, 0);
                dt3.Rows.Add(obj);
            }

            if (dt1.Rows.Count >= dt2.Rows.Count)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    for (int x = 0; x < dt1.Rows.Count; x++)  ///zz
                    {
                        if (dt1.Rows[x][0].ToString() == dt2.Rows[i][0].ToString())//zz
                        {
                            for (int j = 0; j < dt2.Columns.Count; j++)
                            {
                                dt3.Rows[x][j + dt1.Columns.Count] = dt2.Rows[i][j].ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                DataRow dr3;
                for (int i = 0; i < dt2.Rows.Count - dt1.Rows.Count; i++)
                {
                    dr3 = dt3.NewRow();
                    dt3.Rows.Add(dr3);
                }
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    for (int j = 0; j < dt2.Columns.Count; j++)
                    {
                        dt3.Rows[i][j + dt1.Columns.Count] = dt2.Rows[i][j].ToString();
                    }
                }
            }
            dt3.TableName = DTName; //设置DT的名字
            return dt3;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void imbt_Toexcel_Click(object sender, EventArgs e)
        {
            FrmBLL.DataGridViewToExcel.DataToExcel(dgvallrepairinfo);
        }

        private void imbt_QryRepair_Click(object sender, EventArgs e)
        {
            if (cmbstatus.SelectedIndex == 3)
            {
                QueryRepairStatus((cmbstatus.SelectedIndex + 1).ToString());
            }
            else
            {
                QueryRepairStatus((cmbstatus.SelectedIndex).ToString());
            }
        }

        private void QueryRepairStatus(string status)
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.QueryRepairStatus(status));
            UpdateStatusToChinese(dt);
            dgvQrepair.DataSource = dt;
            labCount.Text = "共计 " + dt.Rows.Count.ToString() + " 笔";

        }  

        private void cbreasoncode_Leave(object sender, EventArgs e)
        {
            try
            {
                tbrcdesc.Text = tbrcdesc.Text.ToUpper();
                tbrcdesc.Text = FrmBLL.publicfuntion.getNewTable(dtrc, string.Format("reasoncode='{0}'", cbreasoncode.Text)).Rows[0]["reasondesc"].ToString();
            }
            catch
            {
                MessageBox.Show("原因代码未找到,请重新选择");
                cbreasoncode.Focus();
                cbreasoncode.SelectAll();
            }
        }
          List<string> ColnumName = new List<string>();
        /// <summary>
        /// 增加标示栏位名称
        /// </summary>
          private void GetColnumName()
          {
              ColnumName.Clear();
              ColnumName.Add("不良代码");
              ColnumName.Add("不良描述");
              ColnumName.Add("原因代码");
              ColnumName.Add("原因描述");
              ColnumName.Add("责任单位");
              ColnumName.Add("唯一条码");
              ColnumName.Add("工单");
              ColnumName.Add("料号");
              ColnumName.Add("产品名称");
              ColnumName.Add("途程");
              ColnumName.Add("转入人员");
              ColnumName.Add("维修人员");
              ColnumName.Add("零件位置");
              ColnumName.Add("备注");
          }
        private void imbt_Query_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(combStime.Text) || string.IsNullOrEmpty(combEtime.Text))
            //{
            //    MessageBox.Show("时间为空,请选择");
            //    return;
            //}

            if (dtpSdate.Value.Date > dtpEdate.Value.Date)
            {
               MessageBox.Show("开始日期不能大于结束日期");
                return;
            }
           
            //if (dtpSdate.Value.Date == dtpEdate.Value.Date)
            //{

            //    //if ((Convert.ToInt32(combStime.Text.Substring(0, 2))) > (Convert.ToInt32(combEtime.Text.Substring(0, 2))))
            //    //{
            //    //   MessageBox.Show("选择相同日期,开始时间不能大于结束时间");
            //    //    return;
            //    //}
            //}

            string StartDate = dtpSdate.Value.ToString("yyyyMMdd");
           // string etime = combEtime.Text.Substring(0, 2);
            //if (etime == "00")
            //{
            //    etime = "23";
            //}
            //else
            //{
            //    etime = (Convert.ToInt32(etime) - 1).ToString();
            //}
            string EndDate = dtpEdate.Value.ToString("yyyyMMdd");

            DataTable RepairDt = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetRepairReport(StartDate, EndDate,CB_Class.Text));
          
            for (int i = 0; i < ColnumName.Count; i++)
            {
                RepairDt.Columns[i].ColumnName = ColnumName[i].ToString();
            }
            for (int x = 0; x < RepairDt.Rows.Count; x++)
            {
                try
                {
                    RepairDt.Rows[x][9] = FrmBLL.publicfuntion.getNewTable(dtCraft, string.Format("craftid='{0}'", RepairDt.Rows[x][9].ToString())).Rows[0][1].ToString();

                }
                catch
                {
                }
            }

            if (dtProduct != null)
            {
                for (int x = 0; x < RepairDt.Rows.Count; x++)
                {
                    DataTable check = FrmBLL.publicfuntion.getNewTable(dtProduct, string.Format("partnumber='{0}'", RepairDt.Rows[x][7].ToString()));
                    if (check.Rows.Count == 0)
                        RepairDt.Rows[x].Delete();
                }
            }
            else
            {
            }

             dgvRepairReport.DataSource = RepairDt;

            labReportCount.Text="共计 "+dgvRepairReport.RowCount.ToString()+" 笔";
        }

        private void imbt_RepairToExcel_Click(object sender, EventArgs e)
        {
            if (dgvRepairReport.Rows.Count != 0)
            {
                FrmBLL.DataGridViewToExcel.DataToExcel(dgvRepairReport);
                MessageBox.Show("汇出Excel完成");
            }
        }

        private void dgvRepairReport_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvRepairReport.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }
        }

        private void cbreasoncode_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cb_duty_Leave(object sender, EventArgs e)
        {
            try
            {
                tb_dutydesc.Text = FrmBLL.publicfuntion.getNewTable(dtDuty, string.Format("Duty='{0}'", cb_duty.Text)).Rows[0]["DutyDesc"].ToString();
            }
            catch
            {
                MessageBox.Show("责任单位未找到,请重新选择");
                cb_duty.Focus();
                cb_duty.SelectAll();
            }
        }

        private void cb_duty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tb_dutydesc.Text = FrmBLL.publicfuntion.getNewTable(dtDuty, string.Format("Duty='{0}'", cb_duty.Text)).Rows[0]["DutyDesc"].ToString();
            }
            catch
            {
                MessageBox.Show("责任单位未找到,请重新选择");
                cb_duty.Focus();
                cb_duty.SelectAll();
            }
        }

        private void imbt_addDuty_Click(object sender, EventArgs e)
        {
            FrmDuty fd = new FrmDuty(this);
            fd.ShowDialog();
        }

        private void btn_reason_Click(object sender, EventArgs e)
        {
          
        }

        private void Labelreason_Click(object sender, EventArgs e)
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtReasonCode.Instance.GetReasonCode());
            SelectData sd = new SelectData(this, dt);
            sd.ShowDialog();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btProduct_Click(object sender, EventArgs e)
        {
            SelectDataListBox sdl = new SelectDataListBox(this,"",dtProduct);
            sdl.ShowDialog();
        }

        private void panelEx9_Click(object sender, EventArgs e)
        {

        }

        private void combEtime_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
