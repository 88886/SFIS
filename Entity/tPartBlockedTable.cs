using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
   public class tPartBlockedTable
    {
        /// <summary>
        /// Rowid
        /// </summary>
       public string id { get; set; }

        /// <summary>
        /// 物料料号
        /// </summary>
       public string Part_No { get; set; }

        /// <summary>
        /// 物料生产周期
        /// </summary>
       public string Date_Code { get; set; }

        /// <summary>
        /// 物料生产厂商
        /// </summary>
       public string VenderCode { get; set; }

        /// <summary>
        /// 物料生产批次
        /// </summary>
       public string LotId { get; set; }

        /// <summary>
        /// 确认类型
        /// </summary>
       public string CheckType { get; set; }

        /// <summary>
        /// 是否检查 Y或N
        /// </summary>
       public string UseFlag { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
       public DateTime InTime { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
       public string UserId { get; set; }

    }
}
