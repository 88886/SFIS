using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class FrmPrivliege : Office2007Form //Form
    {
        public FrmPrivliege(MainParent sInfo)
        {
            InitializeComponent();
            sMain = sInfo;
        }
        MainParent sMain;
        private void FrmPrivliege_Load(object sender, EventArgs e)
        {
            try
            {
                #region 添加应用程序
                if (this.sMain.gUserInfo.rolecaption == "系统开发员")
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

            }
            catch (Exception ex)
            {
                this.sMain.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
            Initialization = new InitializationPrg(InitializationInfo);
            Initialization.BeginInvoke(null, null);

            panelEx2.Width = panelEx4.Width / 2 - 100;
            panelEx5.Width = panelEx4.Width / 2 - 40;
            panelEx3.Dock = DockStyle.Fill;


            //btntoleft.Location = new Point(panelEx3.Width - (panelEx3.Width - 10), panelEx3.Height - 100);
            //btntoright.Location = new Point(panelEx3.Width - (panelEx3.Width - 10), panelEx3.Height - 500);


            btntoleft.Location = new Point(30, 158);
            btntoright.Location = new Point(30, 58);

            btntoleft.Width = panelEx3.Width / 2 - 40;
            btntoright.Width = panelEx3.Width / 2 - 40;

            btntoleft.Size = new Size(74, 48);
            btntoright.Size = new Size(74, 48);

            btn_modify.Location = new Point(panelEx4.Width-(panelEx4.Width) / 4, 22);

        }

        private delegate void InitializationPrg();
        InitializationPrg Initialization;

        private void InitializationInfo()
        {
            DownLoadUser();
            DownLoadModule();
        }
        private void DownLoadUser()
        {
            cb_employee.BeginInvoke(new EventHandler(delegate
            {
                cb_employee.Items.Clear();
            }));
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.Sort = dt.Columns[0].ToString();
                DataTable dts = dv.ToTable();


                cb_employee.BeginInvoke(new EventHandler(delegate
                {
                    foreach (DataRow dr in dts.Rows)
                    {
                        cb_employee.Items.Add(dr[0].ToString() + "-" + dr[1].ToString());
                    }
                }));


                cb_employee.BeginInvoke(new EventHandler(delegate
                {
                    cb_employee.SelectedIndex = 0;
                }));

            }

        }
        DataTable dt = null;
        private void DownLoadModule()
        {
            dt = null; //FrmBLL.ReleaseData.arrByteToDataTable(refwebtPrivliege.Instance.DownLoadModule());

            DataTable dts = dt.DefaultView.ToTable(true, "PrgName");

            cb_prgname.BeginInvoke(new EventHandler(delegate
            {
                cb_prgname.Items.Clear();
                cb_prgname.Items.Add("ALL");
            }));

            foreach (DataRow dr in dts.Rows)
            {
                cb_prgname.BeginInvoke(new EventHandler(delegate
                    {
                        cb_prgname.Items.Add(dr[0].ToString());
                    }));
            }

            DataView dv = new DataView(dt);
            dv.Sort = dt.Columns[0].ToString();
            DataTable sDt = dv.ToTable();
            dgvprgList.BeginInvoke(new EventHandler(delegate
                {
                   //dgvprgList.DataSource = sDt;

                    foreach (DataRow dr in sDt.Rows)
                    {
                        dgvprgList.Rows.Add(dr[0].ToString(),dr[1].ToString());
                    }
                    dgvprgList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }));
        }

        private void cb_prgname_TextChanged(object sender, EventArgs e)
        {
            DataTable dtprg = null;
            dgvprgList.Rows.Clear();
            if (cb_prgname.Text == "ALL")
            {
                dtprg = dt;
              
            }
            else
            {

                 dtprg = FrmBLL.publicfuntion.getNewTable(dt, string.Format("PrgName='{0}'", cb_prgname.Text));
            }
       
           
            foreach (DataRow dr in dtprg.Rows)
            {
                bool flag = true;
                for (int x = 0; x < dgvSelectPrgList.Rows.Count; x++)
                {
                    if (dr[0].ToString() + dr[1].ToString() == dgvSelectPrgList.Rows[x].Cells[0].Value.ToString() + dgvSelectPrgList.Rows[x].Cells[1].Value.ToString())
                    {
                        flag = false;
                    }                                       
                }
                if (flag)
                    dgvprgList.Rows.Add(dr[0].ToString(), dr[1].ToString());
            }
        }


        private void btntoright_Click(object sender, EventArgs e)
        {
            try
            {
                int idx = dgvprgList.CurrentRow.Index;
                dgvSelectPrgList.Rows.Add(dgvprgList.Rows[idx].Cells[0].Value.ToString(), dgvprgList.Rows[idx].Cells[1].Value.ToString(), "0");
                dgvprgList.Rows.RemoveAt(idx);
            }
            catch
            {

            }
        }

        private void btntoleft_Click(object sender, EventArgs e)
        {
            try
            {
                int idx = dgvSelectPrgList.CurrentRow.Index;
                dgvprgList.Rows.Add(dgvSelectPrgList.Rows[idx].Cells[0].Value.ToString(), dgvSelectPrgList.Rows[idx].Cells[1].Value.ToString());
                dgvSelectPrgList.Rows.RemoveAt(idx);
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
        }
    }
}
