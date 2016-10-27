using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Data;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tB_SNRULE_WO 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tB_SNRULE_WO : System.Web.Services.WebService
    {
        BLL.B_SNRULE_WO BSW = new BLL.B_SNRULE_WO();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public byte[] GetB_SNRULE_WO(string woId)
        {
            return mlc.GetDataSetSurrogateZipBytes(BSW.GetB_SNRULE_WO(woId));
        }
        [WebMethod]
        public byte[] GetALLB_SnRule_WO()
        {
            return mlc.GetDataSetSurrogateZipBytes(BSW.GetALLB_SnRule_WO());
        }
        [WebMethod]
        public string InsertB_SNRULE_WO(string dicstring)
        {
            return BSW.InsertB_SNRULE_WO(dicstring);
        }
        [WebMethod]
        public string UpdateB_SNRULE_WO(string dicstring)
        {
            return BSW.UpdateB_SNRULE_WO(dicstring);
        }
        [WebMethod]
        public string UpdateCustPalletCartonNo_WO(string dicstring, string UpdateData, int Flag)
        {
            return BSW.UpdateCustPalletCartonNo_WO(dicstring, UpdateData, Flag);
        }

   
    }
}
