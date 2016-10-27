using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class Frm_Create_Route : Office2007Form //Form
    {
        public Frm_Create_Route(MainParent Frm)
        {
            InitializeComponent();
            mFrm = Frm;
        }

        MainParent mFrm;
        private readonly string strFnumName = "RouteEdit";
        List<string> Ls_craftname = new List<string>();
         List<string> Ls_nextcraft = new List<string>();
         List<string> Ls_reflowcraft = new List<string>();
         List<string> Ls_repaircraft = new List<string>();        
        private void GetAllRouteAtt()
        {
            this.Invoke(new EventHandler(delegate
          {
              DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtRouteInfo.Instance.GetAllRouteAtt());           
              dgv_routeatt.DataSource = FrmBLL.publicfuntion.DataTableToSort(dt, dt.Columns[0].ColumnName);
          }));
        }
        private void Get_Craft_Name()
        {
            this.Invoke(new EventHandler(delegate
           {
               cb_craftname.Items.Clear();
               cb_nextcraft.Items.Clear();
               cb_reflowcraft.Items.Clear();
               cb_repaircraft.Items.Clear();

               DataTable dt = FrmBLL.publicfuntion.DataTableToSort(FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo()), "CRAFTNAME");
               foreach (DataRow dr in dt.Rows)
               {
                   Ls_craftname.Add(dr["CRAFTNAME"].ToString());
                   Ls_nextcraft.Add(dr["CRAFTNAME"].ToString());
                   Ls_reflowcraft.Add(dr["CRAFTNAME"].ToString());
                   Ls_repaircraft.Add(dr["CRAFTNAME"].ToString());
               }
               Ls_craftname.Add("");
               Ls_nextcraft.Add("");
               Ls_nextcraft.Add("NA");
               Ls_reflowcraft.Add("");
               Ls_repaircraft.Add("");
               cb_craftname.Items.AddRange(Ls_craftname.ToArray());
               cb_nextcraft.Items.AddRange(Ls_nextcraft.ToArray());
               cb_reflowcraft.Items.AddRange(Ls_reflowcraft.ToArray());
               cb_repaircraft.Items.AddRange(Ls_repaircraft.ToArray());

              
           }));
        }
        private void FillDataGridView(DataTable dt)
        {
            this.dgv_woinfo.Invoke(new EventHandler(delegate
            {
                this.dgv_woinfo.DataSource = dt;
              
            }));
        }

        private void Frm_Create_Route_Load(object sender, EventArgs e)
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
            GetAllRouteAtt();
            Get_Craft_Name();
            imbt_select_Click(null, null);
            cbx_SelectFiled.SelectedIndex = 1;
        }

        private void dgv_routeatt_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string routgroupId = dgv_routeatt.Rows[e.RowIndex].Cells[0].Value.ToString();
                #region 检查是否用户是否正在编辑此流程
                List<string> LsIp = new List<string>();
                LsIp = FrmBLL.publicfuntion.GetIPList();

               string err= FrmBLL.publicfuntion.ChktEditing(routgroupId, this.strFnumName, this.mFrm.gUserInfo.userId, this.mFrm.gUserInfo.username);
                if (err != "OK")
                {
                    if (err.IndexOf("ERROR") != -1)
                    {
                        this.mFrm.ShowPrgMsg(err, MainParent.MsgType.Error);
                        return;
                    }
                    else
                    {
                        MessageBoxEx.Show(string.Format("用户:[{0}]/[{1}]--{2}正在编辑中,您现在不能进行修改!!",
err.Split(':')[1].Split(',')[0], err.Split(':')[1].Split(',')[1], err.Split(':')[1].Split(',')[2]), "提示!!");
                        return;
                    }
                }
                #endregion


                lb_routecode.Text = routgroupId;
                lb_routedesc.Text = dgv_routeatt.Rows[e.RowIndex].Cells[1].Value.ToString();
                dgv_routeinfo.Rows.Clear();
                DataTable dt =FrmBLL.ReleaseData.arrByteToDataTable( refWebtRouteInfo.Instance.Get_Route_Info(routgroupId));

                ///正常流程
                DataTable temp = FrmBLL.publicfuntion.DataTableToSort(FrmBLL.publicfuntion.getNewTable(dt, string.Format("STATION_FLAG='{0}'", "0")), "SEQ");
                foreach (DataRow dr in temp.Rows)
                {
                    dgv_routeinfo.Rows.Add(dr["SEQ"].ToString(), dr["CRAFTNAME"].ToString(), dr["NEXTCRAFTNAME"].ToString(), "", "");
                }
                ///维修流程
                temp = FrmBLL.publicfuntion.DataTableToSort(FrmBLL.publicfuntion.getNewTable(dt, string.Format("STATION_FLAG='{0}'", "1")), "SEQ");
                foreach (DataRow dr in temp.Rows)
                {
                    foreach (DataGridViewRow dgvr in dgv_routeinfo.Rows)
                    {
                        if (dr["CRAFTNAME"].ToString() == dgvr.Cells["CRAFTNAME"].Value.ToString())
                        {

                            dgvr.Cells["REPAIR_CRAFT"].Value = dr["NEXTCRAFTNAME"].ToString();
                        }
                    }
                }
                ///维修回流
                temp = FrmBLL.publicfuntion.DataTableToSort(FrmBLL.publicfuntion.getNewTable(dt, string.Format("STATION_FLAG='{0}'", "2")), "SEQ");
                foreach (DataRow dr in temp.Rows)
                {
                    foreach (DataGridViewRow dgvr in dgv_routeinfo.Rows)
                    {
                        if (dr["CRAFTNAME"].ToString() == dgvr.Cells["REPAIR_CRAFT"].Value.ToString())
                        {
                            dgvr.Cells["REFLOW_CRAFT"].Value = dr["NEXTCRAFTNAME"].ToString();
                        }
                        
                    }
                }
            }
        }

        private void dgv_routeinfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           

        }

        private void dgv_routeinfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                string SEQ = dgv_routeinfo.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (MessageBox.Show(string.Format("是否删除行[{0}]", SEQ), "删除提示",  MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    dgv_routeinfo.Rows.RemoveAt(e.RowIndex);
                    int x = 1;
                    foreach (DataGridViewRow dgvr in dgv_routeinfo.Rows)
                    {
                        dgvr.Cells[0].Value = x++.ToString();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void imbt_add_Click(object sender, EventArgs e)
        {
             
                RoutegroupAtt ra = new RoutegroupAtt(this);
                if (ra.ShowDialog() == DialogResult.OK)
                {
                    if (dgv_routeinfo.Rows.Count > 0)
                    {
                        if (MessageBox.Show("是否要清除右边显示信息?", "清除信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            dgv_routeinfo.Rows.Clear();
                        }
                    }
                }
           
        }

      
 

        private void cb_craftname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_repaircraft.Items.IndexOf("R_" + cb_craftname.Text) >= 0)
                cb_repaircraft.SelectedIndex = cb_repaircraft.Items.IndexOf("R_" + cb_craftname.Text);
            else
                cb_repaircraft.Text = "";
        }

        private void imbt_OK_Click(object sender, EventArgs e)
        {
            if (lb_routecode.Text == "NA")
            {
                MessageBox.Show("请先新增流程属性,再建立途程相关信息", "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(cb_craftname.Text) || string.IsNullOrEmpty(cb_nextcraft.Text))
            {
                MessageBox.Show("途程为空,请确认....", "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (cb_craftname.Text == cb_nextcraft.Text)
            {
                MessageBox.Show("当前途程与下一途程相同,请确认....", "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(cb_repaircraft.Text))
            {
                if (string.IsNullOrEmpty(cb_reflowcraft.Text))
                {
                    MessageBox.Show("有维修途程时,必须有回流途程", "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (chknum.Checked)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dgv_routeinfo);
                int InserRows=Convert.ToInt32( numUpDown.Value)-1;
                this.dgv_routeinfo.Rows.Insert( InserRows, row);
                dgv_routeinfo.Rows[InserRows].Cells[0].Value = "";
                dgv_routeinfo.Rows[InserRows].Cells[1].Value = cb_craftname.Text;
                dgv_routeinfo.Rows[InserRows].Cells[2].Value = cb_nextcraft.Text;
                dgv_routeinfo.Rows[InserRows].Cells[3].Value = cb_repaircraft.Text;
                dgv_routeinfo.Rows[InserRows].Cells[4].Value = cb_reflowcraft.Text;

                #region 重新再排序
                int x = 1;
                foreach (DataGridViewRow dgvr in dgv_routeinfo.Rows)
                {
                    dgvr.Cells[0].Value = x++.ToString();
                }
                #endregion
            }
            else
            dgv_routeinfo.Rows.Add((dgv_routeinfo.Rows.Count + 1).ToString(), cb_craftname.Text,cb_nextcraft.Text, cb_repaircraft.Text, cb_reflowcraft.Text);
            cb_craftname.SelectedIndex = cb_craftname.Items.IndexOf(cb_nextcraft.Text);
            cb_nextcraft.SelectedIndex = cb_nextcraft.Items.IndexOf("");
            cb_repaircraft.SelectedIndex = cb_repaircraft.Items.IndexOf("");
            cb_reflowcraft.SelectedIndex = cb_reflowcraft.Items.IndexOf("");
            cb_craftname_SelectedIndexChanged(null,null);
        }

        private void imbt_clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清除信息吗?", "清除信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                dgv_routeinfo.Rows.Clear();
            }
        }


        private void imbt_save_Click(object sender, EventArgs e)
        {
            string LogMsg = string.Empty;   
            if (dgv_routeinfo.Rows.Count > 0)
            {

                if (Check_The_Integrity_of_The_Routing())
                {

                    //List<WebServices.tRouteInfo.tRouteInfo1> _lsroutinfo = new List<WebServices.tRouteInfo.tRouteInfo1>();
                    IList<IDictionary<string, object>> _lsroutinfo = new List<IDictionary<string, object>>();
                    Dictionary<string, object> dic = null;
                    int x = dgv_routeinfo.Rows.Count;
                    int OutCraftCount = 0;
                    foreach (DataGridViewRow dgvr in dgv_routeinfo.Rows)
                    {
                        dic = new Dictionary<string, object>();
                        dic.Add("CRAFTID", "NA");
                        dic.Add("NEXTROUTEID", "NA");
                        if (dgvr.Cells["NEXTCRAFTNAME"].Value.ToString() == "NA")
                        {
                            OutCraftCount++;
                            dic.Add("ROUTEDESC", "OUT");
                        }
                        else
                            dic.Add("ROUTEDESC", "NA");
                        dic.Add("SEQ", int.Parse(dgvr.Cells["SEQ"].Value.ToString()));
                        dic.Add("STATION_FLAG", 0);
                        dic.Add("ROUTGROUPID", lb_routecode.Text);
                        dic.Add("CRAFTNAME", dgvr.Cells["CRAFTNAME"].Value.ToString());
                        dic.Add("NEXTCRAFTNAME", dgvr.Cells["NEXTCRAFTNAME"].Value.ToString());
                        dic.Add("LsRouteCraftparameter".ToUpper(), null);
                        _lsroutinfo.Add(dic);

                        LogMsg += string.Format("{0},{1}\r\n", dgvr.Cells["CRAFTNAME"].Value.ToString(), dgvr.Cells["NEXTCRAFTNAME"].Value.ToString());
                    }                   
                    if (OutCraftCount > 1)
                    {
                        MessageBox.Show("产出途程发现多个,请更正后保存!");
                        return;
                    }


                    //foreach (DataGridViewRow dgvr in dgv_routeinfo.Rows)
                    //{
                    //    _lsroutinfo.Add(new WebServices.tRouteInfo.tRouteInfo1()
                    //    {
                    //        craftId = "NA",
                    //        nextrouteId = "NA",
                    //        routedesc = "NA",
                    //        seq = int.Parse(dgvr.Cells["SEQ"].Value.ToString()),
                    //        station_flag = 0,
                    //        routgroupId = lb_routecode.Text,
                    //        CraftName = dgvr.Cells["CRAFTNAME"].Value.ToString(),
                    //        NextCtaftName = dgvr.Cells["NEXTCRAFTNAME"].Value.ToString(),
                    //        LsRouteCraftparameter = null
                    //    });
                    //}
                    x++;
                    //_lsroutinfo.Add(new WebServices.tRouteInfo.tRouteInfo1()
                    //{
                    //    craftId = "NA",
                    //    nextrouteId = "NA",
                    //    routedesc = "NA",
                    //    seq = x++,
                    //    station_flag = 0,
                    //    routgroupId = lb_routecode.Text,
                    //    CraftName = dgv_routeinfo.Rows[dgv_routeinfo.Rows.Count - 1].Cells["NEXTCRAFTNAME"].Value.ToString(),
                    //    NextCtaftName ="NA",
                    //    LsRouteCraftparameter = null
                    //});
                    _lsroutinfo[0]["routedesc".ToUpper()] = "IN";                  
                 //   _lsroutinfo[dgv_routeinfo.Rows.Count - 1]["routedesc".ToUpper()] = "OUT";

                    foreach (DataGridViewRow dgvr in dgv_routeinfo.Rows)  //增加维修途程
                    {
                        if (!string.IsNullOrEmpty(dgvr.Cells["REPAIR_CRAFT"].Value.ToString()))
                        {                           
                            dic = new Dictionary<string, object>();
                            dic.Add("CRAFTID", "NA");
                            dic.Add("NEXTROUTEID", "NA");
                            dic.Add("ROUTEDESC", "NA");
                            dic.Add("SEQ",x++);
                            dic.Add("STATION_FLAG", 1);
                            dic.Add("ROUTGROUPID", lb_routecode.Text);
                            dic.Add("CRAFTNAME", dgvr.Cells["CRAFTNAME"].Value.ToString());
                            dic.Add("NEXTCRAFTNAME", dgvr.Cells["REPAIR_CRAFT"].Value.ToString());
                            dic.Add("LsRouteCraftparameter".ToUpper(), null);
                            _lsroutinfo.Add(dic);
                            LogMsg += string.Format("{0},{1}\r\n", dgvr.Cells["CRAFTNAME"].Value.ToString(), dgvr.Cells["NEXTCRAFTNAME"].Value.ToString());
                        }
                    }
                    foreach (DataGridViewRow dgvr in dgv_routeinfo.Rows) //增加回流途程
                    {
                        if (!string.IsNullOrEmpty(dgvr.Cells["REPAIR_CRAFT"].Value.ToString()) && (!string.IsNullOrEmpty(dgvr.Cells["REFLOW_CRAFT"].Value.ToString())))
                        {
                             dic = new Dictionary<string, object>();
                            dic.Add("CRAFTID", "NA");
                            dic.Add("NEXTROUTEID", "NA");
                            dic.Add("ROUTEDESC", "NA");
                            dic.Add("SEQ", x++);
                            dic.Add("STATION_FLAG", 2);
                            dic.Add("ROUTGROUPID", lb_routecode.Text);
                            dic.Add("CRAFTNAME", dgvr.Cells["REPAIR_CRAFT"].Value.ToString());
                            dic.Add("NEXTCRAFTNAME", dgvr.Cells["REFLOW_CRAFT"].Value.ToString());
                            dic.Add("LsRouteCraftparameter".ToUpper(), null);
                            _lsroutinfo.Add(dic);
                            LogMsg += string.Format("{0},{1}\r\n", dgvr.Cells["CRAFTNAME"].Value.ToString(), dgvr.Cells["NEXTCRAFTNAME"].Value.ToString());
                        }
                    }

                    //WebServices.tRouteInfo.tRouteAtt _routeAtt = new WebServices.tRouteInfo.tRouteAtt();

                    //_routeAtt.routgroupId = lb_routecode.Text;
                    //_routeAtt.routgroupdesc = lb_routedesc.Text;
                    //_routeAtt.routgroupxmlContent = "NA";
                    //_routeAtt.LsRouteInfo = _lsroutinfo.ToArray();
                    Dictionary<string, object> _routeAtt = new Dictionary<string, object>();
                    _routeAtt.Add("ROUTGROUPID", lb_routecode.Text);
                    _routeAtt.Add("ROUTGROUPDESC", lb_routedesc.Text);
                    _routeAtt.Add("ROUTGROUPXMLCONTENT", "NA");
                    _routeAtt.Add("LSROUTE", _lsroutinfo);

                    string _StrErr = RefWebService_BLL.refWebtRouteInfo.Instance.InsertRouteAllItme(FrmBLL.ReleaseData.DictionaryToJson(_routeAtt));

                    if (LogMsg.Length > 480)
                        LogMsg = LogMsg.Substring(0, 480);

                    FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "RouteEdit", "RouteEdit",string.Format( "RouteEdit:{0}\r\n{1}", lb_routecode.Text , LogMsg));
                    //refWebRecodeSystemLog.Instance.InsertSystemLog(new WebServices.RecodeSystemLog.T_SYSTEM_LOG()
                    //{
                    //    userId = mFrm.gUserInfo.userId,
                    //    prg_name = "RouteEdit",
                    //    action_type = "RouteEdit",
                    //    action_desc = "RouteEdit: " + lb_routecode.Text
                    //});
                    if (string.IsNullOrEmpty(_StrErr) || _StrErr == "OK")
                    {
                        MessageBox.Show("流程建立成功", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgv_routeinfo.Rows.Clear();
                        GetAllRouteAtt();
                    }
                    else
                    {
                        MessageBox.Show(string.Format("流程建立失败[{0}]", _StrErr), "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("流程信息为空,不能保存", "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tb_routecode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //FrmBLL.publicfuntion.SelectDataGridViewRows(tb_routecode.Text,dgv_routeatt,cbx_SelectFiled.SelectedIndex);
                //for (int x = 0; x < dgv_routeatt.Rows.Count; x++)
                //{
                //    if (dgv_routeatt.Rows[x].Cells[cbx_SelectFiled.SelectedIndex].Value.ToString() == tb_routecode.Text)
                //    {
                //        dgv_routeatt_CellDoubleClick(null, new DataGridViewCellEventArgs(0, x));
                //    }
                //}
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtRouteInfo.Instance.GetAllRouteAtt());
                if (!string.IsNullOrEmpty(tb_routecode.Text))
                {

                    if (cbx_SelectFiled.SelectedIndex == 0)
                        dgv_routeatt.DataSource = FrmBLL.publicfuntion.getNewTable(dt, string.Format("ROUTGROUPID LIKE '{0}'", tb_routecode.Text + "%"));
                    else
                        dgv_routeatt.DataSource = FrmBLL.publicfuntion.getNewTable(dt, string.Format("ROUTGROUPDESC LIKE '{0}'", tb_routecode.Text + "%"));
                }
                else
                    GetAllRouteAtt();
 
                tb_routecode.SelectAll();
            }
        }

        private void imbt_manageroute_Click(object sender, EventArgs e)
        {
            ManageRoute mr = new ManageRoute(mFrm);
            mr.ShowDialog();
        }

        private void chknum_Click(object sender, EventArgs e)
        {
            if (chknum.Checked)
            {
                numUpDown.Enabled = true;
            }
            else
            {
                numUpDown.Enabled = false;
            }
        }

        private void Frm_Create_Route_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                refwebtEditing.Instance.DeletetEditingByUserIdAndPrj(this.mFrm.gUserInfo.userId, this.strFnumName);
            }
            catch
            {
            }
        }

        private void txt_woid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {               
                imbt_select_Click(null, null);
                txt_woid.SelectAll();
            }
        }

        private void imbt_select_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txt_woid.Text))
                    FillDataGridView(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWoInfo.Instance.GetWoInfo(this.txt_woid.Text.Trim(), null, null)));
                else
                    FillDataGridView(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWoInfo.Instance.GetWoInfo(null,null,null)));
            }
            catch (Exception ex)
            {
                mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void dgv_woinfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
 
        }

        private void dgv_woinfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                tb_routecode.Text = dgv_woinfo.Rows[e.RowIndex].Cells["ROUTGROUPID"].Value.ToString();
                cbx_SelectFiled.SelectedIndex = 0;
                tb_routecode_KeyDown(null, new KeyEventArgs(Keys.Enter));

                for (int x=0;x<dgv_routeatt.Rows.Count;x++)
                {
                    if (dgv_routeatt.Rows[x].Cells[0].Value.ToString()==tb_routecode.Text)
                    {
                       dgv_routeatt_CellDoubleClick(null,new DataGridViewCellEventArgs(0,x));
                    }
                }

            }
        }
        /// <summary>
        /// 检查途程完整性
        /// </summary>
        /// <returns></returns>
        private bool Check_The_Integrity_of_The_Routing()
        {
            string Next_Craft = string.Empty;
            string Start_Carft = string.Empty;
            int RowsIndex = 0; //第一行开始途程无需判定上一制程
            foreach (DataGridViewRow dgvr_Next_Craft in dgv_routeinfo.Rows)
            {
                int x = 0;
                Next_Craft = dgvr_Next_Craft.Cells[2].Value.ToString();
                if (Next_Craft == "NA")
                    continue;
                foreach (DataGridViewRow dgvr_Craft in dgv_routeinfo.Rows)
                {
                    if (Next_Craft == dgvr_Craft.Cells[1].Value.ToString())
                    {
                        x++;
                    }
                }
                if (x == 0) //判定下一途程是否连续
                {
                    MessageBox.Show(string.Format("检查途程完整性失败,途程[{0}],没有连续到下一途程", Next_Craft), "检查途程完整性提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                Start_Carft = dgvr_Next_Craft.Cells[1].Value.ToString();
                x = 0;
                RowsIndex++;
                foreach (DataGridViewRow dgvr_Craft in dgv_routeinfo.Rows)
                {
                    if (RowsIndex == 1)
                        continue;
                    if (Start_Carft == dgvr_Craft.Cells[2].Value.ToString())
                    {
                        x++;
                    }
                }
                if (RowsIndex > 1)
                {
                    if (Start_Carft == dgv_routeinfo.Rows[0].Cells[1].Value.ToString()) //如果第一站是跳跃站,则不判定与上一制程联系
                        continue;
                }
               
                if (x == 0 && RowsIndex!=1) //判定开始途程行与上一行连续
                {
                    MessageBox.Show(string.Format("检查途程完整性失败,途程[{0}],没有连续到上一途程", Start_Carft), "检查途程完整性提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

               
            }
            return true;
        }

        private void dgv_woinfo_ColumnHeaderCellChanged(object sender, DataGridViewColumnEventArgs e)
        {
        }

        private void dgv_woinfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FrmBLL.publicfuntion.UpdateFieldName(dgv_woinfo);
        }

        private void dgv_routeatt_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FrmBLL.publicfuntion.UpdateFieldName(dgv_routeatt);
        }

        private void StripMenu_Clear_Click(object sender, EventArgs e)
        {
            imbt_clear_Click(null,null);
        }

        private void StripMenu_Delete_Click(object sender, EventArgs e)
        {
            string RouteGroupId = dgv_routeatt.Rows[CurrentRowIndex].Cells[0].Value.ToString();
            if (MessageBox.Show("确定要删除流程编号[" + RouteGroupId + "]?", "删除信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
               string _StrErr= refWebtRouteInfo.Instance.Delete_Route_Info(RouteGroupId);
               if (_StrErr == "OK")
                   FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "RouteEdit", "RouteDelete", string.Format("Delete:[{0}],[{1}] ", dgv_routeatt.Rows[CurrentRowIndex].Cells[0].Value.ToString(), dgv_routeatt.Rows[CurrentRowIndex].Cells[1].Value.ToString()));
                MessageBox.Show(_StrErr=="OK"?"删除流程成功":"删除流程失败","删除信息提示",MessageBoxButtons.OK,_StrErr=="OK"?MessageBoxIcon.Information:MessageBoxIcon.Error);
                GetAllRouteAtt();
            }
        }
        /// <summary>
        /// 当前要删除的行号
        /// </summary>
        private int CurrentRowIndex { get; set; }
        private void dgv_routeatt_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {

                    //若行已是选中状态就不再进行设置
                    //if (dataGridView1.Rows[e.RowIndex].Selected == false)
                    //{
                    //    dataGridView1.ClearSelection();
                    //    dataGridView1.Rows[e.RowIndex].Selected = true;
                    //}
                    //只选中一行时设置活动单元格
                    // if (dgv_routeatt.SelectedRows.Count == 1)
                    //  {
                    CurrentRowIndex = e.RowIndex;
                    dgv_routeatt.CurrentCell = dgv_routeatt.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //  }
                    //弹出操作菜单
                    contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void cb_craftname_TextUpdate(object sender, EventArgs e)
        {
            this.cb_craftname.Items.Clear();
            List<string> ListNew = new List<string>();
            foreach (string str in Ls_craftname)
            {
                if (str.Contains(cb_craftname.Text))
                {
                    ListNew.Add(str);
                }
            }
            cb_craftname.Items.AddRange(ListNew.ToArray());
            cb_craftname.SelectionStart = cb_craftname.Text.Length;
            this.cb_craftname.DroppedDown = true;
            Cursor = Cursors.Default;

        }

        private void cb_craftname_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(cb_craftname.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 3);  
        }

        private void cb_nextcraft_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(cb_nextcraft.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 3);  
        }

        private void cb_repaircraft_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(cb_repaircraft.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 3);  
        }

        private void cb_reflowcraft_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(cb_reflowcraft.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 3);  
        }

        private void cb_nextcraft_TextUpdate(object sender, EventArgs e)
        {
            this.cb_nextcraft.Items.Clear();
            List<string> ListNew = new List<string>();
            foreach (string str in Ls_craftname)
            {
                if (str.Contains(cb_nextcraft.Text))
                {
                    ListNew.Add(str);
                }
            }
            cb_nextcraft.Items.AddRange(ListNew.ToArray());
            cb_nextcraft.SelectionStart = cb_nextcraft.Text.Length;
            this.cb_nextcraft.DroppedDown = true;
            Cursor = Cursors.Default;
        }

        private void cb_repaircraft_TextUpdate(object sender, EventArgs e)
        {
            this.cb_repaircraft.Items.Clear();
            List<string> ListNew = new List<string>();
            foreach (string str in Ls_craftname)
            {
                if (str.Contains(cb_repaircraft.Text))
                {
                    ListNew.Add(str);
                }
            }
            cb_repaircraft.Items.AddRange(ListNew.ToArray());
            cb_repaircraft.SelectionStart = cb_repaircraft.Text.Length;
            this.cb_repaircraft.DroppedDown = true;
            Cursor = Cursors.Default;
        }

        private void cb_reflowcraft_TextUpdate(object sender, EventArgs e)
        {
            this.cb_reflowcraft.Items.Clear();
            List<string> ListNew = new List<string>();
            foreach (string str in Ls_craftname)
            {
                if (str.Contains(cb_reflowcraft.Text))
                {
                    ListNew.Add(str);
                }
            }
            cb_reflowcraft.Items.AddRange(ListNew.ToArray());
            cb_reflowcraft.SelectionStart = cb_reflowcraft.Text.Length;
            this.cb_reflowcraft.DroppedDown = true;
            Cursor = Cursors.Default;
        }

      

      
 

        
    }
}
