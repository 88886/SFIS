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
    public partial class Frm_Fqc_Report : Office2007Form
    {
        public Frm_Fqc_Report(MainParent mfm)
        {
            InitializeComponent();
            mFrm = mfm;
        }
        MainParent mFrm;
        DataTable dt_QC_report = new DataTable();
        DataTable Dt_QC_info = new DataTable();

        private void Frm_Fqc_Report_Load(object sender, EventArgs e)
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

            dt_QC_report.Columns.Add("日期");
            dt_QC_report.Columns.Add("工单号");
            dt_QC_report.Columns.Add("检验数量");
            dt_QC_report.Columns.Add("抽检数量");
            dt_QC_report.Columns.Add("不良数");
            dt_QC_report.Columns.Add("不良率");
            dt_QC_report.Columns.Add("检验员");



            Dt_QC_info.Columns.Add("日期");
            Dt_QC_info.Columns.Add("检验编号");
            Dt_QC_info.Columns.Add("工单号");
            Dt_QC_info.Columns.Add("栈板号");
            Dt_QC_info.Columns.Add("Tray盘号");
            Dt_QC_info.Columns.Add("卡通箱号");
            Dt_QC_info.Columns.Add("ESN");
            Dt_QC_info.Columns.Add("是否良品");
            Dt_QC_info.Columns.Add("不良代码");
            Dt_QC_info.Columns.Add("不良原因");
            Dt_QC_info.Columns.Add("检验员");


            tabControl1.SelectedTabIndex = 0;
            Cmb_ReportType.SelectedIndex = 0;
        }

        private void Btn_Sel_QC_Click(object sender, EventArgs e)
        {


            if (Cmb_ReportType.Text == "检验统计报表")
            {
                dt_QC_report.Clear();
                DataSet dt = FrmBLL.ReleaseData.arrByteToDataSet(refWebtFQC.Instance.Sel_Fqc_report(Txt_UserId.Text, "", DtTime_Sta.Value, DtTime_End.Value));
                foreach (DataRow item in dt.Tables[0].Rows)
                {
                    double Per_Error = Convert.ToDouble(item[4].ToString()) / Convert.ToDouble(item[3].ToString()) * 100;
                    dt_QC_report.Rows.Add(item[6], item[1], item[2], item[3], item[4], Per_Error.ToString("0.00") + "%", item[5]);
                }
                dataGridViewX1.DataSource = dt_QC_report;
            }
            if (Cmb_ReportType.Text == "检验清单报表")
            {
                // Sel_Fqc_ErrorInfo
                Dt_QC_info.Clear();
                DataSet dt = FrmBLL.ReleaseData.arrByteToDataSet(refWebtFQC.Instance.Sel_Fqc_ErrorInfo(Txt_UserId.Text, "", DtTime_Sta.Value, DtTime_End.Value));

                foreach (DataRow item in dt.Tables[0].Rows)
                {
                    Dt_QC_info.Rows.Add(
                            item["In_station_time"],
                            item["QC_No"],
                            item["Wo_id"],
                            item["Pallet_No"],
                            item["Tray_no"],
                            item["Carton_id"],
                            item["Esn"],
                            Convert.ToBoolean(int.Parse(item["Is_Pass"].ToString())) ? "良品" : "不良品",
                            item["Error_Code"],
                            item["REMARK"],
                            item["UserID"]
                        );
                }
                dataGridViewX1.DataSource = Dt_QC_info;
                // dataGridViewX1.DataSource = dt.Tables[0];
            }
        }

        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            FrmBLL.DataGridViewToExcel.DataToExcel(dataGridViewX1);
        }

        private void Btn_RqcInfo_Click(object sender, EventArgs e)
        {
            //查询所有需要再次QC的信息
            DataSet dt = FrmBLL.ReleaseData.arrByteToDataSet(refWebtFQC.Instance.Sel_RcheckInfo());
            Dg_RecInfo.DataSource = dt.Tables[0];
        }

        private void Btn_Input_Click(object sender, EventArgs e)
        {
            DataTable _datatable = FrmBLL.publicfuntion.getNewTable(this.Dg_RecInfo.DataSource as DataTable, "CK=1");

            foreach (DataRow item in _datatable.Rows)
            {
                refWebtFQC.Instance.inser_report(item["QC_NO"].ToString(), item["QC_WO_ID"].ToString(), int.Parse(item["PRO_COUNT"].ToString()), int.Parse(item["QC_COUNT"].ToString()), int.Parse(item["ERROR_COUNT"].ToString()), mFrm.gUserInfo.userId, DateTime.Now, int.Parse(item["R_CHECKDATE"].ToString()), item["PALLETNUMBER"].ToString(), int.Parse(item["CHECK_NUMBER"].ToString()) + 1);

            }
            Btn_RqcInfo_Click(null, null);
        }


    }
}
