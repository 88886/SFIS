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
    public partial class FrmReasonCode :Office2007Form// Form
    {
        public FrmReasonCode(MainParent msInfo)
        {
            InitializeComponent();
            sMain = msInfo;
        }
        MainParent sMain;

       
        private void GetReasonCode()
        {
            DataTable dt=FrmBLL.ReleaseData.arrByteToDataTable( refWebtReasonCode.Instance.GetReasonCode());
            dgvReason.DataSource = dt;
            DataView dataView = dt.DefaultView;
            DataTable dataTableDistinct = dataView.ToTable(true, "REASONTYPE");
            txt_reasontype.Items.Clear();
            foreach (DataRow dr in dataTableDistinct.Rows)
            {
                txt_reasontype.Items.Add(dr[0].ToString());
            }
        }

        private void ClearData()
        {
            txt_ReasonCode.Text = "";
            txt_reasondesc.Text = "";
            txt_reasondesc2.Text = "";
            txt_dutystation.Text = "";
            txt_reasontype.Text = "";
            RcCode = "";
            txt_ReasonCode.Enabled = true;
            txt_reasondesc.Enabled = true;
            txt_reasondesc2.Enabled = true;
            txt_reasontype.Enabled = true;
            txt_dutystation.Enabled = true;


            btReasonCode_Add.Enabled = true;
            btReasonCode_Delete.Enabled = true;
            btReasonCode_Modify.Enabled = true;
        }
        private enum CheckAddOrModift
        {
            新增,
            修改
        }

        CheckAddOrModift sFlag;      


        private void lbok_MouseEnter(object sender, EventArgs e)
        {
            lbok.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void lbok_MouseLeave(object sender, EventArgs e)
        {
            lbok.BackColor=Color.FromArgb( 192, 192, 0);
        }

        private void lbcancel_MouseEnter(object sender, EventArgs e)
        {
            lbcancel.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void lbcancel_MouseLeave(object sender, EventArgs e)
        {
            lbcancel.BackColor = Color.FromArgb(192, 192, 0);
        }

        private void lbok_MouseDown(object sender, MouseEventArgs e)
        {
            lbok.BackColor = Color.FromArgb(255, 128, 128);
        }

        private void lbcancel_MouseDown(object sender, MouseEventArgs e)
        {
           lbcancel.BackColor = Color.FromArgb(255, 128, 128);
        }

        private void lbok_Click(object sender, EventArgs e)
        {
            try
            {
                switch (sFlag)
                {
                    case  CheckAddOrModift.新增:
                        if ((string.IsNullOrEmpty(txt_ReasonCode.Text)) || (string.IsNullOrEmpty(txt_reasondesc.Text))
                            || (string.IsNullOrEmpty(txt_reasondesc2.Text)) || (string.IsNullOrEmpty(txt_reasontype.Text))
                            || (string.IsNullOrEmpty(txt_dutystation.Text)))
                        {
                            sMain.ShowPrgMsg("输入项不能为空,请确认...",MainParent.MsgType.Error);
                            return;
                        }


                        if (!CheckRcInDatabase)
                        {
                            MessageBox.Show("原因代码: " + txt_ReasonCode.Text.Trim() + " 已经存在,不需要重复添加");
                            return;
                        }

                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        FrmBLL.publicfuntion.SerializeControl(dic, PalData);
                        refWebtReasonCode.Instance.InserInToReasonCode(FrmBLL.ReleaseData.DictionaryToJson(dic));
                        //refWebtReasonCode.Instance.InserInToReasonCode(new WebServices.tReasonCode.tReasonCodeTable()
                        //    {
                        //        ReasonCode = txt_ReasonCode.Text.Trim(),
                        //        ReasonDesc = txt_reasondesc.Text.Trim(),
                        //        ReasonDesc2 = txt_reasondesc2.Text.Trim(),
                        //        ReasonType = txt_reasontype.Text.Trim(),
                        //        DutyStation = txt_dutystation.Text.Trim()                               
                                 
                        //    });                       

                        FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "原因代码", "新增", "原因代码: " + txt_ReasonCode.Text.Trim());

                        txt_dutystation.Text = "";
                        txt_ReasonCode.Text = "";
                        txt_reasondesc.Text = "";
                        txt_reasondesc2.Text = "";
                        txt_reasontype.Text = "";
                        txt_ReasonCode.Focus();
                        GetReasonCode();
                        GetDataLocationFocus(txt_ReasonCode.Text.Trim());
                  

                        break;

                    case CheckAddOrModift.修改:
                        
                          Dictionary<string, object> dicmodify = new Dictionary<string, object>();
                          FrmBLL.publicfuntion.SerializeControl(dicmodify, PalData);
                          refWebtReasonCode.Instance.UpdateReasonCode(FrmBLL.ReleaseData.DictionaryToJson(dicmodify));
                        //refWebtReasonCode.Instance.UpdateReasonCode(new WebServices.tReasonCode.tReasonCodeTable()
                        //{
                        //    ReasonCode = txt_ReasonCode.Text.Trim(),
                        //    ReasonDesc = txt_reasondesc.Text.Trim(),
                        //    ReasonDesc2 = txt_reasondesc2.Text.Trim(),
                        //    ReasonType = txt_reasontype.Text.Trim(),
                        //    DutyStation = txt_dutystation.Text.Trim()

                        //});
                        
                        FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "原因代码", "修改", "原因代码: " + txt_ReasonCode.Text.Trim());


                        GetReasonCode();
                        GetDataLocationFocus(txt_ReasonCode.Text.Trim());
                        lbcancel_Click(null,null);                 
                        break;


                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                sMain.ShowPrgMsg(ex.Message,MainParent.MsgType.Error);
            }
        }

        private void lbcancel_Click(object sender, EventArgs e)
        {
            ClearData();
            PalData.Visible = false;
        }

        private void FrmReasonCode_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.sMain.gUserInfo.rolecaption == "系统开发员")
            {
                IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PROGID", this.Name);
                dic.Add("PROGNAME", this.Text);
                dic.Add("PROGDESC", this.Text);
                FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
            }
            #endregion
            PalData.Visible = false;
            GetReasonCode();

        }

        private void btReasonCode_Add_Click(object sender, EventArgs e)
        {
            PalData.Visible = true;
            ClearData();
            btReasonCode_Modify.Enabled = false;
            btReasonCode_Delete.Enabled = false;
            txt_ReasonCode.Focus();
            sFlag = CheckAddOrModift.新增;
        }

        private void btReasonCode_Modify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ReasonCode.Text))
            {
                MessageBox.Show("请先选择需要修改的资料", "修改提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PalData.Visible = true;
            txt_ReasonCode.Enabled = false;
            btReasonCode_Add.Enabled = false;
            btReasonCode_Delete.Enabled = false;
            sFlag = CheckAddOrModift.修改;
        }

        bool CheckRcInDatabase = false;
        private void tb_ReasonCode_Leave(object sender, EventArgs e)
        {
            if (dgvReason.RowCount == 0)
            {
                CheckRcInDatabase = true;
                return;
            }

            for (int i = 0; i < dgvReason.RowCount; i++)
            {
                if (dgvReason[0, i].Value.ToString() == txt_ReasonCode.Text.Trim())
                {
                    MessageBox.Show("原因代码: " + txt_ReasonCode.Text.Trim() + " 已经存在,不需要重复添加");
                    txt_ReasonCode.Focus();
                    txt_ReasonCode.SelectAll();
                    CheckRcInDatabase = false;
                }
                else
                    CheckRcInDatabase = true;
            }
        }


        private void GetDataLocationFocus(string C_RC)
        {
            for (int i = 0; i < dgvReason.RowCount; i++)
            {
                if (dgvReason[0, i].Value.ToString() == C_RC)
                {
                    dgvReason.Rows[i].Selected = true;
                    dgvReason.FirstDisplayedScrollingRowIndex = i;
                }
                else
                    dgvReason.Rows[i].Selected = false;
            }
        }

        private void btReasonCode_Delete_Click(object sender, EventArgs e)     

        {
            if (string.IsNullOrEmpty(RcCode))
            {
                MessageBox.Show("请先选择需要删除的原因代码", "删除信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("确定要删除错误代码: " + RcCode, "删除信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                refWebtReasonCode.Instance.DeleteReasonCode(txt_ReasonCode.Text.Trim());

                FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "原因代码", "删除", "原因代码: " + txt_ReasonCode.Text.Trim());

                GetReasonCode();
                sMain.ShowPrgMsg("原因代码删除成功", MainParent.MsgType.Incoming);
                tb_QryReason.Focus();
                ClearData();
            }
        }
        string RcCode = "";
        private void dgvReason_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (btReasonCode_Add.Enabled == true && btReasonCode_Modify.Enabled == true)
            {
                if (e.RowIndex != -1)
                {
                   txt_ReasonCode.Text = RcCode = dgvReason[0, e.RowIndex].Value.ToString();
                  txt_reasontype.Text = dgvReason[1, e.RowIndex].Value.ToString();
                   txt_reasondesc.Text = dgvReason[2, e.RowIndex].Value.ToString();
                   txt_reasondesc2.Text = dgvReason[3, e.RowIndex].Value.ToString();
                   txt_dutystation.Text = dgvReason[4, e.RowIndex].Value.ToString();

                }
            }
        }

        private void tb_ReasonCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_reasontype.Focus();
                txt_reasontype.SelectAll();
            }
        }

        private void tb_dutystation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_reasondesc.Focus();
                txt_reasondesc.SelectAll();
            }
        }

        private void tb_reasontype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_dutystation.Focus();
                txt_dutystation.SelectAll();
            }
        }

        private void tb_reasondesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_reasondesc2.Focus();
                txt_reasondesc2.SelectAll();
            }
        }

        private void tb_QryReason_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tb_QryReason.Text)) && (e.KeyCode == Keys.Enter))
            {
                GetDataLocationFocus(tb_QryReason.Text.Trim());
            }
        }
   
       

      

      

        
       
    }
}
