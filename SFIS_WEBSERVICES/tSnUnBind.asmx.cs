using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tSnUnBind 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tSnUnBind : System.Web.Services.WebService
    {
        BLL.t_Sn_UnBind SnUnBind = new BLL.t_Sn_UnBind();
        MapListConverter mlc = new MapListConverter();

        [WebMethod]
        public string Insert_Sn_UnBind(string ListJson)
        {
            IList<IDictionary<string, object>> ListMst = MapListConverter.JsonToListDictionary(ListJson);
            return SnUnBind.Insert_Sn_UnBind(ListMst);
        }
    }
}
