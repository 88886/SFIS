using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Runtime.InteropServices;


namespace SFIS_V2
{
    public partial class Frm_StockOut : Office2007Form// Form
    {
        #region varible
        private DataTable dt = new DataTable();
        private string rdstring;
        #endregion

        public Frm_StockOut(MainParent frm)
        {
            InitializeComponent();
            this.mFrm = frm;
        }
        MainParent mFrm;
        Dictionary<string, string> diccustomerId = new Dictionary<string, string>();
        #region Form Load
        private void Frm_StockOut_Load(object sender, EventArgs e)
        {




            try
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

                #region 服务端加载
                IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];
                txtServerIP.Text = ipAddr.ToString();
                this.onDownLoadList += new dDownloadList(Frm_StockOut_onDownLoadList);
                this.onDownLoadList1 += new dDownloadList1(Frm_StockOut_onDownLoadList1);
                #endregion
                string[,] objs = { { "0", "--选择--" }, { "6", "  售出" }, { "7", "  借出" }, { "8", "  领用" }, { "9", "  重工" }, { "10", "  赠品" }, { "11", "  调拨" } };
                DataTable dtcmb = ConverttoDt(objs);
                cmbstatus.DataSource = dtcmb;
                cmbstatus.DisplayMember = "value";
                cmbstatus.ValueMember = "key";
                this.panel9.Visible = false;

                //dgvStockList.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetStockList(txtPartNumber.Text.Trim()));

                AddButton("btPartition", "操作", "拆分箱", dgvStockList);


            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

        }
        #endregion
        #region 添加按钮列
        private void AddButton(string butName, string headText, string butText, DataGridView dgvName)
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = butName;
            btn.FlatStyle = FlatStyle.System;// Standard;

            btn.DefaultCellStyle.NullValue = butText;

            btn.UseColumnTextForButtonValue = false;
            btn.HeaderText = headText; ;
            dgvName.Columns.Add(btn);
            btn.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        #endregion
        #region 将M行N咧 数组转化为datatable
        public static DataTable ConverttoDt(string[,] Arrays)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("key", typeof(string));
            dt.Columns.Add("value", typeof(string));
            //string[] ColumnNames=  { "key", "value" };
            //foreach (string ColumnName in ColumnNames)
            //{
            //    dt.Columns.Add(ColumnName, typeof(string));
            //}

