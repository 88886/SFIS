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
    public partial class SelectDataListBox : Office2007Form//Form
    {
        public SelectDataListBox(Office2007Form _Frm, string FrmSelect,DataTable mdt)
        {
            InitializeComponent();
            mFrm = _Frm;
            mFrmSelect = FrmSelect;
            dt = mdt;
        }

        Office2007Form mFrm;
        DataTable dt;
        string mFrmSelect;
        /// <summary>
        /// 选中项
        /// </summary>
        System.Collections.ArrayList ls = new System.Collections.ArrayList();
        Dictionary<string, string> CraftList = new Dictionary<string, string>();
        private List<string> lsNames = new List<string>();
        /// <summary>
        /// 产品名称表
        /// </summary>
        DataTable dtProduct = null;
        private void SelectDataListBox_Load(object sender, EventArgs e)
        {
            try
            {
                if (mFrm is QualityManagement)
                {
                    listAllData.Items.Clear();

                    if ((mFrm as QualityManagement).sortname == mFrmSelect)
                    {
                       
                        DataView dataView = dt.DefaultView;
                        DataTable dataTableDistinct = dataView.ToTable(true, "sortname");//注：其中ToTable（）的第一个参数为是否DISTINCT

                        foreach (DataRow dr in dataTableDistinct.Rows)
                        {
                            listAllData.Items.Add(dr[0].ToString());
                        }
                    }

                    if ((mFrm as QualityManagement).MODEL == mFrmSelect)
                    {
                        DataTable dtModel=null;
                        if ((mFrm as QualityManagement).listSortname.Items.Count == 1)
                        {
                            if ((mFrm as QualityManagement).listSortname.Items[0].ToString()== "ALL")
                                dtModel = dt;
                            else
                            {                                
                                    dtModel = FrmBLL.publicfuntion.getNewTable(dt, string.Format("sortname ='{0}' ", (mFrm as QualityManagement).listSortname.Items[0]));
                            }
                        }
                        else
                        {
                            string Col = string.Empty;
                            foreach (string str in (mFrm as QualityManagement).listSortname.Items)
                            {
                                Col += str + "','";
                            }
                            Col ="'"+ Col.Substring(0, Col.Length - 3)+"'";
                            dtModel = FrmBLL.publicfuntion.getNewTable(dt, " sortname in (" + Col +")");

                        }
                        foreach (DataRow dr in FrmBLL.publicfuntion.DataTableToSort(dtModel, dtModel.Columns[0].ColumnName).Rows)
                        {
                            listAllData.Items.Add(dr[0].ToString() + "-" + dr[2].ToString());
                        }
                    }
                    if ((mFrm as QualityManagement).woId == mFrmSelect)
                    {

                        foreach (DataRow dr in FrmBLL.publicfuntion.DataTableToSort(dt, dt.Columns[0].ColumnName).Rows)
                        {
                            listAllData.Items.Add(dr[0].ToString());
                        }
                    }
                    if ((mFrm as QualityManagement).LINE == mFrmSelect)
                    {

                        foreach (DataRow dr in FrmBLL.publicfuntion.DataTableToSort(dt, dt.Columns[0].ColumnName).Rows)
                        {
                            listAllData.Items.Add(dr[0].ToString());
                        }
                    }

                    if ((mFrm as QualityManagement).ROUTE == mFrmSelect)
                    {
                        CraftList.Clear();     
                        
                        foreach (DataRow dr in FrmBLL.publicfuntion.DataTableToSort(dt,dt.Columns[1].ColumnName).Rows)
                        {
                            if (!listAllData.Items.Contains(dr[1].ToString()))
                            {
                                listAllData.Items.Add(dr[1].ToString());
                                CraftList.Add(dr[1].ToString(), dr[0].ToString());
                              
                            }
                        }
                    }

                }

                if (mFrm is FrmRepair)
                {              

                    dtProduct = FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetAllProduct());    
                    DataView dv = dtProduct.DefaultView;
                    DataTable dts = dv.ToTable(true, "sortname");
                    foreach (DataRow dr in dts.Rows)
                    {
                        listAllData.Items.Add(dr[0].ToString());
                    }

                }

                for (int i = 0; i < this.listAllData.Items.Count; i++)
                {
                    this.lsNames.Add(this.listAllData.Items[i].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "错误!!");
            }

        }

        private void imbt_left_Click(object sender, EventArgs e)
        {
            ls.Clear();
            for (int i = 0; i < this.listAllData.SelectedItems.Count; i++)
            {
                this.listSelect.Items.Add(listAllData.SelectedItems[i].ToString());
                ls.Add(listAllData.SelectedItems[i].ToString());
            }

            for (int x = 0; x < ls.Count; x++)
            {
                this.listAllData.Items.Remove(ls[x].ToString());
                this.lsNames.Remove(ls[x].ToString());
            }
        }

        private void imbt_rightall_Click(object sender, EventArgs e)
        {
            if (listSelect.Items.Count != 0)
            {
                for (int i = 0; i < listSelect.Items.Count; i++)
                {
                    listAllData.Items.Add(listSelect.Items[i].ToString());
                    this.lsNames.Add(listSelect.Items[i].ToString());
                }
                listSelect.Items.Clear();
            }
        }

        private void imbt_leftall_Click(object sender, EventArgs e)
        {
            if (listAllData.Items.Count != 0)
            {
                for (int i = 0; i < listAllData.Items.Count; i++)
                {
                    listSelect.Items.Add(listAllData.Items[i].ToString());
                    this.lsNames.Remove(listAllData.Items[i].ToString());
                }
                listAllData.Items.Clear();
            }
        }

        private void imbt_right_Click(object sender, EventArgs e)
        {
            ls.Clear();
            for (int i = 0; i < this.listSelect.SelectedItems.Count; i++)
            {
                this.listAllData.Items.Add(listSelect.SelectedItems[i].ToString());
                this.lsNames.Add(this.listSelect.SelectedItems[i].ToString());
                ls.Add(listSelect.SelectedItems[i].ToString());
            }

            for (int x = 0; x < ls.Count; x++)
            {
                this.listSelect.Items.Remove(ls[x].ToString());
            }
        }

        private void imbt_submit_Click(object sender, EventArgs e)
        {
            if (mFrm is QualityManagement)
            {
                //string DataSelect = "";
                if (listSelect.Items.Count == 0)
                {
                    listSelect.Items.Add("ALL");
                }
             /*   else
                {
                    #region 解析选择的资料
                    if ((mFrm as QualityManagement).MODEL == mFrmSelect)
                    {
                        for (int j = 0; j < listSelect.Items.Count; j++)
                        {
                            string[] arr = new string[10];
                            arr = listSelect.Items[j].ToString().Split('-');

                            if (j == listSelect.Items.Count - 1)
                            {
                                DataSelect = DataSelect + "'" + arr[0] + "'";
                            }
                            else
                            {
                                DataSelect = DataSelect + "'" + arr[0] + "',";
                            }
                        }
                    }
                    else
                    {
                        if ((mFrm as QualityManagement).ROUTE == mFrmSelect)
                        {
                            for (int j = 0; j < listSelect.Items.Count; j++)
                            {
                                if (j == listSelect.Items.Count - 1)
                                {
                                    DataSelect = DataSelect + "'" + CraftList[listSelect.Items[j].ToString()] + "'";
                                }
                                else
                                {
                                    DataSelect = DataSelect + "'" + CraftList[listSelect.Items[j].ToString()] + "',";
                                }
                            }
                        }
                        else
                        {
                            if (((mFrm as QualityManagement).LINE == mFrmSelect) || ((mFrm as QualityManagement).woId == mFrmSelect))
                            {
                                for (int j = 0; j < listSelect.Items.Count; j++)
                                {
                                    if (j == listSelect.Items.Count - 1)
                                    {
                                        DataSelect = DataSelect + "'" + listSelect.Items[j].ToString() + "'";
                                    }
                                    else
                                    {
                                        DataSelect = DataSelect + "'" + listSelect.Items[j].ToString() + "',";
                                    }
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(DataSelect))
                    DataSelect = DataSelect.Substring(0, DataSelect.Length - 1);
                }
                    #endregion
                */
                if (mFrm is QualityManagement)
                {
                    if ((mFrm as QualityManagement).sortname == mFrmSelect)
                    {
                       // (mFrm as QualityManagement).SelectMode = DataSelect;
                        (mFrm as QualityManagement).listSortname.Items.Clear();
                        (mFrm as QualityManagement).listSortname.Items.AddRange(this.listSelect.Items);
                        #region  选择完成后加载料号
                        DataTable dtModel = null;
                        if ((mFrm as QualityManagement).listSortname.Items.Count == 1)
                        {
                            if ((mFrm as QualityManagement).listSortname.Items[0] == "ALL")
                                dtModel = dt;
                            else                            
                                dtModel = FrmBLL.publicfuntion.getNewTable(dt, string.Format("sortname ='{0}' ", (mFrm as QualityManagement).listSortname.Items[0]));
                            
                        }
                        else
                        {
                            string Col = string.Empty;
                            foreach (string str in (mFrm as QualityManagement).listSortname.Items)
                            {
                                Col += str + "','";
                            }
                            Col = "'" + Col.Substring(0, Col.Length - 3) + "'";
                            dtModel = FrmBLL.publicfuntion.getNewTable(dt, " sortname in (" + Col + ")");

                        }
                        (mFrm as QualityManagement).ListMode.Items.Clear();
                        foreach (DataRow dr in FrmBLL.publicfuntion.DataTableToSort(dtModel, dtModel.Columns[0].ColumnName).Rows)
                        {
                            (mFrm as QualityManagement).ListMode.Items.Add(dr[0].ToString() + "-" + dr[2].ToString());
                        }

                        #endregion

                        this.DialogResult = DialogResult.OK;
                        return;
                    }
                    if ((mFrm as QualityManagement).MODEL == mFrmSelect)
                    {
                       // (mFrm as QualityManagement).SelectMode = DataSelect;
                        (mFrm as QualityManagement).ListMode.Items.Clear();
                        (mFrm as QualityManagement).ListMode.Items.AddRange(this.listSelect.Items);
                        this.DialogResult = DialogResult.OK;
                        return;
                    }
                    if ((mFrm as QualityManagement).woId == mFrmSelect)
                    {
                      //  (mFrm as QualityManagement).SelectwoId = DataSelect;
                        (mFrm as QualityManagement).Listwo.Items.Clear();
                        (mFrm as QualityManagement).Listwo.Items.AddRange(this.listSelect.Items);
                        this.DialogResult = DialogResult.OK;
                        return;
                    }
                    if ((mFrm as QualityManagement).LINE == mFrmSelect)
                    {
                      //  (mFrm as QualityManagement).SelectLine = DataSelect;
                        (mFrm as QualityManagement).ListLine.Items.Clear();
                        (mFrm as QualityManagement).ListLine.Items.AddRange(this.listSelect.Items);
                        this.DialogResult = DialogResult.OK;
                        return;
                    }
                    if ((mFrm as QualityManagement).ROUTE == mFrmSelect)
                    {
                      //  (mFrm as QualityManagement).SelectRoute = DataSelect;
                        (mFrm as QualityManagement).ListRoute.Items.Clear();
                        (mFrm as QualityManagement).ListRoute.Items.AddRange(this.listSelect.Items);
                        this.DialogResult = DialogResult.OK;
                        return;
                    }
                }
            }
            if (mFrm is FrmRepair)
            {
                DataTable dtTemp = new DataTable("mydt");
                dtTemp.Columns.Add("partnumber", typeof(string));


                if (listSelect.Items.Count == 0)
                {
                    (mFrm as FrmRepair).dtProduct = null;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    for (int x = 0; x < listSelect.Items.Count; x++)
                    {
                        DataTable dt = FrmBLL.publicfuntion.getNewTable(dtProduct, string.Format("sortname='{0}'", listSelect.Items[x].ToString()));
                        foreach (DataRow dr in dt.Rows)
                        {
                            dtTemp.Rows.Add(dr[1].ToString());
                        }
                    }
                    (mFrm as FrmRepair).dtProduct = dtTemp;
                    this.DialogResult = DialogResult.OK;
                }

            }       

        }

        private DataTable GetPartNumber()
        {
            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            string sSQL = "select partnumber,sortname,productname,productcolor,productdesc,other from tProduct";
            return ass.GetDatatable(sSQL);

        }
        private DataTable GetWoIdList()
        {
            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            string sSQL = "select woId from tWorkOrderInfo";
            return ass.GetDatatable(sSQL);
        }
        private DataTable GetLineList()
        {
            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            string sSQL = "select * from tLineInfo";
            return ass.GetDatatable(sSQL);
        }
        private DataTable GetCraftList()
        {
            FrmBLL.cdbAccess ass = new FrmBLL.cdbAccess();
            string sSQL = "select craftId,craftname,craftparameterurl,beworkseg from tCraftInfo";
            return ass.GetDatatable(sSQL);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.listAllData.Items.Clear();
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                this.listAllData.Items.AddRange(this.lsNames.ToArray());
                return;
            }
            for (int i = 0; i < this.lsNames.Count; i++)
            {
                if (lsNames[i].ToUpper().IndexOf(this.textBox1.Text.Trim().ToUpper()) != -1)
                {
                    this.listAllData.Items.Add(lsNames[i]);
                }
            }
        }
    }
}
