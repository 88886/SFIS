using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GenericProvider;
using SystemObject;
using GenericUtil;

namespace BLL
{
    public partial class tBomKeyPart
    {

        public tBomKeyPart()
        {

        }
        string table = "SFCB.B_BOM_KEYPART";
        public List<string> GetBomNumberList()
        {
            List<string> Bomnumber = new List<string>();
            string fieldlist = "distinct bomnumber";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);

            System.Data.DataTable _dt = dp.GetData(table, fieldlist, null, out count).Tables[0];

            foreach (DataRow dr in _dt.Rows)
            {
                Bomnumber.Add(dr["bomnumber"].ToString());
            }
            return Bomnumber;
        }

        public System.Data.DataSet GetBomNoParts(string BomNumber)
        {
            int count = 0;
            string fieldlist = "kpnumber,kprelation,station";
            string filter = "bomnumber={0}";
            IList<OrderKey> order = new List<OrderKey>();
            OrderKey od1 = new OrderKey();
            od1.KeyName = "station,kprelation";
            od1.IsAsc = true;
            order.Add(od1);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("bomnumber", BomNumber);
            return TransactionManager.GetData(table, fieldlist, filter, mst, order, null, out count);
        }

        public void InsertBomNumber(string dicstring)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(dicstring);
            dp.AddData(table, mst);

        }
        public void DeleteBomNumber(string Bom)
        {
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("bomnumber", Bom);
            dp.DeleteData(table, mst);
        }
    }
}