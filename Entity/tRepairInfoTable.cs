using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tRepairInfoTable
    {

        /// <summary>
        /// Rowid
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 不良代码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 原因代码
        /// </summary>
        public string ReasonCode { get; set; }
        /// <summary>
        /// 产品唯一条码
        /// </summary>
        public string ESN { get; set; }

        /// <summary>
        /// 工单
        /// </summary>
        public string  woId { get; set; }

        /// <summary>
        /// 产品料号
        /// </summary>
        public string PartNumber  { get; set; }

        /// <summary>
        ///途程编号
        /// </summary>
        public string CraFtId { get; set; }

        /// <summary>
        /// 转入人员
        /// </summary>
        public string InputUser  { get; set; }

        /// <summary>
        /// 状态 0,待转入,1维修接收,2 维修转出,3生产接收
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime Recdate  { get; set; }

        /// <summary>
        /// 转出时间
        /// </summary>
        public DateTime OutDate  { get; set; }

        /// <summary>
        ///转入日期
        /// </summary>
        public DateTime InputDate  { get; set; }

        /// <summary>
        /// 维修人员 
        /// </summary>
        public string ReUser { get; set; }

         /// <summary>
        ///线别
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        ///零件位置
        /// </summary>
        public string Location  { get; set; }

        /// <summary>
        ///  备注
        /// </summary>
        public string Remark { get; set; }
          /// <summary>
        ///  转出途程
        /// </summary>
        public string OutcraftId { get; set; }
       

        /// <summary>
        ///  物料料号
        /// </summary>
        public string PartNo { get; set; }

        /// <summary>
        ///  厂商代码
        /// </summary>
        public string VenderCode { get; set; }

        /// <summary>
        ///  生产周期
        /// </summary>
        public string DateCode { get; set; }

        /// <summary>
        ///  生产批次
        /// </summary>
        public string LotCode { get; set; }


        /// <summary>
        ///  责任单位
        /// </summary>
        public string Duty { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get;set;}

        public bool Show_ClassDate { get; set; }
        public bool Show_PartNumber { get; set; }
        public bool Show_ProductName { get; set; }
        public bool Show_woId { get; set; }
        public bool Show_CraftId { get; set; }
        public bool Show_Line { get; set; }
        public bool Show_Location { get; set; }


    }
}
