using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace TestWeserver
{
    /// <summary>
    /// tEditing 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tEditing : System.Web.Services.WebService
    {
        BLL.tEditing mediting = new BLL.tEditing();

        /// <summary>
        /// 增加一个被编辑的项目
        /// </summary>
        /// <param name="_editing"></param>
        /// <returns></returns>
        [WebMethod]
        public string InserttEditing(string  Dicediting)
        {
            return mediting.InserttEditing(Dicediting);
        }

        /// <summary>
        /// 获取一个被编辑的项目
        /// </summary>
        /// <param name="funname"></param>
        /// <returns></returns>
        [WebMethod]
        public string GettEditingInfo(string funname)
        {
            return mediting.GettEditingInfo(funname);
        }
        /// <summary>
        /// 删除一个被锁住的资源
        /// </summary>
        /// <param name="funname"></param>
        /// <returns></returns>
        [WebMethod]
        public string DeletetEditingByfunname(string funname)
        {
            return mediting.DeletetEditingByfunname(funname);
        }
        [WebMethod]
        public string DeletetEditingByUserId(string userId)
        {
            return mediting.DeletetEditingByUserId(userId);
        }
        [WebMethod]
        public string DeletetEditingByUserIdAndPrj(string userId, string prj)
        {
            return mediting.DeletetEditingByUserIdAndPrj(userId, prj);
        }
        [WebMethod]
        public string ChktEditing(string  Dicediting)
        {
            return mediting.ChktEditing(Dicediting);
        }
    }
}
