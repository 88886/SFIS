using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Stocking_In
{
    public partial class Frm_StationName : Office2007Form //Form
    {
        public Frm_StationName(Office2007Form Frm)
        {
            InitializeComponent();
            sFrm = Frm;
        }
        Office2007Form sFrm;
        private void Frm_StationName_Load(object sender, EventArgs e)
        {
            cb_line.Items.Clear();
            List<string> LsLine =OperateDB.Get_Line_List();

            foreach (string str in LsLine)
            {
                cb_line.Items.Add(str);
            }
            cb_line.SelectedIndex = 0;

            dgvstation.DataSource = OperateDB.Get_All_Station();

        }

        private void dgvstation_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {

            }
        }

        private void dgvstation_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txt_section.Text = dgvstation.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_group.Text = dgvstation.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_station.Text = dgvstation.Rows[e.RowIndex].Cells[2].Value.ToString();
             
            }
        }

        private void imbt_ok_Click(object sender, EventArgs e)
        {
            (sFrm as Frm_StockingIn).Line_Name = cb_line.Text;
            (sFrm as Frm_StockingIn).MYGROUP = txt_group.Text;
            (sFrm as Frm_StockingIn).Station = txt_station.Text;
            (sFrm as Frm_StockingIn).Section = txt_section.Text;
            (sFrm as Frm_StockingIn).SetStation();
            this.DialogResult = DialogResult.OK;
        }

        private void imbt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkLine_Click(object sender, EventArgs e)
        {             
            cb_line.Enabled = chkLine.Checked;  
        }
    }
}
