using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GenericProvider;

namespace BLL
{
   public class tProductRealSummary
    {
       public tProductRealSummary()
       {       
       }

       public DataSet Get_Product_Real_Summary(string WORK_DATE,string Class)
       {
           int count = 0;
           string table = "mesdb.t_product_real_summary";
           string fieldlist = "workdate,line,linedesc,bu,section,productname,class,targetqty,planoutqty,actualqty,targetrate";
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("WORKDATE", WORK_DATE);
           mst.Add("CLASS", Class);
           return dp.GetData(table, fieldlist, mst, out count);
       }
    }
}
