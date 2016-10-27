using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// T_DS_Out 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class T_DS_Out : System.Web.Services.WebService
    {
        BLL.T_DS_Out Ds = new BLL.T_DS_Out();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public byte[] Sel_Product_Info(string SN_Type, string SN)
        {
            return mlc.GetDataSetZipBytes(Ds.Sel_Product_Info(SN_Type, SN));
        }
        [WebMethod]
        public string GET_OutCode()
        {
            return Ds.GET_OutCode();
        }
        //[WebMethod]
        //public string Upd_SFC_Out(List<string> ESN, string Lot_Code, string User, string SNType)
        //{
        //    return Ds.Upd_SFC_Out(ESN, Lot_Code, User, SNType);
        //}
        //[WebMethod]
        //public byte[] sel_Erp_Item(string LOGI_NO)
        //{
        //    return mlc.GetDataSetZipBytes(Ds.sel_Erp_Item(LOGI_NO));
        //}
        //[WebMethod]
        //public string Update_SHOPEXInfo(string SFlag, string InUser, string log_No)//出货
        //{
        //    return Ds.Update_SHOPEXInfo(SFlag, InUser, log_No);
        //}
        [WebMethod]
        public byte[] Get_DSInfo(DateTime rec_Sta, DateTime rec_End, out string status)
        {
            return mlc.GetDataSetSurrogateZipBytes(Ds.Get_DSInfo(rec_Sta, rec_End, out  status));
        }
        //[WebMethod]
        //public string Save_Erp_Info(DataTable Dt_ERP, DataTable Dt_Erp_item)
        //{
        //    return Ds.Save_Erp_Info(Dt_ERP, Dt_Erp_item);
        //}

         

    }
}
