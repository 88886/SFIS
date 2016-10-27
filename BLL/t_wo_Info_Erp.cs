using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemObject;
using GenericUtil;
using GenericProvider;
using System.Data;

namespace BLL
{
   public  class t_wo_Info_Erp
    {

       public t_wo_Info_Erp()
       {
       }

       public System.Data.DataSet Get_Erp_woinfo()
       {
           int count = 0;
           string table = "mesdb.t_wo_info_erp".ToUpper();
           string fieldlist = "WOID,POID,QTY,PARTNUMBER,BOMVER,SAPWOTYPE,SAPROUTETYPE,SAPROUTEGROUP,SAPROUTEINDEX,WOSTARTTIME,WOFINISHTIME,WORLSFLAG,FACTORYID,LOC";
           string filter = "woid not in (select woid from sfcr.t_wo_info) and WORLSFLAG='0'".ToUpper();           
           return TransactionManager.GetData(table, fieldlist, filter, null,null, null, out count);
       }
       public System.Data.DataSet Get_WO_Info_Erp(string woId, string Fields)
       {
           int count = 0;
           string table = "mesdb.t_wo_info_erp".ToUpper();
           string fieldlist = "WOID,POID,QTY,PARTNUMBER,BOMVER,SAPWOTYPE,SAPROUTETYPE,SAPROUTEGROUP,SAPROUTEINDEX,WOSTARTTIME,WOFINISHTIME,WORLSFLAG,FACTORYID,LOC";
           if (string.IsNullOrEmpty(Fields))
               Fields = fieldlist;       
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           Dictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("WOID",woId);
           return dp.GetData(table, Fields, mst, out count);
       }

       public Dictionary<string, object> Get_Erp_WoList()
       {
           Dictionary<string, object> dic = new Dictionary<string, object>();
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           int count = 0;
           string table = "mesdb.t_wo_info_erp".ToUpper();
           string fieldlist = "WOID,PARTNUMBER";       
           DataSet ds = dp.GetData(table, fieldlist, null, out count);
           if (ds.Tables[0].Rows.Count > 0)
           {
               DataTable dt = DataTableToSort(ds.Tables[0],"WOID");
               foreach (DataRow dr in dt.Rows)
               {
                   dic.Add(dr["WOID"].ToString(), dr["PARTNUMBER"].ToString());
               }
           }
           return dic;
       }

       private DataTable DataTableToSort(System.Data.DataTable dt, string Colnums)
       {
           DataView dv = dt.DefaultView;
           dv.Sort = string.Format("{0} ASC", Colnums);
           return dv.ToTable();
       }

       public string Update_Erp_woInfo(string dicstring)
       {
           try
           {
               IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicstring);

               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               IDictionary<string, object> mst = new Dictionary<string, object>();
               dp.UpdateData("mesdb.t_wo_info_erp".ToUpper(), new string[] { "WOID" }, dic);
               return "OK";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }

    }
}
