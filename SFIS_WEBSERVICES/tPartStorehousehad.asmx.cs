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
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tPartStorehousehad 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tPartStorehousehad : System.Web.Services.WebService
    {
        BLL.tPartStorehousehad mPartstorehousehad = new BLL.tPartStorehousehad();       
        MapListConverter mlc = new MapListConverter();
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();

        [WebMethod]
        public byte[] GetGangInfoByTrsn(string trsn)
        {
            return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetGangInfoByTrsn(trsn));
        }

        [WebMethod]
        public byte[] GetGangInfoInWare(string name, string value)
        {
            return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetGangInfoInWare(name, value));
        }

        [WebMethod]
        public byte[] GetGangInfoInStore()
        {
            return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetGangInfoInWare(null,null));
        }

        [WebMethod]
        public byte[] Getgangwang()
        {
            return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.Getgangwang());
        }
        [WebMethod]
        public byte[] GetMaxTrsn()
        {
            return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetMaxTrsn());
        }

        [WebMethod]
        public byte[] GetNotUsedLocId(string storehouseid)
        {
            return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetNotUsedLocId(storehouseid));
        }
        [WebMethod]
        public void InsertPartStorehousehad(string dicstring, out string err)
        {


            err = "";
            //mPro.PRO_INSERTSTOREHOUSEHADRECOUNT(dicstring, out err); //待完成 20150120
        }

        [WebMethod]
        public byte[] QueryKpnumber()
        {
            return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.QueryKpnumber());
        }

        [WebMethod]
        public byte[] QueryTrSn(string trsn)
        {
            return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.QueryTrsn(trsn));
        }

        [WebMethod]
        public void UpdateTrSnStatus(string Status, string userid, string trsn)
        {
            mPartstorehousehad.UpdateTrSnStatus(Status, userid, trsn);
        }

        [WebMethod]
        public void UpdateGangInfo(string dicstring)
        {
            mPartstorehousehad.UpdateGangInfo(dicstring);
        }

        //[WebMethod]
        //public void UpdateByTrsn(string trsn, string storehouseid, string locid)
        //{
        // mPartstorehousehad.UpdateByTrsn(trsn, storehouseid, locid);
        //}
        //[WebMethod]
        //public void UpdateGangWangUseCount(string trsn, int total)
        //{
        //    mPartstorehousehad.UpdateGangWangUseCount(trsn, total);
        //}

        //[WebMethod]
        //public byte[] QueryKpnumberMoreThanDays(string KpNumber)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.QueryKpnumberMoreThanDays(KpNumber));
        //}


        //[WebMethod]
        //public byte[] GetGangwangTotal(string kpnumber)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetGangwangTotal(kpnumber));
        //}

        //[WebMethod]
        //public string GetSeqTrSn()
        //{
        //    return mPartstorehousehad.GetSeqTrSnInfo();
        //}

     

       
        //[WebMethod]
        //public byte[] QueryTrSn_WinCE(string trsn)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.QueryTrsn(trsn));
        //}

        //[WebMethod]
        //public byte[] QueryMaterialInputOutQTY(string Flag, Entity.tPartStorehousehadRecount MaterIO)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.QueryMaterialInputOutQTY(Flag, MaterIO));
        //}
        //[WebMethod]
        //public byte[] QueryMaterialInputOutQTY_WinCE(string Flag, Entity.tPartStorehousehadRecount MaterIO)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.QueryMaterialInputOutQTY(Flag, MaterIO));
        //}

        //[WebMethod]
        //public byte[] QueryMaterialStocks(Entity.tPartStorehousehad Stocks)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.QueryMaterialStocks(Stocks));
        //}
        //[WebMethod]
        //public byte[] QueryMaterialStocks_WinCE(Entity.tPartStorehousehad Stocks)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.QueryMaterialStocks(Stocks));
        //}

        //[WebMethod]
        //public byte[] QueryTrsnDetail(string trsn)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.QueryTrsnDetail(trsn));
        //}
        //[WebMethod]
        //public byte[] QueryTrsnDetail_WinCE(string trsn)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.QueryTrsnDetail(trsn));
        //}

      

      
        //[WebMethod]
        //public byte[] QueryKpnumberMoreThanDays_WinCE(string KpNumber)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.QueryKpnumberMoreThanDays(KpNumber));
        //}

        //[WebMethod]
        //public byte[] QueryWoidByKpnumber(string kpnumber)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.QueryWoidByKpnumber(kpnumber));
        //}
        //[WebMethod]
        //public byte[] QueryWoidByKpnumber_WinCE(string kpnumber)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.QueryWoidByKpnumber(kpnumber));
        //}
        /// <summary>
        /// 获取料号所在的库存位置
        /// </summary>
        /// <param name="kpnumber"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        //[WebMethod]
        //public  byte[] GetKpnumberLocation(string kpnumber, int total)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetKpnumberLocation(kpnumber, total));
        //}
        //[WebMethod]
        //public byte[] GetKpnumberLocation_WinCE(string kpnumber, int total)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetKpnumberLocation(kpnumber, total));
        //}
       // #region 仓储原料报表
        //[WebMethod]
        //public byte[] GetAllMaterialStocks()
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetAllMaterialStocks());
        //}
        //[WebMethod]
        //public byte[] GetAllMaterialStocks_WinCE()
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetAllMaterialStocks());
        //}

        //[WebMethod]
        //public byte[] GetMaterialInfoByKpnumber(string kpnumber)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetMaterialInfoByKpnumber(kpnumber));
        //}
        //[WebMethod]
        //public byte[] GetMaterialInfoByKpnumber_WinCE(string kpnumber)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetMaterialInfoByKpnumber(kpnumber));
        //}
        //#endregion

       // #region 待入库
        //[WebMethod]
        //public  byte[] GetStoregabuffer()
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetStoregabuffer());
        //}
        //[WebMethod]
        //public byte[] GetStoregabuffer_WinCE()
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetStoregabuffer());
        //}

        //[WebMethod]
        //public  string UpdateKeyPartlocByTrsn(string storehouseId, string locId, string trsn,string userId)
        //{
        //    return mPartstorehousehad.UpdateKeyPartlocByTrsn(storehouseId, locId, trsn,userId);
        //}
        /// <summary>
        /// 根据提供的料号更新库位
        /// </summary>
        /// <param name="storehouseId"></param>
        /// <param name="locId"></param>
        /// <param name="kpnumber"></param>
        /// <returns></returns>
        //[WebMethod]
        //public  string UpdateKeyPartlocByKpnumber(string storehouseId,string locId,string kpnumber)
        //{
        //    return mPartstorehousehad.UpdateKeyPartlocByKpnumber(storehouseId, locId, kpnumber);
        //}

        /// <summary>
        /// 根据提供的厂商代码更新库位
        /// </summary>
        /// <param name="storehouseId"></param>
        /// <param name="locId"></param>
        /// <param name="vendercode"></param>
        /// <returns></returns>
        //[WebMethod]
        //public string UpdateKeyPartlocByVendercode(string storehouseId, string locId, string vendercode)
        //{
        //    return mPartstorehousehad.UpdateKeyPartlocByVendercode(storehouseId, locId, vendercode);
        //}
    //    #endregion


        //[WebMethod]
        //public  byte[] GetStoregabufferByKpnumber(string kpnumber)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetStoregabufferByKpnumber(kpnumber));
        //}
        //[WebMethod]
        //public byte[] GetStoregabufferByKpnumber_WinCE(string kpnumber)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetStoregabufferByKpnumber(kpnumber));
        //}


        /*   [WebMethod]
           public  byte[] GetStoregabufferByVendercode(string Vc)
           {
             return  mlc.GetDataSetSurrogateZipBytes( mPartstorehousehad.GetStoregabufferByVendercode(Vc));
           }
           [WebMethod]
           public byte[] GetStoregabufferByVendercode_WinCE(string Vc)
           {
               return mlc.GetDataSetZipBytes(mPartstorehousehad.GetStoregabufferByVendercode(Vc));
           }

           [WebMethod]
           public  byte[] GetStoregabufferByDatecode(string dc)
           {
               return mlc.GetDataSetSurrogateZipBytes( mPartstorehousehad.GetStoregabufferByDatecode(dc));
           }
           [WebMethod]
           public byte[] GetStoregabufferByDatecode_WinCE(string dc)
           {
               return mlc.GetDataSetZipBytes(mPartstorehousehad.GetStoregabufferByDatecode(dc));
           }

           [WebMethod]
           public byte[] GetStoregabufferByUserId(string userId)
           {
               return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetStoregabufferByUserId(userId));
           }
           [WebMethod]
           public byte[] GetStoregabufferByUserId_WinCE(string userId)
           {
               return mlc.GetDataSetZipBytes(mPartstorehousehad.GetStoregabufferByUserId(userId));
           }
           [WebMethod]
           public byte[] QueryWoid(string woid)
           {
               return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.QueryWoid(woid));
           }
           [WebMethod]
           public byte[] QueryWoid_WinCE(string woid)
           {
               return mlc.GetDataSetZipBytes(mPartstorehousehad.QueryWoid(woid));
           }

           [WebMethod]
           public byte[] GetKpnumberByWoid(string woid, string process)
           {
               return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetKpnumberByWoid(woid, process));
           }
           [WebMethod]
           public byte[] GetKpnumberByWoid_WinCE(string woid, string process)
           {
               return mlc.GetDataSetZipBytes(mPartstorehousehad.GetKpnumberByWoid(woid, process));
           }*/
      
        //[WebMethod]
        //public byte[] Getgangwang_WinCE()
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.Getgangwang());
        //}

       
        //[WebMethod]
        //public byte[] GetGangInfoByTrsn_WinCE(string trsn)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetGangInfoByTrsn(trsn));
        //}

        //[WebMethod]
        //public byte[] GetGangInfoByVC(string vendercode)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetGangInfoByVC(vendercode));
        //}
        //[WebMethod]
        //public byte[] GetGangInfoByVC_WinCE(string vendercode)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetGangInfoByVC(vendercode));
        //}

        //[WebMethod]
        //public byte[] GetGangInfoByKpnumber(string kpnumber)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetGangInfoByKpnumber(kpnumber));
        //}
        //[WebMethod]
        //public byte[] GetGangInfoByKpnumber_WinCE(string kpnumber)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetGangInfoByKpnumber(kpnumber));
        //}

        
        //[WebMethod]
        //public void UpdateGangInfo_WinCE(Entity.tPartStorehousehad partstoremodel, string trsn)
        //{
        //    mPartstorehousehad.UpdateGangInfo(partstoremodel, trsn);
        //}
        [WebMethod]
        public string DeleteGangInfoByTrsn(string trsn)
        {
           return mPartstorehousehad.DeleteGangInfoByTrsn(trsn);
        }
        //[WebMethod]
        //public void DeleteGangInfoByTrsn_WinCE(string trsn)
        //{
        //    mPartstorehousehad.DeleteGangInfoByTrsn(trsn);
        //}

        //[WebMethod]
        //public byte[] GetGangInfoByCondition(string name, string value)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetGangInfoByCondition(name, value));
        //}
        //[WebMethod]
        //public byte[] GetGangInfoByCondition_WinCE(string name, string value)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetGangInfoByCondition(name, value));
        //}

        //[WebMethod]
        //public byte[] GetAllGangInfo()
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetAllGangInfo());
        //}
        //[WebMethod]
        //public byte[] GetAllGangInfo_WinCE()
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetAllGangInfo());
        //}
      
        //[WebMethod]
        //public byte[] GetMaxTrsn_WinCE()
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetMaxTrsn());
        //}
      
        //[WebMethod]
        //public byte[] GetNotUsedLocId_WinCE(string storehouseid)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetNotUsedLocId(storehouseid));
        //}
      
        //[WebMethod]
        //public byte[] GetGangwangTotal_WinCE(string kpnumber)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetGangwangTotal(kpnumber));
        //}

        [WebMethod]
        public byte[] GetGangwangQuery(string kpnumber, string vendercode, string trsn)
        {
            return null;// mlc.GetDataSetSurrogateZipBytes(mPro.GetGangWangQuery(kpnumber, vendercode, trsn));
            //  return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetGangwangQuery(kpnumber,vendercode,trsn));
        }
        [WebMethod]
        public byte[] GetGangwangQuery_WinCE(string kpnumber, string vendercode, string trsn)
        {
            return null;///mlc.GetDataSetZipBytes(mPro.GetGangWangQuery(kpnumber, vendercode, trsn));
            //  return mlc.GetDataSetZipBytes(mPartstorehousehad.GetGangwangQuery(kpnumber, vendercode, trsn));
        }

      
        //[WebMethod]
        //public byte[] QueryKpnumber_WinCE()
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.QueryKpnumber());
        //}
      
        //[WebMethod]
        //public byte[] UpdateByTrsn_WinCE(string trsn, string storehouseid, string locid)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.UpdateByTrsn(trsn, storehouseid, locid));
        //}
        
       
        //[WebMethod]
        //public byte[] GetGangInfoInStore_WinCE()
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetGangInfoInWare());
        //}
        //[WebMethod]
        //public byte[] GetGangInfoInWare(string name, string value)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetGangInfoInWare(name, value));
        //}
        //[WebMethod]
        //public byte[] GetGangInfoInWare_WinCE(string name, string value)
        //{
        //    return mlc.GetDataSetZipBytes(mPartstorehousehad.GetGangInfoInWare(name, value));
        //}
        //[WebMethod]
        //public void MaterialPrint(Entity.tPartStorehousehad sd, string kpdesc, string partgroup, string vendername,string PO)
        //{

        //    mPartstorehousehad.MaterialPrint(sd,kpdesc,partgroup,vendername,PO);
        //}
        //[WebMethod]
        //public byte[] GetStoregabufferByTrsn(string trsn)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetStoregabufferByTrsn(trsn));
        //}
        //[WebMethod]
        //public void UpdateTrSnWoid(string woid, string TrSn)
        //{
        //    mPartstorehousehad.UpdateTrSnWoid(woid, TrSn);
        //}
        //[WebMethod]
        //public byte[] GetMaterialInfoByWoid(string woid)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.GetMaterialInfoByWoid(woid));
        //}

        //[WebMethod]
        //public byte[] checkLocIDbySHID(string storehouseid, string locid)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mPartstorehousehad.checkLocIDbySHID(storehouseid, locid));
        //}




    }

}