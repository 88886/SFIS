using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.IO.Compression;
using System.Data;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// Web_FQC 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Web_FQC : System.Web.Services.WebService
    {
        BLL.BLL_FQC fqc = new BLL.BLL_FQC();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        //[WebMethod]
        //public byte[] Sel_FQCInfo_Date(DateTime Dt)
        //{
        //    return GetDataSetSurrogateZipBytes(fqc.Sel_FQCInfo_Date(Dt));
        //}
        [WebMethod]
        public void inser_FQCInfo(string QC_No, string Wo_id, string Pallet_No, string Carton_id, string Esn, bool Is_pass, string Error_Code, string User_id, string Tray_no, string qc_error_info)
        {
            fqc.inser_FQCInfo(QC_No, Wo_id, Pallet_No, Carton_id, Esn, Is_pass, Error_Code, User_id, Tray_no, qc_error_info);
        }
        [WebMethod]
        public byte[] Sel_QCNO_Date(DateTime Dt, string Line)
        {
            return mlc.GetDataSetSurrogateZipBytes(fqc.Sel_QCNO_Date(Dt, Line));
        }
        //[WebMethod]
        //public byte[] Sel_FQC_info_NumType(string Numtype, string boxid)
        //{
        //    return GetDataSetSurrogateZipBytes(fqc.Sel_FQC_info_NumType(Numtype, boxid));
        //}
        [WebMethod]
        public byte[] Sel_Fqc_report(string UserID, string Wo_ID, DateTime Dt_Sta, DateTime dt_End)
        {
            return mlc.GetDataSetSurrogateZipBytes(fqc.Sel_Fqc_report(UserID, Wo_ID, Dt_Sta, dt_End));
        }
        [WebMethod]
        public byte[] Sel_Fqc_ErrorInfo(string UserID, string Wo_ID, DateTime Dt_Sta, DateTime dt_End)
        {
            return mlc.GetDataSetSurrogateZipBytes(fqc.Sel_Fqc_ErrorInfo(UserID, Wo_ID, Dt_Sta, dt_End));
        }
        [WebMethod]
        public void inser_report(string qc_no, string qc_Wo_ID, int Pro_Count, int QC_count, int error_count, string UserID, DateTime In_Station_date, int R_CHECKDATE, string palletnumber, int checknumber)
        {
            fqc.inser_report(qc_no, qc_Wo_ID, Pro_Count, QC_count, error_count, UserID, In_Station_date, R_CHECKDATE, palletnumber, checknumber);
        }
        [WebMethod]
        public string Sel_QCNO()
        {
            return fqc.Sel_QCNO();

        }
        [WebMethod]
        public byte[] Sel_RcheckInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(fqc.Sel_RcheckInfo());
        }


      
    }
}
