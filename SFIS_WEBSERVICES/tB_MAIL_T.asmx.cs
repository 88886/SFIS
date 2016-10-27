using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tB_MAIL_T 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tB_MAIL_T : System.Web.Services.WebService
    {
        BLL.tB_MAIL_T Mail = new BLL.tB_MAIL_T();
        MapListConverter mlc = new MapListConverter();

        [WebMethod(MessageName = "GetMailAddress")]
        public byte[] Get_B_MAIL_T(string Mail_App)
        {
            return  mlc.GetDataSetSurrogateZipBytes( Mail.Get_B_MAIL_T(Mail_App));
        }
    }
}
