using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Packing_Ctn
{
    public partial class Frm_StationName : Office2007Form //Form
    {
        public Frm_StationName(Frm_Packing_Ctn Frm)
        {
            InitializeComponent();
            sFrm = Frm;
        }
        Frm_Packing_Ctn sFrm;
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
            sFrm.Line_Name = cb_line.Text;
            sFrm.MYGROUP = txt_group.Text;
            sFrm.Station=txt_station.Text;
            sFrm.Section = txt_section.Text;
            sFrm.SetStation();
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
