using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.IO;

namespace SFIS_V2
{
    public partial class FrmQuerySix : Office2007Form // Form
    {
        public FrmQuerySix(MainParent sInfo)
        {
            InitializeComponent();
            sMain = sInfo;
        }
        MainParent sMain;        
        int To_ExcelFlag = 0;
        bool QueryWip = false;
     
        private void FrmQuerySix_Load(object sender, EventArgs e)
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

            //string C_RES = "";
            //mPro.PRO_TEST_STOCKIN("A16KA0010B", "PACK_StoreIn", "PACK_StoreIn", "FX005563-147", "NA", "SSSS", out C_RES);

            this.btOutPutWoSn.Visible = false;
            btSn.Text = "产品\r\nESN";
            btcarton.Text = "产品\r\n箱号";
            bttray.Text = "Tray\r\n盘号";
            tbDataSelect.Focus();
            CbSelect.SelectedIndex = 0;
        }    
      
        private void btSn_Click(object sender, EventArgs e)
        {
          string SerialSN=  Input.InputQuery.ShowInputBox("请输入唯一条码",string.Empty);
          if (!string.IsNullOrEmpty(SerialSN))
          {
              QueryWIP("ESN",SerialSN);
              GetWipKeyParts(SerialSN);
          }

        }

        private void btWo_Click(object sender, EventArgs e)
        {
            string Wo = Input.InputQuery.ShowInputBox("请输入工单",string.Empty);
            if (!string.IsNullOrEmpty(Wo))
            {
                QueryWIP("woid",Wo);
            }
        }

