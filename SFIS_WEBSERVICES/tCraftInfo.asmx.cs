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
    /// tCraftInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tCraftInfo : System.Web.Services.WebService
    {
        BLL.tCraftInfo mcraftinfo = new BLL.tCraftInfo();
        BLL.ProPublicStoredproc ProPubStor = new BLL.ProPublicStoredproc();
        BLL.tWoInfo woinfo = new BLL.tWoInfo();
        MapListConverter mlc = new MapListConverter();


        #region 工艺
        /// <summary>
        /// 获取所有工艺名称，craftId,craftname,craftparamet
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public  byte[] GetAllCraftInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mcraftinfo.GetAllCraftInfo(null));
        }       

        /// <summary>
        /// 获取工艺，craftId,craftname,craftparamet
        /// </summary>
        /// <param name="craftId"></param>
        /// <returns></returns>
        [WebMethod(MessageName = "Get_Craft_Info")]
        public  byte[] Get_Craft_Info(string Json)
        {
             IDictionary<string,object> mst=  MapListConverter.JsonToDictionary(Json);
             return mlc.GetDataSetSurrogateZipBytes(mcraftinfo.GetAllCraftInfo(mst));
        }
        ///// <summary>
        ///// 更新工艺
        ///// </summary>
        ///// <param name="craftId"></param>
        ///// <param name="craftInfo"></param>
        //[WebMethod]
        //public  void EditCraftInfo(string dicstring)
        //{
        //    mcraftinfo.EditCraftInfo(dicstring);
        //}
        ///// <summary>
        ///// 添加工艺描述
        ///// </summary>
        ///// <param name="craftinfo"></param>
        //[WebMethod]
        //public  void InsertCraftInfo(Entity.tCraftInfo craftinfo)
        //{
        //    mcraftinfo.InsertCraftInfo_SP(craftinfo);
        //}

        ///// <summary>
        ///// 删除工艺描述
        ///// </summary>
        ///// <param name="craftId"></param>
        //[WebMethod]
        //public  void DeleteCraftInfo(string craftId)
        //{
        //    mcraftinfo.DeleteCraftInfo(craftId);
        //}
        #endregion

        #region 工艺项目
        ///// <summary>
        ///// 添加工艺项目及参数
        ///// </summary>
        ///// <param name="craftitem"></param>
        ///// <param name="err"></param>
        //[WebMethod]
        //public  void InsertCraftItem(Entity.tCraftItem craftitem, out string err)
        //{
        //    mcraftinfo.InsertCraftItem(craftitem, out err);
        //}

        /// <summary>
        /// 添加工艺项目及参数(先删除所有存在的工艺项目Id)
        /// </summary>
        /// <param name="lsCraftItem"></param>
        /// <param name="err"></param>
        [WebMethod]
        public void InsertCraftItems(string craftId, string lsCraftItem, out string err)
        {
            mcraftinfo.InsertCraftItem(craftId, lsCraftItem, out err);
        }
        /// <summary>
        /// 获取所有的工艺项目及参数
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public  byte[] GetAllCraftItme()
        {
            return mlc.GetDataSetSurrogateZipBytes(mcraftinfo.GetAllCraftInfo(null));
        }

        /// <summary>
        /// 根据工艺Id获取对应的工艺项目
        /// </summary>
        /// <param name="craftId"></param>
        /// <returns></returns>
        [WebMethod]
        public  byte[] GetCraftItemByCraftId(string craftId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mcraftinfo.GetCraftItemByCraftId(craftId));
        }

        /// <summary>
        /// 删除工艺项目
        /// </summary>
        /// <param name="craftId"></param>
        [WebMethod]
        public  void DeleteCraftItem(string craftId)
        {
            mcraftinfo.DeleteCraftItem(craftId);
        }
        #endregion

        /// <summary>
        /// 添加整个工艺及对应的工艺项目
        /// </summary>
        /// <param name="craftinfo"></param>
        /// <param name="lsCraftItem"></param>
        /// <param name="err"></param>
        [WebMethod]
        public string InsertRefCraftInfo(string craftinfo, string lsCraftItem, out string err)
        {
            return mcraftinfo.InsertRefCraftInfo(craftinfo, lsCraftItem, out err);
        }

        /// <summary>
        /// 获取所有制程段
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetAllWorksegment()
        {
            return mlc.GetDataSetSurrogateZipBytes(mcraftinfo.GetAllWorksegment());
        }
        [WebMethod]
        public  void InsertWorkSegment(string bworksegment)
        {
            mcraftinfo.InsertWorkSegment(bworksegment);
        }

        [WebMethod]
        public List<string> GetCraftInfoCraftparameterurl()
        {
            return mcraftinfo.GetCraftInfoCraftparameterurl();     
        }
        [WebMethod]
        public byte[] GetAllCarftInfoByWo(string woId)
        {
            return mlc.GetDataSetSurrogateZipBytes(woinfo.GetWOCraftInfo_ds(woId,"0"));
        }
        [WebMethod]
        public byte[] GetAllCraftInfo2()
        {
            return mlc.GetDataSetSurrogateZipBytes(mcraftinfo.GetAllCraftInfo2());
        }

      

    }
}
