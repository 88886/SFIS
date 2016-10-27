using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using System.Data.Common;
using SystemObject;
using GenericUtil;
using SrvComponent;

namespace BLL
{
    public partial class tWipTracking
    {

        public tWipTracking()
        {
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)
                DB_Flag = 0;
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
                DB_Flag = 1;
        }
        /// <summary>
        /// 数据库标记 0 MySQL;1 Oracle
        /// </summary>
        int DB_Flag = 0;       
    
        System.Diagnostics.Stopwatch myWatch = System.Diagnostics.Stopwatch.StartNew();
        public System.Data.DataSet Get_WIP_TRACKING(string ColumnName, string Data)
        {
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add(ColumnName, Data);
            return GetWipTracking(mst, null);
        }
        public System.Data.DataSet Get_WIP_TRACKING(string ColumnName, string Data, string Fields)
        {         
                 string Colnum = "ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE,"+
                            "ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,LINE,SECTIONNAME,ROUTGROUPID,"+
                            "STORENUMBER,WEIGHTQTY,QA_NO,QA_RESULT,TRACK_NO,ATE_STATION_NO,IN_LINE_TIME,BOMNUMBER,REWORKNO";
       

            if (string.IsNullOrEmpty(Fields))
                Fields = Colnum;       
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add(ColumnName, Data);

            return GetWipTracking(mst, Fields);
        }

