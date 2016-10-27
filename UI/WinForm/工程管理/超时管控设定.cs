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
    public partial class Frm_TimeOut : Office2007Form //Form
    {
        public Frm_TimeOut(MainParent Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }
        MainParent mFrm;
        private void Frm_TimeOut_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
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

            Get_t_Check_Timeout();
            Get_Route();
            PanelData.Visible = false;
        }
        public void Get_Route()
        {
            cbx_CHECK_ROUTE.Items.Clear();
            cbx_ROLLBACK_ROUTE.Items.Clear();
            DataTable dt = FrmBLL.publicfuntion.DataTableToSort(FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo()), "CRAFTNAME");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["TESTFLAG"].ToString() == "2")
                    continue;
                cbx_CHECK_ROUTE.Items.Add(dr["CRAFTNAME"].ToString());
                cbx_ROLLBACK_ROUTE.Items.Add(dr["CRAFTNAME"].ToString());
            }

            cbx_CHECK_ROUTE.SelectedIndex = 0;
            cbx_ROLLBACK_ROUTE.SelectedIndex = 0;
        }

        public void Get_t_Check_Timeout()
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebt_Check_Timeout.Instance.Get_t_Check_Timeout(null));
            dt.Columns["CHECK_NO"].ColumnName = "编号";
            dt.Columns["CHECK_ROUTE"].ColumnName = "检查途程";
            dt.Columns["ROLLBACK_ROUTE"].ColumnName = "退回途程";
            dt.Columns["CHECK_TIMEOUT"].ColumnName = "超时时间";
            dt.Columns["REST_TIME"].ColumnName = "休息时间";
            dt.Columns["USERID"].ColumnName = "修改人员";
            dt.Columns["RECORD_DATE"].ColumnName = "修改时间";
            dgvTimeOut.DataSource = dt;
            if (dt.Rows.Count>0)            
            this.dgvTimeOut.Columns[4].FillWeight = dt.Rows[0][4].ToString().Length; 
        }

        public enum sFlag
        {
            新增,
            修改
        }

        sFlag _sFlag;
        private void imbt_Add_Click(object sender, EventArgs e)
        {
            imbt_Add.Enabled = true;
            imbt_Modify.Enabled = false;
            imbt_Delete.Enabled = false;
            _sFlag = sFlag.新增;
            txt_CHECK_NO.Text = Get_CHEK_NUMBER();
            PanelData.Visible = true;
            txt_REST_TIME.Text = "10:00~10:15|11:50~12:40|15:00~15:15|17:40~18:20|22:00~22:15|23:50~00:40|03:00~03:15|05:40~06:20";
        }

        private string Get_CHEK_NUMBER()
        {
            string _CHKNO = string.Empty;
            for (int x = 1; x <= 10000; x++)
            {
                _CHKNO = "C" + x.ToString().PadLeft(6, '0');
                bool chkflag = false;
                foreach (DataGridViewRow dgvr in dgvTimeOut.Rows)
                {
                    if (dgvr.Cells[0].Value.ToString() == _CHKNO)
                    {
                        chkflag = true;
                        break;
                    }
                }
                if (!chkflag)
                    break;
            }

            return _CHKNO;
        }
        private void imbt_Cancel_Click(object sender, EventArgs e)
        {
            PanelData.Visible = false;
            imbt_Add.Enabled = true;
            imbt_Modify.Enabled = true;
            imbt_Delete.Enabled = true;
            txt_CHECK_NO.Text = string.Empty;
            txt_REST_TIME.Text = string.Empty;
        }

        private void imbt_Modify_Click(object sender, EventArgs e)
        {
            if (SelectIndex != -1)
            {                
                //string err = refwebtEditing.Instance.ChktEditing(new WebServices.tEditing.tEditing1()
                //{
                //    userId = this.mFrm.gUserInfo.userId,
                //    prj = "TimeOut",
                //    funname = this.dgvTimeOut[0, SelectIndex].Value.ToString(),
                //    username = this.mFrm.gUserInfo.username,
                //    PC_NAME = FrmBLL.publicfuntion.HostName + " IP:" + ((LsIp.Count > 1) ? LsIp[1] : LsIp[0]),
                //    MAC_ADD = FrmBLL.publicfuntion.getMacList()[0]
                //});
                
                string err = FrmBLL.publicfuntion.ChktEditing(this.dgvTimeOut[0, SelectIndex].Value.ToString(), "TimeOut", this.mFrm.gUserInfo.userId, this.mFrm.gUserInfo.username);
                if (err != "OK")
                {
                    if (err.IndexOf("ERROR") != -1)
                    {
                        this.mFrm.ShowPrgMsg(err, MainParent.MsgType.Error);
                        return;
                    }
                    else
                    {
                        MessageBoxEx.Show(string.Format("用户:[{0}]/[{1}]--{2}正在编辑中,您现在不能进行修改!!",
                       err.Split(':')[1].Split(',')[0], err.Split(':')[1].Split(',')[1], err.Split(':')[1].Split(',')[2]), "提示!!");
                        return;
                    }
                }

                PanelData.Visible = true;
                imbt_Add.Enabled = false;
                imbt_Modify.Enabled = true;
                imbt_Delete.Enabled = false;
                _sFlag = sFlag.修改;
            }
        }

        private void dgvTimeOut_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex !=-1)
            {
                txt_CHECK_NO.Text = dgvTimeOut[0, e.RowIndex].Value.ToString();
                cbx_CHECK_ROUTE.SelectedIndex = cbx_CHECK_ROUTE.Items.IndexOf(dgvTimeOut[1, e.RowIndex].Value.ToString());
                cbx_ROLLBACK_ROUTE.SelectedIndex = cbx_ROLLBACK_ROUTE.Items.IndexOf(dgvTimeOut[2, e.RowIndex].Value.ToString());
                num_CHECK_TIMEOUT.Value = Convert.ToDecimal(dgvTimeOut[3, e.RowIndex].Value.ToString());
                txt_REST_TIME.Text = dgvTimeOut[4, e.RowIndex].Value.ToString();
                imbt_Modify_Click(sender,null);
            }
        }

        private void imbt_Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("确定要删除编号[{0}]", dgvTimeOut[0, SelectIndex].Value.ToString()), "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string _StrErr = refWebt_Check_Timeout.Instance.Delete_Check_Timeout(dgvTimeOut[0, SelectIndex].Value.ToString());

                if (_StrErr == "OK")
                {
                    FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "TimeOut", "Delete", string.Format("Delete TimeOut,CHKNO[{0}]", txt_CHECK_NO.Text));
                    MessageBox.Show("删除完成");
                    imbt_Cancel_Click(sender, null);
                    Get_t_Check_Timeout();
                }
                else
                {
                    MessageBox.Show("删除失败:" + _StrErr);
                }
            }

        }
        int SelectIndex =-1;
        private void dgvTimeOut_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex !=-1 &&   _sFlag != sFlag.新增)
            {
                SelectIndex = e.RowIndex;
             
                    txt_CHECK_NO.Text = dgvTimeOut[0, e.RowIndex].Value.ToString();
                    cbx_CHECK_ROUTE.SelectedIndex = cbx_CHECK_ROUTE.Items.IndexOf(dgvTimeOut[1, e.RowIndex].Value.ToString());
                    cbx_ROLLBACK_ROUTE.SelectedIndex = cbx_ROLLBACK_ROUTE.Items.IndexOf(dgvTimeOut[2, e.RowIndex].Value.ToString());
                    num_CHECK_TIMEOUT.Value = Convert.ToDecimal(dgvTimeOut[3, e.RowIndex].Value.ToString());
                    txt_REST_TIME.Text = dgvTimeOut[4, e.RowIndex].Value.ToString();
                
            }
        }

        private void imbt_OK_Click(object sender, EventArgs e)
        {
            string _StrErr = string.Empty;
            if (!Check_Time_Format(txt_REST_TIME.Text))
                return;
            Dictionary<string, object> dic = null;
            switch (_sFlag)
            {
                case sFlag.新增:
                    dic = new Dictionary<string, object>();
                    FrmBLL.publicfuntion.SerializeControl(dic, PanelData);
                    dic.Add("CHECK_TIMEOUT", num_CHECK_TIMEOUT.Value);
                    dic.Add("USERID",mFrm.gUserInfo.userId);                   
                    _StrErr = refWebt_Check_Timeout.Instance.Insert_Check_Timeout(FrmBLL.ReleaseData.DictionaryToJson(dic));
                   
                    if (_StrErr == "OK")
                    {
                        FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "TimeOut", "ADD", string.Format("add TimeOut,CHKNO[{0}]", txt_CHECK_NO.Text));
                        MessageBox.Show("新增完成");
                        imbt_Cancel_Click(sender, null);
                       
                    }
                    else
                    {
                        MessageBox.Show("新增失败:" + _StrErr);
                    }

                    
                    break;
                case sFlag.修改:

                      dic = new Dictionary<string, object>();
                    FrmBLL.publicfuntion.SerializeControl(dic, PanelData);
                    dic.Add("CHECK_TIMEOUT", num_CHECK_TIMEOUT.Value);
                    dic.Add("USERID",mFrm.gUserInfo.userId);              
                    _StrErr = refWebt_Check_Timeout.Instance.Update_Check_Timeout(FrmBLL.ReleaseData.DictionaryToJson(dic));
                  
                    if (_StrErr == "OK")
                    {
                        FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "TimeOut", "Modify", string.Format("Modify TimeOut,CHKNO[{0}]", txt_CHECK_NO.Text));
                        refwebtEditing.Instance.DeletetEditingByfunname(txt_CHECK_NO.Text);
                        MessageBox.Show("修改完成");
                        imbt_Cancel_Click(sender, null);

                    }
                    else
                    {
                        MessageBox.Show("修改失败:" + _StrErr);
                    }
                    break;

                default:
                    break;
            }


            imbt_Cancel_Click(sender,null);
            Get_t_Check_Timeout();
            SelectIndex =-1;
        }

        /// <summary>
        /// 检查输入时间格式
        /// </summary>
        /// <param name="RestTime"></param>
        /// <returns></returns>
        private bool Check_Time_Format(string RestTime)
        {
            if (RestTime.Split('|').Length > 3)
            {
                int x = 0;
                string sTime = string.Empty;
                foreach (string Item in RestTime.Split('|'))
                {
                    sTime = Item;
                    x = 0;
                    if (Item.Split('~').Length < 2)
                    {
                        break;
                    }
                    else
                    {
                        x++;
                    }
                }

                if (x < 1)
                {
                    MessageBox.Show(string.Format("输入字符串格式不正确[{0}]",sTime));
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("时间格式不正确");
                return false;
            }
        }

        private void Frm_TimeOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                refwebtEditing.Instance.DeletetEditingByUserIdAndPrj(this.mFrm.gUserInfo.userId, "TimeOut");
            }
            catch
            {
            }
        }
    }
}