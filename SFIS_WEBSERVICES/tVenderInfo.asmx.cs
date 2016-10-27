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
using GenericUtil;


namespace TestWeserver
{
    /// <summary>
    /// tVenderInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tVenderInfo : System.Web.Services.WebService
    {
        BLL.tVenderInfo mVenderinfo = new BLL.tVenderInfo();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public byte[] QueryVender(string vendnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(mVenderinfo.QueryVenderInfo(vendnumber));
        }
        //[WebMethod]
        //public  byte[] QueryVenderInfoByVenderId(string venderId)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mVenderinfo.QueryVenderInfoByVenderId(venderId));
        //}

        [WebMethod]
        public void InsertVenderInfo(string dicvenderinfo)
        {
            mVenderinfo.InsertVenderInfo(dicvenderinfo);
        }
        
    }
}
