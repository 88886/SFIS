using System;
using System.Collections.Generic;
using System.Text;
using SystemObject;
using GenericProvider;
using GenericUtil;
using System.Data;

namespace BLL
{
  public  class tStationrecount
    {
      public tStationrecount()
      {
      }
      BLL.tWoInfo woinfo = new tWoInfo();
      public System.Data.DataSet GetStationRec(string Stime, string Etime)
      {
          //MySqlCommand cmd = new MySqlCommand();
          //cmd.CommandText = "select woId,craftId,workDate,worksection,class,classDate,lineId,passQty,failQty,rPassQty,rFailQty,flag from " +
          //                 " SFCR.T_STATION_RECOUNT where workDate||worksection>=@Sdate and workDate||worksection<=@Edate";
          //cmd.Parameters.Add("Sdate", MySqlDbType.VarChar, Stime.Length).Value = Stime;
          //cmd.Parameters.Add("Edate", MySqlDbType.VarChar, Stime.Length).Value = Etime;

          //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
          int count = 0;
          string table = "SFCR.T_STATION_RECOUNT";
          string fieldlist = "woId,craftId,workDate,worksection,class,classDate,lineId,passQty,failQty,rPassQty,rFailQty,flag".ToUpper();
          string filter = "CONCAT(workDate,worksection)>={0} and CONCAT(workDate,worksection)<={1}";         
          IDictionary<string, object> mst = new Dictionary<string, object>();
          mst.Add("workDate1", Stime);
          mst.Add("workDate2", Etime);
          return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

      }

      public System.Data.DataSet Get_YieldRate(string  dicstring)
      {
         // string Param = string.Empty;
         // MySqlCommand cmd = new MySqlCommand();

         // string Colnums=string.Empty;
         // if (sr.Show_woId)
         //     Colnums += "woId,";
         // if (sr.Show_CraftId)
         //     Colnums += "CRAFTID,";
         // if (sr.Show_WorkData)
         //     Colnums += "WORKDATE,";
         // if (sr.Show_PartNumber)
         //     Colnums += "PARTNUMBER,";
         // if (sr.Show_ProductName)
         //     Colnums += "PRODUCTNAME,";
         // if (sr.Show_Line)
         //     Colnums += "LINEID,";
         // if (sr.Show_ClassDate)
         //     Colnums += "CLASSDATE,";
         // if (sr.Show_Class)
         //     Colnums += "CLASS,";
         // if (sr.Show_RePassQTY)
         //     Colnums += "RPASSQTY,";
         // if (sr.Show_ReFailQTY)
         //     Colnums += "RFAILQTY,";
         // Colnums = Colnums.Substring(0, Colnums.Length - 1);

         // string SelectwoId=string.Empty;
      
         // string sSQL = string.Empty;
         //  sSQL = string.Format("select  {0},sum(passqty) as PASSQTY,sum(failqty) as FAILQTY  from sfcb.view_station_rec  where ",Colnums);
         //  if (!string.IsNullOrEmpty(sr.woId) && (sr.woId != "ALL"))
         //  {
         //      Param = string.Empty;
         //      for (int x = 0; x < sr.woId.Split(',').Length; x++)
         //      {
         //          Param += ("@C_WOID" + x.ToString() + ",");
         //          cmd.Parameters.Add("C_WOID" + x.ToString(), MySqlDbType.VarChar).Value = sr.woId.Split(',')[x];
         //      }
         //      sSQL += string.Format(" woId in ({0}) and ", Param.Substring(0, Param.Length - 1));          
         //  }
         //  if (!string.IsNullOrEmpty(sr.PartNumber) && (sr.PartNumber != "ALL"))
         // {
         //      Param = string.Empty;
         //     for (int x = 0; x < sr.PartNumber.Split(',').Length; x++)
         //     {
         //         Param += ("@C_MODEL" + x.ToString()+",");
         //         cmd.Parameters.Add("C_MODEL"+x.ToString(), MySqlDbType.VarChar).Value = sr.PartNumber.Split(',')[x];
         //     }
         //     sSQL += string.Format(" partnumber in ({0}) and ", Param.Substring(0, Param.Length-1));
             
         // }
         //  if (!string.IsNullOrEmpty(sr.CraftId) && (sr.CraftId != "ALL"))
         //  {
         //      Param = string.Empty;
         //      for (int x = 0; x < sr.CraftId.Split(',').Length; x++)
         //      {
         //          Param += ("@C_ROUTE" + x.ToString() + ",");
         //          cmd.Parameters.Add("C_ROUTE" + x.ToString(), MySqlDbType.VarChar).Value = sr.CraftId.Split(',')[x];
         //      }
         //      sSQL += string.Format(" CRAFTID IN ({0}) and ", Param.Substring(0, Param.Length - 1));
              
         // }
         // if (!string.IsNullOrEmpty(sr.Line) && (sr.Line != "ALL"))
         // {
         //     Param = string.Empty;
         //     for (int x = 0; x < sr.Line.Split(',').Length; x++)
         //     {
         //         Param += ("@C_LINE" + x.ToString() + ",");
         //         cmd.Parameters.Add("C_LINE" + x.ToString(), MySqlDbType.VarChar).Value = sr.Line.Split(',')[x];
         //     }
         //     sSQL += string.Format(" LINEID IN ({0}) and ", Param.Substring(0, Param.Length - 1));
         // }

         // sSQL += string.Format(" workDate||worksection>=@SDATE and workDate||worksection<=@EDATE  group by {0} ", Colnums);
         // cmd.CommandText = sSQL.ToUpper();
         // cmd.Parameters.Add("SDATE", MySqlDbType.VarChar).Value = sr.StartTime;
         // cmd.Parameters.Add("EDATE", MySqlDbType.VarChar ).Value = sr.EndTime;
         //// throw new Exception("[" + sr.StartTime + "]" + "[" + sr.EndTime + "]" + sSQL.ToUpper());
       
         // return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

          IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicstring);
          int count = 0;
          string table = "sfcb.view_station_rec";
          string fieldlist = null;
          if (Convert.ToBoolean(dic["SHOW_WOID"]))
              fieldlist += "WOID,";
          if (Convert.ToBoolean(dic["SHOW_CRAFTID"]))
              fieldlist += "CRAFTID,";
          if (Convert.ToBoolean(dic["SHOW_WORKDATE"]))
              fieldlist += "WORKDATE,";
          if (Convert.ToBoolean(dic["SHOW_PARTNUMBER"]))
              fieldlist += "PARTNUMBER,";
          if (Convert.ToBoolean(dic["SHOW_PRODUCTNAME"]))
              fieldlist += "PRODUCTNAME,";
          if (Convert.ToBoolean(dic["SHOW_LINEID"]))
              fieldlist += "LINEID,";
          if (Convert.ToBoolean(dic["SHOW_CLASSDATE"]))
              fieldlist += "CLASSDATE,";
          if (Convert.ToBoolean(dic["SHOW_CLASS"]))
              fieldlist += "CLASS,";
          if (Convert.ToBoolean(dic["SHOW_RPASSQTY"]))
              fieldlist += "PASSQTY,";
          if (Convert.ToBoolean(dic["SHOW_RFAILQTY"]))
              fieldlist += "RFAILQTY,";

