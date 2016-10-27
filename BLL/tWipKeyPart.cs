using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using SystemObject;
using SrvComponent;

namespace BLL
{
  public  class tWipKeyPart
    {
    
      public  tWipKeyPart()
      {
          if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)
              DB_Flag = 0;
          if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
              DB_Flag = 1;
      }
      /// <summary>
      /// 数据库标记 0 MySQL;1 Oracle
      /// </summary>
      int DB_Flag = 0;
 
      public System.Data.DataSet GetWipKeyParts(string SNTYPE, string Snval)
      {
          string table = "SFCR.T_WIP_KEYPART_ONLINE";
          if (DB_Flag == 1)
              table = "SFCR.T_WIP_KEYPART";
          string fieldlist = "ESN,WOID,SNTYPE,SNVAL,STATION,KPNO,RECDATE";
          int count = 0;
          IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
          IDictionary<string, object> mst = new Dictionary<string, object>();
          mst.Add(SNTYPE, Snval);
          return dp.GetData(table, fieldlist, mst, out count);
      }

      /// <summary>
      /// 检查序列号是否在这个工单里是否存在(工单号可以为空=查询整张表的记录,序列号类型可以为空)
      /// </summary>
      /// <param name="serial"></param>
      /// <param name="sntype"></param>
      /// <param name="woid"></param>
      /// <returns></returns>
      public System.Data.DataSet ChkKeyParts(string serial,string sntype, string woid)
      {          
          IDictionary<string, object> mst = new Dictionary<string, object>();
          int x = 0;
          int count = 0;
          string table = "SFCR.T_WIP_KEYPART_ONLINE";
          if (DB_Flag == 1)
              table = "SFCR.T_WIP_KEYPART";
          string fieldlist = "esn,sntype".ToUpper();
          string filter = "SNVAL={"+x.ToString()+"}";
          mst.Add("SNVAL", serial);
          if (!string.IsNullOrEmpty(woid))
          {
              x = x + 1;
              filter += " AND WOID={"+x.ToString()+"}";
              mst.Add("WOID", woid);
          }
          if (!string.IsNullOrEmpty(sntype))
          {
              x = x + 1;
              filter += " AND SNTYPE={" + x.ToString() + "}";
              mst.Add("SNTYPE", sntype);
          }
          else
          {
              filter += " and sntype not in ('ESN') ";              
          }
         
         return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

      }

      


      ///// <summary>
      ///// 通过keyparts找到esn
      ///// </summary>
      ///// <param name="val"></param>
      ///// <returns></returns>
      //public string GetEsnForKeyParts(string val)
      //{          
      //    string table = "SFCR.T_WIP_KEYPART_ONLINE";
      //    if (DB_Flag == 1)
      //        table = "SFCR.T_WIP_KEYPART";
      //    string fieldlist = "ESN";
      //    int count = 0;
      //    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
      //    IDictionary<string, object> mst = new Dictionary<string, object>();
      //    mst.Add("SNVAL", val);
      //    DataTable dt = dp.GetData(table, fieldlist,  mst, out count).Tables[0];

      //    if (dt!=null && dt.Rows.Count != 0)
      //    {
      //        return dt.Rows[0][0].ToString();
      //    }
      //    else
      //    {
      //        return "ERROR";
      //    }
      //}
      ///// <summary>
      ///// 获取历史信息
      ///// </summary>
      ///// <param name="woId"></param>
      ///// <param name="strHour"></param>
      ///// <returns></returns>
      //public System.Data.DataSet H_GetWoAllSerial(string woId, int strHour)
      //{
      //    int count = 0;
      //    string table = "sfcr.h_Wip_KeyPart a,sfcr.T_WIP_TRACKING_ONLINE b".ToUpper();
      //    if (DB_Flag == 1)
      //        table = "sfcr.h_Wip_KeyPart a,sfcr.T_WIP_TRACKING b".ToUpper();
      //    string fieldlist = "b.cartonnumber,a.woId,a.esn,a.sntype,a.snval".ToUpper();
      //    string filter = "a.woId={0}".ToUpper();         
      //    IDictionary<string, object> mst = new Dictionary<string, object>();
      //    mst.Add("WOID", woId);
      //    return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
      //} 

