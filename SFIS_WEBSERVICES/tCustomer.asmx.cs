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
    /// tCustomer 用户信息的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tCustomer : System.Web.Services.WebService
    {
        BLL.tCustomer _tCustomer = new BLL.tCustomer();
        MapListConverter mlc = new MapListConverter();
        /// <summary>
        /// 获取所有的检验批次信息
        /// </summary>
        /// <returns>facId,facname,address</returns>
        [WebMethod]
        public byte[] GettCustomerAll()
        {
            return mlc.GetDataSetSurrogateZipBytes( _tCustomer.GettCustomerAll());
           
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="facinfo"></param>
        [WebMethod]
        public void InsertCustomer(string insp)
        {
            _tCustomer.InserttCustomer(insp);
        }


        // [WebMethod]
        //public byte[] GetCustomerId(string serialcode, string connetperson, string customername)
        // {
            
        //    return mlc.GetDataSetSurrogateZipBytes( _tCustomer.GetCustomerId(serialcode, connetperson, customername));
           
        // }

        [WebMethod]
        public byte[] GetCustomerByName(string customername)
        {
            return mlc.GetDataSetSurrogateZipBytes(_tCustomer.GetCustomerByName(customername));
        }

      
    }
}
