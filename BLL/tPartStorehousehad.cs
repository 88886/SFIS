using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using System.Data.Common;
using SrvComponent;
using SystemObject;
using GenericUtil;

namespace BLL
{
    public partial class tPartStorehousehad
    {
        
        public tPartStorehousehad()
        {
            
        }

        public System.Data.DataSet GetGangInfoByTrsn(string trsn)
        {
            string fieldlist = "trsn,vendercode,kpnumber,storehouseId,locId,case sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废' end as sstatus,recdate,datecode,lotId,remark,userId";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("TRSN", trsn);
            return dp.GetData("SFCR.T_PART_STOREHOUSE_HAD", fieldlist.ToUpper(), mst, out count);
        }
        public System.Data.DataSet GetGangInfoInWare(string name, string value)
        {
            int count = 0;
            string table = "SFCR.T_PART_STOREHOUSE_HAD";
            string fieldlist = " trsn,vendercode,kpnumber,storehouseId,locId,case sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废'else '未定义' end as sstatus,recdate,datecode,lotId,remark,userId";             
 
            string filter = " trsn like '999999%' and sstatus in(0,6,9) "; 
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
            {
                filter+=" and " +name.ToUpper() + " ={0}";
                mst.Add(name.ToUpper(), value);
            }
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }

        /// <summary>
        /// 获取表中所有钢板的信息
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet Getgangwang()
        {           

            int count = 0;
            string table = "SFCR.T_PART_STOREHOUSE_HAD";
            string fieldlist = "trsn,vendercode,kpnumber,storehouseId,locId, case sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废' end as sstatus,recdate,remark";
            string filter = "trsn like {0}";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("trsn", "999999%");
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }

        public System.Data.DataSet GetMaxTrsn()
        {       
            int count = 0;
            string table = "SFCR.T_PART_STOREHOUSE_HAD";
            string fieldlist = "max(trsn) as trsn";
            string filter = "trsn like {0}";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("trsn", "999999%");
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }

        /// <summary>
        /// 获取空的库位
        /// </summary>
        /// <param name="storehouseid"></param>
        /// <returns></returns>
        public System.Data.DataSet GetNotUsedLocId(string storehouseid)
        {
          
            string table = "SFCR.T_STOREHOUSE_LOCTION_INFO";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("STOREHOUSEID", storehouseid);
            return dp.GetData(table, "LOCID", mst, out count);
            
        }

        public System.Data.DataSet QueryTrsn(string trsn)
        {
            string table = "SFCR.T_PART_STOREHOUSE_HAD";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("TRSN", trsn);
            return dp.GetData(table, "*", mst, out count);
        }

        //更改TrSn状态
        public void UpdateTrSnStatus(string Status, string userid, string TrSn)
        {

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("SSTATUS", Status);
            mst.Add("USERID", userid);
            mst.Add("RECDATE", System.DateTime.Now);
            mst.Add("TRSN", TrSn);
            dp.UpdateData("SFCR.T_PART_STOREHOUSE_HAD", new string[] { "TRSN" }, mst);
        }


        /// <summary>
        /// 更新钢网信息
        /// </summary>
        /// <param name="partstoremodel"></param>
        /// <param name="trsn"></param>
        /// <returns></returns>
        public void UpdateGangInfo(string dicstring)
        {          
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            dp.UpdateData("SFCR.T_PART_STOREHOUSE_HAD", new string[] { "TRSN" }, mst);
        }

        ///// <summary>
        ///// 根据钢网编号，修改仓库和库位
        ///// </summary>
        ///// <param name="trsn"></param>
        ///// <param name="storehouseid"></param>
        ///// <param name="locid"></param>
        ///// <returns></returns>
        //public void UpdateByTrsn(string trsn, string storehouseid, string locid)
        //{
        //    //MySqlCommand cmd = new MySqlCommand();
        //    //cmd.CommandText = "update  SFCR.T_PART_STOREHOUSE_HAD set storehouseId=@storehouseid,locId=@locid where trsn=@trsn ";
        //    //cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 20).Value = trsn;
        //    //cmd.Parameters.Add("storehouseid", MySqlDbType.VarChar, 20).Value = storehouseid;
        //    //cmd.Parameters.Add("locid", MySqlDbType.VarChar, 20).Value = locid;
        //    //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

        //    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
        //    IDictionary<string, object> mst = new Dictionary<string, object>();
        //    mst.Add("STOREHOUSEID", storehouseid);
        //    mst.Add("LOCID", locid);         
        //    mst.Add("TRSN", trsn);
        //    dp.UpdateData("SFCR.T_PART_STOREHOUSE_HAD", new string[] { "TRSN" }, mst);
        //}

        ///// <summary>
        ///// 记录钢网使用次数
        ///// </summary>
        ///// <param name="trsn"></param>
        ///// <param name="total"></param>
        //public void UpdateGangWangUseCount(string trsn, int total)
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "UPDATE SFCR.T_GANGWANG Set usernum=@total+usernum where trsn=@trsn";
        //    cmd.Parameters.Add("total", MySqlDbType.Int32).Value = total;
        //    cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 20).Value = trsn;
        //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        //}

