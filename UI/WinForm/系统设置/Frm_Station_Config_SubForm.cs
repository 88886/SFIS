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
    public partial class Frm_Station_Config_SubForm : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Station_Config_SubForm(Office2007Form Frm ,  string FrmText)
        {
            InitializeComponent();
            mFrm = Frm;
            mFrmText = FrmText;
          
        }
        Office2007Form mFrm;
        string mFrmText;
        private void Frm_Station_Config_SubForm_Load(object sender, EventArgs e)
        {
            if (mFrm is Frm_Station_Config)
            {
                this.Text = mFrmText;
                cbx_selectdata.Items.AddRange((mFrm as Frm_Station_Config).lsData.ToArray());
            }
        }

        private void cbx_selectdata_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void imbt_ok_Click(object sender, EventArgs e)
        {
            if (mFrm is Frm_Station_Config)
            {
                (mFrm as Frm_Station_Config).SelectData = cbx_selectdata.Text;
            }
            DialogResult = DialogResult.OK;
        }

        private void imbt_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cbx_selectdata_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(cbx_selectdata.Text))
            {

                if (!cbx_selectdata.Items.Contains(cbx_selectdata.Text))
                {
                    MessageBox.Show("请输入正确的信息");
                    cbx_selectdata.Focus();
                }
            }
        }
    }
}