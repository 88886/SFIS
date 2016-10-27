using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public   class tPrivliegeTable
    {
        /// <summary>
        /// 权限
        /// </summary>
        public string Fun {get;set;}

        /// <summary>
        /// 权限标记
        /// </summary>
        public int Privliege { get; set; }

        /// <summary>
        /// 程式名称
        /// </summary>
        public string PrgName { get; set; }
    }
}
