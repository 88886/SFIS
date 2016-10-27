using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.Xml;

namespace SFIS_V2
{
    public partial class Frm_Plan_Control : Office2007Form //Form
    {
        public Frm_Plan_Control(MainParent sFrm)
        {
            InitializeComponent();
            sMain = sFrm;
        }
        MainParent sMain;
        string CradtName = string.Empty;
        private void Frm_Plan_Control_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.sMain.gUserInfo.rolecaption == "系统开发员")
            {
                IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PROGID", this.Name);
                dic.Add("PROGNAME", this.Text);
                dic.Add("PROGDESC", this.Text);
                FrmBLL.publicfuntion.AddProgInfo(dic,lsfunls);
                //FrmBLL.publicfuntion.AddProgInfo(new WebServices.tUserInfo.tProgInfo()
                //{
                //    progid = this.Name,
                //    progname = this.Text,
                //    progdesc = this.Text

                //}, lsfunls);
            }
            #endregion

            //dtStart.Value = DateTime.Now.AddDays(-1);
            //dtEnd.Value = DateTime.Now;
            //dtStart.MaxDate = DateTime.Now.AddDays(-1);
            //dtEnd.MaxDate = DateTime.Now;
           
            //this.dgvData.RowsDefaultCellStyle.BackColor = Color.Bisque;
            //this.dgvData.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            //XmlDocument doc = new XmlDocument();
            //string XmlName = "DllConfig.xml";
            //doc.Load(XmlName);
            // CradtName = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("Plan_Report").SelectSingleNode("CRAFTNAME")).GetAttribute("Name").ToString();

            //labcraft.Text = "获取途程:" + CradtName;
            dtStart.Value = DateTime.Now.AddDays(-1);
            dtEnd.Value = DateTime.Now;
            dtStart.MaxDate = DateTime.Now.AddDays(-1);
            dtEnd.MaxDate = DateTime.Now;

            dtRepairStart.Value = DateTime.Now.AddDays(-1);
            dtRepairStart.MaxDate = DateTime.Now;

            dtRepairEnd.Value = DateTime.Now;
            dtRepairEnd.MaxDate = DateTime.Now;

            this.dgvData.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvData.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;


