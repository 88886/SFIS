using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using WebServices.Webt_woInfo;

namespace SFIS_V2
{
    public partial class Frm_SnRange : Office2007Form
    {
        public Frm_SnRange(Office2007Form Frm)
        {
            InitializeComponent();
            mFrm=Frm;
        }
        Office2007Form mFrm;

        DataTable dt_Plan = new DataTable();//记录所有的在用号段
        DataTable Dt_Sel_Pan = new DataTable();//记录选中的号段信息
        DataTable dt_Numtye = new DataTable();//记录所有KRMS内号码类型
     
     
       public string _woType = string.Empty;
        private void Frm_SnRange_Load(object sender, EventArgs e)
        {
            #region 获取KRMS所有Release过的投产单  Mage
            //获取所有release号段；并将状态该为用完
            dt_Plan = FrmBLL.ReleaseData.arrByteToDataSet(refWEB_T_plan_list.Instance.Sel_plan_list_SFIS()).Tables[0];
            dt_Numtye = FrmBLL.ReleaseData.arrByteToDataSet(refWEB_t_numtype_info.Instance.Sel_NumType()).Tables[0];


            //绑定到cmb_PlanOrder
            DataTable dtNew = new DataTable();
            dtNew = dt_Plan.DefaultView.ToTable(true, "PLAN_ORDER");
            cmb_PlanOrder.DisplayMember = "PLAN_ORDER";
            cmb_PlanOrder.ValueMember = "PLAN_ORDER";
            cmb_PlanOrder.DataSource = dtNew;

            #endregion
 
            cmb_PlanOrder.Enabled = chk_krms.Checked;
            Lb_PlanInfo.Enabled = chk_krms.Checked;
        }

