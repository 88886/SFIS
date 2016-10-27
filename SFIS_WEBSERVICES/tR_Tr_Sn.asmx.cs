using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Xml.Serialization;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tR_Tr_Sn 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tR_Tr_Sn : System.Web.Services.WebService
    {
        BLL.R_Tr_Sn tr_sn = new BLL.R_Tr_Sn();
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();
        MapListConverter mlc = new MapListConverter();
        WMS_Material.IMTR_Business _wmsMaterial = new WMS_Material.MTR_BusinessClient();
        WMS_SapConnector.ISapConnector _wmsSapConnector = new WMS_SapConnector.SapConnectorClient();
        BLL.FileHelper _FileHelper = new BLL.FileHelper();
   

        [WebMethod]
        public byte[] Sel_Tr_Sn_Info(string T_sn, out string Status)
        {
            return mlc.GetDataSetSurrogateZipBytes(tr_sn.Sel_Tr_Sn_Info(T_sn, out  Status));
        }
        [WebMethod(MessageName = "Update_TR_SN")]
        public string Update_TR_SN(string R_Trsn, string R_WOID, string R_USERID, string R_STATUS, string R_RMAK1, string R_RMAK2)
        {
            return mPro.UPDATE_TR_SN(R_Trsn, R_WOID, R_USERID, R_STATUS, R_RMAK1, R_RMAK2);
          
        }
        [WebMethod(MessageName = "UPDATE_TR_SN_Json")]
        public string UPDATE_TR_SN(string Json)
        {
            return mPro.UPDATE_TR_SN(Json);
        }

        //[WebMethod]
        //public string Update_Tr_Sn_QTY(string dicstring)
        //{
        //    return tr_sn.Update_Tr_Sn_QTY(dicstring);
        //}
        [WebMethod]
        public byte[] Sel_woId_Trsn_List(string woId, string Flag)
        {
            return mlc.GetDataSetSurrogateZipBytes(tr_sn.Sel_woId_Trsn_List(woId, Flag));
        }
        [WebMethod]
        public byte[] Sel_woId_Trsn_List_WinCe(string woId, string Flag)
        {
            return mlc.GetDataSetZipBytes(tr_sn.Sel_woId_Trsn_List(woId, Flag));
        }
        [WebMethod]
        public string GetSeqTrSnInfo()
        {
            return tr_sn.GetSeqTrSnInfo();
        }
        [WebMethod]
        public string insert_into_R_tr_sn(string dicstring)
        {
            return tr_sn.insert_into_R_tr_sn(MapListConverter.JsonToDictionary(dicstring));
        }
        [WebMethod]
        public string insert_into_R_tr_sn_detail(string dicstring)
        { 
            return tr_sn.insert_into_R_tr_sn_detail( MapListConverter.JsonToDictionary( dicstring));
        }
        [WebMethod]
        public string Del_TR_SN(string R_Trsn)
        {
            return tr_sn.Del_TR_SN(R_Trsn);
        }
        [WebMethod]
        public byte[] Get_Tr_Sn_Detail(string Tr_Sn)
        {
            return mlc.GetDataSetSurrogateZipBytes(tr_sn.Get_Tr_Sn_Detail(Tr_Sn));
        }
      [WebMethod]
        public byte[] WMS_get_R_Sap_Back_Shipping(string JsonStr)
      {
          _FileHelper.Insert_DB_Log("WMS_get_R_Sap_Back_Shipping--"+JsonStr);
          IDictionary<string, object> dic = MapListConverter.JsonToDictionary(JsonStr);
          return _wmsMaterial.get_R_Sap_Back_Shipping(dic["WOID"].ToString(),dic["DEBCRED"].ToString());
      }
      [WebMethod]
      public byte[] WMS_RFC_ZMM_GOODSMVT_CREATE_list(string ListJson)
      {
          _FileHelper.Insert_DB_Log("WMS_RFC_ZMM_GOODSMVT_CREATE_list--"+ListJson);
          return _wmsSapConnector.RFC_ZMM_GOODSMVT_CREATE_JSON(ListJson, "NA");
      }
      [WebMethod]
      public string WMS_Insert_R_SAP_BACK_SHIPPING(string ListJson)
      {
          _FileHelper.Insert_DB_Log("WMS_Insert_R_SAP_BACK_SHIPPING--" + ListJson);
          return _wmsMaterial.Insert_R_SAP_BACK_SHIPPING_JSON(ListJson);

      }
         [WebMethod]
      public string WMS_Update_R_Sap_Back_Shipping(string ListJson, string Transaction)
      {
          _FileHelper.Insert_DB_Log("WMS_Update_R_Sap_Back_Shipping--"+ListJson + "-" + Transaction);
          return _wmsMaterial.Update_R_Sap_Back_Shipping_JSON(ListJson, Transaction);
      }
         
        
    }
}
