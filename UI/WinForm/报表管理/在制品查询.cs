using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;


namespace SFIS_V2
{
    public partial class FrmWipTracking : Office2007Form  //Form
    {
        public FrmWipTracking(MainParent sInfo)
        {
            InitializeComponent();
            sMain = sInfo;
        }
        MainParent sMain;
        System.Windows.Forms.Timer My_RefreshTime = new System.Windows.Forms.Timer();
        Dictionary<string, string> dsCraft = new Dictionary<string, string>();
        private void FrmWipTracking_Load(object sender, EventArgs e)
        {
            My_RefreshTime.Interval = 60000;
            My_RefreshTime.Enabled = true;
            My_RefreshTime.Tick +=new EventHandler(My_RefreshTime_Tick);

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
            #region 单元格交替颜色
           // this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque;
           // this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            #endregion
            PartList.Text = listallPart.Text;
            wolist.Text = listallwo.Text;
            GetWoIdPartNumberList();
            
            //this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque;
            //this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            

            DataTable dt =FrmBLL.ReleaseData.arrByteToDataTable( RefWebService_BLL.refWebtCraftInfo.Instance.GetAllCraftInfo());
            foreach (DataRow dr in dt.Rows)
            {
                dsCraft.Add(dr[0].ToString(),dr[1].ToString());
            }
        }
        /// <summary>
        /// 刷新标记
        /// </summary>
        bool My_Refresh = true;
        private void My_RefreshTime_Tick(object sender,EventArgs e)
        {
            label1.BackColor = Color.Green;
            My_Refresh = true;
            My_RefreshTime.Enabled = false;
        }

