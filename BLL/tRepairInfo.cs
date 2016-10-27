using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SystemObject;
using System.Data.Common;
using SrvComponent;

namespace BLL
{
    public partial class tRepairInfo
    {        
        public tRepairInfo()
        {
           
        }
        public System.Data.DataSet GetRepairSnInfo(string ESN)
        {
           // MySqlCommand cmd = new MySqlCommand();
           // cmd.CommandText = "select a.ErrorCode,a.ReasonCode,a.esn,a.woId,a.partnumber,a.craftid,a.inputuser,a.status,a.recdate,a.outdate,a.inputdate," +
           //                  "a.reuser,a.lineId,a.location,a.remark,a.outcraftId from SFCR.T_REPAIR_INFO a where a.esn=@esn ";
           //// cmd.Parameters.Add("@esn", MySqlDbType.VarChar, 50).Value = ESN;
           // cmd.Parameters.AddRange(new MySqlParameter[]
           // {
           //     new MySqlParameter("esn",MySqlDbType.VarChar){Value=ESN}
           // });
           // return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCR.T_REPAIR_INFO";
            string fieldlist = "rowid as ID,ErrorCode,ReasonCode,esn,woId,partnumber,craftid,inputuser,status,recdate,outdate,inputdate,reuser,lineId,location,remark,outcraftId".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ESN",ESN);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        public System.Data.DataSet GetRepairMaterialInfo(string ESN)
        {
           // MySqlCommand cmd = new MySqlCommand();
           // cmd.CommandText = "select ESN,REPAIRTIME,KPNO,VENDERCODE,DATECODE,LOTCODE from SFCR.T_REPAIR_MATERIAL_INFO where esn=@esn";
           //// cmd.Parameters.Add("@esn", MySqlDbType.VarChar, 50).Value = ESN;
           // cmd.Parameters.AddRange(new MySqlParameter[]
           // {
           //     new MySqlParameter("esn",MySqlDbType.VarChar){Value=ESN}
           // });
           // return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            string table = "SFCR.T_REPAIR_MATERIAL_INFO";
            string fieldlist = "ESN,REPAIRTIME,KPNO,VENDERCODE,DATECODE,LOTCODE";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ESN", ESN);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        public void UpdateRepairSnStatus(string sStatus, string ROWID)
        {          
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (sStatus == "1")
            {
                mst.Add("INPUTDATE",System.DateTime.Now);               
            }
            if (sStatus == "3")
            {
                mst.Add("OUTDATE", System.DateTime.Now);            
            }
            mst.Add("ROWID", ROWID);
            mst.Add("STATUS", sStatus);
            dp.UpdateData("SFCR.T_REPAIR_INFO", new string[] { "ROWID" }, mst);
        }
        public DataSet GetFailErrListCount(string ESN)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select ErrorCode as 不良代码,count(esn) 不良点数 from SFCR.T_REPAIR_INFO where esn=@SN group by ErrorCode";
            //cmd.Parameters.AddRange(new MySqlParameter[]
            //{
            //    new MySqlParameter("SN",MySqlDbType.VarChar){Value=ESN}
            //});
            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);            

            int count = 0;
            string table = "SFCR.T_REPAIR_INFO";
            string fieldlist = "ErrorCode ,count(esn) as  QTY";
            string filter = "ESN={0}";           
            IList<OrderKey> order = new List<OrderKey>();
            OrderKey od = new OrderKey();
            od.KeyName = "ErrorCode";
            od.IsAsc = true;      
            order.Add(od);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ESN", ESN);
            return TransactionManager.GetData(table, fieldlist, filter, mst, order, null, out count);
        }
        public DataSet GetWipAndRouteData(string ESN)
        {
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = @"select ab.craftname,ab.nextcraftname from SFCB.B_ROUTE_INFO ab, 
//                                (select b.routgroupId,b.nextcraftname from sfcr.T_WIP_TRACKING_ONLINE a ,SFCB.B_ROUTE_INFO b  where a.routgroupId = b.routgroupId 
//                                and a.locstation=b.craftname and a.esn=@SN  and a.errflag=b.station_flag) ac 
//                                where ab.routgroupId = ac.routgroupId and ab.craftname=ac.nextcraftname and ab.station_flag='2' ";
//           // cmd.Parameters.Add("@esn", MySqlDbType.VarChar, 50).Value = ESN;
//            cmd.Parameters.AddRange(new MySqlParameter[]
//            {
//                new MySqlParameter("SN",MySqlDbType.VarChar){Value=ESN}
//            });
//            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            DataSet ds = new DataSet();

            string table = "SFCR.T_WIP_TRACKING_ONLINE";
            string fieldlist = "LOCSTATION,ERRFLAG,ROUTGROUPID";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ESN", ESN);
           DataTable dt= dp.GetData(table, fieldlist, mst, out count).Tables[0];
           if (dt.Rows.Count > 0)
           {
               fieldlist = "STATION_FLAG,CRAFTNAME,NEXTCRAFTNAME";
               mst = new Dictionary<string, object>();
               mst.Add("ROUTGROUPID", dt.Rows[0][2].ToString());
               DataTable dtRoute = dp.GetData("SFCB.B_ROUTE_INFO", fieldlist, mst, out count).Tables[0];
               if (dtRoute.Rows.Count==0)
                   throw new Exception("ROUTE NOT FOUND");
               DataTable dttemp = getNewTable(dtRoute, string.Format("CRAFTNAME='{0}' AND STATION_FLAG='{1}'", dt.Rows[0]["LOCSTATION"].ToString(), dt.Rows[0]["ERRFLAG"].ToString()));
               if (dttemp.Rows.Count==0)
                   throw new Exception("REPAIR ROUTE NOT FOUND");
               DataTable  dtres=  getNewTable(dtRoute, string.Format("CRAFTNAME='{0}' AND STATION_FLAG='{1}'", dttemp.Rows[0]["NEXTCRAFTNAME"].ToString(), "2"));
               if (dtres.Rows.Count==0)
                   throw new Exception("REFLOW ROUTE NOT FOUND");
               DataTable dt1 = new DataTable();
               dt1.Columns.Add("CRAFTNAME");
               dt1.Columns.Add("NEXTCRAFTNAME");
               dt1.Rows.Add(dtres.Rows[0]["CRAFTNAME"].ToString(), dtres.Rows[0]["NEXTCRAFTNAME"].ToString());
               ds.Tables.Add(dt1);
           }
           else
           {
               throw new Exception("WIP NOT DATA");
           }
           return ds;

        }


