using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tCustomer  //客户信息表
    {
        //customerId, customername, address, contactperson, phone, city
        /// <summary>
        /// 客户编号
        /// </summary>
        public string customerId { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string customername { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        ///联系人
        /// </summary>
        public string contactperson { get; set; }

        /// <summary>
        ///联系电话
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        ///所在城市
        /// </summary>
        public string city { get; set; }
    }
}
