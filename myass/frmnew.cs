using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SFIS_V2;

namespace myass
{

    interface iAssemblyVersion
    {
        string getAssemblyVersion();
    }

    public partial class frmnew : Office2007Form, iAssemblyVersion// Form
    {
        public frmnew(MainParent _mfrm)
        {
            InitializeComponent();
            this.mfrm = _mfrm;
        }

        public frmnew()
        {
            InitializeComponent();
        }
        MainParent mfrm;
        private void frmnew_Load(object sender, EventArgs e)
        {
            MessageBox.Show("测试成功");
            mfrm.ShowPrgMsg("测试反射", MainParent.MsgType.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mfrm.ShowPrgMsg("测试反射", MainParent.MsgType.Error);
        }

        public string getAssemblyVersion()
        {
            return string.Empty;
        }

    }
}
