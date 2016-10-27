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
    public partial class FrmPalletRePrint : Office2007Form //Form
    {
        public FrmPalletRePrint(FrmPallet Print)
        {
            InitializeComponent();
            sMain = Print;
        }

        FrmPallet sMain;
        private void FrmPalletRePrint_Load(object sender, EventArgs e)
        {
           
        }

       

        private void tbPrintQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }   
      

        private void tbPrintQTY_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPrintQTY.Text))
            {
                tbPrintQTY.Text = "2";
            }
        }
        
        private void tbPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbPalletNo.Text)) && (e.KeyCode == Keys.Enter))
            {                
               DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPalletInfo.Instance.GetPalletAndMpalletInfo(tbPalletNo.Text,0));
            
               if (dt.Rows.Count > 0)
               {
                   DataTable dtProduct = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtProduct.Instance.GetProductByPartNumber(dt.Rows[0][6].ToString()));
                   if (dtProduct.Rows.Count > 0)
                   {

                       DataTable dtPrint = new DataTable("LabelPring");
                       dtPrint.Columns.Add("Colnum", typeof(string));
                       dtPrint.Columns.Add("Values", typeof(string));
                       dtPrint.Rows.Add("PALLETNO", dt.Rows[0]["PALLETNUMBER"].ToString());
                       dtPrint.Rows.Add("MPALLETNO", dt.Rows[0]["MPALLETNUMBER"].ToString());
                       dtPrint.Rows.Add("PARTNUMBER", dt.Rows[0]["PARTNUMBER"].ToString());
                       dtPrint.Rows.Add("PRODUCTNAME", dtProduct.Rows[0]["PRODUCTNAME"].ToString());
                       dtPrint.Rows.Add("PRODUCTCOLOR", dtProduct.Rows[0]["PRODUCTCOLOR"].ToString());                       
                   
                       int x = 0;
                       int Total = 0;
                       foreach (DataRow dr in dt.Rows)
                       {
                           x++;
                           dtPrint.Rows.Add("CARTON_NO" + x.ToString(), dr[2].ToString());
                           dtPrint.Rows.Add("MCARTON_NO" + x.ToString(), dr[3].ToString());
                           Total += Convert.ToInt32(dr[4].ToString());
                       }
                       dtPrint.Rows.Add("QTY", Total.ToString());

                       sMain.PrintPalletLabel(dtPrint,Convert.ToInt32(tbPrintQTY.Text), sMain.LabFilepatch);
                       tbPalletNo.Text = "";
                   }
                   else
                   {
                       MessageBox.Show("产品信息没有找到");
                   }
               }
               else
               {
                   MessageBox.Show("栈板号输入错误");
                   tbPalletNo.SelectAll();
               }

            }
        }
    }
}
