using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using DevComponents.DotNetBar;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;
using RefWebService_BLL;
using System.Xml;


namespace SFIS_V2
{
    public partial class FrmPallet : Office2007Form
    {
        public FrmPallet(MainParent sInfo)
        {
            InitializeComponent();         
            sMain = sInfo;
        }
        MainParent sMain;
        bool errflag = false;
        LabelManager2.ApplicationClass lbl; //= new ApplicationClass();
       public string LabFilepatch = System.IO.Directory.GetCurrentDirectory() + "\\LabelFile\\PALLET.lab";

       private readonly string IniFilePatch = "C:\\SFIS\\SFIS.ini";
        /// <summary>
        /// 生产线别
        /// </summary>
       string ProductLine = string.Empty;

       public string MyLine = "";
        public string LineCode = "";
        /// <summary>
        /// 途程
        /// </summary>
        public string MYGROUP = "";

        public string MyStation = string.Empty;
        public string MySection = string.Empty;

        public string facid = "";
        public bool Line = false;
        public bool Craft = false;
        string Model = "";     

        public void SetStation()
        {
            LabConfig.Text = "线体: " + MyLine + "  --  途程: " + MYGROUP;
            FrmBLL.ReadIniFile.IniWriteValue("PACK_PALLET", "LINE", MyLine, IniFilePatch);
            FrmBLL.ReadIniFile.IniWriteValue("PACK_PALLET", "CRFTNAME", MYGROUP, IniFilePatch);  
        }

        private void FrmPallet_Load(object sender, EventArgs e)
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
            
            labemployee.Text = sMain.gUserInfo.username;


            #region 调节控件位置                        

            panelEx3.Width = PalConfig.Width / 2;         
            panelEx6.Left = panelEx5.Width / 3;
            panelEx7.Left = panelEx5.Width / 3;
            panelEx8.Left = panelEx5.Width / 3;
            panelEx9.Left = panelEx5.Width / 3;
            panelEx11.Left = panelEx5.Width / 3;
            label4.Left = panelEx5.Width / 3-100;
            label5.Left = panelEx5.Width / 3 - 100;
            label8.Left = panelEx5.Width / 3 - 100;
            label6.Left = label4.Left - label4.Width + 22;
            label7.Left = label4.Left - label4.Width + 22;

            //panelEx10.Location = new Point(label7.Location.X,panelEx9.Location.Y + 50);
            //panelEx10.Size = new Size(PalConfig.Width / 2 - 100, panelEx5.Height - panelEx9.Location.Y - 60);

            #endregion

            //XmlDocument doc = new XmlDocument();
            //string XmlName = "DllConfig.xml";
            //doc.Load(XmlName);
            //LineName=((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PALLET").SelectSingleNode("LINE")).GetAttribute("Name").ToString();
            //LineCode = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PALLET").SelectSingleNode("LINECODE")).GetAttribute("Name").ToString();
            //CraftName = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PALLET").SelectSingleNode("CRFTNAME")).GetAttribute("Name").ToString();
            //CraftId = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PALLET").SelectSingleNode("CRFTID")).GetAttribute("Name").ToString();

           MyLine =FrmBLL.ReadIniFile.IniReadValue("PACK_PALLET", "LINE", IniFilePatch);
           MYGROUP = FrmBLL.ReadIniFile.IniReadValue("PACK_PALLET", "CRFTNAME", IniFilePatch);
         //  LineCode = FrmBLL.ReadIniFile.IniReadValue("PACK_PALLET", "LINECODE", IniFilePatch);      

            facid = sMain.gUserInfo.facId;

            if (string.IsNullOrEmpty(facid))
            {
                MessageBox.Show("厂区代码为空");            
            }
            SetStation();
            labTotalCount.Text = "0";
            labcount.Text = "0";
            labver.Text = "";
            labpallet.Text = "";
            LabCustPallet.Text = "";
            lbl = new LabelManager2.ApplicationClass(); 
        }

