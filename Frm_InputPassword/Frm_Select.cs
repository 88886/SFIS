using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Frm_Public
{
    public partial class Frm_Select : Form
    {
        public Frm_Select(DataTable _dt,ref Dictionary<string,object> dic)
        {
            InitializeComponent();
            mDatatable = _dt;
            mDic = dic;
        }

        DataTable mDatatable = null;
        Dictionary<string, object> mDic = null;
        private void Frm_Select_Load(object sender, EventArgs e)
        {
            this.dgv_showdata.DataSource = mDatatable;
        }

        private void cbx_selecttype_DropDown(object sender, EventArgs e)
        {
            this.cbx_selecttype.Items.Clear();
            for (int i = 0; i < this.mDatatable.Columns.Count; i++)
            {
                this.cbx_selecttype.Items.Add(this.mDatatable.Columns[i].ColumnName.ToString());
            }
        }

        private void txt_selectvalues_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_selectvalues.Text))
            {
                this.dgv_showdata.DataSource = this.mDatatable;
            }
            else
            {
                this.dgv_showdata.DataSource = getNewTable(this.mDatatable, string.Format("{0}='{1}'",
                    this.cbx_selecttype.Text.Trim(), this.txt_selectvalues.Text.Trim()));
            }
        }
        /// <summary>
        /// 返回一个新的datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable getNewTable(DataTable dt, string sql)
        {
            try
            {
                DataTable mydt = new DataTable();
                mydt = dt.Clone();
                DataRow[] arrDr = dt.Select(sql);
                for (int i = 0; i < arrDr.Length; i++)
                {
                    mydt.ImportRow((DataRow)arrDr[i]);
                }
                return mydt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dgv_showdata_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            mDic.Clear();
            for (int x = 0; x < dgv_showdata.ColumnCount; x++)
            {
                mDic.Add(dgv_showdata.Columns[x].Name, dgv_showdata.Rows[e.RowIndex].Cells[x].Value.ToString());
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
