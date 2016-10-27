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
    public partial class FrmTargetPlan : Office2007Form //Form
    {
        public FrmTargetPlan(MainParent sInfo)
        {
            InitializeComponent();
            sMain = sInfo;
        }

        MainParent sMain;




        private void GetCraftparameterurl()
        {
            string[] item = refWebtCraftInfo.Instance.GetCraftInfoCraftparameterurl();
            foreach (string Ditem in item)
            {
                cbstationlist.Invoke(new EventHandler(delegate
                {
                    cbstationlist.Items.Add(Ditem);
                    Cb_EndStation.Items.Add(Ditem);
                }));
            }
        }


        private delegate void GetLineAllList();
        GetLineAllList Linelist;

        //private delegate void Craftparameterurl();
        //Craftparameterurl ListGroup;
        string[] time;
        private void FrmTargetPlan_Load(object sender, EventArgs e)
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

            try
            {
                lbPartNumber.Text = "";

                GetTargetPlan(); //获取资料

                dtpWorkdate.MaxDate = DateTime.Now.Date.AddDays(6);
                dtpWorkdate.MinDate = DateTime.Now.Date.AddDays(-1);

                //ListGroup = new Craftparameterurl(GetCraftparameterurl);
                //ListGroup.BeginInvoke(null, null);

                Linelist = new GetLineAllList(GetLineList);
                Linelist.BeginInvoke(null, null);

                GetWoIdPartNumberList();

            }
            catch
            {
                MessageBoxEx.Show("加载失败, 请重新打开");
            }
        }

        DataTable mdt = null;
        private void GetWoIdPartNumberList()
        {

            mdt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWipTracking.Instance.GetPartNumberAndwoIdList());//.Tables[0];
            foreach (DataRow dr in mdt.Rows)
            {
                cbwolist.Invoke(new EventHandler(delegate
                {
                    cbwolist.Items.Add(dr["woid"].ToString());
                }));
            }

            for (int i = 0; i < 24; i++)
            {
                Cb_WorkTime.Items.Add(i.ToString().PadLeft(2, '0') + ":30 ~ " +
                   ( (i + 1).ToString().PadLeft(2, '0') == "24" ? "00" : (i + 1).ToString().PadLeft(2, '0')) + ":30");
            }

        }

        private void GetLineList()
        {
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtLineInfo.Instance.GetAllLineInfo());
            foreach (DataRow dr in dt.Rows)
            {
                cblinelist.Invoke(new EventHandler(delegate
                {

                    cblinelist.Items.Add(dr["线别"].ToString());
                }));

            }
        }

        private void tbTargetQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void cbwolist_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void tbstarttime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void tbendtime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void LBok_MouseLeave(object sender, EventArgs e)
        {
            LBok.BackColor = Color.FromArgb(255, 128, 255);
        }

        private void lbcancel_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            btdelete.Enabled = true;
            btadded.Enabled = true;
        }

        private void lbcancel_MouseLeave(object sender, EventArgs e)
        {
            lbcancel.BackColor = Color.FromArgb(255, 128, 255);
        }

        private void LBok_MouseEnter(object sender, EventArgs e)
        {
            LBok.BackColor = Color.FromArgb(120, 230, 115);
        }

        private void LBok_Click(object sender, EventArgs e)
        {

        }

        private void lbcancel_MouseEnter(object sender, EventArgs e)
        {
            lbcancel.BackColor = Color.FromArgb(120, 230, 115);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        private void LBok_Click_1(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(dtpWorkdate.Value.Date.ToString()))
            {
                sMain.ShowPrgMsg("日期错误,请选择", MainParent.MsgType.Error);
                dtpWorkdate.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cbwolist.Text))
            {
                sMain.ShowPrgMsg("工单为空,请选择工单", MainParent.MsgType.Error);
                cbwolist.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cbClass.Text))
            {
                sMain.ShowPrgMsg("班别为空,请选择班别", MainParent.MsgType.Error);
                cbClass.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cbstationlist.Text))
            {
                sMain.ShowPrgMsg("开始途程为空,请选择途程", MainParent.MsgType.Error);
                cbstationlist.Focus();
                return;
            }
            if (string.IsNullOrEmpty(Cb_EndStation.Text))
            {

                sMain.ShowPrgMsg("结束途程为空,请选择途程", MainParent.MsgType.Error);
                Cb_EndStation.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cblinelist.Text))
            {
                sMain.ShowPrgMsg("线别为空,请选择线别", MainParent.MsgType.Error);
                cblinelist.Focus();
                return;
            }
            //if (tbstarttime.Text == tbendtime.Text)
            //{
            //    sMain.ShowPrgMsg("开始时间与结束时间相同,请确认...", MainParent.MsgType.Outgoing);
            //    return;
            //}
            if (cbClass.Text == "D")
            {
                //:

                time = Cb_WorkTime.Text.Split('~');
                string starttime = time[0].Trim().Split(':')[0] + time[0].Trim().Split(':')[1];
                string endtime = time[1].Trim().Split(':')[0] + time[1].Trim().Split(':')[1];
                if (string.Compare(starttime, "0830") < 0 || string.Compare(starttime, "2030") > 0 ||
                    string.Compare(endtime, "0830") < 0 || string.Compare(endtime, "2030") > 0)
                {
                    sMain.ShowPrgMsg("请确认白班时间段为[0830-2030]", MainParent.MsgType.Outgoing);
                    return;
                }
            }

            if (string.IsNullOrEmpty(tbside.Text))
            {
                sMain.ShowPrgMsg("面别为空,请重新输入", MainParent.MsgType.Error);
                tbside.Focus();
                return;
            }
            switch (sflag)
            {
                case Flag.新增:
                    {
                        InserTargetPlanData();
                        sMain.ShowPrgMsg("新增完成", MainParent.MsgType.Incoming);
                    }
                    break;
                case Flag.修改:
                    {
                        UpdateTargetPlan(RowIdx);
                        sMain.ShowPrgMsg("修改完成", MainParent.MsgType.Incoming);
                    }
                    break;

            }

            GetTargetPlan();

        }
        private enum Flag
        {
            新增,
            修改
        }

        Flag sflag;

        private void btadded_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            sflag = Flag.新增;
            btdelete.Enabled = false;

            cbwolist.Text = "";
            cbClass.Text = "";
            cbstationlist.Text = "";
            Cb_EndStation.Text = "";
            dtpWorkdate.Value = DateTime.Now.Date;
            lbPartNumber.Text = "";
            cblinelist.Text = "";
            tbTargetQty.Text = "";
            //tbstarttime.Text = "";
            //tbendtime.Text = "";
            Cb_WorkTime.Text = "";
            tbside.Text = "";

            cbwolist.Enabled = true;
            cbClass.Enabled = true;
            cbstationlist.Enabled = true;
            Cb_EndStation.Enabled = true;
            dtpWorkdate.Enabled = true;
            cblinelist.Enabled = true;
        }

        private void InserTargetPlanData()
        {

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("WORKDATE", dtpWorkdate.Value.Date.ToString("yyyyMMdd"));
            dic.Add("CLASS", cbClass.Text.Trim());
            dic.Add("LOCSTATION", cbstationlist.Text.Trim());
            dic.Add("ENDSTATION", Cb_EndStation.Text.Trim());
            dic.Add("WOID", cbwolist.Text.Trim());
            dic.Add("PARTNUMBER", lbPartNumber.Text.Trim());
            dic.Add("LINE", cblinelist.Text.Trim());
            dic.Add("TARGETQTY", tbTargetQty.Text.Trim());
            dic.Add("STARTTIME", time[0]);
            dic.Add("ENDTIME", time[1]);
            dic.Add("SIDE", tbside.Text.Trim());

            RefWebService_BLL.refWebtTargetPlan.Instance.InsertTargetPlan(FrmBLL.ReleaseData.DictionaryToJson(dic));
            //RefWebService_BLL.refWebtTargetPlan.Instance.InsertTargetPlan(new WebServices.tTargetPlan.tTargetPlanTable()
            //    {
            //        woId = cbwolist.Text.Trim(),
            //        PartNumber = lbPartNumber.Text.Trim(),
            //        WorkDate = dtpWorkdate.Value.Date.ToString("yyyyMMdd"),
            //        Class = cbClass.Text.Trim(),
            //        Line = cblinelist.Text.Trim(),
            //        EndTime = time[1],
            //        StartTime = time[0],
            //        Side = tbside.Text.Trim(),
            //        Locstation = cbstationlist.Text.Trim(),
            //        EndStatin=Cb_EndStation.Text.Trim(),
            //        TargetQty = tbTargetQty.Text.Trim()
            //    });

            FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "目标设定", "新增", "工作日期:" + dtpWorkdate.Value.ToString() + " 工单:" + cbwolist.Text.Trim() + " 目标:" + tbTargetQty.Text.Trim());
            tbTargetQty.Text = "";
            //tbstarttime.Text = "";
            // tbendtime.Text = "";
            Cb_WorkTime.Text = "";
            tbside.Text = "";
        }
        private void DeleteTargetPlan(string Idx)
        {
            refWebtTargetPlan.Instance.DeleteTargetPlan(Idx);
            FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "目标设定", "删除", " 工单:" + cbwolist.Text.Trim() + " 目标:" + tbTargetQty.Text.Trim());
            sMain.ShowPrgMsg("删除资料完成", MainParent.MsgType.Incoming);
            GetTargetPlan();
        }


        private void UpdateTargetPlan(string Rowid)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("ROWID", Rowid);
            //dic.Add("CLASS", cbClass.Text.Trim());
            //dic.Add("LOCSTATION", cbstationlist.Text.Trim());
            //dic.Add("ENDSTATION", Cb_EndStation.Text.Trim());
            //dic.Add("WOID", cbwolist.Text.Trim());
            //dic.Add("PARTNUMBER", lbPartNumber.Text.Trim());
            //dic.Add("LINE", cblinelist.Text.Trim());
            dic.Add("TARGETQTY", tbTargetQty.Text.Trim());
            dic.Add("STARTTIME", time[0]);
            dic.Add("ENDTIME", time[1]);
            dic.Add("SIDE", tbside.Text.Trim());
            refWebtTargetPlan.Instance.UpdateTargetPlan(FrmBLL.ReleaseData.DictionaryToJson(dic));
            //refWebtTargetPlan.Instance.UpdateTargetPlan(new WebServices.tTargetPlan.tTargetPlanTable()
            //    {
            //        // woId = cbwolist.Text.Trim(),
            //        //  PartNumber = lbPartNumber.Text.Trim(),
            //        //  WorkDate = dtpWorkdate.Value.ToString(),
            //        //   Class = cbClass.Text.Trim(),
            //        //   Line = cblinelist.Text.Trim(),
            //        Idx = Rowid,
            //        EndTime = time[1],
            //        StartTime = time[0],
            //        Side = tbside.Text.Trim(),
            //        //   Locstation = cbstationlist.Text.Trim(),
            //        TargetQty = tbTargetQty.Text.Trim()
            //    });
            FrmBLL.publicfuntion.InserSystemLog(sMain.gUserInfo.userId, "目标设定", "修改", " 工单:" + cbwolist.Text.Trim() + " 目标:" + tbTargetQty.Text.Trim());
        }


        private void GetTargetPlan()
        {
            DgvTarget.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtTargetPlan.Instance.GetTargetPlanAll());
        }

        string RowIdx = "";
        private void DgvTarget_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex != -1) && (btdelete.Enabled))
            {
                RowIdx = DgvTarget[0, e.RowIndex].Value.ToString();
                dtpWorkdate.Value = DateTime.Now.Date;
                cbClass.Text = DgvTarget["班别", e.RowIndex].Value.ToString();
                cbstationlist.Items.Clear();
                cbstationlist.Items.Add(DgvTarget["站位", e.RowIndex].Value.ToString());
                cbstationlist.SelectedIndex = 0;

                Cb_EndStation.Items.Clear();
                Cb_EndStation.Items.Add(DgvTarget["站位", e.RowIndex].Value.ToString());
                Cb_EndStation.SelectedIndex = 0;

                cbwolist.Text = DgvTarget["工单", e.RowIndex].Value.ToString();
                lbPartNumber.Text = DgvTarget["产品料号", e.RowIndex].Value.ToString();
                cblinelist.Text = DgvTarget["线别", e.RowIndex].Value.ToString();
                tbTargetQty.Text = DgvTarget["目标产出", e.RowIndex].Value.ToString();
                //tbstarttime.Text = DgvTarget["开始时间", e.RowIndex].Value.ToString(); //string.Format("{0:HH:mm}", DgvTarget["开始时间", e.RowIndex].Value);
                //tbendtime.Text = DgvTarget["结束时间", e.RowIndex].Value.ToString(); //string.Format("{0:HH:mm}", DgvTarget["结束时间", e.RowIndex].Value);
                Cb_WorkTime.Text = DgvTarget["开始时间", e.RowIndex].Value.ToString() + "~" + DgvTarget["结束时间", e.RowIndex].Value.ToString();
                tbside.Text = DgvTarget["面别", e.RowIndex].Value.ToString();

            }
        }

        private void DgvTarget_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                sflag = Flag.修改;
                btadded.Enabled = false;
                btdelete.Enabled = false;
                panel1.Visible = true;
                cbwolist.Enabled = false;
                cbClass.Enabled = false;
                cbstationlist.Enabled = false;
                Cb_EndStation.Enabled = false;
                dtpWorkdate.Enabled = false;
                cblinelist.Enabled = false;
            }
        }

        private void btdelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(RowIdx))
            {

                if (MessageBox.Show("确定要删除吗?", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DeleteTargetPlan(RowIdx);
                }
            }
            else
            {
                sMain.ShowPrgMsg("请选择需要删除的资料", MainParent.MsgType.Error);
            }
        }

        private void cbwolist_Validating(object sender, CancelEventArgs e)
        {
            this.cbstationlist.Items.Clear();
            this.Cb_EndStation.Items.Clear();
            if (string.IsNullOrEmpty(this.cbwolist.Text.Trim()))
            {
                this.lbPartNumber.Text = "";
                this.cbwolist.Text = "";
                return;
            }
            DataTable dtPart = FrmBLL.publicfuntion.getNewTable(mdt, string.Format("woId='{0}'", cbwolist.Text.Trim()));
            if (dtPart == null || dtPart.Rows.Count < 1)
            {
                this.sMain.ShowPrgMsg(string.Format("工单号{0}不存在", this.cbwolist.Text), MainParent.MsgType.Warning);
                this.lbPartNumber.Text = "";
                this.cbwolist.Text = "";
                return;
            }
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetAllCraftInfo(this.cbwolist.Text.Trim()));
            foreach (DataRow item in dt.Rows)
            {
                this.cbstationlist.Items.Add(item["craftname"].ToString());
                this.Cb_EndStation.Items.Add(item["craftname"].ToString());
            }
            lbPartNumber.Text = dtPart.Rows[0][0].ToString();
        }
    }
}
