using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tSFIS_Interface 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tSFIS_Interface : System.Web.Services.WebService
    {
        BLL.tUserInfo mUserInfo = new BLL.tUserInfo();
        BLL.ProPublicStoredproc ProPubStor = new BLL.ProPublicStoredproc();
        BLL.tWoInfo woinfo = new BLL.tWoInfo();
        MapListConverter mlc = new MapListConverter();

        [WebMethod(Description = "返回string类型")]
        public string String_INTERFACE(string Interface, string Json)
        {
            string _StrErr = string.Empty;
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(Json);
            switch (Interface)
            {
                case "CHECK_SET_LINE_EMPLOYEE":
                    _StrErr = mUserInfo.CHECK_SET_LINE_EMPLOYEE(dic);
                    break;
                case "GET_WO_SNRULE":
                   _StrErr= ProPubStor.Get_WO_SnRule(dic["WOID"].ToString()).GetXml();
                    break;
                default:
                    _StrErr = "Interface Error";
                    break;
            }
            return _StrErr;
        }
       [WebMethod(Description = "返回Byte类型")]
        public byte[] Byte_INTERFACE(string Interface, string Json)
        {
            Byte[] _StrErr = null;
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(Json);
            switch (Interface)
            {
                case "GetUserInfo":
                    string UserId = null;
                    string username = null;
                    string PWD = null;
                    if (dic.ContainsKey("USERID"))
                        UserId = dic["USERID"].ToString();
                    if (dic.ContainsKey("USERNAME"))
                        UserId = dic["USERNAME"].ToString();
                    if (dic.ContainsKey("PWD"))
                        UserId = dic["PWD"].ToString();
                    _StrErr =  mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetUserInfo(UserId,username,PWD));
                    break; 
                case "GetWoInfo":
                       _StrErr =  mlc.GetDataSetSurrogateZipBytes( woinfo.GetWoInfo(dic["WOID"].ToString(), dic["PARTNUMBER"].ToString(),null));
                    break;
                default:
                    _StrErr =null;
                    break;
            }
            return _StrErr;
        }
    }
}
