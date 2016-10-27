using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tPoltInfo
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string plotId { get; set; }
        /// <summary>
        /// 工单编号
        /// </summary>
        public string woId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime startTime { get; set; }
        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime endTime { get; set; }
        /// <summary>
        /// 计划状态:开始;结束;暂停
        /// </summary>
        public int plotState { get; set; }
        /// <summary>
        /// 计划目标产能
        /// </summary>
        public int yield { get; set; }
        /// <summary>
        /// 生产阶段
        /// </summary>
        public string mfpd { get; set; }
    }
}
