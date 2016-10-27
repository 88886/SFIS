using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;


namespace FrmBLL
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

        public string  ExecuteAccessCommandTable(DataTable dt,string sTable,string Counum,string Col)
        {
            try
            {                   
                conn.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(string.Format("select {0} from {1}  where {2}=0 ", Counum,sTable,Col), conn);   //建立空表结构
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da); //根据dt修改的情况自动生成updateCommand传递给dataAdapter
                da.Update(dt);   //dtable已经初始化
                return "OK";
            }
            catch (Exception ee)
            {
                return ee.Message;
            }
        }
        public string ExecuteAccessCommandProduct(DataTable dt, string sTable, string Counum, string Col)
        {
            try
            {
                conn.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(string.Format("select {0} from {1}  where {2}=0 ", Counum, sTable, Col), conn);   //建立空表结构
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da); //根据dt修改的情况自动生成updateCommand传递给dataAdapter
                da.Update(dt);   //dtable已经初始化
                return "OK";
            }
            catch (Exception ee)
            {
                return ee.Message;
            }
        }
        public bool ExecuteOracleCommand(string sql)
        {
            try
            {
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
