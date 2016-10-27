using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DAL;
 using MySql.Data.MySqlClient;
using GenericProvider;
using SystemObject;

namespace BLL
{
    public partial class BllMsSqllib : DAL.IMsSqlLib
    {
        BllMsSqllib()
        {
            msqllib = new MsSqlLib(BLL.ServerConfig.Instance.ConnStr);
        }
        #region 静态成员
        private readonly static BLL.BllMsSqllib instance = new BllMsSqllib();

        public static BLL.BllMsSqllib Instance
        {
            get { return BllMsSqllib.instance; }
        }
        static DAL.MsSqlLib msqllib = null;
        static BllMsSqllib()
        {
            if (msqllib == null)
                msqllib = new MsSqlLib(BLL.ServerConfig.Instance.ConnStr);

        }
        #endregion
        #region IMsSqlLib 成员

        //public void CloseDataBase()
        //{
        //    msqllib.CloseDataBase();
        //}

        //public void ConnectDataBase()
        //{
        //    msqllib.ConnectDataBase();
        //}

        //public void ExecteNonQuery(MySqlCommand cmd)
        //{
        //    msqllib.ExecteNonQuery(cmd);
        //}
        //public void ExecteNonQueryArr(List<MySqlCommand> cmd)
        //{
        //    msqllib.ExecteNonQueryArr(cmd);
        //}
        //public string ExecteNonQueryTransaction(List<MySqlCommand> cmd)
        //{
        //    return msqllib.ExecteNonQueryTransaction(cmd);
        //}

        //public DataSet ExecuteDataSet(MySqlCommand cmd)
        //{
        //    return msqllib.ExecuteDataSet(cmd);
        //}
        public DataSet ExecuteDataSet(string Sql, Dictionary<string, string> dic, string _DbString)
        {
            return msqllib.ExecuteDataSet(Sql, dic, _DbString);
        }
        //public string[] GetMaxSn()
        //{
        //    return msqllib.GetMaxSn();
        //}  

        //public DataTable gettb(string sql, int start, int count, string tablename)
        //{
        //    return msqllib.gettb(sql, start, count, tablename);
        //} 

        //public string InsertProcut(Entity.tProduct product, List<Entity.tProductSerialInfo> lsserialinfo, Entity.tPackParametersTable palletinfo)
        //{
        //    return msqllib.InsertProcut(product, lsserialinfo, palletinfo);
        //}

        //public string InsertRouteAtt(Entity.tRouteAtt routeatt)
        //{
        //    return msqllib.InsertRouteAtt(routeatt);
        //}

        //public string InsertRouteCraftParamerter(Entity.tRoutCraftparameter routecraftpara)
        //{
        //    return msqllib.InsertRouteCraftParamerter(routecraftpara);
        //}      

        //public void InsertSmtKPDetalt(Entity.SMT_KP_DETALT KPDetalt, out string Err)
        //{
        //    msqllib.InsertSmtKPDetalt(KPDetalt, out Err);
        //}

        //public string InsertSmtKpDetaltForWo(Entity.SMT_KP_DETALTForWo DETALTFORWO)
        //{
        //    return msqllib.InsertSmtKpDetaltForWo(DETALTFORWO);
        //}

        //public void InsertSmtKPMarster(Entity.SMT_KP_MASTER KPMarster, out string marsterId, out string Err)
        //{
        //    msqllib.InsertSmtKPMarster(KPMarster, out  marsterId, out Err);
        //}

        //public void InsertSnRule(Entity.tSnRule snrule)
        //{
        //    msqllib.InsertSnRule(snrule);
        //}
       
        //public string InsertWoInfo(Entity.T_WO_INFO twi, string esn, List<Entity.WoSnRule> snrule)
        //{
        //    return msqllib.InsertWoInfo(twi, esn, snrule);
        //}

        //public string PublicStationPro(string Storedproc, DataTable dt)
        //{
        //    return msqllib.PublicStationPro(Storedproc, dt);
        //}
        //public string ExecuteProcedure(string Storedproc,List<Entity.ProcedureKey> EPK)
        //{
        //    return msqllib.ExecuteProcedure(Storedproc,EPK);
        //}

        //public List<string> PublicStationProParam(string Storedproc, DataTable dt)
        //{
        //    return msqllib.PublicStationProParam(Storedproc, dt);
        //}

        //public void SP_InsertStorehousehadRecount(Entity.tPartStorehousehad trsn, out string err)
        //{
        //    msqllib.SP_InsertStorehousehadRecount(trsn, out err);
        //}

        //public object sqlExecuteScalar(MySqlCommand cmd)
        //{
        //    return msqllib.sqlExecuteScalar(cmd);
        //}

        //public string GetPalletNumber(string Facid,string Line)
        //{
        //    return msqllib.GetPalletNumber(Facid,Line);
        //}
        //public string SP_TEST_MAIN_ONLY(string DATA, string MYGROUP, string EMP, string EC, string LINE)
        //{
        //    return msqllib.SP_TEST_MAIN_ONLY(DATA, MYGROUP, EMP, EC, LINE);
        //}      
        //public string SP_TEST_STOCKIN(string DATA, string MYGROUP, string EMP, string EC, string LINE)
        //{
        //    return msqllib.SP_TEST_STOCKIN(DATA, MYGROUP, EMP, EC, LINE);
        //}  

        //public string InsertCartonInfo(Entity.tCartonInfo cartioninfo)
        //{
        //    return msqllib.InsertCartonInfo(cartioninfo);
        //}

