using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using SAP.Middleware.Connector;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Data;
using System.Xml.Serialization;
using GenericUtil;


namespace TestWeserver
{
    /// <summary>
    /// SapConnector 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SapConnector : System.Web.Services.WebService
    {

        MapListConverter mlc = new MapListConverter();
        private string _strError = string.Empty;
        [WebMethod(Description = "获取错误消息,没有错误则为空")]
        public string GetErrorMsg()
        {
            return _strError;
        }

        public SapConnector()
        {
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
            return rfcCfg;
        }
        /// <summary>
        /// 获取生产工单备料信息(该方法返回一个dataset集合包含两个datatable 第一个是工单备料信息；第二个是当前工单的工艺流程SAP里的)
        /// </summary>
        /// <param name="woid">工单号</param>
        /// <returns></returns>
        [WebMethod]
        public byte[] Get_Z_RFC_ZMM011(string woid)
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
                rfcFunction.Invoke(_desination);
                string[] arrTB = new string[] { "OUTPUT1", "OUTPUT2" };

                IRfcTable table = rfcFunction.GetTable(arrTB[0]);

                for (int i = 0; i < table.RowCount; i++)
                {
                    if (table[i].GetInt("BDMNG") < 1)
                        continue;
                    mDt1.Rows.Add(table[i].GetString("AUFNR").TrimStart('0'), table[i].GetString("MATNR1").TrimStart('0'),
                            table[i].GetString("MATNR2").TrimStart('0'), table[i].GetString("MAKTX2").TrimStart('0'),
                            table[i].GetInt("BDMNG"));
                }
                ds.Tables.Add(mDt1);

                table = rfcFunction.GetTable(arrTB[1]);
                for (int i = 0; i < table.RowCount; i++)
                {
                    mDt2.Rows.Add(table[i].GetString("VORNR").TrimStart('0'), table[i].GetString("LTXA1").TrimStart('0'));
                }
                ds.Tables.Add(mDt2);

                return mlc.GetDataSetSurrogateZipBytes(ds);
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        public byte[] Get_Z_RFC_ZMM011_WinCE(string woid)
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
                rfcFunction.Invoke(_desination);
                string[] arrTB = new string[] { "OUTPUT1", "OUTPUT2" };

                IRfcTable table = rfcFunction.GetTable(arrTB[0]);

                for (int i = 0; i < table.RowCount; i++)
                {
                    if (table[i].GetInt("BDMNG") < 1)
                        continue;
                    mDt1.Rows.Add(table[i].GetString("AUFNR").TrimStart('0'), table[i].GetString("MATNR1").TrimStart('0'),
                            table[i].GetString("MATNR2").TrimStart('0'), table[i].GetString("MAKTX2").TrimStart('0'),
                            table[i].GetInt("BDMNG"));
                }
                ds.Tables.Add(mDt1);

                table = rfcFunction.GetTable(arrTB[1]);
                for (int i = 0; i < table.RowCount; i++)
                {
                    mDt2.Rows.Add(table[i].GetString("VORNR").TrimStart('0'), table[i].GetString("LTXA1").TrimStart('0'));
                }
                ds.Tables.Add(mDt2);

                return mlc.GetDataSetZipBytes(ds);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取生产工单信息
        /// </summary>
        /// <param name="woid">工单号</param>
        /// <returns></returns>
        [WebMethod]
        public byte[] Get_Z_RFC_AFPO(string woid)
        {
            this._strError = string.Empty;
            try
            {
                DataSet ds = new DataSet();
                System.Data.DataTable mDt = new System.Data.DataTable("Z_RFC_AFPO");
                mDt.Columns.Add("AUFNR", typeof(string));
                mDt.Columns.Add("PLNUM", typeof(string));
                mDt.Columns.Add("MATNR", typeof(string));
                mDt.Columns.Add("PSMNG", typeof(int));
                mDt.Columns.Add("MAKTX", typeof(string));
                mDt.Columns.Add("DAUAT", typeof(string));
                mDt.Columns.Add("DWERK", typeof(string));
                mDt.Columns.Add("GSTRI", typeof(string));
                mDt.Columns.Add("GLTRI", typeof(string));
                mDt.Columns.Add("STTXT", typeof(string));
                RfcDestination destination = RfcDestinationManager.GetDestination(this.GetCfgParameters());
                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_AFPO");

                rfcFunction.SetValue("AUFNR", woid);

                rfcFunction.Invoke(destination);

                IRfcTable table = rfcFunction.GetTable("ZRFC_AFPO");

                for (int i = 0; i < table.RowCount; i++)
                {
                    mDt.Rows.Add(
                        table[i].GetString("AUFNR").TrimStart('0'),
                        table[i].GetString("PLNUM").TrimStart('0'),
                        table[i].GetString("MATNR").TrimStart('0'),
                        table[i].GetInt("PSMNG"),
                        table[i].GetString("MAKTX").TrimStart('0'),
                        table[i].GetString("DAUAT").TrimStart('0'),
                        table[i].GetString("DWERK").TrimStart('0'),
                        table[i].GetString("GSTRI").TrimStart('0'),
                        table[i].GetString("GLTRI").TrimStart('0'),
                        table[i].GetString("STTXT").TrimStart('0'));
                }
                ds.Tables.Add(mDt);
                return mlc.GetDataSetSurrogateZipBytes(ds);
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
        [WebMethod]
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
                outDataTable.Columns.Add("MENGE", typeof(int));
                outDataTable.Columns.Add("ALPGR", typeof(string));
                outDataTable.Columns.Add("SORTF", typeof(string));
                outDataTable.Columns.Add("REVLV", typeof(string));
                outDataTable.Columns.Add("TEXT1", typeof(string));
                RfcDestination destination = RfcDestinationManager.GetDestination(this.GetCfgParameters());
                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_ZPP007");

                rfcFunction.SetValue("MATNR", partNumber);
                rfcFunction.SetValue("WERKS", facCode);
                rfcFunction.SetValue("DATE", strDate);

                rfcFunction.Invoke(destination);

                this._strError = rfcFunction.GetValue("RETURN").ToString();

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
                       Convert.ToInt16(table[i].GetDouble("MENGE")),
                        table[i].GetString("ALPGR").TrimStart('0'),
                          table[i].GetString("SORTF").TrimStart('0'),
                        table[i].GetString("REVLV").TrimStart('0'),
                        string.Format("{0}{1}{2}",
                        string.IsNullOrEmpty(text1) ? "" : text1.Substring(text1.Length - 1, 1) == "," ? text1 : text1 + ",",
                        string.IsNullOrEmpty(text2) ? "" : text1.Substring(text2.Length - 1, 1) == "," ? text2 : text2 + ",",
                        string.IsNullOrEmpty(text2) ? "" : text1.Substring(text3.Length - 1, 1) == "," ? text3 : text3 + ","));
                }
                DataSet ds = new DataSet();
                ds.Tables.Add(outDataTable);
                return mlc.GetDataSetSurrogateZipBytes(ds);
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
        /// <param name="MJAHR">年度</param>
        /// <returns></returns>
        [WebMethod]
        public byte[] Get_Z_RFC_LIPS(string PVBELN, string MJAHR)
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
                mDt.Columns.Add("NAME1_AG", typeof(string));
                mDt.Columns.Add("BSTKD", typeof(string));
                RfcDestination destination = RfcDestinationManager.GetDestination(this.GetCfgParameters());
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
                        table[i].GetString("NAME1").TrimStart('0'),
                        table[i].GetString("NAME1_AG").TrimStart('0'),
                        table[i].GetString("BSTKD").TrimStart('0'));
                }
                DataSet ds = new DataSet();
                ds.Tables.Add(mDt);
                return mlc.GetDataSetSurrogateZipBytes(ds);
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
        [WebMethod]
        public byte[] Get_Z_RFC_MSEG(string IMBLNR, string MJAHR)
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
                RfcDestination destination = RfcDestinationManager.GetDestination(this.GetCfgParameters());
                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_MSEG");

                rfcFunction.SetValue("I_MBLNR", IMBLNR);
                if (!string.IsNullOrEmpty(MJAHR))
                    rfcFunction.SetValue("I_MJAHR", MJAHR);


                rfcFunction.Invoke(destination);

                this._strError = rfcFunction.GetValue("E_MESSAGE").ToString();

                IRfcTable table = rfcFunction.GetTable("T_ZRFC_MSEG");

                for (int i = 0; i < table.RowCount; i++)
                {
                    outDataTable.Rows.Add(
                        table[i].GetString("MBLNR").TrimStart('0'),
                        table[i].GetString("MJAHR").TrimStart('0'),
                        table[i].GetString("ZEILE").TrimStart('0'),
                        table[i].GetString("MATNR").TrimStart('0'),
                        table[i].GetString("LIFNR").TrimStart('0'),
                        table[i].GetString("NAME1").TrimStart('0'),
                        table[i].GetString("ERFMG").TrimStart('0'));
                }
                DataSet ds = new DataSet();
                ds.Tables.Add(outDataTable);
                return mlc.GetDataSetSurrogateZipBytes(ds);
            }
            catch
            {
                this._strError = "SAP Connect Error";
                return null;
            }
        }

        private string RemoveIndexZero(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str.Substring(0, 1) == "0")
                    str = str.Substring(1, str.Length - 1);
                else
                    break;
            }

            return str;
        }

        /// <summary>
        /// 根据PO采购订单号返回订单信息
        /// </summary>
        /// <param name="EBELN"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] Get_Z_RFC_PO(string EBELN)
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
            RfcDestination destination = RfcDestinationManager.GetDestination(this.GetCfgParameters());
            IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_PO");

            rfcFunction.SetValue("I_EBELN", EBELN);

            rfcFunction.Invoke(destination);

            LIFNR = rfcFunction.GetValue("LIFNR").ToString().TrimStart('0');
            LIFNM = rfcFunction.GetValue("LIFNM").ToString().TrimStart('0');
            ERNAM = rfcFunction.GetValue("ERNAM").ToString().TrimStart('0');
            LOEKZ = rfcFunction.GetValue("LOEKZ").ToString().TrimStart('0');
            E_RET = rfcFunction.GetValue("E_RET").ToString().TrimStart('0');

            IRfcTable table = rfcFunction.GetTable("T_POITEM");
            mtable.Rows.Add(EBELN, LIFNR, LIFNM, ERNAM, LOEKZ, E_RET);
            for (int i = 0; i < table.RowCount; i++)
            {
                mDt.Rows.Add(
                    table[i].GetString("EBELP").TrimStart('0'),
                    table[i].GetString("LOEKZ").TrimStart('0'),
                    table[i].GetString("MATNR").TrimStart('0'),
                    table[i].GetString("MAKTX").TrimStart('0'),
                    table[i].GetString("MENGE").TrimStart('0'),
                    table[i].GetString("MEINS").TrimStart('0'),
                    table[i].GetString("MATKL").TrimStart('0'),
                    table[i].GetString("WERKS").TrimStart('0'),
                    table[i].GetString("LGORT").TrimStart('0'),
                    table[i].GetString("RETPO").TrimStart('0'));
            }
            mDs.Tables.Add(mtable);
            mDs.Tables.Add(mDt);
            return mlc.GetDataSetSurrogateZipBytes(mDs);
        }

        #region  入库接口
        [WebMethod(Description = "SAP入库接口(GM_CODE:SAP的T-code相关02,PSTNG_DATE:过账日期,DOC_DATE:凭证日期,I_TYPE:出入库类型,woId:工单,PartNumber:料号,QTY:数量,MVT_IND:F,PLANT:2100,MOVE_TYPE:101) ")]
        public List<string> Get_Z_RFC_AUFNR_MIGO(string DicRFCAufnrMigo)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(DicRFCAufnrMigo);

            List<string> LsMsg = new List<string>();           
            try
            {
               #region GM_CODE是和SAP的T-code相关
                /*01 MB01
                02 MB31
                03 MB1A
                04 MB1B
                05 MB1C
                06 MB11*/
               #endregion

           
                RfcDestination destination = RfcDestinationManager.GetDestination(this.GetCfgParameters());         
                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_AUFNR_MIGO");

                IRfcStructure IS_HEAD = rfcFunction.GetStructure("IS_HEAD");
                IS_HEAD.SetValue("GM_CODE", "02");
                IS_HEAD.SetValue("PSTNG_DATE", mst.ContainsKey("PSTNG_DATE") ? DateTime.Now.ToString("yyyyMMdd") : mst["PSTNG_DATE"].ToString());
                IS_HEAD.SetValue("DOC_DATE", mst.ContainsKey("DOC_DATE") ? DateTime.Now.ToString("yyyyMMdd") : mst["DOC_DATE"].ToString());
                IS_HEAD.SetValue("HEADER_TXT", mst["EMP_NO"].ToString() + mst["EMP_NAME"].ToString());//人员权限
                rfcFunction.SetValue("IS_HEAD", IS_HEAD);   //设置参数         
               
                IRfcStructure IS_ITEM = rfcFunction.GetStructure("IS_ITEM");
                IS_ITEM.SetValue("MATERIAL", mst["PARTNUMBER"].ToString());
                if (mst.ContainsKey("PLANT"))
                    IS_ITEM.SetValue("PLANT", mst["PLANT"].ToString());
                else
                    IS_ITEM.SetValue("PLANT", "2100");
                IS_ITEM.SetValue("MOVE_TYPE", "101");
                IS_ITEM.SetValue("ENTRY_QNT",Convert.ToInt32( mst["QTY"]));
                IS_ITEM.SetValue("ORDERID", mst["WOID"]);
                IS_ITEM.SetValue("MVT_IND", "F");
              
                rfcFunction.SetValue("IS_ITEM", IS_ITEM);   //设置参数 
                rfcFunction.SetValue("I_TYPE", "1");//1成品入库 2出库

                rfcFunction.Invoke(destination);
             
              string SAP_STOCKNO=  rfcFunction.GetValue("E_MBLNR").ToString().TrimStart('0'); //物料凭证号码      
              IRfcStructure ES_RETURN = rfcFunction.GetStructure("ES_RETURN");
               string SAP_TYPE = ES_RETURN.GetValue("TYPE").ToString(); //是否成功 S 表示成功
               string SAP_E_ID = ES_RETURN.GetValue("ID").ToString();
               string SAP_E_NUM = ES_RETURN.GetValue("NUMBER").ToString();
               string SAP_MSG = ES_RETURN.GetValue("MESSAGE").ToString();

               
                LsMsg.Add(SAP_STOCKNO);
                LsMsg.Add(SAP_TYPE);
                LsMsg.Add(SAP_E_ID);
                LsMsg.Add(SAP_E_NUM);
                LsMsg.Add(SAP_MSG);              
                return LsMsg;              
            }
            catch (Exception ex)
            {
                LsMsg.Add(ex.Message);
                return LsMsg;
            }           
        }
       
        [WebMethod(Description = "SAP出库接口(GM_CODE:SAP的T-code相关03,PSTNG_DATE:过账日期,DOC_DATE:凭证日期,I_TYPE:出入库类型,woId:工单,PartNumber:料号,QTY:数量,MOVE_REAS:0001,PLANT:2100,MOVE_TYPE:261) ")]
        public List<string> Get_Z_RFC_AUFNR_MIGO_OUT(string DicRFCAufnrMigo)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(DicRFCAufnrMigo);
            List<string> LsMsg = new List<string>();
            try
            {
                #region GM_CODE是和SAP的T-code相关
                /*01 MB01
                02 MB31
                03 MB1A
                04 MB1B
                05 MB1C
                06 MB11*/
                #endregion


                RfcDestination destination = RfcDestinationManager.GetDestination(this.GetCfgParameters());
                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_AUFNR_MIGO");

                IRfcStructure IS_HEAD = rfcFunction.GetStructure("IS_HEAD");
                IS_HEAD.SetValue("GM_CODE", "03");
                //IS_HEAD.SetValue("PSTNG_DATE", string.IsNullOrEmpty(RFCAufnrMigo.PSTNG_DATE) ? DateTime.Now.ToString("yyyyMMdd") : RFCAufnrMigo.PSTNG_DATE);
                //IS_HEAD.SetValue("DOC_DATE", string.IsNullOrEmpty(RFCAufnrMigo.DOC_DATE) ? DateTime.Now.ToString("yyyyMMdd") : RFCAufnrMigo.DOC_DATE);
                //IS_HEAD.SetValue("HEADER_TXT", RFCAufnrMigo.EMP_NO + RFCAufnrMigo.EMP_NAME);//人员权限
                IS_HEAD.SetValue("PSTNG_DATE", mst.ContainsKey("PSTNG_DATE") ? DateTime.Now.ToString("yyyyMMdd") : mst["PSTNG_DATE"].ToString());
                IS_HEAD.SetValue("DOC_DATE", mst.ContainsKey("DOC_DATE") ? DateTime.Now.ToString("yyyyMMdd") : mst["DOC_DATE"].ToString());
                IS_HEAD.SetValue("HEADER_TXT", mst["EMP_NO"].ToString() + mst["EMP_NAME"].ToString());//人员权限
                rfcFunction.SetValue("IS_HEAD", IS_HEAD);   //设置参数         

                IRfcStructure IS_ITEM = rfcFunction.GetStructure("IS_ITEM");
                IS_ITEM.SetValue("MATERIAL", mst["PARTNUMBER"].ToString());
                IS_ITEM.SetValue("PLANT", mst["PLANT"].ToString());
                IS_ITEM.SetValue("MOVE_TYPE", "Z10");
                IS_ITEM.SetValue("ENTRY_QNT",Convert.ToInt32( mst["QTY"]));
                IS_ITEM.SetValue("ORDERID", mst["WOID"].ToString());
                IS_ITEM.SetValue("MOVE_REAS","0001");
              
                rfcFunction.SetValue("IS_ITEM", IS_ITEM);   //设置参数 
                rfcFunction.SetValue("I_TYPE", "2");//1成品入库 //2出库

                rfcFunction.Invoke(destination);

                string SAP_STOCKNO = rfcFunction.GetValue("E_MBLNR").ToString().TrimStart('0'); //物料凭证号码      
                IRfcStructure ES_RETURN = rfcFunction.GetStructure("ES_RETURN");
                string SAP_TYPE = ES_RETURN.GetValue("TYPE").ToString(); //是否成功 S 表示成功
                string SAP_E_ID = ES_RETURN.GetValue("ID").ToString();
                string SAP_E_NUM = ES_RETURN.GetValue("NUMBER").ToString();
                string SAP_MSG = ES_RETURN.GetValue("MESSAGE").ToString();


                LsMsg.Add(SAP_STOCKNO);
                LsMsg.Add(SAP_TYPE);
                LsMsg.Add(SAP_E_ID);
                LsMsg.Add(SAP_E_NUM);
                LsMsg.Add(SAP_MSG);
                return LsMsg;
            }
            catch (Exception ex)
            {
                LsMsg.Add(ex.Message);
                return LsMsg;
            }

        }

        [WebMethod(Description = "SAP检查数入库接口(GM_CODE:SAP的T-code相关02,PSTNG_DATE:过账日期,DOC_DATE:凭证日期,I_TYPE:出入库类型,woId:工单,PartNumber:料号,QTY:数量,MVT_IND:F,PLANT:2100,MOVE_TYPE:101) ")]
        public List<string> CHK_STOCKIN_Z_RFC_AUFNR_MIGO(string DicRFCAufnrMigo)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(DicRFCAufnrMigo);
            List<string> LsMsg = new List<string>();
            try
            {
                #region GM_CODE是和SAP的T-code相关
                /*01 MB01
                02 MB31
                03 MB1A
                04 MB1B
                05 MB1C
                06 MB11*/
                #endregion


                RfcDestination destination = RfcDestinationManager.GetDestination(this.GetCfgParameters());
                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_AUFNR_MIGO");

                IRfcStructure IS_HEAD = rfcFunction.GetStructure("IS_HEAD");
                IS_HEAD.SetValue("GM_CODE",  "02" );
                //IS_HEAD.SetValue("PSTNG_DATE", string.IsNullOrEmpty(RFCAufnrMigo.PSTNG_DATE) ? DateTime.Now.ToString("yyyyMMdd") : RFCAufnrMigo.PSTNG_DATE);
                //IS_HEAD.SetValue("DOC_DATE", string.IsNullOrEmpty(RFCAufnrMigo.DOC_DATE) ? DateTime.Now.ToString("yyyyMMdd") : RFCAufnrMigo.DOC_DATE);
                //IS_HEAD.SetValue("HEADER_TXT", RFCAufnrMigo.EMP_NO + RFCAufnrMigo.EMP_NAME);//人员权限
                IS_HEAD.SetValue("PSTNG_DATE", DateTime.Now.ToString("yyyyMMdd") );
                IS_HEAD.SetValue("DOC_DATE",  DateTime.Now.ToString("yyyyMMdd") );
                IS_HEAD.SetValue("HEADER_TXT", mst["EMP_NO"].ToString() + mst["EMP_NAME"].ToString());//人员权限
                rfcFunction.SetValue("IS_HEAD", IS_HEAD);   //设置参数         

                IRfcStructure IS_ITEM = rfcFunction.GetStructure("IS_ITEM");
                IS_ITEM.SetValue("MATERIAL", mst["PARTNUMBER"].ToString());

                if (mst.ContainsKey("PLANT"))
                IS_ITEM.SetValue("PLANT", mst["PLANT"].ToString());
                else
                IS_ITEM.SetValue("PLANT", "2100");

                IS_ITEM.SetValue("MOVE_TYPE", "101");
                IS_ITEM.SetValue("ENTRY_QNT",Convert.ToInt32( mst["QTY"]));
                IS_ITEM.SetValue("ORDERID", mst["WOID"].ToString());
                IS_ITEM.SetValue("MVT_IND", "F" );
              
                rfcFunction.SetValue("IS_ITEM", IS_ITEM);   //设置参数 
                rfcFunction.SetValue("I_TYPE", "1");//1成品入库 2出库
                rfcFunction.SetValue("TESTRUN", "X"); //检查入库,不过账

                rfcFunction.Invoke(destination);

                string SAP_STOCKNO = rfcFunction.GetValue("E_MBLNR").ToString().TrimStart('0'); //物料凭证号码      
                IRfcStructure ES_RETURN = rfcFunction.GetStructure("ES_RETURN");
                string SAP_TYPE = ES_RETURN.GetValue("TYPE").ToString(); //是否成功 S 表示成功
                string SAP_E_ID = ES_RETURN.GetValue("ID").ToString();
                string SAP_E_NUM = ES_RETURN.GetValue("NUMBER").ToString();
                string SAP_MSG = ES_RETURN.GetValue("MESSAGE").ToString();


                LsMsg.Add(SAP_STOCKNO);
                LsMsg.Add(SAP_TYPE);
                LsMsg.Add(SAP_E_ID);
                LsMsg.Add(SAP_E_NUM);
                LsMsg.Add(SAP_MSG);
                return LsMsg;
            }
            catch (Exception ex)
            {
                LsMsg.Add(ex.Message);
                return LsMsg;
            }
        }
        #endregion

        [WebMethod(Description = "成品出货回抛")]
        public List<string> Get_Z_RFC_GD_DELIVERY(string I_VBELN)
        {
            List<string> LsMsg = new List<string>();
            try
            {
                RfcDestination destination = RfcDestinationManager.GetDestination(this.GetCfgParameters());
                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_GD_DELIVERY");                                                                                 
                rfcFunction.SetValue("I_VBELN", I_VBELN);
                rfcFunction.Invoke(destination);

                IRfcStructure E_RETURN = rfcFunction.GetStructure("E_RETURN");
                string SAP_TYPE = E_RETURN.GetValue("TYPE").ToString(); //是否成功 S 表示成功
                string SAP_E_ID = E_RETURN.GetValue("ID").ToString();
                string SAP_E_NUM = E_RETURN.GetValue("NUMBER").ToString();
                string SAP_MSG = E_RETURN.GetValue("MESSAGE").ToString();
                LsMsg.Add(SAP_TYPE);
                LsMsg.Add(SAP_E_ID);
                LsMsg.Add(SAP_E_NUM);
                LsMsg.Add(SAP_MSG); 
            }
            catch (Exception ex)
            {
                LsMsg.Add("ERR:"+ex.Message);                
            } 

            return LsMsg;
        }
        [WebMethod(Description = "成品移库使用")]
        public List<string> WHS_MOVE_Z_RFC_AUFNR_MIGO(string DicRFCAufnrMigo, string TestFlag)
        {
            IDictionary<string, object> mst = MapListConverter.JsonToDictionary(DicRFCAufnrMigo);
            List<string> LsMsg = new List<string>();
            try
            {
                #region GM_CODE是和SAP的T-code相关
                /*01 MB01
                02 MB31
                03 MB1A
                04 MB1B
                05 MB1C
                06 MB11*/
                #endregion


                RfcDestination destination = RfcDestinationManager.GetDestination(this.GetCfgParameters());
                IRfcFunction rfcFunction = destination.Repository.CreateFunction("Z_RFC_AUFNR_MIGO");

                IRfcStructure IS_HEAD = rfcFunction.GetStructure("IS_HEAD");
                IS_HEAD.SetValue("GM_CODE", "04");
                //IS_HEAD.SetValue("PSTNG_DATE", string.IsNullOrEmpty(RFCAufnrMigo.PSTNG_DATE) ? DateTime.Now.ToString("yyyyMMdd") : RFCAufnrMigo.PSTNG_DATE);
                //IS_HEAD.SetValue("DOC_DATE", string.IsNullOrEmpty(RFCAufnrMigo.DOC_DATE) ? DateTime.Now.ToString("yyyyMMdd") : RFCAufnrMigo.DOC_DATE);
                //IS_HEAD.SetValue("HEADER_TXT", RFCAufnrMigo.EMP_NO + RFCAufnrMigo.EMP_NAME);//人员权限
                IS_HEAD.SetValue("PSTNG_DATE", mst.ContainsKey("PSTNG_DATE") ? DateTime.Now.ToString("yyyyMMdd") : mst["PSTNG_DATE"].ToString());
                IS_HEAD.SetValue("DOC_DATE", mst.ContainsKey("DOC_DATE") ? DateTime.Now.ToString("yyyyMMdd") : mst["DOC_DATE"].ToString());
                IS_HEAD.SetValue("HEADER_TXT", mst["EMP_NO"].ToString() + mst["EMP_NAME"].ToString());//人员权限
                rfcFunction.SetValue("IS_HEAD", IS_HEAD);   //设置参数         

                IRfcStructure IS_ITEM = rfcFunction.GetStructure("IS_ITEM");
                IS_ITEM.SetValue("MATERIAL", mst["PARTNUMBER"].ToString());
                IS_ITEM.SetValue("PLANT", mst["PLANT"].ToString());
                IS_ITEM.SetValue("STGE_LOC", mst["STGE_LOC"].ToString());// RFCAufnrMigo.STGE_LOC); //转出仓
                IS_ITEM.SetValue("MOVE_TYPE", "311");//仓库之间转移
                IS_ITEM.SetValue("ENTRY_QNT",Convert.ToInt32( mst["STGE_LOC"]));// RFCAufnrMigo.QTY);
                IS_ITEM.SetValue("MOVE_PLANT", mst["MOVE_PLANT"].ToString());
                IS_ITEM.SetValue("MOVE_STLOC", mst["MOVE_STLOC"].ToString());// RFCAufnrMigo.MOVE_STLOC);
                rfcFunction.SetValue("IS_ITEM", IS_ITEM);   //设置参数 

                rfcFunction.SetValue("I_TYPE", "3");//1成品入库 2出库  03 移库
                if (!string.IsNullOrEmpty(TestFlag))
                    rfcFunction.SetValue("TESTRUN", "X"); //检查入库,不过账

                rfcFunction.Invoke(destination);

                string SAP_STOCKNO = rfcFunction.GetValue("E_MBLNR").ToString().TrimStart('0'); //物料凭证号码      
                IRfcStructure ES_RETURN = rfcFunction.GetStructure("ES_RETURN");
                string SAP_TYPE = ES_RETURN.GetValue("TYPE").ToString(); //是否成功 S 表示成功
                string SAP_E_ID = ES_RETURN.GetValue("ID").ToString();
                string SAP_E_NUM = ES_RETURN.GetValue("NUMBER").ToString();
                string SAP_MSG = ES_RETURN.GetValue("MESSAGE").ToString();


                LsMsg.Add(SAP_STOCKNO);
                LsMsg.Add(SAP_TYPE);
                LsMsg.Add(SAP_E_ID);
                LsMsg.Add(SAP_E_NUM);
                LsMsg.Add(SAP_MSG);
                return LsMsg;
            }
            catch (Exception ex)
            {
                LsMsg.Add(ex.Message);
                return LsMsg;
            }
        }

       
    }
}
