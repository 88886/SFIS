using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;

namespace BLL
{
   public partial class tPartKeyParts
    {
       
       public tPartKeyParts()
       {
           
       }
       string table = "SFCB.B_PART_KEYPARTS";
       public  void InsertPartKeyParts(string dicstring)
       {         

           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);        
           dp.AddData(table, mst);
       }

       public  void DeletePartKeyParts(string dicstring)
       {         
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
           dp.DeleteData(table, mst);
       }
    }
}
