using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tUserInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tUserInfo : System.Web.Services.WebService
    {
        BLL.tUserInfo mUserInfo = new BLL.tUserInfo();
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();
        MapListConverter mlc = new MapListConverter();
        BLL.FileHelper _FileHelper = new BLL.FileHelper();
        [WebMethod]
        public void DeleteUserInfoByUserId(string userId)
        {
            mUserInfo.DeleteUserInfoByUserId(userId);
        }
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="userinfo"></param>
        /// <param name="Err"></param>
        [WebMethod]
        public string InsertUserInfo(string dicuserinfo)
        {
           return mUserInfo.InsertUserInfo(dicuserinfo);
        }

        /// <summary>
        /// 获取所有的用户信息
        /// </summary>
        /// <returns></returns>
        [WebMethod(MessageName = "获取所有的用户信息")]
        public byte[] GetUserInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetUserInfo());
        }
        [WebMethod]
        public byte[] GetUserInfo_WinCE()
        {
            return mlc.GetDataSetZipBytes(mUserInfo.GetUserInfo());
        }

        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [WebMethod(MessageName = "根据条件获取用户信息")]
        public byte[] GetUserInfo(string UserId,string username,string PWD)
        {
            return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetUserInfo(UserId,username,PWD));
        }
        [WebMethod(MessageName = "根据条件获取用户信息_WINCE")]
        public byte[] GetUserInfo_WinCE(string UserId, string username, string PWD)
        {
            return mlc.GetDataSetZipBytes(mUserInfo.GetUserInfo(UserId, username, PWD));
        }

        //[WebMethod]
        //public byte[] GetUserInfoByUserName_WinCE(string username)
        //{
        //    return mlc.GetDataSetZipBytes(mUserInfo.GetUserInfoByUserName(username));
        //}
        /// <summary>
        /// 根据用户工号获取用户信息;
        /// userId,username,rolecaption,deptname,facId,userphone,useremail,userstatus
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetUserInfoByUserId(string userId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetUserInfo(userId, "", ""));
        }
        //[WebMethod]
        //public byte[] GetUserInfoByUserId_WinCE(string userId)
        //{
        //    return mlc.GetDataSetZipBytes(mUserInfo.GetUserInfoByUserId(userId));
        //}
        /// <summary>
        /// 根据部门获取用户信息
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        //[WebMethod]
        //public byte[] GetUserInfoByDept(string dept)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetUserInfoByDept(dept));
        //}
        //[WebMethod]
        //public byte[] GetUserInfoByDept_WinCE(string dept)
        //{
        //    return mlc.GetDataSetZipBytes(mUserInfo.GetUserInfoByDept(dept));
        //}

        ///// <summary>
        ///// 根据工厂获取用户信息
        ///// </summary>
        ///// <param name="facname"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] GetUserInfoByFacname(string facname)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetUserInfoByFacname(facname));
        ////}
        //[WebMethod]
        //public byte[] GetUserInfoByFacname_WinCE(string facname)
        //{
        //    return mlc.GetDataSetZipBytes(mUserInfo.GetUserInfoByFacname(facname));
        //}

        ///// <summary>
        ///// 根据角色名称获取用户信息
        ///// </summary>
        ///// <param name="rolename"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] GetUserInfoByRole(string rolename)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetUserInfoByRole(rolename));
        //}
        //[WebMethod]
        //public byte[] GetUserInfoByRole_WinCE(string rolename)
        //{
        //    return mlc.GetDataSetZipBytes(mUserInfo.GetUserInfoByRole(rolename));
        //}

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userInfo"></param>
        [WebMethod]
        public void EditUserInfo(string  dicuserInfo)
        {
            mUserInfo.EditUserInfo(dicuserInfo);
        }
        [WebMethod]
        public bool CheckUserInfoByUserId(string userId)
        {
            return mUserInfo.CheckUserInfoByUserId(userId);
        }

        [WebMethod]
        public bool ChkUserInfoIdAndPwd(string userId, string pwd)
        {
            return mUserInfo.ChkUserInfoIdAndPwd(userId, pwd);
        }

        #region 用户权限部分
        ///// <summary>
        ///// 返回用户的部分信息
        ///// </summary>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] GetJurUserInfo()
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetJurUserInfo());
        //}
        //[WebMethod]
        //public byte[] GetJurUserInfo_WinCE()
        //{
        //    return mlc.GetDataSetZipBytes(mUserInfo.GetJurUserInfo());
        //}
        /// <summary>
        /// 返回所有功能
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetAllProgFunctionInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetAllProgFunctionInfo());
        }
        [WebMethod]
        public byte[] GetAllProgFunctionInfo_WinCE()
        {
            return mlc.GetDataSetZipBytes(mUserInfo.GetAllProgFunctionInfo());
        }

        /// <summary>
        /// 添加用户权限
        /// </summary>
        /// <param name="ArrUserPopList"></param>
        /// <returns></returns>
        [WebMethod]
        public string AddUserJurisdiction(string LsDicstring)
        {      
            return mUserInfo.AddUserJurisdiction(LsDicstring);
        }

        /// <summary>
        /// 获取指定人员的用户权限列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetUserJurisdictionByUserId(string userId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetUserJurisdictionByUserId(userId));
        }

        [WebMethod]
        public byte[] GetUserJurisdictionByUserId_WinCE(string userId)
        {
            return mlc.GetDataSetZipBytes(mUserInfo.GetUserJurisdictionByUserId(userId));
        }
        /// <summary>
        /// 获取指定人员的用户权限列表(通过SQL语句实现)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetUserJurisdictionByUserId2(string userId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetUserJurisdictionByUserId2(userId));
        }
        [WebMethod]
        public byte[] GetUserJurisdictionByUserId2_WinCE(string userId)
        {
            return mlc.GetDataSetZipBytes(mUserInfo.GetUserJurisdictionByUserId2(userId));
        }

        ///// <summary>
        ///// 根据用户工号返回用户的信息
        ///// </summary>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] GetJurUserInfoById(string userId)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetJurUserInfoById(userId));
        ////}
        //[WebMethod]
        //public byte[] GetJurUserInfoById_WinCE(string userId)
        //{
        //    return mlc.GetDataSetZipBytes(mUserInfo.GetJurUserInfoById(userId));
        //}

        ///// <summary>
        ///// 根据用户名返回用户的信息
        ///// </summary>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] GetJurUserInfoByName(string username)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetJurUserInfoByName(username));
        //}

        //[WebMethod]
        //public byte[] GetJurUserInfoByName_WinCE(string username)
        //{
        //    return mlc.GetDataSetZipBytes(mUserInfo.GetJurUserInfoByName(username));
        //}

        /// <summary>
        /// 添加程序
        /// </summary>
        /// <param name="proginfomodel"></param>
        [WebMethod]
        public void AddProgInfo(string proginfomodel)
        {
            mUserInfo.AddProgInfo(proginfomodel);
        }
        /// <summary>
        /// 添加功能
        /// </summary>
        /// <param name="lsfunlist"></param>
        [WebMethod]
        public void AddFunctionList(string lsfunlist)
        {
            mUserInfo.AddFunctionList(lsfunlist);
        }
        /// <summary>
        /// 检查应用程序编号是否存在
        /// </summary>
        /// <param name="progid"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ChkProgId(string progid)
        {
            return mUserInfo.ChkProgId(progid);
        }
        /// <summary>
        /// 检查应用程序的功能是否存在
        /// </summary>
        /// <param name="progid"></param>
        /// <param name="funid"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ChkFunctionList(string progid, string funid)
        {
            return mUserInfo.ChkFunctionList(progid, funid);
        }
        /// <summary>
        /// 用户自己修改信息
        /// </summary>
        /// <param name="userinfo"></param>
        [WebMethod]
        public void UpdateUserPassword(string dicuserinfo)
        {
            mUserInfo.UpdateUserPassword(dicuserinfo);
        }
        #endregion

        ///// <summary>
        ///// 根据用户工号返回用户的信息
        ///// </summary>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] GetJurUserInfoByIdandpwd(string userId, string pwd)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetJurUserInfoByIdandpwd(userId, pwd));
        //}\
        [WebMethod]
        public string Clear_User_Info(string UserId)
        {
            return mUserInfo.Clear_User_Info(UserId);
        }

        #region 测试权限
        /// <summary>
        /// 添加用户ATE权限
        /// </summary>
        /// <param name="lsAteEmp"></param>
        /// <returns></returns>
        [WebMethod]
        public string AddUserAteEmp(string lsAteEmp)
        {
            return mUserInfo.AddUserAteEmp(lsAteEmp);
        }
        /// <summary>
        /// 获取用户使用ATE测试的权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetUserAteEmp(string userId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mUserInfo.GetUserAteEmp(userId));
        }

        /// <summary>
        /// 检查是否具有ate权限
        /// </summary>
        /// <param name="ateemp"></param>
        /// <returns></returns>
        [WebMethod]
        public string ChkUserAteEmp(string UserId, string CraftId)
        {
            return mUserInfo.ChkUserAteEmp( UserId, CraftId);
        }
        [WebMethod]
        public string CheckEmp_NEW(string EMP, string ipaddress, string macaddress, string mygroup)
        {
            return mPro.PRO_CHECKEMP_NEW(EMP, ipaddress, macaddress, mygroup);
                
                //mUserInfo.CheckEmp_NEW(EMP, ipaddress, macaddress, mygroup);
        }
        [WebMethod]
        public string DeleteLogin(string userId, string strIpaddress)
        {
            return null; // mUserInfo.DeleteLogin(userId, strIpaddress);
        }
        [WebMethod(MessageName = "CheckLineEmployee")]
         public string CHECK_SET_LINE_EMPLOYEE(string UserId,string PWD)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("USERID", UserId);
            dic.Add("PWD", PWD);
            return mUserInfo.CHECK_SET_LINE_EMPLOYEE(dic);
        }
        #endregion


         
    }
}
