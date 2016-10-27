using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class tProduct
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string sortname { get; set; }
        /// <summary>
        /// 成品料号
        /// </summary>
        public string partnumber { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string pdname { get; set; }
        /// <summary>
        /// 产品颜色
        /// </summary>
        public string pdcolor { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string pddes { get; set; }
        /// <summary>
        /// 其他信息
        /// </summary>
        public string other { get; set; }

        /// <summary>
        /// 产品SN
        /// </summary>
        public string Productsn { get; set; }
        /// <summary>
        /// 产品平台
        /// </summary>
        public string solution { get; set; }

        /// <summary>
        /// 条码长度
        /// </summary>
        public string BarcodeLen { get; set; }

        /// <summary>
        /// 网标前缀
        /// </summary>
        public string NALPREFIX { get; set; }
    }
}
