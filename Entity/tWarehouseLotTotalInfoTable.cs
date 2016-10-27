using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tWarehouseLotTotalInfoTable // 批次信息表

    {
        /// <summary>
        /// 批次号
        /// </summary>
        public string lotcode { get; set; }

        /// <summary>
        /// 成品料号
        /// </summary>
        public string partnumber{ get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int qty { get; set; }

      
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime  recdate{ get; set; }

        /// <summary>
        ///InOrOut
        /// </summary>
        public string InOrOut { get; set; }


     
    }
}
