using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.Xml;

namespace SFIS_V2
{
    public partial class FrmQryKpMaster : Office2007Form //Form
    {
        public FrmQryKpMaster(MainParent Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }

        MainParent mFrm;
        Timer AutoRefresh = new Timer();
        Timer QryTime = new Timer();

        private void FrmQryKpMaster_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
            {
                IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PROGID", this.Name);
                dic.Add("PROGNAME", this.Text);
                dic.Add("PROGDESC", this.Text);
                FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
            }
            #endregion

            #region  设置自动刷新时间 以毫秒计算
            int AutoTime = 0;
            try
           {
               XmlDocument doc = new XmlDocument();
               string XmlName = "DllConfig.xml";
               doc.Load(XmlName);
               string Rtime = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("REFRESHTIME").SelectSingleNode("TIME")).GetAttribute("Name").ToString();
               AutoTime = Convert.ToInt32(Rtime);
               if (AutoTime < 300000)
               {
                   AutoTime = 1800000;
               }
           }
           catch
           {
               AutoTime = 1800000;
           }
            #endregion

            AutoRefresh.Interval = AutoTime;
            AutoRefresh.Enabled = true;
            AutoRefresh.Tick +=new EventHandler(AutoRefresh_Tick);

            QryTime.Interval = 300000;
            QryTime.Enabled = true;
            QryTime.Tick+=new EventHandler(QryTime_Tick);

            #region 单元格交替颜色
            this.dgvkpmaster.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvkpmaster.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            #endregion
            QryKpMasterToDgv();
        }
        private void AutoRefresh_Tick(object sender, EventArgs e)
        {
            QryKpMasterToDgv();
        }

       // int i = 300;
        private void QryTime_Tick(object sender, EventArgs e)
        {         
             imbt_Query.Enabled = true;
             QryTime.Enabled = false;
          
        }

        private void QryKpMasterToDgv()
        {
          DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.QueryKpMasterAudit(Convert.ToInt32(numDays.Value)));
          if (dt.Rows.Count > 0)
          {
              dt.DefaultView.Sort = "recdate desc ";
              dgvkpmaster.DataSource = dt.DefaultView.ToTable();
              statuslabel.Text = "刷新时间:  " + DateTime.Now.ToString();
          }
          else
          {
              MessageBox.Show("没有数据","未查询到数据",MessageBoxButtons.OK,MessageBoxIcon.Information);
          }
        }

        private void imbt_Query_Click(object sender, EventArgs e)
        {
            QryKpMasterToDgv();
            imbt_Query.Enabled = false;
            QryTime.Enabled = true;
         
        }

        private void dgvkpmaster_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvkpmaster.RowHeadersDefaultCellStyle.ForeColor))
            {
                int linen = 0;
                linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }
        }



      
        
    }
}
