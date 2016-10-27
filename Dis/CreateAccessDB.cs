using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using ADOX;
using System.Data;
using System.IO;

namespace Dis
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
                this.oleComm.Connection.Close();
                this.oleComm.Dispose();
                this.oleConn.Close();
                this.oleConn.Dispose();
            }
            catch
            {
            }
        }
        public CreateAccessDb(string _strpath)
        {
            this.DBPath = _strpath + "dbtemp.mdb";
            if (File.Exists(this.DBPath))
                File.Delete(this.DBPath);
            this.ConnectStr =@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.DBPath;
            oleConn.ConnectionString = this.ConnectStr;
            oleComm.Connection = oleConn;
        }
        public CreateAccessDb(string _strpath,bool isCreateDB)
        {
            this.DBPath = _strpath + "dbtemp.mdb";
            if (isCreateDB)
            {
                if (File.Exists(this.DBPath))
                    File.Delete(this.DBPath);
            }
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
        public bool _CreateWoSnrule()
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

                table.Columns.Append("woId", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("sntype", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("snstart", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("snend", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("snprefix", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("snpostfix", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("ver", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("reve", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("snleng", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("usenum", ADOX.DataTypeEnum.adInteger, 20);
                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CreateBomData()
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
                table.Columns.Append("kptype", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("qty", ADOX.DataTypeEnum.adVarWChar, 20);
                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CreateKpDetalt()
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

        public bool CreateSmtSoftKpnumber()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "smtsoftkplist";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                column.Name = "IID";
                column.Type = ADOX.DataTypeEnum.adInteger;
                column.DefinedSize = 255;
                column.Properties["AutoIncrement"].Value = true;
                table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("smtsoftname", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("pcbside", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("lineId", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("stationId", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("kpnumber", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("feeder", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("stationcount", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("loction", ADOX.DataTypeEnum.adLongVarWChar, 5000);

                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CreateTargetPlan()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "TargetPlan";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                column.Name = "IDT";
                column.Type = ADOX.DataTypeEnum.adInteger;
                column.DefinedSize = 255;
                column.Properties["AutoIncrement"].Value = true;
                table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("workdate", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("class", ADOX.DataTypeEnum.adVarWChar, 10);
                table.Columns.Append("locstation", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("woId", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("partnumber", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("line", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("targetqty", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("starttime", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("endtime", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("side", ADOX.DataTypeEnum.adVarWChar, 20);
                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 建立Access产品料号表
        /// </summary>
        /// <returns></returns>
        public bool CreatetProduct()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "tProduct";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                column.Name = "ID";
                column.Type = ADOX.DataTypeEnum.adInteger;
                column.DefinedSize = 255;
                column.Properties["AutoIncrement"].Value = true;
                table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("partnumber", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("sortname", ADOX.DataTypeEnum.adVarWChar, 40);
                table.Columns.Append("productname", ADOX.DataTypeEnum.adVarWChar, 100);
                table.Columns.Append("productcolor", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("productdesc", ADOX.DataTypeEnum.adVarWChar, 150);
                table.Columns.Append("other", ADOX.DataTypeEnum.adVarWChar, 20);

                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 建立Access工单表
        /// </summary>
        /// <returns></returns>
        public bool CreatetWorkOrderInfo()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "tWorkOrderInfo";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                column.Name = "ID";
                column.Type = ADOX.DataTypeEnum.adInteger;
                column.DefinedSize = 255;
                column.Properties["AutoIncrement"].Value = true;
                table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                string sColumns = "woId,poId,qty,wostate,userId,partnumber,bomver,inputgroup,outputgroup,routgroupId,bomnumber,wotype";

                string[] sColumn = sColumns.Split(',');
                for (int i = 0; i < sColumn.Length; i++)
                {
                    table.Columns.Append(sColumn[i], ADOX.DataTypeEnum.adVarWChar, 100);
                }
                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 建立Access线体表
        /// </summary>
        /// <returns></returns>
        public bool CreatetLineInfo()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "tLineInfo";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                column.Name = "ID";
                column.Type = ADOX.DataTypeEnum.adInteger;
                column.DefinedSize = 255;
                column.Properties["AutoIncrement"].Value = true;
                table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("lineId", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("linedesc", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("startIpAddr", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("endIpAddr", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("wsId", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("userId", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("plotId", ADOX.DataTypeEnum.adVarWChar, 50);

                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 产品本地测试记录
        /// </summary>
        /// <returns></returns>
        public bool CreatetTestInfo()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "tTestInfo";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                column.Name = "ID";
                column.Type = ADOX.DataTypeEnum.adInteger;
                column.DefinedSize = 255;
                column.Properties["AutoIncrement"].Value = true;
                table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("serialnumber", ADOX.DataTypeEnum.adVarWChar, 30);
                table.Columns.Append("recdate", ADOX.DataTypeEnum.adVarWChar, 20);
                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 建立Access途程表
        /// </summary>
        /// <returns></returns>
        public bool CreatetCraftInfo()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "tCraftInfo";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                column.Name = "ID";
                column.Type = ADOX.DataTypeEnum.adInteger;
                column.DefinedSize = 255;
                column.Properties["AutoIncrement"].Value = true;
                table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("craftId", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("craftname", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("craftparameterurl", ADOX.DataTypeEnum.adVarWChar, 230);
                table.Columns.Append("beworkseg", ADOX.DataTypeEnum.adVarWChar, 50);

                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 建立Access tStationRec表
        /// </summary>
        /// <returns></returns>
        public bool CreatetStationRec()
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "tStationRec";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                //column.Name = "ID";
                //column.Type = ADOX.DataTypeEnum.adInteger;
                //column.DefinedSize = 255;
                //column.Properties["AutoIncrement"].Value = true;
                //table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                //table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("woId", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("partnumber", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("ProductName", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("craftId", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("craftname", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("workDate", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("worksection", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("class", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("classDate", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("lineId", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("passQty", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("failQty", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("rPassQty", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("rFailQty", ADOX.DataTypeEnum.adVarWChar, 80);
                table.Columns.Append("flag", ADOX.DataTypeEnum.adVarWChar, 80);

                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 建立Access 不良代码表
        /// </summary>
        /// <returns></returns>
        public bool CreatetErrorCode()  //20130129
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "tErrorCode";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                //column.Name = "ID";
                //column.Type = ADOX.DataTypeEnum.adInteger;
                //column.DefinedSize = 255;
                //column.Properties["AutoIncrement"].Value = true;
                //table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                //table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("ErrorCode", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("ErrorDesc", ADOX.DataTypeEnum.adVarWChar, 200);
                table.Columns.Append("ErrorDesc2", ADOX.DataTypeEnum.adVarWChar, 245);
                table.Columns.Append("Recdate", ADOX.DataTypeEnum.adVarWChar, 80);

                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 建立Access 原因代码表
        /// </summary>
        /// <returns></returns>
        public bool CreatetReasonCode()  //20130129
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "tReasonCode";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                //column.Name = "ID";
                //column.Type = ADOX.DataTypeEnum.adInteger;
                //column.DefinedSize = 255;
                //column.Properties["AutoIncrement"].Value = true;
                //table.Columns.Append(column, ADOX.DataTypeEnum.adInteger, 255);
                //table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);

                table.Columns.Append("ReasonCode", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("ReasonType", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("ReasonDesc", ADOX.DataTypeEnum.adVarWChar, 200);
                table.Columns.Append("ReasonDesc2", ADOX.DataTypeEnum.adVarWChar, 245);
                table.Columns.Append("DutyStation", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("RecDate", ADOX.DataTypeEnum.adVarWChar, 40);
                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 建立Access 维修表
        /// </summary>
        /// <returns></returns>
        public bool CreatetRepair()  //20130129
        {
            try
            {
                ADOX.Catalog MyCataLog = new Catalog();
                MyCataLog.ActiveConnection = obj;
                ADOX.Table table = new Table();
                table.Name = "tRepairInfo";
                ADOX.Column column = new Column();
                column.ParentCatalog = MyCataLog;
                string[] arr = new string[50];
                string sColumns = "ErrorCode,ErrorDesc,ReasonCode,ReasonDesc,esn,woId,partnumber,ProductName,CraftId,CraftName,lineId,Location,TClassDate,TCLASS,TWorkSection,RClassDate,RCLASS,RWorkSection,FLAG";
                arr = sColumns.Split(',');
                for (int x = 0; x < arr.Length; x++)
                {
                    table.Columns.Append(arr[x], ADOX.DataTypeEnum.adVarWChar, 100);
                }

                MyCataLog.Tables.Append(table);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 执行SQL语句

        //private OleDbConnection conn;
        private OleDbDataAdapter oda = new OleDbDataAdapter();
        private DataSet myds = new DataSet();

        public DataSet GetDatatable(string sql)
        {
            //conn = new OleDbConnection(this.ConnectStr);
            //conn.Open();
            oleConn.Open();
            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(sql, oleConn);
            oda.Fill(ds, "temp");
            oleConn.Close();
            return ds;
        }

        public bool ExecuteOracleCommand(string sql)
        {
            try
            {
               //conn = new OleDbConnection(this.ConnectStr);
                oleConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = oleConn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                oleConn.Close();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
