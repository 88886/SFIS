using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tWarehouseWipKeyPartTable // 唯一序列号对应信息表
    {
        /// <summary>
        /// idx
        /// </summary>
        public Guid idx { get; set; }

        /// <summary>
        /// 唯一序列号
        /// </summary>
        public string esn{ get; set; }

        /// <summary>
        /// 工单号
        /// </summary>
        public string woId { get; set; }

        /// <summary>
        /// 序列号类型
        /// </summary>
        public string sntype { get; set; }

         /// <summary>
        ///序列号的值
        /// </summary>
        public string snval { get; set; }
      
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime  recdate{ get; set; }

     
    }
}
