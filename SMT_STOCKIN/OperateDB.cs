using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using GenericProvider;
using GenericUtil;
using SystemObject;

namespace SMT_STOCKIN
{
   public class OperateDB
    {


     //   private MySqlConnection SFIS_conn;
       BLL.tUserInfo user = new BLL.tUserInfo();
       BLL.tLineInfo _LineInfo = new BLL.tLineInfo();
       BLL.tWoInfo _woinfo = new BLL.tWoInfo();
       BLL.tWipTracking _wipinfo = new BLL.tWipTracking();
       BLL.Db_Procedure _DbPro = new BLL.Db_Procedure();
       WebServices.tZ_Whs_SAP_BackFlush.tZ_Whs_SAP_BackFlush zBackFlush = new WebServices.tZ_Whs_SAP_BackFlush.tZ_Whs_SAP_BackFlush();

       public string Check_User(string userId,string UserPwd)
       {
           string _StrErr = "NO EMP";
           try
           {
               DataTable dt = user.GetUserInfo(userId, null, UserPwd).Tables[0];
               if (dt.Rows.Count > 0)
               {
                   if (dt.Rows[0]["PWD"].ToString() == UserPwd)
                   {
                       _StrErr = "OK";
                   }
               }
           }
           catch (Exception ex)
           {
               _StrErr = ex.Message;
           }
           return _StrErr;
       }
       public List<string> Get_Line_Info()
       {
           List<string> Line = new List<string>();
           DataTable dt = _LineInfo.GetAllLineInfo().Tables[0];
           DataView dv = dt.DefaultView;
           dv.Sort = string.Format("{0} ASC", dt.Columns[0].ColumnName);
           DataTable dttemp = dv.ToTable();
           foreach (DataRow dr in dttemp.Rows)
           {
               Line.Add(dr[0].ToString());
           }
           return Line;
       }
       public DataTable Get_ALL_Line_Info()
       {     

           DataTable dt = _LineInfo.GetAllLineInfo().Tables[0];
           DataView dv = dt.DefaultView;
           dv.Sort = string.Format("{0} ASC", dt.Columns[0].ColumnName);
           DataTable dttemp = dv.ToTable();

           return dttemp;
       }
       public DataTable Get_woId_Info(string woId)      
       {
 
           return _woinfo.GetWoInfo(woId,null,"FACTORYID,LOC,OUTPUTGROUP,LINEID").Tables[0];
               
       }
       public DataTable Get_WIP_Tracking(string woId,string WIP_GROUP)
       {
            string Colnum = @"ESN,WOID,PARTNUMBER,PRODUCTNAME,VERSIONCODE,TYPE,LOCSTATION,STATIONNAME,WIPSTATION,NEXTSTATION,USERID,RECDATE,
                            ERRFLAG,SCRAPFLAG,SN,MAC,IMEI,CARTONNUMBER,TRAYNO,PALLETNUMBER,MCARTONNUMBER,MPALLETNUMBER,LINE,SECTIONNAME,ROUTGROUPID,
                            STORENUMBER,WEIGHTQTY,QA_NO,QA_RESULT,TRACK_NO,ATE_STATION_NO,IN_LINE_TIME,BOMNUMBER,REWORKNO";

             string table = "sfcr.t_wip_tracking_online".ToUpper();
                string fieldlist = Colnum;
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WOID", woId);
                mst.Add("WIPSTATION", WIP_GROUP);
                DataSet ds = dp.GetData(table, fieldlist,  mst, out count);
                return ds.Tables[0];
                   
       }
       public DataTable Get_WIP_Tracking_ReUpload(string STOCK_NO)
       {

           DataTable dt = _wipinfo.Get_WIP_TRACKING("STORENUMBER", STOCK_NO).Tables[0];
         DataTable dtUpload = dt.Clone();
         if (dt.Rows.Count > 0)
         {
             string MYGROUP = Get_woId_Info(dt.Rows[0]["WOID"].ToString()).Rows[0]["OUTPUTGROUP"].ToString();
             foreach (DataRow dr in dt.Rows)
             {
                 if (CheckRoute(dr["ESN"].ToString(), MYGROUP) == "OK")
                     dtUpload.ImportRow(dr);
             }
         }

         return dtUpload;
            
       }
       /// <summary>
       /// 获取入库编号
       /// </summary>
       /// <param name="Facid"></param>
       /// <returns></returns>
       public string GetStockInNumber()
       {
           string C_SEQ = string.Empty;
           string PRGNAME = "STOCKIN";
           string table = "sfcb.sequence";
           string fieldlist = "current_value,increment";
           int count = 0;
           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("NAME", PRGNAME);
           DataTable dtSEQ = dp.GetData(table, fieldlist, mst, out count).Tables[0];

           C_SEQ = "SFS" + dtSEQ.Rows[0][0].ToString().PadLeft(8, '0'); 
           mst = new Dictionary<string, object>();
           mst.Add("current_value", Convert.ToInt32(dtSEQ.Rows[0][0].ToString()) + Convert.ToInt32(dtSEQ.Rows[0][1].ToString()));
           mst.Add("NAME", PRGNAME);
           dp.UpdateData(table, new string[] { "NAME" }, mst);

           return C_SEQ;
           
       }

