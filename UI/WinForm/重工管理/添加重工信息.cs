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
    public partial class FrmReworkInfo : Office2007Form //Form
    {
        public FrmReworkInfo(Office2007Form sInfo)
        {
            InitializeComponent();
            mFrm = sInfo;
        }
       // FrmReworkProduction
        Office2007Form mFrm;
        private void FrmReworkInfo_Load(object sender, EventArgs e)
        {
            if (mFrm is Frm_ReworkPD)
            {               
                this.tb_woid.Text = (mFrm as Frm_ReworkPD).Rework_WOID;
                this.tb_partnumber.Text = (mFrm as Frm_ReworkPD).Rework_Partnumber;
                this.tb_memo.Text = string.Empty;
                this.tb_RewDesc.Text = string.Empty;
            }
        }

        private void imbt_exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void imbt_OK_Click(object sender, EventArgs e)
        {          
            if (mFrm is Frm_ReworkPD)
            {
                (mFrm as Frm_ReworkPD).Rework_WOID = string.IsNullOrEmpty(tb_woid.Text) ? "NA" : tb_woid.Text;
                (mFrm as Frm_ReworkPD).Rework_Partnumber = string.IsNullOrEmpty(tb_partnumber.Text) ? "NA" : tb_partnumber.Text;
                (mFrm as Frm_ReworkPD).Rework_MEMO = string.IsNullOrEmpty(tb_memo.Text) ? "NA" : tb_memo.Text;
                (mFrm as Frm_ReworkPD).Rework_DESC = string.IsNullOrEmpty(tb_RewDesc.Text) ? "NA" : tb_RewDesc.Text;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
