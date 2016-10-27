using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ColorBoxPrint
{
    public partial class Frm_RePrint : Form
    {
        public Frm_RePrint(Frm_BoxPrint Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }

        Frm_BoxPrint mFrm;
        private void Frm_RePrint_Load(object sender, EventArgs e)
        {

        }

        private void imbt_close_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void tb_data_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_data.Text) && e.KeyCode == Keys.Enter)
            {
                string sColnum = string.Empty;
                string StrErr = string.Empty;
              
                if (mFrm.rdesn.Checked)
                    sColnum = "ESN";
                if (mFrm.rdIMEI.Checked)
                    sColnum = "IMEI";

                DataTable dt = ReleaseData.arrByteToDataTable(mFrm.tWipTrack.GetQueryWipAllInfo(sColnum, tb_data.Text));
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["WOID"].ToString() == mFrm.tb_wo.Text)
                    {
                        mFrm.PrintLabel(dt.Rows[0]["ESN"].ToString());
                    }
                    else
                    {
                        MessageBox.Show(string.Format("工单不同{0}≠{1}",dt.Rows[0]["WOID"].ToString(), mFrm.tb_wo.Text));
                    }
                }
                else
                    MessageBox.Show("NO DATA");

                tb_data.Text = "";
            }
        }
    }
}
