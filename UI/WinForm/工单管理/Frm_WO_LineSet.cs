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
    public partial class Frm_WO_LineSet : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_WO_LineSet(Office2007Form Frm,int sFlag)
        {
            InitializeComponent();
            mFrm = Frm;
            Flag = sFlag;
        }
        Office2007Form mFrm;
        int Flag;
        DataTable dt_All_wo = null;
        public string UserId = string.Empty;
        public string UserName = string.Empty;
        private void Frm_WO_LineSet_Load(object sender, EventArgs e)
        {
            UserId = (mFrm as Frm_MO_Manage).UserId;
            UserName = (mFrm as Frm_MO_Manage).UserName;
            GetAllWoInfo();
            FillDataGridView_MO();
            this.dgv_woinfo.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgv_woinfo.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            txt_woid.Focus();
        }
        /// <summary>
        /// 获取全部工单
        /// </summary>
        private void GetAllWoInfo()
        {
            dt_All_wo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetAllWoInfo());
        }
        /// <summary>
        /// 填充工单到DataGridView
        /// </summary>
        private void FillDataGridView_MO()
        {
            DataTable dt = dt_All_wo;
            dgv_woinfo.DataSource = dt;           
        }

        private void txt_woid_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_woid.Text) && e.KeyCode == Keys.Enter)
            {
                FrmBLL.publicfuntion.SelectDataGridViewRows(txt_woid.Text, dgv_woinfo, 0);
                txt_woid.SelectAll();
                }
        }

        private void dgv_woinfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FrmBLL.publicfuntion.UpdateFieldName(dgv_woinfo);
        }
    }
}