using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
  public  class tPartStorehousehad
    {
       
        /// <summary>
        /// 物料唯一序列号Tr_Sn
        /// </summary>      
      string _Tr_Sn = "";
      public string Tr_Sn
      {
          get { return _Tr_Sn; }
          set {  _Tr_Sn=value; }
      }

        /// <summary>
        /// 物料料号
        /// </summary>      
      string _KpNumber = "NA";
      public string KpNumber
      {
          get { return _KpNumber; }
          set { _KpNumber = value; }
      }

        /// <summary>
        /// 厂商代码
        /// </summary> 
      string _VenderCode = "NA";
      public string VenderCode
      {
          get { return _VenderCode; }
          set { _VenderCode = value; }
      }
        /// <summary>
        /// 生产周期
        /// </summary> 
      string _DateCode = "NA";
      public string DateCode
      {
          get { return _DateCode; }
          set { _DateCode = value; }
      } 

        /// <summary>
        ///生产批次
        /// </summary> 
      string _LotId = "NA";
      public string LotId
      {
          get { return _LotId; }
          set { _LotId = value; }
      } 

        /// <summary>
        /// 入库数量
        /// </summary> 
      int _QTY = 0;
      public int QTY
      {
          get { return _QTY; }
          set { _QTY = value; }
      }

      /// <summary>
      /// 出库数量
      /// </summary> 
      int _OUTQTY = 0;
      public int OUTQTY
      {
          get { return _OUTQTY; }
          set { _OUTQTY = value; }
      }
      /// <summary>
      /// 备注
      /// </summary> 
      string _Remark = "NA";
      public string Remark  {
          get { return _Remark; }
          set {  _Remark=value; }
      }

      /// <summary>
      /// 状态
      /// </summary> 
      int _status = 0;
      public int status //{ get; set; }
      {
          get { return _status; }
          set { _status = value; }
      }

      /// <summary>
      /// 时间
      /// </summary> 
      string _recdate = "NA";
      public string recdate
      {
          get { return _recdate; }
          set { _recdate = value; }
      }

      /// <summary>
      /// 仓位或料架
      /// </summary> 
      string _LocId = "NA";
      public string LocId
      {
          get { return _LocId; }
          set {  _LocId=value; }
      }

      /// <summary>
      /// 仓库编号   12-09-11加
      /// </summary>
      string _storehouseId = "NA";
      public string storehouseId
      {
          get { return _storehouseId; }
          set { _storehouseId=value; }
      }

      /// <summary>
      /// 人员工号
      /// </summary> 
      public string UserId { get; set; }

      #region  资料库无以下栏位,只供查询程式使用参数

      /// <summary>
      /// 出入库标记(只能填写 IN 或者是 OUT ,必须为大写)
      /// </summary> 
      public string FLAG { get; set; }

      /// <summary>
      /// 显示料号
      /// </summary> 
      public bool ShowPN { get; set; }

      /// <summary>
      /// 显示厂商代码
      /// </summary> 
      public bool ShowVC { get; set; }

      /// <summary>
      /// 显示生产周期
      /// </summary> 
      public bool ShowDC { get; set; }

      /// <summary>
      /// 显示生产批次
      /// </summary> 
      public bool ShowLC { get; set; }

      /// <summary>
      /// 显示库位
      /// </summary> 
      public bool ShowLoc { get; set; }

      #endregion

    }
}
