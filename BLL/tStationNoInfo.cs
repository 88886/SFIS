using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using System.Data;
using MySql.Data.MySqlClient;

namespace BLL
{
    public partial class tStationNoInfo
    {
       
        public tStationNoInfo()
        {
            
        }

        /// <summary>
        /// 获取所有料站信息
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetAllStationNoInfo()
        {
            return null;
            //OracleCommand cmd = new OracleCommand();
            //cmd.CommandText = "select stationno as 料站编号,lineId as 产线编号,machineId as 机台编号,des as 描述,stationspec as 料站规格 from sfcb.stationnoInfo";
            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 获取线体的料站信息
        /// </summary>
        /// <param name="lineId">线体编号</param>
        /// <returns></returns>
        public System.Data.DataSet GetStationNoInfoByLineId(string lineId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select stationno,lineId,machineId,des,stationspec from sfcb.stationnoInfo where lineId=@lineId";
            cmd.Parameters.Add("lineId", MySqlDbType.VarChar, 50).Value = lineId;
            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 获取机器的料站信息
        /// </summary>
        /// <param name="machineId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetStationNoInfoByMachineId(string machineId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select stationno,lineId,machineId,des,stationspec from sfcb.stationnoInfo where machineId=@machineId";
            cmd.Parameters.Add("machineId", MySqlDbType.VarChar, 50).Value = machineId;

            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 获取指定料站的信息
        /// </summary>
        /// <param name="stationno">料站编号</param>
        /// <returns></returns>
        public System.Data.DataSet GetStationNoInfoBystationno(string stationno)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select stationno,lineId,machineId,des,stationspec from sfcb.stationnoInfo where stationno=@stationno";
            cmd.Parameters.Add("stationno", MySqlDbType.VarChar, 50).Value = stationno;

            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 根据站位编号修改信息
        /// </summary>
        /// <param name="stationno"></param>
        /// <param name="sni"></param>
        public void EditStationNoInfo(string stationno, Entity.tStationNoInfo sni)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "update sfcb.stationnoInfo set lineId=@lineId,machineId=@machineId,des=@des,stationspec=@stationspec where stationno=@stationno";
            cmd.Parameters.Add("lineId", MySqlDbType.VarChar, 50).Value = sni.lineId;
            cmd.Parameters.Add("machineId", MySqlDbType.VarChar, 50).Value = sni.machineId;
            cmd.Parameters.Add("des", MySqlDbType.VarChar, 50).Value = sni.des;
            cmd.Parameters.Add("stationspec", MySqlDbType.VarChar, 50).Value = sni.stationspec;
            cmd.Parameters.Add("stationno", MySqlDbType.VarChar, 50).Value = stationno;
             BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        }

        /// <summary>
        /// 新增料站信息
        /// </summary>
        /// <param name="sni"></param>
        public void InsertStationNoInfo(Entity.tStationNoInfo sni)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "update sfcb.stationnoInfo set lineId=@lineId,machineId=@machineId,des=@des,stationspec=@stationspec from stationnoInfo where stationno=@stationno";
            cmd.Parameters.Add("lineId", MySqlDbType.VarChar, 50).Value = sni.lineId;
            cmd.Parameters.Add("machineId", MySqlDbType.VarChar, 50).Value = sni.machineId;
            cmd.Parameters.Add("des", MySqlDbType.VarChar, 50).Value = sni.des;
            cmd.Parameters.Add("stationspec", MySqlDbType.VarChar, 50).Value = sni.stationspec;
            cmd.Parameters.Add("stationno", MySqlDbType.VarChar, 50).Value = sni.stationno;
             BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
        }
    }
}
