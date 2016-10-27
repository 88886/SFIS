using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SAP.Middleware.Connector;
using System.Data;

namespace SAPConnect
{
    public class SapConn
    {
        private static RfcConfigParameters rfcCfg = new RfcConfigParameters();
        private static RfcDestination destination = null;

        SapConn()
        {
            #region xxx
            //rfcCfg.Add(RfcConfigParameters.Name, "mycon");
            //rfcCfg.Add(RfcConfigParameters.AppServerHost, "172.16.100.51");
            //rfcCfg.Add(RfcConfigParameters.Client, "300");
            //rfcCfg.Add(RfcConfigParameters.User, "rfc");
            //rfcCfg.Add(RfcConfigParameters.Password, "123456");
            //rfcCfg.Add(RfcConfigParameters.SystemNumber, "00");
            //rfcCfg.Add(RfcConfigParameters.Language, "ZH");
            //rfcCfg.Add(RfcConfigParameters.PoolSize, "5");
            //rfcCfg.Add(RfcConfigParameters.IdleTimeout, "500");
            #endregion

        }

         static SapConn()
         {
             XmlDocument doc = new XmlDocument();
             string XmlName = "DllConfig.xml";
             doc.Load(XmlName);
             rfcCfg.Add(RfcConfigParameters.Name,
                 ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("CONN")).GetAttribute("Name").ToString());
             rfcCfg.Add(RfcConfigParameters.AppServerHost,
                 ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("ASHOST")).GetAttribute("Name").ToString());
             rfcCfg.Add(RfcConfigParameters.Client,
                 ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("CLIENT")).GetAttribute("Name").ToString());
             rfcCfg.Add(RfcConfigParameters.User,
                 ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("USER")).GetAttribute("Name").ToString());
             rfcCfg.Add(RfcConfigParameters.Password,
                 ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("PASSWD")).GetAttribute("Name").ToString());
             rfcCfg.Add(RfcConfigParameters.SystemNumber,
                 ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("SYSNR")).GetAttribute("Name").ToString());
             rfcCfg.Add(RfcConfigParameters.Language,
                 ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("LANG")).GetAttribute("Name").ToString());
             rfcCfg.Add(RfcConfigParameters.PoolSize,
                 ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("POOL_SIZE")).GetAttribute("Name").ToString());
             rfcCfg.Add(RfcConfigParameters.IdleTimeout,
                 ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("TIMEOUT")).GetAttribute("Name").ToString());
             rfcCfg.Add(RfcConfigParameters.LogonGroup,
                 ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("GROUP")).GetAttribute("Name").ToString());
             destination = RfcDestinationManager.GetDestination(rfcCfg);
         }

        /// <summary>
        /// 获取生产工单备料信息(该方法返回一个dataset集合包含两个datatable 第一个是工单备料信息；第二个是当前工单的工艺流程SAP里的)
        /// </summary>
        /// <param name="woid">工单号</param>
        /// <returns></returns>
        public static System.Data.DataSet Get_Z_RFC_ZMM011(string woid)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataTable mDt1 = new System.Data.DataTable("Z_RFC_ZMM011_TB1");
            System.Data.DataTable mDt2 = new System.Data.DataTable("Z_RFC_ZMM011_TB2");

            mDt1.Columns.Add("AUFNR", typeof(string));
            mDt1.Columns.Add("MATNR1", typeof(string));
            mDt1.Columns.Add("MATNR2", typeof(string));
            mDt1.Columns.Add("MAKTX2", typeof(string));
            mDt1.Columns.Add("BDMNG", typeof(Int32));

            mDt2.Columns.Add("VORNR", typeof(string));
            mDt2.Columns.Add("LTXA1", typeof(string));

            IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_ZMM011");

            rfcFunction.SetValue("IMPORT1", woid);

            rfcFunction.Invoke(destination);
            string[] arrTB = new string[] { "OUTPUT1", "OUTPUT2" };

            IRfcTable table = rfcFunction.GetTable(arrTB[0]);
            for (int i = 0; i < table.RowCount; i++)
            {
                if (table[i].GetInt("BDMNG") < 1)
                    continue;
                mDt1.Rows.Add(
                        table[i].GetString("AUFNR").TrimStart('0'),
                        table[i].GetString("MATNR1").TrimStart('0'),
                        table[i].GetString("MATNR2").TrimStart('0'),
                        table[i].GetString("MAKTX2").TrimStart('0'),
                        table[i].GetInt("BDMNG"));
            }
            ds.Tables.Add(mDt1);

            table = rfcFunction.GetTable(arrTB[1]);
            for (int i = 0; i < table.RowCount; i++)
            {
                mDt2.Rows.Add(
                        table[i].GetString("VORNR"),
                        table[i].GetString("LTXA1"));
            }
            ds.Tables.Add(mDt2);

            return ds;
        }
        /// <summary>
        /// 获取生产工单信息
        /// </summary>
        /// <param name="woid">工单号</param>
        /// <returns></returns>
        public static System.Data.DataTable Get_Z_RFC_AFPO(string woid)
        {
            System.Data.DataTable mDt = new System.Data.DataTable("Z_RFC_AFPO");
            mDt.Columns.Add("AUFNR", typeof(string));
            mDt.Columns.Add("PLNUM", typeof(string));
            mDt.Columns.Add("MATNR", typeof(string));
            mDt.Columns.Add("PSMNG", typeof(Int32));//PGMNG
            mDt.Columns.Add("MAKTX", typeof(string));
            mDt.Columns.Add("DAUAT", typeof(string));
            mDt.Columns.Add("DWERK", typeof(string));
            mDt.Columns.Add("GSTRI", typeof(string));
            mDt.Columns.Add("GLTRI", typeof(string));

            IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_AFPO");

            rfcFunction.SetValue("AUFNR", woid);

            rfcFunction.Invoke(destination);
            IRfcTable table = rfcFunction.GetTable("ZRFC_AFPO");

            for (int i = 0; i < table.RowCount; i++)
            {
                mDt.Rows.Add(
                    table[i].GetString("AUFNR"),
                    table[i].GetString("PLNUM"),
                    table[i].GetString("MATNR"),
                    table[i].GetInt("PSMNG"),
                    table[i].GetString("MAKTX"),
                    table[i].GetString("DAUAT"),
                    table[i].GetString("DWERK"),
                    table[i].GetString("GSTRI"),
                    table[i].GetString("GLTRI"));
            }
            return mDt;
        }


       /// <summary>
        /// 根据PO采购订单号返回订单信息
        /// </summary>
        /// <param name="EBELN"></param>
        /// <returns></returns>
        public static System.Data.DataSet Get_Z_RFC_PO(string EBELN)
        {
            string LIFNR = string.Empty;
            string LIFNM = string.Empty;
            string ERNAM = string.Empty;
            string LOEKZ = string.Empty;
            string E_RET = string.Empty;
            System.Data.DataSet mDs = new System.Data.DataSet();
            System.Data.DataTable mDt = new System.Data.DataTable("Z_RFC_PO");
            System.Data.DataTable mtable = new System.Data.DataTable("Header");
            mDt.Columns.Add("EBELP", typeof(string));
            mDt.Columns.Add("LOEKZ", typeof(string));
            mDt.Columns.Add("MATNR", typeof(string));
            mDt.Columns.Add("MAKTX", typeof(string));
            mDt.Columns.Add("MENGE", typeof(string));
            mDt.Columns.Add("MEINS", typeof(string));
            mDt.Columns.Add("MATKL", typeof(string));
            mDt.Columns.Add("WERKS", typeof(string));
            mDt.Columns.Add("LGORT", typeof(string));
            mDt.Columns.Add("RETPO", typeof(string));

            mtable.Columns.Add("EBELN", typeof(string));
            mtable.Columns.Add("LIFNR", typeof(string));
            mtable.Columns.Add("LIFNM", typeof(string));
            mtable.Columns.Add("ERNAM", typeof(string));
            mtable.Columns.Add("LOEKZ", typeof(string));
            mtable.Columns.Add("E_RET", typeof(string));
            IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_PO");

            rfcFunction.SetValue("I_EBELN", EBELN);

            rfcFunction.Invoke(destination);

            LIFNR = rfcFunction.GetValue("LIFNR").ToString();
            LIFNM = rfcFunction.GetValue("LIFNM").ToString();
            ERNAM = rfcFunction.GetValue("ERNAM").ToString();
            LOEKZ = rfcFunction.GetValue("LOEKZ").ToString();
            E_RET = rfcFunction.GetValue("E_RET").ToString();

            IRfcTable table = rfcFunction.GetTable("T_POITEM");
            mtable.Rows.Add(EBELN, LIFNR, LIFNM, ERNAM, LOEKZ, E_RET);
            for (int i = 0; i < table.RowCount; i++)
            {
                mDt.Rows.Add(
                    table[i].GetString("EBELP"),
                    table[i].GetString("LOEKZ"),
                    table[i].GetString("MATNR"),
                    table[i].GetString("MAKTX"),
                    table[i].GetString("MENGE"),
                    table[i].GetString("MEINS"),
                    table[i].GetString("MATKL"),
                    table[i].GetString("WERKS"),
                    table[i].GetString("LGORT"),
                    table[i].GetString("RETPO"));
            }
            mDs.Tables.Add(mDt);
            mDs.Tables.Add(mtable);
            return mDs;
        }
    
    
        /// <summary>
        /// 产品料号BOM信息
        /// </summary>
        /// <param name="partNumber">成品料号</param>
        /// <param name="facCode">工厂代码</param>
        /// <param name="strDate">有效日期(130101)</param>
        /// <returns></returns>
        public static string Get_Z_RFC_ZPP007(string partNumber, string facCode,
            string strDate, out System.Data.DataTable outDataTable)
        {
            //RETURN(c,30)
            outDataTable = new System.Data.DataTable("Z_RFC_ZPP007");
            try
            {
                string err = string.Empty;

                outDataTable.Columns.Add("IDNRK", typeof(string));
                outDataTable.Columns.Add("MFRPN", typeof(string));
                outDataTable.Columns.Add("MAKTX", typeof(string));
                outDataTable.Columns.Add("MENGE", typeof(int));
                outDataTable.Columns.Add("ALPGR", typeof(string));
                outDataTable.Columns.Add("SORTF", typeof(string));
                outDataTable.Columns.Add("REVLV", typeof(string));
                outDataTable.Columns.Add("TEXT1", typeof(string));

                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_ZPP007");

                rfcFunction.SetValue("MATNR", partNumber);
                rfcFunction.SetValue("WERKS", facCode);
                rfcFunction.SetValue("DATE", strDate);
                //rfcFunction.SetValue("REVLV", "A");

                rfcFunction.Invoke(destination);

                err = rfcFunction.GetValue("RETURN").ToString();

                IRfcTable table = rfcFunction.GetTable("T_BOM");

                for (int i = 0; i < table.RowCount; i++)
                {
                    string text1 = table[i].GetString("TEXT1").Trim();
                    string text2 = table[i].GetString("TEXT2").Trim();
                    string text3 = table[i].GetString("TEXT3").Trim();
                    outDataTable.Rows.Add(
                        table[i].GetString("IDNRK").TrimStart('0'),
                        table[i].GetString("MFRPN").TrimStart('0'),
                        table[i].GetString("MAKTX").TrimStart('0'),
                        Convert.ToInt16 (table[i].GetDouble("MENGE")),
                        table[i].GetString("ALPGR").TrimStart('0'),
                          table[i].GetString("SORTF").TrimStart('0'),
                        table[i].GetString("REVLV").TrimStart('0'),
                        string.Format("{0}{1}{2}",
                        string.IsNullOrEmpty(text1) ? "" : text1.Substring(text1.Length - 1, 1) == "," ? text1 : text1 + ",",
                        string.IsNullOrEmpty(text2) ? "" : text1.Substring(text2.Length - 1, 1) == "," ? text2 : text2 + ",",
                        string.IsNullOrEmpty(text2) ? "" : text1.Substring(text3.Length - 1, 1) == "," ? text3 : text3 + ","));
                }
                return string.IsNullOrEmpty(err) ? string.Empty : err;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 出货单信息
        /// </summary>
        /// <param name="PVBELN">发货单号</param>
        /// <returns></returns>
        public static System.Data.DataTable Get_Z_RFC_LIPS(string PVBELN, string MJAHR)
        {
            System.Data.DataTable mDt = new System.Data.DataTable("Z_RFC_LIPS");
            mDt.Columns.Add("VBELN", typeof(string));
            mDt.Columns.Add("MATNR", typeof(string));
            mDt.Columns.Add("MAKTX", typeof(string));
            mDt.Columns.Add("LFIMG", typeof(string));
            mDt.Columns.Add("WERKS", typeof(string));
            mDt.Columns.Add("KUNNR", typeof(string));
            mDt.Columns.Add("NAME1", typeof(string));

            IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_LIPS");

            rfcFunction.SetValue("PVBELN", PVBELN);
            if (!string.IsNullOrEmpty(MJAHR))
                rfcFunction.SetValue("I_MJAHR", MJAHR);

            rfcFunction.Invoke(destination);
            IRfcTable table = rfcFunction.GetTable("IT_LIPS");

            for (int i = 0; i < table.RowCount; i++)
            {
                mDt.Rows.Add(
                    table[i].GetString("VBELN").TrimStart('0'),
                    table[i].GetString("MATNR").TrimStart('0'),
                    table[i].GetString("MAKTX").TrimStart('0'),
                    table[i].GetString("LFIMG").TrimStart('0'),
                    table[i].GetString("WERKS").TrimStart('0'),
                    table[i].GetString("KUNNR").TrimStart('0'),
                    table[i].GetString("NAME1").TrimStart('0'));
            }
            return mDt;


        }

        /// <summary>
        /// IQC收货单信息
        /// </summary>
        /// <param name="IMBLNR">凭证号</param>
        /// <param name="outDataTable">返回的datatable</param>
        /// <returns>返回错误信息,否则返回空</returns>
        public static string Get_Z_RFC_MSEG(string IMBLNR, out System.Data.DataTable outDataTable)
        {
            string err = string.Empty;
            outDataTable = new System.Data.DataTable("Z_RFC_MSEG");
            outDataTable.Columns.Add("MBLNR", typeof(string));
            outDataTable.Columns.Add("MJAHR", typeof(string));
            outDataTable.Columns.Add("ZEILE", typeof(string));
            outDataTable.Columns.Add("MATNR", typeof(string));
            outDataTable.Columns.Add("LIFNR", typeof(string));
            outDataTable.Columns.Add("NAME1", typeof(string));
            outDataTable.Columns.Add("ERFMG", typeof(string));

            IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_MSEG");

            rfcFunction.SetValue("I_MBLNR", IMBLNR);

            rfcFunction.Invoke(destination);


            err = rfcFunction.GetValue("E_MESSAGE").ToString();
            IRfcTable table = rfcFunction.GetTable("T_ZRFC_MSEG");

            for (int i = 0; i < table.RowCount; i++)
            {
                outDataTable.Rows.Add(
                    table[i].GetString("MBLNR"),
                    table[i].GetString("MJAHR"),
                    table[i].GetString("ZEILE"),
                    table[i].GetString("MATNR"),
                    table[i].GetString("LIFNR"),
                    table[i].GetString("NAME1"),
                    table[i].GetString("ERFMG"));
            }
            return string.IsNullOrEmpty(err) ? string.Empty : err;
        }
    }

    public class SapConn2
    {
        private RfcConfigParameters rfcCfg = new RfcConfigParameters();
        private RfcDestination destination = null;
        private string _strError = string.Empty;
        private string ipadd = string.Empty;
        public string GetErrorMsg()
        {
            return _strError;
        }

        public SapConn2()
        {
            //try
            //{         
            //    XmlDocument doc = new XmlDocument();
            //    string XmlName = AppDomain.CurrentDomain.BaseDirectory + "DllConfig.xml";
            //    doc.Load(XmlName);
            //    rfcCfg.Add(RfcConfigParameters.Name,
            //        ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("CONN")).GetAttribute("Name").ToString());
            //    rfcCfg.Add(RfcConfigParameters.AppServerHost,
            //      ipadd =  ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("ASHOST")).GetAttribute("Name").ToString());
            //    rfcCfg.Add(RfcConfigParameters.Client,
            //        ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("CLIENT")).GetAttribute("Name").ToString());
            //    rfcCfg.Add(RfcConfigParameters.User,
            //        ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("USER")).GetAttribute("Name").ToString());
            //    rfcCfg.Add(RfcConfigParameters.Password,
            //        ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("PASSWD")).GetAttribute("Name").ToString());
            //    rfcCfg.Add(RfcConfigParameters.SystemNumber,
            //        ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("SYSNR")).GetAttribute("Name").ToString());
            //    rfcCfg.Add(RfcConfigParameters.Language,
            //        ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("LANG")).GetAttribute("Name").ToString());
            //    rfcCfg.Add(RfcConfigParameters.PoolSize,
            //        ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("POOL_SIZE")).GetAttribute("Name").ToString());
            //    rfcCfg.Add(RfcConfigParameters.IdleTimeout,
            //        ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("TIMEOUT")).GetAttribute("Name").ToString());
            //    destination = RfcDestinationManager.GetDestination(rfcCfg);
            //}
            //catch (Exception ex)
            //{
            //    this._strError = ex.Message;
            //}
        }

        private RfcConfigParameters GetCfgParameters()
        {
            RfcConfigParameters rfcCfg = new RfcConfigParameters();
            XmlDocument doc = new XmlDocument();
            string XmlName = AppDomain.CurrentDomain.BaseDirectory + "DllConfig.xml";
            doc.Load(XmlName);
            rfcCfg.Add(RfcConfigParameters.Name,
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("CONN")).GetAttribute("Name").ToString());
            rfcCfg.Add(RfcConfigParameters.AppServerHost,
              ipadd = ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("ASHOST")).GetAttribute("Name").ToString());
            rfcCfg.Add(RfcConfigParameters.Client,
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("CLIENT")).GetAttribute("Name").ToString());
            rfcCfg.Add(RfcConfigParameters.User,
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("USER")).GetAttribute("Name").ToString());
            rfcCfg.Add(RfcConfigParameters.Password,
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("PASSWD")).GetAttribute("Name").ToString());
            rfcCfg.Add(RfcConfigParameters.SystemNumber,
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("SYSNR")).GetAttribute("Name").ToString());
            rfcCfg.Add(RfcConfigParameters.Language,
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("LANG")).GetAttribute("Name").ToString());
            rfcCfg.Add(RfcConfigParameters.PoolSize,
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("POOL_SIZE")).GetAttribute("Name").ToString());
            rfcCfg.Add(RfcConfigParameters.IdleTimeout,
                ((XmlElement)doc.SelectSingleNode("AutoCreate").SelectSingleNode("ClientSettings").SelectSingleNode("TIMEOUT")).GetAttribute("Name").ToString());
            return rfcCfg;
        }
        /// <summary>
        /// 获取生产工单备料信息(该方法返回一个dataset集合包含两个datatable 第一个是工单备料信息；第二个是当前工单的工艺流程SAP里的)
        /// </summary>
        /// <param name="woid">工单号</param>
        /// <returns></returns>
        public string Get_Z_RFC_ZMM011(string woid)
        {
            this._strError = string.Empty;
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                System.Data.DataTable mDt1 = new System.Data.DataTable("Z_RFC_ZMM011_TB1");
                System.Data.DataTable mDt2 = new System.Data.DataTable("Z_RFC_ZMM011_TB2");

                mDt1.Columns.Add("AUFNR", typeof(string));
                mDt1.Columns.Add("MATNR1", typeof(string));
                mDt1.Columns.Add("MATNR2", typeof(string));
                mDt1.Columns.Add("MAKTX2", typeof(string));
                mDt1.Columns.Add("BDMNG", typeof(Int32));

                mDt2.Columns.Add("VORNR", typeof(string));
                mDt2.Columns.Add("LTXA1", typeof(string));
                RfcDestination _desination = RfcDestinationManager.GetDestination(this.GetCfgParameters());
                IRfcFunction rfcFunction = _desination.Repository.CreateFunction("Z_RFC_ZMM011");

                rfcFunction.SetValue("IMPORT1", woid);
                _strError += ipadd;
                rfcFunction.Invoke(_desination);
                string[] arrTB = new string[] { "OUTPUT1", "OUTPUT2" };

                IRfcTable table = rfcFunction.GetTable(arrTB[0]);
                _strError += "2";
                for (int i = 0; i < table.RowCount; i++)
                {
                    mDt1.Rows.Add(table[i].GetString("AUFNR").TrimStart('0'), table[i].GetString("MATNR1").TrimStart('0'),
                            table[i].GetString("MATNR2").TrimStart('0'), table[i].GetString("MAKTX2").TrimStart('0'),
                            table[i].GetInt("BDMNG"));
                }
                ds.Tables.Add(mDt1);

                table = rfcFunction.GetTable(arrTB[1]);
                for (int i = 0; i < table.RowCount; i++)
                {
                    mDt2.Rows.Add(table[i].GetString("VORNR"), table[i].GetString("LTXA1"));
                }
                ds.Tables.Add(mDt2);

                return "GetDataSetSurrogateZipBytes(ds)" + ds.Tables.Count.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;// this._strError += "SAP Connect Error";
                return null;
            }
        }
        /// <summary>
        /// 获取生产工单信息
        /// </summary>
        /// <param name="woid">工单号</param>
        /// <returns></returns>
        public byte[] Get_Z_RFC_AFPO(string woid)
        {
            this._strError = string.Empty;
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                System.Data.DataTable mDt = new System.Data.DataTable("Z_RFC_AFPO");
                mDt.Columns.Add("AUFNR", typeof(string));
                mDt.Columns.Add("PLNUM", typeof(string));
                mDt.Columns.Add("MATNR", typeof(string));
                mDt.Columns.Add("PGMNG", typeof(string));
                mDt.Columns.Add("MAKTX", typeof(string));
                mDt.Columns.Add("DAUAT", typeof(string));
                mDt.Columns.Add("DWERK", typeof(string));
                mDt.Columns.Add("GSTRI", typeof(string));
                mDt.Columns.Add("GLTRI", typeof(string));

                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_AFPO");

                rfcFunction.SetValue("AUFNR", woid);

                rfcFunction.Invoke(destination);

                IRfcTable table = rfcFunction.GetTable("ZRFC_AFPO");

                for (int i = 0; i < table.RowCount; i++)
                {
                    mDt.Rows.Add(
                        table[i].GetString("AUFNR"),
                        table[i].GetString("PLNUM"),
                        table[i].GetString("MATNR"),
                        table[i].GetString("PGMNG"),
                        table[i].GetString("MAKTX"),
                        table[i].GetString("DAUAT"),
                        table[i].GetString("DWERK"),
                        table[i].GetString("DWERK"),
                        table[i].GetString("GSTRI"),
                        table[i].GetString("GLTRI"));
                }
                ds.Tables.Add(mDt);
                return null;
            }
            catch
            {
                this._strError = "SAP Connect Error";
                return null;
            }
        }

        /// <summary>
        /// 产品料号BOM信息
        /// </summary>
        /// <param name="partNumber">成品料号</param>
        /// <param name="facCode">工厂代码</param>
        /// <param name="strDate">有效日期(130101)</param>
        /// <returns>返回错误信息，否则返回空</returns>
        public byte[] Get_Z_RFC_ZPP007(string partNumber, string facCode, string strDate)
        {
            this._strError = string.Empty;
            System.Data.DataTable outDataTable = new System.Data.DataTable("Z_RFC_ZPP007");
            try
            {
                string err = string.Empty;
                outDataTable.Columns.Add("IDNRK", typeof(string));
                outDataTable.Columns.Add("MFRPN", typeof(string));
                outDataTable.Columns.Add("MAKTX", typeof(string));
                outDataTable.Columns.Add("MENGE", typeof(string));
                outDataTable.Columns.Add("ALPGR", typeof(string));
                outDataTable.Columns.Add("REVLV", typeof(string));
                outDataTable.Columns.Add("TEXT1", typeof(string));

                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_ZPP007");

                rfcFunction.SetValue("MATNR", partNumber);
                rfcFunction.SetValue("WERKS", facCode);
                rfcFunction.SetValue("DATE", strDate);

                rfcFunction.Invoke(destination);

                this._strError = rfcFunction.GetValue("RETURN").ToString();

                IRfcTable table = rfcFunction.GetTable("OUTPUT1");

                for (int i = 0; i < table.RowCount; i++)
                {
                    outDataTable.Rows.Add(
                        table[i].GetString("IDNRK"),
                        table[i].GetString("MFRPN"),
                        table[i].GetString("MAKTX"),
                        table[i].GetString("MENGE"),
                        table[i].GetString("ALPGR"),
                        table[i].GetString("REVLV"),
                        table[i].GetString("TEXT1"));
                }
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.Tables.Add(outDataTable);
                return null;// GetDataSetSurrogateZipBytes(ds);
            }
            catch
            {
                this._strError = "SAP Connect Error";
                return null;
            }
        }

        /// <summary>
        /// 出货单信息
        /// </summary>
        /// <param name="PVBELN">发货单号</param>
        /// <returns></returns>
        public byte[] Get_Z_RFC_LIPS(string PVBELN)
        {
            this._strError = string.Empty;
            try
            {
                System.Data.DataTable mDt = new System.Data.DataTable("Z_RFC_LIPS");
                mDt.Columns.Add("VBELN", typeof(string));
                mDt.Columns.Add("MATNR", typeof(string));
                mDt.Columns.Add("MAKTX", typeof(string));
                mDt.Columns.Add("LFIMG", typeof(string));
                mDt.Columns.Add("WERKS", typeof(string));
                mDt.Columns.Add("KUNNR", typeof(string));
                mDt.Columns.Add("NAME1", typeof(string));

                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_LIPS");

                rfcFunction.SetValue("PVBELN", PVBELN);

                rfcFunction.Invoke(destination);
                IRfcTable table = rfcFunction.GetTable("IT_LIPS");

                for (int i = 0; i < table.RowCount; i++)
                {
                    mDt.Rows.Add(
                        table[i].GetString("VBELN"),
                        table[i].GetString("MATNR"),
                        table[i].GetString("MAKTX"),
                        table[i].GetString("LFIMG"),
                        table[i].GetString("WERKS"),
                        table[i].GetString("KUNNR"),
                        table[i].GetString("NAME1"));
                }
                DataSet ds = new DataSet();
                ds.Tables.Add(mDt);
                return null;// GetDataSetSurrogateZipBytes(ds);
            }
            catch
            {
                this._strError = "SAP Connect Error";
                return null;
            }
        }

        /// <summary>
        /// IQC收货单信息
        /// </summary>
        /// <param name="IMBLNR">凭证号</param>
        /// <param name="outDataTable">返回的datatable</param>
        /// <returns>返回错误信息否则返回空</returns>
        public byte[] Get_Z_RFC_MSEG(string IMBLNR)
        {
            this._strError = string.Empty;
            System.Data.DataTable outDataTable = new System.Data.DataTable("Z_RFC_MSEG");
            try
            {
                outDataTable.Columns.Add("MBLNR", typeof(string));
                outDataTable.Columns.Add("MJAHR", typeof(string));
                outDataTable.Columns.Add("ZEILE", typeof(string));
                outDataTable.Columns.Add("MATNR", typeof(string));
                outDataTable.Columns.Add("LIFNR", typeof(string));
                outDataTable.Columns.Add("NAME1", typeof(string));
                outDataTable.Columns.Add("ERFMG", typeof(string));

                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_MSEG");

                rfcFunction.SetValue("I_MBLNR", IMBLNR);

                rfcFunction.Invoke(destination);

                this._strError = rfcFunction.GetValue("E_MESSAGE").ToString();

                IRfcTable table = rfcFunction.GetTable("T_ZRFC_MSEG");

                for (int i = 0; i < table.RowCount; i++)
                {
                    outDataTable.Rows.Add(
                        table[i].GetString("MBLNR"),
                        table[i].GetString("MJAHR"),
                        table[i].GetString("ZEILE"),
                        table[i].GetString("MATNR"),
                        table[i].GetString("LIFNR"),
                        table[i].GetString("NAME1"),
                        table[i].GetString("ERFMG"));
                }
                DataSet ds = new DataSet();
                ds.Tables.Add(outDataTable);
                return null;// GetDataSetSurrogateZipBytes(ds);
            }
            catch
            {
                this._strError = "SAP Connect Error";
                return null;
            }
        }
  

    }
}