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
    public partial class ServerIpConfig :Office2007Form// Form
    {
        public ServerIpConfig(Login lg,string _ipadd)
        {
            InitializeComponent();
            mLg = lg;
            this.mIpadd = _ipadd;
        }
        Login mLg;
        string mIpadd = string.Empty;
        private void bt_saveip_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_ipaddress.Value))
            {
                this.mLg.WebServiceIpAddress = this.tb_ipaddress.Value;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void ServerIpConfig_Load(object sender, EventArgs e)
        {
            this.tb_ipaddress.Value = this.mIpadd;
        }
    }
}