        /// <summary>
        /// 返回一个新的datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataTable getNewTable(DataTable dt, string sql)
        {
            try
            {
                DataTable mydt = new DataTable();
                mydt = dt.Clone();
                DataRow[] arrDr = dt.Select(sql);
                for (int i = 0; i < arrDr.Length; i++)
                {
                    mydt.ImportRow((DataRow)arrDr[i]);
                }
                return mydt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
//        public void UpdateRepairInformation(string  dicRepair)
//        {
//            string workdate = "NA";
//            string worksection = "NA";
//            string classdate = "NA";
//            string sclass = "NA";

////            MySqlCommand scmd = new MySqlCommand();
////            scmd.CommandText = @"select DATE_FORMAT(NOW(),'%Y%m%d') workdate,WorkSection,Day,Class from SFCB.B_WORK_CLASS  where 
////                               	StartTime<=DATE_FORMAT(NOW(),'%H%i') 
////                               	and EndTime> DATE_FORMAT(NOW(),'%H%i'))";
////            DataTable dt = BLL.BllMsSqllib.Instance.ExecuteDataSet(scmd).Tables[0];

//            int count = 0;
//            string table = "SFCB.B_WORK_CLASS";
//            string fieldlist = "DATE_FORMAT(NOW(),'%Y%m%d') workdate,WorkSection,Day,Class";
//            string filter = "StartTime<=DATE_FORMAT(NOW(),'%H%i') and EndTime> DATE_FORMAT(NOW(),'%H%i')";           
//            DataTable dt= TransactionManager.GetData(table, fieldlist, filter, null, null, null, out count).Tables[0];
//            try
//            {
//                workdate = dt.Rows[0][0].ToString();
//                worksection = dt.Rows[0][1].ToString();
//                if (dt.Rows[0][2].ToString() == "TOMORROW")
//                    classdate = (Convert.ToInt32(workdate) - 1).ToString();
//                else
//                    classdate = workdate;
//                sclass = dt.Rows[0][3].ToString();
//            }
//            catch
//            {
//            }

////            MySqlCommand cmd = new MySqlCommand();
////            cmd.CommandText = @"update SFCR.T_REPAIR_INFO set ReasonCode=@RC,recdate=SYSDATE(),status='2',reuser=@sUsed,location=@loc,
////             remark=@sremark,outcraftId=@sroute,RClassDate=@sDate,RCLASS=@sclass,RWorkSection=@sTion,duty=@sduty where esn=@sID";
           
////            cmd.Parameters.AddRange(new MySqlParameter[]
////            {
////                new MySqlParameter("RC",MySqlDbType.VarChar){Value=Repair.ReasonCode},
////                new MySqlParameter("sUsed",MySqlDbType.VarChar){Value=Repair.ReUser},
////                new MySqlParameter("loc",MySqlDbType.VarChar){Value=Repair.Location},
////                new MySqlParameter("sremark",MySqlDbType.VarChar){Value=Repair.Remark},
////                new MySqlParameter("sroute",MySqlDbType.VarChar){Value=Repair.OutcraftId},
////                new MySqlParameter("sID",MySqlDbType.VarChar){Value=Repair.ESN},
////                new MySqlParameter("sDate",MySqlDbType.VarChar){Value=classdate},
////                new MySqlParameter("sclass",MySqlDbType.VarChar){Value=sclass},
////                new MySqlParameter("sTion",MySqlDbType.VarChar){Value=worksection},
////                new MySqlParameter("sduty",MySqlDbType.VarChar){Value=Repair.Duty}
////            });

//       //     BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

//            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
//            IDictionary<string, object> mst1 = MapListConverter.JsonToDictionary(dicRepair);
//            IDictionary<string, object> mst = new Dictionary<string, object>();
//            mst.Add("ReasonCode", mst1["REASONCODE"]);
//            mst.Add("reuser", mst1["REUSER"]);
//            mst.Add("location", mst1["LOCATION"]);
//            mst.Add("remark", mst1["REMARK"]);
//            mst.Add("outcraftId", mst1["OUTCRAFTID"]);
//            mst.Add("RWorkSection", worksection);
//            mst.Add("duty", mst1["DUTY"]);
//            mst.Add("RClassDate", classdate);
//            mst.Add("RCLASS", sclass);
//            mst.Add("RECDATE", System.DateTime.Now);
//            mst.Add("STATUS", "2");
//            mst.Add("ESN", mst1["ESN"]);
//            dp.UpdateData("SFCR.T_REPAIR_INFO", new string[] { "ESN" }, mst);
//        }
        //public void InsertRepairMaterialInfo(string dicRepair)
        //{     
          
        //    //MySqlCommand cmd = new MySqlCommand();
        //    //cmd.CommandText = "insert into SFCR.T_REPAIR_MATERIAL_INFO (ESN,KPNO,VENDERCODE,DATECODE,LOTCODE) VALUES (@SN,@kpno,@vc,@dc,@lc)";
    
        //    //cmd.Parameters.AddRange(new MySqlParameter[]
        //    //{
        //    //   // new MySqlParameter("id",MySqlDbType.VarChar){Value=Repair.id},
        //    //    new MySqlParameter("SN",MySqlDbType.VarChar){Value=Repair.ESN},
        //    //    new MySqlParameter("kpno",MySqlDbType.VarChar){Value=Repair.PartNo},           
        //    //    new MySqlParameter("vc",MySqlDbType.VarChar){Value=Repair.VenderCode},
        //    //    new MySqlParameter("dc",MySqlDbType.VarChar){Value=Repair.DateCode},
        //    //    new MySqlParameter("lc",MySqlDbType.VarChar){Value=Repair.LotCode}
        //    //});

        //    // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

        //    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
        //    IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicRepair);
        //    IDictionary<string, object> dic = new Dictionary<string, object>();
        //    dic.Add("ESN", mst["ESN"]);
        //    dic.Add("repairtime", System.DateTime.Now);
        //    dic.Add("KPNO", mst["KPNO"]);
        //    dic.Add("VENDERCODE", mst["VENDERCODE"]);
        //    dic.Add("DATECODE", mst["DATECODE"]);
        //    dic.Add("LOTCODE", mst["LOTCODE"]);
        //    dp.AddData("SFCR.T_REPAIR_MATERIAL_INFO", dic);
        //}


        public string UpdateRepairToWip(string CraftId, string ESN, string sStatus, string rowid)
        {
            //try
            //{

            //    MySqlCommand[] cmd = new MySqlCommand[5];
            //    List<MySqlCommand> lsCmd = new List<MySqlCommand>();
            //    string sSQL = "update SFCR.T_REPAIR_INFO set status=@stu ";
            //    if (sStatus == "1")
            //    {
            //        sSQL = sSQL + ",inputdate=SYSDATE() ";
            //    }
            //    if (sStatus == "3")
            //    {
            //        sSQL = sSQL + ",outdate=SYSDATE() ";
            //    }
            //    sSQL = sSQL + " where ESN=@sID";
            //    cmd[0] = new MySqlCommand();
            //    cmd[0].CommandText = sSQL;
            //    cmd[0].Parameters.AddRange(new MySqlParameter[]
            //    {
            //      new MySqlParameter("sID",MySqlDbType.VarChar){Value=ESN},
            //      new MySqlParameter("stu",MySqlDbType.VarChar){Value=sStatus}
            //    });
            //    lsCmd.Add(cmd[0]);


            //    cmd[1] = new MySqlCommand();
            //    cmd[1].CommandText = "update sfcr.T_WIP_TRACKING_ONLINE set nextstation=@Craft,errflag='0',locstation=wipstation,wipstation=@Craft,line='REPAIR' where esn=@SN";
            //    cmd[1].Parameters.AddRange(new MySqlParameter[]
            //{
            //    new MySqlParameter("Craft",MySqlDbType.VarChar){Value=CraftId},
            //    new MySqlParameter("SN",MySqlDbType.VarChar){Value=ESN}
            //});


            //    lsCmd.Add(cmd[1]);
            //    BLL.BllMsSqllib.Instance.ExecteNonQueryTransaction(lsCmd);
            //    return "OK";
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}

            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                if (sStatus == "1")
                {                  
                    mst.Add("INPUTDATE", System.DateTime.Now);
                }
                if (sStatus == "3")
                {                 
                    mst.Add("OUTDATE", System.DateTime.Now);
                }
                mst.Add("STATUS", sStatus);
                mst.Add("ROWID", rowid);
                dp.UpdateData(tx, "SFCR.T_REPAIR_INFO", new string[] { "ROWID" }, mst);

                //mst = new Dictionary<string, object>();
                //mst.Add("NEXTSTATION", CraftId);
                //mst.Add("ERRFLAG", "0");
                //mst.Add("WIPSTATION", CraftId);
                //mst.Add("LINE", "REPAIR");
                //mst.Add("ESN", ESN);
                //dp.UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);

                StringBuilder ofilter = new StringBuilder();
                ofilter.Append("NEXTSTATION = {0},ERRFLAG={1},WIPSTATION={2},LOCSTATION=WIPSTATION,LINE={3} ");
                IDictionary<string, object> modFields = new Dictionary<string, object>();
                modFields.Add("NEXTSTATION", CraftId);
                modFields.Add("ERRFLAG", "0");
                modFields.Add("WIPSTATION", CraftId);
                modFields.Add("LINE", "REPAIR");

                string filter = "ESN = {0}";
                IDictionary<string, object> keyVals = new Dictionary<string, object>();
                keyVals.Add("ESN", ESN);

                TransactionManager.UpdateBatchData("SFCR.T_WIP_TRACKING_ONLINE", ofilter.ToString(), modFields, filter, keyVals);
                tx.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                tx.Rollback();
                return ex.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

        }

        public  void UpdateRepairScrapWip(string ESN)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "update sfcr.T_WIP_TRACKING_ONLINE set nextstation='NA',scrapflag='1',wipstation=@Craft,line='REPAIR' where esn=@SN";
            ////cmd.Parameters.Add("@Craft", MySqlDbType.VarChar, 50).Value = "REPAIR_SCRAP"; //报废仓位待定
            ////cmd.Parameters.Add("@SN", MySqlDbType.VarChar, 50).Value = ESN;
            //cmd.Parameters.AddRange(new MySqlParameter[]
            //{
            //    new MySqlParameter("Craft",MySqlDbType.VarChar){Value="REPAIR_SCRAP"},
            //    new MySqlParameter("SN",MySqlDbType.VarChar){Value=ESN}
            //});
            //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("NEXTSTATION", "NA");
            mst.Add("SCRAPFLAG", "1");
            mst.Add("WIPSTATION", "REPAIR_SCRAP");
            mst.Add("LINE", "REPAIR");
            mst.Add("ESN", ESN);
            dp.UpdateData("SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);

        }
        public System.Data.DataSet GetRepairInfoIN(string dicstring ) //20130129
        {
            /* MySqlCommand cmd = new MySqlCommand();
             //cmd.CommandText = "select ErrorCode,ReasonCode,esn,woId,partnumber,craftId,lineId,location,TClassDate,TCLASS,TWorkSection,RClassDate,RCLASS,RWorkSection from " +
             //     "SFCR.T_REPAIR_INFO where TClassDate||TWorkSection>=@Sdate and  TClassDate||TWorkSection<=@Edate";

             //cmd.Parameters.AddRange(new MySqlParameter[]
             //{
             //    new MySqlParameter("Sdate",MySqlDbType.VarChar){Value=StartTime},
             //    new MySqlParameter("Edate",MySqlDbType.VarChar){Value=EndTime}
             //});
             string Param = string.Empty;
             string Colnums = string.Empty;           

             //if (rit.Show_PartNumber)
             //    Colnums += "PARTNUMBER,";
             //if (rit.Show_ProductName)
             //    Colnums += "PRODUCTNAME,";
             //if (rit.Show_woId)
             //    Colnums += "WOID,";
             //if (rit.Show_Line)
             //    Colnums += "LINEID,";
             //if (rit.Show_CraftId)
             //    Colnums += "CRAFTID,";
             //if (rit.Show_ClassDate)
             //    Colnums += "TClassDate,";
             //Colnums += "ERRORCODE,ERRORDESC".ToUpper();

             string sSQL = string.Format("SELECT {0},COUNT(ESN) as QTY FROM SFCB.VIEW_REPAIR_INFO where ", Colnums);


            // if (!string.IsNullOrEmpty(rit.woId) && (rit.woId != "ALL"))
             if (!string.IsNullOrEmpty(mst["WOID"].ToString()) && (mst["WOID"].ToString() != "ALL"))
             {

                 Param = string.Empty;
               //  for (int x = 0; x < rit.woId.Split(',').Length; x++)
                 for (int x = 0; x < mst["WOID"].ToString().Split(',').Length; x++)
                 {
                     Param += ("@C_WOID" + x.ToString() + ",");
                   //  cmd.Parameters.Add("C_WOID" + x.ToString(), MySqlDbType.VarChar).Value = rit.woId.Split(',')[x];
                 }
                 sSQL += string.Format(" woId in ({0}) and ", Param.Substring(0, Param.Length - 1));   


                 //sSQL += " woId in (@C_WOID) and ";
                 //cmd.Parameters.Add("C_WOID", MySqlDbType.VarChar).Value = rit.woId;
             }
             if (!string.IsNullOrEmpty(rit.PartNumber) && (rit.PartNumber != "ALL"))
             {
                 Param = string.Empty;
                 for (int x = 0; x < rit.PartNumber.Split(',').Length; x++)
                 {
                     Param += ("@C_MODEL" + x.ToString() + ",");
                     cmd.Parameters.Add("C_MODEL" + x.ToString(), MySqlDbType.VarChar).Value = rit.PartNumber.Split(',')[x];
                 }
                 sSQL += string.Format(" partnumber in ({0}) and ", Param.Substring(0, Param.Length - 1));

                 //sSQL += " partnumber in (@C_MODEL) and ";
                 //cmd.Parameters.Add("C_MODEL", MySqlDbType.VarChar).Value = rit.PartNumber;
             }
             if (!string.IsNullOrEmpty(rit.CraFtId) && (rit.CraFtId != "ALL"))
             {
                 Param = string.Empty;
                 for (int x = 0; x < rit.CraFtId.Split(',').Length; x++)
                 {
                     Param += ("@C_ROUTE" + x.ToString() + ",");
                     cmd.Parameters.Add("C_ROUTE" + x.ToString(), MySqlDbType.VarChar).Value = rit.CraFtId.Split(',')[x];
                 }
                 sSQL += string.Format(" CRAFTID IN ({0}) and ", Param.Substring(0, Param.Length - 1));

                 //sSQL += " CRAFTID IN (@C_ROUTE) and ";
                 //cmd.Parameters.Add("C_ROUTE", MySqlDbType.VarChar).Value = rit.CraFtId;
             }
             if (!string.IsNullOrEmpty(rit.Line) && (rit.Line != "ALL"))
             {
                 Param = string.Empty;
                 for (int x = 0; x < rit.Line.Split(',').Length; x++)
                 {
                     Param += ("@C_LINE" + x.ToString() + ",");
                     cmd.Parameters.Add("C_LINE" + x.ToString(), MySqlDbType.VarChar).Value = rit.Line.Split(',')[x];
                 }
                 sSQL += string.Format(" LINEID IN ({0}) and ", Param.Substring(0, Param.Length - 1));

                 //sSQL += " LINEID IN (@C_LINE) and ";
                 //cmd.Parameters.Add("C_LINE", MySqlDbType.VarChar).Value = rit.Line;
             }


             sSQL+=string.Format(" TClassDate||TWorkSection>=@Sdate and  TClassDate||TWorkSection<=@Edate GROUP BY {0} ", Colnums);
           
             cmd.Parameters.Add("Sdate", MySqlDbType.VarChar).Value = rit.StartTime;
             cmd.Parameters.Add("Edate", MySqlDbType.VarChar).Value = rit.EndTime;
             cmd.CommandText = sSQL;

             return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd); */

            string Param = string.Empty;
            string Colnums = string.Empty;    
           int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            StringBuilder FieldList = new StringBuilder();

            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);

            if (Convert.ToBoolean(mst["SHOW_PARTNUMBER"]))
                FieldList.Append("PARTNUMBER,");
            if (Convert.ToBoolean(mst["SHOW_PRODUCTNAME"]))
                FieldList.Append("PRODUCTNAME,");
            if (Convert.ToBoolean(mst["SHOW_WOID"]))
                FieldList.Append("WOID,");
            if (Convert.ToBoolean(mst["SHOW_LINEID"]))
                FieldList.Append("LINEID,");
            if (Convert.ToBoolean(mst["SHOW_CRAFTID"]))
                FieldList.Append("CRAFTID,");
            if (Convert.ToBoolean(mst["SHOW_TCLASSDATE"]))
                FieldList.Append("TCLASSDATE,");
            FieldList.Append("ERRORCODE,ERRORDESC");
            int y = 0;
            StringBuilder filter = new StringBuilder();
            IDictionary<string, object> dicdata = new Dictionary<string, object>();
             if (!string.IsNullOrEmpty(mst["WOID"].ToString()) && (mst["WOID"].ToString() != "ALL"))
             {
                 filter.Append("WOID IN ");
                 for (int x = 0; x < mst["WOID"].ToString().Split(',').Length; x++)
                 {
                     if (x == mst["WOID"].ToString().Split(',').Length-1)
                         filter.Append("{" + y++.ToString() + "})");
                     else
                     filter.Append("{" +  y++.ToString() + "}");
                     dicdata.Add("WOID" + x.ToString(), mst["WOID"].ToString().Split(',')[x]);
                 }                
             }

             if (!string.IsNullOrEmpty(mst["PARTNUMBER"].ToString()) && (mst["PARTNUMBER"].ToString() != "ALL"))
             {
                 if (!string.IsNullOrEmpty(filter.ToString()))
                     filter.Append("AND ");
                 filter.Append("PARTNUMBER IN (");
                 for (int x = 0; x < mst["PARTNUMBER"].ToString().Split(',').Length; x++)
                 {
                     if (x == mst["PARTNUMBER"].ToString().Split(',').Length-1)
                         filter.Append("{" + y++.ToString() + "})");
                     else
                     filter.Append("{" + y++.ToString() + "},");
                     dicdata.Add("PARTNUMBER" + x.ToString(), mst["PARTNUMBER"].ToString().Split(',')[x]);
                 }
                 
             }
             if (!string.IsNullOrEmpty(mst["CRAFTID"].ToString()) && (mst["CRAFTID"].ToString() != "ALL"))
             {
                 if (!string.IsNullOrEmpty(filter.ToString()))
                     filter.Append("AND ");
                 filter.Append("CRAFTID IN ");

                 for (int x = 0; x < mst["CRAFTID"].ToString().Split(',').Length; x++)
                 {
                     if (x == mst["CRAFTID"].ToString().Split(',').Length-1)
                         filter.Append("{" + y++.ToString() + "})");
                     else
                     filter.Append("{" + y++.ToString() + "}");
                     dicdata.Add("CRAFTID" + x.ToString(), mst["CRAFTID"].ToString().Split(',')[x]);
                 }                      
             }
             if (!string.IsNullOrEmpty(mst["LINEID"].ToString()) && (mst["LINEID"].ToString() != "ALL"))
             {
                 if (!string.IsNullOrEmpty(filter.ToString()))
                     filter.Append("AND ");
                 filter.Append("LINEID IN ");
                 for (int x = 0; x < mst["LINEID"].ToString().Split(',').Length; x++)
                 {
                     if (x == mst["LINEID"].ToString().Split(',').Length-1)
                         filter.Append("{" + y++.ToString() + "})");
                     else
                     filter.Append("{" + y++.ToString() + "}");
                     dicdata.Add("LINEID" + x.ToString(), mst["LINEID"].ToString().Split(',')[x]);
                 }

             }
           
             if (!string.IsNullOrEmpty(filter.ToString()))
             filter.Append(" AND  TClassDate||TWorkSection>=");
             else
             filter.Append(" TClassDate||TWorkSection>=");

             filter.Append("{" + y++.ToString() + "}");
             dicdata.Add("STARTTIME", mst["STARTTIME"]);

             filter.Append(" AND TClassDate||TWorkSection<=");
             filter.Append("{" + y++.ToString() + "}");
             dicdata.Add("ENDTIME", mst["ENDTIME"]);
            
             return dp.GetData("SFCB.VIEW_REPAIR_INFO", FieldList.ToString(), filter.ToString(), dicdata, null, FieldList.ToString(), out count);
        }

