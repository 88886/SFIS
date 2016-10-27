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
    /// tTargetPlan 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tTargetPlan : System.Web.Services.WebService
    {
        BLL.tTargetPlan mTargetplan = new BLL.tTargetPlan();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public byte[] GetTargetPlanAll()
        {
            return mlc.GetDataSetSurrogateZipBytes(mTargetplan.GetTargetPlanAll());
        }
        [WebMethod]
        public  void UpdateTargetPlan(string dicsPlan)
        {
            mTargetplan.UpdateTargetPlan(dicsPlan);
        }
        [WebMethod]
        public void InsertTargetPlan(string dicPlan)
        {
            mTargetplan.InsertTargetPlan(dicPlan);
        }
        [WebMethod]
        public void DeleteTargetPlan(string Idx)
        {
            mTargetplan.DeleteTargetPlan(Idx);
        }
        //[WebMethod]
        //public byte[] GetTargetPlan(string Line, string Flag,string MyGroup, out string Err)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mTargetplan.GetTargetPlan(Line, Flag, MyGroup,out Err));
        //}
        [WebMethod]
        public byte[] GetTargetPlan1(String liness, String getDate, String ctype)
        {
            return mlc.GetDataSetSurrogateZipBytes(mTargetplan.GetTargetPlan1(liness, getDate, ctype));
        }

        [WebMethod]
        public byte[] GetTargetPlan2(String liness, String getDate, String ctype)
        {
            return mlc.GetDataSetSurrogateZipBytes(mTargetplan.GetTargetPlan2(liness, getDate, ctype));
        }
        [WebMethod]
        public byte[] GetTargetPlan3(String liness, String getDate, String ctype)
        {
            return mlc.GetDataSetSurrogateZipBytes(mTargetplan.GetTargetPlan3(liness, getDate, ctype));
        }

 
    }
}
