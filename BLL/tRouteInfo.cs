using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SystemObject;
using System.Data.Common;
using SrvComponent;

namespace BLL
{
    public partial class tRouteInfo
    {
     
        public tRouteInfo()
        {
           
        }

        #region 途程
        /// <summary>
        /// 添加途程信息
        /// </summary>
        /// <param name="routeinfo"></param>
        /// <param name="err"></param>
        /// <returns>返回途程编号</returns>
        public string InsertRouteInfo(Dictionary<string,object> dic)
        {           
            try
            {
            //    MySqlCommand cmd = new MySqlCommand();
            //    cmd.CommandText = " INSERT INTO SFCB.B_ROUTE_INFO   (ROUTGROUPID, CRAFTID, NEXTROUTEID, SEQ, STATION_FLAG, ROUTEDESC,CRAFTNAME,NEXTCRAFTNAME)" +
            //                      " VALUES   (@ROUTGROUPID,@CRAFTID, @NEXTROUTEID, @SEQ, @STATION_FLAG, @ROUTEDESC,@CRTANME,@NEXTCRTNAME)";
            //    cmd.Parameters.AddRange(new MySqlParameter[]
            //{
            //    new MySqlParameter("ROUTGROUPID",MySqlDbType.VarChar){Value=routeinfo.routgroupId},
            //    new MySqlParameter("CRAFTID",MySqlDbType.VarChar){Value=routeinfo.craftId},
            //    new MySqlParameter("NEXTROUTEID",MySqlDbType.VarChar){Value=routeinfo.nextrouteId},
            //    new MySqlParameter("SEQ",MySqlDbType.Int32){Value=routeinfo.seq},
            //    new MySqlParameter("STATION_FLAG",MySqlDbType.Int32){Value=routeinfo.station_flag},
            //    new MySqlParameter("ROUTEDESC",MySqlDbType.VarChar){Value=routeinfo.routedesc},
            //    new MySqlParameter("CRTANME",MySqlDbType.VarChar){Value=routeinfo.CraftName},
            //    new MySqlParameter("NEXTCRTNAME",MySqlDbType.VarChar){Value=routeinfo.NextCtaftName}
            //});
            //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);


                Dictionary<string, object> mst = new Dictionary<string, object>();
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);                 
                mst.Add("ROUTGROUPID", dic["ROUTGROUPID"]);
                mst.Add("CRAFTID", dic["CRAFTID"]);
                mst.Add("NEXTROUTEID", dic["NEXTROUTEID"]);
                mst.Add("SEQ", dic["SEQ"]);
                mst.Add("STATION_FLAG", dic["STATION_FLAG"]);
                mst.Add("ROUTEDESC", dic["ROUTEDESC"]);
                mst.Add("CRAFTNAME", dic["CRAFTNAME"]);
                mst.Add("NEXTCRAFTNAME", dic["NEXTCRAFTNAME"]);                   
                dp.AddData("SFCB.B_ROUTE_INFO", mst);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取途程信息(根据途程组编号)
        /// </summary>
        /// <param name="woId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetRouteInfoByRoutgroupId(string routgroupId)
        {          

            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "SELECT CRAFTID,NEXTROUTEID,SEQ,STATION_FLAG,ROUTEDESC,CRAFTNAME FROM SFCB.B_ROUTE_INFO  WHERE ROUTGROUPID =@ROUTEID AND STATION_FLAG=0 ";
            //cmd.Parameters.AddRange(new MySqlParameter[]
            //{
            //    new MySqlParameter("ROUTEID",MySqlDbType.VarChar){Value=routgroupId}
            //});
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCB.B_ROUTE_INFO";
            string fieldlist = "CRAFTID,NEXTROUTEID,SEQ,STATION_FLAG,ROUTEDESC,CRAFTNAME";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ROUTGROUPID", routgroupId);
            return dp.GetData(table, fieldlist, mst, out count);

        }

        /// <summary>
        /// 获取所有的途程信息（routgroupId,craftId,nextrouteId,seq,station_flag,routedesc）
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetAllRouteInfo(string routgroupId)
        {
            //string sSQL = "select routgroupId,craftid,craftname,nextcraftname,seq,station_flag,routedesc from SFCB.B_ROUTE_INFO ";
            //MySqlCommand cmd = new MySqlCommand();

            //if (!string.IsNullOrEmpty(routgroupId))
            //{
            //    sSQL += " WHERE ROUTGROUPID =@ROUTEID ";
            //    cmd.Parameters.AddRange(new MySqlParameter[]
            //    {
            //        new MySqlParameter("ROUTEID",MySqlDbType.VarChar){Value=routgroupId}
            //    });
            //}
            //cmd.CommandText = sSQL;
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            string table = "SFCB.B_ROUTE_INFO";
            string fieldlist = "routgroupId,craftid,craftname,nextcraftname,seq,station_flag,routedesc".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(routgroupId))
            {
                mst.Add("ROUTGROUPID", routgroupId);
            }
            return dp.GetData(table, fieldlist, mst, out count);
        }

