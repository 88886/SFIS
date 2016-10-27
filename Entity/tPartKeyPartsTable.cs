using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tPartKeyPartsTable
    {
       /// <summary>
        /// 主件
        /// </summary>
       public Guid idx { get; set; }

       /// <summary>
        /// 成品料号
        /// </summary>
       public string PartNumber { get; set; }

       /// <summary>
        ///物料料号
        /// </summary>
       public string KpNumber { get; set; }
    }
}
