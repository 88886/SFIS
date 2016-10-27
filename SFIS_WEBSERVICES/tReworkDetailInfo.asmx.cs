using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tReworkDetailInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tReworkDetailInfo : System.Web.Services.WebService
    {
        BLL.tReworkDetailInfo ReworkDetail = new BLL.tReworkDetailInfo();
        MapListConverter mlc = new MapListConverter();

        [WebMethod]
        public string GetReworkNo(string UserId)
        {
            return ReworkDetail.GetReworkNo(UserId);
        }
        //[WebMethod]
        //public string UpdateDataForRework(List<string> ClearWip, List<string> ESN, string LocGroup, string WipGroup, string ReworkNo,int Total)
        //{
        //    return ReworkDetail.UpdateDataForRework(ClearWip, ESN, LocGroup, WipGroup, ReworkNo,Total);
        //}
        //[WebMethod]
        //public string UpdateDataForReworkSigle(List<string> ClearWip, string Listsn, string LocGroup, string WipGroup, string ReworkNo)
        //{
        //    return ReworkDetail.UpdateDataForReworkSigle(ClearWip, Listsn, LocGroup, WipGroup, ReworkNo);
        //}

        [WebMethod]
        public string InsertReworkDetail(string dicstring)
        {
            return ReworkDetail.InsertReworkDetail(dicstring);
        }
        //[WebMethod]
        //public string UpdateDataForReworkWareHouse(List<string> ClearWip, List<string> ESN, string LocGroup, string WipGroup, string ReworkNo, int Total)
        //{
        //    return ReworkDetail.UpdateDataForReworkWareHouse(ClearWip, ESN, LocGroup, WipGroup, ReworkNo,Total);
        //}
        [WebMethod]
        public byte[] GetReworkInfo(string ReworkNo)
        {
            return mlc.GetDataSetSurrogateZipBytes(ReworkDetail.GetReworkInfo(ReworkNo));
        }
        //[WebMethod]
        //public string InsertReworkTempInfo(string woid, List<string>  lsesn)
        //{
        //    return ReworkDetail.InsertReworkTempInfo(woid, lsesn);
        //}
        [WebMethod]
        public string Release_Bound(string ESN, string INPUTGROUP, string ReworkNo)
        {
            return ReworkDetail.Release_Bound(ESN, INPUTGROUP, ReworkNo);
        }

        [WebMethod]
        public string Rework_SN(string Json, List<string> LsKeyParts)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(Json);
            return ReworkDetail.Rework_SN(mst, LsKeyParts);
        }
        [WebMethod]
        public string Scrap_SN(string Json, string Json_scrap)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(Json);
            IDictionary<string, object> mst_scrap = MapListConverter.JsonToDictionary(Json_scrap);
            return ReworkDetail.Scrap_SN(mst, mst_scrap);
        }
    }
}
