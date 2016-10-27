using System;
using System.Collections.Generic;
using System.Text;
using GenericProvider;
using GenericUtil;

namespace BLL
{
    public partial class t_Check_Timeout
    {
        public t_Check_Timeout()
        {
        }


        public System.Data.DataSet Get_t_Check_Timeout(string CHECK_NO)
        {          

            string table = "SFCR.T_CHECK_TIMEOUT";
                string fieldlist = "CHECK_NO,CHECK_ROUTE,ROLLBACK_ROUTE,CHECK_TIMEOUT,REST_TIME,USERID,RECORD_DATE";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(CHECK_NO))
                 mst.Add("CHECK_NO", CHECK_NO);
                return dp.GetData(table, fieldlist, mst, out count);
        }

        public string Insert_Check_Timeout(string dicstring)
        {           
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
                dp.AddData("SFCR.T_CHECK_TIMEOUT", mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string Update_Check_Timeout(string dicstring)
        {              
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
                dp.UpdateData("SFCR.T_CHECK_TIMEOUT", new string[] { "CHECK_NO" }, mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Delete_Check_Timeout(string CHECK_NO)
        {        
            try
            {
                string table = "SFCR.T_CHECK_TIMEOUT";
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("CHECK_NO", CHECK_NO);
                dp.DeleteData(table, mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
