using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;
using GenericProvider;
using SrvComponent;
using SystemObject;

namespace Stocking_In
{
    public class OperateDB
    {
        public OperateDB()
        {
        }

        public static DataTable DataTableToSort(DataTable dt, string Colnums)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} ASC", Colnums);
            return dv.ToTable();
        }
        /// <summary>
        /// 返回一个新的datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable getNewTable(DataTable dt, string sql)
        {
            try
            {
                DataTable mydt = new DataTable();
                mydt = dt.Clone();
                DataRow[] arrDr = dt.Select(sql);
                for (int i = 0; i < arrDr.Length; i++)
                {
                    mydt.ImportRow((DataRow)arrDr[i]);
                }
                return mydt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<string> Get_Line_List()
        {
            return refGetLineInfo.Instance.GetLineList();
        }

        public static DataTable Get_All_Station()
        {
            DataTable dt = reftCraftInfo.Instance.GetAllCraftInfo(null).Tables[0];
            DataTable temp = null;
            if (dt.Rows.Count > 0)
            {
                temp = getNewTable(dt, string.Format("TESTFLAG='{0}'", "0"));
            }
            DataTable dtLine = new DataTable();
            dtLine.Columns.Add("SECTION", typeof(string));
            dtLine.Columns.Add("GROUP", typeof(string));
            dtLine.Columns.Add("STATION", typeof(string));
            foreach (DataRow dr in temp.Rows)
            {
                dtLine.Rows.Add(dr["BEWORKSEG"].ToString(), dr["CRAFTNAME"].ToString(), dr["CRAFTPARAMETERURL"].ToString());
            }
            return DataTableToSort(dtLine, "GROUP");
        }


        public static DataTable Get_User_Info(string UserId,string UserPwd)
        {
            return reftUserInfo.Instance.GetUserInfo(UserId,null ,UserPwd).Tables[0];
        }
        public static DataTable Get_Wip_Tracking(string Colnums,string DATA)
        {
          string fieldlist = "ESN,WOID,PARTNUMBER,LOCSTATION,ERRFLAG,SCRAPFLAG"; 
          return  reftWipTracking.Instance.Get_WIP_TRACKING(Colnums, DATA, fieldlist).Tables[0];
        }
        public static DataTable Get_Wip_Tracking(string Colnums, string DATA, string fieldlist)
        {            
            return reftWipTracking.Instance.Get_WIP_TRACKING(Colnums, DATA, fieldlist).Tables[0];
        } 
        public static DataTable  Get_WoInfo(string woId)
        {
           return reftWoInfo.Instance.GetWoInfo(woId, null, "WOSTATE,FACTORYID,LOC").Tables[0];
        }

        public static DataTable Get_Pallet_Info(IDictionary<string, object> mst)
        {
          return  reftPalletInfo.Instance.GetPalletInfo(mst).Tables[0];
        }

        public static string CHECK_ROUTE(string DATA,string MYGROUP)
        {
           return refDb_Procedure.Instance.PRO_CHECK_ROUTE(DATA, MYGROUP);
        }

        public static int CHECK_DATA_Z_WHS_WIP_TRACKING(string DATA)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("ESN", DATA);
           return refZ_WHS_TRACKING.Instance.Get_Z_WHS_TRACKING(dic,"ESN").Tables[0].Rows.Count;
        }
        public static string GetStockinNumber()
        {
            return refProPublicStoredproc.Instance.GetStockInNumber();
        }
        public static string UPDATE_WIP_TRACKING(IDictionary<string, object> mst ,List<string> ListFields)
        {
            return reftWipTracking.Instance.Update_WIP_TRACKING(mst,ListFields);
        }

        public static string TEST_MAIN_ONLY(string DATA, string MYGROUP,string SECTION_NAME,string STATION_NAME,string EMP,string LINE)
        {
            string C_RES = string.Empty;
            refDb_Procedure.Instance.PRO_TEST_MAIN_ONLY(DATA, MYGROUP, SECTION_NAME, STATION_NAME, EMP, "NA", LINE, out C_RES);
             return C_RES;
        }

        public static string   Inser_Z_WIP_TRACKING(string STOCK_NO)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = @"INSERT INTO sfcr.z_whs_tracking (ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE,
                            ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,LINE,SECTIONNAME,ROUTGROUPID,
                            STORENUMBER,WEIGHTQTY,QA_NO,QA_RESULT,TRACK_NO,ATE_STATION_NO,IN_LINE_TIME,BOMNUMBER,REWORKNO,LOTIN,STOREHOUSEID,RECDATE1,STATUS)