      public string CHECK_KPS_VALID_FOR_REPAIR(string ESN, string KPS, string SNTYPE, string WOID)
      {
          string _StrErr = string.Empty;
          string C_WOID = string.Empty;
          string C_LOCSTATION = string.Empty;
          string C_LASTGROUP = string.Empty;
          string C_BOM = string.Empty;
          string C_PARTNUMBER = string.Empty;
          try
          {
              if (ESN == KPS)
                  throw new Exception("SN = KPS");
              if (SNTYPE != "KPESN")
                  throw new Exception("SN TYPE NOT [KPESN]");
              DataTable dt_wip_keyparts = GetWipKeyParts( "SNVAL",KPS).Tables[0];
              if (dt_wip_keyparts.Rows.Count > 0)
                  throw new Exception("KPS DUP");
              BLL.tWipTracking twip = new tWipTracking();
              DataTable KPS_WIP = twip.Get_WIP_TRACKING("ESN", KPS).Tables[0];
              if (KPS_WIP.Rows.Count == 0)
                  throw new Exception("KPS NOT FOUND");

              if (KPS_WIP.Rows[0]["STORENUMBER"].ToString().IndexOf("_R") > 0)
                  throw new Exception("KPS Release Bound,Waiting For Rework");

              C_WOID = KPS_WIP.Rows[0]["WOID"].ToString();
              C_LOCSTATION = KPS_WIP.Rows[0]["LOCSTATION"].ToString();
              C_PARTNUMBER = KPS_WIP.Rows[0]["PARTNUMBER"].ToString();
              if (C_WOID == WOID)
                  throw new Exception("THE SAME TWO WO");
              BLL.tWoInfo woinfo = new tWoInfo();

              DataTable KPS_WO_INFO = woinfo.GetWoInfo(C_WOID, null, "OUTPUTGROUP").Tables[0];
              if (KPS_WO_INFO.Rows.Count == 0)
                  throw new Exception("KPS WO NOT FOUND");
              C_LASTGROUP = KPS_WO_INFO.Rows[0]["OUTPUTGROUP"].ToString();
              if (C_LOCSTATION == C_LASTGROUP || C_LOCSTATION == "SC_TEST" || C_LOCSTATION == "SC_SMT")
              {
                  DataTable WO_INFO = woinfo.GetWoInfo(WOID, null, "BOMNUMBER").Tables[0];
                  if (WO_INFO.Rows.Count == 0)
                      throw new Exception("WO NOT FOUND");
                  C_BOM = WO_INFO.Rows[0]["BOMNUMBER"].ToString();
                  if (string.IsNullOrEmpty(C_BOM) || C_BOM == "NA")
                      throw new Exception("KPS BOM NOT FOUND");

                  BLL.tBomKeyPart bkp = new tBomKeyPart();
                  DataTable dt_BOM = bkp.GetBomNoParts( C_BOM ).Tables[0];
                  int x = 0;
                  foreach (DataRow dr in dt_BOM.Rows)
                  {

                      if (dr["KPNUMBER"].ToString() == C_PARTNUMBER)
                          x++;
                  }
                  if (x > 0)
                      _StrErr = "OK";
                  else
                      _StrErr = "KPS NOT IN BOM";

              }
              else
              {
                  _StrErr = "KPS ROUTE NOT END";
              }
          }
          catch (Exception ex)
          {
              _StrErr = ex.Message;
          }
          return _StrErr;
      }