        private void FrmPallet_FormClosing(object sender, FormClosingEventArgs e)
        {           
      
            
        }
        #region 向客户端发送数据
        private void SendMsgToClient(string SendMsg, string SendMsg2)
        {
            labfrmMsg.Text = SendMsg;       
        }
        #endregion
        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbInput.Text)) && (e.KeyCode == Keys.Enter))
            {
                if (tbInput.Text.Trim().ToUpper() == "NA") //系统默认参数,直接退出
                {
                    tbInput.SelectAll();
                    return;
                }
                try
                {
                    if (errflag)
                    {
                        SendMsgToClient("错误未解除", "Error");
                        return;
                    }
                    if (tbInput.Text.Trim().ToUpper() == "UNDO")
                    {
                        listctn.Items.Clear();
                        labcount.Text = "0";
                        labTotalCount.Text = "0";
                        labPartnumber.Text = "";
                        labwoId.Text = "";
                        labver.Text = "";
                        tbInput.SelectAll();
                        SendMsgToClient("UNDO OK.", "UNDO OK.");
                    }
                    else
                    {
                        if (Convert.ToInt32(labcount.Text) >= Convert.ToInt32(labTotalCount.Text))
                        {
                            listctn.Items.Clear();
                            labcount.Text = "0";
                            labTotalCount.Text = "0";
                            labPartnumber.Text = "";
                            labwoId.Text = "";
                        }

                        tbInput.SelectAll();
                        string InputCarton = "";

                        InputCarton = GetCartonNoInfo(tbInput.Text.Trim());//根据选择项,找出carton号码
                        if (InputCarton == "ERROR")
                        {
                            SendMsgToClient("ERROR: 获取到系统默认参数","ERROR: System Default Data");
                            return;
                        }
                        DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo("cartonnumber", InputCarton));

                        if (dt.Rows.Count != 0)
                        {
                            DataTable dterrflag = FrmBLL.publicfuntion.getNewTable(dt, "ERRFLAG <> 0");
                            if (dterrflag.Rows.Count != 0)
                            {
                                SendMsgToClient("ERROR: " + dterrflag.Rows[0][0].ToString() + " 为不良品,请做维修", "ERROR: " + dterrflag.Rows[0][0].ToString() + " In Repair");
                                return;
                            }

                            DataTable dtScrapflag = FrmBLL.publicfuntion.getNewTable(dt, "SCRAPFLAG <> 0 ");
                            if (dtScrapflag.Rows.Count != 0)
                            {
                                SendMsgToClient("ERROR: " + dtScrapflag.Rows[0][0].ToString() + " 为报废", "ERROR: " + dtScrapflag.Rows[0][0].ToString() + " Has Scrap");
                                return;
                            }

                            labPartnumber.Text = dt.Rows[0]["PARTNUMBER"].ToString();
                            if (string.IsNullOrEmpty(labwoId.Text))
                            {
                                labwoId.Text = dt.Rows[0]["WOID"].ToString();
                            }
                            else
                            {   
                                //  检查刷入产品工单信息是否一致
                                if (dt.Rows[0]["WOID"].ToString() != labwoId.Text)
                                {
                                    SendMsgToClient("ERROR: 工单不一致", "ERROR: WO Different");
                                    return;
                                }
                            }

                            #region 判定Carton是否关闭
                            int flag = 0;
                            if (!refWebtPalletInfo.Instance.CheckCartonOrPalletClosed(InputCarton, 1, out flag))
                            {
                                if (flag == 1)
                                    SendMsgToClient("ERROR: 未找到Carton包装资料.", "ERROR: NO Carton Data");
                                else
                                    if (flag == 2)
                                        SendMsgToClient("ERROR: Carton找到多笔资料.", "ERROR: Carton No  Multiple Data");
                                    else
                                        if (flag == 3)
                                            SendMsgToClient("ERROR: Carton未关闭.", "ERROR: Carton Not Closed");
                                return;
                            }
                            #endregion     

                            #region 检查工单信息和途程
                            string sEndGroup = string.Empty;
                            if (!CheckWOInfo(labwoId.Text.Trim(),labPartnumber.Text.Trim(),out sEndGroup))
                            {
                                return;
                            }
                            if (!CHECK_PRODUCT_LINE())
                                return;
                            if (!CheckRoute(dt, sEndGroup))
                            {
                                return;
                            }
                            #endregion

                            if (Convert.ToInt32(labcount.Text) < 1)
                            {
                                #region 第一个Carton获取包装资料                               
                                if (!GetPackParameters()) //第一箱产品获取包装容量
                                    return;
                               // 检查是否有未关闭的栈板

                                Dictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add("LINE", MyLine);
                                dic.Add("WOID", labwoId.Text);
                                dic.Add("PARTNUMBER", labPartnumber.Text);
                                dic.Add("CLOSEFLAG", 0);
                                dic.Add("PACKTYPE", 2);
                                DataTable dtpallet = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPalletInfo.Instance.GetPalletInfo(FrmBLL.ReleaseData.DictionaryToJson(dic)));
                              
                                if (dtpallet.Rows.Count == 0)
                                {
                                    //产生新的栈板号和客户栈板号码
                                    PalletNo = refWebProPublicStoredproc.Instance.GetPalletNumber(sMain.gUserInfo.facId, MyLine);
                                    CustPalletNo= GetCustPalletInfo(labPartnumber.Text);
                                    CustPalletNo = CustPalletNo == "NA" ? PalletNo : CustPalletNo;
                                    if (CustPalletNo == "ERROR")
                                    {
                                        return;
                                    }
                                    if (PalletNo == "ERROR")
                                    {
                                        SendMsgToClient("ERROR: 生成栈板号码错误,请联系SFC人员", "ERROR: Creat Pallet Error,Call Sfc Team");
                                        return;
                                    }

                                    dic = new Dictionary<string, object>();
                                    dic.Add("WOID", labwoId.Text);
                                    dic.Add("PARTNUMBER", labPartnumber.Text);
                                    dic.Add("LINE", MyLine);
                                    dic.Add("PALLETNUMBER", PalletNo);
                                    dic.Add("PACKTYPE", 2);
                                    dic.Add("CLOSEFLAG", 0);
                                    dic.Add("TOTAL", Convert.ToInt32(labTotalCount.Text));
                                    dic.Add("COMPUTER", FrmBLL.publicfuntion.HostName);
                                    refWebtPalletInfo.Instance.InsertPalletInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));                                  
                                    labpallet.Text = PalletNo;
                                    LabCustPallet.Text = CustPalletNo;
                                }
                                else
                                {
                                   DataTable PaltList=  FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo("palletnumber", dtpallet.Rows[0]["palletnumber"].ToString()));
                                   DataTable CtnList = PaltList.DefaultView.ToTable(true, new string[] { "woid", "cartonnumber", "palletnumber", "mcartonnumber", "mpalletnumber" });
                                    if (CtnList.Rows.Count > 0)
                                    {
                                        if (CtnList.Rows[0]["WOID"].ToString() != labwoId.Text.Trim())
                                        {
                                            SendMsgToClient("ERROR: 工单不一致", "ERROR: WO Different");
                                            return;
                                        }
                                        foreach (DataRow dr in CtnList.Rows)
                                        {
                                            DataTable dtqty = FrmBLL.publicfuntion.getNewTable(PaltList, string.Format("CARTONNUMBER='{0}'", dr["CARTONNUMBER"].ToString()));
                                            listctn.Items.Add(dr["CARTONNUMBER"].ToString()+"-"+dr["MCARTONNUMBER"].ToString()+"-"+dtqty.Rows.Count.ToString());
                                        }
                                        labcount.Text = CtnList.Rows.Count.ToString();
                                        CustPalletNo = CtnList.Rows[0]["mpalletnumber"].ToString();
                                        PalletNo = CtnList.Rows[0]["palletnumber"].ToString();
                                    }
                                    else
                                    {
                                        SendMsgToClient(string.Format("ERROR: 查找栈板信息错误[{0}]",PalletNo), string.Format("ERROR: Select PalletNo Error [{0}]",PalletNo));
                                        return;
                                    }
                                }

                                labpallet.Text = PalletNo;
                                LabCustPallet.Text = CustPalletNo=="NA"?PalletNo:CustPalletNo;

                                #endregion
                            }
                            else
                            {
                                #region  后续做资料check                               
                                //else
                                //    if (dtver.Rows[0]["pver"].ToString() != labver.Text )
                                //    {
                                //        SendMsgToClient("ERROR: 版本不同");
                                //        return;
                                //    }
                              

                                #endregion

                            }           
                            

                           #region  产品过站添加栈板号码,第一箱产品增加栈板标记
                            string Pallet = labpallet.Text;
                            string MPallet =LabCustPallet.Text;
                            //------------------------2013-06-26
                            List<string> sLsEsn = new List<string>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                sLsEsn.Add(dr[0].ToString());
                            }

                            string RES = refWebProPublicStoredproc.Instance.SP_TEST_CTN_PALT_TRAY_NEW(sLsEsn.ToArray(), MYGROUP, sMain.gUserInfo.userId + "-" + sMain.gUserInfo.pwd, "NA", MyLine, Pallet, MPallet, 2);
                            if (RES != "OK")
                            {
                                SendMsgToClient("ERROR: 执行TEST_CTN_PALT_TRAY错误, " + RES, "ERROR: TEST_CTN_PALT_TRAY Execept, " + RES);
                                return;
                            }              

                            labcount.Text = (Convert.ToInt32(labcount.Text) + 1).ToString();
                            btn_ClosePallet.Enabled = true;                        

                            SendMsgToClient(InputCarton + " OK!", InputCarton + " OK!-->" + labcount.Text + "/" + labTotalCount.Text);

                            DataTable DistCtn = dt.DefaultView.ToTable(true, new string[] { "woid", "cartonnumber", "palletnumber", "mcartonnumber", "mpalletnumber" });
                            foreach (DataRow dr in DistCtn.Rows)
                            {
                                listctn.Items.Add(dr[1].ToString()+"-"+dr[3].ToString()+"-"+dt.Rows.Count.ToString());
                              string sPalletRes= refWebtPalletInfo.Instance.InsertPalletAndMpalletInfo(labpallet.Text, LabCustPallet.Text, dr[1].ToString(), dr[3].ToString(), dt.Rows.Count, labwoId.Text, labPartnumber.Text);
                              if (sPalletRes != "OK")
                              {
                                  SendMsgToClient("ERROR: [" + InputCarton + "] Inser PalletInfo Fail "+sPalletRes, "ERROR: [" + InputCarton + "] Inser PalletInfo Fail "+sPalletRes);
                                  MessageBox.Show("ERROR: [" + InputCarton + "] Inser PalletInfo Fail " + sPalletRes);
                              }
                            }                         

                            if (Convert.ToInt32(labcount.Text) >= Convert.ToInt32(labTotalCount.Text))
                            {
                                btn_ClosePallet_Click(null, null);
                            }

                            #endregion
                        }
                        else
                        {
                            SendMsgToClient("ERROR: " + tbInput.Text + " 没有资料,请重新刷入", "ERROR: " + tbInput.Text + " No Data");
                        }
                     
                    }
                }
                catch (Exception ex)
                {
                    errflag = true;
                    SendMsgToClient("程式发生异常" + ex.Message, "PALLET PRG ERR-->" + ex.Message);
                    if (MessageBoxEx.Show("程式发生异常" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    {
                        errflag = false;
                    }
                }
            }
        }


        /// <summary>
        /// 获取栈板容量
        /// </summary>
        /// <returns></returns>
        private bool GetPackParameters()
        {
           DataTable dtpack=FrmBLL.ReleaseData.arrByteToDataTable(refWebtPackParameters.Instance.GetPackModelParameters(labPartnumber.Text,labver.Text));
           if (dtpack.Rows.Count != 0)
           {
               labTotalCount.Text = dtpack.Rows[0]["palletqty"].ToString();
               if (int.Parse(labTotalCount.Text) == 0)
               {
                   SendMsgToClient("ERROR: 包装容量设定错误", "ERROR: Packing Capacity Set Err ");
                   return false;
               }
               else
               {
                   return true;
               }
           }
           else
           {
               SendMsgToClient("ERROR: 包装容量未设定,请先设定包装容量", "ERROR: Packing Capacity Not Set ");
               return false;
           }
        }

        private bool CheckRoute(DataTable dt,string woEndGroup)
        {
            bool sChkFlag = true;
            DataTable dtlocation = dt.DefaultView.ToTable(true, "LOCSTATION");     
            foreach (DataRow dr in dtlocation.Rows)
            {
                DataTable dtesn = FrmBLL.publicfuntion.getNewTable(dt, string.Format("LOCSTATION='{0}'", dr["LOCSTATION"].ToString()));
                string chkesn = dtesn.Rows[0]["ESN"].ToString();
                string RouteCode = dtesn.Rows[0]["ROUTGROUPID"].ToString();
                string ErrFlag = dtesn.Rows[0]["ERRFLAG"].ToString();
                string LocGroup = dtesn.Rows[0]["LOCSTATION"].ToString();
                string NextStation = dtesn.Rows[0]["NEXTSTATION"].ToString();
                string woId = dtesn.Rows[0]["WOID"].ToString();     

                Dictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("DATA", chkesn);
                mst.Add("MYGROUP", MYGROUP);
                string SnResult = refWebProPublicStoredproc.Instance.ExecuteProcedure("pro_CheckRoute".ToUpper(), FrmBLL.ReleaseData.DictionaryToJson(mst));
                if (SnResult != "OK")
                {
                    SendMsgToClient("ERROR: " + SnResult, "ERROR: " + SnResult);
                    sChkFlag = false;
                    break;
                }
            }

            return sChkFlag;

        }
   
        private void btnSelectLine_Click(object sender, EventArgs e)
        {

            Frm_StationName fsn = new Frm_StationName(this);
            fsn.ShowDialog();

            //string[] EmpData = Input.InputBox.ShowInputBox("输入权限", string.Empty);
            //try
            //{
            //    string UserId = EmpData[0];
            //    string PWD = EmpData[1];
            //    if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(PWD))
            //    {
            //        string _StrErr = refWebtUserInfo.Instance.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);
            //        if (_StrErr == "OK")
            //        {
                          // SendMsgToClient( "权限正确","Employee OK");

                            //Line = true;
                            //DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtLineInfo.Instance.GetAllLineInfo());
                            //SelectData sd = new SelectData(this, dt);
                            //sd.ShowDialog();
                            //SetStation();
                            //Line = false;
                       

            //        }
            //        else
            //        {
            //            SendMsgToClient(_StrErr,"ERROR:"+ _StrErr);

            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    SendMsgToClient("权限格式不正确:" + ex.Message, "ERROR: Emp Format Error:" + ex.Message);
            //}
         
        }

  

        private void FrmPallet_FormClosed(object sender, FormClosedEventArgs e)
        {
            string path = System.Environment.CurrentDirectory;
            if (lbxServer.Items.Count != 0)
            {
                FrmBLL.publicfuntion.SaveTxtLog(path + "\\PalletLog", lbxServer);
            }



            //XmlDocument doc = new XmlDocument();
            //string XmlName = "DllConfig.xml";
            //doc.Load(XmlName);

            //((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PALLET").SelectSingleNode("LINE")).SetAttribute("Name", LineName);
            //((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PALLET").SelectSingleNode("LINECODE")).SetAttribute("Name", LineCode);
            //((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PALLET").SelectSingleNode("CRFTNAME")).SetAttribute("Name", CraftName);
            //((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PALLET").SelectSingleNode("CRFTID")).SetAttribute("Name", CraftId);
            //doc.Save(XmlName);
            //try
            //{
            //    lbl.Quit();
            //}
            //catch
            //{
            //}
        }

        private void btn_ClosePallet_Click(object sender, EventArgs e)
        {
            btn_ClosePallet.Enabled = false;
            labcount.Text = "0";           
            ClosePalletNo();               
            if (LabelPrint.Checked)
            {
                //PalletNo = labpallet.Text.Trim();
                //PartNo = labPartnumber.Text.Trim();
                //PartName = Model;
                //PartVer = labver.Text.Trim();
                //priner_Pallet_label("");
                DataTable dtProduct = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtProduct.Instance.GetProductByPartNumber(labPartnumber.Text.Trim()));
                DataTable dt = new DataTable("LabelPring");
                dt.Columns.Add("Colnum",typeof(string));
                dt.Columns.Add("Values", typeof(string));
                dt.Rows.Add("PALLETNO", labpallet.Text.Trim());
                dt.Rows.Add("MPALLETNO", LabCustPallet.Text.Trim());
                dt.Rows.Add("PARTNUMBER", dtProduct.Rows[0]["PARTNUMBER"].ToString());
                dt.Rows.Add("PRODUCTNAME", dtProduct.Rows[0]["PRODUCTNAME"].ToString());
                dt.Rows.Add("PRODUCTCOLOR", dtProduct.Rows[0]["PRODUCTCOLOR"].ToString());      

                int Total = 0;
                for (int x = 0; x < listctn.Items.Count;x++ )
                {                 

                    dt.Rows.Add("CARTON_NO" + (x+1).ToString(),listctn.Items[x].ToString().Split('-')[0]);
                    dt.Rows.Add("MCARTON_NO" + (x+1).ToString(), listctn.Items[x].ToString().Split('-')[1]);
                    Total += Convert.ToInt32(listctn.Items[x].ToString().Split('-')[2]);
                }
                dt.Rows.Add("QTY", Total.ToString());
                PrintPalletLabel(dt, 2, LabFilepatch);
            }
            listctn.Items.Clear();
        }

        private void ClosePalletNo()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("WOID",labwoId.Text.Trim());
             dic.Add("PARTNUMBER",labPartnumber.Text.Trim());
             dic.Add("PALLETNUMBER",labpallet.Text.Trim());
             dic.Add("LINE", MyLine);
             dic.Add("CLOSEFLAG",1);
             refWebtPalletInfo.Instance.UpdatePalletCloseFlag(FrmBLL.ReleaseData.DictionaryToJson(dic));
            //refWebtPalletInfo.Instance.UpdatePalletCloseFlag(new WebServices.tPalletInfo.tPalletInfoTable()
            //{
            //    woId = labwoId.Text.Trim(),
            //    PartNumber = labPartnumber.Text.Trim(),
            //    PalletNumber = labpallet.Text.Trim(),
            //    Line = LineName,
            //    CloseFlag = 1

            //});
        }



        private void tsmesn_Click(object sender, EventArgs e)
        {
            tsmcarton.Checked = false;
            tsmkeyparts.Checked = false;
            MessageBox.Show("当前选择刷入ESN");
        }

        private void tsmcarton_Click(object sender, EventArgs e)
        {
            tsmesn.Checked = false;
            tsmkeyparts.Checked = false;
            MessageBox.Show("当前选择刷入Carton");
        }
        private void tsmkeyparts_Click(object sender, EventArgs e)
        {
            tsmesn.Checked = false;
            tsmcarton.Checked = false;
            MessageBox.Show("当前选择刷入 KeyPart");
        }

        #region 栈板列印需求参数
        /// <summary>
        /// 栈板号
        /// </summary>
        public string PalletNo = "";

        /// <summary>
        /// 客户栈板号码
        /// </summary>
        public string CustPalletNo = "";
   
        /// <summary>
        /// 产品料号
        /// </summary>
        public string PartNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string PartName { get; set; }
        /// <summary>
        /// 产品版本
        /// </summary>
        public string PartVer { get; set; }
        #endregion

        private void ReprintPallet_Click(object sender, EventArgs e)
        {
            FrmPalletRePrint fpr = new FrmPalletRePrint(this);
            fpr.ShowDialog();
        }

        private void smClosePallet_Click(object sender, EventArgs e)
        {
            FrmPalletClose fpc = new FrmPalletClose(this);
            fpc.ShowDialog();
        }

        private void labfrmMsg_Click(object sender, EventArgs e)
        {

        }

        private string GetCartonNoInfo(string sData)
        {
            string CartonNo=string.Empty;
            string Colnum = string.Empty;
            if (tsmcarton.Checked)
            {
                CartonNo = sData == "NA" ? "ERROR" : sData;
            }
            else
            {
                if (tsmesn.Checked)
                {
                    Colnum = "ESN";
                }
                if (tsmkeyparts.Checked)
                {
                    Colnum = "SN";
                    #region
                    //string KeyPartToEsn = refWebtWipKeyPart.Instance.GetEsnForKeyParts(sData);
                    //if (KeyPartToEsn != "ERROR")
                    //{                  

                    //    DataTable dtesn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo("ESN", KeyPartToEsn));
                    //    if (dtesn.Rows.Count != 0)
                    //    {

                    //        if (dtesn.Rows[0]["产品箱号"].ToString() == "NA")
                    //        {
                    //            SendMsgToClient("ERROR: 获取到系统默认参数", "ERROR: " + tbInput.Text + " Default Data");
                    //            return;
                    //        }
                    //        tbInput.Text = dtesn.Rows[0]["产品箱号"].ToString();
                    //        this.tbInput.SelectAll();
                    //        this.tbInput.Focus();
                    //    }
                    //    else
                    //    {
                    //        SendMsgToClient("ERROR: " + tbInput.Text + "未找到产品箱号", "ERROR: " + tbInput.Text + " No Data");
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    SendMsgToClient("ERROR: " + tbInput.Text + "未找到KeyPart", "ERROR: " + tbInput.Text + " No KeyPart");
                    //    return;
                    //}

                    #endregion
                }

                DataTable dtesn = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo(Colnum, sData));
                if (dtesn.Rows.Count != 0)
                {
                    CartonNo = dtesn.Rows[0]["cartonnumber"].ToString() == "NA" ? "ERROR" : dtesn.Rows[0]["cartonnumber"].ToString();
                }
                else
                {
                    SendMsgToClient("ERROR: " + tbInput.Text + "未找到产品箱号", "ERROR: " + tbInput.Text + " No Data");
                    CartonNo = "ERROR";
                }
            }

            return CartonNo;
        }

        /// <summary>
        /// 获取客户箱号
        /// </summary>
        /// <param name="PartNo"></param>
        /// <returns></returns>
        private string GetCustPalletInfo(string PartNo)
        {
            try
            {
                string CustPalletNo = "ERROR";
                DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtB_SnRule_PartNumber.Instance.GetB_SNRULE_PARTNUMBER(PartNo));
                if (dt.Rows.Count == 0)
                {
                    SendMsgToClient("ERROR:没有设定客户资料规则", "No Define Customer Information");
                    throw new Exception("ERROR");
                }
                if (dt.Rows.Count >1)
                {
                    SendMsgToClient("ERROR:客户规则发现多笔资料,请确认...", "There Are Too Many Customer Rules");
                    throw new Exception("ERROR");
                }


                string Cust_Pallet_PreFix = dt.Rows[0]["CUST_PALLET_PREFIX"].ToString() == "NA" ? "" : dt.Rows[0]["CUST_PALLET_PREFIX"].ToString();
                string Cust_Pallet_PostFix = dt.Rows[0]["CUST_PALLET_POSTFIX"].ToString() == "NA" ? "" : dt.Rows[0]["CUST_PALLET_POSTFIX"].ToString();
                int Cust_Pallet_Leng =Convert.ToInt32( dt.Rows[0]["CUST_PALLET_LENG"].ToString());
                string Cust_Pallet_STR = dt.Rows[0]["CUST_PALLET_STR"].ToString();
                string RuleType=dt.Rows[0]["RULE_TYPE"].ToString();
                string Cust_Last_Pallet = dt.Rows[0]["CUST_LAST_PALLET"].ToString();
                if (string.IsNullOrEmpty(Cust_Pallet_PreFix) || Cust_Pallet_PreFix == "NA")
                {
                    CustPalletNo = "NA";
                }
                else
                {

                    CustPalletNo = Cust_Pallet_PreFix + Cust_Last_Pallet + Cust_Pallet_PostFix;
                    if (CustPalletNo.Length != Cust_Pallet_Leng)
                    {
                        SendMsgToClient(string.Format("ERROR:生成客户栈板规则长度错误实际[{0}]位,规则要求[{1}]位,请确认...", CustPalletNo.Length.ToString(), Cust_Pallet_Leng.ToString()), string.Format("ERROR:Creat Customer Pallet Error,[{0}]≠[{1}]", CustPalletNo.Length.ToString(), Cust_Pallet_Leng.ToString()));
                        throw new Exception("ERROR");
                    }
                    DataTable dtchekuse = FrmBLL.ReleaseData.arrByteToDataTable(refWebtPalletInfo.Instance.GetPalletAndMpalletInfo(CustPalletNo, 1));
                    if (dtchekuse.Rows.Count > 0)
                    {
                        SendMsgToClient("ERROR: 客户栈板重复: " + CustPalletNo, "ERROR: CustPallet Duplicate: " + CustPalletNo);
                        throw new Exception("ERROR");
                    }


                    string NextPalletStr = (Convert.ToInt32(Cust_Last_Pallet) + 1).ToString().PadLeft(Cust_Pallet_STR.Length, '0');
                   
                    Dictionary<string,object> Dic = new  Dictionary<string,object>(); 
                    Dic.Add("PARTNUMBER",PartNo);
                    Dic.Add("RULE_TYPE",RuleType);
                    string CustRes = refWebtB_SnRule_PartNumber.Instance.UpdateCustPalletCartonNo(FrmBLL.ReleaseData.DictionaryToJson(Dic), NextPalletStr, 3);
                    //string CustRes = refWebtB_SnRule_PartNumber.Instance.UpdateCustPalletCartonNo(new WebServices.tB_SnRule_PartNumber.B_SNRULE_PARTNUMBER_Table()
                    //    {
                    //        PARTNUMBER = PartNo,
                    //        RULE_TYPE = RuleType

                    //    }, NextPalletStr, 3);
                    if (CustRes != "OK")
                    {
                        SendMsgToClient("ERROR:更新客户规则信息错误", "ERROR: Update Next Custmer CTN Fail ");
                        throw new Exception("ERROR");
                    }
                }
              return CustPalletNo;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private bool CheckWOInfo(string woId, string PartNumber,out string EndGroup)
        {
            EndGroup = "ERROR";
            bool sChkFlag = true;
            DataTable dtver = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(woId,null,"WOSTATE,OUTPUTGROUP,PVER,LINEID"));
            if (dtver.Rows.Count != 0)
            {
                labver.Text = dtver.Rows[0]["pver"].ToString();
                int WOStatus = Convert.ToInt32(dtver.Rows[0]["wostate"].ToString());
                EndGroup = dtver.Rows[0]["outputgroup"].ToString();
                ProductLine = dtver.Rows[0]["LINEID"].ToString();
                if (WOStatus == 0)
                {
                    SendMsgToClient("ERROR: 工单待Relaese", "ERROR: Waiting Relaese ");
                    sChkFlag = false;
                }
                else
                    if (WOStatus == 3)
                    {
                        SendMsgToClient("ERROR: 工单已关闭", "ERROR: WO IS CLOSED");
                        sChkFlag = false;
                    }
                    else
                        if (WOStatus == 4)
                        {
                            SendMsgToClient("ERROR: 工单HOLD", "ERROR: WO HOLD");
                            sChkFlag = false;
                        }

            }
            else
            {
                SendMsgToClient("ERROR: 工单资料不存在,请确认...", "ERROR: WO Not Found");
                sChkFlag = false;
            }
           return sChkFlag;
        }




        public void PrintPalletLabel(DataTable dtPrint, int PrintQTY, string FilePatch)
        {

            if (!File.Exists(FilePatch))  //判断条码文件是否存在
            {
                sMain.ShowPrgMsg("条码档没有找到,请确认当前目录是否存在 PALLET.lab", MainParent.MsgType.Error);
                return;
            }

            try
            {
                lbl.Documents.Open(FilePatch, false);// 调用设计好的label文件
                LabelManager2.Document doc = lbl.ActiveDocument;
                foreach (DataRow dr in dtPrint.Rows)
                {
                    try
                    {
                        doc.Variables.FormVariables.Item(dr[0].ToString()).Value = dr[1].ToString();
                    }
                    catch
                    {
                    }
                }
                int Num = Convert.ToInt32(PrintQTY);        //打印数量
                doc.PrintDocument(Num);                             //打印
            }
            catch (Exception ex)
            {
                sMain.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
            finally
            {
                //退出
            }
        }


        private bool CHECK_PRODUCT_LINE()
        {

            return true;

            bool flag = false;
            foreach (string str in ProductLine.Split(','))
            {
                if (str == MyLine)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                SendMsgToClient(string.Format("此工单不可在{0}生产", MyLine), string.Format(" Can not be produced in the [{0}] Line", MyLine));
            return flag;
        }

        private void imbt_LineSelect_Click(object sender, EventArgs e)
        {
            Frm_StationName fsn = new Frm_StationName(this);
            fsn.ShowDialog();
        }

    }
}
