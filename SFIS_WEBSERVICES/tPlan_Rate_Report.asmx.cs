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
    /// tPlan_Rate_Report 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tPlan_Rate_Report : System.Web.Services.WebService
    {
        BLL.tPlan_Rate_Report planrate = new BLL.tPlan_Rate_Report();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public byte[] getPlan_Rate_Set_bydate(string workmonth, string workdate)
        {
            return mlc.GetDataSetSurrogateZipBytes(planrate.getPlan_Rate_Set_bydate(workmonth, workdate));
        }
        [WebMethod]
        public byte[] getPlan_Rate_Set_bypn(string workmonth, string workdate, string pn)
        {
            return mlc.GetDataSetSurrogateZipBytes(planrate.getPlan_Rate_Set_bypn(workmonth, workdate, pn));
        }
        [WebMethod]
        public string insert_Plan_Rate(string dicPlanRate)
        {
            return planrate.insert_Plan_Rate(dicPlanRate);
        }
        [WebMethod]
        public string delete_Plan_Rate(string dicPlanRate)
        {
            return planrate.delete_Plan_Rate(dicPlanRate);
        }
    }
}
