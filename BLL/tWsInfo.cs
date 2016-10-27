using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SystemObject;

namespace BLL
{
   public partial class tWsInfo
    {
       
       public  tWsInfo()
       {
           
       }

       /// <summary>
       /// 新增车间信息
       /// </summary>
       /// <param name="wsinfo"></param>
       public  void InsertWsInfo(string dicwsinfo)
       {
           //MySqlCommand cmd = new MySqlCommand();
           //cmd.CommandText = "insert into SFCB.B_WS_INFO (wsId,facId,userId,wsname) values(@wsId,@facId,@userId,@wsname)";

           //cmd.Parameters.Add("wsId", MySqlDbType.VarChar, 25).Value = wsinfo.wsId;
           //cmd.Parameters.Add("facId", MySqlDbType.VarChar, 25).Value = wsinfo.facId;
           //cmd.Parameters.Add("userId", MySqlDbType.VarChar, 25).Value = wsinfo.userId;
           //cmd.Parameters.Add("wsname", MySqlDbType.VarChar, 25).Value = wsinfo.wsname;
           // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicwsinfo);
           dp.AddData("SFCB.B_WS_INFO", mst);
       }

       /// <summary>
       /// 根据车间Id修改车间信息
       /// </summary>
       /// <param name="wsId">需要修改的车间Id</param>
       /// <param name="wsinfo">车间实体</param>
       public  void EditWsInfo(string dicwsinfo)
       {
           //MySqlCommand cmd = new MySqlCommand();
           //cmd.CommandText = "update SFCB.B_WS_INFO set facId=@facId,userId=@userId,wsname=@wsname where wsId=@wsId";

           //cmd.Parameters.Add("wsId", MySqlDbType.VarChar, 25).Value = wsId;
           //cmd.Parameters.Add("facId", MySqlDbType.VarChar, 25).Value = wsinfo.facId;
           //cmd.Parameters.Add("userId", MySqlDbType.VarChar, 25).Value = wsinfo.userId;
           //cmd.Parameters.Add("wsname", MySqlDbType.VarChar, 25).Value = wsinfo.wsname;
           // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicwsinfo);
        
           dp.UpdateData("SFCB.B_WS_INFO", new string[] { "WSID" }, mst);
       }

       /// <summary>
       /// 获取所有车间信息,
       /// wsId,wsname,userId,facId,facname
       /// </summary>
       /// <returns> wsId,wsname,userId,facId,facname</returns>
       public  System.Data.DataSet GetAllWsInfo()
       {
           //MySqlCommand cmd = new MySqlCommand();
           //cmd.CommandText = "select ws.wsId as 车间编号,ws.wsname as 车间名称,ws.userId as 负责人,ws.facId as 工厂编号,f.facname as 工厂名称 from SFCB.B_WS_INFO ws,SFCB.B_FAC_INFO f where f.facId=ws.facId";

           //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
           int count = 0;
           string table = "SFCB.B_WS_INFO ws,SFCB.B_FAC_INFO f";
           string fieldlist = "ws.wsId as 车间编号,ws.wsname as 车间名称,ws.userId as 负责人,ws.facId as 工厂编号,f.facname as 工厂名称";
           string filter = "f.facId=ws.facId";          
           return TransactionManager.GetData(table, fieldlist, filter, null, null, null, out count);
       }

       /// <summary>
       /// 删除指定的车间信息
       /// </summary>
       /// <param name="wsId"></param>
       public  void DeleteWsInfo(string wsId)
       {
           //MySqlCommand cmd = new MySqlCommand();
           //cmd.CommandText = "delete from SFCB.B_WS_INFO where wsId=@wsId";
           //cmd.Parameters.Add("wsId", MySqlDbType.VarChar, 25).Value = wsId;
           // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = new Dictionary<string,object>();
           mst.Add("WSID",wsId);
           dp.DeleteData("SFCB.B_WS_INFO", mst);
       }
    }
}
