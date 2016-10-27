using System;
using System.Collections.Generic;
using System.Text;
using RefWebService_BLL;
using System.Runtime.InteropServices;

namespace FrmBLL
{
   public class ModifyLocalTime
    {
       ModifyLocalTime()
       {
       }
       
       public static void ModifyTime()
       {
         SetTime(RefWebService_BLL.refWebtGetServersTime.Instance.GetServersTime());
       }     

       #region 修改本地系统时间
       [DllImport("Kernel32.dll")]
       private extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

       [DllImport("Kernel32.dll")]
       private extern static uint SetLocalTime(ref SYSTEMTIME lpSystemTime);

       [StructLayout(LayoutKind.Sequential)]
       private struct SYSTEMTIME
       {
           public ushort wYear;
           public ushort wMonth;
           public ushort wDayOfWeek;
           public ushort wDay;
           public ushort wHour;
           public ushort wMinute;
           public ushort wSecond;
           public ushort wMilliseconds;
       }

       public static void SetTime(DateTime SqlServerTime)
       {
           SYSTEMTIME st = new SYSTEMTIME();
           st.wYear = Convert.ToUInt16(SqlServerTime.Year);
           st.wMonth = Convert.ToUInt16(SqlServerTime.Month);
           st.wDay = Convert.ToUInt16(SqlServerTime.Day);
           st.wHour = Convert.ToUInt16(SqlServerTime.Hour);
           st.wMilliseconds = Convert.ToUInt16(SqlServerTime.Millisecond);
           st.wMinute = Convert.ToUInt16(SqlServerTime.Minute);
           st.wSecond = Convert.ToUInt16(SqlServerTime.Second);
           SetLocalTime(ref st);
       }

       #endregion
    }
}
