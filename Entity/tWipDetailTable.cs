using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tWipDetailTable
    {

        /// <summary>
        /// Rowid    
        /// </summary>
        public string idx { get; set; }

        /// <summary>
        ///  唯一条码
        /// </summary>
        public string ESN { get; set; }

        /// <summary>
        /// 工单
        /// </summary>
        public string WO { get; set; }

        /// <summary>
        /// 产品料号
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// 当前站位
        /// </summary>
        public string locstation { get; set; }

        /// <summary>
        /// 下一站,WIP_GROUP
        /// </summary>
        public string wipstation { get; set; }

        /// <summary>
        /// 优先级,下一制成
        /// </summary>
        public string nextstation { get; set; }

        /// <summary>
        /// 用户,使用者工号
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string recdate { get; set; }

        /// <summary>
        /// 错误标记,产品不良
        /// </summary>
        public string errflag { get; set; }

        /// <summary>
        /// 报废标记,产品报废
        /// </summary>
        public string scrapflag { get; set; }

        /// <summary>
        /// 产品箱号
        /// </summary>
        public string cartonnumber { get; set; }

        /// <summary>
        /// 产品Tray盘号
        /// </summary>
        public string TrayNO { get; set; }

        /// <summary>
        /// 产品栈板号
        /// </summary>
        public string palletnumber { get; set; }

        /// <summary>
        /// SN号码
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// MACID
        /// </summary>
        public string MAC { get; set; }

        /// <summary>
        /// 产品客户箱号
        /// </summary>
        public string mcartonnumbr { get; set; }

        /// <summary>
        /// 产品客户栈板号
        /// </summary>
        public string mpalletnumber { get; set; }

        /// <summary>
        /// 生产线别
        /// </summary>
        public string line { get; set; }

        /// <summary>
        /// 流程代码
        /// </summary>
        public string routgroupId { get; set; }

        /// <summary>
        /// 入库编号
        /// </summary>
        public string storenumber { get; set; }

    }
}
