using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ram
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void Btn_OpenRAM_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(RefWebService_BLL.refWebRam.Instance.GetConnectStr());
            //return;
            
            RamRecord Frm = new RamRecord();
            //this.Hide();
            Frm.Show();
        }

        private void Btn_OpenTest_Click(object sender, EventArgs e)
        {
            TestRecord Frm = new TestRecord();
           // this.Hide();
            Frm.Show();
        }

        private void Btn_OpenService_Click(object sender, EventArgs e)
        {
            ServiceRecord Frm = new ServiceRecord();
           // this.Hide();
            Frm.Show();
        }

        private void Btn_OpenPack_Click(object sender, EventArgs e)
        {
            PackRecord Frm = new PackRecord();
           // this.Hide();
            Frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Print Frm = new Print();
            // this.Hide();
            Frm.Show();
        }
    }
}
