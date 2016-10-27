using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GenericProvider;
using GenericUtil;
using SystemObject;
using System.Data.Common;
using SrvComponent;
using System.Data;

namespace BLL
{
    public partial class tUserInfo
    {
        
        public tUserInfo()
        {
           //if(! Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory+"\\log"))
           //{
           //    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "\\log");
           //}
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
        public string InsertUserInfo(string dicuserinfo)
        {
           string Err = "";
            try
            {

                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicuserinfo);
                dp.AddData("SFCB.B_USER_INFO", mst);
                Err = "OK";
            }
            catch (Exception ex)
            {
                Err = ex.Message;       

            }
            return Err;

        }

        /// <summary>
        /// 获取所有的用户信息
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetUserInfo()
        {           
            string table = "SFCB.B_USER_INFO";
            string fieldlist = "userId as 工号,username as 用户名,rolecaption as 角色名称,deptname as 部门名称,facId as 工厂编号,pwd as 密码,userphone as 联系电话,useremail as 电子邮箱,userstatus as 用户状态";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);           
            return  dp.GetData(table, fieldlist, null, out count);
        }

        /// <summary>
        /// 根据用户工号获取用户信息;
        /// userId,username,rolecaption,deptname,facId,userphone,useremail,userstatus
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetUserInfo(string userId,string UserName,string PWD)
        {          
            string table = "SFCB.B_USER_INFO";
            string fieldlist = "USERID,USERNAME,ROLECAPTION,DEPTNAME,FACID,USERPHONE,USEREMAIL,USERSTATUS,PWD";

            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(userId))
            mst.Add("USERID", userId);
            if (!string.IsNullOrEmpty(UserName))
                mst.Add("USERNAME", UserName);
            if (!string.IsNullOrEmpty(PWD))
                mst.Add("PWD", PWD);
           return dp.GetData(table, fieldlist,   mst, out count);

        }

        public bool CheckUserInfoByUserId(string userId)
        {
            if (GetUserInfo(userId,null,null).Tables[0].Rows.Count < 1)
                return false;
            else
                return true;
        }
        public string Clear_User_Info(string UserId)
        {

            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("USERID", UserId);
                mst.Add("PWD", "");
                mst.Add("USERSTATUS", "0");
                dp.UpdateData(tx, "SFCB.B_USER_INFO", new string[] { "USERID" }, mst);
                mst = new Dictionary<string, object>();
                mst.Add("USERID", UserId);
                dp.DeleteData(tx, "sfcb.b_user_poplist".ToUpper(), mst);
                tx.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                tx.Rollback();
                return ex.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userInfo"></param>
        public void EditUserInfo(string dicuserInfo)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicuserInfo);
            dp.UpdateData("SFCB.B_USER_INFO", new string[] { "USERID" }, mst);
        }
        /// <summary>
        /// 检查用户名和密码是否匹配
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool ChkUserInfoIdAndPwd(string userId, string pwd)
        {
            DataTable _dt = this.GetUserInfo(userId, null, pwd).Tables[0];
            if (_dt == null || _dt.Rows.Count < 1)
              
                return false;
            else
                return true;
        }

        #region 用户权限部分
        ///// <summary>
        ///// 返回用户的部分信息
        ///// </summary>
        ///// <returns></returns>
        //public System.Data.DataSet GetJurUserInfo(string UserId, string UserName, string PWD)
        //{
        //    //MySqlCommand cmd = new MySqlCommand();
        //    //cmd.CommandText = "select  username,userId,deptname from SFCB.B_USER_INFO";
        //    //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

        //    string table = "SFCB.B_USER_INFO";
        //    string fieldlist = "username,userId,deptname";
        //    int count = 0;
        //    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
        //    IDictionary<string, object> mst = new Dictionary<string, object>();
        //    if (!string.IsNullOrEmpty(UserId))
        //        mst.Add("USERID", UserId);
        //    if (!string.IsNullOrEmpty(UserName))
        //        mst.Add("USERNAME", UserName);
        //    if (!string.IsNullOrEmpty(PWD))
        //        mst.Add("PWD", PWD);

        //   return dp.GetData(table, fieldlist,mst, out count);

        //}

        ///// <summary>
        ///// 根据用户工号返回用户的信息
        ///// </summary>
        ///// <returns></returns>
        //public System.Data.DataSet GetJurUserInfoById(string userId)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "select  username,userId,deptname from SFCB.B_USER_INFO where userId=@userId";
        //    cmd.Parameters.Add("userId", MySqlDbType.VarChar, 15).Value = userId;

        //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}

        ///// <summary>
        ///// 根据用户名返回用户的信息
        ///// </summary>
        ///// <returns></returns>
        //public System.Data.DataSet GetJurUserInfoByName(string username)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "select username,userId,deptname from SFCB.B_USER_INFO where username like @username";
        //    cmd.Parameters.Add("username", MySqlDbType.VarChar, 15).Value = username;

        //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}

        /// <summary>
        /// 返回所有功能
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetAllProgFunctionInfo()
        {         
            int count = 0;
            string table = "SFCB.B_FUNCTION_LIST f,SFCB.B_PROG_INFO p";
            string fieldlist = "0 as Jurisdiction, p.progId,p.progname ,f.funId,f.funname,f.fundesc";
            string filter = "p.progId=f.progId";            
            return TransactionManager.GetData(table, fieldlist, filter, null, null, null, out count);
        }

        /// <summary>
        /// 获取指定人员的用户权限列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetUserJurisdictionByUserId(string userId)
        {          
            string table = "SFCB.B_USER_POPLIST";
            string fieldlist = "progid,funId";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("USERID", userId);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        public string CHECK_SET_LINE_EMPLOYEE(IDictionary<string,object> dic )
        {
            string C_RES="USERID OR PASSWORD IS NULL";
            if (!string.IsNullOrEmpty(dic["USERID"].ToString()) && !string.IsNullOrEmpty(dic["PWD"].ToString()))
            {

                DataTable dt = GetUserInfo(dic["USERID"].ToString(), null, dic["PWD"].ToString()).Tables[0];
              if (dt.Rows.Count == 0)
              {
                  C_RES = "NO EMPLOYEE";
              }
              else
              {
                 DataTable dtProgList = GetUserJurisdictionByUserId(dic["USERID"].ToString()).Tables[0];
                 DataRow[] ArrDr = dtProgList.Select(string.Format("progid='Frm_MO_Manage' and funId='{0}'", "MODIFY_LINE"));
                if ((ArrDr == null || ArrDr.Length < 1) && dt.Rows[0]["rolecaption"].ToString() != "系统开发员")
                {
                    C_RES = "NO SET LINE EMPLOYEE,Please Call TE";
                }
                else
                {
                    C_RES="OK";
                }
              }
            }
            return C_RES;
            
        }

        /// <summary>
        /// 获取指定人员的用户权限列表(通过SQL语句实现)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetUserJurisdictionByUserId2(string userId)
        {           
            int count = 0;
            string table = "(select p.progId,p.progname,f.funId,f.funname,f.fundesc from SFCB.B_FUNCTION_LIST f, SFCB.B_PROG_INFO p";

            string fieldlist = "B.USERID,A.* ";
            string filter = "p.progId = f.progId) A LEFT JOIN SFCB.B_USER_POPLIST B ON  B.FUNID=A.FUNID AND B.PROGID=A.PROGID AND B.USERID={0} ";
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("USERID", userId);
            DataTable dt= TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
            DataTable dts = new DataTable();
            dts.Columns.Add("Jurisdiction", typeof(int));
            dts.Columns.Add("PROGID", typeof(string));
            dts.Columns.Add("PROGNAME", typeof(string));
            dts.Columns.Add("FUNID", typeof(string));
            dts.Columns.Add("FUNNAME", typeof(string));
            dts.Columns.Add("FUNDESC", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                
                if (!string.IsNullOrEmpty(dr[0].ToString()))
                    dts.Rows.Add(1,dr[1].ToString(),dr[2].ToString(),dr[3].ToString(),dr[4].ToString(),dr[5].ToString());
                else
                    dts.Rows.Add(0, dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
           
            }
          
            DataSet ds = new DataSet();
            ds.Tables.Add(dts.Copy());
            return ds;
        }

        /// <summary>
        /// 增加用户权限
        /// </summary>
        /// <param name="ArrUserPopList"></param>
        public string AddUserJurisdiction(string LsDicstring)
        {
            //StringBuilder sql = new StringBuilder();
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
              IList<IDictionary<string,object>> LsDic= MapListConverter.JsonToListDictionary(LsDicstring);              
              string table = "SFCB.B_USER_POPLIST";            
              IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
              IDictionary<string, object> dic = new Dictionary<string, object>();
              dic.Add("USERID", LsDic[0]["USERID"]);
              dp.DeleteData(tx, table, dic);
              dp.AddListData(tx, table, LsDic);

              tx.Commit();
                return null;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                return ex.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        ///// <summary>
        ///// 根据用户工号和密码返回用户的信息
        ///// </summary>
        ///// <returns></returns>
        //public System.Data.DataSet GetJurUserInfoByIdandpwd(string userId, string pwd)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "select  username,userId,deptname from SFCB.B_USER_INFO where userId=@userId and pwd=@pwd ";
        //    cmd.Parameters.Add("userId", MySqlDbType.VarChar, 30).Value = userId;
        //    cmd.Parameters.Add("pwd", MySqlDbType.VarChar, 30).Value = pwd;

        //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}

        #region 程序功能维护
        /// <summary>
        /// 添加程序
        /// </summary>
        /// <param name="proginfomodel"></param>
        public void AddProgInfo(string proginfomodel)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(proginfomodel);
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            if (!ChkProgId(mst["PROGID"].ToString()))
            {
                //cmd.CommandText = "insert into SFCB.B_PROG_INFO (progid,progname,progdesc) values(@progid,@progname,@progdesc)";
                //cmd.Parameters.Add("progid", MySqlDbType.VarChar, 25).Value = proginfomodel.progid;
                //cmd.Parameters.Add("progname", MySqlDbType.VarChar, 25).Value = proginfomodel.progname;
                //cmd.Parameters.Add("progdesc", MySqlDbType.VarChar, 25).Value = proginfomodel.progdesc;
                //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                dp.AddData("SFCB.B_PROG_INFO", mst);
            }
            else
            {
                //cmd.CommandText = "update SFCB.B_PROG_INFO set progname=@progname,progdesc=@progdesc where progid=@progid";
                //cmd.Parameters.Add("progid", MySqlDbType.VarChar, 25).Value = proginfomodel.progid;
                //cmd.Parameters.Add("progname", MySqlDbType.VarChar, 25).Value = proginfomodel.progname;
                //cmd.Parameters.Add("progdesc", MySqlDbType.VarChar, 25).Value = proginfomodel.progdesc;
                //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                dp.UpdateData("SFCB.B_PROG_INFO", new string[] {"PROGID" }, mst);
            }
            //MySqlCommand cmd = new MySqlCommand();
            //if (!ChkProgId(proginfomodel.progid))
            //{
            //    cmd.CommandText = "insert into SFCB.B_PROG_INFO (progid,progname,progdesc) values(@progid,@progname,@progdesc)";
            //    cmd.Parameters.Add("progid", MySqlDbType.VarChar, 25).Value = proginfomodel.progid;
            //    cmd.Parameters.Add("progname", MySqlDbType.VarChar, 25).Value = proginfomodel.progname;
            //    cmd.Parameters.Add("progdesc", MySqlDbType.VarChar, 25).Value = proginfomodel.progdesc;
            //     BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
            //}
            //else
            //{
            //    cmd.CommandText = "update SFCB.B_PROG_INFO set progname=@progname,progdesc=@progdesc where progid=@progid";
            //    cmd.Parameters.Add("progid", MySqlDbType.VarChar, 25).Value = proginfomodel.progid;
            //    cmd.Parameters.Add("progname", MySqlDbType.VarChar, 25).Value = proginfomodel.progname;
            //    cmd.Parameters.Add("progdesc", MySqlDbType.VarChar, 25).Value = proginfomodel.progdesc;
            //     BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
            //}
        }
        /// <summary>
        /// 添加功能
        /// </summary>
        /// <param name="lsfunlist"></param>
        public void AddFunctionList(string Lsdicstring)
        {
            IList<IDictionary<string, object>> LsDic = MapListConverter.JsonToListDictionary(Lsdicstring);
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            foreach (IDictionary<string, object> mst in LsDic)
            {
              //  MySqlCommand cmd = new MySqlCommand();
                if (!ChkFunctionList(mst["PROGID"].ToString(), mst["FUNID"].ToString()))
                {
                    //cmd.CommandText = "insert into SFCB.B_FUNCTION_LIST(funId,progid,funname,fundesc) values(@funId,@progid,@funname,@fundesc)";
                    //cmd.Parameters.Add("funId", MySqlDbType.VarChar, 25).Value = fls.funId;
                    //cmd.Parameters.Add("progid", MySqlDbType.VarChar, 25).Value = fls.progid;
                    //cmd.Parameters.Add("funname", MySqlDbType.VarChar, 25).Value = fls.funname;
                    //cmd.Parameters.Add("fundesc", MySqlDbType.VarChar, 25).Value = fls.fundesc;
                    //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

                    dp.AddData("SFCB.B_FUNCTION_LIST", mst);
                }
                else
                {
                    //cmd.CommandText = "update SFCB.B_FUNCTION_LIST set funname=@funname,fundesc=@fundesc where funid=@funid and progid=@progid";
                    //cmd.Parameters.Add("funId", MySqlDbType.VarChar, 25).Value = fls.funId;
                    //cmd.Parameters.Add("progid", MySqlDbType.VarChar, 25).Value = fls.progid;
                    //cmd.Parameters.Add("funname", MySqlDbType.VarChar, 25).Value = fls.funname;
                    //cmd.Parameters.Add("fundesc", MySqlDbType.VarChar, 25).Value = fls.fundesc;
                    //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                    dp.UpdateData("SFCB.B_FUNCTION_LIST", new string[] { "FUNID","PROGID" }, mst);
                }
            }

           // foreach (Entity.tFunctionList fls in lsfunlist)
          //  {
                //MySqlCommand cmd = new MySqlCommand();
                //if (!ChkFunctionList(fls.progid, fls.funId))
                //{
                //    cmd.CommandText = "insert into SFCB.B_FUNCTION_LIST(funId,progid,funname,fundesc) values(@funId,@progid,@funname,@fundesc)";
                //    cmd.Parameters.Add("funId", MySqlDbType.VarChar, 25).Value = fls.funId;
                //    cmd.Parameters.Add("progid", MySqlDbType.VarChar, 25).Value = fls.progid;
                //    cmd.Parameters.Add("funname", MySqlDbType.VarChar, 25).Value = fls.funname;
                //    cmd.Parameters.Add("fundesc", MySqlDbType.VarChar, 25).Value = fls.fundesc;
                //     BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                //}
                //else
                //{
                //    cmd.CommandText = "update SFCB.B_FUNCTION_LIST set funname=@funname,fundesc=@fundesc where funid=@funid and progid=@progid";
                //    cmd.Parameters.Add("funId", MySqlDbType.VarChar, 25).Value = fls.funId;
                //    cmd.Parameters.Add("progid", MySqlDbType.VarChar, 25).Value = fls.progid;
                //    cmd.Parameters.Add("funname", MySqlDbType.VarChar, 25).Value = fls.funname;
                //    cmd.Parameters.Add("fundesc", MySqlDbType.VarChar, 25).Value = fls.fundesc;
                //     BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                //}
           // }
        }
        /// <summary>
        /// 检查应用程序编号是否存在
        /// </summary>
        /// <param name="progid"></param>
        /// <returns></returns>
        public bool ChkProgId(string progid)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select progid from SFCB.B_PROG_INFO where progid=@progid";
            //cmd.Parameters.Add("progid", MySqlDbType.VarChar, 25).Value = progid;
            //object obj =  BLL.BllMsSqllib.Instance.sqlExecuteScalar(cmd);

            string table = "SFCB.B_PROG_INFO";
            string fieldlist = "progid";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("progid", progid);           
            DataTable dt = dp.GetData(table, fieldlist, mst, out count).Tables[0];

            if (dt.Rows.Count>0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 检查应用程序的功能是否存在
        /// </summary>
        /// <param name="progid"></param>
        /// <param name="funid"></param>
        /// <returns></returns>
        public bool ChkFunctionList(string progid, string funid)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select funname from SFCB.B_FUNCTION_LIST where funId=@funid and progid=@progid";
            //cmd.Parameters.Add("funid", MySqlDbType.VarChar, 25).Value = funid;
            //cmd.Parameters.Add("progid", MySqlDbType.VarChar, 25).Value = progid;
           // object obj = BLL.BllMsSqllib.Instance.sqlExecuteScalar(cmd);
            string table = "SFCB.B_FUNCTION_LIST";
            string fieldlist = "funname";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("funId", funid);
            mst.Add("progid", progid);
            DataTable dt= dp.GetData(table, fieldlist, mst, out count).Tables[0];
            if (dt.Rows.Count>0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 用户自己修改信息
        /// </summary>
        /// <param name="userinfo"></param>
        public void UpdateUserPassword(string dicuserinfo)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicuserinfo);
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            dp.UpdateData("SFCB.B_USER_INFO", new string[] { "USERID" }, mst);
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "update SFCB.B_USER_INFO set pwd=@pwd, userphone=@userphone, useremail=@useremail where userId=@userId";
            //cmd.Parameters.Add("userId", MySqlDbType.VarChar, 20).Value = userinfo.userId;
            //cmd.Parameters.Add("userphone", MySqlDbType.VarChar, 30).Value = userinfo.userphone;
            //cmd.Parameters.Add("useremail", MySqlDbType.VarChar, 50).Value = userinfo.useremail;
            //cmd.Parameters.Add("pwd", MySqlDbType.VarChar, 15).Value = userinfo.pwd;
            //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        }
        #endregion
        #endregion

        #region 测试权限
        /// <summary>
        /// 获取用户使用ATE测试的权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataSet GetUserAteEmp(string userId)
        {        
            string table = "sfcb.b_Ate_Emp".ToUpper();
            string fieldlist = "craftId,craftname".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("USERID", userId);
           return dp.GetData(table, fieldlist,  mst, out count);
        }
        /// <summary>
        /// 添加用户ATE权限
        /// </summary>
        /// <param name="lsAteEmp"></param>
        /// <returns></returns>
        public string AddUserAteEmp(string lsAteEmp)
        {
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
                IList<IDictionary<string, object>> LsDic = MapListConverter.JsonToListDictionary(lsAteEmp);           

                string table = "sfcb.b_Ate_Emp".ToUpper();
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("USERID", LsDic[0]["USERID"]);
                dp.DeleteData(tx, table, dic);
                dp.AddListData(tx, table, LsDic);

                tx.Commit();

                return "OK";
            }
            catch (Exception ex)
            {
                tx.Rollback();
                return ex.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 检查是否具有ate权限
        /// </summary>
        /// <param name="ateemp"></param>
        /// <returns></returns>
        public string ChkUserAteEmp(string UserId,string CraftId)
        {            
            int count = 0;
            string table = "sfcb.b_Ate_Emp";
            string fieldlist = "craftId";
            string filter = "userId={0} and ((craftId={1}) OR (craftId='ALL'))";
             
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("userId", UserId);
            mst.Add("craftId", CraftId);
           DataTable dt= TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
                if (dt == null || dt.Rows.Count < 1)
                    return "No Emp";
                else
                    return "OK";
          
        }
        #endregion
    }
}
