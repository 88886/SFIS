using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADOX;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;

namespace Stocking_In
{
   public class CreateAccessDb
    {

        private string _connectStr;
        private string _DBPath;

        public string DBPath
        {
            get { return _DBPath; }
            set { _DBPath = value; }
        }
        public string ConnectStr
        {
            get { return _connectStr; }
            set { _connectStr = value; }
        }

        private OleDbConnection oleConn = new OleDbConnection();
        OleDbCommand oleComm = new OleDbCommand();

        public void CloseDB()
        {
            try
            {
                this.oleConn.Close();
            }
            catch
            {
            }
        }

        public CreateAccessDb()
        {
            this.DBPath = Application.StartupPath + "\\Database\\dbtemp.mdb";
            this.ConnectStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.DBPath;
            oleConn.ConnectionString = this.ConnectStr;
            oleComm.Connection = oleConn;
        }

        object obj;

        // 创建数据库
        public void _CreateMDB()
        {
            //使用ADOX 创建ACCESS数据库

            ADOX.Catalog MyCataLog = new Catalog();
            obj = MyCataLog.Create(_connectStr);

        }

        //添加数据表
        public bool _Create_T_CHECK_SAP()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "T_CHECK_SAP";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                column.Name = "ID";
                column.Type = ADOX.DataTypeEnum.adInteger;
                column.DefinedSize = 255;
                column.Properties["AutoIncrement"].Value = true;
                table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("woId", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("PARTNUMBER", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("DATA_NO", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("DATA_QTY", ADOX.DataTypeEnum.adInteger, 30);             
                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }      

    }

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

        public string ExecuteAccessCommandTable(DataTable dt, string sTable, string Counum, string Col)
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
        public bool ExecuteOleDbCommand(string sql)
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
