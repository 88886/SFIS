using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tWipKeyPartTable
    {

        /// <summary>
        /// Rowid    
        /// </summary>
        public string idx { get; set; }

         /// <summary>
        ///唯一条码
        /// </summary>
        public string esn { get; set; }

         /// <summary>
        ///产品工单
        /// </summary>
        public string woId { get; set; }

         /// <summary>
        ///序列号类型
        /// </summary>
        public string sntype { get; set; }

         /// <summary>
        ///序列号值
        /// </summary>
        public string snval { get; set; }

        /// <summary>
        ///途程
        /// </summary>
        public string station { get; set; }

        /// <summary>
        ///KPNO
        /// </summary>
        public string KPNO { get; set; }

         /// <summary>
        ///时间
        /// </summary>
        public string recdate { get; set; }

     
    }
}
