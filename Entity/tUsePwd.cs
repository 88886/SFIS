using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tUsePwd
    {
        /// <summary>
        /// 工单号
        /// </summary>
        public string woId { get; set; }
        /// <summary>
        /// MAC号
        /// </summary>
        public string mac { get; set; }
        /// <summary>
        /// 密码类型
        /// </summary>
        public string pwdtype { get; set; }
        /// <summary>
        /// 密码的值
        /// </summary>
        public string pwdval { get; set; }
    }
}
