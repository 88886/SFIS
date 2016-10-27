using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
  public  class tPalletInfoTable
    {
        /// <summary>
        /// 工单编号
        /// </summary>
        public string woId { get; set; }

        /// <summary>
        /// 栈板或Carton号码
        /// </summary>
        public string PalletNumber { get; set; }

        /// <summary>
        /// 线别
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// 产品料号
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// 包装类型(Carton,tray,pallet)
        /// </summary>
        public int Packtype { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 关闭标记
        /// </summary>
        public int CloseFlag { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Recdate { get; set; }

        /// <summary>
        /// 电脑名称
        /// </summary>
        public string Computer { get; set; }
    }
}
