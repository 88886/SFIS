using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// T_wo_Info_Erp 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class T_wo_Info_Erp : System.Web.Services.WebService
    {
        MapListConverter mlc = new MapListConverter();
        BLL.t_wo_Info_Erp erpwo = new BLL.t_wo_Info_Erp();

        [WebMethod]
        public byte[] Get_Erp_woinfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(erpwo.Get_Erp_woinfo());
        }
        [WebMethod]
        public string Update_Erp_woInfo(string dicstring)
        {
            return   erpwo.Update_Erp_woInfo(dicstring);
        }
        [WebMethod]
        public string Get_Erp_WoList()
        {
            return  MapListConverter.DictionaryToJson(erpwo.Get_Erp_WoList());
        }
        [WebMethod]
        public byte[] Get_WO_Info_Erp(string woId, string Fields)
        {
          return   mlc.GetDataSetSurrogateZipBytes(erpwo.Get_WO_Info_Erp(woId, Fields));
        }
    }
}
