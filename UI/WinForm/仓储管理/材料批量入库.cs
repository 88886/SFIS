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
    public partial class storagebuffer : Office2007Form// Form
    {
        public storagebuffer(MainParent frm, string trsn, int flag)
        {
            InitializeComponent();
            mFrm = frm;
            this.mTrsn = trsn;
            this.mFlag = flag;
        }
        MainParent mFrm;
        string mTrsn;
        int mFlag = 0;
        ComboBox cbo = new ComboBox();
        private enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error } //文字输出类型
        private Color[] LogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };//文字颜色

        private void storagebuffer_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
            {
                List<WebServices.tUserInfo.tFunctionList> lsfunls = new List<WebServices.tUserInfo.tFunctionList>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                FrmBLL.publicfuntion.AddProgInfo(new WebServices.tUserInfo.tProgInfo()
                {
                    progid = this.Name,
                    progname = this.Text,
                    progdesc = this.Text

                }, lsfunls);
            }
            #endregion

            this.cbo_storehouseId.DataSource = this.DataTableToList(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtStorehouseManage.Instance.GetAllWarehouseId()), "storehouseId");
            LoadAllbuffer();
        }

        private void LoadAllbuffer()
        {
            if (mFlag == 1)
            {
                this.cm_selectmodel.SelectedIndex = 5;
                this.tb_selectvalue.Text = mTrsn;
                bt_query_Click(null, null);
            }
            else
                this.dgv_showdata.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.GetStoregabuffer());
            this.cbo.Visible = false;
            if (this.dgv_showdata.Rows.Count > 0)
            {
                cbo.Validated += new EventHandler(cbo_Validated);
                this.dgv_showdata.Controls.Add(cbo);
            }
        }

        private void cbo_Validated(object sender, EventArgs e)
        {
            this.dgv_showdata.CurrentCell.Value = ((ComboBox)sender).Text == "选择.." ? string.Empty : ((ComboBox)sender).Text;
        }

        private void dgv_showdata_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dgv_showdata.CurrentCell == null)
                    return;
                if (this.dgv_showdata.CurrentCell.ColumnIndex == this.dgv_showdata.Columns["storehouseId"].Index)
                {
                    Rectangle rect =
                        dgv_showdata.GetCellDisplayRectangle(dgv_showdata.CurrentCell.ColumnIndex,
                        dgv_showdata.CurrentCell.RowIndex, false);
                    DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtStorehouseManage.Instance.GetAllWarehouseId());
                    cbo.DataSource = this.DataTableToList(dt, "storehouseId");
                    cbo.DisplayMember = "storehouseId";
                    cbo.Left = rect.Left;
                    cbo.Top = rect.Top;
                    cbo.Width = rect.Width;
                    cbo.Height = rect.Height;
                    cbo.Visible = true;
                    cbo.Text = this.dgv_showdata.CurrentCell.Value.ToString();
                    cbo.SelectAll();
                    cbo.Focus();
                }
                else if (this.dgv_showdata.CurrentCell.ColumnIndex == this.dgv_showdata.Columns["locId"].Index)
                {
                    Rectangle rect = dgv_showdata.GetCellDisplayRectangle(
                        dgv_showdata.CurrentCell.ColumnIndex,
                        dgv_showdata.CurrentCell.RowIndex, false);
                    string str = this.dgv_showdata[this.dgv_showdata.Columns["storehouseId"].Index,
                        this.dgv_showdata.CurrentCell.RowIndex].Value.ToString();
                    if (string.IsNullOrEmpty(str))
                    {
                        cbo.DataSource = null;
                        cbo.DisplayMember = "locId";
                    }
                    else
                    {

                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtStorehouseManage.Instance.GetAllLocIdByWarehouseId(str));
                        cbo.DataSource = this.DataTableToList(dt, "locId");
                        cbo.DisplayMember = "locId";
                    }
                    cbo.Left = rect.Left;
                    cbo.Top = rect.Top;
                    cbo.Width = rect.Width;
                    cbo.Height = rect.Height;
                    cbo.Visible = true;
                    cbo.Text = this.dgv_showdata.CurrentCell.Value.ToString();
                    cbo.SelectAll();
                    cbo.Focus();
                }
                else
                {
                    cbo.Visible = false;
                }
                cbo.Items.Add("");
            }
            catch
            {
            }
        }
        private List<string> DataTableToList(DataTable dt, string col)
        {
            List<string> ls = new List<string>();
            ls.Add("");
            foreach (DataRow dr in dt.Rows)
            {
                ls.Add(dr[col].ToString());
            }
            return ls;
        }
        private void dgv_showdata_Scroll(object sender, ScrollEventArgs e)
        {
            this.cbo.Visible = false;
        }

        private void dgv_showdata_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.cbo.Visible = false;
        }
        private void ShowData(DataTable dt)
        {
            this.dgv_showdata.Invoke(new EventHandler(delegate
            {
                this.dgv_showdata.DataSource = dt;
            }));
        }
        private void bt_query_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_selectvalue.Text))
            {
                switch (this.cm_selectmodel.SelectedIndex)
                {
                    case 1:
                        ShowData(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.GetStoregabufferByKpnumber(this.tb_selectvalue.Text.Trim())));
                        break;
                    case 2:
                        ShowData(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.GetStoregabufferByVendercode(this.tb_selectvalue.Text.Trim())));
                        break;
                    case 3:
                        ShowData(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.GetStoregabufferByDatecode(this.tb_selectvalue.Text.Trim())));
                        break;
                    case 4:
                        ShowData(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.GetStoregabufferByUserId(this.tb_selectvalue.Text.Trim())));
                        break;
                    case 5:
                        ShowData(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.GetStoregabufferByTrsn(this.tb_selectvalue.Text.Trim())));
                        break;
                    default:
                        DataTable dt = new DataTable();
                        dt = (this.dgv_showdata.DataSource as DataTable).Clone();
                        ShowData(dt);
                        break;
                }
            }
            else
            {
                ShowData(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtPartStorehousehad.Instance.GetStoregabuffer()));
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (this.dgv_showdata.Rows.Count < 1)
            {
                this.mFrm.ShowPrgMsg("没有可以修改的数据", MainParent.MsgType.Warning);
                return;
            }
            if (MessageBoxEx.Show("请确认数据的正确性后再保存\n\n是否继续?\n\n保存[Yes] 取消[No]",
                "提示",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Asterisk) != DialogResult.Yes)
                return;
            this.UpdateWarehouse();
        }

        private void UpdateWarehouse()
        {
            this.cbo.Visible = false;

            foreach (DataRow dr in (this.dgv_showdata.DataSource as DataTable).Rows)
            {
                if (!string.IsNullOrEmpty(dr["locId"].ToString()) &&
                    !string.IsNullOrEmpty(dr["storehouseId"].ToString()))
                {
                    RefWebService_BLL.refWebtPartStorehousehad.Instance.UpdateKeyPartlocByTrsn(dr["storehouseId"].ToString(),
                        dr["locId"].ToString(), dr["trsn"].ToString(),
                        this.mFrm.gUserInfo.userId);
                    ShowMsg(LogMsgType.Outgoing,
                        string.Format("编号:{0};料号:{1};修改库位成功:[{2}]-[{3}]",
                        dr["trsn"].ToString(),
                        dr["kpnumber"].ToString(),
                        dr["storehouseId"].ToString(),
                        dr["locId"].ToString()));
                }
            }
            this.LoadAllbuffer();
            this.mFrm.ShowPrgMsg("操作完成", MainParent.MsgType.Outgoing);
        }

        private void bt_submit_Click(object sender, EventArgs e)
        {
            try
            {
                bt_query_Click(null, null);
                if (string.IsNullOrEmpty(this.tb_selectvalue.Text))
                {
                    if (this.cm_selectmodel.Text != "ALL")
                    {
                        MessageBoxEx.Show("如果需要修改全部，请将查询条件选择为'ALL'\n\n否则请填写对应的条件进行修改");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(this.cbo_storehouseId.Text) ||
                    string.IsNullOrEmpty(this.cbo_locId.Text))
                    throw new Exception("仓库编号和库位编号都不能为空");
                for (int i = 0; i < this.dgv_showdata.Rows.Count; i++)
                {
                    this.dgv_showdata["storehouseId", i].Value = this.cbo_storehouseId.Text;
                    this.dgv_showdata["storehouseId", i].Tag = this.cbo_storehouseId.Text;
                    this.dgv_showdata["storehouseId", i].Style = new DataGridViewCellStyle()
                    {
                        BackColor = Color.FromArgb(164, 217, 173)

                    };
                    this.dgv_showdata["locId", i].Value = this.cbo_locId.Text;
                    this.dgv_showdata["locId", i].Tag = this.cbo_locId.Text;
                    this.dgv_showdata["locId", i].Style = new DataGridViewCellStyle()
                    {
                        BackColor = Color.FromArgb(164, 217, 173)

                    };
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void cbo_storehouseId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cbo_storehouseId.Text))
            {
                this.cbo_locId.DataSource = this.DataTableToList(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtStorehouseManage.Instance.GetAllLocIdByWarehouseId(this.cbo_storehouseId.Text)), "locId");
            }
        }

        private void cbo_locId_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cbo_locId.Text))
            {
                bool flag = false;
                for (int i = 0; i < this.cbo_locId.Items.Count; i++)
                {
                    if (this.cbo_locId.Text.ToUpper() == this.cbo_locId.Items[i].ToString())
                        flag = true;
                }
                if (!flag)
                {
                    MessageBoxEx.Show("输入的库位编号非法");
                    this.cbo_locId.SelectAll();
                    this.cbo_locId.Focus();
                }
            }
        }


        private void ShowMsg(LogMsgType msgtype, string msg)
        {
            this.rtb_log.Invoke(new EventHandler(delegate
            {
                rtb_log.TabStop = false;
                rtb_log.SelectedText = string.Empty;
                rtb_log.SelectionFont = new Font(rtb_log.SelectionFont, FontStyle.Bold);
                rtb_log.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtb_log.AppendText(msg + "\n");
                rtb_log.ScrollToCaret();
            }));
        }
    }
}
