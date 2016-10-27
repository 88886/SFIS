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
    /// tStorehouseManage 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tStorehouseManage : System.Web.Services.WebService
    {
        BLL.tStorehouseManage mStorehousemanage = new BLL.tStorehouseManage();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        #region 新增
        [WebMethod]
        public string AddStorehouse(string dicwarehouse)
        {
            return mStorehousemanage.AddStorehouse(dicwarehouse);
        }

        [WebMethod]
        public string AddStorehouseloction(string  dicwarehouseloc)
        {
            return mStorehousemanage.AddStorehouseloction(dicwarehouseloc);
        }

        [WebMethod]
        public void AddStorehouselocType(string loctype)
        {
            mStorehousemanage.AddStorehouselocType(loctype);
        }

        #endregion
        #region 修改
        [WebMethod]
        public void UpdateStorehouse(string dicwarehouse)
        {
            mStorehousemanage.UpdateStorehouse(dicwarehouse);
        }

        [WebMethod]
        public void UpdateStorehouseloction(string dicwarehouseloc)
        {
            mStorehousemanage.UpdateStorehouseloction(dicwarehouseloc);
        }

        #endregion
        #region 删除
        //[WebMethod]
        //public void DeleteStorehouse(string warehouseid)
        //{
        //    mStorehousemanage.DeleteStorehouse(warehouseid);
        //}

        [WebMethod]
        public void DeleteStorehouseloction(string locid)
        {
            mStorehousemanage.DeleteStorehouseloction(locid);
        }

        #endregion
        #region 查询
        [WebMethod]
        public bool ChkStoreLocaltion(string store, string locid)
        {
            return mStorehousemanage.ChkStoreLocaltion(store, locid);
        }
        [WebMethod]
        public byte[] GetAlltStorehouseInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GetAlltStorehouseInfo());
        }

        [WebMethod]
        public byte[] GetAlltStorehouseLoctionInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GetAlltStorehouseLoctionInfo());
        }
        [WebMethod]
        public byte[] GettStorehouseInfoById(string warehouseid)
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GettStorehouseInfoById(warehouseid));
        }

        [WebMethod]
        public byte[] GettStorehouseLoctionInfo(string _selname, string _value)
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GettStorehouseLoctionInfo(_selname, _value));
        }

        [WebMethod]
        public byte[] GettStorehouseLoctionInfoById(string locid)
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GettStorehouseLoctionInfoById(locid));
        }

        [WebMethod]
        public byte[] GettStorehouseType()
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GettStorehouseType());
        }

        [WebMethod]
        public byte[] GettStorehouseLoctionType()
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GettStorehouseLoctionType());
        }

        
        /// <summary>
        /// 获取所有仓库的编号和对应的名称
        /// </summary>
        [WebMethod]
        public byte[] GetWarehouseList()
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GetWarehouseList());
        }

         /// <summary>
        /// 根据参考编号获取该仓库下所有的库位编号列表
        /// </summary>
        /// <param name="storehouseId"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetWarehouseLoctionListBystorehouseId(string storehouseId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GetWarehouseLoctionListBystorehouseId(storehouseId));
        }
        //[WebMethod]
        //public byte[] GetPartNuByVp(string vpId)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GetPartNuByVp(vpId));
        //}
        #endregion
        [WebMethod]
        public  byte[] GetAllWarehouseId()
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GetAllWarehouseId());
        }
        [WebMethod]
        public  byte[] GetAllLocIdByWarehouseId(string storehouseId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GetAllLocIdByWarehouseId(storehouseId));
        }
        [WebMethod]
        public byte[] GetLotIdByStorehouseId(string storehouseid)
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GetLotIdByStorehouseId(storehouseid));
        }
        [WebMethod]
        public byte[] GetWarehouseListInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mStorehousemanage.GetWarehouseListInfo());
        }
         

    }
}
