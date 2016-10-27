using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tWsInfo
    {
        /// <summary>
        /// 车间编号
        /// </summary>
        public string wsId { get; set; }
        /// <summary>
        /// 工厂编号
        /// </summary>
        public string facId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 车间名称
        /// </summary>
        public string wsname { get; set; }
    }
}
