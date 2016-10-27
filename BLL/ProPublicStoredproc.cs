using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using SystemObject;
using SrvComponent;

namespace BLL
{
    public partial class ProPublicStoredproc
    {
        
        public ProPublicStoredproc()
        {
           
        }

        public DataSet GetListStationType()
        {

            string table = "SFCB.B_STATION_TYPE_NAME";
            string fieldlist = "stationtype,stationtypename";
            int count = 0;
              //待实现 order by 20150203 michael
            List<OrderKey> lok = new List<OrderKey>();
            OrderKey ok = new OrderKey();
            ok.KeyName = "StationType";
            ok.IsAsc = true;
            lok.Add(ok);
            return TransactionManager.GetData(table, fieldlist,null,null,lok,null, out count);
        }

        public DataSet GetStoredprocValues(string Pro)  //查询存储过程需要的参数
        {         
            string table = null;
            string fieldlist = null;
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = null;
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)
            {
                 table = "sfcb.b_work_type_parameters";
                 fieldlist = "argument_name";                
                 mst = new Dictionary<string, object>();
                mst.Add("object_name", Pro);
                mst.Add("IN_OUT", "IN");
            }
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
            {
                 table = "all_arguments";
                 fieldlist = "argument_name";
                 mst = new Dictionary<string, object>();
                mst.Add("owner".ToUpper(), "SFCB");
                mst.Add("object_name".ToUpper(), Pro);
                mst.Add("IN_OUT".ToUpper(), "IN");
            }
             
            return dp.GetData(table, fieldlist,  mst, out count);
        }


        public DataSet GetStoredProcValuesParam(string Pro) //查询存储过程需要的参数 20130613
        {           
            string table = null;
            string fieldlist = null;
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = null;
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)
            {
                 table = "sfcb.b_work_type_parameters";
                 fieldlist = "argument_name,IN_OUT";               
                mst = new Dictionary<string, object>();
                mst.Add("object_name", Pro);
            }

            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
            {
                table = "all_arguments";
                fieldlist = "argument_name,IN_OUT";
                mst = new Dictionary<string, object>();
                mst.Add("owner".ToUpper(), "SFCB");
                mst.Add("object_name".ToUpper(), Pro);
            }

            return dp.GetData(table, fieldlist, mst, out count);
        }

        public DataSet GetSystemInputData()  //查询出系统所需要的参数
        {       
            string fieldlist = "worktypename,code,param";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);    
            return dp.GetData("SFCB.B_WORK_TYPE_NAME", fieldlist, null, out count);
        }
    

        /// <summary>
        /// 获取存储过程明细
        /// </summary>
        /// <param name="StationId"></param>
        /// <returns></returns>
        public DataSet GetSystemStoredproc(int StationId)
        {
            int count = 0;
            string table = "SFCB.B_WORK_TYPE a ,SFCB.b_Work_Type_Name b";
            string fieldlist = "a.STATION_TYPE,a.Storedproc,a.StoredprocIdx,a.WORK_TYPE_INDEX,a.SECOND,a.WorkType,a.Fork,a.InRam,a.LastOne,b.WorkTypeName,b.Code,b.PARAM";
            string filter = "a.WorkType = b.WorkType and a.Station_Type={0}";
            //string group = "a.STATION_TYPE";
            IList<OrderKey> order = new List<OrderKey>();
            OrderKey od1 = new OrderKey();
            od1.KeyName = "a.StoredprocIdx";
            od1.IsAsc = true;
            OrderKey od2 = new OrderKey();
            od2.KeyName = "a.WORK_TYPE_INDEX ";
            od2.IsAsc = true;
            order.Add(od1);
            order.Add(od2);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("Station_Type", StationId);
            return  TransactionManager.GetData(table, fieldlist, filter, mst, order, null, out count);

        }     

        /// <summary>
        /// 20151015替换方法 20151131后停用
        /// </summary>
        /// <param name="woId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetWOSnRule(string woId)
        {          
            int count = 0;
            string table = "SFCR.T_WO_SN_RULE a,SFCR.T_WO_INFO b";
            string fieldlist = "a.woId,a.sntype,a.snprefix,a.snpostfix,a.snstart,a.snend,a.snleng,a.ver,a.reve,a.recdate,b.partnumber,b.qty,b.pver,a.usenum";
            string filter = "a.woId=b.woId and  a.woId={0}";          
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }

        public System.Data.DataSet Get_WO_SnRule(string woId)
        {
            string table = "SFCR.T_WO_SN_RULE";
            string fieldlist = "WOID,SNTYPE,SNPREFIX,SNPOSTFIX,SNSTART,SNEND,SNLENG,VER,REVE,RECDATE,USENUM";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        public string GetStockInNumber()
        {
            string C_SEQ = string.Empty;
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)
            {             
                string PRGNAME = "STOCKIN";         
   
                string table = "sfcb.sequence";
                string fieldlist = "current_value,increment";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("NAME", PRGNAME);
                DataTable dtSEQ = dp.GetData(table, fieldlist, mst, out count).Tables[0];

                C_SEQ = "SFS" + dtSEQ.Rows[0][0].ToString().PadLeft(8, '0');

                mst = new Dictionary<string, object>();
                mst.Add("current_value", Convert.ToInt32(dtSEQ.Rows[0][0].ToString()) + Convert.ToInt32(dtSEQ.Rows[0][1].ToString()));
                mst.Add("NAME", PRGNAME);
                dp.UpdateData(table, new string[] { "NAME" }, mst);

            }
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
            {
                string table = "DUAL";
                string fieldlist = "'SFS' ||LPAD(TO_CHAR(SEQ_STOCKIN.NEXTVAL), 7, '0') AS OUTSEQ";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);                
                DataSet ds = dp.GetData(table, fieldlist, null, out count);
                C_SEQ= ds.Tables[0].Rows[0][0].ToString();
            }

            return C_SEQ;
;
        }   

    }
}
