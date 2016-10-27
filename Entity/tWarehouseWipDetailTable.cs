using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tWarehouseWipDetailTable // 仓库跟踪表

    {

        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 产品序列号
        /// </summary>
        public string esn{ get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string userId{ get; set; }

         /// <summary>
        /// 批次号
        /// </summary>
        public string lotcode{ get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int sstatus { get; set; }

      
        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime  recdate{ get; set; }

       
    }
}
