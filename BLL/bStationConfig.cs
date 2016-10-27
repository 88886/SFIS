using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GenericProvider;

namespace BLL
{
   public class bStationConfig
    {
       public bStationConfig()
       {
       }

       public DataSet Get_StationConfig(string HostId, string Fields)
       {
           string fieldlist = "HostID,Station_IDX,Station_NUMBER,Line_Name,Section_Name,Group_Name,Station_Name,Task_Code".ToUpper();
           if (string.IsNullOrEmpty(Fields))
               Fields = fieldlist;
           int count = 0;
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("HOSTID", HostId);
           return dp.GetData("SFCB.B_STATION_CONFIG", Fields, mst, out count);
       }

       public string Insert_StationConfig(IDictionary<string,object> dic)
       {
           try
           {
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               dp.AddData("SFCB.B_STATION_CONFIG", dic);
               return "OK";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }
       public string Update_StationConfig(IDictionary<string, object> dic)
       {
           try
           {
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               dp.AddData("SFCB.B_STATION_CONFIG", dic);
               return "OK";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }

    }
}
