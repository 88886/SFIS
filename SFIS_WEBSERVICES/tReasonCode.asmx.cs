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
    /// tReasonCode 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tReasonCode : System.Web.Services.WebService
    {
        BLL.tReasonCode mReasoncode = new BLL.tReasonCode();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public byte[] GetReasonCode()
        {
            return mlc.GetDataSetSurrogateZipBytes(mReasoncode.GetReasonCode());
        }
        [WebMethod]
        public void InserInToReasonCode(string dicstring)
        {
            mReasoncode.InserInToReasonCode(dicstring);
        }

        [WebMethod]
        public void UpdateReasonCode(string dicstring)
        {
            mReasoncode.UpdateReasonCode(dicstring);
        }

        [WebMethod]
        public void DeleteReasonCode(string ReasconCode)
        {
            mReasoncode.DeleteReasonCode(ReasconCode);
        }
        [WebMethod]
        public byte[] GetDutyInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mReasoncode.GetDutyInfo());
        }
        [WebMethod]
        public string InsertDuty(string Duty, string DutyDesc)
        {
            return mReasoncode.InsertDuty(Duty, DutyDesc);
        }
        [WebMethod]
        public string DeleteDuty(string Duty)
        {
            return mReasoncode.DeleteDuty(Duty);
        }
 
    }
}
