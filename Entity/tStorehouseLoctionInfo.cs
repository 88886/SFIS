using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tStorehouseLoctionInfo
    {
        /// <summary>
        /// 储位编号
        /// </summary>
        public string locId { get; set; }
        /// <summary>
        /// 上级储位编号
        /// </summary>
        public string uplocId { get; set; }
        /// <summary>
        /// 储为类型编号
        /// </summary>
        public string loctype { get; set;}
        /// <summary>
        /// 所属仓库编号
        /// </summary>
        public string storehouseId { get; set; }
        /// <summary>
        /// 储物描述
        /// </summary>
        public string locdesc { get; set; }
        /// <summary>
        /// 储为容量
        /// </summary>
        public int loctotal { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string  remark { get; set; }
    }
}
