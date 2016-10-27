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
    public partial class StationNoInfo :Office2007Form// Form
    {
        public StationNoInfo(MainParent mp)
        {
            InitializeComponent();
            mPm = mp;
        }
        private MainParent mPm;
        private void comboBoxEx1_DropDown(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt = FrmBLL.ReleaseData.arrByteToDataTable( refWebtLineInfo.Instance.GetAllLineInfo());// BLL.tLineInfo.GetAllLineInfo();
                
                this.cb_lineId.Items.Clear();
                foreach (DataRow dr in _dt.Rows)
                {
                    this.cb_lineId.Items.Add(dr["线别"].ToString());
                }
            }
            catch (Exception ex)
            {
                mPm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void cb_machineId_DropDown(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtMachineInfo.Instance.GetAllMachineInfo());// BLL.tMachineId.GetAllMachineInfo();
                this.cb_machineId.Items.Clear();
                foreach (DataRow dr in _dt.Rows)
                {
                    this.cb_machineId.Items.Add(dr["machineId"].ToString());
                }
            }
            catch (Exception ex)
            {
                mPm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            /*try
            {
                if (string.IsNullOrEmpty(this.tb_stationno.Text))
                {
                    MessageBoxEx.Show("料站编号不能为空");
                    return;
                }
                if (string.IsNullOrEmpty(this.cb_lineId.Text))
                {
                    MessageBoxEx.Show("请选择料站所在线体");
                    return;
                }
                // BLL.tStationNoInfo.InsertStationNoInfo( new Entity.tStationNoInfo()
                //{
                //    stationno = this.tb_stationno.Text.Trim(),
                //    lineId = this.cb_lineId.Text.Trim(),
                //    machineId = this.cb_machineId.Text.Trim(),
                //    stationspec = this.tb_spec.Text.Trim(),
                //    des = ""
                //});
                refWebtStationNoInfo.Instance.InsertStationNoInfo(new WebServices.tStationNoInfo.tStationNoInfo1() 
                {
                    stationno = this.tb_stationno.Text.Trim(),
                    lineId = this.cb_lineId.Text.Trim(),
                    machineId = this.cb_machineId.Text.Trim(),
                    stationspec = this.tb_spec.Text.Trim(),
                    des = ""
                });

                ShowAllStationNoInfo();
            }
            catch (Exception ex)
            {
                mPm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }*/
        }

        private void ShowAllStationNoInfo()
        {
           // this.dgv_showstatonnoinfo.DataSource = FrmBLL.ReleaseData.arrByteToDataTable(refWebtStationNoInfo.Instance.GetAllStationNoInfo());// BLL.tStationNoInfo.GetAllStationNoInfo();
        }

        private void StationNoInfo_Load(object sender, EventArgs e)
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
               // ShowAllStationNoInfo();
            }
            catch (Exception ex)
            {
                mPm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_addmachine_Click(object sender, EventArgs e)
        {
            MachineInfo mi = new MachineInfo(this.mPm);
            mi.ShowDialog();
        }

        private void bt_deletestation_Click(object sender, EventArgs e)
        {

        }
    }
}
