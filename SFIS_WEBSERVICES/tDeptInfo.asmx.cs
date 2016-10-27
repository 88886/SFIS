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
    /// tDeptInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tDeptInfo : System.Web.Services.WebService
    {
        BLL.tDeptInfo mdeptinfo = new BLL.tDeptInfo();
        MapListConverter mlc = new MapListConverter();
        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="deptinfo"></param>
        [WebMethod]
        public void InsertDeptInfo(string deptinfo)
        {
            mdeptinfo.InsertDeptInfo(deptinfo);
        }

        /// <summary>
        /// 获取所有部门信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetDeptInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mdeptinfo.GetDeptInfo());
        }

        ///// <summary>
        ///// 根据工厂名称获取部门信息
        ///// </summary>
        ///// <param name="facname"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] GetDeptInfoByFacname(string facname)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mdeptinfo.GetDeptInfoByFacname(facname));
        //}

        ///// <summary>
        ///// 获取指定用户所负责的部门
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] GetDeptInfoByUserId(string userId)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mdeptinfo.GetDeptInfoByUserId(userId));
        //}

        /// <summary>
        /// 修改部门信息根据部门名称
        /// </summary>
        /// <param name="deptname"></param>
        /// <param name="deptinfo"></param>
        [WebMethod]
        public void EditDeptInfo(string deptname)
        {
            mdeptinfo.EditDeptInfo(deptname);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deptname"></param>
        [WebMethod]
        public void DeleteDeptInfo(string deptname)
        {
            mdeptinfo.DeleteDeptInfo(deptname);
        }

         

    }
}
