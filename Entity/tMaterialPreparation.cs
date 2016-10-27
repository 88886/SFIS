using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tMaterialPreparation
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string mpId { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string woId { get; set; }
        /// <summary>
        /// 成品料号
        /// </summary>
        public string partnumber { get; set; }
        /// <summary>
        /// 用户工号
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 料站编号
        /// </summary>
        public string stationno { get; set; }
        /// <summary>
        /// 料站表头编号
        /// </summary>
        public string masterId { get; set; }
        /// <summary>
        /// 组件料号
        /// </summary>
        public string kpnumber { get; set; }
        /// <summary>
        /// 组件描述
        /// </summary>
        public string kpdesc { get; set; }

        /// <summary>
        /// 所在的物料组(当有替代料时的分组)
        /// </summary>
        public string replacegroup { get; set; }
        /// <summary>
        /// 是否主料
        /// </summary>
        public bool kpdistinct { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string localtion { get; set; }
        /// <summary>
        /// bom版本
        /// </summary>
        public string bomver { get; set; }
        /// <summary>
        /// pcb面
        /// </summary>
        public string side { get; set; }
        /// <summary>
        /// Feeder类型
        /// </summary>
        public string feedertype { get; set; }
    }
}
