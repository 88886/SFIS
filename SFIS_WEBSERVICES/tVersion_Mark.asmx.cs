using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;
using System.Collections.Generic;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tVersion_Mark 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tVersion_Mark : System.Web.Services.WebService
    {
        BLL.tVersion_Mark_info VersionMark = new BLL.tVersion_Mark_info();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public byte[] QueryVersionInfoByWo(string wo)
        {
            return mlc.GetDataSetSurrogateZipBytes(VersionMark.QueryVersionInfoByWo(wo));
        }
        [WebMethod]
        public string InsertVersionInfo(string wo, string lsVersionMark, string sId)
        {
            return VersionMark.InsertVersionInfo(wo, lsVersionMark, sId);
        }
        [WebMethod]
        public byte[] QueryMarkBitByPn(string pn)
        {
            return mlc.GetDataSetSurrogateZipBytes(VersionMark.QueryMarkBitByPn(pn));
        }
        [WebMethod]
        public string InsertMarkBitInfo(string pn, string lsVersionMark, string sId)
        {
            return VersionMark.InsertMarkBitInfo(pn, lsVersionMark, sId);
        }
    }
}
