using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
  public  class T_WO_BOM_INFO
    {
      /// <summary>
      /// 表的主ID
      /// </summary>
      public Guid wbiId { get; set; }
      /// <summary>
      /// 工单号
      /// </summary>
      public string woId { get; set; }
      /// <summary>
      /// 用户ID
      /// </summary>
      public string userId { get; set; }
      /// <summary>
      /// 成品料号
      /// </summary>
      public string partnumber { get; set; }
      /// <summary>
      /// 组件料号
      /// </summary>
      public string kpnumber { get; set; }
      /// <summary>
      /// 组件描述
      /// </summary>
      public string kpdesc { get; set; }
      /// <summary>
      /// 数量
      /// </summary>
      public int qty { get; set; }
      /// <summary>
      /// 制程段
      /// </summary>
      public string process { get; set; }
      /// <summary>
      /// 料表版本
      /// </summary>
      public string bomver { get; set; }
      /// <summary>
      /// 是否锁定
      /// </summary>
      public int blocked { get; set; }
    }
}
