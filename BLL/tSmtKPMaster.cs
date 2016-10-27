using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SystemObject;

namespace BLL
{
    public partial class tSmtKPMaster
    {
       
       public  tSmtKPMaster()
        {
            
        }
        /// <summary>
        /// 根据备料表获取是否为最新料表
        /// </summary>
        /// <param name="MasterId"></param>
        /// <returns></returns>
       public System.Data.DataSet GetSmtKpMaster(string MasterId)
        {      
            string table = "SFCR.T_SMT_KP_MASTER";
            string fieldlist = "masterId,lineId,userId,partnumber,modelname,bomver,pcbside,recdate,reserve1,case reserve2 when '0' then '待审核' when '1' then '审核通过' else '审核失败' end as reserve2";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MASTERID", MasterId);
           return dp.GetData(table, fieldlist, mst, out count);
        }
        public  void DeleteSmtKpMaster(string KpMaster)
        {         
            string table = "SFCR.T_SMT_KP_MASTER";
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);          
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MASTERID", KpMaster);       
            dp.DeleteData(table, mst);
        }
        public  void DeleteSmtKPDetalt(string KpMaster)
        {          
            string table = "SFCR.T_SMT_KP_DETALT";
             IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
             IDictionary<string, object> mst = new Dictionary<string, object>();
             mst.Add("MASTERID", KpMaster);
             dp.DeleteData(table, mst);
        }
        public string DeleteSmtKpMaster(List<string> LsMachine, string PARTNUMBER, string PCBASIDE)
        {
            try
            {
               
                foreach (string Str in LsMachine)
                {
                    string table = "SFCR.T_SMT_KP_MASTER";
                    string fieldlist = "MASTERID";
                    int count = 0;
                    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                    IDictionary<string, object> mst = new Dictionary<string, object>();
                    mst.Add("LINEID", Str);
                    mst.Add("PARTNUMBER", PARTNUMBER);
                    mst.Add("PCBSIDE", PCBASIDE);                     
                    System.Data.DataTable dt= dp.GetData(table, fieldlist, mst, out count).Tables[0];

                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        DeleteSmtKPDetalt(dr["MASTERID"].ToString());
                        DeleteSmtKpMaster(dr["MASTERID"].ToString());
                    }
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 根据成品料号获取该料号在哪些SMT产线上生产
        /// </summary>
        /// <param name="partnumber">成品料号</param>
        /// <returns></returns>
        public  System.Data.DataSet GetMachineIdAndMasterIdListByPartnumber(string partnumber)
        {          
            string table = "SFCR.T_SMT_KP_MASTER";
            string fieldlist = "masterId,lineId".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("PARTNUMBER", partnumber);
            return dp.GetData(table, fieldlist, mst, out count);


        }     

        #region 修改为压缩以后添加的
        public  System.Data.DataSet GetAllKpMaster()
        {            
            string table = "SFCR.T_SMT_KP_MASTER";
            string fieldlist = "masterId,lineId,userId,partnumber,modelname,bomver,pcbside,recdate,reserve1,case reserve2 when '0' then '待审核' when '1' then '审核通过' else '审核失败' end as reserve2".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);            
            return dp.GetData(table, fieldlist, null, out count);
        }

        public  System.Data.DataSet GetKpDetalt(string masterId)
        {           
            string table = "SFCR.T_SMT_KP_DETALT";
            string fieldlist = "stationno,kpnumber,kpdesc,case kpdistinct when '1' then 'True' else 'False' end as kpdistinct,replacegroup,priorityclass,loction,reserve1,reserve".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MASTERID", masterId);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        /// <summary>
        /// 获取所有待审核的料站表
        /// </summary>
        /// <returns></returns>
        public  System.Data.DataSet GetSmtKpMasterNotChecked()
        {
            string table = "SFCR.T_SMT_KP_MASTER";
            string fieldlist = "masterId,lineId,userId,partnumber,modelname,bomver,pcbside,recdate,reserve1,case reserve2 when '0' then '待审核' when '1' then '审核通过' else '审核失败' end as reserve2".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("reserve2", "0");
            return dp.GetData(table, fieldlist, mst, out count);
        }
        /// <summary>
        /// 更新料站表状态
        /// </summary>
        /// <param name="smtkpmaster"></param>
        /// <returns></returns>
        public  string UpdateAuditingStatus(string dicsmtkpmaster)
        {
            try
            {                
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicsmtkpmaster);
                dp.UpdateData("SFCR.T_SMT_KP_MASTER", new string[] { "MASTERID" }, mst);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public System.Data.DataSet QueryKpMasterAudit(int Days)
        {         
           
            int count = 0;
            string table = "SFCR.T_SMT_KP_MASTER";
            string fieldlist = "masterId,lineId,userId,partnumber,modelname,bomver,pcbside,recdate,reserve1,Auditinguser,case reserve2 when '0' then '待审核' when '1' then '审核通过' else '审核失败' end as reserve2";
           // string filter = "recdate>DATE_SUB(NOW(),INTERVAL {0} DAY";     
            string filter = "recdate>{0}";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("recdate", System.DateTime.Now.AddDays(-Days));
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }
        #endregion

        /// <summary>
        /// 获取所有的料站表头
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetAllMasterid()
        {          
            string table = "SFCR.T_SMT_KP_MASTER";
            string fieldlist = "masterId";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);             
            return dp.GetData(table, fieldlist,null, out count);    
        }
        public System.Data.DataSet GetKpDetail(string masterid, string stationno)
        {
            string table = "SFCR.T_SMT_KP_DETALT";
            string fieldlist = "kpdistinct,replacegroup,priorityclass,loction".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MASTERID", masterid);
            mst.Add("STATIONNO", stationno);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        /// <summary>
        /// 由成品料号，机器编号及PCB面获得该产品的备料料站信息
        /// </summary>
        /// <param name="partnumber"></param>
        /// <param name="lineid"></param>
        /// <param name="pcbside"></param>
        /// <returns></returns>
        public System.Data.DataSet GetStationno(string partnumber, string lineid, string pcbside)
        {
            int count = 0;
            string table = "SFCR.T_SMT_KP_DETALT a";
            string fieldlist = "distinct a.stationno,a.masterId";
            string filter = " exists (select b.masterId from SFCR.T_SMT_KP_MASTER b where a.masterId=b.masterId and b.partnumber={0} and b.lineId={1} and pcbside={2})";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("partnumber", partnumber);
            mst.Add("lineId", lineid);
            mst.Add("pcbside", pcbside);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }
        public System.Data.DataSet GettSmtKPDetaltForWo()
        {       
            string table = "SFCR.T_SMT_KP_DETALT_FORWO";
            string fieldlist = "woId,kpnumber,stationno,kpdesc,kpdistinct,replacegroup,recdate";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            return dp.GetData(table, fieldlist, null, out count);   
        }
        public System.Data.DataSet GettSmtKPDetaltForWoByKpnumber(string kpnumber)
        {
            string table = "SFCR.T_SMT_KP_DETALT_FORWO";
            string fieldlist = "woId,kpnumber,stationno,kpdesc,kpdistinct,replacegroup,recdate".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("KPNUMBER", kpnumber);          
            return dp.GetData(table, fieldlist, mst, out count);
        }
        /// <summary>
        /// 判断该料在料站表中是否存在
        /// </summary>
        /// <param name="stationno"></param>
        /// <param name="masterid"></param>
        /// <param name="kpnumber"></param>
        /// <returns></returns>
        public System.Data.DataSet JudgeKpExists(string stationno, string masterid, string kpnumber)
        {            
            string table = "SFCR.T_SMT_KP_DETALT";
            string fieldlist = "kpnumber,kpdesc".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("STATIONNO", stationno);
            mst.Add("MASTERID", masterid);
            mst.Add("KPNUMBER", kpnumber);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        public System.Data.DataSet GetPartnumberAData(string partnumber, string lineid, string pcbside)
        {
          
            int x = 0;
            int count = 0;
            string table = "SFCR.T_SMT_KP_MASTER a,SFCR.T_SMT_KP_DETALT b";
            string fieldlist = "a.partnumber,a.lineId,a.pcbside,a.bomver,b.stationno,b.kpnumber,b.kpdesc,b.replacegroup,b.loction";
            string filter = "a.masterId=b.masterId and a.partnumber={0}";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("partnumber", partnumber);
            if (!string.IsNullOrEmpty(lineid))
            {
                x = x + 1;
                filter += " and a.lineId like {"+x.ToString()+"}";
                mst.Add("lineId", lineid + "%");
            }
            if (!string.IsNullOrEmpty(pcbside))
            {
                x = x + 1;
                filter += " and a.pcbside={" + x.ToString() + "}";
                mst.Add("pcbside", pcbside);
            }           
           
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }
    }
}