        public System.Data.DataSet GetRepairInfoOUT(string dicstring) //20130129
        {
          //  Entity.tRepairInfoTable rit = new Entity.tRepairInfoTable();
          //  MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select ErrorCode,ReasonCode,esn,woId,partnumber,craftId,lineId,location,TClassDate,TCLASS,TWorkSection,RClassDate,RCLASS,RWorkSection from " +
            //     "SFCR.T_REPAIR_INFO where RClassDate||RWorkSection>=@Sdate and  RClassDate||RWorkSection<=@Edate";

            //cmd.Parameters.AddRange(new MySqlParameter[]
            //{
            //    new MySqlParameter("Sdate",MySqlDbType.VarChar){Value=StartTime},
            //    new MySqlParameter("Edate",MySqlDbType.VarChar){Value=EndTime}
            //});

            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicstring);

            string Param = string.Empty;
            //string Colnums = string.Empty;
            string table = "SFCB.VIEW_REPAIR_INFO";
            string fieldlist = string.Empty ;
            StringBuilder filter = new StringBuilder();
            Dictionary<string, object> dicdata = new Dictionary<string, object>();

            //if (rit.Show_PartNumber)
            //    Colnums += "PARTNUMBER,";
            //if (rit.Show_ProductName)
            //    Colnums += "PRODUCTNAME,";
            //if (rit.Show_woId) 
            //    Colnums += "WOID,";
            //if (rit.Show_Line)
            //    Colnums += "LINEID,";
            //if (rit.Show_CraftId)
            //    Colnums += "CRAFTID,";
            //if (rit.Show_ClassDate)
            //    Colnums += "RClassDate,";
            //if (rit.Show_Location)
            //    Colnums += "LOCTION,";

