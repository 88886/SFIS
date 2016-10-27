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
    public partial class Frm_RepairMain : Office2007Form//Form
    {
        public Frm_RepairMain(MainParent Frm)
        {
            InitializeComponent();
            sFrm = Frm;
        }
        MainParent sFrm;


        public string M_MYGROUP = string.Empty;
        public string M_sThisEsn = string.Empty;
        public string M_sThisErrorCode = string.Empty;
        public string M_sThisErrorCode_Desc = string.Empty;
        public string M_sThiswoId = string.Empty;
        public string M_sThisPartnumber = string.Empty;
        public string M_sThisCraftId = string.Empty;
        public string M_sThisInputUser = string.Empty;
        public string M_sThisStatus = string.Empty;
        public string M_sThisRepairer=string.Empty;
        public string M_sThisLineId = string.Empty;
        public string M_sThisLocation = string.Empty;
        public string M_sThisRemark = string.Empty;
        public string M_sThisOutCraftId = string.Empty;
        public string M_sThisTClassDate = string.Empty;
        public string M_sThisTclass = string.Empty;
        public string M_sThisTworkSection = string.Empty;
        public string M_sThisRclassDate = string.Empty;
        public string M_sThisRclass = string.Empty;
        public string M_sThisRworkSection = string.Empty;
        public string M_sThisDuty = string.Empty;
        public string M_sThisAteStationNo = string.Empty;
        public string M_sMac = string.Empty;
        public string M_sImei = string.Empty;
        public string M_sSn = string.Empty;
        public string M_sProduct = string.Empty;
        public string M_sVersion = string.Empty;
        public string M_sIn_Line_Time = string.Empty;
        public string M_sIn_Station_Time = string.Empty;
        public string M_sThisRouteCode = string.Empty;
        /// <summary>
        /// 维修解除绑定
        /// </summary>
        public bool M_Repair_Release_Bound = false;

        public string M_sTheNextGroup = string.Empty;
        public List<string> Ls_M_sTheNextGroup = new List<string>();
        public string M_sRepair_Rowid = string.Empty;
        public string M_RepairType = string.Empty;

        public string R_sThisReasonCode = string.Empty;
        public string R_sThisReasonCodeDesc = string.Empty;
        public string R_sThisLocation = string.Empty;
        public string R_sThisDuty = string.Empty;
        public string R_sThisMemo = string.Empty;

        public IDictionary<string, object> DicMaterial = new Dictionary<string, object>();
        public IList<IDictionary<string, object>> List_DicMaterial = new List<IDictionary<string, object>>();

        string My_PCIP=string.Empty;
        string My_PCMAC=string.Empty;

        private void Frm_RepairMain_Load(object sender, EventArgs e)
        {
            try
            {
                #region 添加应用程序
                if (this.sFrm.gUserInfo.rolecaption == "系统开发员")
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
                this.sFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }

            M_sThisRepairer = sFrm.gUserInfo.userId;
            Initialization();
            AddDgvColnum();         
            txt_Repairer.Text = sFrm.gUserInfo.username;
            this.imbt_Finish.Enabled = false;
            this.imbt_UpdateRepair.Enabled = false;
            this.imbt_addNewRC.Enabled = false;
            Frm_InputSn Fis = new Frm_InputSn(this);
            Fis.ShowDialog();

        }
        public void Initialization()
        {
            LabEsn.Text = string.Empty;
            LabSn.Text = string.Empty;
            LabwoId.Text = string.Empty;
            LabLine.Text = string.Empty;
            LabPartnumber.Text = string.Empty;
            LabProductname.Text = string.Empty;
            LabVersion.Text = string.Empty;
            LabGroup.Text = string.Empty;
            LabInLineTime.Text = string.Empty;
            LabInStationTime.Text = string.Empty;
            LabMac.Text = string.Empty;
            LabImei.Text = string.Empty;
            LabErrorCode.Text = string.Empty;
            LabErrorDesc.Text = string.Empty;
            LabAteStationNo.Text = string.Empty;
            LabErrCnt.Text = "0";

            M_sThisEsn = string.Empty;

            My_PCIP = FrmBLL.publicfuntion.GetIpv4();
            My_PCMAC = FrmBLL.publicfuntion.getMacList()[0];
        }
        private void AddDgvColnum()
        {
            dgvRepair.Columns.Add("ERROR_CODE","不良代码");
            dgvRepair.Columns.Add("ERROR_DESC", "不良描述");
            dgvRepair.Columns.Add("WOID", "工单");
            dgvRepair.Columns.Add("PARTNUMBER", "料号");
            dgvRepair.Columns.Add("PRODUCTNAME", "产品描述");
            dgvRepair.Columns.Add("INPUTUSER", "人员");

            dgvRepair.Columns.Add("REASON_CODE", "原因代码");
            dgvRepair.Columns.Add("REASON_DESC", "原因描述");
            dgvRepair.Columns.Add("LOCATION", "零件位置");
            dgvRepair.Columns.Add("DUTY", "责任单位");
            dgvRepair.Columns.Add("MEMO", "备注");
            dgvRepair.Columns.Add("ROWID", "ROWID");
            dgvRepair.Columns[dgvRepair.Columns.Count - 1].Visible = false;


            dgvNgCount.Columns.Add("ERRORCODE", "");
          
        }

        public void SHOW_RC_DATA()
        {
            LabEsn.Text = M_sThisEsn;
            LabSn.Text = M_sSn;
            LabwoId.Text = M_sThiswoId;
            LabLine.Text = M_sThisLineId;
            LabProductname.Text = M_sProduct;
            LabPartnumber.Text = M_sThisPartnumber;
            LabVersion.Text = M_sVersion;
            LabGroup.Text = M_sThisLocation;
            LabInLineTime.Text = M_sIn_Line_Time;
            LabInStationTime.Text = M_sIn_Station_Time;
            LabMac.Text = M_sMac;
            LabImei.Text = M_sImei;
            LabErrorCode.Text = M_sThisErrorCode;
            LabErrorDesc.Text = M_sThisErrorCode_Desc;
            LabAteStationNo.Text = M_sThisAteStationNo;

            dgvRepair.Rows.Clear();
            dgvNgCount.Rows.Clear();
            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebRepairInfo.Instance.GetRepairSnInfo(M_sThisEsn));
            
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["STATUS"].ToString() == "1")
                {
                    List<string> ErrorDesc = refWebtErrorCode.Instance.GetErrorCodeDesc(dr["ERRORCODE"].ToString()).ToList();
                    dgvRepair.Rows.Add(dr["ERRORCODE"].ToString(), ErrorDesc[0], dr["WOID"].ToString(), dr["PARTNUMBER"].ToString(), M_sProduct, dr["INPUTUSER"].ToString(), dr["REASONCODE"].ToString(), "", dr["LOCATION"].ToString(), "", dr["REMARK"].ToString(),dr["ID"].ToString());
                    dgvNgCount.Rows.Add(dr["ERRORCODE"].ToString());
                }
            }
            LabErrCnt.Text = dgvNgCount.Rows.Count.ToString();
            Fill_DataGridView_Color();
            this.imbt_UpdateRepair.Enabled = true;
            this.imbt_Finish.Enabled = true;
            this.imbt_addNewRC.Enabled = true;
           
        }

        public void SHOW_REPAIR_DATA()
        {
            if (M_RepairType == "D")
            {              

                dgvRepair.Rows.Add(dgvRepair.Rows[dgvRepair.Rows.Count - 1].Cells["ERROR_CODE"].Value.ToString(),
                                    dgvRepair.Rows[dgvRepair.Rows.Count - 1].Cells["ERROR_DESC"].Value.ToString(),
                                    dgvRepair.Rows[dgvRepair.Rows.Count - 1].Cells["WOID"].Value.ToString(),
                                    dgvRepair.Rows[dgvRepair.Rows.Count - 1].Cells["PARTNUMBER"].Value.ToString(),
                                    dgvRepair.Rows[dgvRepair.Rows.Count - 1].Cells["PRODUCTNAME"].Value.ToString(),
                                    sFrm.gUserInfo.userId,
                                   R_sThisReasonCode, R_sThisReasonCodeDesc, R_sThisLocation, R_sThisDuty,R_sThisMemo,"");                
            }
            else
            {
                foreach (DataGridViewRow dgvr in dgvRepair.Rows)
                {
                    if (dgvr.Cells["ROWID"].Value.ToString() == M_sRepair_Rowid)
                    {
                        dgvr.Cells["REASON_CODE"].Value = R_sThisReasonCode;
                        dgvr.Cells["REASON_DESC"].Value = R_sThisReasonCodeDesc;
                        dgvr.Cells["LOCATION"].Value = R_sThisLocation;
                        dgvr.Cells["DUTY"].Value = R_sThisDuty;
                        dgvr.Cells["MEMO"].Value = R_sThisMemo;
                    }
                }
            }
            Fill_DataGridView_Color();
           
        }
        private void Frm_RepairMain_SizeChanged(object sender, EventArgs e)
        {
          
        }

        private void dgvRepair_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        private void imbt_UpdateRepair_Click(object sender, EventArgs e)
        {       
            Repair_Update();
        }

        private void Frm_RepairMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M)
                Repair_Update();
            if (e.KeyCode == Keys.C)
                imbt_Close_Click(null,null);
            if (e.KeyCode == Keys.F)
                Finish();
        }
        private void ShowMessage(string Msg)
        {
            MessageBox.Show(Msg,"信息提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        private void Repair_Update()
        {
            if (dgvRepair.Rows.Count > 0)
            {

                bool Check_Repair = false;
                foreach (DataGridViewRow dgvr in dgvRepair.Rows)
                {
                    if (!string.IsNullOrEmpty(dgvr.Cells["REASON_CODE"].Value.ToString()) && dgvr.Cells["ROWID"].Value.ToString() == M_sRepair_Rowid)
                        Check_Repair = true;

                }
                if (Check_Repair)
                {
                    ShowMessage("维修信息已填写完成");
                    return;
                }


                M_RepairType = string.Empty;
                if (dgvRepair.Rows.Count == 1)
                    M_sRepair_Rowid = dgvRepair.Rows[0].Cells["ROWID"].Value.ToString();
                else
                {
                    if (string.IsNullOrEmpty(M_sRepair_Rowid))
                    {
                        ShowMessage("请先选择待维修信息");
                        return;
                    }
                }
                Frm_Update fu = new Frm_Update(this);
                fu.ShowDialog();
            }
        }

        private void imbt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void imbt_Close_Click(object sender, EventArgs e)
        {
            Initialization();
            dgvNgCount.Rows.Clear();
            dgvRepair.Rows.Clear();
            List_DicMaterial.Clear();
            this.imbt_Finish.Enabled = false;
            this.imbt_UpdateRepair.Enabled = false;
            this.imbt_addNewRC.Enabled = false;
            Frm_InputSn fis = new Frm_InputSn(this);
            fis.ShowDialog();
        }

        private void dgvNgCount_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvNgCount.ClearSelection();
        }


        private void Fill_DataGridView_Color()
        {

            foreach (DataGridViewRow dgvr in dgvNgCount.Rows)
            {

                dgvr.DefaultCellStyle.BackColor = Color.Red;
                dgvr.DefaultCellStyle.ForeColor = Color.Yellow;
                dgvr.Cells[0].Style.BackColor = Color.Red;
                dgvr.Cells[0].Style.ForeColor = Color.Yellow;
                dgvr.DefaultCellStyle.Font = new Font("宋体", 10.0F, FontStyle.Bold);

            }
            foreach (DataGridViewRow dgvr in dgvRepair.Rows)
            {
                if (string.IsNullOrEmpty(dgvr.Cells["REASON_CODE"].Value.ToString()))
                {
                    dgvr.DefaultCellStyle.BackColor = Color.Red;
                    dgvr.Cells[0].Style.BackColor = Color.Red;
                }
                else
                {
                    dgvr.DefaultCellStyle.BackColor = Color.Green;
                    dgvr.Cells[0].Style.BackColor = Color.Green;
                }
                dgvr.Cells[0].Style.ForeColor = Color.Yellow;
                dgvr.DefaultCellStyle.ForeColor = Color.Yellow;

                dgvr.DefaultCellStyle.Font = new Font("宋体", 10.0F, FontStyle.Bold);

            }

            dgvRepair.ClearSelection();
            dgvNgCount.ClearSelection();

        
        }

        private void dgvRepair_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Fill_DataGridView_Color();            
        }

        private void imbt_Finish_Click(object sender, EventArgs e)
        {
            Finish();
        }
        private void Finish()
        {
            if (Check_Finish())
            {
                imbt_Close_Click(null,null);
            }
        }
        private bool Check_Finish()
        {
            bool Result = false;
            try
            {

                if (dgvRepair.Rows.Count <= 0)
                    return Result;
              

                foreach (DataGridViewRow dgvr in dgvRepair.Rows)
                {
                    if (string.IsNullOrEmpty(dgvr.Cells["REASON_CODE"].Value.ToString()))
                        throw new Exception("维修没有完成,请先填写维修信息");
                }
                if (M_Repair_Release_Bound)
                {
                    if (!Release_Bound(M_sThisEsn))
                        return Result;
                }

                   if (Ls_M_sTheNextGroup.Count > 0)
                {
                    if (Ls_M_sTheNextGroup.Count > 1)
                    {
                        if (Ls_M_sTheNextGroup.Count == 1 && M_sTheNextGroup == Ls_M_sTheNextGroup[0])
                            M_sThisOutCraftId = M_sTheNextGroup;
                        else
                        {
                            Frm_SelcetNextStation sns = new Frm_SelcetNextStation(this);
                            sns.ShowDialog();
                            M_sThisOutCraftId = M_sTheNextGroup;
                        }
                    }
                    else
                    {
                        M_sThisOutCraftId = M_sTheNextGroup;
                    }
                }
                else
                {
                    throw new Exception("没有维修回流途程信息");
                }

                string _StrErr = string.Empty;
                if (List_DicMaterial.Count > 0)
                {                  
                    _StrErr = refWebRepairInfo.Instance.InsertRepairMaterialInfo(FrmBLL.ReleaseData.ListDictionaryToJson(List_DicMaterial));
                 if (_StrErr != "OK")
                     throw new Exception("Inser Material Error:"+_StrErr);
                }
               
                IList<IDictionary<string, object>> LsDic = new List<IDictionary<string, object>>();
                IDictionary<string, object> mst = null;
                foreach (DataGridViewRow dgvr in dgvRepair.Rows)
                {
                    mst = new Dictionary<string, object>();
                    mst.Add("ESN", M_sThisEsn);
                    mst.Add("ERROR_CODE", dgvr.Cells["ERROR_CODE"].Value.ToString());
                    mst.Add("REASON_CODE", dgvr.Cells["REASON_CODE"].Value.ToString());
                    mst.Add("WOID", dgvr.Cells["WOID"].Value.ToString());
                    mst.Add("PARTNUMBER",  dgvr.Cells["PARTNUMBER"].Value.ToString());
                    mst.Add("CRAFTID", M_sThisLocation);
                    mst.Add("REUSER", sFrm.gUserInfo.userId);
                    mst.Add("LINE", "REPAIR");
                    mst.Add("LOCATION", dgvr.Cells["LOCATION"].Value.ToString());
                    mst.Add("REMARK", dgvr.Cells["MEMO"].Value.ToString());
                    mst.Add("OUTCRAFTID", M_sThisOutCraftId);
                    mst.Add("DUTY", dgvr.Cells["DUTY"].Value.ToString());
                    mst.Add("ID", string.IsNullOrEmpty(dgvr.Cells["ROWID"].Value.ToString()) ? "NA" : dgvr.Cells["ROWID"].Value.ToString());
                    LsDic.Add(mst);             
                }

               _StrErr= refWebRepairInfo.Instance.UpdateRepairInformation(FrmBLL.ReleaseData.ListDictionaryToJson(LsDic));
                if (_StrErr != "OK")
                    throw new Exception("Update Repair Error:" + _StrErr);

                MessageBox.Show("维修已经完成", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Result = true;
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "维修未完成提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


           return Result;
        }

        private void dgvRepair_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                M_sRepair_Rowid = dgvRepair.Rows[e.RowIndex].Cells["ROWID"].Value.ToString();
            }
            Fill_DataGridView_Color();
        }

        private void Menu_changeKeyParts_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(M_sThisEsn))
            {
                Frm_ChangeKP fck = new Frm_ChangeKP(this);
                fck.ShowDialog();
                if (M_Repair_Release_Bound)
                {
                    lab_ReleaseBound.Visible = true;
                    lab_ReleaseBound.Text = "解除绑定关系";
                }
                else
                {
                    lab_ReleaseBound.Visible = false;
                }
            }
        }

        private void imbt_addNewRC_Click(object sender, EventArgs e)
        {
            bool Check_Repair = false;
            foreach (DataGridViewRow dgvr in dgvRepair.Rows)
            {
                if (!string.IsNullOrEmpty(dgvr.Cells["REASON_CODE"].Value.ToString()))
                    Check_Repair = true;
              
            }
            if (Check_Repair)
            {
                M_RepairType = "D";
                Frm_Update fu = new Frm_Update(this);
                fu.ShowDialog();
            }
            else
            {
                ShowMessage("请先填写维修信息,再增加其它维修项");
            }
        }

        /// <summary>
        /// 解除绑定关系
        /// </summary>
        /// <param name="Esn"></param>
        /// <returns></returns>
        private bool Release_Bound(string Esn)
        {
               string _StrErr = string.Empty;            
                DataTable dtwoInfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(M_sThiswoId,null));
                if (dtwoInfo.Rows.Count > 0)
                {                
                  //  _StrErr = refWebtWipKeyPart.Instance.Insert_WipKeyParts_Undo(Esn, null, null);
                   _StrErr= InserSnUnBind(Esn);
                    if (_StrErr == "OK")
                    {
                        DataTable dtKps =FrmBLL.ReleaseData.arrByteToDataTable( refWebtWipKeyPart.Instance.GetWipKeyPart(Esn));
                        foreach (DataRow dr in dtKps.Rows)
                        {
                            if (dr["SNTYPE"].ToString() == "KPESN")
                            {
                                Dictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add("ESN", dr["SNVAL"].ToString());
                                dic.Add("WIPSTATION", "MB_Repair");
                                _StrErr = refWebtWipTracking.Instance.Update_Wip_Tracking(FrmBLL.ReleaseData.DictionaryToJson(dic));
                            }
                        }
                        if (_StrErr == "OK")
                        {
                            _StrErr = refWebtWipKeyPart.Instance.DELETE_WipKeyParts(Esn, null, null);
                            if (_StrErr == "OK")
                            {
                                M_sTheNextGroup = dtwoInfo.Rows[0]["INPUTGROUP"].ToString();
                                _StrErr = "OK";
                            }
                        }
                        else
                            _StrErr = "Update WipStation Error:"+_StrErr;
                    }
                    else
                      _StrErr="Insert_WipKeyParts_Undo\r\n" + _StrErr;

                }
                else
                {
                   _StrErr="没有找到工单信息";
                }

                if (_StrErr == "OK")
                    return true;
                else
                {
                    MessageBox.Show(_StrErr,"解绑提示");
                    return false;
                }
        }

        /// <summary>
        /// 记录解除绑定信息
        /// </summary>
        /// <param name="ESN"></param>
        /// <returns></returns>
        private string InserSnUnBind(string ESN)
        {

           string _StrErr="OK";
           DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyParts("ESN", ESN));
            IList<IDictionary<string, object>> ListDic = new List<IDictionary<string, object>>();
            IDictionary<string, object> dic = null;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SNTYPE"].ToString() == "KPESN")
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("ESN", dr["ESN"].ToString());
                    dic.Add("WOID", dr["WOID"].ToString());
                    dic.Add("SNTYPE", dr["SNTYPE"].ToString());
                    dic.Add("SNVAL", dr["SNVAL"].ToString());
                    dic.Add("STATION", dr["STATION"].ToString());
                    dic.Add("KPNO", dr["KPNO"].ToString());
                    dic.Add("RECDATE", dr["RECDATE"].ToString());
                    dic.Add("USERID", sFrm.gUserInfo.userId);
                    dic.Add("UNBIND_STATION", M_MYGROUP);
                    dic.Add("PC_IP", My_PCIP);
                    dic.Add("PC_MAC", My_PCMAC);
                    ListDic.Add(dic);
                }
            }

            if (ListDic.Count > 0)             
                _StrErr = refWebtSnUnBind.Instance.Insert_Sn_UnBind(FrmBLL.ReleaseData.ListDictionaryToJson(ListDic));

            return _StrErr;
             
        }
        
       
    }

}
