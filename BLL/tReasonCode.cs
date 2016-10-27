using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;

namespace BLL
{
    public partial class tReasonCode
    {
        
        public tReasonCode()
        {
          
        }

        public System.Data.DataSet GetReasonCode()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select ReasonCode,ReasonType,ReasonDesc,ReasonDesc2,DutyStation,RecDate from SFCB.B_REASON_CODE";
            //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCB.B_REASON_CODE";
            string fieldlist = "ReasonCode,ReasonType,ReasonDesc,ReasonDesc2,DutyStation,RecDate".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);            
            return dp.GetData(table, fieldlist, null, out count);
        }

        public void InserInToReasonCode(string dicstring)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "insert into SFCB.B_REASON_CODE (ReasonCode,ReasonType,ReasonDesc,ReasonDesc2,DutyStation,RecDate) VALUES (@ReasonCode,@ReasonType,@ReasonDesc,@ReasonDesc2,@DutyStation,now())";
            //cmd.Parameters.Add("ReasonCode", MySqlDbType.VarChar).Value = RC.ReasonCode;
            //cmd.Parameters.Add("ReasonType", MySqlDbType.VarChar).Value = RC.ReasonType;
            //cmd.Parameters.Add("ReasonDesc", MySqlDbType.VarChar).Value = RC.ReasonDesc;
            //cmd.Parameters.Add("ReasonDesc2", MySqlDbType.VarChar).Value = RC.ReasonDesc2;
            //cmd.Parameters.Add("DutyStation", MySqlDbType.VarChar).Value = RC.DutyStation;
            // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            dp.AddData("SFCB.B_REASON_CODE", mst);
        }

        public void UpdateReasonCode(string dicstring)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "update SFCB.B_REASON_CODE SET ReasonType =@ReasonType,ReasonDesc=@ReasonDesc ,ReasonDesc2=@ReasonDesc2 ,DutyStation=@DutyStation ,RecDate=SYSDATE() where ReasonCode =@ReasonCode  ";
            //cmd.Parameters.Add("ReasonCode", MySqlDbType.VarChar).Value = RC.ReasonCode;
            //cmd.Parameters.Add("ReasonType", MySqlDbType.VarChar).Value = RC.ReasonType;
            //cmd.Parameters.Add("ReasonDesc", MySqlDbType.VarChar).Value = RC.ReasonDesc;
            //cmd.Parameters.Add("ReasonDesc2", MySqlDbType.VarChar).Value = RC.ReasonDesc2;
            //cmd.Parameters.Add("DutyStation", MySqlDbType.VarChar).Value = RC.DutyStation;
            // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            dp.UpdateData("SFCB.B_REASON_CODE", new string[] { "REASONCODE" }, mst);

        }

        public void DeleteReasonCode(string ReasonCode)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "delete from SFCB.B_REASON_CODE where ReasonCode =@ReasonCode  ";
            //cmd.Parameters.Add("ReasonCode", MySqlDbType.VarChar).Value = RC.ReasonCode;
            // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("REASONCODE", ReasonCode);
            dp.DeleteData("SFCB.B_REASON_CODE", mst);
        }

        public string InsertDuty(string Duty, string DutyDesc)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "insert into SFCB.B_DUTY_INFO (Duty,DutyDesc) values (@Duty,@DutyDesc)";
            //cmd.Parameters.Add("Duty", MySqlDbType.VarChar).Value = Duty;
            //cmd.Parameters.Add("DutyDesc", MySqlDbType.VarChar).Value = DutyDesc;
            try
            {
                //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("DUTY", Duty);
                mst.Add("DUTYDESC", DutyDesc);
                dp.AddData("SFCB.B_DUTY_INFO", mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public System.Data.DataSet GetDutyInfo()
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select Duty,DutyDesc,recdate from SFCB.B_DUTY_INFO";
            //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            string table = "SFCB.B_DUTY_INFO";
            string fieldlist = "Duty,DutyDesc,recdate".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            return dp.GetData(table, fieldlist, null, out count);
        }

        public string DeleteDuty(string Duty)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "delete from SFCB.B_DUTY_INFO where Duty=@Duty ";
            //cmd.Parameters.Add("Duty", MySqlDbType.VarChar).Value = Duty;      
            try
            {
                //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("DUTY", Duty);
                dp.DeleteData("SFCB.B_DUTY_INFO", mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } 
    }
}
