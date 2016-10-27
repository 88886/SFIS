using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class tPackParametersTable
    {
       /// <summary>
        /// Rowid编号
        /// </summary>
       public string Rowid {get;set;}


        /// <summary>
        /// 产品料号
        /// </summary>
       public string PartNumber {get;set;}


         /// <summary>
        /// 产品版本
        /// </summary>
       public string VersionCode {get;set;}

        /// <summary>
        ///  Tray容量
        /// </summary>
       public int TrayQty {get;set;}

        /// <summary>
        /// Carton容量(等于装多少个Tray)
        /// </summary>
       public int CartonQty {get;set;}

      /// <summary>
        /// 栈板容量(等于装多少个Carton)
        /// </summary>
       public int PalletQty {get;set;}

        /// <summary>
        /// 时间
        /// </summary>
       public DateTime RecDate { get; set; }
    }
}
