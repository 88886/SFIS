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
    public partial class Frm_SelcetNextStation : Office2007Form //Form
    {
        public Frm_SelcetNextStation(Office2007Form Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Office2007Form mFrm;
        private void Frm_SelcetNextStation_Load(object sender, EventArgs e)
        {
            cb_NextStation.Items.Clear();
            foreach (string Str in (mFrm as Frm_RepairMain).Ls_M_sTheNextGroup)
            {
                cb_NextStation.Items.Add(Str);
            }
            cb_NextStation.SelectedIndex = 0;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
        }

        private void imbt_OK_Click(object sender, EventArgs e)
        {
            SelectStation();
        }
        private void SelectStation()
        {
            (mFrm as Frm_RepairMain).M_sTheNextGroup = cb_NextStation.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void Frm_SelcetNextStation_FormClosing(object sender, FormClosingEventArgs e)
        {
            SelectStation();
        }
    }
}
