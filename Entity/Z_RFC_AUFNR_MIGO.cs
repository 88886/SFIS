using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Z_RFC_AUFNR_MIGO
    {
        /// <summary>
        /// SAP的T-code相关02
        /// </summary>
        public string GM_CODE { get; set; }

        /// <summary>
        /// PSTNG_DATE:过账日期
        /// </summary>
        public string PSTNG_DATE { get; set; }

        /// <summary>
        /// DOC_DATE:凭证日期
        /// </summary>
        public string DOC_DATE { get; set; }

        /// <summary>
        /// I_TYPE:出入库类型
        /// </summary>
        public string I_TYPE { get; set; }

        /// <summary>
        /// woId:工单
        /// </summary>
        public string woId { get; set; }

        /// <summary>
        /// PartNumber:料号
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// QTY数量
        /// </summary>
        public int QTY { get; set; }

        /// <summary>
        /// MVT_IND:默认F
        /// </summary>
        public string MVT_IND { get; set; }

        /// <summary>
        /// PLANT:默认2100
        /// </summary>
        public string PLANT { get; set; }

        /// <summary>
        /// MOVE_TYPE:101 成品入库,261 重工出库 601 成品出货
        /// </summary>
        public string MOVE_TYPE { get; set; }

        /// <summary>
        /// 人员工号
        /// </summary>
        public string EMP_NO { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string EMP_NAME { get; set; }

        /// <summary>
        /// 转出仓
        /// </summary>
        public string STGE_LOC { get; set; }

        /// <summary>
        /// 收货工厂
        /// </summary>
        public string MOVE_PLANT { get; set; }

        /// <summary>
        /// 收货库位
        /// </summary>
        public string MOVE_STLOC { get; set; }
    }
}
