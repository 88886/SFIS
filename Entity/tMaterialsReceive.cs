using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tMaterialsReceive
    {
       /// <summary>
       /// 唯一序列号
       /// </summary>
       public string Trsn { get; set; }

       /// <summary>
       /// 采购订单号
       /// </summary>
       public string PO { get; set; }

       /// <summary>
       /// 料号
       /// </summary>
       public string Kpnumber { get; set; }

       /// <summary>
       /// 数量
       /// </summary>
       public int Qty { get; set; }

       /// <summary>
       /// 状态
       /// </summary>
       public int Status { get; set; }

       /// <summary>
       /// 标志位
       /// </summary>
       public string Flag { get; set; }

       
    }
}