            for (int i1 = 0; i1 < Arrays.GetLength(0); i1++)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < 2; i++)
                {
                    dr[i] = Arrays[i1, i].ToString();
                }
                dt.Rows.Add(dr);
            }

            return dt;

        }

        #endregion

        #region 输入料号 带出该料号的库存清单,供出货选择
        private void txtPartNumber_MouseLeave(object sender, EventArgs e)
        {

        }
        private void txtPartNumber_LostFocus(object sender, EventArgs e)
        {
            try
            {
                //判断号是否正确 -----------
                if (!string.IsNullOrEmpty(txtPartNumber.Text.Trim()))
                {
                    //dgvStockList.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetStockList(txtPartNumber.Text.Trim()));
                    LoadDatalist();
                    if (dgvStockList.RowCount == 0)
                    {
                        // MessageBox.Show("该料号没有库存或料号错误！"); 
                        mFrm.ShowPrgMsg("该料号没有库存或料号错误！", MainParent.MsgType.Error);
                        txtPartNumber.Text = "";
                        return;
                    }
                    else
                    {
                        getbalance();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }
        /// <summary>
        /// 求剩余库存数量
        /// </summary>
        private void getbalance()
        {
            float sum1 = 0;
            for (int i = 0; i < dgvStockList.Rows.Count; i++)
            {
                if (dgvStockList.Rows[i].Cells[7].Value != null)
                {
                    sum1 += float.Parse(dgvStockList.Rows[i].Cells[7].Value.ToString());
                }
            }
            lblBalance.Text = sum1.ToString();
        }
        #endregion

        #region  输入SAP出货单号，带出SAP出货清单

        private void txtSAPCode_Leave(object sender, EventArgs e)
        {
            //判断号是否正确 -----------
            if (!string.IsNullOrEmpty(txtSAPCode.Text.Trim()))
            {
                //MessageBox.Show("SAPcode?");
                DataTable dtSAP = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebSapConnector.Instance.Get_Z_RFC_LIPS(txtSAPCode.Text.Trim(), ""));
                this.dgvSAPList.Invoke(new EventHandler(delegate
                {
                    dgvSAPList.DataSource = dtSAP; //("0080000010"))
                }));

                if (dtSAP.Rows.Count == 0)
                {
                    MessageBox.Show("该SAP出货单号错误！"); txtPartNumber.Text = ""; return;

                }
                else
                {
                    this.txtPartNumber.Text = this.dgvSAPList["MATNR", 0].Value.ToString().TrimStart('0');// "901001206";//
                    this.txtQty.Text = this.dgvSAPList["LFIMG", 0].Value.ToString();
                    txtPartNumber_LostFocus(null, null);
                    //---取出客户编号----   customername, address, contactperson
                    string SerialCode = "CU" + Common.RandomTimeSerial(DateTime.Now, 3);

                    string contactperson = dtSAP.Rows[0]["KUNNR"].ToString();
                    string customername = dtSAP.Rows[0]["NAME1"].ToString();

                    //MessageBox.Show("GetCustomerId(" + SerialCode + "'" + contactperson + "'" + customername);
                    txtCustomerId.Text = "ERROR"; //FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtCustomer.Instance.GetCustomerId(SerialCode, contactperson, customername)).Rows[0]["customerId"].ToString();
                    //判断Dictionary中是否包含该料号，不包含则添加
                    bool haskey = diccustomerId.ContainsKey(this.txtPartNumber.Text);
                    if (!haskey)
                    {
                        diccustomerId.Add(txtPartNumber.Text, txtCustomerId.Text);
                    }

                }

            }
        }

        #endregion

        #region 按钮文本变更
        private void dgvStockList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dgvStockList.Rows.Count > 0)
            {
                for (int i = 0; i < dgvStockList.RowCount; i++)
                {
                    if (Convert.ToInt32(dgvStockList.Rows[i].Cells["数量"].Value.ToString()) == 1)
                    {
                        dgvStockList[0, i].Value = "";
                    }
                }
            }
        }
        #endregion

        #region 选中出货信息或 点拆分箱操作弹出拆分界面进行操作====>
        private void dgvStockList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStockList.CurrentCell == null) { return; }


            try
            {
                int i = dgvStockList.CurrentCell.RowIndex;

                if ((e.RowIndex != null && e.ColumnIndex >= 0 && dgvStockList.Columns[e.ColumnIndex].Name == "btPartition") && (dgvStockList.Rows[i].Cells["数量"].Value.ToString() != "1") && (dgvStockList.CurrentCell.ColumnIndex == 0))
                {
                    string cartonnumber = dgvStockList["卡通号", dgvStockList.CurrentRow.Index].Value.ToString();
                    DataPartition dp = new DataPartition(this, cartonnumber, 1);
                    dp.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }
        #endregion

        #region 选择出--拣货
        string RecStyle = "2";
        string RecCode;
        private void btSelect_Click(object sender, EventArgs e)
        {
            string cartonnumber;
            string palletnumber;
            int totalqty;
            int subsum = 0;
            string errorMsg = CheckData();
            if (!string.IsNullOrEmpty(errorMsg))
            {
                MessageBox.Show(errorMsg); return;
            }

            if (cmbstatus.SelectedValue.ToString() != "6")
            {
                if (dgvStockOut.Rows.Count < 1)
                {
                    if (Common.CInt(txtQty.Text.Trim()) > Common.CInt(lblBalance.Text.Trim()))
                    {
                        MessageBox.Show(" 你的库存不足，请修改" + cmbstatus.Text.Trim() + "出库数量！"); return;
                    }
                }
                else
                {
                    DataTable dt = FrmBLL.publicfuntion.getNewTable(
                       dgvStockOut.DataSource as DataTable, string.Format("partnumber={0}", this.txtPartNumber.Text.Trim()));
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        if (Common.CInt(txtQty.Text.Trim()) > Common.CInt(lblBalance.Text.Trim()))
                        {
                            MessageBox.Show(" 你的库存不足，请修改" + cmbstatus.Text.Trim() + "出库数量！"); return;
                        }
                    }
                    if (dt.Rows.Count == 1)
                    {
                        int selectQty = Convert.ToInt32(dt.Rows[0]["Outqty"].ToString());
                        int sumQty = selectQty + Convert.ToInt32(lblBalance.Text.Trim());
                        if (Convert.ToInt32(txtQty.Text.Trim()) > sumQty)
                        {
                            MessageBox.Show(" 你的库存不足，请修改" + cmbstatus.Text.Trim() + "出库数量！"); return;

                        }
                    }

                }
                //bool JHStatus = chkJHstatus();
                //MessageBox.Show(JHStatus.ToString());
                //if (JHStatus == false)
                //{
                //    if (MessageBox.Show("出库数量确定？开始检货将不能修改！", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                //    {
                //        return;
                //    }
                //}


            }
            // 判断当前已经拣货数量
            if (dgvStockOut.Rows.Count > 0)
            {
                for (int i = 0; i < dgvStockOut.Rows.Count; i++)
                {
                    if (dgvStockOut["partnumber", i].Value.ToString() == this.txtPartNumber.Text.Trim() && dgvStockOut["Outstatus", i].Value.ToString() == "货物备齐")
                    {
                        MessageBox.Show("货物已经备齐"); return;
                    }
                    else if (dgvStockOut["partnumber", i].Value.ToString() == this.txtPartNumber.Text.Trim() && dgvStockOut["Outstatus", i].Value.ToString() == "备货中..")
                    {
                        subsum += Convert.ToInt32(dgvStockOut["Outqty", i].Value.ToString());
                    }
                }
            }

            if (dgvStockList.CurrentCell != null)
            {
                //messagedgvStockList.CurrentCell.Value.ToString ()
                cartonnumber = dgvStockList["卡通号", dgvStockList.CurrentRow.Index].Value.ToString();
                palletnumber = dgvStockList["栈板号", dgvStockList.CurrentRow.Index].Value.ToString();

                if (dgvStockList.Rows.Count == 0) return;

                if (dgvStockList["栈板号", dgvStockList.CurrentRow.Index].Selected == true)
                {
                    RecStyle = "1";
                    RecCode = dgvStockList["栈板号", dgvStockList.CurrentRow.Index].Value.ToString();

                    int sum1 = 0;
                    for (int i = 0; i < dgvStockList.Rows.Count; i++)
                    {
                        if (dgvStockList["栈板号", i].Value.ToString() == RecCode)
                        {
                            sum1 += Convert.ToInt32(dgvStockList["数量", dgvStockList.CurrentRow.Index].Value.ToString());
                        }
                    }
                    totalqty = sum1;

                }
                else
                {
                    RecStyle = "2";
                    RecCode = dgvStockList["卡通号", dgvStockList.CurrentRow.Index].Value.ToString();
                    totalqty = subsum + Convert.ToInt32(dgvStockList["数量", dgvStockList.CurrentRow.Index].Value.ToString());
                }


                if (totalqty > float.Parse(txtQty.Text.ToString()))
                {
                    if (RecStyle == "1")
                    {
                        string msg = "你选择的数量为：" + totalqty.ToString() + ",大于你的需求数量请少选或进行拆箱/栈板动作！\n\t 手动拆箱[是] 取消拆箱[否]";
                        DialogResult dr = MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            DataPartition dp = new DataPartition(this, palletnumber, 2);

                            dp.ShowDialog(); return;
                        }
                        else
                            return;
                    }
                    else
                    {
                        string msg = "你选择的数量为：" + totalqty.ToString() + ",大于你的需求数量请少选或进行拆箱/栈板动作！\n\t默认拆箱[是] 手动拆箱[否] 取消拆箱[取消]";
                        DialogResult dr = MessageBox.Show(msg, "Confirm Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            string carton = dgvStockList["卡通号", dgvStockList.CurrentRow.Index].Value.ToString();
                            Random rad = new Random();//实例化随机数产生器rad；
                            int value = rad.Next(1000, 10000);
                            string newcarton = carton.Substring(0, 7) + "U" + value.ToString();
                            string count = Convert.ToString(totalqty - int.Parse(txtQty.Text.Split('.')[0].ToString()));
                            refWebtWarehouseWipTracking.Instance.DefaultDataPartition(carton, newcarton, count);

                        }

                        else if (dr == DialogResult.No)
                        {
                            DataPartition dp = new DataPartition(this, cartonnumber, 1);

                            dp.ShowDialog(); return;
                        }
                        else
                            return;
                    }
                }
                //int RecType = Convert.ToInt32(this.cmbstatus.SelectedValue.ToString());
                string SAPCode = txtSAPCode.Text.Trim();
                string partnumber = txtPartNumber.Text.Trim();
                int SAPQty = Convert.ToInt32(float.Parse(txtQty.Text.ToString()));
                string CustomerId = txtCustomerId.Text.Trim();
                //生成批次，不更改状态
                refWebtWarehouseWipTracking.Instance.ProductSFClotcode(SAPCode, partnumber, SAPQty, CustomerId, RecStyle, RecCode);

                LoadDatalist();
                getbalance();
            }
        }

        private void LoadDatalist()
        {
            try
            {
                dgvStockList.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetdgvStockList(txtPartNumber.Text.Trim())/*.GetStockList(txtPartNumber.Text.Trim(), "")*/);
                dgvStockOut.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.StockOutEnd(txtSAPCode.Text.Trim(), Convert.ToInt32(cmbstatus.SelectedValue), 1));
                txtsnval.Text = "";
                //  txtPartNumber_LostFocus(null, null);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void dgvStockList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //if (dgvStockList.CurrentCell != null)
                //{

                //    btSelect_Click(null, null);
                //}
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    string flag = "cartonnumber";
                    string ColumnValue = dgvStockList["卡通号", e.RowIndex].Value.ToString();
                    DataTable dt= FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductInstockSerialInfo(flag, ColumnValue));
                    if (dt.Rows.Count > 0)
                    {
                        DataView dv = dt.DefaultView;
                        dv.Sort = "ESN ASC";
                        DataTable dTemp = dv.ToTable();
                        SerialInfo si = new SerialInfo(this,
                        FrmBLL.DataTableCrosstab.DataTableCross(dTemp, 4),
                        this.dgvStockList["料号", e.RowIndex].Value.ToString(),
                        this.dgvStockList["卡通号", e.RowIndex].Value.ToString(),
                        this.dgvStockList["栈板号", e.RowIndex].Value.ToString(),
                        null);
                        si.ShowDialog();
                    }

                }
            }
            catch
            {

            }

        }
        #endregion

        //取消选择

        #region 提交产生SFC出库批次，并记录相关信息到数据库
        private void btSubmit_Click(object sender, EventArgs e)
        {
            //数据合法性验证
            string errorMsg = CheckData();
            if (!string.IsNullOrEmpty(errorMsg))
            {
                MessageBox.Show(errorMsg);
                return;
            }
            if (dgvStockOut.Rows.Count == 0)
            {
                mFrm.ShowPrgMsg("还未拣货，提交生成出库单！", MainParent.MsgType.Error); return;
            }
            else
            {
                for (int i = 0; i < dgvStockOut.Rows.Count; i++)
                {
                    if (dgvStockOut["Outstatus", i].Value.ToString() == "备货中..")
                    {
                        mFrm.ShowPrgMsg("请先备齐货物，然后再提交生成出库单！", MainParent.MsgType.Error); return;
                    }

                }
            }

            if (dgvStockOut.Rows.Count < 1)
                return;

            //确定出库(售出/借出)
            if (cmbstatus.SelectedValue.ToString() == "6" || cmbstatus.SelectedValue.ToString() == "7")
            {
                int RecType = Convert.ToInt32(this.cmbstatus.SelectedValue.ToString());
                string SAPCode = txtSAPCode.Text.Trim();
                string userid = mFrm.gUserInfo.userId;
                for (int i = 0; i < dgvStockOut.Rows.Count; i++)
                {
                    string partnumber = dgvStockOut["partnumber", i].Value.ToString(); //txtPartNumber.Text.Trim();
                    int SAPQty = Convert.ToInt32(dgvStockOut["qty", i].Value.ToString()); //Convert.ToInt32(float.Parse(txtQty.Text.ToString()));
                    string CustomerId = diccustomerId[partnumber];//txtCustomerId.Text.Trim();

                    refWebtWarehouseWipTracking.Instance.StockOut(RecType, SAPCode, partnumber, SAPQty, CustomerId, RecStyle, userid);
                }
            }
            else
            {
                int RecType = Convert.ToInt32(this.cmbstatus.SelectedValue.ToString());
                string SAPCode = txtSAPCode.Text.Trim();
                string userid = mFrm.gUserInfo.userId;
                string partnumber = txtPartNumber.Text.Trim();
                int SAPQty = Convert.ToInt32(float.Parse(txtQty.Text.ToString()));
                string CustomerId = txtCustomerId.Text.Trim();
                refWebtWarehouseWipTracking.Instance.StockOut(RecType, SAPCode, partnumber, SAPQty, CustomerId, RecStyle, userid);
            }


            DataTable tmpdt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.StockOutEnd(txtSAPCode.Text.Trim(), Convert.ToInt32(cmbstatus.SelectedValue), 2));

            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\出库单.txt", false, Encoding.GetEncoding("gb2312"));

            StringBuilder sb = new StringBuilder();
            sb.Append(txtSAPCode.Text.Trim() + "出库清单");
            sb.Append(Environment.NewLine);
            sb.Append("-----------------------------------------------------------------");
            sb.Append(Environment.NewLine);
            for (int k = 1; k < tmpdt.Columns.Count; k++)
            {
                sb.Append(tmpdt.Columns[k].ColumnName.ToString() + "\t");

            }
            sb.Append(Environment.NewLine);

            for (int i = 0; i < tmpdt.Rows.Count; i++)
            {
                for (int j = 1; j < tmpdt.Columns.Count; j++)
                {
                    sb.Append(tmpdt.Rows[i][j].ToString() + "\t");

                }
                sb.Append(Environment.NewLine);
            }
            sw.Write(sb.ToString());
            sw.Flush();
            sw.Close();//释放资源

            System.Diagnostics.Process.Start("出库单.txt", Application.StartupPath); ;


        }

        /// <summary>
        /// 数据合法性验证
        /// </summary>
        /// <returns></returns>
        public string CheckData()
        {
            string msg = "";

            if (string.IsNullOrEmpty(this.txtSAPCode.Text.Trim()))
            {
                msg += "没有填写SAP出库单号\r\n";
            }

            if (string.IsNullOrEmpty(this.txtPartNumber.Text.Trim()))
            {
                msg += "没有填写成品料号！\r\n";
            }

            if (string.IsNullOrEmpty(this.txtQty.Text.Trim()))
            {
                msg += "没有填写出库数量\r\n";
            }

            if (string.IsNullOrEmpty(this.txtCustomerId.Text.Trim()))
            {
                msg += "没有选择客户编号\r\n";
            }

            if (cmbstatus.SelectedValue.ToString() == "0")
            {
                msg += "没有选择出库类型\r\n";
            }

            return msg;
        }

        #endregion
        #region 只允许数量文本框输入证整数
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar == 46)
            //{
            //    e.Handled = true;
            //}
            //if (e.KeyChar == 48 && (((TextBox)sender).SelectionStart == 0 ))
            //{
            //    e.Handled = true;
            //}
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            { e.Handled = true; }
            if (e.KeyChar == 13)
            {
                SendKeys.Send("\t");
            }

        }
        #endregion

        #region 出库状态的变化
        private void cmbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResiveForm(cmbstatus.SelectedValue.ToString(), cmbstatus.Text.ToString());

        }

        void ResiveForm(string status, string key)
        {
            this.txtSAPCode.Text = "";
            this.txtPartNumber.Text = "";
            this.txtQty.Text = "";
            this.txtCustomerId.Text = "";
            if (status == "6" || status == "7")
            {
                this.lblSAP.Text = "SAP出库单号";
                this.txtSAPCode.Enabled = true;
                this.lblSAPList.Visible = true;
                this.dgvSAPList.Visible = true;
                this.txtPartNumber.Enabled = false;
                this.txtQty.Enabled = false;
                buttonX1.Visible = false;
                this.panelEx1.Height = 214;

            }
            else
            {
                if (key.Trim() == "--选择--")
                {
                    this.lblSAP.Text = "出库单号";
                    this.txtSAPCode.Text = "";
                }
                else
                {
                    Random rad = new Random();
                    this.lblSAP.Text = key.Trim() + "出库单号";
                    this.txtSAPCode.Text = status + string.Format("{0:yyMMdd}", DateTime.Now) + rad.Next(1000, 10000).ToString();
                }
                this.txtSAPCode.Enabled = false;
                this.lblSAPList.Visible = false;
                this.dgvSAPList.Visible = false;
                this.txtPartNumber.Enabled = true;
                this.txtQty.Enabled = true;
                buttonX1.Visible = true;
                this.panelEx1.Height = 72;
                // dgvstockIn.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetReturnList("0", ""));

            }
        }

        #endregion

        #region 根据SAPList ， 选择拣货料号、数量
        private void dgvSAPList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //string str = "0001234500";
                //str = str.TrimStart('0');
                //MessageBox.Show(this.dgvSAPList["MATNR", e.RowIndex].Value.ToString().TrimStart('0'));

                this.txtPartNumber.Text = this.dgvSAPList["MATNR", e.RowIndex].Value.ToString().TrimStart('0'); //料号  "901001206";// 
                this.txtQty.Text = this.dgvSAPList["LFIMG", e.RowIndex].Value.ToString();        //数量
                txtPartNumber_LostFocus(null, null);

                //---取出客户编号----   customername, address, contactperson
                string SerialCode = "CU" + Common.RandomTimeSerial(DateTime.Now, 3);//CUD4H1EFP94FRY

                string contactperson = dgvSAPList["KUNNR", e.RowIndex].Value.ToString();// dtSAP.Rows[0]["KUNNR"].ToString();
                string customername = dgvSAPList["NAME1", e.RowIndex].Value.ToString(); //dtSAP.Rows[0]["NAME1"].ToString();
                txtCustomerId.Text = "ERROR"; //FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtCustomer.Instance.GetCustomerId(SerialCode, contactperson, customername)).Rows[0]["customerId"].ToString();

                //判断Dictionary中是否包含该料号，不包含则添加
                bool haskey = diccustomerId.ContainsKey(this.txtPartNumber.Text);
                if (!haskey)
                {
                    diccustomerId.Add(this.dgvSAPList["MATNR", e.RowIndex].Value.ToString().TrimStart('0'), txtCustomerId.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }

        }
        #endregion

        private void txtSAPCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("\t");
            }
        }

        private void buttonX1_Click(object sender, EventArgs e) // 选择客户编号
        {
            if (cmbstatus.SelectedValue.ToString() == "11")  /// update 2013/02/22  
            {
                SelectData dz = new SelectData(this, FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetWarehouseList()));
                dz.ShowDialog();
            }
            else
            {
                fCustomerInfo dz = new fCustomerInfo(mFrm, this);

                dz.ShowDialog();
            }
        }

        private void bt_Refesh_Click(object sender, EventArgs e)
        {
            txtPartNumber_LostFocus(null, null);

        }

        private void txtPartNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("\t");
                txtPartNumber_LostFocus(null, null);
            }

        }

        private void dgvStockOut_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvStockOut.Rows.Count != 0)
            {
                for (int i = 0; i < dgvStockOut.Rows.Count; )
                {

                    if (Convert.ToInt32(dgvStockOut["qty", i].Value.ToString()) == Convert.ToInt32(dgvStockOut["Outqty", i].Value.ToString()))
                    {
                        dgvStockOut["Outstatus", i].Value = "货物备齐";
                        dgvStockOut.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                    }
                    else
                    {
                        dgvStockOut["Outstatus", i].Value = "备货中..";
                    }

                    i++;
                }
            }
        }

        // 如果该料号已经备货 就不能更改数量
        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (cmbstatus.SelectedValue.ToString() != "6")
            {
                for (int i = 0; i < dgvStockOut.Rows.Count; )
                {

                    if (dgvStockOut["partnumber", i].Value.ToString() == txtPartNumber.Text.Trim())
                    {
                        MessageBox.Show("已经拣货不能中途修改数量！");
                        this.txtQty.Text = dgvStockOut["Qty", i].Value.ToString();
                        return;
                    }

                }
            }
        }

        private bool chkJHstatus()
        {
            bool msg = false;
            for (int i = 0; i < dgvStockOut.Rows.Count; )
            {

                if (dgvStockOut["partnumber", i].Value.ToString() == txtPartNumber.Text.Trim())
                {
                    msg = true;
                }
            }
            return msg;
        }

        //唯一序列号查询
        private void txtsnval_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(txtsnval.Text.Trim())) && (e.KeyCode == Keys.Enter))
            {
                btQuery_Click(null, null);
            }
        }

        //根据条码查询出esn，并带出箱号..
        private void btQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string cartonnumber = "";
                DataTable dttmp = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetCartonbysn(txtsnval.Text));

                if (dttmp.Rows.Count == 0)
                {
                    mFrm.ShowPrgMsg("你唯一序列号错误！", MainParent.MsgType.Error); return;
                }

                cartonnumber = dttmp.Rows[0]["cartonnumber"].ToString();

                //根据cartonnumber 带出箱 数据

                dgvStockList.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetStockList(txtPartNumber.Text.Trim(), cartonnumber));
                //如果带不出数据，说明这个序列号不属于该料号或还没入库
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dgvStockOut_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    SelectData sd = new SelectData(this,
                        FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.CheckStockList(
                        dgvStockOut["sfclotcode", e.RowIndex].Value.ToString(), "1")));
                    sd.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_back_Click(object sender, EventArgs e)
        {
            if (dgvStockOut.Rows.Count > 0)
            {
                string sfclotcode = dgvStockOut["sfclotcode", dgvStockOut.CurrentRow.Index].Value.ToString();
                string partnumber = dgvStockOut["partnumber", dgvStockOut.CurrentRow.Index].Value.ToString();
                string RES;
                if (MessageBox.Show(string.Format("确定取消SAP：{0}\n料号：{1}的出库?", sfclotcode, partnumber),
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //撤销拣货出库，删除生成的批次
                    RES = refWebtWarehouseWipTracking.Instance.CancelSFClotcode(this.txtSAPCode.Text.Trim(), partnumber, sfclotcode);
                    if (!string.IsNullOrEmpty(RES))
                    {
                        MessageBox.Show(RES);
                    }
                    LoadDatalist();
                    getbalance();
                }
            }
        }

        private void bt_excel_Click(object sender, EventArgs e)
        {
            this.bt_excel.Enabled = false;
            if (dgvStockOut.Rows.Count < 1)
            {
                this.mFrm.ShowPrgMsg("没有要导出的数据,请确认...", MainParent.MsgType.Normal);
                return;
            }
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(
                refWebtWarehouseWipTracking.Instance.CheckStockList(this.txtSAPCode.Text.Trim(), "2"));
            if (dt.Rows.Count > 0)
            {
                DataToExcel(dt);
            }
            this.bt_excel.Enabled = true;
        }
        //将datatable的数据导出到excel
        private void DataToExcel(DataTable mdt)
        {
            Excel.ApplicationClass oExcel = new Excel.ApplicationClass();
            Excel.Workbooks oBooks = oExcel.Workbooks;

            Excel._Workbook oBook = null;

            oBook = (Excel._Workbook)(oExcel.Workbooks.Add(true));// 引用excel工作薄 

            for (int i = 0; i < mdt.Columns.Count; i++)
            {
                oExcel.Cells[2, i + 1] = mdt.Columns[i].ColumnName.ToString();// m_DataView.Columns[i].HeaderText.ToString();
            }

            for (int i = 0; i < mdt.Rows.Count; i++)
            {
                for (int j = 0; j < mdt.Columns.Count; j++)
                {
                    oExcel.Cells[i + 3, j + 1] = mdt.Rows[i][j].ToString();
                }
            }

            oExcel.Visible = true;
            object Missing = System.Reflection.Missing.Value;
            //  oExcel.Run("Sheet1.printdoc", Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing);

            oBook.Application.DisplayAlerts = false;



        }

        private void dgvStockList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
                        dgvStockList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                       dgvStockList.RowHeadersDefaultCellStyle.Font, rectangle,
                       dgvStockList.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }



        #region socket服务
        #region variable
        /**
         * 服务端所需变量
         * */
        //服务器端监听对象
        private TcpListener listener;
        private NetworkStream nsServer;
        //存储客户端的消息byte[]
        private byte[] msgBytesByClient;
        //用于标识用户客户端执行是何种操作(0:发送数据　1:发送文件 2:发送数据和文件)
        private int iFunction = 0;
        //存储文件跟数据的总共byte[]
        private byte[] totalBuffer;
        private PrintDocument pdoc;
        private String printText;

        private String idcard;


        /**
         * 客户端所需变量
         * */
        //客户端连接对象
        private TcpClient client;
        //客户端网络工作流
        private NetworkStream nsClient;
        //接收服务端传来的消息
        private byte[] msgBytesByServer;
        private Queue<string> clientQueue;
        private object locker;
        private Thread workerThread;
        #endregion

        #region 启动服务器
        /// <summary>
        /// 启动服务器按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartServer_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 port = int.Parse(txtServerPort.Text);
                IPAddress localAddr = IPAddress.Parse(this.txtServerIP.Text);
                Frm_StockOut_onDownLoadList("正在创建" + txtServerIP.Text + "服务端TcpListener对象");
                //创建TcpListener监听对象
                listener = new TcpListener(localAddr, port);
                Frm_StockOut_onDownLoadList("已成功创建TcpListener对象，端口号为：" + txtServerPort.Text);
                //启动对端口的连接请求监听
                listener.Start();
                //this.button1.Enabled = true;
                this.btnStartServer.Enabled = false;
                //启动定时器
                this.timerByServer.Enabled = true;
                Thread th = new Thread(new ThreadStart(ListenerServer));
                th.IsBackground = true;
                th.Start();
                this.mFrm.ShowPrgMsg("成功启动服务!", MainParent.MsgType.Outgoing);
            }
            catch (FormatException fx)
            {
                MessageBox.Show("服务器IP地址或端口号不是有效数据！详细信息：" + fx.ToString(), "输入数据格式错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("IP地址或端口号为空！详细信息：" + ex.ToString(), "输入数据错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("TcpListener启动失败！详细信息：" + ex.ToString(), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 线程启动方法监听客户端的连接请求
        public void ListenerServer()
        {
            try
            {
                while (true)
                {
                    //判断是否客户端连接请求
                    if (listener.Pending())
                    {
                        // 接收客户端的请求，并创建一个客户端连接
                        TcpClient client = listener.AcceptTcpClient();
                        this.Frm_StockOut_onDownLoadList("接收来自" + client.Client.RemoteEndPoint + "请求！");
                        //为每一个客户端创建一个线程用于监听客户端消息
                        Thread thread = new Thread(new ParameterizedThreadStart(CreateNetworkstream));
                        thread.IsBackground = true;
                        thread.Start(client);
                    }
                    Thread.Sleep(200);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("操作无效！,详细信息：" + ex.ToString());
            }
            catch (SocketException ex)
            {
                MessageBox.Show("发生异常，详细信息：" + ex.ToString());
            }
        }
        #endregion


        #region 客户端用于监听服务端消息的定时器
        private void timerByClient_Tick(object sender, EventArgs e)//?
        {
            try
            {
                lock (nsClient)
                {
                    if (nsClient != null && nsClient.CanRead)
                    {
                        if (nsClient.DataAvailable)
                        {
                            BeginReadServerMsg(nsClient);
                        }
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                MessageBox.Show("数据无法读取，流对象已被销毁或与服务端已断开连接！详细信息：" + ex.ToString());
            }
        }
        #endregion

        #region 读取来自客户端的数据
        /// <summary>
        /// 读取来自客户端的数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void BeginReadServerMsg(NetworkStream nsClient)
        {
            lock (nsClient)
            {
                try
                {
                    if (nsClient.CanRead && nsClient.DataAvailable)
                    {
                        //保存从流中读取的字节数
                        int readSize = 0;
                        //保存从流中读取的总字节数
                        int totalSize = 0;
                        //创建临时保存每次读取数据的流对象
                        MemoryStream ms = new MemoryStream();
                        while (nsClient.DataAvailable)
                        {
                            byte[] msgByte = new byte[10240];
                            //每次从流中读取1KB的数据
                            readSize = nsClient.Read(msgByte, 0, 10240);
                            //累计总共流中保存的字节数
                            totalSize += readSize;
                            //写入临时流中用于一次性全部读取数据
                            ms.Write(msgByte, 0, readSize);
                            Frm_StockOut_onDownLoadList("已接收" + readSize + "字节的数据");
                        }
                        Frm_StockOut_onDownLoadList("服务端共发送" + totalSize + "字节的数据");
                        msgBytesByServer = new byte[totalSize];

                        // **** 这里，从ms中读取数据前，ms指针必须回零，不然会出错。****
                        ms.Position = 0;

                        //将ms临时流中保存的数据全部读出
                        int readAllSize = ms.Read(msgBytesByServer, 0, totalSize);
                        Frm_StockOut_onDownLoadList("已接收到" + readAllSize + "字节的数据,字节数据的长度：" + msgBytesByServer.Length);
                        //将接收到的byte[]转成String
                        String serverMsg = Encoding.Default.GetString(msgBytesByServer);

                        // **** 在数组上调用ToString()得不到数据的
                        Frm_StockOut_onDownLoadList("服务端消息：" + serverMsg.ToString());
                        nsClient.Flush();
                        ms.Dispose();

                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
            }
        }
        #endregion

        #region 服务器用于监听客户端连接的定时器
        private void timerByServer_Tick(object sender, EventArgs e)
        {
            try
            {
                //判断是否客户端连接请求
                if (listener.Pending())
                {
                    // 接收客户端的请求，并创建一个客户端连接
                    TcpClient client = listener.AcceptTcpClient();
                    this.lbxServer.Items.Add("接收来自" + client.Client.RemoteEndPoint + "请求！");
                    //为每一个客户端创建一个线程用于监听客户端消息
                    Thread thread = new Thread(new ParameterizedThreadStart(CreateNetworkstream));
                    thread.IsBackground = true;
                    thread.Start(client);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("操作无效！详细信息：" + ex.ToString());
            }
            catch (SocketException ex)
            {
                MessageBox.Show("发生异常，详细信息：" + ex.ToString());
            }
        }
        #endregion

        #region 创建接受客户端请求的网络工作流对象
        /// <summary>
        /// 创建网络工作流对象
        /// </summary>
        /// <param name="o">线程执行方法参数对象</param>
        public void CreateNetworkstream(Object o)
        {
            TcpClient client = o as TcpClient;
            CreateNetworkstream(client);
        }
        #endregion

        #region 创建客户端网络工作流对象
        /// <summary>
        /// 接受客户端的连接请求并创建网络工作流对象
        /// </summary>
        /// <param name="client">连接请求TcpClient对象</param>
        public void CreateNetworkstream(TcpClient server)
        {

            try
            {
                //接受客户端连接请求
                NetworkStream nsServer = server.GetStream();
                this.nsServer = nsServer;
                Frm_StockOut_onDownLoadList("创建" + server.Client.RemoteEndPoint + "客户端的网络工作流对象");
                while (true)
                {
                    if (nsServer == null)
                    {
                        Thread.CurrentThread.Abort();
                        this.Frm_StockOut_onDownLoadList("销毁" + server.Client.RemoteEndPoint + "客户端的线程");
                        break;
                    }
                    if (nsServer != null && nsServer.CanRead && nsServer.DataAvailable)
                    {
                        ReadByClient(nsServer);
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建客户端网络工作流失败！详细信息：" + ex.ToString());
            }
        }
        #endregion

        #region 开始读取来自客户端的数据
        /// <summary>
        /// 读取客户端数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void ReadDataByClient(byte[] msgBytesByClient)
        {

            //将接收到的byte[]转成String
            String serverMsg = Encoding.Default.GetString(msgBytesByClient, 0, msgBytesByClient.Length);
            Frm_StockOut_onDownLoadList("客户端说：" + serverMsg);
            tb_input.Invoke(new EventHandler(delegate
            {
                tb_input.Text = serverMsg;
            }));

            tb_input.Invoke(new EventHandler(delegate
            {
                tb_input_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }));
            //return serverMsg.ToString();
        }
        #endregion

        #region 读取来自客户端的文件
        /// <summary>
        /// 读取客户端文件数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void ReadByClient(NetworkStream nsServer)
        {
            //锁住客户端数据读取
            lock (nsServer)
            {
                try
                {

                    if (nsServer.DataAvailable)
                    {
                        //保存从流中读取的字节数
                        int readSize = 0;
                        //保存从流中读取的总字节数
                        long totalSize = 0;
                        Frm_StockOut_onDownLoadList("正在接收客户端数据，请稍候……");
                        byte[] hBuffer = new byte[100];
                        nsServer.Read(hBuffer, 0, 100);
                        string hMsg = Encoding.Default.GetString(hBuffer, 0, 100);
                        totalSize += 100;


                        if (hMsg.Trim().Replace("\0", "").IndexOf("DATA:") != -1)
                        {
                            MemoryStream ms = new MemoryStream();
                            while (nsServer.DataAvailable)
                            {
                                try
                                {
                                    byte[] msgByte = new byte[40960];
                                    //每次从流中读取1KB的数据
                                    readSize = nsServer.Read(msgByte, 0, 40960);
                                    //累计总共流中保存的字节数
                                    totalSize += readSize;
                                    //写入临时流中用于一次性全部读取数据
                                    ms.Write(msgByte, 0, readSize);
                                    Thread.Sleep(50);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("数据发生异常，详细信息：" + ex.ToString());
                                }
                            }
                            byte[] msgByClient = new byte[ms.Length];
                            ms.Position = 0;

                            //将ms临时流中保存的文件以及数据全部读出
                            int readAllSize = ms.Read(msgByClient, 0, (int)msgByClient.Length);
                            Frm_StockOut_onDownLoadList("收到客户端发送" + totalSize + "字节的数据");
                            //this.cb_fixtureid.Invoke(new EventHandler(delegate
                            //{
                            //    this.cb_fixtureid.Text = ReadDataByClient(msgByClient);
                            //}));
                            // this.tb_fixtureid.Text = ReadDataByClient(msgByClient);
                            ReadDataByClient(msgByClient);
                        }
                        else
                        {
                            try
                            {
                                if (hMsg.Split(':').Length >= 2)
                                {
                                    string filename = hMsg.Split(':')[1].Trim().Replace("\0", "");

                                    if (!Directory.Exists(Application.StartupPath + @"\Download"))
                                    {
                                        Directory.CreateDirectory(Application.StartupPath + @"\Download\");
                                    }
                                    String filePath = Application.StartupPath + @"\Download\" + filename;
                                    //将接收到的byte[]写成文件
                                    try
                                    {
                                        FileStream fs = new FileStream(filePath, FileMode.Create);
                                        while (nsServer.DataAvailable)
                                        {
                                            try
                                            {
                                                Thread.Sleep(50);
                                                byte[] filebyte = new byte[40960];
                                                //每次从流中读取1KB的数据
                                                readSize = nsServer.Read(filebyte, 0, 40960);
                                                //累计总共流中保存的字节数
                                                totalSize += readSize;
                                                //写入临时流中用于一次性全部读取数据
                                                fs.Write(filebyte, 0, readSize);
                                                fs.Flush();
                                                Frm_StockOut_onDownLoadList1("已接收：" + totalSize + " 字节");
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("数据发生异常，详细信息：" + ex.ToString());
                                            }
                                        }

                                        Frm_StockOut_onDownLoadList("收到客户端发送" + totalSize + "字节的【" + filename + "】文件");
                                        if (nsServer != null)
                                        {
                                            //获取要发送的数据
                                            String msg = "服务器端已接收【" + filename + "】文件";
                                            //将string转成byte[]
                                            Byte[] data = Encoding.Default.GetBytes(msg);
                                            //向流中写数据发送到客户端
                                            nsServer.Write(data, 0, data.Length);
                                        }
                                        fs.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("写文件发生异常！" + ex.ToString());
                                    }
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
                finally
                {
                    nsServer.Flush();
                }

            }
        }
        #endregion


        #region 读取来自客户端的文件
        /// <summary>
        /// 读取客户端文件数据
        /// </summary>
        /// <param name="nsClient"></param>
        public void ReadFileByClient(byte[] msgBytesByClient, string fileName)
        {
            if (!Directory.Exists(Application.StartupPath + @"\Download"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Download\");
            }
            String filePath = Application.StartupPath + @"\Download\" + fileName;
            //将接收到的byte[]写成文件
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                fs.Write(msgBytesByClient, 0, msgBytesByClient.Length);
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("写文件发生异常！" + ex.ToString());
            }
        }
        #endregion

        #region 读取客户端封装的文件和数据byte[]
        /// <summary>
        /// 读取客户端传送过来的存储着文件和数据byte[]
        /// </summary>
        /// <param name="nsServer"></param>
        public void ReadFileAndDataByClient(NetworkStream nsServer)
        {
            //锁住客户端数据读取
            lock (nsServer)
            {
                try
                {
                    if (nsServer.DataAvailable)
                    {
                        //保存从流中读取的字节数
                        int readSize = 0;
                        //保存从流中读取的总字节数
                        int totalSize = 0;
                        //创建临时保存每次读取数据的流对象
                        MemoryStream ms = new MemoryStream();
                        while (nsServer.DataAvailable)
                        {
                            byte[] msgByte = new byte[1024];
                            //每次从流中读取1KB的数据
                            readSize = nsServer.Read(msgByte, 0, 1024);
                            //累计总共流中保存的字节数
                            totalSize += readSize;
                            //写入临时流中用于一次性全部读取数据
                            ms.Write(msgByte, 0, readSize);
                            lbxServer.Items.Add("已接收" + readSize + "字节的数据");
                        }

                        lbxServer.Items.Add("客户端共发送" + totalSize + "字节的数据");
                        msgBytesByClient = new byte[totalSize];
                        ms.Position = 0;

                        //将ms临时流中保存的文件以及数据全部读出
                        int readAllSize = ms.Read(msgBytesByClient, 0, totalSize);

                        //取出byte[]中的文件数据并生成文件
                        String filePath = Application.StartupPath + @"\zp.wlt";
                        //将接收到的byte[]写成文件
                        try
                        {
                            FileStream fs = new FileStream(filePath, FileMode.Create);
                            fs.Write(msgBytesByClient, 0, 1024);
                            fs.Flush();
                            fs.Close();
                            lbxServer.Items.Add("已接收到1024个字节的文件");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("写文件发生异常！" + ex.ToString());
                        }

                        //取出byte[]中的数据并生成string
                        String msg = Encoding.Default.GetString(msgBytesByClient, 1024, msgBytesByClient.Length - 1024);
                        lbxServer.Items.Add("客户端：" + msg);
                        nsServer.Flush();
                        ms.Dispose();

                        printText = msg;

                        //解压zp.wlt相片文件
                        DesImageFile();
                        //打印客户端数据
                        printMethod();
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("读取来自客户端的数据发生异常，详细信息：" + ex.ToString());
                }
            }
        }
        #endregion

        #region 打印客户端数据
        /// <summary>
        /// 打印客户端数据
        /// </summary>
        public void printMethod()
        {
            string msg = WriteImageFile();

            lock (locker)
            {
                lbxServer.Items.Add("入队列:" + msg);
                clientQueue.Enqueue(msg);		// 入队列
            }

            if (workerThread != null &&
                (workerThread.ThreadState & ThreadState.Suspended)
                != ThreadState.Running)
            {
                workerThread.Resume();		// 唤醒线程
            }
        }

        public String WriteImageFile()
        {
            string filepath = "";
            try
            {
                /**
                 *绘制背景图片
                 * 
                 */
                Image imgBack = Image.FromFile(Application.StartupPath + @"\IDCard.bmp");
                //create a Bitmap the Size of the original photograph
                Bitmap bmBack = new Bitmap(imgBack.Width, imgBack.Height);
                Graphics e = Graphics.FromImage(bmBack);
                //既然使用位图，就需要先画出图片，在画字
                e.DrawImage(imgBack, new Rectangle(0, 0, imgBack.Width, imgBack.Height));

                /**
                 * 
                 * 绘制头像图片
                 * */
                Image imgPhoto = Image.FromFile(Application.StartupPath + @"\zp.bmp");
                Bitmap bmPhoto = new Bitmap(imgPhoto);
                bmPhoto.MakeTransparent(bmPhoto.GetPixel(20, 20));

                //叠加
                e.DrawImage(bmPhoto, new Rectangle(200, 32, imgPhoto.Width, imgPhoto.Height));  //Set the detination Position
                String[] idCardInfo = printText.Split('|');
                idcard = idCardInfo[5];
                if (idCardInfo != null && idCardInfo.Length > 0)
                {
                    //姓名
                    e.DrawString(idCardInfo[0], new Font("宋体", 12), new System.Drawing.SolidBrush(Color.Black), 60, 32);
                    //性别
                    e.DrawString(idCardInfo[1], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 55);
                    //民族
                    e.DrawString(idCardInfo[2], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 130, 55);
                    try
                    {
                        //出生日期 
                        String year = idCardInfo[3].Substring(0, 4);
                        String month = idCardInfo[3].Substring(5, 2);
                        String date = idCardInfo[3].Substring(8, 2);
                        e.DrawString(year, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 80);
                        e.DrawString(month, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 110, 80);
                        e.DrawString(date, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 140, 80);
                    }
                    catch
                    {
                    }
                    //住址
                    if (idCardInfo[4].Length > 11)
                    {
                        int rows = 0;
                        if (idCardInfo[4].Length % 11 == 0)
                        {
                            rows = idCardInfo[4].Length / 11;
                        }
                        else
                        {
                            rows = idCardInfo[4].Length / 11 + 1;
                        }
                        if (rows == 2)
                        {
                            String startStr = idCardInfo[4].Substring(0, 11);
                            String endStr = idCardInfo[4].Substring(11, idCardInfo[4].Length - 11);
                            e.DrawString(startStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 108);
                            e.DrawString(endStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 125);
                        }
                        if (rows == 3)
                        {
                            String startStr = idCardInfo[4].Substring(0, 11);
                            String middleStr = idCardInfo[4].Substring(11, 11);
                            String endStr = idCardInfo[4].Substring(22, idCardInfo[4].Length - 22);
                            e.DrawString(startStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 108);
                            e.DrawString(middleStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 125);
                            e.DrawString(endStr, new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 142);
                        }
                    }
                    else
                    {
                        e.DrawString(idCardInfo[4], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 60, 108);
                    }
                    //身份号码
                    e.DrawString(idCardInfo[5], new Font("宋体", 12, FontStyle.Bold), new System.Drawing.SolidBrush(Color.Black), 106, 170);
                    //签发机关
                    e.DrawString(idCardInfo[6], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 130, 364);
                    //有效期限
                    e.DrawString(idCardInfo[7], new Font("宋体", 9), new System.Drawing.SolidBrush(Color.Black), 130, 389);
                }
                filepath = Application.StartupPath + @"\" + idcard + ".bmp";
                bmBack.Save(filepath, ImageFormat.Bmp);
                e.Dispose();
                bmBack.Dispose();
                imgBack.Dispose();
                return filepath;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return filepath;
        }

        void pdoc_BeginPrint(object sender, PrintEventArgs e)
        {
            EventHandler del = delegate
            {
                lbxServer.Items.Add(String.Format("{0} 开始打印...{1}", DateTime.Now, Environment.NewLine));
            };

            lbxServer.Invoke(del);
        }

        void pdoc_EndPrint(object sender, PrintEventArgs e)
        {
            EventHandler del = delegate
            {
                lbxServer.Items.Add(String.Format("{0} 打印结束...{1}{1}", DateTime.Now, Environment.NewLine));
            };

            lbxServer.Invoke(del);
        }

        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // 这里的值不能是txtMessage.Text，而应该是由队列中取出的
            // 因为要打印时，txtMessgae的Text已经改变了，txtMessage和printer不同步
            string msg;
            lock (locker)
            {
                msg = clientQueue.Dequeue();	// 出队列
            }
            e.Graphics.DrawImage(Image.FromFile(msg), new Point(100, 100));
            lbxServer.Items.Add(msg);
        }
        #endregion

        #region 将zp.wlt文件解密成照片
        /// <summary>
        /// 解密zp.wlt相片文件
        /// </summary>
        /// <returns>解密结果</returns>
        public void DesImageFile()
        {
            try
            {
                String filePath = Application.StartupPath + @"\zp.wlt";
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("zp.wlt文件不存在！");
                    return;
                }
                int returnValue = GetBmp(filePath, 1);
                String showMsg = "";
                switch (returnValue)
                {
                    case 0: showMsg = "调用sdtapi.dll错误！"; break;
                    case -1: showMsg = "照片解密错误！"; break;
                    case -2: showMsg = "wlt文件后缀错误！"; break;
                    case -3: showMsg = "wlt文件打开错误！"; break;
                    case -4: showMsg = "wlt文件格式错误！"; break;
                    case -5: showMsg = "软件未授权！"; break;
                    case -6: showMsg = "设备连接错误！"; break;
                    default: showMsg = ""; break;
                }
                if (showMsg != "")
                {
                    MessageBox.Show(showMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 向客户端发送数据
        private void ServerSendToClient(string SendMsg)
        {
            try
            {
                if (nsServer != null)
                {
                    //获取要发送的数据
                    String msg = SendMsg;
                    //将string转成byte[]
                    Byte[] data = Encoding.Default.GetBytes(msg);
                    //向流中写数据发送到客户端
                    nsServer.Write(data, 0, data.Length);
                    //发送数据
                    nsServer.Flush();
                    Frm_StockOut_onDownLoadList(data.Length + "字节的数据已成功发送！");
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("向客户端发送数据失败，详细信息：" + ex.ToString());
            }
        }
        #endregion


        #region 解密相片文件
        /// <summary>
        /// 解密文件的SDK方法
        /// </summary>
        /// <param name="file_name"></param>
        /// <param name="intf"></param>
        /// <returns></returns>
        [DllImport("WltRS.dll")]
        public static extern int GetBmp(string file_name, int intf);
        #endregion


        //委托
        public delegate void dDownloadList(string msg);
        //事件
        public event dDownloadList onDownLoadList;

        public void Frm_StockOut_onDownLoadList(string msg)
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new Frm_StockOut.dDownloadList(Frm_StockOut_onDownLoadList), new object[] { msg });
            }
            else
            {
                lbxServer.Items.Add(msg);
                Application.DoEvents();
            }
        }

        //委托
        public delegate void dDownloadList1(string msg);
        //事件
        public event dDownloadList1 onDownLoadList1;

        public void Frm_StockOut_onDownLoadList1(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Frm_StockOut.dDownloadList1(Frm_StockOut_onDownLoadList1), new object[] { msg });
            }
            else
            {
                //lblFileSend.Text = msg;
                Application.DoEvents();
            }
        }
        public void dispose()
        {
            if (nsClient != null)
                nsClient.Close();
            if (client != null)
                client.Close();
        }
        /// <summary>
        /// 将listview的内容写入txt文本。
        /// </summary>
        private void ListViewToTxt()
        {
            if (lbxServer.Items.Count < 1)
                return;
            try
            {
                string str = string.Empty;
                long cols = lbxServer.Items.Count;
                foreach (ListViewItem lvi in lbxServer.Items)
                {
                    for (long i = 0; i < cols; i++)
                    {
                        str += lvi.SubItems[(Int32)i].Text + "\r\n";
                    }
                }
                FileStream fs = new FileStream(string.Format("{0}Socket.txt",
                System.AppDomain.CurrentDomain.BaseDirectory), FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(str);
                sw.Close();
                fs.Close();
                lbxServer.Items.Clear();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        #endregion
        string InputData = "";
        /// <summary>
        /// 存放出库类型命令
        /// </summary>
        string sStatus = "";
        /// <summary>
        /// 存放sap单号
        /// </summary>
        string SapCode = "";
        /// <summary>
        /// 存放序列类型命令
        /// </summary>
        string sType = "";
        /// <summary>
        /// 存放序列值
        /// </summary>
        string sSerial = "";
        /// <summary>
        /// 存放料号
        /// </summary>
        string sPartnumber = "";
        /// <summary>
        /// 存放数量
        /// </summary>
        string sQty = "";
        private void tb_input_KeyDown(object sender, KeyEventArgs e)
        {
            InputData = this.tb_input.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(InputData) && e.KeyValue == 13)
            {
                this.tb_input.Text = "";
                if (string.IsNullOrEmpty(sStatus))
                {
                    #region 判断出库类型
                    if (InputData == "SOLD" || InputData == "LEND" || InputData == "TAKE" ||
                    InputData == "REWORK" || InputData == "GIFT" || InputData == "MOVE")
                    {
                        sStatus = InputData;
                        ServerSendToClient("Type CMD OK!");
                        #region +++
                        if (sStatus == "TAKE")
                        {
                            this.cmbstatus.SelectedValue = "8";
                            cmbstatus_SelectedIndexChanged(null, null);
                            ServerSendToClient("TAKE OUT!");
                            SapCode = txtSAPCode.Text;

                            return;
                        }
                        if (sStatus == "REWORK")
                        {
                            this.cmbstatus.SelectedValue = "9";
                            cmbstatus_SelectedIndexChanged(null, null);
                            ServerSendToClient("REWORK OUT!");
                            SapCode = txtSAPCode.Text;

                            return;
                        }
                        if (sStatus == "GIFT")
                        {
                            this.cmbstatus.SelectedValue = "10";
                            cmbstatus_SelectedIndexChanged(null, null);
                            ServerSendToClient("GIFT OUT!");
                            SapCode = txtSAPCode.Text;

                            return;
                        }
                        if (sStatus == "MOVE")
                        {
                            this.cmbstatus.SelectedValue = "11";
                            cmbstatus_SelectedIndexChanged(null, null);
                            ServerSendToClient("MOVE OUT!");
                            SapCode = txtSAPCode.Text;

                            return;
                        }
                        #endregion
                    }
                    else
                    {
                        sStatus = "";
                        ServerSendToClient("ERROR:CMD ERROR!");
                        return;
                    }
                    #endregion
                }

                else
                    if (InputData == "UNDO")
                    {
                        sStatus = "";
                        sType = "";
                        InputData = "";
                        SapCode = "";
                        sType = "";
                        sSerial = "";
                        sPartnumber = "";
                        sQty = "";
                        cmbstatus.SelectedValue = 0;
                        this.cmbstatus.SelectedValue = "0";
                        this.txtSAPCode.Text = "";
                        this.txtPartNumber.Text = "";
                        this.txtQty.Text = "";
                        this.txtCustomerId.Text = "";
                        ServerSendToClient("UNDO OK,Please Input CMD?");
                        return;
                    }
                    else
                        #region 判断不同出库类型

                        if (sStatus == "SOLD")//售出
                        {
                            #region 输入sap单号

                            if (string.IsNullOrEmpty(SapCode))
                            {
                                this.cmbstatus.SelectedValue = "6";
                                cmbstatus_SelectedIndexChanged(null, null);
                                ServerSendToClient("SOLD OUT!");

                                txtSAPCode.Text = SapCode = InputData;
                                txtSAPCode_Leave(null, null);
                                return;
                            }
                            #endregion
                            else
                                if (InputData=="CLEAR")
                                {
                                    sType ="";
                                    sSerial = "";
                                    ServerSendToClient("CLEAR Serial OK!");
                                }
                                else
                                    if (string.IsNullOrEmpty(sType))
                                    {
                                        if (InputData == "SN" || InputData == "TRAY" ||
                                            InputData == "CARTON" || InputData == "PALLET")
                                        {
                                            sType = InputData;
                                            ServerSendToClient("Serial CMD OK!");
                                        }
                                        else
                                        {
                                            sType = "";
                                            ServerSendToClient("ERROR:Serial CMD ERROR!");
                                            return;
                                        }
                                    }
                                    else
                                        JudgeSLInfo();
                        }
                        else
                            if (sStatus == "LEND")//借出
                            {
                                #region 输入sap单号

                                if (string.IsNullOrEmpty(SapCode))
                                {
                                    this.cmbstatus.SelectedValue = "7";
                                    cmbstatus_SelectedIndexChanged(null, null);
                                    ServerSendToClient("LEND OUT!");

                                    txtSAPCode.Text = SapCode = InputData;
                                    txtSAPCode_Leave(null, null);
                                    return;
                                }
                                #endregion
                                else
                                    if (string.IsNullOrEmpty(sType))
                                    {
                                        if (InputData == "SN" || InputData == "TRAY" ||
                                            InputData == "CARTON" || InputData == "PALLET")
                                        {
                                            sType = InputData;
                                            ServerSendToClient("Serial CMD OK!");
                                        }
                                        else
                                        {
                                            sType = "";
                                            ServerSendToClient("ERROR:Serial CMD ERROR!");
                                            return;
                                        }
                                    }
                                    else
                                        JudgeSLInfo();
                            }
                            else
                                if (sStatus == "TAKE")//领用
                                {
                                    if (string.IsNullOrEmpty(sPartnumber))
                                    {
                                        txtPartNumber.Text = sPartnumber = InputData;
                                        txtPartNumber_LostFocus(null, null);
                                    }
                                    else
                                        if (string.IsNullOrEmpty(sQty))
                                        {
                                            txtQty.Text = sQty = InputData;
                                        }
                                        else
                                            if (string.IsNullOrEmpty(sType))
                                            {
                                                if (InputData == "SN" || InputData == "TRAY" ||
                                                    InputData == "CARTON" || InputData == "PALLET")
                                                {
                                                    sType = InputData;
                                                    ServerSendToClient("Serial CMD OK!");
                                                }
                                                else
                                                {
                                                    sType = "";
                                                    ServerSendToClient("ERROR:Serial CMD ERROR!");
                                                    return;
                                                }
                                            }
                                            else
                                                JudgeTRGMInfo();
                                }
                                else
                                    if (sStatus == "REWORK")//重工
                                    {
                                        if (string.IsNullOrEmpty(sPartnumber))
                                        {
                                            txtPartNumber.Text = sPartnumber = InputData;
                                            txtPartNumber_LostFocus(null, null);
                                        }
                                        else
                                            if (string.IsNullOrEmpty(sQty))
                                            {
                                                txtQty.Text = sQty = InputData;
                                            }
                                            else
                                                if (string.IsNullOrEmpty(sType))
                                                {
                                                    if (InputData == "SN" || InputData == "TRAY" ||
                                                        InputData == "CARTON" || InputData == "PALLET")
                                                    {
                                                        sType = InputData;
                                                        ServerSendToClient("Serial CMD OK!");
                                                    }
                                                    else
                                                    {
                                                        sType = "";
                                                        ServerSendToClient("ERROR:Serial CMD ERROR!");
                                                        return;
                                                    }
                                                }
                                                else
                                                    JudgeTRGMInfo();
                                    }
                                    else
                                        if (sStatus == "GIFT")//赠品
                                        {
                                            if (string.IsNullOrEmpty(sPartnumber))
                                            {
                                                txtPartNumber.Text = sPartnumber = InputData;
                                                txtPartNumber_LostFocus(null, null);
                                            }
                                            else
                                                if (string.IsNullOrEmpty(sQty))
                                                {
                                                    txtQty.Text = sQty = InputData;
                                                }
                                                else
                                                    if (string.IsNullOrEmpty(sType))
                                                    {
                                                        if (InputData == "SN" || InputData == "TRAY" ||
                                                            InputData == "CARTON" || InputData == "PALLET")
                                                        {
                                                            sType = InputData;
                                                            ServerSendToClient("Serial CMD OK!");
                                                        }
                                                        else
                                                        {
                                                            sType = "";
                                                            ServerSendToClient("ERROR:Serial CMD ERROR!");
                                                            return;
                                                        }
                                                    }
                                                    else
                                                        JudgeTRGMInfo();
                                        }
                                        else
                                            if (sStatus == "MOVE")//调拨
                                            {
                                                if (string.IsNullOrEmpty(sPartnumber))
                                                {
                                                    txtPartNumber.Text = sPartnumber = InputData;
                                                    txtPartNumber_LostFocus(null, null);
                                                }
                                                else
                                                    if (string.IsNullOrEmpty(sQty))
                                                    {
                                                        txtQty.Text = sQty = InputData;
                                                    }
                                                    else
                                                        if (string.IsNullOrEmpty(sType))
                                                        {
                                                            if (InputData == "SN" || InputData == "TRAY" ||
                                                                InputData == "CARTON" || InputData == "PALLET")
                                                            {
                                                                sType = InputData;
                                                                ServerSendToClient("Serial CMD OK!");
                                                            }
                                                            else
                                                            {
                                                                sType = "";
                                                                ServerSendToClient("ERROR:Serial CMD ERROR!");
                                                                return;
                                                            }
                                                        }
                                                        else
                                                            JudgeTRGMInfo();
                                            }

                        #endregion


            }
        }
        /// <summary>
        /// 当拣货的料与控件料号不匹配时，调用该填充控件方法
        /// </summary>
        /// <param name="index"></param>
        private void FillControl(int index)
        {
            try
            {
                this.txtPartNumber.Text = this.dgvSAPList["MATNR", index].Value.ToString().TrimStart('0'); //料号  "901001206";// 
                this.txtQty.Text = this.dgvSAPList["LFIMG", index].Value.ToString();        //数量
                txtPartNumber_LostFocus(null, null);

                //---取出客户编号----   customername, address, contactperson
                string SerialCode = "CU" + Common.RandomTimeSerial(DateTime.Now, 3);//CUD4H1EFP94FRY

                string contactperson = dgvSAPList["KUNNR", index].Value.ToString();// dtSAP.Rows[0]["KUNNR"].ToString();
                string customername = dgvSAPList["NAME1", index].Value.ToString(); //dtSAP.Rows[0]["NAME1"].ToString();
                txtCustomerId.Text = "ERROR";//FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtCustomer.Instance.GetCustomerId(SerialCode, contactperson, customername)).Rows[0]["customerId"].ToString();

                //判断Dictionary中是否包含该料号，不包含则添加
                bool haskey = diccustomerId.ContainsKey(this.txtPartNumber.Text);
                if (!haskey)
                {
                    diccustomerId.Add(this.dgvSAPList["MATNR", index].Value.ToString().TrimStart('0'), txtCustomerId.Text);
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Normal);
            }
        }
        /// <summary>
        /// 出库拣货判定
        /// </summary>
        /// <param name="serialval"></param>
        /// <param name="flag"></param>
        private void SelectProductOut(string serialval, string flag)
        {
            int totalqty = 0;
            int subsum = 0;
            string errorMsg = CheckData();
            if (!string.IsNullOrEmpty(errorMsg))
            {
                MessageBox.Show(errorMsg); return;
            }

            if (cmbstatus.SelectedValue.ToString() != "6")
            {
                //if (Common.CInt(txtQty.Text.Trim()) > Common.CInt(lblBalance.Text.Trim()))
                //{
                //    MessageBox.Show(" 你的库存不足，请修改" + cmbstatus.Text.Trim() + "出库数量！"); return;
                //}
                if (dgvStockOut.Rows.Count < 1)
                {
                    if (Common.CInt(txtQty.Text.Trim()) > Common.CInt(lblBalance.Text.Trim()))
                    {
                        MessageBox.Show(" 你的库存不足，请修改" + cmbstatus.Text.Trim() + "出库数量！"); return;
                    }
                }
                else
                {
                    DataTable dt = FrmBLL.publicfuntion.getNewTable(
                       dgvStockOut.DataSource as DataTable, string.Format("partnumber={0}", this.txtPartNumber.Text.Trim()));
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        if (Common.CInt(txtQty.Text.Trim()) > Common.CInt(lblBalance.Text.Trim()))
                        {
                            MessageBox.Show(" 你的库存不足，请修改" + cmbstatus.Text.Trim() + "出库数量！"); return;
                        }
                    }
                    if (dt.Rows.Count == 1)
                    {
                        int selectQty = Convert.ToInt32(dt.Rows[0]["Outqty"].ToString());
                        int sumQty = selectQty + Convert.ToInt32(lblBalance.Text.Trim());
                        if (Convert.ToInt32(txtQty.Text.Trim()) > sumQty)
                        {
                            MessageBox.Show(" 你的库存不足，请修改" + cmbstatus.Text.Trim() + "出库数量！"); return;

                        }
                    }


                }
            }
            // 判断当前已经拣货数量
            if (dgvStockOut.Rows.Count > 0)
            {
                for (int i = 0; i < dgvStockOut.Rows.Count; i++)
                {
                    if (dgvStockOut["partnumber", i].Value.ToString() == this.txtPartNumber.Text.Trim() && dgvStockOut["Outstatus", i].Value.ToString() == "货物备齐")
                    {
                        MessageBox.Show("货物已经备齐"); return;
                    }
                    else if (dgvStockOut["partnumber", i].Value.ToString() == this.txtPartNumber.Text.Trim() && dgvStockOut["Outstatus", i].Value.ToString() == "备货中..")
                    {
                        subsum += Convert.ToInt32(dgvStockOut["Outqty", i].Value.ToString());
                    }
                }
            }

            if (dgvStockList.Rows.Count > 0)
            {
                if (flag == "1")
                {
                    RecStyle = "1";
                    RecCode = serialval;

                    int sum1 = 0;
                    for (int i = 0; i < dgvStockList.Rows.Count; i++)
                    {
                        if (dgvStockList["栈板号", i].Value.ToString() == RecCode)
                        {
                            sum1 += Convert.ToInt32(dgvStockList["数量", i].Value.ToString());
                        }
                    }
                    totalqty = sum1;

                }
                else
                {
                    RecStyle = "2";
                    RecCode = serialval;
                    for (int i = 0; i < dgvStockList.Rows.Count; i++)
                    {
                        if (dgvStockList["卡通号", i].Value.ToString() == RecCode)
                        {
                            totalqty = subsum + Convert.ToInt32(dgvStockList["数量", i].Value.ToString());
                            break;
                        }
                    }
                }


                if (totalqty > float.Parse(txtQty.Text.ToString()))
                {
                    string msg = "你选择的数量为：" + totalqty.ToString() + ",大于你的需求数量请少选或进行拆箱动作！\n\t默认拆箱[是] 手动拆箱[否] 取消拆箱[取消]";

                    if (RecStyle == "1")
                    {
                        DialogResult dr = MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            DataPartition dp = new DataPartition(this, serialval, 2);

                            dp.ShowDialog(); return;
                        }
                        else
                            return;
                    }
                    else
                    {

                        DialogResult dr = MessageBox.Show(msg, "Confirm Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            string carton = serialval;
                            Random rad = new Random();//实例化随机数产生器rad；
                            int value = rad.Next(1000, 10000);
                            string newcarton = carton.Substring(0, 7) + "U" + value.ToString();
                            string count = Convert.ToString(totalqty - int.Parse(txtQty.Text.Split('.')[0].ToString()));
                            refWebtWarehouseWipTracking.Instance.DefaultDataPartition(carton, newcarton, count);

                        }

                        else if (dr == DialogResult.No)
                        {
                            DataPartition dp = new DataPartition(this, serialval, 1);

                            dp.ShowDialog(); return;
                        }
                        else
                            return;

                    }
                }
                int RecType = Convert.ToInt32(this.cmbstatus.SelectedValue.ToString());
                string SAPCode = txtSAPCode.Text.Trim();
                string partnumber = txtPartNumber.Text.Trim();
                int SAPQty = Convert.ToInt32(float.Parse(txtQty.Text.ToString()));
                string CustomerId = txtCustomerId.Text.Trim();
                //生成批次，不更改状态
                refWebtWarehouseWipTracking.Instance.ProductSFClotcode(SAPCode, partnumber, SAPQty, CustomerId, RecStyle, RecCode);

                LoadDatalist();
                getbalance();
            }

        }

        /// <summary>
        /// 售出/借出序列信息判定
        /// </summary>
        private void JudgeSLInfo()
        {
            #region 序列信息判定
            if (sType == "SN")
            {
                sSerial = InputData;
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductAllInfo("snval", sSerial));
                if (dt != null && dt.Rows.Count > 0)
                {
                    string mCartonnumber = mCartonnumber = dt.Rows[0]["cartonnumber"].ToString();
                    string mPartnumber = dt.Rows[0]["partnumber"].ToString();
                    int index = 0;//获取该料号在datagridview中的行索引
                    if (this.txtPartNumber.Text != mPartnumber)
                    {
                        for (int i = 0; i < dgvSAPList.Rows.Count; i++)
                        {
                            if (dgvSAPList["MATNR", i].Value.ToString() == mPartnumber)
                            {
                                index = i;
                                break;
                            }
                            ServerSendToClient("Product Not In SAP");
                        }
                        FillControl(index);
                    }

                    SelectProductOut(mCartonnumber, "2");

                }
                else
                {
                    ServerSendToClient("Product Unexist!");
                    return;
                }
            }
            else
                if (sType == "CARTON")
                {
                    sSerial = InputData;
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductAllInfo("cartonnumber", sSerial));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string mPartnumber = dt.Rows[0]["partnumber"].ToString();
                        int index = 0;//获取该料号在datagridview中的行索引
                        if (this.txtPartNumber.Text != mPartnumber)
                        {
                            for (int i = 0; i < dgvSAPList.Rows.Count; i++)
                            {
                                if (dgvSAPList["MATNR", i].Value.ToString() == mPartnumber)
                                {
                                    index = i;
                                    break;
                                }
                                ServerSendToClient("Product Not In SAP");
                            }
                            FillControl(index);
                        }

                        SelectProductOut(sSerial, "2");
                    }
                }
                else
                    if (sType == "PALLET")
                    {
                        sSerial = InputData;
                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductAllInfo("palletnumber", sSerial));

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string mPartnumber = dt.Rows[0]["partnumber"].ToString();
                            int index = 0;//获取该料号在datagridview中的行索引
                            if (this.txtPartNumber.Text != mPartnumber)
                            {
                                for (int i = 0; i < dgvSAPList.Rows.Count; i++)
                                {
                                    if (dgvSAPList["MATNR", i].Value.ToString() == mPartnumber)
                                    {
                                        index = i;
                                        break;
                                    }
                                    ServerSendToClient("Product Not In SAP");
                                }
                                FillControl(index);
                            }

                            SelectProductOut(sSerial, "1");
                        }
                    }
            #endregion
        }

        /// <summary>
        /// 领用/重工/赠品/调拨序列信息判定
        /// </summary>
        private void JudgeTRGMInfo()
        {
            #region 序列信息判定
            if (sType == "SN")
            {
                sSerial = InputData;
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductAllInfo("snval", sSerial));
                if (dt != null && dt.Rows.Count > 0)
                {
                    string mCartonnumber = mCartonnumber = dt.Rows[0]["cartonnumber"].ToString();
                    string mPartnumber = dt.Rows[0]["partnumber"].ToString();
                    if (this.txtPartNumber.Text.Trim() != mPartnumber)
                    {
                        ServerSendToClient("PARTNUMBER ERROR!");
                        return;
                    }
                    if (dgvStockList.Rows.Count < 1)
                    {
                        ServerSendToClient("PRODUCT NONE!");
                        return;
                    }
                    SelectProductOut(mCartonnumber, "2");
                }
                else
                {
                    ServerSendToClient("Product Unexist!");
                    return;
                }
            }
            else
                if (sType == "CARTON")
                {
                    sSerial = InputData;
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductAllInfo("cartonnumber", sSerial));

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string mPart = dt.Rows[0]["partnumber"].ToString();
                        if (this.txtPartNumber.Text.Trim() != mPart)
                        {
                            ServerSendToClient("PARTNUMBER ERROR!");
                            return;
                        }
                        if (dgvStockList.Rows.Count < 1)
                        {
                            ServerSendToClient("PRODUCT NONE!");
                            return;
                        }

                        SelectProductOut(sSerial, "2");
                    }
                }
                else
                    if (sType == "PALLET")
                    {
                        sSerial = InputData;
                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductAllInfo("palletnumber", sSerial));

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string mPartnumber = dt.Rows[0]["partnumber"].ToString();
                            if (this.txtPartNumber.Text.Trim() != mPartnumber)
                            {
                                ServerSendToClient("PARTNUMBER ERROR!");
                                return;
                            }
                            if (dgvStockList.Rows.Count < 1)
                            {
                                ServerSendToClient("PRODUCT NONE!");
                                return;
                            }

                            SelectProductOut(sSerial, "1");
                        }
                    }
            #endregion
        }

        private void bt_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDatalist();
                getbalance();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Normal);
            }
        }

    }
}