            if (Convert.ToBoolean(dic["SHOW_PARTNUMBER"]))
                 fieldlist+= "PARTNUMBER,";
            if (Convert.ToBoolean(dic["SHOW_PRODUCTNAME"]))
                fieldlist += "PRODUCTNAME,";
            if (Convert.ToBoolean(dic["SHOW_WOID"]))
                fieldlist += "WOID,";
            if (Convert.ToBoolean(dic["SHOW_LINEID"]))
                fieldlist += "LINEID,";
            if (Convert.ToBoolean(dic["SHOW_CRAFTID"]))
                fieldlist += "CRAFTID,";
            if (Convert.ToBoolean(dic["SHOW_RCLASSDATE"]))
                fieldlist += "RCLASSDATE,";
            if (Convert.ToBoolean(dic["SHOW_LOCATION"]))
                fieldlist += "LOCATION,";

           // Colnums += "ERRORCODE,ERRORDESC".ToUpper();
            fieldlist += "ERRORCODE,ERRORDESC,COUNT(ESN) AS QTY";

          //  string sSQL = string.Format("SELECT {0},COUNT(ESN) as QTY FROM SFCB.VIEW_REPAIR_INFO WHERE ", Colnums);


            //if (!string.IsNullOrEmpty(rit.woId) && (rit.woId != "ALL"))
            //{
            //    Param = string.Empty;
            //    for (int x = 0; x < rit.woId.Split(',').Length; x++)
            //    {
            //        Param += ("@C_WOID" + x.ToString() + ",");
            //        cmd.Parameters.Add("C_WOID" + x.ToString(), MySqlDbType.VarChar).Value = rit.woId.Split(',')[x];
            //    }
            //    sSQL += string.Format(" woId in ({0}) and ", Param.Substring(0, Param.Length - 1));                 
            //}
            int y = 0;
            if (dic.ContainsKey("WOID") && (dic["WOID"].ToString()!= "ALL"))
            {
                filter.Append("WOID IN (");
                for (int x = 0; x < dic["WOID"].ToString().Split(',').Length; x++)
                {
                    if (x == dic["WOID"].ToString().Split(',').Length-1)
                        filter.Append("{" + y++.ToString() + "})");
                    else
                    filter.Append("{" + y++.ToString() + "},");
                    dicdata.Add("WOID" + x.ToString(), dic["WOID"].ToString().Split(',')[x]);
                }  
            }

