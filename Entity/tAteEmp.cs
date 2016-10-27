using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tAteEmp
    {
        /// <summary>
        /// 工艺编号
        /// </summary>
        public string craftId { get; set; }
        /// <summary>
        /// 工艺名称
        /// </summary>
        public string craftname { get; set; }
        /// <summary>
        /// 拥有权限的用户
        /// </summary>
        public string userId { get; set; }
    }
}
