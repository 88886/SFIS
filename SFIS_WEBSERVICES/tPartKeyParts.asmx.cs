using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace TestWeserver
{
    /// <summary>
    /// tPartKeyParts 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tPartKeyParts : System.Web.Services.WebService
    {
        BLL.tPartKeyParts mPartkeyparts = new BLL.tPartKeyParts();

        [WebMethod]
        public void InsertPartKeyParts(string dicstring)
        {
            mPartkeyparts.InsertPartKeyParts(dicstring);
        }

        [WebMethod]
        public void DeletePartKeyParts(string dicstring)
        {
            mPartkeyparts.DeletePartKeyParts(dicstring);
        }
    }
}
