using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tWipTrackingTable
    {
        string _Default = "NA";
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
        string _cartonnumber = "NA";
        public string cartonnumber
        {
            get { return _cartonnumber; }
            set { _cartonnumber = value; }
        }

        /// <summary>
        /// 产品Tray盘号
        /// </summary>
        string _TrayNO = "NA";
        public string TrayNO
        {
            get { return _TrayNO; }
            set { _TrayNO = value; }
        }

        /// <summary>
        /// 产品栈板号
        /// </summary>
        string _palletnumber = "NA";
        public string palletnumber
        {
            get { return _palletnumber; }
            set { _palletnumber = value; }
        }

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
        string _mcartonnumbr = "NA";
        public string mcartonnumbr
        {
            get { return _mcartonnumbr; }
            set { _mcartonnumbr = value; }
        }

        /// <summary>
        /// 产品客户栈板号
        /// </summary>       
        public string mpalletnumber
        {
            get { return _Default; }
            set { _Default = value; }
        }

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
