using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class T_SYSTEM_LOG
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string prg_name { get; set; }
        /// <summary>
        /// 操作描述
        /// </summary>
        public string action_desc { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string action_type { get; set; }
    }
}
