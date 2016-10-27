using System;
using System.Collections.Generic;
using System.Text;
using GenericProvider;
using GenericUtil;

namespace BLL
{
    public partial class B_SNRULE_PARTNUMBER
    {
        public B_SNRULE_PARTNUMBER()
        {
        }

        public System.Data.DataSet GetB_SNRULE_PARTNUMBER(string PartNo)
        {

               string table = "SFCB.B_SNRULE_PARTNUMBER";
                string fieldlist = @"PARTNUMBER,RULE_TYPE,VERSION_CODE,CUST_PARTNUBMER,CUST_VERSION_CODE,CUST_PARTNUMBER_DESC,UPCEANDATA,CUST_SN_PREFIX,
                           CUST_VENDER_CODE,CUST_SN_POSTFIX,CUST_SN_LENG,CUST_SN_STR,CUST_LAST_SN,CUST_BOX_PREFIX,CUST_BOX_LENG,CUST_BOX_STR,CUST_LAST_BOX,BOX_LAB_NAME,CUST_CARTON_PREFIX,
                           CUST_CARTON_POSTFIX,CUST_CARTON_LENG,CUST_CARTON_STR,CUST_LAST_CARTON,CARTON_LAB_NAME,CUST_PALLET_PREFIX,CUST_PALLET_POSTFIX,CUST_PALLET_LENG,CUST_PALLET_STR,
                            CUST_LAST_PALLET,PALLET_LAB_NAME,IN_STATION_TIME,EMP_NO";
                int count = 0;
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("PARTNUMBER", PartNo);
                return dp.GetData(table, fieldlist, mst, out count);                
            
        }
        public System.Data.DataSet GetALLB_SnRule_PartNumber()
        {
            string fieldlist = @"PARTNUMBER,RULE_TYPE,VERSION_CODE,CUST_PARTNUBMER,CUST_VERSION_CODE,CUST_PARTNUMBER_DESC,UPCEANDATA,CUST_SN_PREFIX,
                            CUST_VENDER_CODE,CUST_SN_POSTFIX,CUST_SN_LENG,CUST_SN_STR,CUST_LAST_SN,CUST_BOX_PREFIX,CUST_BOX_LENG,CUST_BOX_STR,CUST_LAST_BOX,BOX_LAB_NAME,CUST_CARTON_PREFIX,
                            CUST_CARTON_POSTFIX,CUST_CARTON_LENG,CUST_CARTON_STR,CUST_LAST_CARTON,CARTON_LAB_NAME,CUST_PALLET_PREFIX,CUST_PALLET_POSTFIX,CUST_PALLET_LENG,CUST_PALLET_STR,
                            CUST_LAST_PALLET,PALLET_LAB_NAME,IN_STATION_TIME,EMP_NO";
            string table = "SFCB.B_SNRULE_PARTNUMBER";

            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);             
            return dp.GetData(table, fieldlist, null, out count);  
        }

        public string InsertB_SNRULE_PARTNUMBER(string dicstring)
        {
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
                dp.AddData("SFCB.B_SNRULE_PARTNUMBER", mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateB_SNRULE_PARTNUMBER(string dicstring, int flag)
        {
            try
            {
                string[] TableKey = new string[1];
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
                if (flag == 0)
                {
                    TableKey[0] = "PARTNUMBER";
                    mst.Remove("RULE_TYPE");
                }
                else
                {
                    TableKey[0] = "RULE_TYPE";
                    mst.Remove("PARTNUMBER");
                }

                dp.UpdateData("SFCB.B_SNRULE_PARTNUMBER", TableKey, mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateCustPalletCartonNo(string DicString,string UpdateData,int Flag)
        {
            //Flag 0 SN;1 BOX; 2 CARTON;3;PALLET
            
            try
            {
                List<string> TableKey = new List<string>();
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = MapListConverter.JsonToDictionary(DicString);
                if (mst["RULE_TYPE"].ToString() == "NA")
                {
                    TableKey.Add("PARTNUMBER");
                    mst.Remove("RULE_TYPE");
                }
                else
                {
                    TableKey.Add("RULE_TYPE");
                    mst.Remove("PARTNUMBER");
                }
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

                dp.UpdateData("SFCB.B_SNRULE_PARTNUMBER", TableKey.ToArray(), mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
