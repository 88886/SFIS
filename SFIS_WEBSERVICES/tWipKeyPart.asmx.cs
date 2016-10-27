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
using System.Collections.Generic;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tWipKeyPart 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tWipKeyPart : System.Web.Services.WebService
    {
        BLL.tWipKeyPart mWipkeypart = new BLL.tWipKeyPart();
        BLL.tWipTracking mWipTracking = new BLL.tWipTracking();
        MapListConverter mlc = new MapListConverter();
  
        [WebMethod]
        public byte[] GetWipKeyParts(string SNTYPE,string SNVAL)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWipkeypart.GetWipKeyParts(SNTYPE, SNVAL));
        }
        [WebMethod]
        public byte[] GetWipKeyPart(string Serial)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWipTracking.GetEsnDataInfo("ESN",Serial));
        }
        [WebMethod]
        public byte[] GetEsnDataInfo(string Sntype, string Value)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWipTracking.GetEsnDataInfo(Sntype, Value));
        }
        [WebMethod]
        public byte[] ChkKeyParts(string serial, string sntype, string woid)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWipkeypart.ChkKeyParts(serial, sntype, woid));
        }

        [WebMethod]
        public string InsertKeyParts(string  diclsWipKeyParts)
        {
           IList<IDictionary<string,object>> LsDic=  MapListConverter.JsonToListDictionary(diclsWipKeyParts);

            string msg = string.Empty;
            try
            {
                foreach (Dictionary<string, object> item in LsDic)
                {
                    mWipTracking.InsertWipKeyPart(item);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
 
        }
        //[WebMethod]
        //public string GetEsnForKeyParts(string val)
        //{
        //    return mWipkeypart.GetEsnForKeyParts(val);
        //}
        //[WebMethod]
        //public byte[] H_GetWoAllSerial(string woId, int strHour)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mWipkeypart.H_GetWoAllSerial(woId, strHour));
        //}
        [WebMethod]
        public string CHECK_KPS_VALID_FOR_REPAIR(string ESN, string KPS, string SNTYPE, string WOID)
        {
            return mWipkeypart.CHECK_KPS_VALID_FOR_REPAIR(ESN, KPS, SNTYPE, WOID);
        }
        [WebMethod]
        public string Update_WIP_KEYPARTS(string ESN, string KPS, string OLDKPS, string SNTYPE)
        {
            return mWipkeypart.Update_WIP_KEYPARTS(ESN, KPS, OLDKPS, SNTYPE);
        }
        [WebMethod]
        public string Insert_WipKeyParts_Undo(string ESN, string SNTYPE, string SNVAL)
        {
            return mWipkeypart.Insert_WipKeyParts_Undo(ESN, SNTYPE, SNVAL);
        }
        [WebMethod]
        public string DELETE_WipKeyParts(string ESN, string SNTYPE, string SNVAL)
        {
            return mWipkeypart.DELETE_WipKeyParts(ESN, SNTYPE, SNVAL);
        }

      
    }
}
