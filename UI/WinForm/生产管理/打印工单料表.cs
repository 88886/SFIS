using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using FrmBLL;
using RefWebService_BLL;
using System.IO;


namespace SFIS_V2
{
    public partial class WoBomInfo : Office2007Form// Form
    {
        public WoBomInfo(MainParent _mfrm)
        {
            InitializeComponent();
            this.mFrm = _mfrm;
        }
        private delegate void delegateloadwobominfo(string woid, bool usesapdata);
        private delegateloadwobominfo eventloadbominfo;
        private IAsyncResult iasyncresult;

        private MainParent mFrm = null;
        private enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error } //文字输出类型
        private Color[] LogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };//文字颜色
        /// <summary>
        /// BOM表信息抬头(用来比较选择的Excel文件格式是否正确)
        /// </summary>
        private readonly string[] mBomHadInfo =
            new string[] { "生产工单号", "成品料号", "组件料号", "组件物料描述", "数量", "制程段" };
        private enum eBomHadInfo
        {
            生产工单号,
            成品料号,
            组件料号,
            组件物料描述,
            数量,
            制程段
        }
        private void bt_inputwobominfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.rtb_msglog.Clear();
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "选择工单料表";
                ofd.Filter = "(*.xls Excel 2003)|*.xls|(*.*)|*.*";
                //  ofd.InitialDirectory = "c:\\";
                DialogResult dlr = ofd.ShowDialog();

                if (dlr == DialogResult.Yes || dlr == DialogResult.OK)
                {
                    //获取excel中有几个表
                    List<string> lsname = ClsReadExcel.GetTableNames(ofd.FileName);// BLL.ClsReadExcel.GetTableNames(ofd.FileName);
                    if (lsname.Count > 1)
                        throw new Exception("料表格式错误,请确保Excel文件中只有一张表..");
                    if (lsname[0].Split('-').Length != 3)
                        throw new Exception("料表名称规则不符,正确格式 => \"工单号-成品料号-BOM版本\"");

                    DataTable _dt = ClsReadExcel.getTable(ofd.FileName, lsname[0]);
                    if (_dt.Rows.Count < 1)
                        throw new Exception("料表中没有数据,请检查..");

                    if (_dt.Columns.Count != this.mBomHadInfo.Length)
                        throw new Exception("料表格式错误,请检查料表的第一行标题信息是否不符合规则");

                    for (int x = 0; x < _dt.Columns.Count; x++)
                    {
                        bool flag = false;
                        foreach (string item in this.mBomHadInfo)
                        {
                            if (_dt.Columns[x].Caption == item)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                            throw new Exception("料表标题不符合规则,请查看文件");

                        DataTable mdt = _dt.DefaultView.ToTable(true, new string[] { "生产工单号" });
                        int xx = 0;
                        for (int i = 0; i < mdt.Rows.Count; i++)
                        {

                            if (!string.IsNullOrEmpty(mdt.Rows[i][0].ToString()))
                                xx++;
                        }
                        if (xx != 1)
                            throw new Exception("导入的工单料表中存在多个工单号名称,请检查..");


                        DataTable mdtt = _dt.DefaultView.ToTable(true, new string[] { "成品料号" });
                        int xxx = 0;
                        for (int i = 0; i < mdtt.Rows.Count; i++)
                        {

                            if (!string.IsNullOrEmpty(mdtt.Rows[i][0].ToString()))
                                xxx++;
                        }
                        if (xxx != 1)
                            throw new Exception("导入的工单料表中存在多个成品料号,请检查..");
                    }

                    //查询导入的料表对应的工单号是否在系统中已经建立
                    int ic = 0;
                    this.mFrm.ShowPrgMsg(string.Format("累计[{0}]颗物料需要导入系统", _dt.Rows.Count), MainParent.MsgType.Warning);
                    foreach (DataRow dr in _dt.Rows)
                    {
                        if (!dr.IsNull("生产工单号") && !dr.IsNull("成品料号") && !dr.IsNull("组件料号"))
                        {

                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("KPDESC", dr[eBomHadInfo.组件物料描述.ToString()].ToString());
                            dic.Add("KPNUMBER", dr[eBomHadInfo.组件料号.ToString()].ToString());
                            dic.Add("PARTNUMBER", dr[eBomHadInfo.成品料号.ToString()].ToString());
                            dic.Add("PROCESS", dr[eBomHadInfo.制程段.ToString()].ToString());
                              dic.Add("QTY",string.IsNullOrEmpty(dr[eBomHadInfo.数量.ToString()].ToString()) ? 0 : int.Parse(dr[eBomHadInfo.数量.ToString()].ToString()));
                              dic.Add("USERID",this.mFrm.gUserInfo.userId);
                              dic.Add("WOID", dr[eBomHadInfo.生产工单号.ToString()].ToString());
                              dic.Add("BOMVER", lsname[0].Split('-')[2].Replace("$'", ""));
                             string _StrErr= refWebtWoBomInfo.Instance.InsertWoBomInfo(FrmBLL.ReleaseData.DictionaryToJson(dic));
                            
                             if (_StrErr == "OK")
                                 this.ShowMsg(LogMsgType.Outgoing, string.Format("第[{0}]颗:[{1}]导入成功", ic + 1, dr[eBomHadInfo.组件料号.ToString()].ToString()));
                             else
                                 this.ShowMsg(LogMsgType.Error, string.Format("第[{0}]颗:[{1}]导入失败:"+_StrErr, ic + 1, dr[eBomHadInfo.组件料号.ToString()].ToString()));
                             
                            ic++;
                        }
                    }
                    this.cb_woname.Text = _dt.Rows[0][eBomHadInfo.生产工单号.ToString()].ToString();
                    this.bt_entrywo_Click(null, null);
                    
                    FrmBLL.publicfuntion.InserSystemLog(this.mFrm.gUserInfo.userId, "导入备料表", "新增", "导入备料表: " + "生产工单:" + _dt.Rows[0][eBomHadInfo.生产工单号.ToString()].ToString() + ";成品料号: " + _dt.Rows[0][eBomHadInfo.成品料号.ToString()].ToString());

                    this.mFrm.ShowPrgMsg("料件导入完成..", MainParent.MsgType.Incoming);
                    MessageBoxEx.Show(string.Format("料件导入完成\n累计成功导入[{0}]颗物料,请核对", ic));
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        //IAsyncResult iasyncresult;
        private void bt_printMpTable_Click(object sender, EventArgs e)
        {
            if (iasyncresult != null && !iasyncresult.IsCompleted)
            {
                MessageBoxEx.Show("程序还在运行中,请稍后");
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(this.cb_woname.Text))
                    throw new Exception("请选择生产工单号");
                if (string.IsNullOrEmpty(this.cb_machineId.Text))
                    throw new Exception("请选择机器编号");
                if (string.IsNullOrEmpty(this.cb_pcbside.Text))
                    throw new Exception("请填写或选择所备料的 PCB面(TOP面/BOTTOM面");

                eventdelegateprint = new EventPrint(DelegatePrint);
                iasyncresult = eventdelegateprint.BeginInvoke(
                     this.cb_woname.Text.Trim(),
                     this.mFrm.gUserInfo.userId,
                     this.cb_machineId.Text.Trim(),
                     this.cb_pcbside.Text.Trim(),
                     ref MyTable, null, null);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        private string getstring(string[] arr, int icount)
        {
            string str = "";
            for (int x = 0; x < arr.Length; x++)
            {
                str += arr[x] + ",";
                if (x != 0 && 0 == (x % icount))
                    str += "|";
                if (x + 1 == arr.Length)
                    str += "|";
            }
            return str;
        }

        private delegate void EventPrint(string woId, string userId, string MachineId, string pcbSide, ref DataTable _datatable);
        private EventPrint eventdelegateprint;
        private string sEditUser = string.Empty;
        private string sCheckedUser = string.Empty;

        public void DelegatePrint(string woId, string userId, string MachineId, string pcbSide, ref DataTable _datatable)
        {
            string Err;
            System.Data.DataTable masterTable = null;
            System.Data.DataTable _dtkpdetale = null;
            try
            {
                if (_datatable.Rows.Count < 1)
                    throw new Exception("请先选择需要打印料站表的生产工单或导入工单料表..");

                //明确需要备料的工单、为哪个机器备料、备料是PCB的TOP面还是bottom面

                //根据填写的机器号和pcb面 以及成品料号 找出料站表头一行信息
                masterTable = FrmBLL.ReleaseData.arrByteToDataTable(refWebExcelToDb.Instance.GetKpMasterInfo(_datatable.Rows[0]["成品料号"].ToString(),MachineId, pcbSide));

                ////检测料站表是否经过品质审核
                //if (!refWebtSmtKpMonitor.Instance.CheckKpMasterIdStatus(masterTable.Rows[0]["masterId"].ToString()))
                //    throw new Exception("料站表[" + masterTable.Rows[0]["masterId"].ToString() + "]没有经过审核");

                //找到料表的创建者和审核者  --2013-4-26                
                string _Msg = refWebtSmtKpMonitor.Instance.GetKpMasterIdStatus(masterTable.Rows[0]["masterId"].ToString());
                if (_Msg.IndexOf("ERROR") != -1)
                    throw new Exception(_Msg);
                this.sEditUser = _Msg.Split(',')[0].Split(':')[1];
                this.sCheckedUser = _Msg.Split(',')[1].Split(':')[1];
                //比对BOM版本
                //_BomVer = masterTable.Rows[0]["bomver"].ToString().Trim().ToUpper();//20151117不检查ME导入料表版本与工单表一致性
                //if (masterTable.Rows[0]["bomver"].ToString().Trim().ToUpper() != this._BomVer)
                //{ //_datatable.Rows[0]["BOM版本"].ToString().Trim().ToUpper())
                //    throw new Exception(string.Format("料站表的BOM版本:[{0}] 与 备料表的BOM版本[{1}] 不一致!!",
                //        masterTable.Rows[0]["bomver"].ToString(), this._BomVer));
                //}
                if (masterTable.Rows[0]["bomver"].ToString().Trim().ToUpper() != this._BomVer)
                { 
                    throw new Exception(string.Format("料站表的BOM版本:[{0}] 与 备料表的BOM版本[{1}] 不一致!!",
                        masterTable.Rows[0]["bomver"].ToString(), this._BomVer));
                }

                //根据料站表头Id获取产品料站详细信息
                //_dtkpdetale = FrmBLL.ReleaseData.arrByteToDataTable(refWebExcelToDb.Instance.GetSmtKpDetaltByMasterId(masterTable.Rows[0]["masterId"].ToString(), out Err));

                //根据新的料站规则找料
                _dtkpdetale = FrmBLL.ReleaseData.arrByteToDataTable(refWebExcelToDb.Instance.GetSmtKpDetaltByMasterIdNew(masterTable.Rows[0]["masterId"].ToString(), woId, out Err));
                if (Err != "OK")
                    throw new Exception("GetSmtKpDetaltByMasterIdNew-"+Err);

                IList<IDictionary<string, object>> lsdic = new List<IDictionary<string, object>>();
                IDictionary<string, object> dic = null;
                DataRow[] MyArrDr = null;

                foreach (DataRow dr in _dtkpdetale.Rows)
                {
                    DataRow[] _drItem;
                    MyArrDr = _datatable.Select(string.Format("组件料号='{0}'", dr["kpnumber"].ToString()));

                    if (MyArrDr.Length == 1)
                    {
                        dic = new Dictionary<string, object>();
                        dic.Add("WOID", woId);
                        dic.Add("PARTNUMBER", _datatable.Rows[0]["成品料号"].ToString());
                        dic.Add("USERID", userId);
                        dic.Add("STATIONNO", dr["stationno"].ToString());
                        dic.Add("MASTERID", masterTable.Rows[0]["masterId"].ToString());
                        dic.Add("KPNUMBER", dr["kpnumber"].ToString());
                        dic.Add("KPDESC", dr["kpdesc"].ToString());
                        dic.Add("REPLACEGROUP", dr["replacegroup"].ToString());
                        dic.Add("KPDISTINCT", dr["kpdistinct"].ToString());
                        dic.Add("BOMVER", this._BomVer);
                        dic.Add("LOCALTION", dr["loction"].ToString());
                        dic.Add("SIDE", pcbSide);
                        dic.Add("FEEDERTYPE", dr["reserve"].ToString());                      
                        lsdic.Add(dic);                      
                    }

                    if (MyArrDr.Length > 1)
                        throw new Exception(string.Format("同一个料号 \"{0}\" 在发料记录中存在两笔", dr["kpnumber"].ToString()));

                    if (MyArrDr.Length < 1)
                    {
                        if (string.IsNullOrEmpty(dr["replacegroup"].ToString().Trim()))
                        {
                            throw new Exception(string.Format("在工单备料表中没有发现物料:\"{0}\" ,备料表不能打印..",
                                dr["kpnumber"].ToString()));
                        }

                        _drItem = _dtkpdetale.Select(string.Format("replacegroup='{0}'", dr["replacegroup"]));
                        bool flag = false;
                        for (int x = 0; x < _drItem.Length; x++)
                        {
                            MyArrDr = _datatable.Select(
                                string.Format("组件料号='{0}'", _drItem[x]["kpnumber"].ToString()));

                            if (MyArrDr.Length > 0)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                            throw new Exception(string.Format("在工单备料表中没有发现物料:\"{0}\" 及对应的物料组别 ,备料表不能打印..",
                                dr["kpnumber"].ToString()));
                    }
                }
                Err = refWebtWoBomInfo.Instance.InsertWoBomPrintInfos(FrmBLL.ReleaseData.ListDictionaryToJson(lsdic));     
                if (Err!="OK")
                {
                     throw new Exception(Err);
                }
                PrintForTable(userId, FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoBomInfo.Instance.GetMaterialPreparation(woId,
                    masterTable.Rows[0]["masterId"].ToString())),
                    masterTable.Rows[0]["lineId"].ToString(),
                    System.AppDomain.CurrentDomain.BaseDirectory + @"Excel");

            }
            catch (Exception ex)
            {
                ShowMsg(LogMsgType.Error, ex.Message);
                mFrm.ShowPrgMsg("备料信息打印失败!", MainParent.MsgType.Error);// throw ex;
            }
        }

        public void PrintForTable(string userId, DataTable _datatable, string machineId, string filepath)
        {//传进来的datatable需要进过排序

            ClsAllExcel excel = new ClsAllExcel();
            if (System.IO.Directory.Exists(filepath))
            {
                if (!System.IO.File.Exists(string.Format(@"{0}\print.xlt", filepath)))
                {
                    mFrm.ShowPrgMsg("模板没有找到,正在从Ftp下载..", MainParent.MsgType.Warning);
                    FrmBLL.Ftp_MyFtp ftp = new FrmBLL.Ftp_MyFtp();
                    ftp.DownloadFile("print.xlt", System.AppDomain.CurrentDomain.BaseDirectory + "excel", "print.xlt");
                    mFrm.ShowPrgMsg("下载完成", MainParent.MsgType.Outgoing);

                    //throw new Exception("备料表打印模板文件不存在");
                }
            }
            else
            {
                throw new Exception("打印模板文件路错误(请在程序运行目录下建立\"Excel\"文件夹");
            }
            excel.OpenFileName = string.Format(@"{0}\print.xlt", filepath);// :"D:\BOM\New\print.xlt";
            excel.OpenExcelFile();
            int rowindex = 10;
            excel.setOneCellValue(2, 6, string.Format("*{0} {1}*",
                _datatable.Rows[0]["masterId"].ToString(), _datatable.Rows[0]["woId"].ToString()));
            excel.setOneCellValue(3, 6, string.Format("{0} {1}",
               _datatable.Rows[0]["masterId"].ToString(), _datatable.Rows[0]["woId"].ToString()));
            excel.setOneCellValue(3, 2, _datatable.Rows[0]["woId"].ToString());
            excel.setOneCellValue(3, 3, _datatable.Rows[0]["modelname"].ToString());
            excel.setOneCellValue(4, 2, _datatable.Rows[0]["partnumber"].ToString());
            excel.setOneCellValue(4, 4, _datatable.Rows.Count.ToString());
            excel.setOneCellValue(5, 4, _datatable.Rows[0]["side"].ToString());
            excel.setOneCellValue(6, 4, _datatable.Rows[0]["bomver"].ToString());

            excel.setOneCellValue(5, 9, userId);
            excel.setOneCellValue(6, 9, FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo(userId,null,null)).Rows[0]["username"].ToString());

            excel.setOneCellValue(5, 6, this.sEditUser);
            if (!string.IsNullOrEmpty(this.sEditUser))
                excel.setOneCellValue(6, 6, FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo(this.sEditUser,null,null)).Rows[0]["username"].ToString());
            excel.setOneCellValue(5, 7, this.sCheckedUser);
            if (!string.IsNullOrEmpty(this.sCheckedUser))
                excel.setOneCellValue(6, 7, FrmBLL.ReleaseData.arrByteToDataTable(refWebtUserInfo.Instance.GetUserInfo(this.sCheckedUser,null,null)).Rows[0]["username"].ToString());
            excel.setOneCellValue(6, 2, machineId);

            for (int z = 0; z < _datatable.Rows.Count; z++)// (DataRow dr in _datatable.Rows)
            {
                excel.setOneCellValue(rowindex, 1, _datatable.Rows[z]["stationno"].ToString());
                excel.setOneCellValue(rowindex, 2, _datatable.Rows[z]["kpnumber"].ToString());
                excel.setOneCellValue(rowindex, 3, _datatable.Rows[z]["kpdesc"].ToString());
                excel.setOneCellValue(rowindex, 10, _datatable.Rows[z]["feedertype"].ToString());

                string[] arrStr = getstring(_datatable.Rows[z]["localtion"].ToString().Split(','), 4).Split('|');
                for (int y = 0; y < arrStr.Length; y++)
                {
                    if (!string.IsNullOrEmpty(arrStr[y]))
                    {
                        excel.setOneCellValue(rowindex, 6, arrStr[y]);
                        rowindex++;
                    }
                }

                if (!(z + 1 == _datatable.Rows.Count))
                {
                    //料站相同但是物料组不同的
                    if ((_datatable.Rows[z]["stationno"].ToString().ToUpper() == _datatable.Rows[z + 1]["stationno"].ToString().ToUpper()) &&
                        ((string.IsNullOrEmpty(_datatable.Rows[z]["replacegroup"].ToString().ToUpper()) ? "A" : _datatable.Rows[z]["replacegroup"].ToString().ToUpper()) !=
                        (string.IsNullOrEmpty(_datatable.Rows[z + 1]["replacegroup"].ToString().ToUpper()) ? "B" : _datatable.Rows[z + 1]["replacegroup"].ToString().ToUpper())))
                    {
                        throw new Exception(string.Format("物料替代关系错误:{0}与{1}不存在替代关系,或料站表制作有误.请检查",
                            _datatable.Rows[z]["kpnumber"].ToString(), _datatable.Rows[z + 1]["kpnumber"].ToString()));
                    }

                    if (_datatable.Rows[z]["stationno"].ToString().ToUpper() == _datatable.Rows[z + 1]["stationno"].ToString().ToUpper() &&
                        _datatable.Rows[z]["replacegroup"].ToString().ToUpper() == _datatable.Rows[z + 1]["replacegroup"].ToString().ToUpper())
                    {
                        excel.setOneCellValue(rowindex, 2, _datatable.Rows[z + 1]["kpnumber"].ToString());
                        excel.setOneCellValue(rowindex, 3, _datatable.Rows[z + 1]["kpdesc"].ToString());
                        rowindex++;
                        z++;
                    }
                }

                excel.setOneCellValue(rowindex, 1, "----------------------------------------------------------------------------------------------------------------------------------------------");
                rowindex++;
            }

            excel.SaveFileName = string.Format(@"{2}\{0}_{1}.xls", _datatable.Rows[0]["woId"].ToString(), machineId, filepath);
            excel.SaveAsExcel(true);
            excel.CloseExcelApplication();
        }

        private void cb_woname_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bt_entrywo_Click(sender, e);
        }

        private DataTable MyTable = null;
        string _BomVer = string.Empty;
        private void bt_entrywo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.cb_woname.Text))
                {                  
                    if (iasyncresult != null && !iasyncresult.IsCompleted)
                    {
                        MessageBoxEx.Show("还有任务没有完成,请稍后..");
                        return;
                    }
                    bool _usesapdata = false;
                    //20151117 取消从SAP下载料表
                    //if (MessageBoxEx.Show("是否需要重新从SAP导入备料表?\n重新导入请选择[Yes],否则请选择[NO]",  
                    //    "提示", MessageBoxButtons.YesNo,
                    //    MessageBoxIcon.Asterisk,
                    //    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    //{
                    //    _usesapdata = true;
                    //}
                    eventloadbominfo = new delegateloadwobominfo(this.LoadWoBomInfo);
                    iasyncresult = eventloadbominfo.BeginInvoke(this.cb_woname.Text.Trim(), _usesapdata, null, null);
                }
                else
                {
                    throw new Exception("请输入或选择生产工单号");
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);

            }
        }


        private void LoadWoBomInfo(string woid, bool usesapdata)
        {
            #region 20151117 取消与ME导入料表与工单BOM一致性 20160107需求打开
            //List<string> LsWoinfo = new List<string>();
            //LsWoinfo.Add("BOMVER");
            DataTable _mdt = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWoInfo.Instance.GetWoInfo(woid, null, "BOMVER"));
            //DataTable _mdt = FrmBLL.ReleaseData.arrByteToDataTable(refWebt_wo_Info_Erp.Instance.Get_WO_Info_Erp(woid, "BOMVER"));

            if (_mdt == null || _mdt.Rows.Count < 1)
            {
                this.ShowMsg(LogMsgType.Error, "系统中没有找到对应的工单信息,请SFIS专员建立生产工单");
                return;
            }
            this._BomVer = _mdt.Rows[0]["bomver"].ToString();      
            #endregion
            #region 20151117 取消从SAP下载bom信息
            //if (usesapdata)
            //{
            //    this.ShowMsg(LogMsgType.Outgoing, "正在连接SAP系统,获取备料表信息..");
            //    DataTable _datatable = FrmBLL.ReleaseData.arrByteToDataTable(refWebSapConnector.Instance.Get_Z_RFC_ZMM011(woid));
            //    this.ShowMsg(LogMsgType.Incoming, "SAP备料信息获取完成,正在保存数据..");
            //    if (cb_yuanyu.Checked)
            //    {
            //        this.mFrm.ShowPrgMsg("该工单为远御工单,处理中...", MainParent.MsgType.Error);
            //        for (int i = _datatable.Rows.Count - 1; i >= 0; i--)
            //        {
            //            string str = _datatable.Rows[i]["MATNR2"].ToString();
            //            if (!str.Contains("AF-"))
            //                _datatable.Rows.RemoveAt(i);
            //        }
            //    }

            //    #region 向SFIS系统填充备料信息      
            //    IList<IDictionary<string, object>> lsdic = new List<IDictionary<string, object>>();
            //    Dictionary<string, object> dic = null;
            //    foreach (DataRow dr in _datatable.Rows)
            //    {
            //        dic = new Dictionary<string, object>();
            //        dic.Add("KPNUMBER", dr["MATNR2"].ToString());
            //        dic.Add("BOMVER",_BomVer);
            //         dic.Add("PARTNUMBER",dr["MATNR1"].ToString());
            //         dic.Add("KPDESC",dr["MAKTX2"].ToString());
            //         dic.Add("QTY", string.IsNullOrEmpty(dr["BDMNG"].ToString()) ? 0 : int.Parse(dr["BDMNG"].ToString()));
            //         dic.Add("USERID",this.mFrm.gUserInfo.userId);
            //         dic.Add("WOID",woid);
            //         dic.Add("PROCESS","SMT");
            //         dic.Add("BLOCKED",0);
            //         lsdic.Add(dic);                 
                  
            //    }     
            //    string msg = refWebtWoBomInfo.Instance.InsertWoBomInfoList(FrmBLL.ReleaseData.ListDictionaryToJson(lsdic));
            //    if (!string.IsNullOrEmpty(msg) && msg!="OK")
            //    {
            //        this.ShowMsg(LogMsgType.Error, msg);
            //        return;
            //    }
            //    #endregion
            //}
            #endregion
            this.ShowMsg(LogMsgType.Outgoing, "确认工单信息..");
            DataTable _dt = MyTable = FrmBLL.ReleaseData.arrByteToDataTable(refWebtWoBomInfo.Instance.GetWoBomInfo(woid));
            if (_dt != null && _dt.Rows.Count > 0)
            {
                this.FillDataGridView(_dt);
                this.ShowMsg(LogMsgType.Outgoing, string.Format("选择了工单:{0} BOM版本: {1}", woid, _BomVer));
            }
            else
            {
                this.FillDataGridView(null);
                this.ShowMsg(LogMsgType.Warning, "选择的工单号没有找到备料信息,请重新导入..");
            }
            this.ShowMsg(LogMsgType.Incoming, "备料信息加载完成..");
        }

        private void FillDataGridView(DataTable dt)
        {
            ;
            if (dt == null)
                return;
            this.dvg_wobominfo.Invoke(new EventHandler(delegate
            {
                this.dvg_wobominfo.DataSource = dt.DefaultView;
                this.dvg_wobominfo.Refresh();
            }));
        }
        private void cb_woname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (!string.IsNullOrEmpty(this.cb_woname.Text))
                    bt_entrywo_Click(sender, e);
            }
        }

        private void cb_woname_DropDown(object sender, EventArgs e)
        {
            try
            {
                this.cb_woname.Items.Clear();

                IDictionary<string, object> dic = FrmBLL.ReleaseData.JsonToDictionary(refWebt_wo_Info_Erp.Instance.Get_Erp_WoList());
               // this.cb_woname.Items.AddRange(refWebtWoInfo.Instance.GetWoList()/* BLL.tWoInfo.GetWoList.ToArray()*/);
                foreach (KeyValuePair<string,object> _keyValues in dic)
                {
                    cb_woname.Items.Add(_keyValues.Key);
                }

                this.cb_woname.Refresh();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void WoBomInfo_Load(object sender, EventArgs e)
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

            FileConvert = new ConvertFile(Readtxtfile);
            FileConvert.BeginInvoke(null, null);
        }
        private void ShowMsg(LogMsgType msgtype, string msg)
        {
            this.rtb_msglog.Invoke(new EventHandler(delegate
            {
                rtb_msglog.TabStop = false;
                rtb_msglog.SelectedText = string.Empty;
                rtb_msglog.SelectionFont = new Font(rtb_msglog.SelectionFont, FontStyle.Bold);
                rtb_msglog.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtb_msglog.AppendText(msg + "\n");
                rtb_msglog.ScrollToCaret();
            }));
        }
        private void cb_machineId_DropDown(object sender, EventArgs e)
        {
            try
            {
                this.cb_machineId.Items.Clear();
                if (this.dvg_wobominfo.Rows.Count < 1)
                    throw new Exception("请先选择需要打印料站表的生产工单或导入工单料表..");

                this.cb_machineId.Items.AddRange(refWebExcelToDb.Instance.GetMachineIdList(this.dvg_wobominfo["成品料号", 0].Value.ToString()));
                this.cb_machineId.Refresh();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void cb_machineId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mFrm.ShowPrgMsg(string.Format("选择了机器:{0}", this.cb_machineId.Text), MainParent.MsgType.Outgoing);
        }

        // List<string> lst = new List<string>();
        private List<string> GetFileName()
        {
            List<string> lst = new List<string>();
            string dirPath = System.AppDomain.CurrentDomain.BaseDirectory + "ConvertFile";

            DirectoryInfo mydir = new DirectoryInfo(dirPath);
            FileInfo[] files = mydir.GetFiles("*.txt", SearchOption.AllDirectories);
            foreach (FileInfo info in files)
            {
                lst.Add(dirPath + "\\" + info.Name);

            }
            return lst;

        }

        private void Readtxtfile()
        {
            List<string> lst = new List<string>();
            lst = GetFileName();
            this.mFrm.ShowPrgMsg(string.Format("共[{0}]个工单需要导入系统.", lst.Count), MainParent.MsgType.Warning);
            if (lst.Count != 0)
            {
                for (int x = 0; x < lst.Count; x++)
                {
                    this.mFrm.ShowPrgMsg(string.Format("正在处理第[{0}]个工单..", x + 1), MainParent.MsgType.Warning);
                    StreamReader sr = new StreamReader(lst[x], Encoding.Default);
                    string line;
                    int i = 0;
                    DataTable dt = new DataTable("mydt");
                    dt.Columns.Add("生产工单号", System.Type.GetType("System.String"));
                    dt.Columns.Add("成品料号", System.Type.GetType("System.String"));
                    dt.Columns.Add("组件料号", System.Type.GetType("System.String"));
                    dt.Columns.Add("组件物料描述", System.Type.GetType("System.String"));
                    dt.Columns.Add("数量", System.Type.GetType("System.String"));
                    dt.Columns.Add("制程段", System.Type.GetType("System.String"));
                    while ((line = sr.ReadLine()) != null)
                    {
                        i++;
                        if (i > 6)
                        {
                            if (line.IndexOf("----------------------") == -1)
                            {
                                string[] arr = line.Split('|');
                                dt.Rows.Add(arr[1], arr[2], arr[5], arr[6], arr[8], arr[11]);
                            }
                        }
                    }
                    ClsAllExcel cs = new ClsAllExcel();
                    int len = (System.AppDomain.CurrentDomain.BaseDirectory + "ConvertFile").Length + 1;
                    string filename = lst[x].Substring(len, lst[x].Length - len);
                    filename = filename.Substring(0, filename.Length - 4);
                    cs.ExportExcel(dt, System.AppDomain.CurrentDomain.BaseDirectory + "ConvertFileOK\\" + filename + ".xls", dt.Rows[1][0].ToString().Trim() + "-" + dt.Rows[1][1].ToString().Trim() + "-A");
                    sr.Close();
                    if (File.Exists(lst[x]))
                    {
                        File.Delete(lst[x]);
                    }
                }
                this.mFrm.ShowPrgMsg("处理完成..", MainParent.MsgType.Incoming);
            }
        }
        private delegate void ConvertFile();
        ConvertFile FileConvert;

        private void dvg_wobominfo_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                this.dvg_wobominfo[e.ColumnIndex, e.RowIndex].ToolTipText =
                    string.Format("当前累计:[{0}]笔数据", this.dvg_wobominfo.Rows.Count);
            }
        }


    }


}
