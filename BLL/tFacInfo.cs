using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;

namespace BLL
{
    public partial class tFacInfo
    {
       
        public tFacInfo()
        {
           
        }
        string table = "SFCB.B_FAC_INFO";
        /// <summary>
        /// 获取所有的工厂信息
        /// </summary>
        /// <returns>facId,facname,address</returns>
        public System.Data.DataSet GetFacInfo()
        {           
            string fieldlist = "facId,facname,address ,facCode".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);          
            return dp.GetData(table, fieldlist, null, out count);
        }

        /// <summary>
        /// 新增工厂信息
        /// </summary>
        /// <param name="facinfo"></param>
        public void InsertFacInfo(string dicFacinfo)
        {           
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicFacinfo);            
            dp.AddData(table, dic);

        }

        /// <summary>
        /// 修改工厂信息
        /// </summary>
        /// <param name="facId"></param>
        /// <param name="facinfo"></param>
        public void EditFacInfo(string dicFacinfo)
        {          
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicFacinfo);
            dp.UpdateData(table, new string[] { "FACID" }, mst);
        }

        /// <summary>
        /// 删除工厂信息
        /// </summary>
        /// <param name="facId"></param>
        public void DeleteFacInfo(string facId)
        {         
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("FACID", facId);
            dp.DeleteData(table, mst);
        }
        public DataSet GetFacCodeList()
        {          
            string fieldlist = "distinct facCode".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            return dp.GetData(table, fieldlist, null, out count);
        }
    }
}
