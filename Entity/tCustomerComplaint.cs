using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tCustomerComplaint
    {
        /// <summary>
        /// 投诉编号
        /// </summary>
        public Guid  Id { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 成品料号
        /// </summary>
        public string Partnumber { get; set; }
        /// <summary>
        /// 投诉项目
        /// </summary>
        public string BadItem { get; set; }
        /// <summary>
        /// 原因分析
        /// </summary>
        public string AnalysisCause { get; set; }
        /// <summary>
        /// 改进措施
        /// </summary>
        public string Measure { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime Recdate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /*---查询时的判定条件---*/
        /// <summary>
        /// 选择时间日期
        /// </summary>
        public bool Ck_Datetime { get; set; }
        /// <summary>
        /// 按年查询
        /// </summary>
        public bool Rdb_Year { get; set; }
        /// <summary>
        /// 按月查询
        /// </summary>
        public bool Rdb_Mouth { get; set; }
        /// <summary>
        /// 按周查询
        /// </summary>
        public bool Rdb_Week { get; set; }
        /// <summary>
        /// 按天查询
        /// </summary>
        public bool Rdb_Day { get; set; }

        /// <summary>
        /// 日期：年
        /// </summary>
        public DateTime Dtp_Year { get; set; }
        /// <summary>
        /// 日期：月
        /// </summary>
        public DateTime Dtp_Mouth { get; set; }
        /// <summary>
        /// 日期：周
        /// </summary>
        public DateTime Dtp_Week { get; set; }
        /// <summary>
        /// 日期：天
        /// </summary>
        public DateTime Dtp_Day { get; set; }

        public DateTime begin_year{get;set;}
        public DateTime end_year{get;set;}

        public DateTime begin_mouth { get; set; }
        public DateTime end_mouth { get; set; }

        public DateTime begin_week { get; set; }
        public DateTime end_week { get; set; }


    }
}
