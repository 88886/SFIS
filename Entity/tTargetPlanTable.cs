using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
  public  class tTargetPlanTable
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string Idx { get; set; }

        /// <summary>
        /// 工作日期
        /// </summary>
        public string WorkDate { get; set; }


        /// <summary>
        /// 班别
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 开始途程
        /// </summary>
        public string Locstation { get; set; }
      /// <summary>
      /// 结束途程
      /// </summary>
        public string EndStatin { get; set; }

        /// <summary>
        /// 工单
        /// </summary>
        public string  woId { get; set; }

         /// <summary>
        /// 产品料号
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// 线别
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// 目标产出
        /// </summary>
        public string TargetQty  { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime  { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 面别
        /// </summary>
        public string Side  { get; set; } 
    }
}
