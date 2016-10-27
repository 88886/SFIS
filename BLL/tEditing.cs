using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;

namespace BLL
{
   public partial class tEditing
    {
       public tEditing()
       {

       }

       /// <summary>
       /// "SFCB.B_EDITING"
       /// </summary>
       string table = "SFCB.B_EDITING";
       /// <summary>
       /// 增加一个被编辑的项目
       /// </summary>
       /// <param name="_editing"></param>
       /// <returns></returns>
       public string InserttEditing(string Dicediting)
       {
           try
           {            
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               IDictionary<string, object> dic = MapListConverter.JsonToDictionary(Dicediting);
               dp.AddData(table, dic);
               return "OK";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }

       /// <summary>
       /// 获取一个被编辑的项目
       /// </summary>
       /// <param name="funname"></param>
       /// <returns></returns>
       public string GettEditingInfo(string funname)
       {                 
           string fieldlist = "userId, username, PC_NAME,MAC_ADDRESS,recdate";
           int count = 0;
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("funname", funname);
           DataSet ds = dp.GetData(table, fieldlist, mst, out count);
           DataTable dt = ds.Tables[0];
           if (dt == null || dt.Rows.Count < 1)
               return string.Empty;
           else
           {
               return dt.Rows[0][0].ToString() + "," + dt.Rows[0][1].ToString() + "," + dt.Rows[0][2].ToString() + "," + dt.Rows[0][3].ToString() + "," + dt.Rows[0][4].ToString() ;
           }  
           
       }
       /// <summary>
       /// 获取一个被编辑的项目
       /// </summary>
       /// <param name="funname"></param>
       /// <returns></returns>
       public string GettEditingInfo(string funname,string Prj)
       {
           string fieldlist = "userId, username, PC_NAME,MAC_ADDRESS,recdate";
           int count = 0;
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("funname", funname);
           mst.Add("PRJ", Prj);
           DataSet ds = dp.GetData(table, fieldlist, mst, out count);
           DataTable dt = ds.Tables[0];
           if (dt == null || dt.Rows.Count < 1)
               return string.Empty;
           else
           {
               return dt.Rows[0][0].ToString() + "," + dt.Rows[0][1].ToString() + "," + dt.Rows[0][2].ToString() + "," + dt.Rows[0][3].ToString() + "," + dt.Rows[0][4].ToString();
           }

       }
    
       /// <summary>
       /// 删除一个被锁住的资源
       /// </summary>
       /// <param name="funname"></param>
       /// <returns></returns>
       public string DeletetEditingByfunname(string funname)
       {
           try
           {            
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               IDictionary<string, object> mst = new Dictionary<string, object>();
               mst.Add("funname", funname);
               dp.DeleteData(table, mst);
               return "OK";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }

       public string DeletetEditingByUserId(string userId)
       {
           try
           {              
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               IDictionary<string, object> mst = new Dictionary<string, object>();
               mst.Add("userId", userId);
               dp.DeleteData(table, mst);
               return "OK";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }

       public string DeletetEditingByUserIdAndPrj(string userId,string prj)
       {
           try
           {              
               IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
               IDictionary<string, object> mst = new Dictionary<string, object>();
               mst.Add("userId", userId);
               mst.Add("prj", prj);
               dp.DeleteData(table, mst);
               return "OK";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }

       public string ChktEditing(string Dicediting)
       {

           IDictionary<string, object> dic = MapListConverter.JsonToDictionary(Dicediting);
           try
           {
               string str = this.GettEditingInfo(dic["FUNNAME"].ToString(), dic["PRJ"].ToString());
                   if (string.IsNullOrEmpty(str))
                   {                      
                       DeletetEditingByUserIdAndPrj(dic["USERID"].ToString(), dic["PRJ"].ToString());
                       return this.InserttEditing(Dicediting);                   
                   }
                   string[] arr = str.Split(',');
                   if (arr[0].ToUpper() != dic["USERID"].ToString().ToUpper() || arr[2].ToUpper() != dic["PC_NAME"].ToString().ToUpper())
                   {
                       return string.Format("WARNING:{0}", str);
                   }
                   else
                   {
                       return "OK";
                   }
                
           }
           catch(Exception ex)
           {
               return "ERROR:" + ex.Message;
           }
       }
    }
}
