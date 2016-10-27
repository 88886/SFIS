using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// RecodeSystemLog 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class RecodeSystemLog : System.Web.Services.WebService
    {
        BLL.RecodeSystemLog rsl = new BLL.RecodeSystemLog();
        BLL.FileHelper _FileHelper = new BLL.FileHelper();
       
        [WebMethod]
        public void InsertSystemLog(string dicstring, out string err)
        {
            rsl.InsertSystemLog(dicstring, out err);
        }
        [WebMethod]
        public void Insert_Info(string StrLog)
        {
            _FileHelper.Insert_DB_Log(StrLog);
        }
        [WebMethod]
        public void Insert_Trace(string StrLog)
        {
            _FileHelper.Insert_Trace(StrLog);
        }
        [WebMethod]
        public void Insert_Debug(string StrLog)
        {
            _FileHelper.Insert_Debug(StrLog);
        }
    }
}
