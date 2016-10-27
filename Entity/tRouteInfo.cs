using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tRouteInfo
    {
        /// <summary>
        /// 流程编号
        /// </summary>
        public string routgroupId { get; set; }
        /// <summary>
        /// 工艺编号
        /// </summary>
        public string craftId { get; set; }
        /// <summary>
        /// 下一个工艺编号
        /// </summary>
        public string nextrouteId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int seq { get; set; }
        /// <summary>
        /// 状态跳转(区分是正常途程还是维修途程)
        /// </summary>
        public int station_flag { get; set; }
        /// <summary>
        /// 入口站或出口站
        /// </summary>
        public string routedesc { get; set; }

        /// <summary>
        /// 工艺名
        /// </summary>
        public string CraftName { get; set; }

        /// <summary>
        /// 下一工艺名
        /// </summary>
        public string NextCtaftName { get; set; }

        private List<Entity.tRoutCraftparameter> _lsRouteCraftparameter = new List<tRoutCraftparameter>();

        /// <summary>
        /// 流程下工艺的工艺项目和参数
        /// </summary>
        public List<Entity.tRoutCraftparameter> LsRouteCraftparameter
        {
            get { return _lsRouteCraftparameter; }
            set { _lsRouteCraftparameter = value; }
        }

        /// <summary>
        /// 成品料号2013-3-14
        /// </summary>
        public string Partnumber { get; set; }

        /// <summary>
        /// 产品型号2013-3-14
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// BOM 编号2013-3-14
        /// </summary>
        public string BomNumber { get; set; }


    }
}
