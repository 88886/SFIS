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
    public partial class PackRecord : Form
    {
        public PackRecord()
        {
            InitializeComponent();
        }
        WebServices.OperateRam.RamPackInfo ram = new WebServices.OperateRam.RamPackInfo();
        string path;
        int fileNum = 0;
        string status;
        private void Btn_Set_Click(object sender, EventArgs e)
        {
            checkUserid();
        }

        public void checkUserid()
        {
            
            try
            {
                ram.userid = Txt_UserId.Text;
                DataTable IsUserID = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.checkUserID(ram.userid));
                if (IsUserID.Rows.Count == 0)
                {
                    //MessageBox.Show();
                    Lbl_Status.Text = "你的账户没有其功能权限；请联系管理员！";
                    return;
                }
                Txt_UserId.ReadOnly = true;
                Txt_Mac.Focus();
            }
            catch (Exception ex)
            {
                Lbl_Status.Text = ex.ToString();
                //throw;
            }
           
            
          
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            Txt_UserId.Text = "";
            Txt_UserId.ReadOnly = false;
        }

        private void Btn_Submit_Click(object sender, EventArgs e)
        {
            submit();

        }

        private void Ckb_IsUse_CheckedChanged(object sender, EventArgs e)
        {
            if (Ckb_IsUse.Checked == true)
            {
                Txt_BoxNum.Enabled = true;
            }
            else
            {
                Txt_BoxNum.Enabled = false;
            }
        }

        private void PackRecord_Load(object sender, EventArgs e)
        {
            Rdb_Mac.Checked = true;
            if (Rdb_Mac.Checked == true)
            {
                label2.Text = "MAC:";
            }
            if (Rdb_SN.Checked == true)
            {
                label2.Text = "SN:";
            }
            Txt_BoxNum.Enabled = false;
           // Txt_Mac.Enabled = false;
            Txt_NewSn.Enabled = false;
            ShowPackInfo();
        }

        //private void checkBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (checkBox1.Checked == true)
        //    {
        //        Txt_Mac.Enabled = true;
        //    }
        //    else
        //    {
        //        Txt_Mac.Enabled = false;
        //    }
        //}

        //private void Txt_OldSn_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
                
        //        if (checkBox2.Checked == false && Ckb_IsUse.Checked == false)
        //        {
        //            submit();
        //            return;
        //        }
        //        //if (checkBox1.Checked == true)
        //        //{
        //        //    DataTable MAC = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.GetMac_Oldsn(Txt_OldSn.Text));
        //        //    if (MAC.Rows.Count != 0)
        //        //    {
        //        //        Txt_Mac.Text = MAC.Rows[0][3].ToString();
        //        //        if (checkBox2.Checked == false && Ckb_IsUse.Checked == false)
        //        //        {
        //        //            submit();
        //        //            return;
        //        //        }
        //        //    }
        //        //    else
        //        //    {
        //        //        Txt_Mac.Focus();
        //        //        return;
        //        //    }
        //        //}
        //        if (checkBox2.Checked == true)
        //        {
        //            Txt_NewSn.Focus();
        //            return;
        //        }
        //        if (Ckb_IsUse.Checked==true)
        //        {
        //            Txt_BoxNum.Focus();
        //            return;
        //        }

        //    }
        //}

        private void Txt_Mac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (checkBox2.Checked == false && Ckb_IsUse.Checked == false)
                {
                    submit();
                    return;
                }
                Txt_NewSn.Focus();
            }
        }

        private void Txt_NewSn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Ckb_IsUse.Checked == true)
                {
                    Txt_BoxNum.Focus();
                }
                else
                {
                    submit();
                }
            }
        }

        public void submit()
        {
           
            try
            {
                ram.recdate = DateTime.Now;
                ram.newsn = Txt_NewSn.Text;
                ram.mac = Txt_Mac.Text;
                ram.boxNum = Txt_BoxNum.Text;
                status = refWebRam.Instance.Inser_Pack(ram);
                Lbl_Status.Text = status;
                Txt_NewSn.Text = "";
                Txt_Mac.Text = "";
                Txt_BoxNum.Text = "";
                Txt_Mac.Focus();
                ShowPackInfo();
                Print_report(ram);
            }
            catch (Exception ex)
            {
                
                Lbl_Status.Text=ex.ToString();
                //throw;
            }
           
            
           
        }

        public void ShowPackInfo()
        {
            try
            {
                DataTable Dt_PackInfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.Get_PackInfo());
                dataGridView1.DataSource = Dt_PackInfo;
                dataGridView1.Columns.Clear();
                DisplayCol(dataGridView1, "mac", "Mac/SN", 100);
                DisplayCol(dataGridView1, "newsn", "新SN", 100);
                DisplayCol(dataGridView1, "boxnum", "箱号", 100);
                DisplayCol(dataGridView1, "userid", "工号", 100);
                DisplayCol(dataGridView1, "recdate", "记录日期", 100);
            }
            catch (Exception ex)
            {
                Lbl_Status.Text = ex.ToString();
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


        private void Txt_BoxNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void Txt_UserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkUserid();
               
               
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                Txt_NewSn.Enabled = true;
            }
            else
            {
                Txt_NewSn.Enabled = false;
            }
        }

        public void Print_report(WebServices.OperateRam.RamPackInfo Ram)
        {
            //做打印操作
            try
            {
                if (fileNum == 0 || string.IsNullOrEmpty(path))
                {
                    Lbl_Status.Text = "请选择路径和填写打印数量！";
                    return;
                }
                Dictionary<string, string> Dic = new Dictionary<string, string>();
                Dic.Add("MAC", Ram.mac);
                Dic.Add("SN", ram.newsn);
                Dic.Add("BOXNUM", ram.boxNum);
                CodeSoftPrint.CodeSoftPrint code = new CodeSoftPrint.CodeSoftPrint();
                code.PrintDoc(path, Dic, fileNum);
            }
            catch(Exception ex)
            {
                Lbl_Status.Text = ex.ToString();
            }
        }

        private void Rdb_Mac_CheckedChanged(object sender, EventArgs e)
        {
            if (Rdb_Mac.Checked == true)
            {
                label2.Text = "MAC:";
            }
            if (Rdb_SN.Checked == true)
            {
                label2.Text = "SN:";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedlg = new OpenFileDialog();
            filedlg.Title = "选择模板文件";
            //文本文件(*.txt)|*.txt|所有文件(*.*)|*.*
            filedlg.Filter = "所有文件(*.*)|*.*|Lab文件(*.lab)|*.lab";
            if (filedlg.ShowDialog() == DialogResult.OK)
            {
                Txt_Filepath.Text = filedlg.FileName;
            }
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            if (Btn_OK.Text == "确定")
            {
                try
                {
                    Txt_Filepath.ReadOnly = true;
                    Txt_Num.ReadOnly = true;
                    path = Txt_Filepath.Text;
                    fileNum = int.Parse(Txt_Num.Text);
                    Btn_OK.Text = "重置";
                    return;
                }
                catch (Exception ex)
                {
                    Lbl_Status.Text = ex.ToString();
                    Txt_Filepath.ReadOnly = false;
                    Txt_Num.ReadOnly = false;
                
                }

            }
            if (Btn_OK.Text == "重置")
            {
                path = ""; 
                Txt_Filepath.Text = "";
                fileNum = 0;
                Txt_Num.Text="0";
                Btn_OK.Text = "确定";
                Txt_Filepath.ReadOnly = false;
                Txt_Num.ReadOnly = false;
                return;
            
            }
        }
    }
}
