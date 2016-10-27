using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// t_Check_Timeout 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class t_Check_Timeout : System.Web.Services.WebService
    {
        BLL.t_Check_Timeout tct = new BLL.t_Check_Timeout();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public byte[] Get_t_Check_Timeout(string CHECK_NO)
        {
            return mlc.GetDataSetSurrogateZipBytes(tct.Get_t_Check_Timeout(CHECK_NO));
        }
        [WebMethod]
        public string Update_Check_Timeout(string dicstring)
        {
            return tct.Update_Check_Timeout(dicstring);
        }
        [WebMethod]
        public string Insert_Check_Timeout(string dicstring)
        {
            return tct.Insert_Check_Timeout(dicstring);
        }
        [WebMethod]
        public string Delete_Check_Timeout(string CHECK_NO)
        {
            return tct.Delete_Check_Timeout(CHECK_NO);
        }
    }
}
