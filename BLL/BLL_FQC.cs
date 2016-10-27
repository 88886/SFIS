using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SystemObject;
using SrvComponent;

namespace BLL
{
    public class BLL_FQC
    {

        public BLL_FQC()
        {
        }

        /// <summary>
        /// 获取当天最大QCNO
        /// </summary>
        /// <param name="Dt">当天时间</param>
        /// <returns></returns>
        public DataSet Sel_QCNO_Date(DateTime Dt, string Line)
        {          

            int count = 0;
            string table = "sfcr.t_fqc_info".ToUpper();
            string fieldlist = "QC_NO,WO_ID,TRAY_NO,PALLET_NO,CARTON_ID,ESN,IS_PASS,ERROR_CODE,USERID,IN_STATION_TIME,REMARK";
            string filter = "WORK_DATE={0} and QC_No like {1}";             
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WORK_DATE", Dt.ToString("yyyyMMdd"));
            mst.Add("QC_No", Line + "%");
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }




//        /// <summary>
//        /// 存入检验数据
//        /// </summary>
//        /// <param name="QC_No">检验编码</param>
//        /// <param name="Wo_id">工单号</param>
//        /// <param name="Pallet_No">站板号</param>
//        /// <param name="Carton_id">卡通箱号</param>
//        /// <param name="Esn">ESN号</param>
//        /// <param name="Is_pass">是否良品</param>
//        /// <param name="Error_Code">不良代码</param>
//        /// <param name="In_station_time">进站时间</param>
//        /// <param name="User_id">工号</param>
//        /// <param name="Tray_no">Tray号</param>
        public void inser_FQCInfo(string QC_No, string Wo_id, string Pallet_No, string Carton_id, string Esn, bool Is_pass, string Error_Code, string User_id, string Tray_no, string qc_error_info)
        {

            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("QC_NO", QC_No);
            mst.Add("WO_ID", Wo_id);
            mst.Add("PALLET_NO", Pallet_No);
            mst.Add("CARTON_ID", Carton_id);
            mst.Add("ESN", Esn);
            mst.Add("IS_PASS", Is_pass ? "1" : "0");
            mst.Add("ERROR_CODE", Error_Code);
            mst.Add("USERID", User_id);
            mst.Add("TRAY_NO", Tray_no);
            mst.Add("REMARK", qc_error_info);
            mst.Add("WORK_DATE", DateTime.Now.ToString("yyyyMMdd"));
            dp.AddData("SFCR.T_FQC_INFO", mst);
        }


        public DataSet Sel_Fqc_report(string UserID,string Wo_ID,DateTime Dt_Sta,DateTime dt_End)  //ok
        {

                int count = 0;
                string table = "SFCR.T_FQCREPORT";
                string fieldlist = "QC_NO,QC_WO_ID,PRO_COUNT,QC_COUNT,ERROR_COUNT,USERID,IN_STATION_DATE,R_CHECKDATE,PALLETNUMBER,CHECK_NUMBER";
                string filter = " WORK_DATE>={0} and WORK_DATE<={1} ";

                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("WORK_DATE1", Dt_Sta.ToString("yyyyMMdd"));
                mst.Add("WORK_DATE2", dt_End.ToString("yyyyMMdd"));             
                int x = 1;
                if (!string.IsNullOrEmpty(UserID))
                {
                    x++;
                    filter += " AND USERID={"+x.ToString()+"} ";
                    mst.Add("USERID", UserID);
                }
                if (!string.IsNullOrEmpty(Wo_ID))
                {
                    x++;
                    filter += " AND QC_WO_ID={" + x.ToString() + "} ";
                    mst.Add("QC_WO_ID", Wo_ID);
                }
       
               return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
          
        }


