using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SFIS_DCT_INPUT
{
    public partial class Frm_RePrint : Form
    {
        public Frm_RePrint( DevComponents.DotNetBar.Office2007Form Frm )
        {
            InitializeComponent();
            _mFrm = Frm;
        }
        DevComponents.DotNetBar.Office2007Form _mFrm;
        private void txt_esn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_esn.Text) && e.KeyCode == Keys.Enter)
            {
                try
                {
                    (_mFrm as SFIS_DCT_INPUT).PrintLabel(txt_esn.Text);
                }
                catch (Exception ex)
                {
                    (_mFrm as SFIS_DCT_INPUT).SendPrgMsg(SFIS_DCT_INPUT.mLogMsgType.Error, ex.Message);
                }
                finally
                {
                    txt_esn.Text = string.Empty;
                }
            }
        }
    }
}
