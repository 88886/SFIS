using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tCraftItem
    {
        /// <summary>
        /// 工艺项目Id
        /// </summary>
        public string craftId { get; set; }
        /// <summary>
        /// 工艺项目序号
        /// </summary>
        public string craftItem { get; set; }
        /// <summary>
        /// 工艺项目描述
        /// </summary>
        public string craftparameterdes { get; set; }
        /// <summary>
        /// 工艺项目参数上限
        /// </summary>
        public string upperlimit { get; set; }
        /// <summary>
        /// 工艺项目参数下限
        /// </summary>
        public string lowerlimit { get; set; }
        /// <summary>
        /// 其他参数
        /// </summary>
        public string other { get; set; }
    }
}
