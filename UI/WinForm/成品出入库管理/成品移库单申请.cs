using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.IO;

namespace SFIS_V2
{
    public partial class Frm_StockMove : Office2007Form// Form
    {
        public Frm_StockMove(MainParent frm)
        {
            InitializeComponent();
            this.mfrm = frm;
        }

        MainParent mfrm;
        private DataTable dt_qty = null;


        enum SelectConditionEnum
        {
            移库单号,
            料号
           
        }
        enum SelectFlagEnum
        {
            move_store_id,
            partnumber
        }

        private void Frm_StockMove_Load(object sender, EventArgs e)
        {
            try
            {

                #region 添加应用程序

                
                if (this.mfrm .gUserInfo .rolecaption  == "系统开发员")
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

                this.dgv_productsninfo.RowsDefaultCellStyle.BackColor = Color.Bisque;
                this.dgv_productsninfo.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            }
            catch (Exception ex)
            {
                this.mfrm .ShowPrgMsg (ex.Message, MainParent.MsgType.Error);
            }
        }

        private void tb_cancel_Click(object sender, EventArgs e)
        {
            tb_initializers();
        }

        private void tb_initializers()
        {
            this.tb_pn.Text = "";
            this.tb_qty.Text = "";
            this.tb_out.Text = "";
            this.tb_in.Text = "";
            this.tb_pn.Focus();
            this.tb_pn.SelectAll();
        }

        private void tb_add_Click(object sender, EventArgs e)
        {
            try
            {
                #region 基本条件判断

                this.rtbmsg.Text = "";
                if (string.IsNullOrEmpty(this.tb_pn.Text.Trim()))
                { 
                    ShowPrgMsg("请输入成品料号！", mLogMsgType.Error); return;
                }

                if (string.IsNullOrEmpty(this.tb_qty.Text.Trim()))
                   
                { 
                    ShowPrgMsg("请填写移库数量！", mLogMsgType.Error); return;
                }
                if (string.IsNullOrEmpty(this.tb_out.Text.Trim()))
                  
                {
                    ShowPrgMsg("请填写移出库厂区！", mLogMsgType.Error); return;
                }
                if (string.IsNullOrEmpty(this.tb_in.Text.Trim()))
                    
                {
                    ShowPrgMsg("请填写移入库厂区！", mLogMsgType.Error); return;
                }

                if (this.tb_in.Text.Trim() == this.tb_out.Text.Trim())
                {
                    ShowPrgMsg("移出库和移入库厂区相同，无需移库！", mLogMsgType.Error);
                    return;
                }
                dt_qty = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWarehouseWipTracking.Instance.GetQtyByPartnumber(this.tb_pn.Text.Trim(), this.tb_out.Text.Trim()));

                if (dt_qty.Rows.Count > 0)
                {
                    string OutQty = dt_qty.Rows[0]["qty"].ToString();

                    if (Convert.ToInt32(OutQty) == 0)
                    {
                        ShowPrgMsg("没有资料，请确认成品料号和厂区是否一致！", mLogMsgType.Error); return;
                    }

                    if (Convert.ToInt32(OutQty) < Convert.ToInt32(this.tb_qty.Text.Trim()))
                    {
                        ShowPrgMsg("移库数量大于库存数量,库存数量为：" + OutQty, mLogMsgType.Error); return;
                    }

                    if (this.listView.Items.Count > 0)
                    {
                        for (int i = 0; i < this.listView.Items.Count; i++)
                        {
                            string ssss = this.listView.Items[i].SubItems[1].Text.ToString();
                            if (this.tb_pn.Text.Trim() == ssss)
                            {
                                ShowPrgMsg("该成品料号已经在当前移库单号存在，请输入其他成品料号！", mLogMsgType.Error);
                                this.tb_qty.Text = "";
                                this.tb_out.Text = "";
                                this.tb_in.Text = "";
                                this.tb_pn.Focus();
                                this.tb_pn.SelectAll();
                                return;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(this.tb_id.Text))
                    {
                        this.tb_id.Text = refWebtz_whs_move_store.Instance.Sel_MOVEWH_ID();


                    }

                #endregion

                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();
                    dt.Columns.Add("cH_ID");
                    dt.Columns.Add("cH_PN");
                    dt.Columns.Add("cH_PROD");
                    dt.Columns.Add("cH_QTY");
                    dt.Columns.Add("cH_OUT");
                    dt.Columns.Add("cH_IN");

                    DataRow NEWROW = dt.NewRow();
                    NEWROW["cH_ID"] = this.tb_id.Text;
                    NEWROW["cH_PN"] = this.tb_pn.Text.Trim();
                    NEWROW["cH_PROD"] = dt_qty.Rows[0]["productname"].ToString();
                    NEWROW["cH_QTY"] = this.tb_qty.Text.Trim();
                    NEWROW["cH_OUT"] = this.tb_out.Text.Trim();
                    NEWROW["cH_IN"] = this.tb_in.Text.Trim();

                    dt.Rows.Add(NEWROW);
                    ds.Tables.Add(dt);
                    ds.AcceptChanges();
                    ListViewItem ivi = new ListViewItem();
                    this.listView.View = System.Windows.Forms.View.Details;

                    ivi.SubItems[0].Text = ds.Tables[0].Rows[0]["cH_ID"].ToString();
                    ivi.SubItems.Add(ds.Tables[0].Rows[0]["cH_PN"].ToString());
                    ivi.SubItems.Add(ds.Tables[0].Rows[0]["cH_PROD"].ToString());
                    ivi.SubItems.Add(ds.Tables[0].Rows[0]["cH_QTY"].ToString());
                    ivi.SubItems.Add(ds.Tables[0].Rows[0]["cH_OUT"].ToString());
                    ivi.SubItems.Add(ds.Tables[0].Rows[0]["cH_IN"].ToString());

                    this.listView.Items.Add(ivi);

                    tb_initializers();

                }
                else
                {
                    ShowPrgMsg("没有资料，请确认成品料号和厂区是否一致！", mLogMsgType.Error); return;
                }
            }
            catch (Exception ex)
            {
                    this.mfrm .ShowPrgMsg (ex.Message, MainParent.MsgType.Error);
            }

           
           
        }
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };

