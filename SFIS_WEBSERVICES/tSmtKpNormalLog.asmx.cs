using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Data;
using System.Xml.Serialization;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tSmtKpNormalLog 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tSmtKpNormalLog : System.Web.Services.WebService
    {
        BLL.tSmtKpNormalLog mSmtkpnormallog = new BLL.tSmtKpNormalLog();
        MapListConverter mlc = new MapListConverter();
        

        //[WebMethod]
        //public  bool GetNormalLogStatus(string masterId, string woId, string pcbside, string machineId,string stationId)
        //{
        //    return mSmtkpnormallog.GetNormalLogStatus(masterId, woId, pcbside, machineId, stationId);
        //}

        //[WebMethod]
        //public void InsertSmtKpNormalLogs(List<Entity.tSmtKpNormalLog> lskpnormallog, out string err)
        //{
        //    mSmtkpnormallog.InsertSmtKpNormalLogs(lskpnormallog, out err);
        //}

        [WebMethod]
        public  byte[] QuerytSmtKpNormallog(string dicstring,bool ShowTotal)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpnormallog.QuerytSmtKpNormallog(dicstring,ShowTotal));
        }

         

    }
}
