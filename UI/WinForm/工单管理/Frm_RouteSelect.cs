using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SFIS_V2
{
    public partial class Frm_RouteSelect : Office2007Form //Form
    {
        public Frm_RouteSelect(Office2007Form Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        } 
        Office2007Form mFrm;
        internal class cRouteGroupInOrOut
        {
            public string InCraftId { get; set; }
            public string OutCraftId { get; set; }
        }
        Dictionary<string, cRouteGroupInOrOut> lscgio = new Dictionary<string, cRouteGroupInOrOut>();

        private void Frm_RouteSelect_Load(object sender, EventArgs e)
        {
            DataTable dtTemp = null;
            if (this.mFrm is Frm_WO_Update)
                dtTemp = (this.mFrm as Frm_WO_Update).dtRoute;
            else
                dtTemp = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtRouteInfo.Instance.GetAllRouteInfo());
            DataView dv = dtTemp.DefaultView;
            dv.Sort = "routgroupId,station_flag,SEQ Asc";
            DataTable dt = dv.ToTable();

            foreach (DataRow dr in dt.DefaultView.ToTable(true, "routgroupId").Rows)
            {
                TreeNode a = new TreeNode();
                a.Text = dr["routgroupId"].ToString();
                a.ImageIndex = 0;

                string strIn = "";
                string strOut = "";
                foreach (DataRow dar in FrmBLL.publicfuntion.getNewTable(dt, string.Format("routgroupId='{0}'", dr["routgroupId"].ToString())).Rows)
                {
                    if (dar["station_flag"].ToString() != "2")
                    {
                        if (!a.Nodes.ContainsKey(dar["craftname"].ToString()))
                            a.Nodes.Add(dar["craftname"].ToString(), dar["craftname"].ToString(), 1, 1);

                        if (dar["routedesc"].ToString() == "IN")
                            strIn = dar["craftname"].ToString();
                        if (dar["routedesc"].ToString() == "OUT")
                            strOut = dar["craftname"].ToString();
                        if (dar["station_flag"].ToString() == "1")
                        {
                       
                            if (a.Nodes.ContainsKey(dar["craftname"].ToString()))
                            {
                                a.Nodes[dar["craftname"].ToString()].Nodes.Add(dar["nextcraftname"].ToString(), dar["nextcraftname"].ToString(), 1, 1);
                            }
                        }
                    }
                }

                this.tv_showroute.Nodes.Add(a);
                lscgio.Add(dr["routgroupId"].ToString(), new cRouteGroupInOrOut()
                {
                    InCraftId = strIn,
                    OutCraftId = strOut
                });
            }
        }

        private void bt_select_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_routegroupid.Text))
            {
                if (this.mFrm is WorkOrderCreate)
                {
                    DataTable dt = new DataTable("rout");
                    dt.Columns.Add("routgroupId", typeof(string));
                    dt.Columns.Add("craftname", typeof(string));
                    dt.Columns.Add("routedesc", typeof(string));

                    dt.Rows.Add(this.tb_routegroupid.Text, this.lscgio[this.tb_routegroupid.Text].InCraftId, "IN");
                    dt.Rows.Add(this.tb_routegroupid.Text, this.lscgio[this.tb_routegroupid.Text].OutCraftId, "OUT");
                    (this.mFrm as WorkOrderCreate).mRouteTable = dt;

                    (this.mFrm as WorkOrderCreate).tb_routgroupid.Items.Clear();
                    (this.mFrm as WorkOrderCreate).tb_routgroupid.Text = "";
                    (this.mFrm as WorkOrderCreate).tb_inputstation.Text = "";
                    (this.mFrm as WorkOrderCreate).tb_outputstation.Text = "";
                    (this.mFrm as WorkOrderCreate).tb_routgroupid.Items.Add(this.tb_routegroupid.Text);
                    (this.mFrm as WorkOrderCreate).tb_routgroupid.SelectedIndex = 0;

                    #region 检查BI数量
                    //   (this.mfrm as WorkOrderCreate).tb_chkbicraft.Text="NA";
                    //DataTable  dt1=  FrmBLL.publicfuntion.getNewTable(dtchkBI,string.Format("routgroupId='{0}' and nextcraftname='{1}' and station_flag=0 ",this.tb_routegroupid.Text, (this.mfrm as WorkOrderCreate).tb_craftbi.Text));
                    //   if (dt1.Rows.Count>0)
                    //   {
                    //       string BeforeRoute=dt1.Rows[0]["craftname"].ToString();
                    //       DataTable dt2 = FrmBLL.publicfuntion.getNewTable(dtchkBI, string.Format("routgroupId='{0}' and craftname='{1}' and station_flag=0   and nextcraftname<>'{2}' and   nextcraftname<>'NA' ", this.tb_routegroupid.Text, BeforeRoute, (this.mfrm as WorkOrderCreate).tb_craftbi.Text));
                    //       if (dt2.Rows.Count > 0)
                    //       {
                    //           (this.mfrm as WorkOrderCreate).tb_chkbicraft.Text = dt2.Rows[0]["nextcraftname"].ToString(); ;
                    //       }
                    //   }
                    #endregion



                    this.DialogResult = DialogResult.OK;
                }

                if (this.mFrm is Frm_WO_Update)
                {
                    DataTable dt = new DataTable("rout");
                    dt.Columns.Add("routgroupId", typeof(string));
                    dt.Columns.Add("craftname", typeof(string));
                    dt.Columns.Add("routedesc", typeof(string));

                    dt.Rows.Add(this.tb_routegroupid.Text, this.lscgio[this.tb_routegroupid.Text].InCraftId, "IN");
                    dt.Rows.Add(this.tb_routegroupid.Text, this.lscgio[this.tb_routegroupid.Text].OutCraftId, "OUT");
                    (this.mFrm as Frm_WO_Update).mRouteTable = dt;

                    (this.mFrm as Frm_WO_Update).txt_routgroupid.Text = tb_routegroupid.Text.Trim();
                   (this.mFrm as Frm_WO_Update).txt_inputgroup.Text=  this.lscgio[this.tb_routegroupid.Text].InCraftId;
                   (this.mFrm as Frm_WO_Update).txt_outputgroup.Text = this.lscgio[this.tb_routegroupid.Text].OutCraftId;
                    this.DialogResult = DialogResult.OK;
                }
                if (this.mFrm is ManageRoute)
                {
                    (this.mFrm as ManageRoute).tb_routgroupid.Text = tb_routegroupid.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                }
            }
        }
        private void tv_showroute_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {

                this.tb_routegroupid.Text = e.Node.FullPath.Split('\\')[0];

                //FrmBLL.Ftp_MyFtp ftp = new FrmBLL.Ftp_MyFtp();
                //this.pictureBox1.Image = ftp.GetImage(this.tb_routegroupid.Text + ".png");

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "错误");
            }
        }

      
    }
}