SELECT ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE,
                            ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,LINE,SECTIONNAME,ROUTGROUPID,
                            STORENUMBER,WEIGHTQTY,QA_NO,QA_RESULT,TRACK_NO,ATE_STATION_NO,IN_LINE_TIME,BOMNUMBER,REWORKNO,STORENUMBER,'NA',NOW(),0
  FROM sfcr.t_wip_tracking_online WHERE STORENUMBER =@C_STORENUMBER ";
                cmd.Parameters.Add("C_STORENUMBER", MySqlDbType.VarChar).Value = STOCK_NO;

                ExecteNonQuery(cmd);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public static DataTable GetStockInPrint(string StockIn)
        {
            int count = 0;
            string table = "SFCR.T_WIP_TRACKING_ONLINE a";
            string fieldlist = "a.woId, a.partnumber, a.wipstation, a.productname, count(a.esn) qty";
            string filter = "a.storenumber = {0} ";
            string group = "a.woId, a.partnumber, a.wipstation, a.productname";
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("storenumber", StockIn);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, group, out count).Tables[0];

        }

        public static bool Check_Version(string Prg_Name,string Prg_Ver,string Ap_Desc,string Ap_Type,string Ap_Path,out string Msg)
        {
           return refCheck_Vsersion.Instance.CheckPrgVsersion(Prg_Name, Prg_Ver, Ap_Desc, Ap_Type, Ap_Path, out Msg);
        }

        private static void ExecteNonQuery(MySqlCommand cmd)
        {
 
            MySqlConnection _conn = new MySqlConnection(ProConfiguration.GetConfig().DatabaseConnect);
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                cmd.Connection = _conn;
                cmd.CommandTimeout = 84100;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
                _conn.Dispose();
            }
        }


        public static string GetWOLoc(string woId)
        {
            DataTable dt = reftwoInfoErp.Instance.Get_WO_Info_Erp(woId,"LOC").Tables[0];
            if (dt.Rows.Count > 0 )
                return string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? "1000" : dt.Rows[0][0].ToString();
            else
                return "NA";
        }
    }

    public class refGetLineInfo
    {
        private static BLL.tLineInfo instance;
        public static BLL.tLineInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL.tLineInfo();
                return instance;
            }
        }
        static refGetLineInfo()
        {
            instance = new BLL.tLineInfo();
        }
    }
    public class reftCraftInfo
    {
        private static BLL.tCraftInfo instance;

        public static BLL.tCraftInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL.tCraftInfo();
                return instance;
            }
        }
        static reftCraftInfo()
        {
            instance = new BLL.tCraftInfo();
        }
    }
     public class reftUserInfo
    {
        private static BLL.tUserInfo instance;

        public static BLL.tUserInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL.tUserInfo();
                return instance;
            }
        }
        static reftUserInfo()
        {
            instance = new BLL.tUserInfo();
        }
    }
     public class reftWipTracking
     {
         private static BLL.tWipTracking instance;

         public static BLL.tWipTracking Instance
         {
             get
             {
                 if (instance == null)
                     instance = new BLL.tWipTracking();
                 return instance;
             }
         }
         static reftWipTracking()
         {
             instance = new BLL.tWipTracking();
         }
     }
     public class reftWoInfo
     {
         private static BLL.tWoInfo instance;

         public static BLL.tWoInfo Instance
         {
             get
             {
                 if (instance == null)
                     instance = new BLL.tWoInfo();
                 return instance;
             }
         }
         static reftWoInfo()
         {
             instance = new BLL.tWoInfo();
         }
     }
     public class reftPalletInfo
     {
         private static BLL.tPalletInfo instance;

         public static BLL.tPalletInfo Instance
         {
             get
             {
                 if (instance == null)
                     instance = new BLL.tPalletInfo();
                 return instance;
             }
         }
         static reftPalletInfo()
         {
             instance = new BLL.tPalletInfo();
         }
     }
     public class refDb_Procedure
     {
         private static BLL.Db_Procedure instance;

         public static BLL.Db_Procedure Instance
         {
             get
             {
                 if (instance == null)
                     instance = new BLL.Db_Procedure();
                 return instance;
             }
         }
         static refDb_Procedure()
         {
             instance = new BLL.Db_Procedure();
         }
     }
     public class refZ_WHS_TRACKING
     {
         private static BLL.tZ_WHS_TRACKING instance;

         public static BLL.tZ_WHS_TRACKING Instance
         {
             get
             {
                 if (instance == null)
                     instance = new BLL.tZ_WHS_TRACKING();
                 return instance;
             }
         }
         static refZ_WHS_TRACKING()
         {
             instance = new BLL.tZ_WHS_TRACKING();
         }
     }
     public class refProPublicStoredproc
     {
         private static BLL.ProPublicStoredproc instance;

         public static BLL.ProPublicStoredproc Instance
         {
             get
             {
                 if (instance == null)
                     instance = new BLL.ProPublicStoredproc();
                 return instance;
             }
         }
         static refProPublicStoredproc()
         {
             instance = new BLL.ProPublicStoredproc();
         }
     }
     public class refCheck_Vsersion
     {
         private static BLL.Check_Vsersion instance;

         public static BLL.Check_Vsersion Instance
         {
             get
             {
                 if (instance == null)
                     instance = new BLL.Check_Vsersion();
                 return instance;
             }
         }
         static refCheck_Vsersion()
         {
             instance = new BLL.Check_Vsersion();
         }
     }
     public class reftwoInfoErp
     {
         private static BLL.t_wo_Info_Erp instance;

         public static BLL.t_wo_Info_Erp Instance
         {
             get
             {
                 if (instance == null)
                     instance = new BLL.t_wo_Info_Erp();
                 return instance;
             }
         }
         static reftwoInfoErp()
         {
             instance = new BLL.t_wo_Info_Erp();
         }
     }


}
