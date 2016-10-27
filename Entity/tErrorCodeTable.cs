using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tErrorCodeTable
    {
        /// <summary>
        /// 错误代码
        /// </summary>
       public string ErrorCode { get; set; }

       /// <summary>
       /// 英文描述
       /// </summary>
       public string ErrorDesc { get; set; }

       /// <summary>
       /// 中文描述
       /// </summary>
       public string ErrorDesc2 { get; set; }


       /// <summary>
       /// 时间
       /// </summary>
       public DateTime RecDate { get; set; }
    }
}
