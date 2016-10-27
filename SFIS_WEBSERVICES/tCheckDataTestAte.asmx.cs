using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using GenericUtil;
using System.Text;
namespace TestWeserver
{
    /// <summary>
    /// tCheckDataTestAte 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tCheckDataTestAte : System.Web.Services.WebService
    {

        BLL.ProPublicStoredproc ProPubStor = new BLL.ProPublicStoredproc();
        BLL.tLineInfo lineinfo = new BLL.tLineInfo();
        BLL.tWipTracking wiptracking = new BLL.tWipTracking();
        BLL.tWoInfo woinfo = new BLL.tWoInfo();
        BLL.tWipKeyPart wipkeyp = new BLL.tWipKeyPart();
        BLL.tUserInfo userinfo = new BLL.tUserInfo();
        BLL.tErrorCode _errcode = new BLL.tErrorCode();
        BLL.tProduct _Prod = new BLL.tProduct();
        BLL.T_EQC_TOOLS tet = new BLL.T_EQC_TOOLS();
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();
        BLL.T_TESTRESULT_INFO testresult = new BLL.T_TESTRESULT_INFO();
        BLL.tVersion_Mark_info VersionMark = new BLL.tVersion_Mark_info();
        BLL.tCraftInfo craftinfo = new BLL.tCraftInfo();
        BLL.FileHelper _FileHelper = new BLL.FileHelper();

        [WebMethod(Description = "确认权限,返回一个string类型")]
        public string CheckEmp(string EMP)
        {
            return mPro.PRO_CHECKEMP(EMP);
        }
        [WebMethod(Description = "确认权限,返回一个string类型")]
        public string CheckEmp_NEW(string EMP, string ipaddress, string macaddress, string mygroup)
        {
            return mPro.PRO_CHECKEMP_NEW(EMP, ipaddress, macaddress, mygroup);
        }

        [WebMethod(Description = "传入一个工单,返回该工单的所有途程Dataset")]
        public DataSet GetAllStation(string woId)
        {
            return woinfo.GetWOCraftInfo_ds(woId, "1");
        }

        [WebMethod(Description = "传入一个工单,返回该工单的所有途程XML")]
        public string GetAllStationXML(string woId)
        {
            DataSet _ds = woinfo.GetWOCraftInfo_ds(woId, "1");
            if (_ds == null || _ds.Tables[0] == null || _ds.Tables[0].Rows.Count < 1)
                return string.Empty;
            else
                return _ds.GetXml();
        }
        [WebMethod(Description = "传入一个工单,返回该工单的所有测试途程List 0 非测试途程,1 测试途程")]
        public List<string> GetWOCraftInfo(string woId, string Flag)
        {
            return woinfo.GetWOCraftInfo(woId, Flag);
        }
        [WebMethod(Description = "传入一个途程代码,返回该工单的所有测试途程List 0 非测试途程,1 测试途程")]
        public List<string> GetCraftInfoByRouteId(string RouteId, string Flag)
        {
            List<string> LsCraft = new List<string>();
            DataSet ds = woinfo.GetCraftInfoByRouteId(RouteId, Flag);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtcraft = ds.Tables[0];
                if (dtcraft.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtcraft.Rows)
                    {
                        LsCraft.Add(dr["craftname"].ToString());
                    }
                }
            }
            return LsCraft;

        }

        [WebMethod(Description = "传入输入参数 DATA(唯一条码ESN),途程 Route,工单 WO,返回一个string类型的参数 ")]
        public string Check_Route_ATE(string DATA, string Route, string WO)
        {
            string Storedproc = "pro_Check_Route_ATE";

            IDictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DATA", DATA);
            dic.Add("MYGROUP", Route);
            dic.Add("WO", WO);
            return mPro.ExecuteProcedure(Storedproc.ToUpper(), MapListConverter.DictionaryToJson(dic));
        }

        [WebMethod(Description = "传入输入参数 DATA(25位ESN,8位的不良代码,20位的机器代码,20位治具号,5位测试标记(PASS,ERROR),位数不足以空格补足),途程 Route,人员权限 EMP,线别 LINE,返回一个string类型的参数 ")]
        public string INS_ATE_Back(string DATA, string Route, string EMP, string LINE)   //
        {           
            string C_RES;
            mPro.PRO_INS_ATE_BACK(DATA, Route, "NA", Route + "2", EMP, LINE, out C_RES);
            return C_RES;
        }
        [WebMethod(MessageName = "ATE_Call_Back_First", Description = "传入输入参数 DATA(25位ESN,8位的不良代码,20位的机器代码,20位治具号,5位测试标记(PASS,ERROR),位数不足以空格补足),途程 Route,人员权限 EMP,线别 LINE,返回一个string类型的参数 ")]
        public string PRO_INS_ATE_BACK(string DATA, string MYGROUP,string SECTION_NAME,string STATION_NAME, string EMP, string LINE)   //
        {          
            string C_RES;
            mPro.PRO_INS_ATE_BACK(DATA, MYGROUP, SECTION_NAME, STATION_NAME, EMP, LINE, out C_RES);
            return C_RES;
        }
        [WebMethod(MessageName = "ATE_Call_Back_Secord", Description = "Json")]
        public string PRO_INS_ATE_BACK(string Json)   //
        {
          return mPro.ExecuteProcedure("PRO_INS_ATE_BACK", Json);             
        }

        [WebMethod(Description = "传入输入参数 DATA(25位 ESN,8位不良代码,20位机器代码,20位治具号,5位测试标记(PASS,ERROR),位数不足以空格补足),返回一个string类型的参数,传入KeyParts,FLAG 0 必须传入KeyParts")]
        public string INS_ATE_BACK_INS_WIPKEYPARTS(string DATA, string MYGROUP, string EMP, string LINE, string diclswipkeypart, int Flag)
        {
            IList<IDictionary<string, object>> lsdic = MapListConverter.JsonToListDictionary(diclswipkeypart);

            string _StrErr = "OK";
            if (Flag == 0)
                _StrErr = wiptracking.InsertWipKeyParts(lsdic);
            if (_StrErr == "OK")
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("DATA", DATA);
                dic.Add("MYGROUP", MYGROUP);
                dic.Add("EMP", EMP);
                dic.Add("LINE", LINE);

                _StrErr = mPro.ExecuteProcedure("PRO_INS_ATE_BACK", MapListConverter.DictionaryToJson(dic));
            }
            return _StrErr;
        }

        [WebMethod(Description = "返回所有线体,DataSet")]
        public DataSet GetLineList()
        {
            return lineinfo.GetAllLineInfo();
        }
        [WebMethod(Description = "返回所有线体,XML")]
        public string GetLineListXML()
        {
            DataSet _ds = lineinfo.GetAllLineInfo();
            if (_ds == null || _ds.Tables[0] == null || _ds.Tables[0].Rows.Count < 1)
                return string.Empty;
            else
                return _ds.GetXml();
        }
        [WebMethod(Description = "返回所有线体,List")]
        public List<string> Get_Line_List()
        {
            return lineinfo.GetLineList();
        }

        [WebMethod(Description = "MAC等区间信息,Dataset")] //20151131后停用
        public DataSet GetWOSnRule(string woId)
        {
            return ProPubStor.GetWOSnRule(woId);
        }
        [WebMethod(Description = "MAC等区间信息,XML")] //20151131后停用
        public string GetWOSnRuleXML(string woId)
        {
            DataSet _ds = ProPubStor.GetWOSnRule(woId);
            if (_ds == null || _ds.Tables[0] == null || _ds.Tables[0].Rows.Count < 1)
                return string.Empty;
            else
                return _ds.GetXml();
        }
        [WebMethod(MessageName = "Sn_Rule_XML")]
        public string Get_WO_SnRule(string woId)
        {
            return ProPubStor.Get_WO_SnRule(woId).GetXml();
        }

        [WebMethod(Description = "插入wipkeypart数据,产品序列号绑定")]
        public string InsertWipKeyParts(List<Entity.tWipKeyPartTable> lswipkeypart)
        {
            IList<IDictionary<string, object>> LsDic = new List<IDictionary<string, object>>();
            IDictionary<string, object> dic = null;
            foreach (Entity.tWipKeyPartTable wpt in lswipkeypart)
            {
                dic = new Dictionary<string, object>();
                dic.Add("ESN", wpt.esn);
                dic.Add("WOID", wpt.woId);
                dic.Add("SNTYPE", wpt.sntype);
                dic.Add("SNVAL", wpt.snval);
                dic.Add("STATION", string.IsNullOrEmpty(wpt.station) ? "NA" : wpt.station);
                dic.Add("KPNO", string.IsNullOrEmpty(wpt.KPNO) ? "NA" : wpt.KPNO);
                dic.Add("RECDATE", System.DateTime.Now);
                LsDic.Add(dic);

            }
            if (LsDic.Count > 0)
                return wiptracking.InsertWipKeyParts(LsDic);
            else
                return "没有要插入的数据";

        }

        [WebMethod(Description = "根据esn获取数据,输入的可以是除密码外类型任何序号类型")]
        public DataSet GetEsnDataInfo(string sntype, string snval)
        {
            return wiptracking.GetEsnDataInfo(sntype, snval);
        }

        [WebMethod(Description = "根据esn获取数据,输入的可以是除密码外类型任何序号类型")]
        public string GetEsnDataInfoXML(string sntype, string snval)
        {
            DataSet _ds = wiptracking.GetEsnDataInfo(sntype, snval);
            if (_ds == null || _ds.Tables[0] == null || _ds.Tables[0].Rows.Count < 1)
                return string.Empty;
            else
                return _ds.GetXml();
        }
        [WebMethod(Description = "根据esn获取数据,输入的可以是除密码外类型任何序号类型")]
        public string GetEsnDataInfoXMLBySntype(string sntype, string snval, string[] arrSntype)
        {
            DataSet _ds = wiptracking.GetEsnDataInfo(sntype, snval);
            if (_ds == null || _ds.Tables[0] == null || _ds.Tables[0].Rows.Count < 1)
                return _ds.Clone().GetXml();
            else
            {
                string sql = string.Empty;
                foreach (string str in arrSntype)
                {
                    sql += string.Format("'{0}',", str);
                }
                if (!string.IsNullOrEmpty(sql))
                {
                    sql = sql.Substring(0, sql.Length - 1);
                    _ds = getNewTable(_ds.Tables[0], string.Format("sntype in({0})", sql));
                }
                return _ds.GetXml();
            }
        }
        [WebMethod(Description = "根据esn获取数据,输入的可以是除密码外类型任何序号类型")]
        public DataSet GetEsnDataInfoBySntype(string sntype, string snval, string[] arrSntype)
        {
            DataSet _ds = wiptracking.GetEsnDataInfo(sntype, snval);
            if (_ds == null || _ds.Tables[0] == null || _ds.Tables[0].Rows.Count < 1)
                return _ds.Clone();
            else
            {
                string sql = string.Empty;
                foreach (string str in arrSntype)
                {
                    sql += string.Format("'{0}',", str);
                }
                if (!string.IsNullOrEmpty(sql))
                {
                    sql = sql.Substring(0, sql.Length - 1);
                    _ds = getNewTable(_ds.Tables[0], string.Format("sntype in({0})", sql));
                }
                return _ds;
            }
        }
        private DataSet getNewTable(DataTable dt, string sql)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable mydt = new DataTable();
                mydt = dt.Clone();
                DataRow[] arrDr = dt.Select(sql);
                for (int i = 0; i < arrDr.Length; i++)
                {
                    mydt.ImportRow((DataRow)arrDr[i]);
                }
                ds.Tables.Add(mydt);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod(Description = "获取指定的序列号是否存在")]
        public System.Data.DataSet GetSnInfo(string serial)
        {
            return wiptracking.GetSnInfo(serial);
        }

        [WebMethod(Description = "获取指定的序列号是否存在")]
        public string GetSnInfoXML(string serial)
        {
            DataSet _ds = wiptracking.GetSnInfo(serial);
            if (_ds == null || _ds.Tables[0] == null || _ds.Tables[0].Rows.Count < 1)
                return string.Empty;
            else
                return _ds.GetXml();
        }

        [WebMethod(Description = "刷入SN,返回连接关系资料")]
        public System.Data.DataSet GetSnWipKeyPartsData(string SN)
        {
            return wiptracking.GetEsnDataInfo("ESN", SN);
        }
        [WebMethod(Description = "刷入SN,返回连接关系资料")]
        public string GetSnWipKeyPartsDataXML(string SN)
        {
            return wiptracking.GetEsnDataInfo("ESN", SN).GetXml();
        }

        [WebMethod(Description = "获取工单列表,返回List")]
        public List<string> GetWoList()
        {
            return woinfo.GetWoList();
        }

        [WebMethod]
        public DataSet GetATEScripts(string woid)
        {
            return woinfo.GetAteScripts(woid);
        }
        [WebMethod]
        public string GetAteScriptsStr(string woId)
        {
            return woinfo.GetAteScriptsStr(woId);
        }
        [WebMethod]
        public string GetAteScriptsXML(string woId)
        {
            return woinfo.GetAteScriptsXML(woId);
        }

        [WebMethod]
        public string DeleteLogOut(string userId, string strIpaddress)
        {
            return "OK";//  ProPubStor.DeleteLogOut(userId, strIpaddress);
        }
        /// <summary>
        /// 获取卡通箱需要打印的所有内容
        /// </summary>
        /// <param name="cartonId">卡通箱编号</param>
        /// <param name="Content">需要打印的内容</param>
        /// <returns></returns>
        [WebMethod(Description = "获取卡通箱需要打印的所有内容")]
        public DataSet GetCartonPrintContent(string cartonId, string[] Content)
        {
            return wiptracking.GetCartonPrintContent(cartonId, Content);
        }


        [WebMethod(Description = "产品投入,条码一阶到底,DATA:条码序号,LINE:线体,MYGROUP:途程,WO:投入工单,EMP:员工权限")]
        public string SP_TEST_INPUT_ALL(string DATA, string LINE, string MYGROUP, string WOID, string EMPNO)
        {
            string RES = string.Empty;
            mPro.PRO_TEST_INPUT_ALL(DATA, LINE, MYGROUP,"NA",MYGROUP, WOID, EMPNO, out RES);
            return RES;

        }

        /// <summary>
        /// 检查是否具有ate权限
        /// </summary>
        /// <param name="ateemp"></param>
        /// <returns></returns>
        [WebMethod]
        public string ChkUserAteEmp(string UserId, string CraftId)
        {
            return userinfo.ChkUserAteEmp(UserId, CraftId);
        }

        [WebMethod]
        public string GetErrorCode(string ec)
        {
            return _errcode.GetErrorCode_Desc(ec);
        }

        [WebMethod]
        public string GetErrorCodeXML()
        {
            DataSet _ds = _errcode.GetErrorCode(null);
            if (_ds == null || _ds.Tables[0] == null || _ds.Tables[0].Rows.Count < 1)
                return string.Empty;
            else
                return _ds.GetXml();
        }
        [WebMethod(Description = "手机客户ESN投板使用")]
        public string SP_PRO_SN_INPUT_MOBILE(string DATA, string MYGROUP, string LINE, string WO, string EMP)
        {
            return mPro.PRO_SN_INPUT_MOBILE(DATA, MYGROUP, LINE, WO, EMP);
        }
        [WebMethod(Description = "获取工单信息")]
        public DataSet GetWoInfoByWo(string woId)
        {
            return woinfo.GetWoInfo(woId, null,null);
        }
        [WebMethod(Description = "获取工单信息XML")]
        public string GetWoInfoByWoXml(string woId)
        {
            return woinfo.GetWoInfo(woId, null,null).GetXml();
        }
        [WebMethod(Description = "获取产品信息")]
        public DataSet GetProductByPartNumber(string PartNumber)
        {
            return _Prod.GetProduct(PartNumber, null);
        }
        [WebMethod]
        public List<string> GetProductLableNames(string partnumber)
        {
            return _Prod.GetProductLableNames(partnumber);
        }
        [WebMethod(Description = "Esn 产品sn,sntype 条码类型,woId 工单,snqty 需要获取几个条码[使用此方法]")]
        public List<string> GetSnMacImeiForAte(string Esn, string sntype, string woId, int snqty)
        {
            return woinfo.GetSnMacImeiForAte(Esn, sntype, woId, snqty);
        }

        [WebMethod(Description = "直通率接口")]
        public string Insert_T_TESTRESULT_INFO(Entity.tT_TESTRESULT_INFO tti)
        {
            IDictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("ESN", tti.ESN);
            dic.Add("WOID", tti.WOID);
            dic.Add("TESTSTATION", tti.TESTSTATION);
            dic.Add("TESTRESULT", tti.TESTRESULT);
            dic.Add("EQPID", tti.EQPID);
            dic.Add("EQPTYPE", tti.EQPTYPE);
            dic.Add("CUSER", tti.CUSER);
            dic.Add("FAILDESC", tti.FAILDESC);
            dic.Add("PARTNUMBER", tti.PARTNUMBER);
            dic.Add("PRODUCTNAME", tti.PRODUCTNAME);
            dic.Add("RepairFlag", tti.RepairFlag);
            return testresult.Insert_T_TESTRESULT_INFO(MapListConverter.DictionaryToJson(dic));
        }
        [WebMethod(Description = "获取工单QCN版本和TP固定版本---xml格式")]
        public string GetWoVersionInfoXML(string woid)
        {
            DataSet _ds = VersionMark.QueryVersionInfoByWo(woid);
            if (_ds == null || _ds.Tables[0] == null || _ds.Tables[0].Rows.Count < 1)
                return string.Empty;
            else
                return _ds.GetXml();
        }
        [WebMethod(Description = "获取产品料号的标示位---xml格式")]
        public string GetPnMarkBitInfoXML(string pn)
        {
            DataSet _ds = VersionMark.QueryMarkBitByPn(pn);
            if (_ds == null || _ds.Tables[0] == null || _ds.Tables[0].Rows.Count < 1)
                return string.Empty;
            else
                return _ds.GetXml();
        }
        [WebMethod(Description = "获取工单QCN版本和TP固定版本--dataset格式")]
        public DataSet GetWoVersionInfoDataSet(string woid)
        {
            return VersionMark.QueryVersionInfoByWo(woid);
        }
        [WebMethod(Description = "获取产品料号的标示位--dataset格式")]
        public DataSet GetPnMarkBitInfoDataSet(string pn)
        {
            return VersionMark.QueryMarkBitByPn(pn);
        }

        [WebMethod(Description = "传入输入参数 DATA(唯一条码ESN),途程 Route,工单 WO,治具 Tools,返回一个string类型的参数 ")]
        public string CHECK_ROUTE_TOOLS_ATE(string DATA, string MYGROUP, string WO, string Tools)
        {
            string _StrErr = string.Empty;
            _StrErr = tet.CHECK_TOOLS(Tools);
            if (_StrErr.Contains("OK"))
            {
                string Storedproc = "pro_Check_Route_ATE".ToUpper();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("DATA", DATA);
                dic.Add("MYGROUP", MYGROUP);
                dic.Add("WO", WO);
                _StrErr = mPro.ExecuteProcedure(Storedproc, MapListConverter.DictionaryToJson(dic));
            }
            return _StrErr;
        }
      //  [WebMethod(MessageName = "GetRouteToXML")]
        public string Get_Route_Info(string SECTION_NAME, string TEST_FLAG, string CHECK_TOOLS_FLAG)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(SECTION_NAME))
                dic.Add("BEWORKSEG", SECTION_NAME);
            if (!string.IsNullOrEmpty(TEST_FLAG))
                dic.Add("TESTFLAG", TEST_FLAG);
            if (!string.IsNullOrEmpty(CHECK_TOOLS_FLAG))
                dic.Add("CHECKTOOLSFLAG", CHECK_TOOLS_FLAG);
            DataTable dt = craftinfo.GetAllCraftInfo(dic).Tables[0];
            DataTable dtcraft = new DataTable();
            int x = 0;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    dtcraft.Columns.Add(dr["craftname"].ToString(), typeof(string));
                    if (dtcraft.Rows.Count == 0)
                    {
                        dtcraft.Rows.Add(dr["checktoolsflag"].ToString());
                    }
                    else
                    {
                        dtcraft.Rows[0][x++] = dr["checktoolsflag"].ToString();
                    }
                }
                catch
                {
                }
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtcraft);
            return ds.GetXml();
        }
        [WebMethod(MessageName = "GetRouteInfo")]
        public string Get_Route_Info(string SECTION_NAME, string CRAFTNAME, string TEST_FLAG, string CHECK_TOOLS_FLAG)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(SECTION_NAME))
                dic.Add("BEWORKSEG", SECTION_NAME);
            if (!string.IsNullOrEmpty(CRAFTNAME))
                dic.Add("CRAFTNAME", CRAFTNAME);
            if (!string.IsNullOrEmpty(TEST_FLAG))
                dic.Add("TESTFLAG", TEST_FLAG);
            if (!string.IsNullOrEmpty(CHECK_TOOLS_FLAG))
                dic.Add("CHECKTOOLSFLAG", CHECK_TOOLS_FLAG);
            DataSet ds = craftinfo.GetAllCraftInfo(dic);
            return ds.GetXml();
        }
        [WebMethod(MessageName = "CheckLineEmployee")]
        public string CHECK_SET_LINE_EMPLOYEE(string UserId, string PWD)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("USERID", UserId);
            dic.Add("PWD", PWD);

            return userinfo.CHECK_SET_LINE_EMPLOYEE(dic);
        }
        [WebMethod(MessageName = "PublicInterface")]
        public string STR_INTERFACE(string INTERFACE, string Json)
        {
            string _StrErr = string.Empty;
            switch (INTERFACE)
            {
                case "CHECK_SET_LINE_EMPLOYEE":
                    IDictionary<string, object> dic = MapListConverter.JsonToDictionary(Json);
                    _StrErr = userinfo.CHECK_SET_LINE_EMPLOYEE(dic);
                    break;
                default:
                    _StrErr = "Interface Error";
                    break;
            }
            return _StrErr;
        }

        [WebMethod(MessageName = "UPDATE_WIP_TRACKING")]
        public string UPDATE_WIP_TRACKING(string Json)
        {
            _FileHelper.Insert_DB_Log("CheckDataTestAte-->" + Json);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(Json);
            List<string> Fields = new List<string>();
            Fields.Add("ESN");
            return wiptracking.Update_WIP_TRACKING(mst, Fields);
        }
        [WebMethod(MessageName = "Get_WIP_TRACKING")]
        public string Get_WIP_TRACKING_XML(string Esn, string Fields)
        {
            return wiptracking.Get_WIP_TRACKING("ESN", Esn, Fields).GetXml();
        }

    }
}
