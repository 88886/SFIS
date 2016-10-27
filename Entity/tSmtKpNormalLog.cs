using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tSmtKpNormalLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 料站表头Id
        /// </summary>
        public string masterId { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string woId { get; set; }
        /// <summary>
        /// PCB面
        /// </summary>
        public string pcbside { get; set; }
        /// <summary>
        /// 机器编号
        /// </summary>
        public string machineId { get; set; }
        /// <summary>
        /// 料站编号
        /// </summary>
        public string stationId { get; set; }
        /// <summary>
        /// Feeder编号
        /// </summary>
        string _feederId = "NA";
        public string feederId
        {
            get { return _feederId; }
            set { _feederId = value; }
        }
        /// <summary>
        /// 批次号
        /// </summary>
        public string lotId { get; set; }
        /// <summary>
        /// 料号
        /// </summary>
        public string kpnumber { get; set; }

        string _kp_sn = "NA";
        public string kp_sn
        {
            get { return _kp_sn; }
            set { _kp_sn = value; }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime inputtime { get; set; }
        /// <summary>
        /// 命令
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 厂商代码
        /// </summary>
        public string vendercode { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int lotQty { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string modelname { get; set; }
        /// <summary>
        /// DateCode
        /// </summary>
        public string datecode { get; set; }


        /// <summary>
        /// 是否显示总数
        /// </summary>
        public bool ShowTotal { get; set; }
    }
}
