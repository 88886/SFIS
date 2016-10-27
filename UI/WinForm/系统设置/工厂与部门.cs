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
    public partial class fFacDeptInfo :Office2007Form// Form
    {
        public fFacDeptInfo(MainParent sInfo)
        {
            InitializeComponent();
            sFInfo = sInfo;
        }
       // string sSQL;
        MainParent sFInfo;
        string MsgErr=string.Empty;
        string sDeptname;

        Dictionary<string, string> facNameAndId = new Dictionary<string, string>();

        private enum sFlag
        {
            新增,
            修改
        }
        sFlag sSFlag;
        private void fFacDeptInfo_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.sFInfo.gUserInfo.rolecaption == "系统开发员")
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
            try
            {
                QueryDeptInfo();
                //QueryFacInfo();
                panel1.Visible = false;
            }
            catch (Exception ex)
            {
                sFInfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void QueryDeptInfo()
        {
            //sSQL = "select deptname as 部门名称,facid as 工厂编号,userid as 负责人,deptdesc as 部门描述 from tDeptInfo";
            //dataGridViewX1.DataSource = BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);  
            try
            {
                this.dataGridViewX1.DataSource =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtDeptInfo.Instance.GetDeptInfo());// BLL.tDeptInfo.GetDeptInfo();
            }
            catch (Exception ex)
            {
                sFInfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

        }


        public void QueryFacInfo()
        {
            try
            {
                combfacname.Items.Clear();
                this.facNameAndId.Clear();
                //sSQL = "select * from tfacinfo";
                //sDataTable = BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);
               // if (this.facNameAndId.Count < 1)
               // {
                DataTable _dt =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtFacInfo.Instance.GetFacInfo());// BLL.tFacInfo.GetFacInfo();
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        combfacname.Items.Add(_dt.Rows[i]["facname"].ToString());
                        this.facNameAndId.Add(_dt.Rows[i]["facname"].ToString(), _dt.Rows[i]["facId"].ToString());
                    }
               // }
            }
            catch (Exception ex)
            {
                sFInfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void butadd_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            edtuserId.Text="";
            combfacname.Text = "";
            edtdeptname.Text = "";
            edtdeptdesc.Text = "";
            butmodify.Enabled = false;
            butdelete.Enabled = false;
            combfacname.Enabled = true;
            sSFlag = sFlag.新增;
        }

        private void butmodify_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            butdelete.Enabled = false;
            butadd.Enabled = false;
            combfacname.Enabled = false;
            sSFlag = sFlag.修改;
        }

        private void butcancel_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            butadd.Enabled = true;
            butmodify.Enabled = true;
            butdelete.Enabled = true;
        }
   
        private void butOK_Click(object sender, EventArgs e)
        {
            try
            {
                switch (sSFlag)
                {
                    case sFlag.新增:

                        if (!string.IsNullOrEmpty(edtdeptname.Text) && !string.IsNullOrEmpty(combfacname.Text))
                        {

                            //sSQL = string.Format("insert into tDeptInfo (deptname,facId,userId,deptdesc) VALUES ('{0}','{1}','{2}','{3}')",
                            //    edtdeptname.Text.Trim(), combfacname.Text.Trim(), edtuserId.Text.Trim(), edtdeptdesc.Text.Trim());
                            //BLL.BllMsSqllib.Instance.ExecteSQLNonQuery(sSQL);

                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("DEPTNAME", edtdeptname.Text.Trim());
                            dic.Add("FACID", this.facNameAndId[combfacname.Text.Trim()]);
                            dic.Add("USERID",this.edtuserId.Text.Trim());
                            dic.Add("DEPTDESC", edtdeptdesc.Text.Trim());
                            refWebtDeptInfo.Instance.InsertDeptInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));
                            // refWebtDeptInfo.Instance.InsertDeptInfo(new WebServices.tDeptInfo.tDeptInfo1() 
                            //{
                            //    deptname = edtdeptname.Text.Trim(),
                            //    deptdesc = edtdeptdesc.Text.Trim(),
                            //    facId = this.facNameAndId[combfacname.Text.Trim()],
                            //    userId = this.edtuserId.Text.Trim()
                            //});                            

                             FrmBLL.publicfuntion.InserSystemLog(sFInfo.gUserInfo.userId, "部门管理", "新增", "部门名称: " + edtdeptname.Text.Trim() + " 工厂编号: " + combfacname.Text.Trim());

                            QueryDeptInfo();
                            sFInfo.ShowPrgMsg("部门新增完成  " + MsgErr, MainParent.MsgType.Incoming);
                            edtuserId.Text = "";
                            edtdeptname.Text = "";
                            edtdeptdesc.Text = "";
                            combfacname.Text = "";
                        }
                        else
                        {
                            sFInfo.ShowPrgMsg("部门名称和工厂编号不能为空  " + MsgErr, MainParent.MsgType.Incoming);
                        }
                        break;

                    case sFlag.修改:

                        if (!string.IsNullOrEmpty(edtdeptname.Text) && !string.IsNullOrEmpty(combfacname.Text))
                        {
                            //sSQL = string.Format("update  tDeptInfo set facId='{0}',userId='{1}',deptdesc='{2}' where  deptname='{3}'",
                            //     combfacname.Text.Trim(), edtuserId.Text.Trim(), edtdeptdesc.Text.Trim(), edtdeptname.Text.Trim());
                            //BLL.BllMsSqllib.Instance.ExecteSQLNonQuery(sSQL);


                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("DEPTNAME", edtdeptname.Text.Trim());
                            dic.Add("FACID", this.facNameAndId[combfacname.Text.Trim()]);
                            dic.Add("USERID", this.edtuserId.Text.Trim());
                            dic.Add("DEPTDESC", edtdeptdesc.Text.Trim());
                            refWebtDeptInfo.Instance.EditDeptInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));
                            //refWebtDeptInfo.Instance.EditDeptInfo(this.edtdeptname.Text.Trim(),new WebServices.tDeptInfo.tDeptInfo1() 
                            //    {
                            //        deptdesc = this.edtdeptdesc.Text.Trim(),
                            //        deptname = this.edtdeptname.Text.Trim(),
                            //        facId = facNameAndId[this.combfacname.Text.Trim()],
                            //        userId = this.edtuserId.Text.Trim()
                            //    });
                           
                             FrmBLL.publicfuntion.InserSystemLog(sFInfo.gUserInfo.userId, "部门管理", "修改", "部门名称: " + edtdeptname.Text.Trim() + " 工厂编号: " + combfacname.Text.Trim());
                            QueryDeptInfo();
                            sFInfo.ShowPrgMsg("部门修改完成  " + MsgErr, MainParent.MsgType.Incoming);
                        }
                        else
                        {
                            sFInfo.ShowPrgMsg("部门名称和工厂编号不能为空  " + MsgErr, MainParent.MsgType.Incoming);
                        }
                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                sFInfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void butdelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要删除此部门吗?\r\r  部门名称=" + sDeptname, "删除信息提示 ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //sSQL = string.Format("delete  tDeptInfo  where  deptname='{0}'", sDeptname);
                    //BLL.BllMsSqllib.Instance.ExecteSQLNonQuery(sSQL);

                     refWebtDeptInfo.Instance.DeleteDeptInfo(sDeptname);// BLL.tDeptInfo.DeleteDeptInfo(sDeptname);
                   
                   FrmBLL.publicfuntion.InserSystemLog(sFInfo.gUserInfo.userId, "部门管理", "删除", "部门名称: " + edtdeptname.Text.Trim() + " 工厂编号: " + combfacname.Text.Trim());
                    QueryDeptInfo();
                    sFInfo.ShowPrgMsg("删除完成  " + MsgErr, MainParent.MsgType.Incoming);
                }
            }
            catch (Exception ex)
            {
                sFInfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
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
                        edtdeptname.Text = sDeptname = dataGridViewX1[0, e.RowIndex].Value.ToString();
                        combfacname.Text = dataGridViewX1[1, e.RowIndex].Value.ToString();
                        edtuserId.Text = dataGridViewX1[2, e.RowIndex].Value.ToString();
                        edtdeptdesc.Text = dataGridViewX1[3, e.RowIndex].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                sFInfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            fFacInfo dt = new fFacInfo(sFInfo);
            dt.ShowDialog();
        }

        private void bt_queryuser_Click(object sender, EventArgs e)
        {
             DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo());
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if ((dt.Columns[i].ToString().ToUpper() == "PWD") || (dt.Columns[i].ToString().ToUpper() == "密码"))
                {
                    dt.Columns.Remove(dt.Columns[i].ToString());
                }
            }
            SelectData sd = new SelectData(this, dt/* BLL.tUserInfo.GetUserInfo()*/);
            sd.ShowDialog();
        }

        private void combfacname_DropDown(object sender, EventArgs e)
        {
            this.QueryFacInfo();
        }

       
    }
}
