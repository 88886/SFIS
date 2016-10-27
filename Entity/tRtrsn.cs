using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tRtrsn
    {
        /// <summary>
        /// PO单号
        /// </summary>
        public string PO_ID { get; set; }
        /// <summary>
        /// 唯一条码编号
        /// </summary>
        public string TR_SN { get; set; }
        /// <summary>
        /// 物料料号
        /// </summary>
        public string KP_NO { get; set; }
        /// <summary>
        /// 物料描述
        /// </summary>
        public string KP_DESC { get; set; }
        /// <summary>
        /// 厂商代码
        /// </summary>
        public string VENDER_ID { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string VENDER_NAME { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public string DATE_CODE { get; set; }
        /// <summary>
        /// 生产批次
        /// </summary>
        public string LOT_CODE { get; set; }
        /// <summary>
        /// 料盘数量
        /// </summary>
        public string QTY { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string STOCK_ID { get; set; }
        /// <summary>
        /// 储位编码
        /// </summary>
        public string LOC_ID { get; set; }
        /// <summary>
        /// 移库单号
        /// </summary>
        public string TANSFER_NO { get; set; }
        /// <summary>
        /// 急料
        /// </summary>
        public string URGENT_PN { get; set; }
        /// <summary>
        /// 入库单号
        /// </summary>
        public string STOCK_NO { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public string STOCK_DATE { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string WOID { get; set; }
        /// <summary>
        /// 列印人员
        /// </summary>
        public string USER_ID { get; set; }
        /// <summary>
        /// 唯一条码状态
        /// </summary>
        public string STATUS { get; set; }
        /// <summary>
        /// 唯一条码先进先出DC
        /// </summary>
        public string FIFO_DC { get; set; }
    }
}

