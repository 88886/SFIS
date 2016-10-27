using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tOutputlotrecordTable //出库批次记录表

    {
        /// <summary>
        /// SFC系统出库编号
        /// </summary>
        public string sfclotcode { get; set; }

         /// <summary>
        /// SAP系统出库单号
        /// </summary>
        public string saplotcode { get; set; }

        /// <summary>
        /// 成品料号
        /// </summary>
        public int partnumber{ get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int qty { get; set; }
              

        /// <summary>
        /// 客户编号
        /// </summary>
        public string customerId { get; set; }


        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime  recdate{ get; set; }

       
    }
}
