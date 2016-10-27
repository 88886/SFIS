using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Data;
using System.Data.OracleClient;

namespace 测试用例1
{
    public partial class MsSqlLib
    {
        #region 成员变量
        private string _strDBString;
        private string _AppPath;
        private string _AppPathLog;
        private string _strSharePath;
        private string _strError;

        private OracleTransaction _trans;
        private OracleConnection _conn;
        private OracleCommand cmd;
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

        //public OracleCommand Cmd
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
        public OracleTransaction Trans
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
            this.CloseDataBase();
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
                _conn = new OracleConnection(_strDBString);
                _conn.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseDataBase()
        {
            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }

        /// <summary>
        /// 连接到数据库
        /// </summary>
        public void ConnectDataBase()
        {
            if (_conn.State == ConnectionState.Closed)
            {
                try
                {
                    _conn.Open();
                }
                catch (Exception e)
                {
                    throw e;
                }
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
            using (OracleConnection _conn = new OracleConnection(this._strDBString))
            {
                _conn.Open();
                OracleDataAdapter sda = new OracleDataAdapter(sql, _conn);

                sda.Fill(myds, start, count, tablename);
                _conn.Close();
                return myds.Tables[tablename];
            }
        }

        #endregion

        #region
        /// <summary>
        /// 执行一个sql语句，返回datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string sql)
        {
            try
            {
                DataTable dt = new DataTable();
                cmd = new OracleCommand(sql, _conn);
                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                cmd.Dispose();
                adapter.Dispose();
                return dt;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        int xxx;
        #region command对象
        public void ExecteNonQuery(OracleCommand cmd)
        {
            try
            {
                
                    cmd.Connection = _conn;
                    cmd.CommandTimeout = 84100;
                  xxx=  cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();
               
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ExecteNonQueryArr(List<OracleCommand> cmd)
        {
            //ConnectDataBase();
            try
            {
                using (OracleConnection cn = new OracleConnection(this._strDBString))
                {
                    cn.Open();
                    foreach (OracleCommand _cmd in cmd)
                    {
                        _cmd.Connection = cn;
                        _cmd.CommandTimeout = 84100;
                        _cmd.ExecuteNonQuery();
                        _cmd.Dispose();
                    }
                    cn.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void inputRecordAsync(IAsyncResult ar)
        {
            //OracleCommand OracleCommand = ar.AsyncState as OracleCommand;
            //int completeParam = OracleCommand.EndExecuteNonQuery(ar);
            //OracleCommand.Connection.Close();
        }


        public object sqlExecuteScalar(OracleCommand cmd)
        {
            try
            {
                object obj = null;
                using (OracleConnection cn = new OracleConnection(this._strDBString))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    obj = cmd.ExecuteScalar();
                    cmd.Dispose();
                    cn.Close();
                }
                
                return obj;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        
        }
        public DataSet ExecuteDataSet(OracleCommand cmd)
        {
            try
            {
                DataSet dsData;
                using (OracleConnection cn = new OracleConnection(this._strDBString))
                {
                    string str = "T" + DateTime.Now.ToString("hhmmss").ToString();
                    cmd.Connection = cn;
                    cn.Open();
                    OracleDataAdapter _adapter = new OracleDataAdapter(cmd);
                    dsData = new DataSet();
                    _adapter.Fill(dsData, str);
                    _adapter.Dispose();
                    cn.Close();
                }
                return dsData;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private OracleDataReader ExecuteDataReader(OracleCommand cmd)
        {
            try
            {
                OracleDataReader myReader;
                using (OracleConnection cn = new OracleConnection(this._strDBString))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    myReader = cmd.ExecuteReader();
                    cmd.Dispose();
                    cn.Close();
                }
                return myReader;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private DataTable ExecuteDataTable(OracleCommand cmd)
        {
            try
            {
                string str = "T" + DateTime.Now.ToString("hhmmss").ToString();
                DataTable dtData = new DataTable(str);
                using (OracleConnection cn = new OracleConnection(this._strDBString))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.CommandTimeout = 60 * 10;
                    OracleDataAdapter _adapter = new OracleDataAdapter();
                    _adapter.SelectCommand = cmd;
                   
                    _adapter.Fill(dtData);
                    cmd.Dispose();
                    _adapter.Dispose();
                    cn.Close();
                }
                return dtData;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private DataRowCollection ExecuteDataRow(OracleCommand cmd, string tablename)
        {
            ConnectDataBase();
            try
            {
                DataSet dsData;
                using (OracleConnection cn = new OracleConnection(this._strDBString))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    OracleDataAdapter _adapter = new OracleDataAdapter(cmd);
                    dsData = new DataSet();
                    _adapter.Fill(dsData, tablename);
                    _adapter.Dispose();
                    cn.Close();
                }
                return dsData.Tables[tablename].Rows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private DataView ExecuteDataView(OracleCommand cmd, string strTable)
        {
            ConnectDataBase();

            DataSet dsData;
            try
            {
                DataView result = null;
                using (OracleConnection cn = new OracleConnection(this._strDBString))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    OracleDataAdapter _adapter = new OracleDataAdapter(cmd);
                    dsData = new DataSet();
                    _adapter.Fill(dsData, strTable);
                     result = dsData.Tables[strTable].DefaultView;
                    _adapter.Dispose();
                    cn.Close();
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public string ExecteNonQueryTransaction(List<OracleCommand> cmd)
        {
            //ConnectDataBase();

            try
            {
                using (OracleConnection cn = new OracleConnection(this._strDBString))
                {
                    cn.Open();
                    OracleTransaction Tran = cn.BeginTransaction();
                    try
                    {
                        int x = 0;
                        foreach (OracleCommand item in cmd)
                        {
                            item.Connection = cn;
                            item.Transaction = Tran;
                            item.CommandTimeout = 84100;
                            if (item.ExecuteNonQuery() < 1)
                                throw new Exception("Fail:Update Total Zero");
                            // cmd[i].Connection.Close();
                            item.Dispose();
                            // _conn.Close();
                        }
                        Tran.Commit();
                        cn.Close();
                        return "OK";
                    }
                    catch (Exception e)
                    {
                        Tran.Rollback();
                        cn.Close();
                        return e.Message;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
