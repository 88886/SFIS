using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using MySql.Data.MySqlClient;
//using Oracle.DataAccess.Client;
using System.Data;
using System.Globalization;


namespace DAL
{
    public partial class MsSqlLib : DAL.IMsSqlLib
    {
        #region 成员变量
        private string _strDBString;
        private string _AppPath;
        private string _AppPathLog;
        private string _strSharePath;
        private string _strError;

        private MySqlTransaction _trans;

        #endregion
        #region 属性
        public string StrError
        {
            get { return _strError; }
            set { _strError = value; }
        }

        public string AppPath
        {
            get
            {
                return _AppPath;
            }
        }

        public string AppPathLog
        {
            get
            {
                return _AppPathLog;
            }
        }

        public string SharePath
        {
            get
            {
                return _strSharePath;
            }
            set
            {
                _strSharePath = value;
            }
        }

        //public MySqlCommand Cmd
        //{
        //    get
        //    {
        //        return _cmd;
        //    }
        //    set
        //    {
        //        _cmd = value;
        //    }
        //}
        //public SqlDataReader Reader
        //{
        //    get
        //    {
        //        return _reader;
        //    }
        //    set
        //    {
        //        _reader = value;
        //    }
        //}
        //public SqlDataAdapter Adapter
        //{
        //    get
        //    {
        //        return _adapter;
        //    }
        //    set
        //    {
        //        _adapter = value;
        //    }
        //}
        public MySqlTransaction Trans
        {
            get
            {
                return _trans;
            }
            set
            {
                _trans = value;
            }
        }
        #endregion

        #region 构造函数
        public MsSqlLib(string strDBString)
        {
            _strDBString = strDBString;
            Init();

        }

