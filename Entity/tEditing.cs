using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public  class tEditing
    {
        /// <summary>
        /// 当前正在被编辑项的名称
        /// </summary>
        public string funname { get; set; }
        /// <summary>
        /// 当前编辑人工号
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 当前编辑人名
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 当前正在编辑所属的项目
        /// </summary>
        public string prj { get; set; }
        /// <summary>
        /// 电脑MAC
        /// </summary>
        public string MAC_ADD  { get; set; }

        /// <summary>
        /// 电脑名
        /// </summary>
        public string PC_NAME  { get; set; }

    }
}
