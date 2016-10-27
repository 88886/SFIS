using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using GenericUtil;


namespace TestWeserver
{
    /// <summary>
    /// tMaterialsReceive 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tMaterialsReceive : System.Web.Services.WebService
    {
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        BLL.tMaterialsReceive tmr = new BLL.tMaterialsReceive();

        //[WebMethod]
        //public void InsertMaterialsReceive(Entity.tMaterialsReceive mr)
        //{
        //    tmr.InsertMaterialsReceive(mr);
        //}

        //[WebMethod]
        //public byte[] GetMaterialsByPO(string materialno)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(tmr.GetMaterialsByPO(materialno));
        //}

        //[WebMethod]
        //public byte[] GetMaterialsInfo(string queryvalue, string flag)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(tmr.GetMaterialsInfo(queryvalue, flag));
        //}

        //[WebMethod]
        //public void UpdateMaterialStatus(string trsn,string flag)
        //{
        //    tmr.UpdateMaterialStatus(trsn,flag);
        //}

        //[WebMethod]
        //public byte[] GetMatterialByTrsn(string trsn)
        //{
        //    return  mlc.GetDataSetSurrogateZipBytes(tmr.GetMatterialByTrsn(trsn));
        //}
        //[WebMethod]
        //public byte[] GetMatterialByTrsn_WinCE(string trsn)
        //{
        //    return mlc.GetDataSetZipBytes(tmr.GetMatterialByTrsn(trsn));
        //}
        //[WebMethod]
        //public void UpdateMaterialStatusQty(string trsn, int qty)
        //{
        //    tmr.UpdateMaterialStatusQty(trsn, qty);
        //}
      
    }
}
