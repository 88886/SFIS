using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SFIS_PRINT_SYSTEM_WIFI
{
    public partial class ShowCartonData : Office2007Form// Form
    {
        public ShowCartonData(PrintMain pm,DataTable _dt)
        {
            InitializeComponent();
            this.mFrm = pm;
            mdt = _dt;
        }
        private PrintMain mFrm = null;
        private DataTable mdt = new DataTable();
        private void ShowCartonData_Load(object sender, EventArgs e)
        {
            this.dgvShowData.DataSource = mdt;
        }

        private void dgvShowData_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            if (!clkchk)
            {
                MessageBoxEx.Show(string.Format("选中数据所在的工单[{0}]与设置的工单[{1}]不符!!",
                    this.dgvShowData["woId", e.RowIndex].Value.ToString().Trim().ToUpper(), this.mFrm.mWoInfo.woId));
                return;
            }
            this.mFrm.gCartonInfo.woId = this.dgvShowData["woId", e.RowIndex].Value.ToString();
            this.mFrm.gCartonInfo.cartonId= this.dgvShowData["cartonId", e.RowIndex].Value.ToString();
            this.mFrm.gCartonInfo.lineId = this.dgvShowData["lineId", e.RowIndex].Value.ToString();
            this.mFrm.gCartonInfo.mcartonnumber = this.dgvShowData["cartonnumber", e.RowIndex].Value.ToString();
            this.mFrm.gCartonInfo.number = int.Parse(this.dgvShowData["num", e.RowIndex].Value.ToString());
            this.DialogResult = DialogResult.Yes;
        }
        private bool clkchk = false;
        private void dgvShowData_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                for (int i = 0; i < this.dgvShowData.Rows.Count; i++)
                {
                    this.dgvShowData.Rows[i].Selected = false;
                }
                this.dgvShowData.Rows[e.RowIndex].Selected = true;

                if (this.mFrm.mWoInfo.woId.ToUpper() != this.dgvShowData["woId", e.RowIndex].Value.ToString())
                {
                    this.dgvShowData[e.ColumnIndex, e.RowIndex].ToolTipText = string.Format("选中数据所在的工单[{0}]与设置的工单[{1}]不符\n是否切换工单!!",
                    this.dgvShowData["woId", e.RowIndex].Value.ToString().Trim().ToUpper(), this.mFrm.mWoInfo.woId);
                    this.clkchk = false;
                }
                else
                    this.clkchk = true;
            }
        }
    }
}
