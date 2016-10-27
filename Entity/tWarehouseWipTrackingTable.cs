using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tWarehouseWipTrackingTable
    {
      
       /// <summary>
       /// 唯一条码
       /// </summary>
       public string Esn { get ;set;}

       /// <summary>
       /// 栈板号
       /// </summary>
       public string PalletNumber { get; set; }
       /// <summary>
       /// 客户栈板号
       /// </summary>
       public string mPalletNumber { get; set; }

       /// <summary>
       /// 产品箱号
       /// </summary>
       public string CartonNumber { get; set; }

       /// <summary>
       /// 客户产品箱号
       /// </summary>
       public string mCartonNumber { get; set; }

       /// <summary>
       /// Tray
       /// </summary>
       public string Tray { get; set; }

       /// <summary>
       /// 产品料号
       /// </summary>
       public string PartNumber { get; set; }

       /// <summary>
       /// 状态
       /// </summary>
       public string Status { get; set; }


       /// <summary>
       /// 入库单号
       /// </summary>
       public string lotin { get; set; }

       /// <summary>
       /// 仓库编号
       /// </summary>
       public string storehouseId { get; set; }
     
     /// <summary>
       /// 库位编号
     /// </summary>         
       public string locId { get; set; }

       /// <summary>
       /// 用户
       /// </summary>
       public string userid { get; set; }

       /// <summary>
       /// 出库.出货批次
       /// </summary>
       public string lotout { get; set; }

       /// <summary>
       /// 时间
       /// </summary>
       public DateTime recdate { get; set; }


       //2013-4-27
       /// <summary>
       /// 标志位
       /// </summary>
       public string mFlag { get; set; }
       
       /// <summary>
       /// 工单
       /// </summary>
       public string WoId { get; set; }
       
       /// <summary>
       /// 客户编号
       /// </summary>
       public string CustomerId { get; set; }

       /// <summary>
       /// 查询开始时间
       /// </summary>
       public string DateStart { get; set; }
       /// <summary>
       /// 查询结束时间
       /// </summary>
       public string DateEnd { get; set; }

       /// <summary>
       /// SAP单号
       /// </summary>
       public string SapCode { get; set; }

       /// <summary>
       /// 出库地址
       /// </summary>
       public string Address { get; set; }


    }
}
