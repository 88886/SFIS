using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Notice
{
    public partial class DetailWin :Office2007Form// Form
    {
        string detail = string.Empty;
        private DelegateDefine.Callback cb = null;
        public DetailWin(string detail,DelegateDefine.Callback cb)
        {
            this.cb = cb;
            this.detail = detail;
            InitializeComponent();
            this.Disposed += new EventHandler(DetailWin_Disposed);
        }

        void DetailWin_Disposed(object sender, EventArgs e)
        {
            if (cb!=null)
            {
                cb();
            }
        }

        private void DetailWin_Load(object sender, EventArgs e)
        {
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.textBox1.Text = detail;
        }
    }
}
