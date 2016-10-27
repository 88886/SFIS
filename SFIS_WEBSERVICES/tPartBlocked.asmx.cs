using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tPartBlocked 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tPartBlocked : System.Web.Services.WebService
    {
        BLL.tPartBlocked mPartblocked = new BLL.tPartBlocked();
        MapListConverter mlc = new MapListConverter();

        [WebMethod]
        public  bool Check_MaterialScrap(string sPN, string sVC, string sDC, string sLC)
        {
            return mPartblocked.Check_MaterialScrap(sPN, sVC, sDC, sLC);
        }

        [WebMethod]
        public  byte[] QueryPartBlocked()
        {
            return mlc.GetDataSetSurrogateZipBytes(mPartblocked.QueryPartBlocked());
        }
        [WebMethod]
        public void InsertPartBlocked(string dicstring)
        {
            mPartblocked.InsertPartBlocked(dicstring);
        }
        [WebMethod]
        public  void UpdatePartBlocked(string dicstring)
        {
            mPartblocked.UpdatePartBlocked(dicstring);
        }
        [WebMethod]
        public void DeletePartBlocked(string dicstring)
        {
            mPartblocked.DeletePartBlocked(dicstring);
        }
        [WebMethod]
        public byte[] QueryPartBlockedList(string PartNo)
        {
            return mlc.GetDataSetSurrogateZipBytes(mPartblocked.GettPartBlockedByPartNo(PartNo));
        }

        [WebMethod]
        public byte[] GettPartBlockedByPartNo(string partno)
        {
            return mlc.GetDataSetSurrogateZipBytes(mPartblocked.GettPartBlockedByPartNo(partno));
        }

         
    }
}
