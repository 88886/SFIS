using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tSmtIOTable
    {
        /// <summary>
        ///SQE
        /// </summary>
        public string MasterId { get; set; }

        /// <summary>
        /// 工单
        /// </summary>
        public string WoId { get; set; }

        /// <summary>
        /// 使用者工号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 机器编号
        /// </summary>
        public string MachineCode { get; set; }

        /// <summary>
        ///状态标志
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string dtime { get; set; }

        /// <summary>
        /// 面别
        /// </summary>
        public string Side { get; set; }

    }
}
