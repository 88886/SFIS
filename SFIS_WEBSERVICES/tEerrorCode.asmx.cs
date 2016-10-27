using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using GenericUtil;
using System.Xml.Serialization;

//using Microsoft.Web.Administration;


namespace TestWeserver
{

    /// <summary>
    /// tEerrorCode 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tEerrorCode : System.Web.Services.WebService
    {
        BLL.tErrorCode mErrcode = new BLL.tErrorCode();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public byte[] GetErrorCode()
        {
            return mlc.GetDataSetSurrogateZipBytes(mErrcode.GetErrorCode(null));
        }
        [WebMethod]
        public  void InsertErrorCode(string dicstring)
        {
            mErrcode.InsertErrorCode(dicstring);
        }
        [WebMethod]
        public void UpdateErrorCode(string dicstring)
        {
            mErrcode.UpdateErrorCode(dicstring);
        }
        [WebMethod]
        public void DeleteErrorCode(string errorcode)
        {
            mErrcode.DeleteErrorCode(errorcode);
        }
        [WebMethod]
        public List<string> GetErrorCodeDesc(string EC)
        {
            return mErrcode.GetErrorCodeDesc(EC);
        }

        ///// <summary>
        ///// 应用程序池回收
        ///// </summary>
        ///// <param name="PoolName">应用程序池名称</param>
        ///// <returns>返回状态描述</returns>
        //[WebMethod]
        //public string ApplicationPoolsRecycle(string PoolName)
        //{
        //    try
        //    {
        //        string msg = string.Empty;
        //        using (ServerManager serMan = new ServerManager())
        //        {
        //            switch (serMan.ApplicationPools[PoolName].State)
        //            {
        //                case ObjectState.Unknown:
        //                    serMan.ApplicationPools[PoolName].Start();
        //                    msg = "Start OK";
        //                    break;
        //                case ObjectState.Started:
        //                    serMan.ApplicationPools[PoolName].Recycle();
        //                    msg = "Recycle OK";
        //                    break;
        //                case ObjectState.Starting:
        //                    msg = "Starting";
        //                    break;
        //                case ObjectState.Stopped:
        //                    serMan.ApplicationPools[PoolName].Start();
        //                    msg = "Start OK";
        //                    break;
        //                case ObjectState.Stopping:
        //                    msg = "Stoping";
        //                    break;
        //                default:
        //                    msg = "Unknown";
        //                    break;
        //            }
        //        }
        //        return msg;
        //    }
        //    catch(Exception ex)
        //    {
        //        return ex.Message;// "ApplicationPools Error";
        //    }
        //}

         

    }
}
