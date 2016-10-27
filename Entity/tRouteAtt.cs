using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tRouteAtt
    {
        /// <summary>
        /// 流程编号
        /// </summary>
        public string routgroupId { get; set; }
        /// <summary>
        /// xml内容
        /// </summary>
        public string routgroupxmlContent { get; set; }
        /// <summary>
        /// 流程描述
        /// </summary>
        public string routgroupdesc { get; set; }

        private List<tRouteInfo> _lsRouteInfo = new List<tRouteInfo>();
        /// <summary>
        /// 流程下的工艺集合
        /// </summary>
        public List<tRouteInfo> LsRouteInfo
        {
            get { return _lsRouteInfo; }
            set { _lsRouteInfo = value; }
        }
    }
}
