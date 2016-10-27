using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace TestWeserver
{
    /// <summary>
    /// MoblieWeb 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class MoblieWeb : System.Web.Services.WebService
    {
        BLL.tWarehouseWipTracking whs = new BLL.tWarehouseWipTracking();

       
        //[WebMethod]
        //public List<string> ShippingInformation(string Colnum,string DATA)
        //{
        //    List<string> LsShp = new List<string>();
        //    DataSet ds = new DataSet();
        //    DataTable dtkeypart = null;
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("SAPDN", typeof(string));
        //    dt.Columns.Add("SHIPDATE", typeof(string));
        //    dt.Columns.Add("CUSTOMER", typeof(string));
        //    DataTable dtz = whs.QueryZ_WIP_TRACKING(Colnum, DATA).Tables[0];
        //    if (dtz.Rows.Count < 1)
        //    {
        //      //  msg = string.Format("传入{0}序号不正确", Colnum);
        //        return null;
        //    }
        //    else
        //    {
        //        string sfcdn = dtz.Rows[0]["LOTOUT"].ToString();
        //        DataTable dtkey = whs.GetProductAllInfo("ESN", dtz.Rows[0]["ESN"].ToString()).Tables[0];
        //        if (dtkey.Rows.Count > 0)
        //        {
        //            dtkeypart = new DataTable();
        //            dtkeypart.Columns.Add("ESN", typeof(string));
        //            dtkeypart.Columns.Add("SNTYPE", typeof(string));
        //            dtkeypart.Columns.Add("SNVAL", typeof(string));
        //            LsShp.Add("ESN:"+dtz.Rows[0]["ESN"].ToString());
        //            foreach (DataRow dr in dtkey.Rows)
        //            {
        //               // dtkeypart.Rows.Add(dr["ESN"].ToString(), dr["SNTYPE"].ToString(), dr["SNVAL"].ToString());
        //                LsShp.Add(dr["SNTYPE"].ToString() + ":" + dr["SNVAL"].ToString());
        //            }
                    
        //        }
        //        if (sfcdn != "NA")
        //        {
        //            DataTable dtsap =whs.GetSAP_DN("SFCLOTCODE", dtz.Rows[0]["LOTOUT"].ToString()).Tables[0];
        //            if (dtsap.Rows.Count > 0)
        //            {
        //                dt.Rows.Add(dtsap.Rows[0]["SAPLOTCODE"].ToString(), dtz.Rows[0]["RECDATE"].ToString(), dtsap.Rows[0]["CUSTOMERNAME2"].ToString());

        //                LsShp.Add("SAPDN:" + dtsap.Rows[0]["SAPLOTCODE"].ToString());
        //                LsShp.Add("SHIPDATE:" + dtz.Rows[0]["RECDATE"].ToString());
        //                LsShp.Add("CUSTOMER:" + dtsap.Rows[0]["CUSTOMERNAME2"].ToString());
        //            }
                  
        //        }                
        //    }
        //    ds.Tables.Add(dt);
        //    ds.Tables.Add(dtkeypart);
        //  //  msg = "OK";
        //    return LsShp;
        //}

        //[WebMethod]
        //public string ShippingInformation_1([XmlText]String param)
        //{
        //    DataSet ds = new DataSet();
        //    DataTable dtkeypart = null;
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("SAPDN", typeof(string));
        //    dt.Columns.Add("SHIPDATE", typeof(string));
        //    dt.Columns.Add("CUSTOMER", typeof(string));
        //    DataTable dtz = whs.QueryZ_WIP_TRACKING(Colnum, DATA).Tables[0];
        //    if (dtz.Rows.Count < 1)
        //    {
        //        //  msg = string.Format("传入{0}序号不正确", Colnum);
        //        return string.Empty;
        //    }
        //    else
        //    {
        //        string sfcdn = dtz.Rows[0]["LOTOUT"].ToString();
        //        DataTable dtkey = whs.GetProductAllInfo("ESN", dtz.Rows[0]["ESN"].ToString()).Tables[0];
        //        if (dtkey.Rows.Count > 0)
        //        {
        //            dtkeypart = new DataTable();
        //            dtkeypart.Columns.Add("ESN", typeof(string));
        //            dtkeypart.Columns.Add("SNTYPE", typeof(string));
        //            dtkeypart.Columns.Add("SNVAL", typeof(string));
        //            foreach (DataRow dr in dtkey.Rows)
        //            {
        //                dtkeypart.Rows.Add(dr["ESN"].ToString(), dr["SNTYPE"].ToString(), dr["SNVAL"].ToString());
        //            }
        //        }
        //        if (sfcdn != "NA")
        //        {
        //            DataTable dtsap = whs.GetSAP_DN("SFCLOTCODE", dtz.Rows[0]["LOTOUT"].ToString()).Tables[0];
        //            if (dtsap.Rows.Count > 0)
        //            {
        //                dt.Rows.Add(dtsap.Rows[0]["SAPLOTCODE"].ToString(), dtz.Rows[0]["RECDATE"].ToString(), dtsap.Rows[0]["CUSTOMERNAME2"].ToString());
        //            }
        //        }
        //    }
        //    ds.Tables.Add(dt);
        //    ds.Tables.Add(dtkeypart);
        //    //  msg = "OK";
        //    return ds.GetXml();
        //}
    }
}
