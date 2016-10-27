using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GenericProvider;

namespace BLL
{
    public class tB_MAIL_T
    {
        public tB_MAIL_T()
        {
        }

        public DataSet Get_B_MAIL_T(string Mail_App)
        {
            string table = "MESDB.B_MAIL_T";
            string fieldlist = "MAIL_APP,MAIL_TO,MAIL_TO_OTHER,MAIL_CC,MAIL_BCC,MAIL_SUBJECT,MAIL_CONTENT,MAIL_MANAGEMENT,MAIL_DOMAIN_NAME,RECDATE";
            int count = 0;
            IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
            IDictionary<string, object> mst = new Dictionary<string, object>();
            mst.Add("MAIL_APP", Mail_App);
          return dp.GetData(table, fieldlist, mst, out count);
           
        }
    }
}
