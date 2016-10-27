using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;

namespace BLL
{
  public  class tVenderInfo
    {
     
      public tVenderInfo()
      {
         
      }
      string table = "SFCB.B_VENDER_INFO";
      public System.Data.DataSet QueryVenderInfo(string vendnumber)
      {
          //MySqlCommand cmd = new MySqlCommand();
          //cmd.CommandText = "select * from sfcb.b_vender_info";
          //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);

          string fieldlist = "VENDERID,VENDERNAME,VENDERNAME2";
          int count = 0;
          IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
          IDictionary<string, object> mst = new Dictionary<string, object>();
          if (!string.IsNullOrEmpty(vendnumber))
          mst.Add("vendnumber", vendnumber);
          return dp.GetData(table, fieldlist, mst, out count);
       
      }

      //public  System.Data.DataSet QueryVenderInfoByVenderId(string venderId)
      //{
      //    //MySqlCommand cmd = new MySqlCommand();
      //    //cmd.CommandText = "select venderId,vendername,vendername2 from sfcb.b_vender_info where venderId=@venderId";
      //    //cmd.Parameters.Add("venderId", MySqlDbType.VarChar, 25).Value = venderId;
      //    //return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
      //    string fieldlist = "kpnumber,vendername,vendcode";
      //    int count = 0;
      //    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
      //    IDictionary<string, object> mst = new Dictionary<string, object>();
      //    mst.Add("vendnumber", vendnumber);
      //    return dp.GetData(table, fieldlist, mst, out count);
      //}

      /// <summary>
      /// 添加商家信息
      /// </summary>
      /// <param name="venderinfo"></param>
      public  void InsertVenderInfo(string dicvenderinfo)
      {
          //MySqlCommand cmd = new MySqlCommand();
          //cmd.CommandText = "insert into sfcb.b_vender_info (venderId,vendername,vendername2) values (@venderId,@vendername,@vendername2)";
          //cmd.Parameters.Add("venderId", MySqlDbType.VarChar, 20).Value = venderinfo.VenderId;
          //cmd.Parameters.Add("vendername", MySqlDbType.VarChar, 50).Value = venderinfo.VenderName;
          //cmd.Parameters.Add("vendername2", MySqlDbType.VarChar, 100).Value = venderinfo.VenderName2;
          // BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);

          IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
          IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicvenderinfo);       
          dp.AddData(table,  mst);
      }
    }
}
