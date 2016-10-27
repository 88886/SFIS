using System;
//using Oracle.DataAccess.Client;
using MySql.Data.MySqlClient;
using Entity;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public partial interface IMsSqlLib
    {   
        //void CloseDataBase();
        //void ConnectDataBase();
    
        DataSet ExecuteDataSet(string Sql, Dictionary<string, string> dic, string _DbString);
       
        //System.Data.DataTable gettb(string sql, int start, int count, string tablename);    
        //object sqlExecuteScalar(MySqlCommand cmd);
        //string PublicUpdateExecteNonQuery(string Table, string Fileds, string sFilters, MySqlParameter [] Parameter);
        //string PublicInsertExecteNonQuery(string Table, string Fileds, string sFilters, MySqlParameter[] Parameter);
        //string PublicDeleteExecteNonQuery(string Table, string Fileds, MySqlParameter[] Parameter);  
        //string SaveERPInfo(DataTable dt);
        //string SaveERPItem(DataTable dt);

    }
}
