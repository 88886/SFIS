using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tScrapInfoTable
   {



       /// <summary>
       /// 产品条码
       /// </summary>
       public string ESN { get; set; }

       /// <summary>
       /// 人员工号
       /// </summary>
       public string UserId { get; set; }

       /// <summary>
       /// 产品料号
       /// </summary>
       public string PartNumber { get; set; }

       /// <summary>
       /// 工单
       /// </summary>
       public string woId { get; set; }

       /// <summary>
       /// 报废仓位
       /// </summary>
       public string StoreId { get; set; }

       /// <summary>
       /// 报废SFC单据号
       /// </summary>
       public string scrapno { get; set; }

       /// <summary>
       /// 报废原因
       /// </summary>
       public string ReasonCode { get; set; }

       /// <summary>
       /// 站位
       /// </summary>
       public string LocStation { get; set; }

       /// <summary>
       /// 备注
       /// </summary>
       public string MEMO { get; set; }



     
   }
}
