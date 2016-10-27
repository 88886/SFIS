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
    public partial class Frm_LabelQty : Office2007Form //Form
    {
        public Frm_LabelQty(string sPatch)
        {
            InitializeComponent();
            IniFilePath = sPatch;
        }
        string IniFilePath;
        private void Frm_LabelQty_Load(object sender, EventArgs e)
        {
            string LabelQty = FrmBLL.ReadIniFile.IniReadValue("PACK_CTN", "LabelQty", IniFilePath);
           numqty.Value =Convert.ToDecimal( LabelQty); 
        }

        private void imbt_OK_Click(object sender, EventArgs e)
        {
          FrmBLL.ReadIniFile.IniWriteValue("PACK_CTN", "LabelQty", numqty.Value.ToString(), IniFilePath);
            this.DialogResult = DialogResult.OK;
        }

        private void imbt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