        DataTable mdt = null;
        Dictionary<string, string> names = new Dictionary<string, string>();
        private void GetWoIdPartNumberList()
        {
            string key = string.Empty;
            try
            {
                mdt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWipTracking.Instance.GetPartNumberAndwoIdList());//.Tables[0];
                DataTable dz = mdt.DefaultView.ToTable(true, "partnumber", "productname");
                foreach (DataRow dr in dz.Rows)
                {
                    listallPart.Items.Add(dr["partnumber"].ToString() + "-" + dr["productname"].ToString());
                    //listallPart.Items.Add(dr["partnumber"].ToString() + "-" + (str=FrmBLL.publicfuntion.getNewTable(mdt, string.Format("partnumber='{0}'", dr["partnumber"].ToString())).Rows[0][2].ToString()));
                    key = dr["partnumber"].ToString();
                    if (!names.ContainsKey(dr["partnumber"].ToString()))
                    names.Add(dr["partnumber"].ToString(), dr["productname"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + key);
            }
        }

        private void btpartselect_Click(object sender, EventArgs e)
        {
            if (listallPart.Visible == false)
            {
                PalPartNo.Visible = true;
                btpartselect.Text = "料号选择 ▲";
            }
            else
            {
                PalPartNo.Visible = false;
                btpartselect.Text = "料号选择 ▼";                
            }
        }

        private void btwoselect_Click(object sender, EventArgs e)
        {
            if (listallwo.Visible == false)
            {
                Palwo.Visible = true;
                btwoselect.Text = "工单选择 ▲";
            }
            else
            {
                Palwo.Visible = false;
                btwoselect.Text = "工单选择 ▼";
            }

        }
        System.Collections.ArrayList NamesWo = new System.Collections.ArrayList();
        private void btPartOK_Click(object sender, EventArgs e)
        {
            PartList.Items.Clear();
            if (this.listallPart.SelectedItems.Count < 1)
            {
                this.sMain.ShowPrgMsg("没有选择任何项..", MainParent.MsgType.Warning);
                return;
            }
            for (int i = 0; i < this.listallPart.SelectedItems.Count; i++)
            {
                string[] arr = new string[10];
                arr = this.listallPart.SelectedItems[i].ToString().Split('-');
                if (PartList.FindString(arr[0]) == -1)
                    PartList.Items.Add(arr[0]);
            }
            btpartselect_Click(null, null);


            listallwo.Items.Clear();
            string model = "";
            GetListStringPart(out model, PartList);
            DataTable dtwoid = FrmBLL.publicfuntion.getNewTable(mdt, string.Format("PartNumber in ({0})", model));
            foreach (DataRow dr in dtwoid.Rows)
            {
                listallwo.Items.Add(dr["woid"].ToString());
                NamesWo.Add(dr["woid"].ToString());
            }
            wolist.Items.Clear();
        }

        private void btPartCancel_Click(object sender, EventArgs e)
        {
            btpartselect_Click(null, null);
        }

        private void btwoOK_Click(object sender, EventArgs e)
        {
            wolist.Items.Clear();
            if (this.listallwo.SelectedItems.Count < 1)
            {
                this.sMain.ShowPrgMsg("没有选择任何项..", MainParent.MsgType.Warning);
                return;
            }
            for (int i = 0; i < this.listallwo.SelectedItems.Count; i++)
            {
                if (this.wolist.FindString(this.listallwo.SelectedItems[i].ToString()) == -1)
                    wolist.Items.Add(this.listallwo.SelectedItems[i].ToString());
            }
            btwoselect_Click(null, null);
        }

        private void btwoCancel_Click(object sender, EventArgs e)
        {
            btwoselect_Click(null, null);
        }

        private List<string> GetListString(ListBox StrList)
        {
            List<string> LsStr = new List<string>();
            for (int x = 0; x < StrList.Items.Count; x++)
            {
                LsStr.Add(string.Format("{0}", StrList.Items[x].ToString()));                  
               
            }

            return LsStr;
        }
        private string GetListStringPart(out string str, ListBox StrList)
        {
            str = "";
            for (int x = 0; x < StrList.Items.Count; x++)
            {

                if (x < StrList.Items.Count - 1)
                    str = str +"'" +StrList.Items[x].ToString() + "',";
                else
                    str = str +"'"+ StrList.Items[x].ToString()+"'";
            }

            return str;
        }

        private void btQuery_Click(object sender, EventArgs e)
        {
            if (My_Refresh)
            {
                label1.BackColor = Color.Red;
                My_Refresh = false;
                My_RefreshTime.Enabled = true;
            }
            else
            {
                MessageBox.Show("距离上次刷新间隔时间需等待一分钟", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((PartList.Items.Count == 0) || (wolist.Items.Count == 0))
            {

                sMain.ShowPrgMsg("产品料号或工单不能为空,请选择.....",MainParent.MsgType.Error);
                return;
            }

            if (chkallline.Checked)
            {
                #region 显示所有线体
                DataTable dtwip = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWipTracking.Instance.GetWipTrackingList(GetListString(PartList).ToArray(), GetListString(wolist).ToArray()));

                if (dtwip==null || dtwip.Rows.Count <1)
                {
                    dataGridView1.DataSource = null; 
                    MessageBox.Show("没有找到数据");

                    return;
                }

                DataTable dt = GetWorkInProcess(dtwip, 0);
                dt.Rows.Add("Total");
                for (int i = 5; i < dt.Columns.Count; i++)
                {
                    int x = 0;
                    for (int y=0;y<dt.Rows.Count-1;y++)
                    {
                        x += int.Parse(dt.Rows[y][i].ToString());
                    }

                    dt.Rows[dt.Rows.Count - 1][i] = x.ToString();
                }
                dataGridView1.DataSource = dt;
               
                #endregion
            }
            else
            {
                #region 显示所有线体
                DataTable dtwip = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWipTracking.Instance.GetWipTrackingLineList(GetListString(PartList).ToArray(), GetListString(wolist).ToArray()));
                if (dtwip == null || dtwip.Rows.Count < 1)
                {
                    dataGridView1.DataSource = null; 
                    MessageBox.Show("没有找到数据");
                    return;
                }

                DataTable dt = GetWorkInProcess(dtwip,1);
                dt.Rows.Add("Total");
                for (int i = 5; i < dt.Columns.Count-1; i++)
                {
                    int x = 0;
                    for (int y = 0; y < dt.Rows.Count - 1; y++)
                    {
                        x += int.Parse(dt.Rows[y][i].ToString());
                    }

                    dt.Rows[dt.Rows.Count - 1][i] = x.ToString();
                }
                dataGridView1.DataSource = dt;
            
                #endregion

            }
            #region
            //if (chkallline.Checked)
            //{
            //    #region  不显示线体
            //   // DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWipTracking.Instance.GetWipTrackingList(StrPart, StrWoId));
            //    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWipTracking.Instance.CROSSTAB_WIP( StrWoId,StrPart,0));
            //    //DataTable dt = ds.Tables[0];
            //    DataTable dz = SelectDistinct(dt, "wipstation");
            //    dataGridView1.Columns.Clear();
            //    dataGridView1.Columns.Add("工单", "工单");
            //    dataGridView1.Columns.Add("产品料号", "产品料号");
            //    dataGridView1.Columns.Add("产品名称", "产品名称");
            //    dataGridView1.Columns.Add("工单套数", "工单套数");
            //    dataGridView1.Columns.Add("线别", "线别");
            //    foreach (DataRow item in dz.Rows)
            //    {
            //        dataGridView1.Columns.Add(item["wipstation"].ToString(), item["wipstation"].ToString());
            //    }
            //    dataGridView1.Columns.Add("未投入数", "未投入数");

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            dataGridView1.Rows.Add(dt.Rows[i]["woid"].ToString());
            //            dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i]["partnumber"].ToString();
            //            dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i]["productname"].ToString();
            //            dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i]["targetqty"].ToString();
            //            dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = dt.Rows[i]["notinput"].ToString();

            //        }
            //        else
            //            if (dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value.ToString() != dt.Rows[i]["woid"].ToString())
            //            {
            //                dataGridView1.Rows.Add(dt.Rows[i]["woid"].ToString());
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = dt.Rows[i]["partnumber"].ToString();
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = dt.Rows[i]["productname"].ToString();
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = dt.Rows[i]["targetqty"].ToString();                
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dataGridView1.Columns.Count - 1].Value = dt.Rows[i]["notinput"].ToString();
            //            }
            //        for (int x = 5; x < dataGridView1.Columns.Count - 1; x++)
            //        {
            //            if (dt.Rows[i]["wipstation"].ToString() == dataGridView1.Columns[x].Name.ToString())
            //            {
            //                if (i == 0)
            //                    dataGridView1.Rows[i].Cells[x].Value = dt.Rows[i]["qty"].ToString();
            //                else
            //                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[x].Value = dt.Rows[i]["qty"].ToString();
            //            }

            //            if (i == 0)
            //            {
            //                dataGridView1.Rows[i].Cells[4].Value = "ALL";

            //                try
            //                {
            //                    dataGridView1.Rows[i].Cells[x].Value.ToString();
            //                }
            //                catch
            //                {
            //                    dataGridView1.Rows[i].Cells[x].Value = "0";
            //                }

            //            }
            //            else
            //            {
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = "ALL";
            //                try
            //                {
            //                    string.IsNullOrEmpty(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[x].Value.ToString());
            //                }
            //                catch
            //                {
            //                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[x].Value = "0";
            //                }

            //            }

            //        }

            //    }

            //    if (dataGridView1.RowCount != 0)
            //    {
            //        dataGridView1.Rows.Add("Total");
            //        int Total = 0;
            //        for (int j = 0; j <= dz.Rows.Count; j++)
            //        {
            //            Total = 0;

            //            for (int z = 0; z < dataGridView1.Rows.Count - 1; z++)
            //            {
            //                Total = Total + Convert.ToInt32(dataGridView1.Rows[z].Cells[j + 5].Value.ToString());
            //            }

            //            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[j + 5].Value = Total.ToString();
            //        }
            //    }
            //    #endregion
            //}
            //else
            //{

            //    #region 显示线体         
            // //  DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWipTracking.Instance.GetWipTrackingLineList(StrPart, StrWoId));
            //   DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWipTracking.Instance.CROSSTAB_WIP( StrWoId,StrPart, 1));
            //    DataTable dz = SelectDistinct(dt, "wipstation");
            //    dataGridView1.Columns.Clear();
            //    dataGridView1.Columns.Add("工单", "工单");
            //    dataGridView1.Columns.Add("产品料号", "产品料号");
            //    dataGridView1.Columns.Add("产品名称", "产品名称");
            //    dataGridView1.Columns.Add("工单套数", "工单套数");
            //    dataGridView1.Columns.Add("线别", "线别");
            //    foreach (DataRow item in dz.Rows)
            //    {
            //        dataGridView1.Columns.Add(item["wipstation"].ToString(), item["wipstation"].ToString());
            //    }
            //    dataGridView1.Columns.Add("未投入数", "未投入数");

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            dataGridView1.Rows.Add(dt.Rows[i]["woid"].ToString());
            //            dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i]["partnumber"].ToString();
            //            dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i]["productname"].ToString();
            //            dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i]["targetqty"].ToString();
            //            dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i]["line"].ToString();
            //            dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = dt.Rows[i]["notinput"].ToString();

            //        }
            //        else
            //        {
            //            #region 删除整行为0的行/每次增加时删除
            //            for (int j = 0; j <= dataGridView1.Rows.Count - 1; j++)
            //            {
            //                int Count0 = 0;
            //                for (int yy = 5; yy <= dataGridView1.Columns.Count - 2; yy++)
            //                {
            //                    if (dataGridView1.Rows[j].Cells[yy].Value.ToString() == "0")
            //                    {
            //                        Count0 = Count0 + 1;
            //                    }
            //                }

            //                if (Count0 == dataGridView1.Columns.Count - 6)
            //                {
            //                    dataGridView1.Rows.RemoveAt(j);
            //                }
            //            }
            //            #endregion

            //            if ((dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value.ToString() != dt.Rows[i]["woid"].ToString()) || (dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value.ToString() != dt.Rows[i]["line"].ToString()))
            //            {
            //                dataGridView1.Rows.Add(dt.Rows[i]["woid"].ToString());
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = dt.Rows[i]["partnumber"].ToString();
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = dt.Rows[i]["productname"].ToString();
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = dt.Rows[i]["targetqty"].ToString();
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = dt.Rows[i]["line"].ToString();
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dataGridView1.Columns.Count - 1].Value = dt.Rows[i]["notinput"].ToString();
                        
