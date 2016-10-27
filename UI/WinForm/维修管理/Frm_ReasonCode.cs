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
    public partial class Frm_ReasonCode : Office2007Form //Form
    {
        public Frm_ReasonCode( Office2007Form Frm, DataTable dt)
        {
            InitializeComponent();
            mdt = dt;
            mFrm = Frm;
        }

        DataTable mdt;
        Office2007Form mFrm;
        private void Frm_ReasonCode_Load(object sender, EventArgs e)
        {
            dgvReasonCode.DataSource = mdt;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void dgvReasonCode_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (mFrm is Frm_Update)
                {
                    (mFrm as Frm_Update).ReasonCode = dgvReasonCode.Rows[e.RowIndex].Cells["REASONCODE"].Value.ToString();
                    (mFrm as Frm_Update).ReasonCodeDesc = dgvReasonCode.Rows[e.RowIndex].Cells["REASONDESC"].Value.ToString();
                    (mFrm as Frm_Update).ShowReasonInfo();
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void imbt_search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_reasoncode.Text))
            {
                dgvReasonCode.DataSource = mdt;
            }
            else
            {
                dgvReasonCode.DataSource = FrmBLL.publicfuntion.getNewTable(mdt, string.Format("REASONCODE='{0}'",tb_reasoncode.Text.Trim()));
            }
            tb_reasoncode.SelectAll();
        }

        private void tb_reasoncode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                imbt_search_Click(null,null);
            }
        }
    }
}
