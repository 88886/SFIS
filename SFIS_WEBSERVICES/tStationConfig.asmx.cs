using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tStationConfig 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tStationConfig : System.Web.Services.WebService
    {

        BLL.bStationConfig bsc = new BLL.bStationConfig();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public byte[] Get_StationConfig(string HostId, string Fields)
        {
            return mlc.GetDataSetSurrogateZipBytes( bsc.Get_StationConfig(HostId, Fields));
        }
        [WebMethod]
        public string Insert_StationConfig(string Json)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(Json);
            return bsc.Insert_StationConfig(mst);
        }
        [WebMethod]
        public string Update_StationConfig(string Json)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(Json);
            return bsc.Update_StationConfig(mst);
        }
    }
}