            //            }
            //        }

          
            //        for (int x = 4; x < dataGridView1.Columns.Count - 1; x++)
            //        {
            //            bool sFlag = false;
            //            for  (int z=0;z < dataGridView1.Rows.Count - 1; z++)
            //            {
            //                if ((dt.Rows[i]["line"].ToString() == dataGridView1.Rows[z].Cells[4].Value.ToString()) && (dt.Rows[i]["woId"].ToString()==dataGridView1.Rows[z].Cells[0].Value.ToString()))
            //                {
            //                    for (int mm = 4; mm < dataGridView1.Columns.Count - 1; mm++)
            //                    {
            //                        if (dt.Rows[i]["wipstation"].ToString() == dataGridView1.Columns[mm].Name.ToString())
            //                        {

            //                            dataGridView1.Rows[z].Cells[mm].Value = dt.Rows[i]["qty"].ToString();
            //                            sFlag = true;
            //                        }
            //                    }
            //                }

            //            }
            //            if (!sFlag)
            //            {
            //                if (dt.Rows[i]["wipstation"].ToString() == dataGridView1.Columns[x].Name.ToString())
            //                {
            //                    if (i == 0)
            //                    {

            //                        dataGridView1.Rows[i].Cells[x].Value = dt.Rows[i]["qty"].ToString();

