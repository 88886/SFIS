using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using GenericUtil;


namespace TestWeserver
{
    /// <summary>
    /// tWoInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tWoInfo : System.Web.Services.WebService
    {
        BLL.tWoInfo mWoinfo = new BLL.tWoInfo();
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();
        MapListConverter mlc = new MapListConverter();
        BLL.FileHelper _FileHelper = new BLL.FileHelper();
        
        /// <summary>
        /// 获取所有的工单信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetAllWoInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetWoInfo(null, null,null));
        }

        /// <summary>
        /// 获取所有工单
        /// </summary>
        /// <returns></returns>
        [WebMethod(MessageName = "GetWoInfo")]
        public byte[] GetWoInfo(string strWo, string Partnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetWoInfo(strWo, Partnumber, null));
        }
        /// <summary>
        /// 获取所有工单
        /// </summary>
        /// <returns></returns>
        [WebMethod(MessageName = "GetWoInfo_Custom")]
        public byte[] Get_WoInfo(string Json, string Fields)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(Json);
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetWoInfo(mst, Fields));
        }

        /// <summary>
        /// 获取所有工单
        /// </summary>
        /// <returns></returns>
        [WebMethod(MessageName = "GetWoInfo_Fields")]
        public byte[] GetWoInfo(string strWo, string Partnumber, string Fields)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetWoInfo(strWo, Partnumber,Fields));
        }

        /// <summary>
        /// 获取指定的工单信息
        /// </summary>
        /// <param name="strWo"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetWoInfoByWo(string strWo)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetWoInfo(strWo, null, null));
        }
        ///// <summary>
        ///// 获取指定料号的工单信息
        ///// </summary>
        ///// <param name="partnumber"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] GetWoInfoByPartNumer(string partnumber)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetWoInfo(null,partnumber));
        //}
        /// <summary>
        /// 新增工单信息
        /// </summary>
        /// <param name="TWI"></param>
        /// <param name="Err"></param>
        [WebMethod(MessageName = "InsertWoInfo")]
        public string InsertWoInfo(string dicwoinfo, string AteScript, string esn, string diclssnrule)
        {
            return mWoinfo.InsertWoInfo(dicwoinfo, AteScript, esn, diclssnrule);
        }
        /// <summary>
        /// 新增工单信息
        /// </summary>
        /// <param name="TWI"></param>
        /// <param name="Err"></param>
        [WebMethod(MessageName = "Insert_Wo_Info")]
        public string Insert_Wo_Info(string dicwoinfo, string AteScript, string Lisdicsnrule, string ESN_TYPE)
        {
            string _StrErr = "OK";         
            if (!string.IsNullOrEmpty(dicwoinfo))
            {
                if (_StrErr=="OK")
                _StrErr = mWoinfo.InsertWoInfo(dicwoinfo);
              
            }
            if (!string.IsNullOrEmpty(AteScript))
            {
                if (_StrErr == "OK")
                    _StrErr = mWoinfo.InsertAteScript(AteScript);
             
            }       
            if (!string.IsNullOrEmpty(Lisdicsnrule))
            {
                if (_StrErr == "OK")
                    _StrErr = mWoinfo.InserSnRule(Lisdicsnrule, ESN_TYPE);             
            }

            _FileHelper.Insert_DB_Log("Insert_Wo_Info->" + dicwoinfo + AteScript + Lisdicsnrule);
            return _StrErr;

        }

        /// <summary>
        /// 获取指定工单对应的所有序列号及其区间设置；
        ///  serialname,seriallimit,formatname
        /// </summary>
        /// <param name="woId"></param>
        //[WebMethod]
        //public byte[] GetWoSnRange(string woId)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetWoSnRange(woId));
        //}

        /// <summary>
        /// 获取指定工单对应的所有序列号及其区间设置
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetWoSnRule(string woid,string sntype)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetWoSnRule(woid,sntype));
        }
        [WebMethod]
        public byte[] Get_ESN_Rule(string woId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.Get_ESN_Rule(woId));
        }
        ///// <summary>
        ///// 检查序列号是否在区间内
        ///// </summary>
        ///// <param name="serial"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] ChkSerialNumberRule(string serial)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mWoinfo.ChkSerialNumberRule(serial));
        //}
        /// <summary>
        /// 获取工单编号列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<string> GetWoList()
        {
            return mWoinfo.GetWoList();
        }

        /// <summary>
        /// 检查工单是否有效
        /// </summary>
        /// <param name="woId"></param>
        /// <returns></returns>
        [WebMethod]
        public bool CheckWoISavailability(string woId)
        {
            return mWoinfo.CheckWoISavailability(woId);
        }
        //[WebMethod]
        //public List<string> GetPartNumberList()
        //{
        //    return mWoinfo.GetPartNumberList();
        //}
        [WebMethod]
        public byte[] GetWoInfoByState( string wostate, List<string> str)
        {
            return this.mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetWoInfoByStateOrwoId(wostate,null, str));

        }

        ///// <summary>
        ///// 更新工单的投入站和出口站
        ///// </summary>
        ///// <param name="woId"></param>
        ///// <param name="routgroupId"></param>
        ///// <param name="inputroutgroupid"></param>
        ///// <param name="outpputgroupid"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public string UpdateWoRoutegroup(string woId, string routgroupId,string inputroutgroupid, string outpputgroupid)
        //{
        //    return mWoinfo.UpdateWoRoutegroup(woId, routgroupId,  inputroutgroupid, outpputgroupid);
        //}
        [WebMethod]
        public string[] GetWoListByState(int wostate)
        {
            return mWoinfo.GetWoListByState(wostate);
        }

        [WebMethod]
        public string[] GetWoListByState_WinCE(int wostate)
        {
            return mWoinfo.GetWoListByState(wostate);
        }

        [WebMethod]
        public void InsetSnRule(string dicsnrule)
        {
            mWoinfo.InsetSnRule(dicsnrule);
        }

        [WebMethod]
        public string[] GetMaxSn()
        {
            string C_MAXSN=string.Empty;
            string C_WOID=string.Empty;
            string C_ROWID=string.Empty;
              mPro.PRO_GETSNEND(out C_MAXSN, out C_WOID, out C_ROWID);  //mWoinfo.GetMaxSn();
            string[] aa= new string[3];
            aa[0]=C_MAXSN;
            aa[1]=C_WOID;
            aa[2]=C_ROWID;

            return aa;
        }
        [WebMethod]
        public void UpdateSnRule(string Dicsnrule)
        {
            mWoinfo.UpdateSnRule(Dicsnrule);
        }
        ///// <summary>
        /////获取指定的工单全部信息
        ///// </summary>
        ///// <param name="strWo"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] GetALLWoInfoByWoinfo(string strWo)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetALLWoInfoByWoinfo(strWo));
        //}
        [WebMethod]
        public byte[] GetWoInfoBywoId(string woid, List<string> str)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetWoInfoByStateOrwoId(null,woid, str));
        }     

        ///// <summary>
        ///// 添加工单自定义密码
        ///// </summary>
        ///// <param name="lsUsePwd"></param>
        ///// <returns></returns>
        //[WebMethod(Description = "添加工单自定义密码")]
        //public string InserttUsePwd(List<Entity.tUsePwd> lsUsePwd)
        //{
        //    return mWoinfo.InserttUsePwd(lsUsePwd);
        //}
        [WebMethod]
        public byte[] GetEsnInfoByWoId(string woid)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetEsnInfoByWoId(woid));
        }
        [WebMethod]
        public byte[] GetATEScripts(string woid)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetAteScripts(woid));
        }
        [WebMethod]
        public string GetAteScriptsStr(string woId)
        {
            return mWoinfo.GetAteScriptsStr(woId);
        }
        //[WebMethod]
        //public string GetWoState(string woId)
        //{
        //    return mWoinfo.GetWoState(woId);
        //}
        [WebMethod]
        public string ChkSerialNumberRule_New(string woId, string snstart, string snend)
        {
            return mWoinfo.ChkSerialNumberRule_New(woId, snstart, snend);
        }
        [WebMethod]
        public string Calculation_MacList(string wo, string sntype, int Flag)
        {
            return mPro.Calculation_MacList(wo, sntype, Flag);
        }
        [WebMethod]
        public byte[] GetAllCraftInfo(string woId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetWOCraftInfo_ds(woId,"0"));
        }
         [WebMethod]
        public byte[] GetCraftInfoByRouteId(string RouteId, string Flag)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetCraftInfoByRouteId(RouteId, Flag));
        }
        [WebMethod]
        public byte[] GetWoSnListInfo(string woId,string sntype)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetWoSnListInfo(woId, sntype));
        }
        //[WebMethod]
        //public List<string> GetWoListByWoType()
        //{
        //    return mWoinfo.GetWoListByWoType();
        //}
        [WebMethod] //需判定是否使用此方法
        public string InsertWoSnRule(string  lswosnrule)
        {
            _FileHelper.Insert_Exception_Log("InsertWoSnRule->" + lswosnrule);
            return mWoinfo.InsertWoSnRule(lswosnrule);
        }
        [WebMethod]
        public byte[] GetTargetQtyAndNotInputQty(List<string> WoId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWoinfo.GetTargetQtyAndNotInputQty(WoId));
        }
        [WebMethod(Description = "更新工单信息 0减少投板数")]
        public string UpdateWoInfo(string woId)
        {
            return mWoinfo.UpdateWoInfo(woId);
        }
        [WebMethod]
        public bool CheckEsnRule(string wo, string esnno)
        {
            return mWoinfo.CheckEsnRule(wo, esnno);
        }
        [WebMethod]
        public string GetCraftInfoBywoid(string woid)
        {
            return mWoinfo.GetCraftInfoBywoid(woid);
        }

    }
}
