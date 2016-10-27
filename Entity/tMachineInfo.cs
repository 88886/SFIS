using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tMachineInfo
    {
        /// <summary>
        /// 机器/工站编号
        /// </summary>
        public string machineId { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string fixtureId { get; set; }
        /// <summary>
        /// 机器/工站描述
        /// </summary>
        public string machinedesc { get; set; }
        /// <summary>
        /// IP地址1
        /// </summary>
        public string ipaddress1 { get; set; }
        /// <summary>
        /// IP地址2
        /// </summary>
        public string ipaddress2 { get; set; }
        /// <summary>
        /// IP地址3
        /// </summary>
        public string ipaddress3 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// 线体编号
        /// </summary>
        public string lineId { get; set; }
       
    }
}
