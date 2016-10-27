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
    public  class tVersion_Mark_info
    {
        public tVersion_Mark_info()
        {

        }
        public System.Data.DataSet QueryVersionInfoByWo(string WO)
        {
            //            OracleCommand cmd = new OracleCommand();
            //            cmd.CommandText = @"SELECT A.WOID,A.VERSION_NAME,A.VERSION_VALUES,B.USERNAME,A.RECDATE FROM SFCB.B_VERSION_INFO A,SFCB.B_USER_INFO B 
            //                                WHERE A.USER_ID=B.USERID(+) AND  A.WOID=:WO ORDER BY A.VERSION_NAME ";
            //            cmd.Parameters.Add("WO", OracleDbType.Varchar2, 25).Value = WO;
            //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            int count = 0;
            string table = "SFCB.B_VERSION_INFO A,SFCB.B_USER_INFO B";
            string fieldlist = "A.WOID,A.VERSION_NAME,A.VERSION_VALUES,B.USERNAME,A.RECDATE ";
            string filter = "A.USER_ID=B.USERID AND  A.WOID={0} ORDER BY A.VERSION_NAME";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", WO);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

        }
        public string InsertVersionInfo(string WO, string lsVersionMark, string sId)
        {
            //OracleCommand cmd = new OracleCommand();
            //cmd.CommandText = " DELETE FROM SFCB.B_VERSION_INFO WHERE WOID=:woid ";
            //cmd.Parameters.Add("woid", OracleDbType.Varchar2, 25).Value = WO;
            //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            //foreach (Entity.tVersionMark item in lsVersionMark)
            //{
            //    cmd = new OracleCommand();
            //    cmd.CommandText = "insert into sfcb.b_version_info(woid,version_name,version_values,user_id,recdate)values(:woid,:version_name,:version_values,:sId,sysdate)";
            //    cmd.Parameters.Add("woid", OracleDbType.Varchar2, 20).Value = WO;
            //    cmd.Parameters.Add("version_name", OracleDbType.Varchar2, 50).Value = item.VERSION_NAME;
            //    cmd.Parameters.Add("version_values", OracleDbType.Varchar2, 100).Value = item.VERSION_VALUES;
            //    cmd.Parameters.Add("sId", OracleDbType.Varchar2, 100).Value = sId;
            //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
            //}
            //return "OK";
            IList<IDictionary<string, object>> VersionMark = MapListConverter.JsonToListDictionary(lsVersionMark);

            string table = "SFCB.B_VERSION_INFO";
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> parms = new Dictionary<string, object>();
            parms.Add("WOID", WO);
            dp.DeleteData(table, parms);

            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
                IDictionary<string, object> mst = new Dictionary<string, object>();
                foreach (Dictionary<string, object> dic in VersionMark)
                {
                    mst = new Dictionary<string, object>();
                    mst.Add("WOID", dic["WOID"]);
                    mst.Add("VERSION_NAME", dic["VERSION_NAME"]);
                    mst.Add("VERSION_VALUES", dic["VERSION_VALUES"]);
                    mst.Add("user_id", sId);
                    mst.Add("recdate", System.DateTime.Now);
                    dp.AddData(tx, "sfcb.b_version_info", mst);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                tx.Rollback();
                return ex.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public System.Data.DataSet QueryMarkBitByPn(string PN)
        {
            //            OracleCommand cmd = new OracleCommand();
            //            cmd.CommandText = @"SELECT A.PARTNUMBER,A.MARK_BIT_NAME,A.MARK_BIT_VALUES,B.USERNAME,A.RECDATE FROM SFCB.B_MARK_BIT_INFO A,SFCB.B_USER_INFO B 
            //                                WHERE A.USER_ID=B.USERID(+) AND  A.PARTNUMBER=:PART ORDER BY A.MARK_BIT_NAME";
            //            cmd.Parameters.Add("PART", OracleDbType.Varchar2, 25).Value = PN;
            //            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            int count = 0;
            string table = "SFCB.B_MARK_BIT_INFO A,SFCB.B_USER_INFO B";
            string fieldlist = "A.PARTNUMBER,A.MARK_BIT_NAME,A.MARK_BIT_VALUES,B.USERNAME,A.RECDATE ";
            string filter = "A.USER_ID=B.USERID AND  A.PARTNUMBER={0} ORDER BY A.MARK_BIT_NAME";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("PARTNUMBER", PN);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }
        public string InsertMarkBitInfo(string PN, string lsVersionMark, string sId)
        {
            // OracleCommand cmd = new OracleCommand();
            // cmd.CommandText = "DELETE FROM SFCB.B_MARK_BIT_INFO WHERE PARTNUMBER=:part";
            // cmd.Parameters.Add("part", OracleDbType.Varchar2, 25).Value = PN;
            // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            //foreach (Entity.tVersionMark item in lsVersionMark)
            // {
            //     cmd = new OracleCommand();
            //     cmd.CommandText = "insert into sfcb.b_mark_bit_info(partnumber,mark_bit_name,mark_bit_values,user_id,recdate)values(:partnumber,:mark_bit_name,:mark_bit_values,:sId,sysdate)";
            //     cmd.Parameters.Add("partnumber", OracleDbType.Varchar2, 20).Value = PN;
            //     cmd.Parameters.Add("mark_bit_name", OracleDbType.Varchar2, 50).Value = item.MARK_BIT_NAME;
            //     cmd.Parameters.Add("mark_bit_values", OracleDbType.Varchar2, 100).Value = item.MARK_BIT_VALUES;
            //     cmd.Parameters.Add("sId", OracleDbType.Varchar2, 100).Value = sId;
            //     BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
            // }
            // return "OK";
            IList<IDictionary<string, object>> VersionMark = MapListConverter.JsonToListDictionary(lsVersionMark);

            string table = "SFCB.B_MARK_BIT_INFO";
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> parms = new Dictionary<string, object>();
            parms.Add("PARTNUMBER", PN);
            dp.DeleteData(table, parms);

            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
                IDictionary<string, object> mst = new Dictionary<string, object>();
                foreach (Dictionary<string, object> dic in VersionMark)
                {
                    mst = new Dictionary<string, object>();
                    mst.Add("PARTNUMBER", dic["PARTNUMBER"]);
                    mst.Add("MARK_BIT_NAME", dic["MARK_BIT_NAME"]);
                    mst.Add("MARK_BIT_VALUES", dic["MARK_BIT_VALUES"]);
                    mst.Add("user_id", sId);
                    mst.Add("recdate", System.DateTime.Now);
                    dp.AddData(tx, "sfcb.b_mark_bit_info", mst);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                tx.Rollback();
                return ex.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
