using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;

namespace BLL
{
    public partial class tMachineId
    {
        
        public tMachineId()
        {
           
        }

        string table = "SFCB.B_MACHINE_INFO";
        /// <summary>
        /// 获取所有机器/工站信息
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetMachineInfo(string machineId)
        {
            string fieldlist = "machineId,lineId,fixtureId,machinedesc,IpAddress,IpAddress1,IpAddress2,note".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(machineId))
                mst.Add("MACHINEID", machineId);
            return dp.GetData(table, fieldlist, mst, out count);
        }       
        /// <summary>
        /// 新增机器/工站信息
        /// </summary>
        /// <param name="machineinfo"></param>
        public void InsertMachineInfo(string dicMachineinfo)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicMachineinfo);
            dp.AddData(table, dic);
        }

        /// <summary>
        /// 修改机器/工站信息
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="machineInfo"></param>
        public void EditMachineInfo(string dicMachineInfo)
        {         
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicMachineInfo);
            dp.UpdateData(table, new string[] { "MACHINEID" }, mst);
        }

        /// <summary>
        /// 删除机器/工站信息
        /// </summary>
        /// <param name="machineId"></param>
        public void DeleteMachineInfo(string machineId)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MACHINEID", machineId);
            dp.DeleteData(table, mst);
        }

        public List<string> GetSMTLineList()
        {          
            List<string> lsLine = new List<string>();
            string fieldlist = "distinct lineid".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            DataTable dt = dp.GetData(table, fieldlist, null, out count).Tables[0];
            foreach ( DataRow dr in dt.Rows)
            {
                lsLine.Add(dr[0].ToString());
            }

            return lsLine;
        }
    }
}