        public string CHECK_ROUTE_INFO(string C_ERRFLAG, string C_ROUTECODE, string C_LOCGROUP, string MYGROUP) //2014/12/30 存储过程翻译使用
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = " SELECT NEXTCRAFTNAME FROM SFCB.B_ROUTE_INFO  WHERE STATION_FLAG = @C_ERRFLAG AND ROUTGROUPID = @C_ROUTECODE AND CRAFTNAME = @C_LOCGROUP AND NEXTCRAFTNAME = @MYGROUP   LIMIT 1";
            //cmd.Parameters.AddRange(new MySqlParameter[]
            //    {
            //        new MySqlParameter("C_ERRFLAG",MySqlDbType.VarChar){Value=C_ERRFLAG},
            //         new MySqlParameter("C_ROUTECODE",MySqlDbType.VarChar){Value=C_ROUTECODE},
            //              new MySqlParameter("C_LOCGROUP",MySqlDbType.VarChar){Value=C_LOCGROUP},
            //                   new MySqlParameter("MYGROUP",MySqlDbType.VarChar){Value=MYGROUP}
            //    });

            //DataTable dt = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
            string fieldlist = "NEXTCRAFTNAME";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("STATION_FLAG", C_ERRFLAG);
            mst.Add("ROUTGROUPID", C_ROUTECODE);
            mst.Add("CRAFTNAME", C_LOCGROUP);
            mst.Add("NEXTCRAFTNAME", MYGROUP);
            DataTable dt = dp.GetData("SFCB.B_ROUTE_INFO", fieldlist, mst, out count).Tables[0];
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["NEXTCRAFTNAME"].ToString();
            else
                return null;

        }
        public string CHECK_ROUTE_INFO(string C_ROUTECODE, string C_LOCGROUP, string C_ERRFLAG) //2014/12/30 存储过程翻译使用
        {            
            int count = 0;
            string table = "SFCB.B_ROUTE_INFO";
            string fieldlist = "NEXTCRAFTNAME";
            string filter = "ROUTGROUPID ={0} AND SEQ = (SELECT MAX(SEQ) FROM SFCB.B_ROUTE_INFO WHERE CRAFTNAME = {1} AND ROUTGROUPID = {0} AND STATION_FLAG = {2}) AND NEXTCRAFTNAME <> 'NA' AND STATION_FLAG = {2}";
            
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ROUTGROUPID", C_ROUTECODE);
            mst.Add("CRAFTNAME", C_LOCGROUP);
            mst.Add("STATION_FLAG", C_ERRFLAG);
            DataTable dt = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];


