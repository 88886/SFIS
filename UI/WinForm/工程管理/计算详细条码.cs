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
    public partial class FrmGetDetailedSnList : Office2007Form //Form
    {
        public FrmGetDetailedSnList()
        {
            InitializeComponent();
        }

        private void FrmGetDetailedSnList_Load(object sender, EventArgs e)
        {

        }

        private void tb_wo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tb_wo.Text)) && (e.KeyCode==Keys.Enter))
            {
              DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoSnRule(tb_wo.Text,""));
              if (dt.Rows.Count != 0)
              {
                  dgvwosnrule.DataSource = dt;
                  DataTable dtsntype=  dt.DefaultView.ToTable(true, "sntype");
                  chkListSnType.Items.Clear();
                  foreach (DataRow dr in dtsntype.Rows)
                  {
                    //  if (dr[0].ToString().ToUpper() == "MAC")
                          chkListSnType.Items.Add(dr[0].ToString());
                  }
              }
              else
              {
                  MessageBox.Show("没有条码区间信息");
                 
              }
              tb_wo.Focus();
              tb_wo.SelectAll();
            }
        }

        private void btn_compute_Click(object sender, EventArgs e)
        {
          
                for (int i = 0; i < chkListSnType.CheckedItems.Count; i++)
                {
                    MessageBox.Show(chkListSnType.CheckedItems[i].ToString() + "计算: " + refWebtWoInfo.Instance.Calculation_MacList(dgvwosnrule.Rows[0].Cells[0].Value.ToString(), chkListSnType.CheckedItems[i].ToString(),0));
                } 


        }

        private void imbt_delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListSnType.CheckedItems.Count; i++)
            {
                MessageBox.Show(chkListSnType.CheckedItems[i].ToString() + "删除: " + refWebtWoInfo.Instance.Calculation_MacList(dgvwosnrule.Rows[0].Cells[0].Value.ToString(), chkListSnType.CheckedItems[i].ToString(), 1));
            }
        }
    }
}
