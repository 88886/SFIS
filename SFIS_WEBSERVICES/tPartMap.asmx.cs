using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tPartMap 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tPartMap : System.Web.Services.WebService
    {
        BLL.tPartMap mPartmap = new BLL.tPartMap();
        MapListConverter mlc = new MapListConverter();

        //[WebMethod]
        //public byte[] QueryPartMap(Entity.tPartMap PartMap)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartmap.QueryPartMap(PartMap));
        //}
     
        //[WebMethod]
        //public byte[] AllPartMapData()
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartmap.AllPartMapData());
        //}
        //[WebMethod]
        //public void InsertPartMap(Entity.tPartMap PartMap)
        //{
        //    mPartmap.InsertPartMap(PartMap);
        //}
        //[WebMethod]
        //public  void UpdatePartMap(Entity.tPartMap PartMap)
        //{
        //    mPartmap.UpdatePartMap(PartMap);
        //}
        [WebMethod]
        public byte[] QueryPartMaps(string kpnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(mPartmap.QueryPartMaps(kpnumber));
        }
        //[WebMethod]
        //public byte[] QueryPartMaps_CE(string kpnumber)
        //{
        //    return mlc.GetDataSetZipBytes(mPartmap.QueryPartMaps(kpnumber));
        //}
        //[WebMethod]
        //public  byte[] GetPartMapsBy(string IdOrCodeOrkp)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartmap.GetPartMapsBy(IdOrCodeOrkp));
        //}
        //[WebMethod]
        //public byte[] AllPartMapsData_s()
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartmap.AllPartMapsData_s());
        //}
        //[WebMethod]
        //public void InsertPartMaps_s(Entity.tPartMap PartMap)
        //{
        //    mPartmap.InsertPartMaps_s(PartMap);
        //}
        //[WebMethod]
        //public void UpdatePartMaps_s(Entity.tPartMap PartMap)
        //{
        //    mPartmap.UpdatePartMaps_s(PartMap);
        //}

       

    }
}
