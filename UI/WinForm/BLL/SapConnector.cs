using System;
using System.Collections.Generic;
using System.Text;
//using SAP.Middleware.Connector;
using System.Xml;

namespace FrmBLL
{
    //public class SapConnector
    //{
    //    private static RfcConfigParameters rfcCfg = new RfcConfigParameters();
    //    private static RfcDestination destination = null;

    //    SapConnector()
    //    {

    //    }

    //    static SapConnector()
    //    {
    //        XmlDocument doc = new XmlDocument();
    //        string XmlName = "DllConfig.xml";
    //        doc.Load(XmlName);
    //        rfcCfg.Add(RfcConfigParameters.Name,
    //            ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("CONN")).GetAttribute("Name").ToString());
    //        rfcCfg.Add(RfcConfigParameters.AppServerHost,
    //            ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("ASHOST")).GetAttribute("Name").ToString());
    //        rfcCfg.Add(RfcConfigParameters.Client,
    //            ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("CLIENT")).GetAttribute("Name").ToString());
    //        rfcCfg.Add(RfcConfigParameters.User,
    //            ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("USER")).GetAttribute("Name").ToString());
    //        rfcCfg.Add(RfcConfigParameters.Password,
    //            ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("PASSWD")).GetAttribute("Name").ToString());
    //        rfcCfg.Add(RfcConfigParameters.SystemNumber,
    //            ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("SYSNR")).GetAttribute("Name").ToString());
    //        rfcCfg.Add(RfcConfigParameters.Language,
    //            ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("LANG")).GetAttribute("Name").ToString());
    //        rfcCfg.Add(RfcConfigParameters.PoolSize,
    //            ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("POOL_SIZE")).GetAttribute("Name").ToString());
    //        rfcCfg.Add(RfcConfigParameters.IdleTimeout,
    //            ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("TIMEOUT")).GetAttribute("Name").ToString());
    //        destination = RfcDestinationManager.GetDestination(rfcCfg);
    //    }
    //    /// <summary>
    //    /// 获取生产工单备料信息(该方法返回一个dataset集合包含两个datatable 第一个是工单备料信息；第二个是当前工单的工艺流程SAP里的)
    //    /// </summary>
    //    /// <param name="woid">工单号</param>
    //    /// <returns></returns>
    //    public static System.Data.DataSet Get_Z_RFC_ZMM011(string woid)
    //    {
    //        try
    //        {
    //            System.Data.DataSet ds = new System.Data.DataSet();
    //            System.Data.DataTable mDt1 = new System.Data.DataTable("Z_RFC_ZMM011_TB1");
    //            System.Data.DataTable mDt2 = new System.Data.DataTable("Z_RFC_ZMM011_TB2");

    //            mDt1.Columns.Add("AUFNR", typeof(string));
    //            mDt1.Columns.Add("MATNR1", typeof(string));
    //            mDt1.Columns.Add("MATNR2", typeof(string));
    //            mDt1.Columns.Add("MAKTX2", typeof(string));
    //            mDt1.Columns.Add("BDMNG", typeof(Int32));

    //            mDt2.Columns.Add("VORNR", typeof(string));
    //            mDt2.Columns.Add("LTXA1", typeof(string));

    //            IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_ZMM011");

    //            rfcFunction.SetValue("IMPORT1", woid);

    //            rfcFunction.Invoke(destination);
    //            string[] arrTB = new string[] { "OUTPUT1", "OUTPUT2" };

    //            IRfcTable table = rfcFunction.GetTable(arrTB[0]);
    //            for (int i = 0; i < table.RowCount; i++)
    //            {
    //                mDt1.Rows.Add(table[i].GetString("AUFNR").TrimStart('0'), table[i].GetString("MATNR1").TrimStart('0'),
    //                        table[i].GetString("MATNR2").TrimStart('0'), table[i].GetString("MAKTX2").TrimStart('0'),
    //                        table[i].GetInt("BDMNG"));
    //            }
    //            ds.Tables.Add(mDt1);

