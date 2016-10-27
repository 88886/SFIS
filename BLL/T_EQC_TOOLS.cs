using System;
using System.Collections.Generic;
using System.Text;
using GenericProvider;
using SystemObject;

namespace BLL
{
    public class T_EQC_TOOLS
    {

        public T_EQC_TOOLS()
        {
        }
 
        string table = "MESDB.EQC_TOOLS";
        public System.Data.DataSet Get_T_EQC_TOOLS(string TOOSNO)
        {          
            string fieldlist = "TOOLS_TYPE,TOOLS_NO,MAX_AMOUNT,USE_QTY,STATUS,LOC_ID,RECDATE,REMARK";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("TOOLS_NO", TOOSNO);
            return  dp.GetData(table, fieldlist, mst, out count);
        }

        public string UPDATE_TOOLS_Use_Quantity(string TOOSNO)
        {
            try
            {
                StringBuilder ofilter = new StringBuilder();
                ofilter.Append(" USE_QTY=USE_QTY+1 ");
                IDictionary<string, object> modFields = new Dictionary<string, object>();
                string filter = "TOOLS_NO = {0}";
                IDictionary<string, object> keyVals = new Dictionary<string, object>();
                keyVals.Add("TOOLS_NO", TOOSNO);
                TransactionManager.UpdateBatchData(table, ofilter.ToString(), modFields, filter, keyVals);                
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string CHECK_TOOLS(string TOOSNO)
        {
            string _StrErr ="TOOLS ERROR";
            System.Data.DataTable dt = Get_T_EQC_TOOLS(TOOSNO).Tables[0];
            if (dt.Rows.Count > 0)
            {
                //0 待入库 1在库 2出库 3待维修 4维修中 5过期 6报废
                string STATUS = dt.Rows[0]["STATUS"].ToString();
                int MAX_AMOUNT = Convert.ToInt32(dt.Rows[0]["MAX_AMOUNT"].ToString());
                int USE_AMOUNT = Convert.ToInt32(dt.Rows[0]["USE_QTY"].ToString());
                if (USE_AMOUNT <= MAX_AMOUNT)
                {
                    switch (STATUS)
                    {
                        case "0":
                            _StrErr = "This Tools Waiting Put In Storage";
                            break;
                        case "1":
                            _StrErr = "This Tools In Storage";
                            break;
                        case "2":
                            _StrErr = "OK";
                            break;
                        case "3":
                            _StrErr = "This Tools Waiting In Repair ";
                            break;
                        case "4":
                            _StrErr = "This Tools Repairs In Progress";
                            break;
                        case "5":
                            _StrErr = "This Tools Has Expired";
                            break;
                        case "6":
                            _StrErr = "This Tools Already Scrap";
                            break;
                        default:
                            _StrErr = "TOOLS STATUS ERROR";
                            break;
                    }
                }
                else
                {
                    _StrErr = "Excess Use Amount";
                }
            }
            return _StrErr;
        }
 
 

    }
}
