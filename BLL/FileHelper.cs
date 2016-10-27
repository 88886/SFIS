using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using NLog;

namespace BLL
{
    
    public  class FileHelper
    {
        public FileHelper()
        {
            
        }

        private  Logger logger = LogManager.GetLogger("FileHelper");//加载LOG样式

        

        delegate void runEvent(string StrLog);
        runEvent deleevnet;
        public void Insert_DB_Log(string StrLog)
        {         
            deleevnet = new runEvent(Insert_Log);
            deleevnet.BeginInvoke(StrLog, null, null);           
        }
        private void Insert_Log(string StrLog)
        {
            try
            {
                logger.Info(StrLog);
            }
            catch (Exception ex)
            {
                Save_DB_Log(ex.Message);
            }
        }
        public void Insert_Trace(string StrLog)
        {
            try
            {
                logger.Trace(StrLog);
            }
            catch (Exception ex)
            {
                Save_DB_Log(ex.Message);
            }
        }
        public void Insert_Debug(string StrLog)
        {
            try
            {
                logger.Debug(StrLog);
            }
            catch (Exception ex)
            {
                Save_DB_Log(ex.Message);
            }
        } 

        private  void Save_DB_Log(string StrLog)
        {
            #region 存储失败日志在服务器
            try
            {
                string TodayDate = DateTime.Now.ToString("yyyyMMdd");
                string FilePatch = "D:\\LOG\\" + TodayDate;
                if (!Directory.Exists(FilePatch))
                    Directory.CreateDirectory(FilePatch);
                //如果文件a.txt存在就打开，不存在就新建 .append 是追加写 
                GregorianCalendar gc = new GregorianCalendar();
               // +gc.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString();
              
                string FileName=TodayDate+"01";
              if ( DateTime.Now.Hour>12)
                      FileName= TodayDate+"13";

              FileStream fst = new FileStream(string.Format("{0}\\{1}.log",FilePatch,  FileName), FileMode.Append);
                //写数据到a.txt格式 
                StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
                swt.WriteLine(StrLog + "  Time:" + DateTime.Now.ToString());
                swt.Close();
                fst.Close();            
            }
            catch 
            {
               
            }
            #endregion
        }

        delegate void runEvent_Execption(string StrLog);
        runEvent_Execption deleevnet_Execption;

        public void Insert_Exception_Log(string StrLog)
        {
            deleevnet_Execption = new runEvent_Execption(Save_Exception_Log);
            deleevnet_Execption.BeginInvoke(StrLog, null, null);     
                   
        }
        private void Save_Exception_Log(string StrLog)
        {
            try
            {
                logger.Error(StrLog);
            }
            catch (Exception ex)
            {
                Save_DB_Log(ex.Message);
            } 
        }
      
    }




}
