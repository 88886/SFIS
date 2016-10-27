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
    public partial class fCustomerInfo : Office2007Form// Form
    {
        public fCustomerInfo(MainParent Finfo, Office2007Form dt)
        {
            InitializeComponent();
            sFinfo = Finfo;
            sdt = dt;
        }

        MainParent sFinfo;
        Office2007Form sdt;
        DataTable mdtcustomer = new DataTable();
        private void fCustomerInfo_Load(object sender, EventArgs e)
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
            txtCustomername.SelectAll();
            txtCustomername.Focus();
        }

        private void QueryAllData()
        {
            dataGridViewX1.DataSource = mdtcustomer = FrmBLL.ReleaseData.arrByteToDataTable(refWebtCustomer.Instance.GettCustomerAll());
        }

        private void dataGridViewX1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //  this.dataGridViewX1[e.ColumnIndex, e.RowIndex].ToolTipText = "双击鼠标删除";
        }
        #region keypress
        private void txtCustomername_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13 && txtCustomername.Text.Trim() != "")
            {
                txtAddress.SelectAll();
                txtAddress.Focus();
            }
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtAddress.Text.Trim() != "")
            {
                txtContactperson.SelectAll();
                txtContactperson.Focus();
            }
        }

        private void txtContactperson_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtContactperson.Text.Trim() != "")
            {
                txtPhone.SelectAll();
                txtPhone.Focus();
            }
        }


        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCity.SelectAll();
                txtCity.Focus();

            }
        }

        private void txtCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                butadd.Focus();
            }

        }
        #endregion

        #region  添加新的客户信息
        private void butadd_Click(object sender, EventArgs e)
        {




            if (!string.IsNullOrEmpty(txtCustomername.Text) && !string.IsNullOrEmpty(txtAddress.Text) && !string.IsNullOrEmpty(txtContactperson.Text))
            {
                for (int i = 0; i < dataGridViewX1.RowCount; i++)
                {
                    if (txtCustomername.Text.Trim() == this.dataGridViewX1["customername", i].Value.ToString() && txtContactperson.Text.Trim() == dataGridViewX1["contactperson", i].Value.ToString())
                    {
                        MessageBox.Show("客户信息已经存在,请检查");
                        sFinfo.ShowPrgMsg("客户信息已经存在,请检查.", MainParent.MsgType.Error);
                        return;
                    }

                }

                if (txtCustomername.Text.Trim() == "" || txtContactperson.Text.Trim() == "")
                {
                    sFinfo.ShowPrgMsg("客户名称重复或联系人不能为空,请检查.", MainParent.MsgType.Error);
                }
                refWebtCustomer.Instance.InsertCustomer(new WebServices.tCustomer.tCustomer1()
               {
                   customerId = "CU" + Common.RandomTimeSerial(DateTime.Now, 3),
                   customername = this.txtCustomername.Text.Trim(),
                   address = this.txtAddress.Text.Trim(),
                   contactperson = this.txtContactperson.Text.Trim(),
                   phone = this.txtPhone.Text.Trim(),
                   city = this.txtCity.Text.Trim()
               });

                QueryAllData();
                txtCustomername.Text = "";
                txtAddress.Text = "";
                txtContactperson.Text = "";
                txtCustomername.SelectAll();
                sFinfo.ShowPrgMsg("新增客户完成.", MainParent.MsgType.Incoming);
            }
            else
            {
                MessageBox.Show("输入项不能为空,请输入完整后再添加.");
                sFinfo.ShowPrgMsg("输入项不能为空,请输入完整后再添加.", MainParent.MsgType.Error);
            }
        }

        #endregion
        private void dataGridViewX1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }


        #region 双击选择客户信息
        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {               
                if (sdt is Frm_ProductOut)
                {
                    Frm_ProductOut po = sdt as Frm_ProductOut;
                    if (po.mFlag == "0")
                    {
                        (sdt as Frm_ProductOut).txtCustomerId.Text = "";
                        (sdt as Frm_ProductOut).txtCustomerId.Text = this.dataGridViewX1["customerId", e.RowIndex].Value.ToString();
                        this.DialogResult = DialogResult.OK;
                        return;
                    }
                    if (po.mFlag == "1")
                    {
                        (sdt as Frm_ProductOut).tb_customerid.Text = "";
                        (sdt as Frm_ProductOut).tb_customerid.Text = this.dataGridViewX1["customerId", e.RowIndex].Value.ToString();
                        this.DialogResult = DialogResult.OK;
                        return;
                    }
                   
                }            

            }

            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        #endregion

        private void bt_select_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_customerName.Text.Trim()))
            {
                dataGridViewX1.DataSource = mdtcustomer;
            }
            else
            {
                dataGridViewX1.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtCustomer.Instance.GetCustomerByName(this.tb_customerName.Text.Trim()));
            }
        }


    }
}

