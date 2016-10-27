using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
  public  class tSmtKPMasterTable
    {
        /// <summary>
        /// MasterId
        /// </summary>
      public string MasterId { get; set; }

        /// <summary>
        /// 线别
        /// </summary>
      public string LineId { get; set; }

        /// <summary>
        /// 使用者工号
        /// </summary>
      public string UserId { get; set; }

        /// <summary>
        /// 产品料号         
        /// </summary>
      public string kpnumber { get; set; }

        /// <summary>
        ///  产品名称(Model_Name)
        /// </summary>
      public string ModelName { get; set;}

        /// <summary>
        /// Bom版本
        /// </summary>
      public string BomRev { get; set; }

        /// <summary>
        /// PCB面别
        /// </summary>
      public string PcbSide { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
      public string RecDate { get; set; }

        /// <summary>
        /// 预留1
        /// </summary>
      public string Reserve1 { get; set; }

      /// <summary>
      /// 预留2
      /// </summary>
      public string Reserve2 { get; set; }
      /// <summary>
      /// 审核人
      /// </summary>
      public string Auditinguser { get; set; }
      public enum KpMasterStatus
      {
          待审核 = 0,
          审核通过 = 1,
          审核失败 = 2
      }

    }
}
