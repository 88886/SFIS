using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;

namespace BLL
{
   public partial class tRoleInfo
    {
       
       public tRoleInfo()
       {
           
       }

       /// <summary>
       /// 获取所有角色信息
       /// </summary>
       /// <returns></returns>
       public  System.Data.DataSet GetRoleInfo()
       {
           //MySqlCommand cmd = new MySqlCommand();
           //cmd.CommandText = "select rolecaption,rolelevel,roledesc from SFCB.B_ROLE_INFO";
           //return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

           string table = "SFCB.B_ROLE_INFO";
           string fieldlist = "rolecaption,rolelevel,roledesc";
           int count = 0;
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);           
           return dp.GetData(table, fieldlist, null, out count);
       }

       /// <summary>
       /// 新增角色信息
       /// </summary>
       /// <param name="roleinfo"></param>
       public  void InsertRoleInfo(string roleinfo)
       {
           //MySqlCommand cmd = new MySqlCommand();
           //cmd.CommandText = "insert into SFCB.B_ROLE_INFO(rolecaption,rolelevel,roledesc) values(@rolecaption,@rolelevel,@roledesc)";
           //cmd.Parameters.Add("rolecaption", MySqlDbType.VarChar, 30).Value = roleinfo.rolecaption;
           //cmd.Parameters.Add("rolelevel", MySqlDbType.VarChar, 30).Value = roleinfo.rolelevel;
           //cmd.Parameters.Add("roledesc", MySqlDbType.VarChar, 30).Value = roleinfo.roledesc;
           // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> dic = MapListConverter.JsonToDictionary(roleinfo);
           
           dp.AddData("SFCB.B_ROLE_INFO", dic);
       }

       /// <summary>
       /// 修改角色信息
       /// </summary>
       /// <param name="rolecaption"></param>
       /// <param name="roleinfo"></param>
       public  void EditRoleInfo(string roleinfo)
       {
           //MySqlCommand cmd = new MySqlCommand();
           //cmd.CommandText = "update SFCB.B_ROLE_INFO set rolelevel=@rolelevel,roledesc=@roledesc where rolecaption=@rolecaption";
           //cmd.Parameters.Add("rolelevel", MySqlDbType.Int32).Value = roleinfo.rolelevel;
           //cmd.Parameters.Add("roledesc", MySqlDbType.VarChar,50).Value = roleinfo.roledesc;
           //cmd.Parameters.Add("rolecaption", MySqlDbType.VarChar, 30).Value = rolecaption;
           // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);


           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> dic = MapListConverter.JsonToDictionary(roleinfo);          
           dp.UpdateData("SFCB.B_ROLE_INFO",new string[]{"ROLECAPTION"}, dic);
       }

       /// <summary>
       /// 删除角色
       /// </summary>
       /// <param name="rolecaption"></param>
       public  void DeleteRoleInfo(string rolecaption)
       {
           //MySqlCommand cmd = new MySqlCommand();
           //cmd.CommandText = "delete from SFCB.B_ROLE_INFO where rolecaption=@rolecaption";
           //cmd.Parameters.Add("rolecaption", MySqlDbType.VarChar,30).Value = rolecaption;
           // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("rolecaption", rolecaption);
           dp.DeleteData("SFCB.B_ROLE_INFO", mst);
       }

       //public  System.Data.DataSet QueryRoleInfo()
       //{
       //    MySqlCommand cmd = new MySqlCommand();
       //    cmd.CommandText = "select * from SFCB.B_ROLE_INFO";
       //    return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       //}
    }
}
