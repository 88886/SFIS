using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class tFixtureRegist
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string FixtureRegistId{get;set;}
        /// <summary>
        /// 设备编号
        /// </summary>
        public string FixtureId{get;set;}
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId{get;set;}
        /// <summary>
        /// 维护项目
        /// </summary>
        public string FixtureItem{get;set;}
        /// <summary>
        /// 维护内容
        /// </summary>
        public string FixtureContext { get; set; }
        /// <summary>
        /// 维护开始日期
        /// </summary>
        public string FixtureStartdate{get;set;}
        /// <summary>
        /// 维护结束日期
        /// </summary>
        public string FixtureEnddate{get;set;}
        /// <summary>
        /// 备注
        /// </summary>
        public string FixtureNote{get;set;}

    }
}
