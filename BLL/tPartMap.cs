using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;

namespace BLL
{
    public partial class tPartMap
    {
        
        public tPartMap()
        {
           
        }

    

     
        /// <summary>
        /// 根据料号获取信息
        /// </summary>
        /// <param name="kpnumber">料号</param>
        /// <returns></returns>
        public System.Data.DataSet QueryPartMaps(string kpnumber)
        {
            string table = "SFCR.T_PART_MAPS";
            string fieldlist = "kpnumber,selfkpnumber,venderId,vendercode,venderkpnumber,kpdesc,parttype,partgroup,partstate".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("KPNUMBER", kpnumber);
           return    dp.GetData(table, fieldlist,  mst, out count);
        }

    }
}
