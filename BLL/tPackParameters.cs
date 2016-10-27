using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using GenericUtil;
using SystemObject;

namespace BLL
{
    public partial class tPackParameters
    {

        public tPackParameters()
        {

        }

        string table = "SFCB.B_PACK_PARAMETERS";
        public System.Data.DataSet GetPackParameters(string Model)
        {           

            string fieldlist = "PartNumber,VersionCode,TrayQty,CartonQty,PalletQty,RecDate".ToUpper();
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(Model))
                mst.Add("PARTNUMBER", Model);
            return dp.GetData(table, fieldlist,mst, out count);

        }       

        public void InsertPackParameters(string dicstring)
        {           
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> dic = MapListConverter.JsonToDictionary(dicstring);
            dic.Add("RECDATE",System.DateTime.Now);
            dp.AddData(table, dic);
        }

        public void UpdatePackParameters(string dicstring)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            mst.Add("RECDATE",System.DateTime.Now);
            dp.UpdateData(table, new string[] { "PARTNUMBER" }, mst);
        }

        public void DeletePackParameters(string partnumber)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("PARTNUMBER", partnumber);
            dp.DeleteData(table, mst);
        }
        public System.Data.DataSet GetPackParametersByWoid(string woid)
        {
            int count = 0;
            string table = "sfcb.b_pack_parameters p,sfcr.t_wo_info w".ToUpper();
            string fieldlist = "p.PartNumber,VersionCode,TrayQty,CartonQty,PalletQty,p.RecDate".ToUpper();
            string filter = "p.PartNumber=w.partnumber and w.woId={0}".ToUpper();          
           
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("WOID", woid);
            return TransactionManager.GetData(table, fieldlist, filter, mst, null, null, out count);

        }
    }
}