            //if (!string.IsNullOrEmpty(rit.PartNumber) && (rit.PartNumber != "ALL"))
            //{
            //    Param = string.Empty;
            //    for (int x = 0; x < rit.PartNumber.Split(',').Length; x++)
            //    {
            //        Param += ("@C_MODEL" + x.ToString() + ",");
            //        cmd.Parameters.Add("C_MODEL" + x.ToString(), MySqlDbType.VarChar).Value = rit.PartNumber.Split(',')[x];
            //    }
            //    sSQL += string.Format(" partnumber in ({0}) and ", Param.Substring(0, Param.Length - 1));

            //    //sSQL += " partnumber in (@C_MODEL) and ";
            //    //cmd.Parameters.Add("C_MODEL", MySqlDbType.VarChar).Value = rit.PartNumber;
            //}

            if (dic.ContainsKey("PARTNUMBER") && (dic["PARTNUMBER"].ToString() != "ALL"))
            {
                filter.Append("PARTNUMBER IN (");
                for (int x = 0; x < dic["PARTNUMBER"].ToString().Split(',').Length; x++)
                {
                    if (x == dic["PARTNUMBER"].ToString().Split(',').Length - 1)
                        filter.Append("{" + y++.ToString() + "})");
                    else
                    filter.Append("{" + y++.ToString() + "}");
                    dicdata.Add("PARTNUMBER" + x.ToString(), dic["PARTNUMBER"].ToString().Split(',')[x]);
                }
            }

            //if (!string.IsNullOrEmpty(rit.CraFtId) && (rit.CraFtId != "ALL"))
            //{
            //    Param = string.Empty;
            //    for (int x = 0; x < rit.CraFtId.Split(',').Length; x++)
            //    {
            //        Param += ("@C_ROUTE" + x.ToString() + ",");
            //        cmd.Parameters.Add("C_ROUTE" + x.ToString(), MySqlDbType.VarChar).Value = rit.CraFtId.Split(',')[x];
            //    }
            //    sSQL += string.Format(" CRAFTID IN ({0}) and ", Param.Substring(0, Param.Length - 1));

            //    //sSQL += " CRAFTID IN (@C_ROUTE) and ";
            //    //cmd.Parameters.Add("C_ROUTE", MySqlDbType.VarChar).Value = rit.CraFtId;
            //}
            if (dic.ContainsKey("CRAFTID") && (dic["CRAFTID"].ToString() != "ALL"))
            {
                filter.Append("CRAFTID IN (");
                for (int x = 0; x < dic["CRAFTID"].ToString().Split(',').Length; x++)
                {
                    if (x == dic["CRAFTID"].ToString().Split(',').Length - 1)
                        filter.Append("{" + y++.ToString() + "})");
                    else
                    filter.Append("{" + y++.ToString() + "}");
                    dicdata.Add("CRAFTID" + x.ToString(), dic["CRAFTID"].ToString().Split(',')[x]);
                }
            }

            //if (!string.IsNullOrEmpty(rit.Line) && (rit.Line != "ALL"))
            //{
            //    Param = string.Empty;
            //    for (int x = 0; x < rit.Line.Split(',').Length; x++)
            //    {
            //        Param += ("@C_LINE" + x.ToString() + ",");
            //        cmd.Parameters.Add("C_LINE" + x.ToString(), MySqlDbType.VarChar).Value = rit.Line.Split(',')[x];
            //    }
            //    sSQL += string.Format(" LINEID IN ({0}) and ", Param.Substring(0, Param.Length - 1));

            //    //sSQL += " LINEID IN (@C_LINE) and ";
            //    //cmd.Parameters.Add("C_LINE", MySqlDbType.VarChar).Value = rit.Line;
            //}
            if (dic.ContainsKey("LINEID") && (dic["LINEID"].ToString() != "ALL"))
            {
                filter.Append("LINEID IN (");
                for (int x = 0; x < dic["LINEID"].ToString().Split(',').Length; x++)
                {
                    if (x == dic["LINEID"].ToString().Split(',').Length - 1)
                        filter.Append("{" + y++.ToString() + "})");
                    else
                    filter.Append("{" + y++.ToString() + "}");
                    dicdata.Add("LINEID" + x.ToString(), dic["LINEID"].ToString().Split(',')[x]);
                }
            }

            if (!string.IsNullOrEmpty(filter.ToString()))
                filter.Append(" AND RClassDate||RWorkSection>={" + y++.ToString() + "}");
            else
              filter.Append(" RClassDate||RWorkSection>={" + y++.ToString() + "}");
              dicdata.Add("RClassDate1",dic["STARTTIME"]);
              filter.Append(" and  RClassDate||RWorkSection<={" + y++.ToString() + "}");
              dicdata.Add("RClassDate2", dic["ENDTIME"]);

