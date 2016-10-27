using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;

namespace BLL
{
   public partial class tVendCode
    {
       
       public  tVendCode()
       {
          
       }
       public  DataSet GetVendCodeInfo(string vendnumber)
       {
           //MySqlCommand cmd = new MySqlCommand();
           //cmd.CommandText = "select kpnumber,vendername,vendcode from SFCR.T_VEND_CODE where vendnumber=@vendnumber";
           //cmd.Parameters.Add("vendnumber", MySqlDbType.VarChar, 25).Value = vendnumber;

           //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

           string table = "SFCR.T_VEND_CODE";
           string fieldlist = "kpnumber,vendername,vendcode";
           int count = 0;
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("vendnumber", vendnumber);
           return dp.GetData(table, fieldlist, mst, out count);
       }
    }
}
