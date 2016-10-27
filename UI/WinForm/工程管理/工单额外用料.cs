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
    public partial class AddMaterials : Office2007Form // Form
    {
        public AddMaterials(MainParent mfrm)
        {
            InitializeComponent();
            this.mFrm = mfrm;
        }
        MainParent mFrm;

        private enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error } //文字输出类型
        private Color[] LogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };//文字颜色
        private Dictionary<string, string> mWoidPartList = new Dictionary<string, string>();
        private Dictionary<string, string> mStationMasterList = new Dictionary<string, string>();
        private delegate void delegateshow();
        delegateshow ds;
        private void ShowData()
        {
            dgv_showdate(FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.GettSmtKPDetaltForWo()));
        }

        private void AddMaterials_Load(object sender, EventArgs e)
        {
            try
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
                this.mWoidPartList.Clear();
                this.cb_woid.Focus();
                this.tabControl1.SelectedTabIndex = 0;
                //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetAllWoInfo());
                //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfoByState(WebServices.tWoInfo.eWoState.Start,
                //    new WebServices.tWoInfo.WoInfoColumns[] { WebServices.tWoInfo.WoInfoColumns.woId, WebServices.tWoInfo.WoInfoColumns.partnumber }));
                //List<string> LsoInfo = new List<string>();
                //LsoInfo.Add("WOID");
                //LsoInfo.Add("PARTNUMBER");
                //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfoByState("0", LsoInfo.ToArray()));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    this.cb_woid.Items.Add(dr["woId"].ToString());
                //    this.mWoidPartList.Add(dr["woId"].ToString(), dr["partnumber"].ToString());
                //}
                //dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfoByState("1", LsoInfo.ToArray()));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    this.cb_woid.Items.Add(dr["woId"].ToString());
                //    this.mWoidPartList.Add(dr["woId"].ToString(), dr["partnumber"].ToString());
                //}
                //dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfoByState("2", LsoInfo.ToArray()));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    this.cb_woid.Items.Add(dr["woId"].ToString());
                //    this.mWoidPartList.Add(dr["woId"].ToString(), dr["partnumber"].ToString());
                //}
                string Json = refWebt_wo_Info_Erp.Instance.Get_Erp_WoList();
                IDictionary<string, object> mst = FrmBLL.ReleaseData.JsonToDictionary(Json);
                foreach (KeyValuePair<string, object> kvp in mst)
                {
                    this.cb_woid.Items.Add(kvp.Key);
                    this.mWoidPartList.Add(kvp.Key, kvp.Value.ToString());
                }

                //dgv_showdate(FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.GettSmtKPDetaltForWo()));
                ds = new delegateshow(ShowData);
                ds.BeginInvoke(null, null);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private void ShowMsg(LogMsgType msgtype, string msg)
        {
            this.rtb_message.Invoke(new EventHandler(delegate
            {
                rtb_message.TabStop = false;
                rtb_message.SelectedText = string.Empty;
                rtb_message.SelectionFont = new Font(rtb_message.SelectionFont, FontStyle.Bold);
                rtb_message.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtb_message.AppendText(msg + "\n");
                rtb_message.ScrollToCaret();
            }));
        }

        private void AddStationnoItems(DataTable dt)
        {
            this.cb_stationno.Invoke(new EventHandler(delegate
            {
                this.mStationMasterList.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    this.cb_stationno.Items.Add(dr["stationno"].ToString());
                    mStationMasterList.Add(dr["stationno"].ToString(), dr["masterId"].ToString());
                }
            }));
        }

        private void tb_kpnumber_Leave(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartMap.Instance.QueryPartMaps(this.tb_kpnumber.Text.Trim()));
                if (dt.Rows.Count < 1)
                {
                    this.mFrm.ShowPrgMsg("该料号不存在...", MainParent.MsgType.Warning);
                }
                if (dt.Rows.Count >= 1)
                {
                    this.tb_kpdesc.Text = dt.Rows[0]["kpdesc"].ToString();
                }
            }
            catch
            {
                this.mFrm.ShowPrgMsg("物料信息加载失败", MainParent.MsgType.Error);
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.cb_woid.Text))
                    throw new Exception("请选择工单号...");
                if (string.IsNullOrEmpty(this.cb_lineid.Text))
                    throw new Exception("请选择机器编号...");
                if (string.IsNullOrEmpty(this.cb_stationno.Text))
                    throw new Exception("请选择料站编号...");
                if (string.IsNullOrEmpty(this.tb_kpnumber.Text))
                    throw new Exception("请填写料号...");
                if (string.IsNullOrEmpty(this.tb_kpdesc.Text))
                    throw new Exception("请填写料号描述...");
                if (string.IsNullOrEmpty(this.tb_qty.Text))
                    throw new Exception("请填写数量...");
                if (string.IsNullOrEmpty(this.tb_process.Text))
                    throw new Exception("请填写制程段...");

                DataTable dtable = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoBomInfo.Instance.QueryWoBomInfoData(this.tb_kpnumber.Text.Trim()));
                foreach (DataRow dr in dtable.Rows)
                {
                    if (this.tb_kpnumber.Text.Trim()==dr["kpnumber"].ToString())
                    {
                        this.mFrm.ShowPrgMsg("工单发料表中已有该料,无需再额外添加!", MainParent.MsgType.Warning);
                        this.tb_kpnumber.SelectAll();
                        this.tb_kpnumber.Focus();
                        return;
                    }
                }
                //refWebtWoBomInfo.Instance.InsertWoBomInfo(new WebServices.tWoBomInfo.T_WO_BOM_INFO()
                //{
                //    wbiId = Guid.NewGuid(),
                //    woId = this.cb_woid.Text.Trim(),
                //    partnumber = this.mWoidPartList[this.cb_woid.Text],
                //    kpnumber = this.tb_kpnumber.Text.Trim(),
                //    kpdesc = this.tb_kpdesc.Text.Trim(),
                //    qty = int.Parse(this.tb_qty.Text.Trim()),
                //    process = this.tb_process.Text,
                //    bomver = this.tb_bomver.Text.Trim(),
                //    userId = mFrm.gUserInfo.userId,
                //    blocked = 1
                //});
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.JudgeKpExists(
                    this.cb_stationno.Text, 
                    this.mStationMasterList[this.cb_stationno.Text], 
                    this.tb_kpnumber.Text.Trim()));
                //查看是否在tSmtKPDetalt中
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.mFrm.ShowPrgMsg("该料已在料站表中,无需再额外添加用料信息...", MainParent.MsgType.Warning);
                    this.tb_kpnumber.SelectAll();
                    this.tb_kpnumber.Focus();
                    return;
                }

                Dictionary<string,object> mst = new Dictionary<string,object>();
                mst.Add("MASTERID",this.mStationMasterList[this.cb_stationno.Text]);
                mst.Add("WOID",this.cb_woid.Text);
                mst.Add("USERID",this.mFrm.gUserInfo.userId);
                mst.Add("STATIONNO",this.cb_stationno.Text);
                mst.Add("KPNUMBER", this.tb_kpnumber.Text.Trim());
                mst.Add("KPDESC", this.tb_kpdesc.Text.Trim());
           
                string sRES = refWebSmtKpMaster.Instance.InsertSmtKpDetaltForWo(FrmBLL.ReleaseData.DictionaryToJson(mst));
               //string sRES= refWebSmtKpMaster.Instance.InsertSmtKpDetaltForWo(new WebServices.tSmtKpMaster.SMT_KP_DETALTForWo()
               // {
               //     WoId = this.cb_woid.Text,
               //     MasterId = this.mStationMasterList[this.cb_stationno.Text],
               //     UserId = this.mFrm.gUserInfo.userId,
               //     Stationno = this.cb_stationno.Text,
               //     Kpnumber = this.tb_kpnumber.Text.Trim(),
               //     Kpdesc = this.tb_kpdesc.Text.Trim()
               // });
               this.ShowMsg(LogMsgType.Outgoing, string.Format("料号{0}添加成功! {1}", this.tb_kpnumber.Text.Trim(), sRES));
                dgv_showdate(FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.GettSmtKPDetaltForWo()));
                ClearInfo();
            }
            catch (Exception ex)
            {
                ShowMsg(LogMsgType.Error, ex.Message);
                MessageBoxEx.Show("数据保存失败!!");
            }
        }

        private void cb_lineid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.cb_lineid.Text) && !string.IsNullOrEmpty(this.cb_woid.Text) && !string.IsNullOrEmpty(cb_pcbside.Text))
                {
                    this.mStationMasterList.Clear();
                    this.cb_stationno.Items.Clear();
                    this.cb_stationno.Text = "";
                    //AddStationnoItems(FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.GetStationno(this.mWoidPartList[this.cb_woid.Text], this.cb_lineid.Text, this.cb_pcbside.Text)));
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.GetStationno(this.mWoidPartList[this.cb_woid.Text], this.cb_lineid.Text, this.cb_pcbside.Text));
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        this.cb_stationno.Items.Clear();
                        this.mStationMasterList.Clear();
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            this.cb_stationno.Items.Add(dr["stationno"].ToString());
                            mStationMasterList.Add(dr["stationno"].ToString(), dr["masterId"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_query_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.tb_kpnumber.Text.Trim()))
                {
                    dgv_showdate(FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.GettSmtKPDetaltForWoByKpnumber(this.tb_kpnumber.Text.Trim())));
                }
                else
                    dgv_showdate(FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.GettSmtKPDetaltForWo()));
            }
            catch(Exception ex)
            {
                ShowMsg(LogMsgType.Error, ex.Message);
                MessageBoxEx.Show("查询失败!!");
            }
        }

        private void dgv_showdate(DataTable dt)
        {
            this.dgvdata.Invoke(new EventHandler(delegate
            {
                dgvdata.DataSource = dt;
            }));
        }
        private void ClearInfo()
        {
            this.tb_kpnumber.Text = "";
            this.tb_kpdesc.Text = "";
            this.cb_lineid.Text = "";
            this.cb_woid.Text = "";
            this.cb_stationno.Text = "";
            this.cb_pcbside.Text = "";
            this.tb_process.Text = "";
            this.tb_qty.Text = "";

        }

        private void cb_pcbside_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.cb_lineid_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
            ClearInfo();
            this.tb_bomver.Text = "";
        }

        private void tb_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void cb_woid_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.cb_woid.Text))
                {
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebt_wo_Info_Erp.Instance.Get_WO_Info_Erp(this.cb_woid.Text, "BOMVER"));
                    if (dt.Rows.Count == 0)
                        throw new Exception("没有找到工单信息");
                    tb_bomver.Text = dt.Rows[0][0].ToString();


                    this.cb_lineid.Items.Clear();
                    this.cb_lineid.Text = "";
                    bool flag = false;
                    foreach (string str in this.mWoidPartList.Keys)
                    {
                        if (str.Trim() == this.cb_woid.Text.Trim())
                            flag = true;
                    }
                    if (!flag)
                    {
                        MessageBoxEx.Show("工单不存在!! 请重新输入");
                        this.cb_lineid.Items.Clear();
                        this.cb_stationno.Items.Clear();
                        this.mStationMasterList.Clear();
                        return;
                    }
                    string[] lineTemp = refWebExcelToDb.Instance.GetMachineIdList(this.mWoidPartList[this.cb_woid.Text]);
                    if (lineTemp == null || lineTemp.Length < 1)
                    {
                        this.cb_lineid.Items.Clear();
                        this.cb_stationno.Items.Clear();
                        this.mStationMasterList.Clear();
                    }
                    else
                        this.cb_lineid.Items.AddRange(lineTemp);
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg("工单加载失败: "+ex.Message, MainParent.MsgType.Error);
            }
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbwoid.Text) || string.IsNullOrEmpty(tbLine.Text) || string.IsNullOrEmpty(this.cbpcbside.Text))
            {
                this.mFrm.ShowPrgMsg("请填写完整的查询信息..", MainParent.MsgType.Error);
                this.dataGridView1.DataSource = null;
                return;
            }
            string err;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(this.tbwoid.Text, null, "partnumber".ToUpper()));
            if (dt == null || dt.Rows.Count < 1)
                return;
            string partnumber = dt.Rows[0]["partnumber"].ToString();
            dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebExcelToDb.Instance.GetKpMasterInfo(dt.Rows[0]["partnumber"].ToString(), tbLine.Text.Trim(), this.cbpcbside.Text.Trim()));
            if (dt == null || dt.Rows.Count < 1)
                return;
            string mastId = dt.Rows[0]["masterId"].ToString();
            this.dataGridView1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebExcelToDb.Instance.GetSmtKpDetaltByMasterIdNew(mastId, this.tbwoid.Text, out err));
        }

        private void tsmi_deletedata_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbwoid.Text) || string.IsNullOrEmpty(tbLine.Text) || string.IsNullOrEmpty(this.cbpcbside.Text))
            {
                this.ShowMsg(LogMsgType.Warning,"请填写完整的查询信息..");
                this.dataGridView1.DataSource = null;
                return;
            }
            string err;
            string _stationno;
            string _kpnumber;

            try
            {
                if (this.dataGridView1.SelectedRows.Count < 1)
                {
                    this.ShowMsg(LogMsgType.Warning, "没有选中任何数据");
                    return;
                }
                _stationno = this.dataGridView1.SelectedRows[0].Cells["stationno"].Value.ToString().Trim();
                _kpnumber = this.dataGridView1.SelectedRows[0].Cells["kpnumber"].Value.ToString().Trim();

                if (MessageBoxEx.Show(string.Format("确认需要删除一下数据?\n\n料号:{0}\n\n机器号:{1}\n\n站位:{2}\n\n面别:{3} 面\\nn工单号:{4}\n\n确认请选择 [YES]   取消请选择[NO]",
                     _kpnumber, tbLine.Text, _stationno, this.cbpcbside.Text, this.tbwoid.Text.Trim()), "提示!!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }

                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(this.tbwoid.Text, null, "partnumber".ToUpper()));
                if (dt == null || dt.Rows.Count < 1)
                    return;
                string partnumber = dt.Rows[0]["partnumber"].ToString();
                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebExcelToDb.Instance.GetKpMasterInfo(dt.Rows[0]["partnumber"].ToString(), tbLine.Text.Trim(), this.cbpcbside.Text.Trim()));
                if (dt == null || dt.Rows.Count < 1)
                    return;
                string mastId = dt.Rows[0]["masterId"].ToString();

                refWebExcelToDb.Instance.DeleteSmtKPDetaltForWo(this.tbwoid.Text.Trim(), mastId, _stationno, _kpnumber);

                this.dataGridView1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebExcelToDb.Instance.GetSmtKpDetaltByMasterIdNew(mastId, this.tbwoid.Text, out err));
                this.ShowMsg(LogMsgType.Outgoing, "数据删除成功..");
            }
            catch (Exception ex)
            {
                this.ShowMsg(LogMsgType.Error, ex.Message);
            }

        }

        private void imbt_UpdateBomVer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("工单[{0}]的BOM版本将修改为[{1}]", cb_woid.Text, tb_bomver.Text), "修改提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Dictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOID", cb_woid.Text);
                mst.Add("BOMVER", tb_bomver.Text);
               string _StrErr= refWebt_wo_Info_Erp.Instance.Update_Erp_woInfo(FrmBLL.ReleaseData.DictionaryToJson(mst));
               if (_StrErr == "OK")
               {
                   this.ShowMsg(LogMsgType.Incoming, "修改成功");
                   FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "UpdateBomVer", "Modify", FrmBLL.ReleaseData.DictionaryToJson(mst));
               }
               else
                   this.ShowMsg(LogMsgType.Error, "修改失败:" + _StrErr);
            }
        }
    }
}
