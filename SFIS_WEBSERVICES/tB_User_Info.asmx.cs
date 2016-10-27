using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tB_User_Info 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tB_User_Info : System.Web.Services.WebService
    {
        BLL.tB_User_Info mUserInfo = new BLL.tB_User_Info();
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();
        MapListConverter mlc = new MapListConverter();


        /// <summary>
        /// 获取所有的用户信息
        /// </summary>
        /// <returns></returns>
         [WebMethod(MessageName = "GetUserInfo")]
        public byte[] Get_UserInfo(string JsonUser, string FieldList)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(JsonUser);
            return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetUserInfo(mst, FieldList));
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="userinfo"></param>
        /// <param name="Err"></param>
       [WebMethod(MessageName = "InsertUserInfo")]
        public string Insert_UserInfo(string dicuserinfo)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicuserinfo);
            return mUserInfo.InsertUserInfo(mst);
        }
       [WebMethod(MessageName = "ModifyUserInfo")]
       public void Modify_UserInfo(string dicuserInfo)
       {
           IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicuserInfo);
           mUserInfo.Modify_UserInfo(mst);
       }

         [WebMethod(MessageName = "DeleteUserInfo")]
        public void Delete_UserInfo(string userId)
        {
            mUserInfo.DeleteUserInfoByUserId(userId);
        }
    }
}