        ///// <summary>
        ///// 获取钢网要刷的板子总数
        ///// </summary>
        ///// <param name="kpnumber"></param>
        ///// <returns></returns>
        //public System.Data.DataSet GetGangwangTotal(string kpnumber)
        //{

        //    MySqlCommand cmd = new MySqlCommand();
        //    if (!string.IsNullOrEmpty(kpnumber))
        //    {
        //        cmd.CommandText = "SELECT D.TRSN,C.KPNUMBER,D.STOREHOUSEID,D.LOCID,D.REMARK,SUM(C.QTY) AS TOTAL " +
        //                        "FROM (SELECT A.WOID, A.KPNUMBER, B.QTY FROM SFCR.T_SMT_KP_NORMAL_LOG A, SFCR.T_WO_INFO B " +
        //                        "WHERE A.WOID = B.WOID AND A.KPNUMBER = @sPN GROUP BY A.WOID, A.KPNUMBER, B.QTY) C " +
        //                        "INNER JOIN SFCR.T_PART_STOREHOUSE_HAD D ON C.KPNUMBER = D.KPNUMBER " +
        //                        "GROUP BY C.KPNUMBER, D.TRSN, D.STOREHOUSEID, D.LOCID, D.REMARK ";
        //        cmd.Parameters.Add("sPN", MySqlDbType.VarChar, 30).Value = kpnumber;
        //    }
        //    else
        //    {
        //        cmd.CommandText = "SELECT D.TRSN,C.KPNUMBER,D.STOREHOUSEID,D.LOCID,D.REMARK,SUM(C.QTY) AS TOTAL FROM (SELECT A.WOID, A.KPNUMBER, B.QTY " +
        //                        "FROM SFCR.T_SMT_KP_NORMAL_LOG A, SFCR.T_WO_INFO B WHERE A.WOID = B.WOID AND A.KPNUMBER IN (SELECT KPNUMBER " +
        //                        "FROM SFCR.T_PART_STOREHOUSE_HAD WHERE TRSN LIKE '999999%') GROUP BY A.WOID, A.KPNUMBER, B.QTY) C " +
        //                        "INNER JOIN SFCR.T_PART_STOREHOUSE_HAD D ON C.KPNUMBER = D.KPNUMBER " +
        //                        "GROUP BY C.KPNUMBER, D.TRSN, D.STOREHOUSEID, D.LOCID, D.REMARK ";
        //    }
        //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);


        //}


        ///// <summary>
        ///// 获取在仓库的钢网
        ///// </summary>
        ///// <returns></returns>
        //public System.Data.DataSet GetGangInfoInWare()
        //{
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandText = "select trsn,vendercode,kpnumber,storehouseId,locId,case sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废'else '未定义' end as sstatus,recdate,datecode,lotId,remark,userId from SFCR.T_PART_STOREHOUSE_HAD where trsn like '999999%' and sstatus in(0,6,9)";
        //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        //}
    

      //public  string GetSeqTrSnInfo()
      //  {
      //      string C_SEQ = string.Empty;
      //      string PRGNAME = "SEQ_PARTID";
      //      MySqlCommand cmd = new MySqlCommand();
      //      cmd.CommandText = "select current_value from sfcb.sequence where name=@PRGNAME";
      //      cmd.Parameters.Add("PRGNAME", MySqlDbType.VarChar).Value = PRGNAME;
      //      DataTable dtSEQ = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
      //      C_SEQ =  dtSEQ.Rows[0][0].ToString().PadLeft(7, '0');
      //      cmd = new MySqlCommand();
      //      cmd.CommandText = "update sfcb.sequence set current_value=current_value+increment where name=@PRGNAME";
      //      cmd.Parameters.Add("PRGNAME", MySqlDbType.VarChar).Value = PRGNAME;
      //      BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

      //      return System.DateTime.Now.ToString("yyyyMMdd")+C_SEQ;


      //      //MySqlCommand cmd = new MySqlCommand();
      //      //cmd.CommandText = "SELECT TO_CHAR(SYSDATE(),'YYMMDD')||LPAD(TO_CHAR(SEQ_PARTID.NEXTVAL),7,'0') FROM DUAL";
      //      //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0].Rows[0][0].ToString();
      //  }
        
       //public  void InsertTrSn(Entity.tPartStorehousehad TrSnInfo, out string err)
       //{
       //    err = "";
       //     BLL.BllMsSqllib.Instance.SP_InsertStorehousehadRecount(TrSnInfo, out err);

