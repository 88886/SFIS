using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericProvider;
using GenericUtil;
using System.Data;
using SystemObject;

namespace BLL
{
    public class tSmtWoMerge
    {

        public tSmtWoMerge()
        {
        }

        public string Insert_Smt_WO_Merge(string Json)
        {
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(Json);
                dp.AddData("SFCR.T_SMT_WO_MERGE", mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public DataSet Get_Smt_WO_Merge(string Json,string Field)
        {              
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(Json);
                if (string.IsNullOrEmpty(Field))
                    Field = "new_masterid,new_woid,old_masterid,old_woid,userid,merge_time,merge_num".ToUpper();
              return  dp.GetData("SFCR.T_SMT_WO_MERGE", Field, mst, out count);                 
            
        }
        public string Get_Merge_No()
        {
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);

            return dp.GetData("DUAL", "UNIX_TIMESTAMP(NOW())", null, out count).Tables[0].Rows[0][0].ToString();      
        }

        public DataSet Get_Smt_WO_Merge(string woId)
        {
            int count = 0;
            string table = "SFCR.T_SMT_WO_MERGE";
            string fieldlist = "DISTINCT  merge_num";
            string filter = "new_woid={0} OR old_woid={1}";            
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("NEW_WOID", woId);
            mst.Add("OLD_WOID", woId);
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

        }
    }
}
