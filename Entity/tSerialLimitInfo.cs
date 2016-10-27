using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tSerialLimitInfo
    {
        /// <summary>
        /// 工单号
        /// </summary>
        public string woId { get; set; }
        /// <summary>
        /// 序列号格式(十六进制、十进制、字符)
        /// </summary>
        public string formatname { get; set; }
        /// <summary>
        /// 序列号区间
        /// </summary>
        public string seriallimit { get; set; }
        /// <summary>
        /// 序列号名称
        /// </summary>
        public string serialname { get; set; }
    }
}