          string group = fieldlist.Substring(0, fieldlist.Length - 1);
          fieldlist += "sum(passqty) as PASSQTY,sum(failqty) as FAILQTY";

          string filter =null;
          IDictionary<string, object> mst = new Dictionary<string, object>();
          int y=-1;
         string Param = string.Empty;
          if (!string.IsNullOrEmpty(dic["WOID"].ToString()) && (dic["WOID"].ToString() != "ALL"))
          {
              Param = string.Empty;
              for (int x = 0; x < dic["WOID"].ToString().Split(',').Length; x++)
              {
                  y++;
                  Param += ("{" + x.ToString() + "},");                 
                  mst.Add("WOID" + y.ToString(), dic["WOID"].ToString().Split(',')[x]);
              }
              filter += string.Format(" WOID IN ({0}) AND ", Param.Substring(0,Param.Length-1));
          }
          if (!string.IsNullOrEmpty(dic["PARTNUMBER"].ToString()) && (dic["PARTNUMBER"].ToString() != "ALL"))
          {
              Param = string.Empty;
              for (int x = 0; x < dic["PARTNUMBER"].ToString().Split(',').Length; x++)
              {
                  y++;
                  Param += ("{" + x.ToString() + "},");   
                  mst.Add("PARTNUMBER" + y.ToString(), dic["PARTNUMBER"].ToString().Split(',')[x]);
              }
              filter += string.Format(" PARTNUMBER IN ({0}) AND ", Param.Substring(0, Param.Length - 1));
          }
          if (!string.IsNullOrEmpty(dic["CRAFTID"].ToString()) && (dic["CRAFTID"].ToString() != "ALL"))
          {
              Param = string.Empty;
              for (int x = 0; x < dic["CRAFTID"].ToString().Split(',').Length; x++)
              {
                  y++;
                  Param += ("{" + x.ToString() + "},");   
                  mst.Add("CRAFTID" + y.ToString(), dic["CRAFTID"].ToString().Split(',')[x]);
              }
              filter += string.Format(" CRAFTID IN ({0}) AND ", Param.Substring(0, Param.Length - 1));
          }
          if (!string.IsNullOrEmpty(dic["LINEID"].ToString()) && (dic["LINEID"].ToString() != "ALL"))
          {
              Param = string.Empty;
              for (int x = 0; x < dic["LINEID"].ToString().Split(',').Length; x++)
              {
                  y++;
                  Param += ("{" + x.ToString() + "},");   
                  mst.Add("LINEID" + y.ToString(), dic["LINEID"].ToString().Split(',')[x]);
              }
              filter += string.Format(" LINEID IN ({0}) AND ", Param.Substring(0, Param.Length - 1));
          }
          y++;
          filter += " CONCAT(workDate,worksection)>={" + y.ToString() + "} ";
          mst.Add("workDate1", dic["STARTTIME"].ToString());
          y++;
          filter += " and CONCAT(workDate,worksection)<={" + y.ToString() + "} ";
           mst.Add("workDate2", dic["ENDTIME"].ToString());
          // throw new Exception("[" + MapListConverter.DictionaryToJson(mst) + "]");
          return TransactionManager.GetData(table, fieldlist, filter.ToUpper(), mst, null, group, out count);
      }

