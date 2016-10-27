using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tPartStorehousehadRecount
    {
        /// <summary>
      /// 工作日期
      /// 
       public string workdate { get; set; }

       /// <summary>
       /// 工作时段
       /// 
       public string worksection { get; set; }

       /// <summary>
       /// 料号
       /// </summary>
       public string kpnumber { get; set; }

       /// <summary>
       /// 厂商代码
       /// </summary> 
       public string vendercode { get; set; }

       /// <summary>
       ///生产周期
       /// </summary>
       public string datecode { get; set; }

       /// <summary>
       ///生产批次
       /// </summary>
       public string lotid { get ;set;}

        /// <summary>
        ///入库数量
        /// </summary>
       public int Inqty { get; set; }

        /// <summary>
        ///出库数
        /// </summary>
       public int Outqty { get; set; }

        /// <summary>
        ///生产班别日期
        /// </summary>
       public string classdate { get; set; }

        /// <summary>
        ///班别
        /// </summary>
       public string sclass { get; set; }


#region 数据库无此栏位

        /// <summary>
        ///开始日期
        /// </summary>
      public string StartDate{get;set;}

       /// <summary>
        ///结束日期
        /// </summary>
      public string EndDate { get;set;}

        /// <summary>
        ///开始时间
        /// </summary>
      public string StartTime { get; set; }

        /// <summary>
        ///结束时间
        /// </summary>
      public string EndTime { get; set; }

      /// <summary>
      ///是否显示料号
      /// </summary>
      public bool showpn { get; set; }

      /// <summary>
      ///是否显示厂商代码
      /// </summary>
      public bool showvc { get; set; }

      /// <summary>
      ///是否生产周期
      /// </summary>
      public bool showdc { get; set; }

      /// <summary>
      ///是否显示生产批次
      /// </summary>
      public bool showlc { get; set; }

      /// <summary>
      ///是否显示工作日期
      /// </summary>
      public bool showworkdate { get; set; }



#endregion
       
    }
}
