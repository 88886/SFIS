using System;
using System.Collections.Generic;
using System.Text;
using GenericProvider;
using GenericUtil;
using System.IO;
using System.Reflection;

namespace BLL
{
   public class Check_Vsersion
    {
       public bool CheckPrgVsersion(string Prg_Name,string Prg_Ver,string Ap_Desc,string Ap_Type,string Ap_Path,out string Msg)
       {
           Msg = "Error";
           if (string.IsNullOrEmpty(Ap_Desc))
               Ap_Desc = "NA";
           if (string.IsNullOrEmpty(Ap_Type))
               Ap_Type = "Y";
           if (string.IsNullOrEmpty(Ap_Path))
               Ap_Path = "NA";

           bool CheckFlag = false;

           string SystemVer = string.Empty;
           try
           {             

               int count = 0;
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               IDictionary<string, object> mst = new Dictionary<string, object>();
               mst.Add("ap_name", Prg_Name);
               System.Data.DataSet ds= dp.GetData("sfcb.b_ap_info", "ap_name,ap_version,ap_desc,ap_type,ap_path", mst, out count);    


               if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
               {

                   SystemVer = ds.Tables[0].Rows[0]["ap_version"].ToString();
                   System.Version SysVer = new Version(SystemVer);
                   System.Version PrgVer = new Version(Prg_Ver);
                   int ChkVal = PrgVer.CompareTo(SysVer);
                   if (ChkVal < 0)
                   {
                       CheckFlag = false;
                   }
                   else
                   {
                       if (ChkVal == 1)
                       {                          
                           dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                           mst = new Dictionary<string, object>();
                           mst.Add("ap_name", Prg_Name);
                           mst.Add("ap_version", Prg_Ver);
                           mst.Add("edit_time", System.DateTime.Now);
                           dp.UpdateData("sfcb.b_ap_info", new string[] { "ap_name" }, mst);
                          
                       }
                       CheckFlag = true;
                   }
               }
               else
               {                   

                    dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                    mst = new Dictionary<string, object>();
                    mst.Add("ap_name",Prg_Name );
                    mst.Add("ap_version", Prg_Ver);
                    mst.Add("ap_desc", Ap_Desc);
                    mst.Add("ap_type", Ap_Type);
                    mst.Add("ap_path", Ap_Path);
                   dp.AddData("sfcb.b_ap_info", mst);
                   CheckFlag = true;
               }
               Msg = "OK";
           }
           catch (Exception ex)
           {
               Msg = ex.Message;
               #region 存储失败日志在服务器
               //如果文件a.txt存在就打开，不存在就新建 .append 是追加写 
              // FileStream fst = new FileStream(System.Environment.CurrentDirectory + "\\log.txt", FileMode.Append);
               FileStream fst = new FileStream("D:\\LOG\\log.txt", FileMode.Append);
               //写数据到a.txt格式 
               StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
               swt.WriteLine("ChkVer: "+ex.Message + "  Time" + DateTime.Now.ToString());
               swt.Close();
               fst.Close();           
               #endregion              
               CheckFlag = false;
               return CheckFlag;
               throw new Exception(ex.Message);
           }
       
           return CheckFlag; 
         
       }

       public string CheckPrgVsersion(string Prg_Name, string Prg_Ver)
       {
           string SystemVer = string.Empty;


           int count = 0;
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("ap_name", Prg_Name);
           System.Data.DataSet ds = dp.GetData("sfcb.b_ap_info", "ap_name,ap_version,ap_desc,ap_type,ap_path", mst, out count);


           if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
           {

               SystemVer = ds.Tables[0].Rows[0]["ap_version"].ToString();
               System.Version SysVer = new Version(SystemVer);
               System.Version PrgVer = new Version(Prg_Ver);
               if (SysVer != PrgVer)
                   return string.Format(" Current Program Version: {0} \r\n New Program Version:{1}", PrgVer.ToString(), SysVer.ToString());

           }
           return "OK";
       }


       public string SystemMsg()
       {
           try
           {
               return Read();
           }
           catch
           {
               return null;
           }
       }     

       public string Read()
       {           //直接读取出字符串
           return System.IO.File.ReadAllText(@"D:\ApplicationMsg\SFIS.TXT", Encoding.UTF8);                 
       }
    }
}
