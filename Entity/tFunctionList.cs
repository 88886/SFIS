using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tFunctionList
    {
        /// <summary>
        /// 程序功能编号
        /// </summary>
        public string funId { get; set; }
        /// <summary>
        /// 程序编号
        /// </summary>
        public string progid { get; set; }
        /// <summary>
        /// 程序功能名称
        /// </summary>
        public string funname { get; set; }
        /// <summary>
        /// 程序功能描述
        /// </summary>
        public string fundesc { get; set; }
    }
}