        public DataSet Sel_Fqc_ErrorInfo(string UserID, string Wo_ID, DateTime Dt_Sta, DateTime dt_End)  //ok
        {           
            int count = 0;
            string table = "SFCR.t_FQC_INFO";
            string fieldlist = "QC_NO,WO_ID,TRAY_NO,PALLET_NO,CARTON_ID,ESN,IS_PASS,ERROR_CODE,USERID,IN_STATION_TIME,REMARK";
            string filter = " WORK_DATE>={0} WORK_DATE<={1}";
             
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("In_station_time1", Dt_Sta.ToString("yyyyMMdd"));
            mst.Add("In_station_time2", dt_End.ToString("yyyyMMdd"));
            if (!string.IsNullOrEmpty(UserID))
                mst.Add("USERID", UserID);
            if (!string.IsNullOrEmpty(Wo_ID))
                mst.Add("WO_ID", Wo_ID);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }


        /// <summary>
        /// 新增报表信息
        /// </summary>
        /// <param name="qc_no"></param>
        /// <param name="qc_Wo_ID"></param>
        /// <param name="Pro_Count"></param>
        /// <param name="QC_count"></param>
        /// <param name="?"></param>
        /// <param name="error_count"></param>
        /// <param name="UserID"></param>
        /// <param name="In_Station_date"></param>
        public void inser_report(string qc_no, string qc_Wo_ID, int Pro_Count, int QC_count, int error_count, string UserID, DateTime In_Station_date, int R_CHECKDATE, string palletnumber,int CHECK_NUMBER)  //ok
        {

            
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("QC_NO", qc_no);
                mst.Add("QC_WO_ID", qc_Wo_ID);
                mst.Add("PRO_COUNT", Pro_Count);
                mst.Add("QC_COUNT", QC_count);
                mst.Add("ERROR_COUNT", error_count);
                mst.Add("USERID", UserID);
                mst.Add("IN_STATION_DATE", In_Station_date);
                mst.Add("R_CHECKDATE", R_CHECKDATE);
                mst.Add("PALLETNUMBER", palletnumber);
                mst.Add("CHECK_NUMBER", CHECK_NUMBER);
                mst.Add("WORK_DATE", System.DateTime.Now.ToString("yyyyMMdd"));
                dp.AddData("SFCR.T_FQCREPORT", mst);               
        }
        public string Sel_QCNO()
        {
            string C_SEQ = string.Empty;
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
            {
               
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);            
               C_SEQ= DateTime.Now.ToString("yyyyMMdd") + dp.GetData("DUAL", "SEQ_FQC.NEXTVAL", null, out count).Tables[0].Rows[0][0].ToString();
            }
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)
            {

                string PRGNAME = "SEQ_FQC";            

                string table = "sfcb.sequence";
                string fieldlist = "current_value,increment";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("name", PRGNAME);
                DataTable dtSEQ = dp.GetData(table, fieldlist, mst, out count).Tables[0];

                C_SEQ = dtSEQ.Rows[0][0].ToString().PadLeft(6, '0');             
                mst = new Dictionary<string, object>();
                mst.Add("current_value", Convert.ToInt32(dtSEQ.Rows[0]["current_value"].ToString()) + Convert.ToInt32(dtSEQ.Rows[0]["increment"].ToString()));
                mst.Add("name", PRGNAME);
                dp.UpdateData("sfcb.sequence", new string[] { "name" }, mst);


                C_SEQ= DateTime.Now.ToString("yyyyMMdd") + C_SEQ;
            }

            return C_SEQ;
 
        }

        public DataSet Sel_RcheckInfo()  //ok
        {

            int count = 0;
            string table = "sfcr.t_fqcreport a";
            string fieldlist = "0 Ck,a.*";
            string filter = "NOW()";
            if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
                filter = "SYSDATE";
              filter += "-R_CHECKDATE >=IN_STATION_DATE and check_number=(select max(check_number) from sfcr.t_fqcreport where qc_no=a.qc_no)and QC_No in(select QA_NO from sfcr.z_whs_tracking where QA_NO=A.QC_NO AND (STATUS=1 OR STATUS=0))";
         
            IDictionary<string, object> mst = new Dictionary<string, object>();         
           return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }

    

    }
}