        public void ShowPrgMsg(string msg, mLogMsgType msgtype)
        {
            try
            {
                this.rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = mLogMsgTypeColor[(int)msgtype];
                    rtbmsg.AppendText(msg + "\n");
                    rtbmsg.ScrollToCaret();
                }));
            }
            catch
            {
            }
        }

        private void bt_modify_Click(object sender, EventArgs e)
        {
            try
            {
            if (this.listView.Items.Count > 0)
            {
                if (this.listView.SelectedItems!=null)
                {
                    
                   this.tb_pn.Text = this.listView.SelectedItems[0].SubItems[1].Text;
                   this.tb_qty.Text = this.listView.SelectedItems[0].SubItems[2].Text;
                   this.listView.SelectedItems[0].Remove();

                }
                else
                {
                    ShowPrgMsg("请选择需要修改的选项！" , mLogMsgType.Error); return;
                }
            }
            }
            catch (Exception ex)
            {
                this.mfrm .ShowPrgMsg (ex.Message, MainParent.MsgType.Error);
            }

    
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            if (this.listView.Items.Count > 0)
            {
                if (this.listView.SelectedItems != null)
                {
                    for (int index = 0; index < this.listView.SelectedItems.Count; index++)
                {
                    this.listView.SelectedItems[index].Remove();
                    if (this.listView.Items.Count == 0)
                    {
                        tb_initializers();
                        this.tb_id.Text = "";
                    }
                }
                
                }
            }
        }

        private void bt_confirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView.Items.Count > 0)
                {
                    string Result="";
                    for (int i = 0; i < this.listView.Items.Count; i++)
                    {
                         Result = refWebtz_whs_move_store.Instance.Insertmove_store_id(new WebServices.tz_whs_move_store.tz_whs_move_storeTable()
                        {
                                MOVE_STORE_ID = this.listView .Items [i].SubItems [0].Text,
                                PARTNUMBER = this.listView .Items [i].SubItems [1].Text,
                                PRODUCTNAME = this.listView.Items[i].SubItems[2].Text,
                                QTY = this.listView .Items [i].SubItems [3].Text,
                                MOVE_STORE = this.listView .Items [i].SubItems [4].Text,
                                IMMIGRATION_STORE = this.listView .Items [i].SubItems [5].Text,
                                USERID= mfrm.gUserInfo.userId
                        });

                        if (Result != "OK")
                        {
                            ShowPrgMsg("移库单号申请失败！", mLogMsgType.Error); return;
                        }

                    }

                    if ( Result == "OK")
                    {
                        //this.listView.Clear();
                        listView.Items.Clear();
                        tb_initializers();                     
                        ShowPrgMsg("移库单号:" + this.tb_id.Text + "申请成功！", mLogMsgType.Error);
                        this.tb_id.Text = "";
                        return;
                        
                    }

                }
            }

            catch (Exception ex)
            {
                this.mfrm .ShowPrgMsg  (ex.Message, MainParent.MsgType.Error);
            }
        }

        private string flag = "";

        private void cb_selectcondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bt_select.Enabled = true;
            switch (this.cb_selectcondition.SelectedIndex)
            {
                case 0:
                    flag = SelectFlagEnum.move_store_id.ToString();
                    break;
                case 1:
                    flag = SelectFlagEnum.partnumber.ToString();
                    break;
            }
        }

        private void tb_selectvalue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                bt_select_Click(null, null);
            }
        }

        private void tb_value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                bt_selectd_Click(null, null);
            }
        }

        private void bt_select_Click(object sender, EventArgs e)
        {
            try
            {
                this.bt_select.Enabled = false;
                DataSet dstemp = new DataSet(); 
                if (string.IsNullOrEmpty(flag) && string.IsNullOrEmpty(this.tb_selectvalue.Text.Trim()))
                {
                    this.dgv_productsninfo.DataSource = null;
                    this.bt_select.Enabled = true;
                    return;
                }
                else
                {DataTable dtSapCode = FrmBLL.ReleaseData.arrByteToDataTable(
                         refWebtz_whs_move_store.Instance.Getmovestoreid(this.tb_selectvalue.Text.Trim(), this.tb_selectvalue.Text.Trim()));
                    if (dtSapCode.Rows.Count > 0)
                    {

                        DataView dvs = dtSapCode.DefaultView;
                        DataTable dTemp = dvs.ToTable();
                        this.dgv_productsninfo.DataSource = dTemp;
                    }
                    else
                    {
                        MessageBox.Show("没有数据");
                    }
                }
            

                this.bt_select.Enabled = true;

            }
            catch (Exception ex)
            {
                this.bt_select.Enabled = true;
                this.mfrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_selectd_Click(object sender, EventArgs e)
        {
            try
            {
                this.bt_selectd.Enabled = false;
               
                if (string.IsNullOrEmpty(flag) && string.IsNullOrEmpty(this.tb_value.Text.Trim()))
                {
                    this.dgv_moveinfo.DataSource  = null;
                    this.bt_selectd.Enabled = true;
                    return;
                }
                else
                {
                    DataTable dtCode = FrmBLL.ReleaseData.arrByteToDataTable(
                            refWebtz_whs_move_store.Instance.Getmovestoreinfo(this.tb_value.Text.Trim()));
                    if (dtCode.Rows.Count > 0)
                    {

                        DataView dv = dtCode.DefaultView;
                        DataTable dT = dv.ToTable();
                        this.dgv_moveinfo.DataSource = dT;
                    }
                    else
                    {
                        MessageBox.Show("没有数据");
                    }
                }


                this.bt_selectd.Enabled = true;

            }
            catch (Exception ex)
            {
                this.bt_selectd.Enabled = true;
                this.mfrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void to_excel_Click(object sender, EventArgs e)
        {
            if (this.dgv_productsninfo.RowCount == 0)
            {
                MessageBox.Show("没有数据可以导出到Excel"); return;
            }
            else
            {
                //DataToExcel(dgvStockInOut);
                WriteExcel(dgv_productsninfo);
            }
        }

        private void bt_rrefresh_Click(object sender, EventArgs e)
        {
            bt_select_Click(null, null);
        }

        private void bt_excel_Click(object sender, EventArgs e)
        {
            if (this.dgv_productsninfo.RowCount == 0)
            {
                MessageBox.Show("没有数据可以导出到Excel");
                return;
            }
            else
            {
                //DataToExcel(dgv_productsninfo);
                WriteExcel(dgv_productsninfo);
            }
        }

        public static void WriteExcel(DataGridView m_DataView)
        {
            if (m_DataView.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可供导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                SaveFileDialog ss = new SaveFileDialog();
                ss.Filter = "Execl files (*.xls)|*.xls";
                ss.FilterIndex = 0;
                ss.RestoreDirectory = true;
                //saveFileDialog2.CreatePrompt = true;
                ss.Title = "导出文件保存路径";
                ss.FileName = null;
                ss.ShowDialog();
                string FileName = ss.FileName;

                if (FileName.Length != 0)
                {
                    //toolStripProgressBar1.Visible = true;
                    //toolStripProgressBar1.Value = 0;
                    try
                    {
                        StreamWriter sw = new StreamWriter(FileName, false, Encoding.GetEncoding("gb2312"));
                        StringBuilder sb = new StringBuilder();


                        for (int k = 0; k < m_DataView.Columns.Count; k++)
                        {
                            sb.Append(@"=""" + m_DataView.Columns[k].HeaderText.ToString() + @"""" + "\t");
                        }
                        sb.Append(Environment.NewLine);

                        for (int i = 0; i < m_DataView.Rows.Count; i++)
                        {
                            for (int j = 0; j < m_DataView.Columns.Count; j++)
                            {
                                sb.Append(@"=""" + m_DataView.Rows[i].Cells[j].Value.ToString() + @"""" + "\t");
                            }
                            sb.Append(Environment.NewLine);//每写一行数据后换行
                            //toolStripProgressBar1.Value += 100 / dt.Rows.Count;
                        }
                        sw.Write(sb.ToString());
                        sw.Flush();
                        sw.Close();//释放资源
                        MessageBox.Show("数据已经成功导出到：" + ss.FileName.ToString(), "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        //toolStripProgressBar1.Value = 0;
                        //toolStripProgressBar1.Visible = false;
                    }
                }
            }
        }

        private void to_excelid_Click(object sender, EventArgs e)
        {
            if (this.dgv_moveinfo.RowCount == 0)
            {
                MessageBox.Show("没有数据可以导出到Excel");
                return;
            }
            else
            {
                //DataToExcel(dgv_productsninfo);
                WriteExcel(dgv_moveinfo);
            }
        }

        private void bt_rrefreshid_Click(object sender, EventArgs e)
        {
            bt_selectd_Click(null, null);
        }

        private void b_excel_Click(object sender, EventArgs e)
        {
            if (this.dgv_moveinfo.RowCount == 0)
            {
                MessageBox.Show("没有数据可以导出到Excel");
                return;
            }
            else
            {
                //DataToExcel(dgv_productsninfo);
                WriteExcel(dgv_moveinfo);
            }
        }

        private void tb_qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.tb_out.SelectAll();
                this.tb_out.Focus();
            }
        }

        private void tb_out_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.tb_in.SelectAll();
                this.tb_in.Focus();
            }
        }

        private void tb_pn_KeyDown(object sender, KeyEventArgs e)
        {
             if  (e.KeyCode == Keys.Enter)
                {
                this.tb_qty.SelectAll();
                this.tb_qty.Focus();
                }
        }
    }
}
