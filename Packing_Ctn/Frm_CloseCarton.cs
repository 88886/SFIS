using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Packing_Ctn
{
    public partial class Frm_CloseCarton : Office2007Form //Form
    {
        public Frm_CloseCarton(Frm_Packing_Ctn Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Frm_Packing_Ctn mFrm;
        private void Frm_CloseCarton_Load(object sender, EventArgs e)
        {
           
        }

        private void txt_data_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                imbt_OK_Click(null,null);
            }
        }

        private void imbt_OK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_data.Text))
            {
                string _Colnum = string.Empty;
                if (radESN.Checked)
                    _Colnum = "ESN";
               if (radSN.Checked)
                   _Colnum = "SN";
               if (radIMEI.Checked)
                   _Colnum = "IMEI";
               if (radCarton.Checked)
                   _Colnum = "CARTONNUMBER";
               DataTable dt = ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo(_Colnum,txt_data.Text));
               if (dt.Rows.Count > 0)
               {

                   mFrm.Close_Carton(dt.Rows[0]["CARTONNUMBER"].ToString());
                 //  mFrm.PrintCartonLabel_CodeSoft(dt.Rows[0]["CARTONNUMBER"].ToString());
                   txt_data.Text = string.Empty;
                   txt_data.Focus();
                   MessageBox.Show("This Carton Already Closed");
               }
               else
               {
                   txt_data.SelectAll();
                   MessageBox.Show("NO DATA");
               }
              
            }
                
        }

        private void imbt_abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
