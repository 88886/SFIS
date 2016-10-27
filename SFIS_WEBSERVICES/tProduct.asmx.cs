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
    /// tProduct 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tProduct : System.Web.Services.WebService
    {
        BLL.tProduct mProduct = new BLL.tProduct();
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();
        MapListConverter mlc = new MapListConverter();

       
        #region 产品分类
        /// <summary>
        /// 增加一种产品分类
        /// </summary>
        /// <param name="product"></param>
        [WebMethod]
        public  void InsertProdutsort(string productsort)
        {
            mProduct.InsertProdutsort(productsort);
        }

        /// <summary>
        /// 查询产品分类是否存在
        /// </summary>
        /// <param name="productsort"></param>
        /// <returns></returns>
        [WebMethod]
        public  bool GetProductSortByName(string sortname)
        {
            return mProduct.GetProductSortByName(sortname);
        }

        /// <summary>
        /// 获取指定成品料号所对应的产品序列号，serialname
        /// </summary>
        /// <param name="partnumber">成品料号</param>
        /// <returns></returns>
        [WebMethod]
        public  byte[] GetProductSnType(string partnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(mProduct.GetProductSnType(partnumber));
        }

        /// <summary>
        /// 添加序列号类型
        /// </summary>
        /// <param name="lname"></param>
        [WebMethod]
        public  void AddLableName(string sn)
        {
            mProduct.AddLableName(sn);
        }

        /// <summary>
        /// 获取产品类型列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public  List<string> GetProductSort()
        {
            return mProduct.GetProductSort();
        }
        #endregion

        #region 产品
        /// <summary>
        /// 获取指定料号的产品信息
        /// </summary>
        /// <param name="partnumber"></param>
        /// <returns></returns>
        [WebMethod]
        public  byte[] GetProductByPartNumber(string partnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(mProduct.GetProduct(partnumber,null));
        }

        /// <summary>
        /// 获取指定产品名称的产品信息
        /// </summary>
        /// <param name="productname"></param>
        /// <returns></returns>
        [WebMethod]
        public  byte[] GetProductByName(string productname)
        {
            return mlc.GetDataSetSurrogateZipBytes(mProduct.GetProduct(null,productname));
        }
        [WebMethod]
        public byte[] GetProductByName_CE(string productname)
        {
            return mlc.GetDataSetZipBytes(mProduct.GetProduct(null,productname));
        }
        /// <summary>
        /// 获取所有的产品
        /// </summary>
        [WebMethod]
        public byte[] GetProduct()
        {
            return mlc.GetDataSetSurrogateZipBytes( mProduct.GetProduct(null,null));
        }

        /// <summary>
        /// 增加产品信息
        /// </summary>
        /// <param name="product"></param>
        /// <param name="Err"></param>
        [WebMethod]
        public string InsertProdctInfo(string StrProduct, string LsSerialinfo, string StrPalletinfo)
        {
            return mPro.InsertProdctInfo(StrProduct, LsSerialinfo, StrPalletinfo);
        }
        /// <summary>
        /// 增加产品所对应的标签
        /// </summary>
        /// <param name="partnumber"></param>
        /// <param name="lslablenames"></param>
        /// <param name="Err"></param>
        [WebMethod]
        private void InsertProductLable(string partnumber, List<string> lslablenames, out string Err)
        {       
            Err = string.Empty;
            mProduct.InsertProductLable(partnumber, lslablenames, out Err);
        }
        /// <summary>
        /// 根据工单号获取产品信息
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetProductInfoByWoId(string woid)
        {
           return  mlc.GetDataSetSurrogateZipBytes(mProduct.GetProductInfoByWoId(woid));
        }
        [WebMethod]
        public byte[] GetProductInfoByWoId_WinCE(string woid)
        {
            return mlc.GetDataSetZipBytes(mProduct.GetProductInfoByWoId(woid));
        }
        [WebMethod]
        public byte[] GetProductInfo(string partnumber, string productName)
        {
            return mlc.GetDataSetSurrogateZipBytes(mProduct.GetProductInfo(partnumber, productName));
        }
        [WebMethod]
        public byte[] GetAllProduct()
        {
            return mlc.GetDataSetSurrogateZipBytes(mProduct.GetProduct(null,null));
        }
        #endregion

        #region 标签
        /// <summary>
        /// 获取所有的标签类型
        /// </summary>
        [WebMethod]
        public  List<string> GetLableList()
        {
            return mProduct.GetLableList();
        }

        /// <summary>
        /// 获取指定产品所对应的序列号类型列表
        /// </summary>
        /// <param name="partnumber">产品料号</param>
        /// <returns>序列号类型列表</returns>
        [WebMethod]
        public  List<string> GetProductLableNames(string partnumber)
        {
            return mProduct.GetProductLableNames(partnumber);
        }
        #endregion

     

    }
}
