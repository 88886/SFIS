using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Data;
using System.Xml.Serialization;
using GenericUtil;

namespace TestWeserver
{
    /// <summary>
    /// tWoBomInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tWoBomInfo : System.Web.Services.WebService
    {
        BLL.tWoBomInfo mWobominfo = new BLL.tWoBomInfo();
        MapListConverter mlc = new MapListConverter();
        /// <summary>
        /// 获取已经导入的工单用料信息
        /// </summary>
        /// <param name="woId"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] GetWoBomInfo(string woId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWobominfo.GetWoBomInfo(woId));
        }
        [WebMethod]
        public byte[] GetWoBomInfo_WinCE(string woId)
        {
            return mlc.GetDataSetZipBytes(mWobominfo.GetWoBomInfo(woId));
        }
        /// <summary>
        /// 增加工单用料信息
        /// </summary>
        /// <param name="twbi"></param>
        /// <param name="Err"></param>
        [WebMethod]
        public void InsertWoBomInfo(string dicstring, out string Err)
        {
            mWobominfo.InsertWoBomInfo(dicstring, out Err);
        }
        [WebMethod]
        public string InsertWoBomInfoList(string  diclstwbom)
        {
            return mWobominfo.InsertWoBomInfoList(diclstwbom);
        }

        /// <summary>
        /// 新增工单上料打印料表信息
        /// </summary>
        /// <param name="mp"></param>
        /// <param name="Err"></param>
        [WebMethod]
        public string InsertWoBomPrintInfos(string lsdicstring)
        {
            return mWobominfo.InsertWoBomPrintInfo(lsdicstring);
        }

        //[WebMethod]
        //public void InsertWoBomPrintInfo(Entity.tMaterialPreparation mp, out string Err)
        //{
        //    mWobominfo.InsertWoBomPrintInfo(mp, out Err);
        //}

        [WebMethod]
        public byte[] GetMaterialPreparation(string woId, string masterId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWobominfo.GetMaterialPreparation(woId, masterId));
        }
        [WebMethod]
        public byte[] QueryWoBomInfoData(string WO)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWobominfo.QueryWoBomInfoData(WO));
        }
        [WebMethod]
        public void InserWoBomData(string dicstring)
        {
            mWobominfo.InserWoBomData(dicstring);
        }

         
    }
}
