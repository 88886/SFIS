using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tStationrecount 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tStationrecount : System.Web.Services.WebService
    {
        BLL.tStationrecount stnrec = new BLL.tStationrecount();
        MapListConverter mlc = new MapListConverter();

        [WebMethod]
        public byte[] GetStationRec(string Stime, string Etime)
        {
            return mlc.GetDataSetSurrogateZipBytes(stnrec.GetStationRec(Stime, Etime));
        }
        [WebMethod]
        public byte[] Get_YieldRate(string dicstring)
        {
            return mlc.GetDataSetSurrogateZipBytes(stnrec.Get_YieldRate(dicstring));
        }
        [WebMethod]
        public string get_station_qty(string woid, string station, string line)
        {
            return stnrec.get_station_qty(woid, station, line);
        }
       
    }
}
