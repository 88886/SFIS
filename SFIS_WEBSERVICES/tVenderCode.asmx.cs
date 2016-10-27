using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Data;
using System.Xml.Serialization;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tVenderCode 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tVenderCode : System.Web.Services.WebService
    {
        BLL.tVendCode mVendcode = new BLL.tVendCode();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public byte[] GetVendCodeInfo(string vendnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(mVendcode.GetVendCodeInfo(vendnumber));
        }


       
    }
}
