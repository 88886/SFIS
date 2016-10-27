using System;
using System.Collections.Generic;
using System.Text;
using GenericProvider;
using GenericUtil;

namespace BLL
{
   public class T_TESTRESULT_INFO
    {
       public T_TESTRESULT_INFO()
       {
       }

       public string Insert_T_TESTRESULT_INFO(string dicstring)
       {
           try
           {           
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
               mst.Add("CDATETIME", System.DateTime.Now);
               mst.Add("CDATE", System.DateTime.Now.ToString("yyyyMMdd"));
               dp.AddData("SFCR.T_TESTRESULT_INFO", mst);
               return "OK";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }


    }
}