      public string Update_WIP_KEYPARTS(string ESN, string KPS, string OLDKPS, string SNTYPE)
      {
          try
          {
              string table = "SFCR.T_WIP_KEYPART_ONLINE";
              if (DB_Flag == 1)
                  table = "SFCR.T_WIP_KEYPART";          

              string fieldlist = "SNVAL={0},RECDATE={1}";
              string filter = "ESN={0} AND SNTYPE={1} AND SNVAL={2}";
              IDictionary<string, object> modFields = new Dictionary<string, object>();
              modFields.Add("SNVAL", KPS);
              modFields.Add("RECDATE", DateTime.Now);    
              IDictionary<string, object> keyVals = new Dictionary<string, object>();
              keyVals.Add("ESN", ESN);
              keyVals.Add("SNTYPE", SNTYPE);
              keyVals.Add("SNVAL1", OLDKPS);
              TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);


              table = "SFCR.T_WIP_TRACKING_ONLINE";
              if (DB_Flag == 1)
                  table = "SFCR.T_WIP_TRACKING";
              if ((SNTYPE == "MAC") || (SNTYPE == "IMEI") || (SNTYPE == "SN"))
              {               
                 

                  fieldlist = SNTYPE + "={0}, STORENUMBER=CONCAT(STORENUMBER,'_R')";
                  filter = "ESN={0}";
                  modFields = new Dictionary<string, object>();
                  modFields.Add(SNTYPE,KPS);
                  keyVals = new Dictionary<string, object>();
                  keyVals.Add("ESN", OLDKPS);
                  TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);

              }
              else
              {
                  fieldlist = "STORENUMBER=CONCAT(STORENUMBER,'_R')";
                  filter = "ESN={0}";
                  modFields = new Dictionary<string, object>();
                  keyVals = new Dictionary<string, object>();
                  keyVals.Add("ESN", OLDKPS);
                  TransactionManager.UpdateBatchData(table, fieldlist, modFields, filter, keyVals);
              }
            

              return "OK";
          }
          catch (Exception ex)
          {
              return ex.Message;
          }
      }
      public string Insert_WipKeyParts_Undo(string ESN, string SNTYPE, string SNVAL)
      {
          try
          {
              int count = 0;
              IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
              IList<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
              IDictionary<string, object> mst = null;
               mst = new Dictionary<string, object>();
               mst.Add("ESN", ESN);
               if (!string.IsNullOrEmpty(SNTYPE) && !string.IsNullOrEmpty(SNVAL))
               {
                   mst.Add("SNTYPE", SNTYPE);
                   mst.Add("SNVAL", SNVAL);
               }
              DataSet ds = null;
              if (DB_Flag==0)
               ds = dp.GetData("SFCR.T_WIP_KEYPART_ONLINE", "ESN,WOID,SNTYPE,SNVAL,STATION,KPNO,RECDATE", mst, out count);
                if (DB_Flag==1)
                ds = dp.GetData("SFCR.T_WIP_KEYPART", "ESN,WOID,SNTYPE,SNVAL,STATION,KPNO,RECDATE", mst, out count);

              DataTable dt = ds.Tables[0];
              foreach (DataRow dr in dt.Rows)
              {
                  mst = new Dictionary<string, object>();
                  mst.Add("ESN", dr["ESN"].ToString());
                  mst.Add("WOID", dr["WOID"].ToString());
                  mst.Add("SNTYPE", dr["SNTYPE"].ToString());
                  mst.Add("SNVAL", dr["SNVAL"].ToString());
                  mst.Add("STATION", dr["STATION"].ToString());
                  mst.Add("KPNO", dr["KPNO"].ToString());
                  mst.Add("RECDATE", dr["RECDATE"].ToString());
                  list.Add(mst);
              }
              dp.AddListData("SFCR.T_WIP_KEYPART_UNDO", list);             
              return "OK";
          }
          catch (Exception ex)
          {
              return ex.Message;
          }
      }
      public string DELETE_WipKeyParts(string ESN, string SNTYPE, string SNVAL)
      {
          try
          {             
              string table = "SFCR.T_WIP_KEYPART_ONLINE";
              if (DB_Flag==1)
                  table = "SFCR.T_WIP_KEYPART";
              IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);          
              IDictionary<string, object> mst = new Dictionary<string, object>();
              mst.Add("ESN", ESN);
              if (!string.IsNullOrEmpty(SNTYPE) && !string.IsNullOrEmpty(SNVAL))
              {
                  mst.Add("SNTYPE", SNTYPE);
                  mst.Add("SNVAL", SNVAL);
              }
              dp.DeleteData(table, mst);
              return "OK";
          }
          catch (Exception ex)
          {
              return ex.Message;
          }
      }


    }
}
