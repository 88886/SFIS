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
    public partial class Frm_StockQuery : Office2007Form// Form
    {
        public Frm_StockQuery(MainParent frm)
        {
            InitializeComponent();
            this.mFrm = frm;
        }
        MainParent mFrm;
        enum SelectConditionEnum
        {
            工单,
            料号,
            栈板,
            卡通,
            库位,
            入库批次,
            出库批次,
      
        }
        enum SelectFlagEnum
        {
            woId,
            partnumber,
            palletnumber,
            cartonnumber,
            locId,
            lotin,
            lotout,
            esn,
            snval
          
        }
        private void Frm_StockQuery_Load(object sender, EventArgs e)
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

              //  txtMth.Text = string.Format("{0:yyyy-MM}", DateTime.Now);
                cb_select.SelectedIndex = 0;

                this.dgvStock.RowsDefaultCellStyle.BackColor = Color.Bisque;
                this.dgvStock.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

                this.dgv_productsninfo.RowsDefaultCellStyle.BackColor = Color.Bisque;
                this.dgv_productsninfo.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

        }



        #region 出入库查询   

   

        //成品出入库报表
        private void to_excel_Click(object sender, EventArgs e)
        {
            if (this.dgv_productsninfo.RowCount == 0)
            {
                MessageBox.Show("没有数据可以导出到Excel"); return;
            }
            else
            {
                //DataToExcel(dgvStockInOut);
                WriteExcel(dgv_productsninfo);
            }
        }
        #endregion

        #region 库存查询
        private void bt_Refesh2_Click(object sender, EventArgs e)
        {
            btQuery2_Click(null, null);
        }


        private void btQuery2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDATA.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(cb_select.Text))
                {
                    string Colnum = "ESN";
                    if (cb_select.SelectedIndex == 0)
                    {
                        Colnum = "ESN";
                    }
                    if (cb_select.SelectedIndex == 1)
                    {
                        Colnum = "CARTONNUMBER";
                    }
                    if (cb_select.SelectedIndex == 2)
                    {
                        Colnum = "PALLETNUMBER";
                    }

                    this.dgvStock.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.QueryZ_WIP_TRACKING(Colnum, txtDATA.Text.Trim()));
                    txtDATA.SelectAll();
                }

               // this.dgvStock.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.StockQuery("NA", "NA", txtpartnumber.Text.Trim(), 2, 0));

            }
        }

        private void stock_toexcel_Click(object sender, EventArgs e)
        {
            if (this.dgvStock.RowCount == 0)
            {
                MessageBox.Show("没有数据可以导出到Excel"); return;
            }
            else
            {
                DataToExcel(dgvStock);
            }
        }
        #endregion

        #region 库存报表
        //private void bt_Query3_Click(object sender, EventArgs e)
        //{
        //    this.dgvStockReport.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWarehouseWipTracking.Instance.StockQuery(txtMth.Text.Trim(), "NA", "NA", 3, 0));
        //}

        //private void bt_refresh_Click(object sender, EventArgs e)
        //{
        //    bt_Query3_Click(null, null);
        //}
        //private void bt_toexcel_Click(object sender, EventArgs e)
        //{
        //    if (this.dgvStockReport.RowCount == 0)
        //    {
        //        MessageBox.Show("没有数据可以导出到Excel"); return;
        //    }
        //    else
        //    {
        //        DataToExcel(dgvStockReport);
        //    }
        //}
        #endregion


        #region 将datagridview 中的数据导出到excel
        private void DataToExcel(DataGridView m_DataView)
        {
            Excel.ApplicationClass oExcel = new Excel.ApplicationClass();
            Excel.Workbooks oBooks = oExcel.Workbooks;

            Excel._Workbook oBook = null;
            oBook = (Excel._Workbook)(oExcel.Workbooks.Add(true));// 引用excel工作薄 

            //xSheet.Columns("A:G").Selection.NumberFormatLocal = ":";//设置A-G列为文本格式
            for (int i = 0; i < m_DataView.Columns.Count; i++)
            {
                if (m_DataView.Columns[i].Visible == true)
                {
                    oExcel.Cells[2, i + 1] = m_DataView.Columns[i].HeaderText.ToString();
                }
            }

            for (int i = 0; i < m_DataView.Rows.Count; i++)
            {
                for (int j = 0; j < m_DataView.Columns.Count; j++)
                {
                    if (m_DataView.Columns[j].Visible == true)
                    {
                        oExcel.Cells[i + 3, j + 1] = m_DataView.Rows[i].Cells[j].Value.ToString();

                    }
                }
            }

            oExcel.Visible = true;
            object Missing = System.Reflection.Missing.Value;
            //  oExcel.Run("Sheet1.printdoc", Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing);

            oBook.Application.DisplayAlerts = false;



        }


        #endregion

        #region 产品信息查询
        private string flag = "";

        private void cb_selectcondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bt_select.Enabled = true;
            switch (this.cb_selectcondition.SelectedIndex)
            {
                case 0:
                    flag = SelectFlagEnum.woId.ToString();
                    break;
                case 1:
                    flag = SelectFlagEnum.palletnumber.ToString();
                    break;
                case 2:
                   flag = SelectFlagEnum.cartonnumber.ToString();
                    break;
                case 3:
                     flag = SelectFlagEnum.lotin.ToString();
                    break;
                case 4:
                      flag = SelectFlagEnum.lotout.ToString();
                    break;
                case 5:
                    flag = SelectFlagEnum.esn.ToString();
                    break;
                case 6:
                    flag = SelectFlagEnum.snval.ToString();
                    break;            
               
            }
        }
        private void bt_select_Click(object sender, EventArgs e)
        {
            try
            {

                dgv_productsninfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                this.bt_select.Enabled = false;
                if (string.IsNullOrEmpty(flag) && string.IsNullOrEmpty(this.tb_selectvalue.Text.Trim()))
                    return;
                if (flag == "woId" || flag == "esn")
                {
                    if (string.IsNullOrEmpty(this.tb_selectvalue.Text.Trim()))
                    {
                        dgv_productsninfo.DataSource = null;
                        return;
                    }
                }

                if (flag.ToUpper() == "SAPCODE")
                {
                    //DataTable dtSapCode = FrmBLL.ReleaseData.arrByteToDataTable(
                    //     refWebtWarehouseWipTracking.Instance.GetProductInfo(
                    //     new WebServices.tWarehouseWipTracking.tWarehouseWipTrackingTable()
                    //     {
                    //         SapCode = this.tb_selectvalue.Text.Trim()
                    //     }));
                    //if (dtSapCode.Rows.Count > 0)
                    //{
                    //    DataView dvs = dtSapCode.DefaultView;
                    //    dvs.Sort = "ESN ASC";
                    //    DataTable dTemp = dvs.ToTable();
                    //    this.dgv_productsninfo.DataSource = FrmBLL.DataTableCrosstab.DataTableCross(dTemp, dTemp.Columns.Count);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("没有数据");
                    //}
                }
                else
                {
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetProductAllInfo(flag, this.tb_selectvalue.Text.Trim()));

                    if (flag.ToUpper() == "SNVAL")
                    {
                        dgv_productsninfo.DataSource = dt;
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataView dv = dt.DefaultView;
                            dv.Sort = "ESN ASC";
                            DataTable dTemp = dv.ToTable();
                            dgv_productsninfo.DataSource = FrmBLL.DataTableCrosstab.DataTableCross(dTemp, dTemp.Columns.Count);
                        }
                        else
                        {
                            MessageBox.Show("没有数据");
                        }
                    }
                }
                this.bt_select.Enabled = true;

            }
            catch (Exception ex)
            {
                this.bt_select.Enabled = true;
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        ///// <summary>
        ///// 处理数据(未使用)
        ///// </summary>
        ///// <param name="_dt"></param>
        ///// <returns></returns>
        //private DataTable GetDealWithData(DataTable _dt)
        //{

        //    DataTable _mdt = _dt.Clone();
        //    DataTable _dtTemp = _dt.DefaultView.ToTable(true, "esn");//筛选出不同esn的行
        //    DataSet mds = new DataSet();
        //    int x = 0;

        //    foreach (DataRow dr in _dtTemp.Rows)
        //    {
        //        Dictionary<string, string> _ser = new Dictionary<string, string>();
        //        DataRow[] arrDr = null;
        //        for (int i = 0; i < _mdt.Columns.Count; i++)
        //        {

        //            string sql = string.Format("esn='{0}' and {1}<>'{2}'", dr["esn"].ToString(), _mdt.Columns[i].ColumnName, "NA");
        //            arrDr = _dt.Select(sql);
        //            if (_mdt.Columns[i].ColumnName.ToUpper() == "ESN" || _mdt.Columns[i].ColumnName.ToUpper() == "WOID"
        //                || _mdt.Columns[i].ColumnName.ToUpper() == "PALLETNUMBER" || _mdt.Columns[i].ColumnName.ToUpper() == "CARTONNUMBER"
        //                || _mdt.Columns[i].ColumnName.ToUpper() == "PARTNUMBER" || _mdt.Columns[i].ColumnName.ToUpper() == "LOTIN"
        //                || _mdt.Columns[i].ColumnName.ToUpper() == "LOCID")
        //            {
        //                _ser.Add(_mdt.Columns[i].ColumnName, arrDr[0][_mdt.Columns[i].ColumnName].ToString());
        //                continue;
        //            }

        //            if (_mdt.Columns[i].ColumnName.ToUpper() == "STOREHOUSEID")
        //            {
        //                _ser.Add(_mdt.Columns[i].ColumnName, ""); continue;
        //            }

        //            if (arrDr == null || arrDr.Length < 1)
        //            {
        //                string sql1 = string.Format(" esn='{0}' and {1}='{2}'", dr["esn"].ToString(), _mdt.Columns[i].ColumnName, "NA");
        //                arrDr = _dt.Select(sql1);
        //                _ser.Add(_mdt.Columns[i].ColumnName, arrDr[0][_mdt.Columns[i].ColumnName].ToString());
        //                continue;
        //            }
        //            //return null;
        //            if (arrDr != null && arrDr.Length > 1)
        //                return null;
        //            _ser.Add(_mdt.Columns[i].ColumnName, arrDr[0][_mdt.Columns[i].ColumnName].ToString());
        //        }
        //        _mdt.Rows.Add(arrDr[0].ItemArray);
        //        foreach (string str in _ser.Keys)
        //        {
        //            _mdt.Rows[x][str] = _ser[str];
        //        }
        //        x++;
        //    }

        //    mds.Tables.Add(_mdt);
        //    return mds.Tables[0];
        //}
        private void tb_selectvalue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                bt_select_Click(null, null);
            }
        }

        private void bt_rrefresh_Click(object sender, EventArgs e)
        {
            bt_select_Click(null, null);
        }

        private void bt_excel_Click(object sender, EventArgs e)
        {
            if (this.dgv_productsninfo.RowCount == 0)
            {
                MessageBox.Show("没有数据可以导出到Excel");
                return;
            }
            else
            {
                //DataToExcel(dgv_productsninfo);
                WriteExcel(dgv_productsninfo);
            }
        }
        public static void WriteExcel(DataGridView m_DataView)
        {
            if (m_DataView.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可供导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                SaveFileDialog ss = new SaveFileDialog();
                ss.Filter = "Execl files (*.xls)|*.xls";
                ss.FilterIndex = 0;
                ss.RestoreDirectory = true;
                //saveFileDialog2.CreatePrompt = true;
                ss.Title = "导出文件保存路径";
                ss.FileName = null;
                ss.ShowDialog();
                string FileName = ss.FileName;

                if (FileName.Length != 0)
                {
                    //toolStripProgressBar1.Visible = true;
                    //toolStripProgressBar1.Value = 0;
                    try
                    {
                        StreamWriter sw = new StreamWriter(FileName, false, Encoding.GetEncoding("gb2312"));
                        StringBuilder sb = new StringBuilder();

                        
                        for (int k = 0; k < m_DataView.Columns.Count; k++)
                        {
                            sb.Append(@"=""" + m_DataView.Columns[k].HeaderText.ToString() + @"""" + "\t");
                        }
                        sb.Append(Environment.NewLine);

                        for (int i = 0; i < m_DataView.Rows.Count; i++)
                        {
                            for (int j = 0; j < m_DataView.Columns.Count; j++)
                            {
                                sb.Append(@"=""" + m_DataView.Rows[i].Cells[j].Value.ToString() + @"""" + "\t");
                            }
                            sb.Append(Environment.NewLine);//每写一行数据后换行
                            //toolStripProgressBar1.Value += 100 / dt.Rows.Count;
                        }
                        sw.Write(sb.ToString());
                        sw.Flush();
                        sw.Close();//释放资源
                        MessageBox.Show("数据已经成功导出到：" + ss.FileName.ToString(), "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        //toolStripProgressBar1.Value = 0;
                        //toolStripProgressBar1.Visible = false;
                    }
                }
            }
        }

        
        #endregion

        private void bt_customer_Click(object sender, EventArgs e)
        {
            //fCustomerInfo dz = new fCustomerInfo(mFrm, this);

            //dz.ShowDialog();
        }
        /// <summary>
        /// 标志位，在选择数据时使用
        /// </summary>
       // private Entity.tWarehouseWipTrackingTable twh = new Entity.tWarehouseWipTrackingTable();

        //private WebServices.tWarehouseWipTracking.tWarehouseWipTrackingTable twh = new WebServices.tWarehouseWipTracking.tWarehouseWipTrackingTable();

        //public WebServices.tWarehouseWipTracking.tWarehouseWipTrackingTable Twh
        //{
        //    get { return twh; }
        //    set { twh = value; }
        //}
        //private void bt_selectwoid_Click(object sender, EventArgs e)
        //{
        //    twh.mFlag = "1";
        //    SelectData sd = new SelectData(this,
        //        FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetAllWoInfo()));
        //    sd.ShowDialog();
        //}

        //private void bt_selectstor_Click(object sender, EventArgs e)
        //{
        //    twh.mFlag = "2";
        //    SelectData sd = new SelectData(this,
        //        FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetWarehouseList()));
        //    sd.ShowDialog();
        //}

        //private void bt_selectloc_Click(object sender, EventArgs e)
        //{
        //    twh.mFlag = "3";
        //    SelectData sd = new SelectData(this,
        //        FrmBLL.ReleaseData.arrByteToDataTable(refWebtStorehouseManage.Instance.GetAlltStorehouseLoctionInfo()));
        //    sd.ShowDialog();
        //}
        private void dgv_productsninfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
                        dgv_productsninfo.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                       dgv_productsninfo.RowHeadersDefaultCellStyle.Font, rectangle,
                       dgv_productsninfo.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgvStock_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
                     dgvStock.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                       dgvStock.RowHeadersDefaultCellStyle.Font, rectangle,
                       dgvStock.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //private void Btn_Ds_Info_Click(object sender, EventArgs e)
        //{
        //    string status="";
        //    this.Dgv_Ds_Info.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebT_DS_Out.Instance.Get_DSInfo(Dt_Sta.Value, Dt_End.Value, out status));
        //    //Dgv_Ds_Info
        //}


       

    }
}
