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
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tSmtIO 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tSmtIO : System.Web.Services.WebService
    {
        BLL.tSmtIO mSmtipo = new BLL.tSmtIO();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public byte[] GetSmtIO(string sSEQ, string sMO)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtipo.GetSmtIO(sSEQ, sMO));
        }
        [WebMethod]
        public void InserSmtIo(string dicsmtio)
        {
            mSmtipo.InserSmtIo(dicsmtio);
        }
        [WebMethod]
        public  void UpdateSmtIOStatus(string dicsit)
        {
            mSmtipo.UpdateSmtIOStatus(dicsit);
        }
        [WebMethod]
        public byte[] GetAllSmtIO(string sSEQ, string sMO)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtipo.GetAllSmtIO(sSEQ, sMO));
        }
        [WebMethod]
        public void DeleteSmtIo(string sSEQ, string sMO)
        {
            mSmtipo.DeleteSmtIo(sSEQ, sMO);
        }
 

    }
}
