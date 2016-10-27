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
    /// tCustomerComplaint 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tCustomerComplaint : System.Web.Services.WebService
    {
        BLL.tCustomerComplaint _CustomerComplaint = new BLL.tCustomerComplaint();
        MapListConverter mlc = new MapListConverter();
               
        [WebMethod]
        public byte[] GetCustomerComplaint()
        {
            return mlc.GetDataSetSurrogateZipBytes(_CustomerComplaint.GetCustomerComplaint());
        }
        [WebMethod]
        public byte[] GetCustomerComplaintByCondiction(Entity.tCustomerComplaint customercomplaint)
        {
            return mlc.GetDataSetSurrogateZipBytes(_CustomerComplaint.GetCustomerComplaintByCondiction(customercomplaint));
        }

        [WebMethod]
        public byte[] GetCustomerComplaintTOP3(Entity.tCustomerComplaint customercomplaint)
        {
            return mlc.GetDataSetSurrogateZipBytes(_CustomerComplaint.GetCustomerComplaintTOP3(customercomplaint));
        }

        [WebMethod]
        public void UpdateCustomerComplaint(Entity.tCustomerComplaint customercomplaint)
        {
            _CustomerComplaint.UpdateCustomerComplaint(customercomplaint);
        }
         
    }
}
