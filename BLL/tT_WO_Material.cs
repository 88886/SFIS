using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericProvider;
using System.Data.Common;
using SrvComponent;
using System.Data;

namespace BLL
{
    public class tT_WO_Material
    {
        public tT_WO_Material()
        {
        }

        string table = "SFCR.T_WO_MATERIAL";

        public string Insert_T_WO_Material(string TrSn, string UserID,string STATUS)
        {          
            try
            {

                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);

                int count = 0;
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("TR_SN", TrSn);
                DataTable dt = dp.GetData("SFCR.R_TR_SN", "TR_SN,KP_NO,VENDER_ID,DATE_CODE,LOT_CODE,QTY,WOID", mst, out count).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    mst = new Dictionary<string, object>();
                    mst.Add("TR_SN", dt.Rows[0]["TR_SN"].ToString());
                    mst.Add("KP_NO", dt.Rows[0]["KP_NO"].ToString());
                    mst.Add("VENDER_ID", dt.Rows[0]["VENDER_ID"].ToString());
                    mst.Add("DATE_CODE", dt.Rows[0]["DATE_CODE"].ToString());
                    mst.Add("LOT_CODE", dt.Rows[0]["LOT_CODE"].ToString());
                    mst.Add("QTY", Convert.ToInt32(dt.Rows[0]["QTY"].ToString()));
                    mst.Add("WOID", dt.Rows[0]["WOID"].ToString());
                    mst.Add("USER_ID", UserID);
                    mst.Add("STATUS", STATUS);
                    dp.AddData(table, mst);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Delete_T_WO_Material(string TrSn,string woId)
        {
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                IDictionary<string, object> mst = new Dictionary<string, object>();
                mst.Add("TR_SN", TrSn);
                mst.Add("WOID", woId);
                dp.DeleteData(table, mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Insert_T_WO_Material(IDictionary<string,object> mst )
        {
            try
            {
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);
                dp.AddData(table, mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UPDATE_T_WO_Material(IDictionary<string, object> mst)
        {
            try
            {
                List<string> tableKey = new List<string>();
                tableKey.Add("TR_SN");
                tableKey.Add("WOID");
                IAdminProvider dp = (IAdminProvider)DpFactory.Create(typeof(IAdminProvider), DpFactory.ADMIN);     
                if (mst.ContainsKey("STATUS"))
                    tableKey.Add("STATUS");
                dp.UpdateData(table, tableKey.ToArray(), mst);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



    }
}
