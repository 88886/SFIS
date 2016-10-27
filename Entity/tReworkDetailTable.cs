using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tReworkDetailTable
    {
        /// <summary>
        /// Reowk编号
        /// </summary>
        public string ReworkNo { get; set; }
        /// <summary>
        /// 工作日期
        /// </summary>
        public string WorkDate { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 产品料号
        /// </summary>
        public string PartNumber { get; set; }
        /// <summary>
        /// 工单
        /// </summary>
        public string woId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int TotalQTY { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MEMO { get; set; }
        /// <summary>
        /// 重工退回原因
        /// </summary>
        public string ReworkDesc { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string ReworkDate { get; set; }
    }
}
