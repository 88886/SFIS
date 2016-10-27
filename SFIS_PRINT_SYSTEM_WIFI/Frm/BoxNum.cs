using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SFIS_PRINT_SYSTEM_WIFI
{
    public partial class BoxNum : Form
    {
        public BoxNum(PrintMain _mfrm)
        {
            InitializeComponent();
            mFrm = _mfrm;
        }
        PrintMain mFrm;
        private void BoxNum_Load(object sender, EventArgs e)
        {

        }

        private void tb_box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.mFrm.strBoxNumber = this.tb_box.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void tb_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