            //                    }
            //                    else
            //                    {
            //                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[x].Value = dt.Rows[i]["qty"].ToString();

            //                    }
            //                }
            //            }
                       
            //            if (x > 4)
            //            {
            //                if (i == 0)
            //                {
                           

            //                    try
            //                    {
            //                        dataGridView1.Rows[i].Cells[x].Value.ToString();
            //                    }
            //                    catch
            //                    {
            //                        dataGridView1.Rows[i].Cells[x].Value = "0";
            //                    }

            //                }
            //                else
            //                {
                         
            //                    try
            //                    {
            //                        string.IsNullOrEmpty(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[x].Value.ToString());
            //                    }
            //                    catch
            //                    {
            //                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[x].Value = "0";
            //                    }

            //                }

                            
            //            }

            //        }

            //    }         


            //    if (dataGridView1.RowCount != 0)
            //    {
            //        dataGridView1.Rows.Add("Total");
            //        int Total = 0;
            //        for (int j = 0; j <= dz.Rows.Count; j++)
            //        {
            //            Total = 0;

            //            for (int z = 0; z < dataGridView1.Rows.Count - 1; z++)
            //            {
            //                Total = Total + Convert.ToInt32(dataGridView1.Rows[z].Cells[j + 5].Value.ToString());
            //            }
            //            if (j < dz.Rows.Count)
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[j + 5].Value = Total.ToString();
            //        }

