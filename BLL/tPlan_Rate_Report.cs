using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using System.Data.Common;
using SrvComponent;
using SystemObject;
using GenericUtil;

namespace BLL
{
    public partial class tPlan_Rate_Report
    {
        public tPlan_Rate_Report()
        { }
        public DataSet getPlan_Rate_Set_bydate(string workmonth,string workdate)
        {
            int count = 0;
            string table = "sfcr.t_plan_rate_report";
            string fieldlist = "work_month,work_date,work_class,business_unit,productname,partnumber,productcolor,section_name,craftname,cumulative_plan,cumulative_qty,target_qty,actual_qty,userid,recdate ";
            string filter = "";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(workmonth) && string.IsNullOrEmpty(workdate))
            {
                filter = "work_month={0} ";
                mst.Add("work_month", workmonth);
            }
            if (string.IsNullOrEmpty(workmonth) && !string.IsNullOrEmpty(workdate))
            {
                filter = "work_date={0} ";
                mst.Add("work_date", workdate);
            }
            if (!string.IsNullOrEmpty(workmonth) && !string.IsNullOrEmpty(workdate))
            {
                filter = "work_month={0} and work_date={1} ";
                mst.Add("work_month", workmonth);
                mst.Add("work_date", workdate);
            }
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }
        public DataSet getPlan_Rate_Set_bypn(string workmonth, string workdate,string pn)
        {
            int count = 0;
            string table = "sfcr.t_plan_rate_report a,sfcb.b_user_info b";
            string fieldlist = @"a.work_month,a.work_date,a.work_class,a.business_unit,a.productname,a.partnumber,a.productcolor,a.section_name,
                               a.craftname,a.cumulative_plan,a.cumulative_qty,a.target_qty,a.actual_qty,b.username,a.recdate";
            string filter = "a.userid=b.userid ";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(workdate) && string.IsNullOrEmpty(pn))
            {
                filter += "and a.work_date={0}";
                mst.Add("work_date", workdate);
            }
            if (string.IsNullOrEmpty(workdate) && !string.IsNullOrEmpty(pn))
            {
                filter += "and a.partnumber={0}";
                mst.Add("partnumber", pn);
            }
            if (!string.IsNullOrEmpty(workdate) && !string.IsNullOrEmpty(pn))
            {
                filter += "and a.work_date={0} and a.partnumber={1}";
                mst.Add("work_date", workdate);
                mst.Add("partnumber", pn);
            }
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }
        public string insert_Plan_Rate(string dicplan)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> Dic = MapListConverter.JsonToDictionary(dicplan);
            Dic.Add("CUMULATIVE_PLAN", 0);
            Dic.Add("CUMULATIVE_QTY", 0);
            Dic.Add("ACTUAL_QTY", 0);
            Dic.Add("RECDATE", System.DateTime.Now);
            dp.AddData("sfcr.t_plan_rate_report", Dic);


            //IDictionary<string, object> Dic = MapListConverter.JsonToDictionary(dicplan);
            //DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            //DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            //IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            //Dic.Add("CUMULATIVE_PLAN", 0);
            //Dic.Add("CUMULATIVE_QTY", 0);
            //Dic.Add("ACTUAL_QTY", 0);
            //Dic.Add("RECDATE", System.DateTime.Now);
            //dp.AddData(tx, "sfcr.t_plan_rate_report", Dic);
            return "OK";
        }
        public string delete_Plan_Rate(string dicplan)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> Dic = MapListConverter.JsonToDictionary(dicplan);
            dp.DeleteData("sfcr.t_plan_rate_report", Dic);
            //IDictionary<string, object> Dic = MapListConverter.JsonToDictionary(dicplan);
            //DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            //DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            //IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            //IDictionary<string, object> mst = new Dictionary<string, object>();
            //mst.Add("WORK_DATE", Dic["WORK_DATE"]);
            //mst.Add("WORK_CLASS", Dic["WORK_CLASS"]);
            //mst.Add("BUSINESS_UNIT", Dic["BUSINESS_UNIT"]);
            //mst.Add("PARTNUMBER", Dic["PARTNUMBER"]);
            //mst.Add("SECTION_NAME", Dic["SECTION_NAME"]);
            //mst.Add("CRAFTNAME", Dic["CRAFTNAME"]);
            //dp.DeleteData(tx, "sfcr.t_plan_rate_report", mst);
            return "OK";
        }
    }
}
