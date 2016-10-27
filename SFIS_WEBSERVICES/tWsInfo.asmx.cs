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
    /// tWsInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tWsInfo : System.Web.Services.WebService
    {
        BLL.tWsInfo mWsinfo = new BLL.tWsInfo();
        MapListConverter mlc = new MapListConverter();
        /// <summary>
        /// 新增车间信息
        /// </summary>
        /// <param name="wsinfo"></param>
        [WebMethod]
        public void InsertWsInfo(string dicwsinfo)
        {
            mWsinfo.InsertWsInfo(dicwsinfo);
        }

        /// <summary>
        /// 根据车间Id修改车间信息
        /// </summary>
        /// <param name="wsId">需要修改的车间Id</param>
        /// <param name="wsinfo">车间实体</param>
        [WebMethod]
        public void EditWsInfo(string dicwsinfo)
        {
            mWsinfo.EditWsInfo(dicwsinfo);
        }

        /// <summary>
        /// 获取所有车间信息,
        /// wsId,wsname,userId,facId,facname
        /// </summary>
        /// <returns> wsId,wsname,userId,facId,facname</returns>
        [WebMethod]
        public  byte[] GetAllWsInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mWsinfo.GetAllWsInfo());
        }

        /// <summary>
        /// 删除指定的车间信息
        /// </summary>
        /// <param name="wsId"></param>
        [WebMethod]
        public  void DeleteWsInfo(string wsId)
        {
           mWsinfo.DeleteWsInfo(wsId);
        }

        
    }
}
