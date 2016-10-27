using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
   public partial class tScrapInfo
    {
       public tScrapInfo()
       {
       }

       //public string  InsertScrapInfo(Entity.tScrapInfoTable sit)
       //{
       // string res="";  

       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "insert into sfcr.tScrapInfo (SCRAPNO,ReasonCode,LocStation,MEMO) VALUES (@SCRAPNO,@ReasonCode,@LocStation,@MEMO)";
       //    cmd.Parameters.Add("SCRAPNO", MySqlDbType.VarChar, sit.scrapno.Length).Value = sit.scrapno;
       //    cmd.Parameters.Add("ReasonCode", MySqlDbType.VarChar, sit.ReasonCode.Length).Value = sit.ReasonCode;
       //    cmd.Parameters.Add("LocStation", MySqlDbType.VarChar, sit.LocStation.Length).Value = sit.LocStation;
       //    cmd.Parameters.Add("MEMO", MySqlDbType.VarChar, sit.MEMO.Length).Value = sit.MEMO;

       //    try
       //    {
       //        BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
             
       //    }
       //    catch (Exception ex)
       //    {
       //        res= ex.Message;

       //    }

       //    return res;
           
       //}

       //public string InsertScrapList(Entity.tScrapInfoTable sit)
       //{
       //    string res = "";  
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "insert into sfcr.tScrapList (ESN,SCRAPNO,UserId,PartNumber,woId,LocStation,ReasonCode,StoreId) VALUES (@ESN,@SCRAPNO,@UserId,@PartNumber,@woId,@LocStation,@ReasonCode,@StoreId)";
       //    cmd.Parameters.Add("ESN", MySqlDbType.VarChar, sit.ESN.Length).Value = sit.ESN;
       //    cmd.Parameters.Add("SCRAPNO", MySqlDbType.VarChar, sit.scrapno.Length).Value = sit.scrapno;
       //    cmd.Parameters.Add("UserId", MySqlDbType.VarChar, sit.UserId.Length).Value = sit.UserId;
       //    cmd.Parameters.Add("PartNumber", MySqlDbType.VarChar, sit.PartNumber.Length).Value = sit.PartNumber;
       //    cmd.Parameters.Add("woId", MySqlDbType.VarChar,  sit.woId.Length).Value = sit.woId;
       //    cmd.Parameters.Add("LocStation", MySqlDbType.VarChar, sit.LocStation.Length).Value = sit.LocStation;
       //    cmd.Parameters.Add("ReasonCode", MySqlDbType.VarChar, sit.ReasonCode.Length).Value = sit.ReasonCode;
       //    cmd.Parameters.Add("StoreId", MySqlDbType.VarChar, sit.StoreId.Length).Value = sit.StoreId;

       //    try
       //    {
       //        BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

       //    }
       //    catch (Exception ex)
       //    {
       //        res = ex.Message;

       //    }

       //    return res;
       //}
    }
}
