using System;
using System.Collections.Generic;
using System.Text;
//using Oracle.DataAccess.Client;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL
{
    public partial class MsSqlLib : DAL.IMsSqlLib
    {
        //public System.Data.DataSet TablePublicSelect(string Table, string Fileds, string sFilters, MySqlParameter[] Parameter, IList<OrderKey> OrderKeys = null)
        //{
        //    string sSQL = "";
        //    MySqlCommand cmd = new MySqlCommand();
        //    sSQL = "select " + Fileds + " from " + Table;
        //    if (!string.IsNullOrEmpty(sFilters))
        //        sSQL = sSQL + " where  " + sFilters;
        //    if (OrderKeys != null)
        //    {
        //        string Ords = null;
        //        foreach (OrderKey od in OrderKeys)
        //        {
        //            if (od.IsAsc)
        //            {
        //                Ords += od.Colname + " ,";
        //            }
        //            else
        //            {
        //                Ords += od.Colname + " desc ,";
        //            }
        //        }
        //        sSQL = sSQL + " order by " + Ords.Substring(0, Ords.Length - 1);
        //    }

        //    cmd.CommandText = sSQL;
        //    if (Parameter != null)
        //    {
        //        cmd.Parameters.AddRange(Parameter);
        //    } 
        //    return ExecuteDataSet(cmd);
        //}

        //public string PublicUpdateExecteNonQuery(string Table, string Fileds, string sFilters,MySqlParameter[] Parameter)
        //{
        //    try
        //    {
        //        string sSQL = "";
        //        MySqlCommand cmd = new MySqlCommand();
        //        sSQL = "update " + Table + " set " + Fileds + " where " + sFilters;
        //        cmd.CommandText = sSQL;              
        //        cmd.Parameters.AddRange(Parameter);    
        //        ExecteNonQuery(cmd);
        //        return "OK";
        //    }
        //    catch (Exception ex)

        //    {
        //        return ex.Message;
        //    }

        //}

        //public string PublicInsertExecteNonQuery(string Table, string Fileds, string sFilters, MySqlParameter[] Parameter)
        //{
        //    try
        //    {
        //        string sSQL = "";
        //        MySqlCommand cmd = new MySqlCommand();
        //        sSQL = "Insert Into " + Table + "  (" + Fileds + ") values  ( " + sFilters+" )";
        //        cmd.CommandText = sSQL;
        //        cmd.Parameters.AddRange(Parameter);        
        //        ExecteNonQuery(cmd);
        //        return "OK";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //public string PublicDeleteExecteNonQuery(string Table, string Fileds, MySqlParameter[] Parameter)
        //{
        //    try
        //    {
        //        string sSQL = "";
        //        MySqlCommand cmd = new MySqlCommand();
        //        sSQL = "delete from " + Table + " where " + Fileds;
        //        cmd.CommandText = sSQL;
        //        cmd.Parameters.AddRange(Parameter);
        //        ExecteNonQuery(cmd);
        //        return "OK";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

    }
}
