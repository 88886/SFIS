using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SFIS_V2
{
    public partial class Frm_Duty : Office2007Form //Form
    {
        public Frm_Duty(Office2007Form Frm, DataTable dt)
        {
            InitializeComponent();
            mdt = dt;
            mFrm = Frm;
        }
       
         DataTable mdt;
        Office2007Form mFrm;
        private void Frm_Duty_Load(object sender, EventArgs e)
        {
            dgvduty.DataSource = mdt;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void imbt_search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_duty.Text))
            {
                dgvduty.DataSource = mdt;
            }
            else
            {
                dgvduty.DataSource = FrmBLL.publicfuntion.getNewTable(mdt, string.Format("DUTY='{0}'", tb_duty.Text.Trim()));
            }

            tb_duty.SelectAll();
        }

        private void tb_duty_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Enter)
            {              
                imbt_search_Click(null, null);
            }
        }

        private void dgvduty_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (mFrm is Frm_Update)
                {
                    (mFrm as Frm_Update).Duty = dgvduty.Rows[e.RowIndex].Cells["DUTY"].Value.ToString();
                    (mFrm as Frm_Update).DutyDesc = dgvduty.Rows[e.RowIndex].Cells["DUTYDESC"].Value.ToString();
                    (mFrm as Frm_Update).ShowDuty();
                    this.DialogResult = DialogResult.OK;
                }
            }

        }
    }
}
