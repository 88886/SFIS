using System;
using System.Collections.Generic;
using System.Text;

namespace FrmBLL
{
   public class DayWeek
    {
       /// <summary>
       /// 获取一个月的第一天
       /// </summary>
       /// <param name="datetime"></param>
       /// <returns></returns>
        public static DateTime FirstDayOfMonth(DateTime datetime) 
        { 
            return datetime.AddDays(1 - datetime.Day);
        }
       /// <summary>
       /// 获取一个月的最后一天
       /// </summary>
       /// <param name="datetime"></param>
       /// <returns></returns>
        public static DateTime LastDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }


        /// <summary>
        /// 求当前日期是一年的中第几周
        /// </summary>
        /// <param name="curDay"></param>
        /// <returns></returns>
        public static int WeekOfYear(DateTime curDay)
        {
            int firstdayofweek = Convert.ToInt32(Convert.ToDateTime(curDay.Year.ToString() + "- " + "1-1 ").DayOfWeek);
            int days = curDay.DayOfYear;
            int daysOutOneWeek = days - (7 - firstdayofweek);
            if (daysOutOneWeek <= 0)
            {
                return 1;
            }
            else
            {
                int weeks = daysOutOneWeek / 7;
                if (daysOutOneWeek % 7 != 0)
                    weeks++;
                return weeks + 1;
            }
        }

        /// <summary>
        /// 求某年有多少周
        /// </summary>
        /// <param name="strYear"></param>
        /// <returns></returns>
        public static int GetYearWeekCount(int strYear)
        {
            System.DateTime fDt = DateTime.Parse(strYear.ToString() + "-01-01");
            int k = Convert.ToInt32(fDt.DayOfWeek);//得到该年的第一天是周几 
            if (k == 1)
            {
                int countDay = fDt.AddYears(1).AddDays(-1).DayOfYear;
                int countWeek = countDay / 7 + 1;
                return countWeek;
            }
            else
            {
                int countDay = fDt.AddYears(1).AddDays(-1).DayOfYear;
                int countWeek = countDay / 7 + 2;
                return countWeek;
            }
        }

        /// <summary>
        /// 得到一年中的某周的起始日和截止日
        /// </summary>
        /// <param name="nYear">年 nYear</param>
        /// <param name="nNumWeek">周数 nNumWeek</param>
        /// <param name="dtWeekStart">周始 out dtWeekStart</param>
        /// <param name="dtWeekeEnd">周终 out dtWeekeEnd</param>
        public static void GetWeek(int nYear, int nNumWeek, out   DateTime dtWeekStart, out   DateTime dtWeekeEnd)
        {
            DateTime dt = new DateTime(nYear, 1, 1);
            dt = dt + new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
            dtWeekStart = dt.AddDays(-(int)dt.DayOfWeek + (int)DayOfWeek.Monday);
            dtWeekeEnd = dt.AddDays((int)DayOfWeek.Saturday - (int)dt.DayOfWeek + 1);
        }

       /// <summary>
       /// 检测当前日期是否为该周的第一天
       /// </summary>
       /// <returns></returns>
        public static bool ChkStartWeek(DateTime _date)
        {
            DateTime startWeek = new DateTime();
            DateTime endWeek = new DateTime();
            GetWeek(_date.Year, WeekOfYear(_date), out startWeek, out endWeek);
            if (startWeek.ToString("yyyy/MM/dd") == _date.ToString("yyyy/MM/dd"))
                return true;
            else
                return false;
        }
    }
}
