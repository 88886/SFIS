using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericProvider;
using GenericUtil;

namespace BLL
{
   public class tB_User_Info
    {
       public tB_User_Info()
       {
       }

       /// <summary>
       /// 获取所有的用户信息
       /// </summary>
       /// <returns></returns>
       public System.Data.DataSet GetUserInfo(IDictionary<string, object> mst, string FieldList)
       {
           int count = 0;
           string table = "SFCB.B_USER_INFO";
           if (string.IsNullOrEmpty(FieldList))
               FieldList = "USERID,ROLECAPTION,DEPTNAME,FACID,USERNAME,PWD,USERPHONE,USEREMAIL,USERSTATUS";
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           return dp.GetData(table, FieldList, mst, out count);
       }
       /// <summary>
       /// 修改用户信息
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="userInfo"></param>
       public void Modify_UserInfo(IDictionary<string, object> mst)
       {
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);        ;
           dp.UpdateData("SFCB.B_USER_INFO", new string[] { "USERID" }, mst);
       }

       /// <summary>
       /// 根据用户工号删除用户
       /// </summary>
       /// <param name="userId"></param>
       public void DeleteUserInfoByUserId(string userId)
       {
           string table = "SFCB.B_USER_INFO";
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("USERID", userId);
           dp.DeleteData(table, mst);
           dp.DeleteData("sfcb.b_user_poplist".ToUpper(), mst);

       }

       /// <summary>
       /// 新增用户信息
       /// </summary>
       /// <param name="userinfo"></param>
       /// <param name="Err"></param>
       public string InsertUserInfo(IDictionary<string, object> mst)
       {
           string Err = "";
           try
           {
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);             
               dp.AddData("SFCB.B_USER_INFO", mst);
               Err = "OK";
           }
           catch (Exception ex)
           {
               Err = ex.Message;

           }
           return Err;

       }

    }
}
