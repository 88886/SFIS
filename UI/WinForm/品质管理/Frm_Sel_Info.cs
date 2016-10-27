using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class Frm_Sel_Info : Office2007Form
    {
        public Frm_Sel_Info(Frm_FQC f)
        {
            InitializeComponent();
            frm = f;
        }

        Frm_FQC frm;

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            List<string> ClearData = new List<string>();

            if (chk_tray.Checked)
            {
                ClearData.Add("0-TrayNo");
            }
            if (chk_CartonNo.Checked)
            {
                ClearData.Add("0-cartonnumber");
                ClearData.Add("0-mcartonnumber");
            }
            if (chk_PalletNo.Checked)
            {
                ClearData.Add("0-palletnumber");
                ClearData.Add("0-mpalletnumber");
            }
            if (chk_sn.Checked)
            {
                ClearData.Add("0-SN");
            }
            if (chk_mac.Checked)
            {
                ClearData.Add("0-MAC");
            }
            if (chk_stockno.Checked)
            {
                ClearData.Add("0-storenumber");
            }


            for (int i = 0; i < clb_labeltypes.CheckedItems.Count; i++)
            {
                ClearData.Add("1-" + clb_labeltypes.CheckedItems[i].ToString());
            }
            frm.ClearData = ClearData;
            DialogResult = DialogResult.OK;
        }

        private void Frm_Sel_Info_Load(object sender, EventArgs e)
        {
            FillType();
        }
        /// <summary>
        /// 加载标签类型
        /// </summary>
        private void FillType()
        {
            this.clb_labeltypes.Items.Clear();
            string[] lslablenames = refWebtProduct.Instance.GetLableList();// BLL.tProduct.GetLableList;
            foreach (string str in lslablenames)
            {
                this.clb_labeltypes.Items.Add(str);
            }
            this.clb_labeltypes.Refresh();
        }
    }
}
