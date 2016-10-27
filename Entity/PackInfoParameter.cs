using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class PackInfoParameter
    {

       /// <summary>
       /// 输入产品SN
       /// </summary>
       public string DATA { get; set; }

       /// <summary>
       /// 输入产品SN
       /// </summary>
       public string ESN { get; set; }

       /// <summary>
       /// 线别
       /// </summary>
       public string Line { get; set; }

       /// <summary>
       /// 途程
       /// </summary>
       public string MyGroup { get; set; }

       /// <summary>
       /// 人员权限
       /// </summary>
       public string EMP { get; set; }

       /// <summary>
       /// Carton包装容量
       /// </summary>
       public string CartonCapacity { get; set; }

       /// <summary>
       /// 产品料号
       /// </summary>
       public string PartNumber { get; set; }

       /// <summary>
       /// 工单
       /// </summary>
       public string woId { get; set; }


       /// <summary>
       /// 产品版本
       /// </summary>
       public string VersionCode { get; set; }

       /// <summary>
       /// 途程代码
       /// </summary>
       public string RouteCode { get; set; }

       /// <summary>
       /// 错误标记
       /// </summary>
       public string ErrFlag { get; set; }

       /// <summary>
       /// 当前站位
       /// </summary>
       public string LocStation { get; set; }

       /// <summary>
       /// 下一个途程
       /// </summary>
       public string NextStation { get; set; }

       /// <summary>
       /// 途程的最后一站
       /// </summary>
       public string EndGroup { get; set; }

       /// <summary>
       /// 产品箱号
       /// </summary>
       public string CartonNo { get; set; }

       /// <summary>
       /// 产品客户箱号
       /// </summary>
       public string MCartonNo { get; set; }

       /// <summary>
       /// 产品栈板号
       /// </summary>
       public string PalletNo { get; set; }

       /// <summary>
       /// 产品客户栈板号
       /// </summary>
       public string MPalletNo { get; set; }

       /// <summary>
       /// 产品Tray
       /// </summary>
       public string TrayNo { get; set; }
    }
}
