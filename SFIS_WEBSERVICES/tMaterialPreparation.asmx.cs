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
    /// tMaterialPreparation 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tMaterialPreparation : System.Web.Services.WebService
    {
        BLL.tMaterialPreparation_1 mMaterialpre1 = new BLL.tMaterialPreparation_1();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public byte[] GetMaterialPreparationKpAndStation(string dicstring)
        {
            return mlc.GetDataSetSurrogateZipBytes(mMaterialpre1.GetMaterialPreparationKpAndStation(dicstring));
        }
        [WebMethod]
        public byte[] GetMaterialPreparationStation(string dicstring)
        {
            return mlc.GetDataSetSurrogateZipBytes(mMaterialpre1.GetMaterialPreparationStation(dicstring));
        }
        [WebMethod]
        public  byte[] GetMaterialPreparation(string dicstring)
        {
            return mlc.GetDataSetSurrogateZipBytes(mMaterialpre1.GetMaterialPreparation(dicstring));
        }
        [WebMethod]
        public  byte[] GetKpdistinctMaterial(string dicstring)
        {
            return mlc.GetDataSetSurrogateZipBytes(mMaterialpre1.GetKpdistinctMaterial(dicstring));
        }
        //[WebMethod]
        //public byte[] CheckNewStationList(string sSEQ)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mMaterialpre1.CheckNewStationList(sSEQ));
        //}
        [WebMethod]
        public byte[] QueryAllStationList(string sMasterId, string sWO)
        {
            return mlc.GetDataSetSurrogateZipBytes(mMaterialpre1.QueryAllStationList(sMasterId, sWO));
        }
        //[WebMethod]
        //public  void UpdateMaterialPreparation(string dicstring)
        //{
        //    mMaterialpre1.UpdateMaterialPreparation(dicstring);
        //}
        [WebMethod]
        public byte[] PublicMaterialPreparationSelect(string woId)
        {
           return mlc.GetDataSetSurrogateZipBytes(mMaterialpre1.PublicMaterialPreparationSelect(woId));
        }
        [WebMethod]
        public string InsertMaterialPreparation_1(string sMasterid, string sWoid, string sUserid)
        {
          return  mMaterialpre1.InsertMaterialPreparation_1(sMasterid, sWoid, sUserid);
        }
        [WebMethod]
        public byte[] GetMaterialPreByStation_1(string mWoid, string mMasterId, string mStationno)
        {
            return mlc.GetDataSetSurrogateZipBytes(mMaterialpre1.GetMaterialPreByStation_1(mWoid, mMasterId, mStationno));
        }
        [WebMethod]
        public byte[] GetMaterialPreByKpnumber_1(string mWoid, string mKpnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(mMaterialpre1.GetMaterialPreByKpnumber_1(mWoid, mKpnumber));
        }
        [WebMethod]
        public byte[] Get_T_MATERIAL_PREPARATION(string dicstring)
        {
            return   mlc.GetDataSetSurrogateZipBytes(mMaterialpre1.Get_T_MATERIAL_PREPARATION(dicstring));
        }
        
    }
}
