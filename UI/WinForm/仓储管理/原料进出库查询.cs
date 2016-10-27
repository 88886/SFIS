using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class FrmWhMaterial : Office2007Form // Form
    {
        public FrmWhMaterial(MainParent Msg)
        {
            InitializeComponent();
            sMsg = Msg;
        }
        MainParent sMsg;

        //RefWebService_BLL.refWeb_tPartStorehousehad.tPartStorehousehad materilRec = new RefWebService_BLL.refWeb_tPartStorehousehad.tPartStorehousehad();

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FrmWhMaterial_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.sMsg.gUserInfo.rolecaption == "系统开发员")
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

            startdate.Text = DateTime.Now.ToString();
            enddate.Text = DateTime.Now.ToString();
            starttime.Items.Add("ALL");
            endtime.Items.Add("ALL");
            for (int i = 0; i <= 23; i++)
            {
                // starttime.Items.Add(i.ToString().PadLeft(2,'0') + "00");
                ///  endtime.Items.Add(i.ToString().PadLeft(2,'0')+"00");
                starttime.Items.Add(i.ToString().PadLeft(2, '0') + "30");
                endtime.Items.Add(i.ToString().PadLeft(2, '0') + "30");

            }
            startdate.Enabled = false;
            enddate.Enabled = false;
            starttime.Enabled = false;
            endtime.Enabled = false;
            cbclass.Enabled = false;

            starttime.Text = "ALL";
            endtime.Text = "ALL";
            cbclass.Text = "ALL";


        }

        private void chkdate_Click(object sender, EventArgs e)
        {
            if (chkdate.Checked)
            {
                startdate.Enabled = true;
                enddate.Enabled = true;
                starttime.Enabled = true;
                endtime.Enabled = true;
                cbclass.Enabled = true;
            }
            else
            {
                startdate.Enabled = false;
                enddate.Enabled = false;
                starttime.Enabled = false;
                endtime.Enabled = false;
                cbclass.Enabled = false;
            }
        }

        private delegate void QyrInputOutput(string Flag, bool ShowPn, bool ShowDc, bool ShowVc, bool ShowLc, bool ShowWorkDate, string PN, string DC, string VC, string LC, string Sdate, string Edate, string Stime, string Etime, string Class);
        QyrInputOutput MaterialQuery;

        private delegate void QyrMaterilStocks(bool ShowLoc, bool ShowPN, bool ShowVC, bool ShowDC, bool ShowLC, string PN, string DC, string VC, string LC);
        QyrMaterilStocks MaterilStocks;


        private delegate void QuqeryTrsn(string Trsn);
        QuqeryTrsn QryTrSN;

        private delegate void QueryrTrsnDetail(string Trsn);
        QueryrTrsnDetail QyTrSnDetail;

        private void buttonX1_Click(object sender, EventArgs e)
        {
            bt_query_Click(null, null);
            bt_MaterialStocks_Click(null, null);
            if (QryTrsn)
                imbt_refresh_Click(null, null);
        }
        private void bt_query_Click(object sender, EventArgs e)
        {
            string DateFlag = "0";
            dataGridView1.DataSource = null;

            if (chkdate.Checked)
            {
                if (starttime.Text != "ALL" & endtime.Text != "ALL")
                {
                    if (startdate.Text.Trim() == enddate.Text.Trim())
                    {
                        if (Convert.ToInt32(starttime.Text) >= Convert.ToInt32(endtime.Text))
                        {
                            sMsg.ShowPrgMsg("同一天查询时,开始时间不能大于结束时间或开始时间等于结束时间", MainParent.MsgType.Error);
                            return;
                        }

                    }
                }

                if (startdate.Value.Date > enddate.Value.Date)
                {
                    sMsg.ShowPrgMsg("开始日期不能大于结束日期", MainParent.MsgType.Error);
                    return;
                }

                DateFlag = "1";


            }
            else
            {
                if (string.IsNullOrEmpty(tb_pn.Text))
                {
                    sMsg.ShowPrgMsg("料号不能为空", MainParent.MsgType.Error);
                    return;
                }
            }


            string sStartDate = startdate.Value.ToString("yyyyMMdd");
            string sEndDate = enddate.Value.ToString("yyyyMMdd");

            MaterialQuery = new QyrInputOutput(MaterialInputOutputQuery);
            MaterialQuery.BeginInvoke(DateFlag, tolShowPN.Checked, tolShowdc.Checked, tolShowvc.Checked, tolShowlc.Checked, tolShowworkdate.Checked, tb_pn.Text.Trim(), tb_dc.Text.Trim(), tb_vc.Text.Trim(), tb_lc.Text.Trim(),
               sStartDate, sEndDate, starttime.Text, endtime.Text, cbclass.Text, null, null);

        }

        private void MaterialInputOutputQuery(string Flag, bool ShowPn, bool ShowDc, bool ShowVc, bool ShowLc, bool ShowWorkDate, string PN, string DC, string VC, string LC, string Sdate, string Edate, string Stime, string Etime, string Class)
        {
            dataGridView1.Invoke(new EventHandler(delegate
            {
                dataGridView1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.QueryMaterialInputOutQTY(Flag, new WebServices.tPartStorehousehad.tPartStorehousehadRecount()
                      {
                          kpnumber = PN,
                          datecode = DC,
                          vendercode = VC,
                          lotid = LC,
                          StartDate = Sdate,
                          EndDate = Edate,
                          StartTime = Stime,
                          EndTime = Etime,
                          sclass = Class,
                          showdc = ShowDc,
                          showpn = ShowPn,
                          showlc = ShowLc,
                          showvc = ShowVc,
                          showworkdate = ShowWorkDate

                      }));
            }));
        }

        private void starttime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (starttime.Text == "ALL")
            {
                endtime.Text = "ALL";
            }
            else
            {
                endtime.Text = endtime.Items[1].ToString();
            }
        }

        private void endtime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (endtime.Text == "ALL")
            {
                starttime.Text = "ALL";
            }
            else
            {
                starttime.Text = starttime.Items[1].ToString();
            }
        }

        private void 库存查询_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }
        bool QryTrsn = false;
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelEx1.Enabled = true;
            tb_pn.Location = new Point(99, 21);
            tb_pn.Size = new Size(200, 21);
            label1.Text = "料号[PN]:";
            label1.Location = new Point(34, 24);
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            tb_dc.Visible = true;
            tb_lc.Visible = true;
            tb_vc.Visible = true;
            tb_pn.Text = "";
            QryTrsn = false;
            switch (this.tabControl1.SelectedIndex)
            {

                case 0:
                    panelEx1.Enabled = true;
                    panelEx2.Enabled = true;
                    tb_pn.Focus();
                    tb_pn.SelectAll();
                    break;
                case 1:
                    panelEx2.Enabled = true;
                    panelEx1.Enabled = false;
                    break;
                case 2:
                    panelEx2.Enabled = true;
                    label1.Text = "输入唯一序列号:";
                    panelEx1.Enabled = false;
                    tb_pn.Focus();
                    tb_pn.SelectAll();
                    tb_pn.Location = new Point(154, 32);
                    tb_pn.Size = new Size(300, 25);
                    label1.Location = new Point(48, 35);
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    tb_dc.Visible = false;
                    tb_lc.Visible = false;
                    tb_vc.Visible = false;
                    QryTrsn = true;
                    break;
                case 3:
                    panelEx1.Enabled = false;
                    panelEx2.Enabled = false;
                    ds = new delegateShow(datagridviewshow);
                    ds.BeginInvoke(null, null);
                    break;

            }

        }

        private void bt_MaterialStocks_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;

            if (!string.IsNullOrEmpty(tb_pn.Text))
            {
                MaterilStocks = new QyrMaterilStocks(QueryMaterilStocks);
                MaterilStocks.BeginInvoke(showstocksloc.Checked, Showstockpn.Checked, showstockvc.Checked, showstockdc.Checked, showstocklc.Checked, tb_pn.Text.Trim(), tb_dc.Text.Trim(), tb_vc.Text, tb_lc.Text, null, null);
            }
            else
            {
                sMsg.ShowPrgMsg("库存查询时,料号不能为空", MainParent.MsgType.Error);
                return;
            }
        }

        private void QueryMaterilStocks(bool _ShowLoc, bool _ShowPN, bool _ShowVC, bool _ShowDC, bool _ShowLC, string PN, string DC, string VC, string LC)
        {

            dataGridView2.Invoke(new EventHandler(delegate
                {
                    dataGridView2.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.QueryMaterialStocks(new WebServices.tPartStorehousehad.tPartStorehousehad1()
                        {
                            KpNumber = PN,
                            DateCode = DC,
                            VenderCode = VC,
                            LotId = LC,
                            ShowLoc = _ShowLoc,
                            ShowPN = _ShowPN,
                            ShowDC = _ShowDC,
                            ShowLC = _ShowLC,
                            ShowVC = _ShowVC

                        }));
                }));


        }

        private void ShowPN_Click(object sender, EventArgs e)
        {
            tolShowPN.Checked = true;
        }

        private void Showvc_Click(object sender, EventArgs e)
        {
            if (tolShowvc.Checked)
                tolShowvc.Checked = false;
            else
                tolShowvc.Checked = true;
        }

        private void Showdc_Click(object sender, EventArgs e)
        {
            if (tolShowdc.Checked)
                tolShowdc.Checked = false;
            else
                tolShowdc.Checked = true;
        }

        private void Showlc_Click(object sender, EventArgs e)
        {
            if (tolShowlc.Checked)
                tolShowlc.Checked = false;
            else
                tolShowlc.Checked = true;
        }

        private void Showworkdate_Click(object sender, EventArgs e)
        {
            if (tolShowworkdate.Checked)
                tolShowworkdate.Checked = false;
            else
                tolShowworkdate.Checked = true;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }

        private void imbt_refresh_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_pn.Text))
            {
                QryTrSN = new QuqeryTrsn(QueryTrsnData);
                QryTrSN.BeginInvoke(tb_pn.Text.Trim(), null, null);

                QyTrSnDetail = new QueryrTrsnDetail(QueryTrsnDetailData);
                QyTrSnDetail.BeginInvoke(tb_pn.Text.Trim(), null, null);
                tb_pn.SelectAll();
            }
            else
            {
                sMsg.ShowPrgMsg("唯一序列号不能为空", MainParent.MsgType.Error);
                tb_pn.SelectAll();
            }
        }

        private void QueryTrsnData(string trsn)
        {
            dgvtrsn.Invoke(new EventHandler(delegate
                {
                    dgvtrsn.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.QueryTrSn(trsn));
                }));
        }

        private void QueryTrsnDetailData(string trsn)
        {
            dgvtrsndetail.Invoke(new EventHandler(delegate
            {
                dgvtrsndetail.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.QueryTrsnDetail(trsn));
            }));
        }

        private void Showstockpn_Click(object sender, EventArgs e)
        {
            Showstockpn.Checked = true;
        }

        private void showstockvc_Click(object sender, EventArgs e)
        {
            if (showstockvc.Checked)
                showstockvc.Checked = false;
            else
                showstockvc.Checked = true;
        }

        private void showstockdc_Click(object sender, EventArgs e)
        {
            if (showstockdc.Checked)
                showstockdc.Checked = false;
            else
                showstockdc.Checked = true;
        }

        private void showstocklc_Click(object sender, EventArgs e)
        {
            if (showstocklc.Checked)
                showstocklc.Checked = false;
            else
                showstocklc.Checked = true;
        }

        private void showstocksloc_Click(object sender, EventArgs e)
        {
            if (showstocksloc.Checked)
                showstocksloc.Checked = false;
            else
                showstocksloc.Checked = true;
        }

        private void to_excel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount != 0)
                DataToExcel(dataGridView1);
            else
                sMsg.ShowPrgMsg("没有资料可以导出Excel", MainParent.MsgType.Error);
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
                MessageBox.Show(this, "保存EXCEL成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sMsg.ShowPrgMsg("保存EXCEL成功 ", MainParent.MsgType.Incoming);
            }
        }

        private void ToExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView2.RowCount != 0)
                DataToExcel(dataGridView2);
            else
                sMsg.ShowPrgMsg("没有资料可以导出Excel", MainParent.MsgType.Error);
        }


        #region 库存报表
        private delegate void delegateShow();
        delegateShow ds;

        private void datagridviewshow()
        {
            this.dataGridViewX2.Invoke(new EventHandler(delegate
            {
                dataGridViewX2.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPartStorehousehad.Instance.GetAllMaterialStocks());
            }));
        }
        private void FrmStores_Load(object sender, EventArgs e)
        {
            ds = new delegateShow(datagridviewshow);
            ds.BeginInvoke(null, null);
        }

        private void bt_refresh_Click(object sender, EventArgs e)
        {
            ds = new delegateShow(datagridviewshow);
            ds.BeginInvoke(null, null);
        }


        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (this.chkdate.Checked == true)
            //{
            //    FrmWareReport fwr = new FrmWareReport(this);
            //    fwr.ShowDialog();

            //    //FrmStores fs = new FrmStores(sMsg);
            //    //fs.ShowDialog();

            //}
        }

        private void bt_toexcel_Click(object sender, EventArgs e)
        {
            if (dataGridViewX2.RowCount != 0)
                DataToExcel(dataGridViewX2);
            else
                sMsg.ShowPrgMsg("没有资料可以导出Excel", MainParent.MsgType.Error);
        }

        private void dataGridViewX2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MaterialsStorageInfo msi = new MaterialsStorageInfo(dataGridViewX2["KpNumber", e.RowIndex].Value.ToString());
            msi.ShowDialog();
        }
        #endregion
       
    }
}