            //    }

            //    #endregion


            //    #region 删除整行为0的行/查询完成后再执行一次
            //    for (int j = 0; j <= dataGridView1.Rows.Count - 1; j++)
            //    {
            //        int Count0 = 0;
            //        for (int yy = 5; yy <= dataGridView1.Columns.Count - 2; yy++)
            //        {
            //            if (dataGridView1.Rows[j].Cells[yy].Value.ToString() == "0")
            //            {
            //                Count0 = Count0 + 1;
            //            }
            //        }

            //        if (Count0 == dataGridView1.Columns.Count - 6)
            //        {
            //            dataGridView1.Rows.RemoveAt(j);
            //        }
            //    }
            //    #endregion

            //}
            #endregion

            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[3].Frozen = true;
            dataGridView1.Columns[4].Frozen = true;
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridView1.Columns[3].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridView1.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
       

        }

        private DataTable SelectDistinct(DataTable SourceTable, params string[] FieldNames)
        {
            object[] lastValues;
            DataTable newTable;
            DataRow[] orderedRows;

            if (FieldNames == null || FieldNames.Length == 0)
                throw new ArgumentNullException("FieldNames");

            lastValues = new object[FieldNames.Length];
            newTable = new DataTable();

            foreach (string fieldName in FieldNames)
                newTable.Columns.Add(fieldName, SourceTable.Columns[fieldName].DataType);

            orderedRows = SourceTable.Select("", string.Join(",", FieldNames));

            foreach (DataRow row in orderedRows)
            {
                                     
                if (!fieldValuesAreEqual(lastValues, row, FieldNames))
                {
                    newTable.Rows.Add(createRowClone(row, newTable.NewRow(), FieldNames));

                    setLastValues(lastValues, row, FieldNames);
                }
            }

            return newTable;
        }

        private bool fieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
        {
            bool areEqual = true;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
                {
                    areEqual = false;
                    break;
                }
            }

