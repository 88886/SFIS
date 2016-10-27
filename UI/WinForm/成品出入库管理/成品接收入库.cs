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
    public partial class Frm_StockReceive : Office2007Form// Form
    {

        public Frm_StockReceive(MainParent frm)
        {
            InitializeComponent();
            this.mFrm = frm;
        }
        MainParent mFrm;
        private DataTable mdtDgvstoreIn = new DataTable("dgvstore");
        private DataTable mdtClonePallet = null;
        private DataTable mdtCloneCarton = null;
        private DataTable mdtToSap = null;
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };

        public void ShowPrgMsg(string msg,mLogMsgType msgtype)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = mLogMsgTypeColor[(int)msgtype];
                    rtbmsg.AppendText(msg + "\n");
                    rtbmsg.ScrollToCaret();
                }));
            }
            catch
            {
            }
        }

        private void Frm_StockReceive_Load(object sender, EventArgs e)
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

                this.comboBoxEx1.Items.Add("卡通号");
                this.comboBoxEx1.Items.Add("栈板号");
                string[,] objs = { { "0", "--选择--" }, { "1", "生产入库" }, { "2", "客退入库" }, { "3", "商务客退" }, { "4", "拣货上架" } };

                DataTable dtcmb = ConverttoDt(objs);
                cmbstatus.DataSource = dtcmb;
                cmbstatus.DisplayMember = "value";
                cmbstatus.ValueMember = "key";
             
            }
            catch (Exception ex)
            {
                ShowPrgMsg(ex.Message, mLogMsgType.Error);
            }

        }

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

        //入库方式改变，刷入批次改变  ====》 入库方式的选择改变
        private void cmbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
            this.tb_selectedcount.Text = "";
            this.bt_refresh.Enabled = true;

            gbrdstock3.Enabled = true;
            gbrdstock2.Enabled = true;
            gbrdstock1.Enabled = true;
      

            if (cmbstatus.SelectedValue.ToString() == "1")
            {
                this.gbrdstock0.Text = "以批次方式";
                this.gbrdstock1.Text = "以栈板号方式";
                this.btnSubmit.Name = "提交接收";

                this.gbrdstock3.Visible = true;             
                this.dgvstockIn.Visible = true;
                this.dgv_showcarton.Visible = false;
                dgvstockIn.DataSource = null;

                gbrdstock3.Enabled = false;
                gbrdstock2.Enabled = false;
                gbrdstock1.Enabled = false;
              

                //加载待入库批次
                //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetAllLotInfo());
                //dt.DefaultView.Sort = "批次 asc";
                //dgv_showlotinfo.DataSource = dt.DefaultView.ToTable();



            }
            else if (cmbstatus.SelectedValue.ToString() == "2" || cmbstatus.SelectedValue.ToString() == "3")
            {
                this.gbrdstock0.Text = "以刷SN号方式";
                this.gbrdstock1.Text = "以刷MAC号方式";
                this.btnSubmit.Name = "提交接收";

                this.gbrdstock3.Visible = false;            
                txtCode.Enabled = true;
                this.dgv_showcarton.Visible = false;
                //txtCode_LostFocus(null, null);
                //dgvstockIn.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetlotinfoList(""));
                dgvstockIn.DataSource = mdtDgvstoreIn = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetReturnList("0", ""));//显示没列的列名
              //  this.dgv_showlotinfo.DataSource = null;
                this.dgv_showcarton.DataSource = mdtCloneCarton;

            }
            else if (cmbstatus.SelectedValue.ToString() == "4")
            {
            
                this.gbrdstock0.Text = "以批次方式";
                this.gbrdstock1.Text = "以栈板号方式";
                this.gbrdstock2.Text = "以卡通方式";
                this.gbrdstock3.Text = "以Tray方式";
                this.btnSubmit.Text = "确定";
                this.dgv_showcarton.Visible = false;

                this.gbrdstock3.Visible = true;
                txtCode.Enabled = false;
                dgvstockIn.DataSource = mdtDgvstoreIn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductNotLocId());
             //   this.dgv_showlotinfo.DataSource = null;
                this.dgv_showcarton.DataSource = mdtCloneCarton;
                dgvstockIn.Columns["TrayNo"].Visible = false;
            }
            else
            {
                dgvstockIn.DataSource = null;            
                this.dgv_showcarton.DataSource = mdtCloneCarton;
            }
            this.gbrdstock0.Checked = true;
        }

        private void cmbStockIn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //选择仓库储位编号
        private void btlocid_Click(object sender, EventArgs e)
        {
            SelectData sd = new SelectData(this,
                FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetWarehouseListInfo()));//.GetAlltStorehouseLoctionInfo()));
            sd.ShowDialog();
        }


        //刷入条码
        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCode.Text.Trim()) && e.KeyCode == Keys.Enter)
            {
                //btnSubmit_Click(null, null);
                string instatus=cmbstatus.SelectedValue.ToString();
                if (instatus == "1")
                {
                    if (gbrdstock0.Checked || gbrdstock1.Checked)
                    {
                        this.dgvstockIn.Visible = true;
                        this.dgv_showcarton.Visible = false;
                        this.bt_refresh.Enabled = true;
                        mdtDgvstoreIn = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetlotinfoByLotin(this.txtCode.Text.Trim()));
                        dgvstockIn.DataSource = ShowPallet(mdtDgvstoreIn);
                        mdtToSap= GetUploadSap(mdtDgvstoreIn);//整理传给SAP数据
                        if (mdtDgvstoreIn.Rows.Count==0)
                        {
                            MessageBox.Show("此单号没有数据或已经接收入库,如上传SAP失败,请再次上传数据...");
                        }
                    }
                    if (gbrdstock2.Checked)
                    {
                        this.dgvstockIn.Visible = false;
                        this.dgv_showcarton.Visible = true;
                        this.bt_refresh.Enabled = false;
                        dgv_showcarton.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetCarinfoByCarton(this.txtCode.Text.Trim()));
                    }
                }
                if (instatus=="2"||instatus=="3")
                {
                    txtCode_LostFocus(null, null);
                }
            }


        }
        #region 提交信息
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                #region 接收方式
                string RecStyle = "0";

                for (int i = 0; i < this.gbtype.Controls.Count; i++)//将它们放入容器里
                {
                    if (gbtype.Controls[i] is RadioButton)
                    {
                        RadioButton temp = (RadioButton)gbtype.Controls[i];
                        if (temp.Checked)//判断是否选中
                            RecStyle = temp.TabIndex.ToString();
                    }
                }


                #endregion
                #region 条件判定
                if (cmbstatus.SelectedValue.ToString() == "0")
                {
                    ShowPrgMsg("请选择入库类型！", mLogMsgType.Error); return;
                }

                if (string.IsNullOrEmpty(txtstore.Text.Trim()))
                {
                    ShowPrgMsg("请选择储位信息！", mLogMsgType.Error); return;
                }

                if ((dgvstockIn.Rows.Count < 1) && (cmbstatus.SelectedValue.ToString() == "1"))
                {
                    ShowPrgMsg("没有生产待入库信息！", mLogMsgType.Error); return;
                }
                if (txtCode.Text == "")
                {
                    ShowPrgMsg("入库条码信息不能为空！", mLogMsgType.Error); return;
                }

                if ((cmbstatus.SelectedValue.ToString() == "2" || cmbstatus.SelectedValue.ToString() == "3"))
                {
                    for (int i = 0; i < dgvstockIn.RowCount; i++)
                    {
                        string state = dgvstockIn.Rows[i].Cells["状态"].Value.ToString();
                        if (state == "待入库" || state == "已在库")
                        {
                            ShowPrgMsg("你的条码内容中包括非法的客退数据！", mLogMsgType.Error); return;
                        }
                    }

                }

                #endregion

                #region 接收入库
                string msg = "";
                if (RecStyle == "0" && cmbstatus.SelectedValue.ToString() == "1")
                {
                    msg = "确认将批次号为" + mdtToSap.Rows[0][0].ToString() + "待入库的产品全部接收入库？";
                }
                else if (RecStyle == "1" && cmbstatus.SelectedValue.ToString() == "1")
                {
                    msg = "确认将栈板号为" + txtCode.Text + "待入库的产品全部接收入库？";
                }
                else if (RecStyle == "2" && cmbstatus.SelectedValue.ToString() == "1")
                {
                    msg = "确认将卡通号为" + txtCode.Text + "待入库的产品全部接收入库？";
                }
                else if (RecStyle == "3" && cmbstatus.SelectedValue.ToString() == "1")
                {
                    msg = "确认将TrayNo为" + txtCode.Text + "待入库的产品全部接收入库？";
                }
                #region 上架
                else if (RecStyle == "0" && cmbstatus.SelectedValue.ToString() == "4")
                {
                    msg = "确认将批次号为" + txtCode.Text + "的入库产品全部分配到该库位？";
                }
                else if (RecStyle == "1" && cmbstatus.SelectedValue.ToString() == "4")
                {
                    msg = "确认将栈板号为" + txtCode.Text + "的入库产品全部分配到该库位？";
                }
                else if (RecStyle == "2" && cmbstatus.SelectedValue.ToString() == "4")
                {
                    msg = "确认将卡通号为" + txtCode.Text + "的入库产品全部分配到该库位？";
                }
                else if (RecStyle == "3" && cmbstatus.SelectedValue.ToString() == "4")
                {
                    msg = "确认将TrayNo为" + txtCode.Text + "的入库产品全部分配到该库位？";
                }
                #endregion
                else
                {
                    msg = "确认将编号为：" + txtCode.Text + "客退的产品全部接收入库？";
                }
                if (MessageBox.Show(msg, "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (cmbstatus.SelectedValue.ToString() == "4")//上架
                    {
                        string RecType = RecStyle;
                        string RecCode = txtCode.Text.Trim();
                        string storeId = txtstore.Text.Trim();
                        string locId = txtlocid.Text.Trim();
                        refWebtWarehouseWipTracking.Instance.UpdateStoreLocId(RecType, RecCode, storeId, locId);
                        ShowPrgMsg("添加库位成功!!", mLogMsgType.Outgoing);
                        dgvstockIn.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductNotLocId());
                        txtCode.Text = "";
                        txtlocid.Text = "";
                        txtstore.Text = "";
                    }
                    else
                    {
                        if (dgvstockIn.Rows.Count < 1 && dgv_showcarton.Rows.Count < 1)
                        {
                            ShowPrgMsg("不存在待入库数据,请确认...", mLogMsgType.Normal);
                            return;
                        }
                        string RecType = cmbstatus.SelectedValue.ToString();
                        string lotcode = mLotCode == null ? "" : mLotCode;//cmbStockIn.Text.Trim();
                        string RecCode = txtCode.Text.Trim();
                        string storeId = txtstore.Text.Trim();
                        string locId = txtlocid.Text.Trim();
                        string userid = mFrm.gUserInfo.userId;

                        string RES = string.Empty;
                        RES = refWebtWarehouseWipTracking.Instance.StockReceive(RecType, RecStyle, lotcode, RecCode, storeId, locId, userid);
                        if (RES == "OK")
                        {
                            ShowPrgMsg("数据成功接收入库！", mLogMsgType.Normal);
                            ShowPrgMsg("开始上传SAP....", mLogMsgType.Outgoing);
                            if (gbrdstock0.Checked)
                            {
                                UpLoadSAP(mdtToSap);
                                ShowPrgMsg("上传SAP完成", mLogMsgType.Outgoing);
                            }
                        }
                        else
                        {
                            ShowPrgMsg("接收入库失败: " + RES, mLogMsgType.Error);
                            MessageBox.Show(RES);
                        }
                        //this.cmbStockIn.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.Getlotinfo(0));
                        //cmbStockIn.DisplayMember = "lotin";
                        //cmbStockIn.ValueMember = "lotin";

                        if (cmbstatus.SelectedValue.ToString() == "1")
                        {
                            //加载待入库批次
                            //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetAllLotInfo());
                            //dt.DefaultView.Sort = "批次 asc";
                            //dgv_showlotinfo.DataSource = dt.DefaultView.ToTable();

                            this.dgv_showcarton.DataSource = mdtCloneCarton;
                            this.dgvstockIn.DataSource = mdtClonePallet;
                        }
                        else
                            dgvstockIn.DataSource = mdtDgvstoreIn = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetReturnList("0", ""));//显示列名
                        txtCode.Text = "";

                    }
                }
            }
            catch (Exception ex)
            {
                ShowPrgMsg("接收入库发生异常:" + ex.Message, mLogMsgType.Error);
            }
            finally
            {
                dgvstockIn.Rows.Clear();
            }

            #endregion
        }


        #endregion

        #region  待入库批次清单，选择要提交的信息
        private void dgvstockIn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.tb_selectedcount.Text = "";
            string RecStyle = "";
            if (gbrdstock0.Checked == true)
            {
                RecStyle = "0";
            }
            else if (gbrdstock1.Checked == true)
            {
                RecStyle = "1";
            }
            else if (gbrdstock2.Checked == true && cmbstatus.SelectedValue.ToString() != "1")
            {
                RecStyle = "2";
            }
            else if (gbrdstock3.Checked == true)
            {
                RecStyle = "3";
            }

            if (!dgvstockIn.RowCount.Equals(0))
            {
                int i = dgvstockIn.CurrentCell.RowIndex;
                string state = dgvstockIn.Rows[i].Cells["状态"].Value.ToString();
                if ((state == "待入库" && cmbstatus.SelectedValue.ToString() == "1") ||
                    (state == "已在库" && cmbstatus.SelectedValue.ToString() == "4"))
                {
                    if (RecStyle == "0")
                    {
                        txtCode.Text = dgvstockIn.Rows[i].Cells["入库批次"].Value.ToString();
                    }
                    else if (RecStyle == "1")
                    {
                        txtCode.Text = dgvstockIn.Rows[i].Cells["栈板号"].Value.ToString();
                    }
                    else if (RecStyle == "2")
                    {
                        txtCode.Text = dgvstockIn.Rows[i].Cells["卡通号"].Value.ToString();
                    }
                    else if (RecStyle == "3")
                    {
                        txtCode.Text = dgvstockIn.Rows[i].Cells["TrayNo"].Value.ToString();
                    }
                }
                else if (cmbstatus.SelectedValue.ToString() == "1")
                {
                    txtCode.Text = "";
                }
            }
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (cmbstatus.SelectedValue.ToString() == "1")
                {
                    this.tb_selectedcount.Text = dgvstockIn["总数量", e.RowIndex].Value.ToString();
                }
                else
                    this.tb_selectedcount.Text = dgvstockIn["数量", e.RowIndex].Value.ToString();
            }

        }
        #endregion
        #region radiobutton checkedchanged
        private void gbrdstock0_CheckedChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
        }

        private void gbrdstock1_CheckedChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
        }

        private void gbrdstock2_CheckedChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
        }

        private void gbrdstock3_CheckedChanged(object sender, EventArgs e)
        {
            txtCode.Text = "";
        }
        #endregion

        #region txtcode_lostfocus 事件
        private void txtCode_LostFocus(object sender, EventArgs e)
        {
            //判断号是否正确
            //Show 数据 到datagridview 
            string RecStyle = "0";

            for (int i = 0; i < this.gbtype.Controls.Count; i++)//将它们放入容器里
            {
                if (gbtype.Controls[i] is RadioButton)
                {
                    RadioButton temp = (RadioButton)gbtype.Controls[i];
                    if (temp.Checked)//判断是否选中
                        RecStyle = temp.TabIndex.ToString();
                }
            }
            if (cmbstatus.SelectedValue.ToString() == "2" || cmbstatus.SelectedValue.ToString() == "3")
            {
                dgvstockIn.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetReturnList(RecStyle, txtCode.Text.Trim()));

                if (dgvstockIn.RowCount == 0)
                {
                    txtCode.Text = "";
                    ShowPrgMsg("你的条码错误或从未经过仓库！", mLogMsgType.Error); return;
                }
            }
        }

        private void txtCode_MouseLeave(object sender, EventArgs e)
        {
            //txtCode_LostFocus(null, null);
        }
        #endregion
        private void dgvstockIn_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex != -1 && e.RowIndex != -1 &&
                //    (cmbstatus.SelectedValue.ToString() == "1" || cmbstatus.SelectedValue.ToString() == "4"))
                //{
                //    string flag = "";
                //    string ColumnValue = "";
                //    string ColumnName = dgvstockIn.Columns[e.ColumnIndex].Name;
                //    if (ColumnName == "总数量")
                //    {
                //        flag = "palletnumber";
                //        ColumnValue = dgvstockIn["栈板号", e.RowIndex].Value.ToString();

                //        SerialInfo si = new SerialInfo(this,
                //        FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductAllSerial(flag, ColumnValue)),
                //        this.dgvstockIn["料号", e.RowIndex].Value.ToString(), null,
                //        this.dgvstockIn["栈板号", e.RowIndex].Value.ToString(),
                //        this.dgvstockIn["入库批次", e.RowIndex].Value.ToString());
                //        si.ShowDialog();
                //    }
                //    if (ColumnName == "箱数量" || ColumnName == "栈板号")
                //    {
                //        this.dgvstockIn.Visible = false;
                //        this.dgv_showcarton.Visible = true;
                //        dgv_showcarton.DataSource = FrmBLL.publicfuntion.getNewTable(mdtDgvstoreIn, string.Format("栈板号='{0}'", dgvstockIn["栈板号", e.RowIndex].Value.ToString()));

                //    }
                //}
            }
            catch (Exception ex)
            {
                ShowPrgMsg(ex.Message, mLogMsgType.Error);
            }
        }

        private void dgvstockIn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string bl = dgvstockIn.Visible.ToString();
            if (bl == "True" && dgvstockIn.Rows.Count > 0 && cmbstatus.SelectedValue.ToString() == "1")
            {
                this.tb_totalcount.Text = "";
                int sum = 0;
                for (int i = 0; i < dgvstockIn.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(dgvstockIn.Rows[i].Cells["总数量"].Value.ToString());
                }
                this.tb_totalcount.Text = sum.ToString();
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbboxnumber.Text))
            {
                if (cmbstatus.SelectedValue.ToString() == "1")
                {
                    this.dgvstockIn.Visible = true;
                    this.dgv_showcarton.Visible = false;
                    this.dgvstockIn.DataSource = ShowPallet(mdtDgvstoreIn);
                }
                else
                    this.dgvstockIn.DataSource = mdtDgvstoreIn;
                return;
            }
            if (string.IsNullOrEmpty(this.comboBoxEx1.Text))
            {
                MessageBoxEx.Show("没有选择条件类型!!");
                return;
            }
            if (cmbstatus.SelectedValue.ToString() == "1")
            {
                switch (this.comboBoxEx1.SelectedIndex)
                {
                    case 0:
                        //卡通号
                        this.dgvstockIn.Visible = false;
                        this.dgv_showcarton.Visible = true;
                        this.dgv_showcarton.DataSource = FrmBLL.publicfuntion.getNewTable(mdtDgvstoreIn,
                             string.Format("卡通号='{0}'", this.tbboxnumber.Text.Trim()));
                        break;
                    case 1:
                        //栈板号
                        this.dgvstockIn.Visible = true;
                        this.dgv_showcarton.Visible = false;
                        this.dgvstockIn.DataSource = FrmBLL.publicfuntion.getNewTable(ShowPallet(mdtDgvstoreIn),
                            string.Format("栈板号='{0}'", this.tbboxnumber.Text.Trim()));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (this.comboBoxEx1.SelectedIndex)
                {
                    case 0:
                        //卡通号
                        this.dgvstockIn.DataSource = FrmBLL.publicfuntion.getNewTable(mdtDgvstoreIn,
                             string.Format("卡通号='{0}'", this.tbboxnumber.Text.Trim()));
                        break;
                    case 1:
                        //栈板号
                        this.dgvstockIn.DataSource = FrmBLL.publicfuntion.getNewTable(mdtDgvstoreIn,
                            string.Format("栈板号='{0}'", this.tbboxnumber.Text.Trim()));
                        break;
                    default:
                        break;
                }
            }

        }
        /// <summary>
        /// 显示栈板数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable ShowPallet(DataTable dt)
        {
            DataTable mdt = new DataTable();
            mdt.Columns.Add("入库批次");
            mdt.Columns.Add("工单号");
            mdt.Columns.Add("料号");
            mdt.Columns.Add("型号");
            mdt.Columns.Add("栈板号");
            mdt.Columns.Add("箱数量");
            mdt.Columns.Add("总数量");
            mdt.Columns.Add("状态");

            mdtClonePallet = mdt.Clone();
            mdtCloneCarton = dt.Clone();
            DataTable dtTemp = dt.DefaultView.ToTable(true, "栈板号");
            int i = 0;
            foreach (DataRow dr in dtTemp.Rows)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                DataRow[] arrDr = null;

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string sql = string.Format("栈板号='{0}'", dr["栈板号"].ToString());
                    arrDr = dt.Select(sql);
                    if (dt.Columns[j].ColumnName.ToString() == "入库批次" || dt.Columns[j].ColumnName.ToString() == "工单号"
                        || dt.Columns[j].ColumnName.ToString() == "料号" || dt.Columns[j].ColumnName.ToString() == "状态"
                        || dt.Columns[j].ColumnName.ToString() == "栈板号" || dt.Columns[j].ColumnName.ToString() == "型号")
                    {
                        dic.Add(dt.Columns[j].ColumnName, arrDr[0][dt.Columns[j].ColumnName].ToString());
                        continue;
                    }
                    if (dt.Columns[j].ColumnName.ToString() == "数量")
                    {
                        int palletqty = 0;
                        for (int m = 0; m < arrDr.Length; m++)
                        {
                            palletqty += int.Parse(arrDr[m]["数量"].ToString());
                        }
                        dic.Add("总数量", palletqty.ToString());
                        continue;
                    }
                    if (dt.Columns[j].ColumnName == "卡通号")
                    {
                        dic.Add("箱数量", arrDr.Length.ToString());
                        continue;
                    }
                    if (dt.Columns[j].ColumnName.ToUpper() == "TRAYNO")
                    {
                        continue;
                    }
                }
                mdt.Rows.Add("", "", "", "", "", "", "", "");//建一行
                foreach (string str in dic.Keys)
                {
                    mdt.Rows[i][str] = dic[str];
                }
                i++;
                continue;
            }
            return mdt;
        }


        private void bt_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgvstockIn.Visible = true;
                this.dgv_showcarton.Visible = false;
                dgvstockIn_DataBindingComplete(null, null);
            }
            catch
            { }
        }

        private void dgv_showcarton_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex != -1 && e.RowIndex != -1 &&
            //    (cmbstatus.SelectedValue.ToString() == "1" || cmbstatus.SelectedValue.ToString() == "4"))
            //{
            //    string flag = "cartonnumber";
            //    string ColumnValue = dgv_showcarton["卡通号", e.RowIndex].Value.ToString();
            //    DataTable dt =FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductAllSerial(flag, ColumnValue));
            //    if (dt.Rows.Count > 0)
            //    {
            //        DataView dv = dt.DefaultView;
            //        dv.Sort = "ESN ASC";
            //        DataTable dTemp = dv.ToTable();
            //        SerialInfo si = new SerialInfo(this, FrmBLL.DataTableCrosstab.DataTableCross(dTemp, 4),
            //        this.dgv_showcarton["料号", e.RowIndex].Value.ToString(),
            //        this.dgv_showcarton["卡通号", e.RowIndex].Value.ToString(),
            //        this.dgv_showcarton["栈板号", e.RowIndex].Value.ToString(),
            //        this.dgv_showcarton["入库批次", e.RowIndex].Value.ToString());
            //        si.ShowDialog();
            //    }
            //    else
            //        MessageBox.Show("没有数据");
            //}
        }

        private void dgv_showcarton_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.tb_selectedcount.Text = "";
            string RecStyle = "";
            if (gbrdstock0.Checked == true)
            {
                RecStyle = "0";
            }
            else if (gbrdstock1.Checked == true)
            {
                RecStyle = "1";
            }
            else if (gbrdstock2.Checked == true)
            {
                RecStyle = "2";
            }
            else if (gbrdstock3.Checked == true)
            {
                RecStyle = "3";
            }

            if (!dgv_showcarton.RowCount.Equals(0))
            {
                int i = dgv_showcarton.CurrentCell.RowIndex;
                string state = dgv_showcarton.Rows[i].Cells["状态"].Value.ToString();
                if ((state == "待入库" && cmbstatus.SelectedValue.ToString() == "1") ||
                    (state == "已在库" && cmbstatus.SelectedValue.ToString() == "4"))
                {
                    if (RecStyle == "0")
                    {
                        txtCode.Text = dgv_showcarton.Rows[i].Cells["入库批次"].Value.ToString();
                    }
                    else if (RecStyle == "1")
                    {
                        txtCode.Text = dgv_showcarton.Rows[i].Cells["栈板号"].Value.ToString();
                    }
                    else if (RecStyle == "2")
                    {
                        txtCode.Text = dgv_showcarton.Rows[i].Cells["卡通号"].Value.ToString();
                    }
                    else if (RecStyle == "3")
                    {
                        txtCode.Text = dgv_showcarton.Rows[i].Cells["TrayNo"].Value.ToString();
                    }
                }
                else if (cmbstatus.SelectedValue.ToString() == "1")
                {
                    txtCode.Text = "";
                }
            }
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                this.tb_selectedcount.Text = dgv_showcarton["数量", e.RowIndex].Value.ToString();
            }
        }

        private void dgv_showcarton_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string bl = dgv_showcarton.Visible.ToString();
            if (bl == "True" && dgv_showcarton.Rows.Count > 0 && cmbstatus.SelectedValue.ToString() == "1")
            {
                this.tb_totalcount.Text = "";
                int sum = 0;
                for (int i = 0; i < dgv_showcarton.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(dgv_showcarton.Rows[i].Cells["数量"].Value.ToString());
                }
                this.tb_totalcount.Text = sum.ToString();
            }
        }

        private string mLotCode = null;
        private void dgv_showlotinfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex != -1 && e.ColumnIndex != -1)
            //{
            //    this.dgvstockIn.Visible = true;
            //    this.dgv_showcarton.Visible = false;
            //    this.bt_refresh.Enabled = true;
            //    if (dgv_showlotinfo.Rows.Count > 0) //存在未入库批次
            //    {
            //        mLotCode = this.dgv_showlotinfo["批次", e.RowIndex].Value.ToString();
            //        mdtDgvstoreIn = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetlotinfoByLotin(this.dgv_showlotinfo["批次", e.RowIndex].Value.ToString()));
            //        dgvstockIn.DataSource = ShowPallet(mdtDgvstoreIn);
            //    }
            //    else
            //    {
            //        dgvstockIn.DataSource = mdtClonePallet;
            //        //mdtDgvstoreIn = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.GetlotinfoByLotin(""));
            //        //dgvstockIn.DataSource = ShowPallet(mdtDgvstoreIn);
            //    }
            //}
        }

        private void dgv_showcarton_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
                        dgv_showcarton.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                       dgv_showcarton.RowHeadersDefaultCellStyle.Font, rectangle,
                       dgv_showcarton.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgvstockIn_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
                        dgv_showcarton.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                       dgv_showcarton.RowHeadersDefaultCellStyle.Font, rectangle,
                       dgv_showcarton.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void tb_inputstock_KeyDown(object sender, KeyEventArgs e)
        {
           
        }


        private DataTable GetUploadSap(DataTable dt)
        {
            DataTable dtLwolf = new DataTable();
            dtLwolf.Columns.AddRange(new DataColumn[] { new DataColumn("入库批次", typeof(string)),
                new DataColumn("工单号", typeof(string)),
                new DataColumn("料号", typeof(string)),
                new DataColumn("型号", typeof(string)),              
                new DataColumn("总数量", typeof(int))});


          
            DataTable dtName = dt.DefaultView.ToTable(true, "入库批次", "工单号", "料号", "型号");
            for (int i = 0; i < dtName.Rows.Count; i++)
            {
                //"工单号='" + dtName.Rows[i][0] + "',"
                DataRow[] rows = dt.Select(string.Format("入库批次='{0}' and 工单号='{1}' and 料号='{2}' and 型号='{3}'", dtName.Rows[i]["入库批次"].ToString(), dtName.Rows[i]["工单号"].ToString(), dtName.Rows[i]["料号"].ToString(), dtName.Rows[i]["型号"].ToString()));
                //temp用来存储筛选出来的数据
                DataTable temp = dt.Clone();
                foreach (DataRow row in rows)
                {
                    temp.Rows.Add(row.ItemArray);
                }

                DataRow dr = dtLwolf.NewRow();
                dr[0] = dtName.Rows[i][0].ToString();
                dr[1] = dtName.Rows[i][1].ToString();
                dr[2] = dtName.Rows[i][2].ToString();
                dr[3] = dtName.Rows[i][3].ToString();
                dr[4] = temp.Compute("sum(数量)", "");
                dtLwolf.Rows.Add(dr);
            }
            foreach (DataRow dr in dtLwolf.Rows)
            {
                ShowPrgMsg(string.Format("入库批次[{0}],工单号[{1}],料号[{2}],型号[{3}],数量[{4}]",dr[0].ToString(),
                    dr[1].ToString(),dr[2].ToString(),dr[3].ToString(),dr[4].ToString()), mLogMsgType.Incoming);
            }
            return dtLwolf;
        }

        private void UpLoadSAP(DataTable dt)
        {
            try
            {
                foreach (DataRow dr in dt.Rows)
                {

                    string SFCMSG = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.InsertZ_Whs_SAP_BackFlush(new WebServices.tZ_Whs_SAP_BackFlush.Z_WHS_SAP_BACKFLUSHTable()
                    {
                        LOTIN = dr[0].ToString(),
                        WOID = dr[1].ToString(),
                        PARTNUMBER = dr[2].ToString(),
                        PRODUCTNAME = dr[3].ToString(),
                        LOTIN_QTY = Convert.ToInt32(dr[4].ToString()),
                        MOVE_TYPE = "101",
                        PLANT = "2100",
                        UPLOAD_FLAG = "N"
                    });
                    string sSAP_MSG = string.Format("入库编号:{0},工单:{1},料号:{2},型号:{3},数量:{4},MSG:{5}", dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), SFCMSG);
                    ShowPrgMsg("存入SFIS成功: " + sSAP_MSG, mLogMsgType.Incoming);

                    /* string[] LsMsg = RefWebService_BLL.refWebSapConnector.Instance.Get_Z_RFC_AUFNR_MIGO(new WebServices.SapConnector.Z_RFC_AUFNR_MIGO()
                     {
                         woId = dr[1].ToString(),
                         PartNumber = dr[2].ToString(),
                         QTY = Convert.ToInt32(dr[4].ToString()),
                         EMP_NAME = mFrm.gUserInfo.username,
                         EMP_NO = mFrm.gUserInfo.userId
                            
                             
                     });
                            
                      //   "02", null, null, "1", dr[1].ToString(), dr[2].ToString(),Convert.ToInt32(dr[4].ToString()) , "F", "2100", "101");
                     if (LsMsg.Length < 5)
                     {
                         ShowPrgMsg(LsMsg[0].ToString(), mLogMsgType.Error);
                         string SFCMSG = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.InsertZ_Whs_SAP_BackFlush(new WebServices.tZ_Whs_SAP_BackFlush.Z_WHS_SAP_BACKFLUSHTable()
                         {
                             LOTIN = dr[0].ToString(),
                             WOID = dr[1].ToString(),
                             PARTNUMBER = dr[2].ToString(),
                             PRODUCTNAME = dr[3].ToString(),
                             LOTIN_QTY = Convert.ToInt32(dr[4].ToString()),
                             MOVE_TYPE = "101",
                             PLANT = "2100",
                             UPLOAD_FLAG = "N"
                         });
                     }
                     else
                     {
                         string SAP_STOCKNO = LsMsg[0].ToString();
                         string SAP_TYPE = LsMsg[1].ToString();
                         string SAP_E_ID = LsMsg[2].ToString();
                         string SAP_E_NUM = LsMsg[3].ToString();
                         string SAP_MSG = LsMsg[4].ToString();
                         if (SAP_TYPE.ToUpper() == "S")
                         {
                             string InserRes = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.InsertZ_Whs_SAP_BackFlush(new WebServices.tZ_Whs_SAP_BackFlush.Z_WHS_SAP_BACKFLUSHTable()
                             {
                                 LOTIN = dr[0].ToString(),
                                 WOID = dr[1].ToString(),
                                 PARTNUMBER = dr[2].ToString(),
                                 PRODUCTNAME = dr[3].ToString(),
                                 LOTIN_QTY = Convert.ToInt32(dr[4].ToString()),
                                 MOVE_TYPE = "101",
                                 PLANT = "2100",
                                 UPLOAD_FLAG = "Y"

                             });
                             ShowPrgMsg(string.Format("上传SAP成功,SAP单号[{0}],工单[{1}],料号[{2}],产品型号[{3}],数量[{4}]->" + InserRes, SAP_STOCKNO, dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString()), mLogMsgType.Normal);
                         }
                         else
                         {
                             string SFCMSG = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.InsertZ_Whs_SAP_BackFlush(new WebServices.tZ_Whs_SAP_BackFlush.Z_WHS_SAP_BACKFLUSHTable()
                             {
                                 LOTIN = dr[0].ToString(),
                                 WOID = dr[1].ToString(),
                                 PARTNUMBER = dr[2].ToString(),
                                 PRODUCTNAME = dr[3].ToString(),
                                 LOTIN_QTY = Convert.ToInt32(dr[4].ToString()),
                                 MOVE_TYPE = "101",
                                 PLANT = "2100",
                                 UPLOAD_FLAG = "N"
                             });
                             ShowPrgMsg("Insert SFC " + SFCMSG, SFCMSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error);
                             string sSAP_MSG = string.Format("入库编号:{0},工单:{1},料号:{2},型号:{3},数量:{4},MSG:{5}", dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), SAP_TYPE + "-" + SAP_E_ID + "-" + SAP_E_NUM + "-" + SAP_MSG);
                             ShowPrgMsg("SAP:" + sSAP_MSG, SAP_MSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error);

                             SFCMSG = RefWebService_BLL.refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG()
                             {
                                 userId = "UPLOAD",
                                 prg_name = "UPSTOCKIN",
                                 action_type = dr[0].ToString(),
                                 action_desc = sSAP_MSG
                             });
                             ShowPrgMsg("SFC Log:" + SFCMSG, SFCMSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error);
                         }
                     }*/
                }

            }
            catch (Exception ex)
            {
                //string SFCMSG = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.InsertZ_Whs_SAP_BackFlush(new WebServices.tZ_Whs_SAP_BackFlush.Z_WHS_SAP_BACKFLUSHTable()
                //{
                //    LOTIN = dr[0].ToString(),
                //    WOID = dr[1].ToString(),
                //    PARTNUMBER = dr[2].ToString(),
                //    PRODUCTNAME = dr[3].ToString(),
                //    LOTIN_QTY = Convert.ToInt32(dr[4].ToString()),
                //    MOVE_TYPE = "101",
                //    PLANT = "2100",
                //    UPLOAD_FLAG = "N"
                //});
                ShowPrgMsg("上传SAP异常" + ex.Message, mLogMsgType.Error);
            }
            UpLoadStockIn(dt.Rows[0][0].ToString());
        }

        #region 入库抛SAP
        private void UpLoadStockIn(string lotin)
        {       
            ShowPrgMsg("上传SAP.....", mLogMsgType.Normal);
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.GetUpload_whs_Sap_Info("101"));

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["LOTIN"].ToString() == lotin)
                    UpLoadSAP_StockIn(dr["LOTIN"].ToString(), dr["woId"].ToString(), dr["PARTNUMBER"].ToString(), dr["PRODUCTNAME"].ToString(), Convert.ToInt32(dr["LOTIN_QTY"].ToString()), dr["ROWID"].ToString());
                }
            }
            ShowPrgMsg("上传SAP结束.....", mLogMsgType.Normal);
         
        }
        private void UpLoadSAP_StockIn(string SFCNmuber, string woId, string PartNo, string Product, int QTY, string sRowid)
        {
            string[] LsMsg = RefWebService_BLL.refWebSapConnector.Instance.Get_Z_RFC_AUFNR_MIGO(new WebServices.SapConnector.Z_RFC_AUFNR_MIGO()
            {
                PartNumber = PartNo,
                woId = woId,
                QTY = QTY,
                EMP_NO = mFrm.gUserInfo.userId,
                EMP_NAME = mFrm.gUserInfo.username
            });      
            if (LsMsg.Length < 5)
            {
                ShowPrgMsg(LsMsg[0].ToString(), mLogMsgType.Error);
            }
            else
            {
                string SAP_STOCKNO = LsMsg[0].ToString();
                string SAP_TYPE = LsMsg[1].ToString();
                string SAP_E_ID = LsMsg[2].ToString();
                string SAP_E_NUM = LsMsg[3].ToString();
                string SAP_MSG = LsMsg[4].ToString();
                if (SAP_TYPE.ToUpper() == "S")
                {
                    string sRes = RefWebService_BLL.refWebtZ_Whs_SAP_BackFlush.Instance.Update_Z_Whs_SAP_BackFlush_Flag(sRowid, "Y");
                    ShowPrgMsg(string.Format("上传SAP成功,SAP单号[{0}],入库单[{6}],工单[{1}],料号[{2}],产品型号[{3}],数量[{4}],时间[{5}] ->" + sRes, SAP_STOCKNO, woId, PartNo, Product, QTY.ToString(), DateTime.Now.ToString(), SFCNmuber), mLogMsgType.Normal);
                }
                else
                {
                    string sSAP_MSG = string.Format("工单:{0},料号:{1},型号:{2},数量:{3},MSG:{4},时间[{5}]", woId, PartNo, Product, QTY.ToString(), SAP_TYPE + "-" + SAP_E_ID + "-" + SAP_E_NUM + "-" + SAP_MSG, DateTime.Now.ToString());
                    ShowPrgMsg("SAP:" + sSAP_MSG, SAP_MSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error);

                    string SFCMSG = RefWebService_BLL.refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG()
                    {
                        userId = "UPLOAD",
                        prg_name = "UPSTOCKIN",
                        action_type = woId,
                        action_desc = sSAP_MSG
                    });
                    ShowPrgMsg("SFC Log:" + SFCMSG, SFCMSG == "OK" ? mLogMsgType.Incoming : mLogMsgType.Error);
                }
            }
        }
        #endregion
    }
}


