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
    /// ExcelToDb 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ExcelToDb : System.Web.Services.WebService
    {
        BLL.ExcelToDb exceltodb = new BLL.ExcelToDb();
        MapListConverter mlc = new MapListConverter();
        /// <summary>
        /// 添加料站表数据
        /// </summary>
        /// <param name="kpmaster">料站表头数据</param>
        /// <param name="lskpdetalt">料站表身数据列表</param>
        [WebMethod]
        public void InsertMaterTable(string kpmaster, string lskpdetalt)
        {
            exceltodb.InsertMaterTable(kpmaster, lskpdetalt);
        }

        /// <summary>
        /// 获取指定产品所有备料的机器列表
        /// </summary>
        [WebMethod]
        public  List<string> GetMachineIdList(string partnumber)
        {
            return exceltodb.GetMachineIdList(partnumber);
        }
        

        /// <summary>
        /// 获取料站表头的一行信息
        /// </summary>
        /// <param name="partnumber">成品料号</param>
        /// <param name="machineId">机器编号</param>
        /// <param name="side">pcb面</param>
        /// <returns></returns>
        [WebMethod]
        public  byte[] GetKpMasterInfo(string partnumber, string machineId, string side)
        {
            return mlc.GetDataSetSurrogateZipBytes(exceltodb.GetKpMasterInfo(partnumber, machineId, side));
        }

        ///// <summary>
        ///// 根据料站表头Id获取详细的料站信息
        ///// </summary>
        ///// <param name="masterId"></param>
        ///// <param name="Err"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public  byte[] GetSmtKpDetaltByMasterId(string masterId, out string Err)
        //{
        //    return mlc.GetDataSetSurrogateZipBytes(exceltodb.GetSmtKpDetaltByMasterId(masterId, out Err));
        //}
        [WebMethod]
        public byte[]  GetSmtKpDetaltByMasterIdNew(string masterId, string woId, out string Err)
        {
            return mlc.GetDataSetSurrogateZipBytes(exceltodb.GetSmtKpDetaltByMasterIdNew(masterId, woId, out Err));
        }
        [WebMethod]
        public string DeleteSmtKPDetaltForWo(string woId, string masterId, string stationno, string kpnumber)
        {
            return exceltodb.DeleteSmtKPDetaltForWo(woId, masterId, stationno, kpnumber);
        }

         
    }
}
