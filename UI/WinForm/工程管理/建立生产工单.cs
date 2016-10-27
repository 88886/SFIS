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
using System.Linq;


namespace SFIS_V2
{
    public partial class WorkOrderCreate : Office2007Form// Form
    {
        public WorkOrderCreate(MainParent _mfrm)
        {
            InitializeComponent();
            mFrm = _mfrm;
        }
        private readonly string strFunName = "WoEdit";
        MainParent mFrm = null;
        public List<string> LsIp = FrmBLL.publicfuntion.GetIPList();
        private List<string> mAllPwdColumns = new List<string>();
        /// <summary>
        /// 工单类型
        /// </summary>
        //  public Entity.T_WO_INFO.SnType mWosntype;
       // public List<WebServices.tWoInfo.tUsePwd> lstUserPwd = new List<WebServices.tWoInfo.tUsePwd>();
        string[] mBomNumberList = null;
        public DataTable gWoInfodt = new DataTable();
        private ecpwd mecpwd;
        public enum ecpwd
        {
            PROG,
            FILE,
            USERDEF
        } 

        public string _SapWoType;
        public string _SfcWoType;
        public int woid_status = 0;

        /// <summary>
        /// 工单产出数量
        /// </summary>
        private string InputQTY = "0";

        Dictionary<string, string> ListProduct = new Dictionary<string, string>();

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

      
        private void bt_selectwo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.tb_selectwoname.Text))
                    FrmBLL.publicfuntion.SelectDataGridViewRows(tb_selectwoname.Text, dgv_showwoinfo, 0);
                //  FillDataGridView(FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfoByWo(this.tb_selectwoname.Text.Trim()))/*BLL.tWoInfo.GetWoInfoByWo(this.tb_selectwoname.Text.Trim())*/);
                else
                    FillDataGridView(GetAllWoInfo()/*BLL.tWoInfo.GetAllWoInfo*/);
            }
            catch (Exception ex)
            {
                mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void FillDataGridView(DataTable _dt)
        {
            this.dgv_showwoinfo.Invoke(new EventHandler(delegate
            {
                this.dgv_showwoinfo.DataSource = _dt;
            }));
        }

        private void tb_partnumber_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_workordername.Text))
            {
                MessageBoxEx.Show("请先填写工单号");
                this.tb_partnumber.Text = "";
                this.tb_workordername.Focus();
                return;
            }
            if (ListProduct.ContainsKey(this.tb_partnumber.Text.Trim()))
                tb_product.Text = ListProduct[string.IsNullOrEmpty(this.tb_partnumber.Text.Trim()) ? "NA" : this.tb_partnumber.Text.Trim()];
            else
                tb_product.Text = "NA";
            if (string.IsNullOrEmpty(this.tb_partnumber.Text))
                return;

            #region 检查当前编辑是否被锁住

            #endregion

            string[] names = refWebtProduct.Instance.GetProductLableNames(this.tb_partnumber.Text);// BLL.tProduct.GetProductLableNames(this.tb_partnumber.Text);
            this.ip_labletypes.Items.Clear();
            this.cbSetEsn.Items.Clear();
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

            #region 添加流程
            mRouteTable = FrmBLL.ReleaseData.arrByteToDataTable(refWebtRouteInfo.Instance.GetRouteManageByPartnumber(this.tb_partnumber.Text.Trim()));
            //this.tb_routgroupid.Items.Clear();
            this.tb_routgroupid.Text = "";
            this.tb_inputstation.Text = "";
            this.tb_outputstation.Text = "";
            this.tb_routgroupid.Items.Clear();
            foreach (DataRow dr in mRouteTable.DefaultView.ToTable(true, "routgroupId").Rows)
            {
                //this.cb_bomnumber.Items.Add(dr["bomnumber"].ToString());
                this.tb_routgroupid.Items.Add(dr["routgroupId"].ToString());
            }
            if (this.tb_routgroupid.Items.Count == 1)
            {
                this.tb_routgroupid.SelectedIndex = 0;
            }
            #endregion

            #region 锁住当前编辑

            #endregion
        }

        private void cbi_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            MessageBoxEx.Show("dddd");
        }

        private void InitControlText()
        {
            this.tb_workordername.Text = "";
            this.tb_partnumber.Text = "";
            this.tb_poname.Text = "";
            this.tb_woqty.Text = "";
            this.tb_inputstation.Text = "";
            this.tb_outputstation.Text = "";
            this.tb_pver.Text = "";
            this.tb_bomver.Text = "";
            this.cb_bomnumber.Text = "";
            this.ip_labletypes.Items.Clear();
            this.ip_labletypes.Refresh();
            this.tb_routgroupid.Items.Clear();
            this.cbSetEsn.Items.Clear();
            this.listbScript.Items.Clear();
            this.tb_Fw.Text = "NA";
            this.tb_nal.Text = "NA";
            this.tb_sw.Text = "NA";
            this.tb_chkno.Text = "NA";
        }
        private void bt_savewoinfo_Click(object sender, EventArgs e)
        {
            try
            {

                #region 基本条件判定
                if (string.IsNullOrEmpty(this.cbSetEsn.Text.Trim()))
                {
                    if (MessageBox.Show("没有指定ESN\n 是否确定 ?? \n返回请选择[NO],继续请选择[Yes]", "提示",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    {
                        return;
                    }
                }
                if (string.IsNullOrEmpty(this.tb_workordername.Text))
                    throw new Exception("请填写生产工单号");
                if (string.IsNullOrEmpty(this.tb_partnumber.Text))
                    throw new Exception("请填写成品料号");
                if (string.IsNullOrEmpty(this.tb_woqty.Text))
                    throw new Exception("工单数量不能为空");
                if (string.IsNullOrEmpty(this.tb_bomver.Text))
                    throw new Exception("请填写BOM版本");
                if (string.IsNullOrEmpty(this.cbwotye.Text))
                    throw new Exception("请选择工单类型");
                #endregion
                //if (string.IsNullOrEmpty(this.tb_inputstation.Text))
                //    throw new Exception("流程不存在入口站!!");
                //if (string.IsNullOrEmpty(this.tb_outputstation.Text))
                //    throw new Exception("流程不存在出口站!!");

                if (string.IsNullOrEmpty(this.tb_routgroupid.Text) ||
                    string.IsNullOrEmpty(this.cb_bomnumber.Text))
                {
                    if (MessageBoxEx.Show("部分工单信息没有填写,是否继续?\n\n继续[Yes] 返回[No]",
                         "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
                    {
                        this.mFrm.ShowPrgMsg("工单没有保存", MainParent.MsgType.Warning);
                        return;
                    }
                }

                   string _StrErr= CHK_PACKING_ROUTE();
                   if (_StrErr != "OK")
                   {
                       MessageBox.Show(_StrErr, "判定信息提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                       return;
                   }

                FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
              //  List<WebServices.tWoInfo.WoSnRule> lssnrule = new List<WebServices.tWoInfo.WoSnRule>();
                IList<IDictionary<string, object>> diclssnrule = new List<IDictionary<string, object>>();
                IDictionary<string, object> dic = null;

                for (int i = 0; i < this.ip_labletypes.Items.Count; i++)
                {
                    if (((DevComponents.DotNetBar.CheckBoxItem)this.ip_labletypes.Items[i]).Checked)
                    {
                        DataTable _mdt = ass.GetDatatable(string.Format("select distinct  woId,sntype,snstart,snend,snprefix,snpostfix,ver,reve,snleng,usenum from wosnrule where woId='{0}' and sntype='{1}'",
                            this.tb_workordername.Text.Trim(), this.ip_labletypes.Items[i].Name));
                        foreach (DataRow dr in _mdt.Rows)
                        {
                            dic = new Dictionary<string, object>();
                            dic.Add("WOID", dr["woId"].ToString());
                            dic.Add("SNTYPE", dr["sntype"].ToString());
                            dic.Add("SNSTART", dr["snstart"].ToString());
                            dic.Add("SNEND", dr["snend"].ToString());
                            dic.Add("SNPREFIX", dr["snprefix"].ToString());
                            dic.Add("SNPOSTFIX", dr["snpostfix"].ToString());
                            dic.Add("VER", dr["ver"].ToString());
                            dic.Add("SNLENG", int.Parse(dr["snleng"].ToString()));
                            dic.Add("USENUM", dr["usenum"].ToString());
                            diclssnrule.Add(dic);
                            //lssnrule.Add(new WebServices.tWoInfo.WoSnRule()
                            //{
                            //    woid = dr["woId"].ToString(),
                            //    sntype = dr["sntype"].ToString(),
                            //    snstart = dr["snstart"].ToString(),
                            //    snend = dr["snend"].ToString(),
                            //    snprefix = dr["snprefix"].ToString(),
                            //    snpostfix = dr["snpostfix"].ToString(),
                            //    ver = dr["ver"].ToString(),
                            //    reve = dr["reve"].ToString(),
                            //    snleng = int.Parse(dr["snleng"].ToString()),
                            //    usenum = dr["usenum"].ToString()
                            //});
                        }
                    }
                }

                string atescript = string.Empty;
                for (int i = 0; i < this.listbScript.Items.Count; i++)
                {
                    atescript += this.listbScript.Items[i].ToString() + ",";
                }
                if (atescript != null && !string.IsNullOrEmpty(atescript))
                {
                    atescript = atescript.Substring(0, atescript.Length - 1);
                }
                Dictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOID", this.tb_workordername.Text.Trim());
                mst.Add("POID", this.tb_poname.Text.Trim());
                mst.Add("PARTNUMBER",this.tb_partnumber.Text.Trim());
                mst.Add("BOMVER",this.tb_bomver.Text.Trim());
                mst.Add("QTY",int.Parse(this.tb_woqty.Text));
                mst.Add("USERID",this.mFrm.gUserInfo.userId);
                mst.Add("WOSTATE", woid_status);
                mst.Add("BOMNUMBER",this.cb_bomnumber.Text.Trim());
                mst.Add("INPUTGROUP",this.tb_inputstation.Text);
                mst.Add("OUTPUTGROUP",this.tb_outputstation.Text);
                mst.Add("ROUTGROUPID",this.tb_routgroupid.Text.Trim());
                mst.Add("SAPWOTYPE",string.IsNullOrEmpty(_SapWoType) ? "NA" : _SapWoType);
                mst.Add("INPUTQTY",0);
                mst.Add("OUTPUTQTY", 0);
                mst.Add("PVER", this.tb_pver.Text.Trim());
                mst.Add("WOTYPE", this.cbwotye.Text.Trim());
                mst.Add("SCRAPQTY", 0);
                mst.Add("CPWD",this.mecpwd);
                mst.Add("SW_VER",string.IsNullOrEmpty(tb_sw.Text) ? "NA" : tb_sw.Text);
                mst.Add("FW_VER",string.IsNullOrEmpty(tb_Fw.Text) ? "NA" : tb_Fw.Text);
                mst.Add("NAL_PREFIX", string.IsNullOrEmpty(tb_nal.Text) ? "NA" : tb_nal.Text);
                if (ListProduct.ContainsKey(this.tb_partnumber.Text.Trim()))
                mst.Add("PRODUCTNAME",ListProduct[this.tb_partnumber.Text.Trim()]);
                else
                    mst.Add("PRODUCTNAME","NA");
                 mst.Add("CHECK_NO",string.IsNullOrEmpty(tb_chkno.Text) ? "NA" : tb_chkno.Text);
                           
                 string sRES = refWebtWoInfo.Instance.InsertWoInfo(FrmBLL.ReleaseData.DictionaryToJson(mst), atescript, this.cbSetEsn.Text, FrmBLL.ReleaseData.ListDictionaryToJson(diclssnrule));
                //string sRES = refWebtWoInfo.Instance.InsertWoInfo(new WebServices.tWoInfo.T_WO_INFO()
                // {
                //     woId = this.tb_workordername.Text.Trim(),
                //     poId = this.tb_poname.Text.Trim(),
                //     partnumber = this.tb_partnumber.Text.Trim(),
                //     bomver = this.tb_bomver.Text.Trim(),
                //     qty = int.Parse(this.tb_woqty.Text),
                //     userId = this.mFrm.gUserInfo.userId,
                //     wostate = 0,
                //     bomnumber = this.cb_bomnumber.Text.Trim(),
                //     inputgroup = this.tb_inputstation.Text,
                //     outputgroup = this.tb_outputstation.Text,
                //     routgroupId = this.tb_routgroupid.Text.Trim(),
                //     sapwotype = string.IsNullOrEmpty(_SapWoType) ? "NA" : _SapWoType,
                //     inputqty = 0,
                //     outputqty = 0,
                //     per = this.tb_pver.Text.Trim(),
                //     wotype = this.cbwotye.Text.Trim(),
                //     scrapqty = 0,
                //     strAteScript = atescript,
                //     cpwd = this.mecpwd,
                //     sw_ver = string.IsNullOrEmpty(tb_sw.Text) ? "NA" : tb_sw.Text,
                //     fw_ver = string.IsNullOrEmpty(tb_Fw.Text) ? "NA" : tb_Fw.Text,
                //     nal_prefix = string.IsNullOrEmpty(tb_nal.Text) ? "NA" : tb_nal.Text,
                //     ProductName = ListProduct[this.tb_partnumber.Text.Trim()],
                //     CHECK_NO = string.IsNullOrEmpty(tb_chkno.Text) ? "NA" : tb_chkno.Text,
                //   //  BI_Proportion=cb_Proportion.Text,
                //    // BI_Warning= cb_Warning.Text,
                //   //  CHK_BI_ROUTE=tb_chkbicraft.Text

                // }, this.cbSetEsn.Text, lssnrule.ToArray());
                //工单保存后将本地保存的区间缓存删除
                if (sRES != "OK")
                    MessageBox.Show(sRES);
                else
                {
                    if (Chk_Plan.Checked)
                    {
                        #region 以工单的形式下载数据
                        //GetWoSnRule
                        if (FrmBLL.ReleaseData.arrByteToDataSet(refWebtWoInfo.Instance.GetWoSnRule(tb_workordername.Text.Trim(), null)).Tables[0].Rows.Count <= 0)
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
                                            IN_USERID = mFrm.gUserInfo.userId,
                                            IN_BUFFER = 0,
                                            IN_PLANORDER = cmb_PlanOrder.Text,
                                            IN_WOID = tb_workordername.Text,
                                            IN_REMARK = "NA",
                                            IN_WOQTY = int.Parse(tb_woqty.Text),
                                            IN_RANGETYPE = ((DevComponents.DotNetBar.CheckBoxItem)ip_item).Name
                                        };
                                        string ERR = refWebt_woInfo.Instance.inser_WoList_New(info);
                                       /// dt_WO_Info_SFIS=dt_WO_Info.Tables[0].Clone();
                                        if (ERR!="OK")
                                         {
                                             MessageBox.Show("ERROR:KRMS IS ERROR");
                                         }
                                        
                                    }

                                }
                            }
                            DataTable dt_WO_Info_SFIS = FrmBLL.ReleaseData.arrByteToDataTable(refWebt_woInfo.Instance.Sel_WoInfo(tb_workordername.Text, ""));

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
                                dicmst.Add("VER", tb_pver.Text);
                                dicmst.Add("SNSTART", sfis_sta);
                                dicmst.Add("SNEND", sfis_End);
                                dicmst.Add("SNLENG", int.Parse(item["name_len"].ToString()));
                                dicmst.Add("SNPREFIX", sfis_sta.Substring(0, RangeIndex));
                                dicmst.Add("SNPOSTFIX", item["POSTFIX"].ToString());
                                dicmst.Add("SNTYPE", item["RANGE_TYPE"].ToString());
                                dicmst.Add("USENUM", item["unit_qty"].ToString());
                                dicmst.Add("WOID", item["WORK_ORDER"].ToString());
                                LsDic.Add(dicmst);
                                //WebServices.tWoInfo.WoSnRule snrule = new WebServices.tWoInfo.WoSnRule
                                //{
                                //    reve = "NA",
                                //    ver = tb_pver.Text,
                                //    snstart = sfis_sta,
                                //    snend = sfis_End,
                                //    snleng = int.Parse(item["name_len"].ToString()),
                                //    snprefix = sfis_sta.Substring(0, RangeIndex),
                                //    snpostfix = item["POSTFIX"].ToString(),
                                //    sntype = item["RANGE_TYPE"].ToString(),
                                //    usenum = item["unit_qty"].ToString(),
                                //    woid = item["WORK_ORDER"].ToString()

                                //};

                                //新增到SFIS
                               // listwosn[i] = snrule;
                                i++;
                            }

                            string str_Status = refWebtWoInfo.Instance.InsertWoSnRule(FrmBLL.ReleaseData.ListDictionaryToJson(LsDic));
                            //Txt_Status.Text = str_Status;

                            this.mFrm.ShowPrgMsg(str_Status, MainParent.MsgType.Incoming);
                            #endregion






                            //查询
                            //DataTable dt_WO_Info = ReleaseData.arrByteToDataSet(refWebt_woInfo.Instance.Sel_WoInfo(cmb_woInfo.Text, "")).Tables[0];

                        }
                        #endregion

                    }

                }




                ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{0}'", this.tb_workordername.Text.Trim()));
                this.mFrm.ShowPrgMsg(string.Format("工单:\"{0}\"已经保存", this.tb_workordername.Text), MainParent.MsgType.Outgoing);

                string LogSnList = "";
                //foreach (WebServices.tWoInfo.WoSnRule item in lssnrule)
                //{
                //    LogSnList += item.sntype + "[" + item.snstart + "--" + item.snend + "]";
                //}
                foreach (Dictionary<string, object> item in diclssnrule)
                {
                    LogSnList += item["SNTYPE"] + "[" + item["SNSTART"] + "--" + item["SNEND"] + "]";
                }
                
                LogSnList = "工单信息: " + this.tb_workordername.Text.Trim() + LogSnList;
                if (LogSnList.Length > 512)
                {
                    LogSnList = LogSnList.Substring(0, 510);
                }

                 
                FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "工单信息", "工单", LogSnList + " TargetQTY:" + tb_woqty.Text);


                #region 释放被锁住的资源
                if (refwebtEditing.Instance.DeletetEditingByfunname(this.tb_workordername.Text.Trim()) != "OK")
                {
                    this.mFrm.ShowPrgMsg("资源释放失败..", MainParent.MsgType.Error);
                }
                #endregion
                this.InitControlText();
                this.dgv_showwoinfo.DataSource =GetAllWoInfo();
                tb_partnumber.ReadOnly = false;
                tb_woqty.ReadOnly = false;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("FK_TWORKORD_REFERENCE_TPRODUCT") != -1)
                    this.mFrm.ShowPrgMsg(string.Format("成品料号:{0}不存在,请先添加对应的产品!!",
                    this.tb_partnumber.Text), MainParent.MsgType.Error);
                else
                    this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
            TextBoxReadOnly(true);
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

        /// <summary>
        /// 检查是否为密码类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool ChkPwd(string name)
        {
            foreach (string str in this.mAllPwdColumns)
            {
                if (str.ToUpper() == name.ToUpper())
                    return true;
            }
            return false;
        }




        private void ip_labletypes_ItemClick(object sender, EventArgs e)
        {
            try
            {
                if (sender is DevComponents.DotNetBar.CheckBoxItem)
                {
                    if (!ChkPwd(((DevComponents.DotNetBar.CheckBoxItem)sender).Name))
                    {
                        #region SN修改需要权限
                        if (((DevComponents.DotNetBar.CheckBoxItem)sender).Name.ToUpper() == "SN")
                        {
                            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(this.tb_workordername.Text,null));
                            if (dt == null || dt.Rows.Count < 1)
                            {
                                mFrm.ShowPrgMsg("工单未找到,请确认...", MainParent.MsgType.Error);
                                return;
                            }
                            if (Convert.ToInt32(dt.Rows[0]["inputqty"]) > 0)
                            {
                                string pwd = Input.InputQuery.ShowInputBox("输入密码", string.Empty, '*');
                                if (pwd != "feixun67754400")
                                {
                                    mFrm.ShowPrgMsg("密码不符,请确认...", MainParent.MsgType.Error);
                                    return;
                                }
                            }
                        }
                        #endregion
                        if (((DevComponents.DotNetBar.CheckBoxItem)sender).Checked)
                        {//选中
                            this.cbSetEsn.Items.Add(((DevComponents.DotNetBar.CheckBoxItem)sender).Name);
                            if (string.IsNullOrEmpty(this.tb_workordername.Text) || string.IsNullOrEmpty(this.cbwotye.Text))
                            {
                                MessageBoxEx.Show("请先工单号和工单类型");
                                return;
                            }
                            else
                            {
                                if (MessageBoxEx.Show(string.Format("是否需要对[{0}]号设定区间?\n继续 , 请选择[Yes] ,不设定区间 , 请选择[No]",
                                    ((DevComponents.DotNetBar.CheckBoxItem)sender).Name)
                                     , "区间设定提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                {
                                    AddSNRange asr = new AddSNRange(this.mFrm, this.tb_workordername.Text,
                                        this, ((DevComponents.DotNetBar.CheckBoxItem)sender).Name);
                                    asr.ShowDialog();
                                }
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
                                AddSNRange asr = new AddSNRange(this.mFrm, this.tb_workordername.Text,
                                    this, ((DevComponents.DotNetBar.CheckBoxItem)sender).Name);
                                asr.ShowDialog();
                            }
                            else
                            {
                                FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
                                ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{0}' and sntype='{1}'",
                                    this.tb_workordername.Text.Trim(), ((DevComponents.DotNetBar.CheckBoxItem)sender).Name));
                            }
                            // this.NewLableTypesValues.Remove(((DevComponents.DotNetBar.CheckBoxItem)sender).Name);
                        }
                    }
                    else
                    {
                        this.mFrm.ShowPrgMsg(string.Format("当前选择的属于密码类型{0}",
                            ((DevComponents.DotNetBar.CheckBoxItem)sender).Name), MainParent.MsgType.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void textBoxX1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }


        DataTable dt_Plan = new DataTable();//记录所有的在用号段
        DataTable Dt_Sel_Pan = new DataTable();//记录选中的号段信息
        DataTable dt_Numtye = new DataTable();//记录所有KRMS内号码类型
        /// <summary>
        /// 取得所有途程
        /// </summary>
      public  DataTable AllRouteInfo = new DataTable("mydt");

        private void WorkOrderCreate_Load(object sender, EventArgs e)
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

            InitializationWoInfo = new Initialization(InitWoInfo);
            InitializationWoInfo.BeginInvoke(null, null);
            TextBoxReadOnly(true);
        }

        private delegate void Initialization();
        Initialization InitializationWoInfo;

        private DataTable  GetAllWoInfo()
        {
            string woFlag = string.Empty;
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(null,null,null));

            if (radstatus0.Checked)
                woFlag = "0";
            if (radstatus1.Checked)
                woFlag = "1";
            if (radstatus2.Checked)
                woFlag = "2";
            if (radstatus3.Checked)
                woFlag = "3";
            if (radstatus4.Checked)
                woFlag = "4";         
          return  FrmBLL.publicfuntion.getNewTable(dt,string.Format("WOSTATE='{0}'",woFlag));
        }

        private void InitWoInfo()
        {
            this.Invoke(new EventHandler(delegate
            {
                try
                {

                    this.dgv_showwoinfo.DataSource = GetAllWoInfo();

                    //加载bom编号列表
                    mBomNumberList = refWebtBomKeyPart.Instance.GetBomNumerList();
                    this.cb_bomnumber.Items.AddRange(mBomNumberList);
                    this.cbCratePwd.SelectedIndex = 0;
                    this.mecpwd = ecpwd.PROG;

                    string[] arr = refWebtWipTracking.Instance.GetPwdColumns(string.Empty);
                    if (arr == null || arr.Length < 1)
                        throw new Exception("密码类型加载失败");
                    this.mAllPwdColumns.AddRange(arr);


                    #region 获取KRMS所有Release过的投产单  Mage
                    //获取所有release号段；并将状态该为用完
                    dt_Plan = FrmBLL.ReleaseData.arrByteToDataSet(refWEB_T_plan_list.Instance.Sel_plan_list_SFIS()).Tables[0];
                    dt_Numtye = FrmBLL.ReleaseData.arrByteToDataSet(refWEB_t_numtype_info.Instance.Sel_NumType()).Tables[0];
                    #region 新增到sfis内；并已存在的投产单查询出来 DEL
                    //WebServices.tWoInfo.WoSnRule[] listwosn = new WebServices.tWoInfo.WoSnRule[dt_Plan.Rows.Count];
                    //int i = 0;
                    //string sfis_sta = string.Empty;
                    //string sfis_End = string.Empty;
                    //foreach (DataRow item in dt_Plan.Rows)
                    //{
                    //    sfis_sta = item["PREFIX"].ToString() + item["FIRST_NO"].ToString() + (item["POSTFIX"].ToString() == "NA" ? "" : item["POSTFIX"].ToString());
                    //    sfis_End = item["PREFIX"].ToString() + item["LAST_NO"].ToString() + (item["POSTFIX"].ToString() == "NA" ? "" : item["POSTFIX"].ToString());

                    //    int RangeIndex = StrCompare(sfis_sta, sfis_End);

                    //    WebServices.tWoInfo.WoSnRule snrule = new WebServices.tWoInfo.WoSnRule
                    //    {
                    //        reve = "NA",
                    //        ver = "A",
                    //        snstart = sfis_sta,
                    //        snend = sfis_End,
                    //        snleng = int.Parse(item["name_len"].ToString()),
                    //        snprefix = sfis_sta.Substring(0, RangeIndex),
                    //        snpostfix = item["POSTFIX"].ToString(),
                    //        sntype = item["RANGE_TYPE"].ToString(),
                    //        usenum = item["unit_qty"].ToString(),
                    //        woid = item["plan_ORDER"].ToString()

                    //    };

                    //    //新增到SFIS
                    //    listwosn[i] = snrule;
                    //    i++;
                    //}

                    //string str_Status = refWebtWoInfo.Instance.InsertWoSnRule(listwosn);

                    // dt_Plan = new DataTable();
                    // dt_Plan = FrmBLL.ReleaseData.arrByteToDataSet(refWebtWoInfo.Instance.GetALLWoSnRule()).Tables[0];
                    #endregion
                    //绑定到cmb_PlanOrder
                    DataTable dtNew = new DataTable();
                    dtNew = dt_Plan.DefaultView.ToTable(true, "PLAN_ORDER");
                    cmb_PlanOrder.DisplayMember = "PLAN_ORDER";
                    cmb_PlanOrder.ValueMember = "PLAN_ORDER";
                    cmb_PlanOrder.DataSource = dtNew;
                    #endregion


                    Chk_Plan.Checked = false;
                    cmb_PlanOrder.Enabled = false;
                    Lb_PlanInfo.Enabled = false;

                    AllRouteInfo = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtRouteInfo.Instance.GetAllRouteInfo());
                   
                }
                catch (Exception ex)
                {
                    this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                }

                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetAllProduct());
                foreach (DataRow dr in dt.Rows)
                {
                    ListProduct.Add(dr[0].ToString(), dr[2].ToString());
                }
                ListProduct.Add("NA", "NA");
            }));
        }

        string aaa = string.Empty;
        private void dgv_showwoinfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    ////获取工单状态
                    //if ((sWoStateTemp = refWebtWoInfo.Instance.GetWoState(sWoIdTemp = this.dgv_showwoinfo["woId", e.RowIndex].Value.ToString())) != "ERR")
                    //{

                    //}
                    //else
                    //{
                    //    sWoStateTemp = string.Empty;
                    //}

                    this.tb_workordername.Text = this.dgv_showwoinfo["woId", e.RowIndex].Value.ToString();

                    #region 检查是否已经被编辑
                    //string err = refwebtEditing.Instance.ChktEditing(new WebServices.tEditing.tEditing1()
                    //{
                    //    prj = this.strFunName,
                    //    funname = this.tb_workordername.Text.Trim(),
                    //    userId = this.mFrm.gUserInfo.userId,
                    //    username = this.mFrm.gUserInfo.username,
                    //    PC_NAME = FrmBLL.publicfuntion.HostName + " IP:" + ((LsIp.Count > 1) ? LsIp[1] : LsIp[0]),
                    //    MAC_ADD = FrmBLL.publicfuntion.getMacList()[0]
                    //});
                    string err = FrmBLL.publicfuntion.ChktEditing(this.tb_workordername.Text.Trim(), this.strFunName, this.mFrm.gUserInfo.userId, this.mFrm.gUserInfo.username);
                    if (err != "OK")
                    {
                        if (err.IndexOf("ERROR") != -1)
                        {
                            this.mFrm.ShowPrgMsg(err, MainParent.MsgType.Error);
                            return;
                        }
                        else
                        {
                            MessageBoxEx.Show(string.Format("用户:[{0}]/[{1}]--{2}正在编辑中,您现在不能进行修改!!",
                                err.Split(':')[1].Split(',')[0], err.Split(':')[1].Split(',')[1], err.Split(':')[1].Split(',')[2]), "提示!!");
                            return;
                        }
                    }
                    #endregion
                    this.tb_poname.Text = this.dgv_showwoinfo["poId", e.RowIndex].Value.ToString();
                    this.tb_woqty.Text = this.dgv_showwoinfo["qty", e.RowIndex].Value.ToString();
                    this.tb_partnumber.Text = this.dgv_showwoinfo["partnumber", e.RowIndex].Value.ToString();
                    this.tb_partnumber_Leave(null, null);
                    this.tb_bomver.Text = this.dgv_showwoinfo["bomver", e.RowIndex].Value.ToString();
                    this.cb_bomnumber.Text = this.dgv_showwoinfo["bomnumber", e.RowIndex].Value.ToString();
                    this.tb_pver.Text = this.dgv_showwoinfo["pver", e.RowIndex].Value.ToString();
                    this.cbwotye.Text = this.dgv_showwoinfo["wotype", e.RowIndex].Value.ToString();
                    this._SfcWoType = this.dgv_showwoinfo["wotype", e.RowIndex].Value.ToString();
                    this._SapWoType = this.dgv_showwoinfo["sapwotype", e.RowIndex].Value.ToString();
                    this.tb_sw.Text = this.dgv_showwoinfo["SW_VER", e.RowIndex].Value.ToString();
                    this.tb_Fw.Text = this.dgv_showwoinfo["FW_VER", e.RowIndex].Value.ToString();
                    this.tb_nal.Text = this.dgv_showwoinfo["NAL_PREFIX", e.RowIndex].Value.ToString();
                    this.tb_chkno.Text = this.dgv_showwoinfo["CHECK_NO", e.RowIndex].Value.ToString();
                    InputQTY = this.dgv_showwoinfo["INPUTQTY", e.RowIndex].Value.ToString();
                    this.tb_routgroupid.Items.Clear();
                    if (!string.IsNullOrEmpty(this.dgv_showwoinfo["routgroupId", e.RowIndex].Value.ToString()))
                    {
                        this.mRouteTable = new DataTable("rout");
                        mRouteTable.Columns.Add("routgroupId", typeof(string));
                        mRouteTable.Columns.Add("craftname", typeof(string));
                        mRouteTable.Columns.Add("routedesc", typeof(string));

                        mRouteTable.Rows.Add(this.dgv_showwoinfo["routgroupId", e.RowIndex].Value.ToString(),
                            this.dgv_showwoinfo["inputgroup", e.RowIndex].Value.ToString(), "IN");
                        mRouteTable.Rows.Add(this.dgv_showwoinfo["routgroupId", e.RowIndex].Value.ToString(),
                            this.dgv_showwoinfo["outputgroup", e.RowIndex].Value.ToString(), "OUT");

                        this.tb_routgroupid.Items.Add(this.dgv_showwoinfo["routgroupId", e.RowIndex].Value.ToString());
                        this.tb_routgroupid.SelectedIndex = 0;
                    }
                    switch (this.dgv_showwoinfo["cpwd", e.RowIndex].Value.ToString().ToUpper())
                    {
                        case "PROG":
                            this.cbCratePwd.SelectedIndex = 0;
                            break;
                        case "FILE":
                            this.cbCratePwd.SelectedIndex = 1;
                            break;
                        case "USERDEF":
                            this.cbCratePwd.SelectedIndex = 2;
                            break;
                        default:
                            break;
                    }
                    this.listbScript.Items.Clear();
                    //加载脚本
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetATEScripts(this.tb_workordername.Text));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (string item in dt.Rows[0][0].ToString().Split(','))
                        {
                            this.listbScript.Items.Add(item);
                        }
                    }
                    GetFtp_ATE_SCRIPT();
                //    tb_partnumber.ReadOnly = true;
                //    tb_woqty.ReadOnly = true;
                }
                this.ShowWoSnRange(this.tb_workordername.Text.Trim());
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void ShowWoSnRange(string woid)
        {
            DataTable _mdt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWoInfo.Instance.GetWoSnRule(woid, null));
            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            foreach (DataRow dr in _mdt.Rows)
            {
                string sql = string.Format("insert into wosnrule(woId,sntype,snstart,snend,snprefix,snpostfix,ver,reve,snleng,usenum) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                    dr["woId"].ToString(),
                    dr["sntype"].ToString(),
                    dr["snstart"].ToString(),
                    dr["snend"].ToString(),
                    dr["snprefix"].ToString(),
                    dr["snpostfix"].ToString(),
                    dr["ver"].ToString(),
                    dr["reve"].ToString(),
                    dr["snleng"].ToString(),
                    dr["usenum"].ToString());
                if (!ass.ExecuteOracleCommand(sql))
                    throw new Exception("本地数据记录失败!!");
            }
            this.ip_labletypes.Refresh();
        }

      

        private void bt_inputwoinfo_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    // return;
            //    this.dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataSet(refWebSapConnector.Instance.Get_Z_RFC_ZMM011("7102412")).Tables[0];
            //    MessageBoxEx.Show("功能开发中..");
            //    this.dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataSet(refWebSapConnector.Instance.Get_Z_RFC_ZPP007("905000003", "2100", "20130101")).Tables[0];
            //    MessageBoxEx.Show("功能开发中..");
            //    //this.dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebSapConnector.Instance.Get_Z_RFC_MSEG("5000025698"));
            //    MessageBoxEx.Show("功能开发中..");
            //    this.dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebSapConnector.Instance.Get_Z_RFC_LIPS("0080000010", ""));
            //    MessageBoxEx.Show("功能开发中..");
            //    this.dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebSapConnector.Instance.Get_Z_RFC_AFPO("7100098"));
            //    MessageBoxEx.Show("功能开发中..");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            Frm_ErpWoInfo erpWo = new Frm_ErpWoInfo();
            if (erpWo.ShowDialog() == DialogResult.OK)
            {
                DataView dv = GetAllWoInfo().DefaultView;
                dv.Sort = string.Format("{0} DESC", "RECDATE");
                this.dgv_showwoinfo.DataSource = dv.ToTable(); ;
                MessageBox.Show("新建工单成功","新建工单提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

      

        private void bt_selectRoutGroup_Click(object sender, EventArgs e)
        {
            ShowRoutGroup srg = new ShowRoutGroup(this);
            srg.ShowDialog();
        }


        private void cb_bomnumber_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cb_bomnumber.Text))
            {
                if (!ChkBomNumber(this.cb_bomnumber.Text.Trim()))
                {
                    MessageBoxEx.Show("输入的BOM编号不存在，请重新输入;\n\n如果不需要BOM编号,请留空!!");
                    this.cb_bomnumber.SelectAll();
                    this.cb_bomnumber.Focus();
                }
            }
        }

        /// <summary>
        /// 检查BomNumber是否存在
        /// </summary>
        /// <param name="bomnumber"></param>
        /// <returns></returns>
        private bool ChkBomNumber(string bomnumber)
        {
            bool flag = false;
            foreach (string str in this.mBomNumberList)
            {
                if (this.cb_bomnumber.Text.Trim() == str)
                { flag = true; }
            }
            return flag;
        }



        private void cbwotye_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //switch (this.cbwotye.Text)
                //{
                //    case "正常工单":
                //        this.mWosntype = Entity.T_WO_INFO.SnType.正常工单;
                //        break;
                //    case "重工工单":
                //        this.mWosntype = Entity.T_WO_INFO.SnType.重工工单;
                //        break;
                //    case "RAM工单":
                //        this.mWosntype = Entity.T_WO_INFO.SnType.RAM工单;
                //        break;
                //    case "试产工单":
                //        this.mWosntype = Entity.T_WO_INFO.SnType.试产工单;
                //        break;
                //    case "SMT工单":
                //        this.mWosntype = Entity.T_WO_INFO.SnType.SMT工单;
                //        break;
                //    case "组包工单":
                //        this.mWosntype = Entity.T_WO_INFO.SnType.组包工单;
                //        break;
                //    case "外包工单":
                //        this.mWosntype = Entity.T_WO_INFO.SnType.外包工单;
                //        break;
                //}
                this._SfcWoType = this.cbwotye.Text;
                FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
                ass.ExecuteOracleCommand(string.Format("delete from wosnrule where woId='{0}'", this.tb_workordername.Text.Trim()));
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void bt_selectbomnumber_Click(object sender, EventArgs e)
        {

        }

     

        private void tb_selectwoname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.bt_selectwo_Click(null, null);
            }
        }


        private void cbCratePwd_SelectedValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_workordername.Text))
            {
                this.cbCratePwd.SelectedIndex = 0;
                this.mecpwd =ecpwd.PROG;
                return;
            }
            switch (this.cbCratePwd.SelectedIndex)
            {
                case 0:
                    this.mecpwd = ecpwd.PROG;
                    break;
                case 1:
                    this.mecpwd = ecpwd.FILE;
                    break;
                case 2:
                    this.mecpwd = ecpwd.USERDEF;
                    ////打开导入窗体
                    //FrmUsePwd fup = new FrmUsePwd(this.tb_workordername.Text.Trim(), this);
                    //if (fup.ShowDialog() != DialogResult.OK)
                    //{
                    //    this.cbCratePwd.SelectedIndex = 0;
                    //    this.mecpwd = ecpwd.PROG;
                    //}
                    //else
                    //{
                    //    this.mFrm.ShowPrgMsg("正在保存数据....", MainParent.MsgType.Incoming);
                    //    //string err = RefWebService_BLL.refWebtWoInfo.Instance.InserttUsePwd(lstUserPwd.ToArray());
                    //    //if (!string.IsNullOrEmpty(err))
                    //    //{
                    //    //    this.mFrm.ShowPrgMsg(err, MainParent.MsgType.Error);
                    //    //    return;
                    //    //}
                    //    this.mFrm.ShowPrgMsg("数据保存完成", MainParent.MsgType.Incoming);
                    //}
                    break;
                default:
                    break;
            }
            this.tb_pver.Focus();
        }

        private void tb_workordername_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_workordername.Text))
            {
                ClearTextBox();
            }
        }

        private void ClearTextBox()
        {
            this.cbCratePwd.SelectedIndex = 0;
            this.mecpwd = ecpwd.PROG;
            this.tb_routgroupid.Text = "";
            this.tb_partnumber.Text = "";
            this.tb_poname.Text = "";
            this.tb_woqty.Text = "";
            this.tb_inputstation.Text = "";
            this.tb_outputstation.Text = "";
            this.tb_pver.Text = "";
            this.tb_bomver.Text = "";
            this.cb_bomnumber.Text = "";
            this.tb_sw.Text = string.Empty;
            this.tb_Fw.Text = string.Empty;
            this.tb_nal.Text = string.Empty;
            this.tb_product.Text = string.Empty;
            this.ip_labletypes.Items.Clear();
            this.ip_labletypes.Refresh();
        }

        private void tb_workordername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_workordername.Text))
                return;
         
            //string err = refwebtEditing.Instance.ChktEditing(new WebServices.tEditing.tEditing1()
            //{
            //    prj = this.strFunName,
            //    funname = this.tb_workordername.Text.Trim(),
            //    userId = this.mFrm.gUserInfo.userId,
            //    username = this.mFrm.gUserInfo.username,
            //    PC_NAME = FrmBLL.publicfuntion.HostName + " IP:" + ((LsIp.Count > 1) ? LsIp[1] : LsIp[0]),
            //    MAC_ADD = FrmBLL.publicfuntion.getMacList()[0]
            //});
            string err = FrmBLL.publicfuntion.ChktEditing(this.tb_workordername.Text.Trim(), this.strFunName, this.mFrm.gUserInfo.userId, this.mFrm.gUserInfo.username);

            if (err != "OK")
            {
                if (err.IndexOf("ERROR") != -1)
                {
                    this.mFrm.ShowPrgMsg(err, MainParent.MsgType.Error);
                    return;
                }
                else
                {
                    MessageBoxEx.Show(string.Format("用户:[{0}]/[{1}]--{2}正在编辑中,您现在不能进行修改!!",
            err.Split(':')[1].Split(',')[0], err.Split(':')[1].Split(',')[1], err.Split(':')[1].Split(',')[2]), "提示!!");
                    return;
                }
            }
            #region 关闭从RFC接口获取工单信息
            //FrmMsg fmsg = new FrmMsg(this, this.tb_workordername.Text.Trim(), "正在获取SAP工单信息,请稍后..");
            //if (fmsg.ShowDialog() == DialogResult.OK && this.gWoInfodt != null && this.gWoInfodt.Rows.Count > 0)
            //{
            //    if (this.gWoInfodt.Rows[0]["STTXT"].ToString().Trim().ToUpper().IndexOf("REL") < 0)
            //    {
            //        MessageBox.Show("工单没有Release,请确认.... \r\n " + this.gWoInfodt.Rows[0]["STTXT"].ToString().Trim().ToUpper());
            //        tb_partnumber.ReadOnly = false;
            //        tb_woqty.ReadOnly = false;
            //        return;
            //    }
            //    this.tb_partnumber.Text = this.gWoInfodt.Rows[0]["MATNR"].ToString().Trim();
            //    this.tb_woqty.Text = this.GetIntByString(this.gWoInfodt.Rows[0]["PSMNG"].ToString().Trim()).ToString();
            //    this.tb_poname.Text = this.gWoInfodt.Rows[0]["PLNUM"].ToString().Trim();
            //    this.cbwotye.SelectedItem = this.GetWOtype(this.gWoInfodt.Rows[0]["DAUAT"].ToString().Trim());
            //    _SapWoType = this.gWoInfodt.Rows[0]["DAUAT"].ToString().Trim();

            //    tb_partnumber_Leave(null, null);

            //    tb_partnumber.ReadOnly = true;
            //    tb_woqty.ReadOnly = true;
                
            //}
            //else
            //{
            //    MessageBoxEx.Show("SAP工单信息获取失败,请手动填写..", "提示");
            //    tb_partnumber.ReadOnly = false;
            //    tb_woqty.ReadOnly = false;
            //}
            #endregion
            GetFtp_ATE_SCRIPT();
        }

        private int GetIntByString(string str)
        {
            try
            {
                return int.Parse(str);
            }
            catch
            {
                return 0;
            }
        }

        private void tb_routgroupid_SelectedValueChanged(object sender, EventArgs e)
        {
            DataRow[] arDr = this.mRouteTable.Select(string.Format("routgroupId='{0}' and routedesc='IN'",
                this.tb_routgroupid.Text.Trim()));

            if (arDr.Length > 1)
            {
                MessageBoxEx.Show("当前选择的流程编号存在两个投入站", "错误");
                this.tb_inputstation.Text = "";
                this.tb_outputstation.Text = "";
                return;
            }
            if (arDr.Length < 1)
            {
                MessageBoxEx.Show("当前选择的流程编号不存在投入站", "错误");
                this.tb_inputstation.Text = "";
                this.tb_outputstation.Text = "";
                return;
            }
            this.tb_inputstation.Text = arDr[0]["craftname"].ToString();

            arDr = this.mRouteTable.Select(string.Format("routgroupId='{0}' and routedesc='OUT'",
                this.tb_routgroupid.Text.Trim()));
            if (arDr.Length > 1)
            {
                MessageBoxEx.Show("当前选择的流程编号存在两个结束站", "错误");
                return;
            }
            if (arDr.Length < 1)
            {
                MessageBoxEx.Show("当前选择的流程编号不存在结束站", "错误");
                return;
            }
            this.tb_outputstation.Text = arDr[0]["craftname"].ToString();
        }

        private bool tes(string str, string[] arrStr)
        {
            bool tmp = false;
            foreach (string ss in arrStr)
            {
                if (ss == str)
                {
                    tmp = true;
                    break;
                }
            }
            return tmp;
        }
        private void btLoadATEScript_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Title = "选择测试脚本文件";
            ofd.InitialDirectory = @"d:\";
            ofd.Filter = "(*.tes;*.ts;*.lab)|*.tes;*.ts;*.lab|(*.tes)|*.tes|(*.ts)|*.ts|(*.lab)|*.lab|(*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (this.listbScript.Items.Count < 1)
                {
                    this.listbScript.Items.AddRange(ofd.SafeFileNames);
                }
                else
                {
                    List<string> ls = new List<string>();
                    for (int i = 0; i < this.listbScript.Items.Count; i++)
                    {
                        if (!tes(this.listbScript.Items[i].ToString(), ofd.SafeFileNames))
                        {
                            ls.Add(listbScript.Items[i].ToString());
                        }
                    }
                    this.listbScript.Items.Clear();
                    ls.AddRange(ofd.SafeFileNames);
                    this.listbScript.Items.AddRange(ls.ToArray());
                }
            }
        }

        private void listbScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)
            {
                for (int i = 0; i < this.listbScript.SelectedItems.Count; i++)
                {
                    this.listbScript.Items.Remove(this.listbScript.SelectedItems[i]);
                }
                this.listbScript.Refresh();
            }
        }

        private void WorkOrderCreate_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                refwebtEditing.Instance.DeletetEditingByUserIdAndPrj(this.mFrm.gUserInfo.userId, this.strFunName);
            }
            catch
            {
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            FrmGetDetailedSnList fgs = new FrmGetDetailedSnList();
            fgs.ShowDialog();
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

        private void Chk_Plan_CheckedChanged(object sender, EventArgs e)
        {
            cmb_PlanOrder.Enabled = Chk_Plan.Checked;
            Lb_PlanInfo.Enabled = Chk_Plan.Checked;
        }

        /// <summary>
        /// 重新从Ftp获取ATE脚本
        /// </summary>
        private void GetFtp_ATE_SCRIPT()
        {
            if (listbScript.Items.Count > 0)
            {
                if (MessageBox.Show("是否需要从Ftp 重新导入 ATE 脚本? ", "导入脚本提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Get_ATE_Script();
                }
            }
            else
            {
                Get_ATE_Script();
            }
        }
        private void Get_ATE_Script()
        {

            FrmMsg fmg = new FrmMsg(this, tb_workordername.Text, null);
            fmg.ShowDialog();

            //listbScript.Items.Clear();
            // string IniFileName = System.AppDomain.CurrentDomain.BaseDirectory + "config.ini";
            // string FtpHost = FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "Host", IniFileName);
            // string User = FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "User", IniFileName);
            // string UsePwd = FrmBLL.ReadIniFile.IniReadValue("ATESCRIPT", "Password", IniFileName);
            // FrmBLL.Ftp_Socket fm = new FrmBLL.Ftp_Socket(FtpHost, tb_workordername.Text, User, UsePwd, 21);
            //List<string> ss = fm.FileList("*.ts");          
            //foreach (string item in ss)
            //{
            //    listbScript.Items.Add(item);
            //}
            //List<string> Lab = fm.FileList("*.Lab");
            //foreach (string item in Lab)
            //{
            //    listbScript.Items.Add(item);
            //}
        }

        private void imbt_selectno_Click(object sender, EventArgs e)
        {
            Frm_TimeOutSelect tos = new Frm_TimeOutSelect(this);
            tos.ShowDialog();
        }
        public string CHK_PACKING_ROUTE()
        {
             if (mRouteTable.Rows.Count > 0 && !string.IsNullOrEmpty( mChkRoute) && !string.IsNullOrEmpty(mRollBackRoute) && tb_chkno.Text!="NA" )
                {
                    DataTable dtRoute = FrmBLL.publicfuntion.getNewTable(AllRouteInfo, string.Format("ROUTGROUPID='{0}'", mRouteTable.Rows[0][0].ToString()));
                    if (dtRoute.AsEnumerable().Where(h => h.Field<string>("CRAFTNAME") == mChkRoute).Count() <= 0)
                    {
                        return string.Format("[{0}]在途程编号[{1}]中不存在", mChkRoute, mRouteTable.Rows[0][0].ToString());
                    }
                    if (dtRoute.AsEnumerable().Where(h => h.Field<string>("CRAFTNAME") == mRollBackRoute).Count() <= 0)
                    {
                        return string.Format("[{0}]在途程编号[{1}]中不存在", mRollBackRoute, mRouteTable.Rows[0][0].ToString());
                    }
                }
        
             return "OK";
        }
              

        private void radstatus0_CheckedChanged(object sender, EventArgs e)
        {
            ClearTextBox();
            write_only();
            woid_status = 0;
            this.dgv_showwoinfo.DataSource = GetAllWoInfo();
            bt_savewoinfo.Enabled = true;
            imbt_OnLine.Enabled = true;
            imbt_UnOnLine.Enabled = false;
        }

        private void radstatus1_CheckedChanged(object sender, EventArgs e)
        {
            read_only();
            woid_status = 1;
            bt_savewoinfo.Enabled = true;
            imbt_OnLine.Enabled = false;
            imbt_UnOnLine.Enabled = true;
            this.tb_workordername.Text = string.Empty;
            ClearTextBox();
            this.dgv_showwoinfo.DataSource = GetAllWoInfo();
           
        }

        private void radstatus2_CheckedChanged(object sender, EventArgs e)
        {
            read_only();
            woid_status = 2;
            bt_savewoinfo.Enabled = true;
            imbt_OnLine.Enabled = false;
            imbt_UnOnLine.Enabled = false;
            this.tb_workordername.Text = string.Empty;
            ClearTextBox();
            this.dgv_showwoinfo.DataSource = GetAllWoInfo();
        }
        private void read_only()
        {
            tb_sw.ReadOnly = true;
            tb_poname.ReadOnly = true;
            tb_bomver.ReadOnly = true;
            cb_bomnumber.Enabled = false;
            tb_Fw.ReadOnly = true;
            cbwotye.Enabled = false;
            tb_pver.ReadOnly = true;
            cbCratePwd.Enabled = false;
            tb_nal.ReadOnly = true;
        }
        private void write_only()
        {
            tb_sw.ReadOnly = false;
            tb_poname.ReadOnly = false;
            tb_bomver.ReadOnly = false;
            cb_bomnumber.Enabled = true;
            tb_Fw.ReadOnly = false;
            cbwotye.Enabled = true;
            tb_pver.ReadOnly = false;
            cbCratePwd.Enabled = true;
            tb_nal.ReadOnly = false;
        }
        private void radstatus3_CheckedChanged(object sender, EventArgs e)
        {
            bt_savewoinfo.Enabled = false;
            imbt_OnLine.Enabled = false;
            imbt_UnOnLine.Enabled = false;
            this.tb_workordername.Text = string.Empty;
            ClearTextBox();
            this.dgv_showwoinfo.DataSource = GetAllWoInfo();
        }

        private void radstatus4_CheckedChanged(object sender, EventArgs e)
        {
            bt_savewoinfo.Enabled = false;
            imbt_OnLine.Enabled = false;
            imbt_UnOnLine.Enabled = false;
            this.tb_workordername.Text = string.Empty;
            ClearTextBox();
            this.dgv_showwoinfo.DataSource = GetAllWoInfo();
        }

        private void TextBoxReadOnly(bool Flag)
        {
            tb_workordername.ReadOnly = Flag;
            tb_woqty.ReadOnly = Flag;
            tb_partnumber.ReadOnly = Flag;
        }

        private void imbt_OnLine_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_workordername.Text))
            {
                if (MessageBox.Show(string.Format("确定要将工单[{0}]变更为待投产状态?", tb_workordername.Text), "状态变更提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Dictionary<string, object> mst = new Dictionary<string, object>();
                    mst.Add("WOID", tb_workordername.Text.Trim());
                    mst.Add("WOSTATE", 1);
                    string _StrErr = refWebtWoInfo.Instance.InsertWoInfo(FrmBLL.ReleaseData.DictionaryToJson(mst), null, null, null);
                    tb_workordername.Text = string.Empty;
                    ClearTextBox();
                    this.dgv_showwoinfo.DataSource = GetAllWoInfo();
                }
            }
        }

        private void imbt_UnOnLine_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_workordername.Text))
            {
                if (Convert.ToInt32(InputQTY) > 0)
                {
                    MessageBox.Show("该工单已经开始生产,不能变更为待创建状态");
                }
                else
                {
                    if (MessageBox.Show(string.Format("确定要将工单[{0}]变更为待创建状态?", tb_workordername.Text), "状态变更提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Dictionary<string, object> mst = new Dictionary<string, object>();
                        mst.Add("WOID", tb_workordername.Text.Trim());
                        mst.Add("WOSTATE", 0);
                        string _StrErr = refWebtWoInfo.Instance.InsertWoInfo(FrmBLL.ReleaseData.DictionaryToJson(mst), null, null, null);
                        tb_workordername.Text = string.Empty;
                        ClearTextBox();
                        this.dgv_showwoinfo.DataSource = GetAllWoInfo();
                    }
                }
            }
        }
       
    }
}
