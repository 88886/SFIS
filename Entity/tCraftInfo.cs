using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tCraftInfo
    {
        /// <summary>
        /// 工艺Id
        /// </summary>
        public string craftId { get; set; }
        /// <summary>
        /// 工艺名称
        /// </summary>
        public string craftname { get; set; }
        /// <summary>
        /// 参数文件
        /// </summary>
        public string craftparameterurl { get; set; }
        /// <summary>
        /// 所属制程段
        /// </summary>
        public string beworkseg { get; set; }
        /// <summary>
        /// 测试站标记
        /// </summary>
        public string TestFlag { get; set; }
    }
}
