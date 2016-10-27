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
    public partial class FrmPalletClose : Office2007Form //Form
    {
        public FrmPalletClose(FrmPallet sPalt)
        {
            InitializeComponent();
            sMain = sPalt;
        }

        FrmPallet sMain;
        private void FrmPalletClose_Load(object sender, EventArgs e)
        {

        }

        private void tbInputPallet_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbInputPallet.Text)) && (e.KeyCode == Keys.Enter))
            {
                int flag;
                tbInputPallet.Focus();
                tbInputPallet.SelectAll();
                if (!refWebtPalletInfo.Instance.CheckCartonOrPalletClosed(tbInputPallet.Text.Trim(), 2, out flag))
                {

                    if (flag == 1)
                    {
                        MessageBox.Show("未找到栈板包装资料");
                        return;
                    }
                    else
                        if (flag == 2)
                        {
                            MessageBox.Show("栈板号找到多笔资料.");
                            return;
                        }
                }
                else
                {
                    MessageBox.Show("此栈板已经是关闭状态!");
                    return;
                }

                //BLL.tPalletInfo.UpdatePalletCloseFlag(new Entity.tPalletInfoTable()
                //{
                //    PartNumber = "",
                //    woId = "",
                //    Line = "",
                //    PalletNumber = tbInputPallet.Text.Trim(),
                //    CloseFlag = 1

                //});

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PALLETNUMBER", tbInputPallet.Text.Trim());
                dic.Add("CLOSEFLAG",1);
                refWebtPalletInfo.Instance.UpdatePalletCloseFlag(FrmBLL.ReleaseData.DictionaryToJson(dic));

                //refWebtPalletInfo.Instance.UpdatePalletCloseFlag(new WebServices.tPalletInfo.tPalletInfoTable()
                //    {
                //        PalletNumber = tbInputPallet.Text.Trim(),
                //        CloseFlag = 1
                //    });

            MessageBox.Show(" 板已关闭完成");

            }
          
        }

     
    }
}
