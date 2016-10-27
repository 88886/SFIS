using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public  class WoSnRule
    {
        /// <summary>
        /// 工单号
        /// </summary>
        public string woid { get; set; }
        /// <summary>
        /// 序列号类型
        /// </summary>
        public string sntype { get; set; }
        /// <summary>
        /// 序列号前缀
        /// </summary>
        public string snprefix { get; set; }
        /// <summary>
        /// 序列号后缀
        /// </summary>
        public string snpostfix { get; set; }
        /// <summary>
        /// 开始序列号
        /// </summary>
        public string snstart { get; set; }
        /// <summary>
        /// 结束序列号
        /// </summary>
        public string snend { get; set; }
        /// <summary>
        /// 序列号长度
        /// </summary>
        public int snleng { get; set; }
        /// <summary>
        /// 序列号版本
        /// </summary>
        public string ver { get; set; }
        /// <summary>
        /// 保留位
        /// </summary>
        public string reve { get; set; }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string  recdate { get; set; }

        /// <summary>
        /// 单个产品序列号用量
        /// </summary>
        public string usenum { get; set; }
    }
}
