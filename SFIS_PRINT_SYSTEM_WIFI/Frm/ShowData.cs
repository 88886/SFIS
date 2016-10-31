using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SFIS_PRINT_SYSTEM_WIFI.Frm;

namespace SFIS_PRINT_SYSTEM_WIFI
{
    public partial class ShowData :Office2007Form// Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_frm">传进去的窗体</param>
        /// <param name="_dt">传进去的数据</param>
        /// <param name="bFlag">控件是否接受双击事件</param>
        public ShowData(Office2007Form _frm,DataTable _dt,bool bFlag)
        {
            InitializeComponent();
            mFrm = _frm;
            mdt = _dt;
            mFlag = bFlag;

        }
        /// <summary>
        /// 接收传进来的窗体
        /// </summary>
        private Office2007Form mFrm;
        /// <summary>
        /// 接收传进来的数据
        /// </summary>
        private DataTable mdt = null;
        /// <summary>
        /// 表示当前的datagridview控件是否接受双击事件
        /// </summary>
        private bool mFlag = false;
        private void ShowData_Load(object sender, EventArgs e)
        {
            this.dgvdata.DataSource = mdt;
            for (int i = 0; i < this.dgvdata.Columns.Count; i++)
            {
                this.cbwhere.Items.Add(this.dgvdata.Columns[i].Name.ToString());
            }
            this.cbwhere.Text = this.cbwhere.Items[0].ToString();
        }

        private void btselect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbvalue.Text))
            {
                this.dgvdata.DataSource = this.mdt;
            }
            else
            {
                DataTable _dt = SFIS_PRINT_SYSTEM_WIFI.publicfunction.getNewTable(this.mdt, string.Format("{0} like'{1}%'", this.cbwhere.Text, this.tbvalue.Text));
               if (_dt == null || _dt.Rows.Count < 1)
                   this.dgvdata.DataSource = null;
               else
                   this.dgvdata.DataSource = _dt;
            }
        }

        private void dgvdata_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            if (!this.mFlag)
                return;
            #region PrintMain
            if (this.mFrm is PrintMain)
            {
                (this.mFrm as PrintMain).MWoInfo = new WoInfo()
                {
                    WoId = this.dgvdata["woId", e.RowIndex].Value.ToString(),
                    PoId = this.dgvdata["poId", e.RowIndex].Value.ToString(),
                    Bomnumber = this.dgvdata["bomnumber", e.RowIndex].Value.ToString(),
                    Inputgroup = this.dgvdata["inputgroup", e.RowIndex].Value.ToString(),
                    Outputgroup = this.dgvdata["outputgroup", e.RowIndex].Value.ToString(),
                    Partnumber = this.dgvdata["partnumber", e.RowIndex].Value.ToString(),
                    Qty = int.Parse(this.dgvdata["qty", e.RowIndex].Value.ToString()),
                    RoutgroupId = this.dgvdata["routgroupId", e.RowIndex].Value.ToString(),
                    Bomver = this.dgvdata["bomver", e.RowIndex].Value.ToString(),
                    Wostate = int.Parse(this.dgvdata["wostate", e.RowIndex].Value.ToString()),
                    Cpwd = this.Getcpwd(this.dgvdata["cpwd", e.RowIndex].Value.ToString()),
                    Wotype = this.dgvdata["wotype", e.RowIndex].Value.ToString(),
                  //  ProductLine = this.dgvdata["LINEID", e.RowIndex].Value.ToString(),
                };
                (this.mFrm as PrintMain).tbwoid.Text = this.dgvdata["woId", e.RowIndex].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
            #endregion
        }
        private  WoInfo.Ecpwd Getcpwd(string cpwd)
        {
            WoInfo.Ecpwd _cpwd;
            switch (cpwd.ToUpper())
            {
                case "PROG":
                    _cpwd=WoInfo.Ecpwd.Prog; //Entity.T_WO_INFO.ecpwd.PROG;
                    break;
                case "FILE":
                    _cpwd = WoInfo.Ecpwd.File;
                    break ;
                case "USERDEF":
                    _cpwd = WoInfo.Ecpwd.Userdef;
                    break;
                default:
                    _cpwd = WoInfo.Ecpwd.Prog;
                    break;
            }
            return _cpwd;
        }
        private void tbvalue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.btselect_Click(sender, null);
            }
        }
    }
}
