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
    public partial class Frm_Update : Office2007Form //Form
    {
        public Frm_Update(Office2007Form Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Office2007Form mFrm;

        public string ReasonCode = string.Empty;
        public string ReasonCodeDesc = string.Empty;
        public string Duty = string.Empty;
        public string DutyDesc = string.Empty;
        DataTable dtReason = null;
        DataTable dtDuty = null;
        private void Frm_Update_Load(object sender, EventArgs e)
        {
            LabTime.Text = System.DateTime.Now.ToString();
            dtReason = FrmBLL.ReleaseData.arrByteToDataTable(refWebtReasonCode.Instance.GetReasonCode());
            dtDuty = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetDutyInfo());
            tb_reasoncode.Focus();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;

             
            dgvMaterial.Columns.Add("ESN","ESN");
            dgvMaterial.Columns.Add("KPNO", "KPNUMBER");
            dgvMaterial.Columns.Add("VENDER_CODE", "VENDER_CODE");
            dgvMaterial.Columns.Add("DATE_CODE", "DATE_CODE");
            dgvMaterial.Columns.Add("LOT_CODE", "LOT_CODE");

            if ((mFrm as Frm_RepairMain).List_DicMaterial.Count > 0)
            {
                foreach (Dictionary<string, string> dic in (mFrm as Frm_RepairMain).List_DicMaterial)
                {
                    dgvMaterial.Rows.Add(dic["ESN"], dic["KPNUMBER"], dic["VENDER_CODE"], dic["DATE_CODE"], dic["LOT_CODE"]);
                }
            }

        }

        public void ShowReasonInfo()
        {
            tb_reasoncode.Text = ReasonCode;
            tb_ReasonCodeDesc.Text = ReasonCodeDesc;
        }
        public void ShowDuty()
        {
            tb_Duty.Text = Duty;
            tb_DutyDesc.Text = DutyDesc;
        }
        private void Check_Reason()
        {
            DataTable dt = FrmBLL.publicfuntion.getNewTable(dtReason, string.Format("REASONCODE='{0}'", tb_reasoncode.Text.Trim()));
            if (dt.Rows.Count > 0)
            {
                ReasonCode = dt.Rows[0]["REASONCODE"].ToString();
                ReasonCodeDesc = dt.Rows[0]["REASONDESC"].ToString();
                ShowReasonInfo();
            }
            else
            {
                MessageBox.Show("没有找到此原因代码","错误提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                tb_reasoncode.SelectAll();
                tb_reasoncode.Focus();
            }
        }

        private void Check_Duty()
        {
            DataTable dt = FrmBLL.publicfuntion.getNewTable(dtDuty, string.Format("DUTY='{0}'",tb_Duty.Text.Trim()));
            if (dt.Rows.Count > 0)
            {
                Duty = dt.Rows[0]["DUTY"].ToString();
                DutyDesc = dt.Rows[0]["DUTYDESC"].ToString();
                ShowDuty();
            }
            else
            {
                MessageBox.Show("没有找到责任单位", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb_Duty.SelectAll();
                tb_Duty.Focus();
            }
        }
        private void imbt_browserRC_Click(object sender, EventArgs e)
        {
            Frm_ReasonCode frc = new Frm_ReasonCode(this,FrmBLL.publicfuntion.DataTableToSort( dtReason,"REASONCODE"));
            frc.ShowDialog();
        }

        private void tb_reasoncode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_reasoncode.Text) && e.KeyCode==Keys.Enter)
            {
                Check_Reason();            
            }
        }     


        private void tb_reasoncode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_reasoncode.Text))
                Check_Reason();

        }

        private void imbt_browserduty_Click(object sender, EventArgs e)
        {
            Frm_Duty fd = new Frm_Duty(this,FrmBLL.publicfuntion.DataTableToSort( dtDuty,"DUTY"));
            fd.ShowDialog();
        }

        private void tb_Duty_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void tb_Duty_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_Duty.Text) && e.KeyCode == Keys.Enter)
            {
                Check_Duty();
            }
        }

        private void tb_Duty_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_Duty.Text))
                Check_Duty();
        }

        private void tb_trsn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_trsn.Text) && e.KeyCode == Keys.Enter)
            {
                string _StrErr = string.Empty;
              DataTable dt=  FrmBLL.ReleaseData.arrByteToDataTable(refWebtR_Tr_Sn.Instance.Sel_Tr_Sn_Info(tb_trsn.Text,out _StrErr));
              if (dt.Rows.Count > 0)
              {
                  tb_Partnumber.Text = dt.Rows[0]["KP_NO"].ToString();
                  tb_Vender.Text = dt.Rows[0]["VENDER_ID"].ToString();
                  tb_LotNo.Text = dt.Rows[0]["LOT_CODE"].ToString();
                  tb_DateCode.Text = dt.Rows[0]["DATE_CODE"].ToString();
                  tb_trsn.SelectAll();
                  if (Chk_AutoCommit.Checked)
                  {
                      imbt_CommitMaterial_Click(null,null);
                  }
              }
              else
              {
                  MessageBox.Show("物料条码不存在", "物料条码检查", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
            }
        }

        private void imbt_OK_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(ReasonCode))
            {
                ShowMessage("请选择维修原因代码..");
                return;
            }
            if (string.IsNullOrEmpty(tb_Locataion.Text))
            {
                ShowMessage("请输入零件位置");
                return;
            }
            if (string.IsNullOrEmpty(tb_Duty.Text))
            {
                ShowMessage("请选择责任单位");
                return;
            }           

            (mFrm as Frm_RepairMain).R_sThisReasonCode = ReasonCode;
            (mFrm as Frm_RepairMain).R_sThisReasonCodeDesc = ReasonCodeDesc;
            (mFrm as Frm_RepairMain).R_sThisLocation = tb_Locataion.Text;
            (mFrm as Frm_RepairMain).R_sThisDuty = tb_Duty.Text;
            (mFrm as Frm_RepairMain).R_sThisMemo = string.IsNullOrEmpty(tb_memo.Text) ? "NA" : tb_memo.Text;
            (mFrm as Frm_RepairMain).List_DicMaterial.Clear();
            foreach (DataGridViewRow dgvr in dgvMaterial.Rows)
            {              
                (mFrm as Frm_RepairMain).DicMaterial = new Dictionary<string,object>();
                (mFrm as Frm_RepairMain).DicMaterial.Add("ESN",dgvr.Cells[0].Value.ToString());
                (mFrm as Frm_RepairMain).DicMaterial.Add("KPNUMBER", dgvr.Cells[1].Value.ToString());
                (mFrm as Frm_RepairMain).DicMaterial.Add("VENDER_CODE", dgvr.Cells[2].Value.ToString());
                (mFrm as Frm_RepairMain).DicMaterial.Add("DATE_CODE", dgvr.Cells[3].Value.ToString());
                (mFrm as Frm_RepairMain).DicMaterial.Add("LOT_CODE", dgvr.Cells[4].Value.ToString());
                (mFrm as Frm_RepairMain).List_DicMaterial.Add((mFrm as Frm_RepairMain).DicMaterial);
            }
            (mFrm as Frm_RepairMain).SHOW_REPAIR_DATA();
            DialogResult = DialogResult.OK;
        }

        private void imbt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void imbt_CommitMaterial_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_Partnumber.Text))
            {
                foreach (DataGridViewRow dgvr in dgvMaterial.Rows)
                {
                    if (dgvr.Cells[0].Value.ToString() == (mFrm as Frm_RepairMain).M_sThisEsn &&
                        dgvr.Cells[1].Value.ToString() == tb_Partnumber.Text &&
                        dgvr.Cells[2].Value.ToString() == tb_Vender.Text &&
                        dgvr.Cells[3].Value.ToString() == tb_DateCode.Text &&
                        dgvr.Cells[4].Value.ToString() == tb_LotNo.Text)
                    {
                        MessageBox.Show("输入物料信息重复", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                dgvMaterial.Rows.Add((mFrm as Frm_RepairMain).M_sThisEsn, tb_Partnumber.Text, tb_Vender.Text, tb_DateCode.Text, tb_LotNo.Text);
                tb_Partnumber.Text = string.Empty;
                tb_Vender.Text = string.Empty;
                tb_DateCode.Text = string.Empty;
                tb_LotNo.Text = string.Empty;
                tb_trsn.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("料号不能为空", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMaterial_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string KPNUMBER = dgvMaterial.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (MessageBox.Show(string.Format("是否删除物料[{0}]", KPNUMBER), "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    dgvMaterial.Rows.RemoveAt(e.RowIndex);                   
                }

            }
        }
        private void ShowMessage(string Msg)
        {
            MessageBox.Show(Msg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
