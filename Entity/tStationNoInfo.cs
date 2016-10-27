using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tStationNoInfo
    {
        /// <summary>
        /// 料站编号
        /// </summary>
        public string stationno { get; set; }
        /// <summary>
        /// 所属线体
        /// </summary>
        public string lineId { get; set; }
        /// <summary>
        /// 机器编号
        /// </summary>
        public string machineId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string des { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string stationspec { get; set; }
    }
}
