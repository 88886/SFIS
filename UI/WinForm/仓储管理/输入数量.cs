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
    public partial class InputQty : Office2007Form//Form
    {
        public InputQty(FrmReceiveMaterials rm)
        {
            InitializeComponent();
            this.mRm = rm;
        }

        FrmReceiveMaterials mRm;
        private void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_all.Checked)
            {
                this.tb_qty.Text = mRm.lb_qty.Text.Trim();
            }
            else
                this.tb_qty.Text = "";
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_qty.Text.Trim()))
            {
                MessageBox.Show("请输入收货数量!");
                return;
            }
            if (Convert.ToInt32(this.tb_qty.Text.Trim()) > Convert.ToInt32(mRm.lb_qty.Text))
            {
                MessageBox.Show("输入的入库数量超出该物料的最大待收数量,请确认...");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void tb_qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue==13)
            {
                bt_ok_Click(null, null);
            }
        }
    }
}