              //IList<OrderKey> order = new List<OrderKey>();
              //OrderKey od = new OrderKey();
              //for (int x = 0; x < fieldlist.Split(',').Length; x++)
              //{
              //     od = new OrderKey();
              //     od.KeyName = fieldlist.Split(',')[0];
              //    od.IsAsc = true;
              //    order.Add(od);
              //}

          //  sSQL += string.Format(" RClassDate||RWorkSection>=@Sdate and  RClassDate||RWorkSection<=@Edate GROUP BY {0} ", Colnums);

            //cmd.Parameters.Add("Sdate", MySqlDbType.VarChar).Value = rit.StartTime;
            //cmd.Parameters.Add("Edate", MySqlDbType.VarChar).Value = rit.EndTime;
           // cmd.CommandText = sSQL;

           // return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);



            int count = 0;


            return TransactionManager.GetData(table, fieldlist, filter.ToString(), dicdata, null, fieldlist, out count);


        }

        public System.Data.DataSet QueryRepairStatus(string status)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select a.partnumber,c.productname,a.woId,a.ErrorCode,b.ErrorDesc, count(a.esn) as qty,status from SFCR.T_REPAIR_INFO a,SFCB.B_ERROR_CODE b,SFCB.B_PRODUCT c where status=@status and a.ErrorCode=b.ErrorCode " +
            //                  "and a.partnumber=c.partnumber GROUP BY a.partnumber,c.productname,a.woId,a.ErrorCode,b.ErrorDesc,a.status";
          
            //cmd.Parameters.AddRange(new MySqlParameter[]
            //{
            //    new MySqlParameter("status",MySqlDbType.VarChar){Value=status}
            //});
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            int count = 0;
            string table = string.Format("(select a.partnumber,a.woId wd,a.woId,a.ErrorCode,b.ErrorDesc, count(a.esn) as qty,a.status from SFCR.T_REPAIR_INFO a, SFCB.B_ERROR_CODE b where a.ErrorCode = b.ErrorCode AND a.status = '{0}' GROUP BY a.partnumber,a.woId,a.ErrorCode,b.ErrorDesc,a.status) mm ,SFCB.B_PRODUCT c".ToUpper(),status);
            string fieldlist = "mm.partnumber,c.productname,mm.woId,mm.ErrorCode,mm.ErrorDesc,mm.qty,mm.status".ToUpper();
            string filter = "mm.partnumber = c.partnumber".ToUpper();
            string group = "mm.partnumber,c.productname,mm.woId,mm.ErrorCode,mm.ErrorDesc,mm.status".ToUpper();

