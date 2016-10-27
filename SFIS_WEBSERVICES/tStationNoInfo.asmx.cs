using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Data;
using System.Xml.Serialization;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tStationNoInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tStationNoInfo : System.Web.Services.WebService
    {
        BLL.tStationNoInfo mStationNoinfo = new BLL.tStationNoInfo();
        MapListConverter mlc = new MapListConverter();
        /// <summary>
        /// 获取所有料站信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetAllStationNoInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mStationNoinfo.GetAllStationNoInfo());
            //string sql = "select stationno,lineId,machineId,des,stationspec from stationnoInfo";
            //return BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sql);
        }

        /// <summary>
        /// 获取线体的料站信息
        /// </summary>
        /// <param name="lineId">线体编号</param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetStationNoInfoByLineId(string lineId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mStationNoinfo.GetStationNoInfoByLineId(lineId));
            //string sql = string.Format("select stationno,lineId,machineId,des,stationspec from stationnoInfo where lineId='{0}'", lineId);
            //return BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sql);
        }

        /// <summary>
        /// 获取机器的料站信息
        /// </summary>
        /// <param name="machineId"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetStationNoInfoByMachineId(string machineId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mStationNoinfo.GetStationNoInfoByMachineId(machineId));
            //string sql = string.Format("select stationno,lineId,machineId,des,stationspec from stationnoInfo where machineId='{0}'", machineId);
            //return BLL.BllMsSqllib.Instance.ExecuteQuerySQL(sql);
        }

        /// <summary>
        /// 获取指定料站的信息
        /// </summary>
        /// <param name="stationno">料站编号</param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetStationNoInfoBystationno(string stationno)
        {
            return mlc.GetDataSetSurrogateZipBytes(mStationNoinfo.GetStationNoInfoBystationno(stationno));
        }

        /// <summary>
        /// 根据站位编号修改信息
        /// </summary>
        /// <param name="stationno"></param>
        /// <param name="sni"></param>
        [WebMethod]
        public void EditStationNoInfo(string stationno, Entity.tStationNoInfo sni)
        {
            mStationNoinfo.EditStationNoInfo(stationno, sni);
        }

        /// <summary>
        /// 新增料站信息
        /// </summary>
        /// <param name="sni"></param>
        [WebMethod]
        public void InsertStationNoInfo(Entity.tStationNoInfo sni)
        {
            mStationNoinfo.InsertStationNoInfo(sni);
        }

       

    }
}
