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
    public partial class FrmQueryTargetPlan : Office2007Form //Form
    {
        public FrmQueryTargetPlan(MainParent sInfo)
        {
            InitializeComponent();
            sMain = sInfo;
        }
        MainParent sMain;
        private void FrmTargetPlan_Load(object sender, EventArgs e)
        {
            try
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
                    FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
                }
                #endregion

            }
            catch (Exception ex)
            {
                this.sMain.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

            GetCraftInfo();
            DateTimeNow.Interval = 1000;
            DateTimeNow.Enabled = true;
            DateTimeNow.Tick += new EventHandler(DateTimeNow_Tick);

            TimeAutoRefresh.Interval = 60000;
            TimeAutoRefresh.Enabled = false;
            TimeAutoRefresh.Tick+=new EventHandler(TimeAutoRefresh_Tick);


            RefreshTime.Interval = 30000;
            RefreshTime.Enabled = true;
            RefreshTime.Tick += new EventHandler(RefreshTime_Tick);

        }
        /// <summary>
        /// 刷新标记
        /// </summary>
        bool _Refresh = true;
        private void RefreshTime_Tick(object sender, EventArgs e)
        {
            label5.BackColor = Color.Green;
            _Refresh = true;
            RefreshTime.Enabled = false;
        }

        private void DateTimeNow_Tick(object sender,EventArgs e)
        {
            LabTime.Text = DateTime.Now.ToString();
        }
        int x = 0;
        private void TimeAutoRefresh_Tick(object sender, EventArgs e)
        {
            if (x <= 0)
            {
                btrefresh_Click(null, null);
                x = Convert.ToInt32(nudMin.Value);
            }
            else
                x = x - 1;
        }

        System.Windows.Forms.Timer DateTimeNow = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TimeAutoRefresh = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer RefreshTime = new System.Windows.Forms.Timer();



        string LineSelect = string.Empty;
        private void btrefresh_Click(object sender, EventArgs e)
        {
            if (_Refresh)
            {
                label5.BackColor = Color.Red;
                _Refresh = false;
                RefreshTime.Enabled = true;
            }
            else
            {
                MessageBox.Show("距离上次刷新间隔时间需等待30秒", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //string Err = "";
            //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtTargetPlan.Instance.GetTargetPlan("", "0", cbcraftId.Text, out Err));
            DataTable dt = Getresult("");
            //UpdateColumnName(dt);
          //  dt.Columns[dt.Columns.Count - 1].;

            DgvTarget.DataSource = dt;
            DgvTarget.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            refreshdgvFont(DgvTarget, 0);

            if (!string.IsNullOrEmpty(LineSelect))
            {
                //DataTable dtLine = FrmBLL.ReleaseData.arrByteToDataTable(refWebtTargetPlan.Instance.GetTargetPlan(LineSelect, "1", "", out Err));
                //UpdateColumnName(dtLine);
                DataTable dtLine = Getresult(LineSelect);
                DgvTargetLine.DataSource = dtLine;
                DgvTargetLine.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                refreshdgvFont(DgvTargetLine, 1);
            }

           // System.Threading.Thread.Sleep(10000);

        }

        private DataTable Getresult(String liness)
        {
            String getdate = String.Empty;
            String ctype = String.Empty;
            if (510 < System.DateTime.Now.Hour * 60 + System.DateTime.Now.Minute & System.DateTime.Now.Hour * 60 + System.DateTime.Now.Minute < 1230)
            {
                getdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString("yyyyMMdd");
                ctype = "D";
            }
            else
            {
                getdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour - 12, DateTime.Now.Minute, DateTime.Now.Second).ToString("yyyyMMdd");
                ctype = "N";
            }

            DataTable dt1 = FrmBLL.ReleaseData.arrByteToDataTable(refWebtTargetPlan.Instance.GetTargetPlan1(liness, getdate, ctype));
            DataTable dt2 = FrmBLL.ReleaseData.arrByteToDataTable(refWebtTargetPlan.Instance.GetTargetPlan2(liness, getdate, ctype));
            DataTable dt3 = FrmBLL.ReleaseData.arrByteToDataTable(refWebtTargetPlan.Instance.GetTargetPlan3(liness, getdate, ctype));
            foreach (DataRow dr1 in dt1.Rows)
            {
                foreach (DataRow dr2 in dt2.Rows)
                {
                    if (dr2["woId"].ToString() == dr1["woId"].ToString() && dr2["craftid"].ToString() == dr1["Throughout"].ToString() && dr2["lineId"].ToString() == dr1["line"].ToString())
                    {
                        int fir = 0;
                        int sec = 1;
                        String stion = String.Empty;
                        if (int.Parse(System.DateTime.Now.ToString("t").ToString().Substring(3, 2)) > 30)
                        {
                            stion = (int.Parse(System.DateTime.Now.ToString("t").ToString().Substring(0, 2)) + 1).ToString();
                        }
                        else
                        {
                            stion = (int.Parse(System.DateTime.Now.ToString("t").ToString().Substring(0, 2))).ToString();
                        }

                        int a1 = int.Parse(dr1["StartTime"].ToString().Substring(0, 2)) * 60 + int.Parse(dr1["StartTime"].ToString().Substring(3, 2));
                        int a2 = int.Parse(System.DateTime.Now.ToString("t").ToString().Substring(0, 2)) * 60 + int.Parse(System.DateTime.Now.ToString("t").ToString().Substring(3, 2));
                        int a3 = int.Parse(dr1["EndTime"].ToString().Substring(0, 2)) * 60 + int.Parse(dr1["EndTime"].ToString().Substring(3, 2));
                        if (ctype == "N")
                        {
                            if (int.Parse(dr1["StartTime"].ToString().Substring(0, 2)) < 12)
                            {
                                a1 += 1440;
                            }

                            if (int.Parse(dr1["EndTime"].ToString().Substring(0, 2)) < 12)
                            {
                                a3 += 1440;
                            }

                            if (int.Parse(System.DateTime.Now.ToString("t").ToString().Substring(0, 2)) < 12)
                            {
                                a2 += 1440;
                            }
                        }
                        fir = a2 - a1;
                        sec = a3 - a1;
                        if (a2 > a1 && a2 < a3)
                        {
                            dr1["Plan_Out"] = int.Parse(dr1["Target_Qty"].ToString()) * fir / sec;
                            dr1["Actual"] = int.Parse(dr2["passQty"].ToString()) + int.Parse(dr2["failQty"].ToString());
                            dr1["Hit_Rate"] = ((int.Parse(dr2["passQty"].ToString()) + int.Parse(dr2["failQty"].ToString())) / (int.Parse(dr1["Target_Qty"].ToString()) * fir / sec) * 100).ToString() + "%";
                            if (int.Parse(dr2["passQty"].ToString()) + int.Parse(dr2["failQty"].ToString()) > 0)
                            {
                                foreach (DataRow dr3 in dt3.Rows)
                                {
                                    if (dr3["woId"].ToString() == dr1["woId"].ToString() && dr3["lineId"].ToString() == dr1["line"].ToString() && dr3["craftid"].ToString() == cbcraft.Text && dr3["worksection"].ToString() == stion)
                                    {
                                        if (int.Parse(dr3["passQty"].ToString()) + int.Parse(dr3["failQty"].ToString()) > 0)
                                        {
                                            dr1["Yield_Rate"] = (int.Parse(dr3["passQty"].ToString()) * 100 / (int.Parse(dr3["passQty"].ToString()) + int.Parse(dr3["failQty"].ToString()))).ToString() + "%";

                                        }
                                    }
                                }
                            }
                        }
                        else if (a2 >= a3)
                        {
                            dr1["Plan_Out"] = int.Parse(dr1["Target_Qty"].ToString());
                            dr1["Actual"] = int.Parse(dr2["passQty"].ToString()) + int.Parse(dr2["failQty"].ToString());
                            dr1["Hit_Rate"] = ((int.Parse(dr2["passQty"].ToString()) + int.Parse(dr2["failQty"].ToString())) * 100 / (int.Parse(dr1["Target_Qty"].ToString()))).ToString() + "%";
                            if (int.Parse(dr2["passQty"].ToString()) + int.Parse(dr2["failQty"].ToString()) > 0)
                            {
                                foreach (DataRow dr3 in dt3.Rows)
                                {
                                    if (dr3["woId"].ToString() == dr1["woId"].ToString() && dr3["lineId"].ToString() == dr1["line"].ToString() && dr3["craftid"].ToString() == cbcraft.Text && dr3["worksection"].ToString() == stion)
                                    {
                                        if (int.Parse(dr3["passQty"].ToString()) + int.Parse(dr3["failQty"].ToString()) > 0)
                                        {
                                            dr1["Yield_Rate"] = (int.Parse(dr3["passQty"].ToString()) * 100 / (int.Parse(dr3["passQty"].ToString()) + int.Parse(dr3["failQty"].ToString()))).ToString() + "%";

                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            return dt1;
        }



        private void DgvTarget_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //string Err = "";
                LineSelect = DgvTarget[0, e.RowIndex].Value.ToString();
                //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtTargetPlan.Instance.GetTargetPlan(LineSelect, "1", "", out Err));
                DataTable dt = Getresult(LineSelect);
                UpdateColumnName(dt);
                DgvTargetLine.DataSource = dt;
                DgvTargetLine.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                refreshdgvFont(DgvTargetLine, 1);
            }

        }

        DataTable dtCraft = null;
        private void GetCraftInfo()
        {
            dtCraft = FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo());
            DataTable dtst=dtCraft;
            dtst.DefaultView.Sort="craftname";
            DataTable dts = dtst.DefaultView.ToTable();
            foreach (DataRow dr in dts.Rows)
            {
                if ((dr[1].ToString().Substring(0, 2) != "R_") && (dr[1].ToString().Substring(0, 2) != "R-"))
                {
                    cbcraft.Items.Add(dr[1].ToString());
                    cbcraftId.Items.Add(dr[0].ToString());
                }

            }
            cbcraft.SelectedIndex = 0;
        }
        private void UpdateColumnName(DataTable Udt)
        {
            for (int i = 0; i < Udt.Rows.Count; i++)
            {
                try
                {
                    Udt.Rows[i][1] = FrmBLL.publicfuntion.getNewTable(dtCraft, string.Format("CraftId='{0}'", Udt.Rows[i][1].ToString())).Rows[0][1].ToString();
                }
                catch
                {
                }

            }
        }

        private void cbcraft_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbcraftId.SelectedIndex = cbcraft.SelectedIndex;
        }

        private void DgvTarget_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {


        }

        private void refreshdgvFont(DataGridView dgv, int Flag)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (Flag == 0)
                {
                    //  if (Convert.ToInt32(DgvTarget[DgvTarget.ColumnCount - 1, i].Value.ToString().Substring(0, DgvTarget[DgvTarget.ColumnCount - 1, i].Value.ToString().Length - 1)) < 98)
                    if ((Convert.ToDouble(dgv[dgv.ColumnCount - 4, i].Value.ToString().Split('%')[0]) < Convert.ToInt32(tb_hit.Text)) && (Convert.ToDouble(dgv[dgv.ColumnCount - 4, i].Value.ToString().Split('%')[0]) > 0))//达成率
                    {
                        dgv.Rows[i].DefaultCellStyle.Font = new Font("宋体", 10.0F, FontStyle.Bold);
                        dgv.Rows[i].DefaultCellStyle.Font = new Font("宋体", 10.0F, FontStyle.Italic);

                    }
                    else
                    {
                        dgv.Rows[i].DefaultCellStyle.Font = new Font("宋体", 10.0F, FontStyle.Regular);
                    }

                    if ((Convert.ToDouble(dgv[dgv.ColumnCount - 3, i].Value.ToString().Split('%')[0]) < Convert.ToInt32(tb_Yield.Text)) && (Convert.ToDouble(dgv[dgv.ColumnCount - 3, i].Value.ToString().Split('%')[0]) > 0))  //良率
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Aquamarine;
                    }
                    else
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.White; ;
                    }
                }
                else
                {
                    if ((Convert.ToDouble(dgv[dgv.ColumnCount - 3, i].Value.ToString().Split('%')[0]) < Convert.ToInt32(tb_hit.Text)) && (Convert.ToDouble(dgv[dgv.ColumnCount - 3, i].Value.ToString().Split('%')[0]) > 0))//达成率
                    {
                        dgv.Rows[i].DefaultCellStyle.Font = new Font("宋体", 10.0F, FontStyle.Bold);
                        dgv.Rows[i].DefaultCellStyle.Font = new Font("宋体", 10.0F, FontStyle.Italic);

                    }
                    else
                    {
                        dgv.Rows[i].DefaultCellStyle.Font = new Font("宋体", 10.0F, FontStyle.Regular);
                    }
                }
            }


            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void tb_hit_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string hit = Input.InputQuery.ShowInputBox("请输入达成率%", string.Empty);
            if (!string.IsNullOrEmpty(hit))
            {
                tb_hit.Text = hit;
            }
        }

        private void tb_Yield_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string Yield = Input.InputQuery.ShowInputBox("请输入良率%", string.Empty);
            if (!string.IsNullOrEmpty(Yield))
            {
                tb_Yield.Text = Yield;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            x = Convert.ToInt32(nudMin.Value);
            TimeAutoRefresh.Enabled = true;
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            btrefresh.Enabled = false;

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            TimeAutoRefresh.Enabled = false;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btrefresh.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DgvTarget.Rows.Count != 0)
            {
                FrmBLL.DataGridViewToExcel.DataToExcel(DgvTarget);
                MessageBox.Show("汇出Excel完成", "汇出提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("没有资料可以汇出", "汇出提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvTarget_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}