        private void btQuery_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbDataSelect.Text)) && (!string.IsNullOrEmpty(CbSelect.Text))  )
            {
                try
                {
                    if (chkhistory.Checked)
                    {
                        if (CbSelect.Text != "唯一条码")
                        {
                            MessageBox.Show("历史资料只能以[唯一条码]查询");
                            return;
                        }
                    }

                    string Column = "";

                    switch (CbSelect.Text)
                    {
                        case "唯一条码":
                            Column = "ESN";
                            break;
                        case "工单":
                            Column = "WOID";
                            break;
                        case "产品箱号":
                            Column = "cartonnumber";
                            break;
                        case "Tray盘号":
                            Column = "trayno";
                            break;
                        case "栈板号":
                            Column = "palletnumber";
                            break;
                        case "SN":
                            Column = "SN";
                            break;
                        case "MAC":
                            Column = "MAC";
                            break;
                        case "入库编号":
                            Column = "storenumber";
                            break;
                        default:
                            Column = "ESN";
                            break;

                    }
                    dgvWipKeyparts.DataSource = null;
                    dgvWipTracking.DataSource = null;

                    if (CbSelect.Text == "KeyParts")
                    {
                        DataTable dtKeyparts = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyParts("SNVAL",tbDataSelect.Text.Trim()));
                        string c_esn = "ERROR";
                        if (dtKeyparts.Rows.Count > 0)
                            c_esn = dtKeyparts.Rows[0]["ESN"].ToString();
                        if (c_esn != "ERROR")
                        {
                            QueryWIP("ESN", c_esn);
                            GetWipKeyParts(c_esn);
                        }
                        else
                        {
                            MessageBox.Show("No Data");
                            tbDataSelect.Focus();
                            tbDataSelect.SelectAll();
                            return;
                        }
                    }
                    else
                    {
                        if (chkhistory.Checked)
                        {
                            HistoryWipTracking(Column, tbDataSelect.Text.Trim());
                            HistoryWipKeyparts(Column, tbDataSelect.Text.Trim());
                        }
                        else
                        {
                           
                            QueryWIP(Column, tbDataSelect.Text.Trim());
                            if (CbSelect.Text == "唯一条码")
                            {
                                GetWipKeyParts(tbDataSelect.Text.Trim());
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("查询异常:"+ex.Message);
                }
                finally
                {
                    tbDataSelect.Focus();
                    tbDataSelect.SelectAll();
                }
            }
        }

        private void btcarton_Click(object sender, EventArgs e)
        {
            string Carton = Input.InputQuery.ShowInputBox("请输入产品箱号", string.Empty);
            if (!string.IsNullOrEmpty(Carton))
            {
                QueryWIP("cartonnumber", Carton);
            }
        }

        private void bttray_Click(object sender, EventArgs e)
        {
            string Tray = Input.InputQuery.ShowInputBox("请输入Tray盘号",string.Empty);
            if (!string.IsNullOrEmpty(Tray))
            {
                QueryWIP("trayno",Tray);
            }
        }

        private void btpallet_Click(object sender, EventArgs e)
        {
            string Pallet = Input.InputQuery.ShowInputBox("请输入产品栈板号", string.Empty);
            if (!string.IsNullOrEmpty(Pallet))
            {
                QueryWIP("palletnumber",Pallet);
            }
        }

        private void btPartsn_Click(object sender, EventArgs e)
        {
            string SN = Input.InputQuery.ShowInputBox("请输入SN号码",string.Empty);
            if (!string.IsNullOrEmpty(SN))
            {
                QueryWIP("SN", SN);
            }
        }

        private void btMAC_Click(object sender, EventArgs e)
        {
            string MAC = Input.InputQuery.ShowInputBox("请输入产品MAC",string.Empty);
            if (!string.IsNullOrEmpty(MAC))
            {
                QueryWIP("MAC",MAC);
            }
        }

        private void QueryWIP(string ColumnName, string Qdata)
        {
            DataTable dt=null;
            //if (chkhistory.Checked)
            // dt=FrmBLL.ReleaseData.arrByteToDataTable( refWebtWipTracking.Instance.GetHistoryQueryWipAllInfo(ColumnName,Qdata));
            //else
             dt=FrmBLL.ReleaseData.arrByteToDataTable( refWebtWipTracking.Instance.GetQueryWipAllInfo(ColumnName,Qdata));

          //  UpdateCraftName(dt);
            dgvWipTracking.DataSource=dt;
            QueryWip = true;
     
        }

        private void GetWipDetail(string Serial)
        {
            DataTable dt=null;         
           dt=  FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipDetail.Instance.GetWipDetail(Serial));         
            dgvWipTracking.DataSource = dt;
        }
        private void GetSnRange(string woId)
        {
            DataTable dt = tCheckDataTestAte.Instance.GetWOSnRule(woId).Tables[0];
         //  DataSet.Tables["XX"].Columns["xx"].ColumnName = "NewColumnName";
            dt.Columns[0].ColumnName = "工单";
            dt.Columns[1].ColumnName = "条码类型";
            dt.Columns[2].ColumnName = "条码前缀";
            dt.Columns[3].ColumnName = "条码后缀";
            dt.Columns[4].ColumnName = "区间开始";
            dt.Columns[5].ColumnName = "区间结束";
            dt.Columns[6].ColumnName = "条码长度";
            dt.Columns[7].ColumnName = "条码版本";
            dt.Columns[8].ColumnName = "描述";
            dt.Columns[9].ColumnName = "时间";
            dt.Columns[10].ColumnName = "料号";
            dt.Columns[11].ColumnName = "数量";
            dt.Columns[12].ColumnName = "PVER";
            dt.Columns[13].ColumnName = "USENUM";
            dgvWipTracking.DataSource = dt;
        }
      
      
        private void GetWipKeyParts(string Serial)
        {     
            dgvWipKeyparts.DataSource = FrmBLL.ReleaseData.arrByteToDataTable( refWebtWipKeyPart.Instance.GetWipKeyParts("ESN",Serial));      
        }

        private void dgvWipTracking_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (QueryWip == true)
                {
                    string ESN = dgvWipTracking.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (chkhistory.Checked)
                    {
                        dgvWipTracking.DataSource = HistoryWipDetail("ESN", ESN);                         
                    }
                    else
                    {
                        GetWipDetail(ESN);
                      //  GetWipKeyParts(ESN);
                    }
                }
            }
        }

        private void tbDataSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDataSelect.Text))
            {
                if (e.KeyValue == 13)
                    this.btQuery_Click(null, null);
            }
        }

        private void Bt_excel_Click(object sender, EventArgs e)
        {
            if (To_ExcelFlag == 0)
            {
                if (dgvWipTracking.RowCount != 0)
                {
                    FrmBLL.DataGridViewToExcel.DataToExcel(dgvWipTracking);
                    MessageBox.Show(this, "保存EXCEL成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    sMain.ShowPrgMsg("没有资料可以导出", MainParent.MsgType.Warning);
                }
            }
            else
                if (To_ExcelFlag == 1)
                {
                    if (dgvWipKeyparts.RowCount != 0)
                    {
                        FrmBLL.DataGridViewToExcel.DataToExcel(dgvWipKeyparts);
                        MessageBox.Show(this, "保存EXCEL成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        sMain.ShowPrgMsg("没有资料可以导出", MainParent.MsgType.Warning);
                    }
                }
        }

       
        private void dgvWipTracking_Click(object sender, EventArgs e)
        {
            To_ExcelFlag = 0;
        }

        private void dgvWipKeyparts_Click(object sender, EventArgs e)
        {
            To_ExcelFlag = 1;
        }

        private void CbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.CbSelect.SelectedItem.ToString())
            {
                case "工单":
                    this.btOutPutWoSn.Visible = true;
                    break;
                default:
                    this.btOutPutWoSn.Visible = false;
                    break;
            }
            tbDataSelect.Focus();
        }

        private void btSnRange_Click(object sender, EventArgs e)
        {
            QueryWip = false;
            string Range = Input.InputQuery.ShowInputBox("请输入工单号码", string.Empty);
            if (!string.IsNullOrEmpty(Range))
            {
                GetSnRange(Range);
            }
        }


        //private void UpdateCraftName(DataTable dtStatus)
        //{
        //    for (int i = 0; i < dtStatus.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            //dtStatus.Rows[i]["当前站位"] = CraftList[dtStatus.Rows[i]["当前站位"].ToString()];
        //            //dtStatus.Rows[i]["下一站"] = CraftList[dtStatus.Rows[i]["下一站"].ToString()];
        //            //dtStatus.Rows[i]["优先途程"] = CraftList[dtStatus.Rows[i]["优先途程"].ToString()];
        //            dtStatus.Rows[i]["当前站位"] =dtStatus.Rows[i]["当前站位"].ToString();
        //            dtStatus.Rows[i]["下一站"] = dtStatus.Rows[i]["下一站"].ToString();
        //            dtStatus.Rows[i]["优先途程"] = dtStatus.Rows[i]["优先途程"].ToString();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}

        private void dgvWipTracking_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
                this.dgvWipTracking[e.ColumnIndex, e.RowIndex].ToolTipText =
                    string.Format("当前累计:{0} 行数据", this.dgvWipTracking.Rows.Count.ToString());
        }

        private void btOutPutWoSn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbDataSelect.Text))
                return;
            try
            {
                DataTable dt = null;
                //if (chkhistory.Checked)
                //    dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.H_GetWoAllSerial(this.tbDataSelect.Text.Trim(), 1000));
                //else
                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetWoAllSerial(this.tbDataSelect.Text.Trim(), 1000));
                DataView dv = dt.DefaultView;
                dv.Sort = "ESN ASC";
                DataTable dTemp = dv.ToTable();
                WriteExcel(GetCrossTable(dTemp));
                dTemp.Dispose();
                dv.Dispose();
                //  MessageBoxEx.Show("保存成功", "提示");

            }
            catch (Exception ex)
            {
                this.sMain.ShowPrgMsg(ex.Message, MainParent.MsgType.Warning);
            }
        }

        //private void btsntestmach_Click(object sender, EventArgs e)
        //{
        //    //QueryWip = false;
        //    //string esn = Input.InputQuery.ShowInputBox("请输入ESN", string.Empty);
        //    //if (!string.IsNullOrEmpty(esn))
        //    //{
        //    //    GetSnTestMachineInfo(esn);
        //    //}
        //}
        ///// <summary>
        ///// 获取产品序号在某个机台号上测试的
        ///// </summary>
        ///// <param name="esn"></param>
        //private void GetSnTestMachineInfo(string esn)
        //{
        //    DataTable dt =FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetSnTestMachineInfo(esn));
        
        //    dt.Columns[0].ColumnName = "唯一序列号";
        //    dt.Columns[1].ColumnName = "工单";
        //    dt.Columns[2].ColumnName = "途程";
        //    dt.Columns[3].ColumnName = "机器编号";
        //    dt.Columns[4].ColumnName = "治具编号";
        //    dt.Columns[5].ColumnName = "测试标记";
        //    dt.Columns[6].ColumnName = "人员工号";
        //    dt.Columns[7].ColumnName = "时间";  

          
        //    dgvWipTracking.DataSource = dt;
        //}

        private void btsnlist_Click(object sender, EventArgs e)
        {
            QueryWip = false;
            string woId = string.Empty;
            string sntype = "MAC";
            string InputStr = Input.InputQuery.ShowInputBox("请输入工单号码(工单-条码类型)", string.Empty);
            if (!string.IsNullOrEmpty(InputStr))
            {
                try
                {
                    woId = InputStr.Split('-')[0];
                    sntype = InputStr.Split('-')[1];                   
                }
                catch
                {
                   
                }
                GetWoSnListInfo(woId, sntype); 
            }
        }

        private void GetWoSnListInfo(string woId,string sntype)
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoSnListInfo(woId, sntype));
            dt.Columns[0].ColumnName = "工单";
            dt.Columns[1].ColumnName = "类型";
            dt.Columns[2].ColumnName = sntype.ToUpper()+"号码";
            dt.Columns[3].ColumnName = "状态";
            dt.Columns[4].ColumnName = "序号";
            dt.Columns[5].ColumnName = "序号值"; 
            dgvWipTracking.DataSource = dt;
        }

        public static DataTable GetCrossTable(DataTable dt)
        {
            if (dt == null || dt.Columns.Count != 5 || dt.Rows.Count == 0)
            {
                return dt;
            }
            else
            {

                DataTable result = new DataTable();
                result.Columns.Add(dt.Columns[0].ColumnName);
                result.Columns.Add(dt.Columns[1].ColumnName);//增加第二列的值
                result.Columns.Add(dt.Columns[2].ColumnName);//增加第三列的值
                DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[3].ColumnName); //以第四列为字段建立交叉表列名
                for (int i = 0; i < dtColumns.Rows.Count; i++)
                {
                    string colName;
                    if (dtColumns.Rows[1][0] is DateTime)
                    {
                        colName = Convert.ToDateTime(dtColumns.Rows[i][0]).ToString();
                    }
                    else
                    {
                        colName = dtColumns.Rows[i][0].ToString();
                    }
                    result.Columns.Add(colName);
                    result.Columns[i + 3].DefaultValue = "0";// result.Columns[i + 1].DefaultValue = "0"; 因为前两列值一致,增加一个默认值
                }
                DataRow drNew = result.NewRow();
                drNew[0] = dt.Rows[0][0];
                drNew[1] = dt.Rows[0][1]; //增加第二列的值
                drNew[2] = dt.Rows[0][2]; //增加第三列的值
                string rowName = drNew[0].ToString();            
                foreach (DataRow dr in dt.Rows)
                {
                    string colName = dr[3].ToString(); //以第四列为字段建立交叉表列名
                   // double dValue = Convert.ToDouble(dr[4]);//以第五列值添加
                    string dValue = dr[4].ToString();
                    if ((dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase)) && 
                        (dr[1].ToString().Equals(drNew[1].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                        (dr[2].ToString().Equals(drNew[2].ToString(), StringComparison.CurrentCultureIgnoreCase)))
                    {

                        drNew[colName] = dValue.ToString();
                    }
                    else
                    {
                        result.Rows.Add(drNew);
                        drNew = result.NewRow();
                        drNew[0] = dr[0];
                        drNew[1] = dr[1];//增加第二列的值
                        drNew[2] = dr[2];//增加第三列的值
                        rowName = drNew[0].ToString();
                        drNew[colName] = dValue.ToString();
                    }
                }
                result.Rows.Add(drNew);
                return result;
            }
        }

        /// <summary>
        /// 将DataTable中的列名及数据导出到Excel表中
        /// </summary>
        /// <param name="tmpDataTable">要导出的DataTable</param>
        /// <param name="strFileName">Excel的保存路径及名称</param>
        public static void DataTabletoExcel(System.Data.DataTable tmpDataTable, string strFileName)
        {
            if (tmpDataTable == null)
                return;
            int rowNum = tmpDataTable.Rows.Count;
            int columnNum = tmpDataTable.Columns.Count;
            int rowIndex = 1;
            int columnIndex = 0;

            Excel.Application xlApp = new Excel.ApplicationClass();
            xlApp.DefaultFilePath = "";
            xlApp.DisplayAlerts = true;
            xlApp.SheetsInNewWorkbook = 1;
            Excel.Workbook xlBook = xlApp.Workbooks.Add(true);

            //将DataTable的列名导入Excel表第一行
            foreach (DataColumn dc in tmpDataTable.Columns)
            {
                columnIndex++;
                xlApp.Cells[rowIndex, columnIndex] = dc.ColumnName;
            }

            //将DataTable中的数据导入Excel中
            for (int i = 0; i < rowNum; i++)
            {
                rowIndex++;
                columnIndex = 0;
                for (int j = 0; j < columnNum; j++)
                {
                    columnIndex++;
                    xlApp.Cells[rowIndex, columnIndex] = tmpDataTable.Rows[i][j].ToString();
                }
            }
            xlBook.SaveCopyAs(strFileName);
        }

        public void WriteExcel(DataTable m_DataView)
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
                            sb.Append(@"=""" + m_DataView.Columns[k].ColumnName.ToString() + @"""" + "\t");
                        }
                        sb.Append(Environment.NewLine);

                        for (int i = 0; i < m_DataView.Rows.Count; i++)
                        {
                            for (int j = 0; j < m_DataView.Columns.Count; j++)
                            {
                                sb.Append(@"=""" + m_DataView.Rows[i][j].ToString() + @"""" + "\t");
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

        private void tb_tr_sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_tr_sn.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    string _StrErr = string.Empty;
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_Tr_Sn_Info(tb_tr_sn.Text.Trim(),out _StrErr));

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        dt.Rows[x]["STATUS"] = FrmBLL.publicfuntion.MaterialStatus(dt.Rows[x]["STATUS"].ToString());
                    }
                    dgv_Trsn_Info.DataSource = dt;
                    trsndetail = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    tb_tr_sn.Text = string.Empty;
                }

            }
        }

        private void dgv_Trsn_Info_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        
        }
        bool trsndetail = true;
        private void dgv_Trsn_Info_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (trsndetail)
                {
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Get_Tr_Sn_Detail(dgv_Trsn_Info.Rows[e.RowIndex].Cells["tr_sn"].Value.ToString()));

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        dt.Rows[x]["STATUS"] = FrmBLL.publicfuntion.MaterialStatus(dt.Rows[x]["STATUS"].ToString());
                    }

                    dgv_Trsn_Info.DataSource = FrmBLL.publicfuntion.DataTableToSort(dt, "STOCK_DATE");
                    trsndetail = false;
                }
            }
        }

        private void HistoryWipTracking(string Colnum,string DATA)
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebHistoryService.Instance.H_WIP_TRACKING(Colnum, DATA));
            dgvWipTracking.DataSource = dt;
        }
        private void HistoryWipKeyparts(string Colnum, string DATA)
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebHistoryService.Instance.H_WIP_KEYPARTS(Colnum,DATA));
            dgvWipKeyparts.DataSource=dt;
        }

        private DataTable HistoryWipDetail(string Colnum, string DATA)
        {
            return FrmBLL.ReleaseData.arrByteToDataTable(refWebHistoryService.Instance.H_WIP_DETAIL(Colnum, DATA));
        }

        private void dgvWipTracking_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FrmBLL.publicfuntion.UpdateFieldName(dgvWipTracking);
        }

        private void dgvWipKeyparts_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FrmBLL.publicfuntion.UpdateFieldName(dgvWipKeyparts);
        }


       
    }
}
