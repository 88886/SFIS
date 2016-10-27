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
    public partial class Frm_ColorBox_RePrint : Office2007Form
    {
        public Frm_ColorBox_RePrint(Office2007Form Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }


        Office2007Form mFrm;

        private void Frm_ColorBox_RePrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Frm_ColorBox_RePrint_Load(object sender, EventArgs e)
        {
            this.txt_esn.Text = string.Empty;
        }

        private void txt_esn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_esn.Text) && e.KeyCode==Keys.Enter)
            {
                if (mFrm is Frm_ColorBoxPrint)
                {
                    (mFrm as Frm_ColorBoxPrint).PrintLabel(txt_esn.Text);
                    FrmBLL.publicfuntion.WriteLog("ColorBox_RePrint: " + txt_esn.Text);
                    this.txt_esn.Text = string.Empty;
                }
            }
        }
    }
}