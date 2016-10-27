using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tUserInfo
    {
        /// <summary>
        /// 用户工号(主键)
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public string rolecaption { get; set; }
        /// <summary>
        /// 所在部门
        /// </summary>
        public string deptname { get; set; }
        /// <summary>
        /// 所属工厂编号
        /// </summary>
        public string facId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string pwd { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string userphone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string useremail { get; set; }
        /// <summary>
        /// 用户状态:0停用；1:启用
        /// </summary>
        public bool userstatus { get; set; }

        /// <summary>
        /// 保存用户的权限信息(progid and funid)
        /// </summary>
        public System.Data.DataTable userPopList { get; set; }
    }
}
