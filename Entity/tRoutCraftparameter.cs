using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tRoutCraftparameter
    {
        /// <summary>
        /// 流程编号
        /// </summary>
        public string routgroupId { get; set; }
        /// <summary>
        /// 工艺编号
        /// </summary>
        public string craftId { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public int craftItem { get; set; }
        /// <summary>
        /// 参数描述
        /// </summary>
        public string craftparameterdes { get; set; }
        /// <summary>
        /// 参数上限
        /// </summary>
        public string upperlimit { get; set; }
        /// <summary>
        /// 参数下限
        /// </summary>
        public string lowerlimit { get; set; }
        /// <summary>
        /// 其他信息
        /// </summary>
        public string other { get; set; }
        /// <summary>
        /// 参数连接
        /// </summary>
        public string url { get; set; }
    }
}
