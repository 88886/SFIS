using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SystemObject;
using SrvComponent;
using System.Globalization;
using System.IO;

namespace BLL
{
    public partial class tSmtKpNormalLog
    {
       
        public tSmtKpNormalLog()
        {
            
        }
         //public void InsertSmtKpNormalLog(string userid,string masterid,string woId,string pcbside,string machine,string stationId,string lotId,
         //    string kpnumber,string data,string vendercode,int lotqty,string modelname,string datecode,string TrSn ,out string err)
        public void InsertSmtKpNormalLog(IDictionary<string,object> mst ,out string err)
        {         
            try
            {
              

                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);                        
                dp.AddData("sfcr.t_smt_kp_normal_log".ToUpper(), mst);
                err = "OK";
            }
            catch (Exception ex)
            {
                err=ex.Message;
                Insert_System_Log(ex.Message);
            }
        }  

        public System.Data.DataSet QuerytSmtKpNormallog(string dicstring,bool ShowTotal)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //string sSQL = "";
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);    
            int count = 0;
            string table = "sfcr.t_smt_kp_normal_log".ToUpper();
            string fieldlist = null;
            string filter = null;
            DataSet ds =null;
           
            if (ShowTotal == false)
            {
                //if (!string.IsNullOrEmpty(sknl.woId))
                //{
                //    sSQL = sSQL + " and woid=@woid";
                //    cmd.Parameters.Add("woid", MySqlDbType.VarChar, 50).Value = sknl.woId;
                //}
                //if (!string.IsNullOrEmpty(sknl.kpnumber))
                //{
                //    sSQL = sSQL + " and kpnumber=@kpnumber";
                //    cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 50).Value = sknl.kpnumber;

                //}
                //if (!string.IsNullOrEmpty(sknl.vendercode))
                //{
                //    sSQL = sSQL + " and vendercode=@vendercode";
                //    cmd.Parameters.Add("vendercode", MySqlDbType.VarChar, 50).Value = sknl.vendercode;
                //}
                //if (!string.IsNullOrEmpty(sknl.datecode))
                //{
                //    sSQL = sSQL + " and datecode=@datecode";
                //    cmd.Parameters.Add("datecode", MySqlDbType.VarChar, 50).Value = sknl.datecode;
                //}
                //if (!string.IsNullOrEmpty(sknl.lotId))
                //{
                //    sSQL = sSQL + " and lotid=@lotid";
                //    cmd.Parameters.Add("lotid", MySqlDbType.VarChar, 50).Value = sknl.lotId;
                //}

                //sSQL = sSQL.Substring(5, sSQL.Length - 5);
                //sSQL = "select userid as 用户,masterid as 料表编号,woid as 工单,machineid as 机器代码,stationid as 料站号,feederid as Feeder,kpnumber as 料号,modelname as 产品型号," +
                //     "vendercode as 厂商代码,datecode as 生产周期,lotid as 生产批次,lotqty as 数量,kp_sn as KP_SN,data as 上料命令,pcbside as PCB面,inputtime as 刷入时间 from sfcr.t_smt_kp_normal_log where " + sSQL;
                //cmd.CommandText = sSQL;



                fieldlist = "userid as 用户,masterid as 料表编号,woid as 工单,machineid as 机器代码,stationid as 料站号,feederid as Feeder,kpnumber as 料号,modelname as 产品型号," +
                      "vendercode as 厂商代码,datecode as 生产周期,lotid as 生产批次,lotqty as 数量,kp_sn as KP_SN,data as 上料命令,pcbside as PCB面,inputtime as 刷入时间";
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);                          
                ds = dp.GetData(table, fieldlist,  mst, out count);
                
            }
            else
            {
               
            
                //string sCON = "";
                //string sConName = "";


                //if (!string.IsNullOrEmpty(sknl.woId))
                //{
                //    sCON = sCON + " ,woid";
                //    sConName = sConName + " ,woid as 工单";
                //    sSQL = sSQL + " and woid=@woid";
                //    cmd.Parameters.Add("woid", MySqlDbType.VarChar, 50).Value = sknl.woId;
                //}
                //sCON = sCON + " ,kpnumber";
                //sConName = sConName + " ,kpnumber as 料号";
                //if (!string.IsNullOrEmpty(sknl.kpnumber))
                //{
                //    sSQL = sSQL + " and kpnumber=@kpnumber";
                //    cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 50).Value = sknl.kpnumber;
                //}
                //if (!string.IsNullOrEmpty(sknl.vendercode))
                //{
                //    sCON = sCON + " ,vendercode";
                //    sConName = sConName + " ,vendercode as 厂商代码";
                //    sSQL = sSQL + " and vendercode=@vendercode";
                //    cmd.Parameters.Add("vendercode", MySqlDbType.VarChar, 50).Value = sknl.vendercode;
                //}
                //if (!string.IsNullOrEmpty(sknl.datecode))
                //{
                //    sCON = sCON + " ,datecode";
                //    sConName = sConName + " ,datecode as 生产周期";
                //    sSQL = sSQL + " and datecode=@datecode";
                //    cmd.Parameters.Add("datecode", MySqlDbType.VarChar, 50).Value = sknl.datecode;

                //}
                //if (!string.IsNullOrEmpty(sknl.lotId))
                //{
                //    sCON = sCON + " ,lotid";
                //    sConName = sConName + " ,lotid as 生产批次";
                //    sSQL = sSQL + " and lotid=@lotid";
                //    cmd.Parameters.Add("lotid", MySqlDbType.VarChar, 50).Value = sknl.lotId;
                //}

                //sCON = sCON.Substring(2, sCON.Length - 2);
                //sConName = sConName.Substring(2, sConName.Length - 2);
                //sSQL = sSQL.Substring(5, sSQL.Length - 5);

                //sSQL = "select " + sConName + ",sum(lotqty) 数量 from sfcr.t_smt_kp_normal_log where  " + sSQL + " group by " + sCON;
                //cmd.CommandText = sSQL;
                IDictionary<string, object> mstin = new Dictionary<string, object>();
                string group = null;
                int x = -1;
                if (mst.ContainsKey("WOID"))
                {
                     x++;
                    group = " ,WOID"; 
                    fieldlist += " ,WOID as 工单";
                    filter += " AND WOID={" + x.ToString() + "}";
                    mstin.Add("WOID", mst["WOID"]);
                }
                group += " ,KPNUMBER";
                fieldlist += " ,KPNUMBER as 料号";
                if (mst.ContainsKey("KPNUMBER"))
                {
                    x++;
                    filter += " AND KPNUMBER={" + x.ToString() + "}";
                    mstin.Add("KPNUMBER", mst["KPNUMBER"]);
                }
                if (mst.ContainsKey("VENDERCODE"))
                {
                     x++;
                    group = " ,VENDERCODE";
                    fieldlist += " ,VENDERCODE as 厂商代码";
                    filter += " AND VENDERCODE={" + x.ToString() + "}";
                    mstin.Add("VENDERCODE", mst["VENDERCODE"]);
                }
                if (mst.ContainsKey("DATECODE"))
                {
                    x++;
                    group = " ,DATECODE";
                    fieldlist += " ,DATECODE as 生产周期";
                    filter += " AND DATECODE={" + x.ToString() + "}";
                    mstin.Add("DATECODE", mst["DATECODE"]);
                }
                if (mst.ContainsKey("LOTID"))
                {
                    x++;
                    group = " ,LOTID";
                    fieldlist += " ,LOTID as 生产批次";
                    filter += " AND LOTID={" + x.ToString() + "}";
                    mstin.Add("LOTID", mst["LOTID"]);
                }
                group = group.Substring(2, group.Length - 2);
                filter = filter.Substring(5, filter.Length - 5);
                fieldlist = fieldlist.Substring(2, fieldlist.Length - 2);
                fieldlist += ",sum(lotqty) as 数量";
              // throw new Exception("[" + filter + "]");
                ds = TransactionManager.GetData(table, fieldlist, filter, mstin, null, group, out count);
            }


           // return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

            return ds;
        }

        private void Insert_System_Log(string StrLog)
        {
            #region 存储失败日志在服务器
            //如果文件a.txt存在就打开，不存在就新建 .append 是追加写 
            GregorianCalendar gc = new GregorianCalendar();
            string LogName = DateTime.Now.ToString("yyyy") + gc.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString();
            FileStream fst = new FileStream("D:\\LOG\\" + LogName + ".log", FileMode.Append);
            //写数据到a.txt格式 
            StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
            swt.WriteLine(StrLog + "  -->" + DateTime.Now.ToString());
            swt.Close();
            fst.Close();
            #endregion
        }
    }
}
