using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 测试用例
{
    public partial class MainForm : Form
    {
        // 定义下拉列表框
        private ComboBox cmb_Temp = new ComboBox();

        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 绑定性别下拉列表框
        /// </summary>
        private void BindSex()
        {
            DataTable dtSex = new DataTable();
            dtSex.Columns.Add("Value");
            dtSex.Columns.Add("Name");
            DataRow drSex;
            drSex = dtSex.NewRow();
            drSex[0] = "1";
            drSex[1] = "男";
            dtSex.Rows.Add(drSex);
            drSex = dtSex.NewRow();
            drSex[0] = "0";
            drSex[1] = "女";
            dtSex.Rows.Add(drSex);
            cmb_Temp.ValueMember = "Value";
            cmb_Temp.DisplayMember = "Name";
            cmb_Temp.DataSource = dtSex;
            cmb_Temp.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// 为避免连接数据库，这里手工构造数据表，实际应用中应从数据库中获取
        /// </summary>
        private void BindData()
        {
            DataTable dtData = new DataTable();
            dtData.Columns.Add("ID");
            dtData.Columns.Add("Name");
            dtData.Columns.Add("Sex");
            DataRow drData;
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "张三";
            drData[2] = "1";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 2;
            drData[1] = "李四";
            drData[2] = "1";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 3;
            drData[1] = "王五";
            drData[2] = "1";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 4;
            drData[1] = "小芳";
            drData[2] = "0";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 5;
            drData[1] = "小娟";
            drData[2] = "0";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 6;
            drData[1] = "赵六";
            drData[2] = "1";
            dtData.Rows.Add(drData);
            this.dgv_User.DataSource = dtData;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 绑定性别下拉列表框
            BindSex();

            //绑定数据表
            BindData();

            // 设置下拉列表框不可见
            cmb_Temp.Visible = false;

            // 添加下拉列表框事件
            cmb_Temp.SelectedIndexChanged += new EventHandler(cmb_Temp_SelectedIndexChanged);

            // 将下拉列表框加入到DataGridView控件中
            this.dgv_User.Controls.Add(cmb_Temp);
        }

        // 当用户移动到性别这一列时单元格显示下拉列表框
        private void dgv_User_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dgv_User.CurrentCell.ColumnIndex == 2)
                {
                    Rectangle rect = dgv_User.GetCellDisplayRectangle(dgv_User.CurrentCell.ColumnIndex, dgv_User.CurrentCell.RowIndex, false);
                    string sexValue = dgv_User.CurrentCell.Value.ToString();
                    if (sexValue == "1")
                    {
                        cmb_Temp.Text = "男";
                    }
                    else
                    {
                        cmb_Temp.Text = "女";
                    }
                    cmb_Temp.Left = rect.Left;
                    cmb_Temp.Top = rect.Top;
                    cmb_Temp.Width = rect.Width;
                    cmb_Temp.Height = rect.Height;
                    cmb_Temp.Visible = true;
                }
                else
                {
                    cmb_Temp.Visible = false;
                }
            }
            catch
            {
            }
        }

        // 当用户选择下拉列表框时改变DataGridView单元格的内容
        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "男")
            {
                dgv_User.CurrentCell.Value = "男";
                dgv_User.CurrentCell.Tag = "1";
            }
            else
            {
                dgv_User.CurrentCell.Value = "女";
                dgv_User.CurrentCell.Tag = "0";
            }
        }

        // 滚动DataGridView时将下拉列表框设为不可见
        private void dgv_User_Scroll(object sender, ScrollEventArgs e)
        {
            this.cmb_Temp.Visible = false;
        }

        // 改变DataGridView列宽时将下拉列表框设为不可见
        private void dgv_User_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.cmb_Temp.Visible = false;
        }

        // 绑定数据表后将性别列中的每一单元格的Value和Tag属性（Tag为值文本，Value为显示文本）
        private void dgv_User_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < this.dgv_User.Rows.Count; i++)
            {
                if (dgv_User.Rows[i].Cells[2].Value != null && dgv_User.Rows[i].Cells[2].ColumnIndex == 2)
                {
                    dgv_User.Rows[i].Cells[2].Tag = dgv_User.Rows[i].Cells[2].Value.ToString();
                    if (dgv_User.Rows[i].Cells[2].Value.ToString() == "1")
                    {
                        dgv_User.Rows[i].Cells[2].Value = "男";
                    }
                    else if (dgv_User.Rows[i].Cells[2].Value.ToString() == "0")
                    {
                        dgv_User.Rows[i].Cells[2].Value = "女";
                    }
                }
            }
        }

        private void dgv_User_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
