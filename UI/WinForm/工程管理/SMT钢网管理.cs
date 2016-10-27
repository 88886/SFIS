using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevComponents.DotNetBar;
using RefWebService_BLL;
//using System.Runtime.InteropServices;
using LabelManager2;
using System.Collections;


namespace SFIS_V2
{
    public partial class SMTgangwangManage : Office2007Form// Form
    {
        public SMTgangwangManage(MainParent frm)
        {
            InitializeComponent();
            this.mFrm = frm;
        }

        MainParent mFrm;
        private delegate void delegateshowdata();
        private delegate void delegateopenlabel(string filename);
        delegateshowdata dsd;
        private ApplicationClass lbl;
        bool isprinter = true;
        private enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error } //文字输出类型
        private Color[] LogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };//文字颜色
        delegateopenlabel dpl;
        IAsyncResult iasyncresult;
        private string labelfilepatch = System.IO.Directory.GetCurrentDirectory() + "\\Print\\FEIXUN_LOT.lab";
        Dictionary<string, object> dic = null;

        private void eventopenlablefile(string filename)
        {
            if (!File.Exists(filename))  //判断条码文件是否存在
            {
                try
                {
                    mFrm.ShowPrgMsg("条码档没有找到,正在从Ftp下载..", MainParent.MsgType.Warning);
                    FrmBLL.Ftp_MyFtp ftp = new FrmBLL.Ftp_MyFtp();
                    ftp.DownloadFile("FEIXUN_LOT.Lab", System.AppDomain.CurrentDomain.BaseDirectory + "Print", "FEIXUN_LOT.Lab");
                    mFrm.ShowPrgMsg("下载完成", MainParent.MsgType.Outgoing);
                }
                catch
                {
                    this.isprinter = false;
                    return;
                }

                //mFrm.ShowPrgMsg("条码档没有找到,请确认当前目录是否存在 FEIXUN_LOT.lab", MainParent.MsgType.Error);
                //this.isprinter = false;
            }
            this.isprinter = true;
            lbl = new ApplicationClass();
            lbl.Documents.Open(labelfilepatch, false);// 调用设计好的label文件
        }
        private void loaddata()
        {
            try
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.Getgangwang());
                ShowData(dt);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

        }

        private void tb_kpnumber_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_kpnumber.Text))
            {
                if (this.tb_kpnumber.Text.Trim().Split('-').Length != 3)
                {
                    MessageBoxEx.Show("编号格式不符\n\n正确格式:产品料号-版本-序号\n\n请填写正确的编号格式", "提示");
                    this.tb_kpnumber.SelectAll();
                    this.tb_kpnumber.Focus();
                }
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.QueryKpnumber());
                foreach (DataRow dr in dt.Rows)
                {
                    if (this.tb_kpnumber.Text.Trim() == dr["kpnumber"].ToString())
                    {
                        mFrm.ShowPrgMsg("该料号已经存在,请确认!", MainParent.MsgType.Warning);
                        this.tb_kpnumber.Text = "";
                        this.tb_kpnumber.Focus();
                    }
                }

            }
        }
        /// <summary>
        /// 只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_vendercode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void tb_vendercode_Leave(object sender, EventArgs e)
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebVenderInfo.Instance.QueryVender(null));
            foreach (DataRow dr in dt.Rows)
            {
                if (this.tb_vendercode.Text.Trim() == dr["venderId"].ToString())
                {
                    this.tb_kpnumber.Focus();
                    this.mFrm.ShowPrgMsg("该厂商编号已存在,请重新输入...", MainParent.MsgType.Warning);
                    this.tb_vendercode.SelectAll();
                    this.tb_vendercode.Focus();
                    return;
                }
            }
        }

        private void SMTgangwangManage_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
            {
                IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                dic = new Dictionary<string, object>();
                dic.Add("PROGID", this.Name);
                dic.Add("PROGNAME", this.Text);
                dic.Add("PROGDESC", this.Text);
                FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
            }
            #endregion

            dsd = new delegateshowdata(loaddata);
            dsd.BeginInvoke(null, null);

            dpl = new delegateopenlabel(eventopenlablefile);
            iasyncresult = dpl.BeginInvoke(labelfilepatch, null, null);
            //this.tb_kpnumber.Text = "料号-版本-序号";

        }
        private void cb_store_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cb_local.Text = "";
            this.cb_local.Items.Clear();
            //foreach (DataRow dr in FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetWarehouseLoctionListBystorehouseId(this.cb_store.Text.Trim())).Rows)
            foreach (DataRow dr in FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetNotUsedLocId(this.cb_store.Text.Trim())).Rows)
            {
                this.cb_local.Items.Add(dr["locId"].ToString());
            }
        }

        private void cb_store_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cb_store.Text))
                return;
            cb_store_SelectedIndexChanged(sender, e);
        }
        private void bt_print_Click(object sender, EventArgs e)
        {
            if (iasyncresult != null && !iasyncresult.IsCompleted)
            {
                mFrm.ShowPrgMsg("打印模板还没有初始化成功,请稍后..", MainParent.MsgType.Warning);
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(this.tb_kpnumber.Text.Trim()) && string.IsNullOrEmpty(this.tb_vendercode.Text.Trim()) && string.IsNullOrEmpty(this.cb_store.Text) && string.IsNullOrEmpty(this.cb_local.Text))
                    GridViewPrint();
            }
            catch (Exception exp)
            {
                this.mFrm.ShowPrgMsg(exp.Message, MainParent.MsgType.Error);
            }
        }
        private void bt_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.tb_kpnumber.Text.Trim()))
                    throw new Exception("请输入产品料号!");
                if (string.IsNullOrEmpty(this.tb_vendercode.Text.Trim()))
                    throw new Exception("请输入厂商编号!");
                if (string.IsNullOrEmpty(this.cb_store.Text) || string.IsNullOrEmpty(this.cb_local.Text))
                    throw new Exception("请选择仓库或库位!");
                if (string.IsNullOrEmpty(this.rtb_remark.Text.Trim()))
                    throw new Exception("备注不能为空,且前两项是:连板数-板属性");
                if (string.IsNullOrEmpty(this.cb_vcname.Text.Trim()))
                    throw new Exception("请填写厂商名称!");

                 dic = new Dictionary<string, object>();
                dic.Add("VENDERID", tb_vendercode.Text.Trim());
                dic.Add("VENDERNAME", cb_vcname.Text.Trim());
                dic.Add("VENDERNAME2", "NA");
                refWebVenderInfo.Instance.InsertVenderInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));
                //refWebVenderInfo.Instance.InsertVenderInfo(new WebServices.tVenderInfo.tWenderInfo()
                //{
                //    VenderId = this.tb_vendercode.Text.Trim(),
                //    VenderName = this.cb_vcname.Text.Trim(),
                //    VenderName2 = "NA"
                //});
                string trsn;
                DataTable dtable = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.Getgangwang());
                if (dtable.Rows.Count < 1)
                    trsn = string.Format("999999" + "{0:D7}", 0);
                else
                {
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetMaxTrsn());
                    trsn = (UInt64.Parse(dt.Rows[0]["trsn"].ToString()) + 1).ToString();
                }

                // 把当前文本框值赋给实体对象
                tPartStorehousehad partwaremodel = new tPartStorehousehad();
                partwaremodel.VenderCode = this.tb_vendercode.Text.Trim();
                partwaremodel.DateCode = System.DateTime.Now.ToString("yyyyMMdd");
                partwaremodel.LotId = System.DateTime.Now.ToString("yyyyMMdd");
                partwaremodel.KpNumber = this.tb_kpnumber.Text.Trim();
                partwaremodel.LocId = this.cb_local.Text;
                partwaremodel.storehouseId = this.cb_store.Text;
                partwaremodel.status = 0;
                partwaremodel.QTY = 1;
                partwaremodel.recdate = System.DateTime.Now.ToString();
                partwaremodel.Tr_Sn = trsn;
                partwaremodel.UserId = mFrm.gUserInfo.userId;
                partwaremodel.OUTQTY = 0;
                partwaremodel.Remark = this.rtb_remark.Text.Trim();
                partwaremodel.FLAG = "DEPART";

                dic = new Dictionary<string, object>();
                dic.Add("VENDERCODE", this.tb_vendercode.Text.Trim());
                dic.Add("DATECODE", System.DateTime.Now.ToString("yyyyMMdd"));
                dic.Add("LOTID", System.DateTime.Now.ToString("yyyyMMdd"));
                dic.Add("KPNUMBER", this.tb_kpnumber.Text.Trim());
                dic.Add("LOCID", this.cb_local.Text.Trim());
                dic.Add("STOREHOUSEID", this.cb_store.Text.Trim());
                dic.Add("STATUS", 0);
                dic.Add("QTY", 1);
                dic.Add("RECDATE", System.DateTime.Now.ToString());
                dic.Add("TRSN", trsn);
                dic.Add("USERID", mFrm.gUserInfo.userId);
                dic.Add("OUTQTY", 0);
                dic.Add("REMARK", rtb_remark.Text.Trim());
                dic.Add("FLAG", "DEPART");


                if (cb_check.Checked)
                {
                    if (iasyncresult != null && !iasyncresult.IsCompleted)
                    {
                        mFrm.ShowPrgMsg("打印模板还没有初始化成功,请稍后..", MainParent.MsgType.Warning);
                        return;
                    }
                    //入库之后即刻打印条码
                    if (!string.IsNullOrEmpty(this.tb_kpnumber.Text.Trim())
                        && (!string.IsNullOrEmpty(this.tb_vendercode.Text.Trim()))
                        && (!string.IsNullOrEmpty(this.cb_store.Text))
                        && (!string.IsNullOrEmpty(this.cb_local.Text)))
                    {
                        refWebtPartStorehousehad.Instance.InsertPartStorehousehad(FrmBLL.ReleaseData.DictionaryToJson(dic));
                        this.ShowMsg(LogMsgType.Outgoing, string.Format("钢网{0}入库成功!", partwaremodel.Tr_Sn));
                        PrinterLable(partwaremodel.Remark, partwaremodel.Tr_Sn, partwaremodel);
                        ClearInfo();
                        ShowData(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.Getgangwang()));

                    }
                    //查询datagridview中，有选中的选项则打印
                    GridViewPrint();

                }
                //仅执行入库操作
                else
                {
                    string sss = refWebtPartStorehousehad.Instance.InsertPartStorehousehad(FrmBLL.ReleaseData.DictionaryToJson(dic));
                    this.ShowMsg(LogMsgType.Outgoing, string.Format("钢网{0}入库成功!", partwaremodel.Tr_Sn + sss));
                    ClearInfo();
                    ShowData(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.Getgangwang()));
                }
            }
            catch (Exception exo)
            {
                this.mFrm.ShowPrgMsg(exo.Message, MainParent.MsgType.Error);
            }
        }
        /// <summary>
        /// 标签打印
        /// </summary>
        /// <param name="kpdesc">描述</param>
        /// <param name="trsn">唯一序列号</param>
        /// <param name="partstorehousehadmodel">记录物料信息的实体</param>
        private void PrinterLable(string kpdesc, string trsn, tPartStorehousehad partstorehousehadmodel)
        {
            if (this.isprinter)
            {
                Document doc = lbl.ActiveDocument;
                doc.Variables.FormVariables.Item("PN").Value = partstorehousehadmodel.KpNumber;// edt_pn.Text.Trim(); //给参数传值
                doc.Variables.FormVariables.Item("VENDER_CODE").Value = partstorehousehadmodel.VenderCode;// edt_vc.Text.Trim();
                doc.Variables.FormVariables.Item("DATECODE").Value = partstorehousehadmodel.DateCode;// edt_dc.Text.Trim();
                doc.Variables.FormVariables.Item("LOT_ID").Value = partstorehousehadmodel.LotId;// edt_lot.Text.Trim();
                doc.Variables.FormVariables.Item("UNIT_SIZE").Value = partstorehousehadmodel.QTY.ToString();// edt_qty.Text.Trim();
                doc.Variables.FormVariables.Item("EMP_NO").Value = mFrm.gUserInfo.userId;
                doc.Variables.FormVariables.Item("REMARK").Value = kpdesc;
                doc.Variables.FormVariables.Item("TR_SN").Value = trsn;
                doc.Variables.FormVariables.Item("STORLOC").Value = partstorehousehadmodel.storehouseId + "-" + partstorehousehadmodel.LocId;
                doc.PrintDocument(1);//打印
                this.ShowMsg(LogMsgType.Outgoing, string.Format("编号:{1}; 料号:{0}; 打印成功", partstorehousehadmodel.KpNumber, trsn));
            }
            else
            {
                throw new Exception("没有发现条码文档..");
            }
        }
        /// <summary>
        /// 打印datagridview选中行的条码
        /// </summary>
        private void GridViewPrint()
        {
            this.dataGridView1.Invoke(new EventHandler(delegate
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["Select"].Value))
                    {
                        string trsn = dataGridView1.Rows[i].Cells["钢网编号"].Value.ToString();
                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangInfoByTrsn(trsn));
                        PrinterLable(dt.Rows[0]["remark"].ToString(), trsn, new  tPartStorehousehad()
                        {

                            DateCode = dt.Rows[0]["datecode"].ToString(),
                            LotId = dt.Rows[0]["lotId"].ToString(),
                            KpNumber = dt.Rows[0]["kpnumber"].ToString(),
                            LocId = dt.Rows[0]["LocId"].ToString(),
                            storehouseId = dt.Rows[0]["storehouseId"].ToString(),
                            status = 0,
                            QTY = 1,
                            VenderCode = dt.Rows[0]["vendercode"].ToString(),
                            recdate = dt.Rows[0]["recdate"].ToString(),
                            UserId = dt.Rows[0]["userId"].ToString(),
                            Remark = dt.Rows[0]["remark"].ToString(),
                            FLAG = "DEPART"
                        });
                        this.ShowMsg(LogMsgType.Outgoing, string.Format("编号:{1}; 料号:{0}; 打印成功",
                    dt.Rows[0]["kpnumber"].ToString(), trsn));
                    }
                }
            }));

        }
        private void ShowMsg(LogMsgType msgtype, string msg)
        {
            this.rtb_log.Invoke(new EventHandler(delegate
            {
                rtb_log.TabStop = false;
                rtb_log.SelectedText = string.Empty;
                rtb_log.SelectionFont = new Font(rtb_log.SelectionFont, FontStyle.Bold);
                rtb_log.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtb_log.AppendText(msg + "\n");
                rtb_log.ScrollToCaret();
            }));
        }
        private void ShowData(DataTable dt)
        {
            this.dataGridView1.Invoke(new EventHandler(delegate
            {
                dataGridView1.DataSource = dt;
            }));
        }
        /// <summary>
        /// 清除控件文本及enabled
        /// </summary>
        private void ClearInfo()
        {
            this.tb_kpnumber.Text = "";
            this.tb_vendercode.Text = "";
            this.tb_vendercode.Focus();
            this.cb_local.Text = "";
            this.cb_store.Text = "";
            this.rtb_remark.Text = "";

            this.cb_machineId.Text = "";
            this.tb_masterId.Text = "";

            this.tb_no.Enabled = true;
            this.tb_vc.Enabled = true;
            this.tb_kpnum.Enabled = true;
            this.tb_no.Text = "";
            this.tb_vc.Text = "";
            this.tb_kpnum.Text = "";
            this.cb_ware.Text = "";
            this.cb_location.Text = "";
            this.cb_state.Text = "";
            this.tb_mark.Text = "";
            this.bt_query.Enabled = true;
            //this.bt_delete.Enabled = true;
            this.bt_update.Enabled = true;

            this.nud_usercount.Value = 1;
            this.tb_woid.Text = "";
            this.tb_partnumber.Text = "";
            this.tb_kpno.Text = "";
            this.tb_kpdesc.Text = "";
            this.tb_process.Text = "";
            this.tb_bomver.Text = "";
            this.tb_relocid.Text = "";
            this.tb_restoreid.Text = "";
            this.tb_venderid.Text = "";
        }
        private void bt_addmanufac_Click(object sender, EventArgs e)
        {
            AddManufacturer amf = new AddManufacturer(mFrm, this);
            amf.ShowDialog();
        }

        #region 钢网管理

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                string s = dataGridViewX1["status", e.RowIndex].Value.ToString();
                if (s != "仓库" && s != "退回仓库" && s != "报废")
                {
                    MessageBox.Show("未在仓库中的钢网不能进行编辑...");
                    return;
                }
                this.tb_no.Enabled = false;
                this.tb_vc.Enabled = false;
                string state = dataGridViewX1["status", e.RowIndex].Value.ToString();
                this.tb_no.Text = dataGridViewX1["no", e.RowIndex].Value.ToString();
                this.tb_kpnum.Text = dataGridViewX1["knumber", e.RowIndex].Value.ToString();
                this.tb_vc.Text = dataGridViewX1["厂商编号", e.RowIndex].Value.ToString();
                this.cb_ware.Text = dataGridViewX1["wareid", e.RowIndex].Value.ToString();
                //this.cb_state.Items.Add(dataGridViewX1["status", e.RowIndex].Value.ToString());
                this.cb_location.Text = dataGridViewX1["location", e.RowIndex].Value.ToString();
                this.tb_mark.Text = dataGridViewX1["备注", e.RowIndex].Value.ToString();
            }
        }

        //private void AddStateType()
        //{
        //    this.cb_state.Items.Clear();
        //    ArrayList al = new ArrayList();
        //    al.AddRange(Enum.GetValues(typeof(StateType)));
        //    foreach (object item in al)
        //    {
        //        this.cb_state.Items.Add(item);
        //    }

        //}
        private void tabControl1_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            switch (this.tabControl1.SelectedTabIndex)
            {

                case 0:
                    this.tb_vendercode.Focus();
                    break;
                case 1:
                    this.cb_querycondition.Focus();
                    showdata_GridViewX2(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangwangQuery("NA", "NA", "NA")));
                    break;
                case 2:
                    this.tb_no.Focus();
                    //AddStateType();
                    showdata_GridViewX1(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangInfoInStore()));
                    break;
                case 3:
                    //this.tb_venderid.Focus();
                    //bt_inout.Enabled = false;
                    //radiolinestore_Click(null, null);
                    //ShowData_GridView2(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangwangTotal(this.tb_kpno.Text.Trim())));
                    break;
            }
        }

        private void cb_ware_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cb_location.Text = "";
            this.cb_location.Items.Clear();
            foreach (DataRow dr in FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetNotUsedLocId(this.cb_ware.Text.Trim())).Rows)
            {
                this.cb_location.Items.Add(dr["locId"].ToString());
            }
        }

        private void bt_update_Click(object sender, EventArgs e)
        {
            this.tb_no.Enabled = false;
            this.bt_delete.Enabled = false;
            this.bt_query.Enabled = false;
            try
            {
                if (!string.IsNullOrEmpty(this.tb_no.Text.Trim()))
                {
                    if (string.IsNullOrEmpty(this.tb_kpnum.Text.Trim()))
                        throw new Exception("料号不能为空!");
                    if (string.IsNullOrEmpty(this.cb_ware.Text))
                        throw new Exception("仓库为空!请选择...");
                    if (string.IsNullOrEmpty(this.cb_location.Text))
                        throw new Exception("库位为空!请选择...");
                    if (string.IsNullOrEmpty(this.cb_state.SelectedItem.ToString()))//Items[cb_state.SelectedIndex]
                        throw new Exception("状态不能为空!");

                    if (!string.IsNullOrEmpty(this.tb_kpnum.Text))
                    {
                        if (this.tb_kpnum.Text.Trim().Split('-').Length != 3)
                        {
                            this.mFrm.ShowPrgMsg("编号格式不符,正确格式:产品料号-版本-序号.", MainParent.MsgType.Error);
                            return;
                        }
                    }
                   tPartStorehousehad waremodel = new tPartStorehousehad();
                    if (this.cb_state.SelectedItem.ToString().Trim() == "仓库")
                        waremodel.status = 0;
                    if (this.cb_state.SelectedItem.ToString().Trim() == "已使用")
                        waremodel.status = 3;
                    if (this.cb_state.SelectedItem.ToString().Trim() == "外借")
                        waremodel.status = 8;
                    if (this.cb_state.SelectedItem.ToString().Trim() == "维修")
                        waremodel.status = 7;
                    if (this.cb_state.SelectedItem.ToString().Trim() == "生产线")
                        waremodel.status = 2;
                    if (this.cb_state.SelectedItem.ToString().Trim() == "线边仓")
                        waremodel.status = 1;
                    if (this.cb_state.SelectedItem.ToString().Trim() == "退回仓库")
                        waremodel.status = 6;
                    if (this.cb_state.SelectedItem.ToString().Trim() == "报废")
                        waremodel.status = 9;


                   // MessageBox.Show(waremodel.status.ToString());

                    waremodel.KpNumber = this.tb_kpnum.Text.Trim();
                    waremodel.VenderCode = this.tb_vc.Text.Trim();
                    waremodel.storehouseId = this.cb_ware.Text;
                    waremodel.LocId = this.cb_location.Text;
                    waremodel.Remark = this.tb_mark.Text.Trim();

                     dic = new Dictionary<string, object>();
                    dic.Add("TRSN", this.tb_no.Text);
                    dic.Add("KPNUMBER", this.tb_kpnum.Text);
                    dic.Add("VENDERCODE", this.tb_vc.Text);
                    dic.Add("STOREHOUSEID", this.cb_ware.Text);
                    dic.Add("LOCID", this.cb_location.Text);
                    dic.Add("SSTATUS", waremodel.status.ToString());
                    dic.Add("REMARK", this.tb_mark.Text);
                    

                    refWebtPartStorehousehad.Instance.UpdateGangInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));
                    this.mFrm.ShowPrgMsg("更新数据成功!", MainParent.MsgType.Normal);
                }
                showdata_GridViewX1(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.Getgangwang()));
                ClearInfo();

            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private void showdata_GridViewX1(DataTable dt)
        {
            this.dataGridViewX1.Invoke(new EventHandler(delegate
            {
                dataGridViewX1.DataSource = dt;
            }));
        }
        private void bt_delete_Click(object sender, EventArgs e)
        {
            this.tb_no.Enabled = true;
            this.bt_update.Enabled = false;
            this.bt_query.Enabled = false;
            try
            {
                if (!string.IsNullOrEmpty(this.tb_no.Text.Trim()))
                {
                    refWebtPartStorehousehad.Instance.DeleteGangInfoByTrsn(this.tb_no.Text.Trim());
                    this.mFrm.ShowPrgMsg("删除数据成功!", MainParent.MsgType.Normal);
                }
                if (string.IsNullOrEmpty(this.tb_no.Text.Trim()))
                    this.mFrm.ShowPrgMsg("钢网编号不能为空!", MainParent.MsgType.Warning);
                showdata_GridViewX1(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.Getgangwang()));
                ClearInfo();
            }
            catch (Exception exa)
            {
                this.mFrm.ShowPrgMsg(exa.Message, MainParent.MsgType.Error);
            }
        }
        private void bt_query_Click(object sender, EventArgs e)
        {
            this.tb_kpnum.Enabled = true;
            this.tb_no.Enabled = true;
            this.tb_vc.Enabled = true;
            try
            {
                if (!string.IsNullOrEmpty(this.tb_no.Text.Trim()))
                    showdata_GridViewX1(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangInfoInWare("trsn", this.tb_no.Text.Trim())));
                if (!string.IsNullOrEmpty(this.tb_kpnum.Text.Trim()))
                    showdata_GridViewX1(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangInfoInWare("kpnumber", this.tb_kpnum.Text.Trim())));
                if (!string.IsNullOrEmpty(this.tb_vc.Text.Trim()))
                    showdata_GridViewX1(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangInfoInWare("vendercode", this.tb_vc.Text.Trim())));
                else
                    dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangInfoInStore());
            }
            catch (Exception exb)
            {
                this.mFrm.ShowPrgMsg(exb.Message, MainParent.MsgType.Error);
            }
        }
        private void refresh_data()
        {
            showdata_GridViewX1(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangInfoInStore()));
        }
        private void bt_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                dsd = new delegateshowdata(refresh_data);
                dsd.BeginInvoke(null, null);
            }
            catch (Exception exd)
            {
                this.mFrm.ShowPrgMsg(exd.Message, MainParent.MsgType.Error);
            }
        }

        private void tb_kpnum_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_kpnum.Text))
            {
                if (this.tb_kpnum.Text.Trim().Split('-').Length != 3)
                {
                    MessageBoxEx.Show("编号格式不符\n\n正确格式:产品料号-版本-序号\n\n请填写正确的编号格式", "提示");
                    this.tb_kpnum.SelectAll();
                    this.tb_kpnum.Focus();
                }
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.QueryKpnumber());
                foreach (DataRow dr in dt.Rows)
                {
                    if (this.tb_kpnum.Text.Trim() == dr["kpnumber"].ToString() && this.tb_no.Text.Trim() != dr["trsn"].ToString())
                    {
                        mFrm.ShowPrgMsg("该料号在另一个钢网编号下已存在,请确认!", MainParent.MsgType.Warning);
                        this.tb_kpnum.SelectAll();
                        this.tb_kpnum.Focus();
                    }
                }
            }
        }
        private void tb_no_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_no.Text))
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangInfoByTrsn(this.tb_no.Text));
                if (dt.Rows.Count < 1)
                {
                    this.mFrm.ShowPrgMsg("该数据不存在!", MainParent.MsgType.Error);
                    this.tb_no.SelectAll();
                    this.tb_no.Focus();
                    return;
                }
            }
        }

        private void tb_mark_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_mark.Text.Trim()))
            {
                this.mFrm.ShowPrgMsg("备注不能为空,且前面两项为:连板数-板属性", MainParent.MsgType.Warning);
            }
        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
            ClearInfo();
        }

        #endregion

        #region 钢网查询
        private void showdata_GridViewX2(DataTable dt)
        {
            this.dataGridViewX2.Invoke(new EventHandler(delegate
            {
                dataGridViewX2.DataSource = dt;
            }));
        }

        private void bt_select_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.cb_querycondition.Text.Trim()) && string.IsNullOrEmpty(this.tb_value.Text.Trim()))
                    showdata_GridViewX2(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangwangQuery("NA", "NA", "NA")));
                if (this.cb_querycondition.Text.Trim() == "钢网编号")
                {
                    showdata_GridViewX2(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangwangQuery("NA", "NA", this.tb_value.Text.Trim())));
                }
                if (this.cb_querycondition.Text.Trim() == "厂商编号")
                    showdata_GridViewX2(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangwangQuery("NA", this.tb_value.Text.Trim(), "NA")));

                if (this.cb_querycondition.Text.Trim() == "产品料号")
                    showdata_GridViewX2(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangwangQuery(this.tb_value.Text.Trim(), "NA", "NA")));
            }
            catch (Exception exe)
            {
                this.mFrm.ShowPrgMsg(exe.Message, MainParent.MsgType.Error);
            }
        }
        private void tb_value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                bt_select_Click(null, null);
            }
        }
        #endregion

        #region 钢网收发
        private void tb_venderid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (!string.IsNullOrEmpty(this.tb_venderid.Text.Trim()))
                {
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangInfoByTrsn(this.tb_venderid.Text.Trim()));
                    if (dt.Rows.Count > 0)
                    {
                        this.tb_kpno.Text = dt.Rows[0]["kpnumber"].ToString();
                        this.tb_kpdesc.Text = dt.Rows[0]["remark"].ToString();
                        this.tb_process.Text = "SMD";
                        this.tb_bomver.Text = tb_kpno.Text.Trim().Split('-')[1];
                        this.tb_restoreid.Text = dt.Rows[0]["storehouseId"].ToString();
                        this.tb_relocid.Text = dt.Rows[0]["locId"].ToString();

                        string status = dt.Rows[0]["sstatus"].ToString();
                        #region 发料
                        if (radiolinestore.Checked)
                        {
                            if (status == "仓库" || status == "退回仓库")
                            {
                                bt_inout.Enabled = true;
                            }
                            if (status == "线边仓")
                            {
                                mFrm.ShowPrgMsg("该钢网已经发放到线边仓", MainParent.MsgType.Warning);
                                tb_kpno.SelectAll();
                                return;
                            }
                            if (status == "生产线")
                            {
                                mFrm.ShowPrgMsg("该钢网已经发放到生产线", MainParent.MsgType.Warning);
                                tb_kpno.SelectAll();
                                return;
                            }
                            if (status == "已使用")
                            {
                                mFrm.ShowPrgMsg("该钢网使用中", MainParent.MsgType.Warning);
                                tb_kpno.SelectAll();
                                return;
                            }
                            if (status == "维修")
                            {
                                mFrm.ShowPrgMsg("该刚网在维修", MainParent.MsgType.Warning);
                                tb_kpno.SelectAll();
                                return;
                            }
                            if (status == "外借")
                            {
                                mFrm.ShowPrgMsg("该钢网在外借中", MainParent.MsgType.Warning);
                                tb_kpno.SelectAll();
                                return;
                            }
                            if (status == "报废")
                            {
                                mFrm.ShowPrgMsg("该钢网已报废", MainParent.MsgType.Warning);
                                tb_kpno.SelectAll();
                                return;
                            }

                        }
                        #endregion
                        #region 收料
                        if (radiostore.Checked)
                        {
                            //7维修，8外借，9报废
                            if (status == "线边仓" || status == "已使用" || status == "维修" || status == "外借")
                            {
                                bt_inout.Enabled = true;
                            }
                            if (status == "仓库" || status == "退回仓库")
                            {
                                mFrm.ShowPrgMsg("该钢网已在仓库,不需要重复接收...", MainParent.MsgType.Warning);
                                tb_kpno.SelectAll();
                                return;

                            }
                            if (status == "生产线")
                            {
                                mFrm.ShowPrgMsg("该钢网已经发放到生产线,不能做收料..", MainParent.MsgType.Warning);
                                tb_kpno.SelectAll();
                                return;
                            }

                            if (status == "报废")
                            {
                                mFrm.ShowPrgMsg("该钢网已报废,不能做收料..", MainParent.MsgType.Warning);
                                tb_kpno.SelectAll();
                                return;
                            }

                        }
                        #endregion
                    }
                }
            }
        }
        private void radiolinestore_Click(object sender, EventArgs e)
        {
            if (this.radiolinestore.Checked)
            {
                this.bt_inout.Text = "发料";
                this.label17.Visible = false;
                this.label18.Visible = false;
                this.tb_relocid.Visible = false;
                this.tb_restoreid.Visible = false;

                this.label26.Visible = false;
                this.nud_usercount.Visible = false;

                this.label19.Visible = true;
                this.label20.Visible = true;
                this.tb_partnumber.Visible = true;
                this.tb_woid.Visible = true;
                this.cb_machineId.Visible = true;
                this.label25.Visible = true;
            }
        }

        private void radiostore_Click(object sender, EventArgs e)
        {
            if (this.radiostore.Checked)
            {
                this.bt_inout.Text = "收料";
                this.label17.Visible = true;
                this.label18.Visible = true;
                this.tb_relocid.Visible = true;
                this.tb_restoreid.Visible = true;

                this.label26.Visible = true;
                this.nud_usercount.Visible = true;
                this.nud_usercount.Value = 1;

                this.label19.Visible = false;
                this.label20.Visible = false;
                this.tb_partnumber.Visible = false;
                this.tb_woid.Visible = false;
                this.cb_machineId.Visible = false;
                this.label25.Visible = false;

                this.tb_woid.Text = "";
                this.tb_partnumber.Text = "";
                this.cb_machineId.DataSource = null;
                this.cb_machineId.Text = "";
                this.tb_masterId.Text = "";
            }
        }

        private void tb_woid_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_woid.Text.Trim()))
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(this.tb_woid.Text.Trim(),null,null));
                if (dt.Rows.Count > 0)
                {
                    this.tb_partnumber.Text = dt.Rows[0]["partnumber"].ToString();
                    DataTable mdt = FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.GetMachineIdAndMasterIdListByPartnumber(this.tb_partnumber.Text.Trim()));
                    DataTable dtTemp = mdt.Clone();

                    foreach (DataRow dr in mdt.Rows)
                    {
                        DataRow[] arrDr;
                        if ((arrDr = mdt.Select(string.Format("lineId='{0}'", dr["lineId"].ToString().Substring(0, dr["lineId"].ToString().Length - 1) + "L"))).Length == 1)
                        {
                            if (dtTemp.Select(string.Format("lineId='{0}'", dr["lineId"].ToString().Substring(0, dr["lineId"].ToString().Length - 1) + "L")).Length < 1)
                            {
                                dtTemp.Rows.Add(arrDr[0].ItemArray);
                                //dtTemp.Rows.Add(dr["masterId"].ToString(), dr["lineId"].ToString().Substring(0, dr["lineId"].ToString().Length - 1) + "X");
                            }
                        }
                        else
                        {
                            if ((arrDr = mdt.Select(string.Format("lineId='{0}'", dr["lineId"].ToString().Substring(0, dr["lineId"].ToString().Length - 1) + "R"))).Length == 1)
                            {
                                dtTemp.Rows.Add(arrDr[0].ItemArray);
                            }
                        }
                    }

                    this.cb_machineId.DataSource = dtTemp;

                    this.cb_machineId.DisplayMember = "lineId";
                    this.cb_machineId.ValueMember = "masterId";
                    this.cb_machineId.Text = "";
                }
                else
                {
                    mFrm.ShowPrgMsg("该工单不存在", MainParent.MsgType.Warning);
                    this.cb_machineId.DataSource = null;
                    this.tb_partnumber.Text = "";
                    this.cb_machineId.Text = "";
                    this.tb_masterId.Text = "";
                    this.tb_woid.SelectAll();
                    this.tb_woid.Focus();
                    return;
                }
            }
        }

        private void bt_inout_Click(object sender, EventArgs e)
        {
           /* if (string.IsNullOrEmpty(this.tb_venderid.Text.Trim()))
            {
                mFrm.ShowPrgMsg("钢网编号为空...", MainParent.MsgType.Warning);
                return;
            }
            try
            {
                #region 发料
                if (radiolinestore.Checked)
                {
                    if (string.IsNullOrEmpty(this.tb_woid.Text.Trim()))
                        throw new Exception("请输入工单号!");
                    if (string.IsNullOrEmpty(this.tb_partnumber.Text.Trim()))
                        throw new Exception("成品料号不能为空!");
                    if (string.IsNullOrEmpty(this.tb_kpno.Text) || string.IsNullOrEmpty(this.tb_kpdesc.Text))
                        throw new Exception("料号及描述不能为空!");
                    if (string.IsNullOrEmpty(this.tb_masterId.Text))
                        throw new Exception("请重新选择机器");

                    if (MessageBox.Show(string.Format("确定要发钢网{0}到线边仓吗?", this.tb_venderid.Text.Trim()),
                        "钢网提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        refWebtWoBomInfo.Instance.InsertWoBomInfo(new WebServices.tWoBomInfo.T_WO_BOM_INFO()
                        {
                            wbiId = Guid.NewGuid(),
                            woId = this.tb_woid.Text.Trim(),
                            partnumber = this.tb_partnumber.Text,
                            kpnumber = this.tb_kpno.Text.Trim(),
                            kpdesc = this.tb_kpdesc.Text.Trim(),
                            qty = 1,
                            process = this.tb_process.Text,
                            bomver = this.tb_bomver.Text,
                            userId = mFrm.gUserInfo.userId
                        });
                        refWebtPartStorehousehad.Instance.UpdateTrSnStatus("1", mFrm.gUserInfo.userId, this.tb_venderid.Text.Trim());

                        refWebSmtKpMaster.Instance.InsertSmtKpDetaltForWo(new WebServices.tSmtKpMaster.SMT_KP_DETALTForWo()
                        {
                            WoId = this.tb_woid.Text.Trim(),
                            MasterId = this.tb_masterId.Text,
                            UserId = this.mFrm.gUserInfo.userId,
                            Kpnumber = this.tb_kpno.Text,
                            Kpdesc = this.tb_kpdesc.Text.Trim(),
                            Stationno = "0001A"
                        });
                        mFrm.ShowPrgMsg("钢网发到线边仓", MainParent.MsgType.Outgoing);
                        this.tb_venderid.Text = "";
                        this.tb_venderid.Focus();
                        this.bt_inout.Enabled = false;
                    }
                    else
                    {
                        mFrm.ShowPrgMsg("钢网未发放...", MainParent.MsgType.Outgoing);
                        this.tb_venderid.SelectAll();
                        this.tb_venderid.Focus();
                        this.bt_inout.Enabled = false;
                    }
                }
                #endregion
                #region 收料
                if (radiostore.Checked)
                {
                    if (string.IsNullOrEmpty(this.tb_restoreid.Text.Trim()))
                        throw new Exception("仓库为空!");
                    if (string.IsNullOrEmpty(this.tb_relocid.Text.Trim()))
                        throw new Exception("库位为空!");
                    if (this.nud_usercount.Value < 1)
                    {
                        if (MessageBoxEx.Show("没有填写钢网使用次数,是否继续? \n\n继续请按[Yes] 重新填写请按[NO]", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
                            return;
                    }

                    if (MessageBox.Show(string.Format("确定要把钢网{0}收到仓库吗?", this.tb_venderid.Text.Trim()), "钢网提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        refWebtPartStorehousehad.Instance.UpdateTrSnStatus("6", mFrm.gUserInfo.userId, this.tb_venderid.Text.Trim());
                        refWebtPartStorehousehad.Instance.UpdateByTrsn(this.tb_venderid.Text.Trim(), this.tb_restoreid.Text.Trim(), this.tb_relocid.Text.Trim());
                        refWebtPartStorehousehad.Instance.UpdateGangWangUseCount(this.tb_venderid.Text.Trim(), (int)this.nud_usercount.Value);
                        mFrm.ShowPrgMsg("已将钢网收到仓库", MainParent.MsgType.Outgoing);
                        this.tb_venderid.SelectAll();
                        this.tb_venderid.Focus();
                        this.bt_inout.Enabled = false;
                    }
                    else
                    {
                        mFrm.ShowPrgMsg("钢网未收回...", MainParent.MsgType.Outgoing);
                        this.tb_venderid.SelectAll();
                        this.tb_venderid.Focus();
                        this.bt_inout.Enabled = false;
                    }
                }
                #endregion

                ClearInfo();//清空控件信息
            }
            catch (Exception EX)
            {
                this.mFrm.ShowPrgMsg(EX.Message, MainParent.MsgType.Error);
            }*/
        }

        private void bt_toexcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewX2.RowCount != 0)
                    DataToExcel(dataGridViewX2);
                else
                    mFrm.ShowPrgMsg("没有资料可以导出Excel", MainParent.MsgType.Error);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void DataToExcel(DataGridView m_DataView)
        {
            SaveFileDialog kk = new SaveFileDialog();
            kk.Title = "保存EXECL文件";
            //     kk.Filter = "EXECL文件(*.xls)|*.xls|所有文件(*.*) |*.*";
            kk.Filter = "EXECL 97-2003工作薄|*.xls|所有文件(*.*) |*.*";
            kk.FilterIndex = 1;
            if (kk.ShowDialog() == DialogResult.OK)
            {
                string FileName = kk.FileName;// +".xls";
                if (File.Exists(FileName))
                    File.Delete(FileName);
                FileStream objFileStream;
                StreamWriter objStreamWriter;
                string strLine = "";
                objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
                objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
                for (int i = 0; i < m_DataView.Columns.Count; i++)
                {
                    if (m_DataView.Columns[i].Visible == true)
                    {
                        strLine = strLine + m_DataView.Columns[i].HeaderText.ToString() + Convert.ToChar(9);
                    }
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";

                for (int i = 0; i < m_DataView.Rows.Count; i++)
                {
                    if (m_DataView.Columns[0].Visible == true)
                    {
                        if (m_DataView.Rows[i].Cells[0].Value == null)
                            strLine = strLine + " " + Convert.ToChar(9);
                        else
                            strLine = strLine + m_DataView.Rows[i].Cells[0].Value.ToString() + Convert.ToChar(9);
                    }
                    for (int j = 1; j < m_DataView.Columns.Count; j++)
                    {
                        if (m_DataView.Columns[j].Visible == true)
                        {
                            if (m_DataView.Rows[i].Cells[j].Value == null)
                                strLine = strLine + " " + Convert.ToChar(9);
                            else
                            {
                                string rowstr = "";
                                rowstr = m_DataView.Rows[i].Cells[j].Value.ToString();
                                if (rowstr.IndexOf("\r\n") > 0)
                                    rowstr = rowstr.Replace("\r\n", " ");
                                if (rowstr.IndexOf("\t") > 0)
                                    rowstr = rowstr.Replace("\t", " ");
                                strLine = strLine + rowstr + Convert.ToChar(9);
                            }
                        }
                    }
                    objStreamWriter.WriteLine(strLine);
                    strLine = "";
                }
                objStreamWriter.Close();
                objFileStream.Close();
                mFrm.ShowPrgMsg("保存EXCEL成功 ", MainParent.MsgType.Incoming);
            }
        }

        private void bt_selectqty_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ShowData_GridView2(FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetGangwangTotal(this.tb_kpno.Text.Trim())));
            //}
            //catch (Exception ex)
            //{
            //    this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            //}
        }

        private void ShowData_GridView2(DataTable dt)
        {
            this.dataGridView2.Invoke(new EventHandler(delegate
            {
                dataGridView2.DataSource = dt;
            }));
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (dataGridView2.Rows.Count < 1)
                return;
            try
            {
                string[] str = dataGridView2["备注消息", e.RowIndex].Value.ToString().Split('-');
                int panelnum = Convert.ToInt32(str[0]);
                string panelattr = str[1];
                int qty = Convert.ToInt32(dataGridView2["产量", e.RowIndex].Value);
                if (panelattr == "T/B" || panelattr == "t/b" || panelattr == "T" || panelattr == "B" || panelattr == "t" || panelattr == "b")
                    this.dataGridView2["刷网次数", e.RowIndex].Value = (qty + 1) / panelnum;
                if (panelattr == "S")
                    this.dataGridView2["刷网次数", e.RowIndex].Value = ((qty + 1) / panelnum) * 2;
                if (Convert.ToInt32(dataGridView2["刷网次数", e.RowIndex].Value) >= 35000)
                    this.dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.OrangeRed;
                if (Convert.ToInt32(dataGridView2["刷网次数", e.RowIndex].Value) >= 40000)
                {
                    this.dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    refWebtPartStorehousehad.Instance.UpdateTrSnStatus("9", mFrm.gUserInfo.userId, this.tb_venderid.Text.Trim());
                }

            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_clearinfo_Click(object sender, EventArgs e)
        {
            ClearInfo();
        }

        private void cb_restoreid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.tb_relocid.Text = "";
            //this.tb_relocid.Items.Clear();
            ////foreach (DataRow dr in FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetWarehouseLoctionListBystorehouseId(this.cb_store.Text.Trim())).Rows)
            //foreach (DataRow dr in FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetNotUsedLocId(this.tb_restoreid.Text)).Rows)
            //{
            //    this.tb_relocid.Items.Add(dr["locId"].ToString());
            //}
        }

        #endregion

        private void cb_machineId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.tb_masterId.Text = this.cb_machineId.SelectedValue.ToString();
        }

        private void SMTgangwangManage_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                lbl.Documents.CloseAll(false);
                lbl.Quit();                                         //退出      
            }
            catch
            {
            }
        }


    }

    public class tPartStorehousehad
    {

        /// <summary>
        /// 物料唯一序列号Tr_Sn
        /// </summary>      
        string _Tr_Sn = "";
        public string Tr_Sn
        {
            get { return _Tr_Sn; }
            set { _Tr_Sn = value; }
        }

        /// <summary>
        /// 物料料号
        /// </summary>      
        string _KpNumber = "NA";
        public string KpNumber
        {
            get { return _KpNumber; }
            set { _KpNumber = value; }
        }

        /// <summary>
        /// 厂商代码
        /// </summary> 
        string _VenderCode = "NA";
        public string VenderCode
        {
            get { return _VenderCode; }
            set { _VenderCode = value; }
        }
        /// <summary>
        /// 生产周期
        /// </summary> 
        string _DateCode = "NA";
        public string DateCode
        {
            get { return _DateCode; }
            set { _DateCode = value; }
        }

        /// <summary>
        ///生产批次
        /// </summary> 
        string _LotId = "NA";
        public string LotId
        {
            get { return _LotId; }
            set { _LotId = value; }
        }

        /// <summary>
        /// 入库数量
        /// </summary> 
        int _QTY = 0;
        public int QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        /// <summary>
        /// 出库数量
        /// </summary> 
        int _OUTQTY = 0;
        public int OUTQTY
        {
            get { return _OUTQTY; }
            set { _OUTQTY = value; }
        }
        /// <summary>
        /// 备注
        /// </summary> 
        string _Remark = "NA";
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        /// <summary>
        /// 状态
        /// </summary> 
        int _status = 0;
        public int status //{ get; set; }
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// 时间
        /// </summary> 
        string _recdate = "NA";
        public string recdate
        {
            get { return _recdate; }
            set { _recdate = value; }
        }

        /// <summary>
        /// 仓位或料架
        /// </summary> 
        string _LocId = "NA";
        public string LocId
        {
            get { return _LocId; }
            set { _LocId = value; }
        }

        /// <summary>
        /// 仓库编号   12-09-11加
        /// </summary>
        string _storehouseId = "NA";
        public string storehouseId
        {
            get { return _storehouseId; }
            set { _storehouseId = value; }
        }

        /// <summary>
        /// 人员工号
        /// </summary> 
        public string UserId { get; set; }

        public string FLAG { get; set; }
    }
}
