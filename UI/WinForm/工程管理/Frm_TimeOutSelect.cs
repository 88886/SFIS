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
    public partial class Frm_TimeOutSelect : Office2007Form //Form
    {
        public Frm_TimeOutSelect(Office2007Form frm)
        {
            InitializeComponent();
            mFrm = frm;
        }

        Office2007Form mFrm;
        DataTable dt = null;

        
        private void Frm_TimeOutSelect_Load(object sender, EventArgs e)
        {
            DataTable dtChk = FrmBLL.ReleaseData.arrByteToDataTable(refWebt_Check_Timeout.Instance.Get_t_Check_Timeout(null));
            DataView dv = dtChk.DefaultView;
            dv.Sort = "CHECK_NO Asc";
            dt = dv.ToTable();
            foreach (DataRow dr in dt.DefaultView.ToTable(true, "CHECK_NO").Rows)
            {
                TreeNode a = new TreeNode();
                a.Text = dr["CHECK_NO"].ToString();
             //   a.ImageIndex = 0;
                this.treeView1.Nodes.Add(a);
            }

            this.treeView1.Nodes.Add("NA");
        }

        private void imbt_select_Click(object sender, EventArgs e)
        {
            if (this.mFrm is WorkOrderCreate)
            {
               string _StrErr= (this.mFrm as WorkOrderCreate).CHK_PACKING_ROUTE();
               if (_StrErr == "OK")
               {                   
                   this.DialogResult = DialogResult.OK;
               }
               else
               {
                   MessageBox.Show(_StrErr, "判定信息提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
               }
            }
            if (this.mFrm is Frm_WO_Update)
            {
                string _StrErr = (this.mFrm as Frm_WO_Update).CHK_PACKING_ROUTE();
                if (_StrErr == "OK")
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(_StrErr, "判定信息提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        string SelectNo = string.Empty;
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectNo=e.Node.FullPath.Split('\\')[0];
            if (this.mFrm is WorkOrderCreate)
            {
                (this.mFrm as WorkOrderCreate).tb_chkno.Text = SelectNo;
                if (SelectNo != "NA")
                {
                    DataTable dtTemp = FrmBLL.publicfuntion.getNewTable(dt, string.Format("CHECK_NO='{0}'", SelectNo));
                    ListNo.Items.Clear();
                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        ListNo.Items.Add("检查编号:" + dr[0].ToString());
                        ListNo.Items.Add("检查途程:" + dr[1].ToString());
                        (this.mFrm as WorkOrderCreate).mChkRoute = dr[1].ToString();
                        ListNo.Items.Add("退回途程:" + dr[2].ToString());
                        (this.mFrm as WorkOrderCreate).mRollBackRoute = dr[2].ToString();
                        ListNo.Items.Add("超时时间:" + dr[3].ToString());
                        ListNo.Items.Add("休息时间:");
                        foreach (string Str in dr[4].ToString().Split('|'))
                        {
                            ListNo.Items.Add(Str);
                        }
                    }
                }
                else
                {
                    (this.mFrm as WorkOrderCreate).mChkRoute = string.Empty;
                    (this.mFrm as WorkOrderCreate).mRollBackRoute = string.Empty;
                }
            }
            if (this.mFrm is Frm_WO_Update)
            {
                (this.mFrm as Frm_WO_Update).txt_check_no.Text = SelectNo;
                if (SelectNo != "NA")
                {
                    DataTable dtTemp = FrmBLL.publicfuntion.getNewTable(dt, string.Format("CHECK_NO='{0}'", SelectNo));
                    ListNo.Items.Clear();
                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        ListNo.Items.Add("检查编号:" + dr[0].ToString());
                        ListNo.Items.Add("检查途程:" + dr[1].ToString());
                        (this.mFrm as Frm_WO_Update).mChkRoute = dr[1].ToString();
                        ListNo.Items.Add("退回途程:" + dr[2].ToString());
                        (this.mFrm as Frm_WO_Update).mRollBackRoute = dr[2].ToString();
                        ListNo.Items.Add("超时时间:" + dr[3].ToString());
                        ListNo.Items.Add("休息时间:");
                        foreach (string Str in dr[4].ToString().Split('|'))
                        {
                            ListNo.Items.Add(Str);
                        }
                    }
                }
                else
                {
                    (this.mFrm as Frm_WO_Update).mChkRoute = string.Empty;
                    (this.mFrm as Frm_WO_Update).mRollBackRoute = string.Empty;
                }
            }

        }
    }
}
