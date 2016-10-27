using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.IO;

namespace TestWeserver
{
    /// <summary>
    /// AddProductInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class AddProductInfo : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public void  SAVESTR(string str)
        {
            BLL.CfgIniFile cf = new BLL.CfgIniFile();
            cf.test(str);
        }
    }
}
