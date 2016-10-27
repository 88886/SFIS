using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class SMT_KP_DETALTForWo
    {
        public long Id { get; set; }
        /// <summary>
        /// 用户工号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string WoId { get; set; }
        /// <summary>
        /// 料站编号
        /// </summary>
        public string Stationno { get; set; }
        /// <summary>
        /// 料站表头编号
        /// </summary>
        public string MasterId { get; set; }
        /// <summary>
        /// 料号
        /// </summary>
        public string Kpnumber { get; set; }
        /// <summary>
        /// 料号描述
        /// </summary>
        public string Kpdesc { get; set; }
        /// <summary>
        /// 是否主料
        /// </summary>
        public bool Kpdistinct { get; set; }
        /// <summary>
        /// 替代组
        /// </summary>
        public string Replacegroup { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priorityclass { get; set; }
        /// <summary>
        /// 贴片位置
        /// </summary>
        public string Loction { get; set; }
        /// <summary>
        /// 记录日期
        /// </summary>
        //private DateTime Recdate;

        public string Reserve1 { get; set; }
        public string Reserve { get; set; }

    }
}
