using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DAL;

namespace BLL
{
    public class BllMsSqllib
    {
        private readonly static BLL.BllMsSqllib instance = new BllMsSqllib();

        public static BLL.BllMsSqllib Instance
        {
            get { return BllMsSqllib.instance; }
        }
        static DAL.MsSqlLib msqllib = null;

        public DAL.MsSqlLib MsSqlLib
        {
            get { return BllMsSqllib.msqllib; }
        }
        static BllMsSqllib()
        {
            msqllib = new MsSqlLib(BLL.ServerConfig.Instance.ConnStr);
        }

        public DataTable ExecuteQuerySQL(string sSQL)
        {
            return msqllib.ExecuteDataTable(sSQL);
        }

        public void ExecteSQLNonQuery(string sSQL)
        {
            msqllib.ExecteNonQuery(sSQL);
        }

        public DataView ExecuteSQLDataView(string sSQL, string strTable)
        {
            return msqllib.ExecuteDataView(sSQL, strTable);
        }

        public string GetSeqBasics
        {
            get { return this.MsSqlLib.GetSeqBasics; }
        }
        public string GetSeq
        {
            get { return this.MsSqlLib.GetSqlSequence; }
        }
    }
}
