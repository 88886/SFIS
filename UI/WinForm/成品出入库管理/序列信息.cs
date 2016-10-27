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
    public partial class SerialInfo :Office2007Form// Form
    {
        public SerialInfo(Office2007Form _sr,DataTable dt,string partnumber,string cartonId,string palletnumber,string lotid)
        {
            InitializeComponent();
            mFsr = _sr;
            dt.Columns.Add("partnumber");
            dt.Columns.Add("CartonId");
            dt.Columns.Add("PalletNumber");
            dt.Columns.Add("lotId");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["partnumber"] = partnumber;
                dt.Rows[i]["CartonId"] = cartonId;
                dt.Rows[i]["PalletNumber"] = palletnumber;
                dt.Rows[i]["lotId"] = lotid;
            }
            mdt = dt;
        }
        Office2007Form mFsr;
        private DataTable mdt;

        private void SerialInfo_Load(object sender, EventArgs e)
        {
            dgv_serialinfo.DataSource = mdt;
        }

        private void bt_excel_Click(object sender, EventArgs e)
        {
            if (dgv_serialinfo.Rows.Count < 1)
                MessageBox.Show("没有可导出的数据!!");
            FrmBLL.DataGridViewToExcel.DataToExcel(dgv_serialinfo);
        }

        private void dgv_serialinfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
                 dgv_serialinfo.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                       dgv_serialinfo.RowHeadersDefaultCellStyle.Font, rectangle,
                       dgv_serialinfo.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

        }

        
    }
}