            return areEqual;
        }

        private DataRow createRowClone(DataRow sourceRow, DataRow newRow, string[] fieldNames)
        {
            foreach (string field in fieldNames)
                newRow[field] = sourceRow[field];

            return newRow;
        }

        private void setLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
        {
            for (int i = 0; i < fieldNames.Length; i++)
                lastValues[i] = sourceRow[fieldNames[i]];
        }

        private void bt_ToExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount != 0)
                FrmBLL.DataGridViewToExcel.DataToExcel(dataGridView1);
            else
                sMain.ShowPrgMsg("没有资料可以导出Excel",MainParent.MsgType.Error);
        }

        private void listallPart_MouseEnter(object sender, EventArgs e)
        {
          
        }

        private void listallPart_MouseClick(object sender, MouseEventArgs e)
        {
            if (listallPart.Items.Count != 0)
            {
                toolTip1.SetToolTip(this.listallPart, this.listallPart.Items[this.listallPart.SelectedIndex].ToString());
                toolTip1.ShowAlways = true;
            }
        }

        private void tb_selectpartnumber_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_selectpartnumber.Text))
            {
                this.listallPart.Items.Clear();
                foreach (string str in this.names.Keys)
                {
                    if (str.IndexOf(this.tb_selectpartnumber.Text) != -1)
                    {
                        this.listallPart.Items.Add(str + "-" + this.names[str]);
                    }
                }
            }
            else
            {
                this.listallPart.Items.Clear();
                foreach (string str in this.names.Keys)
                {
                    this.listallPart.Items.Add(str + "-" + this.names[str]);
                }
            }
        }

        private void tb_selectwoId_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_selectwoId.Text))
            {
                this.listallwo.Items.Clear();
                foreach (string str in this.NamesWo)
                {
                    if (str.IndexOf(this.tb_selectwoId.Text) != -1)
                    {
                        this.listallwo.Items.Add(str);
                    }
                }
            }
            else
            {
                this.listallwo.Items.Clear();
                foreach (string str in this.NamesWo)
                {
                    this.listallwo.Items.Add(str);
                }
            }

        }

        private void wolist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (wolist.Items.Count < 1)
                    return;
                for (int i = 0; i < this.wolist.SelectedItems.Count; i++)
                {
                    wolist.Items.Remove(this.wolist.SelectedItems[i]);
                }
            }
        }

        private void PartList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (PartList.Items.Count < 1)
                    return;
                for (int i = 0; i < this.PartList.SelectedItems.Count; i++)
                {
                    PartList.Items.Remove(this.PartList.SelectedItems[i]);
                }
            }
        }

        private void btClearwolist_Click(object sender, EventArgs e)
        {
            this.wolist.Items.Clear();
        }

        private void btclearpartlist_Click(object sender, EventArgs e)
        {
            this.PartList.Items.Clear();
        }

        public  DataTable GetCrossWipTable(DataTable dt)
        {
            if (dt == null || dt.Columns.Count != 7 || dt.Rows.Count == 0)
            {
                return dt;
            }
            else
            {

                DataTable result = new DataTable();
                result.Columns.Add(dt.Columns[0].ColumnName);
                result.Columns.Add(dt.Columns[1].ColumnName);//增加第二列的值
                result.Columns.Add(dt.Columns[2].ColumnName);//增加第三列的值
                result.Columns.Add(dt.Columns[3].ColumnName);//增加第四列的值
                result.Columns.Add(dt.Columns[4].ColumnName);//增加第五列的值
                DataTable dtColumns = dt.DefaultView.ToTable("dtColumns", true, dt.Columns[5].ColumnName); //以第四列为字段建立交叉表列名
                for (int i = 0; i < dtColumns.Rows.Count; i++)
                {
                    string colName;
                    if (dtColumns.Rows[i][0] is DateTime)
                    {
                        colName = Convert.ToDateTime(dtColumns.Rows[i][0]).ToString();
                    }
                    else
                    {
                        colName = dtColumns.Rows[i][0].ToString();
                    }
                    result.Columns.Add(colName);
                    result.Columns[i + 5].DefaultValue = "0";// result.Columns[i + 1].DefaultValue = "0"; 因为前两列值一致,增加一个默认值
                }
                result.Columns.Add("Not_Input");
                DataRow drNew = result.NewRow();
                drNew[0] = dt.Rows[0][0];
                drNew[1] = dt.Rows[0][1]; //增加第二列的值
                drNew[2] = dt.Rows[0][2]; //增加第三列的值
                drNew[3] = dt.Rows[0][3]; //增加第三列的值
                drNew[4] = dt.Rows[0][4]; //增加第三列的值
                string rowName = drNew[0].ToString();
                foreach (DataRow dr in dt.Rows)
                {
                    string colName = dr[5].ToString(); //以第四列为字段建立交叉表列名
                    // double dValue = Convert.ToDouble(dr[4]);//以第五列值添加
                    string dValue = dr[6].ToString();
                    if ((dr[0].ToString().Equals(rowName, StringComparison.CurrentCultureIgnoreCase)) &&
                        (dr[1].ToString().Equals(drNew[1].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                        (dr[2].ToString().Equals(drNew[2].ToString(), StringComparison.CurrentCultureIgnoreCase)) &&
                        (dr[3].ToString().Equals(drNew[3].ToString(), StringComparison.CurrentCultureIgnoreCase))  &&
                        (dr[4].ToString().Equals(drNew[4].ToString(), StringComparison.CurrentCultureIgnoreCase)))
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
                        drNew[3] = dr[3];//增加第四列的值
                        drNew[4] = dr[4];//增加第五列的值
                        rowName = drNew[0].ToString();
                        drNew[colName] = dValue.ToString();
                    }
                }
                result.Rows.Add(drNew);
                return result;
            }
        }
        public DataTable GetCrossWipLineTable(DataTable dt)
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
                    if (dtColumns.Rows[i][0] is DateTime)
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


        private DataTable GetWorkInProcess(DataTable dtwip, int Flag)
        {
            ///0 无显示详细线体 1 有显示详细线体
            #region 获取详细工单
            DataView dataView = dtwip.DefaultView;
            DataTable dataTableDistinct = dataView.ToTable(true, "WOID");
            List<string> LsWo = new List<string>();
            foreach (DataRow dr in dataTableDistinct.Rows)
            {
                LsWo.Add(dr[0].ToString());
            }
            DataTable dtwoinfo = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWoInfo.Instance.GetTargetQtyAndNotInputQty(LsWo.ToArray()));
            #endregion


            DataView dv = dtwip.DefaultView;
            if (Flag == 0)
                dv.Sort = "WOID ASC";
            else
                dv.Sort = "WOID,LINE ASC";

            DataTable dTemp = dv.ToTable();

            DataTable dt = new DataTable("mydt");
            dt.Columns.Add("工单", typeof(string));
            dt.Columns.Add("产品料号", typeof(string));
            dt.Columns.Add("产品名称", typeof(string));
            dt.Columns.Add("工单套数", typeof(string));
            dt.Columns.Add("线别", typeof(string));
            dt.Columns.Add("途程", typeof(string));
            dt.Columns.Add("数量", typeof(string));

            foreach (DataRow dr in dTemp.Rows)
            {
                if (Flag == 0)
                    dt.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), "0", "ALL",dr[3].ToString(), dr[4].ToString());
                else
                    dt.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), "0", dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }

            DataTable dtwipData = GetCrossWipTable(dt);
            for (int x = 0; x < dtwipData.Rows.Count; x++)
            {
                DataTable dtswo = FrmBLL.publicfuntion.getNewTable(dtwoinfo, string.Format("WOID='{0}'", dtwipData.Rows[x][0].ToString()));
                string TargetQty = dtswo.Rows[0][1].ToString();
                string notInput = dtswo.Rows[0][2].ToString();
                dtwipData.Rows[x][3] = TargetQty;
                dtwipData.Rows[x][dtwipData.Columns.Count - 1] = notInput;
            }

            return dtwipData;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                Graphics gh = e.Graphics;
                Color cl = Color.Gainsboro;
                gh.FillRectangle(new SolidBrush(cl), e.CellBounds);
            }
        }


     
    }
}
