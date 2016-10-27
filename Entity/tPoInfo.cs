using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tPoInfo
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string poId { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public Guid customerId { get; set; }
        /// <summary>
        /// 产品料号
        /// </summary>
        public string matnumber { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int qty { get; set; }
        /// <summary>
        /// 交货日期
        /// </summary>
        public DateTime dlydate { get; set; }
        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime recdate { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int postate { get; set; }
    }
}
