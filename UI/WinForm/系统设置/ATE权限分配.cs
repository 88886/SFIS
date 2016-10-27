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
    public partial class FrmAteEmp :Office2007Form// Form
    {
        public FrmAteEmp(MainParent frm)
        {
            InitializeComponent();
            mFrm = frm;
        }
        private string mUserId = string.Empty;
        private MainParent mFrm;
        private void FrmAteEmp_Load(object sender, EventArgs e)
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
            this.dgvAteEmp.DataSource =  FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtCraftInfo.Instance.GetAllCraftInfo2());

            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtUserInfo.Instance.GetUserInfo(null,null,null));
            DataTable temp = new DataTable();
            temp.Columns.Add("USERNAME", typeof(string));
            temp.Columns.Add("USERID", typeof(string));
            temp.Columns.Add("DEPTNAME", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                temp.Rows.Add(dr["USERNAME"].ToString(), dr["USERID"].ToString(), dr["DEPTNAME"].ToString());
            }
            this.dgvUserInfo.DataSource = temp;
            this.dgvUserInfo.SelectionChanged += new EventHandler(dgvUserInfo_SelectionChanged);
        }

        void dgvUserInfo_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvUserInfo.SelectedRows.Count < 1)
                    return;
                bool _flag = false;
                this.mUserId = this.dgvUserInfo.SelectedRows[0].Cells["userId"].Value.ToString();
                DataTable _dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtUserInfo.Instance.GetUserAteEmp(this.mUserId));
                if (_dt != null && _dt.Rows.Count > 0)
                    _flag = true;
                for (int i = 0; i < this.dgvAteEmp.Rows.Count; i++)
                {
                    if (!_flag || (_dt.Select(string.Format("craftId='{0}'", this.dgvAteEmp["craftId", i].Value.ToString().ToUpper())).Length < 1))
                        this.dgvAteEmp.Rows[i].Cells["chk"].Value = 0;
                    else
                        this.dgvAteEmp.Rows[i].Cells["chk"].Value = 1;
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_selectuser_Click(object sender, EventArgs e)
        {
            DataTable _dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtUserInfo.Instance.GetUserInfo(this.tb_username.Text.Trim(), null, null));
            if (_dt != null && _dt.Rows.Count > 0)
                this.dgvUserInfo.DataSource = _dt;
            else
            {
                _dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtUserInfo.Instance.GetUserInfo(null,this.tb_username.Text.Trim(),null));
                if (_dt != null && _dt.Rows.Count > 0)
                    this.dgvUserInfo.DataSource = _dt;
                else
                    this.dgvUserInfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtUserInfo.Instance.GetUserInfo(null,null,null));
            }
        }
        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.mUserId))
                    this.mFrm.ShowPrgMsg("没有选择任何用户!请重新选择", MainParent.MsgType.Warning);
                else
                {
                    if (MessageBoxEx.Show(string.Format("是否保存用户[{0}]的权限?", this.mUserId), "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        this.AddUserAteEmp(this.mUserId, this.dgvAteEmp.DataSource as DataTable);
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void AddUserAteEmp(string userId, DataTable mdt)
        {
          //  List<WebServices.tUserInfo.tAteEmp> lsateemp = new List<WebServices.tUserInfo.tAteEmp>();
            IList<IDictionary<string, object>> lsDic = new List<IDictionary<string, object>>();
            Dictionary<string, object> dic = null;
             DataRow[] arrDr = mdt.Select("chk=1");
            foreach (DataRow dr in arrDr)
            {
                dic = new Dictionary<string, object>();
                //lsateemp.Add(new WebServices.tUserInfo.tAteEmp()
                //{
                //    userId = userId,
                //    craftId = dr["craftId"].ToString(),
                //    craftname = dr["craftname"].ToString()
                //});
                dic.Add("USERID",userId);
                dic.Add("CRAFTID", dr["craftId"].ToString());
                dic.Add("CRAFTNAME", dr["craftname"].ToString());
                lsDic.Add(dic);
            }
            if (lsDic.Count < 1)
                return;
            string msg = RefWebService_BLL.refWebtUserInfo.Instance.AddUserAteEmp(FrmBLL.ReleaseData.ListDictionaryToJson(lsDic));
            if (msg == "OK")
                this.mFrm.ShowPrgMsg(string.Format("用户:[{0}]的ATE测试权限分配成功", userId), MainParent.MsgType.Incoming);
            else
                this.mFrm.ShowPrgMsg(msg, MainParent.MsgType.Warning);
        }

        private void dgvAteEmp_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1 && e.ColumnIndex!=-1)
            {
                if (this.dgvAteEmp["chk", e.RowIndex].Value.ToString() == "1")
                {
                    this.dgvAteEmp.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    this.dgvAteEmp.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }     
            }
        }

        private void btToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dgvAteEmp.SelectedRows.Count > 0)
            {
                for (int i = 0; i < this.dgvAteEmp.SelectedRows.Count; i++)
                {
                    if (this.dgvAteEmp.SelectedRows[i].Cells["chk"].Value.ToString() == "1")
                        this.dgvAteEmp.SelectedRows[i].Cells["chk"].Value = 0;
                    else
                        this.dgvAteEmp.SelectedRows[i].Cells["chk"].Value = 1;
                }
            }
        }

        private void tb_username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                this.bt_selectuser_Click(null, null);
        }
    }
}
