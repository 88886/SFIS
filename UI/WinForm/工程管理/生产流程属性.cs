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
    public partial class RoutegroupAtt :Office2007Form// Form
    {
        public RoutegroupAtt(Office2007Form crg)
        {
            InitializeComponent();
            mfrm = crg;
        }
        Office2007Form mfrm;
        private void RoutegroupAtt_Load(object sender, EventArgs e)
        {
            this.tb_routegroupId.Text= RefWebService_BLL.refWebtRouteInfo.Instance.GetRouteCode();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_routegroupId.Text))
            {
                MessageBoxEx.Show("流程编号不能为空..");
                return;
            }
            if (string.IsNullOrEmpty(this.tb_routegroupname.Text))
            {
                MessageBoxEx.Show("请填写流程名称");
                return;
            }
            if (mfrm is CreateRoute)
            {
                (this.mfrm as CreateRoute).gRoutegroupId = this.tb_routegroupId.Text.Trim();
                (this.mfrm as CreateRoute).gRoutegroupname = this.tb_routegroupname.Text.Trim();
            }
            if (mfrm is Frm_Create_Route)
            {
                (this.mfrm as Frm_Create_Route).lb_routecode.Text = this.tb_routegroupId.Text.Trim();
                (this.mfrm as Frm_Create_Route).lb_routedesc.Text = this.tb_routegroupname.Text.Trim();
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
