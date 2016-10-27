using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;

namespace SFIS_V2
{
    public partial class ChkKpMarster :Office2007Form// Form
    {
        public ChkKpMarster(MainParent frm)
        {
            InitializeComponent();
            this.mFrm = frm;
        }
        MainParent mFrm;
        private ComboBox cbo = new ComboBox();
        private List<string> lskpstatus = new List<string>() { "待审核", "审核通过", "审核失败" };

        private void ChkKpMarster_Load(object sender, EventArgs e)
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
                FrmBLL.publicfuntion.AddProgInfo(dic,lsfunls);
                }
                #endregion

                this.ShowKpMaster(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebSmtKpMaster.Instance.GetSmtKpMasterNotChecked()));
                cbo.Visible = false;
                cbo.DataSource = lskpstatus;
                if (this.dgv_showkpmaster.Rows.Count > 0)
                {
                    this.cbo.Validated += new EventHandler(cbo_Validated);
                    this.dgv_showkpmaster.Controls.Add(cbo);
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void cbo_Validated(object sender, EventArgs e)
        {
            try
            {
                this.dgv_showkpmaster.CurrentCell.Value = ((ComboBox)sender).Text == "选择.." ? string.Empty : ((ComboBox)sender).Text;
                Color clr = Color.White;
                switch (cbo.Text.Trim())
                {
                    case "待审核":
                        clr = Color.FromArgb(250,250,160);
                        break ;
                    case "审核通过":
                        clr = Color.FromArgb(61,170,49);
                        break ;
                    case "审核失败":
                        clr = Color.FromArgb(255,142,142);
                        break;
                    default:
                        break;

                }
                this.dgv_showkpmaster.Rows[this.dgv_showkpmaster.CurrentCell.RowIndex].DefaultCellStyle.BackColor = clr;
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void ShowKpMaster(DataTable dt)
        {
            this.dgv_showkpmaster.Invoke(new EventHandler(delegate
            {
                this.dgv_showkpmaster.DataSource = dt;
            }));
        }
        private void ShowKpDetalt(DataTable dt)
        {
            this.dgv_showkpdetalt.Invoke(new EventHandler(delegate
            {
                if (dt == null || dt.Rows.Count < 1)
                {
                    for (int i = 0; i < this.dgv_showkpdetalt.Rows.Count; i++)
                    {
                        this.dgv_showkpdetalt.Rows.RemoveAt(i);
                    }
                }
                else
                {
                    this.dgv_showkpdetalt.DataSource = dt;
                }
            }));
        }
        private DataGridViewRow RecDr = null;
        private void dgv_showkpmaster_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                RecDr = this.dgv_showkpmaster.Rows[e.RowIndex];
                this.ShowKpDetalt(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebSmtKpMaster.Instance.GetKpDetalt(this.dgv_showkpmaster["masterId", e.RowIndex].Value.ToString())));
            }
        }

        private void dgv_showkpmaster_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dgv_showkpmaster.CurrentCell == null)
                    return;
                if (this.dgv_showkpmaster.CurrentCell.ColumnIndex == this.dgv_showkpmaster.Columns["RESERVE2"].Index)
                {
                    Rectangle rect =
                        dgv_showkpmaster.GetCellDisplayRectangle(dgv_showkpmaster.CurrentCell.ColumnIndex,
                        dgv_showkpmaster.CurrentCell.RowIndex, false);
                   // DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtStorehouseManage.Instance.GetAllWarehouseId());
                    cbo.DataSource = lskpstatus;
                    cbo.DisplayMember = "RESERVE2";
                    cbo.Left = rect.Left;
                    cbo.Top = rect.Top;
                    cbo.Width = rect.Width;
                    cbo.Height = rect.Height;
                    cbo.Visible = true;
                    cbo.Text = this.dgv_showkpmaster.CurrentCell.Value.ToString();
                    cbo.SelectAll();
                    cbo.Focus();
                }
                else
                {
                    cbo.Visible = false;
                }
                cbo.Items.Add("");
            }
            catch
            {
            }
        }

        private void dgv_showkpmaster_Scroll(object sender, ScrollEventArgs e)
        {
            this.cbo.Visible = false;
        }

        private void dgv_showkpmaster_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.cbo.Visible = false;
        }

        private void dgv_showkpmaster_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= dgv_showkpmaster.Rows.Count)
                return;
            DataGridViewRow dgr = dgv_showkpmaster.Rows[e.RowIndex];
            try
            {
                if (dgr.Cells["RESERVE2"].Value.ToString() == "待审核")
                {
                    this.dgv_showkpmaster.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 160);
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_viewkpmaster_Click(object sender, EventArgs e)
        {
            if (this.dgv_showkpmaster.SelectedRows.Count > 0)
            {
                this.ShowKpDetalt(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebSmtKpMaster.Instance.GetKpDetalt(this.dgv_showkpmaster["masterId", this.dgv_showkpmaster.SelectedRows[0].Index].Value.ToString())));
            }
        }

        private void bt_submit_Click(object sender, EventArgs e)
        {
            
            if (this.dgv_showkpmaster.Rows.Count<1 ||this.dgv_showkpmaster.Rows.Count == FrmBLL.publicfuntion.getNewTable(this.dgv_showkpmaster.DataSource as DataTable, "reserve2='待审核'").Rows.Count)
            {
                MessageBoxEx.Show("没有需要审核的项目 \n或 请选择审核状态", "提示",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                return;
            }
            for (int i = 0; i < this.dgv_showkpmaster.Rows.Count; i++)
            {
                string status = string.Empty;
                switch (this.dgv_showkpmaster["RESERVE2", i].Value.ToString())
                {
                    case "审核通过":
                        status = "1";
                        break;
                    case "审核失败":
                        status = "2";
                        break;
                    default:
                        status = string.Empty;
                        break;
                }
                if (!string.IsNullOrEmpty(status))
                {

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("RESERVE2", status);
                    dic.Add("Auditinguser".ToUpper(), this.mFrm.gUserInfo.userId);
                    dic.Add("MASTERID", this.dgv_showkpmaster["MASTERID", i].Value.ToString());
                    RefWebService_BLL.refWebSmtKpMaster.Instance.UpdateAuditingStatus(FrmBLL.ReleaseData.DictionaryToJson(dic));
                    //RefWebService_BLL.refWebSmtKpMaster.Instance.UpdateAuditingStatus(new WebServices.tSmtKpMaster.tSmtKPMasterTable()
                    //{
                    //    Auditinguser = this.mFrm.gUserInfo.userId,
                    //    Reserve2 = status,
                    //    MasterId = this.dgv_showkpmaster["MASTERID", i].Value.ToString()
                    //});
                }
            }

            ChkKpMarster_Load(null, null);
        }

        private void bt_printer_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.RecDr == null)
                {
                    this.mFrm.ShowPrgMsg("请双击需要打印的料表编号", MainParent.MsgType.Warning);
                    return;
                }
                if (!File.Exists(string.Format("{0}Excel\\KpMaster.xlt", System.AppDomain.CurrentDomain.BaseDirectory)))
                {
                    FrmBLL.Ftp_MyFtp ftp = new FrmBLL.Ftp_MyFtp();
                    ftp.DownloadFile("KpMaster.xlt", System.AppDomain.CurrentDomain.BaseDirectory + "Excel", "KpMaster.xlt");
                }
                string FileNameTemp = string.Format("{0}Excel\\{1}_{2}_{3}_{4}.xls",
                                                        System.AppDomain.CurrentDomain.BaseDirectory,
                                                        this.RecDr.Cells["masterId"].Value.ToString(),
                                                        this.RecDr.Cells["partnumber"].Value.ToString(),
                                                        this.RecDr.Cells["lineId"].Value.ToString(),
                                                        this.RecDr.Cells["pcbside"].Value.ToString());
                FrmBLL.ClsAllExcel excel = new FrmBLL.ClsAllExcel();

                excel.OpenFileName = string.Format("{0}Excel\\KpMaster.xlt", System.AppDomain.CurrentDomain.BaseDirectory);
                excel.OpenExcelFile();
                excel.setOneCellValue(1, 1, this.RecDr.Cells["reserve2"].Value.ToString() != "审核通过" ? "SMT料站表--待审核" : "SMT料站表");
                excel.setOneCellValue(2, 2, this.RecDr.Cells["masterId"].Value.ToString());//料表编号
                excel.setOneCellValue(3, 2, this.RecDr.Cells["lineId"].Value.ToString());//机器编号
                excel.setOneCellValue(4, 2, this.RecDr.Cells["partnumber"].Value.ToString());//产品料号
                excel.setOneCellValue(5, 2, this.RecDr.Cells["bomver"].Value.ToString());//BOM版本
                excel.setOneCellValue(5, 7, this.RecDr.Cells["reserve1"].Value.ToString());//SMT程式
                excel.setOneCellValue(4, 4, this.RecDr.Cells["modelname"].Value.ToString());//产品型号
                excel.setOneCellValue(5, 4, this.RecDr.Cells["pcbside"].Value.ToString());//PCB面

                for (int i = 0; i < this.dgv_showkpdetalt.Rows.Count - 1; i++)
                {
                    excel.setOneCellValue(8 + i, 1, this.dgv_showkpdetalt["stationno", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 2, this.dgv_showkpdetalt["kpnumber", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 3, this.dgv_showkpdetalt["kpdesc", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 4, this.dgv_showkpdetalt["kpdistinct", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 5, this.dgv_showkpdetalt["replacegroup", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 6, this.dgv_showkpdetalt["priorityclass", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 7, this.dgv_showkpdetalt["loction", i].Value.ToString());
                    excel.setOneCellValue(8 + i, 8, this.dgv_showkpdetalt["_reserve", i].Value.ToString());
                }
                excel.CellsDrawFrame(8, 1, this.dgv_showkpdetalt.Rows.Count - 2 + 8, 8, true, true,
                    true, true, true, true, false, false,
                    Excel.XlLineStyle.xlDashDot,
                    Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexNone);

                    this.mFrm.ShowPrgMsg("正在导出数据", MainParent.MsgType.Incoming);
                    excel.SaveFileName = FileNameTemp;
                    bool isView = false;
                    if (MessageBoxEx.Show("是否预览文件?\n是 请选择[Yes] 否则请选择[NO]", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        isView = true;
                    excel.SaveAsExcel(isView);
                    excel.CloseExcelApplication();
                    this.mFrm.ShowPrgMsg("数据导出完成", MainParent.MsgType.Incoming);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
    }
}
