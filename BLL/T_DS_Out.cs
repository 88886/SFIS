using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SystemObject;
using System.Data.Common;
using GenericProvider;
using SrvComponent;

namespace BLL
{
    public class T_DS_Out
    {
        /// <summary>
        /// 按号码类型查询资料
        /// </summary>
        /// <param name="SN_Type"></param>
        /// <param name="SN"></param>
        /// <returns></returns>
        public DataSet Sel_Product_Info(string SN_Type, string SN)
        {
           
            int count = 0;
            string Fileds = @"ESN,SN,MAC,IMEI,CARTONNUMBER,PALLETNUMBER,TRAYNO,PARTNUMBER,STOREHOUSEID,STATUS";
            Dictionary<string,object> dic = new Dictionary<string,object>();
            dic.Add(SN_Type,SN);
           return TransactionManager.GetData("SFCR.Z_WHS_TRACKING",Fileds,null,dic,null,null,out count);

        }

    
        public string GET_OutCode()
        {
            try
            {

                string C_SEQ = string.Empty;
                if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.MySql)
                {
                    string PRGNAME = "STOCKOUT";              
                    int count = 0;

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("name", PRGNAME);
                    DataTable dtSEQ = TransactionManager.GetData("sfcb.sequence", "current_value", null, dic, null, null, out count).Tables[0];
                                 
                    C_SEQ = "SFS" + System.DateTime.Now.ToString("yyyyMMdd") + dtSEQ.Rows[0][0].ToString().PadLeft(5, '0');               
                    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                    dic = new Dictionary<string, object>();
                    dic.Add("current_value", Convert.ToInt32(dtSEQ.Rows[0][0].ToString()) + Convert.ToInt32(dtSEQ.Rows[0][1].ToString()));
                    dic.Add("NAME", PRGNAME);
                    dp.UpdateData("sfcb.sequence", new string[] { "NAME" }, dic);

                 
                }
                if (ProConfiguration.GetConfig().DatabaseType == SysModel.DataLinkType.Oracle)
                {
                    string table = "DUAL";
                    string fieldlist = "'SFC_DS' || TO_CHAR(SYSDATE, 'YYMMDD') ||  LPAD(TO_CHAR(SEQ_STOCKOUT.NEXTVAL), 5, '0')";
                    int count = 0;
                    IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                     
                    DataSet ds = dp.GetData(table, fieldlist, null, out count);
                    C_SEQ = ds.Tables[0].Rows[0][0].ToString();
                }

                return C_SEQ;
            }
            catch
            {
                return "ERROR";
            }
        }



        public DataSet Get_DSInfo(DateTime rec_Sta, DateTime rec_End, out string status)
        {
            int count = 0;
            string table = "SFCR.Z_SHOPEX_SALES a,SFCR.Z_SHOPEX_SALES_DETAIL b";
            string fieldlist = "b.PARTNUMBER,b.PRODUCTDESC,sum( b.NUMS) as Pat_Count";
            string filter = "a.ORDER_NO=b.ORDER_NO and a.FLAG=1 and a.RECDATE>={0}  and a.RECDATE<={1}";
            string group = "b.PARTNUMBER,b.productdesc";
            
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("RECDATE", rec_Sta);
            mst.Add("RECDATE1", rec_End);
            status = "OK";
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, group, out count);

            
        }      

    }
}
