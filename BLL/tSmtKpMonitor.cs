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
    public partial class tSmtKpMonitor
    {
        
        public  tSmtKpMonitor()
        {
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)
                DB_Flag = 0;
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
                DB_Flag = 1;
        }
        /// <summary>
        /// 数据库标记 0 MySQL;1 Oracle
        /// </summary>
        int DB_Flag = 0;
        public enum CDATA
        {
            缺料,
            已经补料,
            刷完换料,
            首盘备料,
            删除
        }
        public void InsertSmtKpMonitor(string MASTERID,string WOID,string MACHINEID,string STATIONNO, string CDATA,string KPNUMBER,string SCARCITYUSER, out string err)
        {          
            try
            {                

                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("MASTERID",MASTERID);
                 mst.Add("WOID",WOID);
                 mst.Add("MACHINEID",MACHINEID);
                 mst.Add("STATIONNO",STATIONNO);
                 mst.Add("CDATA",CDATA);
                 mst.Add("KPNUMBER",KPNUMBER);
                 mst.Add("SCARCITYTIME",System.DateTime.Now);
                 mst.Add("SCARCITYUSER", SCARCITYUSER);
                dp.AddData("SFCR.T_SMT_KP_MONNITOR", mst);
                err = "";
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
        }

        /// <summary>
        /// 改变料的状态
        /// </summary>
        /// <param name="kpmonitorId"></param>
        /// <param name="cdata"></param>
        public void EditSmtKpMonitorFlag(string kpmonitorId, CDATA cdata)
        {
            EditSmtKpMonitorFlag(kpmonitorId, (int)cdata);
        }
        public void EditSmtKpMonitorFlag(string kpmonitorId, int cdata)
        {          
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("CDATA", cdata);
            mst.Add("ROWID", kpmonitorId);
            dp.UpdateData("SFCR.T_SMT_KP_MONNITOR", new string[] { "ROWID" }, mst);
        }

        public System.Data.DataSet GetKpNumberInSEQ(string MasterId, string WoId, string kpnumber, string station)
        {
            
            string table = "SFCR.T_MATERIAL_PREPARATION" ;
            string fieldlist = "WOID,PARTNUMBER,USERID,STATIONNO,MASTERID,KPNUMBER,KPDESC,REPLACEGROUP,KPDISTINCT,LOCALTION,RECDATE,BOMVER,SIDE,UNIT,FEEDERTYPE";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MASTERID", MasterId);
            mst.Add("WOID", WoId);
            mst.Add("KPNUMBER", kpnumber);
            if (!string.IsNullOrEmpty(station))
                mst.Add("STATIONNO", station);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        public System.Data.DataSet CheckKpSupply(string MasterId, string WoId, string stationno,string kpnumber, string cdata)
        {
           
            string table = "SFCR.T_SMT_KP_MONNITOR";
            string fieldlist = "ROWID,MASTERID,WOID,MACHINEID,STATIONNO,CDATA,KPNUMBER,SCARCITYTIME,SCARCITYUSER,SUPPLYTIME,SUPPLYUSER,FLAG,QTY,VENDERCODE,DATECODE,LOTID,TRSN";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();         
            mst.Add("MASTERID", MasterId);          
            mst.Add("WOID", WoId);
            if (!string.IsNullOrEmpty(stationno))
                mst.Add("STATIONNO", stationno);
            if (!string.IsNullOrEmpty(kpnumber))
            mst.Add("KPNUMBER", kpnumber);
            if (!string.IsNullOrEmpty(cdata))
            mst.Add("CDATA", cdata);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        public System.Data.DataSet GetSmtkPMonnitorStation(string sSEQ, string sMO)
        {        

            string table = "sfcr.t_smt_kp_monnitor".ToUpper();
            string fieldlist = "distinct stationno".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MASTERID", sSEQ);
            mst.Add("WOID", sMO);
            return  dp.GetData(table, fieldlist, mst, out count);
        }
        public System.Data.DataSet ChkStationInMonitor(string masterId, string woId, string stationno)
        {          
            string table = "sfcr.t_smt_kp_monnitor".ToUpper();
            string fieldlist = "MASTERID,WOID,MACHINEID,STATIONNO,CDATA,KPNUMBER,SCARCITYTIME,SCARCITYUSER,SUPPLYTIME,SUPPLYUSER,FLAG,QTY,VENDERCODE,DATECODE,LOTID,TRSN";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MASTERID", masterId);
            mst.Add("WOID", woId);
            mst.Add("STATIONNO", stationno);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        public void InsertSmtIoData(string dicskm)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicskm);
            dp.AddData("SFCR.T_SMT_KP_MONNITOR", mst);
        }

        public System.Data.DataSet RefreshMaterialMonitor()
        {
            string table = string.Empty;
            string fieldlist = string.Empty;
            string filter = string.Empty;
            int count = 0;
            if (DB_Flag == 0)
            {
                 table = "sfcr.t_smt_kp_monnitor a, SFCB.B_MACHINE_INFO b";
                 fieldlist = "a.rowid as kpmonitorid,a.*,b.*,IFNULL(TIMESTAMPDIFF(MINUTE,supplytime,NOW()),0)  as nochgkp,IFNULL(TIMESTAMPDIFF(MINUTE,scarcitytime,NOW()),0) as nosupply";
                 filter = "a.machineId = b.machineId and (cdata = '0' or cdata = '1')";
            }
            if (DB_Flag == 1)
            {
                table = "sfcr.t_smt_kp_monnitor a, SFCB.B_MACHINE_INFO b";
                fieldlist = "a.rowid as kpmonitorid,a.*,b.*,NVL(round((sysdate - supplytime) * 1440, 0), 0) as nochgkp,NVL(round((sysdate - scarcitytime) * 1440, 0), 0) as nosupply";
                filter = "a.machineId = b.machineId and (cdata = '0' or cdata = '1')";
            }

            return TransactionManager.GetData(table, fieldlist, filter, null, null, null, out count);
        }

        public void UpdateSmtkPMonnitorCdata(string rowid,string cdata)
        {           
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ROWID",rowid);
            mst.Add("CDATA", cdata);
            dp.UpdateData("sfcr.t_smt_kp_monnitor", new string[] { "ROWID" }, mst);
        }

        public System.Data.DataSet QueryMaterialInOutPut(string woId,string kpnumber,bool Total)
        {          

            int count = 0;
            string table = "sfcr.t_smt_kp_monnitor";
             string group = null;
                 
                 string fieldlist = string.Empty;
                 if (Total)
                 {
                     fieldlist = "woId as 工单 ,kpnumber as 料号,sum(qty) as 数量";
                     group = "woId,kpnumber";
                 }
                 else
                     fieldlist = "masterid as SEQ,woid as 工单,machineid as 机器代码,kpnumber as 料号,stationno as 料站号码,vendercode as 厂商代码,datecode as 生产周期,lotid as 生产批次,qty as 数量,cdata as 标志位,scarcitytime as 缺料时间,scarcityuser as 刷缺料人员,supplytime as 补料时间,supplyuser as 补料人员,trsn";
            string filter = "woId={0}";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("woId", woId);
            if (!string.IsNullOrEmpty(kpnumber))
            {
                filter += " and kpnumber={1} ";
                mst.Add("kpnumber",kpnumber);
            }
            filter += " and cdata>1";
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, group, out count);

        }   

        public System.Data.DataSet GetQueliaoStationList(string sSEQ, string sMO)
        {          
            int count = 0;
            string table = "sfcr.t_smt_kp_monnitor".ToUpper();
            string fieldlist = "MASTERID,WOID,MACHINEID,STATIONNO,CDATA,KPNUMBER,SCARCITYTIME,SCARCITYUSER,SUPPLYTIME,SUPPLYUSER,FLAG,QTY,VENDERCODE,DATECODE,LOTID,TRSN";
            string filter = "masterid={0}  and woId={1} and (cdata='0' or cdata='1')";           
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("masterid", sSEQ);
            mst.Add("woId", sMO);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }

        public System.Data.DataSet CheckSupplyMaterial(string sSEQ, string sMO, string sPartNo)
        {         
            int count = 0;
            string table = "sfcr.t_smt_kp_monnitor a,SFCR.T_MATERIAL_PREPARATION b ";
            string fieldlist = "a.rowid";
            string filter = "a.masterid=b.masterid and a.woid=b.woid and a.stationno=b.stationno and a.masterId ={0} and a.woId ={1} and a.cdata='0' and b.kpnumber={2}";            
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("masterId", sSEQ);
            mst.Add("woId", sMO);
            mst.Add("kpnumber", sPartNo);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
             
        }

        public void UpdateSupplyMaterialStatus(string dicskm)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicskm);
            mst.Add("supplytime".ToUpper(),System.DateTime.Now);
            dp.UpdateData("sfcr.t_smt_kp_monnitor", new string[] { "ROWID" }, mst);

        }
        /// <summary>
        /// 检测料站表是否经过确认
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public bool CheckKpMasterIdStatus(string masterId)
        {          
            string table = "SFCR.T_SMT_KP_MASTER";
            string fieldlist = "reserve2".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("masterId", masterId);
            DataTable _dt = dp.GetData(table, fieldlist,mst, out count).Tables[0];

            if (_dt != null && int.Parse(_dt.Rows[0][0].ToString()) == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 检测料站表是否经过确认并返回建立人和确认人
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public string GetKpMasterIdStatus(string masterId)
        {          

            string table = "SFCR.T_SMT_KP_MASTER";
            string fieldlist = "userId,reserve2,Auditinguser";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("masterId", masterId);
            DataTable _dt = dp.GetData(table, fieldlist,mst, out count).Tables[0];

            if (_dt != null && _dt.Rows.Count > 0)
            {
                if (int.Parse(_dt.Rows[0]["reserve2"].ToString()) == 1)
                {
                    return string.Format("EditUser:{0},CheckUser:{1}", _dt.Rows[0]["userId"].ToString(), _dt.Rows[0]["Auditinguser"].ToString());
                }
                else
                {
                    return "ERROR:QC NO CHECK";
                }
            }
            else
            {
                return "ERROR:NO MasterId";
            }
        } 

        /// <summary>
        /// 获取工单备料表Trsn
        /// </summary>
        /// <returns></returns>
        public  List<string> GetMaterialTrsnList(string sSEQ, string sWO)
        {

            List<string> Trsnlist = new List<string>();        
       
            string table = "sfcr.t_smt_kp_monnitor";
            string fieldlist = "Trsn";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("masterId", sSEQ);
            mst.Add("woId", sWO);
            DataTable _dt = dp.GetData(table, fieldlist, mst, out count).Tables[0];

            foreach (DataRow item in _dt.Rows)
            {
                Trsnlist.Add(item["Trsn"].ToString());
            }
            return Trsnlist;

        }

        public  void DeleteSmtKpMonitor(string sSEQ, string sMO)
        {          
            string table = "sfcr.t_smt_kp_monnitor";
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MASTERID", sSEQ);
            mst.Add("WOID", sMO);
            dp.DeleteData(table, mst);

        }

        public bool CheckSCARCITYStation(string Masterid, string woId, string Machine, string Station)
        {            

            int count = 0;
            string table = "sfcr.t_smt_kp_monnitor";
            string fieldlist = "stationno";
            string filter = "masterId={0} and woId={1} and machineId={2} and cdata<2";
            
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("masterId", Masterid);
            mst.Add("woId", woId);
            mst.Add("machineId", Machine);
            DataSet ds = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

            DataTable dt = ds.Tables[0];  /// BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];

            if ((dt == null) || (dt.Rows.Count == 0))
                return true;
            else
                return false;
        }

        public string Update_SmtKpMonitor(string woId,string OldMaterId,string NewMasterId,string Machine)
        {
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {               
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                StringBuilder ofilter = new StringBuilder();
                ofilter.Append("MASTERID={0},MACHINEID={1}");              
                IDictionary<string, object> modFields = new Dictionary<string, object>();
                modFields.Add("MASTERID", NewMasterId);
                modFields.Add("MACHINEID", Machine);
                string filter = "MASTERID={0} AND WOID={1}";
                IDictionary<string, object> keyVals = new Dictionary<string, object>();
                keyVals.Add("MASTERID1", OldMaterId);
                keyVals.Add("WOID", woId);
                dp.UpdateBatchData(tx, "SFCR.T_SMT_KP_MONNITOR", ofilter.ToString(), modFields, filter, keyVals);


                ofilter = new StringBuilder();
                ofilter.Append("MASTERID={0},MACHINEID={1}");
                modFields = new Dictionary<string, object>();
                modFields.Add("MASTERID", NewMasterId);
                modFields.Add("MACHINEID", Machine);
                filter = "MASTERID={0} AND WOID={1}";
                keyVals = new Dictionary<string, object>();
                keyVals.Add("MASTERID1", OldMaterId);
                keyVals.Add("WOID", woId);
                dp.UpdateBatchData(tx, "SFCR.T_SMT_IO", ofilter.ToString(), modFields, filter, keyVals);
                tx.Commit();

                 return "OK";// +woId + "--" + OldMaterId + "--" + NewMasterId + "--" + Machine;
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

        public string Update_SmtKpMonitor(string Json, List<string> TablesKey)
        {
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(Json);
                dp.UpdateData("SFCR.T_SMT_KP_MONNITOR", TablesKey.ToArray(), mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

     
    }
}
