using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Packing_Ctn
{
   public class OperateDB
    {
        

       public static List<string> Get_Line_List()
       {
           List<string> LsLine = new List<string>();
           DataTable dt = ReleaseData.arrByteToDataTable(refWebtLineInfo.Instance.GetAllLineInfo());
           DataTable dttemp = Function.DataTableToSort(dt,dt.Columns[0].ColumnName);
           foreach (DataRow dr in dttemp.Rows)
           {
               LsLine.Add(dr[0].ToString());
           }
           return LsLine;
       }
       public static DataTable Get_All_Station()
       {
            DataTable dt=ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo());
            DataTable temp = null;
            if (dt.Rows.Count > 0)
            {
                 temp =Function.getNewTable(dt, string.Format("TESTFLAG='{0}'", "0"));
            }
           DataTable dtLine = new DataTable();
           dtLine.Columns.Add("SECTION",typeof(string));
           dtLine.Columns.Add("GROUP",typeof(string));
           dtLine.Columns.Add("STATION",typeof(string));
           foreach (DataRow dr in temp.Rows)
           {
               dtLine.Rows.Add(dr["BEWORKSEG"].ToString(), dr["CRAFTNAME"].ToString(), dr["CRAFTPARAMETERURL"].ToString());
           }
           return Function.DataTableToSort(dtLine, "GROUP");
       }
       public static DataTable  Get_User_Info(string EMP_NO,string EMP_PWD)
       {//.GetJurUserInfoByIdandpwd(EMP_NO,EMP_PWD)
          return   ReleaseData.arrByteToDataTable(  refWebtUserInfo.Instance.GetUserInfo(EMP_NO,null,EMP_PWD));   

       }


    }
}
