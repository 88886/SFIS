using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tDeptInfo
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string deptname { get; set; }
        /// <summary>
        /// 所属工厂/公司
        /// </summary>
        public string facId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 部门描述
        /// </summary>
        public string deptdesc { get; set; }
    }
}
