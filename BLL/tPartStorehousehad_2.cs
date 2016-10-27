using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace BLL
{
    public partial class tPartStorehousehad
    {
        /// <summary>
        /// 获取表中所有钢板的信息
        /// </summary>
        /// <returns></returns>
        public  System.Data.DataSet Getgangwang()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select trsn,vendercode,kpnumber,storehouseId,locId, case sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废' end as sstatus,recdate,remark from SFCR.T_PART_STOREHOUSE_HAD where trsn like '999999%'";
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        public  System.Data.DataSet GetMaxTrsn()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select max(trsn) as trsn from SFCR.T_PART_STOREHOUSE_HAD where trsn like '999999%'";
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        public  System.Data.DataSet GetGangInfoByTrsn(string trsn)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select trsn,vendercode,kpnumber,storehouseId,locId,case sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废' end as sstatus,recdate,datecode,lotId,remark,userId from SFCR.T_PART_STOREHOUSE_HAD where trsn=@trsn";
            cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 20).Value = trsn;
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        public  System.Data.DataSet GetGangInfoByVC(string vendercode)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select trsn,vendercode,kpnumber,storehouseId,locId,case sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废' end as sstatus,recdate,datecode,lotId,remark,userId from SFCR.T_PART_STOREHOUSE_HAD where vendercode=@vendercode";
            cmd.Parameters.Add("vendercode", MySqlDbType.VarChar, 20).Value = vendercode;
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        public  System.Data.DataSet GetGangInfoByKpnumber(string kpnumber)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select trsn,vendercode,kpnumber,storehouseId,locId,case sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废' end as sstatus,recdate,datecode,lotId,remark,userId from SFCR.T_PART_STOREHOUSE_HAD where kpnumber=@kpnumber";
            cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 更新钢网信息
        /// </summary>
        /// <param name="partstoremodel"></param>
        /// <param name="trsn"></param>
        /// <returns></returns>
        public  void UpdateGangInfo(Entity.tPartStorehousehad partstoremodel, string trsn)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "update SFCR.T_PART_STOREHOUSE_HAD set kpnumber=@kpnumber,vendercode=@vendercode,storehouseId=@storehouseId,locId=@locId,sstatus=@ssta,remark=@remark where trsn=@trsn";
            cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 30).Value = trsn;
            cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 30).Value = partstoremodel.KpNumber;
            cmd.Parameters.Add("vendercode", MySqlDbType.VarChar, 30).Value = partstoremodel.VenderCode;
            cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 20).Value = partstoremodel.storehouseId;
            cmd.Parameters.Add("locId", MySqlDbType.VarChar, 20).Value = partstoremodel.LocId;
            cmd.Parameters.Add("ssta", MySqlDbType.VarChar).Value = partstoremodel.status.ToString();
            cmd.Parameters.Add("remark", MySqlDbType.VarChar, 100).Value = partstoremodel.Remark;           
             BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        }
        /// <summary>
        /// 删除一条钢网信息
        /// </summary>
        /// <param name="trsn"></param>
        /// <returns></returns>
        public  void DeleteGangInfoByTrsn(string trsn)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "delete from SFCR.T_PART_STOREHOUSE_DTA where trsn=@trsn ;" + "delete from SFCR.T_PART_STOREHOUSE_HAD where trsn=@trsn";
            cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 30).Value = trsn;
             BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        public  System.Data.DataSet GetGangInfoByCondition(string name, string value)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select a.trsn, a.kpnumber, a.vendercode,a.remark, case a.sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废' end as sstatus, a.recdate, a.locId, a.storehouseId,e.machineId from SFCR.T_PART_STOREHOUSE_HAD a," +
                              "(select  b.machineId,c.kpnumber from tSmtKpNormalLog b,tWoBomInfo c " +
                              "where c.woId=b.woId and c.kpnumber in (select d.kpnumber from SFCR.T_PART_STOREHOUSE_HAD d where d.trsn like '999999%' and d.sstatus!=9 and d." + name + " like @value)" +
                              " and rownum=1  order by b.inputtime  desc)  e where a.trsn like '999999%' and a.sstatus!=9 a." + name + " like @value";
            cmd.Parameters.Add("value", MySqlDbType.VarChar, 30).Value = value + '%';
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        public  System.Data.DataSet GetAllGangInfo()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select a.trsn, a.kpnumber, a.vendercode,a.remark, case a.sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废' end as sstatus, a.recdate, a.locId, a.storehouseId,e.machineId from SFCR.T_PART_STOREHOUSE_HAD a," +
                              "(select  b.machineId,c.kpnumber from tSmtKpNormalLog b,tWoBomInfo c " +
                              "where c.woId=b.woId and c.kpnumber in (select d.kpnumber from SFCR.T_PART_STOREHOUSE_HAD d where d.trsn like '999999%' and d.sstatus!=9) " +
                              "and rowmun=1  order by b.inputtime  desc) as e where a.trsn like '999999%' and a.sstatus!=9";
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 获取空的库位
        /// </summary>
        /// <param name="storehouseid"></param>
        /// <returns></returns>
        public  System.Data.DataSet GetNotUsedLocId(string storehouseid)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select a.locId from SFCR.T_STOREHOUSE_LOCTION_INFO a where a.storehouseId=@storehouseid and not exists (select 1 from sfcr.t_part_storehouse_had b where a.locId=b.locId and (sstatus>'9' and sstatus<'9'))";
            cmd.Parameters.Add("storehouseid", MySqlDbType.VarChar, 20).Value = storehouseid;
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 获取钢网要刷的板子总数
        /// </summary>
        /// <param name="kpnumber"></param>
        /// <returns></returns>
        public  System.Data.DataSet GetGangwangTotal(string kpnumber)
        {
          
            MySqlCommand cmd = new MySqlCommand();
            if (!string.IsNullOrEmpty(kpnumber))
            {
                cmd.CommandText = "SELECT D.TRSN,C.KPNUMBER,D.STOREHOUSEID,D.LOCID,D.REMARK,SUM(C.QTY) AS TOTAL " +
                                "FROM (SELECT A.WOID, A.KPNUMBER, B.QTY FROM SFCR.T_SMT_KP_NORMAL_LOG A, SFCR.T_WO_INFO B " +
                                "WHERE A.WOID = B.WOID AND A.KPNUMBER = @sPN GROUP BY A.WOID, A.KPNUMBER, B.QTY) C " +
                                "INNER JOIN SFCR.T_PART_STOREHOUSE_HAD D ON C.KPNUMBER = D.KPNUMBER " +
                                "GROUP BY C.KPNUMBER, D.TRSN, D.STOREHOUSEID, D.LOCID, D.REMARK ";
                cmd.Parameters.Add("sPN", MySqlDbType.VarChar, 30).Value = kpnumber;
            }
            else
            {
                cmd.CommandText = "SELECT D.TRSN,C.KPNUMBER,D.STOREHOUSEID,D.LOCID,D.REMARK,SUM(C.QTY) AS TOTAL FROM (SELECT A.WOID, A.KPNUMBER, B.QTY " +
                                "FROM SFCR.T_SMT_KP_NORMAL_LOG A, SFCR.T_WO_INFO B WHERE A.WOID = B.WOID AND A.KPNUMBER IN (SELECT KPNUMBER " +
                                "FROM SFCR.T_PART_STOREHOUSE_HAD WHERE TRSN LIKE '999999%') GROUP BY A.WOID, A.KPNUMBER, B.QTY) C " +
                                "INNER JOIN SFCR.T_PART_STOREHOUSE_HAD D ON C.KPNUMBER = D.KPNUMBER " +
                                "GROUP BY C.KPNUMBER, D.TRSN, D.STOREHOUSEID, D.LOCID, D.REMARK ";
            }
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);


        }

        public  System.Data.DataSet GetGangwangQuery(string kpnumber, string vendercode, string trsn)
        {
          
            List<Entity.ProcedureKey> LsPdk = new List<Entity.ProcedureKey>();
            Entity.ProcedureKey Pdk = null;
            Pdk = new Entity.ProcedureKey();
            Pdk.Variable = "KPNUMBER";
            Pdk.Value = kpnumber;
            LsPdk.Add(Pdk);

            Pdk = new Entity.ProcedureKey();
            Pdk.Variable = "VENDERCODE";
            Pdk.Value = vendercode;
            LsPdk.Add(Pdk);
        
            Pdk = new Entity.ProcedureKey();
            Pdk.Variable = "TRSN";
            Pdk.Value = trsn;
            LsPdk.Add(Pdk);
            return BLL.BllMsSqllib.Instance.PublicReurnDataSet("pro_GetgangwangQuery", LsPdk, "RES");


        }
        public  System.Data.DataSet QueryKpnumber()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select kpnumber,trsn from SFCR.T_PART_STOREHOUSE_HAD where trsn like '99999%'";
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 根据钢网编号，修改仓库和库位
        /// </summary>
        /// <param name="trsn"></param>
        /// <param name="storehouseid"></param>
        /// <param name="locid"></param>
        /// <returns></returns>
        public  System.Data.DataSet UpdateByTrsn(string trsn, string storehouseid, string locid)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "update  SFCR.T_PART_STOREHOUSE_HAD set storehouseId=@storehouseid,locId=@locid where trsn=@trsn ";
            cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 20).Value = trsn;
            cmd.Parameters.Add("storehouseid", MySqlDbType.VarChar, 20).Value = storehouseid;
            cmd.Parameters.Add("locid", MySqlDbType.VarChar, 20).Value = locid;
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 获取在仓库的钢网
        /// </summary>
        /// <returns></returns>
        public  System.Data.DataSet GetGangInfoInWare()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select trsn,vendercode,kpnumber,storehouseId,locId,case sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废'else '未定义' end as sstatus,recdate,datecode,lotId,remark,userId from SFCR.T_PART_STOREHOUSE_HAD where trsn like '999999%' and sstatus in(0,6,9)";
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        public  System.Data.DataSet GetGangInfoInWare(string name,string value)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select trsn,vendercode,kpnumber,storehouseId,locId,case sstatus  when 0 then '仓库' when 1 then '线边仓' when 2 then '生产线' when 3 then '已使用' when 6 then '退回仓库'when 7 then '维修' when 8 then '外借' when 9 then '报废'else '未定义' end as sstatus,recdate,datecode,lotId,remark,userId from SFCR.T_PART_STOREHOUSE_HAD where trsn like '999999%' and sstatus in(0,6,9) and "+
                name+" =@value";
            cmd.Parameters.Add("value", MySqlDbType.VarChar, 20).Value = value;
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        //添加工单到发料表的remark字段
        public void UpdateTrSnWoid(string woid, string TrSn)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "update SFCR.T_PART_STOREHOUSE_HAD set remark=@woid,recdate=sysdate where trsn=@trsn";
            cmd.Parameters.Add("woid", MySqlDbType.VarChar, 30).Value = woid;
            cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 30).Value = TrSn;
            BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        }

        /// <summary>
        /// 获取该工单的发料数量
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        public System.Data.DataSet GetMaterialInfoByWoid(string woid)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select remark,kpnumber,SUM(qty) qty,storehouseId,locId from SFCR.T_PART_STOREHOUSE_HAD  where remark=@woid group by remark,kpnumber,storehouseId,locId order by kpnumber asc";
            cmd.Parameters.Add("woid", MySqlDbType.VarChar,20).Value = woid;
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        public DataSet checkLocIDbySHID(string storehouseid, string locid)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = " select * from sfcr.t_storehouse_loction_info a where  a.storehouseid=@storehouseid and a.locid=@locid    ";
            cmd.Parameters.Add("storehouseid", MySqlDbType.VarChar).Value = storehouseid;
            cmd.Parameters.Add("locid", MySqlDbType.VarChar).Value = locid;
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

        }
    }
}
