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
    public partial class tMaterialPreparation_1
    {
        
        public tMaterialPreparation_1()
        {
            
        }

        string table = "SFCR.T_MATERIAL_PREPARATION";
        public System.Data.DataSet GetMaterialPreparationKpAndStation(string dicstring)
        {            
            string fieldlist = "stationno as 料站,kpnumber as 料号".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);             
            return dp.GetData(table, fieldlist, mst, out count);
        }

        public System.Data.DataSet GetMaterialPreparationStation(string dicstring)
        {            
            string fieldlist = "distinct stationno".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        public System.Data.DataSet GetMaterialPreparation(string dicstring)
        {           
            string fieldlist = "*".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        /// <summary>
        /// 获得主代用料
        /// </summary>
        /// <param name="tmp"></param>
        /// <returns></returns>
        public System.Data.DataSet GetKpdistinctMaterial(string dicstring)
        {           
            string fieldlist = "*".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        public System.Data.DataSet QueryAllStationList(string sSEQ, string sMO)
        {            
            string fieldlist = "WOID,PARTNUMBER,USERID,STATIONNO,MASTERID,KPNUMBER,KPDESC,REPLACEGROUP,KPDISTINCT,LOCALTION,RECDATE,BOMVER,SIDE,UNIT,FEEDERTYPE".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MASTERID", sSEQ);
            mst.Add("WOID", sMO);
            return dp.GetData(table, fieldlist, mst, out count);
        }  
        public System.Data.DataSet PublicMaterialPreparationSelect(string woId)
        {          
            string fieldlist = "stationno,kpnumber".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        /// <summary>
        /// 导入料表到MaterialPreparation_1
        /// </summary>
        /// <param name="masterid"></param>
        /// <param name="woid"></param>
        /// <param name="userid"></param>
        public string InsertMaterialPreparation_1(string masterid, string woid, string userid)
        {
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);          
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOID", woid);
                mst.Add("MASTERID", masterid);
                dp.DeleteData(tx, "sfcr.t_material_preparation_1", mst);
                string fieldlist = "woId,partnumber,stationno,masterId,kpnumber,bomver".ToUpper();
                int count = 0;
                mst = new Dictionary<string, object>();
                mst.Add("WOID", woid);
                mst.Add("MASTERID", masterid);
                DataTable dt = dp.GetData(table, fieldlist, mst, out count).Tables[0];
                IList<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
                foreach (DataRow dr in dt.Rows)
                {
                    mst = new Dictionary<string, object>();
                    mst.Add("WOID", dr["WOID"].ToString());
                    mst.Add("PARTNUMBER", dr["PARTNUMBER"].ToString());
                    mst.Add("USERID", userid);
                    mst.Add("STATIONNO", dr["STATIONNO"].ToString());
                    mst.Add("STATIONNUM", (dt.Select(string.Format("STATIONNO='{0}'", dr["STATIONNO"].ToString())).Length).ToString());
                    mst.Add("MASTERID", dr["MASTERID"].ToString());
                    mst.Add("KPNUMBER", dr["KPNUMBER"].ToString());
                    mst.Add("BOMVER", dr["BOMVER"].ToString());
                    mst.Add("RECDATE", System.DateTime.Now);
                    list.Add(mst);
                }
                dp.AddListData(tx, "sfcr.t_material_preparation_1", list);
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

        public System.Data.DataSet GetMaterialPreByStation_1(string sWoid, string sMasterId, string sStationno)
        {          
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            string fieldlist = "woId,partnumber,userId,stationno,stationnum,masterId,kpnumber".ToUpper();
            int count = 0;
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", sWoid);
            mst.Add("MASTERID", sMasterId);
            mst.Add("STATIONNO", sStationno);
            return dp.GetData("sfcr.t_material_preparation_1".ToUpper(), fieldlist, mst, out count);

        }
        public System.Data.DataSet GetMaterialPreByKpnumber_1(string sWoid, string sKpnumber)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            string fieldlist = "woId,partnumber,userId,stationno,stationnum,masterId,kpnumber".ToUpper();
            int count = 0;
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", sWoid);
            mst.Add("KPNUMBER", sKpnumber);           
            return dp.GetData("sfcr.t_material_preparation_1".ToUpper(), fieldlist, mst, out count);
        }

        public System.Data.DataSet Get_T_MATERIAL_PREPARATION(string dicstring)
        {           
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            string fieldlist = "WOID,PARTNUMBER,USERID,STATIONNO,MASTERID,KPNUMBER,KPDESC,REPLACEGROUP,KPDISTINCT,LOCALTION,RECDATE,BOMVER,SIDE,UNIT,FEEDERTYPE".ToUpper();
            int count = 0;
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);         
            
            return dp.GetData(table, fieldlist, mst, out count);

        }
    }
}
