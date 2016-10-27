using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Diagnostics;
using SFIS_PRINT_SYSTEM.BLL;
using System.IO;
using System.Xml;
using LabelManager2;
using System.Text.RegularExpressions;
using System.Threading;
//using GenericUtil;

namespace SFIS_PRINT_SYSTEM
{
    public partial class PrintMain : Office2007Form// Form
    {
        public PrintMain()
        {
            InitializeComponent();
        }

        //==========================================================================================
        //======================              成员变量                             =================
        //==========================================================================================

        #region 成员变量
        /// <summary>
        /// 是否提示的标志
        /// </summary>
        private bool bMask = true;
        /// <summary>
        /// 模板文件默认存放路径
        /// </summary>
        private string LabDir = string.Empty;
        private readonly string DllConfig = System.Windows.Forms.Application.StartupPath + "\\DllConfig.xml";
        /// <summary>
        /// 使能输入强制转换为大写
        /// </summary>
        private bool bInputUpper = false;

        Dictionary<string, object> dic = null;

        /// <summary>
        /// 表示是否已经显示了卡通箱内容
        /// </summary>
        private bool IsShowCartonContent = false;
        /// <summary>
        /// 老流程ATE检查标志
        /// </summary>
        private bool atecheckflag = false;
        /// <summary>
        /// 是否显示其他标签打印的数据显示
        /// </summary>
        private bool bShowData = false;
        /// <summary>
        /// 记录当前选择的是哪个打印模块(其他标签还是卡通箱标签)
        /// </summary>
        private TabItem mTabItem = new TabItem();
        /// <summary>
        /// 卡通箱内容
        /// </summary>
        public tCartonInfo gCartonInfo = new tCartonInfo();
        /// <summary>
        /// 当前在包装的卡通箱编号
        /// </summary>
        private string mCartonId = string.Empty;
        /// <summary>
        /// 使用SN递增规则
        /// </summary>
        private bool mUseSnRule = true;
        /// <summary>
        /// 配置文件路劲
        /// </summary>
        private static string IniFilePath = System.Windows.Forms.Application.StartupPath + "\\config.ini";
        /// <summary>
        /// 属于密码的类型
        /// </summary>
        private readonly string[] mPasswordName = new string[] { "PIN", "WEPKEY", "DEK", "SSID", "AES" };
        #region 卡通箱标签标识
        private string ESN = string.Empty;
        private string ESNVALUE = string.Empty;

        private string MAC = string.Empty;
        private string MACVALUE = string.Empty;

        private string SN = string.Empty;
        private string SNVALUE = string.Empty;

        private string KT = string.Empty;
        private string KTVALUE = string.Empty;

        private string PCBASN = string.Empty;
        private string PCBASNVALUE = string.Empty;

        private string SPMAC = string.Empty;
        private string SPMACVALUE = string.Empty;
        #endregion
        /// <summary>
        /// 记录卡通箱需要打印字段的内容
        /// </summary>
        private string mPrtColumns = string.Empty;
        /// <summary>
        /// 重复打印标志
        /// </summary>
        private bool mRprint = false;
        /// <summary>
        /// 打印数量
        /// </summary>
        private int mPrintNumber = 0;
        /// <summary>
        /// 各种密码的来源(默认为true由程序算法产生,false由模板算法产生)
        /// </summary>
        // private bool mCreateKeyFromProg = true;
        /// <summary>
        /// 全局的 保存当前的esn号
        /// </summary>
        private string mCurrentEsn = string.Empty;
        /// <summary>
        /// 保存需要通过MAC计算出来的各种密码
        /// </summary>
        private List<string> mAllKeys = new List<string>();
        private Dictionary<string, string> mdicAllKey = new Dictionary<string, string>();
        /// <summary>
        /// 用来记录当前序列号组唯一号码
        /// </summary>
        private string msEsn = string.Empty;
        /// <summary>
        /// 用来统计已经记录的几个序列号
        /// </summary>
        private int miReadCount = 0;
        /// <summary>
        /// 文本输入框正在执行动作的对当前编辑框是否再次响应事件的标志
        /// </summary>
        private bool FlagLeave = false;
        /// <summary>
        /// 文本编辑框失去焦点后的一个标志 
        /// </summary>
        private bool bdebug = false;
        /// <summary>
        /// 线体信息实体
        /// </summary>
        public tLineInfo mLineInfo { get; set; }
        /// <summary>
        /// 记录所有线提信息
        /// </summary>
        private DataTable mdtAllLineInfo = null;      
        /// <summary>
        /// 站位名称
        /// </summary>
        private string mCraftName = string.Empty;
        /// <summary>
        /// 工单实体
        /// </summary>
       // public T_WO_INFO.ecpwd mWoInfo { get; set; }
        public T_WO_INFO mWoInfo { get; set; }
        /// <summary>
        /// 当前应用程序的名称
        /// </summary>
        private string mAppFileName = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
        /// <summary>
        /// 提示消息类型
        /// </summary>
        public enum mLogMsgType { Incoming, Outgoing, Normal, Warning, Error }
        /// <summary>
        /// 提示消息文字颜色
        /// </summary>
        private Color[] mLogMsgTypeColor = { Color.Green, Color.Blue, Color.Black, Color.Orange, Color.Red };
        /// <summary>
        /// 标签模板文件路径和名称
        /// </summary>
        private string mPrintFileName = string.Empty;
        /// <summary>
        /// 保存打开的模板文件的产品名称
        /// </summary>
        private string msProductText = string.Empty;

        /// <summary>
        /// 保存登录的用户信息
        /// </summary>
        private tUserInfo    mUserInfo;

        /// <summary>
        /// 保存登录的用户信息
        /// </summary>
        public tUserInfo gUserInfo
        {
            get { return mUserInfo; }
            set { mUserInfo = value; }
        }
        /// <summary>
        /// 登陆成功标志
        /// </summary>
        public bool loginOk { get; set; }

        /// <summary>
        /// 实例化CodeSoft实例
        /// </summary>
        private LabelManager2.ApplicationClass mlppx = null;//new LabelManager2.ApplicationClass();
        /// <summary>
        /// CodeSoft文档
        /// </summary>
        private LabelManager2.Document mLibdoc = null;

        /// <summary>
        /// 是否保存模板文件
        /// </summary>
        private bool mbIsSaveLabFile = false;
        /// <summary>
        /// 记录卡通箱变量的数量
        /// </summary>
        private int miCatonBoxTotal = 0;

        ///// <summary>
        ///// 记录选择的功能区
        ///// </summary>
        //private enum tabpagecontrol
        //{
        //    /// <summary>
        //    /// 其他标签
        //    /// </summary>
        //    OTHER = 0,
        //    /// <summary>
        //    /// 卡通箱标签
        //    /// </summary>
        //    CARTON
        //}
        ///// <summary>
        ///// 记录选择的功能区
        ///// </summary>
        //private tabpagecontrol mtabpagestutes = tabpagecontrol.OTHER;
        /// <summary>
        /// 标示是否需要保存当前打开的模板文件
        /// </summary>
        private bool mSaveLibFileFlag = false;
        /// <summary>
        /// 记录动态加载的文本框控件集合(需要输入的)
        /// </summary>
        private static MyTextBox[] arrTextbox;
        /// <summary>
        /// 记录动态加载的文本框(需要输入的)控件集合的标题
        /// </summary>
        private static Label[] arrLabel;
        /// <summary>
        /// 记录动态加载的文本框控件集合(不需要手工输入的)
        /// </summary>
        private static MyTextBox[] arrTextboxReadOnly;
        /// <summary>
        /// 记录动态加载的文本框(不需要手工输入的)控件集合的标题
        /// </summary>
        private static Label[] arrLabelReadOnly;
        /// <summary>
        /// 显示确认选择框
        /// </summary>
        private CheckBox ShowDataChk;

        /// <summary>
        /// 记录从模板来的变量的各种属性
        /// </summary>
        public struct MyVariable
        {
            /// <summary>
            /// 记录正常填充变量的名称
            /// </summary>
            public string[] arrVariable;
            /// <summary>
            /// 记录模板中变量的个数
            /// </summary>
            public int[] arrVariableCount;
            /// <summary>
            /// 记录模板中每个变量的长度
            /// </summary>
            public Dictionary<string, int> DicVarLen;
            /// <summary>
            /// 序列号名称(公式变量)
            /// </summary>
            public string[] arrAutoSerialVariableName;
            /// <summary>
            /// 序列号值(公式变量)
            /// </summary>
            public string[] arrAutoSerialVariableValue;
        }
        /// <summary>
        /// 结构体(模板变量集合)
        /// </summary>
        private MyVariable MyVar;

        /// <summary>
        /// 记录模板中所有需要记录数据的变量名称
        /// </summary>
        private List<string> lsAllVarName = new List<string>();

        /// <summary>
        /// 记录所有需要刷入的内容的缓存
        /// </summary>
        private Dictionary<string, string> dicVarBuf = new Dictionary<string, string>();

        /// <summary>
        /// 记录CNS项目的首箱箱号
        /// </summary>
        public string strBoxNumber = string.Empty;

        /// <summary>
        /// 记录模板是否打印卡通箱条码标志（False：不打印 True：打印）
        /// </summary>
        public bool mCartonFlag = false;
        #endregion
        string Line_IniFilePath = "C:\\SFIS\\SFIS.ini";
        /// <summary>
        /// 委托运行没有参数的方法
        /// </summary>
        private delegate void delegateRunNoParmet();

        private delegate void delegateShowCartonData(bool show);
        private delegate void delegateShowOtherData(string woid, bool show);
        /// <summary>
        /// 委托运行没有参数的方法的实例
        /// </summary>
        private delegateRunNoParmet EventRunNoparamet;
        private delegateRunNoParmet eventRunFunction;
        private delegateShowCartonData eventshowcartondata;
        private delegateShowOtherData eventshowotherdata;

        /// <summary>
        /// 托管跟踪
        /// </summary>
        private IAsyncResult miasyncresult;
        private IAsyncResult iasyncresult;
        private IAsyncResult iasyncresult1;
        private IAsyncResult iasyncresult2;
        private IAsyncResult ShowCartonIasyncresult;

        //==========================================================================================
        //======================              公共函数                             =================
        //==========================================================================================
        #region 公共函数       
        /// <summary>
        /// 显示卡通箱数据
        /// </summary>
        /// <param name="show">是否显示</param>
        private void ShowCartonData(bool show)
        {
            this.dgvdata.Invoke(new EventHandler(delegate
            {
                if (show)
                {
                    this.dgvdata.DataSource = SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetPackCarton(this.mWoInfo.woId, this.mLineInfo.lineId));
                    this.dgvdata.Refresh();
                }
            }));
        }

