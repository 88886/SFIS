using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;
using ADOX;
using System.IO;

namespace SFIS_PRINT_SYSTEM.BLL
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

        /// <summary>
        /// 关闭连接
        /// </summary>
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
        /// <summary>
        /// 构造器
        /// </summary>
        public CreateAccessDb()
        {
            if (!Directory.Exists(Application.StartupPath + "\\Database"))
                Directory.CreateDirectory(Application.StartupPath + "\\Database");
            if (File.Exists(Application.StartupPath + @"\Database\dbtemp.mdb"))
                File.Delete(Application.StartupPath + @"\Database\dbtemp.mdb");
            if (!File.Exists(Application.StartupPath + @"\Database\dbtemp.mdb"))
            {
                this.DBPath = Application.StartupPath + "\\Database\\dbtemp.mdb";
                this.ConnectStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.DBPath;
                oleConn.ConnectionString = this.ConnectStr;
                oleComm.Connection = oleConn;

                _CreateMDB();
                _CreateWoSnrule();
                _CreateWoPwd();
            }
        }

        object obj;
        /// <summary>
        /// 创建数据库
        /// </summary>
        public void _CreateMDB()
        {
            //使用ADOX 创建ACCESS数据库

            ADOX.Catalog MyCataLog = new Catalog();
            obj = MyCataLog.Create(_connectStr);
        }

        /// <summary>
        /// 添加数据表
        /// </summary>
        /// <returns></returns>
        private bool _CreateWoSnrule()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "wosnrule";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                column.Name = "ID";
                column.Type = ADOX.DataTypeEnum.adInteger;
                column.DefinedSize = 255;
                column.Properties["AutoIncrement"].Value = true;
                table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("poid", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("woId", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("sntype", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("snstart", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("snend", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("snprefix", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("snpostfix", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("ver", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("reve", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("usenum", ADOX.DataTypeEnum.adInteger, 20);
                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加数据表
        /// </summary>
        /// <returns></returns>
        private bool _CreateWoPwd()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "UsePwd";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                column.Name = "ID";
                column.Type = ADOX.DataTypeEnum.adInteger;
                column.DefinedSize = 255;
                column.Properties["AutoIncrement"].Value = true;
                table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("mac", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("woId", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("pwdtype", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("pwdval", ADOX.DataTypeEnum.adVarWChar, 30);
                MyCataLog.Tables.Append(table);
           
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加数据表
        /// </summary>
        /// <returns></returns>
        private bool CreateBomData()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "bomdata";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                column.Name = "ID";
                column.Type = ADOX.DataTypeEnum.adInteger;
                column.DefinedSize = 255;
                column.Properties["AutoIncrement"].Value = true;
                table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("kpnumber", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("loction", ADOX.DataTypeEnum.adLongVarWChar, 5000);
                table.Columns.Append("kpdesc", ADOX.DataTypeEnum.adVarWChar, 250);
                table.Columns.Append("kpgroup", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("bomver", ADOX.DataTypeEnum.adVarWChar, 20);
                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加数据表
        /// </summary>
        /// <returns></returns>
        private bool CreateKpDetalt()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "kpdetalt";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                column.Name = "ID";
                column.Type = ADOX.DataTypeEnum.adInteger;
                column.DefinedSize = 255;
                column.Properties["AutoIncrement"].Value = true;
                table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("kpnumber", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("kpdesc", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("loction", ADOX.DataTypeEnum.adLongVarWChar, 5000);
                table.Columns.Append("kpgroup", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("partnumber", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("lineId", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("pcbside", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("bomver", ADOX.DataTypeEnum.adVarWChar, 20);
                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
