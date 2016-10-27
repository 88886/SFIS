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
    public partial class FrmPartInfo : Office2007Form// Form
    {
        public FrmPartInfo(FrmBomCompare frm)
        {
            InitializeComponent();
            this.mFbc = frm;
        }
        FrmBomCompare mFbc;
        private void FrmPartInfo_Load(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.tbPartnumber.Text))
                {
                    MessageBoxEx.Show("成品料号不能为空,请填写成品料号");

                    this.tbPartnumber.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.tbproductname.Text))
                {
                    MessageBoxEx.Show("产品名称不能为空,请填写产品名称");
                    this.tbproductname.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.tblineid.Text))
                {
                    MessageBoxEx.Show("产线编号不能为空,请填写产线编号");
                    this.tblineid.Focus();
                    return;
                }
                if(this.tblineid.Text.Trim().Length !=6)
                {
                    MessageBoxEx.Show("产线编号规则不符,请重新输入..");
                    this.tblineid.SelectAll();
                    this.tblineid.Focus();
                    return;
                }

                this.mFbc.mPartnumber = this.tbPartnumber.Text.Trim();
                this.mFbc.mProductname = this.tbproductname.Text.Trim();
                this.mFbc.mLineId = this.tblineid.Text;
                this.DialogResult = DialogResult.Yes;
            }
            catch
            {
                this.DialogResult = DialogResult.No;
            }
        }

        private void tblineid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void tblineid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (!string.IsNullOrEmpty(this.tblineid.Text))
                {
                    this.buttonX1.Focus();
                }
            }
        }
    }
}
