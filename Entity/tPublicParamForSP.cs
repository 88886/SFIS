using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tPublicParamForSP
    {
        /// <summary>
        /// 输入参数
        /// </summary>
        public string DATA { get; set; }

       /// <summary>
       /// 当前站位
       /// </summary>
        public string MYGROUP { get; set; }

       /// <summary>
       /// 途程代码
       /// </summary>
        public string ROUTECODE { get; set; }

       /// <summary>
       /// 错误标记
       /// </summary>
        public string ERRORFLAG { get; set; }

       /// <summary>
       /// 产品当前站
       /// </summary>
        public string LOCSTATION { get; set; }

       /// <summary>
       /// 下一站
       /// </summary>
        public string NEXTSTATION { get; set; }

       /// <summary>
       /// 工单
       /// </summary>
        public string woId { get; set; }

       /// <summary>
       /// 结束途程
       /// </summary>
        public string ENDGROUP { get; set; }

       /// <summary>
       /// 线体
       /// </summary>
        public string LINE { get;set; }

       /// <summary>
       /// 人员工号
       /// </summary>
        public string EMPNO { get; set; }

    }
}
