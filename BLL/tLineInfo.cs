using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using SystemObject;
using GenericUtil;

namespace BLL
{
    public partial class tLineInfo
    {
       
        public tLineInfo()
        {
           
        }

        /// <summary>
        /// 获取所有线别信息
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetAllLineInfo()
        {
            int count = 0;
            string table = "SFCB.B_LINE_INFO l,SFCB.B_WS_INFO w";
            string fieldlist = "l.lineId as 线别,l.linedesc as 线体描述,l.userId as 负责人,w.wsname as 车间名称,l.startIpAddr as 开始IP地址,l.endIpAddr as 结束IP地址,l.wsId as 车间编号,l.plotId as 计划ID".ToUpper();
            string filter = "w.wsId=l.wsId";
            return TransactionManager.GetData(table, fieldlist, filter, null, null, null, out count);
    
        }
        public System.Data.DataSet GetLineInfo(IDictionary<string, object> mst, string Fields)
        {
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            if (string.IsNullOrEmpty(Fields))
                Fields = "LINEID,LINEDESC,STARTIPADDR,ENDIPADDR,WSID,USERID";
            return dp.GetData("SFCB.B_LINE_INFO", Fields, mst, out count);
        }

        public List<string> GetLineList()
        {
            List<string> LsLine = new List<string>();
            string table = "SFCB.B_LINE_INFO";
            string fieldlist = "LINEID,LINEDESC";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);      
            DataTable dt= dp.GetData(table, fieldlist, null, out count).Tables[0];
            DataView dv = new DataView(dt);
            dv.Sort = dt.Columns[0].ToString();
            DataTable dt2 = dv.ToTable();
            foreach (DataRow dr in dt2.Rows)
            {
                LsLine.Add(dr["LINEID"].ToString());
            }
            return LsLine;
        }

        /// <summary>
        /// 根据线体编号修改线体信息
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="lineinfo"></param>
        public void EditLineInfo(string   dicLineinfo)
        {          
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicLineinfo);           
            dp.UpdateData("SFCB.B_LINE_INFO", new string[] { "LINEID" }, mst);
        }

        /// <summary>
        /// 新增线体信息
        /// </summary>
        /// <param name="lineinfo"></param>
        public void InsertLineInfo(string dicLineinfo)
        {         
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicLineinfo);
            dp.AddData("SFCB.B_LINE_INFO", dic);
        }

        /// <summary>
        /// 删除线体
        /// </summary>
        /// <param name="lineId"></param>
        public void DeleteLineInfo(string lineId)
        {           
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("LINEID", lineId);
            dp.DeleteData("SFCB.B_LINE_INFO", mst);
        }
    }
}
