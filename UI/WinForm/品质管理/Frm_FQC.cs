using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RefWebService_BLL;
using System.Threading;
using System.Xml;

namespace SFIS_V2
{
    public partial class Frm_FQC : Office2007Form
    {
        public Frm_FQC(MainParent mfm)
        {
            InitializeComponent();
            mFrm = mfm;
        }
        MainParent mFrm;

        DataTable Dt_Box_Id = new DataTable();
        DataTable Dt_Product_Id = new DataTable();//产品信息
        DataTable Dt_QC_Info = new DataTable();//检验信息
        // DataTable dt_ProName;
        DataTable dt;//Tracking 信息
        Thread Bind;//资料绑定线程
        Thread ThrWipstation;//
        string RouteCode = "";//流程代码
        //string groupname = "";
        //string nextstation = "";
        //string ShowCarton = "";
        string wo_id = "";//工单编号
        string EndGroup = "";//结束流程
        string qc_Num_Type =string.Empty;//检验类型
        string esn = "";//ESN号
        public string CraftId = "";
        string Show_Msg;//Message信息
        DataTable Dt_EC;//ERRORCODE
        // string Str_EC = "N/A";
        //bool isPass = false;
        DataTable dt_Pal_No;
       // DataTable Dt_Product_Info;
        DataTable dt_Routeinfo;
        DataTable CraftForGroup;
        Dictionary<string, string> Dc_Route = new Dictionary<string, string>();
        string ReworkNo = "";
        public List<string> ClearData = new List<string>();
        List<string> Listesn = new List<string>();
        public string mFlag = "0";
        public bool D_Flag = false;
     //   int Index = 0;
        private void Frm_FQC_Load(object sender, EventArgs e)
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
            D_Flag = true;
            //查询QC编号Sel_QCNO()

            Btn_WipStation.Enabled = false;


            CraftForGroup = new DataTable("mydt");
            CraftForGroup.Columns.Add("nextrouteId", System.Type.GetType("System.String"));
            CraftForGroup.Columns.Add("craftid", System.Type.GetType("System.String"));

            cbreasoncode.Text = "N/A";
            tbrcdesc.Text = "N/A";
            Lab_Craft.Text = "N/A";
            QC_Product_count.Text = "0";
            Cmb_Sel_Imei_Esn.SelectedIndex = 0;
            Cmb_Sel_NumType.SelectedIndex = 0;
            Txt_EC.ReadOnly = true;
            TXT_BackUp.ReadOnly = true;
            checkBoxX1.Enabled = false;
            Cmb_RouteName.Enabled = false;
            try
            {
                XmlDocument doc = new XmlDocument();
                string XmlName = "DllConfig.xml";
                doc.Load(XmlName);
                cbreasoncode.Text = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FQC").SelectSingleNode("LINE")).GetAttribute("Name").ToString();
                tbrcdesc.Text = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FQC").SelectSingleNode("LINEUSER")).GetAttribute("Name").ToString();
                Lab_Craft.Text = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FQC").SelectSingleNode("CRFTNAME")).GetAttribute("Name").ToString();
                CraftId = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FQC").SelectSingleNode("CRFTID")).GetAttribute("Name").ToString();
                r_Date = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FQC").SelectSingleNode("RDATE")).GetAttribute("Name").ToString();
                Lab_NextQC.Text = r_Date + "天";

                // Txt_Error_Code.Text = "线体: " + cbreasoncode.Text + " 途程: " + Lab_Craft.Text;
                //Set_QCID();
            }
            catch
            {

            }

            Dt_Box_Id.Columns.Add("栈板号");
            Dt_Box_Id.Columns.Add("Tray盘号");
            Dt_Box_Id.Columns.Add("产品箱号");
            Dt_Box_Id.Columns.Add("工单");
            Dt_Box_Id.Columns.Add("检验编号");

            Dt_Product_Id.Columns.Add("产品箱号");
            Dt_Product_Id.Columns.Add("唯一条码");
            Dt_Product_Id.Columns.Add("工单");
            Dt_Product_Id.Columns.Add("产品料号");
            Dt_Product_Id.Columns.Add("SN");
            Dt_Product_Id.Columns.Add("MAC");
            Dt_Product_Id.Columns.Add("Tray盘号");
            Dt_Product_Id.Columns.Add("栈板号");
            Dt_Product_Id.Columns.Add("当前站位");
            Dt_Product_Id.Columns.Add("优先途程");
            Dt_Product_Id.Columns.Add("途程代码");

