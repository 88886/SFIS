#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using BLL;
using Buzzer;
using DevComponents.DotNetBar;
using LabelManager2;
using SFIS_PRINT_SYSTEM_WIFI.Properties;
using Application = System.Windows.Forms.Application;
using DateTime = System.DateTime;
using Image = System.Drawing.Image;

#endregion

namespace SFIS_PRINT_SYSTEM_WIFI.Frm
{
    public partial class PrintMain : Office2007Form // Form
    {
        private readonly string _mIpaddress = string.Empty;

        private bool _bIsChkMac = true;
        private bool _bIsChkSpmac = true;

        private string _cnsFlag = string.Empty;
        private DelegateRunNoParmet _eventRunFunction;

        /// <summary>
        ///     委托运行没有参数的方法的实例
        /// </summary>
        private DelegateRunNoParmet _eventRunNoparamet;

        private DelegateShowCartonData _eventshowcartondata;
        private DelegateShowOtherData _eventshowotherdata;
        private IAsyncResult _iasyncresult;
        private IAsyncResult _iasyncresult2;

        /// <summary>
        ///     托管跟踪
        /// </summary>
        private IAsyncResult _miasyncresult;

        private IAsyncResult _showCartonIasyncresult;

        public string StrEnaPwd = string.Empty;

        public PrintMain()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     获取当前软件版本
        /// </summary>
        private string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        ///     给蜂鸣器发送声音
        /// </summary>
        private void SendBuzz()
        {
            _bzz.SendMsg("F");
        }

        //==========================================================================================
        //======================              公共函数                             =================
        //==========================================================================================

        private void LoadSystemLine()
        {
            //获取所有生产线的信息          
            cblineId.Items.Clear();
            List<string> lsLine = new List<string>(refWebtLineInfo.Instance.GetLineList());
            foreach (string str in lsLine)
            {
                cblineId.Items.Add(str);
            }
            cblineId.SelectedIndex = 0;
        }

        private void Load_All_Station()
        {
            DataTable dt = ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo());
            DataTable temp = null;
            if (dt.Rows.Count > 0)
            {
                temp = publicfunction.getNewTable(dt, string.Format("TESTFLAG<>'{0}' and TESTFLAG<>'{1}'", "1", "2"));
            }
            DataTable dtLine = new DataTable();
            dtLine.Columns.Add("SECTION", typeof (string));
            dtLine.Columns.Add("GROUP", typeof (string));
            dtLine.Columns.Add("STATION", typeof (string));
            if (temp != null)
                foreach (DataRow dr in temp.Rows)
                {
                    dtLine.Rows.Add(dr["BEWORKSEG"].ToString(), dr["CRAFTNAME"].ToString(),
                        dr["CRAFTPARAMETERURL"].ToString());
                }
            DataTable dtSort = publicfunction.DataTableToSort(dtLine, "GROUP");
            foreach (DataRow dr in dtSort.Rows)
            {
                cbstationId.Items.Add(dr["GROUP"].ToString());
            }
        }


