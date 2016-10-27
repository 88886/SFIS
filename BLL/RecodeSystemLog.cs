using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;

namespace BLL
{
    public partial class RecodeSystemLog
    {
       
       public RecodeSystemLog()
       {
         
       }
       public void InsertSystemLog(string dicstring, out string err)
       {
           try
           {
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);  
               dp.AddData("SFCR.T_SYSTEM_LOG", mst);
               err = "OK";
           }
           catch (Exception ex)
           {
               err = ex.Message;
           }        

       }

    }
}
