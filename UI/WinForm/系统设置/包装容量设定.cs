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
    public partial class FrmPackParameters : Office2007Form // Form
    {
        public FrmPackParameters(MainParent sInfo)
        {
            InitializeComponent();
            sMain = sInfo;
        }

        MainParent sMain;
        private void FrmPackParameters_Load(object sender, EventArgs e)
        {
            GetPackParameters();
            PalData.Visible = false;           
        }

        private void GetPackParameters()
        {
            dgvPackParam.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPackParameters.Instance.GetPackParameters());
   
        }

        private void btAddPackParam_Click(object sender, EventArgs e)
        {
            txt_PartNumber.Text = "";
            txt_VersionCode.Text = "";
            num_cartonqty.Value = 1;
            num_trayqty.Value = 1;
            num_palletqty.Value = 1;
            PalData.Visible = true;
            btDeletePackParam.Enabled = false;
            btModifyPackParam.Enabled = false;
            txt_PartNumber.Focus();
            sFlag = CheckInsertOrUpdate.新增;
            txt_PartNumber.DropDownStyle =ComboBoxStyle.DropDownList;

            //string[] PartNumber =  refWebtWoInfo.Instance.GetPartNumberList();
            //foreach (string item in PartNumber)
            //{
            //    txt_PartNumber.Items.Add(item);
            //}
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetProduct());
            foreach (DataRow dr in dt.Rows)
            {
                txt_PartNumber.Items.Add(dr["PARTNUMBER"].ToString());
            }
        }

        private void lbok_MouseEnter(object sender, EventArgs e)
        {
            lbok.BackColor = Color.FromArgb(128, 255, 128);

        }

        private void lbok_MouseLeave(object sender, EventArgs e)
        {
            lbok.BackColor = Color.FromArgb(192, 192, 0);
        }

        private void lbok_MouseDown(object sender, MouseEventArgs e)
        {
            lbok.BackColor = Color.FromArgb(255, 128, 128);
        }

       private enum CheckInsertOrUpdate
        {
            新增,
            修改
        }
        CheckInsertOrUpdate sFlag;

       string deletedata="";
        private void lbok_Click(object sender, EventArgs e)
        {
            
            switch (sFlag)
            {
                case CheckInsertOrUpdate.新增:

                    if ((string.IsNullOrEmpty(txt_PartNumber.Text)) || (string.IsNullOrEmpty(txt_VersionCode.Text)))
                    {
                        sMain.ShowPrgMsg("产品料号或版本为空", MainParent.MsgType.Error);
                        return;
                    }


                    for (int i = 0; i < dgvPackParam.RowCount; i++)
                    {
                        if ((dgvPackParam[1, i].Value.ToString() == txt_PartNumber.Text.Trim()) && (dgvPackParam[2, i].Value.ToString() == txt_VersionCode.Text.Trim()))
                        {
                            MessageBox.Show("     产品料号: " + txt_PartNumber.Text.Trim() + "\r\n     产品版本: " + txt_VersionCode.Text.Trim() + "\r\n     已经存在,不需要重复添加");
                            txt_VersionCode.Focus();
                            txt_VersionCode.SelectAll();
                            return;
                        }
                    }
                    Dictionary<string,object> dic = new Dictionary<string,object>();
                    FrmBLL.publicfuntion.SerializeControl(dic,PalData);
                    dic.Add("TRAYQTY", Convert.ToInt32(num_trayqty.Value.ToString()));
                    dic.Add("CARTONQTY", Convert.ToInt32(num_cartonqty.Value.ToString()));
                    dic.Add("PALLETQTY", Convert.ToInt32(num_palletqty.Value.ToString()));
                    refWebtPackParameters.Instance.InsertPackParameters(FrmBLL.ReleaseData.DictionaryToJson(dic));

                    //refWebtPackParameters.Instance.InsertPackParameters(new WebServices.tPackParameters.tPackParametersTable()
                    //{
                    //    PartNumber = txt_PartNumber.Text.Trim(),
                    //    VersionCode = txt_VersionCode.Text.Trim(),
                    //    TrayQty = Convert.ToInt32(num_trayqty.Value.ToString()),
                    //    CartonQty = Convert.ToInt32(num_cartonqty.Value.ToString()),
                    //    PalletQty = Convert.ToInt32(num_palletqty.Value.ToString())
                    //});

                  
                   FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "PackParam", "Add", "PartNumber: " + txt_PartNumber.Text.Trim() + " Ver: " + txt_VersionCode.Text.Trim());

                    GetPackParameters();
                    GetDataLocationFocus(tb_QryPartNo.Text.Trim());
                    txt_PartNumber.Text = "";
                    txt_VersionCode.Text = "";
                    num_palletqty.Value = 1;
                    num_trayqty.Value = 1;
                    num_cartonqty.Value = 1;
                    txt_PartNumber.Focus();
                    sMain.ShowPrgMsg("新增包装参数完成",MainParent.MsgType.Incoming);

                    break;

                case CheckInsertOrUpdate.修改:
                    //refWebtPackParameters.Instance.UpdatePackParameters(new WebServices.tPackParameters.tPackParametersTable()
                    //{
                    //    PartNumber = txt_PartNumber.Text.Trim(),
                    //    TrayQty = Convert.ToInt32(num_trayqty.Value.ToString()),
                    //    CartonQty = Convert.ToInt32(num_cartonqty.Value.ToString()),
                    //    PalletQty = Convert.ToInt32(num_palletqty.Value.ToString())
                    //});     
                    Dictionary<string,object> dicModify = new Dictionary<string,object>();
                    FrmBLL.publicfuntion.SerializeControl(dicModify, PalData);
                    dicModify.Add("TRAYQTY", Convert.ToInt32(num_trayqty.Value.ToString()));
                    dicModify.Add("CARTONQTY", Convert.ToInt32(num_cartonqty.Value.ToString()));
                    dicModify.Add("PALLETQTY", Convert.ToInt32(num_palletqty.Value.ToString()));
                    refWebtPackParameters.Instance.UpdatePackParameters(FrmBLL.ReleaseData.DictionaryToJson(dicModify));

                   string ssRes = FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "PackParam", "Modify", "PartNumber: " + txt_PartNumber.Text.Trim() + " Ver: " + txt_VersionCode.Text.Trim());
                    GetPackParameters();
                    GetDataLocationFocus(tb_QryPartNo.Text.Trim());
                    lbcancel_Click(null,null);
                    sMain.ShowPrgMsg("修改包装参数完成" + ssRes, MainParent.MsgType.Incoming);
                    break;

                default:
                    break;


            }

        }

        private void btModifyPackParam_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(deletedata))
            {
                MessageBox.Show("请先选择需要修改的资料", "修改提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            btAddPackParam.Enabled = false;
            btDeletePackParam.Enabled = false;
            PalData.Visible = true;
            txt_VersionCode.Enabled = false;
            txt_PartNumber.Enabled = false;
            sFlag = CheckInsertOrUpdate.修改;
        }

        private void lbcancel_Click(object sender, EventArgs e)
        {
            PalData.Visible = false;
            btAddPackParam.Enabled = true;
            btDeletePackParam.Enabled = true;
            btModifyPackParam.Enabled = true;
            txt_PartNumber.Enabled = true;
            txt_VersionCode.Enabled = true;
        }

        private void dgvPackParam_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (btAddPackParam.Enabled == true && btModifyPackParam.Enabled == true)
            {
                if (e.RowIndex != -1)
                {
                    deletedata = dgvPackParam[1, e.RowIndex].Value.ToString();
                    txt_PartNumber.Text = dgvPackParam[1, e.RowIndex].Value.ToString();
                    txt_VersionCode.Text = dgvPackParam[2, e.RowIndex].Value.ToString();
                    num_trayqty.Value = Convert.ToInt32(dgvPackParam[3, e.RowIndex].Value.ToString());
                    num_cartonqty.Value =Convert.ToInt32(dgvPackParam[4, e.RowIndex].Value.ToString());
                    num_palletqty.Value = Convert.ToInt32(dgvPackParam[5, e.RowIndex].Value.ToString());
          
                }
            }
        }

        private void btDeletePackParam_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(deletedata))
            {
                MessageBox.Show("请先选择需要删除的资料", "删除提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("   确定要删除产品料号: " + txt_PartNumber.Text.Trim()+"\r\n     产品版本: "+txt_VersionCode.Text.Trim(), "删除信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                refWebtPackParameters.Instance.DeletePackParameters(txt_PartNumber.Text.Trim());
                
 
                FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "PackParam", "Delete", " PartNumber " + txt_PartNumber.Text.Trim() + " Ver: " + txt_VersionCode.Text.Trim());

                GetPackParameters();
                sMain.ShowPrgMsg("删除完成", MainParent.MsgType.Incoming);

            }
        }

        private void GetDataLocationFocus(string PartNumber)
        {
            for (int i = 0; i < dgvPackParam.RowCount; i++)
            {
                if (dgvPackParam[1, i].Value.ToString() == PartNumber)
                {
                    dgvPackParam.Rows[i].Selected = true;
                    dgvPackParam.FirstDisplayedScrollingRowIndex = i;
                }
                else
                    dgvPackParam.Rows[i].Selected = false;
            }
        }

        private void tb_QryPartNo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tb_QryPartNo.Text)) && (e.KeyCode == Keys.Enter))
            {
                GetDataLocationFocus(tb_QryPartNo.Text.Trim());
                tb_QryPartNo.SelectAll();
            }
        }

        private void lbcancel_MouseDown(object sender, MouseEventArgs e)
        {
            lbcancel.BackColor = Color.FromArgb(255, 128, 128);
        }

        private void lbcancel_MouseEnter(object sender, EventArgs e)
        {
            lbcancel.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void lbcancel_MouseLeave(object sender, EventArgs e)
        {
            lbcancel.BackColor = Color.FromArgb(192, 192, 0);
        }

     
    }
}
