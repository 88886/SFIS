using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tSnRule
    {
        /// <summary>
        /// 工单号
        /// </summary>
        public string woid { get; set; }
        /// <summary>
        /// SN前固定码
        /// </summary>
        public string snprefix { get; set; }
        /// <summary>
        /// SN后固定码
        /// </summary>
        public string snpostfix { get; set; }
        /// <summary>
        /// SN开始
        /// </summary>
        public string snstart { get; set; }
        /// <summary>
        /// sn结束
        /// </summary>
        public string snend { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string ver { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public string reve { get; set; }
        /// <summary>
        /// 当前SN
        /// </summary>
        public string currsn { get; set; }

    }
}
