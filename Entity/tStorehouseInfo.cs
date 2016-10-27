using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tStorehouseInfo
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string storehouseId { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string storehousename { get; set; }
        /// <summary>
        /// 仓库描述
        /// </summary>
        public string storehousedesc { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string storehouseman { get; set; }
        /// <summary>
        /// 仓库类型
        /// </summary>
        public string storehousetype { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
