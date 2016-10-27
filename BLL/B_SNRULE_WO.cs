using System;
using System.Collections.Generic;
using System.Text;
using GenericProvider;
using GenericUtil;

namespace BLL
{
    public partial class B_SNRULE_WO
    {
        public B_SNRULE_WO()
        { 
        }

        string table = "SFCB.B_SNRULE_WO";
        public System.Data.DataSet GetB_SNRULE_WO(string woId)
        {
          
            string fieldlist = @"WOID,CUST_SN_PREFIX,CUST_SN_POSTFIX,CUST_SN_LENG,CUST_SN_STR,CUST_LAST_SN,CUST_BOX_PREFIX,CUST_BOX_LENG,CUST_BOX_STR,
                    CUST_LAST_BOX,BOX_LAB_NAME,CUST_CARTON_PREFIX,CUST_CARTON_POSTFIX,CUST_CARTON_LENG,CUST_CARTON_STR,CUST_LAST_CARTON,CARTON_LAB_NAME,CUST_PALLET_PREFIX,
                    CUST_PALLET_POSTFIX,CUST_PALLET_LENG,CUST_PALLET_STR,CUST_LAST_PALLET,PALLET_LAB_NAME,IN_STATION_TIME,EMP_NO,CUST_END_CARTON";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woId);
            return dp.GetData(table, fieldlist, mst, out count);      

        }
        public System.Data.DataSet GetALLB_SnRule_WO()
        {
            string Fileds = @"WOID,CUST_SN_PREFIX,CUST_SN_POSTFIX,CUST_SN_LENG,CUST_SN_STR,CUST_LAST_SN,CUST_BOX_PREFIX,CUST_BOX_LENG,CUST_BOX_STR,
                             CUST_LAST_BOX,BOX_LAB_NAME,CUST_CARTON_PREFIX,CUST_CARTON_POSTFIX,CUST_CARTON_LENG,CUST_CARTON_STR,CUST_LAST_CARTON,CARTON_LAB_NAME,CUST_PALLET_PREFIX,
                             CUST_PALLET_POSTFIX,CUST_PALLET_LENG,CUST_PALLET_STR,CUST_LAST_PALLET,PALLET_LAB_NAME,IN_STATION_TIME,EMP_NO,CUST_END_CARTON";
        
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);          
            return dp.GetData(table, Fileds, null, out count);      
        }


        public string InsertB_SNRULE_WO(string dicstring)
        {
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
                dp.AddData(table, mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string UpdateB_SNRULE_WO(string dicstring)
        {
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst  = MapListConverter.JsonToDictionary(dicstring);
                dp.UpdateData(table, new string[] { "WOID" }, mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string UpdateCustPalletCartonNo_WO(string dicstring, string UpdateData,int Flag)
        {
            ////Flag 0 SN;1 BOX; 2 CARTON;3;PALLET         

            try
            {
            
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
                switch (Flag)
                {
                    case 0:
                      
                        mst.Add("CUST_LAST_SN", UpdateData);
                        break;
                    case 1:
                    
                        mst.Add("CUST_LAST_BOX", UpdateData);
                        break;
                    case 2:
                      
                        mst.Add("CUST_LAST_CARTON", UpdateData);
                        break;
                    case 3:
                        
                        mst.Add("CUST_LAST_PALLET", UpdateData);
                        break;
                    default:

                        break;

                }             

                dp.UpdateData(table, new string[] { "WOID" }, mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
    }
 
}
