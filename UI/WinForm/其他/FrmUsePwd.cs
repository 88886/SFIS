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
    public partial class FrmUsePwd : Office2007Form// Form
    {
        public FrmUsePwd(string _woId,WorkOrderCreate _woc)
        {
            InitializeComponent();
            this.mwoId = _woId;
            this.mwoc = _woc;
        }
        private string mwoId = "";
        private WorkOrderCreate mwoc;
        private void btSave_Click(object sender, EventArgs e)
        {
            this.label1.Text = "正在保存数据....";
            this.timer1.Enabled = true;
            if (this.dataGridView1.Rows.Count < 1)
            {
                MessageBoxEx.Show("没有导入任何数据!!", "提示");
                return;
            }

            bool macflag = false;
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                if (!string.IsNullOrEmpty(this.dataGridView1.Columns[i].Name))
                {
                    if (this.dataGridView1.Columns[i].Name.ToUpper() == "MAC")
                    {
                        macflag = true;
                        break;
                    }
                }
            }
            if (!macflag)
            {
                MessageBoxEx.Show("没有MAC号字段,请重新设置..", "提示");
                return;
            }
           // List<WebServices.tWoInfo.tUsePwd> _lstUserPwd = new List<WebServices.tWoInfo.tUsePwd>();
            //for (int x = 0; x < this.dataGridView1.Rows.Count; x++)
            //{
            //    for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            //    {
            //        if (this.dataGridView1.Columns[i].Name.ToUpper() != "MAC")
            //        {
            //            _lstUserPwd.Add(new WebServices.tWoInfo.tUsePwd()
            //            {
            //                mac = this.dataGridView1.Rows[x].Cells["mac"].Value.ToString(),
            //                pwdtype = this.dataGridView1.Columns[i].Name,
            //                pwdval = this.dataGridView1.Rows[x].Cells[i].Value.ToString(),
            //                woId = this.mwoId
            //            });
            //        }
            //    }
            //}
            //try
            //{
            //    this.mwoc.lstUserPwd = _lstUserPwd;
            //}
            //catch (Exception ex)
            //{
            //    _lstUserPwd.Clear();
            //    MessageBoxEx.Show(ex.Message, "提示");
            //}
            this.timer1.Enabled = false;
            this.label1.Text = "数据保存完成..";
            this.DialogResult = DialogResult.OK;
        }

        private void openfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择密码数据文件";
            ofd.Filter = "(*.xls Excel 2003)|*.xls";
            DialogResult dlr = ofd.ShowDialog();
            if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
            {
                List<string> sheetNames = FrmBLL.ClsReadExcel.GetTableNames(ofd.FileName);
                if (sheetNames == null || sheetNames.Count < 1)
                {
                    MessageBoxEx.Show("打开的文件没有数据表,请检查..");
                    return;
                }
                DataTable mdt = FrmBLL.ClsReadExcel.getTable(ofd.FileName, sheetNames[0]);
                this.dataGridView1.DataSource = mdt;
            }
        }

        private void FrmUsePwd_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Value = this.progressBar1.Value + 1;
            if ((this.progressBar1.Value + 1) >= this.progressBar1.Maximum)
                this.progressBar1.Value = 0;
        }
    }
}