        //public string UpdateWipAndRecCartonBox(Entity.tWipTrackingTable wiptrack)//(string cartonId, string mcartionId, string palletnumber, string mpalletnumber, string trayno, string line, string mygroup, string esn, string userid, string flag)
        //{
        //    return msqllib.UpdateWipAndRecCartonBox(wiptrack);
        //}   

      
        #endregion

       
    
        //public System.Data.DataSet TablePublicSelect(string Table, string Fileds, string sFilters, MySqlParameter[] Parameter, IList<OrderKey> OrderKeys = null)
        //{
        //    return msqllib.TablePublicSelect(Table, Fileds, sFilters, Parameter, OrderKeys);
        //}
        //public string PublicUpdateExecteNonQuery(string Table, string Fileds, string sFilters, MySqlParameter[] Parameter)
        //{
        //    return msqllib.PublicUpdateExecteNonQuery(Table, Fileds, sFilters, Parameter);
        //}
        //public string PublicInsertExecteNonQuery(string Table, string Fileds, string sFilters, MySqlParameter[] Parameter)
        //{
        //    return msqllib.PublicInsertExecteNonQuery(Table, Fileds, sFilters, Parameter);
        //}
        //public string PublicDeleteExecteNonQuery(string Table, string Fileds, MySqlParameter[] Parameter)
        //{
        //    return msqllib.PublicDeleteExecteNonQuery(Table,Fileds,Parameter);
        //}

        //public string SaveERPInfo(DataTable dt)
        //{
        //    return BLL.BllMsSqllib.msqllib.SaveERPInfo(dt);
        //}

        //public string SaveERPItem(DataTable dt)
        //{
        //    return BLL.BllMsSqllib.msqllib.SaveERPItem(dt);
        //}
    
        //public string ExecteNonUpd(MySqlCommand cmdSel, MySqlCommand cmdIns, MySqlCommand cmdUpd)
        //{
        //    return msqllib.ExecteNonUpd(cmdSel, cmdIns, cmdUpd);
        //}
        public string ExecteNonUpd(int pring_qty, string _DbString)
        {
            return msqllib.ExecteNonUpd(pring_qty, _DbString);
        }
        
    }

    public class AllFuntion
    {
        public AllFuntion()
        {

        }
        public string GetSeqBasics()
        {    
         
            try
            {
              //  MySqlCommand cmd = new MySqlCommand();
              ////  cmd.CommandText = "select 'B' ||TO_CHAR(SYSDATE, 'IIW') from dual";
              //  cmd.CommandText = "select  DATE_FORMAT(NOW(),'%y') ,LPAD(weekofyear(curdate()),2,0);";
              //  DataTable dt = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];

             
                string fieldlist = "DATE_FORMAT(NOW(),'%y') ,LPAD(weekofyear(curdate()),2,0)";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                DataTable dt = dp.GetData("DUAL", fieldlist, null, out count).Tables[0];
                string yyweek = "B" + dt.Rows[0][0].ToString() + dt.Rows[0][1].ToString();
           

                //cmd = new MySqlCommand();
                //cmd.CommandText = "SELECT MAX(CRAFTID) FROM SFCB.B_CRAFT_INFO WHERE CRAFTID LIKE @sSQE ";
                //cmd.Parameters.Add("sSQE", MySqlDbType.VarChar).Value = yyweek + "%";
                //DataTable dtseq = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];   

               
                 fieldlist = "MAX(CRAFTID)";
                 string filter = " CRAFTID LIKE {0}";              
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("CRAFTID", yyweek + "%");
                DataTable dtseq  = TransactionManager.GetData("SFCB.B_CRAFT_INFO", fieldlist, filter, mst, null, null, out count).Tables[0];

                if (dtseq.Rows.Count > 0)
                {
                    string OldNumber=dtseq.Rows[0][0].ToString();
                    if (!string.IsNullOrEmpty(OldNumber))
                    {
                        return yyweek + ((Convert.ToInt32(OldNumber.Replace(yyweek, "000")) + 1).ToString()).PadLeft(3,'0');
                    }
                    else
                    {
                        return yyweek + "001";
                    }
                }
                else
                {
                    return  yyweek+"001";
                }
            }
            catch (Exception ex)
            {
                return ex.Message ;
            }
        }
        public string GetSqlSequence()
        {
            string C_SEQ = string.Empty;
            string PRGNAME = "SEQ_NEWSEQVALUE";
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandText = "select current_value from sfcb.sequence where name=@PRGNAME";
            //cmd.Parameters.Add("PRGNAME", MySqlDbType.VarChar).Value = PRGNAME;
            //DataTable dtSEQ = BLL.BllMsSqllib.Instance.ExecuteDataSet(cmd).Tables[0];
            string fieldlist = "current_value,increment";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            Dictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("name", PRGNAME);
            DataTable dtSEQ = dp.GetData("sfcb.sequence", fieldlist, mst, out count).Tables[0];

            C_SEQ = "S" + dtSEQ.Rows[0]["current_value"].ToString().PadLeft(6, '0');
            //cmd = new MySqlCommand();
            //cmd.CommandText = "update sfcb.sequence set current_value=current_value+increment where name=@PRGNAME";
            //cmd.Parameters.Add("PRGNAME", MySqlDbType.VarChar).Value = PRGNAME;
            //BLL.BllMsSqllib.Instance.ExecteNonQuery(cmd);
            mst = new Dictionary<string, object>();
            mst.Add("current_value", Convert.ToInt32(dtSEQ.Rows[0]["current_value"].ToString()) + Convert.ToInt32(dtSEQ.Rows[0]["increment"].ToString()));
            mst.Add("NAME", PRGNAME);
            dp.UpdateData("sfcb.sequence", new string[] { "NAME" },mst);

            return C_SEQ;
       
        }
       
    }
}
