using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class B_SNRULE_PARTNUMBER_Table
    {
        /// <summary>
        /// 产品料号
        /// </summary>
        public string PARTNUMBER { get; set; }
        /// <summary>
        /// 客户类型
        /// </summary>
        public string RULE_TYPE { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string VERSION_CODE { get; set; }
        /// <summary>
        /// 客户料号
        /// </summary>
        public string CUST_PARTNUBMER { get; set; }
        /// <summary>
        /// 客户版本
        /// </summary>
        public string CUST_VERSION_CODE { get; set; }
        /// <summary>
        /// 客户料号描述
        /// </summary>
        public string CUST_PARTNUMBER_DESC { get; set; }
        public string UPCEANDATA { get; set; }
        /// <summary>
        /// 客户SN前缀
        /// </summary>
        public string CUST_SN_PREFIX { get; set; }
        /// <summary>
        /// 客户厂商代码
        /// </summary>
        public string CUST_VENDER_CODE { get; set; }
        /// <summary>
        /// 客户SN后缀
        /// </summary>
        public string CUST_SN_POSTFIX { get; set; }
        /// <summary>
        /// 客户SN长度
        /// </summary>
        public int CUST_SN_LENG { get; set; }
        /// <summary>
        /// 客户SN
        /// </summary>
        public string CUST_SN_STR { get; set; }
        /// <summary>
        /// 最后使用客户SN
        /// </summary>
        public string CUST_LAST_SN { get; set; }
        /// <summary>
        /// 客户BOX前缀
        /// </summary>
        public string CUST_BOX_PREFIX { get; set; }
        /// <summary>
        /// 客户BOX长度
        /// </summary>
        public int CUST_BOX_LENG { get; set; }
        /// <summary>
        /// 客户BOX STR
        /// </summary>
        public string CUST_BOX_STR { get; set; }
        /// <summary>
        /// 最后使用客户box号码
        /// </summary>
        public string CUST_LAST_BOX { get; set; }
        /// <summary>
        /// BOX条码档名
        /// </summary>
        public string BOX_LAB_NAME { get; set; }
        /// <summary>
        /// 客户CARTON前缀
        /// </summary>
        public string CUST_CARTON_PREFIX { get; set; }
        /// <summary>
        /// 客户CARONT后缀
        /// </summary>
        public string CUST_CARTON_POSTFIX { get; set; }
        /// <summary>
        /// 客户CARTON后缀
        /// </summary>
        public int CUST_CARTON_LENG { get; set; }
        /// <summary>
        /// 客户CARTON STR
        /// </summary>
        public string CUST_CARTON_STR { get; set; }
        /// <summary>
        /// 最后使用客户CARTON 号码
        /// </summary>
        public string CUST_LAST_CARTON { get; set; }
        /// <summary>
        /// 客户CARTON 条码档名
        /// </summary>
        public string CARTON_LAB_NAME { get; set; }
        /// <summary>
        /// 客户 PALLET 前缀
        /// </summary>
        public string CUST_PALLET_PREFIX { get; set; }
        /// <summary>
        /// 客户 PALLET 后缀
        /// </summary>
        public string CUST_PALLET_POSTFIX { get; set; }
        /// <summary>
        /// 客户栈板长度
        /// </summary>
        public int CUST_PALLET_LENG { get; set; }
        /// <summary>
        /// 客户栈板 STR
        /// </summary>
        public string CUST_PALLET_STR { get; set; }
        /// <summary>
        /// 最后使用客户栈板号码
        /// </summary>
        public string CUST_LAST_PALLET { get; set; }
        /// <summary>
        /// 客户栈板条码档名
        /// </summary>
        public string PALLET_LAB_NAME { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime IN_STATION_TIME { get; set; }
        /// <summary>
        /// 人员工号
        /// </summary>
        public string EMP_NO { get; set; }

    }
}
