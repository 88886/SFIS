using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;

namespace BLL
{
    public partial class tWipDetail
    {
        
        public tWipDetail()
        {
            
        }

        public System.Data.DataSet GetWipDetail(string Serial)
        {
            string Colnum = @"ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE,
                            ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,LINE,SECTIONNAME,ROUTGROUPID,
                            STORENUMBER,WEIGHTQTY,QA_NO,QA_RESULT,TRACK_NO,ATE_STATION_NO,IN_LINE_TIME,BOMNUMBER,REWORKNO";
//            string sSQL = string.Format("select {0} from SFCR.T_WIP_DETAIL_A where esn=@Data ", Colnum);
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = sSQL;
//            cmd.Parameters.Add("Data", MySqlDbType.VarChar, 50).Value = Serial;
//            return  BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
            string table = "SFCR.T_WIP_DETAIL_A";
            string fieldlist = Colnum;
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("ESN", Serial);
           return dp.GetData(table, fieldlist,  mst, out count);
        }
//        public System.Data.DataSet GetHistoyrWipDetail(string Serial)
//        {
//            string Colnum = @"ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE,
//                            ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,LINE,SECTIONNAME,ROUTGROUPID,
//                            STORENUMBER,WEIGHTQTY,QA_NO,QA_RESULT,TRACK_NO,ATE_STATION_NO,IN_LINE_TIME,BOMNUMBER,REWORKNO";
//            string sSQL = string.Format("SELECT {0} FROM SFCR.H_WIP_DETAIL  WHERE ESN=@Data ",Colnum);
//            MySqlCommand cmd = new MySqlCommand();
//            cmd.CommandText = sSQL;
//            cmd.Parameters.Add("Data", MySqlDbType.VarChar, 50).Value = Serial;
//            return BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd);
//        }
    }
}
