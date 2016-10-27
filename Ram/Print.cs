using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RefWebService_BLL;

namespace Ram
{
    public partial class Print : Form
    {
        public Print()
        {
            InitializeComponent();
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            submit();

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

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            //DataToExcel()
            FrmBLL.DataGridViewToExcel.DataToExcel(dataGridView1);

        }

        private void Txt_WorkID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        public void submit()
        {
            DataTable DT_info;
            try
            {
                DT_info = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.Get_WorkIDInfo(Txt_WorkID.Text));
                dataGridView1.DataSource = DT_info;
                dataGridView1.Columns.Clear();
                DisplayCol(dataGridView1, "sn", "OldSn", 100);
                DisplayCol(dataGridView1, "newsn", "NewSn", 100);
                DisplayCol(dataGridView1, "mac", "Mac", 100);
                DisplayCol(dataGridView1, "BadCause", "测试状况", 100);
                DisplayCol(dataGridView1, "BadLoca", "不良位置", 100);
                DisplayCol(dataGridView1, "BadDevice", "不良原因", 100);
                DisplayCol(dataGridView1, "userid", "维修人员", 100);
                DisplayCol(dataGridView1, "recdate", "维修日期", 100);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                Rct_Status.Text = ex.ToString();
               // throw;
            }
           // DataTable DT_info = FrmBLL.ReleaseData.arrByteToDataTable(refWebRam.Instance.Get_WorkIDInfo(Txt_WorkID.Text));
           
            // ser.BadLoca,ser.BadDevice,Ram.recdate
          
        
        }

    }
}
