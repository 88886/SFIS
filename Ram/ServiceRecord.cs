using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RefWebService_BLL;
using Entity;

namespace Ram
{
    public partial class ServiceRecord : Form
    {
        public ServiceRecord()
        {
            InitializeComponent();
        }
        WebServices.OperateRam.RamServiceInfo ram = new WebServices.OperateRam.RamServiceInfo();
        
        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            ram.Userid = Txt_UserId.Text;
            
            try
            {
                DataTable IsUserID = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.checkUserID(ram.Userid));
                if (IsUserID.Rows.Count == 0)
                {
                    // MessageBox.Show("");
                    lbl_status.Text = "你的账户没有其功能权限；请联系管理员！";
                    return;
                }
            }
            catch (Exception ex)
            {
                lbl_status.Text = ex.ToString();
                //throw;
            }

            Txt_UserId.ReadOnly = true;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Txt_UserId.ReadOnly = false;
            Txt_UserId.Text = "";
        }

        private void Btn_SubMit_Click(object sender, EventArgs e)
        {
            subMit();
        }

        private void ServiceRecord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    DataTable Badcuase = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.Get_BadCase(Txt_Mac.Text));
                    if (Badcuase.Rows.Count == 0)
                    {
                        //  MessageBox.Show("你输入的SN有误或者板子尚未做检测！");
                        lbl_status.Text = "你输入的Mac有误或者板子尚未做检测！";
                        return;
                    }



                    if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false)
                    {
                        subMit();
                        return;
                    }



                    if (checkBox3.Checked == true && checkBox2.Checked == true)
                    {
                        Txt_BadCause.Text = Badcuase.Rows[0][0].ToString();
                        Txt_BadDevice.Focus();
                        return;
                    }

                    if (checkBox1.Checked == true)
                    {
                        Txt_BadCause.Text = Badcuase.Rows[0][0].ToString();
                        subMit();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lbl_status.Text = ex.ToString();
                    // throw;
                }

            }
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Enabled == true)
            {
                Txt_BadDevice.Enabled = true;
            }
            else
            {
                Txt_BadDevice.Enabled = false;
            }
        }

        private void ServiceRecord_Load(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            checkBox3.Checked = true;
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
           // Txt_BadDevice.Enabled = false;
           // Txt_BadLoca.Enabled = false;
            ShowInfo();
        }
        public void subMit()
        {
            ram.Mac = Txt_Mac.Text;
            ram.recDate = DateTime.Now;
            ram.badDevice = Txt_BadDevice.Text;
            ram.badCase = Txt_BadCause.Text;
            ram.BadLoca = Txt_BadLoca.Text;
            try
            {
                string status = refWebRam.Instance.Inser_service(ram);
                // MessageBox.Show(status);
                lbl_status.Text = status;
            }
            catch (Exception ex)
            {
                lbl_status.Text = ex.ToString();
                //throw;
            }

            Txt_Mac.Text = "";
            Txt_BadDevice.Text = "";
            Txt_BadCause.Text = "";
            Txt_BadLoca.Text = "";
            Txt_Mac.Focus();
            ShowInfo();
        }
        public void ShowInfo()
        {
            try
            {
                DataTable serviceInfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.Get_ServiceInfo());
                dataGridView1.DataSource = serviceInfo;
                dataGridView1.Columns.Clear();
                DisplayCol(dataGridView1, "mac", "Mac", 100);
                DisplayCol(dataGridView1, "BadCause", "检测状况", 100);
                DisplayCol(dataGridView1, "BadDevice", "不良原因", 100);
                DisplayCol(dataGridView1, "BadLoca", "不良位置", 100);
                DisplayCol(dataGridView1, "userid", "工号", 100);
                DisplayCol(dataGridView1, "RecDate", "记录日期", 100);
            }
            catch (Exception ex)
            {
                lbl_status.Text = ex.ToString();
               // throw;
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


        private void Txt_BadCause_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (checkBox2.Checked == true)
                {
                    Txt_BadDevice.Focus();
                }
                else
                {
                    subMit();
                }
            }
        }

        private void Txt_BadDevice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (checkBox3.Checked == true)
                {
                    Txt_BadLoca.Focus();
                    return;
                }
                else
                {
                    subMit();
                }

            }
        }

        private void Txt_UserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ram.Userid = Txt_UserId.Text;
                try
                {
                    DataTable IsUserID = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.checkUserID(ram.Userid));
                    if (IsUserID.Rows.Count == 0)
                    {
                        // MessageBox.Show();
                        lbl_status.Text = "你的账户没有其功能权限；请联系管理员！";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lbl_status.Text = ex.ToString();
                    //throw;
                }

                Txt_Mac.Focus();
                Txt_UserId.ReadOnly = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                Txt_BadLoca.Enabled = true;
            }
            else
            {
                Txt_BadLoca.Enabled = false;
            }
        }

        private void Txt_BadLoca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                subMit();
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
