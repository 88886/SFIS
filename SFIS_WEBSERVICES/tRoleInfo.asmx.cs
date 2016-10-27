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
    /// tRoleInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tRoleInfo : System.Web.Services.WebService
    {
        BLL.tRoleInfo mRoleinfo = new BLL.tRoleInfo();
        MapListConverter mlc = new MapListConverter();
        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public  byte[] GetRoleInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mRoleinfo.GetRoleInfo());
        }

        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="roleinfo"></param>
        [WebMethod]
        public void InsertRoleInfo(string roleinfo)
        {
            mRoleinfo.InsertRoleInfo(roleinfo);
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="rolecaption"></param>
        /// <param name="roleinfo"></param>
        [WebMethod]
        public  void EditRoleInfo(string roleinfo)
        {
            mRoleinfo.EditRoleInfo(roleinfo);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="rolecaption"></param>
        [WebMethod]
        public  void DeleteRoleInfo(string rolecaption)
        {
            mRoleinfo.DeleteRoleInfo(rolecaption);
        }

        [WebMethod]
        public  byte[] QueryRoleInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mRoleinfo.GetRoleInfo());
        }
        
    }
}
