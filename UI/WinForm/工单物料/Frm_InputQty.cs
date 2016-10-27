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
    public partial class Frm_InputQty : Office2007Form //Form
    {
        public Frm_InputQty(Frm_Modify_TrSn_Qty Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }

        Frm_Modify_TrSn_Qty mFrm;

        System.Windows.Forms.Timer TimerPrg = new System.Windows.Forms.Timer();
        private void TimerPrg_Tick(object sender, EventArgs e)
        {
            if (this.LabMsg.ForeColor == Color.Green)
                this.LabMsg.ForeColor = Color.Blue;
            else
                this.LabMsg.ForeColor = Color.Green;
            
        }
        private void Frm_InputQty_Load(object sender, EventArgs e)
        {
            this.TimerPrg.Interval = 1000;
            this.TimerPrg.Enabled = true;
            this.TimerPrg.Tick += new EventHandler(TimerPrg_Tick);
        }

        private void imbt_OK_Click(object sender, EventArgs e)
        {
            mFrm.Modify_Qty = this.txt_qty.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void imbt_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {

            int WM_KEYDOWN = 256;

            int WM_SYSKEYDOWN = 260;

            if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
            {

                switch (keyData)
                {

                    case Keys.Escape:

                        this.Close();//esc关闭窗体

                        break;

                }



            }

            return false;

        }

        private void txt_qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_qty.Text) && e.KeyCode == Keys.Enter)
            {
                imbt_OK_Click(null, null);
            }
        }
    }
}
