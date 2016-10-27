using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace BLL
{
    public class tCustomerComplaint
    {

        #region 数据表内没有资料  20131030 michael
       
        /// <summary>
        /// 获取全部的客诉问题   //
        /// </summary>
        /// <returns></returns>
        public DataSet GetCustomerComplaint()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select partnumber,productname,customername,baditem,analysiscause,measure,case status when '0' then '提交' when '1' then '处理中' when '2' then '完成' end as status ,userId,recdate,remark,id from tCustomerComplaint";
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 按机种，产品料号，年，月，周，日查询客诉问题
        /// </summary>
        /// <param name="customercomplaint"></param>
        /// <returns></returns>
        public DataSet GetCustomerComplaintByCondiction(Entity.tCustomerComplaint customercomplaint)
        {
            MySqlCommand cmd = new MySqlCommand();
            string sql = "";
            sql = "select partnumber,productname,customername,baditem,analysiscause,measure,status,userId,recdate,remark from tCustomerComplaint  ";
            if (customercomplaint.Ck_Datetime)
            {
                if (customercomplaint.Rdb_Year)//按年查询
                {
                    sql = sql + "  where recdate between @begin_year and @end_year ";
                    cmd.Parameters.Add("begin_year", SqlDbType.DateTime).Value = customercomplaint.begin_year;
                    cmd.Parameters.Add("end_year", SqlDbType.DateTime).Value = customercomplaint.end_year;
                }
                if (customercomplaint.Rdb_Mouth)//按月查询
                {
                    sql = sql + " where recdate between @begin_mouth and @end_mouth ";
                    cmd.Parameters.Add("begin_mouth", SqlDbType.DateTime).Value = customercomplaint.begin_mouth;
                    cmd.Parameters.Add("end_mouth", SqlDbType.DateTime).Value = customercomplaint.end_mouth;
                }
                if (customercomplaint.Rdb_Week)//按周查询
                {
                    sql = sql + " where recdate between @begin_week and @end_week ";
                    cmd.Parameters.Add("begin_week", SqlDbType.DateTime).Value = customercomplaint.begin_week;
                    cmd.Parameters.Add("end_week", SqlDbType.DateTime).Value = customercomplaint.end_week;
                }
                if (customercomplaint.Rdb_Day)//按天查询
                {
                    sql = sql + " where recdate=@dtp_day ";
                    cmd.Parameters.Add("dtp_day", SqlDbType.DateTime).Value = customercomplaint.Dtp_Day;
                }
                if (!string.IsNullOrEmpty(customercomplaint.Partnumber))
                {
                    sql = sql + " and partnumber=@partnumber";
                    cmd.Parameters.Add("partnumber", MySqlDbType.VarChar, 20).Value = customercomplaint.Partnumber;
                }
                if (!string.IsNullOrEmpty(customercomplaint.ProductName))
                {
                    sql = sql + " and productname=@productname";
                    cmd.Parameters.Add("productname", MySqlDbType.VarChar, 50).Value = customercomplaint.ProductName;
                }
                cmd.CommandText = sql;
            }
            else
            {
                if (!string.IsNullOrEmpty(customercomplaint.Partnumber))
                {
                    sql = sql + " where partnumber=@partnumber";
                    cmd.Parameters.Add("partnumber", MySqlDbType.VarChar, 20).Value = customercomplaint.Partnumber;
                }
                if (!string.IsNullOrEmpty(customercomplaint.ProductName))
                {
                    sql = sql + " where productname=@productname";
                    cmd.Parameters.Add("productname", MySqlDbType.VarChar, 50).Value = customercomplaint.ProductName;
                }
                cmd.CommandText = sql;
            }
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 按月日，产品类型筛选出客户投诉状况top3
        /// </summary>
        /// <param name="customercomplaint"></param>
        /// <returns></returns>
        public DataSet GetCustomerComplaintTOP3(Entity.tCustomerComplaint customercomplaint)
        {
            MySqlCommand cmd = new MySqlCommand();
            string sql = "select baditem,COUNT(*) as baditemnumber from tCustomerComplaint  ";
            if (customercomplaint.Rdb_Mouth)
            {
                sql = sql + " where recdate between @begin_mouth and @end_mouth ";
                cmd.Parameters.Add("begin_mouth", SqlDbType.DateTime).Value = customercomplaint.begin_mouth;
                cmd.Parameters.Add("end_mouth", SqlDbType.DateTime).Value = customercomplaint.end_mouth;
            }
            if (customercomplaint.Rdb_Week)
            {
                sql = sql + " where recdate between @begin_week and @end_week ";
                cmd.Parameters.Add("begin_week", SqlDbType.DateTime).Value = customercomplaint.begin_week;
                cmd.Parameters.Add("end_week", SqlDbType.DateTime).Value = customercomplaint.end_week;
            }
            cmd.CommandText = sql + " group by baditem order by baditemnumber desc";
            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 修改客户投诉问题
        /// </summary>
        /// <param name="customercomplaint"></param>
        public void UpdateCustomerComplaint(Entity.tCustomerComplaint customercomplaint)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "update tCustomerComplaint set productname=@productname,customername=@customername,userId=@userId,status=@status,analysiscause=@analysiscause,measure=@measure,remark=@remark "
                                 + " where id=@id";
            cmd.Parameters.Add("productname", MySqlDbType.VarChar, 50).Value = customercomplaint.ProductName;
            cmd.Parameters.Add("customername", MySqlDbType.VarChar, 30).Value = customercomplaint.CustomerName;
            cmd.Parameters.Add("userId", MySqlDbType.VarChar, 10).Value = customercomplaint.UserId;
            cmd.Parameters.Add("status", MySqlDbType.VarChar, 20).Value = customercomplaint.Status;
            cmd.Parameters.Add("analysiscause", MySqlDbType.VarChar, 500).Value = customercomplaint.AnalysisCause;
            cmd.Parameters.Add("measure", MySqlDbType.VarChar, 500).Value = customercomplaint.Measure;
            cmd.Parameters.Add("remark", MySqlDbType.VarChar, 30).Value = customercomplaint.Remark;
            cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = customercomplaint.Id;
             BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
        }

        #endregion
    }
}
