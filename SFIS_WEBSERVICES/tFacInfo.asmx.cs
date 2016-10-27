using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tFacInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tFacInfo : System.Web.Services.WebService
    {
        BLL.tFacInfo mFacinfo = new BLL.tFacInfo();
        MapListConverter mlc = new MapListConverter();
        /// <summary>
        /// 获取所有的工厂信息
        /// </summary>
        /// <returns>facId,facname,address</returns>
        [WebMethod]
        public  byte[] GetFacInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mFacinfo.GetFacInfo());
        }

        /// <summary>
        /// 新增工厂信息
        /// </summary>
        /// <param name="facinfo"></param>
        [WebMethod]
        public void InsertFacInfo(string dicFacinfo)
        {
            mFacinfo.InsertFacInfo(dicFacinfo);
        }

        /// <summary>
        /// 修改工厂信息
        /// </summary>
        /// <param name="facId"></param>
        /// <param name="facinfo"></param>
        [WebMethod]
        public void EditFacInfo(string dicFacinfo)
        {
            mFacinfo.EditFacInfo(dicFacinfo);
        }

        /// <summary>
        /// 删除工厂信息
        /// </summary>
        /// <param name="facId"></param>
        [WebMethod]
        public  void DeleteFacInfo(string facId)
        {
            mFacinfo.DeleteFacInfo(facId);
        }
        [WebMethod]
        public byte[] GetFacCodeList()
        {
            return mlc.GetDataSetSurrogateZipBytes(mFacinfo.GetFacCodeList());
        }

         
    }
}
