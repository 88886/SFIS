using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SystemObject;

namespace BLL
{
    public partial class tCustomer
    {
       public  tCustomer()
        {
        }

        /// <summary>
       /// SFCB.B_CUSTOMER
        /// </summary>
       string table = "SFCB.B_CUSTOMER";
        // 获取用户信息表的所有记录
        public  System.Data.DataSet GettCustomerAll()
        {        
            string fieldlist = "customerId, customername, address, contactperson, phone, city";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);             
            return dp.GetData(table, fieldlist, null, out count);

        }

        //新建一条记录
        public  void InserttCustomer(string Insp)
        {           
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(Insp);
            dp.AddData(table, dic);
          
        }
        
        /// <summary>
        /// 根据客户编号，模糊查询
        /// </summary>
        /// <param name="customername"></param>
        /// <returns></returns>
        public System.Data.DataSet GetCustomerByName(string customername)
        {         
            int count = 0;
            string table = "SFCB.B_CUSTOMER";
            string fieldlist = "*";
            string filter = "customername like {0}";           
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("customername", '%' + customername + '%');
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }
      
    }
}

