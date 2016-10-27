using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using FrmBLL;
using WebServices;
using RefWebService_BLL;
using WebServices.tB_SnRule_PartNumber;
using System.Text.RegularExpressions;

namespace SFIS_V2
{
    public partial class Frm_Cust_SnRule : Office2007Form//Form
    {
        public Frm_Cust_SnRule(MainParent Msg)
        {
            InitializeComponent();
            sInfo = Msg;
        }
        MainParent sInfo;

        DataTable dt_snRuleInfo = new DataTable();//规则数据表
        DataTable dt_snRulewo = null;
        private void Frm_Cust_SnRule_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.sInfo.gUserInfo.rolecaption == "系统开发员")
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

            #region 以料号新增规则 加载信息
            LoadInfo();
            LoadInfoSnRuleWO();
            Btn_Update_Partn.Enabled = false;
            #endregion

            Btn_PartN_Clear_Click(sender,null);
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadInfo()
        {
            dt_snRuleInfo = ReleaseData.arrByteToDataSet(refWebtB_SnRule_PartNumber.Instance.GetALLB_SnRule_PartNumber()).Tables[0];
            Dg_Cust_PnInfo.DataSource = dt_snRuleInfo;

            Txt_CUST_LAST_BOX.Enabled = false;
            Txt_CUST_LAST_CARTON.Enabled = false;
            Txt_CUST_LAST_PALLET.Enabled = false;
            Txt_CUST_LAST_SN.Enabled = false;          

        }

        public void LoadInfoSnRuleWO()
        {
            dt_snRulewo = ReleaseData.arrByteToDataTable(refWebtB_SnRule_WO.Instance.GetALLB_SnRule_WO());
            dgv_snrulewo.DataSource = dt_snRulewo;
        }

