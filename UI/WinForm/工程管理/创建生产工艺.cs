using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using FrmBLL;
using System.Threading;

namespace SFIS_V2
{
    public partial class CreateCraft : Office2007Form// Form
    {
        public CreateCraft(MainParent frm)
        {
            InitializeComponent();
            mFrm = frm;
        }
        /// <summary>
        /// 主窗体的引用
        /// </summary>
        private MainParent mFrm;
        private DataTable mdatatable;
        private void bt_getcarfttId_Click(object sender, EventArgs e)
        {
            this.txt_craftid.Text = refWebExecuteSqlCmd.Instance.GetSeqBasics();// BLL.BllMsSqllib.Instance.GetSeqBasics;
        }

        private void bt_savecraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt_craftid.Text))
                    throw new Exception("工艺编号不能为空");
                if (string.IsNullOrEmpty(this.txt_craftname.Text))
                    throw new Exception("工艺描述不能为空");
                if (string.IsNullOrEmpty(this.cbx_beworkseg.Text))
                    throw new Exception("请选择所属制程段");
                if (cbx_beworkseg.Text.Length > 15)
                    throw new Exception("制程段不能大于15位");
                if (txt_craftname.Text.Length>18)
                    throw new Exception("工艺名称不能大于18位");
                if (string.IsNullOrEmpty(txt_testflag.Text))
                    throw new Exception("工艺标记为空");

                ////if (string.IsNullOrEmpty(this.dgv_addcraftitem["CraftItem",0].Value.ToString()))
                if (this.dgv_addcraftitem.Rows.Count - 1 < 1)
                {
                    if (MessageBoxEx.Show(string.Format("工艺\"{0}\"不存在工艺项目\n 是否确定?", this.txt_craftid.Text), "提示",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
                        return;
                }
                else
                {
                    if (MessageBoxEx.Show(string.Format("工艺\"{0}\"存在[{1}]个工艺项目\n 是否确定?",
                        this.txt_craftid.Text, this.dgv_addcraftitem.Rows.Count - 1), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
                        return;
                }            
             
                //if (FrmBLL.publicfuntion.getNewTable(dgv_showcraftitem.DataSource as DataTable, string.Format("{0}='{1}'", (dgv_showcraftitem.DataSource as DataTable).Columns[1].ColumnName, tb_craftdesc.Text)).Rows.Count > 0)
                //{
                //    MessageBox.Show("工艺名称重复", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //         throw new Exception("工艺名称重复");
                //}

                string err =string.Empty;
                string err1 ;

                //if ((err = refWebtCraftInfo.Instance.InsertRefCraftInfo(new WebServices.tCraftInfo.tCraftInfo1()
                //  {
                //      beworkseg = this.cb_bworkseg.Text.Trim(),
                //      craftId = this.tb_craftid.Text.Trim(),
                //      craftname = this.tb_craftdesc.Text.Trim(),
                //      craftparameterurl = this.tb_craftparametfileurl.Text.Trim(),
                //      TestFlag = tb_craftflag.Text.Trim()
                //  }, this.GetCraftItem().ToArray(), out err1)) != "OK")

                Dictionary<string, object> dic = new Dictionary<string, object>();
                  publicfuntion.SerializeControl(dic,panel5);
                  if ((err=refWebtCraftInfo.Instance.InsertRefCraftInfo(ReleaseData.DictionaryToJson(dic), ReleaseData.ListDictionaryToJson(GetCraftItem()), out err1)) != "OK")

                {
                    this.mFrm.ShowPrgMsg(err, MainParent.MsgType.Error);
                    return;
                }
                ShowCraftInfo();
                this.mFrm.ShowPrgMsg(string.Format("工艺\"{0}\"添加成功",
                    this.txt_craftname.Text), MainParent.MsgType.Outgoing);

                this.txt_craftname.Text = "";
                this.txt_craftid.Text = "";
                this.txt_CRAFTPARAMETERURL.Text = "";
                this.tb_qutrycraftid.Text = "";
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void CreateCraft_Load(object sender, EventArgs e)
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

                cbx_checktoolsflag.Items.Clear();
                cbx_checktoolsflag.Items.Add("N");
                cbx_checktoolsflag.Items.Add("Y");
                cbx_checktoolsflag.SelectedIndex = 0;
            }
            #endregion
            try
            {
                Thread th = new Thread(new ThreadStart(ShowCraftInfo));
                th.Start();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void ShowCraftInfo()
        {
       
            //this.dgv_showcraftitem.DataSource = null;
            //this.dgv_showcraftitem.DataSource = this.mdatatable = refWebtCraftInfo.Instance.GetAllCraftInfo();// BLL.tCraftInfo.GetAllCraftInfo();

            DataTable dt_AllCraftInfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo());
            
            FillCraftInfo(dt_AllCraftInfo);
          //  FillWorkSegment(FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllWorksegment()));
            List<string> DistinceColnum = new List<string>();
            DistinceColnum.Add(dt_AllCraftInfo.Columns[3].ColumnName);
            FillWorkSegment(FrmBLL.publicfuntion.DataTableDistinct( dt_AllCraftInfo,DistinceColnum));
        }

        private void FillWorkSegment(DataTable dt)
        {
            this.cbx_beworkseg.Invoke(new EventHandler(delegate
                {
                   

                    this.cbx_beworkseg.Items.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        this.cbx_beworkseg.Items.Add(dr[0].ToString());
                    }
                }));
        }
        private void FillCraftInfo(DataTable dt)
        {
            this.dgv_showcraftitem.Invoke(new EventHandler(delegate
                {
                    this.dgv_showcraftitem.DataSource = dt;
                    this.mdatatable = dt;
                }));

        }
        //WebServices.tCraftInfo.tCraftItem
        //private List<RefWebService_BLL.refWeb_tCraftInfo.tCraftItem> GetCraftItem()
        private IList<IDictionary<string, object>> GetCraftItem()
        {
            try
            {
               // List<WebServices.tCraftInfo.tCraftItem> lsCraftItem = new List<WebServices.tCraftInfo.tCraftItem>();
                IList<IDictionary<string, object>> lsCraftItem = new List<IDictionary<string, object>>();
                Dictionary<string, object> dic = null; 
                for (int x = 0; x < this.dgv_addcraftitem.Rows.Count - 1; x++)
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("CRAFTID", this.txt_craftid.Text);
                    dic.Add("CRAFTITEM", this.dgv_addcraftitem["CraftItem", x].Value.ToString());
                    dic.Add("CRAFTPARAMETERDES", string.IsNullOrEmpty(this.dgv_addcraftitem["CraftItemDesc", x].Value.ToString()) ? "" : this.dgv_addcraftitem["CraftItemDesc", x].Value.ToString());
                    dic.Add("UPPERLIMIT", string.IsNullOrEmpty(this.dgv_addcraftitem["ItemParmetUpperLimit", x].Value.ToString()) ? "" : this.dgv_addcraftitem["ItemParmetUpperLimit", x].Value.ToString());
                    dic.Add("LOWERLIMIT", string.IsNullOrEmpty(this.dgv_addcraftitem["CraftItemParmetLowerLimit", x].Value.ToString()) ? "" : this.dgv_addcraftitem["CraftItemParmetLowerLimit", x].Value.ToString());
                    dic.Add("OTHER", string.IsNullOrEmpty(Convert.ToString(this.dgv_addcraftitem["CraftItemParametOther", x].Value)) ? "" : this.dgv_addcraftitem["CraftItemParametOther", x].Value.ToString());
                    lsCraftItem.Add(dic);
                    
                    //lsCraftItem.Add(new WebServices.tCraftInfo.tCraftItem()
                    //{
                    //    craftId = this.tb_craftid.Text,
                    //    craftItem = this.dgv_addcraftitem["CraftItem", x].Value.ToString(),
                    //    craftparameterdes = string.IsNullOrEmpty(this.dgv_addcraftitem["CraftItemDesc", x].Value.ToString()) ? "" : this.dgv_addcraftitem["CraftItemDesc", x].Value.ToString(),
                    //    upperlimit = string.IsNullOrEmpty(this.dgv_addcraftitem["ItemParmetUpperLimit", x].Value.ToString()) ? "" : this.dgv_addcraftitem["ItemParmetUpperLimit", x].Value.ToString(),
                    //    lowerlimit = string.IsNullOrEmpty(this.dgv_addcraftitem["CraftItemParmetLowerLimit", x].Value.ToString()) ? "" : this.dgv_addcraftitem["CraftItemParmetLowerLimit", x].Value.ToString(),
                    //    other = string.IsNullOrEmpty(Convert.ToString(this.dgv_addcraftitem["CraftItemParametOther", x].Value)) ? "" : this.dgv_addcraftitem["CraftItemParametOther", x].Value.ToString()
                         
                    //});
                }
                return lsCraftItem;
            }
            catch
            {
                throw new Exception("存在空参数,请检查..");
            }
        }
        private void bt_qutrycraft_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_qutrycraftid.Text))
                this.dgv_showcraftitem.DataSource = this.mdatatable;
            else
                this.dgv_showcraftitem.DataSource = publicfuntion.getNewTable(this.mdatatable,
                    string.Format("craftname like '{0}%'", this.tb_qutrycraftid.Text));
        }
        DataTable MDD;
        private void dgv_showcraftitem_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    if (string.IsNullOrEmpty(this.dgv_showcraftitem["craftId", e.RowIndex].Value.ToString()))
                        return;
                    this.txt_craftid.Text = this.dgv_showcraftitem["craftId", e.RowIndex].Value.ToString();
                    this.txt_craftname.Text = this.dgv_showcraftitem["craftname", e.RowIndex].Value.ToString();
                    this.txt_CRAFTPARAMETERURL.Text = this.dgv_showcraftitem["craftparameterurl", e.RowIndex].Value.ToString();
                    this.txt_testflag.Text = this.dgv_showcraftitem["testflag", e.RowIndex].Value.ToString();
                    this.cbx_beworkseg.SelectedIndex = cbx_beworkseg.Items.IndexOf(this.dgv_showcraftitem["BEWORKSEG", e.RowIndex].Value.ToString());
                    this.cbx_checktoolsflag.SelectedIndex = cbx_checktoolsflag.Items.IndexOf(this.dgv_showcraftitem["checktoolsflag", e.RowIndex].Value.ToString());
                   
                    
                   MDD   = FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetCraftItemByCraftId(this.dgv_showcraftitem["craftId", e.RowIndex].Value.ToString()));// BLL.tCraftInfo.GetCraftItemByCraftId(this.dgv_showcraftitem["craftId", e.RowIndex].Value.ToString());
                   this.dgv_addcraftitem.DataSource = MDD.DefaultView;
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void dgv_addcraftitem_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (this.mouseenter)
            {
                for (int x = 2; x < this.dgv_addcraftitem.Columns.Count; x++)
                {
                    this.dgv_addcraftitem[x, this.dgv_addcraftitem.RowCount - 1].Value = "N/A";
                    this.dgv_addcraftitem[x, this.dgv_addcraftitem.RowCount - 1].Style.ForeColor = Color.Red;
                }
                for (int i = 1; i < this.dgv_addcraftitem.RowCount; i++)
                {
                    this.dgv_addcraftitem["CraftItem", i - 1].Value = i.ToString();
                    this.dgv_addcraftitem["CraftItemParametOther", i - 1].Value = "N/A";
                    this.dgv_addcraftitem["CraftItemParametOther", i - 1].Style.ForeColor = Color.Red;
                }
            }
        }

        private void dgv_addcraftitem_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int i = 1; i < this.dgv_addcraftitem.RowCount; i++)
            {
                this.dgv_addcraftitem["CraftItem", i - 1].Value = i.ToString();
            }

        }

        private void dgv_addcraftitem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
                this.dgv_addcraftitem[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Black;
        }

        private void cb_bworkseg_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cbx_beworkseg.Text.Trim()))
            {
                DataTable dt;
                dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllWorksegment());
                if (dt != null && dt.Rows.Count < 1)
                {
                    if (MessageBox.Show("该制程段不存在，是否确认要添加?\n", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        refWebtCraftInfo.Instance.InsertWorkSegment(this.cbx_beworkseg.Text);
                        this.txt_CRAFTPARAMETERURL.Focus();
                    }
                    else
                    {
                        this.cbx_beworkseg.SelectAll();
                        this.cbx_beworkseg.Focus();
                    }
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (this.cbx_beworkseg.Text == dr["beworkseg"].ToString())
                            return;
                    }
                    if (MessageBox.Show("该制程段不存在，是否确认要添加?\n", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        refWebtCraftInfo.Instance.InsertWorkSegment(this.cbx_beworkseg.Text);
                        this.txt_CRAFTPARAMETERURL.Focus();
                    }
                    else
                    {
                        this.cbx_beworkseg.SelectAll();
                        this.cbx_beworkseg.Focus();
                    }
                }
            }
        }

        private void dgv_addcraftitem_MouseEnter(object sender, EventArgs e)
        {
            mouseenter = true;
        }
        private bool mouseenter = false;
        private void dgv_addcraftitem_MouseLeave(object sender, EventArgs e)
        {
            mouseenter = false;
        }

        private void tb_craftdesc_KeyDown(object sender, KeyEventArgs e)
        {
            

        }

        private void tb_craftdesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar == '_') || (e.KeyChar == '\b') || (e.KeyChar == '.') || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'a' && e.KeyChar <= 'z'))
               e.Handled = false;
            else
                e.Handled = true;  
        }

        private void tb_craftflag_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void tb_craftflag_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            } 
        }
    }
}
