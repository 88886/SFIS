using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Data;
using GenericUtil;
using System.Collections.Generic;


namespace TestWeserver
{
    /// <summary>
    /// tSmtKpMaster 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tSmtKpMaster : System.Web.Services.WebService
    {
        BLL.tSmtKPMaster mSmtkpmaster = new BLL.tSmtKPMaster();
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();
        MapListConverter mlc = new MapListConverter();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public byte[] GetSmtKpMaster(string MasterId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmaster.GetSmtKpMaster(MasterId));
        }

        [WebMethod]
        public byte[] GetAllMasterid()
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmaster.GetAllMasterid());
        }

        [WebMethod]
        public  void DeleteBomKpMaster(string KpMaster)
        {
            mSmtkpmaster.DeleteSmtKPDetalt(KpMaster);
            mSmtkpmaster.DeleteSmtKpMaster(KpMaster);
        }
        [WebMethod]
        public string DeleteSmtKpMaster(List<string> LsMachine, string PARTNUMBER, string PCBASIDE)
        {
           return mSmtkpmaster.DeleteSmtKpMaster(LsMachine, PARTNUMBER, PCBASIDE);
        }

        [WebMethod]
        public byte[] GetMachineIdAndMasterIdListByPartnumber(string partnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmaster.GetMachineIdAndMasterIdListByPartnumber(partnumber));
        }
        [WebMethod]
        public string InsertSmtKpDetaltForWo(string Dicdetaltforwo)
        {
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(Dicdetaltforwo); 
           string _StrErr = string.Empty;
           //  mPro.PRO_INSERTSMTKPDETALTFORWO(detaltforwo.MasterId, detaltforwo.WoId, detaltforwo.UserId, detaltforwo.Stationno, detaltforwo.Kpnumber, detaltforwo.Kpdesc, out _StrErr);

           mPro.PRO_INSERTSMTKPDETALTFORWO(dic["MASTERID"].ToString(), dic["WOID"].ToString(), dic["USERID"].ToString(), dic["STATIONNO"].ToString(), dic["KPNUMBER"].ToString(), dic["KPDESC"].ToString(), out _StrErr);
            return _StrErr;    
                //mSmtkpmaster.InsertSmtKpDetaltForWo(detaltforwo);
        }
        [WebMethod]
        public byte[] GettSmtKPDetaltForWo()
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmaster.GettSmtKPDetaltForWo());
        }
        [WebMethod]
        public byte[] GettSmtKPDetaltForWoByKpnumber(string kpnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmaster.GettSmtKPDetaltForWoByKpnumber(kpnumber));
        }
        [WebMethod]
        public byte[] GetStationno(string partnumber, string lineid,string pcbside)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmaster.GetStationno(partnumber, lineid, pcbside));
        }
        [WebMethod]
        public byte[] JudgeKpExists(string stationno, string masterid, string kpnumber)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmaster.JudgeKpExists(stationno, masterid, kpnumber));
        }
        [WebMethod]
        public byte[] GetPartnumberAData(string partnumber, string lineid, string pcbside)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmaster.GetPartnumberAData(partnumber, lineid, pcbside));
        }

        #region 修改为压缩以后添加的
        [WebMethod]
        public byte[] GetAllKpMaster()
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmaster.GetAllKpMaster());
        }
        [WebMethod]
        public byte[] GetKpDetalt(string masterId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmaster.GetKpDetalt(masterId));
        }
        [WebMethod]
        public byte[] GetSmtKpMasterNotChecked()
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmaster.GetSmtKpMasterNotChecked());
        }
        [WebMethod]
        public string UpdateAuditingStatus(string dicsmtkpmaster)
        {
            return mSmtkpmaster.UpdateAuditingStatus(dicsmtkpmaster);
        }
        [WebMethod]
        public byte[]  QueryKpMasterAudit(int Days)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmaster.QueryKpMasterAudit(Days));
        }
        #endregion

         
    }
}
