using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tLineInfo
    {
        /// <summary>
        /// 线体编号
        /// </summary>
        public string lineId { get; set; }
        /// <summary>
        /// 线体描述
        /// </summary>
        public string linedesc { get; set; }
        /// <summary>
        /// 分配的起始IP地址
        /// </summary>
        public string startIpAddress { get; set; }
        /// <summary>
        /// 分配的结束IP地址
        /// </summary>
        public string endIpAddress { get; set; }
        /// <summary>
        /// 所属车间编号
        /// </summary>
        public string wsId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 正在执行的计划编号
        /// </summary>
        public string plotId { get; set; }
    }
}
