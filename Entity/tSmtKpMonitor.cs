using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tSmtKpMonitor
    {
        public string kpmonitorId { get; set; }
        public string masterId { get; set; }
        public string woId { get; set; }
        public string machineId { get; set; }
        public string stationno { get; set; }
        public int cdata { get; set; }
        public string kpnumber { get; set; }
        public string scarcitytime { get; set; }
        public string scarcityuser { get; set; }
        public string supplytime { get; set; }
        public string supplyuser { get; set; }
        public int flag { get; set; }
        public int qty { get; set; }
        public string vendercode { get; set; }
        public string datecode { get; set; }
        public string lotId { get; set; }
        string _trsn = "";
        public string trsn
        {
            get { return _trsn; }
            set { _trsn = value; }
        }

        /// <summary>
        /// 是否显示总数
        /// </summary>
        public bool ShowTatol { get; set; }
    }
}
