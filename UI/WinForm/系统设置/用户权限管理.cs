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
    public partial class FrmUseManage :Office2007Form// Form
    {
        public FrmUseManage(MainParent _mfrm)
        {
            InitializeComponent();
            this.mFrm = _mfrm;
        }
        private MainParent mFrm;
        private string mUserId = string.Empty;

        private void FrmUseManage_Load(object sender, EventArgs e)
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
            this.dgv_showJurisdiction.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtUserInfo.Instance.GetAllProgFunctionInfo());
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtUserInfo.Instance.GetUserInfo(null, null, null));
                 DataTable temp = new DataTable();
            temp.Columns.Add("USERNAME",typeof(string));
             temp.Columns.Add("USERID",typeof(string));
            temp.Columns.Add("DEPTNAME",typeof(string));
            foreach ( DataRow dr in dt.Rows )
            {
                temp.Rows.Add(dr["USERNAME"].ToString(),dr["USERID"].ToString(),dr["DEPTNAME"].ToString());
            }

            this.dgv_ShowUser.DataSource =temp ;

            this.dgv_ShowUser.SelectionChanged += new EventHandler(dgv_ShowUser_SelectionChanged);         
        }

        private void FrmLoad()
        {
            this.ShowJurisdictionList(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtUserInfo.Instance.GetAllProgFunctionInfo()));
        }

        private void ShowUser(DataTable dt)
        {
            this.dgv_ShowUser.Invoke(new EventHandler(delegate 
                {
                    this.dgv_ShowUser.DataSource = dt;
                }));
        }

        private void ShowJurisdictionList(DataTable dt)
        {
            this.dgv_showJurisdiction.Invoke(new EventHandler(delegate
            {
                this.dgv_showJurisdiction.DataSource = dt;
            }));
        }

        void dgv_ShowUser_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgv_ShowUser.SelectedRows.Count > 0)
            {
                #region 显示选中人员的权限
                //保存选择中的人员工号到局部变量
                this.mUserId = this.dgv_ShowUser.SelectedRows[0].Cells["userId"].Value.ToString();

                DataTable _dta = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtUserInfo.Instance.GetUserJurisdictionByUserId2(this.mUserId)); 
                if (_dta != null && _dta.Rows.Count > 0)
                {
                    this.dgv_showJurisdiction.DataSource = _dta;
                }
                
                #endregion
            }
        }

        private void bt_selectuser_Click(object sender, EventArgs e)
        {
            DataTable _dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtUserInfo.Instance.GetUserInfo(this.tb_username.Text.Trim(),null,null));
            if (_dt != null && _dt.Rows.Count > 0)
            {
            }
            // this.dgv_ShowUser.DataSource = _dt;
            else
            {
                _dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtUserInfo.Instance.GetUserInfo(null, this.tb_username.Text.Trim(), null));
                if (_dt != null && _dt.Rows.Count > 0)
                {
                }
                // this.dgv_ShowUser.DataSource = _dt;
                else
                    //  this.dgv_ShowUser.DataSource =
                    _dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtUserInfo.Instance.GetUserInfo(null, null, null));
            }

            DataTable temp = new DataTable();
            temp.Columns.Add("USERNAME", typeof(string));
            temp.Columns.Add("USERID", typeof(string));
            temp.Columns.Add("DEPTNAME", typeof(string));
            foreach (DataRow dr in _dt.Rows)
            {
                temp.Rows.Add(dr["USERNAME"].ToString(), dr["USERID"].ToString(), dr["DEPTNAME"].ToString());
            }

            this.dgv_ShowUser.DataSource = temp;
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            #region  获取权限列表实体
            string LogList = this.mUserId;
            IList<IDictionary<string, object>> lsDic = new List<IDictionary<string, object>>();
            Dictionary<string, object> dic = null;
            DataTable _datatable = FrmBLL.publicfuntion.getNewTable(this.dgv_showJurisdiction.DataSource as DataTable, "Jurisdiction=1");
            foreach (DataRow dr in _datatable.Rows)
            {
                dic = new Dictionary<string, object>();
                dic.Add("FUNID", dr["funId"].ToString());
                dic.Add("USERID", this.mUserId);
                dic.Add("PROGID", dr["progid"].ToString());
                lsDic.Add(dic);
                LogList +=","+ dr["progid"].ToString()  ;
            }
            #endregion
            #region  插入数据
            string err;
            if (string.IsNullOrEmpty(err = RefWebService_BLL.refWebtUserInfo.Instance.AddUserJurisdiction(FrmBLL.ReleaseData.ListDictionaryToJson(lsDic))))
            {
                this.mFrm.ShowPrgMsg("数据插入成功", MainParent.MsgType.Incoming);
                FrmBLL.publicfuntion.Insert_Trace(mFrm.gUserInfo.userId+" Add:"+LogList);
                FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "UserFun", "add_Fun", LogList.Length > 510 ? LogList.Substring(0, 510) : LogList);
           
            }
            else
                this.mFrm.ShowPrgMsg(err, MainParent.MsgType.Error);
            #endregion
        }

        private void tb_username_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tb_username.Text)) && (e.KeyCode == Keys.Enter))
            {
                bt_selectuser_Click(null,null);
            }
        }

    }
}
