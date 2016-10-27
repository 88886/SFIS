using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace SFIS_PRINT_SYSTEM.BLL
{
    public class cdbAccess
    {
        private OleDbConnection conn;
        private OleDbDataAdapter oda = new OleDbDataAdapter();
        private DataSet myds = new DataSet();

        public cdbAccess()
        {
            conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\Database\dbtemp.mdb");
        }

        public DataTable GetDatatable(string sql)
        {
            conn.Open();
            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(sql, conn);
            oda.Fill(ds, "temp");
            conn.Close();
            return ds.Tables[0];
        }
        private  bool ExeSqlBulkCopy(string TableName, DataTable dt)
        {
            //try
            //{
            //    conn.Open();
            //    using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
            //    {
            //        bulkcopy.DestinationTableName = TableName;
            //        bulkcopy.WriteToServer(dt);
            //    }
            //    return true;
            //}
            //catch
            //{
               return false;
            //}
        }
        public bool ExecuteSqlCommand(string sql)
        {
            try
            {

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
