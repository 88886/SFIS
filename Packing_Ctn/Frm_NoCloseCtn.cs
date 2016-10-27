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
    public partial class Frm_NoCloseCtn : Office2007Form //Form
    {
        public Frm_NoCloseCtn()
        {
            InitializeComponent();
        }

        private void Frm_NoCloseCtn_Load(object sender, EventArgs e)
        {
            lv_Carton.Columns.Clear();
            lv_Carton.Columns.Add("NUM", "NUM");
            lv_Carton.Columns.Add("WOID", "WOID");
            lv_Carton.Columns.Add("PALLETNUMBER", "CARTON_NO");
            lv_Carton.Columns.Add("LINE", "LINE");
            lv_Carton.Columns.Add("PARTNUMBER", "PARTNUMBER");
            lv_Carton.Columns.Add("CLOSE_FLAG", "CLOSE_FLAG");
            lv_Carton.Columns.Add("COMPUTER", "COMPUTER");
          
        }

        private void tb_wo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_wo.Text) && e.KeyCode==Keys.Enter)
            {
                DataTable dtNoCloseCtn = ReleaseData.arrByteToDataTable(refWebtPalletInfo.Instance.Get_Pallet_Info_bywo(tb_wo.Text, 1, 0));
                this.lv_Carton.Items.Clear();
                int x = 1;
                foreach (DataRow dr in dtNoCloseCtn.Rows)
                {
                    ListViewItem lvi = new ListViewItem();
                    this.lv_Carton.View = System.Windows.Forms.View.Details;
                    lvi.SubItems[0].Text = x++.ToString();
                    lvi.SubItems.AddRange(new string[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[6].ToString(), dr[7].ToString()});
                    this.lv_Carton.Items.Add(lvi);
                    lv_Carton.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize); 
                }
                tb_wo.SelectAll();
            }
        }
    }
}
