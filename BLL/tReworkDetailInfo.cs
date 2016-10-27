using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SystemObject;
using GenericProvider;
using GenericUtil;
using System.Data.Common;
using SrvComponent;
using MySql.Data.MySqlClient;

namespace BLL
{
    public class tReworkDetailInfo
    {
        public tReworkDetailInfo()
        {
        }

        public string GetReworkNo(string UserId)
        {
            try
            {
                string reworkno = "";
                int count = 0;
                string table = "sfcr.t_rework_detail_info";
                string fieldlist = "max(reworkno)";
                string filter = "reworkno like {0}";

                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("reworkno", UserId + DateTime.Now.ToString("yyyyMMdd") + "%");
                DataSet ds = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

                reworkno = UserId + DateTime.Now.ToString("yyyyMMdd") + "0001";

                string rework = ds.Tables[0].Rows[0][0].ToString();
                if (!string.IsNullOrEmpty(rework))
                    reworkno = UserId + DateTime.Now.ToString("yyyyMMdd") + (Convert.ToInt32((rework.Substring(rework.Length - 4, 4))) + 1).ToString().PadLeft(4, '0');

                return reworkno.ToUpper();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string InsertReworkDetail(string dicstring)
        {
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
                dp.AddData("SFCR.T_REWORK_DETAIL_INFO", mst);
                return "OK";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
//        /// <summary>
//        /// 未入库重工使用
//        /// </summary>
//        /// <param name="ClearWip"></param>
//        /// <param name="ESN"></param>
//        /// <param name="LocGroup"></param>
//        /// <param name="WipGroup"></param>
//        /// <param name="ReworkNo"></param>
//        /// <returns></returns>
//        public string UpdateDataForRework(List<string> ClearWip, List<string> Listsn, string LocGroup, string WipGroup, string ReworkNo, int Total)
//        {
//            string Msg = "";
//            string ESN = "";
//            if (Listsn.Count != Total)
//            {
//                return string.Format("传入资料异常,传入ESN数量{0}个,实际应该是{1}", Listsn.Count.ToString(), Total.ToString());
//            }
//            else
//            {
//                for (int xi = 0; xi < Listsn.Count; xi++)
//                {
//                    try
//                    {
//                        ESN = Listsn[xi].ToString();

//                        List<string> WipKeyparts = new List<string>();
//                        MySqlCommand[] cmd = new MySqlCommand[100];
//                        List<MySqlCommand> lsCmd = new List<MySqlCommand>();
//                        string ColnumWip = "";
//                        int y = 0;

//                        for (int i = 0; i < ClearWip.Count; i++)
//                        {
//                            string str = ClearWip[i].ToString();
//                            int x = str.IndexOf('-');
//                            string Flag = str.Substring(0, x);
//                            string Colnum = str.Substring(x + 1, str.Length - x - 1);
//                            if (Flag == "0")
//                            {
//                                ColnumWip += string.Format("{0}='NA',", Colnum);
//                            }

//                            if (Flag == "1")
//                            {
//                                WipKeyparts.Add(string.Format("sntype='{0}'", Colnum));
//                            }
//                        }
//                        ColnumWip = ColnumWip + "Locstation=@loc,wipstation=@wip,";
//                        ColnumWip = ColnumWip.Substring(0, ColnumWip.Length - 1);

//                        string colnum = @"esn,woid,partnumber,productname,versioncode,type,locstation,stationname,wipstation,nextstation,userid,recdate,errflag,
//                                    scrapflag,sn,mac,imei,cartonnumber,trayno,palletnumber,mcartonnumber,mpalletnumber,line,sectionname,routgroupid,storenumber,
//                                    weightqty,qa_no,qa_result,track_no,ate_station_no,in_line_time,bomnumber";

//                        cmd[y] = new MySqlCommand();
//                        cmd[y].CommandText = string.Format("insert into SFCR.T_WIP_UNDO ({0},reworkno) select {0},@rewno from sfcr.T_WIP_TRACKING_ONLINE where esn=@esn", colnum);
//                        cmd[y].Parameters.Add("rewno", MySqlDbType.VarChar, ReworkNo.Length).Value = ReworkNo;
//                        cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                        lsCmd.Add(cmd[y]);

//                        y++;
//                        cmd[y] = new MySqlCommand();
//                        cmd[y].CommandText = "Update sfcr.T_WIP_TRACKING_ONLINE set reworkno=@rewno,nextstation=@next," + ColnumWip + " where esn=@esn";
//                        cmd[y].Parameters.Add("rewno", MySqlDbType.VarChar, ReworkNo.Length).Value = ReworkNo;
//                        cmd[y].Parameters.Add("next", MySqlDbType.VarChar, WipGroup.Length).Value = WipGroup;
//                        cmd[y].Parameters.Add("loc", MySqlDbType.VarChar, LocGroup.Length).Value = LocGroup;
//                        cmd[y].Parameters.Add("wip", MySqlDbType.VarChar, WipGroup.Length).Value = WipGroup;
//                        cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;

//                        lsCmd.Add(cmd[y]);

//                        if (WipKeyparts.Count != 0)
//                        {
//                            for (int x = 0; x < WipKeyparts.Count; x++)
//                            {
//                                string colnumKeyPart = "esn,woId,sntype,snval,station,kpno,recdate";
//                                y++;
//                                cmd[y] = new MySqlCommand();
//                                cmd[y].CommandText = string.Format("insert into SFCR.T_WIP_KEYPART_UNDO ({1}) select {1} from SFCR.T_WIP_KEYPART_ONLINE  where esn=@esn and {0}", WipKeyparts[x].ToString(), colnumKeyPart);
//                                cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                                lsCmd.Add(cmd[y]);

//                                y++;
//                                cmd[y] = new MySqlCommand();
//                                cmd[y].CommandText = string.Format("Delete FROM  SFCR.T_WIP_KEYPART_ONLINE where esn=@esn and {0}", WipKeyparts[x].ToString());
//                                cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                                lsCmd.Add(cmd[y]);

//                                y++;
//                                cmd[y] = new MySqlCommand();
//                                cmd[y].CommandText = string.Format("update SFCR.T_WO_SNLIST set snval='0' where snval=(select  snval from SFCR.T_WIP_KEYPART_ONLINE where esn=@esn and {0} and rownum=1)", WipKeyparts[x].ToString());
//                                cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                                lsCmd.Add(cmd[y]);

//                            }
//                        }

//                        if (ColnumWip.ToUpper().IndexOf("CARTONNUMBER") > -1)
//                        {
//                            y++;
//                            cmd[y] = new MySqlCommand();
//                            cmd[y].CommandText = "update SFCR.T_CARTON_INFO_HAD set num=num-1,flag='0' where cartonId=(select cartonid from SFCR.T_CARTON_INFO_DTA where esn=@esn and rownum=1)";
//                            cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                            lsCmd.Add(cmd[y]);

//                            y++;
//                            cmd[y] = new MySqlCommand();
//                            cmd[y].CommandText = "Delete FROM SFCR.T_CARTON_INFO_DTA where esn=@esn";
//                            cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                            lsCmd.Add(cmd[y]);
//                        }

//                        //try
//                        //{
//                        BLL.BllMsSqllib.Instance.ExecteNonQueryArr(lsCmd);
//                        Msg = "OK";
//                    }
//                    catch (Exception ex)
//                    {
//                        Msg = ex.Message + "ESN:" + ESN;

//                    }

//                }

//                return Msg;
//            }
//        }


//        /// <summary>
//        /// 未入库重工使用------单个产品一过
//        /// </summary>
//        /// <param name="ClearWip"></param>
//        /// <param name="Listsn"></param>
//        /// <param name="LocGroup"></param>
//        /// <param name="WipGroup"></param>
//        /// <param name="ReworkNo"></param>
//        /// <returns></returns>
//        public string UpdateDataForReworkSigle(List<string> ClearWip, string Listsn, string LocGroup, string WipGroup, string ReworkNo)
//        {
//            string Msg = "";
//            string ESN = "";         
//            try
//            {
//                ESN = Listsn;

//                List<string> WipKeyparts = new List<string>();
//                MySqlCommand[] cmd = new MySqlCommand[100];
//                List<MySqlCommand> lsCmd = new List<MySqlCommand>();
//                string ColnumWip = "";
//                int y = 0;

//                for (int i = 0; i < ClearWip.Count; i++)
//                {
//                    string str = ClearWip[i].ToString();
//                    int x = str.IndexOf('-');
//                    string Flag = str.Substring(0, x);
//                    string Colnum = str.Substring(x + 1, str.Length - x - 1);
//                    if (Flag == "0")
//                    {
//                        ColnumWip += string.Format("{0}='NA',", Colnum);
//                    }

//                    if (Flag == "1")
//                    {
//                        WipKeyparts.Add(string.Format("sntype='{0}'", Colnum));
//                    }
//                }
//                ColnumWip = ColnumWip + "Locstation=@loc,wipstation=@wip,";
//                ColnumWip = ColnumWip.Substring(0, ColnumWip.Length - 1);

//                string colnum = @"esn,woid,partnumber,productname,versioncode,type,locstation,stationname,wipstation,nextstation,userid,recdate,errflag,
//                                    scrapflag,sn,mac,imei,cartonnumber,trayno,palletnumber,mcartonnumber,mpalletnumber,line,sectionname,routgroupid,storenumber,
//                                    weightqty,qa_no,qa_result,track_no,ate_station_no,in_line_time,bomnumber";

//                cmd[y] = new MySqlCommand();
//                cmd[y].CommandText = string.Format("insert into SFCR.T_WIP_UNDO ({0},reworkno) select {0},@rewno from sfcr.T_WIP_TRACKING_ONLINE where esn=@esn", colnum);
//                cmd[y].Parameters.Add("rewno", MySqlDbType.VarChar, ReworkNo.Length).Value = ReworkNo;
//                cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                lsCmd.Add(cmd[y]);

//                y++;
//                cmd[y] = new MySqlCommand();
//                cmd[y].CommandText = "Update sfcr.T_WIP_TRACKING_ONLINE set reworkno=@rewno,nextstation=@next," + ColnumWip + " where esn=@esn";
//                cmd[y].Parameters.Add("rewno", MySqlDbType.VarChar, ReworkNo.Length).Value = ReworkNo;
//                cmd[y].Parameters.Add("next", MySqlDbType.VarChar, WipGroup.Length).Value = WipGroup;
//                cmd[y].Parameters.Add("loc", MySqlDbType.VarChar, LocGroup.Length).Value = LocGroup;
//                cmd[y].Parameters.Add("wip", MySqlDbType.VarChar, WipGroup.Length).Value = WipGroup;
//                cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;

//                lsCmd.Add(cmd[y]);

//                if (WipKeyparts.Count != 0)
//                {
//                    for (int x = 0; x < WipKeyparts.Count; x++)
//                    {
//                        string colnumKeyPart = "esn,woId,sntype,snval,station,kpno,recdate";
//                        y++;
//                        cmd[y] = new MySqlCommand();
//                        cmd[y].CommandText = string.Format("insert into SFCR.T_WIP_KEYPART_UNDO ({1}) select {1} from SFCR.T_WIP_KEYPART_ONLINE  where esn=@esn and {0}", WipKeyparts[x].ToString(), colnumKeyPart);
//                        cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                        lsCmd.Add(cmd[y]);

//                        y++;
//                        cmd[y] = new MySqlCommand();
//                        cmd[y].CommandText = string.Format("DELETE FROM   SFCR.T_WIP_KEYPART_ONLINE where esn=@esn and {0}", WipKeyparts[x].ToString());
//                        cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                        lsCmd.Add(cmd[y]);

//                        y++;
//                        cmd[y] = new MySqlCommand();
//                        cmd[y].CommandText = string.Format("update SFCR.T_WO_SNLIST set snval='0' where snval=(select  snval from SFCR.T_WIP_KEYPART_ONLINE where esn=@esn and {0} and rownum=1)", WipKeyparts[x].ToString());
//                        cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                        lsCmd.Add(cmd[y]);

//                    }
//                }

//                if (ColnumWip.ToUpper().IndexOf("CARTONNUMBER") > -1)
//                {
//                    y++;
//                    cmd[y] = new MySqlCommand();
//                    cmd[y].CommandText = "update SFCR.T_CARTON_INFO_HAD set num=num-1,flag='0' where cartonId=(select cartonid from SFCR.T_CARTON_INFO_DTA where esn=@esn and rownum=1)";
//                    cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                    lsCmd.Add(cmd[y]);

//                    y++;
//                    cmd[y] = new MySqlCommand();
//                    cmd[y].CommandText = "DELETE FROM  SFCR.T_CARTON_INFO_DTA where esn=@esn";
//                    cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                    lsCmd.Add(cmd[y]);


//                }

//                //try
//                //{
//                ExecteNonQueryArr(lsCmd);
//                Msg = "OK";
//            }
//            catch (Exception ex)
//            {
//                Msg = ex.Message + "ESN:" + ESN;

//            }

//            // }

//            return Msg;
//            //}
//        }
        //public void ExecteNonQueryArr(List<MySqlCommand> cmd)
        //{

        //    using (MySqlConnection cn = new MySqlConnection(ProConfiguration.GetConfig().DatabaseConnect))
        //    {
        //        cn.Open();
        //        MySqlTransaction sTran = cn.BeginTransaction();
        //        try
        //        {
        //            foreach (MySqlCommand _cmd in cmd)
        //            {                 
        //                _cmd.Connection = cn;
        //                _cmd.Transaction = sTran;
        //                _cmd.CommandTimeout = 84100;
        //                _cmd.ExecuteNonQuery();
        //                _cmd.Dispose();
        //            }
        //            sTran.Commit();
                 
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


//        /// <summary>
//        /// 已经入库使用
//        /// </summary>
//        /// <param name="ClearWip"></param>
//        /// <param name="ESN"></param>
//        /// <param name="LocGroup"></param>
//        /// <param name="WipGroup"></param>
//        /// <param name="ReworkNo"></param>
//        /// <returns></returns>
//        public string UpdateDataForReworkWareHouse(List<string> ClearWip, List<string> Listsn, string LocGroup, string WipGroup, string ReworkNo, int Total)
//        {
//            string Msg = "";
//            if (Listsn.Count != Total)
//            {
//                return string.Format("传入资料异常,传入ESN数量{0}个,实际应该是{1}", Listsn.Count.ToString(), Total.ToString());
//            }
//            else
//            {
//                for (int xi = 0; xi < Listsn.Count; xi++)
//                {
//                    string ESN = Listsn[xi].ToString();

//                    List<string> WipKeyparts = new List<string>();
//                    MySqlCommand[] cmd = new MySqlCommand[100];
//                    List<MySqlCommand> lsCmd = new List<MySqlCommand>();
//                    string ColnumWip = "";
//                    int y = 0;

//                    for (int i = 0; i < ClearWip.Count; i++)
//                    {
//                        string str = ClearWip[i].ToString();
//                        int x = str.IndexOf('-');
//                        string Flag = str.Substring(0, x);
//                        string Colnum = str.Substring(x + 1, str.Length - x - 1);
//                        if (Flag == "0")
//                        {
//                            ColnumWip += string.Format("{0}='NA',", Colnum);
//                        }

//                        if (Flag == "1")
//                        {
//                            WipKeyparts.Add(string.Format("sntype='{0}'", Colnum));
//                        }
//                    }
//                    ColnumWip = ColnumWip + "Locstation=@loc,wipstation=@wip,";
//                    ColnumWip = ColnumWip.Substring(0, ColnumWip.Length - 1);


//                    string colnum = @"esn,woid,partnumber,productname,versioncode,type,locstation,stationname,wipstation,nextstation,userid,recdate,errflag,
//                                    scrapflag,sn,mac,imei,cartonnumber,trayno,palletnumber,mcartonnumber,mpalletnumber,line,sectionname,routgroupid,storenumber,
//                                    weightqty,qa_no,qa_result,track_no,ate_station_no,in_line_time,bomnumber";

//                    cmd[y] = new MySqlCommand();
//                    cmd[y].CommandText = string.Format("insert into SFCR.T_WIP_UNDO ({0},@rewno) select {0},@rewno from sfcr.T_WIP_TRACKING_ONLINE  where esn=@esn", colnum);
//                    cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                    cmd[y].Parameters.Add("rewno", MySqlDbType.VarChar, ReworkNo.Length).Value = ReworkNo;
//                    lsCmd.Add(cmd[y]);

//                    y++;
//                    cmd[y] = new MySqlCommand();
//                    cmd[y].CommandText = "Update sfcr.T_WIP_TRACKING_ONLINE set nextstation=@nextwip,reworkno=@rewno," + ColnumWip + " where esn=@esn";
//                    cmd[y].Parameters.Add("rewno", MySqlDbType.VarChar, ReworkNo.Length).Value = ReworkNo;
//                    cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                    cmd[y].Parameters.Add("loc", MySqlDbType.VarChar, LocGroup.Length).Value = LocGroup;
//                    cmd[y].Parameters.Add("wip", MySqlDbType.VarChar, WipGroup.Length).Value = WipGroup;
//                    cmd[y].Parameters.Add("nextwip", MySqlDbType.VarChar, WipGroup.Length).Value = WipGroup;
//                    lsCmd.Add(cmd[y]);

//                    y++;
//                    string whcolnum = @"esn,woid,partnumber,productname,versioncode,type,locstation,stationname,wipstation,nextstation,userid,recdate,errflag,
//                                    scrapflag,sn,mac,imei,cartonnumber,trayno,palletnumber,mcartonnumber,mpalletnumber,line,sectionname,routgroupid,storenumber,
//                                    weightqty,qa_no,qa_result,track_no,ate_station_no,in_line_time,bomnumber,lotin,storehouseid,locid,lotout,recdate1,status";
//                    cmd[y] = new MySqlCommand();
//                    cmd[y].CommandText = string.Format(" insert into sfcr.z_whs_tracking_undo ({0},reworkno) select {0},@rewno sfcr.Z_WHS_TRACKING  where esn=@esn", whcolnum);
//                    cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                    cmd[y].Parameters.Add("rewno", MySqlDbType.VarChar, ReworkNo.Length).Value = ReworkNo;
//                    lsCmd.Add(cmd[y]);

//                    y++;
//                    cmd[y] = new MySqlCommand();
//                    cmd[y].CommandText = "DELETE FROM  sfcr.Z_WHS_TRACKING  where esn=@esn";
//                    cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                    lsCmd.Add(cmd[y]);

//                    if (WipKeyparts.Count != 0)
//                    {
//                        for (int x = 0; x < WipKeyparts.Count; x++)
//                        {
//                            y++;
//                            string colnumKeyPart = "esn,woId,sntype,snval,station,kpno,recdate";
//                            cmd[y] = new MySqlCommand();
//                            cmd[y].CommandText = string.Format("insert into SFCR.T_WIP_KEYPART_UNDO ({1}) select {1} from SFCR.T_WIP_KEYPART_ONLINE where esn=@SN and {0}", WipKeyparts[x].ToString(), colnumKeyPart);
//                            cmd[y].Parameters.Add("SN", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                            lsCmd.Add(cmd[y]);

//                            y++;
//                            cmd[y] = new MySqlCommand();
//                            cmd[y].CommandText = string.Format("DELETE FROM   SFCR.T_WIP_KEYPART_ONLINE where esn=@esn and {0}", WipKeyparts[x].ToString());
//                            cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                            lsCmd.Add(cmd[y]);

//                        }
//                    }

//                    y++;
//                    cmd[y] = new MySqlCommand();
//                    cmd[y].CommandText = string.Format("insert into SFCR.z_whs_keypart_undo (esn,woId,sntype,snval,station,kpno,recdate) select  esn,woId,sntype,snval,station,kpno,recdate from SFCR.Z_WHS_KEYPART  where esn=@esn ");
//                    cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                    // cmd[y].Parameters.Add("rewno", MySqlDbType.VarChar, ReworkNo.Length).Value = ReworkNo;
//                    lsCmd.Add(cmd[y]);

//                    y++;
//                    cmd[y] = new MySqlCommand();
//                    cmd[y].CommandText = string.Format("DELETE FROM   SFCR.Z_WHS_KEYPART where esn=@esn ");
//                    cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                    lsCmd.Add(cmd[y]);

//                    if (ColnumWip.ToUpper().IndexOf("CARTONNUMBER") > -1)
//                    {
//                        y++;
//                        cmd[y] = new MySqlCommand();
//                        cmd[y].CommandText = "update SFCR.T_CARTON_INFO_HAD set num=num-1 where cartonId=(select cartonid from tCartonInfodta where esn=@esn)";
//                        cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                        lsCmd.Add(cmd[y]);

//                        y++;
//                        cmd[y] = new MySqlCommand();
//                        cmd[y].CommandText = "DELETE FROM  SFCR.T_CARTON_INFO_DTA where esn=@esn";
//                        cmd[y].Parameters.Add("esn", MySqlDbType.VarChar, ESN.Length).Value = ESN;
//                        lsCmd.Add(cmd[y]);
//                    }

//                    try
//                    {
//                        BLL.BllMsSqllib.Instance.ExecteNonQueryArr(lsCmd);
//                        Msg = "OK";
//                    }
//                    catch (Exception ex)
//                    {
//                        Msg = ex.Message + "ESN:" + ESN;

//                    }

//                }

//                return Msg;
//            }
//        }



        public string Release_Bound(string ESN, string INPUTGROUP, string ReworkNo)
        {
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                int count = 0;
                mst.Add("LOCSTATION", INPUTGROUP);
                mst.Add("STATIONNAME", INPUTGROUP);
                mst.Add("WIPSTATION", INPUTGROUP);
                mst.Add("NEXTSTATION", INPUTGROUP);
                mst.Add("REWORKNO", ReworkNo);
                mst.Add("ESN", ESN);
                dp.UpdateData(tx, "SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);

                mst = new Dictionary<string, object>();
                mst.Add("ESN", ESN);
                DataSet ds = dp.GetData("SFCR.T_WIP_KEYPART_ONLINE", "ESN,WOID,SNTYPE,SNVAL,STATION,KPNO,RECDATE", mst, out count);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["SNTYPE"].ToString() == "KPESN")
                    {
                        mst = new Dictionary<string, object>();
                        mst.Add("ESN", dr["SNVAL"].ToString());
                        mst.Add("WIPSTATION", "MB_Repair");
                        dp.UpdateData(tx, "SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);
                    }
                    mst = new Dictionary<string, object>();
                    mst.Add("ESN", dr["ESN"].ToString());
                    mst.Add("WOID", dr["WOID"].ToString());
                    mst.Add("SNTYPE", dr["SNTYPE"].ToString());
                    mst.Add("SNVAL", dr["SNVAL"].ToString());
                    mst.Add("STATION", dr["STATION"].ToString());
                    mst.Add("KPNO", dr["KPNO"].ToString());
                    mst.Add("RECDATE", Convert.ToDateTime(dr["RECDATE"].ToString()));
                    dp.AddData(tx, "SFCR.T_WIP_KEYPART_UNDO", mst);
                }

                mst = new Dictionary<string, object>();
                mst.Add("ESN", ESN);
                dp.DeleteData(tx, "SFCR.T_WIP_KEYPART_ONLINE", mst);

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

        public string Rework_SN(IDictionary<string, object> mst, List<string> LsKeyParts)
        {
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            string Colnum = "ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE," +
                      "ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,LINE,SECTIONNAME,ROUTGROUPID," +
                      "STORENUMBER,WEIGHTQTY,QA_NO,QA_RESULT,TRACK_NO,ATE_STATION_NO,IN_LINE_TIME,BOMNUMBER,REWORKNO";
            int count = 0;
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("ESN", mst["ESN"]);
                DataTable dt_wip_tracking = dp.GetData("SFCR.T_WIP_TRACKING_ONLINE", Colnum, dic, out count).Tables[0];
                if (count > 0)
                    dp.AddListData(tx, "SFCR.T_WIP_UNDO", DataTableToDictionary(dt_wip_tracking));

                if (LsKeyParts.Count > 0)
                {
                    string colnumKeyPart = "esn,woId,sntype,snval,station,kpno,recdate".ToUpper();
                    DataTable dt_KeyParts = dp.GetData("SFCR.T_WIP_KEYPART_ONLINE", colnumKeyPart, dic, out count).Tables[0];
                    DataTable dt_Backup_KeyParts = new DataTable();
                    foreach (string str in colnumKeyPart.Split(','))
                    {
                        dt_Backup_KeyParts.Columns.Add(str, typeof(string));
                    }
                    if (dt_KeyParts.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt_KeyParts.Rows)
                        {
                            if (LsKeyParts.Contains(dr["SNTYPE"].ToString()))
                            {
                                dt_Backup_KeyParts.Rows.Add(dr["ESN"].ToString(), dr["WOID"].ToString(), dr["SNTYPE"].ToString(), dr["SNVAL"].ToString(), dr["STATION"].ToString(), dr["KPNO"].ToString(), dr["RECDATE"].ToString());
                            }
                        }
                    }
                    if (dt_Backup_KeyParts.Rows.Count > 0)
                    {
                        dp.AddListData(tx, "SFCR.T_WIP_KEYPART_UNDO", DataTableToDictionary(dt_Backup_KeyParts));
                        dp.DeleteListData(tx, "SFCR.T_WIP_KEYPART_ONLINE", DataTableToDictionary(dt_Backup_KeyParts));
                    }
                }
                dp.UpdateData(tx, "SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);
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

        public string Scrap_SN(IDictionary<string, object> mst, IDictionary<string, object> mst_scrap)
        {
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            string Colnum = "ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE," +
                      "ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,LINE,SECTIONNAME,ROUTGROUPID," +
                      "STORENUMBER,WEIGHTQTY,QA_NO,QA_RESULT,TRACK_NO,ATE_STATION_NO,IN_LINE_TIME,BOMNUMBER,REWORKNO";
            int count = 0;
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("ESN", mst["ESN"]);
                DataTable dt_wip_tracking = dp.GetData("SFCR.T_WIP_TRACKING_ONLINE", Colnum, dic, out count).Tables[0];
                if (count > 0)
                    dp.AddListData(tx, "SFCR.T_WIP_UNDO", DataTableToDictionary(dt_wip_tracking));

                dp.UpdateData(tx, "SFCR.T_WIP_TRACKING_ONLINE", new string[] { "ESN" }, mst);

                dp.AddData(tx, "SFCR.T_SN_SCRAP", mst_scrap);

                string table = "SFCR.T_WO_INFO";
                string fieldlist = "SCRAPQTY = SCRAPQTY + {0}";
                string filter = "WOID ={0}";
                IDictionary<string, object> modFields = new Dictionary<string, object>();
                modFields.Add("SCRAPQTY", 1);
                IDictionary<string, object> keyVals = new Dictionary<string, object>();
                keyVals.Add("WOID", mst_scrap["WOID"]);
                TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);

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

        public DataSet GetReworkInfo(string ReworkNo)
        {
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("REWORKNO", ReworkNo);
            return dp.GetData("SFCR.T_REWORK_DETAIL_INFO", "REWORKNO,WORKDATE,USERID,PARTNUMBER,WOID,TOTALQTY,MEMO,REWORKDESC,REWORKDATE", mst, out count);

        }


        private IList<IDictionary<string, object>> DataTableToDictionary(DataTable dt)
        {
            IList<IDictionary<string, object>> LsDic = new List<IDictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                LsDic.Add(dic);
            }
            return LsDic;
        }

    }
}
