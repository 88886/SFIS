using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tMachineInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tMachineInfo : System.Web.Services.WebService
    {
        BLL.tMachineId mMachineid = new BLL.tMachineId();
        MapListConverter mlc = new MapListConverter();
        /// <summary>
        /// 获取所有机器/工站信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public  byte[] GetAllMachineInfo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mMachineid.GetMachineInfo(null));
        }
        [WebMethod]
        public  byte[] GetMachineInfoByMachineid(string id)
        {
            return mlc.GetDataSetSurrogateZipBytes(mMachineid.GetMachineInfo(id));
        }

        /// <summary>
        /// 新增机器/工站信息
        /// </summary>
        /// <param name="machineinfo"></param>
        [WebMethod]
        public  void InsertMachineInfo(string dicMachineinfo)
        {
            mMachineid.InsertMachineInfo(dicMachineinfo);
        }

        /// <summary>
        /// 修改机器/工站信息
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="machineInfo"></param>
        [WebMethod]
        public void EditMachineInfo(string dicMachineinfo)
        {
            mMachineid.EditMachineInfo(dicMachineinfo);
        }

        /// <summary>
        /// 删除机器/工站信息
        /// </summary>
        /// <param name="machineId"></param>
        [WebMethod]
        public  void DeleteMachineInfo(string machineId)
        {
            mMachineid.DeleteMachineInfo(machineId);
        }

        [WebMethod]
        public  List<string> GetSMTLineList()
        {
            return mMachineid.GetSMTLineList();
        }

      

    }
}
