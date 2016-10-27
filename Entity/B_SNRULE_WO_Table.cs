using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class B_SNRULE_WO_Table
    {
        public string WOID { get; set; }
        public string CUST_SN_PREFIX { get; set; }
        public string CUST_SN_POSTFIX { get; set; }
        public int CUST_SN_LENG { get; set; }
        public string CUST_SN_STR { get; set; }
        public string CUST_LAST_SN { get; set; }
        public string CUST_BOX_PREFIX { get; set; }
        public int CUST_BOX_LENG { get; set; }
        public string CUST_BOX_STR { get; set; }
        public string CUST_LAST_BOX { get; set; }
        public string BOX_LAB_NAME { get; set; }
        public string CUST_CARTON_PREFIX { get; set; }
        public string CUST_CARTON_POSTFIX { get; set; }
        public int CUST_CARTON_LENG { get; set; }
        public string CUST_CARTON_STR { get; set; }
        public string CUST_LAST_CARTON { get; set; }
        public string CARTON_LAB_NAME { get; set; }
        public string CUST_PALLET_PREFIX { get; set; }
        public string CUST_PALLET_POSTFIX { get; set; }
        public int CUST_PALLET_LENG { get; set; }
        public string CUST_PALLET_STR { get; set; }
        public string CUST_LAST_PALLET { get; set; }
        public string PALLET_LAB_NAME { get; set; }
        public DateTime IN_STATION_TIME { get; set; }
        public string EMP_NO { get; set; }
        public string CUST_END_CARTON { get; set; }

    }
}