            this.dgvShowData.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvShowData.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
        }

        private DataTable Get_YieldRate(string Start,string End)
        {

            XmlDocument doc = new XmlDocument();
            string XmlName = "DllConfig.xml";
            doc.Load(XmlName);
            string CradtName = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("Plan_Report").SelectSingleNode("CRAFTNAME")).GetAttribute("Name").ToString();

            labcraft.Text = "获取途程:" + CradtName;

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("WOID", "ALL");
            dic.Add("PARTNUMBER", "ALL");
            dic.Add("LINEID", "ALL");
            dic.Add("CRAFTID", CradtName);
            dic.Add("SHOW_WOID", false);
            dic.Add("SHOW_LINEID", false);
            dic.Add("SHOW_PARTNUMBER", true);
            dic.Add("SHOW_PRODUCTNAME", true);
            dic.Add("SHOW_WORKDATE", false);
            dic.Add("SHOW_CLASS", false);
            dic.Add("SHOW_CLASSDATE", true);
            dic.Add("SHOW_CRAFTID", true);
            dic.Add("SHOW_RPASSQTY", false);
            dic.Add("SHOW_RFAILQTY", false);
            dic.Add("STARTTIME", Start);
            dic.Add("ENDTIME", End);
            
            return  FrmBLL.ReleaseData.arrByteToDataTable(refWebtStationRec.Instance.Get_YieldRate(FrmBLL.ReleaseData.DictionaryToJson(dic)));

           //return  FrmBLL.ReleaseData.arrByteToDataTable(refWebtStationRec.Instance.Get_YieldRate(new WebServices.tStationrecount.tStation_Rec()
           // {
           //     woId = "ALL",
           //     PartNumber = "ALL",
           //     Line = "ALL",
           //     CraftId = "SMT_VI,DX_TEST,CIT_TEST,WIFI_TEST,MII_WORK,REST_TEST,STOCK_IN",
           //     Show_woId = false,
           //     Show_Line = false,
           //     Show_PartNumber = true,
           //     Show_ProductName = true,
           //     Show_WorkData = false,
           //     Show_Class = false,
           //     Show_ClassDate = true,
           //     Show_CraftId = true,
           //     Show_RePassQTY = false,
           //     Show_ReFailQTY = false,
           //     StartTime = Start,
           //     EndTime = End
           // }));
        }
        private void UpdateDataTableColoumnsName(DataTable dt)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ToString().ToUpper() == "WORKDATE")
                {
                    dt.Columns[i].ColumnName = "工作日期";
                }

                if (dt.Columns[i].ToString().ToUpper() == "CRAFTID")
                {
                    dt.Columns[i].ColumnName = "途程";
                }

                if (dt.Columns[i].ToString().ToUpper() == "WOID")
                {
                    dt.Columns[i].ColumnName = "工单";
                }

                if (dt.Columns[i].ToString().ToUpper() == "PARTNUMBER")
                {
                    dt.Columns[i].ColumnName = "产品料号";
                }

                if (dt.Columns[i].ToString().ToUpper() == "PRODUCTNAME")
                {
                    dt.Columns[i].ColumnName = "产品名称";
                }

                if (dt.Columns[i].ToString().ToUpper() == "YIELDRATE")
                {
                    dt.Columns[i].ColumnName = "良率(%)";
                }

                if (dt.Columns[i].ToString().ToUpper() == "LINEID")
                {
                    dt.Columns[i].ColumnName = "线别";
                }

                if (dt.Columns[i].ToString().ToUpper() == "RPASSQTY")
                {
                    dt.Columns[i].ColumnName = "RePASS";
                }

                if (dt.Columns[i].ToString().ToUpper() == "RFAILQTY")
                {
                    dt.Columns[i].ColumnName = "ReFAIL";
                }

                if (dt.Columns[i].ToString().ToUpper() == "CLASS")
                {
                    dt.Columns[i].ColumnName = "班别";
                }

                if (dt.Columns[i].ToString().ToUpper() == "CLASSDATE")
                {
                    dt.Columns[i].ColumnName = "班别日期";
                }
                if (dt.Columns[i].ToString().ToUpper() == "TCLASSDATE")
                {
                    dt.Columns[i].ColumnName = "转入日期";
                }

                if (dt.Columns[i].ToString().ToUpper() == "QTY")
                {
                    dt.Columns[i].ColumnName = "数量";
                }

                if (dt.Columns[i].ToString().ToUpper() == "ERRORCODE")
                {
                    dt.Columns[i].ColumnName = "不良代码";
                }

                if (dt.Columns[i].ToString().ToUpper() == "ERRORDESC")
                {
                    dt.Columns[i].ColumnName = "不良描述";
                }
                if (dt.Columns[i].ToString().ToUpper() == "RCLASSDATE")
                {
                    dt.Columns[i].ColumnName = "转出日期";
                }
                if (dt.Columns[i].ToString().ToUpper() == "REASONCODE")
                {
                    dt.Columns[i].ColumnName = "原因代码";
                }
                if (dt.Columns[i].ToString().ToUpper() == "REASONDESC")
                {
                    dt.Columns[i].ColumnName = "原因描述";
                }
            }


        }

        public static DataTable DataTableToSort(DataTable dt, string Colnums)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} DESC", Colnums);
            return dv.ToTable();
        }

        private void imbt_Query_Click(object sender, EventArgs e)
        {
           // DataTable dtdata = new DataTable();
            //if (!chkall.Checked)
            //{

                DataTable dt1 = Get_YieldRate((dtStart.Value.AddDays(-1)).ToString("yyyyMMdd") + "08", dtEnd.Value.ToString("yyyyMMdd") + "07");
                if (dt1.Rows.Count > 0)
                {
                    DataTable dtdata = dt1.Clone();
                    foreach (DataRow dr in dt1.Rows)
                    {
                        dtdata.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), Convert.ToInt32(dr[4].ToString()) + Convert.ToInt32(dr[5].ToString()));
                    }
                    dtdata.Columns.Remove(dtdata.Columns[dtdata.Columns.Count - 1].ColumnName);

                    DataTable dtSort = FrmBLL.DataTableCrosstab.DataTableCross(DataTableToSort(dtdata, "PRODUCTNAME,PARTNUMBER,CRAFTID"), dtdata.Columns.Count);
                    UpdateDataTableColoumnsName(dtSort);
                    #region 以日期先后顺序排序
                    string[] aa = new string[dtSort.Columns.Count];
                    aa[0] = dtSort.Columns[0].ColumnName;
                    aa[1] = dtSort.Columns[1].ColumnName;
                    aa[2] = dtSort.Columns[2].ColumnName;
                    int x = 3;
                    string ColnumsTotalName = string.Empty;
                    for (int y = 0; y < dtSort.Columns.Count; y++)
                    {
                        ColnumsTotalName += dtSort.Columns[y].ColumnName + ",";
                    }
                    for (int z = 0; z <= Convert.ToInt32(((dtEnd.Value - dtStart.Value).TotalDays)); z++)
                    {
                        if (ColnumsTotalName.Contains((dtStart.Value.AddDays(-1 + z)).ToString("yyyyMMdd")))  //用于当某天没有生产时(无数据),无需加载日期进行排序
                            aa[x++] = (dtStart.Value.AddDays(-1 + z)).ToString("yyyyMMdd");
                    }
                    #endregion
                    dgvData.DataSource = null;
                    dgvData.DataSource = dtSort.DefaultView.ToTable(true, aa);
                    dgvData.Columns[0].Frozen = true;
                    dgvData.Columns[1].Frozen = true;
                    dgvData.Columns[2].Frozen = true;
                }
                else
                {
                    MessageBox.Show("没有数据");
                }
        }

        private void chkall_Click(object sender, EventArgs e)
        {
            if (chkall.Checked)
            {
                dtStart.Enabled = true;
                dtEnd.Enabled = true;
            }
            else
            {
                dtStart.Enabled = false;
                dtEnd.Enabled = false;
            }
        }

        private void imbt_Excel_Click(object sender, EventArgs e)
        {
            FrmBLL.DataGridViewToExcel.DataToExcel(dgvData);
            MessageBox.Show("导出Excel成功");
        }

        private void imbt_queryRepair_Click(object sender, EventArgs e)
        {
           DataTable dt= FrmBLL.ReleaseData.arrByteToDataTable( refWebRepairInfo.Instance.QueryRepairStatus(dtRepairStart.Value.ToString("yyyyMMdd"), dtRepairEnd.Value.ToString("yyyyMMdd"), "0"));
           DataTable dt1 = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.QueryRepairStatus(dtRepairStart.Value.ToString("yyyyMMdd"), dtRepairEnd.Value.ToString("yyyyMMdd"), "1"));

           DataTable dts = new DataTable("mydt");
            string colnum="不良代码,产品序列号,工单,料号,途程,转入人员,线别,转入日期,转入班别";
            foreach (string str in colnum.Split(','))
            {
                dts.Columns.Add(str, typeof(string));                 
            }
            foreach (DataRow dr in dt.Rows)
            {
                dts.Rows.Add(dr["ERRORCODE"].ToString(), dr["ESN"].ToString(), dr["WOID"].ToString(), dr["PARTNUMBER"].ToString(), dr["CRAFTID"].ToString(), dr["INPUTUSER"].ToString(), dr["LINEID"].ToString(), dr["TCLASSDATE"].ToString(), dr["TCLASS"].ToString());
            }
            foreach (DataRow dr in dt1.Rows)
            {
                dts.Rows.Add(dr["ERRORCODE"].ToString(), dr["ESN"].ToString(), dr["WOID"].ToString(), dr["PARTNUMBER"].ToString(), dr["CRAFTID"].ToString(), dr["INPUTUSER"].ToString(), dr["LINEID"].ToString(), dr["TCLASSDATE"].ToString(), dr["TCLASS"].ToString());
            }



            dgvShowData.DataSource = FrmBLL.publicfuntion.DataTableToSort(dts,dts.Columns[7].ColumnName); 
        }

        private void dgvShowData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvShowData.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvShowData.Rows.Count > 0)
            {
                FrmBLL.DataGridViewToExcel.DataToExcel(dgvShowData);            
                MessageBox.Show("汇出Excel成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("没有信息可以汇出", "错误提示",  MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
        }

       
    }
}
