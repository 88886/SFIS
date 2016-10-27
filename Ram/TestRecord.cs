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
    public partial class TestRecord : Form
    {
        public TestRecord()
        {
            InitializeComponent();
        }
        //WebServices.OperateRam
        WebServices.OperateRam.RamTestInfo ram = new WebServices.OperateRam.RamTestInfo();
        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            ram.UserId = Txt_UserID.Text;

            try
            {
                DataTable IsUserID = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.checkUserID(ram.UserId));
                if (IsUserID.Rows.Count == 0)
                {
                    //MessageBox.Show("");
                    Lbl_Status.Text = "你的账户没有其功能权限；请联系管理员！";
                    return;
                }
                Txt_UserID.ReadOnly = true;
            }
            catch (Exception ex)
            {
                Lbl_Status.Text = ex.ToString();
               //throw;
            }

            
        }

        private void Btn_Submit_Click(object sender, EventArgs e)
        {
            Submit();
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            Txt_UserID.ReadOnly = false;
            Txt_UserID.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Txt_BadCause.Enabled = true;
            }
            else
            {
                Txt_BadCause.Enabled = false;
            }
        }

        private void TestRecord_Load(object sender, EventArgs e)
        {
            Txt_BadCause.Enabled = false;
            Rdb_Mac.Checked = true;
            if (Rdb_Mac.Checked == true)
            {
                label1.Text = "MAC:";
            }
            if (Rdb_SN.Checked == true)
            {
                label1.Text = "SN:";
            }
            ShowInfo();
        }

        private void Txt_Sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (checkBox1.Checked == true)
                {
                    Txt_BadCause.Focus();
                }
                else
                {
                    Submit();

                }
            }
        }

        private void Txt_BadCause_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                Submit();
            }
        }

        public void Submit()
        {
            ram.Mac = Txt_Mac.Text;
            ram.recdate = DateTime.Now;
            ram.BadCause = Txt_BadCause.Text;
            try
            {
                string status = refWebRam.Instance.Backup_Test(ram);
                // MessageBox.Show(status);
                Lbl_Status.Text = status;
            }
            catch (Exception ex)
            {
                Lbl_Status.Text = ex.ToString();
               // throw;
            }
          
            Txt_Mac.Text = "";
            Txt_BadCause.Text = "";
            ShowInfo();
            Txt_Mac.Focus();
        }
        public void ShowInfo()
        {
            try
            {
                DataTable Dt_Testinfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.Get_TestInfo());

                dataGridView1.DataSource = Dt_Testinfo;
                dataGridView1.Columns.Clear();
                DisplayCol(dataGridView1, "mac", "Mac", 100);
                DisplayCol(dataGridView1, "BadCause", "测试状况", 200);
                DisplayCol(dataGridView1, "Recdate", "录入日期", 100);
                DisplayCol(dataGridView1, "userId", "工号", 100);

            }
            catch (Exception ex)
            {
                Lbl_Status.Text = ex.ToString();
              //  throw;
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


        private void Txt_UserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    ram.UserId = Txt_UserID.Text;

                    DataTable IsUserID = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.checkUserID(ram.UserId));
                    if (IsUserID.Rows.Count == 0)
                    {
                        // MessageBox.Show("");
                        Lbl_Status.Text = "你的账户没有其功能权限；请联系管理员！";
                        return;
                    }
                    Txt_Mac.Focus();
                    Txt_UserID.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    Lbl_Status.Text = ex.ToString();
                }
            }
        }

        private void Rdb_Mac_CheckedChanged(object sender, EventArgs e)
        {
            if (Rdb_Mac.Checked == true)
            {
                label1.Text = "MAC:";
            }
            if (Rdb_SN.Checked == true)
            {
                label1.Text = "SN:";
            }
        }
    }
}
