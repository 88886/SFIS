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
    /// tSmtKpMonitor 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tSmtKpMonitor : System.Web.Services.WebService
    {
        BLL.tSmtKpMonitor mSmtkpmonitor = new BLL.tSmtKpMonitor();
        BLL.tPartBlocked mPartblocked = new BLL.tPartBlocked();
        BLL.tSmtIO mSmtio = new BLL.tSmtIO();
        BLL.tWoBomInfo mWobominfo = new BLL.tWoBomInfo();
        BLL.tSmtKpNormalLog mSmtkpnormallog = new BLL.tSmtKpNormalLog();
        BLL.R_Tr_Sn rTrSn = new BLL.R_Tr_Sn();
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();
        MapListConverter mlc = new MapListConverter();
        BLL.tSmtWoMerge swm = new BLL.tSmtWoMerge();

        [WebMethod]
        public void InsertSmtKpMonitor(string MASTERID,string WOID,string MACHINEID,string STATIONNO, string CDATA,string KPNUMBER,string SCARCITYUSER, out string err)
        {
            mSmtkpmonitor.InsertSmtKpMonitor( MASTERID, WOID, MACHINEID, STATIONNO,  CDATA, KPNUMBER, SCARCITYUSER, out err);
        }    


        [WebMethod]
        public void EditSmtKpMonitorFlag(string kpmonitorId, BLL.tSmtKpMonitor.CDATA cdata)
        {
            mSmtkpmonitor.EditSmtKpMonitorFlag(kpmonitorId, cdata);
        }
        [WebMethod]
        public bool Check_MaterialScrap(string pn, string vc, string dc, string lc)
        {
            return mPartblocked.Check_MaterialScrap(pn, vc, dc, lc);
        }

        [WebMethod]
        public byte[] GetSmtIO(string masterId, string woId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtio.GetSmtIO(masterId, woId));
        }
        [WebMethod]
        public byte[] GetSmtIO_WinCE(string masterId, string woId)
        {
            return mlc.GetDataSetZipBytes(mSmtio.GetSmtIO(masterId, woId));
        }

        [WebMethod]
        public  byte[] GetSmtIOMachineIdStatus(string machineId, BLL.tSmtIO.SmtIOStatus status)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtio.GetSmtIOMachineIdStatus(machineId, status));
        }
        [WebMethod]
        public byte[] GetSmtIOMachineIdStatus_WinCE(string machineId, BLL.tSmtIO.SmtIOStatus status)
        {
            return mlc.GetDataSetZipBytes(mSmtio.GetSmtIOMachineIdStatus(machineId, status));
        }

        [WebMethod]
        public  void EditSmtIOStatus(string masterId, string woId, BLL.tSmtIO.SmtIOStatus status)
        {
            mSmtio.EditSmtIOStatus(masterId, woId, status);
        }

        [WebMethod]
        public byte[] GetMaterialPreparation(string woId,string masterId)
        {
            return mlc.GetDataSetSurrogateZipBytes(mWobominfo.GetMaterialPreparation(woId, masterId));
        }
        [WebMethod]
        public byte[] GetMaterialPreparation_WinCE(string woId, string masterId)
        {
            return mlc.GetDataSetZipBytes(mWobominfo.GetMaterialPreparation(woId, masterId));
        }

        [WebMethod]
        public string InsertSmtKpNormalLog(string distring, string ROWID, int cdata)
        {
            string err="InsertSmtKpNormalLog Error ";
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(distring);
            string Trsn = mst["KP_SN"].ToString();           
                mSmtkpnormallog.InsertSmtKpNormalLog(mst, out  err);
                if (err == "OK")
                {
                    if (!string.IsNullOrEmpty(Trsn) && Trsn != "NA")
                        err = mPro.UPDATE_TR_SN(Trsn, "NA", mst["USERID"].ToString(), "7", "NA", "NA");
                    if (err == "OK")
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(ROWID) && ROWID != "NA")
                            mSmtkpmonitor.EditSmtKpMonitorFlag(ROWID, cdata);
                        }
                        catch(Exception ex)
                        {
                            err = ex.Message;
                        }
                    }                    
                }

                return err;
        }
 
        [WebMethod]
        public byte[] GetKpNumberInSEQ_WinCE(string MasterId, string WoId, string kpnumber, string station)
        {
            return mlc.GetDataSetZipBytes(mSmtkpmonitor.GetKpNumberInSEQ(MasterId, WoId, kpnumber, station));
        }
 
        [WebMethod]
        public byte[] CheckKpSupply_WinCE(string MasterId, string WoId, string station,string kpnumber, string cdata)
        {
            return mlc.GetDataSetZipBytes(mSmtkpmonitor.CheckKpSupply(MasterId, WoId, station,kpnumber, cdata));
        }
 

        [WebMethod]
        public  byte[] GetSmtkPMonnitorStation(string sSEQ, string sMO)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmonitor.GetSmtkPMonnitorStation(sSEQ, sMO));
        }
        [WebMethod]
        public byte[] GetSmtkPMonnitorStation_WinCE(string sSEQ, string sMO)
        {
            return mlc.GetDataSetZipBytes(mSmtkpmonitor.GetSmtkPMonnitorStation(sSEQ, sMO));
        }


        [WebMethod]
        public byte[] ChkStationInMonitor(string masterId, string woId, string stationno)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmonitor.ChkStationInMonitor(masterId, woId, stationno));
        }
        [WebMethod]
        public byte[] ChkStationInMonitor_WinCE(string masterId, string woId, string stationno)
        {
            return mlc.GetDataSetZipBytes(mSmtkpmonitor.ChkStationInMonitor(masterId, woId, stationno));
        }


        [WebMethod]
        public  void InsertSmtIoData(string dicskm)
        {
            mSmtkpmonitor.InsertSmtIoData(dicskm);
        }

        [WebMethod]
        public byte[] RefreshMaterialMonitor()
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmonitor.RefreshMaterialMonitor());
        }
        [WebMethod]
        public byte[] RefreshMaterialMonitor_WinCE()
        {
            return mlc.GetDataSetZipBytes(mSmtkpmonitor.RefreshMaterialMonitor());
        }

        [WebMethod]
        public void UpdateSmtkPMonnitorCdata(string rowid, string cdata)
        {
            mSmtkpmonitor.UpdateSmtkPMonnitorCdata(rowid, cdata);
        }

        [WebMethod]
        public byte[] QueryMaterialInOutPut(string woId, string kpnumber, bool Total)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmonitor.QueryMaterialInOutPut(woId, kpnumber, Total));
        }
        [WebMethod]
        public byte[] QueryMaterialInOutPut_WinCE(string woId, string kpnumber, bool Total)
        {
            return mlc.GetDataSetZipBytes(mSmtkpmonitor.QueryMaterialInOutPut(woId, kpnumber, Total));
        }     
        
        [WebMethod]
        public  byte[] GetQueliaoStationList(string sSEQ, string sMO)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmonitor.GetQueliaoStationList(sSEQ, sMO));
        }
        [WebMethod]
        public byte[] GetQueliaoStationList_WinCE(string sSEQ, string sMO)
        {
            return mlc.GetDataSetZipBytes(mSmtkpmonitor.GetQueliaoStationList(sSEQ, sMO));
        }
        
        
        [WebMethod]
        public  byte[] CheckSupplyMaterial(string sSEQ, string sMO, string sPartNo)
        {
            return mlc.GetDataSetSurrogateZipBytes(mSmtkpmonitor.CheckSupplyMaterial(sSEQ, sMO, sPartNo));
        }
        [WebMethod]
        public byte[] CheckSupplyMaterial_WinCE(string sSEQ, string sMO, string sPartNo)
        {
            return mlc.GetDataSetZipBytes(mSmtkpmonitor.CheckSupplyMaterial(sSEQ, sMO, sPartNo));
        }
        

        [WebMethod]
        public  void UpdateSupplyMaterialStatus(string dicskm)
        {
            mSmtkpmonitor.UpdateSupplyMaterialStatus(dicskm);
        }

        [WebMethod]
        public bool CheckKpMasterIdStatus(string masterId)
        {
            return mSmtkpmonitor.CheckKpMasterIdStatus(masterId);
        }
        /// <summary>
        /// 检测料站表是否经过确认并返回建立人和确认人
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetKpMasterIdStatus(string masterId)
        {
            return mSmtkpmonitor.GetKpMasterIdStatus(masterId);
        }       
        [WebMethod]
        public byte[] Get_TrsnData_Wince(string trsn,out string Msg)
        {
         return   mlc.GetDataSetZipBytes(rTrSn.Sel_Tr_Sn_Info(trsn, out Msg));
         
        }   
         [WebMethod]
        public string Update_TR_SN(string R_Trsn, string R_WOID, string R_USERID, string R_STATUS, string R_RMAK1, string R_RMAK2)
        {
            return mPro.UPDATE_TR_SN(R_Trsn, R_WOID, R_USERID, R_STATUS, R_RMAK1, R_RMAK2);
   
        }     
        [WebMethod]
        public void DeleteSmtKpMonitor(string sSEQ, string sMO)
        {
            mSmtkpmonitor.DeleteSmtKpMonitor(sSEQ, sMO);
        }
        [WebMethod]
        public List<string> GetMaterialTrsnList(string sSEQ, string sWO)
        {
            return mSmtkpmonitor.GetMaterialTrsnList(sSEQ, sWO);
        }
        [WebMethod]
        public bool CheckSCARCITYStation(string Masterid, string woId, string Machine, string Station)
        {
            return mSmtkpmonitor.CheckSCARCITYStation(Masterid, woId, Machine, Station);
        }
        [WebMethod(MessageName = "Update_SmtKpMonitor")]
        public string Update_SmtKpMonitor(string woId, string OldMaterId, string NewMasterId, string Machine)
        {
            return mSmtkpmonitor.Update_SmtKpMonitor(woId,OldMaterId,NewMasterId,Machine);
        }
         [WebMethod(MessageName = "Update_SmtKpMonitor_Json")]
        public string Update_SmtKpMonitor(string Json, List<string> TablesKey)
        {
            return mSmtkpmonitor.Update_SmtKpMonitor(Json, TablesKey);
        }
        [WebMethod]
        public byte[] Get_Smt_WO_Merge(string Json, string Field)
        {
           return mlc.GetDataSetZipBytes(swm.Get_Smt_WO_Merge(Json, Field));
        }
        

    }
}