    //            table = rfcFunction.GetTable(arrTB[1]);
    //            for (int i = 0; i < table.RowCount; i++)
    //            {
    //                mDt2.Rows.Add(table[i].GetString("VORNR"), table[i].GetString("LTXA1"));
    //            }
    //            ds.Tables.Add(mDt2);

    //            return ds;
    //        }
    //        catch
    //        {
    //            throw new Exception("SAP Connect Error");
    //        }
    //    }
    //    /// <summary>
    //    /// 获取生产工单信息
    //    /// </summary>
    //    /// <param name="woid">工单号</param>
    //    /// <returns></returns>
    //    public static System.Data.DataTable Get_Z_RFC_AFPO(string woid)
    //    {
    //        try
    //        {
    //            System.Data.DataTable mDt = new System.Data.DataTable("Z_RFC_AFPO");
    //            mDt.Columns.Add("AUFNR", typeof(string));
    //            mDt.Columns.Add("PLNUM", typeof(string));
    //            mDt.Columns.Add("MATNR", typeof(string));
    //            mDt.Columns.Add("PGMNG", typeof(string));
    //            mDt.Columns.Add("MAKTX", typeof(string));
    //            mDt.Columns.Add("DAUAT", typeof(string));
    //            mDt.Columns.Add("DWERK", typeof(string));
    //            mDt.Columns.Add("GSTRI", typeof(string));
    //            mDt.Columns.Add("GLTRI", typeof(string));

    //            IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_AFPO");

    //            rfcFunction.SetValue("AUFNR", woid);

    //            rfcFunction.Invoke(destination);

    //            IRfcTable table = rfcFunction.GetTable("ZRFC_AFPO");

    //            for (int i = 0; i < table.RowCount; i++)
    //            {
    //                mDt.Rows.Add(
    //                    table[i].GetString("AUFNR"),
    //                    table[i].GetString("PLNUM"),
    //                    table[i].GetString("MATNR"),
    //                    table[i].GetString("PGMNG"),
    //                    table[i].GetString("MAKTX"),
    //                    table[i].GetString("DAUAT"),
    //                    table[i].GetString("DWERK"),
    //                    table[i].GetString("DWERK"),
    //                    table[i].GetString("GSTRI"),
    //                    table[i].GetString("GLTRI"));
    //            }
    //            return mDt;
    //        }
    //        catch
    //        {
    //            throw new Exception("SAP Connect Error");
    //        }
    //    }

    //    /// <summary>
    //    /// 产品料号BOM信息
    //    /// </summary>
    //    /// <param name="partNumber">成品料号</param>
    //    /// <param name="facCode">工厂代码</param>
    //    /// <param name="strDate">有效日期(130101)</param>
    //    /// <returns>返回错误信息，否则返回空</returns>
    //    public static string Get_Z_RFC_ZPP007(string partNumber, string facCode, string strDate,
    //        out System.Data.DataTable outDataTable)
    //    {
    //        outDataTable = new System.Data.DataTable("Z_RFC_ZPP007");
    //        try
    //        {
    //            string err = string.Empty;
    //            outDataTable.Columns.Add("IDNRK", typeof(string));
    //            outDataTable.Columns.Add("MFRPN", typeof(string));
    //            outDataTable.Columns.Add("MAKTX", typeof(string));
    //            outDataTable.Columns.Add("MENGE", typeof(string));
    //            outDataTable.Columns.Add("ALPGR", typeof(string));
    //            outDataTable.Columns.Add("REVLV", typeof(string));
    //            outDataTable.Columns.Add("TEXT1", typeof(string));

    //            IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_ZPP007");

    //            rfcFunction.SetValue("MATNR", partNumber);
    //            rfcFunction.SetValue("WERKS", facCode);
    //            rfcFunction.SetValue("DATE", strDate);

    //            rfcFunction.Invoke(destination);

    //            err = rfcFunction.GetValue("RETURN").ToString();

    //            IRfcTable table = rfcFunction.GetTable("OUTPUT1");

