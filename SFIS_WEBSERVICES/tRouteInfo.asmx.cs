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
    /// tRouteInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tRouteInfo : System.Web.Services.WebService
    {
        BLL.tRouteInfo mRouteinfo = new BLL.tRouteInfo();
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();
        MapListConverter mlc = new MapListConverter();
        #region 途程
        /// <summary>
        /// 添加途程信息
        /// </summary>
        /// <param name="routeinfo"></param>
        /// <param name="err"></param>
        //[WebMethod]
        //public string InsertRouteInfo(string  dicrouteinfo)
        //{
        //    return mRouteinfo.InsertRouteInfo(dicrouteinfo);
        //}

        /// <summary>
        /// 获取途程信息
        /// </summary>
        /// <param name="woId"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetRouteInfoByWoId(string routgroupId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRouteinfo.GetRouteInfoByRoutgroupId(routgroupId));// BLL.BllMsSqllib.Instance.MsSqlLib.GetRouteByWoId(woId);
        }

        /// <summary>
        /// 获取所有的途程信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetAllRouteInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mRouteinfo.GetAllRouteInfo(null));
        }
        #endregion


        #region 途程工艺参数
        /// <summary>
        /// 添加途程工艺参数
        /// </summary>
        /// <param name="routcp"></param>
        /// <param name="err"></param>
        //[WebMethod]
        //public string InsertRouteCraftParamerter(Entity.tRoutCraftparameter routcp)
        //{
        //    return mPro.PRO_INSERTROUTECRAFTPARAMERTER(routcp.routgroupId, routcp.craftId, routcp.craftItem, routcp.craftparameterdes, routcp.upperlimit, routcp.lowerlimit, routcp.other, routcp.url);
                  
        //        //mRouteinfo.InsertRouteCraftParamerter(routcp);// BLL.BllMsSqllib.Instance.MsSqlLib.InsertRouteCraftParamerter(routcp, out err);
        //}
        /// <summary>
        /// 获取途程工艺参数信息
        /// </summary>
        /// <param name="woId"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetRouteCraftParameterByWoId(string woId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRouteinfo.GetRouteCraftParameterByWoId(woId));// BLL.BllMsSqllib.Instance.MsSqlLib.GetRouteCraftParameterByWoId(woId);
        }
        [WebMethod]
        public  string InsertRouteAllItme(string  dicrouteatt)
        {     
            string _Sys_StrErr = string.Empty;
            try
            {
                IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicrouteatt);
                _Sys_StrErr = "Save PRO_INSERTROUTEATT Error";
                string _StrErr = mPro.PRO_INSERTROUTEATT(dic["ROUTGROUPID"].ToString(), dic["ROUTGROUPDESC"].ToString(), dic["ROUTGROUPXMLCONTENT"].ToString());//  .InsertRouteAtt(routeatt);
                if (_StrErr != "OK")
                    return "流程XML添加失败" + _StrErr;
                
                _Sys_StrErr = "dic[LSROUTE] Error";
                 
                    foreach (Dictionary<string, object> mst in MapListConverter.JsonToListDictionary(dic["LSROUTE"].ToString()))
                    {
                         _Sys_StrErr = "InsertRouteInfo Error";
                      
                        if (!string.IsNullOrEmpty(mRouteinfo.InsertRouteInfo(mst)))
                            return "流程添加失败";
                      
                        _Sys_StrErr = "InsertRouteCraftParamerter Analysis of failure";

                        if (mst["LsRouteCraftparameter".ToUpper()] != null)
                        {
                            _Sys_StrErr = " Analysis of failure";
                            foreach (Dictionary<string, object> obj in MapListConverter.JsonToListDictionary(mst["LsRouteCraftparameter".ToUpper()].ToString())) //mst["LsRouteCraftparameter".ToUpper()] as List<Dictionary<string, object>>)
                            {
                                _Sys_StrErr = "InsertRouteCraftParamerter failed";
                                if (mPro.PRO_INSERTROUTECRAFTPARAMERTER(obj["ROUTGROUPID"].ToString(), obj["CRAFTID"].ToString(), Convert.ToInt32(obj["CRAFTITEM"].ToString()), obj["CRAFTPARAMETERDES"].ToString(), obj["UPPERLIMIT"].ToString(), obj["LOWERLIMIT"].ToString(), obj["OTHER"].ToString(), obj["URL"].ToString()) != "OK")
                                    return "流程工艺及工艺项目添加失败";
                            }
                        }
                    }
                 
                return "OK";
            }
            catch (Exception ex)
            {
                return _Sys_StrErr+"\r\n"+ex.Message;
            }
        }
        [WebMethod]
        public  byte[] GetAllRouteAtt()
        {
            return mlc.GetDataSetSurrogateZipBytes(mRouteinfo.GetAllRouteAtt());
        }
        /// <summary>
        /// 获取指定流程编号的XML内容
        /// </summary>
        /// <param name="routgroupId"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetRouteAttBy(string routgroupId)
        {
            return mRouteinfo.GetRouteAttBy(routgroupId);
        }
                /// <summary>
        /// 获取流程编号的名称
        /// </summary>
        /// <param name="routgroupid"></param>
        /// <returns></returns>
        [WebMethod]
        public  string GetAttRouteDesc(string routgroupid)
        {
            return mRouteinfo.GetAttRouteDesc(routgroupid);
        }
        #endregion       

        [WebMethod]
        public byte[] GetRouteStartAndEnd(string partnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRouteinfo.GetRouteStartAndEnd(partnumber));
        }
        [WebMethod]
        public string GetRouteCode()
        {
            return mRouteinfo.GetRouteCode();
        }

        #region 途程管理

        [WebMethod]
        public byte[] GetRouteManage(string JsonQuery)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRouteinfo.GetRouteManage(JsonQuery));
        }

        //[WebMethod]
        //public byte[] GetRouteManageById(string JsonQuery)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(mRouteinfo.GetRouteManageById(JsonQuery));

        //}

        [WebMethod]
        public void InsertRouteManage(string dicroutinfo)
        {
            mRouteinfo.InsertRouteManage(dicroutinfo);
        }

        [WebMethod]
        public byte[] GetRouteManageByPartnumber(string partnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRouteinfo.GetRouteManageByPartnumber(partnumber));
        }

        [WebMethod]
        public byte[] GetRouteStartAndEndByroutgroupId(string routgroupId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mRouteinfo.GetRouteStartAndEndByroutgroupId(routgroupId));
        }
        #endregion

         [WebMethod]
        public byte[] Get_Route_Info(string routgroupId)
        {
         return  mlc.GetDataSetSurrogateZipBytes( mRouteinfo.Get_Route_Info(routgroupId));
        }
         [WebMethod]
         public string Delete_Route_Info(string RouteGroupId)
         {
             return mRouteinfo.Delete_Route_Info(RouteGroupId);
         }

    }
}