        /// <summary>
        /// 判断返回值是否是数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsNumric(string input) //如果是数字或小数点 返回true
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            bool flag = true;
            string pattern = (@"^\d+$");
            //string pattern = (@"^([+-]?)\d*[.]?\d*$");
            Regex validate = new Regex(pattern);
            if (!validate.IsMatch(input))
            {
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 检测填入的数据
        /// </summary>
        public string Check_Info()
        {
            if (string.IsNullOrEmpty(Txt_PARTNUMBER.Text.Trim()))
            {
                return "料号为必填参数不能为空！";
            }
            if ((!string.IsNullOrEmpty(Txt_CUST_BOX_LENG.Text.Trim() == "NA" ? "" : Txt_CUST_BOX_LENG.Text.Trim()) && !IsNumric(Txt_CUST_BOX_LENG.Text.Trim() == "NA" ? "0" : Txt_CUST_BOX_LENG.Text.Trim()))
                || (!string.IsNullOrEmpty(Txt_CUST_PALLET_LENG.Text.Trim() == "NA" ? "" : Txt_CUST_PALLET_LENG.Text.Trim()) && !IsNumric(Txt_CUST_PALLET_LENG.Text.Trim() == "NA" ? "0" : Txt_CUST_PALLET_LENG.Text.Trim()))
                || (!string.IsNullOrEmpty(Txt_CUST_CARTON_LENG.Text.Trim() == "NA" ? "" : Txt_CUST_CARTON_LENG.Text.Trim()) && !IsNumric(Txt_CUST_CARTON_LENG.Text.Trim() == "NA" ? "0" : Txt_CUST_CARTON_LENG.Text.Trim()))
                || (!string.IsNullOrEmpty(Txt_CUST_SN_LENG.Text.Trim() == "NA" ? "" : Txt_CUST_SN_LENG.Text.Trim()) && !IsNumric(Txt_CUST_SN_LENG.Text.Trim() == "NA" ? "0" : Txt_CUST_SN_LENG.Text.Trim()))
                )
            {
                return "长度只能为数字！";
            }

            //判断是否为空
            if ((!string.IsNullOrEmpty(Txt_CUST_PALLET_STR.Text.Trim() == "NA" ? "" : Txt_CUST_PALLET_STR.Text.Trim())))
            {
                //判断是否为数字
                if ((!IsNumric(Txt_CUST_PALLET_STR.Text.Trim() == "NA" ? "0" : Txt_CUST_PALLET_STR.Text.Trim())))
                {
                    return "PALLET开始值只能为数字";
                }
                else
                {

                    if ((int.Parse((Txt_CUST_PALLET_STR.Text.Trim() == "NA" ? "0" : Txt_CUST_PALLET_STR.Text.Trim()))<= 0))
                    {
                        return "PALLET开始值必须大于零！";

                    }
                }
            }
            if ((!string.IsNullOrEmpty(Txt_CUST_SN_STR.Text.Trim() == "NA" ? "" : Txt_CUST_SN_STR.Text.Trim())))
            {
                if ((!IsNumric(Txt_CUST_SN_STR.Text.Trim() == "NA" ? "0" : Txt_CUST_SN_STR.Text.Trim())))
                {
                    return "SN开始值只能为数字";
                }
                else
                {
                    if ((int.Parse((Txt_CUST_SN_STR.Text.Trim() == "NA" ? "0" : Txt_CUST_SN_STR.Text.Trim())) <= 0))
                    {
                        return "SN开始值必须大于零！";
                    }

                }
            }

            if ((!string.IsNullOrEmpty(Txt_CUST_CARTON_STR.Text.Trim() == "NA" ? "" : Txt_CUST_CARTON_STR.Text.Trim())))
            {
                if ((!IsNumric(Txt_CUST_CARTON_STR.Text.Trim() == "NA" ? "0" : Txt_CUST_CARTON_STR.Text.Trim())))
                {
                    return "CARTON开始值只能为数字";
                }
                else
                {
                    if ((int.Parse((Txt_CUST_CARTON_STR.Text.Trim() == "NA" ? "0" : Txt_CUST_CARTON_STR.Text.Trim()))<= 0))
                    {
                        return "CARTON开始值必须大于零！";
                    }
                }
            }


            if ((!string.IsNullOrEmpty(Txt_CUST_BOX_STR.Text.Trim() == "NA" ? "" : Txt_CUST_BOX_STR.Text.Trim())))
            {
                if ((!IsNumric(Txt_CUST_BOX_STR.Text.Trim() == "NA" ? "0" : Txt_CUST_BOX_STR.Text.Trim())))
                {
                    return "BOX开始值只能为数字";
                }
                else
                {
                    if ((int.Parse((Txt_CUST_BOX_STR.Text.Trim() == "NA" ? "0" : Txt_CUST_BOX_STR.Text.Trim())) <= 0))
                    {
                        return "BOX开始值必须大于零！";
                    }
                }

            }


            return null;
        }

        private void Btn_Inser_PartN_Click(object sender, EventArgs e)
        {


            string ERROR = Check_Info();
            if (!string.IsNullOrEmpty(ERROR))
            {
                MessageBox.Show(ERROR, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable New_dt_snRuleInfo = publicfuntion.getNewTable(dt_snRuleInfo, string.Format("PARTNUMBER='{0}'", Txt_PARTNUMBER.Text));
            if (New_dt_snRuleInfo.Rows.Count > 0)
            {
                MessageBox.Show("这个料号已经存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }          

            Dictionary<string, object> Dic = new Dictionary<string, object>();
            FrmBLL.publicfuntion.SerializeControl(Dic, tabPage1);
            Dic.Add("UPCEANDATA", "NA");
            Dic.Add("EMP_NO", sInfo.gUserInfo.userId);
            string Status = refWebtB_SnRule_PartNumber.Instance.InsertB_SNRULE_PARTNUMBER(FrmBLL.ReleaseData.DictionaryToJson(Dic));

            //string Status = refWebtB_SnRule_PartNumber.Instance.InsertB_SNRULE_PARTNUMBER(
            //       new WebServices.tB_SnRule_PartNumber.B_SNRULE_PARTNUMBER_Table()
            //       {
            //           BOX_LAB_NAME = string.IsNullOrEmpty(Txt_BOX_LAB_NAME.Text.Trim()) ? "NA" : Txt_BOX_LAB_NAME.Text.Trim(),
            //           CARTON_LAB_NAME = string.IsNullOrEmpty(Txt_CARTON_LAB_NAME.Text.Trim()) ? "NA" : Txt_CARTON_LAB_NAME.Text.Trim(),
            //           CUST_BOX_LENG = int.Parse(string.IsNullOrEmpty(Txt_CUST_BOX_LENG.Text.Trim()) ? "0" : Txt_CUST_BOX_LENG.Text.Trim()),

            //           CUST_BOX_PREFIX = string.IsNullOrEmpty(Txt_CUST_BOX_PREFIX.Text.Trim()) ? "NA" : Txt_CUST_BOX_PREFIX.Text.Trim(),
            //           CUST_BOX_STR = string.IsNullOrEmpty(Txt_CUST_BOX_STR.Text.Trim()) ? "001" : Txt_CUST_BOX_STR.Text.Trim(),
            //           CUST_CARTON_LENG = int.Parse(string.IsNullOrEmpty(Txt_CUST_CARTON_LENG.Text.Trim()) ? "0" : Txt_CUST_CARTON_LENG.Text.Trim()),

            //           CUST_CARTON_POSTFIX = string.IsNullOrEmpty(Txt_CUST_CARTON_POSTFIX.Text.Trim()) ? "NA" : Txt_CUST_CARTON_POSTFIX.Text.Trim(),
            //           CUST_CARTON_PREFIX = string.IsNullOrEmpty(Txt_CUST_CARTON_PREFIX.Text.Trim()) ? "NA" : Txt_CUST_CARTON_PREFIX.Text.Trim(),
            //           CUST_CARTON_STR = string.IsNullOrEmpty(Txt_CUST_CARTON_STR.Text.Trim()) ? "001" : Txt_CUST_CARTON_STR.Text.Trim(),

            //           CUST_LAST_BOX = string.IsNullOrEmpty(Txt_CUST_LAST_BOX.Text.Trim()) ? "001" : Txt_CUST_LAST_BOX.Text.Trim(),
            //           CUST_LAST_CARTON = string.IsNullOrEmpty(Txt_CUST_LAST_CARTON.Text.Trim()) ? "001" : Txt_CUST_LAST_CARTON.Text.Trim(),
            //           CUST_LAST_PALLET = string.IsNullOrEmpty(Txt_CUST_LAST_PALLET.Text.Trim()) ? "001" : Txt_CUST_LAST_PALLET.Text.Trim(),
            //           CUST_LAST_SN = string.IsNullOrEmpty(Txt_CUST_LAST_SN.Text.Trim()) ? "001" : Txt_CUST_LAST_SN.Text.Trim(),
            //           CUST_PALLET_LENG = int.Parse(string.IsNullOrEmpty(Txt_CUST_PALLET_LENG.Text.Trim()) ? "0" : Txt_CUST_PALLET_LENG.Text.Trim()),
            //           CUST_PALLET_POSTFIX = string.IsNullOrEmpty(Txt_CUST_PALLET_POSTFIX.Text.Trim()) ? "NA" : Txt_CUST_PALLET_POSTFIX.Text.Trim(),

            //           CUST_PALLET_PREFIX = string.IsNullOrEmpty(Txt_CUST_PALLET_PREFIX.Text.Trim()) ? "NA" : Txt_CUST_PALLET_PREFIX.Text.Trim(),
            //           CUST_PALLET_STR = string.IsNullOrEmpty(Txt_CUST_PALLET_STR.Text.Trim()) ? "001" : Txt_CUST_PALLET_STR.Text.Trim(),
            //           CUST_PARTNUBMER = string.IsNullOrEmpty(Txt_CUST_PARTNUBMER.Text.Trim()) ? "NA" : Txt_CUST_PARTNUBMER.Text.Trim(),
            //           CUST_PARTNUMBER_DESC = string.IsNullOrEmpty(Txt_CUST_PARTNUMBER_DESC.Text.Trim()) ? "NA" : Txt_CUST_PARTNUMBER_DESC.Text.Trim(),

            //           CUST_SN_LENG = int.Parse(string.IsNullOrEmpty(Txt_CUST_SN_LENG.Text.Trim()) ? "0" : Txt_CUST_SN_LENG.Text.Trim()),
            //           CUST_SN_POSTFIX = string.IsNullOrEmpty(Txt_CUST_SN_POSTFIX.Text.Trim()) ? "NA" : Txt_CUST_SN_POSTFIX.Text.Trim(),
            //           CUST_SN_PREFIX = string.IsNullOrEmpty(Txt_CUST_SN_PREFIX.Text.Trim()) ? "NA" : Txt_CUST_SN_PREFIX.Text.Trim(),

            //           CUST_SN_STR = string.IsNullOrEmpty(Txt_CUST_SN_STR.Text.Trim()) ? "001" : Txt_CUST_SN_STR.Text.Trim(),

            //           CUST_VENDER_CODE = string.IsNullOrEmpty(Txt_CUST_VENDER_CODE.Text.Trim()) ? "NA" : Txt_CUST_VENDER_CODE.Text.Trim(),
            //           CUST_VERSION_CODE = string.IsNullOrEmpty(Txt_CUST_VERSION_CODE.Text.Trim()) ? "NA" : Txt_CUST_VERSION_CODE.Text.Trim(),
            //           UPCEANDATA = "NA",
            //           VERSION_CODE = string.IsNullOrEmpty(Txt_VERSION_CODE.Text.Trim()) ? "NA" : Txt_VERSION_CODE.Text.Trim(),
            //           EMP_NO = sInfo.gUserInfo.userId,
            //           PALLET_LAB_NAME = string.IsNullOrEmpty(Txt_PALLET_LAB_NAME.Text.Trim()) ? "NA" : Txt_PALLET_LAB_NAME.Text.Trim(),
            //           PARTNUMBER = string.IsNullOrEmpty(Txt_PARTNUMBER.Text.Trim()) ? "NA" : Txt_PARTNUMBER.Text.Trim(),
            //           RULE_TYPE = string.IsNullOrEmpty(Txt_RULE_TYPE.Text.Trim()) ? "NA" : Txt_RULE_TYPE.Text.Trim()
            //       });
            if (Status == "OK")
            {
                LoadInfo();
                Btn_PartN_Clear_Click(null, null);
            }
            else
            {
                MessageBox.Show(Status, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void Dg_Cust_PnInfo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            Btn_Update_Partn.Enabled = true;
            Txt_PARTNUMBER.ReadOnly = true;
            Txt_RULE_TYPE.ReadOnly = true;
            Btn_Inser_PartN.Enabled = false;
            Txt_BOX_LAB_NAME.Text = Dg_Cust_PnInfo["BOX_LAB_NAME", e.RowIndex].Value.ToString();
            Txt_CARTON_LAB_NAME.Text = Dg_Cust_PnInfo["CARTON_LAB_NAME", e.RowIndex].Value.ToString();
            Txt_CUST_BOX_LENG.Text = Dg_Cust_PnInfo["CUST_BOX_LENG", e.RowIndex].Value.ToString();
            Txt_CUST_BOX_LENG.Enabled = false;
            Txt_CUST_BOX_PREFIX.Text = Dg_Cust_PnInfo["CUST_BOX_PREFIX", e.RowIndex].Value.ToString();
            Txt_CUST_BOX_PREFIX.Enabled = false;
            Txt_CUST_BOX_STR.Text = Dg_Cust_PnInfo["CUST_BOX_STR", e.RowIndex].Value.ToString();
            Txt_CUST_BOX_STR.Enabled = false;
            Txt_CUST_CARTON_LENG.Text = Dg_Cust_PnInfo["CUST_CARTON_LENG", e.RowIndex].Value.ToString();
            Txt_CUST_CARTON_POSTFIX.Text = Dg_Cust_PnInfo["CUST_CARTON_POSTFIX", e.RowIndex].Value.ToString();
            Txt_CUST_CARTON_PREFIX.Text = Dg_Cust_PnInfo["CUST_CARTON_PREFIX", e.RowIndex].Value.ToString();
            Txt_CUST_CARTON_STR.Text = Dg_Cust_PnInfo["CUST_CARTON_STR", e.RowIndex].Value.ToString();
            Txt_CUST_PALLET_LENG.Text = Dg_Cust_PnInfo["CUST_PALLET_LENG", e.RowIndex].Value.ToString();
            Txt_CUST_PALLET_POSTFIX.Text = Dg_Cust_PnInfo["CUST_PALLET_POSTFIX", e.RowIndex].Value.ToString();
            Txt_CUST_PALLET_PREFIX.Text = Dg_Cust_PnInfo["CUST_PALLET_PREFIX", e.RowIndex].Value.ToString();
            Txt_CUST_PALLET_STR.Text = Dg_Cust_PnInfo["CUST_PALLET_STR", e.RowIndex].Value.ToString();
            Txt_CUST_PARTNUBMER.Text = Dg_Cust_PnInfo["CUST_PARTNUBMER", e.RowIndex].Value.ToString();
            Txt_CUST_PARTNUMBER_DESC.Text = Dg_Cust_PnInfo["CUST_PARTNUMBER_DESC", e.RowIndex].Value.ToString();
            Txt_CUST_SN_LENG.Text = Dg_Cust_PnInfo["CUST_SN_LENG", e.RowIndex].Value.ToString();
            Txt_CUST_SN_POSTFIX.Text = Dg_Cust_PnInfo["CUST_SN_POSTFIX", e.RowIndex].Value.ToString();
            Txt_CUST_SN_PREFIX.Text = Dg_Cust_PnInfo["CUST_SN_PREFIX", e.RowIndex].Value.ToString();
            Txt_CUST_SN_STR.Text = Dg_Cust_PnInfo["CUST_SN_STR", e.RowIndex].Value.ToString();
            Txt_CUST_VENDER_CODE.Text = Dg_Cust_PnInfo["CUST_VENDER_CODE", e.RowIndex].Value.ToString();
            Txt_CUST_VERSION_CODE.Text = Dg_Cust_PnInfo["CUST_VERSION_CODE", e.RowIndex].Value.ToString();
            Txt_VERSION_CODE.Text = Dg_Cust_PnInfo["VERSION_CODE", e.RowIndex].Value.ToString();
            Txt_PALLET_LAB_NAME.Text = Dg_Cust_PnInfo["PALLET_LAB_NAME", e.RowIndex].Value.ToString();
            Txt_PARTNUMBER.Text = Dg_Cust_PnInfo["PARTNUMBER", e.RowIndex].Value.ToString();
            Txt_RULE_TYPE.Text = Dg_Cust_PnInfo["RULE_TYPE", e.RowIndex].Value.ToString();


            Txt_CUST_LAST_BOX.Text = Dg_Cust_PnInfo["CUST_LAST_BOX", e.RowIndex].Value.ToString();
            Txt_CUST_LAST_CARTON.Text = Dg_Cust_PnInfo["CUST_LAST_CARTON", e.RowIndex].Value.ToString();
            Txt_CUST_LAST_PALLET.Text = Dg_Cust_PnInfo["CUST_LAST_PALLET", e.RowIndex].Value.ToString();
            Txt_CUST_LAST_SN.Text = Dg_Cust_PnInfo["CUST_LAST_SN", e.RowIndex].Value.ToString();

            Txt_CUST_LAST_BOX.Enabled = true;
            Txt_CUST_LAST_CARTON.Enabled = true;
            Txt_CUST_LAST_PALLET.Enabled = true;
            Txt_CUST_LAST_SN.Enabled = true;


            Txt_CUST_BOX_LENG.Enabled = false;
            Txt_CUST_BOX_PREFIX.Enabled = false;
            Txt_CUST_BOX_STR.Enabled = false;

            Txt_CUST_CARTON_LENG.Enabled = false;
            Txt_CUST_CARTON_PREFIX.Enabled = false;
            Txt_CUST_CARTON_STR.Enabled = false;
            Txt_CUST_CARTON_POSTFIX.Enabled = false;

            Txt_CUST_PALLET_LENG.Enabled = false;
            Txt_CUST_PALLET_PREFIX.Enabled = false;
            Txt_CUST_PALLET_STR.Enabled = false;
            Txt_CUST_PALLET_POSTFIX.Enabled = false;

            Txt_CUST_SN_LENG.Enabled = false;
            Txt_CUST_SN_PREFIX.Enabled = false;
            Txt_CUST_SN_STR.Enabled = false;
            Txt_CUST_SN_POSTFIX.Enabled = false;
            Txt_CUST_VENDER_CODE.Enabled = false;


        }

        private void Btn_Update_Partn_Click(object sender, EventArgs e)
        {
            string ERROR = Check_Info();
            if (!string.IsNullOrEmpty(ERROR)
               )
            {
                MessageBox.Show(ERROR, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int flag = 0;
            if (Txt_RULE_TYPE.Text == "NA")
            {
                flag = 0;
            }
            else
            {
                flag = 1;
                DataTable New_dt_snRuleInfo = publicfuntion.getNewTable(dt_snRuleInfo, string.Format("RULE_TYPE='{0}'", Txt_RULE_TYPE.Text));
                string Change_Total = "将同时修改的料号为：\r\n";
                if (New_dt_snRuleInfo.Rows.Count > 1)
                {

                    foreach (DataRow item in New_dt_snRuleInfo.Rows)
                    {
                        Change_Total += item["PARTNUMBER"] + "\r\n";
                    }

                    if (MessageBox.Show(Change_Total, "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        MessageBox.Show("修改已取消", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }


            Dictionary<string, object> Dic = new Dictionary<string, object>();
            FrmBLL.publicfuntion.SerializeControl(Dic, tabPage1);
            Dic.Add("UPCEANDATA", "NA");
            Dic.Add("EMP_NO", sInfo.gUserInfo.userId);
            string Status = refWebtB_SnRule_PartNumber.Instance.UpdateB_SNRULE_PARTNUMBER(ReleaseData.DictionaryToJson(Dic), flag);
           /* string Status = refWebtB_SnRule_PartNumber.Instance.UpdateB_SNRULE_PARTNUMBER(new WebServices.tB_SnRule_PartNumber.B_SNRULE_PARTNUMBER_Table()
                  {
                      BOX_LAB_NAME = string.IsNullOrEmpty(Txt_BOX_LAB_NAME.Text.Trim()) ? "NA" : Txt_BOX_LAB_NAME.Text.Trim(),
                      CARTON_LAB_NAME = string.IsNullOrEmpty(Txt_CARTON_LAB_NAME.Text.Trim()) ? "NA" : Txt_CARTON_LAB_NAME.Text.Trim(),
                      CUST_BOX_LENG = int.Parse(string.IsNullOrEmpty(Txt_CUST_BOX_LENG.Text.Trim()) ? "0" : Txt_CUST_BOX_LENG.Text.Trim()),

                      CUST_BOX_PREFIX = string.IsNullOrEmpty(Txt_CUST_BOX_PREFIX.Text.Trim()) ? "NA" : Txt_CUST_BOX_PREFIX.Text.Trim(),
                      CUST_BOX_STR = string.IsNullOrEmpty(Txt_CUST_BOX_STR.Text.Trim()) ? "001" : Txt_CUST_BOX_STR.Text.Trim(),
                      CUST_CARTON_LENG = int.Parse(string.IsNullOrEmpty(Txt_CUST_CARTON_LENG.Text.Trim()) ? "0" : Txt_CUST_CARTON_LENG.Text.Trim()),

                      CUST_CARTON_POSTFIX = string.IsNullOrEmpty(Txt_CUST_CARTON_POSTFIX.Text.Trim()) ? "NA" : Txt_CUST_CARTON_POSTFIX.Text.Trim(),
                      CUST_CARTON_PREFIX = string.IsNullOrEmpty(Txt_CUST_CARTON_PREFIX.Text.Trim()) ? "NA" : Txt_CUST_CARTON_PREFIX.Text.Trim(),
                      CUST_CARTON_STR = string.IsNullOrEmpty(Txt_CUST_CARTON_STR.Text.Trim()) ? "001" : Txt_CUST_CARTON_STR.Text.Trim(),

                      CUST_LAST_BOX = string.IsNullOrEmpty(Txt_CUST_LAST_BOX.Text.Trim()) ? "001" : Txt_CUST_LAST_BOX.Text.Trim(),
                      CUST_LAST_CARTON = string.IsNullOrEmpty(Txt_CUST_LAST_CARTON.Text.Trim()) ? "001" : Txt_CUST_LAST_CARTON.Text.Trim(),
                      CUST_LAST_PALLET = string.IsNullOrEmpty(Txt_CUST_LAST_PALLET.Text.Trim()) ? "001" : Txt_CUST_LAST_PALLET.Text.Trim(),
                      CUST_LAST_SN = string.IsNullOrEmpty(Txt_CUST_LAST_SN.Text.Trim()) ? "001" : Txt_CUST_LAST_SN.Text.Trim(),

                      CUST_PALLET_LENG = int.Parse(string.IsNullOrEmpty(Txt_CUST_PALLET_LENG.Text.Trim()) ? "0" : Txt_CUST_PALLET_LENG.Text.Trim()),
                      CUST_PALLET_POSTFIX = string.IsNullOrEmpty(Txt_CUST_PALLET_POSTFIX.Text.Trim()) ? "NA" : Txt_CUST_PALLET_POSTFIX.Text.Trim(),

                      CUST_PALLET_PREFIX = string.IsNullOrEmpty(Txt_CUST_PALLET_PREFIX.Text.Trim()) ? "NA" : Txt_CUST_PALLET_PREFIX.Text.Trim(),
                      CUST_PALLET_STR = string.IsNullOrEmpty(Txt_CUST_PALLET_STR.Text.Trim()) ? "001" : Txt_CUST_PALLET_STR.Text.Trim(),
                      CUST_PARTNUBMER = string.IsNullOrEmpty(Txt_CUST_PARTNUBMER.Text.Trim()) ? "NA" : Txt_CUST_PARTNUBMER.Text.Trim(),
                      CUST_PARTNUMBER_DESC = string.IsNullOrEmpty(Txt_CUST_PARTNUMBER_DESC.Text.Trim()) ? "NA" : Txt_CUST_PARTNUMBER_DESC.Text.Trim(),

                      CUST_SN_LENG = int.Parse(string.IsNullOrEmpty(Txt_CUST_SN_LENG.Text.Trim()) ? "0" : Txt_CUST_SN_LENG.Text.Trim()),
                      CUST_SN_POSTFIX = string.IsNullOrEmpty(Txt_CUST_SN_POSTFIX.Text.Trim()) ? "NA" : Txt_CUST_SN_POSTFIX.Text.Trim(),
                      CUST_SN_PREFIX = string.IsNullOrEmpty(Txt_CUST_SN_PREFIX.Text.Trim()) ? "NA" : Txt_CUST_SN_PREFIX.Text.Trim(),

                      CUST_SN_STR = string.IsNullOrEmpty(Txt_CUST_SN_STR.Text.Trim()) ? "001" : Txt_CUST_SN_STR.Text.Trim(),

                      CUST_VENDER_CODE = string.IsNullOrEmpty(Txt_CUST_VENDER_CODE.Text.Trim()) ? "NA" : Txt_CUST_VENDER_CODE.Text.Trim(),
                      CUST_VERSION_CODE = string.IsNullOrEmpty(Txt_CUST_VERSION_CODE.Text.Trim()) ? "NA" : Txt_CUST_VERSION_CODE.Text.Trim(),
                      UPCEANDATA = "NA",
                      VERSION_CODE = string.IsNullOrEmpty(Txt_VERSION_CODE.Text.Trim()) ? "NA" : Txt_VERSION_CODE.Text.Trim(),
                      EMP_NO = sInfo.gUserInfo.userId,
                      PALLET_LAB_NAME = string.IsNullOrEmpty(Txt_PALLET_LAB_NAME.Text.Trim()) ? "NA" : Txt_PALLET_LAB_NAME.Text.Trim(),
                      PARTNUMBER = string.IsNullOrEmpty(Txt_PARTNUMBER.Text.Trim()) ? "NA" : Txt_PARTNUMBER.Text.Trim(),
                      RULE_TYPE = string.IsNullOrEmpty(Txt_RULE_TYPE.Text.Trim()) ? "NA" : Txt_RULE_TYPE.Text.Trim()
                  }, flag);
            */
            if (Status == "OK")
            {
                LoadInfo();
                Btn_PartN_Clear_Click(null, null);
            }
            else
            {
                MessageBox.Show(Status, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void Btn_PartN_Clear_Click(object sender, EventArgs e)
        {
            ClearCntrValue(groupBox1);
            ClearCntrValue(groupBox2);
            ClearCntrValue(groupBox3);
            ClearCntrValue(groupBox4);
            ClearCntrValue(groupBox5);
            ClearCntrValue(groupBox6);
            Btn_Update_Partn.Enabled = false;
            Txt_PARTNUMBER.ReadOnly = false;
            Txt_RULE_TYPE.ReadOnly = false;
            Btn_Inser_PartN.Enabled = true;

            Txt_CUST_LAST_BOX.Enabled = false;
            Txt_CUST_LAST_CARTON.Enabled = false;
            Txt_CUST_LAST_PALLET.Enabled = false;
            Txt_CUST_LAST_SN.Enabled = false;

            Txt_CUST_BOX_LENG.Enabled = true;
            Txt_CUST_BOX_PREFIX.Enabled = true;
            Txt_CUST_BOX_STR.Enabled = true;

            Txt_CUST_CARTON_LENG.Enabled = true;
            Txt_CUST_CARTON_PREFIX.Enabled = true;
            Txt_CUST_CARTON_STR.Enabled = true;
            Txt_CUST_CARTON_POSTFIX.Enabled = true;

            Txt_CUST_PALLET_LENG.Enabled = true;
            Txt_CUST_PALLET_PREFIX.Enabled = true;
            Txt_CUST_PALLET_STR.Enabled = true;
            Txt_CUST_PALLET_POSTFIX.Enabled = true;

            Txt_CUST_SN_LENG.Enabled = true;
            Txt_CUST_SN_PREFIX.Enabled = true;
            Txt_CUST_SN_STR.Enabled = true;
            Txt_CUST_SN_POSTFIX.Enabled = true;
            Txt_CUST_VENDER_CODE.Enabled = true;
            
            Txt_CUST_BOX_LENG.Text = "0";                        
            Txt_CUST_BOX_STR.Text ="001" ;
            Txt_CUST_CARTON_LENG.Text =  "0";           
            Txt_CUST_CARTON_STR.Text ="001";
            Txt_CUST_LAST_BOX.Text = "001";
            Txt_CUST_LAST_CARTON.Text =  "001" ;
            Txt_CUST_LAST_PALLET.Text =  "001" ;
            Txt_CUST_LAST_SN.Text =  "001" ;
            Txt_CUST_PALLET_LENG.Text = "0";                      
            Txt_CUST_PALLET_STR.Text = "001";                  
            Txt_CUST_SN_LENG.Text = "0";               
            Txt_CUST_SN_STR.Text =  "001" ;
                   

        }



        ///<summary>
        ///清除容器里面某些控件的值
        ///</summary>
        ///<param name="parContainer">容器类控件</param> 
        public void ClearCntrValue(Control parContainer)
        {
            for (int index = 0; index < parContainer.Controls.Count; index++)
            {
                // 如果是容器类控件，递归调用自己                

                if (parContainer.Controls[index].HasChildren)
                {
                    ClearCntrValue(parContainer.Controls[index]);
                }
                else
                {

                    switch (parContainer.Controls[index].GetType().Name)
                    {
                        case "TextBoxX":
                            parContainer.Controls[index].Text = "NA";
                            break;
                        case "RadioButton":
                            ((RadioButton)(parContainer.Controls[index])).Checked = false;
                            break;
                        case "CheckBox":
                            ((CheckBox)(parContainer.Controls[index])).Checked = false;
                            break;
                        case "ComboBox":
                            ((ComboBox)(parContainer.Controls[index])).Text = "";
                            break;

                    }
                }
            }
        }

        private void Txt_CUST_SN_STR_MouseLeave(object sender, EventArgs e)
        {
            Txt_CUST_LAST_SN.Text = Txt_CUST_SN_STR.Text;
        }

        private void Txt_CUST_CARTON_STR_MouseLeave(object sender, EventArgs e)
        {
            Txt_CUST_LAST_CARTON.Text = Txt_CUST_CARTON_STR.Text;
        }

        private void Txt_CUST_PALLET_STR_MouseLeave(object sender, EventArgs e)
        {
            Txt_CUST_LAST_PALLET.Text = Txt_CUST_PALLET_STR.Text;
        }

        private void Txt_CUST_BOX_STR_MouseLeave(object sender, EventArgs e)
        {
            Txt_CUST_LAST_BOX.Text = Txt_CUST_BOX_STR.Text;
        }

        private void Txt_CUST_SN_POSTFIX_TextChanged(object sender, EventArgs e)
        {

        }

        private void imbt_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbt_WOID.Text))
                {
                    throw new Exception("工单不能为空");
                }

                foreach (DataGridViewRow dgvr in dgv_snrulewo.Rows)
                {
                    if (tbt_WOID.Text.Trim() == dgvr.Cells["WOID"].Value.ToString())
                    {
                        throw new Exception("工单重复,请修改此工单即可...");
                    }
                }
                Dictionary<string, object> Dic = new Dictionary<string, object>();
                FrmBLL.publicfuntion.SerializeControl(Dic, tabPage2);               
                Dic.Add("EMP_NO", sInfo.gUserInfo.userId);
                string sRes = refWebtB_SnRule_WO.Instance.InsertB_SNRULE_WO(FrmBLL.ReleaseData.DictionaryToJson(Dic));
                //string sRes = refWebtB_SnRule_WO.Instance.InsertB_SNRULE_WO(new WebServices.tB_SnRule_WO.B_SNRULE_WO_Table()
                //      {
                //          WOID = tb_wo.Text.Trim(),
                //          CUST_SN_PREFIX = tb_CUST_SN_PREFIX.Text,
                //          CUST_SN_POSTFIX = tb_CUST_SN_POSTFIX.Text,
                //          CUST_SN_LENG = Convert.ToInt32(tb_CUST_SN_LENG.Text),
                //          CUST_SN_STR = tb_CUST_SN_STR.Text,
                //          CUST_LAST_SN = tb_CUST_LAST_SN.Text,
                //          CUST_BOX_LENG = Convert.ToInt32(tb_CUST_BOX_LENG.Text),
                //          CUST_BOX_PREFIX = tb_CUST_BOX_PREFIX.Text,
                //          CUST_BOX_STR = tb_CUST_BOX_STR.Text,
                //          BOX_LAB_NAME = tb_BOX_LAB_NAME.Text,
                //          CUST_LAST_BOX = tb_CUST_LAST_BOX.Text,
                //          CUST_CARTON_LENG = Convert.ToInt32(tb_CUST_CARTON_LENG.Text),
                //          CARTON_LAB_NAME = tb_CARTON_LAB_NAME.Text,
                //          CUST_CARTON_POSTFIX = tb_CUST_CARTON_POSTFIX.Text,
                //          CUST_CARTON_PREFIX = tb_CUST_CARTON_PREFIX.Text,
                //          CUST_CARTON_STR = tb_CUST_CARTON_STR.Text,
                //          CUST_LAST_CARTON = tb_CUST_LAST_CARTON.Text,
                //          CUST_LAST_PALLET = tb_CUST_LAST_PALLET.Text,
                //          CUST_PALLET_LENG = Convert.ToInt32(tb_CUST_PALLET_LENG.Text),
                //          CUST_PALLET_POSTFIX = tb_CUST_PALLET_POSTFIX.Text,
                //          CUST_PALLET_PREFIX = tb_CUST_PALLET_PREFIX.Text,
                //          CUST_PALLET_STR = tb_CUST_PALLET_STR.Text,
                //          PALLET_LAB_NAME = tb_PALLET_LAB_NAME.Text,
                //          EMP_NO = sInfo.gUserInfo.userId,
                //          CUST_END_CARTON=tb_cust_end_carton.Text
                //      });
                MessageBox.Show(sRes == "OK" ? "新增完成" : "新增数据失败:" + sRes);
                LoadInfoSnRuleWO();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void imbt_clear_Click(object sender, EventArgs e)
        {
            ClearCntrValue(groupBox7);
            ClearCntrValue(groupBox9);
            ClearCntrValue(groupBox10);
            ClearCntrValue(groupBox11);
            ClearCntrValue(groupBox12);
            tbt_CUST_BOX_LENG.Text = "0";
            tbt_CUST_SN_LENG.Text = "0";
            tbt_CUST_PALLET_LENG.Text = "0";
            tbt_CUST_CARTON_LENG.Text = "0";


        }

        private void imbt_modify_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> Dic = new Dictionary<string, object>();
            FrmBLL.publicfuntion.SerializeControl(Dic, tabPage2);
            Dic.Add("EMP_NO", sInfo.gUserInfo.userId);
            string sRes = refWebtB_SnRule_WO.Instance.UpdateB_SNRULE_WO(FrmBLL.ReleaseData.DictionaryToJson(Dic));
            //string sRes = refWebtB_SnRule_WO.Instance.UpdateB_SNRULE_WO(new WebServices.tB_SnRule_WO.B_SNRULE_WO_Table()
            //    {
            //        WOID = tbt_WOID.Text.Trim(),
            //        CUST_SN_PREFIX = tbt_CUST_SN_PREFIX.Text,
            //        CUST_SN_POSTFIX = tbt_CUST_SN_POSTFIX.Text,
            //        CUST_SN_LENG = Convert.ToInt32(tbt_CUST_SN_LENG.Text),
            //        CUST_SN_STR = tbt_CUST_SN_STR.Text,
            //        CUST_LAST_SN = tbt_CUST_LAST_SN.Text,
            //        CUST_BOX_LENG = Convert.ToInt32(tbt_CUST_BOX_LENG.Text),
            //        CUST_BOX_PREFIX = tbt_CUST_BOX_PREFIX.Text,
            //        CUST_BOX_STR = tbt_CUST_BOX_STR.Text,
            //        BOX_LAB_NAME = tbt_BOX_LAB_NAME.Text,
            //        CUST_LAST_BOX = tbt_CUST_LAST_BOX.Text,
            //        CUST_CARTON_LENG = Convert.ToInt32(tbt_CUST_CARTON_LENG.Text),
            //        CARTON_LAB_NAME = tbt_CARTON_LAB_NAME.Text,
            //        CUST_CARTON_POSTFIX = tbt_CUST_CARTON_POSTFIX.Text,
            //        CUST_CARTON_PREFIX = tbt_CUST_CARTON_PREFIX.Text,
            //        CUST_CARTON_STR = tbt_CUST_CARTON_STR.Text,
            //        CUST_LAST_CARTON = tbt_CUST_LAST_CARTON.Text,
            //        CUST_LAST_PALLET = tbt_CUST_LAST_PALLET.Text,
            //        CUST_PALLET_LENG = Convert.ToInt32(tbt_CUST_PALLET_LENG.Text),
            //        CUST_PALLET_POSTFIX = tbt_CUST_PALLET_POSTFIX.Text,
            //        CUST_PALLET_PREFIX = tbt_CUST_PALLET_PREFIX.Text,
            //        CUST_PALLET_STR = tbt_CUST_PALLET_STR.Text,
            //        PALLET_LAB_NAME = tbt_PALLET_LAB_NAME.Text,
            //        EMP_NO = sInfo.gUserInfo.userId,
            //         CUST_END_CARTON=tbt_cust_end_carton.Text
                     
            //    });
            MessageBox.Show(sRes == "OK" ? "修改完成" : "修改数据失败:" + sRes);
            LoadInfoSnRuleWO();

        }

        private void dgv_snrulewo_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgv_snrulewo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                tbt_CUST_SN_PREFIX.Enabled = false;
                tbt_CUST_SN_POSTFIX.Enabled = false;
                tbt_CUST_SN_STR.Enabled = false;
                tbt_CUST_SN_LENG.Enabled = false;

                tbt_WOID.Text = dgv_snrulewo["WOID", e.RowIndex].Value.ToString();
                tbt_CUST_SN_PREFIX.Text = dgv_snrulewo["CUST_SN_PREFIX", e.RowIndex].Value.ToString();
                tbt_CUST_SN_POSTFIX.Text = dgv_snrulewo["CUST_SN_POSTFIX", e.RowIndex].Value.ToString();
                tbt_CUST_SN_LENG.Text = dgv_snrulewo["CUST_SN_LENG", e.RowIndex].Value.ToString();
                tbt_CUST_SN_STR.Text = dgv_snrulewo["CUST_SN_STR", e.RowIndex].Value.ToString();
                tbt_CUST_LAST_SN.Text = dgv_snrulewo["CUST_LAST_SN", e.RowIndex].Value.ToString();
                tbt_CUST_BOX_LENG.Text = dgv_snrulewo["CUST_BOX_LENG", e.RowIndex].Value.ToString();
                tbt_CUST_BOX_PREFIX.Text = dgv_snrulewo["CUST_BOX_PREFIX", e.RowIndex].Value.ToString();
                tbt_CUST_BOX_STR.Text = dgv_snrulewo["CUST_BOX_STR", e.RowIndex].Value.ToString();
                tbt_BOX_LAB_NAME.Text = dgv_snrulewo["BOX_LAB_NAME", e.RowIndex].Value.ToString();
                tbt_CUST_LAST_BOX.Text = dgv_snrulewo["CUST_LAST_BOX", e.RowIndex].Value.ToString();
                tbt_CUST_CARTON_LENG.Text = dgv_snrulewo["CUST_CARTON_LENG", e.RowIndex].Value.ToString();
                tbt_CARTON_LAB_NAME.Text = dgv_snrulewo["CARTON_LAB_NAME", e.RowIndex].Value.ToString();
                tbt_CUST_CARTON_POSTFIX.Text = dgv_snrulewo["CUST_CARTON_POSTFIX", e.RowIndex].Value.ToString();
                tbt_CUST_CARTON_PREFIX.Text = dgv_snrulewo["CUST_CARTON_PREFIX", e.RowIndex].Value.ToString();
                tbt_CUST_CARTON_STR.Text = dgv_snrulewo["CUST_CARTON_STR", e.RowIndex].Value.ToString();
                tbt_CUST_LAST_CARTON.Text = dgv_snrulewo["CUST_LAST_CARTON", e.RowIndex].Value.ToString();
                tbt_CUST_LAST_PALLET.Text = dgv_snrulewo["CUST_LAST_PALLET", e.RowIndex].Value.ToString();
                tbt_CUST_PALLET_LENG.Text = dgv_snrulewo["CUST_PALLET_LENG", e.RowIndex].Value.ToString();
                tbt_CUST_PALLET_POSTFIX.Text = dgv_snrulewo["CUST_PALLET_POSTFIX", e.RowIndex].Value.ToString();
                tbt_CUST_PALLET_PREFIX.Text = dgv_snrulewo["CUST_PALLET_PREFIX", e.RowIndex].Value.ToString();
                tbt_CUST_PALLET_STR.Text = dgv_snrulewo["CUST_PALLET_STR", e.RowIndex].Value.ToString();
                tbt_PALLET_LAB_NAME.Text = dgv_snrulewo["PALLET_LAB_NAME", e.RowIndex].Value.ToString();
                tbt_cust_end_carton.Text = dgv_snrulewo["CUST_END_CARTON", e.RowIndex].Value.ToString();

            }

          

        }










        /*
         
            WOID
            CUST_SN_PREFIX
            CUST_SN_POSTFIX
            CUST_SN_LENG
            CUST_SN_STR
            CUST_LAST_SN
            CUST_BOX_PREFIX
            CUST_BOX_LENG
            CUST_BOX_STR
            CUST_LAST_BOX
            BOX_LAB_NAME
            CUST_CARTON_PREFIX
            CUST_CARTON_POSTFIX
            CUST_CARTON_LENG
            CUST_CARTON_STR
            CUST_LAST_CARTON
            CARTON_LAB_NAME
            CUST_PALLET_PREFIX
            CUST_PALLET_POSTFIX
            CUST_PALLET_LENG
            CUST_PALLET_STR
            CUST_LAST_PALLET
            PALLET_LAB_NAME
            IN_STATION_TIME
            EMP_NO
         
         */





    }
}
