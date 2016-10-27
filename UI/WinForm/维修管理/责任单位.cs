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
    public partial class FrmDuty :Office2007Form //Form
    {
        public FrmDuty(FrmRepair frinfo)
        {
            InitializeComponent();
            sRepair = frinfo;
        }

        FrmRepair sRepair;
        private void FrmDuty_Load(object sender, EventArgs e)
        {
             GetDutyData = new GetDuty(GetDutyInfo);
             GetDutyData.BeginInvoke(null,null);
        }
        private void UpdateDataTableColnumName(DataTable dts)
        {
            dts.Columns[0].ColumnName = "责任单位";
            dts.Columns[1].ColumnName = "责任描述";
            dts.Columns[2].ColumnName = "时间";

        }

         
        private delegate void GetDuty();
        GetDuty GetDutyData;
        private void GetDutyInfo()
        {          
        
             dgvDuty.Invoke(new EventHandler(delegate
                {
                    
                  DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetDutyInfo());
                  UpdateDataTableColnumName(dt);
                  dgvDuty.DataSource = dt;

                }));     
        }

        private void tb_Duty_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void tb_Duty_KeyDown(object sender, KeyEventArgs e)
        {

            if ((!string.IsNullOrEmpty(tb_Duty.Text)) && (e.KeyCode == Keys.Enter))
            {

                tb_DutyDesc.Focus();
                tb_DutyDesc.SelectAll();
            }
          
        }

        private void tb_Duty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只能输入数字和字母  退格键
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar==8))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
           
        }

        private void imbt_OK_Click(object sender, EventArgs e)
        {
            bool chkFlag = true;
            for (int i = 0; i < dgvDuty.Rows.Count; i++)
            {
                if (dgvDuty.Rows[i].Cells[0].Value.ToString() == tb_Duty.Text.Trim())
                {              
                    chkFlag = false;
                    break;
                }
            }

            if (!chkFlag)
            {
                MessageBox.Show("责任单位重复");
                return;
            }

            string Err = refWebRepairInfo.Instance.InsertDutyInfo(tb_Duty.Text.Trim(),tb_DutyDesc.Text.Trim());
            if (Err == "OK")
            {
                GetDutyData = new GetDuty(GetDutyInfo);
                GetDutyData.BeginInvoke(null, null);
                MessageBox.Show("新增责任单位完成", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tb_Duty.Text = "";
                tb_DutyDesc.Text = "";
            }
            else
            {
                MessageBox.Show(string.Format("新增责任单位失败[{0}]", Err), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDuty_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string DutyCode= dgvDuty.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (MessageBox.Show(string.Format("确定要删除责任单位[{0}]吗？", DutyCode), "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    string Err = refWebRepairInfo.Instance.DeleteDutyInfo(DutyCode);
                    if (Err == "OK")
                    {
                        GetDutyData = new GetDuty(GetDutyInfo);
                        GetDutyData.BeginInvoke(null, null);
                        MessageBox.Show("删除责任单位完成", "删除信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("删除责任单位失败[{0}]", Err), "删除信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } 
                }
            }
        }

        private void imbt_exit_Click(object sender, EventArgs e)
        {        
            this.Close();
        }

        private void FrmDuty_FormClosing(object sender, FormClosingEventArgs e)
        {
            sRepair.RefreshDutyInfo();
        }
    }
}