    //            for (int i = 0; i < table.RowCount; i++)
    //            {
    //                outDataTable.Rows.Add(
    //                    table[i].GetString("IDNRK"),
    //                    table[i].GetString("MFRPN"),
    //                    table[i].GetString("MAKTX"),
    //                    table[i].GetString("MENGE"),
    //                    table[i].GetString("ALPGR"),
    //                    table[i].GetString("REVLV"),
    //                    table[i].GetString("TEXT1"));
    //            }
    //            return string.IsNullOrEmpty(err) ? string.Empty : err;
    //        }
    //        catch
    //        {
    //            return "SAP Connect Error";
    //        }
    //    }

    //    /// <summary>
    //    /// 出货单信息
    //    /// </summary>
    //    /// <param name="PVBELN">发货单号</param>
    //    /// <returns></returns>
    //    public static System.Data.DataTable Get_Z_RFC_LIPS(string PVBELN)
    //    {
    //        try
    //        {
    //            System.Data.DataTable mDt = new System.Data.DataTable("Z_RFC_LIPS");
    //            mDt.Columns.Add("VBELN", typeof(string));
    //            mDt.Columns.Add("MATNR", typeof(string));
    //            mDt.Columns.Add("MAKTX", typeof(string));
    //            mDt.Columns.Add("LFIMG", typeof(string));
    //            mDt.Columns.Add("WERKS", typeof(string));
    //            mDt.Columns.Add("KUNNR", typeof(string));
    //            mDt.Columns.Add("NAME1", typeof(string));

    //            IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_LIPS");

    //            rfcFunction.SetValue("PVBELN", PVBELN);

    //            rfcFunction.Invoke(destination);
    //            IRfcTable table = rfcFunction.GetTable("IT_LIPS");

    //            for (int i = 0; i < table.RowCount; i++)
    //            {
    //                mDt.Rows.Add(
    //                    table[i].GetString("VBELN"),
    //                    table[i].GetString("MATNR"),
    //                    table[i].GetString("MAKTX"),
    //                    table[i].GetString("LFIMG"),
    //                    table[i].GetString("WERKS"),
    //                    table[i].GetString("KUNNR"),
    //                    table[i].GetString("NAME1"));
    //            }
    //            return mDt;
    //        }
    //        catch
    //        {
    //            throw new Exception("SAP Connect Error");
    //        }
    //    }

    //    /// <summary>
    //    /// IQC收货单信息
    //    /// </summary>
    //    /// <param name="IMBLNR">凭证号</param>
    //    /// <param name="outDataTable">返回的datatable</param>
    //    /// <returns>返回错误信息否则返回空</returns>
    //    public static string Get_Z_RFC_MSEG(string IMBLNR,
    //        out System.Data.DataTable outDataTable)
    //    {
    //        outDataTable = new System.Data.DataTable("Z_RFC_MSEG");
    //        try
    //        {
    //            string err = string.Empty;
    //            outDataTable.Columns.Add("MBLNR", typeof(string));
    //            outDataTable.Columns.Add("MJAHR", typeof(string));
    //            outDataTable.Columns.Add("ZEILE", typeof(string));
    //            outDataTable.Columns.Add("MATNR", typeof(string));
    //            outDataTable.Columns.Add("LIFNR", typeof(string));
    //            outDataTable.Columns.Add("NAME1", typeof(string));
    //            outDataTable.Columns.Add("ERFMG", typeof(string));

    //            IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_MSEG");

    //            rfcFunction.SetValue("I_MBLNR", IMBLNR);

    //            rfcFunction.Invoke(destination);

    //            err = rfcFunction.GetValue("E_MESSAGE").ToString();

    //            IRfcTable table = rfcFunction.GetTable("T_ZRFC_MSEG");

    //            for (int i = 0; i < table.RowCount; i++)
    //            {
    //                outDataTable.Rows.Add(
    //                    table[i].GetString("MBLNR"),
    //                    table[i].GetString("MJAHR"),
    //                    table[i].GetString("ZEILE"),
    //                    table[i].GetString("MATNR"),
    //                    table[i].GetString("LIFNR"),
    //                    table[i].GetString("NAME1"),
    //                    table[i].GetString("ERFMG"));
    //            }
    //            return string.IsNullOrEmpty(err) ? string.Empty : err;
    //        }
    //        catch
    //        {
    //            return "SAP Connect Error";
    //        }
    //    }
    //}
}
