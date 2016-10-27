using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tz_whs_move_storeTable
    {

        /// <summary>
        /// 移库单号
        /// </summary>
        public string MOVE_STORE_ID { get; set; }

        /// <summary>
        /// 料号
        /// </summary>
        public string PARTNUMBER { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string PRODUCTNAME { get; set; }

        /// <summary>
        /// 移库数量
        /// </summary>
        public string QTY {get;set;}

        /// <summary>
        /// 移出库
        /// </summary>
        public string MOVE_STORE {get;set;}

        /// <summary>
        /// 移人库
        /// </summary>
         public string IMMIGRATION_STORE {get;set;}

        /// <summary>
       /// 申请时间
       /// </summary>
       public DateTime RECDATE { get; set; }
       /// <summary>
       /// 移库单申请人
       /// </summary>
       public string USERID { get; set; }
  
    }
}