       public void UPDATE_STOCK_NO(string STOCK_NO,string WOID,string WIPSTATION,string ESN)
       {         

           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("STORENUMBER", STOCK_NO);
           mst.Add("WOID", WOID);
           mst.Add("WIPSTATION", WIPSTATION);
           mst.Add("ESN", ESN);
           dp.UpdateData("sfcr.t_wip_tracking_online", new string[] { "WOID", "WIPSTATION", "ESN" }, mst);
       }

       public void UPDATE_WIPSTATION(string STOCK_NO,string WIP_STATION)
       {

           IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("STORENUMBER", STOCK_NO);
           mst.Add("WIPSTATION", string.IsNullOrEmpty(WIP_STATION) ? "1003" : WIP_STATION);
           dp.UpdateData("sfcr.t_wip_tracking_online", new string[] { "STORENUMBER" }, mst);
       }

       public string TEST_MAIN_ONLY(string DATA, string MYGROUP, string EMP, string EC, string LINE)
       {
           string _StrErr = string.Empty;         
           try
           {
               Dictionary<string, object> dic = new Dictionary<string, object>();
               dic.Add("DATA", DATA);
               dic.Add("MYGROUP", MYGROUP);
               dic.Add("EMP", EMP);
               dic.Add("EC", EC);
               dic.Add("LINE", LINE);

               _StrErr = _DbPro.ExecuteProcedure("PRO_TEST_MAIN_ONLY", MapListConverter.DictionaryToJson(dic));           
               return _StrErr;
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }

       public DataTable GetStockInPrint(string StockIn)
       {
           int count = 0;
           string table = "SFCR.T_WIP_TRACKING_ONLINE a ";
           string fieldlist = "a.woId, a.partnumber, a.wipstation, a.productname, count(a.esn) qty";
           string filter = "a.storenumber = {0} ";
           string group = "a.woId, a.partnumber, a.wipstation, a.productname";

           IDictionary<string, object> mst = new Dictionary<string, object>();
           mst.Add("storenumber", StockIn);
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, group, out count).Tables[0];
       }

       //public void Insert_SAP_BackFlush(string STOCK_NO,string WOID,string PARTNUMBER,string PRODUCTNAME,int QTY)
       //{
       //    string Plant =  Get_woId_Info(WOID).Rows[0]["factoryid".ToUpper()].ToString();

       //    OracleCommand cmd = new OracleCommand();
       //    cmd.CommandText = "SELECT * FROM SFCR.Z_WHS_SAP_BACKFLUSH WHERE LOTIN=:C_LOTIN ";
       //    cmd.Parameters.AddRange(new OracleParameter[]
       //     {
       //         new OracleParameter("C_LOTIN",OracleDbType.Varchar2){Value=STOCK_NO}  
       //     });
       //    if (ExecuteDataSet(cmd, WMS_conn).Tables[0].Rows.Count == 0)
       //    {

       //        cmd = new OracleCommand();
       //        cmd.CommandText = "INSERT INTO SFCR.Z_WHS_SAP_BACKFLUSH (WOID,PARTNUMBER,PRODUCTNAME,LOTIN,LOTIN_QTY,LOTOUT,LOTOUT_QTY,PLANT,MOVE_TYPE,UPLOAD_FLAG,UPLOAD_DATE) VALUES (:sMO,:PartNo,:Porduct,:LTIN,:LTQTY,:LTOUT,:LTOUTQTY,:sPlan,:sMove,:sUpFlag,sysdate)";
       //        cmd.Parameters.AddRange(new OracleParameter[]
       //     {
       //         new OracleParameter("sMO",OracleDbType.Varchar2){Value=WOID},            
       //         new OracleParameter("PartNo",OracleDbType.Varchar2){Value=PARTNUMBER},         
       //         new OracleParameter("Porduct",OracleDbType.Varchar2){Value=PRODUCTNAME},
       //         new OracleParameter("LTIN",OracleDbType.Varchar2){Value=STOCK_NO},
       //         new OracleParameter("LTQTY",OracleDbType.Int32){Value=QTY},
       //         new OracleParameter("LTOUT",OracleDbType.Varchar2){Value="NA"},
       //         new OracleParameter("LTOUTQTY",OracleDbType.Int32){Value=0},
       //         new OracleParameter("sPlan",OracleDbType.Varchar2){Value=string.IsNullOrEmpty(Plant)?"2100":Plant},
       //         new OracleParameter("sMove",OracleDbType.Varchar2){Value="101"},
       //         new OracleParameter("sUpFlag",OracleDbType.Varchar2){Value="N"}         

       //     });
       //        ExecteNonQuery(cmd, WMS_conn);
       //    }
       //}
       public void Insert_SAP_BackFlush(string STOCK_NO,string WOID,string PARTNUMBER,string PRODUCTNAME,int QTY)
       {
        string _StrErr=   zBackFlush.Insert_SAP_BackFlush(STOCK_NO, WOID, PARTNUMBER, PRODUCTNAME, QTY);
        if (_StrErr != "OK")
            throw new Exception(_StrErr);
       }

       /// <summary>
       /// 返回一个新的datatable
       /// </summary>
       /// <param name="dt"></param>
       /// <param name="sql"></param>
       /// <returns></returns>
       public  DataTable getNewTable(DataTable dt, string sql)
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

       public string CheckRoute(string ESN, string MYGROUP)
       {         
         return  _DbPro.PRO_CHECK_ROUTE(ESN,MYGROUP);           
       }
       public string CHECK_SET_LINE_EMPLOYEE(IDictionary<string,object> dic)
       {
          return user.CHECK_SET_LINE_EMPLOYEE(dic);
       }
    }
}