        ~MsSqlLib()
        {
            // this.CloseDataBase();
        }
        #endregion

        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        private void Init()
        {
            //_conn = null;
            //_reader = null;
            _trans = null;
            _strSharePath = "";
            _strError = string.Empty;

            string path = Assembly.GetCallingAssembly().Location;

            _AppPath = path.Substring(0, path.LastIndexOf(@"\"));
            _AppPathLog = path.Substring(0, path.LastIndexOf(@"\")) + @"\Log";

            if (!Directory.Exists(_AppPathLog))
            {
                Directory.CreateDirectory(_AppPathLog);
            }

            try
            {
                // _conn = new MySqlConnection(_strDBString);
                // _conn.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

   
 

        #region 分页功能
        /// <summary>
        /// 分页返回DataTable
        /// </summary>
        /// <param name="sql">查询的sql语句</param>
        /// <param name="i">从第i行开始返回</param>
        /// <param name="j">共返回j行记录</param>
        /// <param name="tablename">返回DataSet中的表明</param>
        /// <returns>返回DataTable</returns>
        public DataTable gettb(string sql, int start, int count, string tablename)
        {
            DataSet myds = new DataSet();
            using (MySqlConnection _conn = new MySqlConnection(this._strDBString))
            {
                _conn.Open(); 
                MySqlDataAdapter sda = new MySqlDataAdapter(sql, _conn);

                
                sda.Fill(myds, start, count, tablename);
                _conn.Close();
                return myds.Tables[tablename];
            }
        }

        #endregion

        int xxx;
        #region command对象
        //public void ExecteNonQuery(MySqlCommand cmd)
        //{
        //    try
        //    {
           
        //        using (MySqlConnection cn = new MySqlConnection(this._strDBString))
        //        {
        //            try
        //            {

        //                cn.Open();
        //                cmd.Connection = cn;
        //                cmd.CommandTimeout = 84100;
        //                xxx = cmd.ExecuteNonQuery();
        //                cmd.Connection.Close();
        //                cmd.Dispose();

        //            }
        //            catch (Exception e)
        //            {
        //                throw e;
        //            }
        //            finally
        //            {
        //                cn.Close();
        //                cn.Dispose();
        //            }
        //        }
              
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public void ExecteNonQueryArr(List<MySqlCommand> cmd)
        //{         

        //    using (MySqlConnection cn = new MySqlConnection(this._strDBString))
        //    {
        //        cn.Open();
        //        MySqlTransaction sTran = cn.BeginTransaction();
        //        try
        //        {                   
        //            foreach (MySqlCommand _cmd in cmd)
        //            {
        //             //   ((Oracle.DataAccess.Client.MySqlCommand)_cmd).BindByName = true;
        //                _cmd.Connection = cn;
        //                _cmd.Transaction = sTran;
        //                _cmd.CommandTimeout = 84100;
        //                _cmd.ExecuteNonQuery();
        //                _cmd.Dispose();
        //            }
        //            sTran.Commit();
        //            //throw new Exception(_strDBString);
        //        }
        //        catch (Exception ex)
        //        {
        //            sTran.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            cn.Close();
        //            cn.Dispose();
        //        }
        //    }


        //}

        //private void inputRecordAsync(IAsyncResult ar)
        //{
        //    MySqlCommand MySqlCommand = ar.AsyncState as MySqlCommand;
        //    int completeParam = MySqlCommand.EndExecuteNonQuery(ar);
        //    MySqlCommand.Connection.Close();
        //}


        //public object sqlExecuteScalar(MySqlCommand cmd)
        //{
        // //   ((Oracle.DataAccess.Client.MySqlCommand)cmd).BindByName = true;
        //    object obj = null;
        //    using (MySqlConnection cn = new MySqlConnection(this._strDBString))
        //    {
        //        try
        //        {
        //            cmd.Connection = cn;
        //            cn.Open();
        //            obj = cmd.ExecuteScalar();
        //            cmd.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            cn.Close();
        //            cn.Dispose();
        //        }
        //    }

        //    return obj;


        //}
        //public  DataSet ExecuteDataSet(MySqlCommand cmd)
        //{         
    
        //    DataSet dsData;
        //    using (MySqlConnection cn = new MySqlConnection(this._strDBString))
        //    {
        //        try
        //        {
        //            string str = "T" + DateTime.Now.ToString("hhmmss").ToString();
        //            cmd.Connection = cn;                  
        //            cn.Open();
        //            MySqlDataAdapter _adapter = new MySqlDataAdapter(cmd);
        //            dsData = new DataSet();
        //            _adapter.Fill(dsData, str);
        //            _adapter.Dispose();             
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            cn.Close();
        //            cn.Dispose();
                    
        //        }
        //    }
        //    return dsData;

        //}

        
        
        //private MySqlDataReader ExecuteDataReader(MySqlCommand cmd)
        //{
        //   // ((Oracle.DataAccess.Client.MySqlCommand)cmd).BindByName = true;
            
        //    MySqlDataReader myReader;
        //    using (MySqlConnection cn = new MySqlConnection(this._strDBString))
        //    {
        //        try
        //        {
        //            cmd.Connection = cn;
        //            cn.Open();
        //            myReader = cmd.ExecuteReader();
        //            cmd.Dispose();

        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            cn.Close();
        //            cn.Dispose();
        //        }
        //    }
        //    return myReader;

        //}
        //private DataTable ExecuteDataTable(MySqlCommand cmd)
        //{
        //   // ((Oracle.DataAccess.Client.MySqlCommand)cmd).BindByName = true;
        //    string str = "T" + DateTime.Now.ToString("hhmmss").ToString();
        //    DataTable dtData = new DataTable(str);
        //    using (MySqlConnection cn = new MySqlConnection(this._strDBString))
        //    {
        //        try
        //        {

        //            cmd.Connection = cn;
        //            cn.Open();
        //            cmd.CommandTimeout = 60 * 10;
        //            MySqlDataAdapter _adapter = new MySqlDataAdapter();
        //            _adapter.SelectCommand = cmd;
        //            _adapter.Fill(dtData);
        //            cmd.Dispose();
        //            _adapter.Dispose();

        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            cn.Close();
        //            cn.Dispose();
        //        }
        //    }
        //    return dtData;

        //}
        //private DataRowCollection ExecuteDataRow(MySqlCommand cmd, string tablename)
        //{
        //    //((Oracle.DataAccess.Client.MySqlCommand)cmd).BindByName = true;
        //    DataSet dsData;
        //    using (MySqlConnection cn = new MySqlConnection(this._strDBString))
        //    {
        //        try
        //        {
        //            cmd.Connection = cn;
        //            cn.Open();
        //            MySqlDataAdapter _adapter = new MySqlDataAdapter(cmd);
        //            dsData = new DataSet();
        //            _adapter.Fill(dsData, tablename);
        //            _adapter.Dispose();

        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            cn.Close();
        //            cn.Dispose();
        //        }
        //    }
        //    return dsData.Tables[tablename].Rows;

        //}

        //private DataView ExecuteDataView(MySqlCommand cmd, string strTable)
        //{

        // //   ((Oracle.DataAccess.Client.MySqlCommand)cmd).BindByName = true;
        //    DataSet dsData;

        //    DataView result = null;
        //    using (MySqlConnection cn = new MySqlConnection(this._strDBString))
        //    {
        //        try
        //        {
        //            cmd.Connection = cn;
        //            cn.Open();
        //            MySqlDataAdapter _adapter = new MySqlDataAdapter(cmd);
        //            dsData = new DataSet();
        //            _adapter.Fill(dsData, strTable);
        //            result = dsData.Tables[strTable].DefaultView;
        //            _adapter.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            cn.Close();
        //            cn.Dispose();
        //        }
        //    }
        //    return result;

        //}


        //public string ExecteNonQueryTransaction(List<MySqlCommand> cmd)
        //{
        //    //ConnectDataBase();
   
        //    using (MySqlConnection cn = new MySqlConnection(this._strDBString))
        //    {
        //        cn.Open();
        //        MySqlTransaction Tran = cn.BeginTransaction();
        //        try
        //        {                  
        //            foreach (MySqlCommand item in cmd)
        //            {
        //               // ((Oracle.DataAccess.Client.MySqlCommand)item).BindByName = true;
        //                item.Connection = cn;
        //                item.Transaction = Tran;
        //                item.CommandTimeout = 84100;
        //                if (item.ExecuteNonQuery() < 1)
        //                    throw new Exception("Fail:Update Total Zero");
        //                // cmd[i].Connection.Close();
        //                item.Dispose();
        //                // _conn.Close();
        //            }
        //            Tran.Commit();
        //            cn.Close();
        //            return "OK";
        //        }
        //        catch (Exception e)
        //        {
        //            Tran.Rollback();
        //            cn.Close();
        //            return e.Message;
        //        }
        //        finally
        //        {
        //            cn.Close();
        //            cn.Dispose();
        //        }
        //    }

        //}


//        public string SaveERPInfo(DataTable dt)
//        {

//            if ((dt == null) || (dt.Rows.Count < 1))
//            {
//                throw new Exception("Input DataTable Is Null");
//            }


//            using (MySqlConnection cn = new MySqlConnection(this._strDBString))
//            {
//                try
//                {

//                    using (MySqlCommand cmd = cn.CreateCommand())
//                    {

//                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
//                        {

//                            MySqlCommandBuilder ocb = new MySqlCommandBuilder(da);
//                            //必须要有SelectCommand, da.SelectCommand对象是cmd的引用
//                            da.SelectCommand.CommandText = @"select SHOP_CODE,
//                                                            SHOP_NAME,
//                                                            ORDER_NO,
//                                                            MEMBER_NAME,
//                                                            SALE_NO,
//                                                            PAY_METHOD,
//                                                            SALE_TIME,
//                                                            ORDER_CREATE_TIME,
//                                                            PAY_TIME,
//                                                            SHIP_TIME,
//                                                            ORDER_CHECK_OP,
//                                                            ORDER_CHECK_TIME,
//                                                            GOODS_AMOUNT,
//                                                            FREIGHT_AMOUNT,
//                                                            ADDITIONAL_AMOUNT,
//                                                            HAS_TAX,
//                                                            PMT_AMOUNT,
//                                                            SALE_AMOUNT,
//                                                            LOGI_NAME,
//                                                            LOGI_NO,
//                                                            BRANCH_NAME,
//                                                            BRANCH_BN,
//                                                            DELIVERY_NO,
//                                                            CONSIGNEE,
//                                                            CONSIGNEE_AREA,
//                                                            CONSIGNEE_ADDR,
//                                                            CONSIGNEE_ZIP,
//                                                            CONSIGNEE_TEL,
//                                                            CONSIGNEE_MOBILE,
//                                                            CONSIGNEE_EMAIL
//                                                        from SFCR.Z_SHOPEX_SALES where limit 0";
//                            da.InsertCommand = ocb.GetInsertCommand();
//                            //da.Fill(dtDB);   如果上面指定了InsertCommand 就不用fill
//                            da.Update(dt);
                           
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    return ex.Message;

//                }
//                finally
//                {
//                    cn.Close();
//                    dt.Dispose();
//                }

//            }
//            return "OK";

//        }


//        public string SaveERPItem(DataTable dt)
//        {

//            if ((dt == null) || (dt.Rows.Count < 1))
//            {
//                throw new Exception("Input DataTable Is Null");
//            }

//            using (MySqlConnection cn = new MySqlConnection(this._strDBString))
//            {
//                try
//                {
//                    using (MySqlCommand cmd = cn.CreateCommand())
//                    {
//                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
//                        {

//                            MySqlCommandBuilder ocb = new MySqlCommandBuilder(da);
//                            //必须要有SelectCommand, da.SelectCommand对象是cmd的引用
//                            da.SelectCommand.CommandText = @"select order_no,
//                                                               PARTNUMBER,
//                                                               PRODUCTDESC,
//                                                               SPEC_NAME,
//                                                               PRICE,
//                                                               NUMS,
//                                                               PMT_PRICE,
//                                                               SALE_PRICE,
//                                                               APPORTION_PMT,
//                                                               SALES_AMOUNT,
//                                                               ITEM_TYPE
//                                                          from SFCR.Z_SHOPEX_SALES_DETAIL where limit 0";
//                            da.InsertCommand = ocb.GetInsertCommand();
//                            //da.Fill(dtDB);   如果上面指定了InsertCommand 就不用fill
//                            da.Update(dt);
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    return ex.Message;

//                }
//                finally
//                {
//                    cn.Close();
//                    dt.Dispose();
//                }
//            }
//            return "OK";

//        }

        //public string ExecteNonUpd(MySqlCommand cmdSel, MySqlCommand cmdIns, MySqlCommand cmdUpd)
        //{    
        //    MySqlDataReader myReader;

        //    string TR_SN = "";
        //    using (MySqlConnection cn = new MySqlConnection(this._strDBString))
        //    {

        //        try
        //        {
        //            cn.Open();
        //            cmdSel.Connection = cn;
        //            myReader = cmdSel.ExecuteReader();
        //            while (myReader.Read())
        //            {
        //                TR_SN = myReader[0].ToString();
        //            }
        //            myReader.Close();
        //            cmdSel.Dispose();

        //            if (string.IsNullOrEmpty(TR_SN))
        //            {
                       
        //                cmdIns.Connection = cn;
        //                cmdIns.ExecuteNonQuery();
        //                cmdIns.Connection.Close();
        //                cmdIns.Dispose();
        //                TR_SN = "S"+DateTime.Now.ToString("yy") + GetWeekOfYear() + "0000000";
        //            }
        //            else
        //            {
                        
        //                cmdUpd.Connection = cn;
        //                cmdUpd.ExecuteNonQuery();
        //                cmdUpd.Connection.Close();
        //                cmdUpd.Dispose();
        //            }

        //            return TR_SN;

        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            cn.Close();
        //            cn.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// 获取当前时间是第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetWeekOfYear()
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear.ToString().PadLeft(2, '0');
        }
        #endregion


        #region  MySQL
        public string ExecteNonUpd(int pring_qty,string _DbString)
        {
            MySqlCommand cmd_Sel = new MySqlCommand();          
            cmd_Sel.CommandText = "select SN_CURRENT from sfcr.R_SN_RULE where SN_RULE=concat('S',(right(year(curdate()),2)),LPAD(weekofyear(curdate()),2,0))";
            MySqlCommand cmd_Ins = new MySqlCommand();
            cmd_Ins.CommandText = "insert into sfcr.R_SN_RULE (sn_rule,sn_current,curr_seq) VALUES ( concat('S',(right(year(curdate()),2)),LPAD(weekofyear(curdate()),2,0)), CONCAT('S', (RPAD(concat((right(year(curdate()),2)),LPAD(weekofyear(curdate()),2,0)),12,0))+@qty),@qty )";
            cmd_Ins.Parameters.Add("qty", MySqlDbType.Int32).Value = pring_qty;
            MySqlCommand cmd_Upd = new MySqlCommand();
            cmd_Upd.CommandText = "update sfcr.R_SN_RULE set sn_current=CONCAT('S',right(sn_current ,length(sn_current)-1)+@qty),curr_seq=curr_seq+@qty,updated_date=NOW() where SN_RULE= concat('S',(right(year(curdate()),2)),LPAD(weekofyear(curdate()),2,0))";
            cmd_Upd.Parameters.Add("qty", MySqlDbType.Int32).Value = pring_qty;

            MySqlDataReader myReader;

            string TR_SN = "";
            using (MySqlConnection cn = new MySqlConnection(_DbString))
            {
                try
                {
                    cn.Open();
                    cmd_Sel.Connection = cn;
                    myReader = cmd_Sel.ExecuteReader();
                    while (myReader.Read())
                    {
                        TR_SN = myReader[0].ToString();
                    }
                    myReader.Close();
                    cmd_Sel.Dispose();

                    if (string.IsNullOrEmpty(TR_SN))
                    {

                        cmd_Ins.Connection = cn;
                        cmd_Ins.ExecuteNonQuery();
                        cmd_Ins.Connection.Close();
                        cmd_Ins.Dispose();
                        TR_SN = "S" + DateTime.Now.ToString("yy") + GetWeekOfYear() + "0000000";
                    }
                    else
                    {

                        cmd_Upd.Connection = cn;
                        cmd_Upd.ExecuteNonQuery();
                        cmd_Upd.Connection.Close();
                        cmd_Upd.Dispose();
                    }

                    return TR_SN;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cn.Close();
                    cn.Dispose();
                }
            }
        }

        public DataSet ExecuteDataSet(string Sql, Dictionary<string, string> dic, string _DbString)
        {

            DataSet dsData;
            using (MySqlConnection cn = new MySqlConnection(_DbString))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Sql;
                    foreach (KeyValuePair<string, string> kv in dic)
                    {
                        cmd.Parameters.Add(kv.Key, MySqlDbType.VarChar).Value = kv.Value;
                    }
                    string str = "T" + DateTime.Now.ToString("hhmmss").ToString();
                    cmd.Connection = cn;
                    cn.Open();
                    MySqlDataAdapter _adapter = new MySqlDataAdapter(cmd);
                    dsData = new DataSet();
                    _adapter.Fill(dsData, str);
                    _adapter.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cn.Close();
                    cn.Dispose();

                }
            }
            return dsData;

        }

        #endregion
    }
}
