using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SFIS_PRINT_SYSTEM_WIFI
{
    public partial class ServerIpConfig :Office2007Form// Form
    {
        public ServerIpConfig(UserLogin lg)
        {
            InitializeComponent();
            mLg = lg;
        }
        UserLogin mLg;

        private void bt_saveip_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_ipaddress.Value))
            {
                this.mLg.WebServiceIpAddress = this.tb_ipaddress.Value;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
