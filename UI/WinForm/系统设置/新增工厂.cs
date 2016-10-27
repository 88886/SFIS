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
    public partial class fFacInfo :Office2007Form// Form
    {
        public fFacInfo(MainParent sInfo/*, fFacDeptInfo FacDeptInfo*/)
        {
            InitializeComponent();
            //sFacDeptInfo = FacDeptInfo;
            sFinfo = sInfo;
        }
        MainParent sFinfo;
        //fFacDeptInfo sFacDeptInfo;
        //string sSQL;        
        private void fFacInfo_Load(object sender, EventArgs e)
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
            QueryFacInfo();
            txt_Facid.SelectAll();
            txt_Facid.Focus();
            GetFacid();

            FillFacCode();
            
        }

        private void FillFacCode()
        {
          DataTable dt =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtFacInfo.Instance.GetFacCodeList());
          List<string> ls = new List<string>();
          
          foreach (DataRow dr in dt.Rows)
          {
              facCode.Remove(dr["facCode"].ToString());
          }
          this.cbx_facCode.Items.AddRange(this.facCode.ToArray());
          this.cbx_facCode.SelectedIndex = 0;
        }
        private void QueryFacInfo()
        {
            //sSQL = "select facid as 工厂编号,facname as 工厂名称,address as 工厂地址 from tfacinfo";
            //dataGridViewX1.DataSource = BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sSQL);

            this.dataGridViewX1.DataSource =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtFacInfo.Instance.GetFacInfo());// BLL.tFacInfo.GetFacInfo();

        }

        private void edtFacid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txt_Facid.Text.Trim() != "")
            {
                txt_facname.SelectAll();
                txt_facname.Focus();
            }
        }

        private void edtfacname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txt_facname.Text.Trim() != "")
            {
                txt_address.SelectAll();
                txt_address.Focus();
            }
        }

        private void edtaddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txt_address.Text.Trim() != "")
            {
                butadd.Focus();
            }
        }

        private List<string> facCode = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private void butadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_Facid.Text) && !string.IsNullOrEmpty(txt_facname.Text)
                    && !string.IsNullOrEmpty(txt_address.Text))
                {
                    //sSQL = string.Format("insert into tFacInfo (facId,facname,address) VALUES ('{0}','{1}','{2}')",
                    //    edtFacid.Text.Trim(), edtfacname.Text.Trim(), edtaddress.Text.Trim());
                    //BLL.BllMsSqllib.Instance.ExecteSQLNonQuery(sSQL);

                    // refWebtFacInfo.Instance.InsertFacInfo(new WebServices.tFacInfo.tFacInfo1() 
                    //{
                    //    facId = edtFacid.Text.Trim(),
                    //    facname = edtfacname.Text.Trim(),
                    //    address = edtaddress.Text.Trim(),
                    //    facCode= cb_facCode.Text.Trim()
                    //});
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    FrmBLL.publicfuntion.SerializeControl(dic, panelEx2);
                    refWebtFacInfo.Instance.InsertFacInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));

                    QueryFacInfo();
                   
                    FrmBLL.publicfuntion.InserSystemLog(sFinfo.gUserInfo.userId, "工厂新增", "新增", "工厂编号: " + txt_Facid.Text.Trim() + " 工厂名称: " + txt_facname.Text.Trim() + " 工厂地址:" + txt_address.Text.Trim());

                    txt_Facid.Text = "";
                    txt_facname.Text = "";
                    txt_address.Text = "";
                    txt_Facid.Focus();
                    MessageBox.Show("新增工厂资料完成 ", "提示", MessageBoxButtons.OK);
                    GetFacid();
                    FillFacCode();
                }
                else
                {
                    MessageBox.Show("工厂编号,工厂名称,工厂地址都不能为空 ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                sFinfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private void fFacInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //sFacDeptInfo.QueryFacInfo();            
        }

        private void labelX4_Click(object sender, EventArgs e)
        {
            
        }

        private void GetFacid()
        {
            txt_Facid.Text = "";// BLL.BllMsSqllib.Instance.GetSeqBasics;
        }

        private void bt_getid_Click(object sender, EventArgs e)
        {
            try
            {
                txt_Facid.Text =  refWebExecuteSqlCmd.Instance.GetSeqBasics();// BLL.BllMsSqllib.Instance.GetSeqBasics;
            }
            catch (Exception ex)
            {
                sFinfo.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
    }
}
