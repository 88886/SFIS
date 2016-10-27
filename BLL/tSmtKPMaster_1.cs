using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace BLL
{
   public partial class tSmtKPMaster
    {
       /// <summary>
       /// 获取所有的料站表头
       /// </summary>
       /// <returns></returns>
       public  System.Data.DataSet GetAllMasterid()
       {
           MySqlCommand cmd = new MySqlCommand();
           cmd.CommandText = "select masterId from SFCR.T_SMT_KP_MASTER";
           return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       }
       public  System.Data.DataSet GetKpDetail(string masterid, string stationno)
       {
           MySqlCommand cmd = new MySqlCommand();
           cmd.CommandText = "select kpdistinct,replacegroup,priorityclass,loction from SFCR.T_SMT_KP_DETALT where masterId=@masterid and stationno=@stationno";
           cmd.Parameters.Add("masterid", MySqlDbType.VarChar, 20).Value = masterid;
           cmd.Parameters.Add("stationno", MySqlDbType.VarChar, 20).Value = stationno;
           return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       }
       /// <summary>
       /// 由成品料号，机器编号及PCB面获得该产品的备料料站信息
       /// </summary>
       /// <param name="partnumber"></param>
       /// <param name="lineid"></param>
       /// <param name="pcbside"></param>
       /// <returns></returns>
       public  System.Data.DataSet GetStationno(string partnumber,string lineid,string pcbside)
       {
           MySqlCommand cmd = new MySqlCommand();
           cmd.CommandText = "select distinct a.stationno,a.masterId  from SFCR.T_SMT_KP_DETALT a where exists (select b.masterId from SFCR.T_SMT_KP_MASTER b where a.masterId=b.masterId and b.partnumber=@partnumber and b.lineId=@lineid and pcbside=@pcbside)";
           cmd.Parameters.Add("partnumber", MySqlDbType.VarChar, 20).Value = partnumber;
           cmd.Parameters.Add("lineid", MySqlDbType.VarChar, 20).Value = lineid;
           cmd.Parameters.Add("pcbside", MySqlDbType.VarChar, 10).Value = pcbside;
           return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       }
       public  System.Data.DataSet GettSmtKPDetaltForWo()
       {
           MySqlCommand cmd = new MySqlCommand();
           cmd.CommandText = "select woId,kpnumber,stationno,kpdesc,kpdistinct,replacegroup,recdate  from SFCR.T_SMT_KP_DETALT_FORWO";
           return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       }
       public  System.Data.DataSet GettSmtKPDetaltForWoByKpnumber(string kpnumber)
       {
           MySqlCommand cmd = new MySqlCommand();
           cmd.CommandText = "select woId,kpnumber,stationno,kpdesc,kpdistinct,replacegroup,recdate  from SFCR.T_SMT_KP_DETALT_FORWO where kpnumber=@kpnumber";
           cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
           return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       }
       /// <summary>
       /// 判断该料在料站表中是否存在
       /// </summary>
       /// <param name="stationno"></param>
       /// <param name="masterid"></param>
       /// <param name="kpnumber"></param>
       /// <returns></returns>
       public  System.Data.DataSet JudgeKpExists(string stationno,string masterid,string kpnumber)
       {
           MySqlCommand cmd = new MySqlCommand();
           cmd.CommandText = "select kpnumber,kpdesc from SFCR.T_SMT_KP_DETALT where stationno=@stationno and masterId=@masterid and kpnumber=@kpnumber";
           cmd.Parameters.Add("stationno", MySqlDbType.VarChar, 20).Value = stationno;
           cmd.Parameters.Add("masterid", MySqlDbType.VarChar, 20).Value = masterid;
           cmd.Parameters.Add("kpnumber", MySqlDbType.VarChar, 20).Value = kpnumber;
           return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       }

       public System.Data.DataSet GetPartnumberAData(string partnumber, string lineid, string pcbside)
       {
           MySqlCommand cmd = new MySqlCommand();
           string strTemp = string.Empty;
           if (!string.IsNullOrEmpty(lineid))
           {
               strTemp += " and a.lineId like @lineId ";
               cmd.Parameters.Add("lineId", MySqlDbType.VarChar, 20).Value = lineid + "%";
           }
           if (!string.IsNullOrEmpty(pcbside))
           {
               strTemp += " and a.pcbside=@pcbside ";
               cmd.Parameters.Add("pcbside", MySqlDbType.VarChar, 20).Value = pcbside;
           }
           cmd.CommandText = string.Format("select a.partnumber,a.lineId,a.pcbside,a.bomver,b.stationno,b.kpnumber,b.kpdesc,b.replacegroup,b.loction from SFCR.T_SMT_KP_MASTER a,SFCR.T_SMT_KP_DETALT b where a.masterId=b.masterId and a.partnumber=@partnumber {0}", strTemp);
           cmd.Parameters.Add("partnumber", MySqlDbType.VarChar, 25).Value = partnumber;

           return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
       }
    }
}