        public DataSet GetWipTracking(IDictionary<string, object> mst, string Fields)
        {
            string Colnum = "ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE," +
                            "ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,LINE,SECTIONNAME,ROUTGROUPID," +
                            "STORENUMBER,WEIGHTQTY,QA_NO,QA_RESULT,TRACK_NO,ATE_STATION_NO,IN_LINE_TIME,BOMNUMBER,REWORKNO";
           
            string table = "SFCR.T_WIP_TRACKING_ONLINE";
            if (DB_Flag == 1)
                table = "SFCR.T_WIP_TRACKING";

            if (string.IsNullOrEmpty(Fields))
                Fields = Colnum;

            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN); 
            DataSet ds = dp.GetData(table, Fields, mst, out count);
            return ds;
        }
    


        public System.Data.DataSet GetWipTrackingList(List<string> Model,List<string> WoId)
        {
//            MySqlCommand cmd = new MySqlCommand();
//            string sPartCol = "";
//            int x = 0;
//            foreach (string Item in Model)
//            {
//                x++;
//                sPartCol += "@sPart" + x.ToString() + ",";
//                cmd.Parameters.Add("sPart" + x.ToString(), MySqlDbType.VarChar).Value = Item;
//            }
//            sPartCol = sPartCol.Substring(0, sPartCol.Length - 1);
      
//            string sMoCol = "";
//            int y = 0;
//            foreach (string item in WoId)
//            {
//                y++;
//                sMoCol += "@sMO" + y.ToString() + ",";
//                cmd.Parameters.Add("sMO" + y.ToString(), MySqlDbType.VarChar).Value = item;
//            }
//            sMoCol = sMoCol.Substring(0, sMoCol.Length - 1);

//            cmd.CommandText =string.Format( @"select woId,partnumber,productname,wipstation,count(esn) as qty from sfcr.T_WIP_TRACKING_ONLINE where woId in ({0}) and PartNumber in ({1})
//                                group by woId,partnumber,productname,wipstation", sMoCol, sPartCol);
           
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);


            int count = 0;

            string table = "SFCR.T_WIP_TRACKING_ONLINE";
            if (DB_Flag == 1)
                table = "SFCR.T_WIP_TRACKING";
            string fieldlist = "woId,partnumber,productname,wipstation,count(esn) as qty".ToUpper();
            string filter = "PARTNUMBER IN (";
            int x = -1;
            IDictionary<string, object> mst = new Dictionary<string, object>();
           
             foreach (string Item in Model)
             {
                 x++;
                 filter += "{" + x.ToString() + "},";       
                 mst.Add("PARTNUMBER" + x.ToString(), Item);
             }
             filter = filter.Substring(0, filter.Length - 1);
             filter += ") AND WOID IN (";
             foreach (string Item in WoId)
             {
                 x++;
                 filter += "{" + x.ToString() + "},";            
                 mst.Add("WOID" + x.ToString(), Item);
             }
             filter = filter.Substring(0, filter.Length - 1);
             filter += ")";


             return TransactionManager.GetData(table, fieldlist, filter, mst, null, "woId,partnumber,productname,wipstation".ToUpper(), out count);
        }

        public System.Data.DataSet GetWipTrackingLineList(List<string> Model, List<string> WoId)
        {           
//            MySqlCommand cmd = new MySqlCommand();
//            string sPartCol = "";
//            int x = 0;
//            foreach (string Item in Model)
//            {
//                x++;
//                sPartCol += "@sPart" + x.ToString() + ",";
//                cmd.Parameters.Add("sPart" + x.ToString(), MySqlDbType.VarChar).Value = Item;
//            }
//            sPartCol = sPartCol.Substring(0, sPartCol.Length - 1);

//            string sMoCol = "";
//            int y = 0;
//            foreach (string item in WoId)
//            {
//                y++;
//                sMoCol += "@sMO" + y.ToString() + ",";
//                cmd.Parameters.Add("sMO" + y.ToString(), MySqlDbType.VarChar).Value = item;
//            }
//            sMoCol = sMoCol.Substring(0, sMoCol.Length - 1);

//            cmd.CommandText = string.Format(@"select woId,partnumber,productname,line,wipstation,count(esn) from sfcr.T_WIP_TRACKING_ONLINE where woId in ({0}) and PartNumber in ({1})
//                                 group by woId,partnumber,productname,line,wipstation", sMoCol, sPartCol);

//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            int count = 0;
            string table = "SFCR.T_WIP_TRACKING_ONLINE";
            if (DB_Flag == 1)
                table = "SFCR.T_WIP_TRACKING";
            string fieldlist = "woId,partnumber,productname,line,wipstation,count(esn) as qty".ToUpper();
            string filter = "PARTNUMBER IN (";
            int x = -1;
            IDictionary<string, object> mst = new Dictionary<string, object>();

            foreach (string Item in Model)
            {
                x++;
                filter += "{" + x.ToString() + "},";
                // cmd.Parameters.Add("sPart" + x.ToString(), MySqlDbType.VarChar).Value = Item;
                mst.Add("PARTNUMBER" + x.ToString(), Item);
            }
            filter = filter.Substring(0, filter.Length - 1);
            filter += ") AND WOID IN (";
            foreach (string Item in WoId)
            {
                x++;
                filter += "{" + x.ToString() + "},";
                mst.Add("WOID" + x.ToString(), Item);
            }
            filter = filter.Substring(0, filter.Length - 1);
            filter += ")";


            return TransactionManager.GetData(table, fieldlist, filter, mst, null, "woId,partnumber,productname,line,wipstation".ToUpper(), out count);
          
        }

        public System.Data.DataSet GetPartNumberAndwoIdList()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select a.partnumber,a.woid,b.productname from SFCR.T_WO_INFO a,sfcb.b_product b where wostate in ('0','2') and a.partnumber = b.partnumber";
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            string table = "SFCR.T_WO_INFO";
            string fieldlist = "partnumber,woid,productname".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOSTATE", "2");
           return dp.GetData(table, fieldlist, mst, out count);
        }

        public void UpdateWipCartonTrayPallet(int PackType, string Data, string Data2, string ESN)
        {
            ////PackType  0@ TRAY 1@ CARTON  2@ PALLET 
            //MySqlCommand cmd = new MySqlCommand();
            //string sSQL = " update sfcr.T_WIP_TRACKING_ONLINE ";
            //if (PackType == 0)
            //{
            //    sSQL = sSQL + " set TrayNO=@tray ";
            //    cmd.Parameters.Add("tray", MySqlDbType.VarChar, 20).Value = Data;

            //}
            //else
            //    if (PackType == 1)
            //    {
            //        sSQL = sSQL + " set cartonnumber=@carton,mcartonnumber=@mcarton ";
            //        cmd.Parameters.Add("carton", MySqlDbType.VarChar, 20).Value = Data;
            //        cmd.Parameters.Add("mcarton", MySqlDbType.VarChar, 20).Value = Data2;
            //    }
            //    else
            //        if (PackType == 2)
            //        {
            //            sSQL = sSQL + " set palletnumber=@pallet,mpalletnumber=@mpallet";
            //            cmd.Parameters.Add("pallet", MySqlDbType.VarChar, 20).Value = Data;
            //            cmd.Parameters.Add("mpallet", MySqlDbType.VarChar, 20).Value = Data2;
            //        }

            //sSQL = sSQL + " where esn=@esn ";
            //cmd.Parameters.Add("esn", MySqlDbType.VarChar, 50).Value = ESN;
            //cmd.CommandText = sSQL;
            //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);


            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            switch (PackType)
            {
                case 0:
                    mst.Add("TRAYNO", Data);
                    break;
                case 1:
                    mst.Add("CARTONNUMBER", Data);
                    mst.Add("MCARTONNUMBER", Data2);
                    break;

                case 2:
                     mst.Add("PALLETNUMBER", Data);
                     mst.Add("MPALLETNUMBER", Data2);
                    break;
            }
            mst.Add("ESN", ESN);
            string table = "SFCR.T_WIP_TRACKING_ONLINE";
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
                table = "SFCR.T_WIP_TRACKING";
            dp.UpdateData(table, new string[] { "ESN" }, mst);
        }

        //public void UpdateWipStockInNumber(string ColumnName, string Data, string StockNo)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    string sSQL = " update sfcr.T_WIP_TRACKING_ONLINE set storenumber=@stor where  " + ColumnName + " = @data";
        //    cmd.CommandText = sSQL;
        //    cmd.Parameters.Add("stor", MySqlDbType.VarChar, 20).Value = StockNo;
        //    cmd.Parameters.Add("data", MySqlDbType.VarChar, 20).Value = Data;
        //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //}

        //public void UpdateStockInWipGroup(string wip, string esn)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "update sfcr.T_WIP_TRACKING_ONLINE set wipstation=@wip where  esn = @data ";
        //    cmd.Parameters.Add("wip", MySqlDbType.VarChar, 20).Value = wip;
        //    cmd.Parameters.Add("data", MySqlDbType.VarChar, 20).Value = esn;
        //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //}
        //public System.Data.DataSet GetStockInPrint(string StockIn)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "select a.woId,a.partnumber,a.wipstation,b.productname,count(a.esn) qty from sfcr.T_WIP_TRACKING_ONLINE a, tProduct b " +
        //                     " where a.storenumber=@stcok and a.partnumber = b.partnumber group by a.woId,a.partnumber,a.wipstation,b.productname ";
        //    cmd.Parameters.Add("stcok", MySqlDbType.VarChar, 20).Value = StockIn;
        //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}

        //public string InsertWipUndo(Entity.tWipTrackingTable wtt)
        //{
        //    string res = "";
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "insert into SFCR.T_WIP_UNDO select newid(),esn,woId,partnumber,locstation,wipstation,nextstation,userId,recdate,errflag,scrapflag" +
        //                    " ,SN,MAC,cartonnumber,TrayNO,palletnumber,mcartonnumber,mpalletnumber,line,routgroupId,storenumber from twiptracking where esn=@esn";
        //    cmd.Parameters.Add("esn", MySqlDbType.VarChar, wtt.ESN.Length).Value = wtt.ESN;


        //    try
        //    {
        //        BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //    return res;
        //}

        public string UpdateScrapWipTracking(string dicstring)
        {
           // string C_RES = "OK";
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicstring);

            //List<MySqlCommand> LsCmd = new List<MySqlCommand>();

            //MySqlCommand cmd = null;

            //cmd = new MySqlCommand();
            //cmd.CommandText = "insert into sfcr.t_wip_undo select * from sfcr.T_WIP_TRACKING_ONLINE where esn=@c_esn";
            //cmd.Parameters.Add("c_esn", MySqlDbType.VarChar, wtt.ESN.Length).Value = wtt.ESN;
            //LsCmd.Add(cmd);

            //cmd = new MySqlCommand();
            //cmd.CommandText = "update SFCR.T_WIP_TRACKING_ONLINE  set locstation=@loc, nextstation=@next,userid=@suID,wipstation=@wip,storenumber=@stockno,line=@sline,scrapflag=@scflag,recdate=SYSDATE() where esn=@SN";
            //cmd.Parameters.Add("loc",MySqlDbType.VarChar, wtt.locstation.Length).Value = wtt.locstation;
            //cmd.Parameters.Add("next", MySqlDbType.VarChar, wtt.nextstation.Length).Value = wtt.nextstation;
            //cmd.Parameters.Add("wip", MySqlDbType.VarChar, wtt.wipstation.Length).Value = wtt.wipstation;
            //cmd.Parameters.Add("stockno", MySqlDbType.VarChar, wtt.storenumber.Length).Value = wtt.storenumber;
            //cmd.Parameters.Add("sline", MySqlDbType.VarChar, wtt.line.Length).Value = wtt.line;
            //cmd.Parameters.Add("SN", MySqlDbType.VarChar, wtt.ESN.Length).Value = wtt.ESN;
            //cmd.Parameters.Add("scflag", MySqlDbType.VarChar, wtt.scrapflag.Length).Value = wtt.scrapflag;
            //cmd.Parameters.Add("suID", MySqlDbType.VarChar, wtt.userId.Length).Value = wtt.userId;

            //LsCmd.Add(cmd);
            //cmd = new MySqlCommand();
            //cmd.CommandText = "update sfcr.t_wo_info set scrapqty=scrapqty+1 where woId=@sMO";
            //cmd.Parameters.Add("sMO", MySqlDbType.VarChar, wtt.WO.Length).Value = wtt.WO;

            //try
            //{
            //    BLL.BllMsSqllib.Instance.ExecteNonQueryArr(LsCmd);
            //    res = "OK";
            //}
            //catch (Exception ex)
            //{
            //    res = ex.Message;
            //}
            //return res;
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                DataTable dt = Get_WIP_TRACKING("ESN", dic["ESN"].ToString()).Tables[0];
                IDictionary<string, object> mst = new Dictionary<string, object>();
                for (int x = 0; x < dt.Columns.Count; x++)
                {
                    mst.Add(dt.Columns[x].ColumnName, dt.Rows[0][x].ToString());
                }

                dp.AddData(tx, "sfcr.t_wip_undo".ToUpper(), mst);

                mst = new Dictionary<string, object>();
                mst.Add("LOCSTATION", dic["LOCSTATION"]);
                mst.Add("NEXTSTATION", dic["NEXTSTATION"]);
                mst.Add("USERID", dic["USERID"]);
                mst.Add("WIPSTATION", dic["WIPSTATION"]);
                mst.Add("STORENUMBER", dic["STORENUMBER"]);
                mst.Add("LINE", dic["LINE"]);
                mst.Add("SCRAPFLAG", dic["SCRAPFLAG"]);
                mst.Add("ESN", dic["ESN"]);
                string table="SFCR.T_WIP_TRACKING_ONLINE";
                if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
                    table = "SFCR.T_WIP_TRACKING";
                dp.UpdateData(tx, table, new string[] { "ESN" }, mst);

                Dictionary<string, object> mst_scrap = new Dictionary<string, object>();
                mst_scrap.Add("SCRAP_NO", dic["STORENUMBER"]);
                mst_scrap.Add("USERID", dic["USERID"]);
                mst_scrap.Add("WOID", dic["WOID"]);
                mst_scrap.Add("PARTNUMBER", "NA");
                mst_scrap.Add("PRODUCTNAME", "NA");
                mst_scrap.Add("VERSION_CODE", "NA");
                mst_scrap.Add("ESN", dic["ESN"]);
                mst_scrap.Add("LINEID", dic["LINE"]);
                mst_scrap.Add("SECTION_NAME", "NA");
                mst_scrap.Add("LOCSTATION", "NA");
                mst_scrap.Add("STATION_NAME", "NA");
                mst_scrap.Add("REMARK", "NA");
                mst_scrap.Add("SCRAP_REASON", "NA");
                mst_scrap.Add("SCRAP_FLAG", "1");
                mst_scrap.Add("SCRAP_KIND", "2");
                mst_scrap.Add("REASON_CODE","NA");
                mst_scrap.Add("REASON_TYPE", "NA");
                mst_scrap.Add("DUTY_STATION", "NA");
                mst_scrap.Add("QTY", 1);
                mst_scrap.Add("GD", "NA");
                mst_scrap.Add("LOC", "NA");
                mst_scrap.Add("PRE_SCRAP_GROUP_NAME","NA");
                mst_scrap.Add("ITHT", "Y");
                dp.AddData(tx, "SFCR.T_SN_SCRAP", mst_scrap);

                StringBuilder ofilter = new StringBuilder();
                ofilter.Append(" SCRAPQTY=SCRAPQTY+{0} ");
                IDictionary<string, object> modFields = new Dictionary<string, object>();
                modFields.Add("SCRAPQTY",1);

                string filter = "WOID = {0}";
                IDictionary<string, object> keyVals = new Dictionary<string, object>();
                keyVals.Add("WOID", dic["WOID"]);

                dp.UpdateBatchData(tx,"SFCR.T_WO_INFO", ofilter.ToString(), modFields, filter, keyVals);
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
                

        /// <summary>
        /// 根据Esn查询数据信息
        /// </summary>
        /// <param name="sntype">序列号类型</param>
        /// <param name="snval">序列号的值</param>
        /// <returns></returns>
        public DataSet GetEsnDataInfo(string sntype, string snval)
        {
            DataTable _dt = string.IsNullOrEmpty(sntype) ? null : this.GetPwdColumns(sntype).Tables[0];
            string _sntype = string.Empty;
            if (_dt != null && _dt.Rows.Count > 0)
                return null;      
            DataSet ds = null;         

            string table = "SFCR.T_WIP_KEYPART_ONLINE";
            if (DB_Flag == 1)
                table = "SFCR.T_WIP_KEYPART";
            string fieldlist = "ESN,WOID,SNTYPE,SNVAL";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = null;
            string _ESN = snval;
            if (sntype.ToUpper() != "ESN")
            {
                mst = new Dictionary<string, object>();
                mst.Add("SNVAL", _ESN);
              DataSet dsSnval=  dp.GetData(table, fieldlist, mst, out count);
              if (dsSnval.Tables[0].Rows.Count > 0)
              {
                  _ESN = dsSnval.Tables[0].Rows[0]["ESN"].ToString();
              }
            }
            mst = new Dictionary<string, object>();
            mst.Add("ESN", _ESN);
            ds = dp.GetData(table, fieldlist, mst, out count);
            return ds; //BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 插入数据(wipkeypart集合)
        /// </summary>
        /// <param name="lsWipKeyPart"></param>
        /// <returns></returns>
        public string InsertWipKeyParts(IList<IDictionary <string,object>> lsdic )
        {          
            List<string> lssntype = new List<string>();
            List<string> lssnval = new List<string>();
            List<string> lsesn = new List<string>();
            List<string> lswoid = new List<string>();
            DataTable dtTemp = new DataTable();
            string err = string.Empty;
          //  List<Entity.tWipKeyPartTable> _lswipkeypart = new List<Entity.tWipKeyPartTable>();
            List<Dictionary<string, object>> _lsdic = new List<Dictionary<string, object>>();
            if (lsdic.Count < 1)
                return "没有要插入的数据";
            #region 检查内容规则
            foreach (Dictionary<string,object> item in lsdic)
            {
                if (string.IsNullOrEmpty(item["ESN"].ToString()) ||
                    string.IsNullOrEmpty(item["WOID"].ToString()) ||
                    string.IsNullOrEmpty(item["SNTYPE"].ToString()) ||
                    string.IsNullOrEmpty(item["SNVAL"].ToString()))
                {
                    return string.Format("输入的数据存在空值@\nESN@{0}\nwoId@{1}\nSnType@{2}\nSN@{3}",
                        item["ESN"].ToString(), item["WOID"].ToString(), item["SNTYPE"].ToString(), item["SNVAL"].ToString());
                }
                if (lswoid.Count > 0)
                {
                    if (!ChkList(item["WOID"].ToString(), lswoid))
                        return string.Format("严重错误@存在不同的工单号{0}≠{1}",
                            item["WOID"].ToString(), lswoid[0]);
                }
                else
                {
                    lswoid.Add(item["WOID"].ToString());
                }

                if (lsesn.Count > 0)
                {
                    if (!ChkList(item["ESN"].ToString(), lsesn))
                        return string.Format("同一笔数据存在不相同的ESN号[{0}]≠[{1}]",
                            item["ESN"].ToString(), lsesn[0]);
                }
                else
                {
                    lsesn.Add(item["ESN"].ToString());
                }

                if (ChkList(item["SNVAL"].ToString(), lssnval))
                {
                    if (!ChkList(item["SNVAL"].ToString(), lsesn)) //如果ESN和其他的序列号重复 允许
                        return string.Format("序列号[{0}]重复", item["SNVAL"].ToString());
                    else
                        lssnval.Add(item["SNVAL"].ToString());
                }
                else
                {
                    lssnval.Add(item["SNVAL"].ToString());
                }

                if (ChkList(item["SNTYPE"].ToString(), lssntype))
                    return string.Format("同一个产品的序号类型[{0}]重复", item["SNTYPE"].ToString());
                else
                {
                    lssntype.Add(item["SNTYPE"].ToString());
                }

            }
            #endregion
            DataTable _dtPwd = this.GetPwdColumns(string.Empty).Tables[0];
            DataTable _dtEsn = this.GetEsnDataInfo("ESN", lsdic[0]["ESN"].ToString()).Tables[0];// this.GetSnInfo(lsWipKeyPart[0].esn).Tables[0];
            #region 比对实体列表中的数据是否符合规则
            if (_dtEsn == null || _dtEsn.Rows.Count < 1)
            {//如果根据esn找的数据不存在
                //则检查当前数据是否重复
                //首先判断是否是密码类型
                foreach (Dictionary<string,object> item in lsdic)
                {
                    DataRow[] dr = _dtPwd.Select(string.Format("PWDNAME='{0}'", item["SNTYPE"].ToString()));
                    if (dr == null || dr.Length < 1)
                    {//不是密码类型,则需要比对是否重复，因是_dtEsn为空那么只要找到数据则表示数据有重复
                        dtTemp = this.GetSnInfo(item["SNVAL"].ToString()).Tables[0];
                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                            return string.Format("序列号{0}重复", item["SNVAL"].ToString());
                    }
                  //  _lswipkeypart.Add(item);//记录要插入数据库的数据  
                    _lsdic.Add(item);
                }
            }
            else
            {//如果根据esn找到的数据存在
                foreach (Dictionary<string,object> item in lsdic)
                {//用实体里的序列号类型去找
                    DataRow[] _dr = _dtEsn.Select(string.Format("sntype='{0}'", item["SNTYPE"].ToString()));
                    //如果没有找到 那么还要使用这个类型的值去找该值是否重复
                    if (_dr == null || _dr.Length < 1)
                    {
                        dtTemp = this.GetSnInfo(item["SNVAL"].ToString()).Tables[0];
                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                            return string.Format("序列号{0}重复", item["SNVAL"].ToString());
                    }
                    else
                    {
                        //如果找到了 那么需要比对找到的值和当前的值是否一致
                        if (_dr[0]["snval"].ToString().ToUpper() != item["SNVAL"].ToString().ToUpper())
                            return string.Format("序列号:[{0}]当前数据和历史数据不相等{1}≠{2}",
                                item["SNTYPE"].ToString(), item["SNVAL"].ToString(), _dr[0]["snval"].ToString());
                        else
                            continue;
                    }
                 //   _lswipkeypart.Add(item);
                    _lsdic.Add(item);
                }
            }
            #endregion
            #region 保存数据
            foreach (Dictionary<string, object> item in _lsdic)
            {
                err = InsertWipKeyPart(item);
                if (!string.IsNullOrEmpty(err))
                    return err;
            }
            #endregion
            return "OK";        
        }

        /// <summary>
        /// 获取属于密码的字段
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetPwdColumns(string sntype)
        {            
            string table = "SFCB.B_PWD_COLUMNS";
            string fieldlist = "PWDNAME";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(sntype))
                mst.Add("PWDNAME", sntype);
          return dp.GetData(table, fieldlist, mst, out count);
        }
        /// <summary>
        /// 根据序号查询该序列号的内容
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public System.Data.DataSet GetSnInfo(string serial)
        {     
            string table = "SFCR.T_WIP_KEYPART_ONLINE";
            if (DB_Flag == 1)
                table = "SFCR.T_WIP_KEYPART";
            string fieldlist = "esn,woId,sntype,snval".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("SNVAL", serial);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        /// <summary>
        /// 插入数据(wipkeypart个体)
        /// </summary>
        /// <param name="wipkeyparts"></param>
        /// <returns></returns>
        public string InsertWipKeyPart(IDictionary<string,object> dic)
        {
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
                string table = "SFCR.T_WIP_KEYPART_ONLINE";
                if (DB_Flag == 1)
                    table = "SFCR.T_WIP_KEYPART";               

                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                dp.AddData(tx, table, dic);
              
                if ((dic["SNTYPE"].ToString().ToUpper() == "MAC") || (dic["SNTYPE"].ToString().ToUpper() == "IMEI") || (dic["SNTYPE"].ToString().ToUpper() == "SN"))
                {
                   
                    IDictionary<string, object> mst = new Dictionary<string, object>();
                    mst.Add("ESN", dic["ESN"]);
                    mst.Add(dic["SNTYPE"].ToString().ToUpper(), dic["SNVAL"]);
                    if (DB_Flag == 0)
                    dp.UpdateData(tx, "SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);
                    if (DB_Flag == 1)
                         dp.UpdateData(tx, "SFCR.T_WIP_TRACKING", new string[] { "ESN" }, mst);
                }
                tx.Commit();
                return string.Empty;
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
       
        /// <summary>
        /// 获取该工单或工单+线体的最大卡通箱号的内容
        /// </summary>
        /// <param name="woId">当前工单</param>
        /// <param name="lineId">当前线体,可以为空</param>
        /// <returns>返回dataset</returns>
     //   public System.Data.DataSet GetMaxBoxNumberBywoId(string woId, string lineId)
     //   {
            //string __line = string.Empty;
            //if (string.IsNullOrEmpty(woId))
            //    return null;
            //MySqlCommand cmd = new MySqlCommand();
            //if (!string.IsNullOrEmpty(lineId))
            //{
            //    __line = " and lineId=@lineId";
            //    cmd.Parameters.Add("lineId", MySqlDbType.VarChar, lineId.Length).Value = lineId;
            //}
            //cmd.CommandText = string.Format("select cartonId,lineId,woId,num,flag,cartonnumber FROM SFCR.T_CARTON_INFO_HAD  WHERE cartonId=(SELECT max(cartonId) from SFCR.T_CARTON_INFO_HAD where woId =@woId {0}", __line);
            //cmd.Parameters.Add("woId", MySqlDbType.VarChar, woId.Length).Value = woId;
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);         
            
        //}
        /// <summary>
        /// 获取工单的最大的卡通箱编号
        /// </summary>
        /// <param name="woId">工单号</param>
        /// <returns></returns>
        //public string GetMaxBoxNumber(string woId, string lineId, string partnumber)
        //{

        //    return BLL.BllMsSqllib.Instance.GetMaxCartonId(woId, lineId, partnumber);
        
        //}
        /// <summary>
        /// 增加卡通箱记录
        /// </summary>
        /// <param name="cartioninfo">卡通箱信息</param>
        /// <returns></returns>
        //public string InsertCartonInfo(Entity.tCartonInfo cartioninfo)
        //{
        //    return BLL.BllMsSqllib.Instance.InsertCartonInfo(cartioninfo);
        //}

        /// <summary>
        ///  产品过站和记录卡通箱
        /// </summary>
        /// <param name="cartonId">卡通箱编号</param>
        /// <param name="line">产线编号</param>
        /// <param name="mygroup">当前流程</param>
        /// <param name="esn">产品跟踪序列号</param>
        /// <param name="userid">当前用户</param>
        /// <param name="flag">状态</param>
        /// <returns></returns>
        //public string UpdateWipAndRecCartonBox(Entity.tWipTrackingTable wiptrack)//(string cartonId, string mcartionId, string palletnumber, string mpalletnumber, string trayno, string line, string mygroup, string esn, string userid, string flag)
        //{
        //    return Pro.PRO_UPDATEWIPANDRECCARTONBOX(wiptrack.line, wiptrack.locstation, wiptrack.ESN, wiptrack.userId, wiptrack.errflag, wiptrack.cartonnumber,
        //        wiptrack.mcartonnumbr, wiptrack.palletnumber, wiptrack.mpalletnumber, wiptrack.TrayNO);            
                
                
                
        //        ///.UpdateWipAndRecCartonBox (wiptrack);//(cartonId, mcartionId, palletnumber, mpalletnumber, trayno, line, mygroup, esn, userid, flag);
        //}

        /// <summary>
        /// 获取卡通箱编号的状态
        /// </summary>
        /// <param name="cartonId">卡通箱编号</param>
        /// <returns>
        /// 0：在包装
        /// 1：关闭(包装完成)
        /// 2：分解
        /// 3：合并
        ///</returns>
        public string GetCartonState(string cartonId)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select flag from SFCR.T_CARTON_INFO_HAD where cartonId=@cartonId";
            //cmd.Parameters.Add("cartonId", MySqlDbType.VarChar, cartonId.Length).Value = cartonId;
            //object obj = BLL.BllMsSqllib.Instance.sqlExecuteScalar(cmd);
            //if (obj == null || string.IsNullOrEmpty(obj.ToString()))
            //    return string.Empty;
            //return obj.ToString();

            string table = "SFCR.T_CARTON_INFO_HAD";
            string fieldlist = "FLAG";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("CARTONID", cartonId);
            DataTable dt= dp.GetData(table, fieldlist,  mst, out count).Tables[0];
            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            return dt.Rows[0][0].ToString();
        }

        /// <summary>
        /// 获取卡通箱需要打印的所有内容
        /// </summary>
        /// <param name="cartonId">卡通箱编号</param>
        /// <param name="Content">需要打印的内容</param>
        /// <returns></returns>
        public System.Data.DataSet GetCartonPrintContent(string cartonId, string[] Content)
        {
            string __content = string.Empty;
            foreach (string str in Content)
            {
                if (!string.IsNullOrEmpty(str))
                    __content += string.Format("'{0}',", str);
            }
            __content = __content.Substring(0, __content.Length - 1);
            __content = string.Format(" and k.sntype in({0})", __content);
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = string.Format("select k.woId,c.cartonId,d.cartonnumber,UPPER(k.esn) as esn,k.sntype,UPPER(k.snval) as snval from SFCR.T_WIP_KEYPART_ONLINE k,SFCR.T_CARTON_INFO_DTA c,SFCR.T_CARTON_INFO_HAD d where c.esn=k.esn and c.cartonId=d.cartonId and c.cartonId=@cartonId {0}", __content);
            //cmd.Parameters.Add("cartonId", MySqlDbType.VarChar, cartonId.Length).Value = cartonId;
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            int count = 0;
            string table = "SFCR.T_WIP_KEYPART_ONLINE k,SFCR.T_CARTON_INFO_DTA c,SFCR.T_CARTON_INFO_HAD d";
            if (DB_Flag == 1)
                 table = "SFCR.T_WIP_KEYPART k,SFCR.T_CARTON_INFO_DTA c,SFCR.T_CARTON_INFO_HAD d";
            string fieldlist = "k.woId,c.cartonId,d.cartonnumber,UPPER(k.esn) as esn,k.sntype,UPPER(k.snval) as snval";
            string filter = " c.esn=k.esn and c.cartonId=d.cartonId and c.cartonId={0} " + __content;          
         
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("cartonId", cartonId);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }

        /// <summary>
        /// 获取这条线没有包装完成的卡通箱列表
        /// </summary>
        /// <param name="lineId">产线编号</param>
        /// <returns></returns>
        public System.Data.DataSet GetNotCloseBoxInfo(string lineId)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select woId,lineId,cartonId,cartonnumber,num,flag from SFCR.T_CARTON_INFO_HAD where lineId=@lineId and flag='0'";
            //cmd.Parameters.Add("lineId", MySqlDbType.VarChar, 20).Value = lineId;
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCR.T_CARTON_INFO_HAD";
            string fieldlist = "woId,lineId,cartonId,cartonnumber,num,flag".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("LINEID", lineId);
            mst.Add("FLAG", "0");
           return dp.GetData(table, fieldlist,  mst, out count);
        }

        /// <summary>
        /// 获取一个工单在一条产线上的包装信息
        /// </summary>
        /// <param name="woId"></param>
        /// <param name="lineId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetPackCarton(string woId, string lineId)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "SELECT cartonId,lineId,woId,cartonnumber,num,flag from SFCR.T_CARTON_INFO_HAD where lineid=@lineid and woId=@woid";
            //cmd.Parameters.Add("lineid", MySqlDbType.VarChar, lineId.Length).Value = lineId;
            //cmd.Parameters.Add("woid", MySqlDbType.VarChar, woId.Length).Value = woId;
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            string table = "SFCR.T_CARTON_INFO_HAD";
            string fieldlist = "woId,lineId,cartonId,cartonnumber,num,flag".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("LINEID", lineId);
            mst.Add("WOID", woId);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        //public System.Data.DataSet GetCartonInfo(string cartonId)
        //{
        //    //返回一个卡通箱的详细信息
        //    return null;
        //}

        /// <summary>
        /// 获取整个工单的序列号的对应关系
        /// </summary>
        /// <param name="woId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetWoAllSerial(string woId, int strHour)
        {     
           
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = @"select b.cartonnumber,a.woId,a.esn,a.sntype,a.snval from sfcr.T_WIP_KEYPART_ONLINE a,sfcr.T_WIP_TRACKING_ONLINE b where a.woId=@sMO
//                                and a.woid=b.woid and a.esn=b.esn ";
//            cmd.Parameters.Add("sMO", MySqlDbType.VarChar).Value = woId;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            int count = 0;
            string table = "sfcr.T_WIP_KEYPART_ONLINE a,sfcr.T_WIP_TRACKING_ONLINE b";
            if (DB_Flag == 1)
                 table = "sfcr.T_WIP_KEYPART a,sfcr.T_WIP_TRACKING b";
            string fieldlist = "b.cartonnumber,a.woId,a.esn,a.sntype,a.snval";
            string filter = "a.woId={0} and a.woid=b.woid and a.esn=b.esn";           
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("woid", woId);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }

        /// <summary>
        /// 返回卡通箱的包装内容
        /// </summary>
        /// <param name="cartonId"></param>
        /// <returns></returns>
        //public System.Data.DataSet GetCartonContent(string cartonId)
        //{
    
        //    List<Entity.ProcedureKey> LsPdk = new List<Entity.ProcedureKey>();
        //    Entity.ProcedureKey Pdk = null;
        //    Pdk = new Entity.ProcedureKey();
        //    Pdk.Variable = "V_CARTONID";
        //    Pdk.Value = cartonId;
        //    LsPdk.Add(Pdk);

        //    return BLL.BllMsSqllib.Instance.PublicReurnDataSet("PRO_GETCARTONCONTENT",LsPdk,"RES");
        //}
        /// <summary>
        /// 检查内容是否存在list中
        /// </summary>
        /// <param name="str"></param>
        /// <param name="lsstr"></param>
        /// <returns></returns>
        private bool ChkList(string str, List<string> lsstr)
        {
            bool flag = false;
            foreach (string item in lsstr)
            {
                if (item.ToUpper() == str.ToUpper())
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        ///// <summary>
        ///// 新WIP方法
        ///// </summary>
        ///// <param name="woId"></param>
        ///// <param name="Part"></param>
        ///// <param name="Flag"></param>
        ///// <returns></returns>
        //public System.Data.DataSet CROSSTAB_WIP(string woId, string Part, int Flag)
        //{
        //  //  //return BLL.BllMsSqllib.Instance.CROSSTAB_WIP(woId, Part, Flag);
        //  //  List<Entity.ProcedureKey> LsPdk = new List<Entity.ProcedureKey>();
        //  //  Entity.ProcedureKey Pdk = null;
        //  //  Pdk = new Entity.ProcedureKey();
        //  //  Pdk.Variable = "V_WOID";
        //  //  Pdk.Value = woId;
        //  //  LsPdk.Add(Pdk);

        //  //  Pdk = new Entity.ProcedureKey();
        //  //  Pdk.Variable = "V_PART";
        //  //  Pdk.Value = Part;
        //  //  LsPdk.Add(Pdk);

        //  //  Pdk = new Entity.ProcedureKey();
        //  //  Pdk.Variable = "FLAG";
        //  //  Pdk.intValue = Flag;
        //  //  LsPdk.Add(Pdk);

        //  ////  throw new Exception(woId);
        //  //  return BLL.BllMsSqllib.Instance.PublicReurnDataSet("PRO_CROSSTAB_WIP", LsPdk, "RES");
        //    return null;
        //}
        /// <summary>
        /// 强制关闭卡通箱
        /// </summary>
        /// <param name="cartongId"></param>
        /// <returns></returns>
        public string CloseCartonBox(string cartongId)
        {         
            
                //List<MySqlCommand> LsCmd = new List<MySqlCommand>();
                //MySqlCommand cmd = null;
                //cmd = new MySqlCommand();
                //cmd.CommandText = "  UPDATE SFCR.T_CARTON_INFO_HAD SET FLAG='1' WHERE CARTONID=@V_CARTONID ";
                //cmd.Parameters.AddRange(new MySqlParameter[]
                //{
                //new MySqlParameter("V_CARTONID",MySqlDbType.VarChar){Value=cartongId}
                // });

                //LsCmd.Add(cmd);              
                //cmd = new MySqlCommand();
                //cmd.CommandText = "   UPDATE SFCR.T_PALLET_INFO SET CLOSEFLAG=1 WHERE PALLETNUMBER=@V_CARTONID ";
                //cmd.Parameters.AddRange(new MySqlParameter[]
                //{
                //new MySqlParameter("V_CARTONID",MySqlDbType.VarChar){Value=cartongId}
                //});
                //LsCmd.Add(cmd);
           
                //BLL.BllMsSqllib.Instance.ExecteNonQueryArr(LsCmd);
              string _StrErr = "OK";
                DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
                DbTransaction tx = ProviderHelper.BeginTransaction(conn);
                try
                {
                    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                    IDictionary<string, object> mst = new Dictionary<string, object>();
                    mst.Add("CLOSEFLAG", 1);
                    mst.Add("CARTONID", cartongId);
                    dp.UpdateData(tx, "SFCR.T_CARTON_INFO_HAD", new string[] { "CARTONID" }, mst);

                    mst = new Dictionary<string, object>();
                    mst.Add("FLAG", "1");
                    mst.Add("PALLETNUMBER", cartongId);
                    dp.UpdateData(tx, "SFCR.T_PALLET_INFO", new string[] { "CARTONID" }, mst);
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    _StrErr = ex.Message;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
                return _StrErr;
            



        }

        //public System.Data.DataSet GetCartonSnList(string CartonId,int Flag)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    if (Flag == 0)
        //    {
        //        cmd.CommandText = "select cartonId,esn from SFCR.T_CARTON_INFO_DTA where cartonId=@cartonid";
        //    }
        //    if (Flag == 1)
        //    {
        //        cmd.CommandText = " select b.esn,b.sntype,b.snval from SFCR.T_CARTON_INFO_DTA a,SFCR.T_WIP_KEYPART_ONLINE b where a.cartonId=@cartonid  and a.esn=b.esn";
        //    }
        //    cmd.Parameters.AddRange(new MySqlParameter[]
        //    {
        //        new MySqlParameter("cartonid",MySqlDbType.VarChar){Value=CartonId}
        //    });

        //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}

        //public System.Data.DataSet GetSnTestMachineInfo(string esn)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText="select ESN,woId,GROUP_NAME,MACINE_NO,TOOL_NO,TEST_FLAG,EMP_NO,IN_STATION_TIME from SFCR.T_ATE_MACHINE_LOG where ESN=@esn";
        //    cmd.Parameters.AddRange(new MySqlParameter[]
        //    {
        //        new MySqlParameter("esn",MySqlDbType.VarChar){Value=esn}
        //    });

        //     return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}

        public void UpdateFirstCarton(string newcarton)
        {
                string[] boxnum = newcarton.ToUpper().Split('C');
                string ctnsn = boxnum[1];
                string ctnno = boxnum[0] + "C0001";
                //List<MySqlCommand> LsCmd = new List<MySqlCommand>();
             
                //MySqlCommand cmd = new MySqlCommand();
                //cmd.CommandText = "update SFCR.T_CARTON_INFO_HAD set cartonId=@NEWCTN,cartonnumber=@CTNSN where cartonId=@CTNNO ";                 
                //cmd.Parameters.Add("NEWCTN", MySqlDbType.VarChar, 20).Value = newcarton;
                //cmd.Parameters.Add("CTNSN", MySqlDbType.VarChar, 10).Value = ctnsn;
                //cmd.Parameters.Add("CTNNO", MySqlDbType.VarChar, 20).Value = ctnno;
                //LsCmd.Add(cmd);

                //cmd = new MySqlCommand();
                //cmd.CommandText=" update SFCR.T_PALLET_INFO set palletnumber=@NEWCTN where palletnumber=@CTNNO";
                //cmd.Parameters.Add("NEWCTN", MySqlDbType.VarChar, 20).Value = newcarton;
                //cmd.Parameters.Add("CTNNO", MySqlDbType.VarChar, 20).Value = ctnno;
                //LsCmd.Add(cmd);
                //BLL.BllMsSqllib.Instance.ExecteNonQueryArr(LsCmd);

             
                DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
                DbTransaction tx = ProviderHelper.BeginTransaction(conn);
                try
                {
                    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                    StringBuilder ofilter = new StringBuilder();
                    ofilter.Append(" CARTONID={0},CARTONNUMBER={1}");            
                    IDictionary<string, object> modFields = new Dictionary<string, object>();
                    modFields.Add("CARTONID", newcarton);
                    modFields.Add("CARTONNUMBER", ctnsn);
                    string filter = "CARTONID = {0}";
                    IDictionary<string, object> keyVals = new Dictionary<string, object>();
                    keyVals.Add("CARTONID", ctnno);
                    dp.UpdateBatchData(tx,"SFCR.T_CARTON_INFO_HAD", ofilter.ToString(), modFields, filter, keyVals);


                    ofilter = new StringBuilder();
                    ofilter.Append("PALLETNUMBER={0}");
                     modFields = new Dictionary<string, object>();
                     modFields.Add("PALLETNUMBER", newcarton);
                     filter = "PALLETNUMBER = {0}";
                     keyVals = new Dictionary<string, object>();
                     keyVals.Add("PALLETNUMBER", ctnno);
                     dp.UpdateBatchData(tx, "SFCR.T_PALLET_INFO", ofilter.ToString(), modFields, filter, keyVals);

                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
           
        }
        public string UpdateWipTrackingWeight(string esn,string weight)
        {
            try
            {
            //    MySqlCommand cmd = new MySqlCommand();
            //    cmd.CommandText = "update sfcr.T_WIP_TRACKING_ONLINE set weightqty=@weight where esn=@sn ";
            //    cmd.Parameters.AddRange(new MySqlParameter[]
            //{
            //    new MySqlParameter("sn",MySqlDbType.VarChar){Value=esn},
            //    new MySqlParameter("weight",MySqlDbType.VarChar){Value=weight},
            //});
            //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
            //    return "OK";
                string table = "SFCR.T_WIP_TRACKING_ONLINE";
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("ESN", esn);
                mst.Add("weightqty".ToUpper(), weight);
                if (DB_Flag == 1)
                    table = "SFCR.T_WIP_TRACKING";
                dp.UpdateData(table, new string[] { "ESN" }, mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
       public System.Data.DataSet GetMinCartonByWoid(string woid)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select MIN(cartonnumber) as mincarton from sfcr.t_carton_info_had where woId=@sMO";
            //cmd.Parameters.Add("sMO", MySqlDbType.VarChar, 20).Value = woid;
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            string table = "sfcr.t_carton_info_had".ToUpper();
            string fieldlist = "MIN(cartonnumber) as mincarton".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woid);
            return dp.GetData(table, fieldlist, mst, out count);
        }

       public string Update_Wip_Tracking(string dicstring)
       {           
           //try
           //{
           //    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           //    IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);         
           //    string table = "SFCR.T_WIP_TRACKING_ONLINE";
           //    if (DB_Flag == 1)
           //        table = "SFCR.T_WIP_TRACKING";
           //    dp.UpdateData(table, new string[] { "ESN" }, mst);
           //    return "OK";
           //}
           //catch (Exception ex)
           //{
           //    return ex.Message;
           //}
           IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);  
           List<string> Fields = new List<string>();
           Fields.Add("ESN");
           return Update_WIP_TRACKING(mst, Fields);
       }


       /// <summary>
       /// 更新QA编号
       /// </summary>
       /// <param name="QA"></param>
       /// <param name="UpType"></param>
       /// <returns></returns>
       public string Update_QA(string QAInfo, string UpType, string QAUnit)
       {
           string STATUS = "OK";
           try
           {             

               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               IDictionary<string, object> mst = new Dictionary<string, object>();
               mst.Add("QA_NO", QAInfo);
               mst.Add("QA_RESULT", "P");
               string Colnum = "NA";
               switch (UpType)
               {
                   case "Tray盘号":
                       Colnum = "TRAYNO";
                       break;
                   case "产品箱号":                  
                       Colnum = "CARTONNUMBER";
                       break;
                   case "栈板号":                        
                       Colnum = "PALLETNUMBER";
                       break;
                   default:
                       break;
               }
               mst.Add(Colnum, QAUnit);

               string table = "SFCR.T_WIP_TRACKING_ONLINE";
               if (DB_Flag == 1)
                   table = "SFCR.T_WIP_TRACKING";
               dp.UpdateData(table, new string[] { Colnum }, mst);
           }
           catch (Exception EX)
           {
               STATUS = EX.Message;
           }
           return STATUS;
       }


       public string RollBack_Station(string ESN, string Station)
       {
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("WIPSTATION", Station);
           mst.Add("NEXTSTATION", Station);
           mst.Add("ESN", ESN);
           List<string> Fields = new List<string>();
           Fields.Add("ESN");
           return Update_WIP_TRACKING(mst, Fields);
       }
       public void check_wip_tracking(string Dic_Wip, string lsdic_key)
       {
               int count = 0;
               string table = "sfcr.t_wip_tracking_online";
               if (DB_Flag == 1)
                   table = "SFCR.T_WIP_TRACKING";
               string fieldlist = "ESN";
               string filter = "ESN={0}";
               IDictionary<string, object> dicEsn = MapListConverter.JsonToDictionary(Dic_Wip);
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               if(dicEsn.Count>0)
               {
                   IDictionary<string, object> mst = new Dictionary<string, object>();
                   mst.Add("ESN", dicEsn["esn"]);
                   DataSet ds = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

                   if (ds.Tables[0].Rows.Count == 0)
                   {
                        dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                       IDictionary<string, object> Dic_t_wip = MapListConverter.JsonToDictionary(Dic_Wip);
                       dp.AddData(table, Dic_t_wip);
                   }

                   table = "SFCR.Z_WHS_TRACKING";
                   fieldlist = "ESN";
                   filter = "ESN={0}";
                   mst = new Dictionary<string, object>();
                   mst.Add("ESN", dicEsn["esn"]);
                   DataSet ds_z_wip = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
                   if (ds_z_wip.Tables[0].Rows.Count == 0)
                   {
                       dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                       IDictionary<string, object> Dic_z_wip = MapListConverter.JsonToDictionary(Dic_Wip);
                       Dic_z_wip.Add("status", "9");
                       dp.AddData("sfcr.Z_WHS_TRACKING", Dic_z_wip);
                   }


                   table = "SFCR.t_wip_keypart_online";
                   if (DB_Flag == 1)
                       table = "SFCR.T_WIP_KEYPART";
                   fieldlist = "ESN";
                   filter = "ESN={0}";
                   mst = new Dictionary<string, object>();
                   mst.Add("ESN", dicEsn["esn"]);
                   DataSet ds_key = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
                   if (ds_key.Tables[0].Rows.Count < 1)
                   {
                       dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                       IList<IDictionary<string, object>> LsDic_t_key = MapListConverter.JsonToListDictionary(lsdic_key);
                       dp.AddListData("SFCR.t_wip_keypart_online", LsDic_t_key);
                   }

                   table = "SFCR.Z_WHS_KEYPART";
                   fieldlist = "ESN";
                   filter = "ESN={0}";
                   mst = new Dictionary<string, object>();
                   mst.Add("ESN", dicEsn["esn"]);
                   DataSet ds_z_key = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
                   if (ds_z_key.Tables[0].Rows.Count < 1)
                   {
                       dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                       IList<IDictionary<string, object>> LsDic_t_key = MapListConverter.JsonToListDictionary(lsdic_key);
                       dp.AddListData("SFCR.Z_WHS_KEYPART", LsDic_t_key);
                   }
               }
       }
       public string UpdateWipTrackingstatus(string esn, string userid, string lotout)
       {
           try
           {
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               IDictionary<string, object> mst = new Dictionary<string, object>();
               mst.Add("ESN", esn);
               mst.Add("USERID", userid);
               mst.Add("LOTOUT", lotout);
               mst.Add("STATUS", "9");
               mst.Add("RECDATE1", System.DateTime.Now);
               dp.UpdateData("SFCR.Z_WHS_TRACKING", new string[] { "ESN" }, mst);
               return "OK";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }

       public string Update_WIP_TRACKING(IDictionary<string, object> mst ,List<string> ListFields )
       {
           try
           {
               string table = "SFCR.T_WIP_TRACKING_ONLINE";
               if (DB_Flag == 1)
                   table = "SFCR.T_WIP_TRACKING";
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);             
               dp.UpdateData(table, ListFields.ToArray(), mst);
               return "OK";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }

       public  DataSet  Query_WIP_Tracking(IDictionary<string, object> dic)
       {
           string Param = string.Empty;
           int y = -1;
           string filter = null;
           int count;
           Dictionary<string, object> mst = new Dictionary<string, object>();
           if (dic.ContainsKey("WOID"))
           {
               List<string> List_woId = (List<string>)dic["WOID"];
               Param = string.Empty;
               for (int x = 0; x < List_woId.Count; x++)
               {
                   y++;
                   Param += ("{" + y.ToString() + "},");
                   mst.Add("WOID" + y.ToString(), List_woId[x]);
               }
               filter += string.Format(" WOID IN ({0}) AND ", Param.Substring(0, Param.Length - 1));
           }
           if (dic.ContainsKey("CARTONNUMBER"))
           {
               List<string> List_Carton = (List<string>)dic["CARTONNUMBER"];
               Param = string.Empty;
               for (int x = 0; x < List_Carton.Count; x++)
               {
                   y++;
                   Param += ("{" + y.ToString() + "},");
                   mst.Add("CARTONNUMBER" + y.ToString(), List_Carton[x]);
               }
               filter += string.Format(" CARTONNUMBER IN ({0}) AND ", Param.Substring(0, Param.Length - 1));
           }
           if (dic.ContainsKey("PALLETNUMBER"))
           {
               List<string> List_Pallet = (List<string>)dic["PALLETNUMBER"];
               Param = string.Empty;
               for (int x = 0; x < List_Pallet.Count; x++)
               {
                   y++;
                   Param += ("{" + y.ToString() + "},");
                   mst.Add("PALLETNUMBER" + y.ToString(), List_Pallet[x]);
               }
               filter += string.Format(" PALLETNUMBER IN ({0}) AND ", Param.Substring(0, Param.Length - 1));
           }
           if (dic.ContainsKey("WIPSTATION"))
           {
               List<string> List_WipStation = (List<string>)dic["WIPSTATION"];
               Param = string.Empty;
               for (int x = 0; x < List_WipStation.Count; x++)
               {
                   y++;
                   Param += ("{" + y.ToString() + "},");
                   mst.Add("WIPSTATION" + y.ToString(), List_WipStation[x]);
               }
               filter += string.Format(" WIPSTATION IN ({0}) AND ", Param.Substring(0, Param.Length - 1));
           }
           if (dic.ContainsKey("ESN"))
           {
               List<string> List_Esn = (List<string>)dic["ESN"];
               Param = string.Empty;
               for (int x = 0; x < List_Esn.Count; x++)
               {
                   y++;
                   Param += ("{" + y.ToString() + "},");
                   mst.Add("ESN" + y.ToString(), List_Esn[x]);
               }
               filter += string.Format(" ESN IN ({0}) AND ", Param.Substring(0, Param.Length - 1));
           }
           if (dic.ContainsKey("REWORKNO"))
           {
               Param = string.Empty;
             
                   y++;
                   Param += ("{" + y.ToString() + "},");
                   mst.Add("REWORKNO" + y.ToString(), dic["REWORKNO"]);

                   filter += string.Format(" REWORKNO ={0} AND ", Param.Substring(0, Param.Length - 1));
           }
           if (dic.ContainsKey("QA_NO"))
           {
               Param = string.Empty;
               y++;
               Param += ("{" + y.ToString() + "},");
               mst.Add("QA_NO" + y.ToString(), dic["QA_NO"]);

               filter += string.Format(" QA_NO ={0} AND ", Param.Substring(0, Param.Length - 1));
           }

           filter += " ERRFLAG='0' AND SCRAPFLAG='0' AND STORENUMBER='NA' ";

           string fieldlist = "ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE," +
                     "ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,LINE,SECTIONNAME,ROUTGROUPID," +
                     "STORENUMBER,WEIGHTQTY,QA_NO,QA_RESULT,TRACK_NO,ATE_STATION_NO,IN_LINE_TIME,BOMNUMBER,REWORKNO";
           return TransactionManager.GetData( "SFCR.T_WIP_TRACKING_ONLINE", fieldlist, filter.ToUpper(), mst, null, null, out count);

       }

       public  DataSet GetStockInPrint(string StockIn)
       {
           int count = 0;
           string table = "SFCR.T_WIP_TRACKING_ONLINE a";
           string fieldlist = "a.woId, a.partnumber, a.wipstation, a.productname, count(a.esn) qty";
           string filter = "a.storenumber = {0} ";
           string group = "a.woId, a.partnumber, a.wipstation, a.productname";
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("storenumber", StockIn);
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, group, out count);

       }

    }
}
