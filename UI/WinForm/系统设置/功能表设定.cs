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
    public partial class fworkfunctioninfo :Office2007Form// Form
    {
        public fworkfunctioninfo(MainParent Finfo)
        {
            InitializeComponent();
            sFinfo = Finfo;
        }

        MainParent sFinfo;
        string sWfid;
        string MsgErr;

        private enum sFlag
        {
            新增,
            修改
        }

        sFlag ssFlag;

        private void fworkfunctioninfo_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.sFinfo.gUserInfo.rolecaption == "系统开发员")
            {
                List<WebServices.tUserInfo.tFunctionList> lsfunls = new List<WebServices.tUserInfo.tFunctionList>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                FrmBLL.publicfuntion.AddProgInfo(new WebServices.tUserInfo.tProgInfo()
                {
                    progid = this.Name,
                    progname = this.Text,
                    progdesc = this.Text

                }, lsfunls);
            }
            #endregion
            QueryAllData();
            QuerysRoleInfo();
            panel1.Visible = false;
        }

        private void QuerysRoleInfo()
        {
           // string sSQL = "select * from tRoleInfo";
           // DataTable Dc =  refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);// BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);
            DataTable Dc = FrmBLL.ReleaseData.arrByteToDataTable(refWebtRoleInfo.Instance.QueryRoleInfo());
            
            for (int x = 0; x <= Dc.Rows.Count - 1; x++)
            {
                edtrolecaption.Items.Add(Dc.Rows[x][0].ToString());

            }

        }


        private void QueryAllData()
        {
         //   string sSQL = "select wfid as 功能编号,rolecaption as 角色名称,wfcaption as  功能名称,wfdesc as 功能说明,workurl as 功能连接 from tWorkFunctionInfo";
          //  dataGridViewX1.DataSource =  refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);// BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);
            dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWorkFunctionInfo.Instance.GetALLWorkFunctionInfo());
        
        }

        private void butadd_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            edtwfid.Text = "";
            edtwfdesc.Text = "";
            edtwfcaption.Text = "";
            edtrolecaption.Text = "";
            edtworkurl.Text = "";
            butmodify.Enabled = false;
            butdelete.Enabled = false;
            ssFlag = sFlag.新增;
            edtwfid.SelectAll();
            edtwfid.Focus();


        }

        private void butmodify_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            butadd.Enabled = false;
            butdelete.Enabled = false;
            edtwfid.Enabled = false;


            ssFlag = sFlag.修改;

        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            butadd.Enabled = true;
            butmodify.Enabled = true;
            butdelete.Enabled = true;
            panel1.Visible = false;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (edtwfid.Text.Trim() == "" || edtwfcaption.Text.Trim() == "")
            {
                sFinfo.ShowPrgMsg("功能编号或功能名称为空,请检查.", MainParent.MsgType.Error);
            }
            else
            {

                switch (ssFlag)
                {
                    case sFlag.新增:

                       // sSQL = string.Format("insert into tWorkFunctionInfo (wfId,rolecaption,wfcaption,wfdesc,workurl) VALUES ('{0}','{1}','{2}','{3}','{4}') ",
                        //    edtwfid.Text.Trim(), edtrolecaption.Text.Trim(), edtwfcaption.Text.Trim(), edtwfdesc.Text.Trim(), edtworkurl.Text.Trim());
                       //  refWebExecuteSqlCmd.Instance.ExecteSQLNonQuery(sSQL);// BLL.BllMsSqllib.Instance.ExecteSQLNonQuery(sSQL);
                         refWebtWorkFunctionInfo.Instance.InsertToWorkFunctionInfo(new WebServices.tWorkFunctionInfo.tWorkFunctionInfoTable()
                             {
                                 wfid = edtwfid.Text.Trim(),
                                 rolecaption = edtrolecaption.Text.Trim(),
                                 wfcaption = edtwfcaption.Text.Trim(),
                                 wfdesc = edtwfdesc.Text.Trim(),
                                 workurl = edtworkurl.Text.Trim()
                             });


                       refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG() 
                        {
                            userId = sFinfo.gUserInfo.userId,
                            prg_name = "功能表设定",
                            action_type = "新增",
                            action_desc = "功能编号: "+edtwfid.Text.Trim()+" 角色名称: "+edtrolecaption.Text.Trim()+" 功能名称: "+edtwfcaption.Text.Trim()
                        });
                        QueryAllData();
                        sFinfo.ShowPrgMsg("新增功能表资料完成 "+MsgErr, MainParent.MsgType.Incoming);
                        panel1.Visible = false;
                        butadd.Enabled = true;
                        butmodify.Enabled = true;
                        butdelete.Enabled = true;

                        break;

                    case

                sFlag.修改:

                      //  sSQL =string.Format( "update tWorkFunctionInfo set rolecaption='{0}',wfcaption='{1}',wfdesc='{2}',workurl='{3}' where wfId='{4}'  ",
                    //        edtrolecaption.Text.Trim(), edtwfcaption.Text.Trim(), edtwfdesc.Text.Trim(), edtworkurl.Text.Trim(), edtwfid.Text.Trim());
                      //   refWebExecuteSqlCmd.Instance.ExecteSQLNonQuery(sSQL);// BLL.BllMsSqllib.Instance.ExecteSQLNonQuery(sSQL);

                         refWebtWorkFunctionInfo.Instance.UpdateWorkFunctionInfo(new WebServices.tWorkFunctionInfo.tWorkFunctionInfoTable()
                         {
                             wfid = edtwfid.Text.Trim(),
                             rolecaption = edtrolecaption.Text.Trim(),
                             wfcaption = edtwfcaption.Text.Trim(),
                             wfdesc = edtwfdesc.Text.Trim(),
                             workurl = edtworkurl.Text.Trim(),

                         });



                        //BLL.RecodeSystemLog.InsertSystemLog(new Entity.T_SYSTEM_LOG
                        //{
                        //    userId = sFinfo.gUserInfo.userId,
                        //    prg_name = "功能表设定",
                        //    action_type = "修改",
                        //    action_desc = "功能编号: " + edtwfid.Text.Trim() + " 角色名称: " + edtrolecaption.Text.Trim() + " 功能名称: " + edtwfcaption.Text.Trim()
                        //}, out MsgErr);

                         refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG() 
                        {
                            userId = sFinfo.gUserInfo.userId,
                            prg_name = "功能表设定",
                            action_type = "修改",
                            action_desc = "功能编号: " + edtwfid.Text.Trim() + " 角色名称: " + edtrolecaption.Text.Trim() + " 功能名称: " + edtwfcaption.Text.Trim()
                        });
                        QueryAllData();
                        sFinfo.ShowPrgMsg("修改功能表资料成功 "+MsgErr, MainParent.MsgType.Incoming);
                        panel1.Visible = false;
                        butadd.Enabled = true;
                        butmodify.Enabled = true;
                        butdelete.Enabled = true;

                        break;

                    default:

                        break;
                }
            }
        }

        private void dataGridViewX1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (butmodify.Enabled == true || butdelete.Enabled == true)
            {
                if (e.RowIndex != -1)
                {
                    edtwfid.Text = sWfid = dataGridViewX1[0, e.RowIndex].Value.ToString();
                    edtrolecaption.Text = dataGridViewX1[1, e.RowIndex].Value.ToString();
                    edtwfcaption.Text = dataGridViewX1[2, e.RowIndex].Value.ToString();
                    edtwfdesc.Text = dataGridViewX1[3, e.RowIndex].Value.ToString();
                    edtworkurl.Text = dataGridViewX1[4, e.RowIndex].Value.ToString();
                }
            }
        }

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (panel1.Visible == false)
            {
                butmodify_Click(null, EventArgs.Empty);
            }
        }

        private void edtwfid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && edtwfid.Text.Trim() != "")
            {
                edtrolecaption.SelectAll();
                edtrolecaption.Focus();
            }
        }

        private void edtrolecaption_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(edtrolecaption.Text))
            {
                edtwfcaption.SelectAll();
                edtwfcaption.Focus();
            }
        }

        private void edtwfcaption_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(edtwfcaption.Text))
            {
                edtwfdesc.SelectAll();
                edtwfdesc.Focus();
            }
        }

        private void edtwfdesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(edtwfdesc.Text))
            {
                edtworkurl.SelectAll();
                edtworkurl.Focus();
            }
        }

        private void edtworkurl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(edtworkurl.Text))
            {
                butOK.Focus();
            }
        }

        private void butdelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除此功能编号吗?\r\n  功能编号=" + sWfid, "删除信息提示 ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
             //   sSQL = string.Format("delete tWorkFunctionInfo where wfid='{0}' ",edtwfid.Text.Trim());
              //   refWebExecuteSqlCmd.Instance.ExecteSQLNonQuery(sSQL);// BLL.BllMsSqllib.Instance.ExecteSQLNonQuery(sSQL);
                 refWebtWorkFunctionInfo.Instance.DeleteWorkFunctionInfo(edtwfid.Text.Trim());

                //BLL.RecodeSystemLog.InsertSystemLog(new Entity.T_SYSTEM_LOG
                //{
                //    userId = sFinfo.gUserInfo.userId,
                //    prg_name = "功能表设定",
                //    action_type = "删除",
                //    action_desc = "功能编号: " + edtwfid.Text.Trim() + " 角色名称: " + edtrolecaption.Text.Trim() + " 功能名称: " + edtwfcaption.Text.Trim()
                //}, out MsgErr);

                 refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG() 
                {
                    userId = sFinfo.gUserInfo.userId,
                    prg_name = "功能表设定",
                    action_type = "删除",
                    action_desc = "功能编号: " + edtwfid.Text.Trim() + " 角色名称: " + edtrolecaption.Text.Trim() + " 功能名称: " + edtwfcaption.Text.Trim()
                });

                QueryAllData();
                sFinfo.ShowPrgMsg("删除功能表资料成功 "+MsgErr, MainParent.MsgType.Incoming);
            }
        }
    }
}
