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
    public partial class fuserinfo :Office2007Form// Form
    {
        public fuserinfo(MainParent Finfo)
        {
            InitializeComponent();
            sFinfo = Finfo;
        }

        MainParent sFinfo;
        //DataTable TotalData;
        string UserId;

        private  enum sFlag     
        {
             新增,
             修改
         }
        sFlag sSFlag;


        private void fuserinfo_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.sFinfo.gUserInfo.rolecaption == "系统开发员")
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
            QuertAllUser();
            loadRoleInfo();
            QueryDeptInfo();
            panel1.Visible = false;
        }

        public  void loadRoleInfo()
        {

            comrolecaption.Items.Clear();
            //sSQL = "select * from tRoleInfo";
            //TotalData = BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);
            DataTable _dt =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtRoleInfo.Instance.GetRoleInfo());// BLL.tRoleInfo.GetRoleInfo();
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                comrolecaption.Items.Add(_dt.Rows[i][0].ToString());
            }
            this.DialogResult = DialogResult.OK;
            //sSQL = "select * from tRoleInfo";
            //TotalData = BLL.MsSqllib.Instance.ExecuteQuerySQL(sSQL);
            //for (int i = 0; i <= TotalData.Rows.Count - 1; i++)
            //{
            //    comrolecaption.Items.Add(TotalData.Rows[i][0].ToString());
            //}

        }
        private Dictionary<string, string> mDeptandFac = new Dictionary<string, string>();
        private Dictionary<string, string> mDeptAndFacId = new Dictionary<string, string>();
        private void QueryDeptInfo()
        {
            //combdeptname.Items.Clear();
            //sSQL = "select * from tdeptinfo";
            //TotalData = BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);
            if (this.mDeptandFac.Count < 1)
            {
                DataTable _dt =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtDeptInfo.Instance.GetDeptInfo());// BLL.tDeptInfo.GetDeptInfo();
                for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                {
                    combdeptname.Items.Add(_dt.Rows[i]["部门名称"].ToString());
                    this.mDeptandFac.Add(_dt.Rows[i]["部门名称"].ToString(), _dt.Rows[i]["负责人"].ToString());
                    this.mDeptAndFacId.Add(_dt.Rows[i]["部门名称"].ToString(), _dt.Rows[i]["工厂编号"].ToString());
                }
            }
        }

        private void QuertAllUser()
        {
            //sSQL = "select userid as 工号,rolecaption as 角色名称,deptname as 部门,facid as 工厂编号,username as 用户名,"+
            //      " pwd as 密码,userphone as 联系电话,useremail as 电子邮箱,userstatus as 用户状态 from tUserInfo";
            //dataGridViewX1.DataSource = BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);
            this.dataGridViewX1.DataSource =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo());// BLL.tUserInfo.GetUserInfo();
        }


        private void buttonX1_Click(object sender, EventArgs e)
        {
            fRoleInfo dz = new fRoleInfo(sFinfo,this);
            dz.ShowDialog();
           
        }

        private void butadd_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            butdelete.Enabled = false;
            butmodify.Enabled = false;
            combdeptname.Text = "";        
            edtpwd.Text = "";
            edtuseremail.Text = "";
            edtuserid.Text = "";
            edtusername.Text = "";
            edtuserphone.Text = "";            
            edtuserid.Enabled = true;
            combfacId.Text = "";
            combdeptname.Text="";
            comrolecaption.Text="";     
           
            sSFlag = sFlag.新增;
           
        }

        private void butmodify_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            edtuserid.Enabled = false;
            butadd.Enabled = false;
            butdelete.Enabled = false;             
            sSFlag = sFlag.修改;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            try
            {
                switch (sSFlag)
                {
                    case sFlag.新增:
                        if (!string.IsNullOrEmpty(edtuserid.Text) && !string.IsNullOrEmpty(comrolecaption.Text) &&
                           !string.IsNullOrEmpty(edtusername.Text))
                        {

                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("USERID", this.edtuserid.Text.Trim());
                            dic.Add("ROLECAPTION", this.comrolecaption.Text.Trim());
                            dic.Add("DEPTNAME", this.combdeptname.Text.Trim());
                            dic.Add("FACID", this.mDeptAndFacId.Count < 1 ? "" : this.mDeptAndFacId[this.combdeptname.Text.Trim()]);
                            dic.Add("USERNAME", this.edtusername.Text.Trim());
                            dic.Add("PWD", this.edtpwd.Text.Trim());
                            dic.Add("USEREMAIL", this.edtuseremail.Text.Trim());
                            dic.Add("USERPHONE", this.edtuserphone.Text.Trim());
                            dic.Add("USERSTATUS", this.radioTrue.Checked ? "1" : "0");
                            string _StrErr = refWebtUserInfo.Instance.InsertUserInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));

                            FrmBLL.publicfuntion.InserSystemLog(sFinfo.gUserInfo.userId, "用户权限", "Add", "UserId: " + edtuserid.Text.Trim() + " Rolecaption: " + comrolecaption.Text.Trim() + " DeptName:" + combdeptname.Text.Trim() + " FacId:" + combfacId.Text.Trim());

                            QuertAllUser();
                            sFinfo.ShowPrgMsg(_StrErr + "--新增权限成功 ", MainParent.MsgType.Error);
                            panel1.Visible = false;
                            butadd.Enabled = true;
                            butmodify.Enabled = true;
                            butdelete.Enabled = true;

                        }
                        else
                        {
                            sFinfo.ShowPrgMsg("工号,角色名称,用户名,用户状态不能为空 ", MainParent.MsgType.Error);
                        }
                        break;
                    case sFlag.修改:
                        if (!string.IsNullOrEmpty(comrolecaption.Text) && !string.IsNullOrEmpty(edtusername.Text))
                        {
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("USERID", this.edtuserid.Text.Trim());
                            dic.Add("ROLECAPTION", this.comrolecaption.Text.Trim());
                            dic.Add("DEPTNAME", this.combdeptname.Text.Trim());
                            dic.Add("FACID", this.mDeptAndFacId.Count < 1 ? "" : this.mDeptAndFacId[this.combdeptname.Text.Trim()]);
                            dic.Add("USERNAME", this.edtusername.Text.Trim());
                            dic.Add("PWD", this.edtpwd.Text.Trim());
                            dic.Add("USEREMAIL", this.edtuseremail.Text.Trim());
                            dic.Add("USERPHONE", this.edtuserphone.Text.Trim());
                            dic.Add("USERSTATUS", this.radioTrue.Checked ? "1" : "0");
                            refWebtUserInfo.Instance.EditUserInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));


                            FrmBLL.publicfuntion.InserSystemLog(sFinfo.gUserInfo.userId, "用户权限", "Modify", "UserId: " + edtuserid.Text.Trim() + " Rolecaption: " + comrolecaption.Text.Trim() + " DeptName:" + combdeptname.Text.Trim() + " FacId:" + combfacId.Text.Trim());
                            QuertAllUser();
                            sFinfo.ShowPrgMsg("修改权限成功 ", MainParent.MsgType.Error);
                            MessageBox.Show("修改权限成功");
                            panel1.Visible = false;
                            butadd.Enabled = true;
                            butmodify.Enabled = true;
                            butdelete.Enabled = true;
                        }
                        else
                        {
                            sFinfo.ShowPrgMsg("角色名称,用户名,用户状态不能为空 ", MainParent.MsgType.Error);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                sFinfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            butadd.Enabled = true;
            butmodify.Enabled = true;
            butdelete.Enabled = true;
        }

        private void butdelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要删除此人员权限吗?\r\n  工号=" + UserId, "删除信息提示 ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                     refWebtUserInfo.Instance.DeleteUserInfoByUserId(UserId);                 
                     FrmBLL.publicfuntion.InserSystemLog(sFinfo.gUserInfo.userId, "用户权限", "删除", "工号: " + edtuserid.Text.Trim() + " 角色名称: " + comrolecaption.Text.Trim() + " 部门:" + combdeptname.Text.Trim() + " 工厂编号:" + combfacId.Text.Trim());
                    QuertAllUser();
                    sFinfo.ShowPrgMsg("删除权限成功 ", MainParent.MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                sFinfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void dataGridViewX1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (butmodify.Enabled == true || butdelete.Enabled == true)
                {
                    if (e.RowIndex != -1)
                    {
                        edtuserid.Text = UserId = dataGridViewX1[0, e.RowIndex].Value.ToString();
                       // comrolecaption.Text =   dataGridViewX1[3, e.RowIndex].Value.ToString();
                        comrolecaption.SelectedIndex = comrolecaption.Items.IndexOf(dataGridViewX1[2, e.RowIndex].Value.ToString());
                       // combdeptname.Text = dataGridViewX1[2, e.RowIndex].Value.ToString();
                        combdeptname.SelectedIndex =combdeptname.Items.IndexOf( dataGridViewX1[3, e.RowIndex].Value.ToString());
                        combfacId.Text = dataGridViewX1[4, e.RowIndex].Value.ToString();
                        edtusername.Text = dataGridViewX1[1, e.RowIndex].Value.ToString();
                        edtpwd.Text = dataGridViewX1[5, e.RowIndex].Value.ToString();
                        edtuserphone.Text = dataGridViewX1[6, e.RowIndex].Value.ToString();
                        edtuseremail.Text = dataGridViewX1[7, e.RowIndex].Value.ToString();
                        if (dataGridViewX1[8, e.RowIndex].Value.ToString() == true.ToString())
                        {
                            radioTrue.Checked = true;
                        }
                        if (dataGridViewX1[8, e.RowIndex].Value.ToString() == false.ToString())
                        {
                            radiofalse.Checked = true;
                        }
                        //  edtuserstatus.Text = dataGridViewX1[8, e.RowIndex].Value.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                sFinfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (panel1.Visible == false)
            {
                butmodify_Click(null, EventArgs.Empty);
            }
        }       


        private void combdeptname_SelectionChangeCommitted(object sender, EventArgs e)
        {
            combfacId.Text = "";
            combfacId.Items.Clear();
           // this.combfacId.Text = this.mDeptandFac[this.combdeptname.Text.Trim()];
            this.combfacId.Text = this.mDeptAndFacId[this.combdeptname.Text.Trim()];
            //TotalData.Clear();
            //sSQL = string.Format("select * from tDeptInfo where deptname='{0}'", combdeptname.Text.Trim());
            //TotalData = BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);

            //for (int z = 0; z <= TotalData.Rows.Count - 1; z++)
            //{
            //    combfacId.Items.Add(TotalData.Rows[z][1].ToString());
            //}
        }

        private void dataGridViewX1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 把第4列显示*号，*号的个数和实际数据的长度相同
            if (e.ColumnIndex == 5)
            {
                if (e.Value != null && e.Value.ToString().Length > 0)
                {
                    e.Value = new string('*', e.Value.ToString().Length);
                }
            }

        }

        private void bt_userJur_Click(object sender, EventArgs e)
        {
            this.sFinfo.imbt_userJurisdictionmanage_Click(null, null);
        }

        private void tb_query_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_query.Text) && e.KeyCode == Keys.Enter)
            {
                for (int i = 0; i < dataGridViewX1.Rows.Count; i++)
                {
                    if (dataGridViewX1[0, i].Value.ToString() == tb_query.Text.Trim())
                    {
                        dataGridViewX1[0, i].Selected = true;

                        dataGridViewX1.CurrentCell = dataGridViewX1[0, i];
                        // dataGridViewX1.Rows[i].Selected = true;
                        dataGridViewX1.FirstDisplayedScrollingRowIndex = i;
                    }
                }
                tb_query.SelectAll();
            }
        }

        private void imbt_ClearUser_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("确认要清除用户[{0}]权限", UserId), "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
              string _StrErr= refWebtUserInfo.Instance.Clear_User_Info(UserId);
              if (_StrErr == "OK")
                  MessageBox.Show("清除成功");
              else
                  MessageBox.Show("清除权限失败:"+_StrErr);
              FrmBLL.publicfuntion.InserSystemLog(sFinfo.gUserInfo.userId, "用户权限", "User_Clear", "Clear UserId:"+UserId);
              QuertAllUser();
            }
        }

   

           

      
    }
}