using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SystemObject;

namespace BLL
{
    public partial class tTargetPlan
    {

        public tTargetPlan()
        {

        }

        public System.Data.DataSet GetTargetPlanAll()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select rowid,WorkDate,Class,Locstation,woId,PartNumber,Line,TargetQty,StartTime,EndTime,Side from SFCR.T_TARGET_PLAN";
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCR.T_TARGET_PLAN";
            string fieldlist = "rowid,WorkDate,Class,Locstation,woId,PartNumber,Line,TargetQty,StartTime,EndTime,Side".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);           
            return dp.GetData(table, fieldlist,null, out count);
        }

        public void InsertTargetPlan(string dicPlan)
        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = @"insert into SFCR.T_TARGET_PLAN (WorkDate,Class,Locstation,ENDSTATION,woId,PartNumber,Line,TargetQty,StartTime,EndTime,Side) 
//                                                        VALUES (@WorkDate,@Class,@Locstation,@estation,@woId,@PartNumber,@Line,@TargetQty,@StartTime,@EndTime,@Side)";
//            cmd.Parameters.Add("WorkDate", MySqlDbType.VarChar, Plan.WorkDate.Length).Value = Plan.WorkDate;
//            cmd.Parameters.Add("Class", MySqlDbType.VarChar, Plan.Class.Length).Value = Plan.Class;
//            cmd.Parameters.Add("Locstation", MySqlDbType.VarChar, Plan.Locstation.Length).Value = Plan.Locstation;
//            cmd.Parameters.Add("woId", MySqlDbType.VarChar, Plan.woId.Length).Value = Plan.woId;
//            cmd.Parameters.Add("PartNumber", MySqlDbType.VarChar, Plan.PartNumber.Length).Value = Plan.PartNumber;
//            cmd.Parameters.Add("Line", MySqlDbType.VarChar, Plan.Line.Length).Value = Plan.Line;
//            cmd.Parameters.Add("TargetQty", MySqlDbType.VarChar, Plan.TargetQty.Length).Value = Plan.TargetQty;
//            cmd.Parameters.Add("StartTime", MySqlDbType.VarChar, Plan.StartTime.Length).Value = Plan.StartTime;
//            cmd.Parameters.Add("EndTime", MySqlDbType.VarChar, Plan.EndTime.Length).Value = Plan.EndTime;
//            cmd.Parameters.Add("estation", MySqlDbType.VarChar, Plan.EndStatin.Length).Value = Plan.EndStatin;
//            cmd.Parameters.Add("Side", MySqlDbType.VarChar, Plan.Side.Length).Value = Plan.Side;
//            BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicPlan);
            dp.AddData("SFCR.T_TARGET_PLAN", mst);
        }
        public void UpdateTargetPlan(string dicstring)
        {

            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "update SFCR.T_TARGET_PLAN set TargetQty=@sQTY, StartTime=@STime, EndTime=@ETime, Side=@SDE where ROWID=@idx";
            //cmd.Parameters.Add("sQTY", MySqlDbType.VarChar).Value = sPlan.TargetQty;
            //cmd.Parameters.Add("STime", MySqlDbType.VarChar).Value = sPlan.StartTime;
            //cmd.Parameters.Add("ETime", MySqlDbType.VarChar).Value = sPlan.EndTime;
            //cmd.Parameters.Add("SDE", MySqlDbType.VarChar).Value = sPlan.Side;
            //cmd.Parameters.Add("idx", MySqlDbType.VarChar).Value = sPlan.Idx;
            //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            dp.UpdateData("SFCR.T_TARGET_PLAN", new string[] { "ROWID" }, mst);

        }

        public void DeleteTargetPlan(string Idx)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "delete from SFCR.T_TARGET_PLAN where rowid=@idx  ";
            //cmd.Parameters.Add("idx", MySqlDbType.VarChar, Idx.Length).Value = Idx;
            //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ROWID", Idx);
            dp.DeleteData("SFCR.T_TARGET_PLAN", mst);
        }

        //public System.Data.DataSet GetTargetPlan(string Line, string Flag, string MyGroup, out string Err)
        //{
        //    //return BLL.BllMsSqllib.Instance.GetTargetPlan(Line,Flag,MyGroup,out Err);

        //    List<Entity.ProcedureKey> LsPdk = new List<Entity.ProcedureKey>();
        //    Entity.ProcedureKey Pdk = null;
        //    Pdk = new Entity.ProcedureKey();
        //    Pdk.Variable = "LINE";
        //    Pdk.Value = Line;
        //    LsPdk.Add(Pdk);

        //    Pdk = new Entity.ProcedureKey();
        //    Pdk.Variable = "FLAG";
        //    Pdk.Value = Flag;
        //    LsPdk.Add(Pdk);


        //    Pdk = new Entity.ProcedureKey();
        //    Pdk.Variable = "MYGROUP";
        //    Pdk.Value = MyGroup;
        //    LsPdk.Add(Pdk);

        //    return BLL.BllMsSqllib.Instance.PublicReurnDataSetOutString("PRO_GETTARGETPLAN", LsPdk, "RESGRID", out Err);
        //}
       
        public System.Data.DataSet GetTargetPlan1(String liness, String getDate, String ctype)
        {
      //      MySqlCommand cmd = new MySqlCommand();
      //      if (!String.IsNullOrEmpty(liness))
      //      {
      //          cmd.CommandText = "SELECT a.Line line,a.Locstation Throughout,a.woId,a.PartNumber Part_Number,b.productname Product_Name,a.TargetQty Target_Qty,'0' Plan_Out,'0' Actual,'0' Hit_Rate,'100%' Yield_Rate,a.StartTime,a.EndTime " +
      //"FROM SFCR.T_TARGET_PLAN a,B_PRODUCT b where WorkDate = @getDate and a.PartNumber = b.partnumber and a.Line=@liness and a.class=@ctype";
      //          cmd.Parameters.Add("liness", MySqlDbType.VarChar).Value = liness;
      //      }
      //      else
      //      {
      //          cmd.CommandText = "SELECT a.Line line,a.Locstation Throughout,a.woId,a.PartNumber Part_Number,b.productname Product_Name,a.TargetQty Target_Qty,'0' Plan_Out,'0' Actual,'0' Hit_Rate,'100%' Yield_Rate,a.StartTime,a.EndTime " +
      //"FROM SFCR.T_TARGET_PLAN a,B_PRODUCT b where WorkDate = @getDate and a.PartNumber = b.partnumber and a.class = @ctype";
      //      }
      //      cmd.Parameters.Add("getDate", MySqlDbType.VarChar).Value = getDate;
      //      cmd.Parameters.Add("ctype", MySqlDbType.VarChar).Value = ctype;
      //      return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            int count = 0;
            string table = "SFCR.T_TARGET_PLAN a,B_PRODUCT b";
            string fieldlist = "a.Line line,a.Locstation Throughout,a.woId,a.PartNumber Part_Number,b.productname Product_Name,a.TargetQty Target_Qty,'0' Plan_Out,'0' Actual,'0' Hit_Rate,'100%' Yield_Rate,a.StartTime,a.EndTime";
            string filter = "WorkDate = {0} and a.PartNumber = b.partnumber and a.class = {1} ";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WorkDate", getDate);
            mst.Add("class", ctype);

            if (!String.IsNullOrEmpty(liness))
            {
                filter += " and a.Line={2} ";
                mst.Add("Line", liness);
            }
          
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);


        }

        public System.Data.DataSet GetTargetPlan2(String liness, String getDate, String ctype)
        {
      //      MySqlCommand cmd = new MySqlCommand();
      //      if (!String.IsNullOrEmpty(liness))
      //      {
      //          cmd.CommandText = "SELECT woId,craftid,class,lineId,sum(passQty) passQty,sum(failQty) failQty FROM SFCR.T_STATION_RECOUNT" +
      //" where classDate = @getDate and lineId = @liness and class=@ctype group by woId,craftid,class,lineId";
      //          cmd.Parameters.Add("liness", MySqlDbType.VarChar).Value = liness;
      //      }
      //      else
      //      {
      //          cmd.CommandText = "SELECT woId,craftid,class,lineId,sum(passQty) passQty,sum(failQty) failQty FROM SFCR.T_STATION_RECOUNT" +
      //" where classDate = @getDate and class=@ctype group by woId,craftid,class,lineId";

      //      }
      //      cmd.Parameters.Add("getDate", MySqlDbType.VarChar).Value = getDate;
      //      cmd.Parameters.Add("ctype", MySqlDbType.VarChar).Value = ctype;
      //      return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            int count = 0;
            string table = "SFCR.T_STATION_RECOUNT";
            string fieldlist = "woId,craftid,class,lineId,sum(passQty) passQty,sum(failQty) failQty";
            string filter = "classDate ={0} and class={1} ";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("classDate", getDate);
            mst.Add("class", ctype);

            if (!String.IsNullOrEmpty(liness))
            {
                filter += " and lineId ={2} ";
                mst.Add("lineId", liness);
            }

            return TransactionManager.GetData(table, fieldlist, filter, mst, null, "woId,craftid,class,lineId", out count);
        }

        public System.Data.DataSet GetTargetPlan3(String liness, String getDate, String ctype)
        {
      //      MySqlCommand cmd = new MySqlCommand();
      //      if (!String.IsNullOrEmpty(liness))
      //      {
      //          cmd.CommandText = "SELECT woId,class,lineId,craftid,worksection,sum(passQty) passQty,sum(failQty) failQty FROM SFCR.T_STATION_RECOUNT" +
      //" where classDate = @getDate  and lineId = @liness and class=@ctype group by woId,class,lineId,craftid,worksection";
      //          cmd.Parameters.Add("liness", MySqlDbType.VarChar).Value = liness;
      //      }
      //      else
      //      {
      //          cmd.CommandText = "SELECT woId,class,lineId,craftid,worksection,sum(passQty) passQty,sum(failQty) failQty FROM SFCR.T_STATION_RECOUNT" +
      //" where classDate = @getDate and class=@ctype group by woId,class,lineId,craftid,worksection";

      //      }
      //      cmd.Parameters.Add("getDate", MySqlDbType.VarChar).Value = getDate;
      //      cmd.Parameters.Add("ctype", MySqlDbType.VarChar).Value = ctype;
      //      return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);


            int count = 0;
            string table = "SFCR.T_STATION_RECOUNT";
            string fieldlist = "oId,class,lineId,craftid,worksection,sum(passQty) passQty,sum(failQty) failQty";
            string filter = "classDate ={0} and class={1} ";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("classDate", getDate);
            mst.Add("class", ctype);

            if (!String.IsNullOrEmpty(liness))
            {
                filter += " and lineId ={2} ";
                mst.Add("lineId", liness);
            }

            return TransactionManager.GetData(table, fieldlist, filter, mst, null, "woId,class,lineId,craftid,worksection", out count);
        }


    }
}
