using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Data;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tPrivliege 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tPrivliege : System.Web.Services.WebService
    {
       // BLL.Privliege Priv = new BLL.Privliege();
        MapListConverter mlc = new MapListConverter();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        //[WebMethod]
        //public byte[] DownLoadModule()
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(Priv.DownLoadModule());
        //}
        //[WebMethod]
        //public byte[] DownLoadPrivliege(string EmpNo)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(Priv.DownLoadPrivliege(EmpNo));
        //}
        //[WebMethod]
        //public string InsertIntoPrivliege(string EmpNo, List<Entity.tPrivliegeTable> Plt)
        //{
        //    return Priv.InsertIntoPrivliege(EmpNo,Plt);
        //}
         
    }
}