      public System.Data.DataSet Get_StationRec_Report(string Stime)
      {
          //MySqlCommand cmd = new MySqlCommand();
          //cmd.CommandText = "SELECT PARTNUMBER,CRAFTID,SUM(PASSQTY+FAILQTY) FROM SFCR.T_STATION_RECOUNT WHERE CRAFTID IN ('SMT_VI','STOCK_IN','DX_TEST') AND CLASSDATE=@Sdate GROUP BY PARTNUMBER,CRAFTID";
          //cmd.Parameters.Add("Sdate", MySqlDbType.VarChar, Stime.Length).Value = Stime;
          //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);


          int count = 0;
          string table = "SFCR.T_STATION_RECOUNT";
          string fieldlist = "PARTNUMBER,CRAFTID,SUM(PASSQTY+FAILQTY)";
          string filter = "CRAFTID IN ('SMT_VI','STOCK_IN','DX_TEST') AND CLASSDATE={0}";
          string group = "PARTNUMBER,CRAFTID";         
          IDictionary<string, object> mst = new Dictionary<string, object>();
          mst.Add("CLASSDATE", Stime);
          return TransactionManager.GetData(table, fieldlist, filter, mst, null, group, out count);
      }
      public string update_station_recout(string ROWID,string WOID, string CRAFTID, string WORKDATE, string WORKSECTION, string LINEID, int PASSQTY, int FAILQTY, int RPASSQTY, int RFAILQTY, string FLAG)
      {
        
          //string table = "SFCR.T_STATION_RECOUNT";
          //string fieldlist = "PASSQTY=PASSQTY+{0},FAILQTY=FAILQTY+{1},RPASSQTY=RPASSQTY+{2},RFAILQTY=RFAILQTY+{3}, FLAG={4}";
          //string filter = "WOID ={0} AND CRAFTID = {1} AND WORKDATE ={2} AND WORKSECTION = {3} AND LINEID = {4} LIMIT 1";

          //IDictionary<string, object> modFields = new Dictionary<string, object>();
          //modFields.Add("PASSQTY", PASSQTY);
          //modFields.Add("FAILQTY", FAILQTY);
          //modFields.Add("RPASSQTY", RPASSQTY);
          //modFields.Add("RFAILQTY", RFAILQTY);
          //modFields.Add("FLAG", FLAG);

          //IDictionary<string, object> keyVals = new Dictionary<string, object>();
          //keyVals.Add("WOID", WOID);
          //keyVals.Add("CRAFTID", CRAFTID);
          //keyVals.Add("WORKDATE", WORKDATE);
          //keyVals.Add("WORKSECTION", WORKSECTION);
          //keyVals.Add("LINEID", LINEID);

          //TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);
          //return "OK";
          string table = "SFCR.T_STATION_RECOUNT";
          string filter = "ROWID={0}";
          string fieldlist = null;
          IDictionary<string, object> modFields = null;
          if (PASSQTY != 0)
          {
              fieldlist = "PASSQTY=PASSQTY+{0}";
              modFields = new Dictionary<string, object>();
              modFields.Add("PASSQTY", PASSQTY);
          }
          if (FAILQTY != 0)
          {
              fieldlist = "FAILQTY=FAILQTY+{0}";
              modFields = new Dictionary<string, object>();
              modFields.Add("FAILQTY", FAILQTY);
          }
          if (RPASSQTY != 0)
          {
              fieldlist = "RPASSQTY=RPASSQTY+{0}";
              modFields = new Dictionary<string, object>();
              modFields.Add("RPASSQTY", RPASSQTY);
          }
          if (RFAILQTY != 0)
          {
              fieldlist = "RFAILQTY=RFAILQTY+{0}";
              modFields = new Dictionary<string, object>();
              modFields.Add("RFAILQTY", RFAILQTY);
          }

          IDictionary<string, object> keyVals = new Dictionary<string, object>();
          keyVals.Add("ROWID", ROWID);
         // keyVals.Add("WOID", WOID);
          //keyVals.Add("CRAFTID", CRAFTID);
          //keyVals.Add("WORKDATE", WORKDATE);
          //keyVals.Add("WORKSECTION", WORKSECTION);
          //keyVals.Add("LINEID", LINEID);

          TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);
          return "OK";
      }


      public string Insert_StnRec_Temp(string WOID, string CRAFTID, string WORKDATE, string PARTNUMBER, string WORKSECTION, string CLASS, string CLASSDATE, string LINEID, int PASSQTY, int FAILQTY, int RPASSQTY, int RFAILQTY, string FLAG)
      {
          IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
          IDictionary<string, object> mst = new Dictionary<string, object>();
          mst.Add("WOID", WOID);
          mst.Add("CRAFTID", CRAFTID);
          mst.Add("WORKDATE", WORKDATE);
          mst.Add("PARTNUMBER", PARTNUMBER);
          mst.Add("WORKSECTION", WORKSECTION);
          mst.Add("CLASS", CLASS);
          mst.Add("CLASSDATE", CLASSDATE);
          mst.Add("LINEID", LINEID);
          mst.Add("PASSQTY", PASSQTY);
          mst.Add("FAILQTY", FAILQTY);
          mst.Add("RPASSQTY", RPASSQTY);
          mst.Add("RFAILQTY", RFAILQTY);
          mst.Add("FLAG", FLAG);
          dp.AddData("SFCR.T_STATION_RECOUNT_TEMP", mst);
          return "OK";

      }

      public string insert_station_recount(string WOID, string CRAFTID, string WORKDATE, string PARTNUMBER, string WORKSECTION, string CLASS, string CLASSDATE, string LINEID, int PASSQTY, int FAILQTY, int RPASSQTY, int RFAILQTY, string FLAG)
      {
//          MySqlCommand cmd = new MySqlCommand();
//          cmd.CommandText = @"INSERT INTO SFCR.T_STATION_RECOUNT(WOID,CRAFTID,WORKDATE,WORKSECTION,CLASS,CLASSDATE, LINEID,PASSQTY,FAILQTY,RPASSQTY,RFAILQTY,FLAG,RECDATE)
//                VALUES(@V_WOID,@V_CRAFTID,@V_WORKDATE,@V_WORKSECTION,@V_CLASS,@V_CLASSDATE,@V_LINEID,@V_PASSQTY,@V_FAILQTY,@V_RPASSQTY,@V_RFAILQTY,@V_FLAG,NOW())";
//          cmd.Parameters.AddRange(new MySqlParameter[] {
//                new MySqlParameter("V_WOID",MySqlDbType.VarChar){Value=WOID},
//                new MySqlParameter("V_CRAFTID",MySqlDbType.VarChar){Value=CRAFTID},
//                new MySqlParameter("V_WORKDATE",MySqlDbType.VarChar){Value=WORKDATE},
//                new MySqlParameter("V_WORKSECTION",MySqlDbType.VarChar){Value=WORKSECTION},
//                new MySqlParameter("V_CLASS",MySqlDbType.VarChar){Value=CLASS},
//                new MySqlParameter("V_CLASSDATE",MySqlDbType.VarChar){Value=CLASSDATE},
//                new MySqlParameter("V_LINEID",MySqlDbType.VarChar){Value=LINEID},
//                new MySqlParameter("V_PASSQTY",MySqlDbType.Int32){Value=PASSQTY},
//                new MySqlParameter("V_FAILQTY",MySqlDbType.Int32){Value=FAILQTY},
//                new MySqlParameter("V_RPASSQTY",MySqlDbType.Int32){Value=RPASSQTY},
//                new MySqlParameter("V_RFAILQTY",MySqlDbType.Int32){Value=RFAILQTY},
//                new MySqlParameter("V_FLAG",MySqlDbType.VarChar){Value=FLAG}
//                });
//          BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

          IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
          IDictionary<string, object> mst = new Dictionary<string, object>();
          mst.Add("WOID", WOID);
          mst.Add("CRAFTID", CRAFTID);
          mst.Add("WORKDATE", WORKDATE);
          mst.Add("PARTNUMBER", PARTNUMBER);
          mst.Add("WORKSECTION", WORKSECTION);
          mst.Add("CLASS", CLASS);
          mst.Add("CLASSDATE", CLASSDATE);
          mst.Add("LINEID", LINEID);          
          mst.Add("PASSQTY", PASSQTY);
          mst.Add("FAILQTY", FAILQTY);
          mst.Add("RPASSQTY", RPASSQTY);
          mst.Add("RFAILQTY", RFAILQTY);
          mst.Add("FLAG", FLAG);          
          dp.AddData("SFCR.T_STATION_RECOUNT", mst);
          return "OK";
      }

      public System.Data.DataSet GET_STN_REC(string C_WORKDATE, string C_WORKSECTION, string WO, string MYGROUP, string LINE)
      {
          //MySqlCommand cmd = new MySqlCommand();
          //cmd.CommandText = " SELECT WOID,CRAFTID,WORKDATE,PARTNUMBER,WORKSECTION,CLASS,CLASSDATE,LINEID,PASSQTY,FAILQTY,RPASSQTY,RFAILQTY,FLAG FROM SFCR.T_STATION_RECOUNT WHERE WORKDATE = @C_WORKDATE AND WORKSECTION = @C_WORKSECTION AND WOID = @WO AND CRAFTID = @MYGROUP AND LINEID = @LINE";
          //cmd.Parameters.AddRange(new MySqlParameter[] {
          //                  new MySqlParameter("C_WORKDATE",MySqlDbType.VarChar){Value=C_WORKDATE},
          //                  new MySqlParameter("C_WORKSECTION",MySqlDbType.VarChar){Value=C_WORKSECTION},
          //                  new MySqlParameter("WO",MySqlDbType.VarChar){Value=WO},
          //                  new MySqlParameter("MYGROUP",MySqlDbType.VarChar){Value=MYGROUP},
          //                  new MySqlParameter("LINE",MySqlDbType.VarChar){Value=LINE}
          //                  });

          //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
          string table = "SFCR.T_STATION_RECOUNT";
          string fieldlist = "WOID,CRAFTID,WORKDATE,PARTNUMBER,WORKSECTION,CLASS,CLASSDATE,LINEID,PASSQTY,FAILQTY,RPASSQTY,RFAILQTY,FLAG";
          int count = 0;
          IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
          IDictionary<string, object> mst = new Dictionary<string, object>();
          mst.Add("WORKDATE", C_WORKDATE);
          mst.Add("WORKSECTION", C_WORKSECTION);
          mst.Add("WOID", WO);
          mst.Add("CRAFTID", MYGROUP);
          mst.Add("LINEID", LINE);
          return dp.GetData(table, fieldlist,  mst, out count);
      }
      public System.Data.DataSet GET_STN_REC_2(string C_WORKDATE, string C_WORKSECTION, string WO, string MYGROUP, string LINE)
      {
          string table = "SFCR.T_STATION_RECOUNT";
          string fieldlist = "ROWID";
          int count = 0;
          IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
          IDictionary<string, object> mst = new Dictionary<string, object>();
          mst.Add("WORKDATE", C_WORKDATE);
          mst.Add("WORKSECTION", C_WORKSECTION);
          mst.Add("WOID", WO);
          mst.Add("CRAFTID", MYGROUP);
          mst.Add("LINEID", LINE);
          return dp.GetData(table, fieldlist, mst, out count);
      }

      public string get_station_qty(string woid, string station, string line)
      {
          string station_qty = "0";
          string wo_qty = "0";
          string table = "SFCR.T_STATION_RECOUNT";
          string fieldlist = "CASE WHEN SUM(PASSQTY+FAILQTY) is null then 0 else SUM(PASSQTY+FAILQTY) END";
          int count = 0;
          IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
          IDictionary<string, object> mst = new Dictionary<string, object>();
          mst.Add("WOID", woid);
          mst.Add("LINEID", line);
          mst.Add("CRAFTID", station);
          DataSet ds = dp.GetData(table, fieldlist, mst, out count);
          if (ds.Tables[0].Rows.Count > 0)
          {
              station_qty = ds.Tables[0].Rows[0][0].ToString();
          }
          DataSet ds_wo = woinfo.GetWoInfo(woid, "","QTY");
          if (ds_wo.Tables[0].Rows.Count > 0)
          {
              wo_qty = ds_wo.Tables[0].Rows[0]["qty"].ToString();
          }
          return wo_qty + "/" + station_qty;
      }
  }
}