        private void ShowOtherData(string woId, bool show)
        {
            this.dgvdata.Invoke(new EventHandler(delegate
            {
                if (show)
                {
                    this.dgvdata.DataSource = SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetWoAllSerial(this.mWoInfo.woId, 4));
                    this.dgvdata.Refresh();
                }
            }));
        }
        /// <summary>
        /// 重新打印卡通箱标签
        /// </summary>
        /// <param name="_cartonnumber">卡通箱编号</param>
        private void RepearePrintCarton(string _cartonnumber)
        {
            List<string> lsColumns = new List<string>();
            string[] arColumns = this.mPrtColumns.Split(',');
            foreach (string str in arColumns)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    lsColumns.Add(str);
                }
            }
            //判断打印的这箱有没有关闭或满箱
            string cartonState = refWebtWipTracking.Instance.GetCartonState(_cartonnumber);
            if (cartonState == null || cartonState.Trim() != "1")
            {
                this.ShowMsg(mLogMsgType.Error, "卡通箱:" + _cartonnumber + "还没有关闭");
            }

            string NewErr = string.Empty;
            if (lsColumns != null && lsColumns.Count > 0)
                NewErr = this.PrintCartonBox(SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetCartonPrintContent(_cartonnumber, lsColumns.ToArray())), this.mPrintNumber, false);
            else
                this.ShowMsg(mLogMsgType.Warning, "当前没有选择需要打印的内容,打印不执行..");

            if (!string.IsNullOrEmpty(NewErr))
                this.ShowMsg(mLogMsgType.Error, "打印错误:" + NewErr);
            else
                this.ShowMsg(mLogMsgType.Outgoing, "完成..");
        }
        /// <summary>
        /// SN号需要按递增规则
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
                    if ((history.Substring(0, i)) != (serial.Substring(0, i)))
                    {
                        flag = i;
                        break;
                    }
                }
                if (flag == 0)
                    return false;
                if (serial.Substring(0, flag - 1) != history.Substring(0, flag - 1))
                    return false;
                int _histroy = int.Parse(history.Substring(flag - 1, history.Length - flag + 1));
                int _serial = int.Parse(serial.Substring(flag - 1, history.Length - flag + 1));
                if (_serial == _histroy + 1)
                    return true;
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                ShowMsg(mLogMsgType.Error, ex.Message + ": FrmMain 317");
                return false;
            }
        }
        /// <summary>
        /// 填充自定义控件内容
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
        /// 查询内容是否存在于数组
        /// </summary>
        /// <param name="str">查询的值</param>
        /// <param name="content">数组</param>
        /// <returns></returns>
        private bool CompareArray(string str, string[] content)
        {
            bool flag = false;
            foreach (string item in content)
            {
                if (str == item)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        /// <summary>
        /// 查询内容是否存在于数组
        /// </summary>
        /// <param name="str">查询的值</param>
        /// <returns></returns>
        private bool CompareArray(string str)
        {
            return this.CompareArray(str, this.mPasswordName);
        }
        /// <summary>
        /// 检查当前输入的内容dicVarBuf中的esn是否合法,如果合法则返回esn和其对应的数据
        /// </summary>
        /// <param name="__strEsnTemp">返回的数据</param>
        /// <param name="__dtEsnTemp">返回的数据</param>
        /// <returns>如果返回真则表示当前输入的esn所查找到的值均和当前输入的一致,
        /// 如果返回假则表示当前输入的esn查找到的内容至少有一项和当前输入不符/不存在</returns>
        private bool ChkEsn(ref string __strEsnTemp, ref  DataTable __dtEsnTemp)
        {
            #region 判断esn是否存在
            // __dtEsnTemp = null;
            string strErr = string.Empty;
            bool __bFlag = false;
            foreach (string str in dicVarBuf.Keys)
            {
                if (str.ToUpper() == "ESN")
                    __bFlag = true;
            }
            if (!__bFlag)
                return __bFlag;

            foreach (string item in dicVarBuf.Keys)
            {
                if (item.ToUpper() == "ESN")
                {
                    __strEsnTemp = dicVarBuf["ESN"];


                    //判断esn流程
                    if ((strErr = this.ChkRoute(__strEsnTemp, this.mCraftName).ToUpper()) != "OK")
                        throw new Exception(string.Format("输入的序列号[{0}]流程错误\n{1},请检查..", __strEsnTemp, strErr));

                    DataTable mDt = BLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyPart(__strEsnTemp));
                    foreach (DataRow dr in mDt.Rows)
                    {
                        __dtEsnTemp.Rows.Add(dr.ItemArray);
                    }

                    if (__dtEsnTemp != null && __dtEsnTemp.Rows.Count > 0)
                    {
                        //判断esn是否存在
                        if (__dtEsnTemp == null || __dtEsnTemp.Rows.Count < 1)
                            throw new Exception(string.Format("流程错误,当前序列号[{0}]不存在,请确认是否经过投板站..", __strEsnTemp));
                        //判断esn是否是当前工单的
                        if (__dtEsnTemp.Rows[0]["woId"].ToString() != this.mWoInfo.woId)
                            throw new Exception(string.Format("输入的序列号[{0}]不属于当前工单[{1}] ≠ [{2}],请检查..",
                                   __strEsnTemp, this.mWoInfo.woId, __dtEsnTemp.Rows[0]["woId"].ToString()));


                        //判断通过esn取出的数据是否和刷入的数据一致 如果一致则不需要再对输入的数据进行有效性验证,而是直接将值用于当前的数据
                        foreach (string strKey in this.dicVarBuf.Keys)
                        {
                            if (strKey.ToUpper() == "ESN")
                                continue;
                            DataRow[] dr = __dtEsnTemp.Select(string.Format("sntype='{0}'", strKey));
                            if (dr == null || dr.Length < 1)
                            {
                                __bFlag = false;
                                continue;
                            }
                            if (dr != null && dr.Length > 1)
                                throw new Exception(string.Format("严重错误!!,序列号[{0}] 在同一个ESN:[{1}] 下存在多次..", dicVarBuf[strKey], strKey));

                            if (string.IsNullOrEmpty(dr[0]["snval"].ToString()))
                                throw new Exception(string.Format("严重错误:{0}存在空值", strKey));

                            if (dr[0]["snval"].ToString().ToUpper().Trim() != this.dicVarBuf[strKey].ToUpper().Trim())
                                throw new Exception(string.Format("当前输入序列号的值[{0}] ≠ 历史数据[{1}]",
                                    this.dicVarBuf[strKey], dr[0]["snval"].ToString()));
                        }

                        break;
                    }
                    else
                    {
                        __bFlag = false;
                    }
                }
            }
            return __bFlag;
            #endregion
        }

        /// <summary>
        /// 检查当前输入内容
        /// </summary>
        /// <param name="__strEsnTemp"></param>
        /// <param name="__dtOtherSnTemp"></param>
        /// <param name="__InsertDtaTemp"></param>
        /// <param name="__dtEsnTemp"></param>
        private void ChkCurrentInput(ref string __strEsnTemp, ref DataTable __dtOtherSnTemp,
            ref Dictionary<string, string> __InsertDtaTemp, ref  DataTable __dtEsnTemp)
        {
            string strErr;
            foreach (string strKey in this.dicVarBuf.Keys)
            {
                if (strKey.ToUpper() != "ESN")
                {
                    #region _dtEsnTemp不为空
                    if (__dtEsnTemp != null && __dtEsnTemp.Rows.Count > 0)
                    {
                        DataRow[] _arrDr = __dtEsnTemp.Select(string.Format("sntype='{0}'", strKey));
                        if (_arrDr != null && _arrDr.Length > 1)
                            throw new Exception("同一个esn:" + __strEsnTemp + "类型" + strKey + "存在多次,请修正..");
                        if (_arrDr != null && _arrDr.Length == 1)
                        {
                            if (_arrDr[0]["snval"].ToString().ToUpper() != this.dicVarBuf[strKey].ToUpper())
                            {
                                throw new Exception(string.Format("esn:{0}当前输入的{1}={2}与历史数据{1}={3}不相符,请检查..",
                                    __strEsnTemp, strKey, this.dicVarBuf[strKey].ToUpper(), _arrDr[0]["snval"].ToString().ToUpper()));
                            }
                            else
                                continue;
                        }
                        //当前dtEsnTemp中没有当前的序列号类型则继续往下走
                    }
                    #endregion
                    __dtOtherSnTemp = BLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.ChkKeyParts(this.dicVarBuf[strKey],
                          string.Empty, this.mWoInfo.woId));
                    //如果返回的数据为空则表示该序列号还没有使用过
                    if (__dtOtherSnTemp == null || __dtOtherSnTemp.Rows.Count < 1)
                    {
                        __InsertDtaTemp.Add(strKey, this.dicVarBuf[strKey]);
                        // __dtEsnTemp.Rows.Add(__strEsnTemp, this.mWoInfo.woId, strKey, dicVarBuf[strKey]);//esn,woId,sntype,snval
                        continue;
                    }
                    //如果一个序列号返回的数据是多行 则表示数据重复
                    if (__dtOtherSnTemp != null && __dtOtherSnTemp.Rows.Count > 1)
                        throw new Exception(string.Format("序列号[{0}]重复,请确认..", this.dicVarBuf[strKey]));
                    //如果返回了一行数据 则需要比对返回的esn是否和其他的一致 否则提示不是一组数据
                    if (__dtOtherSnTemp != null && __dtOtherSnTemp.Rows.Count == 1)
                    {
                        if (string.IsNullOrEmpty(__strEsnTemp))
                        {
                            //判断根据序列号找到的序列号类型是否一致
                            if (__dtOtherSnTemp.Rows[0]["sntype"].ToString().ToUpper() != strKey.ToUpper())
                                throw new Exception(string.Format("序列号[{0}]当前对应的序列号类型为{1}号,与历史数据{2}类型不符,请检查..",
                                    this.dicVarBuf[strKey], strKey.ToUpper(), __dtOtherSnTemp.Rows[0]["sntype"].ToString().ToUpper()));

                            __strEsnTemp = __dtOtherSnTemp.Rows[0]["esn"].ToString().ToUpper();
                            //判断流程是否正确
                            //string strErr;
                            if ((strErr = this.ChkRoute(__strEsnTemp, this.mCraftName).ToUpper()) != "OK")
                                throw new Exception(string.Format("输入的序列号[{0}]流程错误\n{1},请检查..", __strEsnTemp, strErr));

                            //__dtEsnTemp.Rows.Add(__strEsnTemp, this.mWoInfo.woId, strKey, dicVarBuf[strKey]);
                            __dtEsnTemp = BLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyPart(__strEsnTemp));
                        }
                        else
                        {
                            //判断根据序列号找到的序列号类型是否一致
                            if (__dtOtherSnTemp.Rows[0]["sntype"].ToString().ToUpper() != strKey.ToUpper())
                                throw new Exception(string.Format("序列号[{0}]当前对应的序列号类型为{1}号,与历史数据{2}类型不符,请检查..",
                                    this.dicVarBuf[strKey], strKey.ToUpper(), __dtOtherSnTemp.Rows[0]["sntype"].ToString().ToUpper()));
                            //判断esn是否相等
                            if (__dtOtherSnTemp.Rows[0]["esn"].ToString().ToUpper() != __strEsnTemp.ToUpper())
                                throw new Exception(string.Format("数据绑定错误ESN:{0}≠{1},..",
                                    __strEsnTemp, __dtOtherSnTemp.Rows[0]["esn"].ToString()));
                            // __dtEsnTemp.Rows.Add(__strEsnTemp, this.mWoInfo.woId, strKey, dicVarBuf[strKey]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 检查模板公式下的变量
        /// </summary>
        /// <param name="__strEsnTemp"></param>
        /// <param name="_keyTemp">模板公式下变量产生的值</param>
        /// <param name="_formulasname">模板公式下变量的名称</param>
        /// <param name="__dtEsnTemp"></param>
        /// <param name="__dtKeyTemp"></param>
        /// <param name="__lsKeyEsn"></param>
        /// <param name="__dicKeysTemp"></param>
        /// <param name="__InsertDtaTemp"></param>
        private void ChkCurrAutoCreateInput(
            string __strEsnTemp,
            string _keyTemp,
            string _formulasname,
            DataTable __dtEsnTemp,
            out DataTable __dtKeyTemp,
            out List<string> __lsKeyEsn,
            out Dictionary<string, string> __dicKeysTemp,
            ref Dictionary<string, string> __InsertDtaTemp)
        {
            __dtKeyTemp = null;
            __lsKeyEsn = new List<string>();
            __dicKeysTemp = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(__strEsnTemp))
            {
                #region strEsn的值为空
                __dtKeyTemp = BLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.ChkKeyParts(_keyTemp, _formulasname,
                    this.mWoInfo.woId));

                //检查esn和序列号类型是否都匹配
                if (__dtKeyTemp == null || __dtKeyTemp.Rows.Count < 1)
                    __InsertDtaTemp.Add(_formulasname, _keyTemp);
                else
                {
                    foreach (DataRow dr in __dtKeyTemp.Rows)
                    {
                        if (CompareArray(dr["esn"].ToString(), __lsKeyEsn.ToArray()))
                        {
                            __lsKeyEsn.Add(dr["esn"].ToString()); //通过密码找到的esn不能用作当前esn但是需要拿来与当前esn做对比
                            //再加一个key的缓存
                            __dicKeysTemp.Add(dr["esn"].ToString(), _formulasname + "," + _keyTemp);//保存能够找到值的key和值
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region strEsn不为空
                if (__dtEsnTemp == null || __dtEsnTemp.Rows.Count < 1)
                {
                    #region dtEsn的数据为空
                    __dtKeyTemp = BLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.ChkKeyParts(_keyTemp,
                        _formulasname, this.mWoInfo.woId));

                    //检查esn和序列号类型是否都匹配
                    if (__dtKeyTemp == null || __dtKeyTemp.Rows.Count < 1)
                        __InsertDtaTemp.Add(_formulasname, _keyTemp);
                    else
                    {
                        DataRow[] arrDr = __dtKeyTemp.Select(string.Format("esn='{0}'", __strEsnTemp));
                        if (arrDr != null && arrDr.Length > 1)
                            throw new Exception("严重错误:密码数据重复..");
                        if (arrDr == null || arrDr.Length < 1)
                            __InsertDtaTemp.Add(_formulasname, _keyTemp);
                    }
                    #endregion
                }
                else
                {
                    #region dtEsn数据不为空
                    //查询通过esn找到的值中是否包含当前key的类型
                    DataRow[] _arrDr = __dtEsnTemp.Select(string.Format("sntype='{0}'", _formulasname));
                    if (_arrDr == null || _arrDr.Length < 1)
                    {
                        __InsertDtaTemp.Add(_formulasname, _keyTemp);
                    }
                    else if (_arrDr != null && _arrDr.Length > 1)
                    {
                        throw new Exception("同一个esn:" + __strEsnTemp + "下类型名称[" + _formulasname + "]重复..");
                    }
                    else
                    {
                        if (_arrDr[0]["snval"].ToString().ToUpper() != _keyTemp.ToUpper())
                            throw new Exception(string.Format("相同esn:{4}下,当前{0}={1},与历史记录{2}={3} 不相符,请检查..",
                                _formulasname, _keyTemp, _formulasname, _arrDr[0]["snval"].ToString(), __strEsnTemp));
                    }
                    #endregion
                }
                #endregion
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        private bool mPrintLable()
        {
            string log = string.Empty;
            //定义计时器     
            Stopwatch Mywatch = new Stopwatch();
            ShowMsg(mLogMsgType.Incoming, string.Format("{0}{1}", "初始化设备..", System.DateTime.Now.ToString("HH:mm:ss")));
            if (this.mLibdoc == null)
                throw new Exception("模板文件还没有初始化,请重新打开模板文件...");

            #region 局部变量
            string strErr = string.Empty;
            //标志位 是否需要进一步检测各个序列号的有效性
            bool __bFlag = true;
            //是否保存模板标志
            bool __IsSave = true;
            //缓存esn记录
            string __strEsnTemp = string.Empty;
            //根据esn获取到的表
            DataTable __dtEsnTemp = new DataTable("esn");
            __dtEsnTemp = new DataTable("esn");
            __dtEsnTemp.Columns.Add("esn", typeof(string));
            __dtEsnTemp.Columns.Add("woId", typeof(string));
            __dtEsnTemp.Columns.Add("sntype", typeof(string));
            __dtEsnTemp.Columns.Add("snval", typeof(string));
            //根据其他序列号或取得到的表
            DataTable __dtOtherSnTemp = null;
            // 保存需要记录到数据库的内容(部分数据可能已经存在于数据库中)
            Dictionary<string, string> __InsertDtaTemp = new Dictionary<string, string>();
            //所有的变量的名字
            string[] __arrAllVariableName = new string[MyVar.arrVariable.Length + MyVar.arrAutoSerialVariableName.Length];
            //所有变量的值
            string[] __arrAllVariableValue = new string[MyVar.arrVariable.Length + MyVar.arrAutoSerialVariableName.Length];
            //保存模板公式下变量的值或公式
            string[] __formutemp = null;
            //保存模板公式下变量定义的长度
            int[] __formulengtemp = null;
            #endregion
            try
            {
                this.mLibdoc.ViewMode = enumViewMode.lppxViewModeSize;
                #region 保存公式下所有变量的内容信息
                __formutemp = new string[this.mLibdoc.Variables.Formulas.Count];
                __formulengtemp = new int[__formutemp.Length];
                Dictionary<string, int> __formunameandleng = new Dictionary<string, int>();
                for (int h = 0; h < __formutemp.Length; h++)
                {
                    __formulengtemp[h] = this.mLibdoc.Variables.Formulas.Item(h + 1).Length;
                    __formutemp[h] = this.mLibdoc.Variables.Formulas.Item(h + 1).Expression;
                    __formunameandleng.Add(this.mLibdoc.Variables.Formulas.Item(h + 1).Name.ToUpper(),
                        this.mLibdoc.Variables.Formulas.Item(h + 1).Length);
                }
                #endregion

                #region 取出刷入的缓存内容
                __bFlag = this.ChkEsn(ref __strEsnTemp, ref __dtEsnTemp);
                if (!__bFlag)
                {
                    this.ChkCurrentInput(ref __strEsnTemp, ref __dtOtherSnTemp, ref __InsertDtaTemp, ref __dtEsnTemp);

                    for (int i = 0; i < this.mLibdoc.Variables.FormVariables.Count; i++)
                    {
                        this.mLibdoc.Variables.FormVariables.Item(i + 1).Value = this.dicVarBuf[this.mLibdoc.Variables.FormVariables.Item(i + 1).Name];
                    }
                }
                else
                {
                    ShowMsg(mLogMsgType.Outgoing, "输入的值在系统中已经存在,正在使用系统的值填充...");
                    foreach (string strKey in this.dicVarBuf.Keys)
                    {
                        if (strKey.ToUpper() != "ESN")
                        {
                            //将缓存中的值赋给模板变量
                            this.mLibdoc.Variables.FormVariables.Item(strKey).Value = this.dicVarBuf[strKey];
                            log += this.dicVarBuf[strKey] + "\t";
                        }
                    }
                }
                #endregion
                #region 处理模板自动产生的内容
                /************************************************************************************************
            *********在使用模板自动产生的值前先要根据手动输入的信息判断ESN是否存在，*************************
            *********如果存在那么查看存在的数据中是否有包含需要模板自动产生的序列号类型，********************
            *********如果有则使用查询到的值填充模板，反之使用模板自动产生的值，******************************
            *********然后使用模板自动产生的值与数据库比对查看是否有重复的数据,*******************************
            *********当前自动产生的数据是否有存在与另一个ESN上**********************************************/
                List<string> __lsKeyEsn = new List<string>();
                Dictionary<string, string> __dicKeysTemp = new Dictionary<string, string>();
                DataTable __dtKeyTemp = null;

                if (__dtEsnTemp != null && __dtEsnTemp.Rows.Count > 0)
                {
                    #region _存在esn的数据
                    for (int i = 0; i < this.mLibdoc.Variables.Formulas.Count; i++)
                    {
                        string _formulasname = this.mLibdoc.Variables.Formulas.Item(i + 1).Name;

                        //查看esn数据中是否存在模板中的序列号类型
                        DataRow[] arrTemp = __dtEsnTemp.Select(string.Format("sntype='{0}'", _formulasname));

                        if (arrTemp != null && arrTemp.Length > 1) //序列号类型相同超过一个
                            throw new Exception(string.Format("严重错误:相同的esn:[{1}]下序列号类型[{0}]存在多个,请检查..",
                                _formulasname, __strEsnTemp));
                        else if (arrTemp != null && arrTemp.Length == 1)
                        {
                            #region 找到了序号类型相同 且只有一个
                            if (this.CompareArray(_formulasname.ToUpper()))
                            {
                                #region 属于密码类型
                                string _keyTemp = string.Empty;

                                switch (this.mWoInfo.cpwd)
                                {
                                    case  T_WO_INFO.ecpwd.FILE:
                                        _keyTemp = this.mLibdoc.Variables.Formulas.Item(i + 1).Value.ToString();
                                        break;
                                    case T_WO_INFO.ecpwd.USERDEF:
                                        _keyTemp = this.mdicAllKey[_formulasname];
                                        break;
                                    default:
                                        _keyTemp = this.getMacKey(_formulasname, this.mAllKeys);
                                        break;
                                }

                                if (string.IsNullOrEmpty(_keyTemp))
                                    throw new Exception(string.Format("密码:[{0}]没有计算出值,请检查该产品是否需要改密码,或没有输入MAC号...", _formulasname));
                                if (arrTemp[0]["snval"].ToString().Trim() != _keyTemp.Trim())
                                    throw new Exception(string.Format("系统中已经存在的值{0}={1}与当前的值不一致{0}={2}",
                                        _formulasname, arrTemp[0]["snval"].ToString().Trim(), _keyTemp.Trim()));

                                if (this.mWoInfo.cpwd !=  T_WO_INFO.ecpwd.FILE)
                                {
                                    this.mLibdoc.Variables.Formulas.Item(i + 1).Prefix = string.Empty;
                                    this.mLibdoc.Variables.Formulas.Item(i + 1).Expression = string.Format("trim(\"{0}\")", _keyTemp);
                                    this.mLibdoc.Variables.Formulas.Item(i + 1).Length = __formulengtemp[i];
                                }
                                this.MyVar.arrAutoSerialVariableValue[i] = _keyTemp;
                                #endregion
                            }
                            else
                            {
                                #region 不属于密码类型
                                this.ShowMsg(mLogMsgType.Warning, "系统中存在了该序列号的值,正在使用系统的值填充..");
                                __IsSave = false;
                                this.mLibdoc.Variables.Formulas.Item(i + 1).Prefix = string.Empty;
                                this.mLibdoc.Variables.Formulas.Item(i + 1).Expression = string.Format("trim(\"{0}\")", arrTemp[0]["snval"].ToString());
                                this.mLibdoc.Variables.Formulas.Item(i + 1).Length = __formulengtemp[i];
                                this.MyVar.arrAutoSerialVariableValue[i] = arrTemp[0]["snval"].ToString();
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region  在存在的esn数据中没有找到模板中的序列号类型
                            //查询当前模板变量的名字是否是密码类型的
                            if (CompareArray(_formulasname.ToUpper()))
                            {
                                #region 属于密码类型
                                if (this.mWoInfo.cpwd != T_WO_INFO.ecpwd.FILE)
                                {
                                    #region   密码由程序dll产生
                                    string _keyTemp = string.Empty;
                                    switch (this.mWoInfo.cpwd)
                                    {
                                        case T_WO_INFO.ecpwd.USERDEF:
                                            _keyTemp = this.mdicAllKey[_formulasname];
                                            break;
                                        default:
                                            _keyTemp = this.getMacKey(_formulasname, this.mAllKeys);
                                            break;
                                    }
                                    if (string.IsNullOrEmpty(_keyTemp))
                                        throw new Exception(string.Format("密码:[{0}]没有计算出值,请检查该产品是否需要改密码,或没有输入MAC号...", _formulasname));

                                    this.ChkCurrAutoCreateInput(__strEsnTemp, _keyTemp, _formulasname, __dtEsnTemp,
                                        out __dtKeyTemp, out __lsKeyEsn, out __dicKeysTemp,
                                        ref __InsertDtaTemp);

                                    this.mLibdoc.Variables.Formulas.Item(i + 1).Prefix = string.Empty;
                                    this.mLibdoc.Variables.Formulas.Item(i + 1).Expression = string.Format("trim(\"{0}\")", _keyTemp);
                                    this.mLibdoc.Variables.Formulas.Item(i + 1).Length = __formulengtemp[i];
                                    this.MyVar.arrAutoSerialVariableValue[i] = _keyTemp;
                                    #endregion
                                }
                                else
                                {
                                    #region     密码由模板自动产生
                                    string _keyTemp = this.mLibdoc.Variables.Formulas.Item(i + 1).Value.ToString();
                                    ChkCurrAutoCreateInput(__strEsnTemp, _keyTemp, _formulasname, __dtEsnTemp,
                                        out __dtKeyTemp, out __lsKeyEsn, out __dicKeysTemp,
                                        ref __InsertDtaTemp);

                                    this.mLibdoc.Variables.Formulas.Item(i + 1).Prefix = string.Empty;
                                    this.MyVar.arrAutoSerialVariableValue[i] = this.mLibdoc.Variables.Formulas.Item(i + 1).Value.ToString();
                                    this.mLibdoc.Variables.Formulas.Item(i + 1).Length = __formulengtemp[i];
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                #region  不是密码类型
                                string ValTemp = string.Empty;
                                ValTemp = this.mLibdoc.Variables.Formulas.Item(this.MyVar.arrAutoSerialVariableName[i]).Value.ToString();
                                if (ValTemp.Length != __formunameandleng[this.MyVar.arrAutoSerialVariableName[i].ToUpper()])
                                    throw new Exception("模板产生的序列号长度与设置的长度不符!!");

                                if (__dtEsnTemp != null && __dtEsnTemp.Rows.Count > 0)
                                {
                                    #region 如果dtEsn数据有值，则比对模板中的序列号类型是否在dtEsn数据中
                                    DataRow[] _arrDr = __dtEsnTemp.Select(string.Format("sntype='{0}'", _formulasname));
                                    if (_arrDr != null && _arrDr.Length > 1)
                                        throw new Exception("同一个esn:" + __strEsnTemp + "类型" + _formulasname + "存在多次,请修正..");
                                    if (_arrDr != null && _arrDr.Length == 1)
                                    {
                                        if (_arrDr[0]["snval"].ToString().ToUpper() != ValTemp.ToUpper())
                                        {
                                            throw new Exception(string.Format("esn:{0}当前输入的{1}={2}与历史数据{1}={3}不相符,请检查..",
                                                __strEsnTemp, _formulasname, ValTemp.ToUpper(), _arrDr[0]["snval"].ToString().ToUpper()));
                                        }
                                        else
                                            continue;
                                    }
                                    #endregion
                                }

                                DataTable _atoSnTemp = BLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.ChkKeyParts(ValTemp,
                                    string.Empty, this.mWoInfo.woId));
                                if (_atoSnTemp != null && _atoSnTemp.Rows.Count > 1)
                                    throw new Exception("严重错误:模板自动产生的序列号[" + ValTemp + "]重复,请检查..");
                                if (_atoSnTemp == null || _atoSnTemp.Rows.Count < 1)
                                    __InsertDtaTemp.Add(_formulasname, ValTemp);
                                else
                                {
                                    if (string.IsNullOrEmpty(__strEsnTemp))
                                    {
                                        this.ShowMsg(mLogMsgType.Warning, "提示:ESN为空，请检查数据的准确性..");
                                        if (_atoSnTemp != null && _atoSnTemp.Rows.Count == 1)
                                        {
                                            __InsertDtaTemp.Add(_formulasname, ValTemp);
                                            __strEsnTemp = _atoSnTemp.Rows[0]["esn"].ToString();
                                            //检查流程
                                            if ((strErr = this.ChkRoute(__strEsnTemp, this.mCraftName).ToUpper()) != "OK")
                                                throw new Exception(strErr);

                                            DataTable _mdt = BLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyPart(__strEsnTemp));
                                            //比对通过模板自动产生的值在数据库中找到的内容与手动输入的值是否相符
                                            foreach (string item in this.dicVarBuf.Keys)
                                            {
                                                DataRow[] _aDr = _mdt.Select(string.Format("sntype='{0}'", item));
                                                //如果没有找到则表示还没有记录过,如果找到了数据那么就需要比对值是否相同
                                                if (_aDr != null && _aDr.Length > 1)
                                                    throw new Exception("严重错误:序列号类型名称" + item + "重复");
                                                if (_aDr != null && _aDr.Length == 1)
                                                {
                                                    if (_aDr[0]["snval"].ToString().ToUpper() != this.dicVarBuf[item].ToUpper())
                                                        throw new Exception(string.Format("序列号{0}:当前值与历史值不相同:{1}≠{2}",
                                                            item, this.dicVarBuf[item], _aDr[0]["snval"].ToString()));
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (_atoSnTemp != null && _atoSnTemp.Rows.Count == 1)
                                        {
                                            if (_atoSnTemp.Rows[0]["esn"].ToString().ToUpper() != __strEsnTemp)
                                            {
                                                throw new Exception(string.Format("序列号绑定错误:序列号[{0}]已被其他的产品esn:[{1}]使用过,不能再使用",
                                                    ValTemp, _atoSnTemp.Rows[0]["esn"].ToString().ToUpper()));
                                            }
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
                    for (int i = 0; i < this.mLibdoc.Variables.Formulas.Count; i++)
                    {
                        #region  取出模板里公式下所有变量的值
                        string _formulasname = this.mLibdoc.Variables.Formulas.Item(i + 1).Name;
                        //查询当前模板变量的名字是否是密码类型的
                        if (CompareArray(_formulasname.ToUpper()))
                        {
                            #region 属于密码类型
                            if (this.mWoInfo.cpwd != T_WO_INFO.ecpwd.FILE)
                            {
                                #region   密码由程序dll产生
                                string _keyTemp = string.Empty;

                                switch (this.mWoInfo.cpwd)
                                {
                                    case T_WO_INFO.ecpwd.USERDEF:
                                        _keyTemp = this.mdicAllKey[_formulasname];
                                        break;
                                    default:
                                        _keyTemp = this.getMacKey(_formulasname, this.mAllKeys);
                                        break;
                                }
                                if (string.IsNullOrEmpty(_keyTemp))
                                    throw new Exception(string.Format("密码:[{0}]没有计算出值,请检查该产品是否需要改密码,或没有输入MAC号...", _formulasname));

                                this.ChkCurrAutoCreateInput(__strEsnTemp, _keyTemp, _formulasname, __dtEsnTemp,
                                    out __dtKeyTemp, out __lsKeyEsn, out __dicKeysTemp,
                                    ref __InsertDtaTemp);

                                this.mLibdoc.Variables.Formulas.Item(i + 1).Prefix = string.Empty;
                                this.mLibdoc.Variables.Formulas.Item(i + 1).Expression = string.Format("trim(\"{0}\")", _keyTemp);
                                this.mLibdoc.Variables.Formulas.Item(i + 1).Length = __formulengtemp[i];
                                this.MyVar.arrAutoSerialVariableValue[i] = _keyTemp;
                                #endregion
                            }
                            else
                            {
                                #region     密码由模板自动产生
                                string _keyTemp = this.mLibdoc.Variables.Formulas.Item(i + 1).Value.ToString();
                                ChkCurrAutoCreateInput(__strEsnTemp, _keyTemp, _formulasname, __dtEsnTemp,
                                    out __dtKeyTemp, out __lsKeyEsn, out __dicKeysTemp,
                                    ref __InsertDtaTemp);

                                this.mLibdoc.Variables.Formulas.Item(i + 1).Prefix = string.Empty;
                                this.MyVar.arrAutoSerialVariableValue[i] = this.mLibdoc.Variables.Formulas.Item(i + 1).Value.ToString();
                                this.mLibdoc.Variables.Formulas.Item(i + 1).Length = __formulengtemp[i];
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region  不是密码类型
                            string ValTemp = string.Empty;
                            ValTemp = this.mLibdoc.Variables.Formulas.Item(i + 1).Value.ToString();
                            if (ValTemp.Length != __formunameandleng[this.mLibdoc.Variables.Formulas.Item(i + 1).Name.ToUpper()])
                                throw new Exception("模板产生的序列号长度与设置的长度不符!!");
                            if (__dtEsnTemp != null && __dtEsnTemp.Rows.Count > 0)
                            {
                                this.ShowMsg(mLogMsgType.Warning, "提示:检查数据准确性");
                                #region 如果esn数据存在
                                DataRow[] _arrDr = __dtEsnTemp.Select(string.Format("sntype='{0}'", _formulasname));
                                if (_arrDr != null && _arrDr.Length > 1)
                                    throw new Exception("同一个esn:" + __strEsnTemp + "类型" + _formulasname + "存在多次,请修正..");
                                if (_arrDr != null && _arrDr.Length == 1)
                                {
                                    if (_arrDr[0]["snval"].ToString().ToUpper() != ValTemp.ToUpper())
                                    {
                                        throw new Exception(string.Format("esn:{0}当前输入的{1}={2}与历史数据{1}={3}不相符,请检查..",
                                            __strEsnTemp, _formulasname, ValTemp.ToUpper(), _arrDr[0]["snval"].ToString().ToUpper()));
                                    }
                                    else
                                        continue;
                                }
                                #endregion
                            }

                            DataTable _atoSnTemp = BLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.ChkKeyParts(ValTemp,
                                string.Empty, this.mWoInfo.woId));
                            if (_atoSnTemp != null && _atoSnTemp.Rows.Count > 1)
                                throw new Exception("严重错误:模板自动产生的序列号[" + ValTemp + "]重复,请检查..");
                            if (_atoSnTemp == null || _atoSnTemp.Rows.Count < 1)
                                __InsertDtaTemp.Add(_formulasname, ValTemp);
                            else
                            {
                                if (string.IsNullOrEmpty(__strEsnTemp))
                                {
                                    #region strEsn为空
                                    if (_atoSnTemp != null && _atoSnTemp.Rows.Count == 1)
                                    {
                                        __InsertDtaTemp.Add(_formulasname, ValTemp);
                                        __strEsnTemp = _atoSnTemp.Rows[0]["esn"].ToString();
                                        if ((strErr = this.ChkRoute(__strEsnTemp, this.mCraftName).ToUpper()) != "OK")
                                            throw new Exception(strErr);
                                        DataTable _mdt = BLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyPart(__strEsnTemp));
                                        //比对通过模板自动产生的值在数据库中找到的内容与手动输入的值是否相符
                                        foreach (string item in this.dicVarBuf.Keys)
                                        {
                                            DataRow[] _aDr = _mdt.Select(string.Format("sntype='{0}'", item));
                                            //如果没有找到则表示还没有记录过,如果找到了数据那么就需要比对值是否相同
                                            if (_aDr != null && _aDr.Length > 1)
                                                throw new Exception("严重错误:序列号类型名称" + item + "重复");
                                            if (_aDr != null && _aDr.Length == 1)
                                            {
                                                if (_aDr[0]["snval"].ToString().ToUpper() != this.dicVarBuf[item].ToUpper())
                                                    throw new Exception(string.Format("序列号{0}:当前值与历史值不相同:{1}≠{2}",
                                                        item, this.dicVarBuf[item], _aDr[0]["snval"].ToString()));
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    if (_atoSnTemp != null && _atoSnTemp.Rows.Count == 1)
                                    {
                                        if (_atoSnTemp.Rows[0]["esn"].ToString().ToUpper() != __strEsnTemp)
                                        {
                                            throw new Exception(string.Format("序列号绑定错误:序列号[{0}]已被其他的产品esn:[{1}]使用过,不能再使用",
                                                ValTemp, _atoSnTemp.Rows[0]["esn"].ToString().ToUpper()));
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
                if (string.IsNullOrEmpty(__strEsnTemp))
                {
                    throw new Exception("没有发现任何esn序列号,请检查..");
                }

                foreach (string item in __dicKeysTemp.Keys)
                {
                    if (item.ToUpper() != __strEsnTemp.ToUpper())
                    {
                        __InsertDtaTemp.Add(__dicKeysTemp[item].Split(',')[0], __dicKeysTemp[item].Split(',')[1]);
                    }
                }

                if (!CHECK_PRODUCT_LINE())
                    throw new Exception("请切换线别");


                this.ShowMsg(mLogMsgType.Outgoing, "正在记录数据..");
              //  List<WebServices.tWipKeyPart.tWipKeyPartTable> wipkeyparts = new List<WebServices.tWipKeyPart.tWipKeyPartTable>();
                IList<IDictionary<string, object>> LsDic = new List<IDictionary<string, object>>();
                foreach (string item in __InsertDtaTemp.Keys)
                {
                    //再一次对插入数据库的数据进行比对是否输入与当前工单的范围
                    if (!this.CompareArray(item.ToUpper()))
                    {
                        if (!this.CompareSerialnumber(this.mWoInfo.woId, __InsertDtaTemp[item], item))
                            throw new Exception(string.Format("序列号{0}:{1}不在工单设置范围内,请检查..", item, __InsertDtaTemp[item]));
                    }

                    dic = new Dictionary<string, object>();
                    dic.Add("ESN", __strEsnTemp);
                    dic.Add("SNTYPE", item);
                    dic.Add("SNVAL", __InsertDtaTemp[item]);
                    dic.Add("WOID", this.mWoInfo.woId);
                    dic.Add("STATION", mCraftName);
                    dic.Add("KPNO", "NA");
                    LsDic.Add(dic);

                    //wipkeyparts.Add(new WebServices.tWipKeyPart.tWipKeyPartTable()
                    //{
                    //    esn = __strEsnTemp,
                    //    sntype = item,
                    //    snval = __InsertDtaTemp[item],
                    //    woId = this.mWoInfo.woId,
                    //    station = mCraftName,
                    //    KPNO="NA"
                    //});
                }
                this.ShowMsg(mLogMsgType.Incoming, "数据记录完成");

                #region 重复打印不记录过站信息 和 序列号匹配信息 2013-4-13
                if (!this.mRprint)
                {
                    strErr = string.Empty;
                    this.ShowMsg(mLogMsgType.Outgoing, "正在保存过站信息..");
                    
                 
                    #region ====记录过站信息
                    strErr = refWebtWipKeyPart.Instance.InsertKeyParts(MapListConverter.ListDictionaryToJson(LsDic));
                    strErr = string.IsNullOrEmpty(strErr) ? "OK" : strErr;
                    if (strErr!="OK")
                        throw new Exception(strErr);

                    strErr = refWebtPublicStoredproc.Instance.SP_TEST_MAIN_ONLY(__strEsnTemp, mCraftName, this.mUserInfo.userId + "-" + this.mUserInfo.pwd, "NA", this.mLineInfo.lineId);
           
                    if (strErr.ToUpper() != "OK")
                        throw new Exception(strErr + "\n过站失败!!");
                    this.ShowMsg(mLogMsgType.Incoming, "过站信息保存完成");
                 
                    #endregion
                }
                else
                {
                    //记录重复打印记录

                    dic = new Dictionary<string, object>();
                    dic.Add("USERID", this.strEnaPwd.Split('-')[0]);
                    dic.Add("PRG_NAME", "REPEATPRINT");
                    dic.Add("ACTION_TYPE", "PRINT");
                    dic.Add("ACTION_DESC", "PRINT: " + __strEsnTemp);
                    refWebRecodeSystemLog.Instance.InsertSystemLog(MapListConverter.DictionaryToJson(dic));
                    this.strEnaPwd = string.Empty;
                }
                #endregion
                for (int x = 0; x < this.MyVar.arrAutoSerialVariableName.Length; x++)
                {
                    FillTextBOX(arrTextboxReadOnly[x], this.mLibdoc.Variables.Formulas.Item(arrTextboxReadOnly[x].Name).Value.ToString());
                }
                this.ShowMsg(mLogMsgType.Outgoing, "正在打印标签....");
                this.mLibdoc.PrintDocument(this.mPrintNumber);
                this.ShowMsg(mLogMsgType.Incoming, "标签打印完成");

                this.ShowMsg(mLogMsgType.Outgoing, "正在初始化模板....");
                for (int y = 0; y < __formutemp.Length; y++)
                {
                    this.mLibdoc.Variables.Formulas.Item(y + 1).Prefix = "";
                    this.mLibdoc.Variables.Formulas.Item(y + 1).Expression = __formutemp[y];
                    this.mLibdoc.Variables.Formulas.Item(y + 1).Length = __formulengtemp[y];
                }
                if (!this.mRprint)
                {
                    if (__IsSave)
                        this.mLibdoc.Save();
                }
                this.ShowMsg(mLogMsgType.Incoming, "全部完成");
                return true;
            }
            catch (Exception ex)
            {
                this.ShowMsg(mLogMsgType.Outgoing, "正在初始化模板....");
                for (int y = 0; y < __formutemp.Length; y++)
                {
                    this.mLibdoc.Variables.Formulas.Item(y + 1).Prefix = "";
                    this.mLibdoc.Variables.Formulas.Item(y + 1).Expression = __formutemp[y];
                    this.mLibdoc.Variables.Formulas.Item(y + 1).Length = __formulengtemp[y];
                }
                this.ShowMsg(mLogMsgType.Error, ex.Message);
                return false;
            }
            finally
            {
                if (this.mRprint)
                {
                    this.mLibdoc.Close(false);
                    this.mlppx.Documents.CloseAll(false);
                    this.mlppx.Quit();
                    this.mlppx = new ApplicationClass();
                    this.mLibdoc = this.mlppx.Documents.Open(this.mPrintFileName, false);
                    this.mLibdoc.Activate();
                    this.ShowMsg(mLogMsgType.Incoming, "关闭重复打印");
                }
                this.mRprint = false;

            }
        }
        /// <summary>
        /// 根据密码的名称获取根据mac计算出来的密码值
        /// </summary>
        /// <param name="_strName">密码名称</param>
        /// <param name="_keys">密码值列表</param>
        /// <returns>返回对应的密码值</returns>
        private string getMacKey(string _strName, List<string> _keys)
        {
            string _key = string.Empty;
            if (_keys == null || _keys.Count < 1)
                return _key;
            try
            {
                switch (_strName.ToUpper())
                {
                    case "SSID":
                        _key = _keys.ToArray()[0];
                        break;
                    case "WEPKEY":
                        _key = _keys.ToArray()[1];
                        break;
                    case "PIN":
                        _key = _keys.ToArray()[2];
                        break;
                    case "DEK":
                        _key = _keys.ToArray()[3];
                        break;
                    case "AES":
                        _key = _keys.ToArray()[4];
                        break;
                    default:
                        break;
                }
                return _key;
            }
            catch
            {
                throw new Exception("Key名称有误: FrmMain 952");
            }
        }
        /// <summary>
        /// 检查系统进程中是否存在指定的进程
        /// </summary>
        /// <param name="prcname">进程名称</param>
        /// <returns>存在则返回真</returns>
        private bool checkprocessisrun(string prcname)
        {
            Process[] prc = Process.GetProcessesByName(prcname);
            if (prc.Length < 1)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 结束指定的进程
        /// </summary>
        /// <param name="prcname"></param>
        private void closeproc(string prcname)
        {
            Process[] prc = Process.GetProcessesByName(prcname.Substring(0, prcname.LastIndexOf('.')));
            if (prc.Length > 0)
                foreach (Process pc in prc)
                {
                    pc.Kill();
                }
        }
        /// <summary>
        /// 杀死进程(目前写死为lppa.exe)
        /// </summary>
        private void KillAllProcess()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Arguments = "/c taskkill /im lppa.exe /f";
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.Start();
            cmd.Close();
        }
        /// <summary>
        /// 获取当前软件版本
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        /// <summary>
        /// 自动运行指定的程序
        /// </summary>
        /// <param name="dir">所在路径</param>
        /// <param name="localFileName">程序名称</param>
        /// <param name="thisappname"></param>
        private static void RunFile(string dir, string localFileName, string thisappname)
        {
            try
            {
                if (File.Exists(Path.Combine(dir, localFileName)))
                {
                    Process myProcess = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = dir + localFileName;
                    psi.WorkingDirectory = dir;
                    psi.UseShellExecute = false;
                    psi.Arguments = thisappname;
                    psi.RedirectStandardError = true;
                    psi.CreateNoWindow = true;
                    // psi.RedirectStandardOutput = true;
                    psi.WindowStyle = ProcessWindowStyle.Normal;
                    myProcess.StartInfo = psi;
                    myProcess.Start();
                    myProcess.WaitForExit(20);
                    myProcess.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":Application Run Error");
            }
        }
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
        /// <summary>
        /// 显示进度条
        /// </summary>
        /// <param name="ival"></param>
        private void ShowProgressbar(int ival)
        {
            this.statusStrip1.Invoke(new EventHandler(delegate
            {
                this.runprogbar.Value = ival;
            }));
        }
        /// <summary>
        /// 设置进度条的最大值
        /// </summary>
        /// <param name="ival"></param>
        private void SetProgressbarMaxValue(int ival)
        {
            this.statusStrip1.Invoke(new EventHandler(delegate
            {
                this.runprogbar.Maximum = ival;
            }));
        }

        /// <summary>
        /// 初始化自定义控件的内容（针对其他标签打印的界面控件）
        /// </summary>
        private void InitCtlPannel()
        {
            this.gpcartonprint.Invoke(new EventHandler(delegate
            {
                foreach (Control tb in gpotherprint.Controls)
                {
                    if (tb is MyTextBox)
                        tb.Text = "";
                }
            }));
        }
        /// <summary>
        /// 将打开的模板变量信息记录到内存中
        /// </summary>
        /// <param name="_FileName"></param>
        /// <param name="_tempNull"></param>
        private void InitLab(string _FileName)
        {
            this.MyVar.DicVarLen = new Dictionary<string, int>();
            List<string> vname = new List<string>();
            List<string> vallname = new List<string>();
            List<string[]> arrname = new List<string[]>();
            this.lsAllVarName.Clear();
            this.MyVar.DicVarLen.Clear();
            string vtemp = string.Empty;
            mCartonFlag = false;
            try
            {
                this.mLibdoc = this.mlppx.Documents.Open(_FileName, false);

                this.mLibdoc.ViewMode = enumViewMode.lppxViewModeSize;

                int _formvariablecount = mLibdoc.Variables.FormVariables.Count;

                int _formmulascount = mLibdoc.Variables.Formulas.Count;

                #region xxx【_formvariablecount】
                for (int cu = 1; cu <= mLibdoc.Variables.FormVariables.Count; cu++)//读取的是填充器下的变量
                {
                    if (Regex.Replace(mLibdoc.Variables.FormVariables.Item(cu).Name, @"\d", "").Trim() != vtemp)
                    {
                        if (vname.IndexOf(Regex.Replace(mLibdoc.Variables.FormVariables.Item(cu).Name, @"\d", "").Trim(),
                            0, vname.Count) == -1)
                        {
                            vname.Add(Regex.Replace(mLibdoc.Variables.FormVariables.Item(cu).Name, @"\d", "").Trim());
                        }
                    }
                    vallname.Add(vtemp = Regex.Replace(mLibdoc.Variables.FormVariables.Item(cu).Name, @"\d", "").Trim());
                }

                foreach (string str in vname)
                {
                    if (str.ToUpper() != "CARTONNUMBER")
                        arrname.Add(vallname.FindAll(delegate(string ss) { return ss == str; }).ToArray());
                    else
                        mCartonFlag = true;
                }
                for (int xxx = 0; xxx < arrname.Count; xxx++)
                {
                    if (arrname[xxx].Length != arrname[arrname.Count - 1].Length)
                    {
                        throw new Exception("模板变量数量设置不对称,请修正..");
                    }
                }
                this.miCatonBoxTotal = arrname[0].Length;
                #endregion

                MyVar.arrVariable = new string[_formvariablecount];
                MyVar.arrVariableCount = new int[_formvariablecount];

                MyVar.arrAutoSerialVariableName = new string[mLibdoc.Variables.Formulas.Count];//读取的是公式下的变量
                MyVar.arrAutoSerialVariableValue = new string[mLibdoc.Variables.Formulas.Count];

                for (int i = 0; i < MyVar.arrVariableCount.Length; i++)
                {
                    this.lsAllVarName.Add(MyVar.arrVariable[i] = mLibdoc.Variables.FormVariables.Item(i + 1).Name);
                    MyVar.arrVariableCount[i] = mLibdoc.Variables.FormVariables.Item(i + 1).Length;
                    MyVar.DicVarLen.Add(mLibdoc.Variables.FormVariables.Item(i + 1).Name, mLibdoc.Variables.FormVariables.Item(i + 1).Length);
                }

                for (int x = 0; x < MyVar.arrAutoSerialVariableName.Length; x++)
                {
                    this.lsAllVarName.Add(MyVar.arrAutoSerialVariableName[x] = mLibdoc.Variables.Formulas.Item(x + 1).Name);
                    MyVar.arrAutoSerialVariableValue[x] = mLibdoc.Variables.Formulas.Item(x + 1).Value;
                }

                //arrVariableBuffer = new string[MyVar.arrVariable.Length];
                this.dicVarBuf.Clear();
                mLibdoc.CopyToClipboard();
                this.ShowPicture(Clipboard.GetImage());


                string strTemp = string.Empty;
                bool bflag = false;
                for (int x = 1; x < mLibdoc.Variables.FormVariables.Count; x++)
                {
                    string str = mLibdoc.Variables.FormVariables.Item(x).Name;//遍历填充器下的每个变量名称
                    int flag = 0;
                    string s = string.Empty;
                    for (int i = 0; i < str.Trim().Length; i++)
                    {
                        flag = i;
                        if (isNumberic(s = str.Substring(i, 1)))
                        {
                            flag = i;
                            break;
                        }

                    }
                    if (flag < 1)
                        throw new Exception("变量名称第一位不能是数字");
                    if (string.IsNullOrEmpty(strTemp))
                    {
                        strTemp = str.Substring(0, flag);
                    }
                    else
                    {
                        if (strTemp.ToUpper() == str.Substring(0, flag).ToUpper())
                        {
                            bflag = true;
                            break;
                        }
                    }

                }

                //添加智能选择功能区
                this.bMask = false;
                if (bflag)//_formvariablecount >= 5
                {
                    //选择卡通箱标签打印区

                    this.tbcLable.SelectedTabIndex = 1;
                }
                else
                {
                    //选择其他标签打印区
                    this.tbcLable.SelectedTabIndex = 0;
                }
                // this.pictureBox1.Image = Clipboard.GetImage();
            }
            catch (Exception ex)
            {
                this.mLibdoc = null;
                ShowMsg(mLogMsgType.Error, "控件及变量初始化失败\n" + ex.Message);
            }
        }
        private void ShowPicture(System.Drawing.Image img)
        {
            this.pictureBox1.Invoke(new EventHandler(delegate
            {
                this.pictureBox1.Image = img;
                this.pictureBox1.Refresh();
            }));
        }
        /// <summary>
        /// 清空剪切板
        /// </summary>
        private void ClipboardImageClear()
        {
            Clipboard.Clear();
        }
        /// <summary>
        /// 加载工单所有的序列号区间到本地数据库以减轻服务器的压力
        /// </summary>
        private void DownloadWoSnRule()
        {
            this.ShowMsg(mLogMsgType.Warning, "正在加载工单序列号区间..");
            BLL.cdbAccess ass = new cdbAccess();
            ass.ExecuteSqlCommand("delete from wosnrule");
            DataTable DtwoSnrule = BLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoSnRule(this.mWoInfo.woId, string.Empty));
            this.SetProgressbarMaxValue(DtwoSnrule.Rows.Count);
            int i = 0;
            foreach (DataRow dr in DtwoSnrule.Rows)
            {
                if (dr["sntype"].ToString().ToUpper() != "MAC")
                {
                    i++;
                    string sql = string.Format("insert into wosnrule(poid,woId,sntype,snstart,snend,snprefix,snpostfix,ver,reve,usenum) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                        "NA",
                        dr["woId"].ToString(),
                        dr["sntype"].ToString(),
                        dr["snstart"].ToString(),
                        dr["snend"].ToString(),
                        dr["snprefix"].ToString(),
                        dr["snpostfix"].ToString(),
                        dr["ver"].ToString(),
                        dr["reve"].ToString(),
                        dr["usenum"].ToString());
                    ass.ExecuteSqlCommand(sql);
                    this.ShowProgressbar(i);
                }
            }
            this.ShowMsg(mLogMsgType.Warning, "工单序列号区间加载完成.");
        }

        /// <summary>
        /// 显示统计信息
        /// </summary>
        /// <param name="CurrentValue">当前的值</param>
        /// <param name="Total">总数</param>
        private void ShowStation(string CurrentValue, string Total)
        {
            this.lbcurentstation.Invoke(new EventHandler(delegate
            {
                this.lbcurentstation.Text = string.Format("{0}/{1}", CurrentValue, Total);
                this.lbcurentstation.Refresh();
            }));
        }

        /// <summary>
        /// 显示当前卡通箱包装的数量
        /// </summary>
        /// <param name="currentValue"></param>
        private void ShowCartonStation(string currentValue)
        {
            this.lb_cartoncount.Invoke(new EventHandler(delegate
            {
                this.lb_cartoncount.Text = string.Format("{0}/{1}", currentValue, this.miCatonBoxTotal);
                this.lb_cartoncount.Refresh();
            }));
        }
        /// <summary>
        /// 设置卡通箱号
        /// </summary>
        /// <param name="cartonId"></param>
        private void SetCartonID(string cartonId)
        {
            this.tb_Boxcount.Invoke(new EventHandler(delegate
            {
                this.tb_Boxcount.Text = cartonId;
                this.tb_Boxcount.Refresh();
            }));
        }

        /// <summary>
        /// 根据模板内容显示对应的控件
        /// </summary>
        /// <param name="MyVar"></param>
        private void ShowControl(MyVariable MyVar)
        {
            this.gpcartonprint.Invoke(new EventHandler(delegate
            {
                this.gpotherprint.Controls.Clear();
                #region xxxxx
                arrTextbox = new MyTextBox[MyVar.arrVariable.Length];
                arrLabel = new Label[MyVar.arrVariableCount.Length];

                arrTextboxReadOnly = new MyTextBox[MyVar.arrAutoSerialVariableName.Length];
                arrLabelReadOnly = new Label[MyVar.arrAutoSerialVariableValue.Length];

                int RowHeight = 37;

                Point TextBoxPoint = new Point(220, 24);
                Point LabelPoint = new Point(80, 30);

                Size TextBoxLocation = new Size(100, 20);
                Size LabelLocation = new Size(5, 20);

                Point ReadOnlyTextBoxPoint = new Point(220, 24);
                Point ReadOnlyLabelPoint = new Point(80, 30);

                Size ReadOnlyTextBoxLocation = new Size(100, (this.gpotherprint.Height - 45));
                Size ReadOnlyLabelLocation = new Size(5, (this.gpotherprint.Height - 45));

                Point CheckBoxPoint = new Point(80, 25);
                Size CheckBoxLoction = new Size(430, 250);
                ShowDataChk = new CheckBox();
                ShowDataChk.BackColor = Color.Transparent;
                ShowDataChk.Location = new System.Drawing.Point(CheckBoxLoction);
                ShowDataChk.Size = new System.Drawing.Size(CheckBoxPoint);
                ShowDataChk.Text = "显示数据";
                ShowDataChk.CheckedChanged += new EventHandler(ShowDataChk_CheckedChanged);
                #endregion
                try
                {
                    #region xxxxx
                    for (int x = 0; x < MyVar.arrVariable.Length; x++)
                    {
                        arrTextbox[x] = new MyTextBox();
                        arrLabel[x] = new Label();
                        arrLabel[x].BackColor = Color.Transparent;
                        arrTextbox[x].Location = new System.Drawing.Point(TextBoxLocation);
                        arrTextbox[x].Font = new System.Drawing.Font("宋体", 15);
                        arrLabel[x].Location = new System.Drawing.Point(LabelLocation);
                        arrLabel[x].Font = new System.Drawing.Font("宋体", 13, FontStyle.Bold);
                        arrTextbox[x].Size = new System.Drawing.Size(TextBoxPoint);
                        arrLabel[x].Size = new System.Drawing.Size(LabelPoint);
                        arrTextbox[x].TabIndex = x + 5;
                        arrTextbox[x].KeyDown += new KeyEventHandler(textbox_KeyDown);
                        arrTextbox[x].Leave += new EventHandler(textbox_Leave);
                        if ((arrTextbox[x].Name = MyVar.arrVariable[x]) == "MAC")
                        {
                            //getMacAddressChk = new CheckBox();
                            //getMacAddressChk.Text = "自动获取MAC地址";
                            //getMacAddressChk.Location = new System.Drawing.Point(TextBoxPoint.X + 150, TextBoxPoint.Y + 2);
                            //getMacAddressChk.CheckedChanged += new EventHandler(getMacAddressChk_CheckedChanged);
                        }

                        arrLabel[x].Text = MyVar.arrVariable[x] + ":";
                        arrLabel[x].TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        TextBoxLocation.Height += RowHeight;
                        LabelLocation.Height += RowHeight;
                    }
                    this.gpotherprint.Controls.Add(ShowDataChk);
                    this.gpotherprint.Controls.AddRange(arrTextbox);
                    this.gpotherprint.Controls.AddRange(arrLabel);
                    //  this.ControlBox.Controls.Add(getMacAddressChk);
                    arrTextbox[0].Focus();
                    #endregion

                    #region xxxxxx
                    for (int i = 0; i < MyVar.arrAutoSerialVariableName.Length; i++)
                    {
                        arrTextboxReadOnly[i] = new MyTextBox();

                        arrTextboxReadOnly[i].Location = new System.Drawing.Point(ReadOnlyTextBoxLocation);
                        arrTextboxReadOnly[i].Font = new System.Drawing.Font("宋体", 15);
                        arrTextboxReadOnly[i].Size = new System.Drawing.Size(ReadOnlyTextBoxPoint);
                        arrTextboxReadOnly[i].Name = MyVar.arrAutoSerialVariableName[i];
                        arrTextboxReadOnly[i].Text = MyVar.arrAutoSerialVariableValue[i];
                        arrTextboxReadOnly[i].ReadOnly = true;
                        ReadOnlyTextBoxLocation.Height += -35;

                        arrLabelReadOnly[i] = new Label();
                        arrLabelReadOnly[i].BackColor = Color.Transparent;
                        arrLabelReadOnly[i].Location = new System.Drawing.Point(ReadOnlyLabelLocation);
                        arrLabelReadOnly[i].Font = new System.Drawing.Font("宋体", 13, FontStyle.Bold);
                        arrLabelReadOnly[i].Size = new System.Drawing.Size(ReadOnlyLabelPoint);
                        arrLabelReadOnly[i].Text = MyVar.arrAutoSerialVariableName[i] + ":";
                        arrLabelReadOnly[i].TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        ReadOnlyLabelLocation.Height += -35;

                        //arrLabelReadOnly[i] = new Label();
                    }
                    #endregion

                    this.gpotherprint.Controls.AddRange(arrTextboxReadOnly);
                    this.gpotherprint.Controls.AddRange(arrLabelReadOnly);

                }
                catch (Exception ex)
                {
                    ShowMsg(mLogMsgType.Error, "控件显示错误\n" + ex.Message);
                }
            }));
        }

        void ShowDataChk_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowDataChk.Checked)
            {
                this.bShowData = true;
                this.ShowOtherData(this.mWoInfo.woId, this.bShowData);
            }
            else
                this.bShowData = false;
        }

        /// <summary>
        /// 显示模板文件路径
        /// </summary>
        /// <param name="path"></param>
        private void ShowLibFilePath(string filename, string path)
        {
            this.lblibfilename.Invoke(new EventHandler(delegate
            {
                this.lblibfilename.Text = filename;
                this.lb_showmfpath.Text = path;
                this.lblibfilename.Refresh();
            }));
        }
        /// <summary>
        /// 存放最小箱号，根据工单
        /// </summary>
        string minCarton = string.Empty;
        /// <summary>
        /// 打开模板文件
        /// </summary>
        /// <returns></returns>
        private void OpenLabFile(string filepath)
        {
            if (miasyncresult != null && !miasyncresult.IsCompleted)
            {
                ShowMsg(mLogMsgType.Warning, "程序正在初始化,请稍候...");
                return;
            }
            try
            {
                this.InitCtlPannel();
                if (this.mLibdoc != null)
                {
                    this.mLibdoc.Close(this.mbIsSaveLabFile);
                    this.mlppx.Documents.CloseAll(this.mbIsSaveLabFile);
                    this.mlppx.Quit();
                    this.mLibdoc = null;
                }
                if (this.mlppx != null)
                {
                    try
                    {
                        this.mlppx.Quit();
                    }
                    catch
                    {
                    }
                }
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "选择模板文件";
                ofd.Filter = "(*.lab)|*.lab";
                ofd.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                if (!string.IsNullOrEmpty(filepath) || ofd.ShowDialog() == DialogResult.OK)
                {
                    this.mPrintFileName = !string.IsNullOrEmpty(filepath) ? filepath : ofd.FileName;//this.lb_showmfpath.Text =
                    this.msProductText = Path.GetFileNameWithoutExtension(mPrintFileName); //ofd.SafeFileName.Remove(ofd.SafeFileName.IndexOf("." + ofd.SafeFileName.Split('.')[ofd.SafeFileName.Split('.').Length - 1]));

                    ShowLibFilePath(string.Format("[{0}] 标签打印", this.msProductText), this.mPrintFileName);
                    mlppx = new LabelManager2.ApplicationClass();

                    InitLab(mPrintFileName);
                    switch (this.mTabItem.Name)
                    {
                        case "tabItem1":
                            this.dgvdata.ContextMenuStrip = null;
                            this.mSaveLibFileFlag = true;
                            ShowControl(MyVar);
                            this.ShowStation("0", this.mWoInfo.qty.ToString());
                            break;
                        case "tabItem2":
                            this.dgvdata.ContextMenuStrip = this.contextMenuStrip2;
                            this.SetBtOkState(true);
                            // this.bt_ok.Enabled = true;
                            this.mSaveLibFileFlag = false;
                            if (this.miCatonBoxTotal < 1)
                                throw new Exception("每箱总数不能为零,请确认模板是否选择!!");
                            //获取没有包装完成的箱号列表
                            DataTable mdt = new DataTable();
                            bool flag = false;
                            mdt = SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetNotCloseBoxInfo(this.mLineInfo.lineId));
                            if (mdt != null && mdt.Rows.Count > 0)
                            {
                                this.dgvNotCloseBoxNumber.DataSource = mdt.DefaultView;
                                if (MessageBoxEx.Show(string.Format("当前产线[{0}]还有没有包装完成的箱号,是否继续包装未完成的箱号?\n继续包装未完成的箱号 请选择[Yes] \n包装新的一箱,请选择[No]",
                                     this.mLineInfo.linedesc),
                                     "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.No)
                                {
                                    SFIS_PRINT_SYSTEM.Frm.ShowCartonData SCD = new SFIS_PRINT_SYSTEM.Frm.ShowCartonData(this, mdt);
                                    if (SCD.ShowDialog() != DialogResult.Yes)
                                        throw new Exception("错误:卡通箱信息加载失败...");
                                    else
                                        flag = true;
                                }
                                else
                                {
                                    if (MessageBoxEx.Show("使用该功能将产生一个新的箱号\n工单不是第一次生产请谨慎使用\n请做好记录防止致箱号混乱\n是否继续?",
                                        "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    {
                                        return;
                                    }
                                }
                            }
                            //获取当前要包装的卡通号,如果没有则找到当前工单的最大箱号+1
                            if (flag)
                                this.mCartonId = this.gCartonInfo.cartonId;
                            else
                            {
                                DataTable dtmincarton =ReleaseData.arrByteToDataTable( 
                                    refWebtWipTracking.Instance.GetMinCartonByWoid(this.mWoInfo.woId));
                                //不是首次生产，跳箱需要权限
                                if (!string.IsNullOrEmpty(dtmincarton.Rows[0]["mincarton"].ToString()))
                                {
                                    #region 添加密码输入
                                    EnaPwd ed = new EnaPwd(this);
                                    if (ed.ShowDialog() == DialogResult.OK)
                                    {
                                        if (strEnaPwd != "checkbox1988")
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
                                }
                                string err = refWebtWipTracking.Instance.GetMaxBoxNumber(this.mWoInfo.woId,
                                    this.mLineInfo.lineId, this.mWoInfo.partnumber);
                                if (err.ToUpper() == "ERR")
                                {
                                    throw new Exception("箱号获取失败,请检查!!");
                                }
                                
                                if (err.ToUpper() == tbwoid.Text.Trim() + "C0001")
                                {
                                    //首次生产，针对CNS项目提供首箱输入功能
                                    if (CNSFlag.ToUpper() == "CNS")
                                    {
                                        BoxNum bn = new BoxNum(this);
                                        if (bn.ShowDialog() == DialogResult.OK)
                                        {
                                            if (!string.IsNullOrEmpty(strBoxNumber))
                                            {
                                                err = tbwoid.Text.Trim() + "C" + strBoxNumber.Trim().PadLeft(4, '0');
                                                refWebtWipTracking.Instance.UpdateFirstCarton(err);                                            

                                            }
                                        }
                                    }
                                }
                                
                                this.mCartonId = err;
                            }

                            DataTable dtmin = ReleaseData.arrByteToDataTable(
                                    refWebtWipTracking.Instance.GetMinCartonByWoid(this.mWoInfo.woId));
                            minCarton = dtmin.Rows[0]["mincarton"].ToString();
                            minCarton = string.IsNullOrEmpty(minCarton) ? "0" : minCarton;

                            this.ShowCartonStation(this.gCartonInfo.number.ToString());
                            this.SetCartonID(this.mCartonId);
                            break;
                        default:
                            throw new Exception("警告:不明选项..");
                            break;
                    }
                }
                else
                {
                    return;
                }
                return;
            }
            catch (Exception ex)
            {
                ShowMsg(mLogMsgType.Error, ex.Message + ": FrmMain 2233");
                return;
            }
        }

        /// <summary>
        /// 打开模板前判断生产信息是否填写完成
        /// </summary>
        /// <returns></returns>
        private bool CheckSelectIsOK()
        {
            #region 判定是否选择
            if (string.IsNullOrEmpty(this.tbwoid.Text))
            {
                this.tbwoid.Focus();
                ShowMsg(mLogMsgType.Warning, "请设定生产信息");
                return false;
            }
            if (string.IsNullOrEmpty(this.cbstationId.Text))
            {
                this.cbstationId.Focus();
                ShowMsg(mLogMsgType.Warning, "请设定生产信息");
                return false;
            }
            if (string.IsNullOrEmpty(this.cblineId.Text))
            {
                this.cblineId.Focus();
                ShowMsg(mLogMsgType.Warning, "请设定生产信息");
                return false;
            }
            if (string.IsNullOrEmpty(this.mWoInfo.woId))
            {
                ShowMsg(mLogMsgType.Warning, "没有找到工单号");
                return false;
            }
            if (string.IsNullOrEmpty(this.mWoInfo.routgroupId))
            {
                ShowMsg(mLogMsgType.Warning, "没有发现工单的流程编号,请重新设置");
                return false;
            }

            #endregion
            return true;
        }
        /// <summary>
        /// 保存当前配置
        /// </summary>
        private void SaveConfig()
        {
            try
            {
                if (this.mWoInfo == null)
                    return;
                XmlDocument doc = new XmlDocument();
                doc.Load(System.Windows.Forms.Application.StartupPath + "\\DllConfig.xml");
                #region WOENTITY
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("WOID")).SetAttribute("woid", this.mWoInfo.woId);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("PARTNUMBER")).SetAttribute("partnumber", this.mWoInfo.partnumber);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("INPUTGROUP")).SetAttribute("inputgroup", this.mWoInfo.inputgroup);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("OUTPUTGROUP")).SetAttribute("outputgroup", this.mWoInfo.outputgroup);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("POID")).SetAttribute("poid", this.mWoInfo.poId);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("QTY")).SetAttribute("qty", this.mWoInfo.qty.ToString());
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("ROUTGROUPID")).SetAttribute("routgroupid", this.mWoInfo.routgroupId);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("BOMNUMBER")).SetAttribute("bomnumber", this.mWoInfo.bomnumber);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("BOMVER")).SetAttribute("bomver", this.mWoInfo.bomver);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("WOSTATE")).SetAttribute("wostate", this.mWoInfo.wostate.ToString());
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("CPWD")).SetAttribute("cpwd", this.mWoInfo.cpwd.ToString());
                #endregion
                #region LINEENTITY
               // ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LINEENTITY").SelectSingleNode("LINEID")).SetAttribute("lineid", this.mLineInfo.lineId);
                ReadIniFile.IniWriteValue("SFIS_PRINT_SYSTEM", "LINE", Encoder.EncryptString(this.mLineInfo.lineId), Line_IniFilePath);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LINEENTITY").SelectSingleNode("LINEDESC")).SetAttribute("linedesc", this.mLineInfo.linedesc);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LINEENTITY").SelectSingleNode("STARTIPADDRESS")).SetAttribute("startipaddress", this.mLineInfo.startIpAddress);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LINEENTITY").SelectSingleNode("ENDIPADDRESS")).SetAttribute("endipaddress", this.mLineInfo.endIpAddress);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LINEENTITY").SelectSingleNode("PLOTID")).SetAttribute("plotid", this.mLineInfo.plotId);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LINEENTITY").SelectSingleNode("WSID")).SetAttribute("wsid", this.mLineInfo.wsId);
                #endregion
                #region CRAFTENTITY
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("CRAFTENTITY").SelectSingleNode("CRAFTNAME")).SetAttribute("craftname", this.lbstationname.Text);
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("CRAFTENTITY").SelectSingleNode("CRAFTID")).SetAttribute("craftid", this.mCraftName);
                #endregion
                doc.Save(System.Windows.Forms.Application.StartupPath + "\\DllConfig.xml");
            }
            catch (Exception ex)
            {
                throw new Exception("配置文件保存失败" + ex.Message + ",请检查!!");
            }

        }

        /// <summary>
        /// 读取上次退出时的配置信息
        /// </summary>
        private void ReadConfig()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(System.Windows.Forms.Application.StartupPath + "\\DllConfig.xml");
                #region WOENTITY
                this.mWoInfo = new T_WO_INFO()
                {
                    woId = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("WOID")).GetAttribute("woid"),
                    partnumber = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("PARTNUMBER")).GetAttribute("partnumber"),
                    inputgroup = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("INPUTGROUP")).GetAttribute("inputgroup"),
                    outputgroup = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("OUTPUTGROUP")).GetAttribute("outputgroup"),
                    poId = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("POID")).GetAttribute("poid"),
                    qty = int.Parse(((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("QTY")).GetAttribute("qty")),
                    routgroupId = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("ROUTGROUPID")).GetAttribute("routgroupid"),
                    bomnumber = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("BOMNUMBER")).GetAttribute("bomnumber"),
                    bomver = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("BOMVER")).GetAttribute("bomver"),
                    wostate = int.Parse(((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("WOSTATE")).GetAttribute("wostate")),
                    cpwd = this.Getcpwd(((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("WOENTITY").SelectSingleNode("CPWD")).GetAttribute("cpwd"))
                };
                #endregion
                #region LINEENTITY
                this.mLineInfo = new tLineInfo()
                {
                   // lineId = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LINEENTITY").SelectSingleNode("LINEID")).GetAttribute("lineid"),
                    lineId = Encoder.DecryptString(ReadIniFile.IniReadValue("SFIS_PRINT_SYSTEM", "LINE", Line_IniFilePath)),
                    linedesc = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LINEENTITY").SelectSingleNode("LINEDESC")).GetAttribute("linedesc"),
                    startIpAddress = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LINEENTITY").SelectSingleNode("STARTIPADDRESS")).GetAttribute("startipaddress"),
                    endIpAddress = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LINEENTITY").SelectSingleNode("ENDIPADDRESS")).GetAttribute("endipaddress"),
                    plotId = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LINEENTITY").SelectSingleNode("PLOTID")).GetAttribute("plotid"),
                    wsId = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LINEENTITY").SelectSingleNode("WSID")).GetAttribute("wsid")
                };
                #endregion
                #region CRAFTENTITY
                this.mCraftName = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("CRAFTENTITY").SelectSingleNode("CRAFTNAME")).GetAttribute("craftname");
              //  this.mCraftId = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("CRAFTENTITY").SelectSingleNode("CRAFTID")).GetAttribute("craftid");
                #endregion

                #region LABCONFIG
                LabDir = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("PrintConfig").SelectSingleNode("LABCONFIG").SelectSingleNode("LABPATH")).GetAttribute("labdir");
                if (string.IsNullOrEmpty(LabDir))
                    LabDir = "D";
                #endregion

            }
            catch
            {
                throw new Exception("配置文件加载失败,请检查!!");
            }
        }

        private T_WO_INFO.ecpwd Getcpwd(string cpwd)
        {
            T_WO_INFO.ecpwd _cpwd;
            switch (cpwd.ToUpper())
            {
                case "PROG":
                    _cpwd = T_WO_INFO.ecpwd.PROG;
                    break;
                case "FILE":
                    _cpwd = T_WO_INFO.ecpwd.FILE;
                    break;
                case "USERDEF":
                    _cpwd = T_WO_INFO.ecpwd.USERDEF;
                    break;
                default:
                    _cpwd = T_WO_INFO.ecpwd.PROG;
                    break;
            }
            return _cpwd;
        }
        /// <summary>
        /// 使用上次退出时的配置信息填写控件
        /// </summary>
        private void FillConfig()
        {
            this.tbwoid.Text = this.mWoInfo.woId;
            //this.cblineId.Items.Clear();
            this.cblineId.Items.Add(this.mLineInfo.lineId);
            this.cblineId.SelectedIndex = 0;
            this.lbLinename.Text = this.mLineInfo.linedesc;
            //this.cbstationId.Items.Add(this.mCraftId);
            this.cbstationId.SelectedItem = this.mCraftName;
            //this.cbstationId.Text = this.mCraftId;
            this.lbstationname.Text = this.mCraftName;
        }

        /// <summary>
        /// 比对序列号是否在工单定义的区间范围内
        /// </summary>
        /// <param name="woid">工单号</param>
        /// <param name="serial">序列号号</param>
        /// <param name="sntype">序列号类型</param>
        /// <returns></returns>
        private bool CompareSerialnumber(string woid, string serial, string sntype)
        {
            bool falg = false;
            BLL.cdbAccess ass = new cdbAccess();
            string _sql = string.Empty;
            if (sntype.ToUpper() == "IMEI" || sntype.ToUpper() == "MEID")
            {
                _sql = string.Format("SELECT * FROM wosnrule where woid='{1}' and sntype='{2}' and '{0}' between snstart and snend",
                    serial.Length > 14 ? serial.Substring(0, 14) : serial, woid, sntype);
            }
            else
            {
                _sql = string.Format("SELECT * FROM wosnrule where woid='{1}' and sntype='{2}' and ('{0}' between snstart and snend) and len(snstart)=len('{0}') ",
                    serial, woid, sntype);
            }
            DataTable dt = ass.GetDatatable(_sql);
            dt.DefaultView.Sort = "snstart asc";
            dt = dt.DefaultView.ToTable();
            if (dt == null || dt.Rows.Count < 1)
            {
                falg = false;
                return falg;
            }
            else
            {
                falg = true;
            }
            //添加检查MAC和SPMAC递增规则检查  2013-04-22
            switch (sntype.ToUpper())
            {
                case "MAC":
                    if (!this.bIsChkMac)
                        return true;
                    if (!ChkSerial(dt.Rows[0]["snstart"].ToString().ToUpper(),
                         serial.ToUpper(), int.Parse(dt.Rows[0]["usenum"].ToString())))
                        return false;
                    break;
                case "SPMAC":
                    if (!this.bIsChkSpmac)
                        return true;
                    if (!ChkSerial(dt.Rows[0]["snstart"].ToString().ToUpper(),
                        serial.ToUpper(), int.Parse(dt.Rows[0]["usenum"].ToString())))
                        return false;
                    break;
                default:
                    break;
            }

            return falg;
        }

        /// <summary>
        /// 检查流程
        /// </summary>
        /// <param name="esndata">esn号</param>
        /// <param name="currentRoute">当前流程号</param>
        /// <returns>返回提示信息 非OK均为不良</returns>
        private string ChkRoute(string esndata, string currentRoute)
        {
            if (this.mRprint)
                return "OK";
            dic = new Dictionary<string, object>();
            dic.Add("DATA", esndata);
            dic.Add("MYGROUP", currentRoute);
            return refWebProPublicStoredproc.Instance.ExecuteProcedure("PRO_CHECKROUTE", MapListConverter.DictionaryToJson(dic));
        }

        /// <summary>
        /// 初始化自定义的控件(清空内容)
        /// </summary>
        private void InitMyControl()
        {
            this.gpotherprint.Invoke(new EventHandler(delegate
            {
                for (int i = 0; i < this.MyVar.arrVariable.Length; i++)
                {
                    arrTextbox[i].Text = string.Empty;
                    arrTextbox[i].NotErr = false;
                }
                this.mCurrentEsn = string.Empty;
                arrTextbox[0].Focus();
            }));
        }
        /// <summary>
        /// 卡通箱标签打印时定位光标用
        /// </summary>
        /// <param name="TextName"></param>
        private void SetTextBoxFocus(string TextName)
        {
            switch (TextName)
            {
                case "tb_esninput":
                    if (this.tb_macinput.Enabled)
                    {
                        tb_macinput.Focus();
                    }
                    else
                    {
                        if (this.tb_sninput.Enabled)
                        {
                            this.tb_sninput.Focus();
                        }
                        else
                        {
                            if (this.tb_ktinput.Enabled)
                            {
                                this.tb_ktinput.Focus();
                            }
                            else
                            {
                                if (this.tb_pcbasninput.Enabled)
                                {
                                    this.tb_pcbasninput.Focus();
                                }
                                else
                                {
                                    if (this.tb_spmacinput.Enabled)
                                    {
                                        this.tb_spmacinput.Focus();
                                    }
                                    else
                                    {
                                        this.bt_ok.Focus();
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "tb_macinput":
                    if (this.tb_sninput.Enabled)
                    {
                        this.tb_sninput.Focus();
                    }
                    else
                    {
                        if (this.tb_ktinput.Enabled)
                        {
                            this.tb_ktinput.Focus();
                        }
                        else
                        {
                            if (this.tb_pcbasninput.Enabled)
                            {
                                this.tb_pcbasninput.Focus();
                            }
                            else
                            {
                                if (this.tb_spmacinput.Enabled)
                                {
                                    this.tb_spmacinput.Focus();
                                }
                                else
                                {
                                    this.bt_ok.Focus();
                                }
                            }
                        }
                    }
                    break;
                case "tb_sninput":
                    if (this.tb_ktinput.Enabled)
                    {
                        this.tb_ktinput.Focus();
                    }
                    else
                    {
                        if (this.tb_pcbasninput.Enabled)
                        {
                            this.tb_pcbasninput.Focus();
                        }
                        else
                        {
                            if (this.tb_spmacinput.Enabled)
                            {
                                this.tb_spmacinput.Focus();
                            }
                            else
                            {
                                this.bt_ok.Focus();
                            }
                        }
                    }
                    break;
                case "tb_ktinput":
                    if (this.tb_pcbasninput.Enabled)
                    {
                        this.tb_pcbasninput.Focus();
                    }
                    else
                    {
                        if (this.tb_spmacinput.Enabled)
                        {
                            this.tb_spmacinput.Focus();
                        }
                        else
                        {
                            this.bt_ok.Focus();
                        }
                    }
                    break;
                case "tb_pcbasninput":
                    if (this.tb_spmacinput.Enabled)
                    {
                        this.tb_spmacinput.Focus();
                    }
                    else
                    {
                        this.bt_ok.Focus();
                    }
                    break;
                case "tb_spmacinput":
                    this.bt_ok.Focus();
                    break;
                default:
                    if (this.tb_macinput.Enabled)
                    {
                        this.tb_macinput.Focus();
                    }
                    else
                    {
                        if (this.tb_sninput.Enabled)
                        {
                            this.tb_sninput.Focus();
                        }
                        else
                        {
                            if (this.tb_ktinput.Enabled)
                            {
                                this.tb_ktinput.Focus();
                            }
                            else
                            {
                                if (this.tb_pcbasninput.Enabled)
                                {
                                    this.tb_pcbasninput.Focus();
                                }
                                else
                                {
                                    if (this.tb_spmacinput.Enabled)
                                    {
                                        this.tb_spmacinput.Focus();
                                    }
                                    else
                                    {
                                        this.bt_ok.Focus();
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }
        #endregion

        //==========================================================================================
        //======================              控件事件                             =================
        //==========================================================================================

        private void PrintMain_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("C:\\SFIS"))
                Directory.CreateDirectory("C:\\SFIS");

            this.mTabItem = this.tbcLable.SelectedTab;
            SetBtOkState(false);
         //   this.Text = string.Format("{0}({1})", this.Text, this.AssemblyVersion);
            this.Text = string.Format("{0} Version:{1} (Build Date:{2})", this.Text, this.AssemblyVersion, System.IO.File.GetLastWriteTime(System.Windows.Forms.Application.ExecutablePath).ToShortDateString());
            this.cbstationId.Enabled = false;
            this.cblineId.Enabled = false;
            this.nudPrintNum.ReadOnly = true;

            this.SN_Printer.Checked = true;
            this.tb_Boxcount.ReadOnly = true;

            if (checkprocessisrun("lppa"))
            {
                if (MessageBoxEx.Show("检测到有打开的模板文件 \n\n 请先关闭模板文件再运行程序,否则可能导致程序出错!! \n\n 关闭模板文件选择[Yes] 否则选择[NO]", "提示!!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    KillAllProcess();
                }
            }

           
            if (!refWebCheck_Version.Instance.CheckPrgVsersion("SFIS_PRINT_SYSTEM", System.Windows.Forms.Application.ProductVersion, null, null, null))
            {
                MessageBox.Show("该程序为版本不是最新版\r\n请更新后运行");
                RunFile(System.Windows.Forms.Application.StartupPath + @"\", "AutoUpdate.exe", mAppFileName);
                string FileName = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
                Process[] prc = Process.GetProcessesByName(FileName.Substring(0, FileName.LastIndexOf('.')));
                if (prc.Length > 0)
                    foreach (Process pc in prc)
                    {
                        pc.Kill();
                    }
                return;
            }

            SFIS_PRINT_SYSTEM.Frm.UserLogin ul = new SFIS_PRINT_SYSTEM.Frm.UserLogin(this);
            while (ul.ShowDialog() == DialogResult.No) ;

            if (this.loginOk)
            {
                if (this.mUserInfo != null)
                    this.lbusername.Text = this.mUserInfo.username;

                //加载配置信息
                ReadConfig();
                FillConfig();
                this.btInputToUpper_Click(null, null);
                #region 添加应用程序
                //if (this.gUserInfo.rolecaption == "系统开发员")
                //{
                //    List<WebServices.tUserInfo.tFunctionList> lsfunls = new List<WebServices.tUserInfo.tFunctionList>();
                //    SFIS_PRINT_SYSTEM.BLL.publicfunction.GetFromCtls(this, ref lsfunls);
                //    SFIS_PRINT_SYSTEM.BLL.publicfunction.AddProgInfo(new WebServices.tUserInfo.tProgInfo()
                //    {
                //        progid = this.Name,
                //        progname = this.Text,
                //        progdesc = this.Text

                //    }, lsfunls);
                //}
                #endregion
            }
        }

        public void PrintMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBoxEx.Show("是否确定退出!!!", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                SaveConfig();
                closeproc("AutoUpdate.exe");
                try
                {
                    //try
                    //{
                    //    this.SaveConfig();
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBoxEx.Show(ex.Message);
                    //}
                    refWebtUserInfo.Instance.DeleteLogin(this.gUserInfo.userId, this.mIpaddress);
                    if (this.mLibdoc != null)
                    {
                        this.mLibdoc.Close(false);
                        this.mlppx.Documents.CloseAll(false);
                    }
                    this.mlppx.Quit();
                }
                catch
                {
                }
                this.KillAllProcess();
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }

        private void btselectwo_Click(object sender, EventArgs e)
        {
            //获取未结工单列表
            SFIS_PRINT_SYSTEM.Frm.ShowData sd = new SFIS_PRINT_SYSTEM.Frm.ShowData(this,
                SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(SFIS_PRINT_SYSTEM.BLL.refWebtWoInfo.Instance.GetWoInfo(null,null)), true);
            sd.ShowDialog();
            this.ShowMsg(mLogMsgType.Outgoing, string.Format("此工单可在[{0}]线生产", mWoInfo.ProductLine));
        }

        private void tbwoid_TextChanged(object sender, EventArgs e)
        {
            ShowMsg(mLogMsgType.Outgoing, "选择了工单:" + this.mWoInfo.woId + "\n切换后需要重新打开模板");
            this.gpotherprint.Controls.Clear();
            this.gpotherprint.Refresh();
            SetBtOkState(false);
            this.pictureBox1.Image = null;
            this.mPrintFileName = this.lb_showmfpath.Text = "";

            DataTable _dt = SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(SFIS_PRINT_SYSTEM.BLL.refWebtRouteInfo.Instance.GetRouteInfoByWoId(this.mWoInfo.routgroupId));
    
            this.cbstationId.Items.Clear();
            foreach (DataRow dr in _dt.Rows)
            {
              this.cbstationId.Items.Add(dr["CRAFTNAME"].ToString());        
              
            }

          //  获取自定义的密码
            if (this.mWoInfo.cpwd == T_WO_INFO.ecpwd.USERDEF)
            {
                Thread thread = new Thread(new ThreadStart(this.LoadWoPwd));
                thread.Start();
            }


            //加载工单序列号区间
            this.EventRunNoparamet = new delegateRunNoParmet(this.DownloadWoSnRule);
            this.iasyncresult = this.EventRunNoparamet.BeginInvoke(null, null);

            //针对CNS项目
            DataTable dtproduct = BLL.ReleaseData.arrByteToDataTable(refWebtProduct.Instance.GetProductInfoByWoId(this.tbwoid.Text));
            if (dtproduct == null || dtproduct.Rows.Count < 1)
            {
                this.ShowMsg(mLogMsgType.Error, "工单" + this.tbwoid.Text + "没有对应的产品信息");
                return;
            }
            DataTable dtpackparater = BLL.ReleaseData.arrByteToDataTable(refWebtPackParameters.Instance.GetPackParametersByWoid(this.tbwoid.Text));
            if (dtpackparater == null || dtpackparater.Rows.Count < 1)
            {
                this.ShowMsg(mLogMsgType.Error, "工单" + this.tbwoid.Text + "没有对应的产品信息");
                return;
            }
            CNSFlag = dtproduct.Rows[0]["other"].ToString();
            CNSCartonQTY = dtpackparater.Rows[0]["CartonQty"].ToString();
        }
        private void LoadWoPwd()
        {
            //获取自定义的密码
            this.ShowMsg(mLogMsgType.Outgoing, "正在加载,预定义密码内容..");
            DataTable _dt = null; //SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetPwd(this.mWoInfo.woId));
            if (_dt != null && _dt.Rows.Count > 0)
                publicfunction.InsertLocalWoPwd(ref _dt);
            this.ShowMsg(mLogMsgType.Incoming, "预定义密码内容加载完成");
        }
        private void cbline_DropDown(object sender, EventArgs e)
        {
            //获取所有生产线的信息
            if (this.mdtAllLineInfo == null || this.mdtAllLineInfo.Rows.Count < 1)
            {
                this.cblineId.Items.Clear();
                this.mdtAllLineInfo = SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(SFIS_PRINT_SYSTEM.BLL.refWebtLineInfo.Instance.GetAllLineInfo());
                foreach (DataRow dr in this.mdtAllLineInfo.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["线别"].ToString()))
                    {
                        this.cblineId.Items.Add(dr["线别"].ToString());
                    }
                }
            }
        }

        private void cblineId_SelectedValueChanged(object sender, EventArgs e)
        {
            //保存选中的线别信息
            if (this.mdtAllLineInfo != null)
            {
                DataTable _dt = SFIS_PRINT_SYSTEM.BLL.publicfunction.getNewTable(this.mdtAllLineInfo, string.Format("{0}='{1}'", "线别", this.cblineId.Text));
                this.mLineInfo = new  tLineInfo()
                {
                    lineId = _dt.Rows[0]["线别"].ToString(),
                    linedesc = this.lbLinename.Text = _dt.Rows[0]["线体描述"].ToString(),
                    wsId = _dt.Rows[0]["车间编号"].ToString(),
                    startIpAddress = _dt.Rows[0]["开始IP地址"].ToString(),
                    endIpAddress = _dt.Rows[0]["结束IP地址"].ToString(),
                    plotId = _dt.Rows[0]["计划ID"].ToString()
                };
            }
            ShowMsg(mLogMsgType.Outgoing, string.Format("选中了线别:{0}[{1}]", this.lbLinename.Text, this.cblineId.Text));
        }

        private void cbstation_DropDown(object sender, EventArgs e)
        {
            if (this.mWoInfo == null)
            {
                ShowMsg(mLogMsgType.Warning, "请先选择生产工单!");
                return;
            }
        }

        private void cbstationId_SelectedValueChanged(object sender, EventArgs e)
        {
            this.gpotherprint.Controls.Clear();
            this.gpotherprint.Refresh();
            SetBtOkState(false);
            this.pictureBox1.Image = null;
            this.mPrintFileName = this.lb_showmfpath.Text = "";

            this.lbstationname.Text = this.cbstationId.Text;             
            this.mCraftName = this.lbstationname.Text;

            //添加自动找到模板文件并打开
            //到数据库中找到该站使用哪个模板文件
            DataTable _dt = BLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetATEScripts(this.mWoInfo.woId));
            string labfilename = string.Empty;
            string labfilefullpath = string.Empty;
            if (_dt != null && _dt.Rows.Count > 0)
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["script"].ToString()))
                    {
                        string[] arrScripts = dr["script"].ToString().Split(',');
                        foreach (string str in arrScripts)
                        {
                            if (str.Trim().ToUpper().IndexOf(this.lbstationname.Text.Trim().ToUpper()) != -1)
                            {
                                //if (!string.IsNullOrEmpty(labfilename))
                                //{
                                //    if (labfilename.Trim().ToUpper() != str.Trim().ToUpper())
                                //    {
                                //        this.ShowMsg(mLogMsgType.Warning, "找到的模板文件不一致");
                                //        return;
                                //    }
                                //}
                                labfilename = str.Trim();
                                labfilefullpath = string.Format(@"{0}\{1}\{2}", this.LabDir.IndexOf(":") != -1 ? this.LabDir : this.LabDir + ":", this.mWoInfo.woId, labfilename);
                                if (!File.Exists(labfilefullpath))
                                {
                                    this.ShowMsg(mLogMsgType.Warning, string.Format(@"{0}:\{1}\{2}",
                                        this.LabDir, this.mWoInfo.woId, labfilename) + ":文件不存在\n请手动选择模板文件..");
                                    labfilefullpath = string.Empty;
                                    continue;
                                }
                                if (!string.IsNullOrEmpty(labfilefullpath))
                                {
                                    if (MessageBoxEx.Show("该站的模板文件已经找到 [" + labfilename + "] 是否打开?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    {
                                        ShowMsg(mLogMsgType.Normal, "发现该站的模板文件"+labfilefullpath);
                                        this.OpenLabAndDowloadSnRule(labfilefullpath); return;
                                    }
                                }
                                else
                                {
                                    ShowMsg(mLogMsgType.Error, "没有发现该站的模板文件,请手动选择");
                                    continue;
                                }
                            }
                        }
                    }
                }
            }

        }


        private void imbtconfig_Click(object sender, EventArgs e)
        {
            string[] EmpData = InputBox.ShowInputBox("输入权限", string.Empty);
            try
            {
                string UserId = EmpData[0];
                string PWD = EmpData[1];
                if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(PWD))
                {
                    string _StrErr = refWebtUserInfo.Instance.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);   // mUserInfo.CHECK_SET_LINE_EMPLOYEE(UserId, PWD);
                    if (_StrErr == "OK")
                    {
                        ShowMsg(mLogMsgType.Incoming, "权限正确");
                        //Dictionary<string, object> dic = new Dictionary<string, object>();
                        //Frm_Public.Frm_Select fd = new Frm_Public.Frm_Select(ReleaseData.arrByteToDataTable(LsLine.GetAllLineInfo()), ref dic);
                        //if (fd.ShowDialog() == DialogResult.OK)
                        //{
                        //    LabLine.Text = dic["线别"].ToString();
                        //    Encoder.ReadIniFile.IniWriteValue("BOX_PRINT", "LINE", Encoder.Encoder.EncryptString(dic["线别"].ToString()), IniFilePath);
                        //}
                        this.cbstationId.Enabled = true;
                        this.cblineId.Enabled = true;

                    }
                    else
                    {
                        ShowMsg(mLogMsgType.Error, _StrErr);

                    }
                }

            }
            catch (Exception ex)
            {
                ShowMsg(mLogMsgType.Error, "权限格式不正确:" + ex.Message);
            }
            //this.cbstationId.Enabled = true;
            //this.cblineId.Enabled = true;
        }
        private void btenablesnrule_Click(object sender, EventArgs e)
        {
            this.mUseSnRule = !mUseSnRule;
            if (mUseSnRule)
            {
                this.ShowMsg(mLogMsgType.Outgoing, "开启SN排序规则");
                this.btenablesnrule.Text = "使用SN规则 = 开启";
                // this.btenablesnrule.Image = global::CallCodeSoftPrint.Properties.Resources.good;
            }
            else
            {
                this.ShowMsg(mLogMsgType.Warning, "关闭SN排序规则");
                this.btenablesnrule.Text = "使用SN规则 = 关闭";
                //  this.btenablesnrule.Image = global::CallCodeSoftPrint.Properties.Resources.NotShow;
            }
        }
        private string mIpaddress = string.Empty;
        /// <summary>
        /// 打开模板(屏蔽)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbtopenlibfile_Click(object sender, EventArgs e)
        {
            #region 添加密码输入
            EnaPwd ed = new EnaPwd(this);
            if (ed.ShowDialog() == DialogResult.OK)
            {
                if (strEnaPwd != "print2013")
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
            this.OpenLabAndDowloadSnRule(string.Empty);
        }

        private void OpenLabAndDowloadSnRule(string filepath)
        {
            if (!this.CheckSelectIsOK())
                return;
            //XmlDocument doc = new XmlDocument();
            //string XmlName = "DllConfig.xml";
            //doc.Load(XmlName);
            //if (!string.IsNullOrEmpty(mIpaddress))
            //    refWebtUserInfo.Instance.DeleteLogin(this.gUserInfo.userId, this.mIpaddress);
            //string ipadd = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ServerIP")).GetAttribute("IP").ToString();
            //mIpaddress = publicfunction.ChkIpIsInLocal(ipadd.Substring(0, 4));
            //string msg = refWebtUserInfo.Instance.CheckEmp_NEW(string.Format("{0}-{1}", this.gUserInfo.userId, this.gUserInfo.pwd), mIpaddress, cComputerMsg.GetCPUID, this.mCraftName);

            

            //if (msg != "OK")
            //{
            //    ShowMsg(mLogMsgType.Error, msg);
            //    return;
            //}
            //this.EventRunNoparamet = new delegateRunNoParmet(this.DownloadWoSnRule);
            //this.iasyncresult = this.EventRunNoparamet.BeginInvoke(null, null);
            this.OpenLabFile(filepath);
            this.SaveConfig();
            this.mPrintNumber = int.Parse(this.nudPrintNum.Value.ToString());
            this.ShowMsg(mLogMsgType.Incoming, "模板文件初始化完成..");
        }

        private void textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.cblineId.Enabled = false;
                this.cbstationId.Enabled = false;
                #region 判断准备条件
                if (string.IsNullOrEmpty(this.mPrintFileName))
                {
                    ShowMsg(mLogMsgType.Warning, "没有选择模板文件..");
                    return;
                }
                if (iasyncresult != null && !iasyncresult.IsCompleted)
                {
                    ShowMsg(mLogMsgType.Warning, "工单序列号区间还在加载中,请稍候..");
                    return;
                }
                if (miasyncresult != null && !miasyncresult.IsCompleted)
                {
                    ShowMsg(mLogMsgType.Warning, "上一个动作还在进行中,请稍候..");
                    ((MyTextBox)sender).SelectAll();
                    ((MyTextBox)sender).Focus();
                    return;
                }
                #endregion
                try
                {
                    FlagLeave = false;

                    //判断基本的长度是否符合
                    if (((MyTextBox)sender).Text.Trim().Length != this.MyVar.DicVarLen[((MyTextBox)sender).Name])
                    {
                        ShowMsg(mLogMsgType.Warning, ((MyTextBox)sender).Name + "长度不符");
                        ((MyTextBox)sender).NotErr = true;
                        ((MyTextBox)sender).SelectAll();
                        return;
                    }
                    //判断序列号是否在工单区间范围内
                    if (!this.CompareSerialnumber(this.mWoInfo.woId, ((MyTextBox)sender).Text.Trim(), ((MyTextBox)sender).Name)
                        && ((MyTextBox)sender).Name.ToUpper() != "ESN" && ((MyTextBox)sender).Name.ToUpper() != "MAC") //????
                    {
                        ((MyTextBox)sender).NotErr = true;
                        ShowMsg(mLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                            ((MyTextBox)sender).Text, ((MyTextBox)sender).Name));
                        ((MyTextBox)sender).NotErr = true;
                        ((MyTextBox)sender).SelectAll();
                        return;
                    }
                    else
                    {
                        ((MyTextBox)sender).NotErr = false;
                    }
                    ////判断当前序列号是否被使用过
                    //DataTable esnTemp = BLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.ChkKeyParts(((MyTextBox)sender).Text.Trim(),
                    //    string.Empty, this.mWoInfo.woId));

                    #region 检查刷入的是什么序列号
                    switch (((MyTextBox)sender).Name.ToUpper())
                    {
                        //???如果当前输入的esn号能够带出当前模板的变量的所有内容 该如何处理
                        //???如果当前输入的esn号能够带出当前模板的变量的部分内容 该如何处理

                        case "ESN"://如果是esn则判断是否存在(即有没有跟PCBA esn绑定) + 根据esn判断流程
                            #region ESN判断
                            //DataTable esnInfo = BLL.ReleaseData.arrByteToDataTable(refWebtWipKeyPart.Instance.GetWipKeyParts(((MyTextBox)sender).Text.Trim()));
                            //if (esnInfo == null || esnInfo.Rows.Count < 1)
                            //{
                            //    ShowMsg(mLogMsgType.Error, string.Format("流程错误,当前序列号[{0}]不存在,请确认是否经过投板站..", ((MyTextBox)sender).Text.Trim()));
                            //    ((MyTextBox)sender).NotErr = true;
                            //    ((MyTextBox)sender).SelectAll();
                            //    this.mCurrentEsn = string.Empty;
                            //    return;
                            //}
                            ////判断esn是否是当前工单的
                            //if (esnInfo.Rows[0]["工单"].ToString() != this.mWoInfo.woId)
                            //{
                            //    ShowMsg(mLogMsgType.Error, string.Format("输入的序列号[{0}]不属于当前工单[{1}] ≠ [{2}],请检查..",
                            //        ((MyTextBox)sender).Text.Trim(),
                            //        this.mWoInfo.woId,
                            //        esnInfo.Rows[0]["工单"].ToString()));
                            //    ((MyTextBox)sender).NotErr = true;
                            //    ((MyTextBox)sender).SelectAll();
                            //    this.mCurrentEsn = string.Empty;
                            //    return;
                            //}
                            ////判断esn流程
                            //string strErr;
                            //if ((strErr = this.ChkRoute(((MyTextBox)sender).Text.Trim(), this.mCraftId).ToUpper()) != "OK")
                            //{
                            //    ShowMsg(mLogMsgType.Error, string.Format("输入的序列号[{0}]流程错误\n{1},请检查..",
                            //         ((MyTextBox)sender).Text.Trim(),
                            //         strErr));
                            //    ((MyTextBox)sender).NotErr = true;
                            //    ((MyTextBox)sender).SelectAll();
                            //    this.mCurrentEsn = string.Empty;
                            //    return;
                            //}
                            //this.mCurrentEsn = ((MyTextBox)sender).Text.Trim();
                            #endregion
                            break;
                        case "MAC":  //计算出MAC号对应的所有密码
                            switch (this.mWoInfo.cpwd)
                            {
                                case T_WO_INFO.ecpwd.PROG:
                                    this.mAllKeys = SFIS_PRINT_SYSTEM.BLL.macPassword.getMacAllPassword(((MyTextBox)sender).Text.Trim());
                                    break;
                                case T_WO_INFO.ecpwd.USERDEF:
                                    this.mdicAllKey = SFIS_PRINT_SYSTEM.BLL.publicfunction.GetMacPwdByUse(this.mWoInfo.woId, ((MyTextBox)sender).Text.Trim());
                                    break;
                                default:
                                    break;
                            }
                            goto default;
                            break;
                        default:
                            //if (esnTemp != null || esnTemp.Rows.Count > 0)
                            //{
                            //    ShowMsg(mLogMsgType.Error, string.Format("序列号[{0}]已经被使用过了", ((MyTextBox)sender).Text.Trim()));
                            //    ((MyTextBox)sender).NotErr = true;
                            //    ((MyTextBox)sender).SelectAll();
                            //    return;
                            //}
                            break;
                    }
                    #endregion
                    #region 判断是否在同一个输入控件填写
                    bool bTemp = false;
                    foreach (string str in dicVarBuf.Keys)
                    {
                        if (str.ToUpper() == ((MyTextBox)sender).Name.ToUpper())
                        {
                            bTemp = true;
                            this.dicVarBuf.Remove(((MyTextBox)sender).Name.ToUpper());
                            break;
                        }
                    }
                    #endregion
                    this.dicVarBuf.Add(((MyTextBox)sender).Name.Trim(),
                        this.bInputUpper ? ((MyTextBox)sender).Text.Trim().ToUpper() : ((MyTextBox)sender).Text.Trim());
                    if (!bTemp)
                        this.miReadCount++;
                    #region  判断是否所有的序列号都已经记录完成
                    if (this.miReadCount >= MyVar.arrVariable.Length)
                    {
                      
                        #region  判断是否有刷esn序列号
                        //if (string.IsNullOrEmpty(this.mCurrentEsn))
                        //{
                        //    ShowMsg(mLogMsgType.Warning, "刷入的序列号中不包含esn,请确认..");
                        //    return;
                        //}
                        #endregion
                        #region  判断所有的输入内容是否都是正确的
                        bool isErr = false;
                        for (int i = 0; i < this.MyVar.arrVariable.Length; i++)
                        {
                            if (arrTextbox[i].NotErr)
                            {
                                isErr = true;
                                ShowMsg(mLogMsgType.Error, string.Format("输入存在错误:{0}", arrTextbox[i].Text));
                                arrTextbox[i].SelectAll();
                                arrTextbox[i].Focus();
                                return;
                            }
                            isErr = false;
                        }
                        #endregion
                        if (!isErr)
                        {//如果所有的输入都没有错误 则开始打印

                            this.enaothergroup(false);
                            eventRunFunction = new delegateRunNoParmet(this.mPrinterLable);
                            miasyncresult = eventRunFunction.BeginInvoke(null, null);
                            #region 添加到委托线程
                            //if (this.mPrintLable())
                            //{
                            //    this.miReadCount = 0;
                            //    this.FlagLeave = false;
                            //    this.dicVarBuf.Clear();
                            //    this.InitMyControl();
                            //    //显示数据
                            //    if (iasyncresult2 == null || iasyncresult2.IsCompleted)
                            //    {
                            //        this.eventshowotherdata = new delegateShowOtherData(this.ShowOtherData);
                            //        this.iasyncresult2 = this.eventshowotherdata.BeginInvoke(this.mWoInfo.woId, bShowData, null, null);
                            //    }
                            //}
                            //else
                            //{
                            //    throw new Exception("标签打印失败,请检查....");
                            //}
                            #endregion
                            //判断当前序列号组合得到的esn的流程是否和当前流程相符
                            //m_sqllib.ConnectDataBase();
                            //dp = new delegatePrinter(Printer);
                            //miasyncresult = dp.BeginInvoke(arrVariableBuffer, MyVar.arrVariable,
                            //    prtFilename, m_sqllib, this.check_rp.Checked, this._repeat, null, null);
                        }
                        this.miReadCount = 0;
                        //FlagLeave = false;
                        //this.dicVarBuf.Clear();
                        ////arrVariableBuffer = new string[MyVar.arrVariable.Length];
                        //arrTextbox[this.miReadCount].Focus();
                    }
                    #endregion
                    //焦点移到下一个控件
                    arrTextbox[this.miReadCount].Focus();
                }
                catch (System.Exception ex)
                {
                    miReadCount = 0;
                    FlagLeave = false;
                    this.dicVarBuf.Clear();
                    this.InitMyControl();
                    ShowMsg(mLogMsgType.Error, ex.Message);
                }
            }
        }
        private void enaothergroup(bool isena)
        {
            this.gpotherprint.Invoke(new EventHandler(delegate
            {
                this.gpotherprint.Enabled = isena;
            }));
        }
        private void mPrinterLable()
        {
            if (this.mPrintLable())
            {
                //显示数据
                if (iasyncresult2 == null || iasyncresult2.IsCompleted)
                {
                    this.eventshowotherdata = new delegateShowOtherData(this.ShowOtherData);
                    this.iasyncresult2 = this.eventshowotherdata.BeginInvoke(this.mWoInfo.woId, bShowData, null, null);
                }
            }
            else
            {
                this.ShowMsg(mLogMsgType.Error, "标签打印失败,请检查....");
            }
            this.miReadCount = 0;
            this.FlagLeave = false;
            this.dicVarBuf.Clear();
            this.enaothergroup(true);
            this.InitMyControl();

        }
        private void textbox_Leave(object sender, EventArgs e)
        {
            if (!bdebug)
            {
                if (!FlagLeave)
                {
                    //  textbox_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                }
            }
        }

        private void imbt_CreateKeyFrom_Click(object sender, EventArgs e)
        {
            //if (MessageBoxEx.Show("继续将改变产品KEY的值\n\n是否继续?", "警告",
            //    MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
            //    return;
            //else
            //{
            //    this.mCreateKeyFromProg = !mCreateKeyFromProg;
            //    if (mCreateKeyFromProg)
            //    {
            //        this.imbt_CreateKeyFrom.Text = "KEY由模板产生";
            //        ShowMsg(mLogMsgType.Warning, "改变KEY的来源:由模板产生");
            //    }
            //    else
            //    {
            //        this.imbt_CreateKeyFrom.Text = "KEY由程序产生";
            //        ShowMsg(mLogMsgType.Outgoing, "改变KEY的来源:由程序产生");
            //    }
            //}
        }
        public string strEnaPwd = string.Empty;
        private void imbtRprint_Click(object sender, EventArgs e)
        {
            if (this.mTabItem.Name.Trim().ToUpper() == "TABITEM2")
            {
                this.ShowMsg(mLogMsgType.Warning, "当前正在使用打印卡通箱功能,不能使用该功能");
                return;
            }
            #region 添加密码输入
            EnaPwd ed = new EnaPwd(this);
            if (ed.ShowDialog() == DialogResult.OK)
            {
                if (!refWebtUserInfo.Instance.ChkUserInfoIdAndPwd(strEnaPwd.Split('-')[0], strEnaPwd.Split('-')[1]))
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
            this.ShowMsg(mLogMsgType.Outgoing, "开启重复打印");
            this.mRprint = true;
        }

        private void nudPrintNum_Leave(object sender, EventArgs e)
        {
            this.nudPrintNum.ReadOnly = true;
        }

        private void nudPrintNum_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.nudPrintNum.ReadOnly = false;
        }

        private void nudPrintNum_ValueChanged(object sender, EventArgs e)
        {
            this.mPrintNumber = (int)this.nudPrintNum.Value;
        }

        #region 卡通箱部分
        private void MAC_Printer_CheckedChanged(object sender, EventArgs e)
        {
            if (miasyncresult != null && !miasyncresult.IsCompleted)
            {
                ShowMsg(mLogMsgType.Warning, "程序正在初始化,请稍候...");
                return;
            }
            if (((CheckBox)sender).Checked)
            {
                ((CheckBox)sender).Text = "打印";
                this.mPrtColumns += string.Format(",{0}", ((CheckBox)sender).Name.Split('_')[0]);
            }
            else
            {
                this.mPrtColumns = mPrtColumns.Replace(string.Format(",{0}", ((CheckBox)sender).Name.Split('_')[0]), string.Empty);
                ((CheckBox)sender).Text = "";
            }
        }
        private void EnableEsnInputBox_CheckedChanged(object sender, EventArgs e)
        {
            if (miasyncresult != null && !miasyncresult.IsCompleted)
            {
                ShowMsg(mLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox)sender).Checked = false;
                return;
            }
            if (EnableEsnInputBox.Checked)
            {
                this.label19.Visible = true;
                this.tb_esninput.Enabled = true;
                this.tb_esninput.Visible = true;
                this.ESN_Printer.Visible = true;
            }
            else
            {
                this.label19.Visible = false;
                this.tb_macinput.Text = null;
                this.tb_esninput.Enabled = false;
                this.tb_esninput.Visible = false;
                this.ESN_Printer.Visible = false;
            }
        }
        private void EnableMacInputBox_CheckedChanged(object sender, EventArgs e)
        {
            if (miasyncresult != null && !miasyncresult.IsCompleted)
            {
                ShowMsg(mLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox)sender).Checked = false;
                return;
            }
            if (EnableMacInputBox.Checked)
            {
                this.label7.Visible = true;
                this.tb_macinput.Enabled = true;
                this.tb_macinput.Visible = true;
                this.MAC_Printer.Visible = true;
            }
            else
            {
                this.label7.Visible = false;
                this.tb_macinput.Text = null;
                this.tb_macinput.Enabled = false;
                this.tb_macinput.Visible = false;
                this.MAC_Printer.Visible = false;
            }
        }
        private void EnableSnInputBox_CheckedChanged(object sender, EventArgs e)
        {
            if (miasyncresult != null && !miasyncresult.IsCompleted)
            {
                ShowMsg(mLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox)sender).Checked = false;
                return;
            }
            if (EnableSnInputBox.Checked)
            {
                this.label8.Visible = true;
                this.tb_sninput.Enabled = true;
                this.tb_sninput.Visible = true;
                this.SN_Printer.Visible = true;
            }
            else
            {
                this.label8.Visible = false;
                this.tb_sninput.Text = null;
                this.tb_sninput.Enabled = false;
                this.tb_sninput.Visible = false;
                this.SN_Printer.Visible = false;
            }
        }
        private void EnableKtInputBox_CheckedChanged(object sender, EventArgs e)
        {
            if (miasyncresult != null && !miasyncresult.IsCompleted)
            {
                ShowMsg(mLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox)sender).Checked = false;
                return;
            }
            if (EnableKtInputBox.Checked)
            {
                this.label9.Visible = true;
                this.tb_ktinput.Enabled = true;
                this.tb_ktinput.Visible = true;
                this.KT_Printer.Visible = true;
            }
            else
            {
                this.label9.Visible = false;
                this.tb_ktinput.Text = null;
                this.tb_ktinput.Enabled = false;
                this.tb_ktinput.Visible = false;
                this.KT_Printer.Visible = false;
            }
        }
        private void EnablePCBASNInput_CheckedChanged(object sender, EventArgs e)
        {
            if (miasyncresult != null && !miasyncresult.IsCompleted)
            {
                ShowMsg(mLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox)sender).Checked = false;
                return;
            }
            if (EnablePCBASNInput.Checked)
            {
                this.label6.Visible = true;
                this.tb_pcbasninput.Enabled = true;
                this.tb_pcbasninput.Visible = true;
                this.PCBASN_Printer.Visible = true;
            }
            else
            {
                this.label6.Visible = false;
                this.tb_pcbasninput.Text = null;
                this.tb_pcbasninput.Enabled = false;
                this.tb_pcbasninput.Visible = false;
                this.PCBASN_Printer.Visible = false;
            }
        }
        private void EnableSPMACInput_CheckedChanged(object sender, EventArgs e)
        {
            if (miasyncresult != null && !miasyncresult.IsCompleted)
            {
                ShowMsg(mLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox)sender).Checked = false;
                return;
            }
            if (EnableSPMACInput.Checked)
            {
                this.label13.Visible = true;
                this.tb_spmacinput.Enabled = true;
                this.tb_spmacinput.Visible = true;
                this.SPMAC_Printer.Visible = true;
            }
            else
            {
                this.label13.Visible = false;
                this.tb_spmacinput.Text = null;
                this.tb_spmacinput.Enabled = false;
                this.tb_spmacinput.Visible = false;
                this.SPMAC_Printer.Visible = false;
            }
        }
        private void EnableDEKInput_CheckedChanged(object sender, EventArgs e)
        {
            if (miasyncresult != null && !miasyncresult.IsCompleted)
            {
                ShowMsg(mLogMsgType.Warning, "程序正在初始化,请稍候...");
                ((CheckBox)sender).Checked = false;
                return;
            }
            if (EnableDEKInput.Checked)
            {
                this.label5.Visible = true;
                this.tb_dekinput.Enabled = true;
                this.tb_dekinput.Visible = true;
                this.DEK_Printer.Visible = true;
            }
            else
            {
                this.label5.Visible = false;
                this.tb_dekinput.Text = null;
                this.tb_dekinput.Enabled = false;
                this.tb_dekinput.Visible = false;
                this.DEK_Printer.Visible = false;
            }
        }
        private void tbInputText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (string.IsNullOrEmpty(this.mPrintFileName))
                {
                    ShowMsg(mLogMsgType.Warning, "请先选择模板文件..");
                    ((TextBox)sender).SelectAll();
                    return;
                }
                if (!string.IsNullOrEmpty(((TextBox)sender).Text))
                {
                    SetTextBoxFocus(((TextBox)sender).Name);
                }
            }
        }
        private void s_TextBoxInput_Leave(object sender, EventArgs e)
        {
            ((TextBox)(sender)).BackColor = Color.White;
            ((TextBox)(sender)).ForeColor = Color.Black;
            try
            {
                #region 条件选择
                switch (((TextBox)sender).Name)
                {
                    case "tb_esninput":
                        ESN = "ESN";
                        ESNVALUE = this.bInputUpper ? ((TextBox)sender).Text.Trim().ToUpper() : ((TextBox)sender).Text.Trim();
                        break;
                    case "tb_macinput":
                        #region MAC
                        if (!string.IsNullOrEmpty(((TextBox)sender).Text.Trim()))
                        {
                            #region 检查ATE
                            //if (this.ATECheck)
                            //{
                            //    if (!this.atecheckflag)
                            //        this.atecheckflag |= m_freecomm.getATETestContent(((TextBox)sender).Text.Trim(), this.mponame, ref strError, m_sqllib1);
                            //}
                            #endregion
                            #region 检查Mac是否符合工单要求
                            MAC = "MAC";
                            MACVALUE = this.bInputUpper ? ((TextBox)sender).Text.Trim().ToUpper() : ((TextBox)sender).Text.Trim();
                           
                          //  2013.11.15
                            if (this.CompareSerialnumber(this.mWoInfo.woId,
                                ((TextBox)sender).Text.Trim(), "MAC"))
                            {
                                MAC = "MAC";
                                MACVALUE = this.bInputUpper ? ((TextBox)sender).Text.Trim().ToUpper() : ((TextBox)sender).Text.Trim();
                            }
                            else
                            {
                                ShowMsg(mLogMsgType.Error, string.Format("MAC::【{0}】不存在于该生产工单", ((TextBox)sender).Text));
                                ((TextBox)sender).SelectAll();
                                ((TextBox)sender).Focus();
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            return;
                        }
                        #endregion
                        break;
                    case "tb_sninput":
                        #region SN
                        if (!string.IsNullOrEmpty(((TextBox)sender).Text.Trim()))
                        {
                            #region 检查ATE
                            //if (this.ATECheck)
                            //{
                            //    if (!this.atecheckflag)
                            //        this.atecheckflag |= m_freecomm.getATETestContent(((TextBox)sender).Text.Trim(), this.mponame, ref strError, m_sqllib1);
                            //}
                            #endregion
                            #region 检查是否为递增序列号
                            if (mUseSnRule)
                            {
                                if (!CompareSnAreaHistory(((TextBox)sender).Text.Trim(), CIniConfig.IniReadValue("SETUP", "SnHistory", IniFilePath)))
                                {
                                    if (MessageBoxEx.Show("序列号不符合规则,是否继续?\n继续请按[Yes],取消请按[No]", "警告!!",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    {
                                        ((TextBox)sender).SelectAll();
                                        ((TextBox)sender).Focus();
                                        return;
                                    }
                                }
                            }
                            CIniConfig.IniWriteValue("SETUP", "SnHistory", ((TextBox)sender).Text.Trim(), IniFilePath);
                            #endregion
                            #region 检查是否符合工单要求
                            if (this.CompareSerialnumber(this.mWoInfo.woId,
                                ((TextBox)sender).Text.Trim(), "SN"))
                            {
                                SN = "SN";
                                SNVALUE = this.bInputUpper ? ((TextBox)sender).Text.Trim().ToUpper() : ((TextBox)sender).Text.Trim();
                            }
                            else
                            {
                                ShowMsg(mLogMsgType.Error, string.Format("当前输入【{0}】与工单设置不匹配,或已超出范围！！",
                                                    ((TextBox)sender).Text));
                                ((TextBox)sender).SelectAll();
                                ((TextBox)sender).Focus();
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            return;
                        }
                        #endregion
                        break;
                    case "tb_ktinput":
                        #region KT
                        if (!string.IsNullOrEmpty(((TextBox)sender).Text.Trim()))
                        {
                            if (this.CompareSerialnumber(this.mWoInfo.woId,
                                ((TextBox)sender).Text.Trim(), "KT"))
                            {
                                KT = "KT";
                                KTVALUE = this.bInputUpper ? ((TextBox)sender).Text.Trim().ToUpper() : ((TextBox)sender).Text.Trim();
                            }
                            else
                            {
                                ShowMsg(mLogMsgType.Error, string.Format("当前输入[{0}]与工单设置不匹配,或已超出范围！！",
                                                    ((TextBox)sender).Text));
                                ((TextBox)sender).SelectAll();
                                ((TextBox)sender).Focus();
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                        #endregion
                        break;
                    case "tb_pcbasninput":
                        #region PCBASN
                        if (!string.IsNullOrEmpty(((TextBox)sender).Text.Trim()))
                        {
                            #region 检查ATE
                            //if (this.ATECheck)
                            //{
                            //    if (!this.atecheckflag)
                            //        this.atecheckflag |= m_freecomm.getATETestContent(((TextBox)sender).Text.Trim(), this.mponame, ref strError, m_sqllib1);
                            //}
                            #endregion
                            #region 检查是否符合工单要求
                            if (this.CompareSerialnumber(this.mWoInfo.woId,
                                ((TextBox)sender).Text.Trim(), "PCBASN"))
                            {
                                PCBASN = "PCBASN";
                                PCBASNVALUE = this.bInputUpper ? ((TextBox)sender).Text.Trim().ToUpper() : ((TextBox)sender).Text.Trim();
                            }
                            else
                            {
                                ShowMsg(mLogMsgType.Error, string.Format("当前输入[{0}]与工单设置不匹配,或已超出范围！！",
                                    ((TextBox)sender).Text));
                                ((TextBox)sender).SelectAll();
                                ((TextBox)sender).Focus();
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            return;
                        }
                        #endregion
                        break;
                    case "tb_spmacinput":
                        #region SPMAC
                        if (!string.IsNullOrEmpty(((TextBox)sender).Text.Trim()))
                        {
                            if (this.CompareSerialnumber(this.mWoInfo.woId,
                                ((TextBox)sender).Text.Trim(), "SPMAC"))
                            {
                                SPMAC = "SPMAC";
                                SPMACVALUE = this.bInputUpper ? ((TextBox)sender).Text.Trim().ToUpper() : ((TextBox)sender).Text.Trim();
                            }
                            else
                            {
                                ShowMsg(mLogMsgType.Error, string.Format("当前输入[{0}]与工单设置不匹配,或已超出范围！！",
                                    ((TextBox)sender).Text));
                                ((TextBox)sender).SelectAll();
                                ((TextBox)sender).Focus();
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ((TextBox)sender).SelectAll();
                ((TextBox)sender).Focus();
                ShowMsg(mLogMsgType.Error, ex.Message);
            }
        }
        private void bt_ok_Click(object sender, EventArgs e)
        {
            InputEvent();
        }
        private void bt_ok_Enter(object sender, EventArgs e)
        {
            this.bt_ok_Click(sender, e);
        }
        private void InputEvent()
        {
            try
            {
                if (string.IsNullOrEmpty(this.mPrintFileName))
                {
                    ShowMsg(mLogMsgType.Warning, "没有选择模板文件..");
                    return;
                }
                if (iasyncresult != null && !iasyncresult.IsCompleted)
                {
                    ShowMsg(mLogMsgType.Warning, "工单序列号区间还在加载中,请稍候..");
                    return;
                }

                this.cbstationId.Enabled = false;
                this.cblineId.Enabled = false;
                #region MAC
                if (this.tb_macinput.Enabled)
                {
                    if (!string.IsNullOrEmpty(this.tb_macinput.Text.Trim()))
                    {
                        //判断序列号是否在工单区间范围内
                        if (!this.CompareSerialnumber(this.mWoInfo.woId, this.tb_macinput.Text.Trim()
                            , "MAC"))
                        {
                            ShowMsg(mLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                                tb_macinput.Text, "MAC"));
                            this.tb_macinput.SelectAll();
                            this.tb_macinput.Focus();
                            return;
                        }
                    }
                    else
                    {
                        this.tb_macinput.Focus();
                        return;
                    }
                }
                #endregion
                #region SN
                if (this.tb_sninput.Enabled)
                {
                    if (!string.IsNullOrEmpty(this.tb_sninput.Text.Trim()))
                    {
                        //判断序列号是否在工单区间范围内
                        if (!this.CompareSerialnumber(this.mWoInfo.woId, this.tb_sninput.Text.Trim()
                            , "SN"))
                        {
                            ShowMsg(mLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                                tb_sninput.Text, "SN"));
                            this.tb_sninput.SelectAll();
                            this.tb_sninput.Focus();
                            return;
                        }
                    }
                    else
                    {
                        this.tb_sninput.Focus();
                        return;
                    }
                }
                #endregion
                #region xxx-KT(2013-09-11)
                if (this.tb_ktinput.Enabled)
                {
                    if (!string.IsNullOrEmpty(this.tb_ktinput.Text.Trim()))
                    {
                        //判断序列号是否在工单区间范围内
                        if (!this.CompareSerialnumber(this.mWoInfo.woId, this.tb_ktinput.Text.Trim()
                            , "KT"))
                        {
                            ShowMsg(mLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                                tb_ktinput.Text, "KT"));
                            this.tb_ktinput.SelectAll();
                            this.tb_ktinput.Focus();
                            return;
                        }
                        //判断序列号是否在规定箱号内
                        string ckBoxMsgErr = CheckBoxNum(this.tb_ktinput.Text.Trim());
                        if (!string.IsNullOrEmpty(ckBoxMsgErr))
                        {
                            ShowMsg(mLogMsgType.Error, ckBoxMsgErr);
                            this.tb_ktinput.SelectAll();
                            this.tb_ktinput.Focus();
                            return;
                        }
                    }
                    else
                    {
                        this.tb_ktinput.Focus();
                        return;
                    }
                }
                #endregion
                #region PCBASN
                if (this.tb_pcbasninput.Enabled)
                {
                    if (!string.IsNullOrEmpty(this.tb_pcbasninput.Text.Trim()))
                    {
                        //判断序列号是否在工单区间范围内
                        if (!this.CompareSerialnumber(this.mWoInfo.woId, this.tb_pcbasninput.Text.Trim()
                            , "PCBASN"))
                        {
                            ShowMsg(mLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                                tb_pcbasninput.Text, "PCBASN"));
                            this.tb_pcbasninput.SelectAll();
                            this.tb_pcbasninput.Focus();
                            return;
                        }
                    }
                    else
                    {
                        this.tb_pcbasninput.Focus();
                        return;
                    }
                }
                #endregion
                #region SPMAC
                if (this.tb_spmacinput.Enabled)
                {
                    if (!string.IsNullOrEmpty(this.tb_spmacinput.Text.Trim()))
                    {
                        //判断序列号是否在工单区间范围内
                        if (!this.CompareSerialnumber(this.mWoInfo.woId, this.tb_spmacinput.Text.Trim()
                            , "SPMAC"))
                        {
                            ShowMsg(mLogMsgType.Error, string.Format("序列号[{1}]:[{0}]不在工单设置范围内,请检查..",
                                tb_spmacinput.Text, "SPMAC"));
                            this.tb_spmacinput.SelectAll();
                            this.tb_spmacinput.Focus();
                            return;
                        }
                    }
                    else
                    {
                        this.tb_spmacinput.Focus();
                        return;
                    }
                }
                #endregion

                #region  xxxx
                #region 清空控件内容，重新设定空间焦点
                this.tb_esninput.Clear();
                this.tb_macinput.Clear();
                this.tb_sninput.Clear();
                this.tb_ktinput.Clear();
                this.tb_pcbasninput.Clear();
                this.tb_spmacinput.Clear();
                SetTextBoxFocus("");
                #endregion
                #region 当前刷入的内容和内容类型
                //记录当前卡通箱包装刷入了哪些序列号的值
                string[] arrV = null;
                string strV = string.Format("{0},{1},{2},{3},{4}", MACVALUE, SNVALUE, KTVALUE, PCBASNVALUE, SPMACVALUE);
              
                //记录当前卡通箱包装刷如了哪些序列号类型
                string[] arrN = null;
                string strN = string.Format("{0},{1},{2},{3},{4}", MAC, SN, KT, PCBASN, SPMAC);

                if ((arrV = strV.Trim().Split(',')).Length != (arrN = strN.Trim().Split(',')).Length)
                    throw new Exception("错误:序列号类型个数和序列号值的个数不一致,请检查..");

                List<string> ls = new List<string>();
                foreach (string str in strV.Split(','))
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        ls.Add(str);
                    }
                }
                arrV = ls.ToArray();
                ls.Clear();
                foreach (string str in strN.Split(','))
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        ls.Add(str);
                    }
                }
                arrN = ls.ToArray();
                ls.Clear();

                #endregion
                #region 临时的内部变量
                int i = 0;
                string __err = string.Empty;
                string __strEsnTemp = string.Empty;
                DataTable __mdt = new DataTable();
                DataRow[] __arrDr = null;
                //用来记录需要新绑定的序列号
                Dictionary<string, string> __dicInsertTemp = new Dictionary<string, string>();
                List<string> __lsstrV = new List<string>();
                #endregion
                //判断卡通箱刷入的每个序列号返回的esn是否是指向相同的一个
                __strEsnTemp = ESNVALUE;
                #region 判断流程与工单 20131125 michael
                DataTable dtwip = ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetQueryWipAllInfo("ESN", __strEsnTemp));
                if (dtwip == null || dtwip.Rows.Count < 1)
                    throw new Exception("WIP No Data,请检查..");
                if (dtwip.Rows[0]["WOID"].ToString() != tbwoid.Text.Trim())
                    throw new Exception("工单不同: [" + dtwip.Rows[0][1].ToString() + "],请检查..");
                if (!CHECK_PRODUCT_LINE())
                    throw new Exception("请切换线别");

                //需要添加流程CHECK  重复打印要不要检查流程？？？？？？？？？
                __err = ChkRoute(__strEsnTemp, this.mCraftName);
                if (__err.ToUpper() != "OK")
                    throw new Exception("流程错误:当前esn:" + __strEsnTemp + ":" + __err);
                this.ShowMsg(mLogMsgType.Outgoing, "流程检测通过");
                #endregion



                #region 处理刷入的内容
                foreach (string item in arrV)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        __mdt = SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetSnInfo(/*arrN[i]*/ item));
                        //判断是否存在  做到在卡通箱包装可以绑定序列号
                        if (__mdt == null || __mdt.Rows.Count < 1)
                        {
                            __lsstrV.Add(item);
                            __dicInsertTemp.Add(arrN[i], item);
                            i++;
                            continue;
                        }
                        if (__mdt != null && __mdt.Rows.Count > 1)
                            throw new Exception("严重错误:同一个序列号[" + item + "]系统中存在多笔,请检查..");
                        else
                        {
                            //判断是否是同一个工单
                            if (this.mWoInfo.woId.Trim().ToUpper() != __mdt.Rows[0]["woId"].ToString().Trim().ToUpper())
                                throw new Exception(string.Format("序列号{0}在其他工单[0]中存在,请检查..", item, __mdt.Rows[0]["woId"].ToString()));
                         //   __strEsnTemp = __mdt.Rows[0]["esn"].ToString().Trim();

                            //通过esn找到该esn所对应的所有数据和当前的输入值进行比对看看是否是一致的
                            /********如果当前输入的值在esn找到的数据中不存在，
                            *********那么需要拿这个值到系统中查找看看是否已经使用过了，
                            *********如果没有使用过则绑定到当前数据 ，如果使用过了则直接报错*/
                            __mdt = SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetEsnDataInfo("esn", __strEsnTemp));
                            //if (__mdt == null || __mdt.Rows.Count < 1)
                            //    throw new Exception("严重错误:esn:" + __strEsnTemp + "找不到数据,请检查..");
        
                            #region 利用通过esn找到的数据和当前输入的数据进行比对
                            for (int x = 0; x < arrV.Length; x++)
                            {
                                if (!string.IsNullOrEmpty(arrV[x]))
                                {
                                    __arrDr = __mdt.Select(string.Format("woId='{0}' and sntype='{1}' and snval='{2}'",
                                        this.mWoInfo.woId, arrN[x], arrV[x]));
                                    __arrDr = __mdt.Select(string.Format("woId='{0}' and sntype='{1}'",
                                        this.mWoInfo.woId, arrN[x]));
                                    //如果序列号类型找到了但是值不相等 怎么办
                                    if (__arrDr != null && __arrDr.Length > 1)
                                        throw new Exception("严重错误:序列号类型:" + arrN[x] + "存在多个,请检查...");
                                    if (__arrDr != null && __arrDr.Length == 1)
                                    {
                                        if (__arrDr[0]["snval"].ToString().ToUpper() != arrV[x].ToUpper())
                                            throw new Exception(string.Format("序列号类型:[{0}]当前输入的值:[{1}],和历史记录数据[{2}]不相等.记录失败!!!",
                                                arrN[x], arrV[x], __arrDr[0]["snval"].ToString()));
                                    }
                                    if (__arrDr == null || __arrDr.Length < 1)//只有类型都找不到的情况下才可以进行判断是否需要添加
                                    {
                                        //需要查看一下这个号是否存在与__dicInsertTemp中,以避免多次查询
                                        if (!this.CompareArray(arrV[x], __lsstrV.ToArray()))
                                        {
                                            //再到数据库中查找一下看看是否有且工单和esn是一致的
                                            DataTable _dt = SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetEsnDataInfo(/*arrN[x]*/string.Empty, arrV[x]));
                                            if (_dt == null || _dt.Rows.Count < 1)
                                            {
                                                __lsstrV.Add(arrV[x]);
                                                __dicInsertTemp.Add(arrN[x], arrV[x]);
                                                continue;
                                            }
                                            if (_dt != null && _dt.Rows.Count > 1)
                                                throw new Exception("严重错误:同一个序列号[" + arrV[x] + "]系统中存在多笔,请检查..");
                                            else
                                            {
                                                if (_dt.Rows[0]["woId"].ToString().Trim() != this.mWoInfo.woId.Trim())
                                                    throw new Exception("序列号在另一个工单[" + _dt.Rows[0]["woId"].ToString() + "]中使用过了,请检查....");
                                                if (_dt.Rows[0]["esn"].ToString().Trim() != __strEsnTemp)
                                                    throw new Exception(string.Format("序列号{0}已经在其他的产品上使用过了esn:{1}",
                                                        arrV[x], _dt.Rows[0]["esn"].ToString()));
                                                if (_dt.Rows[0]["sntype"].ToString().Trim() != arrN[x])
                                                    throw new Exception(string.Format("序列号对应的序列号类型不一致{0}≠{1}",
                                                        _dt.Rows[0]["sntype"].ToString().Trim(), arrN[x]));
                                                {
                                                    throw new Exception("不明错误,请联系系统管理员,谢谢");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                            break;
                        }
                    }
                }
                #endregion

                #region 判断数据有效性
                //卡通箱包装有没有ESN刷入??????

                //if (string.IsNullOrEmpty(__strEsnTemp))
                //{
                //    DataTable mdt = BLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetEsnInfoByWoId(this.mWoInfo.woId));
                //    bool bl = false;
                //    foreach (DataRow dr in mdt.Rows)
                //    {
                //        foreach (string str in __dicInsertTemp.Values)
                //        {
                //            if (mdt == null || mdt.Rows.Count < 1)
                //            {
                //                bl = false;
                //                break;
                //            }
                //            if (string.Compare(str, dr["snstart"].ToString()) > -1 &&
                //                string.Compare(str, dr["snend"].ToString()) < 1)
                //            {
                //                bl = true;
                //                __strEsnTemp = str;
                //                break;
                //            }
                //        }
                //    }

                //    if (!bl)
                //        throw new Exception("错误:输入的序列号没有找到Esn,请确认该产品是否有投线..");
                //}
                //this.ShowMsg(mLogMsgType.Outgoing, "找到Esn:" + __strEsnTemp);
                #endregion

             

                #region  记录新绑定的内容
                foreach (string str in __dicInsertTemp.Keys)
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("ESN", __strEsnTemp);
                    dic.Add("SNTYPE", str);
                    dic.Add("SNVAL", __dicInsertTemp[str]);
                    dic.Add("WOID", this.mWoInfo.woId);
                    dic.Add("STATION", mCraftName);
                    dic.Add("KPNO", "NA");
                    string _strErr = refWebtWipTracking.Instance.InsertWipKeyPart(MapListConverter.DictionaryToJson(dic));                
                   _strErr = string.IsNullOrEmpty(_strErr) ? "OK" : _strErr;
                   if (_strErr != "OK")
                       throw new Exception(string.Format("错误:插入KeyParts失败[{0}]" , _strErr));
                    
                }
                #endregion
                //Stopwatch Mywatch = new Stopwatch();
                // Mywatch.Start();
                #region 使用箱号编码原则填充箱号包含(过站,记录产能）
                this.ShowMsg(mLogMsgType.Warning, "正在进行过站记录..");

                dic = new Dictionary<string, object>();
                dic.Add("CARTONNUMBER", this.mCartonId);
                dic.Add("LINE", this.mLineInfo.lineId);
                dic.Add("LOCSTATION", this.mCraftName);
                dic.Add("USERID", this.mUserInfo.userId);
                dic.Add("ESN", __strEsnTemp);
                dic.Add("MCARTONNUMBER", this.mCartonId.ToUpper().Split('C')[1]);
                dic.Add("ERRFLAG", "0");
                __err = refWebtWipTracking.Instance.UpdateWipAndRecCartonBox(MapListConverter.DictionaryToJson(dic));
          
                #endregion
                #region 判断返回的信息
                //查看当前这一箱是否包满 xxx:xxx:xxx:
                if (__err.ToUpper().IndexOf("BOXISCLOSE") != -1)
                    throw new Exception("箱号" + mCartonId + "已经包装完成");

                if (__err.ToUpper().IndexOf("NEWBOX_ERR") != -1)
                    throw new Exception(string.Format("产生新的卡通箱编号时出错了:{0}..", __err));

                if (__err.ToUpper().IndexOf("OK") == -1 && __err.ToUpper().IndexOf("BOXCLOSE") == -1)
                    throw new Exception("错误:过站失败!!,错误信息:\n" + __err + "\n请联系管理员检查..");
                this.ShowMsg(mLogMsgType.Outgoing, "过站记录完成");
                this.miCatonBoxTotal = int.Parse(__err.Split(':')[2]);

                if (__err.ToUpper().IndexOf("BOXCLOSE") != -1)
                    this.ShowCartonStation(__err.Split(':')[2]);//显示卡通号的状态
                else
                    this.ShowCartonStation(__err.Split(':')[1]);//显示卡通号的状态
                #endregion
                #region 满箱执行打印
                if (__err.ToUpper().IndexOf("BOXCLOSE") != -1)   //包装关闭，开始打印
                {
                    List<string> lsColumns = new List<string>();
                    string[] arColumns = this.mPrtColumns.Split(',');
                    foreach (string str in arColumns)
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            lsColumns.Add(str);
                        }
                    }
                    string NewErr = string.Empty;
                    if (lsColumns != null && lsColumns.Count > 0)
                        NewErr = this.PrintCartonBox(SFIS_PRINT_SYSTEM.BLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetCartonPrintContent(this.mCartonId, lsColumns.ToArray())), this.mPrintNumber, false);
                    else
                        this.ShowMsg(mLogMsgType.Warning, "当前没有选择需要打印的内容,打印不执行..");
                    //赋值给新的卡通箱编号
                    this.mCartonId = __err.Split(':')[1];
                    this.SetCartonID(this.mCartonId);
                    this.ShowCartonStation("0");
                    if (!string.IsNullOrEmpty(NewErr))
                        throw new Exception("打印错误:" + NewErr);
                }
                #endregion
                //判断当前这条生产线包装到了第几箱&&获取当前最大的包装箱号
                // Mywatch.Stop();
                //  ShowMsg(mLogMsgType.Incoming, string.Format("完成打印用时{0}毫秒", Mywatch.ElapsedMilliseconds.ToString()));
                #endregion
            }
            catch (Exception ex)
            {
                ShowMsg(mLogMsgType.Error, ex.Message);
            }
            finally
            {
                if (ShowCartonIasyncresult == null || ShowCartonIasyncresult.IsCompleted)
                {
                    eventshowcartondata = new delegateShowCartonData(this.ShowCartonData);
                    ShowCartonIasyncresult = eventshowcartondata.BeginInvoke(this.chkShowdata.Checked, null, null);
                }
                #region 初始化变量
                MACVALUE = string.Empty;
                SNVALUE = string.Empty;
                KTVALUE = string.Empty;
                PCBASNVALUE = string.Empty;

                SN = string.Empty;
                MAC = string.Empty;
                KT = string.Empty;
                PCBASN = string.Empty;
                SPMAC = string.Empty;
                #endregion
                this.ShowMsg(mLogMsgType.Incoming, "初始化完成");
            }
        }
        /// <summary>
        /// 打印卡通箱内容
        /// </summary>
        /// <param name="dtPrint"></param>
        /// <param name="printNum"></param>
        /// <param name="printUserCartonId"></param>
        private string PrintCartonBox(DataTable dtPrint, int printNum, bool printUserCartonId)
        {
            ShowMsg(mLogMsgType.Incoming, string.Format("{0}{1}", "初始化设备..", System.DateTime.Now.ToString("HH:mm:ss")));
            if (dtPrint == null || dtPrint.Rows.Count < 1)
                return "错误:没有需要打印的内容1,请检查..";
            if (this.mLibdoc == null)
                return "模板文件没有初始化";
            string log = string.Empty;
            this.mLibdoc.ViewMode = enumViewMode.lppxViewModeSize;
            //bool boxflag = false;
            try
            {
                #region 处理模板公式下面的变量
                if (mLibdoc.Variables.Formulas.Count > 0)
                {
                    try
                    {
                        //填充模板公式变量的内容 主要用来填充该箱的附加信息(箱号、工单、重量等)
                        for (int i = 0; i < this.mLibdoc.Variables.Formulas.Count; i++)
                        {
                            //------2013-06-27
                            //if (mLibdoc.Variables.Formulas.Item(i + 1).Name.ToUpper() == "BOXNUM")
                            //    boxflag = true;
                            //if (boxflag)
                            //{
                            //    string valTemp = string.Format("upper(\"{0}\")", printUserCartonId ? dtPrint.Rows[0]["cartonId"].ToString() : int.Parse(dtPrint.Rows[0]["cartonnumber"].ToString()).ToString()).Trim();
                            //    mLibdoc.Variables.Formulas.Item("BOXNUM").Prefix = string.Empty;
                            //    // mLibdoc.Variables.Formulas.Item("BOXNUM").Length = valTemp.Length;
                            //    mLibdoc.Variables.Formulas.Item("BOXNUM").Expression = valTemp;
                            //}
                            //----------------------

                            //------2013-06-27------------
                            switch (mLibdoc.Variables.Formulas.Item(i + 1).Name.ToUpper())
                            {
                                case "BOXNUM":
                                    string valTemp = string.Format("upper(\"{0}\")", printUserCartonId ? dtPrint.Rows[0]["cartonId"].ToString() : int.Parse(dtPrint.Rows[0]["cartonnumber"].ToString()).ToString()).Trim();
                                    mLibdoc.Variables.Formulas.Item("BOXNUM").Prefix = string.Empty;
                                    // mLibdoc.Variables.Formulas.Item("BOXNUM").Length = valTemp.Length;
                                    mLibdoc.Variables.Formulas.Item("BOXNUM").Expression = valTemp;
                                    break;
                                case "COUNT":
                                    int icount = dtPrint.Rows.Count / (dtPrint.DefaultView.ToTable(true, "sntype").Rows.Count);
                                    mLibdoc.Variables.Formulas.Item("COUNT").Prefix = string.Empty;
                                    mLibdoc.Variables.Formulas.Item("COUNT").Expression = icount.ToString();
                                    break;
                                default:
                                    break;
                            }
                            //------------------------------
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
                DataTable OutNewTable;
                string err = string.Empty;
                //用于一个箱号分多张纸打印，而不满箱的情况标志
                bool cflag = false;
                if (!string.IsNullOrEmpty(err = this.GetPrintContentTable(ref dtPrint, out OutNewTable)))
                    return err;
                //if (dtPrint.Rows.Count > this.mLibdoc.Variables.FormVariables.Count)
                //    return "模板文件需要填充变量个数小于数据填充数量,请重新设置..";//2013-08-22
                #region 一箱一张纸
                //if (CNSFlag.ToUpper() == "CNS")
                if (mCartonFlag)
                {
                    string cartonnumber = dtPrint.Rows[0]["cartonId"].ToString();
                    this.mLibdoc.Variables.FormVariables.Item("CARTONNUMBER").Length = cartonnumber.Length;
                    this.mLibdoc.Variables.FormVariables.Item("CARTONNUMBER").Value = cartonnumber;

                }
                if (dtPrint.Rows.Count <= this.mLibdoc.Variables.FormVariables.Count)
                {
                    for (int a = 0; a < OutNewTable.Rows.Count; a++)
                    {
                        for (int i = 0; i < OutNewTable.Columns.Count; i++)
                        {
                            try
                            {
                                this.mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}", OutNewTable.Columns[i].ColumnName.ToUpper(), (a + 1).ToString())).Length =
                                    OutNewTable.Rows[a][i].ToString().Trim().Length;
                                this.mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}", OutNewTable.Columns[i].ColumnName.ToUpper(), (a + 1).ToString())).Value =
                                    OutNewTable.Rows[a][i].ToString().Trim();
                            }
                            catch (Exception ex)
                            {
                                return "卡通箱模板赋值出现错误:" + ex.Message;
                            }
                        }
                    }
                    //开始打印
                    this.mLibdoc.PrintDocument(this.mPrintNumber);
                    return string.Empty;
                }
                #endregion
                else
                {
                    #region 一箱多张纸打印情况
                    for (int a = 0, VarCount = 1; a < OutNewTable.Rows.Count; a++, VarCount++)
                    {
                        for (int i = 0; i < OutNewTable.Columns.Count; i++)
                        {
                            try
                            {
                                this.mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}", OutNewTable.Columns[i].ColumnName.ToUpper(), (VarCount).ToString())).Length =
                                    OutNewTable.Rows[a][i].ToString().Trim().Length;
                                this.mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}", OutNewTable.Columns[i].ColumnName.ToUpper(), (VarCount).ToString())).Value =
                                    OutNewTable.Rows[a][i].ToString().Trim();
                            }
                            catch (Exception ex)
                            {
                                return "卡通箱模板赋值出现错误:" + ex.Message;
                            }
                        }
                        //针对一个箱号分多张纸打印情况
                        if (VarCount == mLibdoc.Variables.FormVariables.Count)
                        {
                            //开始打印
                            this.mLibdoc.PrintDocument(this.mPrintNumber);
                            VarCount = 0;
                            cflag = true;
                        }
                        if (cflag)
                        {//先清空再赋值
                            for (int z = 0; z < this.mLibdoc.Variables.FormVariables.Count; z++)
                            {
                                this.mLibdoc.Variables.FormVariables.Item(z + 1).Prefix = string.Empty;
                                this.mLibdoc.Variables.FormVariables.Item(z + 1).Value = string.Empty;
                            }
                            cflag = false;
                        }

                    }
                    //最后一箱未满箱打印
                    if (!string.IsNullOrEmpty(this.mLibdoc.Variables.FormVariables.Item(1).Value))
                        this.mLibdoc.PrintDocument(this.mPrintNumber);
                }
                    #endregion
                #endregion

                #region 2013-04-13 修改(不能对SN排序)(待测试)
                //int __EsnRowCount = 0;
                //try
                //{
                //    //dtPrint : woId,cartonId,cartonnumber,esn,sntype,snval
                //    //1.判断用来填充模板的值的个数是否与模板变量数量一样多
                //    if (dtPrint.Rows.Count > this.mLibdoc.Variables.FormVariables.Count)
                //        return "模板文件需要填充变量个数小于数据填充数量,请重新设置..";


                //    //2. 找出不重复的esn（即有多少个产品要打印到卡通箱上）
                //    DataTable __EsnTable = dtPrint.DefaultView.ToTable(true, "esn");

                //    //3. 循环找到的不重复的esn 根据esn在去找其对应的其他的序列号以及序号类型
                //    for (int xx = 0; xx < __EsnTable.Rows.Count; xx++)
                //    {
                //        DataRow[] arrDr = dtPrint.Select(string.Format("esn='{0}'", __EsnTable.Rows[xx]["esn"].ToString()));
                //        if (__EsnRowCount > 0)
                //        {
                //            if (__EsnRowCount != arrDr.Length)
                //                return "ESN: " + __EsnTable.Rows[xx]["esn"].ToString() + "与其他的号内容数量不一致,请检查!!";
                //        }
                //        __EsnRowCount = arrDr.Length;
                //        foreach (DataRow DrItem in arrDr)
                //        {
                //            try
                //            {
                //                this.mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}", DrItem["sntype"].ToString().Trim().ToUpper(), (xx + 1).ToString())).Length =
                //                    DrItem["snval"].ToString().Trim().Length;

                //                this.mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}", DrItem["sntype"].ToString().Trim().ToUpper(), (xx + 1).ToString())).Value =
                //                    DrItem["snval"].ToString().Trim();
                //            }
                //            catch
                //            {
                //                return "模板文件中的类型异常";
                //            }
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    return "错误:" + ex.Message;
                //}
                #endregion

                #region 打印多个类型序列号时会产生乱序  暂停使用
                //使用需要打印的内容去填充模板变量
                //Dictionary<string, DataTable> _dicPrintContent = new Dictionary<string, DataTable>();
                //int varCount = 0;
                //string __err = this.GetPrintContent(ref dtPrint, out _dicPrintContent, out varCount);
                //if (!string.IsNullOrEmpty(__err))
                //    return __err;
                //if (_dicPrintContent == null || _dicPrintContent.Count < 1)
                //    return "错误:没有需要打印的内容2,请检查..";
                //if (varCount > this.mLibdoc.Variables.FormVariables.Count)
                //    return "模板文件需要填充变量个数小于数据填充数量,请重新设置..";
                //try
                //{
                //    foreach (string str in _dicPrintContent.Keys)
                //    {
                //        for (int x = 0; x < _dicPrintContent[str].Rows.Count; x++)
                //        {
                //            //if (this.mLibdoc.Variables.FormVariables.Item(x + 1).Name.ToUpper().IndexOf(str) == -1)
                //            //    return "模板文件中不存在需要打印的类型:" + str;
                //            try
                //            {
                //                this.mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}", str, (x + 1).ToString())).Length = _dicPrintContent[str].Rows[x]["snval"].ToString().Length;
                //                this.mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}", str, (x + 1).ToString())).Value = _dicPrintContent[str].Rows[x]["snval"].ToString();
                //                //MessageBox.Show(this.mLibdoc.Variables.FormVariables.Item(string.Format("{0}{1}", str, (x + 1).ToString())).Value);
                //            }
                //            catch
                //            {
                //                return "模板文件中的类型异常";
                //            }
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    return "错误:" + ex.Message;
                //}
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
                for (int y = 0; y < this.mLibdoc.Variables.Formulas.Count; y++)
                {
                    // mLibdoc.Variables.Formulas.Item(y + 1).Prefix = string.Empty;
                    //  mLibdoc.Variables.Formulas.Item(y + 1).Expression = string.Format("upper(\"{0}\")", 0);
                }
                for (int z = 0; z < this.mLibdoc.Variables.FormVariables.Count; z++)
                {
                    this.mLibdoc.Variables.FormVariables.Item(z + 1).Prefix = string.Empty;
                    this.mLibdoc.Variables.FormVariables.Item(z + 1).Value = string.Empty;
                }
            }
        }
        /// <summary>
        /// 处理从数据库中导出的需要打印到卡通箱的内容
        /// </summary>
        /// <param name="mdt">从数据库中导出的数据</param>
        /// <param name="_dicPrintContent">返回的处理过后的数据(排序后的)</param>
        private string GetPrintContent(ref DataTable mdt, out Dictionary<string, DataTable> _dicPrintContent, out int varCount)
        {
            varCount = 0;
            DataTable _dtTemp = new DataTable();
            int _rowCount = 0;
            _dicPrintContent = new Dictionary<string, DataTable>();
            DataTable _dt = mdt.DefaultView.ToTable(true, "sntype");
            if (_dt == null || _dt.Rows.Count < 1)
                return "没有任何数据..";
            foreach (DataRow dr in _dt.Rows)
            {
                _dtTemp = SFIS_PRINT_SYSTEM.BLL.publicfunction.getNewTable(mdt, string.Format("sntype='{0}'", dr["sntype"].ToString()));
                varCount += _dt.Rows.Count;
                if (_rowCount > 0)
                {
                    if (_rowCount != _dtTemp.Rows.Count)
                        return "错误:查询到的数据内容不一致,请检查";
                }
                _rowCount = _dtTemp.Rows.Count;
                _dtTemp.DefaultView.Sort = "snval asc";
                _dicPrintContent.Add(dr["sntype"].ToString(), _dtTemp.DefaultView.ToTable());
            }
            return string.Empty;
        }
        /// <summary>
        /// 将table横向排列
        /// </summary>
        /// <param name="mdt"></param>
        /// <param name="NewTable"></param>
        /// <returns></returns>
        private string GetPrintContentTable(ref DataTable mdt, out DataTable NewTable)
        {
            NewTable = new DataTable("Print");
            try
            {
                //找出有多少种序列号类型
                DataTable sntypedt = mdt.DefaultView.ToTable(true, "sntype");
                bool sortsn = false;
                bool sortkt = false;
                foreach (DataRow dritem in sntypedt.Rows)
                {
                    NewTable.Columns.Add(dritem["sntype"].ToString().Trim());
                    if ("SN" == dritem["sntype"].ToString().Trim().ToUpper())
                        sortsn = true;
                    if ("KT" == dritem["sntype"].ToString().Trim().ToUpper())                    
                        sortkt = true;
                  

                }
                DataTable esndt = mdt.DefaultView.ToTable(true, "esn");
                for (int i = 0; i < esndt.Rows.Count; i++)
                {
                    DataRow[] arrDr = mdt.Select(string.Format("esn='{0}'", esndt.Rows[i]["esn"].ToString().Trim()));
                    DataRow Newdr = NewTable.NewRow();
                    foreach (DataRow dr in arrDr)
                    {
                        Newdr[dr["sntype"].ToString().Trim()] = dr["snval"].ToString();
                    }
                    NewTable.Rows.Add(Newdr);
                }
                if (sortsn)
                    NewTable.DefaultView.Sort = "sn asc";
                if (sortkt)
                    NewTable.DefaultView.Sort = "KT asc";
                NewTable = NewTable.DefaultView.ToTable();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private void dgvNotCloseBoxNumber_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                return;
                if (dgvNotCloseBoxNumber.Rows.Count < 1)
                    return;
                if (this.dgvNotCloseBoxNumber["_lineId", e.RowIndex].Value.ToString() != this.mLineInfo.lineId)
                {
                    if (MessageBoxEx.Show("选中的数据不是有当前这条生产线进行生产的,是否继续?", "提示",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                        return;
                }
                if (this.dgvNotCloseBoxNumber["_cartonId", e.RowIndex].Value.ToString().ToUpper() !=
                    this.mCartonId.ToUpper())
                {
                    if (MessageBoxEx.Show(string.Format("当前在包的卡通箱号码为:[{0}],变更后的卡通箱号码为:[{1}]\n是否确认更换卡通箱进行包装？\n确认 请选择[Yes] 否则请选择[No]",
                         this.mCartonId, this.dgvNotCloseBoxNumber["_cartonId", e.RowIndex].Value.ToString()), "提示", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.No)
                    {
                        this.mCartonId = this.dgvNotCloseBoxNumber["_cartonId", e.RowIndex].Value.ToString().ToUpper();
                        this.gCartonInfo.woId = this.dgvNotCloseBoxNumber["_woId", e.RowIndex].Value.ToString().ToUpper();
                        this.gCartonInfo.lineId = this.dgvNotCloseBoxNumber["_lineId", e.RowIndex].Value.ToString().ToUpper();
                        this.gCartonInfo.mcartonnumber = this.dgvNotCloseBoxNumber["_cartonnumber", e.RowIndex].Value.ToString().ToUpper();
                        this.gCartonInfo.number = int.Parse(this.dgvNotCloseBoxNumber["_number", e.RowIndex].Value.ToString());
                        this.ShowCartonStation(this.gCartonInfo.number.ToString());
                    }
                }
            }
        }
        private void btRepearCartonBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvdata.SelectedRows.Count > 0)
                {
                    for (int i = 0; i < this.dgvdata.SelectedRows.Count; i++)
                    {
                        if (this.dgvdata.SelectedRows[i].Cells["flag"].Value.ToString().Trim() != "1")
                        {

                            if (MessageBoxEx.Show("当前卡通箱还没有包装完成,是否强制关闭?\n关闭后该箱将不能再包装,是否继续? \n继续请选择[YES] 返回请选择 [NO]",
                                "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                //强制关闭卡通箱
                                refWebtWipTracking.Instance.CloseCartonBox(this.dgvdata.SelectedRows[i].Cells["cartonId"].Value.ToString());
                            }
                            else
                                throw new Exception("卡通箱还没有关闭,不能打印..");
                        }
                        this.RepearePrintCarton(this.dgvdata.SelectedRows[i].Cells["cartonId"].Value.ToString());
                    }
                }
                else
                {
                    this.ShowMsg(mLogMsgType.Warning, "没有选中任何需要打印的信息,请重新选择..");
                }
            }
            catch (Exception ex)
            {
                this.ShowMsg(mLogMsgType.Error, ex.Message);
            }
        }
        private void chkShowdata_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.dgvdata.ContextMenuStrip = this.contextMenuStrip2;
                this.IsShowCartonContent = false;
                this.ShowCartonData(this.chkShowdata.Checked);
            }
            catch (Exception ex)
            {
                this.ShowMsg(mLogMsgType.Error, ex.Message);
            }
        }
        #endregion

        private void s_Person_Enter(object sender, EventArgs e)
        {
            ((TextBox)(sender)).BackColor = Color.Green;
            ((TextBox)(sender)).ForeColor = Color.White;
        }
        private void s_Person_Leave(object sender, EventArgs e)
        {
            ((TextBox)(sender)).BackColor = Color.White;
            ((TextBox)(sender)).ForeColor = Color.Black;
        }
        #region 功能页面切换
        private void tbcLable_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            if (e.NewTab.Name != this.mTabItem.Name)
            {
                //是不是自动打开的
                if (bMask)
                {
                    if (MessageBoxEx.Show("切换后需要重新打开模板\n\n\n是否需要切换? ", "提示",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        this.mTabItem = e.NewTab;
                        this.tbcLable.SelectedTab = mTabItem;
                        this.gpotherprint.Controls.Clear();
                        this.gpotherprint.Refresh();
                        SetBtOkState(false);
                        //this.bt_ok.Enabled = false;

                        this.pictureBox1.Image = null;
                        this.mPrintFileName = this.lb_showmfpath.Text = "";
                    }
                    else
                    {
                        this.tbcLable.SelectedTab = e.OldTab;
                    }
                }
                else
                {
                    this.mTabItem = e.NewTab;
                    this.tbcLable.SelectedTab = mTabItem;
                    this.gpotherprint.Controls.Clear();
                    this.gpotherprint.Refresh();
                    SetBtOkState(false);
                }
            }
            bMask = true;
        }

        private void SetBtOkState(bool ena)
        {
            this.bt_ok.Invoke(new EventHandler(delegate
            {
                this.bt_ok.Enabled = ena;
                this.label11.Visible = ena;
                this.lb_cartoncount.Text = "";
                this.lb_cartoncount.Visible = ena;
                this.tb_Boxcount.Visible = ena;
                this.tb_Boxcount.Text = "";
                this.chkShowdata.Visible = ena;
                if (!ena)
                {
                    this.EnableMacInputBox.Checked = false;
                    this.EnableSnInputBox.Checked = false;
                    this.EnableKtInputBox.Checked = false;
                    this.EnablePCBASNInput.Checked = false;
                    this.EnableSPMACInput.Checked = false;
                    this.EnableDEKInput.Checked = false;
                    this.EnableEsnInputBox.Checked = false;
                }
            }));
        }
        #endregion
        #region 鼠标状态
        private void dataGridView1_MouseEnter(object sender, EventArgs e)
        {
            this.bdebug = true;
        }
        private void dataGridView1_MouseLeave(object sender, EventArgs e)
        {
            this.bdebug = false;
        }
        private void rtb_Msg_MouseDown(object sender, MouseEventArgs e)
        {
            this.gpotherprint.Focus();
        }
        private void rtb_Msg_MouseEnter(object sender, EventArgs e)
        {
            bdebug = true;
        }
        private void rtb_Msg_MouseLeave(object sender, EventArgs e)
        {
            bdebug = false;
        }
        #endregion

        private void cbstationId_Leave(object sender, EventArgs e)
        {
            // this.cbstationId.Enabled = false;
        }

        private void cblineId_Leave(object sender, EventArgs e)
        {
            // this.cblineId.Enabled = false;
        }

        public void imbtexit_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Windows.Forms.Application.Exit();
        }   

        private void dgvdata_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.mTabItem.Name.Trim().ToUpper() == "TABITEM2")
            {
                string catonId = string.Empty;
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    if (!IsShowCartonContent)
                    {
                        catonId = this.dgvdata["cartonId", e.RowIndex].Value.ToString();
                        this.dgvdata.DataSource = BLL.ReleaseData.arrByteToDataTable(refWebtWipTracking.Instance.GetCartonContent(catonId));
                        IsShowCartonContent = true;
                        this.dgvdata.ContextMenuStrip = null;
                    }
                }
            }
        }

        private void btInputToUpper_Click(object sender, EventArgs e)
        {
            bInputUpper = !bInputUpper;
            if (bInputUpper)
            {
                this.btInputToUpper.Text = "强制输入大写 = 开启";
                this.ShowMsg(mLogMsgType.Outgoing, "强制输入大写 = 开启");
            }
            else
            {
                this.btInputToUpper.Text = "强制输入大写 = 关闭";
                this.ShowMsg(mLogMsgType.Outgoing, "强制输入大写 = 关闭");
            }
        }

        private bool ChkSerial(string startSn, string strVal, int iusenum)
        {
            if (startSn.Length != strVal.Length)
                return false;
            if (startSn.ToUpper() == strVal.ToUpper())
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
            if (IsNum(str2))
            {
                if ((int.Parse(str2) - int.Parse(str1)) % iusenum == 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if ((int.Parse(str2, System.Globalization.NumberStyles.HexNumber) -
                    int.Parse(str1, System.Globalization.NumberStyles.HexNumber)) % iusenum == 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsNum(String str)
        {
            return false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] <= '0' || str[i] >= '9')
                    return false;
            }
            return true;
        }
        public bool isNumberic(string str)
        {
            try
            {
                Convert.ToInt32(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool bIsChkMac = true;
        private bool bIsChkSpmac = true;
        private void imbt_chkmac_Click(object sender, EventArgs e)
        {
            #region 添加密码输入
            EnaPwd ed = new EnaPwd(this);
            if (ed.ShowDialog() == DialogResult.OK)
            {
                if (!refWebtUserInfo.Instance.ChkUserInfoIdAndPwd(strEnaPwd.Split('-')[0], strEnaPwd.Split('-')[1]))
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
            this.bIsChkMac = !bIsChkMac;
            if (!this.bIsChkMac)
            {
                this.imbt_chkmac.Text = "ChkMac  -  FALSE";
                this.imbt_chkmac.ForeColor = Color.Red;
            }
            else
            {
                this.imbt_chkmac.Text = "ChkMac  -  TRUE";
                this.imbt_chkmac.ForeColor = Color.Green;
            }
        }

        private void imbt_chkspmac_Click(object sender, EventArgs e)
        {
            #region 添加密码输入
            EnaPwd ed = new EnaPwd(this);
            if (ed.ShowDialog() == DialogResult.OK)
            {
                if (!refWebtUserInfo.Instance.ChkUserInfoIdAndPwd(strEnaPwd.Split('-')[0], strEnaPwd.Split('-')[1]))
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
            this.bIsChkSpmac = !bIsChkSpmac;
            if (!this.bIsChkSpmac)
            {
                this.imbt_chkspmac.Text = "ChkSPMac  -  FALSE";
                this.imbt_chkspmac.ForeColor = Color.Red;
            }
            else
            {
                this.imbt_chkspmac.Text = "ChkSPMac  -  TRUE";
                this.imbt_chkspmac.ForeColor = Color.Green;
            }
        }

        private void GetKTWoSnrule()
        {

        }
        /// <summary>
        /// CNS项目，Check箱号2013/09/06
        /// </summary>
        /// <param name="ktnum">KT号</param>
        string CNSFlag = string.Empty;
        string CNSCartonQTY = string.Empty;
        private string CheckBoxNum(string ktnum)
        {
            #region CNS项目，Check箱号2013/09/06
            string ErrMsg = string.Empty;

            if (CNSFlag.ToUpper() == "CNS")
            {
                string strinput = this.tb_ktinput.Text.Trim();
                //从本地access拉取序列号段
                BLL.cdbAccess ass = new cdbAccess();
                string _sql = string.Empty;
                _sql = string.Format("SELECT * FROM wosnrule where woid='{0}' and sntype='{1}' ",
                     this.tbwoid.Text, "KT");
                DataTable dtsnrule = ass.GetDatatable(_sql);
                //DataTable dtsnrule = BLL.ReleaseData.arrByteToDataTable(refWebtWoInfo.Instance.GetWoSnRule(this.tbwoid.Text, "KT"));
                if (dtsnrule == null || dtsnrule.Rows.Count < 1)
                {
                    ErrMsg = "工单" + this.tbwoid.Text + "没有设立KT区间";
                    return ErrMsg;
                }
                if (dtsnrule.Rows.Count == 1)
                {
                    string strstart = dtsnrule.Rows[0]["snstart"].ToString();
                    string strend = dtsnrule.Rows[0]["snend"].ToString();
                    if (strstart.Length != strend.Length)
                    {
                        ErrMsg = "两个字符串长度不一致~~";
                        return ErrMsg;
                    }
                    int index = GetIndex(strstart, strend); 

                    int istart = Convert.ToInt32(strstart.Substring(index));
                    int iinput = Convert.ToInt32(strinput.Substring(index));
                    int iend = Convert.ToInt32(strend.Substring(index));
                    string boxnum = this.tb_Boxcount.Text.Trim();
                    int realboxnum = (iinput - istart) / Convert.ToInt32(CNSCartonQTY) + Convert.ToInt32(minCarton);
                    int packboxnum = Convert.ToInt32(boxnum.Substring(boxnum.Length - 4));
                    if (realboxnum != packboxnum)
                    {
                        ErrMsg = "KT【" + strinput + "】应包装在【" + realboxnum + "】";
                        return ErrMsg;
                    }
                }
                if (dtsnrule.Rows.Count > 1)
                {
                    dtsnrule.DefaultView.Sort = "snstart asc";

                    ErrMsg = string.Format("工单有【{0}】个号段,请确认每个号段是否整箱,不然不能正确卡住箱号", dtsnrule.Rows.Count);


                }
            }
            return ErrMsg;
            #endregion
        }
        /// <summary>
        /// 获取索引，截取两个字符串不同的部分
        /// </summary>
        /// <param name="strstart"></param>
        /// <param name="strend"></param>
        /// <returns></returns>
        private int GetIndex(string strstart, string strend)
        {

            int index = 0;
            for (int s = 0; s < strstart.Length; s++)
            {
                if (strstart[s].CompareTo(strend[s]) != 0)
                {
                    index = s;
                    break;
                }
            }
            return index;
        }

        private void lb_showmfpath_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {

        }

        private void imbtlogout_Click(object sender, EventArgs e)
        {

        }
        private bool CHECK_PRODUCT_LINE()
        {
            bool flag = false;
            foreach (string str in this.mWoInfo.ProductLine.Split(','))
            {
                if (str == cblineId.Text)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                this.ShowMsg(mLogMsgType.Error, string.Format("此工单不可在{0}生产", cblineId.Text));
            return flag;
        }

    
    }

    /// <summary>
    /// 自定义控件类(TextBox)
    /// </summary>
    public class MyTextBox : TextBox
    {
        private bool _NotErr = false;
        public MyTextBox()
            : base()
        {
            this._NotErr = false;
        }
        public bool NotErr
        {
            get { return _NotErr; }
            set { _NotErr = value; }
        }
    }
    public class tUserInfo
    {
        /// <summary>
        /// 用户工号(主键)
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public string rolecaption { get; set; }
        /// <summary>
        /// 所在部门
        /// </summary>
        public string deptname { get; set; }
        /// <summary>
        /// 所属工厂编号
        /// </summary>
        public string facId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string pwd { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string userphone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string useremail { get; set; }
        /// <summary>
        /// 用户状态:0停用；1:启用
        /// </summary>
        public bool userstatus { get; set; }

        /// <summary>
        /// 保存用户的权限信息(progid and funid)
        /// </summary>
        public System.Data.DataTable userPopList { get; set; }
    }

    public class T_WO_INFO
    {


        /// <summary>
        /// 工单号
        /// </summary>
        public string woId
        { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string poId
        { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int qty { get; set; }
        /// <summary>
        /// 工单状态
        /// </summary>
        public int wostate { get; set; }
        /// <summary>
        /// 建立人
        /// </summary>
        public string userId
        { get; set; }

        /// <summary>
        /// 成品料号
        /// </summary>
        public string partnumber
        { get; set; }

        /// <summary>
        /// 成品名
        /// </summary>
        public string ProductName
        { get; set; }

        /// <summary>
        /// BOM版本
        /// </summary>
        public string bomver
        { get; set; }
        /// <summary>
        /// 工艺入口站
        /// </summary>
        public string inputgroup
        { get; set; }
        /// <summary>
        /// 工艺出口站
        /// </summary>
        public string outputgroup
        { get; set; }
        /// <summary>
        /// 工单类型
        /// </summary>
        public string wotype
        { get; set; }
        /// <summary>
        /// SAP工单类型
        /// </summary>
        public string sapwotype
        { get; set; }
        /// <summary>
        /// 产品版本
        /// </summary>
        public string per
        { get; set; }
        /// <summary>
        /// BOM编号（组装用BOM）
        /// </summary>
        public string bomnumber
        { get; set; }
        /// <summary>
        /// 生产流程编号
        /// </summary>
        public string routgroupId { get; set; }
        /// <summary>
        /// 产出数
        /// </summary>
        public int outputqty { get; set; }
        /// <summary>
        /// 投入数
        /// </summary>
        public int inputqty { get; set; }
        /// <summary>
        /// 报废数
        /// </summary>
        public int scrapqty { get; set; }
        /// <summary>
        /// 密码来源
        /// </summary>
        public ecpwd cpwd { get; set; }
        /// <summary>
        /// ATE脚本
        /// </summary>
        public string strAteScript { get; set; }

        /// <summary>
        /// 软件版本
        /// </summary>
        public string sw_ver { get; set; }

        /// <summary>
        /// 硬件版本
        /// </summary>
        public string fw_ver { get; set; }

        /// <summary>
        /// 网标前缀
        /// </summary>
        public string nal_prefix { get; set; }

        /// <summary>
        /// 检查BI站
        /// </summary>
        public string CHK_BI_ROUTE { get; set; }

        /// <summary>
        /// BI率
        /// </summary>
        public string BI_Proportion { get; set; }

        /// <summary>
        /// BI检查提示率
        /// </summary>
        public string BI_Warning { get; set; }

        /// <summary>
        /// 包装站检查标号
        /// </summary>
        public string CHECK_NO { get; set; }


        public string ProductLine { get; set; }

        public enum ecpwd
        {
            PROG,
            FILE,
            USERDEF
        }



        public enum WoInfoColumns
        {
            /// <summary>
            /// 工单号
            /// </summary>
            woId,
            /// <summary>
            /// 工单数量
            /// </summary>
            qty,
            /// <summary>
            /// 工单状态
            /// </summary>
            wostate,
            /// <summary>
            /// 建立人
            /// </summary>
            userId,
            /// <summary>
            /// 料号
            /// </summary>
            partnumber,
            /// <summary>
            /// BOM版本
            /// </summary>
            bomver,
            /// <summary>
            /// 工单投入站
            /// </summary>
            inputgroup,
            /// <summary>
            /// 工单出口站
            /// </summary>
            outputgroup,
            /// <summary>
            /// 工单生产使用的流程号
            /// </summary>
            routgroupId,
        }

        ///// <summary>
        ///// 工单类型
        ///// </summary>
        //public enum SnType
        //{
        //    正常工单 = 0,
        //    重工工单,
        //    RAM工单,
        //    试产工单,
        //    SMT工单,
        //    组包工单,
        //    外包工单
        //}
    }

    public class tLineInfo
    {
        /// <summary>
        /// 线体编号
        /// </summary>
        public string lineId { get; set; }
        /// <summary>
        /// 线体描述
        /// </summary>
        public string linedesc { get; set; }
        /// <summary>
        /// 分配的起始IP地址
        /// </summary>
        public string startIpAddress { get; set; }
        /// <summary>
        /// 分配的结束IP地址
        /// </summary>
        public string endIpAddress { get; set; }
        /// <summary>
        /// 所属车间编号
        /// </summary>
        public string wsId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 正在执行的计划编号
        /// </summary>
        public string plotId { get; set; }
    }

    public class tCartonInfo
    {
        /// <summary>
        /// 卡通箱编号
        /// </summary>
        public string cartonId { get; set; }
        /// <summary>
        /// 产品唯一序列号
        /// </summary>
        public string esn { get; set; }
        /// <summary>
        /// 产线编号
        /// </summary>
        public string lineId { get; set; }
        /// <summary>
        /// 工单编号
        /// </summary>
        public string woId { get; set; }
        /// <summary>
        /// 客户卡通箱编号
        /// </summary>
        public string mcartonnumber { get; set; }
        /// <summary>
        /// 产品SN号
        /// </summary>
        public string sn { get; set; }
        /// <summary>
        /// 产品MAC号
        /// </summary>
        public string mac { get; set; }
        /// <summary>
        /// 当前作业的电脑
        /// </summary>
        public string computer { get; set; }
        /// <summary>
        /// 当前该箱的数量
        /// </summary>
        public int number { get; set; }
    }
}
