using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using SystemObject;
using GenericUtil;

namespace BLL
{
    public partial class tKeyPart
    {
       
        public tKeyPart()
        {
            
        }
       
        public System.Data.DataSet GetKeyParts()
        {         
            int count = 0;
            string table = "SFCB.B_KEYPART a,SFCB.B_PART_KEYPARTS b";
            string fieldlist = "a.kpnumber,b.partnumber,a.kpname,a.kpdesc".ToUpper();
            string filter = "a.kpnumber = b.kpnumber";        
            return TransactionManager.GetData(table, fieldlist, filter, null, null, null, out count);

        }

        public int GetGetKeyPartsCount(string Kpnumber)
        {          
            string fieldlist = "kpnumber".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("kpnumber".ToUpper(), Kpnumber);
            return dp.GetData("SFCB.B_KEYPART", fieldlist, mst, out count).Tables[0].Rows.Count;
        }

        public System.Data.DataSet CheckDupPartsNumber(string KpNo, string PartNo)
        {           
            int count = 0;
            string table = "SFCB.B_KEYPART a,sfcb.b_part_keyparts b";
            string fieldlist = "a.kpnumber,b.partnumber,a.kpname,a.kpdesc";
            string filter = "a.kpnumber = b.kpnumber  and  b.partnumber={0}";     
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("partnumber", PartNo);
            if (!string.IsNullOrEmpty(KpNo))
            {
                filter += " and a.kpnumber={1}";
                mst.Add("kpnumber", KpNo);
            }
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);
        }

        public void InsertKeyParts(string dickpt)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dickpt);
            dp.AddData("SFCB.B_KEYPART", dic);
             
        }

        public void UpdateKeyParts(string dickpt)
        {            
             IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
             IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dickpt);
             dp.UpdateData("SFCB.B_KEYPART", new string[] { "KPNUMBER" }, mst);
        }
        public void DeleteKeyParts(string kpnumber)
        {        
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("kpnumber", kpnumber);
            dp.DeleteData("SFCB.B_KEYPART", mst);
        }
    }
}
