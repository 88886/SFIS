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
    public partial class Frm_ChangeKP : Office2007Form //Form
    {
        public Frm_ChangeKP(Office2007Form Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        Office2007Form mFrm;
        string M_ThisEsn = string.Empty;
        string M_ThiswoId = string.Empty;
        private void Frm_ChangeKP_Load(object sender, EventArgs e)
        {

            Initialization();

            M_ThisEsn = (mFrm as Frm_RepairMain).M_sThisEsn;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            Show_KeyParts(M_ThisEsn);
           
            this.dgv_keyparts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void Show_KeyParts(string Snval)
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyPart(Snval));
                dgv_keyparts.DataSource = dt;
            
        }

        private void dgv_keyparts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
         
        }

        private void dgv_keyparts_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Lab_esn.Text = dgv_keyparts.Rows[e.RowIndex].Cells["ESN"].Value.ToString();
                lab_sntype.Text = dgv_keyparts.Rows[e.RowIndex].Cells["SNTYPE"].Value.ToString();
                lab_snval.Text = dgv_keyparts.Rows[e.RowIndex].Cells["SNVAL"].Value.ToString();
                M_ThiswoId = dgv_keyparts.Rows[e.RowIndex].Cells["WOID"].Value.ToString();
            }
        }


        private void tb_keypats_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_keypats.Text) && e.KeyCode == Keys.Enter)
            {
               if(Check_KP())
                   MessageBox.Show("检查完成,可以使用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
               this.tb_keypats.SelectAll();
            }
        }

        private void ShowMessage(string Msg)
        {
            MessageBox.Show(Msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            tb_keypats.SelectAll();
        }
        private bool Check_KP()
        {
            bool Result=false;

            if (!string.IsNullOrEmpty(Lab_esn.Text))
            {
                string _StrErr = refWebtWipKeyPart.Instance.CHECK_KPS_VALID_FOR_REPAIR(Lab_esn.Text, tb_keypats.Text, lab_sntype.Text, M_ThiswoId);
                if (_StrErr == "OK")
                    Result = true;
                else
                    ShowMessage(_StrErr);
            }
            else
            {
                ShowMessage("ESN 不能为空");
            }

            return Result;
        }

        private void imbt_ok_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_keypats.Text) && !string.IsNullOrEmpty(Lab_esn.Text))
            {

                if (!Check_KP())
                    return;
                string _StrErr = string.Empty;
                _StrErr = refWebtWipKeyPart.Instance.Insert_WipKeyParts_Undo(Lab_esn.Text, lab_sntype.Text, lab_snval.Text);
                if (_StrErr != "OK")
                {
                    ShowMessage(_StrErr);
                    return;
                }

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("ESN", lab_snval.Text);
                 dic.Add("WIPSTATION","MB_Repair");
                _StrErr = refWebtWipTracking.Instance.Update_Wip_Tracking(FrmBLL.ReleaseData.DictionaryToJson(dic));
                 if (_StrErr != "OK")
                {
                    ShowMessage("Update WipStation Error: "+_StrErr);
                    return;
                }
                _StrErr = refWebtWipKeyPart.Instance.Update_WIP_KEYPARTS(Lab_esn.Text, tb_keypats.Text, lab_snval.Text, lab_sntype.Text);
                if (_StrErr != "OK")
                {
                    ShowMessage(_StrErr);
                    return;
                }
                FrmBLL.publicfuntion.InserSystemLog((mFrm as Frm_RepairMain).M_sThisRepairer, "Repair", "ReleaseBound", "ESN:" + Lab_esn.Text + "," + lab_sntype.Text + ":" + lab_snval.Text+"->"+tb_keypats.Text);

                Show_KeyParts(Lab_esn.Text);
                Initialization();
                MessageBox.Show("Change KPS OK","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
        }
        private void Initialization()
        {
            Lab_esn.Text = string.Empty;
            lab_sntype.Text = string.Empty;
            lab_snval.Text = string.Empty;
            tb_keypats.Text = string.Empty;
            (mFrm as Frm_RepairMain).M_Repair_Release_Bound = false;
        }

        private void imbt_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void imbt_ReleaseBound_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定做关系绑定解除,\r\n维修完成后将解除绑定关系.", "解绑提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                (mFrm as Frm_RepairMain).M_Repair_Release_Bound = true;
            }
            else
            {
                (mFrm as Frm_RepairMain).M_Repair_Release_Bound = false;
            }
           
        }

        private void Frm_ChangeKP_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

    }
}