            IDictionary<string, object> mst = new Dictionary<string, object>();
          //  mst.Add("STATUS", status);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, group, out count);
             
        }
        public System.Data.DataSet QueryRepairStatus(string StartDate, string EndDate, string Status)
        {
            //OracleCommand cmd = new OracleCommand();
            //cmd.CommandText = "SELECT ERRORCODE,REASONCODE,ESN,WOID,PARTNUMBER,CRAFTID,INPUTUSER,STATUS,RECDATE,OUTDATE,INPUTDATE,REUSER,LINEID,LOCATION,REMARK,TCLASSDATE,TCLASS FROM " +
            //                "SFCR.T_REPAIR_INFO WHERE (TCLASSDATE >=:StartDate and TCLASSDATE <=:EndDate ) and STATUS=:status  ";
            //cmd.Parameters.AddRange(new OracleParameter[]
            //{new OracleParameter("StartDate",OracleDbType.Varchar2){Value=StartDate},
            //    new OracleParameter("EndDate",OracleDbType.Varchar2){Value=EndDate},
            //    new OracleParameter("status",OracleDbType.Varchar2){Value=Status}
            //});
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            int count = 0;
            string table = "SFCR.T_REPAIR_INFO";
            string fieldlist = "ERRORCODE,REASONCODE,ESN,WOID,PARTNUMBER,CRAFTID,INPUTUSER,STATUS,RECDATE,OUTDATE,INPUTDATE,REUSER,LINEID,LOCATION,REMARK,TCLASSDATE,TCLASS";
            string filter = "(TCLASSDATE >={0} and TCLASSDATE <={1} ) and STATUS={2}";
         //   string group = "a.partnumber,c.productname,a.woId,a.ErrorCode,b.ErrorDesc,a.status";
            //IList<OrderKey> order = new List<OrderKey>();
            //OrderKey od1 = new OrderKey();
            //od1.KeyName = "a.StoredprocIdx";
            //od1.IsAsc = true;
            //OrderKey od2 = new OrderKey();
            //od2.KeyName = "a.WORK_TYPE_INDEX ";
            //od2.IsAsc = true;
            //order.Add(od1);
            //order.Add(od2);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("TCLASSDATE1", StartDate);
            mst.Add("TCLASSDATE2", EndDate);
            mst.Add("STATUS", Status);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null,null , out count);
        }

        public System.Data.DataSet GetRepairReport(string StartDate, string EndDate,string Class)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //string sSQL = "select a.ErrorCode,b.ErrorDesc,a.ReasonCode,c.ReasonDesc,a.Duty,a.esn,a.woId,a.partnumber,d.productname,a.craftId," +
            //                  " a.inputuser,a.reuser,a.location,a.remark from SFCR.T_REPAIR_INFO a,SFCB.B_ERROR_CODE b,SFCB.B_REASON_CODE c,SFCB.B_PRODUCT d"+
            //                  " where a.ErrorCode=b.ErrorCode and a.ReasonCode=c.ReasonCode and a.partnumber=d.partnumber and status=3"+
            //                  " and RClassDate>=@Sdate and RClassDate<=@Edate ";
            //if (Class != "ALL")
            //{
            //    sSQL = sSQL + " and RCLASS=@class";
            //    cmd.Parameters.AddRange(new MySqlParameter[]
            //   {
            //    new MySqlParameter("class",MySqlDbType.VarChar){Value=Class}
            //   });
            //}

            //cmd.CommandText = sSQL;
            //cmd.Parameters.AddRange(new MySqlParameter[]
            //{
            //    new MySqlParameter("Sdate",MySqlDbType.VarChar){Value=StartDate},
            //    new MySqlParameter("Edate",MySqlDbType.VarChar){Value=EndDate}
            //});

            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            int count = 0;
            string table = "SFCR.T_REPAIR_INFO a,SFCB.B_ERROR_CODE b,SFCB.B_REASON_CODE c,SFCB.B_PRODUCT d";
            string fieldlist = "a.ErrorCode,b.ErrorDesc,a.ReasonCode,c.ReasonDesc,a.Duty,a.esn,a.woId,a.partnumber,d.productname,a.craftId,a.inputuser,a.reuser,a.location,a.remark";
            string filter = "a.ErrorCode=b.ErrorCode and a.ReasonCode=c.ReasonCode and a.partnumber=d.partnumber and status=3 and RClassDate>={0} and RClassDate<={1}";   
        
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("RClassDate", StartDate);
            mst.Add("RClassDate1", EndDate);
            if (Class != "ALL")
            {
                filter += " and RCLASS={2}";
                mst.Add("RCLASS", Class);
            }
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }


        public System.Data.DataSet GetDutyInfo()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select Duty,DutyDesc,recdate from SFCB.B_DUTY_INFO";
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);


            string table = "SFCB.B_DUTY_INFO";
            string fieldlist = "Duty,DutyDesc,recdate";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);            
            return dp.GetData(table, fieldlist, null, out count);
        }

        public string InsertDutyInfo(string Duty, string DutyDesc)
        {
            try
            {
            //    MySqlCommand cmd = new MySqlCommand();
            //    cmd.CommandText = "insert into SFCB.B_DUTY_INFO (Duty,DutyDesc) values (@Duty,@DutyDesc)";
            //    cmd.Parameters.AddRange(new MySqlParameter[]
            //{
            //    new MySqlParameter("Duty",MySqlDbType.VarChar){Value=Duty},
            //    new MySqlParameter("DutyDesc",MySqlDbType.VarChar){Value=DutyDesc}
            //});

            //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

                string table = "SFCB.B_DUTY_INFO";
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("Duty", Duty);
                mst.Add("DutyDesc", DutyDesc);
                dp.DeleteData(table, mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteDutyInfo(string Duty)
        {
            try
            {
            //    MySqlCommand cmd = new MySqlCommand();
            //    cmd.CommandText = "delete from SFCB.B_DUTY_INFO where Duty=@Duty ";
            //    cmd.Parameters.AddRange(new MySqlParameter[]
            //{
            //    new MySqlParameter("Duty",MySqlDbType.VarChar){Value=Duty}
            //});

            //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("Duty", Duty);
                dp.DeleteData("SFCB.B_DUTY_INFO", mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateRepairInformation(string ListDic_Repair)
        {
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = null;
            try
            {
                //string workdate = "NA";
                string worksection = "NA";
                string classdate = "NA";
                string sclass = "NA";
                string _StrErr = string.Empty;
                IList<IDictionary<string, object>> LsDic = MapListConverter.JsonToListDictionary(ListDic_Repair);

//                OracleCommand scmd = new OracleCommand();
//                scmd.CommandText = @"select TO_CHAR(SYSDATE,'YYYYMMDD') workdate,WorkSection,Day,Class from SFCB.B_WORK_CLASS  where
//                                  StartTime<=TO_CHAR(SYSDATE,'HH24mi')
//                                  and EndTime> TO_CHAR(SYSDATE,'HH24mi')";
//                DataTable dt = BLL.BllMsSqllib.Instance.ExecuteDataSet(scmd).Tables[0];
//                try
//                {
//                    workdate = dt.Rows[0][0].ToString();
//                    worksection = dt.Rows[0][1].ToString();
//                    if (dt.Rows[0][2].ToString() == "TOMORROW")
//                        classdate = (Convert.ToInt32(workdate) - 1).ToString();
//                    else
//                        classdate = workdate;
//                    sclass = dt.Rows[0][3].ToString();
//                }
//                catch
//                {
//                }

                PRO_GETWORKCLASS(out worksection, out sclass, out classdate, out _StrErr);
                if (_StrErr != "OK")
                    throw new Exception(_StrErr);

                    foreach (Dictionary<string, object> dic in LsDic)
                    {
                        if (!string.IsNullOrEmpty(dic["ID"].ToString()) && dic["ID"].ToString()!="NA")
                        {
                          
                            mst = new Dictionary<string, object>();
                            mst.Add("REASONCODE", dic["REASON_CODE"]);
                            mst.Add("RECDATE", System.DateTime.Now);
                            mst.Add("STATUS", "2");
                            mst.Add("REUSER", dic["REUSER"]);
                            mst.Add("LOCATION", dic["LOCATION"]);
                            mst.Add("REMARK", dic["REMARK"]);
                            mst.Add("OUTCRAFTID", dic["OUTCRAFTID"]);
                            mst.Add("RCLASSDATE", classdate);
                            mst.Add("RCLASS", sclass);
                            mst.Add("RWORKSECTION", worksection);
                            mst.Add("DUTY", dic["DUTY"]);
                            mst.Add("ROWID", dic["ID"]);
                            dp.UpdateData(tx, "SFCR.T_REPAIR_INFO", new string[] { "ROWID" }, mst);

                        }
                        else
                        {
                      
                            mst = new Dictionary<string, object>();
                            mst.Add("ERRORCODE", dic["ERROR_CODE"]);
                            mst.Add("REASONCODE", dic["REASON_CODE"]);
                            mst.Add("ESN", dic["ESN"]);
                            mst.Add("WOID", dic["WOID"]);
                            mst.Add("PARTNUMBER", dic["PARTNUMBER"]);
                            mst.Add("CRAFTID", dic["CRAFTID"]);
                            mst.Add("INPUTUSER", dic["REUSER"]);
                            mst.Add("STATUS","2");
                            mst.Add("RECDATE", System.DateTime.Now);
                            mst.Add("REUSER", dic["REUSER"]);
                            mst.Add("LINEID", dic["LINE"]);
                            mst.Add("LOCATION", dic["LOCATION"]);
                            mst.Add("REMARK", dic["REMARK"]);
                            mst.Add("OUTCRAFTID", dic["OUTCRAFTID"]);
                            mst.Add("TCLASSDATE", classdate);
                            mst.Add("TCLASS", sclass);
                            mst.Add("TWORKSECTION", worksection);
                            mst.Add("RCLASSDATE", classdate);
                            mst.Add("RCLASS", sclass);
                            mst.Add("RWORKSECTION", worksection);
                            mst.Add("DUTY", dic["DUTY"]);
                            dp.AddData(tx, "SFCR.T_REPAIR_INFO", mst);
                        }
                    }
                 

            

                //List<OracleCommand> LsCmd = new List<OracleCommand>();
                //foreach (Entity.tRepairInfoTable Repair in ListRepair)
                //{
                //    OracleCommand cmd = null;
                //    if (!string.IsNullOrEmpty(Repair.id))
                //    {
                //        cmd = new OracleCommand();
                //        cmd.CommandText = "UPDATE SFCR.T_REPAIR_INFO set ReasonCode=:C_RC,recdate=sysdate,status='2',reuser=:C_USER,location=:C_LOCATION, remark=:C_REMARK,outcraftId=:C_NEXTSTATION,RClassDate=:C_CLASSDATE,RCLASS=:C_CLASS,RWorkSection=:C_WORKSECTION,duty=:C_DUTY where rowid=:C_ROWID".ToUpper();

                //        cmd.Parameters.AddRange(new OracleParameter[]
                //    {
                //        new OracleParameter("C_RC",OracleDbType.Varchar2){Value=Repair.ReasonCode},
                //        new OracleParameter("C_USER",OracleDbType.Varchar2){Value=Repair.ReUser},
                //        new OracleParameter("C_LOCATION",OracleDbType.Varchar2){Value=Repair.Location},
                //        new OracleParameter("C_REMARK",OracleDbType.Varchar2){Value=Repair.Remark},
                //        new OracleParameter("C_NEXTSTATION",OracleDbType.Varchar2){Value=Repair.OutcraftId},
                //        new OracleParameter("C_ROWID",OracleDbType.Varchar2){Value=Repair.id.ToString()},
                //        new OracleParameter("C_CLASSDATE",OracleDbType.Varchar2){Value=classdate},
                //        new OracleParameter("C_CLASS",OracleDbType.Varchar2){Value=sclass},
                //        new OracleParameter("C_WORKSECTION",OracleDbType.Varchar2){Value=worksection},
                //        new OracleParameter("C_DUTY",OracleDbType.Varchar2){Value=Repair.Duty}
                //      });
                //        LsCmd.Add(cmd);
                //    }
                //    else
                //    {
                //        string sSQL = "INSERT INTO SFCR.T_REPAIR_INFO (ERRORCODE,REASONCODE,ESN,WOID,PARTNUMBER,CRAFTID,INPUTUSER,STATUS,RECDATE,REUSER,LINEID,LOCATION,REMARK,OUTCRAFTID,TCLASSDATE,TCLASS,TWORKSECTION,RCLASSDATE,RCLASS,RWORKSECTION,DUTY) VALUES ";
                //        sSQL += "(:C_ERRORCODE,:C_REASONCODE,:C_ESN,:C_WOID,:C_PARTNUMBER,:C_CRAFTID,:C_REUSER,:C_STATUS,SYSDATE,:C_REUSER,:C_LINEID,:C_LOCATION,:C_REMARK,:C_OUTCRAFTID,:C_TCLASSDATE,:C_TCLASS,:C_TWORKSECTION,:C_TCLASSDATE,:C_TCLASS,:C_TWORKSECTION,:C_DUTY)";
                //        cmd = new OracleCommand();
                //        cmd.CommandText = sSQL;
                //        cmd.Parameters.AddRange(new OracleParameter[]
                //    {
                //        new OracleParameter("C_ERRORCODE",OracleDbType.Varchar2){Value=Repair.ErrorCode},
                //        new OracleParameter("C_REASONCODE",OracleDbType.Varchar2){Value=Repair.ReasonCode},
                //        new OracleParameter("C_ESN",OracleDbType.Varchar2){Value=Repair.ESN},
                //        new OracleParameter("C_WOID",OracleDbType.Varchar2){Value=Repair.woId},
                //        new OracleParameter("C_PARTNUMBER",OracleDbType.Varchar2){Value=Repair.PartNumber},
                //        new OracleParameter("C_CRAFTID",OracleDbType.Varchar2){Value=Repair.CraFtId},
                //        new OracleParameter("C_REUSER",OracleDbType.Varchar2){Value=Repair.ReUser},
                //        new OracleParameter("C_STATUS",OracleDbType.Varchar2){Value="2"},
                //        new OracleParameter("C_LINEID",OracleDbType.Varchar2){Value=Repair.Line},
                //        new OracleParameter("C_LOCATION",OracleDbType.Varchar2){Value=Repair.Location},
                //        new OracleParameter("C_REMARK",OracleDbType.Varchar2){Value=Repair.Remark},
                //        new OracleParameter("C_OUTCRAFTID",OracleDbType.Varchar2){Value=Repair.OutcraftId},
                //         new OracleParameter("C_TCLASSDATE",OracleDbType.Varchar2){Value=classdate},                       
                //        new OracleParameter("C_TCLASS",OracleDbType.Varchar2){Value=sclass},
                //        new OracleParameter("C_TWORKSECTION",OracleDbType.Varchar2){Value=worksection},
                //        new OracleParameter("C_DUTY",OracleDbType.Varchar2){Value=Repair.Duty}                        
                //      });
                //        LsCmd.Add(cmd);
                //    }
                //}

                //BLL.BllMsSqllib.Instance.ExecteNonQueryArr(LsCmd);
                    tx.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                tx.Rollback();
                return ex.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public string InsertRepairMaterialInfo(string dicListReMaterial)
        {
            try
            {
              IList<IDictionary<string,object>> LsDic=  MapListConverter.JsonToListDictionary(dicListReMaterial);
              IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
              //IList<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
              //IDictionary<string, object> mst = null;

              //foreach (IDictionary<string, object> ReMaterial in LsDic)
              //  {

              //      //OracleCommand cmd = new OracleCommand();
              //      //cmd.CommandText = "INSERT INTO SFCR.T_REPAIR_MATERIAL_INFO (ESN,KPNO,VENDERCODE,DATECODE,LOTCODE) VALUES (:SN,:kpno,:vc,:dc,:lc)";

              //      //cmd.Parameters.AddRange(new OracleParameter[]
              //      //{             
              //      //    new OracleParameter("SN",OracleDbType.Varchar2){Value=ReMaterial.ESN},
              //      //    new OracleParameter("kpno",OracleDbType.Varchar2){Value=ReMaterial.PartNo},          
              //      //    new OracleParameter("vc",OracleDbType.Varchar2){Value=ReMaterial.VenderCode},
              //      //    new OracleParameter("dc",OracleDbType.Varchar2){Value=ReMaterial.DateCode},
              //      //    new OracleParameter("lc",OracleDbType.Varchar2){Value=ReMaterial.LotCode}
              //      //});
              //      //LsCmd.Add(cmd);

                  
              //      mst.Add("errorcode", "Insert4");
              //      mst.Add("errordesc", "Insert5");
              //      mst.Add("errordesc2", "Insert6");
              //      mst.Add("recdate", "2015-01-10");                   

              //      list.Add(mst);                   
                   
              //  }
              dp.AddListData("SFCR.T_REPAIR_MATERIAL_INFO", LsDic);
               // BLL.BllMsSqllib.Instance.ExecteNonQueryArr(LsCmd);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
        private void PRO_GETWORKCLASS(out string C_WORKSECTION, out string C_CLASS, out string C_DAY, out string ERROR)
        {
            C_WORKSECTION = string.Empty;
            C_CLASS = string.Empty;
            C_DAY = string.Empty;
            ERROR = "OK";
            try
            {
                //                MySqlCommand cmd = new MySqlCommand();
                //                cmd.CommandText = @"SELECT WORKSECTION, CLASS, DAY FROM SFCB.B_WORK_CLASS WHERE STARTTIME <= TIME_FORMAT(SYSDATE(),'%H%i')
                //                 AND ENDTIME > TIME_FORMAT(SYSDATE(),'%H%i')";
                //                DataTable dt = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];

                int count = 0;
                string table = "SFCB.B_WORK_CLASS";
                string fieldlist = "WORKSECTION, CLASS, DAY";
                string filter = "STARTTIME <= TIME_FORMAT(SYSDATE(),'%H%i') AND ENDTIME > TIME_FORMAT(SYSDATE(),'%H%i')";
                DataTable dt = TransactionManager.GetData(table, fieldlist, filter, null, null, null, out count).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    C_WORKSECTION = dt.Rows[0]["WORKSECTION"].ToString();
                    C_CLASS = dt.Rows[0]["CLASS"].ToString();
                    C_DAY = dt.Rows[0]["DAY"].ToString();
                }
                else
                {
                    ERROR = "NO DATA";
                }
            }
            catch
            {
                ERROR = "WORK CLASS EXCEPTION";
            }
        }
    }
}
