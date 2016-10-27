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
    public partial class FrmErrorCode :Office2007Form// Form
    {
        public FrmErrorCode(MainParent msInfo)
        {
            InitializeComponent();
            sMain = msInfo;
        }
        MainParent sMain;


        private void GetErrorCode()
        {
          dgverror.DataSource=  FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtErrorCode.Instance.GetErrorCode());
        }


        private void ClearData()
        {
            txt_ErrorCode.Text = "";
            txt_errordesc.Text = "";
            txt_errordesc2.Text = "";
            EcCode = "";
            txt_ErrorCode.Enabled = true;
            txt_errordesc.Enabled = true;
            txt_errordesc2.Enabled = true;
            btErrorCode_Add.Enabled = true;
            btErrorCode_Delete.Enabled = true;
            btErrorCode_Modify.Enabled = true;
        }
        private enum CheckAddOrModift
        {
            新增,
            修改
        }

        CheckAddOrModift sFlag;      


        private void lbok_MouseEnter(object sender, EventArgs e)
        {
            lbok.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void lbok_MouseLeave(object sender, EventArgs e)
        {
            lbok.BackColor=Color.FromArgb( 192, 192, 0);
        }

        private void lbcancel_MouseEnter(object sender, EventArgs e)
        {
            lbcancel.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void lbcancel_MouseLeave(object sender, EventArgs e)
        {
            lbcancel.BackColor = Color.FromArgb(192, 192, 0);
        }

        private void lbok_MouseDown(object sender, MouseEventArgs e)
        {
            lbok.BackColor = Color.FromArgb(255, 128, 128);
        }

        private void lbcancel_MouseDown(object sender, MouseEventArgs e)
        {
           lbcancel.BackColor = Color.FromArgb(255, 128, 128);
        }

        private void lbok_Click(object sender, EventArgs e)
        {
           
            if ((string.IsNullOrEmpty(txt_ErrorCode.Text)) || (string.IsNullOrEmpty(txt_errordesc.Text)) || (string.IsNullOrEmpty(txt_errordesc2.Text)))
            {
                sMain.ShowPrgMsg("输入参数不能为空",MainParent.MsgType.Error);
                return;
            }

            try
            {
                switch (sFlag)
                {
                    case CheckAddOrModift.新增:
                        if (!CheckECHave)
                        {
                            MessageBox.Show("错误代码: " + txt_ErrorCode.Text.Trim() + " 已经存在,不需要重复添加");
                            return;
                        }


                        //RefWebService_BLL.refWebtErrorCode.Instance.InsertErrorCode(new WebServices.tEerrorCode.tErrorCodeTable()
                        //    {
                        //        ErrorCode = txt_ErrorCode.Text.Trim(),
                        //        ErrorDesc = txt_errordesc.Text.Trim(),
                        //        ErrorDesc2 = txt_errordesc2.Text.Trim()
                        //    });
                        Dictionary<string,object> dic = new Dictionary<string,object>();
                        FrmBLL.publicfuntion.SerializeControl(dic,PalData);
                        RefWebService_BLL.refWebtErrorCode.Instance.InsertErrorCode(FrmBLL.ReleaseData.DictionaryToJson(dic));
                        
                        FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "错误代码", "新增", "错误代码: " + txt_ErrorCode.Text.Trim());

                        GetErrorCode();
                        sMain.ShowPrgMsg("新增错误代码完成", MainParent.MsgType.Incoming);
                        GetDataLocation(txt_ErrorCode.Text.Trim());
                        txt_ErrorCode.Text = "";
                        txt_errordesc.Text = "";
                        txt_errordesc2.Text = "";
                        txt_ErrorCode.Focus();                  
                        break;
                    case CheckAddOrModift.修改:
                        //RefWebService_BLL.refWebtErrorCode.Instance.UpdateErrorCode(new WebServices.tEerrorCode.tErrorCodeTable()
                        //{
                        //    ErrorCode = txt_ErrorCode.Text.Trim(),
                        //    ErrorDesc = txt_errordesc.Text.Trim(),
                        //    ErrorDesc2 = txt_errordesc2.Text.Trim()                             
                        //});
                           Dictionary<string,object> dicModify = new Dictionary<string,object>();
                           FrmBLL.publicfuntion.SerializeControl(dicModify, PalData);
                           RefWebService_BLL.refWebtErrorCode.Instance.UpdateErrorCode(FrmBLL.ReleaseData.DictionaryToJson(dicModify));
                        FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "错误代码", "修改", "错误代码: " + txt_ErrorCode.Text.Trim());

                        GetErrorCode();
                        sMain.ShowPrgMsg("修改错误代码完成", MainParent.MsgType.Incoming);
                        GetDataLocation(txt_ErrorCode.Text.Trim());
                        lbcancel_Click(null,null);                    
                        break;
                    default:
                        break;
                }
           }
         catch (Exception ems)
            {
             sMain.ShowPrgMsg(ems.Message,MainParent.MsgType.Error);
         }
        }

        private void lbcancel_Click(object sender, EventArgs e)
        {
            ClearData();
            PalData.Visible = false;
        }

        private void FrmErrorCode_Load(object sender, EventArgs e)
        { 
            #region 添加应用程序
            if (this.sMain.gUserInfo.rolecaption == "系统开发员")
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
            PalData.Visible = false;
            GetErrorCode();
            tb_Query.Focus();

        }

        private void btErrorCode_Add_Click(object sender, EventArgs e)
        {
            PalData.Visible = true;
            ClearData();
            btErrorCode_Modify.Enabled = false;
            btErrorCode_Delete.Enabled = false;
            sFlag = CheckAddOrModift.新增;
            txt_ErrorCode.Focus();
        }

        private void btErrorCode_Modify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ErrorCode.Text))
            {
                MessageBox.Show("请先选择需要修改的资料","修改提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            PalData.Visible = true;
            txt_ErrorCode.Enabled = false;
            btErrorCode_Add.Enabled = false;
            btErrorCode_Delete.Enabled = false;
            sFlag = CheckAddOrModift.修改;

        }

        private void btErrorCode_Delete_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(EcCode))
            {
                 MessageBox.Show("请先选择需要删除的错误代码","删除信息提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                 return;
            }

            if (MessageBox.Show("确定要删除错误代码: " + EcCode, "删除信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                RefWebService_BLL.refWebtErrorCode.Instance.DeleteErrorCode(txt_ErrorCode.Text.Trim());

                        
                FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "ERRCODE", "DELETE", "ERRCODE: " + txt_ErrorCode.Text.Trim());

                GetErrorCode();
                sMain.ShowPrgMsg("错误代码删除成功",MainParent.MsgType.Incoming);
                tb_Query.Focus();
                ClearData();
            }
        }
        string EcCode = "";
        private void dgverror_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (btErrorCode_Add.Enabled == true && btErrorCode_Modify.Enabled == true)
            {
                if (e.RowIndex != -1)
                {
                    txt_ErrorCode.Text = EcCode = dgverror[0, e.RowIndex].Value.ToString();
                    txt_errordesc.Text = dgverror[1, e.RowIndex].Value.ToString();
                    txt_errordesc2.Text = dgverror[2, e.RowIndex].Value.ToString();
                }
            }

        }

        private void tb_ErrorCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                txt_errordesc.Focus();
            }
        }

        private void tb_errordesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_errordesc2.Focus();
            }
        }

        private void tb_errordesc2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lbok.Focus();
            }
        }

        private void tb_Query_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetDataLocation(tb_Query.Text.Trim());
              tb_Query.SelectAll();
            }

         
        }


        private void GetDataLocation(string C_EC)
        {
            for (int i = 0; i < dgverror.RowCount; i++)
            {
                if (dgverror[0, i].Value.ToString() == C_EC)
                {
                    dgverror.Rows[i].Selected = true;
                    dgverror.FirstDisplayedScrollingRowIndex = i;
                }
                else
                    dgverror.Rows[i].Selected = false;
            }
        }

        bool CheckECHave = false;
        private void tb_ErrorCode_MouseLeave(object sender, EventArgs e)
        {
       
        }

        private void tb_ErrorCode_Leave(object sender, EventArgs e)
        {
            if (dgverror.RowCount == 0)
            {
                CheckECHave = true;
                return;
            }
          
            for (int i = 0; i < dgverror.RowCount; i++)
            {
                if (dgverror[0, i].Value.ToString() == txt_ErrorCode.Text.Trim())
                {
                    MessageBox.Show("错误代码: " + txt_ErrorCode.Text.Trim() + " 已经存在,不需要重复添加");
                    txt_ErrorCode.Focus();
                    txt_ErrorCode.SelectAll();
                    CheckECHave = false;
                    break;
                }
                else
                    CheckECHave = true;

            }
        }

      

   
       

      

      

        
       
    }
}