        /// <summary>
        ///     显示卡通箱数据
        /// </summary>
        /// <param name="show">是否显示</param>
        private void ShowCartonData(bool show)
        {
            dgvdata.Invoke(new EventHandler(delegate
            {
                if (!show) return;
                dgvdata.DataSource =
                    ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetPackCarton(MWoInfo.WoId, cblineId.Text));
                dgvdata.Refresh();
            }));
        }

        private void ShowOtherData(string woId, bool show)
        {
            dgvdata.Invoke(new EventHandler(delegate
            {
                if (!show) return;
                dgvdata.DataSource =
                    ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetWoAllSerial(MWoInfo.WoId, 4));
                dgvdata.Refresh();
            }));
        }

        /// <summary>
        ///     重新打印卡通箱标签
        /// </summary>
        /// <param name="cartonnumber">卡通箱编号</param>
        private void RepearePrintCarton(string cartonnumber)
        {
            List<string> lsColumns = new List<string>();
            string[] arColumns = _mPrtColumns.Split(',');
            foreach (string str in arColumns)
            {
                if (!string.IsNullOrEmpty(str) && str.ToUpper() != "PSN")
                {
                    lsColumns.Add(str);
                }
            }
            //判断打印的这箱有没有关闭或满箱
            string cartonState = refWebtWipTracking.Instance.GetCartonState(cartonnumber);
            if (cartonState == null || cartonState.Trim() != "1")
            {
                ShowMsg(MLogMsgType.Error, "卡通箱:" + cartonnumber + "还没有关闭");
            }

            string newErr = string.Empty;
            if (lsColumns.Count > 0)
                newErr =
                    PrintCartonBox(
                        ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetCartonPrintContent(cartonnumber,
                            lsColumns.ToArray())), false);
            else
                ShowMsg(MLogMsgType.Warning, "当前没有选择需要打印的内容,打印不执行..");

            if (!string.IsNullOrEmpty(newErr))
                ShowMsg(MLogMsgType.Error, "打印错误:" + newErr);
            else
                ShowMsg(MLogMsgType.Outgoing, "完成..");
        }

        /// <summary>
        ///     SN号需要按递增规则
        /// </summary>
        /// <param name="serial">当前的SN号</param>
        /// <param name="history">上一个SN号</param>
        /// <returns></returns>
        private bool CompareSnAreaHistory(string serial, string history)
        {
            try
            {
                if (history.Length != serial.Length)
                    return false;
                int flag = 0;
                for (int i = 1; i <= history.Length; i++)
                {
                    if (history.Substring(0, i) == serial.Substring(0, i)) continue;
                    flag = i;
                    break;
                }
                if (flag == 0)
                    return false;
                if (serial.Substring(0, flag - 1) != history.Substring(0, flag - 1))
                    return false;

                int histroy = int.Parse(history.Substring(flag - 1, history.Length - flag + 1));
                var serials = int.Parse(serial.Substring(flag - 1, history.Length - flag + 1));

                return serials == histroy + 1;
            }
            catch (Exception ex)
            {
                ShowMsg(MLogMsgType.Error, ex.Message + ": FrmMain 317");
                return false;
            }
        }

        /// <summary>
        ///     填充自定义控件内容
        /// </summary>
        /// <param name="mb"></param>
        /// <param name="str"></param>
        private void FillTextBOX(MyTextBox mb, string str)
        {
            mb.Invoke(new EventHandler(delegate
            {
                mb.Text = str;
                mb.Refresh();
            }));
        }

        /// <summary>
        ///     查询内容是否存在于数组
        /// </summary>
        /// <param name="str">查询的值</param>
        /// <param name="content">数组</param>
        /// <returns></returns>
        private static bool CompareArray(string str, string[] content)
        {
            bool flag = false;
            foreach (string item in content)
            {
                if (str != item) continue;
                flag = true;
                break;
            }
            return flag;
        }

        /// <summary>
        ///     查询内容是否存在于数组
        /// </summary>
        /// <param name="str">查询的值</param>
        /// <returns></returns>
        private bool CompareArray(string str)
        {
            return CompareArray(str, _mPasswordName);
        }

        /// <summary>
        ///     检查当前输入的内容dicVarBuf中的esn是否合法,如果合法则返回esn和其对应的数据
        /// </summary>
        /// <param name="strEsnTemp">返回的数据</param>
        /// <param name="dtEsnTemp">返回的数据</param>
        /// <returns>
        ///     如果返回真则表示当前输入的esn所查找到的值均和当前输入的一致,
        ///     如果返回假则表示当前输入的esn查找到的内容至少有一项和当前输入不符/不存在
        /// </returns>
        private bool ChkEsn(ref string strEsnTemp, ref DataTable dtEsnTemp)
        {
            #region 判断esn是否存在

            bool bFlag = false;
            foreach (string str in _dicVarBuf.Keys)
            {
                if (str.ToUpper() == "ESN")
                    bFlag = true;
            }
            if (!bFlag)
                return false;

            foreach (string item in _dicVarBuf.Keys)
            {
                if (item.ToUpper() != "ESN") continue;
                strEsnTemp = _dicVarBuf["ESN"];

                //判断esn流程
                string strErr;
                if ((strErr = ChkRoute(strEsnTemp, _mCraftName).ToUpper()) != "OK")
                    throw new Exception(string.Format("输入的序列号[{0}]流程错误\n{1},请检查..", strEsnTemp, strErr));

                DataTable mDt =
                    ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyPart(strEsnTemp));
                foreach (DataRow dr in mDt.Rows)
                {
                    dtEsnTemp?.Rows.Add(dr.ItemArray);
                }

                if (dtEsnTemp != null && dtEsnTemp.Rows.Count > 0)
                {
                    //判断esn是否存在
                    if (dtEsnTemp == null || dtEsnTemp.Rows.Count < 1)
                        throw new Exception(string.Format("流程错误,当前序列号[{0}]不存在,请确认是否经过投板站..", strEsnTemp));
                    //判断esn是否是当前工单的
                    if (dtEsnTemp.Rows[0]["woId"].ToString() != MWoInfo.WoId)
                        throw new Exception(string.Format("输入的序列号[{0}]不属于当前工单[{1}] ≠ [{2}],请检查..",
                            strEsnTemp, MWoInfo.WoId, dtEsnTemp.Rows[0]["woId"]));

                    //判断通过esn取出的数据是否和刷入的数据一致 如果一致则不需要再对输入的数据进行有效性验证,而是直接将值用于当前的数据
                    foreach (string strKey in _dicVarBuf.Keys)
                    {
                        if (strKey.ToUpper() == "ESN")
                            continue;

                        DataRow[] dr = dtEsnTemp.Select(string.Format("sntype='{0}'", strKey));
                        if (dr.Length < 1)
                        {
                            bFlag = false;
                            continue;
                        }
                        if (dr.Length > 1)
                            throw new Exception(string.Format("严重错误!!,序列号[{0}] 在同一个ESN:[{1}] 下存在多次..",
                                _dicVarBuf[strKey], strKey));

                        if (string.IsNullOrEmpty(dr[0]["snval"].ToString()))
                            throw new Exception(string.Format("严重错误:{0}存在空值", strKey));

                        if (dr[0]["snval"].ToString().ToUpper().Trim() != _dicVarBuf[strKey].ToUpper().Trim())
                            throw new Exception(string.Format("当前输入序列号的值[{0}] ≠ 历史数据[{1}]",
                                _dicVarBuf[strKey], dr[0]["snval"]));
                    }

                    break;
                }
                bFlag = false;
            }
            return bFlag;

            #endregion
        }

        /// <summary>
        ///     检查当前输入内容
        /// </summary>
        /// <param name="strEsnTemp"></param>
        /// <param name="dtOtherSnTemp"></param>
        /// <param name="insertDtaTemp"></param>
        /// <param name="dtEsnTemp"></param>
        private void ChkCurrentInput(ref string strEsnTemp, ref DataTable dtOtherSnTemp,
            ref Dictionary<string, string> insertDtaTemp, ref DataTable dtEsnTemp)
        {
            foreach (string strKey in _dicVarBuf.Keys)
            {
                if (strKey.ToUpper() == "ESN") continue;

                #region _dtEsnTemp不为空

                if (dtEsnTemp != null && dtEsnTemp.Rows.Count > 0)
                {
                    DataRow[] arrDr = dtEsnTemp.Select(string.Format("sntype = '{0}'", strKey));
                    if (arrDr.Length > 1)
                        throw new Exception("同一个esn:" + strEsnTemp + "类型" + strKey + "存在多次,请修正..");
                    if (arrDr.Length == 1)
                    {
                        if (
                            !string.Equals(arrDr[0]["snval"].ToString(), _dicVarBuf[strKey],
                                StringComparison.CurrentCultureIgnoreCase))
                        {
                            throw new Exception(string.Format("esn:{0}当前输入的{1}={2}与历史数据{1}={3}不相符,请检查..",
                                strEsnTemp, strKey, _dicVarBuf[strKey].ToUpper(),
                                arrDr[0]["snval"].ToString().ToUpper()));
                        }
                        continue;
                    }
                }

                #endregion

                dtOtherSnTemp =
                    ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.ChkKeyParts(_dicVarBuf[strKey],
                        string.Empty, MWoInfo.WoId));
                //如果返回的数据为空则表示该序列号还没有使用过
                if (dtOtherSnTemp == null || dtOtherSnTemp.Rows.Count < 1)
                {
                    insertDtaTemp.Add(strKey, _dicVarBuf[strKey]);
                    continue;
                }
                //如果一个序列号返回的数据是多行 则表示数据重复
                if (dtOtherSnTemp != null && dtOtherSnTemp.Rows.Count > 1)
                    throw new Exception(string.Format("序列号[{0}]重复,请确认..", _dicVarBuf[strKey]));
                //如果返回了一行数据 则需要比对返回的esn是否和其他的一致 否则提示不是一组数据
                if (dtOtherSnTemp == null || dtOtherSnTemp.Rows.Count != 1) continue;

                if (string.IsNullOrEmpty(strEsnTemp))
                {
                    //判断根据序列号找到的序列号类型是否一致
                    if (
                        !string.Equals(dtOtherSnTemp.Rows[0]["sntype"].ToString(), strKey,
                            StringComparison.CurrentCultureIgnoreCase))
                        throw new Exception(string.Format("序列号[{0}]当前对应的序列号类型为{1}号,与历史数据{2}类型不符,请检查..",
                            _dicVarBuf[strKey], strKey.ToUpper(),
                            dtOtherSnTemp.Rows[0]["sntype"].ToString().ToUpper()));

                    strEsnTemp = dtOtherSnTemp.Rows[0]["esn"].ToString().ToUpper();
                    //判断流程是否正确
                    string strErr;
                    if ((strErr = ChkRoute(strEsnTemp, _mCraftName).ToUpper()) != "OK")
                        throw new Exception(string.Format("输入的序列号[{0}]流程错误\n{1},请检查..", strEsnTemp, strErr));

                    dtEsnTemp =
                        ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyPart(strEsnTemp));
                }
                else
                {
                    //判断根据序列号找到的序列号类型是否一致
                    if (
                        !string.Equals(dtOtherSnTemp.Rows[0]["sntype"].ToString(), strKey,
                            StringComparison.CurrentCultureIgnoreCase))
                        throw new Exception(string.Format("序列号[{0}]当前对应的序列号类型为{1}号,与历史数据{2}类型不符,请检查..",
                            _dicVarBuf[strKey], strKey.ToUpper(),
                            dtOtherSnTemp.Rows[0]["sntype"].ToString().ToUpper()));
                    //判断esn是否相等
                    if (
                        !string.Equals(dtOtherSnTemp.Rows[0]["esn"].ToString(), strEsnTemp,
                            StringComparison.CurrentCultureIgnoreCase))
                        throw new Exception(string.Format("数据绑定错误ESN:{0}≠{1},..",
                            strEsnTemp, dtOtherSnTemp.Rows[0]["esn"]));
                }
            }
        }

        /// <summary>
        ///     检查模板公式下的变量
        /// </summary>
        /// <param name="strEsnTemp"></param>
        /// <param name="keyTemp">模板公式下变量产生的值</param>
        /// <param name="formulasname">模板公式下变量的名称</param>
        /// <param name="dtEsnTemp"></param>
        /// <param name="dtKeyTemp"></param>
        /// <param name="lsKeyEsn"></param>
        /// <param name="dicKeysTemp"></param>
        /// <param name="insertDtaTemp"></param>
        private void ChkCurrAutoCreateInput(
            string strEsnTemp,
            string keyTemp,
            string formulasname,
            DataTable dtEsnTemp,
            out DataTable dtKeyTemp,
            out List<string> lsKeyEsn,
            out Dictionary<string, string> dicKeysTemp,
            ref Dictionary<string, string> insertDtaTemp)
        {
            dtKeyTemp = null;
            lsKeyEsn = new List<string>();
            dicKeysTemp = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(strEsnTemp))
            {
                #region strEsn的值为空

                dtKeyTemp =
                    ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.ChkKeyParts(keyTemp, formulasname,
                        MWoInfo.WoId));

                //检查esn和序列号类型是否都匹配
                if (dtKeyTemp == null || dtKeyTemp.Rows.Count < 1)
                    insertDtaTemp.Add(formulasname, keyTemp);
                else
                {
                    foreach (DataRow dr in dtKeyTemp.Rows)
                    {
                        if (!CompareArray(dr["esn"].ToString(), lsKeyEsn.ToArray())) continue;

                        lsKeyEsn.Add(dr["esn"].ToString()); //通过密码找到的esn不能用作当前esn但是需要拿来与当前esn做对比
                        //再加一个key的缓存
                        dicKeysTemp.Add(dr["esn"].ToString(), formulasname + "," + keyTemp); //保存能够找到值的key和值
                    }
                }

                #endregion
            }
            else
            {
                #region strEsn不为空

                if (dtEsnTemp == null || dtEsnTemp.Rows.Count < 1)
                {
                    #region dtEsn的数据为空

                    dtKeyTemp = ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.ChkKeyParts(keyTemp,
                        formulasname, MWoInfo.WoId));

                    //检查esn和序列号类型是否都匹配
                    if (dtKeyTemp == null || dtKeyTemp.Rows.Count < 1)
                        insertDtaTemp.Add(formulasname, keyTemp);
                    else
                    {
                        DataRow[] arrDr = dtKeyTemp.Select(string.Format("esn='{0}'", strEsnTemp));
                        if (arrDr.Length > 1)
                            throw new Exception("严重错误:密码数据重复..");
                        if (arrDr.Length < 1)
                            insertDtaTemp.Add(formulasname, keyTemp);
                    }

                    #endregion
                }
                else
                {
                    #region dtEsn数据不为空

                    //查询通过esn找到的值中是否包含当前key的类型
                    DataRow[] arrDr = dtEsnTemp.Select(string.Format("sntype='{0}'", formulasname));
                    if (arrDr.Length < 1)
                    {
                        insertDtaTemp.Add(formulasname, keyTemp);
                    }
                    else if (arrDr.Length > 1)
                    {
                        throw new Exception("同一个esn:" + strEsnTemp + "下类型名称[" + formulasname + "]重复..");
                    }
                    else
                    {
                        if (
                            !string.Equals(arrDr[0]["snval"].ToString(), keyTemp,
                                StringComparison.CurrentCultureIgnoreCase))
                            throw new Exception(string.Format("相同esn:{4}下,当前{0}={1},与历史记录{2}={3} 不相符,请检查..",
                                formulasname, keyTemp, formulasname, arrDr[0]["snval"], strEsnTemp));
                    }

                    #endregion
                }

                #endregion
            }
        }

        /// <summary>
        ///     打印
        /// </summary>
        /// <returns></returns>
        private bool MPrintLable()
        {
            ShowMsg(MLogMsgType.Incoming, string.Format("{0}{1}", "初始化设备..", DateTime.Now.ToString("HH:mm:ss")));
            if (_mLibdoc == null)
                throw new Exception("模板文件还没有初始化,请重新打开模板文件...");

            #region 局部变量

            //是否保存模板标志
            bool isSave = true;
            //缓存esn记录
            string strEsnTemp = string.Empty;
            //根据esn获取到的表
            var dtEsnTemp = new DataTable("esn");
            dtEsnTemp.Columns.Add("esn", typeof (string));
            dtEsnTemp.Columns.Add("woId", typeof (string));
            dtEsnTemp.Columns.Add("sntype", typeof (string));
            dtEsnTemp.Columns.Add("snval", typeof (string));
            //根据其他序列号或取得到的表
            DataTable dtOtherSnTemp = null;
            // 保存需要记录到数据库的内容(部分数据可能已经存在于数据库中)
            Dictionary<string, string> insertDtaTemp = new Dictionary<string, string>();
            //保存模板公式下变量的值或公式
            string[] formutemp = null;
            //保存模板公式下变量定义的长度
            int[] formulengtemp = null;

            #endregion

            try
            {
                _mLibdoc.ViewMode = enumViewMode.lppxViewModeSize;

                #region 保存公式下所有变量的内容信息

                formutemp = new string[_mLibdoc.Variables.Formulas.Count];
                formulengtemp = new int[formutemp.Length];
                Dictionary<string, int> formunameandleng = new Dictionary<string, int>();
                for (int h = 0; h < formutemp.Length; h++)
                {
                    formulengtemp[h] = _mLibdoc.Variables.Formulas.Item(h + 1).Length;
                    formutemp[h] = _mLibdoc.Variables.Formulas.Item(h + 1).Expression;
                    formunameandleng.Add(_mLibdoc.Variables.Formulas.Item(h + 1).Name.ToUpper(),
                        _mLibdoc.Variables.Formulas.Item(h + 1).Length);
                }

                #endregion

                #region 取出刷入的缓存内容

                var bFlag = ChkEsn(ref strEsnTemp, ref dtEsnTemp);
                if (!bFlag)
                {
                    ChkCurrentInput(ref strEsnTemp, ref dtOtherSnTemp, ref insertDtaTemp, ref dtEsnTemp);
                    for (int i = 0; i < _mLibdoc.Variables.FormVariables.Count; i++)
                    {
                        _mLibdoc.Variables.FormVariables.Item(i + 1).Value =
                            _dicVarBuf[_mLibdoc.Variables.FormVariables.Item(i + 1).Name];
                    }
                }
                else
                {
                    ShowMsg(MLogMsgType.Outgoing, "输入的值在系统中已经存在,正在使用系统的值填充...");
                    foreach (string strKey in _dicVarBuf.Keys)
                    {
                        //将缓存中的值赋给模板变量
                        _mLibdoc.Variables.FormVariables.Item(strKey).Value = _dicVarBuf[strKey];
                    }
                }

                #endregion

                #region 处理模板自动产生的内容

                /********在使用模板自动产生的值前先要根据手动输入的信息判断ESN是否存在，*************************
            *********如果存在那么查看存在的数据中是否有包含需要模板自动产生的序列号类型，********************
            *********如果有则使用查询到的值填充模板，反之使用模板自动产生的值，******************************
            *********然后使用模板自动产生的值与数据库比对查看是否有重复的数据,*******************************
            *********当前自动产生的数据是否有存在与另一个ESN上**********************************************/

                List<string> lsKeyEsn;
                Dictionary<string, string> dicKeysTemp = new Dictionary<string, string>();
                DataTable dtKeyTemp;

                string strErr;
                if (dtEsnTemp != null && dtEsnTemp.Rows.Count > 0)
                {
                    #region _存在esn的数据

                    for (int i = 0; i < _mLibdoc.Variables.Formulas.Count; i++)
                    {
                        string formulasname = _mLibdoc.Variables.Formulas.Item(i + 1).Name;

                        //查看esn数据中是否存在模板中的序列号类型
                        DataRow[] arrTemp = dtEsnTemp.Select(string.Format("sntype='{0}'", formulasname));
                        if (arrTemp != null && arrTemp.Length > 1) //序列号类型相同超过一个
                            throw new Exception(string.Format("严重错误:相同的esn:[{1}]下序列号类型[{0}]存在多个,请检查..",
                                formulasname, strEsnTemp));

                        if (arrTemp != null && arrTemp.Length == 1)
                        {
                            #region 找到了序号类型相同 且只有一个

                            if (CompareArray(formulasname.ToUpper()))
                            {
                                #region 属于密码类型

                                string keyTemp;
                                switch (MWoInfo.Cpwd)
                                {
                                    case WoInfo.Ecpwd.File:
                                        keyTemp = _mLibdoc.Variables.Formulas.Item(i + 1).Value;
                                        break;
                                    case WoInfo.Ecpwd.Userdef:
                                        keyTemp = _mdicAllKey[formulasname];
                                        break;
                                    default:
                                        keyTemp = GetMacKey(formulasname, _mAllKeys);
                                        break;
                                }

                                if (string.IsNullOrEmpty(keyTemp))
                                    throw new Exception(string.Format("密码:[{0}]没有计算出值,请检查该产品是否需要改密码,或没有输入MAC号...",
                                        formulasname));
                                if (arrTemp[0]["snval"].ToString().Trim() != keyTemp.Trim())
                                    throw new Exception(string.Format("系统中已经存在的值{0}={1}与当前的值不一致{0}={2}",
                                        formulasname, arrTemp[0]["snval"].ToString().Trim(), keyTemp.Trim()));

                                if (MWoInfo.Cpwd != WoInfo.Ecpwd.File)
                                {
                                    _mLibdoc.Variables.Formulas.Item(i + 1).Prefix = string.Empty;
                                    _mLibdoc.Variables.Formulas.Item(i + 1).Expression =
                                        string.Format("trim(\"{0}\")", keyTemp);
                                    _mLibdoc.Variables.Formulas.Item(i + 1).Length = formulengtemp[i];
                                }
                                _myVar.ArrAutoSerialVariableValue[i] = keyTemp;

                                #endregion
                            }
                            else
                            {
                                #region 不属于密码类型

                                ShowMsg(MLogMsgType.Warning, "系统中存在了该序列号的值,正在使用系统的值填充..");
                                isSave = false;
                                _mLibdoc.Variables.Formulas.Item(i + 1).Prefix = string.Empty;
                                _mLibdoc.Variables.Formulas.Item(i + 1).Expression = string.Format("trim(\"{0}\")",
                                    arrTemp[0]["snval"]);
                                _mLibdoc.Variables.Formulas.Item(i + 1).Length = formulengtemp[i];
                                _myVar.ArrAutoSerialVariableValue[i] = arrTemp[0]["snval"].ToString();

                                #endregion
                            }

                            #endregion
                        }
                        else
                        {
                            #region  在存在的esn数据中没有找到模板中的序列号类型

                            //查询当前模板变量的名字是否是密码类型的
                            if (CompareArray(formulasname.ToUpper()))
                            {
                                #region 属于密码类型

                                if (MWoInfo.Cpwd != WoInfo.Ecpwd.File)
                                {
                                    #region   密码由程序dll产生

                                    string keyTemp;
                                    switch (MWoInfo.Cpwd)
                                    {
                                        case WoInfo.Ecpwd.Userdef:
                                            keyTemp = _mdicAllKey[formulasname];
                                            break;
                                        default:
                                            keyTemp = GetMacKey(formulasname, _mAllKeys);
                                            break;
                                    }
                                    if (string.IsNullOrEmpty(keyTemp))
                                        throw new Exception(string.Format("密码:[{0}]没有计算出值,请检查该产品是否需要改密码,或没有输入MAC号...",
                                            formulasname));

                                    ChkCurrAutoCreateInput(strEsnTemp, keyTemp, formulasname, dtEsnTemp,
                                        out dtKeyTemp, out lsKeyEsn, out dicKeysTemp,
                                        ref insertDtaTemp);

                                    _mLibdoc.Variables.Formulas.Item(i + 1).Prefix = string.Empty;
                                    _mLibdoc.Variables.Formulas.Item(i + 1).Expression =
                                        string.Format("trim(\"{0}\")", keyTemp);
                                    _mLibdoc.Variables.Formulas.Item(i + 1).Length = formulengtemp[i];
                                    _myVar.ArrAutoSerialVariableValue[i] = keyTemp;

                                    #endregion
                                }
                                else
                                {
                                    #region     密码由模板自动产生

                                    string keyTemp = _mLibdoc.Variables.Formulas.Item(i + 1).Value;
                                    ChkCurrAutoCreateInput(strEsnTemp, keyTemp, formulasname, dtEsnTemp,
                                        out dtKeyTemp, out lsKeyEsn, out dicKeysTemp,
                                        ref insertDtaTemp);

                                    _mLibdoc.Variables.Formulas.Item(i + 1).Prefix = string.Empty;
                                    _myVar.ArrAutoSerialVariableValue[i] =
                                        _mLibdoc.Variables.Formulas.Item(i + 1).Value;
                                    _mLibdoc.Variables.Formulas.Item(i + 1).Length = formulengtemp[i];

                                    #endregion
                                }

                                #endregion
                            }
                            else
                            {
                                #region  不是密码类型

                                var valTemp = _mLibdoc.Variables.Formulas.Item(_myVar.ArrAutoSerialVariableName[i])
                                    .Value;
                                if (valTemp.Length !=
                                    formunameandleng[_myVar.ArrAutoSerialVariableName[i].ToUpper()])
                                    throw new Exception("模板产生的序列号长度与设置的长度不符!!");

                                if (dtEsnTemp != null && dtEsnTemp.Rows.Count > 0)
                                {
                                    #region 如果dtEsn数据有值，则比对模板中的序列号类型是否在dtEsn数据中

                                    DataRow[] arrDr = dtEsnTemp.Select(string.Format("sntype='{0}'", formulasname));
                                    if (arrDr != null && arrDr.Length > 1)
                                        throw new Exception("同一个esn:" + strEsnTemp + "类型" + formulasname +
                                                            "存在多次,请修正..");
                                    if (arrDr != null && arrDr.Length == 1)
                                    {
                                        if (
                                            !string.Equals(arrDr[0]["snval"].ToString(), valTemp,
                                                StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            throw new Exception(string.Format(
                                                "esn:{0}当前输入的{1}={2}与历史数据{1}={3}不相符,请检查..",
                                                strEsnTemp, formulasname, valTemp.ToUpper(),
                                                arrDr[0]["snval"].ToString().ToUpper()));
                                        }
                                        continue;
                                    }

                                    #endregion
                                }

                                DataTable atoSnTemp =
                                    ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.ChkKeyParts(valTemp,
                                        string.Empty, MWoInfo.WoId));
                                if (atoSnTemp != null && atoSnTemp.Rows.Count > 1)
                                    throw new Exception("严重错误:模板自动产生的序列号[" + valTemp + "]重复,请检查..");
                                if (atoSnTemp == null || atoSnTemp.Rows.Count < 1)
                                    insertDtaTemp.Add(formulasname, valTemp);
                                else
                                {
                                    if (string.IsNullOrEmpty(strEsnTemp))
                                    {
                                        ShowMsg(MLogMsgType.Warning, "提示:ESN为空，请检查数据的准确性..");
                                        if (atoSnTemp == null || atoSnTemp.Rows.Count != 1) continue;
                                        insertDtaTemp.Add(formulasname, valTemp);
                                        strEsnTemp = atoSnTemp.Rows[0]["esn"].ToString();
                                        //检查流程
                                        if ((strErr = ChkRoute(strEsnTemp, _mCraftName).ToUpper()) !=
                                            "OK")
                                            throw new Exception(strErr);

                                        DataTable mdt =
                                            ReleaseData.arrByteToDataTable(
                                                refWebtWipKeyPart.Instance.GetWipKeyPart(strEsnTemp));
                                        //比对通过模板自动产生的值在数据库中找到的内容与手动输入的值是否相符
                                        foreach (string item in _dicVarBuf.Keys)
                                        {
                                            DataRow[] aDr = mdt.Select(string.Format("sntype='{0}'", item));
                                            //如果没有找到则表示还没有记录过,如果找到了数据那么就需要比对值是否相同
                                            if (aDr != null && aDr.Length > 1)
                                                throw new Exception("严重错误:序列号类型名称" + item + "重复");
                                            if (aDr == null || aDr.Length != 1) continue;

                                            if (
                                                !string.Equals(aDr[0]["snval"].ToString(), _dicVarBuf[item],
                                                    StringComparison.CurrentCultureIgnoreCase))
                                                throw new Exception(string.Format("序列号{0}:当前值与历史值不相同:{1}≠{2}",
                                                    item, _dicVarBuf[item], aDr[0]["snval"]));
                                        }
                                    }
                                    else
                                    {
                                        if (atoSnTemp == null || atoSnTemp.Rows.Count != 1) continue;

                                        if (atoSnTemp.Rows[0]["esn"].ToString().ToUpper() != strEsnTemp)
                                        {
                                            throw new Exception(
                                                string.Format("序列号绑定错误:序列号[{0}]已被其他的产品esn:[{1}]使用过,不能再使用",
                                                    valTemp, atoSnTemp.Rows[0]["esn"].ToString().ToUpper()));
                                        }
                                    }
                                }

                                #endregion
                            }

                            #endregion
                        }
                    }

                    #endregion
                }
                else
                {
                    #region 不存在esn数据

                    for (int i = 0; i < _mLibdoc.Variables.Formulas.Count; i++)
                    {
                        #region  取出模板里公式下所有变量的值

                        string formulasname = _mLibdoc.Variables.Formulas.Item(i + 1).Name;
                        //查询当前模板变量的名字是否是密码类型的
                        if (CompareArray(formulasname.ToUpper()))
                        {
                            #region 属于密码类型

                            if (MWoInfo.Cpwd != WoInfo.Ecpwd.File)
                            {
                                #region   密码由程序dll产生

                                string keyTemp;
                                switch (MWoInfo.Cpwd)
                                {
                                    case WoInfo.Ecpwd.Userdef:
                                        keyTemp = _mdicAllKey[formulasname];
                                        break;
                                    default:
                                        keyTemp = GetMacKey(formulasname, _mAllKeys);
                                        break;
                                }
                                if (string.IsNullOrEmpty(keyTemp))
                                    throw new Exception(string.Format("密码:[{0}]没有计算出值,请检查该产品是否需要改密码,或没有输入MAC号...",
                                        formulasname));

                                ChkCurrAutoCreateInput(strEsnTemp, keyTemp, formulasname, dtEsnTemp,
                                    out dtKeyTemp, out lsKeyEsn, out dicKeysTemp,
                                    ref insertDtaTemp);

                                _mLibdoc.Variables.Formulas.Item(i + 1).Prefix = string.Empty;
                                _mLibdoc.Variables.Formulas.Item(i + 1).Expression = string.Format("trim(\"{0}\")",
                                    keyTemp);
                                _mLibdoc.Variables.Formulas.Item(i + 1).Length = formulengtemp[i];
                                _myVar.ArrAutoSerialVariableValue[i] = keyTemp;

                                #endregion
                            }
                            else
                            {
                                #region     密码由模板自动产生

                                string keyTemp = _mLibdoc.Variables.Formulas.Item(i + 1).Value;
                                ChkCurrAutoCreateInput(strEsnTemp, keyTemp, formulasname, dtEsnTemp,
                                    out dtKeyTemp, out lsKeyEsn, out dicKeysTemp,
                                    ref insertDtaTemp);

                                _mLibdoc.Variables.Formulas.Item(i + 1).Prefix = string.Empty;
                                _myVar.ArrAutoSerialVariableValue[i] =
                                    _mLibdoc.Variables.Formulas.Item(i + 1).Value;
                                _mLibdoc.Variables.Formulas.Item(i + 1).Length = formulengtemp[i];

                                #endregion
                            }

                            #endregion
                        }
                        else
                        {
                            #region  不是密码类型

                            var valTemp = _mLibdoc.Variables.Formulas.Item(i + 1).Value;
                            if (valTemp.Length !=
                                formunameandleng[_mLibdoc.Variables.Formulas.Item(i + 1).Name.ToUpper()])
                                throw new Exception("模板产生的序列号长度与设置的长度不符!!");
                            if (dtEsnTemp != null && dtEsnTemp.Rows.Count > 0)
                            {
                                ShowMsg(MLogMsgType.Warning, "提示:检查数据准确性");

                                #region 如果esn数据存在

                                DataRow[] arrDr = dtEsnTemp.Select(string.Format("sntype='{0}'", formulasname));
                                if (arrDr != null && arrDr.Length > 1)
                                    throw new Exception("同一个esn:" + strEsnTemp + "类型" + formulasname + "存在多次,请修正..");
                                if (arrDr != null && arrDr.Length == 1)
                                {
                                    if (arrDr[0]["snval"].ToString().ToUpper() != valTemp.ToUpper())
                                    {
                                        throw new Exception(string.Format("esn:{0}当前输入的{1}={2}与历史数据{1}={3}不相符,请检查..",
                                            strEsnTemp, formulasname, valTemp.ToUpper(),
                                            arrDr[0]["snval"].ToString().ToUpper()));
                                    }
                                    continue;
                                }

                                #endregion
                            }

                            DataTable atoSnTemp =
                                ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.ChkKeyParts(valTemp,
                                    string.Empty, MWoInfo.WoId));
                            if (atoSnTemp != null && atoSnTemp.Rows.Count > 1)
                                throw new Exception("严重错误:模板自动产生的序列号[" + valTemp + "]重复,请检查..");
                            if (atoSnTemp == null || atoSnTemp.Rows.Count < 1)
                                insertDtaTemp.Add(formulasname, valTemp);
                            else
                            {
                                if (string.IsNullOrEmpty(strEsnTemp))
                                {
                                    #region strEsn为空

                                    if (atoSnTemp == null || atoSnTemp.Rows.Count != 1) continue;

                                    insertDtaTemp.Add(formulasname, valTemp);
                                    strEsnTemp = atoSnTemp.Rows[0]["esn"].ToString();
                                    if ((strErr = ChkRoute(strEsnTemp, _mCraftName).ToUpper()) != "OK")
                                        throw new Exception(strErr);

                                    DataTable mdt =
                                        ReleaseData.arrByteToDataTable(
                                            refWebtWipKeyPart.Instance.GetWipKeyPart(strEsnTemp));
                                    //比对通过模板自动产生的值在数据库中找到的内容与手动输入的值是否相符
                                    foreach (string item in _dicVarBuf.Keys)
                                    {
                                        DataRow[] aDr = mdt.Select(string.Format("sntype='{0}'", item));
                                        //如果没有找到则表示还没有记录过,如果找到了数据那么就需要比对值是否相同
                                        if (aDr != null && aDr.Length > 1)
                                            throw new Exception("严重错误:序列号类型名称" + item + "重复");
                                        if (aDr == null || aDr.Length != 1) continue;
                                        if (
                                            !string.Equals(aDr[0]["snval"].ToString(), _dicVarBuf[item],
                                                StringComparison.CurrentCultureIgnoreCase))
                                            throw new Exception(string.Format("序列号{0}:当前值与历史值不相同:{1}≠{2}",
                                                item, _dicVarBuf[item], aDr[0]["snval"]));
                                    }

                                    #endregion
                                }
                                else
                                {
                                    if (atoSnTemp != null && atoSnTemp.Rows.Count == 1)
                                    {
                                        if (atoSnTemp.Rows[0]["esn"].ToString().ToUpper() != strEsnTemp)
                                        {
                                            throw new Exception(
                                                string.Format("序列号绑定错误:序列号[{0}]已被其他的产品esn:[{1}]使用过,不能再使用",
                                                    valTemp, atoSnTemp.Rows[0]["esn"].ToString().ToUpper()));
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        #endregion
                    }

                    #endregion
                }

                #endregion

                //如果所有的变量都循环过了还没有找到esn
                if (string.IsNullOrEmpty(strEsnTemp))
                {
                    throw new Exception("没有发现任何esn序列号,请检查..");
                }

                foreach (string item in dicKeysTemp.Keys)
                {
                    if (!string.Equals(item, strEsnTemp, StringComparison.CurrentCultureIgnoreCase))
                    {
                        insertDtaTemp.Add(dicKeysTemp[item].Split(',')[0], dicKeysTemp[item].Split(',')[1]);
                    }
                }

                if (!CHECK_PRODUCT_LINE())
                    throw new Exception("请切换线别");

                ShowMsg(MLogMsgType.Outgoing, "正在记录数据..");
                IList<IDictionary<string, object>> lsDic = new List<IDictionary<string, object>>();
                foreach (string item in insertDtaTemp.Keys)
                {
                    //再一次对插入数据库的数据进行比对是否输入与当前工单的范围
                    if (!CompareArray(item.ToUpper()))
                    {
                        if (!CompareSerialnumber(MWoInfo.WoId, insertDtaTemp[item], item))
                            throw new Exception(string.Format("序列号{0}:{1}不在工单设置范围内,请检查..", item, insertDtaTemp[item]));
                    }
                    _dic = new Dictionary<string, object>
                    {
                        {"ESN", strEsnTemp},
                        {"SNTYPE", item},
                        {"SNVAL", insertDtaTemp[item]},
                        {"WOID", MWoInfo.WoId},
                        {"STATION", _mCraftName},
                        {"KPNO", "NA"}
                    };
                    lsDic.Add(_dic);
                }
                ShowMsg(MLogMsgType.Incoming, "数据记录完成");

                #region 重复打印不记录过站信息 和 序列号匹配信息 2013-4-13

                if (!_mRprint)
                {
                    ShowMsg(MLogMsgType.Outgoing, "正在保存过站信息..");

                    #region 过站和记录产能

                    if (lsDic.Count > 0)
                    {
                        strErr =
                            refWebtWipTracking.Instance.InsertWipKeyParts(MapListConverter.ListDictionaryToJson(lsDic));
                        strErr = string.IsNullOrEmpty(strErr) ? "OK" : strErr;
                        if (strErr != "OK")
                            throw new Exception(strErr);

                        string strSn = "NA";
                        string strKt = "NA";
                        string strPcbasn = "NA";
                        string strSpmac = "NA";
                        string strKcode = "NA";
                        foreach (var objects in lsDic)
                        {
                            var dic = (Dictionary<string, object>) objects;
                            if (dic["SNTYPE"].ToString() == "SN")
                                strSn = dic["SNVAL"].ToString();
                            if (dic["SNTYPE"].ToString() == "KT")
                                strKt = dic["SNVAL"].ToString();
                            if (dic["SNTYPE"].ToString() == "PCBASN")
                                strPcbasn = dic["SNVAL"].ToString();
                            if (dic["SNTYPE"].ToString() == "SPMAC")
                                strSpmac = dic["SNVAL"].ToString();
                            if (dic["SNTYPE"].ToString() == "KCODE")
                                strKcode = dic["SNVAL"].ToString();
                        }
                        Fill_DatagridView(strEsnTemp, strSn, strKt, strPcbasn, strSpmac, strKcode);
                    }
                    strErr = refWebtPublicStoredproc.Instance.SP_TEST_MAIN_ONLY(strEsnTemp, _mCraftName,
                        _mUserInfo.UserId + "-" + _mUserInfo.Pwd, "NA", _lineName);
                    if (strErr.ToUpper() != "OK")
                        throw new Exception(strErr + "\n过站失败!!");
                    ShowMsg(MLogMsgType.Incoming, "过站信息保存完成");

                    #endregion
                }
                else
                {
                    //记录重复打印记录
                    _dic = new Dictionary<string, object>
                    {
                        {"USERID", StrEnaPwd.Split('-')[0]},
                        {"PRG_NAME", "REPEATPRINT"},
                        {"ACTION_TYPE", "PRINT"},
                        {"ACTION_DESC", "PRINT: " + strEsnTemp}
                    };
                    refWebRecodeSystemLog.Instance.InsertSystemLog(MapListConverter.DictionaryToJson(_dic));
                    StrEnaPwd = string.Empty;
                }

                #endregion

                for (int x = 0; x < _myVar.ArrAutoSerialVariableName.Length; x++)
                {
                    FillTextBOX(_arrTextboxReadOnly[x],
                        _mLibdoc.Variables.Formulas.Item(_arrTextboxReadOnly[x].Name).Value);
                }
                ShowMsg(MLogMsgType.Outgoing, "正在打印标签....");
                _mLibdoc.PrintDocument(_mPrintNumber);
                ShowMsg(MLogMsgType.Incoming, "标签打印完成");
                ShowMsg(MLogMsgType.Outgoing, "正在初始化模板....");
                for (int y = 0; y < formutemp.Length; y++)
                {
                    _mLibdoc.Variables.Formulas.Item(y + 1).Prefix = "";
                    _mLibdoc.Variables.Formulas.Item(y + 1).Expression = formutemp[y];
                    _mLibdoc.Variables.Formulas.Item(y + 1).Length = formulengtemp[y];
                }
                if (!_mRprint)
                {
                    if (isSave)
                        _mLibdoc.Save();
                }
                ShowMsg(MLogMsgType.Incoming, "全部完成");
                return true;
            }
            catch (Exception ex)
            {
                ShowMsg(MLogMsgType.Outgoing, "正在初始化模板....");
                for (int y = 0; y < formutemp.Length; y++)
                {
                    _mLibdoc.Variables.Formulas.Item(y + 1).Prefix = "";
                    _mLibdoc.Variables.Formulas.Item(y + 1).Expression = formutemp[y];
                    _mLibdoc.Variables.Formulas.Item(y + 1).Length = formulengtemp[y];
                }
                ShowMsg(MLogMsgType.Error, ex.Message);
                return false;
            }
            finally
            {
                if (_mRprint)
                {
                    _mLibdoc.Close(false);
                    _mlppx.Documents.CloseAll(false);
                    _mlppx.Quit();
                    _mlppx = new ApplicationClass();
                    _mLibdoc = _mlppx.Documents.Open(_mPrintFileName);
                    _mLibdoc.Activate();
                    ShowMsg(MLogMsgType.Incoming, "关闭重复打印");
                }
                _mRprint = false;
            }
        }

        /// <summary>
        ///     根据密码的名称获取根据mac计算出来的密码值
        /// </summary>
        /// <param name="strName">密码名称</param>
        /// <param name="keys">密码值列表</param>
        /// <returns>返回对应的密码值</returns>
        private static string GetMacKey(string strName, List<string> keys)
        {
            string key = string.Empty;
            if (keys == null || keys.Count < 1)
                return key;
            try
            {
                switch (strName.ToUpper())
                {
                    case "SSID":
                        key = keys.ToArray()[0];
                        break;
                    case "WEPKEY":
                        key = keys.ToArray()[1];
                        break;
                    case "PIN":
                        key = keys.ToArray()[2];
                        break;
                    case "DEK":
                        key = keys.ToArray()[3];
                        break;
                    case "AES":
                        key = keys.ToArray()[4];
                        break;
                }
                return key;
            }
            catch
            {
                throw new Exception("Key名称有误: FrmMain 952");
            }
        }

        /// <summary>
        ///     检查系统进程中是否存在指定的进程
        /// </summary>
        /// <param name="prcname">进程名称</param>
        /// <returns>存在则返回真</returns>
        private static bool Checkprocessisrun(string prcname)
        {
            Process[] prc = Process.GetProcessesByName(prcname);
            return prc.Length >= 1;
        }

        /// <summary>
        ///     结束指定的进程
        /// </summary>
        /// <param name="prcname"></param>
        private static void Closeproc(string prcname)
        {
            Process[] prc = Process.GetProcessesByName(prcname.Substring(0, prcname.LastIndexOf('.')));
            if (prc.Length <= 0) return;
            foreach (Process pc in prc)
            {
                pc.Kill();
            }
        }

        /// <summary>
        ///     杀死进程(目前写死为lppa.exe)
        /// </summary>
        private static void KillAllProcess()
        {
            Process cmd = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    Arguments = "/c taskkill /im lppa.exe /f",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };
            cmd.Start();
            cmd.Close();
        }

        /// <summary>
        ///     显示消息函数
        /// </summary>
        /// <param name="msgtype"></param>
        /// <param name="msg"></param>
        public void ShowMsg(MLogMsgType msgtype, string msg)
        {
            try
            {
                rtbmsg.Invoke(new EventHandler(delegate
                {
                    rtbmsg.TabStop = false;
                    rtbmsg.SelectedText = string.Empty;
                    rtbmsg.SelectionFont = new Font(rtbmsg.SelectionFont, FontStyle.Bold);
                    rtbmsg.SelectionColor = _mLogMsgTypeColor[(int) msgtype];
                    rtbmsg.AppendText(msg + "\n");
                    rtbmsg.ScrollToCaret();

                    if (_mLogMsgTypeColor[(int) msgtype] == Color.Red)
                    {
                        SendBuzz();
                    }
                }));
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     显示进度条
        /// </summary>
        /// <param name="ival"></param>
        private void ShowProgressbar(int ival)
        {
            statusStrip1.Invoke(new EventHandler(delegate { runprogbar.Value = ival; }));
        }

        /// <summary>
        ///     设置进度条的最大值
        /// </summary>
        /// <param name="ival"></param>
        private void SetProgressbarMaxValue(int ival)
        {
            statusStrip1.Invoke(new EventHandler(delegate { runprogbar.Maximum = ival; }));
        }

        /// <summary>
        ///     初始化自定义控件的内容（针对其他标签打印的界面控件）
        /// </summary>
        private void InitCtlPannel()
        {
            gpcartonprint.Invoke(new EventHandler(delegate
            {
                foreach (Control tb in gpotherprint.Controls)
                {
                    if (tb is MyTextBox)
                        tb.Text = "";
                }
            }));
        }

        /// <summary>
        ///     将打开的模板变量信息记录到内存中
        /// </summary>
        /// <param name="fileName"></param>
        private void InitLab(string fileName)
        {
            _myVar.DicVarLen = new Dictionary<string, int>();
            List<string> vname = new List<string>();
            List<string> vallname = new List<string>();
            List<string[]> arrname = new List<string[]>();
            _lsAllVarName.Clear();
            _myVar.DicVarLen.Clear();
            string vtemp = string.Empty;
            try
            {
                _mLibdoc = _mlppx.Documents.Open(fileName);
                _mLibdoc.ViewMode = enumViewMode.lppxViewModeSize;

                int formvariablecount = _mLibdoc.Variables.FormVariables.Count;

                #region xxx【_formvariablecount】

                for (int cu = 1; cu <= _mLibdoc.Variables.FormVariables.Count; cu++) //读取的是填充器下的变量
                {
                    if (Regex.Replace(_mLibdoc.Variables.FormVariables.Item(cu).Name, @"\d", "").Trim() != vtemp)
                    {
                        if (vname.IndexOf(
                            Regex.Replace(_mLibdoc.Variables.FormVariables.Item(cu).Name, @"\d", "").Trim(),
                            0, vname.Count) == -1)
                        {
                            vname.Add(Regex.Replace(_mLibdoc.Variables.FormVariables.Item(cu).Name, @"\d", "").Trim());
                        }
                    }
                    vallname.Add(vtemp = Regex.Replace(_mLibdoc.Variables.FormVariables.Item(cu).Name, @"\d", "").Trim());
                }

                foreach (string str in vname)
                {
                    if (str.ToUpper() != "CARTONNUMBER")
                        arrname.Add(vallname.FindAll(ss => ss == str).ToArray());
                }

                for (int xxx = 0; xxx < arrname.Count; xxx++)
                {
                    if (arrname[xxx].Length != arrname[arrname.Count - 1].Length)
                    {
                        throw new Exception("模板变量数量设置不对称,请修正..");
                    }
                }
                _miCatonBoxTotal = arrname[0].Length;

                #endregion

                _myVar.ArrVariable = new string[formvariablecount];
                _myVar.ArrVariableCount = new int[formvariablecount];
                _myVar.ArrAutoSerialVariableName = new string[_mLibdoc.Variables.Formulas.Count]; //读取的是公式下的变量
                _myVar.ArrAutoSerialVariableValue = new string[_mLibdoc.Variables.Formulas.Count];

                for (int i = 0; i < _myVar.ArrVariableCount.Length; i++)
                {
                    _lsAllVarName.Add(_myVar.ArrVariable[i] = _mLibdoc.Variables.FormVariables.Item(i + 1).Name);
                    _myVar.ArrVariableCount[i] = _mLibdoc.Variables.FormVariables.Item(i + 1).Length;
                    _myVar.DicVarLen.Add(_mLibdoc.Variables.FormVariables.Item(i + 1).Name,
                        _mLibdoc.Variables.FormVariables.Item(i + 1).Length);
                }

                for (int x = 0; x < _myVar.ArrAutoSerialVariableName.Length; x++)
                {
                    _lsAllVarName.Add(
                        _myVar.ArrAutoSerialVariableName[x] = _mLibdoc.Variables.Formulas.Item(x + 1).Name);
                    _myVar.ArrAutoSerialVariableValue[x] = _mLibdoc.Variables.Formulas.Item(x + 1).Value;
                }

                _dicVarBuf.Clear();
                _mLibdoc.CopyToClipboard();
                ShowPicture(Clipboard.GetImage());
            }
            catch (Exception ex)
            {
                _mLibdoc = null;
                ShowMsg(MLogMsgType.Error, "控件及变量初始化失败\n" + ex.Message);
            }
        }

        private void ShowPicture(Image img)
        {
            pictureBox1.Invoke(new EventHandler(delegate
            {
                pictureBox1.Image = img;
                pictureBox1.Refresh();
            }));
        }

        /// <summary>
        ///     加载工单所有的序列号区间到本地数据库以减轻服务器的压力
        /// </summary>
        private void DownloadWoSnRule()
        {
            ShowMsg(MLogMsgType.Warning, "正在加载工单序列号区间..");
            cdbAccess ass = new cdbAccess();
            ass.ExecuteSqlCommand("delete from wosnrule");
            DataTable dtwoSnrule =
                ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoSnRule(MWoInfo.WoId, string.Empty));
            SetProgressbarMaxValue(dtwoSnrule.Rows.Count);
            int i = 0;
            foreach (DataRow dr in dtwoSnrule.Rows)
            {
                i++;
                string sql =
                    $"insert into wosnrule(poid,woId,sntype,snstart,snend,snprefix,snpostfix,ver,reve,usenum) values('{"NA"}','{dr["woId"]}','{dr["sntype"]}','{dr["snstart"]}','{dr["snend"]}','{dr["snprefix"]}','{dr["snpostfix"]}','{dr["ver"]}','{dr["reve"]}','{dr["usenum"]}')";
                ass.ExecuteSqlCommand(sql);
                ShowProgressbar(i);
            }
            ShowMsg(MLogMsgType.Warning, "工单序列号区间加载完成.");
        }

        /// <summary>
        ///     显示统计信息
        /// </summary>
        /// <param name="currentValue">当前的值</param>
        /// <param name="total">总数</param>
        private void ShowStation(string currentValue, string total)
        {
            lbcurentstation.Invoke(new EventHandler(delegate
            {
                lbcurentstation.Text = string.Format("{0}/{1}", currentValue, total);
                lbcurentstation.Refresh();
            }));
        }

        /// <summary>
        ///     根据模板内容显示对应的控件
        /// </summary>
        /// <param name="myVar"></param>
        private void ShowControl(MyVariable myVar)
        {
            gpcartonprint.Invoke(new EventHandler(delegate
            {
                gpotherprint.Controls.Clear();

                #region Add controls to panel

                _arrTextbox = new MyTextBox[myVar.ArrVariable.Length];
                _arrLabel = new Label[myVar.ArrVariableCount.Length];

                _arrTextboxReadOnly = new MyTextBox[myVar.ArrAutoSerialVariableName.Length];
                _arrLabelReadOnly = new Label[myVar.ArrAutoSerialVariableValue.Length];

                const int rowHeight = 37;

                Point textBoxPoint = new Point(220, 24);
                Point labelPoint = new Point(80, 30);

                Size textBoxLocation = new Size(100, 20);
                Size labelLocation = new Size(5, 20);

                Point readOnlyTextBoxPoint = new Point(220, 24);
                Point readOnlyLabelPoint = new Point(80, 30);

                Size readOnlyTextBoxLocation = new Size(100, gpotherprint.Height - 45);
                Size readOnlyLabelLocation = new Size(5, gpotherprint.Height - 45);

                Point checkBoxPoint = new Point(80, 25);
                Size checkBoxLoction = new Size(430, 250);
                _showDataChk = new CheckBox
                {
                    BackColor = Color.Transparent,
                    Location = new Point(checkBoxLoction),
                    Size = new Size(checkBoxPoint),
                    Text = Resources.ShowData
                };
                _showDataChk.CheckedChanged += ShowDataChk_CheckedChanged;

                #endregion

                try
                {
                    #region xxxxx

                    for (int x = 0; x < myVar.ArrVariable.Length; x++)
                    {
                        _arrTextbox[x] = new MyTextBox();
                        _arrLabel[x] = new Label {BackColor = Color.Transparent};
                        _arrTextbox[x].Location = new Point(textBoxLocation);
                        _arrTextbox[x].Font = new Font("宋体", 15);
                        _arrLabel[x].Location = new Point(labelLocation);
                        _arrLabel[x].Font = new Font("宋体", 13, FontStyle.Bold);
                        _arrTextbox[x].Size = new Size(textBoxPoint);
                        _arrLabel[x].Size = new Size(labelPoint);
                        _arrTextbox[x].TabIndex = x + 5;
                        _arrTextbox[x].KeyDown += textbox_KeyDown;
                        _arrLabel[x].Text = myVar.ArrVariable[x] + Resources.Colon;
                        _arrLabel[x].TextAlign = ContentAlignment.MiddleRight;
                        textBoxLocation.Height += rowHeight;
                        labelLocation.Height += rowHeight;
                    }
                    gpotherprint.Controls.Add(_showDataChk);
                    // ReSharper disable once CoVariantArrayConversion
                    gpotherprint.Controls.AddRange(_arrTextbox);
                    // ReSharper disable once CoVariantArrayConversion
                    gpotherprint.Controls.AddRange(_arrLabel);
                    _arrTextbox[0].Focus();

                    #endregion

                    #region xxxxxx

                    for (int i = 0; i < myVar.ArrAutoSerialVariableName.Length; i++)
                    {
                        _arrTextboxReadOnly[i] = new MyTextBox
                        {
                            Location = new Point(readOnlyTextBoxLocation),
                            Font = new Font("宋体", 15),
                            Size = new Size(readOnlyTextBoxPoint),
                            Name = myVar.ArrAutoSerialVariableName[i],
                            Text = myVar.ArrAutoSerialVariableValue[i],
                            ReadOnly = true
                        };
                        readOnlyTextBoxLocation.Height += -35;

                        _arrLabelReadOnly[i] = new Label
                        {
                            BackColor = Color.Transparent,
                            Location = new Point(readOnlyLabelLocation),
                            Font = new Font("宋体", 13, FontStyle.Bold),
                            Size = new Size(readOnlyLabelPoint),
                            Text = myVar.ArrAutoSerialVariableName[i] + Resources.Colon,
                            TextAlign = ContentAlignment.MiddleRight
                        };
                        readOnlyLabelLocation.Height += -35;
                    }

                    #endregion

                    // ReSharper disable once CoVariantArrayConversion
                    gpotherprint.Controls.AddRange(_arrTextboxReadOnly);
                    // ReSharper disable once CoVariantArrayConversion
                    gpotherprint.Controls.AddRange(_arrLabelReadOnly);
                }
                catch (Exception ex)
                {
                    ShowMsg(MLogMsgType.Error, "控件显示错误\n" + ex.Message);
                }
            }));
        }

        private void ShowDataChk_CheckedChanged(object sender, EventArgs e)
        {
            if (_showDataChk.Checked)
            {
                _bShowData = true;
                ShowOtherData(MWoInfo.WoId, _bShowData);
            }
            else
                _bShowData = false;
        }

        /// <summary>
        ///     显示模板文件路径
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="path"></param>
        private void ShowLibFilePath(string filename, string path)
        {
            lblibfilename.Invoke(new EventHandler(delegate
            {
                lblibfilename.Text = filename;
                lb_showmfpath.Text = path;
                lblibfilename.Refresh();
            }));
        }

        /// <summary>
        ///     打开模板文件
        /// </summary>
        /// <returns></returns>
        private void OpenLabFile(string filepath)
        {
            if (_miasyncresult != null && !_miasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "程序正在初始化,请稍候...");
                return;
            }

            try
            {
                InitCtlPannel();
                if (_mLibdoc != null)
                {
                    _mLibdoc.Close(_mbIsSaveLabFile);
                    _mlppx.Documents.CloseAll(_mbIsSaveLabFile);
                    _mlppx.Quit();
                    _mLibdoc = null;
                }
                if (_mlppx != null)
                {
                    try
                    {
                        _mlppx.Quit();
                    }
                    catch
                    {
                        // ignored
                    }
                }
                OpenFileDialog ofd = new OpenFileDialog
                {
                    Title = Resources.ChooseModelFile,
                    Filter = Resources.Filter,
                    InitialDirectory = Application.StartupPath
                };
                if (string.IsNullOrEmpty(filepath) && ofd.ShowDialog() != DialogResult.OK) return;

                _mPrintFileName = !string.IsNullOrEmpty(filepath) ? filepath : ofd.FileName;
                _msProductText = Path.GetFileNameWithoutExtension(_mPrintFileName);
                ShowLibFilePath(string.Format("[{0}] 标签打印", _msProductText), _mPrintFileName);
                _mlppx = new ApplicationClass();

                InitLab(_mPrintFileName);
                switch (_mTabItem.Name)
                {
                    case "tabItem1":
                        dgvdata.ContextMenuStrip = null;
                        ShowControl(_myVar);
                        ShowStation("0", MWoInfo.Qty.ToString());
                        break;

                    case "tabItem2":
                        dgvdata.ContextMenuStrip = contextMenuStrip2;
                        SetBtOkState(true);
                        bt_ok.Enabled = true;
                        break;

                    default:
                        throw new Exception("警告:不明选项..");
                }
            }
            catch (Exception ex)
            {
                ShowMsg(MLogMsgType.Error, ex.Message + ": FrmMain 2233");
            }
        }

        /// <summary>
        ///     打开模板前判断生产信息是否填写完成
        /// </summary>
        /// <returns></returns>
        private bool CheckSelectIsOk()
        {
            #region 判定是否选择

            if (string.IsNullOrEmpty(tbwoid.Text))
            {
                tbwoid.Focus();
                ShowMsg(MLogMsgType.Warning, "请设定生产信息");
                return false;
            }
            if (string.IsNullOrEmpty(cbstationId.Text))
            {
                cbstationId.Focus();
                ShowMsg(MLogMsgType.Warning, "请设定生产信息");
                return false;
            }
            if (string.IsNullOrEmpty(cblineId.Text))
            {
                cblineId.Focus();
                ShowMsg(MLogMsgType.Warning, "请设定生产信息");
                return false;
            }
            if (string.IsNullOrEmpty(MWoInfo.WoId))
            {
                ShowMsg(MLogMsgType.Warning, "没有找到工单号");
                return false;
            }
            if (!string.IsNullOrEmpty(MWoInfo.RoutgroupId)) return true;

            ShowMsg(MLogMsgType.Warning, "没有发现工单的流程编号,请重新设置");
            return false;

            #endregion
        }

        /// <summary>
        ///     保存当前配置
        /// </summary>
        private void SaveConfig()
        {
            try
            {
                if (MWoInfo == null)
                    return;

                #region WOENTITY

                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "WOID", MWoInfo.WoId, _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "PARTNUMBER", MWoInfo.Partnumber,
                    _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "INPUTGROUP", MWoInfo.Inputgroup,
                    _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "OUTPUTGROUP", MWoInfo.Outputgroup,
                    _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "POID", MWoInfo.PoId, _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "QTY", MWoInfo.Qty.ToString(), _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "ROUTGROUPID", MWoInfo.RoutgroupId,
                    _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "BOMNUMBER", MWoInfo.Bomnumber,
                    _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "BOMVER", MWoInfo.Bomver, _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "WOSTATE", MWoInfo.Wostate.ToString(),
                    _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "CPWD", MWoInfo.Cpwd.ToString(),
                    _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "WOTYPE", MWoInfo.Wotype, _sfisIniFilePath);

                #endregion

                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "LINE", cblineId.Text, _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "CRAFTNAME", lbstationname.Text,
                    _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "CRAFTID", _mCraftName, _sfisIniFilePath);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "PrintQTY",
                    numPrintQty.Value.ToString(CultureInfo.InvariantCulture),
                    _sfisIniFilePath);
            }
            catch (Exception ex)
            {
                throw new Exception("配置文件保存失败" + ex.Message + ",请检查!!");
            }
        }

        /// <summary>
        ///     读取上次退出时的配置信息
        /// </summary>
        private void ReadConfig()
        {
            try
            {
                #region WOENTITY

                MWoInfo = new WoInfo
                {
                    WoId = ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "WOID", _sfisIniFilePath),
                    Partnumber = ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "PARTNUMBER", _sfisIniFilePath),
                    Inputgroup = ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "INPUTGROUP", _sfisIniFilePath),
                    Outputgroup = ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "OUTPUTGROUP", _sfisIniFilePath),
                    PoId = ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "POID", _sfisIniFilePath),
                    Qty =
                        int.Parse(
                            string.IsNullOrEmpty(ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "QTY",
                                _sfisIniFilePath))
                                ? "0"
                                : ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "QTY", _sfisIniFilePath)),
                    RoutgroupId = ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "ROUTGROUPID", _sfisIniFilePath),
                    Bomnumber = ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "BOMNUMBER", _sfisIniFilePath),
                    Bomver = ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "BOMVER", _sfisIniFilePath),
                    Wostate =
                        int.Parse(
                            string.IsNullOrEmpty(ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "WOSTATE",
                                _sfisIniFilePath))
                                ? "0"
                                : ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "WOSTATE", _sfisIniFilePath)),
                    Cpwd = Getcpwd(ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "CPWD", _sfisIniFilePath)),
                    Wotype = ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "WOTYPE", _sfisIniFilePath)
                };

                #endregion

                _mCraftName = ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "CRAFTNAME", _sfisIniFilePath);
                cblineId.SelectedIndex =
                    cblineId.Items.IndexOf(ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "LINE", _sfisIniFilePath));
                numPrintQty.Value =
                    Convert.ToDecimal(
                        string.IsNullOrEmpty(ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "PrintQTY",
                            _sfisIniFilePath))
                            ? "0"
                            : ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "PrintQTY", _sfisIniFilePath));

                #region LabConfig

                try
                {
                    _labDir = ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM_WIFI", "LABPATH", _sfisIniFilePath);
                    if (string.IsNullOrEmpty(_labDir))
                        _labDir = "D";
                }
                catch
                {
                    ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM_WIFI", "LABPATH", "D", _sfisIniFilePath);
                    _labDir = "D";
                }

                #endregion
            }
            catch
            {
                throw new Exception("配置文件加载失败,请检查!!");
            }
        }

        private WoInfo.Ecpwd Getcpwd(string cpwd)
        {
            // ReSharper disable once InconsistentNaming
            WoInfo.Ecpwd _cpwd;
            switch (cpwd.ToUpper())
            {
                case "PROG":
                    _cpwd = WoInfo.Ecpwd.Prog;
                    break;
                case "FILE":
                    _cpwd = WoInfo.Ecpwd.File;
                    break;
                case "USERDEF":
                    _cpwd = WoInfo.Ecpwd.Userdef;
                    break;
                default:
                    _cpwd = WoInfo.Ecpwd.Prog;
                    break;
            }
            return _cpwd;
        }

        /// <summary>
        ///     使用上次退出时的配置信息填写控件
        /// </summary>
        private void FillConfig()
        {
            tbwoid.Text = MWoInfo.WoId;
            cbstationId.SelectedItem = _mCraftName;
            lbstationname.Text = _mCraftName;
        }

        /// <summary>
        ///     比对序列号是否在工单定义的区间范围内
        /// </summary>
        /// <param name="woid">工单号</param>
        /// <param name="serial">序列号号</param>
        /// <param name="sntype">序列号类型</param>
        /// <returns></returns>
        private bool CompareSerialnumber(string woid, string serial, string sntype)
        {
            bool falg;
            if (sntype == "KCODE")
            {
                falg = CheckKCode(serial);
            }
            else
            {
                cdbAccess ass = new cdbAccess();
                string sql;
                if (sntype.ToUpper() == "IMEI" || sntype.ToUpper() == "MEID")
                {
                    sql =
                        string.Format(
                            "SELECT * FROM wosnrule where woid='{1}' and sntype='{2}' and '{0}' between snstart and snend",
                            serial.Length > 14 ? serial.Substring(0, 14) : serial, woid, sntype);
                }
                else
                {
                    sql =
                        string.Format(
                            "SELECT * FROM wosnrule where woid='{1}' and sntype='{2}' and ('{0}' between snstart and snend) and len(snstart)=len('{0}')",
                            serial, woid, sntype);
                }

                DataTable dt = ass.GetDatatable(sql);
                dt.DefaultView.Sort = "snstart asc";
                dt = dt.DefaultView.ToTable();
                if (dt.Rows.Count < 1)
                {
                    if (sntype != "SN" || (MWoInfo.Wotype != "Rework" && MWoInfo.Wotype != "RMA"))
                        return false;

                    sql = string.Format("SELECT * FROM wosnrule where woid='{0}' and sntype='{1}' ", woid, sntype);
                    DataTable dtsn = ass.GetDatatable(sql);
                    dtsn.DefaultView.Sort = "snstart asc";
                    dtsn = dtsn.DefaultView.ToTable();
                    return dtsn.Rows.Count < 1;
                }

                falg = true;
                //添加检查MAC和SPMAC递增规则检查  2013-04-22
                switch (sntype.ToUpper())
                {
                    case "MAC":
                        return true;

                    case "SPMAC":
                        if (!_bIsChkSpmac)
                            return true;
                        if (!ChkSerial(dt.Rows[0]["snstart"].ToString().ToUpper(),
                            serial.ToUpper(), int.Parse(dt.Rows[0]["usenum"].ToString())))
                            return false;
                        break;
                }
            }

            return falg;
        }

        /// <summary>
        ///     检查K码规则
        /// </summary>
        /// <param name="kCode"></param>
        /// <returns></returns>
        private bool CheckKCode(string kCode)
        {
            if (kCode.Length != 10)
            {
                ShowMsg(MLogMsgType.Error, string.Format("序列号[{1}]:[{0}]长度不符,请检查..", kCode, "KCODE"));
                return false;
            }

            if (Regex.IsMatch(kCode, @"^[a-zA-Z0-9]+$")) return true;
            ShowMsg(MLogMsgType.Error, string.Format("序列号[{1}]:[{0}]规则不符,请检查..", kCode, "KCODE"));
            return false;
        }

        /// <summary>
        ///     检查流程
        /// </summary>
        /// <param name="esndata">esn号</param>
        /// <param name="currentRoute">当前流程号</param>
        /// <returns>返回提示信息 非OK均为不良</returns>
        private string ChkRoute(string esndata, string currentRoute)
        {
            if (_mRprint)
                return "OK";

            _dic = new Dictionary<string, object> {{"DATA", esndata}, {"MYGROUP", currentRoute}};
            return refWebProPublicStoredproc.Instance.ExecuteProcedure("PRO_CHECKROUTE",
                MapListConverter.DictionaryToJson(_dic));
        }

        /// <summary>
        ///     初始化自定义的控件(清空内容)
        /// </summary>
        private void InitMyControl()
        {
            gpotherprint.Invoke(new EventHandler(delegate
            {
                for (int i = 0; i < _myVar.ArrVariable.Length; i++)
                {
                    _arrTextbox[i].Text = string.Empty;
                    _arrTextbox[i].NotErr = false;
                }
                _mCurrentEsn = string.Empty;
                _arrTextbox[0].Focus();
            }));
        }

        /// <summary>
        ///     卡通箱标签打印时定位光标用
        /// </summary>
        /// <param name="textName"></param>
        private void SetTextBoxFocus(string textName)
        {
            SetBtOkState(true);
            switch (textName)
            {
                case "tb_psninput": //2013-10-24
                    if (tb_esninput.Enabled)
                    {
                        tb_esninput.Text = "";
                        tb_esninput.Focus();
                    }
                    else
                    {
                        if (tb_esninput.Enabled)
                        {
                            tb_esninput.Focus();
                        }
                        else
                        {
                            if (tb_kcodeinput.Enabled)
                            {
                                tb_kcodeinput.Focus();
                            }
                            else if (tb_macinput.Enabled)
                            {
                                tb_macinput.Focus();
                            }
                            else if (tb_sninput.Enabled)
                            {
                                tb_sninput.Focus();
                            }
                            else
                            {
                                if (tb_ktinput.Enabled)
                                {
                                    tb_ktinput.Focus();
                                }
                                else
                                {
                                    if (tb_pcbasninput.Enabled)
                                    {
                                        tb_pcbasninput.Focus();
                                    }
                                    else
                                    {
                                        if (tb_spmacinput.Enabled)
                                        {
                                            tb_spmacinput.Focus();
                                        }
                                        else
                                        {
                                            bt_ok.Focus();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "tb_esninput": //2013-10-24

                    if (tb_kcodeinput.Enabled)
                    {
                        tb_kcodeinput.Text = "";
                        tb_kcodeinput.Focus();
                    }
                    else
                    {
                        if (tb_macinput.Enabled)
                        {
                            tb_macinput.Focus();
                        }
                        else
                        {
                            if (tb_sninput.Enabled)
                            {
                                tb_sninput.Focus();
                            }
                            else
                            {
                                if (tb_ktinput.Enabled)
                                {
                                    tb_ktinput.Focus();
                                }
                                else
                                {
                                    if (tb_pcbasninput.Enabled)
                                    {
                                        tb_pcbasninput.Focus();
                                    }
                                    else
                                    {
                                        if (tb_spmacinput.Enabled)
                                        {
                                            tb_spmacinput.Focus();
                                        }
                                        else
                                        {
                                            bt_ok.Focus();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;

                case "tb_kcodeinput":
                    if (tb_macinput.Enabled)
                    {
                        tb_macinput.Text = "";
                        tb_macinput.Focus();
                    }
                    else
                    {
                        if (tb_sninput.Enabled)
                        {
                            tb_sninput.Focus();
                        }
                        else
                        {
                            if (tb_ktinput.Enabled)
                            {
                                tb_ktinput.Focus();
                            }
                            else
                            {
                                if (tb_pcbasninput.Enabled)
                                {
                                    tb_pcbasninput.Focus();
                                }
                                else
                                {
                                    if (tb_spmacinput.Enabled)
                                    {
                                        tb_spmacinput.Focus();
                                    }
                                    else
                                    {
                                        bt_ok.Focus();
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "tb_macinput":
                    if (tb_sninput.Enabled)
                    {
                        tb_sninput.Text = "";
                        tb_sninput.Focus();
                    }
                    else
                    {
                        if (tb_ktinput.Enabled)
                        {
                            tb_ktinput.Focus();
                        }
                        else
                        {
                            if (tb_pcbasninput.Enabled)
                            {
                                tb_pcbasninput.Focus();
                            }
                            else
                            {
                                if (tb_spmacinput.Enabled)
                                {
                                    tb_spmacinput.Focus();
                                }
                                else
                                {
                                    bt_ok.Focus();
                                }
                            }
                        }
                    }
                    break;
                case "tb_sninput":
                    if (tb_ktinput.Enabled)
                    {
                        tb_ktinput.Text = "";
                        tb_ktinput.Focus();
                    }
                    else
                    {
                        if (tb_pcbasninput.Enabled)
                        {
                            tb_pcbasninput.Focus();
                        }
                        else
                        {
                            if (tb_spmacinput.Enabled)
                            {
                                tb_spmacinput.Focus();
                            }
                            else
                            {
                                bt_ok.Focus();
                            }
                        }
                    }
                    break;
                case "tb_ktinput":
                    if (tb_pcbasninput.Enabled)
                    {
                        tb_pcbasninput.Text = "";
                        tb_pcbasninput.Focus();
                    }
                    else
                    {
                        if (tb_spmacinput.Enabled)
                        {
                            tb_spmacinput.Focus();
                        }
                        else
                        {
                            bt_ok.Focus();
                        }
                    }
                    break;
                case "tb_pcbasninput":
                    if (tb_spmacinput.Enabled)
                    {
                        tb_spmacinput.Text = "";
                        tb_spmacinput.Focus();
                    }
                    else
                    {
                        bt_ok.Focus();
                    }
                    break;
                case "tb_spmacinput":
                    bt_ok.Focus();
                    break;
                default:
                    if (tb_psninput.Enabled)
                    {
                        tb_psninput.Focus();
                    }
                    else if (tb_esninput.Enabled)
                    {
                        tb_esninput.Focus();
                    }
                    else if (tb_kcodeinput.Enabled)
                    {
                        tb_kcodeinput.Focus();
                    }
                    else if (tb_macinput.Enabled)
                    {
                        tb_macinput.Focus();
                    }
                    else
                    {
                        if (tb_sninput.Enabled)
                        {
                            tb_sninput.Focus();
                        }
                        else
                        {
                            if (tb_ktinput.Enabled)
                            {
                                tb_ktinput.Focus();
                            }
                            else
                            {
                                if (tb_pcbasninput.Enabled)
                                {
                                    tb_pcbasninput.Focus();
                                }
                                else
                                {
                                    if (tb_spmacinput.Enabled)
                                    {
                                        tb_spmacinput.Focus();
                                    }
                                    else
                                    {
                                        bt_ok.Focus();
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }

        //==========================================================================================
        //======================              控件事件                             =================
        //==========================================================================================

        private void PrintMain_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("C:\\SFIS"))
                Directory.CreateDirectory("C:\\SFIS");

            _mTabItem = tbcLable.SelectedTab;
            SetBtOkState(false);
            Text = string.Format("{0} Version:{1} (Build Date:{2})", Text, AssemblyVersion,
                File.GetLastWriteTime(Application.ExecutablePath).ToShortDateString());

            cbstationId.Enabled = false;
            cblineId.Enabled = false;
            nudPrintNum.ReadOnly = true;
            numPrintQty.Enabled = false;

            SN_Printer.Checked = true;
            tb_Boxcount.ReadOnly = true;

            if (Checkprocessisrun("lppa"))
            {
                if (MessageBoxEx.Show("检测到有打开的模板文件 \n\n 请先关闭模板文件再运行程序,否则可能导致程序出错!! \n\n 关闭模板文件选择[Yes] 否则选择[NO]", "提示!!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    KillAllProcess();
                }
            }

            /*if (!refWebCheck_Version.Instance.CheckPrgVsersion("SFIS_PRINT_SYSTEM_2", System.Windows.Forms.Application.ProductVersion, null, null, null))
            {

                RunFile(System.Windows.Forms.Application.StartupPath + @"\", "AutoUpdate.exe", mAppFileName);
                MessageBox.Show("该程序为版本不是最新版\r\n请更新后运行");
                string FileName = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
                Process[] prc = Process.GetProcessesByName(FileName.Substring(0, FileName.LastIndexOf('.')));
                if (prc.Length > 0)
                    foreach (Process pc in prc)
                    {
                        pc.Kill();
                    }
                return;
            }*/


            /*SFIS_PRINT_SYSTEM_WIFI.UserLogin ul = new SFIS_PRINT_SYSTEM_WIFI.UserLogin(this);
            while (ul.ShowDialog() == DialogResult.No) ;

            if (this.loginOk)*/
            {
                if (_mUserInfo != null)
                    lbusername.Text = _mUserInfo.Username;

                LoadSystemLine();
                Load_All_Station();
                ReadConfig();
                FillConfig();
                btInputToUpper_Click(null, null);
            }
            string cRes = string.Empty;
            try
            {
                cRes = "加载串口DLL失败";
                _bzz = new buzzer();
                cRes = "连接串口失败";
                _bzz.ConnPort("LablePrint");
                ShowMsg(MLogMsgType.Incoming, "串口连接成功");
            }
            catch
            {
                ShowMsg(MLogMsgType.Error, cRes);
            }

            btInputToUpper_Click(null, null);
            cblineId.Enabled = true;
            cbstationId.Enabled = true;
        }

        public void PrintMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBoxEx.Show("是否确定退出!!!", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                SaveConfig();
                Closeproc("AutoUpdate.exe");
                try
                {
                    refWebtUserInfo.Instance.DeleteLogin(GUserInfo.UserId, _mIpaddress);
                    if (_mLibdoc != null)
                    {
                        _mLibdoc.Close(false);
                        _mlppx.Documents.CloseAll(false);
                    }
                    _mlppx.Quit();
                    _bzz.ClosePort();
                }
                catch
                {
                    // ignored
                }
                KillAllProcess();
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }

        private void btselectwo_Click(object sender, EventArgs e)
        {
            //获取未结工单列表
            DataTable dt = ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoInfo(null, null));
            ShowData sd = new ShowData(this,
                publicfunction.getNewTable(dt, "WOSTATE<>\'3\'"), true);
            sd.ShowDialog();
        }

        private void tbwoid_TextChanged(object sender, EventArgs e)
        {
            ShowMsg(MLogMsgType.Outgoing, "选择了工单:" + MWoInfo.WoId + "\n切换后需要重新打开模板");
            gpotherprint.Controls.Clear();
            gpotherprint.Refresh();
            SetBtOkState(false);
            pictureBox1.Image = null;
            _mPrintFileName = lb_showmfpath.Text = "";

            //获取自定义的密码
            if (MWoInfo.Cpwd == WoInfo.Ecpwd.Userdef)
            {
                Thread thread = new Thread(LoadWoPwd);
                thread.Start();
            }

            //加载工单序列号区间
            _eventRunNoparamet = DownloadWoSnRule;
            _iasyncresult = _eventRunNoparamet.BeginInvoke(null, null);

            //获取机型SN
            DataTable dtproduct =
                ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetProductInfoByWoId(tbwoid.Text));
            if (dtproduct == null || dtproduct.Rows.Count < 1)
            {
                ShowMsg(MLogMsgType.Error, "工单" + tbwoid.Text + "没有对应的产品信息");
                return;
            }

            _cnsFlag = dtproduct.Rows[0]["other"].ToString();
            StrProductSn = dtproduct.Rows[0]["productsn"].ToString();
        }

        private void LoadWoPwd()
        {
            //获取自定义的密码
            ShowMsg(MLogMsgType.Outgoing, "正在加载,预定义密码内容..");
            ShowMsg(MLogMsgType.Incoming, "预定义密码内容加载完成");
        }

        private void cbline_DropDown(object sender, EventArgs e)
        {
        }

        private void cblineId_SelectedValueChanged(object sender, EventArgs e)
        {
            _lineName = cblineId.Text;
            ShowMsg(MLogMsgType.Outgoing, string.Format("选中了线别:{0}[{1}]", lbLinename.Text, cblineId.Text));
        }

        private void cbstation_DropDown(object sender, EventArgs e)
        {
            if (MWoInfo != null) return;
            ShowMsg(MLogMsgType.Warning, "请先选择生产工单!");
        }

        private void cbstationId_SelectedValueChanged(object sender, EventArgs e)
        {
            gpotherprint.Controls.Clear();
            gpotherprint.Refresh();
            SetBtOkState(false);
            pictureBox1.Image = null;
            _mPrintFileName = lb_showmfpath.Text = "";
            _mCraftName = cbstationId.Text;
            lbstationname.Text = cbstationId.Text;
            //添加自动找到模板文件并打开
            //到数据库中找到该站使用哪个模板文件
            DataTable dt = ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetATEScripts(MWoInfo.WoId));
            if (dt == null || dt.Rows.Count <= 0) return;

            foreach (DataRow dr in dt.Rows)
            {
                if (string.IsNullOrEmpty(dr["script"].ToString())) continue;

                string[] arrScripts = dr["script"].ToString().Split(',');
                foreach (string str in arrScripts)
                {
                    if (str.Trim().ToUpper().IndexOf(lbstationname.Text.Trim().ToUpper(), StringComparison.Ordinal) ==
                        -1) continue;

                    var labfilename = str.Trim();
                    var labfilefullpath = string.Format(@"{0}\{1}\{2}",
                        _labDir.IndexOf(":", StringComparison.Ordinal) != -1 ? _labDir : _labDir + ":", MWoInfo.WoId,
                        labfilename);
                    if (!File.Exists(labfilefullpath))
                    {
                        ShowMsg(MLogMsgType.Warning, string.Format(@"{0}:\{1}\{2}",
                            _labDir, MWoInfo.WoId, labfilename) + ":文件不存在\n请手动选择模板文件..");
                        continue;
                    }
                    if (!string.IsNullOrEmpty(labfilefullpath))
                    {
                        if (
                            MessageBoxEx.Show("该站的模板文件已经找到 [" + labfilename + "] 是否打开?", "提示",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk,
                                MessageBoxDefaultButton.Button1) != DialogResult.Yes) continue;
                        ShowMsg(MLogMsgType.Normal, "发现该站的模板文件" + labfilefullpath);
                        OpenLabAndDowloadSnRule(labfilefullpath);
                        return;
                    }
                    ShowMsg(MLogMsgType.Error, "没有发现该站的模板文件,请手动选择");
                }
            }
        }


        private void imbtconfig_Click(object sender, EventArgs e)
        {
            string[] empData = InputBox.ShowInputBox("输入权限", string.Empty);
            try
            {
                string userId = empData[0];
                string pwd = empData[1];
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(pwd)) return;
                string strErr = refWebtUserInfo.Instance.CHECK_SET_LINE_EMPLOYEE(userId, pwd);
                if (strErr == "OK")
                {
                    ShowMsg(MLogMsgType.Incoming, "权限正确");
                    cbstationId.Enabled = true;
                    cblineId.Enabled = true;
                    numPrintQty.Enabled = true;
                }
                else
                {
                    ShowMsg(MLogMsgType.Error, strErr);
                }
            }
            catch (Exception ex)
            {
                ShowMsg(MLogMsgType.Error, "权限格式不正确:" + ex.Message);
            }
        }

        private void btenablesnrule_Click(object sender, EventArgs e)
        {
            _mUseSnRule = !_mUseSnRule;
            if (_mUseSnRule)
            {
                ShowMsg(MLogMsgType.Outgoing, "开启SN排序规则");
                btenablesnrule.Text = Resources.OpenSnRule;
            }
            else
            {
                ShowMsg(MLogMsgType.Warning, "关闭SN排序规则");
                btenablesnrule.Text = Resources.CloseSnRule;
            }
        }

        /// <summary>
        ///     打开模板(屏蔽)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbtopenlibfile_Click(object sender, EventArgs e)
        {
            #region 添加密码输入

            EnaPwd ed = new EnaPwd(this);
            if (ed.ShowDialog() == DialogResult.OK)
            {
                if (StrEnaPwd != "print2013")
                {
                    MessageBoxEx.Show("密码错误!!");
                    return;
                }
            }
            else
            {
                return;
            }

            #endregion

            OpenLabAndDowloadSnRule(string.Empty);
        }

        private void OpenLabAndDowloadSnRule(string filepath)
        {
            if (!CheckSelectIsOk())
                return;

            OpenLabFile(filepath);
            SaveConfig();
            _mPrintNumber = int.Parse(numPrintQty.Value.ToString());
            ShowMsg(MLogMsgType.Incoming, "模板文件初始化完成..");
        }

        private void textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13) return;

            cblineId.Enabled = false;
            cbstationId.Enabled = false;

            #region 判断准备条件

            if (string.IsNullOrEmpty(_mPrintFileName))
            {
                ShowMsg(MLogMsgType.Warning, "没有选择模板文件..");
                return;
            }
            if (_iasyncresult != null && !_iasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "工单序列号区间还在加载中,请稍候..");
                return;
            }
            if (_miasyncresult != null && !_miasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "上一个动作还在进行中,请稍候..");
                ((MyTextBox) sender).SelectAll();
                ((MyTextBox) sender).Focus();
                return;
            }

            #endregion

            try
            {
                //判断基本的长度是否符合
                if (((MyTextBox) sender).Text.Trim().Length != _myVar.DicVarLen[((MyTextBox) sender).Name])
                {
                    ShowMsg(MLogMsgType.Warning, ((MyTextBox) sender).Name + "长度不符");
                    ((MyTextBox) sender).NotErr = true;
                    ((MyTextBox) sender).SelectAll();
                    return;
                }

                //判断序列号是否在工单区间范围内
                if (!CompareSerialnumber(MWoInfo.WoId, ((MyTextBox) sender).Text.Trim(), ((MyTextBox) sender).Name)
                    && ((MyTextBox) sender).Name.ToUpper() != "ESN") //????
                {
                    ((MyTextBox) sender).NotErr = true;
                    ShowMsg(MLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                        ((MyTextBox) sender).Text, ((MyTextBox) sender).Name));
                    ((MyTextBox) sender).NotErr = true;
                    ((MyTextBox) sender).SelectAll();
                    return;
                }

                ((MyTextBox) sender).NotErr = false;

                #region 检查刷入的是什么序列号

                switch (((MyTextBox) sender).Name.ToUpper())
                {
                    case "ESN": //如果是esn则判断是否存在(即有没有跟PCBA esn绑定) + 根据esn判断流程
                        break;

                    case "MAC": //计算出MAC号对应的所有密码
                        switch (MWoInfo.Cpwd)
                        {
                            case WoInfo.Ecpwd.Prog:
                                _mAllKeys = macPassword.getMacAllPassword(((MyTextBox) sender).Text.Trim());
                                break;
                            case WoInfo.Ecpwd.Userdef:
                                _mdicAllKey = publicfunction.GetMacPwdByUse(MWoInfo.WoId,
                                    ((MyTextBox) sender).Text.Trim());
                                break;
                        }
                        break;
                }

                #endregion

                #region 判断是否在同一个输入控件填写

                bool bTemp = false;
                foreach (string str in _dicVarBuf.Keys)
                {
                    if (!string.Equals(str, ((MyTextBox) sender).Name, StringComparison.CurrentCultureIgnoreCase))
                        continue;

                    bTemp = true;
                    _dicVarBuf.Remove(((MyTextBox) sender).Name.ToUpper());
                    break;
                }

                #endregion

                _dicVarBuf.Add(((MyTextBox) sender).Name.Trim(),
                    _bInputUpper ? ((MyTextBox) sender).Text.Trim().ToUpper() : ((MyTextBox) sender).Text.Trim());
                if (!bTemp)
                    _miReadCount++;

                #region  判断是否所有的序列号都已经记录完成

                if (_miReadCount >= _myVar.ArrVariable.Length)
                {
                    #region  判断所有的输入内容是否都是正确的

                    for (int i = 0; i < _myVar.ArrVariable.Length; i++)
                    {
                        if (!_arrTextbox[i].NotErr) continue;

                        ShowMsg(MLogMsgType.Error, string.Format("输入存在错误:{0}", _arrTextbox[i].Text));
                        _arrTextbox[i].SelectAll();
                        _arrTextbox[i].Focus();
                        return;
                    }

                    #endregion

                    //如果所有的输入都没有错误 则开始打印
                    Enaothergroup(false);
                    _eventRunFunction = MPrinterLable;
                    _miasyncresult = _eventRunFunction.BeginInvoke(null, null);
                    _miReadCount = 0;
                }

                #endregion

                //焦点移到下一个控件
                _arrTextbox[_miReadCount].Focus();
            }
            catch (Exception ex)
            {
                _miReadCount = 0;
                _dicVarBuf.Clear();
                InitMyControl();
                ShowMsg(MLogMsgType.Error, ex.Message);
            }
        }

        private void Enaothergroup(bool isena)
        {
            gpotherprint.Invoke(new EventHandler(delegate { gpotherprint.Enabled = isena; }));
        }

        private void MPrinterLable()
        {
            if (MPrintLable())
            {
                //显示数据
                if (_iasyncresult2 == null || _iasyncresult2.IsCompleted)
                {
                    _eventshowotherdata = ShowOtherData;
                    _iasyncresult2 = _eventshowotherdata.BeginInvoke(MWoInfo.WoId, _bShowData, null, null);
                }
            }
            else
            {
                ShowMsg(MLogMsgType.Error, "标签打印失败,请检查....");
            }
            _miReadCount = 0;
            _dicVarBuf.Clear();
            Enaothergroup(true);
            InitMyControl();
        }

        private void imbtRprint_Click(object sender, EventArgs e)
        {
            #region 添加密码输入

            EnaPwd ed = new EnaPwd(this);
            if (ed.ShowDialog() == DialogResult.OK)
            {
                if (!refWebtUserInfo.Instance.ChkUserInfoIdAndPwd(StrEnaPwd.Split('-')[0], StrEnaPwd.Split('-')[1]))
                {
                    MessageBoxEx.Show("密码错误!!");
                    return;
                }
            }
            else
            {
                return;
            }

            #endregion

            ShowMsg(MLogMsgType.Outgoing, "开启重复打印");
            _mRprint = true;
        }

        private void nudPrintNum_Leave(object sender, EventArgs e)
        {
            nudPrintNum.ReadOnly = true;
        }

        private void nudPrintNum_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            nudPrintNum.ReadOnly = false;
        }

        private void nudPrintNum_ValueChanged(object sender, EventArgs e)
        {
            _mPrintNumber = (int) nudPrintNum.Value;
        }

        private void s_Person_Enter(object sender, EventArgs e)
        {
            ((TextBox) sender).BackColor = Color.Green;
            ((TextBox) sender).ForeColor = Color.White;
        }

        public void imbtexit_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void dgvdata_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_mTabItem.Name.Trim().ToUpper() != "TABITEM2") return;

            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            if (_isShowCartonContent) return;

            var catonId = dgvdata["cartonId", e.RowIndex].Value.ToString();
            dgvdata.DataSource = ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetCartonContent(catonId));
            _isShowCartonContent = true;
            dgvdata.ContextMenuStrip = null;
        }

        private void btInputToUpper_Click(object sender, EventArgs e)
        {
            _bInputUpper = !_bInputUpper;
            if (_bInputUpper)
            {
                btInputToUpper.Text = Resources.OpenForceUpper;
                ShowMsg(MLogMsgType.Outgoing, Resources.OpenForceUpper);
            }
            else
            {
                btInputToUpper.Text = Resources.CloseForceUpper;
                ShowMsg(MLogMsgType.Outgoing, Resources.CloseForceUpper);
            }
        }

        private bool ChkSerial(string startSn, string strVal, int iusenum)
        {
            if (startSn.Length != strVal.Length)
                return false;
            if (string.Equals(startSn, strVal, StringComparison.CurrentCultureIgnoreCase))
                return true;

            int f = 0;
            for (int i = 0; i <= startSn.Length; i++)
            {
                if (startSn.Substring(0, i + 1) == strVal.Substring(0, i + 1))
                    f++;
                else
                    break;
            }
            string str1 = startSn.Substring(f, startSn.Length - f);
            string str2 = strVal.Substring(f, strVal.Length - f);

            return (int.Parse(str2, NumberStyles.HexNumber) -
                    int.Parse(str1, NumberStyles.HexNumber))%iusenum == 0;
        }

        private bool ShowPwdWnd()
        {
            EnaPwd ed = new EnaPwd(this);
            if (ed.ShowDialog() != DialogResult.OK) return false;

            if (refWebtUserInfo.Instance.ChkUserInfoIdAndPwd(StrEnaPwd.Split('-')[0], StrEnaPwd.Split('-')[1]))
                return true;

            MessageBoxEx.Show("密码错误!!");
            return false;
        }

        private void imbt_chkmac_Click(object sender, EventArgs e)
        {
            if (!ShowPwdWnd()) return;

            _bIsChkMac = !_bIsChkMac;
            if (!_bIsChkMac)
            {
                imbt_chkmac.Text = Resources.CheckMacFalse;
                imbt_chkmac.ForeColor = Color.Red;
            }
            else
            {
                imbt_chkmac.Text = Resources.CheckMacTrue;
                imbt_chkmac.ForeColor = Color.Green;
            }
        }

        private void imbt_chkspmac_Click(object sender, EventArgs e)
        {
            if (!ShowPwdWnd()) return;

            _bIsChkSpmac = !_bIsChkSpmac;
            if (!_bIsChkSpmac)
            {
                imbt_chkspmac.Text = Resources.CheckSpMacFalse;
                imbt_chkspmac.ForeColor = Color.Red;
            }
            else
            {
                imbt_chkspmac.Text = Resources.CheckSpMacTrue;
                imbt_chkspmac.ForeColor = Color.Green;
            }
        }

        private void dgvdata_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvdata.RowHeadersDefaultCellStyle.ForeColor))
            {
                var linen = e.RowIndex + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X,
                    e.RowBounds.Location.Y + 5);
            }
        }

        private bool CHECK_PRODUCT_LINE()
        {
            return true;
        }

        private void EnableESNInput_CheckedChanged(object sender, EventArgs e)
        {
            if (_miasyncresult != null && !_miasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox) sender).Checked = false;
                return;
            }
            if (EnableESNInput.Checked)
            {
                label12.Visible = true;
                tb_esninput.Enabled = true;
                tb_esninput.Visible = true;
                ESN_Printer.Visible = true;
                ESN_count.Visible = true;
            }
            else
            {
                label12.Visible = false;
                tb_esninput.Text = null;
                tb_esninput.Enabled = false;
                tb_esninput.Visible = false;
                ESN_Printer.Visible = false;
                ESN_count.Visible = false;
            }
        }

        private void EnableKCODEInput_CheckedChanged(object sender, EventArgs e)
        {
            if (_miasyncresult != null && !_miasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox) sender).Checked = false;
                return;
            }
            if (EnableKCODEInput.Checked)
            {
                label15.Visible = true;
                tb_kcodeinput.Enabled = true;
                tb_kcodeinput.Visible = true;
                KCODE_Printer.Visible = true;
                KCODE_count.Visible = true;
            }
            else
            {
                label15.Visible = false;
                tb_kcodeinput.Text = null;
                tb_kcodeinput.Enabled = false;
                tb_kcodeinput.Visible = false;
                KCODE_Printer.Visible = false;
                KCODE_count.Visible = false;
            }
        }

        private void Fill_DatagridView(string esn, string sn, string kt, string pcbasn, string spmac, string kcode)
        {
            Invoke(new EventHandler(delegate
            {
                dgvdata.Rows.Add(esn, sn, kt, pcbasn, spmac, kcode);
                dgvdata.FirstDisplayedScrollingRowIndex = dgvdata.Rows.Count - 1;
            }));
        }

        /// <summary>
        ///     委托运行没有参数的方法
        /// </summary>
        private delegate void DelegateRunNoParmet();

        private delegate void DelegateShowCartonData(bool show);

        private delegate void DelegateShowOtherData(string woid, bool show);

        //==========================================================================================
        //======================              成员变量                             =================
        //==========================================================================================

        #region 成员变量

        /// <summary>
        ///     模板文件默认存放路径
        /// </summary>
        private string _labDir = string.Empty;

        private Dictionary<string, object> _dic;

        /// <summary>
        ///     使能输入强制转换为大写
        /// </summary>
        private bool _bInputUpper;

        /// <summary>
        ///     表示是否已经显示了卡通箱内容
        /// </summary>
        private bool _isShowCartonContent;

        /// <summary>
        ///     是否显示其他标签打印的数据显示
        /// </summary>
        private bool _bShowData;

        /// <summary>
        ///     记录当前选择的是哪个打印模块(其他标签还是卡通箱标签)
        /// </summary>
        private TabItem _mTabItem = new TabItem();

        /// <summary>
        ///     卡通箱内容
        /// </summary>
        public CartonInfo GCartonInfo = new CartonInfo();

        /// <summary>
        ///     使用SN递增规则
        /// </summary>
        private bool _mUseSnRule = true;

        /// <summary>
        ///     配置文件路劲
        /// </summary>
        private static readonly string IniFilePath = Application.StartupPath + "\\config.ini";

        private readonly string _sfisIniFilePath = "C:\\SFIS\\SFIS.ini";

        /// <summary>
        ///     属于密码的类型
        /// </summary>
        private readonly string[] _mPasswordName = {"PIN", "WEPKEY", "DEK", "SSID", "AES"};

        #region 卡通箱标签标识

        private string _esn = string.Empty;
        private string _esnvalue = string.Empty;

        private string _mac = string.Empty;
        private string _macvalue = string.Empty;

        private string _sn = string.Empty;
        private string _snvalue = string.Empty;

        private string _kt = string.Empty;
        private string _ktvalue = string.Empty;

        private string _pcbasn = string.Empty;
        private string _pcbasnvalue = string.Empty;

        private string _spmac = string.Empty;
        private string _spmacvalue = string.Empty;

        private string _kcode = string.Empty;
        private string _kcodevalue = string.Empty;

        #endregion

        /// <summary>
        ///     记录卡通箱需要打印字段的内容
        /// </summary>
        private string _mPrtColumns = string.Empty;

        /// <summary>
        ///     重复打印标志
        /// </summary>
        private bool _mRprint;

        /// <summary>
        ///     打印数量
        /// </summary>
        private int _mPrintNumber;

        /// <summary>
        ///     全局的 保存当前的esn号
        /// </summary>
        private string _mCurrentEsn = string.Empty;

        /// <summary>
        ///     保存需要通过MAC计算出来的各种密码
        /// </summary>
        private List<string> _mAllKeys = new List<string>();

        private Dictionary<string, string> _mdicAllKey = new Dictionary<string, string>();

        /// <summary>
        ///     用来统计已经记录的几个序列号
        /// </summary>
        private int _miReadCount;

        /// <summary>
        ///     站位编号
        /// </summary>
        private string _lineName = string.Empty;

        /// <summary>
        ///     站位名称
        /// </summary>
        private string _mCraftName = string.Empty;

        /// <summary>
        ///     工单实体
        /// </summary>
        public WoInfo MWoInfo { get; set; }

        /// <summary>
        ///     提示消息类型
        /// </summary>
        public enum MLogMsgType
        {
            Incoming,
            Outgoing,
            Normal,
            Warning,
            Error
        }

        /// <summary>
        ///     提示消息文字颜色
        /// </summary>
        private readonly Color[] _mLogMsgTypeColor = {Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red};

        /// <summary>
        ///     标签模板文件路径和名称
        /// </summary>
        private string _mPrintFileName = string.Empty;

        /// <summary>
        ///     保存打开的模板文件的产品名称
        /// </summary>
        private string _msProductText = string.Empty;

        /// <summary>
        ///     保存登录的用户信息
        /// </summary>
        private UserInfo _mUserInfo;

        /// <summary>
        ///     保存登录的用户信息
        /// </summary>
        public UserInfo GUserInfo
        {
            get { return _mUserInfo; }
            set { _mUserInfo = value; }
        }

        /// <summary>
        ///     登陆成功标志
        /// </summary>
        public bool LoginOk { get; set; }

        /// <summary>
        ///     实例化CodeSoft实例
        /// </summary>
        private ApplicationClass _mlppx;

        /// <summary>
        ///     CodeSoft文档
        /// </summary>
        private Document _mLibdoc;

        /// <summary>
        ///     是否保存模板文件
        /// </summary>
        private readonly bool _mbIsSaveLabFile = false;

        /// <summary>
        ///     记录卡通箱变量的数量
        /// </summary>
        private int _miCatonBoxTotal;

        /// <summary>
        ///     记录动态加载的文本框控件集合(需要输入的)
        /// </summary>
        private static MyTextBox[] _arrTextbox;

        /// <summary>
        ///     记录动态加载的文本框(需要输入的)控件集合的标题
        /// </summary>
        private static Label[] _arrLabel;

        /// <summary>
        ///     记录动态加载的文本框控件集合(不需要手工输入的)
        /// </summary>
        private static MyTextBox[] _arrTextboxReadOnly;

        /// <summary>
        ///     记录动态加载的文本框(不需要手工输入的)控件集合的标题
        /// </summary>
        private static Label[] _arrLabelReadOnly;

        /// <summary>
        ///     显示确认选择框
        /// </summary>
        private CheckBox _showDataChk;

        /// <summary>
        ///     记录从模板来的变量的各种属性
        /// </summary>
        public struct MyVariable
        {
            /// <summary>
            ///     记录正常填充变量的名称
            /// </summary>
            public string[] ArrVariable;

            /// <summary>
            ///     记录模板中变量的个数
            /// </summary>
            public int[] ArrVariableCount;

            /// <summary>
            ///     记录模板中每个变量的长度
            /// </summary>
            public Dictionary<string, int> DicVarLen;

            /// <summary>
            ///     序列号名称(公式变量)
            /// </summary>
            public string[] ArrAutoSerialVariableName;

            /// <summary>
            ///     序列号值(公式变量)
            /// </summary>
            public string[] ArrAutoSerialVariableValue;
        }

        /// <summary>
        ///     结构体(模板变量集合)
        /// </summary>
        private MyVariable _myVar;

        /// <summary>
        ///     记录模板中所有需要记录数据的变量名称
        /// </summary>
        private readonly List<string> _lsAllVarName = new List<string>();

        /// <summary>
        ///     记录所有需要刷入的内容的缓存
        /// </summary>
        private readonly Dictionary<string, string> _dicVarBuf = new Dictionary<string, string>();

        /// <summary>
        ///     记录CNS项目的首箱箱号2013-10-15
        /// </summary>
        public string StrBoxNumber = string.Empty;

        /// <summary>
        ///     记录产品的机型SN:2013-10-24
        /// </summary>
        public string StrProductSn = string.Empty;

        private buzzer _bzz;

        #endregion

        #region 卡通箱部分

        /// <summary>
        ///     2013-10-24序号比对
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SN_Check_CheckedChanged(object sender, EventArgs e)
        {
            if (_miasyncresult != null && !_miasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "程序正在初始化,请稍候...");
                return;
            }
            if (((CheckBox) sender).Checked)
            {
                ((CheckBox) sender).Text = Resources.Compare;
                _mPrtColumns += string.Format(",{0}", ((CheckBox) sender).Name.Split('_')[0]);
            }
            else
            {
                _mPrtColumns = _mPrtColumns.Replace(string.Format(",{0}", ((CheckBox) sender).Name.Split('_')[0]),
                    string.Empty);
                ((CheckBox) sender).Text = "";
            }
        }

        private void EnableMacInputBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_miasyncresult != null && !_miasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox) sender).Checked = false;
                return;
            }
            if (EnableMacInputBox.Checked)
            {
                label7.Visible = true;
                tb_macinput.Enabled = true;
                tb_macinput.Visible = true;
                MAC_Printer.Visible = true;
                MAC_count.Visible = true;
            }
            else
            {
                label7.Visible = false;
                tb_macinput.Text = null;
                tb_macinput.Enabled = false;
                tb_macinput.Visible = false;
                MAC_Printer.Visible = false;
                MAC_count.Visible = false;
            }
        }

        private void EnableSnInputBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_miasyncresult != null && !_miasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox) sender).Checked = false;
                return;
            }
            if (EnableSnInputBox.Checked)
            {
                label8.Visible = true;
                tb_sninput.Enabled = true;
                tb_sninput.Visible = true;
                SN_Printer.Visible = true;
                SN_count.Visible = true;
            }
            else
            {
                label8.Visible = false;
                tb_sninput.Text = null;
                tb_sninput.Enabled = false;
                tb_sninput.Visible = false;
                SN_Printer.Visible = false;
                SN_count.Visible = false;
            }
        }

        private void EnableKtInputBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_miasyncresult != null && !_miasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox) sender).Checked = false;
                return;
            }
            if (EnableKtInputBox.Checked)
            {
                label9.Visible = true;
                tb_ktinput.Enabled = true;
                tb_ktinput.Visible = true;
                KT_Printer.Visible = true;
                KT_count.Visible = true;
            }
            else
            {
                label9.Visible = false;
                tb_ktinput.Text = null;
                tb_ktinput.Enabled = false;
                tb_ktinput.Visible = false;
                KT_Printer.Visible = false;
                KT_count.Visible = false;
            }
        }

        private void EnablePCBASNInput_CheckedChanged(object sender, EventArgs e)
        {
            if (_miasyncresult != null && !_miasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox) sender).Checked = false;
                return;
            }
            if (EnablePCBASNInput.Checked)
            {
                label6.Visible = true;
                tb_pcbasninput.Enabled = true;
                tb_pcbasninput.Visible = true;
                PCBASN_Printer.Visible = true;
                PCBASN_count.Visible = true;
            }
            else
            {
                label6.Visible = false;
                tb_pcbasninput.Text = null;
                tb_pcbasninput.Enabled = false;
                tb_pcbasninput.Visible = false;
                PCBASN_Printer.Visible = false;
                PCBASN_count.Visible = false;
            }
        }

        private void EnableSPMACInput_CheckedChanged(object sender, EventArgs e)
        {
            if (_miasyncresult != null && !_miasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox) sender).Checked = false;
                return;
            }
            if (EnableSPMACInput.Checked)
            {
                label13.Visible = true;
                tb_spmacinput.Enabled = true;
                tb_spmacinput.Visible = true;
                SPMAC_Printer.Visible = true;
                SPMAC_count.Visible = true;
            }
            else
            {
                label13.Visible = false;
                tb_spmacinput.Text = null;
                tb_spmacinput.Enabled = false;
                tb_spmacinput.Visible = false;
                SPMAC_Printer.Visible = false;
                SPMAC_count.Visible = false;
            }
        }

        private void EnableDEKInput_CheckedChanged(object sender, EventArgs e)
        {
            if (_miasyncresult != null && !_miasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox) sender).Checked = false;
                return;
            }
            if (EnableDEKInput.Checked)
            {
                label5.Visible = true;
                tb_dekinput.Enabled = true;
                tb_dekinput.Visible = true;
                DEK_Printer.Visible = true;
                DEK_count.Visible = true;
            }
            else
            {
                label5.Visible = false;
                tb_dekinput.Text = null;
                tb_dekinput.Enabled = false;
                tb_dekinput.Visible = false;
                DEK_Printer.Visible = false;
                DEK_count.Visible = false;
            }
        }

        //2013-10-24
        private void EnablePSNInput_CheckedChanged(object sender, EventArgs e)
        {
            if (_miasyncresult != null && !_miasyncresult.IsCompleted)
            {
                ShowMsg(MLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox) sender).Checked = false;
                return;
            }
            if (EnablePSNInput.Checked)
            {
                label19.Visible = true;
                tb_psninput.Enabled = true;
                tb_psninput.Visible = true;
                PSN_Printer.Visible = true;
            }
            else
            {
                label19.Visible = false;
                tb_psninput.Text = null;
                tb_psninput.Enabled = false;
                tb_psninput.Visible = false;
                PSN_Printer.Visible = false;
            }
        }

        private int _m = 1;
        private string _recSerial = string.Empty;
        private string _inputTxt = string.Empty;

        private void CompareProductSn(string txtName)
        {
            switch (txtName)
            {
                case "tb_psninput":
                    if (StrProductSn != tb_psninput.Text.Trim())
                    {
                        ShowMsg(MLogMsgType.Error,
                            string.Format("机型SN::【{0}】与系统【{1}】不匹配", tb_psninput.Text, StrProductSn));
                        tb_psninput.Text = "";
                        tb_psninput.Focus();
                        return;
                    }
                    _m = 1;
                    break;
                case "tb_esninput":
                    _m = 1;
                    break;
                case "tb_kcodeinput":
                    if (KCODE_Printer.Checked)
                    {
                        #region KCODE

                        if (_m == 1)
                        {
                            if (!CompareSerialnumber(MWoInfo.WoId, tb_kcodeinput.Text.Trim(), "KCODE"))
                            {
                                tb_kcodeinput.Text = "";
                                return;
                            }

                            _recSerial = _inputTxt;
                            tb_kcodeinput.Text = "";
                            KCODE_count.Text = _m + Resources.BackSlash + nudPrintNum.Value;
                            _m++;
                            return;
                        }
                        if (_m > 1 && _m < nudPrintNum.Value)
                        {
                            if (_recSerial == _inputTxt)
                            {
                                tb_kcodeinput.Text = "";
                                KCODE_count.Text = _m + Resources.BackSlash + nudPrintNum.Value;
                                _m++;
                                return;
                            }
                            tb_kcodeinput.Text = "";
                            ShowMsg(MLogMsgType.Error,
                                string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                            return;
                        }
                        if (_m == nudPrintNum.Value)
                        {
                            if (_recSerial == _inputTxt)
                            {
                                KCODE_count.Text = _m + Resources.BackSlash + nudPrintNum.Value;
                                KCODE_count.BackColor = Color.Green;
                                _m = 1;
                            }
                            else
                            {
                                tb_kcodeinput.Text = "";
                                ShowMsg(MLogMsgType.Error,
                                    string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                                _m = 1;
                                KCODE_count.Text = Resources.ZeroSlash + nudPrintNum.Value;
                            }
                        }

                        #endregion
                    }
                    break;
                case "tb_macinput":
                    if (MAC_Printer.Checked)
                    {
                        #region MAC

                        if (_m == 1)
                        {
                            _recSerial = _inputTxt;
                            tb_macinput.Text = "";
                            MAC_count.Text = _m + Resources.BackSlash + nudPrintNum.Value;
                            _m++;
                            return;
                        }
                        if (_m > 1 && _m < nudPrintNum.Value)
                        {
                            if (_recSerial == _inputTxt)
                            {
                                tb_macinput.Text = "";
                                MAC_count.Text = _m + Resources.BackSlash + nudPrintNum.Value;
                                _m++;
                                return;
                            }
                            tb_macinput.Text = "";
                            ShowMsg(MLogMsgType.Error,
                                string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                            return;
                        }
                        if (_m == nudPrintNum.Value)
                        {
                            if (_recSerial == _inputTxt)
                            {
                                MAC_count.Text = _m + Resources.BackSlash + nudPrintNum.Value;
                                MAC_count.BackColor = Color.Green;
                                _m = 1;
                            }
                            else
                            {
                                tb_macinput.Text = "";
                                ShowMsg(MLogMsgType.Error,
                                    string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                            }
                        }

                        #endregion
                    }
                    break;
                case "tb_sninput":
                    if (SN_Printer.Checked)
                    {
                        #region SN

                        if (_m == 1)
                        {
                            if (!CompareSerialnumber(MWoInfo.WoId, tb_sninput.Text.Trim(), "SN"))
                            {
                                ShowMsg(MLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                                    tb_sninput.Text, "SN"));
                                tb_sninput.Text = "";
                                return;
                            }

                            _recSerial = _inputTxt;
                            tb_sninput.Text = "";
                            SN_count.Text = _m + Resources.BackSlash + nudPrintNum.Value;
                            _m++;
                            return;
                        }
                        if (_m > 1 && _m < nudPrintNum.Value)
                        {
                            if (_recSerial == _inputTxt)
                            {
                                tb_sninput.Text = "";
                                SN_count.Text = _m + Resources.BackSlash + nudPrintNum.Value;
                                _m++;
                                return;
                            }
                            tb_sninput.Text = "";
                            ShowMsg(MLogMsgType.Error,
                                string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                            return;
                        }
                        if (_m == nudPrintNum.Value)
                        {
                            if (_recSerial == _inputTxt)
                            {
                                SN_count.Text = _m + Resources.BackSlash + nudPrintNum.Value;
                                SN_count.BackColor = Color.Green;
                                _m = 1;
                            }
                            else
                            {
                                tb_sninput.Text = "";
                                ShowMsg(MLogMsgType.Error,
                                    string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                                _m = 1;
                                SN_count.Text = Resources.ZeroSlash + nudPrintNum.Value;
                            }
                        }

                        #endregion
                    }
                    break;
                case "tb_ktinput":
                    if (KT_Printer.Checked)
                    {
                        #region KT

                        if (nudPrintNum.Value > 1)
                        {
                            if (_m == 1)
                            {
                                _recSerial = _inputTxt;
                                tb_ktinput.Text = "";
                                KT_count.Text = _m + Resources.BackSlash + nudPrintNum.Value;
                                _m++;
                                return;
                            }
                            if (_m > 1 && _m < nudPrintNum.Value)
                            {
                                if (_recSerial == _inputTxt)
                                {
                                    tb_ktinput.Text = "";
                                    KT_count.Text = GetTextMsg(_m);
                                    _m++;
                                    return;
                                }
                                tb_ktinput.Text = "";
                                ShowMsg(MLogMsgType.Error,
                                    string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                                return;
                            }
                            if (_m == nudPrintNum.Value)
                            {
                                if (_recSerial == _inputTxt)
                                {
                                    KT_count.Text = GetTextMsg(_m);
                                    KT_count.BackColor = Color.Green;
                                    _m = 1;
                                }
                                else
                                {
                                    tb_ktinput.Text = "";
                                    ShowMsg(MLogMsgType.Error,
                                        string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                                }
                            }
                        }

                        #endregion
                    }
                    break;
                case "tb_pcbasninput":
                    if (PCBASN_Printer.Checked)
                    {
                        #region PCBASN

                        if (_m == 1)
                        {
                            _recSerial = _inputTxt;
                            tb_pcbasninput.Text = "";
                            PCBASN_count.Text = GetTextMsg(_m);
                            _m++;
                            return;
                        }
                        if (_m > 1 && _m < nudPrintNum.Value)
                        {
                            if (_recSerial == _inputTxt)
                            {
                                tb_pcbasninput.Text = "";
                                PCBASN_count.Text = GetTextMsg(_m);
                                _m++;
                                return;
                            }
                            tb_pcbasninput.Text = "";
                            ShowMsg(MLogMsgType.Error,
                                string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                            return;
                        }
                        if (_m == nudPrintNum.Value)
                        {
                            if (_recSerial == _inputTxt)
                            {
                                PCBASN_count.Text = GetTextMsg(_m);
                                PCBASN_count.BackColor = Color.Green;
                                _m = 1;
                            }
                            else
                            {
                                tb_pcbasninput.Text = "";
                                ShowMsg(MLogMsgType.Error,
                                    string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                            }
                        }

                        #endregion
                    }
                    break;
                case "tb_spmacinput":
                    if (SPMAC_Printer.Checked)
                    {
                        #region SPMAC

                        if (_m == 1)
                        {
                            _recSerial = _inputTxt;
                            tb_spmacinput.Text = "";
                            SPMAC_count.Text = GetTextMsg(_m);
                            _m++;
                            return;
                        }
                        if (_m > 1 && _m < nudPrintNum.Value)
                        {
                            if (_recSerial == _inputTxt)
                            {
                                tb_spmacinput.Text = "";
                                SPMAC_count.Text = GetTextMsg(_m);
                                _m++;
                                return;
                            }
                            tb_spmacinput.Text = "";
                            ShowMsg(MLogMsgType.Error,
                                string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                            return;
                        }
                        if (_m == nudPrintNum.Value)
                        {
                            if (_recSerial == _inputTxt)
                            {
                                SPMAC_count.Text = GetTextMsg(_m);
                                SPMAC_count.BackColor = Color.Green;
                                _m = 1;
                            }
                            else
                            {
                                tb_spmacinput.Text = "";
                                ShowMsg(MLogMsgType.Error,
                                    string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                            }
                        }

                        #endregion
                    }
                    break;
                case "tb_dekinput":
                    if (SN_Printer.Checked)
                    {
                        #region DEK

                        if (_m == 1)
                        {
                            _recSerial = _inputTxt;
                            tb_dekinput.Text = "";
                            DEK_count.Text = GetTextMsg(_m);
                            _m++;
                            return;
                        }
                        if (_m > 1 && _m < nudPrintNum.Value)
                        {
                            if (_recSerial == _inputTxt)
                            {
                                tb_dekinput.Text = "";
                                DEK_count.Text = GetTextMsg(_m);
                                _m++;
                                return;
                            }
                            tb_dekinput.Text = "";
                            ShowMsg(MLogMsgType.Error,
                                string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                            return;
                        }
                        if (_m == nudPrintNum.Value)
                        {
                            if (_recSerial == _inputTxt)
                            {
                                DEK_count.Text = GetTextMsg(_m);
                                DEK_count.BackColor = Color.Green;
                                _m = 1;
                            }
                            else
                            {
                                tb_dekinput.Text = "";
                                ShowMsg(MLogMsgType.Error,
                                    string.Format("【{0}】与最初的【{1}】不一致,请确认...", _inputTxt, _recSerial));
                            }
                        }

                        #endregion
                    }
                    break;
            }
        }

        /// <summary>
        ///     获取需要显示的文本信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private string GetTextMsg(int m)
        {
            return m + Resources.BackSlash + nudPrintNum.Value;
        }

        private void tbInputText_KeyDown(object sender, KeyEventArgs e)
        {
            _inputTxt = ((TextBox) sender).Text.Trim();
            if (string.IsNullOrEmpty(_inputTxt) || e.KeyValue != 13) return;

            if (nudPrintNum.Value > 1)
            {
                CompareProductSn(((TextBox) sender).Name);
            }

            if (!string.IsNullOrEmpty(((TextBox) sender).Text))
            {
                SetTextBoxFocus(((TextBox) sender).Name);
            }
        }

        private void s_TextBoxInput_Leave(object sender, EventArgs e)
        {
            ((TextBox) sender).BackColor = Color.White;
            ((TextBox) sender).ForeColor = Color.Black;
            try
            {
                #region 条件选择

                switch (((TextBox) sender).Name)
                {
                    case "tb_psninput":
                        break;
                    case "tb_esninput":

                        #region 检查ESN是否符合工单要求

                        _esn = "ESN";
                        _esnvalue = _bInputUpper
                            ? ((TextBox) sender).Text.Trim().ToUpper()
                            : ((TextBox) sender).Text.Trim();

                        #endregion

                        break;
                    case "tb_macinput":

                        #region MAC

                        if (!string.IsNullOrEmpty(((TextBox) sender).Text.Trim()))
                        {
                            #region 检查Mac是否符合工单要求

                            if (CompareSerialnumber(MWoInfo.WoId,
                                ((TextBox) sender).Text.Trim(), "MAC"))
                            {
                                _mac = "MAC";
                                _macvalue = _bInputUpper
                                    ? ((TextBox) sender).Text.Trim().ToUpper()
                                    : ((TextBox) sender).Text.Trim();
                            }
                            else
                            {
                                ShowMsg(MLogMsgType.Error, string.Format("MAC::【{0}】不存在于该生产工单", ((TextBox) sender).Text));
                                ((TextBox) sender).SelectAll();
                                ((TextBox) sender).Focus();
                            }

                            #endregion
                        }

                        #endregion

                        break;
                    case "tb_sninput":

                        #region SN

                        if (!string.IsNullOrEmpty(((TextBox) sender).Text.Trim()))
                        {
                            #region 检查是否为递增序列号

                            if (_mUseSnRule)
                            {
                                if (
                                    !CompareSnAreaHistory(((TextBox) sender).Text.Trim(),
                                        CIniConfig.IniReadValue("SETUP", "SnHistory", IniFilePath)))
                                {
                                    ShowMsg(MLogMsgType.Error, "序列号不符合规则,是否继续?");
                                    if (MessageBoxEx.Show("序列号不符合规则,是否继续?\n继续请按[Yes],取消请按[No]", "警告!!",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) ==
                                        DialogResult.No)
                                    {
                                        ((TextBox) sender).SelectAll();
                                        ((TextBox) sender).Focus();
                                        return;
                                    }
                                }
                            }
                            CIniConfig.IniWriteValue("SETUP", "SnHistory", ((TextBox) sender).Text.Trim(), IniFilePath);

                            #endregion

                            #region 检查是否符合工单要求

                            if (CompareSerialnumber(MWoInfo.WoId,
                                ((TextBox) sender).Text.Trim(), "SN"))
                            {
                                _sn = "SN";
                                _snvalue = _bInputUpper
                                    ? ((TextBox) sender).Text.Trim().ToUpper()
                                    : ((TextBox) sender).Text.Trim();
                            }
                            else
                            {
                                ShowMsg(MLogMsgType.Error,
                                    string.Format("当前输入【{0}】与工单设置不匹配,或已超出范围！！", ((TextBox) sender).Text));
                                ((TextBox) sender).SelectAll();
                                ((TextBox) sender).Focus();
                                SetTextBoxFocus("");
                                _m = 1;
                            }

                            #endregion
                        }

                        #endregion

                        break;
                    case "tb_ktinput":

                        #region KT

                        if (!string.IsNullOrEmpty(((TextBox) sender).Text.Trim()))
                        {
                            if (CompareSerialnumber(MWoInfo.WoId,
                                ((TextBox) sender).Text.Trim(), "KT"))
                            {
                                _kt = "KT";
                                _ktvalue = _bInputUpper
                                    ? ((TextBox) sender).Text.Trim().ToUpper()
                                    : ((TextBox) sender).Text.Trim();
                            }
                            else
                            {
                                ShowMsg(MLogMsgType.Error, string.Format("当前输入[{0}]与工单设置不匹配,或已超出范围！！",
                                    ((TextBox) sender).Text));
                                ((TextBox) sender).SelectAll();
                                ((TextBox) sender).Focus();
                            }
                        }

                        #endregion

                        break;
                    case "tb_pcbasninput":

                        #region PCBASN

                        if (!string.IsNullOrEmpty(((TextBox) sender).Text.Trim()))
                        {
                            #region 检查是否符合工单要求

                            if (CompareSerialnumber(MWoInfo.WoId,
                                ((TextBox) sender).Text.Trim(), "PCBASN"))
                            {
                                _pcbasn = "PCBASN";
                                _pcbasnvalue = _bInputUpper
                                    ? ((TextBox) sender).Text.Trim().ToUpper()
                                    : ((TextBox) sender).Text.Trim();
                            }
                            else
                            {
                                ShowMsg(MLogMsgType.Error, string.Format("当前输入[{0}]与工单设置不匹配,或已超出范围！！",
                                    ((TextBox) sender).Text));
                                ((TextBox) sender).SelectAll();
                                ((TextBox) sender).Focus();
                            }

                            #endregion
                        }

                        #endregion

                        break;
                    case "tb_spmacinput":

                        #region SPMAC

                        if (!string.IsNullOrEmpty(((TextBox) sender).Text.Trim()))
                        {
                            if (CompareSerialnumber(MWoInfo.WoId,
                                ((TextBox) sender).Text.Trim(), "SPMAC"))
                            {
                                _spmac = "SPMAC";
                                _spmacvalue = _bInputUpper
                                    ? ((TextBox) sender).Text.Trim().ToUpper()
                                    : ((TextBox) sender).Text.Trim();
                            }
                            else
                            {
                                ShowMsg(MLogMsgType.Error, string.Format("当前输入[{0}]与工单设置不匹配,或已超出范围！！",
                                    ((TextBox) sender).Text));
                                ((TextBox) sender).SelectAll();
                                ((TextBox) sender).Focus();
                            }
                        }

                        #endregion

                        break;

                    case "tb_kcodeinput":

                        #region KCODE

                        if (!string.IsNullOrEmpty(((TextBox) sender).Text.Trim()))
                        {
                            if (!CheckKCode(((TextBox) sender).Text))
                            {
                                ((TextBox) sender).SelectAll();
                                ((TextBox) sender).Focus();
                                return;
                            }
                            _kcode = "KCODE";
                            _kcodevalue = _bInputUpper
                                ? ((TextBox) sender).Text.Trim().ToUpper()
                                : ((TextBox) sender).Text.Trim();
                        }

                        #endregion

                        break;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ((TextBox) sender).SelectAll();
                ((TextBox) sender).Focus();
                ShowMsg(MLogMsgType.Error, ex.Message);
            }
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {
            InputEvent();
        }

        private void bt_ok_Enter(object sender, EventArgs e)
        {
            bt_ok_Click(sender, e);
        }

        private void InputEvent()
        {
            try
            {
                if (_iasyncresult != null && !_iasyncresult.IsCompleted)
                {
                    ShowMsg(MLogMsgType.Warning, "工单序列号区间还在加载中,请稍候..");
                    return;
                }

                cbstationId.Enabled = false;
                cblineId.Enabled = false;
                numPrintQty.Enabled = false;

                #region MAC

                if (tb_macinput.Enabled)
                {
                    if (!string.IsNullOrEmpty(tb_macinput.Text.Trim()))
                    {
                        // 判断序列号是否在工单区间范围内 ---取消本地判定工单 20131125 michael
                        if (!CompareSerialnumber(MWoInfo.WoId, tb_macinput.Text.Trim()
                            , "MAC"))
                        {
                            ShowMsg(MLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                                tb_macinput.Text, "MAC"));
                            tb_macinput.SelectAll();
                            tb_macinput.Focus();
                            return;
                        }
                    }
                    else
                    {
                        tb_macinput.Focus();
                        return;
                    }
                }

                #endregion

                #region SN

                if (tb_sninput.Enabled)
                {
                    if (!string.IsNullOrEmpty(tb_sninput.Text.Trim()))
                    {
                        //判断序列号是否在工单区间范围内
                        if (!CompareSerialnumber(MWoInfo.WoId, tb_sninput.Text.Trim()
                            , "SN"))
                        {
                            ShowMsg(MLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                                tb_sninput.Text, "SN"));
                            tb_sninput.SelectAll();
                            tb_sninput.Focus();
                            return;
                        }
                    }
                    else
                    {
                        tb_sninput.Focus();
                        return;
                    }
                }

                #endregion

                #region xxx-KT(2013-09-11)

                if (tb_ktinput.Enabled)
                {
                    if (!string.IsNullOrEmpty(tb_ktinput.Text.Trim()))
                    {
                        //判断序列号是否在工单区间范围内
                        if (!CompareSerialnumber(MWoInfo.WoId, tb_ktinput.Text.Trim()
                            , "KT"))
                        {
                            ShowMsg(MLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                                tb_ktinput.Text, "KT"));
                            tb_ktinput.SelectAll();
                            tb_ktinput.Focus();
                            return;
                        }
                    }
                    else
                    {
                        tb_ktinput.Focus();
                        return;
                    }
                }

                #endregion

                #region PCBASN

                if (tb_pcbasninput.Enabled)
                {
                    if (!string.IsNullOrEmpty(tb_pcbasninput.Text.Trim()))
                    {
                        //判断序列号是否在工单区间范围内
                        if (!CompareSerialnumber(MWoInfo.WoId, tb_pcbasninput.Text.Trim()
                            , "PCBASN"))
                        {
                            ShowMsg(MLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                                tb_pcbasninput.Text, "PCBASN"));
                            tb_pcbasninput.SelectAll();
                            tb_pcbasninput.Focus();
                            return;
                        }
                    }
                    else
                    {
                        tb_pcbasninput.Focus();
                        return;
                    }
                }

                #endregion

                #region SPMAC

                if (tb_spmacinput.Enabled)
                {
                    if (!string.IsNullOrEmpty(tb_spmacinput.Text.Trim()))
                    {
                        //判断序列号是否在工单区间范围内
                        if (!CompareSerialnumber(MWoInfo.WoId, tb_spmacinput.Text.Trim(), "SPMAC"))
                        {
                            ShowMsg(MLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                                tb_spmacinput.Text, "SPMAC"));
                            tb_spmacinput.SelectAll();
                            tb_spmacinput.Focus();
                            return;
                        }
                    }
                    else
                    {
                        tb_spmacinput.Focus();
                        return;
                    }
                }

                #endregion

                #region KCODE

                if (tb_kcodeinput.Enabled)
                {
                    if (!string.IsNullOrEmpty(tb_kcodeinput.Text.Trim()))
                    {
                        if (!CompareSerialnumber(MWoInfo.WoId, tb_kcodeinput.Text.Trim(), "KCODE"))
                        {
                            tb_kcodeinput.SelectAll();
                            tb_kcodeinput.Focus();
                            return;
                        }
                    }
                    else
                    {
                        tb_kcodeinput.Focus();
                        return;
                    }
                }

                #endregion

                #region 清空控件内容，重新设定空间焦点

                tb_psninput.Clear();
                tb_esninput.Clear();
                tb_macinput.Clear();
                tb_sninput.Clear();
                tb_ktinput.Clear();
                tb_pcbasninput.Clear();
                tb_spmacinput.Clear();
                tb_kcodeinput.Clear();
                MAC_count.Text = Resources.Zero;
                SN_count.Text = Resources.Zero;
                KT_count.Text = Resources.Zero;
                PCBASN_count.Text = Resources.Zero;
                SPMAC_count.Text = Resources.Zero;
                DEK_count.Text = Resources.Zero;
                KCODE_count.Text = Resources.Zero;
                SetTextBoxFocus("");

                #endregion

                #region 当前刷入的内容和内容类型

                //记录当前卡通箱包装刷入了哪些序列号的值
                string strV = string.Format("{0},{1},{2},{3},{4},{5}", _macvalue, _snvalue, _ktvalue, _pcbasnvalue,
                    _spmacvalue, _kcodevalue);

                //记录当前卡通箱包装刷如了哪些序列号类型
                string strN = string.Format("{0},{1},{2},{3},{4},{5}", _mac, _sn, _kt, _pcbasn, _spmac, _kcode);
                if (strV.Trim().Split(',').Length != strN.Trim().Split(',').Length)
                    throw new Exception("错误:序列号类型个数和序列号值的个数不一致,请检查..");

                List<string> ls = new List<string>();
                foreach (string str in strV.Split(','))
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        ls.Add(str);
                    }
                }
                var arrV = ls.ToArray();
                ls.Clear();
                foreach (string str in strN.Split(','))
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        ls.Add(str);
                    }
                }
                var arrN = ls.ToArray();
                ls.Clear();

                #endregion

                #region 临时的内部变量

                int i = 0;
                //用来记录需要新绑定的序列号
                Dictionary<string, string> dicInsertTemp = new Dictionary<string, string>();
                List<string> lsstrV = new List<string>();

                #endregion

                if (!_mRprint)
                {
                    #region 判断流程与工单 20131125 michael

                    DataTable dtwip =
                        ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo("ESN",
                            _esnvalue));
                    if (dtwip == null || dtwip.Rows.Count < 1)
                        throw new Exception("WIP No Data,请检查..");
                    if (dtwip.Rows[0][1].ToString() != tbwoid.Text.Trim())
                        throw new Exception("工单不同: [" + dtwip.Rows[0][1] + "],请检查..");
                    if (!CHECK_PRODUCT_LINE())
                        throw new Exception("请切换线别");

                    //需要添加流程CHECK  重复打印要不要检查流程？？？？？？？？？
                    var strEsnTemp = _esnvalue;
                    var err = ChkRoute(strEsnTemp, _mCraftName);
                    if (err.ToUpper() != "OK")
                        throw new Exception("流程错误:当前esn:" + strEsnTemp + ":" + err);
                    ShowMsg(MLogMsgType.Outgoing, "流程检测通过");

                    #endregion

                    //判断卡通箱刷入的每个序列号返回的esn是否是指向相同的一个

                    #region 处理刷入的内容

                    foreach (string item in arrV)
                    {
                        if (string.IsNullOrEmpty(item)) continue;

                        var mdt = ReleaseData.arrByteToDataTable(
                            refWebtWipTracking.Instance.GetSnInfo(item));
                        //判断是否存在  做到在卡通箱包装可以绑定序列号
                        if (mdt == null || mdt.Rows.Count < 1)
                        {
                            lsstrV.Add(item);
                            dicInsertTemp.Add(arrN[i], item);
                            i++;
                            continue;
                        }
                        if (mdt.Rows.Count > 1)
                            throw new Exception("严重错误:同一个序列号[" + item + "]系统中存在多笔,请检查..");

                        //通过esn找到该esn所对应的所有数据和当前的输入值进行比对看看是否是一致的
                        /********如果当前输入的值在esn找到的数据中不存在，
                        *********那么需要拿这个值到系统中查找看看是否已经使用过了，
                        *********如果没有使用过则绑定到当前数据 ，如果使用过了则直接报错*/

                        #region 取消判定ESN 20151112 michael

                        mdt =
                            ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetEsnDataInfo(
                                "esn", strEsnTemp));

                        #endregion

                        #region 利用通过esn找到的数据和当前输入的数据进行比对

                        for (int x = 0; x < arrV.Length; x++)
                        {
                            if (string.IsNullOrEmpty(arrV[x])) continue;

                            mdt.Select(string.Format("woId='{0}' and sntype='{1}' and snval='{2}'",
                                MWoInfo.WoId, arrN[x], arrV[x]));
                            var arrDr = mdt.Select(string.Format("woId='{0}' and sntype='{1}'",
                                MWoInfo.WoId, arrN[x]));

                            //如果序列号类型找到了但是值不相等 怎么办
                            if (arrDr.Length > 1)
                                throw new Exception("严重错误:序列号类型:" + arrN[x] + "存在多个,请检查...");
                            if (arrDr.Length == 1)
                            {
                                if (
                                    !string.Equals(arrDr[0]["snval"].ToString(), arrV[x],
                                        StringComparison.CurrentCultureIgnoreCase))
                                    throw new Exception(
                                        string.Format("序列号类型:[{0}]当前输入的值:[{1}],和历史记录数据[{2}]不相等.记录失败!!!",
                                            arrN[x], arrV[x], arrDr[0]["snval"]));
                            }
                            if (arrDr.Length >= 1) continue;

                            //需要查看一下这个号是否存在与__dicInsertTemp中,以避免多次查询
                            if (CompareArray(arrV[x], lsstrV.ToArray())) continue;
                            //再到数据库中查找一下看看是否有且工单和esn是一致的
                            DataTable dt =
                                ReleaseData.arrByteToDataTable(
                                    refWebtWipTracking.Instance.GetEsnDataInfo(
                                        string.Empty, arrV[x]));
                            if (dt == null || dt.Rows.Count < 1)
                            {
                                lsstrV.Add(arrV[x]);
                                dicInsertTemp.Add(arrN[x], arrV[x]);
                                continue;
                            }
                            if (dt.Rows.Count > 1)
                                throw new Exception("严重错误:同一个序列号[" + arrV[x] + "]系统中存在多笔,请检查..");

                            if (dt.Rows[0]["woId"].ToString().Trim() !=
                                MWoInfo.WoId.Trim())
                                throw new Exception("序列号在另一个工单[" +
                                                    dt.Rows[0]["woId"] +
                                                    "]中使用过了,请检查....");
                            if (dt.Rows[0]["esn"].ToString().Trim() != strEsnTemp)
                                throw new Exception(string.Format("序列号{0}已经在其他的产品上使用过了esn:{1}",
                                    arrV[x], dt.Rows[0]["esn"]));
                            if (dt.Rows[0]["sntype"].ToString().Trim() != arrN[x])
                                throw new Exception(string.Format("序列号对应的序列号类型不一致{0}≠{1}",
                                    dt.Rows[0]["sntype"].ToString().Trim(), arrN[x]));
                            {
                                throw new Exception("不明错误,请联系系统管理员,谢谢");
                            }
                        }

                        #endregion

                        break;
                    }

                    #endregion

                    #region  记录新绑定的内容

                    IList<IDictionary<string, object>> dicKps = new List<IDictionary<string, object>>();
                    foreach (string str in dicInsertTemp.Keys)
                    {
                        _dic = new Dictionary<string, object>
                        {
                            {"ESN", strEsnTemp},
                            {"SNTYPE", str},
                            {"SNVAL", dicInsertTemp[str]},
                            {"WOID", MWoInfo.WoId},
                            {"STATION", _mCraftName},
                            {"KPNO", "NA"}
                        };
                        dicKps.Add(_dic);
                    }
                    if (dicKps.Count > 0)
                    {
                        string strErr =
                            refWebtWipTracking.Instance.InsertWipKeyParts(MapListConverter.ListDictionaryToJson(dicKps));
                        strErr = string.IsNullOrEmpty(strErr) ? "OK" : strErr;
                        if (strErr != "OK")
                        {
                            throw new Exception("错误:记录KeyPart失败 " + strErr + "\n请联系管理员检查..");
                        }
                    }

                    #endregion

                    #region 使用箱号编码原则填充箱号包含(过站,记录产能）2013-10-24

                    ShowMsg(MLogMsgType.Warning, "正在进行过站记录..");
                    _dic = new Dictionary<string, object>
                    {
                        {"DATA", strEsnTemp},
                        {"MYGROUP", _mCraftName},
                        {"SECTION_NAME", "NA"},
                        {"STATION_NAME", _mCraftName + "1"},
                        {"EMP", _mUserInfo.UserId + "-" + _mUserInfo.Pwd},
                        {"EC", "NA"},
                        {"LINE", _lineName}
                    };
                    err = refWebProcedure.Instance.ExecuteProcedure("PRO_TEST_MAIN_ONLY",
                        MapListConverter.DictionaryToJson(_dic));

                    #endregion

                    #region 判断返回的信息 2013-10-24

                    if (err.ToUpper().IndexOf("OK", StringComparison.Ordinal) == -1)
                    {
                        throw new Exception("错误:过站失败!!,错误信息:\n" + err + "\n请联系管理员检查..");
                    }

                    Fill_DatagridView(strEsnTemp,
                        dicInsertTemp.ContainsKey("SN") ? dicInsertTemp["SN"] : "NA",
                        dicInsertTemp.ContainsKey("KT") ? dicInsertTemp["KT"] : "NA",
                        dicInsertTemp.ContainsKey("PCBASN") ? dicInsertTemp["PCBASN"] : "NA",
                        dicInsertTemp.ContainsKey("SPMAC") ? dicInsertTemp["SPMAC"] : "NA",
                        dicInsertTemp.ContainsKey("KCODE") ? dicInsertTemp["KCODE"] : "NA");
                }

                Dictionary<string, string> dicPrint = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(_esn))
                    dicPrint.Add(_esn, _esnvalue);

                if (!string.IsNullOrEmpty(_mac))
                    dicPrint.Add(_mac, _macvalue);

                if (!string.IsNullOrEmpty(_sn))
                    dicPrint.Add(_sn, _snvalue);

                if (!string.IsNullOrEmpty(_kt))
                    dicPrint.Add(_kt, _ktvalue);

                if (!string.IsNullOrEmpty(_pcbasn))
                    dicPrint.Add(_pcbasn, _pcbasnvalue);

                if (!string.IsNullOrEmpty(_spmac))
                    dicPrint.Add(_spmac, _spmacvalue);

                if (!string.IsNullOrEmpty(_kcode))
                    dicPrint.Add(_kcode, _kcodevalue);

                Print_Label(dicPrint, Convert.ToInt32(numPrintQty.Value));

                #endregion
            }
            catch (Exception ex)
            {
                ShowMsg(MLogMsgType.Error, ex.Message);
            }
            finally
            {
                if (_showCartonIasyncresult == null || _showCartonIasyncresult.IsCompleted)
                {
                    _eventshowcartondata = ShowCartonData;
                    _showCartonIasyncresult = _eventshowcartondata.BeginInvoke(chkShowdata.Checked, null, null);
                }

                #region 初始化变量

                _esnvalue = string.Empty;
                _macvalue = string.Empty;
                _snvalue = string.Empty;
                _ktvalue = string.Empty;
                _pcbasnvalue = string.Empty;
                _spmacvalue = string.Empty;
                _kcodevalue = string.Empty;

                _esn = string.Empty;
                _sn = string.Empty;
                _mac = string.Empty;
                _kt = string.Empty;
                _pcbasn = string.Empty;
                _spmac = string.Empty;
                _kcode = string.Empty;

                #endregion

                if (_mRprint)
                {
                    _mRprint = false;
                    ShowMsg(MLogMsgType.Incoming, "关闭重复打印");
                }

                ShowMsg(MLogMsgType.Incoming, "初始化完成");
            }
        }

        private void Print_Label(Dictionary<string, string> printDic, int printQty)
        {
            if (printQty == 0)
                return;
            try
            {
                ShowMsg(MLogMsgType.Incoming, string.Format("{0}{1}", "初始化打印设备..", DateTime.Now.ToString("HH:mm:ss")));
                if (printDic == null || printDic.Count < 1)
                    throw new Exception("错误:没有需要打印的内容1,请检查..");
                if (_mLibdoc == null)
                    throw new Exception("模板文件没有初始化");
                _mLibdoc.ViewMode = enumViewMode.lppxViewModeSize;

                foreach (KeyValuePair<string, string> keyValue in printDic)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(keyValue.Value)) continue;

                        _mLibdoc.Variables.FormVariables.Item(keyValue.Key).Value = keyValue.Value;
                        ShowMsg(MLogMsgType.Normal, string.Format("填充模板信息{0}->{1}", keyValue.Key, keyValue.Value));
                    }
                    catch
                    {
                        // ignored
                    }
                }

                _mLibdoc.PrintDocument(printQty);
                ShowMsg(MLogMsgType.Incoming, "打印完成...");
            }
            catch (Exception ex)
            {
                ShowMsg(MLogMsgType.Error, ex.Message);
            }
            finally
            {
                if (_mLibdoc != null)
                {
                    for (int z = 0; z < _mLibdoc.Variables.FormVariables.Count; z++)
                    {
                        _mLibdoc.Variables.FormVariables.Item(z + 1).Value = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        ///     打印卡通箱内容
        /// </summary>
        /// <param name="dtPrint"></param>
        /// <param name="printUserCartonId"></param>
        private string PrintCartonBox(DataTable dtPrint, bool printUserCartonId)
        {
            ShowMsg(MLogMsgType.Incoming, string.Format("{0}{1}", "初始化设备..", DateTime.Now.ToString("HH:mm:ss")));
            if (dtPrint == null || dtPrint.Rows.Count < 1)
                return "错误:没有需要打印的内容1,请检查..";
            if (_mLibdoc == null)
                return "模板文件没有初始化";
            _mLibdoc.ViewMode = enumViewMode.lppxViewModeSize;
            try
            {
                #region 处理模板公式下面的变量

                if (_mLibdoc.Variables.Formulas.Count > 0)
                {
                    try
                    {
                        //填充模板公式变量的内容 主要用来填充该箱的附加信息(箱号、工单、重量等)
                        for (int i = 0; i < _mLibdoc.Variables.Formulas.Count; i++)
                        {
                            switch (_mLibdoc.Variables.Formulas.Item(i + 1).Name.ToUpper())
                            {
                                case "BOXNUM":
                                    string valTemp =
                                        string.Format("upper(\"{0}\")",
                                            printUserCartonId
                                                ? dtPrint.Rows[0]["cartonId"].ToString()
                                                : int.Parse(dtPrint.Rows[0]["cartonnumber"].ToString()).ToString())
                                            .Trim();
                                    _mLibdoc.Variables.Formulas.Item("BOXNUM").Prefix = string.Empty;
                                    _mLibdoc.Variables.Formulas.Item("BOXNUM").Expression = valTemp;
                                    break;

                                case "COUNT":
                                    int icount = dtPrint.Rows.Count/
                                                 dtPrint.DefaultView.ToTable(true, "sntype").Rows.Count;
                                    _mLibdoc.Variables.Formulas.Item("COUNT").Prefix = string.Empty;
                                    _mLibdoc.Variables.Formulas.Item("COUNT").Expression = icount.ToString();
                                    break;
                            }
                        }
                    }
                    catch
                    {
                        return "错误:在模板公式产生错误";
                    }
                }

                #endregion

                #region 处理填充器下的变量内容

                #region 2013-04-13 修改(可以排序SN)

                //2013-08-22 修改一箱打印多个条码
                DataTable outNewTable;
                string err;
                //用于一个箱号分多张纸打印，而不满箱的情况标志
                bool cflag = false;
                if (!string.IsNullOrEmpty(err = GetPrintContentTable(ref dtPrint, out outNewTable)))
                    return err;

                #region 一箱一张纸

                if (_cnsFlag.ToUpper() == "CNS")
                {
                    string cartonnumber = dtPrint.Rows[0]["cartonId"].ToString();
                    _mLibdoc.Variables.FormVariables.Item("CARTONNUMBER").Length = cartonnumber.Length;
                    _mLibdoc.Variables.FormVariables.Item("CARTONNUMBER").Value = cartonnumber;
                }
                if (dtPrint.Rows.Count <= _mLibdoc.Variables.FormVariables.Count)
                {
                    for (int a = 0; a < outNewTable.Rows.Count; a++)
                    {
                        for (int i = 0; i < outNewTable.Columns.Count; i++)
                        {
                            try
                            {
                                _mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}",
                                    outNewTable.Columns[i].ColumnName.ToUpper(), a + 1)).Length =
                                    outNewTable.Rows[a][i].ToString().Trim().Length;
                                _mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}",
                                    outNewTable.Columns[i].ColumnName.ToUpper(), a + 1)).Value =
                                    outNewTable.Rows[a][i].ToString().Trim();
                            }
                            catch (Exception ex)
                            {
                                return "卡通箱模板赋值出现错误:" + ex.Message;
                            }
                        }
                    }
                    //开始打印
                    _mLibdoc.PrintDocument(_mPrintNumber);
                    return string.Empty;
                }

                    #endregion

                else
                {
                    #region 一箱多张纸打印情况

                    for (int a = 0, varCount = 1; a < outNewTable.Rows.Count; a++, varCount++)
                    {
                        for (int i = 0; i < outNewTable.Columns.Count; i++)
                        {
                            try
                            {
                                _mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}",
                                    outNewTable.Columns[i].ColumnName.ToUpper(), varCount)).Length =
                                    outNewTable.Rows[a][i].ToString().Trim().Length;
                                _mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}",
                                    outNewTable.Columns[i].ColumnName.ToUpper(), varCount)).Value =
                                    outNewTable.Rows[a][i].ToString().Trim();
                            }
                            catch (Exception ex)
                            {
                                return "卡通箱模板赋值出现错误:" + ex.Message;
                            }
                        }
                        //针对一个箱号分多张纸打印情况
                        if (varCount == _mLibdoc.Variables.FormVariables.Count)
                        {
                            //开始打印
                            _mLibdoc.PrintDocument(_mPrintNumber);
                            varCount = 0;
                            cflag = true;
                        }
                        if (!cflag) continue; //先清空再赋值

                        for (int z = 0; z < _mLibdoc.Variables.FormVariables.Count; z++)
                        {
                            _mLibdoc.Variables.FormVariables.Item(z + 1).Prefix = string.Empty;
                            _mLibdoc.Variables.FormVariables.Item(z + 1).Value = string.Empty;
                        }
                        cflag = false;
                    }
                    //最后一箱未满箱打印
                    if (!string.IsNullOrEmpty(_mLibdoc.Variables.FormVariables.Item(1).Value))
                        _mLibdoc.PrintDocument(_mPrintNumber);
                }

                #endregion

                #endregion

                return string.Empty;

                #endregion
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                //模板内容
                for (int z = 0; z < _mLibdoc.Variables.FormVariables.Count; z++)
                {
                    _mLibdoc.Variables.FormVariables.Item(z + 1).Prefix = string.Empty;
                    _mLibdoc.Variables.FormVariables.Item(z + 1).Value = string.Empty;
                }
            }
        }

        /// <summary>
        ///     将table横向排列
        /// </summary>
        /// <param name="mdt"></param>
        /// <param name="newTable"></param>
        /// <returns></returns>
        private static string GetPrintContentTable(ref DataTable mdt, out DataTable newTable)
        {
            newTable = new DataTable("Print");
            try
            {
                //找出有多少种序列号类型
                DataTable sntypedt = mdt.DefaultView.ToTable(true, "sntype");
                bool sortsn = false;
                foreach (DataRow dritem in sntypedt.Rows)
                {
                    newTable.Columns.Add(dritem["sntype"].ToString().Trim());
                    if ("SN" == dritem["sntype"].ToString().Trim().ToUpper())
                        sortsn = true;
                }
                DataTable esndt = mdt.DefaultView.ToTable(true, "esn");
                for (int i = 0; i < esndt.Rows.Count; i++)
                {
                    DataRow[] arrDr = mdt.Select(string.Format("esn='{0}'", esndt.Rows[i]["esn"].ToString().Trim()));
                    DataRow newdr = newTable.NewRow();
                    foreach (DataRow dr in arrDr)
                    {
                        newdr[dr["sntype"].ToString().Trim()] = dr["snval"].ToString();
                    }
                    newTable.Rows.Add(newdr);
                }
                if (sortsn)
                    newTable.DefaultView.Sort = "sn asc";
                newTable = newTable.DefaultView.ToTable();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void dgvNotCloseBoxNumber_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        private void btRepearCartonBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvdata.SelectedRows.Count > 0)
                {
                    for (int i = 0; i < dgvdata.SelectedRows.Count; i++)
                    {
                        if (dgvdata.SelectedRows[i].Cells["flag"].Value.ToString().Trim() != "1")
                        {
                            if (MessageBoxEx.Show("当前卡通箱还没有包装完成,是否强制关闭?\n关闭后该箱将不能再包装,是否继续? \n继续请选择[YES] 返回请选择 [NO]",
                                "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) ==
                                DialogResult.Yes)
                            {
                                //强制关闭卡通箱
                                refWebtWipTracking.Instance.CloseCartonBox(
                                    dgvdata.SelectedRows[i].Cells["cartonId"].Value.ToString());
                            }
                            else
                                throw new Exception("卡通箱还没有关闭,不能打印..");
                        }
                        RepearePrintCarton(dgvdata.SelectedRows[i].Cells["cartonId"].Value.ToString());
                    }
                }
                else
                {
                    ShowMsg(MLogMsgType.Warning, "没有选中任何需要打印的信息,请重新选择..");
                }
            }
            catch (Exception ex)
            {
                ShowMsg(MLogMsgType.Error, ex.Message);
            }
        }

        private void chkShowdata_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dgvdata.ContextMenuStrip = contextMenuStrip2;
                _isShowCartonContent = false;
                ShowCartonData(chkShowdata.Checked);
            }
            catch (Exception ex)
            {
                ShowMsg(MLogMsgType.Error, ex.Message);
            }
        }

        #endregion

        #region 功能页面切换

        private void tbcLable_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            switch (e.NewTab.Name)
            {
                case "tabItem1":
                    _mTabItem = e.NewTab;
                    tbcLable.SelectedTab = _mTabItem;
                    gpotherprint.Controls.Clear();
                    gpotherprint.Refresh();
                    SetBtOkState(false);
                    break;

                case "tabItem2":
                    if (MessageBoxEx.Show("切换后需要重新打开模板\n\n\n是否需要切换? ", "提示",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        _mTabItem = e.NewTab;
                        tbcLable.SelectedTab = _mTabItem;
                        gpotherprint.Controls.Clear();
                        gpotherprint.Refresh();
                        pictureBox1.Image = null;
                        _mPrintFileName = lb_showmfpath.Text = "";
                    }
                    else
                    {
                        tbcLable.SelectedTab = e.OldTab;
                        SetBtOkState(false);
                    }
                    break;
            }
        }

        private void SetBtOkState(bool ena)
        {
            bt_ok.Invoke(new EventHandler(delegate
            {
                bt_ok.Enabled = ena;
                lb_cartoncount.Text = "";
                lb_cartoncount.Visible = ena;
                tb_Boxcount.Text = "";
                if (ena) return;

                EnableMacInputBox.Checked = false;
                EnableSnInputBox.Checked = false;
                EnableKtInputBox.Checked = false;
                EnablePCBASNInput.Checked = false;
                EnableSPMACInput.Checked = false;
                EnableDEKInput.Checked = false;
                EnablePSNInput.Checked = false;
                EnableESNInput.Checked = false;
                EnableKCODEInput.Checked = false;
            }));
        }

        #endregion
    }

    /// <summary>
    ///     自定义控件类(TextBox)
    /// </summary>
    public class MyTextBox : TextBox
    {
        public MyTextBox()
        {
            NotErr = false;
        }

        public bool NotErr { get; set; }
    }

    public class UserInfo
    {
        /// <summary>
        ///     用户工号(主键)
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     用户角色
        /// </summary>
        public string Rolecaption { get; set; }

        /// <summary>
        ///     所在部门
        /// </summary>
        public string Deptname { get; set; }

        /// <summary>
        ///     所属工厂编号
        /// </summary>
        public string FacId { get; set; }

        /// <summary>
        ///     用户名称
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     用户密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        ///     联系电话
        /// </summary>
        public string Userphone { get; set; }

        /// <summary>
        ///     电子邮件
        /// </summary>
        public string Useremail { get; set; }

        /// <summary>
        ///     用户状态:0停用；1:启用
        /// </summary>
        public bool Userstatus { get; set; }

        /// <summary>
        ///     保存用户的权限信息(progid and funid)
        /// </summary>
        public DataTable UserPopList { get; set; }
    }

    public class WoInfo
    {
        public enum Ecpwd
        {
            Prog,
            File,
            Userdef
        }

        /// <summary>
        ///     工单号
        /// </summary>
        public string WoId { get; set; }

        /// <summary>
        ///     订单号
        /// </summary>
        public string PoId { get; set; }

        /// <summary>
        ///     数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        ///     工单状态
        /// </summary>
        public int Wostate { get; set; }

        /// <summary>
        ///     建立人
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     成品料号
        /// </summary>
        public string Partnumber { get; set; }

        /// <summary>
        ///     成品名
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///     BOM版本
        /// </summary>
        public string Bomver { get; set; }

        /// <summary>
        ///     工艺入口站
        /// </summary>
        public string Inputgroup { get; set; }

        /// <summary>
        ///     工艺出口站
        /// </summary>
        public string Outputgroup { get; set; }

        /// <summary>
        ///     工单类型
        /// </summary>
        public string Wotype { get; set; }

        /// <summary>
        ///     SAP工单类型
        /// </summary>
        public string Sapwotype { get; set; }

        /// <summary>
        ///     产品版本
        /// </summary>
        public string Per { get; set; }

        /// <summary>
        ///     BOM编号（组装用BOM）
        /// </summary>
        public string Bomnumber { get; set; }

        /// <summary>
        ///     生产流程编号
        /// </summary>
        public string RoutgroupId { get; set; }

        /// <summary>
        ///     产出数
        /// </summary>
        public int Outputqty { get; set; }

        /// <summary>
        ///     投入数
        /// </summary>
        public int Inputqty { get; set; }

        /// <summary>
        ///     报废数
        /// </summary>
        public int Scrapqty { get; set; }

        /// <summary>
        ///     密码来源
        /// </summary>
        public Ecpwd Cpwd { get; set; }

        /// <summary>
        ///     ATE脚本
        /// </summary>
        public string StrAteScript { get; set; }

        /// <summary>
        ///     软件版本
        /// </summary>
        public string SwVer { get; set; }

        /// <summary>
        ///     硬件版本
        /// </summary>
        public string FwVer { get; set; }

        /// <summary>
        ///     网标前缀
        /// </summary>
        public string NalPrefix { get; set; }

        /// <summary>
        ///     检查BI站
        /// </summary>
        public string ChkBiRoute { get; set; }

        /// <summary>
        ///     BI率
        /// </summary>
        public string BiProportion { get; set; }

        /// <summary>
        ///     BI检查提示率
        /// </summary>
        public string BiWarning { get; set; }

        /// <summary>
        ///     包装站检查标号
        /// </summary>
        public string CheckNo { get; set; }
    }

    public class CartonInfo
    {
        /// <summary>
        ///     卡通箱编号
        /// </summary>
        public string CartonId { get; set; }

        /// <summary>
        ///     产品唯一序列号
        /// </summary>
        public string Esn { get; set; }

        /// <summary>
        ///     产线编号
        /// </summary>
        public string LineId { get; set; }

        /// <summary>
        ///     工单编号
        /// </summary>
        public string WoId { get; set; }

        /// <summary>
        ///     客户卡通箱编号
        /// </summary>
        public string Mcartonnumber { get; set; }

        /// <summary>
        ///     产品SN号
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        ///     产品MAC号
        /// </summary>
        public string Mac { get; set; }

        /// <summary>
        ///     当前作业的电脑
        /// </summary>
        public string Computer { get; set; }

        /// <summary>
        ///     当前该箱的数量
        /// </summary>
        public int Number { get; set; }
    }
}