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
    public partial class fRoleInfo :Office2007Form// Form
    {
        public fRoleInfo(MainParent Finfo,fuserinfo dt)
        {
            InitializeComponent();
            sFinfo = Finfo;
            sdt = dt;
        }

        MainParent sFinfo;
        fuserinfo sdt;

        private void fRoleInfo_Load(object sender, EventArgs e)
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
         QueryAllData();
         edtrolecaption.SelectAll();
         edtrolecaption.Focus();
        }

        private void QueryAllData()
        {
         //   sSQL = "select rolecaption as 角色名称,rolelevel as 角色等级,roledesc as 角色说明 from tRoleInfo";
         //   dataGridViewX1.DataSource =  refWebExecuteSqlCmd.Instance.ExecuteQuerySQL(sSQL);// BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);
            dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtRoleInfo.Instance.QueryRoleInfo());
        }

        private void dataGridViewX1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
          //  this.dataGridViewX1[e.ColumnIndex, e.RowIndex].ToolTipText = "双击鼠标删除";
        }

        private void edtrolecaption_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && edtrolecaption.Text.Trim() != "")
            {
                edtrolelevel.SelectAll();
                edtrolelevel.Focus();
            }
        }

        private void edtrolelevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && edtrolelevel.Text.Trim() != "")
            {
                edtroledesc.SelectAll();
                edtroledesc.Focus();
            }
        }

        private void edtroledesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && edtroledesc.Text.Trim() != "")
            {
                butadd.Focus();
            }
        }

        private void butadd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(edtrolecaption.Text) && !string.IsNullOrEmpty(edtrolelevel.Text) && !string.IsNullOrEmpty(edtroledesc.Text))
            {
                for (int i = 0; i <= dataGridViewX1.RowCount-1; i++)
                {
                    if (edtrolecaption.Text.Trim() == dataGridViewX1[0, i].Value.ToString())
                    {
                        MessageBox.Show("角色名称重复,请检查");
                        sFinfo.ShowPrgMsg("角色名称重复,请检查.", MainParent.MsgType.Error);
                        return;
                    }

                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("ROLECAPTION",edtrolecaption.Text.Trim());
                 dic.Add("ROLELEVEL",int.Parse(this.edtrolelevel.Text.Trim()));
                 dic.Add("ROLEDESC", edtroledesc.Text.Trim());

                 refWebtRoleInfo.Instance.InsertRoleInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));
                // refWebtRoleInfo.Instance.InsertRoleInfo(new WebServices.tRoleInfo.tRoleInfo1()
                //{
                //    rolecaption = this.edtrolecaption.Text.Trim(),
                //    rolelevel = int.Parse(this.edtrolelevel.Text.Trim()),
                //    roledesc = this.edtroledesc.Text.Trim()
                //});

                 FrmBLL.publicfuntion.InserSystemLog(sFinfo.gUserInfo.userId, "角色", "新增", "角色名称: " + edtrolecaption.Text.Trim() + " 角色等级: " + edtrolelevel.Text.Trim() + " 角色说明: " + edtroledesc.Text.Trim());
                
                QueryAllData();
                edtrolecaption.Text = "";
                edtrolelevel.Text = "";
                edtroledesc.Text = "";
                edtrolecaption.SelectAll();
                        sFinfo.ShowPrgMsg("新增角色完成.",MainParent.MsgType.Incoming);
            }
            else
            {
                MessageBox.Show("输入项不能为空,请输入完整后再添加.");
                sFinfo.ShowPrgMsg("输入项不能为空,请输入完整后再添加.", MainParent.MsgType.Error);
            }
        }

        private void dataGridViewX1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
                       
        }

        private void fRoleInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            sdt.loadRoleInfo();          
    
        }
    }
}
