using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tCartonInfo
    {
        /// <summary>
        /// 卡通箱编号
        /// </summary>
        public string cartonId { get; set; }
        /// <summary>
        /// 产品唯一序列号
        /// </summary>
        public string esn { get; set; }
        /// <summary>
        /// 产线编号
        /// </summary>
        public string lineId { get; set; }
        /// <summary>
        /// 工单编号
        /// </summary>
        public string woId { get; set; }
        /// <summary>
        /// 客户卡通箱编号
        /// </summary>
        public string mcartonnumber { get; set; }
        /// <summary>
        /// 产品SN号
        /// </summary>
        public string sn { get; set; }
        /// <summary>
        /// 产品MAC号
        /// </summary>
        public string mac { get; set; }
        /// <summary>
        /// 当前作业的电脑
        /// </summary>
        public string computer { get; set; }
        /// <summary>
        /// 当前该箱的数量
        /// </summary>
        public int number { get; set; }
    }
}
