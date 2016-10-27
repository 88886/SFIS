using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Data;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tLineInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tLineInfo : System.Web.Services.WebService
    {
        BLL.tLineInfo mLineinfo = new BLL.tLineInfo();
        MapListConverter mlc = new MapListConverter();
        /// <summary>
        /// 获取所有线别信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public  byte[] GetAllLineInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mLineinfo.GetAllLineInfo());
        }
       [WebMethod(MessageName = "GetLineInfo")]
        public byte[] GetLineInfo(string Json, string Fields)
        {
           IDictionary<string, object> mst= new Dictionary<string, object>();
           if (!string.IsNullOrEmpty(Json))
            mst = MapListConverter.JsonToDictionary(Json);
            return mlc.GetDataSetSurrogateZipBytes(mLineinfo.GetLineInfo(mst, Fields));
        }

        /// <summary>
        /// 根据线体编号修改线体信息
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="lineinfo"></param>
        [WebMethod]
        public void EditLineInfo(string dicLineinfo)
        {
            mLineinfo.EditLineInfo(dicLineinfo);
        }

        /// <summary>
        /// 新增线体信息
        /// </summary>
        /// <param name="lineinfo"></param>
        [WebMethod]
        public void InsertLineInfo(string dicLineinfo)
        {
            mLineinfo.InsertLineInfo(dicLineinfo);
        }

        /// <summary>
        /// 删除线体
        /// </summary>
        /// <param name="lineId"></param>
        [WebMethod]
        public void DeleteLineInfo(string lineId)
        {
            mLineinfo.DeleteLineInfo(lineId);
        }

        [WebMethod]
        public List<string> GetLineList()
        {
           return mLineinfo.GetLineList();
        }

    }
}
