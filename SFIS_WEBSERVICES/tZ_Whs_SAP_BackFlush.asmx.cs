using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Data;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tZ_Whs_SAP_BackFlush 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tZ_Whs_SAP_BackFlush : System.Web.Services.WebService
    {

        WH_Business.IWarehouse_Business wbc = new WH_Business.Warehouse_BusinessClient();
        BLL.t_wo_Info_Erp ErpWo = new BLL.t_wo_Info_Erp();
        [WebMethod(MessageName = "WMS_InsertSAPBackFlush")]
        public string Insert_SAP_BackFlush(string STOCK_NO,string WOID,string PARTNUMBER,string PRODUCTNAME,int QTY)
        {
            DataTable dt = ErpWo.Get_WO_Info_Erp(WOID, "FACTORYID").Tables[0];
            if (dt.Rows.Count==0)
                return  string.Format("WOID[{0}] Not Found",WOID);
            string Plant = string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? "2100" : dt.Rows[0][0].ToString();
            return wbc.Insert_SAP_BackFlush(STOCK_NO, WOID, PARTNUMBER, PRODUCTNAME, QTY, Plant);

        }


      /*  BLL.tZ_WHS_SAP_BACKFLUSH WhsSapBack = new BLL.tZ_WHS_SAP_BACKFLUSH();
        MapListConverter mlc = new MapListConverter();
        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}
        [WebMethod]
        public string InsertZ_Whs_SAP_BackFlush(Entity.Z_WHS_SAP_BACKFLUSHTable zhsb)
        {
            return WhsSapBack.Insert_Z_Whs_SAP_BackFlush(zhsb);
        }
        [WebMethod]
        public string Update_Z_Whs_SAP_BackFlush_Flag(string sRowid, string sFlag)
        {
            return WhsSapBack.Update_Z_Whs_SAP_BackFlush_Flag(sRowid, sFlag);
        }
        [WebMethod(Description = "获取待上抛SAP的数据")]
        public byte[] GetUpload_whs_Sap_Info(string MOVE_TYPE)
        {
            return mlc.GetDataSetSurrogateZipBytes(WhsSapBack.GetUpload_whs_Sap_Info(MOVE_TYPE));
        }
        [WebMethod(Description = "获取各单号数据")]
        public byte[] GetUpload_whs_Sap_Info_No(string MOVE_TYPE, string DocNumber, string Colnum)
        {
            return mlc.GetDataSetSurrogateZipBytes(WhsSapBack.GetUpload_whs_Sap_Info_No(MOVE_TYPE, DocNumber, Colnum));
        }
        */
         
    }
}
