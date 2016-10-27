using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tWoBomInfoTable
    {
        /// <summary>
        /// Rowid
        /// </summary>
       public Guid wbiId { get; set; }
        
        /// <summary>
        /// 工单号码
        /// </summary>
       public string woId { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
       public string UserId { get; set; }

        /// <summary>
        /// 产品料号
        /// </summary>
       public string PartNnumber { get; set; }


       /// <summary>
       /// 物料料号
       /// </summary>
       public string KpNumber { get; set; }


        /// <summary>
        /// 物料描述
        /// </summary>
       public string KpDesc { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
       public string QTY { get; set; }

        /// <summary>
        /// 制成段
        /// </summary>
       public string Process { get; set; }


        /// <summary>
        /// Bom版本
        /// </summary>
       public string BomRev { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
       public string RecTime { get; set; }

       /// <summary>
       /// 是否锁定
       /// </summary>
       public int blocked { get; set; }
    }
}
