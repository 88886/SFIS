using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Frm_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        BLL.FileHelper dd = new BLL.FileHelper();
        private void button1_Click(object sender, EventArgs e)
        {
            //    dd.Insert_DB_Log("testset");

            MsgNotice.MsgNotice SS = new MsgNotice.MsgNotice();
            SS.ShowMsg("SDFSDFSDFSDFSDFSDF");

            return;
            //    dd.Insert_Exception_Log("testsetdddddddddd");
            DataTable dt = new DataTable();
            dt.Columns.Add("CO1", typeof(string));
            dt.Columns.Add("CO2", typeof(string));
            dt.Columns.Add("CO3", typeof(string));
            dt.Columns.Add("CO4", typeof(string));
            dt.Rows.Add("AAA", "VVVV", "BBBB", "RRRR");
            dt.Rows.Add("AAA1", "VVVV1", "BBBB1", "RRRR1");
            dt.Rows.Add("AAA2", "VVVV2", "BBBB2", "RRRR2");
            IList<IDictionary<string, object>> d = DataTableToDictionary(dt);
        }


        private IList<IDictionary<string, object>> DataTableToDictionary(DataTable dt)
        {
            IList<IDictionary<string, object>> LsDic = new List<IDictionary<string, object>>();
            foreach (DataRow  dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn  dc in dt.Columns)
                {
                    dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                LsDic.Add(dic);
            }
            return LsDic;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // main.Caption:= 'Move_Date MySQL ' + ' (Build Date : ' + FormatDateTime('yyyy/mm/dd', FileDateToDateTime(FileAge(Application.ExeName))) + ')';
            this.Text = System.IO.File.GetLastWriteTime(Application.ExecutablePath).ToShortDateString();//  CreationTime();//"Move_Date MySQL " + " (Build Date : " + FormatDateTime('yyyy/mm/dd', FileDateToDateTime(FileAge(Application.ExeName))) + ")"
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dd = DateTime.Now;

            MessageBox.Show(dd.ToString());
        }

       
    }
}
