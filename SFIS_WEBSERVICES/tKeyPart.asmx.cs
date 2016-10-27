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
    /// tKeyPart 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tKeyPart : System.Web.Services.WebService
    {
        BLL.tKeyPart mKeypart = new BLL.tKeyPart();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public  byte[] GetKeyParts()
        {
            return mlc.GetDataSetSurrogateZipBytes(mKeypart.GetKeyParts());
        }
        [WebMethod]
        public  void InsertKeyParts(string dickpt)
        {
            mKeypart.InsertKeyParts(dickpt);
        }

        [WebMethod]
        public void UpdateKeyParts(string dickpt)
        {
            mKeypart.UpdateKeyParts(dickpt);
        }

        [WebMethod]
        public  void DeleteKeyParts(string kpnumber)
        {
            mKeypart.DeleteKeyParts(kpnumber);
        }
        [WebMethod]
        public byte[] CheckDupPartsNumber(string KpNo, string PartNo)
        {
            return mlc.GetDataSetSurrogateZipBytes(mKeypart.CheckDupPartsNumber(KpNo, PartNo));
        }
        [WebMethod]
        public  int GetGetKeyPartsCount(string Kpnumber)
        {
            return mKeypart.GetGetKeyPartsCount(Kpnumber);
        }

         

    }
}
