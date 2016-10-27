using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tProductRealSummary 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tProductRealSummary : System.Web.Services.WebService
    {
        BLL.tProductRealSummary Prs = new BLL.tProductRealSummary();
        MapListConverter mlc = new MapListConverter();

        [WebMethod(MessageName = "Product_Real_Summary")]
        public byte[] Get_ProductRealSummary(string WORK_DATE, string Class)
        {
            return mlc.GetDataSetSurrogateZipBytes(Prs.Get_Product_Real_Summary(WORK_DATE, Class));
        }
    }
}
