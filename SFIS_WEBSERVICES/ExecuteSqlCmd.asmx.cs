using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Data;

namespace TestWeserver
{
    /// <summary>
    /// ExecuteSqlCmd 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ExecuteSqlCmd : System.Web.Services.WebService
    {
        BLL.AllFuntion afun = new BLL.AllFuntion();
        [WebMethod]
        public string GetSeqBasics()
        {
            return afun.GetSeqBasics();
        }
        [WebMethod]
        public string GetSeq()
        {
            return afun.GetSqlSequence();
        }
       
    }
}
