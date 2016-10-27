using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tBomKeyPart 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tBomKeyPart : System.Web.Services.WebService
    {
        BLL.tBomKeyPart bomkeypart = new BLL.tBomKeyPart();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public List<string> GetBomNumerList()
        {
            return bomkeypart.GetBomNumberList();
        }
        [WebMethod]
        public  byte[] GetBomNoParts(string BomNumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(bomkeypart.GetBomNoParts(BomNumber));
        }
        [WebMethod]
        public void InsertBomNumber(string dicstring)
        {
            bomkeypart.InsertBomNumber(dicstring);
        }
        [WebMethod]
        public void DeleteBomNumber(string Bom)
        {
            bomkeypart.DeleteBomNumber(Bom);
        }


        
    }
}
