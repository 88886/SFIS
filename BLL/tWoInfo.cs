using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using System.Data.Common;
using SrvComponent;
using SystemObject;
using System.Globalization;
using System.IO;

namespace BLL
{
    public partial class tWoInfo
    {
        public tWoInfo()
        {
        }

        /// <summary>
        /// SFCR.T_WO_INFO
        /// </summary>
        string table = "SFCR.T_WO_INFO";       

        /// <summary>
        /// 获取工单指定字段
        /// </summary>
        /// <param name="strWo"></param>
        /// <param name="Partnumber"></param>
        /// <param name="Fields"></param>
        /// <returns></returns>
        public DataSet GetWoInfo(string strWo, string Partnumber, string Fields)
        {
            string fieldlist = "woId,poId,qty,wostate,userId,partnumber,bomver,inputgroup,outputgroup,wotype,sapwotype,recdate,pver,bomnumber,routgroupId,outputqty,inputqty,scrapqty,cpwd,productname,wo_close_time,sw_ver,fw_ver,nal_prefix,check_no,lineid,clear_serial_type,factoryid,loc".ToUpper();
            if (string.IsNullOrEmpty(Fields))
                Fields = fieldlist;
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(strWo))
                mst.Add("WOID", strWo);
            if (!string.IsNullOrEmpty(Partnumber))
                mst.Add("PARTNUMBER", Partnumber);
            return dp.GetData(table, Fields, mst, out count);
        }
        public DataSet GetWoInfo(IDictionary<string, object> mst, string Fields)
        {
            string fieldlist = "woId,poId,qty,wostate,userId,partnumber,bomver,inputgroup,outputgroup,wotype,sapwotype,recdate,pver,bomnumber,routgroupId,outputqty,inputqty,scrapqty,cpwd,productname,wo_close_time,sw_ver,fw_ver,nal_prefix,check_no,lineid,clear_serial_type,factoryid,loc".ToUpper();
            if (string.IsNullOrEmpty(Fields))
                Fields = fieldlist;
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);            
            return dp.GetData(table, Fields, mst, out count);
        }
      
        /// <summary>
        /// 新增工单信息
        /// </summary>
        /// <param name="TWI"></param>
        /// <param name="Err"></param>
        public string InsertWoInfo(string dicwoinfo,string AteScript,string esn, string  diclssnrule)
        {
            string err = "";
            IDictionary<string, object> dicWO = MapListConverter.JsonToDictionary(dicwoinfo);
             IList<IDictionary<string, object>> lsdicsnrule =null;
            if (!string.IsNullOrEmpty(diclssnrule))
               lsdicsnrule = MapListConverter.JsonToListDictionary(diclssnrule);
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
                try
                {
                   
                    #region 添加工单                                
                    string fieldlist = "COUNT(1)";
                    int count = 0;
                    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                    IDictionary<string, object> mst = new Dictionary<string, object>();
                    mst.Add("WOID", dicWO["WOID"]);
                    DataTable dt= dp.GetData(table, fieldlist,  mst, out count).Tables[0];
      
                    err = "[woinfo]";
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        dicWO.Add("RECDATE", System.DateTime.Now);
                        dp.AddData(tx,table,dicWO);
                        #region
                  
                        #endregion
                    }
                    else
                    {
                        dicWO.Add("RECDATE", System.DateTime.Now);
                        dp.UpdateData(tx,table, new string[] { "WOID" }, dicWO);                     
                    }
                
                    err = "[AteScript]";
                    #region 脚本信息
                    if (!string.IsNullOrEmpty(AteScript))
                    {
                        mst = new Dictionary<string, object>();
                        mst.Add("WOID", dicWO["WOID"]);
                        DataTable dtate = dp.GetData("SFCB.B_ATE_SCRIPT", "COUNT(1)", mst, out count).Tables[0];
                     
                        if (dtate.Rows[0][0].ToString() == "0")
                        {                           
                            mst = new Dictionary<string, object>();
                            mst.Add("WOID", dicWO["WOID"]);
                            mst.Add("SCRIPT", AteScript);
                            dp.AddData(tx, "SFCB.B_ATE_SCRIPT", mst);
                        }
                        else
                        {                           
                            mst = new Dictionary<string, object>();
                            mst.Add("WOID", dicWO["WOID"]);
                            mst.Add("SCRIPT", AteScript);
                            dp.UpdateData(tx, "SFCB.B_ATE_SCRIPT", new string[] { "WOID" }, mst);

                        }
                    }
                 
                    #endregion

                    #endregion


                    err = "[lsdicsnrule]";
                    #region 添加工单序列号区间
                    if (lsdicsnrule != null &&  lsdicsnrule.Count > 0)
                    {
                        err = "[lsdicsnrule_1]";                    
                    mst= new Dictionary<string,object>();
                    mst.Add("WOID",dicWO["WOID"]);
                    mst.Add("REVE","1");
                    dp.DeleteData(tx, "SFCR.T_SN_RULE", mst); 
                   
                        mst = new Dictionary<string, object>();
                        mst.Add("WOID", lsdicsnrule[0]["WOID"]);
                        dp.DeleteData("SFCR.T_WO_SN_RULE", mst);
                        foreach (IDictionary<string, object> dic in lsdicsnrule)
                        {
                            dic.Add("RECDATE",System.DateTime.Now);
                        }

                        dp.AddListData(tx, "SFCR.T_WO_SN_RULE", lsdicsnrule);

                        foreach (IDictionary<string, object> dic in lsdicsnrule)
                        {
                            if (dic["SNTYPE"].ToString() == esn.ToUpper())
                            {
                                mst = new Dictionary<string, object>();
                                mst.Add("WOID", dic["WOID"]);
                                mst.Add("SNPREFIX", dic["SNPREFIX"]);
                                mst.Add("SNPOSTFIX", dic["SNPOSTFIX"]);
                                mst.Add("SNSTART", dic["SNSTART"]);
                                mst.Add("SNEND", dic["SNEND"]);
                                mst.Add("REVE", "1");
                                mst.Add("VER", dic["VER"]);
                                mst.Add("CURRSN", "NA");
                                mst.Add("RECDATE",System.DateTime.Now);
                                dp.AddData(tx, "SFCR.T_SN_RULE", mst);
                            }
                        }                       
                    }
                    #endregion
                    tx.Commit();
                    return "OK";
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return err+ex.Message;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
         
        }

        public string InsertWoInfo(string dicwoinfo)
        {
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicwoinfo);
            string fieldlist = "COUNT(1)";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", dic["WOID"]);
            DataTable dt = dp.GetData(table, fieldlist, mst, out count).Tables[0];
            try
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    mst.Add("RECDATE", System.DateTime.Now);
                    dp.AddData(table, dic);
                }
                else
                {
                    mst.Add("RECDATE", System.DateTime.Now);
                    dp.UpdateData(table, new string[] { "WOID" }, dic);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string InsertAteScript(string AteScript)
        {
            try
            {
                int count = 0;
                IDictionary<string, object> mstAteScript = MapListConverter.JsonToDictionary(AteScript);
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst = new Dictionary<string, object>();
                mst.Add("WOID", mstAteScript["WOID"]);
                DataTable dtate = dp.GetData("SFCB.B_ATE_SCRIPT", "COUNT(1)", mst, out count).Tables[0];
                if (dtate.Rows[0][0].ToString() == "0")
                {
                    dp.AddData("SFCB.B_ATE_SCRIPT", mstAteScript);
                }
                else
                {
                    dp.UpdateData("SFCB.B_ATE_SCRIPT", new string[] { "WOID" }, mstAteScript);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string InserSnRule(string Lisdicsnrule,string ESN_TYPE)
        {
           IList<IDictionary<string, object>> Lsdicrule = MapListConverter.JsonToListDictionary(Lisdicsnrule);
           IDictionary<string, object> mst = null;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {
                mst = new Dictionary<string, object>();
                mst.Add("WOID", Lsdicrule[0]["WOID"]);
                mst.Add("REVE", "1");
                dp.DeleteData(tx, "SFCR.T_SN_RULE", mst);

                mst = new Dictionary<string, object>();
                mst.Add("WOID", Lsdicrule[0]["WOID"]);
                dp.DeleteData(tx, "SFCR.T_WO_SN_RULE", mst);      

                foreach (Dictionary<string, object> dicrule in Lsdicrule)
                {
                    if (dicrule["SNTYPE"].ToString() != "NA") ////如果前台没有添加区间信息,判定SNTYPE=NA,则不增加区间
                    {                       
                        dp.AddData(tx, "SFCR.T_WO_SN_RULE", dicrule);
                        if (dicrule["SNTYPE"].ToString() == ESN_TYPE)
                        {
                            mst = new Dictionary<string, object>();
                            mst.Add("WOID", dicrule["WOID"]);
                            mst.Add("SNPREFIX", dicrule["SNPREFIX"]);
                            mst.Add("SNPOSTFIX", dicrule["SNPOSTFIX"]);
                            mst.Add("SNSTART", dicrule["SNSTART"]);
                            mst.Add("SNEND", dicrule["SNEND"]);
                            mst.Add("REVE", "1");
                            mst.Add("VER", dicrule["VER"]);
                            mst.Add("CURRSN", "NA");
                            dp.AddData(tx, "SFCR.T_SN_RULE", mst);
                        }
                    }
                }
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
        ///  获取指定工单对应的所有序列号及其区间设置；
        ///  woId,sntype,snprefix,snpostfix,snstart,snend,snleng,ver,reve
        /// </summary>
        /// <param name="woid">工单号</param>
        /// <param name="sntype">序列号类型,可以为NULL</param>
        /// <returns></returns>
        public System.Data.DataSet GetWoSnRule(string woid, string sntype)
        {
           
            string table = "SFCR.T_WO_SN_RULE";
                string fieldlist = "distinct woId,sntype,snprefix,snpostfix,snstart,snend,snleng,ver,reve,usenum".ToUpper();
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOID", woid);
            if (!string.IsNullOrEmpty(sntype))
                 mst.Add("SNTYPE", sntype);
             return dp.GetData(table, fieldlist,  mst, out count);
        }
        public System.Data.DataSet Get_ESN_Rule(string woid)
        {
            string table = "SFCR.T_SN_RULE";
            string fieldlist = "WOID,SNPREFIX,SNPOSTFIX,SNSTART,SNEND,SNLENG,VER,REVE,RECDATE,CURRSN".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woid);            
            return dp.GetData(table, fieldlist, mst, out count); 
        }     

        private DataSet Get_T_WO_SN_RULE(string SN)
        {            
             int count = 0;
            string table = "SFCR.T_WO_SN_RULE";
            string fieldlist = "woId";
            string filter = "{0} between snstart and snend";            
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("SN", SN);
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }
        private DataSet Get_T_WO_SN_RULE_Range(string snstart,string snend,int flag)
        {
            int count = 0;
            string table = "SFCR.T_WO_SN_RULE";
            string fieldlist = "woId";
            string filter = null;
            if (flag==0)
            filter= "snstart between {0} and {1}";    
            else
            filter= "snend between {0} and {1}";

            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("snstart", snstart);
            mst.Add("snend", snend);
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }

        public string ChkSerialNumberRule_New(string woId, string snstart, string snend)
        {
           
                DataTable _dt = new DataTable();
        
                _dt=Get_T_WO_SN_RULE(snstart).Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    foreach (DataRow item in _dt.Rows)
                    {
                        if (item[0].ToString().ToUpper() != woId.ToUpper())
                            return string.Format("序列号[{1}]已经在工单[{0}]中使用过!!", item[0].ToString(), snstart);
                    }
                }            
                 _dt=Get_T_WO_SN_RULE(snend).Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    foreach (DataRow item in _dt.Rows)
                    {
                        if (item[0].ToString().ToUpper() != woId.ToUpper())
                            return string.Format("序列号[{1}]已经在工单[{0}]中使用过!!", item[0].ToString(), snend);
                    }
                }
       
               _dt= Get_T_WO_SN_RULE_Range(snstart,snend,0).Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    foreach (DataRow item in _dt.Rows)
                    {
                        if (item[0].ToString().ToUpper() != woId.ToUpper())
                            return string.Format("序列号[{1}]已经在工单[{0}]中使用过!!", item[0].ToString(), snend);
                    }
                }
                 _dt= Get_T_WO_SN_RULE_Range(snstart,snend,1).Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    foreach (DataRow item in _dt.Rows)
                    {
                        if (item[0].ToString().ToUpper() != woId.ToUpper())
                            return string.Format("序列号[{1}]已经在工单[{0}]中使用过!!", item[0].ToString(), snend);
                    }
                }
       
            return "OK";
        }

        /// <summary>
        /// 获取工单号列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetWoList()
        {
            List<string> wolist = new List<string>();         

            string table = "SFCR.T_WO_INFO";
                string fieldlist = "DISTINCT WOID";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);             
               DataTable _dt = dp.GetData(table, fieldlist,  null, out count).Tables[0];
            foreach (DataRow item in _dt.Rows)
            {
                wolist.Add(item["woId"].ToString());
            }
            return wolist;
        }

        /// <summary>
        /// 检查工单是否有效
        /// </summary>
        /// <param name="woId"></param>
        /// <returns></returns>
        public bool CheckWoISavailability(string woId)
        {          
            string table = "SFCR.T_WO_INFO";
                string fieldlist = "WOID";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOID", woId);
               mst.Add("WOSTATE", 0);
                DataTable _dt = dp.GetData(table, fieldlist,  mst, out count).Tables[0];
            if (_dt == null || _dt.Rows.Count < 1)
                return false;
            return true;
        }
    

        /// <summary>
        /// 根据工单状态 获取工单名称编号列表
        /// </summary>
        /// <returns></returns>
        public string[] GetWoListByState(int wostate)
        {
            List<string> ls = new List<string>();          
            string table = "SFCR.T_WO_INFO";
                string fieldlist = "WOID";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOSTATE", wostate);
                DataSet _ds = dp.GetData(table, fieldlist,  mst, out count);

            foreach (DataRow dr in _ds.Tables[0].Rows)
            {
                ls.Add(dr["woId"].ToString());
            }
            return ls.ToArray();
        }

        /// <summary>
        /// 根据工单状态获取工单指定的信息
        /// </summary>
        /// <param name="wostate"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public System.Data.DataSet GetWoInfoByStateOrwoId(string wostate,string woId, List<string> str)
        {
            List<string> ls = new List<string>();   
            string temp = string.Empty;
            for (int i = 0; i < str.Count; i++)
            {
                if (i < 1)
                {
                    temp = str[i].ToString();
                }
                else
                {
                    temp += "," + str[i].ToString();
                }
            }        
            string table = "SFCR.T_WO_INFO";
                string fieldlist = temp;
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
              if (!string.IsNullOrEmpty(wostate))
                mst.Add("WOSTATE", wostate);
            if (!string.IsNullOrEmpty(woId))
                mst.Add("WOID", woId);
              return dp.GetData(table, fieldlist,  mst, out count);
        }     

        #region 自动获取 N 个序列号
             


        private int convertHexStringToInt(string hexstring)
        {
            try
            {
                return int.Parse(hexstring, System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
                throw new Exception("MAC格式有误！！");
            }
        }
         
        public  bool GetWoSnListCount(string woId, string sntype, int Flag)
        {          

            int count = 0;
            string table = "SFCR.T_WO_SNLIST";
            string fieldlist = "WOID,SNTYPE,SNVAL,STATUS,SEQ,ESN";
            string filter = null;
            if (Flag==0)
              filter= "WOID={0} and SNTYPE={1}  LIMIT 1";
            if (Flag==1)
                filter = "WOID={0} and SNTYPE={1} AND STATUS=1";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            mst.Add("SNTYPE", sntype);
            DataTable dt = TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
            if (dt.Rows.Count != 0)
                return false;
            else
                return true;
        }

        //public List<string> GetSnListForAte(string sntype, string woId, int snqty)
        //{
        //    List<string> BarcodeList = new List<string>();
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = string.Format("select rowid,snval from SFCR.T_WO_SNLIST where woId=@woId and sntype=@sntype and status=0 and rownum<={0} order by seq", snqty);
        //    cmd.Parameters.Add("woId", MySqlDbType.VarChar, woId.Length).Value = woId;
        //    cmd.Parameters.Add("sntype", MySqlDbType.VarChar, sntype.Length).Value = sntype;
        //    DataTable dt = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
        //    if (dt != null && dt.Rows.Count != 0)
        //    {
        //        if (dt.Rows.Count == snqty)
        //        {
        //            MySqlCommand[] cmdList = new MySqlCommand[100];
        //            List<MySqlCommand> lsCmd = new List<MySqlCommand>();
        //            BarcodeList.Add("OK");
        //            int x = 0;
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                int y = x++;
        //                cmdList[y] = new MySqlCommand();
        //                cmdList[y].CommandText = "Update SFCR.T_WO_SNLIST set status=@flag where rowid=@idx and status=0";
        //                cmdList[y].Parameters.Add("idx", MySqlDbType.VarChar, 50).Value = dr["rowid"].ToString();
        //                cmdList[y].Parameters.Add("flag", MySqlDbType.Int32).Value = 2;

        //                lsCmd.Add(cmdList[y]);
        //                BarcodeList.Add(dr["snval"].ToString());
        //            }
        //            string msg = BLL.BllMsSqllib.Instance.ExecteNonQueryTransaction(lsCmd);
        //            if (msg != "OK")
        //            {
        //                return GetSnListForAte(sntype, woId, snqty);
        //            }
        //        }
        //        else
        //        {
        //            BarcodeList.Add("Bar code is not enough");

        //        }
        //    }
        //    else
        //    {
        //        BarcodeList.Add("FAIL");

        //    }
        //    return BarcodeList;
        //}

        //public string UpdateStatusGetSnListForAte(string sntype, string woId, List<string> listsn, int sStatus)
        //{
        //    int Flag = 0;
        //    if (sStatus == 1)
        //        Flag = 1;

        //    MySqlCommand[] cmd = new MySqlCommand[100];
        //    List<MySqlCommand> lsCmd = new List<MySqlCommand>();
        //    int x = 0;
        //    for (int i = 0; i < listsn.Count; i++)
        //    {
        //        int y = x++;
        //        cmd[y] = new MySqlCommand();
        //        cmd[y].CommandText = "Update SFCR.T_WO_SNLIST set status=@flag where woId=@woId and sntype=@sntype and snval=@snval";
        //        cmd[y].Parameters.Add("flag", MySqlDbType.Int32).Value = Flag;
        //        cmd[y].Parameters.Add("sntype", MySqlDbType.VarChar, sntype.Length).Value = sntype;
        //        cmd[y].Parameters.Add("woId", MySqlDbType.VarChar, woId.Length).Value = woId;
        //        cmd[y].Parameters.Add("snval", MySqlDbType.VarChar, listsn[i].Length).Value = listsn[i];
        //        lsCmd.Add(cmd[y]);
        //    }
        //    return BLL.BllMsSqllib.Instance.ExecteNonQueryTransaction(lsCmd);
        //}

        #endregion
        //#region 产品跟踪序列号
        /// <summary>
        /// 添加序列号区间
        /// </summary>
        /// <param name="snrule"></param>
        public void InsetSnRule(string dicstring)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);            
            dp.AddData("SFCR.T_SN_RULE", mst);
        }

        ///// <summary>
        ///// 获取最大的SN序号以及对应的工单号
        ///// </summary>
        ///// <returns></returns>
        //public string[] GetMaxSn()
        //{
        //    return BLL.BllMsSqllib.Instance.GetMaxSn();
        //}

        /// <summary>
        /// 更新跟踪序列号
        /// </summary>
        /// <param name="snrule"></param>
        /// <param name="id"></param>
        public void UpdateSnRule(string Dicsnrule)
        {           

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(Dicsnrule);            
            dp.UpdateData("SFCR.T_SN_RULE", new string[] { "ROWID" }, mst);
        }

        public DataSet GetEsnInfoByWoId(string woid)
        {
              string table = "SFCR.T_SN_RULE";
                string fieldlist = "snstart,snend";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOID", woid);
            return dp.GetData(table, fieldlist,  mst, out count);
        }
      


        #region 密码

        ///// <summary>
        ///// 添加工单自定义密码
        ///// </summary>
        ///// <param name="lsUsePwd"></param>
        ///// <returns></returns>
        //public string InserttUsePwd(List<Entity.tUsePwd> lsUsePwd)
        //{
        //    if (lsUsePwd == null || lsUsePwd.Count < 1)
        //        return "没有需要增加的数据";
        //    MySqlCommand cmd = new MySqlCommand();
        //    try
        //    {
        //        cmd.CommandText = "delete FROM sfcb.tUsePwd where woId=@woId";
        //        cmd.Parameters.Add("woId", MySqlDbType.VarChar, 20).Value = lsUsePwd[0].woId;
        //        BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //        cmd.Parameters.Clear();

        //        foreach (Entity.tUsePwd item in lsUsePwd)
        //        {
        //            cmd.Parameters.Clear();
        //            cmd.CommandText = "insert into sfcb.tUsePwd(mac,woId,pwdtype,pwdval) values(@mac,@woId,@pwdtype,@pwdval)";
        //            cmd.Parameters.Add("mac", MySqlDbType.VarChar, item.mac.Length).Value = item.mac;
        //            cmd.Parameters.Add("woId", MySqlDbType.VarChar, item.woId.Length).Value = item.woId;
        //            cmd.Parameters.Add("pwdtype", MySqlDbType.VarChar, item.pwdtype.Length).Value = item.pwdtype;
        //            cmd.Parameters.Add("pwdval", MySqlDbType.VarChar, item.pwdval.Length).Value = item.pwdval;
        //            BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //        }
        //        return string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        #endregion

        #region 脚本
        /// <summary>
        /// 获取工单的ATE测试脚本
        /// </summary>
        /// <param name="woId"></param>
        /// <returns></returns>
        public DataSet GetAteScripts(string woId)
        {            

            string table = "SFCB.B_ATE_SCRIPT";
            string fieldlist = "script";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        public string GetAteScriptsStr(string woId)
        {
            string _scripts = string.Empty;
            DataTable _dt = this.GetAteScripts(woId).Tables[0];
            if (_dt != null && _dt.Rows.Count > 0)
            {

                foreach (DataRow item in _dt.Rows)
                {
                    _scripts += item["script"].ToString() + ",";
                }
            }
            else
                _scripts = string.Empty;

            return _scripts;

        }
        public string GetAteScriptsXML(string woId)
        {
            DataSet _ds = GetAteScripts(woId);
            if (_ds.Tables[0] != null && _ds.Tables[0].Rows.Count > 0)
                return _ds.GetXml();
            else
                return string.Empty;
        }
        #endregion

      

        public System.Data.DataSet GetWoSnListInfo(string woId, string sntype)
        {           
            string table = "SFCR.T_WO_SNLIST";
            string fieldlist = "woId,sntype,snval,status,SEQ,esn".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            mst.Add("SNTYPE", sntype);
            return dp.GetData(table, fieldlist, mst, out count);
        }


        public string InsertWoSnRule(string lswosnrule)
        {
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IList<IDictionary<string, object>> LsDic = MapListConverter.JsonToListDictionary(lswosnrule);
                dp.AddListData("SFCR.T_WO_SN_RULE", LsDic);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public System.Data.DataSet GetTargetQtyAndNotInputQty(List<string> WoId)
        {
            
            int count = 0;
            string table = "SFCR.T_WO_INFO";
            string fieldlist = "WOID,QTY,QTY-INPUTQTY AS NOTINPUT";
            string filter = null;
            IDictionary<string, object> mst = new Dictionary<string, object>();
            
            int x = -1;
            foreach (string Item in WoId)
            {
                x++;
                filter += "{"+x.ToString()+"},";
                mst.Add("WOID"+x.ToString(), Item);
            }            
           filter = filter.Substring(0, filter.Length - 1);           
            return TransactionManager.GetData(table, fieldlist,"WOID IN ("+ filter+")", mst, null, null, out count);
        }

        public List<string> GetSnMacImeiForAte(string Esn, string sntype, string woId, int snqty)
        {
          
            List<string> BarcodeList = new List<string>();            
            string table = "SFCR.T_WO_SNLIST";
            string fieldlist = "SNVAL";
            int count = 0;
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
            DbTransaction tx = ProviderHelper.BeginTransaction(conn);

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            mst.Add("SNTYPE", sntype);
            mst.Add("ESN", Esn);
            DataSet ds = dp.GetData(table, fieldlist, mst, out count);

            if (ds.Tables[0].Rows.Count == 0)
            {               
                mst = new  Dictionary<string, object>();
                mst.Add("WOID", woId);
                mst.Add("SNTYPE", sntype);            
                string field = "woId={0} and sntype={1} and status=0 limit " + snqty;
                DataTable dt = TransactionManager.GetData(table, "ID AS ROWID,SNVAL", field , mst, null, null, out count).Tables[0];  
                if (dt != null && dt.Rows.Count != 0)
                {
                    if (dt.Rows.Count == snqty)
                    {                        
                        IList<IDictionary<string, object>> lsdic = new List<IDictionary<string, object>>();
                        BarcodeList.Add("OK");
                     
                        foreach (DataRow dr in dt.Rows)
                        {                           
                                                 
                            StringBuilder ofilter = new StringBuilder();
                            ofilter.Append(" ESN = {0},STATUS = {1}");
                            IDictionary<string, object> modFields = new Dictionary<string, object>();
                            modFields.Add("ESN", Esn);
                            modFields.Add("STATUS", 1);

                            string filter = " ID={0} AND STATUS = {1}";
                            IDictionary<string, object> keyVals = new Dictionary<string, object>();
                            keyVals.Add("ID", dr["rowid"].ToString());
                            keyVals.Add("STATUS1", 0);
                            dp.UpdateBatchData(tx,table, ofilter.ToString(), modFields, filter, keyVals);
                            Insert_System_Log(Esn + "-->" + dr["snval"].ToString());
                          
                        }                 
                        string msg = "OK";
                        try
                        {
                            tx.Commit();
                            // dp.UpdateListData(table, new string[] { "ID", "STATUS" }, lsdic);
                        }
                        catch (Exception ex)
                        {
                            tx.Rollback();
                            msg = ex.Message;
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                        }
                        if (msg != "OK")
                        {
                            BarcodeList[0] = "Update DataBase Fall";
                            return BarcodeList;
                        }
                        #region 处理并发时避免重复给号,更新完毕后等待300毫秒再读取一次
                        System.Threading.Thread.Sleep(300);
                        mst = new Dictionary<string, object>();
                        mst.Add("WOID", woId);
                        mst.Add("SNTYPE", sntype);
                        mst.Add("ESN", Esn);
                        DataSet dss = dp.GetData(table, fieldlist, mst, out count);
                        if (dss.Tables[0].Rows.Count == 0)
                        {
                            BarcodeList[0] = "Get Sn Exception";
                            return BarcodeList;
                        }
                        if (dss.Tables[0].Rows.Count != snqty)
                        {
                            BarcodeList[0] = "Get Sn Insufficient Quantity";
                            return BarcodeList;
                        }

                        foreach (DataRow dr in dss.Tables[0].Rows)
                        {
                            BarcodeList.Add(dr["snval"].ToString());
                            Insert_System_Log(Esn + "-->" + dr["snval"].ToString());
                        }
                        #endregion

                    }
                    else
                    {
                        BarcodeList.Add("Bar code is not enough");

                    }
                }
                else
                {
                    BarcodeList.Add("FAIL");

                }
                return BarcodeList;
            }
            else
            {
                BarcodeList.Add("OK");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BarcodeList.Add(dr[0].ToString());
                }
                return BarcodeList;
            }
        }
       /*
        public List<string> GetSnMacImeiForAte(string Esn, string sntype, string woId, int snqty, out string _StrErr)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            int count=0;
            _StrErr = "System Error";
            List<string> BarcodeList = new List<string>();
            if (sntype.ToUpper() != "IMEI")
            {
                _StrErr = "SNTYPE ERROR";
                return BarcodeList;
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("WOID", woId);
            dic.Add("SNTYPE", sntype);
            dic.Add("ESN", Esn);
            System.Data.DataSet ds = dp.GetData("SFCR.T_WO_SNLIST", "SNVAL", dic,out count);
            if (ds.Tables[0].Rows.Count == 0)
            {

                DataTable dt = null;
                GetBrocodeForAte(woId, sntype, snqty, out dt, out _StrErr);
                if (_StrErr != "OK")
                {
                    return BarcodeList;
                }
        
                if (dt != null && dt.Rows.Count != 0)
                {

                    if (dt.Rows.Count == snqty)
                    {
                       IList<IDictionary<string,object>> LsDic = new List<IDictionary<string,object>>();
                       
                        _StrErr = "OK";
                        foreach (DataRow dr in dt.Rows)
                        {
                            dic = new Dictionary<string,object>();
                            //cmd = new OracleCommand();
                            //cmd.CommandText = "Update SFCR.T_WO_SNLIST set esn=:sEsn,status=:flag where rowid=:idx and status=0";
                            //cmd.Parameters.Add("idx", OracleDbType.Varchar2, 50).Value = dr["rowid"].ToString();
                            //cmd.Parameters.Add("flag", OracleDbType.Int32).Value = 1;
                            //cmd.Parameters.Add("sEsn", OracleDbType.Varchar2).Value = Esn;
                            //lsCmd.Add(cmd);
                            dic.Add("ESN",Esn);
                            dic.Add("STATUS",1);
                            dic.Add("ROWID",dr["rowid"].ToString());
                            LsDic.Add(dic);                           
                            BarcodeList.Add(dr["snval"].ToString());
                        }
                        string msg = string.Empty;
                        try
                        {
                            dp.UpdateListData("SFCR.T_WO_SNLIST", new string[] { "ROWID", "STATUS" }, LsDic);
                            msg = "OK";
                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message;
                        }
                        //string msg = BLL.BllMsSqllib.Instance.ExecteNonQueryTransaction(lsCmd);
                        if (msg != "OK")
                        {
                            _StrErr = "Update DataBase Fall";
                            return BarcodeList;
                        }
                    }
                    else
                    {
                        _StrErr = "Bar code is not enough";
                    }
                }
                else
                {
                    _StrErr = "FAIL";

                }
                return BarcodeList;
            }
            else
            {
                _StrErr = "OK";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BarcodeList.Add(dr[0].ToString());
                }
                return BarcodeList;
            }
        }
        private void GetBrocodeForAte(string woId, string sntype, int snqty, out System.Data.DataTable dt, out string _StrErr)
        {
            _StrErr = "OK";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format(" select rowid,snval from (SELECT * FROM SFCR.T_WO_SNLIST WHERE WOID=:woId AND SNTYPE=:sntype AND STATUS=0 ORDER BY SEQ) where rownum<={0}", snqty);
            cmd.Parameters.Add("woId", OracleDbType.Varchar2).Value = woId;
            cmd.Parameters.Add("sntype", OracleDbType.Varchar2).Value = sntype;
            DataSet ds = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            dt = ds.Tables[0];

            if (Convert.ToInt64(dt.Rows[0][1].ToString()) % 2 == 0)
            {
                cmd = new OracleCommand();
                cmd.CommandText = "Update SFCR.T_WO_SNLIST set esn=:sEsn,status=:flag where rowid=:idx and status=0";
                cmd.Parameters.Add("idx", OracleDbType.Varchar2, 50).Value = dt.Rows[0][0].ToString();
                cmd.Parameters.Add("flag", OracleDbType.Int32).Value = 1;
                cmd.Parameters.Add("sEsn", OracleDbType.Varchar2).Value = "NA";
                BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                GetBrocodeForAte(woId, sntype, snqty, out dt, out _StrErr);
            }
            else
            {
                int y = 0;
                string IMEI = dt.Rows[0][1].ToString();
                foreach (DataRow dr in dt.Rows)
                {

                    if (IMEI == dr[1].ToString())
                        continue;
                    else
                    {
                        y++;
                        if (Convert.ToInt64(dr[1].ToString()) != Convert.ToInt64(IMEI) + y)
                        {
                            _StrErr = "DB IMEI ERROR";
                        }
                    }
                }

            }

        }
        */

        /// <summary>
        /// 获取工单途程 0 非测试途程,1 测试途程,2 维修途程
        /// </summary>
        /// <param name="woId"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        public List<string> GetWOCraftInfo(string woId, string Flag)
        {
            List<string> LsCraft = new List<string>();
            DataSet ds = GetWOCraftInfo_ds(woId, Flag);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtcraft = ds.Tables[0];
                if (dtcraft.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtcraft.Rows)
                    {
                        LsCraft.Add(dr["craftname"].ToString());
                    }
                }
            }
            return LsCraft;
        }
        /// <summary>
        /// 获取工单途程 0 非测试途程,1 测试途程,2 维修途程
        /// </summary>
        /// <param name="woId"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        public System.Data.DataSet GetWOCraftInfo_ds(string woId, string Flag)
        {
            try
            {
                DataSet ds = GetWoInfo(woId, null, "routgroupId".ToUpper());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string RouteId = ds.Tables[0].Rows[0]["routgroupId"].ToString();
                    return GetCraftInfoByRouteId(RouteId, Flag);
                }
                else
                {
                    return ds;
                }
            }
            catch
            {
                return null;
            }
        }

        public System.Data.DataSet GetCraftInfoByRouteId(string RouteId, string Flag)
        {
            int count = 0;
            string table = "sfcb.b_route_info a,sfcb.b_craft_info b".ToUpper();
            string fieldlist = "a.craftname as craftid,a.craftname".ToUpper();
            string filter = "a.routgroupid={0} and a.craftname=b.craftname and a.station_flag='0' ";            
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("routgroupid", RouteId);
            if (!string.IsNullOrEmpty(Flag))
            {
                filter += " and b.testflag={1}";
                mst.Add("testflag", Flag);
            }
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

        }

        /// <summary>
        /// 更改工单表
        /// </summary>
        /// <param name="tWoInFo"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        public string UpdateWoInfo(string woId)
        {
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                StringBuilder ofilter = new StringBuilder();
                ofilter.Append("inputqty=inputqty-{0}".ToUpper());
                IDictionary<string, object> modFields = new Dictionary<string, object>();
                modFields.Add("inputqty", 1);

                string filter = "WOID = {0}";
                IDictionary<string, object> keyVals = new Dictionary<string, object>();
                keyVals.Add("WOID", woId);
                dp.UpdateBatchData("SFCR.T_WO_INFO", ofilter.ToString(), modFields, filter, keyVals);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public bool CheckEsnRule(string wo, string esnno)
        {
            bool exist_esn = false;
            int count = 0;
            string table = "SFCR.T_SN_RULE".ToUpper();
            string fieldlist = "woId".ToUpper();
            string filter = "{0} between snstart and snend and woid={1}";
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("esnno", esnno);
            mst.Add("woid", wo);
            DataTable _dt = dp.GetData(table, fieldlist, filter, mst, null, null, out count).Tables[0];
            if (_dt.Rows.Count > 0)
                exist_esn = true;
            return exist_esn;
        }
        public string GetCraftInfoBywoid(string woid)
        {
            int count = 0;
            string table = "sfcr.t_wo_info a,sfcb.b_route_info b".ToUpper();
            string fieldlist = "b.craftname".ToUpper();
            string filter = "a.routgroupid=b.routgroupid and a.woid={0} and routedesc='IN' ";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("woid", woid);

            DataSet ds=TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";

        }
        private void Insert_System_Log(string StrLog)
        {
            deleevnet = new runEvent(Save_DB_Log);
            deleevnet.BeginInvoke(StrLog, null, null);   
        }
        delegate void runEvent(string StrLog);
        runEvent deleevnet;
        public void Save_DB_Log(string StrLog)
        {
            #region 存储失败日志在服务器
            try
            {
                if (!Directory.Exists("D:\\LOG"))
                    Directory.CreateDirectory("D:\\LOG");
                //如果文件a.txt存在就打开，不存在就新建 .append 是追加写 
                GregorianCalendar gc = new GregorianCalendar();
                string LogName = DateTime.Now.ToString("yyyyMMdd"); //+ gc.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString();
                FileStream fst = new FileStream(string.Format("D:\\LOG\\{0}\\SNLIST.log", LogName), FileMode.Append);
                //写数据到a.txt格式 
                StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
                swt.WriteLine(StrLog + "  -->" + DateTime.Now.ToString());
                swt.Close();
                fst.Close();
            }
            catch
            {
            }
            #endregion
        }
    }
}
