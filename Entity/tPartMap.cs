using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tPartMap
    {
        /// <summary>
        /// 新物料料号
        /// </summary>
        public string kpnumber { get; set; }
        /// <summary>
        /// 旧物料料号
        /// </summary>
        public string selfkpnumber { get; set; }
        /// <summary>
        /// 物料描述
        /// </summary>
        public string kpdesc { get; set; }
        /// <summary>
        /// 原厂物料编号
        /// </summary>
        public string venkp { get; set; }

       
    }
}