            if (dt.Rows.Count > 0)
                return dt.Rows[0]["NEXTCRAFTNAME"].ToString();
            else
                return null;

        }
        public string CHECK_ROUTE_INFO_RE(string C_ROUTECODE, string C_LOCGROUP, string C_ERRFLAG) //2014/12/30 存储过程翻译使用
        {           
            int count = 0;
            string table = "SFCB.B_ROUTE_INFO";
            string fieldlist = "NEXTCRAFTNAME";
            string filter = "ROUTGROUPID ={0} AND SEQ = (SELECT MAX(SEQ) FROM SFCB.B_ROUTE_INFO WHERE CRAFTNAME = {1} AND ROUTGROUPID = {0} AND STATION_FLAG = {2}) AND NEXTCRAFTNAME <> 'NA' ";

            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ROUTGROUPID", C_ROUTECODE);
            mst.Add("CRAFTNAME", C_LOCGROUP);
            mst.Add("STATION_FLAG", C_ERRFLAG);
            DataTable dt = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["NEXTCRAFTNAME"].ToString();
            else
                return null;

        }
        public string CHECK_ROUTE_NEXTCRAFTNAME(string C_ERRFLAG, string C_ROUTECODE, string C_LOCGROUP) //2015/1/7 存储过程翻译使用 
        {          

            string table = "SFCB.B_ROUTE_INFO";
            string fieldlist = "NEXTCRAFTNAME";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ROUTGROUPID", C_ROUTECODE);
            mst.Add("CRAFTNAME", C_LOCGROUP);
            mst.Add("STATION_FLAG", C_ERRFLAG);         
            DataTable dt = dp.GetData(table, fieldlist, mst, out count).Tables[0];
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["NEXTCRAFTNAME"].ToString();
            else
                return null;

        }

     
        public System.Data.DataSet GetRouteStartAndEnd(string partnumber)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select distinct  routgroupId,craftId,routedesc from SFCB.B_ROUTE_INFO  where routgroupId like @partnumber+'%' and routedesc in('IN','OUT')";
            //cmd.Parameters.Add("partnumber", MySqlDbType.VarChar, 25).Value = partnumber;
            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            int count = 0;
            string table = "SFCB.B_ROUTE_INFO";
            string fieldlist = "distinct  routgroupId,craftId,routedesc".ToUpper();
            string filter = "routgroupId like {0} and routedesc in('IN','OUT')";             
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("routgroupId", partnumber+"%");
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }
        #endregion


        #region 途程工艺参数
        ///// <summary>
        ///// 添加途程工艺参数
        ///// </summary>
        ///// <param name="routcp"></param>
        ///// <param name="err"></param>
        //public string InsertRouteCraftParamerter(Entity.tRoutCraftparameter routcp)
        //{
        //    return  BLL.BllMsSqllib.Instance.InsertRouteCraftParamerter(routcp);
        //}
        /// <summary>
        /// 获取途程工艺参数信息
        /// </summary>
        /// <param name="woId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetRouteCraftParameterByWoId(string routgroupId)
        {
           // return  BLL.BllMsSqllib.Instance.GetRouteCraftParameterByWoId(routgroupId);
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "SELECT CRAFTID,CRAFTITEM,CRAFTPARAMETERDES,UPPERLIMIT,LOWERLIMIT,OTHER FROM SFCB.B_ROUTE_CRAFT_PARAMETER  WHERE ROUTGROUPID = @RID";
            //cmd.Parameters.AddRange(new MySqlParameter[]{
            //       new MySqlParameter("RID",MySqlDbType.VarChar){Value=routgroupId}                    
            //    });
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCB.B_ROUTE_CRAFT_PARAMETER";
            string fieldlist = "CRAFTID,CRAFTITEM,CRAFTPARAMETERDES,UPPERLIMIT,LOWERLIMIT,OTHER";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ROUTGROUPID", routgroupId);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        #endregion

        #region 途程内容
        /// <summary>
        /// 添加流程编号和对应的Xml内容
        /// </summary>
        /// <param name="routeatt"></param>
        /// <returns></returns>
        //public string InsertRouteAtt(Entity.tRouteAtt routeatt)
        //{
        //    return  BLL.BllMsSqllib.Instance.InsertRouteAtt(routeatt);
        //}
        /// <summary>
        /// 获取所有流程列表
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetAllRouteAtt()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select  routgroupId,routgroupdesc from SFCB.B_ROUTE_ATT ";
            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            string table = "SFCB.B_ROUTE_ATT";
            string fieldlist = "routgroupId,routgroupdesc".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            return dp.GetData(table, fieldlist, null, out count);
        }
        /// <summary>
        /// 获取流程编号的名称
        /// </summary>
        /// <param name="routgroupid"></param>
        /// <returns></returns>
        public string GetAttRouteDesc(string routgroupid)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select routgroupdesc from SFCB.B_ROUTE_ATT where routgroupId=@routgroupId";
            //cmd.Parameters.Add("routgroupId", MySqlDbType.VarChar, 20).Value = routgroupid;
            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0].Rows[0]["routgroupdesc"].ToString();

            string table = "SFCB.B_ROUTE_ATT";
            string fieldlist = "routgroupdesc".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ROUTGROUPID", routgroupid);
            return dp.GetData(table, fieldlist, mst, out count).Tables[0].Rows[0]["routgroupdesc"].ToString();
        }

        public System.Data.DataSet GetAllRouteAtt(string routgroupId)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "SELECT ROUTGROUPID,ROUTGROUPDESC, ROUTGROUPXMLCONTENT FROM SFCB.B_ROUTE_ATT WHERE ROUTGROUPID=@V_ROUTEGROUPID ";
            //cmd.Parameters.Add("V_ROUTEGROUPID", MySqlDbType.VarChar).Value = routgroupId;
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCB.B_ROUTE_ATT";
            string fieldlist = "ROUTGROUPID,ROUTGROUPDESC, ROUTGROUPXMLCONTENT";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ROUTGROUPID", routgroupId);
            return dp.GetData(table, fieldlist, mst, out count);

        }
        /// <summary>
        /// 获取指定流程编号的XML内容
        /// </summary>
        /// <param name="routgroupId"></param>
        /// <returns></returns>
        public string GetRouteAttBy(string routgroupId)
        {          

           // MySqlCommand cmd = new MySqlCommand();
           // cmd.CommandText = "select routgroupxmlContent from SFCB.B_ROUTE_ATT where routgroupId=@routeId";
           // cmd.Parameters.Add("routeId", MySqlDbType.VarChar).Value = routgroupId;
           //// ((Oracle.DataAccess.Client.MySqlCommand)cmd).InitialLONGFetchSize = -1;
           // DataTable dt =  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];

            string table = "SFCB.B_ROUTE_ATT";
            string fieldlist = "ROUTGROUPXMLCONTENT";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ROUTGROUPID", routgroupId);
            DataTable dt = dp.GetData(table, fieldlist, mst, out count).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                //return new tRouteAtt()
                //{
                //    routgroupId = routgroupId,
                 //   routgroupxmlContent =
                return dt.Rows[0]["routgroupxmlContent"].ToString();
                //};
            }
            else
                return null;
        }
        #endregion

        #region 完整的整合
        /// <summary>
        /// 整合了的流程添加方法
        /// </summary>
        /// <param name="routeatt"></param>
        /// <returns></returns>
        //public string InsertRouteAllItme(Entity.tRouteAtt routeatt)
        //{
        //    string _StrErr=InsertRouteAtt(routeatt);
        //    if (_StrErr!="OK")
        //        return "流程XML添加失败" + _StrErr;
        //    foreach (Entity.tRouteInfo routeinfo in routeatt.LsRouteInfo)
        //    {
        //        if (!string.IsNullOrEmpty(InsertRouteInfo(routeinfo)))
        //            return "流程添加失败";

        //        foreach (Entity.tRoutCraftparameter obj in routeinfo.LsRouteCraftparameter)
        //        {
        //            if (InsertRouteCraftParamerter(obj)!="OK")
        //                return "流程工艺及工艺项目添加失败";
        //        }
        //    }
        //    return null;
        //}
        #endregion
        #region

        public string GetRouteCode()
        {
            string num = "0";
            for (int x = 100; x < 1000; x++)
            {
                //MySqlCommand cmd = new MySqlCommand();
                //cmd.CommandText = "select COUNT(1) from SFCB.B_ROUTE_ATT WHERE ROUTGROUPID=@code";
                //cmd.Parameters.Add("code", MySqlDbType.VarChar).Value = x.ToString();
                //num = x.ToString();
                //if (int.Parse(BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0].Rows[0][0].ToString()) == 0)                
                //    break;

                string table = "SFCB.B_ROUTE_ATT";
                string fieldlist = "COUNT(1)";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                Dictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("ROUTGROUPID", x.ToString());
                num = x.ToString();
                if (int.Parse(dp.GetData(table, fieldlist, mst, out count).Tables[0].Rows[0][0].ToString()) == 0)
                    break;
                
            }
            return num;
        }

        #endregion
        #region 流程管理
        ///// <summary>
        ///// 获取全部的流程对应的成品料号信息
        ///// </summary>
        ///// <returns></returns>
        //public System.Data.DataSet GetRouteManage()
        //{
        //    //MySqlCommand cmd = new MySqlCommand();
        //    //cmd.CommandText = "select routgroupId,partnumber,productname,bomnumber from SFCB.B_ROUTE_MANAGE";
        //    //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

        //    string table = "SFCB.B_ROUTE_MANAGE";
        //    string fieldlist = "routgroupId,partnumber,productname,bomnumber";
        //    int count = 0;
        //    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
        //    return dp.GetData(table, fieldlist, null, out count);
        //}
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        public DataSet GetRouteManage(string JsonQuery)
        {
            
            string table = "SFCB.B_ROUTE_MANAGE";
            string fieldlist = "routgroupId,partnumber,productname,bomnumber".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(JsonQuery))
            {
                mst= MapListConverter.JsonToDictionary(JsonQuery);   
            }
            return dp.GetData(table, fieldlist, mst, out count);
        }

        public void InsertRouteManage(string dicroutinfo)
        {       
            int count=0;
            string table="SFCB.B_ROUTE_MANAGE";
              IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicroutinfo);
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PARTNUMBER", mst["PARTNUMBER"]);
            if (dp.GetData(table, "PARTNUMBER", dic,out count).Tables[0].Rows.Count == 0)          
               dp.AddData("SFCB.B_ROUTE_MANAGE", mst);
            else
                dp.UpdateData("SFCB.B_ROUTE_MANAGE", new string[] { "PARTNUMBER" }, mst);
        }

        /// <summary>
        /// 由料号查询流程Id与BOM编号
        /// </summary>
        /// <param name="partnumber"></param>
        /// <returns></returns>
        public DataSet GetRouteManageByPartnumber(string partnumber)
        {
            int count = 0;
            string table = "SFCB.B_ROUTE_MANAGE a,SFCB.B_ROUTE_INFO b";
            string fieldlist = "distinct b.routgroupId,b.craftname,b.routedesc,a.bomnumber";
            string filter = "a.routgroupId=b.routgroupId and a.partnumber={0} and b.routedesc in('IN','OUT')";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("partnumber", partnumber);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

        }
        /// <summary>
        /// 由流程Id获取开始和结束流程
        /// </summary>
        /// <param name="routgroupId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetRouteStartAndEndByroutgroupId(string routgroupId)
        {      
            int count = 0;
            string table = "SFCB.B_ROUTE_INFO";
            string fieldlist = "distinct  routgroupId,craftId,routedesc";
            string filter = "routgroupId = @routgroupId and routedesc in('IN','OUT')";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("routgroupId", routgroupId);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }
        #endregion

        public System.Data.DataSet Get_Route_Info(string routgroupId)
        {         

            string table = "SFCB.B_ROUTE_INFO";
            string fieldlist = "ROUTGROUPID,SEQ,STATION_FLAG,ROUTEDESC,CRAFTNAME,NEXTCRAFTNAME";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(routgroupId))
                dic.Add("ROUTGROUPID", routgroupId);
            return dp.GetData(table, fieldlist, dic, out count);
        }
        public string Delete_Route_Info(string routgroupId)
        {
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("ROUTGROUPID", routgroupId);             
                dp.DeleteData(tx,"SFCB.B_ROUTE_INFO",  mst);               
                dp.DeleteData(tx, "SFCB.B_ROUTE_ATT", mst);  

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
    }
}
