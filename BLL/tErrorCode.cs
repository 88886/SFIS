using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using System.Data.Common;
using SrvComponent;
using SystemObject;

namespace BLL
{
    public partial class tErrorCode
    {
       
        public tErrorCode()
        {
            
        }
        string table = "SFCB.B_ERROR_CODE";
        public  System.Data.DataSet GetErrorCode(string EC)
        {           
            string fieldlist = "ErrorCode,ErrorDesc,ErrorDesc2,recdate".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(EC))
                mst.Add("ERRORCODE", EC);
           return dp.GetData(table, fieldlist, mst, out count);
        }

        public void InsertErrorCode(string dicstring)
        {            
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicstring);
            dic.Add("RECDATE", DateTime.Now);
            dp.AddData(table, dic);
          
        }

        public void UpdateErrorCode(string dicstring)
        {         
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicstring);
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            StringBuilder ofilter = new StringBuilder();
            ofilter.Append("ERRORDESC = {0}, ");
            ofilter.Append("ERRORDESC2 = {1}, ");
             if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)            
            ofilter.Append("RECDATE =now() ");
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
                 ofilter.Append("RECDATE =SYSDATE ");
            IDictionary<string, object> modFields = new Dictionary<string, object>();
            modFields.Add("ERRORDESC", dic["ERRORDESC"]);
            modFields.Add("ERRORDESC2", dic["ERRORDESC2"]);

            string filter = "ERRORCODE = {0}";
            IDictionary<string, object> keyVals = new Dictionary<string, object>();
            keyVals.Add("ERRORCODE", dic["ERRORCODE"]);
            dp.UpdateBatchData(table, ofilter.ToString(), modFields, filter, keyVals);
        }

        public void DeleteErrorCode(string ERRORCODE)
        {           
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ERRORCODE", ERRORCODE);
            dp.DeleteData(table, mst);
            
        }
        public List<string> GetErrorCodeDesc(string EC)
        {
            List<string> ErrList = new List<string>();
            DataTable dt = GetErrorCode(EC).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                ErrList.Add(dt.Rows[0]["ErrorDesc"].ToString());
                ErrList.Add(dt.Rows[0]["ErrorDesc2"].ToString());
            }
            return ErrList;
        }

        public string GetErrorCode_Desc(string ec)
        {
            DataTable dt = GetErrorCode(ec).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0]["ErrorDesc"].ToString().Trim();
            else
                return string.Empty;
        }
    }
}
