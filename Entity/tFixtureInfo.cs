using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tFixtureInfo
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        private string _FixtureId="";
        public string FixtureId
        {
            get { return _FixtureId; }
            set { _FixtureId = value; }
        }  
        /// <summary>
        /// 设备名称
        /// </summary>
        public string FixtureName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string FixtureType { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public int FixtureState { get; set; }
        /// <summary>
        /// 启用日期
        /// </summary>
        public string FixtureBegingdate { get; set; }
        /// <summary>
        /// 保养日期
        /// </summary>
        public string FixtureMaintaindate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string FixtureNote { get; set; }
        /// <summary>
        /// 财产编号
        /// </summary>
        public string Assetscode { get; set; }
        /// <summary>
        /// 铭牌
        /// </summary>
        public string Nameplate{get;set;}
        /// <summary>
        /// 制造商
        /// </summary>
        public string Manufacturer{get;set;}
        /// <summary>
        /// 设备型号
        /// </summary>
        public string FixtureSize{get;set;}
 

    }
}
