using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
  public   class tCheckTimeOut
    {

        /// <summary>
        /// 编号
        /// </summary>
      public string CHECK_NO { get; set; }
        /// <summary>
        /// 检查途程
        /// </summary>
      public string CHECK_ROUTE { get; set; }
        /// <summary>
        /// 退回途程
        /// </summary>
      public string ROLLBACK_ROUTE { get; set; }
        /// <summary>
        /// 超时时间(分钟)
        /// </summary>
      public string CHECK_TIMEOUT { get; set; }
      /// <summary>
      /// 休息时间
      /// </summary>
      public string REST_TIME { get; set; }
      /// <summary>
      ///人员工号
      /// </summary>
      public string USERID { get; set; }
      /// <summary>
      /// 修改时间
      /// </summary>
      public string RECORD_DATE { get; set; }
    }
}
