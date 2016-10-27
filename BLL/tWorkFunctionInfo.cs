using System;
using System.Collections.Generic;
using System.Text;
//using System.Data.OracleClient;
using System.Data;

namespace BLL
{
    public partial class tWorkFunctionInfo
    {
       
        public tWorkFunctionInfo()
        {
            
        }

     /*   public  System.Data.DataSet GetALLWorkFunctionInfo()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select wfid as 功能编号,rolecaption as 角色名称,wfcaption as  功能名称,wfdesc as 功能说明,workurl as 功能连接 from sfcb.tWorkFunctionInfo";
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        public void InsertToWorkFunctionInfo(Entity.tWorkFunctionInfoTable wfi)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "insert into sfcb.tWorkFunctionInfo (wfId,rolecaption,wfcaption,wfdesc,workurl) VALUES (@wfId,@rolecaption,@wfcaption,@wfdesc,@workurl) ";
            cmd.Parameters.Add("wfId", OracleType.VarChar, 50).Value = wfi.wfid;
            cmd.Parameters.Add("rolecaption", OracleType.VarChar, 50).Value = wfi.rolecaption;
            cmd.Parameters.Add("wfcaption", OracleType.VarChar, 50).Value = wfi.wfcaption;
            cmd.Parameters.Add("wfdesc", OracleType.VarChar, 50).Value = wfi.wfdesc;
            cmd.Parameters.Add("workurl", OracleType.VarChar, 50).Value = wfi.workurl;
             BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        }

        public void UpdateWorkFunctionInfo(Entity.tWorkFunctionInfoTable wfi)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "update sfcb.tWorkFunctionInfo set rolecaption=@rolecaption,wfcaption=@wfcaption,wfdesc=@wfdesc,workurl=@workurl where wfId=@wfId  ";
            cmd.Parameters.Add("wfId", OracleType.VarChar, 50).Value = wfi.wfid;
            cmd.Parameters.Add("rolecaption", OracleType.VarChar, 50).Value = wfi.rolecaption;
            cmd.Parameters.Add("wfcaption", OracleType.VarChar, 50).Value = wfi.wfcaption;
            cmd.Parameters.Add("wfdesc", OracleType.VarChar, 50).Value = wfi.wfdesc;
            cmd.Parameters.Add("workurl", OracleType.VarChar, 50).Value = wfi.workurl;
             BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        }

        public void DeleteWorkFunctionInfo(string wfid)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "delete from sfcb.tWorkFunctionInfo where wfid=@wfId ";
            cmd.Parameters.Add("wfId", OracleType.VarChar, 50).Value = wfid;
             BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        }
      * */
    }
}