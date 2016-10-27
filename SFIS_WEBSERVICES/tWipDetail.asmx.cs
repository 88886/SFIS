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
    /// tWipDetail 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tWipDetail : System.Web.Services.WebService
    {
        BLL.tWipDetail mWipdetail = new BLL.tWipDetail();
        MapListConverter mlc = new MapListConverter();

        [WebMethod]
        public  byte[] GetWipDetail(string Serial)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWipdetail.GetWipDetail(Serial));
        }
        //[WebMethod]
        //public byte[] GetHistoyrWipDetail(string Serial)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mWipdetail.GetHistoyrWipDetail(Serial));
        //}

        
    }
}
