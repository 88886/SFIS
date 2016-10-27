using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace TestWeserver
{
    /// <summary>
    /// Check_Version 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Check_Version : System.Web.Services.WebService
    {
        BLL.Check_Vsersion ChkVer = new BLL.Check_Vsersion();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(MessageName = "检查版本,返回消息")]
        public bool CheckPrgVsersion(string Prg_Name, string Prg_Ver, string Ap_Desc, string Ap_Type, string Ap_Path,out string Msg)
        {          
           return ChkVer.CheckPrgVsersion(Prg_Name, Prg_Ver, Ap_Desc, Ap_Type, Ap_Path,out Msg);
        }
        [WebMethod(MessageName = "检查版本")]
        public bool CheckPrgVsersion(string Prg_Name, string Prg_Ver, string Ap_Desc, string Ap_Type, string Ap_Path)
        {
            string Msg = string.Empty;
            return ChkVer.CheckPrgVsersion(Prg_Name, Prg_Ver, Ap_Desc, Ap_Type, Ap_Path, out Msg);
        }
        [WebMethod(MessageName = "Get_PrgVersion")]
        public string CheckPrgVsersion(string Prg_Name, string Prg_Ver)
        {            
            return ChkVer.CheckPrgVsersion(Prg_Name, Prg_Ver);
        }
        [WebMethod(MessageName = "系统公告信息")]
        public string SystemMsg()
        {
            return ChkVer.SystemMsg();
        }
    }
}
