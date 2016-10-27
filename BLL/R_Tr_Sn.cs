using System;
using System.Data;
using System.Collections.Generic;
using GenericProvider;
using System.Data.Common;
using SrvComponent;
using GenericUtil;

namespace BLL
{
    public class R_Tr_Sn
    {

        public R_Tr_Sn()
        {

        }
        private Object thisLock = new Object();
        /// <summary>
        /// 查找所有TR_SN的信息
        /// </summary>
        /// <param name="T_sn"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataSet Sel_Tr_Sn_Info(string T_sn, out string Status)
        {
            Status = "OK";
            try
            {
                string table = "SFCR.R_TR_SN";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("TR_SN", T_sn);
                return dp.GetData(table, "*", mst, out count);
            }
            catch (Exception ex)
            {
                Status = ex.Message;
                    return null;
            }

        }

        public DataSet Sel_woId_Trsn_List(string woId, string Flag)
        {
            string table = "SFCR.R_TR_SN";
            string fieldlist = "PO_ID,TR_SN,KP_NO,KP_DESC,VENDER_ID,VENDER_NAME,DATE_CODE,LOT_CODE,QTY,STOCK_ID,LOC_ID,TANSFER_NO,URGENT_PN,STOCK_NO,STOCK_DATE,WOID,USER_ID,STATUS,REMARK1,REMARK2,FIFO_DC";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            if (!string.IsNullOrEmpty(Flag))
                mst.Add("STATUS", Flag);
            return dp.GetData(table, fieldlist, mst, out count);

        }
 
        public string Del_TR_SN(string R_Trsn)
        {          
            try
            {
                string table = "SFCR.R_TR_SN";
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> parms = new Dictionary<string, object>();
                parms.Add("TR_SN", R_Trsn);
                dp.DeleteData(table, parms);
                return "OK";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }
        /// <summary>
        /// 获取TR_SN编码
        /// </summary>
        /// <returns></returns>
        public string GetSeqTrSnInfo()
        {        
            string Tr_Sn = Get_tr_sn_current(1);
            return  Tr_Sn.Substring(0, 1) + (Convert.ToInt64(Tr_Sn.Substring(1, Tr_Sn.Length - 1)) + 1).ToString();
         
        }
        
        public string Get_tr_sn_current(int pring_qty)
        {
            lock (thisLock)
            {            
                return BLL.BllMsSqllib.Instance.ExecteNonUpd(pring_qty, ProConfiguration.GetConfig().DatabaseConnect);
            }
        }

        /// <summary>
        /// insert into tr_sn资料
        /// </summary>
        /// <returns></returns>
        public string insert_into_R_tr_sn(IDictionary<string, object> mst)
        {
            string _StrErr = "";
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);   
                dp.AddData("sfcr.r_tr_sn", mst);
                _StrErr = "OK";
            }
            catch (Exception ex)
            {
                _StrErr = ex.Message;
            }
            return _StrErr;
        }
        public string insert_into_R_tr_sn_detail(IDictionary<string, object> mst)
        {
            string _StrErr = "";
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);              
                dp.AddData("sfcr.r_tr_sn_detail", mst);
                _StrErr = "OK";
            }
            catch (Exception ex)
            {
                _StrErr = ex.Message;
            }

            return _StrErr;

        }

        public System.Data.DataSet Get_Tr_Sn_Detail(string Tr_Sn)
        {           
            string table = "SFCR.R_TR_SN_DETAIL";
            string fieldlist = "PO_ID,TR_SN,KP_NO,VENDER_ID,VENDER_NAME,DATE_CODE,LOT_CODE,QTY,STOCK_ID,LOC_ID,TANSFER_NO,URGENT_PN,STOCK_NO,STOCK_DATE,WOID,USER_ID,STATUS,REMARK1,REMARK2,UPDATE_DATE,FIFO_DC ";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("TR_SN", Tr_Sn);
            return dp.GetData(table, fieldlist, mst, out count);
            
        }
    }
}
