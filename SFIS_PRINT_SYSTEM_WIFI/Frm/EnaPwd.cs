using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SFIS_PRINT_SYSTEM_WIFI
{
    public partial class EnaPwd : Form
    {
        public EnaPwd(PrintMain mFrm)
        {
            InitializeComponent();
            _frm = mFrm;
        }
        PrintMain _frm;
        private void EnaPwd_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this._frm.strEnaPwd = this.textBox1.Text.Trim();
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
