using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.IO.Compression;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tB_SnRule_PartNumber 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tB_SnRule_PartNumber : System.Web.Services.WebService
    {
        BLL.B_SNRULE_PARTNUMBER bsp = new BLL.B_SNRULE_PARTNUMBER();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public byte[] GetB_SNRULE_PARTNUMBER(string PartNo)
        {
            return mlc.GetDataSetSurrogateZipBytes(bsp.GetB_SNRULE_PARTNUMBER(PartNo));
        }
        [WebMethod]
        public byte[] GetALLB_SnRule_PartNumber()
        {
            return mlc.GetDataSetSurrogateZipBytes(bsp.GetALLB_SnRule_PartNumber());
        }
        [WebMethod]
        public string InsertB_SNRULE_PARTNUMBER(string dicstring)
        {
            return bsp.InsertB_SNRULE_PARTNUMBER(dicstring);
        }
        [WebMethod]
        public string UpdateB_SNRULE_PARTNUMBER(string dicstring, int flag)
        {
            return bsp.UpdateB_SNRULE_PARTNUMBER(dicstring, flag);
        }
        [WebMethod]
        public string UpdateCustPalletCartonNo(string DicString, string UpdateData, int Flag)
        {
            return bsp.UpdateCustPalletCartonNo(DicString, UpdateData, Flag);
        }



    }
}
