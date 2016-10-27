using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    // [Serializable]
    public class T_WO_INFO
    {


        /// <summary>
        /// 工单号
        /// </summary>
        public string woId
        { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string poId
        { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int qty { get; set; }
        /// <summary>
        /// 工单状态
        /// </summary>
        public int wostate { get; set; }
        /// <summary>
        /// 建立人
        /// </summary>
        public string userId
        { get; set; }

        /// <summary>
        /// 成品料号
        /// </summary>
        public string partnumber
        { get; set; }

        /// <summary>
        /// 成品名
        /// </summary>
        public string ProductName
        { get; set; }

        /// <summary>
        /// BOM版本
        /// </summary>
        public string bomver
        { get; set; }
        /// <summary>
        /// 工艺入口站
        /// </summary>
        public string inputgroup
        { get; set; }
        /// <summary>
        /// 工艺出口站
        /// </summary>
        public string outputgroup
        { get; set; }
        /// <summary>
        /// 工单类型
        /// </summary>
        public string wotype
        { get; set; }
        /// <summary>
        /// SAP工单类型
        /// </summary>
        public string sapwotype
        { get; set; }
        /// <summary>
        /// 产品版本
        /// </summary>
        public string per
        { get; set; }
        /// <summary>
        /// BOM编号（组装用BOM）
        /// </summary>
        public string bomnumber
        { get; set; }
        /// <summary>
        /// 生产流程编号
        /// </summary>
        public string routgroupId { get; set; }
        /// <summary>
        /// 产出数
        /// </summary>
        public int outputqty { get; set; }
        /// <summary>
        /// 投入数
        /// </summary>
        public int inputqty { get; set; }
        /// <summary>
        /// 报废数
        /// </summary>
        public int scrapqty { get; set; }
        /// <summary>
        /// 密码来源
        /// </summary>
        public ecpwd cpwd { get; set; }
        /// <summary>
        /// ATE脚本
        /// </summary>
        public string strAteScript { get; set; }

        /// <summary>
        /// 软件版本
        /// </summary>
        public string sw_ver { get; set; }

        /// <summary>
        /// 硬件版本
        /// </summary>
        public string fw_ver { get; set; }

        /// <summary>
        /// 网标前缀
        /// </summary>
        public string nal_prefix { get; set; }

        /// <summary>
        /// 检查BI站
        /// </summary>
        public string CHK_BI_ROUTE { get; set; }

        /// <summary>
        /// BI率
        /// </summary>
        public string BI_Proportion { get; set; }

        /// <summary>
        /// BI检查提示率
        /// </summary>
        public string BI_Warning { get; set; }

        /// <summary>
        /// 包装站检查标号
        /// </summary>
        public string CHECK_NO { get; set; }

        public enum ecpwd
        {
            PROG,
            FILE,
            USERDEF
        } 

        

        public enum WoInfoColumns
        {
            /// <summary>
            /// 工单号
            /// </summary>
            woId,
            /// <summary>
            /// 工单数量
            /// </summary>
            qty,
            /// <summary>
            /// 工单状态
            /// </summary>
            wostate,
            /// <summary>
            /// 建立人
            /// </summary>
            userId,
            /// <summary>
            /// 料号
            /// </summary>
            partnumber,
            /// <summary>
            /// BOM版本
            /// </summary>
            bomver,
            /// <summary>
            /// 工单投入站
            /// </summary>
            inputgroup,
            /// <summary>
            /// 工单出口站
            /// </summary>
            outputgroup,
            /// <summary>
            /// 工单生产使用的流程号
            /// </summary>
            routgroupId,
        }

        ///// <summary>
        ///// 工单类型
        ///// </summary>
        //public enum SnType
        //{
        //    正常工单 = 0,
        //    重工工单,
        //    RAM工单,
        //    试产工单,
        //    SMT工单,
        //    组包工单,
        //    外包工单
        //}
    }
}
