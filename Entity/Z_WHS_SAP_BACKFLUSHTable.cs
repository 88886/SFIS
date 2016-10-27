using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class Z_WHS_SAP_BACKFLUSHTable
    {
        /// <summary>
        /// 工单
        /// </summary>
       public string WOID { get; set; }
        /// <summary>
        /// 料号
        /// </summary>
       public string PARTNUMBER { get; set; }

       /// <summary>
       /// 产品型号
       /// </summary>
       public string PRODUCTNAME { get; set; }

       /// <summary>
       /// 入库批次
       /// </summary>
       public string LOTIN { get; set; }

       /// <summary>
       /// 入库数量
       /// </summary>
       public int LOTIN_QTY { get; set; }

       /// <summary>
       /// 出库批次
       /// </summary>
       public string LOTOUT { get; set; }

       /// <summary>
       /// 出库数量
       /// </summary>
       public int LOTOUT_QTY { get; set; }


       /// <summary>
       /// 出入库时间
       /// </summary>
       public DateTime RECDATE { get; set; }

       /// <summary>
       /// SAP厂区代码
       /// </summary>
       public string PLANT { get; set; }

     /// <summary>
       /// 搬迁标记
       /// </summary>
       public string MOVE_TYPE { get; set; }

       /// <summary>
       /// 上传SAP结果
       /// </summary>
       public string UPLOAD_FLAG { get; set; }      

       /// <summary>
       /// 上传SAP时间
       /// </summary>
       public DateTime UPLOAD_DATE { get; set; }



    }
}
