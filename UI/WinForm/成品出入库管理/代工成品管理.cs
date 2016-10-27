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
    public partial class FrmCustomerProduct : Office2007Form//Form
    {
        public FrmCustomerProduct(MainParent _mfm)
        {
            InitializeComponent();
            this.mFrm = _mfm;
        }
        MainParent mFrm;

       
    }
}