        private void txt_woId_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_woId.Text) && e.KeyCode == Keys.Enter)
            {
                this.txt_woId.SelectAll();
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(txt_woId.Text,null));
                dgv_woinfo.DataSource = dt;
                dgv_snRange.DataSource = null;
                this.ip_labletypes.Items.Clear();
                this.cbSetEsn.Items.Clear();
                ip_labletypes.Refresh();
                if (dt.Rows.Count > 0)
                {
                    _woType = dt.Rows[0]["WOTYPE"].ToString();
                    string[] names = refWebtProduct.Instance.GetProductLableNames(dt.Rows[0]["PARTNUMBER"].ToString());// BLL.tProduct.GetProductLableNames(this.tb_partnumber.Text);
                   
                    foreach (string item in names)
                    {
                        DevComponents.DotNetBar.CheckBoxItem cbi = null;
                        this.ip_labletypes.Items.Add(cbi = new DevComponents.DotNetBar.CheckBoxItem
                        {                       
                            Text = item,
                            Name = item
                        });
                        // cbi.CheckedChanged += new CheckBoxChangeEventHandler(cbi_CheckedChanged);
                        cbi.Checked = true;
                        this.cbSetEsn.Items.Add(cbi.Name);
                    }
                    this.ip_labletypes.Refresh();

                    DownLoad_DB_SnRange(txt_woId.Text);

                }
            }
        }

        private void DownLoad_DB_SnRange(string  m_woId )
        {
          DataTable  _mdt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWoInfo.Instance.GetWoSnRule(m_woId, null));
          if (_mdt.Rows.Count > 0)
          {
              FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();

              ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{0}'", m_woId));
              foreach (DataRow dr in _mdt.Rows)
              {
                  string sql = string.Format("insert into wosnrule(woId,sntype,snstart,snend,snprefix,snpostfix,ver,reve,snleng,usenum) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                        dr["WOID"].ToString(), // this.dgvserialnumberrule["woid", i].Value.ToString(),
                         dr["SNTYPE"].ToString(),  //this.dgvserialnumberrule["sntype", i].Value.ToString(),
                          dr["SNSTART"].ToString(), //this.dgvserialnumberrule["snstart", i].Value.ToString(),
                          dr["SNEND"].ToString(),// this.dgvserialnumberrule["snend", i].Value.ToString(),
                          dr["SNPREFIX"].ToString(),// this.dgvserialnumberrule["snprefix", i].Value.ToString(),
                         dr["SNPOSTFIX"].ToString(), // this.dgvserialnumberrule["snpostfix", i].Value.ToString(),
                         dr["VER"].ToString(), // this.dgvserialnumberrule["ver", i].Value.ToString(),
                         dr["REVE"].ToString(), // this.dgvserialnumberrule["reve", i].Value.ToString(),
                         dr["SNLENG"].ToString(), // this.dgvserialnumberrule["snleng", i].Value.ToString(),
                         dr["USENUM"].ToString()); // this.dgvserialnumberrule["usenum", i].Value.ToString());
                  if (!ass.ExecuteOracleCommand(sql))
                  {
                      ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{1}'", dr["WOID"].ToString()));
                      (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, string.Format("加载条码区间[{0}]失败,请联系SFIS人员", dr["SNTYPE"].ToString()));
                      ip_labletypes.Enabled = false;
                      return;
                  }
              }
          }
             dgv_snRange.DataSource = _mdt;      
        }

        private void dgv_woinfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FrmBLL.publicfuntion.UpdateFieldName(dgv_woinfo);
        }

        private void cmb_PlanOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region 获取当前选择的投产单信息 Mage
            Dt_Sel_Pan = FrmBLL.publicfuntion.getNewTable(dt_Plan, string.Format("PLAN_ORDER='{0}'", cmb_PlanOrder.SelectedValue.ToString()));
            Lb_PlanInfo.Items.Clear();
            // Lb_PlanInfo.Items.Add("产品型号：" + Dt_Sel_Pan.Rows[0]["PRODUCT_NAME"]);
            Lb_PlanInfo.Items.Add("投产单号：" + Dt_Sel_Pan.Rows[0]["PLAN_ORDER"]);
            //Lb_PlanInfo.Items.Add("投产单数量：" + Dt_Sel_Pan.Rows[0]["PLAN_QTY"]);
            foreach (DataRow item in Dt_Sel_Pan.Rows)
            {
                Lb_PlanInfo.Items.Add("号段类型：" + item["range_type"]);
                Lb_PlanInfo.Items.Add("号段起始值：" + item["PREFIX"] + item["FIRST_NO"] + (item["POSTFIX"].ToString() == "NA" ? "" : item["POSTFIX"]));
                Lb_PlanInfo.Items.Add("号段结束值：" + item["PREFIX"] + item["LAST_NO"] + (item["POSTFIX"].ToString() == "NA" ? "" : item["POSTFIX"]));
            }
            #endregion
        }

        private void ip_labletypes_ItemClick(object sender, EventArgs e)
        {
            try
            {

                if (sender is DevComponents.DotNetBar.CheckBoxItem)
                {
                    FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
                    //    if (!ChkPwd(((DevComponents.DotNetBar.CheckBoxItem)sender).Name))
                    //    {
                    #region SN修改需要权限
                    if (((DevComponents.DotNetBar.CheckBoxItem)sender).Name.ToUpper() == "SN")
                    {
                        //DataTable dt = _mdt;
                        //if (dt == null || dt.Rows.Count < 1)
                        //{
                        //    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error ,"工单未找到,请确认...");
                        //    return;
                        //}
                        if (Convert.ToInt32(dgv_woinfo.Rows[0].Cells["inputqty"].Value.ToString()) > 0)
                        {
                            string pwd = Input.InputQuery.ShowInputBox("输入密码", string.Empty, '*');
                            if (pwd != "phicomm_mes")
                            {
                                (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, "密码不符,请确认...");
                                return;
                            }
                        }
                    }
                    #endregion
                    if (((DevComponents.DotNetBar.CheckBoxItem)sender).Checked)
                    {//选中
                        this.cbSetEsn.Items.Add(((DevComponents.DotNetBar.CheckBoxItem)sender).Name);

                        if (MessageBoxEx.Show(string.Format("是否需要对[{0}]号设定区间?\n继续 , 请选择[Yes] ,不设定区间 , 请选择[No]",
                            ((DevComponents.DotNetBar.CheckBoxItem)sender).Name)
                             , "区间设定提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            Frm_AddSnRange asr = new Frm_AddSnRange(((DevComponents.DotNetBar.CheckBoxItem)sender).Name, dgv_woinfo.Rows[0].Cells["WOID"].Value.ToString(), this);
                            asr.ShowDialog();
                        }

                    }
                    else
                    {//取消选中
                        this.cbSetEsn.Items.Remove(((DevComponents.DotNetBar.CheckBoxItem)sender).Name);
                        if (MessageBoxEx.Show("查看数据请选择 [Yes] ,清除原来的数据请选择 [No]", "提示",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            ((DevComponents.DotNetBar.CheckBoxItem)sender).Checked = true;
                            this.cbSetEsn.Items.Add(((DevComponents.DotNetBar.CheckBoxItem)sender).Name);
                            // this.ip_labletypes_ItemClick(sender, null);
                            Frm_AddSnRange asr = new Frm_AddSnRange(((DevComponents.DotNetBar.CheckBoxItem)sender).Name, dgv_woinfo.Rows[0].Cells["WOID"].Value.ToString(), this);
                            asr.ShowDialog();
                        }
                        else
                        {

                            ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{0}' and sntype='{1}'",
                                  dgv_woinfo.Rows[0].Cells["WOID"].Value.ToString(), ((DevComponents.DotNetBar.CheckBoxItem)sender).Name));
                        }

                        // this.NewLableTypesValues.Remove(((DevComponents.DotNetBar.CheckBoxItem)sender).Name);
                    }
                    //}
                    //else
                    //{
                    //    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Warning,string.Format("当前选择的属于密码类型{0}",
                    //        ((DevComponents.DotNetBar.CheckBoxItem)sender).Name));
                    //}
                    //  }

                    dgv_snRange.DataSource = ass.GetDatatable(string.Format("select distinct  woId,sntype,snstart,snend,snprefix,snpostfix,ver,reve,snleng,usenum from wosnrule where woId='{0}' ",
                    dgv_woinfo.Rows[0].Cells["WOID"].Value.ToString()));

                }
            }
            catch (Exception ex)
            {
                (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, ex.Message);

            }
        }

        private void dgv_snRange_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FrmBLL.publicfuntion.UpdateFieldName(dgv_snRange);
        }

        private void chk_krms_Click(object sender, EventArgs e)
        {
            cmb_PlanOrder.Enabled = chk_krms.Checked;
            Lb_PlanInfo.Enabled = chk_krms.Checked;
        }

        private void imbt_save_Click(object sender, EventArgs e)
        {
             string _StrErr=string.Empty;
            string JsonStr = string.Empty;
            if (dgv_woinfo.Rows.Count == 0)
            {
                (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, "请先输入工单....");
                return;
            }
            if (MessageBox.Show("确定保存条码区间?", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string woId = dgv_woinfo.Rows[0].Cells["WOID"].Value.ToString();
                IList<IDictionary<string, object>> diclssnrule = new List<IDictionary<string, object>>();
                IDictionary<string, object> dic = null;

                for (int i = 0; i < this.ip_labletypes.Items.Count; i++)
                {
                    if (((DevComponents.DotNetBar.CheckBoxItem)this.ip_labletypes.Items[i]).Checked)
                    {
                        foreach (DataGridViewRow dgvr in dgv_snRange.Rows)
                        {
                            if (dgvr.Cells["sntype"].Value.ToString() == ((DevComponents.DotNetBar.CheckBoxItem)this.ip_labletypes.Items[i]).Name)
                            {
                                if (_woType != "Rework" && _woType != "RMA")
                                {
                                    _StrErr = refWebtWoInfo.Instance.ChkSerialNumberRule_New(dgvr.Cells["woId"].Value.ToString(), dgvr.Cells["snstart"].Value.ToString(), dgvr.Cells["snend"].Value.ToString());
                                    if (_StrErr.ToUpper() != "OK")
                                    {
                                        (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, _StrErr);
                                        return;
                                    }
                                }
                                dic = new Dictionary<string, object>();
                                dic.Add("WOID", dgvr.Cells["woId"].Value.ToString());
                                dic.Add("SNTYPE", dgvr.Cells["sntype"].Value.ToString());
                                dic.Add("SNSTART", dgvr.Cells["snstart"].Value.ToString());
                                dic.Add("SNEND", dgvr.Cells["snend"].Value.ToString());
                                dic.Add("SNPREFIX", dgvr.Cells["snprefix"].Value.ToString());
                                dic.Add("SNPOSTFIX", dgvr.Cells["snpostfix"].Value.ToString());
                                dic.Add("VER", dgvr.Cells["ver"].Value.ToString());
                                dic.Add("SNLENG", int.Parse(dgvr.Cells["snleng"].Value.ToString()));
                                dic.Add("USENUM", dgvr.Cells["usenum"].Value.ToString());
                                diclssnrule.Add(dic);
                            }
                        }
                    }
                }
                if (diclssnrule.Count == 0)
                {
                    dic = new Dictionary<string, object>(); //如果没有添加区间信息,后台判定SNTYPE=NA,则不增加区间
                    dic.Add("WOID", dgv_woinfo.Rows[0].Cells["WOID"].Value.ToString());
                    dic.Add("SNTYPE", "NA");
                    dic.Add("SNSTART", "NA");
                    dic.Add("SNEND", "NA");
                    dic.Add("SNPREFIX", "NA");
                    dic.Add("SNPOSTFIX", "NA");
                    dic.Add("VER", "NA");
                    dic.Add("SNLENG", 0);
                    dic.Add("USENUM", "0");
                    diclssnrule.Add(dic);
                }

                JsonStr=FrmBLL.ReleaseData.ListDictionaryToJson(diclssnrule);
                _StrErr = "OK";
               

                     _StrErr = refWebtWoInfo.Instance.Insert_Wo_Info(null, null, JsonStr, cbSetEsn.Text);               
                 
                if (_StrErr == "OK")
                {
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, "条码区间保存成功");

                    if (chk_krms.Checked)
                    {
                        #region 以工单的形式下载数据
                        //GetWoSnRule
                        if (FrmBLL.ReleaseData.arrByteToDataSet(refWebtWoInfo.Instance.GetWoSnRule(woId, null)).Tables[0].Rows.Count <= 0)
                        {
                            string aa = "";
                            string bb = aa;
                            #region 按照选定的参数下载资料
                            //  List<string> RangeType = new List<string>();
                            foreach (var ip_item in ip_labletypes.Items)
                            {
                                // if(((DevComponents.DotNetBar.CheckBoxItem)item).Checked)
                                if (((DevComponents.DotNetBar.CheckBoxItem)ip_item).Checked)
                                {
                                    DataRow[] dt_IsInKrms = dt_Numtye.Select(string.Format("TYPE_NAME='{0}'", ((DevComponents.DotNetBar.CheckBoxItem)ip_item).Name));

                                    if (dt_IsInKrms.Length > 0)
                                    {
                                        Inser_TwoInfo info = new Inser_TwoInfo
                                        {
                                            IN_USERID = (mFrm as Frm_MO_Manage).UserId,
                                            IN_BUFFER = 0,
                                            IN_PLANORDER = cmb_PlanOrder.Text,
                                            IN_WOID = woId,
                                            IN_REMARK = "NA",
                                            IN_WOQTY = int.Parse(dgv_woinfo.Rows[0].Cells["QTY"].Value.ToString()),
                                            IN_RANGETYPE = ((DevComponents.DotNetBar.CheckBoxItem)ip_item).Name
                                        };

                                        string ERR = refWebt_woInfo.Instance.inser_WoList_New(info);
                                        if (ERR != "OK")
                                        {
                                            (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, ERR);
                                            MessageBox.Show("ERROR:KRMS IS ERROR：\r\n" + ERR);
                                            return;
                                        }
                                    }
                                }
                            }
                            DataTable dt_WO_Info_SFIS = FrmBLL.ReleaseData.arrByteToDataTable(refWebt_woInfo.Instance.Sel_WoInfo(woId, ""));

                            #endregion
                            #region 将数据新增到SFIS
                            string sfis_sta = "";
                            string sfis_End = "";
                            //  WebServices.tWoInfo.WoSnRule[] listwosn = new WebServices.tWoInfo.WoSnRule[dt_WO_Info_SFIS.Rows.Count];
                            IList<IDictionary<string, object>> LsDic = new List<IDictionary<string, object>>();
                            IDictionary<string, object> dicmst = null;
                            int i = 0;

                            foreach (DataRow item in dt_WO_Info_SFIS.Rows)
                            {
                                sfis_sta = item["PREFIX"].ToString() + item["FIRST_NO"].ToString() + (item["POSTFIX"].ToString() == "NA" ? "" : item["POSTFIX"].ToString());
                                sfis_End = item["PREFIX"].ToString() + item["LAST_NO"].ToString() + (item["POSTFIX"].ToString() == "NA" ? "" : item["POSTFIX"].ToString());

                                int RangeIndex = StrCompare(sfis_sta, sfis_End);
                                dicmst = new Dictionary<string, object>();
                                dicmst.Add("REVE", "NA");
                                dicmst.Add("VER", dgv_woinfo.Rows[0].Cells["PVER"].Value.ToString());
                                dicmst.Add("SNSTART", sfis_sta);
                                dicmst.Add("SNEND", sfis_End);
                                dicmst.Add("SNLENG", int.Parse(item["name_len"].ToString()));
                                dicmst.Add("SNPREFIX", sfis_sta.Substring(0, RangeIndex));
                                dicmst.Add("SNPOSTFIX", item["POSTFIX"].ToString());
                                dicmst.Add("SNTYPE", item["RANGE_TYPE"].ToString());
                                dicmst.Add("USENUM", item["unit_qty"].ToString());
                                dicmst.Add("WOID", item["WORK_ORDER"].ToString());
                                LsDic.Add(dicmst);
                                i++;
                            }
                            JsonStr = FrmBLL.ReleaseData.ListDictionaryToJson(LsDic);
                         //   string str_Status = refWebtWoInfo.Instance.InsertWoSnRule(JsonStr);
                            string str_Status = refWebtWoInfo.Instance.Insert_Wo_Info(null, null, JsonStr, cbSetEsn.Text);
                         
                            //Txt_Status.Text = str_Status;

                            // this.mFrm.ShowPrgMsg(str_Status, MainParent.MsgType.Incoming);
                            (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, str_Status);

                            #endregion


                        }
                        #endregion

                    }
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Normal, "正在写入日志...");
                    FrmBLL.publicfuntion.InserSystemLog((mFrm as Frm_MO_Manage).UserId, "工单信息", "SnRange_" + dgv_woinfo.Rows[0].Cells["WOID"].Value.ToString(), JsonStr);
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Incoming, "增加区间信息完成");
                    this.dgv_snRange.DataSource = null;
                    this.dgv_woinfo.DataSource = null;
                    this.ip_labletypes.Items.Clear();
                    ip_labletypes.Refresh();
                }
                else
                {
                    (mFrm as Frm_MO_Manage).SendMsg(Frm_MO_Manage.mLogMsgType.Error, "添加区间失败:" + _StrErr);
                }
            }
        }
        #region 字符串比对 Mage
        public static int StrCompare(string Str1, string Str2)
        {
            char[] char1 = Str1.ToCharArray();
            char[] char2 = Str2.ToCharArray();
            int i = 0;
            for (; i < char1.Length; i++)
            {
                if (char1[i] != char2[i])
                {
                    break;
                }

            }
            return i;
        }
        #endregion

        private void imbt_ConverSnList_Click(object sender, EventArgs e)
        {
            FrmGetDetailedSnList fgs = new FrmGetDetailedSnList();
            fgs.ShowDialog();
        }
    }
}