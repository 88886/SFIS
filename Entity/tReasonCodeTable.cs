using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tReasonCodeTable
    {

       /// <summary>
        /// 原因代码
        /// </summary>
       public string ReasonCode { get; set; }

       /// <summary>
        /// 原因类型
        /// </summary>
       public string ReasonType { get; set; }

       /// <summary>
        /// 英文描述
        /// </summary>
       public string ReasonDesc { get; set; }

       /// <summary>
        /// 中文描述
        /// </summary>
       public string ReasonDesc2 { get; set; }

       
       /// <summary>
        /// 责任站位
        /// </summary>
       public string DutyStation { get; set; }

       
       /// <summary>
        /// 时间
        /// </summary>
       public DateTime RecDate { get; set; }


    }
}
