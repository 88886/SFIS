using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tSmtWoMerge 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tSmtWoMerge : System.Web.Services.WebService
    {
        BLL.tSmtWoMerge tswm = new BLL.tSmtWoMerge();
        MapListConverter mlc = new MapListConverter();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(MessageName = "GetSmtWOMergeFirst")]
          public byte[] Get_Smt_WO_Merge(string Json,string Field)
        {
            return mlc.GetDataSetSurrogateZipBytes(tswm.Get_Smt_WO_Merge(Json, Field));
        }
        [WebMethod]
        public string Insert_Smt_WO_Merge(string Json)
        {
            return tswm.Insert_Smt_WO_Merge(Json);
        }
         [WebMethod]
        public string Get_Merge_No()
        {
            return tswm.Get_Merge_No();
        }
          [WebMethod(MessageName = "GetSmtWOMergeSecond",Description="获取合并工单的编号")]
         public byte[] Get_Smt_WO_Merge(string woId)
         {
             return mlc.GetDataSetSurrogateZipBytes(tswm.Get_Smt_WO_Merge(woId));
         }
    }
}
