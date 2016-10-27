using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Entity;
using RefWebService_BLL;

namespace Ram
{
    public partial class RamRecord : Form
    {
        public RamRecord()
        {
            InitializeComponent();
        }
        WebServices.OperateRam.RamBaseinfo ram = new WebServices.OperateRam.RamBaseinfo();
        string status;
        private void Btn_setting_Click(object sender, EventArgs e)
        {
            try
            {

                ram.userid = Txt_UserId.Text;
                ram.workid = Txt_WorkId.Text;
                ram.partNum = Txt_PartNum.Text;
                ram.pdName = Txt_PdName.Text;
                DataTable IsUserID = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.checkUserID(ram.userid));
                if (IsUserID.Rows.Count == 0)
                {
                    //  MessageBox.Show();
                    Lab_Status.Text = "你的账户没有其功能权限；请联系管理员！";
                    return;
                }
                if (string.IsNullOrEmpty(ram.workid) || string.IsNullOrEmpty(ram.partNum) || string.IsNullOrEmpty(ram.pdName))
                {
                    //MessageBox.Show("请将基础信息填写完整！");
                    Lab_Status.Text = "请将基础信息填写完整";
                    return;
                }
                Txt_PartNum.ReadOnly = true;
                Txt_PdName.ReadOnly = true;
                Txt_UserId.ReadOnly = true;
                Txt_WorkId.ReadOnly = true;
            }
            catch (Exception ex)
            {
                Lab_Status.Text = ex.ToString();
            }
           
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            Txt_PartNum.ReadOnly = false;
            Txt_PdName.ReadOnly = false;
            Txt_UserId.ReadOnly = false;
            Txt_WorkId.ReadOnly = false;

            Txt_PartNum.Text = "";
            Txt_PdName.Text = "";
            Txt_UserId.Text = "";
            Txt_WorkId.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Submit();

        }

        public void Submit()
        {
           
            try
            {
                ram.Sn = Txt_Sn.Text;
                ram.Mac = Txt_Mac.Text;
                ram.recdate = DateTime.Now;
                if (string.IsNullOrEmpty(ram.Sn) || string.IsNullOrEmpty(ram.Mac))
                {
                    Lab_Status.Text = "SN和mac都不能为空~！";
                    return;
                }
               
                status = refWebRam.Instance.Inser_RamInfo(ram);
                Lab_Status.Text = status;
                Txt_Sn.Text = "";
                Txt_Mac.Text = "";
                Txt_Mac.Focus();
                Show_RamInfo();
            }
            catch (Exception ex)
            {
                Lab_Status.Text = ex.ToString();
                //throw;
            }

           
        }

        public void Show_RamInfo()
        {
           
            try
            {
                DataTable Dt_Raminfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.Get_RamInfo());
                DtG_RamInfo.DataSource = Dt_Raminfo;
                DtG_RamInfo.Columns.Clear();
                DisplayCol(DtG_RamInfo, "SN", "SN", 100);
                DisplayCol(DtG_RamInfo, "MAC", "MAC", 100);
                DisplayCol(DtG_RamInfo, "Recdate", "录入日期", 100);
                DisplayCol(DtG_RamInfo, "UserId", "工号", 100);
            }
            catch (Exception ex)
            {
                Lab_Status.Text = ex.ToString();
                throw;
            }
           

           
            
        }

        private void DisplayCol(DataGridView dgv, String dataPropertyName, String headerText, int width)
        {
            dgv.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn obj = new DataGridViewTextBoxColumn();
            obj.DataPropertyName = dataPropertyName;
            obj.HeaderText = headerText;
            obj.Name = dataPropertyName;
            obj.Width = width;
            obj.Resizable = DataGridViewTriState.True;
            dgv.Columns.AddRange(new DataGridViewColumn[] { obj });
        }


        private void RamRecord_Load(object sender, EventArgs e)
        {
            //Txt_WorkId.Focus();
            Show_RamInfo();
        }

        private void Txt_Sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Submit();
            }
        }

        private void Txt_Mac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                
                Txt_Sn.Focus();
            }
        }

        private void Txt_WorkId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Txt_PartNum.Focus();
            }
        }

        private void Txt_PartNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Txt_PdName.Focus();
            }
        }

        private void Txt_UserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                try
                {
                    ram.userid = Txt_UserId.Text;
                    ram.workid = Txt_WorkId.Text;
                    ram.partNum = Txt_PartNum.Text;
                    ram.pdName = Txt_PdName.Text;
                    DataTable IsUserID = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.checkUserID(ram.userid));
                    if (IsUserID.Rows.Count == 0)
                    {
                        // MessageBox.Show("");
                        Lab_Status.Text = "你的账户没有其功能权限；请联系管理员！";
                        return;
                    }
                    if (string.IsNullOrEmpty(ram.workid) || string.IsNullOrEmpty(ram.partNum) || string.IsNullOrEmpty(ram.pdName))
                    {
                        //MessageBox.Show();
                        Lab_Status.Text = "请将基础信息填写完整！";
                        return;
                    }
                    Txt_PartNum.ReadOnly = true;
                    Txt_PdName.ReadOnly = true;
                    Txt_UserId.ReadOnly = true;
                    Txt_WorkId.ReadOnly = true;
                    Txt_Mac.Focus();

                }
                catch (Exception ex)
                {
                    Lab_Status.Text = ex.ToString();
                    throw;
                }

               
            }

        }

        private void Txt_PdName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Txt_UserId.Focus();
            }
        }
    }
}
