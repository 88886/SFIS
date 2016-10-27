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
    /// tPackParameters 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tPackParameters : System.Web.Services.WebService
    {
        BLL.tPackParameters mPackparameters = new BLL.tPackParameters();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public byte[] GetPackParameters()
        {
            return mlc.GetDataSetSurrogateZipBytes(mPackparameters.GetPackParameters(null));
        }
        [WebMethod]
        public  void InsertPackParameters(string dicstring)
        {
            mPackparameters.InsertPackParameters(dicstring);
        }
        [WebMethod]
        public void UpdatePackParameters(string dicstring)
        {
            mPackparameters.UpdatePackParameters(dicstring);
        }
        [WebMethod]
        public  void DeletePackParameters(string partnumber)
        {
            mPackparameters.DeletePackParameters(partnumber);
        }
        [WebMethod]
        public byte[] GetPackModelParameters(string Model, string ver)
        {
            return mlc.GetDataSetSurrogateZipBytes(mPackparameters.GetPackParameters(Model));
        }
        [WebMethod]
        public byte[] GetPackParametersByWoid(string woid)
        {
            return mlc.GetDataSetSurrogateZipBytes(mPackparameters.GetPackParametersByWoid(woid));
        }

       

    }
}
