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
    public partial class MyContextMenu :Office2007Form// Form
    {
        public MyContextMenu(CreateRoute crg,DataTable dt)
        {
            InitializeComponent();
            mdatatable = dt;
            mcgr = crg;
        }
        CreateRoute mcgr;
        private DataTable mdatatable = new DataTable();

        private void MyContextMenu_Load(object sender, EventArgs e)
        {
            this.dgv_craftitemparamet.DataSource = this.mdatatable;
        }
        /// 工艺项目参数添加类型(1:从数据库中调用参考参数(ATUOADD)和手动修改添加(notautoadd))
        /// </summary>
        private enum ParametAddType
        {
            AutoAdd,
            NotAutoAdd
        }
        ParametAddType PAddType;
        private void dgv_craftitemparamet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
                this.dgv_craftitemparamet[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Black;
        }
        private void dgv_craftitemparamet_MouseEnter(object sender, EventArgs e)
        {
            this.PAddType = ParametAddType.NotAutoAdd;
        }
        private void dgv_craftitemparamet_MouseLeave(object sender, EventArgs e)
        {
            if (dgv_craftitemparamet.DataSource == null)
                return;
            this.PAddType = ParametAddType.AutoAdd;
        }
        private void dgv_craftitemparamet_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (this.PAddType == ParametAddType.NotAutoAdd)
            {
                for (int x = 2; x < this.dgv_craftitemparamet.Columns.Count; x++)
                {
                    this.dgv_craftitemparamet[x, this.dgv_craftitemparamet.RowCount - 1].Value = "N/A";
                    this.dgv_craftitemparamet[x, this.dgv_craftitemparamet.RowCount - 1].Style.ForeColor = Color.Red;
                }
                for (int i = 1; i < this.dgv_craftitemparamet.RowCount; i++)
                {
                    this.dgv_craftitemparamet["CraftItem", i - 1].Value = i.ToString();
                    this.dgv_craftitemparamet["CraftId", i - 1].Value = this.dgv_craftitemparamet["CraftId", 0].Value.ToString();
                    this.dgv_craftitemparamet["other", i - 1].Value = "N/A";
                    this.dgv_craftitemparamet["other", i - 1].Style.ForeColor = Color.Red;
                }
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            this.mcgr.gdatatable = new DataTable("a");
            this.mcgr.gdatatable = (this.dgv_craftitemparamet.DataSource as DataTable).Clone();

            for (int i = 0; i < this.dgv_craftitemparamet.Rows.Count; i++)
            {
                //if (this.dgv_craftitemparamet["craftId", i] != null && !string.IsNullOrEmpty(this.dgv_craftitemparamet["craftId", i].Value.ToString()))
                //{
                if (this.dgv_craftitemparamet.Rows[i] != null && 
                     this.dgv_craftitemparamet.Rows[i].Cells[1] != null && 
                     this.dgv_craftitemparamet.Rows[i].Cells[0].Value != null && 
                    !string.IsNullOrEmpty(this.dgv_craftitemparamet.Rows[i].Cells[0].Value.ToString()))
                {
                    this.mcgr.gdatatable.Rows.Add(
                        this.dgv_craftitemparamet[0, i].Value.ToString(),
                        this.dgv_craftitemparamet[1, i].Value.ToString(),
                        this.dgv_craftitemparamet[2, i].Value.ToString(),
                        this.dgv_craftitemparamet[3, i].Value.ToString(),
                        this.dgv_craftitemparamet[4, i].Value.ToString(),
                        this.dgv_craftitemparamet[5, i].Value.ToString());
                }
            }
            
            this.mcgr.gdatatable = this.dgv_craftitemparamet.DataSource as DataTable;
            this.DialogResult = DialogResult.OK;
        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

    }
}
