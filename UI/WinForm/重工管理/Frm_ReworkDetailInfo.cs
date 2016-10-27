using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class Frm_ReworkDetailInfo : Office2007Form  //Form
    {
        public Frm_ReworkDetailInfo()
        {
            InitializeComponent();
        }

        private void Frm_ReworkDetailInfo_Load(object sender, EventArgs e)
        {

        }

        private void txt_ReworkNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_ReworkNo.Text) && e.KeyCode == Keys.Enter)
            {
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtReworkDetailInfo.Instance.GetReworkInfo(txt_ReworkNo.Text));
                FrmBLL.publicfuntion.Fill_Control(panelEx2, dt);
                txt_ReworkNo.SelectAll();
            }
        }

        private void txt_Userid_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Userid.Text))
            {
                txt_username.Text=string.Empty;
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo(txt_Userid.Text,null,null));
                if (dt.Rows.Count>0)
                    txt_username.Text=dt.Rows[0]["USERNAME"].ToString();
                 

            }
        }
    }
}
