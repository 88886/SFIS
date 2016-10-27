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
    public partial class MachineInfo :Office2007Form// Form
    {
        public MachineInfo(MainParent mp)
        {
            InitializeComponent();
            this.mPm = mp;
        }
        private MainParent mPm;

        private void cb_lineId_DropDown(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt =  FrmBLL.ReleaseData.arrByteToDataTable(refWebtLineInfo.Instance.GetAllLineInfo());// BLL.tLineInfo.GetAllLineInfo();
                this.cbx_lineId.Items.Clear();
                foreach (DataRow  dr in _dt.Rows)
                {
                    this.cbx_lineId.Items.Add(dr["线别"].ToString());               
                }
            }
            catch (Exception ex)
            {
                this.mPm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt_machineId.Text))
                {
                    MessageBoxEx.Show("请填写工站编号");
                    return;
                }
                if (string.IsNullOrEmpty(this.cbx_lineId.Text))
                {
                    MessageBoxEx.Show("请选择线体");
                    return;
                }

                DataTable machinedatatable = FrmBLL.ReleaseData.arrByteToDataTable(refWebtMachineInfo.Instance.GetMachineInfoByMachineid(this.txt_machineId.Text.Trim()));
                if (machinedatatable != null && machinedatatable.Rows.Count > 0)
                {
                    //refWebtMachineInfo.Instance.EditMachineInfo(this.txt_machineId.Text.Trim(),new WebServices.tMachineInfo.tMachineInfo1() 
                    //{
                    //    machineId = this.txt_machineId.Text.Trim(),
                    //    lineId = this.cbx_lineId.Text.Trim(),
                    //    fixtureId = this.txt_fixtureId.Text.Trim(),
                    //    machinedesc = this.txt_machinedesc.Text,
                    //    ipaddress1 = this.txt_ipaddress.Text.Trim(),
                    //    ipaddress2 = this.txt_ipaddress1.Text.Trim(),
                    //    ipaddress3 = this.txt_ipaddress2.Text.Trim(),
                    //    note = ""
                    //});

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    FrmBLL.publicfuntion.SerializeControl(dic, panel2);
                    refWebtMachineInfo.Instance.EditMachineInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));
                }
                else
                {
                    //refWebtMachineInfo.Instance.InsertMachineInfo(new WebServices.tMachineInfo.tMachineInfo1() 
                    //{
                    //    machineId = this.txt_machineId.Text.Trim(),
                    //    lineId = this.cbx_lineId.Text.Trim(),
                    //    fixtureId = this.txt_fixtureId.Text.Trim(),
                    //    machinedesc = this.txt_machinedesc.Text,
                    //    ipaddress1 = this.txt_ipaddress.Text.Trim(),
                    //    ipaddress2 = this.txt_ipaddress1.Text.Trim(),
                    //    ipaddress3 = this.txt_ipaddress2.Text.Trim(),
                    //    note = ""
                    //});
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    FrmBLL.publicfuntion.SerializeControl(dic, panel2);
                    refWebtMachineInfo.Instance.InsertMachineInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));
                }
                this.ShowData();
            }
            catch (Exception ex)
            {
                this.mPm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void ShowData()
        {
            this.dgv_showmachineInfo.DataSource =FrmBLL.ReleaseData.arrByteToDataTable(  refWebtMachineInfo.Instance.GetAllMachineInfo());// BLL.tMachineId.GetAllMachineInfo();
        }

        private void MachineInfo_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mPm.gUserInfo.rolecaption == "系统开发员")
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
            try
            {
                this.ShowData();
            }
            catch (Exception ex)
            {
                this.mPm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除该机器吗?\r\n  机器编号=" + this.txt_machineId.Text, "删除信息提示 ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                refWebtMachineInfo.Instance.DeleteMachineInfo(this.txt_machineId.Text);
                ShowData();
            }
        }

        private void dgv_showmachineInfo_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (!string.IsNullOrEmpty(this.dgv_showmachineInfo["machineId", e.RowIndex].Value.ToString()))
                {
                    this.txt_machineId.Text = this.dgv_showmachineInfo["machineId", e.RowIndex].Value.ToString();
                    this.txt_fixtureId.Text = this.dgv_showmachineInfo["fixtureId", e.RowIndex].Value.ToString();
                    this.cbx_lineId.Text = this.dgv_showmachineInfo["lineId", e.RowIndex].Value.ToString();
                    this.txt_machinedesc.Text = this.dgv_showmachineInfo["machinedesc", e.RowIndex].Value.ToString();
                    this.txt_ipaddress.Text = this.dgv_showmachineInfo["IpAddress", e.RowIndex].Value.ToString();
                    this.txt_ipaddress1.Text = this.dgv_showmachineInfo["IpAddress1", e.RowIndex].Value.ToString();
                    this.txt_ipaddress2.Text = this.dgv_showmachineInfo["IpAddress2", e.RowIndex].Value.ToString();
                }
            }
        }
    }
}
