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
    public partial class Frm_MaterialChgLine : Office2007Form //Form
    {
        public Frm_MaterialChgLine(MainParent Frm)
        {
            InitializeComponent();
            mFrm=Frm;
        }
        MainParent mFrm;
        #region 成员变量
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };
        /// <summary>
        /// 显示消息函数
        /// </summary>
        /// <param name="msgtype"></param>
        /// <param name="msg"></param>
        public void ShowMsg(mLogMsgType msgtype, string msg)
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

        #endregion
        private void Frm_MaterialChgLine_Load(object sender, EventArgs e)
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
            
            this.splitContainer1.SplitterDistance = this.panelEx1.Width / 2-20;
            tb_oldstationList.Focus();
        }

        private bool Check_SMT_IO(string MasterId, string woId, int Flag)
        {
            //0 原料站表  1 新料站表
            bool ChkFlag = true;
            string Msg = string.Empty;
            DataTable dtsmtIo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtSmtKpMonitor.Instance.GetSmtIO(MasterId, woId));
            if (Flag == 0)
            {
                if (dtsmtIo.Rows.Count > 0)
                {
                    string Status = dtsmtIo.Rows[0]["STATUS"].ToString();
                    switch (Status)
                    {
                        case "0":
                            Msg = "正在备料";
                            break;
                        case "1":
                            Msg = null;
                            break;
                        case "2":
                            Msg = "正在换线";
                            break;
                        case "3":
                            Msg = "已换线/正在生产";
                            break;
                        case "4":
                            Msg = "下线";
                            break;
                        default:
                            Msg = "料站表状态异常";
                            break;
                    }
                }
                else
                {
                    Msg = "SMT IO 无此料站表信息";
                }
               
            }
            else
            {
                if (dtsmtIo.Rows.Count > 0)
                {
                    Msg = "新料站表已经备料信息";
                }
            }
            if (!string.IsNullOrEmpty(Msg))
            {
                ShowMsg(mLogMsgType.Error, Msg);
                ChkFlag = false;
            }
            return ChkFlag;

        }

        private DataTable Get_Material_List(string MasterId, string woId)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("MASTERID", MasterId);
            dic.Add("WOID", woId);

            return FrmBLL.ReleaseData.arrByteToDataTable(refWebtMaterialPreparation.Instance.Get_T_MATERIAL_PREPARATION(FrmBLL.ReleaseData.DictionaryToJson(dic)));
            //return FrmBLL.ReleaseData.arrByteToDataTable(refWebtMaterialPreparation.Instance.Get_T_MATERIAL_PREPARATION(
            //  new WebServices.tMaterialPreparation.tMaterialPreparation1()
            //{
            //    masterId = MasterId,
            //    woId = woId
            //}));
        }

        bool Old_Station_List = false;
        bool New_Station_List = false;
        private void tb_oldstationList_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_oldstationList.Text) && e.KeyCode == Keys.Enter)
            {
                Old_Station_List = false;
                try
                {
                    if (Check_SMT_IO(tb_oldstationList.Text.Split(' ')[0], tb_oldstationList.Text.Split(' ')[1], 0))
                    {
                        dgvoldStationList.DataSource = Get_Material_List(tb_oldstationList.Text.Split(' ')[0], tb_oldstationList.Text.Split(' ')[1]);
                        LabWo.Text = tb_oldstationList.Text.Split(' ')[1];
                        Old_Station_List = true;
                    }
                }
                catch (Exception ex)
                {
                    ShowMsg(mLogMsgType.Error, "原料站表异常:" + ex.Message);
                }
                finally
                {
                    tb_oldstationList.SelectAll();
                }
            }
        }

        private void tb_newstationList_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_newstationList.Text) && e.KeyCode == Keys.Enter)
            {
                New_Station_List = false;
                try
                {
                    if (Check_SMT_IO(tb_newstationList.Text.Split(' ')[0], tb_newstationList.Text.Split(' ')[1], 1))
                    {
                        if (LabWo.Text == tb_newstationList.Text.Split(' ')[1])
                        {
                            dgvnewStationList.DataSource = Get_Material_List(tb_newstationList.Text.Split(' ')[0], tb_newstationList.Text.Split(' ')[1]);
                            New_Station_List = true;
                        }
                        else
                        {
                            ShowMsg(mLogMsgType.Error, "工单不同");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowMsg(mLogMsgType.Error, "新料站表异常:" + ex.Message);
                }
                finally
                {
                    tb_newstationList.SelectAll();
                }
            }
        }

        private void imbt_check_Click(object sender, EventArgs e)
        {
            if (dgvoldStationList.Rows.Count > 0 && dgvnewStationList.Rows.Count > 0)
            {
                if (dgvoldStationList.Rows.Count == dgvnewStationList.Rows.Count)
                {
                    ShowMsg(mLogMsgType.Warning, "开始原料表与新料表比对数据");
                    bool ChkListFlag = false;
                    foreach (DataGridViewRow dgvr in dgvoldStationList.Rows)
                    {
                        bool ChkFlag = false;                        
                        foreach (DataGridViewRow dgvrnew in dgvnewStationList.Rows)
                        {
                            if ((dgvr.Cells["WOID"].Value.ToString() == dgvrnew.Cells["WOID"].Value.ToString()) &&
                                 (dgvr.Cells["PARTNUMBER"].Value.ToString() == dgvrnew.Cells["PARTNUMBER"].Value.ToString()) &&
                                (dgvr.Cells["STATIONNO"].Value.ToString() == dgvrnew.Cells["STATIONNO"].Value.ToString())    &&
                                  (dgvr.Cells["KPNUMBER"].Value.ToString() == dgvrnew.Cells["KPNUMBER"].Value.ToString())      )
                            {
                                ChkFlag = true;
                            }
                        }
                        if (!ChkFlag)
                        {
                            dgvr.DefaultCellStyle.BackColor = Color.Red;
                            string _StrErr = string.Format("原料表工单[{0}],料号[{1}],料站[{2}],料号[{3}]不存在新料表", dgvr.Cells["WOID"].Value.ToString(), dgvr.Cells["PARTNUMBER"].Value.ToString(), dgvr.Cells["STATIONNO"].Value.ToString(), dgvr.Cells["KPNUMBER"].Value.ToString());
                            ShowMsg(mLogMsgType.Error, "数据比对发生异常:" + _StrErr);
                            break;
                        }
                        else
                        {
                            ChkListFlag = true;
                        }
                    }     
                    if (ChkListFlag)
                    {
                        ChkListFlag = false;
                        ShowMsg(mLogMsgType.Incoming, "原料表与新料表数据比对成功_1");
                        ShowMsg(mLogMsgType.Warning, "开始新料表与原料表比对数据");
                        foreach (DataGridViewRow dgvr in dgvnewStationList.Rows)
                        {
                            bool ChkFlag = false;
                            foreach (DataGridViewRow dgvrold in dgvoldStationList.Rows)
                            {
                                if ((dgvr.Cells["WOID"].Value.ToString() == dgvrold.Cells["WOID"].Value.ToString()) &&
                                 (dgvr.Cells["PARTNUMBER"].Value.ToString() == dgvrold.Cells["PARTNUMBER"].Value.ToString()) &&
                                (dgvr.Cells["STATIONNO"].Value.ToString() == dgvrold.Cells["STATIONNO"].Value.ToString()) &&
                                  (dgvr.Cells["KPNUMBER"].Value.ToString() == dgvrold.Cells["KPNUMBER"].Value.ToString()))
                                {
                                    ChkFlag = true;
                                }
                            }
                            if (!ChkFlag)
                            {
                                dgvr.DefaultCellStyle.BackColor = Color.Red;
                                string _StrErr = string.Format("新料表工单[{0}],料号[{1}],料站[{2}],料号[{3}]不存在原料表", dgvr.Cells["WOID"].Value.ToString(), dgvr.Cells["PARTNUMBER"].Value.ToString(), dgvr.Cells["STATIONNO"].Value.ToString(), dgvr.Cells["KPNUMBER"].Value.ToString());
                                ShowMsg(mLogMsgType.Error, "数据比对发生异常:" + _StrErr);
                                break;
                            }
                            else
                            {
                                ChkListFlag = true;
                            }
                        }

                    }
                    if (ChkListFlag)
                    {
                        ShowMsg(mLogMsgType.Incoming, "新料表与原料表数据比对成功_2");
                        ShowMsg(mLogMsgType.Incoming, "两套料表相符");
                        Update_Smt_Kp_Monitor(dgvnewStationList.Rows[0].Cells["MASTERID"].Value.ToString(), dgvoldStationList.Rows[0].Cells["MASTERID"].Value.ToString(), dgvoldStationList.Rows[0].Cells["WOID"].Value.ToString());
                    }

                }
                else
                {
                    ShowMsg(mLogMsgType.Error, string.Format("新料站表料站总数与旧料站表料站总数不符[{0}]≠[{1}]", dgvnewStationList.Rows.Count.ToString(), dgvoldStationList.Rows.Count.ToString()));
                }
            }
                else
                {
                    ShowMsg(mLogMsgType.Error,"没有数据可以比对");
                }
        }

        private void Update_Smt_Kp_Monitor(string NewMaterId,string OldMaterId,string woId)
        {
            if (MessageBox.Show("是否要进行首盘备料线体转移?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                //DataTable dtSmtKpMaster=FrmBLL.ReleaseData.arrByteToDataTable( refWebSmtKpMaster.Instance.GetSmtKpMaster(new WebServices.tSmtKpMaster.tSmtKPMasterTable()
                //    {
                //        MasterId = NewMaterId
                //    }));
                DataTable dtSmtKpMaster = FrmBLL.ReleaseData.arrByteToDataTable(refWebSmtKpMaster.Instance.GetSmtKpMaster(NewMaterId));

                if (dtSmtKpMaster.Rows.Count > 0)
                {
                    string Machine = dtSmtKpMaster.Rows[0]["LINEID"].ToString();
                    string _StrErr = refWebtSmtKpMonitor.Instance.Update_SmtKpMonitor(woId, OldMaterId, NewMaterId, Machine);
                    if (_StrErr == "OK")
                    {
                        ShowMsg(mLogMsgType.Incoming, "首盘备料线体转移完成");
                        ClearFormControl();
                        FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "Material_Monitor", "Material_MOVE", string.Format("Old_StationList[{0}],New_StationList[{1}],woId[{2}],Machine[{3}]", OldMaterId, NewMaterId, woId, Machine));
                    }
                    else
                    {
                        ShowMsg(mLogMsgType.Error, "首盘备料线体转移失败:" + _StrErr);
                    }
                }
                else
                {
                    ShowMsg(mLogMsgType.Error, "新料站表表头信息异常 SmtKpMaster");
                }
            }
            else
            {
                ShowMsg(mLogMsgType.Warning, "已经取消首盘备料线体变更");
                ClearFormControl();
            }

        }
        private void ClearFormControl()
        {
            tb_oldstationList.Text = string.Empty;
            tb_newstationList.Text = string.Empty;
            dgvoldStationList.DataSource = null;
            dgvnewStationList.DataSource = null;
        }


    }
}
