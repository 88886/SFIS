using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

namespace TestWeserver
{
    /// <summary>
    /// tPublicStoredproc 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class tPublicStoredproc : System.Web.Services.WebService
    {
        BLL.ProPublicStoredproc ProPubStor = new BLL.ProPublicStoredproc();
        BLL.tLineInfo mLineinfo = new BLL.tLineInfo();
        BLL.tWoInfo woinfo = new BLL.tWoInfo();
        BLL.Db_Procedure mPro = new BLL.Db_Procedure();
        BLL.tCraftInfo craftinfo = new BLL.tCraftInfo();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public  string SP_PublicStoredproc(string Storedproc, DataTable dt) //wince使用
        {           
            return mPro.PublicStationPro(Storedproc, dt);          
        }
        [WebMethod]
        public string ExecuteProcedure(string Storedproc, string dicstring)
        {
            return mPro.ExecuteProcedure(Storedproc, dicstring);
        }
        [WebMethod]
        public DataSet GetListStationType()
        {
            return ProPubStor.GetListStationType();
        }

        [WebMethod]
        public  DataSet GetStoredprocValues(string Pro)  //查询存储过程需要的参数
        {
            return ProPubStor.GetStoredprocValues(Pro);
        }

        [WebMethod]
        public DataSet GetSystemInputData()  //查询出系统所需要的参数
        { 
         return ProPubStor.GetSystemInputData();
        }

        [WebMethod]
        public DataSet GetLineList()
        {
            return mLineinfo.GetAllLineInfo();
        }
        [WebMethod]
        public List<string> Get_Line_List()
        {
            return mLineinfo.GetLineList();
        }      
        [WebMethod(Description = "获取ListStation")]
        public List<string> GetAllStation(string woId)
        {
            List<string> LsStation = new List<string>();
           DataSet ds = woinfo.GetWOCraftInfo_ds(woId, "0");
           if (ds != null)
           {
               DataTable dt =ds.Tables[0];
               if (dt.Rows.Count > 0)
               {
                   DataView dv = new DataView(dt);
                   dv.Sort = dt.Columns[0].ToString();
                   DataTable dt2 = dv.ToTable();
                   foreach (DataRow dr in dt2.Rows)
                   {
                       LsStation.Add(dr[0].ToString());
                   }
               }
           }
          ds = woinfo.GetWOCraftInfo_ds(woId, "3");
           if (ds != null)
           {
               DataTable dt = ds.Tables[0];
               if (dt.Rows.Count > 0)
               {
                   DataView dv = new DataView(dt);
                   dv.Sort = dt.Columns[0].ToString();
                   DataTable dt2 = dv.ToTable();
                   foreach (DataRow dr in dt2.Rows)
                   {
                       LsStation.Add(dr[0].ToString());
                   }
               }
           }
            return LsStation;
        }
        [WebMethod(Description = "直接获取ListStation")]
        public List<string> Get_All_Station()
        {
            List<string> ListStation = new List<string>();
            Dictionary<string, object> dic = new Dictionary<string, object>();        
           DataTable dt = craftinfo.GetAllCraftInfo(dic).Tables[0];
           if (dt.Rows.Count > 0)
           {
               DataView dv = new DataView(dt);
               dv.Sort = "CRAFTNAME";
               DataTable dt2 = dv.ToTable();
               foreach (DataRow dr in dt2.Rows)
               {
                   if (dr["TESTFLAG"].ToString() == "0" || dr["TESTFLAG"].ToString() == "3")
                   ListStation.Add(dr["CRAFTNAME"].ToString());
               }
           }
           return ListStation;
        }
        [WebMethod]
        public List<string> GetWoList()
        {
            return woinfo.GetWoList();
        }
        [WebMethod]
        public string GetStockInNumber()
        {
            return ProPubStor.GetStockInNumber();
        }
        [WebMethod]
        public DataSet GetSystemStoredproc(int StationId)
        {
            return ProPubStor.GetSystemStoredproc(StationId);
        }
        [WebMethod]
        public List<string> SP_PublicStoredprocParam(string Storedproc,string dicstring) //pc dct 使用
        {
            return mPro.PublicStationProParam(Storedproc, dicstring);
        }
        [WebMethod]
        public DataSet GetStoredProcValuesParam(string Pro)
        {
            return ProPubStor.GetStoredProcValuesParam(Pro);
        }
        [WebMethod]
        public string CHECK_ROUTE(string DATA, string MYGROUP)
        {
           return mPro.PRO_CHECK_ROUTE(DATA, MYGROUP);
        }
        [WebMethod(MessageName = "ESN")]
        public string SP_TEST_MAIN_ONLY(string DATA, string MYGROUP, string EMP, string EC, string LINE)
        {
            string C_RES = string.Empty;
            mPro.PRO_TEST_MAIN_ONLY(DATA, MYGROUP, "NA", MYGROUP+"1", EMP, EC, LINE, out C_RES);
            return C_RES;          
        }
        [WebMethod(MessageName = "List_ESN")]
        public string SP_TEST_MAIN_ONLY(List<string> LsDATA, string MYGROUP, string EMP, string EC, string LINE)
        {
            string C_RES = string.Empty;
            foreach (string str in LsDATA)
            {
                mPro.PRO_TEST_MAIN_ONLY(str, MYGROUP, "NA", MYGROUP + "1", EMP, EC, LINE, out C_RES);
                if (C_RES != "OK")
                    return C_RES;
            }
            return C_RES;
        }
        [WebMethod]
        public string SP_TEST_CTN_PALT_TRAY(string DATA, string MYGROUP, string EMP, string EC, string LINE, string LOCDATA, string CUTDATA, int Flag)
        {
            return mPro.PRO_TEST_CTN_PALT_TRAY(DATA, MYGROUP, EMP, EC, LINE, LOCDATA, CUTDATA, Flag);       
        }
        [WebMethod]
        public string SP_TEST_CTN_PALT_TRAY_NEW(List<string> DATA, string MYGROUP, string EMP, string EC, string LINE, string LOCDATA, string CUTDATA, int Flag)
        {
            return mPro.SP_TEST_CTN_PALT_TRAY_NEW(DATA, MYGROUP, EMP, EC, LINE, LOCDATA, CUTDATA, Flag);
        }
        [WebMethod]
        public string GetPalletNumber(string Facid, string Line)
        {
            return mPro.PRO_GETSEQPALLET(Facid, Line);

            // return PPS.SP_GetPalletNumber(Facid,Line);
        }
    }
}
