using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tBomKeyPartTable
    {
        /// <summary>
        /// idx
        /// </summary>
        public Guid idx { get; set; }

        /// <summary>
        /// BOM编号
        /// </summary>
        public string BomNumber { get; set; }

        /// <summary>
        /// 物料料号
        /// </summary>
        public string KpNumber { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int KpRelation { get; set; }

        /// <summary>
        /// 途程站位
        /// </summary>
        public string Station { get; set; }

    }
}
