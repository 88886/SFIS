using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SrvComponent;

namespace BLL
{
    public partial class tSmtIO
    {
     
        public tSmtIO()
        {
           
        }
        public enum SmtIOStatus
        {
            正在备料,
            备料完成,
            正在换线,
            已换线,
            下线
        }
        /// <summary>
        /// 根据备料表编号获取SMTIO数据
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="woId"></param>
        /// <returns></returns>
        public System.Data.DataSet GetSmtIO(string masterId, string woId)
        {
           
            string table = "SFCR.T_SMT_IO";
            string fieldlist = "status,machineId,side".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MASTERID", masterId);
            mst.Add("WOID", woId);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        /// <summary>
        /// 获取SMTIO中指定机器的当前状态(理想值:一台机器的在生产状态同时只存在一次)
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public System.Data.DataSet GetSmtIOMachineIdStatus(string machineId, SmtIOStatus status)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select masterId,woId,machineId from SFCR.T_SMT_IO where machineId=@machineId and status=@status";
            //cmd.Parameters.Add("machineId", MySqlDbType.VarChar).Value = machineId;
            //cmd.Parameters.Add("status", MySqlDbType.Int32).Value = (int)status;
            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            string table = "SFCR.T_SMT_IO";
            string fieldlist = "masterId,woId,machineId".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MACHINEID", machineId);
            mst.Add("STATUS", (int)status);
            return dp.GetData(table, fieldlist, mst, out count);
        }

        /// <summary>
        /// 改SMTIO表中指定备料表头编号的状态
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="woId"></param>
        /// <param name="status"></param>
        public void EditSmtIOStatus(string masterId, string woId, SmtIOStatus status)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "update SFCR.T_SMT_IO set status=@status where masterId=@masterId and woId=@woId";
            //cmd.Parameters.Add("status", MySqlDbType.Int32).Value = (int)status;
            //cmd.Parameters.Add("masterId", MySqlDbType.VarChar).Value = masterId;
            //cmd.Parameters.Add("woId", MySqlDbType.VarChar).Value = woId;
            //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("STATUS", (int)status);
            mst.Add("MASTERID", masterId);
            mst.Add("WOID", woId);
            dp.UpdateData("SFCR.T_SMT_IO", new string[] { "MASTERID", "WOID" }, mst);
        }


        /// <summary>
        /// 插入SMTIO资料
        /// </summary>
        /// <param name="sSEQ"></param>
        /// <param name="sMO"></param>
        /// <param name="userId"></param>
        /// <param name="MachineCode"></param>
        /// <param name="sSIDE"></param>
        public void InserSmtIo(string dicsmtio)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "insert into SFCR.T_SMT_IO ( masterId,woId,userId,machineId,status,dtime,side) VALUES (@sSEQ,@sMO,@userId,@MachineCode,@status,SYSDATE(),@sSIDE) ";
            //cmd.Parameters.Add("sSEQ", MySqlDbType.VarChar).Value = smtio.MasterId;
            //cmd.Parameters.Add("sMO", MySqlDbType.VarChar).Value = smtio.WoId;
            //cmd.Parameters.Add("userId", MySqlDbType.VarChar).Value = smtio.UserId;
            //cmd.Parameters.Add("MachineCode", MySqlDbType.VarChar).Value = smtio.MachineCode;
            //cmd.Parameters.Add("status", MySqlDbType.VarChar).Value = smtio.Status;
            //cmd.Parameters.Add("sSIDE", MySqlDbType.VarChar).Value = smtio.Side;
            // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicsmtio); 
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)
            mst.Add("DTIME", System.DateTime.Now);
            dp.AddData("SFCR.T_SMT_IO", mst);
        }

        public void UpdateSmtIOStatus(string dicsit)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "Update SFCR.T_SMT_IO set status=@status where masterid=@sSEQ and woid=@sMO ";
            //cmd.Parameters.Add("status", MySqlDbType.VarChar).Value = sit.Status;
            //cmd.Parameters.Add("sSEQ", MySqlDbType.VarChar).Value = sit.MasterId;
            //cmd.Parameters.Add("sMO", MySqlDbType.VarChar).Value = sit.WoId;
            // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicsit);          
            dp.UpdateData("SFCR.T_SMT_IO", new string[] { "MASTERID", "WOID" }, mst);
        }
        public System.Data.DataSet GetAllSmtIO(string masterId, string woId)
        {           
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select masterid,woid,userid,machineid,status,dtime,side from SFCR.T_SMT_IO where masterId=@masterId and woId=@woId";
            //cmd.Parameters.Add("masterId", MySqlDbType.VarChar).Value = masterId;
            //cmd.Parameters.Add("woId", MySqlDbType.VarChar).Value = woId;

            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCR.T_SMT_IO";
            string fieldlist = "masterid,woid,userid,machineid,status,dtime,side".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MASTERID", masterId);
            mst.Add("WOID", woId);
            return dp.GetData(table, fieldlist, mst, out count);
        }
        public void DeleteSmtIo(string sSEQ, string sMO)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "delete FROM SFCR.T_SMT_IO where masterId=@sSEQ and woId=@sMO";
            //cmd.Parameters.Add("sSEQ", MySqlDbType.VarChar).Value = sSEQ;
            //cmd.Parameters.Add("sMO", MySqlDbType.VarChar).Value = sMO;
            //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MASTERID", sSEQ);
            mst.Add("WOID", sMO);
            dp.DeleteData("SFCR.T_SMT_IO", mst);
            

        }
    }
}