            Dt_QC_Info.Columns.Add("检验编号");
            Dt_QC_Info.Columns.Add("工单");
            Dt_QC_Info.Columns.Add("栈板号");
            Dt_QC_Info.Columns.Add("产品箱号");
            Dt_QC_Info.Columns.Add("ESN");
            Dt_QC_Info.Columns.Add("是否良品");
            Dt_QC_Info.Columns.Add("不良代码");
            Dt_QC_Info.Columns.Add("不良原因");
            Dt_QC_Info.Columns.Add("检验时间");
            Dt_QC_Info.Columns.Add("检验工号");
            Dt_QC_Info.Columns.Add("Tray盘号");
            Dt_EC = FrmBLL.ReleaseData.arrByteToDataTable(refWebtErrorCode.Instance.GetErrorCode());


            Cmb_Sel_NumType.SelectedIndex = 2;
            Cmb_Sel_Imei_Esn.SelectedIndex = 1;
            //buttonX1.Enabled = false;
            Set_QCID();
        }

        private void Btn_set_Data_Click(object sender, EventArgs e)
        {
            mFlag = "0";
            DataTable dt_Line = FrmBLL.ReleaseData.arrByteToDataTable(refWebtLineInfo.Instance.GetAllLineInfo());
            // refWebtRouteInfo.Instance.GetAllRouteInfo()

            SelectData frm = new SelectData(this, dt_Line);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                XmlDocument doc = new XmlDocument();
                string XmlName = "DllConfig.xml";
                doc.Load(XmlName);
                //SetAttribute
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FQC").SelectSingleNode("LINE")).SetAttribute("Name", cbreasoncode.Text);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FQC").SelectSingleNode("LINEUSER")).SetAttribute("Name", tbrcdesc.Text);
                doc.Save(XmlName);
                //  Set_QCID();

            }
        }
        //生成唯一编码

        private void Set_QCID()
        {
            QC_No.Text = cbreasoncode.Text + refWebtFQC.Instance.Sel_QCNO();
        }





        private void Txt_BoxId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {

                    #region 判定栈板号箱号等信息是否刷入过
                    //DataTable Dt_New_Box = new DataTable();
                    //if ((Dg_Box_No as DataTable) != null)
                    DataTable Dt_New_Box = FrmBLL.publicfuntion.getNewTable(Dt_Box_Id, string.Format("{0}='{1}'", Cmb_Sel_NumType.Text, Txt_BoxId.Text));
                    if (Dt_New_Box.Rows.Count > 0)
                    {
                        mFrm.ShowPrgMsg("已经刷过" + Cmb_Sel_NumType.Text, MainParent.MsgType.Error);
                        Txt_Error_Code.Text = "已经刷过" + Cmb_Sel_NumType.Text;
                        return;
                    }
                    #endregion

                    #region 必须先要设置请先设置线体和途程
                    Btn_WipStation.Text = "过站";
                    if (cbreasoncode.Text == "N/A" ||
                        tbrcdesc.Text == "N/A" ||
                        Lab_Craft.Text == "N/A")
                    {
                        mFrm.ShowPrgMsg("请先设置线体和途程", MainParent.MsgType.Error);
                        Txt_Error_Code.Text = "请先设置线体和途程";
                        return;
                    }
                    #endregion

                    #region 不能提交空字段
                    if (string.IsNullOrEmpty(Txt_BoxId.Text))
                    {
                        Show_Msg = string.Format("请输入{0}", Cmb_Sel_NumType.Text);
                        mFrm.ShowPrgMsg(Show_Msg, MainParent.MsgType.Error);
                        Txt_Error_Code.Text = Show_Msg;
                        return;
                    }
                    #endregion

                    #region 选择号码类型
                    string Num_Type = null;
                    if (Cmb_Sel_NumType.Text == "产品箱号")
                    {
                        Num_Type = "cartonnumber";
                        qc_Num_Type = "Carton_id";
                    }
                    if (Cmb_Sel_NumType.Text == "Tray盘号")
                    {
                        Num_Type = "trayno";
                        qc_Num_Type = "Tray_no";
                    }
                    if (Cmb_Sel_NumType.Text == "ESN")
                    {
                        Num_Type = "ESN";
                        qc_Num_Type = "Esn";
                    }
                    if (Cmb_Sel_NumType.Text == "栈板号")
                    {
                        Num_Type = "palletnumber";
                        qc_Num_Type = "Pallet_No";
                    }
                    #endregion

                    //查询产品信息
                    //  BLL.tWipTracking s = new BLL.tWipTracking();
                    dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo(Num_Type, Txt_BoxId.Text));
                    wo_id = dt.Rows[0]["WOID"].ToString();
                    RouteCode = dt.Rows[0]["ROUTGROUPID"].ToString();



                    #region 加载途程代码信息

                    //获取route信息
                    dt_Routeinfo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtRouteInfo.Instance.GetRouteInfoByWoId(RouteCode));
                    //
                    foreach (DataRow item in dt_Routeinfo.Rows)
                    {

                        try
                        {
                            if (item["station_flag"].ToString() == "0")
                            {
                                Cmb_RouteName.Items.Add(item["craftname"].ToString());
                            }
                        }
                        catch
                        {

                        }
                    }
                    #endregion

                    #region 分析工单状态

                    // BLL.tWoInfo wo = new BLL.tWoInfo();
                    DataTable dtwo = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(wo_id, null));

                    if ((dtwo != null) && (dtwo.Rows.Count > 0))
                    {
                        EndGroup = dtwo.Rows[0]["outputgroup"].ToString();
                        int WoStatus = int.Parse(dtwo.Rows[0]["wostate"].ToString());
                        string RES = "";
                        switch (WoStatus)
                        {
                            case 0:
                                RES = "工单待 Relaese";
                                break;
                            case 1:
                                RES = "OK";
                                break;
                            case 2:
                                RES = "OK";
                                break;
                            case 3:
                                RES = "工单已经关闭";
                                break;
                            case 4:
                                RES = "工单HOLD";
                                break;
                            default:
                                RES = "WO STATUS ERROR";
                                break;
                        }
                        if (RES != "OK")
                        {
                            this.mFrm.ShowPrgMsg("ERROR: " + RES, MainParent.MsgType.Error);
                            Txt_Error_Code.Text = "ERROR: " + RES;
                            return;
                        }
                        //if (WoStatus == 3)
                        //{
                        //    this.mFrm.ShowPrgMsg("ERROR: 工单为初始化状态", MainParent.MsgType.Error);
                        //    Txt_Error_Code.Text = "ERROR: 工单为初始化状态";
                        //    return;
                        //}

                        //if (WoStatus == 2)
                        //{
                        //    this.mFrm.ShowPrgMsg("ERROR: 工单为暂停生产状态", MainParent.MsgType.Error);
                        //    Txt_Error_Code.Text = "ERROR: 工单为暂停生产状态";
                        //    return;
                        //}

                        //if (WoStatus == 1)
                        //{
                        //    this.mFrm.ShowPrgMsg("ERROR: 工单已经关闭", MainParent.MsgType.Error);
                        //    Txt_Error_Code.Text = "ERROR: 工单已经关闭";
                        //    return;
                        //}
                    }
                    else
                    {

                        this.mFrm.ShowPrgMsg("工单" + wo_id + "信息不存在", MainParent.MsgType.Error);
                        Txt_Error_Code.Text = "工单" + wo_id + "信息不存在";
                        return;
                    }
                    #endregion


                    #region 站位检查
                    DataTable dt_Route = dt.DefaultView.ToTable(true, "LOCSTATION", "WIPSTATION");
                    foreach (DataRow item in dt_Route.Rows)
                    {
                        DataTable dt_NewRoute = FrmBLL.publicfuntion.getNewTable(dt, string.Format("LOCSTATION='{0}' and WIPSTATION='{1}'", item["LOCSTATION"].ToString(), item["WIPSTATION"].ToString()));
                        if (!CheckRoute(dt_NewRoute.Rows[0]["ESN"].ToString(), CraftId, RouteCode, dt_NewRoute.Rows[0]["LOCSTATION"].ToString(), dt_NewRoute.Rows[0]["WIPSTATION"].ToString(), wo_id, EndGroup))
                        {
                            Txt_BoxId.Text = "";
                            return;
                        }
                    }


                    #endregion


                    #region 解析箱号等信息
                    if (dt.Rows.Count > 0)
                    {
                        //筛选唯一信息

                        //dt_Pal_No = dt.DefaultView.ToTable(true, "栈板号", "Tray盘号", "产品箱号","工单");
                        dt_Pal_No = dt.DefaultView.ToTable(true, "PALLETNUMBER", "TRAYNO", "CARTONNUMBER", "WOID");
                        if (dt_Pal_No.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt_Pal_No.Rows)
                            {
                                Dt_Box_Id.Rows.Add(dr[0], dr[1], dr[2], dr[3], QC_No.Text);
                            }
                        }
                        else
                        {

                            this.mFrm.ShowPrgMsg("ERROR: 没有资料", MainParent.MsgType.Error);
                            Txt_Error_Code.Text = "ERROR: 没有资料";
                            return;
                        }
                        Dg_Box_No.DataSource = Dt_Box_Id;
                    #endregion

                        Bind = new Thread(new ThreadStart(Bind_Info));
                        Bind.Start();

                        //DataSet dt_qc_Info = FrmBLL.ReleaseData.arrByteToDataSet(refWebtFQC.Instance.Sel_FQC_info_NumType(qc_Num_Type, Txt_BoxId.Text));
                        //if (dt_qc_Info.Tables[0].Rows.Count > 0)
                        //{
                        //    foreach (DataRow item in dt_qc_Info.Tables[0].Rows)
                        //    {
                        //        Dt_QC_Info.Rows.Add(item["QC_NO"],
                        //            item["WO_ID"],
                        //            item["PALLET_NO"],
                        //            item["CARTON_ID"],
                        //            item["ESN"],
                        //          Convert.ToBoolean(int.Parse(item["IS_PASS"].ToString())) ? "良品" : "不良品",
                        //            item["ERROR_CODE"],
                        //            item["REMARK"],
                        //            item["IN_STATION_TIME"],
                        //            item["USERID"],
                        //            item["TRAY_NO"]);
                        //    }
                        //    QC_Product_count.Text = dt_qc_Info.Tables[0].Rows.Count.ToString();
                        //    product_count = dt_qc_Info.Tables[0].Rows.Count;
                        //    Dg_QC_Info.DataSource = Dt_QC_Info;
                        //    QC_No.Text = dt_qc_Info.Tables[0].Rows[0][0].ToString();
                        //    DataTable dt_ErrorInfo = FrmBLL.publicfuntion.getNewTable(Dt_QC_Info, string.Format("是否良品='不良品'"));
                        //    if (dt_ErrorInfo.Rows.Count > 0)
                        //    {
                        //        reWork();
                        //    }
                        // txt_Product_Id.Focus();

                        // }


                    }
                    else
                    {
                        Show_Msg = string.Format("没有该{0}的数据", Cmb_Sel_NumType.Text);
                        mFrm.ShowPrgMsg(Show_Msg, MainParent.MsgType.Error);
                    }
                    Txt_BoxId.Text = "";
                    Cmb_Sel_NumType.Enabled = false;
                }

                catch (Exception ex)
                {
                    mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                }
            }
        }

        public void Bind_Info()
        {
            Dg_Product_Info.Invoke(new EventHandler(delegate
           {
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   //Dt_Product_Id.Rows.Add(dt.Rows[i]["产品箱号"],
                   //                     dt.Rows[i]["唯一条码"], dt.Rows[i]["工单"], dt.Rows[i]["产品料号"], dt.Rows[i]["SN"], dt.Rows[i]["MAC"], dt.Rows[i]["Tray盘号"], dt.Rows[i]["客户栈板号"], dt.Rows[i]["当前站位"], dt.Rows[i]["优先途程"], dt.Rows[i]["途程代码"]);
                   Dt_Product_Id.Rows.Add(dt.Rows[i]["CARTONNUMBER"],
                       dt.Rows[i]["ESN"], dt.Rows[i]["WOID"], dt.Rows[i]["PARTNUMBER"], dt.Rows[i]["SN"], dt.Rows[i]["MAC"], dt.Rows[i]["TRAYNO"], dt.Rows[i]["MPALLETNUMBER"], dt.Rows[i]["LOCSTATION"], dt.Rows[i]["NEXTSTATION"], dt.Rows[i]["ROUTGROUPID"]);
               }


               Dg_Product_Info.DataSource = Dt_Product_Id;
           }));
        }
        int product_count;
        DataTable dtNew = new DataTable();
        private void txt_Product_Id_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (Cmb_Sel_Imei_Esn.Text != "ESN")
                {
                    DataTable dtKeyparts = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyParts("SNVAL", txt_Product_Id.Text.Trim()));
                    esn = "ERROR";
                    if (dtKeyparts.Rows.Count > 0)
                        esn = dtKeyparts.Rows[0]["ESN"].ToString();
                   // esn = refWebtWipKeyPart.Instance.GetEsnForKeyParts(txt_Product_Id.Text);
                }
                else
                    esn = txt_Product_Id.Text;

                if (esn == "ERROR")
                {
                    txt_Product_Id.Text = "";
                    Show_Msg = string.Format("你扫描的{0}号不存在", Cmb_Sel_Imei_Esn.Text);
                    this.mFrm.ShowPrgMsg(Show_Msg, MainParent.MsgType.Error);
                    Txt_Error_Code.Text = Show_Msg;
                    return;
                }
                else
                {

                     dtNew = FrmBLL.publicfuntion.getNewTable(this.Dt_Product_Id, string.Format("唯一条码='{0}'", esn));



                    if (dtNew.Rows.Count <= 0)
                    {

                        txt_Product_Id.Text = "";
                        Show_Msg = string.Format("你扫描的{0}号不在你已扫描的{1}内", Cmb_Sel_Imei_Esn.Text, Cmb_Sel_NumType.Text);
                        this.mFrm.ShowPrgMsg(Show_Msg, MainParent.MsgType.Error);
                        Txt_Error_Code.Text = Show_Msg;
                        return;
                    }


                    //groupname = dtNew.Rows[0]["当前站位"].ToString();
                    //nextstation = dtNew.Rows[0]["优先途程"].ToString();
                    //ShowCarton = dtNew.Rows[0]["产品箱号"].ToString();






                    DataTable IsTest = FrmBLL.publicfuntion.getNewTable(this.Dt_QC_Info, string.Format("ESN='{0}'", esn));
                    if (IsTest.Rows.Count > 0)
                    {
                        txt_Product_Id.Text = "";
                        Show_Msg = string.Format("你扫描的{0}号已经检验过", Cmb_Sel_Imei_Esn.Text);
                        this.mFrm.ShowPrgMsg(Show_Msg, MainParent.MsgType.Error);
                        Txt_Error_Code.Text = Show_Msg;
                        return;
                    }

                  //  Dt_Product_Info = FrmBLL.publicfuntion.getNewTable(this.Dt_Product_Info, string.Format("唯一条码='{0}'", esn));

                    Txt_EC.ReadOnly = false;
                    Txt_EC.Focus();
                    txt_Product_Id.ReadOnly = true;


                }




            }
        }

        private void Btn_SelCraftInfo_Click(object sender, EventArgs e)
        {
            mFlag = "1";
            DataTable dt_Craft = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtCraftInfo.Instance.GetAllCraftInfo());

            SelectData frm = new SelectData(this, dt_Craft);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                XmlDocument doc = new XmlDocument();
                string XmlName = "DllConfig.xml";
                doc.Load(XmlName);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FQC").SelectSingleNode("CRFTNAME")).SetAttribute("Name", Lab_Craft.Text);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FQC").SelectSingleNode("CRFTID")).SetAttribute("Name", CraftId);
                doc.Save(XmlName);

            }
        }
        Thread thr_rework;//重工线程
        private void Btn_WipStation_Click(object sender, EventArgs e)
        {
            Btn_WipStation.Enabled = false;
            //Set_QCID();
            try
            {
                if (!checkBoxX1.Checked)
                {
                   // Wipstation();
                    ThrWipstation = new Thread(new ThreadStart(Wipstation));
                    ThrWipstation.Start();

                }
                else
                {


                    Frm_Sel_Info sel = new Frm_Sel_Info(this);
                    if (sel.ShowDialog() == DialogResult.OK)
                    {
                       //Wip_Rework();
                        thr_rework = new Thread(new ThreadStart(Wip_Rework));
                        thr_rework.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Txt_Error_Code.Text = ex.Message;
            }

        }
        //反工
        public void Wip_Rework()
        {
            string WebMsg = "";
            D_Flag = true;
            int cou = Dt_Product_Id.Rows.Count;
            Pg_Wip_Station.Invoke(new EventHandler(delegate
            {
                ReworkNo = "F" + QC_No.Text;
                string WipGroup = Cmb_RouteName.Text;
                string GroupName = WipGroup;

                Pg_Wip_Station.Maximum = cou;
                foreach (DataRow item in Dt_Product_Id.Rows)
                {
                    cou--;
                    Pg_Wip_Station.Value = cou - 1;
                    // Listesn.Add(item["唯一条码"].ToString());

                    //  WebMsg = refWebtReworkDetailInfo.Instance.UpdateDataForReworkSigle(ClearData.ToArray(), item["唯一条码"].ToString(), GroupName, WipGroup, ReworkNo);

                    Dictionary<string, object> mst = new Dictionary<string, object>();
                    mst.Add("ESN", item["唯一条码"].ToString());
                    mst.Add("LOCSTATION", GroupName);
                    mst.Add("WIPSTATION", WipGroup);
                    mst.Add("NEXTSTATION", WipGroup);
                    mst.Add("REWORKNO", ReworkNo);
                    if (ClearData.Contains("SN"))
                    {
                        mst.Add("SN", "NA");
                    }
                    if (ClearData.Contains("MAC"))
                    {
                        mst.Add("MAC", "NA");
                    }
                    if (ClearData.Contains("IMEI"))
                    {
                        mst.Add("IMEI", "NA");
                    }
                   // WebMsg = refWebtReworkDetailInfo.Instance.Rework_SN(FrmBLL.ReleaseData.DictionaryToJson(mst), ClearData.ToArray());

                }
                if (WebMsg == "OK")
                {  
                    #region 记录检验报表
                    DataTable DT_BoxInfo = Dt_Box_Id.DefaultView.ToTable(true, Cmb_Sel_NumType.Text);

                    foreach (DataRow item in DT_BoxInfo.Rows)
                    {
                        DataTable Dt_ViewProduct = FrmBLL.publicfuntion.getNewTable(Dt_Product_Id, string.Format("{0}='{1}'", Cmb_Sel_NumType.Text, item[Cmb_Sel_NumType.Text].ToString()));
                        DataTable Dt_ViewQC_Info = FrmBLL.publicfuntion.getNewTable(Dt_QC_Info, string.Format("{0}='{1}'", Cmb_Sel_NumType.Text, item[Cmb_Sel_NumType.Text].ToString()));

                        DataTable dt_ErrorInfo = FrmBLL.publicfuntion.getNewTable(Dt_QC_Info, string.Format("是否良品='不良品' and {0}='{1}'", Cmb_Sel_NumType.Text, item[Cmb_Sel_NumType.Text].ToString()));
                        refWebtFQC.Instance.inser_report(QC_No.Text, Cmb_Sel_NumType.Text, Dt_ViewProduct.Rows.Count, Dt_ViewQC_Info.Rows.Count, dt_ErrorInfo.Rows.Count, mFrm.gUserInfo.userId, DateTime.Now, int.Parse(r_Date), item[0].ToString(), 1);
                    }                    
                    #endregion
                    // index++;
                    clear();
                }
                else
                {
                    Txt_Error_Code.Text = WebMsg;
                }

                Txt_Error_Code.Text = "执行完毕";
                D_Flag = false;
            }));


        }


        //过站
        public void Wipstation()
        {
            D_Flag = true;
            this.Invoke(new EventHandler(delegate
            {
                string EC = "NA";
                if (Dt_QC_Info.Rows.Count <= 0)
                {
                  //  Txt_Error_Code.Invoke(new EventHandler(delegate
                  //  {
                        Show_Msg = string.Format("请扫描{0}号并检验一定量的产品", Cmb_Sel_NumType.Text);
                        this.mFrm.ShowPrgMsg(Show_Msg, MainParent.MsgType.Error);
                        Txt_Error_Code.Text = Show_Msg;
                  //  }));
                    return;
                }
                #region 将QC编号记录到数据库
                string QAType = ""; ;
               
                switch (Cmb_Sel_NumType.Text)
                {
                    case "Tray盘号":
                        QAType = "tray盘号";
                        break;
                    case "产品箱号":
                        QAType = "产品箱号";
                        break;
                    case "栈板号":
                        QAType = "栈板号";
                        break;
                    default:
                        break;
                }
             

                DataTable NewUpQA = Dt_Product_Id.DefaultView.ToTable(true, QAType);

                foreach (DataRow item in NewUpQA.Rows)
                {
                    string isOK = refWebtWipTracking.Instance.Update_QA(QC_No.Text, Cmb_Sel_NumType.Text, item[QAType].ToString());

                    if (isOK != "OK")
                    {
                        return;
                    }
                }
          
                #endregion
            #region 执行过站
            int cou = Dt_Product_Id.Rows.Count;
            //Pg_Wip_Station.Invoke(new EventHandler(delegate
            //{
                Pg_Wip_Station.Maximum = cou;
                foreach (DataRow item in Dt_Product_Id.Rows)
                {
                    cou--;
                    Pg_Wip_Station.Value = cou - 1;
                    string RES = refWebProPublicStoredproc.Instance.SP_TEST_MAIN_ONLY(item["唯一条码"].ToString(), CraftId, mFrm.gUserInfo.userId + "-" + mFrm.gUserInfo.pwd, EC, cbreasoncode.Text);
                    if (RES != "OK")
                    {
                        // this.mFrm.ShowPrgMsg("ERROR: 执行TEST_CTN_PALT_TRAY错误, " + RES + "ERROR: TEST_CTN_PALT_TRAY Execept, " + RES, MainParent.MsgType.Error);
                        Txt_Error_Code.Text = "ERROR: 执行TEST_CTN_PALT_TRAY错误, " + RES + "ERROR: TEST_CTN_PALT_TRAY Execept, " + RES;

                        return;
                    }
                }
          //  }));
            #endregion
            #region 记录检验报表
            Txt_Error_Code.Invoke(new EventHandler(delegate
            {
                try
                {
                    #region 记录检验报表
                    DataTable DT_BoxInfo = Dt_Box_Id.DefaultView.ToTable(true, Cmb_Sel_NumType.Text);

                    foreach (DataRow item in DT_BoxInfo.Rows)
                    {
                        DataTable Dt_ViewProduct = FrmBLL.publicfuntion.getNewTable(Dt_Product_Id, string.Format("{0}='{1}'", Cmb_Sel_NumType.Text, item[Cmb_Sel_NumType.Text].ToString()));
                        DataTable Dt_ViewQC_Info = FrmBLL.publicfuntion.getNewTable(Dt_QC_Info, string.Format("{0}='{1}'", Cmb_Sel_NumType.Text, item[Cmb_Sel_NumType.Text].ToString()));

                        DataTable dt_ErrorInfo = FrmBLL.publicfuntion.getNewTable(Dt_QC_Info, string.Format("是否良品='不良品' and {0}='{1}'", Cmb_Sel_NumType.Text, item[Cmb_Sel_NumType.Text].ToString()));
                        refWebtFQC.Instance.inser_report(QC_No.Text, Cmb_Sel_NumType.Text, Dt_ViewProduct.Rows.Count, Dt_ViewQC_Info.Rows.Count, dt_ErrorInfo.Rows.Count, mFrm.gUserInfo.userId, DateTime.Now, int.Parse(r_Date), item[0].ToString(), 1);
                    }
                    #endregion
                }
                catch (Exception ex)
                {

                    Txt_Error_Code.Text = ex.Message;

                }
                clear();

                Txt_Error_Code.Text = "执行完毕";
                D_Flag = false;
            }));
            #endregion

            }));
        }

        //比对途程
        private bool CheckRoute(string ESN, string Route, string RouteCode, string LocGroup, string nextstation, string wo, string outputgroup)
        {
            string Err = refWebProPublicStoredproc.Instance.CHECK_ROUTE(ESN, Route);
            //string Err = refWebProPublicStoredproc.Instance.CHECK_ROUTE(new WebServices.tPublicStoredproc.tPublicParamForSP()
            //{
            //    DATA = ESN,
            //    MYGROUP = Route,
            //    ROUTECODE = RouteCode,
            //    ERRORFLAG = "0",
            //    LOCSTATION = LocGroup,
            //    NEXTSTATION = nextstation,
            //    woId = wo,
            //    ENDGROUP = outputgroup

            //});
            if (Err == "OK")
            {
                mFrm.ShowPrgMsg("CHECK ROUTE: " + ESN + "-->" + Err, MainParent.MsgType.Incoming);
                Txt_Error_Code.Text = "CHECK ROUTE: " + ESN + "-->" + Err;

                return true;
            }
            else
            {
                mFrm.ShowPrgMsg("CHECK ROUTE: " + ESN + "-->" + Txt_BoxId.Text + "-->" + Err, MainParent.MsgType.Error);
                Txt_Error_Code.Text = "CHECK ROUTE: " + ESN + "-->" + Txt_BoxId.Text + "-->" + Err;
                return false;
            }

        }

        private void Txt_EC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txt_Product_Id.Text))
                    return;
                if (string.IsNullOrEmpty(Txt_EC.Text))
                {

                    Dt_QC_Info.Rows.Add(QC_No.Text, dtNew.Rows[0]["工单"].ToString(), dtNew.Rows[0]["栈板号"].ToString(), dtNew.Rows[0]["产品箱号"].ToString(),
                                   dtNew.Rows[0]["唯一条码"].ToString(), "良品", "NA", "NA", DateTime.Now.ToString("yyyy-MM-dd"), mFrm.gUserInfo.userId, dtNew.Rows[0]["TRAY盘号"].ToString());
                    Dg_QC_Info.DataSource = Dt_QC_Info;

                    Inser_qc_Info(true, "NA", dtNew.Rows[0]["工单"].ToString(), dtNew.Rows[0]["栈板号"].ToString(), dtNew.Rows[0]["产品箱号"].ToString()
                        , dtNew.Rows[0]["唯一条码"].ToString(), dtNew.Rows[0]["TRAY盘号"].ToString(), "NA");
                    txt_Product_Id.ReadOnly = false;
                    txt_Product_Id.Focus();
                    txt_Product_Id.Text = "";
                    Txt_EC.ReadOnly = true;
                    TXT_BackUp.ReadOnly = true;
                    Txt_EC.Text = "";
                    TXT_BackUp.Text = "";
                    Btn_WipStation.Enabled = true;
                }
                else
                {
                    int ec_count = FrmBLL.publicfuntion.getNewTable(Dt_EC, string.Format("ErrorCode='{0}'", Txt_EC.Text)).Rows.Count;
                    if (ec_count > 0)
                    {
                        TXT_BackUp.ReadOnly = false;
                        TXT_BackUp.Focus();
                        Txt_EC.ReadOnly = true;
                    }
                }
            }
        }

        protected void Inser_qc_Info(bool isPass, string Str_EC, string Wo_id, string Pal_No, string Box_No, string Esn, string trayNo, string qc_error_info)
        {

            try
            {
                refWebtFQC.Instance.inser_FQCInfo(QC_No.Text, Wo_id, Pal_No, Box_No,
                   Esn, isPass, Str_EC, mFrm.gUserInfo.userId, trayNo, qc_error_info);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //Str_EC = "N/A";
                //isPass = false;
                //Txt_Error_Code.Text = "";

                product_count++;
                QC_Product_count.Text = product_count.ToString();

            }

        }

        private void TXT_BackUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (MessageBox.Show(string.Format("产品{0}：{1}\r\n产品不良代码：{2}\r\n产品不良原因：{3}",
                    Cmb_Sel_Imei_Esn.Text,
                    txt_Product_Id.Text,
                    Txt_EC.Text,
                    TXT_BackUp.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {

                    

                    #region 记录不良



                    Dt_QC_Info.Rows.Add(QC_No.Text, dtNew.Rows[0]["工单"].ToString(), dtNew.Rows[0]["栈板号"].ToString(), dtNew.Rows[0]["产品箱号"].ToString(),
                                     dtNew.Rows[0]["唯一条码"].ToString(), "不良品", Txt_EC.Text, TXT_BackUp.Text, DateTime.Now.ToString("yyyy-MM-dd"), mFrm.gUserInfo.userId, dtNew.Rows[0]["TRAY盘号"].ToString());
                    Dg_QC_Info.DataSource = Dt_QC_Info;

                    Inser_qc_Info(false, Txt_EC.Text, dtNew.Rows[0]["工单"].ToString(), dtNew.Rows[0]["栈板号"].ToString(), dtNew.Rows[0]["产品箱号"].ToString()
                        , dtNew.Rows[0]["唯一条码"].ToString(), dtNew.Rows[0]["TRAY盘号"].ToString(), TXT_BackUp.Text);
                    #endregion



                    txt_Product_Id.ReadOnly = false;
                    txt_Product_Id.Focus();
                    txt_Product_Id.Text = "";

                    Txt_EC.ReadOnly = true;
                    TXT_BackUp.ReadOnly = true;
                    Txt_EC.Text = "";
                    TXT_BackUp.Text = "";
                    reWork();
                }
            }
        }
        private void reWork()
        {
            checkBoxX1.Enabled = true;
            checkBoxX1.Checked = true;
            Cmb_RouteName.Enabled = true;
            Btn_WipStation.Text = "打回";
            Btn_WipStation.Enabled = true;
        }

        /// <summary>
        /// 清楚所有数据
        /// </summary>
        public void clear()
        {
            Dt_Box_Id.Clear();
            Dt_Product_Id.Clear();
            Dt_QC_Info.Clear();
            Cmb_Sel_NumType.Enabled = true;
            checkBoxX1.Checked = false;
            Cmb_RouteName.Text = "";

            Txt_BoxId.Text = "";
            Txt_BoxId.ReadOnly = false;
            Txt_BoxId.Focus();
            Txt_EC.Text = "";
            Txt_EC.ReadOnly = true;
            txt_Product_Id.Text = "";
            txt_Product_Id.ReadOnly = false;
            TXT_BackUp.Text = "";
            TXT_BackUp.ReadOnly = true;
            checkBoxX1.Enabled = false;
            Cmb_RouteName.Enabled = false;
            //Cmb_RouteName.Text = "";
            Btn_WipStation.Text = "过站";
            product_count = 0;

            dt.Clear();

            Dg_Box_No.DataSource = null;
            Dg_Product_Info.DataSource = Dt_Product_Id;
            Dg_QC_Info.DataSource = Dt_QC_Info;
            //LabWo.Text = "NA";
            QC_Product_count.Text = "0";
            Set_QCID();
        }


        private string r_Date;
        public string R_Date
        {
            get { return r_Date; }
            set { r_Date = value; }
        }
        private void Btn_SetDay_Click(object sender, EventArgs e)
        {
            //设定下次检验时间
            Frm_SetDay setday = new Frm_SetDay(this);
            if (setday.ShowDialog() == DialogResult.OK)
            {
                Lab_NextQC.Text = r_Date + "天";


                XmlDocument doc = new XmlDocument();
                string XmlName = "DllConfig.xml";
                doc.Load(XmlName);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("FQC").SelectSingleNode("RDATE")).SetAttribute("Name", r_Date);
                doc.Save(XmlName);
            }
        }

        private void Frm_FQC_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}
