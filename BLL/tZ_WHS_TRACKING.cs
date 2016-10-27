using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericProvider;

namespace BLL
{
   public class tZ_WHS_TRACKING
    {
       public tZ_WHS_TRACKING()
       {
       }


       public System.Data.DataSet Get_Z_WHS_TRACKING(Dictionary<string,object> mst)
       {
           string table = "SFCR.Z_WHS_TRACKING";
           string fieldlist = "ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE,ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,"+
                             "LINE,SECTIONNAME,ROUTGROUPID,STORENUMBER,WEIGHTQTY,QA_NO,QA_RESULT,TRACK_NO,ATE_STATION_NO,IN_LINE_TIME,BOMNUMBER,REWORKNO,LOTIN,STOREHOUSEID,LOCID,LOTOUT,RECDATE1,STATUS";
           int count = 0;
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);        
           return dp.GetData(table, fieldlist, mst, out count);
       }
       public System.Data.DataSet Get_Z_WHS_TRACKING(Dictionary<string, object> mst,string Fields)
       {
           string table = "SFCR.Z_WHS_TRACKING";
           string fieldlist = Fields;
           int count = 0;
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           return dp.GetData(table, fieldlist, mst, out count);
       }
    }
}
