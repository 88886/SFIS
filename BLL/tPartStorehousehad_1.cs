using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace BLL
{
    public partial class tPartStorehousehad
    {
        /// <summary>
        /// 获取指定料号在库的库存位置,和数量
        /// </summary>
        /// <param name="kpnumber">料号</param>
        /// <param name="total">需要获取的数量</param>
        /// <returns></returns>
        public System.Data.DataSet GetKpnumberLocation(string kpnumber, int total)
        {
            int flag = 1;
            bool bf = false;
            MySqlCommand sqlCmd = new MySqlCommand();
            while (!bf)
            {
                //查询所有库存是否达到领料数量
                sqlCmd.Parameters.Clear();
                sqlCmd.CommandText = "select sum(qty) as tal from SFCR.T_PART_STOREHOUSE_HAD where kpnumber=@kpnumber and sstatus in (@sstatus,@sstatus1)";
                sqlCmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
                sqlCmd.Parameters.Add("sstatus", MySqlDbType.Int32).Value = 0;
                sqlCmd.Parameters.Add("sstatus1", MySqlDbType.Int32).Value = 6;

                object obj = BLL.BllMsSqllib.Instance.sqlExecuteScalar(sqlCmd);
                if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                    obj = 0;
                if (int.Parse(obj.ToString()) < total)
                {
                    bf = true;
                    flag = 0;
                    break;
                }

                while (!bf)//2013-07-23
                {
                    //如果总数达到领料数量,则根据先进先出原则找料
                    sqlCmd.Parameters.Clear();
                    sqlCmd.CommandText = "select sum(qty) as tal from tPartStorehousehad where kpnumber=@kpnumber and sstatus in (@sstatus,@sstatus1) " +
                        " and recdate<(select min(recdate) from SFCR.T_PART_STOREHOUSE_HAD  where kpnumber=@kpnumber and sstatus in (@sstatus,@sstatus1))+" + flag;
                    sqlCmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
                    sqlCmd.Parameters.Add("sstatus", MySqlDbType.Int32).Value = 0;
                    sqlCmd.Parameters.Add("sstatus1", MySqlDbType.Int32).Value = 6;

                    object ob = BLL.BllMsSqllib.Instance.sqlExecuteScalar(sqlCmd);
                    if (ob == null || string.IsNullOrEmpty(obj.ToString()))
                        ob = 0;

                    if (int.Parse(ob.ToString()) < total)
                    {
                        bf = false;
                        flag++;
                    }
                    else
                        bf = true;

                }
            }
            sqlCmd.Parameters.Clear();
            //找出所有满足条件的料
            if (flag == 0)
            {
                sqlCmd.CommandText = "select k.trsn,k.kpnumber,s.storehouseId,k.locId,k.qty from SFCR.T_PART_STOREHOUSE_HAD k, SFCR.T_STOREHOUSE_LOCTION_INFO s where k.kpnumber=@kpnumber and s.locId=k.locId and k.sstatus in (@sstatus,@sstatus1)";
                sqlCmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
                sqlCmd.Parameters.Add("sstatus", MySqlDbType.Int32).Value = 0;
                sqlCmd.Parameters.Add("sstatus1", MySqlDbType.Int32).Value = 6;
            }
            else
            {
                sqlCmd.CommandText = "select top 5000 k.trsn,k.kpnumber,s.storehouseId, k.locId,k.qty from SFCR.T_PART_STOREHOUSE_HAD k,SFCR.T_STOREHOUSE_LOCTION_INFO s where k.kpnumber=@kpnumber and k.sstatus in (@sstatus,@sstatus1) and s.locId=k.locId and k.recdate<(select min(recdate) from tPartStorehousehad  where kpnumber=@kpnumber and sstatus in(@sstatus,@sstatus1))+" + flag;
                sqlCmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
                sqlCmd.Parameters.Add("sstatus", MySqlDbType.Int32).Value = 0;
                sqlCmd.Parameters.Add("sstatus1", MySqlDbType.Int32).Value = 6;

            }
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(sqlCmd);
        }

        /// <summary>
        /// 返回仓库暂收但还没有入库的材料
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetStoregabuffer()
        {

            MySqlCommand cmd = new MySqlCommand();
           // cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where (locId='' or locId is null)";
            cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where trsn='1401010000199' ";
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        public System.Data.DataSet GetStoregabufferByKpnumber(string kpnumber)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where kpnumber=@kpnumber and (locId='' or locId is null)";
            cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        public System.Data.DataSet GetStoregabufferByVendercode(string Vc)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where vendercode=@vendercode and (locId='' or locId is null)";
            cmd.Parameters.Add("vendercode", MySqlDbType.VarChar, 20).Value = Vc;
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        public System.Data.DataSet GetStoregabufferByDatecode(string dc)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where datecode=@datecode and (locId='' or locId is null)";
            cmd.Parameters.Add("datecode", MySqlDbType.VarChar, 20).Value = dc;
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        public System.Data.DataSet GetStoregabufferByUserId(string userId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where userId=@userId and (locId='' or locId is null)";
            cmd.Parameters.Add("userId", MySqlDbType.VarChar, 20).Value = userId;
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        public System.Data.DataSet GetStoregabufferByTrsn(string trsn)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select trsn,kpnumber ,vendercode,datecode,lotId,qty,sstatus,recdate,storehouseId,locId,userId from SFCR.T_PART_STOREHOUSE_HAD where trsn=@trsn ";
            cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 20).Value = trsn;
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 根据提供的唯一序列号更新库存库位
        /// </summary>
        /// <param name="storehouseId"></param>
        /// <param name="locId"></param>
        /// <param name="trsn"></param>
        /// <returns></returns>
        public string UpdateKeyPartlocByTrsn(string storehouseId, string locId, string trsn, string userId)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "update SFCR.T_PART_STOREHOUSE_HAD set userId=@userId, locId=@locId,storehouseId=@storehouseId where trsn=@trsn";
                cmd.Parameters.Add("userId", MySqlDbType.VarChar, 15).Value = userId;
                cmd.Parameters.Add("locId", MySqlDbType.VarChar, 20).Value = locId;
                cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 20).Value = storehouseId;
                cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 20).Value = trsn;
                BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 根据提供的料号更新库位
        /// </summary>
        /// <param name="storehouseId"></param>
        /// <param name="locId"></param>
        /// <param name="kpnumber"></param>
        /// <returns></returns>
        public string UpdateKeyPartlocByKpnumber(string storehouseId,
            string locId,
            string kpnumber)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "update SFCR.T_PART_STOREHOUSE_HAD set locId=@locId,storehouseId=@storehouseId where kpnumber=@kpnumber and locId='' or locId is null";
                cmd.Parameters.Add("locId", MySqlDbType.VarChar, 20).Value = locId;
                cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 20).Value = storehouseId;
                cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
                BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }//vendercode

        /// <summary>
        /// 根据提供的厂商代码更新库位
        /// </summary>
        /// <param name="storehouseId"></param>
        /// <param name="locId"></param>
        /// <param name="vendercode"></param>
        /// <returns></returns>
        public string UpdateKeyPartlocByVendercode(string storehouseId, string locId,
            string vendercode)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "update SFCR.T_PART_STOREHOUSE_HAD set locId=@locId,storehouseId=@storehouseId where vendercode=@vendercode and locId='' or locId is null";
                cmd.Parameters.Add("locId", MySqlDbType.VarChar, 20).Value = locId;
                cmd.Parameters.Add("storehouseId", MySqlDbType.VarChar, 20).Value = storehouseId;
                cmd.Parameters.Add("vendercode", MySqlDbType.VarChar, 20).Value = vendercode;
                BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 记录钢网使用次数
        /// </summary>
        /// <param name="trsn"></param>
        /// <param name="total"></param>
        public void UpdateGangWangUseCount(string trsn, int total)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE SFCR.T_GANGWANG Set usernum=@total+usernum where trsn=@trsn";
            cmd.Parameters.Add("total", MySqlDbType.Int32).Value = total;
            cmd.Parameters.Add("trsn", MySqlDbType.VarChar, 20).Value = trsn;
            BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        }

        /// <summary>
        /// 材料接收时打印五合一标签
        /// </summary>
        /// <param name="sd"></param>
        public void MaterialPrint(Entity.tPartStorehousehad sd, string kpdesc, string partgroup, string vendername, string PO)
        {
            BLL.BllMsSqllib.Instance.MaterialPrint(sd, kpdesc, partgroup, vendername, PO);
        }
    }
}