       //}

     
       //#region 发料表
       ///// <summary>
       ///// 查询库存最早一天的物料
       ///// </summary>
       ///// <returns></returns>
       //public  System.Data.DataSet QueryKpnumberMoreThanDays(string KpNumber)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select  * from SFCR.T_PART_STOREHOUSE_HAD where kpnumber=@kpnumber and sstatus='0' " +
       //                               " and recdate<(select min(recdate) from SFCR.T_PART_STOREHOUSE_HAD  where kpnumber=@kpnumber and sstatus='0')+1  and LIMIT 1001";
       //    cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 30).Value = KpNumber;
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

       //}
       ///// <summary>
       ///// 由料号查询属于哪个工单
       ///// </summary>
       ///// <param name="kpnumber"></param>
       ///// <returns></returns>
       //public  System.Data.DataSet QueryWoid(string woid)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select woId from SFCR.T_WO_BOM_INFO where woId=@woid";
       //    cmd.Parameters.Add("woid", MySqlDbType.VarChar, 30).Value = woid;
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}
       //public  System.Data.DataSet QueryWoidByKpnumber(string kpnumber)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select WoId,qty from SFCR.T_WO_BOM_INFO where kpnumber=@kpnumber";
       //    cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 30).Value = kpnumber;
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}
       ///// <summary>
       ///// 由工单号获取所有料号
       ///// </summary>
       ///// <param name="woid"></param>
       ///// <returns></returns>
       //public System.Data.DataSet GetKpnumberByWoid(string woid,string process)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    if(process=="SMD")
       //    cmd.CommandText = " select kpnumber,qty,process from SFCR.T_WO_BOM_INFO where woId=@woid and (process=@process or kpdesc like 'PCB%')";
       //    else
       //        cmd.CommandText = " select kpnumber,qty,process from SFCR.T_WO_BOM_INFO where woId=@woid and process=@process";
       //    cmd.Parameters.Add("woid", MySqlDbType.VarChar, 30).Value = woid;
       //    cmd.Parameters.Add("process", MySqlDbType.VarChar, 30).Value = process;
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}
       //#endregion

      

       ///// <summary>
       ///// 原料出入库查询
       ///// </summary>
       ///// <param name="Flag"></param>
       ///// <param name="QryInOut"></param>
       ///// <returns></returns>
       //public System.Data.DataSet QueryMaterialInputOutQTY(string Flag, Entity.tPartStorehousehadRecount QryInOut)
       //{
       //    string sSQL = "";
       //    string sSQL2 = "";
       //    string sSQL3 = "";


       //    sSQL = "select ";
       //    MySqlCommand cmd = new MySqlCommand();
       //    Dictionary<string, string> nameAndVal = new Dictionary<string, string>();
       //    if (QryInOut.showworkdate)
       //    {
       //        sSQL = sSQL + " WorkDate as 工作日期, ";
       //        sSQL3 = sSQL3 + " WorkDate,";

       //    }
       //    if (QryInOut.showpn)
       //    {
       //        sSQL = sSQL + " kpnumber as 料号, ";
       //        sSQL3 = sSQL3 + " kpnumber,";
       //    }
       //    if (QryInOut.showvc)
       //    {
       //        sSQL = sSQL + " VenderCode as 厂商代码, ";
       //        sSQL3 = sSQL3 + " VenderCode,";
       //    }
       //    if (QryInOut.showdc)
       //    {
       //        sSQL = sSQL + " DateCode as 生产周期, ";
       //        sSQL3 = sSQL3 + " DateCode,";
       //    }
       //    if (QryInOut.showlc)
       //    {
       //        sSQL = sSQL + " LotId as 生产批次, ";
       //        sSQL3 = sSQL3 + " LotId,";
       //    }

       //    if (Flag == "0")
       //    {

       //        sSQL = sSQL + string.Format(" sum(INQTY) as 入库数量,sum(OUTQTY) as 出库数量 from SFCR.T_PART_STOREHOUSE_HAD_RECOUNT where kpnumber=@kpnumber ");
       //        nameAndVal.Add("kpnumber", QryInOut.kpnumber);//2012-11-18号加入
       //        if (!string.IsNullOrEmpty(QryInOut.vendercode))
       //        {
       //            sSQL = sSQL + string.Format(" and VenderCode=@VenderCode ");
       //            nameAndVal.Add("VenderCode", QryInOut.vendercode);//2012-11-18号加入
       //        }
       //        if (!string.IsNullOrEmpty(QryInOut.datecode))
       //        {
       //            sSQL = sSQL + string.Format(" and DateCode=@DateCode ");
       //            nameAndVal.Add("DateCode", QryInOut.datecode);//2012-11-18号加入
       //        }
       //        if (!string.IsNullOrEmpty(QryInOut.lotid))
       //        {
       //            sSQL = sSQL + string.Format(" and LotId=@LotId ");
       //            nameAndVal.Add("LotId", QryInOut.lotid);//2012-11-18号加入
       //        }
       //        sSQL = sSQL + " group by " + sSQL3.Substring(0, sSQL3.Length - 1);

       //        cmd.CommandText = sSQL;
       //        cmd.Parameters.Clear();
       //        foreach (string str in nameAndVal.Keys)
       //        {
       //            cmd.Parameters.Add("@" + str, MySqlDbType.VarChar, 50).Value = nameAndVal[str];
       //        }
       //    }
       //    if (Flag == "1")
       //    {
       //        sSQL2 = "";


       //        sSQL = sSQL + " sum(INQTY) as 入库数量,sum(OUTQTY) as 出库数量 from SFCR.T_PART_STOREHOUSE_HAD_RECOUNT where  ";

       //        if (!string.IsNullOrEmpty(QryInOut.kpnumber))
       //        {
       //            sSQL2 = sSQL2 + string.Format(" and kpnumber=@kpnumber ");
       //            nameAndVal.Add("kpnumber", QryInOut.kpnumber);//2012-11-18号加入
       //        }

       //        if (!string.IsNullOrEmpty(QryInOut.vendercode))
       //        {
       //            sSQL2 = sSQL2 + string.Format(" and VenderCode=@VenderCode ");
       //            nameAndVal.Add("VenderCode", QryInOut.vendercode);//2012-11-18号加入
       //        }
       //        if (!string.IsNullOrEmpty(QryInOut.datecode))
       //        {
       //            sSQL2 = sSQL2 + string.Format(" and DateCode=@DateCode ");
       //            nameAndVal.Add("DateCode", QryInOut.datecode);//2012-11-18号加入
       //        }
       //        if (!string.IsNullOrEmpty(QryInOut.lotid))
       //        {
       //            sSQL2 = sSQL2 + string.Format(" and LotId=@LotId ");
       //            nameAndVal.Add("LotId", QryInOut.lotid);//2012-11-18号加入
       //        }
       //        if (QryInOut.StartTime != "ALL" && QryInOut.EndTime != "ALL")
       //        {
       //            sSQL2 = sSQL2 + string.Format(" and workdate+right('00' + convert(varchar,worksection),2)>=@workdateA and workdate+right('00' + convert(varchar,worksection),2)<@workdateB ",
       //                QryInOut.StartDate + QryInOut.StartTime.Substring(0, 2),
       //                QryInOut.EndDate + QryInOut.EndTime.Substring(0, 2));
       //            nameAndVal.Add("workdateA", QryInOut.StartDate + QryInOut.StartTime.Substring(0, 2));//2012-11-18号加入
       //            nameAndVal.Add("workdateB", QryInOut.EndDate + QryInOut.EndTime.Substring(0, 2));//2012-11-18号加入
       //        }
       //        else
       //        {
       //            sSQL2 = sSQL2 + string.Format(" and WorkDate>=@workdateA and  WorkDate<=@workdateB ", QryInOut.StartDate, QryInOut.EndDate);
       //            nameAndVal.Add("workdateA", QryInOut.StartDate);//2012-11-18号加入
       //            nameAndVal.Add("workdateB", QryInOut.EndDate);//2012-11-18号加入
       //        }

       //        if (QryInOut.sclass != "ALL")
       //        {
       //            sSQL2 = sSQL2 + string.Format(" and class=@class ", QryInOut.sclass);
       //            nameAndVal.Add("class", QryInOut.sclass);//2012-11-18号加入
       //        }

       //        sSQL = sSQL + sSQL2.Substring(5, sSQL2.Length - 5) + " group by " + sSQL3.Substring(0, sSQL3.Length - 1);


       //        cmd.CommandText = sSQL;
       //        cmd.Parameters.Clear();
       //        foreach (string str in nameAndVal.Keys)
       //        {
       //            cmd.Parameters.Add("@" + str, MySqlDbType.VarChar, 50).Value = nameAndVal[str];
       //        }
       //    }
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}

       ///// <summary>
       ///// 查询库存
       ///// </summary>
       ///// <param name="QMS"></param>
       ///// <returns></returns>
       //public  System.Data.DataSet QueryMaterialStocks(Entity.tPartStorehousehad QMS)
       //{
       //    string sSQL = "";
       //    string sSQL2 = "";

       //    sSQL = "select  ";
       //    sSQL2 = " group by ";
       //    MySqlCommand cmd = new MySqlCommand();
       //    Dictionary<string, string> nameAndVal = new Dictionary<string, string>();

       //    if (QMS.ShowPN)
       //    {
       //        sSQL = sSQL + " kpnumber as 料号,";
       //        sSQL2 = sSQL2 + " kpnumber,";
       //    }
       //    if (QMS.ShowVC)
       //    {
       //        sSQL = sSQL + " vendercode as 厂商代码,";
       //        sSQL2 = sSQL2 + " vendercode,";
       //    }
       //    if (QMS.ShowDC)
       //    {
       //        sSQL = sSQL + " datecode as 生产周期,";
       //        sSQL2 = sSQL2 + " datecode,";
       //    }
       //    if (QMS.ShowLC)
       //    {
       //        sSQL = sSQL + " lotid as 生产批次,";
       //        sSQL2 = sSQL2 + " lotid,";
       //    }

       //    if (QMS.ShowLoc)
       //    {
       //        sSQL = sSQL + " locid as 库位,";
       //        sSQL2 = sSQL2 + " locid,";

       //    }

       //    sSQL = sSQL.Substring(0, sSQL.Length - 1);
       //    sSQL2 = sSQL2.Substring(0, sSQL2.Length - 1);


       //    sSQL = sSQL + string.Format(",sum(qty) as 库存数量 from SFCR.T_PART_STOREHOUSE_HAD where kpnumber=@kpnumber ", QMS.KpNumber);
       //    nameAndVal.Add("kpnumber", QMS.KpNumber);//2012-11-18号加入

       //    if (!string.IsNullOrEmpty(QMS.VenderCode))
       //    {
       //        sSQL = sSQL + string.Format(" and vendercode=@vendercode ", QMS.VenderCode);
       //        nameAndVal.Add("vendercode", QMS.VenderCode);//2012-11-18号加入
       //    }
       //    if (!string.IsNullOrEmpty(QMS.DateCode))
       //    {
       //        sSQL = sSQL + string.Format(" and datecode=@datecode ", QMS.DateCode);
       //        nameAndVal.Add("datecode", QMS.DateCode);//2012-11-18号加入
       //    }
       //    if (!string.IsNullOrEmpty(QMS.LotId))
       //    {
       //        sSQL = sSQL + string.Format(" and lotid=@lotid ", QMS.LotId);
       //        nameAndVal.Add("lotid", QMS.LotId);//2012-11-18号加入
       //    }

       //    sSQL = sSQL + " and (sstatus='0' or sstatus='6' ) " + sSQL2;

       //    cmd.CommandText = sSQL;
       //    cmd.Parameters.Clear();
       //    foreach (string str in nameAndVal.Keys)
       //    {
       //        cmd.Parameters.Add( str, MySqlDbType.VarChar, 30).Value = nameAndVal[str];
       //    }
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}

       //public System.Data.DataSet QueryTrsnDetail(string trsn) //查询唯一条码状态
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select trsn ,userid , case when sstatus=0 then '仓库' " +
       //                      " when sstatus=1 then '线边仓'  when sstatus=2 then '生产线' " +
       //                      " when sstatus=3 then '已使用'  when sstatus=4 then '分盘' " +
       //                      " when sstatus=5 then '生产线待退料' when sstatus=6 then '线边仓退回仓库' else '未定义'  end " +
       //                     " as Location,recdate  from SFCR.T_PART_STOREHOUSE_DTA where trsn=@trsn ";
       //    cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 30).Value = trsn;
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}

       //#region 仓储原料报表
       ///// <summary>
       ///// 获取料号的库存
       ///// </summary>
       ///// <returns></returns>
       //public System.Data.DataSet GetAllMaterialStocks()
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select KpNumber,sum(INQTY) as INQTY,sum(OUTQTY) as OUTQTY,sum(INQTY)-sum(OUTQTY) as 库存 from SFCR.T_PART_STOREHOUSE_HAD_RECOUNT GROUP By KpNumber";
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}
       ///// <summary>
       ///// 获取料号的所有出入库记录
       ///// </summary>
       ///// <param name="kpnumber"></param>
       ///// <returns></returns>
       //public  System.Data.DataSet GetMaterialInfoByKpnumber(string kpnumber)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select b.kpnumber,case a.sstatus when 0 then '仓库' when 1 then '线边仓'when 2 then '生产线' when 3 then '已使用'  when 4 then '分盘' when 5 then '生产线待退料' when 6 then '线边仓退回仓库' else '未定义'end as sstatus,b.qty,a.recdate from SFCR.T_PART_STOREHOUSE_DTA a,SFCR.T_PART_STOREHOUSE_HAD b where a.trsn = b.trsn and b.kpnumber=@kpnumber";
       //    cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}
       //#endregion*/

     
     
     
 
       //public  System.Data.DataSet GetGangInfoByVC(string vendercode)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select trsn,vendercode,kpnumber,storehouseId,locId,case sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废' end as sstatus,recdate,datecode,lotId,remark,userId from SFCR.T_PART_STOREHOUSE_HAD where vendercode=@vendercode";
       //    cmd.Parameters.Add("vendercode", MySqlDbType.VarChar, 20).Value = vendercode;
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}
       //public  System.Data.DataSet GetGangInfoByKpnumber(string kpnumber)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select trsn,vendercode,kpnumber,storehouseId,locId,case sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废' end as sstatus,recdate,datecode,lotId,remark,userId from SFCR.T_PART_STOREHOUSE_HAD where kpnumber=@kpnumber";
       //    cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}
    
        /// <summary>
        /// 删除一条钢网信息
        /// </summary>
        /// <param name="trsn"></param>
        /// <returns></returns>
        public string DeleteGangInfoByTrsn(string trsn)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "delete from SFCR.T_PART_STOREHOUSE_DTA where trsn=@trsn ;" +
            //    "delete from SFCR.T_PART_STOREHOUSE_HAD where trsn=@trsn";
            //cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 30).Value = trsn;
            //BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            DbConnection conn = ProviderHelper.GetConnection(ProConfiguration.GetConfig().DatabaseType, ProConfiguration.GetConfig().DatabaseConnect);
              DbTransaction tx = ProviderHelper.BeginTransaction(conn);
            try
            {              

                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("TRSN", trsn);
                dp.DeleteData(tx, "SFCR.T_PART_STOREHOUSE_DTA", mst);

                mst = new Dictionary<string, object>();
                mst.Add("TRSN", trsn);
                dp.DeleteData(tx, "SFCR.T_PART_STOREHOUSE_HAD", mst);
                tx.Commit();
                return "OK";
            }
            catch (Exception ex)
            {
                tx.Rollback();
                return ex.Message;
            }
        }

        public System.Data.DataSet QueryKpnumber()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select kpnumber,trsn from SFCR.T_PART_STOREHOUSE_HAD where trsn like '99999%'";
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            int count = 0;
            string table = "SFCR.T_PART_STOREHOUSE_HAD";
            string fieldlist = "kpnumber,trsn";
            string filter = "trsn like {0}";
           
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("trsn",  "99999%");
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

        }

       //public  System.Data.DataSet GetGangInfoByCondition(string name, string value)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select a.trsn, a.kpnumber, a.vendercode,a.remark, case a.sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废' end as sstatus, a.recdate, a.locId, a.storehouseId,e.machineId from SFCR.T_PART_STOREHOUSE_HAD a," +
       //                      "(select  b.machineId,c.kpnumber from tSmtKpNormalLog b,tWoBomInfo c " +
       //                      "where c.woId=b.woId and c.kpnumber in (select d.kpnumber from SFCR.T_PART_STOREHOUSE_HAD d where d.trsn like '999999%' and d.sstatus!=9 and d." + name + " like @value)" +
       //                      " and limit 1  order by b.inputtime  desc)  e where a.trsn like '999999%' and a.sstatus!=9 a." + name + " like @value";
       //    cmd.Parameters.Add("value", MySqlDbType.VarChar, 30).Value = value + '%';
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}
       //public  System.Data.DataSet GetAllGangInfo()
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select a.trsn, a.kpnumber, a.vendercode,a.remark, case a.sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废' end as sstatus, a.recdate, a.locId, a.storehouseId,e.machineId from SFCR.T_PART_STOREHOUSE_HAD a," +
       //                      "(select  b.machineId,c.kpnumber from tSmtKpNormalLog b,tWoBomInfo c " +
       //                      "where c.woId=b.woId and c.kpnumber in (select d.kpnumber from SFCR.T_PART_STOREHOUSE_HAD d where d.trsn like '999999%' and d.sstatus!=9) " +
       //                      "and rowmun=1  order by b.inputtime  desc) as e where a.trsn like '999999%' and a.sstatus!=9";
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}
     
   

       ////public  System.Data.DataSet GetGangwangQuery(string kpnumber, string vendercode, string trsn)
       ////{
          
       ////    List<Entity.ProcedureKey> LsPdk = new List<Entity.ProcedureKey>();
       ////    Entity.ProcedureKey Pdk = null;
       ////    Pdk = new Entity.ProcedureKey();
       ////    Pdk.Variable = "KPNUMBER";
       ////    Pdk.Value = kpnumber;
       ////    LsPdk.Add(Pdk);

       ////    Pdk = new Entity.ProcedureKey();
       ////    Pdk.Variable = "VENDERCODE";
       ////    Pdk.Value = vendercode;
       ////    LsPdk.Add(Pdk);
        
       ////    Pdk = new Entity.ProcedureKey();
       ////    Pdk.Variable = "TRSN";
       ////    Pdk.Value = trsn;
       ////    LsPdk.Add(Pdk);
       ////    return BLL.BllMsSqllib.Instance.PublicReurnDataSet("pro_GetgangwangQuery", LsPdk, "RES");


       ////}
     
   
      

       ////添加工单到发料表的remark字段
       //public void UpdateTrSnWoid(string woid, string TrSn)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "update SFCR.T_PART_STOREHOUSE_HAD set remark=@woid,recdate=SYSDATE() where trsn=@trsn";
       //    cmd.Parameters.Add("woid", MySqlDbType.VarChar, 30).Value = woid;
       //    cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 30).Value = TrSn;
       //    BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
       //}

       ///// <summary>
       ///// 获取该工单的发料数量
       ///// </summary>
       ///// <param name="woid"></param>
       ///// <returns></returns>
       //public System.Data.DataSet GetMaterialInfoByWoid(string woid)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select remark,kpnumber,SUM(qty) qty,storehouseId,locId from SFCR.T_PART_STOREHOUSE_HAD  where remark=@woid group by remark,kpnumber,storehouseId,locId order by kpnumber asc";
       //    cmd.Parameters.Add("woid", MySqlDbType.VarChar,20).Value = woid;
       //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}

       //public DataSet checkLocIDbySHID(string storehouseid, string locid)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = " select * from sfcr.t_storehouse_loction_info a where  a.storehouseid=@storehouseid and a.locid=@locid    ";
       //    cmd.Parameters.Add("storehouseid", MySqlDbType.VarChar).Value = storehouseid;
       //    cmd.Parameters.Add("locid", MySqlDbType.VarChar).Value = locid;
       //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

       //}

       //  /// <summary>
       ///// 获取指定料号在库的库存位置,和数量
       ///// </summary>
       ///// <param name="kpnumber">料号</param>
       ///// <param name="total">需要获取的数量</param>
       ///// <returns></returns>
       //public System.Data.DataSet GetKpnumberLocation(string kpnumber, int total)
       //{
       //    int flag = 1;
       //    bool bf = false;
       //    MySqlCommand sqlCmd = new MySqlCommand();
       //    while (!bf)
       //    {
       //        //查询所有库存是否达到领料数量
       //        sqlCmd.Parameters.Clear();
       //        sqlCmd.CommandText = "select sum(qty) as tal from SFCR.T_PART_STOREHOUSE_HAD where kpnumber=@kpnumber and sstatus in (@sstatus,@sstatus1)";
       //        sqlCmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
       //        sqlCmd.Parameters.Add("sstatus", MySqlDbType.Int32).Value = 0;
       //        sqlCmd.Parameters.Add("sstatus1", MySqlDbType.Int32).Value = 6;

       //        object obj = BLL.BllMsSqllib.Instance.sqlExecuteScalar(sqlCmd);
       //        if (obj == null || string.IsNullOrEmpty(obj.ToString()))
       //            obj = 0;
       //        if (int.Parse(obj.ToString()) < total)
       //        {
       //            bf = true;
       //            flag = 0;
       //            break;
       //        }

       //        while (!bf)//2013-07-23
       //        {
       //            //如果总数达到领料数量,则根据先进先出原则找料
       //            sqlCmd.Parameters.Clear();
       //            sqlCmd.CommandText = "select sum(qty) as tal from tPartStorehousehad where kpnumber=@kpnumber and sstatus in (@sstatus,@sstatus1) " +
       //                " and recdate<(select min(recdate) from SFCR.T_PART_STOREHOUSE_HAD  where kpnumber=@kpnumber and sstatus in (@sstatus,@sstatus1))+" + flag;
       //            sqlCmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
       //            sqlCmd.Parameters.Add("sstatus", MySqlDbType.Int32).Value = 0;
       //            sqlCmd.Parameters.Add("sstatus1", MySqlDbType.Int32).Value = 6;

       //            object ob = BLL.BllMsSqllib.Instance.sqlExecuteScalar(sqlCmd);
       //            if (ob == null || string.IsNullOrEmpty(obj.ToString()))
       //                ob = 0;

       //            if (int.Parse(ob.ToString()) < total)
       //            {
       //                bf = false;
       //                flag++;
       //            }
       //            else
       //                bf = true;

       //        }
       //    }
       //    sqlCmd.Parameters.Clear();
       //    //找出所有满足条件的料
       //    if (flag == 0)
       //    {
       //        sqlCmd.CommandText = "select k.trsn,k.kpnumber,s.storehouseId,k.locId,k.qty from SFCR.T_PART_STOREHOUSE_HAD k, SFCR.T_STOREHOUSE_LOCTION_INFO s where k.kpnumber=@kpnumber and s.locId=k.locId and k.sstatus in (@sstatus,@sstatus1)";
       //        sqlCmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
       //        sqlCmd.Parameters.Add("sstatus", MySqlDbType.Int32).Value = 0;
       //        sqlCmd.Parameters.Add("sstatus1", MySqlDbType.Int32).Value = 6;
       //    }
       //    else
       //    {
       //        sqlCmd.CommandText = "select top 5000 k.trsn,k.kpnumber,s.storehouseId, k.locId,k.qty from SFCR.T_PART_STOREHOUSE_HAD k,SFCR.T_STOREHOUSE_LOCTION_INFO s where k.kpnumber=@kpnumber and k.sstatus in (@sstatus,@sstatus1) and s.locId=k.locId and k.recdate<(select min(recdate) from tPartStorehousehad  where kpnumber=@kpnumber and sstatus in(@sstatus,@sstatus1))+" + flag;
       //        sqlCmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
       //        sqlCmd.Parameters.Add("sstatus", MySqlDbType.Int32).Value = 0;
       //        sqlCmd.Parameters.Add("sstatus1", MySqlDbType.Int32).Value = 6;

       //    }
       //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(sqlCmd);
       //}

       ///// <summary>
       ///// 返回仓库暂收但还没有入库的材料
       ///// </summary>
       ///// <returns></returns>
       //public System.Data.DataSet GetStoregabuffer()
       //{

       //    MySqlCommand cmd = new MySqlCommand();
       //   // cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where (locId='' or locId is null)";
       //    cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where trsn='1401010000199' ";
       //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}

       //public System.Data.DataSet GetStoregabufferByKpnumber(string kpnumber)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where kpnumber=@kpnumber and (locId='' or locId is null)";
       //    cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
       //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}

       //public System.Data.DataSet GetStoregabufferByVendercode(string Vc)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where vendercode=@vendercode and (locId='' or locId is null)";
       //    cmd.Parameters.Add("vendercode", MySqlDbType.VarChar, 20).Value = Vc;
       //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}

       //public System.Data.DataSet GetStoregabufferByDatecode(string dc)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where datecode=@datecode and (locId='' or locId is null)";
       //    cmd.Parameters.Add("datecode", MySqlDbType.VarChar, 20).Value = dc;
       //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}

       //public System.Data.DataSet GetStoregabufferByUserId(string userId)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where userId=@userId and (locId='' or locId is null)";
       //    cmd.Parameters.Add("userId", MySqlDbType.VarChar, 20).Value = userId;
       //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}

       //public System.Data.DataSet GetStoregabufferByTrsn(string trsn)
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where trsn=@trsn ";
       //    cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 20).Value = trsn;
       //    return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}

       ///// <summary>
       ///// 根据提供的唯一序列号更新库存库位
       ///// </summary>
       ///// <param name="storehouseId"></param>
       ///// <param name="locId"></param>
       ///// <param name="trsn"></param>
       ///// <returns></returns>
       //public string UpdateKeyPartlocByTrsn(string storehouseId, string locId, string trsn, string userId)
       //{
       //    try
       //    {
       //        MySqlCommand cmd = new MySqlCommand();
       //        cmd.CommandText = "update SFCR.T_PART_STOREHOUSE_HAD set userId=@userId, locId=@locId,storehouseId=@storehouseId where trsn=@trsn";
       //        cmd.Parameters.Add("userId", MySqlDbType.VarChar, 15).Value = userId;
       //        cmd.Parameters.Add("locId", MySqlDbType.VarChar, 20).Value = locId;
       //        cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 20).Value = storehouseId;
       //        cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 20).Value = trsn;
       //        BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
       //        return string.Empty;
       //    }
       //    catch (Exception ex)
       //    {
       //        return ex.Message;
       //    }
       //}

       ///// <summary>
       ///// 根据提供的料号更新库位
       ///// </summary>
       ///// <param name="storehouseId"></param>
       ///// <param name="locId"></param>
       ///// <param name="kpnumber"></param>
       ///// <returns></returns>
       //public string UpdateKeyPartlocByKpnumber(string storehouseId,
       //    string locId,
       //    string kpnumber)
       //{
       //    try
       //    {
       //        MySqlCommand cmd = new MySqlCommand();
       //        cmd.CommandText = "update SFCR.T_PART_STOREHOUSE_HAD set locId=@locId,storehouseId=@storehouseId where kpnumber=@kpnumber and locId='' or locId is null";
       //        cmd.Parameters.Add("locId", MySqlDbType.VarChar, 20).Value = locId;
       //        cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 20).Value = storehouseId;
       //        cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
       //        BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
       //        return string.Empty;
       //    }
       //    catch (Exception ex)
       //    {
       //        return ex.Message;
       //    }
       //}//vendercode

       ///// <summary>
       ///// 根据提供的厂商代码更新库位
       ///// </summary>
       ///// <param name="storehouseId"></param>
       ///// <param name="locId"></param>
       ///// <param name="vendercode"></param>
       ///// <returns></returns>
       //public string UpdateKeyPartlocByVendercode(string storehouseId, string locId,
       //    string vendercode)
       //{
       //    try
       //    {
       //        MySqlCommand cmd = new MySqlCommand();
       //        cmd.CommandText = "update SFCR.T_PART_STOREHOUSE_HAD set locId=@locId,storehouseId=@storehouseId where vendercode=@vendercode and locId='' or locId is null";
       //        cmd.Parameters.Add("locId", MySqlDbType.VarChar, 20).Value = locId;
       //        cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 20).Value = storehouseId;
       //        cmd.Parameters.Add("vendercode", MySqlDbType.VarChar, 20).Value = vendercode;
       //        BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
       //        return string.Empty;
       //    }
       //    catch (Exception ex)
       //    {
       //        return ex.Message;
       //    }
       //}

      

       ///// <summary>
       ///// 材料接收时打印五合一标签
       ///// </summary>
       ///// <param name="sd"></param>
       //public void MaterialPrint(Entity.tPartStorehousehad sd, string kpdesc, string partgroup, string vendername, string PO)
       //{
       //    BLL.BllMsSqllib.Instance.MaterialPrint(sd, kpdesc, partgroup, vendername, PO);
       //}
      
    }
}

