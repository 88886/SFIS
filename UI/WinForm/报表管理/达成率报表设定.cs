using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.Threading;
using System.Xml;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace SFIS_V2
{
    public partial class Frm_PLAN_RATE_REPORT : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_PLAN_RATE_REPORT(MainParent mfm)
        {
            InitializeComponent();
            mFrm = mfm;
        }
        MainParent mFrm;
        public DataTable dt_pn = new DataTable();
        private void Frm_PLAN_RATE_REPORT_Load(object sender, EventArgs e)
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
            cb_dateTime.Value =Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd 00:00:00"));
            cb_pn.Items.Clear();
            dt_pn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetAllProduct());
            if (dt_pn.Rows.Count > 0)
            {
                for (int i = 0; i < dt_pn.Rows.Count; i++)
                {
                    cb_pn.Items.Add(dt_pn.Rows[i]["partnumber"].ToString());
                }
            }
            cb_class.SelectedIndex = 0;
            DataTable RouteTable = FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo());
            if (RouteTable.Rows.Count > 0)
            {
                foreach (DataRow dr in RouteTable.DefaultView.ToTable(true, "craftname").Rows)
                {
                    if (dr["craftname"].ToString().Substring(0, 2) != "R_")
                        this.cb_routing.Items.Add(dr["craftname"].ToString());
                }
            }
        }

        private void cb_pn_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtb_msge.Text = "";
            txt_name.Text = "";
            txt_color.Text = "";
            dgv_plan_info.Rows.Clear();
            for (int i = 0; i < dt_pn.Rows.Count; i++)
            {
                if (dt_pn.Rows[i]["partnumber"].ToString() == cb_pn.Text.ToString())
                {
                    txt_name.Text = dt_pn.Rows[i]["productname"].ToString();
                    txt_color.Text = dt_pn.Rows[i]["productcolor"].ToString();
                    break;
                }
            }
            get_plan_ifo();
            txt_bu.SelectAll();
            txt_bu.Focus();
        }
        public void get_plan_ifo()
        {
            DataTable dt_plan_info = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPlan_Rate_Report.Instance.getPlan_Rate_Set_bypn("",cb_dateTime.Value.ToString("yyyyMMdd"), cb_pn.Text.ToString()));
            if (dt_plan_info.Rows.Count > 0)
            {
                for (int i = 0; i < dt_plan_info.Rows.Count; i++)
                {
                    dgv_plan_info.Rows.Add(dt_plan_info.Rows[i]["work_date"].ToString(), dt_plan_info.Rows[i]["partnumber"].ToString(), dt_plan_info.Rows[i]["productname"].ToString(), dt_plan_info.Rows[i]["productcolor"].ToString(),
                     dt_plan_info.Rows[i]["work_class"].ToString(),
                     dt_plan_info.Rows[i]["business_unit"].ToString(), dt_plan_info.Rows[i]["section_name"].ToString(), dt_plan_info.Rows[i]["craftname"].ToString(), dt_plan_info.Rows[i]["target_qty"].ToString(),
                     dt_plan_info.Rows[i]["username"].ToString(), dt_plan_info.Rows[i]["recdate"].ToString());
                }
            }
        }

        private void cb_dateTime_Click(object sender, EventArgs e)
        {
                dgv_plan_info.Rows.Clear();
                get_plan_ifo();
        }

        private void cb_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_bu.SelectAll();
            txt_bu.Focus();
        }

        private void txt_bu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_section.SelectAll();
                txt_section.Focus();
            }
        }

        private void txt_section_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_target.SelectAll();
                txt_target.Focus();
            }
        }

        private void cb_routing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tx_routing.Text.ToString()))
            {
                tx_routing.Text = cb_routing.SelectedItem.ToString();
            }
            else
            {
                tx_routing.Text += ","+cb_routing.SelectedItem.ToString();
            }
            txt_target.SelectAll();
            txt_target.Focus();
        }

        private void txt_target_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                add_info();
            }
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            add_info();
        }
        public void add_info()
        {
            rtb_msge.Text = "";
            #region 检查所资料是否填入完整
            if (string.IsNullOrEmpty(cb_dateTime.Value.ToString()))
            {
                rtb_msge.Text = "请选择日期！！";
                MessageBox.Show("请选择日期！！");
                return;
            }
            if (string.IsNullOrEmpty(txt_name.Text.ToString()) && string.IsNullOrEmpty(txt_color.Text.ToString()))
            {
                rtb_msge.Text = "请选择产品料号！！";
                MessageBox.Show("请选择产品料号！！");
                return;
            }
            if (string.IsNullOrEmpty(cb_class.SelectedItem.ToString()))
            {
                rtb_msge.Text = "请选择班别！！";
                MessageBox.Show("请选择班别！！");
                return;
            }
            if (string.IsNullOrEmpty(txt_bu.Text.ToString()))
            {
                rtb_msge.Text = "请输入BU！！";
                MessageBox.Show("请输入BU！！");
                return;
            }
            if (string.IsNullOrEmpty(txt_section.Text.ToString()))
            {
                rtb_msge.Text = "请输入制程段！！";
                MessageBox.Show("请输入制程段！！");
                return;
            }
            if (string.IsNullOrEmpty(tx_routing.Text.ToString()))
            {
                rtb_msge.Text = "请选择途程！！";
                MessageBox.Show("请选择途程！！");
                return;
            }
            if (string.IsNullOrEmpty(txt_target.Text.ToString()))
            {
                rtb_msge.Text = "请输入计划产出！！";
                MessageBox.Show("请输入计划产出！！");
                return;
            }
            #endregion

            #region 检查是否重复新增
             //if (dgv_plan_info.Rows.Count > 0)
             //{
                 
             //}
            #endregion

            #region 新增资料
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("WORK_MONTH", cb_dateTime.Value.ToString("yyyyMMdd").Substring(0, 6));
            dic.Add("WORK_DATE", cb_dateTime.Value.ToString("yyyyMMdd"));
            dic.Add("WORK_CLASS", cb_class.SelectedItem.ToString());
            dic.Add("BUSINESS_UNIT", txt_bu.Text.ToString());
            dic.Add("PRODUCTNAME", txt_name.Text.ToString());
            dic.Add("PARTNUMBER", cb_pn.Text.ToString());
            dic.Add("PRODUCTCOLOR", txt_color.Text.ToString());
            dic.Add("SECTION_NAME", txt_section.Text.ToString());
            dic.Add("CRAFTNAME", tx_routing.Text.ToString().Trim());
            dic.Add("TARGET_QTY", Convert.ToInt32(txt_target.Text.ToString()));
            dic.Add("USERID", this.mFrm.gUserInfo.userId);

            string Err = refWebtPlan_Rate_Report.Instance.insert_Plan_Rate(FrmBLL.ReleaseData.DictionaryToJson(dic));
            //string Err = refWebtPlan_Rate_Report.Instance.insert_Plan_Rate(new WebServices.tPlan_Rate_Report.tPlanRateReport()
            // {
            //     WORK_MONTH = cb_dateTime.Value.ToString("yyyyMMdd").Substring(0, 6),
            //    WORK_DATE = cb_dateTime.Value.ToString("yyyyMMdd"),
            //     WORK_CLASS = cb_class.SelectedItem.ToString(),
            //    BUSINESS_UNIT=txt_bu.Text.ToString(),
            //    PRODUCTNAME=txt_name.Text.ToString(),
            //    PARTNUMBER =cb_pn.Text.ToString(),
            //    PRODUCTCOLOR = txt_color.Text.ToString(),
            //    SECTION_NAME =txt_section.Text.ToString(),
            //     CRAFTNAME = tx_routing.Text.ToString().Trim(),
            //    TARGET_QTY =Convert.ToInt32(txt_target.Text.ToString()),
            //    USERID =this.mFrm.gUserInfo.userId
            // });
             if (Err == "OK")
             {
                 dgv_plan_info.Rows.Add(cb_dateTime.Value.ToString("yyyyMMdd"), cb_pn.Text.ToString(), txt_name.Text.ToString(), txt_color.Text.ToString(), cb_class.SelectedItem.ToString(),
                     txt_bu.Text.ToString(), txt_section.Text.ToString(), tx_routing.Text.ToString().Trim(), txt_target.Text.ToString(),
                     this.mFrm.gUserInfo.username, System.DateTime.Now);
                 txt_bu.Text = "";
                 txt_section.Text = "";
                 txt_target.Text = "";
                 tx_routing.Text = "";
                 rtb_msge.Text = "新增成功！！";
                 MessageBox.Show("新增成功！！");
             }
             else
             {
                 rtb_msge.Text =  "新增失败！！"+ Err;
                 MessageBox.Show("新增失败！！" + Err);
                 return;
             }

            #endregion


        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            cancel_all();
        }
        public void cancel_all()
        {
            rtb_msge.Text = "";
            txt_name.Text = "";
            txt_color.Text = "";
            txt_bu.Text = "";
            txt_section.Text = "";
            txt_target.Text = "";
            tx_routing.Text = "";
            dgv_plan_info.Rows.Clear();
            bt_add.Visible = true;
            bt_excel_add.Visible = false;
            bt_delete.Visible = true;
            bt_select.Visible = true;
            cb_dateTime.Enabled = true;
            cb_pn.Enabled = true;
        }

        private void bt_select_Click(object sender, EventArgs e)
        {
            dgv_plan_info.Rows.Clear();
            rtb_msge.Text = "";
            if (string.IsNullOrEmpty(cb_dateTime.Value.ToString("yyyyMMdd")) && string.IsNullOrEmpty(cb_class.Text.ToString()))
            {
                rtb_msge.Text = "日期和产品料号至少选择一项！！";
                MessageBox.Show("日期和产品料号至少选择一项！！");
                return;
            }
            else
            {
                get_plan_ifo();
                if (dgv_plan_info.Rows.Count < 1)
                {
                    MessageBox.Show("无资料！！");
                    rtb_msge.Text = "无资料";
                    return;
                }
            }
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            if (this.dgv_plan_info.SelectedRows.Count > 0)
            {
                int k = dgv_plan_info.SelectedRows.Count;
                if (MessageBox.Show("您确认要删除这" + Convert.ToString(k) + "项吗？", "接收提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (k != dgv_plan_info.Rows.Count - 2)//因为还有一行为统计行所以减2
                    {
                        Dictionary<string, object> dic = null;
                        for (int i = k; i >= 1; i--)//从下往上删，避免沙漏效应
                        {
                            dic = new Dictionary<string, object>();
                            dic.Add("WORK_DATE", dgv_plan_info.CurrentRow.Cells[0].Value.ToString());
                            dic.Add("WORK_CLASS", dgv_plan_info.CurrentRow.Cells[4].Value.ToString());
                            dic.Add("BUSINESS_UNIT", dgv_plan_info.CurrentRow.Cells[5].Value.ToString());
                            dic.Add("PARTNUMBER", dgv_plan_info.CurrentRow.Cells[1].Value.ToString());
                            dic.Add("SECTION_NAME", dgv_plan_info.CurrentRow.Cells[6].Value.ToString());
                            dic.Add("CRAFTNAME", dgv_plan_info.CurrentRow.Cells[7].Value.ToString());
                            string Err = refWebtPlan_Rate_Report.Instance.delete_Plan_Rate(FrmBLL.ReleaseData.DictionaryToJson(dic));
                            //string Err = refWebtPlan_Rate_Report.Instance.delete_Plan_Rate(new WebServices.tPlan_Rate_Report.tPlanRateReport()
                            //{
                            //    WORK_DATE = dgv_plan_info.CurrentRow.Cells[0].Value.ToString(),
                            //    WORK_CLASS = dgv_plan_info.CurrentRow.Cells[4].Value.ToString(),
                            //    BUSINESS_UNIT = dgv_plan_info.CurrentRow.Cells[5].Value.ToString(),
                            //    PARTNUMBER = dgv_plan_info.CurrentRow.Cells[1].Value.ToString(),
                            //    SECTION_NAME = dgv_plan_info.CurrentRow.Cells[6].Value.ToString(),
                            //    CRAFTNAME = dgv_plan_info.CurrentRow.Cells[7].Value.ToString()
                            //});
                            if (Err == "OK")
                            {
                                dgv_plan_info.Rows.RemoveAt(dgv_plan_info.SelectedRows[i - 1].Index);
                            }
                            else
                            {
                                rtb_msge.Text = "删除失败！！" + Err;
                                MessageBox.Show("删除失败！！" + Err);
                                return;
                            }
                        }
                        rtb_msge.Text = "删除完成！！";
                        MessageBox.Show("删除完成！！");
                    }
                }
            }
            else
            {
                rtb_msge.Text = "请选中需要删除的行！";
                MessageBox.Show("请选中需要删除的行！！");
                return;
            }
        }
        private void dgv_plan_info_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgv_plan_info.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgv_plan_info.RowHeadersDefaultCellStyle.Font, rectangle,
                dgv_plan_info.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void txt_target_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void bt_cancel_routing_Click(object sender, EventArgs e)
        {
            tx_routing.Text = "";
        }

        private void bt_excel_Click(object sender, EventArgs e)
        {
            rtb_msge.Text = "导入档案格式：第1列:日期,格式:YYYYMMDD;第2列:班别;第3列:BU;第4列:制成别;第5列:料号;第6列:计划数量;第7列:途程";
            MessageBox.Show("导入档案格式：第1列:日期,格式:YYYYMMDD;第2列:班别;第3列:BU;第4列:制成别;第5列:料号;第6列:计划数量;第7列:途程");
             OpenFileDialog file = new OpenFileDialog();
             file.Title = "选择Excel";
             file.Filter = "(*.xls Excel 2003)|*.xls";
             if (file.ShowDialog() == DialogResult.OK)
             {
                 cancel_all();
                 string conString = file.FileName;
                 DataSet da = FrmBLL.ClsReadExcel.getDataSet(file.FileName, FrmBLL.ClsReadExcel.GetTableNames(file.FileName)[0]);
              
                 //string strSoure = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + conString + ";Extended Properties=Excel 8.0;HDR=Yes;IMEX=1";
                 //OleDbConnection conn = new OleDbConnection(strSoure);
                 ////string sqlstring = @"select * from [" + ss + "$]";
                 ////OleDbDataAdapter adapter = new OleDbDataAdapter(sqlstring, conn);
                 //DataSet da = new DataSet();
                 ////adapter.Fill(da);

                 //conn.Open();
                 //DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
                 //string[] strTableNames = new string[dtSheetName.Rows.Count];
                 //for (int k = 0; k < dtSheetName.Rows.Count; k++)
                 //{
                 //    strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
                 //}
                 //OleDbDataAdapter myCommand = null;
                 //DataTable dt = new DataTable();
                 //string strExcel = "select * from [" + strTableNames[0] + "]";
                 //myCommand = new OleDbDataAdapter(strExcel, strSoure);
                 //myCommand.Fill(da, "dtSource");
                 //conn.Close(); 

                 //dt = new DataTable();
                 //myCommand.Fill(dt);
                 //da.Tables.Add(dt);

                 if (da.Tables[0].Rows.Count > 0)
                 {
                     if (da.Tables[0].Columns.Count !=7)
                     {
                         rtb_msge.Text = "档案格式应为7列，当前列数：" + da.Tables[0].Columns.Count + "，请确认档案格式";
                         MessageBox.Show("档案格式应为7列，当前列数：" + da.Tables[0].Columns.Count + "，请确认档案格式");
                         return;
                     }
                     string pn_name = "";
                     string pn_color = "";
                     bool pn_exist = false;
                     int m = 2;
                     for (int i = 0; i < da.Tables[0].Rows.Count; i++)
                     {
                         m += i;
                         dgv_plan_info.Rows.Add(da.Tables[0].Rows[i][0].ToString(), da.Tables[0].Rows[i][4].ToString(), pn_name, pn_color, da.Tables[0].Rows[i][1].ToString(),
                                 da.Tables[0].Rows[i][2].ToString(), da.Tables[0].Rows[i][3].ToString(), da.Tables[0].Rows[i][6].ToString(), da.Tables[0].Rows[i][5].ToString(),
                                 this.mFrm.gUserInfo.username, System.DateTime.Now);
                         #region 检验EXCEL中的数据是否正确
                         if (!IsDate(da.Tables[0].Rows[i][0].ToString()))
                         {
                             rtb_msge.Text = "第" + m + "行,不符合日期格式，请确认->" + da.Tables[0].Rows[i][0].ToString();
                             MessageBox.Show("第" + m + "行,不符合日期格式，请确认->" + da.Tables[0].Rows[i][0].ToString());
                             return;
                         }
                         if (da.Tables[0].Rows[i][1].ToString() != "D" && da.Tables[0].Rows[i][1].ToString() != "N")
                         {
                             rtb_msge.Text = "第" + m + "行,班别设定错误，请确认->" + da.Tables[0].Rows[i][1].ToString();
                             MessageBox.Show("第" + m + "行,班别设定错误，请确认->" + da.Tables[0].Rows[i][1].ToString());
                             return;
                         }
                         if (string.IsNullOrEmpty(da.Tables[0].Rows[i][2].ToString()))
                         {
                             rtb_msge.Text = "第" + m + "行,BU不能为空，请确认！！";
                             MessageBox.Show("第" + m + "行,BU不能为空，请确认！！");
                             return;
                         }
                         if (string.IsNullOrEmpty(da.Tables[0].Rows[i][3].ToString()))
                         {
                             rtb_msge.Text = "第" + m + "行,制程段不能为空，请确认！！";
                             MessageBox.Show("第" + m + "行,制程段不能为空，请确认！！");
                             return;
                         }
                         for (int j = 0; j < dt_pn.Rows.Count; j++)
                         {
                             if (dt_pn.Rows[j]["partnumber"].ToString() == da.Tables[0].Rows[i][4].ToString())
                             {
                                 pn_name = dt_pn.Rows[j]["productname"].ToString();
                                 pn_color = dt_pn.Rows[j]["productcolor"].ToString();
                                 pn_exist = true;
                                 break;
                             }
                         }
                         if (pn_exist == false)
                         {
                             rtb_msge.Text = "第" + m + "行,料号不存在，请确认->" + da.Tables[0].Rows[i][4].ToString();
                             MessageBox.Show("第" + m + "行,料号不存在，请确认->" + da.Tables[0].Rows[i][4].ToString());
                             return;
                         }
                         try
                         {
                             int n = 0;
                             n = int.Parse(da.Tables[0].Rows[i][5].ToString());
                             if (n < 1)
                             {
                                 rtb_msge.Text = "第" + m + "行,计划数量不能小于1，请确认->" + da.Tables[0].Rows[i][5].ToString();
                                 MessageBox.Show("第" + m + "行,计划数量不能小于1，请确认->" + da.Tables[0].Rows[i][5].ToString());
                                 return;
                             }
                         }
                         catch
                         {
                             MessageBox.Show("第" + m + "行,计划数量不是数字，请确认->" + da.Tables[0].Rows[i][5].ToString());
                         }
                         string[] strs = da.Tables[0].Rows[i][6].ToString().Split(',');
                         foreach (string _routing in strs)
                         {
                             if (!cb_routing.Items.Contains(_routing))
                             {
                                 rtb_msge.Text = "第" + m + "行,途程：" + _routing + "，不存在，请确认";
                                 MessageBox.Show("第" + m + "行,途程：" + _routing + "，不存在，请确认");
                                 return;
                             }
                         }
                         #endregion  
                         #region 新增资料
                         //string Err = refWebtPlan_Rate_Report.Instance.insert_Plan_Rate(new WebServices.tPlan_Rate_Report.tPlanRateReport()
                         //{
                         //    WORK_MONTH = da.Tables[0].Rows[i][0].ToString().Substring(0, 6),
                         //    WORK_DATE = da.Tables[0].Rows[i][0].ToString(),
                         //    WORK_CLASS = da.Tables[0].Rows[i][1].ToString(),
                         //    BUSINESS_UNIT = da.Tables[0].Rows[i][2].ToString(),
                         //    PRODUCTNAME = pn_name,
                         //    PARTNUMBER = da.Tables[0].Rows[i][4].ToString(),
                         //    PRODUCTCOLOR = pn_color,
                         //    SECTION_NAME = da.Tables[0].Rows[i][3].ToString(),
                         //    CRAFTNAME = da.Tables[0].Rows[i][6].ToString(),
                         //    TARGET_QTY = Convert.ToInt32(da.Tables[0].Rows[i][5].ToString()),
                         //    USERID = this.mFrm.gUserInfo.userId
                         //});
                         //if (Err == "OK")
                         //{
                         //    dgv_plan_info.Rows.Add(da.Tables[0].Rows[i][0].ToString(), da.Tables[0].Rows[i][4].ToString(), pn_name, pn_color, da.Tables[0].Rows[i][1].ToString(),
                         //        da.Tables[0].Rows[i][2].ToString(), da.Tables[0].Rows[i][3].ToString(), da.Tables[0].Rows[i][6].ToString(), da.Tables[0].Rows[i][5].ToString(),
                         //        this.mFrm.gUserInfo.username, System.DateTime.Now);
                         //}
                         //else
                         //{
                         //    rtb_msge.Text = "第" + m + "行,导入失败！！" + Err + da.Tables[0].Rows[i][4].ToString();
                         //    MessageBox.Show("第" + m + "行,导入失败！！" + Err + da.Tables[0].Rows[i][4].ToString());
                         //    return;
                         //}
                         pn_name = "";
                         pn_color = "";
                         //list_routing = new List<string>();
                         #endregion
                     }
                     rtb_msge.Text = "数据导入完成！！请确认无误后点击<确认导入>按钮，上传到系统";
                     MessageBox.Show("数据导入完成！！请确认无误后点击<确认导入>按钮，上传到系统");
                     bt_excel_add.Visible = true;
                     bt_add.Visible = false;
                     bt_delete.Visible = false;
                     bt_select.Visible = false;
                     cb_dateTime.Enabled = false;
                     cb_pn.Enabled = false;
                 }
                 else
                 {
                     rtb_msge.Text = "EXCEL中无数据，请确认！！";
                     MessageBox.Show("EXCEL中无数据，请确认！！");
                     return;
                 }
             }
        }
        public bool IsDate(string strDate)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void bt_excel_add_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> dic = null;
            if (dgv_plan_info.Rows.Count > 0)
            {
                string Err="";
                for (int i = 0; i < dgv_plan_info.Rows.Count; i++)
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("WORK_MONTH", dgv_plan_info.Rows[i].Cells[0].Value.ToString().Substring(0, 6));
                    dic.Add("WORK_DATE", dgv_plan_info.Rows[i].Cells[0].Value.ToString());
                    dic.Add("WORK_CLASS", dgv_plan_info.Rows[i].Cells[4].Value.ToString());
                    dic.Add("BUSINESS_UNIT", dgv_plan_info.Rows[i].Cells[5].Value.ToString());
                    dic.Add("PRODUCTNAME", dgv_plan_info.Rows[i].Cells[2].Value.ToString());
                    dic.Add("PARTNUMBER", dgv_plan_info.Rows[i].Cells[1].Value.ToString());
                    dic.Add("PRODUCTCOLOR", dgv_plan_info.Rows[i].Cells[3].Value.ToString());
                    dic.Add("SECTION_NAME", dgv_plan_info.Rows[i].Cells[6].Value.ToString());
                    dic.Add("CRAFTNAME", dgv_plan_info.Rows[i].Cells[7].Value.ToString());
                    dic.Add("TARGET_QTY", Convert.ToInt32(dgv_plan_info.Rows[i].Cells[8].Value.ToString()));
                    dic.Add("USERID", this.mFrm.gUserInfo.userId);

                    Err = refWebtPlan_Rate_Report.Instance.insert_Plan_Rate(FrmBLL.ReleaseData.DictionaryToJson(dic));

                    //Err = refWebtPlan_Rate_Report.Instance.insert_Plan_Rate(new WebServices.tPlan_Rate_Report.tPlanRateReport()
                    //{
                    //    WORK_MONTH = dgv_plan_info.Rows[i].Cells[0].Value.ToString().Substring(0, 6),
                    //    WORK_DATE = dgv_plan_info.Rows[i].Cells[0].Value.ToString(),
                    //    WORK_CLASS = dgv_plan_info.Rows[i].Cells[4].Value.ToString(),
                    //    BUSINESS_UNIT = dgv_plan_info.Rows[i].Cells[5].Value.ToString(),
                    //    PRODUCTNAME = dgv_plan_info.Rows[i].Cells[2].Value.ToString(),
                    //    PARTNUMBER = dgv_plan_info.Rows[i].Cells[1].Value.ToString(),
                    //    PRODUCTCOLOR = dgv_plan_info.Rows[i].Cells[3].Value.ToString(),
                    //    SECTION_NAME = dgv_plan_info.Rows[i].Cells[6].Value.ToString(),
                    //    CRAFTNAME = dgv_plan_info.Rows[i].Cells[7].Value.ToString(),
                    //    TARGET_QTY = Convert.ToInt32(dgv_plan_info.Rows[i].Cells[8].Value.ToString()),
                    //    USERID = this.mFrm.gUserInfo.userId
                    //});
                    if (Err != "OK")
                    {
                        rtb_msge.Text = "第" + i + "行,导入失败！！请重新导入" + Err + dgv_plan_info.Rows[i].Cells[4].Value.ToString();
                        MessageBox.Show("第" + i + "行,导入失败！！请重新导入" + Err + dgv_plan_info.Rows[i].Cells[4].Value.ToString());
                    }
                }
                if (Err == "OK")
                {
                    rtb_msge.Text = "数据导入成功！！";
                    MessageBox.Show("数据导入成功");
                    bt_excel_add.Visible = false;
                    bt_add.Visible = true;
                    bt_delete.Visible = true;
                    bt_select.Visible = true;
                    cb_dateTime.Enabled = true;
                    cb_pn.Enabled = true;
                }
            }
        }

    }
}