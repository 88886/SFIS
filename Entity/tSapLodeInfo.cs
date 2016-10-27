using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tSapLodeInfo
    {

        //2013-8-2
        /// <summary>
        /// 收货人
        /// </summary>
        public string ContactPerson { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 料号
        /// </summary>
        public string Partnumber { get; set; }

        /// <summary>
        /// 料号描述
        /// </summary>
        public string ProductDesc { get; set; }

        /// <summary>
        /// SAP单号
        /// </summary>
        public string SAPCode { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int QTY { get; set; }

        /// <summary>
        /// 出货地点
        /// </summary>
        public string SapWarehouse { get; set; }

        /// <summary>
        /// 出货批次
        /// </summary>
        public string SFCcode { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        string _Address = "NA";
        /// <summary>
        /// 合同编号
        /// </summary>
        public string Contractno
        {
            get { return _Contractno; }
            set { _Contractno = value; }
        }

        string _Contractno = "NA";

        /// <summary>
        /// 送达方信息
        /// </summary>
        public string Customername2
        {
            get { return _Customername2; }
            set { _Customername2 = value; }
        }

        string _Customername2="NA";

       
    }
}
